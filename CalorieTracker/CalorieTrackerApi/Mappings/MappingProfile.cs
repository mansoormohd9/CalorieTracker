using AutoMapper;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
