using IR_tech_test.Service.Models;
using IR_tech_test.Service.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace IR_tech_test.Tests.Data
{
  public static class OrderTestData
  {
    public static ICollection<OrderModel> GetCumulativeBidOrders => new List<OrderModel>
    {
      new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitBid,
          Price = 1,
          Volume = 0.04
        },
        new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitBid,
          Price = 1.1,
          Volume = 0.03
        },
        new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitBid,
          Price = 100,
          Volume = 0.0445
        },
        new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitBid,
          Price = 41,
          Volume = 0.12341
        },
    };

    public static ICollection<OrderModel> GetCumulativeOfferOrders => new List<OrderModel>
    {
      new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitOffer,
          Price = 12,
          Volume = 62.04
        },
        new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitOffer,
          Price = 14.1,
          Volume = 1.03
        },
        new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitOffer,
          Price = 110,
          Volume = 3.0445
        },
        new OrderModel
        {
          OrderType = Enums.OrderTypeEnum.LimitOffer,
          Price = 1,
          Volume = 123
        },
    };

    public static OrderBookDto GetOrderbookDto => new OrderBookDto
    {
      BuyOrders = GetCumulativeBidOrders, 
      SellOrders = GetCumulativeOfferOrders
    };
  }
}
