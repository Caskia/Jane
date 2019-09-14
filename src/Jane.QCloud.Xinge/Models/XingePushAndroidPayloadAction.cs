using System.Collections.Generic;

namespace Jane.QCloud.Xinge
{
    public class XingePushAndroidPayloadAction
    {
        /// <summary>
        /// 动作类型，1，打开activity或app本身；2，打开浏览器；3，打开Intent
        /// </summary>
        public int? ActionType { get; set; }

        /// <summary>
        /// 通知栏点击后打开的Activity
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// activity属性，只针对action_type=1的情况
        /// </summary>
        public Dictionary<string, string> AtyAttr { get; set; }

        public Dictionary<string, string> Browser { get; set; }

        public string Intent { get; set; }
    }
}