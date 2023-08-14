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
using DevExpress.XtraTreeList.Nodes;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class dlgSetCustomerCheckReturn : DevExpress.XtraEditors.XtraForm
    {
        long _warehousingId;
        int _returnState;

        long _releaseId;
        string _releaseReceipt;

        long _receiptId;
        string _receipt;
        string _receiptState;

        DataTable _dt;

        public dlgSetCustomerCheckReturn(long receiptId, string receipt, string receiptState, long warehousingId, int returnState)
        {
            InitializeComponent();
            _warehousingId = warehousingId;
            _returnState = returnState;
            _receiptId = receiptId;
            _receipt = receipt;
            _receiptState = receiptState;
            
            _releaseId = -1;


            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));


        }
        private void dlgSetCustomerCheckReturn_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr1 = dtComponentCd.NewRow();
                dr1["KEY"] = ProjectInfo._componetCd[i];
                dr1["VALUE"] = ProjectInfo._componetNm[i];
                dtComponentCd.Rows.Add(dr1);
            }

            Util.LookupEditHelper(leComponentCd, dtComponentCd, "KEY", "VALUE");

            rgCustomerCheckFault.EditValue = _returnState;

            leComponentCd.ItemIndex = 0;


            if(_returnState != 2)
            {
                lcReturnReceipt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcReturnReceipt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCreateReceipt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            getData();
        }

        private void getData()
        {
            JObject jResult = new JObject();

            if (DBUsedPurchase.getUsedPurchaseReleaseInfo(_warehousingId, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    _releaseId = ConvertUtil.ToInt64(jResult["RELEASE_ID"]);
                    _releaseReceipt = ConvertUtil.ToString(jResult["RECEIPT"]);
                    teReleaseReceipt.Text = _releaseReceipt;
                    meRequest.Text = ConvertUtil.ToString(jResult["REQUEST"]);
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }

            usrReceiptPartTreeShort1.setinitialize(_receiptId, _receipt, _receiptState);
            usrReceiptPartTreeShort1.getComponentAll();
        }


        private void sbInsert_Click(object sender, EventArgs e)
        {        
            this.DialogResult = DialogResult.OK;
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(meRequest.Text))
                meRequest.Text = $"{leComponentCd.Text} {teModelNm.Text}";
            else
                meRequest.Text = $"{ meRequest.Text}\r\n{leComponentCd.Text} {teModelNm.Text}";
        }

        private void lgcPart_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1)) //전체보기
            {
                usrReceiptPartTreeShort1.expandAll();
            }
            else if (e.Button.Properties.Tag.Equals(2)) //간략히
            {
                usrReceiptPartTreeShort1.foldAll();
            }
        }

        private void sbReturn_Click(object sender, EventArgs e)
        {
            DataRow[] rows = usrReceiptPartTreeShort1.getDaraRow();
            _dt.Clear();
            int index = 1;
            foreach(DataRow row in rows)
            {
                DataRow dr = _dt.NewRow();
                dr["NO"] = index++;
                dr["COMPONENT_CD"] = row["PART_LT_COMPONENT_CD"];
                dr["INVENTORY_ID"] = row["INVENTORY_ID"];
                dr["MODEL_NM"] = row["EXAMINE_MODEL_NM"];
                _dt.Rows.Add(dr);
            }

            using (dlgCustomerReturnCheck customerReturnCheck = new dlgCustomerReturnCheck(_warehousingId, _dt, meRequest.Text))
            {
                if (customerReturnCheck.ShowDialog(this) == DialogResult.OK)
                {
                    teReleaseReceipt.Text = customerReturnCheck._releaseReceipt;
                    _releaseId = customerReturnCheck._releaseId;
                }
            }
        }
    }
}