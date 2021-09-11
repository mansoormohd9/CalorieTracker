using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiAuthorizationRequired: Attribute
    {
    }
}
