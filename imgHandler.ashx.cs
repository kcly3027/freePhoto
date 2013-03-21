using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using KnetApps.Toolkit.Web;
using System.IO;
using System.Collections;
using KnetApps.Toolkit.Encrypt;
using System.Web.Script.Serialization;
using System.Drawing;
using KnetApps.Toolkit;
using KnetApps.CanYin.Manager.BaseCode;

namespace KnetApps.CanYin.Manager
{
    /// <summary>
    /// imgHandler 的摘要说明
    /// </summary>
    public class imgHandler : IHttpHandler, IRequiresSessionState
    {
        String fileTypes = "jpg,jpeg,png";
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["cmd"] == "UpImage")
            {
                UploadImg(context);
            }
            if (context.Request["cmd"] == "CropImg")
            {
                CropImg(context);
            }
        }

        void UploadImg(HttpContext context)
        {
            try
            {
                string upimgs = context.Server.MapPath("~/upimgs/");
                HttpPostedFile file = context.Request.Files[0];
                if (file == null)
                    Output(true, "请选择图片！", context);
                String fileName = file.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();
                ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));
                if (Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1)) == -1)
                    Output(true, "图片格式不符合规定！", context);

                int countBytes = file.ContentLength;
                byte[] buffer = new byte[countBytes];
                //文件保存路径 完整路径
                string savepath = MD5Helper.Md5Hash32(buffer) + fileExt;
                string fullpath = upimgs + savepath;
                //string imgUrl = "upimgs/" + savepath;
                if (File.Exists(fullpath) == false) file.SaveAs(fullpath);
                Output(false, savepath, context);
            }
            catch (Exception ex)
            {
                XLog.XTrace.WriteLine(ex.Message);
            }
        }

        void CropImg(HttpContext context)
        {
            string Pic = Convert.ToString(context.Request["p"]);
            int PointX = Convert.ToInt32(context.Request["x"]);
            int PointY = Convert.ToInt32(context.Request["y"]);
            int CutWidth = Convert.ToInt32(context.Request["w"]);
            int CutHeight = Convert.ToInt32(context.Request["h"]);
            int PicWidth = Convert.ToInt32(context.Request["pw"]);
            int PicHeight = Convert.ToInt32(context.Request["ph"]);
            int targetWidth = Convert.ToInt32(context.Request["tw"]);
            int targetHeight = Convert.ToInt32(context.Request["th"]);
            Pic = context.Server.MapPath("~/upimgs/") + Pic;

            Bitmap sourceBmp = new Bitmap(Pic);
            Image zoomPhoto = Image.FromStream(ImageClass.ResizeImage(
                sourceBmp,
                Convert.ToInt32((Convert.ToDouble(PicWidth) / Convert.ToDouble(CutWidth)) * targetWidth),
                Convert.ToInt32((Convert.ToDouble(PicHeight) / Convert.ToDouble(CutHeight)) * targetHeight)
            ));

            Bitmap bmPhoto = new Bitmap(targetWidth, targetHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
            gbmPhoto.DrawImage(zoomPhoto, new Rectangle(0, 0, targetWidth, targetHeight), PointX * zoomPhoto.Width / PicWidth, PointY * zoomPhoto.Height / PicHeight, targetWidth, targetHeight, GraphicsUnit.Pixel);

            string fileName = DateTime.Now.Ticks.ToString() + ".jpg";
            bmPhoto.Save(context.Server.MapPath("/cropimgs/" + fileName), System.Drawing.Imaging.ImageFormat.Jpeg);

            zoomPhoto.Dispose();
            gbmPhoto.Dispose();
            bmPhoto.Dispose();

            Output(false, fileName, context);
        }

        #region Log/Output
        public static void Log(string Message, Exception ex)
        {
            if (Message != null)
                XLog.XTrace.WriteLine("error：{0}\r\n", Message);
            if (ex != null)
                XLog.XTrace.WriteLine("exception：{0}\r\n  {1}", ex.Message, ex.StackTrace);
        }
        public static void Output(bool error, string message, HttpContext context)
        {
            Result result = new Result(error, message);
            Output(result, context);
        }
        public static void Output(Result result, HttpContext context)
        {
            Output(JsonSerializer.ToJson(result), context);
        }
        public static void Output(string s, HttpContext context)
        {
            context.Response.Clear();
            context.Response.Write(s);
            context.Response.End();
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}