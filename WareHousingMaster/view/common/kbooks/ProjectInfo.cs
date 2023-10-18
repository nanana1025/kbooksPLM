using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.usedPurchase;

namespace WareHousingMaster.view.common
{
    static class ProjectInfo
    {

        public static System.Drawing.Icon ProjectIcon { get { return Properties.Resources.favicon; } }

#if DEBUG
        public static bool _ISDEBUG = true;
#else
        public static bool _ISDEBUG = false;
#endif

#if DEBUG
        //public static string _url = "https://dangol365.com";
        //public static string _url = "http://211.202.79.253";
        //public static string _url = "https://testcs.asworld.co.kr";
        //public static string _url = "http://localhost:5555";
        public static string _url = "http://3.39.55.175";
#else
        public static string _url = "https://tests1.asworld.co.kr";
        
        
#endif


        public static TabbedView _tabbedView;
        public static DocumentManager _documentManager;
        public static Dictionary<BarButtonItem, XtraForm> _ribbonTabs;

        public static usrBookOrder _usrBookOrder;

        public static BarButtonItem _bbiOrderCartInfo;


        public static int _endStore = 12;

        public static List<string> _LIST_USER_AUTHORTY_TAB;
        public static List<string> _LIST_USER_AUTHORTY;

        public static string _USER_TYPE; //ADMIN, USER






























        //public static string _asisUrl = "https://asisapi.gochigo.kr";


























        //public static usrUsedPurchaseReceiptDetail _usedPurchaseReceiptDetail;

        //public static BarButtonItem _biusrUsedPurchaseReceiptDetail = new BarButtonItem();

        //public static List<string> _lisAllowUser = new List<string>(new[] { "jblee", "rookieson", "shlee", "lta100" });

        //public static string[,] _arrDeviceColumn = new string[6,11]
        //{
        //   { "ID", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT", "NO", "NAME", "MANUFACTURE_NM", "MODEL_NM", "SOCKET_NM", "CODE_NM"},
        //   { "ID", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT", "NO", "NAME", "MANUFACTURE_NM", "MODEL_NM", "MEM_TYPE", "MEM_SIZE"},
        //   { "ID", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT", "NO", "NAME", "MANUFACTURE_NM", "BANDWIDTH", "CAPACITY", "VOLTAGE"},
        //   { "ID", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT", "NO", "NAME", "MANUFACTURE_NM", "STG_TYPE", "CAPACITY", "SPEED"},
        //   { "ID", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT", "NO", "NAME", "MANUFACTURE_NM", "MODEL_NM", "CAPACITY", "MEM_TYPE"},
        //   { "ID", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT", "NO", "NAME", "MANUFACTURE_NM", "MODEL_NM", "SIZE", "RESOLUTION"},
        //};

        //{ "CPU", "MBD", "MEM", "VGA", "STG", "MON", "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT", "PKG", "AIR", "LIC", "PER" }

        //        public static List<string> _listWin32 = new List<string>(new[] {
        //        "Win32_BaseBoard",
        //        "Win32_Battery",
        //        "Win32_BIOS",
        //        "Win32_Bus",
        //        "Win32_CDROMDrive",
        //        "Win32_DiskDrive",
        //        "Win32_DMAChannel",
        //        "Win32_Fan",
        //        "Win32_FloppyController",
        //        "Win32_FloppyDrive",
        //        "Win32_IDEController",
        //        "Win32_IRQResource",
        //        "Win32_Keyboard",
        //        "Win32_MemoryDevice",
        //        "Win32_NetworkAdapter",
        //        "Win32_NetworkAdapterConfiguration",
        //        "Win32_OnBoardDevice",
        //        "Win32_ParallelPort",
        //        "Win32_PCMCIAController",
        //        "Win32_PhysicalMedia",
        //        "Win32_PhysicalMemory",
        //        "Win32_PortConnector",
        //        "Win32_PortResource",
        //        "Win32_Processor",
        //        "Win32_SCSIController",
        //        "Win32_SerialPort",
        //        "Win32_SerialPortConfiguration",
        //        "Win32_SoundDevice",
        //        "Win32_SystemEnclosure",
        //        "Win32_TapeDrive",
        //        "Win32_TemperatureProbe",
        //        "Win32_UninterruptiblePowerSupply",
        //        "Win32_USBController",
        //        "Win32_USBHub",
        //        "Win32_VideoController",
        //        "Win32_VoltageProbe"
        //        });



