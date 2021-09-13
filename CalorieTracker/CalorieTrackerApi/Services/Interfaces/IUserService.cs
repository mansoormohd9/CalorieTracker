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

        (bool, string) CreateUser(User user);

        (bool, string) UpdateUser(User user);

        (bool, string) DeleteUser(string userName);
    }
}
