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
            throw new NotImplementedException();
        }

        public UserDto GetUser(string userName)
        {
            var user = _userRepo.GetUser(userName);
            return _mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

        public (bool, string) UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
