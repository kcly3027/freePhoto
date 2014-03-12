using System;
using System.Security.Cryptography;
using System.Text;
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
        ///  Hash值（16位）
        /// </summary>
        public static string Md5Hash16(byte[] bytes)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(bytes);
            // 16位
            StringBuilder sb = new StringBuilder();
            for (int i = 4; i < 12; i++)
                sb.Append(data[i].ToString("x"));
            return sb.ToString();
        }

        /// <summary>
        ///  Hash值（32位）
        /// </summary>
        public static string Md5Hash32(byte[] bytes)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(bytes);
            // 32位
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data) sb.Append(b.ToString("x"));
            return sb.ToString();
        }

        /// <summary>
        ///  文件唯一资源码
        /// </summary>
        /// <param name="bytes">文件流</param>
        /// <returns></returns>
        public static string SkyFileUnKey(byte[] bytes)
        {
            return Md5Hash32(bytes);
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
