using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(2100)]
        public float CalorieLimit { get; set; }
        public ICollection<FoodEntryDto> FoodEntries { get; set; }
    }
}
