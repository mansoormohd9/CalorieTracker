using AutoMapper;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public (bool, string) CreateUser(UserDto user)
        {
            var existingUser = _userRepo.GetUser(user.UserName);
            if(existingUser != null)
            {
                return (false, "User already exists");
            }
            return _userRepo.CreateUser(_mapper.Map<User>(user));
        }

        public (bool, string) DeleteUser(string userName)
        {
            return _userRepo.DeleteUser(userName);
        }

        public (bool, UserDto) GetUser(string userName)
        {
            var user = _userRepo.GetUser(userName);
            if(user == null)
            {
                return (false, null);
            }
            return (true, _mapper.Map<UserDto>(user));
        }

        public List<UserDto> GetUsers()
        {
            var users = _userRepo.GetUsers();
            return _mapper.Map<List<UserDto>>(users);
        }

        public (bool, string) UpdateUser(UserDto user)
        {
            return _userRepo.UpdateUser(_mapper.Map<User>(user));
        }
    }
}
