using CalorieTrackerApi.Data;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
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

        public (bool, string) CreateUserToken(string userName, UserToken userToken)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return (false, "User doesn't exist");
                }
                user.UserToken = userToken;
                context.SaveChanges();
                return (true, "User Token added");
            }
        }

        public (bool, string) DeleteUserToken(Guid guid)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserToken != null && x.UserToken.Token == guid);
                var userToken = context.UserTokens.FirstOrDefault(x => x.Token == guid);
                user.UserToken = null;
                context.UserTokens.Remove(userToken);
                context.SaveChanges();
                return (false, "User token Removed");
            }
        }

        public List<UserToken> GetUserTokens()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.UserTokens.ToList();
            }
        }

        public UserToken GetUserToken(Guid guid)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userToken = context.UserTokens.FirstOrDefault(x => x.Token == guid);
                return userToken;
            }
        }

        public (bool, string) UpdateUserToken(string userName, UserToken userToken)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return (false, "User doesn't exist");
                }
                user.UserToken = userToken;
                context.SaveChanges();
                return (true, "User Token added");
            }
        }
    }
}
