using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.Report;
using WareHousingMaster.view.common;
using WareHousingMaster.view.PreView;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrUsedPurchaseStatus : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtUsedPurchase;
        DataTable _dtUsedPurchasePart;

        BindingSource _bsUsedPurchase;

        Dictionary<string, Color> _dicStateColor;

        Dictionary<string, string> _dicUserType1;
        Dictionary<string, string> _dicUserType2;
        
        DataRowView _currentRow;
        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        string _receipt;

        bool initialize = true;
        bool initializeEnter = true;


        public usrUsedPurchaseStatus()
        {
            InitializeComponent();

             _dtUsedPurchase = new DataTable();
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_ID", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("NO", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT", typeof(string)));            
            _dtUsedPurchase.Columns.Add(new DataColumn("SOURCE_CD", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TYPE", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TYPE2", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_NM", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TEL", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_HP", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("PURCHASE_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("FAULT_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("ADJUSTMENT_STATE", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("COMPLETE_DT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USEDPURCHASE_STATE", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_REPORT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("CHECK_STATE", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RETURN_STATE", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("DT", typeof(long)));


            _dtUsedPurchasePart = new DataTable();
            _dtUsedPurchasePart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtUsedPurchasePart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtUsedPurchasePart.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtUsedPurchasePart.Columns.Add(new DataColumn("PART_CNT", typeof(int)));

            _bsUsedPurchase = new BindingSource();
            //_dicWarehouMovementList = new Dictionary<long, Dictionary<long, string>>();

            _dicStateColor = new Dictionary<string, Color>();
            _dicStateColor.Add("-1", Color.Transparent);
            _dicStateColor.Add("0", Color.Transparent);
            _dicStateColor.Add("1", Color.Lavender);
            _dicStateColor.Add("2", Color.Transparent);
            _dicStateColor.Add("3", Color.LemonChiffon);
            _dicStateColor.Add("4", Color.Transparent);
            _dicStateColor.Add("5", Color.LimeGreen);
            _dicStateColor.Add("6", Color.Goldenrod);
            _dicStateColor.Add("7", Color.LightGreen);
            _dicStateColor.Add("8", Color.Transparent);
            _dicStateColor.Add("9", Color.Gainsboro);
            _dicStateColor.Add("A", Color.Yellow);
            _dicStateColor.Add("B", Color.Transparent);
            _dicStateColor.Add("C", Color.Transparent);
            _dicStateColor.Add("D", Color.Transparent);
            _dicStateColor.Add("E", Color.Transparent);
            _dicStateColor.Add("F", Color.Transparent);

            _dicUserType1 = new Dictionary<string, string>();
            _dicUserType2 = new Dictionary<string, string>();

            initializeEnter = true;

        }

        private void usrUsedPurchaseStatus_Load(object sender, EventArgs e)
        {
            setInfoBox();   
            setIInitData();

            JObject jResult = new JObject();
            getUsedPurchaseList(ref jResult);

            ArrayList rows = new ArrayList();
            for (int i = 0; i < gvUsedPurchaseList.DataRowCount; i++)
            {
                int rowHandle = gvUsedPurchaseList.GetVisibleRowHandle(i);
                rows.Add(gvUsedPurchaseList.GetDataRow(rowHandle));
            }

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                // Change the field value.
                row["NO"] = i + 1;
            }

            //teInputBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            //teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
        }
        //private void usrComponentKeep_Load(object sender, EventArgs e)
        //{
        //    setInfoBox();
        //    setIInitData();

        //    //teInputBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
        //    //teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
        //}
        private void usrWarehouseMovementStatus_Enter(object sender, EventArgs e)
        {
            if (!initializeEnter)
                receiptRefresh();

            initializeEnter = false;
        }

        private void setInfoBox()
        {
            DataTable dtUserType1 = Util.getCodeList("CD1301", "KEY", "VALUE");
            Util.LookupEditHelper(rileUserType1, dtUserType1, "KEY", "VALUE");

            foreach(DataRow dr in dtUserType1.Rows)
                _dicUserType1.Add(ConvertUtil.ToString(dr["KEY"]), ConvertUtil.ToString(dr["VALUE"]));

            Util.insertRowonTop(dtUserType1, "-1", "선택안합");
            Util.LookupEditHelper(leUserType1, dtUserType1, "KEY", "VALUE");

            DataTable dtUserType2 = Util.getCodeList("CD1302", "KEY", "VALUE");
            Util.insertRowonTop(dtUserType2, "-1", "선택안합");
            Util.LookupEditHelper(rileUserType2, dtUserType2, "KEY", "VALUE");
            Util.LookupEditHelper(leUserType2, dtUserType2, "KEY", "VALUE");

            foreach (DataRow dr in dtUserType2.Rows)
                _dicUserType2.Add(ConvertUtil.ToString(dr["KEY"]), ConvertUtil.ToString(dr["VALUE"]));

            DataTable dtUsedPurchaseState = Util.getCodeList("CD1303", "KEY", "VALUE");
            Util.insertRowonTop(dtUsedPurchaseState, "-1", "선택안합");
            Util.LookupEditHelper(rileUsedPurchaseState, dtUsedPurchaseState, "KEY", "VALUE");
            Util.LookupEditHelper(leReceiptState, dtUsedPurchaseState, "KEY", "VALUE");

            DataTable dtUsedAdjustmentState = Util.getCodeList("CD1304", "KEY", "VALUE");
            Util.LookupEditHelper(rileAdjustmentState, dtUsedAdjustmentState, "KEY", "VALUE");

            DataTable dtCheckState = Util.getCodeList("CD1306", "KEY", "VALUE");
            Util.LookupEditHelper(rileCheckState, dtCheckState, "KEY", "VALUE");

            DataTable dtReturnState = Util.getCodeList("CD1307", "KEY", "VALUE");
            Util.LookupEditHelper(rileReturnState, dtReturnState, "KEY", "VALUE");

            DataTable dtSource= Util.getCodeList("CD1309", "KEY", "VALUE");
            Util.LookupEditHelper(rileSource, dtSource, "KEY", "VALUE");
            

            var today = DateTime.Today;
            var pastDate = today.AddDays(-30);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            leUserType1.ItemIndex = 0;
            leUserType2.ItemIndex = 0;
            leReceiptState.ItemIndex = 0;


        }

        private void setIInitData()
        {
            gcUsedPurchaseList.DataSource = null;
            _bsUsedPurchase.DataSource = _dtUsedPurchase;
            gcUsedPurchaseList.DataSource = _bsUsedPurchase;

            _bsUsedPurchase.Sort = "DT DESC";
        }

        private void gvUsedPurchaseList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvUsedPurchaseList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                _receipt = ConvertUtil.ToString(_currentRow["RECEIPT"]);

            }
            else 
            {
                _currentRow = null;
                _receipt = "";
            }
        }

        private void gvUsedPurchaseList_DoubleClick(object sender, EventArgs e)
        {
            bool isValidRow = (_currentRow != null && gvUsedPurchaseList.RowCount > 0);

            if (isValidRow)
            {


            }
        }
        private void gvUsedPurchaseList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "RECEIPT_REPORT")
            {
                showReceiptPrint(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"], _currentRow["SOURCE_CD"]);
            }
            else if (e.Column.FieldName == "NO" || e.Column.FieldName == "RECEIPT" || e.Column.FieldName == "RECEIPT_DT" 
                || e.Column.FieldName == "USER_TYPE1" || e.Column.FieldName == "USER_TYPE2" || e.Column.FieldName == "USER_NM" || e.Column.FieldName == "SOURCE")
            {
                string tabName = "RECEIPT DETAIL";
                if (!(ProjectInfo._tabbedView.Documents.Any(x => x.Form.Tag.ToString() == tabName) || ProjectInfo._tabbedView.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
                {
                    Dangol.ShowSplash();
                    ProjectInfo._usedPurchaseReceiptDetail = new usrUsedPurchaseReceiptDetail();
                    ProjectInfo._usedPurchaseReceiptDetail.Tag = tabName;

                    ProjectInfo._usedPurchaseReceiptDetail.MdiParent = this.MdiParent;
                    ProjectInfo._usedPurchaseReceiptDetail.setInitData(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"], _currentRow["USEDPURCHASE_STATE"], _currentRow["SOURCE_CD"]);
                    Dangol.CloseSplash();

                    ProjectInfo._usedPurchaseReceiptDetail.Show();
                    ProjectInfo._biusrUsedPurchaseReceiptDetail.Caption = "중고매입상세";
                    if (!ProjectInfo._ribbonTabs.ContainsKey(ProjectInfo._biusrUsedPurchaseReceiptDetail))
                        ProjectInfo._ribbonTabs.Add(ProjectInfo._biusrUsedPurchaseReceiptDetail, ProjectInfo._usedPurchaseReceiptDetail);
                    else
                    {
                        ProjectInfo._ribbonTabs.Remove(ProjectInfo._biusrUsedPurchaseReceiptDetail);
                        ProjectInfo._ribbonTabs.Add(ProjectInfo._biusrUsedPurchaseReceiptDetail, ProjectInfo._usedPurchaseReceiptDetail);
                    }
                }
                else
                {
                    Dangol.ShowSplash();
                    ProjectInfo._usedPurchaseReceiptDetail.reload(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"], _currentRow["USEDPURCHASE_STATE"], _currentRow["SOURCE_CD"]);
                    ProjectInfo._documentManager.View.ActivateDocument(ProjectInfo._ribbonTabs[ProjectInfo._biusrUsedPurchaseReceiptDetail]);
                    Dangol.CloseSplash();
                    ProjectInfo._usedPurchaseReceiptDetail.Show();
                }

            }
        }

        private void showReceiptPrint(object receiptiD, object receipt, object sourceCd)
        {
            JObject jResult = new JObject();

            bool isSuccess = false;

            if (ConvertUtil.ToInt32(sourceCd) == 0)
                isSuccess = DBConnect.getUsedPurchaseDanawaShowPrint(receiptiD, receipt, ref jResult);
            else
                isSuccess = DBConnect.getUsedPurchaseShowPrint(receiptiD, receipt, ref jResult);

            if (isSuccess)
            {
                _dtUsedPurchasePart.Clear();
                string userType1;
                string userType2;
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    userType1 = ConvertUtil.ToString(jData["USER_TYPE"]);
                    userType2 = ConvertUtil.ToString(jData["USER_TYPE2"]);

                    if(_dicUserType1.ContainsKey(userType1))
                        userType1 = _dicUserType1[ConvertUtil.ToString(jData["USER_TYPE"])];
                    if (_dicUserType2.ContainsKey(userType2))
                        userType2 = _dicUserType2[ConvertUtil.ToString(jData["USER_TYPE2"])];


                    JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtUsedPurchasePart.NewRow();

                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["PRICE"] = obj["PRICE"];
                        dr["PART_CNT"] = obj["PART_CNT"];

                        _dtUsedPurchasePart.Rows.Add(dr);
                    }

                    XtraReport report = new XtraReport();
                    rpUsedPurchaseCertificate rpUPC = new rpUsedPurchaseCertificate(jData, userType1, userType2);
                    rpUPC.DataBinding();
                    rpUPC.DataSource = _dtUsedPurchasePart;
                    report = rpUPC;

                    using (DlgReport dlgReport = new DlgReport("접수증", report))
                    {
                        if (dlgReport.ShowDialog(this) == DialogResult.OK)
                        {

                        }
                    }
                }

            }
            else
            {
                return;
            }
        }






        private void lgcUsedPurchasedState_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcUsedPurchaseList.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                showReceiptPrint(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"], _currentRow["SOURCE_CD"]);
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                ImageInfo.GetImage(2, false, $"{_currentRow["RECEIPT"]}");
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

            if(result > 0)
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


            jData.Add("RECEIPT_DT_FROM", dtFrom);
            jData.Add("RECEIPT_DT_TO", dtTo);

            


            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teReceip.Text)))
                jData.Add("RECEIPT", teReceip.Text);

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCustomerNm.Text)))
                jData.Add("USER_NM", teCustomerNm.Text);

            if (!teTel.Text.Equals(ConvertUtil.ToString(teTel.Text)))
                jData.Add("TEL", teTel.Text);

            if (ConvertUtil.ToInt32(leUserType1.EditValue) >= 0)
                jData.Add("USER_TYPE1", ConvertUtil.ToInt32(leUserType1.EditValue));

            if (ConvertUtil.ToInt32(leUserType2.EditValue) >= 0)
                jData.Add("USER_TYPE2", ConvertUtil.ToInt32(leUserType2.EditValue));

            if (!ConvertUtil.ToString(leReceiptState.EditValue).Equals("-1"))
                jData.Add("RECEIPT_STATE", ConvertUtil.ToString(leReceiptState.EditValue));

            return true;
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            receiptRefresh();
            Dangol.CloseSplash();
        }

        private void receiptRefresh()
        {
            string receipt = _receipt;
            gvUsedPurchaseList.FocusedRowObjectChanged -= gvUsedPurchaseList_FocusedRowObjectChanged;
            JObject jResult = new JObject();
            getUsedPurchaseList(ref jResult);
            gvUsedPurchaseList.FocusedRowObjectChanged += gvUsedPurchaseList_FocusedRowObjectChanged;

            int rowHandle = gvUsedPurchaseList.LocateByValue("RECEIPT", receipt);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvUsedPurchaseList.FocusedRowHandle = -2147483646;
                    gvUsedPurchaseList.FocusedRowHandle = rowHandle;
                }
            }
            else
            {
                if (_dtUsedPurchase.Rows.Count > 0)
                {
                    gvUsedPurchaseList.FocusedRowHandle = -2147483646;
                    gvUsedPurchaseList.MoveFirst();
                }
            }

            ArrayList rows = new ArrayList();
            for (int i = 0; i < gvUsedPurchaseList.DataRowCount; i++)
            {
                rowHandle = gvUsedPurchaseList.GetVisibleRowHandle(i);
                rows.Add(gvUsedPurchaseList.GetDataRow(rowHandle));
            }

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                // Change the field value.
                row["NO"] = i+1;
            }
        }

        private bool getUsedPurchaseList(ref JObject jResult)
        {
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }
            gvUsedPurchaseList.BeginDataUpdate();
            _dtUsedPurchase.BeginInit();
            _dtUsedPurchase.Clear();

            if (DBConnect.getUsedPurchaseList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DANAWA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DANAWA_DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtUsedPurchase.NewRow();
                        
                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        //dr["NO"] = index++;
                        dr["RECEIPT"] = obj["RECEIPT"];
                        dr["SOURCE_CD"] = "0";
                        
                        dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                        dr["USER_TYPE"] = ConvertUtil.ToInt32(obj["USER_TYPE"]);
                        dr["USER_TYPE2"] = ConvertUtil.ToInt32(obj["USER_TYPE2"]);
                        dr["USER_ID"] = obj["USER_ID"];
                        dr["USER_NM"] = obj["USER_NM"];
                        dr["USER_TEL"] = obj["USER_TEL"];
                        dr["PURCHASE_COST"] = ConvertUtil.ToInt64(obj["PURCHASE_COST"]);
                        dr["FAULT_COST"] = ConvertUtil.ToInt64(obj["FAULT_COST"]);
                        dr["USER_COST"] = ConvertUtil.ToInt64(obj["USER_COST"]);
                        dr["ADJUSTMENT_STATE"] = ConvertUtil.ToInt32(obj["ADJUSTMENT_STATE"]);
                        dr["COMPLETE_DT"] = ConvertUtil.ToDateTimeNull(obj["COMPLETE_DT"]);
                        dr["USEDPURCHASE_STATE"] = obj["USEDPURCHASE_STATE"];
                        dr["RECEIPT_REPORT"] = "[접수증]";
                        
                        dr["CHECK_STATE"] = ConvertUtil.ToString(obj["CHECK_STATE"]);
                        dr["RETURN_STATE"] = ConvertUtil.ToString(obj["RETURN_STATE"]);
                        dr["ETC"] = obj["ETC"];
                        dr["DT"] = obj["RECEIPT_DT"];
                        _dtUsedPurchase.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtUsedPurchase.NewRow();

                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        //dr["NO"] = index++;
                        dr["RECEIPT"] = obj["RECEIPT"];
                        dr["SOURCE_CD"] = ConvertUtil.ToString(obj["SOURCE_CD"]);
                        
                        dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                        dr["USER_TYPE"] = ConvertUtil.ToInt32(obj["USER_TYPE"]);
                        dr["USER_TYPE2"] = ConvertUtil.ToInt32(obj["USER_TYPE2"]);
                        dr["USER_ID"] = ConvertUtil.ToString(obj["USER_ID"]);
                        dr["USER_NM"] = ConvertUtil.ToString(obj["USER_NM"]);
                        dr["USER_TEL"] = ConvertUtil.ToString(obj["USER_TEL"]);
                        dr["USER_HP"] = ConvertUtil.ToString(obj["USER_HP"]);
                        
                        dr["PURCHASE_COST"] = ConvertUtil.ToInt64(obj["PURCHASE_COST"]);
                        dr["FAULT_COST"] = ConvertUtil.ToInt64(obj["FAULT_COST"]);
                        dr["USER_COST"] = ConvertUtil.ToInt64(obj["PURCHASE_COST"]) - ConvertUtil.ToInt64(obj["FAULT_COST"]);
                        dr["ADJUSTMENT_STATE"] = ConvertUtil.ToString(obj["ADJUSTMENT_STATE"]);
                        dr["COMPLETE_DT"] = ConvertUtil.ToDateTimeNull(obj["COMPLETE_DT"]);
                        dr["USEDPURCHASE_STATE"] = ConvertUtil.ToString(obj["USEDPURCHASE_STATE"]);
                        dr["RECEIPT_REPORT"] = "[접수증]";

                        dr["CHECK_STATE"] = ConvertUtil.ToString(obj["CHECK_STATE"]);
                        dr["RETURN_STATE"] = ConvertUtil.ToString(obj["RETURN_STATE"]);
                        dr["ETC"] = obj["ETC"];
                        dr["DT"] = obj["RECEIPT_DT"];

                        _dtUsedPurchase.Rows.Add(dr);
                    }
                }

               
                //DataView dv = new DataView(_dtUsedPurchase);
                //dv.Sort = "DT DESC";
                //_dtUsedPurchase = dv.ToTable();

                _dtUsedPurchase.EndInit();
                gvUsedPurchaseList.EndDataUpdate();
                
                return true;

            }
            else
            {
                _dtUsedPurchase.EndInit();
                gvUsedPurchaseList.EndDataUpdate();
                return false;
            }
        }

        private bool checkSearch(object registNo,
            object barcode,
            string state,
            string dtFrom,
            string dtTo,
            string registDt,
            string releaseDt,
            string warehousingDt,
            object releaseWarehouseNo,
            object warehousingWarehouse)
        {

            if(string.IsNullOrEmpty(ConvertUtil.ToString(registNo)) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(barcode))&&
                string.IsNullOrEmpty(state) &&
                string.IsNullOrEmpty(dtFrom) &&
                string.IsNullOrEmpty(dtTo) &&
                string.IsNullOrEmpty(registDt) &&
                string.IsNullOrEmpty(releaseDt) &&
                string.IsNullOrEmpty(warehousingDt) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(releaseWarehouseNo)) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(warehousingWarehouse)) 
                )
            {
                return false;
            }

            return true;
        }

        private void gvUsedPurchaseList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0 && e.Column.FieldName == "USEDPURCHASE_STATE")
            {
                string state = ConvertUtil.ToString(View.GetRowCellValue(e.RowHandle, View.Columns["USEDPURCHASE_STATE"]));

                e.Appearance.BackColor = _dicStateColor[state];
            }
        }

        private void gvUsedPurchaseList_MouseMove(object sender, MouseEventArgs e)
        {
            //GridHitInfo calcHintInfo = gvUsedPurchaseList.CalcHitInfo(gvUsedPurchaseList.FocusedRowHandle);
        }

        private void gcUsedPurchaseList_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}