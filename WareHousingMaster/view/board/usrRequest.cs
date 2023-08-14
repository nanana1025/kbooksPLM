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
    public partial class usrRequest : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _currentRequest;
        DataTable _dtReceipt;
        BindingSource _bsReceipt;

        int _requestId;
        string _currentUserId;


        public usrRequest()
        {
            InitializeComponent();


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST_STATE", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST_DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RESPONSE", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("CREATE_USER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RESPONSE_USER_ID", typeof(string)));


            _bsReceipt = new BindingSource();
            _bsReceipt.DataSource = _dtReceipt;

            gcRequest.DataSource = _bsReceipt;

        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {

            setInfoBox();

            teRequest.DataBindings.Add(new Binding("Text", _bsReceipt, "REQUEST", false, DataSourceUpdateMode.Never));
            deReceiptDt.DataBindings.Add(new Binding("Text", _bsReceipt, "CREATE_DT", false, DataSourceUpdateMode.Never));
            leRequestState.DataBindings.Add(new Binding("EditValue", _bsReceipt, "REQUEST_STATE", false, DataSourceUpdateMode.Never));
            teCategory.DataBindings.Add(new Binding("Text", _bsReceipt, "CATEGORY", false, DataSourceUpdateMode.Never));
            meRequest.DataBindings.Add(new Binding("Text", _bsReceipt, "REQUEST_DES", false, DataSourceUpdateMode.Never));
            meResponse.DataBindings.Add(new Binding("Text", _bsReceipt, "RESPONSE", false, DataSourceUpdateMode.Never));
            leUser.DataBindings.Add(new Binding("EditValue", _bsReceipt, "CREATE_USER_ID", false, DataSourceUpdateMode.Never));
            leResponseUser.DataBindings.Add(new Binding("Text", _bsReceipt, "RESPONSE_USER_ID", false, DataSourceUpdateMode.Never));


            var today = DateTime.Today;
            var pastDate = today.AddDays(-10);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

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
            Util.LookupEditHelper(leUser, dtUserId, "KEY", "VALUE");

            Util.insertRowonTop(dtUserId, "-1", "알수없음");
            Util.LookupEditHelper(rileUser, dtUserId, "KEY", "VALUE");


            DataTable dtRequestState = Util.getCodeList("CD1401", "KEY", "VALUE");
            Util.LookupEditHelper(rileRequestState, dtRequestState, "KEY", "VALUE");
            Util.LookupEditHelper(leRequestState, dtRequestState, "KEY", "VALUE");


        }

        private void setEditable(bool flag)
        {
            teCategory.ReadOnly = !flag;
            meRequest.ReadOnly = !flag;
            meResponse.ReadOnly = flag;

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

                teCategory.ReadOnly = true;
                meRequest.ReadOnly = true;
                meResponse.ReadOnly = true;
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

            if (DBBoard.searchRequestList(jData, ref jResult))
            {
                gvRequest.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceipt.NewRow();

                    string createUserId = ConvertUtil.ToString(obj["CREATE_USER_ID"]);

                    dr["NO"] = index++;
                    dr["REQUEST_ID"] = obj["REQUEST_ID"];
                    dr["REQUEST"] = obj["REQUEST"];

                    dr["REQUEST_STATE"] = obj["REQUEST_STATE"];
                    dr["CATEGORY"] = obj["CATEGORY"];
                    dr["REQUEST_DES"] = obj["REQUEST_DES"];
                    dr["RESPONSE"] = obj["RESPONSE"];
                    if (createUserId.Equals(ProjectInfo._userId))
                        dr["CREATE_USER_ID"] = obj["CREATE_USER_ID"];
                    else
                        dr["CREATE_USER_ID"] =-1;
                   
                    dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                    dr["RESPONSE_USER_ID"] = obj["RESPONSE_USER_ID"];

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

                jData.Add("RECEIPT_DT_S", dtFrom);
                jData.Add("RECEIPT_DT_E", dtTo);
            }
            else
            {
                date = 0;
            }

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCreateUserNm.Text)))
            {
                if (!teCreateUserNm.Text.Equals(ProjectInfo._userName))
                {
                    jData.Add("MSG", "요청인은 공백 또는 본인 이름만 입력 가능합니다.");
                    return false;
                } 
                else
                {
                    jData.Add("USER_NM", teCreateUserNm.Text);
                    date++;
                }
            }

            return true;
        }

        private void lcgBoard_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                JObject jResult = new JObject();

                if (DBBoard.insertRequestList(ref jResult))
                {
                    gvRequest.BeginDataUpdate();
                    
                    DataRow dr = _dtReceipt.NewRow();
                    dr["NO"] = 1;
                    dr["REQUEST_ID"] = jResult["REQUEST_ID"];
                    dr["REQUEST"] = jResult["REQUEST"];

                    dr["REQUEST_STATE"] = "1";
                    dr["CATEGORY"] = "";
                    dr["REQUEST_DES"] = "";
                    dr["RESPONSE"] = "";
                    dr["CREATE_USER_ID"] = ProjectInfo._userId;

                    dr["CREATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                    dr["RESPONSE_USER_ID"] = "";
                    _dtReceipt.Rows.InsertAt(dr, 0);

                    DataRow[] rows = _dtReceipt.Select("REQUEST_ID > 0", "REQUEST_ID DESC");

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
                    Dangol.Message("선택된 요청사항이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN($"현재 건의사항을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    if(!_currentUserId.Equals(ProjectInfo._userId))
                    {
                        Dangol.Message("본인이 작성한 건의사항만 삭제할 수 있습니다.");
                        return;
                    }

                    JObject jResult = new JObject();
                    JObject jData = new JObject();
                    jData.Add("REQUEST_ID", ConvertUtil.ToInt64(_currentRequest["REQUEST_ID"]));

                    if (DBBoard.deleteRequestList(jData, ref jResult))
                    {
                        gvRequest.BeginDataUpdate();

                        _dtReceipt.BeginInit();
                        _currentRequest.Delete();

                        DataRow[] rows = _dtReceipt.Select("REQUEST_ID > 0", "REQUEST_ID DESC");

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
                Dangol.Message("선택된 건의사항이 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("REQUEST_ID", ConvertUtil.ToInt64(_currentRequest["REQUEST_ID"]));
            jData.Add("REQUEST_STATE", ConvertUtil.ToString(leRequestState.EditValue));

            if (ConvertUtil.ToString(_currentRequest["CREATE_USER_ID"]).Equals(ProjectInfo._userId))
            {
                jData.Add("CATEGORY", teCategory.Text);
                jData.Add("REQUEST_DES", meRequest.Text);
            }
            else
            {
                jData.Add("RESPONSE", meResponse.Text);
                jData.Add("RESPONSE_USER_ID", ProjectInfo._userId);
            }

            if (DBBoard.updateRequestList(jData, ref jResult))
            {
                _currentRequest["REQUEST_STATE"] = jData["REQUEST_STATE"];

                gvRequest.BeginDataUpdate();
                _currentRequest.BeginEdit();
                if (ConvertUtil.ToString(_currentRequest["CREATE_USER_ID"]).Equals(ProjectInfo._userId))
                {
                    _currentRequest["CATEGORY"] = jData["CATEGORY"];
                    _currentRequest["REQUEST_DES"] = jData["REQUEST_DES"];
                }
                else
                {
                    _currentRequest["RESPONSE"] = jData["RESPONSE"];
                    _currentRequest["RESPONSE_USER_ID"] = ProjectInfo._userId;
                }
                _currentRequest.EndEdit();
                gvRequest.EndDataUpdate();

                Dangol.Message("수정되었습니다.");
            }
        }

        
    }
}