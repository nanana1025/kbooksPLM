using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.consigned;
using WareHousingMaster.view.usedPurchase;

namespace WareHousingMaster.view.release
{
    public partial class usrReleaseReceipt : DevExpress.XtraEditors.XtraForm
    {

        string _componentCd;

        DataRowView _currentReceipt;
        DataRowView _currentReceiptPart;
        DataRowView _currentComponentPart;
        DataRowView _currentBarcodeRow;
        

        DataTable _dtReceipt;
        DataTable _dtPart;
        DataTable _dtPartDetail;
        DataTable _dtBarcodeList;
        Dictionary<long, DataTable> _dicPartDetail;


        BindingSource _bsExport;
        BindingSource _bsPart;
        BindingSource _bsPartDetail;
        BindingSource _bsBarcodeList;

        Dictionary<long, DataTable> _dicReceiptPart;
        Dictionary<long, Dictionary<long, DataTable>> _dicReceiptPartDetail;
        Dictionary<long, List<long>> _dicReceiptInventoryId;
        Dictionary<long, List<long>> _dicReceiptProductId;

        string[] _exportComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "STG", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG", "TBL", "NTB", "DKT", "AIO" };
        string[] _exportComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "저장장치", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스", "태블릿", "노트북", "데스크탑", "올인원" };


        Dictionary<int, string> _dicExportComponentCd;
        Dictionary<string, int> _dicExportComponentCdReverse;

        GridColumn[] _columnsPart;
        GridColumn[] _columnsProduct;

        string _currentGetComponentCd;

        Dictionary<string, string> _dicProductType;
        Dictionary<string, string> _dicGuarantee;

        long _proxyId;
        long _id;
        long _CurrentId;
        long _inventoryId;

        int _currnetPartCnt=0;

        bool _isNew;

        bool _isProductCheck;

        string _currentBarcode;

        List<string> _listBarcode;

        int _barcodeFocusedFowHandl;


        Regex regex1;
        Regex regex2;
        Regex regex3;

        Regex oldCoa;
        Regex newCoa;



