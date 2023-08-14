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
using System.Text.RegularExpressions;
using ScreenCopy;
using System.IO;
using WareHousingMaster.UtilTest;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ButtonPanel;

namespace WareHousingMaster.view.release.External
{
    public partial class usrTechnicalRequest_Unused : DevExpress.XtraEditors.XtraForm
    {
        int _viewCategory = (int)Enum.EnumView.Adjustment;
        int _viewType = (int)Enum.EnumTableAdjustment.ExamList;

        DataTable _dtReceipt;
        DataTable _dtMsg;
        

        BindingSource _bsReceipt;
        BindingSource _bsMsg;

        DataRowView _currentRow1 = null;
        DataRowView _currentRow = null;
        DataRowView _currentMsgRow = null;

        List<string> _listReceiver;
        //List<long> _listExportId;

        List<int> _listAllowReceiptState;
        List<string> _listHideCol;

        List<string> _listMasterCol;
        List<string> _listReadOnlyCol;

        List<string> _lisDefaultHideCol;

        long _receiptId;
        long _msgId;
        string _barcode;
        string _receipt;
        int _receiptState;

        Regex regex1;
        Regex regex2;
        Regex regex3;
        Regex regex4;

        bool initialize = true;


        public usrTechnicalRequest_Unused()
        {
            InitializeComponent();

            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_USER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("EXPORT_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("EXPORT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST_TYPE_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CHARGE_USER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PROCESS_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PROCESS_DES", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ARRIVAL_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MBD_MANUFACT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CPU_INFO", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MON_INFO", typeof(string)));


            _dtMsg = new DataTable();

            _dtMsg.Columns.Add(new DataColumn("NO", typeof(int)));
            
            _dtMsg.Columns.Add(new DataColumn("MESSAGE_ID", typeof(long)));
            _dtMsg.Columns.Add(new DataColumn("FILE_ID", typeof(long)));
            _dtMsg.Columns.Add(new DataColumn("STATE", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("RECEIPT_ID", typeof(long)));
            _dtMsg.Columns.Add(new DataColumn("DEPT_CD", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("RECEIVER_USER_ID", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("CHARGE_USER_ID", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("TITLE", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("SEND_DT", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("RECEIVE_DT", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("PROCESS_TYPE", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("PROCESS_DES", typeof(string)));
            _dtMsg.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _bsReceipt = new BindingSource();
            _bsMsg = new BindingSource();

            //_listExportId = new List<long>();
            _listReceiver = new List<string>();
            //_listReceiver.Add("shlee");

            _listAllowReceiptState = new List<int>(new int[] {1,2,4 });

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

            regex4 = new Regex(@"^E[0-9]{9}$");

        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

        
            teReceipt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT", false, DataSourceUpdateMode.Never));
            leProductType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "PRODUCT_TYPE", false, DataSourceUpdateMode.Never));
            deReceiptDt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT_DT", false, DataSourceUpdateMode.Never));
            leCompanyId1.DataBindings.Add(new Binding("EditValue", _bsReceipt, "COMPANY_ID", false, DataSourceUpdateMode.Never));
            leReceiptUserId.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RECEIPT_USER_ID", false, DataSourceUpdateMode.Never));
            teExport.DataBindings.Add(new Binding("Text", _bsReceipt, "EXPORT", false, DataSourceUpdateMode.Never));
            teBarcode.DataBindings.Add(new Binding("Text", _bsReceipt, "BARCODE", false, DataSourceUpdateMode.Never));
            leModelId.DataBindings.Add(new Binding("EditValue", _bsReceipt, "MODEL_ID", false, DataSourceUpdateMode.Never));
            leRequestType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "REQUEST_TYPE", false, DataSourceUpdateMode.Never));
            leRequestTypeDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "REQUEST_TYPE_DETAIL", false, DataSourceUpdateMode.Never));
            meDes.DataBindings.Add(new Binding("Text", _bsReceipt, "DES", false, DataSourceUpdateMode.Never));

            rgProcess.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RECEIPT_STATE", false, DataSourceUpdateMode.OnPropertyChanged));

            teShiipingDt.DataBindings.Add(new Binding("Text", _bsReceipt, "RELEASE_DT", false, DataSourceUpdateMode.Never));
            teArrivalDt.DataBindings.Add(new Binding("Text", _bsReceipt, "ARRIVAL_DT", false, DataSourceUpdateMode.Never));
            teBrand.DataBindings.Add(new Binding("Text", _bsReceipt, "MBD_MANUFACT", false, DataSourceUpdateMode.Never));
            teModel1.DataBindings.Add(new Binding("Text", _bsReceipt, "PRODUCT_NAME", false, DataSourceUpdateMode.Never));
            teModel2.DataBindings.Add(new Binding("Text", _bsReceipt, "MBD_MODEL_NM", false, DataSourceUpdateMode.Never));
            teProcessor.DataBindings.Add(new Binding("Text", _bsReceipt, "CPU_INFO", false, DataSourceUpdateMode.Never));
            teScreen.DataBindings.Add(new Binding("Text", _bsReceipt, "MON_INFO", false, DataSourceUpdateMode.Never));

            leChargeUserId.DataBindings.Add(new Binding("EditValue", _bsMsg, "CHARGE_USER_ID", false, DataSourceUpdateMode.Never));
            leProcessType.DataBindings.Add(new Binding("EditValue", _bsMsg, "PROCESS_TYPE", false, DataSourceUpdateMode.Never));
            meProcessDes.DataBindings.Add(new Binding("Text", _bsMsg, "PROCESS_DES", false, DataSourceUpdateMode.Never));

            getExportList();

            //getUsedPurchaseList(ref jResult);

            initialize = false;
        }

        private void setInfoBox()
        {
            DataTable dtProductType = new DataTable();

            dtProductType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtProductType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtProductType, 1, "Product");
            Util.insertRowonTop(dtProductType, 2, "Part");
            Util.LookupEditHelper(leProductType, dtProductType, "KEY", "VALUE");

            DataTable dtProcessState = Util.getCodeList("CD0825", "KEY", "VALUE");
            Util.insertRowonTop(dtProcessState, "-1", "SELECT ALL");
            Util.LookupEditHelper(rileReceiptState, dtProcessState, "KEY", "VALUE");
            Util.LookupEditHelper(leReceiptState, dtProcessState, "KEY", "VALUE");
       
            DataTable dtRequestType = Util.getCodeList("CD0822", "KEY", "VALUE");
            Util.LookupEditHelper(leRequestType, dtRequestType, "KEY", "VALUE");

            DataTable dtRequestTypeDetail = Util.getCodeList("CD0823", "KEY", "VALUE");
            Util.LookupEditHelper(leRequestTypeDetail, dtRequestTypeDetail, "KEY", "VALUE");

            DataTable dtProcessType = Util.getCodeList("CD0824", "KEY", "VALUE");
            Util.LookupEditHelper(leProcessType, dtProcessType, "KEY", "VALUE");

            DataTable dtModelId = Util.getCodeList("CD0826", "KEY", "VALUE");
            Util.LookupEditHelper(leModelId, dtModelId, "KEY", "VALUE");

            DataTable dtProcessState1 = Util.getCodeList("CD0827", "KEY", "VALUE");
            Util.LookupEditHelper(rileProcessState, dtProcessState1, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "SELECT ALL");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompanyId1, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId1, dtCompany, "KEY", "VALUE");


            DataTable dtCompanyUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", $"COMPANY_ID = '2'", "USER_ID ASC");
            Util.LookupEditHelper(leChargeUserId, dtCompanyUserId, "KEY", "VALUE");

            Util.LookupEditHelper(leReceiptUserId, ProjectInfo._dtUserId, "KEY", "VALUE");
            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");
            Util.LookupEditHelper(rileUserId1, ProjectInfo._dtUserId, "KEY", "VALUE");

            DataTable dtDeptCd = Util.getCodeList("CD0502", "KEY", "VALUE");
            Util.LookupEditHelper(rileDeptNm, dtDeptCd, "KEY", "VALUE");
            

            dtProcessState = Util.getCodeList("CD0825", "KEY", "VALUE");

            RadioGroupItem[] rgProcessState = new RadioGroupItem[dtProcessState.Rows.Count]; 
            int indexProcess = 0;
    
            for (int i = 0; i < dtProcessState.Rows.Count; i++)
            {
                RadioGroupItem rgItem = new RadioGroupItem(dtProcessState.Rows[i]["KEY"], ConvertUtil.ToString(dtProcessState.Rows[i]["VALUE"]), true, dtProcessState.Rows[i]["KEY"]);
                //_dicProxyState.Add(ConvertUtil.ToString(dtProxyState.Rows[i]["KEY"]), ConvertUtil.ToString(dtProxyState.Rows[i]["VALUE"]));
                rgProcessState[indexProcess++] = rgItem;
              
            }
            this.rgProcess.Properties.Items.AddRange(rgProcessState);
        }

        private void setIInitData()
        {
            //gvList.AutoGenerateColumns = false;

            gcReceipt.DataSource = null;
            _bsReceipt.DataSource = _dtReceipt;
            gcReceipt.DataSource = _bsReceipt;

            gcMessage.DataSource = null;
            _bsMsg.DataSource = _dtMsg;
            gcMessage.DataSource = _bsMsg;




            var today = DateTime.Today;
            var pastDate = today.AddDays(-30);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

            leReceiptState.EditValue = "-1";

            if (ProjectInfo._userCompanyId == 2)
                leCompany.EditValue = "-1";
            else
                leCompany.EditValue = ConvertUtil.ToString(ProjectInfo._userCompanyId);

            if (ProjectInfo._userCompanyId == 2)
            {
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                gcCompany.Visible = true;

                for (int i = 0; i < lcgMessage.CustomHeaderButtons.Count; i++)
                    lcgMessage.CustomHeaderButtons[i].Properties.Visible = true;
            }
            else
            {
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCompany.Visible = false;

                for (int i = 0; i < lcgMessage.CustomHeaderButtons.Count; i++)
                    lcgMessage.CustomHeaderButtons[i].Properties.Visible = false;
            }
        }

        private void setMsgState(bool flag)
        {
            if (flag)
            {
                int state = ConvertUtil.ToInt32(_currentMsgRow["STATE"]);

                if (state == 1)
                {
                    if (ConvertUtil.ToString(_currentMsgRow["CHARGE_USER_ID"]).Equals(ProjectInfo._userId))
                    {
                        JObject jResult = new JObject();
                        JObject jobj = new JObject();
                        jobj.Add("MESSAGE_ID", _msgId);
                        jobj.Add("RECEIVE_DT", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                        jobj.Add("STATE", "2");

                        if (DBRelease.updateMsgInfo(jobj, ref jResult))
                        {
                            _currentMsgRow.BeginEdit();
                            _currentMsgRow["STATE"] = jobj["STATE"];
                            _currentMsgRow["RECEIVE_DT"] = DateTime.UtcNow.ToString("yyyy-MM-dd");
                            _currentMsgRow.EndEdit();
                        }


                        for (int i = 0; i < lcgProcessInfo.CustomHeaderButtons.Count; i++)
                            lcgProcessInfo.CustomHeaderButtons[i].Properties.Enabled = true;

                        lcgProcessInfo.CustomHeaderButtons[1].Properties.Visible = true;
                        lcgProcessInfo.CustomHeaderButtons[2].Properties.Visible = false;

                        sbAttachedFile.Enabled = true;
                    }
                    else
                    {
                        for (int i = 0; i < lcgProcessInfo.CustomHeaderButtons.Count; i++)
                            lcgProcessInfo.CustomHeaderButtons[i].Properties.Enabled = false;

                        sbAttachedFile.Enabled = false;
                    }
                }
                else if (state == 2)
                {
                    if (ConvertUtil.ToString(_currentMsgRow["CHARGE_USER_ID"]).Equals(ProjectInfo._userId))
                    {
                        for (int i = 0; i < lcgProcessInfo.CustomHeaderButtons.Count; i++)
                            lcgProcessInfo.CustomHeaderButtons[i].Properties.Enabled = true;

                        lcgProcessInfo.CustomHeaderButtons[1].Properties.Visible = true;
                        lcgProcessInfo.CustomHeaderButtons[2].Properties.Visible = false;

                        sbAttachedFile.Enabled = true;
                    }
                    else
                    {
                        for (int i = 0; i < lcgProcessInfo.CustomHeaderButtons.Count; i++)
                            lcgProcessInfo.CustomHeaderButtons[i].Properties.Enabled = false;

                        sbAttachedFile.Enabled = false;
                    }
                }
                else if (state == 3)
                {
                    sbAttachedFile.Enabled = false;

                    if (ConvertUtil.ToString(_currentMsgRow["CHARGE_USER_ID"]).Equals(ProjectInfo._userId))
                    {
                        for (int i = 0; i < lcgProcessInfo.CustomHeaderButtons.Count; i++)
                            lcgProcessInfo.CustomHeaderButtons[i].Properties.Enabled = true;

                        lcgProcessInfo.CustomHeaderButtons[0].Properties.Enabled = false;
                        lcgProcessInfo.CustomHeaderButtons[1].Properties.Visible = false;
                        lcgProcessInfo.CustomHeaderButtons[2].Properties.Visible = true;
                    }
                    else
                    {
                        for (int i = 0; i < lcgProcessInfo.CustomHeaderButtons.Count; i++)
                            lcgProcessInfo.CustomHeaderButtons[i].Properties.Enabled = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < lcgProcessInfo.CustomHeaderButtons.Count; i++)
                    lcgProcessInfo.CustomHeaderButtons[i].Properties.Enabled = false;

                sbAttachedFile.Enabled = false;
            }
        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);

            _dtMsg.Clear();
            _listReceiver.Clear();
           
            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _receipt = ConvertUtil.ToString(_currentRow["RECEIPT"]);
                _receiptId = ConvertUtil.ToInt64(_currentRow["RECEIPT_ID"]);
                _receiptState = ConvertUtil.ToInt32(_currentRow["RECEIPT_STATE"]);

                if (!initialize)
                    Dangol.ShowSplash();

                getMsgList();

                if (!initialize)
                    Dangol.CloseSplash();
               
            }
            else
            {
                _currentRow = null;
                _receipt = "";
                _receiptId = -1;
                _receiptState = -1;
            }
        }

        private void gvReceipt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvReceipt.GetRow(e.FocusedRowHandle) as DataRowView;
                _receipt = ConvertUtil.ToString(_currentRow["RECEIPT"]);
                _receiptId = ConvertUtil.ToInt64(_currentRow["RECEIPT_ID"]);
                _receiptState = ConvertUtil.ToInt32(_currentRow["RECEIPT_STATE"]);
            }
            else
            {
                _receipt = "";
                _receiptId = -1;
                _receiptState = -1;
                _currentRow = null;
            }
        }


        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            //bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            //if (isValidRow)
            //{
            //    _currentRow1 = e.Row as DataRowView;

