using CalorieTrackerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Data
{
    public class CalorieTrackerDbContext: DbContext
    {
        public CalorieTrackerDbContext(DbContextOptions<CalorieTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FoodEntry> FoodEntries { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
