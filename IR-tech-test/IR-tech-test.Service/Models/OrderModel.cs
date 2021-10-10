using IR_tech_test.Enums;

namespace IR_tech_test.Service.Models
{
  public class OrderModel
  {
    public OrderTypeEnum OrderType { get; set; }
    public double Price { get; set; }
    public double Volume { get; set; }
  }
}
