using Microsoft.Extensions.Hosting;
using System;

namespace JaneGenericHostExample.Services
{
    public class TestService : ITestService
    {
        private readonly IHostEnvironment _hostEnvironment;

        public TestService(
            IHostEnvironment hostEnvironment
            )
        {
            _hostEnvironment = hostEnvironment;
        }

        public string GetRandomString()
        {
            if (_hostEnvironment.IsDevelopment())
            {
                return "dev" + Guid.NewGuid().ToString();
            }
            else
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}