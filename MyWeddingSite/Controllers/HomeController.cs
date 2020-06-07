using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWeddingSite.Models;
using StackExchange.Redis;

namespace MyWeddingSite.Controllers
{
    public class HomeController : Controller
    {
        private static int _count;
        private ILogger<HomeController> _iLogger;

        private IDatabase _db;
        public HomeController(ILoggerFactory factory, IConnectionMultiplexer redisClient)
        {
            this._iLogger = factory.CreateLogger<HomeController>();

            this._db = redisClient.GetDatabase();
        }

        public IActionResult Index()
        {
            _iLogger.LogInformation($"IP:{base.HttpContext.Request.Host.Value}  于{DateTime.Now}  第{++_count}次访问");
            return View();
        }

        private IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        private IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Photo()
        {
            return View();
        }

        private IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
