using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WareHousingMaster.view.common;
using DevExpress.XtraSplashScreen;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.inventory;
using DevExpress.XtraGrid.Views.Grid;
using WareHousingMaster.view.warehousing.createComponent;
using WareHousingMaster.view.warehousing.selectComponent;
using DevExpress.XtraTreeList.Nodes;
using WareHousingMaster.view.usedPurchase;
using ImportExcel;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;

namespace WareHousingMaster.view.board
{
    public partial class usrJob : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _currentRequest;
        DataTable _dtReceipt;
        BindingSource _bsReceipt;

        int _requestId;
        string _currentUserId;


        public usrJob()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("JOB_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("JOB", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("JOB_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("LEVEL", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("TITLE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("FROM_USER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TO_USER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CREATE_USER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
           


            _bsReceipt = new BindingSource();
            _bsReceipt.DataSource = _dtReceipt;

            gcRequest.DataSource = _bsReceipt;

        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {

            setInfoBox();

            teJob.DataBindings.Add(new Binding("Text", _bsReceipt, "JOB", false, DataSourceUpdateMode.Never));
            leToUser.DataBindings.Add(new Binding("EditValue", _bsReceipt, "TO_USER_ID", false, DataSourceUpdateMode.Never));
            leFromUser.DataBindings.Add(new Binding("EditValue", _bsReceipt, "FROM_USER_ID", false, DataSourceUpdateMode.Never));
            leCreateUser.DataBindings.Add(new Binding("EditValue", _bsReceipt, "CREATE_USER_ID", false, DataSourceUpdateMode.Never));
            leJobState.DataBindings.Add(new Binding("EditValue", _bsReceipt, "JOB_STATE", false, DataSourceUpdateMode.Never));
            teTitle.DataBindings.Add(new Binding("Text", _bsReceipt, "TITLE", false, DataSourceUpdateMode.Never));
            meDes.DataBindings.Add(new Binding("Text", _bsReceipt, "DES", false, DataSourceUpdateMode.Never));
            leLevel.DataBindings.Add(new Binding("EditValue", _bsReceipt, "LEVEL", false, DataSourceUpdateMode.Never));


            var today = DateTime.Today;
            var pastDate = today.AddDays(-100);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

            leFromUser1.EditValue = -1;
            leToUser1.EditValue = -1;

            getReceiptList(true);

        }

        public IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        private void setInfoBox()
        {
            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(leFromUser, dtUserId, "KEY", "VALUE");
            Util.LookupEditHelper(leToUser, dtUserId, "KEY", "VALUE");
            Util.LookupEditHelper(rileUser, dtUserId, "KEY", "VALUE"); 
            Util.LookupEditHelper(leCreateUser, dtUserId, "KEY", "VALUE");

            DataTable dtUserId1 = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.insertRowonTop(dtUserId1, "-1", "전체");

            Util.LookupEditHelper(leFromUser1, dtUserId1, "KEY", "VALUE");
            Util.LookupEditHelper(leToUser1, dtUserId1, "KEY", "VALUE");

            DataTable dtRequestState = Util.getCodeList("CD1402", "KEY", "VALUE");
            Util.LookupEditHelper(rileJobState, dtRequestState, "KEY", "VALUE");
            Util.LookupEditHelper(leJobState, dtRequestState, "KEY", "VALUE");


            DataTable dtLevel = new DataTable();

            dtLevel.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtLevel.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for(int i = 9; i > 0; i--)
                Util.insertRowonTop(dtLevel, i, i.ToString());

            Util.LookupEditHelper(leLevel, dtLevel, "KEY", "VALUE");

        }

        private void setEditable(bool flag)
        {
            lgcDetail.CustomHeaderButtons[0].Properties.Enabled = flag;

        }
  
        private void gvRequest_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvRequest.RowCount > 0);

            if (isValidRow)
            {
                _currentRequest = e.Row as DataRowView;

                _currentUserId = ConvertUtil.ToString(_currentRequest["CREATE_USER_ID"]);

                if (_currentUserId.Equals(ProjectInfo._userId))
                {
                    setEditable(true);
                }
                else
                {
                    setEditable(false);
                }
            }
            else
            {
                _currentRequest = null;
            }
        }


        private void sbSearch_Click(object sender, EventArgs e)
        {
            getReceiptList(false);
        }

        private bool getReceiptList(bool isinit)
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtReceipt.Clear();

            if (DBBoard.searchJobList(jData, ref jResult))
            {
                gvRequest.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceipt.NewRow();

                    dr["NO"] = index++;
                    dr["JOB_ID"] = obj["JOB_ID"];
                    dr["JOB"] = obj["JOB"];

                    dr["JOB_STATE"] = obj["JOB_STATE"];
                    dr["LEVEL"] = obj["LEVEL"];
                    dr["TITLE"] = obj["TITLE"];
                    dr["DES"] = obj["DES"];
                    dr["FROM_USER_ID"] = obj["FROM_USER_ID"];
                    dr["TO_USER_ID"] = obj["TO_USER_ID"];
                    dr["CREATE_USER_ID"] = obj["CREATE_USER_ID"];
                    dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                    
                    _dtReceipt.Rows.Add(dr);
                }

                gvRequest.EndDataUpdate();

                gvRequest.FocusedRowHandle = -2147483646;
                gvRequest.MoveFirst();

                return true;

            }
            else
            {
                return false;
            }
        }

        private bool checkSearch(ref JObject jData)
        {

            string dtFrom = "";
            string dtTo = "";

            int date = 0;
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
            {
                dtFrom = $"{deDtFrom.Text} 00:00:00";
                date++;
            }

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
            {
                dtTo = $"{deDtTo.Text} 23:59:59";
                date++;
            }

            if (date == 2)
            {
                DateTime dtfrom;
                DateTime dtto;
                dtfrom = Convert.ToDateTime(dtFrom);
                dtto = Convert.ToDateTime(dtTo);

                int result = DateTime.Compare(dtfrom, dtto);

                if (result > 0)
                {
                    jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다.");
                    return false;
                }

                TimeSpan TS = dtto - dtfrom;
                int diffDay = TS.Days;

                if (diffDay > 180)
                {
                    jData.Add("MSG", "검색 기간은 6개월(180일)을 초과할 수 없습니다.");
                    return false;
                }

                jData.Add("CREATE_DT_S", dtFrom);
                jData.Add("CREATE_DT_E", dtTo);
            }
            else
            {
                date = 0;
            }

            if (ConvertUtil.ToInt64(leFromUser1.EditValue) != -1)
            {
                jData.Add("FROM_USER_ID", ConvertUtil.ToInt64(leFromUser1.EditValue));
                date++;      
            }

            if (ConvertUtil.ToInt64(leToUser1.EditValue) != -1)
            {
                jData.Add("TO_USER_ID", ConvertUtil.ToInt64(leToUser1.EditValue));
                date++;
            }

            return true;
        }

        private void lcgBoard_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                JObject jResult = new JObject();

                if (DBBoard.insertJobList(ref jResult))
                {
                    gvRequest.BeginDataUpdate();
                    
                    DataRow dr = _dtReceipt.NewRow();
                    dr["NO"] = 1;
                    dr["JOB_ID"] = jResult["JOB_ID"];
                    dr["JOB"] = jResult["JOB"];

                    dr["JOB_STATE"] = "1";
                    dr["TITLE"] = "";
                    dr["DES"] = "";
                    dr["LEVEL"] = 1;
                    dr["FROM_USER_ID"] = ProjectInfo._userId;
                    dr["TO_USER_ID"] = ProjectInfo._userId;
                    dr["CREATE_USER_ID"] = ProjectInfo._userId;

                    dr["CREATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                    _dtReceipt.Rows.InsertAt(dr, 0);

                    DataRow[] rows = _dtReceipt.Select("JOB_ID > 0", "JOB_ID DESC");

                    int index = 1;
                    foreach (DataRow row in rows)
                    {
                        row["NO"] = index++;
                    }

                    gvRequest.EndDataUpdate();

                    gvRequest.MoveFirst();

                    Dangol.Message("추가되었습니다.");
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_currentRequest == null)
                {
                    Dangol.Message("선택된 작업이 없습니다.");
                    return;
                }

                if (!ProjectInfo._userType.Equals("M") && !ProjectInfo._userType.Equals("S"))
                {
                    if (!_currentUserId.Equals(ProjectInfo._userId))
                    {
                        Dangol.Message("본인이 생성한 작업만 삭제 가능합니다.");
                        return;
                    }
                }

                if (Dangol.MessageYN($"현재 작업을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jData = new JObject();
                    jData.Add("JOB_ID", ConvertUtil.ToInt64(_currentRequest["JOB_ID"]));

                    if (DBBoard.deleteJobList(jData, ref jResult))
                    {
                        gvRequest.BeginDataUpdate();

                        _dtReceipt.BeginInit();
                        _currentRequest.Delete();

                        DataRow[] rows = _dtReceipt.Select("JOB_ID > 0", "JOB_ID DESC");

                        int index = 1;
                        foreach (DataRow row in rows)
                        {
                            row["NO"] = index++;
                        }

                        _dtReceipt.EndInit();
                        gvRequest.EndDataUpdate();
                        gvRequest.MoveFirst();

                        Dangol.Message("삭제되었습니다.");
                    }
                }
            }
        }

        private void lgcDetail_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if(_currentRequest == null)
            {
                Dangol.Message("선택된 작업이 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("JOB_ID", ConvertUtil.ToInt64(_currentRequest["JOB_ID"]));
            jData.Add("JOB_STATE", ConvertUtil.ToString(leJobState.EditValue));
            jData.Add("TITLE", teTitle.Text);
            jData.Add("DES", meDes.Text);
            jData.Add("LEVEL", ConvertUtil.ToString(leLevel.EditValue));
            jData.Add("FROM_USER_ID", ConvertUtil.ToString(leFromUser.EditValue));
            jData.Add("TO_USER_ID", ConvertUtil.ToString(leToUser.EditValue));

            if (DBBoard.updateJobList(jData, ref jResult))
            {
                gvRequest.BeginDataUpdate();
                _currentRequest.BeginEdit();

                _currentRequest["JOB_STATE"] = jData["JOB_STATE"];
                _currentRequest["TITLE"] = jData["TITLE"];
                _currentRequest["DES"] = jData["DES"];
                _currentRequest["LEVEL"] = jData["LEVEL"];
                _currentRequest["FROM_USER_ID"] = jData["FROM_USER_ID"];
                _currentRequest["TO_USER_ID"] = jData["TO_USER_ID"];

                _currentRequest.EndEdit();
                gvRequest.EndDataUpdate();

                Dangol.Message("수정되었습니다.");
            }
        }

        private void gvRequest_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0 && e.Column.FieldName == "JOB_STATE")
            {
                int value = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["JOB_STATE"]));

                if (value == 2)
                    e.Appearance.BackColor = System.Drawing.Color.LightGreen;
                else if (value == 3)
                    e.Appearance.BackColor = System.Drawing.Color.LightGray;
                else if (value == 4)
                    e.Appearance.BackColor = System.Drawing.Color.DarkSalmon;
                else if (value == 5)
                    e.Appearance.BackColor = System.Drawing.Color.DarkSeaGreen;

            }
            else if (e.RowHandle >= 0 && e.Column.FieldName == "LEVEL")
            {
                int value = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["LEVEL"]));

                switch (value)
                {
                    case 1:
                        break;
                    case 2:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(254, 234, 232);
                        break;
                    case 3:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(253, 226, 223);
                        break;
                    case 4:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(252,212,208);
                        break;
                    case 5:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(250,187,180);
                        break;
                    case 6:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(247,155,145);
                        break;
                    case 7:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(252,129,120);
                        break;
                    case 8:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(251,91,79);
                        break;
                    case 9:
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(255,0,0);
                        break;
                }
                   
            }
        }
    }
}