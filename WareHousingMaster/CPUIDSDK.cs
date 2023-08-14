using System;
using System.Runtime.InteropServices;

public class CPUIDSDK
{
///////////////////////////////////////////////////////////////////////////
//	Error codes
///////////////////////////////////////////////////////////////////////////

    public const uint CPUIDSDK_ERROR_NO_ERROR =	0x00000000;

    public const uint CPUIDSDK_ERROR_EVALUATION	= 0x00000001;
    public const uint CPUIDSDK_ERROR_DRIVER	= 0x00000002;
    public const uint CPUIDSDK_ERROR_VM_RUNNING	= 0x00000004;
    public const uint CPUIDSDK_ERROR_LOCKED	= 0x00000008;
    public const uint CPUIDSDK_ERROR_INVALID_DLL = 0x00000010;

    public const uint CPUIDSDK_EXT_ERROR_EVAL_1	= 0x00000001;
    public const uint CPUIDSDK_EXT_ERROR_EVAL_2 = 0x00000002;

///////////////////////////////////////////////////////////////////////////
//	Configuration flags
///////////////////////////////////////////////////////////////////////////

    public const uint CPUIDSDK_CONFIG_USE_SOFTWARE = 0x00000002;
    public const uint CPUIDSDK_CONFIG_USE_DMI =	0x00000004;
    public const uint CPUIDSDK_CONFIG_USE_PCI =	0x00000008;
    public const uint CPUIDSDK_CONFIG_USE_ACPI = 0x00000010;
    public const uint CPUIDSDK_CONFIG_USE_CHIPSET = 0x00000020;
    public const uint CPUIDSDK_CONFIG_USE_SMBUS = 0x00000040;
    public const uint CPUIDSDK_CONFIG_USE_SPD = 0x00000080;
    public const uint CPUIDSDK_CONFIG_USE_STORAGE = 0x00000100;
    public const uint CPUIDSDK_CONFIG_USE_GRAPHICS = 0x00000200;
    public const uint CPUIDSDK_CONFIG_USE_HWMONITORING = 0x00000400;
    public const uint CPUIDSDK_CONFIG_USE_PROCESSOR = 0x00000800;
    public const uint CPUIDSDK_CONFIG_USE_DISPLAY_API = 0x00001000;
    public const uint CPUIDSDK_CONFIG_USE_ACPI_TIMER = 0x00004000;

    public const uint CPUIDSDK_CONFIG_UPDATE_PROCESSOR = 0x00010000;
    public const uint CPUIDSDK_CONFIG_UPDATE_GRAPHICS = 0x00020000;
    public const uint CPUIDSDK_CONFIG_UPDATE_STORAGE = 0x00040000;
    public const uint CPUIDSDK_CONFIG_UPDATE_LPCIO = 0x00080000;
    public const uint CPUIDSDK_CONFIG_UPDATE_DRAM = 0x00100000;

    public const uint CPUIDSDK_CONFIG_CHECK_VM = 0x01000000;
    public const uint CPUIDSDK_CONFIG_WAKEUP_HDD = 0x02000000;

    public const uint CPUIDSDK_CONFIG_SERVER_SAFE = 0x80000000;

    public const uint CPUIDSDK_CONFIG_USE_EVERYTHING = 0x7FFFFFFF;
    
///////////////////////////////////////////////////////////////////////////
//	Constants
///////////////////////////////////////////////////////////////////////////

    public static int I_UNDEFINED_VALUE = -1;
    public static float F_UNDEFINED_VALUE = -1.0f;

    public static uint MAX_INTEGER = 0xFFFFFFFF;
    public static float MAX_FLOAT = (float)MAX_INTEGER;

    public bool IS_F_DEFINED(float _f) { return (_f > 0.0f) ? true : false; }
    public bool IS_F_DEFINED(double _f) { return (_f > 0.0) ? true : false; }
    public bool IS_I_DEFINED(int _i) { return (_i == I_UNDEFINED_VALUE) ? false : true; }
    public bool IS_I_DEFINED(uint _i) { return (_i == (uint)I_UNDEFINED_VALUE) ? false : true; }
    public bool IS_I_DEFINED(short _i) { return (_i == (short)I_UNDEFINED_VALUE) ? false : true; }
    public bool IS_I_DEFINED(ushort _i) { return (_i == (ushort)I_UNDEFINED_VALUE) ? false : true; }
    public bool IS_I_DEFINED(sbyte _i) { return (_i == (sbyte)I_UNDEFINED_VALUE) ? false : true; }
    public bool IS_I_DEFINED(byte _i) { return (_i == (byte)I_UNDEFINED_VALUE) ? false : true; }


///////////////////////////////////////////////////////////////////////////
//	Devices classes
///////////////////////////////////////////////////////////////////////////

    public const uint CLASS_DEVICE_UNKNOWN = 0x00000000;
    public const uint CLASS_DEVICE_PCI = 0x00000001;
    public const uint CLASS_DEVICE_SMBUS = 0x00000002;
    public const uint CLASS_DEVICE_PROCESSOR = 0x00000004;
    public const uint CLASS_DEVICE_LPCIO = 0x00000008;
    public const uint CLASS_DEVICE_DRIVE = 0x00000010;
    public const uint CLASS_DEVICE_DISPLAY_ADAPTER = 0x00000020;
    public const uint CLASS_DEVICE_HID = 0x00000040;
    public const uint CLASS_DEVICE_BATTERY = 0x00000080;
    public const uint CLASS_DEVICE_EVBOT = 0x00000100;
    public const uint CLASS_DEVICE_NETWORK = 0x00000200;
    public const uint CLASS_DEVICE_MAINBOARD = 0x00000400;
    public const uint CLASS_DEVICE_MEMORY_MODULE = 0x00000800;
    public const uint CLASS_DEVICE_PSU = 0x00001000;

    public const uint CLASS_DEVICE_TYPE_MASK = 0x7FFFFFFF;
    public const uint CLASS_DEVICE_COMPOSITE = 0x80000000;

///////////////////////////////////////////////////////////////////////////
//	CPU manufacturers, archis & models
///////////////////////////////////////////////////////////////////////////

    public const uint CPU_MANUFACTURER_MASK = 0xFF000000;
    public const uint CPU_FAMILY_MASK = 0xFFFFFF00;
    public const uint CPU_MODEL_MASK = 0xFFFFFFFF;

    // Manufacturers
    public const uint CPU_UNKNOWN = 0x0;
    public const uint CPU_INTEL = 0x1000000;
    public const uint CPU_AMD = 0x2000000;
    public const uint CPU_CYRIX = 0x4000000;
    public const uint CPU_VIA = 0x8000000;
    public const uint CPU_TRANSMETA = 0x10000000;
    public const uint CPU_DMP = 0x20000000;
    public const uint CPU_UMC = 0x40000000;

    // Intel families
    public const uint CPU_INTEL_386 = CPU_INTEL + 0x100;
    public const uint CPU_INTEL_486 = CPU_INTEL + 0x200;
    public const uint CPU_INTEL_P5 = CPU_INTEL + 0x400;
    public const uint CPU_INTEL_P6 = CPU_INTEL + 0x800;
    public const uint CPU_INTEL_NETBURST = CPU_INTEL + 0x1000;
    public const uint CPU_INTEL_MOBILE = CPU_INTEL + 0x2000;
    public const uint CPU_INTEL_CORE = CPU_INTEL + 0x4000;
    public const uint CPU_INTEL_CORE_2 = CPU_INTEL + 0x8000;
    public const uint CPU_INTEL_BONNELL	= CPU_INTEL + 0x010000;
    public const uint CPU_INTEL_SALTWELL = CPU_INTEL + 0x010100;
    public const uint CPU_INTEL_SILVERMONT = CPU_INTEL + 0x010200;
    public const uint CPU_INTEL_GOLDMONT = CPU_INTEL + 0x010400;
    public const uint CPU_INTEL_NEHALEM = CPU_INTEL + 0x20000;
    public const uint CPU_INTEL_SANDY_BRIDGE = CPU_INTEL + 0x020100;
    public const uint CPU_INTEL_HASWELL = CPU_INTEL + 0x020200;
    public const uint CPU_INTEL_SKYLAKE = CPU_INTEL + 0x040000;
    public const uint CPU_INTEL_ITANIUM = CPU_INTEL + 0x100000;
    public const uint CPU_INTEL_ITANIUM_2 = CPU_INTEL + 0x100100;
    public const uint CPU_INTEL_MIC = CPU_INTEL + 0x200000;

    // Intel models
    // P5
    public const uint CPU_PENTIUM = CPU_INTEL_P5 + 0x1;
    public const uint CPU_PENTIUM_MMX = CPU_INTEL_P5 + 0x2;

    // P6
    public const uint CPU_PENTIUM_PRO = CPU_INTEL_P6 + 0x1;
    public const uint CPU_PENTIUM_2 = CPU_INTEL_P6 + 0x2;
    public const uint CPU_PENTIUM_2_M = CPU_INTEL_P6 + 0x3;
    public const uint CPU_CELERON_P2 = CPU_INTEL_P6 + 0x4;
    public const uint CPU_XEON_P2 = CPU_INTEL_P6 + 0x5;
    public const uint CPU_PENTIUM_3 = CPU_INTEL_P6 + 0x6;
    public const uint CPU_PENTIUM_3_M = CPU_INTEL_P6 + 0x7;
    public const uint CPU_PENTIUM_3_S = CPU_INTEL_P6 + 0x8;
    public const uint CPU_CELERON_P3 = CPU_INTEL_P6 + 0x9;
    public const uint CPU_XEON_P3 = CPU_INTEL_P6 + 0xA;

    // Netburst
    public const uint CPU_PENTIUM_4 = CPU_INTEL_NETBURST + 0x1;
    public const uint CPU_PENTIUM_4_M = CPU_INTEL_NETBURST + 0x2;
    public const uint CPU_PENTIUM_4_HT = CPU_INTEL_NETBURST + 0x3;
    public const uint CPU_PENTIUM_4_EE = CPU_INTEL_NETBURST + 0x4;
    public const uint CPU_CELERON_P4 = CPU_INTEL_NETBURST + 0x5;
    public const uint CPU_CELERON_D = CPU_INTEL_NETBURST + 0x6;
    public const uint CPU_XEON_P4 = CPU_INTEL_NETBURST + 0x7;
    public const uint CPU_PENTIUM_D = CPU_INTEL_NETBURST + 0x8;
    public const uint CPU_PENTIUM_XE = CPU_INTEL_NETBURST + 0x9;

    // Mobile
    public const uint CPU_PENTIUM_M = CPU_INTEL_MOBILE + 0x1;
    public const uint CPU_CELERON_M = CPU_INTEL_MOBILE + 0x2;

    // Core 1
    public const uint CPU_CORE_SOLO = CPU_INTEL_CORE + 0x1;
    public const uint CPU_CORE_DUO = CPU_INTEL_CORE + 0x2;
    public const uint CPU_CORE_CELERON_M = CPU_INTEL_CORE + 0x3;
    public const uint CPU_CORE_CELERON = CPU_INTEL_CORE + 0x4;

    // Core 2
    public const uint CPU_CORE_2_DUO = CPU_INTEL_CORE_2 + 0x1;
    public const uint CPU_CORE_2_EE = CPU_INTEL_CORE_2 + 0x2;
    public const uint CPU_CORE_2_XEON = CPU_INTEL_CORE_2 + 0x3;
    public const uint CPU_CORE_2_CELERON = CPU_INTEL_CORE_2 + 0x4;
    public const uint CPU_CORE_2_QUAD = CPU_INTEL_CORE_2 + 0x5;
    public const uint CPU_CORE_2_PENTIUM = CPU_INTEL_CORE_2 + 0x6;
    public const uint CPU_CORE_2_CELERON_DC = CPU_INTEL_CORE_2 + 0x7;
    public const uint CPU_CORE_2_SOLO = CPU_INTEL_CORE_2 + 0x8;

    // Bonnell
    public const uint CPU_BONNELL_ATOM = CPU_INTEL_BONNELL + 0x01;

    // Saltwell
    public const uint CPU_SALTWELL_ATOM = CPU_INTEL_SALTWELL + 0x01;

