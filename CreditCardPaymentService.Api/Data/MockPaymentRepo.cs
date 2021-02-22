using System;
using System.Collections.Generic;
using System.Linq;
using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.Data
{
    public class MockPaymentRepo : IPaymentRepo
    {
        private IList<Payment> _payments = new List<Payment>();
        public void CreatePayment(Payment payment)
        {
            if(payment == null){
                throw new ArgumentNullException(nameof(payment));
            }
            payment.Id = _payments.Count() + 1;
            _payments.Add(payment);
        }

        public void CreatePaymentState(PaymentState paymentState)
        {
            if(paymentState == null){
                throw new ArgumentNullException(nameof(paymentState));
            }
            var payment = _payments.First(x => x.Id == paymentState.PaymentId);
            if (payment.PaymentStates == null)
                payment.PaymentStates = new List<PaymentState>();
            paymentState.Id = payment.PaymentStates.Count() + 1;
            payment.PaymentStates.Add(paymentState);
        }
    }
}