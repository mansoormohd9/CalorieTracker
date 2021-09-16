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

        // GET: api/<FoodEntryController>
        [HttpGet]
        [Route("api/foodEntry/filter")]
        public IEnumerable<FoodEntryDto> Get(FoodEntryFilter foodEntryFilter)
        {
            return _foodEntryService.GetFoodEntries(Utils.GetUsernameFromContext(HttpContext), foodEntryFilter);
        }

        // GET api/<FoodEntryController>/5
        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            try
            {
                var actResult = _foodEntryService.GetFoodEntry(Utils.GetUsernameFromContext(HttpContext), guid);
                if (!actResult.Item1)
                {
                    return BadRequest("FoodEntry doesn't exist");
                }
                return Ok(actResult.Item2);
            }
            catch (Exception ex)
            {
                var message = "Get FoodEntry failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }
        }

        // POST api/<FoodEntryController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateFoodEntryDto foodEntryDto)
        {
            IActionResult result = Ok();
            try
            {
                var createResult = _foodEntryService.CreateFoodEntry(Utils.GetUsernameFromContext(HttpContext), foodEntryDto);
                if (!createResult.Item1)
                {
                    return BadRequest(createResult.Item2);
                }
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
        public IActionResult Put([FromBody] UpdateFoodEntryDto foodEntryDto)
        {
            IActionResult result = Ok();
            try
            {
                var updateResult = _foodEntryService.UpdateFoodEntry(Utils.GetUsernameFromContext(HttpContext), foodEntryDto);
                if (!updateResult.Item1)
                {
                    return BadRequest(updateResult.Item2);
                }
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
                var deleteResult = _foodEntryService.DeleteFoodEntry(Utils.GetUsernameFromContext(HttpContext), guid);
                if (!deleteResult.Item1)
                {
                    return BadRequest(deleteResult.Item2);
                }
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
