using System.ComponentModel.DataAnnotations;

public enum ChipsetType
{
    Laptop,
    Desktop
}

public enum ChipsetSocket
{
    LGA1150,
    LGA1155,
    AM5,
    AM4,
    TR4,
    LGA1151,
    LGA1200,
    LGA1700,
    LGA2066,
    sTRX4
}

public enum ChipsetRAMVersion
{
    DDR3,
    DDR4,
    DDR5
}

public enum ChipsetPerformanceFeatures
{
    [Display(Name = "Unlocked Multiplier")]
    UnlockedMultiplier,
    [Display(Name = "Big.LITTLE Technology")]
    BigLittleTechnology,
    HMP,
}