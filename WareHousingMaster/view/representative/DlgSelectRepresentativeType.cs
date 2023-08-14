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

namespace WareHousingMaster.view.representative
{
    public partial class DlgSelectRepresentativeType : DevExpress.XtraEditors.XtraForm
    {
        public string _representativeType { get; private set; }

        public DlgSelectRepresentativeType()
        {
            InitializeComponent();
        }

        private void DlgMonitorCheck_Load(object sender, EventArgs e)
        {
        }

        private void sbWarehousing_Click_1(object sender, EventArgs e)
        {
            _representativeType = "W";
            this.DialogResult = DialogResult.OK;
        }

        private void sbWarehousingConsigned_Click(object sender, EventArgs e)
        {
            _representativeType = "C";
            this.DialogResult = DialogResult.OK;
        }

        private void sbRelease_Click(object sender, EventArgs e)
        {
            _representativeType = "O";
            this.DialogResult = DialogResult.OK;
        }

        private void sbConsigned_Click(object sender, EventArgs e)
        {
            _representativeType = "P";
            this.DialogResult = DialogResult.OK;
        }

       
    }



    
}