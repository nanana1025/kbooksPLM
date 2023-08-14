using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace Utils
{
    static class CaptureUtils
    {
        // 사용예: ScreenCopy.Copy("test.png");
        // 
        public static Image capture(Form form, ref string resultFileData)
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
            catch (Exception ex)
            {

                screen = arrScreen[0];
            }

            if (screen == null)
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
            bmp.Save(resultFileData);

            var codeBitmap = new Bitmap(bmp);
            Image image = (Image)codeBitmap;

            return image;
        }

        public static Image saveFile(string directory, string fileNm, string outputFilename)
        {
            try
            {


                System.Net.WebClient wcClient = new System.Net.WebClient();

#if DEBUG
                wcClient.UploadFile($"{ProjectInfo._url}/imageBrowser/uploadCapture?path={directory}&fileNm={fileNm}", "POST", outputFilename);
                Image image = GetCheckImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}\\{fileNm}", true);
                //wcClient.UploadFile($"{ProjectInfo._url}/imageBrowser/uploadCapture?path={directory}&fileNm={fileNm}", "POST", outputFilename);
                //Image image = GetCheckImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}/{fileNm}", true);
#else
                wcClient.UploadFile($"{ProjectInfo._url}/imageBrowser/uploadCapture?path={directory}&fileNm={fileNm}", "POST", outputFilename);
                Image image = GetCheckImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}/{fileNm}", true);
#endif

                return image;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static Image getCaptureFile(long fileId)
        {
            try
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("FILE_ID", fileId);

                if (DBInventory.getFileInfo(jobj, ref jResult))
                {
                    string typeDir = ConvertUtil.ToString(jResult["TYPE_DIR"]);
                    string fileDir = ConvertUtil.ToString(jResult["DIR"]);
                    string fileNm = fileId + ".png";
#if DEBUG
                    //string filePath = $"{typeDir}\\{fileDir}\\{fileNm}";
                    string filePath = $"{typeDir}/{fileDir}/{fileNm}";
#else
                    string filePath = $"{typeDir}/{fileDir}/{fileNm}";
#endif
                    Image image = GetCheckImage($"{ProjectInfo._url}/imageBrowser/image?path={filePath}", true);

                    return image;
                }
                else
                {
                    Dangol.Warining("캡쳐 이미지가 없습니다.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

//        public static Image getCaptureFile(string fileDir, long fileId)
//        {
//            try
//            {
//                string fileNm = fileId + ".png";
//#if DEBUG
//                string filePath = $"{ProjectInfo._dicCaptureDirNm[(int)Enum.EnumCaptureType.QC]}\\{fileDir}\\{fileNm}";
//#else
//                string filePath = $"{ProjectInfo._dicCaptureDirNm[(int)Enum.EnumCaptureType.QC]}/{fileDir}";
//#endif
//                Image image = GetCheckImage($"{ProjectInfo._url}/imageBrowser/image?path={filePath}", true);

//                return image;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }




        static private Image GetCheckImage(string url, bool isShowMsg = false)
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
            catch (Exception e)
            {
                return null;
            }
        }




        public static Image[] GetImgFromDangol(int type, bool isProduct, string barcode, ref string[] filePaths, int releaseType = 1)
        {
            JObject jobj = new JObject();
            JObject jResult = new JObject();


            jobj.Add("BARCODE_TYPE", type);
            if (type == 1)
            {
                if (isProduct)
                    jobj.Add("DEVICE_GROUP", "Y");
                else
                    jobj.Add("DEVICE_GROUP", "N");
            }
            else if (type == 3)
            {
                if (releaseType == 1)
                    jobj.Add("DEVICE_GROUP", "R");
                else
                    jobj.Add("DEVICE_GROUP", "T");
            }


            jobj.Add("BARCODE_NUM", barcode);

            if (DBInventory.getImgListInfo(jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    string path;
                    string fullPath;
                    string[] arrStr;
                    int cnt = ConvertUtil.ToInt32(jResult["CNT"]);
                    int index = 0;
                    Image[] imgs = new Image[cnt];
                    Image img;
                    filePaths = new string[cnt];

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        path = ConvertUtil.ToString(obj["BARCODE_PHOTO"]);
                        arrStr = path.Split('/');

                        if (arrStr.Length > 0)
                            filePaths[index] = arrStr[arrStr.Length - 1];
                        else
                            filePaths[index] = "shlee.shlee";

                        if (path.Contains("https"))
                            fullPath = path;
                        else
                            fullPath = $"{ProjectInfo._asisUrl}{path}";

                        img = GetUrlImage($"{fullPath}");
                        imgs[index++] = img;


                    }

                    return imgs;
                }
                else
                {
                    //Dangol.Warining("이미지가 없습니다.");
                    return null;
                }
            }
            else
            {
                //Dangol.Warining(ConvertUtil.ToString(jResult["MSG"]));
                return null;
            }
        }

        public static Image[] GetProductImgFromDangol(int type, bool isProduct, long inventoryId, ref string[] filePaths, string pictureType, ref Dictionary<int, Image> dicImage, ref Dictionary<int, string> dicPlace, string picturePlace)
        {
            JObject jobj = new JObject();
            JObject jResult = new JObject();

            jobj.Add("BARCODE_TYPE", type);
            if (type == 1 && isProduct)
                jobj.Add("DEVICE_GROUP", "Y");
            else
                jobj.Add("DEVICE_GROUP", "N");

            jobj.Add("PHOTO_CATE", pictureType);
            jobj.Add("RETURN_ID", inventoryId);

            if (!picturePlace.Equals("-1"))
                jobj.Add("PHOTO_TYPE", picturePlace);

            if (DBInventory.getImgListInfo(jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    string path;
                    string fullPath;
                    string[] arrStr;
                    int cnt = ConvertUtil.ToInt32(jResult["CNT"]);
                    int index = 0;
                    string place;
                    Image[] imgs = new Image[cnt];

                    filePaths = new string[cnt];

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        path = ConvertUtil.ToString(obj["BARCODE_PHOTO"]);
                        place = ConvertUtil.ToString(obj["PHOTO_TYPE"]);
                        arrStr = path.Split('/');

                        if (arrStr.Length > 0)
                            filePaths[index] = arrStr[arrStr.Length - 1];
                        else
                            filePaths[index] = "shlee.shlee";

                        if (path.Contains("https"))
                            fullPath = path;
                        else
                            fullPath = $"{ProjectInfo._asisUrl}{path}";

                        Image img = GetUrlImage($"{fullPath}");
                        imgs[index] = img;

                        dicImage.Add(index, img);
                        dicPlace.Add(index, place);
                        index++;
                    }

                    return imgs;
                }
                else
                {
                    //Dangol.Warining("이미지가 없습니다.");
                    return null;
                }
            }
            else
            {
                //Dangol.Warining(ConvertUtil.ToString(jResult["MSG"]));
                return null;
            }


        }

        public static Image GetCaptureImg(string directory, string outputFilename)
        {
#if DEBUG
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}\\{outputFilename}");
#else
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}/{outputFilename}");
#endif
            return image;
        }

        public static Image GetQCImg(string directory, string outputFilename)
        {
            bool isExist = false; ;
#if DEBUG
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}\\{outputFilename}", ref isExist);
#else
            Image image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={directory}/{outputFilename}", ref isExist);
#endif


            if (!isExist)
            {
#if DEBUG
                image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={"O210205001"}\\{outputFilename}", ref isExist);
                //image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={"O210205001"}/{outputFilename}", ref isExist);
#else
                image = GetUrlImage($"{ProjectInfo._url}/imageBrowser/image?path={"O210205001"}/{outputFilename}", ref isExist);
#endif
            }


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
            catch (Exception e)
            {
                if (isShowMsg)
                    Dangol.Error("이미지를 찾을수 없습니다.");
                return Image.FromFile($"{System.Windows.Forms.Application.StartupPath}\\imgerror.png");
            }
        }

        static private Image GetUrlImage(string url, ref bool isExist, bool isShowMsg = false)
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
                        isExist = true;
                        return img;
                    }
                }
            }
            catch (Exception e)
            {
                if (isShowMsg)
                    Dangol.Error("이미지를 찾을수 없습니다.");
                isExist = false;
                return Image.FromFile($"{System.Windows.Forms.Application.StartupPath}\\imgerror.png");
            }
        }






    }
}