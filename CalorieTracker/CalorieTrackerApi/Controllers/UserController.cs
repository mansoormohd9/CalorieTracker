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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
                                                   
        public UserController(ILogger<UserController> logger,IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return _userService.GetUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{userName}")]
        public UserDto Get(string userName)
        {
            return _userService.GetUser(userName);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDto user)
        {
            IActionResult result = Ok();
            try
            {
                _userService.CreateUser(_mapper.Map<User>(user));
            }
            catch (Exception ex)
            {
                var message = "Create User failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [AdminAccessRequired]
        public IActionResult Put([FromBody] UserDto user)
        {
            IActionResult result = Ok();
            try
            {
                _userService.UpdateUser(_mapper.Map<User>(user));
            }
            catch (Exception ex)
            {
                var message = "Update User failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{userName}")]
        [AdminAccessRequired]
        public IActionResult Delete(string userName)
        {
            IActionResult result = Ok();
            try
            {
                _userService.DeleteUser(userName);
            }
            catch (Exception ex)
            {
                var message = "Delete User failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }
    }
}
