using CreditCardPaymentService.Api.Data;
using CreditCardPaymentService.Api.Enumerations;
using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.ExternalServices;

namespace CreditCardPaymentService.Api.Services
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

        public PaymentGatewayResponse Response()
        {
            return new PaymentGatewayResponse();
        }
   }
}