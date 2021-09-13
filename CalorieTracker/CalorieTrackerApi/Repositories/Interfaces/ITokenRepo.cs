using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Repositories.Interfaces
{
    public interface ITokenRepo
    {
        List<UserToken> GetUserTokens();

        UserToken GetUserToken(Guid guid);

        (bool, string) CreateUserToken(string userName, UserToken userToken);

        (bool, string) RefreshUserToken(string userName, UserToken userToken);

        (bool, string) DeleteUserToken(Guid guid);
    }
}