    // Silvermont
    public const uint CPU_SILVERMONT_ATOM =	CPU_INTEL_SILVERMONT + 0x01;
    public const uint CPU_SILVERMONT_CELERON = CPU_INTEL_SILVERMONT + 0x02;
    public const uint CPU_SILVERMONT_PENTIUM = CPU_INTEL_SILVERMONT + 0x03;
    public const uint CPU_SILVERMONT_ATOM_X7 = CPU_INTEL_SILVERMONT + 0x04;
    public const uint CPU_SILVERMONT_ATOM_X5 = CPU_INTEL_SILVERMONT + 0x05;
    public const uint CPU_SILVERMONT_ATOM_X3 = CPU_INTEL_SILVERMONT + 0x06;

    // Goldmont
    public const uint CPU_GOLDMONT_ATOM = CPU_INTEL_GOLDMONT + 0x01;
    public const uint CPU_GOLDMONT_CELERON = CPU_INTEL_GOLDMONT + 0x02;
    public const uint CPU_GOLDMONT_PENTIUM = CPU_INTEL_GOLDMONT + 0x03;

    // Nehalem
    public const uint CPU_NEHALEM_CORE_I7 = CPU_INTEL_NEHALEM + 0x1;
    public const uint CPU_NEHALEM_CORE_I7E = CPU_INTEL_NEHALEM + 0x2;
    public const uint CPU_NEHALEM_XEON = CPU_INTEL_NEHALEM + 0x3;
    public const uint CPU_NEHALEM_CORE_I3 = CPU_INTEL_NEHALEM + 0x4;
    public const uint CPU_NEHALEM_CORE_I5 = CPU_INTEL_NEHALEM + 0x5;
    public const uint CPU_NEHALEM_PENTIUM = CPU_INTEL_NEHALEM + 0x7;
    public const uint CPU_NEHALEM_CELERON = CPU_INTEL_NEHALEM + 0x8;

    // Sandybridge
    public const uint CPU_SANDY_BRIDGE_CORE_I7 = CPU_INTEL_SANDY_BRIDGE + 0x01;
    public const uint CPU_SANDY_BRIDGE_CORE_I7E = CPU_INTEL_SANDY_BRIDGE + 0x02;
    public const uint CPU_SANDY_BRIDGE_XEON	= CPU_INTEL_SANDY_BRIDGE + 0x03;
    public const uint CPU_SANDY_BRIDGE_CORE_I3 = CPU_INTEL_SANDY_BRIDGE + 0x04;
    public const uint CPU_SANDY_BRIDGE_CORE_I5 = CPU_INTEL_SANDY_BRIDGE + 0x05;
    public const uint CPU_SANDY_BRIDGE_PENTIUM = CPU_INTEL_SANDY_BRIDGE + 0x07;
    public const uint CPU_SANDY_BRIDGE_CELERON = CPU_INTEL_SANDY_BRIDGE + 0x08;

    // Haswell
    public const uint CPU_HASWELL_CORE_I7 = CPU_INTEL_HASWELL + 0x01;
    public const uint CPU_HASWELL_CORE_I7E = CPU_INTEL_HASWELL + 0x02;
    public const uint CPU_HASWELL_XEON = CPU_INTEL_HASWELL + 0x03;
    public const uint CPU_HASWELL_CORE_I3 = CPU_INTEL_HASWELL + 0x04;
    public const uint CPU_HASWELL_CORE_I5 = CPU_INTEL_HASWELL + 0x05;
    public const uint CPU_HASWELL_PENTIUM = CPU_INTEL_HASWELL + 0x07;
    public const uint CPU_HASWELL_CELERON = CPU_INTEL_HASWELL + 0x08;
    public const uint CPU_HASWELL_CORE_M = CPU_INTEL_HASWELL + 0x09;

    // Skylake
    public const uint CPU_SKYLAKE_XEON = CPU_INTEL_SKYLAKE + 0x01;
    public const uint CPU_SKYLAKE_CORE_I7 = CPU_INTEL_SKYLAKE + 0x02;
    public const uint CPU_SKYLAKE_CORE_I5 = CPU_INTEL_SKYLAKE + 0x03;
    public const uint CPU_SKYLAKE_CORE_I3 = CPU_INTEL_SKYLAKE + 0x04;
    public const uint CPU_SKYLAKE_PENTIUM = CPU_INTEL_SKYLAKE + 0x05;
    public const uint CPU_SKYLAKE_CELERON = CPU_INTEL_SKYLAKE + 0x06;
    public const uint CPU_SKYLAKE_CORE_M7 = CPU_INTEL_SKYLAKE + 0x07;
    public const uint CPU_SKYLAKE_CORE_M5 = CPU_INTEL_SKYLAKE + 0x08;
    public const uint CPU_SKYLAKE_CORE_M3 = CPU_INTEL_SKYLAKE + 0x09;
    public const uint CPU_SKYLAKE_CORE_I9EX = CPU_INTEL_SKYLAKE + 0x0A;
    public const uint CPU_SKYLAKE_CORE_I9X = CPU_INTEL_SKYLAKE + 0x0B;
    public const uint CPU_SKYLAKE_CORE_I7X = CPU_INTEL_SKYLAKE + 0x0C;
    public const uint CPU_SKYLAKE_CORE_I5X = CPU_INTEL_SKYLAKE + 0x0D;
    public const uint CPU_SKYLAKE_XEON_BRONZE =	CPU_INTEL_SKYLAKE + 0x0E;
    public const uint CPU_SKYLAKE_XEON_SILVER =	CPU_INTEL_SKYLAKE + 0x0F;
    public const uint CPU_SKYLAKE_XEON_GOLD	= CPU_INTEL_SKYLAKE + 0x10;
    public const uint CPU_SKYLAKE_XEON_PLATINIUM = CPU_INTEL_SKYLAKE + 0x11;
    public const uint CPU_SKYLAKE_PENTIUM_GOLD = CPU_INTEL_SKYLAKE + 0x12;


    // AMD families
    public const uint CPU_AMD_386 = CPU_AMD + 0x000100;
    public const uint CPU_AMD_486 = CPU_AMD + 0x000200;
    public const uint CPU_AMD_K5 = CPU_AMD + 0x000400;
    public const uint CPU_AMD_K6 = CPU_AMD + 0x000800;
    public const uint CPU_AMD_K7 = CPU_AMD + 0x001000;
    public const uint CPU_AMD_K8 = CPU_AMD + 0x002000;
    public const uint CPU_AMD_K10 = CPU_AMD + 0x004000;
    public const uint CPU_AMD_K12 = CPU_AMD + 0x010000;
    public const uint CPU_AMD_K14 = CPU_AMD + 0x020000;
    public const uint CPU_AMD_K15 = CPU_AMD + 0x040000;
    public const uint CPU_AMD_K16 = CPU_AMD + 0x080000;
    public const uint CPU_AMD_K17 = CPU_AMD + 0x100000;

    // AMD models
    // K5
    public const uint CPU_K5 = CPU_AMD_K5 + 0x01;
    public const uint CPU_K5_GEODE = CPU_AMD_K5 + 0x02;

    // K6
    public const uint CPU_K6 = CPU_AMD_K6 + 0x01;
    public const uint CPU_K6_2 = CPU_AMD_K6 + 0x02;
    public const uint CPU_K6_3 = CPU_AMD_K6 + 0x03;

    // K7
    public const uint CPU_K7_ATHLON = CPU_AMD_K7 + 0x01;
    public const uint CPU_K7_ATHLON_XP = CPU_AMD_K7 + 0x02;
    public const uint CPU_K7_ATHLON_MP = CPU_AMD_K7 + 0x03;
    public const uint CPU_K7_DURON = CPU_AMD_K7 + 0x04;
    public const uint CPU_K7_SEMPRON = CPU_AMD_K7 + 0x05;
    public const uint CPU_K7_SEMPRON_M = CPU_AMD_K7 + 0x06;

    // K8
    public const uint CPU_K8_ATHLON_64 = CPU_AMD_K8 + 0x01;
    public const uint CPU_K8_ATHLON_64_M = CPU_AMD_K8 + 0x02;
    public const uint CPU_K8_ATHLON_64_FX = CPU_AMD_K8 + 0x03;
    public const uint CPU_K8_OPTERON = CPU_AMD_K8 + 0x04;
    public const uint CPU_K8_TURION_64 = CPU_AMD_K8 + 0x05;
    public const uint CPU_K8_SEMPRON = CPU_AMD_K8 + 0x06;
    public const uint CPU_K8_SEMPRON_M = CPU_AMD_K8 + 0x07;
    public const uint CPU_K8_ATHLON_64_X2 = CPU_AMD_K8 + 0x08;
    public const uint CPU_K8_TURION_64_X2 = CPU_AMD_K8 + 0x09;
    public const uint CPU_K8_ATHLON_NEO = CPU_AMD_K8 + 0x0A;

    // K10
    public const uint CPU_K10_PHENOM = CPU_AMD_K10 + 0x01;
    public const uint CPU_K10_PHENOM_X3 = CPU_AMD_K10 + 0x02;
    public const uint CPU_K10_PHENOM_FX = CPU_AMD_K10 + 0x03;
    public const uint CPU_K10_OPTERON = CPU_AMD_K10 + 0x04;
    public const uint CPU_K10_TURION_64 = CPU_AMD_K10 + 0x05;
    public const uint CPU_K10_TURION_64_ULTRA = CPU_AMD_K10 + 0x06;
    public const uint CPU_K10_ATHLON_64 = CPU_AMD_K10 + 0x07;
    public const uint CPU_K10_SEMPRON = CPU_AMD_K10 + 0x08;
    public const uint CPU_K10_ATHLON_2 = CPU_AMD_K10 + 0x11;
    public const uint CPU_K10_ATHLON_2_X2 = CPU_AMD_K10 + 0x0B;
    public const uint CPU_K10_ATHLON_2_X3 = CPU_AMD_K10 + 0x0D;
    public const uint CPU_K10_ATHLON_2_X4 = CPU_AMD_K10 + 0x0C;
    public const uint CPU_K10_PHENOM_II = CPU_AMD_K10 + 0x09;
    public const uint CPU_K10_PHENOM_II_X2 = CPU_AMD_K10 + 0x0A;
    public const uint CPU_K10_PHENOM_II_X3 = CPU_AMD_K10 + 0x0E;
    public const uint CPU_K10_PHENOM_II_X4 = CPU_AMD_K10 + 0x0F;
    public const uint CPU_K10_PHENOM_II_X6 = CPU_AMD_K10 + 0x10;

    // K15
    public const uint CPU_K15_FXB = CPU_AMD_K15 + 0x01;
    public const uint CPU_K15_OPTERON = CPU_AMD_K15 + 0x02;
    public const uint CPU_K15_A10T = CPU_AMD_K15 + 0x03;
    public const uint CPU_K15_A8T = CPU_AMD_K15 + 0x04;
    public const uint CPU_K15_A6T = CPU_AMD_K15 + 0x05;
    public const uint CPU_K15_A4T = CPU_AMD_K15 + 0x06;
    public const uint CPU_K15_ATHLON_X4 = CPU_AMD_K15 + 0x07;
    public const uint CPU_K15_FXV = CPU_AMD_K15 + 0x08;
    public const uint CPU_K15_A10R = CPU_AMD_K15 + 0x09;
    public const uint CPU_K15_A8R = CPU_AMD_K15 + 0x0A;
    public const uint CPU_K15_A6R = CPU_AMD_K15 + 0x0B;
    public const uint CPU_K15_A4R = CPU_AMD_K15 + 0x0C;
    public const uint CPU_K15_SEMPRON = CPU_AMD_K15 + 0x0D;
    public const uint CPU_K15_ATHLON_X2	= CPU_AMD_K15 + 0x0E;
    public const uint CPU_K15_FXC = CPU_AMD_K15 + 0x0F;
    public const uint CPU_K15_A10C = CPU_AMD_K15 + 0x10;
    public const uint CPU_K15_A8C = CPU_AMD_K15 + 0x11;
    public const uint CPU_K15_A6C = CPU_AMD_K15 + 0x12;
    public const uint CPU_K15_A4C = CPU_AMD_K15 + 0x13;
    public const uint CPU_K15_A12 = CPU_AMD_K15 + 0x14;
    public const uint CPU_K15_RX = CPU_AMD_K15 + 0x15;
    public const uint CPU_K15_GX = CPU_AMD_K15 + 0x16;
    public const uint CPU_K15_A9 = CPU_AMD_K15 + 0x17;
    public const uint CPU_K15_E2 = CPU_AMD_K15 + 0x18;

