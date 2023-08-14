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
using DevExpress.XtraEditors.Controls;

namespace WareHousingMaster.view.adjustment
{
    public partial class usrAdjustmentReceiptList : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtAdjustment;
        DataTable _dt;
        DataTable _dtRepairCheck;

        DataTable _dtExamine;
        DataTable _dtRepair;

        DataTable _dtExaminePrice;
        DataTable _dtRepairPrice;

        BindingSource _bsAdjustmentReceipt;
        BindingSource _bs;

        DataRowView _currentAdjustment = null;
        DataRowView _currentRow = null;

        long _adjustmentId = -1;

        string _adjustmentState = "";


        List<long> _listWarehousingId;

        List<long> _listInventoryId = null;
        List<long> _listRepresentativeId = null;

        Dictionary<string, string> _dicAdjustmentState;

        List<string> _listAllowState = null;

        bool _isLoad = true;

        public usrAdjustmentReceiptList()
        {
            InitializeComponent();

            _dtAdjustment = new DataTable();
            _dtAdjustment.Columns.Add(new DataColumn("ADJUSTMENT_ID", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("ADJUSTMENT", typeof(string)));
            _dtAdjustment.Columns.Add(new DataColumn("ADJUSTMENT_STATE", typeof(string)));
            _dtAdjustment.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtAdjustment.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));

