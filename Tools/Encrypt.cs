using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace freePhoto.Tools
{
    /// <summary>
    /// Encrypt 的摘要说明。(加密解密)
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="Input">要加密的字符串</param>
        /// <returns></returns>
        public static string Basic_Encrypt(string Input)
        {
            byte[] rgbKey = new byte[0];
            byte[] rgbIV = new byte[]
			{
				18,
				52,
				86,
				120,
				144,
				171,
				205,
				239
			};
            string text = "$1feel#SyCms#BjSy$";
            string result;
            try
            {
                rgbKey = Encoding.UTF8.GetBytes(text.Substring(0, 8));
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.UTF8.GetBytes(Input);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                result = Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Input">要解密的字符串</param>
        /// <returns></returns>
        public static string Basic_Decrypt(string Input)
        {
            byte[] rgbKey = new byte[0];
            byte[] rgbIV = new byte[]
			{
				18,
				52,
				86,
				120,
				144,
				171,
				205,
				239
			};
            string text = "$1feel#SyCms#BjSy$";
            string result;
            if (!string.IsNullOrEmpty(Input))
            {
                Input = Input.Replace(" ", "+");
                byte[] array = new byte[Input.Length];
                try
                {
                    rgbKey = Encoding.UTF8.GetBytes(text.Substring(0, 8));
                    DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                    array = Convert.FromBase64String(Input);
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                    cryptoStream.Write(array, 0, array.Length);
                    cryptoStream.FlushFinalBlock();
                    Encoding uTF = Encoding.UTF8;
                    result = uTF.GetString(memoryStream.ToArray());
                    return result;
                }
                catch (Exception)
                {
                    result = "";
                    return result;
                }
            }
            result = "";
            return result;
        }
        /// <summary>
        /// 3des加密字符串
        /// </summary>
        /// <param name="a_strString">要加密的字符串</param>
        /// <returns>加密后并经base64编码的字符串</returns>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        public static string Encrypt3DES(string a_strString)
        {
            return Encrypt.Encrypt3DES(Zip.Compress(a_strString), "1feelyqrj960130");
        }
        /// <summary>
        /// 3des加密字符串(不支持中文）
        /// </summary>
        /// <param name="a_strString">要加密的字符串</param>
        /// <param name="a_strKey">密钥</param>
        /// <returns>加密后并经base64编码的字符串</returns>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            a_strString = Zip.Compress(a_strString);
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(a_strKey));
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
            byte[] bytes = Encoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
        }
        /// <summary>
        /// 3des加密字符串
        /// </summary>
        /// <param name="a_strString">要加密的字符串</param>
        /// <param name="a_strKey">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后并经base63编码的字符串</returns>
        /// <remarks>重载，指定编码方式</remarks>
        public static string Encrypt3DES(string a_strString, string a_strKey, Encoding encoding)
        {
            a_strString = Zip.Compress(a_strString);
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(encoding.GetBytes(a_strKey));
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
            byte[] bytes = encoding.GetBytes(a_strString);
            return Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
        }
        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="a_strString">要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        public static string Decrypt3DES(string a_strString)
        {
            return Zip.DeCompress(Encrypt.Decrypt3DES(a_strString, "1feelyqrj960130"));
        }
        /// <summary>
        /// 3des解密字符串（不支持中文）
        /// </summary>
        /// <param name="a_strString">要解密的字符串</param>
        /// <param name="a_strKey">密钥</param>
        /// <returns>解密后的字符串</returns>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(a_strKey));
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
            string text = "";
            try
            {
                byte[] array = Convert.FromBase64String(a_strString);
                text = Encoding.ASCII.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length));
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误：{0}", ex);
                throw new Exception("Invalid Key or input string is not a valid base64 string", ex);
            }
            text = Zip.DeCompress(text);
            return text;
        }
        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="a_strString">要解密的字符串</param>
        /// <param name="a_strKey">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解密后的字符串</returns>
        /// <remarks>静态方法，指定编码方式</remarks>
        public static string Decrypt3DES(string a_strString, string a_strKey, Encoding encoding)
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(encoding.GetBytes(a_strKey));
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
            string text = "";
            try
            {
                byte[] array = Convert.FromBase64String(a_strString);
                text = encoding.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length));
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误：{0}", ex);
                throw new Exception("Invalid Key or input string is not a valid base64 string", ex);
            }
            text = Zip.DeCompress(text);
            return text;
        }
    }
}
