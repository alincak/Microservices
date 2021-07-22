using FreeCourse.API.Catalog.Dtos;
using FreeCourse.API.Catalog.Services;
using FreeCourse.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.API.Catalog.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CoursesController : CustomBaseController
  {
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
      _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var response = await _courseService.GetAllAsync();

      return CreateResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
      var response = await _courseService.GetByIdAsync(id);

      return CreateResponse(response);
    }

    [HttpGet]
    [Route("/api/[controller]/GetAllByUserId/{id}")]
    public async Task<IActionResult> GetAllByUserId(string id)
    {
      var response = await _courseService.GetAllByUserIdAsync(id);

      return CreateResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateDto course)
    {
      var response = await _courseService.CreateAsync(course);

      return CreateResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CourseUpdateDto course)
    {
      var response = await _courseService.UpdateAsync(course);

      return CreateResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var response = await _courseService.DeleteAsync(id);

      return CreateResponse(response);
    }

  }
}
