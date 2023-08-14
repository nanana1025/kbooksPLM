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
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using System.Drawing.Printing;
using System.Management;
using OpenCvSharp;

namespace WareHousingMaster.Utils
{
    public partial class DlgLoginCheck : DevExpress.XtraEditors.XtraForm
    {

        public int _result { get; private set; }

        public DlgLoginCheck(string msg)
        {
            InitializeComponent();

            lcMsg.Text = msg;
            _result = 3;
        }

        private void DlgCameraTest_Load(object sender, EventArgs e)
        {
            
        }

        private void sbBatch_Click(object sender, EventArgs e)
        {
            _result = 1;
            this.DialogResult = DialogResult.OK;
        }

        private void sbSetup_Click(object sender, EventArgs e)
        {
            _result = 1;
            this.DialogResult = DialogResult.OK;
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            _result = 3;
            this.DialogResult = DialogResult.OK;
        }
    }
}