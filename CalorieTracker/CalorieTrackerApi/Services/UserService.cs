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

        public (bool, string) CreateUser(User user)
        {
            return _userRepo.CreateUser(user);
        }

        public (bool, string) DeleteUser(string userName)
        {
            return _userRepo.DeleteUser(userName);
        }

        public UserDto GetUser(string userName)
        {
            var user = _userRepo.GetUser(userName);
            return _mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetUsers()
        {
            var users = _userRepo.GetUsers();
            return _mapper.Map<List<UserDto>>(users);
        }

        public (bool, string) UpdateUser(User user)
        {
            return _userRepo.UpdateUser(user);
        }
    }
}
