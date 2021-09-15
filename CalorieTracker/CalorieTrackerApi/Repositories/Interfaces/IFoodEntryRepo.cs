using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Repositories.Interfaces
{
    public interface IFoodEntryRepo
    {
        List<FoodEntry> GetFoodEntries(string userName);
        List<FoodEntry> GetFoodEntries(DateTime startDate, DateTime endDate);
        List<FoodEntry> GetFoodEntries(string userName, DateTime startDate, DateTime endDate);

        FoodEntry GetFoodEntry(string userName, Guid guid);
        float GetCaloriesAddedForDate(string userName, FoodEntry foodEntry);

        (bool, string) CreateFoodEntry(string userName, FoodEntry foodEntry);

        (bool, string) UpdateFoodEntry(string userName, FoodEntry foodEntry);

        (bool, string) DeleteFoodEntry(string userName, Guid guid);
    }
}
