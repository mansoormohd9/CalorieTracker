using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services.Interfaces
{
    public interface IFoodEntryService
    {
        List<FoodEntryDto> GetFoodEntries(string userName);

        (bool, FoodEntryDto) GetFoodEntry(string userName, Guid guid);

        (bool, string) CreateFoodEntry(string userName, CreateFoodEntryDto foodEntry);

        (bool, string) UpdateFoodEntry(string userName, UpdateFoodEntryDto foodEntry);

        (bool, string) DeleteFoodEntry(string userName, Guid guid);
    }
}
