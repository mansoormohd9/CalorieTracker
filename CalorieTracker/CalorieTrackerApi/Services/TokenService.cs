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
    public class TokenService: ITokenService
    {
        private readonly ITokenRepo _tokenRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TokenService(ITokenRepo tokenRepo, IMapper mapper, IUserService userService)
        {
            _tokenRepo = tokenRepo;
            _mapper = mapper;
            _userService = userService;
        }

        public (bool, string) CreateUserToken(CreateTokenDto userToken)
        {
            var userExists = _userService.GetUser(userToken.UserName);
            if(userExists.Item2 == null)
            {
                return (false, "User doesn't exists");
            }
            return _tokenRepo.CreateUserToken(userToken.UserName, _mapper.Map<UserToken>(userToken));
        }

        public (bool, string) DeleteUserToken(Guid guid)
        {
            return _tokenRepo.DeleteUserToken(guid);
        }

        public List<TokenDto> GetUserTokens()
        {
            var userTokens = _tokenRepo.GetUserTokens();
            return _mapper.Map<List<TokenDto>>(userTokens);
        }

        public (bool, TokenDto) GetUserToken(Guid guid)
        {
            var userToken = _tokenRepo.GetUserToken(guid);
            if (userToken == null)
            {
                return (false, null);
            }
            return (true, _mapper.Map<TokenDto>(userToken));
        }

        public (bool, string) RefreshUserToken(CreateTokenDto userToken)
        {
            var userExists = _userService.GetUser(userToken.UserName);
            if (userExists.Item2 == null)
            {
                return (false, "User doesn't exists");
            }
            return _tokenRepo.RefreshUserToken(userToken.UserName, _mapper.Map<UserToken>(userToken));
        }

        public TokenDto GetUserToken(string userName)
        {
            return _mapper.Map<TokenDto>(_tokenRepo.GetUserToken(userName));
        }
    }
}
