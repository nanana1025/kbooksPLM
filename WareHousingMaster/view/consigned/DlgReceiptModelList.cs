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
    public partial class DlgReceiptModelList : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtReceiptPartGrid;
        BindingSource _bsReceiptPartGrid;
        long _proxyId;
  
        public DlgReceiptModelList(DataTable dtGrid)
        {
            InitializeComponent();

            _dtReceiptPartGrid = new DataTable();
            _dtReceiptPartGrid.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("MODEL_ID", typeof(long)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceiptPartGrid.Columns.Add(new DataColumn("MODEL_CNT", typeof(int)));

            _bsReceiptPartGrid = new BindingSource();

            foreach (DataRow row in dtGrid.Rows)
            {
                DataRow drGrid = _dtReceiptPartGrid.NewRow();

                drGrid["MODEL_ID"] = row["MODEL_ID"];
                drGrid["MODEL_NM"] = row["MODEL_NM"];
                drGrid["MODEL_CNT"] = row["MODEL_CNT"];
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
            this.DialogResult = DialogResult.OK;
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}