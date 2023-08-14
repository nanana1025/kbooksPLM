using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace ScreenCopy
{
    static class FileDownload
    {
        // 사용예: ScreenCopy.Copy("test.png");
        // 


        // 서버의 파일을 다운로드 받기
        public static void DownLoad(string path, string fileUrl)
        {
            try
            {
                Dangol.ShowSplash();

                WebClient downloader = new WebClient();
                // fake as if you are a browser making the request.
                downloader.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                //downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(fileDownloader_DownloadFileCompleted);
                //downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(fileDownloader_DownloadProgressChanged);
                downloader.DownloadFileAsync(new Uri(fileUrl), path);
                // wait for the current thread to complete, since the an async action will be on a new thread.
                while (downloader.IsBusy) { }
            }
            catch (Exception ex)
            {
                Dangol.Error("오류 : " + ex.Message);
                return;
            }
        }

        static void fileDownloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
           // string tmpSetupPath = e.UserState.ToString();
            Dangol.Message("다운로드 완료");
            //Process.Start(tmpSetupPath);    // 다운받은 파일을 실행하고 
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;   // 현재 프로그램을 
            //this.Close();

            Dangol.CloseSplash();
        }

        static void fileDownloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dangol.setSplashContent("DANGOL365 PLM", e.ProgressPercentage.ToString());
            //radProgressBar_update.Value1 = e.ProgressPercentage;
        }
    }
}