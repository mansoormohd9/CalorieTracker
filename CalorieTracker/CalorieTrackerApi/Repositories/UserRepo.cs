using CalorieTrackerApi.Data;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CalorieTrackerApi.Repositories
{
    public class UserRepo: IUserRepo
    {
        private readonly IDbContextFactory<CalorieTrackerDbContext> _contextFactory;

        public UserRepo(IDbContextFactory<CalorieTrackerDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public (bool, string) CreateUser(User user)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return (true, "User create successful");
            }
        }

        public (bool, string) DeleteUser(string userName)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
                if(user == null)
                {
                    return (false, "User doesn't exist");
                }
                var userTokens = context.UserTokens.Where(x => x.UserId == user.ID);
                context.UserTokens.RemoveRange(userTokens);
                context.SaveChanges();

                context.Users.Remove(user);
                context.SaveChanges();
                return (true, "Delete Successful");
            }
        }

        public User GetUser(string userName)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                return context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
            }
        }

        public List<User> GetUsers()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Users.Include(x => x.FoodEntries).ToList();
            }
        }

        public (bool, string) UpdateUser(User user)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                var curUser = context.Users.FirstOrDefault(x => x.UserName == user.UserName);
                if (curUser == null)
                {
                    return (false, "User doesn't exist");
                }
                curUser.IsAdmin = user.IsAdmin;
                curUser.CalorieLimit = user.CalorieLimit;
                context.SaveChanges();
                return (true, "User update successful");
            }
        }
    }
}
