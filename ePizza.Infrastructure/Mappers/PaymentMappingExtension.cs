using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Infrastructure.Entities;


namespace ePizza.Infrastructure.Mappers
{
    public class PaymentMappingExtension : Profile
    {

        public PaymentMappingExtension()
        {
            CreateMap<PaymentDomain, PaymentDetail>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<OrderDomain, Order>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<OrderItemDomain, OrderItem>();
        }
    }
}
