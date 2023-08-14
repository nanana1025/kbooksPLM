using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using System;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSideCheck : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ProcessHandler(int F);
        public event ProcessHandler processHandler;

        SimpleButton[] _arrSb;

        public usrSideCheck()
        {
            InitializeComponent();

            _arrSb = new SimpleButton[] { sbF1, sbF2, sbF3, sbF4, sbF5, sbF6, sbF7, sbF8, sbF9, sbF10 };

        }

        public void setInitLoad(string[] arrrF, bool[] arrFEditable)
        {
            foreach(SimpleButton sb in _arrSb)
                sb.Click += new System.EventHandler(this.sbF_Click);

            setinitialize(arrrF, arrFEditable);
        }


        private void setinitialize(string[] arrrF, bool[] arrFEditable)
        {
            for (int i = 0; i < _arrSb.Length; i++)
            {
                _arrSb[i].Text = arrrF[i];
                _arrSb[i].Enabled = arrFEditable[i];
            }
        }

        private void sbF_Click(object sender, EventArgs e)
        {
            SimpleButton sb = (SimpleButton)sender;
            int F = sb.TabIndex;

            processHandler(F);
        }
    }


}
