using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Back_End_Dot_Net.Models
{
    public class Laptop
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        public string Manufacture { get; set; }
        [DisplayName("CPU")]
        public string Cpu { get; set; }
        [DisplayName("Base CPU Speed")]
        public double CpuSpeedBase { get; set; }
        [DisplayName("Boost CPU Speed")]
        public double CpuSpeedBoost { get; set; }
        [DisplayName("RAM")]
        public int Ram { get; set; }
        [DisplayName("RAM Speed")]
        public int RamSpeed { get; set; }
        [DisplayName("Screen Resolution")]
        public string Resolution { get; set; }
        [DisplayName("Pixels Per Inch")]
        public int Ppi { get; set; }
        public double Weight { get; set; }
        [DisplayName("Internal Storage")]
        public int InStorage { get; set; }
        [DisplayName("Screen Size")]
        public int ScreenSize { get; set; }
        public string Meta { get; set; }
        [DefaultValue(false)]
        public bool Hide { get; set; } = false;
        [DisplayName("Access Time")]
        public int AccessTime { get; set; } = 0;
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
