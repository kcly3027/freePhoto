using freePhoto.Tools;
using freePhoto.Web.AppCode;
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
        /// <summary>
        /// 裁剪大小
        /// </summary>
        private Img CropZoom = new Img() { Width = 430, Height = 301 };

        private string fileTypes = "jpg,jpeg,png";

        protected override void ProcessFunction()
        {
            string action = Request["action"];
            string result = "";
            if (!string.IsNullOrEmpty(action))
            {
                switch (action.ToLower().Trim())
                {
                    case "crop":
                        result = CropImage(Context);
                        break;
                    case "cropzoom":
                        result = CropZoomImage(Context);
                        break;
                    case "upimg":
                        result = UpImage(Context);
                        break;
                }
            }
            OutPut(result);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        private string UpImage(HttpContext context)
        {
            try
            {
                string upimgs = context.Server.MapPath("~/upimages/original/");
                string zoomurl = context.Server.MapPath("~/upimages/zoom/");
                HttpPostedFile file = context.Request.Files[0];
                if (file == null) { return ToJson(false, "请选择图片！"); }
                String fileName = file.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();
                ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));
                if (Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1)) == -1) { return ToJson(false, "图片格式不符合规定！"); }

                int countBytes = file.ContentLength;
                byte[] buffer = new byte[countBytes];
                //文件保存路径 完整路径
                string imagekey = Md5.Md5Hash32(buffer);
                string savepath = imagekey + fileExt;
                string fullpath = upimgs + savepath;
                if (!Directory.Exists(upimgs)) Directory.CreateDirectory(upimgs);
                if (File.Exists(fullpath) == false) file.SaveAs(fullpath);

                Bitmap sourceBmp = new Bitmap(fullpath);
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
                //string json = "{\"result\":true,\"imagekey\":\"" + imagekey + "\",\"fileExt\":\"" + fileExt + "\"}";
                Dictionary<string, object> fileDict = new Dictionary<string, object>();
                fileDict.Add("imagekey", imagekey);
                fileDict.Add("fileExt", fileExt);
                return ToJson(true, fileDict);
            }
            catch (Exception ex)
            {
                return ToJson(false, "文件上传失败");
            }
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        private string CropZoomImage(HttpContext context)
        {
            bool Check = false;
            float scale = 2;

            float viewPortW = GetFloat("viewPortW", out Check) * scale; if (!Check) goto CheckFail;
            float viewPortH = GetFloat("viewPortH", out Check) * scale; if (!Check) goto CheckFail;
            float imageX = GetFloat("imageX", out Check) * scale; if (!Check) goto CheckFail;
            float imageY = GetFloat("imageY", out Check) * scale; if (!Check) goto CheckFail;
            float imageRotate = GetFloat("imageRotate", out Check); if (!Check) goto CheckFail;
            float imageW = GetFloat("imageW", out Check) * scale; if (!Check) goto CheckFail;
            float imageH = GetFloat("imageH", out Check) * scale; if (!Check) goto CheckFail;
            float selectorX = GetFloat("selectorX", out Check) * scale; if (!Check) goto CheckFail;
            float selectorY = GetFloat("selectorY", out Check) * scale; if (!Check) goto CheckFail;
            float selectorW = GetFloat("selectorW", out Check) * scale; if (!Check) goto CheckFail;
            float selectorH = GetFloat("selectorH", out Check) * scale; if (!Check) goto CheckFail;
            string imageSource = Request["imageSource"]; if (string.IsNullOrEmpty(imageSource)) goto CheckFail;
            string ImgKey = Request["ImgKey"]; if (string.IsNullOrEmpty(imageSource)) goto CheckFail;

            //To Values
            float pWidth = imageW;
            float pHeight = imageH;

            //Bitmap img = (Bitmap)Bitmap.FromFile(context.Server.MapPath("/upimages/original/" + ImgKey + ".jpg"));
            Bitmap img = (Bitmap)Bitmap.FromFile(context.Server.MapPath(imageSource));
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
                image_p = (Bitmap)RotateImg(image_p, (double)imageRotate);
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
            image_p.Save(context.Server.MapPath("/upimages/showcrop/" + ImgKey + ".jpg"));


            image_p.Dispose();
            img.Dispose();
            //imgSelector.Dispose();
            return ToJson(true, ImgKey);

        CheckFail:
            return ToJson(false, "");
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        private string CropImage(HttpContext context)
        {
            bool Check = false;
            float scale = (float)(CropImg.Width / CropZoom.Width);

            float viewPortW = GetFloat("viewPortW", out Check) * scale; if (!Check) goto CheckFail;
            float viewPortH = GetFloat("viewPortH", out Check) * scale; if (!Check) goto CheckFail;
            float imageX = GetFloat("imageX", out Check) * scale; if (!Check) goto CheckFail;
            float imageY = GetFloat("imageY", out Check) * scale; if (!Check) goto CheckFail;
            float imageRotate = GetFloat("imageRotate", out Check); if (!Check) goto CheckFail;
            float imageW = GetFloat("imageW", out Check) * scale; if (!Check) goto CheckFail;
            float imageH = GetFloat("imageH", out Check) * scale; if (!Check) goto CheckFail;
            float selectorX = GetFloat("selectorX", out Check) * scale; if (!Check) goto CheckFail;
            float selectorY = GetFloat("selectorY", out Check) * scale; if (!Check) goto CheckFail;
            float selectorW = GetFloat("selectorW", out Check) * scale; if (!Check) goto CheckFail;
            float selectorH = GetFloat("selectorH", out Check) * scale; if (!Check) goto CheckFail;
            string imageSource = Request["imageSource"]; if (string.IsNullOrEmpty(imageSource)) goto CheckFail;
            string ImgKey = Request["ImgKey"]; if (string.IsNullOrEmpty(imageSource)) goto CheckFail;

            //To Values
            float pWidth = imageW;
            float pHeight = imageH;

            Bitmap img = (Bitmap)Bitmap.FromFile(context.Server.MapPath("/upimages/original/" + ImgKey + ".jpg"));
            //Bitmap img = (Bitmap)Bitmap.FromFile(context.Server.MapPath(imageSource));
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
                image_p = (Bitmap)RotateImg(image_p, (double)imageRotate);
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
            image_p.Save(context.Server.MapPath("/upimages/crop/" + ImgKey + ".jpg"));


            image_p.Dispose();
            img.Dispose();
            //imgSelector.Dispose();
            return ToJson(true, ImgKey);

        CheckFail:
            return ToJson(false, "");
        }

        private float GetFloat(string name, out bool check)
        {
            float Value = 0;
            check = float.TryParse(Request[name], out Value);
            return Value;
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

        /*
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
        */

        /// <summary>

        /// 以逆时针为方向对图像进行旋转

        /// </summary>

        /// <param name="b">位图流</param>

        /// <param name="angle">旋转角度[0,360](前台给的)</param>

        /// <returns></returns>

        public Image RotateImg(Bitmap b, double angle)
        {

            angle = 0 - angle % 360;



            //弧度转换

            double radian = angle * Math.PI / 180.0;

            double cos = Math.Cos(radian);

            double sin = Math.Sin(radian);



            //原图的宽和高

            int w = b.Width;

            int h = b.Height;

            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));

            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));



            //目标位图

            Bitmap dsImage = new Bitmap(W, H);

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;



            //计算偏移量

            Point Offset = new Point((W - w) / 2, (H - h) / 2);



            //构造图像显示区域：让图像的中心与窗口的中心点一致

            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);

            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);



            g.TranslateTransform(center.X, center.Y);

            g.RotateTransform((float)(360 - angle));



            //恢复图像在水平和垂直方向的平移

            g.TranslateTransform(-center.X, -center.Y);

            g.DrawImage(b, rect);



            //重至绘图的所有变换

            g.ResetTransform();



            g.Save();

            g.Dispose();

            //保存旋转后的图片

            b.Dispose();

            //dsImage.Save("FocusPoint.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return dsImage;

        }

        #endregion
    }
}