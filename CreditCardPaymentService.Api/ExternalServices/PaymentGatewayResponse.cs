using System;

namespace CreditCardPaymentService.Api.ExternalServices
{
    public class PaymentGatewayResponse
    {
        public string Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}