using CalorieTrackerApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.ViewModel
{
    public class FoodEntryViewModel
    {
        public List<FoodEntryDto> FoodEntries { get; set; }
        public FoodEntryFilter FoodEntryFilter{ get; set; }

    }
}
