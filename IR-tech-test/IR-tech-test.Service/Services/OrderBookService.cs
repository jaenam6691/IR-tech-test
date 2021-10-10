using IR_tech_test.Enums;
using IR_tech_test.Service.Contracts;
using IR_tech_test.Service.Models;
using IR_tech_test.Service.Models.Api;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IR_tech_test.Service.Services
{
  public class OrderBookService : IOrderBookService
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<AppSettings> _settingsConfig;
    private readonly ILogger<OrderBookService> _logger;

    public OrderBookService(
      IHttpClientFactory client,
      IOptions<AppSettings> settings,
      ILogger<OrderBookService> logger)
    {
      _httpClientFactory = client;
      _settingsConfig = settings;
      _logger = logger;
    }

    public async Task<OrderBookDto> GetAsync()
    {
      var client = _httpClientFactory.CreateClient();

      var url = $"{_settingsConfig.Value.ApiBaseUrl}/Public/GetOrderBook?primaryCurrencyCode=xbt&secondaryCurrencyCode=aud";
      var response = await client.GetAsync(url);
      
      if (!response.IsSuccessStatusCode)
      {
        _logger.LogError($"Error retriving Order Book with a statusCode={response.StatusCode}");
        return null;
      }

      try
      {
        var orderBook = await response.Content.ReadAsAsync<OrderBookDto>();
        return orderBook;
      }
      catch (DeserializationException ex)
      {
        _logger.LogError(ex, "Error deserializing.");
        return null;
      }
    }

    public ICollection<OrderModel> GetCumulativeOrders(OrderBookDto orders, double depth)
    {
      var cumulativeOrders = new List<OrderModel>();

      cumulativeOrders.AddRange(CalculateCumulativeOrders(orders.BuyOrders, depth));
      cumulativeOrders.AddRange(CalculateCumulativeOrders(orders.SellOrders, depth));

      return cumulativeOrders;
    }

    private ICollection<OrderModel> CalculateCumulativeOrders(ICollection<OrderModel> orders, double depth)
    {
      var cumulativeOrders = new List<OrderModel>();
      foreach(var order in orders)
      {
        var value = order.Price * order.Volume;
        if (depth <= value)
          break;

        cumulativeOrders.Add(order);
        depth -= value;
      }

      return cumulativeOrders;
    }
  }
}
