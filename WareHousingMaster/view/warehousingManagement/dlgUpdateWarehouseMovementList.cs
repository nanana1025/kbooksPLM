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
    public partial class dlgUpdateWarehouseMovementList : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtBarcodeList;
        DataTable _dtBarcodeListOriginal;
        BindingSource bs;

        List<string> _listBarcode;
        List<long> _listInventoryId;

        DataTable _dtLocation;
        DataTable _dtPallet;
        DataRowView _currentWarehouseMovementRow;

        long _warehouseMovementId;
        public int Cnt { get; private set; }

        public dlgUpdateWarehouseMovementList(long warehouseMovementId, DataRowView currentWarehouseMovementRow, DataTable dtLocation, DataTable dtPallet)
        {
            InitializeComponent();

            _dtBarcodeList = new DataTable();
            _dtBarcodeList.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtBarcodeList.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtBarcodeListOriginal = new DataTable();
            _dtBarcodeListOriginal.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtBarcodeListOriginal.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcodeListOriginal.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtBarcodeListOriginal.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            bs = new BindingSource();

            _listBarcode = new List<string>();
            _listInventoryId = new List<long>();

            _warehouseMovementId = warehouseMovementId;
            _currentWarehouseMovementRow = currentWarehouseMovementRow;
            _dtLocation = dtLocation;
            _dtPallet = dtPallet;
        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            JObject jResult = new JObject();

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
                        dr["CHECK"] = false;
                        _dtBarcodeList.Rows.Add(dr);

                        DataRow row = _dtBarcodeListOriginal.NewRow();
                        row["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        row["BARCODE"] = obj["BARCODE"];
                        row["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        row["CHECK"] = false;
                        _dtBarcodeListOriginal.Rows.Add(row);


                        _listBarcode.Add(ConvertUtil.ToString(obj["BARCODE"]).ToUpper());
                        _listInventoryId.Add(ConvertUtil.ToInt64(obj["INVENTORY_ID"]));
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }

            gcBarcodeList.DataSource = null;
            bs.DataSource = _dtBarcodeList;
            gcBarcodeList.DataSource = bs;

            Util.LookupEditHelper(leWarehousingWarehouseNo, _dtLocation, "KEY", "VALUE");
            Util.LookupEditHelper(leWarehousingPalletNo, _dtPallet, "KEY", "VALUE");

            leWarehousingWarehouseNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_WAREHOUSE_NO"];
            leWarehousingPalletNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_PALLET_NO"];
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
        }

        private void putBarcodeToGrid(string barcode)
        {
            if (_listBarcode.Contains(barcode.ToUpper()))
                return;

            DataRow[] rows = _dtBarcodeListOriginal.Select($"BARCODE = '{barcode}'");

            if (rows.Length > 0)
            {
                DataRow dr = _dtBarcodeList.NewRow();
                dr["INVENTORY_ID"] = rows[0]["INVENTORY_ID"];
                dr["BARCODE"] = rows[0]["BARCODE"];
                dr["COMPONENT_CD"] = rows[0]["COMPONENT_CD"];
                dr["CHECK"] = false;
                _dtBarcodeList.Rows.Add(dr);

                _listBarcode.Add(barcode.ToUpper());
                _listInventoryId.Add(ConvertUtil.ToInt64(rows[0]["INVENTORY_ID"]));
            }
            else
            {
                JObject jResult = new JObject();

                if (DBConnect.getBarcodeValidity(barcode, ref jResult))
                {
                    DataRow dr = _dtBarcodeList.NewRow();
                    dr["INVENTORY_ID"] = jResult["INVENTORY_ID"];
                    dr["BARCODE"] = jResult["BARCODE"];
                    dr["COMPONENT_CD"] = jResult["COMPONENT_CD"];
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
                if (Dangol.MessageYN("입력된 부품을 보관하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();

                    //if (DBConnect.updateWarehousingMovement(_warehouseMovementId, _listInventoryId, ref jResult))
                    //{
                    //    Cnt = _dtBarcodeList.Rows.Count;
                    //    Dangol.Message("수정되었습니다.");
                    //    this.DialogResult = DialogResult.OK;
                    //}
                    //else
                    //{
                    //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    //}
                }
            }

           
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}