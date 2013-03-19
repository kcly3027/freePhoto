using System;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
namespace freePhoto.Tools
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class Md5
    {
        /// <summary>
        /// 32 位 MD5 加密 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MD5(string s)
        {
            string result;
            if (null == HttpContext.Current)
            {
                if (s == null || 0 == s.Length)
                {
                    s = string.Empty;
                }
                result = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5").ToLower();
            }
            else
            {
                MD5 mD = System.Security.Cryptography.MD5.Create();
                byte[] array = HttpContext.Current.Request.ContentEncoding.GetBytes(s);
                array = mD.ComputeHash(array);
                result = BitConverter.ToString(array).Replace("-", "").ToLower();
            }
            return result;
        }
        /// <summary>
        /// MD5加密，16位和32位
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            string result;
            if (code == 16)
            {
                result = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else
            {
                result = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return result;
        }
        /// <summary>
        /// 密码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Password"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CheckPassword(string str, string Password, int code)
        {
            return Md5.md5(str, code) == Password;
        }
    }
}
