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
    static class DBStatistics
    {

        static public bool getCheckStatistics(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/statistics/getCheckStatistics.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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

        static public bool getConsignedReturnCheckStatistics(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/statistics/getConsignedReturnCheckStatistics.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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


        


        static public bool createReceipt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/createReceipt.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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

        static public bool searchReceiptList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/searchReceiptList.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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
        static public bool getPartList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getPartList.json";

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
        static public bool getAdjustmentInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getAdjustmentInfo.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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
        static public bool getPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getPart.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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
        static public bool updateReceiptInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateReceiptInfo.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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
        static public bool saveAdjustmentInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/saveAdjustmentInfo.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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
        static public bool getReleaseReceiptStatistics(ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/export/getReleaseReceiptStatistics.json";

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
        static public bool updateReceiptStatus(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateReceiptStatus.json";

                //jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(usedPartId));
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

        static public bool makeExport(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/makeExport.json";

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
