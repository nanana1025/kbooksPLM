using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
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

            DataTable dtUserType = new DataTable();
            dtUserType.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtUserType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRow(dtUserType, "M", "마스터관리자");
            Util.insertRow(dtUserType, "S", "관리자");
            Util.insertRow(dtUserType, "U", "일반유저");
            Util.LookupEditHelper(leUserType, dtUserType, "KEY", "VALUE");

            //DataTable dtUserType = Util.getCodeList("CD0201", "KEY", "VALUE");
            //Util.LookupEditHelper(leUserType, dtUserType, "KEY", "VALUE");

            //DataTable dtUserState = Util.getCodeList("CD0202", "KEY", "VALUE");
            //Util.LookupEditHelper(leUserState, dtUserState, "KEY", "VALUE");

            //DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            //Util.insertRowonTop(dtCompany, "-1", "-");
            //Util.LookupEditHelper(leCompanyId, dtCompany, "KEY", "VALUE");

            //DataTable dtDeptCd = Util.getCodeList("CD0502", "KEY", "VALUE");
            //Util.insertRowonTop(dtDeptCd, "-1", "-");
            //Util.LookupEditHelper(leDeptCd, dtDeptCd, "KEY", "VALUE");

            //DataTable dtTeamCd = Util.getCodeList("CD0503", "KEY", "VALUE");
            //Util.insertRowonTop(dtTeamCd, "-1", "-");
            //Util.LookupEditHelper(leTeamCd, dtTeamCd, "KEY", "VALUE");

            //DataTable dtPosition = Util.getCodeList("CD0505", "KEY", "VALUE");
            //Util.insertRowonTop(dtPosition, "-1", "-");
            //Util.LookupEditHelper(lePosition, dtPosition, "KEY", "VALUE");



            leUserType.EditValue = "U";
            //leUserState.EditValue = "A";
            //leCompanyId.EditValue = "-1";
            //leDeptCd.EditValue = "-1";
            //leTeamCd.EditValue = "-1";
            //lePosition.EditValue = "-1";
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
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                string url = "/common/execute.json";

                jobj.Add("CREATE_USER_ID", ConvertUtil.ToString(teUserId.Text.Trim()));
                jobj.Add("PASSWD", Encrypt.Password(teUserId.Text, tePassword.Text));
                jobj.Add("USER_NM", ConvertUtil.ToString(teUserNm.Text));
                jobj.Add("STATE_CD", ConvertUtil.ToString(leUserState.EditValue));
                jobj.Add("USER_TYPE", ConvertUtil.ToString(leUserType.EditValue));
                

                string passwd = Encrypt.Password(ConvertUtil.ToString(jobj["CREATE_USER_ID"]), ConvertUtil.ToString(jobj["PASSWD"]));

                string query = $"INSERT INTO TN_USER_MST (USER_ID, PASSWD, USER_NM, USER_TYPE, CREATE_DT, UPDATE_DT) VALUES ('{jobj["CREATE_USER_ID"]}', '{passwd}', '{jobj["USER_NM"]}', '{jobj["USER_TYPE"]}', SYSDATE, SYSDATE)";

                jobj.Add("QUERY", query);

                if (DBConnect.getRequest(jobj, ref jResult, url))
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