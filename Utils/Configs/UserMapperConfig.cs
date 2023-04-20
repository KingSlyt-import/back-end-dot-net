using AutoMapper;
using Back_End_Dot_Net.DTOs;
using Back_End_Dot_Net.Models;

namespace Back_End_Dot_Net.Utils.Configs
{
    public class UserMapperConfig : Profile
    {
        public UserMapperConfig()
        {   
            // GET
            CreateMap<User, GetAllUserDTO>().ReverseMap();
        }
    }
}