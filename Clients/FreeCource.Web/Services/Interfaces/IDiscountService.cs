using FreeCource.Web.Models.Discounts;
using System.Threading.Tasks;

namespace FreeCource.Web.Services.Interfaces
{
  public interface IDiscountService
  {
    Task<DiscountViewModel> GetDiscount(string discountCode);
  }
}
