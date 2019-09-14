using Newtonsoft.Json;

namespace Jane.UMeng.Push
{
    public class UMengPushMessageResult
    {
        [JsonProperty("data")]
        public UMengPushMessageResultData Data { get; set; }

        /// <summary>
        /// 返回结果，"SUCCESS"或者"FAIL"
        /// </summary>
        [JsonProperty("ret")]
        public string Ret { get; set; }
    }
}