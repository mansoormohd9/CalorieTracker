using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Dtos
{
    public class DateReportDto
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public EntryStats EntryStats { get; set; }
        public bool DailyLimitReached { get; set; }
    }
}
