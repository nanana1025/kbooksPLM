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
using DevExpress.XtraLayout;

namespace WareHousingMaster.view.warehousing
{
    public partial class usrInventoryList : DevExpress.XtraEditors.XtraForm
    {

        string _representativeType = "W";
        string _representativeCol = "WAREHOUSING";
        string _representativeIdCol = "WAREHOUSING_ID";
        string _representativeNo = null;
        long _representativeId = -1;

        object _companyId = -1;

        long _type = 1;

        string _componentCd = "ALL";

        DataRowView _currentComponent;
        DataRowView _currentInventory;
        DataRowView _currentBarcode;

        DataTable _dtBarcodeList;

        DataTable _dtWarehousingComponent;
        DataTable _dtWarehousingPart;
        DataTable _dtWarehousingInvnetory;

        DataTable _dtPallet;

        BindingSource _bs;
        BindingSource _bsWarehousingComponent;
        BindingSource _bsWarehousingInvnetory;
        BindingSource _bsPallet;

        BindingSource _bsBarcodeList;

        long _id;
        bool _headerButtonVisible = true;
        short _checkType = 1;
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

        List<string> _listBarcode;

        Dictionary<string, Control> _dicControl;
        Dictionary<string, LayoutControlItem> _diLayoutControl;

        public usrInventoryList()
        {
            InitializeComponent();


            _dtBarcodeList = new DataTable();
            _dtBarcodeList.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtBarcodeList.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("CHECK", typeof(bool)));

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
            _bsBarcodeList = new BindingSource();
            _bs = new BindingSource();
            _bsPallet = new BindingSource();

            _bs.DataSource = _dtWarehousingPart;

            _bsWarehousingComponent.DataSource = _dtWarehousingComponent;
            _bsWarehousingInvnetory.DataSource = _dtWarehousingInvnetory;
            _bsBarcodeList.DataSource = _dtBarcodeList;
            //tnReceipt.EditValue = "LT201130001";
            //lcComponent.Text = _componentCd;



            _listBarcode = new List<string>();
            _dicControl = new Dictionary<string, Control>()
            {
                {"CPU", this.usrComponentCPU1}, {"MBD", this.usrComponentMBD1}, {"MEM", this.usrComponentMEM1}, 
                {"STG", this.usrComponentSTG1}, {"VGA", this.usrComponentVGA1}, {"MON", this.usrComponentMON1}, 
                {"CAS", this.usrComponentCAS1}, {"ADP", this.usrComponentADP1}, {"POW", this.usrComponentPOW1}, 
                {"KEY", this.usrComponentKEY1}, {"MOU", this.usrComponentMOU1}, {"FAN", this.usrComponentFAN1},
                {"CAB", this.usrComponentCAB1}, {"BAT", this.usrComponentBAT1}, {"PKG", this.usrComponentPKG1},
                {"AIR", this.usrComponentAIR1}, {"LIC", this.usrComponentLIC1}, {"PER", this.usrComponentPER1}
            };

            _diLayoutControl = new Dictionary<string, LayoutControlItem>()
            {
                {"CPU", this.lcCPU}, {"MBD", this.lcMBD}, {"MEM", this.lcMEM},
                {"STG", this.lcSTG}, {"VGA", this.lcVGA}, {"MON", this.lcMON},
                {"CAS", this.lcCAS}, {"ADP", this.lcADP}, {"POW", this.lcPOW},
                {"KEY", this.lcKEY}, {"MOU", this.lcMOU}, {"FAN", this.lcFAN},
                {"CAB", this.lcCAB}, {"BAT", this.lcBAT}, {"PKG", this.lcPKG},
                {"AIR", this.lcAIR}, {"LIC", this.lcLIC}, {"PER", this.lcPER},
                {"EMPTY", this.lcEmpty}
            };

            if (ProjectInfo._dicConsignedInfoDetail == null)
            {
                ProjectInfo._dicConsignedInfoDetail = new Dictionary<long, DataTable>();
            }
           
        }


