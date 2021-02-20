using AutoMapper;
using FiledCom.Dtos;
using FiledCom.Models;

namespace FiledCom.Mapping
{
    public class PaymentAutoMapperConfigurator : Profile
    {
        public PaymentAutoMapperConfigurator()
        {
            CreateMap<PaymentDto, Payment>();
            CreateMap<PaymentState, PaymentStateDto>();
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(x => x.States, options => options.MapFrom(s => s.PaymentStates));
        }
    }
}