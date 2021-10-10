using IR_tech_test.Service.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IR_tech_test.Service.Services
{
  /// <summary>
  /// Majority of code taken from: https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-5.0
  /// </summary>
  public class BackgroundTaskService : IHostedService, IDisposable
  {
    private int executionCount = 0;
    private readonly ILogger<BackgroundTaskService> _logger;
    private Timer _timer;
    private readonly IOrderBookService _orderBookService;
    private readonly IMemoryCache _memoryCache;

    public BackgroundTaskService(
      ILogger<BackgroundTaskService> logger,
      IOrderBookService orderBookService,
      IMemoryCache memoryCache
      )
    {
      _logger = logger;
      _orderBookService = orderBookService;
      _memoryCache = memoryCache;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service running.");

      _timer = new Timer(DoWork, null, TimeSpan.Zero,
          TimeSpan.FromSeconds(15));

      return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
      var count = Interlocked.Increment(ref executionCount);

      _logger.LogInformation(
          "Timed Hosted Service is working. Count: {Count}", count);

      var orderBook = Task.Run(() => _orderBookService.GetAsync()).Result;
      _memoryCache.Set("orderBook", orderBook);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}
