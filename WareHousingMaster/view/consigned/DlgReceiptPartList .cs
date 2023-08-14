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
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using WareHousingMaster.view.adjustment;

namespace WareHousingMaster.view.consigned
{
    public partial class DlgReceiptPartList : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtReceiptPartGrid;
        BindingSource _bsReceiptPartGrid;

        DataRowView _currentRow;
        long _proxyId;
  
        public DlgReceiptPartList(long proxyId ,DataTable dtGrid)
        {
            InitializeComponent();

            _dtReceiptPartGrid = new DataTable();
            _dtReceiptPartGrid.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("PROXY_PART_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("DETAIL_DATA", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("ASSIGN_YN_O", typeof(bool)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("ASSIGN_YN", typeof(bool)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _bsReceiptPartGrid = new BindingSource();

            _proxyId = proxyId;

            foreach (DataRow row in dtGrid.Rows)
            {
                DataRow drGrid = _dtReceiptPartGrid.NewRow();

                drGrid["PROXY_PART_ID"] = row["PROXY_PART_ID"];
                drGrid["INVENTORY_ID"] = row["INVENTORY_ID"];
                drGrid["COMPONENT_CD"] = row["COMPONENT_CD"];
                drGrid["DETAIL_DATA"] = row["DETAIL_DATA"];
                drGrid["ASSIGN_YN_O"] = row["ASSIGN_YN"];
                drGrid["ASSIGN_YN"] = row["ASSIGN_YN"];
                drGrid["CONSIGNED_TYPE"] = row["CONSIGNED_TYPE"];
                drGrid["CHECK"] = false;

                _dtReceiptPartGrid.Rows.Add(drGrid);
            }
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

        }

        private void setInfoBox()
        {
            DataTable dtConsignedType = new DataTable();
            dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtConsignedType, 1, "생산대행");
            Util.insertRowonTop(dtConsignedType, 2, "자사재고");
            Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            gcPart.DataSource = null;
            _bsReceiptPartGrid.DataSource = _dtReceiptPartGrid;
            gcPart.DataSource = _bsReceiptPartGrid;
        }

        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            try
            {
                gvPart.BeginUpdate();
                foreach (DataRow row in _dtReceiptPartGrid.Rows)
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

        private void lcgBarcodeList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            int topRowIndex = gvPart.TopRowIndex;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            gvPart.BeginDataUpdate();

            foreach (DataRow row in _dtReceiptPartGrid.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvPart.EndDataUpdate();
        }

        private void sbCreate_Click(object sender, EventArgs e)
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtReceiptPartGrid.Select("CHECK = TRUE");

            if (Dangol.MessageYN($"선택한 부품으로 부품 출고를 접수하시겠습니까?(선택부품: {rows.Length}개)") == DialogResult.Yes)
            {
                JObject jobj = new JObject();
                JObject jResult = new JObject();
                List<long> _listProxyPartId = new List<long>();
                List<long> _listUnAssignProxyPartId = new List<long>();

                jobj.Add("PROXY_ID", _proxyId);
                bool assignYnO;
                bool assignYn;
                string componentCd;
                string modelNmDetail = "";
                foreach (DataRow row in rows)
                {
                    _listProxyPartId.Add(ConvertUtil.ToInt64(row["PROXY_PART_ID"]));
                    componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                    modelNmDetail += $"{componentCd}, ";

                    assignYnO = ConvertUtil.ToBoolean(row["ASSIGN_YN_O"]);
                    assignYn = ConvertUtil.ToBoolean(row["ASSIGN_YN"]);

                    if (assignYnO && !assignYn)
                        _listUnAssignProxyPartId.Add(ConvertUtil.ToInt64(row["PROXY_PART_ID"]));
                }

                if (modelNmDetail.Length > 0)
                    modelNmDetail = modelNmDetail.Substring(0, modelNmDetail.Length - 2);

                jobj.Add("MODEL_NM_DETAIL", modelNmDetail);
                jobj.Add("LIST_PROXY_PART_ID", string.Join(",", _listProxyPartId));

                if (DBConsigned.receiptPartRelease(jobj, ref jResult))
                {
                    jobj.RemoveAll();
                    jobj.Add("BULK_YN", 1);
                    jobj.Add("LIST_PROXY_PART_ID", string.Join(",", _listUnAssignProxyPartId));
                    DBConsigned.cancelReleaseInventory(jobj, ref jResult);

                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.Cancel;
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvPart_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (ConvertUtil.ToInt32(gvPart.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                if (ConvertUtil.ToBoolean(_currentRow["ASSIGN_YN_O"]))
                    gcAssign.OptionsColumn.AllowEdit = true;
                else
                    gcAssign.OptionsColumn.AllowEdit = false;

                if (ConvertUtil.ToInt32(_currentRow["CONSIGNED_TYPE"]) == 2)
                    gvPart.Appearance.FocusedRow.ForeColor = Color.Red;
                else
                    gvPart.Appearance.FocusedRow.ForeColor = Color.Black;
            }
            else
            {
                _currentRow = null;
                gcAssign.OptionsColumn.AllowEdit = false;
            }
        }

        private void gvPart_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "ASSIGN_YN")
            {
                bool assignYnO = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN_O"]));
                bool assignYn = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN"]));
                bool check = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["CHECK"]));

                if (check && assignYnO && !assignYn)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
                }
                else
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Transparent);
            }
        }
    }
}