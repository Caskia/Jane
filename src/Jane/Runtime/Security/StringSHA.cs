using System;
using System.Text;

namespace Jane.Runtime.Security
{
    public static class StringSHA
    {
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }

        public static string SHA1(string content, Encoding encode)
        {
            var bytes = encode.GetBytes(content);
            var sha1 = System.Security.Cryptography.SHA1.Create();

            var hash = sha1.ComputeHash(bytes);

            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}