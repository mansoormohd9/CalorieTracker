using AutoMapper;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services
{
    public class FoodEntryService: IFoodEntryService
    {
        private readonly IFoodEntryRepo _foodEntryRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public FoodEntryService(IFoodEntryRepo foodEntryRepo, IUserRepo userRepo, IMapper mapper)
        {
            _foodEntryRepo = foodEntryRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public (bool, string) CreateFoodEntry(string userName, CreateFoodEntryDto foodEntry)
        {
            var foodEntryModel = _mapper.Map<FoodEntry>(foodEntry);
            if (!CheckCalorieThreshold(userName, foodEntryModel))
            {
                return (false, Constants.Constants.CalorieThresholdLimit);
            }
            return _foodEntryRepo.CreateFoodEntry(userName, _mapper.Map<FoodEntry>(foodEntry));
        }

        public (bool, string) DeleteFoodEntry(string userName, Guid guid)
        {
            return _foodEntryRepo.DeleteFoodEntry(userName, guid);
        }

        public List<FoodEntryDto> GetFoodEntries(string userName)
        {
            var foodEntries = _foodEntryRepo.GetFoodEntries(userName);
            return _mapper.Map<List<FoodEntryDto>>(foodEntries);
        }

        public List<FoodEntryDto> GetFoodEntries(string userName, FoodEntryFilter foodEntryFilter)
        {
            var startDate = (foodEntryFilter.StartDate < foodEntryFilter.EndDate) ? foodEntryFilter.StartDate : foodEntryFilter.EndDate;
            var endDate = (foodEntryFilter.EndDate > foodEntryFilter.StartDate) ? foodEntryFilter.EndDate : foodEntryFilter.StartDate;
            var foodEntries = _foodEntryRepo.GetFoodEntries(userName, startDate, endDate);
            return _mapper.Map<List<FoodEntryDto>>(foodEntries);
        }

        public (bool, FoodEntryDto) GetFoodEntry(string userName, Guid guid)
        {
            var foodEntry = _foodEntryRepo.GetFoodEntry(userName, guid);
            if (foodEntry == null)
            {
                return (false, null);
            }
            return (true, _mapper.Map<FoodEntryDto>(foodEntry));
        }

        public (bool, string) UpdateFoodEntry(string userName, UpdateFoodEntryDto foodEntry)
        {
            var foodEntryModel = _mapper.Map<FoodEntry>(foodEntry);
            if(!CheckCalorieThreshold(userName, foodEntryModel))
            {
                return (false, Constants.Constants.CalorieThresholdLimit);
            }
            return _foodEntryRepo.UpdateFoodEntry(userName, _mapper.Map<FoodEntry>(foodEntry));
        }

        private bool CheckCalorieThreshold(string userName, FoodEntry foodEntry)
        {
            var userCalorieLimit = _userRepo.GetUser(userName).CalorieLimit;//This can be added to session or cached            
            var updatedCalories = _foodEntryRepo.GetCaloriesAddedForDate(userName, foodEntry) + foodEntry.Calories;
            return userCalorieLimit > updatedCalories;
        }
    }
}
