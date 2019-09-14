using System.Collections.Generic;

namespace Jane.QCloud.Cos
{
    public class SignatureInput
    {
        public SortedDictionary<string, string> HttpHeaders { get; set; } = new SortedDictionary<string, string>();

        public string HttpMethod { get; set; }

        public SortedDictionary<string, string> HttpParameters { get; set; } = new SortedDictionary<string, string>();

        public string HttpUri { get; set; }

        public string KeyTime { get; set; }

        public string SignTime { get; set; }
    }
}