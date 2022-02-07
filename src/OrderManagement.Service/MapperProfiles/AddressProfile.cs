using AutoMapper;
using OrderManagement.DTOs;
using OrderManagement.Entities;

namespace OrderManagement.Service.MapperProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressAddDto>().ReverseMap();
            CreateMap<Address, AddressUpdateDto>().ReverseMap();
        }
    }
}
