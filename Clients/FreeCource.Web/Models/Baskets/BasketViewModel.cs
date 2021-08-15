using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCource.Web.Models.Baskets
{
  public class BasketViewModel
  {
    public BasketViewModel()
    {
      _items = new List<BasketItemViewModel>();
    }

    public string UserId { get; set; }

    public string DiscountCode { get; set; }

    public int? DiscountRate { get; set; }
    private List<BasketItemViewModel> _items;

    public List<BasketItemViewModel> Items
    {
      get
      {
        if (HasDiscount)
        {
          //Örnek kurs fiyat 100 TL indirim %10
          _items.ForEach(x =>
          {
            var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
            x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
          });
        }
        return _items;
      }
      set
      {
        _items = value;
      }
    }

    public decimal TotalPrice
    {
      get => _items.Sum(x => x.GetCurrentPrice);
    }

    public bool HasDiscount
    {
      get => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue;
    }

    public void CancelDiscount()
    {
      DiscountCode = null;
      DiscountRate = null;
    }

    public void ApplyDiscount(string code, int rate)
    {
      DiscountCode = code;
      DiscountRate = rate;
    }
  }
}
