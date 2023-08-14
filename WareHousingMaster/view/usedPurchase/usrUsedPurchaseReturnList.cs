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

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrUsedPurchaseReturnList : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _currentReceipt;
        DataRowView _currentPart;

        DataTable _dtReceipt;
        DataTable _dtReceiptPartGrid;
        DataTable _dtReceiptFaultPartGrid;
        DataTable _dtComponentList;
        DataTable _dtAdjustMentProcess;
        DataTable _dtAdjustmentHistory;

        BindingSource _bsReceipt;
        BindingSource _bsComponentList;     
        BindingSource _bsReceiptPartGrid;
        BindingSource _bsReceiptFaultPartGrid;
        BindingSource _bsAdjustMentProcess;
        BindingSource _bsAdjustmentHistory;

        //string[] _consignedComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "SSD", "HDD", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG" };
        //string[] _consignedComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "SSD", "HDD", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스" };

        string[] _receiptStateCd = new string[] { "RECEIPT_DT", "PROCESS_DT", "POSTPONE_DT", "CANCEL_DT", "RELEASE_DT", "RETURN_REQUEST_DT", "RETURN_IN_DT", "EXCHANGE_DT", "RETURN_CANCEL_DT", "RETURN_COMPLETE_DT" };
        Dictionary<int, string> _dicConsignedComponentCd;
        Dictionary<string, int> _dicConsignedComponentCdReverse;

        Dictionary<string, string> _dicUsedPurchaseReleaseState;


        int _releaseState;
        long _partPrice = 0;
        long _warehousingId = 0;
        int _currnetPartCnt=0;

  
        public usrUsedPurchaseReturnList()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("POSTAL_CD", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TEL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DELIVERY_COMPANY", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("WORKER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRICE", typeof(long)));

            _dtReceiptPartGrid = new DataTable();
            _dtReceiptPartGrid.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("RELEASE_PART_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("DETAIL_DATA", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PART_PRICE", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("ASSIGN_YN", typeof(bool)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("INVENTORY_YN", typeof(bool)));

            _dtAdjustMentProcess = new DataTable();
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_DELIVERY", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_PRODUCE", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_QUICK", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_REPRODUCE", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtAdjustmentHistory = new DataTable();
            _dtAdjustmentHistory.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtAdjustmentHistory.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            

            _dtComponentList = new DataTable();
            _dtComponentList.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("VISIBLE", typeof(bool)));

            _dtReceiptFaultPartGrid = new DataTable();
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("RELEASE_FAULT_PART_ID", typeof(long)));
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("FAULT_ID", typeof(long)));
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtReceiptFaultPartGrid.Columns.Add(new DataColumn("PART_PRICE", typeof(string)));


            _bsReceipt = new BindingSource();
            _bsComponentList = new BindingSource();
            _bsReceiptFaultPartGrid = new BindingSource();
            _bsReceiptPartGrid = new BindingSource();
            _bsAdjustMentProcess = new BindingSource();
            _bsAdjustmentHistory = new BindingSource();


            _dicUsedPurchaseReleaseState = new Dictionary<string, string>();

            _releaseState = -1;
        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {

            if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                lcRelease.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                lcRelease.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            setInfoBox();
            setIInitData();
            setGridControl();
            setStatistics();

            teReceipt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT", false, DataSourceUpdateMode.OnPropertyChanged));
            deReceiptDt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT_DT", false, DataSourceUpdateMode.OnPropertyChanged));
            rgReleaseType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RELEASE_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            meRequest.DataBindings.Add(new Binding("Text", _bsReceipt, "REQUEST", false, DataSourceUpdateMode.OnPropertyChanged));

            teCustomerNm1.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel1.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL", false, DataSourceUpdateMode.OnPropertyChanged));
            teHp1.DataBindings.Add(new Binding("Text", _bsReceipt, "HP", false, DataSourceUpdateMode.OnPropertyChanged));
            tePostalCd.DataBindings.Add(new Binding("Text", _bsReceipt, "POSTAL_CD", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddress.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddressDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));

            rgProcess.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RELEASE_STATE", false, DataSourceUpdateMode.OnPropertyChanged));
            //rgReturn.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RETURN_STATE", false, DataSourceUpdateMode.OnPropertyChanged));

            leDelivery.DataBindings.Add(new Binding("EditValue", _bsReceipt, "DELIVERY_COMPANY", false, DataSourceUpdateMode.OnPropertyChanged));
            teDelivery.DataBindings.Add(new Binding("Text", _bsReceipt, "INVOICE", false, DataSourceUpdateMode.OnPropertyChanged));


            var today = DateTime.Today;
            var pastDate = today.AddDays(-10);

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
            dtProxyState = Util.getCodeList("CD1308", "KEY", "VALUE");
            Util.LookupEditHelper(rileProxyState, dtProxyState, "KEY", "VALUE");

            RadioGroupItem[] rgProcessState = new RadioGroupItem[5];
            RadioGroupItem[] rgReturnState = new RadioGroupItem[5];
            RadioGroupItem[] rgASState = new RadioGroupItem[5];

            int indexProcess = 0;

            for (int i = 0; i < dtProxyState.Rows.Count; i++)
            {
                RadioGroupItem rgItem = new RadioGroupItem(dtProxyState.Rows[i]["KEY"], ConvertUtil.ToString(dtProxyState.Rows[i]["VALUE"]), true, dtProxyState.Rows[i]["KEY"]);

                _dicUsedPurchaseReleaseState.Add(ConvertUtil.ToString(dtProxyState.Rows[i]["KEY"]), ConvertUtil.ToString(dtProxyState.Rows[i]["VALUE"]));
                rgProcessState[indexProcess++] = rgItem;
               
            }
            
            this.rgProcess.Properties.Items.AddRange(rgProcessState);

            Util.insertRowonTop(dtProxyState, "-1", "선택안함");
            Util.LookupEditHelper(leReceiptState, dtProxyState, "KEY", "VALUE");

            
            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(rileUserId, dtUserId, "KEY", "VALUE");

            Util.LookupEditHelper(rileUser, dtUserId, "KEY", "VALUE");        

            DataTable dtDelivery = Util.getCodeList("CD0904", "KEY", "VALUE");
            Util.insertRowonTop(dtDelivery, "-1", "없음");
            Util.LookupEditHelper(leDelivery, dtDelivery, "KEY", "VALUE");

            DataTable dtInventoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryCat, dtInventoryCat, "KEY", "VALUE");

            Util.LookupEditHelper(rileComponentCd, ConsignedInfo._dtComponent, "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd2, ConsignedInfo._dtComponent, "KEY", "VALUE");
            

            //DataTable dtState = Util.getCodeList("CD0902", "KEY", "VALUE");

            DataRow dr3 = _dtAdjustMentProcess.NewRow();
            dr3["PRICE"] = 0;
            dr3["PRICE_DELIVERY"] = 0;
            dr3["PRICE_PRODUCE"] = 0;
            dr3["PRICE_QUICK"] = 0;
            dr3["PRICE_REPRODUCE"] = 0;
            dr3["TOTAL_PRICE"] = 0;
            _dtAdjustMentProcess.Rows.Add(dr3);

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;
            _bsComponentList.DataSource = _dtComponentList;
            _bsReceiptFaultPartGrid.DataSource = _dtReceiptFaultPartGrid;
            _bsReceiptPartGrid.DataSource = _dtReceiptPartGrid;
            _bsAdjustMentProcess.DataSource = _dtAdjustMentProcess;
            _bsAdjustmentHistory.DataSource = _dtAdjustmentHistory;

        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsReceipt;

            gcPart.DataSource = null;
            gcPart.DataSource = _bsReceiptPartGrid;

            gcFault.DataSource = null;
            gcFault.DataSource = _bsReceiptFaultPartGrid;

            gcAdjustment.DataSource = null;
            gcAdjustment.DataSource = _bsAdjustMentProcess;

            gcAdjustmentHistory.DataSource = null;
            gcAdjustmentHistory.DataSource = _bsAdjustmentHistory;
        }


        private void setStatistics()
        {
            JObject jResult = new JObject();

            if (DBUsedPurchase.getUsedPurchaseReceiptStatistics(ref jResult))
            {
                teReceiptCnt.Text = $"{jResult["RECEIPT_CNT"]}";
                teProcessCnt.Text = $"{jResult["PROCESS_CNT"]}";
                teHoldCnt.Text = $"{jResult["HOLD_CNT"]}";
            }
        }

       

        private void setAdjustmentReadonly()
        {
            if (_releaseState == 1)
            {
                gvAdjustment.OptionsBehavior.Editable = true;
                lgcComponentList.CustomHeaderButtons[0].Properties.Enabled = true;
                gridColumn21.OptionsColumn.AllowEdit = true;
            }
            else
            {
                gvAdjustment.OptionsBehavior.Editable = false;
                lgcComponentList.CustomHeaderButtons[0].Properties.Enabled = false;
                gridColumn21.OptionsColumn.AllowEdit = false;
            }
        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);
            _dtComponentList.Clear();

            if (isValidRow)
            {
                _currentReceipt = e.Row as DataRowView;

                _releaseState = ConvertUtil.ToInt32(_currentReceipt["RELEASE_STATE"]);
                _warehousingId = ConvertUtil.ToInt64(_currentReceipt["WAREHOUSING_ID"]);
                lgcProxy.BeginInit();
                lcgLicence.BeginInit();
                _currentReceipt.BeginEdit();

                getUsedPurchaseReleaseInfo();
                getAdjustmentInfo();

                setAdjustmentReadonly();

                //if (ConvertUtil.ToInt32(_currentReceipt["RETURN_YN"]) == 1)
                //{
                //    rgProcess.ReadOnly = true;
                //    rgReturn.ReadOnly = true;

                //    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    
                //}
                //else
                {
                    rgProcess.ReadOnly = false;
                    rgReturn.ReadOnly = true;
                    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }

                
                _currentReceipt.EndEdit();
                lgcProxy.EndInit();
                lcgLicence.EndInit();

            }
            else
            {
                _currentReceipt = null;
                _dtComponentList.Clear();


            }
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentPart = e.Row as DataRowView;
                _partPrice = ConvertUtil.ToInt64(_currentPart["PART_PRICE"]);


                if(_releaseState == 1 && ConvertUtil.ToBoolean(_currentPart["ASSIGN_YN"]))
                    gridColumn21.OptionsColumn.AllowEdit = true;
                else
                    gridColumn21.OptionsColumn.AllowEdit = false;
            }
            else
            {
                _partPrice = 0;
                _currentPart = null;
            }
        }

        private void getUsedPurchaseReleaseInfo()
        {
            gvPart.BeginDataUpdate();

            _dtReceiptPartGrid.Clear();
            //_dtReceiptFaultPartGrid.Clear();
            JObject jResult = new JObject();

            if (DBUsedPurchase.getUsedPurchaseReleasePartInfo(_currentReceipt["RELEASE_ID"], ref jResult))
            {
                long price = 0;

                if (ConvertUtil.ToBoolean(jResult["EXAM_DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["EXAM_DATA"].ToString());
                    int index = 1;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                        DataRow drGrid = _dtReceiptPartGrid.NewRow();

                        drGrid["NO"] = index++;
                        drGrid["RELEASE_PART_ID"] = obj["RELEASE_PART_ID"];
                        drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        drGrid["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                        drGrid["DETAIL_DATA"] = obj["MODEL_NM"];
                        drGrid["ASSIGN_YN"] = true;
                        drGrid["PART_PRICE"] = obj["PART_PRICE"];
                        drGrid["PART_CNT"] = 1;
                        drGrid["INVENTORY_YN"] = true;
                        

                        _dtReceiptPartGrid.Rows.Add(drGrid);

                        price += ConvertUtil.ToInt64(obj["PART_PRICE"]);
                    }
                }
                if (ConvertUtil.ToBoolean(jResult["FAULT_DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["FAULT_DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                        DataRow drGrid = _dtReceiptPartGrid.NewRow();

                        drGrid["NO"] = index++;
                        drGrid["RELEASE_PART_ID"] = obj["RELEASE_FAULT_PART_ID"];
                        drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        drGrid["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                        drGrid["DETAIL_DATA"] = obj["MODEL_NM"];
                        drGrid["ASSIGN_YN"] = true;
                        drGrid["PART_PRICE"] = obj["PART_PRICE"];
                        drGrid["PART_CNT"] = ConvertUtil.ToInt64(obj["PART_CNT"]);
                        drGrid["INVENTORY_YN"] = false;

                        _dtReceiptPartGrid.Rows.Add(drGrid);

                        price += ConvertUtil.ToInt64(ConvertUtil.ToInt64(obj["PART_PRICE"]));


                        //DataRow drGrid = _dtReceiptFaultPartGrid.NewRow();

                        //drGrid["NO"] = index++;
                        //drGrid["RELEASE_FAULT_PART_ID"] = obj["RELEASE_FAULT_PART_ID"];
                        //drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        //drGrid["MODEL_NM"] = obj["MODEL_NM"];
                        //drGrid["DES"] = obj["DES"];
                        //drGrid["PART_CNT"] = obj["PART_CNT"];
                        //drGrid["PART_PRICE"] = obj["PART_PRICE"];

                        //_dtReceiptFaultPartGrid.Rows.Add(drGrid);

                        //price += ConvertUtil.ToInt64(obj["PART_PRICE"]);
                    }
                }

                //DataView dv = new DataView(_dtReceiptPartGrid);
                //dv.Sort = "COMPONENT_CD ASC";

                //_dtReceiptPartGrid = dv.ToTable();

                _bsReceiptPartGrid.Sort = "COMPONENT_CD ASC";

                _currentReceipt["PRICE"] = price;
            }

            gvPart.EndDataUpdate();
        }

        private bool saveAdjustmentInfo()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("RELEASE_ID", ConvertUtil.ToInt64(_currentReceipt["RELEASE_ID"]));
            jobj.Add("ADJUSTMENT_TYPE", 1);
            jobj.Add("PRICE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]));
            jobj.Add("PRICE_PRODUCE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"]));
            jobj.Add("PRICE_REPRODUCE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"]));
            jobj.Add("PRICE_DELIVERY", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"]));
            jobj.Add("PRICE_QUICK", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_QUICK"]));
            jobj.Add("REGISTER_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (DBUsedPurchase.updateAdjust(jobj, ref jResult))
            {
                DataRow row = _dtAdjustmentHistory.NewRow();
                row["UPDATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                row["USER_ID"] = ProjectInfo._userId;
                _dtAdjustmentHistory.Rows.InsertAt(row, 0);

                return true;
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return false;
            }
        }


        private void lgcComponentList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if(Dangol.MessageYN("정산 금액을 저장하시겠습니까?") != DialogResult.Yes)
            {
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                if (saveAdjustmentInfo())
                    Dangol.Message("처리되었습니다.");
                else
                    return;
            }
        }
        private void getAdjustmentInfo()
        {
            JObject jResult = new JObject();

            _dtAdjustmentHistory.Clear();

            if (DBUsedPurchase.getAdjustmentInfo(_currentReceipt["RELEASE_ID"], 2, 1, 1, ref jResult))
            {
                _dtAdjustMentProcess.Rows[0]["PRICE"] = _currentReceipt["PRICE"];
                _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = jResult["PRICE_DELIVERY"];
                _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = jResult["PRICE_PRODUCE"];
                _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = jResult["PRICE_QUICK"];
                _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = jResult["PRICE_REPRODUCE"];

                long totalPrice = ConvertUtil.ToInt64(_currentReceipt["PRICE"]) + ConvertUtil.ToInt64(jResult["PRICE_DELIVERY"])
                    + ConvertUtil.ToInt64(jResult["PRICE_QUICK"]) + ConvertUtil.ToInt64(jResult["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(jResult["PRICE_PRODUCE"]);


                _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice;


                JArray jArray = JArray.Parse(jResult["REGISTER_DT_LIST"].ToString());

                string list = jResult["REGISTER_DT_LIST"].ToString();

                List<string> message = JsonConvert.DeserializeObject<List<string>>(jResult["REGISTER_DT_LIST"].ToString());

                foreach (string str in message)
                {
                    string[] arrStr = str.Split('/');

                    DataRow row = _dtAdjustmentHistory.NewRow();

                    row["UPDATE_DT"] = arrStr[0].Trim();
                    row["USER_ID"] = arrStr[1].Trim();

                    _dtAdjustmentHistory.Rows.Add(row);
                }
            }
            else
            {
                _dtAdjustMentProcess.Rows[0]["PRICE"] = 0;
                _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = 0;
                _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = 0;
                _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = 0;
                _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = 0;
                _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = 0;
                _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = 0;

                Dangol.Message(jResult["MSG"]);
                return;
            }
        }


        private void teCustomerNm1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            
        }

        private void teCustomerNm1_EditValueChanged(object sender, EventArgs e)
        {
            gvReceipt.BeginUpdate();
            gvReceipt.EndUpdate();

            int rowhandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            gvReceipt.FocusedRowHandle = -1;
            gvReceipt.FocusedRowHandle = rowhandle;
        }

        private void deReceiptDt_EditValueChanged(object sender, EventArgs e)
        {
            gvReceipt.BeginUpdate();
            gvReceipt.EndUpdate();

            int rowhandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            gvReceipt.FocusedRowHandle = -1;
            gvReceipt.FocusedRowHandle = rowhandle;
        }

        private void rgProcess_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (Dangol.MessageYN($"현재 접수를 '{_dicUsedPurchaseReleaseState[e.NewValue.ToString()]}'처리하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();

                string deliveryCompany = "-1";
                string invoice = "";
                int receiptStatus = ConvertUtil.ToInt32(e.NewValue);

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("RELEASE_ID", ConvertUtil.ToInt64(_currentReceipt["RELEASE_ID"]));
                jobj.Add("CHECKED", 1);
                jobj.Add("RELEASE_STATE", receiptStatus);
                jobj.Add("KEY", _receiptStateCd[receiptStatus]);
                jobj.Add("KEY_NUM", receiptStatus);

                if (receiptStatus == 4)
                {
                    deliveryCompany = ConvertUtil.ToString(leDelivery.EditValue);
                    invoice = teDelivery.Text;

                    if (string.IsNullOrEmpty(invoice))
                    {
                        Dangol.CloseSplash();
                        Dangol.Message("배송정보가 입력되지 않았습니다.");
                        e.Cancel = true;
                        return;
                    }
                        //saveLicenceInfo();
                    saveAdjustmentInfo();
                    jobj.Add("RELEASE_CHARGE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]));
                }
                else
                    jobj.Add("RELEASE_CHARGE", 0);

                if (DBUsedPurchase.updateReceiptStatus(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentReceipt["RELEASE_STATE"] = receiptStatus.ToString();
                    //_currentReceipt["RELEASE_STATE"] = receiptStatus.ToString(); 
                    if (receiptStatus == 1)
                        _currentReceipt["WORKER_ID"] = ProjectInfo._userId;
                    _currentReceipt.EndEdit();

                    if(receiptStatus == 3)
                    {
                        _dtAdjustMentProcess.BeginInit();

                        long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
                        long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

                        _dtAdjustMentProcess.Rows[0]["PRICE"] = 0;
                        _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - price;

                        _dtAdjustMentProcess.EndInit();
                    }

                    _releaseState = receiptStatus;

                    int isKakao = 1;
                    if (_releaseState == 3)
                    {
                        jobj.RemoveAll();
                        jobj.Add("WAREHOUSING_ID", _warehousingId);
                        jobj.Add("DELIVERY_COMPANY", "");
                        jobj.Add("DELIVERY_INVOICE", "");

                        DBUsedPurchase.updateUsedPurchaseDeliveryInfo(jobj, ref jResult);         
                    }
                    else if(_releaseState == 4)
                    {
                        jobj.RemoveAll();
                        jobj.Add("WAREHOUSING_ID", _warehousingId);
                        jobj.Add("DELIVERY_COMPANY", deliveryCompany);
                        jobj.Add("DELIVERY_INVOICE", invoice);

                        if (DBUsedPurchase.updateUsedPurchaseDeliveryInfo(jobj, ref jResult))
                        {
                            object key = jResult["KEY"];
                            //StringBuilder postParams = new StringBuilder();
                            //postParams.Append($"&TEL={teTel1.Text}");
                            //postParams.Append($"&HP={teHp1.Text}");
                            //postParams.Append($"&CUSTOMER_NM={teCustomerNm1.Text}");
                            //postParams.Append($"&KEY={key}");
                            //postParams.Append($"&CUSTOMER_CHECK=2");

                            //if (DBUsedPurchase.updateDanawaReceiptState(postParams))
                            //{
                            //    isKakao = 2;
                            //    //Dangol.Message("검수결과가 전송되었습니다.");
                            //}
                            //else
                            //{
                            //    isKakao = 3;

                            //}

                            JArray array = new JArray();
                            JObject jResult2 = new JObject();
                            JObject jObj = new JObject();

                            string msgType = "E";
                            string des = "";

                            string url = $"{ProjectInfo._url}/layoutUsedPurchase.do?content=usedPurchase&KEY={key}";
                            string profile = "b0be757b8bbcafbba523d6fcc4ed890603de96f9";
                            string hp = ConvertUtil.ToString(teHp1.Text);
                            string content = $@"안녕하세요.
중고 매입 서비스 입니다. {teCustomerNm1.Text} 님께서 요청한 제품이 반송 처리 되었습니다. 택배사 및 송장 번호는 아래의 링크로 접속하여 확인이 가능 합니다. {url} 감사합니다.";
                            string hpn = hp.Replace("-", "");
                            hpn = hpn.Substring(1, hpn.Length - 1);

                            if (hpn.Length == 10)
                            {
                                jObj.Add("message_type", "at");
                                jObj.Add("phn", $"82{hpn}");
                                jObj.Add("profile", profile);
                                jObj.Add("msg", $"{content}");
                                jObj.Add("tmplId", "call_04");
                                jObj.Add("smsKind", "L");
                                jObj.Add("msgSms", $"{content}");
                                jObj.Add("smsSender", "07082404495");

                                array.Add(jObj);


                                if (DBUsedPurchase.sendMsgToCustomer(array, ref jResult2))
                                {
                                    string type = "M";
                                    try
                                    {
                                        JObject jOb2 = new JObject();
                                        jOb2 = (JObject)jResult2["data"];
                                        type = ConvertUtil.ToString(jOb2["type"]);
                                    }
                                    catch (Exception ex) { }

                                    if (type.Equals("AT"))
                                        msgType = "K";
                                    else
                                        msgType = "M";
                                }
                                else
                                    des = ConvertUtil.ToString(jResult2["message"]);
                            }
                            else
                            {
                                des = "전화번호 오류";
                            }

                            jObj.RemoveAll();
                            jObj.Add("MSG_TYPE", msgType);
                            jObj.Add("USER_NM", teCustomerNm1.Text);
                            jObj.Add("HP", hp);
                            jObj.Add("CALLER_HP", "070-8240-4495");
                            jObj.Add("CODE", "call_04");
                            jObj.Add("MSG", $"{content}");
                            jObj.Add("DES", des);


                            DBUsedPurchase.updateMsgHistory(jObj, ref jResult2);
                        }
                        
                    }
                    

                    setAdjustmentReadonly();
                    setStatistics();

                    Dangol.CloseSplash();

                    if (isKakao == 1)
                        Dangol.Message("처리되었습니다.");
                    else if(isKakao == 2)
                        Dangol.Message("배송정보가 전송되었습니다.");
                    else if(isKakao == 3)
                        Dangol.Message("오류가 발생했습니다. ERROR: UR2");

                }
                else
                {
                    Dangol.CloseSplash();
                    Dangol.Message(jResult["MSG"]);
                    return;
                }

            }
            else
            {
                e.Cancel = true;
                return;
            }


        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            setStatistics();
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

            if (DBUsedPurchase.searchUsedPurchaseReceiptList(jData, ref jResult))
            {
                
                gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
                gvReceipt.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceipt.NewRow();

                    dr["NO"] = _dtReceipt.Rows.Count + 1;
                    dr["ID"] = index++;
                    
                    dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                    dr["RELEASE_ID"] = obj["RELEASE_ID"];
                    dr["RELEASE_STATE"] = obj["RELEASE_STATE"];

                    dr["RECEIPT"] = obj["RECEIPT"];
                    dr["RECEIPT_DT"] = obj["RECEIPT_DTS"];
                    dr["RELEASE_TYPE"] = "1";
                    dr["REQUEST"] = obj["REQUEST"];
                    dr["INVOICE"] = obj["INVOICE"];
                    dr["DELIVERY_COMPANY"] = obj["DELIVERY_COMPANY"];

                    dr["CUSTOMER_ID"] = obj["CUSTOMER_ID"];
                    dr["CUSTOMER_NM"] = obj["CUSTOMER_NM"];
                    dr["POSTAL_CD"] = obj["POSTAL_CD"];
                    dr["ADDRESS"] = obj["ADDRESS"];
                    dr["ADDRESS_DETAIL"] = obj["ADDRESS_DETAIL"];

                    dr["TEL"] = obj["TEL"];
                    dr["HP"] = obj["MOBILE"];

                    dr["WORKER_ID"] = obj["WORKER_ID"];

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

        private void getCustomerRefresh()
        {
            JObject jResult = new JObject();

            if (DBUsedPurchase.getUsedPurchaseReceiptCustomerInfo(_currentReceipt["CUSTOMER_ID"], ref jResult))
            {
                _currentReceipt.BeginEdit();

                _currentReceipt["CUSTOMER_NM"] = jResult["CUSTOMER_NM"];
                _currentReceipt["POSTAL_CD"] = jResult["POSTAL_CD"];
                _currentReceipt["ADDRESS"] = jResult["ADDRESS"];
                _currentReceipt["ADDRESS_DETAIL"] = jResult["ADDRESS_DETAIL"];
                _currentReceipt["TEL"] = jResult["TEL"];
                _currentReceipt["HP"] = jResult["MOBILE"];

                _currentReceipt.EndEdit();
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
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

                if (diffDay > 30)
                {
                    jData.Add("MSG", "검색 기간은 30일을 초과할 수 없습니다.");
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
                jData.Add("RELEASE_STATE", ConvertUtil.ToInt32(leReceiptState.EditValue));
            }

            if(date == 0)
            {
                jData.Add("MSG", "접수일을 선택하지 않은 경우, 접수번호 또는 고객명은 필수로 입력되야 합니다.");
                return false;
            }

            return true;
        }

        private void sbRelease_Click(object sender, EventArgs e)
        {
            //if (Dangol.MessageYN($"현재 접수를 '출고'처리하시겠습니까?") == DialogResult.Yes)
            //{
               
            //    JObject jResult = new JObject();
            //    JObject jobj = new JObject();

            //    jobj.Add("RELEASE_ID", ConvertUtil.ToInt64(_currentReceipt["RELEASE_ID"]));

            //    if (DBUsedPurchase.ConsignedOneclickRelease(jobj, ref jResult))
            //    {
            //        _currentReceipt["RELEASE_STATE"] = "4";
            //        //_currentReceipt["RELEASE_STATE"] = receiptStatus.ToString(); 

            //        int rowHandle = gvReceipt.FocusedRowHandle;
            //        gvReceipt.FocusedRowHandle = -2147483646;
            //        gvReceipt.FocusedRowHandle = rowHandle;

            //        setStatistics();

            //        Dangol.Message("처리되었습니다.");
            //    }
            //    else
            //    {
            //        Dangol.Message(jResult["MSG"]);
            //        return;
            //    }

            //}
        }

        private void risePrice_EditValueChanging(object sender, ChangingEventArgs e)
        {

        }

        private void risePrice_EditValueChanged(object sender, EventArgs e)
        {
            

        }

        private void gvAdjustment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!e.Column.FieldName.Equals("TOTAL_PRICE"))
            {
                long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"])
                        + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_QUICK"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"]);

                _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice;
            }
        }

        private void gcReceip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                int rowHandle = gvReceipt.FocusedRowHandle;
                gvReceipt.FocusedRowHandle = -2147483646;
                gvReceipt.FocusedRowHandle = rowHandle;

                setStatistics();
            }
        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            int rowHandle = gvReceipt.FocusedRowHandle;
            gvReceipt.FocusedRowHandle = -2147483646;
            gvReceipt.FocusedRowHandle = rowHandle;

            getCustomerRefresh();

            setStatistics();
        }

        private void risePartPrice_EditValueChanged(object sender, EventArgs e)
        {

            SpinEdit View = sender as SpinEdit;
            long releasePartId = ConvertUtil.ToInt64(_currentPart["RELEASE_PART_ID"]);
            JObject jResult = new JObject();
            long partPrice = ConvertUtil.ToInt64(View.Value);

            if (partPrice < 0)
            {
                Dangol.Message("양수만 입력 가능합니다.");
                View.Value = _partPrice;
                return;
            }

            if (DBUsedPurchase.updatePrice(releasePartId, partPrice, ref jResult))
            {
                _dtAdjustMentProcess.BeginInit();
             
                long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
                long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

                _dtAdjustMentProcess.Rows[0]["PRICE"] = price - _partPrice + partPrice;
                _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - _partPrice + partPrice;

                _partPrice = partPrice;

                _dtAdjustMentProcess.EndInit();
            }


        }

        private void risePartPrice_EditValueChanging(object sender, ChangingEventArgs e)
        {
            //long proxyPartId = ConvertUtil.ToInt64(_currentPart["RELEASE_PART_ID"]);
            //JObject jResult = new JObject();
            //long oPArtPrice = ConvertUtil.ToInt64(e.OldValue);
            //long partPrice = ConvertUtil.ToInt64(e.NewValue);

            //if (partPrice < 0)
            //{
            //    e.Cancel = true;
            //    Dangol.Message("양수만 입력 가능합니다.");
            //    return;
            //}

            //if (DBConnect.updatePrice(proxyPartId, partPrice, ref jResult))
            //{
            //    _dtAdjustMentProcess.BeginInit();

            //    long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
            //    long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

            //    _dtAdjustMentProcess.Rows[0]["PRICE"] = price - oPArtPrice + partPrice;
            //    _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - oPArtPrice + partPrice;

            //    _dtAdjustMentProcess.EndInit();
            //}
        }

        private void teRequest_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void sbSave_Click(object sender, EventArgs e)
        {

            if (ConvertUtil.ToInt32(_currentReceipt["RELEASE_STATE"]) > 0)
            {
                Dangol.Warining("'접수' 상태인 경우만 수정가능합니다.");
                return;
            }

            if (Dangol.MessageYN("접수 정보를 수정하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("RELEASE_ID", $"{ _currentReceipt["RELEASE_ID"]}");
            jobj.Add("RELEASE_TYPE", $"{ _currentReceipt["RELEASE_TYPE"]}");
            jobj.Add("RELEASE_STATE", $"{ _currentReceipt["RELEASE_STATE"]}");
            jobj.Add("REQUEST", $"{ _currentReceipt["REQUEST"]}");

            if (DBUsedPurchase.updateUsedPurchaseRelease(jobj, ref jResult))
            {
                Dangol.Message("처리되었습니다.");
                return;
            }
            else
            {
                Dangol.Message($"{jResult["MSG"]}");
                return;
            }
        }

        private void lgcDelivery_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("배송정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("WAREHOUSING_ID", ConvertUtil.ToInt64(_currentReceipt["WAREHOUSING_ID"]));
                jobj.Add("RELEASE_ID", ConvertUtil.ToInt64(_currentReceipt["RELEASE_ID"]));
                jobj.Add("DELIVERY_COMPANY", ConvertUtil.ToString(leDelivery.EditValue));
                jobj.Add("DELIVERY_COMPANY_NM", leDelivery.Text);
                jobj.Add("INVOICE", teDelivery.Text.Trim());

                if (DBUsedPurchase.updateUsedPurchaseReleaseDeliveryInfo(jobj, ref jResult))
                {
                    _currentReceipt["DELIVERY_COMPANY"] = ConvertUtil.ToString(leDelivery.EditValue);
                    _currentReceipt["INVOICE"] = ConvertUtil.ToString(teDelivery.Text);

                    Dangol.Message("저장되었습니다.");
                }
                else
                {
                    Dangol.Error(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }
    }
}