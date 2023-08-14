using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.usedPurchase;

namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedList : DevExpress.XtraEditors.XtraForm
    {
        string _componentCd;

        DataRowView _currentReceipt;
        DataRowView _currentPart;

        TreeListNode _currentReceiptPart;
        DataRowView _currentComponentPart;

        DataTable _dtReceipt;
        DataTable _dtReceiptPart;
        DataTable _dtReceiptPartGrid;
        DataTable _dtComponentList;
        DataTable _dtCoa;
        DataTable _dtAdjustMentProcess;

        DataTable _dtAdjustmentHistory;

        DataTable _dtAdjustMentReturn;

        BindingSource _bsReceipt;
        BindingSource _bsReceiptPart;
        BindingSource _bsComponentList;
        BindingSource _bsCoa;
        BindingSource _bsReceiptPartGrid;
        BindingSource _bsAdjustMentProcess;
        BindingSource _bsAdjustmentHistory;

        Dictionary<long, List<long>> _dicReceiptPart;
        Dictionary<long, List<long>> _dicConsignedModel;

        GridColumn[] _arrTimeColumn;

        string[] _consignedComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "SSD", "HDD", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG" };
        string[] _consignedComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "SSD", "HDD", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스" };

        string[] _receiptStateCd = new string[] { "RECEIPT_DT", "PROCESS_DT", "PRODUCT_DT", "PACKAGE_DT", "RELEASE_DT", "RETURN_REQUEST_DT", "RETURN_IN_DT", "EXCHANGE_DT", "RETURN_CANCEL_DT", "RETURN_COMPLETE_DT" };
        Dictionary<int, string> _dicConsignedComponentCd;
        Dictionary<string, int> _dicConsignedComponentCdReverse;

        Dictionary<string, string> _dicProxyState;

        string _currentGetComponentCd;

        Dictionary<string, string> _dicProductType;
        Dictionary<string, string> _dicGuarantee;

        Dictionary<long, int> _dicModelSerialType;

        List<string> _listProxyRange;
        List<string> _AllowUserType;

        int _proxyState;
        long _proxyId;
        long _id;
        long _partPrice = 0;
        string _receipt = "";

        int _releaseType;
        int _partRelease;
        int _productType;

        bool _reUse;

        int _preRelease;

        bool initialize = true;
        bool initializeEnter = true;
        public usrConsignedList()
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
            _dtReceipt.Columns.Add(new DataColumn("PART_PRICE", typeof(long)));

            _dtReceipt.Columns.Add(new DataColumn("WORKER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PACKAGE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DTF", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("WORKER_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PACKAGE_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("B_GRADE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PART_YN", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("PRE_YN", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("REUSE_YN", typeof(int)));

            _dtReceipt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtReceipt.Columns.Add(new DataColumn("ERROR", typeof(string)));

            _dtReceiptPart = new DataTable();
            _dtReceiptPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPart.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("P_PART_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("PART_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("MODEL_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_CD_T", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtReceiptPart.Columns.Add(new DataColumn("ASSIGN_YN", typeof(bool)));

            _dtReceiptPartGrid = new DataTable();
            _dtReceiptPartGrid.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PROXY_PART_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("DETAIL_DATA", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PROXY_PRICE", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("ASSIGN_YN", typeof(bool)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("CHECK", typeof(bool)));

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


            _bsReceipt = new BindingSource();
            _bsReceiptPart = new BindingSource();
            _bsComponentList = new BindingSource();
            _bsCoa = new BindingSource();
            _bsReceiptPartGrid = new BindingSource();
            _bsAdjustMentProcess = new BindingSource();
            _bsAdjustmentHistory = new BindingSource();

            _dicReceiptPart = new Dictionary<long, List<long>>();
            _dicConsignedModel = new Dictionary<long, List<long>>();

            _dicProductType = new Dictionary<string, string>();
            _dicGuarantee = new Dictionary<string, string>();
            _dicProxyState = new Dictionary<string, string>();
            _id = 0;

            _dicConsignedComponentCd = new Dictionary<int, string>();
            _dicConsignedComponentCdReverse = new Dictionary<string, int>();

            _dicModelSerialType = new Dictionary<long, int>();

            _AllowUserType = new List<string>(new[] { "M", "S"});

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                _dicConsignedComponentCd.Add(i, _consignedComponetCd[i]);
                _dicConsignedComponentCdReverse.Add(_consignedComponetCd[i], i);
            }

            _currentGetComponentCd = null;
            _proxyState = -1;
            _listProxyRange = new List<string>();

            initialize = true;

            initializeEnter = true;

            _arrTimeColumn = new GridColumn[] { gcModelNm, gcReceiptDt, gcProcessDt, gcProduceDt, gcPackageDt, gcCompleteDt };
        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
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

            teCustomerNm1.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM1", false, DataSourceUpdateMode.OnPropertyChanged));
            teCustomerNm2.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM2", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel1.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL1", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel2.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL2", false, DataSourceUpdateMode.OnPropertyChanged));
            teHp1.DataBindings.Add(new Binding("Text", _bsReceipt, "HP1", false, DataSourceUpdateMode.OnPropertyChanged));
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


            var today = DateTime.Today;
            var pastDate = today.AddDays(0);
            deRelease.EditValue = today;

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            leReceiptState.ItemIndex = 0;

            gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
            getReceiptList(true);
            gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
            lcgReceiptList.CustomHeaderButtons[2].Properties.Checked = false;
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

        private void setInfoBox()
        {
            DataTable dtProxyState = new DataTable();
            dtProxyState = Util.getCodeList("CD0902", "KEY", "VALUE");
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

            _listProxyRange.Add("90");
            _listProxyRange.Add("91");

            rgProcessState[indexProcess++] = rgItemHold;
            rgProcessState[indexProcess++] = rgItemCancel;


            this.rgProcess.Properties.Items.AddRange(rgProcessState);
            this.rgReturn.Properties.Items.AddRange(rgReturnState);
            this.rgAS.Properties.Items.AddRange(rgASState);

            DataTable dtProxyProcessState = new DataTable();
            dtProxyProcessState.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtProxyProcessState.Columns.Add(new DataColumn("VALUE", typeof(string)));

            foreach (DataRow row in dtProxyState.Rows)
            {
                string stateS = ConvertUtil.ToString(row["KEY"]);
                if (!stateS.Equals("D"))
                {
                    int state = ConvertUtil.ToInt32(row["KEY"]);

                    if (state < 5 || state > 9)
                    {
                        Util.insertRow(dtProxyProcessState, stateS, ConvertUtil.ToString(row["VALUE"]));
                        _listProxyRange.Add(stateS);
                    }
                }

            }
            Util.insertRowonTop(dtProxyProcessState, "-1", "선택안함");
            Util.LookupEditHelper(leReceiptState, dtProxyProcessState, "KEY", "VALUE");

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
            Util.insertRowonTop(dtCompany, "-1", "선택안함");
            Util.LookupEditHelper(leComapanyReceipt, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(rileUserId, dtUserId, "KEY", "VALUE");

            Util.LookupEditHelper(rileUser, dtUserId, "KEY", "VALUE");

            DataTable dtSaleRoot = new DataTable();
            dtSaleRoot = Util.getCodeList("CD0905", "KEY", "VALUE");
            Util.insertRowonTop(dtSaleRoot, "-1", "선택안함");
            Util.LookupEditHelper(leSaleRoot, dtSaleRoot, "KEY", "VALUE");

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

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;
            _bsReceiptPart.DataSource = _dtReceiptPart;
            _bsComponentList.DataSource = _dtComponentList;
            _bsCoa.DataSource = _dtCoa;
            _bsReceiptPartGrid.DataSource = _dtReceiptPartGrid;
            _bsAdjustMentProcess.DataSource = _dtAdjustMentProcess;
            _bsAdjustmentHistory.DataSource = _dtAdjustmentHistory;

            leCompany.EditValue = "-1";

            if (ProjectInfo._userType.Equals("E"))
            {
                rgProcess.ReadOnly = true;

                leCompany.EditValue = ProjectInfo._userCompanyId.ToString();

                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCompanyId.Visible = false;

                gcCheck.Visible = false;
                gcCheckPart.Visible = false;

                gvLicence.OptionsBehavior.ReadOnly = true;
                gvAdjustment.OptionsBehavior.ReadOnly = true;
                gvPart.OptionsBehavior.ReadOnly = true;

                leDelivery.ReadOnly = true;
                teDelivery.ReadOnly = true;

                teManageNo.ReadOnly = true;
                teCustomerNo.ReadOnly = true;

                for (int i = 0; i < lcgReceipt.CustomHeaderButtons.Count-1; i++)
                    lcgReceipt.CustomHeaderButtons[i].Properties.Visible = false;

                for (int i = 0; i < lcgReceiptList.CustomHeaderButtons.Count; i++)
                    lcgReceiptList.CustomHeaderButtons[i].Properties.Visible = false;

                for (int i = 0; i < lgcReceiptPart.CustomHeaderButtons.Count; i++)
                    lgcReceiptPart.CustomHeaderButtons[i].Properties.Visible = false;

                

                lcgDelivery.CustomHeaderButtons[0].Properties.Visible = false;
                lcgCoupon.CustomHeaderButtons[0].Properties.Visible = false;
                lgcLicence.CustomHeaderButtons[0].Properties.Visible = false;
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

            gcAdjustmentHistory.DataSource = null;
            gcAdjustmentHistory.DataSource = _bsAdjustmentHistory;
        }


        private void setStatistics()
        {
            JObject jResult = new JObject();

            if (DBConsigned.getConsignedReceiptStatistics(ref jResult))
            {
                teReceiptCnt.Text = $"{jResult["RECEIPT_CNT"]}";
                teProcessCnt.Text = $"{jResult["PROCESS_CNT"]}";
                teHoldCnt.Text = $"{jResult["HOLD_CNT"]}";
                teProductCnt.Text = $"{jResult["PRODUCT_CNT"]}";
                tePackageCnt.Text = $"{jResult["PACKAGE_CNT"]}";
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
                if (_proxyState < 2)
                {
                    lcgCoupon.CustomHeaderButtons[0].Properties.Enabled = true;
                    lcgDelivery.CustomHeaderButtons[0].Properties.Enabled = true;
                    lgcLicence.CustomHeaderButtons[0].Properties.Enabled = true;

                    leDelivery.ReadOnly = false;
                    teDelivery.ReadOnly = false;
                    teManageNo.ReadOnly = false;
                    teCustomerNo.ReadOnly = false;

                    gvLicence.OptionsBehavior.ReadOnly = false;
                }
                else
                {
                    lcgCoupon.CustomHeaderButtons[0].Properties.Enabled = false;
                    lcgDelivery.CustomHeaderButtons[0].Properties.Enabled = false;
                    lgcLicence.CustomHeaderButtons[0].Properties.Enabled = false;

                    leDelivery.ReadOnly = true;
                    teDelivery.ReadOnly = true;
                    teManageNo.ReadOnly = true;
                    teCustomerNo.ReadOnly = true;

                    gvLicence.OptionsBehavior.ReadOnly = true;
                }

                if (_proxyState < 4)
                {
                    gvAdjustment.OptionsBehavior.ReadOnly = false;
                }
                else
                {
                    gvAdjustment.OptionsBehavior.ReadOnly = true;
                }

                if (_proxyState == 1 || _proxyState == 2 || _proxyState == 3)
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
        }

        private void setPartReadonly()
        {
            if (!ProjectInfo._userType.Equals("E"))
            {
                lgcReceiptPart.BeginUpdate();
                if (_proxyState == 0)
                {
                    gcConsignedType.OptionsColumn.AllowEdit = true;

                    for (int i = 1; i < lgcReceiptPart.CustomHeaderButtons.Count; i++)
                        lgcReceiptPart.CustomHeaderButtons[i].Properties.Visible = true;
                }
                else
                {
                    gcConsignedType.OptionsColumn.AllowEdit = false;

                    for (int i = 1; i < lgcReceiptPart.CustomHeaderButtons.Count; i++)
                        lgcReceiptPart.CustomHeaderButtons[i].Properties.Visible = false;
                }

                if (_AllowUserType.Contains(ProjectInfo._userType) || (ProjectInfo._userPosition.Equals("LEADER") && ProjectInfo._userTeamCd.Equals("PRD")))
                {
                    lgcReceiptPart.CustomHeaderButtons[0].Properties.Visible = true;
                    lgcReceiptPart.CustomHeaderButtons[lgcReceiptPart.CustomHeaderButtons.Count - 1].Properties.Visible = true;
                }
                else
                {
                    if (_partRelease == 1 || ConvertUtil.ToInt32(_currentReceipt["PRODUCT_TYPE"]) == 5)
                        lgcReceiptPart.CustomHeaderButtons[0].Properties.Visible = true;
                    else
                        lgcReceiptPart.CustomHeaderButtons[0].Properties.Visible = false;
                }

                if (_proxyState == 4 && (_AllowUserType.Contains(ProjectInfo._userType) || (ProjectInfo._userPosition.Equals("LEADER") && ProjectInfo._userTeamCd.Equals("PRD"))))
                {
                    lcReleaseDt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcReleaseDtBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    lcReleaseDt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcReleaseDtBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                lgcReceiptPart.EndUpdate();
            }
        }

        private void setReadonly()
        {
            lgcReceiptPart.BeginUpdate();

            lcgCoupon.CustomHeaderButtons[0].Properties.Enabled = false;
            lcgDelivery.CustomHeaderButtons[0].Properties.Enabled = false;
            lgcLicence.CustomHeaderButtons[0].Properties.Enabled = false;

            leDelivery.ReadOnly = true;
            teDelivery.ReadOnly = true;
            teManageNo.ReadOnly = true;
            teCustomerNo.ReadOnly = true;

            gvLicence.OptionsBehavior.ReadOnly = true;
  
            gvAdjustment.OptionsBehavior.ReadOnly = true;
            gvAdjustment.OptionsBehavior.Editable = false;
            lgcComponentList.CustomHeaderButtons[0].Properties.Enabled = false;
            gridColumn21.OptionsColumn.AllowEdit = false;
                
            
            gcConsignedType.OptionsColumn.AllowEdit = false;

            for (int i = 0; i < lgcReceiptPart.CustomHeaderButtons.Count; i++)
                lgcReceiptPart.CustomHeaderButtons[i].Properties.Visible = false;

            lcReleaseDt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcReleaseDtBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                

            lgcReceiptPart.EndUpdate();
        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);
            _dtComponentList.Clear();

            if (isValidRow)
            {
                if(!initialize)
                    Dangol.ShowSplash();

                _currentReceipt = e.Row as DataRowView;

                _proxyState = ConvertUtil.ToInt32(_currentReceipt["PROXY_STATE"]);

                gvReceipt.BeginDataUpdate();
                if (_proxyState == 90)
                    gvReceipt.Appearance.FocusedRow.ForeColor = Color.Purple;
                else if (_proxyState == 91)
                    gvReceipt.Appearance.FocusedRow.ForeColor = Color.Red;
                else
                    gvReceipt.Appearance.FocusedRow.ForeColor = Color.Black;
                gvReceipt.EndDataUpdate();

                _proxyId = ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]);
                _receipt = ConvertUtil.ToString(_currentReceipt["RECEIPT"]);
                _partRelease = ConvertUtil.ToInt32(_currentReceipt["PART_YN"]);
                _reUse = ConvertUtil.ToInt32(_currentReceipt["REUSE_YN"]) == 1 ? true : false;              
                _preRelease = ConvertUtil.ToInt32(_currentReceipt["PRE_YN"]);

                lgcProxy.BeginInit();
                lcgLicence.BeginInit();
                _currentReceipt.BeginEdit();


                getConsignedInfo();
                _releaseType = ConvertUtil.ToInt32(_currentReceipt["RELEASE_TYPE"]);
                getLicenceInfo();
                getAdjustmentInfo();

                if (!ProjectInfo._userType.Equals("E"))
                {
                    setAdjustmentReadonly();

                    lcgReceipt.BeginUpdate();
                    if (ConvertUtil.ToInt32(_currentReceipt["RETURN_YN"]) == 1)
                    {
                        rgProcess.ReadOnly = true;
                        rgReturn.ReadOnly = true;
                        rgAS.ReadOnly = true;

                        if (ConvertUtil.ToInt32(_currentReceipt["AS_YN"]) == 1)
                        {
                            lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            lcAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                        else
                        {
                            lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            lcAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        }
                    }
                    else
                    {
                        rgProcess.ReadOnly = false;
                        rgReturn.ReadOnly = true;
                        rgAS.ReadOnly = true;
                        lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;


                        if (_proxyState == 4)
                        {
                            lcgReceipt.CustomHeaderButtons[1].Properties.Visible = true;
                            lcgReceipt.CustomHeaderButtons[2].Properties.Visible = true;
                            lcgReceipt.CustomHeaderButtons[3].Properties.Enabled = false;
                        }
                        else
                        {
                            lcgReceipt.CustomHeaderButtons[1].Properties.Visible = false;
                            lcgReceipt.CustomHeaderButtons[2].Properties.Visible = false;
                            lcgReceipt.CustomHeaderButtons[3].Properties.Enabled = true;
                        }
                    }

                    lcgReceipt.EndUpdate();
                }

                _currentReceipt.EndEdit();
                lcgLicence.EndInit();

                if (ConvertUtil.ToInt32(_currentReceipt["B_GRADE"]) == 1)
                    lcBGrade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    lcBGrade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if (_reUse)
                    lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lgcProxy.EndUpdate();

                if (!ProjectInfo._userType.Equals("E"))
                    setPartReadonly();

                if (!initialize)
                    Dangol.CloseSplash();
            }
            else
            {
                setReadonly();
                _currentReceipt = null;
                _dtComponentList.Clear();
                lgcProxy.BeginUpdate();
                lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcBGrade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lgcProxy.EndUpdate();
                _proxyId = -1;
                _receipt = "";
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


                if ((_proxyState > 0 && _proxyState < 4) && ConvertUtil.ToBoolean(_currentPart["ASSIGN_YN"]))
                    gridColumn21.OptionsColumn.AllowEdit = true;
                else
                    gridColumn21.OptionsColumn.AllowEdit = false;

                if (ConvertUtil.ToInt32(_currentPart["CONSIGNED_TYPE"]) == 2)
                    gvPart.Appearance.FocusedRow.ForeColor = Color.Red;
                else
                    gvPart.Appearance.FocusedRow.ForeColor = Color.Black;
            }
            else
            {
                _partPrice = 0;
                _currentPart = null;
            }
        }

        private bool saveAdjustmentInfo()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
            jobj.Add("ADJUSTMENT_TYPE", 1);
            jobj.Add("PRICE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]));
            jobj.Add("PRICE_PRODUCE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"]));
            jobj.Add("PRICE_REPRODUCE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"]));
            jobj.Add("PRICE_DELIVERY", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"]));
            jobj.Add("PRICE_QUICK", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_QUICK"]));
            jobj.Add("REGISTER_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (DBConsigned.updateAdjust(jobj, ref jResult))
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

        private void getReceiptPart()
        {
            _dtReceiptPartGrid.Clear();

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", _proxyId);
            jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"]));
            jobj.Add("RECEIPT_DT", ConvertUtil.ToString(_currentReceipt["RECEIPT_DT"]));

            if (DBConsigned.getConsignedReceiptPart(jobj, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                long price = 0;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceiptPart.NewRow();

                    long componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                    int releaseCnt = ConvertUtil.ToInt32(obj["RELEASE_CNT"]);
                    bool assignYn = releaseCnt > 0 ? true : false;

                    DataRow drGrid = _dtReceiptPartGrid.NewRow();

                    drGrid["NO"] = index++;
                    drGrid["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    drGrid["DETAIL_DATA"] = obj["DETAIL_DATA"];
                    drGrid["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    drGrid["ASSIGN_YN"] = assignYn;
                    drGrid["PROXY_PRICE"] = obj["PROXY_PRICE"];
                    drGrid["CHECK"] = false;

                    _dtReceiptPartGrid.Rows.Add(drGrid);

                    price += ConvertUtil.ToInt64(obj["PROXY_PRICE"]);

                }

                _currentReceipt["PART_PRICE"] = price;
            }
        }


        private void getConsignedInfo()
        {
            //gvReceipt.BeginDataUpdate();
            _dtReceiptPart.BeginInit();

            _dtReceiptPart.Clear();
            _dtReceiptPartGrid.Clear();
            lbNote.Text = "";
            JObject jResult = new JObject();

            if (DBConsigned.getConsignedInfo(_currentReceipt["PROXY_ID"], ref jResult))
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
                //_currentReceipt["MODEL_NM_DETAIL"] = jResult["MODEL_NM_DETAIL"];
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

                //_currentReceipt["WORKER_DT"] = ConvertUtil.ToDateTimeNull(jResult["WORKER_DT"]);
                //_currentReceipt["PRODUCT_DT"] = ConvertUtil.ToDateTimeNull(jResult["PRODUCT_DT"]);
                //_currentReceipt["PACKAGE_DT"] = ConvertUtil.ToDateTimeNull(jResult["PACKAGE_DT"]);
                //_currentReceipt["COMPLETE_DT"] = ConvertUtil.ToDateTimeNull(jResult["COMPLETE_DT"]);

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

                for (int i = 0; i < _consignedComponetCd.Length; i++)
                {
                    DataRow row = _dtReceiptPart.NewRow();

                    row["ID"] = nId;
                    row["P_PART_ID"] = -1;
                    row["PART_ID"] = i;
                    row["COMPONENT_ID"] = -1;
                    row["CONSIGNED_TYPE"] = -1;
                    row["COMPONENT_CD"] = _consignedComponetCd[i];
                    row["COMPONENT_CD_T"] = _consignedComponetCd[i];
                    //row["MODEL_NM"] = _consignedComponetNm[i];
                    row["MODEL_NM"] = "";
                    row["PART_CNT"] = 0;

                    _dtReceiptPart.Rows.Add(row);
                }

                List<long> listPart = new List<long>();
                List<long> listModel = new List<long>();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;

                long price = 0;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    string componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                    if(componentCd.Equals("MBD"))
                        lbNote.Text = $"{obj["NOTE_NM"]}";

                    listPart.Add(ConvertUtil.ToInt64(obj["COMPONENT_ID"]));

                    long partId = _dicConsignedComponentCdReverse[componentCd];

                    DataRow dr = _dtReceiptPart.NewRow();

                    long componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                    int releaseCnt = ConvertUtil.ToInt32(obj["RELEASE_CNT"]);
                    bool assignYn = releaseCnt > 0 ? true : false;

                    dr["ID"] = ConvertUtil.ToInt64(obj["ID"]);
                    dr["P_PART_ID"] = partId;
                    dr["PART_ID"] = obj["COMPONENT_ID"];
                    dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                    dr["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    dr["COMPONENT_CD_T"] = obj["COMPONENT_CD"];
                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    dr["MODEL_NM"] = obj["MODEL_NM"];
                    dr["PART_CNT"] = obj["COMPONENT_CNT"];
                    dr["ASSIGN_YN"] = assignYn;

                    _dtReceiptPart.Rows.Add(dr);


                    DataRow drGrid = _dtReceiptPartGrid.NewRow();

                    drGrid["NO"] = index++;
                    drGrid["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    drGrid["COMPONENT_ID"] = obj["COMPONENT_ID"];
                    drGrid["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    drGrid["DETAIL_DATA"] = obj["DETAIL_DATA"];
                    drGrid["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    drGrid["ASSIGN_YN"] = assignYn;
                    drGrid["PROXY_PRICE"] = obj["PROXY_PRICE"];


                    drGrid["CHECK"] = false;

                    _dtReceiptPartGrid.Rows.Add(drGrid);

                    price += ConvertUtil.ToInt64(obj["PROXY_PRICE"]);

                    DataRow[] rows = _dtReceiptPart.Select($"ID = {nId} AND PART_ID = {partId} AND P_PART_ID = -1");

                    foreach (DataRow row in rows)
                    {
                        int partCnt = ConvertUtil.ToInt32(row["PART_CNT"]);
                        partCnt++;
                        row["PART_CNT"] = partCnt;
                    }
                }

                _currentReceipt["PART_PRICE"] = price;

                if (_dicReceiptPart.ContainsKey(_id))
                    _dicReceiptPart[_id] = listPart;
                else
                    _dicReceiptPart.Add(_id, listPart);

            }

            _dtReceiptPart.EndInit();
            //gvReceipt.EndDataUpdate();
        }

        private void getLicenceInfo()
        {
            JObject jResult = new JObject();
            JObject jojb = new JObject();
            jojb.Add("PROXY_ID", ConvertUtil.ToInt64(_proxyId));

            string receiptDt = ConvertUtil.ToString(_currentReceipt["RECEIPT_DT"]);
            DateTime dtDate = Convert.ToDateTime(receiptDt);
            DateTime expiredDt = dtDate.AddMonths(-1);

            //jojb.Add("EXPIRED_DT", expiredDt.ToString("yyyy-MM-dd"));
            //jojb.Add("RECEIPT_DT", dtDate.ToString("yyyy-MM-dd"));

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


                    if (modelId> 0)
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
                        else if (serialType == 2)
                        {
                            _dtCoa.Rows[0]["SERIAL_NO"] = systemSn.Replace('/', ' ').Trim();
                            _dtCoa.Rows[1]["SERIAL_NO"] = systemSn.Replace('/', ' ').Trim();
                        }
                        else if (serialType == 3)
                        {
                            string productNm = ConvertUtil.ToString(jMBD["PRODUCT_NAME"]);
                            _dtCoa.Rows[0]["SERIAL_NO"] = $"{productNm.Replace('/', ' ').Trim()}{systemSn.Replace('/', ' ').Trim()}";
                            _dtCoa.Rows[1]["SERIAL_NO"] = $"{productNm.Replace('/', ' ').Trim()}{systemSn.Replace('/', ' ').Trim()}";
                        }
                    }
                }
                if (_proxyState < 5)
                {
                    bool reUse = ConvertUtil.ToBoolean(jResult["REUSE"]);
                    _reUse = _reUse || reUse;
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
                _reUse = false;

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

        private void getAdjustmentInfo()
        {
            JObject jResult = new JObject();

            _dtAdjustmentHistory.Clear();

            if (DBConsigned.getAdjustmentInfo(_currentReceipt["PROXY_ID"], _currentReceipt["COMPANY_ID"], 1, _currentReceipt["PRODUCT_TYPE"], ref jResult))
            {
                if (ConvertUtil.ToInt32(jResult["COMPLETE_YN"]) == 1 && _proxyState == 4)
                {
                    _dtAdjustMentProcess.Rows[0]["PRICE"] = jResult["PRICE"];
                    _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = jResult["PRICE_DELIVERY"];
                    _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = jResult["PRICE_PRODUCE"];
                    _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = jResult["PRICE_QUICK"];
                    _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = jResult["PRICE_REPRODUCE"];
                }
                else
                {
                    int releaseType = ConvertUtil.ToInt32(_currentReceipt["RELEASE_TYPE"]);

                    _dtAdjustMentProcess.Rows[0]["PRICE"] = _currentReceipt["PART_PRICE"];

                    if (_partRelease == 1)
                    {
                        _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = 0;
                        _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = 0;
                        _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = 0;
                        _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = 0;
                    }
                    else if (_preRelease == 1)
                    {
                        _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = 0;
                        _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = 0;
                        _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = 0;
                        _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = 0;
                    }
                    else if(releaseType > 1)
                    {
                        _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = 0;
                        if (_reUse)
                            _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = 0;
                        else
                            _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = jResult["PRICE_PRODUCE"];
                        _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = jResult["PRICE_QUICK"];
                        _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = jResult["PRICE_REPRODUCE"];
                    }
                    else
                    {
                        _dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"] = jResult["PRICE_DELIVERY"];
                        if (_reUse)
                            _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = 0;
                        else
                            _dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"] = jResult["PRICE_PRODUCE"];
                        _dtAdjustMentProcess.Rows[0]["PRICE_QUICK"] = jResult["PRICE_QUICK"];
                        _dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"] = jResult["PRICE_REPRODUCE"];
                    }
                }
 
                long totalPrice = ConvertUtil.ToInt64(_currentReceipt["PART_PRICE"])
                        + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"])
                        + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_QUICK"])
                        + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"])
                        + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"]);

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

                //Dangol.Message(jResult["MSG"]);
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


        private void rgProcess_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if(_currentReceipt == null)
            {
                Dangol.Message("선택된 접수가 없습니다.");
                e.Cancel = true;
                return;
            }

            int state = ConvertUtil.ToInt32(e.NewValue);

            if (state >= 90)
                return;

            if (Dangol.MessageYN($"현재 접수를 '{_dicProxyState[e.NewValue.ToString()]}'처리하시겠습니까?") == DialogResult.Yes)
            {
                int receiptStatus = ConvertUtil.ToInt32(e.NewValue);
                int preStatus = ConvertUtil.ToInt32(e.OldValue);

                if (receiptStatus == 1)
                {
                    string invoice = ConvertUtil.ToString(_currentReceipt["INVOICE"]);
                    if (string.IsNullOrWhiteSpace(invoice))
                    {
                        Dangol.Message("배송정보 입력 후 처리 가능합니다.");
                        e.Cancel = true;
                        return;
                    }
                }

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]));
                jobj.Add("CHECKED", 1);
                jobj.Add("PROXY_STATE", receiptStatus);
                jobj.Add("KEY", _receiptStateCd[receiptStatus]);
                jobj.Add("KEY_NUM", receiptStatus);

                if (_reUse && receiptStatus > 0)
                    jobj.Add("REUSE_YN", 1);
                else
                    jobj.Add("REUSE_YN", 0);

                if (receiptStatus == 2)
                {
                    string manageNo = teManageNo.Text.Trim();
                    string coa = ConvertUtil.ToString(_dtCoa.Rows[1]["COA"]);

                    if (manageNo.Length != 0 && manageNo.Length != 12)
                    {
                        Dangol.Message("관리번호를 확인하세요(자리수 다름).");
                        e.Cancel = true;
                        return;
                    }

                    saveCouponInfo();
                    //saveLicenceInfo();
                    saveAdjustmentInfo();

                    if (manageNo.Length == 0)
                        manageNo = _proxyId.ToString();

                    if (coa.Length == 0)
                        coa = _proxyId.ToString();
 
                    JObject jobjLicence = new JObject();
                    jobjLicence.Add("PROXY_ID", _proxyId);
                    jobjLicence.Add("COUPON", manageNo);
                    jobjLicence.Add("CUSTOMER", ConvertUtil.ToString(teCustomerNo.Text.Trim()));
                    jobjLicence.Add("COA", coa);
                    jobjLicence.Add("USE_YN", 1);
                    jobjLicence.Add("COA_CHECK", 0);

                    if (!DBConsigned.updateLicenceInfo(jobjLicence, ref jResult))
                    {
                        e.Cancel = true;
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
                else if(receiptStatus == 1 || receiptStatus == 0)
                {   
                    string productId = ConvertUtil.ToString(_currentReceipt["PRODUCT_ID"]);

                    if (!string.IsNullOrEmpty(productId))
                    {
                        JObject jobjLicence = new JObject();
                        jobjLicence.Add("PROXY_ID", _proxyId);
                        jobjLicence.Add("USE_YN", 0);

                        if (!DBConsigned.updateLicenceUseInfo(jobjLicence, ref jResult))
                        {
                            e.Cancel = true;
                            Dangol.Message(jResult["MSG"]);
                            return;
                        }
                    }
                }


                if (receiptStatus == 4)
                {
                    long Price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);

                    if (_partRelease == 1 && Price > 0)
                    {
                        if (Dangol.MessageYN($"부품출고에 부품가격이 산정되어있습니다. 그래도 출고완료하시겠습니까?") == DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    //saveLicenceInfo();

                    jobj.Add("RELEASE_CHARGE", ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]));
                }
                else
                {
                    jobj.Add("RELEASE_CHARGE", 0);
                }

                if (DBConsigned.updateReceiptStatus(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentReceipt["PROXY_STATE"] = receiptStatus.ToString();
                    //_currentReceipt["RELEASE_STATE"] = receiptStatus.ToString(); 


                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (receiptStatus == 0)
                    {
                        //_dtAdjustMentProcess.BeginInit();

                        //long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
                        //long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

                        //_dtAdjustMentProcess.Rows[0]["PRICE"] = 0;
                        //_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - price;

                        //_partPrice = 0;

                        //_dtAdjustMentProcess.EndInit();
                    }
                    else if (receiptStatus == 1)
                    {
                        _currentReceipt["WORKER_ID"] = ProjectInfo._userId;
                        _currentReceipt["WORKER_DT"] = now;
                    }
                    else if (receiptStatus == 2)
                    {
                        _currentReceipt["PRODUCT_ID"] = ProjectInfo._userId;
                        _currentReceipt["PRODUCT_DT"] = now;
                    }
                    else if (receiptStatus == 3)
                    {
                        _currentReceipt["PACKAGE_ID"] = ProjectInfo._userId;
                        _currentReceipt["PACKAGE_DT"] = now;
                    }
                    else if (receiptStatus == 4)
                    {
                        _currentReceipt["COMPLETE_ID"] = ProjectInfo._userId;
                        _currentReceipt["COMPLETE_DT"] = now;
                    }

                    

                    //if(receiptStatus == 91)
                    //{
                    //    _dtAdjustMentProcess.BeginInit();

                    //    long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
                    //    long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

                    //    _dtAdjustMentProcess.Rows[0]["PRICE"] = 0;
                    //    _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - price;

                    //    _dtAdjustMentProcess.EndInit();
                    //}

                    _proxyState = receiptStatus;

                    setAdjustmentReadonly();
                    setStatistics();
                    setPartReadonly();

                    lcgReceipt.BeginUpdate();

                    if (ConvertUtil.ToInt32(_currentReceipt["RETURN_YN"]) == 0 && _proxyState == 4)
                    {
                        lcgReceipt.CustomHeaderButtons[1].Properties.Visible = true;
                        lcgReceipt.CustomHeaderButtons[2].Properties.Visible = true;
                    }
                    else
                    {
                        lcgReceipt.CustomHeaderButtons[1].Properties.Visible = false;
                        lcgReceipt.CustomHeaderButtons[2].Properties.Visible = false;
                    }
                    lcgReceipt.EndUpdate();

                    if (_proxyState < 4)
                        _dtAdjustMentProcess.Rows[0]["PRICE"] = _currentReceipt["PART_PRICE"];

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
                return;
            }


        }

        private void receiptRefresh()
        {
            string receipt = _receipt;
            setStatistics();
            gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
            getReceiptList(false);
            gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
            lcgReceiptList.CustomHeaderButtons[2].Properties.Checked = false;
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
            initialize = true;
            receiptRefresh();
            initialize = false;
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
                    dr["MODEL_NM_DETAIL"] = ConvertUtil.ToString(obj["MODEL_NM_DETAIL"]);
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

                    dr["WORKER_ID"] = obj["WORKER_ID"];
                    dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                    dr["PACKAGE_ID"] = obj["PACKAGE_ID"];
                    dr["COMPLETE_ID"] = obj["COMPLETE_ID"];

                    dr["B_GRADE"] = obj["B_GRADE"];
                    dr["PART_YN"] = obj["PART_YN"];
                    dr["PRE_YN"] = obj["PRE_YN"];
                    dr["REUSE_YN"] = obj["REUSE_YN"];

                    
                    dr["RECEIPT_DTF"] = ConvertUtil.ToDateTimeNullWithTime(obj["CREATE_DT"]); 
                    dr["WORKER_DT"] = ConvertUtil.ToDateTimeNullWithTime(obj["WORKER_DT"]);
                    dr["PRODUCT_DT"] = ConvertUtil.ToDateTimeNullWithTime(obj["PRODUCT_DT"]);
                    dr["PACKAGE_DT"] = ConvertUtil.ToDateTimeNullWithTime(obj["PACKAGE_DT"]);
                    dr["COMPLETE_DT"] = ConvertUtil.ToDateTimeNullWithTime(obj["COMPLETE_DT"]);


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
            {
                return true;
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return false;
            }
        }

        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            string manageNo = teManageNo.Text.Trim();

            if (manageNo.Length != 0 && manageNo.Length != 12)
            {
                Dangol.Warining("관리번호를 확인하세요(자리수 다름).");
                return;
            }


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
                serialNo = ConvertUtil.ToString(_dtCoa.Rows[1]["SERIAL_NO"]);
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

                if (!string.IsNullOrWhiteSpace(productNm))
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
            if (!e.Column.FieldName.Equals("TOTAL_PRICE"))
            {
                long totalPrice = ConvertUtil.ToInt64(_currentReceipt["PART_PRICE"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_DELIVERY"])
                        + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_QUICK"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_REPRODUCE"]) + ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE_PRODUCE"]);

                _dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice;
            }
        }

        private void gcReceip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                initialize = true;
                receiptRefresh();
                initialize = false;
            }
        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            
        }

        private void refresh()
        {
           
            int rowHandle = gvReceipt.FocusedRowHandle;
            gvReceipt.FocusedRowHandle = -2147483646;
            gvReceipt.FocusedRowHandle = rowHandle;
            
            setStatistics();
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
                _currentReceipt["PART_PRICE"] = _dtAdjustMentProcess.Rows[0]["PRICE"];

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

        private void receiptSave()
        {

            if (ConvertUtil.ToInt32(_currentReceipt["PROXY_STATE"]) > 0)
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

            jobj.Add("PROXY_ID", $"{ _currentReceipt["PROXY_ID"]}");
            jobj.Add("PC_TYPE", $"{ _currentReceipt["PRODUCT_TYPE"]}");
            jobj.Add("GUARANTEE_DUE", $"{ _currentReceipt["GUARANTEE"]}");
            jobj.Add("GUARANTEE_START", $"{ _currentReceipt["GUARANTEE_FROM"]}");
            jobj.Add("GUARANTEE_END", $"{ _currentReceipt["GUARANTEE_TO"]}");

            jobj.Add("SALE_ROOT", $"{ _currentReceipt["SALE_ROOT"]}");
            jobj.Add("RELEASE_TYPE", $"{ _currentReceipt["RELEASE_TYPE"]}");
            jobj.Add("DES", $"{ _currentReceipt["DES"]}");
            jobj.Add("REQUEST", $"{ _currentReceipt["REQUEST"]}");
            jobj.Add("MODEL_NM_DETAIL", $"{ _currentReceipt["MODEL_NM_DETAIL"]}");

            jobj.Add("ONLY_RECEIPT_INFO", 1);


            if (DBConsigned.updateReceipt(jobj, ref jResult))
            {
                if (ConvertUtil.ToInt32(_currentReceipt["RELEASE_TYPE"]) != _releaseType)
                {
                    getAdjustmentInfo();
                }

                Dangol.Message("처리되었습니다.");
                return;
            }
            else
            {
                Dangol.Message($"{jResult["MSG"]}");
                return;
            }
        }

        private void setCancel()
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

                string productId = ConvertUtil.ToString(_currentReceipt["PRODUCT_ID"]);

                if (!string.IsNullOrEmpty(productId))
                {
                    JObject jobjLicence = new JObject();
                    jobjLicence.Add("PROXY_ID", _proxyId);
                    jobjLicence.Add("USE_YN", 0);

                    if (!DBConsigned.updateLicenceUseInfo(jobjLicence, ref jResult))
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }

                if (DBConsigned.updateReceiptStatus(jobj, ref jResult))
                {
                    Dangol.ShowSplash();

                    _currentReceipt.BeginEdit();
                    _currentReceipt["PROXY_STATE"] = receiptStatus.ToString();
                    _currentReceipt.EndEdit();

                    //_dtAdjustMentProcess.BeginInit();

                    //long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
                    //long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

                    //_dtAdjustMentProcess.Rows[0]["PRICE"] = 0;
                    //_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - price;

                    //_dtAdjustMentProcess.EndInit();

                    _proxyState = receiptStatus;

                    //rgProcess.EditValue = "91";

                    getConsignedInfo();
                    getLicenceInfo();
                    getAdjustmentInfo();

                    setAdjustmentReadonly();
                    setStatistics();
                    setPartReadonly();

                    Dangol.CloseSplash();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }

            }
        }

        private void setHold()
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

                string productId = ConvertUtil.ToString(_currentReceipt["PRODUCT_ID"]);

                if (!string.IsNullOrEmpty(productId))
                {
                    JObject jobjLicence = new JObject();
                    jobjLicence.Add("PROXY_ID", _proxyId);
                    jobjLicence.Add("USE_YN", 0);

                    if (!DBConsigned.updateLicenceUseInfo(jobjLicence, ref jResult))
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }

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

        private void setDelete()
        {
            if (Dangol.MessageYN($"현재 접수를 삭제하시겠습니까?") == DialogResult.Yes)
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

                string productId = ConvertUtil.ToString(_currentReceipt["PRODUCT_ID"]);

                if (!string.IsNullOrEmpty(productId))
                {
                    JObject jobjLicence = new JObject();
                    jobjLicence.Add("PROXY_ID", _proxyId);
                    jobjLicence.Add("USE_YN", 0);

                    if (!DBConsigned.updateLicenceUseInfo(jobjLicence, ref jResult))
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }

                if (DBConsigned.updateReceiptStatus(jobj, ref jResult))
                {    
                    jobj.RemoveAll();
                    jobj.Add("PROXY_ID", _proxyId);
                    jobj.Add("PROXY_STATE", -1);

                    DBConsigned.updateReceiptSimple(jobj, ref jResult);

                    gvReceipt.BeginDataUpdate();
                    _currentReceipt.Delete();
                    gvReceipt.EndDataUpdate();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
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

            if (e.Button.Properties.Tag.Equals(0))
            {
                int rowhandle = gvPart.FocusedRowHandle;
                gvPart.FocusedRowHandle = -1;
                gvPart.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtReceiptPartGrid.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 부품을 할당하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    string componentCd;
                    string url;
                    foreach (DataRow row in rows)
                    {
                        componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);

                        if (!ConvertUtil.ToBoolean(row["ASSIGN_YN"]) || componentCd.Equals("MBD"))
                        {
                            jobj.RemoveAll();
                            jobj.Add("PROXY_ID", _proxyId);
                            jobj.Add("PROXY_PART_ID", ConvertUtil.ToInt64(row["PROXY_PART_ID"]));
                            jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"]));
                            jobj.Add("COMPONENT_ID", ConvertUtil.ToInt64(row["COMPONENT_ID"]));
                            jobj.Add("CONSIGNED_TYPE", ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]));
                            jobj.Add("COMPONENT_CD", componentCd);

                            if (_preRelease == 1)
                            {
                                if (!componentCd.Equals("PKG") && !componentCd.Equals("AIR"))
                                    jobj.Add("PRICE_EXCETP", 1);
                            }
                            else if (_partRelease == 1)
                            {
                                jobj.Add("PRICE_EXCETP", 1);
                            }

                            if (componentCd.Equals("MBD"))
                                url = "/consigned/asignConsignedReleaseInventoryMBD.json";
                            else if (componentCd.Equals("MEM"))
                                url = "/consigned/asignConsignedReleaseInventoryMEM.json";
                            else
                                url = "/consigned/asignConsignedReleaseInventory.json";

                            DBConsigned.assignConsignedReleaseInventory(jobj, url, ref jResult);

                        }
                    }

                    getReceiptPart();
                    Dangol.CloseSplash();

                    //gvPart.EndDataUpdate();

                    if (_proxyState < 4)
                        _dtAdjustMentProcess.Rows[0]["PRICE"] = _currentReceipt["PART_PRICE"];

                    Dangol.Message("처리되었습니다.");
                }
            }
            else if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvPart.FocusedRowHandle;
                gvPart.FocusedRowHandle = -1;
                gvPart.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtReceiptPartGrid.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 부품을 자사재고로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 1);
                    jobj.Add("CONSIGNED_TYPE", 2);

                    List<long> listProxyPartId = new List<long>();
                    foreach (DataRow row in rows)
                        listProxyPartId.Add(ConvertUtil.ToInt64(row["PROXY_PART_ID"]));

                    jobj.Add("LIST_PROXY_PART_ID", string.Join(",", listProxyPartId));

                    if (DBConsigned.updateProxyComponentUnit(jobj, ref jResult))
                    {
                        gvPart.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["CONSIGNED_TYPE"] = 2;
                        }

                        gvPart.EndDataUpdate();
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
                int rowhandle = gvPart.FocusedRowHandle;
                gvPart.FocusedRowHandle = -1;
                gvPart.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtReceiptPartGrid.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 부품을 생산대행재고로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 1);
                    jobj.Add("CONSIGNED_TYPE", 1);

                    List<long> listProxyPartId = new List<long>();
                    foreach (DataRow row in rows)
                        listProxyPartId.Add(ConvertUtil.ToInt64(row["PROXY_PART_ID"]));

                    jobj.Add("LIST_PROXY_PART_ID", string.Join(",", listProxyPartId));

                    if (DBConsigned.updateProxyComponentUnit(jobj, ref jResult))
                    {
                        gvPart.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["CONSIGNED_TYPE"] = 1;
                        }

                        gvPart.EndDataUpdate();
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
                using (DlgConsignedPartModif consignedPartModif = new DlgConsignedPartModif(_proxyId, _receipt, ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"])))
                {
                    consignedPartModif.StartPosition = FormStartPosition.Manual;
                    consignedPartModif.Location = new Point(this.Location.X + (this.Size.Width / 2) - (consignedPartModif.Size.Width / 2),
                    this.Location.Y + (this.Size.Height / 2) - (consignedPartModif.Size.Height / 2));

                    if (consignedPartModif.ShowDialog(this) == DialogResult.OK)
                    {
                        Dangol.ShowSplash();
                        getReceiptPart();
                        Dangol.CloseSplash();

                    }
                }

            }
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

        private void setReturnRequest()
        {
            if (_currentReceipt == null)
            {
                Dangol.Message("선택된 접수가 없습니다.");
                return;
            }

            if (Dangol.MessageYN("반품/교환 신청하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();

                JObject jobj = new JObject();
                JObject jResult = new JObject();

                jobj.Add("PROXY_ID", _proxyId);
                jobj.Add("AS_YN", 0);

                if (DBConsigned.returnReceipt(jobj, ref jResult))
                {
                    _currentReceipt["RETURN_YN"] = 1;
                    _currentReceipt["C_PROXY_ID"] = ConvertUtil.ToInt64(jResult["PROXY_ID"]);

                    lcgReceipt.BeginUpdate();
                    lcgReceipt.CustomHeaderButtons[1].Properties.Visible = false;
                    lcgReceipt.EndUpdate();

                    Dangol.CloseSplash();
                    Dangol.Message("반품/교환이 접수되었습니다.");
                }
                else
                {
                    Dangol.CloseSplash();
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private void setPartRelease()
        {
            if (_currentReceipt == null)
            {
                Dangol.Message("선택된 접수가 없습니다.");
                return;
            }

            if (Dangol.MessageYN("부품출고를 신청하시겠습니까?") == DialogResult.Yes)
            {
                using (DlgReceiptPartList dlgImportBarcode = new DlgReceiptPartList(_proxyId, _dtReceiptPartGrid))
                {
                    dlgImportBarcode.ShowDialog();

                    if (dlgImportBarcode.DialogResult == DialogResult.OK)
                    {
                        Dangol.ShowSplash();
                        getReceiptList(false);
                        lcgReceiptList.CustomHeaderButtons[2].Properties.Checked = false;
                        Dangol.CloseSplash();
                        gvReceipt.MoveFirst();
                        Dangol.Message("처리되었습니다.");

                    }
                }


            }
        }

        private void lcgReceiptList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            gvReceipt.FocusedRowHandle = -1;
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
            gvReceipt.FocusedRowHandle = -1;
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

        private void lcgReceiptList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvReceipt.FocusedRowHandle;
            gvReceipt.FocusedRowHandle = -1;
            gvReceipt.FocusedRowHandle = rowhandle;

            if (e.Button.Properties.Tag.Equals(1))
            {
                DataRow[] rows = _dtReceipt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 접수를 '생산완료'로 변경하시겠습니까?('처리중' 접수만 수행)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();
                    setProxyStateChecked(1, 2);
                    Dangol.CloseSplash();
                    Dangol.Message("처리되었습니다.");
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                DataRow[] rows = _dtReceipt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 접수를 '포장완료'로 변경하시겠습니까?('생산완료' 접수만 수행)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();
                    setProxyStateChecked(2, 3);
                    Dangol.CloseSplash();
                    Dangol.Message("처리되었습니다.");
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                DataRow[] rows = _dtReceipt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 접수를 '출고완료'로 변경하시겠습니까?('포장완료' 접수만 수행)") == DialogResult.Yes)
                {
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    Dangol.ShowSplash();

                    List<long> listProxyId = new List<long>();
                    List<long> listCompanyId = new List<long>();
                    long proxyId;
                    long companyId;
                    int proxyState;
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    foreach (DataRow row in rows)
                    {
                        proxyState = ConvertUtil.ToInt32(row["PROXY_STATE"]);

                        if (proxyState == 3)
                        {
                            proxyId = ConvertUtil.ToInt64(row["PROXY_ID"]);
                            companyId = ConvertUtil.ToInt64(row["COMPANY_ID"]);

                            listProxyId.Add(proxyId);
                            listCompanyId.Add(companyId);
                        }
                    }

                    if(listProxyId.Count <1)
                    {
                        Dangol.CloseSplash();
                        Dangol.Warining("'출고완료' 가능한 접수가 없습니다.");
                        return;
                    }

                    jobj.Add("BULK_YN", 1);
                    jobj.Add("LIST_PROXY_ID", string.Join(",", listProxyId));
                    jobj.Add("LIST_COMPANY_ID", string.Join(",", listCompanyId));



                    if (DBConsigned.consignedReleaseComplete(jobj, ref jResult))
                    {
                        List<long> completeProxyId = new List<long>();
                        JArray jArray = JArray.Parse(jResult["LIST_PROXY_ID"].ToString());
                        long id;
                        foreach (object data in jArray)
                        {
                            id = ConvertUtil.ToInt64(data);
                            if(id > 0)
                                completeProxyId.Add(id);
                            
                        }

                        foreach (DataRow row in rows)
                        {
                            proxyState = ConvertUtil.ToInt32(row["PROXY_STATE"]);

                            if (proxyState == 3)
                            {
                                proxyId = ConvertUtil.ToInt64(row["PROXY_ID"]);
                                if (completeProxyId.Contains(proxyId))
                                {
                                    row.BeginEdit();
                                    row["PROXY_STATE"] = "4";
                                    row["COMPLETE_ID"] = ProjectInfo._userId;
                                    row["COMPLETE_DT"] = now;
                                    row.EndEdit();
                                }
                            }
                        }

                        if (completeProxyId.Contains(_proxyId))
                            refresh();
                    }

                    Dangol.CloseSplash();
                    Dangol.Message("처리되었습니다.");
                }
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        foreach (GridColumn gc in _arrTimeColumn)
                            gc.Visible = true;

                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcReceip.ExportToXlsx(form.FileName, options);

                        foreach (GridColumn gc in _arrTimeColumn)
                            gc.Visible = false;

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
        }

        private void setProxyStateChecked(int curState, int nextState)
        {
            JObject jobj = new JObject();
            JObject jResult = new JObject();
            long proxyId;
            int proxyState;
            DataRow[] rows = _dtReceipt.Select("CHECK = TRUE");
            List<long> completeProxyId = new List<long>();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            gvReceipt.BeginDataUpdate();

            foreach (DataRow row in rows)
            {
                proxyId = ConvertUtil.ToInt64(row["PROXY_ID"]);
                proxyState = ConvertUtil.ToInt32(row["PROXY_STATE"]);

                if (proxyState == curState)
                {
                    jobj.RemoveAll();

                    jobj.Add("PROXY_ID", proxyId);
                    jobj.Add("CHECKED", 1);
                    jobj.Add("PROXY_STATE", nextState);
                    jobj.Add("KEY", _receiptStateCd[nextState]);
                    jobj.Add("KEY_NUM", nextState);
                    jobj.Add("RELEASE_CHARGE", 0);
                    jobj.Add("REUSE_YN", 0);

                    if (DBConsigned.updateReceiptStatus(jobj, ref jResult))
                    {
                        row.BeginEdit();
                        row["PROXY_STATE"] = nextState.ToString();

                        if (nextState == 2)
                        {
                            row["PRODUCT_ID"] = ProjectInfo._userId;
                            row["PRODUCT_DT"] = now;
                        }
                        else if (nextState == 3)
                        {
                            row["PACKAGE_ID"] = ProjectInfo._userId;
                            row["PACKAGE_DT"] = now;
                        }

                        //_currentReceipt["PACKAGE_ID"] = ProjectInfo._userId;

                        row.EndEdit();

                        completeProxyId.Add(proxyId);
                    }
                }
            }

            gvReceipt.EndDataUpdate();

            if (completeProxyId.Contains(_proxyId))
                refresh();
        }

        private void gvReceipt_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (e.Column.FieldName != "RECEIPT" || e.Column.FieldName != "CUSTOMER_NM1")
            {
                bool check = ConvertUtil.ToBoolean(_currentReceipt["CHECK"]);
                gvReceipt.BeginDataUpdate();
                _currentReceipt.BeginEdit();
                _currentReceipt["CHECK"] = !check;
                _currentReceipt.EndEdit();
                gvReceipt.EndDataUpdate();
            }
        }

        private void gvReceipt_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int state = ConvertUtil.ToInt32(gvReceipt.GetDataRow(e.RowHandle)["PROXY_STATE"]);
                if (state == 90)
                {
                    e.Appearance.ForeColor = Color.Purple;
                }
                else if (state == 91)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void usrConsignedList_Enter(object sender, EventArgs e)
        {
            if (!initializeEnter)
            {
                initialize = true;
                receiptRefresh();
                initialize = false;
            }

            initializeEnter = false;
        }

        private void lcgReceipt_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentReceipt == null)
            {
                Dangol.Message("선택된 접수가 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(0))
            {
                setDelete();
            }
            else if (e.Button.Properties.Tag.Equals(1))
            {
                setReturnRequest();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                setPartRelease();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                receiptSave();
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                setHold();
            }
            else if (e.Button.Properties.Tag.Equals(5))
            {
                setCancel();
            }
            else if (e.Button.Properties.Tag.Equals(6))
            {
                refresh();
            }

        }

        private void sbReleaseDtUpdate_Click(object sender, EventArgs e)
        {
            var today = DateTime.Now;
            DateTime dtDate = DateTime.Parse($"{deRelease.Text} {today.ToString("HH:mm:ss")}");
            string releaseDt = dtDate.ToString("yyyy-MM-dd HH:mm:ss");

            if(_proxyState != 4)
            {
                Dangol.Warining("'출고완료'상태에서만 변경 가능합니다.");
                return;
            }


            if (Dangol.MessageYN($"출고일자를 '{releaseDt}'로를 변경하시겠습니까?") != DialogResult.Yes)
            {
                return;
            }

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            jobj.Add("PROXY_ID", _proxyId);
            jobj.Add("RELEASE_DT", releaseDt);
            jobj.Add("COMPLETE_DT", releaseDt);

            if (DBConsigned.updateReceiptSimple(jobj, ref jResult))
            {
                Dangol.Message("처리되었습니다.");
            }
        }
    }
}