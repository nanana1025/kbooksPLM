using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.order.bookOrder
{
    public partial class usrPreOrderBookList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        JObject _jobj;

        long _representativeId;
        string _representativeIdCol;
        string _representativeStateCol;
        string _tableNm;
        int _representativeState;

        bool _isUpdate;

        int _viewType;
        int _processType;

        //public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        //public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;

        //public delegate void SelectHandler();
        //public event SelectHandler selectHandler;


        public usrPreOrderBookList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _bs = new BindingSource();

        }



        public void setInitLoad()
        {
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
        }

      
        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
            }
            else
            {
                _currentRow = null;
            }

            //focusedRowObjectChangeHandler(_currentRow);
        }
        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView View = sender as GridView;               
            }
        }

        public void setData(DataRow[] rows)
        {
            gvList.BeginDataUpdate();
            _dt.Clear();
            int index = 1;
            foreach (DataRow obj in rows)
            {
                DataRow dr = _dt.NewRow();

                dr["NO"] = index++;
                dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                if (obj.Table.Columns.Contains("PURCHCD"))
                    dr["PURCHCD"] = ConvertUtil.ToInt32(obj["PURCHCD"]);
                else
                    dr["PURCHCD"] = -1;

                _dt.Rows.Add(dr);
            }
            gvList.EndDataUpdate();
               
        }

        

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_currentRow != null)
            {
                if (e.Column.FieldName != "CHECK")
                {
                    int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                    if (state == 1)
                        _currentRow["STATE"] = 2;
                }
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

        public DataTable getDataTable()
        {
            return _dt;
        }

        public void clear()
        {
            gvList.BeginDataUpdate();
            _dt.Clear();
            gvList.EndDataUpdate();
        }
    }
}
