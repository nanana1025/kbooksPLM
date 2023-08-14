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
    static class DBPrice
    {

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


        static public bool getAdjustPrice(long ntbPriceId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();

                string url = "/price/getAdjustPrice.json";

                jobj.Add("NTB_PRICE_ID", ntbPriceId);
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

        static public bool updateNtbPrice(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/updateNtbPrice2.json";

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

        static public bool updateAdjustPrice(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/updateAdjustPrice.json";

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


        static public bool insertAdjustPrice(long ntbPriceId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/price/insertAdjustPrice.json";

                jobj.Add("NTB_PRICE_ID", ntbPriceId);
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

        static public bool searchPartPriceMapping(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/searchPartPriceMapping.json";

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

        static public bool updatePartPrice(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/updatePartPrice.json";

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

        static public bool searchWMPartPriceMapping(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/searchWMPartPriceMapping.json";

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

        static public bool getPartPriceMapping(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/getPartPriceMapping.json";

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

        static public bool updatePartPriceMapping(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/updatePartPriceMapping.json";

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

        static public bool insertLTPart(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/insertLTPart.json";

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

        static public bool updatePartNOrder(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/updatePartNOrder.json";

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

        static public bool updateWmPartInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/price/updateWmPartInfo.json";

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



        


    }
}
