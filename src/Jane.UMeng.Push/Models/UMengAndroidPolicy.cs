using Newtonsoft.Json;

namespace Jane.UMeng.Push
{
    public class UMengAndroidPolicy : IUMengPolicy
    {
        /// <summary>
        /// 可选，消息过期时间，其值不可小于发送时间或者
        /// start_time(如果填写了的话)，
        /// 如果不填写此参数，默认为3天后过期。格式同start_time
        /// </summary>
        [JsonProperty("expire_time")]
        public string ExpireTime { get; set; }

        /// <summary>
        /// 可选，发送限速，每秒发送的最大条数。最小值1000
        /// 开发者发送的消息如果有请求自己服务器的资源，可以考虑此参数。
        /// </summary>
        [JsonProperty("max_send_num")]
        public int MaxSendNum { get; set; }

        /// <summary>
        /// 可选，开发者对消息的唯一标识，服务器会根据这个标识避免重复发送。
        /// 有些情况下（例如网络异常）开发者可能会重复调用API导致
        /// 消息多次下发到客户端。如果需要处理这种情况，可以考虑此参数。
        /// 注意, out_biz_no只对任务生效。
        /// </summary>
        [JsonProperty("out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 可选，定时发送时，若不填写表示立即发送。
        /// 定时发送时间不能小于当前时间
        /// 格式: "yyyy-MM-dd HH:mm:ss"。
        /// 注意，start_time只对任务生效。
        /// </summary>
        [JsonProperty("start_time")]
        public string StartTime { get; set; }
    }
}