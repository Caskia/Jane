using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace JaneENodeWebHostExample.Services
{
    public class TestService : ITestService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public TestService(
            IWebHostEnvironment hostEnvironment
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