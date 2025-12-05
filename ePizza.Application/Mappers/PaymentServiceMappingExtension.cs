using AutoMapper;
using ePizza.Application.DTOs.Request;
using ePizza.Domain.Entities;


namespace ePizza.Application.Mappers
{
    public class PaymentServiceMappingExtension : Profile
    {
        public PaymentServiceMappingExtension()
        {
            CreateMap<MakePaymentRequestDto, PaymentDomain>().ReverseMap();

            CreateMap<OrderRequestDto, OrderDomain>();

            CreateMap<OrderItemsRequestDto, OrderItemDomain>();
        }
    }
}
