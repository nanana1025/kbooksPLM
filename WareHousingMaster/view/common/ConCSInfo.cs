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
    static class ConCSInfo
    {
        public static string[] _LTComponentCd = new string[] { "CPU", "M.B", "RAM", "VGA", "HDD", "SSD", "LED", "LCD", "POWER", "CAM", "BAT", "ADT", "S.W", "노트북", "데스크탑" };
        public static string[] _WMComponentCd = new string[] { "CPU", "BOARD", "RAM", "VGA", "HDD/SSD", "LCD/LED", "애플전기종"};
        public static string[] _componentCd = new string[] { "CPU", "BOARD", "RAM", "VGA", "HDD/SSD", "LCD/LED", "애플전기종" };

        public static Dictionary<string, string> _dicLTWMComponentCd = new Dictionary<string, string>()
        {
            {"CPU", "CPU"}, {"RAM", "RAM"}, {"M.B", "BOARD"}, {"VGA", "VGA"}, {"HDD", "HDD/SSD"}
            , {"SSD", "HDD/SSD"}, {"LCD", "LCD/LED"}, {"LED", "LCD/LED"}, {"POWER", "POW"}, {"CAM", "PER"}
            , {"BAT", "BAT"}, {"ADT", "ADP"}, {"S.W", "LIC"}, {"노트북", "NTB"}, {"데스크탑", "DKT"}
        };

        public static Dictionary<string, string> _dicWMLTComponentCd = new Dictionary<string, string>()
        {
            {"CPU", "CPU"}, {"RAM", "RAM"}, {"BOARD", "M.B"}, {"VGA", "VGA"}, {"HDD/SSD", "SSD"},
            {"LCD/LED", "LED"}, {"애플전기종", "노트북"}
        };
    }
}
