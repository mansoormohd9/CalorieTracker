using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services.Interfaces
{
    public interface ITokenService
    {
        List<TokenDto> GetUserTokens();

        TokenDto GetUserToken(Guid guid);

        (bool, string) CreateUserToken(string userName, UserToken userToken);

        (bool, string) UpdateUserToken(string userName, UserToken userToken);

        (bool, string) DeleteUserToken(Guid guid);
    }
}
