using FiledCom.Models;

namespace FiledCom.Services
{
    public interface IPaymentService 
    {
        Payment ProcessPayment(Payment payment);
    }
}