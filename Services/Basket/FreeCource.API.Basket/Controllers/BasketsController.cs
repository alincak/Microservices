using FreeCource.API.Basket.Dtos;
using FreeCource.API.Basket.Services;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCource.API.Basket.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BasketsController : CustomBaseController
  {
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
      _basketService = basketService;
      _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var basket = await _basketService.Get(_sharedIdentityService.GetUserId);
      if (basket == null)
      {
        return CreateResponse(Response<BasketDto>.Fail("Basket not found.", 404));
      }

      return CreateResponse(Response<BasketDto>.Success(basket, 200));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BasketDto basketDto)
    {
      basketDto.UserId = _sharedIdentityService.GetUserId;
      var status = await _basketService.SaveOrUpdate(basketDto);

      return status
        ? CreateResponse(Response<bool>.Success(204))
        : CreateResponse(Response<bool>.Fail("Basket could not update or save", 500));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
      var status = await _basketService.Delete(_sharedIdentityService.GetUserId);

      return status
        ? CreateResponse(Response<bool>.Success(204))
        : CreateResponse(Response<bool>.Fail("Basket not found", 404));
    }

  }
}
