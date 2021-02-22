using System.Linq;
using AutoMapper;
using CreditCardPaymentService.Api.Dtos;
using CreditCardPaymentService.Api.Enumerations;
using CreditCardPaymentService.Api.ExternalServices;
using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.Mapping
{
    public class PaymentAutoMapperConfigurator : Profile
    {
        public PaymentAutoMapperConfigurator()
        {
            CreateMap<PaymentDto, Payment>();

            CreateMap<PaymentState, PaymentStateDto>();

            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(x => x.State, options => options
                    .MapFrom(s => s.PaymentStates.OrderByDescending(r => r.Id).FirstOrDefault()));

            CreateMap<PaymentGatewayResponse, PaymentState>()
                .ForMember(x => x.Status, options => options
                    .MapFrom(s => DetermineStatus(s)));
           
        }

        private PaymentStatusTypes DetermineStatus(PaymentGatewayResponse source)
        {
            if (!string.IsNullOrEmpty(source.Status))
            {
                if (source.Status.ToLower() == "OK".ToLower())
                    return PaymentStatusTypes.Processed;
            }
            return PaymentStatusTypes.Failed;
        }

    }
}