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

    public async Task<ICollection<OrderModel>> Get(int depth)
    {
      var client = _httpClientFactory.CreateClient();

      var url = $"{_settingsConfig.Value.ApiBaseUrl}/Public/GetOrderBook?primaryCurrencyCode=xbt&secondaryCurrencyCode=aud";
      var response = await client.GetAsync(url);
      
      if (!response.IsSuccessStatusCode)
      {
        _logger.LogError($"Error retriving depth={depth} with a statusCode={response.StatusCode}");
        return null;
      }

      try
      {
        var orderBook = await response.Content.ReadAsAsync<OrderBookDto>();
        return orderBook.BuyOrders;
      }
      catch (DeserializationException ex)
      {
        _logger.LogError(ex, "Error deserializing.");
        return null;
      }
    }
  }
}
