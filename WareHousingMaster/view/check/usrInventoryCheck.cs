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

namespace WareHousingMaster.view.check
{
    public partial class usrInventoryCheck : DevExpress.XtraEditors.XtraForm
    {

        string _representativeType = "W";
        string _representativeCol = "WAREHOUSING";
        string _representativeIdCol = "WAREHOUSING_ID";
        string _representativeNo = null;
        
        long _representativeId = -1;
        long _inventoryId = -1;
        object _companyId = -1;
        long _type = 1;
        string _componentCd = "ALL";

        DataRowView _currentWarehousing;
        DataRowView _currentComponent;
        DataRowView _currentInventory;

        DataTable _dtWarehousing;
        DataTable _dtWarehousingComponent;
        DataTable _dtWarehousingPart;
        DataTable _dtWarehousingInvnetory;

        DataTable _dtPallet;

        BindingSource _bs;
        BindingSource _bsWarehousingComponent;
        BindingSource _bsWarehousingInvnetory;
        BindingSource _bsPallet;
        BindingSource _bsWarehousing;

        Dictionary<string, int> _dicDataCheck = null;
        Dictionary<string, int> _dicDataCheckHistory = null;
        DataTable _dtAdjustmentPrice = null;

        Dictionary<string, long> _dicAdjustmentPrice = null;

        string _etcDes = "";
        string _batteryRemain = "";
        string _productGrade = "";
        string _monSize = "";
        string _repairContent = "";

        string _etcDesHistory = "";
        string _batteryRemainHistory = "";
        string _productGradeHistory = "";
        string _monSizeHistory = "";
        string _repairContentHistory = "";

        short _checkType = 1;
        string _barcode = null;

        bool _isCheckExist = false;
        bool _isAdjustmentExist = false;

        bool _isCheckExistHistory = false;
        bool _isAdjustmentExistHistory = false;

        DataTable _dtPrintPort;
        DataTable _dtPGrade;

        public usrInventoryCheck()
        {
            InitializeComponent();

            _dtWarehousing = new DataTable();
            _dtWarehousing.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_STATE", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));

            _dtWarehousingComponent = new DataTable();

            _dtWarehousingComponent.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("WAREHOUSING_CNT", typeof(int)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("INVENTORY_CNT", typeof(int)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("GOOD_CNT", typeof(int)));
            _dtWarehousingComponent.Columns.Add(new DataColumn("FAULT_CNT", typeof(int)));        
            _dtWarehousingComponent.Columns.Add(new DataColumn("RELEASE_CNT", typeof(int)));

            _dtWarehousingInvnetory = new DataTable();

            _dtWarehousingInvnetory.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("INIT_PRICE", typeof(long)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("ADJUST_PRICE", typeof(long)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("ADJUST_DES", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("WAREHOUSE", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("PALLET", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("CHECK_YN", typeof(string)));
            _dtWarehousingInvnetory.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _dtWarehousingPart = new DataTable();

            _dtWarehousingPart.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtWarehousingPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtWarehousingPart.Columns.Add(new DataColumn("P_PART_ID", typeof(long)));
            _dtWarehousingPart.Columns.Add(new DataColumn("PART_ID", typeof(long)));
            _dtWarehousingPart.Columns.Add(new DataColumn("TYPE_CD", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtWarehousingPart.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("INIT_PRICE", typeof(long)));
            _dtWarehousingPart.Columns.Add(new DataColumn("ADJUST_PRICE", typeof(long)));
            _dtWarehousingPart.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtWarehousingPart.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("ECT", typeof(string)));

            _dtWarehousingPart.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("LOCATION", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("PALLET", typeof(string)));
            _dtWarehousingPart.Columns.Add(new DataColumn("USER_ID", typeof(string)));


            _bsWarehousingComponent = new BindingSource();
            _bsWarehousingInvnetory = new BindingSource();
            _bsWarehousing = new BindingSource();
            _bs = new BindingSource();
            _bsPallet = new BindingSource();

            _bs.DataSource = _dtWarehousingPart;

            _dicDataCheck = new Dictionary<string, int>(); 
            _dicDataCheckHistory = new Dictionary<string, int>();

            _dtAdjustmentPrice = new DataTable();

            _dicAdjustmentPrice = new Dictionary<string, long>();

        }

        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            teBarcode.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "BARCODE", false, DataSourceUpdateMode.Never));
            teComponent.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "COMPONENT", false, DataSourceUpdateMode.Never));
            leLocation.DataBindings.Add(new Binding("EditValue", _bsWarehousingInvnetory, "WAREHOUSE", false, DataSourceUpdateMode.Never));
            lePallet.DataBindings.Add(new Binding("EditValue", _bsWarehousingInvnetory, "PALLET", false, DataSourceUpdateMode.Never));
            teUserName.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "USER_ID", false, DataSourceUpdateMode.Never));

