using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.DTO
{
    public class RegisterDto
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
