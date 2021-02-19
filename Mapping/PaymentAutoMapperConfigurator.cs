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
        }
    }
}