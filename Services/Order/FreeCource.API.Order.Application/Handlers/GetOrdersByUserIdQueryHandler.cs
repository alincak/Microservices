using FreeCource.API.Order.Application.Dtos;
using FreeCource.API.Order.Application.Mapping;
using FreeCource.API.Order.Application.Queries;
using FreeCource.API.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCource.API.Order.Application.Handlers
{
  public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, IList<OrderDto>>
  {
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdQueryHandler(OrderDbContext context)
    {
      _context = context;
    }

    public async Task<IList<OrderDto>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
      var orders = await _context.Orders.Include(x => x.OrderItems)
        .Where(x => x.BuyerId == request.UserId)
        .ToListAsync();

      return ObjectMapper.Mapper.Map<IList<OrderDto>>(orders);
    }
  }
}
