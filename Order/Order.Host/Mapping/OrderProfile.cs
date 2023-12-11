using AutoMapper;
using Infrastructure.EventBusMessages.Common;
using Infrastructure.EventBusMessages.Events;
using Order.Host.Data.Entities;
using Order.Host.Models.Dto;

namespace Order.Host.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, BasketCheckoutEvent>().ReverseMap();
            CreateMap<OrderDto, OrderEntity>().ReverseMap();
            CreateMap<ShoppingCartItem, ItemDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
        }
    }
}
