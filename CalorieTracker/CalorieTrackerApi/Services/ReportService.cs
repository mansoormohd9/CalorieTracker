using AutoMapper;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTrackerApi.Services
{
    public class ReportService: IReportService
    {
        private readonly IFoodEntryRepo _foodEntryRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public ReportService(IFoodEntryRepo foodEntryRepo, IUserRepo userRepo, IMapper mapper)
        {
            _foodEntryRepo = foodEntryRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        private class TempStat
        {
            public string UserName { get; set; }
            public int EntriesAdded { get; set; }
            public float CaloriesConsumed { get; set; }
            public double AverageCalories { get; set; }
        }

        public List<ReportDto> GetUserReports(string userName = null)
        {
            var baseDate = DateTime.Today;
            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            var lastWeekStart = thisWeekStart.AddDays(-7);
            var lastWeekEnd = thisWeekStart.AddSeconds(-1);

            var thisWeekStats = GetTempStats(thisWeekStart, thisWeekEnd, userName);
            var lastWeekStats = GetTempStats(lastWeekStart, lastWeekEnd, userName);
            var todayStats = GetTempStats(today, today, userName);

            var users = userName == null ? _userRepo.GetUsers() : new List<User> { _userRepo.GetUser(userName) };
            var userReportDict = new Dictionary<string, ReportDto>();

            foreach(var user in users)
            {
                userReportDict.Add(user.UserName, new ReportDto
                {
                    UserName = user.UserName
                });
            }

            //Past Week
            foreach(var entry in lastWeekStats)
            {
                var curRep = userReportDict[entry.UserName];
                curRep.PastWeek = new EntryStats
                {
                    FoodEntriesAdded = entry.EntriesAdded,
                    CaloriesConsumed = entry.CaloriesConsumed,
                    AverageCaloriesConsumed = entry.AverageCalories,
                };
            }

            //Current Week
            foreach (var entry in thisWeekStats)
            {
                var curRep = userReportDict[entry.UserName];
                curRep.CurrentWeek = new EntryStats
                {
                    FoodEntriesAdded = entry.EntriesAdded,
                    CaloriesConsumed = entry.CaloriesConsumed,
                    AverageCaloriesConsumed = entry.AverageCalories,
                };
            }

            //TOday
            foreach (var entry in todayStats)
            {
                var curRep = userReportDict[entry.UserName];
                curRep.CurrentDay = new EntryStats
                {
                    FoodEntriesAdded = entry.EntriesAdded,
                    CaloriesConsumed = entry.CaloriesConsumed,
                    AverageCaloriesConsumed = entry.AverageCalories,
                };
            }

            return userReportDict.Values.ToList();
        }

        private IEnumerable<TempStat> GetTempStats(DateTime startDate, DateTime endDate, string userName = null)
        {
            var foodEntries = userName == null ? _foodEntryRepo.GetFoodEntries(startDate, endDate) : _foodEntryRepo.GetFoodEntries(userName, startDate, endDate);
            var weekStats = foodEntries.GroupBy(x => x.User.UserName)
                                            .Select(s => new TempStat
                                            {
                                                UserName = s.Key,
                                                EntriesAdded = s.Count(),
                                                CaloriesConsumed = s.Sum(c => c.Calories),
                                                AverageCalories = s.Average(c => c.Calories)
                                            });

            return weekStats;
        }
    }
}
