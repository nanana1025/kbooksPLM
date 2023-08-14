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
using System.Runtime.InteropServices;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.kbooks;

namespace WareHousingMaster.view.login
{
    public partial class usrControlLogin : DevExpress.XtraEditors.XtraForm
    {
        public string _userId { get; private set; }
        public string _userPasswd { get; private set; }

        IniFile ini;
        string _local_path;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public usrControlLogin(string userId, string userPasswd, bool state = true)
        {
            InitializeComponent();
            this.teId.Text = userId;
            this.tePasswd.Text = userPasswd;
            ini = new IniFile();

            if (!state)
            {
                this.tePasswd.Text = userPasswd;
            }

            _local_path = System.Windows.Forms.Application.StartupPath + @"\login.ini";
            //ini.Load(_local_path);

            this.Icon = ProjectInfo.ProjectIcon;

        }

        private void sbOK_Click(object sender, EventArgs e)
        {
            //WM_GUI wmGui = new WM_GUI(this.teId.Text, this.tePasswd.Text);
            //wmGui.ShowDialog(this);
            if (this.teId.Text == "")
            {
                MessageBox.Show("아이디를 입력하세요");
                return;
            }
            if (this.tePasswd.Text == "")
            {
                MessageBox.Show("비밀번호를 입력하세요");
                return;
            }

            _userId = this.teId.Text;
            _userPasswd = Encrypt.Password(_userId, this.tePasswd.Text);

            if (!DBConnect.SendUserCheckRequest(_userId, _userPasswd))
            {
                MessageBox.Show("아이디 또는 비밀번호가 올바르지 않습니다.");
                return;
            }

            

            string local_path = System.Windows.Forms.Application.StartupPath + @"\login.ini";

            if (this.ceAutoLogin.Checked)
            {
                //ini["LOGIN"]["AUTO"] = "TRUE";
                WritePrivateProfileString("LOGIN", "AUTO", "true", local_path);
            }
            else
            {
                WritePrivateProfileString("LOGIN", "AUTO", "false", local_path);
            }
            //ini["LOGIN"]["ID"] = _userId;
            //ini["LOGIN"]["PW"] = _userPasswd;
            WritePrivateProfileString("LOGIN", "ID", _userId, local_path);
            WritePrivateProfileString("LOGIN", "PW", _userPasswd, local_path);

            //ini.Save(_local_path);

            DialogResult = DialogResult.OK;

        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void tePasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
                sbOK_Click(sender, new EventArgs());
        }

        private void usrControlLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
                sbOK_Click(sender, new EventArgs());
        }

        private void teId_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
                sbOK_Click(sender, new EventArgs());
        }
    }
}