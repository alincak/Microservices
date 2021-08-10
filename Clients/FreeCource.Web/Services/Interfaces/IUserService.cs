using FreeCource.Web.Models;
using System.Threading.Tasks;

namespace FreeCource.Web.Services.Interfaces
{
  public interface IUserService
  {
    Task<UserViewModel> GetUser();
  }
}
