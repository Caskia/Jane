using System.Globalization;

namespace Jane.Utils
{
    public static class StringUtils
    {
        public static string CharToLowerString(char c)
        {
            return char.ToLower(c, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
        }

        public static string FirstLetterToUpper(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            char[] a = str.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string ToCustomSeparatedCase(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (!char.IsUpper(s[0]))
                return s;

            string separatedCase = CharToLowerString(s[0]);
            if (s.Length > 1)
            {
                for (int i = 1; i < s.Length; i++)
                {
                    if (char.IsUpper(s[i]))
                    {
                        separatedCase += '_' + CharToLowerString(s[i]);
                    }
                    else
                    {
                        separatedCase += s[i];
                    }
                }
            }
            return separatedCase;
        }

        public static string ToLessFloatString(decimal number, decimal skip)
        {
            return (number / skip).ToString("F");
        }

        public static string ToNotNullString(string str)
        {
            if (str == null)
            {
                return "";
            }
            return str;
        }

        public static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}