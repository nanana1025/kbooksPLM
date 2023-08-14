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
using DevExpress.XtraTreeList.Nodes;
using WareHousingMaster.view.usedPurchase;
using ImportExcel;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace WareHousingMaster.view.consigned
{
    public partial class DlgConsignedInvoiceState : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _currentReceipt;
        DataTable _dtReceipt;
        BindingSource _bsReceipt;

        DataTable _dtInvoiceSate;
        BindingSource _bsInvoiceSatet;
        Dictionary<string, string> _dicDeliveryCompany;

        List<string> _listInvoiceState;

        long _companyId;

        public DlgConsignedInvoiceState(long companyId)
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
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS_DETAIL", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("DELIVERY_COMPANY", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DETAIL", typeof(int)));

            _dtInvoiceSate = new DataTable();
            _dtInvoiceSate.Columns.Add(new DataColumn("KEY", typeof(string)));
            _dtInvoiceSate.Columns.Add(new DataColumn("VALUE", typeof(string)));

            _bsReceipt = new BindingSource();
            _bsInvoiceSatet = new BindingSource();

            _bsInvoiceSatet.DataSource = _dtInvoiceSate;

            _dicDeliveryCompany = new Dictionary<string, string>();
            _listInvoiceState = new List<string>();

            _companyId = companyId;

        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            var today = DateTime.Today;
            var pastDate = today.AddDays(0);

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

            Util.LookupEditHelper(rileInvoiceState, _bsInvoiceSatet, "KEY", "VALUE");

            //foreach (DataRow row in dtDelivery.Rows)
            //    _dicDeliveryCompany.Add(ConvertUtil.ToString(row["VALUE"]), ConvertUtil.ToString(row["KEY"]));

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "없음");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            DataTable dtDetail = new DataTable();
            dtDetail.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtDetail.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtDetail, 0, "");
            Util.insertRowonTop(dtDetail, 1, "[이동]");
            Util.LookupEditHelper(rileDetail, dtDetail, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            // 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;

            leCompany.EditValue = _companyId.ToString();


            if (ProjectInfo._userType.Equals("E"))
            {
                leCompany.EditValue = ProjectInfo._userCompanyId.ToString();

                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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

            Dangol.ShowSplash();
            if (DBConsigned.getReceiptInvoiceState(jData, ref jResult))
            {
                _listInvoiceState.Clear();
                _dtInvoiceSate.Clear();

                gvReceipt.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                string invoiceState;
                string invoice;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceipt.NewRow();

                    invoiceState = ConvertUtil.ToString(obj["INVOICE_STATE"]);
                    invoice = ConvertUtil.ToString(obj["INVOICE"]);

                    if (!_listInvoiceState.Contains(invoiceState))
                        _listInvoiceState.Add(invoiceState);

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
                    dr["ADDRESS"] = obj["ADDRESS"];
                    dr["ADDRESS_DETAIL"] = obj["ADDRESS_DETAIL"];
                    dr["DELIVERY_COMPANY"] = obj["DELIVERY_COMPANY"];
                    dr["INVOICE"] = invoice;
                    dr["INVOICE_STATE"] = invoiceState;

                    if(string.IsNullOrWhiteSpace(invoice))
                        dr["DETAIL"] = 0;
                    else
                        dr["DETAIL"] = 1;

                    _dtReceipt.Rows.Add(dr);
                }

                rileInvoiceState.BeginUpdate();
                foreach (string state in _listInvoiceState)
                {
                    DataRow dr = _dtInvoiceSate.NewRow();
                    dr["KEY"] = state;
                    dr["VALUE"] = state;

                    _dtInvoiceSate.Rows.Add(dr);
                }
                rileInvoiceState.EndUpdate();


                gvReceipt.EndDataUpdate();

                Dangol.CloseSplash();
                return true;

            }
            else
            {
                Dangol.CloseSplash();
                return false;
            }
        }

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("INVOICE_STATE", true);

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

        private void gvReceipt_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "DETAIL" && ConvertUtil.ToInt32(_currentReceipt["DETAIL"]) == 1)
            {
                string url = "";
                string deliveryComapny = ConvertUtil.ToString(_currentReceipt["DELIVERY_COMPANY"]);
                string invoice = ConvertUtil.ToString(_currentReceipt["INVOICE"]);

                if (deliveryComapny.Equals("1"))  // CJ대한통운
                    url = "https://www.doortodoor.co.kr/parcel/doortodoor.do?fsp_action=PARC_ACT_002&fsp_cmd=retrieveInvNoACT&invc_no=" + invoice;
                else if (deliveryComapny.Equals("2"))  // 한진택배
                    url = "https://www.doortodoor.co.kr/tracking/jsp/cmn/Tracking_new.jsp?QueryType=3&pTdNo=" + invoice + "&pOrderNo=&pTelNo=&pFromDate=&pToDate=&pCustId=&pageno=1&rcv_cnt=10";
                else if (deliveryComapny.Equals("3"))  // 현대로지스틱스 (롯데택배)
                    url = "https://www.lotteglogis.com/home/reservation/tracking/linkView?InvNo=" + invoice;
                else if (deliveryComapny.Equals("4"))  // 우체국
                    url = "https://service.epost.go.kr/trace.RetrieveDomRigiTraceList.comm?sid1=" + invoice + "&displayHeader=N";
                else if (deliveryComapny.Equals("5"))  // 로젠택배
                    url = "https://www.ilogen.com/web/personal/trace/" + invoice;

                System.Diagnostics.Process.Start(url);
            }
        }
    }
}