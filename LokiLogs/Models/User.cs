using System;

namespace LokiLogs.Models {
    public class User {
        
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public UserType Type { get; set; }
        public int Age { get; set; }
        
    }
}