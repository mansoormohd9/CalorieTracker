using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetUsers();

        (bool, UserDto) GetUser(string userName);

        (bool, string) CreateUser(UserDto user);

        (bool, string) UpdateUser(UserDto user);

        (bool, string) DeleteUser(string userName);
    }
}
