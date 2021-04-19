using System.Text.Json.Serialization;

namespace Jane.UMeng.Push
{
    public class UMengPushMessage
    {
        /// <summary>
        /// 当type=customizedcast时, 选填(此参数和file_id二选一)
        /// 开发者填写自己的alias, 要求不超过500个alias, 多个alias以英文逗号间隔
        /// 在SDK中调用setAlias(alias, alias_type)时所设置的alias
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// 当type=customizedcast时, 必填
        /// alias的类型, alias_type可由开发者自定义, 开发者在SDK中
        /// 调用setAlias(alias, alias_type)时所设置的alias_type
        /// </summary>
        [JsonPropertyName("alias_type")]
        public string AliasType { get; set; }

        /// <summary>
        /// 必填，应用唯一标识
        /// </summary>
        [JsonPropertyName("appkey")]
        public string AppKey { get; set; }

        /// <summary>
        /// 可选，发送消息描述，建议填写。
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// 当type=unicast时, 必填, 表示指定的单个设备
        /// 当type=listcast时, 必填, 要求不超过500个, 以英文逗号分隔
        /// </summary>
        [JsonPropertyName("device_tokens")]
        public string DeviceTokens { get; set; }

        /// <summary>
        /// 当type=filecast时，必填，file内容为多条device_token，以回车符分割
        /// 当type=customizedcast时，选填(此参数和alias二选一)
        ///   file内容为多条alias，以回车符分隔。注意同一个文件内的alias所对应
        ///   的alias_type必须和接口参数alias_type一致。
        /// 使用文件播需要先调用文件上传接口获取file_id，参照"2.4文件上传接口"
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// 当type=groupcast时，必填，用户筛选条件，如用户标签、渠道等，参考附录G
        /// </summary>
        [JsonPropertyName("filter")]
        public UMengPushMessageFilter Filter { get; set; }

        /// <summary>
        /// 可选，正式/测试模式。默认为true
        /// 测试模式只会将消息发给测试设备。测试设备需要到web上添加。
        /// Android: 测试设备属于正式设备的一个子集。
        /// </summary>
        [JsonPropertyName("production_mode")]
        public bool ProductionMode { get; set; } = true;

        /// <summary>
        /// 必填，时间戳，10位或者13位均可，时间戳有效期为10分钟
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string TimeStamp { get; set; }

        /// <summary>
        /// 必填，消息发送类型,其值可以为:
        ///   unicast-单播
        ///   listcast-列播，要求不超过500个device_token
        ///   filecast-文件播，多个device_token可通过文件形式批量发送
        ///   broadcast-广播
        ///   groupcast-组播，按照filter筛选用户群, 请参照filter参数
        ///   customizedcast，通过alias进行推送，包括以下两种case:
        ///     - alias: 对单个或者多个alias进行推送
        ///     - file_id: 将alias存放到文件后，根据file_id来推送
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}