            //    _barcode = ConvertUtil.ToString(_currentRow1["BARCODE"]);

            //    if(ConvertUtil.ToString(_currentRow1["PART_STATE"]).Equals("E"))
            //    {
            //        //gcSpareParts.OptionsColumn.ReadOnly = false;
            //        //gcEtc.OptionsColumn.ReadOnly = false;
            //        //gcQty.OptionsColumn.ReadOnly = false;
            //    }
            //    else
            //    {
            //        //gcSpareParts.OptionsColumn.ReadOnly = true;
            //        //gcEtc.OptionsColumn.ReadOnly = true;
            //        //gcQty.OptionsColumn.ReadOnly = true;
            //    }
            //}
            //else
            //{
            //    _currentRow1 = null;
            //    _barcode = "";
            //    //gcSpareParts.OptionsColumn.ReadOnly = true;
            //    //gcEtc.OptionsColumn.ReadOnly = true;
            //    //gcQty.OptionsColumn.ReadOnly = true;
            //}
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            //if (isValidRow)
            //{
            //    _currentRow1 = gvList.GetRow(e.FocusedRowHandle) as DataRowView;

            //    _barcode = ConvertUtil.ToString(_currentRow1["BARCODE"]);

            //    if (ConvertUtil.ToString(_currentRow1["PART_STATE"]).Equals("E"))
            //    {
            //        gcSpareParts.OptionsColumn.ReadOnly = false;
            //        gcEtc.OptionsColumn.ReadOnly = false;
            //        gcQty.OptionsColumn.ReadOnly = false;
            //    }
            //    else
            //    {
            //        gcSpareParts.OptionsColumn.ReadOnly = true;
            //        gcEtc.OptionsColumn.ReadOnly = true;
            //        gcQty.OptionsColumn.ReadOnly = true;
            //    }

