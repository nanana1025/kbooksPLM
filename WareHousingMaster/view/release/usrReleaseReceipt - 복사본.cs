using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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

        DataTable _dtReceipt;
        DataTable _dtPart;
        DataTable _dtProduct;
        DataTable _dtPartDetail;
        Dictionary<long, DataTable> _dicPartDetail;


        BindingSource _bsExport;
        BindingSource _bsPart;
        BindingSource _bsProduct;
        BindingSource _bsPartDetail;

        Dictionary<long, DataTable> _dicReceiptPart;
        Dictionary<long, DataTable> _dicReceiptProduct;
        Dictionary<long, Dictionary<long, DataTable>> _dicReceiptPartDetail;
        Dictionary<long, List<long>> _dicReceiptInventoryId;
        Dictionary<long, List<long>> _dicReceiptProductId;

        string[] _consignedComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "STG", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG" };
        string[] _consignedComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "저장장치", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스" };

        Dictionary<int, string> _dicConsignedComponentCd;
        Dictionary<string, int> _dicConsignedComponentCdReverse;

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


            _bsExport = new BindingSource();
            _bsPart = new BindingSource();
            _bsProduct = new BindingSource();
            _bsPartDetail = new BindingSource();

            _dicReceiptPart = new Dictionary<long, DataTable>();
            _dicReceiptProduct= new Dictionary<long, DataTable>();
            _dicReceiptPartDetail = new Dictionary<long, Dictionary<long, DataTable>>();

            _dicReceiptInventoryId = new Dictionary<long, List<long>>();
            _dicReceiptProductId = new Dictionary<long, List<long>>();

            _dicProductType = new Dictionary<string, string>();
            _dicGuarantee = new Dictionary<string, string>();
            _id = 0;

            _columnsPart = new GridColumn[] {gcNumber, gcCheck, gcComponentCd, gcBarcode, gcProduct, gcInventoryState, gcModelNm, gcDes };
            _columnsProduct = new GridColumn[] { gcNumber, gcCheck,gcBarcode, gcProductState, gcModelNm, gcDes };

            _dicConsignedComponentCd = new Dictionary<int, string>();
            _dicConsignedComponentCdReverse = new Dictionary<string, int>();

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                _dicConsignedComponentCd.Add(i, _consignedComponetCd[i]);
                _dicConsignedComponentCdReverse.Add(_consignedComponetCd[i], i);
            }

            _currentGetComponentCd = null;

            _isNew = false;
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
            ceProduct.DataBindings.Add(new Binding("EditValue", _bsExport, "PRODUCT", false, DataSourceUpdateMode.OnPropertyChanged));
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
           

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "직접입력");
            Util.LookupEditHelper(leComapny, dtCompany, "KEY", "VALUE");

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
            //_bsPart.DataSource = _dtPart;
            //_bsProduct.DataSource = _dtProduct;
            //_bsPartDetail.DataSource = _dtPartDetail;
        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsExport;

            //gcPart.DataSource = null;
            //gcPart.DataSource = _bsPart;

            //gcPartDetail.DataSource = null;
            //gcPartDetail.DataSource = _bsPartDetail;

            gcPart.BeginInit();
            gcComponentCd.Visible = true;
            gcProduct.Visible = true;
            gcInventoryState.Visible = true;
            gcProductState.Visible = false;
            gcPart.EndInit();
        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);

            gvPart.BeginDataUpdate();

            if (isValidRow)
            {
                _currentReceipt = e.Row as DataRowView;
                _CurrentId = ConvertUtil.ToInt64(_currentReceipt["ID"]);
                _isProductCheck = ConvertUtil.ToBoolean(_currentReceipt["PRODUCT"]);

                _dtPart = _dicReceiptPart[_CurrentId];
                _dtProduct = _dicReceiptProduct[_CurrentId];
                _dicPartDetail = _dicReceiptPartDetail[_CurrentId];

                setGridPartInit(_isProductCheck);
                gcPart.DataSource = null;
                if (_isProductCheck)
                {
                    lgcReceiptList.CustomHeaderButtons[4].Properties.Checked = true;
                    gcPart.DataSource = _dtProduct;
                }
                else
                {
                    lgcReceiptList.CustomHeaderButtons[4].Properties.Checked = false;
                    gcPart.DataSource = _dtPart;
                }

                gvPart.MoveFirst();

                if (!_isNew)
                    setReceiptPart(_currentReceipt["ID"]);

            }
            else
            {
                _dtPart = null;
                _dtProduct = null;
                _dicPartDetail = null;
                _currentReceipt = null;               
            }

            gvPart.EndDataUpdate();
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            gvPartDetail.BeginDataUpdate();

            if (isValidRow)
            {
                _currentReceiptPart = e.Row as DataRowView;
                _inventoryId = ConvertUtil.ToInt64(_currentReceiptPart["INVENTORY_ID"]);

                if (_dicPartDetail != null)
                {
                    if (_dicPartDetail.ContainsKey(_inventoryId))
                        _dtPartDetail = _dicPartDetail[_inventoryId];
                    else
                        _dtPartDetail = null;

                    gcPartDetail.DataSource = null;
                    gcPartDetail.DataSource = _dtPartDetail;
                }
                else
                {
                    gcPartDetail.DataSource = null;
                }


                if (!_isNew)
                    setReceiptPart(_currentReceipt["ID"]);
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

        private void setReceiptPart(object id)
        {
            //tlPart.BeginUpdate();
            //_bsPart.Filter = $"ID = {id}";
            //tlPart.EndUpdate();
        }




        private void lcgReceipt_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                if (_currentReceipt == null)
                {
                    Dangol.Message("선택된 항목이 없습니다.");
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
                if (Dangol.MessageYN("접수 리스트 정보를 모두 접수하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                //receiptAll();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                AddReceipt();
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                if (_currentReceipt == null)
                {
                    Dangol.Message("선택된 항목이 없습니다.");
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
            DataTable dtProduct;
            DataTable dtPartDetail;

            dtPart = new DataTable();
            dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            dtPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            dtPart.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            dtPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            dtPart.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));
            dtPart.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            dtPart.Columns.Add(new DataColumn("ETC", typeof(string)));
            dtPart.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            dtPart.Columns.Add(new DataColumn("LOCK_YN", typeof(string)));


            dtProduct = new DataTable();
            dtProduct.Columns.Add(new DataColumn("NO", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            dtProduct.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
            dtProduct.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("PRODUCT_STATE", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("ETC", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            dtProduct.Columns.Add(new DataColumn("LOCK_YN", typeof(string)));

            Dictionary<long, DataTable> dicPartDetail = new Dictionary<long, DataTable>();
            List<long> listpartId = new List<long>();
            List<long> listproductId = new List<long>();

            _dicReceiptPart.Add(_id, dtPart);
            _dicReceiptProduct.Add(_id, dtProduct);
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
                Dangol.Message("선택된 항목이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                using (dlgGetBarcode dlgGetBarcode = new dlgGetBarcode())
                {
                    if (dlgGetBarcode.ShowDialog() == DialogResult.OK)
                    {
                        gvPart.FocusedRowObjectChanged -= gvPart_FocusedRowObjectChanged;
                        gvPartDetail.FocusedRowObjectChanged -= gvComponentList_FocusedRowObjectChanged;

                        addPart(dlgGetBarcode._jResult);

                        gvPartDetail.FocusedRowObjectChanged += gvComponentList_FocusedRowObjectChanged;
                        gvPart.FocusedRowObjectChanged += gvPart_FocusedRowObjectChanged;

                        gvPart.FocusedRowHandle = -2147483646;
                        gvPart.MoveFirst();

                        Dangol.Message("추가되었습니다.");
                    }
                }
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
                DataTable dt;

                if (lgcReceiptList.CustomHeaderButtons[4].Properties.Checked)
                {
                    dt = _dtProduct;
                    removeTypeNm = "제품";
                }
                else
                { 
                    dt = _dtPart;
                    removeTypeNm = "부품";
                }

                if (Dangol.MessageYN($"선택한 {removeTypeNm}를 삭제하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                DeleteBarcode();

                lgcReceiptList.CustomHeaderButtons[3].Properties.Checked = false;

                gvPart.BeginUpdate();
                DataRow[] rows = _dtProduct.Select("NO > 0", "NO");

                int index = 1;
                foreach (DataRow row in rows)
                {
                    row["NO"] = index++;
                }

                rows = _dtPart.Select("NO > 0", "NO");

                index = 1;
                foreach (DataRow row in rows)
                {
                    row["NO"] = index++;
                }

                gvPart.EndUpdate();

                Dangol.Message("삭제되었습니다.");
            }

            
        }


        private bool addPart(JObject jResult)
        {

            gvPart.BeginDataUpdate();
            gvPartDetail.BeginDataUpdate();

            List<long> listNewProduct = new List<long>();

            long inventoryId;
            int index;

            List<long> listProductInventoryId = _dicReceiptProductId[_CurrentId];

            if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
            {
                _dtProduct.BeginInit();

                _dicPartDetail = _dicReceiptPartDetail[_CurrentId];

                JArray jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());
                index = _dtProduct.Rows.Count+1;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);

                    if (!listProductInventoryId.Contains(inventoryId))
                    {
                        DataRow dr = _dtProduct.NewRow();
                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["PRODUCT_STATE"] = obj["PRODUCT_STATE"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["ETC"] = obj["ETC"];
                        dr["LOCK_YN"] = obj["LOCK_YN"];
                        dr["CHECK"] = false;
                        if (ConvertUtil.ToString(obj["LOCK_YN"]).Equals("Y"))
                            dr["ETC"] = "사용중";

                        _dtProduct.Rows.Add(dr);

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
                    }
                }

                _dtProduct.EndInit();
            }

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

            if (Convert.ToBoolean(jResult["PART_EXIST"]))
            {
                _dtPart.BeginInit();
                List<long> listInventoryId = _dicReceiptInventoryId[_CurrentId];

                JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                index = _dtPart.Rows.Count+1;

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
                        dr["PRODUCT_YN"] = listProductInventoryId.Contains(inventoryId)?1:0;
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["ETC"] = obj["ETC"];
                        dr["LOCK_YN"] = obj["LOCK_YN"];
                        dr["CHECK"] = false;
                        if (ConvertUtil.ToString(obj["LOCK_YN"]).Equals("Y"))
                            dr["ETC"] = "사용중";

                        _dtPart.Rows.Add(dr);

                        listInventoryId.Add(inventoryId);
                    }
                }
                _dtPart.EndInit();
            }

            gvPart.EndDataUpdate();
            gvPartDetail.EndDataUpdate();

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

 
        private void addComponent()
        {
           
        }

        private void receiptOne()
        {
            Dangol.ShowSplash();
            receipt(_CurrentId);

            teExport.Text = $"{_currentReceipt["EXPORT"]}";
            Dangol.ShowSplash();
        }


        private bool receipt(long id)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            List<long> listInventoryId = new List<long>();
            long inventoryId;

            DataRow[] drRecript = _dtReceipt.Select($"ID = {id}");

            jobj.Add("RECEIPT_DT", $"{ drRecript[0]["RECEIPT_DT"]}");
            jobj.Add("COMPANY_ID", $"{ drRecript[0]["COMPANY_ID"]}");
            jobj.Add("RELEASE_TYPE", $"{ drRecript[0]["RELEASE_TYPE"]}");
            jobj.Add("PAYMENT", $"{ drRecript[0]["PAYMENT"]}");
            jobj.Add("TAX_INVOICE", $"{ drRecript[0]["TAX_INVOICE"]}");
            jobj.Add("DES", $"{ drRecript[0]["DES"]}");
            jobj.Add("CUSTOMER_NM", $"{ drRecript[0]["CUSTOMER_NM"]}");
            jobj.Add("TEL", $"{ drRecript[0]["TEL"]}");
            jobj.Add("MOBILE", $"{ drRecript[0]["HP"]}");
            jobj.Add("POSTAL_CD", $"{ drRecript[0]["POSTAL_CD"]}");
            jobj.Add("ADDRESS", $"{ drRecript[0]["ADDRESS"]}");
            jobj.Add("ADDRESS_DETAIL", $"{ drRecript[0]["ADDRESS_DETAIL"]}");
            jobj.Add("PRODUCT", ConvertUtil.ToBoolean(drRecript[0]["PRODUCT"]) ? 1 : 0);

            //_dtPart = _dicReceiptPart[_CurrentId];
            //_dtProduct = _dicReceiptProduct[_CurrentId];
            //_dicPartDetail = _dicReceiptPartDetail[_CurrentId];

            if (ConvertUtil.ToBoolean(drRecript[0]["PRODUCT"]))
            {
                DataTable dt = _dicReceiptProduct[id];
                Dictionary<long, DataTable> dicPartDetail = _dicReceiptPartDetail[id];
                DataTable dtPartDetail;

                //DataRow[] rows = dt.Select("PRODUCT_STATE < 3 AND LOCK_YN = 'N'");
                DataRow[] rows = dt.Select("PRODUCT_STATE < 3");
                DataRow[] rowsDetail;
                long pInventoryId;

                var jArrayDetail = new JArray();

                foreach (DataRow row in rows)
                {
                    JObject jData = new JObject();

                    pInventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                    jData.Add("INVENTORY_ID", pInventoryId);
                    jData.Add("COMPONENT_CD", "MBD");
                    jData.Add("PRODUCT_YN", 1);

                    jArray.Add(jData);

                    if (!listInventoryId.Contains(pInventoryId))
                        listInventoryId.Add(pInventoryId);

                    dtPartDetail = dicPartDetail[pInventoryId];

                    //rowsDetail = dtPartDetail.Select("INVENTORY_STATE = 'E' AND LOCK_YN = 'N'");
                    rowsDetail = dtPartDetail.Select("INVENTORY_STATE = 'E'");

                    foreach (DataRow rowDetail in rowsDetail)
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

                jobj.Add("DATA", jArray);
                jobj.Add("DATA_DETAIL", jArrayDetail);
            }
            else
            {
                DataTable dt = _dicReceiptPart[id];

                //DataRow[] rows = dt.Select("INVENTORY_STATE = 'E' AND LOCK_YN = 'N'");
                DataRow[] rows = dt.Select("INVENTORY_STATE = 'E'");

                foreach (DataRow row in rows)
                {
                    JObject jData = new JObject();

                    inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                    jData.Add("INVENTORY_ID", inventoryId);
                    jData.Add("COMPONENT_CD", $"{row["COMPONENT_CD"]}");
                    jData.Add("PRODUCT_YN", ConvertUtil.ToInt32(row["PRODUCT_YN"]));

                    jArray.Add(jData);

                    if (!listInventoryId.Contains(inventoryId))
                        listInventoryId.Add(inventoryId);
                }

                jobj.Add("DATA", jArray);
            }

            jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

            if (DBRelease.createReceipt(jobj, ref jResult))
            {
                gvReceipt.BeginDataUpdate();

                drRecript[0]["EXPORT"] = $"{jResult["EXPORT"]}";

                gvReceipt.EndDataUpdate();

                return true;
            }
            else
            {
                return false;
            }
        }



       


        private void makeRecipt(DataTable dt, Dictionary<int, Dictionary<string, Dictionary<long, int>>> dicConsignedComponent, Dictionary<long, string> dicComponentNm, Dictionary<int, List<long>> dicConsignedType)
        {
            gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;


            gvReceipt.BeginDataUpdate();
            _dtPart.BeginInit();

            List<long> listConsignedType;

            var today = DateTime.Today;
            //if (_isTomorrow)
            //    today = DateTime.Now.AddDays(1);

            int prodctType;
            int guarantee = 12;
            foreach (DataRow rowReceipt in dt.Rows)
            {
                int nId = ConvertUtil.ToInt32(rowReceipt["ID"]);
                if (dicConsignedType.ContainsKey(nId))
                    listConsignedType = dicConsignedType[nId];
                else
                    listConsignedType = new List<long>();

                prodctType = ConvertUtil.ToInt32(rowReceipt["TYPE"]);

                if (prodctType == 1 || prodctType == 3 || prodctType == 4)
                    guarantee = 3;
                else
                    guarantee = 6;

                DataRow dr = _dtReceipt.NewRow();

                dr["NO"] = _dtReceipt.Rows.Count + 1;
                dr["ID"] = _id;
                dr["PROXY_ID"] = -1;
                dr["RECEIPT"] = "";
                dr["RECEIPT_DT"] = rowReceipt["RECEIPT_DT"];
                dr["MODEL_NM"] = rowReceipt["MODEL_NM"];
                dr["PRODUCT_TYPE"] = rowReceipt["TYPE"];
                dr["GUARANTEE"] = $"{guarantee}";
                dr["GUARANTEE_FROM"] = string.Format("{0:d}", today);
                dr["GUARANTEE_TO"] = string.Format("{0:d}", today.AddMonths(guarantee));
                dr["COMPANY_ID"] = ProjectInfo._userCompanyId;
                dr["SALE_ROOT"] = "-1";
                dr["RELEASE_TYPE"] = "1";
                dr["MODEL_NM_DETAIL"] = rowReceipt["MODEL_NM_DETAIL"];
                dr["DES"] = rowReceipt["DES"];
                dr["REQUEST"] = rowReceipt["REQUEST"];
                dr["CHECK"] = rowReceipt["CHECK"];
                dr["ERROR"] = rowReceipt["ERROR"];

                dr["CUSTOMER_NM1"] = rowReceipt["CUSTOMER_NM"];
                dr["CUSTOMER_NM2"] = rowReceipt["CUSTOMER_NM"];
                dr["TEL1"] = rowReceipt["TEL"];
                dr["TEL2"] = rowReceipt["TEL"];
                dr["HP1"] = rowReceipt["HP"];
                dr["HP2"] = rowReceipt["HP"];
                dr["POSTAL_CD"] = rowReceipt["POSTAL_CD"];
                dr["ADDRESS"] = rowReceipt["ADDRESS"];
                dr["ADDRESS_DETAIL"] = "";
                dr["B_GRADE"] = rowReceipt["B_GRADE"];
                
                //임시
                dr["OLD_COA_SN"] = rowReceipt["OLD_COA_SN"];
                dr["NEW_COA_SN"] = rowReceipt["NEW_COA_SN"];
                dr["COUPON_MANAGE"] = rowReceipt["COUPON_MANAGE"];
                dr["MBD_SN"] = rowReceipt["MBD_SN"];
                dr["DELIVERY"] = rowReceipt["DELIVERY"];

                //tlPart.BeginUpdate();

                List<long> listPart = new List<long>();
                List<long> listModel = new List<long>();

                long modelId = ConvertUtil.ToInt64(rowReceipt["MODEL_ID"]);

                listModel.Add(modelId);

                if (modelId > 0)
                {
                    DataRow rRow = _dtPart.NewRow();

                    rRow["ID"] = _id;
                    rRow["P_PART_ID"] = 0;
                    rRow["PART_ID"] = modelId * -1;
                    rRow["COMPONENT_ID"] = -1;
                    rRow["MODEL_ID"] = modelId;
                    rRow["CONSIGNED_TYPE"] = -1;
                    rRow["COMPONENT_CD"] = "MODEL";
                    rRow["COMPONENT_CD_T"] = "MODEL";
                    rRow["MODEL_NM"] = rowReceipt["MODEL_ID_NM"];
                    rRow["PART_CNT"] = 1;

                    _dtPart.Rows.Add(rRow);
                }

                Dictionary<string, Dictionary<long, int>> dicComponent = dicConsignedComponent[nId];

                for (int i = 0; i < _consignedComponetCd.Length; i++)
                {
                    string componentCd = _consignedComponetCd[i];
                    DataRow row = _dtPart.NewRow();

                    row["ID"] = _id;
                    row["P_PART_ID"] = -1;
                    row["PART_ID"] = i;
                    row["COMPONENT_ID"] = -1;
                    row["CONSIGNED_TYPE"] = -1;
                    row["COMPONENT_CD"] = componentCd;
                    row["COMPONENT_CD_T"] = componentCd;
                    //row["MODEL_NM"] = _consignedComponetNm[i];
                    row["MODEL_NM"] = "";
                    row["PART_CNT"] = 0;

                    _dtPart.Rows.Add(row);

                    if(dicComponent.ContainsKey(componentCd))
                    {
                        Dictionary<long, int> dicComp = dicComponent[componentCd];
                        long partId = _dicConsignedComponentCdReverse[componentCd];
                        int totalCnt = 0;
                        foreach (KeyValuePair<long, int> item in dicComp)
                        {
                            long compId = item.Key;
                            int cnt = item.Value;
                            totalCnt += cnt;
                            listPart.Add(compId);


                            DataRow drComp = _dtPart.NewRow();

                            drComp["ID"] = _id;
                            drComp["P_PART_ID"] = partId;
                            drComp["PART_ID"] = compId;
                            drComp["COMPONENT_ID"] = compId;
                            if(listConsignedType.Contains(compId))
                                drComp["CONSIGNED_TYPE"] = 2;
                            else
                                drComp["CONSIGNED_TYPE"] = 1;
                            //dr["COMPONENT_CD"] = _currentComponentPart["COMPONENT_CD"];
                            drComp["COMPONENT_CD_T"] = componentCd;
                            drComp["COMPONENT_CD"] = "";
                            drComp["MODEL_NM"] = dicComponentNm[compId];
                            drComp["PART_CNT"] = cnt;

                            _dtPart.Rows.Add(drComp);
                        }
       
                        DataRow[] rows = _dtPart.Select($"ID = {_id} AND PART_ID = {partId} AND P_PART_ID = -1");

                        foreach (DataRow rowPart in rows)
                            rowPart["PART_CNT"] = totalCnt;
                    }
                }
                if (modelId > 0)
                {
                    DataRow[] rowsModel = _dtPart.Select($"ID = {_id} AND PART_ID = 0 AND P_PART_ID = -1");

                    foreach (DataRow rowPart in rowsModel)
                        rowPart["PART_CNT"] = 1;
                }

                _dtReceipt.Rows.Add(dr);
                //_dicReceiptPart.Add(_id, listPart);
                //_dicConsignedModel.Add(_id, listModel);

                _id++;
            }

            _dtPart.EndInit();
            gvReceipt.EndDataUpdate();

            gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
           
            gvReceipt.FocusedRowHandle = -2147483646;
            gvReceipt.MoveLast();
            //tlPart.ExpandAll();
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

            _dtProduct.Clear();
            _dtPart.Clear();

            _dicReceiptPart.Remove(_CurrentId);
            _dicReceiptProduct.Remove(_CurrentId);
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
            DataTable dt;
            DataTable dtRemain;

            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -2147483646;
            gvPart.FocusedRowHandle = rowhandle;

            if (lgcReceiptList.CustomHeaderButtons[4].Properties.Checked)
            {
                dt = _dtProduct;
                dtRemain = _dtPart;
            }
            else
            {
                dt = _dtPart;
                dtRemain = _dtProduct;
            }

            gvPart.BeginDataUpdate();
            gvPartDetail.BeginDataUpdate();

            DataRow[] rows = dt.Select($"CHECK = TRUE");
            DataRow[] rowsRemain;
            DataTable dtPartDetail;
            long inventoryId;
            List<long> listInventoryId = _dicReceiptInventoryId[_CurrentId];
            List<long> listProductId = _dicReceiptProductId[_CurrentId];

            dt.BeginInit();
            foreach (DataRow row in rows)
            {
                inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                rowsRemain = dtRemain.Select($"INVENTORY_ID = {inventoryId}");

                if (rowsRemain.Length > 0)
                {
                    dtRemain.BeginInit();
                    foreach (DataRow dr in rowsRemain)
                    {
                        dr.Delete();
                    }
                    dtRemain.EndInit();
                }

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
            dt.EndInit();

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

        private void tlPart_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e != null && e.Node != null && e.Node["PART_ID"] != null)
            {
                Color backColor = Color.Transparent;
                Color foreColor = Color.Black;

                if (!String.IsNullOrEmpty(ConvertUtil.ToString(e.Node["PART_ID"])))
                {
                    long id = ConvertUtil.ToInt64(e.Node["P_PART_ID"]);

                    switch (id)
                    {
                        case -1:
                            backColor = Color.PapayaWhip;
                            foreColor = Color.Black;
                            break;
                        default:
                            backColor = Color.Transparent;
                            foreColor = Color.Black;
                            break;
                    }

                    e.Appearance.BackColor = backColor;
                }
            }
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
                DataTable dt;

                int rowhandle = gvPart.FocusedRowHandle;
                int topRowIndex = gvPart.TopRowIndex;
                gvPart.FocusedRowHandle = -2147483646;
                gvPart.FocusedRowHandle = rowhandle;

                try
                {
                    if (lgcReceiptList.CustomHeaderButtons[4].Properties.Checked)
                        dt = _dtProduct;
                    else
                        dt = _dtPart;

                    gvPart.BeginUpdate();
                    foreach (DataRow row in dt.Rows)
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
            else if (e.Button.Properties.Tag.Equals(2))
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

                gcComponentCd.Visible = false;
                gcProduct.Visible = false;
                gcInventoryState.Visible = false;

                gcProductState.Visible = true;

                for (int i = 0; i < _columnsProduct.Length; i++)
                    _columnsProduct[i].VisibleIndex = i;

                gcPart.DataSource = null;
                gcPart.DataSource = _dtProduct;

                gvPart.EndDataUpdate();
            }

        }

        private void lgcReceiptList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                DataTable dt;

                int rowhandle = gvPart.FocusedRowHandle;
                int topRowIndex = gvPart.TopRowIndex;
                gvPart.FocusedRowHandle = -2147483646;
                gvPart.FocusedRowHandle = rowhandle;

                gvPart.BeginDataUpdate();

                if (lgcReceiptList.CustomHeaderButtons[4].Properties.Checked)
                    dt = _dtProduct;
                else
                    dt = _dtPart;

                foreach (DataRow row in dt.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
                gvPart.EndDataUpdate();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvPart.FocusedRowHandle;
                int topRowIndex = gvPart.TopRowIndex;
                gvPart.FocusedRowHandle = -2147483646;
                gvPart.FocusedRowHandle = rowhandle;

                gvPart.BeginDataUpdate();

                foreach (DataRow row in _dtProduct.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                gcComponentCd.Visible = true;
                gcProduct.Visible = true;
                gcInventoryState.Visible = true;

                gcProductState.Visible = false;

                for (int i = 0; i < _columnsPart.Length; i++)
                    _columnsPart[i].VisibleIndex = i;

                gcPart.DataSource = null;
                gcPart.DataSource = _dtPart;

                gvPart.EndDataUpdate();
            }
        }

        private void leComapny_EditValueChanged(object sender, EventArgs e)
        {
            long companyId = ConvertUtil.ToInt64(leComapny.EditValue);

            teCustomerNm.EditValueChanged -= teCustomerNm_EditValueChanged;
            if (companyId == -1)
            {
                teCustomerNm.Text = "";
                teCustomerNm.ReadOnly = false;
            }
            else
            {
                teCustomerNm.Text = leComapny.Text;
                teCustomerNm.ReadOnly = true;
            }
            teCustomerNm.EditValueChanged += teCustomerNm_EditValueChanged;

            gvReceipt.BeginDataUpdate();
            _currentReceipt["CUSTOMER_NM"] = teCustomerNm.Text;
            gvReceipt.EndDataUpdate();

            teTel.Text = "";
            teHp.Text = "";
            tePostalCd.Text = "";
            teAddress.Text = "";
            teAddressDetail.Text = "";

        }

        private void setGridPartInit(bool isProductCheck)
        {
            //int rowhandle = gvPart.FocusedRowHandle;
            //int topRowIndex = gvPart.TopRowIndex;
            //gvPart.FocusedRowHandle = -2147483646;
            //gvPart.FocusedRowHandle = rowhandle;

            gvPart.BeginDataUpdate();

            _dtProduct.BeginInit();
            foreach (DataRow row in _dtProduct.Rows)
                row["CHECK"] = false;
            _dtProduct.EndInit();

            _dtPart.BeginInit();
            foreach (DataRow row in _dtPart.Rows)
                row["CHECK"] = false;
            _dtPart.EndInit();

            gcComponentCd.Visible = !isProductCheck;
            gcProduct.Visible = !isProductCheck;
            gcInventoryState.Visible = !isProductCheck;

            gcProductState.Visible = isProductCheck;

            if(isProductCheck)
                for (int i = 0; i < _columnsProduct.Length; i++)
                    _columnsProduct[i].VisibleIndex = i;
            else
                for (int i = 0; i < _columnsPart.Length; i++)
                    _columnsPart[i].VisibleIndex = i;

            gvPart.EndDataUpdate();
        }

        private void teCustomerNm_EditValueChanged(object sender, EventArgs e)
        {
            gvReceipt.BeginDataUpdate();
            _currentReceipt["CUSTOMER_NM"] = teCustomerNm.Text;
            gvReceipt.EndDataUpdate();
        }

        private void deReceiptDt_EditValueChanged(object sender, EventArgs e)
        {
            gvReceipt.BeginDataUpdate();
            _currentReceipt["RECEIPT_DT"] = deReceiptDt.Text;
            gvReceipt.EndDataUpdate();
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
    }
}