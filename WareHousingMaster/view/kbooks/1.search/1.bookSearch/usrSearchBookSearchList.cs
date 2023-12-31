﻿using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.bookDetail;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSearchBookSearchList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        JObject _jobj;

        int _viewType;
        int _processType;

        public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;


        public usrSearchBookSearchList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHOR1", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHOR2", typeof(string)));
            _dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("SPECIALNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("GROUPCD", typeof(string)));
            _dt.Columns.Add(new DataColumn("GROUP_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("STANDCD", typeof(string)));
            _dt.Columns.Add(new DataColumn("STAND_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("RETURN_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("DELIVERY_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK", typeof(int)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _bs = new BindingSource();

            _viewType = 1;
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

        public void setGreidView(int viewType = (int)view.common.Enum.SearchListType.SEARCH)
        {
            _viewType = viewType;

            if (viewType == (int)view.common.Enum.SearchListType.PURCHASE || viewType == (int)view.common.Enum.SearchListType.PUBLISHER)
            {
                //gcPublisher.Visible = false;
                gcType.Visible = false;
                gcDeliveryCnt.Visible = false;
            }

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
            if (e.RowHandle >= 0)
            {
                GridView View = sender as GridView;
                //if (_viewType > (int)view.common.Enum.NRleaseViewType.RECEIPT && _viewType < (int)view.common.Enum.NRleaseViewType.ESTIMATE)
                //{
                //    if (e.Column.FieldName == "PRODUCT_TYPE")
                //    {
                //        int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["STATE"]));

                //        if (state == 2)
                //            e.Appearance.BackColor = Color.FromArgb(150, Color.DarkOrange);
                //    }
                //}
                //else
                //{
                //    if (e.Column.FieldName == "PRODUCT_TYPE")
                //    {
                //        int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["STATE"]));

                //        if (state == 2)
                //            e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                //    }
                //    else if (e.Column.FieldName == "RECEIPT_CNT")
                //    {
                //        int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["CNT_STATE"]));

                //        if (state == 2)
                //            e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                //    }
                //} 

               
            }
        }

        public void getList(JObject jobj)
        {
            _jobj = jobj;

            JObject jResult = new JObject();
            string url = "/search/getSearchBookList.json";

            if (_viewType == (int)view.common.Enum.SearchListType.PURCHASE)
                url = "/search/getSearchBookList4Purchase.json";

            gvList.BeginDataUpdate();
            _dt.Clear();
            gvList.EndDataUpdate();

            if (jobj != null)
            {
                Dangol.ShowSplash();

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                        int index = 1;
                        int cnt;

                        gvList.BeginDataUpdate();

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            DataRow dr = _dt.NewRow();

                            dr["NO"] = index++;
                            dr["ID"] = ConvertUtil.ToInt64(obj["ID"]);
                            dr["SHOPCD"] = ConvertUtil.ToInt32(obj["SHOPCD"]);
                            dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                            dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                            dr["AUTHOR1"] = ConvertUtil.ToString(obj["AUTHOR1"]);
                            dr["AUTHOR2"] = ConvertUtil.ToString(obj["AUTHOR2"]);
                            dr["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
                            dr["SPECIALNM"] = ConvertUtil.ToString(obj["SPECIALNM"]);
                            dr["GROUPCD"] = ConvertUtil.ToString(obj["GROUPCD"]);
                            dr["GROUP_NM"] = ConvertUtil.ToString(obj["GROUP_NM"]);
                            dr["STANDCD"] = ConvertUtil.ToString(obj["STANDCD"]);
                            dr["STAND_NM"] = ConvertUtil.ToString(obj["STAND_NM"]);

                            dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

                            cnt = ConvertUtil.ToInt32(obj["RETURN_CNT"]);
                            if (cnt != 0) dr["RETURN_CNT"] = cnt;
                            else dr["RETURN_CNT"] = DBNull.Value;

                            cnt = ConvertUtil.ToInt32(obj["DELIVERY_CNT"]);
                            if (cnt != 0) dr["DELIVERY_CNT"] = cnt;
                            else dr["DELIVERY_CNT"] = DBNull.Value;

                            cnt = ConvertUtil.ToInt32(obj["STOCK"]);
                            if (cnt != 0) dr["STOCK"] = cnt;
                            else dr["STOCK"] = DBNull.Value;

                            dr["STATE"] = 1;
                            dr["CHECK"] = false;
                            _dt.Rows.Add(dr);
                        }

                        gvList.EndDataUpdate();
                    }
                    
                    Dangol.CloseSplash();

                    if(_dt.Rows.Count < 1)
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
            //    
            //}
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
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
                    //this.ActiveControl = this;
                    //gvList.Focus();
                }
            }
        }

        public void clearGridView()
        {
            gvList.BeginDataUpdate();
            _dt.Clear();
            gvList.EndDataUpdate();
        }
        public bool editingCheck()
        {
            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("STATE = 2");  //shlee

            return rows.Length > 0;
        }

        public void sortList(string col)
        {
            gvList.ClearSorting();
            gvList.Columns[col].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
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

        public void setFocus()
        {
            gvList.Focus();
        }

        public void viewRefresh()
        {
            gvList.RefreshData();
        }

        public DataRow[] getCheckedList()
        {
            if(_dt.Rows.Count > 0)
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");

                if (rows.Length > 0)
                    return rows;
                else
                {
                    if(_currentRow == null)
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
                    //this.ActiveControl = this;
                    //gvList.Focus();
                }
            }   
        }

        public bool saveReleaseReceiptItem()
        {
            bool isSuccess = false;

            //int rowhandle = gvList.FocusedRowHandle;
            //gvList.FocusedRowHandle = -2147483646;
            //gvList.FocusedRowHandle = rowhandle;

            ////DataRow[] rows = _dt.Select("CHECK = TRUE"); //shlee
            ////if (rows.Length < 1)
            ////{
            ////    Dangol.Message("선택된 아이템이 없습니다.");
            ////    return isSuccess;
            ////}

            //DataRow[] rows = _dt.Select("CHECK = TRUE AND STATE = 2");  //shlee
            //if (rows.Length < 1)
            //{
            //    Dangol.Message("수정 가능한 아이템이 없습니다.");
            //    return isSuccess;
            //}

            //if (Dangol.MessageYN("선택한 아이템 수정하시겠습니까?") == DialogResult.Yes)
            //{
            //    JObject jResult = new JObject();
            //    JObject jobj = new JObject();
            //    string url = "/Nrelease/updateReleaseItemReceipt.json";

            //    Dangol.ShowSplash();

            //    var jArrayProduct = new JArray();

            //    foreach (DataRow row in rows)
            //    {
            //        JObject jdata = new JObject();
            //        jdata.Add("ITEM_ID", ConvertUtil.ToInt64(row["ITEM_ID"]));
            //        jdata.Add("USED_YN", ConvertUtil.ToInt32(row["USED_YN"]));
            //        jdata.Add("RECEIPT_CNT", ConvertUtil.ToInt32(row["RECEIPT_CNT"]));
            //        jdata.Add("DES", ConvertUtil.ToString(row["DES"]));

            //        jdata.Add("TABLE_NM", _tableNm);

            //        jArrayProduct.Add(jdata);
            //    }

            //    jobj.Add("DATA", jArrayProduct);

            //    if (DBConnect.getRequest(jobj, ref jResult, url))
            //    {
            //        isSuccess = true;

            //        gvList.BeginDataUpdate();
            //        foreach (DataRow row in rows)
            //            row["STATE"] = 1;

            //        gvList.EndDataUpdate();
            //        Dangol.CloseSplash();
            //        Dangol.Message("처리되었습니다");
            //    }
            //    else
            //    {
            //        Dangol.CloseSplash();
            //        Dangol.Error(jResult["MSG"]);
            //    }
            //}

            return isSuccess;
        }

        public void createReleaseReceiptItemt(long releaseId)
        {
            //using (DlgCreateCandidateItem createCandidateItem = new DlgCreateCandidateItem(releaseId))
            //{
            //    if (createCandidateItem.ShowDialog(this) == DialogResult.OK)
            //    {
            //        Dangol.ShowSplash();

            //        gvList.BeginDataUpdate();
            //        DataRow dr = _dt.NewRow();

            //        dr["NO"] = 0;

            //        dr["ITEM_ID"] = ConvertUtil.ToInt64(createCandidateItem._jobject["ITEM_ID"]);
            //        dr["MODEL_ID"] = ConvertUtil.ToInt64(createCandidateItem._jobject["MODEL_ID"]);

            //        dr["COMPONENT_CD"] = ConvertUtil.ToString(createCandidateItem._jobject["COMPONENT_CD"]);
            //        dr["USED_YN"] = ConvertUtil.ToInt32(createCandidateItem._jobject["USED_YN"]);
            //        dr["MODEL_NM"] = ConvertUtil.ToString(createCandidateItem._jobject["MODEL_NM"]);

            //        //dr["CPU_MODEL_ID"] = ConvertUtil.ToInt64(createCandidateItem._jobject["CPU_MODEL_ID"]);
            //        //dr["CPU"] = ConvertUtil.ToString(createCandidateItem._jobject["CPU"]);
            //        //dr["MEM"] = ConvertUtil.ToInt32(createCandidateItem._jobject["MEM"]);
            //        //dr["CPU_DETAIL"] = ConvertUtil.ToInt32(createCandidateItem._jobject["CPU_DETAIL"]);
            //        //dr["STG"] = ConvertUtil.ToInt32(createCandidateItem._jobject["STG"]);

            //        dr["RECEIPT_CNT"] = ConvertUtil.ToInt32(createCandidateItem._jobject["RECEIPT_CNT"]);
            //        dr["DES"] = ConvertUtil.ToString(createCandidateItem._jobject["DES"]);

            //        dr["STATE"] = 1;
            //        dr["CHECK"] = false;
            //        _dt.Rows.Add(dr);

            //        Common.setGridViewNo(gvList);

            //        gvList.EndDataUpdate();

            //        Dangol.CloseSplash();

            //        Dangol.Message("추가되었습니다.");
            //    }
            //}
        }

        public bool DeleteReleaseReceiptItem()
        {
            bool isSuccess = false;

            //int rowhandle = gvList.FocusedRowHandle;
            //gvList.FocusedRowHandle = -2147483646;
            //gvList.FocusedRowHandle = rowhandle;

            //DataRow[] rows = _dt.Select("CHECK = TRUE"); //shlee
            //if (rows.Length < 1)
            //{
            //    Dangol.Message("선택된 아이템이 없습니다.");
            //}
            //else
            //{
            //    if (Dangol.MessageYN("선택한 아이템을 삭제하시겠습니까?") == DialogResult.Yes)
            //    {
            //        JObject jResult = new JObject();
            //        JObject jobj = new JObject();
            //        string url = "/Nrelease/deleteReleaseReceiptItem.json";

            //        var jArrayProduct = new JArray();
            //        List<long> listItemId = new List<long>();
            //        foreach (DataRow row in rows)
            //            listItemId.Add(ConvertUtil.ToInt64(row["ITEM_ID"]));

            //        //jobj.Add("PRODUCT_YN", 1);
            //        jobj.Add("LIST_ITEM_ID", string.Join(",", listItemId));
            //        //jobj.Add(_representativeIdCol, _representativeId);
            //        //jobj.Add("REPRESENTATIVE_ID_COL", _representativeIdCol);
            //        //jobj.Add("REPRESENTATIVE_ID", _representativeId);
            //        //jobj.Add("PROCESS_TYPE", _processType);
            //        //jobj.Add("TABLE_NM", _tableNm);

            //        Dangol.ShowSplash();

            //        if (DBConnect.getRequest(jobj, ref jResult, url))
            //        {
            //            isSuccess = true;
            //            DBNRelease.wirteUpdateLog(_representativeId, 0, "접수 제품 정보 삭제");

            //            gvList.BeginDataUpdate();

            //            foreach (DataRow row in rows)
            //                row.Delete();

            //            Common.setGridViewNo(gvList);

            //            gvList.EndDataUpdate();
            //            Dangol.CloseSplash();
            //            Dangol.Message("처리되었습니다.");
            //        }
            //        else
            //        {
            //            Dangol.CloseSplash();
            //            Dangol.Error(jResult["MSG"]);
            //        }
            //    }
            //}

            return isSuccess;
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

        private void gcList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                e.Handled = true;
            }
        }

        private void gvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showBookInfoDetail();
            }
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