        //        public static Dictionary<string, List<string>> _dicDeviceFullColumn = new Dictionary<string, List<string>>()
        //        {
        //            {"CPU", new List<string>(new[]{"MANUFACTURE_NM", "CODE_NM", "MODEL_NM", "SPEC_NM", "SOCKET_NM", "CORE_CNT", "THREAD_CNT"}) },
        //            {"MBD", new List<string>(new[]{"MANUFACTURE_NM", "NB_NM", "CODE_NM","SB_NM", "MODEL_NM", "MAX_MEM", "NO_OF_DIMM", "MEM_TYPE", "SKU_NM", "SERIAL_NO", "FAMILY_NM", "MBD_MODEL_NM", "PRODUCT_NAME", "REVISION", "SYSTEM_VERSION", "BIOS_VENDOR", "BIOS_VERSION", "BIOS_DATE", "UUID", "CHASSIS_MANUFACTURER", "CHASSIS_SN", "PROCESSOR_SOCKET", "MBD_SN", "SYSTEM_SN", "MEM_SIZE", "NO_OF_MEM_DEVICE", "MAC_ADDRESS", "OS_PRODUCT_KEY", "OS_SERIAL_NO", "OS_PRODUCT_ID", "OS_OEM_KEY", "OS_NAME"}) },
        //            {"MEM", new List<string>(new[]{"MANUFACTURE_NM", "MEM_TYPE", "MODEL_NM", "MODULE_NM", "CAPACITY", "BANDWIDTH", "MANUFACTURE_DT", "VOLTAGE", "SERIAL_NO"}) },
        //            {"STG", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "STG_TYPE", "CAPACITY", "CAPACITY_M", "SERIAL_NO", "BUS_TYPE", "FEATURE", "SPEED", "REVISION"}) },
        //            {"VGA", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "REVISION", "CODE_NM", "PROCESS", "CAPACITY", "MEM_TYPE", "TECH_NM", "EX_TYPE", "SERIAL_NO"}) },
        //            {"MON", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "MODEL_ID", "SIZE", "RESOLUTION", "MANUFACTURED_DT", "SERIAL_NO", "DEVICE_NAME", "GAMMA", "MAX_PIXEL"}) },

        //            {"ODD", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "TYPE"}) },
        //            {"CAS", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "CASE_CAT","CASE_TYPE"}) },
        //            {"ADP", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "ADP_CAT", "OUTPUT_AMPERE", "OUTPUT_WATT"}) },
        //            {"POW", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "POW_CAT", "POW_TYPE", "POW_CLASS"}) },
        //            {"KEY", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "KEY_CAT", "KEY_TYPE", "KEY_CLASS"}) },
        //            {"MOU", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "MOU_CAT", "MOU_TYPE"}) },
        //            {"FAN", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "FAN_CAT", "FAN_TYPE"}) },
        //            {"CAB", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "CAB_CAT", "CAB_TYPE", "CAB_CLASS"}) },
        //            {"BAT", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "BAT_CAT", "OUTPUT_AMPERE", "OUTPUT_WATT"}) },
        //            {"PKG", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "PACKAGE_TYPE", "CATEGORY", "SIZE"}) },
        //            {"AIR", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "TYPE", "CATEGORY", "SIZE"}) },
        //            {"LIC", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "PART_NAME", "TYPE", "ETC"}) },
        //            {"PER", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "PART_NAME", "TYPE", "ETC"}) },
        //            {"TBL", new List<string>(new[]{"MANUFACTURE_NM", "PRODUCT_NM", "MODEL_NM", "CAPACITY", "RESOLUTION","DISPLAY_SIZE"}) }

        //        };

        //        public static Dictionary<string, List<string>> _dicDeviceColumn = new Dictionary<string, List<string>>()
        //        {
        //            {"ALL", new List<string>(new[]{"DATA1", "DATA2", "DATA3", "DATA4"}) },
        //            {"CPU", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "SOCKET_NM", "CODE_NM"}) },
        //            {"MBD", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "PRODUCT_NAME", "SB_NM"}) },
        //            {"MEM", new List<string>(new[]{"MANUFACTURE_NM", "BANDWIDTH", "CAPACITY", "VOLTAGE"}) },
        //            {"STG", new List<string>(new[]{"MODEL_NM", "STG_TYPE", "CAPACITY_M", "BUS_TYPE"}) },
        //            {"VGA", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "CAPACITY", "MEM_TYPE"}) },
        //            {"MON", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "SIZE", "RESOLUTION"}) },

