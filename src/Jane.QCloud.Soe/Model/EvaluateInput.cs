namespace Jane.QCloud.Soe
{
    public class EvaluateInput
    {
        /// <summary>
        /// 评估模式（0：词模式, 1：句子模式, 2：段落模式，3：自由说模式)
        /// </summary>
        public int EvalMode { get; set; }

        /// <summary>
        /// 评估语音文本
        /// </summary>
        public string RefText { get; set; }

        /// <summary>
        /// 评价苛刻指数（取值为[1.0 - 4.0]，1.0为小年龄段，4.0为最高年龄段）
        /// </summary>
        public float? ScoreCoeff { get; set; }

        /// <summary>
        /// 语音数据（BASE64编码）
        /// </summary>
        public string VoiceData { get; set; }

        /// <summary>
        /// 语音文件类型（1:raw, 2:wav, 3:mp3）
        /// </summary>
        public int VoiceFileType { get; set; }
    }
}