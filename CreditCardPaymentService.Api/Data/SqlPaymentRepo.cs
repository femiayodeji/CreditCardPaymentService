using System;
using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.Data
{
    public class SqlPaymentRepo : IPaymentRepo
    {
        private readonly PaymentContext _context;

        public SqlPaymentRepo(PaymentContext context)
        {
            _context = context;
        }

        public void CreatePayment(Payment payment)
        {
            if(payment == null){
                throw new ArgumentNullException(nameof(payment));
            }
            _context.Payments.Add(payment);
            SaveChanges();
        }

        public void CreatePaymentState(PaymentState paymentState)
        {
            if(paymentState == null){
                throw new ArgumentNullException(nameof(paymentState));
            }
            _context.PaymentStates.Add(paymentState);
            SaveChanges();
        }

        private bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}