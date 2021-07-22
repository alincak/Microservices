using FreeCourse.API.Catalog.Dtos;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.API.Catalog.Services
{
  public interface ICourseService
  {
    Task<Response<IList<CourseDto>>> GetAllAsync();
    Task<Response<CourseDto>> CreateAsync(CourseCreateDto course);
    Task<Response<NoContent>> UpdateAsync(CourseUpdateDto course);
    Task<Response<NoContent>> DeleteAsync(string id);
    Task<Response<CourseDto>> GetByIdAsync(string id);
    Task<Response<IList<CourseDto>>> GetAllByUserIdAsync(string id);
  }
}
