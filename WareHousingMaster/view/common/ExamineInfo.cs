using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousingMaster.view.usedPurchase;

namespace WareHousingMaster.view.common
{
    static class ExamineInfo
    {
        public static string[] _NTBCOLNAME2ND = new string[]{ //            "CASE_HINGE" COOLER 별도 처리
            "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED",
            "DISPLAY", "USB", "MOUSEPAD", "KEYBOARD", "BATTERY",
            "CAM", "ODD", "HDD", "LAN_WIRELESS", "LAN_WIRED", "BIOS", "OS", "TEST_CHECK", "SPEAKER","OVERHEAT","SHUTDOWN" };

        public static string[] _TABLETCOLNAME = new string[]{ //            "CASE_HINGE" COOLER 별도 처리
            "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED",
            "DISPLAY", "BATTERY", "ADAPTER", "BUTTON", "USB_PORT", "USB_CABLE", "PEN", "SD_CARD", "SOFTWARE",
            "CAM", "SOUND", "MIKE", "EAR_PHONE", "LAN_WIRELESS", "TEST_CHECK", "SELF_CHECK" };

        public static string[] _NTBCOLNAMEFULL = new string[]{ //            "CASE_HINGE" COOLER 별도 처리
            "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED","CASE_HINGE", "COOLER",
            "DISPLAY", "USB", "MOUSEPAD", "KEYBOARD", "BATTERY","CAM", "ODD", "HDD", "LAN_WIRELESS", "LAN_WIRED", "BIOS", "OS", "TEST_CHECK" };

        public static string[] _SYMBOL = new string[] { "□", "■", "∨" };

        //public static string[] CASE = new string[]{ "파손", "스크래치", "찍힘", "눌림", "변색" };

        public static Dictionary<string, string> _CASE = new Dictionary<string, string>()
        {
            {"CASE_DESTROYED", "파손"}, {"CASE_SCRATCH", "스크래치"},
            {"CASE_STABBED", "찍힘"}, {"CASE_PRESSED", "눌림"}, {"CASE_DISCOLORED", "변색"}
        };
        public static int[] _BASE = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 131072, 262144, 524288, 1048576 };

        public static List<string> _listSkipCheckCol = new List<string>(new[] { "INVENTORY_ID" });

        public static List<string> _listCaseCheckCol = new List<string>(new[] { "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED" });

        public static List<string> _listTabletCaseCheckCol = new List<string>(new[] { "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_DISCOLORED" });

        public static List<string> _listTabletCol = new List<string>(new[] { "TEST_CHECK", "CASES","DISPLAY", "BATTERY", "ADAPTER", "BUTTON", "USB_PORT",
            "USB_CABLE","PEN", "SD_CARD", "SOFTWARE", "CAM", "SOUND", "EAR_PHONE", "MIKE", "LAN_WIRELESS", "SELF_CHECK"});


        public static List<string> _listAdjustmentPriceCol = new List<string>(new[] { "ADJUST_PRICE_ID", "CASES","DISPLAY", "COOLER","USB", "MOUSEPAD", "KEYBOARD", "BATTERY",
            "CAM", "ODD", "HDD", "LAN_WIRELESS", "LAN_WIRED", "BIOS", "ADAPTER","OS", "TEST_CHECK", "ETC"});

        public static List<string> _listAdjustmentAllInOnePriceCol = new List<string>(new[] { "ADJUST_PRICE_ID", "CASES","DISPLAY","USB", "MOUSEPAD", "KEYBOARD",
            "CAM", "ODD", "HDD", "LAN_WIRELESS", "LAN_WIRED", "BIOS", "OS", "TEST_CHECK", "ETC", "PORT", "ADAPTER"});

        public static List<string> _listAdjustmentTabletPriceCol = new List<string>(new[] { "ADJUST_PRICE_ID","TEST_CHECK", "CASES","DISPLAY", "BATTERY", "ADAPTER", "BUTTON", "USB_PORT",
            "USB_CABLE","PEN", "SD_CARD", "SOFTWARE", "CAM", "SOUND", "EAR_PHONE", "MIKE", "LAN_WIRELESS","SELF_CHECK","ETC"});



