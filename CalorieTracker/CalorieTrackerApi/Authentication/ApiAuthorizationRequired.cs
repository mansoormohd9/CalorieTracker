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
        private ITokenService _tokenService;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
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

            _tokenService = (ITokenService)context.HttpContext.RequestServices.GetService(typeof(ITokenService));
            var userToken = _tokenService.GetUserToken(parsedApiKey);
            if(DateTime.UtcNow > userToken.Expiry)
            {
                context.Result = new UnauthorizedObjectResult("Api Key is expired");
                return;
            }

            //HttpContext.Session.["Username"] = userToken.Token;

            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");            
        }
    }
}
