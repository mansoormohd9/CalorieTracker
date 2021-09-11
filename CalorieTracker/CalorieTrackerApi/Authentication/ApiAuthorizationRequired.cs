using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace CalorieTrackerApi.Authentication
{
    public class ApiAuthorizationRequired : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
            context.Result = new UnauthorizedObjectResult("user is unauthorized");
        }
    }
}
