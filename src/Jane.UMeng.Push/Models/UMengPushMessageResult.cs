using System.Text.Json.Serialization;

namespace Jane.UMeng.Push
{
    public class UMengPushMessageResult
    {
        [JsonPropertyName("data")]
        public UMengPushMessageResultData Data { get; set; }

        /// <summary>
        /// 返回结果，"SUCCESS"或者"FAIL"
        /// </summary>
        [JsonPropertyName("ret")]
        public string Ret { get; set; }
    }
}