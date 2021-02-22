using CreditCardPaymentService.Api.Dtos;
using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.Services
{
    public interface IPaymentService 
    {
        Payment Data { get; set; }
        PaymentResponseDto ProcessPayment(PaymentDto paymentDto);
    }
}