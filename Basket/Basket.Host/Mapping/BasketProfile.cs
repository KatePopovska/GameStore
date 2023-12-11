using AutoMapper;
using Basket.Host.Entities;
using Basket.Host.Models;
using Basket.Host.Models.Requests;
using Infrastructure.EventBusMessages.Common;
using Infrastructure.EventBusMessages.Events;

namespace Basket.Host.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
          CreateMap<Basket.Host.Entities.ShoppingCartItem, Infrastructure.EventBusMessages.Common.ShoppingCartItem>();
            CreateMap<ShoppingCart, ShoppingCartCheckout>().ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
           .ReverseMap();
        }
    }
}
