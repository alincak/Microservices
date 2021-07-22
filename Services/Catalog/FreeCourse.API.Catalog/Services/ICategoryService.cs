using FreeCourse.API.Catalog.Dtos;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.API.Catalog.Services
{
  public interface ICategoryService
  {
    Task<Response<IList<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
  }
}
