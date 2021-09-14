using CalorieTracker.Services.Interfaces;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Services
{
    public class UserViewService: IUserViewService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserViewService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public void GetOrCreateUser(UserDto userDto, HttpContext httpContext)
        {
            var user = _userService.GetUser(userDto.UserName);
            if(!user.Item1)
            {
                _ = _userService.CreateUser(userDto);
            }
            _ = _tokenService.CreateUserToken(new CreateTokenDto { UserName = userDto.UserName });
            httpContext.Session.SetString(Constants.Constants.ApiKey, _tokenService.GetUserToken(userDto.UserName).Token.ToString().ToLower());
            httpContext.Session.SetString(Constants.Constants.UserNamekey, userDto.UserName);
            httpContext.Session.SetString(Constants.Constants.IsAuthenticated, "true");
            httpContext.Session.SetString(Constants.Constants.Admin, user.Item1 ? user.Item2.IsAdmin.ToString() : "false");
        }
    }
}
