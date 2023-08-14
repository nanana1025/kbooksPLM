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
using Newtonsoft.Json;
using DevExpress.XtraGrid.Views.Grid;
using WareHousingMaster.Report;
using WareHousingMaster.view.release;
using DevExpress.XtraReports.UI;
using WareHousingMaster.view.consigned;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraGauges.Core.Primitive;
using WareHousingMaster.view.PreView;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Collections;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrCustomerPaymentState : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtUsedPurchase;
        DataTable _dtUsedPurchasePart;

        BindingSource _bsUsedPurchase;

        Dictionary<string, Color> _dicStateColor;

        Dictionary<string, string> _dicUserType1;
        Dictionary<string, string> _dicUserType2;
        



        BindingSource _bsPallet;

        DataTable _dtLocation;
        DataTable _dtPallet;
        DataTable _dtPalletDetail;
        DataTable _dtPalletRelease;
        DataTable _dtPalletWarehousing;
        DataTable _dtBasket;
        DataRowView _currentRow;
        DataRowView _currentRowDetail;
        List<string> _listBarcode;
        List<long> _listInventoryId;
        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        long _warehouseMovementId;
        long _inventoryId;

        bool isSearch = false;
        bool isInit = true;

        bool initialize = true;
        bool initializeEnter = true;

        string _receipt;


        public usrCustomerPaymentState()
        {
            InitializeComponent();

            _dtUsedPurchase = new DataTable();
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_ID", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("NO", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("SOURCE_CD", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TYPE1", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TYPE2", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_NM", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TEL", typeof(string)));

            _dtUsedPurchase.Columns.Add(new DataColumn("BANK_NM", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("ACCOUNT_NO", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("ACCOUNT_OWNER", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("PROCESS_USER", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_STATE", typeof(string)));

            _dtUsedPurchase.Columns.Add(new DataColumn("EXAMINE_DT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("PURCHASE_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("ADJUST_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("CHANGED_COST", typeof(long)));

            _dtUsedPurchase.Columns.Add(new DataColumn("FAULT_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("SUPPORT_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("TOTAL_COST", typeof(long)));

            _dtUsedPurchase.Columns.Add(new DataColumn("PAYMENT_STATE", typeof(int)));

            _dtUsedPurchase.Columns.Add(new DataColumn("PAYMENT_DT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_REPORT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("DT", typeof(long)));


            _dtUsedPurchasePart = new DataTable();
            _dtUsedPurchasePart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtUsedPurchasePart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtUsedPurchasePart.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtUsedPurchasePart.Columns.Add(new DataColumn("PART_CNT", typeof(int)));

            _bsUsedPurchase = new BindingSource();
            _bsPallet = new BindingSource();

            _listBarcode = new List<string>();
            _listInventoryId = new List<long>();
            //_dicWarehouMovementList = new Dictionary<long, Dictionary<long, string>>();

            _dicStateColor = new Dictionary<string, Color>();
            _dicStateColor.Add("-1", Color.Transparent);
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
            for (int i = 0; i < gvCustomerPaymentList.DataRowCount; i++)
            {
                int rowHandle = gvCustomerPaymentList.GetVisibleRowHandle(i);
                rows.Add(gvCustomerPaymentList.GetDataRow(rowHandle));
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

        private void setComplete()
        {

            if (Dangol.MessageYN("최종 비용 정보를 저장하시겠습니까??") == DialogResult.Yes)
            {
                //JObject jResult = new JObject();
                //JObject jData = new JObject();

                //jData.Add("RECEIPT_ID", ConvertUtil.ToInt64(_currentRow["RECEIPT_ID"]));
                //jData.Add("RECEIPT", ConvertUtil.ToString(_currentRow["RECEIPT"]));

                //jData.Add("MONEY_TYPE", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_TYPE1"]));
                //jData.Add("COM_CD", "");
                //jData.Add("CUSTOMER_NO", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]));
                //jData.Add("PICKUP_COM", "D865");
                //jData.Add("F_PURCHASE_COST", ConvertUtil.ToInt64(seFCost.EditValue));

                ////jData.Add("PURCHASE_COST", 0);
                ////jData.Add("ADJUST_COST", ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADJUST_COST"]));

                //jData.Add("F_ADJUST_COST", ConvertUtil.ToInt64(seFAdjustCost.EditValue) * -1);
                //jData.Add("OUT_COST", 0);
                //jData.Add("F_TOTAL_COST", ConvertUtil.ToInt64(seFTotalCost.EditValue));
                //jData.Add("POST_COST", 0);
                //jData.Add("DEPT_COST", 0);
                //jData.Add("DEPT_WON", 0);
                //jData.Add("DEPT_OFF", 0);
                //jData.Add("DEPT_COST_ADD", 0);
                //jData.Add("ROYALTY", 0);
                //jData.Add("ROYALTY_ADD", 0);
                //jData.Add("N_DEPT_TOTAL", 0);
                //jData.Add("M_STATE", ConvertUtil.ToString(rgMsType.EditValue));
                //jData.Add("BIGO", "");


                //if (DBUsedPurchase.updateRecetFinalState(jData, ref jResult))
                //{
                //    if (!ConvertUtil.ToString(rgMsType.EditValue).Equals("1"))
                //    {
                //        if (ConvertUtil.ToString(rgMsType.EditValue).Equals("9"))
                //            setIInitData();
                //        getReceiptDetail();

                //    }
                //    Dangol.Message("처리되었습니다.");
                //}
                //else
                //{
                //    return;
                //}
            }
        }

        private void setInfoBox()
        {
            DataTable dtUserType1 = Util.getCodeList("CD1301", "KEY", "VALUE");
            Util.LookupEditHelper(rileUserType1, dtUserType1, "KEY", "VALUE");

            foreach(DataRow dr in dtUserType1.Rows)
                _dicUserType1.Add(ConvertUtil.ToString(dr["KEY"]), ConvertUtil.ToString(dr["VALUE"]));

            DataTable dtUserType2 = Util.getCodeList("CD1302", "KEY", "VALUE");
            Util.LookupEditHelper(rileUserType2, dtUserType2, "KEY", "VALUE");

            foreach (DataRow dr in dtUserType2.Rows)
                _dicUserType2.Add(ConvertUtil.ToString(dr["KEY"]), ConvertUtil.ToString(dr["VALUE"]));

            DataTable dtUsedPurchaseState = Util.getCodeList("CD1303", "KEY", "VALUE");
            Util.insertRowonTop(dtUsedPurchaseState, "-1", "선택안합");
            Util.LookupEditHelper(rileUsedPurchaseState, dtUsedPurchaseState, "KEY", "VALUE");
            Util.LookupEditHelper(leReceiptState, dtUsedPurchaseState, "KEY", "VALUE");

            DataTable dtUsedAdjustmentState = Util.getCodeList("CD1304", "KEY", "VALUE");
            Util.insertRowonTop(dtUsedAdjustmentState, "-1", "선택안합");
            Util.LookupEditHelper(rileAdjustmentState, dtUsedAdjustmentState, "KEY", "VALUE");
            Util.LookupEditHelper(lePaymentState, dtUsedAdjustmentState, "KEY", "VALUE");

            DataTable dtSource = Util.getCodeList("CD1309", "KEY", "VALUE");
            Util.LookupEditHelper(rileSource, dtSource, "KEY", "VALUE");

            var today = DateTime.Today;
            var pastDate = today.AddDays(-7);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            lePaymentState.ItemIndex = 0;
            leReceiptState.ItemIndex = 0;
        }

        private void setIInitData()
        {
            gcCustomerPaymentList.DataSource = null;
            _bsUsedPurchase.DataSource = _dtUsedPurchase;
            gcCustomerPaymentList.DataSource = _bsUsedPurchase;

            _bsUsedPurchase.Sort = "DT DESC";
        }

        private void gvCustomerPaymentList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvCustomerPaymentList.RowCount > 0);

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

        private void gvCustomerPaymentList_DoubleClick(object sender, EventArgs e)
        {
            bool isValidRow = (_currentRow != null && gvCustomerPaymentList.RowCount > 0);

            if (isValidRow)
            {


            }
        }
        private void gvUsedPurchaseList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (e.Column.FieldName == "RECEIPT_REPORT")
            //{
            //    showReceiptPrint(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"]);
            //}
            //else if (e.Column.FieldName == "NO" || e.Column.FieldName == "RECEIPT" || e.Column.FieldName == "RECEIPT_DT" 
            //    || e.Column.FieldName == "USER_TYPE1" || e.Column.FieldName == "USER_TYPE2"|| e.Column.FieldName == "USER_NM")
            //{
            //    string tabName = "RECEIPT DETAIL";
            //    if (!(ProjectInfo._tabbedView.Documents.Any(x => x.Form.Tag.ToString() == tabName) || ProjectInfo._tabbedView.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            //    {
            //        ProjectInfo._usedPurchaseReceiptDetail = new usrUsedPurchaseReceiptDetail();
            //        ProjectInfo._usedPurchaseReceiptDetail.Tag = tabName;
            //        ProjectInfo._usedPurchaseReceiptDetail.MdiParent = this.MdiParent;
            //        ProjectInfo._usedPurchaseReceiptDetail.setInitData(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"]);
            //        ProjectInfo._usedPurchaseReceiptDetail.Show();
            //        if (!ProjectInfo._ribbonTabs.ContainsKey(ProjectInfo._biusrUsedPurchaseReceiptDetail))
            //            ProjectInfo._ribbonTabs.Add(ProjectInfo._biusrUsedPurchaseReceiptDetail, ProjectInfo._usedPurchaseReceiptDetail);
            //    }
            //    else
            //    {
            //        ProjectInfo._usedPurchaseReceiptDetail.reload(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"]);
            //        ProjectInfo._documentManager.View.ActivateDocument(ProjectInfo._ribbonTabs[ProjectInfo._biusrUsedPurchaseReceiptDetail]);
            //        ProjectInfo._usedPurchaseReceiptDetail.Show();
            //    }





            //}
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

                    if (_dicUserType1.ContainsKey(userType1))
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
                
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcCustomerPaymentList.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                showReceiptPrint(_currentRow["RECEIPT_ID"], _currentRow["RECEIPT"], _currentRow["SOURCE_CD"]);
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

            if (diffDay > 30)
            {
                jData.Add("MSG", "검색 기간은 30일을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("RECEIPT_DT_FROM", dtFrom);
            jData.Add("RECEIPT_DT_TO", dtTo);

            

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teReceip.Text)))
                jData.Add("RECEIPT", teReceip.Text);

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCustomerNm.Text)))
                jData.Add("USER_NM", teCustomerNm.Text);

            if (ConvertUtil.ToInt32(lePaymentState.EditValue) >= 0)
                jData.Add("PAYMENT_STATE", ConvertUtil.ToInt32(lePaymentState.EditValue));

            if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
                jData.Add("RECEIPT_STATE", ConvertUtil.ToInt32(leReceiptState.EditValue));

            return true;
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            //JObject jResult = new JObject();
            Dangol.ShowSplash();
            //getUsedPurchaseList(ref jResult);
            receiptRefresh();
            Dangol.CloseSplash();
        }

        private void receiptRefresh()
        {
            string receipt = _receipt;
            gvCustomerPaymentList.FocusedRowObjectChanged -= gvCustomerPaymentList_FocusedRowObjectChanged;
            JObject jResult = new JObject();
            getUsedPurchaseList(ref jResult);
            gvCustomerPaymentList.FocusedRowObjectChanged += gvCustomerPaymentList_FocusedRowObjectChanged;

            int rowHandle = gvCustomerPaymentList.LocateByValue("RECEIPT", receipt);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvCustomerPaymentList.FocusedRowHandle = -2147483646;
                    gvCustomerPaymentList.FocusedRowHandle = rowHandle;
                }
            }
            else
            {
                if (_dtUsedPurchase.Rows.Count > 0)
                {
                    gvCustomerPaymentList.FocusedRowHandle = -2147483646;
                    gvCustomerPaymentList.MoveFirst();
                }
            }

            ArrayList rows = new ArrayList();
            for (int i = 0; i < gvCustomerPaymentList.DataRowCount; i++)
            {
                rowHandle = gvCustomerPaymentList.GetVisibleRowHandle(i);
                rows.Add(gvCustomerPaymentList.GetDataRow(rowHandle));
            }

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                // Change the field value.
                row["NO"] = i + 1;
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

            _dtUsedPurchase.Clear();

            if (DBUsedPurchase.getUsedPurchasePaymentList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DANAWA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DANAWA_DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtUsedPurchase.NewRow();
                        
                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        //dr["NO"] = index++;
                        dr["RECEIPT"] = obj["RECEIPT"];

                        dr["SOURCE_CD"] = 0;
                        
                        dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                        dr["USER_TYPE1"] = obj["USER_TYPE1"];
                        dr["USER_ID"] = obj["USER_ID"];
                        dr["USER_NM"] = obj["USER_NM"];
                        dr["USER_TEL"] = obj["USER_TEL"];

                        dr["BANK_NM"] = obj["BANK_NM"];
                        dr["ACCOUNT_NO"] = obj["ACCOUNT_NO"];
                        dr["ACCOUNT_OWNER"] = obj["ACCOUNT_OWNER"];
                        dr["PROCESS_USER"] = obj["PROCESS_USER"];
                        dr["RECEIPT_STATE"] = obj["RECEIPT_STATE"];

                        dr["EXAMINE_DT"] = ConvertUtil.ToDateTimeNull(obj["EXAMINE_DT"]);
                        dr["PURCHASE_COST"] = obj["PURCHASE_COST"];
                        dr["ADJUST_COST"] = obj["ADJUST_COST"];
                        dr["CHANGED_COST"] = obj["CHANGED_COST"];
                        dr["FAULT_COST"] = ConvertUtil.ToInt64(obj["FAULT_COST"])*-1;
                        dr["SUPPORT_COST"] = obj["SUPPORT_COST"];
                        dr["TOTAL_COST"] = obj["TOTAL_COST"];

                        dr["PAYMENT_STATE"] = obj["PAYMENT_STATE"];
                        dr["PAYMENT_DT"] = ConvertUtil.ToDateTimeNull(obj["PAYMENT_DT"]);
                        dr["USER_TEL"] = obj["USER_TEL"];
                        dr["RECEIPT_REPORT"] = "[접수증]";
                        dr["DT"] = obj["RECEIPT_DT"];

                        _dtUsedPurchase.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtUsedPurchase.NewRow();

                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        //dr["NO"] = index++;
                        dr["RECEIPT"] = obj["RECEIPT"];
                        dr["SOURCE_CD"] = obj["SOURCE_CD"];
                        dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                        dr["USER_TYPE1"] = obj["USER_TYPE1"];
                        dr["USER_ID"] = obj["USER_ID"];
                        dr["USER_NM"] = obj["USER_NM"];
                        dr["USER_TEL"] = obj["USER_TEL"];

                        dr["BANK_NM"] = obj["BANK_NM"];
                        dr["ACCOUNT_NO"] = obj["ACCOUNT_NO"];
                        dr["ACCOUNT_OWNER"] = obj["ACCOUNT_OWNER"];
                        dr["PROCESS_USER"] = obj["PROCESS_USER"];
                        dr["RECEIPT_STATE"] = obj["RECEIPT_STATE"];

                        dr["EXAMINE_DT"] = ConvertUtil.ToDateTimeNull(obj["EXAMINE_DT"]);
                        dr["PURCHASE_COST"] = obj["PURCHASE_COST"];
                        dr["ADJUST_COST"] = obj["ADJUST_COST"];
                        dr["CHANGED_COST"] = obj["CHANGED_COST"];
                        dr["FAULT_COST"] = ConvertUtil.ToInt64(obj["FAULT_COST"]);
                        dr["SUPPORT_COST"] = obj["SUPPORT_COST"];
                        dr["TOTAL_COST"] = obj["TOTAL_COST"];

                        dr["PAYMENT_STATE"] = obj["PAYMENT_STATE"];
                        dr["PAYMENT_DT"] = ConvertUtil.ToDateTimeNull(obj["PAYMENT_DT"]);
                        dr["USER_TEL"] = obj["USER_TEL"];
                        dr["RECEIPT_REPORT"] = "[접수증]";
                        dr["DT"] = obj["RECEIPT_DT"];

                        _dtUsedPurchase.Rows.Add(dr);
                    }
                }

                return true;

            }
            else
            {
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
            if (e.RowHandle >= 0 && (e.Column.FieldName == "FAULT_COST" || e.Column.FieldName == "ADJUST_COST"))
            {
                long cost = ConvertUtil.ToInt64(View.GetRowCellValue(e.RowHandle, View.Columns[e.Column.FieldName]));

                if (cost < 0)
                    e.Appearance.ForeColor = Color.Red;
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