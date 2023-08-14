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
using WareHousingMaster.view.warehousing.createComponent;
using WareHousingMaster.view.warehousing.selectComponent;
using ScreenCopy;
using WareHousingMaster.UtilTest;

namespace WareHousingMaster.view.warehousing
{
    public partial class usrWarehousing : DevExpress.XtraEditors.XtraForm
    {
        string _representativeType = "W";
        string _representativeCol = "WAREHOUSING";
        string _representativeIdCol = "WAREHOUSING_ID";
        string _representativeNo = null;
        short _checkType = 1;
        long _type = 1;

        string _componentCd = "ALL";
        GridColumn[] arrGridColumn;

        DataRowView _currentRow;

        BindingSource bs;
        BindingSource bsDetail;
        BindingSource _bsPallet;
        BindingSource _bsPallet2;
        long _id;
        bool _headerButtonVisible = true;

        string _barcode = null;
        string _component = null;
        string _currentComponentCd = null;
        long _inventoryId = -1;
        long _componentId = -1;
        bool _isWarehousingCheck = false;

        DateTime _warehousingDate = new DateTime();
        DataTable _dtPrintPort;
        DataTable _dtPallet;
        DataTable _dtPallet2;
        DataTable _dtPGrade;

        string _dtStart;
        string _dtEnd;

        public usrWarehousing()
        {
            InitializeComponent();

            arrGridColumn = new GridColumn[4] { gc1, gc2, gc3, gc4};

            bs = new BindingSource();
            bsDetail = new BindingSource();
            _bsPallet = new BindingSource();
            _bsPallet2 = new BindingSource();

            bs.DataSource = ProjectInfo._dtDeviceInfo;
            //teWarehousing.EditValue = "B201111001";
            lcComponent.Text = _componentCd;

            Dangol.ShowSplash();

            if (ProjectInfo._dicProductList == null)
            {
                DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");

                if (rows.Length > 0)
                {
                    long inventoryId = ConvertUtil.ToInt64(rows[0]["INVENTORY_ID"]);
                    string barcode = ConvertUtil.ToString(rows[0]["BARCODE"]);
                    if (inventoryId > 0)
                    {
                        Util.checkProductState(inventoryId, barcode);
                        Util.getEtcComponent();
                        Util.checkProductRemainPart();
                    }
                    else
                        ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
                }
                else
                    ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
            }

            if (ProjectInfo._dicPartCheckWarehousing == null)
                getCheckInfoInit();

            setNtbControl();

            Dangol.CloseSplash();
        }

        private void getCheckInfoInit()
        {
            ProjectInfo._dicPartCheckWarehousing = new Dictionary<long, Dictionary<string, int>>();
            ProjectInfo._dicInventoryDesWarehousing = new Dictionary<long, string>();
            ProjectInfo._dicInventoryGradeWarehousing = new Dictionary<long, string>();
            ProjectInfo._dicMonSizeWarehousing = new Dictionary<long, string>();         

            DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");
            JObject jResult = new JObject();

            long mbdInventoryId = -1;


            foreach (DataRow row in rows)
            {
                long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                string componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                if (componentCd.Equals("MBD"))
                    mbdInventoryId = inventoryId;

                if (!ProjectInfo._dicPartCheckWarehousing.ContainsKey(inventoryId))
                {
                    Dictionary<string, int> dicData = new Dictionary<string, int>();
                    ProjectInfo._dicInventoryDesWarehousing.Add(inventoryId, "");
                    ProjectInfo._dicInventoryGradeWarehousing.Add(inventoryId, "0");
                    ProjectInfo._dicMonSizeWarehousing.Add(inventoryId, "");

                    if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                    {
                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;


                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("DES"))
                                    ProjectInfo._dicInventoryDesWarehousing[inventoryId] = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._dicInventoryGradeWarehousing[inventoryId] = x.Value.ToObject<string>();
                                else if (name.Equals("SIZE"))
                                    ProjectInfo._dicMonSizeWarehousing[inventoryId] = x.Value.ToObject<string>();
                                else
                                {
                                    int value = x.Value.ToObject<int>();

                                    if (!dicData.ContainsKey(name))
                                        dicData.Add(name, value);
                                }
                            }
                        }
                    }

