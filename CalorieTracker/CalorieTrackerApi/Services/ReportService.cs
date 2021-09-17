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
            public double CaloriesConsumed { get; set; }
            public double AverageCalories { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public List<ReportDto> GetUserReports(string userName = null)
        {
            var baseDate = DateTime.Today;
            var today = baseDate;
            var thisWeekStart = baseDate.AddDays(-6);
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
                    StartDate = entry.StartDate,
                    EndDate = entry.EndDate
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
                    StartDate = entry.StartDate,
                    EndDate = entry.EndDate
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
                    StartDate = entry.StartDate,
                    EndDate = entry.EndDate
                };
            }

            return userReportDict.Values.ToList();
        }

        public IEnumerable<DateReportDto> GetUserReportGroupedByDate(string userName)
        {
            var userCalorieLimit = _userRepo.GetUser(userName).CalorieLimit;
            var foodEntries = _foodEntryRepo.GetFoodEntries(userName, DateTime.MinValue, DateTime.MaxValue);
            var statsByDate = foodEntries.GroupBy(x => x.Date.Date)
                                            .Select(s => new DateReportDto
                                            {
                                                Date = s.Key,
                                                EntryStats = new EntryStats
                                                {
                                                    FoodEntriesAdded = s.Count(),
                                                    CaloriesConsumed = s.Sum(c => c.Calories),
                                                    AverageCaloriesConsumed = s.Average(c => c.Calories)
                                                },
                                                DailyLimitReached = s.Sum(c => c.Calories) >= userCalorieLimit
                                            });
            return statsByDate;
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
                                                AverageCalories = s.Average(c => c.Calories),
                                                StartDate = startDate.Date,
                                                EndDate = endDate.Date
                                            });

            return weekStats;
        }
    }
}
