using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using ScreenCopy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Utils;
using WareHousingMaster.UtilTest;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.release.External
{
    public partial class usrReleaseList : DevExpress.XtraEditors.XtraForm
    {
        int _viewCategory = (int)Enum.EnumView.Adjustment;
        int _viewType = (int)Enum.EnumTableAdjustment.ExamList;

        DataTable _dtExport;
        DataTable _dt;
        DataTable _dtProductDetail;

        BindingSource _bsExport;
        BindingSource _bs;

        DataRowView _currentRow = null;
        DataRowView _currentExportRow = null;
        
  
        List<long> _listExportId;

        List<string> _listVisibleCol;
        List<string> _listHideCol;

        List<string> _listMasterCol;
        List<string> _listReadOnlyCol;

        List<string> _lisDefaultHideCol;

        Dictionary<string, string> _dicMonType;

        long _companyId;
        string _barcode;
        string _export;

        Regex regex1;
        Regex regex2;
        Regex regex3;


        public usrReleaseList()
        {
            InitializeComponent();

            _dtExport = new DataTable();
            _dtExport.Columns.Add(new DataColumn("NO", typeof(long)));
            _dtExport.Columns.Add(new DataColumn("EXPORT_ID", typeof(long)));
            _dtExport.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dtExport.Columns.Add(new DataColumn("EXPORT", typeof(string)));
            _dtExport.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dtExport.Columns.Add(new DataColumn("ARRIVAL_DT", typeof(string)));
            _dtExport.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtExport.Columns.Add(new DataColumn("RELEASE_CNT", typeof(int)));
            _dtExport.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtExport.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(long)));
            _dt.Columns.Add(new DataColumn("PRODUCE", typeof(string)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            //_dt.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("EXPORT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("EXPORT", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("SA", typeof(int)));
            _dt.Columns.Add(new DataColumn("SACNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("ARRIVAL_DT", typeof(object)));
            _dt.Columns.Add(new DataColumn("CAPTURE_YN", typeof(int)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));

            _dt.Columns.Add(new DataColumn("MANUFACTURER", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));

            _dt.Columns.Add(new DataColumn("COL1", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL2", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL3", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL4", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL5", typeof(string)));

            _dt.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dt.Columns.Add(new DataColumn("CPU_MODEL_NAME", typeof(string)));

            _dt.Columns.Add(new DataColumn("MEM_MANUFACT1", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_CAPACITY1", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_MANUFACT2", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_CAPACITY2", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_MANUFACT3", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_CAPACITY3", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_MANUFACT4", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_CAPACITY4", typeof(string)));

            _dt.Columns.Add(new DataColumn("VGA_MODEL_NAME1", typeof(string)));
            _dt.Columns.Add(new DataColumn("VGA_MODEL_NAME2", typeof(string)));
            _dt.Columns.Add(new DataColumn("STG_TYPE1", typeof(string)));
            _dt.Columns.Add(new DataColumn("STG_CAPACITY1", typeof(string)));
            _dt.Columns.Add(new DataColumn("STG_TYPE2", typeof(string)));
            _dt.Columns.Add(new DataColumn("STG_CAPACITY2", typeof(string)));
            _dt.Columns.Add(new DataColumn("MON_SIZE", typeof(string)));

            _dt.Columns.Add(new DataColumn("ODD", typeof(string)));
            _dt.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dt.Columns.Add(new DataColumn("WIFI", typeof(int)));
            _dt.Columns.Add(new DataColumn("BATTERY", typeof(string)));
            _dt.Columns.Add(new DataColumn("OS_NAME", typeof(string)));
            _dt.Columns.Add(new DataColumn("REMARKS1", typeof(string)));
            _dt.Columns.Add(new DataColumn("REMARKS2", typeof(string)));

            _dt.Columns.Add(new DataColumn("LICENCE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CAPTURE_FILE_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            



            _dtProductDetail = new DataTable();
            _dtProductDetail.Columns.Add(new DataColumn("MBD_MANUFACT", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MBD_MODEL_NAME", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("CPU_MODEL_NAME", typeof(string)));

            _dtProductDetail.Columns.Add(new DataColumn("MEM_MANUFACT1", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MEM_CAPACITY1", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MEM_MANUFACT2", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MEM_CAPACITY2", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MEM_MANUFACT3", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MEM_CAPACITY3", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MEM_MANUFACT4", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MEM_CAPACITY4", typeof(string)));

            _dtProductDetail.Columns.Add(new DataColumn("VGA_MODEL_NAME1", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("VGA_MODEL_NAME2", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("STG_TYPE1", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("STG_CAPACITY1", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("STG_TYPE2", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("STG_CAPACITY2", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("MON_SIZE", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtProductDetail.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtProductDetail.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            _dtProductDetail.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtProductDetail.Columns.Add(new DataColumn("UPDATE_USER_ID", typeof(string)));

            _bsExport = new BindingSource();
            _bs = new BindingSource();

            _dicMonType = new Dictionary<string, string>();

            _listExportId = new List<long>();

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            getVCols();
            //setGridViewColumsWarehousing();
            //setGridViewColums();

            getExportList();
            //getUsedPurchaseList(ref jResult);
        }

        private void setInfoBox()
        {
            //Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

            //DataTable dtCategory = Util.getCodeList("CD1101", "KEY", "VALUE");
            //Util.LookupEditHelper(rileCategory, dtCategory, "KEY", "VALUE");

            //DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            //Util.LookupEditHelper(rileNickName, dtNickName, "KEY", "VALUE");

            //DataTable dtProductGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
            //Util.LookupEditHelper(rileProductGrade, dtProductGrade, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "SELECT ALL");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");


            //DataTable dtWarehousingState = Util.getCodeList("CD0601", "KEY", "VALUE");
            //Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");


            //Util.LookupEditHelper(rileLocation, ProjectInfo._dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(rilePallet, ProjectInfo._dtPallet, "PALLET_ID", "PALLET_NM");

            DataTable dtWarehousingState = new DataTable();

            dtWarehousingState.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtWarehousingState.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtWarehousingState, 0, "");
            Util.insertRowonTop(dtWarehousingState, 1, "complete");
            Util.insertRowonTop(dtWarehousingState, 2, "scan");
            Util.insertRowonTop(dtWarehousingState, 3, "overlap");
            Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");

            DataTable dtMonType = Util.getCodeList("CD03011301", "KEY", "VALUE");
            foreach (DataRow row in dtMonType.Rows)
                _dicMonType.Add(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));

            //DataTable dtmanufactureType = new DataTable();

            //dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            //Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            //Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            //DataTable dtHinge = new DataTable();

            //dtHinge.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtHinge.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtHinge, 0, "");
            //Util.insertRowonTop(dtHinge, 1, "힌지파손");
            //Util.LookupEditHelper(rileHinge, dtHinge, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            gcExport.DataSource = null;
            _bsExport.DataSource = _dtExport;
            gcExport.DataSource = _bsExport;

            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;

            var today = DateTime.Today;
            var pastDate = today.AddDays(-365);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            deArrivalDt.EditValue = today;

            if (ProjectInfo._userCompanyId == 2)
                leCompany.EditValue = "-1";
            else
                leCompany.EditValue = ConvertUtil.ToString(ProjectInfo._userCompanyId);

            if (ProjectInfo._userCompanyId == 2)
            {
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                gcCompany.Visible = true;
            }
            else
            {
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCompany.Visible = false;
            }
        }

        private void getVCols()
        {
            if(ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("G"))
            {
                gcPrice.Visible = true;
                gcPrice1.Visible = true;
            }
            else
            {
                gcPrice.Visible = false;
                gcPrice1.Visible = false;
            }
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
            //lcgBarcodeList.BeginUpdate();
            //lcList.BeginInit();
            //gvList.BeginUpdate();
            //foreach (GridColumn gridcol in gvList.Columns)
            //{
            //    if (_listHideCol.Contains(gridcol.FieldName) || _lisDefaultHideCol.Contains(gridcol.FieldName))
            //        gridcol.Visible = false;
            //    else
            //        gridcol.Visible = true;
            //}

            //if(ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) || ProjectInfo._userId.Equals("lta104"))
            //{
            //    gcInitPrice.Visible = true;
            //    gcAdjust.Visible = true;
            //}
            //else
            //{
            //    gcInitPrice.Visible = false;
            //    gcAdjust.Visible = false;
            //}


            //gvList.EndUpdate();
            //lcList.EndInit();
            //lcgBarcodeList.EndUpdate();
        }

        private void setGridViewColumsWarehousing()
        {
            //lcgBarcodeList.BeginUpdate();
            //lcList.BeginInit();
            //gvList.BeginUpdate();
            //gvWarehousingList.BeginUpdate();
            //foreach (GridColumn gridcol in gvList.Columns)
            //{
            //    if (_listHideCol.Contains(gridcol.FieldName) || _lisDefaultHideCol.Contains(gridcol.FieldName))
            //        gridcol.Visible = false;
            //    else
            //        gridcol.Visible = true;
            //}

            //if (ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) || ProjectInfo._userId.Equals("lta104"))
            //{
            //    gcInitPrice.Visible = true;
            //    gcAdjust.Visible = true;

            //    gcComplete.Visible = true;
            //    gcProcess.Visible = true;
            //    //gcRemain.Visible = true;

            //    gcPasswordCnt.Visible = true;
            //    gcDisplayCnt.Visible = true;
            //    gcKeyboardCnt.Visible = true;
            //    gcUsbCnt.Visible = true;
            //    gcCaseCnt.Visible = true;
            //    gcWirelessCnt.Visible = true;
            //    gcBatteryCnt.Visible = true;
            //    gcEtcCnt.Visible = true;

            //    if (ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId))
            //    {
            //        lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = true;
            //        //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = true;
            //        lcgBarcodeList.CustomHeaderButtons[5].Properties.Visible = true;
            //    }
            //    else
            //    {
            //        lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //        //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //        lcgBarcodeList.CustomHeaderButtons[5].Properties.Visible = false;
            //    }
            //}
            //else
            //{
            //    gcInitPrice.Visible = false;
            //    gcAdjust.Visible = false;

            //    gcComplete.Visible = false;
            //    gcProcess.Visible = false;
            //    //gcRemain.Visible = false;

            //    gcPasswordCnt.Visible = false;
            //    gcDisplayCnt.Visible = false;
            //    gcKeyboardCnt.Visible = false;
            //    gcUsbCnt.Visible = false;
            //    gcCaseCnt.Visible = false;
            //    gcWirelessCnt.Visible = false;
            //    gcBatteryCnt.Visible = false;
            //    gcEtcCnt.Visible = false;

            //    lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //    //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //    lcgBarcodeList.CustomHeaderButtons[5].Properties.Visible = false;
            //}

            //gvWarehousingList.EndUpdate();
            //gvList.EndUpdate();
            //lcList.EndInit();
            //lcgBarcodeList.EndUpdate();
        }

        private void gvExport_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvExport.RowCount > 0);

            if (isValidRow)
            {
                _currentExportRow = e.Row as DataRowView;
                _export = ConvertUtil.ToString(_currentExportRow["EXPORT"]);
            }
            else
            {
                _currentExportRow = null;
                _export = "";
            }
        }

        private void gvExport_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvExport.RowCount > 0);

            if (isValidRow)
            {
                _currentExportRow = gvExport.GetRow(e.FocusedRowHandle) as DataRowView;
                _export = ConvertUtil.ToString(_currentExportRow["EXPORT"]);
            }
            else
            {
                _export = "";
                _currentExportRow = null;
            }
        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
            }
            else
            {
                _currentRow = null;
                _barcode = "";
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
            }
            else
            {
                _currentRow = null;
                _barcode = "";
            }
        }

        private bool getExportList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtExport.Clear();

            if (DBRelease.getExportListGroupByArrivalDt(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtExport.NewRow();

                        dr["EXPORT_ID"] = obj["EXPORT_ID"];
                        dr["COMPANY_ID"] = ConvertUtil.ToInt64(obj["COMPANY_ID"]); 
                        dr["EXPORT"] = obj["EXPORT"];
                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["ARRIVAL_DT"] = ConvertUtil.ToString(obj["ARRIVAL_DT"]);
                        dr["CNT"] = ConvertUtil.ToInt32(obj["CNT"]);
                        dr["RELEASE_CNT"] = ConvertUtil.ToInt32(obj["RELEASE_CNT"]);
                        dr["PRICE"] = ConvertUtil.ToInt64(obj["PRICE"]);
                        dr["CHECK"] = false;
                        _dtExport.Rows.Add(dr);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }



        private void geList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();


            jData.Add("LIST_EXPORT_ID", string.Join(",", _listExportId));
            if(ProjectInfo._userCompanyId != 2)
                jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            _dt.Clear();
            _dtProductDetail.Clear();

            if (DBRelease.getExportListDetail(jData, ref jResult))
            {
                int index = 1;

                if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PRODUCT_DATA_DETAIL"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtProductDetail.NewRow();
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["MBD_MANUFACT"] = obj["MBD_MANUFACT"];
                        dr["MBD_MODEL_NAME"] = obj["MBD_MODEL_NAME"];
                        dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                        dr["CPU_MODEL_NAME"] = obj["CPU_MODEL_NAME"];

                        dr["MEM_MANUFACT1"] = obj["MEM_MANUFACT1"];
                        dr["MEM_CAPACITY1"] = obj["MEM_CAPACITY1"];
                        dr["MEM_MANUFACT2"] = obj["MEM_MANUFACT2"];
                        dr["MEM_CAPACITY2"] = obj["MEM_CAPACITY2"];
                        dr["MEM_MANUFACT3"] = obj["MEM_MANUFACT3"];
                        dr["MEM_CAPACITY3"] = obj["MEM_CAPACITY3"];
                        dr["MEM_MANUFACT4"] = obj["MEM_MANUFACT4"];
                        dr["MEM_CAPACITY4"] = obj["MEM_CAPACITY4"];

                        dr["VGA_MODEL_NAME1"] = obj["VGA_MODEL_NAME1"];
                        dr["VGA_MODEL_NAME2"] = obj["VGA_MODEL_NAME2"];
                        dr["STG_TYPE1"] = obj["STG_TYPE1"];
                        dr["STG_CAPACITY1"] = obj["STG_CAPACITY1"];
                        dr["STG_TYPE2"] = obj["STG_TYPE2"];
                        dr["STG_CAPACITY2"] = obj["STG_CAPACITY2"];
                        dr["MON_SIZE"] = obj["MON_SIZE"];

                        _dtProductDetail.Rows.Add(dr);
                    }



                    jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());
                    DataRow[] rows;
                    DataRow row;
                    long inventoryId;
                    double battery;
                    
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);

                        dr["NO"] = index++;
                        dr["PRODUCE"] = ConvertUtil.ToString(obj["PRODUCE"]);
                        dr["INVENTORY_ID"] = inventoryId;
                        dr["EXPORT_ID"] = obj["EXPORT_ID"];
                        dr["EXPORT"] = obj["EXPORT"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PRICE"] = obj["PRICE"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        
                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                        dr["CAPTURE_YN"] = obj["CAPTURE_YN"];
                        dr["PRODUCT_YN"] = 1;
                        
                        dr["STATE"] = 0;
                        dr["CHECK"] = false;
                        
                        if (string.IsNullOrWhiteSpace(ConvertUtil.ToString(dr["ARRIVAL_DT"])))
                            dr["SACNT"] = 0;
                        else
                            dr["SACNT"] = 1;

                        dr["OS_NAME"] = ConvertUtil.ToString(obj["OS_NAME"]);
                        dr["ODD"] = ConvertUtil.ToString(obj["P_ODD"]);
                        dr["CAM"] = string.IsNullOrWhiteSpace(ConvertUtil.ToString(obj["P_CAM"])) ? 0 : 1;
                        dr["WIFI"] = ConvertUtil.ToInt32(obj["P_WIFI"]);
                        
                        battery = ConvertUtil.ToDouble(obj["P_BATTERY"]);
                        if (battery > 100.0)
                            battery = 100;

                        battery = Math.Round(battery);

                        dr["BATTERY"] = battery.ToString("F0");
                        dr["REMARKS1"] = ConvertUtil.ToString(obj["REMARKS1"]);
                        dr["REMARKS2"] = ConvertUtil.ToString(obj["REMARKS2"]);

                        dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURER"]);
                        dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                        dr["CAPTURE_FILE_ID"] = ConvertUtil.ToString(obj["CAPTURE_FILE_ID"]);
                        

                        rows = _dtProductDetail.Select($"INVENTORY_ID = {inventoryId}");

                        if (rows.Length > 0)
                        {
                            row = rows[0];

                            //dr["MBD_MANUFACT"] = row["MBD_MANUFACT"];
                            //dr["MBD_MODEL_NAME"] = row["MBD_MODEL_NAME"];
                            //dr["PRODUCT_NAME"] = row["PRODUCT_NAME"];
                            dr["CPU_MODEL_NAME"] = row["CPU_MODEL_NAME"];

                            dr["MEM_MANUFACT1"] = row["MEM_MANUFACT1"];
                            dr["MEM_CAPACITY1"] = row["MEM_CAPACITY1"];
                            dr["MEM_MANUFACT2"] = row["MEM_MANUFACT2"];
                            dr["MEM_CAPACITY2"] = row["MEM_CAPACITY2"];
                            dr["MEM_MANUFACT3"] = row["MEM_MANUFACT3"];
                            dr["MEM_CAPACITY3"] = row["MEM_CAPACITY3"];
                            dr["MEM_MANUFACT4"] = row["MEM_MANUFACT4"];
                            dr["MEM_CAPACITY4"] = row["MEM_CAPACITY4"];

                            dr["VGA_MODEL_NAME1"] = row["VGA_MODEL_NAME1"];
                            dr["VGA_MODEL_NAME2"] = row["VGA_MODEL_NAME2"];
                            dr["STG_TYPE1"] = row["STG_TYPE1"];
                            dr["STG_CAPACITY1"] = row["STG_CAPACITY1"];
                            dr["STG_TYPE2"] = row["STG_TYPE2"];
                            dr["STG_CAPACITY2"] = row["STG_CAPACITY2"];
                            dr["MON_SIZE"] = row["MON_SIZE"];
                        }

                        _dt.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["PART_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                    long inventoryId;
                    string componentCd;
                    string value;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                        componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = inventoryId;
                        //dr["PRODUCE"] = obj["PRODUCE"];
                        dr["EXPORT_ID"] = obj["EXPORT_ID"];
                        dr["EXPORT"] = obj["EXPORT"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PRICE"] = ConvertUtil.ToInt64(obj["PRICE"]);
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                        dr["CAPTURE_YN"] = 0;
                        dr["PRODUCT_YN"] = 0;
                        dr["OS_NAME"] = "";
                        dr["STATE"] = 0;
                        dr["CHECK"] = false;
                        if (string.IsNullOrWhiteSpace(ConvertUtil.ToString(dr["ARRIVAL_DT"])))
                            dr["SACNT"] = 0;
                        else
                            dr["SACNT"] = 1;

                        //dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURER"]);
                        //dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);


                        dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURE_NM"]);
                        //dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                        if (componentCd.Equals("MEM"))
                        {
                            value = ConvertUtil.ToString(obj["MODEL_NM"]);
                            if (value.Equals("SO-DIMM"))
                                dr["MODEL_NM"] = "laptop";
                            else if (value.Equals("UDIMM"))
                                dr["MODEL_NM"] = "desktop";
                            else
                                dr["MODEL_NM"] = value;


                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                            value = ConvertUtil.ToString(obj["COL2"]);
                            int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                            if (capa < 1024)
                                dr["COL2"] = value;
                            else
                                dr["COL2"] = $"{capa / 1024}GB";
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                        }
                        else if (componentCd.Equals("VGA"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            value = ConvertUtil.ToString(obj["COL2"]);
                            int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                            if (capa < 1024)
                                dr["COL2"] = value;
                            else
                                dr["COL2"] = $"{capa / 1024}GB";
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                        }
                        else if (componentCd.Equals("MON"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);

                            value = ConvertUtil.ToString(obj["COL3"]);

                            if (_dicMonType.ContainsKey(value))
                                dr["COL3"] = _dicMonType[value];
                            else
                                dr["COL3"] = "unkown";
                        }
                        else if (componentCd.Equals("BAT"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                            dr["COL4"] = ConvertUtil.ToString(obj["COL4"]);
                        }
                        else if (componentCd.Equals("ADP"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                            value = ConvertUtil.ToString(obj["COL4"]);

                            if (string.IsNullOrEmpty(value))
                            {
                                string svolt = ConvertUtil.ToString(obj["COL2"]);
                                string sampere = ConvertUtil.ToString(obj["COL3"]);
                                double volt = ConvertUtil.ParseDouble(svolt);
                                double ampere = ConvertUtil.ParseDouble(sampere);

                                double watt = Math.Round(volt * ampere, 0);

                                dr["COL2"] = ConvertUtil.ToInt16(watt);
                            }
                            else
                                dr["COL2"] = ConvertUtil.ToString(obj["COL4"]);


                            dr["COL3"] = ConvertUtil.ToString(obj["COL2"]);
                            dr["COL4"] = ConvertUtil.ToString(obj["COL3"]);

                            value = ConvertUtil.ToString(obj["COL5"]);
                            if (!string.IsNullOrEmpty(value))
                            {
                                double size = ConvertUtil.ParseDouble(value);
                                if (size > 0)
                                    dr["COL5"] = $"{value} mm";
                                else
                                    dr["COL5"] = value;
                            }
                            else
                                dr["COL5"] = value;

                        }
                        else
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                        }


                        //if (componentCd.Equals("CPU"))
                        //{
                        //    dr["CPU_MODEL_NAME"] = obj["MODEL_NM"]; 
                        //}
                        //else if (componentCd.Equals("MBD"))
                        //{
                        //    dr["MBD_MANUFACT"] = obj["MANUFACTURE_NM"];
                        //    dr["MBD_MODEL_NAME"] = obj["MBD_MODEL_NAME"];
                        //    dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                        //}
                        //else if (componentCd.Equals("MEM"))
                        //{
                        //    dr["MEM_MANUFACT1"] = $"{obj["MANUFACTURE_NM"]}/{obj["SPEC_NM"]}";

                        //    string scapa = Regex.Replace(ConvertUtil.ToString(obj["COL1"]), @"\D", "");
                        //    int capa = ConvertUtil.ToInt32(scapa);
                        //    capa = capa / 1024;

                        //    dr["MEM_CAPACITY1"] = $"{capa} GB";
                        //}
                        //else if (componentCd.Equals("STG"))
                        //{
                        //    string stgType = ConvertUtil.ToString(obj["COL1"]);
                        //    if (stgType.Contains("SSD"))
                        //        dr["STG_TYPE1"] = "SSD";
                        //    else
                        //        dr["STG_TYPE1"] = "HDD";

                        //    dr["STG_CAPACITY1"] = obj["SPEC_NM"];
                        //}
                        //else if (componentCd.Equals("VGA"))
                        //{
                        //    dr["VGA_MODEL_NAME1"] = obj["MODEL_NM"];
                        //}
                        //else if (componentCd.Equals("MON"))
                        //{
                        //    dr["MON_SIZE"] = obj["SPEC_NM"];
                        //}


                        _dt.Rows.Add(dr);
                    }
                }
            }
        }

        private bool checkSearch(ref JObject jData)
        {

            DateTime dtfrom = ConvertUtil.ToDateTime(deDtFrom, 1);
            DateTime dtto = ConvertUtil.ToDateTime(deDtTo, 2);

            if (dtfrom.Year == 1970 || dtto.Year == 1970)
            {
                jData.Add("MSG", "Check the start and end dates are correctly.");
                return false;
            }

            int result = DateTime.Compare(dtfrom, dtto);

            if (result > 0)
            {
                jData.Add("MSG", "The end date must be greater than the start date");
                return false;
            }

            TimeSpan TS = dtto - dtfrom;
            int diffDay = TS.Days;

            if (diffDay > 730)
            {
                jData.Add("MSG", "The search period cannot exceed 2 years(730 days).");
                return false;
            }

            string dtFrom = dtfrom.ToString("yyyy-MM-dd HH:mm:ss");
            string dtTo = dtto.ToString("yyyy-MM-dd HH:mm:ss");


            jData.Add("RELEASE_DT_S", dtFrom);
            jData.Add("RELEASE_DT_E", dtTo);

            if (!string.IsNullOrWhiteSpace(teExport.Text))
                jData.Add("EXPORT", ConvertUtil.ToString(teExport.Text));

            if (!ConvertUtil.ToString(leCompany.EditValue).Equals("-1"))
                jData.Add("COMPANY_ID", ConvertUtil.ToString(leCompany.EditValue));

            jData.Add("TYPE", 1);

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
                    Dangol.Message("There are no changes.");
                    return;
                }

                if (Dangol.MessageYN("Save the changes?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    var jArrayProduct = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 0);

                    int productYn;
                    DateTime dt;
                    foreach (DataRow row in rows)
                    {
                        dt = Convert.ToDateTime(ConvertUtil.ToString(row["ARRIVAL_DT"]));
                        productYn = ConvertUtil.ToInt32(row["PRODUCT_YN"]);
                        JObject jdata = new JObject();
                        jdata.Add("EXPORT_ID", ConvertUtil.ToInt64(row["EXPORT_ID"]));
                        jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));

                        jdata.Add("ARRIVAL_DT", dt);
                        jArray.Add(jdata);

                        if(productYn == 1)
                            jArrayProduct.Add(jdata);
                        
                    }

                    jobj.Add("DATA", jArray);
                    jobj.Add("PRODUCT_DATA", jArray);

                    if (DBRelease.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["STATE"] = 0;
                            row["SACNT"] = 1;
                        }

                        gvList.EndDataUpdate();
                        Dangol.CloseSplash();
                        Dangol.Message("Execution completed.");
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
                
            }
            else if (e.Button.Properties.Tag.Equals(9))
            {
               
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                
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

                        if (Dangol.MessageYN("Finished exporting Excel.\r\n. Open the excel file?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(5))
            {
                Dangol.ShowSplash();
                refresh();
                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(6))
            {
                ImageInfo.GetImage(1, true, _barcode);
            }
        }

        private void refresh()
        {           
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
            int rowhandle = gvExport.FocusedRowHandle;
            gvExport.FocusedRowHandle = -2147483646;
            gvExport.FocusedRowHandle = rowhandle;

            if (e.Button.Properties.Tag.Equals(1))
            {
                DataRow[] rows = _dtExport.Select("CHECK = TRUE");

                if (rows.Length < 1)
                {
                    Dangol.Message("Please choose the shipment number.");
                    return;
                }

                if (Dangol.MessageYN("Take the product information of the selected shipment number?") == DialogResult.No)
                {
                    return;
                }

                _listExportId.Clear();
                long exportId;
                foreach (DataRow row in rows)
                {
                    exportId = ConvertUtil.ToInt64(row["EXPORT_ID"]);
                    if(!_listExportId.Contains(exportId))
                        _listExportId.Add(exportId);
                }

                Dangol.ShowSplash();

                geList();

                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                Dangol.ShowSplash();
                getExportList();
                Dangol.CloseSplash();
            }
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
            if (e.Column.FieldName == "SACNT")
            {
                int cnt = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["SACNT"]));

                if (cnt == 1)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Green);
                else if (cnt == 2)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Orange);
                else if (cnt > 2)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
            }
            else if (e.Column.FieldName == "ARRIVAL_DT")
            {
                int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["STATE"]));

                if (state == 2)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                
            }
        }

        private void sbSetColVisible_Click(object sender, EventArgs e)
        {
            List<string> listHideCol = new List<string>();

            if (!ProjectInfo._userType.Equals("M") && !ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) && !ProjectInfo._userId.Equals("lta104"))
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
            getExportList();
            Dangol.CloseSplash();
        }

        private void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvExport.FocusedRowHandle;
            int topRowIndex = gvExport.TopRowIndex;
            gvExport.FocusedRowHandle = -2147483646;
            gvExport.FocusedRowHandle = rowhandle;

            try
            {
                gvExport.BeginUpdate();
                foreach (DataRow row in _dtExport.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvExport.DataRowCount; i++)
                {
                    int rowHandle = gvExport.GetVisibleRowHandle(i);
                    rows.Add(gvExport.GetDataRow(rowHandle));
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
                gvExport.EndUpdate();
            }
        }

        private void layoutControlGroup1_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvExport.FocusedRowHandle;
            int topRowIndex = gvExport.TopRowIndex;
            gvExport.FocusedRowHandle = -2147483646;
            gvExport.FocusedRowHandle = rowhandle;

            gvExport.BeginDataUpdate();

            foreach (DataRow row in _dtExport.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvExport.EndDataUpdate();
        }

        private void sbInput_Click(object sender, EventArgs e)
        {
            setData();
        }

        private void teScan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                setData();
            }
        }

        private void setData()
        {
            string scanData = teScan.Text.Trim();

            if (scanData.Length < 1 || string.IsNullOrWhiteSpace(scanData))
                return;

            if (scanData.Length == 12 && (regex1.IsMatch(scanData) || regex2.IsMatch(scanData) || regex3.IsMatch(scanData)))
            { 
                DataRow[] rows  = _dt.Select($"BARCODE = '{scanData}'");

                if (rows.Length > 0)
                {
                    gvList.BeginDataUpdate();

                    int value = ConvertUtil.ToInt32(rows[0]["SACNT"]);
                    if (value == 0)
                        rows[0]["SACNT"] = 2;
                    else
                        rows[0]["SACNT"] = 3;

                    string arrivalDt = ConvertUtil.ToString(rows[0]["ARRIVAL_DT"]);
                    rows[0]["ARRIVAL_DT"] = deArrivalDt.Text;

                    if (string.IsNullOrWhiteSpace(arrivalDt))
                        rows[0]["STATE"] = 2;
                    else
                    {
                        DateTime dtArrival = ConvertUtil.ToDateTime(arrivalDt);
                        arrivalDt = dtArrival.ToString("yyyy-MM-dd");

                        if (!arrivalDt.Equals(deArrivalDt.Text))
                            rows[0]["STATE"] = 2;
                    }
                    
                    gvList.EndDataUpdate();

                    int rowHandle = gvList.LocateByValue("BARCODE", scanData);

                    if (rowHandle > -1)
                    {
                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        {
                            gvList.FocusedRowHandle = -2147483646;
                            gvList.FocusedRowHandle = rowHandle;
                            gvList.TopRowIndex = rowHandle;
                        }
                    }
                }
                else
                {
                    JObject jResult = new JObject();
                    JObject jData = new JObject();


                    jData.Add("BARCODE", scanData);
                    if (ProjectInfo._userCompanyId != 2)
                        jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.getExportDataOneDetail(jData, ref jResult))
                    {
                        bool isChange = false;
                        if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
                        {
                            isChange = true;
                            JArray jArray = JArray.Parse(jResult["PRODUCT_DATA_DETAIL"].ToString());

                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                DataRow dr = _dtProductDetail.NewRow();
                                dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                                dr["MBD_MANUFACT"] = obj["MBD_MANUFACT"];
                                dr["MBD_MODEL_NAME"] = obj["MBD_MODEL_NAME"];
                                dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                                dr["CPU_MODEL_NAME"] = obj["CPU_MODEL_NAME"];

                                dr["MEM_MANUFACT1"] = obj["MEM_MANUFACT1"];
                                dr["MEM_CAPACITY1"] = obj["MEM_CAPACITY1"];
                                dr["MEM_MANUFACT2"] = obj["MEM_MANUFACT2"];
                                dr["MEM_CAPACITY2"] = obj["MEM_CAPACITY2"];
                                dr["MEM_MANUFACT3"] = obj["MEM_MANUFACT3"];
                                dr["MEM_CAPACITY3"] = obj["MEM_CAPACITY3"];
                                dr["MEM_MANUFACT4"] = obj["MEM_MANUFACT4"];
                                dr["MEM_CAPACITY4"] = obj["MEM_CAPACITY4"];

                                dr["VGA_MODEL_NAME1"] = obj["VGA_MODEL_NAME1"];
                                dr["VGA_MODEL_NAME2"] = obj["VGA_MODEL_NAME2"];
                                dr["STG_TYPE1"] = obj["STG_TYPE1"];
                                dr["STG_CAPACITY1"] = obj["STG_CAPACITY1"];
                                dr["STG_TYPE2"] = obj["STG_TYPE2"];
                                dr["STG_CAPACITY2"] = obj["STG_CAPACITY2"];
                                dr["MON_SIZE"] = obj["MON_SIZE"];

                                _dtProductDetail.Rows.Add(dr);
                            }



                            jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());
                            DataRow row;
                            long inventoryId;

                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                DataRow dr = _dt.NewRow();

                                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);

                                dr["NO"] = 0;
                                dr["INVENTORY_ID"] = inventoryId;
                                //dr["PRODUCE"] = obj["PRODUCE"];
                                dr["EXPORT_ID"] = obj["EXPORT_ID"];
                                dr["EXPORT"] = obj["EXPORT"];
                                dr["BARCODE"] = obj["BARCODE"];
                                dr["PRICE"] = obj["PRICE"];
                                dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                                dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);

                                string arrivalDt = ConvertUtil.ToString(obj["ARRIVAL_DT"]);
                                if (string.IsNullOrWhiteSpace(arrivalDt))
                                    dr["STATE"] = 2;
                                else
                                {
                                    DateTime dtArrival = ConvertUtil.ToDateTime(arrivalDt);
                                    arrivalDt = dtArrival.ToString("yyyy-MM-dd");

                                    if (!arrivalDt.Equals(deArrivalDt.Text))
                                        dr["STATE"] = 2;
                                    else
                                        dr["STATE"] = 0;
                                }

                                dr["ARRIVAL_DT"] = deArrivalDt.Text;
                                dr["CAPTURE_YN"] = obj["CAPTURE_YN"];
                                dr["PRODUCT_YN"] = 1;
                                dr["CHECK"] = false;
                                if (string.IsNullOrWhiteSpace(arrivalDt))
                                    dr["SACNT"] = 2;
                                else
                                    dr["SACNT"] = 3;

                                dr["OS_NAME"] = ConvertUtil.ToString(obj["OS_NAME"]);

                                dr["ODD"] = ConvertUtil.ToString(obj["P_ODD"]);
                                dr["CAM"] = string.IsNullOrWhiteSpace(ConvertUtil.ToString(obj["P_CAM"])) ? 0 : 1;
                                dr["WIFI"] = ConvertUtil.ToInt32(obj["P_WIFI"]);
                                dr["BATTERY"] = ConvertUtil.ToString(obj["P_BATTERY"]);
                                dr["REMARKS1"] = ConvertUtil.ToString(obj["REMARKS1"]);
                                dr["REMARKS2"] = ConvertUtil.ToString(obj["REMARKS2"]);

                                dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURER"]);
                                dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);


                                rows = _dtProductDetail.Select($"INVENTORY_ID = {inventoryId}");

                                if (rows.Length > 0)
                                {
                                    row = rows[0];

                                    //dr["MBD_MANUFACT"] = row["MBD_MANUFACT"];
                                    //dr["MBD_MODEL_NAME"] = row["MBD_MODEL_NAME"];
                                    //dr["PRODUCT_NAME"] = row["PRODUCT_NAME"];
                                    dr["CPU_MODEL_NAME"] = row["CPU_MODEL_NAME"];

                                    dr["MEM_MANUFACT1"] = row["MEM_MANUFACT1"];
                                    dr["MEM_CAPACITY1"] = row["MEM_CAPACITY1"];
                                    dr["MEM_MANUFACT2"] = row["MEM_MANUFACT2"];
                                    dr["MEM_CAPACITY2"] = row["MEM_CAPACITY2"];
                                    dr["MEM_MANUFACT3"] = row["MEM_MANUFACT3"];
                                    dr["MEM_CAPACITY3"] = row["MEM_CAPACITY3"];
                                    dr["MEM_MANUFACT4"] = row["MEM_MANUFACT4"];
                                    dr["MEM_CAPACITY4"] = row["MEM_CAPACITY4"];

                                    dr["VGA_MODEL_NAME1"] = row["VGA_MODEL_NAME1"];
                                    dr["VGA_MODEL_NAME2"] = row["VGA_MODEL_NAME2"];
                                    dr["STG_TYPE1"] = row["STG_TYPE1"];
                                    dr["STG_CAPACITY1"] = row["STG_CAPACITY1"];
                                    dr["STG_TYPE2"] = row["STG_TYPE2"];
                                    dr["STG_CAPACITY2"] = row["STG_CAPACITY2"];
                                    dr["MON_SIZE"] = row["MON_SIZE"];
                                }

                                _dt.Rows.InsertAt(dr, 0);
                            }

                            gvList.MoveFirst();
                        }

                        if (Convert.ToBoolean(jResult["PART_EXIST"]))
                        {
                            isChange = true;
                            JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                            long inventoryId;
                            string componentCd;
                            string value;
                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                DataRow dr = _dt.NewRow();

                                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                                componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                                dr["NO"] = 0;
                                dr["INVENTORY_ID"] = inventoryId;
                                dr["EXPORT_ID"] = obj["EXPORT_ID"];
                                dr["EXPORT"] = obj["EXPORT"];
                                dr["BARCODE"] = obj["BARCODE"];
                                dr["PRICE"] = obj["PRICE"];
                                dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                                dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                                string arrivalDt = ConvertUtil.ToString(obj["ARRIVAL_DT"]);
                                if (string.IsNullOrWhiteSpace(arrivalDt))
                                    dr["STATE"] = 2;
                                else
                                {
                                    DateTime dtArrival = ConvertUtil.ToDateTime(arrivalDt);
                                    arrivalDt = dtArrival.ToString("yyyy-MM-dd");

                                    if (!arrivalDt.Equals(deArrivalDt.Text))
                                        dr["STATE"] = 2;
                                    else
                                        dr["STATE"] = 0;
                                }
                                dr["ARRIVAL_DT"] = deArrivalDt.Text;
                                dr["CAPTURE_YN"] = 0;
                                dr["PRODUCT_YN"] = 0;
                                dr["CHECK"] = false;
                                if (string.IsNullOrWhiteSpace(ConvertUtil.ToString(dr["ARRIVAL_DT"])))
                                    dr["SACNT"] = 2;
                                else
                                    dr["SACNT"] = 3;
                                dr["OS_NAME"] = "";
                                dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURE_NM"]);
                                //dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                                if (componentCd.Equals("MEM"))
                                {
                                    value = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    if (value.Equals("SO-DIMM"))
                                        dr["MODEL_NM"] = "laptop";
                                    else if (value.Equals("UDIMM"))
                                        dr["MODEL_NM"] = "desktop";
                                    else
                                        dr["MODEL_NM"] = value;


                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                                    value = ConvertUtil.ToString(obj["COL2"]);
                                    int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                                    if (capa < 1024)
                                        dr["COL2"] = value;
                                    else
                                        dr["COL2"] = $"{capa / 1024}GB";
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                }
                                else if (componentCd.Equals("VGA"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    value = ConvertUtil.ToString(obj["COL2"]);
                                    int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                                    if (capa < 1024)
                                        dr["COL2"] = value;
                                    else
                                        dr["COL2"] = $"{capa / 1024}GB";
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                }
                                else if (componentCd.Equals("MON"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);

                                    value = ConvertUtil.ToString(obj["COL3"]);

                                    if (_dicMonType.ContainsKey(value))
                                        dr["COL3"] = _dicMonType[value];
                                    else
                                        dr["COL3"] = "unkown";
                                }
                                else if (componentCd.Equals("BAT"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                    dr["COL4"] = ConvertUtil.ToString(obj["COL4"]);
                                }
                                else if (componentCd.Equals("ADP"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                                    value = ConvertUtil.ToString(obj["COL4"]);

                                    if (string.IsNullOrEmpty(value))
                                    {
                                        string svolt = ConvertUtil.ToString(obj["COL2"]);
                                        string sampere = ConvertUtil.ToString(obj["COL3"]);
                                        double volt = ConvertUtil.ParseDouble(svolt);
                                        double ampere = ConvertUtil.ParseDouble(sampere);

                                        double watt = Math.Round(volt * ampere, 0);

                                        dr["COL2"] = ConvertUtil.ToInt16(watt);
                                    }
                                    else
                                        dr["COL2"] = ConvertUtil.ToString(obj["COL4"]);


                                    dr["COL3"] = ConvertUtil.ToString(obj["COL2"]);
                                    dr["COL4"] = ConvertUtil.ToString(obj["COL3"]);

                                    value = ConvertUtil.ToString(obj["COL5"]);
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        double size = ConvertUtil.ParseDouble(value);
                                        if (size > 0)
                                            dr["COL5"] = $"{value} mm";
                                        else
                                            dr["COL5"] = value;
                                    }
                                    else
                                        dr["COL5"] = value;

                                }
                                else
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                }

                                //if (componentCd.Equals("CPU"))
                                //{
                                //    dr["CPU_MODEL_NAME"] = obj["MODEL_NM"];
                                //}
                                //else if (componentCd.Equals("MBD"))
                                //{
                                //    dr["MBD_MANUFACT"] = obj["MANUFACTURE_NM"];
                                //    dr["MBD_MODEL_NAME"] = obj["MBD_MODEL_NAME"];
                                //    dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                                //}
                                //else if (componentCd.Equals("MEM"))
                                //{
                                //    dr["MEM_MANUFACT1"] = $"{obj["MANUFACTURE_NM"]}/{obj["SPEC_NM"]}";
                                //    dr["MEM_CAPACITY1"] = obj["COL1"];
                                //}
                                //else if (componentCd.Equals("STG"))
                                //{
                                //    string stgType = ConvertUtil.ToString(obj["COL1"]);
                                //    if (stgType.Contains("SSD"))
                                //        dr["STG_TYPE1"] = "SSD";
                                //    else
                                //        dr["STG_TYPE1"] = "HDD";
                                //    dr["STG_CAPACITY1"] = obj["SPEC_NM"];
                                //}
                                //else if (componentCd.Equals("VGA"))
                                //{
                                //    dr["VGA_MODEL_NAME1"] = obj["MODEL_NM"];
                                //}
                                //else if (componentCd.Equals("MON"))
                                //{
                                //    dr["MON_SIZE"] = obj["SPEC_NM"];
                                //}


                                _dt.Rows.InsertAt(dr, 0);
                            }
                            gvList.MoveFirst();
                        }

                        if (isChange)
                        {
                            try
                            {
                                gvList.BeginUpdate();

                                ArrayList arows = new ArrayList();
                                for (int i = 0; i < gvList.DataRowCount; i++)
                                {
                                    int rowHandle = gvList.GetVisibleRowHandle(i);
                                    arows.Add(gvList.GetDataRow(rowHandle));
                                }

                                for (int i = 0; i < arows.Count; i++)
                                {
                                    DataRow row = arows[i] as DataRow;
                                    // Change the field value.
                                    row["NO"] = i + 1;
                                }
                            }
                            finally
                            {
                                gvList.EndUpdate();
                            }
                        }
                    }
                }
            }
            teScan.Text = "";
        }

        private void gvList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "CAPTURE_YN")
            {
                //if (_currentRow == null)
                //    Dangol.Warining("No product selected.");
                //else
                //{
                //    if (ConvertUtil.ToInt32(_currentRow["CAPTURE_YN"]) == 1)
                //    {
                //        Image image = ScreenCapture.GetCaptureImg($"{_currentRow["PRODUCE"]}", $"{_currentRow["INVENTORY_ID"]}.png");

                //        using (DlgImgTest digImgTest = new DlgImgTest(image))
                //        {
                //            digImgTest.ShowDialog(this);
                //            File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_currentRow["INVENTORY_ID"]}.png");
                //        }
                //    }
                //    else
                //        Dangol.Message("The QC image does not exist.");
                //}

                if (_currentRow == null)
                    Dangol.Warining("No product selected.");
                else
                {
                    if (ConvertUtil.ToInt32(_currentRow["CAPTURE_YN"]) == 1)
                    {
                        if (ConvertUtil.ToInt64(_currentRow["CAPTURE_FILE_ID"]) < 1)
                        {
                            Dangol.Message("The QC image does not exist.");
                        }
                        else
                        {
                            Image img = CaptureUtils.getCaptureFile(ConvertUtil.ToInt64(_currentRow["CAPTURE_FILE_ID"]));

                            using (DlgImgTest digImgTest = new DlgImgTest(img))
                            {
                                digImgTest.ShowDialog(this);
                                File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_currentRow["CAPTURE_FILE_ID"]}.png");
                            }
                        }
                    }
                }
            }
        }
    }
}