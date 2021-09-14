using CalorieTracker.Services.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Services
{
    public class TokenViewService: ITokenViewService
    {
        private readonly ITokenService _tokenService;

        public TokenViewService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
    }
}
