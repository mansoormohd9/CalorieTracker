using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Models
{
    public class FoodEntry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }
    }
}
