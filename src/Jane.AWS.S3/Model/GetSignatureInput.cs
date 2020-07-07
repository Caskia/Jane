using System;

namespace Jane.AWS.S3
{
    public class GetSignatureInput
    {
        public string Acl { get; set; } = "public-read";

        public string BucketName { get; set; }

        public string ContentType { get; set; }

        public DateTime ExpirationTime { get; set; }

        public string Key { get; set; }

        public string Region { get; set; }

        public DateTime SignTime { get; set; }
    }
}