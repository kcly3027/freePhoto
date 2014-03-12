using System;
using System.Text;
using System.Web;
namespace freePhoto.Tools
{
    /// <summary>
    /// 函数方法取值
    /// </summary>
    public class Fetch
    {
        /// <summary>
        /// 获得客户端的IP地址
        /// </summary>
        public static string UserIp
        {
            get
            {
                string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (text == null || text == string.Empty)
                {
                    text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                string result;
                if (!Regxp.IsIp(text))
                {
                    result = "Unknown";
                }
                else
                {
                    result = text;
                }
                return result;
            }
        }
        /// <summary>
        /// 获得客户使用的浏览器
        /// </summary>
        public static string UserBrowser
        {
            get
            {
                string text = HttpContext.Current.Request.UserAgent;
                string result;
                if (text == null || text == string.Empty)
                {
                    result = "Unknown";
                }
                else
                {
                    text = text.ToLower();
                    HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
                    if (text.Contains("firefox") || text.Contains("firebird") || text.Contains("myie") || text.Contains("opera") || text.Contains("netscape") || text.Contains("msie"))
                    {
                        result = browser.Browser + browser.Version;
                    }
                    else
                    {
                        result = "Unknown";
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 返回前面路径
        /// </summary>
        public static string FrontPath
        {
            get
            {
                string text = HttpContext.Current.Request.Path;
                text = text.Substring(0, text.LastIndexOf("/"));
                string result;
                if (text == "/")
                {
                    result = string.Empty;
                }
                else
                {
                    result = text;
                }
                return result;
            }
        }
        /// <summary>
        /// 获得上级路径
        /// </summary>
        public static string CurrentPath
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            }
        }
        /// <summary>
        /// 得到当前URL地址(如果为伪静态地址就返回伪静态地址) 已HttpUtility.UrlEncode编码
        /// </summary>
        public static string CurrentUrl
        {
            get
            {
                string text = "";
                try
                {
                    text = HttpContext.Current.Request.Url.ToString();
                    string text2 = HttpContext.Current.Request.RawUrl.ToString();
                    if (text2.Length > 0)
                    {
                        if (!text.Equals(text2))
                        {
                            text = text2;
                        }
                    }
                }
                catch (Exception)
                {
                }
                return HttpUtility.UrlEncode(text);
            }
        }
        /// <summary>
        /// 得到当前URL地址 （带域名。取不到伪静态地址）已HttpUtility.UrlEncode编码
        /// </summary>
        /// <returns></returns>
        public static string CurrentUrl_Http
        {
            get
            {
                string str = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                return HttpUtility.UrlEncode(str);
            }
        }
        /// <summary>
        /// 获得上次页面地址 已HttpUtility.HtmlEncode编码
        /// </summary>
        public static string Referrer
        {
            get
            {
                Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;
                string result;
                if (urlReferrer == null)
                {
                    result = string.Empty;
                }
                else
                {
                    result = HttpUtility.HtmlEncode(Convert.ToString(urlReferrer));
                }
                return result;
            }
        }
        /// <summary>
        /// 是否从其他连接向本域名以POST方式提交表单
        /// </summary>
        public static bool IsGetFromAnotherDomain
        {
            get
            {
                return !(HttpContext.Current.Request.HttpMethod == "POST") && !HttpUtility.HtmlDecode(Fetch.Referrer).Contains(Fetch.ServerDomain());
            }
        }
        /// <summary>
        /// 取FROM POST传值
        /// </summary>
        /// <param name="name">要取的控件值</param>
        /// <returns></returns>
        public static string post(string name)
        {
            string text = "";
            try
            {
                text = HttpContext.Current.Request.Form[name];
            }
            catch (Exception)
            {
            }
            return (text == null) ? string.Empty : text;
        }
        /// <summary>
        /// 取页面GET传值
        /// </summary>
        /// <param name="name">要取变量值</param>
        /// <returns></returns>
        public static string get(string name)
        {
            string text = "";
            try
            {
                text = HttpContext.Current.Request.QueryString[name];
            }
            catch (Exception)
            {
            }
            return (text == null) ? string.Empty : text;
        }
        /// <summary>
        /// 取页面GET传值
        /// </summary>
        /// <param name="name">要取变量值</param>
        /// <returns></returns>
        public static string getpost(string name)
        {
            string text = "";
            try
            {
                text = HttpContext.Current.Request[name];
            }
            catch (Exception)
            {
            }
            return (text == null) ? string.Empty : text;
        }
        /// <summary>
        /// 取所有提交
        /// </summary>
        /// <returns></returns>
        public static string FormQueryString()
        {
            string text = Fetch.QueryString();
            if (text.Length > 0)
            {
                text = text + "&" + Fetch.Form();
            }
            else
            {
                text = Fetch.Form();
            }
            return text.TrimStart(new char[]
			{
				'&'
			}).TrimEnd(new char[]
			{
				'&'
			});
        }
        /// <summary>
        /// 获得url提交的所有值：键名=值
        /// </summary>
        /// <returns></returns>
        public static string QueryString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                int count = HttpContext.Current.Request.QueryString.Count;
                for (int i = 0; i < count; i++)
                {
                    stringBuilder.Append("&" + HttpContext.Current.Request.QueryString.AllKeys[i] + "=" + HttpUtility.UrlEncodeUnicode(HttpUtility.UrlDecode(fun.Url(HttpContext.Current.Request.QueryString[i].ToString()))));
                }
            }
            catch (Exception)
            {
            }
            string result;
            if (stringBuilder.Length > 0)
            {
                result = stringBuilder.ToString().Substring(1);
            }
            else
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// 获得url提交的所有值：{键名}值
        /// </summary>
        /// <returns></returns>
        public static string GetQueryString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                int count = HttpContext.Current.Request.QueryString.Count;
                for (int i = 0; i < count; i++)
                {
                    stringBuilder.Append(string.Concat(new string[]
					{
						"{",
						HttpContext.Current.Request.QueryString.AllKeys[i],
						"}",
						HttpContext.Current.Request.QueryString[i].ToString(),
						"\r\n"
					}));
                }
            }
            catch (Exception)
            {
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 获得FROM所有的提交值
        /// </summary>
        /// <returns></returns>
        public static string Form()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                int count = HttpContext.Current.Request.Form.Count;
                for (int i = 0; i < count; i++)
                {
                    if (!HttpContext.Current.Request.Form.AllKeys[i].Equals("__VIEWSTATE", StringComparison.CurrentCultureIgnoreCase))
                    {
                        stringBuilder.Append("&" + HttpContext.Current.Request.Form.AllKeys[i] + "=" + HttpUtility.UrlEncodeUnicode(fun.Url(HttpContext.Current.Request.Form[i].ToString())));
                    }
                }
            }
            catch (Exception)
            {
            }
            string result;
            if (stringBuilder.Length > 0)
            {
                result = stringBuilder.ToString().Substring(1);
            }
            else
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// 获得FROM所有的提交值
        /// </summary>
        /// <returns></returns>
        public static string GetForm()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                int count = HttpContext.Current.Request.Form.Count;
                for (int i = 0; i < count; i++)
                {
                    if (!HttpContext.Current.Request.Form.AllKeys[i].Equals("__VIEWSTATE", StringComparison.CurrentCultureIgnoreCase))
                    {
                        stringBuilder.Append(string.Concat(new string[]
						{
							"{",
							HttpContext.Current.Request.Form.AllKeys[i],
							"}",
							HttpContext.Current.Request.Form[i].ToString(),
							"\r\n"
						}));
                    }
                }
            }
            catch (Exception)
            {
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        ///  IP 地址字符串形式转换成长整型
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static long Ip2Int(string ip)
        {
            long result;
            if (!Regxp.IsIp(ip))
            {
                result = -1L;
            }
            else
            {
                string[] array = ip.Split(new char[]
				{
					'.'
				});
                long num = long.Parse(array[0]) * 16777216L;
                num += (long)(int.Parse(array[1]) * 65536);
                num += (long)(int.Parse(array[2]) * 256);
                num += (long)int.Parse(array[3]);
                result = num;
            }
            return result;
        }
        /// <summary>
        /// 获得当前网址(HttpUtility.HtmlEncode)
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            string text;
            if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "off")
            {
                text = "http://";
            }
            else
            {
                text = "https://";
            }
            text += HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            if (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] != "80")
            {
                text = text + ":" + HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            }
            return HttpUtility.HtmlEncode(text);
        }
        /// <summary>
        /// 获得当前域名(只能取二级域名。不能取三级域名)
        /// </summary>
        public static string ServerDomain()
        {
            string text = "";
            try
            {
                text = HttpContext.Current.Request.Url.Host.ToLower();
            }
            catch (Exception)
            {
            }
            text = Fetch.ServerDomain(text);
            return text;
        }
        /// <summary>
        /// 获得当前域名(只能取二级域名。不能取三级域名)
        /// </summary>
        public static string ServerDomain(string host)
        {
            string result = "";
            try
            {
                string[] array = host.Split(new char[]
				{
					'.'
				});
                if (array.Length < 3 || Regxp.IsIp(host))
                {
                    result = host;
                }
                else
                {
                    string text = host.Remove(0, host.IndexOf(".") + 1);
                    if (text.StartsWith("com.") || text.StartsWith("net.") || text.StartsWith("org.") || text.StartsWith("gov."))
                    {
                        result = host;
                    }
                    else
                    {
                        result = text;
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 根据URL地址获取网址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDoMain(string value)
        {
            string result;
            if (value == null || value == "")
            {
                result = "";
            }
            else
            {
                string[] array = value.Split(new char[]
				{
					'/'
				});
                if (array.Length < 4)
                {
                    result = "";
                }
                else
                {
                    result = array[0] + "//" + array[2] + "/";
                }
            }
            return result;
        }
        /// <summary>
        /// 根据URL地址获取 域名
        /// </summary>
        /// <returns></returns>
        public static string GetDoMain()
        {
            string text = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.RawUrl, " ") + "/";
            string result = "";
            if (text == null || text == "")
            {
                result = "";
            }
            else
            {
                string[] array = text.Split(new char[]
				{
					'/'
				});
                if (array.Length < 4)
                {
                    result = "";
                }
                else
                {
                    string[] array2 = array[2].Split(new char[]
					{
						':'
					});
                    if (Regxp.IsIp(array2[0].Trim()))
                    {
                        result = array[2].Trim() + "/";
                    }
                    else
                    {
                        array2 = array[2].Split(new char[]
						{
							'.'
						});
                        if (array2.Length >= 2)
                        {
                            if (array[2].Contains("com.") || array[2].Contains("net.") || array[2].Contains("org.") || array[2].Contains("gov."))
                            {
                                result = string.Concat(new string[]
								{
									array2[array2.Length - 3].Trim(),
									".",
									array2[array2.Length - 2].Trim(),
									".",
									array2[array2.Length - 1].Trim(),
									"/"
								});
                            }
                            else
                            {
                                result = array2[array2.Length - 2].Trim() + "." + array2[array2.Length - 1].Trim() + "/";
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 读取一个字符串中指定的长度
        /// </summary>
        /// <param name="f_Str">要读指定长度的字符串</param>
        /// <param name="f_Length">多长。汉字算2个时，长度*2</param>
        /// <param name="f_Flag">1的时候是汉字算一个字符，2的时候汉字算2个。(2011-12-21修复加号和&amp;分号问题）</param>
        /// <returns></returns>
        public static string Intercept_Char(string f_Str, int f_Length, int f_Flag)
        {
            string result = "";
            if (f_Length != 0 && f_Str.Length > 0)
            {
                f_Str = f_Str.Replace("+", "%2b");
                f_Str = HttpUtility.UrlDecode(f_Str);
                int length = f_Str.Length;
                StringBuilder stringBuilder = new StringBuilder();
                string text = "";
                int num = 0;
                int num2 = Fetch.FontSize(f_Str);
                if (num2 < f_Length)
                {
                    result = f_Str;
                }
                else
                {
                    num2 = 0;
                    for (int i = 0; i < length; i++)
                    {
                        string text2 = f_Str.Substring(i, 1);
                        if (text2.Equals("&") || text2.Equals(";"))
                        {
                            string text3;
                            if (i + 6 < length)
                            {
                                text3 = f_Str.Substring(i, 6);
                            }
                            else
                            {
                                text3 = f_Str.Substring(i, length - i);
                            }
                            if (text2.Equals("&") && num == 0 && text3.Contains(";") && HttpUtility.HtmlDecode(text3.Substring(0, text3.IndexOf(";") + 1)).Length == 1)
                            {
                                num = i + 1;
                            }
                            if (text2.Equals(";") && num > 0 && i - num <= 6)
                            {
                                text = f_Str.Substring(num - 1, i - num + 2);
                            }
                        }
                        if (num == 0)
                        {
                            int num3;
                            if (f_Flag == 1)
                            {
                                num3 = 1;
                            }
                            else
                            {
                                num3 = Fetch.FontSize(text2);
                            }
                            if (num2 + num3 <= f_Length)
                            {
                                stringBuilder.Append(text2);
                            }
                            num2 += num3;
                        }
                        else
                        {
                            if (text.Length > 0)
                            {
                                stringBuilder.Append(text);
                                num2++;
                                num = 0;
                                text = "";
                            }
                        }
                        if (num2 >= f_Length)
                        {
                            result = stringBuilder.ToString();
                            break;
                        }
                    }
                    if (num2 < f_Length)
                    {
                        result = f_Str;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 判断汉字长度
        /// </summary>
        /// <returns></returns>
        public static int FontSize(string f_Str)
        {
            return Encoding.GetEncoding("GB2312").GetBytes(f_Str).Length;
        }
        /// <summary>
        /// 判断是否汉字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsChinese(char c)
        {
            return c > 'ё';
        }
        /// <summary>
        /// 是否是同一域名下提交
        /// </summary>
        /// <returns></returns>
        public static bool SiteTips()
        {
            return fun.UrlEqual(HttpUtility.HtmlDecode(Fetch.Referrer), HttpUtility.HtmlDecode(Fetch.CurrentUrl_Http));
        }
    }
}
