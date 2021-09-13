using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<FoodEntry> FoodEntries { get; set; }
    }
}
