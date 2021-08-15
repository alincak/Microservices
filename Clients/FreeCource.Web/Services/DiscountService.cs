﻿using FreeCource.Web.Models.Discounts;
using FreeCource.Web.Services.Interfaces;
using FreeCourse.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCource.Web.Services
{
  public class DiscountService : IDiscountService
  {
    private readonly HttpClient _httpClient;

    public DiscountService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<DiscountViewModel> GetDiscount(string discountCode)
    {
      //[controller]/[action]/{code}

      var response = await _httpClient.GetAsync($"discounts/GetByCode/{discountCode}");

      if (!response.IsSuccessStatusCode)
      {
        return null;
      }

      var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();

      return discount.Data;
    }
  }
}
