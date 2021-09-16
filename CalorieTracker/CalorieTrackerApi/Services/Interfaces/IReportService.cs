using CalorieTrackerApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services.Interfaces
{
    public interface IReportService
    {
        List<ReportDto> GetUserReports(string userName = null);
        IEnumerable<DateReportDto> GetUserReportGroupedByDate(string userName);
    }
}
