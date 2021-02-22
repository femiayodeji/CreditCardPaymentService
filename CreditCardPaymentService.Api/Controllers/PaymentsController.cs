using CreditCardPaymentService.Api.Dtos;
using CreditCardPaymentService.Api.Enumerations;
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

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        //POST api/payments/process
        [HttpPost("Process")]
        public ActionResult<PaymentResponseDto> ProcessPayment(PaymentDto request){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var response = _paymentService.ProcessPayment(request);
            if (response != null)
            {
                if (response.State == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "No response from payment gateway");                

                if (response.State.Status == PaymentStatusTypes.Processed.ToString())
                    return Ok(response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Payment was not successfully Processed");                
        }        
    }
}