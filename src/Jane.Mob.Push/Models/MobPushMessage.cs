using Jane.Extensions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Jane.Mob.Push
{
    public class MobPushMessage
    {
        #region Private Fields

        private string androidTitle;

        private string content;

        private string iosSubtitle;

        private string iosTitle;

        #endregion Private Fields

        /// <summary>
        /// 设置推送别名集合[“alias1″,”alias2”]，target=2则必选
        ///（集合元素限制1000个以内）
        /// </summary>
        [JsonPropertyName("alias")]
        public List<string> Alias { get; set; }

        /// <summary>
        /// 应用KEY
        /// </summary>
        [JsonPropertyName("appkey")]
        [NotNull]
        public string AppKey { get; set; }

        /// <summary>
        /// 用户分群ID，target=6则必选
        /// </summary>
        [JsonPropertyName("block")]
        public string Block { get; set; }

        /// <summary>
        /// 推送地理位置(城市)，target=5则必选
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// 推送内容
        /// </summary>
        [JsonPropertyName("content")]
        [NotNull]
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value.Truncate(40);
            }
        }

        /// <summary>
        /// 自定义类型标题
        /// </summary>
        [JsonPropertyName("customTitle")]
        public string CustomTitle { get; set; }

        /// <summary>
        /// 自定义消息类型：text 文本消息
        /// </summary>
        [JsonPropertyName("customType")]
        public string CustomType { get; set; }

        /// <summary>
        /// moblink功能的参数
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// extras:附加字段键值对的方式，扩展数据 json
        /// </summary>
        [JsonPropertyName("extras")]
        public string Extras { get; set; }

        /// <summary>
        /// 可使用平台，
        /// 1、android ； 2、ios ；如包含ios和android则为[1, 2]
        /// </summary>
        [JsonPropertyName("plats")]
        [NotNull]
        public List<int> Plats { get; set; } = new List<int>();

        /// <summary>
        /// 设置推送Registration Id集合[“Id1″,”id2”]，target=4则必选
        ///（集合元素限制1000个以内）
        /// </summary>
        [JsonPropertyName("registrationIds")]
        public List<string> RegistrationIds { get; set; }

        /// <summary>
        /// moblink功能的uri
        /// </summary>
        [JsonPropertyName("scheme")]
        public string Scheme { get; set; }

        /// <summary>
        /// 设置推送标签集合[“tag1″,”tag2”]，target=3则必选
        ///（集合元素限制100个以内）
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// 推送范围:1广播；2别名；3标签；4regid；5地理位置;6用户分群
        /// </summary>
        [JsonPropertyName("target")]
        [NotNull]
        public int Target { get; set; }

        /// <summary>
        /// 推送类型：1通知；2自定义
        /// </summary>
        [JsonPropertyName("type")]
        [NotNull]
        public int Type { get; set; }

        /// <summary>
        /// 离线时间为0，或者在1~10天以内，单位天， 默认1天
        /// </summary>
        [JsonPropertyName("unlineTime")]
        public int? UnlineTime { get; set; }

        /// <summary>
        /// 调用方提供的唯一编号，需要在当前appkey下唯一不可重复
        /// </summary>
        [JsonPropertyName("workno")]
        public string WorkNo { get; set; }

        #region Android

        /// <summary>
        /// androidstyle样式具体内容：
        /// 0、默认通知无；
        /// 1、长内容则为内容数据；
        /// 2、大图则为图片地址；
        /// 3、横幅则为多行内容
        /// </summary>
        [JsonPropertyName("androidContent")]
        public List<string> AndroidContent { get; set; }

        /// <summary>
        /// 指示灯，默认true
        /// </summary>
        [JsonPropertyName("androidLight")]
        public bool? AndroidLight { get; set; }

        /// <summary>
        /// 震动，默认true
        /// </summary>
        [JsonPropertyName("androidShake")]
        public bool? AndroidShake { get; set; }

        /// <summary>
        /// Android显示样式标识
        /// normal(0,”普通通知”),
        /// bigtext(1,”BigTextStyle通知，点击后显示大段文字内容”),
        /// bigpicture(2,”BigPictureStyle，大图模式”),
        /// hangup(3,”横幅（收件箱）通知”);
        /// </summary>
        [JsonPropertyName("androidstyle")]
        public int? AndroidStyle { get; set; }

        /// <summary>
        /// Android显示标题
        /// </summary>
        [JsonPropertyName("androidTitle")]
        public string AndroidTitle
        {
            get
            {
                return androidTitle;
            }
            set
            {
                androidTitle = value.Truncate(20);
            }
        }

        /// <summary>
        /// 提示音，默认true
        /// </summary>
        [JsonPropertyName("androidVoice")]
        public bool? AndroidVoice { get; set; }

        #endregion Android

        #region IOS

        /// <summary>
        /// 可直接指定 APNs 推送通知的 badge，直接展示在桌面应用图标的右上角，含义是应用未读的消息数。默认值为 1。
        /// </summary>
        [JsonPropertyName("iosBadge")]
        public int? IOSBadge { get; set; }

        /// <summary>
        /// 只有IOS8及以上系统才支持此参数推送
        /// </summary>
        [JsonPropertyName("iosCategory")]
        public string IOSCategory { get; set; }

        [JsonPropertyName("iosContentAvailable")]
        public int? IOSContentAvailable { get; set; }

        /// <summary>
        /// 需要在附加字段中配置相应参数
        /// </summary>
        [JsonPropertyName("iosMutableContent")]
        public int? IOSMutableContent { get; set; }

        /// <summary>
        /// plat = 2下，0测试环境，1生产环境，默认1
        /// </summary>
        [JsonPropertyName("iosProduction")]
        public int? IOSProduction { get; set; }

        /// <summary>
        /// 如果只携带content-available: 1,不携带任何badge，sound 和消息内容等参数，
        /// 则可以不打扰用户的情况下进行内容更新等操作即为“Silent Remote Notifications”。
        /// </summary>
        [JsonPropertyName("iosSlientPush")]
        public int? IOSSlientPush { get; set; }

        /// <summary>
        /// APNs通知，通过这个字段指定声音。默认为default，即系统默认声音。 如果设置为空值，则为静音。
        /// 如果设置为特殊的名称，则需要你的App里配置了该声音才可以正常。
        /// </summary>
        [JsonPropertyName("iosSound")]
        public string IOSSound { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        [JsonPropertyName("iosSubtitle")]
        public string IOSSubtitle
        {
            get
            {
                return iosSubtitle;
            }
            set
            {
                iosSubtitle = value.Truncate(20);
            }
        }

        /// <summary>
        /// 如果不设置，则默认的通知标题为应用的名称
        /// </summary>
        [JsonPropertyName("iosTitle")]
        public string IOSTitle
        {
            get
            {
                return iosTitle;
            }
            set
            {
                iosTitle = value.Truncate(20);
            }
        }

        #endregion IOS
    }
}