using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
//using System.Text.Json.Serialization;


namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
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

        [HttpGet]
        public string Get()
        {
            var rng = new Random();

            IEnumerable<WeatherForecast> Numeros = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            var respuesta = new GenResponse<IEnumerable<WeatherForecast>>();
            respuesta.DataResponse = Numeros;
            respuesta.Success = true;

            var lista = new List<MessagesResponseList>();
            lista.Add(new MessagesResponseList
            {
                Message = "Mensaje 1",
                MesageID = 2
            });

            respuesta.Messages = lista;

            string JsonResponse;
            //JsonSerializer js = new JsonSerializer();
            JsonResponse = JsonConvert.SerializeObject(respuesta);

            return JsonResponse;
        }

        [HttpPost]
        public string CreateWH(WeatherForecast item)
        {
            var respuesta = new GenResponse<int>();
            respuesta.DataResponse = 0;
            //var json = new JsonConvert().SerializeObject(respuesta);

            string jsonString;
            //JsonSerializer js = new JsonSerializer();
            jsonString = JsonConvert.SerializeObject(respuesta);

            return jsonString;
        }
    }
}