                    ProjectInfo._dicPartCheckWarehousing.Add(inventoryId, dicData);
                }
            }
         
            ProjectInfo._dicNtbCheckWarehousing = new Dictionary<string, short>();
            ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;

            ProjectInfo._isExistNtbCheckWarehousing = false;

            ProjectInfo._dicAllInOneCheckWarehousing = new Dictionary<string, short>();
            ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType][col] = 0;

            ProjectInfo._isExistNtbCheckWarehousing = false;

            if (mbdInventoryId > 0)
            {
                if (ProjectInfo._type > 1)
                    setCheckInfo(ProjectInfo._type, mbdInventoryId);
                else
                {
                    setCheckInfo(2, mbdInventoryId);
                    setCheckInfo(3, mbdInventoryId);
                }
            }
        }

        private void getCheckInfo(long inventoryId, string componentCd)
        {
            JObject jResult = new JObject();

            if (!ProjectInfo._dicPartCheckWarehousing.ContainsKey(inventoryId))
            {
                Dictionary<string, int> dicData = new Dictionary<string, int>();

                ProjectInfo._dicInventoryDesWarehousing.Add(inventoryId, "");
                ProjectInfo._dicInventoryGradeWarehousing.Add(inventoryId, "0");

                if(componentCd.Equals("MON"))
                    ProjectInfo._dicMonSizeWarehousing.Add(inventoryId, "");

                if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("DES"))
                                ProjectInfo._dicInventoryDesWarehousing[inventoryId] = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._dicInventoryGradeWarehousing[inventoryId] = x.Value.ToObject<string>();
                            else if (name.Equals("SIZE") && componentCd.Equals("MON"))
                                ProjectInfo._dicMonSizeWarehousing[inventoryId] = x.Value.ToObject<string>();
                            else
                            {
                                int value = x.Value.ToObject<int>();

                                if (!dicData.ContainsKey(name))
                                    dicData.Add(name, value);
                            }
                        }
                    }
                }

                ProjectInfo._dicPartCheckWarehousing.Add(inventoryId, dicData);
            }

            if(componentCd.Equals("MBD"))
            {
                if (ProjectInfo._type > 1)
                    setCheckInfo(ProjectInfo._type, inventoryId);
                else
                {
                    setCheckInfo(2, inventoryId);
                    setCheckInfo(3, inventoryId);
                }
            }
        }

        private void getProductCheckInfoInit(long inventoryId)
        {
            if (inventoryId > 0)
            {
                if (ProjectInfo._type > 1)
                    setCheckInfo(ProjectInfo._type, inventoryId);
                else
                {
                    
                    setCheckInfo(2, inventoryId);
                    
                    setCheckInfo(3, inventoryId);
                }
            }
        }

        private void setCheckInfo(int checkTyep, long inventoryId)
        {
            JObject jResult = new JObject();
            
            if (checkTyep == 2)
            {
                ProjectInfo._dicNtbCheckWarehousing.Clear();
                if (DBConnect.getCheckInfo(inventoryId, "NTB", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                ProjectInfo._caseDestroyDescriptionWarehousing = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                ProjectInfo._batteryRemainWarehousing = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._productGradeWarehousing = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicNtbCheckWarehousing.ContainsKey(name))
                                    ProjectInfo._dicNtbCheckWarehousing.Add(name, value);
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                            ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }

                    ProjectInfo._isExistNtbCheckWarehousing = true;
                }

                if (!ProjectInfo._isExistNtbCheckRepair)
                {
                    JObject jResult1 = new JObject();
                    if (DBConnect.getCheckInfo(inventoryId, "NTB", 3, ref jResult1))
                        ProjectInfo._isExistNtbCheckRepair = true;
                }
            }
            else if (checkTyep == 3)
            {
                ProjectInfo._dicAllInOneCheckWarehousing.Clear();
                if (DBConnect.getCheckInfo(inventoryId, "ALLINONE", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("ETC_DES"))
                                ProjectInfo._etcWarehousing = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._allInOneProductGradeWarehousing = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicAllInOneCheckWarehousing.ContainsKey(name))
                                    ProjectInfo._dicAllInOneCheckWarehousing.Add(name, value);
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                            ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }

                    ProjectInfo._isExistAllInOneCheckWarehousing = true;
                }

                if (!ProjectInfo._isExistAllInOneCheckRepair)
                {
                    JObject jResult1 = new JObject();
                    if (DBConnect.getCheckInfo(inventoryId, "ALLINONE", 3, ref jResult1))
                        ProjectInfo._isExistAllInOneCheckRepair = true;
                }
            }
        }

        private void usrWarehousing_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();
            getWarehousing();

            teBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
            leLocation.DataBindings.Add(new Binding("EditValue", bs, "WAREHOUSE", false, DataSourceUpdateMode.Never));
            lePallet.DataBindings.Add(new Binding("EditValue", bs, "PALLET", false, DataSourceUpdateMode.Never));
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

            DataTable dtDeviceType = new DataTable();

            dtDeviceType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtDeviceType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._arrTypeNm.Length; i++)
            {
                DataRow dr = dtDeviceType.NewRow();

                dr["KEY"] = i;
                dr["VALUE"] = ProjectInfo._arrTypeNm[i];
                dtDeviceType.Rows.Add(dr);
            }

            Util.LookupEditHelper(leProductType, dtDeviceType, "KEY", "VALUE");

            //DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
            DataTable dtLocation = ProjectInfo._dtLocation;
            Util.insertRowonTop(dtLocation, "-1", "선택안합");
            Util.LookupEditHelper(leLocation, dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leWarehousing2, dtLocation, "KEY", "VALUE");


            //_dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            _dtPallet = new DataTable();
            _dtPallet2 = new DataTable();

            _dtPallet = ProjectInfo._dtPallet.Copy();
            _dtPallet2 = ProjectInfo._dtPallet.Copy();

            _bsPallet.DataSource = _dtPallet;
            _bsPallet2.DataSource = _dtPallet2;
            Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");
            Util.LookupEditHelper(lePallet2, _bsPallet2, "PALLET_ID", "PALLET_NM");

            _dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");

            DataTable dtComponeneCd = new DataTable();

            dtComponeneCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponeneCd.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtComponeneCd, "POW", "파워서플라이");
            Util.LookupEditHelper(leComponentCd, dtComponeneCd, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustomLS("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "USE_YN = 1", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(leNickName, dtNickName, "KEY", "VALUE");

            DataTable dtManufactureType = new DataTable();

            dtManufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtManufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtManufactureType, 2, "외산/기타");
            Util.insertRowonTop(dtManufactureType, 1, "삼성/엘지");
            Util.insertRowonTop(dtManufactureType, -1, "알수없음");
            Util.LookupEditHelper(leManufactureType, dtManufactureType, "KEY", "VALUE");

        }


        private void leLocation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        { 
            _bsPallet.Filter = $"WAREHOUSE_ID = '{e.NewValue}'";
            if (_bsPallet.Count > 1)
                lePallet.ItemIndex = 0;
            else
                lePallet.EditValue = null;
        }

        private void leWarehousing2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            _bsPallet2.Filter = $"WAREHOUSE_ID = '{e.NewValue}'";
            if (_bsPallet2.Count > 1)
                lePallet2.ItemIndex = 0;
            else
                lePallet2.EditValue = null;
        }

        private void setIInitData()
        {
            // 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;
            leLocation.EditValue = ProjectInfo._locationId;
            lePallet.EditValue = ProjectInfo._palletId;
            leWarehousing2.EditValue = ProjectInfo._locationId;
            lePallet2.EditValue = ProjectInfo._palletId;
            leProductType.EditValue = ProjectInfo._type;
            teUserName.EditValue = ProjectInfo._userName;        
        }

        private void setNtbControl()
        {
            if (ProjectInfo._type == 2)
            {
                lcNickName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcManufactureType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcNickName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcManufactureType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            leNickName.EditValue = ProjectInfo._ntbListId;
            leManufactureType.EditValue = ProjectInfo._ntbManufactureType;
        }

        private void getWarehousing()
        {
            JObject jResult = new JObject();

            if (ProjectInfo._dicProductList != null && ProjectInfo._type == 2)
            {
                DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");

                if (rows.Length > 0)
                {
                    long inventoryId = ConvertUtil.ToInt64(rows[0]["INVENTORY_ID"]);
                    if (ProjectInfo._dicProductList.ContainsKey(inventoryId))
                    {
                        if (DBConnect.getWarehousing(inventoryId, ref jResult))
                        {
                            if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                            {
                                _representativeNo = $"{jResult["WAREHOUSING"]}";
                                teWarehousing.Text = _representativeNo;
                                return;
                               
                            }
                        }
                    }
                }
            }

            if (DBConnect.getRepNo(1, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        _representativeNo = $"{obj["REP_NO"]}";
                        teWarehousing.Text = _representativeNo;
                        break;
                    }
                }
            }
        }

        private void gvInventory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvInventory.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _component = ConvertUtil.ToString(_currentRow["COMPONENT"]);
                _inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);
                _componentId = ConvertUtil.ToInt64(_currentRow["COMPONENT_ID"]);
                _currentComponentCd = ConvertUtil.ToString(_currentRow["COMPONENT_CD"]);

                if (_currentRow["WAREHOUSE"] != null && _currentRow["WAREHOUSE"] != DBNull.Value)
                {
                    int warehouse = ConvertUtil.ToInt32(_currentRow["WAREHOUSE"]);
                    //if (warehouse != ConvertUtil.ToInt32(leLocation.EditValue))
                        _bsPallet.Filter = $"WAREHOUSE_ID = '{warehouse}'";

                    if (string.IsNullOrEmpty(_barcode) || _inventoryId < 0)
                        setBarcodeButton(false, false, true);
                    else
                        setBarcodeButton(true, true, false);
                }

                checkPictureState();



                gcInventoryDetail.DataSource = null;
                _id = ConvertUtil.ToInt64(_currentRow["ID"]);
                bsDetail.DataSource = ProjectInfo._dicDeviceInfoDetail[_id];
                gcInventoryDetail.DataSource = bsDetail;

            }
            else
            {
                _currentRow = null;
                checkPictureState();
            }
        }

        private void setGridControl()
        {
            gcInventory.DataSource = null;

            List<string> listColnm = ProjectInfo._dicDeviceColumnNm[_componentCd];
            List<string> listCol = ProjectInfo._dicDeviceColumn[_componentCd];

            lcComponent.Text = _componentCd;

            for (int i = 0; i < Math.Min(listCol.Count, arrGridColumn.Length); i++)
            {
                arrGridColumn[i].Caption = listColnm[i];
                arrGridColumn[i].FieldName = listCol[i];
            }
            if (_componentCd.Equals("ALL"))
            {
                bs.Filter = $"";
                gcNo.FieldName = "NO1";
            }
            else
            {
                bs.Filter = $"COMPONENT_CD = '{_componentCd}'";
                gcNo.FieldName = "NO";
            }

            gcInventory.DataSource = bs;

            if (_componentCd.Equals("ALL") && !_headerButtonVisible)
            {
                _headerButtonVisible = true;
                lcgInventory.CustomHeaderButtons[0].Properties.Visible = true;
                //lcgInventory.CustomHeaderButtons[1].Properties.Visible = true;
                gcCheck.Visible = true;
            }
            else if (!_componentCd.Equals("ALL") && _headerButtonVisible)
            {
                _headerButtonVisible = false;
                lcgInventory.CustomHeaderButtons[0].Properties.Visible = false;
                //lcgInventory.CustomHeaderButtons[1].Properties.Visible = false;
                gcCheck.Visible = false;
            }
        }

        private void sbAll_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("ALL"))
                return;

            _componentCd = "ALL";
            setGridControl();
        }

        private void sbCpu_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("CPU"))
                return;

            _componentCd = "CPU";
            setGridControl();
        }

        private void sbMbd_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MBD"))
                return;

            _componentCd = "MBD";
            setGridControl();
        }

        private void sbMEM_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MEM"))
                return;

            _componentCd = "MEM";
            setGridControl();
        }

        private void sbStg_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("STG"))
                return;

            _componentCd = "STG";
            setGridControl();
        }

        private void sbVga_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("VGA"))
                return;

            _componentCd = "VGA";
            setGridControl();
        }

        private void sbMON_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MON"))
                return;

            _componentCd = "MON";
            setGridControl();
        }

        private void sbPOW_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("POW"))
                return;

            _componentCd = "POW";
            setGridControl();
        }



        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.Equals(1))
            {
                foreach(DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = true;
                    row.EndEdit();
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                foreach (DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
            }
        }

        private void lcgInventory_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvInventory.FocusedRowHandle;
                int topRowIndex = gvInventory.TopRowIndex;
                gvInventory.FocusedRowHandle = -1;

                gvInventory.FocusedRowHandle = rowhandle;
                foreach (DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = true;
                    row.EndEdit();
                }
            }

        }

        private void lcgInventory_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvInventory.FocusedRowHandle;
                int topRowIndex = gvInventory.TopRowIndex;
                gvInventory.FocusedRowHandle = -1;
                gvInventory.FocusedRowHandle = rowhandle;

                foreach (DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
            }
        }

        private void sbConstructProduct_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            if (!_componentCd.Equals("ALL"))
            {
                Dangol.Message($"제품 구성은 전체 화면에서만 가능합니다.");
                return;
            }

            DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");
            DataRow[] rowUnCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = FALSE");

            if (rowCheck.Length < 2)
            {
                Dangol.Message("제품은 2개 이상의 부품으로 구성돼야 합니다.");
                return;
            }

            Dangol.ShowSplash();

            Dictionary<string, string> dicInventoryId = new Dictionary<string, string>();
            List<long> listInventoryId = new List<long>();
            long inventoryId;
            long mbdInventoryId = -1;
            string componentCd;
            bool isCheckMbd = false;
            JObject jResult = new JObject();
            int inventoryCnt = 0;

            foreach (DataRow drCheck in rowCheck)
            {
                inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                if (componentCd.Equals("MBD"))
                {
                    isCheckMbd = true;
                    mbdInventoryId = inventoryId;
                }
                else
                {
                    if (inventoryId < 0)
                        continue;

                    if (!listInventoryId.Contains(inventoryId))
                        listInventoryId.Add(inventoryId);
                }

                

                inventoryCnt++;

                if (dicInventoryId.ContainsKey(componentCd))
                {
                    string sinventoryId = dicInventoryId[componentCd];
                    sinventoryId = $"{sinventoryId},{inventoryId}";
                    dicInventoryId[componentCd] = sinventoryId;
                }
                else
                    dicInventoryId.Add(componentCd, ConvertUtil.ToString(inventoryId));

            }

            if (!isCheckMbd)
            {
                Dangol.CloseSplash();
                Dangol.Message("메인보드가 체크되지 않았습니다.");
                return;
            }

            if (mbdInventoryId < 1)
            {
                Dangol.CloseSplash();
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return;
            }

            if (inventoryCnt < 2)
            {
                Dangol.CloseSplash();
                Dangol.Message("제품 구성은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            if (Dangol.MessageYN($"선택하신 부품들로 제품을 구성하시겠습니까?") == DialogResult.No)
            {
                Dangol.CloseSplash();
                return;
            }

            if (DBConnect.constructProduct(_representativeType, _representativeNo, _representativeCol, mbdInventoryId, listInventoryId, ref jResult))
            {
                string id = "";
                string barcode = "";
                ProjectInfo._dicProductList.Clear();

                foreach (DataRow drCheck in rowCheck)
                {
                    inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                    id = ConvertUtil.ToString(inventoryId);
                    barcode = ConvertUtil.ToString(drCheck["BARCODE"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                    if (ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]) > 0)
                    {

                        Dictionary<string, string> dicData = new Dictionary<string, string>();

                        dicData.Add("INVENTORY_ID", id);
                        dicData.Add("BARCODE", barcode);
                        dicData.Add("COMPONENT_CD", componentCd);

                        ProjectInfo._dicProductList.Add(inventoryId, dicData);
                        

                        drCheck["PRODUCT_YN"] = true;
                    }
                    else
                        drCheck["PRODUCT_YN"] = false;
                }

                foreach (DataRow drCheck in rowUnCheck)
                    drCheck["PRODUCT_YN"] = false;

                checkPictureState();
                Dangol.CloseSplash();
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                checkPictureState();
                Dangol.CloseSplash();
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }


        private void sbAddInventory_Click(object sender, EventArgs e)
        {
            if(!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            if (ConvertUtil.ToInt64(leWarehousing2.EditValue) < 0)
            {
                Dangol.Message("창고위치를 입력해주세요.");
                return;
            }

            Dangol.ShowSplash();
            int pallet = -1;
            if (_bsPallet2.Count > 0)
                pallet = ConvertUtil.ToInt32(lePallet2.EditValue);

            JObject jResult = new JObject();
            long warehouseMovementId = -1;

            if (DBConnect.checkWarehouseMovement(leWarehousing2.EditValue.ToString(), pallet, ref jResult))
            {
                warehouseMovementId = ConvertUtil.ToInt64(jResult["ID"]);
            }
            else
            {
                Dangol.Message($"창고이동 조회에서 오류가 발생했습니다. ERROR {jResult["MSG"]}");
                return;
            }

            if (_componentCd.Equals("ALL"))
            {  
                DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

                string componentCd;
                DataTable dtComponentInfo;
                DataRow[] rowDeviceInfo;
                DataRow[] row;
                
                long id;
                long componentId;
                long inventoryId;
                int successCnt = 0;
                int existedCnt = 0;
                int errorCnt = 0;
                string msg = "오류가 발생했습니다. 관리자에게 문의하세요.";
                bool isCreate = false;

                if (rowCheck.Length < 1)
                {
                    Dangol.CloseSplash();
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                foreach(DataRow drCheck in rowCheck)
                {
                    id = ConvertUtil.ToInt64(drCheck["ID"]);
                    componentId = ConvertUtil.ToInt64(drCheck["COMPONENT_ID"]);
                    inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                    if (componentId > 0 && inventoryId > 0)
                    {
                        existedCnt++;
                        continue;
                    }

                    isCreate = true;

                    dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    rowDeviceInfo = dtComponentInfo.Select($"ID = {id}");
                    row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");

                    if (rowDeviceInfo.Length > 0)
                    {

                        if (DBConnect.InsertInventory(_representativeType, _representativeNo, _representativeCol, componentCd, leWarehousing2.EditValue.ToString(), pallet, rowDeviceInfo[0], warehouseMovementId, ref jResult))
                        {
                            row[0].BeginEdit();
                            foreach (string col in ProjectInfo._listKeyColumn)
                            {
                                dtComponentInfo.Rows[0][col] = jResult[col];
                                row[0][col] = jResult[col];
                            }

                            row[0]["INVENTORY_YN"] = true;
                            row[0]["PRODUCT_YN"] = false;
                            row[0]["RELEASE_YN"] = false;
                            row[0]["WAREHOUSE"] = leWarehousing2.EditValue.ToString();
                            row[0]["PALLET"] = pallet;
                            row[0].EndEdit();

                            if (_componentId < 0)
                            {
                                DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];

                                foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                                {
                                    dr.BeginEdit();
                                    dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                                    dr.EndEdit();
                                }

                                if (id == _id)
                                {
                                    gcInventoryDetail.DataSource = null;
                                    bsDetail.DataSource = dtDeviceInfoFromDB;
                                    gcInventoryDetail.DataSource = bsDetail;
                                }
                            }

                            if (id == _id)
                            {
                                _barcode = ConvertUtil.ToString(jResult["BARCODE"]);
                                _component = ConvertUtil.ToString(jResult["COMPONENT"]);
                                _inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);
                                _componentId = ConvertUtil.ToInt64(jResult["COMPONENT_ID"]);

                                setBarcodeButton(true, true, false);
                            }
                            successCnt++;

                            if (componentCd.Equals("MBD"))
                            {
                                JObject jResultos = new JObject();
                                ProjectInfo._isExistNtbCheckWarehousing = false;
                                DBConnect.osInfoCheck(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), rowDeviceInfo[0], ref jResultos);
                            }
                        }
                        else
                        {
                            errorCnt++;
                            msg = ConvertUtil.ToString(jResult["MSG"]);
                        }
                    }
                }

                Dangol.CloseSplash();

                if (successCnt == 0)
                {
                    if(isCreate)
                        Dangol.Message(msg);
                    else
                        Dangol.Message($"이미 등록된 재고입니다.");
                }
                else
                {
                    if(existedCnt > 0)
                        Dangol.Message($"{rowCheck.Length} 부품 중 {successCnt}개 재고가 등록되었습니다.(기존 등록 재고: {existedCnt}개)");
                    else if(errorCnt > 0)
                        Dangol.Message($"{rowCheck.Length} 부품 중 {successCnt}개 재고가 등록되었습니다.(오류: {errorCnt}개)");
                    else
                        Dangol.Message($"{rowCheck.Length} 부품 중 {successCnt}개 재고가 등록되었습니다.");
                }
                        

            }
            else
            {
                if(_inventoryId > 0)
                {
                    Dangol.CloseSplash();
                    Dangol.Message("이미 등록된 재고입니다.");
                    return;
                }

                DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_componentCd];
                DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
                DataRow[] row = ProjectInfo._dtDeviceInfo.Select($"ID = {_id}");

                if (rowDeviceInfo.Length > 0)
                {
                    if(DBConnect.InsertInventory(_representativeType, _representativeNo, _representativeCol, _currentComponentCd, leWarehousing2.EditValue.ToString(), pallet, rowDeviceInfo[0], warehouseMovementId, ref jResult))
                    {
                        row[0].BeginEdit();
                        foreach (string col in ProjectInfo._listKeyColumn)
                        {
                            dtComponentInfo.Rows[0][col] = jResult[col];
                            row[0][col] = jResult[col];
                        }

                        row[0]["INVENTORY_YN"] = true;
                        row[0]["PRODUCT_YN"] = false;
                        row[0]["RELEASE_YN"] = false;
                        row[0]["WAREHOUSE"] = leWarehousing2.EditValue.ToString();
                        row[0]["PALLET"] = pallet;
                        row[0].EndEdit();

                        DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];

                        foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                        {
                            dr.BeginEdit();
                            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                            dr.EndEdit();
                        }

                        gcInventoryDetail.DataSource = null;
                        bsDetail.DataSource = dtDeviceInfoFromDB;
                        gcInventoryDetail.DataSource = bsDetail;
                        

                        _barcode = ConvertUtil.ToString(jResult["BARCODE"]);
                        _component = ConvertUtil.ToString(jResult["COMPONENT"]);
                        _inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);
                        _componentId = ConvertUtil.ToInt64(jResult["COMPONENT_ID"]);

                        setBarcodeButton(true, true, false);

                        if (_currentComponentCd.Equals("MBD"))
                        {
                            JObject jResultos = new JObject();
                            DBConnect.osInfoCheck(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), rowDeviceInfo[0], ref jResultos);
                        }

                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
                else
                    Dangol.CloseSplash();
            }

        }

        private bool checkRepresentativeInfo()
        {
            if(teWarehousing.EditValue == null)
                return false;
            else
             _representativeNo = teWarehousing.EditValue.ToString();

            if (string.IsNullOrWhiteSpace(_representativeNo))
                return false;
            else
                return true;
            
        }

        private void sbAddComponent_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();

            if (_componentCd.Equals("ALL"))
            {
                DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

                string componentCd;
                DataTable dtComponentInfo;
                DataRow[] rowDeviceInfo;
                DataRow[] row;
                JObject jResult = new JObject();
                long id;
                long componentId;
                int successCnt = 0;
                int existedCnt = 0;
                int errorCnt = 0;
                string msg = "오류가 발생했습니다. 관리자에게 문의하세요.";
                bool isCreate = false;

                if (rowCheck.Length < 1)
                {
                    Dangol.CloseSplash();
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                foreach (DataRow drCheck in rowCheck)
                {
                    id = ConvertUtil.ToInt64(drCheck["ID"]);
                    componentId = ConvertUtil.ToInt64(drCheck["COMPONENT_ID"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                    if (componentId > 0)
                    {
                        existedCnt++;
                        continue;
                    }

                    isCreate = true;

                    dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    rowDeviceInfo = dtComponentInfo.Select($"ID = {id}");
                    row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");

                    if (rowDeviceInfo.Length > 0)
                    {
                        if (DBConnect.InsertComponent(componentCd, rowDeviceInfo[0], ref jResult))
                        {
                            row[0].BeginEdit();
                            row[0]["COMPONENT_ID"] = dtComponentInfo.Rows[0]["COMPONENT_ID"] = jResult["COMPONENT_ID"];
                            row[0]["COMPONENT_CD"] = dtComponentInfo.Rows[0]["COMPONENT_CD"] = jResult["COMPONENT_CD"];
                            row[0].EndEdit();

                            DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];

                            foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                            {
                                dr.BeginEdit();
                                dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                                dr.EndEdit();
                            }

                            if (id == _id)
                            {
                                gcInventoryDetail.DataSource = null;
                                bsDetail.DataSource = dtDeviceInfoFromDB;
                                gcInventoryDetail.DataSource = bsDetail;
                            }
                            

                            if (id == _id)
                            {
                                _component = ConvertUtil.ToString(jResult["COMPONENT"]);
                                _componentId = ConvertUtil.ToInt64(jResult["COMPONENT_ID"]);
                            }
                            successCnt++;
                        }
                        else
                        {
                            errorCnt++;
                            msg = ConvertUtil.ToString(jResult["MSG"]);
                        }
                    }
                }

                Dangol.CloseSplash();

                if (successCnt == 0)
                {
                    if (isCreate)
                        Dangol.Message(msg);
                    else
                        Dangol.Message($"이미 등록된 부품입니다.");
                }
                else
                {
                    if (existedCnt > 0)
                        Dangol.Message($"{rowCheck.Length} 부품 중 {successCnt}개 부품이 등록되었습니다.(기존 등록 부품: {existedCnt}개)");
                    else if (errorCnt > 0)
                        Dangol.Message($"{rowCheck.Length} 부품 중 {successCnt}개 부품이 등록되었습니다.(오류: {errorCnt}개)");
                    else
                        Dangol.Message($"{rowCheck.Length} 부품 중 {successCnt}개 부품이 등록되었습니다.");
                }
            }
            else
            {
                if (_componentId > 0)
                {
                    Dangol.CloseSplash();
                    Dangol.Message("이미 등록된 부품입니다.");
                    return;
                }

                DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_componentCd];
                DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
                DataRow[] row = ProjectInfo._dtDeviceInfo.Select($"ID = {_id}");
                JObject jResult = new JObject();


                if (rowDeviceInfo.Length > 0)
                {
                    if (DBConnect.InsertComponent(_currentComponentCd, rowDeviceInfo[0], ref jResult))
                    {
                        row[0].BeginEdit();
                        row[0]["COMPONENT_ID"] = dtComponentInfo.Rows[0]["COMPONENT_ID"] = jResult["COMPONENT_ID"];
                        row[0]["COMPONENT_CD"] = dtComponentInfo.Rows[0]["COMPONENT_CD"] = jResult["COMPONENT_CD"];
                        row[0].EndEdit();

                        DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];

                        foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                        {
                            dr.BeginEdit();
                            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                            dr.EndEdit();
                        }

                        gcInventoryDetail.DataSource = null;
                        bsDetail.DataSource = dtDeviceInfoFromDB;
                        gcInventoryDetail.DataSource = bsDetail;
                      

                        _component = ConvertUtil.ToString(jResult["COMPONENT"]);
                        _componentId = ConvertUtil.ToInt64(jResult["COMPONENT_ID"]);

                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
                else
                    Dangol.CloseSplash();
            }
        }

        private void sbUpdateComponent_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("ALL"))
            {
                DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

                string componentCd;
                DataTable dtComponentInfo;
                DataRow[] rowDeviceInfo;
                DataRow[] row;
                JObject jResult = new JObject();
                long id;
                int successCnt = 0;
                int existedCnt = 0;
                int errorCnt = 0;
                string msg = "오류가 발생했습니다. 관리자에게 문의하세요.";
                bool isUpdate = false;
                bool isSame;

                if (rowCheck.Length < 1)
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                foreach (DataRow drCheck in rowCheck)
                {
                    id = ConvertUtil.ToInt64(drCheck["ID"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);
                    isSame = true;

                    dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    rowDeviceInfo = dtComponentInfo.Select($"ID = {id}");
                    row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");

                    if (rowDeviceInfo.Length > 0)
                    {
                        DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];

                        foreach (DataRow dr in dtDeviceInfoFromDB.Rows) // 부품을 구분하는 컬럼만으로 체크해야한다.
                        {
                            if(!dr["MATCHING_INFO"].Equals(dr["DEVICE_INFO"]))
                            {
                                isSame = false;
                                break;
                            }
                        }

                        if (isSame)
                        {
                            existedCnt++;
                            continue;
                        }

                        isUpdate = true;
                       
                        if (DBConnect.updateComponent(componentCd, rowDeviceInfo[0], ref jResult))
                        {
                            foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                            {
                                dr.BeginEdit();
                                dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                                dr.EndEdit();
                            }

                            if (id == _id)
                            {
                                gcInventoryDetail.DataSource = null;
                                bsDetail.DataSource = dtDeviceInfoFromDB;
                                gcInventoryDetail.DataSource = bsDetail;
                            }

                            successCnt++;
                        }
                        else
                        {
                            errorCnt++;
                            msg = ConvertUtil.ToString(jResult["MSG"]);
                        }
                    }
                }

                if (successCnt == 0)
                {
                    if (isUpdate)
                        Dangol.Message(msg);
                    else
                        Dangol.Message($"수정사항이 없습니다.");
                }
                else
                {
                    Dangol.Message($"{rowCheck.Length} 부품 중 {successCnt}개 부품이 수정되었습니다.");   
                }
            }
            else
            {
                DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_componentCd];
                DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
                DataRow[] row = ProjectInfo._dtDeviceInfo.Select($"ID = {_id}");
                JObject jResult = new JObject();
                bool isSame = true;

                if (rowDeviceInfo.Length > 0)
                {
                    DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];

                    foreach (DataRow dr in dtDeviceInfoFromDB.Rows) // 부품을 구분하는 컬럼만으로 체크해야한다.
                    {
                        if (!dr["MATCHING_INFO"].Equals(dr["DEVICE_INFO"]))
                        {
                            isSame = false;
                            break;
                        }
                    }

                    if (isSame)
                    {
                        Dangol.Message($"수정사항이 없습니다.");
                        return;
                    }

                    if (DBConnect.updateComponent(_currentComponentCd, rowDeviceInfo[0], ref jResult))
                    {
                        foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                        {
                            dr.BeginEdit();
                            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                            dr.EndEdit();
                        }

                        gcInventoryDetail.DataSource = null;
                        bsDetail.DataSource = dtDeviceInfoFromDB;
                        gcInventoryDetail.DataSource = bsDetail;

                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }

        private void sbPrintProduct_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            if (!_componentCd.Equals("ALL"))
            {
                Dangol.Message($"제품 출력은 전체 화면에서만 가능합니다.");
                return;
            }

            DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

            if (rowCheck.Length < 2)
            {
                Dangol.Message("제품 출력은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            Dictionary<string, string> dicInventoryId = new Dictionary<string, string>();
            long inventoryId;
            string componentCd;
            bool isCheckMbd = false;
            bool isMbdInventoryIdExist = false;
            JObject jResult = new JObject();
            int inventoryCnt = 0;

            foreach (DataRow drCheck in rowCheck)
            {
                inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                if(componentCd.Equals("MBD"))
                {
                    isCheckMbd = true;

                    if (inventoryId > 0)
                        isMbdInventoryIdExist = true;
                }

                if (inventoryId < 0)
                    continue;

                inventoryCnt++;

                if (dicInventoryId.ContainsKey(componentCd))
                {
                    string sinventoryId = dicInventoryId[componentCd];
                    sinventoryId= $"{sinventoryId},{inventoryId}";
                    dicInventoryId[componentCd] = sinventoryId;
                }
                else
                    dicInventoryId.Add(componentCd, ConvertUtil.ToString(inventoryId));
            }

            if (!isCheckMbd)
            {
                Dangol.Message("메인보드가 체크되지 않았습니다.");
                return;
            }

            if (!isMbdInventoryIdExist)
            {
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return;
            }

            if(inventoryCnt < 2)
            {
                Dangol.Message("제품 출력은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            if (DBConnect.printProduct(_representativeType, _representativeNo, _representativeCol, dicInventoryId, lePrintPort.EditValue.ToString(), ref jResult))
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }


        }

        private void sbPrintPart_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
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

        private void sbNtbPrint_Click(object sender, EventArgs e)
        {
            JObject jResult = ntbModelPrint();

            if (ConvertUtil.ToBoolean(jResult["SUCCESS"]))
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }
        private JObject ntbModelPrint()
        {
            JObject jResult = new JObject();

            jResult.Add("SUCCESS", false);

            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return jResult;
            }

            if (!_componentCd.Equals("ALL"))
            {
                Dangol.Message($"제품 출력은 전체 화면에서만 가능합니다.");
                return jResult;
            }

            DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

            //if (rowCheck.Length < 2)
            //{
            //    Dangol.Message("제품 출력은 2개 이상 부품으로 구성돼야 합니다.");
            //    return;
            //}

            long inventoryId = -1;
            string componentCd;
            bool isCheckMbd = false;
            bool isMbdInventoryIdExist = false;
            

            foreach (DataRow drCheck in rowCheck)
            {               
                componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                if (componentCd.Equals("MBD"))
                {
                    inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                    isCheckMbd = true;

                    if (inventoryId > 0)
                        isMbdInventoryIdExist = true;

                    break;
                }
            }

            if (!isCheckMbd)
            {
                Dangol.Message("메인보드가 체크되지 않았습니다.");
                return jResult;
            }

            if (!isMbdInventoryIdExist)
            {
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return jResult;
            }

            DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult);

            return jResult;
        }


        public void printCheckResult(string caseDestroyDescription, string batteryRemain, string pGrade, long palletNo, bool isWithRepair, bool isPrint)
        {
            DataRow[] row = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");
            string barcode = ConvertUtil.ToString(row[0]["BARCODE"]);
            long inventoryId = ConvertUtil.ToInt64(row[0]["INVENTORY_ID"]);
            List<long> listInventoryId = new List<long>();
            long warehouse = ConvertUtil.ToInt64(leWarehousing2.EditValue);

            foreach (DataRow dr in row)
                listInventoryId.Add(ConvertUtil.ToInt64(dr["INVENTORY_ID"]));


            JObject jResult = new JObject();

            if (ProjectInfo._isExistNtbCheckRepair)
            {
                Dangol.Message($"리페어정보가 존재하는 제품은 검수 체크를 변경할 수 없습니다.");
                return;
            }

            ProjectInfo._caseDestroyDescriptionWarehousing = caseDestroyDescription;
            ProjectInfo._batteryRemainWarehousing = batteryRemain;
            ProjectInfo._productGradeWarehousing = pGrade;

            DBConnect.updatePallet(listInventoryId, warehouse, palletNo);

            foreach (DataRow dr in row)
            {
                if (ConvertUtil.ToInt64(dr["WAREHOUSE"]) == warehouse)
                {
                    dr.BeginEdit();
                    dr["PALLET"] = $"{palletNo}";
                    dr.EndEdit();
                }
            }

            _dtEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, _dtStart, _dtEnd, barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, listInventoryId, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, 0, false))
            {
                //JObject jResult = new JObject();

                if (!DBAdjustment.insertAdjustmentPrice(inventoryId, _checkType, ref jResult))
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }

                if (isWithRepair)
                {
                    if (DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, _dtStart, _dtEnd, barcode, inventoryId, 3, ProjectInfo._dicNtbCheckWarehousing, listInventoryId, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, 1, false))
                    {

                        foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                            ProjectInfo._dtNTBAdjustmentPrice.Rows[3][col] = ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col];

                        if (!DBAdjustment.insertAdjustmentPrice(inventoryId, 3, ref jResult))
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                    }
                }

                if (isPrint)
                {
                    if (DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                    {
                        Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                    }
                    else
                    {
                        Dangol.Message($"{ConvertUtil.ToString(jResult["MSG"])} / 검수 정상 저장");
                    }

                    //DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, ProjectInfo._printerPort);
                }
                else
                {
                    Dangol.Message("노트북 검수가 완료되었습니다.");
            }

            ProjectInfo._isExistNtbCheckWarehousing = true;
            }
        }

        private void sbCheck_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            int checkValue = ConvertUtil.ToInt32(leProductType.EditValue);

            if (checkValue == 1)
            {
                Dangol.Message("PC는 개별 부품 체크만 가능합니다.");
                return;
            }

            DataRow[] row = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");
            string barcode = null;
            long inventoryId = -1;
            List<long> listInventoryId = new List<long>();

            if (row.Length > 0)
            {
                barcode = ConvertUtil.ToString(row[0]["BARCODE"]);
                inventoryId = ConvertUtil.ToInt64(row[0]["INVENTORY_ID"]);

                if (string.IsNullOrEmpty(barcode) || inventoryId < 1)
                {
                    Dangol.Message("메인보드가 재고로 등록되지 않았습니다.");
                    return;
                }
            }

            JObject jResult = new JObject();

            if (ProjectInfo._isExistNtbCheckWarehousing || ProjectInfo._isExistNtbCheckRelease || ProjectInfo._isExistNtbCheckRepair
                || ProjectInfo._isExistAllInOneCheckWarehousing || ProjectInfo._isExistAllInOneCheckRelease || ProjectInfo._isExistAllInOneCheckRepair)
            {
                if (DBConnect.getProductInfo(inventoryId, ref jResult))
                {
                    string approveType = ConvertUtil.ToString(jResult["APPROVE_TYPE"]);
                    int adjustmentState = ConvertUtil.ToInt32(jResult["ADJUSTMENT_STATE"]);

                    if (!string.IsNullOrWhiteSpace(approveType) && adjustmentState == 1)
                    {
                        Dangol.Message("승인완료 & 정산대기인 제품은 검수를 변경할 수 없습니다.");
                        return;
                    }


                }
            }



            if (ProjectInfo._type == 2)
            {
                if (ProjectInfo._ntbListId < 1)
                {
                    Dangol.Message("제품 사양을 선택해주세요");
                    return;
                }

                if (ProjectInfo._ntbManufactureType < 1)
                {
                    Dangol.Message("제조사를 선택해주세요");
                    return;
                }
            }

            if (!_isWarehousingCheck)
            {
                if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
                {
                    return;
                }
                _isWarehousingCheck = true;

                
            }

            

            if (ProjectInfo._type == 2)
            {
                ProjectInfo._dicNTBAdjustmentPrice.Clear();
                if (DBConnect.getNTBAdjustmentPrice(_representativeNo, _representativeType, ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string col = "";
                    if (ProjectInfo._ntbManufactureType == 1)
                        col = "MAJOR_PRICE";
                    else
                        col = "ETC_PRICE";
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string key = ConvertUtil.ToString(obj["COL_NM"]);

                        if (!ProjectInfo._dicNTBAdjustmentPrice.ContainsKey(key))
                            ProjectInfo._dicNTBAdjustmentPrice.Add(key, ConvertUtil.ToInt64(obj[col]));
                    }
                }
            }
            else if (ProjectInfo._type == 3)
            {
                ProjectInfo._dicAllInOneAdjustmentPrice.Clear();
                //if (DBConnect.getAllInOneAdjustmentPrice(_representativeNo, _representativeType, ref jResult))
                //{
                //    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                //    string col = "";
                //    if (ProjectInfo._ntbManufactureType == 1)
                //        col = "MAJOR_PRICE";
                //    else
                //        col = "ETC_PRICE";
                //    foreach (JObject obj in jArray.Children<JObject>())
                //    {
                //        string key = ConvertUtil.ToString(obj["COL_NM"]);

                //        if (!ProjectInfo._dicAllInOneAdjustmentPrice.ContainsKey(key))
                //            ProjectInfo._dicAllInOneAdjustmentPrice.Add(key, ConvertUtil.ToInt64(obj[col]));
                //    }
                //}
            }



            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

           
            string checkText = leProductType.Text;

            // 우선은 잠시 숨겨둠

            long warehouse = ConvertUtil.ToInt64(leWarehousing2.EditValue);

            if (!ProjectInfo._userType.Equals("S") && !ProjectInfo._userType.Equals("M"))
            {
                //if (warehouse != 44)
                //{
                //    Dangol.Message($"제품 검수는 검수실({1041})에서만 가능합니다.");
                //    return;
                //}                
            }

            if (ProjectInfo._type != checkValue)
            {
                if (Dangol.MessageYN($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{ProjectInfo._typeNm}, 선택:{checkText})", $"{checkText} 검수 체크") == DialogResult.No)
                    return;
            }

            row = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");

            if (row.Length > 0)
            { 
                //if (Dangol.MessageYN($"등록된 재고가 {row.Length}개 입니다. 검수를 진행 하시겠습니까?", $"{checkText} 검수 체크") == DialogResult.No)
                //    return;

                foreach(DataRow dr in row)
                    listInventoryId.Add(ConvertUtil.ToInt64(dr["INVENTORY_ID"]));
            }


            getProductCheckInfoInit(inventoryId);

            if (checkValue == 2)
            {
                _dtStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (_representativeNo.ToUpper().Equals("B200806001") || _representativeNo.ToUpper().Equals("B201030004") || _warehousingDate > Convert.ToDateTime("2020-11-11"))
                {
                    using (DlgNtb2ndEditionCheck2 ntbCheck = new DlgNtb2ndEditionCheck2(ProjectInfo._dicNtbCheckWarehousing, null, ref ProjectInfo._dtNTBAdjustmentPrice, ProjectInfo._dicNTBAdjustmentPrice, 
                    ProjectInfo._caseDestroyDescriptionWarehousing,ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, 
                    _dtPrintPort, _dtPGrade, _bsPallet, ConvertUtil.ToInt64(lePallet.EditValue), _checkType, ProjectInfo._isExistNtbCheckRepair))
                    {
                        ntbCheck.saveCheckHandler += new DlgNtb2ndEditionCheck2.SaveCheckHandler(printCheckResult);

                        if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            if (ProjectInfo._isExistNtbCheckRepair)
                            {
                                Dangol.Message($"리페어정보가 존재하는 제품은 검수 체크를 변경할 수 없습니다.");
                                return;
                            }

                            ProjectInfo._caseDestroyDescriptionWarehousing = ntbCheck._caseDestroyDescription;
                            ProjectInfo._batteryRemainWarehousing = ntbCheck._batteryRemain;
                            ProjectInfo._productGradeWarehousing = ntbCheck._pGrade;

                            DBConnect.updatePallet(listInventoryId, warehouse, ntbCheck._palletNo);

                            foreach (DataRow dr in row)
                            {
                                if (ConvertUtil.ToInt64(dr["WAREHOUSE"]) == warehouse)
                                {
                                    dr.BeginEdit();
                                    dr["PALLET"] = $"{ntbCheck._palletNo}";
                                    dr.EndEdit();
                                }
                            }

                            _dtEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            if (DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, _dtStart, _dtEnd, barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, listInventoryId, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, 0, false))
                            {
                                //JObject jResult = new JObject();

                                if (!DBAdjustment.insertAdjustmentPrice(inventoryId, _checkType, ref jResult))
                                {
                                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                }

                                if (ntbCheck._isWithRepair)
                                {
                                    if (DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, _dtStart, _dtEnd, barcode, inventoryId, 3, ProjectInfo._dicNtbCheckWarehousing, listInventoryId, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, 1, false))
                                    {

                                        foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                                            ProjectInfo._dtNTBAdjustmentPrice.Rows[3][col] = ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col];

                                        if (!DBAdjustment.insertAdjustmentPrice(inventoryId, 3, ref jResult))
                                        {
                                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                        }
                                    }
                                }

                                if (ntbCheck._isPrint)
                                {
                                    string port = ntbCheck._port.Equals("-1") ? lePrintPort.EditValue.ToString() : ntbCheck._port;

                                    if (DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, port, ref jResult))
                                    {
                                        Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                                    }
                                    else
                                    {
                                        Dangol.Message($"{ConvertUtil.ToString(jResult["MSG"])} / 검수 정상 저장");
                                    }

                                    //DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, ProjectInfo._printerPort);
                                }
                                else
                                {
                                    Dangol.Message("노트북 검수가 완료되었습니다.");
                                }

                                ProjectInfo._isExistNtbCheckWarehousing = true;
                            }
                        }
                        ntbCheck.saveCheckHandler -= new DlgNtb2ndEditionCheck2.SaveCheckHandler(printCheckResult);
                    }
                }
                else
                {
                    using (DlgNtbCheck ntbCheck = new DlgNtbCheck(ProjectInfo._dicNtbCheckWarehousing, _dtPrintPort, _bsPallet, ConvertUtil.ToInt64(lePallet2.EditValue)))
                    {
                        if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            if (ProjectInfo._isExistNtbCheckRepair)
                            {
                                Dangol.Message($"리페어정보가 존재하는 제품은 검수 체크를 변경할 수 없습니다.");
                                return;
                            }
                            DBConnect.updatePallet(listInventoryId, ConvertUtil.ToInt64(leWarehousing2.EditValue), ntbCheck._palletNo);

                            foreach (DataRow dr in row)
                            {
                                if (ConvertUtil.ToInt64(dr["WAREHOUSE"]) == warehouse)
                                {
                                    dr.BeginEdit();
                                    dr["PALLET"] = $"{ntbCheck._palletNo}";
                                    dr.EndEdit();
                                }
                            }

                            if (DBConnect.insertNtbCheck1stEdition(_representativeType, _representativeNo, _representativeCol, barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, listInventoryId))
                            {
                                if (ntbCheck._isPrint)
                                {

                                    //JObject jResult = new JObject();
                                    if (DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                                    {
                                        Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                                    }
                                    else
                                    {
                                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                    }

                                    //DBConnect.printNtbCheck1stEdition(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, ProjectInfo._printerPort);
                                }

                                ProjectInfo._isExistNtbCheckWarehousing = true;
                            }

                        }
                    }
                }
            }
            else if (checkValue == 3)
            {
                //using (DlgAllInOneCheck2 allInOneCheck = new DlgAllInOneCheck2(ProjectInfo._dicAllInOneCheckWarehousing, _dtPrintPort))
                //{
                //    if (allInOneCheck.ShowDialog(this) == DialogResult.OK)
                //    {
                //        DBConnect.insertAllInOneCheck(barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckWarehousing, listInventoryId);

                //        if (allInOneCheck._isPrint)
                //            DBConnect.printAllInOneCheck(barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckWarehousing, ProjectInfo._printerPort);
                //    }
                //}

                using (DlgAllInOneCheck AllInOneCheck = new DlgAllInOneCheck(ProjectInfo._dicAllInOneCheckWarehousing, null, ref ProjectInfo._dtAllInOneAdjustmentPrice, ProjectInfo._dicAllInOneAdjustmentPrice, 
                    ProjectInfo._etcWarehousing,ProjectInfo._allInOneProductGradeWarehousing, _dtPrintPort, _dtPGrade, _bsPallet, ConvertUtil.ToInt64(lePallet.EditValue), _checkType))
                {
                    if (AllInOneCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        if (ProjectInfo._isExistAllInOneCheckRepair)
                        {
                            Dangol.Message($"리페어정보가 존재하는 제품은 검수 체크를 변경할 수 없습니다.");
                            return;
                        }

                        ProjectInfo._etcWarehousing = AllInOneCheck._etcDes;
                        ProjectInfo._allInOneProductGradeWarehousing = AllInOneCheck._pGrade;

                        //DBConnect.updatePallet(listInventoryId, warehouse, AllInOneCheck._palletNo);

                        //foreach (DataRow dr in row)
                        //{
                        //    if (ConvertUtil.ToInt64(dr["WAREHOUSE"]) == warehouse)
                        //    {
                        //        dr.BeginEdit();
                        //        dr["PALLET"] = $"{AllInOneCheck._palletNo}";
                        //        dr.EndEdit();
                        //    }
                        //}

                        if (DBConnect.insertAllInOneCheck(_representativeType, _representativeNo, _representativeCol, barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckWarehousing, listInventoryId, ProjectInfo._etcWarehousing, ProjectInfo._allInOneProductGradeWarehousing))
                        {
                            //JObject jResult = new JObject();

                            if (!DBAdjustment.insertAdjustmentAllInOnePrice(inventoryId, _checkType, ref jResult))
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }

                            if (AllInOneCheck._isPrint)
                            {
                                if (DBConnect.printAllInOneProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                                {
                                    Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                                }
                                else
                                {
                                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                }

                                //DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, ProjectInfo._printerPort);
                            }

                            ProjectInfo._isExistAllInOneCheckWarehousing = true;
                        }
                    }
                }
            }
            else if (checkValue == 5)
            {
                //using (DlgTabletCheck AllInOneCheck = new DlgTabletCheck(ProjectInfo._dicTabletCheck, null, ProjectInfo._etcWarehousing,
                //   ProjectInfo._allInOneProductGradeWarehousing, ProjectInfo._batteryRemainWarehousing, "", _dtPrintPort, _dtPGrade, _bsPallet, ConvertUtil.ToInt64(lePallet.EditValue), _checkType))
                //{
                //    if (AllInOneCheck.ShowDialog(this) == DialogResult.OK)
                //    {
                //        ProjectInfo._etcWarehousing = AllInOneCheck._etcDes;
                //        ProjectInfo._allInOneProductGradeWarehousing = AllInOneCheck._pGrade;
                //        ProjectInfo._batteryRemainWarehousing = AllInOneCheck._batteryRemain;
                //    }
                //}
            }
        }

        private void sbPartCheck_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            if (!_isWarehousingCheck)
            {
                if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
                {
                    return;
                }
                _isWarehousingCheck = true;
            }

            if(string.IsNullOrEmpty(_barcode) || _inventoryId < 0)
            {
                Dangol.Message("선택하신 부품은 재고로 등록되지 않았습니다.");
                return;
            }

            if (!ProjectInfo._dicPartCheckWarehousing.ContainsKey(_inventoryId))
                getCheckInfo(_inventoryId, _currentComponentCd);

            Dictionary<string, int> dicCheckInfo = ProjectInfo._dicPartCheckWarehousing[_inventoryId];


            if (_currentComponentCd.Equals("MON"))
            {
                string size = ProjectInfo._dicMonSizeWarehousing[_inventoryId];

                using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(dicCheckInfo, 
                    ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    size,
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (monitorCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = monitorCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = monitorCheck._pGrade;
                        ProjectInfo._dicMonSizeWarehousing[_inventoryId] = monitorCheck._size;
                        DBConnect.insertMonitorCheck(_barcode, _checkType, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._dicMonSizeWarehousing[_inventoryId]);

                        if (monitorCheck._isPrint)
                            DBConnect.printMonitorCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._dicMonSizeWarehousing[_inventoryId], ProjectInfo._printerPort);


                        string des = monitorCheck._description;
                        long adjustPrice = 0;
                        if (dicCheckInfo["DISPLAY"] > 0)
                        {
                            JObject jobj = new JObject();
                            JObject jresult = new JObject();
                            jobj.Add("INVENTORY_ID", _inventoryId);
                            jobj.Add("INVENTORY_CAT", "F");
                            jobj.Add("DES", des);
                            jobj.Add("INIT_PRICE", 0);
                            jobj.Add("ADJUST_PRICE", 0);

                            DBConnect.updateInventoryInfoDetail(jobj, ref jresult);

                        }
                        else
                        {
                            List<string> _monCase = new List<string>(new string[] { "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED" });

                            if (dicCheckInfo["PORT"] > 0)
                            {
                                adjustPrice += -5000;
                            }

                            if (dicCheckInfo["ADAPTER"] > 0)
                            {
                                adjustPrice += -5000;
                            }

                            foreach (string col in _monCase)
                                if (dicCheckInfo[col] > 0)
                                {
                                    adjustPrice += -5000;
                                    break;
                                }


                            if(adjustPrice < 0)
                            {
                                JObject jobj = new JObject();
                                JObject jresult = new JObject();
                                jobj.Add("INVENTORY_ID", _inventoryId);
                                jobj.Add("INVENTORY_CAT", "B");
                                jobj.Add("DES", des);
                                jobj.Add("ADJUST_PRICE", adjustPrice);

                                DBConnect.updateInventoryInfoDetail(jobj, ref jresult);
                            }
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("CAS"))
            {
                using (DlgCasCheck casCheck = new DlgCasCheck(dicCheckInfo,
                    ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (casCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = casCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = casCheck._pGrade;

                        DBConnect.insertCasCheck(_barcode, _checkType, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                        if (casCheck._isPrint)
                            DBConnect.printCasCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("CPU"))
            {
                using (DlgCpuCheck inventoryCheck = new DlgCpuCheck(dicCheckInfo,
                    ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);


                        string des = inventoryCheck._description;

                        if (dicCheckInfo["FAULT"] > 0)
                        {
                            JObject jobj = new JObject();
                            JObject jresult = new JObject();
                            jobj.Add("INVENTORY_ID", _inventoryId);
                            jobj.Add("INVENTORY_CAT", "F");
                            jobj.Add("DES", des);
                            jobj.Add("INIT_PRICE", 0);
                            jobj.Add("ADJUST_PRICE", 0);

                            DBConnect.updateInventoryInfoDetail(jobj, ref jresult);
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("MEM"))
            {
                using (DlgMEMCheck inventoryCheck = new DlgMEMCheck(dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);

                        string des = inventoryCheck._description;

                        if (dicCheckInfo["FAULT"] > 0)
                        {
                            JObject jobj = new JObject();
                            JObject jresult = new JObject();
                            jobj.Add("INVENTORY_ID", _inventoryId);
                            jobj.Add("INVENTORY_CAT", "F");
                            jobj.Add("DES", des);
                            jobj.Add("INIT_PRICE", 0);
                            jobj.Add("ADJUST_PRICE", 0);

                            DBConnect.updateInventoryInfoDetail(jobj, ref jresult);
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("MBD"))
            {
                using (DlgMbdCheck inventoryCheck = new DlgMbdCheck(dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);

                        string des = inventoryCheck._description;

                        if (dicCheckInfo["FAULT"] > 0)
                        {
                            JObject jobj = new JObject();
                            JObject jresult = new JObject();
                            jobj.Add("INVENTORY_ID", _inventoryId);
                            jobj.Add("INVENTORY_CAT", "F");
                            jobj.Add("DES", des);
                            jobj.Add("INIT_PRICE", 0);
                            jobj.Add("ADJUST_PRICE", 0);

                            DBConnect.updateInventoryInfoDetail(jobj, ref jresult);
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("VGA"))
            {
                using (DlgVgaCheck inventoryCheck = new DlgVgaCheck(dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);

                        string des = inventoryCheck._description;

                        if (dicCheckInfo["FAULT"] > 0)
                        {
                            JObject jobj = new JObject();
                            JObject jresult = new JObject();
                            jobj.Add("INVENTORY_ID", _inventoryId);
                            jobj.Add("INVENTORY_CAT", "F");
                            jobj.Add("DES", des);
                            jobj.Add("INIT_PRICE", 0);
                            jobj.Add("ADJUST_PRICE", 0);

                            DBConnect.updateInventoryInfoDetail(jobj, ref jresult);
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("STG"))
            {
                string type = ConvertUtil.ToString(_currentRow["STG_TYPE"]);

                if (type.Contains("SSD"))
                {
                    using (DlgSsdCheck inventoryCheck = new DlgSsdCheck(dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                    {
                        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                            ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                            if (inventoryCheck._isPrint)
                                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);

                            string des = inventoryCheck._description;

                            if (dicCheckInfo["FAULT"] > 0)
                            {
                                JObject jobj = new JObject();
                                JObject jresult = new JObject();
                                jobj.Add("INVENTORY_ID", _inventoryId);
                                jobj.Add("INVENTORY_CAT", "F");
                                jobj.Add("DES", des);
                                jobj.Add("INIT_PRICE", 0);
                                jobj.Add("ADJUST_PRICE", 0);

                                DBConnect.updateInventoryInfoDetail(jobj, ref jresult);
                            }
                        }
                    }
                }
                else
                {
                    using (DlgHddCheck inventoryCheck = new DlgHddCheck(dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                    {
                        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                            ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                            if (inventoryCheck._isPrint)
                                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);

                            string des = inventoryCheck._description;

                            if (dicCheckInfo["FAULT"] > 0)
                            {
                                JObject jobj = new JObject();
                                JObject jresult = new JObject();
                                jobj.Add("INVENTORY_ID", _inventoryId);
                                jobj.Add("INVENTORY_CAT", "F");
                                jobj.Add("DES", des);
                                jobj.Add("INIT_PRICE", 0);
                                jobj.Add("ADJUST_PRICE", 0);

                                DBConnect.updateInventoryInfoDetail(jobj, ref jresult);
                            }
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("ODD"))
            {
                using (DlgOddCheck inventoryCheck = new DlgOddCheck(dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("POW"))
            {
                using (DlgPowCheck inventoryCheck = new DlgPowCheck(dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId],
                    ProjectInfo._dicInventoryGradeWarehousing[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesWarehousing[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeWarehousing[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesWarehousing[_inventoryId], ProjectInfo._dicInventoryGradeWarehousing[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                return;
            }
        }

        private void sbClear_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN($"관리번호를 초기화하시겠습니까?") == DialogResult.No)
                return;

            DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_currentComponentCd];
            DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
            JObject jResult = new JObject();


            if (rowDeviceInfo.Length > 0)
            {
                ProjectInfo._dicPartCheckWarehousing.Remove(_inventoryId);

                ProjectInfo._dicInventoryDesWarehousing.Remove(_inventoryId);
                ProjectInfo._dicInventoryGradeWarehousing.Remove(_inventoryId);

                if (_currentComponentCd.Equals("MON"))
                    ProjectInfo._dicMonSizeWarehousing.Remove(_inventoryId);

                if (_currentComponentCd.Equals("MBD"))
                {
                    ProjectInfo._dicNtbCheckWarehousing.Clear();
                    ProjectInfo._dicAllInOneCheckWarehousing.Clear();

                    ProjectInfo._caseDestroyDescriptionWarehousing = "";
                    ProjectInfo._batteryRemainWarehousing = "";
                    ProjectInfo._productGradeWarehousing = "";

                    ProjectInfo._etcWarehousing = "";
                    ProjectInfo._allInOneProductGradeWarehousing = "";

                    ProjectInfo._isExistNtbCheckWarehousing = false;
                    ProjectInfo._isExistNtbCheckRepair = false;

                    ProjectInfo._isExistAllInOneCheckWarehousing = false;
                    ProjectInfo._isExistAllInOneCheckRepair = false;

                    ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
                    foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                        ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;

                    ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
                    foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                        ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType][col] = 0;

                    foreach (DataRow dr in ProjectInfo._dtDeviceInfo.Rows)
                        dr["PRODUCT_YN"] = false;
                }

                _currentRow.BeginEdit();
                _currentRow["INVENTORY_ID"] = dtComponentInfo.Rows[0]["INVENTORY_ID"] = -1;
                _currentRow["BARCODE"] = dtComponentInfo.Rows[0]["BARCODE"] = "";
                _currentRow["INVENTORY_YN"] = false;
                _currentRow["PRODUCT_YN"] = false;
                _currentRow["RELEASE_YN"] = false;
                _currentRow["RELEASE_RESULT"] = "";
                _currentRow.EndEdit();

                _barcode = "";
                _inventoryId = -1;

                setBarcodeButton(false, false, true);
                checkPictureState();
            };
        }

        private void sbGetDeviceInfo_Click(object sender, EventArgs e)
        {
            if(teBarcode.EditValue == null || teBarcode.EditValue.ToString().Length < 12)
            {
                Dangol.Message("관리번호를 입력하세요.");
                return;
            }
            if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible) { }
            else SplashScreenManager.CloseForm();
            SplashScreenManager.ShowForm(typeof(WareHousingMaster.Helper.DlgWaitForm));
            SplashScreenManager.Default.SetWaitFormCaption($"입고");
            SplashScreenManager.Default.SetWaitFormDescription("부품 정보 가져오는 중...");

            string barcode = teBarcode.EditValue.ToString();
            DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_currentComponentCd];
            DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
            JObject jResult = new JObject();

            if (rowDeviceInfo.Length > 0)
            {
                if (DBConnect.getInventory(barcode, _currentComponentCd, rowDeviceInfo[0], ref jResult))
                {
                    _currentRow.BeginEdit();
                    DataRow[] rowBarcode = ProjectInfo._dtDeviceInfo.Select($"BARCODE = '{barcode}'");

                    if (rowBarcode.Length > 0)
                    {
                        SplashScreenManager.CloseForm();
                        Dangol.Message("이미 현재 제품에 존재하는 부품입니다.");
                        return;
                    }

                    JObject jData = (JObject)jResult["DATA"];

                    _currentRow["INVENTORY_ID"] = dtComponentInfo.Rows[0]["INVENTORY_ID"] = jData["INVENTORY_ID"];
                    _currentRow["BARCODE"] = dtComponentInfo.Rows[0]["BARCODE"] = jData["BARCODE"];
                    _currentRow["INVENTORY_YN"] = true;


                    DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];
                    string specNm;
                    foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                    {
                        dr.BeginEdit();
                        specNm = ConvertUtil.ToString(dr["SPEC_NM"]);
                        if(jData.ContainsKey(specNm))
                        {
                            dr["MATCHING_INFO"] = ConvertUtil.ToString(jData[specNm]);
                        }
                        else
                            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                        dr.EndEdit();
                    }

                    gcInventoryDetail.DataSource = null;
                    bsDetail.DataSource = dtDeviceInfoFromDB;
                    gcInventoryDetail.DataSource = bsDetail;
                    

                    _barcode = ConvertUtil.ToString(jData["BARCODE"]);
                    _inventoryId = ConvertUtil.ToInt64(jData["INVENTORY_ID"]);

                    getCheckInfo(_inventoryId, _currentComponentCd);
                    setBarcodeButton(true, true, false);


                    if (_currentComponentCd.Equals("MBD"))
                    {
                        Util.checkProductState(_inventoryId, _barcode);
                        Util.getEtcComponent();
                        Util.checkProductRemainPart();

                        setNtbControl();
                        //getWarehousing();
                    }
                    else
                    {
                        if (ProjectInfo._dicProductList.ContainsKey(_inventoryId))
                            _currentRow["PRODUCT_YN"] = true;
                    }

                    if (ProjectInfo._listReleaseList != null)
                        if (ProjectInfo._listReleaseList.Contains(_inventoryId))
                        {
                            _currentRow["RELEASE_YN"] = true;
                            _currentRow["RELEASE_RESULT"] = "정상 등록";
                        }

                    _currentRow.EndEdit();
                    checkPictureState();

                    SplashScreenManager.CloseForm();
                    Dangol.Message("부품 정보를 변경하였습니다.");
                }
                else
                {
                    checkPictureState();
                    SplashScreenManager.CloseForm();
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private void setBarcodeButton(bool teBarcodeFlag, bool sbClearFlag, bool sbGetDeviceInfoFlag)
        {
            teBarcode.Properties.ReadOnly = teBarcodeFlag;
            sbClear.Enabled = sbClearFlag;
            sbGetDeviceInfo.Enabled = sbGetDeviceInfoFlag;
        }

        private void leProductType_EditValueChanged(object sender, EventArgs e)
        {
            ProjectInfo._type = ConvertUtil.ToInt32(leProductType.EditValue);
            ProjectInfo._typeNm = leProductType.Text;

            setNtbControl();
        }

        private void leComponentCd_EditValueChanged(object sender, EventArgs e)
        {
            //_componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
            //if (!string.IsNullOrEmpty(_componentCd))
            //    setGridControl();
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            if (_componentCd.Equals("ALL"))
            {
                Dangol.Message($"부품 추가는 개별 부품 화면에서만 가능합니다.");
                return;
            }

            if (Dangol.MessageYN($"{_componentCd} 부품을 추가하시겠습니까?") == DialogResult.Yes)
            {

            }
        }

        private void sbSelectAdd_Click(object sender, EventArgs e)
        {

            if (leComponentCd.EditValue == null)
            {
                Dangol.Message("품목을 선택하세요");
                return;
            }

            long representativeId = -1;
            JObject jResult = new JObject();

            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate, ref jResult))
            {
                return;
            }

            representativeId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);

            string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
  
            using (dlgSelectComponent createComp = new dlgSelectComponent(_representativeType, _representativeCol, _representativeNo, representativeId, _representativeIdCol, _type, componentCd, _dtPrintPort))
            {
                if (createComp.ShowDialog(this) == DialogResult.OK)
                {
                    gvInventory.BeginDataUpdate();

                    Util.putData(createComp.jPartInfo, componentCd);

                    gvInventory.EndDataUpdate();
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

            long representativeId = -1;
            JObject jResult = new JObject();

            if (!checkRepresentativeInfo())
            {
                Dangol.Warining("입고번호를 입력해주세요.");
                return;
            }

            if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate, ref jResult))
            {
                return;
            }

            representativeId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);

            string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);

            switch (componentCd)
            {
                case "ADP":
                    using (dlgCreateADP createAdp = new dlgCreateADP(_representativeType, _representativeCol, _representativeNo, representativeId, _representativeIdCol, _type, _dtPrintPort))
                    {
                        if (createAdp.ShowDialog(this) == DialogResult.OK)
                        {
                            //sbUpdateComponent_Click(null, null);
                        }
                    }
                    break;
                case "POW":
                    using (dlgCreatePOW createPow = new dlgCreatePOW(_representativeType, _representativeCol, _representativeNo, representativeId, _representativeIdCol, _type, _dtPrintPort))
                    {
                        if (createPow.ShowDialog(this) == DialogResult.OK)
                        {
                            //sbUpdateComponent_Click(null, null);

                            gvInventory.BeginDataUpdate();

                            Util.putData(createPow.jPartInfo, componentCd);

                            gvInventory.EndDataUpdate();
                        }
                    }
                    break;
                default:
                    Dangol.Warining("품목을 선택하세요");
                    break;

            }
        }

        private void sbPlaceUpdate_Click(object sender, EventArgs e)
        {
            long inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);

            if (inventoryId < 0)
            {
                Dangol.Message("등록되지 않은 재고입니다.");
                return;
            }
            else
            {
                List<long> listInventoryId = new List<long>();
                listInventoryId.Add(inventoryId);
                if(DBConnect.updateLocation(listInventoryId, ConvertUtil.ToInt64(leLocation.EditValue), ConvertUtil.ToInt64(lePallet.EditValue)))
                {
                    _currentRow.BeginEdit();

                    _currentRow["WAREHOUSE"] = ConvertUtil.ToString(leLocation.EditValue);
                    _currentRow["PALLET"] = ConvertUtil.ToString(lePallet.EditValue);

                    _currentRow.EndEdit();

                    Dangol.Message("수정되었습니다.");
                }
                else
                {
                    Dangol.Error("오류가 발생했습니다. 관리자에게 문의하세요. ERROR: WUP01");
                }
            }

            
        }

        private void leNickName_EditValueChanged(object sender, EventArgs e)
        {
            ProjectInfo._ntbListId = ConvertUtil.ToInt64(leNickName.EditValue);
        }

        private void leManufactureType_EditValueChanged(object sender, EventArgs e)
        {
            ProjectInfo._ntbManufactureType = ConvertUtil.ToInt32(leManufactureType.EditValue);
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            DataRow[] rowMBD = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");

            if (rowMBD.Length < 0)
            {
                Dangol.Message("메인보드 정보가 없습니다.");
                return;
            }

            long inventoryId = ConvertUtil.ToInt64(rowMBD[0]["INVENTORY_ID"]);

            if(inventoryId < 0)
            {
                Dangol.Message("등록되지 않은 재고입니다.");
                return;
            }

            if (ProjectInfo._dicProductList.Count < 1)
            {
                Dangol.Message("제품으로 등록되지 않은 재고입니다.");
                return;
            }

            if (Dangol.MessageYN("변경사항을 저장하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();

                var jArray = new JArray();
                JObject jobj = new JObject();
                JObject jResult = new JObject();

                jobj.Add("BULK_YN", 0);

                JObject jdata = new JObject();
                jdata.Add("INVENTORY_ID", inventoryId);
                jdata.Add("MANUFACTURE_TYPE", ConvertUtil.ToString(ProjectInfo._ntbManufactureType));
                jdata.Add("NTB_LIST_ID", ConvertUtil.ToInt64(ProjectInfo._ntbListId));
                jArray.Add(jdata);
                
                jobj.Add("DATA", jArray);

                if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                {
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

        private void sbProductPrint_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("입고번호를 입력해주세요.");
                return;
            }

            int checkValue = ConvertUtil.ToInt32(leProductType.EditValue);

            if (checkValue == 1)
            {
                Dangol.Message("PC는 개별 부품 출력만 가능합니다.");
                return;
            }

            DataRow[] row = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");
            string barcode = null;
            long inventoryId = -1;
            List<long> listInventoryId = new List<long>();

            if (row.Length > 0)
            {
                barcode = ConvertUtil.ToString(row[0]["BARCODE"]);
                inventoryId = ConvertUtil.ToInt64(row[0]["INVENTORY_ID"]);

                if (string.IsNullOrEmpty(barcode) || inventoryId < 1)
                {
                    Dangol.Message("메인보드가 재고로 등록되지 않았습니다.");
                    return;
                }
            }

            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

            string checkText = leProductType.Text;

            // 우선은 잠시 숨겨둠

            long warehouse = ConvertUtil.ToInt64(leWarehousing2.EditValue);

            if (ProjectInfo._type != checkValue)
            {
                if (Dangol.MessageYN($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{ProjectInfo._typeNm}, 선택:{checkText})", $"{checkText} 검수 체크") == DialogResult.No)
                    return;
            }

            JObject jResult = new JObject();

            if (checkValue == 2)
            {
                if (DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                {
                    Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
                
            }
            else if (checkValue == 3)
            {         
                if (DBConnect.printAllInOneProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                {
                    Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }              
            }
            else if (checkValue == 5)
            {

            }
        }

        private void sbPartPrint_Click(object sender, EventArgs e)
        {
           
        }

        private void checkPictureState()
        {
            if (_currentRow == null)
            {
                lcCheckPicture.Enabled = false;
                lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                if (ConvertUtil.ToBoolean(_currentRow["INVENTORY_YN"]))
                {
                    lcCheckPicture.Enabled = true;

                    if (_currentComponentCd.Equals("MBD") && ConvertUtil.ToBoolean(_currentRow["PRODUCT_YN"]))
                        lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    else
                        lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    lcCheckPicture.Enabled = false;
                    lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }

        }

        private void sbCheckPicture_Click(object sender, EventArgs e)
        {
            if (ConvertUtil.ToBoolean(_currentRow["INVENTORY_YN"]))
            {
                ImageInfo.GetImage(1, false, _barcode);
            }
        }

        private void sbCheckProductPicture_Click(object sender, EventArgs e)
        {
            if (ConvertUtil.ToBoolean(_currentRow["INVENTORY_YN"]) && _currentComponentCd.Equals("MBD") && ConvertUtil.ToBoolean(_currentRow["PRODUCT_YN"]))
            {
                ImageInfo.GetImage(1, true, _barcode);
            }
        }
    }
}