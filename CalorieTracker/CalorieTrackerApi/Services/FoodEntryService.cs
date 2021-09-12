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
        private readonly IMapper _mapper;

        public FoodEntryService(IFoodEntryRepo foodEntryRepo, IMapper mapper)
        {
            _foodEntryRepo = foodEntryRepo;
            _mapper = mapper;
        }

        public (bool, string) CreateFoodEntry(string userName, FoodEntry foodEntry)
        {
            return _foodEntryRepo.CreateFoodEntry(userName, foodEntry);
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

        public FoodEntryDto GetFoodEntry(string userName, Guid guid)
        {
            var foodEntry = _foodEntryRepo.GetFoodEntry(userName, guid);
            return _mapper.Map<FoodEntryDto>(foodEntry);
        }

        public (bool, string) UpdateFoodEntry(string userName, FoodEntry foodEntry)
        {
            return _foodEntryRepo.UpdateFoodEntry(userName, foodEntry);
        }
    }
}
