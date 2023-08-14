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

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrControl : DevExpress.XtraEditors.XtraForm
    {


        public int Cnt { get; private set; }

        public usrControl()
        {
            InitializeComponent();


        }
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;     

        }



    }
}