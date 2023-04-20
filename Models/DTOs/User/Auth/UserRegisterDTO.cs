using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        public string? Avatar { get; set; }
    }
}
