using AutoMapper;
using CreditCardPaymentService.Api.Dtos;
using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        //POST api/payments/process
        [HttpPost("Process")]
        public ActionResult <PaymentResponseDto> ProcessPayment(PaymentDto request){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var paymentModel = _mapper.Map<Payment>(request);
            var response = _mapper.Map<PaymentResponseDto>(_paymentService.ProcessPayment(paymentModel));
            return Ok(response);
        }        
    }
}