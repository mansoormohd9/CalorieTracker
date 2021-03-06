using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string UserName { get; set; }
        public float CalorieLimit { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<FoodEntry> FoodEntries { get; } = new List<FoodEntry>();
    }
}
