using FreeCource.API.Basket.Dtos;
using System.Threading.Tasks;

namespace FreeCource.API.Basket.Services
{
  public interface IBasketService
  {
    Task<BasketDto> Get(string userId);
    Task<bool> SaveOrUpdate(BasketDto basketDto);
    Task<bool> Delete(string userId);
  }
}
