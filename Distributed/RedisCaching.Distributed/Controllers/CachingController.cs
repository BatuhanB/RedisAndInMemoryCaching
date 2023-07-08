using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCaching.Distributed.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CachingController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public CachingController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("{key}/{value}")]
        public async Task<IActionResult> Set(string key,string value)
        {
            await _distributedCache.SetStringAsync(key,value,options:new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(15),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            return Ok();
        }


        [HttpGet("{key}")]
        public async Task<string> Get(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }
    }
}
