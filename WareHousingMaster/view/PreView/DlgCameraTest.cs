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

namespace WareHousingMaster.view.PreView
{
    public partial class DlgCameraTest : DevExpress.XtraEditors.XtraForm
    {

        CvCapture capture;
        IplImage src;

        public DlgCameraTest()
        {
            InitializeComponent();
        }

        private void DlgCameraTest_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ProjectInfo.ProjectIcon;


                capture = CvCapture.FromCamera(CaptureDevice.DShow, 0);
                capture.SetCaptureProperty(CaptureProperty.FrameWidth, 640);
                capture.SetCaptureProperty(CaptureProperty.FrameHeight, 480);

            }
            catch(Exception ex)
            {
                //Console.Write(ex.Message);
                timer1.Enabled = false;
            }
            finally
            {
               
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            src = capture.QueryFrame();
            pictureBoxIpl1.ImageIpl = src;
        }

        private void DlgCameraTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cv.ReleaseImage(src);
            if (src != null) src.Dispose();
        }
    }
}