        public static List<string> _listAdjustmentPriceColShort = new List<string>(new[] { "CASES","DISPLAY", "USB", "MOUSEPAD", "KEYBOARD", "BATTERY",
            "CAM", "ODD", "HDD", "LAN_WIRELESS", "LAN_WIRED", "BIOS", "OS", "TEST_CHECK", "ETC"}); // 순서 맞춰야 함. dlgntb2ndEdititoncheck.cs의 검수단계랑 순서 맞춰야 함.

        public static List<string> _listAdjustmentAllInOnePriceColShort = new List<string>(new[] { "CASES","DISPLAY", "USB", "MOUSEPAD", "KEYBOARD",
            "CAM", "ODD", "HDD", "LAN_WIRELESS", "LAN_WIRED", "BIOS", "OS", "TEST_CHECK", "PORT", "ADAPTER", "ETC"}); // 순서 맞춰야 함. dlgntb2ndEdititoncheck.cs의 검수단계랑 순서 맞춰야 함.

        public static List<string> _listAdjustmentTabletPriceColShort = new List<string>(new[] { "TEST_CHECK", "CASES","DISPLAY", "BATTERY", "ADAPTER", "BUTTON", "USB_PORT",
            "USB_CABLE","PEN", "SD_CARD", "SOFTWARE", "CAM", "SOUND", "EAR_PHONE", "MIKE", "LAN_WIRELESS","SELF_CHECK","ETC"});


        public static Dictionary<string, List<string>> _NTBCHECK2ND = new Dictionary<string, List<string>>()
        {
            {"CASE", new List<string>(new[]{"1", "2", "3", "4"}) },
            {"DISPLAY", new List<string>(new[]{"인식안됨", "파손", "스크래치","흰멍","빛샘","화면줄","액정속 이물질","터치스크린 인식","키보드자국"}) },
            {"USB", new List<string>(new[]{"인식 안됨","파손"}) },
            {"MOUSEPAD", new List<string>(new[]{"인식 안됨", "파손","좌우버튼 인식 안됨"}) },
            {"KEYBOARD", new List<string>(new[]{"인식 안됨", "자판없음","자판 빠짐 1개","자판 빠짐 2개","자판 빠짐 3개이상"}) },
            {"BATTERY", new List<string>(new[]{"배터리없음","충전 안됨", "노화","파손","정상"}) },
            {"CAM", new List<string>(new[]{"인식 안됨","파손","없음"}) },
            {"ODD", new List<string>(new[]{"인식 안됨", "ODD 없음", "ODD 파손","베젤 없음","베젤 파손","멀티부스트"}) },
            {"HDD", new List<string>(new[]{"인식 안됨", "가이드 없음", "가이드 파손","젠더 없음","젠더 파손"}) },
            {"LAN_WIRELESS", new List<string>(new[]{"인식 안됨", "없음"}) },
            {"LAN_WIRED", new List<string>(new[]{"인식 안됨", "파손"}) },
            {"BIOS", new List<string>(new[]{"CMOS P/W", "CMOS 접근 안됨"}) },
            {"OS", new List<string>(new[]{"윈도우 진입 불가"}) },
            {"TEST_CHECK", new List<string>(new[]{"전원 안 켜짐", "사용중 멈춤","화면 안나옴","액정파손"}) },
            {"SPEAKER", new List<string>(new[]{"SPEAKER"}) },
            {"OVERHEAT", new List<string>(new[]{"OVERHEAT"}) },
            {"SHUTDOWN", new List<string>(new[]{"SHUTDOWN"}) }
        };