        //            {"ODD", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "TYPE", "TYPE"}) },
        //            {"CAS", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "CASE_CAT","CASE_TYPE"}) },
        //            {"ADP", new List<string>(new[]{"MODEL_NM", "ADP_CAT", "OUTPUT_AMPERE", "OUTPUT_WATT"}) },
        //            {"POW", new List<string>(new[]{"MODEL_NM", "POW_CAT", "POW_TYPE","POW_CLASS"}) },
        //            {"KEY", new List<string>(new[]{"MODEL_NM", "KEY_CAT", "KEY_TYPE", "KEY_CLASS"}) },
        //            {"MOU", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "MOU_CAT", "MOU_TYPE"}) },
        //            {"FAN", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "FAN_CAT", "FAN_TYPE"}) },
        //            {"CAB", new List<string>(new[]{"MODEL_NM", "CAB_CAT", "CAB_TYPE", "CAB_CLASS"}) },
        //            {"BAT", new List<string>(new[]{"MODEL_NM", "BAT_CAT", "OUTPUT_AMPERE", "OUTPUT_WATT"}) },
        //            {"PKG", new List<string>(new[]{"MODEL_NM", "PACKAGE_TYPE", "CATEGORY", "SIZE"}) },
        //            {"AIR", new List<string>(new[]{"MODEL_NM", "TYPE", "CATEGORY", "SIZE"}) },
        //            {"LIC", new List<string>(new[]{ "TYPE", "MANUFACTURE_NM", "MODEL_NM", "ETC"}) },
        //            {"PER", new List<string>(new[]{ "TYPE", "MANUFACTURE_NM", "MODEL_NM", "ETC"}) },
        //            {"TBL", new List<string>(new[]{ "MANUFACTURE_NM", "PRODUCT_NM", "MODEL_NM","CAPACITY"}) }
        //        };

        //        public static Dictionary<string, List<string>> _dicDeviceConsignedColumn = new Dictionary<string, List<string>>()
        //        {
        //            {"ALL", new List<string>(new[]{"REP1", "REP2", "REP3"}) },
        //            {"CPU", new List<string>(new[]{"MODEL_NM", "SOCKET_NM", "CODE_NM"}) },
        //            {"MBD", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "PRODUCT_NAME"}) },
        //            {"MEM", new List<string>(new[]{"MANUFACTURE_NM", "BANDWIDTH", "CAPACITY"}) },
        //            {"STG", new List<string>(new[]{"STG_TYPE", "BUS_TYPE", "CAPACITY_M"}) },
        //            {"VGA", new List<string>(new[]{"MODEL_NM", "CAPACITY", "MEM_TYPE"}) },
        //            {"MON", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "SIZE"}) },

        //            {"ODD", new List<string>(new[]{"MANUFACTURE_NM", "MODEL_NM", "TYPE"}) },
        //            {"CAS", new List<string>(new[]{"MODEL_NM", "CASE_CAT","CASE_TYPE"}) },
        //            {"ADP", new List<string>(new[]{"ADP_CAT", "OUTPUT_AMPERE", "OUTPUT_WATT"}) },
        //            {"POW", new List<string>(new[]{"POW_CAT", "POW_TYPE","POW_CLASS"}) },
        //            {"KEY", new List<string>(new[]{"KEY_CAT", "KEY_TYPE", "KEY_CLASS"}) },
        //            {"MOU", new List<string>(new[]{"MODEL_NM", "MOU_CAT", "MOU_TYPE"}) },
        //            {"FAN", new List<string>(new[]{"MODEL_NM", "FAN_CAT", "FAN_TYPE"}) },
        //            {"CAB", new List<string>(new[]{"CAB_CAT", "CAB_TYPE", "CAB_CLASS"}) },
        //            {"BAT", new List<string>(new[]{"BAT_CAT", "OUTPUT_AMPERE", "OUTPUT_WATT"}) },
        //            {"PKG", new List<string>(new[]{"PACKAGE_TYPE", "CATEGORY", "SIZE"}) },
        //            {"AIR", new List<string>(new[]{"TYPE", "CATEGORY", "SIZE"}) },
        //            {"LIC", new List<string>(new[]{"TYPE", "MODEL_NM",  "ETC"}) },
        //            {"PER", new List<string>(new[]{"TYPE", "MODEL_NM",  "ETC"}) },
        //            {"TBL", new List<string>(new[]{ "MANUFACTURE_NM", "PRODUCT_NM",  "MODEL_NM"}) }
        //        };

