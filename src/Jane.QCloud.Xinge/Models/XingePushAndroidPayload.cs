using System.Collections.Generic;

namespace Jane.QCloud.Xinge
{
    public class XingePushAndroidPayload
    {
        /// <summary>
        /// 设置点击通知栏之后的行为，默认为打开app
        /// </summary>
        public XingePushAndroidPayloadAction Action { get; set; }

        /// <summary>
        /// 本地通知样式标识
        /// </summary>
        public int? BuilderId { get; set; }

        /// <summary>
        /// 通知栏是否可清除
        /// </summary>
        public int? Clearable { get; set; }

        /// <summary>
        /// 用户自定义的键值对
        /// </summary>
        public Dictionary<string, string> CustomContent { get; set; }

        /// <summary>
        /// 应用内图标文件名或者下载图标的url地址
        /// </summary>
        public string IconRes { get; set; }

        /// <summary>
        /// 通知栏图标是应用内图标还是上传图标
        /// 1）0：应用内图标
        /// 2）1：上传图标
        /// </summary>
        public int? IconType { get; set; }

        /// <summary>
        /// 是否使用呼吸灯
        /// 1）0：不使用呼吸灯
        /// 2）1：使用呼吸灯
        /// </summary>
        public int? Lights { get; set; }

        /// <summary>
        /// 通知消息对象的唯一标识（只对信鸽通道生效）
        /// 1）大于0：会覆盖先前相同id的消息
        /// 2）等于0：展示本条通知且不影响其他消息
        /// 3）等于-1：将清除先前所有消息，仅展示本条消息
        /// </summary>
        public int? NId { get; set; }

        /// <summary>
        /// 是否有铃声
        /// 1）0：没有铃声
        /// 2）1：有铃声
        /// </summary>
        public int? Ring { get; set; }

        /// <summary>
        /// 指定Android工程里raw目录中的铃声文件名，不需要后缀名
        /// </summary>
        public string RingRaw { get; set; }

        /// <summary>
        /// 消息在状态栏显示的图标，若不设置，则显示应用图标
        /// </summary>
        public string SmallIcon { get; set; }

        /// <summary>
        /// 设置是否覆盖指定编号的通知样式
        /// </summary>
        public int? StyleId { get; set; }

        /// <summary>
        /// 是否使用震动
        /// 1）0：没有震动
        /// 2）1：有震动
        /// </summary>
        public int? Vibrate { get; set; }
    }
}