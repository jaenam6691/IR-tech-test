using IR_tech_test.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IR_tech_test.Service.Contracts
{
  public interface IOrderBookService
  {
    Task<ICollection<OrderModel>> Get(int depth);
  }
}