        private void getCheckInfo(long inventoryId, string componentCd)
        {
            JObject jResult = new JObject();

            if (!ProjectInfo._dicPartCheckRelease.ContainsKey(inventoryId))
            {
                Dictionary<string, int> dicData = new Dictionary<string, int>();

                if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            int value = x.Value.ToObject<int>();

                            if (!dicData.ContainsKey(name))
                                dicData.Add(name, value);
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
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                    ProjectInfo._dicNtbCheckRelease.Add(name, value);
                            }
                        }
                    }
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



        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            //string serialNo = ProjectInfo.osProductNo;
            //serialNo = serialNo.Replace("-", "");
            //serialNo = serialNo.Substring(1, 14);
            //teSerialNo.Text = serialNo;
            //teSerialNo.EditValue = serialNo;

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

            //foreach (KeyValuePair<string, XtraUserControl> item in _dicControl)
            //{
            //    XtraUserControl control = item.Value;
            //    //Console.WriteLine("Key: {0}, Value: {1}", kv.Key, kv.Value);
            //}

            setComponentInitialize();
        }


        private void setComponentInitialize()
        {

            usrComponentADP1._componentCd = "ADP";
            usrComponentADP1.setinitialize();

            usrComponentAIR1._componentCd = "AIR";
            usrComponentAIR1.setinitialize();

            usrComponentBAT1._componentCd = "BAT";
            usrComponentBAT1.setinitialize();

            usrComponentCAB1._componentCd = "CAB";
            usrComponentCAB1.setinitialize();

            usrComponentCAS1._componentCd = "CAS";
            usrComponentCAS1.setinitialize();

            usrComponentCPU1._componentCd = "CPU";
            usrComponentCPU1.setinitialize();

            usrComponentFAN1._componentCd = "FAN";
            usrComponentFAN1.setinitialize();

            usrComponentKEY1._componentCd = "KEY";
            usrComponentKEY1.setinitialize();

            usrComponentLIC1._componentCd = "LIC";
            usrComponentLIC1.setinitialize();

            usrComponentMBD1._componentCd = "MBD";
            usrComponentMBD1.setinitialize();

            usrComponentMEM1._componentCd = "MEM";
            usrComponentMEM1.setinitialize();

            usrComponentMON1._componentCd = "MON";
            usrComponentMON1.setinitialize();

            usrComponentMOU1._componentCd = "MOU";
            usrComponentMOU1.setinitialize();

            usrComponentPER1._componentCd = "PER";
            usrComponentPER1.setinitialize();

            usrComponentPKG1._componentCd = "PKG";
            usrComponentPKG1.setinitialize();

            usrComponentPOW1._componentCd = "POW";
            usrComponentPOW1.setinitialize();

            usrComponentSTG1._componentCd = "STG";
            usrComponentSTG1.setinitialize();

            usrComponentVGA1._componentCd = "VGA";
            usrComponentVGA1.setinitialize();
        }

        private void setComponentInitializeData()
        {
            usrComponentADP1.setDataInitialize();
            usrComponentAIR1.setDataInitialize();
            usrComponentBAT1.setDataInitialize();
            usrComponentCAB1.setDataInitialize();
            usrComponentCAS1.setDataInitialize();
            usrComponentCPU1.setDataInitialize();
            usrComponentFAN1.setDataInitialize();
            usrComponentKEY1.setDataInitialize();
            usrComponentLIC1.setDataInitialize();
            usrComponentMBD1.setDataInitialize();
            usrComponentMEM1.setDataInitialize();
            usrComponentMON1.setDataInitialize();
            usrComponentMOU1.setDataInitialize();
            usrComponentPER1.setDataInitialize();
            usrComponentPKG1.setDataInitialize();
            usrComponentPOW1.setDataInitialize();
            usrComponentSTG1.setDataInitialize();
            usrComponentVGA1.setDataInitialize();
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

            //DataTable dtDeviceType = new DataTable();

            //dtDeviceType.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtDeviceType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //for (int i = 0; i < ProjectInfo._arrTypeNm.Length; i++)
            //{
            //    DataRow dr = dtDeviceType.NewRow();

            //    dr["KEY"] = i;
            //    dr["VALUE"] = ProjectInfo._arrTypeNm[i];
            //    dtDeviceType.Rows.Add(dr);
            //}

            //Util.LookupEditHelper(leProductType, dtDeviceType, "KEY", "VALUE");

            DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");

            Util.insertRowonTop(dtLocation, "-1", " 없음");

            Util.LookupEditHelper(leLocation, dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(rileWarehouse, dtLocation, "KEY", "VALUE");

            

            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");

            Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
            dicPalletDefault.Add("WAREHOUSE_ID", "-1");
            dicPalletDefault.Add("PALLET_ID", "");
            dicPalletDefault.Add("PALLET_NM", "선택안합");

            Util.insertRowonTop(_dtPallet, dicPalletDefault);

            _bsPallet.DataSource = _dtPallet;

            //DataTable dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");

            DataTable dtComponentCd = Util.getCodeList("CD0101", "KEY", "VALUE");
            //Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtInvnetoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            //Util.LookupEditHelper(rileInventoryState, dtInvnetoryState, "KEY", "VALUE");

            DataTable dtInvnetoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            //Util.LookupEditHelper(rileInventoryCat, dtInvnetoryCat, "KEY", "VALUE");
            Util.LookupEditHelper(leInventoryCat, dtInvnetoryCat, "KEY", "VALUE");
            

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

            //Util.LookupEditHelper(leComponentCd, dtComponentCd, "KEY", "VALUE");

            _dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");

            //dtInvnetoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.insertRowonTop(dtInvnetoryState, "-1", "");
            Util.insertRowonTop(dtInvnetoryState, "-2", "등록안됨");
            Util.LookupEditHelper(rileInventoryState, dtInvnetoryState, "KEY", "VALUE");

            

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;
            //leLocation.EditValue = ProjectInfo._location;
            //leProductType.EditValue = ProjectInfo._type;
            //teUserName.EditValue = ProjectInfo._userName;

        }

       

        private void setGridControl()
        {
            //gcWarehousingComponent.DataSource = null;
            //gcWarehousingComponent.DataSource = _bsWarehousingComponent;

            gcBarcodeList.DataSource = null;
            gcBarcodeList.DataSource = _bsBarcodeList;

            //gcWarehousingInvnetory.DataSource = null;
            //gcWarehousingInvnetory.DataSource = _bsWarehousingInvnetory;


            
        }

        private void leLocation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            changePallet(e.NewValue);
        }

        private void changePallet(object palletId)
        {
            _bsPallet.Filter = $"WAREHOUSE_ID = '{palletId}' OR WAREHOUSE_ID = '-1'";
            //lePallet.ItemIndex = 0;
        }


        private void leComponentCd_EditValueChanged(object sender, EventArgs e)
        {
            //_componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
            //if(!string.IsNullOrEmpty(_componentCd))
            //    setGridControl();
        }


        private void gvWarehousingComponent_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            //bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingComponent.RowCount > 0);

            //if (isValidRow)
            //{
            //    _currentComponent = e.Row as DataRowView;
            //    getWarehousingInventory(_currentComponent["COMPONENT_ID"]);
            //}
        }


        private void gvWarehousingInvnetory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            //bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingInvnetory.RowCount > 0);

            //if (isValidRow)
            //{
            //    _currentInventory = e.Row as DataRowView;
            //    changePallet(_currentInventory["WAREHOUSE"]);
            //}
        }


        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            

        }

        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
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
                //jPartInfo.Add("INVENTORY_CAT", ConvertUtil.ToString(leInventoryCat.EditValue));
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

                    //_currentInventory["INVENTORY_CAT"] = jPartInfo["INVENTORY_CAT"];
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


        private void sbClear_Click(object sender, EventArgs e)
        {
           
        }

        private void risePrice_EditValueChanged(object sender, EventArgs e)
        {
           
        }


        private void lcgInventory_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
           
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
           
        }

        private void sbGetReleasePart_Click(object sender, EventArgs e)
        {
            getReleasePart();
        }
        private void getReleasePart()
        { 
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
                deviceInfo = $"{row["DATA2"]}";//BANDWIDTH
                matchingInfo = $"{row["REP2"]}";


                deviceInfo = deviceInfo.Replace("-", "");
                deviceInfo = deviceInfo.Replace(" ", "");


                matchingInfo = matchingInfo.Replace("-", "");
                matchingInfo = matchingInfo.Replace(" ", "");

                string devGen = "-1";
                string matGen = "0";
                if (deviceInfo.Contains("DDR"))
                {
                    int indexDevStart = deviceInfo.IndexOf("DDR");
                    devGen = deviceInfo.Substring(indexDevStart + 3, 1);
                }
                else if (deviceInfo.Contains("PC"))
                {
                    int indexDevStart = deviceInfo.IndexOf("PC");
                    devGen = deviceInfo.Substring(indexDevStart + 2, 1);
                }

                if (matchingInfo.Contains("DDR"))
                {
                    int indexMatStart = matchingInfo.IndexOf("DDR");
                    matGen = matchingInfo.Substring(indexMatStart + 3, 1);
                }
                else if (matchingInfo.Contains("PC"))
                {
                    int indexMatStart = matchingInfo.IndexOf("PC");
                    matGen = matchingInfo.Substring(indexMatStart + 2, 1);
                }

                if (!devGen.Equals(matGen))
                    return false;

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
                deviceInfo = $"{row["DATA2"]}";//STG_TYPE
                matchingInfo = $"{row["REP2"]}";

                if (!deviceInfo.Equals(matchingInfo))
                    return false;

                deviceInfo = $"{row["DATA3"]}";//CAPACITY
                matchingInfo = $"{row["REP3"]}";

                string devCAPA = deviceInfo.Substring(0, 1);
                string matCAPA = matchingInfo.Substring(0, 1);

                if (devCAPA.Equals(matCAPA))
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
            //using (dlgCompare compreComponent = new dlgCompare())
            //{
            //    if (compreComponent.ShowDialog(this) == DialogResult.OK)
            //    {

            //    }
            //}
        }

        private void sbConstructProduct_Click(object sender, EventArgs e)
        {
            //if (!checkRepresentativeInfo())
            //{
            //    Dangol.Message("접수번호를 입력해주세요.");
            //    return;
            //}

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

        //private bool checkRepresentativeInfo()
        //{
        //    if(tnReceipt.EditValue == null)
        //        return false;
        //    else
        //     _representativeNo = tnReceipt.EditValue.ToString();

        //    if (string.IsNullOrWhiteSpace(_representativeNo))
        //        return false;
        //    else
        //    {
        //        return true;
        //    }
            
        //}

       

        private void sbUpdateComponent_Click(object sender, EventArgs e)
        {
            //if (!checkRepresentativeInfo())
            //{
            //    Dangol.Message("접수번호를 입력해주세요.");
            //    return;
            //}

            JObject jResult = new JObject();

            if (DBConnect.getWarehousingComponentPart(_representativeNo, ref jResult))
            {
                _dtWarehousingComponent.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {

                    JObject jData = (JObject)jResult["DATA"];
                    _representativeId = ConvertUtil.ToInt64(jData["WAREHOUSING_ID"]);
                    //teWarehousingDt.Text = ConvertUtil.ToDateTimeNull(jData["WAREHOUSING_DT"]);
                    //teCustomer.Text = ConvertUtil.ToString(jData["CUSTOMER_ID"]);

                    JArray jArray = JArray.Parse(jResult["DATALIST"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
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
            //if (!checkRepresentativeInfo())
            //{
            //    Dangol.Message("접수번호를 입력해주세요.");
            //    return;
            //}

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
            //if (!checkRepresentativeInfo())
            //{
            //    Dangol.Message("접수번호를 입력해주세요.");
            //    return;
            //}

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
            //if (!checkRepresentativeInfo())
            //{
            //    Dangol.Message("접수번호를 입력해주세요.");
            //    return;
            //}

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
        }

        private void sbSelectAdd_Click(object sender, EventArgs e)
        {
            if (_representativeId == -1)
            {
                Dangol.Message("입고정보를 가져온 후 추가해 주세요.");
                return;
            }

            //if (leComponentCd.EditValue == null)
            //{
            //    Dangol.Message("품목을 선택하세요");
            //    return;
            //}

            //string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);

            //switch (componentCd)
            //{
            //    case "ADP":
            //        using (dlgCreateADP createAdp = new dlgCreateADP(_representativeType, _representativeCol, _representativeNo, _representativeId, _representativeIdCol, _type, _dtPrintPort))
            //        {
            //            if (createAdp.ShowDialog(this) == DialogResult.OK)
            //            {
            //                sbUpdateComponent_Click(null, null);
            //            }
            //        }
            //        break;
            //    default:
            //        Dangol.Message("품목을 선택하세요");
            //        break;

            //}
        }

        private void sbInputAdd_Click(object sender, EventArgs e)
        {
            if (_representativeId == -1)
            {
                Dangol.Message("입고정보를 가져온 후 추가해 주세요.");
                return;
            }

            //if (leComponentCd.EditValue == null)
            //{
            //    Dangol.Message("품목을 선택하세요");
            //    return;
            //}

            //string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);

            //switch (componentCd)
            //{
            //    case "ADP":
            //        using (dlgSelectComponent createAdp = new dlgSelectComponent(_representativeType, _representativeCol, _representativeNo, _representativeId, _representativeIdCol, _type, _componentCd, _dtPrintPort))
            //        {
            //            if (createAdp.ShowDialog(this) == DialogResult.OK)
            //            {
            //                sbUpdateComponent_Click(null, null);
            //            }
            //        }
            //        break;
            //    default:
            //        Dangol.Message("품목을 선택하세요");
            //        break;

            //}
        }

        private void seInitPrice_EditValueChanged(object sender, EventArgs e)
        {
            sePrice.EditValue = ConvertUtil.ToInt64(seInitPrice.EditValue) - ConvertUtil.ToInt64(seAdjustPrice.EditValue);
        }

        private void seAdjustPrice_EditValueChanged(object sender, EventArgs e)
        {
            sePrice.EditValue = ConvertUtil.ToInt64(seInitPrice.EditValue) - ConvertUtil.ToInt64(seAdjustPrice.EditValue);
        }


        private void sbInput_Click(object sender, EventArgs e)
        {
            string data = teInputBarcode.Text;
            if (data.Length == 12)
            {
                putBarcodeToGrid(data);
                teInputBarcode.Text = "";
            }
            else
            {
                Dangol.Message("재고번호를 확인하세요");
            }
        }
        private void teInputBarcode_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                string data = teInputBarcode.Text;
                if (data.Length == 12)
                {
                    putBarcodeToGrid(data);
                    teInputBarcode.Text = "";
                }
                else
                {
                    Dangol.Message("재고번호를 확인하세요");
                }
            }
        }

        private void putBarcodeToGrid(string barcode)
        {
            if (_listBarcode.Contains(barcode.ToUpper()))
            {
                return;
            }

            DataRow dr = _dtBarcodeList.NewRow();
            dr["INVENTORY_ID"] = -1;
            dr["BARCODE"] = barcode.ToUpper();
            dr["INVENTORY_STATE"] = "-1";
            dr["CHECK"] = false;
            _dtBarcodeList.Rows.Add(dr);

            _listBarcode.Add(barcode.ToUpper());
        }

        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
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
                        _dtBarcodeList.BeginInit();
                        foreach (DataRow row in rows)
                        {
                            if (_listBarcode.Contains(Convert.ToString(row["BARCODE"])))
                                _listBarcode.Remove(Convert.ToString(row["BARCODE"]));
                            row.Delete();
                        }
                        _dtBarcodeList.EndInit();
                    }
                }

            }
        }
        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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

        private void lcgBarcodeList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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

        private void sbSearch_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();
            string componentCd;

            setComponentInitializeData();

            if (DBConnect.getInventoryList(_listBarcode, ref jResult))
            {

                JArray jArrayResult = JArray.Parse(jResult["DATA"].ToString());

                List<string> listCol = null;
                DataRow[] rows;

                foreach (JObject obj in jArrayResult.Children<JObject>())
                {
                    componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                    //listCol = ProjectInfo._dicDeviceFullColumn[componentCd];
                    rows = _dtBarcodeList.Select($"BARCODE = '{obj["BARCODE"]}'");

                    foreach(DataRow row in rows)
                    {
                        row["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        row["COMPONENT_CD"] = componentCd;
                        row["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                    }

                    if (componentCd.Equals("ADP"))
                        usrComponentADP1.addComponent(obj);
                    else if (componentCd.Equals("AIR"))
                        usrComponentAIR1.addComponent(obj);
                    else if (componentCd.Equals("BAT"))
                        usrComponentBAT1.addComponent(obj);
                    else if (componentCd.Equals("CAB"))
                        usrComponentCAB1.addComponent(obj);
                    else if (componentCd.Equals("CAS"))
                        usrComponentCAS1.addComponent(obj);
                    else if (componentCd.Equals("CPU"))
                        usrComponentCPU1.addComponent(obj);
                    else if (componentCd.Equals("FAN"))
                        usrComponentFAN1.addComponent(obj);
                    else if (componentCd.Equals("KEY"))
                        usrComponentKEY1.addComponent(obj);
                    else if (componentCd.Equals("LIC"))
                        usrComponentLIC1.addComponent(obj);
                    else if (componentCd.Equals("MBD"))
                        usrComponentMBD1.addComponent(obj);
                    else if (componentCd.Equals("MEM"))
                        usrComponentMEM1.addComponent(obj);
                    else if (componentCd.Equals("MON"))
                        usrComponentMON1.addComponent(obj);
                    else if (componentCd.Equals("MOU"))
                        usrComponentMOU1.addComponent(obj);
                    else if (componentCd.Equals("PER"))
                        usrComponentPER1.addComponent(obj);
                    else if (componentCd.Equals("PKG"))
                        usrComponentPKG1.addComponent(obj);
                    else if (componentCd.Equals("POW"))
                        usrComponentPOW1.addComponent(obj);
                    else if (componentCd.Equals("STG"))
                        usrComponentSTG1.addComponent(obj);
                    else if (componentCd.Equals("VGA"))
                        usrComponentVGA1.addComponent(obj);
                }
            }

            lcgInventory.BeginUpdate();
            foreach (KeyValuePair<string, LayoutControlItem> item in _diLayoutControl)
            {
                LayoutControlItem control = item.Value;
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //Console.WriteLine("Key: {0}, Value: {1}", kv.Key, kv.Value);
            }

            componentCd = ConvertUtil.ToString(_currentBarcode["COMPONENT_CD"]);
            if(!string.IsNullOrEmpty(componentCd))
                _diLayoutControl[componentCd].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                _diLayoutControl["EMPTY"].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            // lcADP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcgInventory.EndUpdate();
        }

        private void gvBarcodeList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvBarcodeList.RowCount > 0);

            if (isValidRow)
            {
                _currentBarcode = e.Row as DataRowView;

                lcgInventory.BeginUpdate();
                foreach (KeyValuePair<string, LayoutControlItem> item in _diLayoutControl)
                {
                    LayoutControlItem control = item.Value;
                    control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                string componentCd = ConvertUtil.ToString(_currentBarcode["COMPONENT_CD"]);
                if (!string.IsNullOrEmpty(componentCd))
                    _diLayoutControl[componentCd].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    _diLayoutControl["EMPTY"].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcgInventory.EndUpdate();
            }
        }
    }
}