using System;
using System.Web;
namespace freePhoto.Tools
{
    /// <summary>
    /// Cookies
    /// </summary>
    public class Cookies
    {
        /// <summary>
        /// 建立缓存cookies
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">保存值</param>
        /// <param name="expires">保存时间（分钟）0为关闭浏览器失效</param>
        /// <param name="CacheName">缓存名称</param>
        public static void ResponseCookies(string key, string value, int expires, string CacheName)
        {
            string text = Fetch.GetDoMain().Split(new char[]
			{
				':'
			})[0].Trim();
            text = text.TrimEnd(new char[]
			{
				'/'
			});
            if (Regxp.IsIp(text))
            {
                text = "";
            }
            string name = CacheName + "_mice";
            key = key.ToLower().Trim();
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[name];
            HttpCookie httpCookie2 = new HttpCookie(name);
            if (httpCookie != null)
            {
                httpCookie.Values.Remove(key);
                httpCookie.Values.Add(key, HttpUtility.UrlEncode(value) ?? "");
                httpCookie.Path = "/";
                if (expires > 0)
                {
                    httpCookie.Expires = DateTime.Now.AddMinutes((double)expires);
                }
                if (text.Length > 1)
                {
                    httpCookie.Domain = text;
                }
                HttpContext.Current.Response.Cookies.Remove(name);
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
            else
            {
                if (httpCookie2 != null)
                {
                    httpCookie2.Values.Add(key, HttpUtility.UrlEncode(value) ?? "");
                    httpCookie2.Path = "/";
                    httpCookie2.Expires = DateTime.Now.AddMinutes((double)expires);
                    if (text.Length > 1)
                    {
                        httpCookie2.Domain = text;
                    }
                    HttpContext.Current.Response.Cookies.Add(httpCookie2);
                }
            }
        }
        /// <summary>
        /// 读取指定的COOKIES值
        /// </summary>
        /// <param name="cookiename">关键字</param>
        /// <param name="CacheName">缓存名称</param>
        /// <returns></returns>
        public static string RequestCookies(string cookiename, string CacheName)
        {
            string text = CacheName + "_mice";
            cookiename = cookiename.ToLower().Trim();
            HttpCookie httpCookie = null;
            try
            {
                httpCookie = HttpContext.Current.Request.Cookies[text];
            }
            catch (Exception)
            {
            }
            text = "";
            if (httpCookie != null)
            {
                object obj = httpCookie.Values[cookiename];
                if (obj == null)
                {
                    text = "";
                }
                else
                {
                    text = obj.ToString();
                }
            }
            return HttpUtility.UrlDecode(text);
        }
        /// <summary>
        /// 清除缓存值
        /// </summary>
        /// <param name="CacheName">缓存名称</param>
        /// <param name="CacheUrl">绑定域名</param>
        public static void CleanCookies(string CacheName, string CacheUrl)
        {
            string name = CacheName + "_mice";
            HttpCookie httpCookie = new HttpCookie(name);
            httpCookie.Expires = DateTime.Now.AddMinutes(-1.0);
            if (CacheUrl.Length == 0)
            {
                CacheUrl = Fetch.GetDoMain().Split(new char[]
				{
					':'
				})[0].Trim();
                CacheUrl = CacheUrl.TrimEnd(new char[]
				{
					'/'
				});
                if (Regxp.IsIp(CacheUrl))
                {
                    CacheUrl = "";
                }
            }
            if (CacheUrl.Length > 1)
            {
                httpCookie.Domain = CacheUrl;
            }
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
    }
}
