using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace ScreenCopy
{
    static class ScreenCapture
    {
        // 사용예: ScreenCopy.Copy("test.png");
        // 
        public static Image Copy(Form form, string directory, string outputFilename)
        {
            Screen[] arrScreen = Screen.AllScreens;
            Screen screen = null;
            Point point = new Point(form.Location.X + 100, form.Location.Y + 100);
            if (arrScreen.Length < 1)
            {
                Dangol.Warining("캡처 대상 스크린을 찾을수 없습니다.");
                return null;
            }

            try
            {
                foreach (Screen sc in arrScreen)
                {
                    if (sc.WorkingArea.Contains(point))
                    {
                        screen = sc;
                        break;
                    }
                }
            }
            catch(Exception ex)
            {

                screen = arrScreen[0];
            }

            if(screen == null)
                screen = arrScreen[0];

            // 주화면의 크기 정보 읽기
            //Rectangle rect = Screen.PrimaryScreen.Bounds;
            Rectangle rect = screen.Bounds;
            // 2nd screen = Screen.AllScreens[1]

            // 픽셀 포맷 정보 얻기 (Optional)
            //int bitsPerPixel = Screen.PrimaryScreen.BitsPerPixel;
            int bitsPerPixel = screen.BitsPerPixel;
            PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            if (bitsPerPixel <= 16)
            {
                pixelFormat = PixelFormat.Format16bppRgb565;
            }
            if (bitsPerPixel == 24)
            {
                pixelFormat = PixelFormat.Format24bppRgb;
            }

            // 화면 크기만큼의 Bitmap 생성
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, pixelFormat);

            // Bitmap 이미지 변경을 위해 Graphics 객체 생성
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                // 화면을 그대로 카피해서 Bitmap 메모리에 저장
                gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
            }

            // Bitmap 데이타를 파일로 저장
            bmp.Save(outputFilename);

            System.Net.WebClient wcClient = new System.Net.WebClient();

#if DEBUG
            wcClient.UploadFile($"{ProjectInfo._url}/imageBrowser/uploadCapture?path={directory}", "POST", outputFilename);
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}\\{outputFilename}", true);
#else
            wcClient.UploadFile($"{ProjectInfo._url}/imageBrowser/uploadCapture?path={directory}", "POST", outputFilename);
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}/{outputFilename}", true);
#endif

            bmp.Dispose();

            return image;
        }


        //public static Image[] GetImgFromDangol(int type, bool isProduct, string barcode, ref string[] filePaths)
        //{
        //    JObject jobj = new JObject();
        //    JObject jResult = new JObject();

        //    jobj.Add("BARCODE_TYPE", type);
        //    if(type == 1 && isProduct)
        //        jobj.Add("DEVICE_GROUP", "Y");
        //    else
        //        jobj.Add("DEVICE_GROUP", "N");
        //    jobj.Add("BARCODE_NUM", barcode);

        //    if (DBInventory.getImgListInfo(jobj, ref jResult))
        //    {
        //        if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
        //        {
        //            string path;
        //            string fullPath;
        //            string[] arrStr;
        //            int cnt = ConvertUtil.ToInt32(jResult["CNT"]);
        //            int index = 0;
        //            Image[] imgs = new Image[cnt];
        //            Image img;
        //            filePaths = new string[cnt];

        //            JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    
        //            foreach (JObject obj in jArray.Children<JObject>())
        //            {
        //                path = ConvertUtil.ToString(obj["BARCODE_PHOTO"]);
        //                arrStr = path.Split('/');

        //                if (arrStr.Length > 0)
        //                    filePaths[index] = arrStr[arrStr.Length - 1];
        //                else
        //                    filePaths[index] = "shlee.shlee";

        //                fullPath = $"{ProjectInfo._asisUrl}{path}";

        //                img = GetUrlImage($"{fullPath}");
        //                imgs[index++] = img;
                        
                       
        //            }

        //            return imgs;
        //        }
        //        else
        //        {
        //            //Dangol.Warining("이미지가 없습니다.");
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        Dangol.Warining(ConvertUtil.ToString(jResult["MSG"]));
        //        return null;
        //    }


        //}

        public static Image GetCaptureImg(string directory, string outputFilename)
        {
#if DEBUG
        
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}\\{outputFilename}");
#else
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}/{outputFilename}");
#endif
            return image;
        }

        public static void DeleteDownLoadedLocalImage(string fileName)
        {
            File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{fileName}");
        }

        public static void DeleteDownLoadedLocalImage(string[] fileNames)
        {
            foreach (string fileName in fileNames)
                File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{fileName}");
        }



        static private Image GetUrlImage(string url, bool isShowMsg = false)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] imgArray;
                    imgArray = client.DownloadData(url);

                    using (MemoryStream memstr = new MemoryStream(imgArray))
                    {
                        Image img = Image.FromStream(memstr);
                        return img;
                    }
                }
            }
            catch(Exception e)
            {
                if(isShowMsg)
                    Dangol.Error("이미지를 찾을수 없습니다.");
                return Image.FromFile($"{System.Windows.Forms.Application.StartupPath}\\imgerror.png");
            }

        }

        


    }
}