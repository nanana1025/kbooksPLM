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
    static class DBConsigned
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

        static public string SendRequestMessageForm(StringBuilder postParams, string url)
        {

            string urlPath = url;
            string resultMsg = "";

            
            try
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = encoding.GetBytes(postParams.ToString());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);

                request.Method = "POST";
                request.ContentLength = result.Length;
                request.ContentType = "application/x-www-form-urlencoded";

                // Get the request stream.  
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(result, 0, result.Length);
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
        }

        static public bool getConsignedModelList(long companyId, string componentCd, ref JObject jResult)
        {
            
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getModelList.json";

                jobj.Add("COMPANY_ID", companyId);
                jobj.Add("COMPONENT_CD", componentCd);          
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

        static public bool getConsignedComponentList(long companyId, string componentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedComponentInfo.json";

                jobj.Add("COMPANY_ID", companyId);
                jobj.Add("COMPONENT_CD", componentCd);
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

        static public bool getConsignedList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getConsignedAllList.json";

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

        static public bool searchReceiptList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/searchReceiptList.json";

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

        static public bool getReceiptListFull(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getReceiptListFull.json";

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

        static public bool getReceiptInvoiceState(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getReceiptInvoiceState.json";

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

        static public bool getReceiptPartList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getReceiptPartList.json";

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

        


        static public bool searchReceiptListResult(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/searchReceiptListResult.json";

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

        static public bool assignReceipt(List<long> listProxy, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/assignReceipt.json";

                jobj.Add("LIST_PROXY_ID", string.Join(",", listProxy));
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

        static public bool unAssignReceipt(List<long> listProxy, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/unAssignReceipt.json";

                jobj.Add("LIST_PROXY_ID", string.Join(",", listProxy));
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

        

        static public bool updateInvoice(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateInvoice.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("USER_TYPE", ProjectInfo._userType);

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

        static public bool updateInvoiceOne(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateInvoiceOne.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("USER_TYPE", ProjectInfo._userType);

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


        static public bool updateReceiptStatus(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateReceiptStatus.json";

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

        static public bool consignedReleaseComplete(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/consignedReleaseComplete.json";

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
        
        static public bool preRelease(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/preRelease.json";

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
        

        static public bool ConsignedOneclickRelease(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/releaseProxy.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

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

        static public bool consignedUpdateDetail(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/consignedUpdateDetail.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

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


        static public bool getConsignedInfo(object proxyId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedInfo.json";

                jobj.Add("PROXY_ID",ConvertUtil.ToInt64(proxyId));
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
        static public bool getConsignedReturnInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getConsignedInfo.json";
                
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

        static public bool getConsignedReceiptPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getConsignedReceiptPart.json";
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

        

        static public bool getLicenceInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getLicenceInfo.json";

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

        static public bool updateAdjust(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateAdjust.json";

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

        static public bool getAdjustmentInfo(object proxyId, object companyId, object adjustmentType, object typeId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedAdjustInfo.json";

                jobj.Add("PROXY_ID", ConvertUtil.ToInt64(proxyId));
                jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(companyId));
                jobj.Add("ADJUSTMENT_TYPE", ConvertUtil.ToInt64(adjustmentType));
                jobj.Add("TYPE_ID", ConvertUtil.ToInt64(typeId));

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

        static public bool getConsignedModelComponent(object partListId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getModelListComponent.json";

                jobj.Add("MODEL_LIST_ID", ConvertUtil.ToInt64(partListId));
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

        static public bool createReceipt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/createReceipt.json";

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

        static public bool updateReceipt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateReceipt.json";

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

        static public bool updateReceiptSimple(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateReceiptSimple.json";

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

        static public bool getReceiptModelPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getReceiptModelPart.json";

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

        static public bool getReceiptModel(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getReceiptModel.json";

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
        

        static public bool getReceiptComponentListByExcel(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getReceiptComponentListByExcel.json";

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


        static public bool getConsignedComponentCdList(long _proxyPartId, long companyId, string _componentCd, int _consignedType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedComponentCdListForAssign.json";

                jobj.Add("PROXY_PART_ID", _proxyPartId);
                jobj.Add("COMPANY_ID", companyId);
                jobj.Add("COMPONENT_CD", _componentCd);
                jobj.Add("CONSIGNED_TYPE", _consignedType);
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

        static public bool assignSelectComponent(object componentId, long _proxyPartId, long companyId, string _componentCd, int _consignedType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/assignSelectComponent.json";

                jobj.Add("COMPONENT_ID",ConvertUtil.ToInt64(componentId));
                jobj.Add("PROXY_PART_ID", _proxyPartId);
                jobj.Add("COMPANY_ID", companyId);
                jobj.Add("COMPONENT_CD", _componentCd);
                jobj.Add("CONSIGNED_TYPE", _consignedType);
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

        static public bool getConsignedReceiptStatistics(ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedReceiptStatistics.json";

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

        static public bool getConsignedPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getConsignedPart.json";

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

        static public bool updateReceiptPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateReceiptPart.json";

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

        static public bool updateProxyComponentUnit(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateProxyComponentUnit.json";

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

        static public bool updateReturnReason(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateReturnReason.json";

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

        static public bool updateReleaseReturn(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateReleaseReturn.json";

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

        static public bool assignConsignedReleaseInventory(JObject jobj, string url, ref JObject jResult)
        {
            try
            {
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

        static public bool cancelReleaseInventory(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/cancelReleaseInventory.json";

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

        static public bool returnReceipt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/returnReceipt.json";

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

        static public bool receiptPartRelease(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/receiptPartRelease.json";

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
       
        

        static public bool getReceiptModelStatistics(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getReceiptModelStatistics.json";

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

        static public bool updateLicenceInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateLicenceInfo.json";

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

        static public bool updateLicenceUseInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateLicenceUseInfo.json";

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

        static public bool updateCompanyComponentInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateCompanyComponentInfo.json";

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

        static public bool getCompanyInventoryListByWarehousing(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getCompanyInventoryListByWarehousing.json";

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

        static public bool getCompanyWarehousingInventoryList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getCompanyWarehousingInventoryList.json";

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
        

        static public bool getCompanyInventoryUsedList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getCompanyInventoryUsedList.json";

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
        static public bool getCompanyInventoryReturnList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getCompanyInventoryReturnList.json";

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
        
        static public bool getCompanyModelList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getCompanyModelList.json";

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

        static public bool getModelInventoryList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getModelInventoryList.json";

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

        static public bool updateCompanyModelInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateCompanyModelInfo.json";

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

        static public bool createCompanyModelInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/createCompanyModelInfo.json";

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

        static public bool creteCompanyModelComponent(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/creteCompanyModelComponent.json";

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

        static public bool updateCompanyModelComponent(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateCompanyModelComponent.json";

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

        static public bool deleteCompanyModelComponent(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/deleteCompanyModelComponent.json";

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

        static public bool getCompanyPreInventoryList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getCompanyPreInventoryList.json";

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

        static public bool updateModelInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateModelInfo.json";

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

        static public bool updateModelPartInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/updateModelPartInfo.json";

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

        static public bool getCheckInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getCheckInfo.json";

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

        static public bool insertCheckInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/insertCheckInfo.json";

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
