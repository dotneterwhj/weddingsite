using System;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace MyWeddingSite
{
    public class CounterViewComponent : ViewComponent
    {
        private IDatabase _redisDb;

        public CounterViewComponent(IConnectionMultiplexer redisClient)
        {
            _redisDb = redisClient.GetDatabase();
        }

        public IViewComponentResult Invoke()
        {
            var controller = base.RouteData.Values["controller"] as string;

            var action = base.RouteData.Values["action"] as string;

            var pageId = $"{controller}-{action}";

            if (!string.IsNullOrWhiteSpace(controller) && !string.IsNullOrWhiteSpace(action))
            {
                _redisDb.StringIncrement(pageId);

                var count = _redisDb.StringGet(pageId);

                return View("Default", count);
            }

            throw new Exception("wrong controller or action");
        }
    }
}