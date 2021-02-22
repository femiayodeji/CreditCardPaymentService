using CreditCardPaymentService.Api.Data;
using CreditCardPaymentService.Api.Enumerations;
using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.ExternalServices;
using CreditCardPaymentService.Api.Utilities;
using AutoMapper;
using CreditCardPaymentService.Api.Dtos;

namespace CreditCardPaymentService.Api.Services
{
    public class PaymentService : IPaymentService
    {
        public Payment Data { get; set; }
        private readonly IPaymentRepo _repository;
        private IMapper _mapper;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPaymentGateway _premiumPaymentService;
        public PaymentService(
            IPaymentRepo repository, 
            IMapper mapper,
            ICheapPaymentGateway cheapPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway,
            IPaymentGateway premiumPaymentService
            )
        {
            _repository = repository;
            _mapper = mapper;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentService = premiumPaymentService;
        }

        public PaymentResponseDto ProcessPayment(PaymentDto paymentRequest)
        {
            var payment = _mapper.Map<Payment>(paymentRequest);
            _repository.CreatePayment(payment);
            PaymentState paymentState;
            if (payment.Amount < Constants.CheapAmountUpperBound)
            {
                paymentState = Cheap(payment);
            }
            else if(payment.Amount >= Constants.CheapAmountUpperBound && payment.Amount <= Constants.ExpensiveAmountUpperBound)
            {
                paymentState = Expensive(payment);
            }            
            else if(payment.Amount > Constants.ExpensiveAmountUpperBound)
            {
                paymentState = Premium(payment);
            }
            Data = payment;
            var paymentResponse =_mapper.Map<PaymentResponseDto>(payment);
            return paymentResponse;            
        }

        private PaymentState Cheap(Payment payment)
        {
            var gatewayResponse = _cheapPaymentGateway.GetResponse();
            if(gatewayResponse == null)
                return null;

            var paymentResponse = _mapper.Map<PaymentState>(gatewayResponse);
            paymentResponse.PaymentId = payment.Id;
            _repository.CreatePaymentState(paymentResponse);

            return paymentResponse;
        }

        private PaymentState Expensive(Payment payment)
        {
            var gatewayResponse = _expensivePaymentGateway.GetResponse();
            if(gatewayResponse == null)
                return null;

            var paymentResponse = _mapper.Map<PaymentState>(gatewayResponse);
            paymentResponse.PaymentId = payment.Id;
            _repository.CreatePaymentState(paymentResponse);
            if (paymentResponse.Status != PaymentStatusTypes.Processed)
            {
                gatewayResponse = _cheapPaymentGateway.GetResponse();
                if(gatewayResponse == null)
                    return null;

                paymentResponse = _mapper.Map<PaymentState>(gatewayResponse);
                paymentResponse.PaymentId = payment.Id;
                _repository.CreatePaymentState(paymentResponse);
            }                

            return paymentResponse;
        }

        private PaymentState Premium(Payment payment)
        {
            PaymentGatewayResponse gatewayResponse;
            PaymentState paymentResponse;
            int count = 0;
            do 
            {
                count++;
                gatewayResponse = _premiumPaymentService.GetResponse();
                paymentResponse = _mapper.Map<PaymentState>(gatewayResponse);
                if(paymentResponse == null)
                    break;

                paymentResponse.PaymentId = payment.Id;
                _repository.CreatePaymentState(paymentResponse);
            }
            while(
                count < Constants.PremiumPaymentServiceCount && 
                paymentResponse.Status != PaymentStatusTypes.Processed
            );
            return paymentResponse;
        }

    }
}