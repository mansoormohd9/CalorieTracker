using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class ReportDto
    {
        public EntryStats LastSevenDays { get; set; }
        public EntryStats PastWeek { get; set; }
        public EntryStats CurrentDay { get; set; }

    }
}
