using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Jane
{
    public class DataGenerator : IDataGenerator
    {
        #region Methods

        public string GetRandomString(byte length, bool includeDigit = true, bool includeUppercase = true, bool includeLowercase = true)
        {
            byte[] pw = new byte[length];
            byte value = 0;
            List<byte> types = new List<byte>();
            if (includeDigit) types.Add(1);
            if (includeUppercase) types.Add(2);
            if (includeLowercase) types.Add(3);
            if (types.Count == 0)
            {
                throw new Exception("Must inculde any character type.");
            }
            for (byte i = 0; i < length; i++)
            {
                Random rnd = new Random(RandomSeed());
                int typeIndex = rnd.Next(0, types.Count);
                switch (types[typeIndex])
                {
                    case 1: //number
                        value = (byte)rnd.Next(48, 57);
                        pw[i] = value;
                        break;

                    case 2: //upper
                        value = (byte)rnd.Next(65, 90);
                        pw[i] = value;
                        break;

                    case 3: //lower
                        value = (byte)rnd.Next(97, 122);
                        pw[i] = value;
                        break;
                }
            }
            return ASCIIEncoding.ASCII.GetString(pw);
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// create random seed
        /// </summary>
        /// <returns></returns>
        public int RandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        #endregion Utilities
    }
}