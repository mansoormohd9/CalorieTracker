using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Repositories.Interfaces
{
    public interface IUserRepo
    {
        List<User> GetUsers();
        User GetUser(string userName);

        (bool, string) DeleteUser(string userName);

        (bool, string) CreateUser(User user);

        (bool, string) UpdateUser(User user);
    }
}
