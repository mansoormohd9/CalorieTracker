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

        (bool, TokenDto) GetUserToken(Guid guid);
        TokenDto GetUserToken(string userName);

        (bool, string) CreateUserToken(CreateTokenDto userToken);

        (bool, string) RefreshUserToken(CreateTokenDto userToken);

        (bool, string) DeleteUserToken(Guid guid);
    }
}
