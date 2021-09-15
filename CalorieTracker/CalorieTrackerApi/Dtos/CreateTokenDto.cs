using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class CreateTokenDto
    {
        [Required]
        public string UserName { get; set; }
    }
}
