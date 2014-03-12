using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
namespace freePhoto.Tools
{
    /// <summary>
    ///  VerifyCode 的摘要说明
    /// </summary>
    public class VerifyCode
    {
        private const double PI = 0.0;
        private const double PI2 = 0.0;
        private bool AZ09 = true;
        private int length = 6;
        private int fontSize = 16;
        private int padding = 2;
        private bool chaos = true;
        private Color chaosColor = Color.LightGray;
        private Color backgroundColor = Color.White;
        private Color[] colors = new Color[]
		{
			Color.Black,
			Color.Red,
			Color.DarkBlue,
			Color.Green,
			Color.Orange,
			Color.Brown,
			Color.DarkCyan,
			Color.Purple
		};
        private string[] fonts = new string[]
		{
			"Arial"
		};
        private string codeSerial = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        /// <summary>
        /// 是否英文字符（如果否为汉字）
        /// </summary>
        public bool Az09
        {
            get
            {
                return this.AZ09;
            }
            set
            {
                this.AZ09 = value;
            }
        }
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize
        {
            get
            {
                return this.fontSize;
            }
            set
            {
                this.fontSize = value;
            }
        }
        /// <summary>
        /// 边框补大小
        /// </summary>
        public int Padding
        {
            get
            {
                return this.padding;
            }
            set
            {
                this.padding = value;
            }
        }
        /// <summary>
        /// 是否输出燥点(默认不输出)
        /// </summary>
        public bool Chaos
        {
            get
            {
                return this.chaos;
            }
            set
            {
                this.chaos = value;
            }
        }
        /// <summary>
        /// 输出燥点的颜色(默认灰色)
        /// </summary>
        public Color ChaosColor
        {
            get
            {
                return this.chaosColor;
            }
            set
            {
                this.chaosColor = value;
            }
        }
        /// <summary>
        /// 自定义背景色(默认白色)
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
            }
        }
        /// <summary>
        /// 设置获取随机颜色数组
        /// </summary>
        public Color[] Colors
        {
            get
            {
                return this.colors;
            }
            set
            {
                this.colors = value;
            }
        }
        /// <summary>
        /// 自定义字体数组
        /// </summary>
        public string[] Fonts
        {
            get
            {
                return this.fonts;
            }
            set
            {
                this.fonts = value;
            }
        }
        /// <summary>
        /// 随机字符串序列
        /// </summary>
        public string CodeSerial
        {
            get
            {
                return this.codeSerial;
            }
            set
            {
                this.codeSerial = value;
            }
        }
        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap bitmap = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
            graphics.Dispose();
            double num = bXDir ? ((double)bitmap.Height) : ((double)bitmap.Width);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    double num2 = bXDir ? (0.0 * (double)j / num) : (0.0 * (double)i / num);
                    num2 += dPhase;
                    double num3 = Math.Sin(num2);
                    int num4 = bXDir ? (i + (int)(num3 * dMultValue)) : i;
                    int num5 = bXDir ? j : (j + (int)(num3 * dMultValue));
                    Color pixel = srcBmp.GetPixel(i, j);
                    if (num4 >= 0 && num4 < bitmap.Width && num5 >= 0 && num5 < bitmap.Height)
                    {
                        bitmap.SetPixel(num4, num5, pixel);
                    }
                }
            }
            return bitmap;
        }
        /// <summary>
        /// 生成校验码图片
        /// </summary>
        /// <param name="code">生成的文字</param>
        /// <returns></returns>
        public Bitmap CreateImageCode(string code)
        {
            int num = this.FontSize;
            int num2 = num + this.Padding;
            int width = code.Length * num2 + 9 + this.Padding;
            int num3 = num * 2 + this.Padding;
            if (this.AZ09)
            {
                width = code.Length * num2 + 4 + this.Padding;
                num3 = num + 4 + this.Padding;
            }
            Bitmap bitmap = new Bitmap(width, num3);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(this.BackgroundColor);
            Random random = new Random();
            if (this.Chaos)
            {
                Pen pen = new Pen(this.ChaosColor, 0f);
                int num4 = this.Length * 10;
                for (int i = 0; i < num4; i++)
                {
                    int x = random.Next(bitmap.Width);
                    int y = random.Next(bitmap.Height);
                    graphics.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            int num5 = num3 - this.FontSize - this.Padding * 2;
            int num6 = num5 / 4;
            int num7 = num6;
            int num8 = num6 * 2;
            for (int i = 0; i < code.Length; i++)
            {
                int num9 = random.Next(this.Colors.Length - 1);
                int num10 = random.Next(this.Fonts.Length - 1);
                Font font = new Font(this.Fonts[num10], (float)num, FontStyle.Bold);
                Brush brush = new SolidBrush(this.Colors[num9]);
                int num11;
                if (i % 2 == 1)
                {
                    num11 = num8;
                }
                else
                {
                    num11 = num7;
                }
                int num12 = i * num2;
                if (this.AZ09)
                {
                    num12 += 2;
                    num11 -= 2;
                }
                graphics.DrawString(code.Substring(i, 1), font, brush, (float)num12, (float)num11);
            }
            graphics.DrawRectangle(new Pen(Color.Gainsboro, 0f), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
            graphics.Dispose();
            return this.TwistImage(bitmap, true, 0.0, 0.0);
        }
        /// <summary>
        /// 将创建好的图片输出到页面
        /// </summary>
        /// <param name="code">字符串</param>
        /// <param name="context">输出</param>
        public void CreateImageOnPage(string code, HttpContext context)
        {
            Bitmap bitmap = this.CreateImageCode(code);
            bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            context.Response.Flush();
            bitmap.Dispose();
        }
        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen">长度</param>
        /// <returns></returns>
        public string CreateVerifyCode(int codeLen)
        {
            string text = "";
            if (codeLen == 0)
            {
                codeLen = this.Length;
            }
            if (this.AZ09)
            {
                string[] array = this.CodeSerial.Split(new char[]
				{
					','
				});
                Random random = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < codeLen; i++)
                {
                    int num = random.Next(0, array.Length - 1);
                    text += array[num];
                }
            }
            else
            {
                Encoding encoding = Encoding.GetEncoding("gb2312");
                object[] array2 = VerifyCode.CreateRegionCode(4);
                for (int i = 0; i < array2.Length; i++)
                {
                    text += encoding.GetString((byte[])Convert.ChangeType(array2[i], typeof(byte[])));
                }
            }
            return text;
        }
        /// <summary>
        /// 生成随机字符（验证码长度）
        /// </summary>
        /// <returns></returns>
        public string CreateVerifyCode()
        {
            return this.CreateVerifyCode(0);
        }
        private static object[] CreateRegionCode(int strlength)
        {
            string[] array = new string[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"a",
				"b",
				"c",
				"d",
				"e",
				"f"
			};
            Random random = new Random();
            object[] array2 = new object[strlength];
            for (int i = 0; i < strlength; i++)
            {
                int num = random.Next(11, 14);
                string str = array[num].Trim();
                random = new Random(num * (int)DateTime.Now.Ticks + i);
                int num2;
                if (num == 13)
                {
                    num2 = random.Next(0, 7);
                }
                else
                {
                    num2 = random.Next(0, 16);
                }
                string str2 = array[num2].Trim();
                random = new Random(num2 * (int)DateTime.Now.Ticks + i);
                int num3 = random.Next(10, 16);
                string str3 = array[num3].Trim();
                random = new Random(num3 * (int)DateTime.Now.Ticks + i);
                int num4;
                if (num3 == 10)
                {
                    num4 = random.Next(1, 16);
                }
                else
                {
                    if (num3 == 15)
                    {
                        num4 = random.Next(0, 15);
                    }
                    else
                    {
                        num4 = random.Next(0, 16);
                    }
                }
                string str4 = array[num4].Trim();
                byte b = Convert.ToByte(str + str2, 16);
                byte b2 = Convert.ToByte(str3 + str4, 16);
                byte[] value = new byte[]
				{
					b,
					b2
				};
                array2.SetValue(value, i);
            }
            return array2;
        }
    }
}
