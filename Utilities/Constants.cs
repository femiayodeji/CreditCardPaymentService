using System;

namespace FiledCom.Utilities
{
    public static class Constants
    {
        public const int MinInt = 0;
        public const int MinCreditCardNumberLength = 12;
        public const int MaxCreditCardNumberLength = 16;
        public const int CheapAmountUpperBound = 20;
        public const int ExpensiveAmountUpperBound = 500;
        public const int PremiumPaymentServiceCount = 3;

        public static class ErrorMessages
        {
            public const string RequiredCreditCardNumber = "Credit Card Number is required";
            public const string InvalidCreditCardNumber = "Invalid Credit Card Number";
            public const string RequiredCardHolder = "Card Holder Name is required";
            public const string InvalidExpirationDate = "Invalid Expiration Date";
            public const string ExpiredCreditCard = "Expired Credit Card";
            public const string InvalidSecurityCode = "Invalid Security Code";
            public const string NotPositiveAmount = "Amount must be positive";
        }
    }
}