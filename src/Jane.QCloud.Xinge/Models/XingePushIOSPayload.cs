using System.Collections.Generic;

namespace Jane.QCloud.Xinge
{
    public class XingePushIOSPayload
    {
        public XingePushIOSPayloadAps Aps { get; set; }

        public Dictionary<string, string> Body { get; set; }
    }
}