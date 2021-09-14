using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class FoodEntryDto
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }
    }
}
