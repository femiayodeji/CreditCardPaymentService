using FiledCom.Models;

namespace FiledCom.ExternalServices
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