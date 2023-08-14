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
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace WareHousingMaster.view.warehousing
{
    public partial class usrWarehousingProductManagement : DevExpress.XtraEditors.XtraForm
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

        DataTable _dtWarehousingProduct;

        DataTable _dtPallet;

        BindingSource _bsWarehousingProduct;

        short _checkType = 1;
        string _barcode = null;
        string _currentComponentCd = null;
        long _inventoryId = -1;


        DateTime _warehousingDate = new DateTime();
        DataTable _dtPrintPort;
        DataTable _dtPGrade;

        public usrWarehousingProductManagement()
        {
            InitializeComponent();

            _dtWarehousingProduct = new DataTable();

            _dtWarehousingProduct.Columns.Add(new DataColumn("NO", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            //_dtWarehousingProduct.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("INIT_PRICE", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MBD_MANUFACT", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MBD_MODEL_NAME", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("CPU_MODEL_NAME", typeof(string)));

            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_MANUFACT1", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_CAPACITY1", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_MANUFACT2", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_CAPACITY2", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_MANUFACT3", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_CAPACITY3", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_MANUFACT4", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_CAPACITY4", typeof(string)));

            _dtWarehousingProduct.Columns.Add(new DataColumn("VGA_MODEL_NAME1", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("VGA_MODEL_NAME2", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_TYPE1", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_CAPACITY1", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_TYPE2", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_CAPACITY2", typeof(string)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MON_SIZE", typeof(string)));

            _dtWarehousingProduct.Columns.Add(new DataColumn("ADD_PRICE", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_PRICE1", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_PRICE2", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_PRICE3", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_PRICE4", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_PRICE1", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_PRICE2", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("LICENCE", typeof(string)));

            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_INVENTORY_ID1", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_INVENTORY_ID2", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_INVENTORY_ID3", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("MEM_INVENTORY_ID4", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_INVENTORY_ID1", typeof(long)));
            _dtWarehousingProduct.Columns.Add(new DataColumn("STG_INVENTORY_ID2", typeof(long)));


            _bsWarehousingProduct = new BindingSource();
            _bsWarehousingProduct.DataSource = _dtWarehousingProduct;

            
            //tnReceipt.EditValue = "LT201130001";
            //lcComponent.Text = _componentCd;

            if(ProjectInfo._dicConsignedInfoDetail == null)
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

            teBarcode.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "BARCODE", false, DataSourceUpdateMode.Never));
            //teComponent.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "COMPONENT", false, DataSourceUpdateMode.Never));
            //leLocation.DataBindings.Add(new Binding("EditValue", _bsWarehousingInvnetory, "WAREHOUSE", false, DataSourceUpdateMode.Never));
            //lePallet.DataBindings.Add(new Binding("EditValue", _bsWarehousingInvnetory, "PALLET", false, DataSourceUpdateMode.Never));
            //teUserName.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "USER_ID", false, DataSourceUpdateMode.Never));

            //leInventoryCat.DataBindings.Add(new Binding("EditValue", _bsWarehousingInvnetory, "INVENTORY_CAT", false, DataSourceUpdateMode.Never));
            seInitPrice.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "INIT_PRICE", false, DataSourceUpdateMode.Never));
            //seAdjustPrice.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "ADJUST_PRICE", false, DataSourceUpdateMode.Never));
            //meAdjustDes.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "ADJUST_DES", false, DataSourceUpdateMode.Never));
            sePrice.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "PRICE", false, DataSourceUpdateMode.Never));
            //seReleasePrice.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "RELEASE_PRICE", false, DataSourceUpdateMode.Never));
            //meDes.DataBindings.Add(new Binding("Text", _bsWarehousingInvnetory, "DES", false, DataSourceUpdateMode.Never));

            seMemPrice1.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "MEM_PRICE1", false, DataSourceUpdateMode.Never));
            seMemPrice2.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "MEM_PRICE2", false, DataSourceUpdateMode.Never));
            seMemPrice3.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "MEM_PRICE3", false, DataSourceUpdateMode.Never));
            seMemPrice4.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "MEM_PRICE4", false, DataSourceUpdateMode.Never));
            seStgPrice1.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "STG_PRICE1", false, DataSourceUpdateMode.Never));
            seStgPrice2.DataBindings.Add(new Binding("Text", _bsWarehousingProduct, "STG_PRICE2", false, DataSourceUpdateMode.Never));
            leLicence.DataBindings.Add(new Binding("EditValue", _bsWarehousingProduct, "LICENCE", false, DataSourceUpdateMode.Never));
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

            DataTable dtProductGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductGrade, dtProductGrade, "KEY", "VALUE");

            //_dtPrintPort = new DataTable();

            //_dtPrintPort.Columns.Add(new DataColumn("KEY", typeof(string)));
            //_dtPrintPort.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //for (int i = ProjectInfo._startPort; i < ProjectInfo._endPort; i++)
            //{
            //    DataRow dr = _dtPrintPort.NewRow();

            //    dr["KEY"] = i;
            //    dr["VALUE"] = i;
            //    _dtPrintPort.Rows.Add(dr);
            //}

            //Util.LookupEditHelper(lePrintPort, _dtPrintPort, "KEY", "VALUE");

            ////DataTable dtDeviceType = new DataTable();

            ////dtDeviceType.Columns.Add(new DataColumn("KEY", typeof(int)));
            ////dtDeviceType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            ////for (int i = 0; i < ProjectInfo._arrTypeNm.Length; i++)
            ////{
            ////    DataRow dr = dtDeviceType.NewRow();

            ////    dr["KEY"] = i;
            ////    dr["VALUE"] = ProjectInfo._arrTypeNm[i];
            ////    dtDeviceType.Rows.Add(dr);
            ////}

            ////Util.LookupEditHelper(leProductType, dtDeviceType, "KEY", "VALUE");

            //DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");

            //Util.insertRowonTop(dtLocation, "-1", " 없음");

            //Util.LookupEditHelper(leLocation, dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(rileWarehouse, dtLocation, "KEY", "VALUE");



            //_dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");

            //Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
            //dicPalletDefault.Add("WAREHOUSE_ID", "-1");
            //dicPalletDefault.Add("PALLET_ID", "");
            //dicPalletDefault.Add("PALLET_NM", "선택안합");

            //Util.insertRowonTop(_dtPallet, dicPalletDefault);

            //_bsPallet.DataSource = _dtPallet;

            ////DataTable dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            //Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");

            //DataTable dtComponentCd = Util.getCodeList("CD0101", "KEY", "VALUE");
            //Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            //DataTable dtInvnetoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            //Util.LookupEditHelper(rileInventoryState, dtInvnetoryState, "KEY", "VALUE");

            //DataTable dtInvnetoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            //Util.LookupEditHelper(rileInventoryCat, dtInvnetoryCat, "KEY", "VALUE");
            //Util.LookupEditHelper(leInventoryCat, dtInvnetoryCat, "KEY", "VALUE");


            ////DataTable dtComponentCd2 = new DataTable();

            ////dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            ////dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            ////for(int i = 0; i < ProjectInfo._componetCd.Length; i++ )
            ////{ 
            ////    DataRow dr = dtComponentCd.NewRow();

            ////    dr["KEY"] = ProjectInfo._componetCd[i];
            ////    dr["VALUE"] = ProjectInfo._componetNm[i];
            ////    dtComponentCd.Rows.Add(dr);
            ////}

            //Util.LookupEditHelper(leComponentCd, dtComponentCd, "KEY", "VALUE");

            //_dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            //lePrintPort.EditValue = ProjectInfo._printerPort;
            //leLocation.EditValue = ProjectInfo._location;
            //leProductType.EditValue = ProjectInfo._type;
            //teUserName.EditValue = ProjectInfo._userName;

        }

       

        private void setGridControl()
        {
            //gcWarehousingComponent.DataSource = null;
            //gcWarehousingComponent.DataSource = _bsWarehousingComponent;

            gcWarehousingInvnetory.DataSource = null;
            gcWarehousingInvnetory.DataSource = _bsWarehousingProduct;
        }

        private void leLocation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            changePallet(e.NewValue);
        }

        private void changePallet(object palletId)
        {
            //_bsPallet.Filter = $"WAREHOUSE_ID = '{palletId}' OR WAREHOUSE_ID = '-1'";
            //lePallet.ItemIndex = 0;
        }


        private void leComponentCd_EditValueChanged(object sender, EventArgs e)
        {
            _componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
            if(!string.IsNullOrEmpty(_componentCd))
                setGridControl();
        }



        private void gvWarehousingInvnetory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehousingInvnetory.RowCount > 0);

            if (isValidRow)
            {
                _currentInventory = e.Row as DataRowView;
                //changePallet(_currentInventory["WAREHOUSE"]);
            }
        }


        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(9))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcWarehousingInvnetory.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }

        }

        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("선택하신 제품의 가격을 수정하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jPartInfo = new JObject();
                    JObject jResult = new JObject();

                    long initPrice = ConvertUtil.ToInt64(seInitPrice.EditValue);
                    long price = ConvertUtil.ToInt64(sePrice.EditValue);


                    long memInventoryId1 = ConvertUtil.ToInt64(_currentInventory["MEM_INVENTORY_ID1"]);
                    long memInventoryId2 = ConvertUtil.ToInt64(_currentInventory["MEM_INVENTORY_ID2"]);
                    long memInventoryId3 = ConvertUtil.ToInt64(_currentInventory["MEM_INVENTORY_ID3"]);
                    long memInventoryId4 = ConvertUtil.ToInt64(_currentInventory["MEM_INVENTORY_ID4"]);
                    long stgInventoryId1 = ConvertUtil.ToInt64(_currentInventory["STG_INVENTORY_ID1"]);
                    long stgInventoryId2 = ConvertUtil.ToInt64(_currentInventory["STG_INVENTORY_ID2"]);

                    long memPrice1 = ConvertUtil.ToInt64(seMemPrice1.EditValue);
                    long memPrice2 = ConvertUtil.ToInt64(seMemPrice2.EditValue);
                    long memPrice3 = ConvertUtil.ToInt64(seMemPrice3.EditValue);
                    long memPrice4 = ConvertUtil.ToInt64(seMemPrice4.EditValue);
                    long stgPrice1 = ConvertUtil.ToInt64(seStgPrice1.EditValue);
                    long stgPrice2 = ConvertUtil.ToInt64(seStgPrice2.EditValue);


                    jPartInfo.Add("INVENTORY_ID", ConvertUtil.ToInt64(_currentInventory["INVENTORY_ID"]));
                    jPartInfo.Add("INIT_PRICE", initPrice);
                    jPartInfo.Add("PRICE", price);

                    jPartInfo.Add("MEM_INVENTORY_ID1", memInventoryId1);
                    jPartInfo.Add("MEM_INVENTORY_ID2", memInventoryId2);
                    jPartInfo.Add("MEM_INVENTORY_ID3", memInventoryId3);
                    jPartInfo.Add("MEM_INVENTORY_ID4", memInventoryId4);
                    jPartInfo.Add("STG_INVENTORY_ID1", stgInventoryId1);
                    jPartInfo.Add("STG_INVENTORY_ID2", stgInventoryId2);

                    jPartInfo.Add("MEM_PRICE1", memPrice1);
                    jPartInfo.Add("MEM_PRICE2", memPrice2);
                    jPartInfo.Add("MEM_PRICE3", memPrice3);
                    jPartInfo.Add("MEM_PRICE4", memPrice4);
                    jPartInfo.Add("STG_PRICE1", stgPrice1);
                    jPartInfo.Add("STG_PRICE2", stgPrice2);

                    jPartInfo.Add("LICENCE", ConvertUtil.ToString(leLicence.EditValue));

                    if (DBConnect.updateProductPrice(jPartInfo, ref jResult))
                    {
                        gvWarehousingInvnetory.BeginDataUpdate();
                        _currentInventory["INIT_PRICE"] = initPrice;
                        _currentInventory["PRICE"] = price;
                        long addPrice = 0;

                        if (memInventoryId1 > 0) {
                            _currentInventory["MEM_PRICE1"] = memPrice1;
                            addPrice += memPrice1;
                        }
                        if (memInventoryId2 > 0) {
                            _currentInventory["MEM_PRICE2"] = memPrice2;
                            addPrice += memPrice2;
                        }
                        if (memInventoryId3 > 0)
                        {
                            _currentInventory["MEM_PRICE3"] = memPrice3;
                            addPrice += memPrice3;
                        }
                        if (memInventoryId4 > 0)
                        {
                            _currentInventory["MEM_PRICE4"] = memPrice4;
                            addPrice += memPrice4;
                        }
                        if (stgInventoryId1 > 0) {
                            _currentInventory["STG_PRICE1"] = stgPrice1;
                            addPrice += stgPrice1;
                        }
                        if (stgInventoryId2 > 0) {
                            _currentInventory["STG_PRICE2"] = stgPrice2;
                            addPrice += stgPrice2;
                        }

                        _currentInventory["ADD_PRICE"] = addPrice;
                        _currentInventory["TOTAL_PRICE"] = price + addPrice;
                        gvWarehousingInvnetory.EndDataUpdate();
                        Dangol.Message(jResult["MSG"]);
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                using (DlgImportProductPrice dlgReceip = new DlgImportProductPrice())
                {
                    dlgReceip.ShowDialog();

                    if (dlgReceip._isSuccess)
                    {

                        DataRow[] rows;
                        for (int i = 0; i < dlgReceip.listBarcode.Count; i++)
                        {
                            rows = _dtWarehousingProduct.Select($"BARCODE = '{dlgReceip.listBarcode[i]}'");

                            if (rows.Length > 0)
                            {
                                rows[0]["PRICE"] = dlgReceip.listPrice[i];
                            }
                        }
                    }
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
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            JObject jResult = new JObject();

            if (DBConnect.getWarehousingProduct(_representativeNo, ref jResult))
            {
                _dtWarehousingProduct.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JObject jData = (JObject)jResult["DATA"];
                    _representativeId = ConvertUtil.ToInt64(jData["WAREHOUSING_ID"]);
                    teWarehousingDt.Text = ConvertUtil.ToDateTimeNull(jData["WAREHOUSING_DT"]);
                    teCustomer.Text = ConvertUtil.ToString(jData["CUSTOMER_ID"]);

                    JArray jArray = JArray.Parse(jResult["DATALIST"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehousingProduct.NewRow();

                        long totalPrice = ConvertUtil.ToInt64(obj["PRICE"]) + ConvertUtil.ToInt64(obj["ADD_PRICE"]);

                        dr["NO"] = index++;
                        //dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];
                        dr["INIT_PRICE"] = obj["INIT_PRICE"];
                        dr["PRICE"] = obj["PRICE"];

                        dr["MBD_MANUFACT"] = obj["MBD_MANUFACT"];
                        dr["MBD_MODEL_NAME"] = obj["MBD_MODEL_NAME"];
                        dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                        dr["CPU_MODEL_NAME"] = obj["CPU_MODEL_NAME"];

                        dr["MEM_MANUFACT1"] = obj["MEM_MANUFACT1"];
                        dr["MEM_CAPACITY1"] = obj["MEM_CAPACITY1"];
                        dr["MEM_MANUFACT2"] = obj["MEM_MANUFACT2"];
                        dr["MEM_CAPACITY2"] = obj["MEM_CAPACITY2"];
                        dr["MEM_MANUFACT3"] = obj["MEM_MANUFACT3"];
                        dr["MEM_CAPACITY3"] = obj["MEM_CAPACITY3"];
                        dr["MEM_MANUFACT4"] = obj["MEM_MANUFACT4"];
                        dr["MEM_CAPACITY4"] = obj["MEM_CAPACITY4"];

                        dr["VGA_MODEL_NAME1"] = obj["VGA_MODEL_NAME1"];
                        dr["VGA_MODEL_NAME2"] = obj["VGA_MODEL_NAME2"];
                        dr["STG_TYPE1"] = obj["STG_TYPE1"];
                        dr["STG_CAPACITY1"] = obj["STG_CAPACITY1"];
                        dr["STG_TYPE2"] = obj["STG_TYPE2"];
                        dr["STG_CAPACITY2"] = obj["STG_CAPACITY2"];
                        dr["MON_SIZE"] = obj["MON_SIZE"];

                        dr["ADD_PRICE"] = obj["ADD_PRICE"];
                        dr["TOTAL_PRICE"] = totalPrice;
                        dr["MEM_PRICE1"] = ConvertUtil.ToInt64(obj["MEM_PRICE1"]);
                        dr["MEM_PRICE2"] = ConvertUtil.ToInt64(obj["MEM_PRICE2"]);
                        dr["MEM_PRICE3"] = ConvertUtil.ToInt64(obj["MEM_PRICE3"]);
                        dr["MEM_PRICE4"] = ConvertUtil.ToInt64(obj["MEM_PRICE4"]);
                        dr["STG_PRICE1"] = ConvertUtil.ToInt64(obj["STG_PRICE1"]);
                        dr["STG_PRICE2"] = ConvertUtil.ToInt64(obj["STG_PRICE2"]);
                        dr["LICENCE"] = obj["LICENCE"];

                        dr["MEM_INVENTORY_ID1"] = ConvertUtil.ToInt64(obj["MEM_INVENTORY_ID1"]);
                        dr["MEM_INVENTORY_ID2"] = ConvertUtil.ToInt64(obj["MEM_INVENTORY_ID2"]);
                        dr["MEM_INVENTORY_ID3"] = ConvertUtil.ToInt64(obj["MEM_INVENTORY_ID3"]);
                        dr["MEM_INVENTORY_ID4"] = ConvertUtil.ToInt64(obj["MEM_INVENTORY_ID4"]);
                        dr["STG_INVENTORY_ID1"] = ConvertUtil.ToInt64(obj["STG_INVENTORY_ID1"]);
                        dr["STG_INVENTORY_ID2"] = ConvertUtil.ToInt64(obj["STG_INVENTORY_ID2"]);                

                        _dtWarehousingProduct.Rows.Add(dr);
                    }
                }

                return;
            }
            else
            {
                return;
            }
        }


        //private void AddInventory(int id, string componentCd, DataRow[] rowDeviceInfo)
        //{
        //    JObject jResult = new JObject();

        //    if (DBConnect.InsertInventory(_representativeType, _representativeNo, _representativeCol, componentCd, "-1", -1, rowDeviceInfo[0], ref jResult))
        //    {
        //        DataRow[] rows = ProjectInfo._dtConsignedInfo.Select($"ID = {id}");
        //        foreach (DataRow dr in rows)
        //        {
        //            dr.BeginEdit();
        //            foreach (string col in ProjectInfo._listKeyColumn)
        //                dr[col] = jResult[col];

        //            for (int i = 1; i < 4; i++)
        //                dr[$"REP{i}"] = dr[$"DATA{i}"];

        //            dr["PRICE"] = 0;
        //            dr["RELEASE_PRICE"] = 0;
        //            dr["INVENTORY_CNT"] = 0;
        //            dr["INVENTORY_YN"] = true;
        //            dr["PRODUCT_YN"] = false;

        //            dr.EndEdit();
        //        }

        //        DataTable dtDeviceInfoFromDB = ProjectInfo._dicConsignedInfoDetail[id];
        //        foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
        //        {
        //            dr.BeginEdit();
        //            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
        //            dr.EndEdit();
        //        }

        //        if (componentCd.Equals("MBD"))
        //        {
        //            JObject jResultos = new JObject();
        //            DBConnect.osInfoCheck(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), rowDeviceInfo[0], ref jResultos);
        //        }

        //        DBConnect.updateInventoryState(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), "R", "G", "N", ref jResult);
        //    }
        //}

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

            //if (DBConnect.printProduct(_representativeType, _representativeNo, _representativeCol, dicInventoryId, lePrintPort.EditValue.ToString(), ref jResult))
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}
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

            //if (DBConnect.printInventoryInfo(_representativeType, _representativeNo, _representativeCol, _barcode, lePrintPort.EditValue.ToString(), ref jResult))
            //{
            //    Dangol.Message("부품정보가 출력되었습니다.");
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}
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

            //ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

            int checkValue = 1;
            string checkText = "1";

            //if (checkValue == 1)
            //{
            //    Dangol.Message("PC는 개별 부품 체크만 가능합니다.");
            //    return;
            //}

            //if (ProjectInfo._type != checkValue)
            //{
            //    if (Dangol.MessageYN($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{ProjectInfo._typeNm}, 선택:{checkText})", $"{checkText} 검수 체크") == DialogResult.No)
            //        return;
            //}

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
                    //using (DlgNtb2ndEditionCheck ntbCheck = new DlgNtb2ndEditionCheck(ProjectInfo._dicNtbCheckRelease, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, _dtPrintPort))
                    //{
                    //    if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                    //    {
                    //        ProjectInfo._caseDestroyDescriptionRelease = ntbCheck._caseDestroyDescription;
                    //        ProjectInfo._batteryRemainRelease = ntbCheck._batteryRemain;
                    //        DBConnect.insertNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckRelease, listInventoryId, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease);

                    //        if (ntbCheck._isPrint)
                    //            DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckRelease, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, ProjectInfo._printerPort);

                    //    }
                    //}
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

            Dictionary<string, int> dicCheckInfo = ProjectInfo._dicPartCheckRelease[_inventoryId];

            if (_currentComponentCd.Equals("MON"))
            {
                //using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(dicCheckInfo, _dtPrintPort))
                //{
                //    if (monitorCheck.ShowDialog(this) == DialogResult.OK)
                //    {
                //        DBConnect.insertMonitorCheck(_barcode, _checkType, dicCheckInfo);

                //        if (monitorCheck._isPrint)
                //            DBConnect.printMonitorCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
                //    }
                //}
            }
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
                //_currentRow.BeginEdit();
                //DataRow[] rowBarcode = ProjectInfo._dtDeviceInfo.Select($"BARCODE = '{barcode}'");

                //if (rowBarcode.Length > 0)
                //{ 
                //    Dangol.Message("이미 현재 제품에 존재하는 부품입니다.");
                //    return;
                //}

                //JObject jData = (JObject)jResult["DATA"];

                //_currentRow["INVENTORY_ID"] = dtComponentInfo.Rows[0]["INVENTORY_ID"] = jData["INVENTORY_ID"];
                //_currentRow["BARCODE"] = dtComponentInfo.Rows[0]["BARCODE"] = jData["BARCODE"];
                //_currentRow["COMPONENT_ID"] = dtComponentInfo.Rows[0]["COMPONENT_ID"] = jData["COMPONENT_ID"];
                //_currentRow["COMPONENT"] = dtComponentInfo.Rows[0]["COMPONENT"] = jData["COMPONENT"];

                //_currentRow["INVENTORY_YN"] = true;

                //DataTable dtConsignedInfoFromDB = ProjectInfo._dicConsignedInfoDetail[_id];
                //DataRow[] detailRows;

                //string componentCd = jData["COMPONENT_CD"].ToString();
                //List<string> listFullColum = ProjectInfo._dicDeviceFullColumn[componentCd];

                //foreach (string col in listFullColum)
                //{
                //    detailRows = dtConsignedInfoFromDB.Select($"SPEC_NM = '{col}'");
                //    if (detailRows.Length > 0)
                //        detailRows[0]["MATCHING_INFO"] = jData[col];
                //}

                //gcInventoryDetail.DataSource = null;
                //bsDetail.DataSource = dtConsignedInfoFromDB;
                //gcInventoryDetail.DataSource = bsDetail;
                    

                //_barcode = ConvertUtil.ToString(jData["BARCODE"]);
                //_inventoryId = ConvertUtil.ToInt64(jData["INVENTORY_ID"]);
                //_component = ConvertUtil.ToString(jData["COMPONENT"]);
                //_componentId = ConvertUtil.ToInt64(jData["COMPONENT_ID"]);

                //getCheckInfo(_inventoryId, _currentComponentCd);

                //setBarcodeButton(true, true, false);

                //if (_currentComponentCd.Equals("MBD"))
                //    Util.checkProductState(_inventoryId, _barcode);
                //else
                //{
                //    if (ProjectInfo._dicProductList.ContainsKey(_inventoryId))
                //        _currentRow["PRODUCT_YN"] = true;
                //}

                //if (ProjectInfo._listReleaseList != null)
                //    if (ProjectInfo._listReleaseList.Contains(_inventoryId))
                //    {
                //        _currentRow["RELEASE_YN"] = true;
                //        _currentRow["RELEASE_RESULT"] = "정상 등록";
                //    }
                //_currentRow.EndEdit();

                //Dangol.Message("부품 정보를 변경하였습니다.");
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            
        }

        private void setBarcodeButton(bool teBarcodeFlag, bool sbClearFlag, bool sbGetDeviceInfoFlag)
        {
            teBarcode.Properties.ReadOnly = teBarcodeFlag;
            
        }

        private void sbGetInventoryInfo_Click(object sender, EventArgs e)
        {

        }

        private void leProductType_EditValueChanged(object sender, EventArgs e)
        {
        //    ProjectInfo._type = ConvertUtil.ToInt32(leProductType.EditValue);
        //    ProjectInfo._typeNm = leProductType.Text;
        }

        private void gvInventory_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (ConvertUtil.ToInt32(gvInventory.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }

        private void ribeAsign_Click(object sender, EventArgs e)
        {
            //if(ConvertUtil.ToInt64(_currentRow["PROXY_PART_ID"]) > 0)
            //{

            //}
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

            if (leComponentCd.EditValue == null)
            {
                Dangol.Message("품목을 선택하세요");
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
                            sbUpdateComponent_Click(null, null);
                        }
                    }
                    break;
                default:
                    Dangol.Message("품목을 선택하세요");
                    break;

            }
        }

        private void sbInputAdd_Click(object sender, EventArgs e)
        {
            if (_representativeId == -1)
            {
                Dangol.Message("입고정보를 가져온 후 추가해 주세요.");
                return;
            }

            if (leComponentCd.EditValue == null)
            {
                Dangol.Message("품목을 선택하세요");
                return;
            }

            string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);

            switch (componentCd)
            {
                case "ADP":
                    using (dlgSelectComponent createAdp = new dlgSelectComponent(_representativeType, _representativeCol, _representativeNo, _representativeId, _representativeIdCol, _type, _componentCd, _dtPrintPort))
                    {
                        if (createAdp.ShowDialog(this) == DialogResult.OK)
                        {
                            sbUpdateComponent_Click(null, null);
                        }
                    }
                    break;
                default:
                    Dangol.Message("품목을 선택하세요");
                    break;

            }
        }

        private void seInitPrice_EditValueChanged(object sender, EventArgs e)
        {
            //sePrice.EditValue = ConvertUtil.ToInt64(seInitPrice.EditValue) - ConvertUtil.ToInt64(seAdjustPrice.EditValue);
        }

        private void seAdjustPrice_EditValueChanged(object sender, EventArgs e)
        {
            //sePrice.EditValue = ConvertUtil.ToInt64(seInitPrice.EditValue) - ConvertUtil.ToInt64(seAdjustPrice.EditValue);
        }

        private void gvWarehousingInvnetory_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "MEM_PRICE1")
            {
                string model = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MEM_CAPACITY1"]);
                object price = View.GetRowCellValue(e.RowHandle, View.Columns["MEM_PRICE1"]);
                if (!string.IsNullOrEmpty(model) && ConvertUtil.ToInt64(price) == 0)
                {                  
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
            else if (e.Column.FieldName == "MEM_PRICE2")
            {
                string model = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MEM_CAPACITY2"]);
                object price = View.GetRowCellValue(e.RowHandle, View.Columns["MEM_PRICE2"]);
                if (!string.IsNullOrEmpty(model) && ConvertUtil.ToInt64(price) == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
            else if (e.Column.FieldName == "MEM_PRICE3")
            {
                string model = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MEM_CAPACITY3"]);
                object price = View.GetRowCellValue(e.RowHandle, View.Columns["MEM_PRICE3"]);
                if (!string.IsNullOrEmpty(model) && ConvertUtil.ToInt64(price) == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
            else if (e.Column.FieldName == "MEM_PRICE4")
            {
                string model = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MEM_CAPACITY4"]);
                object price = View.GetRowCellValue(e.RowHandle, View.Columns["MEM_PRICE4"]);
                if (!string.IsNullOrEmpty(model) && ConvertUtil.ToInt64(price) == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
            else if (e.Column.FieldName == "STG_PRICE1")
            {
                string model = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STG_CAPACITY1"]);
                object price = View.GetRowCellValue(e.RowHandle, View.Columns["STG_PRICE1"]);
                if (!string.IsNullOrEmpty(model) && ConvertUtil.ToInt64(price) == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
            else if (e.Column.FieldName == "STG_PRICE2")
            {
                string model = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STG_CAPACITY2"]);
                object price = View.GetRowCellValue(e.RowHandle, View.Columns["STG_PRICE2"]);
                if (!string.IsNullOrEmpty(model) && ConvertUtil.ToInt64(price) == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
        }
    }
}