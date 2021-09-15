﻿using System;
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
        public int Calories { get; set; }
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }
    }
}
