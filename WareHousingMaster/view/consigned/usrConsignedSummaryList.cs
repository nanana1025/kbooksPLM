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
using System.Collections;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedSummaryList : DevExpress.XtraEditors.XtraForm
    {


        string _componentCd;

        DataRowView _currentReceipt;


        DataTable _dtReceipt;

        DataTable _dtReceiptModel;

        BindingSource _bsReceipt;
       
        long _proxyId;
        long _id;

        int _currnetPartCnt=0;

        Dictionary<long, string> _dicModelInfo;

  
        public usrConsignedSummaryList()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM1", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM2", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("WORKER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PACKAGE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RETURN_YN", typeof(int)));

            _dtReceiptModel = new DataTable();
            _dtReceiptModel.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptModel.Columns.Add(new DataColumn("MODEL_ID", typeof(long)));
            _dtReceiptModel.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceiptModel.Columns.Add(new DataColumn("MODEL_CNT", typeof(int)));

            _bsReceipt = new BindingSource();

            _dicModelInfo = new Dictionary<long, string>();

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

            
            //DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            //Util.insertRowonTop(dtCompany, "-1", "없음");
            //Util.LookupEditHelper(leComapny, dtCompany, "KEY", "VALUE");

            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(rileUser, dtUserId, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "선택안함");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;

            leCompany.EditValue = "-1";

            if(ProjectInfo._userType.Equals("E"))
            {
                leCompany.EditValue = ProjectInfo._userCompanyId.ToString();

                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCompanyId.Visible = false;

                lcgReceipt.CustomHeaderButtons[2].Properties.Visible = false;
                lcgReceipt.CustomHeaderButtons[3].Properties.Visible = false;
            }
        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsReceipt;

           
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

            if (DBConsigned.searchReceiptList(jData, ref jResult))
            {
                
                gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
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
                    dr["RECEIPT_DT"] = obj["RECEIPT_DTS"];
                    dr["MODEL_ID"] = obj["MODEL_ID"];
                    dr["MODEL_NM_DETAIL"] = obj["MODEL_NM_DETAIL"];
                    dr["COMPANY_ID"] = obj["COMPANY_ID"];
                    dr["REQUEST"] = obj["REQUEST"];

                    dr["CUSTOMER_NM1"] = obj["CUSTOMER_NM"];
                    dr["CUSTOMER_NM2"] = obj["CUSTOMER_NM"];
                    dr["WORKER_ID"] = obj["WORKER_ID"];
                    dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                    dr["PACKAGE_ID"] = obj["PACKAGE_ID"];
                    dr["COMPLETE_ID"] = obj["COMPLETE_ID"];
                    dr["RETURN_YN"] = ConvertUtil.ToInt32(obj["RETURN_YN"]);
                    
                    dr["INVOICE"] = obj["INVOICE"];

                    _dtReceipt.Rows.Add(dr);
                }

                gvReceipt.EndDataUpdate();

                gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
                gvReceipt.FocusedRowHandle = -2147483646;
                gvReceipt.MoveFirst();

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


        private void Assign()
        {
            int[] selectedRowHandles = gvReceipt.GetSelectedRows();

            if (selectedRowHandles.Count() < 0)
                Dangol.Message("선택된 접수가 없습니다.");
            else
            {
                if (Dangol.MessageYN("선택한 접수를 할당하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    ArrayList rows = new ArrayList();
                    List<long> listProxy = new List<long>();

                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                            rows.Add(gvReceipt.GetDataRow(selectedRowHandle));
                    }
                    try
                    {
                        gvReceipt.BeginUpdate();

                        for (int i = 0; i < rows.Count; i++)
                        {
                            DataRow row = rows[i] as DataRow;
                            // Change the field value.
                            listProxy.Add(ConvertUtil.ToInt64(row["PROXY_ID"]));
                        }

                        if (DBConsigned.assignReceipt(listProxy, ref jResult))
                        {
                            int proxtyState = -1;
                            for (int i = 0; i < rows.Count; i++)
                            {
                                DataRow row = rows[i] as DataRow;
                                row["WORKER_ID"] = ProjectInfo._userId;
                                proxtyState = ConvertUtil.ToInt32(row["PROXY_STATE"]);

                                //if (proxtyState == 0)
                                //    row["PROXY_STATE"] = 0;
                            }  
                        }
                    }
                    finally
                    {
                        gvReceipt.EndUpdate();
                        Dangol.Message("처리되었습니다.");
                    }

                    

                }
            }
        }

        private void unAssign()
        {
            int[] selectedRowHandles = gvReceipt.GetSelectedRows();

            if (selectedRowHandles.Count() < 0)
                Dangol.Message("선택된 접수가 없습니다.");
            else
            {
                if (Dangol.MessageYN("선택한 접수의 할당을 취소하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    ArrayList rows = new ArrayList();
                    List<long> listProxy = new List<long>();

                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                            rows.Add(gvReceipt.GetDataRow(selectedRowHandle));
                    }
                    try
                    {
                        gvReceipt.BeginUpdate();

                        for (int i = 0; i < rows.Count; i++)
                        {
                            DataRow row = rows[i] as DataRow;
                            // Change the field value.
                            listProxy.Add(ConvertUtil.ToInt64(row["PROXY_ID"]));
                        }

                        if (DBConsigned.unAssignReceipt(listProxy, ref jResult))
                        {
                            for (int i = 0; i < rows.Count; i++)
                            {
                                DataRow row = rows[i] as DataRow;
                                row["WORKER_ID"] = "";
                            } 
                        }
                    }
                    finally
                    {
                        gvReceipt.EndUpdate();
                        Dangol.Message("처리되었습니다.");
                    }

                }
            }
        }

        private void lcgReceipt_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                getModelListStatistics();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                getTodayModelListStatistics();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                Assign();
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                unAssign();
            }
            else if (e.Button.Properties.Tag.Equals(8))
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
        }

        private void gcReceip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                int focusedRowHandle = gvReceipt.FocusedRowHandle;
                int topRowIndex = gvReceipt.TopRowIndex;
                getReceiptList(false);

                gvReceipt.FocusedRowHandle = focusedRowHandle;
                gvReceipt.TopRowIndex = topRowIndex;
            }
        }

        private void getModelListStatistics()
        {
            _dtReceiptModel.Clear();
            _dicModelInfo.Clear();

            DataTable dtModel = Util.getCodeListCustom("TN_MODEL_LIST", "MODEL_LIST_ID", "MODEL_NM", $"DEL_YN != 'Y'", "MODEL_LIST_ID ASC");
            
            long modelId;
            long returnYn;

            Dictionary<long, int> dicModelCnt = new Dictionary<long, int>();
            List<long> listModelId = new List<long>();

            foreach (DataRow row in dtModel.Rows)
            {
                modelId = ConvertUtil.ToInt64(row["KEY"]);
                if (!_dicModelInfo.ContainsKey(modelId))
                    _dicModelInfo.Add(modelId, ConvertUtil.ToString(row["VALUE"]));
            }

            ArrayList rows = new ArrayList();
            for (int i = 0; i < gvReceipt.DataRowCount; i++)
            {
                int rowHandle = gvReceipt.GetVisibleRowHandle(i);
                rows.Add(gvReceipt.GetDataRow(rowHandle));
            }

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                modelId = ConvertUtil.ToInt64(row["MODEL_ID"]);
                returnYn = ConvertUtil.ToInt32(row["RETURN_YN"]);

                if (returnYn == 0)
                {
                    if (modelId == -1)
                    {
                        DataRow dr = _dtReceiptModel.NewRow();
                        dr["MODEL_ID"] = -1;
                        dr["MODEL_NM"] = row["MODEL_NM_DETAIL"];
                        dr["MODEL_CNT"] = 1;
                        _dtReceiptModel.Rows.Add(dr);
                    }
                    else
                    {
                        if (listModelId.Contains(modelId))
                            dicModelCnt[modelId]++;
                        else
                        {
                            listModelId.Add(modelId);
                            dicModelCnt.Add(modelId, 1);
                        }
                    }
                }
            }

            foreach(long id in listModelId)
            {
                if(_dicModelInfo.ContainsKey(id))
                {
                    DataRow dr = _dtReceiptModel.NewRow();
                    dr["MODEL_ID"] = id;
                    dr["MODEL_NM"] = _dicModelInfo[id];
                    dr["MODEL_CNT"] = dicModelCnt[id];
                    _dtReceiptModel.Rows.Add(dr);
                }
            }

            using (DlgReceiptModelList dlgReceiptModelList = new DlgReceiptModelList(_dtReceiptModel))
            {
                dlgReceiptModelList.ShowDialog();

                if (dlgReceiptModelList.DialogResult == DialogResult.OK)
                {

                }
            }


        }

        private void getTodayModelListStatistics()
        {
            _dtReceiptModel.Clear();
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            var today = DateTime.Today;
            string day = today.ToString("yyyy-MM-dd");

            jobj.Add("RECEIPT_DT_S", $"{day} 00:00:00");
            jobj.Add("RECEIPT_DT_E", $"{day} 23:59:59");
            jobj.Add("RETURN_YN", 0);

            if (DBConsigned.getReceiptModelStatistics(jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtReceiptModel.NewRow();
                        dr["MODEL_ID"] = obj["MODEL_ID"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["MODEL_CNT"] = obj["MODEL_CNT"];
                        _dtReceiptModel.Rows.Add(dr);
                    }

                    using (DlgReceiptModelList dlgReceiptModelList = new DlgReceiptModelList(_dtReceiptModel))
                    {
                        dlgReceiptModelList.ShowDialog();

                        if (dlgReceiptModelList.DialogResult == DialogResult.OK)
                        {

                        }
                    }
                }
                else
                {
                    Dangol.Message("금일 접수건이 없습니다.");
                }
            }

        }
    }
}