    // K16
    public const uint CPU_K16_A6 = CPU_AMD_K16 + 0x01;
    public const uint CPU_K16_A4 = CPU_AMD_K16 + 0x02;
    public const uint CPU_K16_OPTERON =	CPU_AMD_K16 + 0x05;
    public const uint CPU_K16_ATHLON = CPU_AMD_K16 + 0x06;
    public const uint CPU_K16_SEMPRON = CPU_AMD_K16 + 0x07;
    public const uint CPU_K16_E1 = CPU_AMD_K16 + 0x08;
    public const uint CPU_K16_E2 = CPU_AMD_K16 + 0x09;
    public const uint CPU_K16_A8 = CPU_AMD_K16 + 0x0A;
    public const uint CPU_K16_A10 = CPU_AMD_K16 + 0x0B;
    public const uint CPU_K16_GX = CPU_AMD_K16 + 0x0C;

    // K17
    public const uint CPU_RYZEN = CPU_AMD_K17 + 0x01;
    public const uint CPU_RYZEN_7 = CPU_AMD_K17 + 0x02;
    public const uint CPU_RYZEN_5 = CPU_AMD_K17 + 0x03;
    public const uint CPU_RYZEN_3 = CPU_AMD_K17 + 0x04;
    public const uint CPU_RYZEN_TR = CPU_AMD_K17 + 0x05;
    public const uint CPU_RYZEN_EPYC = CPU_AMD_K17 + 0x06;
    public const uint CPU_RYZEN_M = CPU_AMD_K17 + 0x07;
    public const uint CPU_RYZEN_7_M = CPU_AMD_K17 + 0x08;
    public const uint CPU_RYZEN_5_M = CPU_AMD_K17 + 0x09;
    public const uint CPU_RYZEN_3_M = CPU_AMD_K17 + 0x0A;
    public const uint CPU_RYZEN_ATHLON = CPU_AMD_K17 + 0x0B;

    // Cyrix families
    public const uint CPU_CX486 = CPU_CYRIX + 0x000400;
    public const uint CPU_CX5X86 = CPU_CYRIX + 0x000500;
    public const uint CPU_CX6X86 = CPU_CYRIX + 0x000600;

    // VIA families
    public const uint CPU_VIA_WINCHIP = CPU_VIA + 0x000400;
    public const uint CPU_VIA_C3 = CPU_VIA + 0x000800;
    public const uint CPU_VIA_C7 = CPU_VIA + 0x001000;
    public const uint CPU_VIA_NANO = CPU_VIA + 0x002000;

    // VIA models
    // C3
    public const uint CPU_C3 = CPU_VIA_C3 + 0x01;

    // C7
    public const uint CPU_C7 = CPU_VIA_C7 + 0x01;
    public const uint CPU_C7_M = CPU_VIA_C7 + 0x02;
    public const uint CPU_EDEN = CPU_VIA_C7 + 0x03;
    public const uint CPU_C7_D = CPU_VIA_C7 + 0x04;

    // Nano
    public const uint CPU_NANO_X2 = CPU_VIA_NANO + 0x01;
    public const uint CPU_EDEN_X2 = CPU_VIA_NANO + 0x02;
    public const uint CPU_NANO_X3 = CPU_VIA_NANO + 0x03;
    public const uint CPU_EDEN_X4 = CPU_VIA_NANO + 0x04;
    public const uint CPU_QUADCORE = CPU_VIA_NANO + 0x05;

    // Cyrix
    public const uint CPU_CX6X86L = CPU_CX6X86 + 0x01;
    public const uint CPU_MEDIAGX = CPU_CX6X86 + 0x02;
    public const uint CPU_CX6X86MX = CPU_CX6X86 + 0x03;
    public const uint CPU_MII = CPU_CX6X86 + 0x04;

    // Transmeta
    public const uint CPU_CRUSOE = CPU_TRANSMETA + 0x01;
    public const uint CPU_EFFICEON = CPU_TRANSMETA + 0x02;

    // DMP
    public const uint CPU_VORTEX86_SX = CPU_DMP + 0x01;
    public const uint CPU_VORTEX86_EX = CPU_DMP + 0x02;
    public const uint CPU_VORTEX86_DX = CPU_DMP + 0x03;
    public const uint CPU_VORTEX86_MX = CPU_DMP + 0x04;
    public const uint CPU_VORTEX86_DX3 = CPU_DMP + 0x05;

///////////////////////////////////////////////////////////////////////////
//	Cache descriptors
///////////////////////////////////////////////////////////////////////////

    public const int CACHE_TYPE_DATA = 0x1;             // Data cache
    public const int CACHE_TYPE_INSTRUCTION = 0x2;      // Instruction cache
    public const int CACHE_TYPE_UNIFIED = 0x3;          // Unified cache
    public const int CACHE_TYPE_TRACE_CACHE = 0x4;      // Trace cache

///////////////////////////////////////////////////////////////////////////
//	Instructions sets
///////////////////////////////////////////////////////////////////////////

    public const int ISET_MMX = 0x01;
    public const int ISET_EXTENDED_MMX = 0x02;
    public const int ISET_3DNOW = 0x03;
    public const int ISET_EXTENDED_3DNOW = 0x04;
    public const int ISET_SSE = 0x05;
    public const int ISET_SSE2 = 0x06;
    public const int ISET_SSE3 = 0x07;
    public const int ISET_SSSE3 = 0x08;
    public const int ISET_SSE4_1 = 0x09;
    public const int ISET_SSE4_2 = 0x0C;
    public const int ISET_SSE4A = 0x0D;
    public const int ISET_XOP = 0x0E;
    public const int ISET_X86_64 = 0x10;
    public const int ISET_NX = 0x11;
    public const int ISET_VMX = 0x12;
    public const int ISET_AES  = 0x13;
    public const int ISET_AVX  = 0x14;
    public const int ISET_AVX2 = 0x15;
    public const int ISET_FMA3 = 0x16;
    public const int ISET_FMA4 = 0x17;
    public const int ISET_RTM = 0x18;
    public const int ISET_HLE = 0x19;
    public const int ISET_AVX512F = 0x1A;
    public const int ISET_SHA = 0x1B;


///////////////////////////////////////////////////////////////////////////
//	Memory types
///////////////////////////////////////////////////////////////////////////

    public const int MEMORY_TYPE_SPM_RAM = 0x1;
    public const int MEMORY_TYPE_RDRAM = 0x2;
    public const int MEMORY_TYPE_EDO_RAM = 0x3;
    public const int MEMORY_TYPE_FPM_RAM = 0x4;
    public const int MEMORY_TYPE_SDRAM = 0x5;
    public const int MEMORY_TYPE_DDR_SDRAM = 0x6;
    public const int MEMORY_TYPE_DDR2_SDRAM = 0x7;
    public const int MEMORY_TYPE_DDR2_SDRAM_FB = 0x8;
    public const int MEMORY_TYPE_DDR3_SDRAM = 0x9;
    public const int MEMORY_TYPE_DDR4_SDRAM = 0xA;

///////////////////////////////////////////////////////////////////////////
//	Display Adapter infos source
///////////////////////////////////////////////////////////////////////////

    public const int DISPLAY_CLOCK_DOMAIN_GRAPHICS = 0x0;
    public const int DISPLAY_CLOCK_DOMAIN_MEMORY = 0x4;
    public const int DISPLAY_CLOCK_DOMAIN_PROCESSOR = 0x7;    

    public const int MEMORY_TYPE_SDR = 1;
    public const int MEMORY_TYPE_DDR = 2;
    public const int MEMORY_TYPE_LPDDR2 = 9;
    public const int MEMORY_TYPE_DDR2 = 3;
    public const int MEMORY_TYPE_DDR3 = 7;
    public const int MEMORY_TYPE_GDDR2 = 4;
    public const int MEMORY_TYPE_GDDR3 = 5;
    public const int MEMORY_TYPE_GDDR4 = 6;
    public const int MEMORY_TYPE_GDDR5 = 8;
    public const int MEMORY_TYPE_GDDR5X = 10;
    public const int MEMORY_TYPE_HBM1 = 11;
    public const int MEMORY_TYPE_HBM2 = 12;
    public const int MEMORY_TYPE_SDDR4 = 13;
    public const int MEMORY_TYPE_GDDR6 = 14;

///////////////////////////////////////////////////////////////////////////
//	Storage devices
///////////////////////////////////////////////////////////////////////////

    public const int DRIVE_FEATURE_IS_SSD = (1 << 0);
    public const int DRIVE_FEATURE_SMART = (1 << 1);
    public const int DRIVE_FEATURE_TRIM = (1 << 2);
    public const int DRIVE_FEATURE_IS_REMOVABLE = (1 << 4);

    public const int BUS_TYPE_SCSI = 0x01;
    public const int BUS_TYPE_ATAPI = 0x02;
    public const int BUS_TYPE_ATA = 0x03;
    public const int BUS_TYPE_IEEE1394 = 0x04;
    public const int BUS_TYPE_SSA = 0x05;
    public const int BUS_TYPE_FIBRE = 0x06;
    public const int BUS_TYPE_USB = 0x07;
    public const int BUS_TYPE_RAID = 0x08;
    public const int BUS_TYPE_ISCSI = 0x09;
    public const int BUS_TYPE_SAS = 0x0A;
    public const int BUS_TYPE_SATA = 0x0B;
    public const int BUS_TYPE_SD = 0x0C;
    public const int BUS_TYPE_MMC = 0x0D;
    public const int BUS_TYPE_VIRTUAL = 0x0E;
    public const int BUS_TYPE_FILEBACKEDVIRTUAL = 0x0F;
    public const int BUS_TYPE_SPACES = 0x10;
    public const int BUS_TYPE_NVME = 0x11;


///////////////////////////////////////////////////////////////////////////
//	DLL name & path
///////////////////////////////////////////////////////////////////////////

#if _X64_
#if DEBUG
    public const string szDllPath = ".\\";
#else
    public const string szDllPath = ".\\";
#endif
    public const string szDllFilename = "cpuidsdk64.dll";    
#else   // _X64_
#if DEBUG
    public const string szDllPath = ".\\";
#else
    public const string szDllPath = ".\\";
#endif
    public const string szDllFilename = "cpuidsdk.dll";
#endif  // _X64_

    protected const string szDllName = szDllPath + szDllFilename;

