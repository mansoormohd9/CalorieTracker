using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Models
{
    public class UserToken
    {
        public int ID { get; set; }
        public Guid Token { get; set; }
        public string IpAddress { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime LastLogin { get; set; }

        public User User { get; set; }
    }
}
