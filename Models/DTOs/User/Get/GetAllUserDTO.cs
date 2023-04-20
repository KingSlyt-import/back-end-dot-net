using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.DTOs
{
    public class GetAllUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
    }
}
