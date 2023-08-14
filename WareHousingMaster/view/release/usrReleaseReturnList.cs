using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.Report;
using WareHousingMaster.view.common;
using WareHousingMaster.view.PreView;
using WareHousingMaster.view.usedPurchase;

namespace WareHousingMaster.view.release
{
    public partial class usrReleaseReturnList : DevExpress.XtraEditors.XtraForm
    {
        string _componentCd;

        DataRowView _currentReceipt;
        DataRowView _currentPart;
        DataRowView _currentAdjustment;

        DataRowView _currentComponentPart;

        DataTable _dtReceipt;
        DataTable _dtPartList;
        DataTable _dtPart;
        DataTable _dtAdjustment;

        DataTable _dtReceiptInvoice;
        //DataTable _dtAdjustMentProcess;

        //DataTable _dtAdjustmentHistory;

        //DataTable _dtAdjustMentReturn;

        BindingSource _bsReceipt;
        BindingSource _bsPartList;
        BindingSource _bsAdjustment;
        BindingSource _bsPart;
        //BindingSource _bsAdjustMentProcess;
        //BindingSource _bsAdjustmentHistory;

        Dictionary<long, List<long>> _dicReceiptPart;
        Dictionary<long, List<long>> _dicConsignedModel;

        string[] _consignedComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "SSD", "HDD", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG", "NTB" };
        string[] _consignedComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "SSD", "HDD", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스", "노트북" };

        string[] _receiptStateCd = new string[] { "RECEIPT_DT", "PROCESS_DT", "PRODUCT_DT", "PACKAGE_DT", "RELEASE_DT", "RETURN_REQUEST_DT", "RETURN_IN_DT", "EXCHANGE_DT", "RETURN_CANCEL_DT", "RETURN_COMPLETE_DT" };
        Dictionary<int, string> _dicConsignedComponentCd;
        Dictionary<string, int> _dicConsignedComponentCdReverse;

        Dictionary<string, string> _dicProxyState;

        string _currentGetComponentCd;

        Dictionary<string, string> _dicProductType;
        Dictionary<string, string> _dicGuarantee;

        List<string> _listReleaseRange;

        int _releaseState;
        long _exportId;
        long _representativeId;
        string _export = "";
        string _representativeCol;

        int _partCnt = 0;
        long _partPrice = 0;
        long _partReleasePrice = 0;
        long _partProduceCost = 0;

        int _isProduct;
        int _partRelease;
        int _productType;

        bool _reUse;

        int _preRelease;

