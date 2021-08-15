using System.Collections.Generic;
using System.Linq;

namespace FreeCource.API.Basket.Dtos
{
  public class BasketDto
  {
    public string UserId { get; set; }
    public string DiscountCode { get; set; }
    public int? DiscountRate { get; set; }
    public IList<BasketItemDto> Items { get; set; }
    
    public decimal TotalPrice
    {
      get => Items.Sum(x => x.Price * x.Quantity);
    }
  }
}
