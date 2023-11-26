using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
using WareHousingMaster.view.common.custom;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.payment
{
    public partial class PaymentTradingList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        DataTable _dtPurchCd;
        BindingSource _bsPruchCd;

        DataTable _dtOrderRatio;
        BindingSource _bsOrderRatio;


        JObject _jobj;


        int _shopCd;
        int _storeCd;
        int _groupType; // 조 구분
        int _groupCd;
        int _inpGroupCd;
        string _groupNm;
        int _purchType;
        int _purchCds;
        int _purchCde;
        string _purchNm;
        int _orderType;
        int _orderCondition;
        string _orderNotice;
        int _tradeItem;
        string _orderDt;
        string _inpDt;

        Dictionary<long, LookUpEdit> _dicLookUpEdit;
        Dictionary<long, DataTable> _dicPurchCdTable;
        Dictionary<string, DataTable> _dicOrderRatioTable;

        Dictionary<int, string> _dicPurchNm;
        //Dictionary<int, int> _dicPurchGroupCd;

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

        public delegate void RefreshHandler();
        public event RefreshHandler refreshHandler;


        public PaymentTradingList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("ID", typeof(int)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("STORE", typeof(int)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            
            _dt.Columns.Add(new DataColumn("TRADE_ITEM", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORD_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORDER_RATIO", typeof(double)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING_PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("INP_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("VCNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("PUBSHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("BARCODE_FG", typeof(int)));
            _dt.Columns.Add(new DataColumn("INP_TIME", typeof(string)));
            _dt.Columns.Add(new DataColumn("ISBN_FG", typeof(string)));
            
            //_dt.Columns.Add(new DataColumn("ORDER_CNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));  //0:default, 1:create, 2:available, 3:complete, -1:notavailable
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtPurchCd = new DataTable();

            _dtPurchCd.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
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
            _dicOrderRatioTable = new Dictionary<string, DataTable>();
            _dicPurchNm = new Dictionary<int, string>();
            //_dicPurchGroupCd = new Dictionary<int, int>();
        }


        public void setInitLoad()
        {
            setInfoBox();
            setinitialize();
        }

        private void setInfoBox()
        {
            //Util.LookupEditHelper(rilePurchCd, _dtPurchCd, "KEY", "VALUE");
            Util.LookupEditHelper(rileOrderRatio, _dtOrderRatio, "KEY", "VALUE");

            DataTable dtCondition = new DataTable();

            dtCondition.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCondition.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRow(dtCondition, 30, "30: 위탁매입");
            Util.insertRow(dtCondition, 31, "31: 현매매입");
            Util.insertRow(dtCondition, 32, "32: 위탁할인");
            Util.insertRow(dtCondition, 33, "33: 현매할인");
            Util.insertRow(dtCondition, 34, "34: 위탁반품");
            Util.insertRow(dtCondition, 35, "35: 현매반품");

            Util.insertRow(dtCondition, 20, "20: 위탁매입");
            Util.insertRow(dtCondition, 21, "21: 현매매입");
            Util.insertRow(dtCondition, 28, "28: 위탁반품");
            Util.insertRow(dtCondition, 29, "29: 현매반품");
            Util.insertRow(dtCondition, 25, "25: 위탁반송");
            Util.insertRow(dtCondition, 26, "26: 현매반송");

            Util.insertRow(dtCondition, 40, "40: 현매지불");
            Util.insertRow(dtCondition, 41, "41: 위탁지불");
            Util.insertRow(dtCondition, 42, "42: 매절지불");
            Util.insertRow(dtCondition, 43, "43: 조정지불");

            Util.LookupEditHelper(rileCondition, dtCondition, "KEY", "VALUE");
        }

        public void setinitialize()
        {
            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;

            _bsPruchCd.DataSource = _dtPurchCd;
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
            _purchCds = ConvertUtil.ToInt32(obj["PURCHCD_E"]);
            _purchCde = ConvertUtil.ToInt32(obj["PURCHCD_S"]);
        }

        public void setTableInitialize()
        {
            gvList.BeginDataUpdate();
            _dt.Clear();

            for (int i = 0; i < 30; i++)
            {
                DataRow dr = _dt.NewRow();

                dr["NO"] = i + 1;
                dr["ID"] = -1;

                dr["STATE"] = 0;        //  0:default, 1:create, 2:available, 3:complete, -1:notavailable
                dr["CHECK"] = false;    
                _dt.Rows.Add(dr);
            }

            gvList.EndDataUpdate();
        }

        public void setTableInitialize(JObject jobj)
        {
            _jobj = jobj;
            _shopCd = ConvertUtil.ToInt32(jobj["SHOPCD"]);
            _purchCds = ConvertUtil.ToInt32(jobj["PURCHCD_E"]);
            _purchCde = ConvertUtil.ToInt32(jobj["PURCHCD_S"]);

            JObject jResult = new JObject();

            string url = "/warehousing/getWarehousingBookList.json";

            int index = 0;

            gvList.BeginDataUpdate();
            _dt.Clear();

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        dr["NO"] = index + 1;

                        dr["ID"] = ConvertUtil.ToInt32(obj["SEQ_NO"]);
                        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                        dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                        dr["PRICE"] = ConvertUtil.ToInt32(obj["INP_PRICE"]);
                        dr["INP_CNT"] = ConvertUtil.ToInt32(obj["INP_COUNT"]);
                        dr["ORDER_RATIO"] = ConvertUtil.ToInt32(obj["INP_RATE"]);
                        dr["TRADE_ITEM"] = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
                        dr["PURCHCD"] = ConvertUtil.ToInt32(obj["PURCHCD"]);
                        dr["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
                        dr["BARCODE_FG"] = ConvertUtil.ToInt32(obj["BARCODE_FG"]);
                        dr["INP_TIME"] = ConvertUtil.ToString(obj["INP_TIME"]);
                        dr["ISBN_FG"] = ConvertUtil.ToInt32(obj["ISBN_FG"]);
                        
                        dr["WAREHOUSING_PRICE"] = ConvertUtil.ToInt32(obj["INP_COST"]);
                        dr["TOTAL_PRICE"] = ConvertUtil.ToInt32(obj["INP_COST"]) * ConvertUtil.ToInt32(obj["INP_COUNT"]);

                        setPreBookOrder(dr);

                        dr["ETC"] = ConvertUtil.ToString(obj["INP_TIME"]);
                        dr["STATE"] = 1; //0:default, 1:create, 2:available, 3:complete, -1:notavailable
                        dr["CHECK"] = false;
                        _dt.Rows.Add(dr);

                        index++;
                    }
                }
            }

            gvList.EndDataUpdate();
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

            focusedRowObjectChangeHandler(_currentRow);
        }
        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView View = sender as GridView;
            }
        }

        public bool insertOrder()
        {
            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("STATE <> 0");

            if (rows.Length < 1)
            {
                Dangol.Warining("주문건이 없습니다.");
                return false;
            }
            else
            {
                if (dataVerification())
                {
                    //if (checkOranChange())
                        if (Dangol.MessageYN("입고예정 데이터를 저장하시겠습니까?") == DialogResult.Yes)
                            return insertData();
                }
                else
                {
                    //if (checkOranChange())
                        if (Dangol.MessageYN("완료되지 않은 데이터가 있습니다. 그래도 진행하시겠습니까?") == DialogResult.Yes)
                            return insertData();
                }

                return false;
            }
        }

        public bool checkDataExist()
        {
            DataRow[] rows = _dt.Select("STATE IN (1, 2, 3)");

            return rows.Length > 0;
        }

        private bool dataVerification()
        {
            DataRow[] rows = _dt.Select("STATE IN (1, 2, 3)");

            long bookCd;
            int purchCd;
            int orderRatio;
            int orderCnt;

            int faultCnt = 0;

            foreach (DataRow row in rows)
            {
                bookCd = ConvertUtil.ToInt64(row["BOOKCD"]);
                purchCd = ConvertUtil.ToInt32(row["PURCHCD"]);
                orderRatio = ConvertUtil.ToInt32(row["ORDER_RATIO"]);
                orderCnt = ConvertUtil.ToInt32(row["INP_CNT"]);

                if (bookCd > 0 && purchCd > 0 && orderRatio > 0 && orderCnt > 0)
                    row["STATE"] = 2;
                else
                {
                    row["STATE"] = 1;
                    faultCnt++;
                }
            }

            return faultCnt == 0;
        }

        

        private bool checkOranChange()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/warehousing/checkHMA12HMA08_LOG.json";

            DataRow[] rows = _dt.Select("STATE = 2");

            foreach (DataRow row in rows)
            {
                jobj.RemoveAll();
                jobj.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    if(ConvertUtil.ToBoolean(jResult["EXIST"]))
                    {
                        string bookNm = ConvertUtil.ToString(row["BOOKNM"]);

                        Dangol.Warining($"{bookNm} 조직변경!");
                        return false;
                    }
                    
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                    return false;
                }
            }

            return false;
        }

        private bool insertData()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            DataRow[] rows = _dt.Select("STATE = 2");

            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("PURCHCD", _purchCds);

            foreach (DataRow row in rows)
            {
                JObject jdata = new JObject();
                jdata.Add("SHOPCD", _shopCd);
                jdata.Add("PURCHCD", _purchCds);
                jdata.Add("PUBSHCD", ConvertUtil.ToInt32(row["PUBSHCD"]));
                jdata.Add("PUBSHNM", ConvertUtil.ToString(row["PUBSHNM"]));
                jdata.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));
                jdata.Add("BOOKNM", ConvertUtil.ToString(row["BOOKNM"]));
                jdata.Add("INP_COUNT", ConvertUtil.ToInt32(row["INP_CNT"]));
                jdata.Add("INP_PRICE", ConvertUtil.ToInt32(row["PRICE"]));
                jdata.Add("INP_RATE", ConvertUtil.ToInt32(row["ORDER_RATIO"]));
                jdata.Add("INP_COST", ConvertUtil.ToInt32(row["WAREHOUSING_PRICE"]));
                jdata.Add("TRADE_ITEM", ConvertUtil.ToInt32(row["TRADE_ITEM"]));
                jdata.Add("BARCODE_FG", 1);
                jdata.Add("INP_ID", ProjectInfo._userId);
                jdata.Add("INP_NM", ProjectInfo._userName);
                jdata.Add("INP_TIME", DateTime.Today.ToString("yyMMdd"));

                if (ConvertUtil.ToInt32(row["VCNT"]) > 0)
                {
                    jdata.Add("VCNT_EXIST", 1);

                    jdata.Add("LD_SHOPCD", _shopCd);
                    jdata.Add("CHIT_NO", 99999999);
                    jdata.Add("CHIT_KBN", 80);
                    jdata.Add("SUB_SHOPCD", _shopCd);
                    jdata.Add("LD_PRICE", ConvertUtil.ToInt32(row["PRICE"]));
                    jdata.Add("LD_COUNT", ConvertUtil.ToInt32(row["VCNT"]));
                    jdata.Add("LD_AMOUNT", ConvertUtil.ToInt32(row["WAREHOUSING_PRICE"]) * ConvertUtil.ToInt32(row["VCNT"]));
                    jdata.Add("RATE", ConvertUtil.ToInt32(row["ORDER_RATIO"]));
                    jdata.Add("COST", ConvertUtil.ToInt32(row["WAREHOUSING_PRICE"]));
                    jdata.Add("PURCHNM", _purchNm);
                    jdata.Add("LD_TIME", DateTime.Now.ToString("yyyyMMddHHmmSS"));
                    jdata.Add("OUT_FG", 0);

                    //string date = ConvertUtil.ToString(row["WAREHOUSING_DT"]);
                    //jdata.Add("LD_TIME", date.Replace("-", ""));

                }

                jArray.Add(jdata);
            }

            jobj.Add("DATA", jArray);

            string url = "/warehousing/insertWarehousingBook.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                Dangol.Message($"{rows.Length}개의 데이터가 확정되었습니다.");
                return true;
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
                return false;
            }
        }
        public bool deleteData()
        {
            if(_currentRow == null)
            {
                Dangol.Warining("삭제할 item이 없습니다.");
                return false;
            }
            else
            {
                if (ConvertUtil.ToInt32(_currentRow["ID"]) < 0)
                {
                    Dangol.Warining("삭제할 수 없습니다. 관리자에게 문의하세요.");
                    return false;
                }

                if (Dangol.MessageYN("선택한 ITEM을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    var jArray = new JArray();

                    DataRow[] rows = _dt.Select("STATE = 2");

                    jobj.Add("SHOPCD", _shopCd);
                    jobj.Add("PURCHCD", ConvertUtil.ToInt32(_currentRow["PURCHCD"]));
                    jobj.Add("BOOKCD", ConvertUtil.ToInt64(_currentRow["BOOKCD"]));
                    jobj.Add("SEQ_NO", ConvertUtil.ToInt32(_currentRow["ID"]));

                    string url = "/warehousing/deleteWarehousingBook.json";

                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        gvList.BeginDataUpdate();
                        _currentRow.Delete();
                         gvList.EndDataUpdate();
                        Dangol.Message($"삭제되었습니다.");
                        return true;
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                        return false;
                    }
                }
                else
                    return false;
            }
        }

        public bool confirmData()
        {
            bool separate = true;
            using (CustomMessageBox messageBox = new CustomMessageBox("입력한시간에 따라 전표번호를 분할하시겠습니까?", "YES", "NO", "CANCEL"))
            {
                DialogResult result = messageBox.ShowDialog();

                if (result == DialogResult.Yes)
                    separate = true;
                else if (result == DialogResult.OK)
                    separate = false;
                else
                    return false;
            }

            //separate = Dangol.MessageYN("입력한시간에 따라 전표번호를 분할하시겠습니까?") == DialogResult.Yes;

            JObject jResult = new JObject();
            JObject jobj = new JObject();
           

            Dictionary<int, Dictionary<string, JArray>> dicData = new Dictionary<int, Dictionary<string, JArray>>();

            DataRow[] rows = _dt.Select("INP_CNT > -1 AND ORDER_RATIO > -1 ", "PURCHCD, INP_TIME");

            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("SEPARATE", separate ? 1 : 2);

            int purchCd = -1;
            string inpTime;
            JArray jArray;
            Dictionary<string, JArray> dicTime;

            foreach (DataRow row in rows)
            {
                purchCd = ConvertUtil.ToInt32(row["PURCHCD"]);
                inpTime = ConvertUtil.ToString(row["INP_TIME"]);

                JObject jdata = new JObject();
                jdata.Add("SHOPCD", _shopCd);
                jdata.Add("ROW_NO", ConvertUtil.ToInt32(row["NO"]));
                jdata.Add("PURCHCD", ConvertUtil.ToInt32(row["PURCHCD"]));
                jdata.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));
                jdata.Add("PUBSHCD", ConvertUtil.ToInt32(row["PUBSHCD"]));
                jdata.Add("INP_COUNT", ConvertUtil.ToInt32(row["INP_CNT"]));
                jdata.Add("INP_PRICE", ConvertUtil.ToInt32(row["PRICE"]));
                jdata.Add("INP_RATE", ConvertUtil.ToInt32(row["ORDER_RATIO"]));
                jdata.Add("INP_COST", ConvertUtil.ToInt32(row["WAREHOUSING_PRICE"]));
                jdata.Add("TRADE_ITEM", ConvertUtil.ToInt32(row["TRADE_ITEM"]));
                jdata.Add("BARCODE_FG", ConvertUtil.ToInt32(row["BARCODE_FG"]));
                jdata.Add("ISBN_FG", ConvertUtil.ToInt32(row["ISBN_FG"]));
                jdata.Add("INP_ID", ProjectInfo._userId);
                jdata.Add("INP_NM", ProjectInfo._userName);
                jdata.Add("INP_TIME", ConvertUtil.ToString(row["INP_TIME"]));

                if (dicData.ContainsKey(purchCd))
                {
                    dicTime = dicData[purchCd];
                    if (dicTime.ContainsKey(inpTime))
                    {
                        jArray = dicTime[inpTime];
                        jArray.Add(jdata);
                    }
                    else
                    {
                        var jArrayData = new JArray();
                        jArrayData.Add(jdata);

                        dicTime.Add(inpTime, jArrayData);
                    }
                }
                else
                {
                    var jArrayData = new JArray();
                    jArrayData.Add(jdata);

                    Dictionary<string, JArray> dicTimeData = new Dictionary<string, JArray>();
                    dicTimeData.Add(inpTime, jArrayData);
                    dicData.Add(purchCd, dicTimeData);
                }
            }

            var jArrayPurchase = new JArray();
            
            foreach (KeyValuePair<int, Dictionary<string, JArray>> item in dicData)
            {
                purchCd = item.Key;

                JObject jdata = new JObject();
                jdata.Add("PURCHCD", purchCd);
                var jArrayData = new JArray();

                foreach (KeyValuePair<string, JArray> item2 in item.Value)
                    jArrayData.Add(item2.Value);

                jdata.Add("DATAP", jArrayData);
                jArrayPurchase.Add(jdata);
            }
            
            jobj.Add("DATA", jArrayPurchase);

            string url = "/warehousing/confirmWarehousingBook.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                refreshHandler();
                setTableInitialize(_jobj);
                Dangol.Message($"{rows.Length}개의 데이터가 확정되었습니다.");
                return true;
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
                return false;
            }
        }


        public void clear()
        {
            _currentRow["NO"] = -1;
            _currentRow["BOOKCD"] = DBNull.Value;
            _currentRow["BOOKNM"] = DBNull.Value;
            _currentRow["PURCHCD"] = DBNull.Value;
            //_currentRow["WAREHOUSING_DT"] = DBNull.Value;
            _currentRow["TRADE_ITEM"] = DBNull.Value;

            _currentRow["ORD_CNT"] = DBNull.Value;
            _currentRow["PRICE"] = DBNull.Value;
            _currentRow["ORDER_RATIO"] = DBNull.Value;
            _currentRow["WAREHOUSING_PRICE"] = DBNull.Value;
            _currentRow["TOTAL_PRICE"] = DBNull.Value;
            _currentRow["INP_CNT"] = DBNull.Value;

            //_currentRow["VCNT"] = DBNull.Value;
            

            _currentRow["STATE"] = 0;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable

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
            gvList.RefreshData();
        }

        public bool saveReleaseReceiptItem()
        {
            bool isSuccess = false;

            return isSuccess;
        }
        public void SetFocus()
        {
            gvList.Focus();
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

                    if (e.Column.FieldName == "PURCHCD")
                    {
                        _currentRow["PURCHCD"] = e.Value;
                    }
                }
            }
        }

        private void riteBookCd_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    TextEdit textEditor = (TextEdit)sender;
            //    string data = textEditor.Text;
            //    long bookCd = ConvertUtil.ToInt64(data);

            //    if (bookCd < 1)
            //    {
            //        Dangol.Warining("도서코드를 입력하세요");
            //    }
            //    else
            //    {
            //        if(getBookList(bookCd, ""))
            //            SetColFocus("TRADE_ITEM", gvList.FocusedRowHandle);

            //        //this.gvList.PostEditor();

            //        //this.gvList.SetFocusedRowCellValue("ORDER_CNT", null);
            //    }
            //}
        }

        private void riteTitle_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    TextEdit textEditor = (TextEdit)sender;
            //    string title = textEditor.Text;

            //    //if (string.IsNullOrWhiteSpace(title))
            //    //{
            //    //    Dangol.Warining("도서명을 입력하세요");
            //    //}
            //    //else
            //    //{
            //        if(getBookList(0, title))
            //            SetColFocus("TRADE_ITEM", gvList.FocusedRowHandle);

            //        //this.gvList.PostEditor();
            //        //this.gvList.SetFocusedRowCellValue("ORDER_CNT", null);
            //    //}
            //}
        }

   
        private bool setPreBookOrder(DataRow row)
        {
            long bookCd = ConvertUtil.ToInt64(row["BOOKCD"]);
            string bookNm = ConvertUtil.ToString(row["BOOKNM"]);

            //JObject jData = getSearchInfoHandler();

            //int shopCd = ConvertUtil.ToInt32(jData["SHOPCD"]);
            long prePurchCd = ConvertUtil.ToInt32(row["PURCHCD"]);

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            jobj.Add("BOOKCD", bookCd);
            jobj.Add("SHOPCD", _shopCd);

            string url = "/search/getBookList.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    //foreach (JObject obj in jArray.Children<JObject>())
                    //{
                    //    insertOrderBook(row, obj);
                    //    break;
                    //}

                    if (!_dicLookUpEdit.ContainsKey(bookCd))
                    {
                        url = "/search/getPurchaseList4Order.json";

                        if (DBConnect.getRequest(jobj, ref jResult, url))
                        {
                            if (Convert.ToBoolean(jResult["EXIST"]))
                            {
                                DataTable dtPurchCd;
                                dtPurchCd = new DataTable();
                                dtPurchCd.Columns.Add(new DataColumn("KEY", typeof(int)));
                                dtPurchCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

                                jArray = JArray.Parse(jResult["DATA"].ToString());

                                DataRow[] rows;
                                int purchCdT;
                                string purchNmT;
                                //int ordGroupCd;

                                //long firstPurchCd = -1;
                                int index = 1;
                                long purchCd;

                                foreach (JObject obj in jArray.Children<JObject>())
                                {
                                    purchCd = ConvertUtil.ToInt64(obj["PURCHCD"]);

                                    rows = _dtPurchCd.Select($"BOOKCD = {bookCd} AND KEY = {purchCd}");
                                    if (rows.Length == 0)
                                    {
                                        purchCdT = ConvertUtil.ToInt32(obj["PURCHCD"]);
                                        purchNmT = ConvertUtil.ToString(obj["PURCHNM"]);
                                        //ordGroupCd = ConvertUtil.ToInt32(obj["ORD_GROUPCD"]);

                                        DataRow dr = _dtPurchCd.NewRow();

                                        dr["BOOKCD"] = bookCd;
                                        dr["KEY"] = purchCdT;
                                        dr["VALUE"] = purchNmT;

                                        _dtPurchCd.Rows.Add(dr);

                                        DataRow dr1 = dtPurchCd.NewRow();

                                        dr["BOOKCD"] = bookCd;
                                        dr1["KEY"] = purchCdT;
                                        dr1["VALUE"] = purchNmT;

                                        dtPurchCd.Rows.Add(dr1);

                                        if (!_dicPurchNm.ContainsKey(purchCdT))
                                            _dicPurchNm.Add(purchCdT, purchNmT);

                                        //if (!_dicPurchGroupCd.ContainsKey(purchCdT))
                                        //    _dicPurchGroupCd.Add(purchCdT, ordGroupCd);
                                    }

                                    if (index == 1)
                                    {
                                        //firstPurchCd = purchCd;
                                        index++;
                                    }
                                }

                                LookUpEdit editor = new LookUpEdit();
                                Util.LookupEditHelper(editor, dtPurchCd, "KEY", "VALUE");
                                _dicLookUpEdit.Add(bookCd, editor);
                                _dicPurchCdTable.Add(bookCd, dtPurchCd);

                                //row["PURCHCD"] = _purchCd;

                                //getBookOrdInfo(bookCd, ConvertUtil.ToInt32(_purchCd));

                                return getOrderRatio(row);
                            }
                            else
                            {
                                //Dangol.Warining($"도서코드['{bookCd}']에 대한 매입처가 없습니다. 주문할수없습니다.");
                                //_currentRow["STATE"] = -1;
                                return false;

                            }
                        }
                        else
                            return false;
                    }
                    else
                    {
                        //DataTable dtPurchcd = _dicPurchCdTable[bookCd];
                        //if (dtPurchcd.Rows.Count > 0)
                        //{
                        //    //long purchCd = ConvertUtil.ToInt64(dtPurchcd.Rows[0]["KEY"]);
                        //    row["PURCHCD"] = _purchCd;

                        //    getOrderRatio(row);
                        //}
                        return true;
                    }
                }
                else
                {
                    //Dangol.Warining($"도서코드 ['{bookCd}']가 점별도서테이블에 존재하지 않습니다.");
                    return false;
                }
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
                return false;
            }
        }

        private void insertOrderBook(DataRowView obj)
        {
            _currentRow.BeginEdit();

            _currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            riteTitle.BeginUpdate();
            _currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
            riteTitle.EndUpdate();
            _currentRow["WAREHOUSING_DT"] = DateTime.Now.ToString("yyyy/MM/dd");
            _currentRow["TRADE_ITEM"] = 1;
            _currentRow["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

            _currentRow["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
            _currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);

            //_currentRow["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
            //_currentRow["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);

            //_currentRow["INP_CNT"] = DBNull.Value;
            //_currentRow["RETURN_CNT"] = DBNull.Value;

            _currentRow["ORD_CNT"] = DBNull.Value;
            //_currentRow["ESTI_CNT"] = DBNull.Value;

            //_currentRow["STOCK_CNT"] = ConvertUtil.ToInt32(obj["STOCK"]);
            _currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable

            _currentRow.EndEdit();

        }

        private void insertOrderBook(JObject obj)
        {
            _currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            _currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
            _currentRow["WAREHOUSING_DT"] = DateTime.Now.ToString("yyyy/MM/dd");
            _currentRow["TRADE_ITEM"] = 1;
            _currentRow["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

            _currentRow["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
            _currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
            //_currentRow["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
            //_currentRow["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);

            //_currentRow["INP_CNT"] = DBNull.Value;
            //_currentRow["RETURN_CNT"] = DBNull.Value;

            _currentRow["ORD_CNT"] = DBNull.Value;
            //_currentRow["ESTI_CNT"] = DBNull.Value;

            //_currentRow["STOCK_CNT"] = ConvertUtil.ToInt32(obj["STOCK"]);
            _currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable
        }

        private void insertOrderBook(DataRow row, JObject obj)
        {
            row["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            row["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
            row["WAREHOUSING_DT"] = ConvertUtil.ToString(obj["WAREHOUSING_DT"]);
            row["TRADE_ITEM"] = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
           
            row["STORE"] = $"{obj["STORECD"]}/{obj["INP_GROUPCD"]}";

            row["ORDER_CNT"] = ConvertUtil.ToInt32(obj["INP_PLAN_COUNT"]);
            row["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

            row["ORDER_RATIO"] = ConvertUtil.ToInt32(obj["INP_PLAN_RATE"]);
            row["WAREHOUSING_PRICE"] = ConvertUtil.ToInt32(obj["INP_PLAN_COST"]);
            row["TOTAL_PRICE"] = ConvertUtil.ToInt32(obj["INP_PLAN_COST"]) * ConvertUtil.ToInt32(obj["INP_PLAN_COUNT"]);

            row["INP_CNT"] = ConvertUtil.ToInt32(obj["INP_PLAN_COUNT"]);

            row["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
            row["PUBSHNM"] = ConvertUtil.ToInt32(obj["PUBSHNM"]);

            row["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable
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

            if (view.FocusedColumn.FieldName == "PURCHCD")
            {
                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                long bookCd = ConvertUtil.ToInt64(view.GetFocusedRowCellValue("BOOKCD"));
                if (_dicLookUpEdit.ContainsKey(bookCd))
                    editor.Properties.DataSource = _dicPurchCdTable[bookCd];
                else
                    editor.Properties.DataSource = null;
            }
            else if (view.FocusedColumn.FieldName == "ORDER_RATIO")
            {
                getOrderRatio(false);

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

        private void rilePurchCd_EditValueChanged(object sender, EventArgs e)
        {
            //int purchCd = ConvertUtil.ToInt32(gvList.GetFocusedRowCellValue("PURCHCD"));

            LookUpEdit lookUpEdit = (LookUpEdit)sender;
            var purchCd = lookUpEdit.EditValue;

            getBookOrdInfo(ConvertUtil.ToInt64(_currentRow["BOOKCD"]), ConvertUtil.ToInt32(purchCd));
        }
        private bool getOrderRatio(bool isInit = true)
        { 
            long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("BOOKCD", bookCd);
            jobj.Add("PURCHCD", purchCd);
            jobj.Add("SHOPCD", _shopCd);

            string key = $"{_shopCd}/{bookCd}/{purchCd}";

            if (_dicOrderRatioTable.ContainsKey(key))
            {
                if (isInit)
                {
                    DataTable dtRatio = _dicOrderRatioTable[key];
                    if (dtRatio.Rows.Count > 0)
                    {
                        _currentRow.BeginEdit();
                        _currentRow["ORDER_RATIO"] = ConvertUtil.ToInt32(dtRatio.Rows[0]["KEY"]);
                        _currentRow.EndEdit();
                    }
                }

                return true;
            }

            if (bookCd > 0 && purchCd > 0)
            {
                jobj.Add("RATE_KBN_S", -1);
                jobj.Add("RATE_KBN_E", 120);

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

                        int rate = 0;
                        DataRow[] rows;

                        int firstRate = -1;
                        int index = 1;

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            rate = ConvertUtil.ToInt32(obj["RATE"]);

                            rows = _dtOrderRatio.Select($"KEY = {rate}");

                            if (rows.Length == 0)
                            {
                                DataRow dr = _dtOrderRatio.NewRow();

                                //dr["CD"] = key;
                                dr["KEY"] = rate;
                                dr["VALUE"] = ConvertUtil.ToString(rate);

                                _dtOrderRatio.Rows.Add(dr);
                            }

                            DataRow dr1 = dtOrderRatioTable.NewRow();

                            //dr1["CD"] = bookCd;
                            dr1["KEY"] = rate;
                            dr1["VALUE"] = ConvertUtil.ToString(rate);

                            dtOrderRatioTable.Rows.Add(dr1);

                            if (index == 1)
                            {
                                firstRate = rate;
                                index++;
                            }

                        }

                        _dicOrderRatioTable.Add(key, dtOrderRatioTable);

                        if (isInit)
                        {
                            _currentRow.BeginEdit();
                            _currentRow["ORDER_RATIO"] = firstRate;
                            _currentRow.EndEdit();
                        }

                        return true;
                    }
                    else
                    {
                        Dangol.Warining($"도서코드 ['{bookCd}']에 {_orderNotice} 해당하는 매입율이 없습니다. 주문할 수 없습니다.");
                        return false;
                    }
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool getOrderRatio(DataRow row)
        {
            long bookCd = ConvertUtil.ToInt64(row["BOOKCD"]);
            int purchCd = ConvertUtil.ToInt32(row["PURCHCD"]);

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("BOOKCD", bookCd);
            jobj.Add("PURCHCD", purchCd);
            jobj.Add("SHOPCD", _shopCd);

            string key = $"{_shopCd}/{bookCd}/{purchCd}";

            if (_dicOrderRatioTable.ContainsKey(key))
            {
                DataTable dtRatio = _dicOrderRatioTable[key];
                if(dtRatio.Rows.Count > 0)
                    row["ORDER_RATIO"] = ConvertUtil.ToInt32(dtRatio.Rows[0]["KEY"]);

                return true;
            }

            if (bookCd > 0 && purchCd > 0)
            {
                jobj.Add("RATE_KBN_S", -1);
                jobj.Add("RATE_KBN_E", 120);
                
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

                        int rate = 0;
                        DataRow[] rows;

                        int firstRate = -1;
                        int index = 1;

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            rate = ConvertUtil.ToInt32(obj["RATE"]);

                            rows = _dtOrderRatio.Select($"KEY = {rate}");

                            if (rows.Length == 0)
                            {
                                DataRow dr = _dtOrderRatio.NewRow();

                                //dr["CD"] = key;
                                dr["KEY"] = rate;
                                dr["VALUE"] = ConvertUtil.ToString(rate);

                                _dtOrderRatio.Rows.Add(dr);
                            }

                            DataRow dr1 = dtOrderRatioTable.NewRow();

                            //dr1["CD"] = bookCd;
                            dr1["KEY"] = rate;
                            dr1["VALUE"] = ConvertUtil.ToString(rate);

                            dtOrderRatioTable.Rows.Add(dr1);

                            if (index == 1)
                            {
                                firstRate = rate;
                                index++;
                            }

                        }

                        _dicOrderRatioTable.Add(key, dtOrderRatioTable);

                        row["ORDER_RATIO"] = firstRate;

                        return true;
                    }
                    else
                    {
                        //Dangol.Warining($"도서코드 ['{bookCd}']에 {_orderNotice} 해당하는 매입율이 없습니다. 주문할 수 없습니다.");
                        return false;
                    }

                }
                else
                {
                    //Dangol.Error(jResult["MSG"]);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void setOrderCount(long bookCd, int purchCd, int orderRate)
        {
            if (_groupType == 2)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("SHOPCD", _shopCd);
                jobj.Add("STORECD", _storeCd);

                if(_groupCd > 0)
                    jobj.Add("GROUPCD", _groupCd);
                //if (!string.IsNullOrEmpty(_groupNm))
                    //jobj.Add("GROUPNM", _groupNm);

                jobj.Add("BOOKCD", bookCd);
                jobj.Add("PURCHCD", purchCd);
                jobj.Add("ORDER_RATE", orderRate);

                string url = "/search/getOrderBookCntInfo.json";

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    //if (Convert.ToBoolean(jResult["EXIST"]))
                    //{

                    //}

                    _currentRow["INP_CNT"] = ConvertUtil.ToInt32(jResult["INP_CNT"]);
                    _currentRow["RETURN_CNT"] = ConvertUtil.ToInt32(jResult["RETURN_CNT"]);
                    _currentRow["ORD_CNT"] = ConvertUtil.ToInt32(jResult["ORD_CNT"]);
                    _currentRow["ESTI_CNT"] = ConvertUtil.ToInt32(jResult["ESTI_CNT"]);
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                }
            }
        }

        private void getBookOrdInfo(long bookCd, int purchCd)
        {
            if (bookCd > 0 && purchCd > 0)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("SHOPCD", _shopCd);
                jobj.Add("BOOKCD", bookCd);
                jobj.Add("STORECD", _storeCd);
                jobj.Add("INP_GROUPCD", _inpGroupCd);
                jobj.Add("ORD_DATE", _orderDt);
                jobj.Add("TRADE_ITEM", _tradeItem);
                jobj.Add("PURCHCD", purchCd);

                string url = "/order/getBookOrderInfo.json";

                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    int cnt;
                    _currentRow.BeginEdit();
                    cnt = ConvertUtil.ToInt32(jResult["INP_CNT"]);
                    if (cnt != 0) _currentRow["INP_CNT"] = cnt;
                    else _currentRow["INP_CNT"] = DBNull.Value;

                    //cnt = ConvertUtil.ToInt32(jResult["RET_CNT"]);
                    //if (cnt != 0) _currentRow["RETURN_CNT"] = cnt;
                    //else _currentRow["RETURN_CNT"] = DBNull.Value;

                    cnt = ConvertUtil.ToInt32(jResult["ORD_CNT"]);
                    if (cnt != 0) _currentRow["ORD_CNT"] = cnt;
                    else _currentRow["ORD_CNT"] = DBNull.Value;

                    //cnt = ConvertUtil.ToInt32(jResult["ESTI_CNT"]);
                    //if (cnt != 0) _currentRow["ESTI_CNT"] = cnt;
                    //else _currentRow["ESTI_CNT"] = DBNull.Value;
                    _currentRow.EndEdit();
                }
                else
                {
                    _currentRow.BeginEdit();
                    _currentRow["INP_CNT"] = DBNull.Value;
                    //_currentRow["RETURN_CNT"] = DBNull.Value;

                    _currentRow["ORD_CNT"] = DBNull.Value;
                    //_currentRow["ESTI_CNT"] = DBNull.Value;

                    //_currentRow["STOCK_CNT"] = DBNull.Value;
                    _currentRow.EndEdit();
                }
            }
            else
            {
                _currentRow.BeginEdit();
                _currentRow["INP_CNT"] = DBNull.Value;
                //_currentRow["RETURN_CNT"] = DBNull.Value;

                _currentRow["ORD_CNT"] = DBNull.Value;
                //_currentRow["ESTI_CNT"] = DBNull.Value;

                //_currentRow["STOCK_CNT"] = DBNull.Value;
                _currentRow.EndEdit();
            }
        }
        private void rileOrderRatio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int price = ConvertUtil.ToInt32(_currentRow["PRICE"]);
                int orderRatio = ConvertUtil.ToInt32(_currentRow["ORDER_RATIO"]);

                int wPrice = price * orderRatio / 100;
                _currentRow["WAREHOUSING_PRICE"] = wPrice;

                int inpCnt = ConvertUtil.ToInt32(_currentRow["INP_CNT"]);
                _currentRow["TOTAL_PRICE"] = wPrice * inpCnt;

                SetColFocus("INP_CNT", gvList.FocusedRowHandle + 1);
            }
        }
        private void rileOrderRatio_EditValueChanged(object sender, EventArgs e)
        {
            //long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            //int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);
            //setOrderCount(bookCd, purchCd, orderRatio);


            int price = ConvertUtil.ToInt32(_currentRow["PRICE"]);
            int orderRatio = ConvertUtil.ToInt32(_currentRow["ORDER_RATIO"]);

            int wPrice = price * orderRatio / 100;
            _currentRow["WAREHOUSING_PRICE"] = wPrice;

            int inpCnt = ConvertUtil.ToInt32(_currentRow["INP_CNT"]);
            _currentRow["TOTAL_PRICE"] = wPrice * inpCnt;

            SetColFocus("INP_CNT", gvList.FocusedRowHandle + 1);
        }

        private void gcList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                e.Handled = true;
            }
        }

        private void riseCnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowHandle = gvList.FocusedRowHandle;

                if (rowHandle < 29)
                {
                    string bookNm = gvList.GetDataRow(rowHandle + 1)["BOOKNM"].ToString();

                    if(string.IsNullOrWhiteSpace(bookNm))
                        SetColFocus("BOOKNM", rowHandle + 1);
                    else
                        SetColFocus("ORDER_CNT", rowHandle + 1);
                }
            }
        }
        private void rileCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SetColFocus("ORDER_RATIO", gvList.FocusedRowHandle);
        }

        private void rileCondition_EditValueChanged(object sender, EventArgs e)
        {
            SetColFocus("ORDER_RATIO", gvList.FocusedRowHandle);
        }

        private void riseInpCnt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //SpinEdit editor = (SpinEdit)sender;

            int inpCnt = ConvertUtil.ToInt32(e.NewValue);
            int wPrice = ConvertUtil.ToInt32(_currentRow["WAREHOUSING_PRICE"]);
            _currentRow["TOTAL_PRICE"] = wPrice * inpCnt;
        }

        private void riseInpCnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //SpinEdit editor = (SpinEdit)sender;
                //int inpCnt = ConvertUtil.ToInt32(editor.EditValue);
                //int wPrice = ConvertUtil.ToInt32(_currentRow["WAREHOUSING_PRICE"]);
                //_currentRow["TOTAL_PRICE"] = wPrice * inpCnt;

                SetColFocus("ORD_RATIO", gvList.FocusedRowHandle);
            }
        }

        private void rileVCnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowHandle = gvList.FocusedRowHandle;

                if (rowHandle < 29)
                {
                    string bookNm = gvList.GetDataRow(rowHandle + 1)["BOOKNM"].ToString();

                    if (string.IsNullOrWhiteSpace(bookNm))
                        SetColFocus("BOOKNM", rowHandle + 1);
                    else
                        SetColFocus("TRADE_ITEM", rowHandle + 1);
                }
            }
        }

        public DataTable getDataTable()
        {
            return _dt;
        }

    }
}
