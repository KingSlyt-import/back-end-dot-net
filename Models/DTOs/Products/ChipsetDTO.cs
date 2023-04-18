using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Back_End_Dot_Net.DTOs
{
    public class ChipsetDTO
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Manufacture { get; set; }

        [EnumDataType(typeof(ChipsetType))]
        public ChipsetType Type { get; set; }

        [DisplayName("CPU Socket")]
        [EnumDataType(typeof(ChipsetSocket))]
        public ChipsetSocket CPUSocket { get; set; }

        [DisplayName("CPU Temperature")]
        public int CPUTemp { get; set; }

        [DisplayName("Thermal Design Power")]
        public int TDP { get; set; }

        [DisplayName("Base CPU Speed")]
        public double CpuSpeedBase { get; set; }

        [DisplayName("Boost CPU Speed")]
        public double CpuSpeedBoost { get; set; }

        [DisplayName("CPU Thread")]
        public double CpuThread { get; set; }

        [DisplayName("Semiconductor size")]
        public double semiconductorSize { get; set; }

        [DisplayName("PCIe")]
        public int Pci { get; set; }

        [DisplayName("Memory channels")]
        public int MemoryChannels { get; set; }

        [DisplayName("Performance Features")]
        public IEnumerable<string>? PerformanceFeatures { get; set; }

        [DisplayName("DDR Version")]
        [EnumDataType(typeof(ChipsetRAMVersion))]
        public ChipsetRAMVersion RAMVersion { get; set; }

        [DisplayName("RAM Speed")]
        public int RAMSpeed { get; set; }
    }
}