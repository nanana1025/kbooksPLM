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
    public partial class dlgGetBarcode : DevExpress.XtraEditors.XtraForm
    {

        public List<string> _listBarcode { get; private set; }

        public JObject _jResult { get; private set; }
        

        public dlgGetBarcode()
        {
            InitializeComponent();


            _listBarcode = new List<string>();
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

        private void sbOK_Click(object sender, EventArgs e)
        {
            Char[] delimiter = { '(', ')', '[', ']', '/', '+', '_', ',', ' ','\n', '\r' };

            string barcodes = meBarcode.Text;
            string[] arrBarcodes = barcodes.Split(delimiter);

            foreach (string barcode in arrBarcodes)
                if (!string.IsNullOrWhiteSpace(barcode) && !_listBarcode.Contains(barcode))
                    _listBarcode.Add(barcode);

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("LIST_BARCODE", string.Join(",", _listBarcode));
            jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);



            if (DBRelease.getPartInfo(jobj, ref jResult))
            {
                _jResult = jResult;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }

        }

        private void sbProcess_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}