using IR_tech_test.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IR_test_test.Services
{
  public interface IHomeService
  {
    Task<ICollection<OrderModel>> GetCumulativeOrdersAsync(double depth);
  }
}
