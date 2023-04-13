using System.ComponentModel.DataAnnotations;

public enum FeaturesType
{
    [Display(Name = "Performance")]
    Performance,
    [Display(Name = "Screen")]
    Screen,
    [Display(Name = "Design")]
    Design,
    [Display(Name = "Default")]
    Default
}

public enum FeaturesCategory
{
    [Display(Name = "Chipset")]
    Chipset,
    [Display(Name = "Phone")]
    Phone,
    [Display(Name = "Laptop")]
    Laptop
}