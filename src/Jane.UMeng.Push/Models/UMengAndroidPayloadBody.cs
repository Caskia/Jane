using Jane.Extensions;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.UMeng.Push
{
    public class UMengAndroidPayloadBody
    {
        #region Private Fields

        private string text;

        private string ticker;

        private string title;

        #endregion Private Fields

        /// <summary>
        /// 当after_open=go_activity时，必填。
        /// 通知栏点击后打开的Activity
        /// </summary>
        [JsonPropertyName("activity")]
        public string Activity { get; set; }

        /// <summary>
        /// 必填，值可以为:
        ///  "go_app": 打开应用
        ///  "go_url": 跳转到URL
        ///  "go_activity": 打开特定的activity
        ///  "go_custom": 用户自定义内容。
        /// </summary>
        [JsonPropertyName("after_open")]
        public string AfterOpen { get; set; }

        /// <summary>
        /// 可选，默认为0，用于标识该通知采用的样式。使用该参数时，
        /// 开发者必须在SDK里面实现自定义通知栏样式。
        /// </summary>
        [JsonPropertyName("builder_id")]
        public int BuilderId { get; set; } = 0;

        /// <summary>
        /// 当display_type=message时, 必填
        /// 当display_type=notification且
        /// after_open=go_custom时，必填
        /// 用户自定义内容，可以为字符串或者JSON格式。
        /// </summary>
        [JsonPropertyName("custom")]
        public Dictionary<string, string> Custom { get; set; }

        /// <summary>
        /// 可选，状态栏图标ID，R.drawable.[smallIcon]，
        /// 如果没有，默认使用应用图标。
        /// 图片要求为24*24dp的图标，或24*24px放在drawable-mdpi下。
        /// 注意四周各留1个dp的空白像素
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 可选，通知栏大图标的URL链接。该字段的优先级大于largeIcon。
        /// 该字段要求以http或者https开头。
        /// </summary>
        [JsonPropertyName("img")]
        public string Img { get; set; }

        /// <summary>
        /// 可选，通知栏拉开后左侧图标ID，R.drawable.[largeIcon]，
        /// 图片要求为64*64dp的图标，
        /// 可设计一张64*64px放在drawable-mdpi下，
        /// 注意图片四周留空，不至于显示太拥挤
        /// </summary>
        [JsonPropertyName("largeIcon")]
        public string LargeIcon { get; set; }

        /// <summary>
        /// 可选，收到通知是否闪灯，默认为"true"
        /// </summary>
        [JsonPropertyName("play_lights")]
        public string PlayLights { get; set; } = "true";

        /// <summary>
        /// 可选，收到通知是否发出声音，默认为"true"
        /// </summary>
        [JsonPropertyName("play_sound")]
        public string PlaySound { get; set; } = "true";

        /// <summary>
        /// 可选，收到通知是否震动，默认为"true"
        /// </summary>
        [JsonPropertyName("play_vibrate")]
        public string PlayVibrate { get; set; } = "true";

        /// <summary>
        /// 可选，通知声音，R.raw.[sound]。
        /// 如果该字段为空，采用SDK默认的声音，即res/raw/下的
        /// umeng_push_notification_default_sound声音文件。如果
        /// SDK默认声音文件不存在，则使用系统默认Notification提示音。
        /// </summary>
        [JsonPropertyName("sound")]
        public string Sound { get; set; }

        /// <summary>
        /// 必填，通知文字描述
        /// </summary>
        [JsonPropertyName("text")]
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value.TruncateWithPostfix(160);
            }
        }

        /// <summary>
        /// 必填，通知栏提示文字
        /// </summary>
        [JsonPropertyName("ticker")]
        public string Ticker
        {
            get
            {
                return ticker;
            }
            set
            {
                ticker = value.TruncateWithPostfix(80);
            }
        }

        /// <summary>
        /// 必填，通知标题
        /// </summary>
        [JsonPropertyName("title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value.TruncateWithPostfix(80);
            }
        }

        /// <summary>
        /// 当after_open=go_url时，必填。
        /// 通知栏点击后跳转的URL，要求以http或者https开头
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}