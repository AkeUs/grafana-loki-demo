using System.ComponentModel.DataAnnotations;

namespace LokiLogs.Models {
    public class UserDTO {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(18, 99)]
        public int Age { get; set; }
    }
}