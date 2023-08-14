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
using System.Diagnostics;
using ScreenCopy;
using WareHousingMaster.UtilTest;
using System.IO;
using WareHousingMaster.view.PreView;

namespace WareHousingMaster.view.release
{
    public partial class usrReleaseConsigned : DevExpress.XtraEditors.XtraForm
    {
        string _representativeType = "P";
        string _representativeCol = "RECEIPT";
        string _representativeNo = null;
        object _proxyId = -1;
        object _companyId = -1;
        short _examCheckType = 1;
        short _checkType = 5;
        int _proxyState = 0;

        string _componentCd = "ALL";
        GridColumn[] arrGridColumn;
        GridColumn[] arrGridColumnReceipt;

        DataRowView _currentRow;

        BindingSource bs;
        BindingSource bsDetail;
        BindingSource _bsPallet;
        long _id;
        bool _headerButtonVisible = true;

        string _barcode = null;
        string _component = null;
        string _currentComponentCd = null;
        long _inventoryId = -1;
        long _componentId = -1;
        int _consignedType = -1;
        bool _isWarehousingCheck = false;

        DateTime _warehousingDate = new DateTime();
        DataTable _dtPrintPort;
        DataTable _dtPGrade;
        DataTable _dtPallet;

        DataTable _dtCompare;

        bool memCheck;
        bool stgCheck;

        int _partRelease;
        int _preRelease;

        int _cpuAssignYn;
        public usrReleaseConsigned()
        {
            InitializeComponent();

            arrGridColumn = new GridColumn[3] { gc1, gc2, gc3 };
            arrGridColumnReceipt = new GridColumn[3] { gcc1, gcc2, gcc3 };

            bs = new BindingSource();
            bsDetail = new BindingSource();
            _bsPallet = new BindingSource();

            bs.DataSource = ProjectInfo._dtConsignedInfo;
            //tnReceipt.EditValue = "LT201130001";
            lcComponent.Text = _componentCd;

            if(ProjectInfo._dicConsignedInfoDetail == null)
            {
                ProjectInfo._dicConsignedInfoDetail = new Dictionary<long, DataTable>();
            }

            _dtCompare = new DataTable();
            _dtCompare.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtCompare.Columns.Add(new DataColumn("DATA1", typeof(string)));
            _dtCompare.Columns.Add(new DataColumn("DATA2", typeof(string)));
            _dtCompare.Columns.Add(new DataColumn("DATA3", typeof(string)));
            _dtCompare.Columns.Add(new DataColumn("REP1", typeof(string)));
            _dtCompare.Columns.Add(new DataColumn("REP2", typeof(string)));
            _dtCompare.Columns.Add(new DataColumn("REP3", typeof(string)));

            memCheck = false;
            stgCheck = false;
        }


        private void getCheckInfoInit()
        {
            ProjectInfo._dicPartCheckRelease = new Dictionary<long, Dictionary<string, int>>();
            ProjectInfo._dicInventoryDesRelease = new Dictionary<long, string>();
            ProjectInfo._dicInventoryGradeRelease = new Dictionary<long, string>();
            ProjectInfo._dicMonSizeRelease = new Dictionary<long, string>();

            DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");
            JObject jResult = new JObject();

            long mbdInventoryId = -1;


            foreach (DataRow row in rows)
            {
                long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                string componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                if (componentCd.Equals("MBD"))
                    mbdInventoryId = inventoryId;

                if (!ProjectInfo._dicPartCheckRelease.ContainsKey(inventoryId))
                {
                    Dictionary<string, int> dicData = new Dictionary<string, int>();
                    ProjectInfo._dicInventoryDesRelease.Add(inventoryId, "");
                    ProjectInfo._dicInventoryGradeRelease.Add(inventoryId, "0");
                    ProjectInfo._dicMonSizeRelease.Add(inventoryId, "");

                    if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                    {
                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            { 
                                if (name.Equals("DES"))
                                    ProjectInfo._dicInventoryDesRelease[inventoryId] = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._dicInventoryGradeRelease[inventoryId] = x.Value.ToObject<string>();
                                else if (name.Equals("SIZE"))
                                    ProjectInfo._dicMonSizeRelease[inventoryId] = x.Value.ToObject<string>();
                                else
                                {
                                    int value = x.Value.ToObject<int>();

                                    if (!dicData.ContainsKey(name))
                                        dicData.Add(name, value);
                                }
                            }
                        }
                    }

                    ProjectInfo._dicPartCheckRelease.Add(inventoryId, dicData);
                }
            }
 
