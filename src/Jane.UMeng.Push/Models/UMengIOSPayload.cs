﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.UMeng.Push
{
    public class UMengIOSPayload : UMengPayload
    {
        [JsonProperty("aps")]
        public UMengIOSPayloadAps Aps { get; set; }

        [JsonProperty("body")]
        public Dictionary<string, string> Body { get; set; }
    }
}