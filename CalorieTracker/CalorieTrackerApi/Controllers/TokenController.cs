using AutoMapper;
using CalorieTrackerApi.Authentication;
using CalorieTrackerApi.Dtos;
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
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TokenController(ILogger<TokenController> logger, ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<TokenController>
        [HttpGet]
        [AdminAccessRequired]
        public IEnumerable<TokenDto> Get()
        {
            return _tokenService.GetUserTokens();
        }

        // GET api/<TokenController>/5
        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            try
            {
                var actResult = _tokenService.GetUserToken(guid);
                if (!actResult.Item1)
                {
                    return BadRequest("Token doesn't exist");
                }
                return Ok(actResult.Item2);
            }
            catch (Exception ex)
            {
                var message = "Get Token failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }
        }

        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateTokenDto tokenDto)
        {
            IActionResult result = Ok();
            try
            {
                var actResult = _tokenService.CreateUserToken(tokenDto);
                if (!actResult.Item1)
                {
                    return BadRequest(actResult.Item2);
                }
            }
            catch (Exception ex)
            {
                var message = "Create User Token failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }

        // PUT api/<TokenController>/5
        [HttpPut]
        public IActionResult Put([FromBody] CreateTokenDto tokenDto)
        {
            IActionResult result = Ok();
            try
            {
                var actResult = _tokenService.RefreshUserToken(tokenDto);
                if (!actResult.Item1)
                {
                    return BadRequest(actResult.Item2);
                }
            }
            catch (Exception ex)
            {
                var message = "Update User Token failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }

        // DELETE api/<TokenController>/5
        [HttpDelete("{guid}")]
        [AdminAccessRequired]
        public IActionResult Delete(Guid guid)
        {
            IActionResult result = Ok();
            try
            {
                var actResult = _tokenService.DeleteUserToken(guid);
                if (!actResult.Item1)
                {
                    return BadRequest(actResult.Item2);
                }
            }
            catch (Exception ex)
            {
                var message = "Delete User Token failed";
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return result;
        }
    }
}
