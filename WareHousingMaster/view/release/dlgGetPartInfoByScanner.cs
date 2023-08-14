using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.release
{
    public partial class dlgGetPartInfoByScanner : DevExpress.XtraEditors.XtraForm
    {
        public DataTable _dtBarcodeList { get; private set; }
        BindingSource bs;

        public List<string> _listBarcode { get; private set; }

        string _currentBarcode;

        DataRowView _currentRow;

        Regex regex1;
        Regex regex2;
        Regex regex3;

        Regex oldCoa;
        Regex newCoa;

        public int Cnt { get; private set; }

        public dlgGetPartInfoByScanner()
        {
            InitializeComponent();

            _dtBarcodeList = new DataTable();
            _dtBarcodeList.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtBarcodeList.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("OLD_COA", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("NEW_COA", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));
            _dtBarcodeList.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

            newCoa = new Regex(@"^00[0-9]{12}$");
            oldCoa = new Regex(@"^01[0-9]{12}$");

            bs = new BindingSource();

            _listBarcode = new List<string>();
        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            //JObject jResult = new JObject();

            //if (DBConnect.getBarcodeList(_warehouseMovementId, ref jResult))
            //{
            //    if (Convert.ToBoolean(jResult["EXIST"]))
            //    { 
            //        JArray jArray = JArray.Parse(jResult["DATA"].ToString());

            //        foreach (JObject obj in jArray.Children<JObject>())
            //        {
            //            DataRow dr = _dtBarcodeList.NewRow();
            //            dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
            //            dr["BARCODE"] = obj["BARCODE"];
            //            dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
            //            dr["CHECK"] = false;
            //            _dtBarcodeList.Rows.Add(dr);

            //            DataRow row = _dtBarcodeListOriginal.NewRow();
            //            row["INVENTORY_ID"] = obj["INVENTORY_ID"];
            //            row["BARCODE"] = obj["BARCODE"];
            //            row["COMPONENT_CD"] = obj["COMPONENT_CD"];
            //            row["CHECK"] = false;
            //            _dtBarcodeListOriginal.Rows.Add(row);


            //            _listBarcode.Add(ConvertUtil.ToString(obj["BARCODE"]).ToUpper());
            //            _listInventoryId.Add(ConvertUtil.ToInt64(obj["INVENTORY_ID"]));
            //        }
            //    }
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}

            gcBarcodeList.DataSource = null;
            bs.DataSource = _dtBarcodeList;
            gcBarcodeList.DataSource = bs;

            //Util.LookupEditHelper(leWarehousingWarehouseNo, _dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(leWarehousingPalletNo, _dtPallet, "KEY", "VALUE");

            //leWarehousingWarehouseNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_WAREHOUSE_NO"];
            //leWarehousingPalletNo.EditValue = _currentWarehouseMovementRow["WAREHOUSING_PALLET_NO"];
        }

        private void sbInput_Click(object sender, EventArgs e)
        {
            setData();
        }
        private void teScan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setData();
            }
        }

        private void setData()
        {
            string scanData = teScan.Text;

            if (scanData.Length < 1)
                return;


            if (scanData.Length == 12 && (regex1.IsMatch(scanData) || regex2.IsMatch(scanData) || regex3.IsMatch(scanData)))
            {
                if (string.IsNullOrWhiteSpace(_currentBarcode))
                {
                    teBarcode.Text = scanData;
                    _currentBarcode = scanData;

                    if (_listBarcode.Contains(scanData.ToUpper()))
                    {
                        DataRow[] rows = _dtBarcodeList.Select($"BARCODE = '{scanData}'");

                        if (rows.Length > 0)
                        {
                            teOldCoa.Text = $"{rows[0]["OLD_COA"]}";
                            teNewCoa.Text = $"{rows[0]["NEW_COA"]}";
                            teSerialNo.Text = $"{rows[0]["SERIAL_NO"]}";
                        }
                    }
                }
                else
                {
                    if (!_currentBarcode.Equals(scanData))
                    {
                        putBarcodeToGrid(_currentBarcode);

                        _currentBarcode = scanData;
                        teBarcode.Text = scanData;

                        if (_listBarcode.Contains(scanData.ToUpper()))
                        {
                            DataRow[] rows = _dtBarcodeList.Select($"BARCODE = '{scanData}'");

                            if (rows.Length > 0)
                            {
                                teOldCoa.Text = $"{rows[0]["OLD_COA"]}";
                                teNewCoa.Text = $"{rows[0]["NEW_COA"]}";
                                teSerialNo.Text = $"{rows[0]["SERIAL_NO"]}";
                            }
                            else
                            {
                                teOldCoa.Text = "";
                                teNewCoa.Text = "";
                                teSerialNo.Text = "";
                            }
                        }
                        else
                        {
                            teOldCoa.Text = "";
                            teNewCoa.Text = "";
                            teSerialNo.Text = "";
                        }
                    }
                }
            }
            else if (oldCoa.IsMatch(scanData))
            {
                teOldCoa.Text = scanData;
            }
            else if (newCoa.IsMatch(scanData))
            { 
                teNewCoa.Text = scanData;
            }
            else
            {
                teSerialNo.Text = scanData;
            }

            teScan.Text = "";
        }

        private void putBarcodeToGrid(string barcode)
        {
            gvBarcodeList.BeginDataUpdate();

            if (_listBarcode.Contains(barcode.ToUpper()))
            {
                DataRow[] rows = _dtBarcodeList.Select($"BARCODE = '{barcode}'");

                if (rows.Length > 0)
                {
                    rows[0].BeginEdit();
                    rows[0]["OLD_COA"] = teOldCoa.Text;
                    rows[0]["NEW_COA"] = teNewCoa.Text;
                    rows[0]["SERIAL_NO"] = teSerialNo.Text;
                    rows[0].EndEdit();
                }
            }
            else
            {
                DataRow dr = _dtBarcodeList.NewRow();
                dr["INVENTORY_ID"] = -1;
                dr["BARCODE"] = teBarcode.Text;
                dr["OLD_COA"] = teOldCoa.Text;
                dr["NEW_COA"] = teNewCoa.Text;
                dr["SERIAL_NO"] = teSerialNo.Text;
                dr["CHECK"] = false;
                _dtBarcodeList.Rows.Add(dr);

                _listBarcode.Add(barcode.ToUpper());
            }

            gvBarcodeList.EndDataUpdate();
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
                            if (_listBarcode.Contains(Convert.ToString(row["BARCODE"]).ToUpper()))
                                _listBarcode.Remove(Convert.ToString(row["BARCODE"]).ToUpper());

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
                Dangol.Message("입력된 부품/제품이 없습니다.");
            else
            {
                if (Dangol.MessageYN("입력된 부품/제품을 등록하시겠습니까?") == DialogResult.Yes)
                {
                    //JObject jResult = new JObject();

                    //if (DBConnect.updateWarehousingMovement(_warehouseMovementId, _listInventoryId, ref jResult))
                    //{
                    //    Cnt = _dtBarcodeList.Rows.Count;
                    //    Dangol.Message("수정되었습니다.");
                        this.DialogResult = DialogResult.OK;
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

        private void sbInput1_Click(object sender, EventArgs e)
        {
            putBarcodeToGrid(_currentBarcode);

            _currentBarcode = "";
            teBarcode.Text = "";

            teOldCoa.Text = "";
            teNewCoa.Text = "";
            teSerialNo.Text = "";
            teScan.Text = "";
        }
    }
}