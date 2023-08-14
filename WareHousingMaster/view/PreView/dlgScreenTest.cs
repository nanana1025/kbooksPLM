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

namespace WareHousingMaster.view.PreView
{
    public partial class dlgScreenTest : DevExpress.XtraEditors.XtraForm
    {
        int _index = 0;
        int _maxCnt = 5;
        Color[] _colors;
        
        public dlgScreenTest()
        {
            InitializeComponent();

            _colors = new Color[5] { Color.White, Color.Blue, Color.Red, Color.Green, Color.Black };

        }
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
           
        }

        private void dlgScreenTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                _index++;
                this.BackColor = _colors[_index % _maxCnt];
            }
            else if(e.KeyCode == Keys.Left)
            {
                _index--;
                if (_index == -1)
                    _index = _maxCnt;

                this.BackColor = _colors[_index % _maxCnt];
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}