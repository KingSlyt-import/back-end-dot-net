using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Back_End_Dot_Net.Models
{
    public class Chipset
    {
        // Overview
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Manufacture { get; set; }

        public ChipsetType Type { get; set; }

        [DisplayName("CPU Socket")]
        public ChipsetSocket CPUSocket { get; set; }

        [DisplayName("CPU Temperature")]
        public int CPUTemp { get; set; }

        [DisplayName("Thermal Design Power")]
        public int TDP { get; set; }

        // Performance features
        [DisplayName("Base CPU Speed")]
        public double CpuSpeedBase { get; set; }

        [DisplayName("Boost CPU Speed")]
        public double CpuSpeedBoost { get; set; }

        [DisplayName("CPU Thread")]
        public double CpuThread { get; set; }

        [DisplayName("Semiconductor size")]
        public double semiconductorSize { get; set; }

        [DisplayName("Benchmark Result")]
        public double Benchmark { get; set; }

        [DisplayName("PCIe")]
        public int Pci { get; set; }

        [DisplayName("Memory channels")]
        public int Memory { get; set; }

        [DisplayName("Performance Features")]
        public ChipsetPerformanceFeatures ChipsetPerformanceFeatures { get; set; }

        // RAM Support
        [DisplayName("DDR Version")]
        public ChipsetRAMVersion ChipsetRAMVersion { get; set; }

        [DisplayName("RAM Speed")]
        public int RAMSpeed { get; set; }

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
