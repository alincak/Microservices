using System.Threading.Tasks;

namespace FreeCource.Web.Services.Interfaces
{
  public interface IClientCredentialTokenService
  {
    Task<string> GetToken();
  }
}
