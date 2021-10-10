using IR_tech_test.Service.Contracts;
using IR_tech_test.Service.Models.Api;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace IR_tech_test.Service.Services
{
  public class OrderBookCacheService : IOrderBookCacheService
  {
    private readonly IMemoryCache _memoryCache;
    private readonly IOrderBookService _orderBookService;

    public OrderBookCacheService(
      IMemoryCache memoryCache,
      IOrderBookService orderBookService
      )
    {
      _memoryCache = memoryCache;
      _orderBookService = orderBookService;
    }

    public async Task<OrderBookDto> Get()
    {
      var orders = _memoryCache.Get<OrderBookDto>("orderBook");
      if (orders == null)
      {
        orders = await _orderBookService.GetAsync();

        // Issue with the API or data D.N.E
        if (orders == null)
          throw new Exception("Does not exist or API is down");

        _memoryCache.Set("orderBook", orders);
      }

      return orders;
    }
  }
}
