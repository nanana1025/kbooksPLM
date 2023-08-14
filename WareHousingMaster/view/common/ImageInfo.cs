using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraEditors;
using ScreenCopy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousingMaster.UtilTest;
using WareHousingMaster.view.usedPurchase;

namespace WareHousingMaster.view.common
{
    static class ImageInfo
    {
        public static void GetImage(int type, bool isProduct, string barcode)
        { 
            string[] fileNames = null;
            Image[] images = ScreenCapture.GetImgFromDangol(type, isProduct, barcode, ref fileNames);

            if (images == null)
            {
                Dangol.Message("저장된 사진이 없습니다.");
            }
            else
            {
                using (DlgImgArray digImgTest = new DlgImgArray(images))
                {
                    digImgTest.ShowDialog();
                    ScreenCapture.DeleteDownLoadedLocalImage(fileNames);
                }
            }
        }
    }
}
