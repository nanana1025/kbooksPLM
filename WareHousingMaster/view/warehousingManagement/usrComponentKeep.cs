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
using System.Management;
using Microsoft.Win32;
using DevExpress.XtraGrid.Views.Grid;

namespace WareHousingMaster.view.warehousingManagement
{
    public partial class usrComponentKeep : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtBarcodeList;
        DataTable _dtWarehouMovement;
        BindingSource bs;
        BindingSource bsStorage;

        BindingSource _bsPallet;
        BindingSource _bsPalletRelease;
        BindingSource _bsPalletWarehousing;

        DataTable _dtLocation;
        DataTable _dtPallet;
        DataTable _dtPalletSearch;
        DataTable _dtPalletRelease;
        DataTable _dtPalletWarehousing;

        DataRowView _currentRow;
        DataRowView _currentWarehouseMovementRow;
        List<string> _listBarcode;
        List<long> _listInventoryId;
        Dictionary<string, string> _dicPallet;
        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        long _warehouseMovementId;
        bool _headerButtonVisible = true;
        long _selectWarehouseMovementId = -1;
        object _inventoryId = null;

        string _releasePalletNo = "-1";


        DateTime _warehousingDate = new DateTime();
        DataTable _dtPrintPort;

        public usrComponentKeep()
        {
            InitializeComponent();

            long _warehouseMovementId = -1;

            _dtBarcodeList = new DataTable();
            _dtBarcodeList.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtBarcodeList.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("PALLET", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtWarehouMovement = new DataTable();
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSE_MOVEMENT_ID", typeof(long)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSE_MOVEMENT", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSE_MOVEMENT_STATE", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("RELEASE_WAREHOUSE_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSING_WAREHOUSE_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("RELEASE_PALLET_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("WAREHOUSING_PALLET_ID", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("REGIST_DT", typeof(string)));
            _dtWarehouMovement.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtWarehouMovement.Columns.Add(new DataColumn("PRODUCT_CNT", typeof(int)));
            _dtWarehouMovement.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            bs = new BindingSource();
            bsStorage = new BindingSource();
            _bsPallet = new BindingSource();
            _bsPalletRelease = new BindingSource();
            _bsPalletWarehousing = new BindingSource();

            _listBarcode = new List<string>();
            _listInventoryId = new List<long>();

            _dicPallet = new Dictionary<string, string>();
            //_dicWarehouMovementList = new Dictionary<long, Dictionary<long, string>>();

        }
        private void usrComponentKeep_Load(object sender, EventArgs e)
        {
            //Dangol.ShowSplash();

            setInfoBox();
            setIInitData();

            JObject jResult = new JObject();
            getWarehouseMovementList("", "", "", "", "", "", "E", ref jResult);



            //teInputBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            //teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));

            //Dangol.CloseSplash();

            _bsPallet.Filter = $"WAREHOUSE_ID = '-1'";
            lePalletNo.ItemIndex = 0;

            _bsPalletRelease.Filter = $"WAREHOUSE_ID = '-1'";
            leReleasePalletNo.ItemIndex = 0;

            _bsPalletWarehousing.Filter = $"WAREHOUSE_ID = '-1'";
            leWarehousingPalletNo.ItemIndex = 0;

        }

        private void setInfoBox()
        {
            //DataTable dtDeviceType = new DataTable();

            //dtDeviceType.Columns.Add(new DataColumn("KEY", typeof(string)));
            //dtDeviceType.Columns.Add(new DataColumn("VALUE", typeof(int)));

            //for (int i = 0; i < ProjectInfo._arrTypeNm.Length; i++)
            //{
            //    DataRow dr = dtDeviceType.NewRow();

            //dr["KEY"] = i;
            //dr["VALUE"] = ProjectInfo._arrTypeNm[i];
            //dtDeviceType.Rows.Add(dr);
            //}

            //Util.LookupEditHelper(leWarehouseNo, dtDeviceType, "KEY", "VALUE");
            //Util.LookupEditHelper(leReleaseWarehouseNo, dtDeviceType, "KEY", "VALUE");
            //Util.LookupEditHelper(leWarehousingWarehouseNo, dtDeviceType, "KEY", "VALUE");


 
            _bsPallet.DataSource = _dtPallet;

            _dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
            Util.insertRowonTop(_dtLocation, "", "선택안합");
            
            Util.LookupEditHelper(leWarehouseNo, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leReleaseWarehouseNo, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leWarehousingWarehouseNo, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(rileReleaseWarehouse, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehousingWarehouse, _dtLocation, "KEY", "VALUE");

            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");

            foreach (DataRow row in _dtPallet.Rows)
                if (!_dicPallet.ContainsKey(ConvertUtil.ToString(row["PALLET_NM"])))
                    _dicPallet.Add(ConvertUtil.ToString(row["PALLET_NM"]), ConvertUtil.ToString(row["PALLET_ID"]));

            Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
            dicPalletDefault.Add("WAREHOUSE_ID", "-1");
            dicPalletDefault.Add("PALLET_ID", "");
            dicPalletDefault.Add("PALLET_NM", "선택안합");

            Util.insertRowonTop(_dtPallet, dicPalletDefault);

            _dtPalletSearch = _dtPallet.Copy();
            _dtPalletRelease = _dtPallet.Copy();
            _dtPalletWarehousing = _dtPallet.Copy();

            _bsPallet.DataSource = _dtPalletSearch;
            _bsPalletRelease.DataSource = _dtPalletRelease;
            _bsPalletWarehousing.DataSource = _dtPalletWarehousing;

            Util.LookupEditHelper(lePalletNo, _bsPallet, "PALLET_ID", "PALLET_NM");
            Util.LookupEditHelper(leReleasePalletNo, _bsPalletRelease, "PALLET_ID", "PALLET_NM");
            Util.LookupEditHelper(leWarehousingPalletNo, _bsPalletWarehousing, "PALLET_ID", "PALLET_NM");
            Util.LookupEditHelper(rilePallet, _dtPallet, "PALLET_ID", "PALLET_NM");

            DataTable dtWarehouseMovementState = Util.getCodeList("CD1201", "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehouseMovementState, dtWarehouseMovementState, "KEY", "VALUE");
        }

        private void leWarehouseNo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            _bsPallet.Filter = $"WAREHOUSE_ID = '{e.NewValue}' OR WAREHOUSE_ID = '-1'";
            lePalletNo.ItemIndex = 0;

        }

        private void leReleaseWarehouseNo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            _bsPalletRelease.Filter = $"WAREHOUSE_ID = '{e.NewValue}' OR WAREHOUSE_ID = '-1'";
            leReleasePalletNo.ItemIndex = 0;
        }

        private void leWarehousingWarehouseNo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            _bsPalletWarehousing.Filter = $"WAREHOUSE_ID = '{e.NewValue}' OR WAREHOUSE_ID = '-1'";
            leWarehousingPalletNo.ItemIndex = 0;
        }

        private void setIInitData()
        {
            // 기본 값 설정
            //lePrintPort.EditValue = ProjectInfo._printerPort;
            //leLocation.EditValue = ProjectInfo._location;
            //leProductType.EditValue = ProjectInfo._type;
            //teUserName.EditValue = ProjectInfo._userName;

            gcBarcodeList.DataSource = null;
            bs.DataSource = _dtBarcodeList;
            gcBarcodeList.DataSource = bs;

            gcStorageStatus.DataSource = null;
            bsStorage.DataSource = _dtWarehouMovement;
            gcStorageStatus.DataSource = bsStorage;

        }


        private void gvBarcodeList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvBarcodeList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _inventoryId = _currentRow["INVENTORY_ID"];

                if (!_currentRow["PALLET"].Equals(_releasePalletNo))
                {
                    gvBarcodeList.Appearance.FocusedRow.ForeColor = Color.Red;
                    //gvBarcodeList.Appearance.FocusedCell.ForeColor = Color.Red;
                }
                else
                {
                    gvBarcodeList.Appearance.FocusedRow.ForeColor = Color.Black;
                    //gvBarcodeList.Appearance.FocusedCell.ForeColor = Color.Black;
                }
            }
        }

        private void gvStorageStatus_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvStorageStatus.RowCount > 0);

            if (isValidRow)
            {
                _currentWarehouseMovementRow = e.Row as DataRowView;
                _warehouseMovementId = ConvertUtil.ToInt64(_currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_ID"]);

                //if (ConvertUtil.ToBoolean(_currentWarehouseMovementRow["CHECK"]))
                //{
                //    gvStorageStatus.Appearance.FocusedRow.BackColor = Color.DarkGray;
                //}
                //else
                //{
                //    gvStorageStatus.Appearance.FocusedRow.BackColor = Color.LightSkyBlue;
                //}

            }
            else
            {
                _warehouseMovementId = -1;
            }
        }

        private void lcgStatus_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                //if (_dtBarcodeList.Rows.Count < 1)
                //    Dangol.Message("입력된 부품이 없습니다.");
                //else
                {

                    if (leReleaseWarehouseNo.EditValue == null || string.IsNullOrEmpty(leReleaseWarehouseNo.EditValue.ToString()))
                    {
                        Dangol.Message("출고 창고를 선택하세요.");
                        return;
                    }

                    //if (leReleasePalletNo.EditValue == null || string.IsNullOrEmpty(leReleasePalletNo.EditValue.ToString()))
                    //{
                    //    Dangol.Message("[출고]적재위치를 선택하세요.");
                    //    return;
                    //}

                    if (_listInventoryId.Count < 1)
                    {
                        Dangol.Message("이동할 부품이 없습니다.");
                        return;
                    }


                    if(_selectWarehouseMovementId > 0)
                    {
                        if (Dangol.MessageYN("보관 리스트를 수정하시겠습니까?") == DialogResult.Yes)
                        {
                            JObject jResult = new JObject();

                            if (DBConnect.updateWarehousingMovement(_selectWarehouseMovementId, leReleaseWarehouseNo.EditValue, leWarehousingWarehouseNo.EditValue, leReleasePalletNo.EditValue, leWarehousingPalletNo.EditValue,_listInventoryId, ref jResult))
                            {
                                updateList("E");
                                BarcodeListClear();
                                Dangol.Message("수정되었습니다.");
                            }
                            else
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }
                        }
                    }
                    else
                    {
                        if (Dangol.MessageYN("입력된 부품을 보관하시겠습니까?") == DialogResult.Yes)
                        {
                            JObject jResult = new JObject();
                            long warehouseMovementId = -1;

                            if (makeWarehouseMovement(ref warehouseMovementId, ref jResult))
                            {
                                BarcodeListClear();
                                gvStorageStatus.MoveLast();
                                Dangol.Message("수정되었습니다.");
                            }

                            
                        }
                    }

                    
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_dtBarcodeList.Rows.Count < 1)
                {
                    Dangol.Message("입력된 부품이 없습니다.");
                    return;
                }

                if (leReleaseWarehouseNo.EditValue == null || string.IsNullOrEmpty(leReleaseWarehouseNo.EditValue.ToString()))
                {
                    Dangol.Message("출고 창고를 선택하세요.");
                    return;
                }

                //if (leReleasePalletNo.EditValue == null || string.IsNullOrEmpty(leReleasePalletNo.EditValue.ToString()))
                //{
                //    Dangol.Message("[출고]적재위치를 선택하세요.");
                //    return;
                //}

                if (leWarehousingWarehouseNo.EditValue == null || string.IsNullOrEmpty(leWarehousingWarehouseNo.EditValue.ToString()))
                {
                    Dangol.Message("입고 창고를 선택하세요.");
                    return;
                }

                //if (leWarehousingPalletNo.EditValue == null || string.IsNullOrEmpty(leWarehousingPalletNo.EditValue.ToString()))
                //{
                //    Dangol.Message("[입고]적재위치를 선택하세요.");
                //    return;
                //}

                if (Dangol.MessageYN("현재 부품을 출고이동하시겠습니까?") == DialogResult.Yes)
                {
                    if (_selectWarehouseMovementId > 0)
                    {
                        JObject jResult = new JObject();

                        if (DBConnect.updateWarehousingMovement(_selectWarehouseMovementId, leReleaseWarehouseNo.EditValue, leWarehousingWarehouseNo.EditValue, leReleasePalletNo.EditValue, leWarehousingPalletNo.EditValue, _listInventoryId, ref jResult))
                        {

                            if (releaseWarehouseMovement(_selectWarehouseMovementId, ref jResult))
                            {
                                updateList("R");
                                BarcodeListClear();
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
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
                    else
                    {
                        JObject jResult = new JObject();
                        long warehouseMovementId = -1;

                        if (!makeWarehouseMovement(ref warehouseMovementId, ref jResult))
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            return;
                        }

                        gvStorageStatus.MoveLast();

                        if (releaseWarehouseMovement(_warehouseMovementId, ref jResult))
                        {
                            _currentWarehouseMovementRow.BeginEdit();
                            _currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"] = "R";
                            _currentWarehouseMovementRow.EndEdit();

                            BarcodeListClear();
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                        else
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                    }

                    //if (DBConnect.releaseWarehouseBarcodeList(_listInventoryId, leWarehousingWarehouseNo.EditValue, leWarehousingPalletNo.EditValue, ref jResult))
                    //{
                    //    _dtBarcodeList.Clear();
                    //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    //}
                    //else
                    //{
                    //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    //}
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
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

                            if (_listInventoryId.Contains(Convert.ToInt64(row["INVENTORY_ID"])))
                                _listInventoryId.Remove(Convert.ToInt64(row["INVENTORY_ID"]));

                            row.Delete();
                        }
                        _dtBarcodeList.EndInit();
                    }
                }

            }
        }

        private void updateList(string warehouseMovementState)
        {
            DataRow[] row = _dtWarehouMovement.Select($"WAREHOUSE_MOVEMENT_ID = {_selectWarehouseMovementId}");
            gvStorageStatus.BeginDataUpdate();
            if (row.Length > 0)
            {
                row[0].BeginEdit();
                row[0]["WAREHOUSE_MOVEMENT_STATE"] = warehouseMovementState;
                row[0]["RELEASE_WAREHOUSE_ID"] = leReleaseWarehouseNo.EditValue;
                row[0]["WAREHOUSING_WAREHOUSE_ID"] = leWarehousingWarehouseNo.EditValue;
                row[0]["RELEASE_PALLET_ID"] = leReleasePalletNo.EditValue;
                row[0]["WAREHOUSING_PALLET_ID"] = leWarehousingPalletNo.EditValue;
                row[0]["CHECK"] = false;
                row[0]["CNT"] = _listInventoryId.Count;
                row[0].EndEdit();
            }
            gvStorageStatus.EndDataUpdate();
        }

        private bool makeWarehouseMovement(ref long warehouseMovementId, ref JObject jResult)
        {
            if (DBConnect.makeWarehousingMovement(leReleaseWarehouseNo.EditValue, leWarehousingWarehouseNo.EditValue, leReleasePalletNo.EditValue, leWarehousingPalletNo.EditValue, _listInventoryId, ref jResult))
            {
                warehouseMovementId = Convert.ToInt64(jResult["WAREHOUSE_MOVEMENT_ID"]);

                DataRow dr = _dtWarehouMovement.NewRow();
                dr["WAREHOUSE_MOVEMENT_ID"] = jResult["WAREHOUSE_MOVEMENT_ID"];
                dr["WAREHOUSE_MOVEMENT"] = jResult["WAREHOUSE_MOVEMENT"];
                dr["WAREHOUSE_MOVEMENT_STATE"] = "E";
                dr["RELEASE_WAREHOUSE_ID"] = jResult["RELEASE_WAREHOUSE_ID"];
                dr["WAREHOUSING_WAREHOUSE_ID"] = jResult["WAREHOUSING_WAREHOUSE_ID"];
                dr["RELEASE_PALLET_ID"] = jResult["RELEASE_PALLET_ID"];
                dr["WAREHOUSING_PALLET_ID"] = jResult["WAREHOUSING_PALLET_ID"];
                dr["REGIST_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                dr["CNT"] = _dtBarcodeList.Rows.Count;
                dr["CHECK"] = false;
                _dtWarehouMovement.Rows.Add(dr);

                return true;

            }
            else
            {
                return false;
            }
        }


        private bool releaseWarehouseMovement(long warehouseMovementId, ref JObject jResult)
        {
            if (DBConnect.releaseWarehouseMovementList(warehouseMovementId, ref jResult))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private void sbClear_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("작성중인 창고이동을 초기화하시겠습니까?") == DialogResult.Yes)
            {
                BarcodeListClear();
            }
        }

        private void BarcodeListClear()
        {
            if (_selectWarehouseMovementId > 0)
            {
                DataRow[] row = _dtWarehouMovement.Select($"WAREHOUSE_MOVEMENT_ID = {_selectWarehouseMovementId}");

                if (row.Length > 0)
                {
                    row[0].BeginEdit();
                    row[0]["CHECK"] = false;
                    row[0].EndEdit();
                }
            }

            _selectWarehouseMovementId = -1;
            teWarehouseRegistNo.EditValue = "";
            _dtBarcodeList.Clear();
            _listBarcode.Clear();
            _listInventoryId.Clear();
            leReleaseWarehouseNo.EditValue = null;
            leWarehousingWarehouseNo.EditValue = null;
            leReleasePalletNo.EditValue = null;
            leWarehousingPalletNo.EditValue = null;
        }

        private void lgcWarehouseMovement_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                if (_warehouseMovementId > 0)
                {
                    if (!ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"]).Equals("E"))
                    {
                        Dangol.Message("보관상태의 리스트만 출고가능합니다.");
                        return;
                    }

                    //if(string.IsNullOrEmpty(ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSING_PALLET_ID"])))
                    //{
                    //    Dangol.Message("입고 적재 정보가 없습니다.");
                    //    return;
                    //}

                    if (string.IsNullOrEmpty(ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSING_WAREHOUSE_ID"])))
                    {
                        Dangol.Message("입고창고 정보가 없습니다.");
                        return;
                    }

                    if (Dangol.MessageYN("선택하신 등록리스트를 출고이동하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();

                        if (releaseWarehouseMovement(_warehouseMovementId, ref jResult))
                        {
                            _currentWarehouseMovementRow.BeginEdit();
                            _currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"] = "R";
                            _currentWarehouseMovementRow.EndEdit();

                            if (_selectWarehouseMovementId > 0)
                                BarcodeListClear();

                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                        else
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_warehouseMovementId > 0)
                {
                    if (ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"]).Equals("D"))
                    {
                        Dangol.Message("이미 삭제된 리스트입니다.");
                        return;
                    }
                    else if(ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"]).Equals("R"))
                    {
                        Dangol.Message("출고완료된 리스트는 삭제할 수 없습니다.");
                        return;
                    }
                    else if (ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"]).Equals("W"))
                    {
                        Dangol.Message("입고완료된 리스트는 삭제할 수 없습니다.");
                        return;
                    }

                    if (Dangol.MessageYN("선택하신 등록리스트를 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();

                        if (DBConnect.deleteWarehouseMovementList(_warehouseMovementId, ref jResult))
                        {
                            _currentWarehouseMovementRow.BeginEdit();
                            _currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"] = "D";
                            _currentWarehouseMovementRow["CNT"] = 0;
                            _currentWarehouseMovementRow["PRODUCT_CNT"] = 0;
                            _currentWarehouseMovementRow["CHECK"] = false;
                            _currentWarehouseMovementRow.EndEdit();

                            if (_selectWarehouseMovementId > 0)
                                BarcodeListClear();

                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                        else
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                if(_warehouseMovementId > 0)
                {
                    if (!ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_STATE"]).Equals("E"))
                    {
                        Dangol.Message("보관상태의 리스트만 수정가능합니다.");
                        return;
                    }

                    if (Dangol.MessageYN("선택하신 등록리스트를 수정하시겠습니까? \n[현재 작성중인 관리번호 리스트가 변경됩니다.])") == DialogResult.Yes)
                    {
                        if (getBarcodeList())
                        {
                            _currentWarehouseMovementRow.BeginEdit();
                            _currentWarehouseMovementRow["CHECK"] = true;
                            _currentWarehouseMovementRow.EndEdit();

                            teWarehouseRegistNo.EditValue = _currentWarehouseMovementRow["WAREHOUSE_MOVEMENT"];
                            _selectWarehouseMovementId = ConvertUtil.ToInt64(_currentWarehouseMovementRow["WAREHOUSE_MOVEMENT_ID"]);
                        }

                        //using (dlgUpdateWarehouseMovementList dlgBarcodelist = new dlgUpdateWarehouseMovementList(_warehouseMovementId, _currentWarehouseMovementRow, _dtLocation, _dtPallet))
                        //{
                        //    if (dlgBarcodelist.ShowDialog(this) == DialogResult.OK)
                        //    {
                        //        _currentWarehouseMovementRow.BeginEdit();
                        //        _currentWarehouseMovementRow["CNT"] = dlgBarcodelist.Cnt;
                        //        _currentWarehouseMovementRow.EndEdit();
                        //    }
                        //}
                    }
                }
            }
        }

        private bool getBarcodeList()
        {
            JObject jResult = new JObject();
            _dtBarcodeList.Clear();
            _listBarcode.Clear();
            _listInventoryId.Clear();

            if (DBConnect.getBarcodeList(_warehouseMovementId, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtBarcodeList.NewRow();
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["PALLET"] = obj["PALLET"];
                        dr["CHECK"] = false;
                        _dtBarcodeList.Rows.Add(dr);

                        _listBarcode.Add(ConvertUtil.ToString(obj["BARCODE"]).ToUpper());
                        _listInventoryId.Add(ConvertUtil.ToInt64(obj["INVENTORY_ID"]));
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));

                return false;
            }

            leReleaseWarehouseNo.EditValue = _currentWarehouseMovementRow["RELEASE_WAREHOUSE_ID"];
            leReleasePalletNo.EditValue = _currentWarehouseMovementRow["RELEASE_PALLET_ID"];

            leWarehousingWarehouseNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_WAREHOUSE_ID"];
            leWarehousingPalletNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_PALLET_ID"];

            return true;
        }

        private void lcgStatus_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(4))
            {
                int rowhandle = gvBarcodeList.FocusedRowHandle;
                int topRowIndex = gvBarcodeList.TopRowIndex;
                gvBarcodeList.FocusedRowHandle = -1;
                gvBarcodeList.FocusedRowHandle = rowhandle;

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
        }

        private void lcgStatus_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(4))
            {
                int rowhandle = gvBarcodeList.FocusedRowHandle;
                int topRowIndex = gvBarcodeList.TopRowIndex;
                gvBarcodeList.FocusedRowHandle = -1;
                gvBarcodeList.FocusedRowHandle = rowhandle;

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
        }

        private void sbInput_Click(object sender, EventArgs e)
        {
            string barcode = teInputBarcode.Text;
            if (barcode.Length == 12)
            {
                putBarcodeToGrid(barcode);
                teInputBarcode.Text = "";
            }
            else
            {
                Dangol.Message("관리번호를 확인하세요");
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
                else if (data.Length == 7 || data.Length == 6)
                {
                    putPalletToGrid(data);
                    teInputBarcode.Text = "";
                }
                else
                {
                    Dangol.Message("관리번호를 확인하세요");
                }
            }
        }

        private void putBarcodeToGrid(string barcode)
        {
            if (_listBarcode.Contains(barcode.ToUpper()))
                return;

            JObject jResult = new JObject();

            if (DBConnect.getBarcodeValidity(barcode, ref jResult))
            {
                DataRow dr = _dtBarcodeList.NewRow();
                dr["INVENTORY_ID"] = jResult["INVENTORY_ID"];
                dr["BARCODE"] = jResult["BARCODE"];
                dr["COMPONENT_CD"] = jResult["COMPONENT_CD"];
                dr["PALLET"] = jResult["PALLET"];
                dr["CHECK"] = false;
                _dtBarcodeList.Rows.Add(dr);

                _listBarcode.Add(barcode.ToUpper());
                _listInventoryId.Add(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void putPalletToGrid(string pallet)
        {
            //if (_listBarcode.Contains(barcode.ToUpper()))
            //    return;

            JObject jResult = new JObject();

            if (DBConnect.getBarcodeInPalletValidity(_dicPallet[pallet], ref jResult))
            {

                JArray jResultArray = (JArray)jResult["DATA"];

                foreach (JObject item in jResultArray)
                {
                    string barcode = item.GetValue("BARCODE").ToString();
                    object inventoryId = item.GetValue("INVENTORY_ID");

                    if (_listBarcode.Contains(barcode.ToUpper()))
                        continue;

                    DataRow dr = _dtBarcodeList.NewRow();
                    dr["INVENTORY_ID"] = inventoryId;
                    dr["BARCODE"] = barcode;
                    dr["COMPONENT_CD"] = item.GetValue("COMPONENT_CD");
                    dr["PALLET"] = item.GetValue("PALLET");
                    dr["CHECK"] = false;
                    _dtBarcodeList.Rows.Add(dr);

                    _listBarcode.Add(barcode.ToUpper());
                    _listInventoryId.Add(ConvertUtil.ToInt64(inventoryId));
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            //Dangol.ShowSplash();

            JObject jResult = new JObject();

            string dtFrom = "";
            string dtTo = "";
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
                dtFrom = $"{deDtFrom.Text} 00:00:00";

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
                dtTo = $"{deDtTo.Text} 23:59:59";

            if (getWarehouseMovementList(teRegistNo.EditValue, leWarehouseNo.EditValue, lePalletNo.EditValue, teBarcode.EditValue, dtFrom, dtTo, "E,R", ref jResult))
            {
                if (!Convert.ToBoolean(jResult["EXIST"]))
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }

            //Dangol.CloseSplash();
        }

        private bool getWarehouseMovementList(object registNo,
           object warehouseNo,
           object palletNo,
           object barcode,
           string dtFrom,
           string dtTo,
           string warehouseMovementState,
           ref JObject jResult)
        {

            if (!checkSearch(registNo, barcode, warehouseMovementState, dtFrom, dtTo, warehouseNo, palletNo))
            {
                Dangol.Message("검색 조건을 하나이상 입력하세요.");
                return false;
            }

            if (DBConnect.getWarehouseMovementList(registNo, barcode, warehouseMovementState, dtFrom, dtTo, "","","",warehouseNo, "",palletNo, ref jResult))
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
                        //dr["REGIST_DT"] = obj["CREATE_DT"];
                        dr["CNT"] = obj["CNT"];
                        dr["PRODUCT_CNT"] = obj["PRODUCT_CNT"];
                        dr["CHECK"] = false;
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
            object releaseWarehouseNo,
            object palletNo
            )
        {

            if (string.IsNullOrEmpty(ConvertUtil.ToString(registNo)) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(barcode)) &&
                state.Equals("E,R,W") &&
                string.IsNullOrEmpty(dtFrom) &&
                string.IsNullOrEmpty(dtTo) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(releaseWarehouseNo)) &&
                string.IsNullOrEmpty(ConvertUtil.ToString(palletNo))
                )
            {
                return false;
            }

            return true;

        }

        private void gvStorageStatus_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if(ConvertUtil.ToBoolean(gvStorageStatus.GetDataRow(e.RowHandle)["CHECK"]))
                { 
                    e.Appearance.BackColor = Color.DarkGray;
                }
            }
        }

       

        private void gvBarcodeList_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (!gvBarcodeList.GetDataRow(e.RowHandle)["PALLET"].Equals(_releasePalletNo))
                {
                    e.Appearance.ForeColor = Color.Red;
                    //e.Appearance.BackColor = Color.LemonChiffon;
                }
            }
        }

        private void leReleasePalletNo_EditValueChanged(object sender, EventArgs e)
        {
            if (leReleasePalletNo.EditValue == null)
                _releasePalletNo = "-1";
            else
                _releasePalletNo = leReleasePalletNo.EditValue.ToString();
        }

    }
}