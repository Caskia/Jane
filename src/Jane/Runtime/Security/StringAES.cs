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

            using (var rijndaelCipher = new RijndaelManaged())
            {
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                rijndaelCipher.Key = GetFixedKey(key);
                rijndaelCipher.IV = GetFixedKey(iv);

                var transform = rijndaelCipher.CreateDecryptor();
                var textByte = Convert.FromBase64String(cipherText);
                var plainText = transform.TransformFinalBlock(textByte, 0, textByte.Length);
                return Encoding.UTF8.GetString(plainText);
            }
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

            using (var rijndaelCipher = new RijndaelManaged())
            {
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                rijndaelCipher.Key = GetFixedKey(key);
                rijndaelCipher.IV = GetFixedKey(iv);

                var transform = rijndaelCipher.CreateEncryptor();
                var textByte = Encoding.UTF8.GetBytes(plainText);
                var cipherBytes = transform.TransformFinalBlock(textByte, 0, textByte.Length);

                return Convert.ToBase64String(cipherBytes);
            }
        }

        private static byte[] GetFixedKey(string key, int length = 16)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] bytes = new byte[length];
            int pwdlength = keyBytes.Length;
            if (pwdlength > bytes.Length) pwdlength = bytes.Length;
            Array.Copy(keyBytes, bytes, pwdlength);
            return bytes;
        }
    }
}