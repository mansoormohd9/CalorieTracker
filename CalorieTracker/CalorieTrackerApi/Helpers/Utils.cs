using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Helpers
{
    public class Utils
    {
        public static string GetUsernameFromContext(HttpContext httpContext)
        {
            var token = httpContext.Request?.Headers[Constants.Constants.ApiKey];
            return httpContext.Session.GetString(token.ToString().ToLower());
        }
    }
}
