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
using Newtonsoft.Json.Linq;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.adjustment
{
    public partial class usrAdjustmentCompleteList : DevExpress.XtraEditors.XtraForm
    {
        int _viewCategory = (int)Enum.EnumView.Adjustment;
        int _viewType = (int)Enum.EnumTableAdjustment.CompleteList;

        DataTable _dtWarehousing;
        DataTable _dt;
        DataTable _dtRepairCheck;

        DataTable _dtExamine;
        DataTable _dtRepair;

        DataTable _dtExaminePrice;
        DataTable _dtRepairPrice;

        BindingSource _bsWarehousing;
        BindingSource _bs;

        DataRowView _currentRow = null;
  
        List<long> _listWarehousingId;

        List<string> _listVisibleCol;
        List<string> _listHideCol;

        List<string> _listMasterCol;
        List<string> _listReadOnlyCol;

        List<string> _lisDefaultHideCol;

        List<string> _lisAllowUser;
        

        public usrAdjustmentCompleteList()
        {
            InitializeComponent();

            _dtWarehousing = new DataTable();
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_STATE", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtWarehousing.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("PROCESSING_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("EXAM_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("COMPLETE_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("REMAIN_CNT", typeof(int)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("ADJUSTMENT_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_USER1", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_DT1", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_USER2", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_DT2", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAMINE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("REPAIR_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAMINE_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("REPAIR_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("LOCATION", typeof(long)));
            _dt.Columns.Add(new DataColumn("PALLET", typeof(long)));
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

            _dt.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dt.Columns.Add(new DataColumn("CPU_MODEL_NM", typeof(string)));
            //_dt.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            _dt.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("MON_SIZE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dt.Columns.Add(new DataColumn("CHECK_TYPE", typeof(int))); //1:EXAMINE, 2:REPAIR
            _dt.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
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

            //_bsWarehousing.DataSource = _dtWarehousing;
            //_bs.DataSource = _dt;

            _listWarehousingId = new List<long>();
            _listVisibleCol = new List<string>();
            _listHideCol = new List<string>();
            _listMasterCol = new List<string>(new[] { "PRODUCT_PRICE", "PRODUCT_ADJUST_PRICE","PASSWORD_CNT","DISPLAY_CNT","KEYBOARD_CNT","USB_CNT","CASE_CNT","WIRELESS_CNT","BATTERY_CNT","ETC_CNT"});
            _listReadOnlyCol = new List<string>(new[] { "BARCODE"});
            _lisDefaultHideCol = new List<string>(new[] { "INVENTORY_ID", "CATEGORY", "MON_SIZE", "STATE" });
            _lisAllowUser = new List<string>(new[] { "jblee", "rookieson", "shlee"});
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            getVisibleCol();
            setGridViewColumsWarehousing();
            setGridViewColums();

            JObject jResult = new JObject();
            getWarehousingList(ref jResult);
            //getUsedPurchaseList(ref jResult);

        }

        private void setInfoBox()
        {
            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");
          
            DataTable dtCategory = Util.getCodeList("CD1101", "KEY", "VALUE");
            Util.LookupEditHelper(rileCategory, dtCategory, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(rileNickName, dtNickName, "KEY", "VALUE");

            DataTable dtProductGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductGrade, dtProductGrade, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileComapnyId2, dtCompany, "KEY", "VALUE");
            

            DataTable dtWarehousingState = Util.getCodeList("CD0601", "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");


            Util.LookupEditHelper(rileLocation, ProjectInfo._dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(rilePallet, ProjectInfo._dtPallet, "PALLET_ID", "PALLET_NM");

            //DataTable dtHinge = new DataTable();

            //dtHinge.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtHinge.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtHinge, 0, "");
            //Util.insertRowonTop(dtHinge, 1, "힌지파손");

            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            DataTable dtHinge = new DataTable();

            dtHinge.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtHinge.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtHinge, 0, "");
            Util.insertRowonTop(dtHinge, 1, "힌지파손");
            Util.LookupEditHelper(rileHinge, dtHinge, "KEY", "VALUE");


            var today = DateTime.Today;
           var pastDate = today.AddDays(-30);

           deDtFrom.EditValue = pastDate;
           deDtTo.EditValue = today;
           leProductGrade.ItemIndex = 0;

        }

        private void setIInitData()
        {
            gcWarehousingList.DataSource = null;
            _bsWarehousing.DataSource = _dtWarehousing;
            gcWarehousingList.DataSource = _bsWarehousing;

            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;
        }

        

        private void getVisibleCol()
        {
            JObject jData = new JObject();
            JObject jResult = new JObject();

            jData.Add("VIEW_CATEGORY", _viewCategory);
            jData.Add("VIEW_TYPE", _viewType);

            _listVisibleCol.Clear();
            _listHideCol.Clear();

            if (DBConnect.getVisibleCol(jData, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                string filedName;
                int visible;
                foreach (JObject obj in jArray.Children<JObject>())
                {

                    filedName = ConvertUtil.ToString(obj["FIELD_NAME"]);
                    visible = ConvertUtil.ToInt32(obj["VISIBLE_YN"]);

                    if (visible == 1)
                        _listVisibleCol.Add(filedName);
                    else
                        _listHideCol.Add(filedName);
                }
            }
            else
            {
            }
        }


        private void setGridViewColums()
        {
            lcgBarcodeList.BeginUpdate();
            lcList.BeginInit();
            gvList.BeginUpdate();
            foreach (GridColumn gridcol in gvList.Columns)
            {
                if (_listHideCol.Contains(gridcol.FieldName) || _lisDefaultHideCol.Contains(gridcol.FieldName))
                    gridcol.Visible = false;
                else
                    gridcol.Visible = true;
            }

            if(ProjectInfo._userType.Equals("M") || ProjectInfo._userId.Equals("jblee") || ProjectInfo._userId.Equals("rookieson"))
            {
                gcInitPrice.Visible = true;
                gcAdjust.Visible = true;
            }
            else
            {
                gcInitPrice.Visible = false;
                gcAdjust.Visible = false;
            }


            gvList.EndUpdate();
            lcList.EndInit();
            lcgBarcodeList.EndUpdate();
        }

        private void setGridViewColumsWarehousing()
        {
            lcgBarcodeList.BeginUpdate();
            lcList.BeginInit();
            gvList.BeginUpdate();
            gvWarehousingList.BeginUpdate();
            foreach (GridColumn gridcol in gvList.Columns)
            {
                if (_listHideCol.Contains(gridcol.FieldName) || _lisDefaultHideCol.Contains(gridcol.FieldName))
                    gridcol.Visible = false;
                else
                    gridcol.Visible = true;
            }

            if (ProjectInfo._userType.Equals("M") || _lisAllowUser.Contains(ProjectInfo._userId))
            {
                gcInitPrice.Visible = true;
                gcAdjust.Visible = true;

                gcComplete.Visible = true;
                gcProcess.Visible = true;
                //gcRemain.Visible = true;

                gcPasswordCnt.Visible = true;
                gcDisplayCnt.Visible = true;
                gcKeyboardCnt.Visible = true;
                gcUsbCnt.Visible = true;
                gcCaseCnt.Visible = true;
                gcWirelessCnt.Visible = true;
                gcBatteryCnt.Visible = true;
                gcEtcCnt.Visible = true;

                //lcgBarcodeList.CustomHeaderButtons[0].Properties.Visible = true;
                //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = true;
                //lcgBarcodeList.CustomHeaderButtons[3].Properties.Visible = true;
            }
            else
            {
                gcInitPrice.Visible = false;
                gcAdjust.Visible = false;

                gcComplete.Visible = false;
                gcProcess.Visible = false;
                //gcRemain.Visible = false;

                gcPasswordCnt.Visible = false;
                gcDisplayCnt.Visible = false;
                gcKeyboardCnt.Visible = false;
                gcUsbCnt.Visible = false;
                gcCaseCnt.Visible = false;
                gcWirelessCnt.Visible = false;
                gcBatteryCnt.Visible = false;
                gcEtcCnt.Visible = false;

                //lcgBarcodeList.CustomHeaderButtons[0].Properties.Visible = false;
                //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
                //lcgBarcodeList.CustomHeaderButtons[3].Properties.Visible = false;
            }

            gvWarehousingList.EndUpdate();
            gvList.EndUpdate();
            lcList.EndInit();
            lcgBarcodeList.EndUpdate();
        }

     

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }

        private bool getWarehousingList(ref JObject jResult)
        {
            JObject jData = new JObject();

            //if (!checkSearch(ref jData))
            //{
            //    Dangol.Message(jData["MSG"]);
            //    return false;
            //}
            jData.Add("WAREHOUSING_TYPE", 1);
            jData.Add("WAREHOUSING_CATEGORY", 2);

            _dtWarehousing.Clear();

            if (DBAdjustment.getWarehousingList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehousing.NewRow();

                        dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dr["WAREHOUSING"] = obj["WAREHOUSING"];
                        dr["WAREHOUSING_DT"] = ConvertUtil.ToDateTimeNull(obj["WAREHOUSING_DT"]);
                        dr["WAREHOUSING_STATE"] = obj["WAREHOUSING_STATE"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["PART_CNT"] = obj["PART_CNT"];
                        dr["EXAM_CNT"] = obj["EXAM_CNT"];
                        dr["COMPLETE_CNT"] = obj["COMPLETE_CNT"];
                        dr["REMAIN_CNT"] = obj["REMAIN_CNT"];
                        dr["PROCESSING_CNT"] = obj["PROCESSING_CNT"];
                        dr["CHECK"] = false;
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
                dr["CHECK_YN"] = false;
                dr["CHECK_TYPE"] = 0;
                return;
            }
            else if (rowsExamCheck.Length < 1 && rowsRepairCheck.Length < 1)
            {
                dr["CHECK_YN"] = false;
                dr["CHECK_TYPE"] = 0;
                return;
            }
            else
            {
                dr["CHECK_YN"] = true;
                if (rowsRepairCheck.Length > 0)
                {
                    DataRow rowExamCheck = rowsExamCheck[0];
                    DataRow rowRepairCheck = rowsRepairCheck[0];

                    DataRow drRepairCheck = _dtRepairCheck.NewRow();
                    drRepairCheck["INVENTORY_ID"] = inventoryId;

                    dr["PRODUCT_GRADE"] = rowRepairCheck["PRODUCT_GRADE"];
                    dr["CASE_DES"] = rowRepairCheck["CASE_DES"];
                    dr["CASE_HINGE"] = rowRepairCheck["CASE_HINGE"];
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
                                if (ExamineInfo._dicListAdjustmentPriceColPair.ContainsKey(col) && ConvertUtil.ToInt32(dr[ExamineInfo._dicListAdjustmentPriceColPair[col]]) == 1)
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
                        dr["BATTERY"] += $"{dr["BATTERY"]}[{ batteryRemain}%]";
                    }

                    _dtRepairCheck.Rows.Add(drRepairCheck);
                }
                else
                {
                    DataRow rowExamCheck = rowsExamCheck[0];
                    dr["PRODUCT_GRADE"] = rowExamCheck["PRODUCT_GRADE"];
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
                                if (ExamineInfo._dicListAdjustmentPriceColPair.ContainsKey(col) && ConvertUtil.ToInt32(dr[ExamineInfo._dicListAdjustmentPriceColPair[col]]) == 1)
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
            else if (rowsRepairCheck.Length > 0)
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
            else
            {
                DataRow rowExamCheck = rowsExamCheck[0];

                long price = 0;
                long partPrice = 0;
                foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                {
                    partPrice = ConvertUtil.ToInt64(rowExamCheck[col]);
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
                dr["APPROVAL_TYPE"] = obj["APPROVAL_TYPE"];
                dr["ADJUSTMENT_STATE"] = obj["ADJUSTMENT_STATE"];
               
                dr["APPROVAL_USER1"] = obj["APPROVAL_USER1"];
                dr["APPROVAL_DT1"] = ConvertUtil.ToDateTimeNull(obj["APPROVAL_DT1"]);
               
                dr["APPROVAL_USER2"] = obj["APPROVAL_USER2"];
                dr["APPROVAL_DT2"] = ConvertUtil.ToDateTimeNull(obj["APPROVAL_DT2"]);

                dr["REPAIR_DT"] = ConvertUtil.ToDateTimeNull(obj["REPAIR_DT"]);
                dr["EXAMINE_DT"] = ConvertUtil.ToDateTimeNull(obj["EXAMINE_DT"]);
                dr["LOCATION"] = obj["LOCATION"];
                dr["PALLET"] = obj["PALLET"];
                dr["COMPANY_ID"] = obj["COMPANY_ID"];
                dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                dr["WAREHOUSING"] = obj["WAREHOUSING"];
                dr["BARCODE"] = obj["BARCODE"];

                if(manufactureType == 1)
                    dr["PRODUCT_PRICE"] = obj["LT_DEALER_PRICE_MAJOR"];
                else
                    dr["PRODUCT_PRICE"] = obj["LT_DEALER_PRICE_ETC"];


                //dr["PRODUCT_ADJUST_PRICE"] = obj["PRODUCT_ADJUST_PRICE"];
                dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                dr["MANUFACTURE_NM"] = obj["MANUFACTURE_NM"];
                dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NM"];
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
        private bool geList(ref JObject jResult)
        {
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dt.Clear();
            _dtExamine.Clear();
            _dtRepair.Clear();
            _dtRepairCheck.Clear();

            jData.Add("PRODUCT_TYPE", 2); //노트북
            jData.Add("ADJUSTMENT_STATE", "3,5");
            jData.Add("TN_TABLE_PART", "TN_WAREHOUSING_PART");
            jData.Add("TN_TABLE", "TN_WAREHOUSING");
            jData.Add("REPRESENTATIVE_ID", "WAREHOUSING_ID");
            jData.Add("REPRESENTATIVE_KEY", "WAREHOUSING");
            jData.Add("LIST_WAREHOUSING_ID", string.Join(",", _listWarehousingId));
            

            if (DBAdjustment.getProductList(jData, ref jResult))
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

            if (diffDay > 180)
            {
                jData.Add("MSG", "검색 기간은 180일을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("CREATE_DT_S", dtFrom);
            jData.Add("CREATE_DT_E", dtTo);




            //if (!string.IsNullOrEmpty(ConvertUtil.ToString(teReceip.Text)))
            //    jData.Add("RECEIPT", teReceip.Text);

            //if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCustomerNm.Text)))
            //    jData.Add("USER_NM", teCustomerNm.Text);

            //if (!teTel.Text.Equals(ConvertUtil.ToString(teTel.Text)))
            //    jData.Add("TEL", teTel.Text);

            //if (ConvertUtil.ToInt32(leUserType1.EditValue) >= 0)
            //    jData.Add("USER_TYPE1", ConvertUtil.ToInt32(leUserType1.EditValue));

            //if (ConvertUtil.ToInt32(leUserType2.EditValue) >= 0)
            //    jData.Add("USER_TYPE2", ConvertUtil.ToInt32(leUserType2.EditValue));

            if (ConvertUtil.ToInt32(leProductGrade.EditValue) >= 0)
                jData.Add("PRODUCT_GRADE", ConvertUtil.ToString(leProductGrade.EditValue));

            return true;
        }

        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("STATE = 2");
                if (rows.Length < 1)
                {
                    Dangol.Message("변경사항이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("변경사항을 저장하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 0);

                    foreach (DataRow row in rows)
                    {
                        JObject jdata = new JObject();
                        jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        jdata.Add("INIT_PRICE", ConvertUtil.ToString(row["PRODUCT_PRICE"]));
                        jdata.Add("ADJUST_PRICE", ConvertUtil.ToString(row["PRODUCT_ADJUST_PRICE"]));
                        jdata.Add("MANUFACTURE_TYPE", ConvertUtil.ToString(row["MANUFACTURE_TYPE"]));
                        jdata.Add("NTB_LIST_ID", ConvertUtil.ToString(row["NTB_LIST_ID"]));
                        jArray.Add(jdata);
                    }

                    jobj.Add("DATA", jArray);

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["STATE"] = 0;
                        }

                        gvList.EndDataUpdate();
                        Dangol.CloseSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 제품을 승인하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();
                    
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    bool isAdmin = false;
                    string approvalType = "";
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_TYPE", ProjectInfo._userType);

                    if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                        isAdmin = true;

                    if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    {
                        jobj.Add("ADJUSTMENT_STATE", 1);
                        jobj.Add("APPROVAL_USER2", ProjectInfo._userId);
                        jobj.Add("APPROVAL_DT2", now);
                    }
                    else
                    {                    
                        jobj.Add("APPROVAL_USER1", ProjectInfo._userId);
                        jobj.Add("APPROVAL_DT1", now);
                    }
                    List<long> listInventoryId = new List<long>();
                    foreach (DataRow row in rows)
                    {
                        if(isAdmin)
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        else
                        {
                            approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);
                            if(!approvalType.Equals("M") && !approvalType.Equals("S"))
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        now = DateTime.Today.ToString("yyyy-MM-dd");

                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            
                            foreach (DataRow row in rows)
                            {
                                row["APPROVAL_TYPE"] = ProjectInfo._userType;
                                row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                row["APPROVAL_DT"] = now;
                                row["ADJUSTMENT_STATE"] = 1;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in rows)
                            {
                                if (isAdmin)
                                    listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                                else
                                {
                                    approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);
                                    if (!approvalType.Equals("M") && !approvalType.Equals("S"))
                                    {
                                        row["APPROVAL_TYPE"] = ProjectInfo._userType;
                                        row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                        row["APPROVAL_DT"] = now;
                                    }
                                }
                            }
                        }

                        gvList.EndDataUpdate();
                        Dangol.CloseSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(9))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 제품을 승인취소하시겠습니까?(본인 승인만 취소 가능)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    string userId = "";
                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_TYPE", "");

                    if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    {
                        jobj.Add("APPROVAL_USER2", "");
                        jobj.Add("APPROVAL_DT2", "-1");
                    }
                    else
                    {
                        jobj.Add("APPROVAL_USER1", "");
                        jobj.Add("APPROVAL_DT1", "-1");
                    }

                    List<long> listInventoryId = new List<long>();
                    foreach (DataRow row in rows)
                    {
                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        }
                        else
                        {
                            userId = ConvertUtil.ToString(row["APPROVAL_USER_ID"]);

                            if (userId.Equals(ProjectInfo._userId))
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        }
                        
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();
                       
                        foreach (DataRow row in rows)
                        {
                            userId = ConvertUtil.ToString(row["APPROVAL_USER_ID"]);

                            if (userId.Equals(ProjectInfo._userId))
                            {
                                row["APPROVAL_TYPE"] = "";
                                row["APPROVAL_USER_ID"] = "";
                                row["APPROVAL_DT"] = "";
                            }
                        }              

                        gvList.EndDataUpdate();
                        Dangol.CloseSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 제품으로 정산을 진행하시겠습니까(정산 대기 제품 & 관리자 승인만 진행)?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    List<long> listInventoryId = new List<long>();
                    List<long> listRepresentativeId = new List<long>();
                    string adjustmentState;
                    string approvalType = "";
                    foreach (DataRow row in rows)
                    {
                        adjustmentState = ConvertUtil.ToString(row["ADJUSTMENT_STATE"]);
                        approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);

                        if (adjustmentState.Equals("1") && (approvalType.Equals("M") || approvalType.Equals("S")))
                        {
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));

                            long representativeId = ConvertUtil.ToInt64(row["WAREHOUSING_ID"]);
                            if (!listRepresentativeId.Contains(representativeId))
                            {
                                listRepresentativeId.Add(representativeId);
                            }
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                    jobj.Add("LIST_WAREHOUSING_ID", string.Join(",", listRepresentativeId));
                    jobj.Add("ADJUSTMENT_STATE", 1);
                    jobj.Add("COMPANY_ID", 1);
                    jobj.Add("MAJOR_COL", "LT_DEALER_PRICE_MAJOR");
                    jobj.Add("ETC_COL", "LT_DEALER_PRICE_ETC");
                    jobj.Add("MAJOR_ADJUST_COL", "LT_DEALER_PRICE");
                    jobj.Add("ETC_ADJUST_COL", "LT_DEALER_PRICE_ETC");


                    if (DBAdjustment.getAdjustNtbData(jobj, ref jResult))
                    {
                        using (usrAdjustment Adjustment = new usrAdjustment(jResult, listInventoryId))
                        {
                            Adjustment.geList();
                            Dangol.CloseSplash();
                            if (Adjustment.ShowDialog(this) == DialogResult.OK)
                            {

                            }
                        }

                        //Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(4))
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
        }

        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -1;
            gvList.FocusedRowHandle = rowhandle;

            try
            {
                gvList.BeginUpdate();
                foreach (DataRow row in _dt.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvList.DataRowCount; i++)
                {
                    int rowHandle = gvList.GetVisibleRowHandle(i);
                    rows.Add(gvList.GetDataRow(rowHandle));
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
                gvList.EndUpdate();
            }
        }

        private void lcgBarcodeList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -1;
            gvList.FocusedRowHandle = rowhandle;

            gvList.BeginDataUpdate();

            foreach (DataRow row in _dt.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvList.EndDataUpdate();
        }

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWarehousingList.FocusedRowHandle;
            gvWarehousingList.FocusedRowHandle = -1;
            gvWarehousingList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtWarehousing.Select("CHECK = TRUE");


            if(rows.Length < 1)
            {
                Dangol.Message("입고번호를 선택하세요");
                return;
            }

            if (Dangol.MessageYN("선택하신 입고번호의 제품 정보를 확인하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            _listWarehousingId.Clear();

            foreach (DataRow row in rows)
                _listWarehousingId.Add(ConvertUtil.ToInt64(row["WAREHOUSING_ID"]));

            Dangol.ShowSplash();

            JObject jresult = new JObject();
            geList(ref jresult);

            Dangol.CloseSplash();
        }

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "CHECK" )
            {
                int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                if (state == 0)
                    _currentRow["STATE"] = 2;
            }
            
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "BARCODE")
            {
                string state = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]);

                if (state.Equals("2"))
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
        }

        private void sbSetColVisible_Click(object sender, EventArgs e)
        {
            List<string> listHideCol = new List<string>();

            if (!ProjectInfo._userType.Equals("M") && !_lisAllowUser.Contains(ProjectInfo._userId))
            {
                listHideCol = _listMasterCol;
            }
                
            using (dlgColVisible colVisible = new dlgColVisible(_viewCategory, _viewType, _listReadOnlyCol, listHideCol))
            {
                if (colVisible.ShowDialog(this) == DialogResult.OK)
                {
                    _listHideCol = colVisible._listHideCol;
                    _listVisibleCol = colVisible._listVisibleCol;

                    setGridViewColums();
                }
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();

            JObject jresult = new JObject();
            geList(ref jresult);

            Dangol.CloseSplash();
        }

        private void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWarehousingList.FocusedRowHandle;
            int topRowIndex = gvWarehousingList.TopRowIndex;
            gvWarehousingList.FocusedRowHandle = -1;
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

        private void layoutControlGroup1_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWarehousingList.FocusedRowHandle;
            int topRowIndex = gvWarehousingList.TopRowIndex;
            gvWarehousingList.FocusedRowHandle = -1;
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
    }
}