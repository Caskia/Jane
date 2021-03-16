using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.UMeng.Push
{
    public class UMengAndroidPayload : IUMengPayload
    {
        /// <summary>
        /// 必填，消息体。
        /// 当display_type=message时，body的内容只需填写custom字段。
        /// 当display_type=notification时，body包含如下参数:
        /// </summary>
        [JsonProperty("body")]
        public UMengAndroidPayloadBody Body { get; set; }

        /// <summary>
        /// 必填，消息类型: notification(通知)、message(消息)
        /// </summary>
        [JsonProperty("display_type")]
        public string DisplayType { get; set; }

        /// <summary>
        /// 可选，JSON格式，用户自定义key-value。只对"通知"
        /// (display_type=notification)生效。
        /// 可以配合通知到达后，打开App/URL/Activity使用。
        /// </summary>
        [JsonProperty("extra")]
        public IDictionary<string, string> Extra { get; set; }
    }
}