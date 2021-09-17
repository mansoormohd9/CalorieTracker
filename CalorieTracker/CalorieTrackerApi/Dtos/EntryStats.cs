using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class EntryStats
    {
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int FoodEntriesAdded { get; set; }
        public double CaloriesConsumed { get; set; }
        public double AverageCaloriesConsumed { get; set; }
    }
}
