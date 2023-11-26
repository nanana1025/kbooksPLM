using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WareHousingMaster.view.common
{
    static class Util
    {
        //static DeviceDatasController data_controller;

        //[DllImport("kernel32")]
        //[DllImport("kernel32.dll", SetLastError = true)]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public static bool GetINIInfo()
        {
            //ssl 무시하는 부분임. 향후 ssl 구매후 삭제 예정
            IgnoreBadCertificates();

            IniFile ini = new IniFile();

            //int ret = 0;
            string local_path = System.Windows.Forms.Application.StartupPath + @"\login.ini";

            ini.Load(local_path);

            // printer
            //StringBuilder str_builder = new StringBuilder();
            //ret = GetPrivateProfileString("PRINTER", "PRINTER", null, str_builder, 32, local_path);
            //if (ret < 0)
            //{
            //    MessageBox.Show("Unable to get PRINTER informationr! error num = " + ret);
            //    return false;
            //}

            //ProjectInfo._printerPort = str_builder.ToString();

            //ProjectInfo._printerPort = ini["PRINTER"]["PRINTER"].ToString();

            // id            
            //ret = GetPrivateProfileString("LOGIN", "ID", null, str_builder, 32, local_path);
            //if (ret < 0)
            //{
            //    MessageBox.Show("Unable to get ID information! error num = " + ret);
            //    return false;
            //}

            //ProjectInfo._userId = str_builder.ToString();

            ProjectInfo._userId = ini["LOGIN"]["ID"].ToString();

            // password            
            //ret = GetPrivateProfileString("LOGIN", "PW", null, str_builder, 32, local_path);
            //if (ret < 0)
            //{
            //    MessageBox.Show("Unable to get PW information! error num = " + ret);
            //    return false;
            //}

            //ProjectInfo._userPasswd = str_builder.ToString();
            ProjectInfo._userPasswd = ini["LOGIN"]["PW"].ToString();

            //AUTO LOGIN
            //ret = GetPrivateProfileString("LOGIN", "AUTO", null, str_builder, 32, local_path);
            //if (ret < 0)
            //{
            //    MessageBox.Show("Unable to get Auto login information! error num = " + ret);
            //    return false;
            //}

            //ProjectInfo._autoLogin = str_builder.ToString().Equals("true") ? true : false;
            object check = ini["LOGIN"]["AUTO"].ToString();

            if (check != null)
            {
                ProjectInfo._autoLogin = ini["LOGIN"]["AUTO"].ToString().ToUpper().Equals("TRUE") ? true : false;
            }
            else
            {
                ProjectInfo._autoLogin = false;
            }

            // location
            //ret = GetPrivateProfileString("INFO", "LOCATION", null, str_builder, 32, local_path);
            //if (ret < 0)
            //{
            //    MessageBox.Show("Unable to get PW information! error num = " + ret);
            //    return false;
            //}

            //ProjectInfo._location = str_builder.ToString();
            //ProjectInfo._location = ini["INFO"]["LOCATION"].ToString();
            //ProjectInfo._dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
            //if (ProjectInfo._dtLocation.Rows.Count > 0)
            //{
            //    DataRow[] rows = ProjectInfo._dtLocation.Select($"VALUE = '{ProjectInfo._location}'");
            //    if (rows.Length < 1)
            //        ProjectInfo._locationId = ConvertUtil.ToString(ProjectInfo._dtLocation.Rows[0]["KEY"]);
            //    else
            //        ProjectInfo._locationId = ConvertUtil.ToString(rows[0]["KEY"]);
            //}
            //else
            //{
            //    ProjectInfo._locationId = "-1";
            //}

            //ret = GetPrivateProfileString("INFO", "PALLET", null, str_builder, 32, local_path);
            //if (ret < 0)
            //{
            //    MessageBox.Show("Unable to get PW information! error num = " + ret);
            //    return false;
            //}

            //ProjectInfo._pallet = ini["INFO"]["PALLET"].ToString();

            //ProjectInfo._dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            //if (ProjectInfo._dtPallet.Rows.Count > 0)
            //{
            //    DataRow[] rows = ProjectInfo._dtPallet.Select($"PALLET_NM = '{ProjectInfo._pallet}'");
            //    if (rows.Length < 1)
            //        ProjectInfo._palletId = ConvertUtil.ToString(ProjectInfo._dtPallet.Rows[0]["PALLET_ID"]);
            //    else
            //        ProjectInfo._palletId = ConvertUtil.ToString(rows[0]["PALLET_ID"]);
            //}
            //else
            //{
            //    ProjectInfo._palletId = "-1";
            //}

            //ProjectInfo._dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_NM ASC");


            return true;
        }

        static public void getViewType(DataTable dtCategory, DataTable dtView)
        {

            dtCategory.Columns.Add(new DataColumn("NAME", typeof(string)));
            dtCategory.Columns.Add(new DataColumn("VIEW_YN", typeof(int)));

            dtView.Columns.Add(new DataColumn("NAME", typeof(string)));
            dtView.Columns.Add(new DataColumn("VIEW_YN", typeof(int)));

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            string url = "/board/getViewType.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (ConvertUtil.ToBoolean(jResult["CATEGORY_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["CATEGORY_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = dtCategory.NewRow();

                        dr["NAME"] = ConvertUtil.ToString(obj["NAME"]);
                        dr["VIEW_YN"] = ConvertUtil.ToInt32(obj["VIEW_YN"]);

                        dtCategory.Rows.Add(dr);
                    }
                }

                if (ConvertUtil.ToBoolean(jResult["VIEW_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["VIEW_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = dtView.NewRow();

                        dr["NAME"] = ConvertUtil.ToString(obj["NAME"]);
                        dr["VIEW_YN"] = ConvertUtil.ToInt32(obj["VIEW_YN"]);

                        dtView.Rows.Add(dr);
                    }
                }
            }
        }

        static public string getValue(int type, int category)
        {

            DataTable dtValue = Util.getTable("TN_VALUE_MGNT", "TYPE,CATEGORY,VALUE", $"TYPE ={type} AND CATEGORY ={category}", "TYPE ASC");

            //DataRow[] rows = dtValue.Select($"TYPE = {(int)Enum.VALUE_MGNT_TYPE.BASIC} AND CATEGORY = {(int)Enum.VALUE_MGNT_CATEGORY.SEARCH_DATE}");

            if (dtValue.Rows.Count > 0)
            {
               return ConvertUtil.ToString(dtValue.Rows[0]["VALUE"]);
            }
            else
            {
                return "";
            }
        }

        public static bool GetUserAuthority()
        {
            if (ProjectInfo._LIST_USER_AUTHORTY_TAB == null) ProjectInfo._LIST_USER_AUTHORTY_TAB = new List<string>();
            else ProjectInfo._LIST_USER_AUTHORTY_TAB.Clear();

            if (ProjectInfo._LIST_USER_AUTHORTY == null) ProjectInfo._LIST_USER_AUTHORTY = new List<string>();
            else ProjectInfo._LIST_USER_AUTHORTY.Clear();

            ProjectInfo._USER_TYPE = "";

            DataTable dtUserInfo = getTable("ID_PASS", "USER_ID,PROGRAM_ID", $"USER_ID = '{ProjectInfo._userId}'", "PROGRAM_ID ASC");

            if (dtUserInfo.Rows.Count > 0)
            {
                string programId;
                string programTabId;

                ProjectInfo._USER_TYPE = "USER";

                foreach (DataRow row in dtUserInfo.Rows)
                {
                    programId = ConvertUtil.ToString(row["PROGRAM_ID"]);

                    if (string.IsNullOrWhiteSpace(programId))
                    {
                        ProjectInfo._USER_TYPE = "ADMIN";
                        break;
                    }
                    else
                    {
                        programTabId = Regex.Replace(programId, @"\d", "");//문자 추출

                        if (!ProjectInfo._LIST_USER_AUTHORTY_TAB.Contains(programTabId))
                            ProjectInfo._LIST_USER_AUTHORTY_TAB.Add(programTabId);

                        if (!ProjectInfo._LIST_USER_AUTHORTY.Contains(programId))
                            ProjectInfo._LIST_USER_AUTHORTY.Add(programId);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
             = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }
        private static bool AcceptAllCertifications
        (
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors
        )
        {
            return true;
        }

        static public void ExitProgram()
        {
            Application.ExitThread();
            Environment.Exit(0);
        }

        static public bool checkOnlyNumeric(string data)
        {
            string strNum = Regex.Replace(data, @"\D", "");
            return !string.IsNullOrWhiteSpace(strNum);
        }


        static public void setPasswd(string passwd)
        {
            string local_path = System.Windows.Forms.Application.StartupPath + @"\login.ini";
            WritePrivateProfileString("LOGIN", "PW", passwd, local_path);
        }

        static public void GetSystemInfo()
        {
            //data_controller = new DeviceDatasController();
            //if (!data_controller.LoadCPUZDll())
            //{
            //    MessageBox.Show($"CPUZ DLL을 로드하는 과정에서 오류가 발생했습니다. 관리자에게 문의하세요.");
            //    ExitProgram();
            //}

            //ProjectInfo._listDeviceInfo = new List<DeviceInfo>();
            //ProjectInfo._dicPartCnt = new Dictionary<string, int>();
            //bool isSuccess = data_controller.GetPCInfo();
            //ProjectInfo._dicPartCnt = data_controller._deviceCnt;
            //makePartInfo(data_controller._msg, ProjectInfo._listDeviceInfo);

            //if (!isSuccess) // device data read
            //{
            //    MessageBox.Show($"부품정보를 로드하는 과정에서 오류가 발생했습니다. 관리자에게 문의하세요.\n{data_controller._msg}");
            //    ExitProgram();
            //}

            //InitDataTable();
            //SetDeviceTable();
            //GetDeviceKeyInfo();
        }

        static public void GetSystemInfoWOMatchingInfo()
        {
            //data_controller = new DeviceDatasController();
            //if (!data_controller.LoadCPUZDll())
            //{
            //    MessageBox.Show($"CPUZ DLL을 로드하는 과정에서 오류가 발생했습니다. 관리자에게 문의하세요.");
            //    ExitProgram();
            //}

            //ProjectInfo._listDeviceInfo = new List<DeviceInfo>();
            //ProjectInfo._dicPartCnt = new Dictionary<string, int>();
            //bool isSuccess = data_controller.GetPCInfo();
            //ProjectInfo._dicPartCnt = data_controller._deviceCnt;
            //makePartInfo(data_controller._msg, ProjectInfo._listDeviceInfo);

            //if (!isSuccess) // device data read
            //{
            //    MessageBox.Show($"부품정보를 로드하는 과정에서 오류가 발생했습니다. 관리자에게 문의하세요.\n{data_controller._msg}");
            //    ExitProgram();
            //}

            //InitDataTable();
            //SetDeviceTable();
            //GetDeviceKeyInfoWOMatchingInfo();
        }



        //static private void makePartInfo(string msg, List<DeviceInfo> partInfo)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    foreach (KeyValuePair<string, int> items in ProjectInfo._dicPartCnt)
        //    {
        //        string key = items.Key;
        //        int value = items.Value;

        //        sb.Append($"{key}:{value}/");
        //    }

        //    foreach (DeviceInfo part in partInfo)
        //    {
        //        string key = part.name;
        //        string value = part.value;
        //        if (string.IsNullOrEmpty(value))
        //            value = "NA";

        //        sb.Append($"{key}:{value}/");
        //    }

        //    DBConnect.SendPartInfo(msg, sb);
        //}

        static public string getDateTime(DateEdit dt)
        {
            DateTime orderTime;
            string dtDate = "";
            if (dt.EditValue != null && !string.IsNullOrEmpty(dt.EditValue.ToString()))
                dtDate = $"{dt.Text} 00:00:00";

            if (string.IsNullOrWhiteSpace(dtDate))
            {
                return dtDate;
            }
            else
            {
                orderTime = Convert.ToDateTime(dtDate);
                return orderTime.ToString("yyyyMMdd");
            }
        }

        static public TreeListNode getTreeListNode(TreeList tl, string FieldNm, object value)
        {
            TreeListNode myNode = null;

            myNode = tl.FindNode((node) => {
                return node[FieldNm].ToString().Equals(ConvertUtil.ToString(value));
            }
              );

            return myNode;
        }

        static public void LookupEditHelper(CheckedComboBoxEdit Edit, DataTable dt, string key, string value)
        {
            try
            {
                // 데이터 바인딩
                Edit.Properties.DataSource = new BindingSource(dt, null);
                Edit.Properties.DisplayMember = value;
                Edit.Properties.ValueMember = key;

                //Edit.Properties.Columns.Clear();
                //Edit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value));

                //Edit.Properties.ShowHeader = false;
            }
            catch (Exception e)
            {
                Edit.Properties.DataSource = null;
            }
        }

        static public void LookupEditHelper(CheckedComboBoxEdit Edit, BindingSource bs, string key, string value)
        {
            try
            {
                // 데이터 바인딩
                Edit.Properties.DataSource = new BindingSource(bs, null);
                Edit.Properties.DisplayMember = value;
                Edit.Properties.ValueMember = key;
            }
            catch (Exception e)
            {
                Edit.Properties.DataSource = null;
            }
        }


        static public void LookupEditHelper(LookUpEdit lookUpEdit, DataTable dt, string key, string value)
        {
            try
            {
                // 데이터 바인딩
                lookUpEdit.Properties.DataSource = new BindingSource(dt, null);
                lookUpEdit.Properties.DisplayMember = value;
                lookUpEdit.Properties.ValueMember = key;

                lookUpEdit.Properties.Columns.Clear();
                lookUpEdit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value));

                lookUpEdit.Properties.ShowHeader = false;
            }
            catch (Exception e)
            {
                lookUpEdit.Properties.DataSource = null;
            }
        }
        static public void LookupEditHelper(LookUpEdit lookUpEdit, BindingSource bs, string key, string value)
        {
            try
            {
                // 데이터 바인딩
                lookUpEdit.Properties.DataSource = new BindingSource(bs, null);
                lookUpEdit.Properties.DisplayMember = value;
                lookUpEdit.Properties.ValueMember = key;

                lookUpEdit.Properties.Columns.Clear();
                lookUpEdit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value));

                lookUpEdit.Properties.ShowHeader = false;
            }
            catch (Exception e)
            {
                lookUpEdit.Properties.DataSource = null;
            }
        }

        static public void LookupEditHelper(RepositoryItemLookUpEdit lookUpEdit, DataTable dt, string key, string value)
        {
            try
            {
                // 데이터 바인딩
                lookUpEdit.DataSource = new BindingSource(dt, null);
                lookUpEdit.DisplayMember = value;
                lookUpEdit.ValueMember = key;

                lookUpEdit.Columns.Clear();
                lookUpEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value));

                lookUpEdit.ShowHeader = false;
            }
            catch (Exception e)
            {
                lookUpEdit.DataSource = null;
            }
        }

        static public void LookupEditHelper(RepositoryItemLookUpEdit lookUpEdit, BindingSource bs, string key, string value)
        {
            try
            {
                // 데이터 바인딩
                lookUpEdit.DataSource = new BindingSource(bs, null);
                lookUpEdit.DisplayMember = value;
                lookUpEdit.ValueMember = key;

                lookUpEdit.Columns.Clear();
                lookUpEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value));

                lookUpEdit.ShowHeader = false;
            }
            catch (Exception e)
            {
                lookUpEdit.DataSource = null;
            }
        }
        

        public static void LookupEditHelper(RepositoryItemSearchLookUpEdit editor, DataTable dt, string key, string value)
        {
            editor.BeginUpdate();
            editor.View.Columns.Clear();
            editor.DisplayMember = value;
            GridColumn col = editor.View.Columns.AddField(value);
            col.Visible = true;

            editor.ValueMember = key;

            editor.View.OptionsView.ShowGroupPanel = false;
            editor.View.OptionsView.ShowColumnHeaders = false;
            editor.ShowFooter = false;
            editor.ShowClearButton = false;
            editor.DataSource = dt;
            editor.EndUpdate();
        }

        /// <summary>
        /// DataTable 사용
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="type"></param>
        public static void LookupEditHelper(SearchLookUpEdit editor, DataTable dt, string key, string value)
        {

            editor.Properties.BeginUpdate();
            editor.Properties.View.Columns.Clear();
            editor.Properties.DisplayMember = value;
            GridColumn col = editor.Properties.View.Columns.AddField(value);
            col.Visible = true;

            editor.Properties.ValueMember = key;

            editor.Properties.View.OptionsView.ShowGroupPanel = false;
            editor.Properties.View.OptionsView.ShowColumnHeaders = false;
            editor.Properties.ShowFooter = false;
            editor.Properties.ShowClearButton = false;
            editor.Properties.DataSource = dt;
            editor.Properties.EndUpdate();
        }

        static public void LookupEditHelper(LookUpEdit lookUpEdit, Dictionary<int, string> dic)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("KEY", typeof(int)));
                dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                foreach (KeyValuePair<int, string> item in dic)
                {
                    DataRow dr = dt.NewRow();
                    dr["KEY"] = item.Key;
                    dr["VALUE"] = item.Value;
                    dt.Rows.Add(dr);
                }

                // 데이터 바인딩
                lookUpEdit.Properties.DataSource = new BindingSource(dt, null);
                lookUpEdit.Properties.DisplayMember = "VALUE";
                lookUpEdit.Properties.ValueMember = "KEY";

                lookUpEdit.Properties.Columns.Clear();
                lookUpEdit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VALUE"));

                lookUpEdit.Properties.ShowHeader = false;
            }
            catch (Exception e)
            {
                lookUpEdit.Properties.DataSource = null;
            }
        }
        static public void LookupEditHelper(RepositoryItemLookUpEdit lookUpEdit, Dictionary<int, string> dic)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("KEY", typeof(int)));
                dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                foreach (KeyValuePair<int, string> item in dic)
                {
                    DataRow dr = dt.NewRow();
                    dr["KEY"] = item.Key;
                    dr["VALUE"] = item.Value;
                    dt.Rows.Add(dr);
                }

                // 데이터 바인딩
                lookUpEdit.DataSource = new BindingSource(dt, null);
                lookUpEdit.DisplayMember = "VALUE";
                lookUpEdit.ValueMember = "KEY";

                lookUpEdit.Columns.Clear();
                lookUpEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VALUE"));

                lookUpEdit.ShowHeader = false;
            }
            catch (Exception e)
            {
                lookUpEdit.DataSource = null;
            }
        }

        static public void insertRowonTop(DataTable dt, string key, string value)
        {
            DataRow row = dt.NewRow();
            row["KEY"] = key;
            row["VALUE"] = value;
            dt.Rows.InsertAt(row, 0);
        }
        static public void insertRow(DataTable dt, string key, string value)
        {
            DataRow row = dt.NewRow();
            row["KEY"] = key;
            row["VALUE"] = value;
            dt.Rows.Add(row);
        }

        static public void insertRow(DataTable dt, int key, string value)
        {
            DataRow row = dt.NewRow();
            row["KEY"] = key;
            row["VALUE"] = value;
            dt.Rows.Add(row);
        }

        static public void insertRowonTop(DataTable dt, int key, string value)
        {
            DataRow row = dt.NewRow();
            row["KEY"] = key;
            row["VALUE"] = value;
            dt.Rows.InsertAt(row, 0);
        }

        static public void insertRowonTop(DataTable dt, long key, long value)
        {
            DataRow row = dt.NewRow();
            row["KEY"] = key;
            row["VALUE"] = value;
            dt.Rows.InsertAt(row, 0);
        }

        static public void insertRowonTop(DataTable dt, string keyCol, string valueCol, string key, string value)
        {
            DataRow row = dt.NewRow();
            row[keyCol] = key;
            row[valueCol] = value;
            dt.Rows.InsertAt(row, 0);
        }

        static public void insertRowonTop(DataTable dt, Dictionary<string, object> dicData)
        {
            DataRow row = dt.NewRow();

            foreach (KeyValuePair<string, object> items in dicData)
            {
                string col = items.Key;
                object value = items.Value;
                row[col] = value;
            }

            dt.Rows.InsertAt(row, 0);
        }



        static public DataTable getCodeList(string Code, string key, string value)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/getCodeList.json";

                jobj.Add("CODE", Code);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                JArray jResult = (JArray)jobj["DATA"];

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn(key, typeof(string)));
                dt.Columns.Add(new DataColumn(value, typeof(string)));

                foreach (JObject item in jResult)
                {
                    string codeCd = item.GetValue("CODE_CD").ToString();
                    string codeNm = item.GetValue("CODE_NM").ToString();

                    DataRow dr = dt.NewRow();

                    dr[key] = codeCd;
                    dr[value] = codeNm;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }
        static public DataTable getCodeListInt(string Code, string key, string value)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/getCodeList.json";

                jobj.Add("CODE", Code);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                JArray jResult = (JArray)jobj["DATA"];

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn(key, typeof(int)));
                dt.Columns.Add(new DataColumn(value, typeof(string)));

                foreach (JObject item in jResult)
                {
                    int codeCd = ConvertUtil.ToInt32(item.GetValue("CODE_CD"));
                    string codeNm = item.GetValue("CODE_NM").ToString();

                    DataRow dr = dt.NewRow();

                    dr[key] = codeCd;
                    dr[value] = codeNm;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }


        static public DataTable getCodeListCustom(string table, string key, string value, string condition = "1=1", string orderby = "")
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/getCodeListTable.json";

                jobj.Add("CUSTOM_TABLE", table);
                jobj.Add("CUSTOM_KEY", key);
                jobj.Add("CUSTOM_VALUE", value);
                jobj.Add("CUSTOM_CONDITION", condition);
                jobj.Add("CUSTOM_ORDER", orderby);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                JArray jResult = (JArray)jobj["DATA"];

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("KEY", typeof(string)));
                dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                foreach (JObject item in jResult)
                {
                    string codeCd = item.GetValue("K").ToString();
                    string codeNm = item.GetValue("V").ToString();

                    DataRow dr = dt.NewRow();

                    dr["KEY"] = codeCd;
                    dr["VALUE"] = codeNm;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }

        static public DataTable getCodeListCustomLS(string table, string key, string value, string condition = "1=1", string orderby = "")
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/getCodeListTable.json";

                jobj.Add("CUSTOM_TABLE", table);
                jobj.Add("CUSTOM_KEY", key);
                jobj.Add("CUSTOM_VALUE", value);
                jobj.Add("CUSTOM_CONDITION", condition);
                jobj.Add("CUSTOM_ORDER", orderby);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                JArray jResult = (JArray)jobj["DATA"];

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("KEY", typeof(long)));
                dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                foreach (JObject item in jResult)
                {
                    long codeCd = ConvertUtil.ToInt64(item.GetValue("K"));
                    string codeNm = item.GetValue("V").ToString();

                    DataRow dr = dt.NewRow();

                    dr["KEY"] = codeCd;
                    dr["VALUE"] = codeNm;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }



        static public DataTable getTable(string table, string column, string condition = "1=1", string orderby = "")
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/getTable.json";

                jobj.Add("CUSTOM_TABLE", table);
                jobj.Add("CUSTOM_COLUMN", column);
                jobj.Add("CUSTOM_CONDITION", condition);
                jobj.Add("CUSTOM_ORDER", orderby);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                JArray jResult = (JArray)jobj["DATA"];
                column = column.Replace(" ", "");
                string[] columns = column.Split(',');
                DataTable dt = new DataTable();

                foreach (string col in columns)
                    dt.Columns.Add(new DataColumn(col, typeof(string)));

                foreach (JObject item in jResult)
                {
                    DataRow dr = dt.NewRow();

                    foreach (string col in columns)
                        dr[col] = item.GetValue(col).ToString();

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }
        static public DataTable getTable(string table, string column, string condition = "1=1", string orderby = "", string groupBy = "")
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/getTable.json";

                jobj.Add("CUSTOM_TABLE", table);
                jobj.Add("CUSTOM_COLUMN", column);
                jobj.Add("CUSTOM_CONDITION", condition);
                jobj.Add("CUSTOM_ORDER", orderby);
                jobj.Add("CUSTOM_GROUP", groupBy);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                JArray jResult = (JArray)jobj["DATA"];
                column = column.Replace(" ", "");
                string[] columns = column.Split(',');
                DataTable dt = new DataTable();

                foreach (string col in columns)
                    dt.Columns.Add(new DataColumn(col, typeof(string)));

                foreach (JObject item in jResult)
                {
                    DataRow dr = dt.NewRow();

                    foreach (string col in columns)
                        dr[col] = item.GetValue(col).ToString();

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }

        static public DataTable getTable(string table, string column, string condition = "1=1", string orderby = "", List<string> listLongCol = null)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/getTable.json";

                jobj.Add("CUSTOM_TABLE", table);
                jobj.Add("CUSTOM_COLUMN", column);
                jobj.Add("CUSTOM_CONDITION", condition);
                jobj.Add("CUSTOM_ORDER", orderby);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                JArray jResult = (JArray)jobj["DATA"];
                column = column.Replace(" ", "");
                string[] columns = column.Split(',');
                DataTable dt = new DataTable();

                if (listLongCol == null)
                    foreach (string col in columns)
                        dt.Columns.Add(new DataColumn(col, typeof(string)));
                else
                    foreach (string col in columns)
                    {
                        if (listLongCol.Contains(col))
                            dt.Columns.Add(new DataColumn(col, typeof(long)));
                        else
                            dt.Columns.Add(new DataColumn(col, typeof(string)));
                    }



                foreach (JObject item in jResult)
                {
                    DataRow dr = dt.NewRow();

                    foreach (string col in columns)
                        dr[col] = item.GetValue(col).ToString();

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }



        static public DataTable setlookupByStaticList_Int_String(Dictionary<int, string> dicData, string emptyValue = "")
        {
            try
            {
                List<int> list = new List<int>();
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("KEY", typeof(int)));
                dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                if (!string.IsNullOrWhiteSpace(emptyValue))
                {
                    DataRow dr = dt.NewRow();

                    dr["KEY"] = 0;
                    dr["VALUE"] = emptyValue;

                    dt.Rows.Add(dr);
                    list.Add(0);
                }

                foreach (KeyValuePair<int, string> item in dicData)
                {
                    if (!list.Contains(item.Key))
                    {
                        DataRow dr = dt.NewRow();

                        dr["KEY"] = item.Key;
                        dr["VALUE"] = item.Value;

                        dt.Rows.Add(dr);
                        list.Add(item.Key);
                    }
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }
    }
}
