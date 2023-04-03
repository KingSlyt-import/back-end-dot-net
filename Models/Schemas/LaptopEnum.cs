using System.ComponentModel.DataAnnotations;

public enum LaptopCPUType 
{
    AMD,
    Intel
}

public enum LaptopPerformanceFeatures
{
    NVMe,
    VRAM,
    MultiThreading,
    [Display(Name = "64-bit Storage")]
    Bit64Storage
}

public enum LaptopScreenFeatures
{
    [Display(Name = "Touch Screen")]
    TouchScreen,
    [Display(Name = "Anti-reflection Coating")]
    AntiReflectionCoating
}

public enum LaptopDesignFeatures
{
    [Display(Name = "Fanless Design")]
    FanlessDesign,
    [Display(Name = "Backlit Keyboard")]
    BacklitKeyboard,
    [Display(Name = "Water Sealed")]
    WaterSealed,
    [Display(Name = "Dust Resistance")]
    DustResistance
}

public enum LaptopFeatures
{
    [Display(Name = "Stereo Speakers")]
    StereoSpeakers,
    [Display(Name = "3.5mm Audio Jack")]
    AudioJack,
    [Display(Name = "Front Camera Megapixels")]
    MegapixelsFront,
    [Display(Name = "Back Camera Megapixels")]
    MegapixelsBack,
    [Display(Name = "Dolby Atmos")]
    DolbyAtmos,
    [Display(Name = "Stylus Included")]
    StylusIncluded,
    [Display(Name = "Finger Print Scanner")]
    FingerPrintScanner,
    [Display(Name = "Facial Recognition")]
    FacialRecognition,
    [Display(Name = "Voice Commands")]
    VoiceCommands,
    [Display(Name = "Front Camera")]
    FrontCamera,
    [Display(Name = "S/PDIF Out Port")]
    SPDIFOutport,
    [Display(Name = "Optical Disc Drive")]
    OpticalDiscDrive,
    Gyroscope,
    GPS,
    Accelerometer,
    Compass,
}