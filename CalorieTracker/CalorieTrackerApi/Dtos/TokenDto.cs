using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class TokenDto
    {
        public Guid Token { get; set; }
        public string IpAddress { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime LastLogin { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
