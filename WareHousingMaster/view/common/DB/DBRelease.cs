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
    static class DBRelease
    {

        static public bool getPartInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getPartInfo.json";

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

        static public bool updateReceiptPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateReceiptPart.json";

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

        static public bool getExportPartInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportPartInfo.json";

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

        static public bool updatePartInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updatePartInfo.json";

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

        static public bool getExportListGroupByArrivalDt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportListGroupByArrivalDt.json";

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

        static public bool getExamList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExamList.json";

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

        static public bool getExportListDetail(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportListDetail.json";

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

        static public bool getExportDataOneDetail(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportDataOneDetail.json";

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

        static public bool updateProductInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateProductInfo.json";

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



        static public bool getExportCheckInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportCheckInfo.json";
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

        static public bool getExportDataInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportDataInfo.json";
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

        

        static public bool getExportStatistics(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportStatistics.json";
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
        
        static public bool getExportCheckStatistics(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportCheckStatistics.json";
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
        static public bool getOrderList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getOrderList.json";
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

        static public bool createOrder(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/createOrder.json";
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

        static public bool deleteOrder(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/deleteOrder.json";
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

        static public bool getExportOrderPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportOrderPart.json";
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

        static public bool updateReleaseOrderParts(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateReleaseOrderParts.json";
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

        static public bool updateReleaseOrderPartsBulk(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateReleaseOrderPartsBulk.json";
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
        

        static public bool deleteReleaseOrderParts(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/deleteReleaseOrderParts.json";
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

        static public bool getExportOrderList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportOrderList.json";
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

        static public bool getReleaseOrderReceiptStatistics(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getReleaseOrderReceiptStatistics.json";
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

        static public bool getOrderPartInfoDetail(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getOrderPartInfoDetail.json";
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
        static public bool getOrderReleasePartInfoDetail(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getOrderReleasePartInfoDetail.json";
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

        

        static public bool deleteOrderReleasePart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/deleteOrderReleasePart.json";
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

        static public bool insertOrderReleasePart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/insertOrderReleasePart.json";
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

        static public bool updateOrderReleaseStatus(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateOrderReleaseStatus.json";
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

        static public bool getUserInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getUserInfo.json";
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

        static public bool createTechnicalReceipt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/createTechnicalReceipt.json";
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

        static public bool updateTechnicalReceipt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateTechnicalReceipt.json";
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

        static public bool getReceiptList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getReceiptList.json";
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

        static public bool getMessageList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getMessageList.json";
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

        static public bool insertMsgReceiverList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/insertMsgReceiverList.json";
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

        static public bool deleteMsgReceiverList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/deleteMsgReceiverList.json";
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

        static public bool updateMsgInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateMsgInfo.json";
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

        static public bool getExportInventoryInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getExportInventoryInfo.json";
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

        static public bool getProductInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getProductInfo.json";
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

        static public bool createInventoryProduce(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/createInventoryProduce.json";
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

        static public bool updateInventoryProduce(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/updateInventoryProduce.json";
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

        

        static public bool getInventoryManagementList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getInventoryManagementList.json";
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
        static public bool getInventoryList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/getInventoryList.json";
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

        static public bool createInventoryInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/createInventoryInfo.json";
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

        static public bool deleteInventoryInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/deleteInventoryInfo.json";
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

        static public bool reTechnicalReceipt(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/export/reTechnicalReceipt.json";
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
