using FreeCource.Web.Models.FakePayments;
using FreeCource.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCource.Web.Services
{
  public class PaymentService : IPaymentService
  {
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
    {
      var response = await _httpClient.PostAsJsonAsync("fakepayments", paymentInfoInput);

      return response.IsSuccessStatusCode;
    }
  }
}
