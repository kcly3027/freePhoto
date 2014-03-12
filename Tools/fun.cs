using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
namespace freePhoto.Tools
{
    /// <summary>
    /// 基本函数
    /// </summary>
    public class fun
    {
        /// <summary>
        /// 重复生成某个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string String(string str, int n)
        {
            char[] array = str.ToCharArray();
            char[] array2 = new char[array.Length * n];
            for (int i = 0; i < n; i++)
            {
                Buffer.BlockCopy(array, 0, array2, i * array.Length * 2, array.Length * 2);
            }
            return new string(array2);
        }
        /// <summary>
        /// 判断是不是日期开型
        /// </summary>
        /// <param name="strDate">一个字符串型的日期</param>
        /// <returns>返回真假</returns>
        public static bool IsDate(string strDate)
        {
            bool result = true;
            if (fun.isempty(strDate))
            {
                result = false;
            }
            else
            {
                try
                {
                    DateTime dateTime = DateTime.Parse(strDate);
                }
                catch (FormatException)
                {
                    result = false;
                }
            }
            return result;
        }
        /// <summary>
        /// 判断一个字符串是不是为空null
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回真假</returns>
        public static bool isempty(string str)
        {
            return str == null || str.Length <= 0;
        }
        /// <summary>
        /// 取web.config里的KEY值
        /// </summary>
        /// <param name="key">key值</param>
        /// <returns>返回key对应的value值</returns>
        public static string getapp(string key)
        {
            string result;
            try
            {
                object obj = ConfigurationManager.AppSettings[key];
                if (obj == null)
                {
                    result = "";
                }
                else
                {
                    result = obj.ToString();
                }
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// 取WEB.CONFIG里 连接数据库 ConfigurationManager的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionstrings(string key)
        {
            string result = "";
            try
            {
                result = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 取WEB.CONFIG里 连接数据库 ProviderName的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetProviderName(string key)
        {
            string result = "";
            try
            {
                result = ConfigurationManager.ConnectionStrings[key].ProviderName;
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 判断字符串是否由数字组成 包括零
        /// </summary>
        /// <param name="value">要判断的字符串。</param>
        /// <returns>判断成功返回 true ， 否则返回 false 。</returns>
        public static bool IsNumeric(string value)
        {
            bool result = false;
            value = ((value == null) ? "" : value);
            if (value.Length > 0)
            {
                result = Regex.IsMatch(value, "^[+-]?\\d*[.]?\\d*$");
            }
            return result;
        }
        /// <summary>
        /// 判断是否整数，没有小数点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            bool result = false;
            value = ((value == null) ? "" : value);
            if (value.Length > 0)
            {
                result = Regex.IsMatch(value, "^[+-]?\\d*$");
            }
            return result;
        }
        /// <summary>
        /// 判断是否为正整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUnsign(string value)
        {
            bool result = false;
            value = ((value == null) ? "" : value);
            if (value.Length > 0)
            {
                result = Regex.IsMatch(value, "^\\d*[.]?\\d*$");
            }
            return result;
        }
        /// <summary>
        /// 判断是不是数字，不包括零
        /// </summary>
        /// <param name="value">一个字符串</param>
        /// <returns>返回真假</returns>
        public static bool pnumeric(string value)
        {
            bool flag = fun.IsNumeric(value);
            if (flag)
            {
                if (value.Equals("0") || value.Contains(".") || value.Contains("+") || value.Contains("-"))
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 判断是不是数字
        /// </summary>
        /// <param name="value">一个字符串</param>
        /// <returns>不是数字返回零，是数字返回数字</returns>
        public static string IsZero(string value)
        {
            string result;
            if (!fun.IsNumeric(value))
            {
                result = "0";
            }
            else
            {
                result = value;
            }
            return result;
        }
        /// <summary>
        /// 返回IIS版本
        /// </summary>
        /// <returns></returns>
        public static string IISVersion()
        {
            return HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"].ToString();
        }
        /// <summary>
        /// 判断是不是外部提交
        /// </summary>
        /// <returns>返回真假，不是外部提交返回真，是外部提交返回假</returns>
        public static bool IsSelfRefer()
        {
            bool result = false;
            string text = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            string text2 = Fetch.GetDoMain().Split(new char[]
			{
				':'
			})[0].Replace("/", "");
            if (text == null)
            {
                text = "*******************************";
            }
            try
            {
                if (!fun.IsIP(text2))
                {
                    if (string.IsNullOrEmpty(text2) || text.ToLower().Contains("." + text2.ToLower()))
                    {
                        result = true;
                    }
                }
                else
                {
                    if (text.Contains(text2))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 判断是不是from post提交
        /// </summary>
        /// <returns>返回真假</returns>
        public static bool ispost()
        {
            string a = HttpContext.Current.Request.ServerVariables["Request_method"].ToLower();
            return a == "post";
        }
        /// <summary>
        /// 数字转成chr值 比如chr(34)双引号
        /// </summary>
        /// <param name="asciiCode">数字</param>
        /// <returns>返回字符串</returns>
        public static string Chr(int asciiCode)
        {
            string result;
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
                byte[] bytes = new byte[]
				{
					(byte)asciiCode
				};
                string @string = aSCIIEncoding.GetString(bytes);
                result = @string;
            }
            else
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// 只能获得ASCIIasc码
        /// </summary>
        /// <param name="character">单个字符</param>
        /// <returns>返回数字asc码</returns>
        public static int Asc(string character)
        {
            int result;
            if (character.Length == 1)
            {
                ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
                int num = (int)aSCIIEncoding.GetBytes(character)[0];
                result = num;
            }
            else
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 获得asc码
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int ASC(string character)
        {
            return (int)char.Parse(character);
        }
        /// <summary>
        /// 日期返回数字星期
        /// </summary>
        /// <param name="dt1">日期</param>
        /// <returns></returns>
        public static int Week2Int(DateTime dt1)
        {
            string text = dt1.DayOfWeek.ToString().ToLower();
            int result = 1;
            string text2 = text;
            switch (text2)
            {
                case "monday":
                    result = 1;
                    break;
                case "tuesday":
                    result = 2;
                    break;
                case "wednesday":
                    result = 3;
                    break;
                case "thursday":
                    result = 4;
                    break;
                case "friday":
                    result = 5;
                    break;
                case "saturday":
                    result = 6;
                    break;
                case "sunday":
                    result = 7;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 传入一个字符串，根据正则作替换
        /// </summary>
        /// <param name="fstring">字符串</param>
        /// <param name="patrn">正则</param>
        /// <param name="replstr">要替换成</param>
        /// <returns></returns>
        public static string ReplaceText(string fstring, string patrn, string replstr)
        {
            Regex regex = new Regex(patrn ?? "", RegexOptions.IgnoreCase);
            return regex.Replace(fstring, replstr);
        }
        /// <summary>
        /// 返回真实的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetRealIP()
        {
            string result = "";
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    result = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(new char[]
					{
						','
					})[0].Trim();
                }
                else
                {
                    result = request.UserHostAddress;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 获得域名对应的IP地址
        /// </summary>
        /// <param name="DoMainUrl"></param>
        /// <returns></returns>
        public static string DoMainIp(string DoMainUrl)
        {
            string result = "";
            try
            {
                IPHostEntry iPHostEntry = new IPHostEntry();
                iPHostEntry = Dns.GetHostEntry(DoMainUrl);
                if (iPHostEntry.AddressList.Length > 0)
                {
                    result = iPHostEntry.AddressList[0].ToString();
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 获得本机IP
        /// </summary>
        /// <returns></returns>
        public static string GetHostIp()
        {
            string result = HttpContext.Current.Request.ServerVariables.Get("Local_Addr").ToString();
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            if (hostEntry.AddressList.Length > 1)
            {
                ArrayList arrayList = new ArrayList();
                IPAddress[] addressList = hostEntry.AddressList;
                for (int i = 0; i < addressList.Length; i++)
                {
                    IPAddress iPAddress = addressList[i];
                    if (fun.IsIP(iPAddress.ToString()))
                    {
                        arrayList.Add(iPAddress.ToString());
                    }
                }
                string[] array = new string[]
				{
					"0",
					"0",
					"0",
					"0"
				};
                foreach (string text in arrayList)
                {
                    string[] array2 = text.Split(new char[]
					{
						'.'
					});
                    int num = 0;
                    while (num < array2.Length && num < array.Length)
                    {
                        array[num] = ((int.Parse(array2[num]) + int.Parse(array[num])) % 255).ToString();
                        num++;
                    }
                }
                result = string.Join(".", array);
            }
            return result;
        }
        /// <summary>
        /// 获得本机IP加密后
        /// </summary>
        /// <returns></returns>
        public static string GetHostKey()
        {
            string text = fun.GetHostIp();
            string text2 = Fetch.GetDoMain().Split(new char[]
			{
				':'
			})[0].Trim();
            text2 = text2.TrimEnd(new char[]
			{
				'/'
			});
            string[] array = text.Split(new char[]
			{
				'.'
			});
            double num = 0.0;
            if (array.Length == 4)
            {
                num = double.Parse(array[0]) * double.Parse(array[3]);
                num *= double.Parse(array[1]) + double.Parse(array[2]);
            }
            text = Md5.MD5(num.ToString() + (text2.Equals(text) ? "" : text2));
            text = text.Substring(1, 30);
            string text3 = "";
            for (int i = 0; i < 5; i++)
            {
                text3 = text3 + text.Substring(i * 5, 5) + "-";
            }
            return text3.Substring(0, text3.Length - 1).ToUpper();
        }
        /// <summary>
        /// 获得本机IP加密后
        /// </summary>
        /// <returns></returns>
        public static string NewGetHostKey()
        {
            string str = "BjSyCms";
            string str2 = "YQRJ20101001yqrj";
            string text = Fetch.GetDoMain().Split(new char[]
			{
				':'
			})[0].Trim();
            text = text.TrimEnd(new char[]
			{
				'/'
			});
            string text2 = Md5.MD5(str + text + str2);
            string text3 = "";
            for (int i = 0; i < 5; i++)
            {
                text3 = text3 + text2.Substring(i * 5, 5) + "-";
            }
            return text3.Substring(0, text3.Length - 1).ToUpper();
        }
        /// <summary>
        /// 对IP加上到期时间加密
        /// </summary>
        /// <returns></returns>
        public static string GetHostKey(string DataTime)
        {
            string a_strKey = fun.GetHostIp().Replace(".", "");
            return Encrypt.Encrypt3DES(DataTime, a_strKey);
        }
        /// <summary>
        /// 格式化大小
        /// </summary>
        /// <param name="str">数字从字节开始</param>
        /// <returns></returns>
        public static string ByteSize(string str)
        {
            string result = "";
            try
            {
                double num = double.Parse(str);
                result = Math.Round(num, 2).ToString() + "&nbsp;Byte";
                if (num >= 1024.0)
                {
                    num /= 1024.0;
                    result = Math.Round(num, 2).ToString() + "&nbsp;KB";
                }
                if (num >= 1024.0)
                {
                    num /= 1024.0;
                    result = Math.Round(num, 2).ToString() + "&nbsp;MB";
                }
                if (num >= 1024.0)
                {
                    num /= 1024.0;
                    result = Math.Round(num, 2).ToString() + "&nbsp;GB";
                }
                if (num >= 1024.0)
                {
                    num /= 1024.0;
                    result = Math.Round(num, 2).ToString() + "&nbsp;TB";
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 格式化大小
        /// </summary>
        /// <param name="str">数字从KB开始</param>
        /// <returns></returns>
        public static string ByteSizeKB(string str)
        {
            string result = "";
            try
            {
                double num = double.Parse(str);
                result = Math.Round(num, 2).ToString() + "&nbsp;KB";
                if (num >= 1024.0)
                {
                    num /= 1024.0;
                    result = Math.Round(num, 2).ToString() + "&nbsp;MB";
                }
                if (num >= 1024.0)
                {
                    num /= 1024.0;
                    result = Math.Round(num, 2).ToString() + "&nbsp;GB";
                }
                if (num >= 1024.0)
                {
                    num /= 1024.0;
                    result = Math.Round(num, 2).ToString() + "&nbsp;TB";
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 去除 htmlCode 中所有的HTML标签(包括标签中的属性)
        /// </summary>
        /// <param name="htmlCode">包含 HTML 代码的字符串。</param>
        /// <returns>返回一个不包含 HTML 代码的字符串</returns>
        public static string StripHtml(string htmlCode)
        {
            string result;
            if (htmlCode == null || 0 == htmlCode.Length)
            {
                result = string.Empty;
            }
            else
            {
                htmlCode = htmlCode.Replace("-->", "▓");
                htmlCode = Regex.Replace(htmlCode, "<!--(.[^▓]*)▓", string.Empty, RegexOptions.IgnoreCase);
                htmlCode = htmlCode.Replace("▓", "-->");
                result = Regex.Replace(htmlCode, "<(.[^>]*)>", string.Empty, RegexOptions.IgnoreCase);
            }
            return result;
        }
        /// <summary>
        /// 返回汉字星期几
        /// </summary>
        /// <param name="da1">日期</param>
        /// <returns></returns>
        public static string WeekValue(DateTime da1)
        {
            int num = fun.Week2Int(da1);
            string result = "";
            switch (num)
            {
                case 1:
                    result = "星期一";
                    break;
                case 2:
                    result = "星期二";
                    break;
                case 3:
                    result = "星期三";
                    break;
                case 4:
                    result = "星期四";
                    break;
                case 5:
                    result = "星期五";
                    break;
                case 6:
                    result = "星期六";
                    break;
                case 7:
                    result = "星期日";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 返回月份英文
        /// </summary>
        /// <param name="Month">月份（1-12）</param>
        /// <param name="Type">0为长的英文其它为短的</param>
        /// <returns></returns>
        public static string MonthEn(string Month, string Type)
        {
            string result = "";
            if (Type.Equals("0"))
            {
                switch (Month)
                {
                    case "1":
                        result = "January";
                        break;
                    case "2":
                        result = "February";
                        break;
                    case "3":
                        result = "Marcy";
                        break;
                    case "4":
                        result = "April";
                        break;
                    case "5":
                        result = "May";
                        break;
                    case "6":
                        result = "June";
                        break;
                    case "7":
                        result = "July";
                        break;
                    case "8":
                        result = "August";
                        break;
                    case "9":
                        result = "September";
                        break;
                    case "10":
                        result = "October";
                        break;
                    case "11":
                        result = "November";
                        break;
                    case "12":
                        result = "December";
                        break;
                }
            }
            else
            {
                switch (Month)
                {
                    case "1":
                        result = "Jan";
                        break;
                    case "2":
                        result = "Feb";
                        break;
                    case "3":
                        result = "Mar";
                        break;
                    case "4":
                        result = "Apr";
                        break;
                    case "5":
                        result = "May";
                        break;
                    case "6":
                        result = "June";
                        break;
                    case "7":
                        result = "July";
                        break;
                    case "8":
                        result = "Aug";
                        break;
                    case "9":
                        result = "Sept";
                        break;
                    case "10":
                        result = "Oct";
                        break;
                    case "11":
                        result = "Nov";
                        break;
                    case "12":
                        result = "Dec";
                        break;
                }
            }
            return result;
        }
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="f_getDate">日期</param>
        /// <param name="f_datestyle">格式化日期YY02(O4)  MM  MEJ  MEA  DD  HH  MI  SS  WE</param>
        /// <returns></returns>
        public static string Get_Date(DateTime f_getDate, string f_datestyle)
        {
            string text = f_datestyle;
            if (f_datestyle.Contains("YY02"))
            {
                text = text.Replace("YY02", f_getDate.Year.ToString().Substring(2, 2));
            }
            if (f_datestyle.Contains("YY04"))
            {
                text = text.Replace("YY04", f_getDate.Year.ToString());
            }
            if (f_datestyle.Contains("MM"))
            {
                if (int.Parse(f_getDate.Month.ToString()) < 10)
                {
                    text = text.Replace("MM", "0" + f_getDate.Month.ToString());
                }
                else
                {
                    text = text.Replace("MM", f_getDate.Month.ToString());
                }
            }
            if (f_datestyle.Contains("MEJ"))
            {
                text = text.Replace("MEJ", fun.MonthEn(f_getDate.Month.ToString(), "1"));
            }
            if (f_datestyle.Contains("MEA"))
            {
                text = text.Replace("MEA", fun.MonthEn(f_getDate.Month.ToString(), "0"));
            }
            if (f_datestyle.Contains("DD"))
            {
                if (int.Parse(f_getDate.Day.ToString()) < 10)
                {
                    text = text.Replace("DD", "0" + f_getDate.Day.ToString());
                }
                else
                {
                    text = text.Replace("DD", f_getDate.Day.ToString());
                }
            }
            if (f_datestyle.Contains("HH"))
            {
                if (int.Parse(f_getDate.Hour.ToString()) < 10)
                {
                    text = text.Replace("HH", "0" + f_getDate.Hour.ToString());
                }
                else
                {
                    text = text.Replace("HH", f_getDate.Hour.ToString());
                }
            }
            if (f_datestyle.Contains("MI"))
            {
                if (int.Parse(f_getDate.Minute.ToString()) < 10)
                {
                    text = text.Replace("MI", "0" + f_getDate.Minute.ToString());
                }
                else
                {
                    text = text.Replace("MI", f_getDate.Minute.ToString());
                }
            }
            if (f_datestyle.Contains("SS"))
            {
                if (int.Parse(f_getDate.Second.ToString()) < 10)
                {
                    text = text.Replace("SS", "0" + f_getDate.Second.ToString());
                }
                else
                {
                    text = text.Replace("SS", f_getDate.Second.ToString());
                }
            }
            if (f_datestyle.Contains("WE"))
            {
                text = text.Replace("WE", fun.WeekValue(f_getDate));
            }
            return text;
        }
        /// <summary>
        /// 判断是否为IP
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }
        /// <summary>
        /// 判断是不是英文，数字，并且依英文开头
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <param name="StartLen">最小长度</param>
        /// <param name="EndLen">最大长度</param>
        /// <returns></returns>
        public static bool AscAZ19(string str, int StartLen, int EndLen)
        {
            bool result = true;
            if (!fun.isempty(str))
            {
                int length = str.Length;
                if (length < StartLen || length > EndLen)
                {
                    result = false;
                }
                else
                {
                    int num = fun.ASC(str.Substring(0, 1));
                    if (num < 65 || (num > 90 && num < 97) || num > 122)
                    {
                        result = false;
                    }
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            num = fun.ASC(str.Substring(i, 1));
                            if (num < 48 || (num > 57 && num < 65) || (num != 95 && num > 90 && num < 97) || num > 122)
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 判断是否为汉字。英文，数字，字符长度。
        /// </summary>
        /// <param name="str">判断的字符串</param>
        /// <param name="StartLen">最小长度</param>
        /// <param name="EndLen">最大长度</param>
        /// <returns></returns>
        public static bool AscAZ19HZ(string str, int StartLen, int EndLen)
        {
            bool result = true;
            if (!fun.isempty(str))
            {
                int length = str.Length;
                if (length < StartLen || length > EndLen)
                {
                    result = false;
                }
                else
                {
                    if (Fetch.Intercept_Char(str, EndLen, 2) != str)
                    {
                        result = false;
                    }
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            int num = fun.ASC(str.Substring(i, 1));
                            if ((num > 0 && num < 48) || (num > 57 && num < 65) || (num > 90 && num < 97) || (num > 122 && Math.Abs(num) < 255))
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 根据生日返回星座
        /// </summary>
        /// <param name="birthday">传入生日值。字符型</param>
        /// <returns></returns>
        public static string Horoscope(string birthday)
        {
            string result = "";
            if (fun.IsDate(birthday))
            {
                DateTime dateTime = default(DateTime);
                dateTime = DateTime.Parse(birthday);
                string text = dateTime.Month.ToString();
                string text2 = dateTime.Day.ToString();
                if (text.Length < 2)
                {
                    text = "0" + text;
                }
                if (text2.Length < 2)
                {
                    text2 = "0" + text2;
                }
                int num = int.Parse(text + text2);
                if (num < 120)
                {
                    result = "魔羯座";
                }
                else
                {
                    if (num < 219)
                    {
                        result = "水瓶座";
                    }
                    else
                    {
                        if (num < 321)
                        {
                            result = "双鱼座";
                        }
                        else
                        {
                            if (num < 420)
                            {
                                result = "白羊座";
                            }
                            else
                            {
                                if (num < 521)
                                {
                                    result = "金牛座";
                                }
                                else
                                {
                                    if (num < 622)
                                    {
                                        result = "双子座";
                                    }
                                    else
                                    {
                                        if (num < 723)
                                        {
                                            result = "巨蟹座";
                                        }
                                        else
                                        {
                                            if (num < 823)
                                            {
                                                result = "狮子座";
                                            }
                                            else
                                            {
                                                if (num < 923)
                                                {
                                                    result = "处女座";
                                                }
                                                else
                                                {
                                                    if (num < 1024)
                                                    {
                                                        result = "天秤座";
                                                    }
                                                    else
                                                    {
                                                        if (num < 1122)
                                                        {
                                                            result = "天蝎座";
                                                        }
                                                        else
                                                        {
                                                            if (num < 1222)
                                                            {
                                                                result = "射手座";
                                                            }
                                                            else
                                                            {
                                                                if (num > 1221)
                                                                {
                                                                    result = "魔羯座";
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// html代码
        /// </summary>
        /// <param name="str">字段串</param>
        /// <returns></returns>
        public static string NoCon(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("'", "&#39;");
                str = str.Replace("•", "&bull;");
                str = str.Replace("·", "&bull;");
            }
            return str;
        }
        /// <summary>
        /// html代码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string FNoCon(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("&#39;", "'");
                str = str.Replace("&bull;", "•");
                str = str.Replace("&bull;", "·");
            }
            return str;
        }
        /// <summary>
        /// html代码（转单引号、空格和回车）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string NoConBr(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("'", "&#39;");
                str = str.Replace("•", "&bull;");
                str = str.Replace("·", "&bull;");
                MatchEvaluator evaluator = new MatchEvaluator(fun.ConvertToXML);
                str = Regex.Replace(str, " {2,}", evaluator);
                str = str.Replace(fun.Chr(9), "&nbsp;");
                str = str.Replace(fun.Chr(13) + fun.Chr(10), "<br />");
                str = str.Replace("\r\n", "<br />");
            }
            return str;
        }
        /// <summary>
        /// html代码（转单引号、空格和回车）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string FNoConBr(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("&#39;", "'");
                str = str.Replace("&bull;", "•");
                str = str.Replace("&bull;", "·");
                str = str.Replace("&nbsp;", "");
                str = str.Replace("<br />", "\r\n");
                str = str.Replace("<br/>", "\r\n");
                str = str.Replace("<br>", "\r\n");
            }
            return str;
        }
        /// <summary>
        /// 为替换空格使用
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string ConvertToXML(Match m)
        {
            int length = m.Value.Length;
            string str = " ";
            return str + fun.RepeatString("&nbsp;", length - 1);
        }
        /// <summary>
        /// 原ASP的过滤函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NoSql(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("&", "&amp;");
                str = str.Replace("·", "&bull;");
                str = str.Replace("'", "&#39;");
                str = str.Replace("|", "&#124;");
                str = str.Replace("•", "&bull;");
                str = str.Replace("\"", "&quot;");
                MatchEvaluator evaluator = new MatchEvaluator(fun.ConvertToXML);
                str = Regex.Replace(str, " {2,}", evaluator);
                str = str.Replace(fun.Chr(9), "&nbsp;");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace("(", "&#40;");
                str = str.Replace(")", "&#41;");
                str = str.Replace("--", "&#45;&#45;");
                str = str.Replace(fun.Chr(13) + fun.Chr(10), "<br />");
                str = str.Replace("\r\n", "<br />");
            }
            return str;
        }
        /// <summary>
        /// 重复一个字符串N倍
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="n">重复次数</param>
        /// <returns></returns>
        public static string RepeatString(string str, int n)
        {
            char[] array = str.ToCharArray();
            char[] array2 = new char[array.Length * n];
            for (int i = 0; i < n; i++)
            {
                Buffer.BlockCopy(array, 0, array2, i * array.Length * 2, array.Length * 2);
            }
            return new string(array2);
        }
        /// <summary>
        /// 显示内容。格式化（&lt;&gt;）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ViewContent(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
            }
            return str;
        }
        /// <summary>
        /// 反原ASP的过滤函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FNoSql(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("&amp;", "&");
                str = str.Replace("&bull;", "•");
                str = str.Replace("&bull;", "·");
                str = str.Replace("&#59;", ";");
                str = str.Replace("&#39;", "'");
                str = str.Replace("&#124;", "|");
                str = str.Replace("&quot;", "\"");
                str = str.Replace("&nbsp;", " ");
                str = str.Replace("&lt;", "<");
                str = str.Replace("&gt;", ">");
                str = str.Replace("&#40;", "(");
                str = str.Replace("&#41;", ")");
                str = str.Replace("&#45;&#45;", "--");
                str = str.Replace("<br />", "\r\n");
                str = str.Replace("<br/>", "\r\n");
                str = str.Replace("<br>", "\r\n");
            }
            return str;
        }
        /// <summary>
        /// 反原ASP的过滤函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FNoSqlNobr(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("&amp;", "&");
                str = str.Replace("&bull;", "•");
                str = str.Replace("&bull;", "·");
                str = str.Replace("&#59;", ";");
                str = str.Replace("&#39;", "'");
                str = str.Replace("&quot;", "\"");
                str = str.Replace("&lt;", "<");
                str = str.Replace("&gt;", ">");
                str = str.Replace("&#40;", "(");
                str = str.Replace("&#41;", ")");
                str = str.Replace("&#45;&#45;", "--");
            }
            return str;
        }
        /// <summary>
        /// 转换内容的一些字段
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ContentHtmlCode(string str)
        {
            if (!fun.isempty(str))
            {
                str = str.Replace("&amp;", "&");
                str = str.Replace("&bull;", "•");
                str = str.Replace("&bull;", "·");
                str = str.Replace("&#59;", ";");
                str = str.Replace("&#39;", "'");
                str = str.Replace("&quot;", "\"");
                str = str.Replace("&#40;", "(");
                str = str.Replace("&#41;", ")");
                str = str.Replace("&#45;&#45;", "--");
            }
            return str;
        }
        /// <summary>
        /// 获得不重复的随机数
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="Type">1全数字2英文其它随便</param>
        /// <returns></returns>
        public static string GetGuidRandom(int len, string Type)
        {
            string text = ",";
            string text2 = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,-";
            string text3 = "0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6";
            char[] separator = text.ToCharArray();
            char[] separator2 = text.ToCharArray();
            string[] array = text2.Split(separator, text2.Length);
            string[] array2 = text3.Split(separator2, text3.Length);
            string text4 = Guid.NewGuid().ToString();
            if (Type.Equals("1"))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    text4 = text4.Replace(array[i], array2[i]);
                }
            }
            else
            {
                if (Type.Equals("2"))
                {
                    for (int i = 0; i < array2.Length; i++)
                    {
                        text4 = text4.Replace(array2[i], array[i]);
                    }
                }
            }
            return text4.Substring(0, len);
        }
        /// <summary>
        /// radio
        /// </summary>
        /// <param name="a">值</param>
        /// <param name="b">值</param>
        /// <returns></returns>
        public static string isChecked(string a, string b)
        {
            string result;
            if (("," + a + ",").Contains("," + b + ","))
            {
                result = "checked";
            }
            else
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// selected
        /// </summary>
        /// <param name="a">值</param>
        /// <param name="b">值</param>
        /// <returns></returns>
        public static string isSelected(string a, string b)
        {
            string result;
            if (a.Equals(b))
            {
                result = "selected";
            }
            else
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// 取URL传值（字符串格式？name=&amp;ddd这样的格式）
        /// </summary>
        /// <param name="paras">要取的值</param>
        /// <param name="url">URL字符串格式</param>
        /// <returns></returns>
        public static string GetRequest(string paras, string url)
        {
            string text = "";
            if (paras.Length > 0 && url.Length > 0)
            {
                Regex regex = new Regex("(^|&|\\?)" + paras + "=([^&]*)(&|$)", RegexOptions.IgnoreCase);
                MatchCollection matchCollection = regex.Matches(url);
                for (int i = 0; i < matchCollection.Count; i++)
                {
                    text = text + HttpUtility.UrlDecode(regex.Replace(matchCollection[i].Value, "$2")) + ",";
                }
                if (text.Length > 0)
                {
                    text = text.Substring(0, text.Length - 1);
                }
            }
            return text;
        }
        /// <summary>
        /// 不区分大小写替换字符串
        /// </summary>
        /// <param name="Source">原字符串</param>
        /// <param name="ReplaceStr">要替换的字符</param>
        /// <param name="ReplaceValue">替换后字符</param>
        /// <returns></returns>
        public static string Replace(string Source, string ReplaceStr, string ReplaceValue)
        {
            return Strings.Replace(Source, ReplaceStr, ReplaceValue, 1, -1, CompareMethod.Text);
        }
        /// <summary>
        /// 获得WEBCONFIG里的连接字段串
        /// </summary>
        /// <returns></returns>
        public static string WebConfigSqlLinkString()
        {
            return ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }
        /// <summary>
        /// 替换URL里的'"格式
        /// </summary>
        /// <param name="str">把地址'"转义</param>
        /// <returns></returns>
        public static string Url(string str)
        {
            return str.Replace("'", "%27").Replace("\"", "%22");
        }
        /// <summary>
        /// 判断数值是否大于38位
        /// </summary>
        /// <param name="NumStr">数值字符串</param>
        /// <returns></returns>
        public static string NumericValue(string NumStr)
        {
            if (!NumStr.Contains(","))
            {
                if (NumStr.Length > 38)
                {
                    NumStr = NumStr.Substring(0, 38);
                }
            }
            else
            {
                string[] array = NumStr.Split(new char[]
				{
					','
				});
                NumStr = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (!string.IsNullOrEmpty(array[i]))
                    {
                        if (array[i].Length > 38)
                        {
                            NumStr = NumStr + array[i].Substring(0, 38) + ",";
                        }
                        else
                        {
                            NumStr = NumStr + array[i] + ",";
                        }
                    }
                }
                NumStr = NumStr.TrimEnd(new char[]
				{
					','
				}).TrimStart(new char[]
				{
					','
				});
            }
            return NumStr;
        }
        /// <summary>
        /// 判断两个地址是否同一地址
        /// </summary>
        /// <param name="Url1">地址1</param>
        /// <param name="Url2">地址2</param>
        /// <returns></returns>
        public static bool UrlEqual(string Url1, string Url2)
        {
            string text = Url1;
            string text2 = Url2;
            int num = Url1.IndexOf("://");
            int num2 = Url2.IndexOf("://");
            if (num != -1)
            {
                text = Url1.Substring(num + 3);
            }
            if (num2 != -1)
            {
                text2 = Url2.Substring(num2 + 3);
            }
            num = text.IndexOf("/");
            num2 = text2.IndexOf("/");
            if (num != -1)
            {
                text = text.Substring(0, num);
            }
            if (num2 != -1)
            {
                text2 = text2.Substring(0, num2);
            }
            return text.ToLower().Equals(text2.ToLower());
        }
        /// <summary>
        /// 取传入的传出值。格式“p1s2d3” 取p得1
        /// </summary>
        /// <param name="Value">原值</param>
        /// <param name="Getv">要取的值</param>
        /// <returns></returns>
        public static string ActionGet(string Value, string Getv)
        {
            string input = fun.RegexGet(Value, Getv + "([0-9\\,\\ ]*)");
            return Regex.Replace(input, Getv, "", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 取传入的传出值。
        /// </summary>
        /// <param name="Value">原值</param>
        /// <param name="Getv">要取正则</param>
        /// <returns></returns>
        public static string RegexGet(string Value, string Getv)
        {
            string result = "";
            Regex regex = new Regex(Getv ?? "", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(Value);
            if (matchCollection.Count > 0)
            {
                result = matchCollection[0].Value;
            }
            return result;
        }
        /// <summary>
        /// xml里面的特殊字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string XmlStr(string str)
        {
            str = str.Replace("\"", "&quot;");
            str = str.Replace("'", "&apos;");
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            return str;
        }
        /// <summary>
        /// xml里面的特殊字符(反）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string FXmlStr(string str)
        {
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&apos;", "'");
            str = str.Replace("&amp;", "&");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            return str;
        }
        /// <summary>
        /// 英文字母串（保留英文的完整性）
        /// </summary>
        /// <param name="ViewStr">已经截取的要显示的字母串</param>
        /// <param name="YStr">原来的字符串</param>
        /// <returns></returns>
        public static string EnString(string ViewStr, string YStr)
        {
            string result;
            if (ViewStr.EndsWith(" "))
            {
                result = ViewStr;
            }
            else
            {
                if (YStr.Substring(ViewStr.Length, 1).Equals(" "))
                {
                    result = ViewStr;
                }
                else
                {
                    int num = ViewStr.LastIndexOf(" ");
                    if (num > 0)
                    {
                        result = ViewStr.Substring(0, num);
                    }
                    else
                    {
                        result = ViewStr;
                    }
                }
            }
            return result;
        }
    }
}
