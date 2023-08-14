using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedInvoiceManagement : DevExpress.XtraEditors.XtraForm
    {


        string _componentCd;

        DataRowView _currentReceipt;

        DataTable _dtReceipt;
       
        BindingSource _bsReceipt;
       
        Dictionary<string, string> _dicDeliveryCompany;

        int _proxyState;
        long _proxyId;
        long _id;
        long _partPrice = 0;

        int _currnetPartCnt=0;

        public usrConsignedInvoiceManagement()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TEL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS_DETAIL", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("DELIVERY_COMPANY", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE", typeof(string)));

            _proxyState = -1;

            _bsReceipt = new BindingSource();
            _dicDeliveryCompany = new Dictionary<string, string>();


        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            var today = DateTime.Today;
            var pastDate = today.AddDays(-7);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            leReceiptState.ItemIndex = 0;

            getReceiptList(true);

        }

        public IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        private void setInfoBox()
        {
            DataTable dtProxyState = new DataTable();
            dtProxyState = Util.getCodeList("CD0902", "KEY", "VALUE");
            Util.LookupEditHelper(rileProxyState, dtProxyState, "KEY", "VALUE");

            Util.insertRowonTop(dtProxyState, "-1", "선택안함");
            Util.LookupEditHelper(leReceiptState, dtProxyState, "KEY", "VALUE");

            DataTable dtProductType = new DataTable();
            dtProductType = Util.getCodeList("CD0903", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductType, dtProductType, "KEY", "VALUE");

            DataTable dtDelivery = Util.getCodeList("CD0904", "KEY", "VALUE");
            Util.insertRowonTop(dtDelivery, "-1", "없음");
            Util.LookupEditHelper(rileDeliveryComapny, dtDelivery, "KEY", "VALUE");

            foreach (DataRow row in dtDelivery.Rows)
                _dicDeliveryCompany.Add(ConvertUtil.ToString(row["VALUE"]), ConvertUtil.ToString(row["KEY"]));

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "없음");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;

            leCompany.EditValue = "-1";

            if (ProjectInfo._userType.Equals("E"))
            {
                leCompany.EditValue = ProjectInfo._userCompanyId.ToString();

                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcImportInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCompanyId.Visible = false;

            }

        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsReceipt;
        }

        private void setColumReadonly(bool flag)
        { 
            gcDeliveryComapny.OptionsColumn.ReadOnly = !flag;
            gcInvoice.OptionsColumn.ReadOnly = !flag;
        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);

            if (isValidRow)
            {
                _currentReceipt = e.Row as DataRowView;

                _proxyState = ConvertUtil.ToInt32(_currentReceipt["PROXY_STATE"]);
            }
            else
            {
                _currentReceipt = null;
            }
        }

       


        private void sbSearch_Click(object sender, EventArgs e)
        {
            getReceiptList(false);
        }

        private bool getReceiptList(bool isinit)
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtReceipt.Clear();

            if (DBConsigned.getReceiptListFull(jData, ref jResult))
            {
                gvReceipt.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceipt.NewRow();

                    dr["NO"] = index++;
                    dr["PROXY_ID"] = obj["PROXY_ID"];
                    dr["PROXY_STATE"] = obj["PROXY_STATE"];

                    dr["RECEIPT"] = obj["RECEIPT"];
                    dr["RECEIPT_DT"] = obj["RECEIPT_DT"];
                    dr["PRODUCT_TYPE"] = obj["PRODUCT_TYPE"];
                    dr["COMPANY_ID"] = obj["COMPANY_ID"];
                    dr["MODEL_NM_DETAIL"] = obj["MODEL_NM_DETAIL"];
                    dr["DES"] = obj["DES"];
                    
                    dr["CUSTOMER_NM"] = obj["CUSTOMER_NM"];
                    dr["TEL"] = obj["TEL"];
                    dr["HP"] = obj["HP"];
                    dr["ADDRESS"] = obj["ADDRESS"];
                    dr["ADDRESS_DETAIL"] = obj["ADDRESS_DETAIL"];
                    dr["DELIVERY_COMPANY"] = obj["DELIVERY_COMPANY"];
                    dr["INVOICE"] = obj["INVOICE"];

                    _dtReceipt.Rows.Add(dr);
                }

                gvReceipt.EndDataUpdate();

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

            int date = 0;
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
            {
                dtFrom = $"{deDtFrom.Text} 00:00:00";
                date++;
            }

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
            {
                dtTo = $"{deDtTo.Text} 23:59:59";
                date++;
            }

            if (date == 2)
            {
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
                    jData.Add("MSG", "검색 기간은 6개월(180일)을 초과할 수 없습니다.");
                    return false;
                }

                jData.Add("RECEIPT_DT_S", dtFrom);
                jData.Add("RECEIPT_DT_E", dtTo);
            }
            else
            {
                date = 0;
            }


            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teReceip.Text)))
            {
                jData.Add("RECEIPT", teReceip.Text);
                date++;
            }

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCustomerNm.Text)))
            {
                jData.Add("CUSTOMER_NM", teCustomerNm.Text);
                date++;
            }

            if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
            {
                jData.Add("PROXY_STATE", ConvertUtil.ToInt32(leReceiptState.EditValue));
            }

            if (!ConvertUtil.ToString(leCompany.EditValue).Equals("-1"))
            {
                jData.Add("COMPANY_ID", ConvertUtil.ToString(leCompany.EditValue));
                date++;
            }


            if (date == 0)
            {
                jData.Add("MSG", "접수일을 선택하지 않은 경우, 접수번호 또는 고객명은 필수로 입력되야 합니다.");
                return false;
            }

            return true;
        }

        private void sbExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog form = new SaveFileDialog())
            {
                form.Filter = "Excel 통합문서|*.xlsx";
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                    gcReceip.ExportToXlsx(form.FileName, options);

                    if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                    {
                        Process.Start(form.FileName);
                    }
                }
            }
        }

        private void sbImport_Click(object sender, EventArgs e)
        {
            using (DlgImportInvoice dlgImportBarcode = new DlgImportInvoice(_dicDeliveryCompany))
            {
                dlgImportBarcode.ShowDialog();

                if (dlgImportBarcode._isSuccess)
                {
                    getReceiptList(false);
                }
            }
        }

        private void sbInvoiceState_Click(object sender, EventArgs e)
        {
            using (DlgConsignedInvoiceState dlgCompanyPartByWarehousing = new DlgConsignedInvoiceState(ConvertUtil.ToInt64(leCompany.EditValue)))
            {
                dlgCompanyPartByWarehousing.ShowDialog(this);
            }
        }
    }
}