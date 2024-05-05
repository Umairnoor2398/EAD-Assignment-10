using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace dotnetapigateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GatewayController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Available API endpoints for Dotnet Service are:
        /// hello
        /// callservice2
        /// </summary>
        [HttpGet("dotnetservice/{*path}")]
        public async Task<IActionResult> CallDotnetService(string path)
        {
            return await CallMicroservice("dotnetservice", path);
        }

        /// <summary>
        /// Available API endpoints for Node JS Service are:
        /// hello
        /// </summary>
        [HttpGet("nodeservice/{*path}")]
        public async Task<IActionResult> CallNodeService(string path)
        {
            return await CallMicroservice("nodeservice", path);
        }

        /// <summary>
        /// Available API endpoints for Django Service are:
        /// hello
        /// third-party
        /// third-party/{count}
        /// </summary>
        [HttpGet("djangoservice/{*path}")]
        public async Task<IActionResult> CallDjangoService(string path)
        {
            return await CallMicroservice("djangoservice", path);
        }

        private async Task<IActionResult> CallMicroservice(string baseUrlConfigKey, string path)
        {
            var baseUrl = _configuration.GetValue<string>("Microservices:" + baseUrlConfigKey);
            if (baseUrl == null)
            {
                return NotFound();
            }

            var requestPath = $"{baseUrl}/{path}";

            var response = await _httpClient.GetAsync(requestPath);

            if (response.IsSuccessStatusCode)
            {
                // Return the response from the microservice
                return Content(await response.Content.ReadAsStringAsync(), "application/json");
            }
            else
            {
                // Forward the microservice's error response to the client
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }
    }
}
