using System.Collections.Generic;
using System.Linq;
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
    private readonly IOrderBookCacheService _memoryCache;
    public OrderBookController(
      ILogger<OrderBookController> logger,
      IOrderBookService orderBookService,
      IOrderBookCacheService memoryCache
      )
    {
      _logger = logger;
      _orderBookService = orderBookService;
      _memoryCache = memoryCache;
    }

    [HttpGet("{depth:double:min(0)}")]
    public async Task<IActionResult> Get(double depth)
    {
      var orders = await _memoryCache.Get();

      var cumulativeOrders = _orderBookService.GetCumulativeOrders(orders, depth);

      if (cumulativeOrders == null || !cumulativeOrders.Any())
        return NotFound();

      return Ok(cumulativeOrders);
    }
  }
}
