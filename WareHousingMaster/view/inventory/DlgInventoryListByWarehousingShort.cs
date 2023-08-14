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
    public partial class DlgInventoryListByWarehousingShort : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtPart;
        BindingSource _bs;

        DataRowView _currentRow;
        long _warehousingId;
        long _companyId;
        long _releaseCompany;
        long _componentId;
        string _componentCd;

        bool _isSerial = true;

        Dictionary<long, int> _dicModelSerialType;

        public bool _isUpdate { private set; get; }
        JObject _jobj = new JObject();

        public DlgInventoryListByWarehousingShort(JObject jobj, string componentCd)
        {
            InitializeComponent();

            _dtPart = new DataTable();
            _dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_TYPE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("LOCK_YN", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("CUSTOMER_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtPart.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("RETURN_COMPLETE_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));

            _bs = new BindingSource();
            _jobj = jobj;

            _isUpdate = false;

            
            if (!componentCd.Equals("MBD"))
            {
                gcSerialNo.Visible = false;
                _isSerial = false;
            }
            else
            {
                _isSerial = true;
            }

            if (ConvertUtil.ToInt32(jobj["TYPE"]) == 0)
                gcReturnCompleteDt.Visible = false;

            _dicModelSerialType = new Dictionary<long, int>();

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


            DataTable dtInventoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryCat, dtInventoryCat, "KEY", "VALUE");

            DataTable dtSerialNoType = Util.getCodeListCustom("TN_MODEL_LIST", "MODEL_LIST_ID", "SERIAL_NO_TYPE", "DEL_YN = 'N'", "MODEL_LIST_ID ASC");

            foreach (DataRow row in dtSerialNoType.Rows)
            {
                _dicModelSerialType.Add(ConvertUtil.ToInt64(row["KEY"]), ConvertUtil.ToInt32(row["VALUE"]));
            }


        }

        private void setIInitData()
        {
            gcPart.DataSource = null;
            _bs.DataSource = _dtPart;
            gcPart.DataSource = _bs;


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
            JObject jobj = new JObject();

            _dtPart.Clear();

            _jobj.Remove("USER_ID");
            if (DBInventory.getInventoryByWarehousingShort(_jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;
                    string modelNm;
                    long modelId;
                    foreach (JObject jData in jArray.Children<JObject>())
                    {
                        modelNm = ConvertUtil.ToString(jData["MODEL_NM"]);
                        modelId = ConvertUtil.ToInt64(jData["MODEL_ID"]);

                        DataRow dr1 = _dtPart.NewRow();
                        dr1["NO"] = index++;
                        dr1["INVENTORY_ID"] = jData["INVENTORY_ID"];
                        dr1["BARCODE"] = jData["BARCODE"];
                        dr1["SPEC_NM"] = jData["SPEC_NM"];
                        dr1["MODEL_NM"] = jData["MODEL_NM"];

                        dr1["RECEIPT"] = jData["RECEIPT"];
                        dr1["CUSTOMER_NM"] = jData["CUSTOMER_NM"];
                        dr1["RELEASE_PRICE"] = jData["RELEASE_PRICE"];
                        dr1["WAREHOUSING_ID"] = jData["WAREHOUSING_ID"];
                        dr1["WAREHOUSING"] = jData["WAREHOUSING"];

                        dr1["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(jData["RELEASE_DT"]);
                        dr1["RETURN_COMPLETE_DT"] = ConvertUtil.ToDateTimeNull(jData["RETURN_COMPLETE_DT"]);

                        if (_isSerial)
                        {
                            int serialType = 0;

                            //foreach (KeyValuePair<string, int> item in ConsignedInfo._dicModelSerialType)
                            //{
                            //    if (modelNm.Contains(item.Key))
                            //        serialType = ConsignedInfo._dicModelSerialType[item.Key];
                            //}
                            if (_dicModelSerialType.ContainsKey(modelId))
                                serialType = _dicModelSerialType[modelId];


                            if (serialType == 0)
                                dr1["SERIAL_NO"] = $"{jData["SERIAL_NO"]}/{jData["SYSTEM_SN"]}";
                            else if (serialType == 1)
                                dr1["SERIAL_NO"] = "1s" + ConvertUtil.ToString(jData["SERIAL_NO"]).Replace('/', ' ').Trim();
                            else if (serialType == 2)
                                dr1["SERIAL_NO"] = ConvertUtil.ToString(jData["SYSTEM_SN"]).Replace('/', ' ').Trim();
                            else if (serialType == 3)
                                dr1["SERIAL_NO"] = ConvertUtil.ToString(jData["PRODUCT_NAME"]).Replace('/', ' ').Trim() + ConvertUtil.ToString(jData["SYSTEM_SN"]).Replace('/', ' ').Trim();
                        }

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