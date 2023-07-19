using Jane.AspNetCore.Mvc.Attributes;
using JaneWebHostExample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaneWebHostExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;
        private readonly ITestService _testService;

        public TestController(
            ILogger<TestController> logger,
            ITestService testService
            )
        {
            _logger = logger;
            _testService = testService;
        }

        [HttpGet]
        [Captcha]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Note = _testService.GetRandomString()
            })
            .ToArray();
        }
    }
}