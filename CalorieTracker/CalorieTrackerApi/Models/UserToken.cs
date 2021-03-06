using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Models
{
    public class UserToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Guid Token { get; set; }
        public string IpAddress { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime LastLogin { get; set; }

        [ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        
        public User User { get; set; }
    }
}
