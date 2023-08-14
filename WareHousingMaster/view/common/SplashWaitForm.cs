using DevExpress.XtraSplashScreen;
using System.Drawing;
using System.Windows.Forms;

namespace WareHousingMaster.view.common
{
    static class SplashWaitForm
    {
        static public Image buttonImage;
        static public Image hotButtonImage;

        static public OverlayTextPainter _overlayLabel;
        static public OverlayImagePainter _overlayButton;


        static public void OnCancelButtonClick()
        {
           
        }

        static public void OnProgressChanged(int value)
        {
            _overlayLabel.Text = value.ToString() + "%";
        }

        static public Image CreateButtonImage()
        {
            return ImageHelper.CreateImage(Properties.Resources.cancel_normal);
        }
        static public Image CreateHotButtonImage()
        {
            return ImageHelper.CreateImage(Properties.Resources.cancel_active);
        }


    }
}
