using AutoMapper;
using CalorieTrackerApi.Authentication;
using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Helpers;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalorieTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorizationRequired]
    public class FoodEntryController : ControllerBase
    {
        private readonly IFoodEntryService _foodEntryService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public FoodEntryController(ILogger<FoodEntryController> logger, IFoodEntryService foodEntryService, IMapper mapper)
        {
            _foodEntryService = foodEntryService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<FoodEntryController>
        [HttpGet]
        public IEnumerable<FoodEntryDto> Get()
        {
            return _foodEntryService.GetFoodEntries(Utils.GetUsernameFromContext(HttpContext));
        }

        // GET api/<FoodEntryController>/5
        [HttpGet("{guid}")]
        public FoodEntryDto Get(Guid guid)
        {
            return _foodEntryService.GetFoodEntry(Utils.GetUsernameFromContext(HttpContext), guid);
        }

        // POST api/<FoodEntryController>
        [HttpPost]
        public IActionResult Post([FromBody] FoodEntryDto foodEntryDto)
        {
            IActionResult result = Ok();
            try
            {
                _foodEntryService.CreateFoodEntry(Utils.GetUsernameFromContext(HttpContext), _mapper.Map<FoodEntry>(foodEntryDto));
            }
            catch (Exception ex)
            {
                var message = "Create Food Entry failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }

        // PUT api/<FoodEntryController>/5
        [HttpPut]
        public IActionResult Put([FromBody] FoodEntryDto foodEntryDto)
        {
            IActionResult result = Ok();
            try
            {
                _foodEntryService.UpdateFoodEntry(Utils.GetUsernameFromContext(HttpContext), _mapper.Map<FoodEntry>(foodEntryDto));
            }
            catch (Exception ex)
            {
                var message = "Update Food Entry failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }

        // DELETE api/<FoodEntryController>/5
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult result = Ok();
            try
            {
                _foodEntryService.DeleteFoodEntry(Utils.GetUsernameFromContext(HttpContext), guid);
            }
            catch (Exception ex)
            {
                var message = "Delete Food Entry failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }
    }
}
