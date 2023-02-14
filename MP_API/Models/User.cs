using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MP_API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        public bool Admin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
