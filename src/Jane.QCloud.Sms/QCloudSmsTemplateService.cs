using System.Collections.Generic;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsTemplateService : IQCloudSmsTemplateService
    {
        private readonly Dictionary<string, int> _templateCodePairs;

        public QCloudSmsTemplateService(Dictionary<string, int> templateCodePairs)
        {
            _templateCodePairs = templateCodePairs;
        }

        public int GetTemplateId(string code)
        {
            if (_templateCodePairs.ContainsKey(code))
            {
                return _templateCodePairs[code];
            }
            else
            {
                throw new KeyNotFoundException($"sms code[{code}] not found.");
            }
        }
    }
}