using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
    }
}
