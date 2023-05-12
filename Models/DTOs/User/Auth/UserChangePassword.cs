using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.DTOs
{
    public class UserChangePasswordDTO
    {
        [Required]
        public string? CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string? NewPassword { get; set; }
    }
}
