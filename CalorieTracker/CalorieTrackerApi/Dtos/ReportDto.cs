using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class ReportDto
    {
        public string UserName { get; set; }
        public EntryStats CurrentWeek { get; set; }
        public EntryStats PastWeek { get; set; }
        public EntryStats CurrentDay { get; set; }

    }
}
