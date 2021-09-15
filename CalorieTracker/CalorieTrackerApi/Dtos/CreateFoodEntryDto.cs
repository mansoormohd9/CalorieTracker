using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class CreateFoodEntryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Calories { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
