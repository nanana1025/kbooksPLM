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
using Newtonsoft.Json;

namespace WareHousingMaster.view.warehousingManagement
{
    public partial class usrWarehouseMovementStatus : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtWarehouMovement;
        DataTable _dtWarehouMovementDetail;
        BindingSource bsWarehouseMovementList;
        BindingSource bsWarehouseMovementListDetail;
        BindingSource _bsPallet;

        DataTable _dtLocation;
        DataTable _dtPallet;
        DataTable _dtPalletDetail;
        DataTable _dtPalletRelease;
        DataTable _dtPalletWarehousing;
        DataTable _dtBasket;
        DataRowView _currentRow;
        DataRowView _currentRowDetail;
        List<string> _listBarcode;
        List<long> _listInventoryId;
        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        long _warehouseMovementId;
        long _inventoryId;

        bool isSearch = false;
        bool isInit = true;


        public usrWarehouseMovementStatus()
        {
            InitializeComponent();

            long _warehouseMovementId = -1;



            _dtWarehouMovement = new DataTable();
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSE_MOVEMENT_ID", typeof(long)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSE_MOVEMENT", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSE_MOVEMENT_STATE", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("RELEASE_WAREHOUSE_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSING_WAREHOUSE_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("RELEASE_PALLET_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSING_PALLET_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("REGIST_DT", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtWarehouMovement.Columns.Add(new DataColumn("PRODUCT_CNT", typeof(int)));

            _dtWarehouMovementDetail = new DataTable();
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("WAREHOUSE_MOVEMENT_ID", typeof(long)));
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("PALLET", typeof(string)));
            _dtWarehouMovementDetail.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            bsWarehouseMovementList = new BindingSource();
            bsWarehouseMovementListDetail = new BindingSource();
            _bsPallet = new BindingSource();

