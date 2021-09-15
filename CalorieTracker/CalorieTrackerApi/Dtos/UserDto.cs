using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<CreateFoodEntryDto> FoodEntries { get; set; }
    }
}
