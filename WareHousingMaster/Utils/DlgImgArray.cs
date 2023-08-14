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

namespace WareHousingMaster.UtilTest
{
    public partial class DlgImgArray : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtImage;
        BindingSource _bsImage;
        //CvCapture capture;
        Image[] _image;

        public DlgImgArray(Image[] image)
        {
            InitializeComponent();

            _image = image;

            _dtImage = new DataTable();
            _dtImage.Columns.Add(new DataColumn("IMAGE1", typeof(Image)));
            _dtImage.Columns.Add(new DataColumn("IMAGE2", typeof(Image)));

            _bsImage = new BindingSource();
            _bsImage.DataSource = _dtImage;

            gcImage.DataSource = _bsImage;



        }

        private void DlgCameraTest_Load(object sender, EventArgs e)
        {
            int cnt = _image.Length;

            if(cnt == 1)
            {
                DataRow dr1 = _dtImage.NewRow();
                dr1["IMAGE1"] = _image[0];
                dr1["IMAGE2"] = null;
                _dtImage.Rows.Add(dr1);
            }
            else
            {
                int newCnt = cnt;

                if(cnt%2 == 1)
                    newCnt = cnt - 1;

                for(int i = 0; i < newCnt; i=i+2)
                {
                    DataRow dr1 = _dtImage.NewRow();
                    dr1["IMAGE1"] = _image[i];
                    dr1["IMAGE2"] = _image[i+1];
                    _dtImage.Rows.Add(dr1);
                }

                if (cnt % 2 == 1)
                {
                    DataRow dr1 = _dtImage.NewRow();
                    dr1["IMAGE1"] = _image[cnt-1];
                    dr1["IMAGE2"] = null;
                    _dtImage.Rows.Add(dr1);
                }
            }
        }

    }
}