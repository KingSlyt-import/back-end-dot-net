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
        [DisplayName("Manufacture")]
        public string Maunufacture { get;set; }

        [DisplayName("Battery Power")]
        public int BatteryPower { get; set; }

        [DisplayName("Pixels Per Inch")]
        public int Ppi { get; set; }

        [DisplayName("RAM")]
        public int Ram { get; set; }

        [DisplayName("Screen Resolution")]
        public int Resolution { get; set; }

        [DisplayName("CPU")]
        public string Cpu { get; set; }

        [DisplayName("CPU Speed")]
        public string CpuHz { get; set; }

        [DisplayName("Refresh Rate")]
        public int ScreenHz { get; set; }

        [DisplayName(" Display Brightness")]
        public int Nits { get; set; }

        [DisplayName("Internal Storage")]
        public int InStorage { get; set; }

        [DisplayName("Main Camera Megapixels")]
        public string MainCameraMP { get; set; }
        [DisplayName("Front Camera Megapixels")]
        public int FrontCameraMP { get; set; }
        [DisplayName("Charging Speed")]
        public int Charging { get; set; }
        [DisplayName("Screen Size")]
        public string ScreenSize { get; set; }
        public string Meta { get; set; }
        public bool Hide { get; set; } = false;
        public int AccessTime { get; set; } = 0;
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
