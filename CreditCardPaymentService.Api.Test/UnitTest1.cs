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
        
        [SetUp]
        public void Setup()
        {
            _repository = new MockPaymentRepo();
            var profile = new PaymentAutoMapperConfigurator();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);

            _cheapPaymentGateway = new CheapPaymentGateway(_repository);
            _expensivePaymentGateway = new ExpensivePaymentGateway(_repository);
            _premiumPaymentService = new PremiumPaymentService(_repository);
            _paymentService = new PaymentServiceMock(
                _repository, 
                _mapper,
                _cheapPaymentGateway,
                _expensivePaymentGateway,
                _premiumPaymentService                
            );
            _paymentsController = new PaymentsController(_paymentService);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void PaymentServiceProcess()
        {
            var requestModel = new PaymentDto {
                CreditCardNumber = "4012888888881881",
                CardHolder = "Femi Ayodeji",
                ExpirationDate = Convert.ToDateTime("2021-03"),
                SecurityCode = "123",
                Amount = 510.24m
            };
            var result = _paymentService.ProcessPayment(requestModel);  

            Assert.IsInstanceOf<PaymentResponseDto>(result);
        }

    }
}