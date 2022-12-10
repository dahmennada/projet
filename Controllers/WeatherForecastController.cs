using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet]
        [Route("Users")]
        public async Task <IActionResult> GetUsers()
        {
            return Ok("you have this");
        }

        [HttpGet]
        [Route("Users/{id}")]
        public async Task<IActionResult> GetUserId(int id)
        {
            return Ok(new { userID = id });
        }

        [Authorize]
        [HttpGet]
        [Route("Users/current")]
        public async Task<IActionResult> GetUserLogged()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));
            return Ok(new { userID = id });
        }

        [Authorize]
        [HttpGet]
        [Route("Users/currentSolde")]
        public async Task<IActionResult> GetUserSolde()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userSolde"));
            return Ok(new { userSolde = id });
        }

    }
}