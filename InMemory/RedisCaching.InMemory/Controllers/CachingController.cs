using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RedisCaching.InMemory.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CachingController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("{value}")]
        public void Set(string value)
        {
            _memoryCache.Set("name", value);
        }

        [HttpGet]
        public string Get()
        {
            var res = _memoryCache.TryGetValue<string>("name", out var value);
            if (res)
            {
                return value;
            }
            else
            {
                throw new ArgumentNullException(value);
            }
        }
        
        [HttpGet]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(15),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }

    }
}
