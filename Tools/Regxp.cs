using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
namespace freePhoto.Tools
{
    /// <summary>
    /// 正则
    /// </summary>
    public class Regxp
    {
        /// <summary>
        ///  判断是否为Unicode码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUnicode(string s)
        {
            string pattern = "^[\\u4E00-\\u9FA5\\uE815-\\uFA29]+$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 判断字符串是否由数字组成
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(string s)
        {
            string pattern = "^\\-?[0-9]+$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 判断是否电话号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsTelephone(string s)
        {
            string pattern = "^(0[0-9]{2,3}\\-)?([2-9][0-9]{6,7})+(\\-[0-9]{1,4})?$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 正则判断返回真假
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="pattern">正则</param>
        /// <returns></returns>
        public static bool IsMatch(string s, string pattern)
        {
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 判断字符串是否由汉字组成
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isChinese(string s)
        {
            string pattern = "^[\\u4e00-\\u9fa5]{2,}$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 判断字符串是否是正确的 IP 地址格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsIp(string s)
        {
            string pattern = "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 判断字符串是否存在操作数据库的安全隐患
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsSafety(string s)
        {
            string text = s.Replace("%20", " ");
            text = Regex.Replace(text, "\\s", " ");
            string pattern = "select |insert |delete from |count\\(|drop table|update |truncate |asc\\(|mid\\(|char\\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|\"|\\'| or ";
            return !Regxp.IsMatch(text, pattern);
        }
        /// <summary>
        /// 判断字符串是否是正确的 Url 地址格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUrl(string s)
        {
            string pattern = "^(http|https|ftp|rtsp|mms):(\\/\\/|\\\\\\\\)[A-Za-z0-9%\\-_@]+\\.[A-Za-z0-9%\\-_@]+[A-Za-z0-9\\.\\/=\\?%\\-&_~`@:\\+!;]*$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 判断是否为相对地址（虚拟地址）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsRelativePath(string s)
        {
            return s != null && !(s == string.Empty) && !s.StartsWith("/") && !s.StartsWith("?") && !Regex.IsMatch(s, "^\\s*[a-zA-Z]{1,10}:.*$");
        }
        /// <summary>
        /// 判断是否为绝对地址（物理地址）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPhysicalPath(string s)
        {
            string pattern = "^\\s*[a-zA-Z]:.*$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 判断是否为正确的 email 地址格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmail(string s)
        {
            string pattern = "^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$";
            return Regxp.IsMatch(s, pattern);
        }
        /// <summary>
        /// 信箱是否合法
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return strIn != null && Regex.IsMatch(strIn, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }
        /// <summary>
        /// 正则替换所有的input控件的内容
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RegxpInput(string s)
        {
            Regex regex = new Regex("\\<input[^\\>]+\\>", RegexOptions.IgnoreCase);
            return regex.Replace(s, "");
        }
        /// <summary>
        /// 取Name:值;  Name后面的‘值’
        /// </summary>
        /// <param name="Str">要取的字符串</param>
        /// <param name="Rstring">要取的字段</param>
        /// <returns></returns>
        public static string RegExpValue(string Str, string Rstring)
        {
            return Regxp.RegExpValue(Str, Rstring, ":", ";");
        }
        /// <summary>
        /// 取Name:值;  Name后面的‘值’
        /// </summary>
        /// <param name="Str">要取的字符串</param>
        /// <param name="Rstring">要取的字段</param>
        /// <param name="StartStr">开始标识</param>
        /// <param name="EndStr">结束标识</param>
        /// <returns></returns>
        public static string RegExpValue(string Str, string Rstring, string StartStr, string EndStr)
        {
            string text = "";
            if (Str.Length > 0)
            {
                Regex regex = new Regex(Rstring + StartStr + "([\\S\\s]*?)" + EndStr, RegexOptions.IgnoreCase);
                MatchCollection matchCollection = regex.Matches(HttpUtility.UrlDecode(Str));
                if (matchCollection.Count > 0)
                {
                    text = matchCollection[0].Value;
                }
            }
            if (text.Length > 0)
            {
                text = text.Substring(Rstring.Length + StartStr.Length, text.Length - EndStr.Length - Rstring.Length - StartStr.Length);
            }
            return text;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="Str">要取的字符串</param>
        /// <param name="Rstring">要取的字段</param>
        /// <returns></returns>
        public static string RegExpListValue(string Str, string Rstring)
        {
            string text = "";
            if (Str.Length > 0)
            {
                Regex regex = new Regex(Rstring, RegexOptions.IgnoreCase);
                MatchCollection matchCollection = regex.Matches(HttpUtility.UrlDecode(Str));
                if (matchCollection.Count > 0)
                {
                    for (int i = 0; i < matchCollection.Count; i++)
                    {
                        text = text + matchCollection[i].Value + ((i + 1 != matchCollection.Count) ? "\r\n" : "");
                    }
                }
            }
            return text;
        }
        /// <summary>
        /// 清除WORD格式
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string CleanWordHtml(string html)
        {
            foreach (string pattern in new ArrayList
			{
				"<!--(\\w|\\W)+?-->",
				"<title>(\\w|\\W)+?</title>",
				"\\s?class=\\w+",
				"\\s+style='[^']+'",
				"<(meta|link|/?o:|/?style|/?font|/?strong|/?st\\d|/?head|/?html|body|/?body|/?span|!\\[)[^>]*?>",
				"(<[^>]+>)+ (</\\w+>)+",
				"\\s+v:\\w+=\"[^\"]+\"",
				"(\\n\\r){2,}"
			})
            {
                html = Regex.Replace(html, pattern, "", RegexOptions.IgnoreCase);
            }
            return html;
        }
    }
}
