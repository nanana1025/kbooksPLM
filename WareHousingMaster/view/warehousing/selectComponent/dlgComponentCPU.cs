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

namespace WareHousingMaster.view.warehousing.selectComponent
{
    public partial class dlgComponentCPU : DevExpress.XtraEditors.XtraForm
    {
        public DataRowView _currentRow { get; private set; }
        public string _adpType { get; private set; }

        public dlgComponentCPU(string componentCd)
        {
            InitializeComponent();
            usrComponentCPU1._componentCd = componentCd;
            usrComponentCPU1.setinitialize();
        }
        private void dlgCreateAll_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            usrComponentCPU1.getComponentAll();
        }


        private void sbSelect_Click(object sender, EventArgs e)
        {
            _currentRow = usrComponentCPU1._currentRow;
            //_adpType = usrComponentCPU1._adpType;
            this.DialogResult = DialogResult.OK;
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}