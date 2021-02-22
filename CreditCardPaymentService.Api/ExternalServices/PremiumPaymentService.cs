using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.ExternalServices;

namespace CreditCardPaymentService.Api.Services
{
    public class PremiumPaymentService : IPaymentGateway
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