        //        public static Dictionary<string, List<string>> _dicDeviceColumnNm = new Dictionary<string, List<string>>()
        //        {
        //            {"ALL", new List<string>(new[]{ "부품정보1", "부품정보2", "부품정보3", "부품정보4"}) },
        //            {"CPU", new List<string>(new[]{ "제조사", "모델명", "소켓명", "코드명"}) },
        //            {"MBD", new List<string>(new[]{ "제조사", "모델명1", "모델명2", "SB"}) },
        //            {"MEM", new List<string>(new[]{ "제조사", "대역", "용량", "전압"}) },
        //            {"STG", new List<string>(new[]{ "모델명", "STG타입", "용량", "BUS타입"}) },
        //            {"VGA", new List<string>(new[]{ "제조사", "모델명", "용량", "메모리타입"}) },
        //            {"MON", new List<string>(new[]{ "제조사", "모델명", "사이즈", "해상도"}) },

        //            {"ODD", new List<string>(new[]{ "제조사", "모델명", "타입"}) },
        //            {"CAS", new List<string>(new[]{"제조사", "모델명", "케이스종류","케이스타입"}) },
        //            {"ADP", new List<string>(new[]{"모델명", "어댑터종류", "출력A", "출력V"}) },
        //            {"POW", new List<string>(new[]{ "모델명", "파워종류", "파워타입","CLASS"}) },
        //            {"KEY", new List<string>(new[]{ "모델명", "유무선", "키보드타입", "키보드종류"}) },
        //            {"MOU", new List<string>(new[]{ "제조사", "모델명", "유무선", "마우스타입"}) },
        //            {"FAN", new List<string>(new[]{ "제조사", "모델명", "팬종류", "팬타입"}) },
        //            {"CAB", new List<string>(new[]{ "모델명", "케이블종류", "케이블타입", "CLASS"}) },
        //            {"BAT", new List<string>(new[]{ "모델명", "배터리종류", "출력A", "출력V"}) },
        //            {"PKG", new List<string>(new[]{ "모델명", "박스타입", "종류", "크기"}) },
        //            {"AIR", new List<string>(new[]{ "모델명", "에어타입", "종류", "크기"}) },
        //            {"LIC", new List<string>(new[]{ "종류", "제조사", "모델명", "기타"}) },
        //            {"PER", new List<string>(new[]{ "종류", "제조사", "모델명", "기타"}) },
        //            {"TBL", new List<string>(new[]{ "제조사", "제품명", "모델명", "용량"}) }
        //        };

        //        public static Dictionary<string, List<string>> _dicDeviceConsignedColumnNm = new Dictionary<string, List<string>>()
        //        {
        //            {"ALL", new List<string>(new[]{ "부품정보1", "부품정보2", "부품정보3"}) },
        //            {"CPU", new List<string>(new[]{"모델명", "소켓명", "코드명"}) },
        //            {"MBD", new List<string>(new[]{"제조사", "모델명1", "모델명2"}) },
        //            {"MEM", new List<string>(new[]{"대역", "용량", "전압"}) },
        //            {"STG", new List<string>(new[]{ "STG타입", "BUS타입", "용량"}) },
        //            {"VGA", new List<string>(new[]{ "모델명", "용량", "메모리타입"}) },
        //            {"MON", new List<string>(new[]{ "모델명", "사이즈", "해상도"}) },

        //            {"ODD", new List<string>(new[]{ "제조사", "모델명", "타입"}) },
        //            {"CAS", new List<string>(new[]{"모델명", "케이스종류","케이스타입"}) },
        //            {"ADP", new List<string>(new[]{ "어댑터종류", "출력A", "출력V"}) },
        //            {"POW", new List<string>(new[]{ "파워종류", "파워타입","CLASS"}) },
        //            {"KEY", new List<string>(new[]{ "유무선", "키보드타입", "키보드종류"}) },
        //            {"MOU", new List<string>(new[]{ "모델명", "유무선", "마우스타입"}) },
        //            {"FAN", new List<string>(new[]{ "모델명", "팬종류", "팬타입"}) },
        //            {"CAB", new List<string>(new[]{ "케이블종류", "케이블타입", "CLASS"}) },
        //            {"BAT", new List<string>(new[]{ "배터리종류", "출력A", "출력V"}) },
        //            {"PKG", new List<string>(new[]{ "박스타입", "종류", "크기"}) },
        //            {"AIR", new List<string>(new[]{ "에어타입", "종류", "크기"}) },
        //            {"LIC", new List<string>(new[]{ "종류", "모델명", "기타"}) },
        //            {"PER", new List<string>(new[]{ "종류", "모델명", "기타"}) },
        //            {"TBL", new List<string>(new[]{ "제조사", "제품명", "모델명"}) }
        //        };

