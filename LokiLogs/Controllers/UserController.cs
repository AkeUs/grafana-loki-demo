using System;
using LokiLogs.Models;
using LokiLogs.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LokiLogs.Controllers {
    
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _repository;

        public UserController(ILogger<UserController> logger, UserRepository repository) {
            _logger = logger;
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult GetUsers() {
            var httpCode = RandomHttpStatus();
            switch (httpCode) {
                case 400:
                    _logger.LogWarning("Error from client");
                    return BadRequest();
                case 500:
                    _logger.LogError("An exception occurred");
                    throw new Exception();
                default: {
                    var users = _repository.GetAll();
                    return Ok(users);
                }
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUsers(Guid id) {
            var httpCode = RandomHttpStatus();
            switch (httpCode) {
                case 400:
                    _logger.LogWarning("Error from client");
                    return BadRequest();
                case 500:
                    _logger.LogError("An exception occurred");
                    throw new Exception();
                default: {
                    var user = _repository.GetById(id);
                    if (user is null) {
                        _logger.LogInformation("User Not Found: {id}", id);
                        return NotFound();
                    }
                    return Ok(user);
                }
            }
        }
        
        [HttpPost]
        public IActionResult SaveUser([FromBody] UserDTO userDto) {
            var httpCode = RandomHttpStatus();
            switch (httpCode) {
                case 400:
                    _logger.LogWarning("Error from client");
                    return BadRequest();
                case 500:
                    _logger.LogError("An exception occurred");
                    throw new Exception();
                default: {
                    if (!ModelState.IsValid) {
                        _logger.LogError("Invalid Request");
                        return BadRequest(ModelState);
                    }
            
                    var user = new User {
                        Id = Guid.NewGuid(),
                        Name = userDto.Name,
                        Type = UserType.Normal,
                        Age = userDto.Age
                    };
            
                    _repository.Add(user);
                    return Ok(user);
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveUser(Guid id) {
            var httpCode = RandomHttpStatus();
            switch (httpCode) {
                case 400:
                    _logger.LogWarning("Error from client");
                    return BadRequest();
                case 500:
                    _logger.LogError("An exception occurred");
                    throw new Exception();
                default: {
                    var user = _repository.GetById(id);
                    if (user is null) {
                        _logger.LogInformation("User Not Found: {id}", id);
                        return NotFound();
                    }
                    _repository.Remove(id);
                    return Ok(user);
                }
            }
        }

        private static int RandomHttpStatus() {
            int[] httpStatus = { 200, 400, 500, 200, 400, 500, 200, 400, 500, 200};
            var index = new Random().Next(httpStatus.Length);
            return httpStatus[index];
        }
        
    }
}