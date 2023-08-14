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
    public partial class dlgComponentPOW : DevExpress.XtraEditors.XtraForm
    {
        public DataRowView _currentRow { get; private set; }
        public string _powCat { get; private set; }
        public string _powType { get; private set; }
        public string _powClass { get; private set; }

        public dlgComponentPOW(string componentCd)
        {
            InitializeComponent();
            usrComponentPOW1._componentCd = componentCd;
            usrComponentPOW1.setinitialize();
        }
        private void dlgCreateAll_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            usrComponentPOW1.getComponentAll();
        }


        private void sbSelect_Click(object sender, EventArgs e)
        {
            _currentRow = usrComponentPOW1._currentRow;

            _powCat = usrComponentPOW1._powCat;
            _powType = usrComponentPOW1._powType;
            _powClass = usrComponentPOW1._powClass;
            this.DialogResult = DialogResult.OK;
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}