        public usrReleaseReceipt()
        {
            InitializeComponent();

            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("EXPORT_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("EXPORT", typeof(string)));
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
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT", typeof(bool)));
            _dtReceipt.Columns.Add(new DataColumn("ERROR", typeof(string)));

            _dtPart = new DataTable();
            _dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
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

            _dtPart.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("PRODUCE_COST", typeof(long)));

            _dtPart.Columns.Add(new DataColumn("STATE", typeof(int)));

            _dtPartDetail = new DataTable();
            _dtPartDetail.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtPartDetail.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtPartDetail.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtPartDetail.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtPartDetail.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtPartDetail.Columns.Add(new DataColumn("LOCK_YN", typeof(string)));
            _dtPartDetail.Columns.Add(new DataColumn("ETC", typeof(string)));

            _dtBarcodeList = new DataTable();
            _dtBarcodeList.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtBarcodeList.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("OLD_COA", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("NEW_COA", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("BOX_NO", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _bsExport = new BindingSource();
            _bsPart = new BindingSource();
            _bsPartDetail = new BindingSource();
            _bsBarcodeList = new BindingSource();

            _dicReceiptPart = new Dictionary<long, DataTable>();
            _dicReceiptPartDetail = new Dictionary<long, Dictionary<long, DataTable>>();

            _dicReceiptInventoryId = new Dictionary<long, List<long>>();
            _dicReceiptProductId = new Dictionary<long, List<long>>();

            _dicProductType = new Dictionary<string, string>();
            _dicGuarantee = new Dictionary<string, string>();

            _listBarcode = new List<string>();

            _id = 0;

            _columnsPart = new GridColumn[] {gcNumber, gcCheck, gcComponentCd, gcBarcode, gcProduct, gcInventoryState, gcModelNm, gcDes };
            _columnsProduct = new GridColumn[] { gcNumber, gcCheck,gcBarcode, gcProductState, gcModelNm, gcDes };

            _dicExportComponentCd = new Dictionary<int, string>();
            _dicExportComponentCdReverse = new Dictionary<string, int>();

            for (int i = 0; i < _exportComponetCd.Length; i++)
            {
                _dicExportComponentCd.Add(i, _exportComponetCd[i]);
                _dicExportComponentCdReverse.Add(_exportComponetCd[i], i);
            }

            _currentGetComponentCd = null;

            _isNew = false;

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

            newCoa = new Regex(@"^00[0-9]{12}$");
            oldCoa = new Regex(@"^01[0-9]{12}$");
        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            teExport.DataBindings.Add(new Binding("Text", _bsExport, "EXPORT", false, DataSourceUpdateMode.OnPropertyChanged));
            //leProductType.DataBindings.Add(new Binding("EditValue", _bsExport, "PRODUCT_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            deReceiptDt.DataBindings.Add(new Binding("Text", _bsExport, "RECEIPT_DT", false, DataSourceUpdateMode.OnPropertyChanged));
            //leGuarantee.DataBindings.Add(new Binding("EditValue", _bsExport, "GUARANTEE", false, DataSourceUpdateMode.OnPropertyChanged));
            //deGuaranteeFrom.DataBindings.Add(new Binding("Text", _bsExport, "GUARANTEE_FROM", false, DataSourceUpdateMode.OnPropertyChanged));
            //deGuaranteeTo.DataBindings.Add(new Binding("Text", _bsExport, "GUARANTEE_TO", false, DataSourceUpdateMode.OnPropertyChanged));
            leComapny.DataBindings.Add(new Binding("EditValue", _bsExport, "COMPANY_ID", false, DataSourceUpdateMode.OnPropertyChanged));
            lePayment.DataBindings.Add(new Binding("EditValue", _bsExport, "PAYMENT", false, DataSourceUpdateMode.OnPropertyChanged));
            leTaxInvoice.DataBindings.Add(new Binding("EditValue", _bsExport, "TAX_INVOICE", false, DataSourceUpdateMode.OnPropertyChanged));
            
            rgReleaseType.DataBindings.Add(new Binding("EditValue", _bsExport, "RELEASE_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            //teModelNmDetail.DataBindings.Add(new Binding("Text", _bsExport, "MODEL_NM_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));

            meDes.DataBindings.Add(new Binding("Text", _bsExport, "DES", false, DataSourceUpdateMode.OnPropertyChanged));
            //teRequest.DataBindings.Add(new Binding("Text", _bsExport, "REQUEST", false, DataSourceUpdateMode.OnPropertyChanged));
            //ceProduct.DataBindings.Add(new Binding("EditValue", _bsExport, "PRODUCT", false, DataSourceUpdateMode.OnPropertyChanged));
            leTakeUserId.DataBindings.Add(new Binding("EditValue", _bsExport, "TAKE_USER_ID", false, DataSourceUpdateMode.OnPropertyChanged));
            //teError.DataBindings.Add(new Binding("EditValue", _bsExport, "ERROR", false, DataSourceUpdateMode.OnPropertyChanged));

            teCustomerNm.DataBindings.Add(new Binding("Text", _bsExport, "CUSTOMER_NM", false, DataSourceUpdateMode.OnPropertyChanged));
            //teCustomerNm2.DataBindings.Add(new Binding("Text", _bsExport, "CUSTOMER_NM2", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel.DataBindings.Add(new Binding("Text", _bsExport, "TEL", false, DataSourceUpdateMode.OnPropertyChanged));
            //teTel2.DataBindings.Add(new Binding("Text", _bsExport, "TEL2", false, DataSourceUpdateMode.OnPropertyChanged));
            teHp.DataBindings.Add(new Binding("Text", _bsExport, "HP", false, DataSourceUpdateMode.OnPropertyChanged));
            //teHp2.DataBindings.Add(new Binding("Text", _bsExport, "HP1", false, DataSourceUpdateMode.OnPropertyChanged));
            tePostalCd.DataBindings.Add(new Binding("Text", _bsExport, "POSTAL_CD", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddress.DataBindings.Add(new Binding("Text", _bsExport, "ADDRESS", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddressDetail.DataBindings.Add(new Binding("Text", _bsExport, "ADDRESS_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));
            //ceBGrade.DataBindings.Add(new Binding("EditValue", _bsExport, "B_GRADE", false, DataSourceUpdateMode.OnPropertyChanged));

        }

        private void setInfoBox()
        {
            DataTable dtPayment = new DataTable();
            dtPayment.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtPayment.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtPayment, 2, "후");
            Util.insertRowonTop(dtPayment, 1, "선");         
            Util.insertRowonTop(dtPayment, -1, "해당없음");

            Util.LookupEditHelper(lePayment, dtPayment, "KEY", "VALUE");

            DataTable dtTaxInvoice = new DataTable();
            dtTaxInvoice.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtTaxInvoice.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtTaxInvoice, 1, "현금영수증");
            Util.insertRowonTop(dtTaxInvoice, 2, "미발행");
            Util.insertRowonTop(dtTaxInvoice, 1, "발행");
            Util.insertRowonTop(dtTaxInvoice, -1, "해당없음");

            Util.LookupEditHelper(leTaxInvoice, dtTaxInvoice, "KEY", "VALUE");
           

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "직접입력");
            Util.LookupEditHelper(leComapny, dtCompany, "KEY", "VALUE");

            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < _exportComponetCd.Length; i++)
            {
                DataRow row = dtComponentCd.NewRow();
                row["KEY"] = _exportComponetCd[i];
                row["VALUE"] = _exportComponetNm[i];
                dtComponentCd.Rows.Add(row);
            }
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd1, dtComponentCd, "KEY", "VALUE");

            DataTable dtInventoryState = dtInventoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState, dtInventoryState, "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState1, dtInventoryState, "KEY", "VALUE");

            DataTable dtProductState = dtProductState = Util.getCodeList("CD0804", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductState, dtProductState, "KEY", "VALUE");

            Util.LookupEditHelper(leTakeUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            // 기본 값 설정
            _bsExport.DataSource = _dtReceipt;
            _bsBarcodeList.DataSource = _dtBarcodeList;
            //_bsPart.DataSource = _dtPart;
            //_bsProduct.DataSource = _dtProduct;
            //_bsPartDetail.DataSource = _dtPartDetail;
        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsExport;

            gcBarcodeList.DataSource = null;
            gcBarcodeList.DataSource = _bsBarcodeList;

            //gcPart.DataSource = null;
            //gcPart.DataSource = _bsPart;

            //gcPartDetail.DataSource = null;
            //gcPartDetail.DataSource = _bsPartDetail;
        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);

            gvPart.BeginDataUpdate();
            //_bsPart.DataSource = null;
            if (isValidRow)
            {
                _currentReceipt = e.Row as DataRowView;
                _CurrentId = ConvertUtil.ToInt64(_currentReceipt["ID"]);
                _isProductCheck = ConvertUtil.ToBoolean(_currentReceipt["PRODUCT"]);

                _dtPart = _dicReceiptPart[_CurrentId];
                _dicPartDetail = _dicReceiptPartDetail[_CurrentId];

                //_bsPart.DataSource = _dtPart;
                gcPart.DataSource = _dtPart;
                //setGridPartInit(_isProductCheck);

                gvPart.MoveFirst();

                setGridPartInit();

            }
            else
            {
                _dtPart = null;
                _dicPartDetail = null;
                _currentReceipt = null;               
            }

            if(_dtPart == null || _dtPart.Rows.Count < 1)
            {
                gvPartDetail.BeginDataUpdate();
                _dtPartDetail = null;
                gcPartDetail.DataSource = _dtPartDetail;
                gvPartDetail.EndDataUpdate();
            }

            

            gvPart.EndDataUpdate();
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            gvPartDetail.BeginDataUpdate();
            //_bsPartDetail.
            //_bsPartDetail.DataSource = null;
            
            if (isValidRow)
            {
                _currentReceiptPart = e.Row as DataRowView;
                _inventoryId = ConvertUtil.ToInt64(_currentReceiptPart["INVENTORY_ID"]);

                int productYn = ConvertUtil.ToInt32(_currentReceiptPart["PRODUCT_YN"]);

                if (productYn == 1)
                {
                    gcOldCoa.OptionsColumn.ReadOnly = false;
                    gcNewCoa.OptionsColumn.ReadOnly = false;
                    gcSerial.OptionsColumn.ReadOnly = false;
                }
                else
                {
                    gcOldCoa.OptionsColumn.ReadOnly = true;
                    gcNewCoa.OptionsColumn.ReadOnly = true;
                    gcSerial.OptionsColumn.ReadOnly = true;
                }

                if (_dicPartDetail != null)
                {
                    if (_dicPartDetail.ContainsKey(_inventoryId))
                        _dtPartDetail = _dicPartDetail[_inventoryId];
                    else
                        _dtPartDetail = null;

                    //gcPartDetail.DataSource = null;
                    //gcPartDetail.DataSource = _dtPartDetail;
                }
                else
                {
                    _dtPartDetail = null;
                }

                //_bsPartDetail.DataSource = _dtPartDetail;
                gcPartDetail.DataSource = _dtPartDetail;

            }
            else
            {
                _currentReceiptPart = null;
                gcPartDetail.DataSource = null;
            }

            gvPartDetail.EndDataUpdate();
        }


        private void gvComponentList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPartDetail.RowCount > 0);

            if (isValidRow)
            {
                _currentComponentPart = e.Row as DataRowView;
            }
            else
            {
                _currentComponentPart = null;
            }
        }




        private void lcgReceipt_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                if (_currentReceipt == null)
                {
                    Dangol.Message("선택된 접수가 없습니다.");
                    return;
                }

                if (ConvertUtil.ToInt64(_currentReceipt["EXPORT_ID"]) > 0)
                {
                    Dangol.Message("이미 접수된 정보입니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 정보를 접수하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                receiptOne();

            }
            else if (e.Button.Properties.Tag.Equals(2))
            {

                DataRow[] rows = _dtReceipt.Select("CHECK = TRUE");

                if (rows.Length < 1)
                {
                    Dangol.Message("선택한 접수가 없습니다.");
                    return;
                }

                rows = _dtReceipt.Select("EXPORT_ID < 0");

                if (rows.Length < 1)
                {
                    Dangol.Message("이미 접수된 리스트입니다.");
                    return;
                }

                if (Dangol.MessageYN("접수 리스트 정보를 모두 접수하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                receiptAll();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                AddReceipt();
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                if (_currentReceipt == null)
                {
                    Dangol.Message("선택된 접수가 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 접수를 삭제하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                DeleteReceipt();

                gvReceipt.BeginUpdate();
                DataRow[] rows = _dtReceipt.Select("NO > 0", "NO");

                int index = 1;
                foreach (DataRow row in rows)
                {
                    row["NO"] = index++;
                }
                gvReceipt.EndUpdate();

                Dangol.Message("삭제되었습니다.");
            }
            
        }

        private void AddReceipt()
        {
            _isNew = true;

            DataTable dtPart;

            dtPart = new DataTable();
            dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            dtPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            dtPart.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
            dtPart.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            dtPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            dtPart.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));
            dtPart.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            dtPart.Columns.Add(new DataColumn("PRODUCT_STATE", typeof(string)));
            dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            dtPart.Columns.Add(new DataColumn("ETC", typeof(string)));
            dtPart.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            dtPart.Columns.Add(new DataColumn("LOCK_YN", typeof(string)));
            dtPart.Columns.Add(new DataColumn("NEW_COA", typeof(string)));
            dtPart.Columns.Add(new DataColumn("OLD_COA", typeof(string)));
            dtPart.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));
            dtPart.Columns.Add(new DataColumn("PRICE", typeof(long)));
            dtPart.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            dtPart.Columns.Add(new DataColumn("PRODUCE_COST", typeof(long)));
            dtPart.Columns.Add(new DataColumn("STATE", typeof(int)));

            Dictionary<long, DataTable> dicPartDetail = new Dictionary<long, DataTable>();
            List<long> listpartId = new List<long>();
            List<long> listproductId = new List<long>();

            _dicReceiptPart.Add(_id, dtPart);
            _dicReceiptPartDetail.Add(_id, dicPartDetail);
            _dicReceiptInventoryId.Add(_id, listpartId);
            _dicReceiptProductId.Add(_id, listproductId);

            var today = DateTime.Today;
            //int hour = ConvertUtil.ToInt32(DateTime.Now.ToString("HH"));

            //if (hour > 16)
            //    today = DateTime.Now.AddDays(1);

            string receiptDt = today.ToString("yyyy-MM-dd");


            DataRow dr = _dtReceipt.NewRow();

            dr["NO"] = _dtReceipt.Rows.Count + 1;
            dr["ID"] = _id;
            dr["EXPORT_ID"] = -1;
            dr["EXPORT"] = "접수시 자동발급";
            dr["RECEIPT_DT"] = receiptDt;
            dr["PAYMENT"] = "-1";
            dr["COMPANY_ID"] = "-1";
            dr["RELEASE_TYPE"] = "-1";
            dr["TAX_INVOICE"] = "-1";
            dr["DES"] = "";
            dr["PRODUCT"] = true;
            dr["ERROR"] = "";

            dr["CUSTOMER_NM"] = "";
            dr["TEL"] = "";
            dr["HP"] = "";
            dr["POSTAL_CD"] = "";
            dr["ADDRESS"] = "";
            dr["ADDRESS_DETAIL"] = "";
            dr["TAKE_USER_ID"] = ProjectInfo._userId;

            _dtReceipt.Rows.Add(dr);
            gvReceipt.MoveLast();

            _id++;
            _isNew = false;
        }

        private void lgcReceiptList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if(_currentReceipt == null)
            {
                Dangol.Message("선택된 접수가 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                using (dlgGetPartInfoByScanner dlgGetPartInfoByScanner = new dlgGetPartInfoByScanner())
                {
                    if (dlgGetPartInfoByScanner.ShowDialog() == DialogResult.OK)
                    {
                        List<string> listBarcode = dlgGetPartInfoByScanner._listBarcode;
                        DataTable dt = dlgGetPartInfoByScanner._dtBarcodeList;
                        JObject jResult = new JObject();
                        JObject jobj = new JObject();
                        var jArray = new JArray();

                        string oldCoa;
                        string newCoa;
                        string serialNo;
                        string boxNo;

                        foreach (DataRow row in dt.Rows)
                        {
                            oldCoa = $"{row["OLD_COA"]}";
                            newCoa = $"{row["NEW_COA"]}";
                            serialNo = $"{row["SERIAL_NO"]}";
                            boxNo = $"{row["BOX_NO"]}";
                            if (!string.IsNullOrWhiteSpace(oldCoa) || !string.IsNullOrWhiteSpace(newCoa) || !string.IsNullOrWhiteSpace(serialNo) || !string.IsNullOrWhiteSpace(boxNo))
                            {
                                JObject jData = new JObject();

                                jData.Add("BARCODE", $"{row["BARCODE"]}");
                                if (!string.IsNullOrWhiteSpace(oldCoa))
                                    jData.Add("OLD_COA", oldCoa);
                                if (!string.IsNullOrWhiteSpace(newCoa))
                                    jData.Add("NEW_COA", newCoa);
                                if (!string.IsNullOrWhiteSpace(serialNo))
                                    jData.Add("SERIAL_NO", serialNo);
                                if (!string.IsNullOrWhiteSpace(boxNo))
                                    jData.Add("BOX_NO", boxNo);

                                jArray.Add(jData);
                            }
                        }

                        jobj.Add("DATA", jArray);
                        jobj.Add("LIST_BARCODE", string.Join(",", listBarcode));
                        jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                        if (DBRelease.getPartInfo(jobj, ref jResult))
                        {
                            gvPart.FocusedRowObjectChanged -= gvPart_FocusedRowObjectChanged;
                            gvPartDetail.FocusedRowObjectChanged -= gvComponentList_FocusedRowObjectChanged;

                            addPart(jResult);

                            gvPartDetail.FocusedRowObjectChanged += gvComponentList_FocusedRowObjectChanged;
                            gvPart.FocusedRowObjectChanged += gvPart_FocusedRowObjectChanged;

                            gvPart.FocusedRowHandle = -2147483646;
                            gvPart.MoveFirst();

                            Dangol.Message("추가되었습니다.");
                        }
                        else
                        {
                        }
                    }
                }
                //using (dlgGetBarcode dlgGetBarcode = new dlgGetBarcode())
                //{
                //    if (dlgGetBarcode.ShowDialog() == DialogResult.OK)
                //    {
                //        gvPart.FocusedRowObjectChanged -= gvPart_FocusedRowObjectChanged;
                //        gvPartDetail.FocusedRowObjectChanged -= gvComponentList_FocusedRowObjectChanged;

                //        addPart(dlgGetBarcode._jResult);

                //        gvPartDetail.FocusedRowObjectChanged += gvComponentList_FocusedRowObjectChanged;
                //        gvPart.FocusedRowObjectChanged += gvPart_FocusedRowObjectChanged;

                //        gvPart.FocusedRowHandle = -2147483646;
                //        gvPart.MoveFirst();

                //        Dangol.Message("추가되었습니다.");
                //    }
                //}
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                using (DlgImportBarcode dlgImportBarcode = new DlgImportBarcode())
                {
                    dlgImportBarcode.ShowDialog();

                    if (dlgImportBarcode._isSuccess)
                    {
                        gvPart.FocusedRowObjectChanged -= gvPart_FocusedRowObjectChanged;
                        gvPartDetail.FocusedRowObjectChanged -= gvComponentList_FocusedRowObjectChanged;

                        addPart(dlgImportBarcode._jResult);

                        gvPartDetail.FocusedRowObjectChanged += gvComponentList_FocusedRowObjectChanged;
                        gvPart.FocusedRowObjectChanged += gvPart_FocusedRowObjectChanged;

                        gvPart.FocusedRowHandle = -2147483646;
                        gvPart.MoveFirst();

                        Dangol.Message("추가되었습니다.");
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                string removeTypeNm = "부품";
                int productYn = ConvertUtil.ToInt32(_currentReceiptPart["PRODUCT_YN"]);

                if (productYn == 1)
                {
                    removeTypeNm = "제품";
                }
                else
                { 
                    removeTypeNm = "부품";
                }

                if (Dangol.MessageYN($"선택한 {removeTypeNm}를 삭제하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                DeleteBarcode();

                lgcReceiptList.CustomHeaderButtons[3].Properties.Checked = false;

                gvPart.BeginUpdate();
                DataRow[] rows = _dtPart.Select("NO > 0", "NO");

                int index = 1;
                foreach (DataRow row in rows)
                {
                    row["NO"] = index++;
                }

                gvPart.EndUpdate();

                Dangol.Message("삭제되었습니다.");
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                
                if (Dangol.MessageYN($"변경사항을 저장하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                gvPart.BeginUpdate();
                updatePartInfo();
                gvPart.EndUpdate();

                Dangol.Message("처리되었습니다.");
            }


        }


        private bool addPart(JObject jResult)
        {
            //gvPart.BeginDataUpdate();
            //gvPartDetail.BeginDataUpdate();

            List<long> listNewProduct = new List<long>();

            long inventoryId;
            int index;
            int productType;
            List<long> listProductInventoryId = _dicReceiptProductId[_CurrentId];
            List<long> listInventoryId = _dicReceiptInventoryId[_CurrentId];

            _dtPart.BeginInit();

            if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
            {
                _dicPartDetail = _dicReceiptPartDetail[_CurrentId];

                JArray jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());
                index = _dtPart.Rows.Count+1;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                    productType = ConvertUtil.ToInt32(obj["PRODUCT_TYPE"]);

                    if (!listProductInventoryId.Contains(inventoryId))
                    {
                        DataRow dr = _dtPart.NewRow();
                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                        if (productType == 1)
                            dr["COMPONENT_CD"] = "DKT";
                        else if (productType == 2)
                            dr["COMPONENT_CD"] = "NTB";
                        else if (productType == 3)
                            dr["COMPONENT_CD"] = "AIO";
                        else
                            dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["PRODUCT_STATE"] = obj["PRODUCT_STATE"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["PRODUCT_YN"] = 1;
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["ETC"] = obj["ETC"];
                        dr["LOCK_YN"] = obj["LOCK_YN"];
                        dr["NEW_COA"] = ConvertUtil.ToString(obj["NEW_COA"]);
                        dr["OLD_COA"] = ConvertUtil.ToString(obj["OLD_COA"]);
                        dr["SERIAL_NO"] = ConvertUtil.ToString(obj["SERIAL_NO"]);
                        dr["CHECK"] = false;
                        if (ConvertUtil.ToString(obj["LOCK_YN"]).Equals("Y"))
                            dr["ETC"] = "사용중";

                        dr["PRICE"] = ConvertUtil.ToInt64(obj["PRICE"]);
                        dr["RELEASE_PRICE"] = ConvertUtil.ToInt64(obj["RELEASE_PRICE"]);
                        dr["PRODUCE_COST"] = ConvertUtil.ToInt64(obj["PRODUCE_COST"]);
                        dr["STATE"] = 0;

                        _dtPart.Rows.Add(dr);

                        DataTable dtPartDetail = new DataTable();
                        dtPartDetail.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
                        dtPartDetail.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
                        dtPartDetail.Columns.Add(new DataColumn("BARCODE", typeof(string)));
                        dtPartDetail.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
                        dtPartDetail.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
                        dtPartDetail.Columns.Add(new DataColumn("LOCK_YN", typeof(string))); 
                        dtPartDetail.Columns.Add(new DataColumn("ETC", typeof(string)));
                        _dicPartDetail.Add(inventoryId, dtPartDetail);

                        listProductInventoryId.Add(inventoryId);
                        listNewProduct.Add(inventoryId);
                        listInventoryId.Add(inventoryId);
                    }
                }
            }

            if (Convert.ToBoolean(jResult["PART_EXIST"]))
            {
                JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                index = _dtPart.Rows.Count + 1;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);

                    if (!listInventoryId.Contains(inventoryId))
                    {
                        DataRow dr = _dtPart.NewRow();
                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["PRODUCT_YN"] = 0;
                        dr["PRODUCT_STATE"] = -1;
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["ETC"] = obj["ETC"];
                        dr["LOCK_YN"] = obj["LOCK_YN"];
                        dr["CHECK"] = false;
                        if (ConvertUtil.ToString(obj["LOCK_YN"]).Equals("Y"))
                            dr["ETC"] = "사용중";
                        dr["PRICE"] = ConvertUtil.ToInt64(obj["PRICE"]);
                        dr["RELEASE_PRICE"] = ConvertUtil.ToInt64(obj["RELEASE_PRICE"]);
                        dr["PRODUCE_COST"] = ConvertUtil.ToInt64(obj["PRODUCE_COST"]);
                        dr["STATE"] = 0;

                        _dtPart.Rows.Add(dr);

                        listInventoryId.Add(inventoryId);
                    }
                }
            }

            _dtPart.EndInit();

            if (Convert.ToBoolean(jResult["PART_DETAIL_EXIST"]))
            {
                DataTable dtPartDetail;                   

                JArray jArray = JArray.Parse(jResult["PART_DETAIL_DATA"].ToString());

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    inventoryId = ConvertUtil.ToInt64(obj["P_INVENTORY_ID"]);

                    if (listNewProduct.Contains(inventoryId))
                    {
                        dtPartDetail = _dicPartDetail[inventoryId];

                        DataRow dr = dtPartDetail.NewRow();
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["LOCK_YN"] = obj["LOCK_YN"];
                        if (ConvertUtil.ToString(obj["LOCK_YN"]).Equals("Y"))
                            dr["ETC"] = "사용중";

                        dtPartDetail.Rows.Add(dr);
                    }
                }
            }

            

            //gvPart.EndDataUpdate();
            //gvPartDetail.EndDataUpdate();

            return true;
        }


        private void lgcComponentList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentComponentPart == null)
            {
                Dangol.Message("선택된 항목이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                addComponentPart();
            }
        }

        private void gvComponentList_DoubleClick(object sender, EventArgs e)
        {
            if (_currentComponentPart != null)
            {
                addComponentPart();
            }
        }

        private void addComponentPart()
        {

        }

 
        private void receiptAll()
        {
            Dangol.ShowSplash();

            DataRow[] rowReceipts = _dtReceipt.Select("CHECK = TRUE AND EXPORT_ID < 0");
            gvReceipt.BeginDataUpdate();
            long currentId;
            foreach (DataRow rowReceipt in rowReceipts)
            {
                currentId = ConvertUtil.ToInt64(rowReceipt["ID"]);
                receipt(rowReceipt, currentId);   
            }

            teExport.Text = $"{_currentReceipt["EXPORT"]}";
            gvReceipt.EndDataUpdate();
            Dangol.CloseSplash();
        }

        private void receiptOne()
        {
            Dangol.ShowSplash();
            gvReceipt.BeginDataUpdate();
            DataRow[] rowReceipts = _dtReceipt.Select($"ID = {_CurrentId}");

            foreach (DataRow rowReceipt in rowReceipts)
            {
                receipt(rowReceipt, _CurrentId);
            }

            teExport.Text = $"{_currentReceipt["EXPORT"]}";
            gvReceipt.EndDataUpdate();
            Dangol.CloseSplash();
        }




        private bool receipt(DataRow rowReceipt, long id)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            List<long> listInventoryId = new List<long>();
            List<long> productInventoryId= new List<long>();
            long inventoryId;

            jobj.Add("RECEIPT_DT", $"{ rowReceipt["RECEIPT_DT"]}");
            jobj.Add("COMPANY_ID", $"{ rowReceipt["COMPANY_ID"]}");
            jobj.Add("RELEASE_TYPE", $"{rowReceipt["RELEASE_TYPE"]}");
            jobj.Add("PAYMENT", $"{ rowReceipt["PAYMENT"]}");
            jobj.Add("TAX_INVOICE", $"{ rowReceipt["TAX_INVOICE"]}");
            jobj.Add("DES", $"{ rowReceipt["DES"]}");
            jobj.Add("CUSTOMER_NM", $"{ rowReceipt["CUSTOMER_NM"]}");
            jobj.Add("TEL", $"{ rowReceipt["TEL"]}");
            jobj.Add("MOBILE", $"{ rowReceipt["HP"]}");
            jobj.Add("POSTAL_CD", $"{ rowReceipt["POSTAL_CD"]}");
            jobj.Add("ADDRESS", $"{ rowReceipt["ADDRESS"]}");
            jobj.Add("ADDRESS_DETAIL", $"{ rowReceipt["ADDRESS_DETAIL"]}");
            jobj.Add("TAKE_USER_ID", $"{ rowReceipt["TAKE_USER_ID"]}");
            
            //jobj.Add("PRODUCT", ConvertUtil.ToBoolean(drRecript[0]["PRODUCT"]) ? 1 : 0);

            //_dtPart = _dicReceiptPart[_CurrentId];
            //_dtProduct = _dicReceiptProduct[_CurrentId];
            //_dicPartDetail = _dicReceiptPartDetail[_CurrentId];

            DataTable dt = _dicReceiptPart[id];
            Dictionary<long, DataTable> dicPartDetail = _dicReceiptPartDetail[id];
            DataTable dtPartDetail;

            //DataRow[] rows = dt.Select("PRODUCT_STATE < 3 AND LOCK_YN = 'N'");
            //DataRow[] rows = dt.Select("(PRODUCT_YN = 1 AND PRODUCT_STATE = 3) OR (PRODUCT_YN = 0 AND INVENTORY_STATE  = 'E')");
            DataRow[] rows = dt.Select("PRODUCT_YN = 0 AND INVENTORY_STATE  = 'E'");
            DataRow[] rowsDetail;
            long pInventoryId;
            int productYn;

            var jArrayDetail = new JArray();

            foreach (DataRow row in rows)
            {
                JObject jData = new JObject();

                pInventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                productYn = ConvertUtil.ToInt32(row["PRODUCT_YN"]);

                jData.Add("INVENTORY_ID", pInventoryId);
                jData.Add("COMPONENT_CD", $"{row["COMPONENT_CD"]}");
                jData.Add("PRODUCT_YN", productYn);

                jArray.Add(jData);

                if (!listInventoryId.Contains(pInventoryId))
                    listInventoryId.Add(pInventoryId);

                if (!productInventoryId.Contains(pInventoryId) && productYn == 1)
                {
                    productInventoryId.Add(pInventoryId);

                    dtPartDetail = dicPartDetail[pInventoryId];

                    //rowsDetail = dtPartDetail.Select("INVENTORY_STATE = 'E' AND LOCK_YN = 'N'");
                    //rowsDetail = dtPartDetail.Select("INVENTORY_STATE = 'E'");

                    //foreach (DataRow rowDetail in rowsDetail)
                    foreach (DataRow rowDetail in dtPartDetail.Rows)
                    {
                        JObject jDataDetail = new JObject();

                        inventoryId = ConvertUtil.ToInt64(rowDetail["INVENTORY_ID"]);

                        jDataDetail.Add("P_INVENTORY_ID", pInventoryId);
                        jDataDetail.Add("INVENTORY_ID", inventoryId);
                        jDataDetail.Add("COMPONENT_CD", $"{rowDetail["COMPONENT_CD"]}");

                        jArrayDetail.Add(jDataDetail);


                        if (!listInventoryId.Contains(inventoryId))
                            listInventoryId.Add(inventoryId);
                    }
                }
            }

            jobj.Add("DATA", jArray);
            jobj.Add("DATA_DETAIL", jArrayDetail);
            jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

            if(productInventoryId.Count > 0)
                jobj.Add("LIST_PRODUCT_INVENTORY_ID", string.Join(",", productInventoryId));

            if (DBRelease.createReceipt(jobj, ref jResult))
            {
                rowReceipt["EXPORT"] = $"{jResult["EXPORT"]}";
                rowReceipt["EXPORT_ID"] = $"{jResult["EXPORT_ID"]}";
                return true;
            }
            else
            {
                return false;
            }
        }

        private void updatePartInfo()
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("STATE = 2");
            if (rows.Length < 1)
            {
                Dangol.Message("수정된 부품이 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();
            var jArrayProduct = new JArray();

            //List<long> listInventoryId = new List<long>();
            //List<long> productInventoryId = new List<long>();
            int productYn;

            //DataTable dt = _dicReceiptPart[_CurrentId];

            foreach (DataRow row in rows)
            {
                JObject jData = new JObject();
                productYn = ConvertUtil.ToInt32(row["PRODUCT_YN"]);

                if(productYn == 1)
                {
                    jData.Add("INVENTORY_ID", ConvertUtil.ToInt64( row["INVENTORY_ID"]));
                    jData.Add("PRICE", ConvertUtil.ToInt64(row["PRICE"]));
                    jData.Add("RELEASE_PRICE", ConvertUtil.ToInt64(row["RELEASE_PRICE"]));
                    jData.Add("PRODUCE_COST", ConvertUtil.ToInt64(row["PRODUCE_COST"]));
                    jData.Add("NEW_COA", ConvertUtil.ToString(row["NEW_COA"]));
                    jData.Add("OLD_COA", ConvertUtil.ToString(row["OLD_COA"]));
                    jData.Add("SERIAL_NO", ConvertUtil.ToString(row["SERIAL_NO"]));
                    jData.Add("USER_ID", ProjectInfo._userId);

                    jArrayProduct.Add(jData);
                }
                else
                {
                    jData.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    jData.Add("PRICE", ConvertUtil.ToInt64(row["PRICE"]));
                    jData.Add("RELEASE_PRICE", ConvertUtil.ToInt64(row["RELEASE_PRICE"]));
                    jData.Add("LABOR_COST", ConvertUtil.ToInt64(row["PRODUCE_COST"]));
                    jData.Add("USER_ID", ProjectInfo._userId);

                    jArray.Add(jData);
                }
            }

            jobj.Add("DATA", jArray);
            jobj.Add("PRODUCT_DATA", jArrayProduct);

            if (DBRelease.updatePartInfo(jobj, ref jResult))
            {
                gvPart.BeginDataUpdate();
                foreach (DataRow row in rows)
                {
                    row["STATE"] = 0;
                }
                gvPart.EndDataUpdate();
            }
            else
            {
                //return false;
            }
        }

        private void DeleteReceipt()
        {

            gvPart.BeginDataUpdate();
            gvPartDetail.BeginDataUpdate();

            //DataRow[] rows = dt.Select($"CHECK = TRUE");
            //DataRow[] rowsRemain;
            DataTable dtPartDetail;

            List<long> listInventoryId = _dicReceiptInventoryId[_CurrentId];
            List<long> listProductId = _dicReceiptProductId[_CurrentId];

            foreach (long inventoryId in listProductId)
            {
                if (_dicPartDetail.ContainsKey(inventoryId))
                {
                    dtPartDetail = _dicPartDetail[inventoryId];
                    dtPartDetail.Clear();
                }
            }

            _dicPartDetail.Clear();
            listProductId.Clear();
            listInventoryId.Clear();

            _dtPart.Clear();

            _dicReceiptPart.Remove(_CurrentId);
            _dicReceiptPartDetail.Remove(_CurrentId);
            _dicReceiptInventoryId.Remove(_CurrentId);
            _dicReceiptProductId.Remove(_CurrentId);

            gvPartDetail.EndDataUpdate();
            gvPart.EndDataUpdate();

            gvReceipt.BeginDataUpdate();

            _currentReceipt.Delete();

            gvReceipt.EndDataUpdate();

        }

        private void DeleteBarcode()
        {
            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -2147483646;
            gvPart.FocusedRowHandle = rowhandle;


            gvPart.BeginDataUpdate();
            gvPartDetail.BeginDataUpdate();

            DataRow[] rows = _dtPart.Select($"CHECK = TRUE");
            DataTable dtPartDetail;
            long inventoryId;
            List<long> listInventoryId = _dicReceiptInventoryId[_CurrentId];
            List<long> listProductId = _dicReceiptProductId[_CurrentId];

            _dtPart.BeginInit();
            foreach (DataRow row in rows)
            {
                inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                if (_dicPartDetail.ContainsKey(inventoryId))
                {
                    dtPartDetail = _dicPartDetail[inventoryId];
                    dtPartDetail.Clear();
                    _dicPartDetail.Remove(inventoryId);
                }

                listInventoryId.Remove(inventoryId);
                listProductId.Remove(inventoryId);
                row.Delete();
            }
            _dtPart.EndInit();

            gvPartDetail.EndDataUpdate();
            gvPart.EndDataUpdate();

        }

       


        private void risePartCnt_EditValueChanged(object sender, EventArgs e)
        {
            //tlPart.BeginUpdate();

            //SpinEdit editor = (SpinEdit)sender;
            //int partCnt = ConvertUtil.ToInt32(editor.EditValue);

            //TreeListNode node = _currentReceiptPart.RootNode;
            //int cnt = ConvertUtil.ToInt32(node["PART_CNT"]);
            //cnt -= _currnetPartCnt;
            //cnt += partCnt;
            //_currnetPartCnt = partCnt;
            //_currentReceiptPart["PART_CNT"] = partCnt;
            //node["PART_CNT"] = cnt;
            //tlPart.EndUpdate();
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

        private void lgcReceiptList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {

                int rowhandle = gvPart.FocusedRowHandle;
                int topRowIndex = gvPart.TopRowIndex;
                gvPart.FocusedRowHandle = -2147483646;
                gvPart.FocusedRowHandle = rowhandle;

                try
                {
                    gvPart.BeginUpdate();
                    foreach (DataRow row in _dtPart.Rows)
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
        }

        private void lgcReceiptList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvPart.FocusedRowHandle;
                int topRowIndex = gvPart.TopRowIndex;
                gvPart.FocusedRowHandle = -2147483646;
                gvPart.FocusedRowHandle = rowhandle;

                gvPart.BeginDataUpdate();

               

                foreach (DataRow row in _dtPart.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
                gvPart.EndDataUpdate();
            }
        }

        private void leComapny_EditValueChanged(object sender, EventArgs e)
        {
            if (_currentReceipt != null)
            {
                long companyId = ConvertUtil.ToInt64(leComapny.EditValue);
                gvReceipt.BeginDataUpdate();
                teCustomerNm.EditValueChanged -= teCustomerNm_EditValueChanged;
                if (companyId == -1)
                {
                    //teCustomerNm.Text = "";
                    teCustomerNm.ReadOnly = false;
                }
                else
                {
                    teCustomerNm.Text = leComapny.Text;
                    teCustomerNm.ReadOnly = true;
                }
                teCustomerNm.EditValueChanged += teCustomerNm_EditValueChanged;
                gvReceipt.EndDataUpdate();
                //gvReceipt.BeginDataUpdate();
                //_currentReceipt["CUSTOMER_NM"] = teCustomerNm.Text;
                //gvReceipt.EndDataUpdate();

                //teTel.Text = "";
                //teHp.Text = "";
                //tePostalCd.Text = "";
                //teAddress.Text = "";
                //teAddressDetail.Text = "";
            }

        }

        private void setGridPartInit()
        {
            //int rowhandle = gvPart.FocusedRowHandle;
            //int topRowIndex = gvPart.TopRowIndex;
            //gvPart.FocusedRowHandle = -2147483646;
            //gvPart.FocusedRowHandle = rowhandle;

            lgcReceiptList.CustomHeaderButtons[3].Properties.Checked = false;

            gvPart.BeginDataUpdate();

            _dtPart.BeginInit();
            foreach (DataRow row in _dtPart.Rows)
                row["CHECK"] = false;
            _dtPart.EndInit();

            //gcComponentCd.Visible = !isProductCheck;
            //gcProduct.Visible = !isProductCheck;
            //gcInventoryState.Visible = !isProductCheck;

            //gcProductState.Visible = isProductCheck;

            //if(isProductCheck)
            //    for (int i = 0; i < _columnsProduct.Length; i++)
            //        _columnsProduct[i].VisibleIndex = i;
            //else
            //    for (int i = 0; i < _columnsPart.Length; i++)
            //        _columnsPart[i].VisibleIndex = i;

            gvPart.EndDataUpdate();
        }

        private void teCustomerNm_EditValueChanged(object sender, EventArgs e)
        {
            if (_currentReceipt != null)
            {
                gvReceipt.BeginDataUpdate();
                //_currentReceipt["CUSTOMER_NM"] = teCustomerNm.Text;
                gvReceipt.EndDataUpdate();
            }
        }

        private void deReceiptDt_EditValueChanged(object sender, EventArgs e)
        {
            if (_currentReceipt != null)
            {
                gvReceipt.BeginDataUpdate();
                //_currentReceipt["RECEIPT_DT"] = deReceiptDt.Text;
                gvReceipt.EndDataUpdate();
            }
        }

        private void ceProduct_CheckedChanged(object sender, EventArgs e)
        {
            //bool check = ConvertUtil.ToBoolean(ceProduct.EditValue);

            //gvPart.BeginDataUpdate();

            //gcComponentCd.Visible = !check;
            //gcProduct.Visible = !check;
            //gcInventoryState.Visible = !check;

            //gcProductState.Visible = check;

            //if (check)
            //    for (int i = 0; i < _columnsProduct.Length; i++)
            //        _columnsProduct[i].VisibleIndex = i;
            //else
            //    for (int i = 0; i < _columnsPart.Length; i++)
            //        _columnsPart[i].VisibleIndex = i;

            //gvPart.EndDataUpdate();

        }

        private void sbInput_Click(object sender, EventArgs e)
        {
            setData();
        }

        private void teScan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                setData();
            }
        }

        private void setData()
        {
            string scanData = teScan.Text;

            if (scanData.Length < 1 || string.IsNullOrWhiteSpace(scanData))
                return;


            if (scanData.Length == 12 && (regex1.IsMatch(scanData) || regex2.IsMatch(scanData) || regex3.IsMatch(scanData)))
            {
                
                if (_listBarcode.Contains(scanData))
                {
                    int rowHandle = gvBarcodeList.LocateByValue("BARCODE", scanData);
                    if (rowHandle > -1)
                    {
                        //gvBarcodeList.MoveBy(rowHandle);
                        gvBarcodeList.FocusedRowHandle = rowHandle;
                    }

                }
                else
                {
                    _listBarcode.Add(scanData);
                    gvBarcodeList.BeginDataUpdate();
                    DataRow dr = _dtBarcodeList.NewRow();
                    dr["INVENTORY_ID"] = -1;
                    dr["BARCODE"] = scanData;
                    dr["OLD_COA"] = "";
                    dr["NEW_COA"] = "";
                    dr["SERIAL_NO"] = "";
                    dr["BOX_NO"] = "";
                    dr["CHECK"] = false;
                    _dtBarcodeList.Rows.Add(dr);
                    gvBarcodeList.EndDataUpdate();
                    gvBarcodeList.MoveLast();
                }
            }
            else if (oldCoa.IsMatch(scanData))
            {
                if (_currentBarcodeRow != null)
                {
                    gvBarcodeList.FocusedRowObjectChanged -= gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged -= gvBarcodeList_FocusedRowChanged;
                    gvBarcodeList.BeginDataUpdate();
                    _currentBarcodeRow["OLD_COA"] = scanData;
                    gvBarcodeList.EndDataUpdate();
                    gvBarcodeList.FocusedRowObjectChanged += gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged += gvBarcodeList_FocusedRowChanged;
                    gvBarcodeList.FocusedRowHandle = _barcodeFocusedFowHandl;
                }
            }
            else if (newCoa.IsMatch(scanData))
            {
                if (_currentBarcodeRow != null)
                {
                    gvBarcodeList.FocusedRowObjectChanged -= gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged -= gvBarcodeList_FocusedRowChanged;
                    gvBarcodeList.BeginDataUpdate();
                    _currentBarcodeRow["NEW_COA"] = scanData;
                    gvBarcodeList.EndDataUpdate();
                    gvBarcodeList.FocusedRowObjectChanged += gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged += gvBarcodeList_FocusedRowChanged;
                    gvBarcodeList.FocusedRowHandle = _barcodeFocusedFowHandl;
                }
            }
            else if (scanData.Substring(0,3).ToUpper().Equals("BOX"))
            {
                if (_currentBarcodeRow != null)
                {
                    gvBarcodeList.FocusedRowObjectChanged -= gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged -= gvBarcodeList_FocusedRowChanged;
                    gvBarcodeList.BeginDataUpdate();
                    _currentBarcodeRow["BOX_NO"] = scanData;
                    gvBarcodeList.EndDataUpdate();
                    gvBarcodeList.FocusedRowObjectChanged += gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged += gvBarcodeList_FocusedRowChanged;
                    gvBarcodeList.FocusedRowHandle = _barcodeFocusedFowHandl;
                }
            }
            else
            {
                if (_currentBarcodeRow != null)
                {
                    gvBarcodeList.FocusedRowObjectChanged -= gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged -= gvBarcodeList_FocusedRowChanged;
                    gvBarcodeList.BeginDataUpdate();
                    _currentBarcodeRow["SERIAL_NO"] = scanData;
                    gvBarcodeList.EndDataUpdate();
                    gvBarcodeList.FocusedRowObjectChanged += gvBarcodeList_FocusedRowObjectChanged;
                    gvBarcodeList.FocusedRowChanged += gvBarcodeList_FocusedRowChanged;

                    gvBarcodeList.FocusedRowHandle = _barcodeFocusedFowHandl;
                }
            }

            teScan.Text = "";
        }

        private void gvBarcodeList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvBarcodeList.RowCount > 0);
            _barcodeFocusedFowHandl = e.FocusedRowHandle;

            if (isValidRow)
            {
                _currentBarcodeRow = e.Row as DataRowView;
            }
            else
            {
                _currentBarcodeRow = null;
            }
        }

