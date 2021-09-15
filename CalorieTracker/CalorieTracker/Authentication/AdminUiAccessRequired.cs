using CalorieTrackerApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Authentication
{
    public class AdminUiAccessRequired: Attribute, IActionFilter
    {
        private ITokenRepo _tokenRepo;

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext?.Session.GetString(Constants.Constants.ApiKey);
            if (string.IsNullOrEmpty(apiKey))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Home", action = "LoginRequired" })
                );
                return;
            }

            if (!Guid.TryParse(apiKey, out Guid parsedApiKey))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Home", action = "LoginRequired" })
                );
                return;
            }

            _tokenRepo = (ITokenRepo)context.HttpContext.RequestServices.GetService(typeof(ITokenRepo));
            var userToken = _tokenRepo.GetUserToken(parsedApiKey);
            if (userToken == null || DateTime.UtcNow > userToken.Expiry)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Home", action = "LoginRequired" })
                );
                return;
            }

            if (!userToken.User.IsAdmin)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Home", action = "AdminAccessRequired" })
                );
                return;
            }
        }
    }
}
