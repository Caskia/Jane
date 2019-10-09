using System;
using System.Security.Cryptography;
using System.Text;

namespace Jane.Runtime.Security
{
    public static class StringAES
    {
        public static string DecryptFromBase64String(string cipherText, string key, string iv)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(iv))
            {
                throw new ArgumentNullException(nameof(iv));
            }

            var keyArray = Convert.FromBase64String(key);
            var toEncryptArray = Convert.FromBase64String(cipherText);

            var des = Aes.Create();
            des.Key = keyArray;
            des.Padding = PaddingMode.PKCS7;
            des.IV = Convert.FromBase64String(iv);

            var cTransform = des.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        public static string EncryptToBase64String(string plainText, string key, string iv)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(iv))
            {
                throw new ArgumentNullException(nameof(iv));
            }

            var keyArray = Convert.FromBase64String(key);
            var toEncryptArray = Encoding.UTF8.GetBytes(plainText);

            var des = Aes.Create();
            des.Key = keyArray;
            des.Padding = PaddingMode.PKCS7;
            des.IV = Convert.FromBase64String(iv);

            var cTransform = des.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray);
        }
    }
}