        //        public static List<string> _listCommonColumn = new List<string>(new[] { "ID", "NAME", "NO", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT" });
        //        public static List<string> _listKeyColumn = new List<string>(new[] { "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT" });
        //        public static List<string> _listCheckException = new List<string>(new[] { "CHECK_ID", "INVENTORY_ID", "CHECK_TYPE", "USER_ID", "START_DT", "END_DT", "CREATE_DT", "UPDATE_DT", "UPDATE_USER_ID", "COMPONENT_CD" });

        //        public static List<string> _listDefaultConsignedColum = new List<string>(new[] { "ID", "COMPONENT_CD", "INVENTORY_ID", "COMPONENT_ID", "BARCODE", "COMPONENT", "WAREHOUSE", "PALLET", "NO", "NO1", "NAME", "CHECK", "INVENTORY_YN", "PRODUCT_YN", "MANUFACTURE_NM", "MODEL_NM", "SOCKET_NM", "CODE_NM", "MEM_TYPE", "MEM_SIZE", "BANDWIDTH", "CAPACITY", "PRODUCT_NAME", "VOLTAGE", "STG_TYPE", "BUS_TYPE", "SPEED", "SIZE" });
        //        public static List<string> _listUncheckComponentCd = new List<string>(new[] { "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT", "PKG", "AIR", "LIC", "PER", "TBL" });
        //        public static List<string> _listUncheckComponentCdNm = new List<string>(new[] { "케이스", "어댑터", "파워", "키보드", "마우스", "팬(쿨러)", "케이블", "배터리", "박스", "에어", "라이센스", "주변기기", "태블릿" });


        //        //public static string[,] _arrDeviceColumnNm = new string[7, 4]
        //        //{        
        //        //   { "제조사", "모델명", "소켓명", "코드명"},
        //        //   { "제조사", "모델명", "메모리타입", "메모리사이즈"},
        //        //   { "제조사", "대역", "용량", "전압"},
        //        //   { "제조사", "모델명", "용량", "RPM"},
        //        //   { "제조사", "모델명", "용량", "메모리타입"},
        //        //   { "제조사", "모델명", "사이즈", "해상도"},
        //        //   { "부품정보1", "부품정보2", "부품정보3", "부품정보4"}
        //        //};

        //        public static string[] _componetCd = new string[] { "CPU", "MBD", "MEM", "VGA", "STG", "MON", "ODD", "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT", "PKG", "AIR", "LIC", "PER", "TBL" };
        //        public static string[] _componetNm = new string[] { "CPU", "메인보드", "메모리", "그래픽카드", "저장장치", "모니터", "ODD", "케이스", "어댑터", "파워", "키보드", "마우스", "팬(쿨러)", "케이블", "배터리", "박스", "에어", "라이센스", "주변기기", "태블릿" };
        //        public static string[] _uncheckcomponetCd = new string[] { "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT", "PKG", "AIR", "LIC", "PER", "TBL" };
        //        public static string[] _checkComponetCd = new string[] { "CPU", "MBD", "MEM", "VGA", "STG", "MON", "ODD" };
        //        public static string[] _LTComponentCd = new string[] { "CPU", "M.B", "RAM", "VGA", "HDD", "SSD", "LED", "LCD", "POWER", "CAM", "BAT", "ADT", "S.W", "노트북", "데스크탑" };
        //        public static string[] _componetCdLT = new string[] { "CPU", "MEM", "MBD", "VGA", "STG", "MON", "ODD", "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT", "PKG", "AIR", "LIC", "PER", "NTB", "DKT", "EAD", "EMS" };



