using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Back_End_Dot_Net.Models
{
    public class Phone
    {
        // Overview
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string? Image { get; set; }

        public string? Description { get; set; }

        [DisplayName("Manufacture")]
        public string? Manufacture { get; set; }

        // Performance Properties
        [DisplayName("CPU Name")]
        public string? CPUName { get; set; }

        [DisplayName("CPU Type")]
        public string? CPUType { get; set; }

        [DisplayName("Base CPU Speed")]
        public double CPUSpeedBase { get; set; }

        [DisplayName("Boost CPU Speed")]
        public double CPUSpeedBoost { get; set; }

        [DisplayName("RAM")]
        public int RAM { get; set; }

        [DisplayName("RAM Speed")]
        public int RAMSpeed { get; set; }

        [DisplayName("Internal Storage")]
        public int InStorage { get; set; }

        [DisplayName("Performance Feature")]
        public PhonePerformanceFeatures PerformanceFeatures { get; set; }

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
        public PhoneScreenFeatures ScreenFeatures { get; set; }

        // Design Properties
        public double Weight { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        [DisplayName("Design Feature")]
        public PhoneDesignFeatures DesignFeatures { get; set; }

        // Camera properties
        [DisplayName("Main Camera Megapixels")]
        public string? MainCameraMP { get; set; }

        [DisplayName("Front Camera Megapixels")]
        public int FrontCameraMP { get; set; }

        // Battery properties
        [DisplayName("Battery Power")]
        public int BatteryPower { get; set; }

        [DisplayName("Charging Speed")]
        public int Charging { get; set; }

        [DefaultValue(false)]
        public bool MagSafe { get; set; } = false;

        // Features
        [DisplayName("Other Feature")]
        public PhoneFeatures Features { get; set; }

        // Others
        [DefaultValue(false)]
        [JsonIgnore]
        public bool Hide { get; set; } = false;

        [DisplayName("Access Time")]
        [JsonIgnore]
        public int AccessTime { get; set; } = 0;

        [DisplayName("Created Date")]
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}