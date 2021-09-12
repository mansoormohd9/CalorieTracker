using CalorieTrackerApi.Data;
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
    }
}
