using AutoMapper;
using MyProject.Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.DTOs
{
    public class MapperProfile: Profile
    {
        public MapperProfile() 
        { 
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Hour,UserDTO>().ReverseMap();
        }
    }
}
