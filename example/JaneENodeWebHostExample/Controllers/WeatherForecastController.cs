using ENode.Commanding;
using JaneENodeWebHostExample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaneENodeWebHostExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ICommandService _commandService;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITestService _testService;

        public WeatherForecastController(
            ICommandService commandService,
            ILogger<WeatherForecastController> logger,
            ITestService testService
            )
        {
            _commandService = commandService;
            _logger = logger;
            _testService = testService;
        }

        [HttpGet]
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