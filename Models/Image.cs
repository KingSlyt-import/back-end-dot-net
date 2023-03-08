using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageLink { get; set; }
        [Required]
        public string Meta { get; set; }
        public bool Hide { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
