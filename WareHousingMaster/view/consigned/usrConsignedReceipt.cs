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

namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedReceipt : DevExpress.XtraEditors.XtraForm
    {
        string _componentCd;

        DataRowView _currentReceipt;
        TreeListNode _currentReceiptPart;
        DataRowView _currentComponentPart;

        DataTable _dtReceipt;
        DataTable _dtReceiptPart;
        DataTable _dtComponentList;

        BindingSource _bsReceipt;
        BindingSource _bsReceiptPart;
        BindingSource _bsComponentList;

        Dictionary<long, List<long>> _dicReceiptPart;
        Dictionary<long, List<long>> _dicConsignedModel;

        string[] _consignedComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "SSD", "HDD", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG" };
        string[] _consignedComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "SSD", "HDD", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스" };

        Dictionary<int, string> _dicConsignedComponentCd;
        Dictionary<string, int> _dicConsignedComponentCdReverse;

        string _currentGetComponentCd;

        Dictionary<string, string> _dicProductType;
        Dictionary<string, string> _dicGuarantee;

        long _proxyId;
        long _id;

        int _currnetPartCnt=0;

        bool _isTomorrow;



        public usrConsignedReceipt()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
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
            _dtReceipt.Columns.Add(new DataColumn("B_GRADE", typeof(short)));
            _dtReceipt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtReceipt.Columns.Add(new DataColumn("ERROR", typeof(string)));

            //임시 사용
            _dtReceipt.Columns.Add(new DataColumn("OLD_COA_SN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("NEW_COA_SN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COUPON_MANAGE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MBD_SN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DELIVERY", typeof(string)));


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

            _dtComponentList = new DataTable();
            _dtComponentList.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("VISIBLE", typeof(bool)));


            _bsReceipt = new BindingSource();
            _bsReceiptPart = new BindingSource();
            _bsComponentList = new BindingSource();

            _dicReceiptPart = new Dictionary<long, List<long>>();
            _dicConsignedModel = new Dictionary<long, List<long>>();

            _dicProductType = new Dictionary<string, string>();
            _dicGuarantee = new Dictionary<string, string>();
            _id = 0;

            _dicConsignedComponentCd = new Dictionary<int, string>();
            _dicConsignedComponentCdReverse = new Dictionary<string, int>();

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                _dicConsignedComponentCd.Add(i, _consignedComponetCd[i]);
                _dicConsignedComponentCdReverse.Add(_consignedComponetCd[i], i);
            }

            _currentGetComponentCd = null;
        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            teReceipt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT", false, DataSourceUpdateMode.OnPropertyChanged));
            leProductType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "PRODUCT_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            deReceiptDt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT_DT", false, DataSourceUpdateMode.OnPropertyChanged));
            leGuarantee.DataBindings.Add(new Binding("EditValue", _bsReceipt, "GUARANTEE", false, DataSourceUpdateMode.OnPropertyChanged));
            deGuaranteeFrom.DataBindings.Add(new Binding("Text", _bsReceipt, "GUARANTEE_FROM", false, DataSourceUpdateMode.OnPropertyChanged));
            deGuaranteeTo.DataBindings.Add(new Binding("Text", _bsReceipt, "GUARANTEE_TO", false, DataSourceUpdateMode.OnPropertyChanged));
            leComapny.DataBindings.Add(new Binding("EditValue", _bsReceipt, "COMPANY_ID", false, DataSourceUpdateMode.OnPropertyChanged));
            leSaleRoot.DataBindings.Add(new Binding("EditValue", _bsReceipt, "SALE_ROOT", false, DataSourceUpdateMode.OnPropertyChanged));
            rgReleaseType.DataBindings.Add(new Binding("EditValue", _bsReceipt, "RELEASE_TYPE", false, DataSourceUpdateMode.OnPropertyChanged));
            teModelNmDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "MODEL_NM_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));

            meDes.DataBindings.Add(new Binding("Text", _bsReceipt, "DES", false, DataSourceUpdateMode.OnPropertyChanged));
            teRequest.DataBindings.Add(new Binding("Text", _bsReceipt, "REQUEST", false, DataSourceUpdateMode.OnPropertyChanged));
            ceWarning.DataBindings.Add(new Binding("EditValue", _bsReceipt, "CHECK", false, DataSourceUpdateMode.OnPropertyChanged));
            teError.DataBindings.Add(new Binding("EditValue", _bsReceipt, "ERROR", false, DataSourceUpdateMode.OnPropertyChanged));

            teCustomerNm1.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM1", false, DataSourceUpdateMode.OnPropertyChanged));
            teCustomerNm2.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM2", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel1.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL1", false, DataSourceUpdateMode.OnPropertyChanged));
            teTel2.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL2", false, DataSourceUpdateMode.OnPropertyChanged));
            teHp1.DataBindings.Add(new Binding("Text", _bsReceipt, "HP1", false, DataSourceUpdateMode.OnPropertyChanged));
            teHp2.DataBindings.Add(new Binding("Text", _bsReceipt, "HP1", false, DataSourceUpdateMode.OnPropertyChanged));
            tePostalCd.DataBindings.Add(new Binding("Text", _bsReceipt, "POSTAL_CD", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddress.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS", false, DataSourceUpdateMode.OnPropertyChanged));
            teAddressDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS_DETAIL", false, DataSourceUpdateMode.OnPropertyChanged));
            ceBGrade.DataBindings.Add(new Binding("EditValue", _bsReceipt, "B_GRADE", false, DataSourceUpdateMode.OnPropertyChanged));

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
            Util.LookupEditHelper(leComapny, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");

            DataTable dtSaleRoot = new DataTable();
            dtSaleRoot = Util.getCodeList("CD0905", "KEY", "VALUE");
            Util.LookupEditHelper(leSaleRoot, dtSaleRoot, "KEY", "VALUE");

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

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceipt.DataSource = _dtReceipt;
            _bsReceiptPart.DataSource = _dtReceiptPart;
            _bsComponentList.DataSource = _dtComponentList;

            leCompany.EditValue = ConvertUtil.ToString(ProjectInfo._userCompanyId);

            if (ProjectInfo._userType.Equals("E"))
            {
                leCompany.ReadOnly = true;
            }
        }

        private void setGridControl()
        {
            gcReceip.DataSource = null;
            gcReceip.DataSource = _bsReceipt;

            tlPart.DataSource = null;
            tlPart.DataSource = _bsReceiptPart;

            gcComponentList.DataSource = null;
            gcComponentList.DataSource = _bsComponentList;
        }

        private void leGuarantee_EditValueChanged(object sender, EventArgs e)
        {
            int month = ConvertUtil.ToInt32(leGuarantee.EditValue);
            var today = DateTime.Today;
            var futureDate = today.AddMonths(month);

            deGuaranteeFrom.EditValue = today;
            deGuaranteeTo.EditValue = futureDate;

        }


        private void gvReceipt_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvReceipt.RowCount > 0);
            _dtComponentList.Clear();

            if (isValidRow)
            {
                _currentReceipt = e.Row as DataRowView;
                setReceiptPart(_currentReceipt["ID"]);

                if (ConvertUtil.ToBoolean(_currentReceipt["CHECK"]))
                    teError.Text = "";

                tlPart.ExpandAll();

            }
            else
            {
                _currentReceipt = null;
                _dtComponentList.Clear();
               
            }
        }

        private void tlPart_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _currentReceiptPart = tlPart.FocusedNode;
            //_dtComponentList.Clear();

            if (_currentReceiptPart != null)
            {
                _currnetPartCnt = ConvertUtil.ToInt32(_currentReceiptPart["PART_CNT"]);
                long componentId = ConvertUtil.ToInt64(_currentReceiptPart["COMPONENT_ID"]);
                if (tlPart.IsRootNode(_currentReceiptPart) || componentId < 0)
                    tlPartCnt.OptionsColumn.ReadOnly = true;
                else
                    tlPartCnt.OptionsColumn.ReadOnly = false;
            }
            else
            {
                _currnetPartCnt = 0;
                tlPartCnt.OptionsColumn.ReadOnly = true;
            }
        }

        private void gvComponentList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvComponentList.RowCount > 0);

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
            tlPart.BeginUpdate();
            _bsReceiptPart.Filter = $"ID = {id}";
            tlPart.EndUpdate();
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
            }
            else if (e.Button.Properties.Tag.Equals(5))
            {
                //using (OpenFileDialog file = new OpenFileDialog())
                //{
                //    file.Filter = "Excel 통합문서|*.xlsx;*.xls";

                //    file.Multiselect = false; // 파일 다중 선택

                //    if (file.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                //    {
                //        string fileNm = "";
                //        string filePath = "";

                //        foreach (string data in file.FileNames)
                //        {
                //            string[] fileNms = data.Split('\\');
                //            filePath = data;
                //            fileNm = fileNms[fileNms.Length - 1];
                //        }

                //        DataTable dt = ExcelUtil.getDataTableFromExcel(fileNm, filePath);


                //        gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
                //        tlPart.FocusedNodeChanged -= tlPart_FocusedNodeChanged;

                //        AddReceiptByExcel(dt);

                //        gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
                //        tlPart.FocusedNodeChanged += tlPart_FocusedNodeChanged;

                //        gvReceipt.MoveLast();

                //        var today = DateTime.Today;

                //        deReceiptDt.EditValue = today;
                //        deGuaranteeFrom.EditValue = today;
                //        deGuaranteeTo.EditValue = today;


                //    }
                //}

                using (DlgImportConsignedReceipt dlgReceip = new DlgImportConsignedReceipt(
                    //_dtReceipt,
                    //_dtReceiptPart,
                    _dicProductType,
                    _dicGuarantee,
                    _consignedComponetCd,
                    _dicConsignedComponentCdReverse,
                    _dicReceiptPart,
                    _dicConsignedModel,
                    gvReceipt,
                    _id,
                    _dtReceipt.Rows.Count
                    ))
                {

                    //dlgReceip.receiptChangeEvent += new DlgImportConsignedReceipt.receiptChangeHandler(makeRecipt);

                    gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
                    tlPart.FocusedNodeChanged -= tlPart_FocusedNodeChanged;
                    tlPart.NodeCellStyle -= tlPart_NodeCellStyle;
                    //tlPart.BeginUpdate();
                    //tlPart.DataSource = null;
                    dlgReceip.ShowDialog();
                    //tlPart.DataSource = _dtReceiptPart;
                    //tlPart.EndUpdate();




                    if (dlgReceip._isSuccess)
                    {

                        //makeRecipt(dlgReceip._dtExcel);
                        _id = dlgReceip._id;



                        gvReceipt.BeginDataUpdate();
                        _dtReceipt.Merge(dlgReceip._dtReceipt);
                        gvReceipt.EndDataUpdate();

                        tlPart.BeginUpdate();
                        foreach (DataRow row in dlgReceip._dtReceiptPart.Rows)
                        {
                            DataRow rowComp = _dtReceiptPart.NewRow();

                            for (int i = 0; i < _dtReceiptPart.Columns.Count; i++)
                            {
                                rowComp[i] = row[i];
                            }

                            _dtReceiptPart.Rows.Add(rowComp);
                        }
                        ////_dtReceiptPart.Merge(dlgReceip._dtReceiptPart);
                        tlPart.EndUpdate();
                        gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
                        tlPart.FocusedNodeChanged += tlPart_FocusedNodeChanged;
                        tlPart.NodeCellStyle += tlPart_NodeCellStyle;

                        gvReceipt.MoveLast();
                    }
                    else
                    {
                        //gvReceipt.EndDataUpdate();
                        //gvReceipt.EndDataUpdate();
                        gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
                        tlPart.FocusedNodeChanged += tlPart_FocusedNodeChanged;
                        tlPart.NodeCellStyle += tlPart_NodeCellStyle;
                    }

                    //dlgReceip.receiptChangeEvent -= new DlgImportConsignedReceipt.receiptChangeHandler(makeRecipt);



                }
            }
            else if (e.Button.Properties.Tag.Equals(6))
            {
                using (DlgImportConsignedReceiptByModel dlgReceip1 = new DlgImportConsignedReceiptByModel(
                     _dicProductType,
                    _dicGuarantee,
                    _consignedComponetCd,
                    _dicConsignedComponentCdReverse,
                    _dicReceiptPart,
                    _dicConsignedModel,
                    gvReceipt,
                    _id,
                    _dtReceipt.Rows.Count
                    ))
                {

                    dlgReceip1._companyId = ConvertUtil.ToInt64(leCompany.EditValue);
                    dlgReceip1.ShowDialog();

                    if (dlgReceip1._isSuccess)
                    {
                        _isTomorrow = dlgReceip1._isTomorrow;
                        makeRecipt(dlgReceip1._dtReceipt, dlgReceip1._dicConsignedComponent, dlgReceip1._dicComponentNm, dlgReceip1._dicConsignedType);
                    }
                

                }
            }
        }

   
        private void makeRecipt(DataTable dt)
        {

            //gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
            //tlPart.FocusedNodeChanged -= tlPart_FocusedNodeChanged;
            ////gvReceipt.BeginDataUpdate();
            //AddReceiptByExcel1(dt);

            //gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
            //tlPart.FocusedNodeChanged += tlPart_FocusedNodeChanged;

            //gvReceipt.MoveLast();
        }



        private void lgcReceiptList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if(_currentReceiptPart == null)
            {
                Dangol.Message("선택된 항목이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                TreeListNode rootNode = null;
                if (tlPart.IsRootNode(_currentReceiptPart))
                {
                    rootNode = _currentReceiptPart;
                }
                else
                {
                    rootNode = _currentReceiptPart.RootNode;
                }

                string componentCd = ConvertUtil.ToString(rootNode["COMPONENT_CD"]);

                if(componentCd.Equals("MODEL"))
                    getModelList(ConvertUtil.ToInt64(_currentReceipt["ID"]), ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"]), componentCd);
                else
                    getComponentList(ConvertUtil.ToInt64(_currentReceipt["ID"]), ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"]), componentCd);

                _currentGetComponentCd = componentCd;

            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (tlPart.IsRootNode(_currentReceiptPart))
                {
                    Dangol.Message("부품 정보를 선택해 주세요.");
                }
                else
                {
                    string componentCd = ConvertUtil.ToString(_currentReceiptPart["COMPONENT_CD_T"]);

                    tlPart.BeginUpdate();
                    _dtReceiptPart.BeginInit();

                    int partCnt = ConvertUtil.ToInt32(_currentReceiptPart.RootNode["PART_CNT"]);
                    partCnt -= ConvertUtil.ToInt32(_currentReceiptPart["PART_CNT"]);
                    _currentReceiptPart.RootNode["PART_CNT"] = partCnt;

                    string refType = "COMPONENT_ID";

                    if (componentCd.Equals("MODEL"))
                    {
                        refType = "MODEL_ID";
                        List<long> listModel = _dicConsignedModel[ConvertUtil.ToInt64(_currentReceipt["ID"])];
                        listModel.Remove(ConvertUtil.ToInt64(_currentReceiptPart["MODEL_ID"]));

                        gvReceipt.BeginDataUpdate();
                        _currentReceipt["MODEL_NM"] = "";
                        gvReceipt.EndDataUpdate();
                    }              
                    else
                    {
                        refType = "COMPONENT_ID";
                        List<long> listPart = _dicReceiptPart[ConvertUtil.ToInt64(_currentReceipt["ID"])];
                        listPart.Remove(ConvertUtil.ToInt64(_currentReceiptPart["COMPONENT_ID"]));
                    }

                    if (!string.IsNullOrEmpty(_currentGetComponentCd) && _currentGetComponentCd.Equals(componentCd))
                    {
                        gvComponentList.BeginDataUpdate();
                        DataRow[] rows = _dtComponentList.Select($"COMPONENT_ID = {_currentReceiptPart[refType]}");
                        foreach (DataRow row in rows)
                            row["VISIBLE"] = true;

                        gvComponentList.EndDataUpdate();
                    }

                    _currentReceiptPart.Remove();

                    _dtReceiptPart.EndInit();
                    tlPart.EndUpdate();
                }
            }
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
            string componentCd = ConvertUtil.ToString(_currentComponentPart["COMPONENT_CD"]);

            if (componentCd.Equals("MODEL"))
            {
                if (!ConvertUtil.ToBoolean(_currentComponentPart["VISIBLE"]))
                    Dangol.Message("이미 등록된 모델입니다.");
                else
                    addModel();
            }
            else
            {
                if (!ConvertUtil.ToBoolean(_currentComponentPart["VISIBLE"]))
                    Dangol.Message("이미 등록된 부품입니다.");
                else
                    addComponent();
            }
        }

        private void addModel()
        {
            string componentCd = ConvertUtil.ToString(_currentComponentPart["COMPONENT_CD"]);
            long id = ConvertUtil.ToInt64(_currentReceipt["ID"]);

            List<long> listPart = _dicReceiptPart[id];
            List<long> listModel = _dicConsignedModel[id];

            long partId = _dicConsignedComponentCdReverse[componentCd];
            DataRow[] rowRoots = null;
            DataRow rowRoot = null;

            tlPart.BeginUpdate();
            _dtReceiptPart.BeginInit();

            DataRow rRow = _dtReceiptPart.NewRow();

            rRow["ID"] = id;
            rRow["P_PART_ID"] = partId;
            rRow["PART_ID"] = ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]) * -1;
            rRow["COMPONENT_ID"] = -1;
            rRow["MODEL_ID"] = _currentComponentPart["COMPONENT_ID"];
            rRow["CONSIGNED_TYPE"] =-1;
            rRow["COMPONENT_CD"] = "MODEL";
            rRow["COMPONENT_CD_T"] = "MODEL";
            rRow["MODEL_NM"] = _currentComponentPart["MODEL_NM"];
            rRow["PART_CNT"] = 1;

            _dtReceiptPart.Rows.Add(rRow);

            DataRow[] rowsModel = _dtReceiptPart.Select($"ID = {id} AND PART_ID = 0 AND P_PART_ID = -1");

            foreach (DataRow rowPart in rowsModel)
                rowPart["PART_CNT"] = ConvertUtil.ToInt32(rowPart["PART_CNT"])+1;

            gvReceipt.BeginDataUpdate();
            _currentReceipt["MODEL_NM"] = _currentComponentPart["MODEL_NM"];
            gvReceipt.EndDataUpdate();

  
            long partListId = ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]);

            listModel.Add(partListId);

            JObject jResult = new JObject();

            if (DBConsigned.getConsignedModelComponent(partListId, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {

                        long componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                        partId = _dicConsignedComponentCdReverse[ConvertUtil.ToString(obj["COMPONENT_CD"])];

                        rowRoots = _dtReceiptPart.Select($"ID = {_currentReceipt["ID"]} AND PART_ID = {partId} AND P_PART_ID = -1");
                        if (rowRoots.Length < 1)
                            continue;
                        else
                            rowRoot = rowRoots[0];

                        if (listPart.Contains(componentId))
                        {
                            DataRow[] rows = _dtReceiptPart.Select($"ID = {id} AND COMPONENT_ID = {componentId}");

                            foreach(DataRow row in rows)
                            {
                                int cnt = ConvertUtil.ToInt32(row["PART_CNT"]);
                                cnt += ConvertUtil.ToInt32(obj["COMPONENT_CNT"]);
                                row["PART_CNT"] = cnt;

                                int partCnt = ConvertUtil.ToInt32(rowRoot["PART_CNT"]);
                                partCnt += ConvertUtil.ToInt32(obj["COMPONENT_CNT"]);
                                rowRoot["PART_CNT"] = partCnt;
                            }
                        }
                        else
                        {
                            DataRow dr = _dtReceiptPart.NewRow();
                            

                            dr["ID"] = id;
                            dr["P_PART_ID"] = partId;
                            dr["PART_ID"] = componentId;
                            dr["COMPONENT_ID"] = componentId;
                            dr["MODEL_ID"] = _currentComponentPart["COMPONENT_ID"];
                            dr["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                            //dr["COMPONENT_CD"] = _currentComponentPart["COMPONENT_CD"];
                            dr["COMPONENT_CD_T"] = obj["COMPONENT_CD"];
                            dr["COMPONENT_CD"] = "";
                            dr["MODEL_NM"] = $"{obj["MANUFACTURE_NM"]}/{obj["MODEL_NM"]}/{obj["SPEC_NM"]}";
                            dr["PART_CNT"] = obj["COMPONENT_CNT"];

                            _dtReceiptPart.Rows.Add(dr);

                            int partCnt = ConvertUtil.ToInt32(rowRoot["PART_CNT"]);
                            partCnt++;
                            rowRoot["PART_CNT"] = partCnt;

                            listPart.Add(ConvertUtil.ToInt64(obj["COMPONENT_ID"]));
                        }

                    }
                }
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }

            gvComponentList.BeginDataUpdate();
            _currentComponentPart["VISIBLE"] = false;
            gvComponentList.EndDataUpdate();


            _dtReceiptPart.EndInit();
            tlPart.EndUpdate();

            tlPart.ExpandAll();
        }

 
        private void addComponent()
        {
            string componentCd = ConvertUtil.ToString(_currentComponentPart["COMPONENT_CD"]);

            List<long> listPart = _dicReceiptPart[ConvertUtil.ToInt64(_currentReceipt["ID"])];
            listPart.Add(ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]));

            long partId = _dicConsignedComponentCdReverse[componentCd];

            tlPart.BeginUpdate();
            _dtReceiptPart.BeginInit();

            DataRow dr = _dtReceiptPart.NewRow();

            long componentId = ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]);

            dr["ID"] = ConvertUtil.ToInt64(_currentReceipt["ID"]);
            dr["P_PART_ID"] = partId;
            dr["PART_ID"] = _currentComponentPart["COMPONENT_ID"];
            dr["COMPONENT_ID"] = _currentComponentPart["COMPONENT_ID"];
            dr["CONSIGNED_TYPE"] = 1;
            //dr["COMPONENT_CD"] = _currentComponentPart["COMPONENT_CD"];
            dr["COMPONENT_CD_T"] = _currentComponentPart["COMPONENT_CD"];
            dr["COMPONENT_CD"] = "";
            dr["MODEL_NM"] = _currentComponentPart["MODEL_NM"];
            dr["PART_CNT"] = 1;

            _dtReceiptPart.Rows.Add(dr);

            DataRow[] rows = _dtReceiptPart.Select($"ID = {_currentReceipt["ID"]} AND PART_ID = {partId} AND P_PART_ID = -1");

            foreach (DataRow row in rows)
            {
                int partCnt = ConvertUtil.ToInt32(row["PART_CNT"]);
                partCnt++;
                row["PART_CNT"] = partCnt;
            }

            gvComponentList.BeginDataUpdate();
            _currentComponentPart["VISIBLE"] = false;
            gvComponentList.EndDataUpdate();

            _dtReceiptPart.EndInit();
            tlPart.EndUpdate();

            tlPart.ExpandAll();
        }


        private void receiptOne()
        {
            object id = _currentReceipt["ID"];
            JObject jResult = new JObject();

            long proxyId = ConvertUtil.ToInt64(_currentReceipt["PROXY_ID"]);
            if (proxyId >= 0)
            {
                Dangol.Message("이미 처리된 접수입니다.");
                return;
            }

            gvReceipt.BeginDataUpdate();
            if (receipt(id, ref jResult))
            {
                _currentReceipt["RECEIPT"] = jResult["RECEIPT"];
                _currentReceipt["PROXY_ID"] = jResult["PROXY_ID"];
                gvReceipt.EndDataUpdate();

                Dangol.Message("접수되었습니다.");
            }
            else
            {
                _currentReceipt["RECEIPT"] = jResult["MSG"];
                gvReceipt.EndDataUpdate();
                Dangol.Message($"{jResult["MSG"]}");
            }
            

            int rowhandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            gvReceipt.FocusedRowHandle = -1;
            gvReceipt.FocusedRowHandle = rowhandle;

        }

        private void receiptAll()
        {
            gvReceipt.BeginDataUpdate();

            foreach (DataRow row in _dtReceipt.Rows)
            {
                object id = row["ID"];
                JObject jResult = new JObject();

                long proxyId = ConvertUtil.ToInt64(row["PROXY_ID"]);
                if (proxyId >= 0)
                {
                    continue;
                }

               
                if (receipt(id, ref jResult))
                {
                    row["RECEIPT"] = jResult["RECEIPT"];
                    row["PROXY_ID"] = jResult["PROXY_ID"];            
                }
                else
                {
                    row["RECEIPT"] = jResult["MSG"];
                }
            }

            gvReceipt.EndDataUpdate();
            Dangol.Message("접수되었습니다.");
            

            int rowhandle = gvReceipt.FocusedRowHandle;
            int topRowIndex = gvReceipt.TopRowIndex;
            gvReceipt.FocusedRowHandle = -1;
            gvReceipt.FocusedRowHandle = rowhandle;

        }

        private bool receipt(object id, ref JObject jResult)
        {
            JObject jobj = new JObject();

            DataRow[] drRecript = _dtReceipt.Select($"ID = {id}");


            jobj.Add("PC_TYPE", $"{ drRecript[0]["PRODUCT_TYPE"]}");
            jobj.Add("GUARANTEE_DUE", $"{ drRecript[0]["GUARANTEE"]}");
            jobj.Add("GUARANTEE_START", $"{ drRecript[0]["GUARANTEE_FROM"]}");
            jobj.Add("GUARANTEE_END", $"{ drRecript[0]["GUARANTEE_TO"]}");

            jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(drRecript[0]["COMPANY_ID"]));
            jobj.Add("SALE_ROOT", $"{ drRecript[0]["SALE_ROOT"]}");
            jobj.Add("RELEASE_TYPE", $"{ drRecript[0]["RELEASE_TYPE"]}");
            jobj.Add("DES", $"{ drRecript[0]["DES"]}");
            jobj.Add("REQUEST", $"{ drRecript[0]["REQUEST"]}");
            jobj.Add("MODEL_NM_DETAIL", $"{ drRecript[0]["MODEL_NM_DETAIL"]}");

            jobj.Add("CUSTOMER_NM_S", $"{ drRecript[0]["CUSTOMER_NM1"]}");
            jobj.Add("CUSTOMER_NM_R", $"{ drRecript[0]["CUSTOMER_NM2"]}");
            jobj.Add("TEL_S", $"{ drRecript[0]["TEL1"]}");
            jobj.Add("TEL_R", $"{ drRecript[0]["TEL2"]}");
            jobj.Add("MOBILE_S", $"{ drRecript[0]["HP1"]}");
            jobj.Add("MOBILE_R", $"{ drRecript[0]["HP2"]}");
            jobj.Add("POSTAL_CD", $"{ drRecript[0]["POSTAL_CD"]}");
            jobj.Add("ADDRESS", $"{ drRecript[0]["ADDRESS"]}");
            jobj.Add("ADDRESS_DETAIL", $"{ drRecript[0]["ADDRESS_DETAIL"]}");
            jobj.Add("B_GRADE", $"{ drRecript[0]["B_GRADE"]}");
            

            //임시
            jobj.Add("OLD_COA_SN", $"{ drRecript[0]["OLD_COA_SN"]}");
            jobj.Add("NEW_COA_SN", $"{ drRecript[0]["NEW_COA_SN"]}");
            jobj.Add("COUPON_MANAGE", $"{ drRecript[0]["COUPON_MANAGE"]}");
            jobj.Add("MBD_SN", $"{ drRecript[0]["MBD_SN"]}");
            jobj.Add("DELIVERY", $"{ drRecript[0]["DELIVERY"]}");

            jobj.Add("RECEIPT_DT", $"{ drRecript[0]["RECEIPT_DT"]}");

            //jobj.Add("PC_TYPE", $"{ leProductType.EditValue}");
            //jobj.Add("GUARANTEE_DUE", $"{ leGuarantee.EditValue}");
            //jobj.Add("GUARANTEE_START", $"{ deGuaranteeFrom.Text}");
            //jobj.Add("GUARANTEE_END", $"{ deGuaranteeTo.Text}");

            //jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(leComapny.EditValue));
            //jobj.Add("SALE_ROOT", $"{ leSaleRoot.EditValue}");
            //jobj.Add("RELEASE_TYPE", $"{ rgReleaseType.EditValue}");
            //jobj.Add("DES", $"{ meDes.Text}");
            //jobj.Add("REQUEST", $"{ teRequest.Text}");
            //jobj.Add("MODEL_NM_DETAIL", $"{ teModelNmDetail.Text}");

            //jobj.Add("CUSTOMER_NM_S", teCustomerNm1.Text);
            //jobj.Add("CUSTOMER_NM_R", teCustomerNm2.Text);
            //jobj.Add("TEL_S", teTel1.Text);
            //jobj.Add("TEL_R", teTel2.Text);
            //jobj.Add("MOBILE_S", teHp1.Text);
            //jobj.Add("MOBILE_R", teHp2.Text);
            //jobj.Add("POSTAL_CD", tePostalCd.Text);
            //jobj.Add("ADDRESS", teAddress.Text);
            //jobj.Add("ADDRESS_DETAIL", teAddressDetail.Text);

            jobj.Add("COUNT_CHECK", 2);


            DataRow[] rows = _dtReceiptPart.Select($"ID = {id} AND PART_ID <> -1 AND P_PART_ID > 0");

            var jPArt = new JArray();

            int index = 0;
            foreach (DataRow row in rows)
            {
                JObject jdata = new JObject();

                jdata.Add("COMPONENT_ID", ConvertUtil.ToInt64(row["COMPONENT_ID"]));
                jdata.Add("COMPONENT_CD", _dicConsignedComponentCd[ConvertUtil.ToInt32(row["P_PART_ID"])]);
                jdata.Add("COMPONENT_CNT", ConvertUtil.ToInt32(row["PART_CNT"]));
                jdata.Add("CONSIGNED_TYPE", ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]));
                jdata.Add("DISPLAY_SEQ", index);

                jPArt.Add(jdata);
                index++;
            }

            jobj.Add("DATA", jPArt);

            rows = _dtReceiptPart.Select($"ID = {id} AND PART_ID <> -1 AND P_PART_ID = 0");
            long modelId = -1;
            if (rows.Length > 0)
                modelId = ConvertUtil.ToInt64(rows[0]["MODEL_ID"]);

            jobj.Add("MODEL_ID", modelId);

            if (DBConsigned.createReceipt(jobj, ref jResult))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void getModelList(long id, long companyId, string componentCd)
        {
            _dtComponentList.Clear();

            JObject jResult = new JObject();

            if (DBConsigned.getConsignedModelList(companyId, componentCd, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    List<long> listModel = _dicConsignedModel[id];

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtComponentList.NewRow();

                        long modelListId = ConvertUtil.ToInt64(obj["MODEL_LIST_ID"]);
                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = modelListId;
                        dr["COMPONENT_CD"] = _currentReceiptPart["COMPONENT_CD_T"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        if (listModel.Contains(modelListId))
                            dr["VISIBLE"] = false;
                        else
                            dr["VISIBLE"] = true;

                        _dtComponentList.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }
        }

        private void getComponentList(long id, long companyId, string componentCd)
        {
            _dtComponentList.Clear();

            JObject jResult = new JObject();

            if (DBConsigned.getConsignedComponentList(companyId, componentCd, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    List<long> listPart = _dicReceiptPart[id];

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtComponentList.NewRow();

                        long componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = componentId;
                        dr["COMPONENT_CD"] = _currentReceiptPart["COMPONENT_CD_T"];
                        dr["MODEL_NM"] = obj["REP_NAME"];
                        if (listPart.Contains(componentId))
                            dr["VISIBLE"] = false;
                        else
                            dr["VISIBLE"] = true;

                        _dtComponentList.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }
        }

        private void AddReceipt()
        {
            var today = DateTime.Today;
            int hour = ConvertUtil.ToInt32(DateTime.Now.ToString("HH"));

            if (hour > 16)
                today = DateTime.Now.AddDays(1);

            string receiptDt = today.ToString("yyyy-MM-dd");

            gvReceipt.BeginDataUpdate();

            DataRow dr = _dtReceipt.NewRow();

            dr["NO"] = _dtReceipt.Rows.Count+1;
            dr["ID"] = _id;
            dr["PROXY_ID"] = -1;
            dr["RECEIPT"] = "";
            dr["RECEIPT_DT"] = receiptDt;
            dr["MODEL_NM"] = "";
            dr["PRODUCT_TYPE"] = "-1";
            dr["GUARANTEE"] = "12";
            dr["GUARANTEE_FROM"] = string.Format("{0:d}", DateTime.Today);
            dr["GUARANTEE_TO"] = string.Format("{0:d}", DateTime.Today.AddMonths(12));
            dr["COMPANY_ID"] = leCompany.EditValue;
            dr["SALE_ROOT"] = "-1";
            dr["RELEASE_TYPE"] = "-1";
            dr["MODEL_NM_DETAIL"] = "";
            dr["DES"] = "";
            dr["CHECK"] = true;
            dr["ERROR"] = "";

            dr["CUSTOMER_NM1"] = "";
            dr["CUSTOMER_NM2"] = "";
            dr["TEL1"] = "";
            dr["TEL2"] = "";
            dr["HP1"] = "";
            dr["HP2"] = "";
            dr["POSTAL_CD"] = "";
            dr["ADDRESS"] = "";
            dr["ADDRESS_DETAIL"] = "";
            dr["B_GRADE"] = (short)0;
            

            //tlPart.BeginUpdate();
            _dtReceiptPart.BeginInit();

            for (int i = 0; i < _consignedComponetCd.Length; i++) {

                DataRow row = _dtReceiptPart.NewRow();

                row["ID"] = _id;
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

            _dtReceiptPart.EndInit();
            //tlPart.EndUpdate();

            _dtReceipt.Rows.Add(dr);

            gvReceipt.EndDataUpdate();

            List<long> listPart = new List<long>();
            _dicReceiptPart.Add(_id, listPart);

            List<long> listModel = new List<long>();
            _dicConsignedModel.Add(_id, listModel);
            _id++;

            gvReceipt.MoveLast();

           
            //var today = DateTime.Today;

            //deReceiptDt.EditValue = today;
            //deGuaranteeFrom.EditValue = today;
            //deGuaranteeTo.EditValue = today;
        }

        private void makeRecipt(DataTable dt, Dictionary<int, Dictionary<string, Dictionary<long, int>>> dicConsignedComponent, Dictionary<long, string> dicComponentNm, Dictionary<int, List<long>> dicConsignedType)
        {
            gvReceipt.FocusedRowObjectChanged -= gvReceipt_FocusedRowObjectChanged;
            tlPart.FocusedNodeChanged -= tlPart_FocusedNodeChanged;
            tlPart.NodeCellStyle -= tlPart_NodeCellStyle;

            gvReceipt.BeginDataUpdate();
            _dtReceiptPart.BeginInit();

            List<long> listConsignedType;

            var today = DateTime.Today;
            if (_isTomorrow)
                today = DateTime.Now.AddDays(1);

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
                dr["COMPANY_ID"] = leCompany.EditValue;
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
                    DataRow rRow = _dtReceiptPart.NewRow();

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

                    _dtReceiptPart.Rows.Add(rRow);
                }

                if (dicConsignedComponent.ContainsKey(nId))
                {
                    Dictionary<string, Dictionary<long, int>> dicComponent = dicConsignedComponent[nId];

                    for (int i = 0; i < _consignedComponetCd.Length; i++)
                    {
                        string componentCd = _consignedComponetCd[i];
                        DataRow row = _dtReceiptPart.NewRow();

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

                        _dtReceiptPart.Rows.Add(row);

                        if (dicComponent.ContainsKey(componentCd))
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


                                DataRow drComp = _dtReceiptPart.NewRow();

                                drComp["ID"] = _id;
                                drComp["P_PART_ID"] = partId;
                                drComp["PART_ID"] = compId;
                                drComp["COMPONENT_ID"] = compId;
                                if (listConsignedType.Contains(compId))
                                    drComp["CONSIGNED_TYPE"] = 2;
                                else
                                    drComp["CONSIGNED_TYPE"] = 1;
                                //dr["COMPONENT_CD"] = _currentComponentPart["COMPONENT_CD"];
                                drComp["COMPONENT_CD_T"] = componentCd;
                                drComp["COMPONENT_CD"] = "";
                                drComp["MODEL_NM"] = dicComponentNm[compId];
                                drComp["PART_CNT"] = cnt;

                                _dtReceiptPart.Rows.Add(drComp);
                            }

                            DataRow[] rows = _dtReceiptPart.Select($"ID = {_id} AND PART_ID = {partId} AND P_PART_ID = -1");

                            foreach (DataRow rowPart in rows)
                                rowPart["PART_CNT"] = totalCnt;
                        }
                    }
                }

                if (modelId > 0)
                {
                    DataRow[] rowsModel = _dtReceiptPart.Select($"ID = {_id} AND PART_ID = 0 AND P_PART_ID = -1");

                    foreach (DataRow rowPart in rowsModel)
                        rowPart["PART_CNT"] = 1;
                }

                _dtReceipt.Rows.Add(dr);
                _dicReceiptPart.Add(_id, listPart);
                _dicConsignedModel.Add(_id, listModel);

                _id++;
            }

            _dtReceiptPart.EndInit();
            gvReceipt.EndDataUpdate();

            gvReceipt.FocusedRowObjectChanged += gvReceipt_FocusedRowObjectChanged;
            tlPart.FocusedNodeChanged += tlPart_FocusedNodeChanged;
            tlPart.NodeCellStyle += tlPart_NodeCellStyle;

            gvReceipt.FocusedRowHandle = -2147483646;
            gvReceipt.MoveLast();
            //tlPart.ExpandAll();
        }



        private void DeleteReceipt()
        {
            gvReceipt.BeginDataUpdate();

            _dicReceiptPart.Remove(ConvertUtil.ToInt64(_currentReceipt["ID"]));
            _dicConsignedModel.Remove(ConvertUtil.ToInt64(_currentReceipt["ID"]));

            DataRow[] rows = _dtReceiptPart.Select($"ID = {_currentReceipt["ID"]}");

            tlPart.BeginUpdate();
            _dtReceiptPart.BeginInit();

            foreach(DataRow row in rows)
                row.Delete();

            _dtReceiptPart.EndInit();
            tlPart.EndUpdate();
     
            _currentReceipt.Delete();

            gvReceipt.EndDataUpdate();

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


        private void risePartCnt_EditValueChanged(object sender, EventArgs e)
        {
            tlPart.BeginUpdate();

            SpinEdit editor = (SpinEdit)sender;
            int partCnt = ConvertUtil.ToInt32(editor.EditValue);

            TreeListNode node = _currentReceiptPart.RootNode;
            int cnt = ConvertUtil.ToInt32(node["PART_CNT"]);
            cnt -= _currnetPartCnt;
            cnt += partCnt;
            _currnetPartCnt = partCnt;
            _currentReceiptPart["PART_CNT"] = partCnt;
            node["PART_CNT"] = cnt;
            tlPart.EndUpdate();
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

    }
}