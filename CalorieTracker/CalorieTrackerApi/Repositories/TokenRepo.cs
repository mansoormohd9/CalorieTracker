﻿using CalorieTrackerApi.Data;
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
                context.UserTokens.Add(userToken);
                context.SaveChanges();
                return (true, "User Token added");
            }
        }

        public (bool, string) DeleteUserToken(Guid guid)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userToken = context.UserTokens.FirstOrDefault(x => x.Token == guid);
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

        public (bool, string) RefreshUserToken(string userName, UserToken userToken)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var existingUserToken = context.UserTokens.FirstOrDefault(x => x.User.UserName == userName);
                if (existingUserToken == null)
                {
                    return (false, "User doesn't exist");
                }
                context.UserTokens.Remove(existingUserToken);
                context.SaveChanges();

                context.UserTokens.Add(userToken);
                context.SaveChanges();
                return (true, "User Token added");
            }
        }
    }
}