        public static Dictionary<string, List<string>> _TABLETCHECK = new Dictionary<string, List<string>>()
        {
            {"CASE", new List<string>(new[]{"1", "2"}) },
            {"DISPLAY", new List<string>(new[]{"스크래치", "흰멍", "빛샘","화면줄","액정속 이물질","백라이트불량","파손", "기포"}) },
            {"BATTERY", new List<string>(new[]{"충전 안됨", "부품"}) },
            {"ADAPTER", new List<string>(new[]{"인식안됨", "없음"}) },
            {"BUTTON", new List<string>(new[]{"전원버튼 불량", "볼륨버튼 불량", "홈버튼 불량"}) },
            {"USB_PORT", new List<string>(new[]{"인식안됨","파손"}) },
            {"USB_CABLE", new List<string>(new[]{"인식안됨","없음"}) },
            {"PEN", new List<string>(new[]{"버튼불량", "파손","없음"}) },
            {"SD_CARD", new List<string>(new[]{"파손","없음"}) },
            {"SOFTWARE", new List<string>(new[]{"계정초기화 안됨", "DISPLAY설정안됨","사운드 및 진동 설정 안됨"}) },
            {"CAM", new List<string>(new[]{"인식안됨[전면]","인식안됨[후면]","파손[전면]","파손[후면]"}) },
            {"SOUND", new List<string>(new[]{"출력안됨", "출력이상"}) },
            {"MIKE", new List<string>(new[]{"인식 안됨"}) },
            {"EAR_PHONE", new List<string>(new[]{"출력안됨", "파손", "없음"}) },
            {"LAN_WIRELESS", new List<string>(new[]{"인식 안됨"}) },
            {"TEST_CHECK", new List<string>(new[]{"전원 안 켜짐", "사용중 멈춤","화면 안나옴","액정파손"}) },
            {"SELF_CHECK", new List<string>(new[]{"진동불량", "터치불량","B펜 터치불량"}) }



            //{"CASE", new List<string>(new[]{"1", "2"}) },
            //{"DISPLAY", new List<string>(new[]{"스크래치", "흰멍", "빛샘","화면줄","액정속 이물질","터치스크린 인식","백화", "미세먼지"}) },
            //{"BATTERY", new List<string>(new[]{"충전 안됨", "노화"}) },
            //{"ADAPTER", new List<string>(new[]{"인식안됨", "없음"}) },
            //{"BUTTON", new List<string>(new[]{"전원버튼 불량", "볼륨버튼 불량", "홈버튼 불량"}) },
            //{"USB_PORT", new List<string>(new[]{"인식안됨","파손","없음"}) },
            //{"USB_CABLE", new List<string>(new[]{"인식안됨","없음"}) },
            //{"PEN", new List<string>(new[]{"해당없음", "파손","없음"}) },
            //{"SD_CARD", new List<string>(new[]{"인식안됨", "파손","없음"}) },
            //{"SOFTWARE", new List<string>(new[]{"계정초기화 안됨", "DISPLAY설정안됨","사운드 및 진동 설정 안됨"}) },
            //{"CAM", new List<string>(new[]{"인식안됨[전면]","인식안됨[후면]","파손[전면]","파손[후면]"}) },
            //{"SOUND", new List<string>(new[]{"출력안됨", "파손"}) },
            //{"MIKE", new List<string>(new[]{"인식 안됨"}) },
            //{"EAR_PHONE", new List<string>(new[]{"출력안됨", "파손", "없음"}) },
            //{"LAN_WIRELESS", new List<string>(new[]{"인식 안됨"}) },
            //{"TEST_CHECK", new List<string>(new[]{"전원 안 켜짐", "사용중 멈춤","화면 안나옴","액정파손"}) },
            //{"SELF_CHECK", new List<string>(new[]{"RED", "GREEN","BLUE","DIMMING","MEGA CAM","SENSOR","TOUCH","SLEEP","SPEAKER(L)","SPEAKER(R)","SUB KEY","FRONT CAM","HALL IC","GRIP SENSOR","BLACK"}) }

        };


        public static Dictionary<string, string> _dicListAdjustmentPriceCol = new Dictionary<string, string>()
        {
            {"CASES", "CASE_CNT" },
            {"DISPLAY","DISPLAY_CNT" },
            {"BIOS","PASSWORD_CNT" },
            {"USB","USB_CNT" },
            {"KEYBOARD","KEYBOARD_CNT" },
            {"BATTERY","BATTERY_CNT" },
            {"LAN_WIRELESS","WIRELESS_CNT" },
            {"ETC","ETC_CNT"}
        };

