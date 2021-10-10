using FluentAssertions;
using IR_tech_test.Controllers;
using IR_tech_test.Service.Models;
using IR_tech_test.Tests.Data;
using IR_test_test.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace IR_tech_test.Tests.Controllers
{
  public class HomeControllerTests
  {
    public HomeControllerTests()
    {
      _sut = new HomeController(_Logger.Object, _HomeService.Object);
    }

    private readonly HomeController _sut;
    private readonly Mock<ILogger<HomeController>> _Logger = new Mock<ILogger<HomeController>>();
    private readonly Mock<IHomeService> _HomeService = new Mock<IHomeService>();


    [Fact]
    public async Task Index_ShouldReturnValidResult_WhenContentExists()
    {
      var testData = new List<OrderModel>();
      testData.AddRange(OrderTestData.GetCumulativeBidOrders);
      testData.AddRange(OrderTestData.GetCumulativeOfferOrders);

      _HomeService.Setup(m => m.GetCumulativeOrdersAsync(It.IsAny<double>()))
        .ReturnsAsync(testData);

      var response = (ViewResult) await _sut.Index(new Models.IndexView());
      
      response.Model.Should().NotBeNull();
      response.Model.Should().BeOfType(typeof(Models.IndexView));

      (response.Model as Models.IndexView).BuyOrders.Count
        .Should().Equals(OrderTestData.GetCumulativeBidOrders.Count);
      (response.Model as Models.IndexView).SellOrders.Count
        .Should().Equals(OrderTestData.GetCumulativeOfferOrders.Count);
    }

    [Fact]
    public async Task Index_ShouldReturnValidResult_WhenContentNull()
    {
      _HomeService.Setup(m => m.GetCumulativeOrdersAsync(It.IsAny<double>()))
        .ReturnsAsync((List<OrderModel>)null);

      var response = (ViewResult)await _sut.Index(new Models.IndexView());

      response.Model.Should().NotBeNull();
      response.Model.Should().BeOfType(typeof(Models.IndexView));

      (response.Model as Models.IndexView).BuyOrders.Should().BeNullOrEmpty();
      (response.Model as Models.IndexView).SellOrders.Should().BeNullOrEmpty();
    }
  }
}
