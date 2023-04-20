using AutoMapper;
using Back_End_Dot_Net.DTOs;
using Back_End_Dot_Net.Models;

namespace Back_End_Dot_Net.Utils.Configs
{
    public class ProductMapperConfig : Profile
    {
        public ProductMapperConfig()
        {   
            // POST
            CreateMap<Chipset, ChipsetDTO>().ReverseMap();
            CreateMap<Laptop, LaptopDTO>().ReverseMap();
            CreateMap<Phone, PhoneDTO>().ReverseMap();
        }
    }
}