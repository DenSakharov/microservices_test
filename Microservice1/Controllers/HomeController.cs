using Microsoft.AspNetCore.Mvc;

namespace Microsevice1.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var result = await client.GetAsync("https://localhost:5001/home");

            var content =await result.Content.ReadAsStringAsync();
            string resp = "received response from microservices " + content.ToString();
            return Ok(resp)
                ;
        }
    }
}
