﻿using System;
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
    public class upimg : IHttpHandler
    {
        /// <summary>
        /// 首次上传照片，进行缩放
        /// 如果图片较小，则不用缩放
        /// </summary>
        private Img ZoomImg = new Img() { Width = 1200, Height = 840 };
        /// <summary>
        /// 裁剪大小
        /// 5 寸 5x3.5 12.7*8.9 1200x840以上 100万像素
        /// </summary>
        private Img CropImg = new Img() { Width = 1200, Height = 840 };

        private string fileTypes = "jpg,jpeg,png";

        private HttpRequest Request = null;
        private HttpResponse Response = null;
        private HttpContext Context = null;
        public void ProcessRequest(HttpContext context)
        {
            Context = context; Request = context.Request; Response = context.Response;
            Response.ContentType = "application/json";
            string action = context.Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action.ToLower().Trim())
                {
                    case "crop":
                        CropImage(context);
                        break;
                    case "upimg":
                        UpImage(context);
                        break;
                }
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        private void UpImage(HttpContext context)
        {
            try
            {
                string upimgs = context.Server.MapPath("~/upimages/original/");
                string zoomurl = context.Server.MapPath("~/upimages/zoom/");
                HttpPostedFile file = context.Request.Files[0];
                if (file == null) { OutPut(false, "请选择图片！"); return; }
                String fileName = file.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();
                ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));
                if (Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1)) == -1) { OutPut(false, "图片格式不符合规定！"); return; }

                int countBytes = file.ContentLength;
                byte[] buffer = new byte[countBytes];
                //文件保存路径 完整路径
                string imagekey = DateTime.Now.Ticks.ToString();
                string savepath = /*MD5Helper.Md5Hash32(buffer)*/ imagekey + fileExt;
                string fullpath = upimgs + savepath;
                if (!Directory.Exists(upimgs)) Directory.CreateDirectory(upimgs);
                if (File.Exists(fullpath) == false) file.SaveAs(fullpath);
                Bitmap sourceBmp = new Bitmap(fullpath);
                Image zoomPhoto = Image.FromStream(ImageClass.ResizeImage(
                    sourceBmp,
                    Convert.ToInt32(ZoomImg.Width),
                    Convert.ToInt32(ZoomImg.Height)
                ));
                if (!Directory.Exists(zoomurl)) Directory.CreateDirectory(zoomurl);
                zoomPhoto.Save(zoomurl + imagekey + fileExt);
                string json = "{\"result\":true,\"imagekey\":\"" + imagekey + "\",\"fileExt\":\"" + fileExt + "\"}";
                Dictionary<string, object> fileDict = new Dictionary<string, object>();
                fileDict.Add("imagekey", imagekey);
                fileDict.Add("fileExt", fileExt);
                OutPut(true, fileDict);
            }
            catch (Exception ex)
            {
                OutPut(false, "文件上传失败");
            }
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        private void CropImage(HttpContext context)
        {
            bool Check = false;
            float viewPortW = GetFloat("viewPortW", out Check); if (!Check) goto CheckFaill;
            float viewPortH = GetFloat("viewPortH", out Check); if (!Check) goto CheckFaill;
            float imageX = GetFloat("imageX", out Check); if (!Check) goto CheckFaill;
            float imageY = GetFloat("imageY", out Check); if (!Check) goto CheckFaill;
            float imageRotate = GetFloat("imageRotate", out Check); if (!Check) goto CheckFaill;
            float imageW = GetFloat("imageW", out Check); if (!Check) goto CheckFaill;
            float imageH = GetFloat("imageH", out Check); if (!Check) goto CheckFaill;
            float selectorX = GetFloat("selectorX", out Check); if (!Check) goto CheckFaill;
            float selectorY = GetFloat("selectorY", out Check); if (!Check) goto CheckFaill;
            float selectorW = GetFloat("selectorW", out Check); if (!Check) goto CheckFaill;
            float selectorH = GetFloat("selectorH", out Check); if (!Check) goto CheckFaill;
            string imageSource = Request["imageSource"]; if (string.IsNullOrEmpty(imageSource)) goto CheckFaill;
            string ImgKey = Request["ImgKey"]; if (string.IsNullOrEmpty(imageSource)) goto CheckFaill;

            imageW = (float)CropImg.Width;
            imageH = (float)CropImg.Height;
            selectorW = (float)CropImg.Width;
            selectorH = (float)CropImg.Height;
            //To Values
            float pWidth = imageW;
            float pHeight = imageH;

            Bitmap img = (Bitmap)Bitmap.FromFile(context.Server.MapPath("/upimages/original/" + ImgKey + ".jpg"));
            //Original Values
            int _width = img.Width;
            int _height = img.Height;

            //Resize
            Bitmap image_p = ImageClass.ResizeBitmap(img, Convert.ToInt32(pWidth), Convert.ToInt32(pHeight));

            int widthR = image_p.Width;
            int heightR = image_p.Height;
            //Rotate if angle is not 0.00 or 360
            if (imageRotate > 0.0F && imageRotate < 360.00F)
            {
                image_p = (Bitmap)RotateImage(image_p, (double)imageRotate);
                pWidth = image_p.Width;
                pHeight = image_p.Height;
            }

            //Calculate Coords of the Image into the ViewPort
            float src_x = 0;
            float dst_x = 0;
            float src_y = 0;
            float dst_y = 0;

            if (pWidth > viewPortW)
            {
                src_x = (float)Math.Abs(imageX - Math.Abs((imageW - pWidth) / 2));
                dst_x = 0;
            }
            else
            {
                src_x = 0;
                dst_x = (float)(imageX + ((imageW - pWidth) / 2));
            }
            if (pHeight > viewPortH)
            {
                src_y = (float)Math.Abs(imageY - Math.Abs((imageH - pHeight) / 2));
                dst_y = 0;
            }
            else
            {
                src_y = 0;
                dst_y = (float)(imageY + ((imageH - pHeight) / 2));
            }


            //Get Image viewed into the ViewPort
            image_p = ImageCopy(image_p, dst_x, dst_y, src_x, src_y, viewPortW, viewPortH, pWidth, pHeight);
            //image_p.Save(context.Server.MapPath("test_viewport.jpg"));
            //Get Selector Portion
            image_p = ImageCopy(image_p, 0, 0, selectorX, selectorY, selectorW, selectorH, viewPortW, viewPortH);
            string FileName = String.Format("test{0}.jpg", DateTime.Now.Ticks.ToString());
            image_p.Save(context.Server.MapPath(FileName));


            image_p.Dispose();
            img.Dispose();
            //imgSelector.Dispose();
            OutPut(true, FileName);
            return;

            CheckFaill:
                OutPut("Fail");
                    return;
        }

        private float GetFloat(string name, out bool check)
        {
            float Value = 0;
            check = float.TryParse(Request[name], out Value);
            return Value;
        }

        private void OutPut(bool result, string message)
        {

            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(new JsonResult(result,message) , writer);
            OutPut(writer.ToString());
        }

        private void OutPut(bool result, Dictionary<string,object> _obj)
        {

            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(new JsonResult(result, "", _obj), writer);
            OutPut(writer.ToString());
        }

        private void OutPut(JsonResult model)
        {

            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(model, writer);
            OutPut(writer.ToString());
        }

        private void OutPut(string message)
        {
            Response.Clear();
            Response.Write(message);
        }

        #region 裁剪图片

        private Bitmap ImageCopy(Bitmap srcBitmap, float dst_x, float dst_y, float src_x, float src_y, float dst_width, float dst_height, float src_width, float src_height)
        {
            // Create the new bitmap and associated graphics object
            RectangleF SourceRec = new RectangleF(src_x, src_y, dst_width, dst_height);
            RectangleF DestRec = new RectangleF(dst_x, dst_y, dst_width, dst_height);
            Bitmap bmp = new Bitmap(Convert.ToInt32(dst_width), Convert.ToInt32(dst_height));
            Graphics g = Graphics.FromImage(bmp);
            // Draw the specified section of the source bitmap to the new one
            g.DrawImage(srcBitmap, DestRec, SourceRec, GraphicsUnit.Pixel);
            // Clean up
            g.Dispose();

            // Return the bitmap
            return bmp;

        }


        private Bitmap ResizeImage(Bitmap img, int width, int height)
        {
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(GetThumbAbort);
            return (Bitmap)img.GetThumbnailImage(width, height, callback, System.IntPtr.Zero);

        }

        public bool GetThumbAbort()
        {
            return false;
        }

        /// <summary>
        /// method to rotate an image either clockwise or counter-clockwise
        /// </summary>
        /// <param name="img">the image to be rotated</param>
        /// <param name="rotationAngle">the angle (in degrees).
        /// NOTE: 
        /// Positive values will rotate clockwise
        /// negative values will rotate counter-clockwise
        /// </param>
        /// <returns></returns>
        private Image RotateImage(Bitmap img, double rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform((float)rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
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