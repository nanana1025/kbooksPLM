using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHousingMaster.view.common.custom;

namespace WareHousingMaster.view.common
{
    static class Dangol
    {
        static public void Message(string msg)
        {
            MessageBox.Show(msg, "dangol365ERP");
        }

        static public void Message(object msg)
        {
            MessageBox.Show(ConvertUtil.ToString(msg), "dangol365ERP");
        }

        static public void Info(object msg)
        {
            MessageBox.Show(ConvertUtil.ToString(msg), "dangol365ERP");
        }

        static public void Warining(object msg)
        {
            MessageBox.Show(ConvertUtil.ToString(msg), "dangol365ERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public void Error(object msg)
        {
            MessageBox.Show(ConvertUtil.ToString(msg), "dangol365ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public DialogResult Custom(object msg)
        {
            using (CustomMessageBox messageBox = new CustomMessageBox(ConvertUtil.ToString(msg)))
            {
                DialogResult result = messageBox.ShowDialog();
                return result;
            }
        }

        static public DialogResult MessageYN(string msg, string title=null)
        {
            if(title == null)
                title = "dangol365 PLM";
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
        }

        static public void ShowSplash(string title = "DANGOL365 PLM", string content = "Wait...")
        {
            if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible) { }
            else SplashScreenManager.CloseForm();

            SplashScreenManager.ShowForm(typeof(WareHousingMaster.Helper.DlgWaitForm));

            SplashScreenManager.Default.SetWaitFormCaption(title);
            SplashScreenManager.Default.SetWaitFormDescription(content);
        }

        static public void setSplashContent(string title = "DANGOL365 PLM", string content = "Wait...")
        {
            SplashScreenManager.Default.SetWaitFormCaption(title);
            SplashScreenManager.Default.SetWaitFormDescription(content);
        }

        //static public void closeSplash()
        //{
        //    if(SplashScreenManager.Default!= null)
        //        SplashScreenManager.CloseForm();
        //}
        static public void CloseSplash()
        {
            if (SplashScreenManager.Default != null)
                SplashScreenManager.CloseForm();
        }

        static public void setDlgPositionCenter(XtraForm form, XtraForm dlgFrom)
        {
            dlgFrom.StartPosition = FormStartPosition.Manual;
            dlgFrom.Location = new Point(form.Location.X + (form.Size.Width / 2) - (dlgFrom.Size.Width / 2),
            form.Location.Y + (form.Size.Height / 2) - (dlgFrom.Size.Height / 2));
        }
        //Dangol.ShowSplash();
        //Dangol.CloseSplash();


    }
}
