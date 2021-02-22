using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.ExternalServices
{
    public interface IPaymentGateway 
    {
        PaymentState Charge(Payment payment);
        PaymentGatewayResponse Response { get; set; }
        Payment Request { get; set; }
        PaymentGatewayResponse GetResponse();
    }

    public interface ICheapPaymentGateway : IPaymentGateway
    {
    }
    public interface IExpensivePaymentGateway : IPaymentGateway
    {
    }
}