        //        public static Dictionary<string, string> _dicLTComponentCd = new Dictionary<string, string>()
        //        {
        //            {"CPU", "CPU"}, {"RAM", "MEM"}, {"M.B", "MBD"}, {"VGA", "VGA"}, {"HDD", "STG"}
        //            , {"SSD", "STG"}, {"LCD", "MON"}, {"LED", "MON"}, {"POWER", "POW"}, {"CAM", "PER"}
        //            , {"BAT", "BAT"}, {"ADT", "ADP"}, {"S.W", "LIC"}, {"노트북", "NTB"}, {"데스크탑", "DKT"}
        //            , {"ETCADD", "EAD"}, {"ETCMINUS", "EMS"}
        //        };
        //        public static Dictionary<string, string> _dicLTComponentCdRevers = new Dictionary<string, string>()
        //        {
        //            {"CPU", "CPU"}, {"MEM", "RAM"}, {"MBD", "M.B"}, {"VGA", "VGA"}
        //            , {"STG", "SSD"}, {"MON", "LED"}, {"POW", "POWER"}, {"PER", "CAM"}
        //            , {"BAT", "BAT"}, {"ADT", "ADP"}, {"LIC", "S.W"}, {"NTB", "노트북"}, {"DKT", "데스크탑"}
        //            , {"EAD", "ETCADD"}, {"EMS", "ETCMINUS"}
        //        };

        //        public static Dictionary<string, List<string>> _dicCompareNm = new Dictionary<string, List<string>>()
        //        {
        //            {"CPU", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"MBD", new List<string>(new[]{ "SB_NM"}) },
        //            {"MEM", new List<string>(new[]{ "BANDWIDTH", "CAPACITY"}) },
        //            {"STG", new List<string>(new[]{ "MODEL_NM", "STG_TYPE", "CAPACITY"}) },
        //            {"VGA", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"MON", new List<string>(new[]{ "MODEL_NM", "SIZE"}) },
        //            {"ODD", new List<string>(new[]{ "MODEL_NM", "TYPE"}) },
        //            {"CAS", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"ADP", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"POW", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"KEY", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"MOU", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"FAN", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"CAB", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"BAT", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"PKG", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"AIR", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"LIC", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"PER", new List<string>(new[]{ "MODEL_NM"}) },
        //            {"TBL", new List<string>(new[]{ "MODEL_NM"}) }

        //        };

        //#if DEBUG
        //        public static string _DBName = "DB: InventoryTest";
        //#else
        //        public static string _DBName="DB: dangol365";
        //#endif
        public static string _version { get; set; }

        //        public static int _startPort = 5000;

        //        public static int _endPort = 5010;

        //        public static string osSerialNo = "";

        //        public static string osProductNo = "";

        //        public static string _osName { get; set; }

        //        public static int _getComponentInformationYn { get; set; }

        public static string _userName { get; set; }
        public static string _userId { get; set; }
        public static string _userType { get; set; }
        public static string _userTeamCd { get; set; }
        public static string _userPosition { get; set; }
        public static string _userPasswd { get; set; }
        public static string _IPType { get; set; }

        //        public static long _userCompanyId { get; set; }

        public static bool _autoLogin { get; set; }
        //        public static string _location { get; set; }
        //        public static string _locationId { get; set; }

        //        public static string _pallet { get; set; }
        //        public static string _palletId { get; set; }

        //        public static string _printerPort { get; set; }

        //        public static List<string> _listNTBCatetory = new List<string>(new[] { "SAMSUNG", "LG" });

        //        public static List<string> _listCpuLaptopCheck = new List<string>(new[] { "FCBGA", "BGA", "RPGA", "478" });

        //        public static List<string> _listCpuCodeNm = new List<string>(new[] { "APOLLO LAKE", "ARRANDALE", "BAY TRAIL", "BRASWELL", "BRISTOL RIDGE", "BROADWELL", "CARRIZO", "CLARKDALE", "CLARKSFIELD", "COFFEE LAKE", "COMET LAKE", "CONROE", "CONROE", "DENEB", "DIAMONDVILLE", "HASWELL", "IVY BRIDGE", "KABINI", "KABY LAKE", "KAVERI", "KENTSFIELD", "LLANO", "LYNNFIELD", "MATISSE", "MEROM", "MULLINS", "PENRYN", "PICASSO", "PINNACLE RIDGE", "PROPUS", "RAVEN RIDGE", "REGOR", "RICHLAND", "SANDY BRIDGE", "SKYLAKE", "SUMMIT RIDGE", "THUBAN", "TRINIDAD", "TRINITY", "VISHERA", "WHISKEY LAKE", "WINDSOR", "WOLFDALE", "YORKFIELD", "ZACATE", "ZAMBEZI", "CLARKSFIELD", "ROCKET LAKE" });

        //        public static string[] _arrTypeNm = { "Unknown", "PC", "노트북", "일체형PC", "기타", "태블릿" };

