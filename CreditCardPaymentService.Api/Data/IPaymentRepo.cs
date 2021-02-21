using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.Data
{
    public interface IPaymentRepo
    {
        void CreatePayment(Payment payment);
        void CreatePaymentState(PaymentState paymentState);
    }
}