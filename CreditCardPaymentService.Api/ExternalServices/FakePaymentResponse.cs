using System;
using CreditCardPaymentService.Api.Enumerations;
using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.ExternalServices
{
    public static class FakePaymentResponse
    {
        public static PaymentState RandomizePaymentReponse(Payment payment)
        {
            PaymentState paymentState = new PaymentState();
            paymentState.PaymentId = payment.Id;

            var paymentStatusTypes = Enum.GetValues(typeof(PaymentStatusTypes));
            Random random = new Random();
            PaymentStatusTypes randomStatus = (PaymentStatusTypes)paymentStatusTypes.GetValue(random.Next(paymentStatusTypes.Length));
            paymentState.Status = randomStatus;

            return paymentState;
        }
    }
}