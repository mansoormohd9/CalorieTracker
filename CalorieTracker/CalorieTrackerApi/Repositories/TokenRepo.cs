using CalorieTrackerApi.Data;
using CalorieTrackerApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Repositories
{
    public class TokenRepo: ITokenRepo
    {
        private readonly IDbContextFactory<CalorieTrackerDbContext> _contextFactory;

        public TokenRepo(IDbContextFactory<CalorieTrackerDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
