using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IR_tech_test.Models;
using IR_test_test.Services;

namespace IR_tech_test.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeService _homeService;

    public HomeController(
      ILogger<HomeController> logger,
      IHomeService homeService)
    {
      _logger = logger;
      _homeService = homeService;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IndexView model)
    {
      var orders = await _homeService.GetCumulativeOrders(model.Depth);
      return Ok(orders);
    }
  }
}
