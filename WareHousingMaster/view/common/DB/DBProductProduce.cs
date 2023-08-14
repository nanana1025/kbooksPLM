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
    static class DBProductProduce
    {

        static public bool getProduceList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/productProduceService/getProduceList.json";

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

        static public bool getProductListByProduceId(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/productProduceService/getProductListByProduceId.json";

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

        static public bool updateProduceInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/productProduceService/updateProduceInfo.json";

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
        static public bool createProduceInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/productProduceService/createProduceInfo.json";

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

        static public bool deleteProduce(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/productProduceService/deleteProduce.json";

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

        static public bool getProductList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/inventoryCheck/getProduceProductList.json";

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


        static public bool getAdjustNtbData(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/getAdjustNtbDataForProduce.json";

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

        static public bool insertProduce(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/productProduceService/insertProduce.json";

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

        static public bool deleteWarehousingProduce(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/productProduceService/deleteWarehousingProduce.json";

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

        




























        static public bool getProductListByBarcode(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/inventoryCheck/getProductListByBarcode.json";

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
        static public bool getProductListByWarehousingId(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/inventoryCheck/getProductListByWarehousingId.json";

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


        static public bool updateProductInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/updateProductInfo.json";

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

        //static public bool getAdjustNtbData(JObject jobj, ref JObject jResult)
        //{
        //    try
        //    {
        //        string url = "/product/getAdjustNtbData.json";

        //        jobj.Add("USER_ID", ProjectInfo._userId);

        //        // 요청 전송
        //        string result = DBConnect.SendRequestMessage(jobj, url);

        //        // 반환값 파싱
        //        jResult = JObject.Parse(result);
        //        if (Convert.ToBoolean(jResult["SUCCESS"])) // IVT
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //        jResult.Add("MSG", e.Message);
        //        return false;
        //    }
        //}

        static public bool getAdjustStatistics(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/getAdjustStatistics.json";

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

        static public bool insertAdjustmentPrice(long inventoryId, short checkType, string jsonUrl, List<string> listCol, DataTable _dtadjustPrice, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = $"/inventoryCheck/{jsonUrl}.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("CHECK_TYPE", checkType);

                foreach (string col in listCol)
                    jobj.Add(col, ConvertUtil.ToInt64(_dtadjustPrice.Rows[checkType][col]));

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


        static public bool insertAdjustmentPrice(long inventoryId, short checkType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/insertAdjustmentPrice.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("CHECK_TYPE", checkType);

                foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                    jobj.Add(col, ConvertUtil.ToInt64(ProjectInfo._dtNTBAdjustmentPrice.Rows[checkType][col]));

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

        static public bool insertAdjustmentAllInOnePrice(long inventoryId, short checkType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/insertAdjustmentAllInOnePrice.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("CHECK_TYPE", checkType);

                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceColShort)
                    jobj.Add(col, ConvertUtil.ToInt64(ProjectInfo._dtAllInOneAdjustmentPrice.Rows[checkType][col]));

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

        static public bool insertAdjustmentTabletPrice(long inventoryId, short checkType, DataTable dtAdjustmentPrice, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/insertAdjustmentTabletPrice.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("CHECK_TYPE", checkType);

                foreach (string col in ExamineInfo._listAdjustmentTabletPriceColShort)
                    jobj.Add(col, ConvertUtil.ToInt64(dtAdjustmentPrice.Rows[checkType][col]));

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


        static public bool getNtbPrice(ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/price/getNtbPrice.json";


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


        static public bool getAdjustPrice(long ntbListId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();

                string url = "/price/getAdjustPrice.json";

                jobj.Add("NTB_LIST_ID", ntbListId);
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


        static public bool insertAdjustPrice(long ntbListId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/price/insertAdjustPrice.json";

                jobj.Add("NTB_LIST_ID", ntbListId);
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


        static public bool makeAdjustment(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/makeAdjustment.json";

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

        static public bool updateWarehousingAdjustmentState(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/updateWarehousingAdjustmentState.json";

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