	enum PTR : uint
	{
        PTR0 = 0x4FAA9F55,
        PTR1 = 0x21244248,
        PTR2 = 0xE05DC0BB,
        PTR3 = 0x5A0CB419,
        PTR4 = 0xA6654CCA,
        PTR5 = 0x5358A6B1,
        PTR6 = 0x14DE29BC,
        PTR7 = 0x75CCEB99,
        PTR8 = 0xA3FD47FA,
        PTR9 = 0x44FA89F5,
        PTR10 = 0x8EA91D52,
        PTR11 = 0x9DD73BAE,
        PTR12 = 0xBF057E0A,
        PTR13 = 0x06940D28,
        PTR14 = 0xB1F763EE,
        PTR15 = 0x1DB43B68,
        PTR16 = 0xB8B37166,
        PTR17 = 0x1B9E373C,
        PTR18 = 0x6356C6AD,
        PTR19 = 0x80890112,
        PTR20 = 0x67EACFD5,
        PTR21 = 0xCD259A4B,
        PTR22 = 0x447C88F9,
        PTR23 = 0x4BBE977D,
        PTR24 = 0xEFEDDFDB,
        PTR25 = 0x496A92D5,
        PTR26 = 0x2E905D20,
        PTR27 = 0x6342C685,
        PTR28 = 0x1FAE3F5C,
        PTR29 = 0xE925D24B,
        PTR30 = 0x8E311C62,
        PTR31 = 0xBEE77DCE,
        PTR32 = 0x285050A0,
        PTR33 = 0x17842F08,
        PTR34 = 0x04CA0994,
        PTR35 = 0x7136E26D,
        PTR36 = 0x4CE499C9,
        PTR37 = 0xF22FE45F,
        PTR38 = 0xD417A82F,
        PTR39 = 0xF579EAF3,
        PTR40 = 0x433C8679,
        PTR41 = 0x55DEABBD,
        PTR42 = 0x0F161E2C,
        PTR43 = 0xE695CD2B,
        PTR44 = 0x7650ECA1,
        PTR45 = 0x5870B0E1,
        PTR46 = 0xA6634CC6,
        PTR47 = 0x963B2C76,
        PTR48 = 0xD307A60F,
        PTR49 = 0xA7194E32,
        PTR50 = 0xB7F36FE6,
        PTR51 = 0x18383070,
        PTR52 = 0xE78BCF17,
        PTR53 = 0x08941128,
        PTR54 = 0xC7098E13,
        PTR55 = 0x492E925D,
        PTR56 = 0x47808F01,
        PTR57 = 0xB15362A6,
        PTR58 = 0xBEB77D6E,
        PTR59 = 0x4DBA9B75,
        PTR60 = 0x4FE09FC1,
        PTR61 = 0xFDE9FBD3,
        PTR62 = 0x5C68B8D1,
        PTR63 = 0x1E523CA4,
        PTR64 = 0xB97F72FE,
        PTR65 = 0x5D1CBA39,
        PTR66 = 0x4E089C11,
        PTR67 = 0x784AF095,
        PTR68 = 0xB1336266,
        PTR69 = 0xE8B7D16F,
        PTR70 = 0x7D2AFA55,
        PTR71 = 0x6344C689,
        PTR72 = 0x3E107C20,
        PTR73 = 0xFB01F603,
        PTR74 = 0x397872F0,
        PTR75 = 0x2BD857B0,
        PTR76 = 0x34846908,
        PTR77 = 0x092C1258,
        PTR78 = 0x9E1D3C3A,
        PTR79 = 0x6A3CD479,
        PTR80 = 0xF82DF05B,
        PTR81 = 0x2ED45DA8,
        PTR82 = 0x46C68D8D,
        PTR83 = 0x5B12B625,
        PTR84 = 0x64A2C945,
        PTR85 = 0xC3D187A3,
        PTR86 = 0xFED7FDAF,
        PTR87 = 0x735AE6B5,
        PTR88 = 0xFF81FF03,
        PTR89 = 0xADB35B66,
        PTR90 = 0x26E84DD0,
        PTR91 = 0xCC5F98BF,
        PTR92 = 0x4A6494C9,
        PTR93 = 0x6F16DE2D,
        PTR94 = 0xA6594CB2,
        PTR95 = 0x80EB01D6,
        PTR96 = 0xF0E7E1CF,
        PTR97 = 0xD101A203,
        PTR98 = 0xF1D7E3AF,
        PTR99 = 0xDC3DB87B,
        PTR100 = 0x72AEE55D,
        PTR101 = 0xBA6374C6,
        PTR102 = 0x60E6C1CD,
        PTR103 = 0xF003E007,
        PTR104 = 0x5DEABBD5,
        PTR105 = 0x6B1AD635,
        PTR106 = 0xDC09B813,
        PTR107 = 0x48009001,
        PTR108 = 0x004C0098,
        PTR109 = 0x35C66B8C,
        PTR110 = 0x9C15382A,
        PTR111 = 0x81CF039E,
        PTR112 = 0xA1E343C6,
        PTR113 = 0x1DE23BC4,
        PTR114 = 0xEBA3D747,
        PTR115 = 0x31C06380,
        PTR116 = 0x00000000,
        PTR117 = 0x7EC8FD91,
        PTR118 = 0x20B6416C,
        PTR119 = 0xC7CF8F9F,
        PTR120 = 0x2B5E56BC,
        PTR121 = 0x28B85170,
        PTR122 = 0x2B6056C0,
        PTR123 = 0x8E8F1D1E,
        PTR124 = 0x8985130A,
        PTR125 = 0x159A2B34,
        PTR126 = 0xD2CDA59B,
        PTR127 = 0x1A1C3438,
        PTR128 = 0x4BA6974D,
        PTR129 = 0xEC3FD87F,
        PTR130 = 0x96652CCA,
        PTR131 = 0x25044A08,
        PTR132 = 0x6F4ADE95,
        PTR133 = 0x7282E505,
        PTR134 = 0x981F303E,
        PTR135 = 0xA7334E66,
        PTR136 = 0x4E389C71,
        PTR137 = 0x4B909721,
        PTR138 = 0x44FA89F5,
        PTR139 = 0x75DCEBB9,
        PTR140 = 0x93F927F2,
        PTR141 = 0xA3414682,
        PTR142 = 0xDB3BB677,
        PTR143 = 0xD82DB05B,
        PTR144 = 0x10182030,
        PTR145 = 0x7854F0A9,
        PTR146 = 0xD857B0AF,
        PTR147 = 0x25384A70,
        PTR148 = 0x7146E28D,
        PTR149 = 0xC0AD815B,
        PTR150 = 0xB17962F2,
        PTR151 = 0x7A5EF4BD,
        PTR152 = 0xF9CBF397,
        PTR153 = 0xBC5D78BA,
        PTR154 = 0x1C5238A4,
        PTR155 = 0x429A8535,
        PTR156 = 0xEBB7D76F,
        PTR157 = 0x26244C48,
        PTR158 = 0xA0D941B2,
        PTR159 = 0xB9037206,
        PTR160 = 0x106020C0,
        PTR161 = 0x3DE67BCC,
        PTR162 = 0xDCF3B9E7,
        PTR163 = 0xA7CF4F9E,
        PTR164 = 0xD4B7A96F,
        PTR165 = 0x38E471C8,
        PTR166 = 0x87A10F42,
        PTR167 = 0xFB45F68B,
        PTR168 = 0xEDEBDBD7,
        PTR169 = 0xE2DFC5BF,
        PTR170 = 0x907320E6,
        PTR171 = 0x3FCC7F98,
        PTR172 = 0x139E273C,
        PTR173 = 0x02880510,
        PTR174 = 0x8D9F1B3E,
        PTR175 = 0x6AC4D589,
        PTR176 = 0x95C12B82,
        PTR177 = 0xDA39B473,
        PTR178 = 0xA2B5456A,
        PTR179 = 0x6568CAD1,
        PTR180 = 0xE123C247,
        PTR181 = 0x634AC695,
        PTR182 = 0xF1EDE3DB,
        PTR183 = 0xFC07F80F,
        PTR184 = 0x1CB03960,
        PTR185 = 0xE989D313,
        PTR186 = 0x6FE2DFC5,
        PTR187 = 0x0B021604,
        PTR188 = 0xECFDD9FB,
        PTR189 = 0x8A291452,
        PTR190 = 0xE6C9CD93,
        PTR191 = 0xFC21F843,
        PTR192 = 0x4A829505,
        PTR193 = 0x6D0ADA15,
        PTR194 = 0x195232A4,
        PTR195 = 0xA0C7418E,
        PTR196 = 0xEE61DCC3,
        PTR197 = 0xB99B7336,
        PTR198 = 0x4C3A9875,
        PTR199 = 0x7526EA4D,
        PTR200 = 0xA8B35166,
        PTR201 = 0xE5ABCB57,
        PTR202 = 0x2CA6594C,
        PTR203 = 0xECA1D943,
        PTR204 = 0x4986930D,
        PTR205 = 0x2C6058C0,
        PTR206 = 0xEED1DDA3,
        PTR207 = 0x48C6918D,
        PTR208 = 0xEA89D513,
        PTR209 = 0x779EEF3D,
        PTR210 = 0x40988131,
        PTR211 = 0x2346468C,
        PTR212 = 0x17442E88,
        PTR213 = 0x60B4C169,
        PTR214 = 0xF2E3E5C7,
        PTR215 = 0xB3296652,
        PTR216 = 0x31046208,
        PTR217 = 0xD3B3A767,
        PTR218 = 0x8047008E,
        PTR219 = 0xCEB79D6F,
        PTR220 = 0x7FD4FFA9,
        PTR221 = 0x4A5294A5,
        PTR222 = 0x04300860,
        PTR223 = 0xA7634EC6,
        PTR224 = 0x514CA299,
        PTR225 = 0xBBB97772,
        PTR226 = 0x2BD057A0,
        PTR227 = 0x6CD8D9B1,
        PTR228 = 0xE179C2F3,
        PTR229 = 0x36546CA8,
        PTR230 = 0xE6C1CD83,
        PTR231 = 0x69BAD375,
        PTR232 = 0x89BF137E,
        PTR233 = 0xF367E6CF,
        PTR234 = 0x89C5138A,
        PTR235 = 0x365A6CB4,
        PTR236 = 0x5C42B885,
        PTR237 = 0x9E193C32,
        PTR238 = 0x31426284,
        PTR239 = 0x7A5AF4B5,
        PTR240 = 0xC1238247,
        PTR241 = 0x9C993932,
        PTR242 = 0x19A43348,
        PTR243 = 0xBB7576EA,
        PTR244 = 0x316E62DC,
        PTR245 = 0x48989131,
        PTR246 = 0xFD2DFA5B,
        PTR247 = 0x0E521CA4,
        PTR248 = 0x4E769CED,
        PTR249 = 0x43828705,
        PTR250 = 0xA7AD4F5A,
        PTR251 = 0xDBB1B763,
        PTR252 = 0x106820D0,
        PTR253 = 0xDB65B6CB,
        PTR254 = 0xEEFFDDFF,
        PTR255 = 0x297E52FC
    };

///////////////////////////////////////////////////////////////////////////
//	DLL functions
///////////////////////////////////////////////////////////////////////////    

