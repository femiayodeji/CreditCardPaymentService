using FiledCom.Data;
using FiledCom.Enumerations;
using FiledCom.Models;
using FiledCom.ExternalServices;

namespace FiledCom.Services
{
    public class PremiumPaymentService : IPaymentGateway
    {
        private readonly IPaymentRepo _repository;
        public PremiumPaymentService(IPaymentRepo repository)
        {
            _repository = repository;
        }

        public PaymentState Charge(Payment payment)
        {
            PaymentState paymentState = new PaymentState();
            paymentState.PaymentId = payment.Id;
            paymentState.Type = PaymentStateTypes.Pending;
            _repository.CreatePaymentState(paymentState);
            return paymentState;
        }
   }
}