using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.board
{
    public partial class usrView : DevExpress.XtraEditors.XtraForm
    {
        long _companyId = -1;
        TreeListNode _focusedNode;
        DataRowView _currentRow;

        DataRowView _currentCategoryRow;
        DataRowView _currentViewRow;

        DataTable _dtUser;
        BindingSource _bs;

        DataTable _dtCategory;
        BindingSource _bsCategory;

        DataTable _dtView;
        BindingSource _bsView;

        List<string> listMasterUserId;

        string _userId;

        public usrView()
        {
            InitializeComponent();


            _dtUser = new DataTable();
            _dtUser.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtUser.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtUser.Columns.Add(new DataColumn("USER_NM", typeof(string)));
            _dtUser.Columns.Add(new DataColumn("USER_TYPE", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("STATE_CD", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            //_dtUser.Columns.Add(new DataColumn("DEPT_CD", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("MOBILE", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("TEAM_CD", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("POSITION", typeof(string)));
            _dtUser.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtCategory = new DataTable();
            _dtCategory.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtCategory.Columns.Add(new DataColumn("VIEW_ID", typeof(int)));
            _dtCategory.Columns.Add(new DataColumn("VIEW_CATEGORY", typeof(int)));
            _dtCategory.Columns.Add(new DataColumn("VIEW_NAME", typeof(string)));
            _dtCategory.Columns.Add(new DataColumn("VIEW_YN", typeof(int)));
            _dtCategory.Columns.Add(new DataColumn("STATE", typeof(int)));

            _dtView = new DataTable();
            _dtView.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtView.Columns.Add(new DataColumn("VIEW_ID", typeof(int)));
            _dtView.Columns.Add(new DataColumn("VIEW_NAME", typeof(string)));
            _dtView.Columns.Add(new DataColumn("VIEW_YN", typeof(int)));
            _dtView.Columns.Add(new DataColumn("STATE", typeof(int)));

            _bs = new BindingSource();
            _bsCategory = new BindingSource();
            _bsView = new BindingSource();

            //listMasterUserId = new List<string> ( new string[] {"MASTER","SUPER USER","MANAGER","USER","EXTERNAL" } );

        }



        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();
            getUserInfo();
        }
        private void setInfoBox()
        {
            DataTable dtUserType = new DataTable();
            dtUserType.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtUserType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRow(dtUserType, "M", "마스터관리자");
            Util.insertRow(dtUserType, "S", "관리자");
            Util.insertRow(dtUserType, "U", "일반유저");

            Util.LookupEditHelper(rileUserType, dtUserType, "KEY", "VALUE");
            //Util.LookupEditHelper(leUserType, dtUserType, "KEY", "VALUE");

            //DataTable dtUserType = Util.getCodeList("CD0201", "KEY", "VALUE");
            //Util.insertRowonTop(dtUserType, "-1", "관리자 설정 유저");

            //Util.LookupEditHelper(rileUserType, dtUserType, "KEY", "VALUE");

            ////DataTable dtUserState = Util.getCodeList("CD0202", "KEY", "VALUE");
            ////Util.LookupEditHelper(rileUserState, dtUserState, "KEY", "VALUE");

            //DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            //Util.insertRowonTop(dtCompany, "-1", "-");
            //Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            //DataTable dtDeptCd = Util.getCodeList("CD0502", "KEY", "VALUE");
            //Util.insertRowonTop(dtDeptCd, "-1", "-");
            //Util.LookupEditHelper(rileDeptCd, dtDeptCd, "KEY", "VALUE");

            //DataTable dtTeamCd = Util.getCodeList("CD0503", "KEY", "VALUE");
            //Util.insertRowonTop(dtTeamCd, "-1", "-");
            //Util.LookupEditHelper(rileTeamCd, dtTeamCd, "KEY", "VALUE");

            //DataTable dtPosition = Util.getCodeList("CD0505", "KEY", "VALUE");
            //Util.insertRowonTop(dtPosition, "-1", "-");
            //Util.LookupEditHelper(rilePosition, dtPosition, "KEY", "VALUE");


        }

        private void setIInitData()
        {
            if (ProjectInfo._userId.Equals("shlee"))
                lcViewUpdate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        private void setGridControl()
        {
            _bs.DataSource = _dtUser;
            gcUser.DataSource = null;
            gcUser.DataSource = _bs;

            _bsCategory.DataSource = _dtCategory;
            gcCategory.DataSource = null;
            gcCategory.DataSource = _bsCategory;

            _bsView.DataSource = _dtView;
            gcView.DataSource = null;
            gcView.DataSource = _bsView;

        }

        private void gvUser_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvUser.RowCount > 0);

            _dtCategory.Clear();
            _dtView.Clear();

            if (isValidRow)
            {
                _currentRow = gvUser.GetRow(e.FocusedRowHandle) as DataRowView;
                teUserNm.Text = ConvertUtil.ToString(_currentRow["USER_NM"]);
                _userId = ConvertUtil.ToString(_currentRow["USER_ID"]);

                /*
                if (listMasterUserId.Contains(_userId))
                {
                    lcgCategory.CustomHeaderButtons[1].Properties.Visible = ProjectInfo._userType.Equals("M");
                    lcgView.CustomHeaderButtons[1].Properties.Visible = ProjectInfo._userType.Equals("M");
                }
                else
                {
                    lcgCategory.CustomHeaderButtons[1].Properties.Visible = true;
                    lcgView.CustomHeaderButtons[1].Properties.Visible = true;
                }
                */
                getCategoryType();

            }
            else
            {
                _currentRow = null;
                teUserNm.Text = "";
                _userId = "";

                lcgCategory.CustomHeaderButtons[1].Properties.Visible = false;
                lcgView.CustomHeaderButtons[1].Properties.Visible = false;
            }
        }

        private void gvCategory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {

            bool isValidRow = (e.FocusedRowHandle >= 0 && gvCategory.RowCount > 0);

            _dtView.Clear();
            if (isValidRow)
            {
                _currentCategoryRow = gvCategory.GetRow(e.FocusedRowHandle) as DataRowView;
                getViewType();

            }
            else
            {
                _currentCategoryRow = null;

            }
        }

        private void gvView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvView.RowCount > 0);

            if (isValidRow)
            {
                _currentViewRow = gvView.GetRow(e.FocusedRowHandle) as DataRowView;

            }
            else
            {
                _currentViewRow = null;

            }
        }

        private void getUserInfo()
        {
            JObject jResult = new JObject();

            gvUser.BeginDataUpdate();
            _dtUser.Clear();


            int index = 1;
            //foreach (string user in listMasterUserId)
            //{
            //    DataRow dr = _dtUser.NewRow();

            //    dr["NO"] = index++;
            //    dr["USER_ID"] = user;
            //    dr["USER_NM"] = user;
            //    dr["USER_TYPE_CD"] = "-1";
            //    dr["STATE_CD"] = "";
            //    dr["COMPANY_ID"] = -1;
            //    dr["MOBILE"] = "";
            //    dr["DEPT_CD"] = "";
            //    dr["TEAM_CD"] = "";
            //    dr["POSITION"] = "";
            //    dr["CHECK"] = false;
            //    _dtUser.Rows.Add(dr);  
            //}

            DataTable dtUser = Util.getTable("TN_USER_MST", "USER_ID,USER_NM,USER_TYPE", "USER_ID IS NOT NULL", "USER_NM ASC");
            
               
            //JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    
            //string userState;
            foreach (DataRow obj in dtUser.Rows)
            {
                DataRow dr = _dtUser.NewRow();

                dr["NO"] = index++;
                dr["USER_ID"] = ConvertUtil.ToString(obj["USER_ID"]);
                //dr["PASSWD"] = ConvertUtil.ToString(obj["PASSWD"]);
                dr["USER_NM"] = ConvertUtil.ToString(obj["USER_NM"]);
                dr["USER_TYPE"] = ConvertUtil.ToString(obj["USER_TYPE"]);
                //dr["COMPANY_TYPE"] = ConvertUtil.ToString(obj["COMPANY_TYPE"]);
                //dr["STATE_CD"] = obj["STATE_CD"];
                //dr["COMPANY_ID"] = ConvertUtil.ToInt64(obj["COMPANY_ID"]);
                //dr["MOBILE"] = ConvertUtil.ToString(obj["MOBILE"]);
                //dr["DEPT_CD"] = ConvertUtil.ToString(obj["DEPT_CD"]);
                //dr["TEAM_CD"] = ConvertUtil.ToString(obj["TEAM_CD"]);
                //dr["POSITION"] = ConvertUtil.ToString(obj["POSITION"]);
                _dtUser.Rows.Add(dr);
            }


            gvUser.EndDataUpdate();
        }

        public void getCategoryType()
        {

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            //DataTable dtUser = Util.getTable("TN_PLM_CATEGORY_VIEW", "VIEW_ID,VIEW_CATEGORY,VIEW_NAME,VIEW_YN", "USER_ID IS NOT NULL", "USER_NM ASC");

            string url = "/board/getViewType2.json";
            jobj.Add("SELECT_USER_ID", ConvertUtil.ToString(_currentRow["USER_ID"]));
            jobj.Add("TYPE", 1);

            _dtCategory.Clear();
 
            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (ConvertUtil.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtCategory.NewRow();
                        dr["NO"] = index++;
                        dr["VIEW_ID"] = ConvertUtil.ToInt32(obj["VIEW_ID"]);
                        dr["VIEW_CATEGORY"] = ConvertUtil.ToInt32(obj["VIEW_CATEGORY"]);
                        dr["VIEW_NAME"] = ConvertUtil.ToString(obj["VIEW_NAME"]);
                        dr["VIEW_YN"] = ConvertUtil.ToInt32(obj["VIEW_YN"]);
                        dr["STATE"] = 0;

                        _dtCategory.Rows.Add(dr);
                    }
                }
            }
        }

        public void getViewType()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            string url = "/board/getViewType2.json";
            jobj.Add("SELECT_USER_ID", ConvertUtil.ToString(_currentRow["USER_ID"]));
            jobj.Add("VIEW_CATEGORY", ConvertUtil.ToInt32(_currentCategoryRow["VIEW_CATEGORY"]));
            jobj.Add("TYPE", 0);

            _dtView.Clear();

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (ConvertUtil.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtView.NewRow();
                        dr["NO"] = index++;
                        dr["VIEW_ID"] = ConvertUtil.ToInt32(obj["VIEW_ID"]);
                        dr["VIEW_NAME"] = ConvertUtil.ToString(obj["VIEW_NAME"]);
                        dr["VIEW_YN"] = ConvertUtil.ToInt32(obj["VIEW_YN"]);
                        dr["STATE"] = 0;

                        _dtView.Rows.Add(dr);
                    }
                }
            }
        }

        private void lcgUser_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Dangol.ShowSplash();
            getUserInfo();
            Dangol.CloseSplash();
        }


        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                Dangol.ShowSplash();
                getCategoryType();
                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvCategory.FocusedRowHandle;
                gvCategory.FocusedRowHandle = -2147483646;
                gvCategory.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtCategory.Select("STATE = 2");
                if (rows.Length < 1)
                {
                    Dangol.Warining("수정할 항목이 없습니다.");
                }
                else
                {
                    if (Dangol.MessageYN("수정사항을 저장하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();
                        JObject jobj = new JObject();
                        var jArrayData = new JArray();
                        string url = "/board/updateViewType.json";

                        foreach (DataRow row in rows)
                        {
                            JObject jdata = new JObject();
                            jdata.Add("VIEW_ID", ConvertUtil.ToInt32(row["VIEW_ID"]));
                            jdata.Add("VIEW_YN", ConvertUtil.ToInt32(row["VIEW_YN"]));
                            jdata.Add("SELECT_USER_ID", ConvertUtil.ToString(_currentRow["USER_ID"]));
                            jArrayData.Add(jdata);
                        }

                        jobj.Add("DATA", jArrayData);

                        if (DBConnect.getRequest(jobj, ref jResult, url))
                        {
                            //gvCategory.BeginDataUpdate();
                            foreach (DataRow row in rows)
                                row["STATE"] = 0;
                            //gvCategory.EndDataUpdate();
                            //gvCategory.RefreshData();
                            Dangol.Message("처리되었습니다.");
                        }
                    }
                }
            }
        }

        private void layoutControlGroup2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                Dangol.ShowSplash();
                getViewType();
                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvView.FocusedRowHandle;
                gvView.FocusedRowHandle = -2147483646;
                gvView.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtView.Select("STATE = 2");
                if (rows.Length < 1)
                {
                    Dangol.Warining("수정할 항목이 없습니다.");
                }
                else
                {
                    if (Dangol.MessageYN("수정사항을 저장하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();
                        JObject jobj = new JObject();
                        var jArrayData = new JArray();
                        string url = "/board/updateViewType.json";

                        foreach (DataRow row in rows)
                        {
                            JObject jdata = new JObject();
                            jdata.Add("VIEW_ID", ConvertUtil.ToInt32(row["VIEW_ID"]));
                            jdata.Add("VIEW_YN", ConvertUtil.ToInt32(row["VIEW_YN"]));
                            jdata.Add("SELECT_USER_ID", ConvertUtil.ToString(_currentRow["USER_ID"]));
                            jArrayData.Add(jdata);
                        }

                        jobj.Add("DATA", jArrayData);

                        if (DBConnect.getRequest(jobj, ref jResult, url))
                        {
                            gvView.BeginDataUpdate();
                            foreach (DataRow row in rows)
                                row["STATE"] = 0;
                            gvView.EndDataUpdate();

                            Dangol.Message("처리되었습니다.");
                        }
                    }
                }
            }
        }

        private void gvCategory_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView View = sender as GridView;
               
                if (e.Column.FieldName == "VIEW_NAME")
                {
                    int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["STATE"]));

                    if (state == 2)
                        e.Appearance.BackColor = Color.FromArgb(150, Color.DarkOrange);
                }
            }
        }

        private void gvView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView View = sender as GridView;

                if (e.Column.FieldName == "VIEW_NAME")
                {
                    int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["STATE"]));

                    if (state == 2)
                        e.Appearance.BackColor = Color.FromArgb(150, Color.DarkOrange);
                }
            }
        }

        private void gvCategory_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_currentCategoryRow != null)
            {
                //int state = ConvertUtil.ToInt32(_currentCategoryRow["STATE"]);
                _currentCategoryRow["STATE"] = 2;
            }
        }

        private void gvView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_currentViewRow != null)
            {
                //int state = ConvertUtil.ToInt32(_currentViewRow["STATE"]);
                _currentViewRow["STATE"] = 2;
                
            }
        }

        private void lcgUser_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Common.gridViewButtonChecked(gvUser, _dtUser);
        }

        private void lcgUser_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Common.gridViewButtonUnchecked(gvUser, _dtUser);
        }

        private void sbChange_Click(object sender, EventArgs e)
        {
            DataRow[] rows = _dtUser.Select("CHECK = true");
            if (rows.Length < 1)
            {
                Dangol.Warining("체크된 유저가 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN($"체크한 유저의 화면설정을 {teUserNm.Text} 유저의 화면 설정으로 복사하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    List<string> listUserId = new List<string>();
                    string url = "/board/changeViewType.json";

                    foreach (DataRow row in rows)
                        listUserId.Add(ConvertUtil.ToString(row["USER_ID"]));

                    jobj.Add("LIST_USER_ID", string.Join(",", listUserId));
                    jobj.Add("SELECT_USER_ID", ConvertUtil.ToString(_currentRow["USER_ID"]));

                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        foreach (DataRow row in rows)
                            row["CHECK"] = false;

                        Dangol.Message("처리되었습니다.");
                    }
                }
            }
        }

        private void gvUser_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView View = sender as GridView;

                if (e.Column.FieldName == "USER_ID")
                {
                    string type = ConvertUtil.ToString(View.GetRowCellValue(e.RowHandle, View.Columns["USER_TYPE_CD"]));

                    if (type.Equals("-1"))
                    {
                        e.Appearance.BackColor = Color.FromArgb(150, Color.Black);
                        e.Appearance.ForeColor = Color.FromArgb(150, Color.White);
                    }
                       
                }
            }
        }

        private void sbNewViewUpdate_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN($"화면을 갱신하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();
                List<string> listUserId = new List<string>();
                string url = "/board/updateUserView.json";

                //jobj.Add("SELECT_USER_ID", "shlee");

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    Dangol.Message("처리되었습니다.");
                }
            }
        }
    }
}