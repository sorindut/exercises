using Microsoft.AspNetCore.Mvc;

namespace RateLimiter.Controllers
{
    public class Limited : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(nameof(Limited))]
        public string Get()
        {
            return "Limited!";
        }
    }
}
