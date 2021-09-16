using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class FoodEntryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 2100, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Calories { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }
    }
}
