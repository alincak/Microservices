using FluentValidation.AspNetCore;
using FreeCource.Web.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace FreeCource.Web.Extensions
{
  public static class ValidatorExtension
  {
    public static IMvcBuilder AddFluentValidationServices(this IMvcBuilder mvcBuilder)
    {
      return mvcBuilder
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CourseCreateInputValidator>())
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CourseUpdateInputValidator>());
    }
  }
}
