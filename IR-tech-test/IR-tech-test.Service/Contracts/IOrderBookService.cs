using IR_tech_test.Service.Models;
using IR_tech_test.Service.Models.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IR_tech_test.Service.Contracts
{
  public interface IOrderBookService
  {
    Task<OrderBookDto> GetAsync();
    ICollection<OrderModel> GetCumulativeOrders(OrderBookDto orders, double depth);
  }
}
