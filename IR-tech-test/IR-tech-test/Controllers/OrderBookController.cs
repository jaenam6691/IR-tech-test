using System.Collections.Generic;
using System.Threading.Tasks;
using IR_tech_test.Service.Contracts;
using IR_tech_test.Service.Models;
using IR_tech_test.Service.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace IR_tech_test.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OrderBookController : ControllerBase
  {
    private readonly ILogger<OrderBookController> _logger;
    private readonly IOrderBookService _orderBookService;
    private readonly IMemoryCache _memoryCache;
    public OrderBookController(
      ILogger<OrderBookController> logger,
      IOrderBookService orderBookService,
      IMemoryCache memoryCache
      )
    {
      _logger = logger;
      _orderBookService = orderBookService;
      _memoryCache = memoryCache;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var orders = _memoryCache.Get<OrderBookDto>("orderBook");

      var allOrders = new List<OrderModel>();
      allOrders.AddRange(orders.BuyOrders);
      allOrders.AddRange(orders.SellOrders);

      return Ok(orders);
    }

    [HttpGet("{depth:double:min(0)}")]
    public async Task<IActionResult> GetCumulativeOrders(double depth)
    {
      var orders = _memoryCache.Get<OrderBookDto>("orderBook");
      if (orders == null)
      {
        orders = await _orderBookService.GetAsync();

        // Issue with the API or data D.N.E - return 404
        if (orders == null)
          return NotFound();

        _memoryCache.Set("orderBook", orders);
      }

      var cumulativeOrders = _orderBookService.GetCumulativeOrders(orders, depth);

      return Ok(cumulativeOrders);
    }
  }
}
