using CalorieTrackerApi.Authentication;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalorieTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AdminAccessRequired]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger _logger;

        public ReportController(ILogger<ReportController> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        // GET: api/<ReportController>
        [HttpGet]
        public IEnumerable<ReportDto> Get()
        {
            return _reportService.GetUserReports();
        }

        // GET api/<ReportController>/5
        [HttpGet("{userName}")]
        public ReportDto Get(string userName)
        {
            return _reportService.GetUserReports(userName)[0];
        }
    }
}
