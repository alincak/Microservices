using System.ComponentModel.DataAnnotations;

namespace FreeCource.Web.Models.Catalogs
{
  public class FeatureViewModel
  {
    [Display(Name = "Kurs süre")]
    public int Duration { get; set; }
  }
}
