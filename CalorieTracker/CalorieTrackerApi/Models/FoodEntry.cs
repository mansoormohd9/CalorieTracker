using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Models
{
    public class FoodEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }

        [ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
