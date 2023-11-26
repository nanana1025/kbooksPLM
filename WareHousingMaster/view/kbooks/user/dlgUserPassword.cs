using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.kbooks;

namespace WareHousingMaster.view.kbooks.user
{
    public partial class dlgUserPassword : DevExpress.XtraEditors.XtraForm
    {
        string _userId = "-1";
        public string _password { get; set; }
        public JObject _jobj { get; set; }

        bool _isAdmin;

        public dlgUserPassword(string userId, string passwd, bool isAdmin = false)
        {
            InitializeComponent();

            _jobj = new JObject();

            _userId = userId;
            _password = passwd;

            _isAdmin = isAdmin;

        }
        private void dlgUserPassword_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            //getRepNo();

            

            teUserId.Text = _userId;
            tePassword.ReadOnly = _isAdmin;

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

            if (!_isAdmin)
            {
                string passwd = Encrypt.Password(_userId, tePassword.Text);

                if (!passwd.Equals(_password))
                {
                    Dangol.Message("현재 비밀번호를 정확히 입력해 주세요.");
                    return;
                }
            }

            //if (teNewPassword.Text.Length < 4)
            //{
            //    Dangol.Message("비밀번호는 4자리 이상으로 구성해야합니다.");
            //    return;
            //}

            if (!teNewPassword.Text.Equals(teNewPasswordConfirm.Text))
            {
                Dangol.Message("변경할 비밀번호와 비밀번호 확인이 일치하지 않습니다.");
                return;
            }


            if (Dangol.MessageYN($"비밀번호를 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
               
                string url = "/common/execute.json";

                string passwd = Encrypt.Password(_userId, teNewPasswordConfirm.Text);

                string query = $"UPDATE TN_USER_MST SET PASSWD = '{passwd}' WHERE USER_ID = '{_userId}'";

                _jobj.Add("QUERY", query);

                if (DBConnect.getRequest(_jobj, ref jResult, url))
                {
                    _password = passwd;
                    Dangol.Message("수정되었습니다.");
                    this.DialogResult = DialogResult.OK;
                    
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                }

                //JObject jPartInfo = new JObject();
                //JObject jResult = new JObject();
                //string url = "/user/changePassword.json";

                //jPartInfo.Add("PASSWD", Encrypt.Password(ProjectInfo._userId, tePassword.Text));
                //jPartInfo.Add("NEW_PASSWD", Encrypt.Password(ProjectInfo._userId, teNewPassword.Text));

                //if (DBConnect.getRequest(jPartInfo, ref jResult, url))
                //{
                //    this.DialogResult = DialogResult.OK;
                //    Dangol.Message("수정되었습니다.");
                //}
                //else
                //{
                //    Dangol.Message(jResult["MSG"]);
                //}
            }

        }




        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}