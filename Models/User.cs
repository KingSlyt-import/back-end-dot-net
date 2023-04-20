using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Back_End_Dot_Net.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public byte[]? Password { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? Avatar { get; set; }

        public string Role { get; set; } = "User"; // default value set to "User"

        [DefaultValue(false)]
        [JsonIgnore]
        public bool Hide { get; set; } = false;

        [DisplayName("Created Date")]
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
