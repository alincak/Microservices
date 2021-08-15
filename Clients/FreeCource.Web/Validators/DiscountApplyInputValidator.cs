using FluentValidation;
using FreeCource.Web.Models.Discounts;

namespace FreeCource.Web.Validators
{
  public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
  {
    public DiscountApplyInputValidator()
    {
      RuleFor(x => x.Code).NotEmpty().WithMessage("indirim kupon alanı boş olamaz");
    }
  }
}