        private void gvBarcodeList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvBarcodeList.RowCount > 0);
            _barcodeFocusedFowHandl = e.FocusedRowHandle;

            if (isValidRow)
            {
                _currentBarcodeRow = gvBarcodeList.GetRow(e.FocusedRowHandle) as DataRowView;
            }
            else
            {
                _currentBarcodeRow = null;
            }
        }

        private void lcgScan_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                if (_currentReceipt == null)
                {
                    Dangol.Message("선택된 접수가 없습니다.");
                    return;
                }


                if (Dangol.MessageYN("선택하신 부품을 추가하시겠습니까?") == DialogResult.Yes)
                {

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    var jArray = new JArray();

                    string oldCoa;
                    string newCoa;
                    string serialNo;
                    string boxNo;

                    foreach (DataRow row in _dtBarcodeList.Rows)
                    {
                        oldCoa = $"{row["OLD_COA"]}";
                        newCoa = $"{row["NEW_COA"]}";
                        serialNo = $"{row["SERIAL_NO"]}";
                        boxNo = $"{row["BOX_NO"]}";
                        if (!string.IsNullOrWhiteSpace(oldCoa) || !string.IsNullOrWhiteSpace(newCoa) || !string.IsNullOrWhiteSpace(serialNo) || !string.IsNullOrWhiteSpace(boxNo))
                        {
                            JObject jData = new JObject();

                            jData.Add("BARCODE", $"{row["BARCODE"]}");
                            if (!string.IsNullOrWhiteSpace(oldCoa))
                                jData.Add("OLD_COA", oldCoa);
                            if (!string.IsNullOrWhiteSpace(newCoa))
                                jData.Add("NEW_COA", newCoa);
                            if (!string.IsNullOrWhiteSpace(serialNo))
                                jData.Add("SERIAL_NO", serialNo);
                            if (!string.IsNullOrWhiteSpace(boxNo))
                                jData.Add("BOX_NO", boxNo);

                            jArray.Add(jData);
                        }
                    }

                    jobj.Add("DATA", jArray);
                    jobj.Add("LIST_BARCODE", string.Join(",", _listBarcode));
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.getPartInfo(jobj, ref jResult))
                    {
                        gvPart.FocusedRowObjectChanged -= gvPart_FocusedRowObjectChanged;
                        gvPartDetail.FocusedRowObjectChanged -= gvComponentList_FocusedRowObjectChanged;

                        addPart(jResult);

                        gvPartDetail.FocusedRowObjectChanged += gvComponentList_FocusedRowObjectChanged;
                        gvPart.FocusedRowObjectChanged += gvPart_FocusedRowObjectChanged;

                        gvPart.FocusedRowHandle = -2147483646;
                        gvPart.MoveFirst();

                        Dangol.Message("추가되었습니다.");
                    }
                    else
                    {
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvBarcodeList.FocusedRowHandle;
                gvBarcodeList.FocusedRowHandle = -1;
                gvBarcodeList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtBarcodeList.Select("CHECK = True");
                if (rows.Length < 1)
                    Dangol.Message("체크된 부품이 없습니다.");
                else
                {
                    if (Dangol.MessageYN("선택하신 부품을 목록에서 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        gvBarcodeList.BeginDataUpdate();
                        _dtBarcodeList.BeginInit();

                        foreach (DataRow row in rows)
                        {
                            if (_listBarcode.Contains(Convert.ToString(row["BARCODE"]).ToUpper()))
                                _listBarcode.Remove(Convert.ToString(row["BARCODE"]).ToUpper());

                            row.Delete();
                        }

                        _dtBarcodeList.EndInit();
                        gvBarcodeList.EndDataUpdate();
                    }
                }

            }
        }

        private void lcgScan_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            bool ischeck = e.Button.IsChecked.Value;
            if (ischeck)
            {
                _dtBarcodeList.BeginInit();
                foreach (DataRow row in _dtBarcodeList.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = ischeck;
                    row.EndEdit();
                }
                _dtBarcodeList.EndInit();
            }
        }

        private void lcgScan_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            bool ischeck = e.Button.IsChecked.Value;

            if (!ischeck)
            {
                _dtBarcodeList.BeginInit();
                foreach (DataRow row in _dtBarcodeList.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = ischeck;
                    row.EndEdit();
                }
                _dtBarcodeList.EndInit();
            }
        }

        

        private void sbReleasePriceCustom_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            long price = ConvertUtil.ToInt64(seReleasePriceCustom.EditValue);
            string sprice = ConvertUtil.ToString(seReleasePriceCustom.Text);

            if (Dangol.MessageYN($"선택한 부품/제품의 출고가를 {sprice}원으로 변경하시겠습니까?") == DialogResult.Yes)
            {
                gvPart.BeginDataUpdate();
                Dangol.ShowSplash();

                foreach (DataRow row in rows)
                {
                    row["RELEASE_PRICE"] = price;
                    row["STATE"] = 2;
                }

                Dangol.CloseSplash();
                gvPart.EndDataUpdate();
                Dangol.Message("처리되었습니다.");
            }
        }

        private void sbReleasePriceRatio_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            double ratio = ConvertUtil.ToDouble(seReleasePriceRatio.EditValue);

            if (Dangol.MessageYN($"선택한 부품/제품의 출고가를 원가대비 {ratio}%로 변경하시겠습니까?") == DialogResult.Yes)
            {
                gvPart.BeginDataUpdate();
                Dangol.ShowSplash();

                double price = 0;
                long releasePrice;
                foreach (DataRow row in rows)
                {
                    price = ConvertUtil.ToDouble(row["PRICE"]) * (ratio/100.0);
                    releasePrice = ConvertUtil.ToInt64(price);
                    row["RELEASE_PRICE"] = releasePrice;
                    row["STATE"] = 2;
                }

                Dangol.CloseSplash();
                gvPart.EndDataUpdate();
                Dangol.Message("처리되었습니다.");
            }
        }

        private void sbReleasePriceAdd_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            long price = ConvertUtil.ToInt64(seReleasePriceAdd.EditValue);
            string sprice = ConvertUtil.ToString(seReleasePriceAdd.Text);

            if (Dangol.MessageYN($"선택한 부품/제품의 출고가를 원가 + {sprice}원으로 변경하시겠습니까?") == DialogResult.Yes)
            {
                gvPart.BeginDataUpdate();
                Dangol.ShowSplash();

                foreach (DataRow row in rows)
                {
                    row["RELEASE_PRICE"] = ConvertUtil.ToInt64(row["PRICE"]) + price;
                    row["STATE"] = 2;
                }

                Dangol.CloseSplash();
                gvPart.EndDataUpdate();
                Dangol.Message("처리되었습니다.");
            }
        }

        private void sbProduceCost_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            long price = ConvertUtil.ToInt64(seProduceCost.EditValue);
            string sprice = ConvertUtil.ToString(seProduceCost.Text);

            if (Dangol.MessageYN($"선택한 부품/제품의 생산비를 {sprice}원으로 변경하시겠습니까?") == DialogResult.Yes)
            {
                gvPart.BeginDataUpdate();
                Dangol.ShowSplash();

                foreach (DataRow row in rows)
                {
                    row["PRODUCE_COST"] = price;
                    row["STATE"] = 2;
                }

                Dangol.CloseSplash();
                gvPart.EndDataUpdate();
                Dangol.Message("처리되었습니다.");
            }
        }

        private void gvPart_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "CHECK")
            {
                int state = ConvertUtil.ToInt32(_currentReceiptPart["STATE"]);

                if (state == 0)
                    _currentReceiptPart["STATE"] = 2;
            }
        }

        private void gvPart_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "BARCODE" && e.RowHandle >= 0)
            {
                int state = ConvertUtil.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]));

                if (state == 2)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
        }
    }
}