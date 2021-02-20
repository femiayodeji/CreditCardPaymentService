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

            var paymentStatusTypes = Enum.GetValues(typeof(PaymentStatusTypes));
            Random random = new Random();
            PaymentStatusTypes randomStatus = (PaymentStatusTypes)paymentStatusTypes.GetValue(random.Next(paymentStatusTypes.Length));
            paymentState.Status = randomStatus;

            return paymentState;
        }
    }
}