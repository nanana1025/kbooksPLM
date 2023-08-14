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
                //ProjectInfo._userType = ConvertUtil.ToString(jobj["USER_TYPE"]);
                //ProjectInfo._userTeamCd = ConvertUtil.ToString(jobj["TEAM_CD"]);
                //ProjectInfo._userPosition = ConvertUtil.ToString(jobj["POSITION"]); 
            }
            catch
            {
                return false;
            }

            return true;
        }

        static public bool SendCheckComponentRequest(string componentCd, DataRow row, ref JObject jResult)
        {
            try
            {
                string url = "/compInven/checkComponentAndInventory.json";

                JObject jobj = new JObject();

                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("USER_ID", ProjectInfo._userId);

                List<string> listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColumn)
                {
                    jobj.Add(col, row[col].ToString());
                }

                string result = SendRequestMessage(jobj, url);

                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"]))
                    return true;
                else
                {
                    //MessageBox.Show(jResult["MSG"].ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static public bool osInfoCheck(long inventoryId, DataRow row, ref JObject jResult, string receipt = "")
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/osInfoCheck.json";

                if (!string.IsNullOrEmpty(receipt))
                    jobj.Add("RECEIPT", receipt);

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("PRODUCT_KEY", row["OS_PRODUCT_KEY"].ToString());
                jobj.Add("SERIAL_NO", row["OS_SERIAL_NO"].ToString());
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



        static public bool InsertInventory(string representativeType, string representativeNo, string representativeCol, string componentCd, string location, int pallet, DataRow row, long warehouseMovementId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/insertInventory.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("LOCATION", location);
                jobj.Add("PALLET", pallet);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("PC_TYPE", ProjectInfo._type);
                jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                jobj.Add("NTB_MANUFACTURE_TYPE", ProjectInfo._ntbManufactureType);
                jobj.Add("WAREHOUSE_MOVEMENT_ID", warehouseMovementId);
                
                jobj.Add("USER_ID", ProjectInfo._userId);

                List<string> listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColumn)
                {
                    jobj.Add(col, row[col].ToString());
                }

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


        static public bool getInventory(string barcode, string componentCd, DataRow row, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/getInventory.json";

                jobj.Add("BARCODE", barcode);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("USER_ID", ProjectInfo._userId);

                List<string> listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColumn)
                {
                    jobj.Add(col, row[col].ToString());
                }

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

        static public bool getInventoryByInventoryId(long invnetoryId, string componentCd,ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/getInventoryByInventoryId.json";

                jobj.Add("INVENTORY_ID", invnetoryId);
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

        static public bool InsertComponent(string componentCd, DataRow row, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/insertComponent.json";

                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("USER_ID", ProjectInfo._userId);

                List<string> listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColumn)
                {
                    jobj.Add(col, row[col].ToString());
                }

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

        static public bool updateComponent(string componentCd, DataRow row, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/updateComponent.json";

                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("USER_ID", ProjectInfo._userId);

                List<string> listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColumn)
                {
                    jobj.Add(col, row[col].ToString());
                }

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

        static public bool printProduct(string representativeType, string representativeNo, string representativeCol, Dictionary<string, string> dicInventoryId, string port, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printProduct.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PORT", port);
                jobj.Add("TYPE", ProjectInfo._type);
                jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                jobj.Add("NTB_LIST_ID", ProjectInfo._ntbListId);
                jobj.Add("NTB_MANUFACTURE_TYPE", ProjectInfo._ntbManufactureType);

                foreach (KeyValuePair<string, string> item in dicInventoryId)
                {
                    jobj.Add(item.Key, dicInventoryId[item.Key]);
                }

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
                return false;
            }
        }

        static public bool printNtbProduct(string representativeType, string representativeNo, string representativeCol, long inventoryId, int checkType, string port, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printNtbProduct.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PORT", port);
                jobj.Add("TYPE", ProjectInfo._type);
                jobj.Add("INVENTORY_ID", inventoryId);
                //jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                //jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                //jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                //jobj.Add("NTB_CATEGORY", ProjectInfo._ntbCategory);
                jobj.Add("CHECK_TYPE", checkType);


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
                return false;
            }
        }

        static public bool printInventoryInfo(string representativeType, string representativeNo, string representativeCol, string barcode, string port, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/print.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("BARCODE", barcode);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PORT", port);

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
                return false;
            }
        }

        static public bool getWarehousingInfo(string representativeNo, ref DateTime _warehousingDate)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingInfo.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("WAREHOUSING", representativeNo);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                string success = jobj["SUCCESS"].ToString().ToUpper();
                if (success.Equals("TRUE"))
                {
                    string wDate = jobj["REGIST_DT"].ToString();
                    _warehousingDate = Convert.ToDateTime(wDate);
                    return true;
                }
                else
                {
                    MessageBox.Show(jobj["MSG"].ToString());
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }


        
        static public bool getProductNtbSpecinfo(long inventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/product/getProductNtbSpecinfo.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("INVENTORY_ID", inventoryId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);

                string success = jResult["SUCCESS"].ToString().ToUpper();
                if (success.Equals("TRUE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        static public bool getNtbListId(ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/price/getNtbListId.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                //jobj.Add("NTB_CATEGORY", ProjectInfo._ntbCategory);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);

                string success = jResult["SUCCESS"].ToString().ToUpper();
                if (success.Equals("TRUE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        static public bool getNTBAdjustmentPrice(string representativeNo, string representativeType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/price/getNTBAdjustmentPrice.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("REPRESENTATIVE_NO", representativeNo);    //입고번호로 업체아이디 가져올때 필요. 당장은 필요 없음
                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add("NTB_LIST_ID", ProjectInfo._ntbListId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);

                string success = jResult["SUCCESS"].ToString().ToUpper();
                if (success.Equals("TRUE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        static public bool getNTBAdjustmentPrice(string representativeNo, string representativeType, long NtbListId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/price/getNTBAdjustmentPrice.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("REPRESENTATIVE_NO", representativeNo);
                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add("NTB_LIST_ID", NtbListId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);

                string success = jResult["SUCCESS"].ToString().ToUpper();
                if (success.Equals("TRUE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        static public bool getWarehousingInfo(string representativeNo, ref DateTime _warehousingDate, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingInfo.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("WAREHOUSING", representativeNo);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);

                string success = jResult["SUCCESS"].ToString().ToUpper();
                if (success.Equals("TRUE"))
                {
                    string wDate = jResult["REGIST_DT"].ToString();
                    _warehousingDate = Convert.ToDateTime(wDate);
                    return true;
                }
                else
                {
                    MessageBox.Show(jResult["MSG"].ToString());
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        static public bool getWarehousingInventoryList(string warehousing, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingInventoryList.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("WAREHOUSING", warehousing);

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
                return false;
            }
        }

        static public bool swapInventory(long warehousingIdFrom, long warehousingIdTo, List<long> listInventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/swapInventory.json";

                jobj.Add("WAREHOUSING_FROM", warehousingIdFrom);
                jobj.Add("WAREHOUSING_TO", warehousingIdTo);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
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
                return false;
            }
        }

        static public bool getProxyInfo(string representativeNo, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getProxyInfo.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("RECEIPT", representativeNo);

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

        static public bool consignedComponentContract(List<long> listProsyPartId, int consignedType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/consignedComponentContract.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PROXY_PART_ID", string.Join(",", listProsyPartId));
                jobj.Add("CONSIGNED_TYPE", consignedType);

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

        static public bool saveConsignedWarehousing(long ProsyPartId, string warehousing, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/saveConsignedWarehousing.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PROXY_PART_ID", ProsyPartId);
                jobj.Add("WAREHOUSING", warehousing);

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

        static public bool asignConsignedReleaseInventory(object proxyid, long ProxyPartId, object companyId, int consignedType, string componentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/asignConsignedReleaseInventory.json";

                if (componentCd.Equals("MBD"))
                    url = "/consigned/asignConsignedReleaseInventoryMBD.json";
                else if (componentCd.Equals("MEM"))
                    url = "/consigned/asignConsignedReleaseInventoryMEM.json";

                jobj.Add("PROXY_ID", ConvertUtil.ToInt64(proxyid));
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PROXY_PART_ID", ProxyPartId);
                jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(companyId));
                jobj.Add("CONSIGNED_TYPE", consignedType);
                jobj.Add("COMPONENT_CD", componentCd);

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

        static public bool asignConsignedState(object proxyId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/asignConsignedState.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PROXY_ID", ConvertUtil.ToInt64(proxyId));


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

        static public bool deleteReleaseComponent(long ProsyPartId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/deleteReleaseComponent.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PROXY_PART_ID", ProsyPartId);

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

        static public bool updatePrice(long ProsyPartId, object price, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/updatePrice.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PROXY_PART_ID", ProsyPartId);
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
        

        static public bool updateLocation(List<long> listInventoryId, long warehouse, long pallet)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/updateLocation.json";

                if(listInventoryId.Count == 1)
                    jobj.Add("INVENTORY_ID", listInventoryId[0]);
                else
                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                jobj.Add("LOCATION", warehouse);
                jobj.Add("PALLET", pallet);
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        static public bool updatePallet(List<long> listInventoryId, long warehouse, long pallet)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/updatePallet.json";

                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                jobj.Add("LOCATION", warehouse);
                jobj.Add("PALLET", pallet);
                jobj.Add("USER_ID", ProjectInfo._userId);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool insertAllInOneCheck(string representativeType, string representativeNo, string representativeCol, string barcode, long inventoryId, 
            short checkType, Dictionary<string, short> dicAllInONeCheck, List<long> listInventoryId, string etcDes, string pGrade, int onlyCheck = 0)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/AllInOneCheck.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", "3");
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("CASE_DESTROYED", dicAllInONeCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicAllInONeCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicAllInONeCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicAllInONeCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicAllInONeCheck["CASE_DISCOLORED"]);
                jobj.Add("MENU", dicAllInONeCheck["MENU"]);              
                jobj.Add("DISPLAY", dicAllInONeCheck["DISPLAY"]);
                jobj.Add("PORT", dicAllInONeCheck["PORT"]);
                jobj.Add("ADAPTER", dicAllInONeCheck["ADAPTER"]);     
                jobj.Add("MOUSEPAD", dicAllInONeCheck["MOUSEPAD"]);
                jobj.Add("KEYBOARD", dicAllInONeCheck["KEYBOARD"]);
                jobj.Add("CAM", dicAllInONeCheck["CAM"]);
                jobj.Add("USB", dicAllInONeCheck["USB"]);
                jobj.Add("LAN_WIRELESS", dicAllInONeCheck["LAN_WIRELESS"]);
                jobj.Add("LAN_WIRED", dicAllInONeCheck["LAN_WIRED"]);
                jobj.Add("HDD", dicAllInONeCheck["HDD"]);
                jobj.Add("ODD", dicAllInONeCheck["ODD"]);
                jobj.Add("TEST_CHECK", dicAllInONeCheck["TEST_CHECK"]);
                jobj.Add("BIOS", dicAllInONeCheck["BIOS"]);
                jobj.Add("OS", dicAllInONeCheck["OS"]);
                jobj.Add("PRODUCT_GRADE", pGrade);
                jobj.Add("ETC_DES", etcDes);
                jobj.Add("C_INVENTORY", string.Join(",", listInventoryId));
                jobj.Add("ONLY_CHECK", onlyCheck);
                

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }



        static public bool printAllInOneProduct(string representativeType, string representativeNo, string representativeCol, long inventoryId, int checkType, string port, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printAllInOneProduct.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PORT", port);
                jobj.Add("TYPE", ProjectInfo._type);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("CHECK_TYPE", checkType);


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
                return false;
            }
        }

        static public JObject insertTabletCheck(string representativeType, string representativeNo, string representativeCol, string barcode, long inventoryId, short checkType, Dictionary<string, int> dicCheck, string etcDes, string pGrade, string batteryRemain, string repairContent)
        {
            JObject jResult = new JObject();

            try
            { 
                JObject jobj = new JObject();
                string url = "/inventoryCheck/TabletCheck.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", 5);
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("CASE_DESTROYED", dicCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", 0);
                jobj.Add("CASE_DISCOLORED", dicCheck["CASE_DISCOLORED"]);
                jobj.Add("DISPLAY", dicCheck["DISPLAY"]);
                jobj.Add("BATTERY", dicCheck["BATTERY"]);
                jobj.Add("ADAPTER", dicCheck["ADAPTER"]);
                jobj.Add("BUTTON", dicCheck["BUTTON"]);
                jobj.Add("USB_PORT", dicCheck["USB_PORT"]);
                jobj.Add("USB_CABLE", dicCheck["USB_CABLE"]);
                jobj.Add("PEN", dicCheck["PEN"]);
                jobj.Add("SD_CARD", dicCheck["SD_CARD"]);
                jobj.Add("SOFTWARE", dicCheck["SOFTWARE"]);
                jobj.Add("CAM", dicCheck["CAM"]);
                jobj.Add("SOUND", dicCheck["SOUND"]);
                jobj.Add("MIKE", dicCheck["MIKE"]);               
                jobj.Add("EAR_PHONE", dicCheck["EAR_PHONE"]);
                jobj.Add("LAN_WIRELESS", dicCheck["LAN_WIRELESS"]);               
                jobj.Add("TEST_CHECK", dicCheck["TEST_CHECK"]);
                jobj.Add("SELF_CHECK", dicCheck["SELF_CHECK"]);               
                jobj.Add("BATTERY_REMAIN", batteryRemain);
                jobj.Add("REPAIR_CONTENT", repairContent);
                jobj.Add("PRODUCT_GRADE", pGrade);
                jobj.Add("ETC_DES", etcDes);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                jResult.Add("SUCCESS", true);
                jResult.Add("MSG", jobj["MSG"]);

                //MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                jResult.Add("SUCCESS", false);
                jResult.Add("MSG", e.Message);
                //Debug.WriteLine(e.Message);
                return jResult;
            }

            return jResult;
        }

        static public bool printTabletProduct(string representativeType, string representativeNo, string representativeCol, long inventoryId, int checkType, string port, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printTabletProduct.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("PORT", port);
                jobj.Add("TYPE", 5);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("CHECK_TYPE", checkType);


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
                return false;
            }
        }



        static public bool insertNtbCheck(string representativeType, string representativeNo, string representativeCol, string startDt, string endDt, string barcode,
            long inventoryId, short checkType, Dictionary<string, short> dicNtbCheck, List<long> listInventoryId, string caseDestroyDescription, string batteryRemain, string pGrade, int onlyCheck = 0, bool showMgs = true)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/NtbCheck.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", "2");
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("START_DT", startDt);
                jobj.Add("END_DT", endDt);
                jobj.Add("CASE_DESTROYED", dicNtbCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicNtbCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicNtbCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicNtbCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicNtbCheck["CASE_DISCOLORED"]);
                jobj.Add("CASE_HINGE", dicNtbCheck["CASE_HINGE"]);
                jobj.Add("COOLER", dicNtbCheck["COOLER"]);
                jobj.Add("CASE_DES", caseDestroyDescription);
                jobj.Add("DISPLAY", dicNtbCheck["DISPLAY"]);
                jobj.Add("BATTERY", dicNtbCheck["BATTERY"]);
                jobj.Add("BATTERY_REMAIN", batteryRemain);
                jobj.Add("MOUSEPAD", dicNtbCheck["MOUSEPAD"]);
                jobj.Add("KEYBOARD", dicNtbCheck["KEYBOARD"]);
                jobj.Add("CAM", dicNtbCheck["CAM"]);
                jobj.Add("USB", dicNtbCheck["USB"]);
                jobj.Add("LAN_WIRELESS", dicNtbCheck["LAN_WIRELESS"]);
                jobj.Add("LAN_WIRED", dicNtbCheck["LAN_WIRED"]);
                jobj.Add("HDD", dicNtbCheck["HDD"]);
                jobj.Add("ODD", dicNtbCheck["ODD"]);
                jobj.Add("TEST_CHECK", dicNtbCheck["TEST_CHECK"]);
                jobj.Add("BIOS", dicNtbCheck["BIOS"]);
                jobj.Add("OS", dicNtbCheck["OS"]);
                jobj.Add("PRODUCT_GRADE", pGrade);
                jobj.Add("C_INVENTORY", string.Join(",", listInventoryId));
                jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                jobj.Add("NTB_LIST_ID", ProjectInfo._ntbListId);
                jobj.Add("NTB_MANUFACTURE_TYPE", ProjectInfo._ntbManufactureType);
                jobj.Add("ONLY_CHECK", onlyCheck);
                

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                if(showMgs)
                    MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        static public bool insertNtbCheckExternal(string representativeType, string representativeNo, string representativeCol, string startDt, string endDt, string barcode,
            long inventoryId, short checkType, Dictionary<string, short> dicNtbCheck, List<long> listInventoryId, string caseDestroyDescription, string batteryRemain, string pGrade, int onlyCheck = 0, bool showMgs = true)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/NtbCheckExternal.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", "2");
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("START_DT", startDt);
                jobj.Add("END_DT", endDt);
                jobj.Add("CASE_DESTROYED", dicNtbCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicNtbCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicNtbCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicNtbCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicNtbCheck["CASE_DISCOLORED"]);
                jobj.Add("CASE_HINGE", dicNtbCheck["CASE_HINGE"]);
                jobj.Add("COOLER", dicNtbCheck["COOLER"]);
                jobj.Add("CASE_DES", caseDestroyDescription);
                jobj.Add("DISPLAY", dicNtbCheck["DISPLAY"]);
                jobj.Add("BATTERY", dicNtbCheck["BATTERY"]);
                jobj.Add("BATTERY_REMAIN", batteryRemain);
                jobj.Add("MOUSEPAD", dicNtbCheck["MOUSEPAD"]);
                jobj.Add("KEYBOARD", dicNtbCheck["KEYBOARD"]);
                jobj.Add("CAM", dicNtbCheck["CAM"]);
                jobj.Add("USB", dicNtbCheck["USB"]);
                jobj.Add("LAN_WIRELESS", dicNtbCheck["LAN_WIRELESS"]);
                jobj.Add("LAN_WIRED", dicNtbCheck["LAN_WIRED"]);
                jobj.Add("HDD", dicNtbCheck["HDD"]);
                jobj.Add("ODD", dicNtbCheck["ODD"]);
                jobj.Add("TEST_CHECK", dicNtbCheck["TEST_CHECK"]);
                jobj.Add("BIOS", dicNtbCheck["BIOS"]);
                jobj.Add("OS", dicNtbCheck["OS"]);
                jobj.Add("SPEAKER", dicNtbCheck["SPEAKER"]);
                jobj.Add("OVERHEAT", dicNtbCheck["OVERHEAT"]);
                jobj.Add("SHUTDOWN", dicNtbCheck["SHUTDOWN"]);
                jobj.Add("PRODUCT_GRADE", pGrade);
                jobj.Add("C_INVENTORY", string.Join(",", listInventoryId));
                jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                jobj.Add("NTB_LIST_ID", ProjectInfo._ntbListId);
                jobj.Add("NTB_MANUFACTURE_TYPE", ProjectInfo._ntbManufactureType);
                jobj.Add("ONLY_CHECK", onlyCheck);


                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                if (showMgs)
                    MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool printNtbCheck(string barcode, long inventoryId, short checkType, Dictionary<string, short> dicNtbCheck, string caseDestroyDescription, string batteryRemain, string pGrade, string port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printNtbCheck2ndEdition.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("MBD", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", "2");
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("EDITION", "2");
                jobj.Add("PORT", port);
                jobj.Add("CASE_DESTROYED", dicNtbCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicNtbCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicNtbCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicNtbCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicNtbCheck["CASE_DISCOLORED"]);
                jobj.Add("CASE_HINGE", dicNtbCheck["CASE_HINGE"]);
                jobj.Add("COOLER", dicNtbCheck["COOLER"]);
                jobj.Add("CASE_DES", caseDestroyDescription);
                jobj.Add("DISPLAY", dicNtbCheck["DISPLAY"]);
                jobj.Add("BATTERY", dicNtbCheck["BATTERY"]);
                jobj.Add("BATTERY_REMAIN", batteryRemain);
                jobj.Add("MOUSEPAD", dicNtbCheck["MOUSEPAD"]);
                jobj.Add("KEYBOARD", dicNtbCheck["KEYBOARD"]);
                jobj.Add("CAM", dicNtbCheck["CAM"]);
                jobj.Add("USB", dicNtbCheck["USB"]);
                jobj.Add("LAN_WIRELESS", dicNtbCheck["LAN_WIRELESS"]);
                jobj.Add("LAN_WIRED", dicNtbCheck["LAN_WIRED"]);
                jobj.Add("HDD", dicNtbCheck["HDD"]);
                jobj.Add("ODD", dicNtbCheck["ODD"]);
                jobj.Add("BIOS", dicNtbCheck["BIOS"]);
                jobj.Add("OS", dicNtbCheck["OS"]);
                jobj.Add("TEST_CHECK", dicNtbCheck["TEST_CHECK"]);
                jobj.Add("PRODUCT_GRADE", pGrade);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool insertNtbCheck1stEdition(string representativeType, string representativeNo, string representativeCol, string barcode, long inventoryId, short checkType, Dictionary<string, short> dicNtbCheck, List<long> listInventoryId)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/NtbCheck.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", "2");
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("CASE_DESTROYED", dicNtbCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicNtbCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicNtbCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicNtbCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicNtbCheck["CASE_DISCOLORED"]);
                jobj.Add("CASE_HINGE", dicNtbCheck["CASE_HINGE"]);
                jobj.Add("DISPLAY", dicNtbCheck["DISPLAY"]);
                jobj.Add("BATTERY", dicNtbCheck["BATTERY"]);
                jobj.Add("MOUSEPAD", dicNtbCheck["MOUSEPAD"]);
                jobj.Add("KEYBOARD", dicNtbCheck["KEYBOARD"]);
                jobj.Add("CAM", dicNtbCheck["CAM"]);
                jobj.Add("USB", dicNtbCheck["USB"]);
                jobj.Add("LAN_WIRELESS", dicNtbCheck["LAN_WIRELESS"]);
                jobj.Add("LAN_WIRED", dicNtbCheck["LAN_WIRED"]);
                jobj.Add("HDD", dicNtbCheck["HDD"]);
                jobj.Add("ODD", dicNtbCheck["ODD"]);
                jobj.Add("ADAPTER", dicNtbCheck["ADAPTER"]);
                jobj.Add("BIOS", dicNtbCheck["BIOS"]);
                jobj.Add("OS", dicNtbCheck["OS"]);
                jobj.Add("C_INVENTORY", string.Join(",", listInventoryId));
                jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                jobj.Add("NTB_MANUFACTURE_TYPE", ProjectInfo._ntbManufactureType);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool printNtbCheck1stEdition(string barcode, long inventoryId, short checkType, Dictionary<string, short> dicNtbCheck, string port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printNtbCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("MBD", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", "2");
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("EDITION", "1");
                jobj.Add("PORT", port);
                jobj.Add("CASE_DESTROYED", dicNtbCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicNtbCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicNtbCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicNtbCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicNtbCheck["CASE_DISCOLORED"]);
                jobj.Add("CASE_HINGE", dicNtbCheck["CASE_HINGE"]);
                jobj.Add("DISPLAY", dicNtbCheck["DISPLAY"]);
                jobj.Add("BATTERY", dicNtbCheck["BATTERY"]);
                jobj.Add("MOUSEPAD", dicNtbCheck["MOUSEPAD"]);
                jobj.Add("KEYBOARD", dicNtbCheck["KEYBOARD"]);
                jobj.Add("CAM", dicNtbCheck["CAM"]);
                jobj.Add("USB", dicNtbCheck["USB"]);
                jobj.Add("LAN_WIRELESS", dicNtbCheck["LAN_WIRELESS"]);
                jobj.Add("LAN_WIRED", dicNtbCheck["LAN_WIRED"]);
                jobj.Add("HDD", dicNtbCheck["HDD"]);
                jobj.Add("ODD", dicNtbCheck["ODD"]);
                jobj.Add("ADAPTER", dicNtbCheck["ADAPTER"]);
                jobj.Add("BIOS", dicNtbCheck["BIOS"]);
                jobj.Add("OS", dicNtbCheck["OS"]);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        //static public bool insertAllInOneCheck(string barcode, long inventoryId, short checkType, Dictionary<string, short> dicAllInOneCheck, List<long> listInventory)
        //{
        //    try
        //    {
        //        JObject jobj = new JObject();
        //        string url = "/inventoryCheck/AllInOneCheck.json";

        //        jobj.Add("USER_ID", ProjectInfo._userId);
        //        jobj.Add("BARCODE", barcode);
        //        jobj.Add("INVENTORY_ID", inventoryId);
        //        jobj.Add("TYPE", "3");
        //        jobj.Add("CHECK_TYPE", checkType);
        //        jobj.Add("CASE_DESTROYED", dicAllInOneCheck["CASE_DESTROYED"]);
        //        jobj.Add("CASE_SCRATCH", dicAllInOneCheck["CASE_SCRATCH"]);
        //        jobj.Add("CASE_STABBED", dicAllInOneCheck["CASE_STABBED"]);
        //        jobj.Add("CASE_PRESSED", dicAllInOneCheck["CASE_PRESSED"]);
        //        jobj.Add("CASE_DISCOLORED", dicAllInOneCheck["CASE_DISCOLORED"]);
        //        jobj.Add("DISPLAY", dicAllInOneCheck["DISPLAY"]);
        //        jobj.Add("CAM", dicAllInOneCheck["CAM"]);
        //        jobj.Add("USB", dicAllInOneCheck["USB"]);
        //        jobj.Add("SOUND", dicAllInOneCheck["SOUND"]);
        //        jobj.Add("LAN_WIRELESS", dicAllInOneCheck["LAN_WIRELESS"]);
        //        jobj.Add("LAN_WIRED", dicAllInOneCheck["LAN_WIRED"]);
        //        jobj.Add("HDD", dicAllInOneCheck["HDD"]);
        //        jobj.Add("ODD", dicAllInOneCheck["ODD"]);
        //        jobj.Add("ADAPTER", dicAllInOneCheck["ADAPTER"]);
        //        jobj.Add("BIOS", dicAllInOneCheck["BIOS"]);
        //        jobj.Add("OS", dicAllInOneCheck["OS"]);
        //        jobj.Add("C_INVENTORY", string.Join(",", listInventory));

        //        // 요청 전송
        //        string result = SendRequestMessage(jobj, url);

        //        // 반환값 파싱
        //        jobj = JObject.Parse(result);

        //        MessageBox.Show(jobj["MSG"].ToString());

        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //        return false;
        //    }

        //    return true;
        //}

        static public bool printAllInOneCheck(string barcode, long inventoryId, short checkType, Dictionary<string, short> dicAllInOneCheck, string port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printAllInOneCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("MBD", barcode);
                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("TYPE", "3");
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("PORT", port);
                jobj.Add("CASE_DESTROYED", dicAllInOneCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicAllInOneCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicAllInOneCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicAllInOneCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicAllInOneCheck["CASE_DISCOLORED"]);
                jobj.Add("DISPLAY", dicAllInOneCheck["DISPLAY"]);
                jobj.Add("CAM", dicAllInOneCheck["CAM"]);
                jobj.Add("USB", dicAllInOneCheck["USB"]);
                jobj.Add("SOUND", dicAllInOneCheck["SOUND"]);
                jobj.Add("LAN_WIRELESS", dicAllInOneCheck["LAN_WIRELESS"]);
                jobj.Add("LAN_WIRED", dicAllInOneCheck["LAN_WIRED"]);
                jobj.Add("HDD", dicAllInOneCheck["HDD"]);
                jobj.Add("ODD", dicAllInOneCheck["ODD"]);
                jobj.Add("ADAPTER", dicAllInOneCheck["ADAPTER"]);
                jobj.Add("BIOS", dicAllInOneCheck["BIOS"]);
                jobj.Add("OS", dicAllInOneCheck["OS"]);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool insertMonitorCheck(string barcode, short checkType, Dictionary<string, int> dicMonitor, string des, string pGrade, string size)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/MonitorCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("CASE_DESTROYED", dicMonitor["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicMonitor["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicMonitor["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicMonitor["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicMonitor["CASE_DISCOLORED"]);
                jobj.Add("DISPLAY", dicMonitor["DISPLAY"]);
                jobj.Add("PORT", dicMonitor["PORT"]);
                jobj.Add("ADAPTER", dicMonitor["ADAPTER"]);
                jobj.Add("DES", des);
                jobj.Add("PRODUCT_GRADE", pGrade);
                jobj.Add("SIZE", size);
                jobj.Add("TYPE", $"{dicMonitor["TYPE"]}");
                jobj.Add("POWER_TYPE", $"{dicMonitor["POWER_TYPE"]}");
                jobj.Add("BRAND", $"{dicMonitor["BRAND"]}");
                jobj.Add("MENU", dicMonitor["MENU"]);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool printMonitorCheck(string barcode, short checkType, Dictionary<string, int> dicMonitor, string componentCd, string des, string pGrade, string size, string port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printInventoryCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("PORT", port);
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("CASE_DESTROYED", dicMonitor["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicMonitor["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicMonitor["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicMonitor["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicMonitor["CASE_DISCOLORED"]);
                jobj.Add("DISPLAY", dicMonitor["DISPLAY"]);
                jobj.Add("CPORT", dicMonitor["PORT"]);
                jobj.Add("ADAPTER", dicMonitor["ADAPTER"]);
                jobj.Add("DES", des);
                jobj.Add("PRODUCT_GRADE", pGrade);
                jobj.Add("SIZE", size);
                jobj.Add("TYPE", $"{dicMonitor["TYPE"]}");
                jobj.Add("POWER_TYPE", $"{dicMonitor["POWER_TYPE"]}");
                jobj.Add("BRAND", $"{dicMonitor["BRAND"]}");
                jobj.Add("MENU", dicMonitor["MENU"]);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool insertCasCheck(string barcode, short checkType, Dictionary<string, int> dicinventory, string des, string pGrade)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/CaseCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("CASE_DESTROYED", dicinventory["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicinventory["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicinventory["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicinventory["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicinventory["CASE_DISCOLORED"]);
                jobj.Add("DES", des);
                jobj.Add("PRODUCT_GRADE", pGrade);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool printCasCheck(string barcode, short checkType, Dictionary<string, int> dicCasitor, string componentCd, string des, string pGrade, string port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printInventoryCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("PORT", port);
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("CASE_DESTROYED", dicCasitor["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicCasitor["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicCasitor["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicCasitor["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicCasitor["CASE_DISCOLORED"]);
                jobj.Add("DES", des);
                jobj.Add("PRODUCT_GRADE", pGrade);



                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool insertInventoryCheck(string barcode, short checkType, string ComponentCd, Dictionary<string, int> dicinventory, string des, string pGrade)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/InventoryCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("COMPONENT_CD", ComponentCd);
                jobj.Add("FAULT", dicinventory["FAULT"]);
                jobj.Add("DES", des);
                jobj.Add("PRODUCT_GRADE", pGrade);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool printInventoryCheck(string barcode, short checkType, Dictionary<string, int> dicinventory, string componentCd, string des, string pGrade, string port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printInventoryCheck.json";

                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("BARCODE", barcode);
                jobj.Add("PORT", port);
                jobj.Add("CHECK_TYPE", checkType);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("FAULT", dicinventory["FAULT"]);
                jobj.Add("DES", des);
                jobj.Add("PRODUCT_GRADE", pGrade);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        static public bool InsertReleasePart(string representativeType, string representativeNo, string representativeCol, List<long> listInventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/release/insertReleasePart.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
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

        static public bool deleteReleasePart(string representativeType, string representativeNo, string representativeCol, List<long> listInventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/release/deleteReleasePart.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
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

        static public bool deleteReleaseProduct(string representativeType, string representativeNo, string representativeCol, long inventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/release/deleteReleaseProduct.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("INVENTORY_ID",inventoryId);
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

        static public bool getCheckInfo(long inventoryId, string componentCd, short checkType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/getCheckInventoryInfo.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("CHECK_TYPE", checkType);
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

        

        static public bool constructProduct(string representativeType, string representativeNo, string representativeCol, long inventoryId, List<long> listInventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/product/constructProduct.json";

                jobj.Add("REPRESENTATIVE_TYPE", representativeType);
                jobj.Add(representativeCol, representativeNo);
                jobj.Add("MBD_INVENTORY_ID", inventoryId);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("CPU_CATEGORY", ProjectInfo._cpuCategory);
                jobj.Add("CPU_GENERATION", ProjectInfo._cpuGeneration);
                jobj.Add("CPU_CODE_NM", ProjectInfo._codeNm);
                jobj.Add("NTB_LIST_ID", ProjectInfo._ntbListId);
                jobj.Add("NTB_MANUFACTURE_TYPE", ProjectInfo._ntbManufactureType);
                jobj.Add("TYPE", ProjectInfo._type);


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
                return false;
            }
        }

        static public bool getProductList(long inventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/product/getProductList.json";

                jobj.Add("INVENTORY_ID", inventoryId);
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
                return false;
            }
        }

        static public bool getProductListByBarcode(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/getProductListByBarcode.json";

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
                return false;
            }
        }

        
        static public bool getReleaseList(string releases, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/release/getProductList.json";

                jobj.Add("RELEASES", releases);
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
                return false;
            }
        }

        static public bool getConsignedReleaseList(string receipt, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedList.json";

                jobj.Add("RECEIPT", receipt);
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
                return false;
            }
        }

        static public bool getConsignedComponentInfo(object proxyId, object companyId, string componentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedComponentList.json";

                jobj.Add("PROXY_ID", ConvertUtil.ToString(proxyId));
                jobj.Add("COMPANY_ID", ConvertUtil.ToString(companyId));
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
                return false;
            }
        }

        static public bool getConsignedInventory(object proxyId, string componentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getConsignedInventory.json";

                jobj.Add("PROXY_ID", ConvertUtil.ToString(proxyId));
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
                return false;
            }
        }


        static public bool getInventoryFromConsigned(string barcode, string componentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getPartInfo.json";

                jobj.Add("BARCODE", barcode);
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

        static public bool updateInventoryInfo(long inventoryId, string componentCd, DataRow row, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/updateInventory.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("USER_ID", ProjectInfo._userId);

                List<string> listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColumn)
                {
                    jobj.Add(col, row[col].ToString());
                }

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

        static public bool updateInventoryInfoDetail(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/compInven/updateInventoryInfoDetail.json";

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

        static public bool updateInventoryInfoBulk(JObject jobj, ref JObject jResult)
        {
            try
            {       
                string url = "/compInven/updateInventoryBulk.json";
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

        static public bool updateInventoryState(long inventoryId, string inventoryState, string inventoryCat, string lockYn, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/updateInventoryState.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("INVENTORY_STATE", inventoryState);
                jobj.Add("INVENTORY_CAT", inventoryCat);
                jobj.Add("LOCK_YN", lockYn);
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

        static public bool getBarcodeValidity(string barcode, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/getBarcodeValidity.json";

                jobj.Add("BARCODE", barcode);
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

        static public bool getBarcodeInPalletValidity(string pallet, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/getBarcodeInPalletValidity.json";

                jobj.Add("PALLET", ConvertUtil.ToInt32(pallet));
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

        static public bool makeWarehousingMovement(object releaseWarehouseNo,
            object warehousingWarehouseNo,
            object releasePalletNo,
            object warehousingPalletNo,
            List<long> listInventoryId,
            ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/makeWarehousingMovement.json";

                jobj.Add("RELEASE_WAREHOUSE_ID", ConvertUtil.ToString(releaseWarehouseNo));
                jobj.Add("WAREHOUSING_WAREHOUSE_ID", ConvertUtil.ToString(warehousingWarehouseNo));
                jobj.Add("RELEASE_PALLET_ID", ConvertUtil.ToString(releasePalletNo));
                jobj.Add("WAREHOUSING_PALLET_ID", ConvertUtil.ToString(warehousingPalletNo));
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
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

        static public bool updateWarehousingMovement(long warehouseMovementId,
            object releaseWarehouseNo,
            object warehousingWarehouseNo,
            object releasePalletNo,
            object warehousingPalletNo,
            List<long> listInventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/updateWarehousingMovement.json";
                jobj.Add("WAREHOUSE_MOVEMENT_ID", warehouseMovementId);
                jobj.Add("RELEASE_WAREHOUSE_ID", ConvertUtil.ToString(releaseWarehouseNo));
                jobj.Add("WAREHOUSING_WAREHOUSE_ID", ConvertUtil.ToString(warehousingWarehouseNo));
                jobj.Add("RELEASE_PALLET_ID", ConvertUtil.ToString(releasePalletNo));
                jobj.Add("WAREHOUSING_PALLET_ID", ConvertUtil.ToString(warehousingPalletNo));
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
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
        static public bool deleteWarehouseMovementList(long warehouseMovementId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/deleteWarehouseMovementList.json";
                jobj.Add("WAREHOUSE_MOVEMENT_ID", warehouseMovementId);
                jobj.Add("WAREHOUSE_MOVEMENT_STATE", "D");
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

        static public bool releaseWarehouseMovementList(long warehouseMovementId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/releaseWarehouseMovementList.json";
                jobj.Add("WAREHOUSE_MOVEMENT_ID", warehouseMovementId);
                jobj.Add("WAREHOUSE_MOVEMENT_STATE", "R");
                jobj.Add("INVENTORY_STATE", "R");
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

        static public bool releaseWarehouseBarcodeList(List<long> listInventoryId, object warehousingWarehouseNo, object warehousingPalletNo, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/releaseWarehouseBarcodeList.json";
                jobj.Add("WAREHOUSING_WAREHOUSE_ID", ConvertUtil.ToString(warehousingWarehouseNo));
                jobj.Add("WAREHOUSING_PALLET_ID", ConvertUtil.ToString(warehousingPalletNo));
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
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

        static public bool getWarehouseMovementList(object registNo,
            object barcode,
            string warehouseMovementState,
            string dtFrom,
           string dtTo,
           string registDt,
           string releaseDt,
           string warehousingDt,
           object releaseWarehouseNo,
           object warehousingWarehouseNo,
           object palletNo,
           ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/getWarehouseMovementList.json";
                jobj.Add("WAREHOUSE_MOVEMENT", ConvertUtil.ToString(registNo));
                jobj.Add("RELEASE_WAREHOUSE_ID", ConvertUtil.ToString(releaseWarehouseNo));
                jobj.Add("WAREHOUSING_WAREHOUSE_ID", ConvertUtil.ToString(warehousingWarehouseNo));
                jobj.Add("RELEASE_PALLET_ID", ConvertUtil.ToString(palletNo));
                jobj.Add("BARCODE", ConvertUtil.ToString(barcode));
                jobj.Add("WAREHOUSE_MOVEMENT_STATE", warehouseMovementState);
                jobj.Add("CREATE_DT_FROM", dtFrom);
                jobj.Add("CREATE_DT_TO", dtTo);
                jobj.Add("REGIST_DT", registDt);
                jobj.Add("RELEASE_DT", releaseDt);
                jobj.Add("WAREHOUSING_DT", warehousingDt);
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

        static public bool getBarcodeList(long warehouseMovementId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/getBarcodeList.json";
                jobj.Add("WAREHOUSE_MOVEMENT_ID", warehouseMovementId);
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

        static public bool updatePalletBulk(long warehouseMovementId, string pallet, List<long> listInventoryId, string state, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/updatePalletBulk.json";
                jobj.Add("WAREHOUSE_MOVEMENT_ID", warehouseMovementId);
                jobj.Add("PALLET", pallet);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                jobj.Add("INVENTORY_STATE", state);
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

        static public bool getComponentList(object location, List<string> listPallet, List<string> listComponentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/getComponentList.json";

                jobj.Add("LOCATION", ConvertUtil.ToString(location));
                jobj.Add("LIST_PALLET", string.Join(",", listPallet));
                jobj.Add("LIST_COMPONENT_CD", string.Join(",", listComponentCd));

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

        static public bool getRecentReturnCheck(object serialNo, object systemSn, object macAddress, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getRecentReturnCheck.json";

                jobj.Add("SERIAL_NO", ConvertUtil.ToString(serialNo));
                jobj.Add("SYSTEM_SN", ConvertUtil.ToString(systemSn));
                jobj.Add("MAC_ADDRESS", ConvertUtil.ToString(macAddress));

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

        static public bool getConsignedAdjustmentSummary(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getConsignedAdjustmentSummary.json";

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

        static public bool getConsignedAdjustmentDetail(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/consigned/getConsignedAdjustmentDetail.json";

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



        static public bool putConsignedCheckInfo(object proxyId, string contents, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/putConsignedCheckInfo.json";

                jobj.Add("PROXY_ID", ConvertUtil.ToString(proxyId));
                jobj.Add("CONTENTS", contents);
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

        static public bool getCompanyInventoryList(long companyId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/getCompanyInventoryList.json";

                jobj.Add("COMPANY_ID", companyId);
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

        static public bool changeCompanyComponentVisible(long companyId, List<object> listCompanyComponentId, int visibleYn, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/consigned/companyComponentVisibleChange.json";

                jobj.Add("COMPANY_ID", companyId);
                jobj.Add("LIST_COMPANY_COMPONENT_ID", string.Join(",", listCompanyComponentId));
                jobj.Add("VISIBLE_YN", visibleYn);
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


        static public bool getUsedPurchaseList(JObject jData, ref JObject jResult)
        {
            try
            {
                string url = "/UsedPurchase/getUsedPurchaseList.json";

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

        static public bool getUsedPurchaseShowPrint(object receiptiD, object receipt, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPurchaseShowPrint.json";

                jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(receiptiD));
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

        static public bool getUsedPurchaseDanawaShowPrint(object receiptiD, object receipt, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/UsedPurchase/getUsedPurchaseDanawaShowPrint.json";

                jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(receiptiD));
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

        


        static public bool getWarehousingPart(string representativeNo, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingPart.json";

                jobj.Add("WAREHOUSING", representativeNo);
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

        static public bool getWarehousingComponentPart(string representativeNo, ref JObject jResult, long warehousingId = -1)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingComponentPart.json";

                
                jobj.Add("USER_ID", ProjectInfo._userId);

                if(warehousingId > 0)
                    jobj.Add("WAREHOUSING_ID", warehousingId);
                else
                    jobj.Add("WAREHOUSING", representativeNo);

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

        static public bool getWarehousingInventoryPart(long representativeId, object componentId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingInventoryPart.json";

                jobj.Add("WAREHOUSING_ID", representativeId);
                jobj.Add("COMPONENT_ID", ConvertUtil.ToInt64(componentId));
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

        static public bool updateInventoryDetail(JObject jPartInfo, ref JObject jResult)
        {
            try
            {
                string url = "/compInven/updateInventoryDetail.json";

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

        static public bool getComponentAll(string componentCd, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/getComponentAll.json";

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

        static public bool getWarehousingProduct(string representativeNo, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingProduct.json";

                jobj.Add("WAREHOUSING", representativeNo);
                jobj.Add("TYPE", 1);
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

        static public bool updateProductPrice(JObject jPartInfo, ref JObject jResult)
        {
            try
            {
                string url = "/produce/updateProductPrice.json";

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

        static public bool updateProductPriceBulk(List<string> listBarcode, List<long> listPrice, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();

                string url = "/produce/updateProductPriceBulk.json";

                jobj.Add("LIST_BARCODE", string.Join(",", listBarcode));
                jobj.Add("LIST_PRICE", string.Join(",", listPrice));
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

        static public bool updateInventorytPriceBulk(List<string> listBarcode, List<long> listPrice, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();

                string url = "/inventory/updateInventorytPriceBulk.json";

                jobj.Add("LIST_BARCODE", string.Join(",", listBarcode));
                jobj.Add("LIST_PRICE", string.Join(",", listPrice));
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

        static public bool getInventoryList(List<string> listBarcode, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();

                string url = "/compInven/getInventoryInfo.json";

                jobj.Add("LIST_BARCODE", string.Join(",", listBarcode));
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

        static public bool getInventoryInfo(long inventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();

                string url = "/compInven/getInventoryInfoOne.json";

                jobj.Add("INVENTORY_ID", inventoryId);
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

        static public bool deleteRepNo(long repType, List<string> listData,ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/user/deleteRepNo.json";
                jobj.Add("REP_TYPE", repType);
                jobj.Add("LIST_REP_NO", string.Join(",", listData));
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

        static public bool InsertRepNo(long repType, string data, int seq, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/user/InsertRepNo.json";
                jobj.Add("REP_TYPE", repType);
                jobj.Add("REP_NO", data);
                jobj.Add("SEQ", seq);
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

        static public bool updateRepNo(long repType, List<string> listData, List<int> listSeq, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/user/updateRepNo.json";
                jobj.Add("REP_TYPE", repType);
                jobj.Add("LIST_REP_NO", string.Join(",", listData));
                jobj.Add("LIST_SEQ", string.Join(",", listSeq));
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

        static public bool getRepNo(long repType, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/user/getRepNo.json";
                jobj.Add("REP_TYPE", repType);
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

        static public bool getWarehousing(long inventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousing.json";
                jobj.Add("INVENTORY_ID", inventoryId);
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

        static public bool getProductList(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/getProductSearchList.json";
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

        static public bool updateProductInfo(JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/updateProductInfo.json";
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


        static public bool moveProduct(string warehousing, JObject jobj, ref JObject jResult)
        {
            try
            {
                string url = "/product/moveProduct.json";
                jobj.Add("WAREHOUSING", warehousing);
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

        static public bool checkWarehouseMovement(string location, int pallet, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/warehouse/checkWarehouseMovement.json";

                jobj.Add("LOCATION", location);
                jobj.Add("PALLET", pallet);
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

        static public bool updateTblSerialNo(long inventoryId, string serialNo,ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/compInven/updateTblSerialNo.json";

                jobj.Add("INVENTORY_ID", inventoryId);
                jobj.Add("COMPONENT_CD", "TBL");
                jobj.Add("USER_ID", ProjectInfo._userId);
                jobj.Add("SERIAL_NO", serialNo);

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

        static public bool getProductInfo(long inventoryId, ref JObject jResult)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/product/getProductInfo.json";

                jobj.Add("INVENTORY_ID", inventoryId);
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
