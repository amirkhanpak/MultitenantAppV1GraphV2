using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AzureB2CAngularSPA.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public readonly IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccesor;
        public SampleDataController(IConfiguration configuration, IHttpContextAccessor httpContextAccesor)
        {
            _configuration = configuration;
            _httpContextAccesor = httpContextAccesor;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        [Authorize]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            //TODO:Aquire Token and pass it to the API
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "MarsUser")]
        //This is not called from the angular UI
        public async Task<ActionResult<string>> GetValues()
        {
            HttpClient client = new HttpClient();
            // var accessToken = ((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpRequestHeaders)Request.Headers).HeaderAuthorization;
            // var accessToken = Request.Headers["Authorization"];

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, error) =>
                {
                    return true;
                };

            var httpClientHandler = new HttpClientHandler()
            {
                UseDefaultCredentials = true,
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            client = new HttpClient(httpClientHandler);

            var accessToken = _httpContextAccesor.HttpContext.Request.Headers["Authorization"];

            var token = accessToken[0].Replace("Bearer ", "");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string s = "";
            // Call the To Do list service.
            HttpResponseMessage response = await client.GetAsync(_configuration.GetValue<string>("WebAPIURL") + "Values");

            if (response.IsSuccessStatusCode)
            {

                // Read the response and databind to the GridView to display To Do items.
                s = await response.Content.ReadAsStringAsync();

            }
            else
            {
                s = await response.Content.ReadAsStringAsync();
            }

            return s;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }



        [HttpGet("[action]")]
       
        public async Task<ActionResult<string>> GetWebTest()
        {
            HttpClient client = new HttpClient();
           // var accessToken = Request.Headers["Authorization"];
            var accessToken =_httpContextAccesor.HttpContext.Request.Headers["Authorization"];
            //var token = accessToken[0].Replace("Bearer ", "");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //string s = "";
            // Call the To Do list service.
            //HttpResponseMessage response = await client.GetAsync(_configuration.GetValue<string>("WebAPIURL") + "Values");

            //if (response.IsSuccessStatusCode)
            //{

            //    // Read the response and databind to the GridView to display To Do items.
            //    s = await response.Content.ReadAsStringAsync();

            //}
            //else
            //{
            //    s = await response.Content.ReadAsStringAsync();
            //}

            return accessToken.ToString();
        }

    }
}
