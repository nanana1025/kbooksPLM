/// <summary>
/// WM_GUI 클래스중 data controll 부분을 따로 분리하여 놓음.
/// </summary>
/// 
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using Enum = System.Enum;

namespace WareHousingMaster
{
    public partial class WM_GUI
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                                      int size, string filePath);
        bool GetINIInfo(ref string id, ref string pw, ref bool autoLogin,  ref string location, ref string printer)
        {
            int ret = 0;
            string local_path = System.Windows.Forms.Application.StartupPath + @"\login.ini";

            // printer
            StringBuilder str_builder = new StringBuilder();
            ret = GetPrivateProfileString("PRINTER", "PRINTER", null, str_builder, 32, local_path);
            if (ret < 0)
            {
                MessageBox.Show("Unable to get PRINTER informationr! error num = " + ret);
                return false;
            }

            printer = str_builder.ToString();

            // id            
            ret = GetPrivateProfileString("LOGIN", "ID", null, str_builder, 32, local_path);
            if (ret < 0)
            {
                MessageBox.Show("Unable to get ID information! error num = " + ret);
                return false;
            }

            id = str_builder.ToString();

            // password            
            ret = GetPrivateProfileString("LOGIN", "PW", null, str_builder, 32, local_path);
            if (ret < 0)
            {
                MessageBox.Show("Unable to get PW information! error num = " + ret);
                return false;
            }

            pw = str_builder.ToString();

            //AUTO LOGIN
            ret = GetPrivateProfileString("LOGIN", "AUTO", null, str_builder, 32, local_path);
            if (ret < 0)
            {
                MessageBox.Show("Unable to get Auto login information! error num = " + ret);
                return false;
            }

            autoLogin = str_builder.ToString().Equals("true")?true:false;

            // location
            ret = GetPrivateProfileString("INFO", "LOCATION", null, str_builder, 32, local_path);
            if (ret < 0)
            {
                MessageBox.Show("Unable to get PW information! error num = " + ret);
                return false;
            }

            location = str_builder.ToString();

            return true;
        }

        /// <summary>
        /// 서버에 메시지 전송하는 함수
        /// </summary>
        /// <param name="msg_jobj">전송할 json데이터</param>
        /// <param name="url">메인 url 뒤에 붙을 path </param>
        /// <returns></returns>
        string SendRequestMessage(JObject msg_jobj, string url)
        {
            if (url.Contains("print"))
            {
                string print_port = combo_printer_.SelectedValue.ToString();
                msg_jobj.Add("PORT", print_port);
            }


#if DEBUG
            url = "http://211.202.79.253:3691" + url;
#else
            url = "http://dangol365.com" + url;
#endif
            string result_msg = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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

                // Display the status.  
                //Debug.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.  
                // The using block ensures the stream is automatically closed.                
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    result_msg = reader.ReadToEnd();
                }

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "server error");
                ExitProgram();
            }
            
            return result_msg;
        }

        string SendRequest(JObject msg_jobj, string url)
        {

#if DEBUG
            url = "http://211.202.79.253:3691" + url;
#else
            url = "http://dangol365.com" + url;
#endif
            string result_msg = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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

                // Display the status.  
                //Debug.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.  
                // The using block ensures the stream is automatically closed.                
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    result_msg = reader.ReadToEnd();
                }

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "server error");
                ExitProgram();
            }

            return result_msg;
        }

        /// <summary>
        /// login 정보 확인 요청
        /// </summary>
        /// <param name="id">ini에서 가져온 사용자 아이디</param>
        /// <param name="pw">사용자 비밀번호</param>
        /// <returns></returns>
        private Dictionary<string, string> getVersion(string id)
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
        public bool SendUserCheckRequest(string id, string pw)
        {
            string url = "/member/loginCheckFromExternalSystem.json";

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
            }
            catch
            {
                return false;
            }

            return true;
        }

        bool SendPartInfo(string msg, StringBuilder sb)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/log/getInventoryLog.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("TYPE", 1);
                jobj.Add("MSG", msg);
                jobj.Add("CONTENT", sb.ToString());

                // 요청 전송
                string result = SendRequest(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                //MessageBox.Show(jobj["MSG"].ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type_cd"></param>
        /// <returns></returns>
        private bool AddDeviceInfo(string type_cd, string representativeType)
        {
            string part_type = "";

            part_type = GetPartType();
            if (part_type.Equals("MEM") || part_type.Equals("STG") || part_type.Equals("MON") || part_type.Equals("VGA"))
            {
                part_type = part_type + "_" + summary_grid_.CurrentCell.RowIndex;
            }

            return (SendDeviceInfo(type_cd, part_type, representativeType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type_cd"></param>
        /// <returns></returns>
        private bool UpdateDeviceInfo(string type_cd, string representativeType)
        {
            string part_type = "";

            part_type = GetPartType();
            if (part_type.Equals("MEM") || part_type.Equals("STG") || part_type.Equals("MON") || part_type.Equals("VGA"))
            {
                part_type = part_type + "_" + summary_grid_.CurrentCell.RowIndex;
            }

            return (SendDeviceInfo(type_cd, part_type, representativeType));
        }

        

        /// <summary>
        /// Dictionary에 담긴 정보를 모두 보여주지 않기 때문에
        /// EndKey를 정하여 보여줄 정보를 제한함
        /// </summary>
        /// <param name="part_type"></param>
        /// <returns></returns>
        private string GetEndKey(ref string part_type)
        {
            string end_key = "";
            if (part_type.Equals("CPU"))
            {
                end_key = "CPU_END";
            }
            else if (part_type.Equals("MBD"))
            {
                end_key = "MBD_END";
            }
            else if (part_type.Equals("MEM"))
            {
                if (selected_button_ != (int)PART_NAME.ALL)
                {
                    int selected_index = summary_grid_.CurrentCell.RowIndex; //start 0 
                    part_type = part_type + "_" + selected_index; // exe MEM_0                    
                }

                end_key = part_type + "_END";
            }
            //else if (part_type.Equals("VGA"))
            //{
            //    end_key = "VGA_MEM_VENDOR";
            //}
            else //mem, stg, mon, vga
            {
                // end key, All Menu가 아닐 때에는  party_type에 번호가 없음.
                if (selected_button_ != (int)PART_NAME.ALL)
                {
                    int selected_index = summary_grid_.CurrentCell.RowIndex; //start 0 
                    part_type = part_type + "_" + selected_index; // exe MEM_0                    
                }

                end_key = part_type + "_END";
            }

            return end_key;
        }

        /// <summary>
        /// 선택된 탭에 해당하는 부품타입을 가져온다
        /// </summary>
        /// <returns></returns>
        string GetPartType()
        {
            if (selected_button_ != (int)PART_NAME.ALL)
            {
                return Enum.GetValues(typeof(PART_NAME)).GetValue(selected_button_).ToString();
            }
            else
            {
                int selected_index = summary_grid_.CurrentCell.RowIndex;
                return summary_grid_[1, selected_index].Value.ToString();
            }
        }

        bool SendDeviceInfo(string type_cd, string part_type, string representativeType)
        {
            try
            {
                if (!InsertPartInfoQuery(type_cd, part_type, representativeType))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return false;
            }
        }

        bool InsertPartInfoQuery(string type_cd, string part_type, string representativeType)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "";

                if (type_cd.Equals("CPN"))
                {
                    url = "/compInven/insertComponent.json";
                }
                else if (type_cd.Equals("UPT"))
                {
                    url = "/compInven/updateComponent.json";
                    type_cd = "CPN";
                }
                else if (type_cd.Equals("IVT"))
                {
                    url = "/compInven/insertInventory.json";

                    if(representativeType.Equals("O"))
                        jobj.Add("RELEASES", w_numb_txt_.Text);
                    else if (representativeType.Equals("W"))
                        jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                    else if (representativeType.Equals("C"))
                        jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                    else if (representativeType.Equals("P"))
                        jobj.Add("RECEIPT", w_numb_txt_.Text);
                    else 
                        jobj.Add("WAREHOUSING", w_numb_txt_.Text);

                    // 재고 정보 추가시에만 location 정보 추가
                    jobj.Add("LOCATION", combo_location_.SelectedValue.ToString());
                }




                jobj.Add("REPRESENTATIVE_TYPE", representativeType);

                jobj.Add("COMPONENT_CD", part_type.Substring(0, 3));

                AddDeviceDataIntoJObject(part_type, ref jobj);

                jobj.Add("USER_ID", lbl_username.Text);

                // 요청 전송
                string result = SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);
                if (!type_cd.Equals("CPN") && Convert.ToBoolean(jobj["SUCCESS"])) // IVT
                {
                    // 제품번호 추가 
                    if (jobj.ContainsKey("COMPONENT"))
                    {
                        lbl_product_no.Text = jobj["COMPONENT"].ToString();
                    }

                    // 바코드 번호 추가
                    lbl_superviseno.Text = jobj["BARCODE"].ToString();
                    barcode_list_[part_type + "_BARCODE"] = jobj["BARCODE"].ToString();
                }

                if (selected_button_ != (int)PART_NAME.ALL)
                {
                    MessageBox.Show(jobj["MSG"].ToString());
                }
                else
                {
                    string isSuccess = jobj["SUCCESS"].ToString();

                    if (isSuccess.Equals("True"))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        void AddDeviceDataIntoJObject(string part_type, ref JObject jobj)
        {
            foreach (var data in device_info_dic_)
            {
                // part_type
                if (data.Key.Contains(part_type))
                {
                    if (data.Key.Contains(part_type) && (data.Value.db_name != "" && data.Value.db_name != null))
                    {
                        if (data.Value.value == null)
                        {
                            if (jobj.ContainsKey(data.Value.db_name))
                                jobj[data.Value.db_name] = "";
                            else
                                jobj.Add(data.Value.db_name, "");
                        }
                        else
                        {
                            if (jobj.ContainsKey(data.Value.db_name))
                                jobj[data.Value.db_name] = data.Value.value;
                            else
                                jobj.Add(data.Value.db_name, data.Value.value);
                        }
                    }
                }
            }

            jobj.Add(device_info_dic_["TYPE"].db_name, device_info_dic_["TYPE"].value);
        }

        bool SendCheckComponentRequest(string part_type, ref JObject matching_result, ref bool add_btn_state)
        {
            try
            {
                string url = "/compInven/checkComponent.json";

                JObject jobj = new JObject();

                jobj.Add("COMPONENT_CD", part_type.Substring(0, 3));

                AddDeviceDataIntoJObject(part_type, ref jobj);

                jobj.Add("LOCATION", lbl_location.Text);
                jobj.Add("USER_ID", lbl_username.Text);

                string result = SendRequestMessage(jobj, url);

                JObject result_obj = JObject.Parse(result);
                if (!Convert.ToBoolean(result_obj["SUCCESS"]))
                {
                    if (result_obj["ERRORCODE"].ToString().Equals("1"))
                    {
                        // 이미 등록된 상품일 경우
                        matching_result = result_obj;
                        lbl_product_no.Text = result_obj["COMPONENT"].ToString();
                    }
                    else
                    {
                        MessageBox.Show(result_obj["MSG"].ToString());
                    }

                    // 부품정보 추가 버튼 상태
                    add_btn_state = false;
                }
                else
                {
                    add_btn_state = true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
        
        bool InsertNtbCheck(string barcode, Dictionary<string, short> dicNtbCheck, List<string> listInventory)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/NtbCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("TYPE", "2");
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
                jobj.Add("C_INVENTORY", string.Join(",", listInventory));

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
        bool InsertNtbCheck2ndEdition(string barcode, Dictionary<string, short> dicNtbCheck, List<string> listInventory, string caseDestroyDescription, string batteryRemain)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/NtbCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("TYPE", "2");
                jobj.Add("CASE_DESTROYED", dicNtbCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicNtbCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicNtbCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicNtbCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicNtbCheck["CASE_DISCOLORED"]);
                jobj.Add("CASE_HINGE", dicNtbCheck["CASE_HINGE"]);
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
                jobj.Add("C_INVENTORY", string.Join(",", listInventory));

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

        bool InsertAllInOneCheck(string barcode, Dictionary<string, short> dicAllInOneCheck, List<string> listInventory)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/AllInOneCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("TYPE", "3");
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
                jobj.Add("C_INVENTORY", string.Join(",", listInventory));

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

        bool InsertMonitorCheck(string barcode, Dictionary<string, int> dicMonitor)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/MonitorCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("CASE_DESTROYED", dicMonitor["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicMonitor["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicMonitor["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicMonitor["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicMonitor["CASE_DISCOLORED"]);
                jobj.Add("DISPLAY", dicMonitor["DISPLAY"]);
                jobj.Add("PORT", dicMonitor["PORT"]);
                jobj.Add("ADAPTER", dicMonitor["ADAPTER"]);

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

        bool InsertCasCheck(string barcode, Dictionary<string, int> dicinventory)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/CaseCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("CASE_DESTROYED", dicinventory["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicinventory["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicinventory["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicinventory["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicinventory["CASE_DISCOLORED"]);

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

        bool InsertInventoryCheck(string barcode, string ComponentCd, Dictionary<string, int> dicinventory)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/inventoryCheck/InventoryCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("COMPONENT_CD", ComponentCd);
                jobj.Add("FAULT", dicinventory["FAULT"]);

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

        bool getWarehousingInfo(string warehousing, ref DateTime _warehousingDate)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/produce/getWarehousingInfo.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("WAREHOUSING", warehousing);

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

            return true;
        }


        bool PrintNtbCheck(string barcode, Dictionary<string, short> dicNtbCheck, int port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printNtbCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("MBD", barcode);
                jobj.Add("TYPE", "2");
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
                string result = SendRequest(jobj, url);

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

        bool PrintNtbCheck2ndEdition(string barcode, Dictionary<string, short> dicNtbCheck, string caseDestroyDescription, string batteryRemain, int port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printNtbCheck2ndEdition.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("MBD", barcode);
                jobj.Add("TYPE", "2");
                jobj.Add("EDITION", "2");
                jobj.Add("PORT", port);
                jobj.Add("CASE_DESTROYED", dicNtbCheck["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicNtbCheck["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicNtbCheck["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicNtbCheck["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicNtbCheck["CASE_DISCOLORED"]);
                jobj.Add("CASE_HINGE", dicNtbCheck["CASE_HINGE"]);
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

                // 요청 전송
                string result = SendRequest(jobj, url);

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

        bool PrintAllInOneCheck(string barcode, Dictionary<string, short> dicAllInOneCheck, int port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printAllInOneCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("MBD", barcode);
                jobj.Add("TYPE", "3");
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
                string result = SendRequest(jobj, url);

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

        bool PrintMonitorCheck(string barcode, Dictionary<string, int> dicMonitor, string componentCd, int port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printInventoryCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("PORT", port);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("CASE_DESTROYED", dicMonitor["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicMonitor["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicMonitor["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicMonitor["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicMonitor["CASE_DISCOLORED"]);
                jobj.Add("DISPLAY", dicMonitor["DISPLAY"]);
                jobj.Add("CPORT", dicMonitor["PORT"]);
                jobj.Add("ADAPTER", dicMonitor["ADAPTER"]);

                // 요청 전송
                string result = SendRequest(jobj, url);

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

        bool PrintCasCheck(string barcode, Dictionary<string, int> dicMonitor, string componentCd, int port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printInventoryCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("PORT", port);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("CASE_DESTROYED", dicMonitor["CASE_DESTROYED"]);
                jobj.Add("CASE_SCRATCH", dicMonitor["CASE_SCRATCH"]);
                jobj.Add("CASE_STABBED", dicMonitor["CASE_STABBED"]);
                jobj.Add("CASE_PRESSED", dicMonitor["CASE_PRESSED"]);
                jobj.Add("CASE_DISCOLORED", dicMonitor["CASE_DISCOLORED"]);


                // 요청 전송
                string result = SendRequest(jobj, url);

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

        bool PrintInventoryCheck(string barcode, Dictionary<string, int> dicinventory, string componentCd, int port)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/print/printInventoryCheck.json";

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("BARCODE", barcode);
                jobj.Add("PORT", port);
                jobj.Add("COMPONENT_CD", componentCd);
                jobj.Add("FAULT", dicinventory["FAULT"]);

                // 요청 전송
                string result = SendRequest(jobj, url);

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
    }
}