            ProjectInfo._dicNtbCheckRelease = new Dictionary<string, short>();
            ProjectInfo._dicAllInOneCheckRelease = new Dictionary<string, short>();
            ProjectInfo._dicNtbCheckWarehousing = new Dictionary<string, short>();

            ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType]["EXIST"] = false;
            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType][col] = 0;

            ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;

            ProjectInfo._isExistNtbCheckWarehousing = false;
            ProjectInfo._isExistNtbCheckRelease = false;

            if (mbdInventoryId > 0)
            {
                if (DBConnect.getCheckInfo(mbdInventoryId, "NTB", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                ProjectInfo._caseDestroyDescriptionRelease = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                ProjectInfo._batteryRemainRelease = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._productGradeRelease = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                ProjectInfo._dicNtbCheckRelease.Add(name, value);
                            }
                        }
                    }
                }

                if (DBConnect.getCheckInfo(mbdInventoryId, "NTB", _examCheckType, ref jResult))
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

                            if (!ProjectInfo._isExistNtbCheckRelease)
                            {
                                if (name.Equals("CASE_DES"))
                                    ProjectInfo._caseDestroyDescriptionRelease = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN"))
                                    ProjectInfo._batteryRemainRelease = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._productGradeRelease = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                        ProjectInfo._dicNtbCheckRelease.Add(name, value);
                                }
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                            ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }
                }

                if (DBConnect.getCheckInfo(mbdInventoryId, "ALLINONE", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            short value = x.Value.ToObject<short>();

                            if (!ProjectInfo._dicAllInOneCheckRelease.ContainsKey(name))
                                ProjectInfo._dicAllInOneCheckRelease.Add(name, value);
                        }
                    }
                }
            }
        }

        private void getCheckInfo(long inventoryId, string componentCd)
        {
            JObject jResult = new JObject();

            if (!ProjectInfo._dicPartCheckRelease.ContainsKey(inventoryId))
            {
                Dictionary<string, int> dicData = new Dictionary<string, int>();
                ProjectInfo._dicInventoryDesRelease.Add(inventoryId, "");
                ProjectInfo._dicInventoryGradeRelease.Add(inventoryId, "0");
                ProjectInfo._dicMonSizeRelease.Add(inventoryId, "");

                if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("DES"))
                                ProjectInfo._dicInventoryDesRelease[inventoryId] = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._dicInventoryGradeRelease[inventoryId] = x.Value.ToObject<string>();
                            else if (name.Equals("SIZE"))
                                ProjectInfo._dicMonSizeRelease[inventoryId] = x.Value.ToObject<string>();
                            else
                            {
                                int value = x.Value.ToObject<int>();

                                if (!dicData.ContainsKey(name))
                                    dicData.Add(name, value);
                            }
                        }
                    }
                }

                ProjectInfo._dicPartCheckRelease.Add(inventoryId, dicData);
            }

            if (componentCd.Equals("MBD"))
            {
                if (DBConnect.getCheckInfo(inventoryId, "NTB", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                ProjectInfo._caseDestroyDescriptionRelease = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                ProjectInfo._batteryRemainRelease = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._productGradeRelease = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                    ProjectInfo._dicNtbCheckRelease.Add(name, value);
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

                    ProjectInfo._isExistNtbCheckRelease = true;
                }

                if (DBConnect.getCheckInfo(inventoryId, "NTB", _examCheckType, ref jResult))
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

                            if (!ProjectInfo._isExistNtbCheckRepair)
                            {
                                if (name.Equals("CASE_DES"))
                                    ProjectInfo._caseDestroyDescriptionRelease = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN"))
                                    ProjectInfo._batteryRemainRelease = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._productGradeRelease = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                        ProjectInfo._dicNtbCheckRelease.Add(name, value);
                                }
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                            ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }

                    ProjectInfo._isExistNtbCheckWarehousing = true;
                }

                if (DBConnect.getCheckInfo(inventoryId, "ALLINONE", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            short value = x.Value.ToObject<short>();

                            if (!ProjectInfo._dicAllInOneCheckRelease.ContainsKey(name))
                                ProjectInfo._dicAllInOneCheckRelease.Add(name, value);
                        }
                    }
                }
            }
        }



        private void usrRelease_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            string serialNo = ProjectInfo.osProductNo;
            serialNo = serialNo.Replace("-", "");
            serialNo = serialNo.Substring(1, 14);
            teSerialNo.Text = serialNo;
            teSerialNo.EditValue = serialNo;

            teBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
            //leLocation.DataBindings.Add(new Binding("EditValue", bs, "WAREHOUSE", false, DataSourceUpdateMode.Never));
            //lePallet.DataBindings.Add(new Binding("EditValue", bs, "PALLET", false, DataSourceUpdateMode.Never));
            //teSerialNo.DataBindings.Add(new Binding("Text", bs, "RELEASE_RESULT", false, DataSourceUpdateMode.Never));
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

            DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
            Util.LookupEditHelper(leLocation, dtLocation, "KEY", "VALUE");

            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            Util.LookupEditHelper(lePallet, _dtPallet, "PALLET_ID", "PALLET_NM");
            _bsPallet.DataSource = _dtPallet;

            DataTable dtEctComponentCd = new DataTable();

            dtEctComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtEctComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for(int i = 0; i < ProjectInfo._listUncheckComponentCd.Count; i++ )
            { 
                DataRow dr = dtEctComponentCd.NewRow();

                dr["KEY"] = ProjectInfo._listUncheckComponentCd[i];
                dr["VALUE"] = ProjectInfo._listUncheckComponentCdNm[i];
                dtEctComponentCd.Rows.Add(dr);
            }

            Util.LookupEditHelper(leComponentCd, dtEctComponentCd, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustomLS("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(leNickName, dtNickName, "KEY", "VALUE");

            DataTable dtManufactureType = new DataTable();

            dtManufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtManufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtManufactureType, 2, "외산/기타");
            Util.insertRowonTop(dtManufactureType, 1, "삼성/엘지");
            Util.insertRowonTop(dtManufactureType, -1, "알수없음");
            Util.LookupEditHelper(leManufactureType, dtManufactureType, "KEY", "VALUE");

            _dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");

            DataTable dtCaptureType = new DataTable();

            dtCaptureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCaptureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtCaptureType, 2, "2");
            Util.insertRowonTop(dtCaptureType, 1, "1");
            Util.LookupEditHelper(leCaptureType, dtCaptureType, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            // 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;
            //leLocation.EditValue = ProjectInfo._location;
            leProductType.EditValue = ProjectInfo._type;
            teUserName.EditValue = ProjectInfo._userName;

            List<string> listCol = ProjectInfo._dicDeviceConsignedColumn["ALL"];
            for (int i = 0; i < Math.Min(listCol.Count, arrGridColumn.Length); i++)
            {
                arrGridColumn[i].FieldName = $"DATA{i + 1}";
                arrGridColumnReceipt[i].FieldName = $"REP{i + 1}";
            }

            leCaptureType.EditValue = 1;
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

        private void gvInventory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvInventory.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                _id = ConvertUtil.ToInt64(_currentRow["ID"]);
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _component = ConvertUtil.ToString(_currentRow["COMPONENT"]);
                _inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);
                _componentId = ConvertUtil.ToInt64(_currentRow["COMPONENT_ID"]);
                _currentComponentCd = ConvertUtil.ToString(_currentRow["COMPONENT_CD"]);

                if (string.IsNullOrEmpty(_barcode) || _inventoryId < 0)
                    setBarcodeButton(false, false, true);
                else
                    setBarcodeButton(true, true, false);

                gcInventoryDetail.DataSource = null;
                bsDetail.DataSource = ProjectInfo._dicConsignedInfoDetail[_id];
                gcInventoryDetail.DataSource = bsDetail;

                if (ConvertUtil.ToInt32(_currentRow["CONSIGNED_TYPE"]) == 2)
                {
                    gvInventory.Appearance.FocusedRow.ForeColor = Color.Red;
                    gvInventory.Appearance.FocusedCell.ForeColor = Color.Red;
                }
                else
                {
                    gvInventory.Appearance.FocusedRow.ForeColor = Color.Black;
                    gvInventory.Appearance.FocusedCell.ForeColor = Color.Black;
                }

                if (ConvertUtil.ToInt64(_currentRow["PROXY_PART_ID"]) > 0)
                {
                    gcWarehousing.OptionsColumn.AllowEdit = true;
                    gcPrice.OptionsColumn.AllowEdit = true;
                }
                else
                {
                    gcWarehousing.OptionsColumn.AllowEdit = false;
                    gcPrice.OptionsColumn.AllowEdit = false;
                }

                _bsPallet.Filter = $"WAREHOUSE_ID = '{_currentRow["WAREHOUSE"]}'";
            }
        }

        private void setGridControl()
        {
            gcInventory.DataSource = null;

            List<string> listColnm = ProjectInfo._dicDeviceConsignedColumnNm[_componentCd];
            List<string> listCol = ProjectInfo._dicDeviceConsignedColumn[_componentCd];

            lcComponent.Text = _componentCd;

            for (int i = 0; i < Math.Min(listCol.Count, arrGridColumn.Length); i++)
            {
                arrGridColumn[i].Caption = listColnm[i];
                //arrGridColumn[i].FieldName = listCol[i];

                arrGridColumnReceipt[i].Caption = listColnm[i];
                //arrGridColumnReceipt[i].FieldName = listCol[i];               
            }
            if (_componentCd.Equals("ALL"))
            {
                //for (int i = 0; i < Math.Min(listCol.Count, arrGridColumn.Length); i++)
                //{
                //    arrGridColumn[i].FieldName = $"DATA{i+1}";
                //}

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
                for(int i = 0; i< lcgInventory.CustomHeaderButtons.Count; i++)
                    lcgInventory.CustomHeaderButtons[i].Properties.Visible = true;
                gcCheck.Visible = true;
            }
            else if (!_componentCd.Equals("ALL") && _headerButtonVisible)
            {
                _headerButtonVisible = false;
                for (int i = 0; i < lcgInventory.CustomHeaderButtons.Count; i++)
                    lcgInventory.CustomHeaderButtons[i].Properties.Visible = false;
                gcCheck.Visible = false;
            }

            uncheck();
        }

        private void sbAll_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("ALL"))
                return;

            leComponentCd.EditValue = null;
            _componentCd = "ALL";
            setGridControl();
        }

        private void sbCpu_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("CPU"))
                return;

            leComponentCd.EditValue = null;
            _componentCd = "CPU";
            setGridControl();
        }

        private void sbMbd_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MBD"))
                return;

            leComponentCd.EditValue = null;
            _componentCd = "MBD";
            setGridControl();
        }

        private void sbMEM_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MEM"))
                return;

            leComponentCd.EditValue = null;
            _componentCd = "MEM";
            setGridControl();
        }

        private void sbStg_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("STG"))
                return;

            leComponentCd.EditValue = null;
            _componentCd = "STG";
            setGridControl();
        }

        private void sbVga_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("VGA"))
                return;

            leComponentCd.EditValue = null;
            _componentCd = "VGA";
            setGridControl();
        }

        private void sbMON_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MON"))
                return;

            leComponentCd.EditValue = null;
            _componentCd = "MON";
            setGridControl();
        }
        private void sbCAS_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("CAS"))
                return;

            _componentCd = "CAS";
            setGridControl();
        }

        private void sbADP_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("ADP"))
                return;

            _componentCd = "ADP";
            setGridControl();
        }

        private void sbPOW_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("POW"))
                return;

            _componentCd = "POW";
            setGridControl();
        }

        private void sbKEY_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("KEY"))
                return;

            _componentCd = "KEY";
            setGridControl();
        }

        private void sbMOU_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MOU"))
                return;

            _componentCd = "MOU";
            setGridControl();
        }

        private void sbFAN_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("FAN"))
                return;

            _componentCd = "FAN";
            setGridControl();
        }

        private void sbCAB_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("CAB"))
                return;

            _componentCd = "CAB";
            setGridControl();
        }

        private void sbBAT_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("BAT"))
                return;

            _componentCd = "BAT";
            setGridControl();
        }

        private void sbPER_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("PER"))
                return;

            _componentCd = "PER";
            setGridControl();
        }

        private void sbLIC_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("LIC"))
                return;

            _componentCd = "LIC";
            setGridControl();
        }

        private void sbAIR_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("AIR"))
                return;

            _componentCd = "AIR";
            setGridControl();
        }

        private void sbPKG_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("PKG"))
                return;

            _componentCd = "PKG";
            setGridControl();
        }

        private void leComponentCd_EditValueChanged(object sender, EventArgs e)
        {
            _componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
            if(!string.IsNullOrEmpty(_componentCd))
                setGridControl();
        }



        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvInventory.FocusedRowHandle;
            int topRowIndex = gvInventory.TopRowIndex;
            gvInventory.FocusedRowHandle = -1;
            gvInventory.FocusedRowHandle = rowhandle;

            if (e.Button.Properties.Tag.Equals(2))
            {
                DataRow[] rows = ProjectInfo._dtConsignedInfo.Select("CHECK = True");
                if (rows.Length < 1)
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                List<long> listProxyPartId = new List<long>();
                List<int> checkNo = new List<int>();
                foreach (DataRow row in rows)
                {
                    long proxyPartId = ConvertUtil.ToInt64(row["PROXY_PART_ID"]);
                    int consignedType = ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]);

                    if (proxyPartId > 0 && consignedType == 1)
                        listProxyPartId.Add(proxyPartId);

                    checkNo.Add(ConvertUtil.ToInt32(row["NO1"]));
                }

                if(listProxyPartId.Count < 1)
                {
                    Dangol.Message("자사재고로 변경할 후보 부품이 없습니다.");
                    return;
                }

                if(Dangol.MessageYN("선택하신 부품 중 생산대행을 자사재고로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();

                    if (DBConnect.consignedComponentContract(listProxyPartId, 2, ref jResult))
                    {
                        getReleasePart();

                        if (rows.Length > 0)
                        {
                            rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                            foreach (DataRow row in rows)
                            {
                                row.BeginEdit();
                                row["CHECK"] = true;
                                row.EndEdit();
                            }
                        }

                        gvInventory.FocusedRowHandle = rowhandle;
                        gvInventory.TopRowIndex = topRowIndex;
                    }
                    else
                    {
                        MessageBox.Show(jResult["MSG"].ToString());
                        return;
                    }
                }


            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                DataRow[] rows = ProjectInfo._dtConsignedInfo.Select("CHECK = True");
                if (rows.Length < 1)
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                List<long> listProxyPartId = new List<long>();
                List<int> checkNo = new List<int>();
                foreach (DataRow row in rows)
                {
                    long proxyPartId = ConvertUtil.ToInt64(row["PROXY_PART_ID"]);
                    int consignedType = ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]);

                    if (proxyPartId > 0 && consignedType == 2)
                        listProxyPartId.Add(proxyPartId);

                    checkNo.Add(ConvertUtil.ToInt32(row["NO1"]));
                }

                if (listProxyPartId.Count < 1)
                {
                    Dangol.Message("자사재고로 변경할 후보 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 부품 중 자사재고을 생산대행으로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();

                    if (DBConnect.consignedComponentContract(listProxyPartId, 1, ref jResult))
                    {
                        getReleasePart();

                        if (rows.Length > 0)
                        {
                            rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                            foreach (DataRow row in rows)
                            {
                                row.BeginEdit();
                                row["CHECK"] = true;
                                row.EndEdit();
                            }
                        }

                        gvInventory.FocusedRowHandle = rowhandle;
                        gvInventory.TopRowIndex = topRowIndex;
                    }
                    else
                    {
                        MessageBox.Show(jResult["MSG"].ToString());
                        return;
                    }
                }
            }     
            else if (e.Button.Properties.Tag.Equals(4))
            {
                DataRow[] rows = ProjectInfo._dtConsignedInfo.Select("CHECK = True");
                if (rows.Length < 1)
                {
                    Dangol.Warining("체크된 부품이 없습니다.");
                    return;
                }

                long proxyPartId = -1;
                string warehousing = "";
                int consignedType = -1;
                bool isUpdated = false;
                List<int> checkNo = new List<int>();
                JObject jResult = new JObject();

                foreach (DataRow row in rows)
                {
                    checkNo.Add(ConvertUtil.ToInt32(row["NO1"]));

                    proxyPartId = ConvertUtil.ToInt64(row["PROXY_PART_ID"]);

                    if (proxyPartId > 0)
                    {
                        warehousing = ConvertUtil.ToString(row["WAREHOUSING"]);
                        consignedType = ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]);

                        if (string.IsNullOrEmpty(warehousing))
                            warehousing = "-1";

                        if (DBConnect.saveConsignedWarehousing(proxyPartId, warehousing, ref jResult))
                        {
                            isUpdated = true;
                        }
                        else
                        {
                            //MessageBox.Show(jResult["MSG"].ToString());
                            //return;
                        }
                    }
                    //else
                    //{
                    //    Dangol.Message("접수되지 않은 부품입니다.");
                    //    return;
                    //}
                }

                if(isUpdated)
                {
                    getReleasePart();

                    if (rows.Length > 0)
                    {
                        rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                        foreach (DataRow row in rows)
                        {
                            row.BeginEdit();
                            row["CHECK"] = true;
                            row.EndEdit();
                        }
                    }

                    gvInventory.FocusedRowHandle = rowhandle;
                    gvInventory.TopRowIndex = topRowIndex;
                }
            }
            else if (e.Button.Properties.Tag.Equals(5))
            {
                if (ConvertUtil.ToInt64(_companyId) == 2 || _cpuAssignYn == 1)
                {
                    DataRow[] CPUrows = ProjectInfo._dtConsignedInfo.Select("COMPONENT_CD = 'CPU'");
                    if(CPUrows.Length < 1)
                    {
                        Dangol.Warining("CPU정보가 없습니다.");
                        return;
                    }
                    else
                    {
                        if(ConvertUtil.ToBoolean(CPUrows[0]["DIFF"]))
                        {
                            Dangol.Warining("해당 제품은 CPU 정보가 달라 부품을 할당할 수 없습니다.\n관리자 또는 담당자에게 문의하세요.");
                            return;
                        }
                    }
                }
                
                DataRow[] rows = ProjectInfo._dtConsignedInfo.Select("CHECK = True");
                if (rows.Length < 1)
                {
                    Dangol.Warining("체크된 부품이 없습니다.");
                    return;
                }

                JObject jResult = new JObject();
                if(DBConnect.asignConsignedState(_proxyId, ref jResult))
                {
                    _proxyState = ConvertUtil.ToInt32(jResult["PROXY_STATE"]);

                    if(_proxyState == 3)
                    {
                        Dangol.Warining("취소된 접수입니다.");
                        return;
                    }
                    else if(_proxyState != 1)
                    {
                        Dangol.Warining("'처리중' 접수만 재고할당이 가능합니다.");
                        return;
                    }
                }
                else
                {
                    Dangol.Warining("접수정보를 확인할 수 없습니다.");
                    return;
                }



                long proxyPartId = -1;
                int consignedType = -1;
                bool isUpdated = false;
                List<int> checkNo = new List<int>();
                
                string componentCd;

                bool memCheck = true;
                bool stgCheck = true;
                JObject jobj = new JObject();
                string url;

                foreach (DataRow row in rows)
                {
                    checkNo.Add(ConvertUtil.ToInt32(row["NO1"]));
                    componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                    proxyPartId = ConvertUtil.ToInt64(row["PROXY_PART_ID"]);

                    if (proxyPartId > 0)
                    {
                        if (componentCd.Equals("STG") || componentCd.Equals("MEM"))
                        {
                            bool diff = ConvertUtil.ToBoolean(row["DIFF"]);

                            if (diff)
                            {
                                if (componentCd.Equals("STG"))
                                    stgCheck = false;
                                else
                                    memCheck = false;

                                continue;
                            }
                        }

                        consignedType = ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]);

                        jobj.RemoveAll();
                        jobj.Add("PROXY_ID", ConvertUtil.ToInt64(_proxyId));
                        jobj.Add("PROXY_PART_ID", ConvertUtil.ToInt64(proxyPartId));
                        jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(_companyId));
                        //jobj.Add("COMPONENT_ID", ConvertUtil.ToInt64(row["COMPONENT_ID"]));
                        jobj.Add("CONSIGNED_TYPE", ConvertUtil.ToInt32(consignedType));
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

                        if (DBConsigned.assignConsignedReleaseInventory(jobj, url, ref jResult))
                        {
                            isUpdated = true;
                        }
                        else
                        {
                            //MessageBox.Show(jResult["MSG"].ToString());
                            //return;
                        }

                        //if (DBConnect.asignConsignedReleaseInventory(_proxyId, proxyPartId, _companyId, consignedType, componentCd, ref jResult))
                        //{
                        //    isUpdated = true;
                        //}
                        //else
                        //{
                        //    //MessageBox.Show(jResult["MSG"].ToString());
                        //    //return;
                        //}
                    }
                    //else
                    //{
                    //    Dangol.Message("접수되지 않은 부품입니다.");
                    //    return;
                    //}
                }

                if (isUpdated)
                {
                    getReleasePart();

                    if (rows.Length > 0)
                    {
                        rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                        foreach (DataRow row in rows)
                        {
                            row.BeginEdit();
                            row["CHECK"] = true;
                            row.EndEdit();
                        }
                    }

                    gvInventory.FocusedRowHandle = rowhandle;
                    gvInventory.TopRowIndex = topRowIndex;
                }


                if(!stgCheck && !memCheck)
                    Dangol.Warining("메모리와 저장장치가 할당되지 않았습니다. 부품을 확인해 주세요. ");
                else if(!stgCheck)
                    Dangol.Warining("저장장치가 할당되지 않았습니다. 부품을 확인해 주세요. ");
                else if (!memCheck)
                    Dangol.Warining("메모리가 할당되지 않았습니다. 부품을 확인해 주세요. ");
            }
            else if (e.Button.Properties.Tag.Equals(6))
            {
                if (ConvertUtil.ToInt64(_companyId) == 2 || _cpuAssignYn == 1)
                {
                    DataRow[] CPUrows = ProjectInfo._dtConsignedInfo.Select("COMPONENT_CD = 'CPU'");
                    if (CPUrows.Length < 1)
                    {
                        Dangol.Warining("CPU정보가 없습니다.");
                        return;
                    }
                    else
                    {
                        if (ConvertUtil.ToBoolean(CPUrows[0]["DIFF"]))
                        {
                            Dangol.Warining("해당 제품은 CPU 정보가 달라 부품을 할당할 수 없습니다.\n관리자 또는 담당자에게 문의하세요.");
                            return;
                        }
                    }
                }


                DataRow[] rows = ProjectInfo._dtConsignedInfo.Select("CHECK = True");
                if (rows.Length < 1)
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }
                if (rows.Length > 1)
                {
                    Dangol.Message("하나의 부품만 선택해 주세요.");
                    return;
                }

                long proxyPartId = -1;
                string componentCd;
                int consignedType = -1;
                List<int> checkNo = new List<int>();

                foreach (DataRow row in rows)
                {
                    checkNo.Add(ConvertUtil.ToInt32(row["NO1"]));
                    componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                    proxyPartId = ConvertUtil.ToInt64(row["PROXY_PART_ID"]);

                    if (proxyPartId < 0)
                    {
                        Dangol.Message("접수되지 않은 부품입니다.");
                        return;
                    }

                    consignedType = ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]);


                    using (dlgConsignedComponentList dlgList = new dlgConsignedComponentList(
                     proxyPartId,
                     ConvertUtil.ToInt64(_companyId),
                    componentCd,
                    consignedType))
                    {
                        if(dlgList.ShowDialog() == DialogResult.OK)
                        {
                            getReleasePart();

                            if (rows.Length > 0)
                            {
                                rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                                foreach (DataRow dr in rows)
                                {
                                    dr.BeginEdit();
                                    dr["CHECK"] = true;
                                    dr.EndEdit();
                                }
                            }

                            gvInventory.FocusedRowHandle = rowhandle;
                            gvInventory.TopRowIndex = topRowIndex;
                        }
                    }


                    break;


                }

                //if (isUpdated)
                //{
                //    getReleasePart();

                //    if (rows.Length > 0)
                //    {
                //        rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                //        foreach (DataRow row in rows)
                //        {
                //            row.BeginEdit();
                //            row["CHECK"] = true;
                //            row.EndEdit();
                //        }
                //    }

                //    gvInventory.FocusedRowHandle = rowhandle;
                //    gvInventory.TopRowIndex = topRowIndex;
                //}

            }

            else if (e.Button.Properties.Tag.Equals(99))
            {

                long proxyPartId = ConvertUtil.ToInt64(_currentRow["PROXY_PART_ID"]);

                if (proxyPartId > 0)
                {
                    DataRow[] rows = ProjectInfo._dtConsignedInfo.Select("CHECK = True");
                    List<int> checkNo = new List<int>();
                    JObject jResult = new JObject();
                    string componentCd;

                    foreach (DataRow row in rows)
                        checkNo.Add(ConvertUtil.ToInt32(row["NO1"]));

                    string warehousing = ConvertUtil.ToString(_currentRow["WAREHOUSING"]);
                    int consignedType = ConvertUtil.ToInt32(_currentRow["CONSIGNED_TYPE"]);
                    componentCd = ConvertUtil.ToString(_currentRow["COMPONENT_CD"]);

                    if (string.IsNullOrEmpty(warehousing))
                        warehousing = "-1";

                    if (DBConnect.saveConsignedWarehousing(proxyPartId, warehousing, ref jResult))
                    {

                        if (DBConnect.asignConsignedReleaseInventory(_proxyId, proxyPartId, _companyId, consignedType, componentCd, ref jResult))
                        {
                            getReleasePart();

                            if (rows.Length > 0)
                            {
                                rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                                foreach (DataRow row in rows)
                                {
                                    row.BeginEdit();
                                    row["CHECK"] = true;
                                    row.EndEdit();
                                }
                            }

                            gvInventory.FocusedRowHandle = rowhandle;
                            gvInventory.TopRowIndex = topRowIndex;
                        }
                    }
                    else
                    {
                        MessageBox.Show(jResult["MSG"].ToString());
                        return;
                    }
                }
            }

        }


        private void sbClear_Click(object sender, EventArgs e)
        {
            int rowhandle = gvInventory.FocusedRowHandle;
            int topRowIndex = gvInventory.TopRowIndex;
            gvInventory.FocusedRowHandle = -1;
            gvInventory.FocusedRowHandle = rowhandle;
            long proxyPartId = ConvertUtil.ToInt64(_currentRow["PROXY_PART_ID"]);

            if (proxyPartId > 0)
            {
                if (_inventoryId < 0)
                {
                    Dangol.Message("할당되지 않은 부품입니다.");
                    return;
                }
                else
                {
                    if (Dangol.MessageYN("선택한 접수 정보의 할당 부품을 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        DataRow[] rows = ProjectInfo._dtConsignedInfo.Select("CHECK = True");
                        List<int> checkNo = new List<int>();
                        JObject jResult = new JObject();

                        foreach (DataRow row in rows)
                            checkNo.Add(ConvertUtil.ToInt32(row["NO1"]));

                        if (DBConnect.deleteReleaseComponent(proxyPartId, ref jResult))
                        {
                            getReleasePart();

                            if (rows.Length > 0)
                            {
                                rows = ProjectInfo._dtConsignedInfo.Select($"NO1 IN ({string.Join(",", checkNo)})");

                                foreach (DataRow row in rows)
                                {
                                    row.BeginEdit();
                                    row["CHECK"] = true;
                                    row.EndEdit();
                                }
                            }

                            gvInventory.FocusedRowHandle = rowhandle;
                            gvInventory.TopRowIndex = topRowIndex;
                        }
                        else
                        {
                            MessageBox.Show(jResult["MSG"].ToString());
                            return;
                        }
                    }
                }
            }
            else
            {
                Dangol.Message("접수되지 않은 부품입니다.");
                return;
            }
        }

        private void risePrice_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit View = sender as SpinEdit;
            long proxyPartId = ConvertUtil.ToInt64(_currentRow["PROXY_PART_ID"]);
            JObject jResult = new JObject();

            if(ConvertUtil.ToInt64(View.Value) < 0)
            {
                Dangol.Message("양수만 입력 가능합니다.");
                View.Value = ConvertUtil.ToInt64(_currentRow["PRICE"]);
                return;
            }

            if (DBConnect.updatePrice(proxyPartId, View.Value, ref jResult))
            {

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

                foreach (DataRow row in ProjectInfo._dtConsignedInfo.Rows)
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
                uncheck();
            }
        }

        private void uncheck()
        {
            int rowhandle = gvInventory.FocusedRowHandle;
            int topRowIndex = gvInventory.TopRowIndex;
            gvInventory.FocusedRowHandle = -1;
            gvInventory.FocusedRowHandle = rowhandle;

            foreach (DataRow row in ProjectInfo._dtConsignedInfo.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
        }

        private void sbGetReleasePart_Click(object sender, EventArgs e)
        {
            memCheck = false;
            stgCheck = false;

            getReleasePart();
        }
        private void getReleasePart()
        { 
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible) { }
            else SplashScreenManager.CloseForm();

            SplashScreenManager.ShowForm(typeof(WareHousingMaster.Helper.DlgWaitForm));
            SplashScreenManager.Default.SetWaitFormCaption($"생산대행");
            SplashScreenManager.Default.SetWaitFormDescription("상태 정보 가져오는 중...");

            JObject jResult = new JObject();
            bool isExistRelease = true;
            List<string> listComponentCd = null;

            if (DBConnect.getProxyInfo(_representativeNo, ref jResult))
            {
                _proxyState = ConvertUtil.ToInt32(jResult["PROXY_STATE"]);
                isExistRelease = ConvertUtil.ToBoolean(jResult["CNT"]);
                _proxyId = jResult["PROXY_ID"];
                _companyId = jResult["COMPANY_ID"];
                _cpuAssignYn = ConvertUtil.ToInt32(jResult["CPU_ASSIGN_YN"]);

                _partRelease = ConvertUtil.ToInt32(jResult["PART_YN"]);
                _preRelease = ConvertUtil.ToInt32(jResult["PRE_YN"]);

                teRequest.Text = ConvertUtil.ToString(jResult["REQUEST"]);
                teModelNmDetail.Text = ConvertUtil.ToString(jResult["MODEL_NM_DETAIL"]);

                if (_proxyState != 1)
                {
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("처리중인 생산대행만 사용할수 있습니다.");
                    return;
                }

                if (isExistRelease)
                {
                    JArray jArray = JArray.Parse(jResult["LIST_COMPONENT_CD"].ToString());
                    listComponentCd = new List<string>();

                    foreach (JObject obj in jArray.Children<JObject>())
                        listComponentCd.Add(ConvertUtil.ToString(obj["COMPONENT_CD"]));

                    if (!listComponentCd.Contains("CPU"))
                        listComponentCd.Add("CPU");
                }
            }
            else
            {
                _partRelease = -1;
                _preRelease = -1;
                SplashScreenManager.CloseForm();
                MessageBox.Show(jResult["MSG"].ToString());
                return;
            }

            SplashScreenManager.Default.SetWaitFormDescription("부품 정보 구성 중...");

            ProjectInfo._dtConsignedInfo.Clear();
            ProjectInfo._dicConsignedInfoDetail.Clear();

            if (!isExistRelease)
            {
                List<string> listFullColumn;
                DataTable dtComponentInfo;
                int id = 0;
                foreach (DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                    foreach(string col in ProjectInfo._listDefaultConsignedColum)
                        dataRow[col] = row[col];

                    List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[$"{row["COMPONENT_CD"]}"];
                    for (int i = 1; i < 4; i++)
                        dataRow[$"DATA{i}"] = row[listConsignedCol[i - 1]];

                    dataRow["DIFF"] = true;
                    dataRow["PRICE"] = 0;
                    dataRow["PROXY_PART_ID"] = -1;
                    dataRow["CONSIGNED_TYPE"] = -1;


                    ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                    id = ConvertUtil.ToInt32(row["ID"]);
                    dataRow["OID"] = id;

                    dtComponentInfo = ProjectInfo._dicDeviceInfoDetail[id];
                    DataTable dtCompare = new DataTable();
                    dtCompare = dtComponentInfo.Copy();
                    ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                }
            }
            else
            {
                JObject jData = new JObject();
                DataRow[] rows;
                DataRow[] detailRows;
                DataRow[] rowDeviceInfo = null;
                List<string> listFullColumn;
                DataTable dtComponentInfo = null;
                int id = 0;
                bool isCpuExist = false;

                foreach (string componentCd in ProjectInfo._componetCd)
                {
                    if (componentCd.Equals("STG"))
                        rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = '{componentCd}'", "STG_TYPE, CAPACITY");
                    else if (componentCd.Equals("MEM"))
                        rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = '{componentCd}'", "CAPACITY");
                    else if (componentCd.Equals("MON"))
                        rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = '{componentCd}'", "SIZE");
                    else if (componentCd.Equals("VGA"))
                        rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = '{componentCd}'", "CAPACITY");
                    else
                        rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = '{componentCd}'");

                    listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
                    //listShortColumn = ProjectInfo._dicDeviceColumn[componentCd];

                    //dataRow["ASSIGN_COMPONENT_ID"] = row["ASSIGN_COMPONENT_ID"];
                    if (listComponentCd.Contains(componentCd))
                    {
                        bool isSuccess = DBConnect.getConsignedComponentInfo(_proxyId, _companyId, componentCd, ref jResult);

                        if (!isSuccess && componentCd.Equals("CPU"))
                        {
                            isSuccess = DBConnect.getConsignedInventory(_proxyId, componentCd, ref jResult);

                            if (componentCd.Equals("CPU") && isSuccess)
                                isCpuExist = true;
                        }

                        if (isSuccess)
                        {
                            JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                            if (componentCd.Equals("MBD") && !isCpuExist)
                            {
                                id = setMbdTable(id, listFullColumn, jArray);
                            }
                            else if (componentCd.Equals("STG"))
                            {
                                id = setStgTable(id, listFullColumn, jArray);
                            }
                            else
                            {

                                if (rows.Length == 0 && jArray.Count > 0)
                                {
                                    int index = 1;

                                    foreach (JObject obj in jArray.Children<JObject>())
                                    {
                                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                                        id++;

                                        dataRow["NAME"] = $"{componentCd}_{index}";
                                        dataRow["COMPONENT_CD"] = componentCd;

                                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                                        for (int i = 1; i < 4; i++)
                                            dataRow[$"REP{i}"] = obj[listConsignedCol[i - 1]];

                                        foreach (string col in ProjectInfo._listKeyColumn)
                                            dataRow[col] = obj[col];

                                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                                        dataRow["PALLET"] = obj["PALLET"];
                                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                                        dataRow["PRICE"] = obj["PRICE"];
                                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                                        dataRow["CHECK"] = false;
                                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                                            dataRow["INVENTORY_YN"] = true;
                                        else
                                            dataRow["INVENTORY_YN"] = false;
                                        dataRow["PRODUCT_YN"] = false;
                                        dataRow["DIFF"] = true;

                                        //dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                                        dataRow["ID"] = id;
                                        dataRow["NO1"] = id;
                                        dataRow["NO"] = index;
                                        dataRow["OID"] = -1;

                                       

                                        DataTable dtCompare = new DataTable();
                                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                                        foreach (string col in listFullColumn)
                                        {
                                            DataRow drCompare = dtCompare.NewRow();
                                            drCompare["SPEC_NM"] = col;
                                            drCompare["DEVICE_INFO"] = "";
                                            drCompare["MATCHING_INFO"] = obj[col];
                                            dtCompare.Rows.Add(drCompare);
                                        }

                                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                                        index++;
                                    }
                                }
                                else if (rows.Length == jArray.Count)
                                {
                                    int index = 0;

                                    foreach (JObject obj in jArray.Children<JObject>())
                                    {
                                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();
                                        id++;
                                        foreach (string col in ProjectInfo._listDefaultConsignedColum)
                                            dataRow[col] = rows[index][col];

                                        //foreach (string col in listShortColumn)
                                        //    dataRow[col] = rows[index][col];                        

                                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                                        for (int i = 1; i < 4; i++)
                                        {
                                            dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];
                                            dataRow[$"REP{i}"] = ConvertUtil.ToString(obj[listConsignedCol[i - 1]]);
                                        }

                                        foreach (string col in ProjectInfo._listKeyColumn)
                                            dataRow[col] = obj[col];

                                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                                        dataRow["PALLET"] = obj["PALLET"];
                                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                                        dataRow["PRICE"] = obj["PRICE"];
                                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                                        dataRow["CHECK"] = false;
                                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                                            dataRow["INVENTORY_YN"] = true;
                                        else
                                            dataRow["INVENTORY_YN"] = false;
                                        dataRow["PRODUCT_YN"] = false;
                                        dataRow["DIFF"] = false;

                                        dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                                        rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                                        dataRow["OID"] = rows[index]["ID"];

                                        dataRow["ID"] = id;
                                        dataRow["NO1"] = id;
                                        dataRow["NO"] = index + 1;
                                        DataTable dtCompare = new DataTable();
                                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                                        foreach (string col in listFullColumn)
                                        {
                                            DataRow drCompare = dtCompare.NewRow();
                                            drCompare["SPEC_NM"] = col;
                                            drCompare["DEVICE_INFO"] = rowDeviceInfo[0][col];
                                            drCompare["MATCHING_INFO"] = obj[col];
                                            dtCompare.Rows.Add(drCompare);
                                        }

                                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                                        index++;
                                    }

                                }
                                else if (rows.Length < jArray.Count)
                                {
                                    int index = 0;

                                    foreach (JObject obj in jArray.Children<JObject>())
                                    {
                                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                                        id++;

                                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];

                                        if (index < rows.Length)
                                        {
                                            foreach (string col in ProjectInfo._listDefaultConsignedColum)
                                                dataRow[col] = rows[0][col];

                                            //foreach (string col in listShortColumn)
                                            //    dataRow[col] = rows[index][col];

                                            for (int i = 1; i < 4; i++)
                                                dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];

                                            dataRow["DIFF"] = false;
                                            dataRow["OID"] = rows[index]["ID"];
                                        }
                                        else
                                        {
                                            dataRow["NAME"] = $"{componentCd}_{index + 1}";
                                            dataRow["COMPONENT_CD"] = componentCd;
                                            dataRow["DIFF"] = true;
                                        }

                                        for (int i = 1; i < 4; i++)
                                            dataRow[$"REP{i}"] = obj[listConsignedCol[i - 1]];


                                        foreach (string col in ProjectInfo._listKeyColumn)
                                            dataRow[col] = obj[col];

                                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                                        dataRow["PALLET"] = obj["PALLET"];
                                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                                        dataRow["PRICE"] = obj["PRICE"];
                                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                                        dataRow["CHECK"] = false;
                                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                                            dataRow["INVENTORY_YN"] = true;
                                        else
                                            dataRow["INVENTORY_YN"] = false;
                                        dataRow["PRODUCT_YN"] = false;

                                        if (index < rows.Length)
                                        {
                                            dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                                            rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                                        }

                                        dataRow["ID"] = id;
                                        dataRow["NO1"] = id;
                                        dataRow["NO"] = index + 1;

                                        DataTable dtCompare = new DataTable();
                                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                                        foreach (string col in listFullColumn)
                                        {
                                            DataRow drCompare = dtCompare.NewRow();
                                            drCompare["SPEC_NM"] = col;
                                            if (index < rows.Length)
                                                drCompare["DEVICE_INFO"] = rowDeviceInfo[0][col];
                                            else
                                                drCompare["DEVICE_INFO"] = "";
                                            drCompare["MATCHING_INFO"] = obj[col];
                                            dtCompare.Rows.Add(drCompare);
                                        }

                                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                                        index++;
                                    }
                                }
                                else if (rows.Length > jArray.Count)
                                {
                                    int index = 0;

                                    foreach (JObject obj in jArray.Children<JObject>())
                                    {
                                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                                        id++;
                                        foreach (string col in ProjectInfo._listDefaultConsignedColum)
                                            dataRow[col] = rows[index][col];

                                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                                        for (int i = 1; i < 4; i++)
                                        {
                                            dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];
                                            dataRow[$"REP{i}"] = ConvertUtil.ToString(obj[listConsignedCol[i - 1]]);
                                        }


                                        foreach (string col in ProjectInfo._listKeyColumn)
                                            dataRow[col] = obj[col];

                                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                                        dataRow["PALLET"] = obj["PALLET"];
                                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                                        dataRow["PRICE"] = obj["PRICE"];
                                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                                        dataRow["CHECK"] = false;
                                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                                            dataRow["INVENTORY_YN"] = true;
                                        else
                                            dataRow["INVENTORY_YN"] = false;
                                        dataRow["PRODUCT_YN"] = false;
                                        dataRow["DIFF"] = false;

                                        dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                                        rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                                        dataRow["OID"] = rows[index]["ID"];

                                        dataRow["ID"] = id;
                                        dataRow["NO1"] = id;
                                        dataRow["NO"] = index + 1;
                                        DataTable dtCompare = new DataTable();
                                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                                        foreach (string col in listFullColumn)
                                        {
                                            DataRow drCompare = dtCompare.NewRow();
                                            drCompare["SPEC_NM"] = col;
                                            drCompare["DEVICE_INFO"] = dtComponentInfo.Rows[0][col];
                                            drCompare["MATCHING_INFO"] = obj[col];
                                            dtCompare.Rows.Add(drCompare);
                                        }

                                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                                        index++;
                                    }

                                    for (int k = index; k < rows.Length; k++)
                                    {
                                        DataRow row = rows[k];
                                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                                        id++;
                                        int devId;
                                        foreach (string col in ProjectInfo._listDefaultConsignedColum)
                                            dataRow[col] = row[col];

                                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                                        for (int i = 1; i < 4; i++)
                                            dataRow[$"DATA{i}"] = row[listConsignedCol[i - 1]];

                                        dataRow["ID"] = id;
                                        dataRow["NO1"] = id;
                                        dataRow["NO"] = k + 1;

                                        dataRow["DIFF"] = true;
                                        dataRow["PRICE"] = 0;
                                        dataRow["PROXY_PART_ID"] = -1;
                                        dataRow["CONSIGNED_TYPE"] = -1;

                                        devId = ConvertUtil.ToInt32(row["ID"]);

                                        dtComponentInfo = ProjectInfo._dicDeviceInfoDetail[devId];
                                        DataTable dtCompare = new DataTable();
                                        dtCompare = dtComponentInfo.Copy();
                                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int index = 1;
                            foreach (DataRow row in rows)
                            {
                                DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                                id++;
                                int devId;
                                foreach (string col in ProjectInfo._listDefaultConsignedColum)
                                    dataRow[col] = row[col];

                                List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                                for (int i = 1; i < 4; i++)
                                    dataRow[$"DATA{i}"] = row[listConsignedCol[i - 1]];

                                dataRow["OID"] = row["ID"];
                                dataRow["ID"] = id;
                                dataRow["NO1"] = id;
                                dataRow["NO"] = index++;
                                dataRow["DIFF"] = true;
                                dataRow["PRICE"] = 0;
                                dataRow["PROXY_PART_ID"] = -1;
                                dataRow["CONSIGNED_TYPE"] = -1;

                                devId = ConvertUtil.ToInt32(row["ID"]);

                                dtComponentInfo = ProjectInfo._dicDeviceInfoDetail[devId];
                                DataTable dtCompare = new DataTable();
                                dtCompare = dtComponentInfo.Copy();
                                ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                                ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                            }
                        }
                    }
                    else // 접수되지 않은 품목
                    {
                        int index = 1;
                        foreach (DataRow row in rows)
                        {
                            DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                            id++;
                            int devId;
                            foreach (string col in ProjectInfo._listDefaultConsignedColum)
                                dataRow[col] = row[col];

                            List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                            for (int i = 1; i < 4; i++)
                                dataRow[$"DATA{i}"] = row[listConsignedCol[i - 1]];

                            dataRow["OID"] = row["ID"];
                            dataRow["ID"] = id;
                            dataRow["NO1"] = id;
                            dataRow["NO"] = index++;
                            dataRow["DIFF"] = true;
                            dataRow["PRICE"] = 0;
                            dataRow["PROXY_PART_ID"] = -1;
                            dataRow["CONSIGNED_TYPE"] = -1;

                            devId = ConvertUtil.ToInt32(row["ID"]);

                            dtComponentInfo = ProjectInfo._dicDeviceInfoDetail[devId];
                            DataTable dtCompare = new DataTable();
                            dtCompare = dtComponentInfo.Copy();
                            ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                            ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                        }
                    }
                }

                foreach (DataRow row in ProjectInfo._dtConsignedInfo.Rows)
                {
                    bool cmp = false;
                    bool diff = ConvertUtil.ToBoolean(row["DIFF"]);
                    string componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                    if(!diff)
                        cmp = checkCompare(componentCd, row);

                    if (!cmp)
                        row["DIFF"] = !cmp;
                }


                dtComponentInfo = ProjectInfo._dicDeviceInfo["MBD"];
                DataRow dr = dtComponentInfo.Rows[0];

                if (DBConnect.getRecentReturnCheck(dr["SERIAL_NO"], dr["SYSTEM_SN"], dr["MAC_ADDRESS"], ref jResult))
                {
                  
                    bool isReuse = ConvertUtil.ToBoolean(jResult["REUSE"]);

                    if (isReuse)
                        lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    else
                        lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    lcReuse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                //f(ProjectInfo._dicPartCheckRelease == null)
                //    getCheckInfoInit();

                //if (ProjectInfo._dicProductList == null)
                //{
                //    rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");

                //    if (rows.Length > 0)
                //    {
                //        long inventoryId = ConvertUtil.ToInt64(rows[0]["INVENTORY_ID"]);
                //        string barcode = ConvertUtil.ToString(rows[0]["BARCODE"]);
                //        if (inventoryId > 0)
                //            Util.checkProductState(inventoryId, barcode);
                //        else
                //            ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
                //    }
                //    else
                //        ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
                //}

                //using (dlgCompare compreComponent = new dlgCompare())
                //{
                //    if (compreComponent.ShowDialog(this) == DialogResult.OK)
                //    {

                //    }
                //}


               

            }

            SplashScreenManager.CloseForm();
        }

        private void putConsignedCheckInfo()
        {
            JObject jResult = new JObject();
            string contents = "";

            DataRow[] rows;
            foreach (string componentCd in ProjectInfo._checkComponetCd)
            {
                rows = ProjectInfo._dtConsignedInfo.Select($"COMPONENT_CD = '{componentCd}'");

                foreach (DataRow row in rows)
                    contents += $"{componentCd}:{row["DATA1"]}:{row["DATA2"]}:{row["DATA3"]};";
            }

            DBConnect.putConsignedCheckInfo(_proxyId, contents, ref jResult);
        }


        private int setStgTable(int id, List<string> listFullColumn, JArray jArrays)
        {
            DataRow[] rows;
            DataRow[] rowDeviceInfo = null;
            DataTable dtComponentInfo = null;
            string componentCd = "STG";
            int componentCdIndex = 1;
            JArray jarraySSD = new JArray();
            JArray jarrayHDD = new JArray();
            Dictionary<string, JArray> dicStg = new Dictionary<string, JArray>();
            Dictionary<string, string> dicSTGIndex = new Dictionary<string, string> { { "SSD", "Fixed, SSD" }, { "HDD", "Fixed" } };


            foreach (JObject obj in jArrays.Children<JObject>())
            {
                string stgType = ConvertUtil.ToString(obj["STG_TYPE"]);
                if (stgType.Contains("SSD"))
                    jarraySSD.Add(obj);
                else
                    jarrayHDD.Add(obj);
            }

            dicStg.Add("SSD", jarraySSD);
            dicStg.Add("HDD", jarrayHDD);

            foreach (KeyValuePair<string, string> data in dicSTGIndex)
            {
                string stgCd = data.Key;
                string stgType = dicSTGIndex[stgCd];

                rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = '{componentCd}' AND STG_TYPE = '{stgType}'", "CAPACITY");
                JArray jArray = dicStg[stgCd];

                if (rows.Length == 0 && jArray.Count > 0)
                {
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                        id++;

                        dataRow["NAME"] = $"{componentCd}_{componentCdIndex}";
                        dataRow["COMPONENT_CD"] = componentCd;

                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                        for (int i = 1; i < 4; i++)
                            dataRow[$"REP{i}"] = obj[listConsignedCol[i - 1]];

                        foreach (string col in ProjectInfo._listKeyColumn)
                            dataRow[col] = obj[col];

                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                        dataRow["PALLET"] = obj["PALLET"];
                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                        dataRow["PRICE"] = obj["PRICE"];
                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                        dataRow["CHECK"] = false;
                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                            dataRow["INVENTORY_YN"] = true;
                        else
                            dataRow["INVENTORY_YN"] = false;
                        dataRow["PRODUCT_YN"] = false;
                        dataRow["DIFF"] = true;

                        //dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                        dataRow["ID"] = id;
                        dataRow["NO1"] = id;
                        dataRow["NO"] = componentCdIndex;
                        dataRow["OID"] = -1;

                        

                        DataTable dtCompare = new DataTable();
                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                        foreach (string col in listFullColumn)
                        {
                            DataRow drCompare = dtCompare.NewRow();
                            drCompare["SPEC_NM"] = col;
                            drCompare["DEVICE_INFO"] = "";
                            drCompare["MATCHING_INFO"] = obj[col];
                            dtCompare.Rows.Add(drCompare);
                        }

                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                        componentCdIndex++;
                    }
                }
                else if (rows.Length == jArray.Count)
                {
                    int index = 0;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();
                        id++;
                        foreach (string col in ProjectInfo._listDefaultConsignedColum)
                            dataRow[col] = rows[index][col];

                        //foreach (string col in listShortColumn)
                        //    dataRow[col] = rows[index][col];                        

                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                        for (int i = 1; i < 4; i++)
                        {
                            dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];
                            dataRow[$"REP{i}"] = ConvertUtil.ToString(obj[listConsignedCol[i - 1]]);
                        }

                        foreach (string col in ProjectInfo._listKeyColumn)
                            dataRow[col] = obj[col];

                        dataRow["NAME"] = $"{componentCd}_{componentCdIndex}";
                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                        dataRow["PALLET"] = obj["PALLET"];
                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                        dataRow["PRICE"] = obj["PRICE"];
                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                        dataRow["CHECK"] = false;
                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                            dataRow["INVENTORY_YN"] = true;
                        else
                            dataRow["INVENTORY_YN"] = false;
                        dataRow["PRODUCT_YN"] = false;
                        dataRow["DIFF"] = false;

                        dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                        rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                        dataRow["OID"] = rows[index]["ID"];

                        dataRow["ID"] = id;
                        dataRow["NO1"] = id;
                        dataRow["NO"] = componentCdIndex;
                        DataTable dtCompare = new DataTable();
                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                        foreach (string col in listFullColumn)
                        {
                            DataRow drCompare = dtCompare.NewRow();
                            drCompare["SPEC_NM"] = col;
                            drCompare["DEVICE_INFO"] = rowDeviceInfo[0][col];
                            drCompare["MATCHING_INFO"] = obj[col];
                            dtCompare.Rows.Add(drCompare);
                        }

                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                        index++;
                        componentCdIndex++;
                    }

                }
                else if (rows.Length < jArray.Count)
                {
                    int index = 0;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                        id++;

                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];

                        if (index < rows.Length)
                        {
                            foreach (string col in ProjectInfo._listDefaultConsignedColum)
                                dataRow[col] = rows[0][col];

                            //foreach (string col in listShortColumn)
                            //    dataRow[col] = rows[index][col];

                            for (int i = 1; i < 4; i++)
                                dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];

                            dataRow["DIFF"] = false;
                            dataRow["OID"] = rows[index]["ID"];
                        }
                        else
                        {
                            dataRow["COMPONENT_CD"] = componentCd;
                            dataRow["DIFF"] = true;
                        }

                        for (int i = 1; i < 4; i++)
                            dataRow[$"REP{i}"] = obj[listConsignedCol[i - 1]];


                        foreach (string col in ProjectInfo._listKeyColumn)
                            dataRow[col] = obj[col];

                        dataRow["NAME"] = $"{componentCd}_{componentCdIndex}";
                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                        dataRow["PALLET"] = obj["PALLET"];
                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                        dataRow["PRICE"] = obj["PRICE"];
                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                        dataRow["CHECK"] = false;
                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                            dataRow["INVENTORY_YN"] = true;
                        else
                            dataRow["INVENTORY_YN"] = false;
                        dataRow["PRODUCT_YN"] = false;

                        if (index < rows.Length)
                        {
                            dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                            rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                        }

                        dataRow["ID"] = id;
                        dataRow["NO1"] = id;
                        dataRow["NO"] = componentCdIndex;

                        DataTable dtCompare = new DataTable();
                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                        foreach (string col in listFullColumn)
                        {
                            DataRow drCompare = dtCompare.NewRow();
                            drCompare["SPEC_NM"] = col;
                            if (index < rows.Length)
                                drCompare["DEVICE_INFO"] = rowDeviceInfo[0][col];
                            else
                                drCompare["DEVICE_INFO"] = "";
                            drCompare["MATCHING_INFO"] = obj[col];
                            dtCompare.Rows.Add(drCompare);
                        }

                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                        index++;
                        componentCdIndex++;
                    }
                }
                else if (rows.Length > jArray.Count)
                {
                    int index = 0;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                        id++;
                        foreach (string col in ProjectInfo._listDefaultConsignedColum)
                            dataRow[col] = rows[index][col];

                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                        for (int i = 1; i < 4; i++)
                        {
                            dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];
                            dataRow[$"REP{i}"] = ConvertUtil.ToString(obj[listConsignedCol[i - 1]]);
                        }


                        foreach (string col in ProjectInfo._listKeyColumn)
                            dataRow[col] = obj[col];

                        dataRow["NAME"] = $"{componentCd}_{componentCdIndex}";
                        dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                        dataRow["PALLET"] = obj["PALLET"];
                        dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                        dataRow["PRICE"] = obj["PRICE"];
                        dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                        dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                        dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                        dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                        dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                        dataRow["CHECK"] = false;
                        if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                            dataRow["INVENTORY_YN"] = true;
                        else
                            dataRow["INVENTORY_YN"] = false;
                        dataRow["PRODUCT_YN"] = false;
                        dataRow["DIFF"] = false;

                        dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                        rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                        dataRow["OID"] = rows[index]["ID"];

                        dataRow["ID"] = id;
                        dataRow["NO1"] = id;
                        dataRow["NO"] = componentCdIndex;
                        DataTable dtCompare = new DataTable();
                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                        foreach (string col in listFullColumn)
                        {
                            DataRow drCompare = dtCompare.NewRow();
                            drCompare["SPEC_NM"] = col;
                            drCompare["DEVICE_INFO"] = dtComponentInfo.Rows[0][col];
                            drCompare["MATCHING_INFO"] = obj[col];
                            dtCompare.Rows.Add(drCompare);
                        }

                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                        index++;
                        componentCdIndex++;
                    }

                    for (int k = index; k < rows.Length; k++)
                    {
                        DataRow row = rows[k];
                        DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                        id++;
                        int devId;
                        foreach (string col in ProjectInfo._listDefaultConsignedColum)
                            dataRow[col] = row[col];

                        List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                        for (int i = 1; i < 4; i++)
                            dataRow[$"DATA{i}"] = row[listConsignedCol[i - 1]];

                        dataRow["NAME"] = $"{componentCd}_{componentCdIndex}";
                        dataRow["ID"] = id;
                        dataRow["NO1"] = id;
                        dataRow["NO"] = componentCdIndex;

                        dataRow["DIFF"] = true;
                        dataRow["PRICE"] = 0;
                        dataRow["PROXY_PART_ID"] = -1;
                        dataRow["CONSIGNED_TYPE"] = -1;

                        devId = ConvertUtil.ToInt32(row["ID"]);

                        dtComponentInfo = ProjectInfo._dicDeviceInfoDetail[devId];
                        DataTable dtCompare = new DataTable();
                        dtCompare = dtComponentInfo.Copy();
                        ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                        ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                        componentCdIndex++;
                    }
                }
            }

            return id;
        }

        private int setMbdTable(int id, List<string> listFullColumn, JArray jArray)
        {
            DataRow[] rows;
            DataRow[] rowDeviceInfo = null;
            DataTable dtComponentInfo = null;
            string componentCd = "MBD";
            string cpuNm = "";


            rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = '{componentCd}'");

            if (rows.Length == 0 && jArray.Count > 0)
            {
                int index = 1;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                    id++;

                    dataRow["NAME"] = $"{componentCd}_{index}";
                    dataRow["COMPONENT_CD"] = componentCd;

                    List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                    for (int i = 1; i < 4; i++)
                        dataRow[$"REP{i}"] = obj[listConsignedCol[i - 1]];

                    foreach (string col in ProjectInfo._listKeyColumn)
                        dataRow[col] = obj[col];

                    dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                    dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                    dataRow["PALLET"] = obj["PALLET"];
                    dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                    dataRow["PRICE"] = obj["PRICE"];
                    dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                    dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                    dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                    dataRow["CHECK"] = false;
                    if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                        dataRow["INVENTORY_YN"] = true;
                    else
                        dataRow["INVENTORY_YN"] = false;
                    dataRow["PRODUCT_YN"] = false;
                    dataRow["DIFF"] = true;

                    //dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    dataRow["ID"] = id;
                    dataRow["NO1"] = id;
                    dataRow["NO"] = index;
                    dataRow["OID"] = -1;

                    

                    DataTable dtCompare = new DataTable();
                    dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                    foreach (string col in listFullColumn)
                    {
                        DataRow drCompare = dtCompare.NewRow();
                        drCompare["SPEC_NM"] = col;
                        drCompare["DEVICE_INFO"] = "";
                        drCompare["MATCHING_INFO"] = obj[col];
                        dtCompare.Rows.Add(drCompare);
                    }

                    ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                    ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                    index++;

                    cpuNm = ConvertUtil.ToString(obj["PRODUCT_NAME"]);
                }
            }
            else if (rows.Length == jArray.Count)
            {
                int index = 0;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();
                    id++;
                    foreach (string col in ProjectInfo._listDefaultConsignedColum)
                        dataRow[col] = rows[index][col];

                    //foreach (string col in listShortColumn)
                    //    dataRow[col] = rows[index][col];

                    List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                    for (int i = 1; i < 4; i++)
                    {
                        dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];
                        dataRow[$"REP{i}"] = ConvertUtil.ToString(obj[listConsignedCol[i - 1]]);
                    }

                    foreach (string col in ProjectInfo._listKeyColumn)
                        dataRow[col] = obj[col];

                    dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                    dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                    dataRow["PALLET"] = obj["PALLET"];
                    dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                    dataRow["PRICE"] = obj["PRICE"];
                    dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                    dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                    dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                    dataRow["CHECK"] = false;
                    if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                        dataRow["INVENTORY_YN"] = true;
                    else
                        dataRow["INVENTORY_YN"] = false;
                    dataRow["PRODUCT_YN"] = false;
                    dataRow["DIFF"] = false;

                    dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                    dataRow["OID"] = rows[index]["ID"];

                    dataRow["ID"] = id;
                    dataRow["NO1"] = id;
                    dataRow["NO"] = index + 1;
                    DataTable dtCompare = new DataTable();
                    dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                    foreach (string col in listFullColumn)
                    {
                        DataRow drCompare = dtCompare.NewRow();
                        drCompare["SPEC_NM"] = col;
                        drCompare["DEVICE_INFO"] = rowDeviceInfo[0][col];
                        drCompare["MATCHING_INFO"] = obj[col];
                        dtCompare.Rows.Add(drCompare);
                    }

                    ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                    ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                    index++;

                    cpuNm = ConvertUtil.ToString(obj["PRODUCT_NAME"]);
                }

            }
            else if (rows.Length < jArray.Count)
            {
                int index = 0;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                    id++;

                    List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];

                    if (index < rows.Length)
                    {
                        foreach (string col in ProjectInfo._listDefaultConsignedColum)
                            dataRow[col] = rows[0][col];

                        //foreach (string col in listShortColumn)
                        //    dataRow[col] = rows[index][col];

                        for (int i = 1; i < 4; i++)
                            dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];

                        dataRow["DIFF"] = false;
                        dataRow["OID"] = rows[index]["ID"];
                    }
                    else
                    {
                        dataRow["NAME"] = $"{componentCd}_{index + 1}";
                        dataRow["COMPONENT_CD"] = componentCd;
                        dataRow["DIFF"] = true;
                    }

                    for (int i = 1; i < 4; i++)
                        dataRow[$"REP{i}"] = obj[listConsignedCol[i - 1]];


                    foreach (string col in ProjectInfo._listKeyColumn)
                        dataRow[col] = obj[col];

                    dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                    dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                    dataRow["PALLET"] = obj["PALLET"];
                    dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                    dataRow["PRICE"] = obj["PRICE"];
                    dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                    dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                    dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                    dataRow["CHECK"] = false;
                    if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                        dataRow["INVENTORY_YN"] = true;
                    else
                        dataRow["INVENTORY_YN"] = false;
                    dataRow["PRODUCT_YN"] = false;

                    if (index < rows.Length)
                    {
                        dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                        rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                    }

                    dataRow["ID"] = id;
                    dataRow["NO1"] = id;
                    dataRow["NO"] = index + 1;

                    DataTable dtCompare = new DataTable();
                    dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                    foreach (string col in listFullColumn)
                    {
                        DataRow drCompare = dtCompare.NewRow();
                        drCompare["SPEC_NM"] = col;
                        if (index < rows.Length)
                            drCompare["DEVICE_INFO"] = rowDeviceInfo[0][col];
                        else
                            drCompare["DEVICE_INFO"] = "";
                        drCompare["MATCHING_INFO"] = obj[col];
                        dtCompare.Rows.Add(drCompare);
                    }

                    ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                    ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                    index++;

                    cpuNm = ConvertUtil.ToString(obj["PRODUCT_NAME"]);
                }
            }
            else if (rows.Length > jArray.Count)
            {
                int index = 0;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                    id++;
                    foreach (string col in ProjectInfo._listDefaultConsignedColum)
                        dataRow[col] = rows[index][col];

                    List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                    for (int i = 1; i < 4; i++)
                    {
                        dataRow[$"DATA{i}"] = rows[index][listConsignedCol[i - 1]];
                        dataRow[$"REP{i}"] = ConvertUtil.ToString(obj[listConsignedCol[i - 1]]);
                    }


                    foreach (string col in ProjectInfo._listKeyColumn)
                        dataRow[col] = obj[col];

                    dataRow["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                    dataRow["WAREHOUSE"] = obj["WAREHOUSE"];
                    dataRow["PALLET"] = obj["PALLET"];
                    dataRow["WAREHOUSING"] = obj["WAREHOUSING"];
                    dataRow["PRICE"] = obj["PRICE"];
                    dataRow["RELEASE_PRICE"] = obj["RELEASE_PRICE"];
                    dataRow["INVENTORY_CNT"] = obj["INVENTORY_CNT"];
                    dataRow["PROXY_PART_ID"] = obj["PROXY_PART_ID"];
                    dataRow["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    dataRow["ASSIGN_COMPONENT_ID"] = obj["ASSIGN_COMPONENT_ID"];

                    dataRow["CHECK"] = false;
                    if (ConvertUtil.ToInt64(obj["INVENTORY_ID"]) > 0)
                        dataRow["INVENTORY_YN"] = true;
                    else
                        dataRow["INVENTORY_YN"] = false;
                    dataRow["PRODUCT_YN"] = false;
                    dataRow["DIFF"] = false;

                    dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    rowDeviceInfo = dtComponentInfo.Select($"ID = {rows[index]["ID"]}");
                    dataRow["OID"] = rows[index]["ID"];

                    dataRow["ID"] = id;
                    dataRow["NO1"] = id;
                    dataRow["NO"] = index + 1;
                    DataTable dtCompare = new DataTable();
                    dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));


                    foreach (string col in listFullColumn)
                    {
                        DataRow drCompare = dtCompare.NewRow();
                        drCompare["SPEC_NM"] = col;
                        drCompare["DEVICE_INFO"] = dtComponentInfo.Rows[0][col];
                        drCompare["MATCHING_INFO"] = obj[col];
                        dtCompare.Rows.Add(drCompare);
                    }

                    ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                    ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);

                    index++;

                    cpuNm = ConvertUtil.ToString(obj["PRODUCT_NAME"]);
                }

                for (int k = index; k < rows.Length; k++)
                {
                    DataRow row = rows[k];
                    DataRow dataRow = ProjectInfo._dtConsignedInfo.NewRow();

                    id++;
                    int devId;
                    foreach (string col in ProjectInfo._listDefaultConsignedColum)
                        dataRow[col] = row[col];

                    List<string> listConsignedCol = ProjectInfo._dicDeviceConsignedColumn[componentCd];
                    for (int i = 1; i < 4; i++)
                        dataRow[$"DATA{i}"] = row[listConsignedCol[i - 1]];

                    dataRow["ID"] = id;
                    dataRow["NO1"] = id;
                    dataRow["NO"] = k + 1;

                    dataRow["DIFF"] = true;
                    dataRow["PRICE"] = 0;
                    dataRow["PROXY_PART_ID"] = -1;
                    dataRow["CONSIGNED_TYPE"] = -1;

                    devId = ConvertUtil.ToInt32(row["ID"]);

                    dtComponentInfo = ProjectInfo._dicDeviceInfoDetail[devId];
                    DataTable dtCompare = new DataTable();
                    dtCompare = dtComponentInfo.Copy();
                    ProjectInfo._dicConsignedInfoDetail.Add(id, dtCompare);
                    ProjectInfo._dtConsignedInfo.Rows.Add(dataRow);
                }
            }


            string[] arrCode = { "I3", "I5", "I7", "I9"};
            string cpuUpperNm = cpuNm.ToUpper();
            int cpuIndex;
            for (cpuIndex = 0; cpuIndex < arrCode.Length; cpuIndex++)
            {
                if (cpuUpperNm.Contains(arrCode[cpuIndex]))
                    break;
            }

            if (cpuIndex == arrCode.Length ||!cpuUpperNm.Contains(arrCode[cpuIndex]))
                return id;


            DataRow[] cpuRow = ProjectInfo._dtConsignedInfo.Select("COMPONENT_CD = 'CPU'");

            foreach(DataRow dr in cpuRow)
            {
                dr.BeginEdit();
                dr["REP1"] = cpuNm;
                dr["DIFF"] = false;
                dr.EndEdit();
            }

            return id;
        }

        private bool checkCompare(string componentCd, DataRow row)
        {
            DataRow[] detailRows;
            string deviceInfo = "";
            string matchingInfo = "";
            DataRow drow;

            if (componentCd.Equals("CPU"))
            {
                string[] arrCode = { "I3", "I5", "I7", "I9" };

                deviceInfo = $"{row["DATA1"]}"; //MODEL_NM
                matchingInfo = $"{row["REP1"]}";

                deviceInfo = deviceInfo.Replace("-", "");
                deviceInfo = deviceInfo.Replace(" ", "");

                matchingInfo = matchingInfo.Replace("-", "");
                matchingInfo = matchingInfo.Replace(" ", "");

                deviceInfo = deviceInfo.ToUpper();
                matchingInfo = matchingInfo.ToUpper();
                int i;
                for (i = 0; i < arrCode.Length; i++)
                {
                    if (deviceInfo.Contains(arrCode[i]))
                        break;
                }

                if (!matchingInfo.Contains(arrCode[i]))
                    return false;

                int indexDevStart = deviceInfo.IndexOf(arrCode[i]);
                string devGen = deviceInfo.Substring(indexDevStart + 2, 1);

                int indexMatStart = matchingInfo.IndexOf(arrCode[i]);
                string matGen = matchingInfo.Substring(indexMatStart + 2, 1);


                if (devGen.Equals(matGen))
                    return true;
                else
                    return false;
            }
            else if (componentCd.Equals("MEM"))
            {
                // 우선 주석처리. 용량만 체크할수 있도록
                //deviceInfo = $"{row["DATA2"]}";//BANDWIDTH
                //matchingInfo = $"{row["REP2"]}";


                //deviceInfo = deviceInfo.Replace("-", "");
                //deviceInfo = deviceInfo.Replace(" ", "");


                //matchingInfo = matchingInfo.Replace("-", "");
                //matchingInfo = matchingInfo.Replace(" ", "");

                //string devGen = "-1";
                //string matGen = "0";
                //if (deviceInfo.Contains("DDR"))
                //{
                //    int indexDevStart = deviceInfo.IndexOf("DDR");
                //    devGen = deviceInfo.Substring(indexDevStart + 3, 1);
                //}
                //else if (deviceInfo.Contains("PC"))
                //{
                //    int indexDevStart = deviceInfo.IndexOf("PC");
                //    devGen = deviceInfo.Substring(indexDevStart + 2, 1);
                //}

                //if (matchingInfo.Contains("DDR"))
                //{
                //    int indexMatStart = matchingInfo.IndexOf("DDR");
                //    matGen = matchingInfo.Substring(indexMatStart + 3, 1);
                //}
                //else if (matchingInfo.Contains("PC"))
                //{
                //    int indexMatStart = matchingInfo.IndexOf("PC");
                //    matGen = matchingInfo.Substring(indexMatStart + 2, 1);
                //}

                //if (!devGen.Equals(matGen))
                //    return false;

                deviceInfo = $"{row["DATA3"]}";//CAPACITY
                matchingInfo = $"{row["REP3"]}";


                deviceInfo = deviceInfo.ToUpper().Replace("MBYTES", "");
                deviceInfo = deviceInfo.ToUpper().Replace("MBYTE", "");

                matchingInfo = matchingInfo.ToUpper().Replace("MBYTES", "");
                matchingInfo = matchingInfo.ToUpper().Replace("MBYTE", "");

                int devCapa = ConvertUtil.ToInt32(deviceInfo);
                int matCapa = ConvertUtil.ToInt32(matchingInfo);


                if (devCapa == matCapa)
                    return true;
                else
                    return false;
            }
            else if (componentCd.Equals("MON"))
            {
                deviceInfo = $"{row["DATA3"]}";//SIZE
                matchingInfo = $"{row["REP3"]}";

                deviceInfo = deviceInfo.Replace("inches", "");
                deviceInfo = deviceInfo.Replace(" ", "");

                matchingInfo = matchingInfo.Replace("inches", "");
                matchingInfo = matchingInfo.Replace("인치", "");
                matchingInfo = matchingInfo.Replace(" ", "");

                double devSize = ConvertUtil.ToDouble(deviceInfo);
                double matSize = ConvertUtil.ToDouble(matchingInfo);

                double compSize = Math.Abs(devSize - matSize);

                if (compSize < 2)
                    return true;
                else
                    return false;
            }
            else if (componentCd.Equals("STG"))
            {
                //우선은 주석처리. 나중에 풀어야함.용량만 체크할수 있도록
                //deviceInfo = $"{row["DATA2"]}";//STG_TYPE
                //matchingInfo = $"{row["REP2"]}";

                //if (!deviceInfo.Equals(matchingInfo))
                //    return false;

                deviceInfo = $"{row["DATA3"]}";//CAPACITY
                matchingInfo = $"{row["REP3"]}";

                string devCAPA;
                string matCAPA;
                int devCapa = 0;
                int matCapa = 0;

                if (deviceInfo.Contains("GB"))
                {
                    devCAPA = deviceInfo.Replace("GB", "").Trim();
                    devCapa = ConvertUtil.ToInt32(devCAPA);
                }
                else if (deviceInfo.Contains("TB"))
                {
                    devCAPA = deviceInfo.Replace("TB", "").Trim();
                    double dCapa = ConvertUtil.ToDouble(devCAPA) * 1000;
                    devCapa = ConvertUtil.ToInt32(dCapa);
                }

                if (matchingInfo.Contains("GB"))
                {
                    matCAPA = matchingInfo.Replace("GB", "").Trim();
                    matCapa = ConvertUtil.ToInt32(matCAPA);
                }
                else if (matchingInfo.Contains("TB"))
                {
                    matCAPA = matchingInfo.Replace("TB", "").Trim();
                    double dCapa = ConvertUtil.ToDouble(matCAPA) * 1000;
                    matCapa = ConvertUtil.ToInt32(dCapa);
                }

                //string devCAPA = deviceInfo.Replace("GB", "").Trim();
                //string matCAPA = matchingInfo.Replace("GB", "").Trim();

                //int devCapa = ConvertUtil.ToInt32(devCAPA);
                //int matCapa = ConvertUtil.ToInt32(matCAPA);
                //string devCAPA = deviceInfo.Substring(0, 1);
                //string matCAPA = matchingInfo.Substring(0, 1);

                if (Math.Abs(devCapa- matCapa) < 2)
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
        }

        private void sbDeleteReleasePart_Click(object sender, EventArgs e)
        {
           
        }

        private bool checkDiff()
        {
           
            DataRow[] rowDiff = ProjectInfo._dtConsignedInfo.Select($"COMPONENT_CD IN ('CPU', 'MEM', 'STG') AND DIFF = true");

            if (rowDiff.Length > 0)
            {
                _dtCompare.Clear();

                foreach (DataRow row in rowDiff)
                {
                    DataRow dr = _dtCompare.NewRow();
                    dr["COMPONENT_CD"] = row["COMPONENT_CD"];
                    dr["DATA1"] = row["DATA1"];
                    dr["DATA2"] = row["DATA2"];
                    dr["DATA3"] = row["DATA3"];
                    dr["REP1"] = row["REP1"];
                    dr["REP2"] = row["REP2"];
                    dr["REP3"] = row["REP3"];
                    _dtCompare.Rows.Add(dr);

                }

                using (dlgCompare compreComponent = new dlgCompare(_dtCompare))
                {
                    DialogResult result = compreComponent.ShowDialog(this);
                    if (result == DialogResult.OK)
                        return true;
                    else if (result == DialogResult.Yes)
                        return false;
                    else
                        return true;
                }            
            }
            else
                return false;
        }

        private void sbConstructProduct_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            if (!_componentCd.Equals("ALL"))
            {
                Dangol.Message($"제품 구성은 전체 화면에서만 가능합니다.");
                return;
            }

            DataRow[] rowCheck = ProjectInfo._dtConsignedInfo.Select($"CHECK = TRUE");
            DataRow[] rowUnCheck = ProjectInfo._dtConsignedInfo.Select($"CHECK = FALSE");

            if (rowCheck.Length < 2)
            {
                Dangol.Message("제품은 2개 이상의 부품으로 구성돼야 합니다.");
                return;
            }

            Dictionary<string, string> dicInventoryId = new Dictionary<string, string>();
            List<long> listInventoryId = new List<long>();
            long inventoryId = -1;
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
                Dangol.Message("메인보드가 체크되지 않았습니다.");
                return;
            }

            if (mbdInventoryId < 1)
            {
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return;
            }

            if (inventoryCnt < 2)
            {
                Dangol.Message("제품 구성은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            if (Dangol.MessageYN($"선택하신 부품들로 제품을 구성하시겠습니까?") == DialogResult.No)
                return;

            if (DBConnect.constructProduct(_representativeType, _representativeNo, _representativeCol, mbdInventoryId, listInventoryId, ref jResult))
            {
                string id = "";
                string barcode = "";
                if (ProjectInfo._dicProductList == null)
                    ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
                else
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

                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void sbAddReleasePart_Click(object sender, EventArgs e)
        {
           

        }

        private bool checkRepresentativeInfo()
        {
            if(tnReceipt.EditValue == null)
                return false;
            else
             _representativeNo = tnReceipt.EditValue.ToString();

            if (string.IsNullOrWhiteSpace(_representativeNo))
                return false;
            else
            {
                return true;
            }
            
        }

       

        private void sbUpdateComponent_Click(object sender, EventArgs e)
        {
            putConsignedCheckInfo();

            if (checkDiff())
            {
                return;
            }

            string msg = "부품 정보가 수정되었습니다.";

            if (_componentCd.Equals("ALL"))
            {
                DataRow[] rowCheck = ProjectInfo._dtConsignedInfo.Select($"CHECK = TRUE");
                DataRow[] rowValidity = ProjectInfo._dtConsignedInfo.Select($"CHECK = TRUE AND ((PROXY_PART_ID > 0 AND INVENTORY_ID > 0) OR COMPONENT_CD = 'CPU')");

                string componentCd;
                DataTable dtComponentInfo;
                DataRow[] rowDeviceInfo;
                DataRow[] row;
                JObject jResult = new JObject();
                int oId;
                int id;
                long componentId;
                long inventoryId;

                if (rowCheck.Length < 1)
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                if (rowValidity.Length < 1)
                {
                    Dangol.Message("업데이트 가능한 부품이 없습니다.");
                    return;
                }

                foreach (DataRow drCheck in rowValidity)
                {
                    oId = ConvertUtil.ToInt32(drCheck["OID"]);
                    id = ConvertUtil.ToInt32(drCheck["ID"]);
                    inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                    if (inventoryId < 0)
                    {
                        if(componentCd.Equals("CPU"))
                        {
                            dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                            rowDeviceInfo = dtComponentInfo.Select($"ID = {oId}");
                            AddInventory(id, componentCd, rowDeviceInfo);
                        }
 
                        continue;
                    }

                    if (ProjectInfo._listUncheckComponentCd.Contains(componentCd))
                        continue;

                    dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    rowDeviceInfo = dtComponentInfo.Select($"ID = {oId}");
                    row = ProjectInfo._dtDeviceInfo.Select($"ID = {oId}");

                    if (rowDeviceInfo.Length > 0)
                    {
                        if (DBConnect.updateInventoryInfo(inventoryId, componentCd, rowDeviceInfo[0], ref jResult))
                        {
                            DataTable dtDeviceInfoFromDB = ProjectInfo._dicConsignedInfoDetail[id];
                            foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                            {
                                dr.BeginEdit();
                                dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                                dr.EndEdit();
                            }

                            if (componentCd.Equals("MBD"))
                            {
                                JObject jResultos = new JObject();
                                DBConnect.osInfoCheck(inventoryId, rowDeviceInfo[0], ref jResultos, _representativeNo);
                            }

                            msg = ConvertUtil.ToString(jResult["MSG"]);
                        }
                        else
                            msg = ConvertUtil.ToString(jResult["MSG"]);
                    }
                }

                Dangol.Message(msg);

            }
            else
            {
                int oId = ConvertUtil.ToInt32(_currentRow["OID"]);
                int id = ConvertUtil.ToInt32(_currentRow["ID"]);
                DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_componentCd];
                DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {oId}");
                JObject jResult = new JObject();

                if (rowDeviceInfo.Length > 0)
                {
                    if (DBConnect.updateInventoryInfo(_inventoryId, _currentComponentCd, rowDeviceInfo[0], ref jResult))
                    {
                        DataTable dtDeviceInfoFromDB = ProjectInfo._dicConsignedInfoDetail[id];
                        foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                        {
                            dr.BeginEdit();
                            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                            dr.EndEdit();
                        }

                        if (_currentComponentCd.Equals("MBD"))
                        {
                            JObject jResultos = new JObject();
                            DBConnect.osInfoCheck(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), rowDeviceInfo[0], ref jResultos);
                        }

                        msg = ConvertUtil.ToString(jResult["MSG"]);
                    }
                    else
                        msg = ConvertUtil.ToString(jResult["MSG"]);
                }

                Dangol.Message(msg);
            }

        }

        private void AddInventory(int id, string componentCd, DataRow[] rowDeviceInfo)
        {
            JObject jResult = new JObject();

            if (DBConnect.InsertInventory(_representativeType, _representativeNo, _representativeCol, componentCd, "-1", -1, rowDeviceInfo[0], -1, ref jResult))
            {
                DataRow[] rows = ProjectInfo._dtConsignedInfo.Select($"ID = {id}");
                foreach (DataRow dr in rows)
                {
                    dr.BeginEdit();
                    foreach (string col in ProjectInfo._listKeyColumn)
                        dr[col] = jResult[col];

                    for (int i = 1; i < 4; i++)
                        dr[$"REP{i}"] = dr[$"DATA{i}"];

                    dr["PRICE"] = 0;
                    dr["RELEASE_PRICE"] = 0;
                    dr["INVENTORY_CNT"] = 0;
                    dr["INVENTORY_YN"] = true;
                    dr["PRODUCT_YN"] = false;

                    dr.EndEdit();
                }

                DataTable dtDeviceInfoFromDB = ProjectInfo._dicConsignedInfoDetail[id];
                foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                {
                    dr.BeginEdit();
                    dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                    dr.EndEdit();
                }

                if (componentCd.Equals("MBD"))
                {
                    JObject jResultos = new JObject();
                    DBConnect.osInfoCheck(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), rowDeviceInfo[0], ref jResultos);
                }

                DBConnect.updateInventoryState(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), "R", "G", "N", ref jResult);
            }
        }

        private void sbPrintProduct_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            if (!_componentCd.Equals("ALL"))
            {
                Dangol.Message($"제품 출력은 전체 화면에서만 가능합니다.");
                return;
            }

            DataRow[] rowCheck = ProjectInfo._dtConsignedInfo.Select($"CHECK = TRUE");

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
                Dangol.Message("접수번호를 입력해주세요.");
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

        private void sbCheck_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return;
            }

            //if (!_isWarehousingCheck)
            //{
            //    if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
            //    {
            //        return;
            //    }
            //    _isWarehousingCheck = true;
            //}



            int checkValue = ConvertUtil.ToInt32(leProductType.EditValue);


            if (checkValue == 1)
            {
                Dangol.Message("PC는 개별 부품 체크만 가능합니다.");
                return;
            }

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

            JObject jResult = new JObject();
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


            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();
            string checkText = leProductType.Text;
            if (ProjectInfo._type != checkValue)
            {
                if (Dangol.MessageYN($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{ProjectInfo._typeNm}, 선택:{checkText})", $"{checkText} 검수 체크") == DialogResult.No)
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

            row = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");

            if (row.Length > 0)
            {
                //if (Dangol.MessageYN($"등록된 재고가 {row.Length}개 입니다. 검수를 진행 하시겠습니까?", $"{checkText} 검수 체크") == DialogResult.No)
                //    return;

                foreach (DataRow dr in row)
                    listInventoryId.Add(ConvertUtil.ToInt64(dr["INVENTORY_ID"]));
            }

            if (checkValue == 2)
            {
                //if ($"{checkText} 검수 체크".Equals("B200806001") || $"{checkText} 검수 체크".Equals("B201030004") || _warehousingDate > Convert.ToDateTime("2020-11-11"))
                //{
                    using (DlgNtb2ndEditionCheck ntbCheck = new DlgNtb2ndEditionCheck(ProjectInfo._dicNtbCheckRelease, ProjectInfo._dicNtbCheckWarehousing, ref ProjectInfo._dtNTBAdjustmentPrice, ProjectInfo._dicNTBAdjustmentPrice, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, ProjectInfo._productGradeRelease, _dtPrintPort, _dtPGrade, _bsPallet, ConvertUtil.ToInt64(lePallet.EditValue), _checkType))
                    {
                        if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            ProjectInfo._caseDestroyDescriptionRelease = ntbCheck._caseDestroyDescription;
                            ProjectInfo._batteryRemainRelease = ntbCheck._batteryRemain;
                            ProjectInfo._productGradeRelease = ntbCheck._pGrade;

                            long warehouse = ConvertUtil.ToInt64(leLocation.EditValue);

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

                        DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, "","", barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckRelease, listInventoryId, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, ProjectInfo._productGradeRelease);

                        if (!DBAdjustment.insertAdjustmentPrice(inventoryId, _checkType, ref jResult))
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }

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

                            //DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckRelease, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, ProjectInfo._productGradeRelease, ProjectInfo._printerPort);
                        }

                        }
                    }
            //    }
            //    else
            //    {
            //        using (DlgNtbCheck ntbCheck = new DlgNtbCheck(ProjectInfo._dicNtbCheck, _dtPrintPort))
            //        {
            //            if (ntbCheck.ShowDialog(this) == DialogResult.OK)
            //            {
            //                DBConnect.insertNtbCheck1stEdition(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheck, listInventoryId);

            //                if (ntbCheck._isPrint)
            //                    DBConnect.printNtbCheck1stEdition(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheck, ProjectInfo._printerPort);

            //            }
            //        }
            //    }
            }
            else if (checkValue == 3)
            {
                using (DlgAllInOneCheck2 allInOneCheck = new DlgAllInOneCheck2(ProjectInfo._dicAllInOneCheckRelease, _dtPrintPort))
                {
                    if (allInOneCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        //DBConnect.insertAllInOneCheck(barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckRelease, listInventoryId);

                        if (allInOneCheck._isPrint)
                            DBConnect.printAllInOneCheck(barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckRelease, ProjectInfo._printerPort);
                    }
                }
            }
        }

        private void sbPartCheck_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            //if (!_isWarehousingCheck)
            //{
            //    if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
            //    {
            //        return;
            //    }
            //    _isWarehousingCheck = true;
            //}

            if (string.IsNullOrEmpty(_barcode) || _inventoryId < 0)
            {
                Dangol.Message("선택하신 부품은 재고로 등록되지 않았습니다.");
                return;
            }

            if (!ProjectInfo._dicPartCheckRelease.ContainsKey(_inventoryId))
                getCheckInfo(_inventoryId, _currentComponentCd);

            Dictionary<string, int> dicCheckInfo = ProjectInfo._dicPartCheckRelease[_inventoryId];

            if (_currentComponentCd.Equals("MON"))
            {
                string size = ProjectInfo._dicMonSizeRelease[_inventoryId];

                using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(dicCheckInfo,
                    ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                     ProjectInfo._dicMonSizeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (monitorCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = monitorCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = monitorCheck._pGrade;
                        ProjectInfo._dicMonSizeRelease[_inventoryId] = monitorCheck._size;

                        DBConnect.insertMonitorCheck(_barcode, _checkType, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._dicMonSizeRelease[_inventoryId]);

                        if (monitorCheck._isPrint)
                            DBConnect.printMonitorCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._dicMonSizeRelease[_inventoryId],ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("CAS"))
            {
                using (DlgCasCheck casCheck = new DlgCasCheck(dicCheckInfo, 
                    ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (casCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = casCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = casCheck._pGrade;

                        DBConnect.insertCasCheck(_barcode, _checkType, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (casCheck._isPrint)
                            DBConnect.printCasCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("CPU"))
            {
                using (DlgCpuCheck inventoryCheck = new DlgCpuCheck(dicCheckInfo,
                    ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("MEM"))
            {
                using (DlgMEMCheck inventoryCheck = new DlgMEMCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("MBD"))
            {
                using (DlgMbdCheck inventoryCheck = new DlgMbdCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("VGA"))
            {
                using (DlgVgaCheck inventoryCheck = new DlgVgaCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("STG"))
            {
                string type = ConvertUtil.ToString(_currentRow["STG_TYPE"]);

                if (type.Contains("SSD"))
                {
                    using (DlgSsdCheck inventoryCheck = new DlgSsdCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                    {
                        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                            ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                            if (inventoryCheck._isPrint)
                                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                        }
                    }
                }
                else
                {
                    using (DlgHddCheck inventoryCheck = new DlgHddCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                    {
                        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                            ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                            if (inventoryCheck._isPrint)
                                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("ODD"))
            {
                using (DlgOddCheck inventoryCheck = new DlgOddCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("POW"))
            {
                using (DlgPowCheck inventoryCheck = new DlgPowCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                return;
            }
        }

        

        private void sbGetDeviceInfo_Click(object sender, EventArgs e)
        {
            if (teBarcode.EditValue == null || teBarcode.EditValue.ToString().Length < 12)
            {
                Dangol.Message("관리번호를 입력하세요.");
                return;
            }

            string barcode = teBarcode.EditValue.ToString();
            DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_currentComponentCd];
            JObject jResult = new JObject();


            if (DBConnect.getInventoryFromConsigned(barcode, _currentComponentCd, ref jResult))
            {
                _currentRow.BeginEdit();
                DataRow[] rowBarcode = ProjectInfo._dtDeviceInfo.Select($"BARCODE = '{barcode}'");

                if (rowBarcode.Length > 0)
                { 
                    Dangol.Message("이미 현재 제품에 존재하는 부품입니다.");
                    return;
                }

                JObject jData = (JObject)jResult["DATA"];

                _currentRow["INVENTORY_ID"] = dtComponentInfo.Rows[0]["INVENTORY_ID"] = jData["INVENTORY_ID"];
                _currentRow["BARCODE"] = dtComponentInfo.Rows[0]["BARCODE"] = jData["BARCODE"];
                _currentRow["COMPONENT_ID"] = dtComponentInfo.Rows[0]["COMPONENT_ID"] = jData["COMPONENT_ID"];
                _currentRow["COMPONENT"] = dtComponentInfo.Rows[0]["COMPONENT"] = jData["COMPONENT"];

                _currentRow["INVENTORY_YN"] = true;

                DataTable dtConsignedInfoFromDB = ProjectInfo._dicConsignedInfoDetail[_id];
                DataRow[] detailRows;

                string componentCd = jData["COMPONENT_CD"].ToString();
                List<string> listFullColum = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColum)
                {
                    detailRows = dtConsignedInfoFromDB.Select($"SPEC_NM = '{col}'");
                    if (detailRows.Length > 0)
                        detailRows[0]["MATCHING_INFO"] = jData[col];
                }

                gcInventoryDetail.DataSource = null;
                bsDetail.DataSource = dtConsignedInfoFromDB;
                gcInventoryDetail.DataSource = bsDetail;
                    

                _barcode = ConvertUtil.ToString(jData["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(jData["INVENTORY_ID"]);
                _component = ConvertUtil.ToString(jData["COMPONENT"]);
                _componentId = ConvertUtil.ToInt64(jData["COMPONENT_ID"]);

                getCheckInfo(_inventoryId, _currentComponentCd);

                setBarcodeButton(true, true, false);

                if (_currentComponentCd.Equals("MBD"))
                    Util.checkProductState(_inventoryId, _barcode);
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

                Dangol.Message("부품 정보를 변경하였습니다.");
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            
        }

        private void setBarcodeButton(bool teBarcodeFlag, bool sbClearFlag, bool sbGetDeviceInfoFlag)
        {
            teBarcode.Properties.ReadOnly = teBarcodeFlag;
            sbClear.Enabled = sbClearFlag;
            sbGetDeviceInfo.Enabled = sbGetDeviceInfoFlag;
        }

        private void sbGetInventoryInfo_Click(object sender, EventArgs e)
        {

        }

        private void leProductType_EditValueChanged(object sender, EventArgs e)
        {
            ProjectInfo._type = ConvertUtil.ToInt32(leProductType.EditValue);
            ProjectInfo._typeNm = leProductType.Text;
        }

        private void gvInventory_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (ConvertUtil.ToInt32(gvInventory.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void ribeAsign_Click(object sender, EventArgs e)
        {
            if(ConvertUtil.ToInt64(_currentRow["PROXY_PART_ID"]) > 0)
            {

            }
        }

        private void gvInventory_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "COMPONENT_CD")
            {
                string componentCd = View.GetRowCellDisplayText(e.RowHandle, View.Columns["COMPONENT_CD"]);
                if (!ProjectInfo._listUncheckComponentCd.Contains(componentCd))
                {
                    string temp = View.GetRowCellDisplayText(e.RowHandle, View.Columns["DIFF"]);
                    if (temp.Equals("Checked"))
                    {
                        e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                    }
                }
            }

            if (e.Column.FieldName == "INVENTORY_YN")
            {
                object inventoryYn = View.GetRowCellValue(e.RowHandle, View.Columns["INVENTORY_YN"]);
                if (ConvertUtil.ToBoolean(inventoryYn))
                {
                    string comp = View.GetRowCellDisplayText(e.RowHandle, View.Columns["COMPONENT_ID"]);
                    string assignComp = View.GetRowCellDisplayText(e.RowHandle, View.Columns["ASSIGN_COMPONENT_ID"]);

                    if (!comp.Equals(assignComp))
                        e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
                }
            }
        }

        private void leLocation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
        }

        private void sbCam_Click(object sender, EventArgs e)
        {
            Process.Start("microsoft.windows.camera:");
        }

        private void sbKeyboard_Click(object sender, EventArgs e)
        {
            string keyboardTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\key_test.exe";
            Process.Start(keyboardTestProgramPath);
        }

        private void sbsound_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "DlgSoundTest")
                {
                    frm.Activate();
                    return;
                }
            }

            DlgSoundTest soundTest = new DlgSoundTest();
            soundTest.Show();

            Process.Start("ms-settings:sound");
            //using (DlgSoundTest soundTest = new DlgSoundTest())
            //{
            //    soundTest.Show();

            //}
        }

        private void sbBluetooth_Click(object sender, EventArgs e)
        {
            //Process.Start("ms-settings:bluetooth");
        }

        private void sbSetting_Click(object sender, EventArgs e)
        {
            string command = "/C DEVMGMT.MSC";
            Process.Start("cmd.exe", command);
        }

        private void sbBattery_Click(object sender, EventArgs e)
        {
            string batteryTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\Battery Status\BattStat.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo(batteryTestProgramPath);
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Process.Start(startInfo);
        }

        private void sbDisk_Click(object sender, EventArgs e)
        {
            
            string command = "/C DISKMGMT.MSC";
            Process.Start("cmd.exe", command);

            //string storageTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\Hard Disk Sentinel\HDSentinel.exe";
            //Process.Start(storageTestProgramPath);
        }
        private void sbMyCom_Click(object sender, EventArgs e)
        {
            Process.Start("ms-settings:about");
        }

        private void sbCapture_Click(object sender, EventArgs e)
        {
            int captureType = ConvertUtil.ToInt32(leCaptureType.EditValue);
            setCapture(captureType);

            if(captureType == 1)
                lcCapture1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else if (captureType == 2)
                lcCapture2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //Dangol.Message("캡쳐 이미지가 저장되었습니다.");
        }

        private void setCapture(int captureType)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Warining("접수번호를 입력해주세요.");
                return;
            }
            Form mainForm = this;

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Main")
                {
                    mainForm = frm;
                    frm.SendToBack();
                    break;
                }
            }

            string fileNm = "";

            if (captureType == 1)
                fileNm = $"{_representativeNo}.png";
            else
                fileNm = $"{_representativeNo}_{captureType}.png";

            Image image = ScreenCapture.Copy(mainForm, "consignedRelease", fileNm);

            using (DlgImgTest digImgTest = new DlgImgTest(image))
            {
                //_isCapture = true;
                digImgTest.ShowDialog(this);
                File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{fileNm}");
            }

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            jobj.RemoveAll();
            jobj.Add("RECEIPT", _representativeNo);
            jobj.Add($"CAPTURE{captureType}_YN", "Y");

            DBConsigned.updateReceiptSimple(jobj, ref jResult);

        }

        private void usrReleaseConsigned_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                sbCapture_Click(null, null);
            }
        }

        private void gcInventory_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                sbCapture_Click(null, null);
            }
        }

        

        private void leCaptureType_EditValueChanged(object sender, EventArgs e)
        {
            lcgConsingedInfo.BeginUpdate();

            if (ConvertUtil.ToInt32(leCaptureType.EditValue) == 1)
            {
                sbCapture.Enabled = true;

                lcMyCom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcSetting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcCamera.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcDisk.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcKeyboard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcSound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcBattery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if(ConvertUtil.ToInt32(leCaptureType.EditValue) == 2)
            {
                sbCapture.Enabled = true;

                lcMyCom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcSetting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCamera.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcDisk.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcKeyboard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcSound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcBattery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcMyCom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcSetting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCamera.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcDisk.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcKeyboard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcSound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcBattery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                sbCapture.Enabled = false;
            }


            lcgConsingedInfo.EndUpdate();
        }

        private void sbGetCapture1_Click(object sender, EventArgs e)
        {
            Image image = ScreenCapture.GetCaptureImg("consignedRelease", $"{_representativeNo}.png");

            using (DlgImgTest digImgTest = new DlgImgTest(image))
            {
                digImgTest.ShowDialog(this);
                File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_representativeNo}.png");
            }
        }

        private void sbGetCapture2_Click(object sender, EventArgs e)
        {
            Image image = ScreenCapture.GetCaptureImg("consignedRelease", $"{_representativeNo}_2.png");

            using (DlgImgTest digImgTest = new DlgImgTest(image))
            {
                digImgTest.ShowDialog(this);
                File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_representativeNo}_2.png");
            }
        }
    }
}