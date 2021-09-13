using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;

namespace CalorieTrackerApi.Authentication
{
    public class ApiAuthorizationRequired : Attribute, IActionFilter
    {
        private ITokenRepo _tokenRepo;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext.Request?.Headers[Constants.Constants.ApiKey];
            if (string.IsNullOrEmpty(apiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key is missing");
                return;
            }

            if(!Guid.TryParse(apiKey, out Guid parsedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid Apikey");
                return;
            }

            _tokenRepo = (ITokenRepo)context.HttpContext.RequestServices.GetService(typeof(ITokenRepo));
            var _httpContextAccessor = (IHttpContextAccessor)context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor));
            var userToken = _tokenRepo.GetUserToken(parsedApiKey);
            if(DateTime.UtcNow > userToken.Expiry)
            {
                context.Result = new UnauthorizedObjectResult("Api Key is expired");
                return;
            }

            var sessionKey = userToken.Token.ToString().ToLower();
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString(sessionKey)))
            {
                _httpContextAccessor.HttpContext.Session.SetString(sessionKey, userToken.User.UserName);
            }           
        }
    }
}
