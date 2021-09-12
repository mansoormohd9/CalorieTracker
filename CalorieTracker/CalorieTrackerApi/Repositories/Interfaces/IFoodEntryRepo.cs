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

        FoodEntry GetFoodEntry(string userName, Guid guid);

        (bool, string) CreateFoodEntry(string userName, FoodEntry foodEntry);

        (bool, string) UpdateFoodEntry(string userName, FoodEntry foodEntry);

        (bool, string) DeleteFoodEntry(string userName, Guid guid);
    }
}
