using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.adjustment;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.check
{
    public partial class usrTabletCheckList : DevExpress.XtraEditors.XtraForm
    {
        int _viewCategory = (int)Enum.EnumView.Check;
        int _viewType = (int)Enum.EnumTableCheck.Tablet;

        DataTable _dtWarehousing;
        DataTable _dt;

        BindingSource _bsWarehousing;
        BindingSource _bs;

        DataRowView _currentRow = null;
        DataRowView _currentWarehousing = null;

        long _warehousingId = -1;

        List<long> _listWarehousingId;

        List<string> _listVisibleCol;
        List<string> _listHideCol;

        List<string> _listReadOnlyCol;

        List<string> _lisDefaultHideCol;

        bool isLoad = true;
        public usrTabletCheckList()
        {
            InitializeComponent();

            _dtWarehousing = new DataTable();
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_STATE", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));

            _dt.Columns.Add(new DataColumn("EXAM_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAM_USER_ID", typeof(string)));

            _dt.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("CAPACITY", typeof(string)));
            _dt.Columns.Add(new DataColumn("RESOLUTION", typeof(string)));
            _dt.Columns.Add(new DataColumn("DISPLAY_SIZE", typeof(string)));
            _dt.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_YN", typeof(string)));

            _dt.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dt.Columns.Add(new DataColumn("SELF_CHECK", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_STABBED", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_PRESSED", typeof(string)));
            _dt.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(string)));
            _dt.Columns.Add(new DataColumn("DISPLAY", typeof(string)));
            _dt.Columns.Add(new DataColumn("BATTERY", typeof(string)));
            _dt.Columns.Add(new DataColumn("ADAPTER", typeof(string)));
            _dt.Columns.Add(new DataColumn("BUTTON", typeof(string)));
            _dt.Columns.Add(new DataColumn("USB_PORT", typeof(string)));
            _dt.Columns.Add(new DataColumn("USB_CABLE", typeof(string)));
            _dt.Columns.Add(new DataColumn("PEN", typeof(string)));
            _dt.Columns.Add(new DataColumn("SD_CARD", typeof(string)));
            _dt.Columns.Add(new DataColumn("SOFTWARE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CAM", typeof(string)));
            _dt.Columns.Add(new DataColumn("SOUND", typeof(string)));
            _dt.Columns.Add(new DataColumn("MIKE", typeof(string)));
            _dt.Columns.Add(new DataColumn("EAR_PHONE", typeof(string)));
            _dt.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(string)));
            _dt.Columns.Add(new DataColumn("TEST_CHECK", typeof(string)));          
            _dt.Columns.Add(new DataColumn("ETC_DES", typeof(string)));



            _bsWarehousing = new BindingSource();
            _bs = new BindingSource();

            //_bsWarehousing.DataSource = _dtWarehousing;
            //_bs.DataSource = _dt;

            _listWarehousingId = new List<long>();
            _listVisibleCol = new List<string>();
            _listHideCol = new List<string>();
            _listReadOnlyCol = new List<string>(new[] { "INVENTORY_ID"});
            _lisDefaultHideCol = new List<string>(new[] { "INVENTORY_ID"});
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            leCheckType.EditValueChanged -= leCheckType_EditValueChanged;

            setInfoBox();
            setIInitData();
            //getVisibleCol();
            setGridViewColums();

            leCheckType.EditValueChanged += leCheckType_EditValueChanged;
            JObject jResult = new JObject();
            getWarehousingList(ref jResult);
            //getUsedPurchaseList(ref jResult);

            isLoad = false;

        }

        private void setInfoBox()
        {
            //Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");
          
            DataTable dtManufactureNm = Util.getCodeList("CD1601", "KEY", "VALUE");
            Util.LookupEditHelper(rileManufactureNm, dtManufactureNm, "KEY", "VALUE");

            DataTable dtModelNme = Util.getCodeList("CD1602", "KEY", "VALUE");
            Util.LookupEditHelper(rileModelNm, dtModelNme, "KEY", "VALUE");

            DataTable dtCapacity = Util.getCodeList("CD1603", "KEY", "VALUE");
            Util.LookupEditHelper(rileCapacity, dtCapacity, "KEY", "VALUE");

            DataTable dtProductGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductGrade, dtProductGrade, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            //Util.LookupEditHelper(rileComapnyId2, dtCompany, "KEY", "VALUE");

            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

            DataTable dtWarehousingState = Util.getCodeList("CD0601", "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");

            DataTable dtCheckTypet = new DataTable();

            dtCheckTypet.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCheckTypet.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtCheckTypet, 3, " 리페어");
            //Util.insertRowonTop(dtCheckTypet, 2, " 생산");
            Util.insertRowonTop(dtCheckTypet, 1, " 입고");

            Util.LookupEditHelper(leCheckType, dtCheckTypet, "KEY", "VALUE");

           var today = DateTime.Today;
           var pastDate = today.AddDays(-180);

           deDtFrom.EditValue = pastDate;
           deDtTo.EditValue = today;

           leCheckType.ItemIndex = 0;
           //leReceiptState.ItemIndex = 0;

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

            gvList.EndUpdate();
            lcList.EndInit();
            lcgBarcodeList.EndUpdate();
        }


        private void gvWarehousingList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingList.RowCount > 0);

            if(!isLoad)
                Dangol.ShowSplash();
            gvList.BeginDataUpdate();
            _dt.Clear();
            if (isValidRow)
            {
                _currentWarehousing = gvWarehousingList.GetRow(e.FocusedRowHandle) as DataRowView;
                _warehousingId = ConvertUtil.ToInt32(_currentWarehousing["WAREHOUSING_ID"]);
                geList();
            }
            else
            {
                _currentWarehousing = null;
                _warehousingId = -1;
            }
            gvList.EndDataUpdate();
            if (!isLoad)
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

        private void leCheckType_EditValueChanged(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            gvList.BeginDataUpdate();
            _dt.Clear();
            geList();
            gvList.EndDataUpdate();
            Dangol.CloseSplash();
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            
            JObject jResult = new JObject();
            //Dangol.ShowSplash();
            getWarehousingList(ref jResult);
            //Dangol.CloseSplash();
        }


        private bool getWarehousingList(ref JObject jResult)
        {
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            jData.Add("WAREHOUSING_TYPE", 1);
            jData.Add("WAREHOUSING_CATEGORY", 2);

            _dtWarehousing.Clear();

            if (DBAdjustment.getWarehousingListAll(jData, ref jResult))
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
                        dr["SPEC_NM"] = obj["SPEC_NM"];
                        
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

        private void setCheckInfo(ref DataRow dr, JObject objCheck)
        {
           
            dr["PRODUCT_GRADE"] = objCheck["PRODUCT_GRADE"];
            dr["ETC_DES"] = objCheck["ETC_DES"];

            int checkValue = 0;
            List<string> checkList;
            string content;

            foreach (string col in ExamineInfo._TABLETCOLNAME)
            {
                content = "";
                checkValue = ConvertUtil.ToInt32(objCheck[col]);

                if (checkValue > 0)
                {
                    if (ExamineInfo._listCaseCheckCol.Contains(col))
                    {
                        checkList = ExamineInfo._TABLETCHECK["CASE"];
                        for (int i = 0; i < checkList.Count; i++)
                        {
                            if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                content += $"{ExamineInfo._TABLETCHECK["CASE"][i]}{ExamineInfo._SYMBOL[1]},";
                            else
                                content += $"{ExamineInfo._TABLETCHECK["CASE"][i]}{ExamineInfo._SYMBOL[0]},";
                        }
                    }
                    else
                    {
                        checkList = ExamineInfo._TABLETCHECK[col];
                        for (int i = 0; i < checkList.Count; i++)
                        {
                            if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                content += $"{ExamineInfo._TABLETCHECK[col][i]},";
                        }
                    }

                    if (content.Length > 1)
                        content = content.Substring(0, content.Length - 1);

                    dr[col] = content;
                }
                else
                    dr[col] = "";
            }

            if(!string.IsNullOrWhiteSpace(ConvertUtil.ToString(objCheck["BATTERY_REMAIN"])))
                dr["BATTERY"] += $"{dr["BATTERY"]} [{ objCheck["BATTERY_REMAIN"]}%]";      
        }

        private void geList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("CHECK_TYPE", ConvertUtil.ToInt32(leCheckType.EditValue)); 
            jData.Add("PRODUCT_TYPE", 5); //노트북
            jData.Add("TN_TABLE_PART", "TN_WAREHOUSING_PART");
            jData.Add("TN_TABLE", "TN_WAREHOUSING");
            jData.Add("REPRESENTATIVE_ID_COL", "WAREHOUSING_ID");
            jData.Add("REPRESENTATIVE_KEY_COL", "WAREHOUSING");
            jData.Add("REPRESENTATIVE_ID", _warehousingId);

            if (DBCheck.getTabletCheckList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        long inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                        string checkYn = ConvertUtil.ToString(obj["CHECK_YN"]);

                        DataRow dr = _dt.NewRow();

                        dr["INVENTORY_ID"] = inventoryId;
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["EXAM_DT"] = ConvertUtil.ToDateTime(obj["EXAM_DT"], "yyyy-MM-dd");
                        dr["EXAM_USER_ID"] = obj["EXAM_USER_ID"];
                        dr["MANUFACTURE_NM"] = obj["MANUFACTURE_NM"];
                        dr["PRODUCT_NM"] = obj["PRODUCT_NM"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["CAPACITY"] = obj["CAPACITY"];
                        dr["RESOLUTION"] = obj["RESOLUTION"];
                        dr["DISPLAY_SIZE"] = obj["DISPLAY_SIZE"];
                        dr["SERIAL_NO"] = obj["SERIAL_NO"];

                        dr["CHECK_YN"] = obj["CHECK_YN"];

                        if (checkYn.Equals("Y"))
                            setCheckInfo(ref dr, obj);

                        _dt.Rows.Add(dr);

                    }
                }
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

            if (diffDay > 365)
            {
                jData.Add("MSG", "검색 기간은 1년(365일)을 초과할 수 없습니다.");
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

            //if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
            //    jData.Add("PRODUCT_GRADE", ConvertUtil.ToString(leReceiptState.EditValue));

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
                        jdata.Add("INIT_PRICE", ConvertUtil.ToString(row["INIT_PRICE"]));
                        jdata.Add("ADJUST_PRICE", ConvertUtil.ToString(row["ADJUST_PRICE"]));
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
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 제품으로 정산을 진행하시겠습니까(정산 대기 제품만 진행)?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    List<long> listInventoryId = new List<long>();
                    List<long> listRepresentativeId = new List<long>();
                    string adjustmentState;
                    foreach (DataRow row in rows)
                    {
                        adjustmentState = ConvertUtil.ToString(row["ADJUSTMENT_STATE"]);

                        if (adjustmentState.Equals("1"))
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
            //GridView View = sender as GridView;
            //if (e.Column.FieldName == "BARCODE")
            //{
            //    string state = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]);

            //    if (state.Equals("2"))
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
            //    }
            //}
        }

        private void sbSetColVisible_Click(object sender, EventArgs e)
        {
            List<string> listHideCol = new List<string>();
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
    }
}