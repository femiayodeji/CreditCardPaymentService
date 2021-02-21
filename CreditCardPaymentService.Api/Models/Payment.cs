using System;
using System.Collections.Generic;

namespace CreditCardPaymentService.Api.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string  CreditCardNumber { get; set; }
        public string  CardHolder { get; set; }
        public DateTime  ExpirationDate { get; set; }
        public string  SecurityCode { get; set; }
        public decimal  Amount { get; set; }
        public virtual ICollection<PaymentState>  PaymentStates { get; set; }
    }
}