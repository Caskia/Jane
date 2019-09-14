namespace Jane.QCloud.Xinge
{
    public class XingePushIOSPayloadAps
    {
        public XingePushIOSPayloadAlert Alert { get; set; }

        public int? Badge { get; set; }

        public string Category { get; set; }

        /// <summary>
        /// 代表静默推送 1
        /// </summary>
        public int? ContentAvailable { get; set; }

        public string Sound { get; set; } = "default";
    }
}