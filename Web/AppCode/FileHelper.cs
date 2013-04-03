using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using freePhoto.Tools;

namespace freePhoto.Web
{
    /// <summary>
    /// 服务配置管理
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetFileContent(string key)
        {
            return GetFileContent(key, true);
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="relative"></param>
        /// <returns></returns>
        public static string GetFileContent(string key, bool relative)
        {
            string relaPath = fun.getapp(key);
            if (string.IsNullOrEmpty(relaPath)) relaPath = key;
            relaPath = string.Format("{0}/{1}", relative ? "~" : "", relaPath);
            string FilePath = HttpContext.Current.Server.MapPath(relaPath);
            string md5 = Md5.MD5(FilePath);
            object obj = cache.GetCache(md5);
            if (obj == null)
            {
                string fileContent = File.ReadAllText(FilePath);
                cache.AddCache(md5, fileContent, FilePath);
                return fileContent;
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}
