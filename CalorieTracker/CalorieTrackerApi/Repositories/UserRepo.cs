using CalorieTrackerApi.Data;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Repositories
{
    public class UserRepo: IUserRepo
    {
        private readonly IDbContextFactory<CalorieTrackerDbContext> _contextFactory;

        public UserRepo(IDbContextFactory<CalorieTrackerDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public User GetUser(string userName)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                return context.Users.FirstOrDefault(x => x.UserName == userName);
            }
        }
    }
}
