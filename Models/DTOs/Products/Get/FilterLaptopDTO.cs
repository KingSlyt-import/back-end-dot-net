using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Back_End_Dot_Net.DTOs
{
    public class FilterLaptopDTO
    {
        [Required]
        public double Price { get; set; }

        public string? Manufacture { get; set; }

        [DisplayName("RAM")]
        public int Ram { get; set; }

        [DisplayName("RAM Speed")]
        public int RamSpeed { get; set; }

        [DisplayName("Internal Storage")]
        public int InStorage { get; set; }

        // Screen Properties
        [DisplayName("Screen Size")]
        public double ScreenSize { get; set; }

        [DisplayName("Screen Resolution")]
        public string? Resolution { get; set; }

        [DisplayName("Refresh Rate")]
        public int ScreenHz { get; set; }

        [DisplayName("Display Brightness")]
        public int Nits { get; set; }

        // Design Properties
        public double Weight { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        // Battery properties
        [DisplayName("Battery Power")]
        public int BatteryPower { get; set; }

        [DefaultValue(false)]
        public bool MagSafe { get; set; } = false;
    }
}
