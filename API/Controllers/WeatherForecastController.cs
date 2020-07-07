using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowEveryone")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var listofweatherforecast = new List<WeatherForecast>();

            ////    Console.WriteLine($"{c.Type}:{c.Value}");
            //var rng = new Random();
            //foreach (var c in User.Claims)
            //{
            //    listofweatherforecast.Add(new WeatherForecast
            //    {
            //        Date = DateTime.Now.AddDays(1),
            //        TemperatureC = rng.Next(-20, 55),
            //        Summary = $"{c.Type}:{c.Value}"
            //    });
            //}
            //return listofweatherforecast;

            if (User.Claims.FirstOrDefault(c => (c.Type == "http://schemas.microsoft.com/identity/claims/scope") && (c.Value == "weather.get")) == null)
            {
                throw new UnauthorizedAccessException();
            }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
