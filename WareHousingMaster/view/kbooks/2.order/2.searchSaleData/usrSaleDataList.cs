using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSaleDataList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }

        DataTable _dtAuthor;
        DataTable _dtEsti;
        DataTable _dtRet;
        DataTable _dtOrd;
        DataTable _dtInp;
        DataTable _dtInpRatio;
        DataTable _dtOrderRatio;

        BindingSource _bsOrderRatio;

        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        JObject _jobj;

        long _representativeId;
        string _representativeIdCol;
        string _representativeStateCol;
        string _tableNm;
        int _representativeState;

        bool _isUpdate;

        int _shopCd;
        int _viewType;
        int _processType;

        Dictionary<string, DataTable> _dicOrderRatioTable;

        public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;


        public usrSaleDataList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            
            _dt.Columns.Add(new DataColumn("STORECD", typeof(int)));
            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("PURCHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHORNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("ESTI_SUM", typeof(int)));
            _dt.Columns.Add(new DataColumn("RET_SUM", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORD_SUM", typeof(int)));
            _dt.Columns.Add(new DataColumn("INP_SUM", typeof(int)));
            _dt.Columns.Add(new DataColumn("SALE_SUM", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORD_RATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORDER_CNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("DEPTCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("ORDER_CNT_YN", typeof(string)));
            _dt.Columns.Add(new DataColumn("GROUPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("STANDCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("TRADE_ITEM", typeof(long)));
            _dt.Columns.Add(new DataColumn("ORD_GROUPCD", typeof(int)));

            _dt.Columns.Add(new DataColumn("SHOPCD", typeof(string)));

            _dt.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));  //0:default, 1:create, 2:changed
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _dtAuthor = new DataTable();
            _dtAuthor.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dtAuthor.Columns.Add(new DataColumn("AUTHORNM", typeof(string)));

            _dtEsti = new DataTable();
            _dtEsti.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dtEsti.Columns.Add(new DataColumn("PURCHCD", typeof(long)));
            _dtEsti.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dtEsti.Columns.Add(new DataColumn("ESTI_SUM", typeof(int)));

            _dtRet = new DataTable();
            _dtRet.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dtRet.Columns.Add(new DataColumn("PURCHCD", typeof(long)));
            _dtRet.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dtRet.Columns.Add(new DataColumn("RET_SUM", typeof(int)));

            _dtOrd = new DataTable();
            _dtOrd.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dtOrd.Columns.Add(new DataColumn("STORECD", typeof(long)));
            _dtOrd.Columns.Add(new DataColumn("PURCHCD", typeof(long)));
            _dtOrd.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dtOrd.Columns.Add(new DataColumn("ORD_SUM", typeof(int)));
            _dtOrd.Columns.Add(new DataColumn("ORD_RATE", typeof(double)));
            _dtOrd.Columns.Add(new DataColumn("TRADE_ITEM", typeof(long)));

            _dtInp = new DataTable();
            _dtInp.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dtInp.Columns.Add(new DataColumn("STORECD", typeof(long)));
            _dtInp.Columns.Add(new DataColumn("PURCHCD", typeof(long)));
            _dtInp.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dtInp.Columns.Add(new DataColumn("INP_SUM", typeof(int)));

            _dtInpRatio = new DataTable();
            _dtInpRatio.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dtInpRatio.Columns.Add(new DataColumn("PURCHCD", typeof(long)));
            _dtInpRatio.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dtInpRatio.Columns.Add(new DataColumn("RATE", typeof(double)));


            _dtOrderRatio = new DataTable();

            _dtOrderRatio.Columns.Add(new DataColumn("CD", typeof(string)));
            _dtOrderRatio.Columns.Add(new DataColumn("KEY", typeof(int)));
            _dtOrderRatio.Columns.Add(new DataColumn("VALUE", typeof(string)));

            _shopCd = 1;
            _bs = new BindingSource();
            _bsOrderRatio = new BindingSource();

            _dicOrderRatioTable = new Dictionary<string, DataTable>();
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

            for(int i = 0; i <= 100; i+=5)
            {
                DataRow dr1 = _dtOrderRatio.NewRow();
                dr1["KEY"] = i;
                dr1["VALUE"] = ConvertUtil.ToString(i);
                _dtOrderRatio.Rows.Add(dr1);
            }

            Util.LookupEditHelper(rileOrderRatio, _dtOrderRatio, "KEY", "VALUE");

        }

        public void setinitialize()
        {
            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;

            //_bsOrderRatio.DataSource = _dtOrderRatio;
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

            //focusedRowObjectChangeHandler(_currentRow);
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
            _shopCd = ConvertUtil.ToInt32(jobj["SHOPCD"]);

            JObject jResult = new JObject();
            string url = "/order/getSaleDataList.json";

            gvList.BeginDataUpdate();
            _dt.Clear();
            gvList.EndDataUpdate();

            _dtAuthor.Clear();
            _dtEsti.Clear();
            _dtRet.Clear();
            _dtOrd.Clear();
            _dtInp.Clear();
            _dtInpRatio.Clear();

            Dangol.ShowSplash();

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray;

                    //if (Convert.ToBoolean(jResult["AUTHOR_EXIST"]))
                    //{
                    //    jArray = JArray.Parse(jResult["AUTHOR_DATA"].ToString());

                    //    foreach (JObject obj in jArray.Children<JObject>())
                    //    {
                    //        DataRow dr = _dtAuthor.NewRow();
                    //        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                    //        dr["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
                    //        _dtAuthor.Rows.Add(dr);
                    //    }
                    //}

                    //if (Convert.ToBoolean(jResult["ESTI_EXIST"]))
                    //{
                    //    jArray = JArray.Parse(jResult["ESTI_DATA"].ToString());

                    //    foreach (JObject obj in jArray.Children<JObject>())
                    //    {
                    //        DataRow dr = _dtEsti.NewRow();
                    //        dr["SHOPCD"] = ConvertUtil.ToInt32(obj["SHOPCD"]);
                    //        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                    //        dr["PURCHCD"] = ConvertUtil.ToInt64(obj["PURCHCD"]);
                    //        dr["ESTI_SUM"] = ConvertUtil.ToInt32(obj["ESTI_SUM"]);
                    //        _dtEsti.Rows.Add(dr);
                    //    }
                    //}

                    //if (Convert.ToBoolean(jResult["RET_EXIST"]))
                    //{
                    //    jArray = JArray.Parse(jResult["RET_DATA"].ToString());

                    //    foreach (JObject obj in jArray.Children<JObject>())
                    //    {
                    //        DataRow dr = _dtRet.NewRow();
                    //        dr["SHOPCD"] = ConvertUtil.ToInt32(obj["SHOPCD"]);
                    //        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                    //        dr["PURCHCD"] = ConvertUtil.ToInt64(obj["PURCHCD"]);
                    //        dr["RET_SUM"] = ConvertUtil.ToInt32(obj["RET_SUM"]);
                    //        _dtRet.Rows.Add(dr);
                    //    }
                    //}

                    //if (Convert.ToBoolean(jResult["ORD_EXIST"]))
                    //{
                    //    jArray = JArray.Parse(jResult["ORD_DATA"].ToString());

                    //    foreach (JObject obj in jArray.Children<JObject>())
                    //    {
                    //        DataRow dr = _dtOrd.NewRow();
                    //        dr["SHOPCD"] = ConvertUtil.ToInt32(obj["SHOPCD"]);
                    //        dr["STORECD"] = ConvertUtil.ToInt64(obj["STORECD"]);
                    //        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                    //        dr["PURCHCD"] = ConvertUtil.ToInt64(obj["PURCHCD"]);
                    //        dr["ORD_SUM"] = ConvertUtil.ToInt32(obj["ORD_SUM"]);
                    //        dr["ORD_RATE"] = ConvertUtil.ToDouble(obj["ORD_RATE"]);
                    //        dr["TRADE_ITEM"] = ConvertUtil.ToInt64(obj["TRADE_ITEM"]);
                    //        _dtOrd.Rows.Add(dr);
                    //    }
                    //}

                    //if (Convert.ToBoolean(jResult["INP_EXIST"]))
                    //{
                    //    jArray = JArray.Parse(jResult["INP_DATA"].ToString());

                    //    foreach (JObject obj in jArray.Children<JObject>())
                    //    {
                    //        DataRow dr = _dtInp.NewRow();
                    //        dr["SHOPCD"] = ConvertUtil.ToInt32(obj["SHOPCD"]);
                    //        dr["STORECD"] = ConvertUtil.ToInt64(obj["STORECD"]);
                    //        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                    //        dr["PURCHCD"] = ConvertUtil.ToInt64(obj["PURCHCD"]);
                    //        dr["INP_SUM"] = ConvertUtil.ToInt32(obj["INP_SUM"]);
                    //        _dtInp.Rows.Add(dr);
                    //    }
                    //}

                    //if (Convert.ToBoolean(jResult["INP_RATIO_EXIST"]))
                    //{
                    //    jArray = JArray.Parse(jResult["INP_RATIO_DATA"].ToString());

                    //    foreach (JObject obj in jArray.Children<JObject>())
                    //    {
                    //        DataRow dr = _dtInpRatio.NewRow();
                    //        dr["SHOPCD"] = ConvertUtil.ToInt32(obj["SHOPCD"]);
                    //        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                    //        dr["PURCHCD"] = ConvertUtil.ToInt64(obj["PURCHCD"]);
                    //        dr["RATE"] = ConvertUtil.ToDouble(obj["RATE"]);
                    //        _dtInpRatio.Rows.Add(dr);
                    //    }
                    //}

                    jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;

                    gvList.BeginDataUpdate();

                    int shopCd;
                    long storeCd;
                    long purchCd;
                    long bookCd;
                    int rate;

                    DataRow[] rows;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        shopCd = ConvertUtil.ToInt32(obj["SHOPCD"]);
                        storeCd = ConvertUtil.ToInt64(obj["STORECD"]);
                        purchCd = ConvertUtil.ToInt64(obj["PURCHCD"]);
                        bookCd = ConvertUtil.ToInt64(obj["BOOKCD"]);

                        dr["NO"] = index++;
                        dr["STORECD"] = storeCd;
                        dr["PURCHCD"] = purchCd;

                        dr["PURCHNM"] = ConvertUtil.ToString(obj["PURCHNM"]);
                        dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                        dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);
                        dr["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
                        dr["ESTI_SUM"] = ConvertUtil.ToInt32(obj["ESTI_SUM"]);

                        dr["RET_SUM"] = ConvertUtil.ToInt32(obj["RET_SUM"]);
                        dr["ORD_SUM"] = ConvertUtil.ToInt32(obj["ORD_SUM"]);
                        dr["ORD_RATE"] = ConvertUtil.ToInt32(obj["RATE"]);
                        dr["TRADE_ITEM"] = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
                        dr["INP_SUM"] = ConvertUtil.ToInt32(obj["INP_SUM"]);

                        dr["ORDER_CNT_YN"] = ConvertUtil.ToInt32(obj["RATE"]) == 0 ? "Y" : "N";
                        rate = ConvertUtil.ToInt32(obj["RATE"]);

                        if (rate > 0)
                        {
                            string key = $"{_shopCd}/{bookCd}/{purchCd}";

                            if (!_dicOrderRatioTable.ContainsKey(key))
                            {
                                DataTable dtOrderRatioTable;
                                dtOrderRatioTable = new DataTable();
                                dtOrderRatioTable.Columns.Add(new DataColumn("KEY", typeof(int)));
                                dtOrderRatioTable.Columns.Add(new DataColumn("VALUE", typeof(string)));

                                DataRow dr1 = dtOrderRatioTable.NewRow();

                                //dr1["CD"] = bookCd;
                                dr1["KEY"] = ConvertUtil.ToInt32(obj["RATE"]);
                                dr1["VALUE"] = ConvertUtil.ToString(obj["RATE"]);

                                dtOrderRatioTable.Rows.Add(dr1);

                                _dicOrderRatioTable.Add(key, dtOrderRatioTable);
                            }
                        }

                        //rate = ConvertUtil.ToInt32(obj["RATE"]);




                        //rows = _dtAuthor.Select($"BOOKCD = {bookCd}");
                        //if(rows.Length > 0)
                        //    dr["AUTHORNM"] = ConvertUtil.ToString(rows[0]["AUTHORNM"]);

                        //rows = _dtEsti.Select($"SHOPCD = {shopCd} AND PURCHCD = {purchCd} AND BOOKCD = {bookCd}");
                        //if (rows.Length > 0)
                        //dr["ESTI_SUM"] = ConvertUtil.ToInt32(rows[0]["ESTI_SUM"]);

                        //rows = _dtRet.Select($"SHOPCD = {shopCd} AND PURCHCD = {purchCd} AND BOOKCD = {bookCd}");
                        //if (rows.Length > 0)
                        //    dr["RET_SUM"] = ConvertUtil.ToInt32(rows[0]["RET_SUM"]);

                        //rows = _dtOrd.Select($"SHOPCD = {shopCd} AND STORECD = {storeCd} AND PURCHCD = {purchCd} AND BOOKCD = {bookCd}");
                        //if (rows.Length > 0)
                        //{
                        //    dr["ORD_SUM"] = ConvertUtil.ToInt32(rows[0]["ORD_SUM"]);
                        //    dr["ORD_RATE"] = ConvertUtil.ToDouble(rows[0]["ORD_RATE"]);
                        //    dr["TRADE_ITEM"] = ConvertUtil.ToInt64(obj["TRADE_ITEM"]);
                        //}
                        //else
                        //    dr["ORD_RATE"] = 0.0;

                        //if (ConvertUtil.ToDouble(dr["ORD_RATE"]) == 0.0) //ORDER_CNT_YN
                        //{
                        //    rows = _dtInpRatio.Select($"SHOPCD = {shopCd} AND PURCHCD = {purchCd} AND BOOKCD = {bookCd}");
                        //    if (rows.Length > 0)
                        //        dr["ORD_RATE"] = ConvertUtil.ToInt32(rows[0]["RATE"]);

                        //    dr["ORDER_CNT_YN"] = "N";
                        //}
                        //else
                        //    dr["ORDER_CNT_YN"] = "Y";

                        //rows = _dtInp.Select($"SHOPCD = {shopCd} AND STORECD = {storeCd} AND PURCHCD = {purchCd} AND BOOKCD = {bookCd}");
                        //if (rows.Length > 0)
                        //    dr["INP_SUM"] = ConvertUtil.ToInt32(rows[0]["INP_SUM"]);



                        dr["SALE_SUM"] = ConvertUtil.ToInt32(obj["SALE_SUM"]);
                        dr["STOCK"] = ConvertUtil.ToInt32(obj["STOCK"]);

                        dr["ORDER_CNT"] = 0;    //주문수량

                        dr["DEPTCD"] = ConvertUtil.ToInt32(obj["DEPTCD"]);
                        dr["BOOKCD"] = bookCd;

                        dr["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
                        dr["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);

                        dr["ORD_GROUPCD"] = ConvertUtil.ToInt32(obj["ORD_GROUPCD"]);

                        dr["SHOPCD"] = ConvertUtil.ToString(obj["SHOPCD"]);

                        dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);
                        dr["STATE"] = 0;
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

        public bool save()
        {
            DataRow[] rows = _dt.Select("ORDER_CNT > 0 AND ORD_RATE > 0");

            if(rows.Length < 1)
            {
                Dangol.Warining("확정할 데이터가 없습니다.");
                return false;
            }
            else
            {
                DataRow[] rowsExcept = _dt.Select("ORDER_CNT <= 0 OR ORD_RATE = 0");

                bool isContinue = false;

                if (rowsExcept.Length > 0)
                    isContinue = Dangol.MessageYN("데이터를 확정하시겠습니까?\n(매입률과 주문수량이 없는 데이터는 확정제외)") == DialogResult.Yes;
                else
                    isContinue = Dangol.MessageYN("데이터를 확정하시겠습니까?") == DialogResult.Yes;

                if (isContinue)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    var jArray = new JArray();

                    int price;
                    int ordRate;
                    int ordCost;
                    int purchCd;

                    foreach (DataRow row in rows)
                    {
                        JObject jdata = new JObject();

                        jdata.Add("SHOPCD", _shopCd);
                        jdata.Add("STORECD", ConvertUtil.ToInt32(row["STORECD"]));
                        int inpGroupCd = ConvertUtil.ToInt32(_jobj["INP_GROUPCD"]);
                        if(inpGroupCd == 1)
                            jdata.Add("INP_GROUPCD", 99);
                        else
                            jdata.Add("INP_GROUPCD", ConvertUtil.ToInt32(_jobj["GROUPCD"]));

                        jdata.Add("ORD_DATE", ConvertUtil.ToString(_jobj["ORDER_DATE"])); 
                        jdata.Add("INP_DATE", ConvertUtil.ToString(_jobj["INP_DATE"]));
                        jdata.Add("TRADE_ITEM", ConvertUtil.ToInt32(row["TRADE_ITEM"])); 

                        jdata.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));
                        jdata.Add("GROUPCD", ConvertUtil.ToInt32(row["GROUPCD"]));
                        jdata.Add("STANDCD", ConvertUtil.ToInt32(row["STANDCD"]));
                        jdata.Add("PURCHCD", ConvertUtil.ToInt32(row["PURCHCD"]));
                        jdata.Add("PURCHNM", ConvertUtil.ToString(row["PURCHNM"]));
                        price = ConvertUtil.ToInt32(row["PRICE"]);
                        ordRate = ConvertUtil.ToInt32(row["ORD_RATE"]);

                        ordCost = (price * ordRate) / 100;

                        jdata.Add("ORD_CNT", ConvertUtil.ToInt32(row["ORDER_CNT"]));
                        jdata.Add("ORD_RATE", ordRate);
                        jdata.Add("ORD_COST", ordCost);
                        jdata.Add("BOOKNM", ConvertUtil.ToString(row["BOOKNM"]));
                        jdata.Add("AUTHORNM", ConvertUtil.ToString(row["AUTHORNM"]));                            

                        jdata.Add("ORD_GROUPCD", ConvertUtil.ToInt32(row["ORD_GROUPCD"]));

                        jArray.Add(jdata);
                    }

                    jobj.Add("DATA", jArray);

                    string url = "/order/insertOrderBookBySaleData.json";

                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        Dangol.Message($"주문자료가 갱신되었습니다.");
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }

                return true;
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
            gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            getList(_jobj);
            gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
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
                    int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                    if (state == 1)
                        _currentRow["STATE"] = 2;
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

        private void gvList_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;

            if (view.FocusedColumn.FieldName == "ORD_RATE")
            {
                getOrderRatio();

                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                long bookCd = ConvertUtil.ToInt64(view.GetFocusedRowCellValue("BOOKCD"));
                int purchCd = ConvertUtil.ToInt32(view.GetFocusedRowCellValue("PURCHCD"));

                string key = $"{_shopCd}/{bookCd}/{purchCd}";

                if (_dicOrderRatioTable.ContainsKey(key))
                    editor.Properties.DataSource = _dicOrderRatioTable[key];
                else
                    editor.Properties.DataSource = null;
            }
        }
        private void getOrderRatio()
        {
            long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("BOOKCD", bookCd);
            jobj.Add("PURCHCD", purchCd);
            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("RATE_KBN", 1);
            

            string key = $"{_shopCd}/{bookCd}/{purchCd}";

            if (!_dicOrderRatioTable.ContainsKey(key) && bookCd > 0 && purchCd > 0)
            {
                string url = "/search/getOrderBookPurchRate.json";

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        DataTable dtOrderRatioTable;
                        dtOrderRatioTable = new DataTable();
                        dtOrderRatioTable.Columns.Add(new DataColumn("KEY", typeof(int)));
                        dtOrderRatioTable.Columns.Add(new DataColumn("VALUE", typeof(string)));

                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            DataRow dr1 = dtOrderRatioTable.NewRow();

                            //dr1["CD"] = bookCd;
                            dr1["KEY"] = ConvertUtil.ToInt32(obj["RATE"]);
                            dr1["VALUE"] = ConvertUtil.ToString(obj["RATE"]);

                            dtOrderRatioTable.Rows.Add(dr1);

                        }

                        _dicOrderRatioTable.Add(key, dtOrderRatioTable);

                    }
                    else
                    {
                        DataTable dtOrderRatioTable;
                        dtOrderRatioTable = new DataTable();
                        dtOrderRatioTable.Columns.Add(new DataColumn("KEY", typeof(int)));
                        dtOrderRatioTable.Columns.Add(new DataColumn("VALUE", typeof(string)));

                        DataRow dr1 = dtOrderRatioTable.NewRow();

                        //dr1["CD"] = bookCd;
                        dr1["KEY"] = 0;
                        dr1["VALUE"] = "0";

                        dtOrderRatioTable.Rows.Add(dr1);

                        //Dangol.Warining($"도서코드 ['{bookCd}']에 {_conditionString} 해당하는 매입율이 없습니다. 주문할 수 없습니다.");
                    }
                        
                }
                else
                {
                    DataTable dtOrderRatioTable;
                    dtOrderRatioTable = new DataTable();
                    dtOrderRatioTable.Columns.Add(new DataColumn("KEY", typeof(int)));
                    dtOrderRatioTable.Columns.Add(new DataColumn("VALUE", typeof(string)));

                    DataRow dr1 = dtOrderRatioTable.NewRow();

                    //dr1["CD"] = bookCd;
                    dr1["KEY"] = 0;
                    dr1["VALUE"] = "0";

                    dtOrderRatioTable.Rows.Add(dr1);
                    //Dangol.Error(jResult["MSG"]);
                }
            }
        }
    }
}