        //        public static Dictionary<string, int> _dicTypeNm = new Dictionary<string, int>()
        //        {
        //            {"Unknown", 0}, {"DESKTOP", 1}, {"LAPTOP", 2}, {"NOTEBOOK", 2}, {"PORTABLE", 2},{"ALLINONE", 3}, {"TABLET", 5}
        //        };
        //        public static int _type { get; set; }
        //        public static string _typeNm { get; set; }

        //        public static string _cpuCategory { get; set; }
        //        public static string _cpuGeneration { get; set; }
        //        public static string _codeNm { get; set; }

        //        public static string _ntbCategory { get; set; }

        //        public static int _wifiYn { get; set; }
        //        public static string _camModelNm { get; set; }
        //        public static double _batteryRemain { get; set; }

        //        public static string _oddType { get; set; }
        //        public static long _ntbListId { get; set; }
        //        public static int _ntbManufactureType { get; set; }

        //        public static bool _isExistNtbCheckWarehousing { get; set; }
        //        public static bool _isExistNtbCheckRelease { get; set; }
        //        public static bool _isExistNtbCheckRepair { get; set; }

        //        public static bool _isExistNtbCheckQC { get; set; }

        //        public static bool _isExistAllInOneCheckWarehousing { get; set; }
        //        public static bool _isExistAllInOneCheckRelease { get; set; }
        //        public static bool _isExistAllInOneCheckRepair { get; set; }
        //        public static bool _isExistAllInOneCheckQC { get; set; }

        //        public static DataTable _dtLocation = new DataTable();

        //        public static DataTable _dtPallet = new DataTable();

        //        public static DataTable _dtUserId = new DataTable();

        //        //public static List<DeviceInfo> _listDeviceInfo;

        //        public static Dictionary<string, int> _dicPartCnt;

        //        public static Dictionary<string, DataTable> _dicDeviceInfo;

        //        public static Dictionary<string, DataTable> _dicDeviceInfoFromDB;

        //        public static Dictionary<string, Dictionary<string, string>> _dicTableColumnInfo;

        //        public static Dictionary<string, string[]> _dicColumnNm = new Dictionary<string, string[]>();

        //        public static DataTable _dtDeviceInfo = new DataTable();

        //        public static DataTable _dtConsignedInfo = new DataTable();

        //        public static Dictionary<long, DataTable> _dicDeviceInfoDetail;

        //        public static Dictionary<long, DataTable> _dicConsignedInfoDetail = null;

        //        public static Dictionary<long, Dictionary<string, string>> _dicProductList = null;

        //        public static Dictionary<string, List<long>> _dicProductInventoryId = null;

        //        public static Dictionary<string, long> _dicNTBAdjustmentPrice = new Dictionary<string, long>();

        //        public static DataTable _dtNTBAdjustmentPrice = new DataTable();

        //        public static DataTable _dtAllInOneAdjustmentPrice = new DataTable();

        //        public static DataTable _dtTabletAdjustmentPrice = new DataTable();

        //        public static Dictionary<string, long> _dicAllInOneAdjustmentPrice = new Dictionary<string, long>();

        //        //public static Dictionary<string, long> _dicWarehousingNTBAdjustmentPrice = new Dictionary<string, long>();
        //        public static BindingSource _bsWarehousingNTBAdjustmentPrice = new BindingSource();

        //        public static List<long> _listInventoryId = new List<long>();



        //        //입고

        //        public static Dictionary<string, short> _dicNtbCheckWarehousing = null;

        //        public static Dictionary<string, short> _dicAllInOneCheckWarehousing = null;

        //        public static Dictionary<long, string> _dicInventoryDesWarehousing = null;

        //        public static Dictionary<long, string> _dicInventoryGradeWarehousing = null;

        //        public static Dictionary<long, string> _dicMonSizeWarehousing = null;

        //        public static Dictionary<long, Dictionary<string, int>> _dicPartCheckWarehousing = null;

        //        public static string _caseDestroyDescriptionWarehousing = "";

        //        public static string _batteryRemainWarehousing = "";

        //        public static string _productGradeWarehousing = "0";

        //        public static string _etcWarehousing = "0";

        //        public static string _allInOneProductGradeWarehousing = "0";



        //        //출고

        //        public static Dictionary<string, short> _dicNtbCheckRelease = null;

        //        public static Dictionary<string, short> _dicAllInOneCheckRelease = null;

        //        public static Dictionary<long, string> _dicInventoryDesRelease = null;

        //        public static Dictionary<long, string> _dicInventoryGradeRelease = null;

