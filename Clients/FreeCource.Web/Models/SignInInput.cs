using System.ComponentModel.DataAnnotations;

namespace FreeCource.Web.Models
{
  public class SigninInput
  {
    [Required]
    [Display(Name = "Email adresiniz")]
    public string Email { get; set; }
    [Display(Name = "Şifreniz")]
    public string Password { get; set; }
    [Display(Name = "Beni hatırla")]
    public bool IsRemember { get; set; }
  }
}
