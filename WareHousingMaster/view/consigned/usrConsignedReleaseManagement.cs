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
using ScreenCopy;
using WareHousingMaster.UtilTest;
using System.IO;

namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedReleaseManagement : DevExpress.XtraEditors.XtraForm
    {


        string _componentCd;
        DataRowView _currentReceipt;
        DataTable _dtReceipt;    
        BindingSource _bsReceipt;

        DataTable _dtReceiptPart;
        BindingSource _bsReceiptPart;

        DataTable _dtReceiptExam;
        BindingSource _bsReceiptExam;

        DataTable _dtReceiptRelease;
        BindingSource _bsReceiptRelease;

        Dictionary<string, string> _dicDeliveryCompany;

        int _proxyState;
        long _proxyId;
        long _id;
        long _partPrice = 0;

        int _currnetPartCnt=0;

        public usrConsignedReleaseManagement()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CAPTURE1_YN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CAPTURE2_YN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("IMAGE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("WORKER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PACKAGE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE", typeof(string)));


            _dtReceiptPart = new DataTable();
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("PART_NM", typeof(string)));

            _dtReceiptExam = new DataTable();
            _dtReceiptExam.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptExam.Columns.Add(new DataColumn("PART_NM", typeof(string)));

            _dtReceiptRelease = new DataTable();
            _dtReceiptRelease.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptRelease.Columns.Add(new DataColumn("PART_NM", typeof(string)));

            _proxyState = -1;

            _bsReceipt = new BindingSource();
            _bsReceiptPart = new BindingSource();
            _bsReceiptExam = new BindingSource();
            _bsReceiptRelease = new BindingSource();

            _dicDeliveryCompany = new Dictionary<string, string>();


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
            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(rileUser, dtUserId, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            DataTable dtProxyState = Util.getCodeList("CD0902", "KEY", "VALUE");
            Util.LookupEditHelper(rileProxyState, dtProxyState, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;
            _bsReceiptPart.DataSource = _dtReceiptPart;
            _bsReceiptExam.DataSource = _dtReceiptExam;
            _bsReceiptRelease.DataSource = _dtReceiptRelease;
            
        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsReceipt;

            gcPart.DataSource = null;
            gcPart.DataSource = _bsReceiptPart;

            gcExam.DataSource = null;
            gcExam.DataSource = _bsReceiptExam;

            gcRelease.DataSource = null;
            gcRelease.DataSource = _bsReceiptRelease;

        }

        private void gvReceipt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);

            _dtReceiptPart.Clear();
            _dtReceiptExam.Clear();
            _dtReceiptRelease.Clear();

            if (isValidRow)
            {
                _currentReceipt = gvReceipt.GetRow(e.FocusedRowHandle) as DataRowView;
                _proxyId = ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]);
                getReceiptPartList();
            }
            else
            {
                _currentReceipt = null;
                _proxyId = -1;
            }
        }


        private bool getReceiptPartList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("PROXY_ID", _proxyId);

            if (DBConsigned.getReceiptPartList(jData, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["PART_EXIST"]))
                {

                    gvPart.BeginDataUpdate();
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtReceiptPart.NewRow();

                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["PART_NM"] = obj["MODEL_NM"];
                        _dtReceiptPart.Rows.Add(dr);
                    }
                    gvPart.EndDataUpdate();
                }

                if (ConvertUtil.ToBoolean(jResult["EXAM_EXIST"]))
                {

                    gvExam.BeginDataUpdate();
                    JArray jArray = JArray.Parse(jResult["EXAM"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtReceiptExam.NewRow();

                        dr["COMPONENT_CD"] = obj["component_cd"];
                        dr["PART_NM"] = obj["model_nm"];
                        _dtReceiptExam.Rows.Add(dr);
                    }
                    gvExam.EndDataUpdate();
                }

                if (ConvertUtil.ToBoolean(jResult["RELEASE_EXIST"]))
                {

                    gvRelease.BeginDataUpdate();
                    JArray jArray = JArray.Parse(jResult["RELEASE"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtReceiptRelease.NewRow();

                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["PART_NM"] = obj["MODEL_NM"];
                        _dtReceiptRelease.Rows.Add(dr);
                    }
                    gvRelease.EndDataUpdate();
                }

                return true;

            }
            else
            {
                return false;
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
                    //dr["PROXY_STATE"] = obj["PROXY_STATE"];

                    dr["RECEIPT"] = obj["RECEIPT"];
                    dr["RECEIPT_DT"] = obj["RECEIPT_DT"];
                    
                    dr["CAPTURE1_YN"] = obj["CAPTURE1_YN"];
                    dr["CAPTURE2_YN"] = obj["CAPTURE2_YN"];
                    dr["IMAGE"] = "[사진보기]";
                    
                    dr["PROXY_STATE"] = obj["PROXY_STATE"];
                    dr["COMPANY_ID"] = obj["COMPANY_ID"];
                    dr["MODEL_NM_DETAIL"] = obj["MODEL_NM_DETAIL"];
                    dr["CUSTOMER_NM"] = obj["CUSTOMER_NM"];
                    
                    dr["WORKER_ID"] = obj["WORKER_ID"];
                    dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                    dr["PACKAGE_ID"] = obj["PACKAGE_ID"];
                    dr["COMPLETE_ID"] = obj["COMPLETE_ID"];
                    dr["INVOICE"] = ConvertUtil.ToString(obj["INVOICE"]);
                    

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

            //if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
            //{
            //    jData.Add("PROXY_STATE", ConvertUtil.ToInt32(leReceiptState.EditValue));
            //}
            jData.Add("PROXY_RANGE", "2,3,4");

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
            if (e.Column.FieldName == "CAPTURE1_YN")
            {
                if (_currentReceipt == null)
                    Dangol.Warining("선택된 접수가 없습니다.");
                else
                {
                    if (ConvertUtil.ToString(_currentReceipt["CAPTURE1_YN"]).Equals("Y"))
                    {
                        Image image = ScreenCapture.GetCaptureImg("consignedRelease", $"{_currentReceipt["RECEIPT"]}.png");

                        using (DlgImgTest digImgTest = new DlgImgTest(image))
                        {
                            digImgTest.ShowDialog(this);
                            File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_currentReceipt["RECEIPT"]}.png");
                        }
                    }
                    //else
                    //    Dangol.Message("캡쳐한 이미지가 없습니다.");
                }
            }
            else if (e.Column.FieldName == "CAPTURE2_YN")
            {
                if (_currentReceipt == null)
                    Dangol.Warining("선택된 접수가 없습니다.");
                else
                {
                    if (ConvertUtil.ToString(_currentReceipt["CAPTURE2_YN"]).Equals("Y"))
                    {
                        Image image = ScreenCapture.GetCaptureImg("consignedRelease", $"{_currentReceipt["RECEIPT"]}_2.png");

                        using (DlgImgTest digImgTest = new DlgImgTest(image))
                        {
                            digImgTest.ShowDialog(this);
                            File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_currentReceipt["RECEIPT"]}_2.png");
                        }
                    }
                    //else
                    //    Dangol.Message("캡쳐한 이미지가 없습니다.");
                }
            }
            else if (e.Column.FieldName == "IMAGE")
            {
                string invoice = $"{_currentReceipt["INVOICE"]}";

                if(string.IsNullOrWhiteSpace(invoice))
                    Dangol.Warining("송장번호가 없습니다.");
                else
                    ImageInfo.GetImage(3, false, invoice);
            }
        }
    }
}