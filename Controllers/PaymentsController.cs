using AutoMapper;
using FiledCom.Dtos;
using FiledCom.Models;
using FiledCom.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _PaymentService;
        private IMapper _mapper;

        public PaymentsController(IPaymentService PaymentService, IMapper mapper)
        {
            _PaymentService = PaymentService;
            _mapper = mapper;
        }

        //POST api/Payments
        [HttpPost]
        public ActionResult <Payment> ProcessPayment(PaymentDto request){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var paymentModel = _mapper.Map<Payment>(request);
            _PaymentService.ProcessPayment(paymentModel);
            return Ok(request);
        }        
    }
}