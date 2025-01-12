using Microsoft.AspNetCore.Mvc;
using RateLimiter.Contracts;
using RateLimiter.RateControllers;
using System.Net;

namespace RateLimiter.Controllers
{
    public class Limited : Controller
    {
        IRateController bucket;

        public Limited(IRateController bucket)
        {
            this.bucket = bucket;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(nameof(Limited))]
        public IActionResult Get()
        {
            var ipAddr = this.Request.HttpContext.Connection.RemoteIpAddress;
            if (this.bucket.ValidateClient(ipAddr!))
            {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(429);
            }
        }
    }
}
