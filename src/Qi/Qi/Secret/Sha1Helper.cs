using System.Security.Cryptography;
using System.Text;

namespace Qi.Secret
{
    public static class Sha1Helper
    {
        #region Sha1

        /// <summary>
        ///用sha1 utf8进行加密
        /// </summary>
        public static byte[] Sha1Utf8(this string content)
        {
            return Sha(content, Encoding.UTF8, new SHA1CryptoServiceProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha1ASCII(this string content)
        {
            return Sha(content, Encoding.ASCII, new SHA1CryptoServiceProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha1Unicode(this string content)
        {
            return Sha(content, Encoding.Unicode, new SHA1CryptoServiceProvider());
        }

        #endregion

        #region sha384

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha384Utf8(this string content)
        {
            return Sha(content, Encoding.UTF8, new SHA384CryptoServiceProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha384ASCII(this string content)
        {
            return Sha(content, Encoding.ASCII, new SHA384CryptoServiceProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha384Unicode(this string content)
        {
            return Sha(content, Encoding.Unicode, new SHA384CryptoServiceProvider());
        }

        #endregion

        #region sha512

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha512Utf8(this string content)
        {
            return Sha(content, Encoding.UTF8, new SHA512CryptoServiceProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha512ASCII(this string content)
        {
            return Sha(content, Encoding.ASCII, new SHA512CryptoServiceProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Sha512Unicode(this string content)
        {
            return Sha(content, Encoding.Unicode, new SHA512CryptoServiceProvider());
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">加密的方法</param>
        /// <param name="getBytesFunc">把content转换为byte的方法</param>
        /// <param name="hashAlgorithm">加密的方式</param>
        /// <returns></returns>
        private static byte[] Sha(string content, Encoding getBytesFunc, HashAlgorithm hashAlgorithm)
        {
            byte[] iput = getBytesFunc.GetBytes(content);
            return hashAlgorithm.ComputeHash(iput);
        }
    }
}