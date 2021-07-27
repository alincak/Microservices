using System;
using System.Collections.Generic;

namespace FreeCource.API.Order.Application.Dtos
{
  public class OrderDto
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; private set; }
    public AddressDto Address { get; private set; }
    public string BuyerId { get; private set; }
    public IList<OrderItemDto> OrderItems { get; private set; }
  }
}
