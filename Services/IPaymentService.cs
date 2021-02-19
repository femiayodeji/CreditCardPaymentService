using System.Collections.Generic;
using FiledCom.Models;

namespace FiledCom.Services
{
    public interface IPaymentService 
    {
        void ProcessPayment(Payment payment);
    }
}