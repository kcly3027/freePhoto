﻿using Aspose.Words;
using freePhoto.Tools;
using freePhoto.Web.AppCode;
using freePhoto.Web.DbHandle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;

namespace freePhoto.Web
{
    public class Img
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    /// <summary>
    /// upimg 的摘要说明
    /// </summary>
    public class upimg : BaseHandler
    {
        private string[] fileTypes = new string[] { "jpg", "jpeg", "png", "gif", "doc", "docx" };

        protected override void ProcessFunction()
        {
            string action = Request["action"];
            string result = "";
            if (!string.IsNullOrEmpty(action))
            {
                switch (action.ToLower().Trim())
                {
                    case "upfile":
                        result = UpFile(Context);
                        break;
                }
            }
            OutPut(result);
        }

        private string UpFile(HttpContext context)
        {
            try
            {
                string baseupdir = context.Server.MapPath("~/upfile/");
                HttpPostedFile file = context.Request.Files[0];
                if (file == null) { return ToJson(false, "请选择文件！"); }
                String fileName = file.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();
                if (Array.IndexOf(fileTypes, fileExt.Substring(1)) == -1) { return ToJson(false, "文件不符合规定！"); }

                int countBytes = file.ContentLength;
                byte[] buffer = new byte[countBytes];
                //文件保存路径 完整路径
                string filekey = IdentityGenerator.Instance.NextIdentity();
                string savepath = filekey + fileExt;
                string fullpath = baseupdir + savepath;
                if (!Directory.Exists(baseupdir)) Directory.CreateDirectory(baseupdir);
                if (File.Exists(fullpath) == false) file.SaveAs(fullpath);

                int filecount = 1;
                if (OrderTools.IsImage(fileExt))
                {
                    /*处理图片*/
                    ConvertImg(context, fullpath, filekey, fileExt);
                    filecount = 1;
                }
                if (OrderTools.IsWord(fileExt))
                {
                    /*处理word*/
                    ConvertPdf(context, fullpath, filekey, out filecount);
                }

                OrderDAL.CreateUpfileHistory(filekey, savepath, filecount, fileExt, fileName);
                Dictionary<string, object> fileDict = new Dictionary<string, object>();
                fileDict.Add("FileKey", filekey);
                fileDict.Add("FileExt", fileExt);
                fileDict.Add("FileCount", filecount);
                fileDict.Add("PreviewUrl", OrderTools.GetPreview(fileExt) + "filekey=" + filekey);
                return ToJson(true, fileDict);
            }
            catch (Exception ex)
            {
                return ToJson(false, "文件上传失败");
            }
        }

        private bool ConvertImg(HttpContext context, string path, string imagekey, string fileExt)
        {
            try
            {
                string zoomurl = context.Server.MapPath("~/convertimg/");
                Img ZoomImg = new Img() { Width = 1200, Height = 840 };
                Bitmap sourceBmp = new Bitmap(path);
                int width = sourceBmp.Width, height = sourceBmp.Height;
                if (width > ZoomImg.Width || height > ZoomImg.Height)
                {
                    if (width > height)
                    {
                        width = (int)ZoomImg.Width;
                        height = Convert.ToInt32((Convert.ToDouble(width) / Convert.ToDouble(sourceBmp.Width)) * height);
                    }
                    else
                    {
                        width = Convert.ToInt32((Convert.ToDouble(height) / Convert.ToDouble(sourceBmp.Height)) * width);
                        height = (int)ZoomImg.Height;
                    }
                }
                Image zoomPhoto = Image.FromStream(ImageClass.ResizeImage(
                    sourceBmp,
                    Convert.ToInt32(width),
                    Convert.ToInt32(height)
                ));
                if (!Directory.Exists(zoomurl)) Directory.CreateDirectory(zoomurl);
                zoomPhoto.Save(zoomurl + imagekey + fileExt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ConvertPdf(HttpContext context, string path, string imagekey,out int filecount)
        {
            try
            {
                string pdfurl = context.Server.MapPath("~/convertpdf/");
                Document doc = new Document(path);
                filecount = doc.PageCount;
                if (!Directory.Exists(pdfurl)) Directory.CreateDirectory(pdfurl);
                doc.SaveToPdf(pdfurl + imagekey + ".pdf");
                return true;
            }
            catch
            {
                filecount = 0;
                return false;
            }
        }
    }
}