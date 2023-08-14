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
    public partial class dlgNewETCPart : DevExpress.XtraEditors.XtraForm
    {

        string _receipt;

        public int Cnt { get; private set; }

        public dlgNewETCPart(string receipt)
        {
            _receipt = receipt;
            InitializeComponent();
        }
        private void dlgNewETCPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            DataRow dr1 = dtComponentCd.NewRow();
            dr1["KEY"] = "ETCADD";
            dr1["VALUE"] = "기타추가";
            dtComponentCd.Rows.Add(dr1);

            DataRow dr2 = dtComponentCd.NewRow();
            dr2["KEY"] = "ETCMINUS";
            dr2["VALUE"] = "기타감소";
            dtComponentCd.Rows.Add(dr2);

            Util.LookupEditHelper(leComponentCd, dtComponentCd, "KEY", "VALUE");

        }



        private void sbClose_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();

            if (leComponentCd == null)
            {
                Dangol.Message("품목을 선택해주세요.");
                return;
            }

            if (DBUsedPurchase.insertUsedPartETCComponent(_receipt, leComponentCd.EditValue, temodelNm.Text, sePrice.EditValue, ref jResult))
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}