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
using Newtonsoft.Json.Linq;

namespace WareHousingMaster.view.warehousingManagement
{
    public partial class dlgSetPallet : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtBarcodeList;
        BindingSource bs;

        List<string> _listBarcode;
        List<string> _listPallet;
        List<long> _listInventoryId;

        Dictionary<string, string> _dicPallet;

        DataTable _dtLocation;
        DataTable _dtPallet;
        DataRowView _currentWarehouseMovementRow;
        DataRowView _currentRow;

        string _pallet;

        List<long> _currentInventoryList;

        long _warehouseMovementId;

        string _warehousingPallet = "-1";
        public int Cnt { get; private set; }

        public dlgSetPallet(long warehouseMovementId, DataRowView currentWarehouseMovementRow, DataTable dtLocation, DataTable dtPallet)
        {
            InitializeComponent();

            _dtBarcodeList = new DataTable();
            _dtBarcodeList.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtBarcodeList.Columns.Add(new DataColumn("PALLET", typeof(string)));        
            _dtBarcodeList.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            bs = new BindingSource();

            _listBarcode = new List<string>();
            _listPallet = new List<string>();
            _listInventoryId = new List<long>();
            _currentInventoryList = new List<long>();
            _dicPallet = new Dictionary<string, string>();


            _warehouseMovementId = warehouseMovementId;
            _currentWarehouseMovementRow = currentWarehouseMovementRow;
            _dtLocation = dtLocation;
            _dtPallet = dtPallet;
        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {

            gcBarcodeList.DataSource = null;
            bs.DataSource = _dtBarcodeList;
            gcBarcodeList.DataSource = bs;

            Util.LookupEditHelper(leWarehousingWarehouseNo, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leWarehousingPalletNo, _dtPallet, "PALLET_ID", "PALLET_NM");
            Util.LookupEditHelper(rilePallet, _dtPallet, "PALLET_ID", "PALLET_NM");
            
            foreach(DataRow row in _dtPallet.Rows)
                if(!_dicPallet.ContainsKey(ConvertUtil.ToString(row["PALLET_NM"])))
                    _dicPallet.Add(ConvertUtil.ToString(row["PALLET_NM"]), ConvertUtil.ToString(row["PALLET_ID"]));

            leWarehousingWarehouseNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_WAREHOUSE_ID"];
            leWarehousingPalletNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_PALLET_ID"];
            _warehousingPallet = ConvertUtil.ToString(_currentWarehouseMovementRow["WAREHOUSING_PALLET_ID"]);

            tePalletNm.EditValue = leWarehousingPalletNo.Text;
            tePallet.EditValue = _warehousingPallet;
        }

        private void sbInput_Click(object sender, EventArgs e)
        {
            //string data = teInputBarcode.Text;
            //if (data.Length == 7)
            //{
            //    string pallet = _dicPallet[data.ToUpper()];

            //    tePalletNm.EditValue = data.ToUpper();
            //    tePallet.EditValue = pallet;

            //    if (!_listPallet.Contains(pallet))
            //        _listPallet.Add(pallet);

            //    teInputBarcode.Text = "";
            //}
            //else if (data.Length == 12)
            //{
            //    putBarcodeToGrid(data);
            //    teInputBarcode.Text = "";
            //}
            //else
            //{
            //    Dangol.Message("입력번호를 확인하세요");
            //}
            inputData();
        }
        private void teInputBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                inputData();
            }
        }


        private void inputData()
        {
            string data = teInputBarcode.Text;
            if (data.Length == 7 || data.Length == 6)
            {
                _pallet = _dicPallet[data.ToUpper()];

                tePalletNm.EditValue = data.ToUpper();
                tePallet.EditValue = _pallet;

                if (!_listPallet.Contains(_pallet))
                    _listPallet.Add(_pallet);

                teInputBarcode.Text = "";

                saveData();
                _currentInventoryList.Clear();
            }
            else if (data.Length == 12)
            {
                putBarcodeToGrid(data);
                teInputBarcode.Text = "";
            }
            else
            {
                teInputBarcode.Text = "";
                //Dangol.Message("입력번호를 확인하세요");
            }
        }

        private void putBarcodeToGrid(string barcode)
        {
            if (string.IsNullOrEmpty(tePalletNm.EditValue.ToString()))
            {
                Dangol.Message("재고위치를 먼저 입력하세요");
                return;
            }

            if (_listBarcode.Contains(barcode.ToUpper()))
            {
                DataRow[] rows = _dtBarcodeList.Select($"BARCODE = '{barcode.ToUpper()}'");

                foreach(DataRow row in rows)
                {
                    row.BeginEdit();
                    row["PALLET"] = tePallet.EditValue;
                    row.EndEdit();
                }

                return;
            }
               

            JObject jResult = new JObject();

            if (DBConnect.getBarcodeValidity(barcode, ref jResult))
            {
                long inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);
                DataRow dr = _dtBarcodeList.NewRow();
                dr["PALLET"] = tePallet.EditValue;
                dr["INVENTORY_ID"] = inventoryId;
                dr["BARCODE"] = jResult["BARCODE"];
                dr["COMPONENT_CD"] = jResult["COMPONENT_CD"];
                dr["CHECK"] = false;
                _dtBarcodeList.Rows.Add(dr);

                _listBarcode.Add(barcode.ToUpper());
                _listInventoryId.Add(inventoryId);
                _currentInventoryList.Add(inventoryId);
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            
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

                            if (_listInventoryId.Contains(Convert.ToInt64(row["INVENTORY_ID"])))
                                _listInventoryId.Remove(Convert.ToInt64(row["INVENTORY_ID"]));

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

        

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (_dtBarcodeList.Rows.Count < 0)
                Dangol.Message("입력된 부품이 없습니다.");
            else
            {
                if (Dangol.MessageYN("재고 위치를 저장하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();

                    List<long> listInventoryId = new List<long>();
                    DataRow[] rows;

                    foreach (string pallet in _listPallet)
                    {
                        rows = _dtBarcodeList.Select($"PALLET = '{pallet}'");

                        if(rows.Length > 0)
                        {
                            listInventoryId.Clear();

                            foreach (DataRow row in rows)
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                            
                            DBConnect.updatePalletBulk(_warehouseMovementId, pallet, listInventoryId, "L", ref jResult);
                        }
                    }

                    Dangol.Message("저장되었습니다.");
                    this.DialogResult = DialogResult.OK;

                }
            }
        }


        private void saveData()
        {
            if (_currentInventoryList.Count > 0)
            {
                JObject jResult = new JObject();
                DBConnect.updatePalletBulk(_warehouseMovementId, _pallet, _currentInventoryList, "L", ref jResult);
            }
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvBarcodeList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (!gvBarcodeList.GetDataRow(e.RowHandle)["PALLET"].Equals(_warehousingPallet))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void gvBarcodeList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvBarcodeList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                if (!_currentRow["PALLET"].Equals(_warehousingPallet))
                {
                    gvBarcodeList.Appearance.FocusedRow.ForeColor = Color.Red;
                }
                else
                {
                    gvBarcodeList.Appearance.FocusedRow.ForeColor = Color.Black;
                }
            }
        }
    }
}