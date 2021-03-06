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
    public class FoodEntryRepo: IFoodEntryRepo
    {
        private readonly IDbContextFactory<CalorieTrackerDbContext> _contextFactory;

        public FoodEntryRepo(IDbContextFactory<CalorieTrackerDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public (bool, string) CreateFoodEntry(string userName, FoodEntry foodEntry)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return (false, "User doesn't exist");
                }
                user.FoodEntries.Add(foodEntry);
                context.SaveChanges();
                return (true, "Food Entry added");
            }
        }

        public (bool, string) DeleteFoodEntry(string userName, Guid guid)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return (false, "User doesn't exist");
                }
                var toBeRemovedFoodEntry = user.FoodEntries.FirstOrDefault(x => x.Guid == guid);
                user.FoodEntries.Remove(toBeRemovedFoodEntry);
                context.SaveChanges();

                return (true, "Food Entry Removed");
            }
        }

        public List<FoodEntry> GetFoodEntries(string userName)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
                return user.FoodEntries.ToList();
            }
        }

        public List<FoodEntry> GetFoodEntries(DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var foodEntries = context.FoodEntries.Include(x => x.User).Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date);
                return foodEntries.ToList();
            }
        }

        public List<FoodEntry> GetFoodEntries(string userName, DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var foodEntries = context.FoodEntries.Include(x => x.User).Where(x => x.User.UserName == userName && x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date);
                return foodEntries.ToList();
            }
        }

        public FoodEntry GetFoodEntry(string userName, Guid guid)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
                return user.FoodEntries.FirstOrDefault(x => x.Guid == guid);
            }
        }

        public double GetCaloriesAddedForDate(string userName, FoodEntry foodEntry)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
                return user.FoodEntries.Where(x => ((x.Date.Date == foodEntry.Date.Date) && (x.Guid != foodEntry.Guid)))
                                        .Sum(y => y.Calories);
            }
        }

        public (bool, string) UpdateFoodEntry(string userName, FoodEntry foodEntry)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.Include(x => x.FoodEntries).FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return (false, "User doesn't exist");
                }
                var toBeRemovedFoodEntry = user.FoodEntries.FirstOrDefault(x => x.Guid == foodEntry.Guid);
                if (toBeRemovedFoodEntry == null)
                {
                    return (false, "Food Entry doesnot exist");
                }
                user.FoodEntries.Remove(toBeRemovedFoodEntry);
                context.FoodEntries.Remove(toBeRemovedFoodEntry);
                context.SaveChanges();

                user.FoodEntries.Add(foodEntry);
                context.SaveChanges();
                return (true, "Food Entry updated");
            }
        }
    }
}
