using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.returns
{
    public partial class usrModifReturnList : DevExpress.XtraEditors.XtraUserControl
    {

        
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        DataTable _dtPurchCd;
        BindingSource _bsPruchCd;

        DataTable _dtOrderRatio;
        BindingSource _bsOrderRatio;

        DataTable _dtRateKbn;

        JObject _jobj;


        int _shopCd;
        int _storeCd;
        int _groupType; // 조 구분
        int _groupCd;
        int _inpGroupCd;
        string _groupNm;
        int _purchType;
        int _purchCd;
        string _purchNm;
        int _orderType;
        int _orderCondition;
        string _conditionString;
        int _tradeItem;
        string _orderDt;
        string _inpDt;


        Dictionary<long, LookUpEdit> _dicLookUpEdit;
        Dictionary<long, DataTable> _dicPurchCdTable;
        Dictionary<long, DataTable> _dicPurchOrder;
        



        //Dictionary<long, DataTable> _dicPurchCdTable;
        Dictionary<string, DataTable> _dicOrderRatioTable;

        Dictionary<int, string> _dicPurchNm;
        Dictionary<int, int> _dicPurchGroupCd;




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

        public delegate JObject GetSearchInfoHandler();
        public event GetSearchInfoHandler getSearchInfoHandler;


        public usrModifReturnList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("ID", typeof(int)));

            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("AUTHOR1", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHOR2", typeof(string)));

            _dt.Columns.Add(new DataColumn("PUBSHCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("PURCHNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("PUR_PROCESS", typeof(string)));
            _dt.Columns.Add(new DataColumn("STOCK", typeof(int)));


            _dt.Columns.Add(new DataColumn("RATE11", typeof(int)));
            _dt.Columns.Add(new DataColumn("RATE12", typeof(int)));
            _dt.Columns.Add(new DataColumn("RATE13", typeof(int)));
            _dt.Columns.Add(new DataColumn("RATE14", typeof(int)));

            _dt.Columns.Add(new DataColumn("STOCK11", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK12", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK13", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK14", typeof(int)));

            _dt.Columns.Add(new DataColumn("RATE21", typeof(int)));
            _dt.Columns.Add(new DataColumn("RATE22", typeof(int)));
            _dt.Columns.Add(new DataColumn("RATE23", typeof(int)));
            _dt.Columns.Add(new DataColumn("RATE24", typeof(int)));

            _dt.Columns.Add(new DataColumn("STOCK21", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK22", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK23", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK24", typeof(int)));

            _dt.Columns.Add(new DataColumn("FIRSTSTORE", typeof(string)));
            _dt.Columns.Add(new DataColumn("LASTSALES", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("RET_PLAN_CNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("RETURN_RATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("RETURN_CNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("RETURN_REMARK", typeof(int)));
            _dt.Columns.Add(new DataColumn("RETURN_KBN", typeof(int)));

            _dt.Columns.Add(new DataColumn("RATE_KBN", typeof(int)));
            _dt.Columns.Add(new DataColumn("RATE_KBN_O", typeof(int)));
            _dt.Columns.Add(new DataColumn("RET_COST", typeof(int)));
            

            _dt.Columns.Add(new DataColumn("GROUPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("STANDCD", typeof(int)));

            //_dt.Columns.Add(new DataColumn("GROUPCD", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STANDCD", typeof(int)));

            //_dt.Columns.Add(new DataColumn("INP_CNT", typeof(int)));
            //_dt.Columns.Add(new DataColumn("RETURN_CNT", typeof(int)));
            //_dt.Columns.Add(new DataColumn("ORD_CNT", typeof(int)));
            //_dt.Columns.Add(new DataColumn("ESTI_CNT", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STOCK_CNT", typeof(int)));

            //_dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            //_dt.Columns.Add(new DataColumn("ORDER_RATIO", typeof(double)));
            //_dt.Columns.Add(new DataColumn("ORDER_CNT", typeof(int)));

            //_dt.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));  //0:default, 1:create, 2:available, 3:complete, -1:notavailable
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtPurchCd = new DataTable();

            _dtPurchCd.Columns.Add(new DataColumn("KEY", typeof(int)));
            _dtPurchCd.Columns.Add(new DataColumn("VALUE", typeof(string)));
            
            _dtOrderRatio = new DataTable();

            _dtOrderRatio.Columns.Add(new DataColumn("CD", typeof(string)));
            _dtOrderRatio.Columns.Add(new DataColumn("KEY", typeof(int)));
            _dtOrderRatio.Columns.Add(new DataColumn("VALUE", typeof(string)));



            _bs = new BindingSource();
            _bsPruchCd = new BindingSource();
            _bsOrderRatio = new BindingSource();

            _dicLookUpEdit = new Dictionary<long, LookUpEdit>();
            _dicPurchCdTable = new Dictionary<long, DataTable>();
            _dicPurchOrder = new Dictionary<long, DataTable>();
            _dicOrderRatioTable = new Dictionary<string, DataTable>();
            _dicPurchNm = new Dictionary<int, string>();
            _dicPurchGroupCd = new Dictionary<int, int>();
        }



        public void setInitLoad()
        {
            setInfoBox();
            setinitialize();
        }

        private void setInfoBox()
        {

            Util.LookupEditHelper(rilePurchCd, _bsPruchCd, "KEY", "VALUE");

            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("KEY", typeof(int)));
            dt1.Columns.Add(new DataColumn("VALUE", typeof(string)));
            for (int i = 10; i > 0; i--) Util.insertRow(dt1, i, i.ToString());
            Util.LookupEditHelper(rileProcess, dt1, "KEY", "VALUE");

            Util.LookupEditHelper(rileOrderRatio, _dtOrderRatio, "KEY", "VALUE");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("KEY", typeof(int)));
            dt2.Columns.Add(new DataColumn("VALUE", typeof(string)));
            for (int i = 1; i < 5; i++) Util.insertRow(dt2, i, i.ToString());
            Util.LookupEditHelper(rileReturnReason, dt2, "KEY", "VALUE");
            Util.LookupEditHelper(rileReturnType, dt2, "KEY", "VALUE");

        }

        public void setinitialize()
        {
            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;


            _bsPruchCd.DataSource = _dtPurchCd;

            //_bsPruchCd.DataSource = _dtPurchCd;
            _bsOrderRatio.DataSource = _dtOrderRatio;
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
            _jobj = obj;

            _shopCd = ConvertUtil.ToInt32(obj["SHOPCD"]);
            //_storeCd = ConvertUtil.ToInt32(obj["STORECD"]);
            //_groupType = ConvertUtil.ToInt32(obj["GROUP_TYPE"]);
            //_groupCd = ConvertUtil.ToInt32(obj["GROUPCD"]);
            //_inpGroupCd = ConvertUtil.ToInt32(obj["INP_GROUPCD"]);
            //_groupNm = ConvertUtil.ToString(obj["GROUPNM"]);
            //_purchType = ConvertUtil.ToInt32(obj["PURCH_TYPE"]);
            //_purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);
            //_purchNm = ConvertUtil.ToString(obj["PURCHNM"]);
            //_orderType = ConvertUtil.ToInt32(obj["ORDER_TYPE"]);
            //_orderCondition = ConvertUtil.ToInt32(obj["ORDER_CONTIDION"]);
            //_conditionString = ConvertUtil.ToString(obj["ORDER_CONTIDION_STRING"]);
            //_tradeItem = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
            //_orderDt = ConvertUtil.ToString(obj["DT_ORDER"]);
            //_inpDt = ConvertUtil.ToString(obj["DT_INP"]);
        }

        public void setTableInitialize()
        {
            gvList.BeginDataUpdate();
            _dt.Clear();

            for (int i = 0; i < 30; i++)
            {
                DataRow dr = _dt.NewRow();

                dr["NO"] = i + 1;
                dr["ID"] = i + 1;

                //dr["BOOK_CD"] = ConvertUtil.ToString(obj["BOOK_CD"]);
                //dr["TITLE"] = ConvertUtil.ToString(obj["TITLE"]);
                //dr["AUTHOR"] = ConvertUtil.ToString(obj["AUTHOR"]);
                //dr["PUBLISHER"] = ConvertUtil.ToString(obj["PUBLISHER"]);
                //dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);
                //dr["RETURN_CNT"] = ConvertUtil.ToInt32(obj["RETURN_CNT"]);
                //dr["DELIVERY_CNT"] = ConvertUtil.ToInt32(obj["DELIVERY_CNT"]);

                //dr["WAREHOUSING_CNT"] = ConvertUtil.ToInt32(obj["WAREHOUSING_CNT"]);
                //dr["WAREHOUSING_EXPECTED_CNT"] = ConvertUtil.ToInt32(obj["WAREHOUSING_EXPECTED_CNT"]);
                //dr["STOCK_CNT"] = ConvertUtil.ToInt32(obj["STOCK_CNT"]);

                //dr["SELLER"] = ConvertUtil.ToString(obj["SELLER"]);
                //dr["ORDER_RATIO"] = ConvertUtil.ToDouble(obj["ORDER_RATIO"]);

                //dr["ORDER_CNT"] = ConvertUtil.ToInt32(obj["ORDER_CNT"]);
                //dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);

                dr["STATE"] = 0; //0:default, 1:create, 2:available, 3:complete, -1:notavailable
                dr["CHECK"] = false;
                _dt.Rows.Add(dr);
            }

            gvList.EndDataUpdate();

            _dtPurchCd.Clear();
            _dtOrderRatio.Clear();

            _dicLookUpEdit.Clear();
            _dicPurchCdTable.Clear();
            _dicPurchOrder.Clear();
            _dicOrderRatioTable.Clear();
        }

        public void setTableEditable(bool isEditable)
        {
            gvList.OptionsBehavior.Editable = isEditable;
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

        public void insertReturn()
        {
            DataRow[] rows = _dt.Select("STATE <> 0");

            if (rows.Length < 1)
                Dangol.Warining("반품건이 없습니다.");
            else
            {
                if (dataVerification())
                {
                    if (Dangol.MessageYN("반품건을 확정하시겠습니까?") == DialogResult.Yes)
                        confirmData();
                }
                else
                {
                    if (Dangol.MessageYN("완료되지 않은 반품건이 있습니다. 그래도 진행하시겠습니까?") == DialogResult.Yes)
                    {
                        confirmData();
                    }
                }
            }
        }
        private bool dataVerification()
        {
            DataRow[] rows = _dt.Select("STATE IN (1, 2, 3)");

            long bookCd;
            int purchCd;
            int returnRate;
            int returnCnt;

            int faultCnt = 0;

            foreach (DataRow row in rows)
            {
                bookCd = ConvertUtil.ToInt64(row["BOOKCD"]);
                purchCd = ConvertUtil.ToInt32(row["PURCHCD"]);
                returnRate = ConvertUtil.ToInt32(row["RETURN_RATE"]);
                //returnCnt = ConvertUtil.ToInt32(row["RETURN_CNT"]);

                if (bookCd > 0 && purchCd > 0 && returnRate > 0)
                    row["STATE"] = 2;
                else
                {
                    row["STATE"] = 1;
                    faultCnt++;
                }
            }

            return faultCnt == 0;
        }

        private void confirmData()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            //jobj.Add("SHOPCD", _shopCd);
            //jobj.Add("STORECD", _storeCd);
            //jobj.Add("GROUPCD", _inpGroupCd);
            //jobj.Add("ORD_DATE", _orderDt);
            //jobj.Add("TRADE_ITEM", _tradeItem);

            DataRow[] rows = _dt.Select("STATE = 2");

            int price;
            int returnRate;
            int returnCost;
            //int purchCd;

            foreach (DataRow row in rows)
            {
                JObject jdata = new JObject();

                jdata.Add("SHOPCD", _shopCd);
                jdata.Add("STORECD", ConvertUtil.ToInt32(_jobj["STORECD"]));

                jdata.Add("ID", ConvertUtil.ToInt32(row["ID"]));
                jdata.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));
                returnRate = ConvertUtil.ToInt32(row["RETURN_RATE"]);
                jdata.Add("RETURN_RATE", returnRate);
                jdata.Add("RET_PLAN_CNT", ConvertUtil.ToInt32(row["RET_PLAN_CNT"]));
                jdata.Add("RETURN_CNT", ConvertUtil.ToInt32(row["RETURN_CNT"]));
                price = ConvertUtil.ToInt32(row["PRICE"]);
                jdata.Add("PRICE", price);

                jdata.Add("RETURN_KBN", ConvertUtil.ToInt32(row["RETURN_KBN"]));
                jdata.Add("RETURN_REMARK", ConvertUtil.ToInt32(row["RETURN_REMARK"]));

                jdata.Add("BOOKNM", ConvertUtil.ToString(row["BOOKNM"]));
                jdata.Add("STOCK", ConvertUtil.ToInt32(row["STOCK"]));
                jdata.Add("RATE_KBN", ConvertUtil.ToInt32(row["RATE_KBN"]));
                jdata.Add("RATE_KBN_O", ConvertUtil.ToInt32(row["RATE_KBN_O"]));
                
                if (_jobj.ContainsKey("RET_GROUPCD"))
                    jdata.Add("RETURN_GROUPCD", ConvertUtil.ToInt32(_jobj["RET_GROUPCD"]));

                if (_jobj.ContainsKey("GROUPCD"))
                    jdata.Add("GROUPCD", ConvertUtil.ToInt32(_jobj["GROUPCD"]));

                jdata.Add("PURCHCD", ConvertUtil.ToInt32(row["PURCHCD"]));
                //jdata.Add("PURCHNM", ConvertUtil.ToString(row["PURCHNM"]));

                //jdata.Add("GROUPCD", ConvertUtil.ToInt32(_jobj["GROUPCD"]));
                //jdata.Add("STANDCD", ConvertUtil.ToInt32(row["STANDCD"]));

                //jdata.Add("RETURN_GROUPCD", ConvertUtil.ToInt32(row["GROUPCD"]));
                //jdata.Add("RETURN_STANDCD", ConvertUtil.ToInt32(row["STANDCD"]));

                //jdata.Add("PUBSHCD", ConvertUtil.ToInt32(row["PUBSHCD"]));
                //jdata.Add("PUBSHNM", ConvertUtil.ToString(row["PUBSHNM"]));

                //jdata.Add("AUTHOR1", ConvertUtil.ToString(row["AUTHOR1"]));
                //jdata.Add("AUTHOR2", ConvertUtil.ToString(row["AUTHOR2"]));

                returnCost = (price * returnRate) / 100;

                jdata.Add("RETURN_COST", returnCost);

                jArray.Add(jdata);
            }

            jobj.Add("DATA", jArray);

            string url = "/returns/confirmReturnBook.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                Dangol.Message($"{rows.Length}개의 데이터가 확정되었습니다.");
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
            }
        }

        public void getList()
        {
            JObject jResult = new JObject();
            string url = "/returns/getReturnBookList.json";

            gvList.BeginDataUpdate();
            _dt.Clear();

            if (DBConnect.getRequest(_jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray;
                    jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    int total;
                    long bookCd;
                    int purchCd;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();
                        bookCd = ConvertUtil.ToInt64(obj["BOOKCD"]);
                        purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);

                        dr["NO"] = index++;
                        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                        dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);

                        dr["PURCHNM"] = ConvertUtil.ToString(obj["PURCHNM"]);
                        dr["PURCHCD"] = purchCd;
                        dr["STOCK"] = ConvertUtil.ToInt32(obj["STOCK"]);

                        dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

                        dr["RETURN_KBN"] = ConvertUtil.ToInt32(obj["RET_KBN"]);
                        dr["RETURN_RATE"] = ConvertUtil.ToInt32(obj["RET_RATE"]);

                        dr["RETURN_CNT"] = ConvertUtil.ToInt32(obj["RET_COUNT"]);
                        dr["RET_COST"] = ConvertUtil.ToInt32(obj["RET_COST"]);
                        dr["RATE_KBN_O"] = dr["RATE_KBN"] = ConvertUtil.ToInt32(obj["RATE_KBN"]);
                        

                        dr["STATE"] = 1;
                        dr["CHECK"] = false;
                        
                        setPurchInfo(bookCd, purchCd, dr);

                        _dt.Rows.Add(dr);

                    }

                    gvList.EndDataUpdate();

                    if (_dt.Rows.Count < 1)
                        Dangol.Info("검색 결과가 없습니다.");
                }
            }
            else
            {
                gvList.EndDataUpdate();
                Dangol.Error(jResult["MSG"]);
            }
        }

        public void deleteRowHandler()
        {
            if (_currentRow == null)
                Dangol.Warining("선택된 행이 없습니다.");
            else if (ConvertUtil.ToInt32(_currentRow["STATE"]) == 0)
                return;
            else
            {
                if (Dangol.MessageYN("선택한 행을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);

                    _currentRow.BeginEdit();

                    foreach (KeyValuePair<int, string> item in Common._dicRateKbn)
                    {
                        _currentRow[$"RATE{item.Key}"] = DBNull.Value;
                        _currentRow[$"STOCK{item.Key}"] = DBNull.Value;
                    }

                    _currentRow["BOOKCD"] = DBNull.Value;
                    _currentRow["BOOKNM"] = DBNull.Value;

                    _currentRow["AUTHOR1"] = DBNull.Value;
                    _currentRow["AUTHOR2"] = DBNull.Value;

                    _currentRow["PUBSHNM"] = DBNull.Value;
                    _currentRow["PUBSHCD"] = DBNull.Value;

                    _currentRow["PURCHNM"] = DBNull.Value;
                    _currentRow["PURCHCD"] = DBNull.Value;

                    _currentRow["PUR_PROCESS"] = DBNull.Value;
                    _currentRow["STOCK"] = DBNull.Value;

                    _currentRow["FIRSTSTORE"] = DBNull.Value;
                    _currentRow["LASTSALES"] = DBNull.Value;

                    _currentRow["PRICE"] = DBNull.Value;

                    _currentRow["RETURN_RATE"] = DBNull.Value;
                    _currentRow["RETURN_CNT"] = DBNull.Value;

                    _currentRow["GROUPCD"] = DBNull.Value;
                    _currentRow["STANDCD"] = DBNull.Value;

                    _currentRow["RETURN_REMARK"] = DBNull.Value;
                    _currentRow["RETURN_KBN"] = DBNull.Value;

                    _currentRow["RATE_KBN"] = DBNull.Value;

                    _currentRow["RET_PLAN_CNT"] = DBNull.Value;


                    _currentRow["STATE"] = 0;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable

                    _currentRow.EndEdit();

                    if (_dt.Select($"BOOKCD = {bookCd}").Length < 1)
                    {
                        if (_dicLookUpEdit.ContainsKey(bookCd)) _dicLookUpEdit.Remove(bookCd);
                        if (_dicPurchOrder.ContainsKey(bookCd)) _dicPurchOrder.Remove(bookCd);
                        if (_dicPurchCdTable.ContainsKey(bookCd)) _dicPurchCdTable.Remove(bookCd);
                    }
                }
            }
        }

        public void getPerformance()
        {
            if (_currentRow == null)
                Dangol.Warining("선택된 데이터가 없습니다.");
            else if (ConvertUtil.ToInt32(_currentRow["STATE"]) == 0)
                return;
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
            getList();
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
                    //int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                    //if (state == 1)
                    //    _currentRow["STATE"] = 2;

                    if (e.Column.FieldName == "PUR_PROCESS")
                    {
                        _currentRow["PUR_PROCESS"] = e.Value;
                    }
                }
            }
        }

        private void riteBookCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                TextEdit textEditor = (TextEdit)sender;
                string data = textEditor.Text;
                long bookCd = ConvertUtil.ToInt64(data);

                if (bookCd < 1)
                {
                    Dangol.Warining("도서코드를 입력하세요");
                }
                else
                {
                    getBookList(bookCd, "");
                    //this.gvList.PostEditor();
                    //this.gvList.SetFocusedRowCellValue("PURCHCD", null);
                }
            }
        }

        private void riteTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEditor = (TextEdit)sender;
                string title = textEditor.Text;

                if (string.IsNullOrWhiteSpace(title))
                {
                    Dangol.Warining("도서명을 입력하세요");
                }
                else
                {
                    getBookList(0, title);
                    //this.gvList.PostEditor();
                    //this.gvList.SetFocusedRowCellValue("PURCHCD", null);
                }
            }
        }

        private void getBookList(long bookCd, string bookNm)
        {
            //JObject jData = getSearchInfoHandler();

            int shopCd = ConvertUtil.ToInt32(_jobj["SHOPCD"]);
            long purchCd = 0;

            if (bookCd == 0)
            {
                if (_jobj.ContainsKey("PURCHCD"))
                    purchCd = ConvertUtil.ToInt64(_jobj["PURCHCD"]);

                using (dlgBookSearch bookSearch = new dlgBookSearch(shopCd, purchCd, bookCd, bookNm))
                {
                    //bookSearch.StartPosition = FormStartPosition.Manual;
                    //bookSearch.Location = new Point(this.Location.X + (this.Size.Width / 2) - (bookSearch.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (bookSearch.Size.Height / 2));

                    if (bookSearch.ShowDialog(this) == DialogResult.OK)
                    {
                        DataRowView objBook = bookSearch._drv;

                        bookNm = ConvertUtil.ToString(objBook["BOOKNM"]);
                        bookCd = ConvertUtil.ToInt64(objBook["BOOKCD"]);

                        setSearchBook(shopCd, bookCd, bookNm, 1);
                    }
                }
            }
            else
            {
                setSearchBook(shopCd, bookCd, bookNm, 2);
            }
        }

        private void setSearchBook(int shopCd, long bookCd, string bookNm, int flag)
        {
            //JObject jResult = new JObject();
            //JObject jobj = new JObject();

            //jobj.Add("BOOKCD", bookCd);
            //jobj.Add("SHOPCD", shopCd);

            //if(_jobj.ContainsKey("PURCHCD"))
            //    jobj.Add("PURCHCD", ConvertUtil.ToInt32(_jobj["PURCHCD"]));

            //string url = "/returns/getReturnBookList.json";

            //if (DBConnect.getRequest(jobj, ref jResult, url))
            //{
            //    if (Convert.ToBoolean(jResult["EXIST"]))
            //    {
            //        if (Convert.ToBoolean(jResult["PURCH_EXIST"]))
            //        {
            //            JArray jArray = JArray.Parse(jResult["DATA"].ToString());
            //            JArray jArrayPurch = JArray.Parse(jResult["PURCH_DATA"].ToString());

            //            DataRow[] rowsBookCd = _dt.Select($"BOOKCD = {bookCd}");

            //            foreach (JObject obj in jArray.Children<JObject>())
            //            {
            //                insertOrderBook(obj);
            //                break;
            //            }

            //            if (rowsBookCd.Length < 1)
            //            {
            //                DataTable dtPurchCd;
            //                dtPurchCd = new DataTable();
            //                dtPurchCd.Columns.Add(new DataColumn("ORDER", typeof(int)));
            //                dtPurchCd.Columns.Add(new DataColumn("KEY", typeof(long)));
            //                dtPurchCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //                DataTable dtPurchOrder;
            //                dtPurchOrder = new DataTable();
            //                dtPurchOrder.Columns.Add(new DataColumn("KEY", typeof(int)));
            //                dtPurchOrder.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //                DataRow[] rows;
            //                int purchCdT;
            //                string purchNmT;

            //                //int maxOrder = 1;
            //                //long maxPurchCd = -1;

            //                //int index = 1;

            //                int order;

            //                foreach (JObject obj in jArrayPurch.Children<JObject>())
            //                {
            //                    purchCdT = ConvertUtil.ToInt32(obj["PURCHCD"]);
            //                    purchNmT = ConvertUtil.ToString(obj["PURCHNM"]);
            //                    order = ConvertUtil.ToInt32(obj["PUR_PROCESS"]);

            //                    rows = _dtPurchCd.Select($"KEY = {purchCdT}");

            //                    if (rows.Length == 0)
            //                    {
            //                        DataRow dr = _dtPurchCd.NewRow();

            //                        dr["KEY"] = purchCdT;
            //                        dr["VALUE"] = purchNmT;

            //                        _dtPurchCd.Rows.Add(dr);
            //                    }

            //                    DataRow dr1 = dtPurchCd.NewRow();

            //                    dr1["ORDER"] = order;
            //                    dr1["KEY"] = purchCdT;
            //                    dr1["VALUE"] = purchNmT;

            //                    dtPurchCd.Rows.Add(dr1);

            //                    DataRow dr2 = dtPurchOrder.NewRow();

            //                    dr2["KEY"] = order;
            //                    dr2["VALUE"] = ConvertUtil.ToString(order);

            //                    dtPurchOrder.Rows.Add(dr2);
            //                }

            //                LookUpEdit editor = new LookUpEdit();
            //                Util.LookupEditHelper(editor, dtPurchOrder, "KEY", "VALUE");

            //                _dicLookUpEdit.Add(bookCd, editor);
            //                _dicPurchOrder.Add(bookCd, dtPurchOrder);
            //                _dicPurchCdTable.Add(bookCd, dtPurchCd);

            //                _bsPruchCd.DataSource = _dtPurchCd;

            //                int maxOrder = Convert.ToInt32(dtPurchOrder.AsEnumerable().Max(row => row["KEY"]));

            //                _currentRow.BeginEdit();
            //                _currentRow["PUR_PROCESS"] = maxOrder;
            //                setPurchInfo(bookCd, maxOrder);
            //                _currentRow.EndEdit();
            //            }
            //            else
            //            {
            //                DataTable dt = _dicPurchOrder[bookCd];

            //                int maxOrder = Convert.ToInt32(dt.AsEnumerable().Max(row => row["KEY"]));

            //                _currentRow.BeginEdit();
            //                _currentRow["PUR_PROCESS"] = maxOrder;
            //                setPurchInfo(bookCd, maxOrder);
            //                _currentRow.EndEdit();
            //            }
            //        }
            //        else
            //        {
            //            if (flag == 1)
            //                Dangol.Warining($"도서명['{bookNm}']에 대한 매입처가 없습니다. 주문할수없습니다.");
            //            else
            //                Dangol.Warining($"도서코드['{bookCd}']에 대한 매입처가 없습니다. 주문할 수 없습니다.");
            //            _currentRow["STATE"] = -1;

            //        }
            //    }
            //    else
            //    {
            //        if (flag == 1)
            //            Dangol.Warining($"도서명 ['{bookNm}']가 점별도서테이블에 존재하지 않습니다.");
            //        else
            //            Dangol.Warining($"도서코드 ['{bookCd}']가 점별도서테이블에 존재하지 않습니다.");
            //    }
            //}
            //else
            //{
            //    Dangol.Error(jResult["MSG"]);
            //}
        }

        private void insertOrderBook(DataRowView obj)
        {
            //_currentRow.BeginEdit();

            //_currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            //riteTitle.BeginUpdate();
            //_currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
            //riteTitle.EndUpdate();
            //_currentRow["AUTHOR"] = ConvertUtil.ToString(obj["AUTHOR1"]);
            //_currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
            //_currentRow["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

            //_currentRow["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
            //_currentRow["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);


            //_currentRow["INP_CNT"] = 0;
            //_currentRow["RETURN_CNT"] = 0;

            //_currentRow["ORD_CNT"] = 0;
            //_currentRow["ESTI_CNT"] = 0;
            //_currentRow["STOCK_CNT"] = 0;
            //_currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable

            //_currentRow.EndEdit();

        }

        private void insertOrderBook(JObject obj)
        {
            _currentRow.BeginEdit();
            _currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            _currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);

            _currentRow["AUTHOR1"] = ConvertUtil.ToString(obj["AUTHOR1"]);
            _currentRow["AUTHOR2"] = ConvertUtil.ToString(obj["AUTHOR12"]);

            _currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
            _currentRow["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);

            _currentRow["STOCK"] = ConvertUtil.ToInt32(obj["STOCK"]);

            _currentRow["FIRSTSTORE"] = ConvertUtil.ToDateTimeNull(obj["FIRSTSTORE"]);
            _currentRow["LASTSALES"] = ConvertUtil.ToDateTimeNull(obj["LASTSALES"]);

            _currentRow["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

            _currentRow["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
            _currentRow["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);

            _currentRow["RETURN_REMARK"] = 1;
            _currentRow["RETURN_KBN"] = 1;

            _currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable

            _currentRow.EndEdit();
        }

        private void gcList_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void gvList_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void gvList_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
          
        }

        private void gvList_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;

            if (view.FocusedColumn.FieldName == "PUR_PROCESS")
            {
                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                long bookCd = ConvertUtil.ToInt64(view.GetFocusedRowCellValue("BOOKCD"));
                if (_dicLookUpEdit.ContainsKey(bookCd))
                    editor.Properties.DataSource = _dicPurchOrder[bookCd];
                else
                    editor.Properties.DataSource = null;
            }
            else if (view.FocusedColumn.FieldName == "RETURN_RATE")
            {
                //getOrderRatio();

                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                long bookCd = ConvertUtil.ToInt64(view.GetFocusedRowCellValue("BOOKCD"));
                int purchCd = ConvertUtil.ToInt32(view.GetFocusedRowCellValue("PURCHCD"));

                string key = $"{_shopCd}/{bookCd}/{purchCd}";

                if (_dicOrderRatioTable.ContainsKey(key))
                {
                    editor.Properties.DataSource = _dicOrderRatioTable[key];
                    _dtRateKbn = _dicOrderRatioTable[key];
                }
                else
                {
                    _dtRateKbn = null;
                    editor.Properties.DataSource = null;
                }
            }
        }

        private void rilePurchCd_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void getOrderRatio()
        { 
            //long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            //int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);

            //JObject jResult = new JObject();
            //JObject jobj = new JObject();

            //jobj.Add("BOOKCD", bookCd);
            //jobj.Add("PURCHCD", purchCd);
            //jobj.Add("SHOPCD", _shopCd);

            //string key = $"{_shopCd}/{bookCd}/{purchCd}";

            //if (!_dicOrderRatioTable.ContainsKey(key) && bookCd > 0 && purchCd > 0)
            //{
            //    if (_orderCondition == 1)
            //    {
            //        jobj.Add("RATE_KBN_S", 10);
            //        jobj.Add("RATE_KBN_E", 20);
            //    }
            //    else
            //    {
            //        jobj.Add("RATE_KBN_S", 20);
            //        jobj.Add("RATE_KBN_E", 30);
            //    }

            //    string url = "/search/getOrderBookPurchRate.json";

            //    if (DBConnect.getRequest(jobj, ref jResult, url))
            //    {
            //        if (Convert.ToBoolean(jResult["EXIST"]))
            //        {
            //            DataTable dtOrderRatioTable;
            //            dtOrderRatioTable = new DataTable();
            //            dtOrderRatioTable.Columns.Add(new DataColumn("KEY", typeof(int)));
            //            dtOrderRatioTable.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //            JArray jArray = JArray.Parse(jResult["DATA"].ToString());

            //            int rate = 0;
            //            DataRow[] rows;
            //            foreach (JObject obj in jArray.Children<JObject>())
            //            {
            //                rate = ConvertUtil.ToInt32(obj["RATE"]);

            //                rows = _dtPurchCd.Select($"KEY = {rate}");

            //                if (rows.Length == 0)
            //                {
            //                    DataRow dr = _dtOrderRatio.NewRow();

            //                    //dr["CD"] = key;
            //                    dr["KEY"] = ConvertUtil.ToInt32(obj["RATE"]);
            //                    dr["VALUE"] = ConvertUtil.ToString(obj["RATE"]);

            //                    _dtOrderRatio.Rows.Add(dr);
            //                }

            //                DataRow dr1 = dtOrderRatioTable.NewRow();

            //                //dr1["CD"] = bookCd;
            //                dr1["KEY"] = ConvertUtil.ToInt32(obj["RATE"]);
            //                dr1["VALUE"] = ConvertUtil.ToString(obj["RATE"]);

            //                dtOrderRatioTable.Rows.Add(dr1);
                            
            //            }

            //            _dicOrderRatioTable.Add(key, dtOrderRatioTable);

            //        }
            //        else
            //            Dangol.Warining($"도서코드 ['{bookCd}']에 {_conditionString} 해당하는 매입율이 없습니다. 주문할 수 없습니다.");
            //    }
            //    else
            //    {
            //        Dangol.Error(jResult["MSG"]);
            //    }
            //}   
        }

        private void setOrderCount(long bookCd, int purchCd, int orderRate)
        {
            //if (_groupType == 2)
            //{
            //    JObject jResult = new JObject();
            //    JObject jobj = new JObject();

            //    jobj.Add("SHOPCD", _shopCd);
            //    jobj.Add("STORECD", _storeCd);

            //    if(_groupCd > 0)
            //        jobj.Add("GROUPCD", _groupCd);
            //    //if (!string.IsNullOrEmpty(_groupNm))
            //        //jobj.Add("GROUPNM", _groupNm);

            //    jobj.Add("BOOKCD", bookCd);
            //    jobj.Add("PURCHCD", purchCd);
            //    jobj.Add("ORDER_RATE", orderRate);

            //    string url = "/search/getOrderBookCntInfo.json";

            //    if (DBConnect.getRequest(jobj, ref jResult, url))
            //    {
            //        //if (Convert.ToBoolean(jResult["EXIST"]))
            //        //{

            //        //}

            //        _currentRow["INP_CNT"] = ConvertUtil.ToInt32(jResult["INP_CNT"]);
            //        _currentRow["RETURN_CNT"] = ConvertUtil.ToInt32(jResult["RETURN_CNT"]);
            //        _currentRow["ORD_CNT"] = ConvertUtil.ToInt32(jResult["ORD_CNT"]);
            //        _currentRow["ESTI_CNT"] = ConvertUtil.ToInt32(jResult["ESTI_CNT"]);
            //    }
            //    else
            //    {
            //        Dangol.Error(jResult["MSG"]);
            //    }
            //}
        }

        private void rileOrderRatio_EditValueChanged(object sender, EventArgs e)
        {
            //long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            //int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);
            //int orderRatio = ConvertUtil.ToInt32(_currentRow["ORDER_RATIO"]);

            //setOrderCount(bookCd, purchCd, orderRatio);
        }

        private void rileProcess_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            //int order = ConvertUtil.ToInt32(e.NewValue);

            //_currentRow.BeginEdit();
            //_currentRow["PUR_PROCESS"] = order;
            //setPurchInfo(bookCd, order);
            //_currentRow.EndEdit();
        }

        private void setPurchInfo(long bookCd, int purchCd, DataRow row)
        {
            setETCInfo(_shopCd, bookCd, purchCd, row);
        }

        private void setETCInfo(int shopCd, long bookCd, int purchCd, DataRow row)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("BOOKCD", bookCd);
            jobj.Add("PURCHCD", purchCd);

            string url = "/returns/getPurchaseRate.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    string key = $"{shopCd}/{bookCd}/{purchCd}";

                    DataTable dtOrderRatioTable;
                    dtOrderRatioTable = new DataTable();
                    dtOrderRatioTable.Columns.Add(new DataColumn("KEY", typeof(int)));
                    dtOrderRatioTable.Columns.Add(new DataColumn("VALUE", typeof(string)));
                    dtOrderRatioTable.Columns.Add(new DataColumn("RATE_KBN", typeof(int)));

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int rateKbn = -1;
                    int rate;
                    string col;

                    int firstRate = 0;
                    int firstRateKbn = 0;
                    int index = 1;

                    DataRow[] rows;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        rateKbn = ConvertUtil.ToInt32(obj["RATE_KBN"]);
                        rate = ConvertUtil.ToInt32(obj["RATE"]);

                        col = $"RATE{rateKbn}";

                        if (_dt.Columns.Contains(col))
                        {
                            row[$"RATE{rateKbn}"] = rate;
                            row[$"STOCK{rateKbn}"] = ConvertUtil.ToInt32(obj["STOCK"]);
                        }

                        rows = _dtOrderRatio.Select($"KEY = {rate}");

                        if (rows.Length == 0)
                        {
                            DataRow dr = _dtOrderRatio.NewRow();

                            dr["KEY"] = rate;
                            dr["VALUE"] = ConvertUtil.ToString(obj["RATE"]);

                            _dtOrderRatio.Rows.Add(dr);
                        }

                        DataRow dr1 = dtOrderRatioTable.NewRow();

                        dr1["KEY"] = rate;
                        dr1["VALUE"] = ConvertUtil.ToString(obj["RATE"]);
                        dr1["RATE_KBN"] = rateKbn;

                        dtOrderRatioTable.Rows.Add(dr1);

                        if(index == 1)
                        {
                            firstRateKbn = rateKbn;
                            firstRate = rate;
                            index++;
                        }
                    }

                    if(!_dicOrderRatioTable.ContainsKey(key))
                        _dicOrderRatioTable.Add(key, dtOrderRatioTable);

                    //_currentRow["RETURN_RATE"] = firstRate;
                }
            }
            else
            {
                //Dangol.Error(jResult["MSG"]);
            }
            
        }

        private void rileOrderRatio_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int rate = ConvertUtil.ToInt32(e.NewValue);

            DataRow[] rows = _dtRateKbn.Select($"KEY = {rate}");

            _currentRow.BeginEdit();
            if (rows.Length > 0)
                _currentRow["RATE_KBN"] = rows[0]["RATE_KBN"];
            else
                _currentRow["RATE_KBN"] = -1;

            _currentRow.EndEdit();
        }
    }
}
