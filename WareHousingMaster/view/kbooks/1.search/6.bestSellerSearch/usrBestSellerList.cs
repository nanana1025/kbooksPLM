using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.bookDetail;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrBestSellerList : DevExpress.XtraEditors.XtraUserControl
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

        public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;


        public usrBestSellerList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHOR1", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHOR2", typeof(string)));
            _dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("DEPTCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("DEPT_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("GROUPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("GROUP_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("STANDCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("STAND_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK", typeof(int)));
            _dt.Columns.Add(new DataColumn("SUM_SALE_COUNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("SUM_SALE_PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("ETC", typeof(string)));
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
            //Util.LookupEditHelper(rileComponentCd, InventoryInfo._dtInventoryCd, "KEY", "VALUE");

            //DataTable dtUsedYn = Util.getCodeList_Int_String("CD0107", "KEY", "VALUE");
            //Util.LookupEditHelper(rileUsedYn, dtUsedYn, "KEY", "VALUE");

        }

        public void setinitialize()
        {
            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;
        }

        public void setGridEditable(bool isEditable)
        {
            
        }

        public void setProcessType(int processType = 1)
        {
            _processType = processType;

            //if (processType == 1)
            //{
            //    _representativeIdCol = "ESTIMATE_ID";
            //    _tableNm = "TN_N_ESTIMATE_PRODUCT";
            //}
        }

        public void setGreidView(int viewType = 2)
        {
            _viewType = viewType;

            //if (viewType == (int)view.common.Enum.NRleaseViewType.ESTIMATE)
            //{
            //    ////gcCandidiateCnt.Visible = false;
            //    //gcCpu.Visible = false;
            //    //gcStg.Visible = false;
            //    ////gcInitPrice.Visible = false;
            //    //gcMarginCost.Visible = false;
            //    //gcSalePrice.Visible = false;
            //    //gcReleaseCnt.Caption = "견적개수";
            //    //gcPrice.Caption = "견적가";
            //    //gcReleasePrice.Caption = "총견적가";
            //    //gcReleasePrice.OptionsColumn.ReadOnly = true;
            //}
           
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

            focusedRowObjectChangeHandler(_currentRow);
        }
        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        public void getList(JObject jobj)
        {
            _jobj = jobj;

            JObject jResult = new JObject();
            string url = "/search/getBestSellerBooksList.json";

            gvList.BeginDataUpdate();
            _dt.Clear();
            gvList.EndDataUpdate();

            if (jobj != null)
            {
                //gcList.BeginUpdate();
                //gcCode.Caption = $"{jobj["SELECTED_TYPE"]} 코드";
                //gcCodeNm.Caption = $"{jobj["SELECTED_TYPE"]} 코드명";
                //gcList.EndUpdate();

                Dangol.ShowSplash();

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                        int index = 1;

                        gvList.BeginDataUpdate();

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            DataRow dr = _dt.NewRow();

                            dr["NO"] = index++;
                            dr["SHOPCD"] = ConvertUtil.ToInt32(obj["SHOPCD"]);
                            dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);

                            dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                            dr["AUTHOR1"] = ConvertUtil.ToString(obj["AUTHOR1"]);
                            dr["AUTHOR2"] = ConvertUtil.ToString(obj["AUTHOR2"]);
                            dr["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);

                            dr["DEPTCD"] = ConvertUtil.ToInt32(obj["DEPTCD"]);
                            dr["DEPT_NM"] = ConvertUtil.ToString(obj["DEPT_NM"]);

                            dr["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
                            dr["GROUP_NM"] = ConvertUtil.ToString(obj["GROUP_NM"]);

                            dr["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);
                            dr["STAND_NM"] = ConvertUtil.ToString(obj["STAND_NM"]);

                            dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);
                            dr["STOCK"] = ConvertUtil.ToInt32(obj["STOCK"]);

                            dr["SUM_SALE_COUNT"] = ConvertUtil.ToInt32(obj["SUM_SALE_COUNT"]);
                            dr["SUM_SALE_PRICE"] = ConvertUtil.ToInt32(obj["SUM_SALE_PRICE"]);

                            dr["ETC"] = "";

                            dr["STATE"] = 1;
                            dr["CHECK"] = false;
                            _dt.Rows.Add(dr);
                        }

                        gvList.EndDataUpdate();
                    }

                    Dangol.CloseSplash();

                    if (_dt.Rows.Count < 1)
                        Dangol.Info("검색 결과가 없습니다.");
                }
                else
                {
                    Dangol.CloseSplash();
                    Dangol.Error(jResult["MSG"]);
                }
            }
            //else
            //{

            //}
        }

        public bool editingCheck()
        {
            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("STATE = 2");  //shlee

            return rows.Length > 0;
        }

        public DataRow[] getCheckedList()
        {
            if (_dt.Rows.Count > 0)
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");

                if (rows.Length > 0)
                    return rows;
                else
                {
                    if (_currentRow == null)
                        return null;
                    else
                    {
                        _currentRow.BeginEdit();
                        _currentRow["CHECK"] = true;
                        _currentRow.EndEdit();

                        return _dt.Select("CHECK = TRUE");
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public void clear()
        {
            gvList.BeginDataUpdate();
            _dt.Clear();
            gvList.EndDataUpdate();
        }

        public void setFocus()
        {
            gvList.Focus();
        }


        public DataTable getTable()
        {
            return _dt;
        }

        public void receiptRefresh()
        {
            gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            getList(_jobj);
            gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
        }

        public void viewRefresh()
        {
            gvList.RefreshData();
        }

        public void showBookInfoDetail()
        {
            if (_currentRow == null)
            {
                Dangol.Warining("선택된 도서정보가 없습니다.");
            }
            else
            {
                using (usrBookDetail bookDetail = new usrBookDetail(ConvertUtil.ToInt64(_currentRow["BOOKCD"]), ConvertUtil.ToInt32(_currentRow["SHOPCD"]), false))
                {
                    //bookDetail.StartPosition = FormStartPosition.Manual;
                    //bookDetail.Location = new Point(this.Location.X + (this.Size.Width / 2) - (bookDetail.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (bookDetail.Size.Height / 2));

                    if (bookDetail.ShowDialog(this) == DialogResult.OK)
                    {

                    }
                }
            }
        }

        public void exportFile()
        {
            Common.exportFile(gvList);
        }

        public void gvList_CustomButtonChecked()
        {
            Common.gridViewButtonChecked(gvList, _dt);
        }

        public void gvList_CustomButtonUnchecked()
        {
            Common.gridViewButtonUnchecked(gvList, _dt);
        }

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        public void SetColFocus(string col, int rowHandle = 0)
        {
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
    }
}
