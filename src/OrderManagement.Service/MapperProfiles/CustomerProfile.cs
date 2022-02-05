using AutoMapper;
using OrderManagement.DTOs;
using OrderManagement.Entities;

namespace OrderManagement.Service.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerAddDto>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDto>().ReverseMap();
            CreateMap<Customer, CustomerWithOrdersDto>().ReverseMap();
        }
    }
}
