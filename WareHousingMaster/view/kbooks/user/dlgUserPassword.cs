using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.kbooks;

namespace WareHousingMaster.view.kbooks.user
{
    public partial class dlgUserPassword : DevExpress.XtraEditors.XtraForm
    {
        string _password = "-1";
        public dlgUserPassword()
        {
            InitializeComponent();
        }
        private void dlgUserPassword_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            //getRepNo();

            teUserId.Text = ProjectInfo._userId;

        }

        //private void getRepNo()
        //{
        //    JObject jResult = new JObject();
        //    JObject jPartInfo = new JObject();
        //    string url = "/user/getUser.json";

        //    jPartInfo.Add("USER_ID", ProjectInfo._userId);

        //    if (DBConnect.getRequest(jPartInfo, ref jResult, url))
        //    {
        //        if (ConvertUtil.ToBoolean(jResult["EXIST"]))
        //        {
        //            _password = ConvertUtil.ToString(jResult["PASSWORD"]);
        //        }
        //    }
        //}

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (teNewPassword.Text.Length < 4)
            {
                Dangol.Message("비밀번호는 4자리 이상으로 구성해야합니다.");
                return;
            }

            if (!teNewPassword.Text.Equals(teNewPasswordConfirm.Text))
            {
                Dangol.Message("변경할 비밀번호와 비밀번호 확인이 일치하지 않습니다.");
                return;
            }

            if (Dangol.MessageYN($"비밀번호를 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jPartInfo = new JObject();
                JObject jResult = new JObject();
                string url = "/user/changePassword.json";

                jPartInfo.Add("PASSWD", Encrypt.Password(ProjectInfo._userId, tePassword.Text));
                jPartInfo.Add("NEW_PASSWD", Encrypt.Password(ProjectInfo._userId, teNewPassword.Text));

                if (DBConnect.getRequest(jPartInfo, ref jResult, url))
                {
                    this.DialogResult = DialogResult.OK;
                    Dangol.Message("수정되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                }
            }

        }




        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}