    [DllImport(szDllName, EntryPoint = "QueryInterface")]
    protected static extern IntPtr CPUIDSDK_fp_QueryInterface(uint _code);

//  Instance management
    private delegate IntPtr CPUIDSDK_fp_CreateInstance();
    private delegate void CPUIDSDK_fp_DestroyInstance(IntPtr objptr);
    private delegate int CPUIDSDK_fp_Init(IntPtr objptr, string _szDllPath, string _szDllFilename, int _config_flag, ref int _errorcode, ref int _extended_errorcode);
    private delegate void CPUIDSDK_fp_Close(IntPtr objptr);
    private delegate void CPUIDSDK_fp_RefreshInformation(IntPtr objptr);
    private delegate void CPUIDSDK_fp_GetDllVersion(IntPtr objptr, ref int _version);

//  CPU
    private delegate int CPUIDSDK_fp_GetNbProcessors(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetProcessorFamily(IntPtr objptr, int _proc_index);
    private delegate int CPUIDSDK_fp_GetProcessorCoreCount(IntPtr objptr, int _proc_index);
    private delegate int CPUIDSDK_fp_GetProcessorThreadCount(IntPtr objptr, int _proc_index);
    private delegate int CPUIDSDK_fp_GetProcessorCoreThreadCount(IntPtr objptr, int _proc_index, int _core_index);
    private delegate int CPUIDSDK_fp_GetProcessorThreadAPICID(IntPtr objptr, int _proc_index, int _core_index, int _thread_index);
    private delegate IntPtr CPUIDSDK_fp_GetProcessorName(IntPtr objptr, int _proc_index);
    private delegate IntPtr CPUIDSDK_fp_GetProcessorCodeName(IntPtr objptr, int _proc_index);
    private delegate IntPtr CPUIDSDK_fp_GetProcessorSpecification(IntPtr objptr, int _proc_index);
    private delegate IntPtr CPUIDSDK_fp_GetProcessorPackage(IntPtr objptr, int _proc_index);
    private delegate IntPtr CPUIDSDK_fp_GetProcessorStepping(IntPtr objptr, int _proc_index);
    private delegate float CPUIDSDK_fp_GetProcessorTDP(IntPtr objptr, int _proc_index);
    private delegate float CPUIDSDK_fp_GetProcessorManufacturingProcess(IntPtr objptr, int _proc_index);
    private delegate int CPUIDSDK_fp_IsProcessorInstructionSetAvailable(IntPtr objptr, int _proc_index, int _iset);
    private delegate float CPUIDSDK_fp_GetProcessorCoreClockFrequency(IntPtr objptr, int _proc_index, int _core_index);
    private delegate float CPUIDSDK_fp_GetProcessorCoreClockMultiplier(IntPtr objptr, int _proc_index, int _core_index);
    private delegate float CPUIDSDK_fp_GetProcessorCoreTemperature(IntPtr objptr, int _proc_index, int _core_index);
    private delegate float CPUIDSDK_fp_GetBusFrequency(IntPtr objptr);
    private delegate float CPUIDSDK_fp_GetProcessorRatedBusFrequency(IntPtr objptr, int _proc_index);
    private delegate int CPUIDSDK_fp_GetProcessorStockClockFrequency(IntPtr objptr, int _proc_index);
    private delegate int CPUIDSDK_fp_GetProcessorStockBusFrequency(IntPtr objptr, int _proc_index);
    private delegate int CPUIDSDK_fp_GetProcessorMaxCacheLevel(IntPtr objptr, int _proc_index);
    private delegate void CPUIDSDK_fp_GetProcessorCacheParameters(IntPtr objptr, int _proc_index, int _cache_level, int _cache_type, ref int _NbCaches, ref int _size);
    private delegate int CPUIDSDK_fp_GetProcessorID(IntPtr objptr, int _proc_index);
    private delegate float CPUIDSDK_fp_GetProcessorVoltageID(IntPtr objptr, int _proc_index);
 
//  Memory
    private delegate int CPUIDSDK_fp_GetMemoryType(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemorySize(IntPtr objptr);
    private delegate float CPUIDSDK_fp_GetMemoryClockFrequency(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryNumberOfChannels(IntPtr objptr);
    private delegate float CPUIDSDK_fp_GetMemoryCASLatency(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryRAStoCASDelay(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryRASPrecharge(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryTRAS(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryTRC(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryCommandRate(IntPtr objptr);

//  NB & BIOS
    private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeVendor(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeModel(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeRevision(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeVendor(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeModel(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeRevision(IntPtr objptr);

    private delegate void CPUIDSDK_fp_GetGraphicBusLinkParameters(IntPtr objptr, ref int _bus_type, ref int _link_width);
    private delegate void CPUIDSDK_fp_GetMemorySlotsConfig(IntPtr objptr, ref int _nbslots, ref int _nbusedslots, ref int _slotmap_h, ref int _slotmap_l, ref int _maxmodulesize);

    private delegate IntPtr CPUIDSDK_fp_GetBIOSVendor(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetBIOSVersion(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetBIOSDate(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetMainboardVendor(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetMainboardModel(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetMainboardRevision(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetMainboardSerialNumber(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSystemManufacturer(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSystemProductName(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSystemVersion(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSystemSerialNumber(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSystemUUID(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSystemSKU(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetSystemFamily(IntPtr objptr);

    private delegate IntPtr CPUIDSDK_fp_GetChassisManufacturer(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetChassisType(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetChassisSerialNumber(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryInfosExt(IntPtr objptr, ref IntPtr _szLocation, ref IntPtr _szUsage, ref IntPtr _szCorrection);
    private delegate int CPUIDSDK_fp_GetNumberOfMemoryDevices(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryDeviceInfos(IntPtr objptr, int _device_index, ref int _size, ref IntPtr _szFormat);
    private delegate int CPUIDSDK_fp_GetMemoryDeviceInfosExt(IntPtr objptr, int _device_index, ref IntPtr _szDesignation, ref IntPtr _szType, ref int _total_width, ref int _data_width, ref int _speed);
    private delegate int CPUIDSDK_fp_GetProcessorSockets(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryMaxCapacity(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetMemoryMaxNumberOfDevices(IntPtr objptr);

//  SPD
    private delegate int CPUIDSDK_fp_GetNumberOfSPDModules(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetSPDModuleType(IntPtr objptr, int _spd_index);
    private delegate int CPUIDSDK_fp_GetSPDModuleSize(IntPtr objptr, int _spd_index);
    private delegate IntPtr CPUIDSDK_fp_GetSPDModuleFormat(IntPtr objptr, int _spd_index);
    private delegate IntPtr CPUIDSDK_fp_GetSPDModuleManufacturer(IntPtr objptr, int _spd_index);
    private delegate int CPUIDSDK_fp_GetSPDModuleManufacturerID(IntPtr objptr, int _spd_index, byte[] _id);
    private delegate IntPtr CPUIDSDK_fp_GetSPDModuleDRAMManufacturer(IntPtr objptr, int _spd_index);
    private delegate int CPUIDSDK_fp_GetSPDModuleMaxFrequency(IntPtr objptr, int _spd_index);
    private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSpecification(IntPtr objptr, int _spd_index);
    private delegate IntPtr CPUIDSDK_fp_GetSPDModulePartNumber(IntPtr objptr, int _spd_index);
    private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSerialNumber(IntPtr objptr, int _spd_index);
    private delegate float CPUIDSDK_fp_GetSPDModuleMinTRCD(IntPtr objptr, int _spd_index);
    private delegate float CPUIDSDK_fp_GetSPDModuleMinTRP(IntPtr objptr, int _spd_index);
    private delegate float CPUIDSDK_fp_GetSPDModuleMinTRAS(IntPtr objptr, int _spd_index);
    private delegate float CPUIDSDK_fp_GetSPDModuleMinTRC(IntPtr objptr, int _spd_index);
    private delegate int CPUIDSDK_fp_GetSPDModuleManufacturingDate(IntPtr objptr, int _spd_index, ref int _year, ref int _week);
    private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfBanks(IntPtr objptr, int _spd_index);
    private delegate int CPUIDSDK_fp_GetSPDModuleDataWidth(IntPtr objptr, int _spd_index);
    private delegate float CPUIDSDK_fp_GetSPDModuleTemperature(IntPtr objptr, int _spd_index);
    private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfProfiles(IntPtr objptr, int _spd_index);
    private delegate void CPUIDSDK_fp_GetSPDModuleProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _nominal_vdd);
    private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles(IntPtr objptr, int _spd_index, ref int _epp_revision);
    private delegate void CPUIDSDK_fp_GetSPDModuleEPPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd);
    private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles(IntPtr objptr, int _spd_index, ref int _xmp_revision);
    private delegate int CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL(IntPtr objptr, int _spd_index, int _profile_index);
    private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos(IntPtr objptr, int _spd_index, int _profile_index, int _cl_index, ref float _frequency, ref float _CL);
    private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _nominal_vdd, ref int _max_freq, ref float _max_CL);
    private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles(IntPtr objptr, int _spd_index, ref int _amp_revision);
    private delegate void CPUIDSDK_fp_GetSPDModuleAMPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref int _frequency, ref float _min_cycle_time, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC);
    private delegate int CPUIDSDK_fp_GetSPDModuleRawData(IntPtr objptr, int _spd_index, int _offset);

//  Display
    private delegate int CPUIDSDK_fp_GetNumberOfDisplayAdapter(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterID(IntPtr objptr, int _adapter_index);
    private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterName(IntPtr objptr, int _adapter_index);
    private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCodeName(IntPtr objptr, int _adapter_index);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels(IntPtr objptr, int _adapter_index);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel(IntPtr objptr, int _adapter_index);
    private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName(IntPtr objptr, int _adapter_index, int _perf_level);
    private delegate float CPUIDSDK_fp_GetDisplayAdapterClock(IntPtr objptr, int _perf_level, int _adapter_index, int _domain);
    private delegate float CPUIDSDK_fp_GetDisplayAdapterStockClock(IntPtr objptr, int _perf_level, int _adapter_index, int _domain);
    private delegate float CPUIDSDK_fp_GetDisplayAdapterTemperature(IntPtr objptr, int _adapter_index, int _domain);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterFanSpeed(IntPtr objptr, int _adapter_index);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterFanPWM(IntPtr objptr, int _adapter_index);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryType(IntPtr objptr, int _adapter_index, ref int _type);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterMemorySize(IntPtr objptr, int _adapter_index, ref int _size);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth(IntPtr objptr, int _adapter_index, ref int _bus_width);
    private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterMemoryVendor(IntPtr objptr, int _adapter_index);
    private delegate IntPtr CPUIDSDK_fp_GetDisplayDriverVersion(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetDirectXVersion(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterBusInfos(IntPtr objptr, int _adapter_index, ref int _bus_type, ref int _multi_vpu);
    private delegate float CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess(IntPtr objptr, int _adapter_index);
    private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCoreFamily(IntPtr objptr, int _adapter_index, ref int _core);

    private delegate int CPUIDSDK_fp_GetNumberOfMonitors(IntPtr objptr);
    private delegate IntPtr CPUIDSDK_fp_GetMonitorName(IntPtr objptr, int _monitor_index);
    private delegate IntPtr CPUIDSDK_fp_GetMonitorVendor(IntPtr objptr, int _monitor_index);
    private delegate IntPtr CPUIDSDK_fp_GetMonitorID(IntPtr objptr, int _monitor_index);
    private delegate IntPtr CPUIDSDK_fp_GetMonitorSerial(IntPtr objptr, int _monitor_index);
    private delegate int CPUIDSDK_fp_GetMonitorManufacturingDate(IntPtr objptr, int _monitor_index, ref int _week, ref int _year);
    private delegate float CPUIDSDK_fp_GetMonitorSize(IntPtr objptr, int _monitor_index);
    private delegate int CPUIDSDK_fp_GetMonitorResolution(IntPtr objptr, int _monitor_index, ref int _width, ref int _height, ref int _frequency);
    private delegate int CPUIDSDK_fp_GetMonitorMaxPixelClock(IntPtr objptr, int _monitor_index);
    private delegate float CPUIDSDK_fp_GetMonitorGamma(IntPtr objptr, int _monitor_index);

//  HDD
    private delegate int CPUIDSDK_fp_GetNumberOfStorageDevice(IntPtr objptr);
    private delegate int CPUIDSDK_fp_GetStorageDriveNumber(IntPtr objptr, int _index);
    private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceName(IntPtr objptr, int _index);
    private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceRevision(IntPtr objptr, int _index);
    private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceSerialNumber(IntPtr objptr, int _index);
    private delegate int CPUIDSDK_fp_GetStorageDeviceBusType(IntPtr objptr, int _index);
    private delegate int CPUIDSDK_fp_GetStorageDeviceRotationSpeed(IntPtr objptr, int _index);
    private delegate int CPUIDSDK_fp_GetStorageDeviceFeatureFlag(IntPtr objptr, int _index);
    private delegate int CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes(IntPtr objptr, int _index);
    private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceVolumeLetter(IntPtr objptr, int _index, int _volume_index);	
    private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity(IntPtr objptr, int _index, int _volume_index);
    private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity(IntPtr objptr, int _index, int _volume_index);
    private delegate int CPUIDSDK_fp_GetStorageDeviceSmartAttribute(IntPtr objptr, int _index, int _attrib_index, ref int _id, ref int _flags, ref int _value, ref int _worst, byte[] _data);
    private delegate int CPUIDSDK_fp_GetStorageDevicePowerOnHours(IntPtr objptr, int _index);
    private delegate int CPUIDSDK_fp_GetStorageDevicePowerCycleCount(IntPtr objptr, int _index);
    private delegate float CPUIDSDK_fp_GetStorageDeviceTotalCapacity(IntPtr objptr, int _index);


    //MONITORING
    private delegate IntPtr CPUIDSDK_fp_GetDeviceName(IntPtr objptr, int _device_index);
    private delegate int CPUIDSDK_fp_GetDisplayAdapterPCIID(IntPtr objptr, int _adapter_index, ref int _vendor_id, ref int _device_id, ref int _revision_id, ref int _sub_vendor_id, ref int _sub_model_id);

    protected IntPtr objptr = IntPtr.Zero;

//  Instance management
    public bool CreateInstance()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR0);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_CreateInstance msd = (CPUIDSDK_fp_CreateInstance)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_CreateInstance));
                objptr = msd();
                if (objptr != IntPtr.Zero)
                    return true;
                else
                    return false;
            }
            else
            {
                objptr = IntPtr.Zero;
                return false;
            }
        }
        catch (Exception e)
        {
            string message = e.Message;
            return false;
        }
    }
    public void DestroyInstance()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR1);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_DestroyInstance msd = (CPUIDSDK_fp_DestroyInstance)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_DestroyInstance));
                msd(objptr);
            }
        }
        catch
        {
        }
    }
    public bool Init(string _szDllPath, string _szDllFilename, uint _config_flag, ref int _errorcode, ref int _extended_errorcode)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR2);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_Init msd = (CPUIDSDK_fp_Init)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_Init));
                res = msd(objptr, _szDllPath, _szDllFilename, (int)_config_flag, ref _errorcode, ref _extended_errorcode);
                if (res == 1) return true;
                else return false;
            }
            else
            {
                _errorcode = (int)CPUIDSDK_ERROR_INVALID_DLL;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public void Close()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR3);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_Close msd = (CPUIDSDK_fp_Close)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_Close));
                msd(objptr);
            }
        }
        catch
        {
        }
    }
    public void RefreshInformation()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR4);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_RefreshInformation msd = (CPUIDSDK_fp_RefreshInformation)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_RefreshInformation));
                msd(objptr);
            }
        }
        catch
        {
        }
    }
    public void GetDllVersion(ref int _version)
    {
       try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR5);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDllVersion msd = (CPUIDSDK_fp_GetDllVersion)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDllVersion));
                msd(objptr, ref _version);
            }
        }
        catch
        {
        }
    }

//  Processor
    public int GetNumberOfProcessors()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR8);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNbProcessors msd = (CPUIDSDK_fp_GetNbProcessors)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNbProcessors));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetProcessorFamily(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR9);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorFamily msd = (CPUIDSDK_fp_GetProcessorFamily)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorFamily));
                return msd(objptr, _proc_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetProcessorCoreCount(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR14);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorCoreCount msd = (CPUIDSDK_fp_GetProcessorCoreCount)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorCoreCount));
                return msd(objptr, _proc_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetProcessorThreadCount(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR15);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorThreadCount msd = (CPUIDSDK_fp_GetProcessorThreadCount)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorThreadCount));
                return msd(objptr, _proc_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetProcessorCoreThreadCount(int _proc_index, int _core_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR16);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorCoreThreadCount msd = (CPUIDSDK_fp_GetProcessorCoreThreadCount)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorCoreThreadCount));
                return msd(objptr, _proc_index, _core_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetProcessorThreadAPICID(IntPtr objptr, int _proc_index, int _core_index, int _thread_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR17);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorThreadAPICID msd = (CPUIDSDK_fp_GetProcessorThreadAPICID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorThreadAPICID));
                return msd(objptr, _proc_index, _core_index, _thread_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public string GetProcessorName(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR18);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorName msd = (CPUIDSDK_fp_GetProcessorName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorName));
                IntPtr pointer = msd(objptr, _proc_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetProcessorCodeName(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR19);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorCodeName msd = (CPUIDSDK_fp_GetProcessorCodeName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorCodeName));
                IntPtr pointer = msd(objptr, _proc_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetProcessorSpecification(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR20);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorSpecification msd = (CPUIDSDK_fp_GetProcessorSpecification)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorSpecification));
                IntPtr pointer = msd(objptr, _proc_index);
                return (Marshal.PtrToStringAnsi(pointer));
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetProcessorPackage(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR21);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorPackage msd = (CPUIDSDK_fp_GetProcessorPackage)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorPackage));
                IntPtr pointer = msd(objptr, _proc_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetProcessorStepping(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR22);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorStepping msd = (CPUIDSDK_fp_GetProcessorStepping)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorStepping));
                IntPtr pointer = msd(objptr, _proc_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public float GetProcessorTDP(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR23);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorTDP msd = (CPUIDSDK_fp_GetProcessorTDP)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorTDP));
                return msd(objptr, _proc_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetProcessorManufacturingProcess(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR24);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorManufacturingProcess msd = (CPUIDSDK_fp_GetProcessorManufacturingProcess)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorManufacturingProcess));
                return msd(objptr, _proc_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public bool IsProcessorInstructionSetAvailable(int _proc_index, int _iset)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR33);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_IsProcessorInstructionSetAvailable msd = (CPUIDSDK_fp_IsProcessorInstructionSetAvailable)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_IsProcessorInstructionSetAvailable));
                int res = msd(objptr, _proc_index, _iset);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public float GetProcessorCoreClockFrequency(int _proc_index, int _core_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR25);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorCoreClockFrequency msd = (CPUIDSDK_fp_GetProcessorCoreClockFrequency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorCoreClockFrequency));
                return msd(objptr, _proc_index, _core_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetProcessorCoreClockMultiplier(int _proc_index, int _core_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR30);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorCoreClockMultiplier msd = (CPUIDSDK_fp_GetProcessorCoreClockMultiplier)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorCoreClockMultiplier));
                return msd(objptr, _proc_index, _core_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetProcessorCoreTemperature(int _proc_index, int _core_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR32);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorCoreTemperature msd = (CPUIDSDK_fp_GetProcessorCoreTemperature)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorCoreTemperature));
                return msd(objptr, _proc_index, _core_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetBusFrequency()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR28);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetBusFrequency msd = (CPUIDSDK_fp_GetBusFrequency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetBusFrequency));
                return msd(objptr);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetProcessorRatedBusFrequency(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR29);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorRatedBusFrequency msd = (CPUIDSDK_fp_GetProcessorRatedBusFrequency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorRatedBusFrequency));
                return msd(objptr, _proc_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetProcessorStockClockFrequency(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR26);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorStockClockFrequency msd = (CPUIDSDK_fp_GetProcessorStockClockFrequency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorStockClockFrequency));
                return msd(objptr, _proc_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetProcessorStockBusFrequency(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR27);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorStockBusFrequency msd = (CPUIDSDK_fp_GetProcessorStockBusFrequency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorStockBusFrequency));
                return msd(objptr, _proc_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public int GetProcessorMaxCacheLevel(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR34);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorMaxCacheLevel msd = (CPUIDSDK_fp_GetProcessorMaxCacheLevel)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorMaxCacheLevel));
                return msd(objptr, _proc_index);
            }
            return 0;
        }
        catch
        {
            return 0;
        }
    }
    public void GetProcessorCacheParameters(int _proc_index, int _cache_level, int _cache_type, ref int _NbCaches, ref int _size)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR35);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorCacheParameters msd = (CPUIDSDK_fp_GetProcessorCacheParameters)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorCacheParameters));
                msd(objptr, _proc_index, _cache_level, _cache_type, ref _NbCaches, ref _size);
            }
        }
        catch
        {
        }
    }
    public uint GetProcessorID(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR39);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorID msd = (CPUIDSDK_fp_GetProcessorID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorID));
                return (uint)msd(objptr, _proc_index);
            }
            return 0;
        }
        catch
        {
            return 0;
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//  FIV/VID
//////////////////////////////////////////////////////////////////////////////////////////////////////////

    public float GetProcessorVoltageID(int _proc_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR40);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorVoltageID msd = (CPUIDSDK_fp_GetProcessorVoltageID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorVoltageID));
                return msd(objptr, _proc_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Memory
//////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int GetMemoryType()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR51);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryType msd = (CPUIDSDK_fp_GetMemoryType)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryType));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetMemorySize()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR50);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemorySize msd = (CPUIDSDK_fp_GetMemorySize)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemorySize));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public float GetMemoryClockFrequency()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR53);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryClockFrequency msd = (CPUIDSDK_fp_GetMemoryClockFrequency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryClockFrequency));
                return msd(objptr);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public int GetMemoryNumberOfChannels()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR52);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryNumberOfChannels msd = (CPUIDSDK_fp_GetMemoryNumberOfChannels)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryNumberOfChannels));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public float GetMemoryCASLatency()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR54);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryCASLatency msd = (CPUIDSDK_fp_GetMemoryCASLatency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryCASLatency));
                return msd(objptr);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public int GetMemoryRAStoCASDelay()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR55);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryRAStoCASDelay msd = (CPUIDSDK_fp_GetMemoryRAStoCASDelay)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryRAStoCASDelay));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetMemoryRASPrecharge()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR56);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryRASPrecharge msd = (CPUIDSDK_fp_GetMemoryRASPrecharge)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryRASPrecharge));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetMemoryTRAS()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR57);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryTRAS msd = (CPUIDSDK_fp_GetMemoryTRAS)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryTRAS));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetMemoryTRC()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR58);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryTRC msd = (CPUIDSDK_fp_GetMemoryTRC)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryTRC));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetMemoryCommandRate()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR59);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryCommandRate msd = (CPUIDSDK_fp_GetMemoryCommandRate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryCommandRate));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Chipset
