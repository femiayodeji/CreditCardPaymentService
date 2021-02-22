using AutoMapper;
using CreditCardPaymentService.Api.Services;
using NUnit.Framework;
using Controllers;
using CreditCardPaymentService.Api.Dtos;
using System;
using Microsoft.AspNetCore.Mvc;
using CreditCardPaymentService.Api.Data;
using CreditCardPaymentService.Api.ExternalServices;
using CreditCardPaymentService.Api.Mapping;
using System.Net;
using CreditCardPaymentService.Api.Models;
using CreditCardPaymentService.Api.Enumerations;

namespace CreditCardPaymentService.Api.Test
{
    public class Tests
    {
        private IPaymentService _paymentService;
        private IMapper _mapper;
        private PaymentsController _paymentsController;
        private IPaymentRepo _repository;
        private ICheapPaymentGateway _cheapPaymentGateway;
        private IExpensivePaymentGateway _expensivePaymentGateway;
        private IPaymentGateway _premiumPaymentService;

        private PaymentDto _creditCardPayment;

        [SetUp]
        public void Setup()
        {
            _repository = new MockPaymentRepo();
            var profile = new PaymentAutoMapperConfigurator();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);

            _cheapPaymentGateway = new CheapPaymentGateway();
            _expensivePaymentGateway = new ExpensivePaymentGateway();
            _premiumPaymentService = new PremiumPaymentService();
            _premiumPaymentService.Response = new PaymentGatewayResponse();
            _paymentService = new PaymentService(
                _repository, 
                _mapper,
                _cheapPaymentGateway,
                _expensivePaymentGateway,
                _premiumPaymentService                
            );
            _paymentsController = new PaymentsController(_paymentService);

            _creditCardPayment = new PaymentDto {
                CreditCardNumber = "4012888888881881",
                CardHolder = "Femi Ayodeji",
                ExpirationDate = Convert.ToDateTime("2021-03"),
                SecurityCode = "123",
                Amount = 510.24m
            };
        }

        [Test]
        public void PaymentServiceProcess()
        {
            var result = _paymentService.ProcessPayment(_creditCardPayment);  

            Assert.IsInstanceOf<PaymentResponseDto>(result);
        }

        [Test]
        public void CheapPayment()
        {
            _cheapPaymentGateway = new CheapPaymentGateway();
            _cheapPaymentGateway.Response = new PaymentGatewayResponse{ Status = "Ok"};
            _paymentService = new PaymentService(
                _repository, 
                _mapper,
                _cheapPaymentGateway,
                _expensivePaymentGateway,
                _premiumPaymentService                
            );

            _creditCardPayment.Amount = 5;

            var result = _paymentService.ProcessPayment(_creditCardPayment);  

            Assert.AreEqual(PaymentStatusTypes.Processed.ToString(), result.State.Status);
        }

        [Test]
        public void ExpensivePayment()
        {
            _expensivePaymentGateway = new ExpensivePaymentGateway();
            _expensivePaymentGateway.Response = new PaymentGatewayResponse{ Status = "Ok"};
            _paymentService = new PaymentService(
                _repository, 
                _mapper,
                _cheapPaymentGateway,
                _expensivePaymentGateway,
                _premiumPaymentService                
            );

            _creditCardPayment.Amount = 25;

            var result = _paymentService.ProcessPayment(_creditCardPayment);  

            Assert.AreEqual(PaymentStatusTypes.Processed.ToString(), result.State.Status);
        }

        [Test]
        public void PremiumPayment()
        {
            _premiumPaymentService = new PremiumPaymentService();
            _premiumPaymentService.Response = new PaymentGatewayResponse{ Status = "Ok"};
            _paymentService = new PaymentService(
                _repository, 
                _mapper,
                _cheapPaymentGateway,
                _expensivePaymentGateway,
                _premiumPaymentService                
            );

            _creditCardPayment.Amount = 600;

            var result = _paymentService.ProcessPayment(_creditCardPayment);  

            Assert.AreEqual(PaymentStatusTypes.Processed.ToString(), result.State.Status);
        }

    }
}