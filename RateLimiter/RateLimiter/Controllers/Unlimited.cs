using Microsoft.AspNetCore.Mvc;

namespace RateLimiter.Controllers
{
    public class Unlimited : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(nameof(Unlimited))]
        public string Get()
        {
            return "Unlimited!";
        }
    }
}
