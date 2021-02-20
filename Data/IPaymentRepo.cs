using FiledCom.Models;

namespace FiledCom.Data
{
    public interface IPaymentRepo
    {
        void CreatePayment(Payment payment);
        void CreatePaymentState(PaymentState paymentState);
    }
}