using System;
using System.Web;
namespace freePhoto.Tools
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class Client
    {
        /// <summary>
        /// 获取浏览器的名字和主（整数）版本号
        /// </summary>
        /// <returns></returns>
        public string Browser_Type()
        {
            return HttpContext.Current.Request.Browser.Type;
        }
        /// <summary>
        /// 获取浏览器字符串（如果有）
        /// </summary>
        /// <returns></returns>
        public string Browser_Browser()
        {
            return HttpContext.Current.Request.Browser.Browser;
        }
        /// <summary>
        /// 获取浏览器的完整版本号
        /// </summary>
        /// <returns></returns>
        public string Browser_Version()
        {
            return HttpContext.Current.Request.Browser.Version;
        }
        /// <summary>
        /// 获取浏览器的主（整数）版本号
        /// </summary>
        /// <returns></returns>
        public string Browser_MajorVersion()
        {
            return HttpContext.Current.Request.Browser.MajorVersion.ToString();
        }
        /// <summary>
        /// 获取浏览器的次（小数）版本号
        /// </summary>
        /// <returns></returns>
        public string Browser_MinorVersion()
        {
            return HttpContext.Current.Request.Browser.MinorVersion.ToString();
        }
        /// <summary>
        /// 获取客户端使用的平台名称（如果已知）
        /// </summary>
        /// <returns></returns>
        public string Platform()
        {
            return HttpContext.Current.Request.Browser.Platform;
        }
        /// <summary>
        /// 获取客户端使用的平台32/64（win系统）
        /// </summary>
        /// <returns></returns>
        public string PlatformType()
        {
            string result;
            if (HttpContext.Current.Request.Browser.Win16)
            {
                result = "(16）";
            }
            else
            {
                if (HttpContext.Current.Request.Browser.Win32)
                {
                    result = "(32）";
                }
                else
                {
                    result = "";
                }
            }
            return result;
        }
        /// <summary>
        /// 语言
        /// </summary>
        /// <returns></returns>
        public string Language()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
        }
        /// <summary>
        /// 客户端电脑名称
        /// </summary>
        /// <returns></returns>
        public string UserHostName()
        {
            return HttpContext.Current.Request.UserHostName;
        }
    }
}
