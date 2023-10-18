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
    static class DBConnect
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
        static public bool execute(string sql, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/execute.json";
                jobj.Add("QUERY", sql);

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

            return true;
        }

        static public bool queryDT(string sql, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/queryDT.json";
                jobj.Add("QUERY", sql);

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

            return true;
        }


        static public bool SendPartInfo(string msg, StringBuilder sb)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/log/getInventoryLog.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("TYPE", 1);
                jobj.Add("MSG", msg);
                jobj.Add("CONTENT", sb.ToString());

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// version 정보 확인 요청
        static public Dictionary<string, string> getVersion(string id)
        {
            Dictionary<string, string> dicVersion = new Dictionary<string, string>();

            string url = "/GetVersion.json";

            JObject jobj = new JObject();

            jobj.Add("USER_ID", id);

            try
            {
                string result = SendRequestMessage(jobj, url);

                jobj = JObject.Parse(result);
                string version = ConvertUtil.ToString(jobj["VERSION"]);
                string content = ConvertUtil.ToString(jobj["CONTENT"]);

                dicVersion.Add("VERSION", version);
                dicVersion.Add("CONTENT", content);

                dicVersion.Add("EXTERNAL_CHECK", ConvertUtil.ToString(jobj["EXTERNAL_CHECK"]));
                dicVersion.Add("EXIST_VERSION_USE_YN", ConvertUtil.ToString(jobj["EXIST_VERSION_USE_YN"]));

                return dicVersion;
            }
            catch
            {
                return dicVersion;
            }
        }

        /// <summary>
        /// login 정보 확인 요청
        /// </summary>
        /// <param name="id">ini에서 가져온 사용자 아이디</param>
        /// <param name="pw">사용자 비밀번호</param>
        /// <returns></returns>
        static public bool SendUserCheckRequest(string id, string pw)
        {
            string url = "/login/login.json";

            JObject jobj = new JObject();

            jobj.Add("USER_ID", id);
            jobj.Add("PASSWD", pw);

            try
            {
                string result = SendRequestMessage(jobj, url);

                jobj = JObject.Parse(result);
                if (!Convert.ToBoolean(jobj["SUCCESS"]))
                {
                    return false;
                }

                ProjectInfo._userName = ConvertUtil.ToString(jobj["USER_NM"]);
                //ProjectInfo._userCompanyId = ConvertUtil.ToInt64(jobj["COMPANY_ID"]);
                ProjectInfo._userType = ConvertUtil.ToString(jobj["USER_TYPE"]);
                //ProjectInfo._userTeamCd = ConvertUtil.ToString(jobj["TEAM_CD"]);
                //ProjectInfo._userPosition = ConvertUtil.ToString(jobj["POSITION"]); 
            }
            catch
            {
                return false;
            }

            return true;
        }


        
        static public bool getVisibleCol(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/common/getVisibleCol.json";

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

        static public bool updateVisibleCol(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/common/updateVisibleCol.json";

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





        static public bool InsertInventory(JObject jPartInfo, ref JObject jResult)
        {
            try
            {
                string url = "/compInven/insertInventory.json";

                jPartInfo.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jPartInfo, url);

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

        static public bool copyInventory(JObject jPartInfo, ref JObject jResult)
        {
            try
            {
                string url = "/compInven/InventoryCopy.json";

                jPartInfo.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jPartInfo, url);

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





        static public bool getRequest(JObject jobj, ref JObject jResult, string url)
        {
            try
            {
                if (jobj.ContainsKey("USER_ID"))
                    jobj.Remove("USER_ID");
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

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
