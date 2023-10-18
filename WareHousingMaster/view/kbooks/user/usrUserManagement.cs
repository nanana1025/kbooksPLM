using DevExpress.XtraSplashScreen;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.user
{
    public partial class usrUserManagement : DevExpress.XtraEditors.XtraForm
    {
        string _userId = "-1";
        DataRowView _currentRow;
        DataTable _dtUser;
        BindingSource _bs;


        public usrUserManagement()
        {
            InitializeComponent();


            _dtUser = new DataTable();

            _dtUser.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtUser.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtUser.Columns.Add(new DataColumn("PASSWD", typeof(string)));
            _dtUser.Columns.Add(new DataColumn("USER_NM", typeof(string)));
            _dtUser.Columns.Add(new DataColumn("USER_TYPE", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("COMPANY_TYPE", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("STATE_CD", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            //_dtUser.Columns.Add(new DataColumn("DEPT_CD", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("MOBILE", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("TEAM_CD", typeof(string)));
            //_dtUser.Columns.Add(new DataColumn("POSITION", typeof(string)));



            _bs = new BindingSource();

        }



        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();
            getUserInfo();

            teUserId.DataBindings.Add(new Binding("Text", _bs, "USER_ID", false, DataSourceUpdateMode.Never));
            teUserNm.DataBindings.Add(new Binding("Text", _bs, "USER_NM", false, DataSourceUpdateMode.Never));

            leUserType.DataBindings.Add(new Binding("EditValue", _bs, "USER_TYPE", false, DataSourceUpdateMode.Never));
            //leUserState.DataBindings.Add(new Binding("EditValue", _bs, "STATE_CD", false, DataSourceUpdateMode.Never));
            //leUserCompanyType.DataBindings.Add(new Binding("EditValue", _bs, "COMPANY_TYPE", false, DataSourceUpdateMode.Never));
            
            //leCompanyId.DataBindings.Add(new Binding("EditValue", _bs, "COMPANY_ID", false, DataSourceUpdateMode.Never));

            //leDeptCd.DataBindings.Add(new Binding("EditValue", _bs, "DEPT_CD", false, DataSourceUpdateMode.Never));
            //teTel.DataBindings.Add(new Binding("Text", _bs, "MOBILE", false, DataSourceUpdateMode.Never));
            //leTeamCd.DataBindings.Add(new Binding("EditValue", _bs, "TEAM_CD", false, DataSourceUpdateMode.Never));
            //lePosition.DataBindings.Add(new Binding("EditValue", _bs, "POSITION", false, DataSourceUpdateMode.Never));

        }

        //public IOverlaySplashScreenHandle ShowProgressPanel()
        //{
        //    return SplashScreenManager.ShowOverlayForm(this);
        //}

        //public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        //{
        //    if (handle != null)
        //        SplashScreenManager.CloseOverlayForm(handle);
        //}

        private void setInfoBox()
        {
            //DataTable dtUserType = Util.getCodeList("CD0201", "KEY", "VALUE");

            DataTable dtUserType = new DataTable();
            dtUserType.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtUserType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRow(dtUserType, "M", "마스터관리자");
            Util.insertRow(dtUserType, "S", "관리자");
            Util.insertRow(dtUserType, "U", "일반유저");

            Util.LookupEditHelper(rileUserType, dtUserType, "KEY", "VALUE");
            Util.LookupEditHelper(leUserType, dtUserType, "KEY", "VALUE");

            //DataTable dtUserState = Util.getCodeList("CD0202", "KEY", "VALUE");
            //Util.LookupEditHelper(rileUserState, dtUserState, "KEY", "VALUE");
            //Util.LookupEditHelper(leUserState, dtUserState, "KEY", "VALUE");

            //DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            //Util.insertRowonTop(dtCompany, "-1", "-");
            //Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            //Util.LookupEditHelper(leCompanyId, dtCompany, "KEY", "VALUE");

            //DataTable dtDeptCd = Util.getCodeList("CD0502", "KEY", "VALUE");
            //Util.insertRowonTop(dtDeptCd, "-1", "-");
            //Util.LookupEditHelper(rileDeptCd, dtDeptCd, "KEY", "VALUE");
            //Util.LookupEditHelper(leDeptCd, dtDeptCd, "KEY", "VALUE");

            //DataTable dtTeamCd = Util.getCodeList("CD0503", "KEY", "VALUE");
            //Util.insertRowonTop(dtTeamCd, "-1", "-");
            //Util.LookupEditHelper(rileTeamCd, dtTeamCd, "KEY", "VALUE");
            //Util.LookupEditHelper(leTeamCd, dtTeamCd, "KEY", "VALUE");

            //DataTable dtPosition = Util.getCodeList("CD0505", "KEY", "VALUE");
            //Util.insertRowonTop(dtPosition, "-1", "-");
            //Util.LookupEditHelper(rilePosition, dtPosition, "KEY", "VALUE");
            //Util.LookupEditHelper(lePosition, dtPosition, "KEY", "VALUE");


            //DataTable dtCompanyType = new DataTable();
            //dtCompanyType.Columns.Add(new DataColumn("KEY", typeof(string)));
            //dtCompanyType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtCompanyType, "I", "내부");
            //Util.insertRowonTop(dtCompanyType, "E", "외부");

            //Util.LookupEditHelper(leUserCompanyType, dtCompanyType, "KEY", "VALUE");
            //Util.LookupEditHelper(rileCompanyType, dtCompanyType, "KEY", "VALUE");       
        }

        private void setIInitData()
        {

        }


        private void setGridControl()
        {
            _bs.DataSource = _dtUser;
            gcUser.DataSource = null;
            gcUser.DataSource = _bs;

        }


        private void gvCompany_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvUser.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _userId = ConvertUtil.ToString(_currentRow["USER_ID"]);
            }
            else
            {
                _currentRow = null;
                _userId = "-1";
            }
        }

        private void gvCompany_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvUser.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvUser.GetRow(e.FocusedRowHandle) as DataRowView;
                _userId = ConvertUtil.ToString(_currentRow["USER_ID"]);
            }
            else
            {
                _currentRow = null;
                _userId = "-1";
            }
        }


        private void getUserInfo()
        {
            JObject jResult = new JObject();

            gvUser.BeginDataUpdate();
            _dtUser.Clear();

            DataTable dtUser = Util.getTable("TN_USER_MST", "USER_ID,PASSWD,USER_NM,USER_TYPE", "USER_ID IS NOT NULL", "USER_NM ASC");
            int index = 1;

            foreach (DataRow obj in dtUser.Rows)
            {
                DataRow dr = _dtUser.NewRow();

                dr["NO"] = index++;
                dr["USER_ID"] = ConvertUtil.ToString(obj["USER_ID"]);
                dr["PASSWD"] = ConvertUtil.ToString(obj["PASSWD"]);
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

        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("선택하신 유저정보를 저장하시겠습니까?") == DialogResult.Yes)
            {
                //JObject jPartInfo = new JObject();
                JObject jResult = new JObject();
                JObject jobj = new JObject();
                string url = "/common/execute.json";

                jobj.Add("USER_ID", _userId);
                jobj.Add("USER_NM", ConvertUtil.ToString(teUserNm.Text));
                jobj.Add("USER_TYPE", ConvertUtil.ToString(leUserType.EditValue));

                ////jPartInfo.Add("STATE_CD", ConvertUtil.ToString(leUserState.EditValue));
                ////jPartInfo.Add("COMPANY_ID", ConvertUtil.ToInt64(leCompanyId.EditValue));
                ////jPartInfo.Add("COMPANY_TYPE", ConvertUtil.ToString(leUserCompanyType.EditValue));
                ////jPartInfo.Add("MOBILE", ConvertUtil.ToString(teTel.Text));
                ////jPartInfo.Add("DEPT_CD", ConvertUtil.ToString(leDeptCd.EditValue));
                ////jPartInfo.Add("TEAM_CD", ConvertUtil.ToString(leTeamCd.EditValue));
                ////jPartInfo.Add("POSITION", ConvertUtil.ToString(lePosition.EditValue));

                string query = $"UPDATE TN_USER_MST SET USER_NM = '{teUserNm.Text.Trim()}', USER_TYPE = '{leUserType.EditValue}' WHERE USER_ID = '{_userId}'";

                jobj.Add("QUERY", query);

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    _currentRow.BeginEdit();

                    _currentRow["USER_NM"] = jobj["USER_NM"];
                    _currentRow["USER_TYPE"] = jobj["USER_TYPE"];
                    //_currentRow["COMPANY_TYPE"] = jPartInfo["COMPANY_TYPE"];
                    //_currentRow["COMPANY_ID"] = jPartInfo["COMPANY_ID"];
                    //_currentRow["MOBILE"] = jPartInfo["MOBILE"];
                    //_currentRow["DEPT_CD"] = jPartInfo["DEPT_CD"];
                    //_currentRow["TEAM_CD"] = jPartInfo["TEAM_CD"];
                    //_currentRow["POSITION"] = jPartInfo["POSITION"];
                    _currentRow.EndEdit();

                    Dangol.Message("수정되었습니다");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
        }

        private void lcgInventory_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(9))
            {
                Dangol.ShowSplash();
                getUserInfo();
                Dangol.CloseSplash();

            }
            else if (e.Button.Properties.Tag.Equals(1))
            {
                dlgCreateUser dlgCreateUser = new dlgCreateUser();

                //dlgCreateUser.StartPosition = FormStartPosition.Manual;
                //dlgCreateUser.Location = new Point(this.Location.X + (this.Size.Width / 2) - (dlgCreateUser.Size.Width / 2),
                //this.Location.Y + (this.Size.Height / 2) - (dlgCreateUser.Size.Height / 2));

                if (dlgCreateUser.ShowDialog(this) == DialogResult.OK)
                {
                    Dangol.ShowSplash();
                    getUserInfo();
                    Dangol.CloseSplash();
                }

            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_currentRow == null)
                {
                    Dangol.Message("선택하신 유저가 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 유저를 삭제하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                JObject jResult = new JObject();
                JObject jobj = new JObject();
                string url = "/common/execute.json";

                string query = $"DELETE FROM TN_USER_MST WHERE USER_ID = '{_userId}'";

                jobj.Add("QUERY", query);

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    gvUser.BeginDataUpdate();
                    _currentRow.Delete();
                    gvUser.EndDataUpdate();

                    Dangol.Message("삭제되었습니다");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }

            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                //dlgUserPassword dlgCreateUser = new dlgUserPassword(ProjectInfo._userId, ProjectInfo._userPasswd);

                dlgUserPassword dlgPasswd = new dlgUserPassword(ConvertUtil.ToString(_currentRow["USER_ID"]), ConvertUtil.ToString(_currentRow["PASSWD"]), true);

                //dlgCreateUser.StartPosition = FormStartPosition.Manual;
                //dlgCreateUser.Location = new Point(this.Location.X + (this.Size.Width / 2) - (dlgCreateUser.Size.Width / 2),
                //this.Location.Y + (this.Size.Height / 2) - (dlgCreateUser.Size.Height / 2));

                if (dlgPasswd.ShowDialog(this) == DialogResult.OK)
                {

                }

            }

        }

        private void lcgInventory_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //_bs.Filter = null;
        }

        private void lcgInventory_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //_bs.Filter = "STATE_CD = 'A'";
        }
    }
}