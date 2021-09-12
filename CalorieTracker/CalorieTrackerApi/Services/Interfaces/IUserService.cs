using CalorieTrackerApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetUsers();

        UserDto GetUser();

        (bool, string) CreateUser();

        (bool, string) UpdateUser();
    }
}
