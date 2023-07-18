using System;
using System.Collections.Generic;
using Jane.Dependency;

namespace Jane.Captcha
{
    public class CaptchaFactory : ICaptchaFactory
    {
        private Dictionary<string, ICaptchaService> _cache;

        public CaptchaFactory()
        {
            _cache = new Dictionary<string, ICaptchaService>(StringComparer.OrdinalIgnoreCase);
        }

        public void AddService(string name, ICaptchaService service)
        {
            if (_cache.ContainsKey(name))
            {
                return;
            }

            _cache.Add(name, service);
        }

        public ICaptchaService GetService(string name)
        {
            if (!_cache.ContainsKey(name))
            {
                throw new NotSupportedException($"not find service [{name}]");
            }

            return _cache[name];
        }

        public void RemoveService(string name)
        {
            if (!_cache.ContainsKey(name))
            {
                throw new NotSupportedException($"not find service [{name}]");
            }

            _cache.Remove(name);
        }
    }
}

