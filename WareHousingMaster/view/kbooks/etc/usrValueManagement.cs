using DevExpress.XtraSplashScreen;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.kbooks.user
{
    public partial class usrValueManagement : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtValue;

        public usrValueManagement()
        {
            InitializeComponent();
        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();

            spSearchNewDt.EditValueChanged -= spSearchNewDt_EditValueChanged;
            spSearchBestDt.EditValueChanged -= spSearchBestDt_EditValueChanged;
            spOrderSearchDt.EditValueChanged -= spOrderSearchDt_EditValueChanged;

            setIInitData();

            spSearchNewDt.EditValueChanged += spSearchNewDt_EditValueChanged;
            spSearchBestDt.EditValueChanged += spSearchBestDt_EditValueChanged;
            spOrderSearchDt.EditValueChanged += spOrderSearchDt_EditValueChanged;

        }

    
        private void setInfoBox()
        {
            _dtValue = Util.getTable("TN_VALUE_MGNT", "TYPE,CATEGORY,VALUE", "TYPE > 0 AND CATEGORY > 0", "TYPE ASC");
        }

        private void setIInitData()
        {
            DataRow[] rows = _dtValue.Select($"TYPE = {(int)Enum.VALUE_MGNT_TYPE.SEARCH} AND CATEGORY = {(int)Enum.VALUE_MGNT_SEARCH.NEW}");

            sbSearchNewDt.Enabled = rows.Length > 0;

            if (rows.Length > 0)
                spSearchNewDt.EditValue = ConvertUtil.ToInt32(rows[0]["VALUE"]);
            else
                spSearchNewDt.EditValue = 0;

            /*-------------------------------------------------------------------------------------------------------------------------------------*/

            rows = _dtValue.Select($"TYPE = {(int)Enum.VALUE_MGNT_TYPE.SEARCH} AND CATEGORY = {(int)Enum.VALUE_MGNT_SEARCH.BEST}");

            sbSearchBestDt.Enabled = rows.Length > 0;

            if (rows.Length > 0)
                spSearchBestDt.EditValue = ConvertUtil.ToInt32(rows[0]["VALUE"]);
            else
                spSearchBestDt.EditValue = 0;

            /*-------------------------------------------------------------------------------------------------------------------------------------*/

            rows = _dtValue.Select($"TYPE = {(int)Enum.VALUE_MGNT_TYPE.ORDER} AND CATEGORY = {(int)Enum.VALUE_MGNT_ORDER.SEARCH}");

            sbOrderSearchDt.Enabled = rows.Length > 0;

            if (rows.Length > 0)
                spOrderSearchDt.EditValue = ConvertUtil.ToInt32(rows[0]["VALUE"]);
            else
                spOrderSearchDt.EditValue = 0;

        }

        private void sbSearchNewDt_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("신간 도서 조회 일을 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();
                string url = "/common/execute.json";

                int searchDt = ConvertUtil.ToInt32(spSearchNewDt.EditValue);

                string query = $"UPDATE TN_VALUE_MGNT SET VALUE = '{searchDt}' WHERE TYPE = {(int)Enum.VALUE_MGNT_TYPE.SEARCH} AND CATEGORY = {(int)Enum.VALUE_MGNT_SEARCH.NEW}";

                jobj.Add("QUERY", query);

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    lcSearchNewDt.AppearanceItemCaption.BackColor = Color.Transparent;
                    Dangol.Message("수정되었습니다");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                }
            }
        }

        private void sbSearchBestDt_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("베스트 셀러 조회일을 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();
                string url = "/common/execute.json";

                int searchDt = ConvertUtil.ToInt32(spSearchBestDt.EditValue);

                string query = $"UPDATE TN_VALUE_MGNT SET VALUE = '{searchDt}' WHERE TYPE = {(int)Enum.VALUE_MGNT_TYPE.SEARCH} AND CATEGORY = {(int)Enum.VALUE_MGNT_SEARCH.BEST}";

                jobj.Add("QUERY", query);

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    lcSearchBestDt.AppearanceItemCaption.BackColor = Color.Transparent;
                    Dangol.Message("수정되었습니다");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                }
            }
        }

        private void sbOrderSearchDt_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("판매 도서 조회 일을 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();
                string url = "/common/execute.json";

                int searchDt = ConvertUtil.ToInt32(spOrderSearchDt.EditValue);

                string query = $"UPDATE TN_VALUE_MGNT SET VALUE = '{searchDt}' WHERE TYPE = {(int)Enum.VALUE_MGNT_TYPE.ORDER} AND CATEGORY = {(int)Enum.VALUE_MGNT_ORDER.SEARCH}";

                jobj.Add("QUERY", query);

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    lcOrderSearchDt.AppearanceItemCaption.BackColor = Color.Transparent;
                    Dangol.Message("수정되었습니다");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                }
            }
        }

        private void spSearchNewDt_EditValueChanged(object sender, EventArgs e)
        {
            lcSearchNewDt.AppearanceItemCaption.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        }

        private void spSearchBestDt_EditValueChanged(object sender, EventArgs e)
        {
            lcSearchBestDt.AppearanceItemCaption.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        }

        private void spOrderSearchDt_EditValueChanged(object sender, EventArgs e)
        {
            lcOrderSearchDt.AppearanceItemCaption.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        }
    }
}