using Microsoft.AspNetCore.Mvc;

namespace Microsevice2.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public string Index()
        {
            return "String from Microsrvice 2";
        }
    }
}
