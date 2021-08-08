using FreeCource.Web.Models;
using FreeCourse.Shared.Dtos;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace FreeCource.Web.Services.Interfaces
{
  interface IIdentityService
  {
    Task<Response<bool>> SignIn(SigninInput signinInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
  }
}
