using System.ComponentModel.DataAnnotations;

namespace MP_API.Models.InputModels
{
    public class RegisterIM
    {
        [Required]
        public string Username { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        public bool Admin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