            leInventoryCat.DataBindings.Add(new Binding("EditValue", _bsWarehousingInvnetory, "INVENTORY_CAT", false, DataSourceUpdateMode.Never));
            seInitPrice.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "INIT_PRICE", false, DataSourceUpdateMode.Never));
            seAdjustPrice.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "ADJUST_PRICE", false, DataSourceUpdateMode.Never));
            meAdjustDes.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "ADJUST_DES", false, DataSourceUpdateMode.Never));
            sePrice.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "PRICE", false, DataSourceUpdateMode.Never));
            seReleasePrice.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "RELEASE_PRICE", false, DataSourceUpdateMode.Never));
            meDes.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "DES", false, DataSourceUpdateMode.Never));

            JObject jResult = new JObject();
            getWarehousingList(ref jResult);

        }

        private void setInfoBox()
        {
            _dtPrintPort = new DataTable();

            _dtPrintPort.Columns.Add(new DataColumn("KEY", typeof(string)));
            _dtPrintPort.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = ProjectInfo._startPort; i < ProjectInfo._endPort; i++)
            {
                DataRow dr = _dtPrintPort.NewRow();

                dr["KEY"] = i;
                dr["VALUE"] = i;
                _dtPrintPort.Rows.Add(dr);
            }

            Util.LookupEditHelper(lePrintPort, _dtPrintPort, "KEY", "VALUE");

            DataTable dtCheckTypet = new DataTable();

            dtCheckTypet.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCheckTypet.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtCheckTypet, 3, " 리페어");
            Util.insertRowonTop(dtCheckTypet, 2, " 출고");
            Util.insertRowonTop(dtCheckTypet, 1, " 입고");

            Util.LookupEditHelper(leCheckType, dtCheckTypet, "KEY", "VALUE");
            

            DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");

            Util.insertRowonTop(dtLocation, "-1", " 없음");

            Util.LookupEditHelper(leLocation, dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehouse, dtLocation, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");


            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");

            Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
            dicPalletDefault.Add("WAREHOUSE_ID", "-1");
            dicPalletDefault.Add("PALLET_ID", "");
            dicPalletDefault.Add("PALLET_NM", "선택안합");

            Util.insertRowonTop(_dtPallet, dicPalletDefault);

            _bsPallet.DataSource = _dtPallet;

            //DataTable dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");

            Util.LookupEditHelper(rileComponentCd, ProjectInfo._dtComponent, "KEY", "VALUE");

            DataTable dtInvnetoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState, dtInvnetoryState, "KEY", "VALUE");

            DataTable dtInvnetoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryCat, dtInvnetoryCat, "KEY", "VALUE");
            Util.LookupEditHelper(leInventoryCat, dtInvnetoryCat, "KEY", "VALUE");

            DataTable dtWarehousingState = Util.getCodeList("CD0601", "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");

            //DataTable dtComponentCd2 = new DataTable();

            //dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            //dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //for(int i = 0; i < ProjectInfo._componetCd.Length; i++ )
            //{ 
            //    DataRow dr = dtComponentCd.NewRow();

            //    dr["KEY"] = ProjectInfo._componetCd[i];
            //    dr["VALUE"] = ProjectInfo._componetNm[i];
            //    dtComponentCd.Rows.Add(dr);
            //}

            Util.LookupEditHelper(leComponentCd, ProjectInfo._dtComponent, "KEY", "VALUE");
            _dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");

            _dtAdjustmentPrice.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtAdjustmentPrice.Columns.Add(new DataColumn("EXIST", typeof(bool)));

            foreach (string col in ExamineInfo._listAdjustmentTabletPriceCol)
                _dtAdjustmentPrice.Columns.Add(new DataColumn(col, typeof(long)));

            for (int i = 1; i < 6; i++)
            {
                DataRow dr = _dtAdjustmentPrice.NewRow();

                dr["TYPE"] = i;
                dr["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentTabletPriceCol)
                    dr[col] = 0;

                _dtAdjustmentPrice.Rows.Add(dr);
            }

            var today = DateTime.Today;
            var pastDate = today.AddDays(-90);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

            leCheckType.EditValue = 1;

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;

            _bsWarehousingComponent.DataSource = _dtWarehousingComponent;
            _bsWarehousingInvnetory.DataSource = _dtWarehousingInvnetory;
            _bsWarehousing.DataSource = _dtWarehousing;
        }

       

        private void setGridControl()
        {
            gcWarehousingList.DataSource = null;
            gcWarehousingList.DataSource = _bsWarehousing;

            gcWarehousingComponent.DataSource = null;
            gcWarehousingComponent.DataSource = _bsWarehousingComponent;

            gcWarehousingInvnetory.DataSource = null;
            gcWarehousingInvnetory.DataSource = _bsWarehousingInvnetory;
            
        }

        private void leLocation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            changePallet(e.NewValue);
        }

        private void changePallet(object palletId)
        {
            _bsPallet.Filter = $"WAREHOUSE_ID = '{palletId}' OR WAREHOUSE_ID = '-1'";
        }



        private void gvWarehousingList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingList.RowCount > 0);

            gvWarehousingComponent.BeginDataUpdate();
            

            _dtWarehousingComponent.Clear();

            if (isValidRow)
            {
                _currentWarehousing = e.Row as DataRowView;
                _representativeNo = ConvertUtil.ToString(_currentWarehousing["WAREHOUSING"]);
                _representativeId = ConvertUtil.ToInt64(_currentWarehousing["WAREHOUSING_ID"]);
                if (_representativeId > 0)
                    getWarehousingComponent(_representativeId);
            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _currentWarehousing = null;
                _dtWarehousingInvnetory.Clear();
            }
        
            gvWarehousingComponent.EndDataUpdate();
        }


        private void gvWarehousingComponent_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingComponent.RowCount > 0);

            gvWarehousingInvnetory.BeginDataUpdate();
            _dtWarehousingInvnetory.Clear();

            if (isValidRow)
            {
                _currentComponent = e.Row as DataRowView;
                _componentCd = ConvertUtil.ToString(_currentComponent["COMPONENT_CD"]);
                getWarehousingInventory(_currentComponent["COMPONENT_ID"]);
            }
            else
            {
                _currentComponent = null;
                _componentCd = "";
            }

            gvWarehousingInvnetory.EndDataUpdate();
        }


        private void gvWarehousingInvnetory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingInvnetory.RowCount > 0);

            if (isValidRow)
            {
                _currentInventory = e.Row as DataRowView;
                _inventoryId = ConvertUtil.ToInt64(_currentInventory["INVENTORY_ID"]);
                _barcode = ConvertUtil.ToString(_currentInventory["BARCODE"]);
                changePallet(_currentInventory["WAREHOUSE"]);
            }
            else
            {
                _inventoryId = -1;
                _barcode = "-1";
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();
            getWarehousingList(ref jResult);
        }

        private bool getWarehousingList(ref JObject jResult)
        {
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtWarehousing.Clear();

            jData.Add("WAREHOUSING_CATEGORY", 2);

            if (DBAdjustment.getWarehousingListAll(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehousing.NewRow();

                        dr["NO"] = index++;
                        dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dr["WAREHOUSING"] = obj["WAREHOUSING"];
                        dr["WAREHOUSING_DT"] = ConvertUtil.ToDateTimeNull(obj["WAREHOUSING_DT"]);
                        dr["WAREHOUSING_STATE"] = obj["WAREHOUSING_STATE"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        
                        _dtWarehousing.Rows.Add(dr);
                    }
                }
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

            if (diffDay > 90)
            {
                jData.Add("MSG", "검색 기간은 90일을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("CREATE_DT_S", dtFrom);
            jData.Add("CREATE_DT_E", dtTo);

            //if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
            //    jData.Add("PRODUCT_GRADE", ConvertUtil.ToString(leReceiptState.EditValue));

            return true;
        }

        private void getWarehousingComponent(long warehousingId)
        {
            if (_currentWarehousing == null)
            {
                Dangol.Message("입고번호가 없습니다.");
                return;
            }

            JObject jResult = new JObject();

            if (DBConnect.getWarehousingComponentPart(_representativeNo, ref jResult, warehousingId))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATALIST"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehousingComponent.NewRow();

                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["COMPONENT"] = obj["COMPONENT"];
                        dr["WAREHOUSING_CNT"] = obj["WAREHOUSING_CNT"];
                        dr["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                        dr["GOOD_CNT"] = obj["GOOD_CNT"];
                        dr["FAULT_CNT"] = obj["FAULT_CNT"];
                        dr["RELEASE_CNT"] = obj["RELEASE_CNT"];
                        _dtWarehousingComponent.Rows.Add(dr);
                    }
                }

                return;
            }
            else
            {
                return;
            }
        }

        private void getWarehousingInventory(object componentId)
        {

            JObject jResult = new JObject();

            if (DBConnect.getWarehousingInventoryPart(_representativeId, componentId, ref jResult))
            {
                _dtWarehousingInvnetory.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATALIST"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehousingInvnetory.NewRow();

                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["COMPONENT"] = obj["COMPONENT"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["INIT_PRICE"] = obj["INIT_PRICE"];
                        dr["ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                        dr["ADJUST_DES"] = obj["ADJUST_DES"];
                        dr["PRICE"] = ConvertUtil.ToInt64(obj["INIT_PRICE"]) + ConvertUtil.ToInt64(obj["ADJUST_PRICE"]);
                        dr["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                        dr["DES"] = obj["DES"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["WAREHOUSE"] = obj["WAREHOUSE"];
                        dr["PALLET"] = obj["PALLET"];
                        dr["USER_ID"] = obj["CREATE_USER_ID"];
                        dr["CHECK_YN"] = obj["CHECK_YN"];
                        dr["CHECK"] = false;
                        _dtWarehousingInvnetory.Rows.Add(dr);
                    }
                }
                return;
            }
            else
            {
                return;
            }
        }

        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if(_currentInventory == null)
            {
                Dangol.Message("선택된 재고가 없습니다.");
                return;
            }

            if (ConvertUtil.ToInt64(leLocation.EditValue) < 0)
            {
                Dangol.Message("창고위치를 입력해주세요.");
                return;
            }

            if (ConvertUtil.ToInt64(lePallet.EditValue) < 0)
            {
                Dangol.Message("적재위치를 입력해주세요.");
                return;
            }

            if (Dangol.MessageYN("선택하신 재고정보를 저장하시겠습니까?") == DialogResult.Yes)
            {
                JObject jPartInfo = new JObject();
                JObject jResult = new JObject();

                jPartInfo.Add("INVENTORY_ID", ConvertUtil.ToInt64(_currentInventory["INVENTORY_ID"]));
                jPartInfo.Add("INVENTORY_CAT", ConvertUtil.ToString(leInventoryCat.EditValue));
                jPartInfo.Add("LOCATION", ConvertUtil.ToInt64(leLocation.EditValue));
                jPartInfo.Add("PALLET", ConvertUtil.ToInt64(lePallet.EditValue));
                jPartInfo.Add("INIT_PRICE", ConvertUtil.ToInt64(seInitPrice.EditValue));
                jPartInfo.Add("ADJUST_PRICE", ConvertUtil.ToInt64(seAdjustPrice.EditValue));
                jPartInfo.Add("PRICE", ConvertUtil.ToInt64(sePrice.EditValue));
                jPartInfo.Add("RELEASE_PRICE", ConvertUtil.ToInt64(seReleasePrice.EditValue));
                jPartInfo.Add("ADJUST_DES", ConvertUtil.ToString(meAdjustDes.Text));
                jPartInfo.Add("DES", ConvertUtil.ToString(meDes.Text));
 

                if (DBConnect.updateInventoryDetail(jPartInfo, ref jResult))
                {
                    _currentInventory.BeginEdit();

                    _currentInventory["INVENTORY_CAT"] = jPartInfo["INVENTORY_CAT"];
                    _currentInventory["WAREHOUSE"] = ConvertUtil.ToInt64(leLocation.EditValue);
                    _currentInventory["PALLET"] = ConvertUtil.ToInt64(lePallet.EditValue);
                    _currentInventory["INIT_PRICE"] = jPartInfo["INIT_PRICE"];
                    _currentInventory["ADJUST_PRICE"] = jPartInfo["ADJUST_PRICE"];
                    _currentInventory["PRICE"] = jPartInfo["PRICE"];
                    _currentInventory["RELEASE_PRICE"] = jPartInfo["RELEASE_PRICE"];
                    _currentInventory["ADJUST_DES"] = jPartInfo["ADJUST_DES"];
                    _currentInventory["DES"] = jPartInfo["DES"];
                    
                    _currentInventory.EndEdit();

                    Dangol.Message(jResult["MSG"]);
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
        }

        private void getCheckInfo(long inventoryId, string componentCd)
        {
            JObject jResult = new JObject();
            _dicDataCheck.Clear();
            _etcDes = "";
            _batteryRemain = "";
            _productGrade = "";
            _monSize = "";
            _repairContent = "";

            _isAdjustmentExist = false;
            _dtAdjustmentPrice.Rows[_checkType]["EXIST"] = false;

            if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
            {
                JObject jData = (JObject)jResult["DATA"];

                foreach (var x in jData)
                {
                    string name = x.Key;
                    if (!ProjectInfo._listCheckException.Contains(name))
                    {
                        if (name.Equals("DES") || name.Equals("ETC_DES"))
                            _etcDes = x.Value.ToObject<string>();
                        else if (name.Equals("PRODUCT_GRADE"))
                            _productGrade = x.Value.ToObject<string>();
                        else if (name.Equals("SIZE") && componentCd.Equals("MON"))
                            _monSize = x.Value.ToObject<string>();
                        else if (name.Equals("BATTERY_REMAIN") && componentCd.Equals("TBL"))
                            _batteryRemain = x.Value.ToObject<string>();
                        else if (name.Equals("REPAIR_CONTENT") && componentCd.Equals("TBL"))
                            _repairContent = x.Value.ToObject<string>();
                        
                        else
                        {
                            int value = x.Value.ToObject<int>();

                            if (!_dicDataCheck.ContainsKey(name))
                                _dicDataCheck.Add(name, value);
                        }
                    }
                }

                if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                {
                    if (_componentCd.Equals("TBL"))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        _dtAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentTabletPriceCol)
                            _dtAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                        _isAdjustmentExist = true;
                    }
                }
            }

            if(_checkType > 1)
            {
                short examCheckType = 1;
                _dicDataCheckHistory.Clear();
                _etcDesHistory = "";
                _batteryRemainHistory = "";
                _repairContentHistory = "";
                _productGradeHistory = "";
                _monSizeHistory = "";

                _isAdjustmentExistHistory = false;
                _dtAdjustmentPrice.Rows[examCheckType]["EXIST"] = false;

                if (DBConnect.getCheckInfo(inventoryId, componentCd, examCheckType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("DES") || name.Equals("ETC_DES"))
                                _etcDesHistory = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                _productGradeHistory = x.Value.ToObject<string>();
                            else if (name.Equals("SIZE") && componentCd.Equals("MON"))
                                _monSizeHistory = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN") && componentCd.Equals("TBL"))
                                _batteryRemainHistory = x.Value.ToObject<string>();
                            else if (name.Equals("REPAIR_CONTENT") && componentCd.Equals("TBL"))
                                _repairContentHistory = x.Value.ToObject<string>();
                            else
                            {
                                int value = x.Value.ToObject<int>();

                                if (!_dicDataCheckHistory.ContainsKey(name))
                                    _dicDataCheckHistory.Add(name, value);
                            }

                            if(!_isAdjustmentExist)
                            {
                                if (name.Equals("DES") || name.Equals("ETC_DES"))
                                    _etcDes = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    _productGrade = x.Value.ToObject<string>();
                                else if (name.Equals("SIZE") && componentCd.Equals("MON"))
                                    _monSize = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN") && componentCd.Equals("TBL"))
                                    _batteryRemain = x.Value.ToObject<string>();
                                else if (name.Equals("REPAIR_CONTENT") && componentCd.Equals("TBL"))
                                    _repairContent = x.Value.ToObject<string>();
                                else
                                {
                                    int value = x.Value.ToObject<int>();

                                    if (!_dicDataCheck.ContainsKey(name))
                                        _dicDataCheck.Add(name, value);
                                }
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        if (_componentCd.Equals("TBL"))
                        {
                            JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                            _dtAdjustmentPrice.Rows[examCheckType]["EXIST"] = true;
                            foreach (string col in ExamineInfo._listAdjustmentTabletPriceCol)
                                _dtAdjustmentPrice.Rows[examCheckType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                            if (!_isAdjustmentExist)
                            {
                                _dtAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                                foreach (string col in ExamineInfo._listAdjustmentTabletPriceCol)
                                    _dtAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                            }

                             _isAdjustmentExistHistory = true;
                        }
                    }
                }

            }
        }


        private void lcgInventory_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
           
        }


        private void sbPrintPart_Click(object sender, EventArgs e)
        {
            if (_currentWarehousing == null)
            {
                Dangol.Message("입고번호가 없습니다.");
                return;
            }

            if (string.IsNullOrEmpty(_barcode) || _inventoryId < 1)
            {
                Dangol.Message("재고로 등록되지 않은 부품입니다.");
                return;
            }

            JObject jResult = new JObject();

            if (DBConnect.printInventoryInfo(_representativeType, _representativeNo, _representativeCol, _barcode, lePrintPort.EditValue.ToString(), ref jResult))
            {
                Dangol.Message("부품정보가 출력되었습니다.");
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void sbPartCheck_Click(object sender, EventArgs e)
        {
            if(!_componentCd.Equals("TBL"))
            {
                Dangol.Message("현재 버전은 태블릿 검수만 가능합니다.");
                return;
            }

            if (_inventoryId < 0)
            {
                Dangol.Message("선택하신 부품은 재고로 등록되지 않았습니다.");
                return;
            }

            _checkType = ConvertUtil.ToInt16(leCheckType.EditValue);

            if(_checkType < 1)
            {
                Dangol.Message("검수타입을 선택하세요.");
                return;
            }

            getCheckInfo(_inventoryId, _componentCd);

            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();
            _dicAdjustmentPrice.Clear();
            if (_componentCd.Equals("TBL"))
            {
                JObject jResult = new JObject();
                if(DBConnect.getInventoryInfo(_inventoryId, ref jResult))
                {
                    if (ConvertUtil.ToBoolean(jResult["EXIST"]))
                    {
                        string serialNo = ConvertUtil.ToString(jResult["SERIAL_NO"]);

                        using (DlgTabletCheck check = new DlgTabletCheck(ref _dicDataCheck, _dicDataCheckHistory, ref _dtAdjustmentPrice, _etcDes,
                           _productGrade, _batteryRemain, _repairContent, serialNo, _dtPrintPort, _dtPGrade, _bsPallet, ConvertUtil.ToInt64(lePallet.EditValue), _checkType))
                        {
                            if (check.ShowDialog(this) == DialogResult.OK)
                            {
                                _etcDes = check._etcDes;
                                _productGrade = check._pGrade;
                                _batteryRemain = check._batteryRemain;
                                _repairContent = check._repairContent;
                                serialNo = check._serialNo;

                                JObject jCheckResult = DBConnect.insertTabletCheck(_representativeType, _representativeNo, _representativeCol, _barcode, _inventoryId,
                                    _checkType, _dicDataCheck, _etcDes, _productGrade, _batteryRemain, _repairContent);

                                if (ConvertUtil.ToBoolean(jCheckResult["SUCCESS"]))
                                {
                                    gvWarehousingInvnetory.BeginDataUpdate();
                                    _currentInventory.BeginEdit();
                                    _currentInventory["CHECK_YN"] = "Y";
                                    _currentInventory.EndEdit();
                                    gvWarehousingInvnetory.EndDataUpdate();

                                    if (DBConnect.updateTblSerialNo(_inventoryId, serialNo, ref jResult))
                                    {

                                    }

                                    if (!DBAdjustment.insertAdjustmentTabletPrice(_inventoryId, _checkType, _dtAdjustmentPrice, ref jResult))
                                    {
                                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                    }

                                    if (check._isPrint)
                                    {
                                        string printPort = check._printPort;

                                        if (DBConnect.printTabletProduct(_representativeType, _representativeNo, _representativeCol, _inventoryId, _checkType, printPort, ref jResult))
                                        {
                                            Dangol.Message("태블릿정보와 검수 정보가 출력되었습니다");
                                        }
                                        else
                                        {
                                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                        }

                                        //DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, ProjectInfo._printerPort);
                                    }
                                    else
                                    {
                                        Dangol.Message(ConvertUtil.ToString(jCheckResult["MSG"]));
                                    }
                                }

                            }
                        }
                    }
                }
                
            }


                //Dictionary<string, int> dicCheckInfo = ProjectInfo._dicPartCheckRelease[_inventoryId];

            //if (_componentCd.Equals("MON"))
            //{
                //using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(dicCheckInfo, _dtPrintPort))
                //{
                //    if (monitorCheck.ShowDialog(this) == DialogResult.OK)
                //    {
                //        DBConnect.insertMonitorCheck(_barcode, _checkType, dicCheckInfo);

                //        if (monitorCheck._isPrint)
                //            DBConnect.printMonitorCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
                //    }
                //}
            //}
            //else if (_currentComponentCd.Equals("CAS"))
            //{
            //    using (DlgCasCheck inventoryCheck = new DlgCasCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertCasCheck(_barcode, _checkType, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printCasCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("CPU"))
            //{
            //    using (DlgCpuCheck inventoryCheck = new DlgCpuCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("MEM"))
            //{
            //    using (DlgRamCheck inventoryCheck = new DlgRamCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("MBD"))
            //{
            //    using (DlgMbdCheck inventoryCheck = new DlgMbdCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("VGA"))
            //{
            //    using (DlgVgaCheck inventoryCheck = new DlgVgaCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("STG"))
            //{
            //    //string type = ConvertUtil.ToString(_currentRow["STG_TYPE"]);

            //    //if (type.Contains("SSD"))
            //    //{
            //    //    using (DlgSsdCheck inventoryCheck = new DlgSsdCheck(dicCheckInfo, _dtPrintPort))
            //    //    {
            //    //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //    //        {
            //    //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //    //            if (inventoryCheck._isPrint)
            //    //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, "SSD", ProjectInfo._printerPort);
            //    //        }
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    using (DlgHddCheck inventoryCheck = new DlgHddCheck(dicCheckInfo, _dtPrintPort))
            //    //    {
            //    //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //    //        {
            //    //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //    //            if (inventoryCheck._isPrint)
            //    //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, "HDD", ProjectInfo._printerPort);
            //    //        }
            //    //    }
            //    //}
            //}
            //else if (_currentComponentCd.Equals("ODD"))
            //{
            //    using (DlgOddCheck inventoryCheck = new DlgOddCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("POW"))
            //{
            //    using (DlgPowCheck inventoryCheck = new DlgPowCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
            //    return;
            //}
        }

        
        private void seInitPrice_EditValueChanged(object sender, EventArgs e)
        {
            sePrice.EditValue = ConvertUtil.ToInt64(seInitPrice.EditValue) + ConvertUtil.ToInt64(seAdjustPrice.EditValue);
        }

        private void seAdjustPrice_EditValueChanged(object sender, EventArgs e)
        {
            sePrice.EditValue = ConvertUtil.ToInt64(seInitPrice.EditValue) + ConvertUtil.ToInt64(seAdjustPrice.EditValue);
        }

        private void sbSelectAdd_Click(object sender, EventArgs e)
        {

            if (leComponentCd.EditValue == null)
            {
                Dangol.Message("품목을 선택하세요");
                return;
            }

            string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);

            using (dlgSelectComponent createComp = new dlgSelectComponent(_representativeType, _representativeCol, _representativeNo, _representativeId, _representativeIdCol, _type, componentCd, _dtPrintPort))
            {
                if (createComp.ShowDialog(this) == DialogResult.OK)
                {
                    _dtWarehousingComponent.Clear();
                    getWarehousingComponent(_representativeId);
                }
            }
        }

        private void sbInputAdd_Click(object sender, EventArgs e)
        {
            if (leComponentCd.EditValue == null)
            {
                Dangol.Warining("품목을 선택하세요");
                return;
            }

            string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);

            switch (componentCd)
            {
                case "ADP":
                    using (dlgCreateADP createAdp = new dlgCreateADP(_representativeType, _representativeCol, _representativeNo, _representativeId, _representativeIdCol, _type, _dtPrintPort))
                    {
                        if (createAdp.ShowDialog(this) == DialogResult.OK)
                        {
                            //sbUpdateComponent_Click(null, null);
                        }
                    }
                    break;
                case "TBL":
                    using (dlgCreateTBL createTbl = new dlgCreateTBL(_representativeType, _representativeCol, _representativeNo, _representativeId, _representativeIdCol, _type, _dtPrintPort))
                    {
                        if (createTbl.ShowDialog(this) == DialogResult.OK)
                        {
                            _dtWarehousingComponent.Clear();
                            getWarehousingComponent(_representativeId);
                        }
                    }
                    break;
                default:
                    Dangol.Warining("품목을 선택하세요");
                    break;

            }
        }

        
    }
}