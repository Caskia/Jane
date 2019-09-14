using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class DirtyWordsGetResponse : QCloudResponse
    {
        public List<string> DirtyWordsList { get; set; } = new List<string>();
    }
}