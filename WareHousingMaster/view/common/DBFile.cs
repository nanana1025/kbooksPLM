
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WareHousingMaster.view.common
{
    static class DBFile
    {
        static public bool downloadAttachedFile(object fileId, object FileNm)
        {
            try
            {
                string FilepathUrl = $"{ProjectInfo._url}/downloadFileByFileId/{fileId}?fileType=kor";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\";
                saveFileDialog.FileName = $"{FileNm}"; //초기 파일명을 지정할 때 사용한다.
                saveFileDialog.Filter = "All Files|*.*";
                saveFileDialog.Title = "Save Download File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Dangol.ShowSplash("DANGOL365 PLM", "download..");
                    DownLoad(saveFileDialog.FileName, FilepathUrl);
                    Dangol.CloseSplash();
                    return true;
                }
                else
                    return false;


            }
            catch (Exception ex)
            {
                return false;
            }
        }


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

        static public JObject uploadAttachedFile(int category, long id, string dir, string filePath)
        {
            try
            {

                JObject jResult = new JObject();
                JObject jobj = new JObject();
                string url = "/getAttachedFileUploadInfo.json";

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {

                    string fileDir = ConvertUtil.ToString(jResult["FILE_DIR"]);
                    long fileId = ConvertUtil.ToInt64(jResult["FILE_ID"]);

                    string FileNm = Path.GetFileName(ConvertUtil.ToString(filePath));
                    string extension = Path.GetExtension(FileNm).Replace(".", "").ToLower();
#if DEBUG

                    string fullPath = $"attachedFile\\{dir}\\{id}\\{fileDir}";
                    string saveDirPath = $"{id}\\{fileDir}";
                    string saveFilePath = $"{fullPath}\\{FileNm}";
                    string typeDirFull = $"attachedFile\\{dir}";
#else
                    string fullPath = $"attachedFile/{dir}/{id}/{fileDir}";
                    string saveDirPath = $"{id}/{fileDir}";
                    string saveFilePath = $"{fullPath}/{FileNm}";
                    string typeDirFull = $"attachedFile/{dir}";
#endif
                    if (saveFile(fullPath, FileNm, filePath))
                    {
#if DEBUG
                        fullPath = $"attachedFile\\\\{dir}\\\\{id}\\\\{fileDir}";
                        saveDirPath = $"{id}\\\\{fileDir}";
                        saveFilePath = $"{fullPath}\\\\{FileNm}";
                        typeDirFull = $"attachedFile\\\\{dir}";
#endif
                        jobj.RemoveAll();
                        jobj.Add("FILE_ID", fileId);
                        jobj.Add("CATEGORY", category);
                        jobj.Add("ID", id);
                        jobj.Add("EXTENSION", extension);
                        jobj.Add("TYPE_DIR", typeDirFull);
                        jobj.Add("DIR", saveDirPath);
                        jobj.Add("FILE_NM", FileNm);
                        jobj.Add("PATH", saveFilePath);


                        string query = $@"INSERT INTO TN_ATTACHED_FILE
(FILE_ID, CATEGORY, ID, EXTENSION, TYPE_DIR, DIR, FILE_NM, PATH, USE_YN, CREATE_USER_ID, CREATE_DT) 
VALUES ({jobj["FILE_ID"]},{jobj["CATEGORY"]},{jobj["ID"]},'{jobj["EXTENSION"]}','{jobj["TYPE_DIR"]}','{jobj["DIR"]}','{jobj["FILE_NM"]}','{jobj["PATH"]}',1,'{ProjectInfo._userId}',NOW())";

                        JObject jData = new JObject();

                        url = "/common/execute.json";

                        jData.Add("QUERY", query);//1:일반, 2: 생산대행

                        //if (insertNewFileInfo(jobj))
                        if (DBConnect.getRequest(jData, ref jResult, url))
                            return jobj;
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool saveFile(string savePath, string fileNm, string FilePath)
        {
            try
            {
                System.Net.WebClient wcClient = new System.Net.WebClient();
                wcClient.UploadFile($"{ProjectInfo._url}/file/uploadFile?path={savePath}&fileNm={fileNm}", "POST", FilePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



    }
}
