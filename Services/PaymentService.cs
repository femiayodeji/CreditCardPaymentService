using FiledCom.Data;
using FiledCom.Enumerations;
using FiledCom.Models;

namespace FiledCom.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _repository;
        public PaymentService(IPaymentRepo repository)
        {
            _repository = repository;
        }

        public void ProcessPayment(Payment payment)
        {
            _repository.CreatePayment(payment);
            PaymentState paymentState = new PaymentState();
            paymentState.PaymentId = payment.Id;
            paymentState.Type = PaymentStateTypes.Pending;
            _repository.CreatePaymentState(paymentState);
        }
    }
}