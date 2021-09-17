using CalorieTrackerApi.Data;
using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerTests.Helpers
{
    public class Utilities
    {
        #region snippet1
        public static void InitializeDbForTests(CalorieTrackerDbContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.UserTokens.Add(GetSeedingUserToken());
            db.FoodEntries.Add(GetSeedingUserFoodEntry());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(CalorieTrackerDbContext db)
        {
            db.Users.RemoveRange(db.Users);
            InitializeDbForTests(db);
        }

        public static List<User> GetSeedingUsers()
        {
            return new List<User>()
            {
                new User(){ 
                    UserName = "Test",
                    CalorieLimit = 2100,
                    IsAdmin = true
                }
            };
        }

        public static UserToken GetSeedingUserToken()
        {
            return new UserToken()
            {
                User = new User()
            };
        }

        public static FoodEntry GetSeedingUserFoodEntry()
        {
            return new FoodEntry()
            {
                Name = "Test"
            };
        }
        #endregion
    }
}
