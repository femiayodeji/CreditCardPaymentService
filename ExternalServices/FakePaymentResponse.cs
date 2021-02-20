using System;
using FiledCom.Enumerations;
using FiledCom.Models;

namespace FiledCom.ExternalServices
{
    public static class FakePaymentResponse
    {
        public static PaymentState RandomizePaymentReponse(Payment payment)
        {
            PaymentState paymentState = new PaymentState();
            paymentState.PaymentId = payment.Id;

            var paymentStateTypes = Enum.GetValues(typeof(PaymentStateTypes));
            Random random = new Random();
            PaymentStateTypes randomStateType = (PaymentStateTypes)paymentStateTypes.GetValue(random.Next(paymentStateTypes.Length));
            paymentState.Type = randomStateType;

            return paymentState;
        }
    }
}