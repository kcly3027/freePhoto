using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Text;
namespace freePhoto.Tools
{
    /// <summary>
    /// 压缩方式。
    /// </summary>
    public enum CompressionType
    {
        /// <summary>
        /// GZip 压缩格式
        /// </summary>
        GZip,
        /// <summary>
        /// BZip2 压缩格式
        /// </summary>
        BZip2,
        /// <summary>
        /// Zip 压缩格式
        /// </summary>
        Zip
    }
    /// <summary>
    /// 使用 SharpZipLib 进行压缩的辅助类，简化对字节数组和字符串进行压缩的操作。
    /// </summary>
    public class Zip
    {
        /// <summary>
        /// 压缩供应者，默认为 GZip。
        /// </summary>
        public static CompressionType CompressionProvider = CompressionType.GZip;
        /// <summary>
        /// 从原始字节数组生成已压缩的字节数组。
        /// </summary>
        /// <param name="bytesToCompress">原始字节数组。</param>
        /// <returns>返回已压缩的字节数组</returns>
        public static byte[] Compress(byte[] bytesToCompress)
        {
            MemoryStream memoryStream = new MemoryStream();
            Stream stream = Zip.OutputStream(memoryStream);
            stream.Write(bytesToCompress, 0, bytesToCompress.Length);
            stream.Close();
            return memoryStream.ToArray();
        }
        /// <summary>
        /// 从原始字符串生成已压缩的字符串。
        /// </summary>
        /// <param name="stringToCompress">原始字符串。</param>
        /// <returns>返回已压缩的字符串。</returns>
        public static string Compress(string stringToCompress)
        {
            byte[] inArray = Zip.CompressToByte(stringToCompress);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// 从原始字符串生成已压缩的字节数组。
        /// </summary>
        /// <param name="stringToCompress">原始字符串。</param>
        /// <returns>返回已压缩的字节数组。</returns>
        public static byte[] CompressToByte(string stringToCompress)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(stringToCompress);
            return Zip.Compress(bytes);
        }
        /// <summary>
        /// 从已压缩的字符串生成原始字符串。
        /// </summary>
        /// <param name="stringToDecompress">已压缩的字符串。</param>
        /// <returns>返回原始字符串。</returns>
        public static string DeCompress(string stringToDecompress)
        {
            string text = string.Empty;
            if (stringToDecompress == null)
            {
                throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");
            }
            string result;
            try
            {
                byte[] bytesToDecompress = Convert.FromBase64String(stringToDecompress.Trim());
                text = Encoding.Unicode.GetString(Zip.DeCompress(bytesToDecompress));
            }
            catch (NullReferenceException ex)
            {
                result = ex.Message;
                return result;
            }
            result = text;
            return result;
        }
        /// <summary>
        /// 从已压缩的字节数组生成原始字节数组。
        /// </summary>
        /// <param name="bytesToDecompress">已压缩的字节数组。</param>
        /// <returns>返回原始字节数组。</returns>
        public static byte[] DeCompress(byte[] bytesToDecompress)
        {
            byte[] array = new byte[4096];
            Stream stream = Zip.InputStream(new MemoryStream(bytesToDecompress));
            MemoryStream memoryStream = new MemoryStream();
            while (true)
            {
                int num = stream.Read(array, 0, array.Length);
                if (num <= 0)
                {
                    break;
                }
                memoryStream.Write(array, 0, num);
            }
            stream.Close();
            byte[] result = memoryStream.ToArray();
            memoryStream.Close();
            return result;
        }
        /// <summary>
        /// 从给定的流生成压缩输出流。
        /// </summary>
        /// <param name="inputStream">原始流。</param>
        /// <returns>返回压缩输出流。</returns>
        private static Stream OutputStream(Stream inputStream)
        {
            Stream result;
            switch (Zip.CompressionProvider)
            {
                case CompressionType.GZip:
                    result = new GZipOutputStream(inputStream);
                    break;
                case CompressionType.BZip2:
                    result = new BZip2OutputStream(inputStream);
                    break;
                case CompressionType.Zip:
                    result = new ZipOutputStream(inputStream);
                    break;
                default:
                    result = new GZipOutputStream(inputStream);
                    break;
            }
            return result;
        }
        /// <summary>
        /// 从给定的流生成压缩输入流。
        /// </summary>
        /// <param name="inputStream">原始流。</param>
        /// <returns>返回压缩输入流。</returns>
        private static Stream InputStream(Stream inputStream)
        {
            Stream result;
            switch (Zip.CompressionProvider)
            {
                case CompressionType.GZip:
                    result = new GZipInputStream(inputStream);
                    break;
                case CompressionType.BZip2:
                    result = new BZip2InputStream(inputStream);
                    break;
                case CompressionType.Zip:
                    result = new ZipInputStream(inputStream);
                    break;
                default:
                    result = new GZipInputStream(inputStream);
                    break;
            }
            return result;
        }
    }
}
