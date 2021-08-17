using FreeCource.API.Order.Infrastructure;
using FreeCourse.Shared.Messages;
using MassTransit;
using System.Threading.Tasks;

namespace FreeCource.API.Order.Application.Consumers
{
  public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
  {
    private readonly OrderDbContext _orderDbContext;

    public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
    {
      _orderDbContext = orderDbContext;
    }

    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
      var newAddress = new Domain.OrderAggregate.Address(context.Message.Province, context.Message.District, context.Message.Street, context.Message.ZipCode, context.Message.Line);

      var order = new Domain.OrderAggregate.Order(newAddress, context.Message.BuyerId);

      foreach (var item in context.Message.OrderItems)
      {
        order.AddOrderItem(item.ProductId, item.ProductName, item.PictureUrl, item.Price);
      }

      await _orderDbContext.Orders.AddAsync(order);
      await _orderDbContext.SaveChangesAsync();
    }
  }
}
