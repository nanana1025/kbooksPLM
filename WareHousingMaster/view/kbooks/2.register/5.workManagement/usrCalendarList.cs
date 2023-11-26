using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.register
{
    public partial class usrCalendarList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        bool[,] _isAllowEdit;
        bool[] _isChanged;

        int _ROWHEIGHT1 = 40;
        int _ROWHEIGHT2 = 20;

        int _dayWeek;
        int _lastDay;
        int _shopCd;
        bool _exist;
        int _thisMonth;


        Dictionary<int, int[]> _dicDatePosition;
        


        //JObject _jobj;

        //long _representativeId;
        //string _representativeIdCol;
        //string _representativeStateCol;
        //string _tableNm;
        //int _representativeState;

        //bool _isUpdate;

        //int _viewType;
        //int _processType;

        //public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        //public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;

        //public delegate void SelectHandler();
        //public event SelectHandler selectHandler;

        public delegate void ExistHandler(bool exist);
        public event ExistHandler existHandler;


        public usrCalendarList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("D0", typeof(int)));
            _dt.Columns.Add(new DataColumn("D1", typeof(int)));
            _dt.Columns.Add(new DataColumn("D2", typeof(int)));
            _dt.Columns.Add(new DataColumn("D3", typeof(int)));
            _dt.Columns.Add(new DataColumn("D4", typeof(int)));
            _dt.Columns.Add(new DataColumn("D5", typeof(int)));
            _dt.Columns.Add(new DataColumn("D6", typeof(int)));
            _bs = new BindingSource();

            _isAllowEdit = new bool[12,7];
            _isChanged = new bool[32];

            _dicDatePosition = new Dictionary<int, int[]>();

            _dayWeek = -1;
            _lastDay = -1;
            _thisMonth = ConvertUtil.ToInt32(DateTime.Now.ToString("yyyyMM"));
        }



        public void setInitLoad(int shopCd)
        {
            _shopCd = shopCd;
            setInfoBox();
            setinitialize();
            
        }

        private void setInfoBox()
        {

        }

        public void setinitialize()
        {
            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;

            var today = DateTime.Today;
            //DateTime day = Convert.ToDateTime($"{today.Year}-{today.Month}-1");

            DateTime MonthFirstDay = DateTime.Now.AddDays(1 - DateTime.Now.Day);

            DayOfWeek dayofweek = MonthFirstDay.DayOfWeek;
            DateTime MonthLastDay = MonthFirstDay.AddMonths(1).AddDays(-1);

            int dayWeek = (int)dayofweek;

            setinitData(dayWeek, MonthLastDay.Day);

            getHolidayData(MonthFirstDay.ToString("yyyyMM"));
            _thisMonth = ConvertUtil.ToInt32(MonthFirstDay.ToString("yyyyMM"));
        }
        public void setinitData(int dayWeek, int lastDay)
        {
            _dayWeek = dayWeek;
            _lastDay = lastDay;

            gvList.BeginDataUpdate();
            _dt.Clear();
            _dicDatePosition.Clear();

            for (int i = 0; i < 12; i++)
            {
                DataRow dr = _dt.NewRow();
                _dt.Rows.Add(dr);

                for (int j = 0; j < 7; j++)
                    _isAllowEdit[i,j] = false;
            }

            int row = 0;
            int day = 1;
            int col = 0;
            
            for (int i = dayWeek; i < lastDay+ dayWeek; i++)
            {
                col = i % 7;

                if (i != 0 && col == 0)
                    row += 2;

                _dt.Rows[row][col] = day;
                _dt.Rows[row+1][col] = 1;

                _isChanged[day] = false;

                int[] position = new int[] { row + 1, col };
                _dicDatePosition.Add(day, position);

                _isAllowEdit[row + 1, col] = true;

                day++;
            }


            gvList.EndDataUpdate();
        }

        public void setFlagInitialize()
        {
            int[] position;

            for (int i = 1; i < _lastDay + 1; i++)
            {
                _isChanged[i] = false;

                position = _dicDatePosition[i];
                _dt.Rows[position[0]][position[1]] = 1;
            }
        }

        public void SetCalendar(DateTime deMonth)
        {
            DayOfWeek dayofweek = deMonth.DayOfWeek;
            DateTime MonthLastDay = deMonth.AddMonths(1).AddDays(-1);

            int dayWeek = (int)dayofweek;

            setinitData(dayWeek, MonthLastDay.Day);
            getHolidayData(deMonth.ToString("yyyyMM"));
            _thisMonth = ConvertUtil.ToInt32(deMonth.ToString("yyyyMM"));
        }


        public void SetFocus()
        {
            gvList.Focus();
        }

        public void SetColFocus(int move = 0)
        {
            int rowHandle = 1;
            int[] position = _dicDatePosition[1];
            
            string col = $"D{position[1]+ move}";

            if ((position[1] == 0) || (position[1] == 6))
                col = $"D{position[1]}";
            else
                col = $"D{position[1] + move}";

            ColumnView View = (ColumnView)gcList.FocusedView;
            GridColumn column = View.Columns[col];
            if (column != null)
            {
                if (rowHandle != GridControl.InvalidRowHandle)
                {
                    View.FocusedRowHandle = rowHandle;
                    View.FocusedColumn = column;
                }
            }
        }

        public void setGridEditable(bool isEditable)
        {
            
        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GridView View = sender as GridView;

            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                int row = e.FocusedRowHandle;

                if (row % 2 == 0)
                {
                    View.FocusedColumn.OptionsColumn.ReadOnly = true;
                    View.FocusedColumn.OptionsColumn.AllowEdit = false;

                    //gvList.OptionsBehavior.ReadOnly = true;
                    //gvList.OptionsBehavior.Editable = false;
                }
                else
                {
                    string colNm = View.FocusedColumn.FieldName;

                    int value = ConvertUtil.ToInt32(_currentRow[colNm]);

                    int col = ConvertUtil.ToInt32(colNm.Substring(1, 1));

                    if (_isAllowEdit[row, col])
                    {
                        View.FocusedColumn.OptionsColumn.ReadOnly = false;
                        View.FocusedColumn.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        View.FocusedColumn.OptionsColumn.ReadOnly = true;
                        View.FocusedColumn.OptionsColumn.AllowEdit = false;
                    }
                }

            }
            else
            {
                _currentRow = null;
                View.FocusedColumn.OptionsColumn.ReadOnly = true;
                View.FocusedColumn.OptionsColumn.AllowEdit = false;
            }

        }

        public void getHolidayData(string date)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("DATE", date);

            string url = "/regist/getHolidayData.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                gvList.BeginDataUpdate();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int[] position;
                    int day;
                    string value;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        value = ConvertUtil.ToString(obj["B_DATE"]);
                        day = ConvertUtil.ToInt32(value.Substring(6, 2));

                        position = _dicDatePosition[day];

                        _dt.Rows[position[0]][position[1]] = ConvertUtil.ToInt32(obj["BUSINESS_FG"]);
                    }
                    existHandler(true);
                    _exist = true;
                }
                else
                {
                    setFlagInitialize();
                    existHandler(false);
                    _exist = false;
                }

                gvList.EndDataUpdate();
            }
            else
            {
                
                Dangol.Error(jResult["MSG"]);
            }
        }

        public void InsertUpdateHolidayDate()
        {
            if(_exist)
            {
                int[] position;
                int flag;
                bool isChanged = false;
                for (int i = 1; i < _lastDay + 1; i++)
                {
                    if (_isChanged[i])
                    {
                        isChanged = true;

                        position = _dicDatePosition[i];
                        flag = ConvertUtil.ToInt32(_dt.Rows[position[0]][position[1]]);
                        if(flag < 1 || (flag > 4 && flag != 9))
                        {
                            Dangol.Warining("점휴일 FLAG를 확인하세요.");
                            return;
                        }
                    }
                }

                if(isChanged)
                {
                    if(Dangol.MessageYN("확정하시겠습니까?") == DialogResult.Yes)
                        updateHolidayData();
                }
                else
                {
                    Dangol.Info("변경된 내용이 없습니다.");
                }
               
            }
            else
            {
                int[] position;
                int flag;
                for (int i = 1; i < _lastDay + 1; i++)
                {
                    position = _dicDatePosition[i];
                    flag = ConvertUtil.ToInt32(_dt.Rows[position[0]][position[1]]);
                    if (flag < 1 || (flag > 4 && flag != 9))
                    {
                        Dangol.Warining("점휴일 FLAG를 확인하세요.");
                        return;
                    }
                }

                if (Dangol.MessageYN("확정하시겠습니까?") == DialogResult.Yes)
                    insertHolidayData();
            }
        }

        public void insertHolidayData()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            jobj.Add("SHOPCD", _shopCd);

            int[] position;
            int flag;
            int col;
            for (int i = 1; i < _lastDay + 1; i++)
            {
                position = _dicDatePosition[i];
                col = position[1];

                flag = ConvertUtil.ToInt32(_dt.Rows[position[0]][position[1]]);

                JObject jdata = new JObject();

                jdata.Add("B_DATE", $"{_thisMonth}{i.ToString("D2")}");
                jdata.Add("BUSINESS_FG", flag);
                jdata.Add("WEEK_FG", col + 1);

                jArray.Add(jdata);
            }

            jobj.Add("DATA", jArray);

            string url = "/regist/insertHolidayData.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                for (int i = 1; i < _lastDay + 1; i++)
                    _isChanged[i] = false;

                existHandler(true);
                _exist = true;

                Dangol.Info("확정되었습니다");

            }
            else
            {

                Dangol.Error(jResult["MSG"]);
            }
        }

        public void updateHolidayData()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            int[] position;
            int flag;
            int col;
            for (int i = 1; i < _lastDay + 1; i++)
            {
                if (_isChanged[i])
                {
                    position = _dicDatePosition[i];
                    col = position[1];

                    flag = ConvertUtil.ToInt32(_dt.Rows[position[0]][position[1]]);

                    JObject jdata = new JObject();
                    jdata.Add("SHOPCD", _shopCd);
                    jdata.Add("B_DATE", $"{_thisMonth}{i.ToString("D2")}");
                    jdata.Add("BUSINESS_FG", flag);
                    jdata.Add("WEEK_FG", col + 1);

                    jArray.Add(jdata);
                }
            }

            jobj.Add("DATA", jArray);

            string url = "/regist/updateHolidayData.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {

                for (int i = 1; i < _lastDay + 1; i++)
                    _isChanged[i] = false;

                Dangol.Info("확정되었습니다");

            }
            else
            {

                Dangol.Error(jResult["MSG"]);
            }
        }



























        public bool editingCheck()
        {
            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("STATE = 2");  //shlee

            return rows.Length > 0;
        }


        public DataTable getTable()
        {
            return _dt;
        }

        public void receiptRefresh()
        {
            //gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            //getList(_jobj);
            //gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
        }

        public void viewRefresh()
        {
            //gvList.RefreshData();
        }

        public void gvList_CustomButtonChecked()
        {
            //Common.gridViewButtonChecked(gvList, _dt);
        }

        public void gvList_CustomButtonUnchecked()
        {
            //Common.gridViewButtonUnchecked(gvList, _dt);
        }

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_currentRow != null)
            {
                string colNm = e.Column.FieldName;
                int row = gvList.FocusedRowHandle -1 ;
                int col = ConvertUtil.ToInt32(colNm.Substring(1,1));

                DataRow rows = _dt.Rows[row];
                int day = ConvertUtil.ToInt32(rows[colNm]);
                _isChanged[day] = true;
            }
        }

        private void gvList_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (_currentRow != null)
            //        selectHandler();
            //}
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
        //    if (_currentRow != null)
        //    {
        //        doubleClickHandler();
        //    }
        }

        private void gvList_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.RowHandle %2 == 0)
                e.RowHeight = _ROWHEIGHT1;
            else
                e.RowHeight = _ROWHEIGHT2;
        }

        private void gvList_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle % 2 == 1)
            {
                e.Appearance.BackColor = Color.Aqua;
            }
        }

        private void gvList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView View = sender as GridView;

                if (e.RowHandle % 2 == 1)
                {
                    e.Appearance.BackColor = Color.Aqua;
                }
                else
                {
                    if (e.Column.FieldName == "D0")
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else if (e.Column.FieldName == "D6")
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.Blue;
                    }

                }
            }
        }

       

        private void gvList_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView View = sender as GridView;

            bool isValidRow = (View.FocusedRowHandle > 0);

            if (_currentRow != null)
            {
                int row = View.FocusedRowHandle;

                if (row % 2 == 0)
                {
                    View.FocusedColumn.OptionsColumn.ReadOnly = true;
                    View.FocusedColumn.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    string colNm = View.FocusedColumn.FieldName;

                    //int value = ConvertUtil.ToInt32(_currentRow[colNm]);
                    int col = ConvertUtil.ToInt32(colNm.Substring(1, 1));

                    if (_isAllowEdit[row, col])
                    {
                        View.FocusedColumn.OptionsColumn.ReadOnly = false;
                        View.FocusedColumn.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        View.FocusedColumn.OptionsColumn.ReadOnly = true;
                        View.FocusedColumn.OptionsColumn.AllowEdit = false;
                    }
                }

            }
            else
            {
                View.FocusedColumn.OptionsColumn.ReadOnly = true;
                View.FocusedColumn.OptionsColumn.AllowEdit = false;
            }
        }
    }
}
