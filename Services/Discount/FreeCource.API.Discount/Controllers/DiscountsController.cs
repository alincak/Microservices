using FreeCource.API.Discount.Services;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCource.API.Discount.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DiscountsController : CustomBaseController
  {
    private readonly IDiscountService _discountService;

    private readonly ISharedIdentityService _sharedIdentityService;

    public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
    {
      _discountService = discountService;
      _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var list = await _discountService.GetAll();
      return CreateResponse(Response<IList<Models.Discount>>.Success(list, 200));
    }

    //api/discounts/4
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var discount = await _discountService.GetById(id);
      if (discount == null)
      {
        return CreateResponse(Response<Models.Discount>.Fail("Discount not found", 404));
      }

      return CreateResponse(Response<Models.Discount>.Success(discount, 200));
    }

    [HttpGet]
    [Route("/api/[controller]/[action]/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
      var userId = _sharedIdentityService.GetUserId;

      var discount = await _discountService.GetByCodeAndUserId(code, userId);
      if (discount == null)
      {
        return CreateResponse(Response<Models.Discount>.Fail("Discount not found", 404));
      }

      return CreateResponse(Response<Models.Discount>.Success(discount, 200));
    }

    [HttpPost]
    public async Task<IActionResult> Save(Models.Discount discount)
    {
      var result = await _discountService.Save(discount);

      return result
        ? CreateResponse(Response<NoContent>.Success(204))
        : CreateResponse(Response<NoContent>.Fail("an error occurred while adding", 500));
    }

    [HttpPut]
    public async Task<IActionResult> Update(Models.Discount discount)
    {
      var result = await _discountService.Update(discount);

      return result 
        ? CreateResponse(Response<NoContent>.Success(204))
        : CreateResponse(Response<NoContent>.Fail("Discount not found", 404));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var result = await _discountService.Delete(id);

      return result
        ? CreateResponse(Response<NoContent>.Success(204))
        : CreateResponse(Response<NoContent>.Fail("Discount not found", 404));
    }
  }
}
