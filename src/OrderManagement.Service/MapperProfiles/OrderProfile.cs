using AutoMapper;
using OrderManagement.DTOs;
using OrderManagement.Entities;

namespace OrderManagement.Service.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderAddDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
        }
    }
}
