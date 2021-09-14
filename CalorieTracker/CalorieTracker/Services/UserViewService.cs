using CalorieTracker.Services.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Services
{
    public class UserViewService: IUserViewService
    {
        private readonly IUserService _userService;

        public UserViewService(IUserService userService)
        {
            _userService = userService;
        }
    }
}
