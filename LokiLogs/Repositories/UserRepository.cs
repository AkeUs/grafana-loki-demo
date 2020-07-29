using System;
using System.Collections.Generic;
using LokiLogs.Models;
using Microsoft.Extensions.Logging;

namespace LokiLogs.Repositories {
    
    public class UserRepository {

        private readonly List<User> _users;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger) {
            _users = new List<User>();
            _logger = logger;
        }

        public void Add(User user) {
            _users.Add(user);
            _logger.LogInformation("New user: {@user}", user);
        }

        public void Remove(Guid id) {
            var user = _users.Find(u => u.Id == id);
            _users.Remove(user);
            _logger.LogInformation("Remove user: {@user}", user);
        }

        public User GetById(Guid id) {
            var user = _users.Find(u => u.Id == id);
            return user;
        }
        
        public List<User> GetAll() {
            return _users;
        }
    }
}