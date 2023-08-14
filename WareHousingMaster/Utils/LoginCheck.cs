using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WareHousingMaster.Utils;
using WareHousingMaster.view.common;

namespace ScreenCopy
{
    static class LoginCheck
    {


        static public void checkVersion(Version version, string latestVersion, int existVersionUseYn, string content)
        {
                if (!ProjectInfo._version.Equals(latestVersion))
                {
                    string msg = $@"프로그램이 업데이트 되었습니다. 최신 버전을 다운로드 하세요.
[ 최신버전: {version} ]
[ 업데이트 내용: {content}]";
                  
                    int result;

                    using (DlgLoginCheck loginCheck = new DlgLoginCheck(msg))
                    {
                        loginCheck.ShowDialog();

                        result = loginCheck._result;
                    }

                    if (result == 1)
                    {
                        string FilepathUrl = $"{ProjectInfo._url}/downloadInventoryCheckFile/WareHousingMaster.zip?fileType=kor";

                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.InitialDirectory = $"{System.Windows.Forms.Application.StartupPath}\\";
                        //saveFileDialog.InitialDirectory = $"C:\\Users\\USER\\Desktop\\리더스텍\\임시 복사\\";
                        saveFileDialog.FileName = $"WareHousingMaster_{ProjectInfo._version}.zip"; //초기 파일명을 지정할 때 사용한다.
                        saveFileDialog.Filter = "ZIP Folders(.ZIP)| *.zip| All files (*.*)|(*.*)";
                        saveFileDialog.Title = "Save a zip File";
                        saveFileDialog.ShowDialog();

                        if (saveFileDialog.FileName != "")
                        {
                            //DownloadFile(FilepathUrl, saveFileDialog.FileName);
                            Dangol.ShowSplash("DANGOL365 PLM", "다운로드 중입니다. 잠시만 기다려주세요..");
                            //Dangol.setSplashContent("DANGOL365 PLM", "다운로드 중입니다. 잠시만 기다려주세요..");
                            FileDownload.DownLoad(saveFileDialog.FileName, FilepathUrl);
                            Dangol.CloseSplash();

                            Dangol.Message("다운로드 완료! 새로운 프로그램으로 재 시작해 주세요.");
                            Util.ExitProgram();
                        }
                    }
                    else 
                    {
                        Util.ExitProgram();
                    }
                    
                }
            
        }

    }
}