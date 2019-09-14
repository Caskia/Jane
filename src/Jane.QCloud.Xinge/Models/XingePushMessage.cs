using System.Collections.Generic;

namespace Jane.QCloud.Xinge
{
    public class XingePushMessage
    {
        /// <summary>
        /// 推送目标
        /// 1）all：全量推送
        /// 2）tag：标签推送
        /// 3）token：单设备推送
        /// 4）token_list：设备列表推送
        /// 5）account：单账号推送
        /// 6）account_list：账号列表推送
        /// </summary>
        public string AudienceType { get; set; }

        /// <summary>
        /// 用户指定推送环境，仅限iOS平台推送使用
        /// 1）product： 推送生产环境
        /// 2）dev： 推送开发环境
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public object Message { get; set; }

        /// <summary>
        /// 消息类型
        /// 1）notify：通知
        /// 2）message：透传消息/静默消息
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 客户端平台类型
        /// 1）android：安卓
        /// 2）ios：苹果
        /// 3）all：安卓&&苹果，仅支持全量推送和标签推送（预留参数，暂不可用）
        /// </summary>
        public string Platform { get; set; }

        #region Audience

        /// <summary>
        /// 账号列表群推
        /// </summary>
        public List<string> AccountList { get; set; }

        /// <summary>
        /// 标签列表推送
        /// </summary>
        public List<string> TagList { get; set; }

        /// <summary>
        /// 设备列表群推
        /// </summary>
        public List<string> TokenList { get; set; }

        #endregion Audience
    }
}