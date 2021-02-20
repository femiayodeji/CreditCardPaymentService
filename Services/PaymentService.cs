using FiledCom.Data;
using FiledCom.Enumerations;
using FiledCom.Models;
using FiledCom.ExternalServices;
using FiledCom.Utilities;

namespace FiledCom.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _repository;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPaymentGateway _premiumPaymentGateway;
        public PaymentService(
            IPaymentRepo repository, 
            ICheapPaymentGateway cheapPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway,
            IPaymentGateway premiumPaymentGateway
            )
        {
            _repository = repository;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentGateway = premiumPaymentGateway;
        }

        public Payment ProcessPayment(Payment payment)
        {
            _repository.CreatePayment(payment);
            PaymentState paymentResponse;
            if (payment.Amount < Constants.CheapAmountUpperBound)
            {
                paymentResponse = _cheapPaymentGateway.Charge(payment);
            }
            else if(payment.Amount >= Constants.CheapAmountUpperBound || payment.Amount <= Constants.ExpensiveAmountUpperBound)
            {
                paymentResponse = _expensivePaymentGateway.Charge(payment);
                if (paymentResponse.Type != PaymentStateTypes.Processed)
                {
                    paymentResponse = _cheapPaymentGateway.Charge(payment);
                }                
            }            
            else if(payment.Amount > Constants.ExpensiveAmountUpperBound)
            {
                int count = 0;
                do 
                {
                    paymentResponse = _premiumPaymentGateway.Charge(payment);
                    count++;
                }
                while(
                    count != Constants.PremiumPaymentServiceCount && 
                    paymentResponse.Type != PaymentStateTypes.Processed
                );
            }
            return payment;            
        }
    }
}