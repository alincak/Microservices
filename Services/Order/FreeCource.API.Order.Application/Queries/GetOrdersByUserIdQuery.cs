using FreeCource.API.Order.Application.Dtos;
using MediatR;
using System.Collections.Generic;

namespace FreeCource.API.Order.Application.Queries
{
  public class GetOrdersByUserIdQuery : IRequest<IList<OrderDto>>
  {
    public string UserId { get; set; }
  }
}
