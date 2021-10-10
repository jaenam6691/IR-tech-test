using Newtonsoft.Json;
using System.Collections.Generic;

namespace IR_tech_test.Service.Models.Api
{
  [JsonObject]
  public class OrderBookDto
  {
    public ICollection<OrderModel> BuyOrders { get; set; } = new List<OrderModel>();
  }
}
