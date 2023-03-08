using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public bool Hide { get; set; } = false;
        public int AccessTime { get; set; } = 0;

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
