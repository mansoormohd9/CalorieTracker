using CalorieTrackerApi.Authentication;
using CalorieTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public FoodEntryController(IFoodEntryService foodEntryService)
        {
            _foodEntryService = foodEntryService;
        }

        // GET: api/<FoodEntryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FoodEntryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FoodEntryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FoodEntryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FoodEntryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