            _listBarcode = new List<string>();
            _listInventoryId = new List<long>();
            //_dicWarehouMovementList = new Dictionary<long, Dictionary<long, string>>();

        }

        private void usrWarehouseMovementStatus_Load(object sender, EventArgs e)
        {
            setInfoBox();
            JObject jResult = new JObject();
            getWarehouseMovementList("", "", "E,R,W", "", "", "", "", "", "", "", ref jResult);
            setIInitData();

            //teInputBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            //teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
        }
        //private void usrComponentKeep_Load(object sender, EventArgs e)
        //{
        //    setInfoBox();
        //    setIInitData();

        //    //teInputBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
        //    //teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
        //}
        private void usrWarehouseMovementStatus_Enter(object sender, EventArgs e)
        {
            if (!isInit)
            {
                int rowhandle = gvWarehouseMovementList.FocusedRowHandle;
                int topRowIndex = gvWarehouseMovementList.TopRowIndex;
                if (isSearch)
                {
                    string dtFrom = "";
                    string dtTo = "";
                    if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
                        dtFrom = $"{deDtFrom.Text} 00:00:00";

                    if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
                        dtTo = $"{deDtTo.Text} 23:59:59";

                    JObject jResult = new JObject();
                    getWarehouseMovementList(teRegistNo.EditValue,
                        teBarcode.EditValue,
                        ConvertUtil.ToString(leWarehouseMovementStatus.EditValue),
                        dtFrom, dtTo, deRegistDt.Text, deReleaseDt.Text, deWarehousingDt.Text, leReleaseWarehouseNo.EditValue, leWarehousingWarehouseNo.EditValue, ref jResult);
                }
                else
                {
                    JObject jResult = new JObject();
                    getWarehouseMovementList("", "", "E,R,W", "", "", "", "", "", "", "", ref jResult);
                }
                gvWarehouseMovementList.FocusedRowHandle = rowhandle;
                gvWarehouseMovementList.TopRowIndex = topRowIndex;
            }
            else
            {
                isInit = false;
            }
        }

        private void setInfoBox()
        {

            _dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
            Util.insertRowonTop(_dtLocation, "", "선택안합");

            Util.LookupEditHelper(leReleaseWarehouseNo, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leWarehousingWarehouseNo, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leReleaseWarehouseNoSelect, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leWarehousingWarehouseNoSelect, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(rileReleaseWarehouse, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehousingWarehouse, _dtLocation, "KEY", "VALUE");


            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");

            Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
            dicPalletDefault.Add("WAREHOUSE_ID", "-1");
            dicPalletDefault.Add("PALLET_ID", "");
            dicPalletDefault.Add("PALLET_NM", "선택안합");

            Util.insertRowonTop(_dtPallet, dicPalletDefault);

            _dtPalletRelease = _dtPallet.Copy();
            _dtPalletWarehousing = _dtPallet.Copy();
            _dtPalletDetail = _dtPallet.Copy();
            

            Util.LookupEditHelper(leReleasePalletNoSelect, _dtPalletRelease, "PALLET_ID", "PALLET_NM");
            Util.LookupEditHelper(leWarehousingPalletNoSelect, _dtPalletWarehousing, "PALLET_ID", "PALLET_NM");

            _bsPallet.DataSource = _dtPalletDetail;
            Util.LookupEditHelper(rileBasket, _bsPallet, "PALLET_ID", "PALLET_NM");


            DataTable dtWarehouseMovementState = Util.getCodeList("CD1201", "KEY", "VALUE");
            Util.insertRowonTop(dtWarehouseMovementState, "", "선택안합");
            Util.LookupEditHelper(leWarehouseMovementStatus, dtWarehouseMovementState, "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehouseMovementState, dtWarehouseMovementState, "KEY", "VALUE");
            

            DataTable dtWarehouseInventoryState = Util.getCodeList("CD1202", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState, dtWarehouseInventoryState, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            gcWarehouseMovementList.DataSource = null;
            bsWarehouseMovementList.DataSource = _dtWarehouMovement;
            gcWarehouseMovementList.DataSource = bsWarehouseMovementList;

            gcWarehouseMovementListDetail.DataSource = null;
            bsWarehouseMovementListDetail.DataSource = _dtWarehouMovementDetail;
            gcWarehouseMovementListDetail.DataSource = bsWarehouseMovementListDetail;

            teRegistNoSelect.DataBindings.Add(new Binding("Text", bsWarehouseMovementList, "WAREHOUSE_MOVEMENT", false, DataSourceUpdateMode.Never));
            leReleaseWarehouseNoSelect.DataBindings.Add(new Binding("EditValue", bsWarehouseMovementList, "RELEASE_WAREHOUSE_ID", false, DataSourceUpdateMode.Never));
            leWarehousingWarehouseNoSelect.DataBindings.Add(new Binding("EditValue", bsWarehouseMovementList, "WAREHOUSING_WAREHOUSE_ID", false, DataSourceUpdateMode.Never));
            leReleasePalletNoSelect.DataBindings.Add(new Binding("EditValue", bsWarehouseMovementList, "RELEASE_PALLET_ID", false, DataSourceUpdateMode.Never));
            leWarehousingPalletNoSelect.DataBindings.Add(new Binding("EditValue", bsWarehouseMovementList, "WAREHOUSING_PALLET_ID", false, DataSourceUpdateMode.Never));
        }

        private void gvWarehouseMovementList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehouseMovementList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _warehouseMovementId = ConvertUtil.ToInt64(_currentRow["WAREHOUSE_MOVEMENT_ID"]);
                setWarehousingMovementListDetail(_warehouseMovementId);

                if (_currentRow["WAREHOUSE_MOVEMENT_STATE"].Equals("R"))
                    lgcWarehouseMovement.CustomHeaderButtons[0].Properties.Enabled = true;
                else
                    lgcWarehouseMovement.CustomHeaderButtons[0].Properties.Enabled = false;

                _bsPallet.Filter = $"WAREHOUSE_ID = '{_currentRow["WAREHOUSING_WAREHOUSE_ID"]}'";

                if (_currentRow["WAREHOUSE_MOVEMENT_STATE"].Equals("W"))
                {
                    lgcWarehouseMovement.CustomHeaderButtons[0].Properties.Enabled = false;
                    lgcWarehouseMovement.CustomHeaderButtons[1].Properties.Enabled = true;

                    lcgStatus.CustomHeaderButtons[0].Properties.Enabled = true;
                    lcgStatus.CustomHeaderButtons[1].Properties.Enabled = true;
                    gcBasket.OptionsColumn.AllowEdit = true;
                }
                else if (_currentRow["WAREHOUSE_MOVEMENT_STATE"].Equals("R"))
                {
                    lgcWarehouseMovement.CustomHeaderButtons[0].Properties.Enabled = true;
                    lgcWarehouseMovement.CustomHeaderButtons[1].Properties.Enabled = false;

                    lcgStatus.CustomHeaderButtons[0].Properties.Enabled = false;
                    lcgStatus.CustomHeaderButtons[1].Properties.Enabled = false;
                    gcBasket.OptionsColumn.AllowEdit = false;
                }
                else if (_currentRow["WAREHOUSE_MOVEMENT_STATE"].Equals("E"))
                {
                    lgcWarehouseMovement.CustomHeaderButtons[0].Properties.Enabled = false;
                    lgcWarehouseMovement.CustomHeaderButtons[1].Properties.Enabled = false;

                    lcgStatus.CustomHeaderButtons[0].Properties.Enabled = false;
                    lcgStatus.CustomHeaderButtons[1].Properties.Enabled = false;
                    gcBasket.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    lcgStatus.CustomHeaderButtons[0].Properties.Enabled = false;
                    lcgStatus.CustomHeaderButtons[1].Properties.Enabled = false;
                    gcBasket.OptionsColumn.AllowEdit = false;
                }
            }
            else 
            {
                lgcWarehouseMovement.CustomHeaderButtons[0].Properties.Enabled = false;
                lcgStatus.CustomHeaderButtons[0].Properties.Enabled = false;
                lcgStatus.CustomHeaderButtons[1].Properties.Enabled = false;
                gcBasket.OptionsColumn.AllowEdit = false;
            }
        }

        private void gvWarehouseMovementListDetail_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehouseMovementListDetail.RowCount > 0);

            if (isValidRow)
            {

                _currentRowDetail = e.Row as DataRowView;
                _inventoryId = ConvertUtil.ToInt64(_currentRowDetail["INVENTORY_ID"]);
            }
        }

        private void lcgStatus_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (dlgSetPallet dlgBasketSet = new dlgSetPallet(_warehouseMovementId, _currentRow, _dtLocation, _dtPallet))
                {
                    if (dlgBasketSet.ShowDialog(this) == DialogResult.OK)
                    {
                        setWarehousingMovementListDetail(_warehouseMovementId);
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvWarehouseMovementListDetail.FocusedRowHandle;
                gvWarehouseMovementListDetail.FocusedRowHandle = -1;
                gvWarehouseMovementListDetail.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtWarehouMovementDetail.Select("CHECK = True");
                if (rows.Length < 1)
                    Dangol.Message("체크된 부품이 없습니다.");

                JObject jResult = new JObject();

                if (Dangol.MessageYN("선택하신 품목을 반환처리하시겠습니까?") == DialogResult.Yes)
                {
                    List<long> listInventoryId = new List<long>();

                    foreach (DataRow row in rows)
                    {
                        listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    }

                    if (DBConnect.makeWarehousingMovement(_currentRow["WAREHOUSING_WAREHOUSE_ID"], 
                        _currentRow["RELEASE_WAREHOUSE_ID"], 
                        _currentRow["WAREHOUSING_PALLET_ID"],
                        _currentRow["RELEASE_PALLET_ID"],
                        listInventoryId, 
                        ref jResult))
                    {
                        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                       
                        DataRow dr = _dtWarehouMovement.NewRow();
                        dr["WAREHOUSE_MOVEMENT_ID"] = jResult["WAREHOUSE_MOVEMENT_ID"];
                        dr["WAREHOUSE_MOVEMENT"] = jResult["WAREHOUSE_MOVEMENT"];
                        dr["WAREHOUSE_MOVEMENT_STATE"] = "E";
                        dr["RELEASE_WAREHOUSE_ID"] = jResult["RELEASE_WAREHOUSE_ID"];
                        dr["WAREHOUSING_WAREHOUSE_ID"] = jResult["WAREHOUSING_WAREHOUSE_ID"];
                        dr["RELEASE_PALLET_ID"] = jResult["RELEASE_PALLET_ID"];
                        dr["WAREHOUSING_PALLET_ID"] = jResult["WAREHOUSING_PALLET_ID"];
                        dr["REGIST_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                        dr["RELEASE_DT"] = null;
                        dr["WAREHOUSING_DT"] = null;
                        dr["CNT"] = rows.Length;
                        _dtWarehouMovement.Rows.Add(dr);


                        string sql = $"UPDATE TN_WAREHOUSE_MOVEMENT_LIST SET INVENTORY_STATE = 'T', UPDATE_USER_ID = '{ProjectInfo._userId}', UPDATE_DT = NOW() WHERE INVENTORY_ID IN = ({string.Join(",", listInventoryId)})";
                        if (!DBConnect.execute(sql, ref jResult))
                        {
                            foreach (DataRow row in rows)
                            {
                                row.BeginEdit();
                                row["INVENTORY_STATE"] = "T";
                                row.EndEdit();

                            }
                            Dangol.Message("반환처리되었습니다.");
                        }
                        else 
                        {                             
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            
                        }
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }

        private void lgcWarehouseMovement_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                if (!_currentRow["WAREHOUSE_MOVEMENT_STATE"].Equals("R"))
                {
                    Dangol.Message("출고된 리스트만 입고가능합니다.");
                    return;
                }

                JObject jResult = new JObject();
                if (Dangol.MessageYN("선택하신 리스트를 입고처리하시겠습니까?") == DialogResult.Yes)
                {
                    string sql = $"UPDATE TN_WAREHOUSE_MOVEMENT SET WAREHOUSE_MOVEMENT_STATE = 'W', WAREHOUSING_DT = NOW(), UPDATE_USER_ID = '{ProjectInfo._userId}', UPDATE_DT = NOW() WHERE WAREHOUSE_MOVEMENT_ID = {_warehouseMovementId}";

                    if (DBConnect.execute(sql, ref jResult))
                    {
                        _currentRow.BeginEdit();
                        _currentRow["WAREHOUSE_MOVEMENT_STATE"] = "W";
                        _currentRow["WAREHOUSING_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                        _currentRow.EndEdit();

                        sql = $"UPDATE TN_WAREHOUSE_MOVEMENT_LIST SET INVENTORY_STATE = 'W', UPDATE_USER_ID = '{ProjectInfo._userId}', UPDATE_DT = NOW() WHERE WAREHOUSE_MOVEMENT_ID = {_warehouseMovementId}";
                        if(DBConnect.execute(sql, ref jResult))
                        {
                            setWarehousingMovementListDetail(_warehouseMovementId);

                            lgcWarehouseMovement.CustomHeaderButtons[1].Properties.Enabled = true;

                            lcgStatus.CustomHeaderButtons[0].Properties.Enabled = true;
                            lcgStatus.CustomHeaderButtons[1].Properties.Enabled = true;
                            gcBasket.OptionsColumn.AllowEdit = true;

                            Dangol.Message("입고처리되었습니다.");
                        }
                        else
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }    
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (!_currentRow["WAREHOUSE_MOVEMENT_STATE"].Equals("W"))
                {
                    Dangol.Message("입고된 리스트만 완료가능합니다.");
                    return;
                }

                JObject jResult = new JObject();
                if (Dangol.MessageYN("선택하신 리스트를 완료처리하시겠습니까?") == DialogResult.Yes)
                {
                    string sql = $"UPDATE TN_WAREHOUSE_MOVEMENT SET WAREHOUSE_MOVEMENT_STATE = 'F', WAREHOUSING_DT = NOW(), UPDATE_USER_ID = '{ProjectInfo._userId}', UPDATE_DT = NOW() WHERE WAREHOUSE_MOVEMENT_ID = {_warehouseMovementId}";

                    if (DBConnect.execute(sql, ref jResult))
                    {
                        _currentRow.BeginEdit();
                        _currentRow["WAREHOUSE_MOVEMENT_STATE"] = "F";
                        _currentRow.EndEdit();

                        lgcWarehouseMovement.CustomHeaderButtons[0].Properties.Enabled = false;

                        lcgStatus.CustomHeaderButtons[0].Properties.Enabled = false;
                        lcgStatus.CustomHeaderButtons[1].Properties.Enabled = false;
                        gcBasket.OptionsColumn.AllowEdit = false;

                        Dangol.Message("완료처리되었습니다.");
                        
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            //Dangol.ShowSplash();

            string dtFrom = "";
            string dtTo = "";
            string dtRegist = "";
            string dtRelease = "";
            string dtWarehousing = "";

            if (deRegistDt.EditValue != null && !string.IsNullOrEmpty(deRegistDt.EditValue.ToString()))
                dtRegist = $"{deRegistDt.Text}";

            if (deReleaseDt.EditValue != null && !string.IsNullOrEmpty(deReleaseDt.EditValue.ToString()))
                dtRelease = $"{deReleaseDt.Text}";

            if (deWarehousingDt.EditValue != null && !string.IsNullOrEmpty(deWarehousingDt.EditValue.ToString()))
                dtWarehousing = $"{deWarehousingDt.Text}";

            
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
                dtFrom = $"{deDtFrom.Text} 00:00:00";

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
                dtTo = $"{deDtTo.Text} 23:59:59";

            JObject jResult = new JObject();
            getWarehouseMovementList(teRegistNo.EditValue, 
                teBarcode.EditValue, 
                ConvertUtil.ToString(leWarehouseMovementStatus.EditValue),
                dtFrom, dtTo, dtRegist, dtRelease, dtWarehousing, leReleaseWarehouseNo.EditValue, leWarehousingWarehouseNo.EditValue, ref jResult);

            isSearch = true;
           // Dangol.CloseSplash();
        }

        private bool getWarehouseMovementList(object registNo,
            object barcode,
            string state,
            string dtFrom,
            string dtTo,
            string registDt,
            string releaseDt,
            string warehousingDt,
            object releaseWarehouseNo,
            object warehousingWarehouse,
            ref JObject jResult)
        {
            if (!checkSearch(registNo, barcode, state, dtFrom, dtTo, registDt, releaseDt, warehousingDt, releaseWarehouseNo, warehousingWarehouse))
            {
                Dangol.Message("검색 조건을 하나이상 입력하세요.");
                return false;
            }

            if (DBConnect.getWarehouseMovementList(registNo, barcode, state, dtFrom, dtTo, registDt, releaseDt, warehousingDt, releaseWarehouseNo, warehousingWarehouse, "", ref jResult))
            {
                _dtWarehouMovement.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehouMovement.NewRow();
                        dr["WAREHOUSE_MOVEMENT_ID"] = obj["WAREHOUSE_MOVEMENT_ID"];
                        dr["WAREHOUSE_MOVEMENT"] = obj["WAREHOUSE_MOVEMENT"];
                        dr["WAREHOUSE_MOVEMENT_STATE"] = obj["WAREHOUSE_MOVEMENT_STATE"];
                        dr["RELEASE_WAREHOUSE_ID"] = obj["RELEASE_WAREHOUSE_ID"];
                        dr["WAREHOUSING_WAREHOUSE_ID"] = obj["WAREHOUSING_WAREHOUSE_ID"];
                        dr["RELEASE_PALLET_ID"] = obj["RELEASE_PALLET_ID"];
                        dr["WAREHOUSING_PALLET_ID"] = obj["WAREHOUSING_PALLET_ID"];
                        dr["REGIST_DT"] = start.AddMilliseconds(ConvertUtil.ToInt64(obj["CREATE_DT"])).ToLocalTime().ToString("yyyy-MM-dd");

                        if (string.IsNullOrEmpty(ConvertUtil.ToString(obj["RELEASE_DT"])))
                            dr["RELEASE_DT"] = null;
                        else
                            dr["RELEASE_DT"] = start.AddMilliseconds(ConvertUtil.ToInt64(obj["RELEASE_DT"])).ToLocalTime().ToString("yyyy-MM-dd");

                        if (string.IsNullOrEmpty(ConvertUtil.ToString(obj["WAREHOUSING_DT"])))
                            dr["WAREHOUSING_DT"] = null;
                        else
                            dr["WAREHOUSING_DT"] = start.AddMilliseconds(ConvertUtil.ToInt64(obj["WAREHOUSING_DT"])).ToLocalTime().ToString("yyyy-MM-dd");

                        //dr["REGIST_DT"] = obj["CREATE_DT"];
                        dr["CNT"] = obj["CNT"];
                        dr["PRODUCT_CNT"] = obj["PRODUCT_CNT"];
                        
                        _dtWarehouMovement.Rows.Add(dr);
                    }
                }

                return true;

            }
            else
            {
                return false;
            }
        }

        private bool checkSearch(object registNo,
            object barcode,
            string state,
            string dtFrom,
            string dtTo,
            string registDt,
            string releaseDt,
            string warehousingDt,
            object releaseWarehouseNo,
            object warehousingWarehouse)
        {

            if(string.IsNullOrEmpty(ConvertUtil.ToString(registNo)) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(barcode))&&
                string.IsNullOrEmpty(state) &&
                string.IsNullOrEmpty(dtFrom) &&
                string.IsNullOrEmpty(dtTo) &&
                string.IsNullOrEmpty(registDt) &&
                string.IsNullOrEmpty(releaseDt) &&
                string.IsNullOrEmpty(warehousingDt) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(releaseWarehouseNo)) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(warehousingWarehouse)) 
                )
            {
                return false;
            }

            return true;
        }


        private bool setWarehousingMovementListDetail(long warehouseMovementId)
        {
            JObject jResult = new JObject();

            if (DBConnect.getBarcodeList(warehouseMovementId, ref jResult))
            {
                _dtWarehouMovementDetail.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    //DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtWarehouMovementDetail.NewRow();
                        dr["WAREHOUSE_MOVEMENT_ID"] = obj["WAREHOUSE_MOVEMENT_ID"];
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["PALLET"] = obj["PALLET"];
                        dr["CHECK"] = false;
                        _dtWarehouMovementDetail.Rows.Add(dr);
                    }
                }

                return true;

            }
            else
            {
                return false;
            }
        }


        private void rileBasket_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            JObject jResult = new JObject();
           
            string sql = $"UPDATE TN_INVENTORY SET PALLET = '{ e.NewValue}', UPDATE_USER_ID = '{ProjectInfo._userId}', UPDATE_DT = NOW() WHERE INVENTORY_ID = {_inventoryId}";
            DBConnect.execute(sql, ref jResult);

            sql = $"UPDATE TN_WAREHOUSE_MOVEMENT_LIST SET INVENTORY_STATE = 'L', UPDATE_USER_ID = '{ProjectInfo._userId}', UPDATE_DT = NOW() WHERE INVENTORY_ID = {_inventoryId}";
            DBConnect.execute(sql, ref jResult);

            _currentRowDetail.BeginEdit();
            _currentRowDetail["INVENTORY_STATE"] = "W";
            _currentRowDetail.EndEdit();
        }

       
    }
}