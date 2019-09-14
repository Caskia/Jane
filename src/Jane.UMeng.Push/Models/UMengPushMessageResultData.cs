using Newtonsoft.Json;

namespace Jane.UMeng.Push
{
    public class UMengPushMessageResultData
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error_msg")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 单播类消息(type为unicast、listcast、customizedcast且不带file_id)返回
        /// </summary>
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        /// <summary>
        /// 任务类消息(type为broadcast、groupcast、filecast、customizedcast且file_id不为空)返回
        /// </summary>
        [JsonProperty("task_id")]
        public string TaskId { get; set; }
    }
}