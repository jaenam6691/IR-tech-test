using FluentAssertions;
using IR_tech_test.Controllers;
using IR_tech_test.Service.Contracts;
using IR_tech_test.Service.Models;
using IR_tech_test.Service.Models.Api;
using IR_tech_test.Tests.Data;
using IR_tech_test.Tests.Services.MockServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace IR_tech_test.Tests.Controllers
{
  public class OrderBookControllerApiTests
  {
    public OrderBookControllerApiTests()
    {
      _sut = new OrderBookController(_Logger.Object, _OrderBookService.Object, _MemoryCache.Object);
    }

    private readonly Mock<ILogger<OrderBookController>> _Logger = new Mock<ILogger<OrderBookController>>();
    private readonly Mock<IOrderBookService> _OrderBookService = new Mock<IOrderBookService>();
    private readonly Mock<IOrderBookCacheService> _MemoryCache = new Mock<IOrderBookCacheService>();
    private readonly OrderBookController _sut;

    [Fact]
    public async Task Get_ShouldReturnValidResult_WhenContentExists()
    {
      _MemoryCache.Setup(m => m.Get()).ReturnsAsync(OrderTestData.GetOrderbookDto);
      _OrderBookService.Setup(m => m.GetCumulativeOrders(It.IsAny<OrderBookDto>(), It.IsAny<double>()))
        .Returns(OrderTestData.GetCumulativeBidOrders);
      var response = await _sut.Get(0) as OkObjectResult;

      response.StatusCode.Should().Be(200);
      response.Value.Should().NotBeNull();
      response.Value.Should().BeOfType(typeof(List<OrderModel>));
      (response.Value as List<OrderModel>).Count.Should().Equals(OrderTestData.GetCumulativeBidOrders.Count);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenNoContentExists()
    {
      _MemoryCache.Setup(m => m.Get()).ReturnsAsync(OrderTestData.GetOrderbookDto);
      _OrderBookService.Setup(m => m.GetCumulativeOrders(It.IsAny<OrderBookDto>(), It.IsAny<double>()))
        .Returns(new List<OrderModel>());

      var response = await _sut.Get(0);
      var statusCodeResult = (IStatusCodeActionResult)response;

      response.Should().NotBeNull();
      statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
  }
}
