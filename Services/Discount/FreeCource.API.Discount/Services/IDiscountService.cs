using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCource.API.Discount.Services
{
  public interface IDiscountService
  {
    Task<IList<Models.Discount>> GetAll();
    Task<Models.Discount> GetById(int id);
    Task<bool> Save(Models.Discount discount);
    Task<bool> Update(Models.Discount discount);
    Task<bool> Delete(int id);
    Task<Models.Discount> GetByCodeAndUserId(string code, string userId);
  }
}
