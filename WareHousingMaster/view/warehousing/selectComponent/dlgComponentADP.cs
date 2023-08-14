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
    public partial class dlgComponentADP : DevExpress.XtraEditors.XtraForm
    {
        public DataRowView _currentRow { get; private set; }
        public string _adpType { get; private set; }

        public dlgComponentADP(string componentCd)
        {
            InitializeComponent();
            usrComponentADP1._componentCd = componentCd;
            usrComponentADP1.setinitialize();
        }
        private void dlgCreateAll_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            usrComponentADP1.getComponentAll();
        }


        private void sbSelect_Click(object sender, EventArgs e)
        {
            _currentRow = usrComponentADP1._currentRow;
            _adpType = usrComponentADP1._adpType;
            this.DialogResult = DialogResult.OK;
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}