        bool initialize = true;
        bool initializeEnter = true;
        public usrReleaseReturnList()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("EXPORT_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("R_EXPORT_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("P_EXPORT_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("C_EXPORT_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("EXPORT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RETURN_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PAYMENT", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("TAX_INVOICE", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TEL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("POSTAL_CD", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TAKE_USER_ID", typeof(string)));
            //_dtReceipt.Columns.Add(new DataColumn("PRODUCT", typeof(bool)));
            _dtReceipt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtReceipt.Columns.Add(new DataColumn("ERROR", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("DELIVERY_COMPANY", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("INVOICE", typeof(string)));

            
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RETURN_YN", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("WORKER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PACKAGE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPLETE_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_ID", typeof(long)));
            


            _dtPartList = new DataTable();
            _dtPartList.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPartList.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtPartList.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtPartList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtPartList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtPartList.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtPartList.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));
            _dtPartList.Columns.Add(new DataColumn("TOTAL_RELEASE_PRICE", typeof(long)));
            _dtPartList.Columns.Add(new DataColumn("TOTAL_PRODUCE_COST", typeof(long)));
            _dtPartList.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtPartList.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dtPartList.Columns.Add(new DataColumn("REPRESENTATIVE_ID", typeof(long)));
            _dtPartList.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));


            _dtPart = new DataTable();
            _dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("PART_PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("PRODUCE_COST", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("PRODUCT_STATE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtPart.Columns.Add(new DataColumn("LOCK_YN", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("NEW_COA", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("OLD_COA", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));

            //_dtAdjustMentProcess = new DataTable();
            //_dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE", typeof(long)));
            //_dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_DELIVERY", typeof(long)));
            //_dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_PRODUCE", typeof(long)));
            //_dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_QUICK", typeof(long)));
            //_dtAdjustMentProcess.Columns.Add(new DataColumn("PRICE_REPRODUCE", typeof(long)));
            //_dtAdjustMentProcess.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            //_dtAdjustmentHistory = new DataTable();
            //_dtAdjustmentHistory.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            //_dtAdjustmentHistory.Columns.Add(new DataColumn("USER_ID", typeof(string)));


            _dtAdjustment = new DataTable();
            _dtAdjustment.Columns.Add(new DataColumn("ADJUSTMENT_ID", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtAdjustment.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtAdjustment.Columns.Add(new DataColumn("PART_PRICE", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("PART_RELEASE_PRICE", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("PRODUCE_COST", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("DELIVERY_COST", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("SUM_COST", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("MARGIN", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("MARGIN_RATIO", typeof(double)));

            _dtReceiptInvoice = new DataTable();
            _dtReceiptInvoice.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptInvoice.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceiptInvoice.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
            _dtReceiptInvoice.Columns.Add(new DataColumn("EA", typeof(string)));
            _dtReceiptInvoice.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtReceiptInvoice.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            _dtReceiptInvoice.Columns.Add(new DataColumn("TOTAL_RELEASE_PRICE", typeof(long)));

            _bsReceipt = new BindingSource();
            _bsPartList = new BindingSource();
            _bsAdjustment = new BindingSource();
            _bsPart = new BindingSource();
            //_bsAdjustMentProcess = new BindingSource();
            //_bsAdjustmentHistory = new BindingSource();

            _dicReceiptPart = new Dictionary<long, List<long>>();
            _dicConsignedModel = new Dictionary<long, List<long>>();

            _dicProductType = new Dictionary<string, string>();
            _dicGuarantee = new Dictionary<string, string>();
            _dicProxyState = new Dictionary<string, string>();

            _dicConsignedComponentCd = new Dictionary<int, string>();
            _dicConsignedComponentCdReverse = new Dictionary<string, int>();

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                _dicConsignedComponentCd.Add(i, _consignedComponetCd[i]);
                _dicConsignedComponentCdReverse.Add(_consignedComponetCd[i], i);
            }

            _currentGetComponentCd = null;
            _releaseState = -1;
            _listReleaseRange = new List<string>();

            initialize = true;

            initializeEnter = true;
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

            teExport.DataBindings.Add(new Binding("Text", _bsReceipt, "EXPORT", false, DataSourceUpdateMode.OnPropertyChanged));
            lePayment.DataBindings.Add(new Binding("EditValue", _bsReceipt, "PAYMENT", false, DataSourceUpdateMode.OnPropertyChanged));
            deReceiptDt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT_DT", false, DataSourceUpdateMode.OnPropertyChanged));        
            leCompany.DataBindings.Add(new Binding("EditValue", _bsReceipt, "COMPANY_ID", false, DataSourceUpdateMode.OnPropertyChanged));
            leTaxInvoice.DataBindings.Add(new Binding("EditValue", _bsReceipt, "TAX_INVOICE", false, DataSourceUpdateMode.OnPropertyChanged));
            rgReleaseType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RELEASE_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            meDes.DataBindings.Add(new Binding("Text", _bsReceipt, "DES", false, DataSourceUpdateMode.OnPropertyChanged));
            teCustomerNm.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL", false, DataSourceUpdateMode.OnPropertyChanged));
            teHp.DataBindings.Add(new Binding("Text", _bsReceipt, "HP", false, DataSourceUpdateMode.OnPropertyChanged));
            tePostalCd.DataBindings.Add(new Binding("Text", _bsReceipt, "POSTAL_CD", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddress.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddressDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));
            rgProcess.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RELEASE_STATE", false, DataSourceUpdateMode.OnPropertyChanged));
            rgReturn.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RETURN_STATE", false, DataSourceUpdateMode.OnPropertyChanged));
            leDelivery.DataBindings.Add(new Binding("EditValue", _bsReceipt, "DELIVERY_COMPANY", false, DataSourceUpdateMode.OnPropertyChanged));
            teDelivery.DataBindings.Add(new Binding("Text", _bsReceipt, "INVOICE", false, DataSourceUpdateMode.OnPropertyChanged));
            leTakeUserId.DataBindings.Add(new Binding("EditValue", _bsReceipt, "TAKE_USER_ID", false, DataSourceUpdateMode.OnPropertyChanged));


            var today = DateTime.Today;
            var pastDate = today.AddDays(-7);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            leReleaseState.ItemIndex = 0;

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
            Util.LookupEditHelper(rileReleaseState, dtProxyState, "KEY", "VALUE");

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

            _listReleaseRange.Add("90");
            _listReleaseRange.Add("91");

            rgProcessState[indexProcess++] = rgItemHold;
            rgProcessState[indexProcess++] = rgItemCancel;


            this.rgProcess.Properties.Items.AddRange(rgProcessState);
            this.rgReturn.Properties.Items.AddRange(rgReturnState);
            //this.rgAS.Properties.Items.AddRange(rgASState);

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
                        _listReleaseRange.Add(stateS);
                    }
                }
            }
            Util.insertRowonTop(dtProxyProcessState, "-1", "선택안함");
            Util.LookupEditHelper(leReleaseState, dtProxyProcessState, "KEY", "VALUE");

            DataTable dtPayment = new DataTable();
            dtPayment.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtPayment.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtPayment, 2, "후");
            Util.insertRowonTop(dtPayment, 1, "선");
            Util.insertRowonTop(dtPayment, -1, "해당없음");

            Util.LookupEditHelper(lePayment, dtPayment, "KEY", "VALUE");

            //foreach (DataRow row in dtProductType.Rows)
            //    _dicProductType.Add(ConvertUtil.ToString(row["VALUE"]), ConvertUtil.ToString(row["KEY"]));

            //DataTable dtGuarantee = new DataTable();
            //dtGuarantee = Util.getCodeList("CD0906", "KEY", "VALUE");
            //Util.insertRowonTop(dtGuarantee, "0", "선택안함");
            //Util.LookupEditHelper(leGuarantee, dtGuarantee, "KEY", "VALUE");

            //foreach (DataRow row in dtGuarantee.Rows)
            //    _dicGuarantee.Add(ConvertUtil.ToString(row["VALUE"]), ConvertUtil.ToString(row["KEY"]));


            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "직접입력");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany1, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(leTakeUserId, dtUserId, "KEY", "VALUE");
            Util.LookupEditHelper(rileUser, dtUserId, "KEY", "VALUE");

            DataTable dtTaxInvoice = new DataTable();
            dtTaxInvoice.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtTaxInvoice.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtTaxInvoice, 1, "현금영수증");
            Util.insertRowonTop(dtTaxInvoice, 2, "미발행");
            Util.insertRowonTop(dtTaxInvoice, 1, "발행");
            Util.insertRowonTop(dtTaxInvoice, -1, "해당없음");

            Util.LookupEditHelper(leTaxInvoice, dtTaxInvoice, "KEY", "VALUE");

            DataTable dtDelivery = Util.getCodeList("CD0904", "KEY", "VALUE");
            Util.insertRowonTop(dtDelivery, "-1", "없음");
            Util.LookupEditHelper(leDelivery, dtDelivery, "KEY", "VALUE");

            DataTable dtInventoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState, dtInventoryState, "KEY", "VALUE");

            DataTable dtProductState = Util.getCodeList("CD0804", "KEY", "VALUE");
            Util.LookupEditHelper(rileProduceState, dtProductState, "KEY", "VALUE");

            DataTable dtReturnState =  Util.getCodeList("CD0805", "KEY", "VALUE");
            Util.LookupEditHelper(rileReturnState, dtReturnState, "KEY", "VALUE");

            DataTable dtReturnType =  Util.getCodeList("CD0806", "KEY", "VALUE");
            Util.LookupEditHelper(rileReturnType, dtReturnType, "KEY", "VALUE");


            //DataTable dtOS = new DataTable();
            //dtOS.Columns.Add(new DataColumn("KEY", typeof(string)));
            //dtOS.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //Util.insertRowonTop(dtOS, "1", "WIN 7");
            //Util.insertRowonTop(dtOS, "2", "WIN 8");
            //Util.insertRowonTop(dtOS, "3", "WIN 10");

            //Util.LookupEditHelper(rileOs, dtOS, "KEY", "VALUE");


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

            DataTable dtAdjustmentType = new DataTable();

            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            dtAdjustmentType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtAdjustmentType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtAdjustmentType, 1, "출고");
            Util.insertRowonTop(dtAdjustmentType, 2, "교환");
            Util.insertRowonTop(dtAdjustmentType, 3, "반품");
            Util.insertRowonTop(dtAdjustmentType, 4, "정산");

            Util.LookupEditHelper(rileType, dtAdjustmentType, "KEY", "VALUE");

            


            

            //DataTable dtConsignedType = new DataTable();

            //dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtConsignedType, 1, "생산대행");
            //Util.insertRowonTop(dtConsignedType, 2, "자사재고");
            //Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");

            //DataTable dtState = Util.getCodeList("CD0902", "KEY", "VALUE");         


            //DataRow dr1 = _dtAdjustment.NewRow();
            //dr1["TYPE"] = 1;
            //dr1["LICENCE"] = "OLD";
            //dr1["OS"] = "";
            //dr1["MANUFACTURE_NM"] = "";
            //dr1["MODEL_NM"] = "";
            //dr1["SERIAL_NO"] = "";
            //dr1["CPU"] = "";
            //dr1["COA"] = "";
            //dr1["PRODUCT_NM"] = "";
            //_dtAdjustment.Rows.Add(dr1);

            //DataRow dr2 = _dtAdjustment.NewRow();
            //dr2["TYPE"] = 2;
            //dr2["LICENCE"] = "NEW";
            //dr2["OS"] = "";
            //dr2["MANUFACTURE_NM"] = "";
            //dr2["MODEL_NM"] = "";
            //dr2["SERIAL_NO"] = "";
            //dr2["CPU"] = "";
            //dr2["COA"] = "";
            //dr2["PRODUCT_NM"] = "";
            //_dtAdjustment.Rows.Add(dr2);



        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;
            _bsPartList.DataSource = _dtPartList;
            _bsAdjustment.DataSource = _dtAdjustment;
            _bsPart.DataSource = _dtPart;
            //_bsAdjustMentProcess.DataSource = _dtAdjustMentProcess;
            //_bsAdjustmentHistory.DataSource = _dtAdjustmentHistory;

            _bsAdjustment.Sort = "TYPE";

            leCompany1.EditValue = "-1";
        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsReceipt;

            gcAdjustment.DataSource = null;
            gcAdjustment.DataSource = _bsAdjustment;

            gcPartList.DataSource = null;
            gcPartList.DataSource = _bsPartList;

            gcPart.DataSource = null;
            gcPart.DataSource = _bsPart;

            //gcAdjustmentHistory.DataSource = null;
            //gcAdjustmentHistory.DataSource = _bsAdjustmentHistory;
        }


        private void setStatistics()
        {
            JObject jResult = new JObject();

            if (DBRelease.getReleaseReceiptStatistics(ref jResult))
            {
                teReceiptCnt.Text = $"{jResult["RECEIPT_CNT"]}";
                teProcessCnt.Text = $"{jResult["PROCESS_CNT"]}";
                teHoldCnt.Text = $"{jResult["HOLD_CNT"]}";
                teProductCnt.Text = $"{jResult["PRODUCT_CNT"]}";
                tePackageCnt.Text = $"{jResult["PACKAGE_CNT"]}";
            }
        }

        private void setReadonly()
        {

            if (_releaseState < 4)
            {
                //lcgCoupon.CustomHeaderButtons[0].Properties.Enabled = true;
                lcgDelivery.CustomHeaderButtons[0].Properties.Enabled = true;
                lgcAdjustment.CustomHeaderButtons[0].Properties.Enabled = true;
                lcgPartList.CustomHeaderButtons[0].Properties.Enabled = true;

                leDelivery.ReadOnly = false;
                teDelivery.ReadOnly = false;

                gvAdjustment.OptionsBehavior.ReadOnly = false;
            }
            else
            {
                lcgDelivery.CustomHeaderButtons[0].Properties.Enabled = false;
                lgcAdjustment.CustomHeaderButtons[0].Properties.Enabled = false;
                lcgPartList.CustomHeaderButtons[0].Properties.Enabled = false;

                leDelivery.ReadOnly = true;
                teDelivery.ReadOnly = true;

                gvAdjustment.OptionsBehavior.ReadOnly = true;
            }

            //if (_releaseState < 4)
            //{
            //    gvAdjustment.OptionsBehavior.ReadOnly = false;
            //}
            //else
            //{
            //    gvAdjustment.OptionsBehavior.ReadOnly = true;
            //}

            //if (_releaseState == 1 || _releaseState == 2 || _releaseState == 3)
            //{
            //    gvAdjustment.OptionsBehavior.Editable = true;
            //    //lgcComponentList.CustomHeaderButtons[0].Properties.Enabled = true;
            //    //gridColumn21.OptionsColumn.AllowEdit = true;
            //}
            //else
            //{
            //    gvAdjustment.OptionsBehavior.Editable = false;
            //    //lgcComponentList.CustomHeaderButtons[0].Properties.Enabled = false;
            //    //gridColumn21.OptionsColumn.AllowEdit = false;
            //}
            
        }

        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);
            _dtPartList.Clear();
            _dtPart.Clear();
            _dtAdjustment.Clear();

            _partCnt = 0;
            _partPrice = 0;
            _partReleasePrice = 0;
            _partProduceCost = 0;

            if (isValidRow)
            {
                if(!initialize)
                    Dangol.ShowSplash();

                _currentReceipt = e.Row as DataRowView;

                _releaseState = ConvertUtil.ToInt32(_currentReceipt["RELEASE_STATE"]);
                _exportId = ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]);
                _export = ConvertUtil.ToString(_currentReceipt["EXPORT"]);

                gvReceipt.BeginDataUpdate();
                if (_releaseState == 90)
                    gvReceipt.Appearance.FocusedRow.ForeColor = Color.Purple;
                else if (_releaseState == 91)
                    gvReceipt.Appearance.FocusedRow.ForeColor = Color.Red;
                else
                    gvReceipt.Appearance.FocusedRow.ForeColor = Color.Black;
                gvReceipt.EndDataUpdate();

                lcgReceiptDetail.BeginInit();
                lcgLicence.BeginInit();
                _currentReceipt.BeginEdit();

                getPartList();
                getAdjustmentInfo();

                setReadonly();

                lcgReceipt.BeginUpdate();
                if (ConvertUtil.ToInt32(_currentReceipt["RETURN_YN"]) == 1)
                {
                    rgProcess.ReadOnly = true;
                    rgReturn.ReadOnly = true;
                }
                else
                {
                    rgProcess.ReadOnly = false;
                    rgReturn.ReadOnly = true;

                    if (_releaseState == 4)
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
                

                _currentReceipt.EndEdit();
                lcgLicence.EndInit();

                lcgReceiptDetail.EndUpdate();

                if (!initialize)
                    Dangol.CloseSplash();
            }
            else
            {
                _currentReceipt = null;
                lcgReceiptDetail.BeginUpdate();
                //lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lcBGrade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgReceiptDetail.EndUpdate();
                _exportId = -1;
                _export = "";
                _reUse = false;

            }
        }


        private void gvAdjustment_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvAdjustment.RowCount > 0);

