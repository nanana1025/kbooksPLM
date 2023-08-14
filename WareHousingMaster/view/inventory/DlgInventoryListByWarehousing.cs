using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.inventory
{
    public partial class DlgInventoryListByWarehousing : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtPart;
        BindingSource _bs;

        DataRowView _currentRow;
        long _warehousingId;
        long _companyId;
        long _releaseCompany;
        long _componentId;
        string _componentCd;

        JObject _jobj;

        public bool _isUpdate { private set; get; }


        public DlgInventoryListByWarehousing(JObject jobj, bool isReadonly = false)
        {
            InitializeComponent();

            _dtPart = new DataTable();
            _dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_TYPE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("LOCK_YN", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            //_warehousingId = warehousingId;
            //_companyId = companyId;
            //_releaseCompany = releaseCompany;
            //_componentId = componentId;
            //_componentCd = componentCd;

            _jobj = jobj;

            _bs = new BindingSource();

            _isUpdate = false;

            if(isReadonly)
            {
                gvPart.OptionsBehavior.ReadOnly = true;

                lcInventoryState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcInventoryStateBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcInventoryCat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcInvnetoryCatBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcPrice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcPriceBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcgPartList.CustomHeaderButtons[0].Properties.Visible = false;
            }
        }
        private void DlgInventoryListByWarehousing_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

            Dangol.ShowSplash();
            getInventoryList();
            Dangol.CloseSplash();

            

            if (ProjectInfo._userType.Equals("E"))
            {
                lcgInventory.BeginUpdate();

                gcCheck.Visible = false;

                lcgPartList.CustomHeaderButtons[0].Properties.Visible = false;
                lcgPartList.CustomHeaderButtons[1].Properties.Visible = false;

                lcInventoryState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcInventoryStateBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcInventoryCat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcInvnetoryCatBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcPrice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcPriceBtn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcgInventory.EndUpdate();

                gcPrice.Visible = false;
                gcReleasePrice.Visible = false;
            }

            

        }

        private void setInfoBox()
        {
            DataTable dtInventoryType = Util.getCodeList("CD0304", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryType, dtInventoryType, "KEY", "VALUE");
            

            DataTable dtInventoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState, dtInventoryState, "KEY", "VALUE");
            Util.LookupEditHelper(leInventoryState, dtInventoryState, "KEY", "VALUE");

            DataTable dtInventoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryCat, dtInventoryCat, "KEY", "VALUE");
            Util.LookupEditHelper(leInventoryCat, dtInventoryCat, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            gcPart.DataSource = null;
            _bs.DataSource = _dtPart;
            gcPart.DataSource = _bs;

            leInventoryState.ItemIndex = 0;
            leInventoryCat.ItemIndex = 0;
            seReleasePrice.EditValue = 0;
        }

        private void sbCreate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvPart_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (ConvertUtil.ToInt32(gvPart.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
               // seReleasePrice.EditValue = ConvertUtil.ToInt64(_currentRow["RELEASE_PRICE"]);
                //if (ConvertUtil.ToBoolean(_currentRow["ASSIGN_YN_O"]))
                //    gcAssign.OptionsColumn.AllowEdit = true;
                //else
                //    gcAssign.OptionsColumn.AllowEdit = false;
            }
            else
            {
                //seReleasePrice.EditValue = 0;
                _currentRow = null;
                //gcAssign.OptionsColumn.AllowEdit = false;
            }
        }

        private void gvPart_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.Column.FieldName == "ASSIGN_YN")
            //{
            //    bool assignYnO = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN_O"]));
            //    bool assignYn = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN"]));
            //    bool check = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["CHECK"]));

            //    if (check && assignYnO && !assignYn)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
            //    }
            //    else
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Transparent);
            //}
        }


        private bool getInventoryList()
        {
            JObject jResult = new JObject();

            _dtPart.Clear();

            if (DBInventory.getInventoryByWarehousing(_jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;
                    foreach (JObject jData in jArray.Children<JObject>())
                    {
                        DataRow dr1 = _dtPart.NewRow();
                        dr1["NO"] = index++;
                        dr1["INVENTORY_ID"] = jData["INVENTORY_ID"];
                        dr1["BARCODE"] = jData["BARCODE"];
                        dr1["MODEL_NM"] = jData["MODEL_NM"];
                        dr1["INVENTORY_TYPE"] = jData["INVENTORY_TYPE"];
                        dr1["INVENTORY_STATE"] = jData["INVENTORY_STATE"];
                        dr1["INVENTORY_CAT"] = jData["INVENTORY_CAT"];
                        dr1["LOCK_YN"] = jData["LOCK_YN"];
                        dr1["PRICE"] = ConvertUtil.ToInt64(jData["INIT_PRICE"]);
                        dr1["RELEASE_PRICE"] = ConvertUtil.ToInt64(jData["RELEASE_PRICE"]);
                        dr1["CREATE_DT"] = ConvertUtil.ToDateTimeNull(jData["CREATE_DT"]);
                        dr1["UPDATE_DT"] = ConvertUtil.ToDateTimeNull(jData["UPDATE_DT"]);
                        dr1["CHECK"] = false;

                        _dtPart.Rows.Add(dr1);
                    }
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private void lcgPartList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("체크된 부품이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN($"체크하신 부품을 삭제하시겠습니까?(출고되거나 사용중인 부품 제외)") == DialogResult.Yes)
                {
                    rows = _dtPart.Select("CHECK = TRUE AND INVENTORY_STATE <> 'R' AND LOCK_YN = 'N'");
                    if (rows.Length < 1)
                    {
                        Dangol.Message("적용할 부품이 없습니다.");
                        return;
                    }

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<long> listId = new List<long>();
                    long data;

                    foreach (DataRow row in rows)
                    {
                        data = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                        if (!listId.Contains(data))
                        {
                            listId.Add(data);
                        }

                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listId));
                    jobj.Add("INVENTORY_STATE", "D");
                    jobj.Add("BULK_YN", 1);

                    if (DBInventory.updateInventorytInfo(jobj, ref jResult))
                    {
                        Dangol.ShowSplash();
                        gvPart.BeginDataUpdate();
                        foreach (DataRow row in rows)
                        {
                            row["INVENTORY_STATE"] = "D";
                        }
                        gvPart.EndDataUpdate();
                        Dangol.CloseSplash();
                        _isUpdate = true;
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
            }
        }

        private void sbInventoryState_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("체크된 부품이 없습니다.");
                return;
            }

            string inventoryState = ConvertUtil.ToString(leInventoryState.EditValue);

            string msg = "";

            if (inventoryState.Equals("D"))
                msg = $"체크하신 부품의 재고상태를 '{leInventoryState.Text}'로 변경하시겠습니까?(출고 또는 사용중인 재고 제외)";
            else
                msg = $"체크하신 부품의 재고상태를 '{leInventoryState.Text}'로 변경하시겠습니까?(사용중인 재고 제외)";


            if (Dangol.MessageYN(msg) == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                List<long> listId = new List<long>();
                long data;
                if (inventoryState.Equals("D"))
                {
                    rows = _dtPart.Select("CHECK = TRUE AND INVENTORY_STATE <> 'R' AND LOCK_YN = 'N'");
                    if (rows.Length < 1)
                    {
                        Dangol.Message("적용할 부품이 없습니다.");
                        return;
                    }
                }
                else
                {
                    rows = _dtPart.Select("CHECK = TRUE AND LOCK_YN = 'N'");
                    if (rows.Length < 1)
                    {
                        Dangol.Message("적용할 부품이 없습니다.");
                        return;
                    }
                }

                foreach (DataRow row in rows)
                {
                    data = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                    if (!listId.Contains(data))
                    {
                        listId.Add(data);
                    }

                }
                

                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listId));
                jobj.Add("INVENTORY_STATE", inventoryState);
                jobj.Add("BULK_YN", 1);

                if (DBInventory.updateInventorytInfo(jobj, ref jResult))
                {
                    Dangol.ShowSplash();
                    gvPart.BeginDataUpdate();
                    foreach (DataRow row in rows)
                    {
                        row["INVENTORY_STATE"] = inventoryState;
                    }
                    gvPart.EndDataUpdate();
                    Dangol.CloseSplash();
                    _isUpdate = true;
                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
        }

        private void sbInventoryCat_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("체크된 부품이 없습니다.");
                return;
            }

            string inventoryCat = ConvertUtil.ToString(leInventoryCat.EditValue);

            if (Dangol.MessageYN($"체크하신 부품의 부품상태를 '{leInventoryCat.Text}'로 변경하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                List<long> listId = new List<long>();
                long data;
                foreach (DataRow row in rows)
                {
                    data = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                    if (!listId.Contains(data))
                    {
                        listId.Add(data);
                    }

                }

                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listId));
                jobj.Add("INVENTORY_CAT", inventoryCat);
                jobj.Add("BULK_YN", 1);

                if (DBInventory.updateInventorytInfo(jobj, ref jResult))
                {
                    Dangol.ShowSplash();
                    gvPart.BeginDataUpdate();
                    foreach (DataRow row in rows)
                    {
                        row["INVENTORY_CAT"] = inventoryCat;
                    }
                    gvPart.EndDataUpdate();
                    Dangol.CloseSplash();

                    _isUpdate = true;
                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
        }
        private void sbReleasePrice_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtPart.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("체크된 부품이 없습니다.");
                return;
            }

            long releasePrice = ConvertUtil.ToInt64(seReleasePrice.EditValue);

            if (Dangol.MessageYN($"체크하신 부품의 출고가를 '{releasePrice}'원으로 변경하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                List<long> listId = new List<long>();
                long data;
                foreach (DataRow row in rows)
                {
                    data = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                    if (!listId.Contains(data))
                    {
                        listId.Add(data);
                    }

                }

                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listId));
                jobj.Add("RELEASE_PRICE", releasePrice);
                jobj.Add("BULK_YN", 1);

                if (DBInventory.updateInventorytInfo(jobj, ref jResult))
                {
                    Dangol.ShowSplash();
                    gvPart.BeginDataUpdate();
                    foreach (DataRow row in rows)
                    {
                        row["RELEASE_PRICE"] = releasePrice;
                    }
                    gvPart.EndDataUpdate();
                    Dangol.CloseSplash();
                    _isUpdate = true;
                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
        }


        private void lcgPartList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            try
            {
                gvPart.BeginUpdate();
                foreach (DataRow row in _dtPart.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvPart.DataRowCount; i++)
                {
                    int rowHandle = gvPart.GetVisibleRowHandle(i);
                    rows.Add(gvPart.GetDataRow(rowHandle));
                }

                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    // Change the field value.
                    row["CHECK"] = true;
                }
            }
            finally
            {
                gvPart.EndUpdate();
            }
        }

        private void lcgPartList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            gvPart.BeginDataUpdate();

            foreach (DataRow row in _dtPart.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvPart.EndDataUpdate();
        }

        
    }
}