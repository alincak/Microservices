using FreeCourse.API.Catalog.Dtos;
using FreeCourse.API.Catalog.Services;
using FreeCourse.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.API.Catalog.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesController : CustomBaseController
  {
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var response = await _categoryService.GetAllAsync();

      return CreateResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
      var response = await _categoryService.GetByIdAsync(id);

      return CreateResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto category)
    {
      var response = await _categoryService.CreateAsync(category);

      return CreateResponse(response);
    }

  }
}
