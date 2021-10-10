using IR_tech_test.Service.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IR_tech_test.Service.Services
{
  public class CacheService<T> : ICacheService<T>
  {
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache memoryCache)
    {
      _cache = memoryCache;
    }

    public T Get(string key)
    {
      var cacheResponse = _cache.Get<T>(key);
      return cacheResponse;
    }

    public void Set(string key, T values)
    {
      //Set expiry slightly longer to account for delays with BackgroundTaskService.cs
      _cache.Set(key, values, TimeSpan.FromSeconds(16));
    }
  }
}
