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
using DevExpress.XtraSplashScreen;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.inventory;
using DevExpress.XtraGrid.Views.Grid;
using WareHousingMaster.view.warehousing.createComponent;
using WareHousingMaster.view.warehousing.selectComponent;
using DevExpress.XtraTreeList.Nodes;
using WareHousingMaster.view.usedPurchase;
using ImportExcel;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;
using WareHousingMaster.view.common.kbooks;

namespace WareHousingMaster.view.kbooks.user
{
    public partial class dlgCreateUser : DevExpress.XtraEditors.XtraForm
    {
        public dlgCreateUser()
        {
            InitializeComponent();
        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;


            DataTable dtUserType = Util.getCodeList("CD0201", "KEY", "VALUE");
            Util.LookupEditHelper(leUserType, dtUserType, "KEY", "VALUE");

            DataTable dtUserState = Util.getCodeList("CD0202", "KEY", "VALUE");
            Util.LookupEditHelper(leUserState, dtUserState, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "-");
            Util.LookupEditHelper(leCompanyId, dtCompany, "KEY", "VALUE");

            DataTable dtDeptCd = Util.getCodeList("CD0502", "KEY", "VALUE");
            Util.insertRowonTop(dtDeptCd, "-1", "-");
            Util.LookupEditHelper(leDeptCd, dtDeptCd, "KEY", "VALUE");

            DataTable dtTeamCd = Util.getCodeList("CD0503", "KEY", "VALUE");
            Util.insertRowonTop(dtTeamCd, "-1", "-");
            Util.LookupEditHelper(leTeamCd, dtTeamCd, "KEY", "VALUE");

            DataTable dtPosition = Util.getCodeList("CD0505", "KEY", "VALUE");
            Util.insertRowonTop(dtPosition, "-1", "-");
            Util.LookupEditHelper(lePosition, dtPosition, "KEY", "VALUE");



            leUserType.EditValue = "U";
            leUserState.EditValue = "A";
            leCompanyId.EditValue = "-1";
            leDeptCd.EditValue = "-1";
            leTeamCd.EditValue = "-1";
            lePosition.EditValue = "-1";





        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ConvertUtil.ToString(teUserNm.Text)))
            {
                Dangol.Message("아이디를 입력하세요.");
                return;
            }

            if (string.IsNullOrWhiteSpace(ConvertUtil.ToString(tePassword.EditValue)))
            {
                Dangol.Message("비밀번호를 입력하세요.");
                return;
            }

            if (!ConvertUtil.ToString(tePassword.EditValue).Equals(ConvertUtil.ToString(tePasswordConfirm.EditValue)))
            {
                Dangol.Message("비밀번호확인이 일치하지 않습니다.");
                return;
            }

            if (string.IsNullOrWhiteSpace(ConvertUtil.ToString(teUserNm.EditValue)))
            {
                Dangol.Message("사용자 이름을 입력하세요.");
                return;
            }

            if (Dangol.MessageYN($"사용자를 추가하시겠습니까?") == DialogResult.Yes)
            {
                JObject jPartInfo = new JObject();
                JObject jResult = new JObject();
                string url = "/user/createUser.json";

                jPartInfo.Add("CREATE_USER_ID", ConvertUtil.ToString(teUserId.Text));
                jPartInfo.Add("PASSWD", Encrypt.Password(teUserId.Text, tePassword.Text));
                jPartInfo.Add("USER_NM", ConvertUtil.ToString(teUserNm.Text));
                jPartInfo.Add("STATE_CD", ConvertUtil.ToString(leUserState.EditValue));
                jPartInfo.Add("USER_TYPE_CD", ConvertUtil.ToString(leUserType.EditValue));
                jPartInfo.Add("COMPANY_ID", ConvertUtil.ToInt64(leCompanyId.EditValue));
                jPartInfo.Add("MOBILE", ConvertUtil.ToString(teTel.Text));
                jPartInfo.Add("DEPT_CD", ConvertUtil.ToString(leDeptCd.EditValue));
                jPartInfo.Add("TEAM_CD", ConvertUtil.ToString(leTeamCd.EditValue));
                jPartInfo.Add("POSITION", ConvertUtil.ToString(lePosition.EditValue));


                if (DBConnect.getRequest(jPartInfo, ref jResult, url))
                {
                    this.DialogResult = DialogResult.OK;
                    Dangol.Message("추가되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }

        }




        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}