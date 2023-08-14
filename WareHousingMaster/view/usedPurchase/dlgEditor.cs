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

namespace WareHousingMaster.view.usedPurchase
{
    public partial class dlgEditor : DevExpress.XtraEditors.XtraForm
    {
        string _title;
        public string _text { get; private set; }



        public int Cnt { get; private set; }

        public dlgEditor(string title, string text)
        {
            InitializeComponent();

            _title = title;
            _text = text;

        }
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            this.Text = _title;
            meText.Text = _text;

        }

        private void sbCustom_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        

        private void sbInsert_Click(object sender, EventArgs e)
        {
            
            _text = meText.Text;
            this.DialogResult = DialogResult.OK;
        }


    }
}