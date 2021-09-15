using CalorieTracker.Authentication;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalorieTracker.Controllers
{
    [AdminUiAccessRequired]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: ReportController
        public ActionResult Index()
        {
            return View(_reportService.GetUserReports());
        }

        // GET: ReportController/Details/5
        public ActionResult Details(string userName)
        {
            return View(_reportService.GetUserReports(userName)[0]);
        }
    }
}
