using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Validations
{
  public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
  {
    private readonly UserManager<ApplicationUser> _userManager;

    public ResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
      var existUser = await _userManager.FindByEmailAsync(context.UserName);
      if (existUser == null)
      {
        var errors = new Dictionary<string, object>
        {
          { "errors", new List<string> { "Email veya şifre yanlış." } }
        };

        context.Result.CustomResponse = errors;

        return;
      }

      var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
      if (!passwordCheck)
      {
        var errors = new Dictionary<string, object>
        {
          { "errors", new List<string> { "Email veya şifre yanlış." } }
        };

        context.Result.CustomResponse = errors;

        return;
      }

      context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
    }
  }
}
