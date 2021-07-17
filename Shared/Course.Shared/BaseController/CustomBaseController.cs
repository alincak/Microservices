using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Shared.BaseController
{
  public class CustomBaseController : ControllerBase
  {
    public IActionResult CreateResponse<T>(Response<T> response)
    {
      return new ObjectResult(response)
      {
        StatusCode = response.StatusCode
      };
    }
  }
}
