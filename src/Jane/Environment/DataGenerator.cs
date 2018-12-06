using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Jane
{
    public class DataGenerator : IDataGenerator
    {
        private Dictionary<int, string> _base36Code = new Dictionary<int, string>() {
            { 0,"0"}, { 1,"1"}, { 2,"2"}, { 3,"3"}, { 4,"4"}, { 5,"5"}, { 6,"6"}, { 7,"7"}, { 8,"8"}, { 9,"9"},
            { 10,"A"}, { 11,"B"}, { 12,"C"}, { 13,"D"},{ 14,"E"}, { 15,"F"}, { 16,"G"}, { 17,"H"}, { 18,"I"}, { 19,"J"},
            { 20,"K"}, { 21,"L"}, { 22,"M"}, { 23,"N"},{ 24,"O"}, { 25,"P"}, { 26,"Q"}, { 27,"R"}, { 28,"S"}, { 29,"T"},
            { 30,"U"}, { 31,"V"}, { 32,"W"}, { 33,"X"},{ 34,"Y"}, { 35,"Z"}
        };

        private Dictionary<int, string> _base64Code = new Dictionary<int, string>() {
            { 0,"0"}, { 1,"1"}, { 2,"2"}, { 3,"3"}, { 4,"4"}, { 5,"5"}, { 6,"6"}, { 7,"7"}, { 8,"8"}, { 9,"9"},
            { 10,"a"}, { 11,"b"}, { 12,"c"}, { 13,"d"}, { 14,"e"}, { 15,"f"}, { 16,"g"}, { 17,"h"}, { 18,"i"}, { 19,"j"},
            { 20,"k"}, { 21,"l"}, { 22,"m"}, { 23,"n"}, { 24,"o"}, { 25,"p"}, { 26,"q"}, { 27,"r"}, { 28,"s"}, { 29,"t"},
            { 30,"u"}, { 31,"v"}, { 32,"w"}, { 33,"x"}, { 34,"y"}, { 35,"z"}, { 36,"A"}, { 37,"B"}, { 38,"C"}, { 39,"D"},
            { 40,"E"}, { 41,"F"}, { 42,"G"}, { 43,"H"}, { 44,"I"}, { 45,"J"}, { 46,"K"}, { 47,"L"}, { 48,"M"}, { 49,"N"},
            { 50,"O"}, { 51,"P"}, { 52,"Q"}, { 53,"R"}, { 54,"S"}, { 55,"T"}, { 56,"U"}, { 57,"V"}, { 58,"W"}, { 59,"X"},
            { 60,"Y"}, { 61,"Z"}, { 62,"-"}, { 63,"_"}
        };

        public Dictionary<string, int> _base36CodeReverse
        {
            get
            {
                return Enumerable.Range(0, _base36Code.Count()).ToDictionary(i => _base36Code[i], i => i);
            }
        }

        public Dictionary<string, int> _base64CodeReverse
        {
            get
            {
                return Enumerable.Range(0, _base64Code.Count()).ToDictionary(i => _base64Code[i], i => i);
            }
        }

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

        public string NumberToS36(long number)
        {
            var a = new StringBuilder();
            while (number >= 1)
            {
                var index = Convert.ToInt16(number - (number / 36) * 36);
                a.Insert(0, _base36Code[index]);
                number = number / 36;
            }
            return a.ToString();
        }

        public string NumberToS64(long number)
        {
            var a = new StringBuilder();
            while (number >= 1)
            {
                var index = Convert.ToInt16(number - (number / 64) * 64);
                a.Insert(0, _base64Code[index]);
                number = number / 64;
            }
            return a.ToString();
        }

        public long S36ToNumber(string s36)
        {
            var a = 0L;
            var power = s36.Length - 1;

            for (int i = 0; i <= power; i++)
            {
                a += _base36CodeReverse[s36[power - i].ToString()] * Convert.ToInt64(Math.Pow(36, i));
            }

            return a;
        }

        public long S64ToNumber(string s64)
        {
            var a = 0L;
            var power = s64.Length - 1;

            for (int i = 0; i <= power; i++)
            {
                a += _base64CodeReverse[s64[power - i].ToString()] * Convert.ToInt64(Math.Pow(64, i));
            }

            return a;
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// create random seed
        /// </summary>
        /// <returns></returns>
        private int RandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        #endregion Utilities
    }
}