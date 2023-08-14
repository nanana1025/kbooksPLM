using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.inventory;
using WareHousingMaster.view.usedPurchase;

namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedReturnList : DevExpress.XtraEditors.XtraForm
    {

        int _checkType = 2;
        string _componentCd;

        DataRowView _currentReceipt;
        DataRowView _currentPart;

        TreeListNode _currentReceiptPart;
        DataRowView _currentComponentPart;

        DataTable _dtReceipt;

        DataTable _dtReceiptPartGrid;
        DataTable _dtComponentList;
        DataTable _dtCoa;
        DataTable _dtAdjustMentProcess;

        DataTable _dtReturnCheck;

        //DataTable _dtAdjustmentHistory;

        DataTable _dtAdjustMentReturn;

        BindingSource _bsReceipt;
        BindingSource _bsReceiptPart;
        BindingSource _bsComponentList;
        BindingSource _bsCoa;
        BindingSource _bsReceiptPartGrid;
        BindingSource _bsAdjustMentProcess;
        BindingSource _bsAdjustMentReturn;
        //BindingSource _bsAdjustmentHistory;

        Dictionary<long, List<long>> _dicReceiptPart;
        Dictionary<long, List<long>> _dicConsignedModel;

        string[] _consignedComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "SSD", "HDD", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG" };
        string[] _consignedComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "SSD", "HDD", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스" };

        string[] _receiptStateCd = new string[] { "RECEIPT_DT", "PROCESS_DT", "PRODUCT_DT", "PACKAGE_DT", "RELEASE_DT", "RETURN_REQUEST_DT", "RETURN_IN_DT", "EXCHANGE_DT", "RETURN_CANCEL_DT", "RETURN_COMPLETE_DT" };
        Dictionary<int, string> _dicConsignedComponentCd;
        Dictionary<string, int> _dicConsignedComponentCdReverse;

        Dictionary<string, string> _dicProxyState;

        Dictionary<string, int> _dicReturnCheck;

        string _currentGetComponentCd;

        Dictionary<string, string> _dicProductType;
        Dictionary<string, string> _dicGuarantee;
        Dictionary<string, string> _dicReturnType;
        Dictionary<long, int> _dicModelSerialType;
        Dictionary<long, long> _dicErrorPart;

        List<string> _listProxyRange;

        List<string> _listErrorPart;

        int _proxyState;
        long _proxyId;
        long _pProxyId;
        long _id;
        long _partPrice = 0;
        long _inventoryId = 0;
        long _proxyReleasePartId = 0;
        int _refundType;
        string _receipt = "";

        bool _MbdPriceExist;
        bool _reUse;
        bool initialize = true;
        bool initializeEnter = true;
        public usrConsignedReturnList()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("P_PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("R_PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("C_PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("GUARANTEE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("GUARANTEE_FROM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("GUARANTEE_TO", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("SALE_ROOT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM1", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM2", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TEL1", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TEL2", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP1", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP2", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("POSTAL_CD", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REASON", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RETURN_PROCESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_DT", typeof(string)));


            _dtReceipt.Columns.Add(new DataColumn("OLD_COA", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("NEW_COA", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("OLD_COA_SN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("NEW_COA_SN", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("DELIVERY_COMPANY", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("COUPON_MANAGE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COUPON_CUSTOMER", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("RELEASE_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RETURN_STATE", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("RETURN_YN", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("AS_YN", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("REUSE_YN", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("PART_PRICE", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("REFUND_TYPE", typeof(short)));

            _dtReceipt.Columns.Add(new DataColumn("WORKER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PACKAGE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("B_GRADE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtReceipt.Columns.Add(new DataColumn("ERROR", typeof(string)));

            _dtReceiptPartGrid = new DataTable();
            _dtReceiptPartGrid.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PROXY_PART_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PROXY_RELEASE_PART_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("DETAIL_DATA", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("RETURN_TYPE", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PROXY_PRICE", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("RETURN_PRICE", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("ASSIGN_YN", typeof(bool)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtAdjustMentProcess = new DataTable();
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_DELIVERY", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_PRODUCE", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_QUICK", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_REPRODUCE", typeof(long)));
            _dtAdjustMentProcess.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtAdjustMentReturn = new DataTable();
            _dtAdjustMentReturn.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtAdjustMentReturn.Columns.Add(new DataColumn("PRICE_DELIVERY", typeof(long)));
            _dtAdjustMentReturn.Columns.Add(new DataColumn("PRICE_PRODUCE", typeof(long)));
            _dtAdjustMentReturn.Columns.Add(new DataColumn("PRICE_QUICK", typeof(long)));
            _dtAdjustMentReturn.Columns.Add(new DataColumn("PRICE_REPRODUCE", typeof(long)));
            _dtAdjustMentReturn.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));
            _dtAdjustMentReturn.Columns.Add(new DataColumn("PRICE_DELIVERY_O", typeof(long)));

            
            //_dtAdjustmentHistory = new DataTable();
            //_dtAdjustmentHistory.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            //_dtAdjustmentHistory.Columns.Add(new DataColumn("USER_ID", typeof(string)));

            _dtComponentList = new DataTable();
            _dtComponentList.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("VISIBLE", typeof(bool)));

            _dtCoa = new DataTable();
            _dtCoa.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtCoa.Columns.Add(new DataColumn("LICENCE", typeof(string)));
            _dtCoa.Columns.Add(new DataColumn("OS", typeof(string)));
            _dtCoa.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dtCoa.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtCoa.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));
            _dtCoa.Columns.Add(new DataColumn("CPU", typeof(string)));
            _dtCoa.Columns.Add(new DataColumn("COA", typeof(string)));
            _dtCoa.Columns.Add(new DataColumn("PRODUCT_NM", typeof(string)));

            //_dtReturnCheck = new DataTable();
            //_dtReturnCheck.Columns.Add(new DataColumn("CHECK_ID", typeof(long)));
            //_dtReturnCheck.Columns.Add(new DataColumn("PROXY_ID", typeof(long)));
            //_dtReturnCheck.Columns.Add(new DataColumn("PRICE_PRODUCE", typeof(int)));
            //_dtReturnCheck.Columns.Add(new DataColumn("PRICE_QUICK", typeof(int)));
            //_dtReturnCheck.Columns.Add(new DataColumn("PRICE_REPRODUCE", typeof(int)));
            //_dtReturnCheck.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(int)));
            //_dtReturnCheck.Columns.Add(new DataColumn("PRICE_DELIVERY_O", typeof(int)));
            //_dtReturnCheck.Columns.Add(new DataColumn("PRICE_DELIVERY_O", typeof(int)));
            //_dtReturnCheck.Columns.Add(new DataColumn("PRICE_DELIVERY_O", typeof(int)));



            _bsReceipt = new BindingSource();
            _bsReceiptPart = new BindingSource();
            _bsComponentList = new BindingSource();
            _bsCoa = new BindingSource();
            _bsReceiptPartGrid = new BindingSource();
            _bsAdjustMentProcess = new BindingSource();
            _bsAdjustMentReturn = new BindingSource();

            _dicReceiptPart = new Dictionary<long, List<long>>();
            _dicConsignedModel = new Dictionary<long, List<long>>();

            _dicProductType = new Dictionary<string, string>();
            _dicGuarantee = new Dictionary<string, string>();
            _dicProxyState = new Dictionary<string, string>();
            _dicReturnType = new Dictionary<string, string>();
            _dicModelSerialType = new Dictionary<long, int>();
            _dicReturnCheck = new Dictionary<string, int>();
            _dicErrorPart = new Dictionary<long, long>();
            _id = 0;

            _dicConsignedComponentCd = new Dictionary<int, string>();
            _dicConsignedComponentCdReverse = new Dictionary<string, int>();

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                _dicConsignedComponentCd.Add(i, _consignedComponetCd[i]);
                _dicConsignedComponentCdReverse.Add(_consignedComponetCd[i], i);
            }

            _currentGetComponentCd = null;
            _proxyState = -1;

            _listProxyRange = new List<string>();
            _listErrorPart = new List<string>();

            initialize = true;
            initializeEnter = true;

        }


        private void usrConsignedReturnList_Load(object sender, EventArgs e)
        {

            //if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
            //    lcRelease.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //else
            //    lcRelease.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            setInfoBox();
            setIInitData();
            setGridControl();
            setStatistics();

            teReceipt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT", false, DataSourceUpdateMode.OnPropertyChanged));
            leProductType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "PRODUCT_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            deReceiptDt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT_DT", false, DataSourceUpdateMode.OnPropertyChanged));
            leGuarantee.DataBindings.Add(new Binding("EditValue", _bsReceipt, "GUARANTEE", false, DataSourceUpdateMode.OnPropertyChanged));
            deGuaranteeFrom.DataBindings.Add(new Binding("Text", _bsReceipt, "GUARANTEE_FROM", false, DataSourceUpdateMode.OnPropertyChanged));
            deGuaranteeTo.DataBindings.Add(new Binding("Text", _bsReceipt, "GUARANTEE_TO", false, DataSourceUpdateMode.OnPropertyChanged));
            leComapanyReceipt.DataBindings.Add(new Binding("EditValue", _bsReceipt, "COMPANY_ID", false, DataSourceUpdateMode.OnPropertyChanged));
            leSaleRoot.DataBindings.Add(new Binding("EditValue", _bsReceipt, "SALE_ROOT", false, DataSourceUpdateMode.OnPropertyChanged));
            rgReleaseType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RELEASE_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            teModelNmDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "MODEL_NM_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));

            meDes.DataBindings.Add(new Binding("Text", _bsReceipt, "DES", false, DataSourceUpdateMode.OnPropertyChanged));
            teRequest.DataBindings.Add(new Binding("Text", _bsReceipt, "REQUEST", false, DataSourceUpdateMode.OnPropertyChanged));

            //teCustomerNm1.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM1", false, DataSourceUpdateMode.OnPropertyChanged));
            teCustomerNm2.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM2", false, DataSourceUpdateMode.OnPropertyChanged));
            //teTel1.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL1", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel2.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL2", false, DataSourceUpdateMode.OnPropertyChanged));
            
            teHp2.DataBindings.Add(new Binding("Text", _bsReceipt, "HP2", false, DataSourceUpdateMode.OnPropertyChanged));
            tePostalCd.DataBindings.Add(new Binding("Text", _bsReceipt, "POSTAL_CD", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddress.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddressDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));

            rgProcess.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RELEASE_STATE", false, DataSourceUpdateMode.OnPropertyChanged));
            rgReturn.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RETURN_STATE", false, DataSourceUpdateMode.OnPropertyChanged));

            leDelivery.DataBindings.Add(new Binding("EditValue", _bsReceipt, "DELIVERY_COMPANY", false, DataSourceUpdateMode.OnPropertyChanged));
            teDelivery.DataBindings.Add(new Binding("Text", _bsReceipt, "INVOICE", false, DataSourceUpdateMode.OnPropertyChanged));
            teManageNo.DataBindings.Add(new Binding("Text", _bsReceipt, "COUPON_MANAGE", false, DataSourceUpdateMode.OnPropertyChanged));
            teCustomerNo.DataBindings.Add(new Binding("Text", _bsReceipt, "COUPON_CUSTOMER", false, DataSourceUpdateMode.OnPropertyChanged));

            teRetuenReason.DataBindings.Add(new Binding("Text", _bsReceipt, "REASON", false, DataSourceUpdateMode.OnPropertyChanged));
            teRetuenProcess.DataBindings.Add(new Binding("Text", _bsReceipt, "RETURN_PROCESS", false, DataSourceUpdateMode.OnPropertyChanged));

            rgReturnType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "REFUND_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));


            var today = DateTime.Today;
            var pastDate = today.AddDays(-15);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            deComplete.EditValue = today;
            leReceiptState.ItemIndex = 0;

            gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
            getReceiptList(true);
            gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
            if (_dtReceipt.Rows.Count > 0)
            {
                gvReceipt.FocusedRowHandle = -2147483646;
                gvReceipt.MoveFirst();
            }

            initialize = false;

            if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                lcgReceipt.CustomHeaderButtons[0].Properties.Visible = true;
            else
                lcgReceipt.CustomHeaderButtons[0].Properties.Visible = false;
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
            DataTable dtProxyState = Util.getCodeList("CD0902", "KEY", "VALUE");
            Util.LookupEditHelper(rileProxyState, dtProxyState, "KEY", "VALUE");

            RadioGroupItem[] rgProcessState = new RadioGroupItem[7];
            RadioGroupItem[] rgReturnState = new RadioGroupItem[5];
            RadioGroupItem[] rgASState = new RadioGroupItem[5];

            int indexProcess = 0;
            int indexReturn = 0;
            int indexAS = 0;

            for (int i = 0; i < dtProxyState.Rows.Count - 3; i++)
            {
                RadioGroupItem rgItem = new RadioGroupItem(dtProxyState.Rows[i]["KEY"], ConvertUtil.ToString(dtProxyState.Rows[i]["VALUE"]), true, dtProxyState.Rows[i]["KEY"]);

                _dicProxyState.Add(ConvertUtil.ToString(dtProxyState.Rows[i]["KEY"]), ConvertUtil.ToString(dtProxyState.Rows[i]["VALUE"]));

                if (i < 5)
                    rgProcessState[indexProcess++] = rgItem;
                else if (i > 4 && i < 10)
                    rgReturnState[indexReturn++] = rgItem;
                else
                    rgASState[indexAS++] = rgItem;
            }

            RadioGroupItem rgItemHold = new RadioGroupItem("90", "보류", false, "90");
            _dicProxyState.Add("90", "보류");

            RadioGroupItem rgItemCancel = new RadioGroupItem("91", "취소", false, "91");
            _dicProxyState.Add("91", "취소");

            rgProcessState[indexProcess++] = rgItemHold;
            rgProcessState[indexProcess++] = rgItemCancel;

            this.rgProcess.Properties.Items.AddRange(rgProcessState);
            this.rgReturn.Properties.Items.AddRange(rgReturnState);
            //this.rgAS.Properties.Items.AddRange(rgASState);

            DataTable dtProxyReturnState = new DataTable();
            dtProxyReturnState.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtProxyReturnState.Columns.Add(new DataColumn("VALUE", typeof(string)));
            foreach (DataRow row in dtProxyState.Rows)
            {
                string stateS = ConvertUtil.ToString(row["KEY"]);
                if (!stateS.Equals("D"))
                {
                    int state = ConvertUtil.ToInt32(row["KEY"]);

                    if (state > 4 && state < 10)
                    {
                        Util.insertRow(dtProxyReturnState, stateS, ConvertUtil.ToString(row["VALUE"]));
                        _listProxyRange.Add(stateS);
                    }
                }

            }
            Util.insertRowonTop(dtProxyReturnState, "-1", "선택안함");
            Util.LookupEditHelper(leReceiptState, dtProxyReturnState, "KEY", "VALUE");

            DataTable dtProductType = new DataTable();
            dtProductType = Util.getCodeList("CD0903", "KEY", "VALUE");
            Util.insertRowonTop(dtProductType, "-1", "선택안함");
            Util.LookupEditHelper(leProductType, dtProductType, "KEY", "VALUE");

            foreach (DataRow row in dtProductType.Rows)
                _dicProductType.Add(ConvertUtil.ToString(row["VALUE"]), ConvertUtil.ToString(row["KEY"]));

            DataTable dtGuarantee = new DataTable();
            dtGuarantee = Util.getCodeList("CD0906", "KEY", "VALUE");
            Util.insertRowonTop(dtGuarantee, "0", "선택안함");
            Util.LookupEditHelper(leGuarantee, dtGuarantee, "KEY", "VALUE");

            foreach (DataRow row in dtGuarantee.Rows)
                _dicGuarantee.Add(ConvertUtil.ToString(row["VALUE"]), ConvertUtil.ToString(row["KEY"]));
            

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "없음");
            Util.LookupEditHelper(leComapanyReceipt, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            Util.LookupEditHelper(rileUser, ProjectInfo._dtUserId, "KEY", "VALUE");        

            DataTable dtSaleRoot = new DataTable();
            dtSaleRoot = Util.getCodeList("CD0905", "KEY", "VALUE");
            Util.insertRowonTop(dtSaleRoot, "-1", "선택안함");
            Util.LookupEditHelper(leSaleRoot, dtSaleRoot, "KEY", "VALUE");

            DataTable dtReturnType= Util.getCodeList("CD0907", "KEY", "VALUE");
            Util.LookupEditHelper(rileReturnType, dtReturnType, "KEY", "VALUE");

            foreach (DataRow row in dtReturnType.Rows)
                _dicReturnType.Add(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));


            DataTable dtDelivery = Util.getCodeList("CD0904", "KEY", "VALUE");
            Util.insertRowonTop(dtDelivery, "-1", "없음");
            Util.LookupEditHelper(leDelivery, dtDelivery, "KEY", "VALUE");


            DataTable dtOS = new DataTable();
            dtOS.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtOS.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtOS, "1", "WIN 7");
            Util.insertRowonTop(dtOS, "2", "WIN 8");
            Util.insertRowonTop(dtOS, "3", "WIN 10");

            Util.LookupEditHelper(rileOs, dtOS, "KEY", "VALUE");

            DataTable dtRefundType = new DataTable();
            dtRefundType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtRefundType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtRefundType, "2", "환불");
            Util.insertRowonTop(dtRefundType, "1", "교환");
            

            Util.LookupEditHelper(rileRefundType, dtRefundType, "KEY", "VALUE");


            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                DataRow row = dtComponentCd.NewRow();
                row["KEY"] = _consignedComponetCd[i];
                row["VALUE"] = _consignedComponetNm[i];
                dtComponentCd.Rows.Add(row);
            }
            //Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtConsignedType = new DataTable();

            dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtConsignedType, 1, "생산대행");
            Util.insertRowonTop(dtConsignedType, 2, "자사재고");
            Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");

            DataTable dtSerialNoType = Util.getCodeListCustom("TN_MODEL_LIST", "MODEL_LIST_ID", "SERIAL_NO_TYPE", "DEL_YN = 'N'", "MODEL_LIST_ID ASC");

            foreach (DataRow row in dtSerialNoType.Rows)
            {
                _dicModelSerialType.Add(ConvertUtil.ToInt64(row["KEY"]), ConvertUtil.ToInt32(row["VALUE"]));
            }

            //DataTable dtState = Util.getCodeList("CD0902", "KEY", "VALUE");



            DataRow dr1 = _dtCoa.NewRow();
            dr1["TYPE"] = 1;
            dr1["LICENCE"] = "OLD";
            dr1["OS"] = "";
            dr1["MANUFACTURE_NM"] = "";
            dr1["MODEL_NM"] = "";
            dr1["SERIAL_NO"] = "";
            dr1["CPU"] = "";
            dr1["COA"] = "";
            dr1["PRODUCT_NM"] = "";
            _dtCoa.Rows.Add(dr1);

            DataRow dr2 = _dtCoa.NewRow();
            dr2["TYPE"] = 2;
            dr2["LICENCE"] = "NEW";
            dr2["OS"] = "";
            dr2["MANUFACTURE_NM"] = "";
            dr2["MODEL_NM"] = "";
            dr2["SERIAL_NO"] = "";
            dr2["CPU"] = "";
            dr2["COA"] = "";
            dr2["PRODUCT_NM"] = "";
            _dtCoa.Rows.Add(dr2);

            DataRow dr3 = _dtAdjustMentProcess.NewRow();
            dr3["PRICE"] = 0;
            dr3["PRICE_DELIVERY"] = 0;
            dr3["PRICE_PRODUCE"] = 0;
            dr3["PRICE_QUICK"] = 0;
            dr3["PRICE_REPRODUCE"] = 0;
            dr3["TOTAL_PRICE"] = 0;
            _dtAdjustMentProcess.Rows.Add(dr3);

            DataRow dr4 = _dtAdjustMentReturn.NewRow();
            dr4["PRICE"] = 0;
            dr4["PRICE_DELIVERY"] = 0;
            dr4["PRICE_PRODUCE"] = 0;
            dr4["PRICE_QUICK"] = 0;
            dr4["PRICE_REPRODUCE"] = 0;
            dr4["TOTAL_PRICE"] = 0;
            dr4["PRICE_DELIVERY_O"] = 0;
            
            _dtAdjustMentReturn.Rows.Add(dr4);

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;
            //_bsReceiptPart.DataSource = _dtReceiptPart;
            _bsComponentList.DataSource = _dtComponentList;
            _bsCoa.DataSource = _dtCoa;
            _bsReceiptPartGrid.DataSource = _dtReceiptPartGrid;
            _bsAdjustMentProcess.DataSource = _dtAdjustMentProcess;
            _bsAdjustMentReturn.DataSource = _dtAdjustMentReturn;

            leCompany.EditValue = "-1";

            if (ProjectInfo._userType.Equals("E"))
            {
                rgReturn.ReadOnly = true;

                leCompany.EditValue = ProjectInfo._userCompanyId.ToString();

                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCompanyId.Visible = false;

                gcCheck.Visible = false;

                rgReturnType.ReadOnly = true;


                gvLicence.OptionsBehavior.ReadOnly = true;
                gvAdjustmentReturn.OptionsBehavior.ReadOnly = true;
                gvPart.OptionsBehavior.ReadOnly = true;

                leDelivery.ReadOnly = true;
                teDelivery.ReadOnly = true;

                teManageNo.ReadOnly = true;
                teCustomerNo.ReadOnly = true;

                for (int i = 0; i < lcgReceipt.CustomHeaderButtons.Count - 1; i++)
                    lcgReceipt.CustomHeaderButtons[i].Properties.Visible = false;

                for (int i = 0; i < lgcReceiptPart.CustomHeaderButtons.Count; i++)
                    lgcReceiptPart.CustomHeaderButtons[i].Properties.Visible = false;

                lcgDelivery.CustomHeaderButtons[0].Properties.Visible = false;
                lcgReturnAdjustment.CustomHeaderButtons[0].Properties.Visible = false;
                lgcComponentList.CustomHeaderButtons[0].Properties.Visible = false;
            }


        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsReceipt;


            gcLicence.DataSource = null;
            gcLicence.DataSource = _bsCoa;

            gcPart.DataSource = null;
            gcPart.DataSource = _bsReceiptPartGrid;

            gcAdjustment.DataSource = null;
            gcAdjustment.DataSource = _bsAdjustMentProcess;

            gcAdjustmentReturn.DataSource = null;
            gcAdjustmentReturn.DataSource = _bsAdjustMentReturn;
        }


        private void setStatistics()
        {
            JObject jResult = new JObject();

            if (DBConsigned.getConsignedReceiptStatistics(ref jResult))
            {
                teReceiptCnt.Text = $"{jResult["RETURN_RECEIPT_CNT"]}";
                teWarehousingCnt.Text = $"{jResult["RETURN_WAREHOUSING_CNT"]}";             
                teExchangeCnt.Text = $"{jResult["RETURN_RELEASE_CNT"]}";

                //teHoldCnt.Text = $"{jResult["HOLD_CNT"]}";
                //tePackageCnt.Text = $"{jResult["PACKAGE_CNT"]}";
            }
        }

        private void leGuarantee_EditValueChanged(object sender, EventArgs e)
        {
            int month = ConvertUtil.ToInt32(leGuarantee.EditValue);
            string from = ConvertUtil.ToString(deGuaranteeFrom.EditValue);
            if (!string.IsNullOrEmpty(from))
            {
                DateTime date = Convert.ToDateTime(from);
                var futureDate = date.AddMonths(month);

                //deGuaranteeFrom.EditValue = today;
                deGuaranteeTo.EditValue = futureDate;
            }
        }

        private void setAdjustmentReadonly()
        {
            if (!ProjectInfo._userType.Equals("E"))
            {
                if (_proxyState < 8)
                {
                    //lcgCoupon.CustomHeaderButtons[0].Properties.Enabled = true;
                    lcgDelivery.CustomHeaderButtons[0].Properties.Enabled = true;
                    leDelivery.ReadOnly = false;
                    teDelivery.ReadOnly = false;

                    gvAdjustmentReturn.OptionsBehavior.ReadOnly = false;


                }
                else
                {
                    //lcgCoupon.CustomHeaderButtons[0].Properties.Enabled = false;
                    lcgDelivery.CustomHeaderButtons[0].Properties.Enabled = false;
                    leDelivery.ReadOnly = true;
                    teDelivery.ReadOnly = true;

                    gvAdjustmentReturn.OptionsBehavior.ReadOnly = true;


                }

                if (_proxyState == 5 || _proxyState == 6)
                {
                    gvAdjustment.OptionsBehavior.Editable = true;
                    lgcComponentList.CustomHeaderButtons[0].Properties.Enabled = true;
                    //lgcLicence.CustomHeaderButtons[0].Properties.Enabled = true;
                    //gcPrice.OptionsColumn.AllowEdit = true;

                    lcgReceipt.CustomHeaderButtons[1].Properties.Enabled = true;

                    rgReturnType.Properties.ReadOnly = false;
                }
                else
                {
                    gvAdjustment.OptionsBehavior.Editable = false;
                    lgcComponentList.CustomHeaderButtons[0].Properties.Enabled = false;
                    //lgcLicence.CustomHeaderButtons[0].Properties.Enabled = false;
                    //gcPrice.OptionsColumn.AllowEdit = false;

                    lcgReceipt.CustomHeaderButtons[1].Properties.Enabled = false;
                    rgReturnType.Properties.ReadOnly = true;
                }

                if (_proxyState == 6)
                {
                    lcgReturnAdjustment.CustomHeaderButtons[0].Properties.Enabled = true;

                }
                else
                {
                    lcgReturnAdjustment.CustomHeaderButtons[0].Properties.Enabled = false;

                }
            }
        }

        private void setPartReadonly()
        {
            if (!ProjectInfo._userType.Equals("E"))
            {
                lgcReceiptPart.BeginUpdate();
                if (_proxyState == 6)
                {
                    for (int i = 0; i < lgcReceiptPart.CustomHeaderButtons.Count; i++)
                        lgcReceiptPart.CustomHeaderButtons[i].Properties.Visible = true;
                }
                else
                {
                    for (int i = 0; i < lgcReceiptPart.CustomHeaderButtons.Count; i++)
                        lgcReceiptPart.CustomHeaderButtons[i].Properties.Visible = false;
                }
                lgcReceiptPart.EndUpdate();
            }
        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);
            _dtComponentList.Clear();

            if (isValidRow)
            {
                if (!initialize)
                    Dangol.ShowSplash();

                _currentReceipt = e.Row as DataRowView;

                _proxyState = ConvertUtil.ToInt32(_currentReceipt["PROXY_STATE"]);
                _proxyId = ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]);
                _pProxyId = ConvertUtil.ToInt64(_currentReceipt["P_PROXY_ID"]);
                _receipt = ConvertUtil.ToString(_currentReceipt["RECEIPT"]);
                _refundType = ConvertUtil.ToInt32(_currentReceipt["REFUND_TYPE"]);
                _reUse = ConvertUtil.ToInt32(_currentReceipt["REUSE_YN"]) == 1 ? true : false;

                _MbdPriceExist = false;
                //rgReturnType.EditValue = ConvertUtil.ToInt16(_currentReceipt["REFUND_TYPE"]);
                lgcProxy.BeginInit();
                lcgLicence.BeginInit();
                _currentReceipt.BeginEdit();
                
                getConsignedInfo();
                getLicenceInfo();
                getProcessAdjustmentInfo();
                getReturnAdjustmentInfo();

                if (!ProjectInfo._userType.Equals("E"))
                    setAdjustmentReadonly();


                if (ConvertUtil.ToInt32(_currentReceipt["RETURN_YN"]) == 1)
                {
                    //rgAS.ReadOnly = true;

                    //if (ConvertUtil.ToInt32(_currentReceipt["AS_YN"]) == 1)
                    //{
                    //    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //    //lcAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //}
                    //else
                    //{
                    //    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //    //lcAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //}
                }
                else
                {
                    //rgAS.ReadOnly = true;
                    //lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //lcAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                


                _currentReceipt.EndEdit();
                lgcProxy.EndInit();
                lcgLicence.EndInit();
                //setReceiptPart(_currentReceipt["ID"]);

                //if (ConvertUtil.ToBoolean(_currentReceipt["CHECK"]))
                //    teError.Text = "";


                //rgProcess.EditValue = "1";


                if(ConvertUtil.ToInt32(_currentReceipt["B_GRADE"]) == 1)
                    lcBGrade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    lcBGrade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if (_reUse)
                    lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if (!ProjectInfo._userType.Equals("E"))
                    setPartReadonly();

                if (!initialize)
                    Dangol.CloseSplash();
            }
            else
            {


                _currentReceipt = null;
                _dtReceiptPartGrid.Clear();
                _dtComponentList.Clear();
                lcBGrade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                _proxyId = -1;
                _pProxyId = -1;
                _receipt = "";
                _refundType = -1;
                _reUse = false;
            }
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentPart = e.Row as DataRowView;
                _partPrice = ConvertUtil.ToInt64(_currentPart["PROXY_PRICE"]);

                _componentCd = ConvertUtil.ToString(_currentPart["COMPONENT_CD"]);
                _proxyReleasePartId = ConvertUtil.ToInt64(_currentPart["PROXY_RELEASE_PART_ID"]);
                _inventoryId = ConvertUtil.ToInt64(_currentPart["INVENTORY_ID"]);

                if (_proxyState == 6)
                {
                    if(_componentCd.Equals("AIR") || _componentCd.Equals("PKG"))
                        gcReturnType.OptionsColumn.AllowEdit = false;
                    else
                    {
                        if(ConvertUtil.ToBoolean(_currentPart["ASSIGN_YN"]))
                            gcReturnType.OptionsColumn.AllowEdit = true;
                        else
                            gcReturnType.OptionsColumn.AllowEdit = false;
                    }
                }
                else
                    gcReturnType.OptionsColumn.AllowEdit = false;

                if (ConvertUtil.ToInt32(_currentPart["CONSIGNED_TYPE"]) == 2)
                    gvPart.Appearance.FocusedRow.ForeColor = Color.Red;
                else
                    gvPart.Appearance.FocusedRow.ForeColor = Color.Black;
            }
            else
            {
                _partPrice = 0;
                _proxyReleasePartId = -1;
                _inventoryId = -1;
                _currentPart = null;
                gcReturnType.OptionsColumn.AllowEdit = false;
            }
        }

        private bool saveAdjustmentInfo()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
            jobj.Add("ADJUSTMENT_TYPE", 3);
            jobj.Add("REFUND_TYPE", ConvertUtil.ToInt16(rgReturnType.EditValue));
            jobj.Add("PRICE", ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE"]) * -1);
            jobj.Add("PRICE_PRODUCE", ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"]) * -1);
            jobj.Add("PRICE_REPRODUCE", ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"]) * -1);
            jobj.Add("PRICE_DELIVERY", ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"]) * -1);
            jobj.Add("PRICE_QUICK", ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_QUICK"]) * -1);
            jobj.Add("REGISTER_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (DBConsigned.updateAdjust(jobj, ref jResult))
            {
                //DataRow row = _dtAdjustmentHistory.NewRow();
                //row["UPDATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                //row["USER_ID"] = ProjectInfo._userId;
                //_dtAdjustmentHistory.Rows.InsertAt(row, 0);

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

        }

        private void getReceiptPart()
        {
            _dtReceiptPartGrid.Clear();

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", _pProxyId);
            jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"]));
            jobj.Add("RECEIPT_DT", ConvertUtil.ToString(_currentReceipt["RECEIPT_DT"]));

            if (DBConsigned.getConsignedReceiptPart(jobj, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                long price = 0;
                long componentId = 0;
                int releaseCnt = 0;
                bool assignYn = false;

                int returnType = 0;
                string componentCd;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    //DataRow dr = _dtReceiptPart.NewRow();

                    componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                    releaseCnt = ConvertUtil.ToInt32(obj["RELEASE_CNT"]);
                    assignYn = releaseCnt > 0 ? true : false;

                    returnType = ConvertUtil.ToInt32(obj["RETURN_TYPE"]);
                    componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                    DataRow drGrid = _dtReceiptPartGrid.NewRow();

                    drGrid["NO"] = index++;
                    drGrid["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    drGrid["PROXY_RELEASE_PART_ID"] = obj["PROXY_RELEASE_PART_ID"];
                    drGrid["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    drGrid["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                    drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    drGrid["DETAIL_DATA"] = obj["DETAIL_DATA"];
                    drGrid["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    drGrid["RETURN_TYPE"] = ConvertUtil.ToInt32(obj["RETURN_TYPE"]);
                    drGrid["ASSIGN_YN"] = assignYn;
                    drGrid["PROXY_PRICE"] = obj["PROXY_PRICE"];

                    if (componentCd.Equals("AIR") || componentCd.Equals("PKG"))
                    {
                        drGrid["RETURN_PRICE"] = 0;
                    }
                    else
                    {
                        if (returnType == 0)
                            drGrid["RETURN_PRICE"] = 0;
                        else
                            drGrid["RETURN_PRICE"] = obj["PROXY_PRICE"];
                    }
                    drGrid["CHECK"] = false;

                    _dtReceiptPartGrid.Rows.Add(drGrid);

                    price += ConvertUtil.ToInt64(drGrid["RETURN_PRICE"]);

                }

                _currentReceipt["PART_PRICE"] = price;

                gvAdjustmentReturn.BeginDataUpdate();
                _dtAdjustMentReturn.Rows[0].BeginEdit();

                price *= -1;
                _dtAdjustMentReturn.Rows[0]["PRICE"] = price;

                if (_refundType == 1)
                    _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
                else
                    _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY_O"];

                long totalPrice = price + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"])
                                                + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_QUICK"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"]);

                _dtAdjustMentReturn.Rows[0]["TOTAL_PRICE"] = totalPrice;

                _dtAdjustMentReturn.Rows[0].EndEdit();
                gvAdjustmentReturn.EndDataUpdate();
            }
        }




        private void getConsignedInfo()
        {
            //gvReceipt.BeginDataUpdate();
            //_dtReceiptPart.BeginInit();

            //_dtReceiptPart.Clear();
            _dtReceiptPartGrid.Clear();
            lbNote.Text = "";
            JObject jResult = new JObject();
            //JObject jobj = new JObject();

            //jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_proxyId));
            //jobj.Add("RETURN_YN", 1);

            if (DBConsigned.getConsignedInfo(_proxyId, ref jResult))
            {
                int nId = ConvertUtil.ToInt32(_currentReceipt["ID"]);

                _currentReceipt["PROXY_STATE"] = jResult["PROXY_STATE"];
                _currentReceipt["RELEASE_STATE"] = jResult["RELEASE_STATE"];
                _currentReceipt["RETURN_STATE"] = jResult["RETURN_STATE"];

                _currentReceipt["PRODUCT_TYPE"] = jResult["PC_TYPE"];
                _currentReceipt["GUARANTEE"] = jResult["GUARANTEE_DUE"];
                _currentReceipt["GUARANTEE_FROM"] = jResult["GUARANTEE_START"];
                _currentReceipt["GUARANTEE_TO"] = jResult["GUARANTEE_END"];
                _currentReceipt["SALE_ROOT"] = jResult["SALE_ROOT"];
                _currentReceipt["RELEASE_TYPE"] = jResult["RELEASE_TYPE"];
                _currentReceipt["MODEL_NM"] = jResult["MODEL_NM"];
                _currentReceipt["MODEL_NM_DETAIL"] = jResult["MODEL_NM_DETAIL"];
                _currentReceipt["DES"] = jResult["DES"];
                _currentReceipt["REQUEST"] = jResult["REQUEST"];        

                _currentReceipt["CUSTOMER_NM1"] = jResult["CUSTOMER_NM_S"];
                _currentReceipt["CUSTOMER_NM2"] = jResult["CUSTOMER_NM_R"];
                _currentReceipt["TEL1"] = jResult["TEL_S"];
                _currentReceipt["TEL2"] = jResult["TEL_R"];
                _currentReceipt["HP1"] = jResult["MOBILE_S"];
                _currentReceipt["HP2"] = jResult["MOBILE_R"];
                _currentReceipt["POSTAL_CD"] = jResult["POSTAL_CD"];
                _currentReceipt["ADDRESS"] = jResult["ADDRESS"];
                _currentReceipt["ADDRESS_DETAIL"] = jResult["ADDRESS_DETAIL"];
                _currentReceipt["REASON"] = jResult["RETURN_REASON"];
                _currentReceipt["RETURN_PROCESS"] = jResult["RETURN_PROCESS"];
                

                _currentReceipt["COUPON_MANAGE"] = jResult["COUPON_MANAGE"];
                _currentReceipt["COUPON_CUSTOMER"] = jResult["COUPON_CUSTOMER"];
                _currentReceipt["OLD_COA"] = jResult["OLD_COA"];
                _currentReceipt["NEW_COA"] = jResult["NEW_COA"];
                _currentReceipt["OLD_COA_SN"] = jResult["OLD_COA_SN"];
                _currentReceipt["NEW_COA_SN"] = jResult["NEW_COA_SN"];
                _currentReceipt["RELEASE_STATE"] = jResult["RELEASE_STATE"];            
                _currentReceipt["RETURN_STATE"] = jResult["RETURN_STATE"];
                _currentReceipt["RETURN_YN"] = jResult["RETURN_YN"];
                _currentReceipt["AS_YN"] = jResult["AS_YN"];
                
                
                //_currentReceipt["WORKER_ID"] = jResult["WORKER_ID"];
                //_currentReceipt["PRODUCT_ID"] = jResult["PRODUCT_ID"];
                //_currentReceipt["PACKAGE_ID"] = jResult["PACKAGE_ID"];
                //_currentReceipt["COMPLETE_ID"] = jResult["COMPLETE_ID"];

                if (ConvertUtil.ToString(jResult["INVOICE"]).Equals("[]"))
                {
                    _currentReceipt["DELIVERY_COMPANY"] = "-1";
                    _currentReceipt["INVOICE"] = "";
                }
                else
                {
                    JArray jArrayInvoice = JArray.Parse(jResult["INVOICE"].ToString());
                    foreach (JObject obj in jArrayInvoice.Children<JObject>())
                    {
                        _currentReceipt["DELIVERY_COMPANY"] = obj["DELIVERY_COMPANY"];
                        _currentReceipt["INVOICE"] = obj["INVOICE"];

                        break;
                    }
                }

                //for (int i = 0; i < _consignedComponetCd.Length; i++)
                //{
                //    DataRow row = _dtReceiptPart.NewRow();

                //    row["ID"] = nId;
                //    row["P_PART_ID"] = -1;
                //    row["PART_ID"] = i;
                //    row["COMPONENT_ID"] = -1;
                //    row["CONSIGNED_TYPE"] = -1;
                //    row["COMPONENT_CD"] = _consignedComponetCd[i];
                //    row["COMPONENT_CD_T"] = _consignedComponetCd[i];
                //    //row["MODEL_NM"] = _consignedComponetNm[i];
                //    row["MODEL_NM"] = "";
                //    row["PART_CNT"] = 0;

                //    _dtReceiptPart.Rows.Add(row);
                //}

                List<long> listPart = new List<long>();
                List<long> listModel = new List<long>();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;

                long price = 0;
                long componentId = 0;
                int releaseCnt = 0;
                bool assignYn = false;

                int returnType = 0;
                string componentCd;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                    if (componentCd.Equals("MBD"))
                        lbNote.Text = $"{obj["NOTE_NM"]}";

                    returnType = ConvertUtil.ToInt32(obj["RETURN_TYPE"]);

                    listPart.Add(ConvertUtil.ToInt64(obj["COMPONENT_ID"]));

                    long partId = _dicConsignedComponentCdReverse[componentCd];

                    //DataRow dr = _dtReceiptPart.NewRow();

                    componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                    releaseCnt = ConvertUtil.ToInt32(obj["RELEASE_CNT"]);
                    assignYn = releaseCnt > 0 ? true : false;

                    //dr["ID"] = ConvertUtil.ToInt64(obj["ID"]);
                    //dr["P_PART_ID"] = partId;
                    //dr["PART_ID"] = obj["COMPONENT_ID"];
                    //dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                    //dr["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    //dr["COMPONENT_CD_T"] = obj["COMPONENT_CD"];
                    //dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    //dr["MODEL_NM"] = obj["MODEL_NM"];
                    //dr["PART_CNT"] = obj["COMPONENT_CNT"];
                    //dr["ASSIGN_YN"] = assignYn;

                    //_dtReceiptPart.Rows.Add(dr);


                    DataRow drGrid = _dtReceiptPartGrid.NewRow();

                    drGrid["NO"] = index++;
                    drGrid["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    drGrid["PROXY_RELEASE_PART_ID"] = obj["PROXY_RELEASE_PART_ID"];
                    drGrid["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    drGrid["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                    drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    drGrid["DETAIL_DATA"] = obj["DETAIL_DATA"];
                    drGrid["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    drGrid["RETURN_TYPE"] = ConvertUtil.ToInt32(obj["RETURN_TYPE"]);
                    drGrid["ASSIGN_YN"] = assignYn;
                    drGrid["PROXY_PRICE"] = obj["PROXY_PRICE"];

                    if (componentCd.Equals("MBD"))
                        if (ConvertUtil.ToInt64(obj["PROXY_PRICE"]) > 0)
                            _MbdPriceExist = true;

                    if (componentCd.Equals("AIR") || componentCd.Equals("PKG"))
                    {
                        drGrid["RETURN_PRICE"] = 0;
                    }
                    else {
                        if (returnType == 0)
                            drGrid["RETURN_PRICE"] = 0;
                        else
                            drGrid["RETURN_PRICE"] = obj["PROXY_PRICE"];
                    }
                    drGrid["CHECK"] = false;

                    _dtReceiptPartGrid.Rows.Add(drGrid);

                    price += ConvertUtil.ToInt64(drGrid["RETURN_PRICE"]);

                    //DataRow[] rows = _dtReceiptPart.Select($"ID = {nId} AND PART_ID = {partId} AND P_PART_ID = -1");

                    //foreach (DataRow row in rows)
                    //{
                    //    int partCnt = ConvertUtil.ToInt32(row["PART_CNT"]);
                    //    partCnt++;
                    //    row["PART_CNT"] = partCnt;
                    //}
                }

                _currentReceipt["PART_PRICE"] = price;

                //if (_dicReceiptPart.ContainsKey(_id))
                //    _dicReceiptPart[_id] = listPart;
                //else
                //    _dicReceiptPart.Add(_id, listPart);

            }

            //_dtReceiptPart.EndInit();
            //gvReceipt.EndDataUpdate();
        }

        private void getLicenceInfo()
        {
            JObject jResult = new JObject();
            JObject jojb = new JObject();
            jojb.Add("PROXY_ID", ConvertUtil.ToInt64(_pProxyId));

            string receiptDt = ConvertUtil.ToString(_currentReceipt["RECEIPT_DT"]);
            DateTime dtDate = Convert.ToDateTime(receiptDt);
            DateTime expiredDt = dtDate.AddMonths(-1);

            jojb.Add("EXPIRED_DT", expiredDt.ToString("yyyy-MM-dd"));

            if (DBConsigned.getLicenceInfo(jojb, ref jResult))
            {
                if (ConvertUtil.ToString(jResult["MBD"]).Equals(""))
                {
                    _dtCoa.Rows[0]["MANUFACTURE_NM"] = "";
                    _dtCoa.Rows[1]["MANUFACTURE_NM"] = "";

                    _dtCoa.Rows[0]["MODEL_NM"] = "";
                    _dtCoa.Rows[1]["MODEL_NM"] = "";

                    _dtCoa.Rows[0]["SERIAL_NO"] = "";
                    _dtCoa.Rows[1]["SERIAL_NO"] = "";
                }
                else
                {
                    JObject jMBD = new JObject();
                    jMBD = (JObject)jResult["MBD"];
                    _dtCoa.Rows[0]["MANUFACTURE_NM"] = jMBD["MANUFACTURE_NM"];
                    _dtCoa.Rows[1]["MANUFACTURE_NM"] = jMBD["MANUFACTURE_NM"];

                    _dtCoa.Rows[0]["MODEL_NM"] = $"{jMBD["MBD_MODEL_NM"]}/{jMBD["PRODUCT_NAME"]}";
                    _dtCoa.Rows[1]["MODEL_NM"] = $"{jMBD["MBD_MODEL_NM"]}/{jMBD["PRODUCT_NAME"]}";


                    _dtCoa.Rows[1]["PRODUCT_NM"] = $"{jMBD["PRODUCT_NAME"]}";

                    long modelId = ConvertUtil.ToInt64(_currentReceipt["MODEL_ID"]);
                    int serialType = 0;

                    //foreach (KeyValuePair<string, int> item in ConsignedInfo._dicModelSerialType)
                    //{
                    //    if (modelNm.Contains(item.Key))
                    //    {
                    //        serialType = ConsignedInfo._dicModelSerialType[item.Key];
                    //        break;
                    //    }
                    //}

                    if (_dicModelSerialType.ContainsKey(modelId))
                        serialType = _dicModelSerialType[modelId];

                    if (modelId > 0)
                    {
                        string serialNo = ConvertUtil.ToString(jMBD["SERIAL_NO"]);
                        string systemSn = ConvertUtil.ToString(jMBD["SYSTEM_SN"]);

                        if (serialType == 0)
                        {
                            _dtCoa.Rows[0]["SERIAL_NO"] = $"{serialNo}/{systemSn}";
                            _dtCoa.Rows[1]["SERIAL_NO"] = $"{serialNo}/{systemSn}";
                        }
                        else if (serialType == 1)
                        {
                            _dtCoa.Rows[0]["SERIAL_NO"] = "1s" + serialNo.Replace('/', ' ').Trim();
                            _dtCoa.Rows[1]["SERIAL_NO"] = "1s" + serialNo.Replace('/', ' ').Trim();
                        }
                        else if(serialType == 2)
                        {
                            _dtCoa.Rows[0]["SERIAL_NO"] = systemSn.Replace('/', ' ').Trim();
                            _dtCoa.Rows[1]["SERIAL_NO"] = systemSn.Replace('/', ' ').Trim();
                        }
                        else if(serialType == 3)
                        {
                            string productNm = ConvertUtil.ToString(jMBD["PRODUCT_NAME"]);
                            _dtCoa.Rows[0]["SERIAL_NO"] = $"{productNm.Replace('/', ' ').Trim()}{systemSn.Replace('/', ' ').Trim()}";
                            _dtCoa.Rows[1]["SERIAL_NO"] = $"{productNm.Replace('/', ' ').Trim()}{systemSn.Replace('/', ' ').Trim()}";
                        }
                    } 
                }

                if (ConvertUtil.ToString(jResult["CPU"]).Equals(""))
                {                  
                    _dtCoa.Rows[0]["CPU"] = "";
                    _dtCoa.Rows[1]["CPU"] = "";
                }
                else
                {
                    JObject jCPU = new JObject();
                    jCPU = (JObject)jResult["CPU"];
                    _dtCoa.Rows[0]["CPU"] = jCPU["MODEL_NM"];
                    _dtCoa.Rows[1]["CPU"] = jCPU["MODEL_NM"];
                }

                _dtCoa.Rows[0]["OS"] = _currentReceipt["OLD_COA"];
                _dtCoa.Rows[0]["COA"] = _currentReceipt["OLD_COA_SN"];

                _dtCoa.Rows[1]["OS"] = _currentReceipt["NEW_COA"];
                _dtCoa.Rows[1]["COA"] = _currentReceipt["NEW_COA_SN"];


            }
            else
            {
                lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                _dtCoa.Rows[0]["OS"] = "1";
                _dtCoa.Rows[0]["COA"] = "";

                _dtCoa.Rows[1]["OS"] = "3";
                _dtCoa.Rows[1]["COA"] = "";

                _dtCoa.Rows[0]["MANUFACTURE_NM"] = "";
                _dtCoa.Rows[1]["MANUFACTURE_NM"] = "";

                _dtCoa.Rows[0]["MODEL_NM"] = "";
                _dtCoa.Rows[1]["MODEL_NM"] = "";

                _dtCoa.Rows[0]["SERIAL_NO"] = "";
                _dtCoa.Rows[1]["SERIAL_NO"] = "";

                _dtCoa.Rows[0]["CPU"] = "";
                _dtCoa.Rows[1]["CPU"] = "";

                Dangol.Message(jResult["MSG"]);
                return;
            }
        }

        private void getProcessAdjustmentInfo()
        {
            JObject jResult = new JObject();

            //_dtAdjustmentHistory.Clear();

            if (DBConsigned.getAdjustmentInfo(_pProxyId, _currentReceipt["COMPANY_ID"], 1, _currentReceipt["PRODUCT_TYPE"], ref jResult))
            {
                _dtAdjustMentProcess.Rows[0]["PRICE"] = jResult["PRICE"];
                _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = jResult["PRICE_DELIVERY"];
                _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = jResult["PRICE_PRODUCE"];
                _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = jResult["PRICE_QUICK"];
                _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = jResult["PRICE_REPRODUCE"];

                long totalPrice = ConvertUtil.ToInt64(jResult["PRICE"]) + ConvertUtil.ToInt64(jResult["PRICE_DELIVERY"])
                    + ConvertUtil.ToInt64(jResult["PRICE_QUICK"]) + ConvertUtil.ToInt64(jResult["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(jResult["PRICE_PRODUCE"]);

                _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice;
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
                
                

                //Dangol.Message(jResult["MSG"]);
                return;
            }
        }

        private void getReturnAdjustmentInfo()
        {
            JObject jResult = new JObject();

            if (DBConsigned.getAdjustmentInfo(_currentReceipt["PROXY_ID"], _currentReceipt["COMPANY_ID"], 3, _currentReceipt["PRODUCT_TYPE"], ref jResult))
            {
                int completeYn = ConvertUtil.ToInt32(jResult["COMPLETE_YN"]);
                if (completeYn == 1 && _proxyState == 9)
                {
                    _dtAdjustMentReturn.Rows[0]["PRICE"] = ConvertUtil.ToInt64(_currentReceipt["PART_PRICE"]) * -1;
                    //if (_refundType == 2)
                    //    _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
                    //else
                        _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = ConvertUtil.ToInt64(jResult["PRICE_DELIVERY"]) * -1;
                    _dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"] = ConvertUtil.ToInt64(jResult["PRICE_PRODUCE"]) * -1;
                    _dtAdjustMentReturn.Rows[0]["PRICE_QUICK"] = ConvertUtil.ToInt64(jResult["PRICE_QUICK"]) * -1;
                    _dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"] = ConvertUtil.ToInt64(jResult["PRICE_REPRODUCE"]) * -1;
                    _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY_O"] = ConvertUtil.ToInt64(jResult["PRICE_DELIVERY_O"]);
                }
                else
                {
                    //
                    //{
                    _dtAdjustMentReturn.Rows[0]["PRICE"] = 0;
                    _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
                    _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
                    _dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"] = 0;
                    _dtAdjustMentReturn.Rows[0]["PRICE_QUICK"] = 0;
                    _dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"] = 0;
                    _dtAdjustMentReturn.Rows[0]["TOTAL_PRICE"] = 0;
                    _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY_O"] = ConvertUtil.ToInt64(jResult["PRICE_DELIVERY_O"]);

                    if (_refundType != 1)
                    {
                        _dtAdjustMentReturn.Rows[0]["PRICE"] = ConvertUtil.ToInt64(_currentReceipt["PART_PRICE"]) * -1;

                        if (_refundType == 2)
                            _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = ConvertUtil.ToInt64(jResult["PRICE_DELIVERY_O"]);
                        else
                            _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
                    }
                }

                long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"])
                    + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_QUICK"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"]);


                _dtAdjustMentReturn.Rows[0]["TOTAL_PRICE"] = totalPrice;
                
            }
            else
            {
                _dtAdjustMentReturn.Rows[0]["PRICE"] = 0;
                _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
                _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
                _dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"] = 0;
                _dtAdjustMentReturn.Rows[0]["PRICE_QUICK"] = 0;
                _dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"] = 0;
                _dtAdjustMentReturn.Rows[0]["TOTAL_PRICE"] = 0;
                _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY_O"] = 0;
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

        private void sbSearchAddress_Click(object sender, EventArgs e)
        {
            using (dlgGetAddress getAddress = new dlgGetAddress())
            {
                if (getAddress.ShowDialog(this) == DialogResult.OK)
                {
                    DataRow dr = (DataRow)getAddress.Tag;

                    tePostalCd.Text = $"{dr["zipcode"]}";
                    teAddress.Text = $"{dr["ADDR1"]}";
                    teAddressDetail.Text = "";

                    // usrReceiptPart1.updateCnt(updatePartCnt.Cnt);

                }
            }
        }


        private void rgReturn_EditValueChanging(object sender, ChangingEventArgs e)
        {
            int state = ConvertUtil.ToInt32(e.NewValue);

            if (state >= 90)
                return;


            if (state == 6 || state == 7)
            {
                if(_refundType == 0)
                {
                    Dangol.Message($"교환타입을 선택하세요.");
                    e.Cancel = true;
                    return;
                }
            }


            if (state == 7)
            {
                if(ConvertUtil.ToInt64(_currentReceipt["R_PROXY_ID"]) > 0)
                {
                    Dangol.Message($"이미 접수된 {_dicProxyState[state.ToString()]}가 있습니다.");
                    e.Cancel = true;
                    return;
                }

                if (Dangol.MessageYN($"현재 접수를 '{_dicProxyState[state.ToString()]}'처리하시겠습니까?") == DialogResult.Yes)
                {
                    int oldState = ConvertUtil.ToInt32(e.OldValue);

                    List<string> listComponent = _refundType == 1 ? ConsignedInfo._listExceipComponetCd : new List<string>();

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    jobj.Add("PROXY_ID", _proxyId);
                    jobj.Add("P_PROXY_ID", _pProxyId);
                    jobj.Add("CHECKED", 1);
                    jobj.Add("PROXY_STATE", state);
                    jobj.Add("KEY", _receiptStateCd[state]);
                    jobj.Add("KEY_NUM", state);
                    jobj.Add("OLD_STATE", oldState);
                    //jobj.Add("OLD_STATE", oldState);

                    DataRow[] rows = _dtReceiptPartGrid.Select("COMPONENT_CD = 'MBD'");
                    if (rows.Length > 0)
                    {
                        JObject jobjLicence = new JObject();
                        jobjLicence.Add("PROXY_ID", _pProxyId);
                        jobjLicence.Add("USE_YN", 1);

                        if (!DBConsigned.updateLicenceUseInfo(jobjLicence, ref jResult))
                        {
                            Dangol.Message(jResult["MSG"]);
                            return;
                        }
                    }

                    var today = DateTime.Today;
                    int hour = ConvertUtil.ToInt32(DateTime.Now.ToString("HH"));

                    if (hour > 16)
                    {
                        var pastDate = today.AddDays(1);
                        jobj.Add("TOMORROW", 1);
                        jobj.Add("RECEIPT_DT", pastDate.ToString("yyyy-MM-dd"));
                    }

                    jobj.Add("LIST_COMPONENT_CD", string.Join(",", listComponent));

                    if (DBConsigned.preRelease(jobj, ref jResult))
                    {
                        _currentReceipt.BeginEdit();
                        if (oldState == 5)
                        {
                            _currentReceipt["PROXY_STATE"] = state.ToString();
                            _proxyState = state;
                        }

                        _currentReceipt["PRODUCT_ID"] = ProjectInfo._userId;
                        _currentReceipt["R_PROXY_ID"] = jResult["PROXY_ID"];
                        _currentReceipt.EndEdit();

                        if (oldState == 5)
                        {
                            setAdjustmentReadonly();
                            setStatistics();
                            setPartReadonly();
                        }
                        else
                            e.Cancel = true;

                        Dangol.Message("처리되었습니다.");
                    }
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                if (Dangol.MessageYN($"현재 접수를 '{_dicProxyState[state.ToString()]}'처리하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    DateTime dtDate = DateTime.Now;

                    jobj.Add("PROXY_ID", _proxyId);
                    jobj.Add("P_PROXY_ID", _pProxyId);
                    jobj.Add("CHECKED", 1);
                    jobj.Add("PROXY_STATE", state);
                    jobj.Add("KEY", _receiptStateCd[state]);
                    jobj.Add("KEY_NUM", state);
                    jobj.Add("RETURN_YN", 1);

                    if(_refundType == 2)
                        jobj.Add("MBD_REUSE_YN", 1);
                    else
                        jobj.Add("MBD_REUSE_YN", 0);

                    if (state == 9)
                    {
                        saveAdjustmentInfo();
                        jobj.Add("RELEASE_CHARGE", ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["TOTAL_PRICE"]));

                        var today = DateTime.Now;
                        dtDate = DateTime.Parse($"{deComplete.Text} {today.ToString("HH:mm:ss")}");

                        jobj.Add("KEY_VALUE", dtDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        
                        DataRow[] rows = _dtReceiptPartGrid.Select("COMPONENT_CD = 'MBD'");
                        if (rows.Length > 0)
                        {
                            int returnType = ConvertUtil.ToInt32(rows[0]["RETURN_TYPE"]);
                            if (returnType != 0)
                            {
                                JObject jobjLicence = new JObject();
                                jobjLicence.Add("PROXY_ID", _pProxyId);
                                jobjLicence.Add("USE_YN", 0);

                                if (!DBConsigned.updateLicenceUseInfo(jobjLicence, ref jResult))
                                {
                                    Dangol.Message(jResult["MSG"]);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        jobj.Add("RELEASE_CHARGE", 0);

                        DataRow[] rows = _dtReceiptPartGrid.Select("COMPONENT_CD = 'MBD'");
                        if (rows.Length > 0)
                        {
                            JObject jobjLicence = new JObject();
                            jobjLicence.Add("PROXY_ID", _pProxyId);
                            jobjLicence.Add("USE_YN", 1);

                            if (!DBConsigned.updateLicenceUseInfo(jobjLicence, ref jResult))
                            {
                                Dangol.Message(jResult["MSG"]);
                                return;
                            }
                        }
                    }

                    if(state == 6 && _refundType == 1)
                        jobj.Add("LIST_COMPONENT_CD", string.Join(",", ConsignedInfo._listExceipComponetCd));

                    if (DBConsigned.updateReceiptStatus(jobj, ref jResult))
                    {
                        _currentReceipt.BeginEdit();
                        _currentReceipt["PROXY_STATE"] = state.ToString();
                        //_currentReceipt["RELEASE_STATE"] = receiptStatus.ToString(); 
                        if (state == 6)
                        {
                            _currentReceipt["WORKER_ID"] = ProjectInfo._userId;
                            //_currentReceipt["REFUND_TYPE"] = ConvertUtil.ToInt16(_refundType);
                            //_refundType = 2;
                        }
                        else if (state == 9)
                        {
                            _currentReceipt["COMPLETE_ID"] = ProjectInfo._userId;
                        }

                        if (state == 9)
                            _currentReceipt["COMPLETE_DT"] = dtDate.ToString("yyyy.MM.dd");
                        else
                            _currentReceipt["COMPLETE_DT"] = "";

                        _currentReceipt.EndEdit();

                        _proxyState = state;

                        setAdjustmentReadonly();
                        setStatistics();
                        setPartReadonly();

                        if (state < 9)
                        {
                            Dangol.ShowSplash();
                            getReceiptPart();
                            Dangol.CloseSplash();
                        }


                        Dangol.Message("처리되었습니다.");
                    }
                    //else
                    //{
                    //    Dangol.Message(jResult["MSG"]);
                    //    return;
                    //}

                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }


        }

        private void receiptRefresh()
        {
            string receipt = _receipt;
            setStatistics();
            gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
            getReceiptList(false);
            gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
            int rowHandle = gvReceipt.LocateByValue("RECEIPT", receipt);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvReceipt.FocusedRowHandle = -2147483646;
                    gvReceipt.FocusedRowHandle = rowHandle;
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

        private void sbSearch_Click(object sender, EventArgs e)
        {
            receiptRefresh();
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
                gvReceipt.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceipt.NewRow();

                    dr["NO"] = _dtReceipt.Rows.Count + 1;
                    dr["ID"] = index++;
                    dr["PROXY_ID"] = obj["PROXY_ID"];
                    dr["P_PROXY_ID"] = obj["P_PROXY_ID"];
                    dr["R_PROXY_ID"] = obj["R_PROXY_ID"];
                    dr["C_PROXY_ID"] = obj["C_PROXY_ID"];
                    dr["PROXY_STATE"] = obj["PROXY_STATE"];

                    dr["RECEIPT"] = obj["RECEIPT"];
                    dr["RECEIPT_DT"] = obj["RECEIPT_DTS"];
                    dr["MODEL_ID"] = ConvertUtil.ToInt64(obj["MODEL_ID"]);
                    dr["MODEL_NM"] = "";
                    dr["PRODUCT_TYPE"] = "";
                    dr["GUARANTEE"] = "";
                    dr["GUARANTEE_FROM"] = "";
                    dr["GUARANTEE_TO"] = "";
                    dr["COMPANY_ID"] = obj["COMPANY_ID"];
                    dr["SALE_ROOT"] = obj["SALE_ROOT"];
                    dr["RELEASE_TYPE"] = "1";
                    dr["MODEL_NM_DETAIL"] = "";
                    dr["DES"] = "";
                    dr["CHECK"] = false;
                    dr["ERROR"] = "";

                    dr["CUSTOMER_NM1"] = obj["CUSTOMER_NM"];
                    dr["CUSTOMER_NM2"] = obj["CUSTOMER_NM"];
                    dr["TEL1"] = obj["TEL"];
                    dr["TEL2"] = obj["TEL"];
                    dr["HP1"] = obj["MOBILE"];
                    dr["HP2"] = obj["MOBILE"];
                    dr["POSTAL_CD"] = "";
                    dr["ADDRESS"] = "";
                    dr["ADDRESS_DETAIL"] = "";
                    dr["REFUND_TYPE"] = obj["REFUND_TYPE"];

                    dr["WORKER_ID"] = obj["WORKER_ID"];
                    dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                    dr["PACKAGE_ID"] = obj["PACKAGE_ID"];
                    dr["COMPLETE_ID"] = obj["COMPLETE_ID"];

                    dr["COMPLETE_DT"] = ConvertUtil.ToString(obj["COMPLETE_DTS"]);

                    dr["B_GRADE"] = obj["B_GRADE"];
                    dr["REUSE_YN"] = obj["REUSE_YN"];


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

                jData.Add("PROXY_RANGE", $"'{ string.Join("','", _listProxyRange)}'");
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

        private void sbRelease_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN($"현재 접수를 '출고'처리하시겠습니까?") == DialogResult.Yes)
            {
               
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));

                if (DBConsigned.ConsignedOneclickRelease(jobj, ref jResult))
                {
                    _currentReceipt["PROXY_STATE"] = "4";
                    //_currentReceipt["RELEASE_STATE"] = receiptStatus.ToString(); 

                    int rowHandle = gvReceipt.FocusedRowHandle;
                    gvReceipt.FocusedRowHandle = -2147483646;
                    gvReceipt.FocusedRowHandle = rowHandle;

                    setStatistics();
                    setPartReadonly();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }

            }
        }

        private bool saveCouponInfo()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
            jobj.Add("COUPON_MANAGE", ConvertUtil.ToString(teManageNo.Text.Trim()));
            jobj.Add("COUPON_CUSTOMER", ConvertUtil.ToString(teCustomerNo.Text.Trim()));

            if (DBConsigned.consignedUpdateDetail(jobj, ref jResult))
                return true;
            else
            {
                Dangol.Message(jResult["MSG"]);
                return false;
            }               
        }

        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("쿠폰정보를 저장하시겠습니까?") != DialogResult.Yes)
            {
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                string customerNo = teCustomerNo.Text.Trim();

                if (customerNo.Length != 0 && customerNo.Length != 12)
                {
                    Dangol.Message("고객번호를 확인하세요(자리수 다름).");
                    return;
                }

                if (saveCouponInfo())
                    Dangol.Message("처리되었습니다.");
                else
                    return;
            }

            //if (e.Button.Properties.Tag.Equals(1))
            //{
            //    JObject jResult = new JObject();
            //    JObject jobj = new JObject();

            //    jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
            //    jobj.Add("OLD_COA", ConvertUtil.ToInt32(_dtCoa.Rows[0]["LICENCE"]));
            //    jobj.Add("NEW_COA", ConvertUtil.ToInt32(_dtCoa.Rows[1]["LICENCE"]));
            //    jobj.Add("OLD_COA_SN", ConvertUtil.ToInt32(_dtCoa.Rows[0]["COA"]));
            //    jobj.Add("NEW_COA_SN", ConvertUtil.ToInt32(_dtCoa.Rows[1]["COA"]));
            //    jobj.Add("SERIAL_NO", ConvertUtil.ToInt32(_dtCoa.Rows[1]["SERIAL_NO"]));

            //    if (DBConsigned.consignedUpdateDetail(jobj, ref jResult))
            //    {
            //        Dangol.Message("처리되었습니다.");
            //    }
            //    else
            //    {
            //        Dangol.Message(jResult["MSG"]);
            //        return;
            //    }

            //}
        }

        private bool saveLicenceInfo()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();


            jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
            jobj.Add("OLD_COA", ConvertUtil.ToInt32(_dtCoa.Rows[0]["OS"]));
            jobj.Add("NEW_COA", ConvertUtil.ToInt32(_dtCoa.Rows[1]["OS"]));
            jobj.Add("OLD_COA_SN", ConvertUtil.ToString(_dtCoa.Rows[0]["COA"]));
            jobj.Add("NEW_COA_SN", ConvertUtil.ToString(_dtCoa.Rows[1]["COA"]));
            
            string serialNo = ConvertUtil.ToString(_dtCoa.Rows[1]["SERIAL_NO"]);

            long modelId = ConvertUtil.ToInt64(_currentReceipt["MODEL_ID"]);
            int serialType = 0;

            //foreach (KeyValuePair<string, int> item in ConsignedInfo._dicModelSerialType)
            //{
            //    if (modelNm.Contains(item.Key))
            //    {
            //        serialType = ConsignedInfo._dicModelSerialType[item.Key];
            //        break;
            //    }
            //}

            if (_dicModelSerialType.ContainsKey(modelId))
                serialType = _dicModelSerialType[modelId];

            if (serialType == 0)
            {
                jobj.Add("SERIAL_NO", "");
                jobj.Add("SERIAL_NO_COL", "SYSTEM_SN");
            }
            else if (serialType == 1)
            {
                if (serialNo.Length > 2)
                {
                    string serialCheck = serialNo.Substring(0, 2).ToUpper();
                    if (serialCheck.Equals("1S"))
                        serialNo = serialNo.Substring(2, serialNo.Length);
                }

                jobj.Add("SERIAL_NO_COL", "SERIAL_NO");
            }
            else if (serialType == 2)
            {
                jobj.Add("SERIAL_NO_COL", "SYSTEM_SN");
            }
            else if (serialType == 3)
            {
                string productNm = $"{_dtCoa.Rows[1]["PRODUCT_NM"]}";

                if(!string.IsNullOrWhiteSpace(productNm))
                {
                    string partialSerialNo = serialNo.Substring(0, productNm.Length);
                    if (productNm.Equals(partialSerialNo))
                    {
                        serialNo = serialNo.Substring(productNm.Length, serialNo.Length - productNm.Length);
                    }
                    else
                    {
                        jobj.Add("PRODUCT_NAME", "");
                    }
                }

                jobj.Add("SERIAL_NO_COL", "SYSTEM_SN");
            }

            jobj.Add("SERIAL_NO", serialNo);
            

            if (DBConsigned.consignedUpdateDetail(jobj, ref jResult))
                return true;
            else
            {
                Dangol.Message(jResult["MSG"]);
                return false;
            }
        }

        private void lgcLicence_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("라이센스 정보를 저장하시겠습니까?") != DialogResult.Yes)
            {
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                gvLicence.MoveFirst();
                gvLicence.MoveLast();
                if (saveLicenceInfo())
                    Dangol.Message("처리되었습니다.");
                else
                    return;
            }
        }

        private void risePrice_EditValueChanging(object sender, ChangingEventArgs e)
        {

        }

        private void risePrice_EditValueChanged(object sender, EventArgs e)
        {
            

        }

        private void gvAdjustment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (!e.Column.FieldName.Equals("TOTAL_PRICE"))
            //{
            //    long totalPrice = ConvertUtil.ToInt64(_currentReceipt["PART_PRICE"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"])
            //            + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_QUICK"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"]);

            //    _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice;
            //}
        }

        private void gvAdjustmentReturn_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }

        private void gvAdjustmentReturn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!e.Column.FieldName.Equals("TOTAL_PRICE"))
            {
                long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"])
                        + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_QUICK"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"]);

                _dtAdjustMentReturn.Rows[0]["TOTAL_PRICE"] = totalPrice;
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
            
        }

        private void risePartPrice_EditValueChanged(object sender, EventArgs e)
        {

            SpinEdit View = sender as SpinEdit;
            long proxyPartId = ConvertUtil.ToInt64(_currentPart["PROXY_PART_ID"]);
            JObject jResult = new JObject();
            long partPrice = ConvertUtil.ToInt64(View.Value);

            if (_proxyState == 0 || _proxyState > 3 || !ConvertUtil.ToBoolean(_currentPart["ASSIGN_YN"]))
            {
                View.Value = _partPrice;
                return;
            }

            if (partPrice < 0)
            {
                Dangol.Message("양수만 입력 가능합니다.");
                View.Value = _partPrice;
                return;
            }

            if (DBConnect.updatePrice(proxyPartId, partPrice, ref jResult))
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
            //long proxyPartId = ConvertUtil.ToInt64(_currentPart["PROXY_PART_ID"]);
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

        private void setSave()
        {

            if (ConvertUtil.ToInt32(_currentReceipt["PROXY_STATE"]) == 9)
            {
                Dangol.Warining("'반품완료' 상태인 경우는 수정할수 없습니다.");
                return;
            }

            if (Dangol.MessageYN("반품정보를 수정하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", $"{ _currentReceipt["PROXY_ID"]}");
            //jobj.Add("PC_TYPE", $"{ _currentReceipt["PRODUCT_TYPE"]}");
            //jobj.Add("GUARANTEE_DUE", $"{ _currentReceipt["GUARANTEE"]}");
            //jobj.Add("GUARANTEE_START", $"{ _currentReceipt["GUARANTEE_FROM"]}");
            //jobj.Add("GUARANTEE_END", $"{ _currentReceipt["GUARANTEE_TO"]}");

            //jobj.Add("SALE_ROOT", $"{ _currentReceipt["SALE_ROOT"]}");
            //jobj.Add("RELEASE_TYPE", $"{ _currentReceipt["RELEASE_TYPE"]}");
            //jobj.Add("DES", $"{ _currentReceipt["DES"]}");
            //jobj.Add("REQUEST", $"{ _currentReceipt["REQUEST"]}");
            //jobj.Add("MODEL_NM_DETAIL", $"{ _currentReceipt["MODEL_NM_DETAIL"]}");
            jobj.Add("REASON", $"{ teRetuenReason.Text}");
            jobj.Add("RETURN_PROCESS", $"{ teRetuenProcess.Text}");

            jobj.Add("ONLY_RECEIPT_INFO", 1);


            if (DBConsigned.updateReturnReason(jobj, ref jResult))
            {
                _currentReceipt["REASON"] = teRetuenReason.Text;
                _currentReceipt["RETURN_PROCESS"] = teRetuenProcess.Text;
                Dangol.Message("수정되었습니다.");
                
                return;
            }
            else
            {
                Dangol.Message($"{jResult["MSG"]}");
                return;
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN($"현재 접수를 '취소' 처리하시겠습니까?") == DialogResult.Yes)
            {
                int receiptStatus = 91;

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
                jobj.Add("CHECKED", 1);
                jobj.Add("PROXY_STATE", receiptStatus);
                jobj.Add("KEY", "CANCEL_DT");
                jobj.Add("KEY_NUM", receiptStatus);

                jobj.Add("RELEASE_CHARGE", 0);

                if (DBConsigned.updateReceiptStatus(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentReceipt["PROXY_STATE"] = receiptStatus.ToString();
                    _currentReceipt.EndEdit();

                    _dtAdjustMentProcess.BeginInit();

                    long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
                    long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

                    _dtAdjustMentProcess.Rows[0]["PRICE"] = 0;
                    _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - price;

                    _dtAdjustMentProcess.EndInit();

                    _proxyState = receiptStatus;

                    rgProcess.EditValue = "91";

                    setAdjustmentReadonly();
                    setStatistics();
                    setPartReadonly();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }

            }
        }

        private void sbHold_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN($"현재 접수를 '보류' 처리하시겠습니까?") == DialogResult.Yes)
            {
                int receiptStatus = 90;

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
                jobj.Add("CHECKED", 1);
                jobj.Add("PROXY_STATE", receiptStatus);
                jobj.Add("KEY", "POSTPONE_DT");
                jobj.Add("KEY_NUM", receiptStatus);

                
                jobj.Add("RELEASE_CHARGE", 0);

                if (DBConsigned.updateReceiptStatus(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentReceipt["PROXY_STATE"] = receiptStatus.ToString();

                    _currentReceipt.EndEdit();

                    _proxyState = receiptStatus;

                    rgProcess.EditValue = "90";

                    setAdjustmentReadonly();
                    setStatistics();
                    setPartReadonly();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }

            }
        }

        private void sbReleaseBulk_Click(object sender, EventArgs e)
        {
            
        }

        private void rileConsignedType_EditValueChanging(object sender, ChangingEventArgs e)
        {
            int value = ConvertUtil.ToInt32(e.NewValue);
            string text = value == 1 ? "생산대행" : "자사재고";

            if (Dangol.MessageYN($"현재 부품을 '{text}'로 변경하시겠습니까?") == DialogResult.Yes)
            {

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("PROXY_PART_ID", ConvertUtil.ToInt64(_currentPart["PROXY_PART_ID"]));
                jobj.Add("CONSIGNED_TYPE", value);

                if (DBConsigned.updateProxyComponentUnit(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentPart["CONSIGNED_TYPE"] = value;

                    _currentReceipt.EndEdit();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    e.Cancel = true;
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void rileConsignedType_Popup(object sender, EventArgs e)
        {
            PopupLookUpEditForm form = (sender as IPopupControl).PopupWindow as PopupLookUpEditForm;
            int width = Common.GetWidth(sender as LookUpEdit);
            if (form != null && form.Width > width)
            {
                form.Width = width;
            }
        }

        private void gvPart_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (ConvertUtil.ToInt32(gvPart.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void lgcReceiptPart_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            try
            {
                gvPart.BeginUpdate();
                foreach (DataRow row in _dtReceiptPartGrid.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvPart.DataRowCount; i++)
                {
                    int rowHandle = gvPart.GetVisibleRowHandle(i);
                    rows.Add(gvPart.GetDataRow(rowHandle));
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
                gvPart.EndUpdate();
            }
        }

        private void lgcReceiptPart_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            gvPart.BeginDataUpdate();

            foreach (DataRow row in _dtReceiptPartGrid.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvPart.EndDataUpdate();
        }

        private void lgcReceiptPart_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentReceipt == null)
            {
                Dangol.Message("선택된 접수가 없습니다.");
                return;
            }

            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtReceiptPartGrid.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("선택한 부품을 '재입고없음'으로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 1);

                    jobj.Add("RETURN_TYPE", 0);
                    jobj.Add("INVENTORY_STATE", "R");
                    jobj.Add("LOCK_YN", "N");
                    jobj.Add("INVENTORY_CAT", "G");

                    List<long> listProxyReleasePartId = new List<long>();
                    List<long> listInventoryId = new List<long>();
                    bool releaseYn;
                    string componentCd;
                    foreach (DataRow row in rows)
                    {
                        componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                        if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG"))
                        {
                            releaseYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);
                            if (releaseYn)
                            {
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                                listProxyReleasePartId.Add(ConvertUtil.ToInt64(row["PROXY_RELEASE_PART_ID"]));
                            }
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                    jobj.Add("LIST_PROXY_RELEASE_PART_ID", string.Join(",", listProxyReleasePartId));

                    if (DBConsigned.updateReleaseReturn(jobj, ref jResult))
                    {
                        gvPart.BeginDataUpdate();
                       
                        foreach (DataRow row in rows)
                        {
                            componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                            if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG"))
                            {
                                releaseYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);
                                if (releaseYn)
                                {
                                    row.BeginEdit();
                                    row["RETURN_TYPE"] = "0";
                                    row.EndEdit();
                                }
                            }
                        }

                        gvPart.EndDataUpdate();

                        setReturnPartPrice();

                        Dangol.CloseSplash();
                        Dangol.Message("처리되었습니다.");
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
                if (Dangol.MessageYN("선택한 부품을 '재입고(정상)'으로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 1);

                    jobj.Add("RETURN_TYPE", 1);
                    jobj.Add("INVENTORY_STATE", "E");
                    jobj.Add("LOCK_YN", "Y");
                    jobj.Add("INVENTORY_CAT", "G");

                    List<long> listProxyReleasePartId = new List<long>();
                    List<long> listInventoryId = new List<long>();
                    bool releaseYn;
                    string componentCd;
                    foreach (DataRow row in rows)
                    {
                        componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                        if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG"))
                        {
                            releaseYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);
                            if (releaseYn)
                            {
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                                listProxyReleasePartId.Add(ConvertUtil.ToInt64(row["PROXY_RELEASE_PART_ID"]));
                            }
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                    jobj.Add("LIST_PROXY_RELEASE_PART_ID", string.Join(",", listProxyReleasePartId));

                    if (DBConsigned.updateReleaseReturn(jobj, ref jResult))
                    {
                        gvPart.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                            if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG"))
                            {
                                releaseYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);
                                if (releaseYn)
                                {
                                    row.BeginEdit();
                                    row["RETURN_TYPE"] = "1";
                                    row.EndEdit();
                                }
                            }
                        }

                        gvPart.EndDataUpdate();

                        setReturnPartPrice();

                        Dangol.CloseSplash();
                        Dangol.Message("처리되었습니다.");
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
                if (Dangol.MessageYN("선택한 부품을 '재입고(불량)'으로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 1);

                    jobj.Add("RETURN_TYPE", 0);
                    jobj.Add("INVENTORY_STATE", "R");
                    jobj.Add("LOCK_YN", "Y");
                    jobj.Add("INVENTORY_CAT", "F");

                    List<long> listProxyReleasePartId = new List<long>();
                    List<long> listInventoryId = new List<long>();
                    bool releaseYn;
                    string componentCd;
                    foreach (DataRow row in rows)
                    {
                        componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                        if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG"))
                        {
                            releaseYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);
                            if (releaseYn)
                            {
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                                listProxyReleasePartId.Add(ConvertUtil.ToInt64(row["PROXY_RELEASE_PART_ID"]));
                            }
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                    jobj.Add("LIST_PROXY_RELEASE_PART_ID", string.Join(",", listProxyReleasePartId));

                    if (DBConsigned.updateReleaseReturn(jobj, ref jResult))
                    {
                        gvPart.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                            if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG"))
                            {
                                releaseYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);
                                if (releaseYn)
                                {
                                    row.BeginEdit();
                                    row["RETURN_TYPE"] = "2";
                                    row.EndEdit();
                                }
                            }
                        }

                        gvPart.EndDataUpdate();

                        setReturnPartPrice();

                        Dangol.CloseSplash();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }

            }
        }

        private void setReturnPartPrice()
        {
            bool assignYn = false;
            int returnType = 0;
            string componentCd;
            long price = 0;

            gvPart.BeginDataUpdate();
            foreach (DataRow row in _dtReceiptPartGrid.Rows)
            {
                componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                returnType = ConvertUtil.ToInt32(row["RETURN_TYPE"]);
                assignYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);

                if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG") && assignYn)
                {
                    row.BeginEdit();


                    if (returnType == 0 || _refundType == 1)
                        row["RETURN_PRICE"] = 0;
                    else   
                        row["RETURN_PRICE"] = row["PROXY_PRICE"];

                    row.EndEdit();
                }

                price += ConvertUtil.ToInt64(row["RETURN_PRICE"]);
            }

            gvPart.EndDataUpdate();

            _currentReceipt["PART_PRICE"] = price;

            gvAdjustmentReturn.BeginDataUpdate();
            _dtAdjustMentReturn.Rows[0].BeginEdit();

            price *= -1;
            _dtAdjustMentReturn.Rows[0]["PRICE"] = price;

            if (_refundType == 1)
                _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = 0;
            else
                _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"] = _dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY_O"];

            long totalPrice = price + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_DELIVERY"])
                                            + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_QUICK"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(_dtAdjustMentReturn.Rows[0]["PRICE_PRODUCE"]);

            _dtAdjustMentReturn.Rows[0]["TOTAL_PRICE"] = totalPrice;

            _dtAdjustMentReturn.Rows[0].EndEdit();
            gvAdjustmentReturn.EndDataUpdate();
        }




        private void lcgDelivery_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentReceipt == null)
            {
                Dangol.Message("선택된 항목이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("운송장 정보를 수정하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("PROXY_ID", _proxyId);
                    jobj.Add("DELIVERY_COMPANY", ConvertUtil.ToString(leDelivery.EditValue));
                    jobj.Add("INVOICE", teDelivery.Text);

                    if (DBConsigned.updateInvoiceOne(jobj, ref jResult))
                    {
                        _currentReceipt["DELIVERY_COMPANY"] = ConvertUtil.ToString(leDelivery.EditValue);
                        _currentReceipt["INVOICE"] = teDelivery.Text;
                        Dangol.CloseSplash();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }

        private void lcgReturnAdjustment_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (Dangol.MessageYN("정산 금액을 저장하시겠습니까?") != DialogResult.Yes)
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

        private void rileReturnType_Popup(object sender, EventArgs e)
        {
            PopupLookUpEditForm form = (sender as IPopupControl).PopupWindow as PopupLookUpEditForm;
            int width = Common.GetWidth(sender as LookUpEdit);
            if (form != null && form.Width > width)
            {
                form.Width = width;
            }
        }

        private void rileReturnType_EditValueChanging(object sender, ChangingEventArgs e)
        {
            int value = ConvertUtil.ToInt32(e.NewValue);

            if (Dangol.MessageYN($"현재 부품을 '{_dicReturnType[value.ToString()]}'로 변경하시겠습니까?") == DialogResult.Yes)
            {

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("PROXY_RELEASE_PART_ID", _proxyReleasePartId);
                jobj.Add("INVENTORY_ID", _inventoryId);
                jobj.Add("RETURN_TYPE", value);

                if(value == 0){
                    jobj.Add("INVENTORY_STATE", "R");
                    jobj.Add("LOCK_YN", "N");
                    jobj.Add("INVENTORY_CAT", "G");
                }else if (value == 1)
                {
                    jobj.Add("INVENTORY_STATE", "E");
                    jobj.Add("LOCK_YN", "Y");
                    jobj.Add("INVENTORY_CAT", "G");
                }
                else if (value == 2)
                {
                    jobj.Add("INVENTORY_STATE", "R");
                    jobj.Add("LOCK_YN", "Y");
                    jobj.Add("INVENTORY_CAT", "F");
                }

                if (DBConsigned.updateReleaseReturn(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentPart["RETURN_TYPE"] = value.ToString();
                    _currentReceipt.EndEdit();

                    setReturnPartPrice();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    e.Cancel = true;
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void gvPart_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "ASSIGN_YN")
            {
                int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["RETURN_TYPE"]));

                if (state == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.SkyBlue);
                }
                else if (state == 2)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
                }
                else
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Transparent);
            }
        }

        private void rgReturnType_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if(_currentReceipt == null)
            {
                Dangol.Message("접수가 선택되지 않았습니다.");
                e.Cancel = true;
                return;
            }
            int value = ConvertUtil.ToInt32(e.NewValue);
            string text = value == 1 ? "교환" : "환불";
            string addtext = value == 1 ? "으" : "";

            if (ConvertUtil.ToInt32(_currentReceipt["PROXY_STATE"]) == 5)
            {
                if (Dangol.MessageYN($"반품 타입을 '{text}'{addtext}로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    jobj.Add("PROXY_ID", _proxyId);
                    jobj.Add("REFUND_TYPE", value);

                    DBConsigned.updateReceiptSimple(jobj, ref jResult);

                    _refundType = value;

                    getReturnAdjustmentInfo();

                    gvReceipt.BeginDataUpdate();
                    _currentReceipt.BeginEdit();
                    _currentReceipt["REFUND_TYPE"] = value;
                    _currentReceipt.EndEdit();
                    gvReceipt.EndDataUpdate();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {

                if (Dangol.MessageYN($"반품 타입을 '{text}'{addtext}로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string componentCd;
                    long proxyReleasePartId;
                    long inventoryId;
                    string inventoryCat;
                    bool assignYn;

                    gvPart.BeginDataUpdate();
                    List<string> listComponent = value == 1 ? ConsignedInfo._listExchangeComponetCd : ConsignedInfo._listRefundComponetCd;
                    foreach (DataRow row in _dtReceiptPartGrid.Rows)
                    {
                        componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                        jobj.RemoveAll();
                        if (!componentCd.Equals("AIR") && !componentCd.Equals("PKG"))
                        {
                            assignYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);
                            if (assignYn)
                            {
                                proxyReleasePartId = ConvertUtil.ToInt64(row["PROXY_RELEASE_PART_ID"]);
                                inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                                inventoryCat = ConvertUtil.ToString(row["INVENTORY_CAT"]);

                                jobj.Add("PROXY_RELEASE_PART_ID", proxyReleasePartId);
                                jobj.Add("INVENTORY_ID", inventoryId);
                                string type;
                                if (listComponent.Contains(componentCd))
                                {
                                    if (inventoryCat.Equals("G"))
                                        type = "1";
                                    else
                                        type = "2";

                                    jobj.Add("RETURN_TYPE", ConvertUtil.ToInt32(type));
                                    jobj.Add("INVENTORY_CAT", inventoryCat);
                                    jobj.Add("INVENTORY_STATE", "E");
                                    jobj.Add("LOCK_YN", "Y");
                                   
                                }
                                else
                                {
                                    type = "0";
                                    jobj.Add("RETURN_TYPE", 0);
                                    jobj.Add("INVENTORY_STATE", "R");
                                    jobj.Add("LOCK_YN", "N");
                                    jobj.Add("INVENTORY_CAT", inventoryCat);
                                }

                                if (DBConsigned.updateReleaseReturn(jobj, ref jResult))
                                {
                                    row["RETURN_TYPE"] = type;
                                }
                            }

                        }
                    }
                    gvPart.EndDataUpdate();

                    jResult.RemoveAll();
                    jobj.RemoveAll();

                    jobj.Add("PROXY_ID", _proxyId);
                    jobj.Add("REFUND_TYPE", value);

                    DBConsigned.updateReceiptSimple(jobj, ref jResult);

                    _refundType = value;

                    setReturnPartPrice();
                    getReturnAdjustmentInfo();

                    gvReceipt.BeginDataUpdate();
                    _currentReceipt.BeginEdit();
                    _currentReceipt["REFUND_TYPE"] = value;
                    _currentReceipt.EndEdit();
                    gvReceipt.EndDataUpdate();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void usrConsignedReturnList_Enter(object sender, EventArgs e)
        {
            if (!initializeEnter)
                receiptRefresh();

            initializeEnter = false;
        }

        private void setDelete()
        {
            if (Dangol.MessageYN($"현재 반품접수를 삭제하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();
                jobj.Add("PROXY_ID", _proxyId);
                jobj.Add("PROXY_STATE", -1);

                DBConsigned.updateReceiptSimple(jobj, ref jResult);

                gvReceipt.BeginDataUpdate();
                _currentReceipt.Delete();
                gvReceipt.EndDataUpdate();

                Dangol.Message("처리되었습니다.");
            }
        }

        private void lcgReceipt_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                setDelete();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                setSave();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                int rowHandle = gvReceipt.FocusedRowHandle;
                gvReceipt.FocusedRowHandle = -2147483646;
                gvReceipt.FocusedRowHandle = rowHandle;

                setStatistics();
            }
        }

        private void sbReturnCheck_Click(object sender, EventArgs e)
        {
            if(_currentReceipt == null)
            {
                Dangol.Warining("접수정보가 선택되지 않았습니다.");
                return;
            }


            getReturnCheckInfo(_pProxyId, _checkType);

            string dtStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (DlgConsignedReturnCheck dlgConsignedReturnCheck = new DlgConsignedReturnCheck(_pProxyId, _dicReturnCheck, _dicErrorPart))
            {
                if (dlgConsignedReturnCheck.ShowDialog(this) == DialogResult.OK)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    _dicReturnCheck = dlgConsignedReturnCheck._dicReturnCheck;

                    jobj.Add("PROXY_ID", _pProxyId);
                    jobj.Add("CHECK_TYPE", _checkType);
                    jobj.Add("START_DT", dtStart);
                    jobj.Add("END_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    if(dlgConsignedReturnCheck._isErrorPartCheck)
                    {
                        var jArray = new JArray();
                       
                        foreach (KeyValuePair<long, long> item in _dicErrorPart)
                        {
                            JObject jData = new JObject();

                            jData.Add("PROXY_PART_ID", item.Key);
                            jData.Add("COMPONENT_ID", item.Value);
                            jArray.Add(jData);
                        }

                        jobj.Add("LIST_ERROR_DATA", jArray);
                    }

                    foreach (KeyValuePair<string, Int32> pair in _dicReturnCheck)
                    {
                        jobj.Add(pair.Key, pair.Value);
                    }

                    if (DBConsigned.insertCheckInfo(jobj, ref jResult))
                    {
                        Dangol.Message("처리되었습니다.");
                    }
                }
            }
        }

        private void getReturnCheckInfo(long proxyId, int checkType)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", proxyId);
            jobj.Add("CHECK_TYPE", checkType);

            _dicReturnCheck.Clear();
            _dicErrorPart.Clear();

            if (DBConsigned.getCheckInfo(jobj, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ConsignedInfo._listCheckExceiptCol.Contains(name))
                        {
                            _dicReturnCheck.Add(name, ConvertUtil.ToInt32(x.Value));
                        }
                    }
                }

                if (Convert.ToBoolean(jResult["PART_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        _dicErrorPart.Add(ConvertUtil.ToInt64(obj["PROXY_PART_ID"]), ConvertUtil.ToInt64(obj["COMPONENT_ID"]));
                    }
                }
            }
        }
    }
}