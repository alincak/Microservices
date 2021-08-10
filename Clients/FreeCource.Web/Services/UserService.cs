using FreeCource.Web.Models;
using FreeCource.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCource.Web.Services
{
  public class UserService : IUserService
  {
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
      _client = client;
    }

    public async Task<UserViewModel> GetUser()
    {
      return await _client.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
    }
  }
}
