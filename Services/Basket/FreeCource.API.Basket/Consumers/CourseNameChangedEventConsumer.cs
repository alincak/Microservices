using FreeCource.API.Basket.Services;
using FreeCourse.Shared.Messages;
using MassTransit;
using System.Threading.Tasks;

namespace FreeCource.API.Basket.Consumers
{
  public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
  {
    private readonly IBasketService _basketService;

    public CourseNameChangedEventConsumer(IBasketService basketService)
    {
      _basketService = basketService;
    }

    public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
    {
      var basket = await _basketService.Get(context.Message.UserId);
      if (basket == null)
      {
        return;
      }

      foreach (var item in basket.Items)
      {
        if (item.CourseId == context.Message.CourseId)
        {
          item.CourseName = context.Message.UpdatedName;
        }
      }

      await _basketService.SaveOrUpdate(basket);
    }
  }
}
