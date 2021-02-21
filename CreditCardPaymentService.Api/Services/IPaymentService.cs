using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.Services
{
    public interface IPaymentService 
    {
        Payment ProcessPayment(Payment payment);
    }
}