using CreditCardPaymentService.Api.Dtos;
using FluentValidation;
using CreditCardPaymentService.Api.Utilities;
using System;
using System.Text;
using System.Linq;

namespace CreditCardPaymentService.Api.Validations
{
  public class PaymentValidator : AbstractValidator<PaymentDto> 
  {
    public PaymentValidator() 
    {
      RuleFor(x => x.CreditCardNumber)
        .NotEmpty().WithMessage(Constants.ErrorMessages.RequiredCreditCardNumber)
        .Must(x => IsCreditCardNumberValid(x)).WithMessage(Constants.ErrorMessages.InvalidCreditCardNumber);

      RuleFor(x => x.CardHolder)
        .NotEmpty().WithMessage(Constants.ErrorMessages.RequiredCardHolder);

      RuleFor(x => x.ExpirationDate)
        .NotNull().WithMessage(Constants.ErrorMessages.InvalidExpirationDate)
        .Must(x => x > DateTime.UtcNow).WithMessage(Constants.ErrorMessages.ExpiredCreditCard);

      When(x => !string.IsNullOrEmpty(x.SecurityCode), () => 
      {
        RuleFor(x => x.SecurityCode)
        .Must(x => x.Length == 3).WithMessage(Constants.ErrorMessages.InvalidSecurityCode);
      });

      RuleFor(x => x.Amount)
        .NotNull()
        .GreaterThan(Constants.MinInt)
        .WithMessage(Constants.ErrorMessages.NotPositiveAmount);
    }

    public bool IsCreditCardNumberValid(string creditCardNumber)
    {
      creditCardNumber = NormalizeCreditCardNumber(creditCardNumber);
      if (
        creditCardNumber.Length >= Constants.MinCreditCardNumberLength &&
        creditCardNumber.Length <= Constants.MaxCreditCardNumberLength
        )
      {
          int sumOfDigits = creditCardNumber
            .Reverse()
            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
            .Sum((e) => e / 10 + e % 10);

          return sumOfDigits % 10 == 0;
      }
      return false;
    }

    private string NormalizeCreditCardNumber(string creditCardNumber)
    {
      if (creditCardNumber == null)
        creditCardNumber = String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (char c in creditCardNumber)
      {
        if (Char.IsDigit(c))
          sb.Append(c);
      }

      return sb.ToString();
    }
  }    
}
