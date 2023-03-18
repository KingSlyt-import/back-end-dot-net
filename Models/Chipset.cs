using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Back_End_Dot_Net.Models
{
    public class Chipset
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        public string Manufacture { get; set; }
        [DisplayName("CPU Speed")]
        public string CpuSpeed { get; set; }
        [DisplayName("CPU Thread")]
        public double CpuThread { get; set; }
        [DisplayName("Turbo Clock Speed")]
        public double MaxCpuSpeed { get; set; }
        [DisplayName("Semiconductor size")]
        public double NanometNumber { get; set; }
        [DisplayName("Benchmark Result")]
        public double Benchmark { get; set; }
        [DisplayName("Peripheral Component Interconnect Express (PCIe)")]
        public int Pci { get; set; }
        [DisplayName("Memory channels")]
        public int Memory { get; set; }
        public string Meta { get; set; }
        [DefaultValue(false)]
        public bool Hide { get; set; } = false;
        [DisplayName("Access Time")]
        public int AccessTime { get; set; } = 0;
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
