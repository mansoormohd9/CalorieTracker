using CalorieTrackerApi.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Services.Interfaces
{
    public interface IUserViewService
    {
        void GetOrCreateUser(UserDto userDto, HttpContext httpContext);
    }
}
