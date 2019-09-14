using System.Collections.Generic;

namespace Jane.Push
{
    public class PushMessage
    {
        public string AccountId { get; set; }

        public string AccountType { get; set; }

        public string Content { get; set; }

        public Dictionary<string, string> CustomData { get; set; }

        public string GroupKey { get; set; }

        public string IdempotentKey { get; set; }

        public string Title { get; set; }
    }
}