using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models.DTO
{
    public class RegisterUser
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