//////////////////////////////////////////////////////////////////////////////////////////////////////////

    public string GetNorthBridgeVendor()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR60);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNorthBridgeVendor msd = (CPUIDSDK_fp_GetNorthBridgeVendor)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNorthBridgeVendor));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetNorthBridgeModel()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR61);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNorthBridgeModel msd = (CPUIDSDK_fp_GetNorthBridgeModel)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNorthBridgeModel));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetNorthBridgeRevision()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR62);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNorthBridgeRevision msd = (CPUIDSDK_fp_GetNorthBridgeRevision)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNorthBridgeRevision));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSouthBridgeVendor()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR63);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSouthBridgeVendor msd = (CPUIDSDK_fp_GetSouthBridgeVendor)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSouthBridgeVendor));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSouthBridgeModel()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR64);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSouthBridgeModel msd = (CPUIDSDK_fp_GetSouthBridgeModel)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSouthBridgeModel));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSouthBridgeRevision()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR65);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSouthBridgeRevision msd = (CPUIDSDK_fp_GetSouthBridgeRevision)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSouthBridgeRevision));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
 
    public void GetMemorySlotsConfig(ref int _nbslots, ref int _nbusedslots, ref int _slotmap_h, ref int _slotmap_l, ref int _maxmodulesize)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR87);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemorySlotsConfig msd = (CPUIDSDK_fp_GetMemorySlotsConfig)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemorySlotsConfig));
                msd(objptr, ref _nbslots, ref _nbusedslots, ref _slotmap_h, ref _slotmap_l, ref _maxmodulesize);
            }
        }
        catch
        {
        }
    }
    public string GetBIOSVendor()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR70);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetBIOSVendor msd = (CPUIDSDK_fp_GetBIOSVendor)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetBIOSVendor));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetBIOSVersion()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR71);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetBIOSVersion msd = (CPUIDSDK_fp_GetBIOSVersion)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetBIOSVersion));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetBIOSDate()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR72);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetBIOSDate msd = (CPUIDSDK_fp_GetBIOSDate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetBIOSDate));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetMainboardVendor()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR75);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMainboardVendor msd = (CPUIDSDK_fp_GetMainboardVendor)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMainboardVendor));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetMainboardModel()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR76);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMainboardModel msd = (CPUIDSDK_fp_GetMainboardModel)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMainboardModel));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetMainboardRevision()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR77);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMainboardRevision msd = (CPUIDSDK_fp_GetMainboardRevision)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMainboardRevision));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetMainboardSerialNumber()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR78);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMainboardSerialNumber msd = (CPUIDSDK_fp_GetMainboardSerialNumber)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMainboardSerialNumber));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSystemManufacturer()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR79);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSystemManufacturer msd = (CPUIDSDK_fp_GetSystemManufacturer)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSystemManufacturer));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSystemProductName()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR80);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSystemProductName msd = (CPUIDSDK_fp_GetSystemProductName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSystemProductName));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSystemVersion()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR81);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSystemVersion msd = (CPUIDSDK_fp_GetSystemVersion)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSystemVersion));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSystemSerialNumber()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR82);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSystemSerialNumber msd = (CPUIDSDK_fp_GetSystemSerialNumber)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSystemSerialNumber));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSystemUUID()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR83);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSystemUUID msd = (CPUIDSDK_fp_GetSystemUUID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSystemUUID));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetChassisManufacturer()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR84);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetChassisManufacturer msd = (CPUIDSDK_fp_GetChassisManufacturer)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetChassisManufacturer));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetChassisType()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR85);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetChassisType msd = (CPUIDSDK_fp_GetChassisType)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetChassisType));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetChassisSerialNumber()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR86);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetChassisSerialNumber msd = (CPUIDSDK_fp_GetChassisSerialNumber)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetChassisSerialNumber));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public bool GetMemoryInfosExt(ref string _szLocation, ref string _szUsage, ref string _szCorrection)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR88);
            if (ptr != IntPtr.Zero)
            {
                IntPtr location = IntPtr.Zero;
                IntPtr usage = IntPtr.Zero;
                IntPtr correction = IntPtr.Zero;

                CPUIDSDK_fp_GetMemoryInfosExt msd = (CPUIDSDK_fp_GetMemoryInfosExt)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryInfosExt));
                res = msd(objptr, ref location, ref usage, ref correction);
                if (res == 1)
                {
                    _szLocation = Marshal.PtrToStringAnsi(location);
                    Marshal.FreeBSTR(location);
                    _szUsage = Marshal.PtrToStringAnsi(usage);
                    Marshal.FreeBSTR(usage);
                    _szCorrection = Marshal.PtrToStringAnsi(correction);
                    Marshal.FreeBSTR(correction);

                    return true;
                }
                return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public int GetNumberOfMemoryDevices()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR89);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNumberOfMemoryDevices msd = (CPUIDSDK_fp_GetNumberOfMemoryDevices)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNumberOfMemoryDevices));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public bool GetMemoryDeviceInfos(int _device_index, ref int _size, ref string _szFormat)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR90);
            if (ptr != IntPtr.Zero)
            {
                IntPtr format = IntPtr.Zero;

                CPUIDSDK_fp_GetMemoryDeviceInfos msd = (CPUIDSDK_fp_GetMemoryDeviceInfos)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryDeviceInfos));
                res = msd(objptr, _device_index, ref _size, ref format);
                if (res == 1)
                {
                    _szFormat = Marshal.PtrToStringAnsi(format);
                    Marshal.FreeBSTR(format);
                    return true;
                }
                return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public bool GetMemoryDeviceInfosExt(int _device_index, ref string _szDesignation, ref string _szType, ref int _total_width, ref int _data_width, ref int _speed)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR91);
            if (ptr != IntPtr.Zero)
            {
                IntPtr designation = IntPtr.Zero;
                IntPtr type = IntPtr.Zero;

                CPUIDSDK_fp_GetMemoryDeviceInfosExt msd = (CPUIDSDK_fp_GetMemoryDeviceInfosExt)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryDeviceInfosExt));
                res = msd(objptr, _device_index, ref designation, ref type, ref _total_width, ref _data_width, ref _speed);
                if (res == 1)
                {
                    _szDesignation = Marshal.PtrToStringAnsi(designation);
                    Marshal.FreeBSTR(designation);

                    _szType = Marshal.PtrToStringAnsi(type);
                    Marshal.FreeBSTR(type);

                    return true;
                }
                return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public int GetMemoryMaxCapacity()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR95);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryMaxCapacity msd = (CPUIDSDK_fp_GetMemoryMaxCapacity)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryMaxCapacity));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetMemoryMaxNumberOfDevices()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR96);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMemoryMaxNumberOfDevices msd = (CPUIDSDK_fp_GetMemoryMaxNumberOfDevices)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMemoryMaxNumberOfDevices));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetProcessorSockets()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR92);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetProcessorSockets msd = (CPUIDSDK_fp_GetProcessorSockets)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetProcessorSockets));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }

    public string GetSystemSKU()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR93);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSystemSKU msd = (CPUIDSDK_fp_GetSystemSKU)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSystemSKU));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSystemFamily()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR94);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSystemFamily msd = (CPUIDSDK_fp_GetSystemFamily)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSystemFamily));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//  SPD
