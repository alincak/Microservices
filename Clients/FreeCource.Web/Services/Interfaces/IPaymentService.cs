using FreeCource.Web.Models.FakePayments;
using System.Threading.Tasks;

namespace FreeCource.Web.Services.Interfaces
{
  public interface IPaymentService
  {
    Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
  }
}
