using System;
using System.Windows.Forms;

namespace WareHousingMaster
{

    public partial class LoadingForm : Form
    {
        string version = "";
        public LoadingForm(string version)
        {
            this.version = version;
            InitializeComponent();
        }
                
        private void LoadingForm_Load(object sender, EventArgs e)
        {
            lbl_version_.Text = version;
        }
    }
}
