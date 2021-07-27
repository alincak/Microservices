using FreeCource.API.Order.Application.Dtos;
using MediatR;
using System.Collections.Generic;

namespace FreeCource.API.Order.Application.Commands
{
  public class CreateOrderCommand : IRequest<CreatedOrderDto>
  {
    public string BuyerId { get; set; }
    public IList<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }
  }
}
