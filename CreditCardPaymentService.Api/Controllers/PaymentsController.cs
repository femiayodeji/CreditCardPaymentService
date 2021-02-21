using System.Linq;
using AutoMapper;
using CreditCardPaymentService.Api.Dtos;
using CreditCardPaymentService.Api.Enumerations;
using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.Services;
using Microsoft.AspNetCore.Http;
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
            if (response.State != null)
                return Ok(response);
            return StatusCode(StatusCodes.Status500InternalServerError, "Payment was not successfully Processed");                
        }        
    }
}