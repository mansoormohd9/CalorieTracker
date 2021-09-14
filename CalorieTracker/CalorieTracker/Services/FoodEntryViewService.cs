using CalorieTracker.Services.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Services
{
    public class FoodEntryViewService:IFoodEntryViewService
    {
        private readonly IFoodEntryService _foodEntryService;

        public FoodEntryViewService(IFoodEntryService foodEntryService)
        {
            _foodEntryService = foodEntryService;
        }
    }
}
