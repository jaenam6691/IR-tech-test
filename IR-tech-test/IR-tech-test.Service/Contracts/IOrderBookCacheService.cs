using IR_tech_test.Service.Models.Api;
using System.Threading.Tasks;

namespace IR_tech_test.Service.Contracts
{
  public interface IOrderBookCacheService
  {
    Task<OrderBookDto> Get();
  }
}
