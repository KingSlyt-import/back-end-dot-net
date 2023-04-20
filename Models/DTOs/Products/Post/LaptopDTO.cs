using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Back_End_Dot_Net.Models;

namespace Back_End_Dot_Net.DTOs
{
    public class LaptopDTO 
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Manufacture { get; set; }

        // Performance properties
        [DisplayName("CPU ID")]
        public Guid? CpuId { get; set; }

        [DisplayName("RAM")]
        public int Ram { get; set; }

        [DisplayName("RAM Speed")]
        public int RamSpeed { get; set; }

        [DisplayName("Internal Storage")]
        public int InStorage { get; set; }

        [DisplayName("Performance Feature")]
        public IEnumerable<string>? PerformanceFeatures { get; set; }

        // Screen Properties
        [DisplayName("Screen Size")]
        public double ScreenSize { get; set; }

        [DisplayName("Screen Resolution")]
        public string? Resolution { get; set; }

        [DisplayName("Refresh Rate")]
        public int ScreenHz { get; set; }

        [DisplayName("Display Brightness")]
        public int Nits { get; set; }

        [DisplayName("Pixels Per Inch")]
        public int Ppi { get; set; }

        [DisplayName("Screen Feature")]
        public IEnumerable<string>? ScreenFeatures { get; set; }

        // Design Properties
        public double Weight { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        [DisplayName("Design Feature")]
        public IEnumerable<string>? DesignFeatures { get; set; }

        // Battery properties
        [DisplayName("Battery Power")]
        public int BatteryPower { get; set; }

        [DefaultValue(false)]
        public bool MagSafe { get; set; } = false;

        // Features
        [DisplayName("Other Feature")]
        public IEnumerable<string>? Features { get; set; }
    }
}