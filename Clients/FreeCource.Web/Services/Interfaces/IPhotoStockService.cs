using FreeCource.Web.Models.PhotoStocks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FreeCource.Web.Services.Interfaces
{
  public interface IPhotoStockService
  {
    Task<PhotoViewModel> UploadPhoto(IFormFile photo);

    Task<bool> DeletePhoto(string photoUrl);
  }
}
