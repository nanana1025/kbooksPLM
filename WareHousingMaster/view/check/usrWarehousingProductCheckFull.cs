using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WareHousingMaster.view.common;
using DevExpress.XtraSplashScreen;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.inventory;
using DevExpress.XtraGrid.Views.Grid;
using WareHousingMaster.view.warehousing.createComponent;
using WareHousingMaster.view.warehousing.selectComponent;
using System.Collections;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace WareHousingMaster.view.check
{
    public partial class usrWarehousingProductCheckFull : DevExpress.XtraEditors.XtraForm
    {

        string _representativeType = "W";
        string _representativeCol = "WAREHOUSING";
        string _representativeIdCol = "WAREHOUSING_ID";
        string _representativeNo = null;
        
        long _representativeId = -1;
        long _inventoryId = -1;
        object _companyId = -1;
        long _type = 1;
        string _componentCd = "ALL";

        List<long> _listRepresentativeId;

        DataRowView _currentWarehousing;
        DataRowView _currentProduct;

        DataTable _dtWarehousing;
        DataTable _dt;
        DataTable _dtRepairCheck;

        DataTable _dtExamine;
        DataTable _dtRepair;

        DataTable _dtExaminePrice;
        DataTable _dtRepairPrice;

        DataTable _dtPallet;

        BindingSource _bs;
        BindingSource _bsWarehousing;

        short _checkType = 1;
        string _barcode = null;
        int _productType = 2;

        DataTable _dtPrintPort;
        DataTable _dtPGrade;


        DataTable _dtNTBAdjustmentPrice = null;
        DataTable _dtAllInOneAdjustmentPrice = null;

        Dictionary<string, short> _dicProductCheck = null;
        Dictionary<string, short> _dicProductCheckHistory = null;

        Dictionary<string, long> _dicAdjustmentPrice = null;
        Dictionary<string, long> _dicAdjustmentPriceHistory = null;

        string _etcDes = "";
        string _batteryRemain = "";
        string _productGrade = "";

        string _etcDesHistory = "";
        string _batteryRemainHistory = "";
        string _productGradeHistory = "";

        bool _isCheckExist = false;
        bool _isCheckExistHistory = false;

        bool _isAdjustmentExist = false;
        bool _isAdjustmentExistHistory = false;


        public usrWarehousingProductCheckFull()
        {
            InitializeComponent();

            _dtWarehousing = new DataTable();
            _dtWarehousing.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_STATE", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("EXAM_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("COMPLETE_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("REMAIN_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("REPAIR_CNT", typeof(int)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("ADJUSTMENT_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(int)));
            
            //_dt.Columns.Add(new DataColumn("APPROVAL_TYPE", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_USER1", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_DT", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_DT1", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAMINE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("REPAIR_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAMINE_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("REPAIR_USER_ID", typeof(string)));
            //_dt.Columns.Add(new DataColumn("QC_YN", typeof(string)));
            //_dt.Columns.Add(new DataColumn("LOCATION", typeof(long)));
            //_dt.Columns.Add(new DataColumn("PALLET", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("PRODUCT_ADJUST_PRICE", typeof(long)));

            _dt.Columns.Add(new DataColumn("PASSWORD_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("DISPLAY_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("KEYBOARD_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("USB_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("CASE_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("WIRELESS_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("BATTERY_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("ETC_CNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("PASSWORD_REPAIR_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("DISPLAY_REPAIR_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("KEYBOARD_REPAIR_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("USB_REPAIR_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("CASE_REPAIR_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("WIRELESS_REPAIR_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("BATTERY_REPAIR_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("ETC_REPAIR_CNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_SN", typeof(string)));
            _dt.Columns.Add(new DataColumn("SYSTEM_SN", typeof(string)));
            _dt.Columns.Add(new DataColumn("CPU_MODEL_NM", typeof(string)));
            //_dt.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            _dt.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("MON_SIZE", typeof(string)));
            //_dt.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dt.Columns.Add(new DataColumn("CHECK_TYPE", typeof(int))); //1:EXAMINE, 2:REPAIR
            _dt.Columns.Add(new DataColumn("PRODUCT_GRADE_EXAM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_GRADE_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_STABBED", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_PRESSED", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_HINGE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dt.Columns.Add(new DataColumn("COOLER", typeof(string)));
            _dt.Columns.Add(new DataColumn("DISPLAY", typeof(string)));
            _dt.Columns.Add(new DataColumn("USB", typeof(string)));
            _dt.Columns.Add(new DataColumn("MOUSEPAD", typeof(string)));
            _dt.Columns.Add(new DataColumn("KEYBOARD", typeof(string)));
            _dt.Columns.Add(new DataColumn("BATTERY", typeof(string)));
            _dt.Columns.Add(new DataColumn("CAM", typeof(string)));
            _dt.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(string)));
            _dt.Columns.Add(new DataColumn("LAN_WIRED", typeof(string)));
            _dt.Columns.Add(new DataColumn("ODD", typeof(string)));
            _dt.Columns.Add(new DataColumn("HDD", typeof(string)));
            _dt.Columns.Add(new DataColumn("BIOS", typeof(string)));
            _dt.Columns.Add(new DataColumn("OS", typeof(string)));
            _dt.Columns.Add(new DataColumn("TEST_CHECK", typeof(string)));

            _dt.Columns.Add(new DataColumn("CASE_DESTROYED_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_SCRATCH_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_STABBED_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_PRESSED_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DISCOLORED_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_HINGE_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DES_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("COOLER_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("DISPLAY_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("USB_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("MOUSEPAD_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("KEYBOARD_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("BATTERY_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("CAM_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("LAN_WIRELESS_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("LAN_WIRED_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("ODD_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("HDD_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("BIOS_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("OS_REPAIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("TEST_CHECK_REPAIR", typeof(string)));

            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));

            _dtExamine = new DataTable();
            _dtExamine.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtExamine.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtExamine.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtExamine.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dtExamine.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtExamine.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            _dtExamine.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtExamine.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtExamine.Columns.Add(new DataColumn("UPDATE_USER_ID", typeof(string)));

            _dtRepair = new DataTable();
            _dtRepair.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtRepair.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtRepair.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtRepair.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dtRepair.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtRepair.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            _dtRepair.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtRepair.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtRepair.Columns.Add(new DataColumn("UPDATE_USER_ID", typeof(string)));



            _dtExaminePrice = new DataTable();
            _dtExaminePrice.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtExaminePrice.Columns.Add(new DataColumn("CASES", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("DISPLAY", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("BATTERY", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("MOUSEPAD", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("KEYBOARD", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("CAM", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("USB", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("LAN_WIRED", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("HDD", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("ODD", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("ADAPTER", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("BIOS", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("OS", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("TEST_CHECK", typeof(long)));
            _dtExaminePrice.Columns.Add(new DataColumn("ETC", typeof(long)));

            _dtRepairPrice = new DataTable();
            _dtRepairPrice.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtRepairPrice.Columns.Add(new DataColumn("CASES", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("DISPLAY", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("BATTERY", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("MOUSEPAD", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("KEYBOARD", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("CAM", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("USB", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("LAN_WIRED", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("HDD", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("ODD", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("ADAPTER", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("BIOS", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("OS", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("TEST_CHECK", typeof(long)));
            _dtRepairPrice.Columns.Add(new DataColumn("ETC", typeof(long)));


            _dtRepairCheck = new DataTable(); //[0]그대로:black, [1]없다가 생김:red, [2]있던게 변경:blue
            _dtRepairCheck.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtRepairCheck.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtRepairCheck.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtRepairCheck.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));



            _bsWarehousing = new BindingSource();
            _bs = new BindingSource();

            _dicProductCheck = new Dictionary<string, short>();
            _dicProductCheckHistory = new Dictionary<string, short>();

            _dicAdjustmentPrice = new Dictionary<string, long>();
            _dicAdjustmentPriceHistory = new Dictionary<string, long>();

            _dtNTBAdjustmentPrice = new DataTable();
            _dtAllInOneAdjustmentPrice = new DataTable();

            _listRepresentativeId = new List<long>();
        }

        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            leProductType.DataBindings.Add(new Binding("EditValue", _bs, "PRODUCT_TYPE", false, DataSourceUpdateMode.Never));
            leAdjustmentState.DataBindings.Add(new Binding("EditValue", _bs, "ADJUSTMENT_STATE", false, DataSourceUpdateMode.Never));
            leManufactureType.DataBindings.Add(new Binding("EditValue", _bs, "MANUFACTURE_TYPE", false, DataSourceUpdateMode.Never));
            leNickName.DataBindings.Add(new Binding("EditValue", _bs, "NTB_LIST_ID", false, DataSourceUpdateMode.Never));

            JObject jResult = new JObject();
            getWarehousingList(ref jResult);
        }

        private void setInfoBox()
        {
            _dtPrintPort = new DataTable();

            _dtPrintPort.Columns.Add(new DataColumn("KEY", typeof(string)));
            _dtPrintPort.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = ProjectInfo._startPort; i < ProjectInfo._endPort; i++)
            {
                DataRow dr = _dtPrintPort.NewRow();

                dr["KEY"] = i;
                dr["VALUE"] = i;
                _dtPrintPort.Rows.Add(dr);
            }

            Util.LookupEditHelper(lePrintPort, _dtPrintPort, "KEY", "VALUE");

            DataTable dtCheckTypet = new DataTable();

            dtCheckTypet.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCheckTypet.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtCheckTypet, 3, " 리페어");
            //Util.insertRowonTop(dtCheckTypet, 2, " 생산");
            Util.insertRowonTop(dtCheckTypet, 1, " 입고");

            Util.LookupEditHelper(leCheckType, dtCheckTypet, "KEY", "VALUE");

            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");        

            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            Util.LookupEditHelper(leManufactureType, dtmanufactureType, "KEY", "VALUE");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(rileNickName, dtNickName, "KEY", "VALUE");
            Util.LookupEditHelper(leNickName, dtNickName, "KEY", "VALUE");

            DataTable dtdjustmentState = Util.getCodeList("CD1501", "KEY", "VALUE");
            Util.LookupEditHelper(rileAdjustmentState, dtdjustmentState, "KEY", "VALUE");
            Util.LookupEditHelper(leAdjustmentState, dtdjustmentState, "KEY", "VALUE");

            DataTable dtWarehousingState = Util.getCodeList("CD0601", "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            _dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");

            Util.LookupEditHelper(rileProductGrade, _dtPGrade, "KEY", "VALUE");

            DataTable dtDeviceType = new DataTable();

            dtDeviceType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtDeviceType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._arrTypeNm.Length; i++)
            {
                DataRow dr = dtDeviceType.NewRow();

                dr["KEY"] = i;
                dr["VALUE"] = ProjectInfo._arrTypeNm[i];
                dtDeviceType.Rows.Add(dr);
            }

            Util.LookupEditHelper(leProductType, dtDeviceType, "KEY", "VALUE");

            _dtNTBAdjustmentPrice.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtNTBAdjustmentPrice.Columns.Add(new DataColumn("EXIST", typeof(bool)));

            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                _dtNTBAdjustmentPrice.Columns.Add(new DataColumn(col, typeof(long)));

            for (int i = 1; i < 6; i++)
            {
                DataRow dr = _dtNTBAdjustmentPrice.NewRow();

                dr["TYPE"] = i;
                dr["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    dr[col] = 0;

                _dtNTBAdjustmentPrice.Rows.Add(dr);
            }

            _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn("EXIST", typeof(bool)));

            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn(col, typeof(long)));

            for (int i = 1; i < 6; i++)
            {
                DataRow dr = _dtAllInOneAdjustmentPrice.NewRow();

                dr["TYPE"] = i;
                dr["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                    dr[col] = 0;

                _dtAllInOneAdjustmentPrice.Rows.Add(dr);
            }

            var today = DateTime.Today;
            var pastDate = today.AddDays(-364);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

            leCheckType.EditValue = 1;


        }

        private void setIInitData()
        {
            //// 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;

            _bsWarehousing.DataSource = _dtWarehousing;
            _bs.DataSource = _dt;
        }

       

        private void setGridControl()
        {
            gcWarehousingList.DataSource = null;
            gcWarehousingList.DataSource = _bsWarehousing;

            gcList.DataSource = null;
            gcList.DataSource = _bs;
        }

        private void gvWarehousingList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingList.RowCount > 0);

            //gvList.BeginDataUpdate();

            //_listRepresentativeId.Clear();
            //_dt.Clear();

            if (isValidRow)
            {
                _currentWarehousing = e.Row as DataRowView;
                //_representativeNo = ConvertUtil.ToString(_currentWarehousing["WAREHOUSING"]);


                //int[] selectedRowHandles = gvWarehousingList.GetSelectedRows();
                //ArrayList rows = new ArrayList();

                //for (int i = 0; i < selectedRowHandles.Length; i++)
                //{
                //    int selectedRowHandle = selectedRowHandles[i];
                //    if (selectedRowHandle >= 0)
                //        rows.Add(gvWarehousingList.GetDataRow(selectedRowHandle));
                //}
                //_representativeId = ConvertUtil.ToInt64(_currentWarehousing["WAREHOUSING_ID"]);
                //if (_representativeId > 0)
                //    getProduct(_representativeId);
            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _currentWarehousing = null;
                //_dt.Clear();
            }

            //gvList.EndDataUpdate();
        }

        private void gvWarehousingList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentProduct = e.Row as DataRowView;
                _productType = ConvertUtil.ToInt32(_currentProduct["PRODUCT_TYPE"]);
                _barcode = ConvertUtil.ToString(_currentProduct["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentProduct["INVENTORY_ID"]);
                _representativeNo = ConvertUtil.ToString(_currentProduct["WAREHOUSING"]);
                _representativeId = ConvertUtil.ToInt64(_currentProduct["WAREHOUSING_ID"]);
            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _currentProduct = null;
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentProduct = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
                _productType = ConvertUtil.ToInt32(_currentProduct["PRODUCT_TYPE"]);
                _barcode = ConvertUtil.ToString(_currentProduct["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentProduct["INVENTORY_ID"]);
                _representativeNo = ConvertUtil.ToString(_currentProduct["WAREHOUSING"]);
                _representativeId = ConvertUtil.ToInt64(_currentProduct["WAREHOUSING_ID"]);
            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _currentProduct = null;
            }
        }


        private void sbSearch_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();
            getWarehousingList(ref jResult);
        }

        private bool getWarehousingList(ref JObject jResult)
        {
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtWarehousing.Clear();

            jData.Add("WAREHOUSING_TYPE", 1);
            jData.Add("WAREHOUSING_CATEGORY", 2);

            if (DBAdjustment.getWarehousingList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehousing.NewRow();

                        dr["NO"] = index++;
                        dr["CHECK"] = false;
                        dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dr["WAREHOUSING"] = obj["WAREHOUSING"];
                        dr["WAREHOUSING_DT"] = ConvertUtil.ToDateTimeNull(obj["WAREHOUSING_DT"]);
                        dr["WAREHOUSING_STATE"] = obj["WAREHOUSING_STATE"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["PART_CNT"] = obj["PART_CNT"];
                        dr["EXAM_CNT"] = obj["EXAM_CNT"];
                        dr["COMPLETE_CNT"] = obj["COMPLETE_CNT"];
                        dr["REMAIN_CNT"] = obj["REMAIN_CNT"];
                        dr["REPAIR_CNT"] = obj["REPAIR_CNT"];

                        _dtWarehousing.Rows.Add(dr);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkSearch(ref JObject jData)
        {

            string dtFrom = "";
            string dtTo = "";
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
                dtFrom = $"{deDtFrom.Text} 00:00:00";

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
                dtTo = $"{deDtTo.Text} 23:59:59";

            DateTime dtfrom;
            DateTime dtto;
            dtfrom = Convert.ToDateTime(dtFrom);
            dtto = Convert.ToDateTime(dtTo);

            int result = DateTime.Compare(dtfrom, dtto);

            if (result > 0)
            {
                jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다.");
                return false;
            }

            TimeSpan TS = dtto - dtfrom;
            int diffDay = TS.Days;

            if (diffDay > 730)
            {
                jData.Add("MSG", "검색 기간은 2년(730일)을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("CREATE_DT_S", dtFrom);
            jData.Add("CREATE_DT_E", dtTo);

            //if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
            //    jData.Add("PRODUCT_GRADE", ConvertUtil.ToString(leReceiptState.EditValue));

            return true;
        }

        private void getProduct()
        {
            _dt.Clear();

            if (_listRepresentativeId.Count < 0)
            {
                Dangol.Message("선택하신 입고번호가 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("TN_TABLE_PART", "TN_WAREHOUSING_PART");
            jData.Add("TN_TABLE", "TN_WAREHOUSING");
            jData.Add("ADJUSTMENT_STATE", "0, 1, 2, 3, 4, 5, 6");
            jData.Add("REPRESENTATIVE_ID_COL", "WAREHOUSING_ID");
            jData.Add("REPRESENTATIVE_ID", "WAREHOUSING_ID");
            jData.Add("REPRESENTATIVE_KEY", "WAREHOUSING");
            jData.Add("REPRESENTATIVE_COL", "WAREHOUSING");
            jData.Add("LIST_REPRESENTATIVE_ID", string.Join(",", _listRepresentativeId));

            if (DBAdjustment.getProductListByWarehousingIdFull(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DATA_EXIST"]))
                {
                    setProductCheckData(jResult);
                }
            }
        }
        private void setProductCheckData(JObject jResult)
        {
            if (Convert.ToBoolean(jResult["EXAMDATA_EXIST"]))
            {
                JArray jArray = JArray.Parse(jResult["EXAMDATA"].ToString());
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                    DataRow dr = _dtExamine.NewRow();

                    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    //dr["CHECK_YN"] = checkYn;
                    //if (checkYn)
                    {
                        dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];
                        dr["CASE_DESTROYED"] = obj["CASE_DESTROYED"];
                        dr["CASE_SCRATCH"] = obj["CASE_SCRATCH"];
                        dr["CASE_STABBED"] = obj["CASE_STABBED"];
                        dr["CASE_PRESSED"] = obj["CASE_PRESSED"];
                        dr["CASE_DISCOLORED"] = obj["CASE_DISCOLORED"];
                        dr["CASE_HINGE"] = obj["CASE_HINGE"];
                        dr["CASE_DES"] = obj["CASE_DES"];

                        dr["DISPLAY"] = obj["DISPLAY"];
                        dr["USB"] = obj["USB"];
                        dr["MOUSEPAD"] = obj["MOUSEPAD"];
                        dr["KEYBOARD"] = obj["KEYBOARD"];
                        dr["BATTERY"] = obj["BATTERY"];
                        dr["BATTERY_REMAIN"] = obj["BATTERY_REMAIN"];
                        dr["CAM"] = obj["CAM"];
                        dr["LAN_WIRELESS"] = obj["LAN_WIRELESS"];
                        dr["LAN_WIRED"] = obj["LAN_WIRED"];
                        dr["ODD"] = obj["ODD"];
                        dr["HDD"] = obj["HDD"];
                        dr["BIOS"] = obj["BIOS"];
                        dr["OS"] = obj["OS"];
                        dr["TEST_CHECK"] = obj["TEST_CHECK"];
                        dr["CREATE_DT"] = obj["CREATE_DT"];
                        dr["UPDATE_DT"] = obj["UPDATE_DT"];
                        dr["UPDATE_USER_ID"] = obj["UPDATE_USER_ID"];
                    }
                    _dtExamine.Rows.Add(dr);
                }
            }

            if (Convert.ToBoolean(jResult["REPAIRDATA_EXIST"]))
            {
                JArray jArray = JArray.Parse(jResult["REPAIRDATA"].ToString());
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                    DataRow dr = _dtRepair.NewRow();

                    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    //dr["CHECK_YN"] = checkYn;
                    //if (checkYn)
                    {
                        dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];
                        dr["CASE_DESTROYED"] = obj["CASE_DESTROYED"];
                        dr["CASE_SCRATCH"] = obj["CASE_SCRATCH"];
                        dr["CASE_STABBED"] = obj["CASE_STABBED"];
                        dr["CASE_PRESSED"] = obj["CASE_PRESSED"];
                        dr["CASE_DISCOLORED"] = obj["CASE_DISCOLORED"];
                        dr["CASE_HINGE"] = obj["CASE_HINGE"];
                        dr["CASE_DES"] = obj["CASE_DES"];

                        dr["DISPLAY"] = obj["DISPLAY"];
                        dr["USB"] = obj["USB"];
                        dr["MOUSEPAD"] = obj["MOUSEPAD"];
                        dr["KEYBOARD"] = obj["KEYBOARD"];
                        dr["BATTERY"] = obj["BATTERY"];
                        dr["BATTERY_REMAIN"] = obj["BATTERY_REMAIN"];
                        dr["CAM"] = obj["CAM"];
                        dr["LAN_WIRELESS"] = obj["LAN_WIRELESS"];
                        dr["LAN_WIRED"] = obj["LAN_WIRED"];
                        dr["ODD"] = obj["ODD"];
                        dr["HDD"] = obj["HDD"];
                        dr["BIOS"] = obj["BIOS"];
                        dr["OS"] = obj["OS"];
                        dr["TEST_CHECK"] = obj["TEST_CHECK"];
                        dr["CREATE_DT"] = obj["CREATE_DT"];
                        dr["UPDATE_DT"] = obj["UPDATE_DT"];
                        dr["UPDATE_USER_ID"] = obj["UPDATE_USER_ID"];
                    }
                    _dtRepair.Rows.Add(dr);
                }
            }

            if (Convert.ToBoolean(jResult["EXAMPRICE_EXIST"]))
            {
                JArray jArray = JArray.Parse(jResult["EXAMPRICEDATA"].ToString());
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                    DataRow dr = _dtExaminePrice.NewRow();

                    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    //dr["CHECK_YN"] = checkYn;
                    //if (checkYn)
                    {
                        foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                            dr[col] = obj[col];
                    }
                    _dtExaminePrice.Rows.Add(dr);
                }
            }

            if (Convert.ToBoolean(jResult["REPAIRPRICE_EXIST"]))
            {
                JArray jArray = JArray.Parse(jResult["REPAIRPRICEDATA"].ToString());
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                    DataRow dr = _dtRepairPrice.NewRow();

                    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    //dr["CHECK_YN"] = checkYn;
                    //if (checkYn)
                    {
                        foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                            dr[col] = obj[col];
                    }
                    _dtRepairPrice.Rows.Add(dr);
                }
            }

            if (Convert.ToBoolean(jResult["DATA_EXIST"]))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                setProductCheckData(jArray);
            }

        }

        private void setProductCheckData(JArray jArray)
        {
            long inventoryId;
            string approvalType;
            string repairUserId;
            int manufactureType;
            foreach (JObject obj in jArray.Children<JObject>())
            {
                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                approvalType = ConvertUtil.ToString(obj["APPROVAL_TYPE"]);
                repairUserId = ConvertUtil.ToString(obj["REPAIR_USER_ID"]);
                manufactureType = ConvertUtil.ToInt32(obj["MANUFACTURE_TYPE"]);

                DataRow dr = _dt.NewRow();

                dr["INVENTORY_ID"] = inventoryId;
                dr["CHECK"] = false;
                dr["ADJUSTMENT_STATE"] = obj["ADJUSTMENT_STATE"];
                dr["PRODUCT_TYPE"] = ConvertUtil.ToInt32(obj["PRODUCT_TYPE"]);
                
                //dr["REPAIR_DT"] = ConvertUtil.ToDateTimeNull(obj["REPAIR_DT"]);
                //dr["EXAMINE_DT"] = ConvertUtil.ToDateTimeNull(obj["EXAMINE_DT"]);

                //dr["QC_YN"] = ConvertUtil.ToString(obj["QC_YN"]);
                //dr["LOCATION"] = obj["LOCATION"];
                //dr["PALLET"] = obj["PALLET"];
                dr["COMPANY_ID"] = ConvertUtil.ToInt64(obj["COMPANY_ID"]);
                dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                dr["WAREHOUSING"] = obj["WAREHOUSING"];
                dr["BARCODE"] = obj["BARCODE"];

                if (manufactureType == 1)
                    dr["PRODUCT_PRICE"] = obj["LT_DEALER_PRICE_MAJOR"];
                else
                    dr["PRODUCT_PRICE"] = obj["LT_DEALER_PRICE_ETC"];


                //dr["PRODUCT_ADJUST_PRICE"] = obj["PRODUCT_ADJUST_PRICE"];
                dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                dr["MANUFACTURE_NM"] = obj["MANUFACTURE_NM"];
                dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NM"];
                dr["MBD_SN"] = obj["MBD_SN"];
                dr["SYSTEM_SN"] = obj["SYSTEM_SN"];
                dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                dr["CPU_MODEL_NM"] = obj["CPU_MODEL_NM"];

                dr["NTB_LIST_ID"] = obj["NTB_LIST_ID"];
                //dr["NTB_NICKNAME"] = obj["NTB_NICKNAME"];
                //dr["MON_SIZE"] = obj["MON_SIZE"];
                //dr["CHECK_YN"] = obj["CHECK_YN"];
                //dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];

                setCheckPriceInfo(inventoryId, ref dr);
                setCheckInfo(inventoryId, ref dr);

                _dt.Rows.Add(dr);
            }
        }

        private void setCheckPriceInfo(long inventoryId, ref DataRow dr)
        {

            DataRow[] rowsExamCheck = _dtExaminePrice.Select($"INVENTORY_ID = {inventoryId}");
            DataRow[] rowsRepairCheck = _dtRepairPrice.Select($"INVENTORY_ID = {inventoryId}");

            if (rowsExamCheck.Length < 1 && rowsRepairCheck.Length < 1)
            {
                foreach (KeyValuePair<string, string> item in ExamineInfo._dicListAdjustmentPriceColPair)
                    dr[item.Value] = 0;

                dr["PRODUCT_ADJUST_PRICE"] = 0;
                return;
            }
            else
            {
                if (rowsExamCheck.Length > 0)
                {
                    DataRow rowExamCheck = rowsExamCheck[0];

                    long price = 0;
                    long partPrice = 0;
                    foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                    {
                        partPrice = ConvertUtil.ToInt64(rowExamCheck[col]);
                        if (ExamineInfo._dicListAdjustmentPriceCol.ContainsKey(col))
                        {
                            price += partPrice;

                            if (partPrice < 0)
                                dr[ExamineInfo._dicListAdjustmentPriceCol[col]] = 1;
                            else
                                dr[ExamineInfo._dicListAdjustmentPriceCol[col]] = 0;

                        }
                        else
                            price += partPrice;
                    }

                    dr["PRODUCT_ADJUST_PRICE"] = price;
                }

                if (rowsRepairCheck.Length > 0)
                {
                    DataRow rowRepairCheck = rowsRepairCheck[0];

                    long price = 0;
                    long partPrice = 0;
                    foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                    {
                        partPrice = ConvertUtil.ToInt64(rowRepairCheck[col]);

                        if (ExamineInfo._dicListAdjustmentPriceColPair.ContainsKey(col))
                        {
                            price += partPrice;

                            if (partPrice < 0)
                                dr[ExamineInfo._dicListAdjustmentPriceColPair[col]] = 1;
                            else
                                dr[ExamineInfo._dicListAdjustmentPriceColPair[col]] = 0;

                        }
                        else
                            price += partPrice;
                    }

                    dr["PRODUCT_ADJUST_PRICE"] = price;
                }
            }
        }

        private void setCheckInfo(long inventoryId, ref DataRow dr)
        {

            DataRow[] rowsExamCheck = _dtExamine.Select($"INVENTORY_ID = {inventoryId}");
            DataRow[] rowsRepairCheck = _dtRepair.Select($"INVENTORY_ID = {inventoryId}");

            if (rowsExamCheck.Length > 0)
            {
                dr["EXAMINE_DT"] = ConvertUtil.ToDateTimeNull(rowsExamCheck[0]["UPDATE_DT"]);
                dr["EXAMINE_USER_ID"] = ConvertUtil.ToString(rowsExamCheck[0]["UPDATE_USER_ID"]);
            }

            if (rowsRepairCheck.Length > 0)
            {
                dr["REPAIR_DT"] = ConvertUtil.ToDateTimeNull(rowsRepairCheck[0]["UPDATE_DT"]);
                dr["REPAIR_USER_ID"] = ConvertUtil.ToString(rowsRepairCheck[0]["UPDATE_USER_ID"]);
            }


            if (rowsExamCheck.Length < 1)
            {
                //dr["CHECK_YN"] = false;
                dr["CHECK_TYPE"] = 0;
                return;
            }
            else if (rowsExamCheck.Length < 1 && rowsRepairCheck.Length < 1)
            {
                //dr["CHECK_YN"] = false;
                dr["CHECK_TYPE"] = 0;
                return;
            }
            else
            {
                //dr["CHECK_YN"] = true;

                if (rowsExamCheck.Length > 0)
                {
                    DataRow rowExamCheck = rowsExamCheck[0];
                    dr["PRODUCT_GRADE_EXAM"] = rowExamCheck["PRODUCT_GRADE"];
                    dr["CASE_DES"] = rowExamCheck["CASE_DES"];
                    dr["CASE_HINGE"] = rowExamCheck["CASE_HINGE"];
                    dr["CHECK_TYPE"] = 1;

                    int checkValue = 0;
                    List<string> checkList;
                    string content;

                    foreach (string col in ExamineInfo._NTBCOLNAME2ND)
                    {
                        content = "";
                        checkValue = ConvertUtil.ToInt32(rowExamCheck[col]);

                        if (checkValue > 0)
                        {
                            if (ExamineInfo._listCaseCheckCol.Contains(col))
                            {
                                if (ConvertUtil.ToInt32(dr["CASE_CNT"]) == 1)
                                {
                                    checkList = ExamineInfo._NTBCHECK2ND["CASE"];
                                    for (int i = 0; i < checkList.Count; i++)
                                    {
                                        if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                            content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[1]},";
                                        else
                                            content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[0]},";
                                    }
                                }
                            }
                            else
                            {
                                //if (ExamineInfo._dicListAdjustmentPriceColPair.ContainsKey(col) && ConvertUtil.ToInt32(dr[ExamineInfo._dicListAdjustmentPriceColPair[col]]) == 1)
                                {
                                    checkList = ExamineInfo._NTBCHECK2ND[col];
                                    for (int i = 0; i < checkList.Count; i++)
                                    {
                                        if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                            content += $"{ExamineInfo._NTBCHECK2ND[col][i]},";
                                    }
                                }
                            }

                            if (content.Length > 1)
                                content = content.Substring(0, content.Length - 1);

                            dr[col] = content;
                        }
                        else
                            dr[col] = "";
                    }

                    checkValue = ConvertUtil.ToInt32(rowExamCheck["BATTERY"]);
                    if ((checkValue & ExamineInfo._BASE[4]) == ExamineInfo._BASE[4])
                        dr["BATTERY"] += $"{dr["BATTERY"]}[{ rowExamCheck["BATTERY_REMAIN"]}%]";
                }

                if (rowsRepairCheck.Length > 0)
                {
                    DataRow rowExamCheck = rowsExamCheck[0];
                    DataRow rowRepairCheck = rowsRepairCheck[0];

                    DataRow drRepairCheck = _dtRepairCheck.NewRow();
                    drRepairCheck["INVENTORY_ID"] = inventoryId;

                    dr["PRODUCT_GRADE_REPAIR"] = rowRepairCheck["PRODUCT_GRADE"];
                    dr["CASE_DES_REPAIR"] = rowRepairCheck["CASE_DES"];
                    dr["CASE_HINGE_REPAIR"] = rowRepairCheck["CASE_HINGE"];
                    dr["CHECK_TYPE"] = 2;

                    if (rowExamCheck["CASE_HINGE"] == rowRepairCheck["CASE_HINGE"])
                        drRepairCheck["CASE_HINGE"] = 0;
                    //else if(ConvertUtil.ToInt32(rowExamCheck["CASE_HINGE"]) < ConvertUtil.ToInt32(rowRepairCheck["CASE_HINGE"]))
                    //    drRepairCheck["CASE_HINGE"] = 1;
                    else
                        drRepairCheck["CASE_HINGE"] = 2;

                    int checkValue = 0;
                    List<string> checkList;
                    string content;

                    foreach (string col in ExamineInfo._NTBCOLNAME2ND)
                    {
                        content = "";
                        checkValue = ConvertUtil.ToInt32(rowRepairCheck[col]);

                        if (checkValue > 0)
                        {
                            if (ExamineInfo._listCaseCheckCol.Contains(col))
                            {
                                if (ConvertUtil.ToInt32(dr["CASE_REPAIR_CNT"]) == 1)
                                {
                                    checkList = ExamineInfo._NTBCHECK2ND["CASE"];
                                    for (int i = 0; i < checkList.Count; i++)
                                    {
                                        if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                            content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[1]},";
                                        else
                                            content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[0]},";
                                    }
                                }
                            }
                            else
                            {
                                //if (ExamineInfo._dicListAdjustmentPriceColPair.ContainsKey(col) && ConvertUtil.ToInt32(dr[ExamineInfo._dicListAdjustmentPriceColPair[col]]) == 1)
                                {
                                    checkList = ExamineInfo._NTBCHECK2ND[col];
                                    for (int i = 0; i < checkList.Count; i++)
                                    {
                                        if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                            content += $"{ExamineInfo._NTBCHECK2ND[col][i]},";
                                    }
                                }
                            }

                            if (content.Length > 1)
                                content = content.Substring(0, content.Length - 1);

                            dr[$"{col}_REPAIR"] = content;
                        }
                        else
                            dr[$"{col}_REPAIR"] = "";

                        if (rowExamCheck[col] == rowRepairCheck[col])
                            drRepairCheck[col] = 0;
                        //else if (ConvertUtil.ToInt32(rowExamCheck[col]) < ConvertUtil.ToInt32(rowRepairCheck[col]))
                        //    drRepairCheck[col] = 1;
                        else
                            drRepairCheck[col] = 2;
                    }

                    checkValue = ConvertUtil.ToInt32(rowExamCheck["BATTERY"]);
                    if ((checkValue & ExamineInfo._BASE[4]) == ExamineInfo._BASE[4])
                    {
                        string batteryRemain = ConvertUtil.ToString(rowExamCheck["BATTERY_REMAIN"]);
                        dr["BATTERY_REPAIR"] += $"{dr["BATTERY_REPAIR"]}[{ batteryRemain}%]";
                    }

                    _dtRepairCheck.Rows.Add(drRepairCheck);
                }
            }
        }


        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("제품 정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                //var jArray = new JArray();
                JObject jobj = new JObject();
                JObject jResult = new JObject();

                jobj.Add("BULK_YN", 0);

                jobj.Add("INVENTORY_PTYPE", ConvertUtil.ToInt32(leProductType.EditValue));
                jobj.Add("MANUFACTURE_TYPE", ConvertUtil.ToInt32(leManufactureType.EditValue));
                jobj.Add("NTB_LIST_ID", ConvertUtil.ToInt64(leNickName.EditValue));
                jobj.Add("ADJUSTMENT_STATE", ConvertUtil.ToString(leAdjustmentState.EditValue));
                jobj.Add("INVENTORY_ID", _inventoryId);
                jobj.Add("UPDATE_ONE", 1);
                //jobj.Add("DATA", jArray);

                if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                {
                    int rowhandle = gvList.FocusedRowHandle;
                    int topRowIndex = gvList.TopRowIndex;
  
                    gvList.BeginDataUpdate();
                    _currentProduct.BeginEdit();
                    _currentProduct["PRODUCT_TYPE"] = ConvertUtil.ToInt32(leProductType.EditValue);
                    _currentProduct["MANUFACTURE_TYPE"] = ConvertUtil.ToString(leManufactureType.EditValue);
                    _currentProduct["NTB_LIST_ID"] = ConvertUtil.ToInt64(leNickName.EditValue);
                    _currentProduct["ADJUSTMENT_STATE"] = ConvertUtil.ToString(leAdjustmentState.EditValue);
                    _currentProduct.EndEdit();
                    gvList.EndDataUpdate();

                    gvList.FocusedRowHandle = rowhandle;
                    gvList.TopRowIndex = topRowIndex;

                    Dangol.Message("저장되었습니다.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private void getCheckInfo(long inventoryId)
        {
            JObject jResult = new JObject();
            string typeNm = "NTB";
            _dicProductCheck.Clear();
            _etcDes = "";
            _batteryRemain = "";
            _productGrade = "";
            _isAdjustmentExist = false;

            if (_productType == 2)
            {
                typeNm = "NTB";
                _dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            }
            else if (_productType == 3)
            {
                typeNm = "ALLINONE";
                _dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            }

            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                _dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;

            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = 0;


            if (DBConnect.getCheckInfo(inventoryId, typeNm, _checkType, ref jResult))
            {
                JObject jData = (JObject)jResult["DATA"];

                foreach (var x in jData)
                {
                    string name = x.Key;
                    if (!ProjectInfo._listCheckException.Contains(name))
                    {
                        if (name.Equals("CASE_DES"))
                            _etcDes = x.Value.ToObject<string>();
                        else if (name.Equals("BATTERY_REMAIN"))
                            _batteryRemain = x.Value.ToObject<string>();
                        else if (name.Equals("PRODUCT_GRADE"))
                            _productGrade = x.Value.ToObject<string>();
                        else
                        {
                            short value = x.Value.ToObject<short>();

                            if (!_dicProductCheck.ContainsKey(name))
                                _dicProductCheck.Add(name, value);
                        }
                    }
                }

                if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                {
                    JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];


                    if (_productType == 2)
                    {
                        _dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                            _dtNTBAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }
                    else if (_productType == 3)
                    {
                        _dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                            _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }
                }

                _isAdjustmentExist = true;
            }


            if (_checkType > 1)
            {
                short examCheckType = 1;
                _dicProductCheckHistory.Clear();
                _etcDesHistory = "";
                _batteryRemainHistory = "";
                _productGradeHistory = "";
                _isAdjustmentExistHistory = false;

                if (_productType == 2)
                    _dtNTBAdjustmentPrice.Rows[examCheckType]["EXIST"] = false;
                else if (_productType == 3)
                    _dtAllInOneAdjustmentPrice.Rows[examCheckType]["EXIST"] = false;

                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    _dtNTBAdjustmentPrice.Rows[examCheckType][col] = 0;

                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                    _dtAllInOneAdjustmentPrice.Rows[examCheckType][col] = 0;

                if (DBConnect.getCheckInfo(inventoryId, typeNm, examCheckType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                _etcDesHistory = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                _batteryRemainHistory = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                _productGradeHistory = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!_dicProductCheckHistory.ContainsKey(name))
                                    _dicProductCheckHistory.Add(name, value);
                            }

                            if (!_isAdjustmentExist)
                            {
                                if (name.Equals("CASE_DES"))
                                    _etcDes = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN"))
                                    _batteryRemain = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    _productGrade = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!_dicProductCheck.ContainsKey(name))
                                        _dicProductCheck.Add(name, value);
                                }
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        if (_productType == 2)
                        {
                            _dtNTBAdjustmentPrice.Rows[examCheckType]["EXIST"] = true;
                            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                                _dtNTBAdjustmentPrice.Rows[examCheckType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                            if (!_isAdjustmentExist)
                            {
                                _dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                                    _dtNTBAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                            }
                        }
                        else if (_productType == 3)
                        {

                            _dtAllInOneAdjustmentPrice.Rows[examCheckType]["EXIST"] = true;
                            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                                _dtAllInOneAdjustmentPrice.Rows[examCheckType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                            if (!_isAdjustmentExist)
                            {
                                _dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                                    _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                            }
                        }
                    }

                    _isAdjustmentExistHistory = true;
                }
            }
        }

        private void sbPrintPart_Click(object sender, EventArgs e)
        {
            if (_currentWarehousing == null)
            {
                Dangol.Message("입고번호가 없습니다.");
                return;
            }

            if (string.IsNullOrEmpty(_barcode) || _inventoryId < 1)
            {
                Dangol.Message("재고로 등록되지 않은 부품입니다.");
                return;
            }

            JObject jResult = new JObject();

            //if (DBConnect.printInventoryInfo(_representativeType, _representativeNo, _representativeCol, _barcode, lePrintPort.EditValue.ToString(), ref jResult))
            //{
            //    Dangol.Message("부품정보가 출력되었습니다.");
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}
        }

        private void sbPartCheck_Click(object sender, EventArgs e)
        {
            _checkType = ConvertUtil.ToInt16(leCheckType.EditValue);

            if(_checkType < 1)
            {
                Dangol.Message("검수타입을 선택하세요.");
                return;
            }

            getCheckInfo(_inventoryId);

            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

            
            JObject jResult = new JObject();

            if (_productType == 2)
            {
                long ntbListId = ConvertUtil.ToInt64(leNickName.EditValue);
                int manufactureType = ConvertUtil.ToInt32(leManufactureType.EditValue);

                if (ntbListId < 1)
                {
                    Dangol.Message("제품 사양을 선택해주세요");
                    return;
                }

                if (manufactureType < 1)
                {
                    Dangol.Message("제조사를 선택해주세요");
                    return;
                }

                _dicAdjustmentPrice.Clear();
                if (DBConnect.getNTBAdjustmentPrice(_representativeNo, _representativeType, ntbListId, ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string col = "";
                    if (manufactureType == 1)
                        col = "MAJOR_PRICE";
                    else
                        col = "ETC_PRICE";
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string key = ConvertUtil.ToString(obj["COL_NM"]);

                        if (!_dicAdjustmentPrice.ContainsKey(key))
                            _dicAdjustmentPrice.Add(key, ConvertUtil.ToInt64(obj[col]));
                    }
                }
            }
            else if(_productType == 3)
            {
                _dicAdjustmentPrice.Clear();
            }
            else
            {
                Dangol.Message("제품 검수는 노트북, 올인원PC만 가능합니다.");
                return;
            }

            if (DBConnect.getProductInfo(_inventoryId, ref jResult))
            {
                string approveType = ConvertUtil.ToString(jResult["APPROVE_TYPE"]);
                int adjustmentState = ConvertUtil.ToInt32(jResult["ADJUSTMENT_STATE"]);

                if (!string.IsNullOrWhiteSpace(approveType) && adjustmentState == 1)
                {
                    if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                    {
                        if(Dangol.MessageYN("승인완료&정산대기인 제품입니다. 계속 진행하시겠습니까?") == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Dangol.Message("승인완료 & 정산대기인 제품은 검수를 변경할 수 없습니다.");
                        return;
                    }
                }
            }
            

            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

            if (_productType == 2)
            {
                string dtStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                using (DlgNtb2ndEditionCheck2 ntbCheck = new DlgNtb2ndEditionCheck2(_dicProductCheck, _dicProductCheckHistory, ref _dtNTBAdjustmentPrice, _dicAdjustmentPrice,_etcDes,
                    _batteryRemain, _productGrade, _dtPrintPort, _dtPGrade, null, -1, _checkType))
                {
                    if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        _etcDes = ntbCheck._caseDestroyDescription;
                        _batteryRemain = ntbCheck._batteryRemain;
                        _productGrade = ntbCheck._pGrade;

                        string dtEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, dtStart, dtEnd, _barcode, _inventoryId, _checkType, _dicProductCheck, new List<long>(new[] {_inventoryId }), _etcDes, _batteryRemain, _productGrade, 1);

                        if (!DBAdjustment.insertAdjustmentPrice(_inventoryId, _checkType, "insertAdjustmentPrice", ExamineInfo._listAdjustmentPriceColShort, _dtNTBAdjustmentPrice, ref jResult))
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }

                        _currentProduct.BeginEdit();

                        if (_checkType == 1)
                        {
                            //_currentProduct["EXAM_CHECK_YN"] = "Y";
                            //_currentProduct["EXAM_ADJUST_YN"] = "Y";
                            _currentProduct["EXAM_DT"] = DateTime.Today.ToString("yyyy-MM-dd");
                            _currentProduct["EXAM_USER_ID"] = ProjectInfo._userId;
                            _currentProduct["PRODUCT_GRADE_EXAM"] = _productGrade;


                        }
                        else if(_checkType == 3)
                        {
                            //_currentProduct["REPAIR_CHECK_YN"] = "Y";
                            //_currentProduct["REPAIR_ADJUST_YN"] = "Y";
                            _currentProduct["REPAIR_DT"] = DateTime.Today.ToString("yyyy-MM-dd");
                            _currentProduct["REPAIR_USER_ID"] = ProjectInfo._userId;
                            _currentProduct["PRODUCT_GRADE_REPAIR"] = _productGrade;
                        }

                        int state = ConvertUtil.ToInt32(_currentProduct["ADJUSTMENT_STATE"]);

                        if (state < 3)
                        {
                            if (_productGrade.Equals("1") || _productGrade.Equals("2") || _productGrade.Equals("3"))
                                _currentProduct["ADJUSTMENT_STATE"] = "1";
                            else if (_productGrade.Equals("4"))
                                _currentProduct["ADJUSTMENT_STATE"] = "2";
                            else if (_productGrade.Equals("5"))
                                _currentProduct["ADJUSTMENT_STATE"] = "3";
                            else
                                _currentProduct["ADJUSTMENT_STATE"] = "0";
                        }

                        _currentProduct.EndEdit();


                        if (ntbCheck._isPrint)
                        {
                            if (DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, _inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                            {
                                Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                            }
                            else
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }
                        }
                    }

                    if (ntbCheck._isCapture)
                    {
                        //JObject jobj = new JObject();

                        //jobj.Add("BULK_YN", 0);
                        //jobj.Add("UPDATE_ONE", 0);
                        //jobj.Add("INVENTORY_ID", _inventoryId);
                        //jobj.Add("CAPTURE_YN", 1);

                        //DBConnect.updateProductInfo(jobj, ref jResult);
                    }
                }
            }
            else if (_productType == 3)
            {
                using (DlgAllInOneCheck AllInOneCheck = new DlgAllInOneCheck(_dicProductCheck, _dicProductCheckHistory, ref _dtAllInOneAdjustmentPrice, _dicAdjustmentPrice, _etcDes,
                    _productGrade, _dtPrintPort, _dtPGrade, null, -1, _checkType))
                {
                    if (AllInOneCheck.ShowDialog(this) == DialogResult.OK)
                    {
                       _etcDes = AllInOneCheck._etcDes;
                        _productGrade = AllInOneCheck._pGrade;

                        if (DBConnect.insertAllInOneCheck(_representativeType, _representativeNo, _representativeCol, _barcode, _inventoryId, _checkType, _dicProductCheck, new List<long>(new[] { _inventoryId }), _etcDes, _productGrade, 1))
                        {
                            if (!DBAdjustment.insertAdjustmentPrice(_inventoryId, _checkType, "insertAdjustmentAllInOnePrice", ExamineInfo._listAdjustmentAllInOnePriceColShort, _dtAllInOneAdjustmentPrice, ref jResult))
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }

                            if (AllInOneCheck._isPrint)
                            {
                                if (DBConnect.printAllInOneProduct(_representativeType, _representativeNo, _representativeCol, _inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                                {
                                    Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                                }
                                else
                                {
                                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                }
                            }
                        }
                    }
                }
            }

        }

        private void leProductType_EditValueChanged(object sender, EventArgs e)
        {
            _productType = ConvertUtil.ToInt32(leProductType.EditValue);
        }

        private void lcgWarehousing_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWarehousingList.FocusedRowHandle;
            gvWarehousingList.FocusedRowHandle = -2147483646;
            gvWarehousingList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtWarehousing.Select("CHECK = TRUE");


            if (rows.Length < 1)
            {
                Dangol.Message("입고번호를 선택하세요");
                return;
            }

            if (Dangol.MessageYN("선택하신 입고번호의 제품 정보를 확인하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            _listRepresentativeId.Clear();

            foreach (DataRow row in rows)
                _listRepresentativeId.Add(ConvertUtil.ToInt64(row["WAREHOUSING_ID"]));

            Dangol.ShowSplash();

            getProduct();

            Dangol.CloseSplash();
        }

        private void lcgWarehousing_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWarehousingList.FocusedRowHandle;
            int topRowIndex = gvWarehousingList.TopRowIndex;
            gvWarehousingList.FocusedRowHandle = -2147483646;
            gvWarehousingList.FocusedRowHandle = rowhandle;

            try
            {
                gvWarehousingList.BeginUpdate();
                foreach (DataRow row in _dtWarehousing.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvWarehousingList.DataRowCount; i++)
                {
                    int rowHandle = gvWarehousingList.GetVisibleRowHandle(i);
                    rows.Add(gvWarehousingList.GetDataRow(rowHandle));
                }

                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    // Change the field value.
                    row["CHECK"] = true;
                }
            }
            finally
            {
                gvWarehousingList.EndUpdate();
            }
        }

        private void lcgWarehousing_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWarehousingList.FocusedRowHandle;
            int topRowIndex = gvWarehousingList.TopRowIndex;
            gvWarehousingList.FocusedRowHandle = -2147483646;
            gvWarehousingList.FocusedRowHandle = rowhandle;

            gvWarehousingList.BeginDataUpdate();

            foreach (DataRow row in _dtWarehousing.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvWarehousingList.EndDataUpdate();
        }

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcList.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                refresh();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                ImageInfo.GetImage(1, true, _barcode);
            }
        }

        private void refresh()
        {
            Dangol.ShowSplash();

            int topRowIndex = gvList.TopRowIndex;
            string barcode = _barcode;
            gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged -= gvList_FocusedRowChanged;
            getProduct();
            gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged += gvList_FocusedRowChanged;

            int rowHandle = gvList.LocateByValue("BARCODE", barcode);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvList.FocusedRowHandle = -2147483646;
                    gvList.FocusedRowHandle = rowHandle;
                    gvList.TopRowIndex = topRowIndex;
                }
            }
            else
            {
                if (_dt.Rows.Count > 0)
                {
                    gvList.FocusedRowHandle = -2147483646;
                    gvList.MoveFirst();
                }
            }

            Dangol.CloseSplash();
        }

        private void gcList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                refresh();
            }
        }
    }
}