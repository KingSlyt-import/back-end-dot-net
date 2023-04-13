using System.ComponentModel;

public enum ChipsetType
{
    Laptop,
    Desktop,
    Phone,
    All
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
    sTRX4,
    FCBGA1744,
    FP6,
    BGA
}

public enum ChipsetRAMVersion
{
    DDR3,
    DDR4,
    LPDDR4X,
    DDR5,
}

public enum ChipsetPerformanceFeatures
{
    [Description("Unlocked Multiplier")]
    UnlockedMultiplier,
    [Description("Big.LITTLE Technology")]
    BigLittleTechnology,
    [Description("Neural Engine")]
    NeuralEngine,
    [Description("Unified Memory Architecture")]
    UnifiedMemoryArchitecture,
    HMP,
}