using CreditCardPaymentService.Api.Data;
using CreditCardPaymentService.Api.Enumerations;
using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.ExternalServices;

namespace CreditCardPaymentService.Api.Services
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public Payment Request { get; set; }
        public PaymentGatewayResponse Response { get; set; }
        public PaymentState Charge(Payment payment)
        {
            throw new System.NotImplementedException();
        }

        public PaymentGatewayResponse GetResponse()
        {
            return Response;
        }
   }
}