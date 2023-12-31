﻿using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrOrderBookModifyList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        JObject _jobj;

        GridColumn[] arrGcCol;

        long _representativeId;
        string _representativeIdCol;
        string _representativeStateCol;
        string _tableNm;
        int _representativeState;

        bool _isUpdate;

        int _viewType;
        int _processType;



        int _shopCd;
        int _searchType;
        int _storeCd;
        int _storeCdE;
        int _storeCdS;
        int _groupCategory;
        int _groupType; // 조 구분
        
        int _rate;
        int _groupCd;

        int _purchType;
        int _purchCd;
        string _purchNm;

        
        int _orderType;
        int _orderCondition;
        string _conditionString;
        int _tradeItem;
        string _orderDt;
        string _inpDt;
        int _start, _end;



        public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;


        public usrOrderBookModifyList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));

            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("PUBSHCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("INP_GROUPCD", typeof(int)));
            //_dt.Columns.Add(new DataColumn("ORD_GROUPCD", typeof(int)));
            //_dt.Columns.Add(new DataColumn("GROUPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("STANDCD", typeof(int)));


            _dt.Columns.Add(new DataColumn("PURCHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("STOCK", typeof(int)));
            _dt.Columns.Add(new DataColumn("TOTAL", typeof(int)));

            for (int i = Order.STORES; i <= Order.STOREE; i++)
            {
                _dt.Columns.Add(new DataColumn($"STORE{i}", typeof(int)));
                _dt.Columns.Add(new DataColumn($"STORE{i}_O", typeof(int)));
            }

            //_dt.Columns.Add(new DataColumn("STORE1", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE2", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE3", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE4", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE5", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE6", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE7", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE8", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE9", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE10", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE11", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE12", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE13", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE14", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE15", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE99", typeof(int)));

            //_dt.Columns.Add(new DataColumn("STORE1_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE2_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE3_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE4_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE5_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE6_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE7_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE8_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE9_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE10_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE11_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE12_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE13_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE14_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE15_O", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STORE99_O", typeof(int)));

            _dt.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));

            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            arrGcCol = new GridColumn[] { gcStore1, gcStore2, gcStore3, gcStore4, gcStore5, gcStore6, gcStore7, gcStore8, gcStore9, gcStore10, gcStore11, gcStore12, gcStore13, gcStore14, gcStore15/*, gcStore99*/ };

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

        public void setCondition(JObject obj)
        {
            _shopCd = ConvertUtil.ToInt32(obj["SHOPCD"]);
            _searchType = ConvertUtil.ToInt32(obj["SEARCH_TYPE"]);
            if (obj.ContainsKey("STORECD"))
                _storeCd = ConvertUtil.ToInt32(obj["STORECD"]);
            else
                _storeCd = -1;

            if (obj.ContainsKey("STORECD_S"))
                _storeCdS = ConvertUtil.ToInt32(obj["STORECD_S"]);
            else
                _storeCdS = -1;

            if (obj.ContainsKey("STORECD_E"))
                _storeCdE = ConvertUtil.ToInt32(obj["STORECD_E"]);
            else
                _storeCdE = -1;

            
            _groupCategory = ConvertUtil.ToInt32(obj["GROUP_CATEGORY"]);
            _groupType = ConvertUtil.ToInt32(obj["GROUP_TYPE"]);


            if (obj.ContainsKey("RATECD"))
                _rate = ConvertUtil.ToInt32(obj["RATECD"]);
            else
                _rate = -1;

            if (obj.ContainsKey("GROUPCD"))
                _groupCd = ConvertUtil.ToInt32(obj["GROUPCD"]);
            else
                _groupCd = -1;
            
            _purchType = ConvertUtil.ToInt32(obj["PURCHCD_TYPE"]);
            if (obj.ContainsKey("PURCHCD"))
                _purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);
            else
                _purchCd = -1;

            _tradeItem = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
            _orderDt = ConvertUtil.ToString(obj["DT_ORDER"]);

            if (_searchType == 1)
            {
                _start = 1;
                _end = 15;
            }
            else
            {
                if (_storeCd > 0)
                    _start = _end = _storeCd;
                else
                {
                    _start = _storeCdS;
                    _end = _storeCdE;
                }
            }
        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                //string column = gvList.FocusedColumn.FieldName;
                //if (column.Contains("STORE"))
                //{
                //    column = column + "_O";
                //    int cnt = ConvertUtil.ToInt32(_currentRow[column]);

                //    gvList.FocusedColumn.OptionsColumn.ReadOnly = (cnt == 0);
                //}
            }
            else
            {
                _currentRow = null;
            }

            //focusedRowObjectChangeHandler(_currentRow);
        }

        private void gvList_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (_currentRow != null)
            {
                string column = gvList.FocusedColumn.FieldName;
                if (column.Contains("STORE"))
                {
                    column = column + "_O";
                    int cnt = ConvertUtil.ToInt32(_currentRow[column]);

                    //gvList.FocusedColumn.OptionsColumn.ReadOnly = (cnt == 0);
                }
            }
            else
            {
                //gvList.FocusedColumn.OptionsColumn.ReadOnly = true;
            }
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
            string url = "/order/getOrderDataList.json";

            gcList.BeginUpdate();
            gvList.BeginDataUpdate();
            for (int i = 0; i < arrGcCol.Length; i++)
            {
                arrGcCol[i].Visible = false;
                arrGcCol[i].VisibleIndex = -1;
            }
            gcEtc.Visible = false;

            int colType = 0;
            int cd = 1;
            int start = 1;
            int end = 1;
            int visibleIndex = 7;

            if (jobj.ContainsKey("STORE_TYPE"))
            {
                if(ConvertUtil.ToString(jobj["STORE_TYPE"]).Equals("SINGLE"))
                {
                    colType = 1;

                    cd = ConvertUtil.ToInt32(jobj["STORECD"]);
                    if (cd != 99)
                    {
                        arrGcCol[cd - 1].Visible = true;
                        arrGcCol[cd - 1].VisibleIndex = visibleIndex++;
                    }
                    else
                    {
                        arrGcCol[arrGcCol.Length - 1].Visible = true;
                        arrGcCol[arrGcCol.Length - 1].VisibleIndex = visibleIndex++;
                    }

                }
                else
                {
                    colType = 2;

                    start = ConvertUtil.ToInt32(jobj["STORECD_S"]);
                    end = ConvertUtil.ToInt32(jobj["STORECD_E"]);

                    int modifEnd = end;

                    if (end == 99)
                        modifEnd = 15;

                    for (int i = start-1; i < modifEnd; i++)
                    {
                        arrGcCol[i].Visible = true;
                        arrGcCol[i].VisibleIndex = visibleIndex++;
                    }

                    if (end == 99)
                    {
                        //arrGcCol[arrGcCol.Length - 1].Visible = true;
                        //arrGcCol[arrGcCol.Length - 1].VisibleIndex = visibleIndex++;
                    }

                }
            }
            else
            {
                colType = 3;

                for (int i = 0; i < arrGcCol.Length; i++)
                {
                    arrGcCol[i].Visible = true;
                    arrGcCol[i].VisibleIndex = visibleIndex++;
                }

                //arrGcCol[arrGcCol.Length - 1].Visible = true;
                //arrGcCol[arrGcCol.Length - 1].VisibleIndex = visibleIndex++;
            }

            gcEtc.Visible = true;
            gcEtc.VisibleIndex = ++visibleIndex;

            

           
            _dt.Clear();
            gvList.EndDataUpdate();
            gcList.EndUpdate();

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray;
                    jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    int total;

                    int modifEnd = end;

                    if (end == 99)
                        modifEnd = 15;

                    gvList.BeginDataUpdate();

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        dr["NO"] = index++;
                        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                        dr["PURCHCD"] = ConvertUtil.ToInt64(obj["PURCHCD"]);
                        dr["PUBSHCD"] = ConvertUtil.ToInt64(obj["PUBSHCD"]);
                        dr["INP_GROUPCD"] = ConvertUtil.ToInt32(obj["INP_GROUPCD"]);
                        dr["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);

                        dr["PURCHNM"] = ConvertUtil.ToString(obj["PURCHNM"]);
                        dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                        dr["STOCK"] = ConvertUtil.ToInt32(obj["STOCK"]);

                        total = 0;
  
                        if (colType == 1)
                        {
                            total = ConvertUtil.ToInt32(obj[$"STORE{cd}"]);
                            dr[$"STORE{cd}"] = ConvertUtil.ToInt32(obj[$"STORE{cd}"]);
                            dr[$"STORE{cd}_O"] = ConvertUtil.ToInt32(obj[$"STORE{cd}"]);
                        }
                        else if (colType == 2)
                        {
                           
                            for (int i = start; i <= modifEnd; i++)
                            {
                                total += ConvertUtil.ToInt32(obj[$"STORE{i}"]);
                                dr[$"STORE{i}"] = ConvertUtil.ToInt32(obj[$"STORE{i}"]);
                                dr[$"STORE{i}_O"] = ConvertUtil.ToInt32(obj[$"STORE{i}"]);
                            }

                            if (end == 99)
                            {
                                //total += ConvertUtil.ToInt32(obj[$"STORE99"]);
                                //dr[$"STORE99"] = ConvertUtil.ToInt32(obj[$"STORE99"]);
                                //dr[$"STORE99_O"] = ConvertUtil.ToInt32(obj[$"STORE99"]);
                            }
                        }
                        else if(colType == 3)
                        {
                            for (int i = 0; i < arrGcCol.Length; i++)
                            {
                                total += ConvertUtil.ToInt32(obj[$"STORE{i+1}"]);
                                dr[$"STORE{i + 1}"] = ConvertUtil.ToInt32(obj[$"STORE{i + 1}"]);
                                dr[$"STORE{i + 1}_O"] = ConvertUtil.ToInt32(obj[$"STORE{i + 1}"]);
                            }

                            //total += ConvertUtil.ToInt32(obj[$"STORE99"]);
                            //dr[$"STORE99"] = ConvertUtil.ToInt32(obj[$"STORE99"]);
                            //dr[$"STORE99_O"] = ConvertUtil.ToInt32(obj[$"STORE99"]);

                        }

                        dr["TOTAL"] = total;
                        dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);
                        dr["STATE"] = 1;
                        dr["CHECK"] = false;
                        _dt.Rows.Add(dr);
                    }

                    gvList.EndDataUpdate();

                    if (_dt.Rows.Count < 1)
                        Dangol.Info("검색 결과가 없습니다.");
                }
                else
                {
                    Dangol.Info("검색 결과가 없습니다.");
                }
            }
            else
            {
                gvList.EndDataUpdate();
                Dangol.Error(jResult["MSG"]);
            }
        }

        public void getPerformance()
        {
            if (_currentRow == null)
                Dangol.Warining("선택된 데이터가 없습니다.");
        }

        public void deleteRow()
        {
            if (_currentRow == null)
                Dangol.Warining("선택된 데이터가 없습니다.");
            else
            {
                if (Dangol.MessageYN("선택한 주문을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    string url = "/order/deleteOrderBookInfo.json";


                    jobj.Add("INP_SHOPCD", _shopCd);
                    if (_searchType == 2)
                    {
                        if (_storeCd > 0)
                            jobj.Add("STORECD", _storeCd);
                        else
                        {
                            jobj.Add("STORECD_S", _storeCdS);
                            jobj.Add("STORECD_E", _storeCdE);
                        }

                        if (_groupCategory == 1)
                        {
                            jobj.Add("INP_GROUPCD", ConvertUtil.ToInt32(_currentRow["INP_GROUPCD"]));
                        }
                        else
                        {
                            if (_groupType == 1)
                                jobj.Add("INP_GROUPCD", 99);
                            else
                                jobj.Add("INP_GROUPCD", _groupCd);
                        }
                    }
                    else
                    {
                        jobj.Add("INP_GROUPCD", ConvertUtil.ToInt32(_currentRow["INP_GROUPCD"]));
                    }

                    if (_purchType == 2)
                        jobj.Add("PURCHCD", _purchCd);
                    else
                        jobj.Add("PURCHCD", ConvertUtil.ToInt32(_currentRow["PURCHCD"]));

                    jobj.Add("PUBSHCD", ConvertUtil.ToInt32(_currentRow["PUBSHCD"]));
                    jobj.Add("DT_ORDER", _orderDt);
                    jobj.Add("TRADE_ITEM", _tradeItem);
                    jobj.Add("BOOKCD", ConvertUtil.ToInt64(_currentRow["BOOKCD"]));

                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        gvList.BeginDataUpdate();
                        _currentRow.Delete();
                        gvList.EndDataUpdate();
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
            }
        }

        public void confirm()
        {
            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            if (_dt.Rows.Count < 1)
                Dangol.Warining("확정할 데이터가 없습니다.");
            else
            {
                DialogResult result = Dangol.Custom("주문 데이터를 확정하시겠습니까?");

                if (result == DialogResult.Yes) //confirm
                {
                    JObject jResult = new JObject();
                    if(save(ref jResult))
                    {
                        if(confirmList(ref jResult))
                        {
                            if (delete(ref jResult))
                                Dangol.Info("주문데이터가 확정되었습니다.");
                            else
                                Dangol.Error(jResult["MSG"]);
                        }
                        else
                        {
                            if (ConvertUtil.ToBoolean(jResult["REFRESH"]))
                            {
                                receiptRefresh();
                                Dangol.Warining(jResult["MSG"]);
                            }
                            else
                                Dangol.Error(jResult["MSG"]);
                        }
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
                else if (result == DialogResult.OK) //save
                {
                    JObject jResult = new JObject();
                    if (save(ref jResult))
                    {
                        Dangol.Info("주문데이터를 저장하였습니다.");
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
            }
        }

        public bool save(ref JObject jResult)
        { 
            JObject jobj = new JObject();
            var jArray = new JArray();

            string url = "/order/updateOrderBookInfo.json";

            int cnt;
            int cnt_O;

            int start, end;

            if (_searchType == 1)
            {
                start = 1;
                end = 15;
            }
            else
            {
                if (_storeCd > 0)
                    start = end = _storeCd;
                else
                {
                    start = _storeCdS;
                    end = _storeCdE;
                }
            }

            DataRow[] rows = _dt.Select("STATE = 2");

            foreach (DataRow row in rows)
            {
                for (int i = start; i <= end; i++)
                {
                    cnt = ConvertUtil.ToInt32(row[$"STORE{i}"]);
                    cnt_O = ConvertUtil.ToInt32(row[$"STORE{i}_O"]);

                    if (cnt != cnt_O)
                    {
                        JObject jdata = new JObject();

                        jdata.Add("INP_SHOPCD", _shopCd);
                        jdata.Add("STORECD", i);
                        jdata.Add("ORD_COUNT", cnt);

                        if (_groupCategory == 1)
                        {
                            jdata.Add("INP_GROUPCD", ConvertUtil.ToInt32(row["INP_GROUPCD"]));
                        }
                        else
                        {
                            if (_groupType == 1)
                                jdata.Add("INP_GROUPCD", 99);
                            else
                                jdata.Add("INP_GROUPCD", _groupCd);
                        }

                        if (_purchType == 2)
                            jdata.Add("PURCHCD", _purchCd);
                        else
                            jdata.Add("PURCHCD", ConvertUtil.ToInt32(row["PURCHCD"]));

                        jdata.Add("PUBSHCD", ConvertUtil.ToInt32(row["PUBSHCD"]));
                        jdata.Add("ORD_DATE", _orderDt);
                        jdata.Add("TRADE_ITEM", _tradeItem);
                        jdata.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));
                        //jdata.Add("BOOKNM", ConvertUtil.ToString(row["BOOKNM"]));
                        //jdata.Add("PURCHCD", ConvertUtil.ToInt64(row["PURCHCD"]));
                        //jdata.Add("ORD_GROUPCD", ConvertUtil.ToInt32(row["ORD_GROUPCD"]));
                        //jdata.Add("GROUPCD", ConvertUtil.ToInt32(row["GROUPCD"]));
                        //jdata.Add("STANDCD", ConvertUtil.ToInt32(row["STANDCD"]));

                        jArray.Add(jdata);
                    }
                }
            }

            if (jArray.Count > 0)
            {
                jobj.Add("DATA", jArray);

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    gvList.BeginDataUpdate();
                    foreach (DataRow row in rows)
                        for (int i = start; i <= end; i++)
                        {
                            row[$"STORE{i}_O"] = ConvertUtil.ToInt32(row[$"STORE{i}"]);
                            row[$"STATE"] = 1;
                        }
                    gvList.EndDataUpdate();
                    return true;
                }
                else
                {
                    //Dangol.Error(jResult["MSG"]);
                    return false;
                }
            }
            return true;
        }


        public bool confirmList(ref JObject jResult)
        {
            JObject jobj = new JObject();

            string url = "/order/confirmOrderBookList.json";
     
            jobj.Add("INP_SHOPCD", _shopCd);

            if (_searchType == 2)
            {
                if (_storeCd > 0)
                    jobj.Add("STORECD", _storeCd);
                else
                {
                    jobj.Add("STORECD_S", _storeCdS);
                    jobj.Add("STORECD_E", _storeCdE);
                }
            }

            if (_groupCategory == 1)// 과
            {
                if (_groupType == 2) //개별
                    jobj.Add("RATE", _groupCd);
            }
            else //조
            {
                if (_groupType == 1) //전체
                    jobj.Add("INP_GROUPCD", 99);
                else
                    jobj.Add("INP_GROUPCD", _groupCd);
            }

            if (_purchType == 2)
                jobj.Add("PURCHCD", _purchCd);

            jobj.Add("ORD_DATE", _orderDt);
            jobj.Add("TRADE_ITEM", _tradeItem);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                //receiptRefresh();
                return true;
            }
            else
            {
                //Dangol.Error(jResult["MSG"]);
                return false;
            }
        }

        public bool delete(ref JObject jResult)
        {
            JObject jobj = new JObject();
            var jArray = new JArray();

            string url = "/order/clearOrderBook.json";

            foreach (DataRow row in _dt.Rows)
            {
                JObject jdata = new JObject();

                jdata.Add("INP_SHOPCD", _shopCd);
                if (_searchType == 2)
                {
                    if (_storeCd > 0)
                        jdata.Add("STORECD", _storeCd);
                    else
                    {
                        jdata.Add("STORECD_S", _storeCdS);
                        jdata.Add("STORECD_E", _storeCdE);
                    }

                    if (_groupCategory == 1)
                    {
                        jdata.Add("INP_GROUPCD", ConvertUtil.ToInt32(row["INP_GROUPCD"]));
                    }
                    else
                    {
                        if (_groupType == 1)
                            jdata.Add("INP_GROUPCD", 99);
                        else
                            jdata.Add("INP_GROUPCD", _groupCd);
                    }
                }
                else
                {
                    jdata.Add("INP_GROUPCD", ConvertUtil.ToInt32(row["INP_GROUPCD"]));
                }

                if (_purchType == 2)
                    jdata.Add("PURCHCD", _purchCd);
                else
                    jdata.Add("PURCHCD", ConvertUtil.ToInt32(row["PURCHCD"]));

                jdata.Add("PUBSHCD", ConvertUtil.ToInt32(row["PUBSHCD"]));
                jdata.Add("DT_ORDER", _orderDt);
                jdata.Add("TRADE_ITEM", _tradeItem);
                jdata.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));

                jArray.Add(jdata);
            }

            jobj.Add("DATA", jArray);
               
            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                gvList.BeginDataUpdate();
                _dt.Clear();
                gvList.EndDataUpdate();

                return true;
            }
            else
            {
                //Dangol.Error(jResult["MSG"]);

                return false;
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
            getList(_jobj);
            //gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
        }

        public void viewRefresh()
        {
            gvList.RefreshData();
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
                    if (e.Column.FieldName.Contains("STORE"))
                    {
                        int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                        if (state == 1)
                            _currentRow["STATE"] = 2;

                        int totalCnt = 0;
                        for(int i = _start; i <= _end; i++)
                            totalCnt += ConvertUtil.ToInt32(_currentRow[$"STORE{i}"]);

                        _currentRow["TOTAL"] = totalCnt;
                    }
                }
            }
        }

       

        private void riteTitle_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    TextEdit textEditor = (TextEdit)sender;

            //    string title = textEditor.Text;

            //    using (dlgBookSearch bookSearch = new dlgBookSearch(title))
            //    {
            //        bookSearch.StartPosition = FormStartPosition.Manual;
            //        bookSearch.Location = new Point(this.Location.X + (this.Size.Width / 2) - (bookSearch.Size.Width / 2),
            //        this.Location.Y + (this.Size.Height / 2) - (bookSearch.Size.Height / 2));

            //        if (bookSearch.ShowDialog(this) == DialogResult.OK)
            //        {
            //            //Dangol.ShowSplash();
            //            //usrReleaseItemList1.receiptRefresh();
            //            //Dangol.CloseSplash();
            //        }
            //    }
            //}
        }

        private void gcList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                e.Handled = true;
            }
        }

        public void setFocus()
        {
            gvList.Focus();
        }

        public void clear()
        {
            gvList.BeginDataUpdate();
            _dt.Clear();
            gvList.EndDataUpdate();
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
