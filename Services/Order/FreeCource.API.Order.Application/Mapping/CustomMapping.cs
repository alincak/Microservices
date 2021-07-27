using AutoMapper;
using FreeCource.API.Order.Application.Dtos;
using FreeCource.API.Order.Domain.OrderAggregate;

namespace FreeCource.API.Order.Application.Mapping
{
  internal class CustomMapping : Profile
  {
    public CustomMapping()
    {
      CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
      CreateMap<OrderItem, OrderItemDto>().ReverseMap();
      CreateMap<Address, AddressDto>().ReverseMap();
    }
  }
}
