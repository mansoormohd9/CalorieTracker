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
        private readonly IMapper _mapper;

        public TokenService(ITokenRepo tokenRepo, IMapper mapper)
        {
            _tokenRepo = tokenRepo;
            _mapper = mapper;
        }

        public (bool, string) CreateUserToken(string userName, UserToken userToken)
        {
            return _tokenRepo.CreateUserToken(userName, userToken);
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

        public TokenDto GetUserToken(Guid guid)
        {
            var userToken = _tokenRepo.GetUserToken(guid);
            return _mapper.Map<TokenDto>(userToken);
        }

        public (bool, string) UpdateUserToken(string userName, UserToken userToken)
        {
            return _tokenRepo.UpdateUserToken(userName, userToken);
        }
    }
}
