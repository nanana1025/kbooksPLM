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
using System.IO;
using WareHousingMaster.UtilTest;
using ScreenCopy;

namespace WareHousingMaster.view.produce
{
    public partial class usrProduceProductCheckList : DevExpress.XtraEditors.XtraForm
    {
        int _viewCategory = (int)Enum.EnumView.produce;
        int _viewType = (int)Enum.EnumProduce.ProductCheckList;

        DataTable _dtProduce;
        DataTable _dt;
        DataTable _dtQCCheck;

        DataTable _dtCheck;
        DataTable _dtQC;

        DataTable _dtCheckPrice;
        DataTable _dtQCPrice;

        BindingSource _bsProduce;
        BindingSource _bs;

        DataRowView _currentRow = null;
  
        List<long> _listProduceId;

        List<string> _listVisibleCol;
        List<string> _listHideCol;

        List<string> _listMasterCol;
        List<string> _listReadOnlyCol;

        List<string> _lisDefaultHideCol;

        long _companyId;
        string _barcode;
        string _representativeNo;
        long _inventoryId;



        public usrProduceProductCheckList()
        {
            InitializeComponent();

            _dtProduce = new DataTable();
            _dtProduce.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtProduce.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dtProduce.Columns.Add(new DataColumn("RELEASES", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("RELEASE_STATE", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("PRODUCT_CNT", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("PROCESS_CNT", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("COMPLETE_CNT", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("REMAIN_CNT", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("QC_PASS_CNT", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("QC_FAIL_CNT", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("DES", typeof(string)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("PRODUCE_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_USER1", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_DT1", typeof(string)));
            //_dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("QC_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("QC_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("LOCATION", typeof(long)));
            _dt.Columns.Add(new DataColumn("PALLET", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("RELEASES", typeof(string)));
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

            _dt.Columns.Add(new DataColumn("PASSWORD_QC_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("DISPLAY_QC_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("KEYBOARD_QC_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("USB_QC_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("CASE_QC_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("WIRELESS_QC_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("BATTERY_QC_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("ETC_QC_CNT", typeof(int)));

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

            _dt.Columns.Add(new DataColumn("CASE_DESTROYED_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_SCRATCH_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_STABBED_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_PRESSED_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DISCOLORED_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_HINGE_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DES_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("COOLER_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("DISPLAY_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("USB_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("MOUSEPAD_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("KEYBOARD_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("BATTERY_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("CAM_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("LAN_WIRELESS_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("LAN_WIRED_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("ODD_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("HDD_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("BIOS_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("OS_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("TEST_CHECK_QC", typeof(string)));
            _dt.Columns.Add(new DataColumn("STATE_QC", typeof(int)));

            _dt.Columns.Add(new DataColumn("CAPTURE_YN", typeof(int)));


            _dtCheck = new DataTable();
            _dtCheck.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtCheck.Columns.Add(new DataColumn("CHECK_TYPE", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtCheck.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("UPDATE_USER_ID", typeof(string)));
            
            _dtQC = new DataTable();
            _dtQC.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtQC.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtQC.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("UPDATE_USER_ID", typeof(string)));
            


            _dtCheckPrice = new DataTable();
            _dtCheckPrice.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtCheckPrice.Columns.Add(new DataColumn("CASES", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("DISPLAY", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("BATTERY", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("MOUSEPAD", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("KEYBOARD", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("CAM", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("USB", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("LAN_WIRED", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("HDD", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("ODD", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("ADAPTER", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("BIOS", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("OS", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("TEST_CHECK", typeof(long)));
            _dtCheckPrice.Columns.Add(new DataColumn("ETC", typeof(long)));

            _dtQCPrice = new DataTable();
            _dtQCPrice.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtQCPrice.Columns.Add(new DataColumn("CASES", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("DISPLAY", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("BATTERY", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("MOUSEPAD", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("KEYBOARD", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("CAM", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("USB", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("LAN_WIRED", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("HDD", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("ODD", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("ADAPTER", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("BIOS", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("OS", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("TEST_CHECK", typeof(long)));
            _dtQCPrice.Columns.Add(new DataColumn("ETC", typeof(long)));


            _dtQCCheck = new DataTable(); //[0]그대로:black, [1]없다가 생김:red, [2]있던게 변경:blue
            _dtQCCheck.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtQCCheck.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtQCCheck.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtQCCheck.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            

            _bsProduce = new BindingSource();
            _bs = new BindingSource();

            //_bsProduce.DataSource = _dtProduce;
            //_bs.DataSource = _dt;

            _listProduceId = new List<long>();
            _listVisibleCol = new List<string>();
            _listHideCol = new List<string>();
            _listMasterCol = new List<string>(new[] { "PRODUCT_PRICE", "PRODUCT_ADJUST_PRICE","PASSWORD_CNT","DISPLAY_CNT","KEYBOARD_CNT","USB_CNT","CASE_CNT","WIRELESS_CNT","BATTERY_CNT","ETC_CNT"});
            _listReadOnlyCol = new List<string>(new[] { "BARCODE"});
            _lisDefaultHideCol = new List<string>(new[] { "INVENTORY_ID", "CATEGORY", "MON_SIZE", "STATE" });
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            //getVisibleCol();
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

            DataTable dtCheckType = Util.getCodeList("CD1701", "KEY", "VALUE");
            Util.LookupEditHelper(rileCheckType, dtCheckType, "KEY", "VALUE");


            DataTable dtProduceState = Util.getCodeList("CD0801", "KEY", "VALUE");
            Util.LookupEditHelper(rileProduceState, dtProduceState, "KEY", "VALUE");


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

            DataTable dtProductProduceState = Util.getCodeList("CD0804", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductProduceState, dtProductProduceState, "KEY", "VALUE");
            


            var today = DateTime.Today;
           var pastDate = today.AddDays(-30);

           deDtFrom.EditValue = pastDate;
           deDtTo.EditValue = today;
           leProductGrade.ItemIndex = 0;

        }

        private void setIInitData()
        {
            gcProduceList.DataSource = null;
            _bsProduce.DataSource = _dtProduce;
            gcProduceList.DataSource = _bsProduce;

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

            if(ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId))
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
            gvProduceList.BeginUpdate();
            foreach (GridColumn gridcol in gvList.Columns)
            {
                if (_listHideCol.Contains(gridcol.FieldName) || _lisDefaultHideCol.Contains(gridcol.FieldName))
                    gridcol.Visible = false;
                else
                    gridcol.Visible = true;
            }

            if (ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId))
            {
                gcInitPrice.Visible = true;
                gcAdjust.Visible = true;

                gcComplete.Visible = true;
                gcProcess.Visible = true;
                gcRemain.Visible = true;

                gcPasswordCnt.Visible = true;
                gcDisplayCnt.Visible = true;
                gcKeyboardCnt.Visible = true;
                gcUsbCnt.Visible = true;
                gcCaseCnt.Visible = true;
                gcWirelessCnt.Visible = true;
                gcBatteryCnt.Visible = true;
                gcEtcCnt.Visible = true;

                lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = true;
                //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = true;
                lcgBarcodeList.CustomHeaderButtons[4].Properties.Visible = true;
            }
            else
            {
                gcInitPrice.Visible = false;
                gcAdjust.Visible = false;

                gcComplete.Visible = false;
                gcProcess.Visible = false;
                gcRemain.Visible = false;

                gcPasswordCnt.Visible = false;
                gcDisplayCnt.Visible = false;
                gcKeyboardCnt.Visible = false;
                gcUsbCnt.Visible = false;
                gcCaseCnt.Visible = false;
                gcWirelessCnt.Visible = false;
                gcBatteryCnt.Visible = false;
                gcEtcCnt.Visible = false;

                lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
                //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
                lcgBarcodeList.CustomHeaderButtons[4].Properties.Visible = false;
            }

            gvProduceList.EndUpdate();
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
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);
                _representativeNo = ConvertUtil.ToString(_currentRow["RELEASES"]);
            }
            else
            {
                _currentRow = null;
                _barcode = "";
                _inventoryId = -1;
                _representativeNo = "";
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);
                _representativeNo = ConvertUtil.ToString(_currentRow["RELEASES"]);
            }
            else
            {
                _currentRow = null;
                _barcode = "";
                _inventoryId = -1;
                _representativeNo = "";
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

            _dtProduce.Clear();

            if (DBProductProduce.getProduceList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtProduce.NewRow();

                        dr["NO"] = index++;
                        dr["CHECK"] = false;
                        dr["RELEASE_ID"] = obj["RELEASE_ID"];
                        dr["RELEASES"] = obj["RELEASES"];
                        dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                        dr["RELEASE_STATE"] = obj["RELEASE_STATE"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["PRODUCT_CNT"] = obj["PRODUCT_CNT"];
                        dr["COMPLETE_CNT"] = obj["COMPLETE_CNT"];
                        dr["REMAIN_CNT"] = obj["REMAIN_CNT"];
                        dr["PROCESS_CNT"] = obj["PROCESS_CNT"];
                        dr["QC_PASS_CNT"] = obj["QC_PASS_CNT"];
                        dr["QC_FAIL_CNT"] = obj["QC_FAIL_CNT"];
                        dr["DES"] = obj["DES"];

                        _dtProduce.Rows.Add(dr);
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

            DataRow[] rowsCheck = _dtCheck.Select($"INVENTORY_ID = {inventoryId}");
            DataRow[] rowsQCCheck = _dtQC.Select($"INVENTORY_ID = {inventoryId}");

            if (rowsCheck.Length > 0)
            {
                dr["CHECK_TYPE"] = ConvertUtil.ToInt32(rowsCheck[0]["CHECK_TYPE"]);
                dr["CHECK_DT"] = ConvertUtil.ToDateTimeNull(rowsCheck[0]["UPDATE_DT"]);
                dr["CHECK_USER_ID"] = ConvertUtil.ToString(rowsCheck[0]["UPDATE_USER_ID"]);
            }

            if (rowsQCCheck.Length > 0)
            {
                dr["QC_DT"] = ConvertUtil.ToDateTimeNull(rowsQCCheck[0]["UPDATE_DT"]);
                dr["QC_USER_ID"] = ConvertUtil.ToString(rowsQCCheck[0]["UPDATE_USER_ID"]);
            }


            if (rowsCheck.Length < 1)
            {
                dr["CHECK_YN"] = false;
                dr["CHECK_TYPE"] = 0;
                return;
            }
            else if (rowsCheck.Length < 1 && rowsQCCheck.Length < 1)
            {
                dr["CHECK_YN"] = false;
                dr["CHECK_TYPE"] = 0;
                return;
            }
            else
            {
                dr["CHECK_YN"] = true;
                if (rowsQCCheck.Length > 0)
                {
                    DataRow rowCheck = rowsCheck[0];
                    DataRow rowQCCheck = rowsQCCheck[0];

                    DataRow drQCCheck = _dtQCCheck.NewRow();
                    drQCCheck["INVENTORY_ID"] = inventoryId;

                    dr["PRODUCT_GRADE"] = rowQCCheck["PRODUCT_GRADE"];
                    dr["CASE_DES"] = rowQCCheck["CASE_DES"];
                    dr["CASE_HINGE"] = rowQCCheck["CASE_HINGE"];
                    dr["CHECK_TYPE"] = 2;

                    if (rowCheck["CASE_HINGE"] == rowQCCheck["CASE_HINGE"])
                        drQCCheck["CASE_HINGE"] = 0;
                    //else if(ConvertUtil.ToInt32(rowCheck["CASE_HINGE"]) < ConvertUtil.ToInt32(rowQCCheck["CASE_HINGE"]))
                    //    drQCCheck["CASE_HINGE"] = 1;
                    else
                        drQCCheck["CASE_HINGE"] = 2;

                    int checkValue = 0;
                    List<string> checkList;
                    string content;

                    foreach (string col in ExamineInfo._NTBCOLNAME2ND)
                    {
                        content = "";
                        checkValue = ConvertUtil.ToInt32(rowQCCheck[col]);

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

                        if (rowCheck[col] == rowQCCheck[col])
                            drQCCheck[col] = 0;
                        //else if (ConvertUtil.ToInt32(rowCheck[col]) < ConvertUtil.ToInt32(rowQCCheck[col]))
                        //    drQCCheck[col] = 1;
                        else
                            drQCCheck[col] = 2;
                    }

                    checkValue = ConvertUtil.ToInt32(rowCheck["BATTERY"]);
                    if ((checkValue & ExamineInfo._BASE[4]) == ExamineInfo._BASE[4])
                    {
                        string batteryRemain = ConvertUtil.ToString(rowCheck["BATTERY_REMAIN"]);
                        dr["BATTERY"] += $"{dr["BATTERY"]}[{ batteryRemain}%]";
                    }

                    _dtQCCheck.Rows.Add(drQCCheck);
                }
                else
                {
                    DataRow rowCheck = rowsCheck[0];
                    dr["PRODUCT_GRADE"] = rowCheck["PRODUCT_GRADE"];
                    dr["CASE_DES"] = rowCheck["CASE_DES"];
                    dr["CASE_HINGE"] = rowCheck["CASE_HINGE"];
                    //dr["CHECK_TYPE"] = 1;

                    int checkValue = 0;
                    List<string> checkList;
                    string content;

                    foreach (string col in ExamineInfo._NTBCOLNAME2ND)
                    {
                        content = "";
                        checkValue = ConvertUtil.ToInt32(rowCheck[col]);

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

                    checkValue = ConvertUtil.ToInt32(rowCheck["BATTERY"]);
                    if ((checkValue & ExamineInfo._BASE[4]) == ExamineInfo._BASE[4])
                        dr["BATTERY"] += $"{dr["BATTERY"]}[{ rowCheck["BATTERY_REMAIN"]}%]";
                }
            }
        }

        private void setCheckPriceInfo(long inventoryId, ref DataRow dr)
        {

            DataRow[] rowsCheck = _dtCheckPrice.Select($"INVENTORY_ID = {inventoryId}");
            DataRow[] rowsQCCheck = _dtQCPrice.Select($"INVENTORY_ID = {inventoryId}");

            if (rowsCheck.Length < 1 && rowsQCCheck.Length < 1)
            {
                foreach (KeyValuePair<string, string> item in ExamineInfo._dicListAdjustmentPriceColPair)
                     dr[item.Value] = 0;

                dr["PRODUCT_ADJUST_PRICE"] = 0;
                return;
            }
            else if (rowsQCCheck.Length > 0)
            {              
                DataRow rowQCCheck = rowsQCCheck[0];

                long price = 0;
                long partPrice = 0;
                foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                {
                    partPrice = ConvertUtil.ToInt64(rowQCCheck[col]);

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
                DataRow rowCheck = rowsCheck[0];

                long price = 0;
                long partPrice = 0;
                foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                {
                    partPrice = ConvertUtil.ToInt64(rowCheck[col]);
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
            //string repairUserId;
            int manufactureType;
            foreach (JObject obj in jArray.Children<JObject>())
            {
                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                approvalType = ConvertUtil.ToString(obj["APPROVAL_PRODUCE_TYPE"]);
                //repairUserId = ConvertUtil.ToString(obj["REPAIR_USER_ID"]);
                manufactureType = ConvertUtil.ToInt32(obj["MANUFACTURE_TYPE"]);
                
                DataRow dr = _dt.NewRow();

                dr["INVENTORY_ID"] = inventoryId;
                dr["CHECK"] = false;

                dr["APPROVAL_TYPE"] = obj["APPROVAL_PRODUCE_TYPE"];
                dr["PRODUCE_STATE"] = obj["PRODUCE_STATE"];
                if (approvalType.Equals("G") || approvalType.Equals("U"))
                {
                    dr["APPROVAL_USER_ID"] = obj["APPROVAL_PRODUCE_USER1"];
                    dr["APPROVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["APPROVAL_PRODUCE_DT1"]);
                }
                else if (approvalType.Equals("S") || approvalType.Equals("M"))
                {
                    dr["APPROVAL_USER_ID"] = obj["APPROVAL_PRODUCE_USER2"];
                    dr["APPROVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["APPROVAL_PRODUCE_DT2"]);
                }
                else
                {
                    dr["APPROVAL_USER_ID"] = "-1";
                    dr["APPROVAL_DT"] = "";
                }
                
                dr["APPROVAL_USER1"] = ConvertUtil.ToString(obj["APPROVAL_PRODUCE_USER1"]);
                dr["APPROVAL_DT1"] = ConvertUtil.ToDateTimeNull(obj["APPROVAL_PRODUCE_DT1"]);
                //dr["QC_DT"] = ConvertUtil.ToDateTimeNull(obj["REPAIR_DT"]);
                //dr["CHECK_DT"] = ConvertUtil.ToDateTimeNull(obj["CHECK_DT"]);
                dr["LOCATION"] = obj["LOCATION"];
                dr["PALLET"] = obj["PALLET"];
                dr["COMPANY_ID"] = ConvertUtil.ToInt64(obj["COMPANY_ID"]);
                dr["RELEASE_ID"] = obj["RELEASE_ID"];
                dr["RELEASES"] = obj["RELEASES"];
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
                dr["CAPTURE_YN"] = obj["CAPTURE_YN"];
                
                //dr["NTB_NICKNAME"] = obj["NTB_NICKNAME"];
                //dr["MON_SIZE"] = obj["MON_SIZE"];
                //dr["CHECK_YN"] = obj["CHECK_YN"];
                //dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];

                setCheckPriceInfo(inventoryId, ref dr);
                setCheckInfo(inventoryId, ref dr);
                
                _dt.Rows.Add(dr);
            }
        }
        private bool geList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dt.Clear();
            _dtCheck.Clear();
            _dtQC.Clear();
            _dtQCCheck.Clear();
            _dtCheckPrice.Clear();
            _dtQCPrice.Clear();

            jData.Add("PRODUCT_TYPE", 2); //노트북
            jData.Add("PRODUCE_STATE", "'1', '2', '3'");
            jData.Add("TN_TABLE_PART", "TN_RELEASE_PART");
            jData.Add("TN_TABLE", "TN_RELEASE");
            jData.Add("REPRESENTATIVE_ID", "RELEASE_ID");
            jData.Add("REPRESENTATIVE_KEY", "RELEASES");
            jData.Add("TN_BOM", "TN_RELEASE_BOM_TREE");
            jData.Add("LIST_REPRESENTATIVE_ID", string.Join(",", _listProduceId));

            if (DBProductProduce.getProductList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["CHECKDATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["CHECKDATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                        DataRow dr = _dtCheck.NewRow();

                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        //dr["CHECK_YN"] = checkYn;
                        //if (checkYn)
                        {
                            dr["CHECK_TYPE"] = ConvertUtil.ToInt32(obj["CHECK_TYPE"]); 
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
                        _dtCheck.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["QCDATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["QCDATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                        DataRow dr = _dtQC.NewRow();

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
                        _dtQC.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["CHECKPRICE_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["CHECKPRICEDATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                        DataRow dr = _dtCheckPrice.NewRow();

                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        //dr["CHECK_YN"] = checkYn;
                        //if (checkYn)
                        {
                            foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                                dr[col] = obj[col];
                        }
                        _dtCheckPrice.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["QCPRICE_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["QCPRICEDATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                        DataRow dr = _dtQCPrice.NewRow();

                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        //dr["CHECK_YN"] = checkYn;
                        //if (checkYn)
                        {
                            foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                                dr[col] = obj[col];
                        }
                        _dtQCPrice.Rows.Add(dr);
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
                gvList.FocusedRowHandle = -2147483646;
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
                        Dangol.closeSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.closeSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
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
                    jobj.Add("APPROVAL_PRODUCE_TYPE", ProjectInfo._userType);

                    if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                        isAdmin = true;

                    if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    {
                        jobj.Add("PRODUCE_STATE", 3);
                        jobj.Add("APPROVAL_PRODUCE_USER2", ProjectInfo._userId);
                        jobj.Add("APPROVAL_PRODUCE_DT2", now);
                    }
                    else
                    {                    
                        jobj.Add("APPROVAL_PRODUCE_USER1", ProjectInfo._userId);
                        jobj.Add("APPROVAL_PRODUCE_DT1", now);
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
                                row["PRODUCE_STATE"] = 3;
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
                                        row["APPROVAL_USER1"] = ProjectInfo._userId;
                                        row["APPROVAL_DT1"] = now;

                                    }
                                }
                            }
                        }

                        gvList.EndDataUpdate();
                        Dangol.closeSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.closeSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(9))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                string mgs="";
                if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    mgs = "선택하신 제품을 승인취소하시겠습니까?";
                else
                    mgs = "선택하신 제품을 승인취소하시겠습니까?(본인 승인만 취소 가능)";

                if (Dangol.MessageYN(mgs) == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    string userId = "";
                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_PRODUCE_TYPE", "");

                    if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    {
                        jobj.Add("PRODUCE_STATE", 1);
                        jobj.Add("APPROVAL_PRODUCE_USER2", "");
                        jobj.Add("APPROVAL_PRODUCE_DT2", "-1");
                    }
                    else
                    {
                        jobj.Add("APPROVAL_PRODUCE_USER1", "");
                        jobj.Add("APPROVAL_PRODUCE_DT1", "-1");
                    }

                    List<long> listInventoryId = new List<long>();
                    List<long> listApprovalInventoryId = new List<long>();
                    long inventoryId;
                    string approvalUserId;
                    foreach (DataRow row in rows)
                    {
                        inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            listInventoryId.Add(inventoryId);

                            approvalUserId = ConvertUtil.ToString(row["APPROVAL_USER1"]);
                            if (!string.IsNullOrEmpty(approvalUserId))
                                listApprovalInventoryId.Add(inventoryId);
                        }
                        else
                        {
                            userId = ConvertUtil.ToString(row["APPROVAL_USER_ID"]);

                            if (userId.Equals(ProjectInfo._userId))
                                listInventoryId.Add(inventoryId);
                        }
                        
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            jobj.RemoveAll();
                            jobj.Add("BULK_YN", 1);
                            jobj.Add("APPROVAL_PRODUCE_TYPE", "G");
                            jobj.Add("LIST_INVENTORY_ID", string.Join(",", listApprovalInventoryId));

                            DBAdjustment.updateProductInfo(jobj, ref jResult);

                            gvList.BeginDataUpdate();

                            foreach (DataRow row in rows)
                            {
                                inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                                if(listApprovalInventoryId.Contains(inventoryId))
                                {
                                    row["APPROVAL_TYPE"] = "G";
                                    row["APPROVAL_USER_ID"] = row["APPROVAL_USER1"];
                                    row["APPROVAL_DT"] = row["APPROVAL_DT1"];
                                }
                                else
                                {
                                    row["APPROVAL_TYPE"] = "";
                                    row["APPROVAL_USER_ID"] = "";
                                    row["APPROVAL_DT"] = "";
                                }
                            }

                            gvList.EndDataUpdate();

                        }
                        else
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
                                    row["APPROVAL_USER1"] ="";
                                    row["APPROVAL_DT1"] = "";
                                }
                            }

                            gvList.EndDataUpdate();
                        }
                        
                        Dangol.closeSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.closeSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 제품으로 출고를 진행 하시겠습니까(관리자 승인만 진행)?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    var jArray = new JArray();

                    List<long> listInventoryId = new List<long>();
                    List<long> listRepresentativeId = new List<long>();
                    List<int> listCheckType = new List<int>();
                    string produceState;
                    string approvalType = "";
                    string QcUserId;

                    foreach (DataRow row in rows)
                    {
                        produceState = ConvertUtil.ToString(row["PRODUCE_STATE"]);
                        approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);

                        if (produceState.Equals("3") && (approvalType.Equals("M") || approvalType.Equals("S")))
                        {
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));

                            JObject jdata = new JObject();
                            jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));

                            QcUserId = ConvertUtil.ToString(row["QC_USER_ID"]);
                            if (!string.IsNullOrWhiteSpace(QcUserId))
                                jdata.Add("CHECK_TYPE", 4);
                            else
                                jdata.Add("CHECK_TYPE", ConvertUtil.ToInt32(row["CHECK_TYPE"]));

                            jArray.Add(jdata);
                            

                            long representativeId = ConvertUtil.ToInt64(row["RELEASE_ID"]);

                            if (!listRepresentativeId.Contains(representativeId))
                            {
  
                                listRepresentativeId.Add(representativeId);
                            }
                        }
                    }

                    jobj.Add("DATA", jArray);
                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                    jobj.Add("LIST_REPRESENTATIVE_ID", string.Join(",", listRepresentativeId));
                    jobj.Add("PRODUCE_STATE", 3);
                    jobj.Add("COMPANY_ID", 1);
                    jobj.Add("MAJOR_COL", "LT_DEALER_PRICE_MAJOR");
                    jobj.Add("ETC_COL", "LT_DEALER_PRICE_ETC");
                    jobj.Add("MAJOR_ADJUST_COL", "LT_DEALER_PRICE");
                    jobj.Add("ETC_ADJUST_COL", "LT_DEALER_PRICE_ETC");


                    if (DBProductProduce.getAdjustNtbData(jobj, ref jResult))
                    {
                        jResult.Add("COMPANY_ID", _companyId);

                        using (usrProduceProduct produceProduct = new usrProduceProduct(jResult, listInventoryId))
                        {
                            produceProduct.geList();
                            Dangol.closeSplash();
                            if (produceProduct.ShowDialog(this) == DialogResult.OK)
                            {
   
                            }

                            if (produceProduct._isReleaseReceipt)
                                refresh();
                        }

                        //Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.closeSplash();
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
            else if (e.Button.Properties.Tag.Equals(5))
            {
                refresh();
            }
        }

        private void refresh()
        {
            Dangol.ShowSplash();

            
            int rowHandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowHandle;

            string barcode = _barcode;
            List<long> listChecked = new List<long>();

            DataRow[] rows = _dt.Select("CHECK = TRUE");
            long inventoryId;
            foreach(DataRow row in rows)
            {
                inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                if (!listChecked.Contains(inventoryId))
                    listChecked.Add(inventoryId);
            }

            gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged -= gvList_FocusedRowChanged;
            geList();

            if (listChecked.Count > 0)
            {
                gvList.BeginDataUpdate();
                foreach (DataRow row in _dt.Rows)
                {
                    inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                    if (listChecked.Contains(inventoryId))
                    {
                        row.BeginEdit();
                        row["CHECK"] = true;
                        row.EndEdit();
                    }
                }
                gvList.EndDataUpdate();
            }
            gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged += gvList_FocusedRowChanged;

            rowHandle = gvList.LocateByValue("BARCODE", barcode);

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

            Dangol.closeSplash();
        }

        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -2147483646;
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
            gvList.FocusedRowHandle = -2147483646;
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
            int rowhandle = gvProduceList.FocusedRowHandle;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtProduce.Select("CHECK = TRUE");


            if(rows.Length < 1)
            {
                Dangol.Message("생산번호를 선택하세요");
                return;
            }

            if (Dangol.MessageYN("선택하신 생산번호의 제품 정보를 확인하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            _listProduceId.Clear();

            foreach (DataRow row in rows)
            {
                _listProduceId.Add(ConvertUtil.ToInt64(row["RELEASE_ID"]));
                _companyId = ConvertUtil.ToInt64(row["COMPANY_ID"]);
            }

            Dangol.ShowSplash();
          
            geList();

            Dangol.closeSplash();
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

            if (!ProjectInfo._userType.Equals("M") && !ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId))
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
            refresh();
        }

        private void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduceList.FocusedRowHandle;
            int topRowIndex = gvProduceList.TopRowIndex;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            try
            {
                gvProduceList.BeginUpdate();
                foreach (DataRow row in _dtProduce.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvProduceList.DataRowCount; i++)
                {
                    int rowHandle = gvProduceList.GetVisibleRowHandle(i);
                    rows.Add(gvProduceList.GetDataRow(rowHandle));
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
                gvProduceList.EndUpdate();
            }
        }

        private void layoutControlGroup1_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduceList.FocusedRowHandle;
            int topRowIndex = gvProduceList.TopRowIndex;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            gvProduceList.BeginDataUpdate();

            foreach (DataRow row in _dtProduce.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvProduceList.EndDataUpdate();
        }

        private void gcList_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F5)
            {
                refresh();
            }
        }

        private void gvList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "CAPTURE_YN")
            {
                if (_currentRow == null)
                    Dangol.Warining("선택된 제품이 없습니다.");
                else
                {
                    if (ConvertUtil.ToInt32(_currentRow["CAPTURE_YN"]) == 1)
                    {
                        Image image = ScreenCapture.GetCaptureImg(_representativeNo, $"{_inventoryId}.png");

                        using (DlgImgTest digImgTest = new DlgImgTest(image))
                        {
                            digImgTest.ShowDialog(this);
                            File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_inventoryId}.png");
                        }
                    }
                    else
                        Dangol.Message("캡쳐한 이미지가 없습니다.");
                }
            }
        }
    }
}