        //        public static Dictionary<long, string> _dicMonSizeRelease = null;

        //        public static Dictionary<long, Dictionary<string, int>> _dicPartCheckRelease = null;

        //        public static string _caseDestroyDescriptionRelease = "";

        //        public static string _batteryRemainRelease = "";

        //        public static string _productGradeRelease = "0";

        //        public static string _etcRelease = "0";

        //        public static string _allInOneProductGradeRelease = "0";

        //        public static Dictionary<long, string> _dicReleaseResult = null;

        //        public static List<long> _listReleaseList = null;



        //        //리페어

        //        public static Dictionary<string, short> _dicNtbCheckRepair = null;

        //        public static Dictionary<string, short> _dicAllInOneCheckRepair = null;

        //        public static Dictionary<long, string> _dicInventoryDesRepair = null;

        //        public static Dictionary<long, string> _dicInventoryGradeRepair = null;

        //        public static Dictionary<long, string> _dicMonSizeRepair = null;

        //        public static Dictionary<long, Dictionary<string, int>> _dicPartCheckRepair = null;

        //        public static string _caseDestroyDescriptionRepair = "";

        //        public static string _batteryRemainRepair = "";

        //        public static string _productGradeRepair = "0";

        //        public static string _etcRepair = "0";

        //        public static string _allInOneProductGradeRepair = "0";

        //        //QC

        //        public static Dictionary<string, short> _dicNtbCheckQC = null;

        //        public static Dictionary<string, short> _dicAllInOneCheckQC = null;

        //        public static Dictionary<long, string> _dicInventoryDesQC = null;

        //        public static Dictionary<long, string> _dicInventoryGradeQC = null;

        //        public static Dictionary<long, string> _dicMonSizeQC = null;

        //        public static Dictionary<long, Dictionary<string, int>> _dicPartCheckQC = null;

        //        public static string _caseDestroyDescriptionQC = "";

        //        public static string _batteryRemainQC = "";

        //        public static string _productGradeQC = "0";

        //        public static string _etcQC = "0";

        //        public static string _allInOneProductGradeQC = "0";

        //        public static Dictionary<long, string> _dicQCResult = null;

        //        public static List<long> _listQCList = null;

        //        //공통
        //        public static DataTable _dtComponent;

        //        public static Dictionary<string, short> _dicTabletCheck = new Dictionary<string, short>();

        //        public static void setDatatable()
        //        {
        //            _dtComponent = new DataTable();

        //            _dtComponent.Columns.Add(new DataColumn("KEY", typeof(string)));
        //            _dtComponent.Columns.Add(new DataColumn("VALUE", typeof(string)));

        //            for (int i = 0; i < _componetCd.Length; i++)
        //            {
        //                DataRow dr = _dtComponent.NewRow();
        //                dr["KEY"] = _componetCd[i];
        //                dr["VALUE"] = _componetNm[i];
        //                _dtComponent.Rows.Add(dr);
        //            }

        //            _dtNTBAdjustmentPrice.Columns.Add(new DataColumn("TYPE", typeof(int)));
        //            _dtNTBAdjustmentPrice.Columns.Add(new DataColumn("EXIST", typeof(bool)));

        //            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
        //                _dtNTBAdjustmentPrice.Columns.Add(new DataColumn(col, typeof(long)));

        //            for (int i = 0; i < 6; i++)
        //            {
        //                DataRow dr = _dtNTBAdjustmentPrice.NewRow();

        //                dr["TYPE"] = i;
        //                dr["EXIST"] = false;
        //                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
        //                    dr[col] = 0;

        //                _dtNTBAdjustmentPrice.Rows.Add(dr);
        //            }

        //            _bsWarehousingNTBAdjustmentPrice.DataSource = _dtNTBAdjustmentPrice;

        //            _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn("TYPE", typeof(int)));
        //            _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn("EXIST", typeof(bool)));

        //            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
        //                _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn(col, typeof(long)));

        //            for (int i = 0; i < 6; i++)
        //            {
        //                DataRow dr = _dtAllInOneAdjustmentPrice.NewRow();

        //                dr["TYPE"] = i;
        //                dr["EXIST"] = false;
        //                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
        //                    dr[col] = 0;

        //                _dtAllInOneAdjustmentPrice.Rows.Add(dr);
        //            }


        //            //_bsWarehousingNTBAdjustmentPrice.DataSource = _dtNTBAdjustmentPrice;

        //}


    }
}
