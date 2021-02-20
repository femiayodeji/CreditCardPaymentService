using System;
using System.Collections.Generic;

namespace FiledCom.Dtos
{
    public class PaymentResponseDto : PaymentDto
    {
        public int Id { get; set; }
        public virtual List<PaymentStateDto>  States { get; set; }
    }
}