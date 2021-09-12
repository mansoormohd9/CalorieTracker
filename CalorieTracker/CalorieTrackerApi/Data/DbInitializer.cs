using CalorieTrackerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(CalorieTrackerDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var user = new User { UserName = "mansoormohd", IsAdmin = true };
            context.Users.Add(user);
            context.SaveChanges();          
        }
    }
}
