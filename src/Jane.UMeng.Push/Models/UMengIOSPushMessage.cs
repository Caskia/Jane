﻿using System.Text.Json.Serialization;

namespace Jane.UMeng.Push
{
    public class UMengIOSPushMessage : UMengPushMessage
    {
        /// <summary>
        /// 必填，JSON格式，具体消息内容(Android最大为1840B, iOS最大为2012B)
        /// </summary>
        [JsonPropertyName("payload")]
        public UMengIOSPayload Payload { get; set; }

        [JsonPropertyName("policy")]
        public UMengIOSPolicy Policy { get; set; }
    }
}