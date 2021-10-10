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

    public Task<T> CacheTryGetValueSet(string key, T entries)
    {
      throw new NotImplementedException();
    }

    //public async Task<T> CacheTryGetValueSet(string key, T entries)
    //{
    //  var cacheEntry = await
    //    _cache.GetOrCreateAsync(key, entry =>
    //    {

    //    });

    //  return cacheEntry;
    //}
  }
}
