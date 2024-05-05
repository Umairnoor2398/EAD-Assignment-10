using Microsoft.AspNetCore.Mvc;

namespace dotnetservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("/hello")]
        public IActionResult Get()
        {
            var responseData = new
            {
                Message = "Hello from Microservice 1!"
            };

            return Ok(responseData);
        }

        [HttpGet]
        [Route("/callservice2")]
        public async Task<IActionResult> Call2()
        {
            // Call Microservice 2
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:3000/hello");

            // Check if request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read response content
                string responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            else
            {
                // If request was not successful, return error status code
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
