using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace WareHousingMaster.view.common.custom
{
    public partial class CustomMessageBox : DevExpress.XtraEditors.XtraForm
    {
        public CustomMessageBox()
        {
            InitializeComponent();
            content.Text = "";
        }
        public CustomMessageBox(string title)
        {
            InitializeComponent();

            content.Text = title;
        }

        public CustomMessageBox(string title, string button1, string button2, string button3)
        {
            InitializeComponent();

            content.Text = title;
            sbConfirm.Text = button1;
            sbSave.Text = button2;
            sbCancel.Text = button3;
        }

        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

        }


        private void sbConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
        private void sbSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

       
    }
}