//////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int GetNumberOfSPDModules()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR100);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNumberOfSPDModules msd = (CPUIDSDK_fp_GetNumberOfSPDModules)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNumberOfSPDModules));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetSPDModuleType(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR102);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleType msd = (CPUIDSDK_fp_GetSPDModuleType)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleType));
                return msd(objptr, _spd_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetSPDModuleSize(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR103);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleSize msd = (CPUIDSDK_fp_GetSPDModuleSize)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleSize));
                return msd(objptr, _spd_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public string GetSPDModuleFormat(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR104);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleFormat msd = (CPUIDSDK_fp_GetSPDModuleFormat)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleFormat));
                IntPtr pointer = msd(objptr, _spd_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSPDModuleManufacturer(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR105);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleManufacturer msd = (CPUIDSDK_fp_GetSPDModuleManufacturer)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturer));
                IntPtr pointer = msd(objptr, _spd_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public bool GetSPDModuleManufacturerID(int _spd_index, byte[] _id)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR106);
            if (ptr != IntPtr.Zero)
            {

                CPUIDSDK_fp_GetSPDModuleManufacturerID msd = (CPUIDSDK_fp_GetSPDModuleManufacturerID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturerID));
                res = msd(objptr, _spd_index, _id);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public string GetSPDModuleDRAMManufacturer(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR130);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleDRAMManufacturer msd = (CPUIDSDK_fp_GetSPDModuleDRAMManufacturer)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleDRAMManufacturer));
                IntPtr pointer = msd(objptr, _spd_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public int GetSPDModuleMaxFrequency(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR110);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleMaxFrequency msd = (CPUIDSDK_fp_GetSPDModuleMaxFrequency)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleMaxFrequency));
                return msd(objptr, _spd_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public string GetSPDModuleSpecification(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR107);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleSpecification msd = (CPUIDSDK_fp_GetSPDModuleSpecification)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleSpecification));
                IntPtr pointer = msd(objptr, _spd_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSPDModulePartNumber(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR108);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModulePartNumber msd = (CPUIDSDK_fp_GetSPDModulePartNumber)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModulePartNumber));
                IntPtr pointer = msd(objptr, _spd_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetSPDModuleSerialNumber(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR109);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleSerialNumber msd = (CPUIDSDK_fp_GetSPDModuleSerialNumber)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleSerialNumber));
                IntPtr pointer = msd(objptr, _spd_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public float GetSPDModuleMinTRCD(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR111);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleMinTRCD msd = (CPUIDSDK_fp_GetSPDModuleMinTRCD)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRCD));
                return msd(objptr, _spd_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetSPDModuleMinTRP(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR112);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleMinTRP msd = (CPUIDSDK_fp_GetSPDModuleMinTRP)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRP));
                return msd(objptr, _spd_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetSPDModuleMinTRAS(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR113);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleMinTRAS msd = (CPUIDSDK_fp_GetSPDModuleMinTRAS)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRAS));
                return msd(objptr, _spd_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetSPDModuleMinTRC(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR114);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleMinTRC msd = (CPUIDSDK_fp_GetSPDModuleMinTRC)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRC));
                return msd(objptr, _spd_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public int GetSPDModuleManufacturingDate(int _spd_index, ref int _year, ref int _week)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR115);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleManufacturingDate msd = (CPUIDSDK_fp_GetSPDModuleManufacturingDate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturingDate));
                return msd(objptr, _spd_index, ref _year, ref _week);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetSPDModuleNumberOfBanks(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR116);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleNumberOfBanks msd = (CPUIDSDK_fp_GetSPDModuleNumberOfBanks)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfBanks));
                return msd(objptr, _spd_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetSPDModuleDataWidth(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR117);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleDataWidth msd = (CPUIDSDK_fp_GetSPDModuleDataWidth)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleDataWidth));
                return msd(objptr, _spd_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public float GetSPDModuleTemperature(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR132);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleTemperature msd = (CPUIDSDK_fp_GetSPDModuleTemperature)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleTemperature));
                return msd(objptr, _spd_index);
            }
            return F_UNDEFINED_VALUE;
        }
        catch
        {
            return F_UNDEFINED_VALUE;
        }
    }
    public int GetSPDModuleNumberOfProfiles(int _spd_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR119);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleNumberOfProfiles msd = (CPUIDSDK_fp_GetSPDModuleNumberOfProfiles)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfProfiles));
                return msd(objptr, _spd_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public void GetSPDModuleProfileInfos(int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _nominal_vdd)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR120);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleProfileInfos msd = (CPUIDSDK_fp_GetSPDModuleProfileInfos)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleProfileInfos));
                msd(objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _nominal_vdd);
            }
        }
        catch
        {
        }
    }
    public int GetSPDModuleNumberOfEPPProfiles(int _spd_index, ref int _epp_revision)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR121);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles msd = (CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles));
                return msd(objptr, _spd_index, ref _epp_revision);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public void GetSPDModuleEPPProfileInfos(int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR122);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleEPPProfileInfos msd = (CPUIDSDK_fp_GetSPDModuleEPPProfileInfos)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleEPPProfileInfos));
                msd(objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC, ref _nominal_vdd);
            }
        }
        catch
        {
        }
    }
    public int GetSPDModuleNumberOfXMPProfiles(int _spd_index, ref int _xmp_revision)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR123);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles msd = (CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles));
                return msd(objptr, _spd_index, ref _xmp_revision);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetSPDModuleXMPProfileNumberOfCL(int _spd_index, int _profile_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR124);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL msd = (CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL));
                return msd(objptr, _spd_index, _profile_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public void GetSPDModuleXMPProfileCLInfos(int _spd_index, int _profile_index, int _cl_index, ref float _frequency, ref float _CL)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR125);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos msd = (CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos));
                msd(objptr, _spd_index, _profile_index, _cl_index, ref _frequency, ref _CL);
            }
        }
        catch
        {
        }
    }
    public void GetSPDModuleXMPProfileInfos(int _spd_index, int _profile_index, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _nominal_vdd, ref int _max_freq, ref float _max_CL)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR126);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleXMPProfileInfos msd = (CPUIDSDK_fp_GetSPDModuleXMPProfileInfos)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileInfos));
                msd(objptr, _spd_index, _profile_index, ref _tRCD, ref _tRAS, ref _tRP, ref _nominal_vdd, ref _max_freq, ref _max_CL);
            }
        }
        catch
        {
        }
    }
    public int GetSPDModuleNumberOfAMPProfiles(int _spd_index, ref int _amp_revision)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR127);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles msd = (CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles));
                return msd(objptr, _spd_index, ref _amp_revision);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public void GetSPDModuleAMPProfileInfos(int _spd_index, int _profile_index, ref int _frequency, ref float _min_cycle_time, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR128);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleAMPProfileInfos msd = (CPUIDSDK_fp_GetSPDModuleAMPProfileInfos)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleAMPProfileInfos));
                msd(objptr, _spd_index, _profile_index, ref _frequency, ref _min_cycle_time, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC);
            }
        }
        catch
        {
        }
    }
    public int GetSPDModuleRawData(int _spd_index, int _offset)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR129);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetSPDModuleRawData msd = (CPUIDSDK_fp_GetSPDModuleRawData)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetSPDModuleRawData));
                return msd(objptr, _spd_index, _offset);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Display
