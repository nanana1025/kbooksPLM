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
    public partial class DlgImgTest : DevExpress.XtraEditors.XtraForm
    {

        CvCapture capture;
        IplImage src;

        public DlgImgTest(Image image)
        {
            InitializeComponent();

            pictureEdit1.Image = image;
            
        }

        private void DlgCameraTest_Load(object sender, EventArgs e)
        {
            
        }

    }
}