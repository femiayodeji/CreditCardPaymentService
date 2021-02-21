using System.Linq;
using AutoMapper;
using CreditCardPaymentService.Api.Dtos;
using CreditCardPaymentService.Api.Enumerations;
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
                    .MapFrom(s => s.PaymentStates.FirstOrDefault(t => t.Status == PaymentStatusTypes.Processed)));
        }
    }
}