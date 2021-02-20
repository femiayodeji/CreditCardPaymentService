using FiledCom.Data;
using FiledCom.Enumerations;
using FiledCom.Models;
using FiledCom.ExternalServices;

namespace FiledCom.Services
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        private readonly IPaymentRepo _repository;
        public CheapPaymentGateway(IPaymentRepo repository)
        {
            _repository = repository;
        }

        public PaymentState Charge(Payment payment)
        {
            var paymentResponse = FakePaymentResponse.RandomizePaymentReponse(payment);
            _repository.CreatePaymentState(paymentResponse);
            return paymentResponse;
        }
   }
}