using System.ComponentModel.DataAnnotations;

public enum PhonePerformanceFeatures
{
    [Display(Name = "Heterogeneous Multiprocessing")]
    HMP,
    [Display(Name = "Advanced Encryption Standard")]
    AES,
    [Display(Name = "Big.LITTLE Technology")]
    BigLittleTechnology,
    [Display(Name = "External Memory Slot")]
    ExternalMemorySlot,
    [Display(Name = "Integrated Graphics")]
    IntegratedGraphics,
    [Display(Name = "Integrated LTE")]
    IntegratedLTE,
    [Display(Name = "64-bit Support")]
    Support64Bit,
    [Display(Name = "Trust Zone")]
    TrustZone,
    [Display(Name = "NX Bit")]
    NXBit,
    [Display(Name = "AI Neural Engine")]
    AINeuralEngine,
    [Display(Name = "Advanced Image Signal Processor")]
    AdvancedImageSignalProcessor,
    [Display(Name = "Secure Enclave")]
    SecureEnclave,
    MultiThreading,
}

public enum PhoneScreenFeatures
{
    [Display(Name = "Damage Resistant Glass")]
    DamageResistantGlass,
    [Display(Name = "Sapphire Glass Display")]
    SapphireGlassDisplay,
    [Display(Name = "Always On Display")]
    AlwaysOnDisplay,
    [Display(Name = "Dynamic AMOLED 2X")]
    DynamicAMOLED2X,
    [Display(Name = "Secondary Screen")]
    SecondaryScreen,
    [Display(Name = "Curved Display")]
    CurvedDisplay,
    [Display(Name = "Paper Display")]
    PaperDisplay,
    [Display(Name = "Dolby Vision")]
    DolbyVision,
    [Display(Name = "3D Display")]
    Display3D,
    [Display(Name = "HDR 10+")]
    HDR10Plus,
    [Display(Name = "HDR 10")]
    HDR10,
    [Display(Name = "Ceramic Shield Front Cover")]
    CeramicShieldFrontCover,
    [Display(Name = "Super Retina XDR Display")]
    SuperRetinaXDRDisplay,
    [Display(Name = "ProMotion Technology")]
    ProMotionTechnology
}

public enum PhoneDesignFeatures
{
    [Display(Name = "Physical Keyboard")]
    PhysicalKeyboard,
    [Display(Name = "Backlit Keyboard")]
    BacklitKeyboard,
    [Display(Name = "Dust Resistance")]
    DustResistance,
    [Display(Name = "Fanless Design")]
    FanlessDesign,
    [Display(Name = "Ceramic Shield")]
    CeramicShield,
    [Display(Name = "Water Sealed")]
    WaterSealed,
    Foldable
}

public enum PhoneFeatures
{
    [Display(Name = "Galileo Satellite Navigation")]
    GalileoSatelliteNavigation,
    [Display(Name = "Emergency SOS via satellite")]
    EmergencySatellite,
    [Display(Name = "Near Field Communication")]
    NFC,
    [Display(Name = "Fingerprint Scanner")]
    FingerprintScanner,
    [Display(Name = "DLNA Certification")]
    DLNACertification,
    [Display(Name = "Heart Rate Monitor")]
    HeartRateMonitor,
    [Display(Name = "Wi-Fi Connectivity")]
    WiFiConnectivity,
    [Display(Name = "Facial Recognition")]
    FacialRecognition,
    [Display(Name = "Built In Projector")]
    BuiltInProjector,
    [Display(Name = "Wireless charging")]
    WirelessCharging,
    [Display(Name = "Optical Tracking")]
    OpticalTracking,
    [Display(Name = "Crash Detection")]
    CrashDetection,
    [Display(Name = "Infrared Sensor")]
    InfraredSensor,
    [Display(Name = "Cellular Module")]
    CellularModule,
    [Display(Name = "Stylus Included")]
    StylusIncluded,
    [Display(Name = "Motion Tracking")]
    MotionTracking,
    [Display(Name = "Iris Scanner")]
    IrisScanner,
    [Display(Name = "HDMI Output")]
    HDMIOutput,
    [Display(Name = "USB Type-C")]
    USBTypeC,
    [Display(Name = "5G Support")]
    Support5G,
    [Display(Name = "ANT Plus")]
    ANTPlus,
    Accelerometer,
    Barometer,
    Gyroscope,
    Compass,
    GPS
}