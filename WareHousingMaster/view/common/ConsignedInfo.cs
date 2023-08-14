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
    static class ConsignedInfo
    {
        /*
         * 1번은 1S추가한 SERIAL_NO(보통 노트북인듯)
         * 2번은 SYSTEM_NO
         * 3번은 PRODUCT_NAME+SYSTEM_NO
         * 
         * 
         */
        //static public Dictionary<string, int> _dicModelSerialType = new Dictionary<string, int>()
        //{
        //    {"600G1", 2}, {"600G2", 2}, {"600G3", 2}, {"6300", 2}, {"E5550", 2}
        //    , {"L450", 1}, {"L540", 1}, {"L560", 1}, {"M700Z", 2}, {"X240", 1}
        //    , {"X260", 1}, {"3020", 2}, {"DB400", 2}, {"E7240", 2}, {"E5540", 2}
        //    , {"3050", 2}, {"3240", 2}, {"1724", 2}, {"1796", 2}, {"M73Z", 2}
        //    , {"M900Z", 3}, {"9020", 2}, {"9010", 2}, {"7450", 2}, {"600G4S", 2}, {"600G4H", 2}
        //};

        public static string[] _componetCd = new string[] { "CPU", "MBD", "MEM", "VGA", "STG", "MON", "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT", "PKG", "AIR", "LIC", "PER", "NTB", "DKT", "EAD", "EMS"};
        public static string[] _componetNm = new string[] { "CPU", "메인보드", "메모리", "그래픽카드", "저장장치", "모니터", "케이스", "어댑터", "파워", "키보드", "마우스", "팬(쿨러)", "케이블", "배터리", "박스", "에어", "라이센스", "주변기기", "NTB", "DKT", "ETCADD", "ETCMINUS" };

        public static DataTable _dtComponent;

        public static List<string> _listExchangeComponetCd = new List<string>(new[]{ "CPU", "MBD", "MEM", "VGA", "STG", "SSD", "HDD", "MON", "CAS", "POW", "LIC" });
        public static List<string> _listRefundComponetCd = new List<string>(new[]{ "CPU", "MBD", "MEM", "VGA", "STG", "SSD", "HDD", "MON", "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT", "PER", "LIC" });
        public static List<string> _listExceipComponetCd = new List<string>(new[] { "ADP", "KEY", "MOU", "CAB", "BAT", "PER"});

        public static List<string> _listLeadersTechComponetCd = new List<string>(new[] { "LIC", "PKG", "AIR"});

        public static List<string> _listCheckExceiptCol = new List<string>(new[] { "CREATE_USER_ID", "UPDATE_USER_ID", "CREATE_DT","UPDATE_DT","START_DT", "END_DT", "CHECK_ID", "PROXY_ID", "CHECK_TYPE"});
        public static void setDatatable()
        {
            _dtComponent = new DataTable();

            _dtComponent.Columns.Add(new DataColumn("KEY", typeof(string)));
            _dtComponent.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < _componetCd.Length; i++)
            {
                DataRow dr = _dtComponent.NewRow();
                dr["KEY"] = _componetCd[i];
                dr["VALUE"] = _componetCd[i];
                _dtComponent.Rows.Add(dr);
            }

        }
    }
}
