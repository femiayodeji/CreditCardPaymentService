using CreditCardPaymentService.Api.Data;
using CreditCardPaymentService.Api.Enumerations;
using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.ExternalServices;
using CreditCardPaymentService.Api.Utilities;

namespace CreditCardPaymentService.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _repository;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPaymentGateway _premiumPaymentService;
        public PaymentService(
            IPaymentRepo repository, 
            ICheapPaymentGateway cheapPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway,
            IPaymentGateway premiumPaymentService
            )
        {
            _repository = repository;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentService = premiumPaymentService;
        }

        public Payment ProcessPayment(Payment payment)
        {
            _repository.CreatePayment(payment);
            PaymentState paymentResponse;
            if (payment.Amount < Constants.CheapAmountUpperBound)
            {
                paymentResponse = _cheapPaymentGateway.Charge(payment);
            }
            else if(payment.Amount >= Constants.CheapAmountUpperBound && payment.Amount <= Constants.ExpensiveAmountUpperBound)
            {
                paymentResponse = _expensivePaymentGateway.Charge(payment);
                if (paymentResponse.Status != PaymentStatusTypes.Processed)
                {
                    paymentResponse = _cheapPaymentGateway.Charge(payment);
                }                
            }            
            else if(payment.Amount > Constants.ExpensiveAmountUpperBound)
            {
                int count = 0;
                do 
                {
                    paymentResponse = _premiumPaymentService.Charge(payment);
                    count++;
                }
                while(
                    count < Constants.PremiumPaymentServiceCount && 
                    paymentResponse.Status != PaymentStatusTypes.Processed
                );
            }
            return payment;            
        }
    }
}