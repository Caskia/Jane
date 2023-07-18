using System;
using Jane.Runtime.Caching;

namespace Jane.Captcha
{
    public interface ICaptchaFactory
    {
        void AddService(string name, ICaptchaService service);

        ICaptchaService GetService(string name);

        void RemoveService(string name);
    }
}

