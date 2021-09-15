using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class EntryStats
    {
        public int FoodEntriesAdded { get; set; }
        public float CaloriesConsumed { get; set; }
        public double AverageCaloriesConsumed { get; set; }
    }
}