        public static Dictionary<string, string> _dicListAdjustmentPriceColPair = new Dictionary<string, string>()
        {
            {"CASES", "CASE_REPAIR_CNT" },
            {"DISPLAY","DISPLAY_REPAIR_CNT" },
            {"BIOS","PASSWORD_REPAIR_CNT" },
            {"USB","USB_REPAIR_CNT" },
            {"KEYBOARD","KEYBOARD_REPAIR_CNT" },
            {"BATTERY","BATTERY_REPAIR_CNT" },
            {"LAN_WIRELESS","WIRELESS_REPAIR_CNT" },
            {"ETC","ETC_REPAIR_CNT"}
        };

        public static Dictionary<string, string> _dicListAdjustmentPriceColPairQC = new Dictionary<string, string>()
        {
            {"CASES", "CASE_QC_CNT" },
            {"DISPLAY","DISPLAY_QC_CNT" },
            {"BIOS","PASSWORD_QC_CNT" },
            {"USB","USB_QC_CNT" },
            {"KEYBOARD","KEYBOARD_QC_CNT" },
            {"BATTERY","BATTERY_QC_CNT" },
            {"LAN_WIRELESS","WIRELESS_QC_CNT" },
            {"ETC","ETC_QC_CNT"}
        };

        public static Dictionary<string, string> _dicListProducePriceColPair = new Dictionary<string, string>()
        {
            {"CASES", "CASE_CNT" },
            {"DISPLAY","DISPLAY_CNT" },
            {"BIOS","PASSWORD_CNT" },
            {"USB","USB_CNT" },
            {"KEYBOARD","KEYBOARD_CNT" },
            {"BATTERY","BATTERY_CNT" },
            {"LAN_WIRELESS","WIRELESS_CNT" },
            {"ETC","ETC_CNT"},
            {"MOUSEPAD","MOUSEPAD_CNT"},
            {"CAM","CAM_CNT"},
            {"ODD","ODD_CNT"},
            {"HDD","HDD_CNT"},
            {"LAN_WIRED","LAN_WIRED_CNT"},
            {"OS","OS_CNT"},
            {"TEST_CHECK","TEST_CHECK_CNT"}
        };

        public static Dictionary<string, string> _dicListProducePriceColPairQC = new Dictionary<string, string>()
        {
            {"CASES", "CASE_QC_CNT" },
            {"DISPLAY","DISPLAY_QC_CNT" },
            {"BIOS","PASSWORD_QC_CNT" },
            {"USB","USB_QC_CNT" },
            {"KEYBOARD","KEYBOARD_QC_CNT" },
            {"BATTERY","BATTERY_QC_CNT" },
            {"LAN_WIRELESS","WIRELESS_QC_CNT" },
            {"ETC","ETC_QC_CNT"},
            {"MOUSEPAD","MOUSEPAD_QC_CNT"},
            {"CAM","CAM_QC_CNT"},
            {"ODD","ODD_QC_CNT"},
            {"HDD","HDD_QC_CNT"},
            {"LAN_WIRED","LAN_WIRED_QC_CNT"},
            {"OS","OS_QC_CNT"},
            {"TEST_CHECK","TEST_CHECK_QC_CNT"}
        };

        public static Dictionary<string, string> _dicListAdjustmentPriceColPairReverse = new Dictionary<string, string>()
        {
            {"CASE_CNT", "CASES" },
            {"DISPLAY_CNT","DISPLAY" },
            {"PASSWORD_CNT","BIOS" },
            {"USB_CNT","USB" },
            {"KEYBOARD_CNT","KEYBOARD" },
            {"BATTERY_CNT","BATTERY" },
            {"WIRELESS_CNT","LAN_WIRELESS" }
        };

        public static List<string> _dicListProduceReleaseCountColPair = new List<string>(new string[]{
           "CASE_CNT",
            "DISPLAY_CNT",
            "USB_CNT",
            "MOUSEPAD_CNT",
            "KEYBOARD_CNT",
            "BATTERY_CNT",
            "CAM_CNT",
            "ODD_CNT",
            "HDD_CNT",
            "LAN_WIRELESS_CNT",
            "LAN_WIRED_CNT",
            "BIOS_CNT",
            "OS_CNT",
            "TEST_CHECK_CNT",
            "SPEAKER_CNT",
            "OVERHEAT_CNT",
            "SHUTDOWN_CNT"
            }
        );

    }
}
