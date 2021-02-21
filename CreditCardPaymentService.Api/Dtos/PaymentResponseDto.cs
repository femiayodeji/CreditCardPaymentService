using System;
using System.Collections.Generic;

namespace CreditCardPaymentService.Api.Dtos
{
    public class PaymentResponseDto : PaymentDto
    {
        public int Id { get; set; }
        public PaymentStateDto State { get; set; }
    }
}