            _dtAdjustment.Columns.Add(new DataColumn("PRODUCT_CNT", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("PRODUCT_PRICE", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("PRODUCT_CNT_O", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("TOTAL_PRICE_0", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("ADJUSTMENT_PRICE", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("PURCHASED_PRICE", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("TAX", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long))); 
            _dtAdjustment.Columns.Add(new DataColumn("TOTAL_PRICE_O", typeof(long)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("ADJUSTMENT_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
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

            _bsAdjustmentReceipt = new BindingSource();
            _bs = new BindingSource();

            //_bsAdjustmentReceipt.DataSource = _dtAdjustment;
            //_bs.DataSource = _dt;

            _listWarehousingId = new List<long>();

            _listInventoryId = new List<long>();
            _listRepresentativeId = new List<long>();

            _dicAdjustmentState = new Dictionary<string, string>();

            _listAllowState = new List<string>(new[] { "1", "2", "3" });
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

            JObject jResult = new JObject();
            getAdjustmentReceiptList(ref jResult);
            //getUsedPurchaseList(ref jResult);


            teProductCnt.DataBindings.Add(new Binding("Text", _bsAdjustmentReceipt, "PRODUCT_CNT", false, DataSourceUpdateMode.Never));
            teProductPrice.DataBindings.Add(new Binding("Text", _bsAdjustmentReceipt, "PRODUCT_PRICE", false, DataSourceUpdateMode.Never));
            teAdjustmentPrice.DataBindings.Add(new Binding("Text", _bsAdjustmentReceipt, "ADJUSTMENT_PRICE", false, DataSourceUpdateMode.Never));
            tePurchasedPrice.DataBindings.Add(new Binding("Text", _bsAdjustmentReceipt, "PURCHASED_PRICE", false, DataSourceUpdateMode.Never));
            teTax.DataBindings.Add(new Binding("Text", _bsAdjustmentReceipt, "TAX", false, DataSourceUpdateMode.Never));
            teTotalPrice.DataBindings.Add(new Binding("Text", _bsAdjustmentReceipt, "TOTAL_PRICE", false, DataSourceUpdateMode.Never));

            rgAdjustmentState.EditValueChanging -= rgAdjustmentState_EditValueChanging;
            rgAdjustmentState.DataBindings.Add(new Binding("EditValue", _bsAdjustmentReceipt, "ADJUSTMENT_STATE", false, DataSourceUpdateMode.OnPropertyChanged));
            rgAdjustmentState.EditValueChanging += rgAdjustmentState_EditValueChanging;

            setAdjustmentReadonly();

            _isLoad = false;
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



            DataTable dtWarehousingState = Util.getCodeList("CD1502", "KEY", "VALUE");
            RadioGroupItem[] rgProcessState = new RadioGroupItem[dtWarehousingState.Rows.Count];
            int indexProcess = 0;

            for (int i = 0; i < dtWarehousingState.Rows.Count; i++)
            {
                RadioGroupItem rgItem = new RadioGroupItem(dtWarehousingState.Rows[i]["KEY"], ConvertUtil.ToString(dtWarehousingState.Rows[i]["VALUE"]), true, dtWarehousingState.Rows[i]["KEY"]);
                rgProcessState[indexProcess++] = rgItem;

                _dicAdjustmentState.Add(ConvertUtil.ToString(dtWarehousingState.Rows[i]["KEY"]), ConvertUtil.ToString(dtWarehousingState.Rows[i]["VALUE"]));
            }

            this.rgAdjustmentState.Properties.Items.AddRange(rgProcessState);

            Util.LookupEditHelper(rileAdjustmentState, dtWarehousingState, "KEY", "VALUE");
            Util.insertRowonTop(dtWarehousingState, "-1", "전체");
            Util.LookupEditHelper(leAdjustmentState, dtWarehousingState, "KEY", "VALUE");



            var today = DateTime.Today;
           var pastDate = today.AddDays(-365);

           deDtFrom.EditValue = pastDate;
           deDtTo.EditValue = today;
           leAdjustmentState.ItemIndex = 0;
        }

        private void setIInitData()
        {
            gcAdjustmentList.DataSource = null;
            _bsAdjustmentReceipt.DataSource = _dtAdjustment;
            gcAdjustmentList.DataSource = _bsAdjustmentReceipt;

            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;
        }

        private void setAdjustmentReadonly()
        {
            if (_listAllowState.Contains(_adjustmentState))
            {
                lcgBarcodeList.CustomHeaderButtons[2].Properties.Enabled = true;
            }
            else
            {
                lcgBarcodeList.CustomHeaderButtons[2].Properties.Enabled = false;
            }
        }

        private void gvAdjustmentList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvAdjustmentList.RowCount > 0);

            if(!_isLoad)
                Dangol.ShowSplash();

            gvList.BeginDataUpdate();
            _dt.Clear();
            _listInventoryId.Clear();
            _listRepresentativeId.Clear();

            rgAdjustmentState.EditValueChanging -= rgAdjustmentState_EditValueChanging;

            if (isValidRow)
            {
                _currentAdjustment = gvAdjustmentList.GetRow(e.FocusedRowHandle) as DataRowView;
                _adjustmentId = ConvertUtil.ToInt32(_currentAdjustment["ADJUSTMENT_ID"]);
                _adjustmentState = ConvertUtil.ToString(_currentAdjustment["ADJUSTMENT_STATE"]);

                geList();
            }
            else
            {
                _currentAdjustment = null;
            }

            rgAdjustmentState.EditValueChanging += rgAdjustmentState_EditValueChanging;
            gvList.EndDataUpdate();

            setAdjustmentReadonly();

            if (!_isLoad)
                Dangol.CloseSplash();
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

        private bool getAdjustmentReceiptList(ref JObject jResult)
        {
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            gvAdjustmentList.BeginDataUpdate();
            _dtAdjustment.Clear();

            if (DBAdjustment.getAdjustmentReceiptList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtAdjustment.NewRow();

                        string adjustmentState = ConvertUtil.ToString(obj["ADJUSTMENT_STATE"]);

                        dr["ADJUSTMENT_ID"] = obj["ADJUSTMENT_ID"];
                        dr["ADJUSTMENT"] = obj["ADJUSTMENT"];
                        dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                        dr["ADJUSTMENT_STATE"] = obj["ADJUSTMENT_STATE"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];

                        dr["PRODUCT_PRICE"] = obj["PRODUCT_PRICE"];
                        dr["ADJUSTMENT_PRICE"] = obj["ADJUSTMENT_PRICE"];
                        dr["PURCHASED_PRICE"] = obj["PURCHASED_PRICE"];
                        dr["TAX"] = obj["TAX"];
                        //if (adjustmentState.Equals("4") || adjustmentState.Equals("5"))
                        //{
                        //    dr["PRODUCT_CNT"] = 0;
                        //    dr["TOTAL_PRICE"] = 0;
                        //}
                        //else
                        {
                            dr["PRODUCT_CNT"] = obj["PRODUCT_CNT"];
                            dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                        }

                        dr["PRODUCT_CNT_O"] = obj["PRODUCT_CNT"];
                        dr["TOTAL_PRICE_O"] = obj["TOTAL_PRICE"];
                        _dtAdjustment.Rows.Add(dr);
                    }
                }
                gvAdjustmentList.EndDataUpdate();
                return true;
            }
            else
            {
                gvAdjustmentList.EndDataUpdate();
                return false;
            }
        }

        private void setCheckInfo(long inventoryId, ref DataRow dr)
        {

            DataRow[] rowsExamCheck = _dtExamine.Select($"INVENTORY_ID = {inventoryId}");
            DataRow[] rowsRepairCheck = _dtRepair.Select($"INVENTORY_ID = {inventoryId}");

            if (rowsExamCheck.Length < 1 && rowsRepairCheck.Length < 1)
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
                                checkList = ExamineInfo._NTBCHECK2ND["CASE"];
                                for (int i = 0; i < checkList.Count; i++)
                                {
                                    if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                        content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[1]},";
                                    else
                                        content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[0]},";
                                }
                            }
                            else
                            {
                                checkList = ExamineInfo._NTBCHECK2ND[col];
                                for (int i = 0; i < checkList.Count; i++)
                                {
                                    if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                        content += $"{ExamineInfo._NTBCHECK2ND[col][i]},";
                                }
                            }

                            dr[col] = content.Substring(0, content.Length - 1);
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
                                checkList = ExamineInfo._NTBCHECK2ND["CASE"];
                                for (int i = 0; i < checkList.Count; i++)
                                {
                                    if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                        content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[1]},";
                                    else
                                        content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[0]},";
                                }
                            }
                            else
                            {
                                checkList = ExamineInfo._NTBCHECK2ND[col];
                                for (int i = 0; i < checkList.Count; i++)
                                {
                                    if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                        content += $"{ExamineInfo._NTBCHECK2ND[col][i]},";
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
            long warehousingId;
            foreach (JObject obj in jArray.Children<JObject>())
            {
                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                approvalType = ConvertUtil.ToString(obj["APPROVAL_TYPE"]);
                repairUserId = ConvertUtil.ToString(obj["REPAIR_USER_ID"]);
                warehousingId = ConvertUtil.ToInt64(obj["WAREHOUSING_ID"]);
                manufactureType = ConvertUtil.ToInt32(obj["MANUFACTURE_TYPE"]);

                DataRow dr = _dt.NewRow();

                dr["INVENTORY_ID"] = inventoryId;
                dr["CHECK"] = false;
                dr["ADJUSTMENT_STATE"] = obj["ADJUSTMENT_STATE"];
                if (approvalType.Equals("G"))
                    dr["APPROVAL_USER_ID"] = obj["APPROVAL_USER1"];
                else if (approvalType.Equals("S") || approvalType.Equals("M"))
                    dr["APPROVAL_USER_ID"] = obj["APPROVAL_USER2"];
                else
                    dr["APPROVAL_USER_ID"] = "-1";
                if(!string.IsNullOrEmpty(repairUserId))
                    dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["REPAIR_DT"]);
                else
                    dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                dr["COMPANY_ID"] = obj["COMPANY_ID"];
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

                _listInventoryId.Add(inventoryId);
                if (!_listRepresentativeId.Contains(warehousingId))
                {
                    _listRepresentativeId.Add(warehousingId);
                }
            }
        }
        private bool geList()
        {
            JObject jResult = new JObject();

            JObject jData = new JObject();

            _dt.Clear();
            _dtExamine.Clear();
            _dtRepair.Clear();
            _dtRepairCheck.Clear();
            _dtExaminePrice.Clear();
            _dtRepairPrice.Clear();
            jData.Add("PRODUCT_TYPE", 2); //노트북
            jData.Add("ADJUSTMENT_ID", _adjustmentId);

            if (DBAdjustment.getProductListByAdjustmentId(jData, ref jResult))
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

            if (diffDay > 730)
            {
                jData.Add("MSG", "검색 기간은 730일(2년)을 초과할 수 없습니다.");
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

            if (ConvertUtil.ToInt32(leAdjustmentState.EditValue) >= 0)
                jData.Add("ADJUSTMENT_STATE", ConvertUtil.ToString(leAdjustmentState.EditValue));

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
                        //jdata.Add("INIT_PRICE", ConvertUtil.ToString(row["INIT_PRICE"]));
                        //jdata.Add("ADJUST_PRICE", ConvertUtil.ToString(row["ADJUST_PRICE"]));
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

                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_TYPE", ProjectInfo._userType);

                    if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    {
                        jobj.Add("ADJUSTMENT_STATE", 1);
                        jobj.Add("APPROVAL_USER2", ProjectInfo._userId);
                    }
                    else
                    {                    
                        jobj.Add("APPROVAL_USER1", ProjectInfo._userId);
                    }
                    List<long> listInventoryId = new List<long>();
                    foreach (DataRow row in rows)
                    {
                        listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            foreach (DataRow row in rows)
                            {
                                row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                row["ADJUSTMENT_STATE"] = 1;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in rows)
                                row["APPROVAL_USER_ID"] = ProjectInfo._userId;
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
                using (usrAdjustmentStatistics Adjustment = new usrAdjustmentStatistics(_adjustmentId, _adjustmentState, _listRepresentativeId, _listInventoryId))
                {
                    if (Adjustment.ShowDialog(this) == DialogResult.OK)
                    {

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
            int rowhandle = gvAdjustmentList.FocusedRowHandle;
            gvAdjustmentList.FocusedRowHandle = -1;
            gvAdjustmentList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtAdjustment.Select("CHECK = TRUE");


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

            JObject jresult = new JObject();
            geList();
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

        private void sbSearch_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();
            getAdjustmentReceiptList(ref jResult);
        }

        private void rgAdjustmentState_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int state = ConvertUtil.ToInt32(e.NewValue);

            if (Dangol.MessageYN($"현재 정산을 '{_dicAdjustmentState[e.NewValue.ToString()]}'처리하시겠습니까?") == DialogResult.Yes)
            {
                int receiptStatus = ConvertUtil.ToInt32(e.NewValue);

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("ADJUSTMENT_ID", _adjustmentId);
                jobj.Add("ADJUSTMENT_STATE", receiptStatus);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", _listInventoryId));

                if (DBAdjustment.updateWarehousingAdjustmentState(jobj, ref jResult))
                {
                    _currentAdjustment.BeginEdit();
                    _currentAdjustment["ADJUSTMENT_STATE"] = receiptStatus.ToString();
                    _currentAdjustment.EndEdit();

                    _adjustmentState = receiptStatus.ToString();

                    setAdjustmentReadonly();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
    }
}