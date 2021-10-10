using IR_tech_test.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IR_test_test.Services
{
  public class HomeService : IHomeService
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HomeService> _logger;

    public HomeService(
      IHttpClientFactory client,
      ILogger<HomeService> logger)
    {
      _httpClientFactory = client;
      _logger = logger;
    }

    public async Task<ICollection<OrderModel>> GetCumulativeOrdersAsync(double depth)
    {
      var baseUrl = "https://localhost:44363/api/orderbook";

      var client = _httpClientFactory.CreateClient();
      var response = await client.GetAsync($"{baseUrl}/{depth}");

      if (!response.IsSuccessStatusCode)
      {
        _logger.LogError($"Error retriving Order Book with a statusCode={response.StatusCode}");
        return null;
      }

      try
      {
        var orderBook = await response.Content.ReadAsAsync<ICollection<OrderModel>>();
        return orderBook;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error deserializing.");
        return null;
      }
    }
  }
}
