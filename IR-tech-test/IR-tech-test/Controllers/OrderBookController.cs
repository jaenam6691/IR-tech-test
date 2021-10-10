using System.Threading.Tasks;
using IR_tech_test.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IR_tech_test.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OrderBookController : ControllerBase
  {
    private readonly ILogger<OrderBookController> _logger;
    private readonly IOrderBookService _orderBookService;

    public OrderBookController(
      ILogger<OrderBookController> logger,
      IOrderBookService orderBookService
      )
    {
      _logger = logger;
      _orderBookService = orderBookService;
    }

    [HttpGet("{depth:int:min(0)}")]
    public async Task<IActionResult> Get(int depth)
    {
      var test = await _orderBookService.Get(depth);
      return Ok("Hello world");
    }
  }
}
