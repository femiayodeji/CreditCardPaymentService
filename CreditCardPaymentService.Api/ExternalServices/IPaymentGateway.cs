using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.ExternalServices
{
    public interface IPaymentGateway 
    {
        PaymentState Charge(Payment payment);
    }

    public interface ICheapPaymentGateway : IPaymentGateway
    {
    }
    public interface IExpensivePaymentGateway : IPaymentGateway
    {
    }
}