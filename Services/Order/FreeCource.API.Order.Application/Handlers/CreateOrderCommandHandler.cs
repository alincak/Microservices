using FreeCource.API.Order.Application.Commands;
using FreeCource.API.Order.Application.Dtos;
using FreeCource.API.Order.Domain.OrderAggregate;
using FreeCource.API.Order.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCource.API.Order.Application.Handlers
{
  public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
  {
    private readonly OrderDbContext _context;

    public CreateOrderCommandHandler(OrderDbContext context)
    {
      _context = context;
    }

    public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
      var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);

      Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(newAddress, request.BuyerId);

      foreach (var item in request.OrderItems)
      {
        newOrder.AddOrderItem(item.ProductId, item.ProductName, item.PictureUrl, item.Price);
      }

      await _context.Orders.AddAsync(newOrder);

      await _context.SaveChangesAsync();

      return new CreatedOrderDto { Id = newOrder.Id };
    }
  }
}
