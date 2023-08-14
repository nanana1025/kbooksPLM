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
    public partial class dlgComponentTBL : DevExpress.XtraEditors.XtraForm
    {
        public DataRowView _currentRow { get; private set; }
        public string _tblManufacture { get; private set; }
        public string _tblModelNm { get; private set; }
        public string _tblCapacity { get; private set; }

        public dlgComponentTBL(string componentCd)
        {
            InitializeComponent();
            usrComponentTBL1._componentCd = componentCd;
            usrComponentTBL1.setinitialize();
        }
        private void dlgCreateAll_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            usrComponentTBL1.getComponentAll();
            usrComponentTBL1.setColumnHide();
        }


        private void sbSelect_Click(object sender, EventArgs e)
        {
            _currentRow = usrComponentTBL1._currentRow;

            _tblManufacture = usrComponentTBL1._tblManufacture;
            _tblModelNm = usrComponentTBL1._tblModelNm;
            _tblCapacity = usrComponentTBL1._tblCapacity;
            this.DialogResult = DialogResult.OK;
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}