//////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int GetNumberOfDisplayAdapter()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR140);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNumberOfDisplayAdapter msd = (CPUIDSDK_fp_GetNumberOfDisplayAdapter)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNumberOfDisplayAdapter));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    //public int GetDisplayAdapterID(int _adapter_index)
    //{
    //    try
    //    {
    //        IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR);
    //        if (ptr != IntPtr.Zero)
    //        {
    //            CPUIDSDK_fp_GetDisplayAdapterID msd = (CPUIDSDK_fp_GetDisplayAdapterID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterID));
    //            return msd(objptr, _adapter_index);
    //        }
    //        return I_UNDEFINED_VALUE;
    //    }
    //    catch
    //    {
    //        return I_UNDEFINED_VALUE;
    //    }
    //}
    public string GetDisplayAdapterName(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR142);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterName msd = (CPUIDSDK_fp_GetDisplayAdapterName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterName));
                IntPtr pointer = msd(objptr, _adapter_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetDisplayAdapterCodeName(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR143);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterCodeName msd = (CPUIDSDK_fp_GetDisplayAdapterCodeName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterCodeName));
                IntPtr pointer = msd(objptr, _adapter_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public int GetDisplayAdapterNumberOfPerformanceLevels(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR144);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels msd = (CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels));
                return msd(objptr, _adapter_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetDisplayAdapterCurrentPerformanceLevel(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR145);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel msd = (CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel));
                return msd(objptr, _adapter_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public string GetDisplayAdapterPerformanceLevelName(int _adapter_index, int _perf_level)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR146);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName msd = (CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName));
                IntPtr pointer = msd(objptr, _adapter_index, _perf_level);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public float GetDisplayAdapterClock(int _adapter_index, int _perf_level, int _domain)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR147);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterClock msd = (CPUIDSDK_fp_GetDisplayAdapterClock)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterClock));
                return msd(objptr, _adapter_index, _perf_level, _domain);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetDisplayAdapterStockClock(int _adapter_index, int _perf_level, int _domain)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR148);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterStockClock msd = (CPUIDSDK_fp_GetDisplayAdapterStockClock)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterStockClock));
                return msd(objptr, _adapter_index, _perf_level, _domain);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetDisplayAdapterManufacturingProcess(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR159);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess msd = (CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess));
                return msd(objptr, _adapter_index);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public float GetDisplayAdapterTemperature(int _adapter_index, int _domain)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR149);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterTemperature msd = (CPUIDSDK_fp_GetDisplayAdapterTemperature)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterTemperature));
                return msd(objptr, _adapter_index, _domain);
            }
            return (float)F_UNDEFINED_VALUE;
        }
        catch
        {
            return (float)F_UNDEFINED_VALUE;
        }
    }
    public int GetDisplayAdapterFanSpeed(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR150);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterFanSpeed msd = (CPUIDSDK_fp_GetDisplayAdapterFanSpeed)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterFanSpeed));
                return msd(objptr, _adapter_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetDisplayAdapterFanPWM(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR151);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterFanPWM msd = (CPUIDSDK_fp_GetDisplayAdapterFanPWM)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterFanPWM));
                return msd(objptr, _adapter_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public bool GetDisplayAdapterMemoryType(int _adapter_index, ref int _type)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR152);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterMemoryType msd = (CPUIDSDK_fp_GetDisplayAdapterMemoryType)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryType));
                res = msd(objptr, _adapter_index, ref _type);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public bool GetDisplayAdapterMemorySize(int _adapter_index, ref int _size)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR153);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterMemorySize msd = (CPUIDSDK_fp_GetDisplayAdapterMemorySize)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemorySize));
                res = msd(objptr, _adapter_index, ref _size);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public bool GetDisplayAdapterMemoryBusWidth(int _adapter_index, ref int _bus_width)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR154);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth msd = (CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth));
                res = msd(objptr, _adapter_index, ref _bus_width);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public string GetDisplayAdapterMemoryVendor(int _adapter_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR171);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterMemoryVendor msd = (CPUIDSDK_fp_GetDisplayAdapterMemoryVendor)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryVendor));
                IntPtr pointer = msd(objptr, _adapter_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetDisplayDriverVersion()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR155);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayDriverVersion msd = (CPUIDSDK_fp_GetDisplayDriverVersion)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayDriverVersion));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetDirectXVersion()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR156);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDirectXVersion msd = (CPUIDSDK_fp_GetDirectXVersion)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDirectXVersion));
                IntPtr pointer = msd(objptr);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public bool GetDisplayAdapterBusInfos(int _adapter_index, ref int _bus_type, ref int _multi_vpu)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR157);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterBusInfos msd = (CPUIDSDK_fp_GetDisplayAdapterBusInfos)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterBusInfos));
                res = msd(objptr, _adapter_index, ref _bus_type, ref _multi_vpu);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public string GetDisplayAdapterCoreFamily(int _adapter_index, ref int _core)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR170);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterCoreFamily msd = (CPUIDSDK_fp_GetDisplayAdapterCoreFamily)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterCoreFamily));
                IntPtr pointer = msd(objptr, _adapter_index, ref _core);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

//  EDID
    public int GetNumberOfMonitors()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR160);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNumberOfMonitors msd = (CPUIDSDK_fp_GetNumberOfMonitors)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNumberOfMonitors));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public string GetMonitorName(int _monitor_index)
    {
       try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR161);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorName msd = (CPUIDSDK_fp_GetMonitorName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorName));
                IntPtr pointer = msd(objptr, _monitor_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetMonitorVendor(int _monitor_index)
    {
       try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR162);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorVendor msd = (CPUIDSDK_fp_GetMonitorVendor)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorVendor));
                IntPtr pointer = msd(objptr, _monitor_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetMonitorID(int _monitor_index)
    {
       try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR163);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorID msd = (CPUIDSDK_fp_GetMonitorID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorID));
                IntPtr pointer = msd(objptr, _monitor_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetMonitorSerial(int _monitor_index)
    {
       try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR164);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorSerial msd = (CPUIDSDK_fp_GetMonitorSerial)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorSerial));
                IntPtr pointer = msd(objptr, _monitor_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public bool GetMonitorManufacturingDate(int _monitor_index, ref int _week, ref int _year)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR165);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorManufacturingDate msd = (CPUIDSDK_fp_GetMonitorManufacturingDate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorManufacturingDate));
                res = msd(objptr, _monitor_index, ref _week, ref _year);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public float GetMonitorSize(int _monitor_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR166);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorSize msd = (CPUIDSDK_fp_GetMonitorSize)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorSize));
                return msd(objptr, _monitor_index);
            }
            return F_UNDEFINED_VALUE;
        }
        catch
        {
            return F_UNDEFINED_VALUE;
        }
    }
    public bool GetMonitorResolution(int _monitor_index, ref int _width, ref int _height, ref int _frequency)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR167);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorResolution msd = (CPUIDSDK_fp_GetMonitorResolution)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorResolution));
                res = msd(objptr, _monitor_index, ref _width, ref _height, ref _frequency);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public int GetMonitorMaxPixelClock(int _monitor_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR168);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorMaxPixelClock msd = (CPUIDSDK_fp_GetMonitorMaxPixelClock)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorMaxPixelClock));
                return msd(objptr, _monitor_index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public float GetMonitorGamma(int _monitor_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR169);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetMonitorGamma msd = (CPUIDSDK_fp_GetMonitorGamma)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetMonitorGamma));
                return msd(objptr, _monitor_index);
            }
            return F_UNDEFINED_VALUE;
        }
        catch
        {
            return F_UNDEFINED_VALUE;
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////
//  HDD
//////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int GetNumberOfStorageDevice()
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR175);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetNumberOfStorageDevice msd = (CPUIDSDK_fp_GetNumberOfStorageDevice)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetNumberOfStorageDevice));
                return msd(objptr);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetStorageDriveNumber(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR176);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDriveNumber msd = (CPUIDSDK_fp_GetStorageDriveNumber)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDriveNumber));
                return msd(objptr, _index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public string GetStorageDeviceName(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR177);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceName msd = (CPUIDSDK_fp_GetStorageDeviceName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceName));
                IntPtr pointer = msd(objptr, _index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetStorageDeviceRevision(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR178);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceRevision msd = (CPUIDSDK_fp_GetStorageDeviceRevision)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceRevision));
                IntPtr pointer = msd(objptr, _index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public string GetStorageDeviceSerialNumber(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR179);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceSerialNumber msd = (CPUIDSDK_fp_GetStorageDeviceSerialNumber)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceSerialNumber));
                IntPtr pointer = msd(objptr, _index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public int GetStorageDeviceBusType(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR180);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceBusType msd = (CPUIDSDK_fp_GetStorageDeviceBusType)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceBusType));
                return msd(objptr, _index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetStorageDeviceRotationSpeed(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR181);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceRotationSpeed msd = (CPUIDSDK_fp_GetStorageDeviceRotationSpeed)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceRotationSpeed));
                return msd(objptr, _index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public uint GetStorageDeviceFeatureFlag(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR182);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceFeatureFlag msd = (CPUIDSDK_fp_GetStorageDeviceFeatureFlag)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceFeatureFlag));
                return (uint)msd(objptr, _index);
            }
            return (uint)I_UNDEFINED_VALUE;
        }
        catch
        {
            return (uint)I_UNDEFINED_VALUE;
        }
    }
    public int GetStorageDeviceNumberOfVolumes(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR183);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes msd = (CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes));
                return msd(objptr, _index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public string GetStorageDeviceVolumeLetter(int _index, int _volume_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR184);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceVolumeLetter msd = (CPUIDSDK_fp_GetStorageDeviceVolumeLetter)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeLetter));
                IntPtr pointer = msd(objptr, _index, _volume_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
    public float GetStorageDeviceVolumeTotalCapacity(int _index, int _volume_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR185);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity msd = (CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity));
                return msd(objptr, _index, _volume_index);
            }
            return F_UNDEFINED_VALUE;
        }
        catch
        {
            return F_UNDEFINED_VALUE;
        }
    }
    public float GetStorageDeviceVolumeAvailableCapacity(int _index, int _volume_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR186);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity msd = (CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity));
                return msd(objptr, _index, _volume_index);
            }
            return F_UNDEFINED_VALUE;
        }
        catch
        {
            return F_UNDEFINED_VALUE;
        }
    }
    public bool GetStorageDeviceSmartAttribute(int _hdd_index, int _attrib_index, ref int _id, ref int _flags, ref int _value, ref int _worst, byte[] _data)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR187);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceSmartAttribute msd = (CPUIDSDK_fp_GetStorageDeviceSmartAttribute)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceSmartAttribute));
                res = msd(objptr, _hdd_index, _attrib_index, ref _id, ref _flags, ref _value, ref _worst, _data);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public int GetStorageDevicePowerOnHours(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR188);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDevicePowerOnHours msd = (CPUIDSDK_fp_GetStorageDevicePowerOnHours)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDevicePowerOnHours));
                return msd(objptr, _index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public int GetStorageDevicePowerCycleCount(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR189);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDevicePowerCycleCount msd = (CPUIDSDK_fp_GetStorageDevicePowerCycleCount)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDevicePowerCycleCount));
                return msd(objptr, _index);
            }
            return I_UNDEFINED_VALUE;
        }
        catch
        {
            return I_UNDEFINED_VALUE;
        }
    }
    public float GetStorageDeviceTotalCapacity(int _index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR190);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetStorageDeviceTotalCapacity msd = (CPUIDSDK_fp_GetStorageDeviceTotalCapacity)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetStorageDeviceTotalCapacity));
                return msd(objptr, _index);
            }
            return F_UNDEFINED_VALUE;
        }
        catch
        {
            return F_UNDEFINED_VALUE;
        }
    }

    public string GetDeviceName(int _device_index)
    {
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR202);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDeviceName msd = (CPUIDSDK_fp_GetDeviceName)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDeviceName));
                IntPtr pointer = msd(objptr, _device_index);
                string tmp = Marshal.PtrToStringAnsi(pointer);
                Marshal.FreeBSTR(pointer);
                return tmp;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public bool GetDisplayAdapterPCIID(int _adapter_index, ref int _vendor_id, ref int _device_id, ref int _revision_id, ref int _sub_vendor_id, ref int _sub_model_id)
    {
        int res;
        try
        {
            IntPtr ptr = CPUIDSDK_fp_QueryInterface((uint)PTR.PTR158);
            if (ptr != IntPtr.Zero)
            {
                CPUIDSDK_fp_GetDisplayAdapterPCIID msd = (CPUIDSDK_fp_GetDisplayAdapterPCIID)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CPUIDSDK_fp_GetDisplayAdapterPCIID));
                res = msd(objptr, _adapter_index, ref _vendor_id, ref _device_id, ref _revision_id, ref _sub_vendor_id, ref _sub_model_id);
                if (res == 1) return true;
                else return false;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}