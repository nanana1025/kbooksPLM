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

namespace WareHousingMaster.view.release
{
    public partial class dlgCompare : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtComponentCompare;
        BindingSource bs;

        public int Cnt { get; private set; }

        public dlgCompare(DataTable dtComponentCompare)
        {
            InitializeComponent();

            _dtComponentCompare = dtComponentCompare;

            bs = new BindingSource();

            bs.DataSource = _dtComponentCompare;

            gcInventory.DataSource = bs;

        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
        }


        private void gvCompareList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (!ConvertUtil.ToBoolean(gvCompareList.GetDataRow(e.RowHandle)["CHECK"]))
            //    {
            //        e.Appearance.BackColor = Color.LemonChiffon;
            //    }
            //}
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void sbProcess_Click(object sender, EventArgs e)
        {
            if(Dangol.MessageYN("접수정보와 다른 부품이 장착되었습니다. 그래도 정보를 업데이트 하시겠습니까?") == DialogResult.Yes)
                this.DialogResult = DialogResult.Yes;
            else
                this.DialogResult = DialogResult.OK;
        }
    }
}