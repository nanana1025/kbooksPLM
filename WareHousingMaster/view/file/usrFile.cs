using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.file
{
    public partial class usrFile : DevExpress.XtraEditors.XtraUserControl
    {
        //int _viewCategory = (int)common.Enum.EnumAttachedFile.WAREHOUSING;

        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }

        DataRowView _currentRow;

        int _category;
        long _id;
        string _dir;

        public usrFile()
        {
            InitializeComponent();

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("FILE_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("CATEGORY", typeof(int)));
            _dt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("EXTENSION", typeof(string)));
            _dt.Columns.Add(new DataColumn("TYPE_DIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("DIR", typeof(string)));
            _dt.Columns.Add(new DataColumn("FILE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("CREATE_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("DOWNLOAD", typeof(int)));

            _bs = new BindingSource();

            _bs.DataSource = _dt;
            gcFile.DataSource = _bs;

            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

        }

        public void setinitialize(int category, string dir, int type = 0)
        {
            _category = category;
            _dir = dir;

            if (type == 1)
            {
                gcFileType.Visible = false;
                gcCreateDt.Visible = false;
                gcCreateUserId.Visible = false;
            }
        }

        public void showEditable(bool flag)
        {
            for (int i = 0; i < lcgFile.CustomHeaderButtons.Count; i++)
                lcgFile.CustomHeaderButtons[i].Properties.Visible = flag;
        }

        public void getFile(long id)
        {
            _id = id;
            gvFile.BeginDataUpdate();
            _dt.Clear();

            if (_id > 0)
            {
                JObject jResult = new JObject();
                JObject jData = new JObject();

                string query = $"SELECT * FROM TN_ATTACHED_FILE WHERE CATEGORY = {_category} AND ID = {_id} AND USE_YN = 1";
                string url = "/common/queryDT.json";

                jData.Add("QUERY", query);//1:일반, 2: 생산대행


                if (DBConnect.getRequest(jData, ref jResult, url))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                        foreach (JObject row in jArray.Children<JObject>())
                        //foreach (DataRow row in dt.Rows)
                        {
                            DataRow dr = _dt.NewRow();

                            dr["FILE_ID"] = ConvertUtil.ToInt64(row["FILE_ID"]);
                            dr["CATEGORY"] = ConvertUtil.ToInt32(row["CATEGORY"]);
                            dr["ID"] = ConvertUtil.ToInt64(row["ID"]);

                            dr["EXTENSION"] = ConvertUtil.ToString(row["EXTENSION"]);
                            dr["TYPE_DIR"] = ConvertUtil.ToString(row["TYPE_DIR"]);
                            dr["DIR"] = ConvertUtil.ToString(row["DIR"]);
                            dr["FILE_NM"] = ConvertUtil.ToString(row["FILE_NM"]);
                            dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(row["CREATE_DT"]);
                            dr["CREATE_USER_ID"] = ConvertUtil.ToString(row["CREATE_USER_ID"]);
                            dr["DOWNLOAD"] = 1;
                            _dt.Rows.Add(dr);
                        }
                    }

                    gvFile.EndDataUpdate();
                }
                else
                {
                    gvFile.EndDataUpdate();
                    Dangol.Error("첨부파일 목록을 가져올 수 없습니다.");

                }
            }
            else
            {
                gvFile.EndDataUpdate();
            }
        }

        private void gvFile_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "DOWNLOAD" && _currentRow != null)
            {
                if (DBFile.downloadAttachedFile(_currentRow["FILE_ID"], _currentRow["FILE_NM"]))
                {
                    Dangol.Message("download complete");
                }

            }
        }

        private void gvFile_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvFile.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }

        private void lcgFile_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (OpenFileDialog fileDialog = new OpenFileDialog())
                {
                    fileDialog.InitialDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\";
                    fileDialog.Filter = "All Files|*.*";
                    fileDialog.Title = "Select File";
                    fileDialog.Multiselect = true;

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath;
                        JObject jobj;
                        gvFile.BeginDataUpdate();
                        for (int i = 0; i < fileDialog.FileNames.Length; i++)
                        {
                            filePath = fileDialog.FileNames[i];

                            jobj = DBFile.uploadAttachedFile(_category, _id, _dir, filePath);

                            if (jobj != null)
                            {
                                DataRow dr = _dt.NewRow();

                                dr["FILE_ID"] = ConvertUtil.ToInt64(jobj["FILE_ID"]);
                                dr["CATEGORY"] = ConvertUtil.ToInt32(jobj["CATEGORY"]);
                                dr["ID"] = ConvertUtil.ToInt64(jobj["ID"]);

                                dr["EXTENSION"] = ConvertUtil.ToString(jobj["EXTENSION"]);
                                dr["TYPE_DIR"] = ConvertUtil.ToString(jobj["TYPE_DIR"]);
                                dr["DIR"] = ConvertUtil.ToString(jobj["DIR"]);
                                dr["FILE_NM"] = ConvertUtil.ToString(jobj["FILE_NM"]);
                                dr["CREATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                                dr["CREATE_USER_ID"] = ProjectInfo._userId;
                                dr["DOWNLOAD"] = 1;
                                _dt.Rows.Add(dr);
                            }

                        }
                        gvFile.EndDataUpdate();
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_currentRow == null)
                {
                    Dangol.Warining("선택한 파일이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 파일을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jData = new JObject();

                    string query = $@"UPDATE TN_ATTACHED_FILE SET USE_YN = 0 WHERE FILE_ID = {_currentRow["FILE_ID"]}";
                    string url = "/common/execute.json";

                    jData.Add("QUERY", query);//1:일반, 2: 생산대행

                    if (DBConnect.getRequest(jData, ref jResult, url))
                    //if (DBFile.deleteAttachedFile(_currentRow["FILE_ID"]))
                    {
                        gvFile.BeginDataUpdate();
                        _currentRow.Delete();
                        gvFile.EndDataUpdate();
                    }

                }
            }
        }
    }
}