            if (isValidRow)
            {
                _currentAdjustment = e.Row as DataRowView;
            }
            else
            {
                _currentAdjustment = null;
            }
        }

        private void gvPartList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPartList.RowCount > 0);

            _dtPart.Clear();
            //_bsPartDetail.
            //_bsPartDetail.DataSource = null;

            if (isValidRow)
            {
                _currentPart = e.Row as DataRowView;
                _representativeId = ConvertUtil.ToInt64(_currentPart["REPRESENTATIVE_ID"]);
                _isProduct = ConvertUtil.ToInt32(_currentPart["PRODUCT_YN"]);
                if (_isProduct == 1)
                {
                    _representativeCol = "NTB_LIST_ID";
                    gcPartState.Visible = false;
                    gcProduceState.Visible = true;
                }
                else
                {
                    _representativeCol = "COMPONENT_ID";
                    gcPartState.Visible = true;
                    gcProduceState.Visible = false;
                }

                getPart();

            }
            else
            {

            }
        }

        private void getPart()
        {
            _dtPart.BeginInit();

            JObject jResult = new JObject();
            JObject jojb = new JObject();
            jojb.Add("EXPORT_ID", ConvertUtil.ToInt64(_exportId));
            jojb.Add(_representativeCol,_representativeId);
            jojb.Add("PRODUCT_YN", _isProduct);

            if (DBRelease.getPart(jojb, ref jResult))
            {
                int index = 1;

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtPart.NewRow();
                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PART_PRICE"] = ConvertUtil.ToInt64(obj["PART_PRICE"]);
                        dr["RELEASE_PRICE"] = ConvertUtil.ToInt64(obj["RELEASE_PRICE"]);
                        dr["PRODUCE_COST"] = ConvertUtil.ToInt64(obj["PRODUCE_COST"]);
                        
                        dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["PRODUCT_STATE"] = obj["PRODUCT_STATE"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["PRODUCT_YN"] = 1;
                        //dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["ETC"] = obj["ETC"];
                        dr["LOCK_YN"] = obj["LOCK_YN"];
                        dr["NEW_COA"] = ConvertUtil.ToString(obj["NEW_COA"]);
                        dr["OLD_COA"] = ConvertUtil.ToString(obj["OLD_COA"]);
                        dr["SERIAL_NO"] = ConvertUtil.ToString(obj["SERIAL_NO"]);
                        //dr["CHECK"] = false;
                        //if (ConvertUtil.ToString(obj["LOCK_YN"]).Equals("Y"))
                        //    dr["ETC"] = "사용중";
                        _dtPart.Rows.Add(dr);
                    }
                }
            }

            _dtPart.EndInit();
        }


        private void getPartList()
        {
            _dtPartList.BeginInit();

            JObject jResult = new JObject();
            JObject jojb = new JObject();
            jojb.Add("EXPORT_ID", ConvertUtil.ToInt64(_exportId));

            if (DBRelease.getPartList(jojb, ref jResult))
            {
                int index = 1;

                if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtPartList.NewRow();
                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["COMPONENT"] = obj["COMPONENT"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                        dr["PART_CNT"] = obj["PART_CNT"];
                        dr["TOTAL_PRICE"] = ConvertUtil.ToInt64(obj["TOTAL_PRICE"]);
                        dr["TOTAL_RELEASE_PRICE"] = ConvertUtil.ToInt64(obj["TOTAL_RELEASE_PRICE"]);
                        dr["TOTAL_PRODUCE_COST"] = ConvertUtil.ToInt64(obj["TOTAL_PRODUCE_COST"]);
                        dr["ETC"] = obj["ETC"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PRODUCT_YN"] = 1;
                        dr["REPRESENTATIVE_ID"] = obj["NTB_LIST_ID"];
                        _dtPartList.Rows.Add(dr);

                        _partCnt += ConvertUtil.ToInt32(obj["PART_CNT"]);
                        _partPrice += ConvertUtil.ToInt64(obj["TOTAL_PRICE"]);
                        _partReleasePrice += ConvertUtil.ToInt64(obj["TOTAL_RELEASE_PRICE"]);
                        _partProduceCost += ConvertUtil.ToInt64(obj["TOTAL_PRODUCE_COST"]);
                    }
                }

                if (Convert.ToBoolean(jResult["PART_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtPartList.NewRow();
                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["COMPONENT"] = obj["COMPONENT"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                        dr["PART_CNT"] = obj["PART_CNT"];
                        dr["TOTAL_PRICE"] = ConvertUtil.ToInt64(obj["TOTAL_PRICE"]);
                        dr["TOTAL_RELEASE_PRICE"] = ConvertUtil.ToInt64(obj["TOTAL_RELEASE_PRICE"]);
                        dr["TOTAL_PRODUCE_COST"] = ConvertUtil.ToInt64(obj["TOTAL_PRODUCE_COST"]);
                        dr["ETC"] = obj["ETC"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PRODUCT_YN"] = 0;
                        dr["REPRESENTATIVE_ID"] = obj["COMPONENT_ID"];
                        _dtPartList.Rows.Add(dr);

                        _partCnt += ConvertUtil.ToInt32(obj["PART_CNT"]);
                        _partPrice += ConvertUtil.ToInt64(obj["TOTAL_PRICE"]);
                        _partReleasePrice += ConvertUtil.ToInt64(obj["TOTAL_RELEASE_PRICE"]);
                        _partProduceCost += ConvertUtil.ToInt64(obj["TOTAL_PRODUCE_COST"]);
                    }
                }
            }

            _dtPartList.EndInit();
        }

        private void getAdjustmentInfo()
        {
            JObject jResult = new JObject();
            JObject jojb = new JObject();
            int type = 1;

            jojb.Add("EXPORT_ID", ConvertUtil.ToInt64(_exportId));
            jojb.Add("ADJUSTMENT_TYPE", type);

            gvAdjustment.BeginDataUpdate();

            if (DBRelease.getAdjustmentInfo(jojb, ref jResult))
            {
                List<int> listType = new List<int>();

                if (Convert.ToBoolean(jResult["ADJUSTMENT_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["ADJUSTMENT_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        listType.Add(ConvertUtil.ToInt32(obj["ADJUSTMENT_TYPE"]));

                        DataRow dr = _dtAdjustment.NewRow();
                        dr["TYPE"] = obj["ADJUSTMENT_TYPE"];
                        dr["DELIVERY_COST"] = obj["DELIVERY_COST"];

                        if (_releaseState < 4 && ConvertUtil.ToInt32(obj["ADJUSTMENT_TYPE"]) == 1)
                        //if (_releaseState < 4)
                        {
                            dr["PART_CNT"] = _partCnt;
                            dr["PART_PRICE"] = _partPrice;
                            dr["PART_RELEASE_PRICE"] = _partReleasePrice;
                            dr["PRODUCE_COST"] = _partProduceCost;
                            dr["SUM_COST"] = ConvertUtil.ToInt64(dr["PART_RELEASE_PRICE"]) + ConvertUtil.ToInt64(dr["PRODUCE_COST"]) + ConvertUtil.ToInt64(dr["DELIVERY_COST"]);
                            dr["MARGIN"] = ConvertUtil.ToInt64(dr["SUM_COST"]) - ConvertUtil.ToInt64(dr["PART_PRICE"]);
                            if (ConvertUtil.ToInt64(dr["PART_PRICE"]) == 0)
                                dr["MARGIN_RATIO"] = 0;
                            else
                                dr["MARGIN_RATIO"] = (ConvertUtil.ToDouble(dr["MARGIN"]) / ConvertUtil.ToDouble(dr["PART_PRICE"])) * 100.0;
                        }
                        else
                        {
                            dr["PART_CNT"] = obj["PART_CNT"];
                            dr["PART_PRICE"] = obj["PART_PRICE"];
                            dr["PART_RELEASE_PRICE"] = obj["PART_RELEASE_PRICE"];
                            dr["PRODUCE_COST"] = obj["PRODUCE_COST"];
                            dr["MARGIN"] = obj["MARGIN"];
                            dr["MARGIN_RATIO"] = obj["MARGIN_RATIO"];
                        }
                                              
                        dr["ADJUSTMENT_ID"] = obj["ADJUSTMENT_ID"];
                        _dtAdjustment.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = _dtAdjustment.NewRow();
                    dr["TYPE"] = type;
                    dr["PRODUCE_COST"] = _partProduceCost;
                    dr["DELIVERY_COST"] = 0;

                    dr["PART_CNT"] = _partCnt;
                    dr["PART_PRICE"] = _partPrice;
                    dr["PART_RELEASE_PRICE"] = _partReleasePrice;
                    dr["SUM_COST"] = ConvertUtil.ToInt64(dr["PART_RELEASE_PRICE"]) + ConvertUtil.ToInt64(dr["PRODUCE_COST"]) + ConvertUtil.ToInt64(dr["DELIVERY_COST"]);
                    dr["MARGIN"] = ConvertUtil.ToInt64(dr["SUM_COST"]) - ConvertUtil.ToInt64(dr["PART_PRICE"]);
                    if (ConvertUtil.ToInt64(dr["PART_PRICE"]) == 0)
                        dr["MARGIN_RATIO"] = 0;
                    else
                        dr["MARGIN_RATIO"] = (ConvertUtil.ToDouble(dr["MARGIN"]) / ConvertUtil.ToDouble(dr["PART_PRICE"])) * 100.0;
                    dr["ADJUSTMENT_ID"] = -1;
                    _dtAdjustment.Rows.Add(dr);
                }

                //for(int i = 1; i < 5; i++)
                //{
                //    if (!listType.Contains(i))
                //    {
                //        
                //    }
                //}

                gvAdjustment.EndDataUpdate();
            }
            else
            {

                //for (int i = 1; i < 5; i++)
                //{
                    DataRow dr = _dtAdjustment.NewRow();
                    dr["TYPE"] = type;
                    dr["PART_CNT"] = 0;
                    dr["PART_PRICE"] = 0;
                    dr["PART_RELEASE_PRICE"] = 0;
                    dr["PRODUCE_COST"] = 0;
                    dr["DELIVERY_COST"] = 0;
                    dr["SUM_COST"] = 0;
                    dr["MARGIN"] = 0;
                    dr["MARGIN_RATIO"] = 0;
                    dr["ADJUSTMENT_ID"] = -1;
                    _dtAdjustment.Rows.Add(dr);
                //}

                gvAdjustment.EndDataUpdate();
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
            int state = ConvertUtil.ToInt32(e.NewValue);

            if (state >= 90)
                return;

            if (Dangol.MessageYN($"현재 접수를 '{_dicProxyState[e.NewValue.ToString()]}'처리하시겠습니까?") == DialogResult.Yes)
            {
                int receiptStatus = ConvertUtil.ToInt32(e.NewValue);
                int preStatus = ConvertUtil.ToInt32(e.OldValue);

                if (receiptStatus > 1)
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

                jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]));
                jobj.Add("CHECKED", 1);
                jobj.Add("EXPORT_STATE", receiptStatus);
                jobj.Add("KEY", _receiptStateCd[receiptStatus]);
                jobj.Add("KEY_NUM", receiptStatus);


                if (DBRelease.updateReceiptStatus(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentReceipt["RELEASE_STATE"] = receiptStatus.ToString();

                    if (receiptStatus == 0)
                    {
     
                    }
                    else if (receiptStatus == 1)
                        _currentReceipt["WORKER_ID"] = ProjectInfo._userId;
                    else if (receiptStatus == 2)
                        _currentReceipt["PRODUCT_ID"] = ProjectInfo._userId;
                    else if (receiptStatus == 3)
                        _currentReceipt["PACKAGE_ID"] = ProjectInfo._userId;
                    else if (receiptStatus == 4)
                        _currentReceipt["COMPLETE_ID"] = ProjectInfo._userId;

                    if (receiptStatus == 4)
                        _currentReceipt["RELEASE_DT"] = DateTime.Today.ToString("yyyy-MM-dd");
                    else
                        _currentReceipt["RELEASE_DT"] = "";

                    _currentReceipt.EndEdit();

                    _releaseState = receiptStatus;

                    setReadonly();
                    setStatistics();

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
            string EXPORT = _export;
            setStatistics();
            gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
            getReceiptList(false);
            gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
            lcgReceiptList.CustomHeaderButtons[2].Properties.Checked = false;
            int rowHandle = gvReceipt.LocateByValue("EXPORT", EXPORT);

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

            if (DBRelease.searchReceiptList(jData, ref jResult))
            {
                gvReceipt.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceipt.NewRow();

                    dr["NO"] = index++;
                    dr["EXPORT_ID"] = obj["EXPORT_ID"];
                    dr["P_EXPORT_ID"] = obj["P_EXPORT_ID"];
                    dr["R_EXPORT_ID"] = obj["R_EXPORT_ID"];
                    dr["C_EXPORT_ID"] = obj["C_EXPORT_ID"];
                    dr["RELEASE_STATE"] = obj["RELEASE_STATE"];

                    dr["EXPORT"] = obj["EXPORT"];
                    dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                    dr["COMPANY_ID"] = ConvertUtil.ToString(obj["COMPANY_ID"]);
                    dr["TAX_INVOICE"] = obj["TAX_INVOICE"];
                    dr["DES"] = obj["DES"];
                    dr["PAYMENT"] = obj["PAYMENT"];
                    
                    dr["CUSTOMER_NM"] = obj["CUSTOMER_NM"];
                    dr["TEL"] = obj["TEL"];
                    dr["HP"] = obj["HP"];
                    dr["POSTAL_CD"] = obj["POSTAL_CD"];
                    dr["ADDRESS"] = obj["ADDRESS"];
                    dr["ADDRESS_DETAIL"] = obj["ADDRESS_DETAIL"];
                    dr["TAKE_USER_ID"] = obj["TAKE_USER_ID"];

                    dr["DELIVERY_COMPANY"] = obj["DELIVERY_COMPANY"];
                    dr["INVOICE"] = obj["INVOICE"];

                    
                    dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                    dr["RETURN_YN"] = obj["RETURN_YN"];
                    dr["WORKER_ID"] = obj["WORKER_ID"];
                    dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                    dr["PACKAGE_ID"] = obj["PACKAGE_ID"];
                    dr["COMPLETE_ID"] = obj["COMPLETE_ID"];
                    dr["CUSTOMER_ID"] = obj["CUSTOMER_ID"];
                    
                    dr["CHECK"] = false;
                    
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

                jData.Add("RELEASE_RANGE", $"'{ string.Join("','", _listReleaseRange)}'");
                jData.Add("RECEIPT_DT_S", dtFrom);
                jData.Add("RECEIPT_DT_E", dtTo);
            }
            else
            {
                date = 0;
            }


            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teReceip1.Text)))
            {
                jData.Add("EXPORT", teReceip1.Text);
                date++;
            }

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCustomerNm1.Text)))
            {
                jData.Add("CUSTOMER_NM", teCustomerNm1.Text);
                date++;
            }

            if (ConvertUtil.ToInt32(leReleaseState.EditValue) >= 0)
            {
                jData.Add("RELEASE_STATE", ConvertUtil.ToInt32(leReleaseState.EditValue));
            }

            if (!ConvertUtil.ToString(leCompany1.EditValue).Equals("-1"))
            {
                jData.Add("COMPANY_ID", ConvertUtil.ToString(leCompany1.EditValue));
                date++;
            }

            if (date == 0)
            {
                jData.Add("MSG", "접수일을 선택하지 않은 경우, 출고번호 또는 고객명은 필수로 입력돼야 합니다.");
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

                jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]));

                if (DBConsigned.ConsignedOneclickRelease(jobj, ref jResult))
                {
                    _currentReceipt["RELEASE_STATE"] = "4";
                    //_currentReceipt["RELEASE_STATE"] = receiptStatus.ToString(); 

                    int rowHandle = gvReceipt.FocusedRowHandle;
                    gvReceipt.FocusedRowHandle = -2147483646;
                    gvReceipt.FocusedRowHandle = rowHandle;

                    setStatistics();
                    //setPartReadonly();

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

            jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]));
            //jobj.Add("COUPON_MANAGE", ConvertUtil.ToString(teManageNo.Text.Trim()));
            //jobj.Add("COUPON_CUSTOMER", ConvertUtil.ToString(teCustomerNo.Text.Trim()));

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
            //string manageNo = teManageNo.Text.Trim();

            //if (manageNo.Length != 0 && manageNo.Length != 12)
            //{
            //    Dangol.Warining("관리번호를 확인하세요(자리수 다름).");
            //    return;
            //}


            if (Dangol.MessageYN("쿠폰정보를 저장하시겠습니까?") != DialogResult.Yes)
            {
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                //string customerNo = teCustomerNo.Text.Trim();

                //if (customerNo.Length != 0 && customerNo.Length != 12)
                //{
                //    Dangol.Message("고객번호를 확인하세요(자리수 다름).");
                //    return;
                //}

                //if (saveCouponInfo())
                //    Dangol.Message("처리되었습니다.");
                //else
                //    return;
            }

        }

        private bool saveAdjustmentInfo()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            foreach(DataRow row in _dtAdjustment.Rows)
            {
                JObject jData = new JObject();

                jData.Add("ADJUSTMENT_ID", ConvertUtil.ToInt64(row["ADJUSTMENT_ID"]));
                jData.Add("ADJUSTMENT_TYPE", ConvertUtil.ToInt32(row["TYPE"]));
                jData.Add("PART_CNT", ConvertUtil.ToInt32(row["PART_CNT"]));
                jData.Add("PART_PRICE", ConvertUtil.ToInt64(row["PART_PRICE"]));

                jData.Add("PART_RELEASE_PRICE", ConvertUtil.ToInt64(row["PART_RELEASE_PRICE"]));
                jData.Add("PRODUCE_COST", ConvertUtil.ToInt64(row["PRODUCE_COST"]));
                jData.Add("DELIVERY_COST", ConvertUtil.ToInt64(row["DELIVERY_COST"]));
                jData.Add("SUM_COST", ConvertUtil.ToInt64(row["SUM_COST"]));
                jData.Add("MARGIN", ConvertUtil.ToInt64(row["MARGIN"]));
                jData.Add("MARGIN_RATIO", ConvertUtil.ToDouble(row["MARGIN_RATIO"]));

                jArray.Add(jData);
            }

            jobj.Add("DATA", jArray);
            jobj.Add("EXPORT_ID", _exportId);

            if (DBRelease.saveAdjustmentInfo(jobj, ref jResult))
            {
                getAdjustmentInfo();
                return true;
            }
                
            else
            {
                Dangol.Message(jResult["MSG"]);
                return false;
            }
        }

        private void lgcLicence_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("정산 정보를 저장하시겠습니까?") != DialogResult.Yes)
            {
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                gvAdjustment.FocusedRowHandle = -2147483646;
                gvAdjustment.FocusedRowHandle = 0;
                if (saveAdjustmentInfo())
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
            if(_currentAdjustment != null)
                resetAdjustment(ConvertUtil.ToInt32(_currentAdjustment["TYPE"]));
        }

        private void resetAdjustment(int type)
        {
            DataRow[] rows = _dtAdjustment.Select($"TYPE = {type}");

            if(rows.Length > 0)
            {
                gvAdjustment.BeginDataUpdate();
                DataRow row = rows[0];

                if (type == 1)
                {
                    row["PART_CNT"] = _partCnt;
                    row["PART_PRICE"] = _partPrice;
                    row["PART_RELEASE_PRICE"] = _partReleasePrice;
                    row["SUM_COST"] = ConvertUtil.ToInt64(row["PART_RELEASE_PRICE"]) + ConvertUtil.ToInt64(row["PRODUCE_COST"]) + ConvertUtil.ToInt64(row["DELIVERY_COST"]);
                    row["MARGIN"] = ConvertUtil.ToInt64(row["SUM_COST"]) - ConvertUtil.ToInt64(row["PART_PRICE"]);
                    if (ConvertUtil.ToInt64(row["PART_PRICE"]) == 0)
                        row["MARGIN_RATIO"] = 0;
                    else
                        row["MARGIN_RATIO"] = (ConvertUtil.ToDouble(row["MARGIN"]) / ConvertUtil.ToDouble(row["PART_PRICE"])) * 100.0;
                }
                else
                {
                    row["SUM_COST"] = ConvertUtil.ToInt64(row["PART_RELEASE_PRICE"]) + ConvertUtil.ToInt64(row["PRODUCE_COST"]) + ConvertUtil.ToInt64(row["DELIVERY_COST"]);
                    row["MARGIN"] = ConvertUtil.ToInt64(row["SUM_COST"]) - ConvertUtil.ToInt64(row["PART_PRICE"]);
                    if (ConvertUtil.ToInt64(row["PART_PRICE"]) == 0)
                        row["MARGIN_RATIO"] = 0;
                    else
                        row["MARGIN_RATIO"] = (ConvertUtil.ToDouble(row["MARGIN"]) / ConvertUtil.ToDouble(row["PART_PRICE"])) * 100.0;
                }
                gvAdjustment.EndDataUpdate();
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

            if (_releaseState == 0 || _releaseState > 3 || !ConvertUtil.ToBoolean(_currentPart["ASSIGN_YN"]))
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
                //_dtAdjustMentProcess.BeginInit();

                //long price = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["PRICE"]);
                //long totalPrice = ConvertUtil.ToInt64(_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"]);

                //_dtAdjustMentProcess.Rows[0]["PRICE"] = price - _partPrice + partPrice;
                //_dtAdjustMentProcess.Rows[0]["TOTAL_PRICE"] = totalPrice - _partPrice + partPrice;
                //_currentReceipt["PART_PRICE"] = _dtAdjustMentProcess.Rows[0]["PRICE"];

                //_partPrice = partPrice;

                //_dtAdjustMentProcess.EndInit();
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

            jobj.Add("EXPORT_ID", $"{ _currentReceipt["EXPORT_ID"]}");
            jobj.Add("PAYMENT", $"{ _currentReceipt["PAYMENT"]}");
            jobj.Add("TAX_INVOICE", $"{ _currentReceipt["TAX_INVOICE"]}");
            jobj.Add("RELEASE_TYPE", $"{ _currentReceipt["RELEASE_TYPE"]}");
            jobj.Add("DES", $"{ _currentReceipt["DES"]}");
            jobj.Add("COMPANY_ID", $"{ _currentReceipt["COMPANY_ID"]}");
            jobj.Add("CUSTOMER_ID", $"{ _currentReceipt["CUSTOMER_ID"]}");
            jobj.Add("CUSTOMER_NM", $"{teCustomerNm.Text}");
            jobj.Add("TEL", $"{teTel.Text }");
            jobj.Add("MOBILE", $"{ teHp.Text}");
            jobj.Add("POSTAL_CD", $"{tePostalCd.Text}");
            jobj.Add("ADDRESS", $"{ teAddress.Text}");
            jobj.Add("ADDRESS_DETAIL", $"{ teAddressDetail.Text}");
            jobj.Add("CUSTOMER", 1);


            if (DBRelease.updateReceiptInfo(jobj, ref jResult))
            {
                gvReceipt.BeginDataUpdate();
                gvReceipt.EndDataUpdate();
                //if (ConvertUtil.ToInt32(_currentReceipt["RELEASE_TYPE"]) != _releaseType)
                //{
                //    getAdjustmentInfo();
                //}

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

                jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]));
                jobj.Add("CHECKED", 1);
                jobj.Add("EXPORT_STATE", receiptStatus);
                jobj.Add("KEY", "CANCEL_DT");
                jobj.Add("KEY_NUM", receiptStatus);

                jobj.Add("RELEASE_CHARGE", 0);

                if (DBRelease.updateReceiptStatus(jobj, ref jResult))
                {
                    Dangol.ShowSplash();

                    _currentReceipt.BeginEdit();
                    _currentReceipt["RELEASE_STATE"] = receiptStatus.ToString();
                    _currentReceipt["RELEASE_DT"] = "";
                    _currentReceipt.EndEdit();

                    _releaseState = receiptStatus;

                    //rgProcess.EditValue = "91";

                    setReadonly();
                    setStatistics();
                    //setPartReadonly();

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

                jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]));
                jobj.Add("CHECKED", 1);
                jobj.Add("EXPORT_STATE", receiptStatus);
                jobj.Add("KEY", "POSTPONE_DT");
                jobj.Add("KEY_NUM", receiptStatus);
                jobj.Add("RELEASE_CHARGE", 0);


                if (DBRelease.updateReceiptStatus(jobj, ref jResult))
                {
                    _currentReceipt.BeginEdit();
                    _currentReceipt["RELEASE_STATE"] = receiptStatus.ToString();
                    _currentReceipt["RELEASE_DT"] = "";
                    _currentReceipt.EndEdit();

                    _releaseState = receiptStatus;

                    rgProcess.EditValue = "90";

                    setReadonly();
                    setStatistics();

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
            //if (Dangol.MessageYN($"현재 접수를 삭제하시겠습니까?") == DialogResult.Yes)
            //{
            //    int receiptStatus = 91;

            //    JObject jResult = new JObject();
            //    JObject jobj = new JObject();

            //    jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]));
            //    jobj.Add("CHECKED", 1);
            //    jobj.Add("EXPORT_STATE", receiptStatus);
            //    jobj.Add("KEY", "CANCEL_DT");
            //    jobj.Add("KEY_NUM", receiptStatus);

            //    jobj.Add("RELEASE_CHARGE", 0);

            //    if (DBRelease.updateReceiptStatus(jobj, ref jResult))
            //    {    
            //        jobj.RemoveAll();
            //        jobj.Add("EXPORT_ID", _exportId);
            //        jobj.Add("RELEASE_STATE", -1);

            //        DBConsigned.updateReceiptSimple(jobj, ref jResult);

            //        gvReceipt.BeginDataUpdate();
            //        _currentReceipt.Delete();
            //        gvReceipt.EndDataUpdate();

            //        Dangol.Message("처리되었습니다.");
            //    }
            //    else
            //    {
            //        Dangol.Message(jResult["MSG"]);
            //        return;
            //    }
            //}
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
                if (ConvertUtil.ToInt32(gvPartList.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void lgcReceiptPart_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPartList.FocusedRowHandle;
            int topRowIndex = gvPartList.TopRowIndex;
            gvPartList.FocusedRowHandle = -1;
            gvPartList.FocusedRowHandle = rowhandle;

            try
            {
                gvPartList.BeginUpdate();
                foreach (DataRow row in _dtPart.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvPartList.DataRowCount; i++)
                {
                    int rowHandle = gvPartList.GetVisibleRowHandle(i);
                    rows.Add(gvPartList.GetDataRow(rowHandle));
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
                gvPartList.EndUpdate();
            }
        }

        private void lgcReceiptPart_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPartList.FocusedRowHandle;
            int topRowIndex = gvPartList.TopRowIndex;
            gvPartList.FocusedRowHandle = -1;
            gvPartList.FocusedRowHandle = rowhandle;

            gvPartList.BeginDataUpdate();

            foreach (DataRow row in _dtPart.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvPartList.EndDataUpdate();
        }

        private void lgcReceiptPart_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentReceipt == null)
            {
                Dangol.Message("선택된 항목이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(0))
            {
                int rowhandle = gvPartList.FocusedRowHandle;
                gvPartList.FocusedRowHandle = -1;
                gvPartList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtPart.Select("CHECK = TRUE");
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
                        if (!ConvertUtil.ToBoolean(row["ASSIGN_YN"]))
                        {
                            componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                            jobj.RemoveAll();
                            jobj.Add("EXPORT_ID", _exportId);
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

                    //getReceiptPart();
                    Dangol.CloseSplash();

                    //gvPart.EndDataUpdate();

                    //if (_releaseState < 4)
                    //    _dtAdjustMentProcess.Rows[0]["PRICE"] = _currentReceipt["PART_PRICE"];

                    Dangol.Message("처리되었습니다.");
                }
            }
            else if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvPartList.FocusedRowHandle;
                gvPartList.FocusedRowHandle = -1;
                gvPartList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtPart.Select("CHECK = TRUE");
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
                        gvPartList.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["CONSIGNED_TYPE"] = 2;
                        }

                        gvPartList.EndDataUpdate();
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
                int rowhandle = gvPartList.FocusedRowHandle;
                gvPartList.FocusedRowHandle = -1;
                gvPartList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtPart.Select("CHECK = TRUE");
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
                        gvPartList.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["CONSIGNED_TYPE"] = 1;
                        }

                        gvPartList.EndDataUpdate();
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
                //using (DlgConsignedPartModif consignedPartModif = new DlgConsignedPartModif(_exportId, _export, ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"])))
                //{
                //    consignedPartModif.StartPosition = FormStartPosition.Manual;
                //    consignedPartModif.Location = new Point(this.Location.X + (this.Size.Width / 2) - (consignedPartModif.Size.Width / 2),
                //    this.Location.Y + (this.Size.Height / 2) - (consignedPartModif.Size.Height / 2));

                //    if (consignedPartModif.ShowDialog(this) == DialogResult.OK)
                //    {
                //        Dangol.ShowSplash();
                //        getReceiptPart();
                //        Dangol.CloseSplash();

                //    }
                //}

            }
        }

        private void lcgDelivery_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentReceipt == null)
            {
                Dangol.Message("선택된 접수가 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("운송장 정보를 수정하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("EXPORT_ID", _exportId);
                    jobj.Add("DELIVERY_COMPANY", ConvertUtil.ToString(leDelivery.EditValue));
                    jobj.Add("INVOICE", teDelivery.Text);

                    if (DBRelease.updateReceiptInfo(jobj, ref jResult))
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
            if (Dangol.MessageYN("반품/교환 신청하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();

                JObject jobj = new JObject();
                JObject jResult = new JObject();

                jobj.Add("EXPORT_ID", _exportId);
                jobj.Add("AS_YN", 0);

                if (DBConsigned.returnReceipt(jobj, ref jResult))
                {
                    _currentReceipt["RETURN_YN"] = 1;
                    _currentReceipt["C_PROXY_ID"] = ConvertUtil.ToInt64(jResult["EXPORT_ID"]);

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
            if (Dangol.MessageYN("부품출고를 신청하시겠습니까?") == DialogResult.Yes)
            {
                //using (DlgReceiptPartList dlgImportBarcode = new DlgReceiptPartList(_exportId, _dtPart))
                //{
                //    dlgImportBarcode.ShowDialog();

                //    if (dlgImportBarcode.DialogResult == DialogResult.OK)
                //    {
                //        Dangol.ShowSplash();
                //        getReceiptList(false);
                //        lcgReceiptList.CustomHeaderButtons[2].Properties.Checked = false;
                //        Dangol.CloseSplash();
                //        gvReceipt.MoveFirst();
                //        Dangol.Message("처리되었습니다.");

                //    }
                //}


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

            DataRow[] rows = _dtReceipt.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
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

                    foreach (DataRow row in rows)
                    {
                        proxyState = ConvertUtil.ToInt32(row["RELEASE_STATE"]);

                        if (proxyState == 3)
                        {
                            proxyId = ConvertUtil.ToInt64(row["EXPORT_ID"]);
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
                            proxyState = ConvertUtil.ToInt32(row["RELEASE_STATE"]);

                            if (proxyState == 3)
                            {
                                proxyId = ConvertUtil.ToInt64(row["EXPORT_ID"]);
                                if (completeProxyId.Contains(proxyId))
                                {
                                    row.BeginEdit();
                                    row["RELEASE_STATE"] = "4";
                                    row["COMPLETE_ID"] = ProjectInfo._userId;
                                    row.EndEdit();
                                }
                            }
                        }

                        if (completeProxyId.Contains(_exportId))
                            refresh();
                    }

                    Dangol.CloseSplash();
                    Dangol.Message("처리되었습니다.");
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

            gvReceipt.BeginDataUpdate();

            foreach (DataRow row in rows)
            {
                proxyId = ConvertUtil.ToInt64(row["EXPORT_ID"]);
                proxyState = ConvertUtil.ToInt32(row["RELEASE_STATE"]);

                if (proxyState == curState)
                {
                    jobj.RemoveAll();

                    jobj.Add("EXPORT_ID", proxyId);
                    jobj.Add("CHECKED", 1);
                    jobj.Add("EXPORT_STATE", nextState);
                    jobj.Add("KEY", _receiptStateCd[nextState]);
                    jobj.Add("KEY_NUM", nextState);
                    jobj.Add("RELEASE_CHARGE", 0);
                    //jobj.Add("REUSE_YN", 0);

                    if (DBRelease.updateReceiptStatus(jobj, ref jResult))
                    {
                        row.BeginEdit();
                        row["RELEASE_STATE"] = nextState.ToString();
                        row["PRODUCT_ID"] = ProjectInfo._userId;

                        //_currentReceipt["PACKAGE_ID"] = ProjectInfo._userId;

                        row.EndEdit();

                        completeProxyId.Add(proxyId);
                    }
                }
            }

            gvReceipt.EndDataUpdate();

            if (completeProxyId.Contains(_exportId))
                refresh();
        }

        private void gvReceipt_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (e.Column.FieldName != "EXPORT" || e.Column.FieldName != "CUSTOMER_NM1")
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
                int state = ConvertUtil.ToInt32(gvReceipt.GetDataRow(e.RowHandle)["RELEASE_STATE"]);
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

            if (e.Button.Properties.Tag.Equals(0)) //X
            {
                setDelete();
            }
            else if (e.Button.Properties.Tag.Equals(1))
            {
                setReturnRequest();
            }
            else if (e.Button.Properties.Tag.Equals(2)) //X
            {
                setPartRelease();
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
            else if (e.Button.Properties.Tag.Equals(7))
            {
                showReceiptPrint();
            }

        }

        private void showReceiptPrint()
        {
            _dtReceiptInvoice.Clear();

            string componentCd;
            long totalReleasePrice = 0;
            foreach(DataRow row in _dtPartList.Rows)
            {
                componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);

                if(_dicConsignedComponentCdReverse.ContainsKey(componentCd))
                {
                    int index = _dicConsignedComponentCdReverse[componentCd];
                    componentCd = _consignedComponetNm[index];
                }

                DataRow dr = _dtReceiptInvoice.NewRow();

                dr["COMPONENT_CD"] = componentCd;
                dr["MODEL_NM"] = row["MODEL_NM"];
                dr["SPEC_NM"] = "";
                dr["EA"] = "원";
                dr["PART_CNT"] = row["PART_CNT"];
                dr["RELEASE_PRICE"] = ConvertUtil.ToInt64(row["TOTAL_RELEASE_PRICE"]) / ConvertUtil.ToInt32(row["PART_CNT"]);
                dr["TOTAL_RELEASE_PRICE"] = row["TOTAL_RELEASE_PRICE"];

                _dtReceiptInvoice.Rows.Add(dr);

                totalReleasePrice += ConvertUtil.ToInt64(row["TOTAL_RELEASE_PRICE"]);
            }

            XtraReport report = new XtraReport();
            rpExportReport rpER = new rpExportReport(_currentReceipt, totalReleasePrice);
            rpER.DataBinding();
            rpER.DataSource = _dtReceiptInvoice;
            report = rpER;

            using (DlgReport dlgReport = new DlgReport("거래명세서", report))
            {
                if (dlgReport.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
            
        }

        private void lcgReceiptDetail_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                receiptSave();
            }
        }

        private void lcgPartList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            using (dlgReleaseReceiptPartModif dlgReleaseReceiptPartModif = new dlgReleaseReceiptPartModif(_exportId))
            {
                if (dlgReleaseReceiptPartModif.ShowDialog(this) == DialogResult.OK)
                {
                    refresh();
                }
            }
        }

        private void leCompany_EditValueChanged(object sender, EventArgs e)
        {
          
            if (_currentReceipt != null)
            {
                long companyId = ConvertUtil.ToInt64(leCompany.EditValue);
                gvReceipt.BeginDataUpdate();
                //teCustomerNm.EditValueChanged -= teCustomerNm_EditValueChanged;
                if (companyId == -1)
                {
                    teCustomerNm.ReadOnly = false;
                }
                else
                {
                    teCustomerNm.Text = leCompany.Text;
                    teCustomerNm.ReadOnly = true;
                }
                //teCustomerNm.EditValueChanged += teCustomerNm_EditValueChanged;
                gvReceipt.EndDataUpdate();

            }

        }

        private void rgReturn_EditValueChanging(object sender, ChangingEventArgs e)
        {

        }
    }
}