using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<FoodEntryDto> FoodEntries { get; set; }
    }
}