            //}
            //else
            //{
            //    _currentRow1 = null;
            //    _barcode = "";
            //    gcSpareParts.OptionsColumn.ReadOnly = true;
            //    gcEtc.OptionsColumn.ReadOnly = true;
            //    gcQty.OptionsColumn.ReadOnly = true;
            //}
        }

        private void gvMessage_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvMessage.RowCount > 0);

            if (isValidRow)
            {
                _currentMsgRow = e.Row as DataRowView;
                _msgId = ConvertUtil.ToInt64(_currentMsgRow["MESSAGE_ID"]);
                if (_listAllowReceiptState.Contains(_receiptState))
                    setMsgState(true);
                else
                    setMsgState(false);
            }
            else
            {
                _currentMsgRow = null;
                setMsgState(false);
                _msgId = -1;
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

            _dtReceipt.Clear();

            if (DBRelease.getReceiptList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtReceipt.NewRow();
                        dr["NO"] = index++;
                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        dr["RECEIPT"] = obj["RECEIPT"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["RECEIPT_STATE"] = obj["RECEIPT_STATE"];
                        dr["RECEIPT_USER_ID"] = obj["RECEIPT_USER_ID"];
                        dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                        dr["COMPLETE_DT"] = ConvertUtil.ToDateTimeNull(obj["COMPLETE_DT"]);

                        dr["INVENTORY_ID"] = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                        dr["BARCODE"] = ConvertUtil.ToString(obj["BARCODE"]);
                        dr["EXPORT_ID"] = ConvertUtil.ToInt64(obj["EXPORT_ID"]);
                        dr["EXPORT"] = ConvertUtil.ToString(obj["EXPORT"]);
                        dr["MODEL_ID"] = obj["MODEL_ID"];
                        dr["PRODUCT_TYPE"] = obj["PRODUCT_TYPE"];
                        dr["REQUEST_TYPE"] = obj["REQUEST_TYPE"];
                        dr["REQUEST_TYPE_DETAIL"] = obj["REQUEST_TYPE_DETAIL"];
                        dr["CHARGE_USER_ID"] = ConvertUtil.ToString(obj["CHARGE_USER_ID"]);
                        dr["PROCESS_TYPE"] = ConvertUtil.ToString(obj["PROCESS_TYPE"]);
                        dr["PRODUCT_YN"] = obj["PRODUCT_YN"];
                        dr["DES"] = obj["DES"];
                        dr["PROCESS_DES"] = obj["PROCESS_DES"];

                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                        dr["MBD_MANUFACT"] = ConvertUtil.ToString(obj["MBD_MANUFACT"]);
                        dr["PRODUCT_NAME"] = ConvertUtil.ToString(obj["PRODUCT_NAME"]);
                        dr["MBD_MODEL_NM"] = ConvertUtil.ToString(obj["MBD_MODEL_NM"]);
                        dr["CPU_INFO"] = ConvertUtil.ToString(obj["CPU_INFO"]);
                        dr["MON_INFO"] = ConvertUtil.ToString(obj["MON_INFO"]);
                        _dtReceipt.Rows.Add(dr);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void getMsgList()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("RECEIPT_ID", _receiptId);
            if (ProjectInfo._userCompanyId != 2)
                jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            if (DBRelease.getMessageList(jobj, ref jResult))
            {

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string componentCd;
                    string userId;
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                        userId = ConvertUtil.ToString(obj["CHARGE_USER_ID"]);

                        DataRow dr = _dtMsg.NewRow();
                        dr["NO"] = index++;
                        dr["MESSAGE_ID"] = obj["MESSAGE_ID"];
                        dr["FILE_ID"] = ConvertUtil.ToInt64(obj["FILE_ID"]);
                        dr["STATE"] = ConvertUtil.ToString(obj["STATE"]);
                        dr["DEPT_CD"] = ConvertUtil.ToString(obj["DEPT_CD"]);
                        
                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        //dr["RECEIVER_USER_ID"] = obj["CHARGE_USER_ID"];
                        dr["CHARGE_USER_ID"] = userId;
                        dr["ETC"] = obj["ETC"];
                        dr["TITLE"] = ConvertUtil.ToString(obj["TITLE"]);
                        dr["DES"] = ConvertUtil.ToString(obj["DES"]);
                        dr["SEND_DT"] = ConvertUtil.ToDateTimeNull(obj["SEND_DT"]);
                        dr["RECEIVE_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIVE_DT"]);

                        dr["PROCESS_TYPE"] = ConvertUtil.ToString(obj["PROCESS_TYPE"]);
                        dr["PROCESS_DES"] = ConvertUtil.ToString(obj["PROCESS_DES"]);

                        dr["CHECK"] = false;

                        //dr["RECEIPT"] = obj["RECEIPT"];
                        _dtMsg.Rows.Add(dr);

                        if (!_listReceiver.Contains(userId))
                            _listReceiver.Add(userId);
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

            if (diffDay > 180)
            {
                jData.Add("MSG", "검색 기간은 180일을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("RECEIPT_DT_S", dtFrom);
            jData.Add("RECEIPT_DT_E", dtTo);

            if (!string.IsNullOrWhiteSpace(teOrder.Text))
                jData.Add("RECEIPT", ConvertUtil.ToString(teOrder.Text));

            if (!ConvertUtil.ToString(leReceiptState.EditValue).Equals("-1"))
                jData.Add("RECEIPT_STATE", ConvertUtil.ToString(leReceiptState.EditValue));

            if (!ConvertUtil.ToString(leCompany.EditValue).Equals("-1"))
                jData.Add("COMPANY_ID", ConvertUtil.ToString(leCompany.EditValue));

            return true;
        }

        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                //int rowhandle = gvList.FocusedRowHandle;
                //gvList.FocusedRowHandle = -2147483646;
                //gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtMsg.Select("STATE = 2 OR NEW = 1");
                if (rows.Length < 1)
                {
                    Dangol.Message("변경사항이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("변경사항을 저장하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    var jArrayNew = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    int productYn;
                    int newPart;
                    foreach (DataRow row in rows)
                    {
                        newPart = ConvertUtil.ToInt32(row["NEW"]);
                        productYn = ConvertUtil.ToInt32(row["PRODUCT_YN"]);
                        
                        if (newPart == 1)
                        {
                            JObject jdata = new JObject();
                            jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                            jdata.Add("EXPORT_ID", ConvertUtil.ToInt64(row["EXPORT_ID"]));
                            jdata.Add("RECEIPT_ID", _receiptId);
                            jdata.Add("PRODUCT_YN", ConvertUtil.ToInt32(row["PRODUCT_YN"]));
                            jdata.Add("SPARE_PARTS", ConvertUtil.ToString(row["SPARE_PARTS"]));
                            jdata.Add("PART_STATE", ConvertUtil.ToString(row["PART_STATE"]));
                            jdata.Add("ETC", ConvertUtil.ToString(row["ETC"]));
                            jdata.Add("QTY", ConvertUtil.ToInt32(row["QTY"]));
                            jArrayNew.Add(jdata);
                        }
                        else
                        {
                            JObject jdata = new JObject();
                            jdata.Add("ORDER_PART_ID", ConvertUtil.ToInt64(row["ORDER_PART_ID"]));
                            jdata.Add("SPARE_PARTS", ConvertUtil.ToString(row["SPARE_PARTS"]));
                            jdata.Add("ETC", ConvertUtil.ToString(row["ETC"]));
                            jdata.Add("QTY", ConvertUtil.ToInt32(row["QTY"]));
                            jArray.Add(jdata);
                        }
                    }

                    jobj.Add("DATA", jArray);
                    jobj.Add("NEW_DATA", jArrayNew);
                    jobj.Add("RECEIPT_ID", _receiptId);
                    jobj.Add("RECEIPT", _receipt);
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.updateReleaseOrderParts(jobj, ref jResult))
                    {
                        refresh();
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
                //int rowhandle = gvList.FocusedRowHandle;
                //gvList.FocusedRowHandle = -2147483646;
                //gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtMsg.Select("CHECK = TRUE AND PART_STATE IN ('E', 'O')");
                if (rows.Length < 1)
                {
                    Dangol.Message("삭제 가능한 품목이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 품목을 삭제하시겠습니까?(대기 및 주문 상태 품목)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    List<long> listOrderPartId = new List<long>();
       
                    foreach (DataRow row in rows)
                        listOrderPartId.Add(ConvertUtil.ToInt64(row["ORDER_PART_ID"]));

                    jobj.Add("LIST_ORDER_PART_ID", string.Join(",", listOrderPartId));
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.deleteReleaseOrderParts(jobj, ref jResult))
                    {
                        refresh();

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
                //int rowhandle = gvList.FocusedRowHandle;
                //gvList.FocusedRowHandle = -2147483646;
                //gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtMsg.Select("CHECK = TRUE AND PART_STATE = 'E' AND NEW = 0");
                if (rows.Length < 1)
                {
                    Dangol.Message("주문 가능한 품목이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 품목을 주문하시겠습니까?(대기중인 품목)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    List<long> listOrderPartId = new List<long>();

                    foreach (DataRow row in rows)
                        listOrderPartId.Add(ConvertUtil.ToInt64(row["ORDER_PART_ID"]));

                    jobj.Add("LIST_ORDER_PART_ID", string.Join(",", listOrderPartId));
                    jobj.Add("PART_STATE", "O");
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.updateReleaseOrderPartsBulk(jobj, ref jResult))
                    {
                        //gvList.BeginDataUpdate();
                        foreach(DataRow row in rows)
                        {
                            row["PART_STATE"] = "O";
                        }
                        //gvList.EndDataUpdate();

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
            else if (e.Button.Properties.Tag.Equals(4))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        //gcList.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
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
            //int rowHandle = gvList.FocusedRowHandle;
            //int topRowIndex = gvList.TopRowIndex;
            //gvList.FocusedRowHandle = -2147483646;
            //gvList.FocusedRowHandle = rowHandle;

            string barcode = _barcode;
            List<long> listChecked = new List<long>();

            //DataRow[] rows = _dtMsg.Select("CHECK = TRUE");
            //long inventoryId;
            //foreach (DataRow row in rows)
            //{
            //    inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
            //    if (!listChecked.Contains(inventoryId))
            //        listChecked.Add(inventoryId);
            //}

            //lcgBarcodeList.CustomHeaderButtons[5].Properties.Checked = false;

            //gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            //gvList.FocusedRowChanged -= gvList_FocusedRowChanged;
            //getMsgList();

            //if (listChecked.Count > 0)
            //{
            //    gvList.BeginDataUpdate();
            //    foreach (DataRow row in _dtMsg.Rows)
            //    {
            //        inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
            //        if (listChecked.Contains(inventoryId))
            //        {
            //            row.BeginEdit();
            //            row["CHECK"] = true;
            //            row.EndEdit();
            //        }
            //    }
            //    gvList.EndDataUpdate();
            //}
            //gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
            //gvList.FocusedRowChanged += gvList_FocusedRowChanged;

            //rowHandle = gvList.LocateByValue("BARCODE", barcode);

            //if (rowHandle > -1)
            //{
            //    if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //    {
            //        gvList.FocusedRowHandle = -2147483646;
            //        gvList.FocusedRowHandle = rowHandle;
            //        gvList.TopRowIndex = topRowIndex;
            //    }
            //}
            //else
            //{
            //    if (_dtMsg.Rows.Count > 0)
            //    {
            //        gvList.FocusedRowHandle = -2147483646;
            //        gvList.MoveFirst();
            //    }
            //}
        }

        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //int rowhandle = gvList.FocusedRowHandle;
            //int topRowIndex = gvList.TopRowIndex;
            //gvList.FocusedRowHandle = -2147483646;
            //gvList.FocusedRowHandle = rowhandle;

            //try
            //{
            //    gvList.BeginUpdate();
            //    foreach (DataRow row in _dtMsg.Rows)
            //    {
            //        row.BeginEdit();
            //        row["CHECK"] = false;
            //        row.EndEdit();
            //    }

            //    ArrayList rows = new ArrayList();
            //    for (int i = 0; i < gvList.DataRowCount; i++)
            //    {
            //        int rowHandle = gvList.GetVisibleRowHandle(i);
            //        rows.Add(gvList.GetDataRow(rowHandle));
            //    }

            //    for (int i = 0; i < rows.Count; i++)
            //    {
            //        DataRow row = rows[i] as DataRow;
            //        // Change the field value.
            //        row["CHECK"] = true;
            //    }
            //}
            //finally
            //{
            //    gvList.EndUpdate();
            //}
        }

        private void lcgBarcodeList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //int rowhandle = gvList.FocusedRowHandle;
            //int topRowIndex = gvList.TopRowIndex;
            //gvList.FocusedRowHandle = -2147483646;
            //gvList.FocusedRowHandle = rowhandle;

            //gvList.BeginDataUpdate();

            //foreach (DataRow row in _dtMsg.Rows)
            //{
            //    row.BeginEdit();
            //    row["CHECK"] = false;
            //    row.EndEdit();
            //}
            //gvList.EndDataUpdate();
            
        }

        private void refreshReceipt()
        {
            int rowHandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            //gvReceipt.FocusedRowHandle = -2147483646;
            //gvReceipt.FocusedRowHandle = rowHandle;

            string receipt = _receipt;
            //List<long> listChecked = new List<long>();

            //DataRow[] rows = _dt.Select("CHECK = TRUE");
            //long inventoryId;
            //foreach (DataRow row in rows)
            //{
            //    inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
            //    if (!listChecked.Contains(inventoryId))
            //        listChecked.Add(inventoryId);
            //}

            //lcgBarcodeList.CustomHeaderButtons[5].Properties.Checked = false;

            gvReceipt.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            gvReceipt.FocusedRowChanged -= gvList_FocusedRowChanged;
            getExportList();

            //if (listChecked.Count > 0)
            //{
            //    gvList.BeginDataUpdate();
            //    foreach (DataRow row in _dt.Rows)
            //    {
            //        inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
            //        if (listChecked.Contains(inventoryId))
            //        {
            //            row.BeginEdit();
            //            row["CHECK"] = true;
            //            row.EndEdit();
            //        }
            //    }
            //    gvList.EndDataUpdate();
            //}
            gvReceipt.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
            gvReceipt.FocusedRowChanged += gvList_FocusedRowChanged;

            rowHandle = gvReceipt.LocateByValue("RECEIPT", receipt);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvReceipt.FocusedRowHandle = -2147483646;
                    gvReceipt.FocusedRowHandle = rowHandle;
                    gvReceipt.TopRowIndex = topRowIndex;
                }
            }
            else
            {
                if (_dtReceipt.Rows.Count > 0)
                {
                    gvReceipt.FocusedRowHandle = -2147483646;
                    gvReceipt.MoveFirst();
                }
            }
        }

        private void lcgReceiptList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                Dangol.ShowSplash();
                refreshReceipt();
                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                using (dlgNewTechnicalReceipt newTechnicalReceipt = new dlgNewTechnicalReceipt())
                {
                    if (newTechnicalReceipt.ShowDialog(this) == DialogResult.OK)
                    {
                        Dangol.ShowSplash();
                        refreshReceipt();
                        Dangol.CloseSplash();
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                if(_currentRow == null)
                {
                    Dangol.Message("선택된 접수가 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 접수를 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();


                    JObject jResult = new JObject();
                    JObject jData = new JObject();
                    jData.Add("RECEIPT_ID", _receiptId);
                    jData.Add("RECEIPT_STATE", "-1");
                    jData.Add("REFRESH", 0);
                    //jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.updateTechnicalReceipt(jData, ref jResult))
                    {
                        gvReceipt.BeginDataUpdate();
                        _currentRow.Delete();
                        gvReceipt.EndDataUpdate();
                    }
    
                    Dangol.CloseSplash();
                }
            }
        }
        

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            getExportList();
            Dangol.CloseSplash();
        }

        private void lcgReceiptList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            gvReceipt.FocusedRowHandle = -2147483646;
            gvReceipt.FocusedRowHandle = rowhandle;

            try
            {
                gvReceipt.BeginUpdate();
                foreach (DataRow row in _dtReceipt.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
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
                    // Change the field value.
                    row["CHECK"] = true;
                }
            }
            finally
            {
                gvReceipt.EndUpdate();
            }
        }

        private void lcgReceiptList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            gvReceipt.FocusedRowHandle = -2147483646;
            gvReceipt.FocusedRowHandle = rowhandle;

            gvReceipt.BeginDataUpdate();

            foreach (DataRow row in _dtReceipt.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvReceipt.EndDataUpdate();
        }


        private void gvList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "BARCODE")
            {
                int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["STATE"]));

                if (state == 2)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
            }
            else if (e.Column.FieldName == "RECEIPT")
            {
                int newPart = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["NEW"]));

                if (newPart == 1)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Beige);
            }
        }

        private void lcgMessage_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                //using (dlgSelectReceiveUser selectReceiveUser = new dlgSelectReceiveUser(_currentRow, _listReceiver))
                //{
                //    if (selectReceiveUser.ShowDialog(this) == DialogResult.OK)
                //    {
                //        _dtMsg.Clear();
                //        _listReceiver.Clear();
                //        Dangol.ShowSplash();
                //        getMsgList();
                //        Dangol.CloseSplash();


                //    }
                //}
            }
        }

        private void lgcReceiptInfo_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            JObject jobj = new JObject();

            string /*e*/xport = teExport.Text.ToUpper().Trim();
            string barcode = teBarcode.Text.ToUpper().Trim();

            if (barcode.Length > 1)
            {
                if (barcode.Length != 12 || (!regex1.IsMatch(barcode) && !regex2.IsMatch(barcode) && !regex3.IsMatch(barcode)))
                {
                    Dangol.Message("Invalid inventory number.");
                    return;
                }
                else
                {
                    jobj.Add("BARCODE", barcode);
                }
            }

            //if (export.Length > 1 && !export.Equals("unkown"))
            //{
            //    if (export.Length != 10 || !regex4.IsMatch(export))
            //    {
            //        Dangol.Message("Invalid export number.");
            //        return;
            //    }
            //    else
            //    {
            //        jobj.Add("EXPORT", export);
            //    }
            //}

            if (Dangol.MessageYN("수정하시겠습니까") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                checkInfo(ref jobj);

                jobj.Add("REFRESH", 1);

                if (DBRelease.updateTechnicalReceipt(jobj, ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        _currentRow.BeginEdit();
                        _currentRow["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        _currentRow["BARCODE"] = obj["BARCODE"];
                        _currentRow["EXPORT_ID"] = obj["EXPORT_ID"];
                        _currentRow["EXPORT"] = obj["EXPORT"];
                        _currentRow["MODEL_ID"] = obj["MODEL_ID"];
                        _currentRow["PRODUCT_TYPE"] = obj["PRODUCT_TYPE"];
                        _currentRow["REQUEST_TYPE"] = obj["REQUEST_TYPE"];
                        _currentRow["REQUEST_TYPE_DETAIL"] = obj["REQUEST_TYPE_DETAIL"];
                        _currentRow["CHARGE_USER_ID"] = ConvertUtil.ToString(obj["CHARGE_USER_ID"]);
                        _currentRow["PROCESS_TYPE"] = ConvertUtil.ToString(obj["PROCESS_TYPE"]);
                        _currentRow["PRODUCT_YN"] = obj["PRODUCT_YN"];
                        _currentRow["DES"] = obj["DES"];

                        _currentRow["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        _currentRow["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                        _currentRow["MBD_MANUFACT"] = ConvertUtil.ToString(obj["MBD_MANUFACT"]);
                        _currentRow["PRODUCT_NAME"] = ConvertUtil.ToString(obj["PRODUCT_NAME"]);
                        _currentRow["MBD_MODEL_NM"] = ConvertUtil.ToString(obj["MBD_MODEL_NM"]);
                        _currentRow["CPU_INFO"] = ConvertUtil.ToString(obj["CPU_INFO"]);
                        _currentRow["MON_INFO"] = ConvertUtil.ToString(obj["MON_INFO"]);
                        _currentRow.EndEdit();
                    }

                    Dangol.Message(ConvertUtil.ToString("수정되었습니다"));
  
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private void checkInfo(ref JObject jData)
        {
            jData.Add("RECEIPT_ID", _receiptId);

            DateTime dt = Convert.ToDateTime(deReceiptDt.EditValue);
            //jData.Add("RECEIPT_DT", dt.ToString("yyyy-MM-dd"));

            jData.Add("COMPANY_ID", ConvertUtil.ToInt64(_currentRow["COMPANY_ID"]));
            //jData.Add("RECEIPT_USER_ID", ConvertUtil.ToString(leReceiptUserId.EditValue));
            
            jData.Add("PRODUCT_TYPE", ConvertUtil.ToString(leProductType.EditValue));
            jData.Add("MODEL_ID", ConvertUtil.ToString(leModelId.EditValue));
            jData.Add("REQUEST_TYPE", ConvertUtil.ToString(leModelId.EditValue));
            jData.Add("REQUEST_TYPE_DETAIL", ConvertUtil.ToString(leModelId.EditValue));
            jData.Add("DES", ConvertUtil.ToString(meDes.Text));
            jData.Add("PRODUCT_YN", ConvertUtil.ToInt32(_currentRow["PRODUCT_YN"]));
        }

        private void lcgProcessInfo_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentMsgRow == null)
            {
                Dangol.Error(ConvertUtil.ToString("no selected message"));
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("처리 정보를 수정하시겠습니까") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    jobj.Add("MESSAGE_ID", _msgId);
                    jobj.Add("PROCESS_TYPE", ConvertUtil.ToString(leProcessType.EditValue));
                    jobj.Add("PROCESS_DES", ConvertUtil.ToString(meProcessDes.Text));

                    if (DBRelease.updateMsgInfo(jobj, ref jResult))
                    {
                        _currentMsgRow.BeginEdit();
                        _currentMsgRow["PROCESS_TYPE"] = jobj["PROCESS_TYPE"];
                        _currentMsgRow["PROCESS_DES"] = jobj["PROCESS_DES"];
                        _currentMsgRow.EndEdit();

                        Dangol.Message(ConvertUtil.ToString("수정되었습니다"));
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (Dangol.MessageYN("처리 정보를 완료하시겠습니까") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    jobj.Add("MESSAGE_ID", _msgId);
                    jobj.Add("STATE", "3");

                    if (DBRelease.updateMsgInfo(jobj, ref jResult))
                    {
                        _currentMsgRow.BeginEdit();
                        _currentMsgRow["STATE"] = jobj["STATE"];
                        _currentMsgRow.EndEdit();

                        setMsgState(true);

                        Dangol.Message(ConvertUtil.ToString("처리되었습니다"));
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                if (Dangol.MessageYN("처리 정보를 처리중으로 변경하시겠습니까") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    jobj.Add("MESSAGE_ID", _msgId);
                    jobj.Add("STATE", "2");

                    if (DBRelease.updateMsgInfo(jobj, ref jResult))
                    {
                        _currentMsgRow.BeginEdit();
                        _currentMsgRow["STATE"] = jobj["STATE"];
                        _currentMsgRow.EndEdit();

                        setMsgState(true);

                        Dangol.Message(ConvertUtil.ToString("처리되었습니다"));
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }

        }

        private void rgProcess_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Dangol.MessageYN("처리 상태를 수정하시겠습니까") == DialogResult.Yes)
            {
                int receiptState = ConvertUtil.ToInt32(e.NewValue);

                JObject jResult = new JObject();
                JObject jobj = new JObject();
                jobj.Add("RECEIPT_ID", _receiptId);
                jobj.Add("RECEIPT_STATE", ConvertUtil.ToString(e.NewValue));

                if(receiptState == 7)
                    jobj.Add("COMPLETE_DT", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                else
                    _currentRow["COMPLETE_DT"] = "";

                jobj.Add("REFRESH", 0);

                if (DBRelease.updateTechnicalReceipt(jobj, ref jResult))
                {
                    _currentRow.BeginEdit();
                    _currentRow["RECEIPT_STATE"] = jobj["RECEIPT_STATE"];
                    if (receiptState == 7)
                        _currentRow["COMPLETE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                    else
                        _currentRow["COMPLETE_DT"] = "";

                    _currentRow.EndEdit();

                    Dangol.Message(ConvertUtil.ToString("수정되었습니다"));
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        
    }
}