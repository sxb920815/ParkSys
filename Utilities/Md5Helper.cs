using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// md5 类
    /// </summary>
    public class Md5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string MD5Encrption(string plainText)
        {
            if (IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException("加密字串为空");
            }

            byte[] Original = Encoding.UTF8.GetBytes(plainText.Trim());
            MD5 s1 = MD5.Create(); //使用MD5 
            byte[] Change = s1.ComputeHash(Original);//加密 
            return ConvertToHex(Change);
        }

        /// <summary>
        /// 对字符串进行MD5，并转换为Hex
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Md5WithHex(string text, Encoding enc)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(enc.GetBytes(text));
            return ConvertToHex(result);
        }

        /// <summary>
        /// 对字符串进行MD5，并转换为Hex
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Md5WithHex(byte[] buff, Encoding enc)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(buff);
            return ConvertToHex(result);
        }

        /// <summary>
        /// 对字符串进行MD5，并转换为Hex
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Md5WithHex(Stream stream, Encoding enc)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(stream);
            return ConvertToHex(result);
        }

        /// <summary>
        /// 对数据转换为HEX模式
        /// </summary>
        /// <param name="bArr">输入字节数组</param>
        /// <returns></returns>
        public static string ConvertToHex(byte[] bArr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bArr.Length; i++)
            {
                byte b = bArr[i];
                int value = (b & 0xFF) + (b < 0 ? 128 : 0);
                sb.Append(value < 16 ? "0" : "");
                sb.Append(value.ToString("x"));
            }
            return sb.ToString();
        }

        public static string MD5Encrption(string plainText, string salted)
        {
            if (IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException("加密字串为空");
            }

            if (IsNullOrEmpty(salted))
            {
                throw new ArgumentNullException("Salted字串为空");
            }

            byte[] Original = Encoding.UTF8.GetBytes(plainText.Trim());
            byte[] SaltValue = Encoding.UTF8.GetBytes(salted.Trim());
            byte[] ToSalt = new byte[Original.Length + SaltValue.Length];
            Original.CopyTo(ToSalt, 0);
            SaltValue.CopyTo(ToSalt, Original.Length);

            MD5 st = MD5.Create();
            byte[] SaltPWD = st.ComputeHash(ToSalt);
            byte[] PWD = new byte[SaltPWD.Length + SaltValue.Length];
            SaltPWD.CopyTo(PWD, 0);
            SaltValue.CopyTo(PWD, SaltPWD.Length);
            return ConvertToHex(PWD);
        }

        private static bool IsNullOrEmpty(string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text.Trim());
        }
    }
}
