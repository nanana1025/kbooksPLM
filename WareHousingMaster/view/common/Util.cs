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


        static private void insertComponentInitialize(string componentCd)
        {
            List<string> listFullColumn;
            DataTable dtComp = new DataTable();
            DataTable dtCompFromDb = new DataTable();
            Dictionary<string, string> dicTableColummComp = new Dictionary<string, string>();

            dtComp.Columns.Add(new DataColumn("ID", typeof(long)));
            dtComp.Columns.Add(new DataColumn("NAME", typeof(string)));
            dtComp.Columns.Add(new DataColumn("NO", typeof(int)));
            dtComp.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            dtComp.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            dtComp.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            dtComp.Columns.Add(new DataColumn("COMPONENT", typeof(string)));

            dtCompFromDb.Columns.Add(new DataColumn("ID", typeof(long)));

            listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
            foreach (string col in listFullColumn)
            {
                dtComp.Columns.Add(new DataColumn(col, typeof(string)));
                dtCompFromDb.Columns.Add(new DataColumn(col, typeof(string)));
                dicTableColummComp.Add($"_{col}", col);
            }

            ProjectInfo._dicDeviceInfo.Add(componentCd, dtComp);
            ProjectInfo._dicDeviceInfoFromDB.Add(componentCd, dtCompFromDb);
            ProjectInfo._dicTableColumnInfo.Add(componentCd, dicTableColummComp);
        }

        //static private bool SetDeviceTable()
        //{
            //    string key = "";
            //    int pk = 1;

            //    Dictionary<string, object> dicDeviceInfo = new Dictionary<string, object>();

            //    try
            //    {
            //        foreach (DeviceInfo part in ProjectInfo._listDeviceInfo)
            //        {
            //            string id = key = part.name;
            //            string value = part.value;

            //            if (!dicDeviceInfo.ContainsKey(id))
            //                dicDeviceInfo.Add(key, value);
            //        }

            //        foreach (KeyValuePair<string, Dictionary<string, string>> item in ProjectInfo._dicTableColumnInfo)
            //        {
            //            string id = key = item.Key;
            //            Dictionary<string, string> dicTableColumn = ProjectInfo._dicTableColumnInfo[id];

            //            DataTable dtDeviceInfo = ProjectInfo._dicDeviceInfo[id];
            //            List<string> listDeviceColumn = ProjectInfo._dicDeviceColumn[id];

            //            if (id.Equals("CPU") || id.Equals("MBD"))
            //            {
            //                DataRow drDeviceInfo = ProjectInfo._dtDeviceInfo.NewRow();
            //                drDeviceInfo["COMPONENT_CD"] = id;

            //                DataRow dr = dtDeviceInfo.NewRow();
            //                dr["ID"] = pk;
            //                dr["NAME"] = id;
            //                dr["NO"] = 1;
            //                dr["INVENTORY_ID"] = -1;
            //                dr["COMPONENT_ID"] = -1;
            //                dr["BARCODE"] = "";
            //                dr["COMPONENT"] = "";

            //                foreach (KeyValuePair<string, string> column in dicTableColumn)
            //                {
            //                    string colNm = column.Key;
            //                    string tableColNm = dicTableColumn[colNm];

            //                    if (dicDeviceInfo.ContainsKey(colNm))
            //                    {
            //                        string colData = ConvertUtil.ToString(dicDeviceInfo[colNm]);

            //                        if (string.IsNullOrEmpty(colData) || colData.ToUpper().Equals("TO BE FILLED BY O.E.M."))
            //                            dr[tableColNm] = "";
            //                        else
            //                            dr[tableColNm] = colData;
            //                    }
            //                    else
            //                        dr[tableColNm] = "";

            //                    if (listDeviceColumn.Contains(colNm))
            //                        drDeviceInfo[colNm] = dr[colNm];
            //                }

            //                drDeviceInfo["ID"] = dr["ID"];
            //                drDeviceInfo["NAME"] = dr["NAME"];
            //                drDeviceInfo["NO"] = dr["NO"];
            //                drDeviceInfo["NO1"] = pk;
            //                drDeviceInfo["INVENTORY_ID"] = dr["INVENTORY_ID"];
            //                drDeviceInfo["COMPONENT_ID"] = dr["COMPONENT_ID"];
            //                drDeviceInfo["BARCODE"] = dr["BARCODE"];
            //                drDeviceInfo["CHECK"] = false;
            //                drDeviceInfo["INVENTORY_YN"] = false;
            //                drDeviceInfo["PRODUCT_YN"] = false;
            //                drDeviceInfo["RELEASE_YN"] = false;
            //                drDeviceInfo["RELEASE_RESULT"] = "";

            //                for (int j = 0; j < listDeviceColumn.Count; j++)
            //                {
            //                    string colNm = $"DATA{j + 1}";
            //                    drDeviceInfo[colNm] = dr[listDeviceColumn[j]];
            //                    drDeviceInfo[listDeviceColumn[j]] = dr[listDeviceColumn[j]];
            //                }

            //                dtDeviceInfo.Rows.Add(dr);
            //                ProjectInfo._dtDeviceInfo.Rows.Add(drDeviceInfo);

            //                pk++;
            //            }
            //            else
            //            {
            //                int nofDevice = ProjectInfo._dicPartCnt[id];

            //                for (int i = 0; i < nofDevice; i++)
            //                {
            //                    DataRow drDeviceInfo = ProjectInfo._dtDeviceInfo.NewRow();
            //                    drDeviceInfo["COMPONENT_CD"] = id;

            //                    DataRow dr = dtDeviceInfo.NewRow();
            //                    dr["ID"] = pk;
            //                    dr["NAME"] = $"{id}_{i + 1}";
            //                    dr["NO"] = i + 1;
            //                    dr["INVENTORY_ID"] = -1;
            //                    dr["COMPONENT_ID"] = -1;
            //                    dr["BARCODE"] = "";
            //                    dr["COMPONENT"] = "";

            //                    foreach (KeyValuePair<string, string> column in dicTableColumn)
            //                    {
            //                        string colId = column.Key;
            //                        string colNm = $"{id}_{i}{column.Key}";
            //                        string tableColNm = dicTableColumn[colId];

            //                        if (dicDeviceInfo.ContainsKey(colNm))
            //                        {
            //                            string colData = ConvertUtil.ToString(dicDeviceInfo[colNm]);

            //                            if (string.IsNullOrEmpty(colData) || colData.ToUpper().Equals("TO BE FILLED BY O.E.M."))
            //                                dr[tableColNm] = "";
            //                            else
            //                                dr[tableColNm] = colData;
            //                        }
            //                        else
            //                            dr[tableColNm] = "";
            //                    }

            //                    drDeviceInfo["ID"] = dr["ID"];
            //                    drDeviceInfo["NAME"] = dr["NAME"];
            //                    drDeviceInfo["NO"] = dr["NO"];
            //                    drDeviceInfo["NO1"] = pk;
            //                    drDeviceInfo["INVENTORY_ID"] = dr["INVENTORY_ID"];
            //                    drDeviceInfo["COMPONENT_ID"] = dr["COMPONENT_ID"];
            //                    drDeviceInfo["BARCODE"] = dr["BARCODE"];
            //                    drDeviceInfo["CHECK"] = false;
            //                    drDeviceInfo["INVENTORY_YN"] = false;
            //                    drDeviceInfo["PRODUCT_YN"] = false;
            //                    drDeviceInfo["RELEASE_YN"] = false;
            //                    drDeviceInfo["RELEASE_RESULT"] = "";

            //                    for (int j = 0; j < listDeviceColumn.Count; j++)
            //                    {
            //                        string colNm = $"DATA{j + 1}";
            //                        drDeviceInfo[colNm] = dr[listDeviceColumn[j]];
            //                        drDeviceInfo[listDeviceColumn[j]] = dr[listDeviceColumn[j]];
            //                    }

            //                    dtDeviceInfo.Rows.Add(dr);
            //                    ProjectInfo._dtDeviceInfo.Rows.Add(drDeviceInfo);

            //                    pk++;
            //                }
            //            }

            //        }


            //        DataTable dtCpu = ProjectInfo._dicDeviceInfo["CPU"];
            //        DataRow row = dtCpu.Rows[0];
            //        string socketNm = ConvertUtil.ToString(row["SOCKET_NM"]).ToUpper();
            //        string modelNm = ConvertUtil.ToString(row["MODEL_NM"]).ToUpper();
            //        string specNm = ConvertUtil.ToString(row["SPEC_NM"]).ToUpper();
            //        string codeNm = ConvertUtil.ToString(row["CODE_NM"]).ToUpper();

            //        setCpuType(modelNm, specNm, codeNm);

            //        DataTable dtMbd = ProjectInfo._dicDeviceInfo["MBD"];
            //        row = dtMbd.Rows[0];
            //        string manufactureNm = ConvertUtil.ToString(row["MANUFACTURE_NM"]).ToUpper();

            //        if (string.IsNullOrEmpty(manufactureNm))
            //        {
            //            ProjectInfo._ntbCategory = "ETC";
            //            ProjectInfo._ntbManufactureType = 2;
            //        }
            //        else
            //        {
            //            bool isFind = false;
            //            foreach (string category in ProjectInfo._listNTBCatetory)
            //                if (manufactureNm.Contains(category))
            //                {
            //                    isFind = true;
            //                    ProjectInfo._ntbCategory = category;
            //                    ProjectInfo._ntbManufactureType = 1;
            //                    break;
            //                }
            //            if (!isFind)
            //            {
            //                ProjectInfo._ntbCategory = "ETC";
            //                ProjectInfo._ntbManufactureType = 2;
            //            }
            //        }

            //        key = "TYPE";

            //        if (dicDeviceInfo.ContainsKey(key))
            //        {
            //            string type = ConvertUtil.ToString(dicDeviceInfo[key]).ToUpper();

            //            if (!string.IsNullOrEmpty(type))
            //            {
            //                if (ProjectInfo._dicTypeNm.ContainsKey(type))
            //                    ProjectInfo._type = ProjectInfo._dicTypeNm[type];
            //                else
            //                    ProjectInfo._type = 0;
            //            }
            //            else
            //                ProjectInfo._type = 0;
            //        }
            //        else
            //            ProjectInfo._type = 0;


            //        if (ProjectInfo._type != 2)
            //            if (ProjectInfo._listCpuLaptopCheck.Contains(socketNm))
            //                ProjectInfo._type = 2;

            //        ProjectInfo._typeNm = ProjectInfo._arrTypeNm[ProjectInfo._type];

            //        if (ProjectInfo._type == 2)
            //        {
            //            JObject jResult = new JObject();
            //            if (DBConnect.getNtbListId(ref jResult))
            //                ProjectInfo._ntbListId = ConvertUtil.ToInt64(jResult["NTB_LIST_ID"]);
            //            else
            //                ProjectInfo._ntbListId = -1;
            //        }
            //        else
            //            ProjectInfo._ntbListId = -1;

            //        return true;
            //    }
            //    catch (Exception e)
            //    {
            //        DBConnect.SendPartInfo(key, new StringBuilder(e.Message.ToString()));

            //        if (key.Equals("TYPE"))
            //        {
            //            ProjectInfo._type = 0;
            //            ProjectInfo._typeNm = ProjectInfo._arrTypeNm[ProjectInfo._type];
            //            ProjectInfo._ntbListId = -1;
            //            MessageBox.Show("디바이스 TYPE을 가져오는 과정에서 오류가 발생했지만 검수는 정상적으로 진행 가능합니다.");
            //            return true;
            //        }

            //        MessageBox.Show(e.Message);
            //        return false;
            //    }
        //}

        static private void GetDeviceKeyInfo()
        {
            List<string> listFullColumn;
            DataTable dtDeviceInfoFromDB;
            DataRow[] row;

            long id;
            //for (int i = 0; i <7; i++ )
            foreach (string componentCd in ProjectInfo._checkComponetCd)
            {
                //string componentCd = ProjectInfo._componetCd[i];
                DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];

                JObject jResult = new JObject();

                if (componentCd.Equals("CPU") || componentCd.Equals("MBD"))
                {
                    listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
                    dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoFromDB[componentCd];
                    id = ConvertUtil.ToInt64(dtComponentInfo.Rows[0]["ID"]);
                    row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");
                    DataRow dr = dtDeviceInfoFromDB.NewRow();

                    if (DBConnect.SendCheckComponentRequest(componentCd, dtComponentInfo.Rows[0], ref jResult))
                    {
                        long inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);

                        foreach (string col in ProjectInfo._listKeyColumn)
                        {
                            dtComponentInfo.Rows[0][col] = jResult[col];
                            row[0][col] = jResult[col];
                        }

                        if (ConvertUtil.ToInt32(jResult["WAREHOUSE"]) > 0)
                            row[0]["WAREHOUSE"] = jResult["WAREHOUSE"];
                        else
                            row[0]["WAREHOUSE"] = ProjectInfo._locationId;

                        if (ConvertUtil.ToInt32(jResult["PALLET"]) > 0)
                            row[0]["PALLET"] = jResult["PALLET"];
                        else
                            row[0]["PALLET"] = null;

                        if (inventoryId > 0)
                        {
                            if (ProjectInfo._listInventoryId.Contains(inventoryId))
                            {
                                row[0]["INVENTORY_ID"] = -1;
                                row[0]["BARCODE"] = "";
                                row[0]["INVENTORY_YN"] = false;
                            }
                            else
                            {
                                row[0]["INVENTORY_YN"] = true;
                                ProjectInfo._listInventoryId.Add(inventoryId);
                            }
                        }
                        else
                        {
                            row[0]["INVENTORY_ID"] = -1;
                            row[0]["BARCODE"] = "";
                            row[0]["INVENTORY_YN"] = false;
                        }



                        JObject jData = (JObject)jResult["DATA"];

                        dr["ID"] = id;

                        foreach (string col in listFullColumn)
                        {
                            if (jData[col] == null)
                                dr[col] = "";
                            else
                                dr[col] = jData[col].ToString();
                        }

                        dtDeviceInfoFromDB.Rows.Add(dr);
                    }
                    else
                    {
                        foreach (string col in ProjectInfo._listKeyColumn)
                        {
                            dtComponentInfo.Rows[0][col] = jResult[col];
                            row[0][col] = jResult[col];
                        }

                        row[0]["INVENTORY_YN"] = false;

                        dr["ID"] = id;

                        foreach (string col in listFullColumn)
                            dr[col] = "";

                        dtDeviceInfoFromDB.Rows.Add(dr);
                    }

                    DataTable dtCompare = new DataTable();
                    dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                    foreach (string col in listFullColumn)
                    {
                        DataRow drCompare = dtCompare.NewRow();
                        drCompare["SPEC_NM"] = col;
                        drCompare["DEVICE_INFO"] = dtComponentInfo.Rows[0][col];
                        drCompare["MATCHING_INFO"] = dr[col];
                        dtCompare.Rows.Add(drCompare);
                    }


                    ProjectInfo._dicDeviceInfoDetail.Add(id, dtCompare);
                }
                else
                {
                    int nofDevice = ProjectInfo._dicPartCnt[componentCd];
                    listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
                    dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoFromDB[componentCd];

                    for (int j = 0; j < nofDevice; j++)
                    {
                        id = ConvertUtil.ToInt64(dtComponentInfo.Rows[j]["ID"]);
                        row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");
                        DataRow dr = dtDeviceInfoFromDB.NewRow();

                        if (DBConnect.SendCheckComponentRequest(componentCd, dtComponentInfo.Rows[j], ref jResult))
                        {
                            long inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);

                            foreach (string col in ProjectInfo._listKeyColumn)
                            {
                                dtComponentInfo.Rows[0][col] = jResult[col];
                                row[0][col] = jResult[col];
                            }

                            if (ConvertUtil.ToInt32(jResult["WAREHOUSE"]) > 0)
                                row[0]["WAREHOUSE"] = jResult["WAREHOUSE"];
                            else
                                row[0]["WAREHOUSE"] = ProjectInfo._locationId;

                            if (ConvertUtil.ToInt32(jResult["PALLET"]) > 0)
                                row[0]["PALLET"] = jResult["PALLET"];
                            else
                                row[0]["PALLET"] = null;

                            if (inventoryId > 0)
                            {
                                if (ProjectInfo._listInventoryId.Contains(inventoryId))
                                {
                                    row[0]["INVENTORY_ID"] = -1;
                                    row[0]["BARCODE"] = "";
                                    row[0]["INVENTORY_YN"] = false;
                                }
                                else
                                {
                                    row[0]["INVENTORY_YN"] = true;
                                    ProjectInfo._listInventoryId.Add(inventoryId);
                                }
                            }
                            else
                            {
                                row[0]["INVENTORY_ID"] = -1;
                                row[0]["BARCODE"] = "";
                                row[0]["INVENTORY_YN"] = false;
                            }

                            JObject jData = (JObject)jResult["DATA"];

                            dr["ID"] = id;

                            foreach (string col in listFullColumn)
                            {
                                if (jData[col] == null)
                                    dr[col] = "";
                                else
                                    dr[col] = jData[col].ToString();
                            }

                            dtDeviceInfoFromDB.Rows.Add(dr);
                        }
                        else
                        {
                            foreach (string col in ProjectInfo._listKeyColumn)
                            {
                                dtComponentInfo.Rows[0][col] = jResult[col];
                                row[0][col] = jResult[col];
                            }

                            row[0]["INVENTORY_YN"] = false;

                            dr["ID"] = id;

                            foreach (string col in listFullColumn)
                                dr[col] = "";

                            dtDeviceInfoFromDB.Rows.Add(dr);
                        }

                        DataTable dtCompare = new DataTable();
                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                        foreach (string col in listFullColumn)
                        {
                            DataRow drCompare = dtCompare.NewRow();
                            drCompare["SPEC_NM"] = col;
                            drCompare["DEVICE_INFO"] = dtComponentInfo.Rows[j][col];
                            drCompare["MATCHING_INFO"] = dr[col];
                            dtCompare.Rows.Add(drCompare);
                        }

                        ProjectInfo._dicDeviceInfoDetail.Add(id, dtCompare);
                    }
                }
            }

            DataTable dtMbd = ProjectInfo._dicDeviceInfo["MBD"];
            DataRow rowMBD = dtMbd.Rows[0];

            long MBDInventoryId = ConvertUtil.ToInt64(rowMBD["INVENTORY_ID"]);
            if (ProjectInfo._type == 2 && MBDInventoryId > 0)
            {
                JObject jResult = new JObject();

                if (DBConnect.getProductInfo(MBDInventoryId, ref jResult))
                {
                    ProjectInfo._ntbListId = ConvertUtil.ToInt64(jResult["NTB_LIST_ID"]);
                    ProjectInfo._ntbManufactureType = ConvertUtil.ToInt32(jResult["MANUFACTURE_TYPE"]);
                }
            }
        }

        static private void GetDeviceKeyInfoWOMatchingInfo()
        {
            List<string> listFullColumn;
            DataTable dtDeviceInfoFromDB;
            DataRow[] row;
            long id;
            for (int i = 0; i < 6; i++)
            {
                string componentCd = ProjectInfo._componetCd[i];
                DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];

                JObject jResult = new JObject();
                jResult.Add("INVENTORY_ID", -1);
                jResult.Add("COMPONENT_ID", -1);
                jResult.Add("BARCODE", "");
                jResult.Add("COMPONENT", "");

                if (componentCd.Equals("CPU") || componentCd.Equals("MBD"))
                {
                    listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
                    dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoFromDB[componentCd];
                    id = ConvertUtil.ToInt64(dtComponentInfo.Rows[0]["ID"]);
                    row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");
                    DataRow dr = dtDeviceInfoFromDB.NewRow();


                    foreach (string col in ProjectInfo._listKeyColumn)
                    {
                        dtComponentInfo.Rows[0][col] = jResult[col];
                        row[0][col] = jResult[col];
                    }

                    row[0]["INVENTORY_YN"] = false;

                    dr["ID"] = id;

                    foreach (string col in listFullColumn)
                        dr[col] = "";

                    dtDeviceInfoFromDB.Rows.Add(dr);


                    DataTable dtCompare = new DataTable();
                    dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                    dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                    foreach (string col in listFullColumn)
                    {
                        DataRow drCompare = dtCompare.NewRow();
                        drCompare["SPEC_NM"] = col;
                        drCompare["DEVICE_INFO"] = dtComponentInfo.Rows[0][col];
                        drCompare["MATCHING_INFO"] = dr[col];
                        dtCompare.Rows.Add(drCompare);
                    }

                    ProjectInfo._dicDeviceInfoDetail.Add(id, dtCompare);
                }
                else
                {
                    int nofDevice = ProjectInfo._dicPartCnt[componentCd];
                    listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
                    dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoFromDB[componentCd];

                    for (int j = 0; j < nofDevice; j++)
                    {
                        id = ConvertUtil.ToInt64(dtComponentInfo.Rows[j]["ID"]);
                        row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");
                        DataRow dr = dtDeviceInfoFromDB.NewRow();

                        foreach (string col in ProjectInfo._listKeyColumn)
                        {
                            dtComponentInfo.Rows[0][col] = jResult[col];
                            row[0][col] = jResult[col];
                        }

                        row[0]["INVENTORY_YN"] = false;

                        dr["ID"] = id;

                        foreach (string col in listFullColumn)
                            dr[col] = "";

                        dtDeviceInfoFromDB.Rows.Add(dr);

                        DataTable dtCompare = new DataTable();
                        dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                        dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                        foreach (string col in listFullColumn)
                        {
                            DataRow drCompare = dtCompare.NewRow();
                            drCompare["SPEC_NM"] = col;
                            drCompare["DEVICE_INFO"] = dtComponentInfo.Rows[j][col];
                            drCompare["MATCHING_INFO"] = dr[col];
                            dtCompare.Rows.Add(drCompare);
                        }

                        ProjectInfo._dicDeviceInfoDetail.Add(id, dtCompare);
                    }
                }
            }
        }

        static public void putData(JObject jPartInfo, string componentCd, bool isOne = false, bool productYn = false)
        {
            if (!ProjectInfo._dicDeviceInfo.ContainsKey(componentCd))
                insertComponentInitialize(componentCd);

            List<long> listInventoryId = new List<long>();
            List<string> listBarcode = new List<string>();

            if (isOne)
            {
                listInventoryId.Add(ConvertUtil.ToInt64(jPartInfo["INVENTORY_ID"]));
                listBarcode.Add(ConvertUtil.ToString(jPartInfo["BARCODE"]));
            }
            else
            {
                JArray jInventoryId = JArray.Parse(jPartInfo["INVENTORY_ID"].ToString());
                JArray jBarcode = JArray.Parse(jPartInfo["BARCODE"].ToString());

                foreach (var item in jInventoryId.Children())
                    listInventoryId.Add(ConvertUtil.ToInt64(item));

                foreach (var item in jBarcode.Children())
                    listBarcode.Add(ConvertUtil.ToString(item));
            }



            Dictionary<string, string> dicTableColumn = ProjectInfo._dicTableColumnInfo[componentCd];
            DataTable dtDeviceInfo = ProjectInfo._dicDeviceInfo[componentCd];
            List<string> listDeviceColumn = ProjectInfo._dicDeviceColumn[componentCd];
            List<string> listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
            DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoFromDB[componentCd];

            long id = ProjectInfo._dtDeviceInfo.Rows.Count + 1;
            long no = dtDeviceInfo.Rows.Count + 1;


            long inventoryId;
            string barcode;


            for (int i = 0; i < listInventoryId.Count; i++, id++, no++)
            {
                inventoryId = listInventoryId[i];
                barcode = listBarcode[i];

                DataRow drDeviceInfo = ProjectInfo._dtDeviceInfo.NewRow();
                drDeviceInfo["COMPONENT_CD"] = componentCd;

                DataRow dr = dtDeviceInfo.NewRow();
                dr["ID"] = id;
                dr["NAME"] = $"{componentCd}_{no}";
                dr["NO"] = no;
                dr["INVENTORY_ID"] = inventoryId;
                dr["COMPONENT_ID"] = ConvertUtil.ToInt64(jPartInfo["COMPONENT_ID"]);
                dr["BARCODE"] = barcode;
                dr["COMPONENT"] = ConvertUtil.ToString(jPartInfo["COMPONENT"]);

                foreach (KeyValuePair<string, string> column in dicTableColumn)
                {
                    string colNm = column.Key;
                    string tableColNm = dicTableColumn[colNm];

                    if (jPartInfo.ContainsKey(tableColNm))
                    {
                        string colData = ConvertUtil.ToString(jPartInfo[tableColNm]);

                        if (string.IsNullOrEmpty(colData))
                            dr[tableColNm] = "";
                        else
                            dr[tableColNm] = colData;
                    }
                    else
                        dr[tableColNm] = "";

                    if (listDeviceColumn.Contains(tableColNm))
                        drDeviceInfo[tableColNm] = dr[tableColNm];
                }

                drDeviceInfo["ID"] = dr["ID"];
                drDeviceInfo["NAME"] = dr["NAME"];
                drDeviceInfo["NO"] = dr["NO"];
                drDeviceInfo["NO1"] = id;
                drDeviceInfo["INVENTORY_ID"] = inventoryId;
                drDeviceInfo["COMPONENT_ID"] = dr["COMPONENT_ID"];
                drDeviceInfo["BARCODE"] = barcode;
                drDeviceInfo["COMPONENT"] = dr["COMPONENT"];
                drDeviceInfo["WAREHOUSE"] = ConvertUtil.ToInt64(jPartInfo["LOCATION"]);
                drDeviceInfo["PALLET"] = ConvertUtil.ToInt64(jPartInfo["PALLET"]);
                drDeviceInfo["CHECK"] = false;
                drDeviceInfo["INVENTORY_YN"] = true;
                drDeviceInfo["PRODUCT_YN"] = productYn;
                drDeviceInfo["RELEASE_YN"] = false;
                drDeviceInfo["RELEASE_RESULT"] = "";

                for (int j = 0; j < listDeviceColumn.Count; j++)
                {
                    string colNm = $"DATA{j + 1}";
                    drDeviceInfo[colNm] = dr[listDeviceColumn[j]];
                    drDeviceInfo[listDeviceColumn[j]] = dr[listDeviceColumn[j]];
                }

                dtDeviceInfo.Rows.Add(dr);
                ProjectInfo._dtDeviceInfo.Rows.Add(drDeviceInfo);


                DataRow row = dtDeviceInfoFromDB.NewRow();


                foreach (string col in listFullColumn)
                {
                    //if (jPartInfo.ContainsKey(col))
                    //    row[col] = jPartInfo[col];
                    //else
                    //    row[col] = "";
                    row[col] = dr[col];
                }

                dtDeviceInfoFromDB.Rows.Add(row);
                DataTable dtCompare = new DataTable();
                dtCompare.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
                dtCompare.Columns.Add(new DataColumn("DEVICE_INFO", typeof(string)));
                dtCompare.Columns.Add(new DataColumn("MATCHING_INFO", typeof(string)));

                foreach (string col in listFullColumn)
                {
                    DataRow drCompare = dtCompare.NewRow();
                    drCompare["SPEC_NM"] = col;
                    drCompare["DEVICE_INFO"] = dtDeviceInfo.Rows[0][col];
                    drCompare["MATCHING_INFO"] = row[col];
                    dtCompare.Rows.Add(drCompare);
                }

                ProjectInfo._dicDeviceInfoDetail.Add(id, dtCompare);
            }
        }




        static private void setCpuType(string modelNm, string specNm, string codeNm)
        {
            string[] arrCode = { "I3", "I5", "I7", "I9" };

            int i;
            for (i = 0; i < arrCode.Length; i++)
            {
                if (modelNm.Contains(arrCode[i]))
                    break;
            }

            if (i < arrCode.Length)
            {
                ProjectInfo._cpuCategory = arrCode[i];
                if (modelNm.Length < 15)
                {
                    if (codeNm.Equals("ARRANDALE"))
                    {
                        //getGenderationException1(specNm);
                        ProjectInfo._cpuGeneration = "1";
                    }
                    else if (codeNm.Equals("CLARKSFIELD"))
                    {
                        ProjectInfo._cpuGeneration = "1";
                    }
                    else
                    {
                        int indexDevStart = specNm.IndexOf(arrCode[i]);
                        ProjectInfo._cpuGeneration = specNm.Substring(indexDevStart + 3, 1);
                    }
                }
                else
                {
                    if (codeNm.Equals("ARRANDALE"))
                    {
                        //getGenderationException1(specNm);
                        ProjectInfo._cpuGeneration = "1";
                    }
                    else if (codeNm.Equals("CLARKSFIELD"))
                    {
                        ProjectInfo._cpuGeneration = "1";
                    }
                    else
                    {
                        int indexDevStart = modelNm.IndexOf(arrCode[i]);
                        ProjectInfo._cpuGeneration = modelNm.Substring(indexDevStart + 3, 1);
                    }
                }
            }
            else
            {
                if (modelNm.Contains("CORE 2"))
                {
                    ProjectInfo._cpuCategory = "CORE2DUO";
                    getGenderationException1(specNm);
                }
                else if (modelNm.Contains("CELERON"))
                {
                    ProjectInfo._cpuCategory = "CELERON";
                    getGenderationException1(specNm);
                }
                else if (modelNm.Contains("ATOM"))
                {
                    ProjectInfo._cpuCategory = "ATOM";
                    getGenderationException1(specNm);
                }
                else if (modelNm.Contains("Core M"))
                {
                    ProjectInfo._cpuCategory = "COREM";
                    //getGenderationException1(specNm);
                }
                else if (modelNm.Contains("PENTIUM"))
                {
                    ProjectInfo._cpuCategory = "PENTIUM";
                    char[] delimiterChars = { ' ' };
                    string[] spModelNm = modelNm.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    if (spModelNm.Length < 3)
                        getGenderationException1(specNm);
                    else
                    {
                        ProjectInfo._cpuGeneration = spModelNm[2].Substring(0, 1);

                        int gen = 0;
                        bool result = int.TryParse(ProjectInfo._cpuGeneration, out gen); //i now = 108  

                        if (!result)
                            ProjectInfo._cpuGeneration = spModelNm[2].Substring(1, 1);
                    }
                }
            }

            foreach (string code in ProjectInfo._listCpuCodeNm)
            {
                if (codeNm.Contains(code))
                {
                    ProjectInfo._codeNm = code;
                    break;
                }
            }

        }

        static private void getGenderationException1(string specNm)
        {
            char[] delimiterChars = { ' ' };
            string[] spModel = specNm.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < spModel.Length; j++)
                if (spModel[j].Equals("@"))
                {
                    ProjectInfo._cpuGeneration = spModel[j - 1].Substring(0, 1);

                    int gen = 0;
                    bool result = int.TryParse(ProjectInfo._cpuGeneration, out gen); //i now = 108  

                    if (!result)
                        ProjectInfo._cpuGeneration = spModel[j - 1].Substring(1, 1);

                    return;
                }
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

        static public void checkProductState(long inventoryId, string barcode)
        {
            JObject jResult = new JObject();

            if (ProjectInfo._dicProductList == null)
                ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
            else
                ProjectInfo._dicProductList.Clear();

            if (ProjectInfo._dicProductInventoryId == null)
                ProjectInfo._dicProductInventoryId = new Dictionary<string, List<long>>();
            else
                ProjectInfo._dicProductInventoryId.Clear();



            if (DBConnect.getProductList(inventoryId, ref jResult))
            {
                foreach (var x in jResult)
                {
                    string id = x.Key;

                    if (!id.Equals("SUCCESS"))
                    {
                        JObject jData = (JObject)jResult[id];
                        Dictionary<string, string> dicData = new Dictionary<string, string>();
                        foreach (var y in jData)
                        {

                            string key = y.Key;
                            string value = y.Value.ToObject<string>();

                            dicData.Add(key, value);
                        }

                        ProjectInfo._dicProductList.Add(ConvertUtil.ToInt64(id), dicData);
                        string componentCd = dicData["COMPONENT_CD"];

                        if (ProjectInfo._dicProductInventoryId.ContainsKey(componentCd))
                        {
                            List<long> listInventoryId = ProjectInfo._dicProductInventoryId[componentCd];
                            listInventoryId.Add(ConvertUtil.ToInt64(id));
                        }
                        else
                        {
                            List<long> listInventoryId = new List<long>();
                            listInventoryId.Add(ConvertUtil.ToInt64(id));
                            ProjectInfo._dicProductInventoryId.Add(componentCd, listInventoryId);
                        }
                    }
                }

                Dictionary<string, string> dicMbdData = new Dictionary<string, string>();

                dicMbdData.Add("INVENTORY_ID", ConvertUtil.ToString(inventoryId));
                dicMbdData.Add("BARCODE", barcode);
                dicMbdData.Add("COMPONENT_CD", "MBD");

                if (!ProjectInfo._dicProductList.ContainsKey(inventoryId))
                    ProjectInfo._dicProductList.Add(inventoryId, dicMbdData);

                if (ProjectInfo._type == 2)
                {
                    if (DBConnect.getProductNtbSpecinfo(inventoryId, ref jResult))
                    {
                        ProjectInfo._ntbListId = ConvertUtil.ToInt64(jResult["NTB_LIST_ID"]);
                        ProjectInfo._ntbManufactureType = ConvertUtil.ToInt32(jResult["MANUFACTURE_TYPE"]);
                    }
                }
            }

            foreach (DataRow dr in ProjectInfo._dtDeviceInfo.Rows)
            {
                long id = ConvertUtil.ToInt64(dr["INVENTORY_ID"]);
                if (ProjectInfo._dicProductList.ContainsKey(id))
                    dr["PRODUCT_YN"] = true;
                else
                    dr["PRODUCT_YN"] = false;
            }

        }

        static public void getEtcComponent()
        {
            JObject jResult = new JObject();
            DataRow[] rows;

            foreach (string componentCd in ProjectInfo._uncheckcomponetCd)
            {
                if (ProjectInfo._dicProductInventoryId.ContainsKey(componentCd))
                {
                    List<long> listInventoryId = ProjectInfo._dicProductInventoryId[componentCd];

                    foreach (long inventoryId in listInventoryId)
                    {
                        rows = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID = {inventoryId}");

                        if (rows.Length == 0)
                        {
                            jResult.RemoveAll();

                            if (DBConnect.getInventoryByInventoryId(inventoryId, componentCd, ref jResult))
                            {
                                Util.putData(jResult, componentCd, true, true);
                            }
                        }
                    }
                }
            }
        }

        static public void checkProductRemainPart()
        {
            List<string> listFullColumn;
            DataTable dtDeviceInfoFromDB;
            DataRow[] rows;
            DataRow[] inventoryRows;
            long id;

            Dictionary<string, List<long>> dicComponentInventory = new Dictionary<string, List<long>>();

            rows = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID = -1");

            foreach (DataRow row in rows)
            {
                id = ConvertUtil.ToInt64(row["ID"]);
                string componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);

                if (ProjectInfo._dicProductInventoryId.ContainsKey(componentCd))
                {
                    List<long> listInventoryId = ProjectInfo._dicProductInventoryId[componentCd];

                    DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];

                    inventoryRows = dtComponentInfo.Select($"ID = {id}");
                    //_dicDeviceInfoDetail
                    listFullColumn = ProjectInfo._dicDeviceFullColumn[componentCd];
                    //dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoFromDB[componentCd];
                    dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[id];


                    Dictionary<string, string> dicData;
                    bool isSame = true;
                    List<long> listUseInventory = new List<long>();
                    if (dicComponentInventory.ContainsKey(componentCd))
                        listUseInventory = dicComponentInventory[componentCd];

                    foreach (long inventoryId in listInventoryId)
                    {
                        isSame = true;

                        if (listUseInventory.Contains(inventoryId))
                            continue;

                        if (ProjectInfo._listInventoryId.Contains(inventoryId))
                            continue;

                        dicData = ProjectInfo._dicProductList[inventoryId];

                        foreach (string col in listFullColumn)
                        {
                            if (!ConvertUtil.ToString(inventoryRows[0][col]).Equals(ConvertUtil.ToString(dicData[col])))
                            {
                                isSame = false;
                                break;
                            }
                        }

                        if (isSame)
                        {
                            foreach (string col in ProjectInfo._listKeyColumn)
                            {
                                inventoryRows[0][col] = dicData[col];
                                row[col] = dicData[col];
                            }

                            if (ConvertUtil.ToInt32(dicData["WAREHOUSE"]) > 0)
                                row["WAREHOUSE"] = dicData["WAREHOUSE"];

                            if (ConvertUtil.ToInt32(dicData["PALLET"]) > 0)
                                row["PALLET"] = dicData["PALLET"];

                            row["INVENTORY_YN"] = true;
                            row["PRODUCT_YN"] = true;

                            inventoryRows[0].BeginEdit();
                            foreach (string col in listFullColumn)
                            {

                                if (dicData[col] == null)
                                    inventoryRows[0][col] = "";
                                else
                                    inventoryRows[0][col] = dicData[col].ToString();
                            }
                            inventoryRows[0].EndEdit();

                            foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                            {
                                dr.BeginEdit();
                                dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                                dr.EndEdit();
                            }

                            if (dicComponentInventory.ContainsKey(componentCd))
                            {
                                List<long> listInventory = dicComponentInventory[componentCd];
                                listInventory.Add(ConvertUtil.ToInt64(dicData["INVENTORY_ID"]));
                            }
                            else
                            {
                                List<long> listInventory = new List<long>();
                                listInventory.Add(ConvertUtil.ToInt64(dicData["INVENTORY_ID"]));
                                dicComponentInventory.Add(componentCd, listInventory);
                            }

                            break;
                        }
                    }
                }
            }
        }
    }
}
