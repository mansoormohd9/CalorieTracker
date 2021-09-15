using AutoMapper;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

            CreateMap<FoodEntry, UpdateFoodEntryDto>();
            CreateMap<UpdateFoodEntryDto, FoodEntry>();

            CreateMap<FoodEntry, CreateFoodEntryDto>();
            CreateMap<CreateFoodEntryDto, FoodEntry>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<FoodEntry, FoodEntryDto>();

            CreateMap<CreateTokenDto, UserToken>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Expiry, opt => opt.MapFrom(src => DateTime.UtcNow.AddHours(Constants.Constants.TokenExpiry)))
                .ForMember(dest => dest.IpAddress, opt => opt.MapFrom<IPAddressResolver>())
                //.ForMember(dest => dest.User, opt => opt.MapFrom<UserResolver>())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserIdResolver>());
            CreateMap<UserToken, CreateTokenDto>();

            CreateMap<TokenDto, UserToken>();
            CreateMap<UserToken, TokenDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            //Dto to Dto
            CreateMap<UpdateFoodEntryDto, FoodEntryDto>();
            CreateMap<CreateFoodEntryDto, FoodEntryDto>();
        }
    }

    public class IPAddressResolver : IValueResolver<CreateTokenDto, UserToken, string>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public IPAddressResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(CreateTokenDto source, UserToken destination, string destMember, ResolutionContext context)
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }

    public class UserResolver : IValueResolver<CreateTokenDto, UserToken, User>
    {
        private IUserRepo _userRepo;

        public UserResolver(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public User Resolve(CreateTokenDto source, UserToken destination, User destMember, ResolutionContext context)
        {
            var user = _userRepo.GetUser(source.UserName);
            if(user != null)
            {
                return user;
            }
            else
            {
                //creating new user
                _ = _userRepo.CreateUser(new User { UserName = source.UserName, IsAdmin = false });
                return _userRepo.GetUser(source.UserName);
            }
        }
    }

    public class UserIdResolver : IValueResolver<CreateTokenDto, UserToken, int>
    {
        private IUserRepo _userRepo;

        public UserIdResolver(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public int Resolve(CreateTokenDto source, UserToken destination, int destMember, ResolutionContext context)
        {
            var user = _userRepo.GetUser(source.UserName);
            if (user != null)
            {
                return user.ID;
            }
            else
            {
                //creating new user
                _ = _userRepo.CreateUser(new User { UserName = source.UserName, IsAdmin = false });
                return _userRepo.GetUser(source.UserName).ID;
            }
        }
    }
}
