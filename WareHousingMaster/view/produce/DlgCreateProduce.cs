using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.produce
{
    public partial class DlgCreateProduce : DevExpress.XtraEditors.XtraForm
    { 
        public DlgCreateProduce()
        {
            InitializeComponent();

        }
        private void dlgCreateADP_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            setInfoBox();
        }

        private void setInfoBox()
        {
            DataTable dtProduceState = Util.getCodeList("CD0801", "KEY", "VALUE");
            Util.LookupEditHelper(leProduceState, dtProduceState, "KEY", "VALUE");

            DataTable dtProduceType = Util.getCodeList("CD0802", "KEY", "VALUE");
            Util.LookupEditHelper(leProduceType, dtProduceType, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.insertRowonTop(dtCompany, -1, "해당없음");
            Util.LookupEditHelper(leCompanyId, dtCompany, "KEY", "VALUE");

        }

        private void sbClose_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void sbPartAdd_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                return;
            }

            if (Dangol.MessageYN($"생산번호를 추가하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jData = new JObject();

                jData.Add("RELEASE_STATE", ConvertUtil.ToString(leProduceState.EditValue));
                jData.Add("RELEASE_TYPE", ConvertUtil.ToString(leProduceType.EditValue));
                jData.Add("COMPANY_ID", ConvertUtil.ToInt64(leCompanyId.EditValue));
                jData.Add("DES", meDes.Text);

                if (DBProductProduce.createProduceInfo(jData, ref jResult))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private bool check()
        {
            //if (leProduceState.EditValue == null)
            //{
            //    Dangol.Message("품목명을 입력하세요.");
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(leProduceType.EditValue))
            //{
            //    Dangol.Message("상세품목명을 입력하세요");
            //    teCategory.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(leCompanyId.EditValue))
            //{
            //    tePartName.Focus();
            //    Dangol.Message("모델명을 입력하세요");
            //    return false;
            //}

            return true;


        }
    }
}