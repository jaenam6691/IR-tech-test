using IR_tech_test.Service.Models;
using System.Collections.Generic;

namespace IR_tech_test.Models
{
  public class IndexView
  {
    public double Depth { get; set; }

    public ICollection<OrderModel> BuyOrders { get; set; } = new List<OrderModel>();
    public ICollection<OrderModel> SellOrders { get; set; } = new List<OrderModel>();
  }
}
