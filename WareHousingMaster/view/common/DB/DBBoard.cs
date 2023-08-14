using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.common
{
    static class DBBoard
    {

        static public string SendRequestMessage(JObject msg_jobj, string url)
        {

            string urlPath = ProjectInfo._url + url;
            string resultMsg = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(msg_jobj.ToString());
                request.ContentLength = byteArray.Length;
                request.ContentType = "application/json";

                // Get the request stream.  
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                // Get the response.  
                WebResponse response = request.GetResponse();
  
                // Get the stream containing content returned by the server.  
                // The using block ensures the stream is automatically closed.                
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    resultMsg = reader.ReadToEnd();
                }

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "server error");
                Util.ExitProgram();
            }

            return resultMsg;
        }

        static public bool sendMessage(JObject jobj, string url, ref JObject jResult)
        {
            try
            {
                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);

                if (Convert.ToBoolean(jResult["SUCCESS"]))
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }






        

        static public bool searchRequestList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/board/searchRequestList.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }

        static public bool insertRequestList(ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/board/insertRequestList.json";


                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }

        static public bool deleteRequestList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/board/deleteRequestList.json";
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }
        
        static public bool updateRequestList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/board/updateRequestList.json";
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }

        static public bool searchJobList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/board/searchJobList.json";
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }

        static public bool insertJobList(ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/board/insertJobList.json";


                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }

        static public bool deleteJobList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/board/deleteJobList.json";
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }

        static public bool updateJobList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/board/updateJobList.json";
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                jResult.Add("MSG", e.Message);
                return false;
            }
        }





    }
}
