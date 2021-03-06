using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class UpdateFoodEntryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
        public double Calories { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Guid Guid { get; set; }
    }
}
