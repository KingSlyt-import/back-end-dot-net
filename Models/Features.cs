using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.Models
{
    public class Features
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Type { get; set; }

        public string? Category { get; set; }

        public string? Description { get; set; }
    }
}