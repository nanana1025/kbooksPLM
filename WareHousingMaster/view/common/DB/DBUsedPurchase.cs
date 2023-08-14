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
    static class DBUsedPurchase
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
                //Util.ExitProgram();
            }

            return resultMsg;
        }

        static public string SendRequestMessageFormByKaKao(StringBuilder postParams, string url)
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
                request.ContentType = "application/json";
                request.Headers.Add("userid", "lta7047582");
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
                //Util.ExitProgram();
            }

            return resultMsg;
        }


        static public string SendRequestMessageFormByKaKao(JArray array, string url)
        {

            string urlPath = url;
            string resultMsg = "";
            string json = array.ToString();

            try
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = encoding.GetBytes(json);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);

                request.Method = "POST";
                request.ContentLength = result.Length;
                request.ContentType = "application/json";
                request.Headers.Add("userid", "lta7047582");
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
                //Util.ExitProgram();
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
        

        static public bool getDanawaReceiptDetail(long receiptId, string receip, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getDanawaReceiptDetail.json";

                jobj.Add("RECEIPT_ID", receiptId);
                jobj.Add("RECEIPT", receip);
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

        static public bool getReceiptDetail(long receiptId, string receip, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getReceiptDetail.json";

                jobj.Add("RECEIPT_ID", receiptId);
                jobj.Add("RECEIPT", receip);
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

        static public bool getStateHistory(long receiptId, string receip, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getStateHistory.json";

                jobj.Add("RECEIPT_ID", receiptId);
                jobj.Add("RECEIPT", receip);
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

        static public bool getDanawaStateHistory(long receiptId, string receip, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getDanawaStateHistory.json";

                jobj.Add("RECEIPT_ID", receiptId);
                jobj.Add("RECEIPT", receip);
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
        

        static public bool getDanawaUsedPartList(object receiptId, object receipt, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getDanawaUsedPartList.json";

                jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(receiptId));
                jobj.Add("RECEIPT", ConvertUtil.ToString(receipt));
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

        static public bool getUsedPartList(object receiptId, object receipt, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPartList.json";

                jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(receiptId));
                jobj.Add("RECEIPT", ConvertUtil.ToString(receipt));
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

        static public bool getDanawaReceiptExaminePartList(object receiptId, object receipt, ref JObject jResult)
        {
            JObject jobj = new JObject();
            string url = "/UsedPurchase/getDanawaReceiptExaminePartList.json";

            jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(receiptId));
            jobj.Add("RECEIPT", ConvertUtil.ToString(receipt));
            jobj.Add("WAREHOUSING", ConvertUtil.ToString(receipt));
            jobj.Add("USER_ID", ProjectInfo._userId);

            // 요청 전송
            return sendMessage(jobj, url, ref jResult);
        }

        static public bool getReceiptExaminePartList(object receiptId, object receipt, object sourceCd, ref JObject jResult)
        {
            JObject jobj = new JObject();
            string url = "/UsedPurchase/getReceiptExaminePartList.json";

            jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(receiptId));
            jobj.Add("RECEIPT", ConvertUtil.ToString(receipt));
            jobj.Add("WAREHOUSING", ConvertUtil.ToString(receipt));
            jobj.Add("SOURCE_CD", ConvertUtil.ToInt32(sourceCd));
            jobj.Add("USER_ID", ProjectInfo._userId);

            // 요청 전송
            return sendMessage(jobj, url, ref jResult);
        }

        static public bool getLTComonent(string componentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getLTComonent.json";

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

        static public bool insertUsedPartComponent(JObject jobj, string path, ref JObject jResult)
        {
            try
            {
               
                string url = $"/UsedPurchase/{path}.json";

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

        static public bool insertUsedPartETCComponent(string receipt, object componentCd, object modelNm, object price, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/insertUsedPartETCComponent.json";

                jobj.Add("RECEIPT", receipt);
                jobj.Add("MODEL_NM", ConvertUtil.ToString(modelNm));
                jobj.Add("COMPONENT_CD", ConvertUtil.ToString(componentCd));
                jobj.Add("PART_PRICE", ConvertUtil.ToInt64(price));
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

        static public bool deleteUsedPartComponent(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = $"/UsedPurchase/deleteUsedPartComponent.json";

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
        


        static public bool updateUsedPartCnt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateUsedPartCnt.json";

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


        static public bool getCounselling(long warehousingId, long _receiptId, int sourceCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getCounselling.json";

                jobj.Add("WAREHOUSING_ID", warehousingId);
                jobj.Add("RECEIPT_ID", _receiptId);
                jobj.Add("SOURCE_CD", sourceCd);
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

        static public bool getResponse(long requestId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getResponse.json";

                jobj.Add("REQUEST_ID", requestId);
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

        static public bool InsertResponse(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/InsertResponse.json";

                
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

        static public bool getCustomerState(string receipt, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getCustomerState.json";

                jobj.Add("WAREHOUSING", receipt);
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

        static public bool updateCustomerState(long warehousingId, string state, int returnState, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/updateCustomerState.json";

                jobj.Add("WAREHOUSING_ID", warehousingId);
                jobj.Add("STATE", state);
                jobj.Add("RETURN_STATE", returnState);
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


        static public bool getUsedPurchaseReleaseInfo(long warehousingId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPurchaseReleaseInfo.json";

                jobj.Add("WAREHOUSING_ID", warehousingId);
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

        static public bool receiptUsedPurchaseReturn(long warehousingId, List<long> listInventoryId, string request, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/receiptUsedPurchaseReturn.json";

                jobj.Add("WAREHOUSING_ID", warehousingId);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                jobj.Add("REQUEST", request);
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


        static public bool receiptUsedPurchaseFaultReturn(long warehousingId, List<long> listFaultId, string request, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/receiptUsedPurchaseFaultReturn.json";

                jobj.Add("WAREHOUSING_ID", warehousingId);
                jobj.Add("LIST_FAULT_ID", string.Join(",", listFaultId));
                jobj.Add("REQUEST", request);
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
            

        static public bool searchUsedPurchaseReceiptList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/searchUsedPurchaseReceiptList.json";

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

        static public bool getUsedPurchaseReceiptCustomerInfo(object customerId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPurchaseReceiptCustomerInfo.json";

                jobj.Add("CUSTOMER_ID", ConvertUtil.ToInt64(customerId));
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
        

        static public bool getUsedPurchaseReceiptStatistics(ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPurchaseReceiptStatistics.json";

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
                string url = "/UsedPurchase/updateAdjust.json";

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

        static public bool getAdjustmentInfo(object releaseId, object companyId, object adjustmentType, object typeId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPurchaseAdjustInfo.json";

                jobj.Add("RELEASE_ID", ConvertUtil.ToInt64(releaseId));
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

        static public bool updateReceiptStatus(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateReceiptStatus.json";

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

        static public bool updateUsedPurchaseDeliveryInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateUsedPurchaseDeliveryInfo.json";

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

        

        static public bool updateUsedPurchaseRelease(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateUsedPurchaseRelease.json";

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

        static public bool updateUsedPurchaseReleaseDeliveryInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateUsedPurchaseReleaseDeliveryInfo.json";

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

        
        static public bool getUsedPurchaseReleasePartInfo(object releaseId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPurchaseReleasePartInfo.json";

                jobj.Add("RELEASE_ID", ConvertUtil.ToInt64(releaseId));
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
        

        static public bool updatePrice(long releasePartId, object price, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/updatePrice.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("RELEASE_PART_ID", releasePartId);
                jobj.Add("PRICE", ConvertUtil.ToInt64(price));

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


        static public bool updateReceiptInfo(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateReceiptInfo.json";

                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        static public bool updateDanawaReceiptInfo(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateDanawaReceiptInfo.json";

                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        

        static public bool getUsedPurchasePaymentList(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/getUsedPurchasePaymentList.json";

                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        static public bool updateDanawaRecetFinalState(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateDanawaRecetFinalState.json";

                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        static public bool updateRecetFinalState(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateRecetFinalState.json";

                jData.Add("USER_ID", ProjectInfo._userId);
                jData.Add("USER_NM", ProjectInfo._userName);
                jData.Add("DEPT_NM", "중고매입상담");

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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
        

        static public bool updateReceiptExamComplete(JObject jData, ref JObject jResult)
        {
            try
            {
                
                string url = "/UsedPurchase/updateReceiptExamComplete.json";

                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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


        static public bool getFaultList(long _receiptId, string _receipt, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getFaultList.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("RECEIPT_ID", _receiptId);
                jobj.Add("WAREHOUSING", _receipt);
                

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
        
        static public bool updateFaultList(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateFaultList.json";
                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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
        static public bool insertFaultList(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/insertFaultList.json";
                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        static public bool deleteFaultList(JObject jData, ref JObject jResult)
        {
            try
            {

                string url = "/UsedPurchase/deleteFaultList.json";

                jData.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        static public bool updateReceiptState(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateReceiptState.json";

                jData.Add("USER_ID", ProjectInfo._userId);
                jData.Add("USER_NM", ProjectInfo._userName);
                jData.Add("DEPT_NM", "중고매입상담");
                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        static public bool updateMsgHistory(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/updateMsgHistory.json";

                jData.Add("USER_ID", ProjectInfo._userId);
                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        static public bool checkExternalRequest(long receiptId, ref JObject jResult)
        {
            try
            {
                JObject jData = new JObject();
                string url = "/UsedPurchase/checkExternalRequest.json";

                jData.Add("RECEIPT_ID", receiptId);
                jData.Add("USER_ID", ProjectInfo._userId);
                // 요청 전송
                string result = SendRequestMessage(jData, url);

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

        



        static public bool updateDanawaReceiptState(StringBuilder postParams)
        {
            try
            {
                string url = "http://cs.asworld.co.kr/ADMIN/Used/CSProductProcessExternal.php";

                postParams.Append($"&USER_ID={ProjectInfo._userId}");

                // 요청 전송
                string result = SendRequestMessageForm(postParams, url);

                // 반환값 파싱
                
               return true;
      
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        //static public bool sendMsgToCustomer(StringBuilder postParams)
        static public bool sendMsgToCustomer(JArray array, ref JObject jResult)
        {
            try
            {
                string url = "https://alimtalk-api.bizmsg.kr/v2/sender/send";

                // 요청 전송
                //string result = SendRequestMessageFormByKaKao(postParams, url);
                string result = SendRequestMessageFormByKaKao(array, url);
                

                JArray jArray = new JArray();
                jArray = JArray.Parse(result);
                // 반환값 파싱
                foreach (JObject obj in jArray.Children<JObject>())
                    jResult = obj;

                if (Convert.ToString(jResult["code"]).Equals("success"))
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

    }
}
