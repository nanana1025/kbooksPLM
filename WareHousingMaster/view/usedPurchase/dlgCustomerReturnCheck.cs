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

namespace WareHousingMaster.view.usedPurchase
{
    public partial class dlgCustomerReturnCheck : DevExpress.XtraEditors.XtraForm
    {
        long _warehousingId;
        string _request;
        DataTable _dt;
        BindingSource _bs;
        public long _releaseId { get; private set; }
        public string _releaseReceipt { get; private set; }
        public dlgCustomerReturnCheck(long warehousingId, DataTable dt, string request)
        {
            InitializeComponent();
            _warehousingId = warehousingId;
            _request = request;
            _dt = dt;
            _bs = new BindingSource();

            _bs.DataSource = _dt;
        }
        private void dlgGetPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            gcPart.DataSource = _bs;
            meRequest.Text = _request;
        }

        private void sbInsert_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("반송내용을 접수하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();

                List<long> listInventoryId = new List<long>();
                foreach(DataRow row in _dt.Rows)
                    listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));

                if (DBUsedPurchase.receiptUsedPurchaseReturn(_warehousingId, listInventoryId, _request, ref jResult))
                {
                    _releaseId = ConvertUtil.ToInt64(jResult["RELEASE_ID"]);
                    _releaseReceipt = ConvertUtil.ToString(jResult["RECEIPT"]);
                    Dangol.Message("접수되었습니다");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}