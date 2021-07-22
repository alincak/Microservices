using FreeCourse.API.PhotoStock.Dtos;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.API.PhotoStock.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PhotosController : CustomBaseController
  {
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile photo, CancellationToken cancellationToken)
    {
      if (photo == null || photo.Length < 1)
      {
        return CreateResponse(Response<NoContent>.Fail("Fotoğraf boş.", 400));
      }

      var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
      using (var stream = new FileStream(path, FileMode.Create))
      {
        await photo.CopyToAsync(stream, cancellationToken);
      }

      var returnPath = "photos/" + photo.FileName;

      var photoDto = new PhotoDto() { Url = returnPath };

      return CreateResponse(Response<PhotoDto>.Success(photoDto, 200));
    }

    [HttpDelete]
    public IActionResult Delete(string photoUrl)
    {
      var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
      if (!System.IO.File.Exists(path))
      {
        return CreateResponse(Response<NoContent>.Fail("Fotoğraf bulunamadı.", 404));
      }

      System.IO.File.Delete(path);

      return CreateResponse(Response<NoContent>.Success(204));
    }

  }
}
