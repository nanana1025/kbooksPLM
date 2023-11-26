using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.custom;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.warehouisng
{
    public partial class WarehousingBarcodeList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        DataTable _dtPurchCd;
        BindingSource _bsPruchCd;

        DataTable _dtOrderRatio;
        BindingSource _bsOrderRatio;
        DataTable _dtOrderRatioAll;


        JObject _jobj;

        int _shopCd;
        int _storeCd;
        int _groupType; // 조 구분
        int _groupCd;
        int _inpGroupCd;
        string _groupNm;
        int _purchType;
        int _purchCd;
        int _purchCds;
        int _purchCde;
        string _purchNm;
        int _orderType;
        int _orderCondition;
        string _orderNotice;
        int _tradeItem;
        string _orderDt;
        string _inpDt;

        long _selectedPurchCd;

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
        int _bookType;

        public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;

        public delegate void RefreshHandler();
        public event RefreshHandler refreshHandler;


        public WarehousingBarcodeList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("ID", typeof(int)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("AUTHORNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("PURCHNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("PUBSHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));

            _dt.Columns.Add(new DataColumn("INP_CNT", typeof(int)));
            
            _dt.Columns.Add(new DataColumn("INP_COUNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("INP_PRICE", typeof(int)));

            _dt.Columns.Add(new DataColumn("RATE_KBN", typeof(int)));
            _dt.Columns.Add(new DataColumn("INP_RATE", typeof(int)));


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

            _dtOrderRatioAll = new DataTable();

            _dtOrderRatioAll.Columns.Add(new DataColumn("KEY", typeof(int)));
            _dtOrderRatioAll.Columns.Add(new DataColumn("VALUE", typeof(string)));

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
            Util.LookupEditHelper(rilePurchCd, _dtPurchCd, "KEY", "VALUE");
            Util.LookupEditHelper(rileOrderRatio, _dtOrderRatio, "KEY", "VALUE");

            DataTable dtCondition = new DataTable();

            dtCondition.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCondition.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRow(dtCondition, 11, "11:위탁1");
            Util.insertRow(dtCondition, 12, "12:위탁2");
            Util.insertRow(dtCondition, 13, "13:위탁3");
            Util.insertRow(dtCondition, 14, "14:위탁4");
            Util.insertRow(dtCondition, 21, "21:현매1");
            Util.insertRow(dtCondition, 22, "22:현매2");
            Util.insertRow(dtCondition, 23, "23:현매3");
            Util.insertRow(dtCondition, 24, "24:현매4");

            Util.LookupEditHelper(rileCondition, dtCondition, "KEY", "VALUE");

            for(int i = 0; i < 101; i++)
                Util.insertRow(_dtOrderRatioAll, i, i.ToString());
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

        public void setGreidView(bool readOnly)
        {
            gcBookCd.OptionsColumn.ReadOnly = readOnly;
            gcBookNm.OptionsColumn.ReadOnly = readOnly;
            gcPurchCd.OptionsColumn.ReadOnly = readOnly;
            gcPurProcess.OptionsColumn.ReadOnly = readOnly;
            gcRateKbn.OptionsColumn.ReadOnly = readOnly;


            if(readOnly)
            {
                gcBookCd.AppearanceCell.BackColor = System.Drawing.Color.Silver;
                gcBookNm.AppearanceCell.BackColor = System.Drawing.Color.Silver;
                gcPurchCd.AppearanceCell.BackColor = System.Drawing.Color.Silver;
                gcPurProcess.AppearanceCell.BackColor = System.Drawing.Color.Silver;
                gcRateKbn.AppearanceCell.BackColor = System.Drawing.Color.Silver;

            }
            else
            {
                gcBookCd.AppearanceCell.BackColor = System.Drawing.Color.Transparent;
                gcBookNm.AppearanceCell.BackColor = System.Drawing.Color.Transparent;
                gcPurchCd.AppearanceCell.BackColor = System.Drawing.Color.Transparent;
                gcPurProcess.AppearanceCell.BackColor = System.Drawing.Color.Transparent;
                gcRateKbn.AppearanceCell.BackColor = System.Drawing.Color.Transparent;
            }
        }

        public void setCondition(JObject obj, int bookType, int processType)
        {
            _jobj = obj;
            _shopCd = ConvertUtil.ToInt32(obj["SHOPCD"]);
            _purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);
            _bookType = bookType;
            _processType = processType;

            _dtOrderRatio.Clear();
            bool readOnly = true;

            if(_bookType == 1)
                readOnly = processType == 1 || processType == 2;
                
            setGreidView(readOnly);

            if (readOnly)
                Util.LookupEditHelper(rileOrderRatio, _dtOrderRatioAll, "KEY", "VALUE");
            else
                Util.LookupEditHelper(rileOrderRatio, _dtOrderRatio, "KEY", "VALUE");

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
            _purchCd = ConvertUtil.ToInt32(jobj["PURCHCD"]);
            //_purchCde = ConvertUtil.ToInt32(jobj["PURCHCD_S"]);

            JObject jResult = new JObject();

            string url = "/warehousing/getWarehousingBookList4Barcode.json";

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

                        //dr["ID"] = ConvertUtil.ToInt32(obj["SEQ_NO"]);
                        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                        dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                        dr["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
                        
                       
                        dr["PURCHNM"] = ConvertUtil.ToString(obj["PURCHNM"]);

                        dr["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
                        dr["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);

                        dr["INP_PRICE"] = ConvertUtil.ToInt32(obj["INP_PRICE"]);
                        dr["INP_COUNT"] = ConvertUtil.ToInt32(obj["INP_COUNT"]);

                        dr["ETC"] = "";
                        dr["STATE"] = 1; //0:default, 1:create, 2:available, 3:complete, -1:notavailable
                        dr["CHECK"] = false;
                      

                        setPreBookOrder(dr);

                        dr["PURCHCD"] = ConvertUtil.ToInt32(obj["PURCHCD"]);
                        dr["INP_RATE"] = ConvertUtil.ToInt32(obj["INP_RATE"]);
                        dr["RATE_KBN"] = ConvertUtil.ToInt32(obj["RATE_KBN"]);

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
                _selectedPurchCd = ConvertUtil.ToInt64(_currentRow["PURCHCD"]);
            }
            else
            {
                _currentRow = null;
                _selectedPurchCd = -1;
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
                orderRatio = ConvertUtil.ToInt32(row["INP_RATE"]);
                orderCnt = ConvertUtil.ToInt32(row["INP_COUNT"]);

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
                jdata.Add("INP_COUNT", ConvertUtil.ToInt32(row["INP_COUNT"]));
                jdata.Add("INP_PRICE", ConvertUtil.ToInt32(row["INP_PRICE"]));
                jdata.Add("INP_RATE", ConvertUtil.ToInt32(row["INP_RATE"]));
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
                    jdata.Add("LD_PRICE", ConvertUtil.ToInt32(row["INP_PRICE"]));
                    jdata.Add("LD_COUNT", ConvertUtil.ToInt32(row["VCNT"]));
                    jdata.Add("LD_AMOUNT", ConvertUtil.ToInt32(row["WAREHOUSING_PRICE"]) * ConvertUtil.ToInt32(row["VCNT"]));
                    jdata.Add("RATE", ConvertUtil.ToInt32(row["INP_RATE"]));
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

                    gvList.BeginDataUpdate();
                    clear();
                    gvList.EndDataUpdate();

                    return true;

                    //JObject jResult = new JObject();
                    //JObject jobj = new JObject();
                    //var jArray = new JArray();

                    //DataRow[] rows = _dt.Select("STATE = 2");

                    //jobj.Add("SHOPCD", _shopCd);
                    //jobj.Add("PURCHCD", ConvertUtil.ToInt32(_currentRow["PURCHCD"]));
                    //jobj.Add("BOOKCD", ConvertUtil.ToInt64(_currentRow["BOOKCD"]));
                    //jobj.Add("SEQ_NO", ConvertUtil.ToInt32(_currentRow["ID"]));

                    //string url = "/warehousing/deleteWarehousingBook.json";

                    //if (DBConnect.getRequest(jobj, ref jResult, url))
                    //{
                    //    gvList.BeginDataUpdate();
                    //    _currentRow.Delete();
                    //     gvList.EndDataUpdate();
                    //    Dangol.Message($"삭제되었습니다.");
                    //    return true;
                    //}
                    //else
                    //{
                    //    Dangol.Error(jResult["MSG"]);
                    //    return false;
                    //}
                }
                else
                    return false;
            }
        }

        public bool barcodePrint()
        {
            if (_currentRow == null)
            {
                Dangol.Warining("발행할 item이 없습니다.");
                return false;
            }
            else
            {
                if (ConvertUtil.ToInt32(_currentRow["ID"]) < 0)
                {
                    Dangol.Warining("발행할 수 없습니다. 관리자에게 문의하세요.");
                    return false;
                }

                if (dataVerification())
                { 
                    DataRow[] rows = _dt.Select("STATE = 2");

                    if (Dangol.MessageYN("ITEM을 발행하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();
                        JObject jobj = new JObject();
                        var jArrayFg = new JArray();
                        var jArrayData = new JArray();

                        string url = "/warehousing/updateWarehousingBookList4Barcode.json";

                        string date = ConvertUtil.ToString(_jobj["PUR_DATE"]);
                        string lblType = "LBLTYPEB";
                        if (_bookType == 1)
                            lblType = "LBLTYPEB";
                        else
                            lblType = "LBLTYPEA";

                        string sval;
                        int val;

                        string bookNm;
                        string bookCd;
                        string meipRate;
                        string meipSun;
                        string meipKbn;

                        foreach(DataRow row in rows)
                        {
                            JObject jdataFg = new JObject();

                            JObject jdata = new JObject();
                            bookCd = ConvertUtil.ToString(row["BOOKCD"]);
                            bookNm = ConvertUtil.ToString(row["BOOKNM"]);
                            val = ConvertUtil.ToInt32(row["INP_RATE"]);

                            jdataFg.Add("SHOPCD", _shopCd);
                            jdataFg.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));
                            jdataFg.Add("PURCHCD", ConvertUtil.ToInt32(row["PURCHCD"]));
                            jdataFg.Add("PUR_DATE", date);

                            jArrayFg.Add(jdataFg);

                            if (val > 94)
                                meipRate = "J";
                            else if (val > 89)
                                meipRate = "I";
                            else if (val > 84)
                                meipRate = "M";
                            else if (val > 79)
                                meipRate = "K";
                            else if (val > 74)
                                meipRate = "F";
                            else if (val > 69)
                                meipRate = "E";
                            else if (val > 64)
                                meipRate = "D";
                            else if (val > 59)
                                meipRate = "C";
                            else if (val > 54)
                                meipRate = "B";
                            else if (val > 49)
                                meipRate = "A";
                            else if (val > 44)
                                meipRate = "O";
                            else if (val > 39)
                                meipRate = "*";
                            else
                            {
                                meipRate = "*";
                                meipSun = "*";
                                meipKbn = "**";
                            }

                            bookNm = bookNm.Substring(0, 15);
                            bookNm = bookNm.PadRight(30);
                            jdata.Add("BOOKNM", bookNm);

                            bookCd = bookCd.PadRight(13, '0');
                            jdata.Add("BOOKCD", bookCd);

                            sval = ConvertUtil.ToString(_shopCd);
                            sval = sval.PadRight(2, '0');
                            jdata.Add("SHOPCD", sval);

                            jdata.Add("INP_RATE", meipRate);

                            sval = ConvertUtil.ToString(row["RATE_KBN"]);
                            sval = sval.PadRight(2, '0');
                            jdata.Add("RATE_KBN", sval);

                            sval = ConvertUtil.ToString(row["PURCHNM"]);
                            sval = sval.Substring(0, 10);
                            sval = sval.PadRight(20);
                            jdata.Add("PURCHNM", sval);

                            jdata.Add("INP_PRICE", string.Format(new CultureInfo("ko-KR"), "{0:C0}", ConvertUtil.ToInt32(row["INP_PRICE"])));
                            jdata.Add("PUR_DATE", date);
                            
                            sval = ConvertUtil.ToString(row["INP_COUNT"]);
                            sval = sval.PadRight(4);
                            jdata.Add("INP_COUNT", sval);


                            jArrayData.Add(jdata);
                        }

                        jobj.Add("DATA", jArrayFg);


                        if (DBConnect.getRequest(jobj, ref jResult, url))
                        {

                        }
                        else
                        {
                            Dangol.Error(jResult["MSG"]);
                            return false;
                        }




                            return true;
                    }
                    else
                        return false;
                }
                else
                {
                    Dangol.Warining("발행 가능한 데이터가 없습니다.데이터를 확인하세요.");
                    return false;
                }

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

            DataRow[] rows = _dt.Select("INP_COUNT > -1 AND INP_RATE > -1 ", "PURCHCD, INP_TIME");

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
                jdata.Add("INP_COUNT", ConvertUtil.ToInt32(row["INP_COUNT"]));
                jdata.Add("INP_PRICE", ConvertUtil.ToInt32(row["INP_PRICE"]));
                jdata.Add("INP_RATE", ConvertUtil.ToInt32(row["INP_RATE"]));
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
            _currentRow["ID"] = -1;
            _currentRow["BOOKCD"] = DBNull.Value;
            _currentRow["BOOKNM"] = DBNull.Value;
            _currentRow["AUTHORNM"] = DBNull.Value;
            //_currentRow["WAREHOUSING_DT"] = DBNull.Value;
            _currentRow["PURCHCD"] = DBNull.Value;
            _currentRow["PURCHNM"] = DBNull.Value;

            _currentRow["INP_CNT"] = DBNull.Value;
            _currentRow["INP_COUNT"] = DBNull.Value;
            _currentRow["INP_PRICE"] = DBNull.Value;
            _currentRow["RATE_KBN"] = DBNull.Value;
            _currentRow["INP_RATE"] = DBNull.Value;

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
                    else if (e.Column.FieldName == "INP_RATE")
                    {
                        _currentRow["INP_RATE"] = ConvertUtil.ToInt32(e.Value);
                    }
                    else if (e.Column.FieldName == "RATE_KBN")
                    {
                        _currentRow["RATE_KBN"] = ConvertUtil.ToInt32(e.Value);
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
                    if (getBookList(bookCd, ""))
                    {
                        _currentRow["RATE_KBN"] = 11;
                        SetColFocus("INP_COUNT", gvList.FocusedRowHandle);
                    }

                    //this.gvList.PostEditor();

                    //this.gvList.SetFocusedRowCellValue("ORDER_CNT", null);
                }
            }
        }

        private void riteTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEditor = (TextEdit)sender;
                string title = textEditor.Text;

                //if (string.IsNullOrWhiteSpace(title))
                //{
                //    Dangol.Warining("도서명을 입력하세요");
                //}
                //else
                //{
                if (getBookList(0, title))
                {
                    _currentRow["RATE_KBN"] = 11;
                    SetColFocus("INP_COUNT", gvList.FocusedRowHandle);
                }

                //this.gvList.PostEditor();
                //this.gvList.SetFocusedRowCellValue("ORDER_CNT", null);
                //}
            }
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

            //string url = "/search/getBookList.json";

            //if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                //if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    //JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    //foreach (JObject obj in jArray.Children<JObject>())
                    //{
                    //    insertOrderBook(row, obj);
                    //    break;
                    //}

                    if (!_dicLookUpEdit.ContainsKey(bookCd))
                    {
                        string url = "/search/getPurchaseList4Order.json";

                        if (DBConnect.getRequest(jobj, ref jResult, url))
                        {
                            if (Convert.ToBoolean(jResult["EXIST"]))
                            {
                                DataTable dtPurchCd;
                                dtPurchCd = new DataTable();
                                dtPurchCd.Columns.Add(new DataColumn("KEY", typeof(int)));
                                dtPurchCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

                                JArray jArray = JArray.Parse(jResult["DATA"].ToString());

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
                        DataTable dtPurchcd = _dicPurchCdTable[bookCd];
                        if (dtPurchcd.Rows.Count > 0)
                        {
                            //long purchCd = ConvertUtil.ToInt64(dtPurchcd.Rows[0]["KEY"]);
                            //row["PURCHCD"] = _purchCd;

                            getOrderRatio(row);
                        }
                        return true;
                    }
                }
                //else
                //{
                //    //Dangol.Warining($"도서코드 ['{bookCd}']가 점별도서테이블에 존재하지 않습니다.");
                //    return false;
                //}
            }
            //else
            //{
            //    Dangol.Error(jResult["MSG"]);
            //    return false;
            //}
        }

        private bool getBookList(long bookCd, string bookNm)
        {
            int shopCd = _shopCd;
            long purchCd = _purchCd;

            if (bookCd == 0)
            {
                //if (jData.ContainsKey("PURCHCD"))
                //purchCd = ConvertUtil.ToInt64(jData["PURCHCD"]);

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

                        insertOrderBook(objBook);

                        if (!_dicLookUpEdit.ContainsKey(bookCd))
                        {
                            JObject jResult = new JObject();
                            JObject jobj = new JObject();
                            jobj.Add("BOOKCD", bookCd);
                            jobj.Add("SHOPCD", _shopCd);

                            string url = "/search/getPurchaseList4Order.json";

                            if (DBConnect.getRequest(jobj, ref jResult, url))
                            {
                                if (Convert.ToBoolean(jResult["EXIST"]))
                                {
                                    DataTable dtPurchCd;
                                    dtPurchCd = new DataTable();
                                    dtPurchCd.Columns.Add(new DataColumn("KEY", typeof(int)));
                                    dtPurchCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

                                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                                    DataRow[] rows;

                                    int purchCdT;
                                    string purchNmT;
                                    //int ordGroupCd;

                                    long firstPurchCd = -1;
                                    int index = 1;
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
                                            firstPurchCd = purchCd;
                                            index++;
                                        }
                                    }

                                    LookUpEdit editor = new LookUpEdit();
                                    Util.LookupEditHelper(editor, dtPurchCd, "KEY", "VALUE");
                                    _dicLookUpEdit.Add(bookCd, editor);
                                    _dicPurchCdTable.Add(bookCd, dtPurchCd);

                                    _currentRow.BeginEdit();
                                    _currentRow["PURCHCD"] = firstPurchCd;
                                    _currentRow.EndEdit();

                                    //getBookOrdInfo(bookCd, ConvertUtil.ToInt32(firstPurchCd));

                                    return getOrderRatio();

                                }
                                else
                                {
                                    Dangol.Warining($"도서명['{bookNm}']에 대한 매입처가 없습니다. 주문할수없습니다.");
                                    _currentRow["STATE"] = -1;
                                    return false;
                                    //_currentRow["BOOKNM"] = "12312121231231";
                                }
                            }
                        }
                        else
                        {
                            DataTable dtPurchcd = _dicPurchCdTable[bookCd];

                            if (dtPurchcd.Rows.Count > 0)
                            {
                                purchCd = ConvertUtil.ToInt64(dtPurchcd.Rows[0]["KEY"]);
                                _currentRow.BeginEdit();
                                _currentRow["PURCHCD"] = purchCd;
                                _currentRow.EndEdit();

                                //getBookOrdInfo(bookCd, ConvertUtil.ToInt32(purchCd));

                                getOrderRatio();
                            }

                            return true;
                        }
                    }
                    else
                        return false;
                }
            }
            else
            {
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

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            insertOrderBook(obj);

                            break;
                        }

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

                                    long firstPurchCd = -1;
                                    int index = 1;

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
                                            //_dicPurchGroupCd.Add(purchCdT, ordGroupCd);
                                        }

                                        if (index == 1)
                                        {
                                            firstPurchCd = purchCd;
                                            index++;
                                        }
                                    }

                                    LookUpEdit editor = new LookUpEdit();
                                    Util.LookupEditHelper(editor, dtPurchCd, "KEY", "VALUE");
                                    _dicLookUpEdit.Add(bookCd, editor);
                                    _dicPurchCdTable.Add(bookCd, dtPurchCd);

                                    _currentRow.BeginEdit();
                                    _currentRow["PURCHCD"] = firstPurchCd;
                                    _currentRow.EndEdit();

                                    //getBookOrdInfo(bookCd, ConvertUtil.ToInt32(firstPurchCd));
                                    return getOrderRatio();
                                }
                                else
                                {
                                    Dangol.Warining($"도서코드['{bookCd}']에 대한 매입처가 없습니다. 주문할수없습니다.");
                                    _currentRow["STATE"] = -1;
                                    return false;

                                }
                            }
                        }
                        else
                        {
                            DataTable dtPurchcd = _dicPurchCdTable[bookCd];

                            if (dtPurchcd.Rows.Count > 0)
                            {
                                purchCd = ConvertUtil.ToInt64(dtPurchcd.Rows[0]["KEY"]);
                                _currentRow.BeginEdit();
                                _currentRow["PURCHCD"] = purchCd;
                                _currentRow.EndEdit();

                                //getBookOrdInfo(bookCd, ConvertUtil.ToInt32(purchCd));
                                getOrderRatio();
                            }

                            return true;
                        }
                    }
                    else
                    {
                        Dangol.Warining($"도서코드 ['{bookCd}']가 점별도서테이블에 존재하지 않습니다.");
                        return false;
                    }
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                    return false;
                }
            }

            return true;
        }

        private void insertOrderBook(DataRowView obj)
        {
            _currentRow.BeginEdit();

            _currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            riteTitle.BeginUpdate();
            _currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
            riteTitle.EndUpdate();

            _currentRow["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
            
            _currentRow["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
            _currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);

            _currentRow["INP_PRICE"] = ConvertUtil.ToInt32(obj["INP_PRICE"]);
            _currentRow["INP_COUNT"] = 0;


            _currentRow["ETC"] = "";
            _currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable

            _currentRow.EndEdit();

        }

        private void insertOrderBook(JObject obj)
        {
            _currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            _currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);

            _currentRow["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);

            _currentRow["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
            _currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);

            _currentRow["INP_PRICE"] = ConvertUtil.ToInt32(obj["INP_PRICE"]);
            _currentRow["INP_COUNT"] = 0;

            _currentRow["ETC"] = "";
            _currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable
        }

        private void insertOrderBook(DataRow row, JObject obj)
        {
            row["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            row["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);

            row["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);

            row["PUBSHCD"] = ConvertUtil.ToInt32(obj["PUBSHCD"]);
            row["PUBSHNM"] = ConvertUtil.ToInt32(obj["PUBSHNM"]);

            row["INP_RATE"] = ConvertUtil.ToInt32(obj["INP_PLAN_RATE"]);
            row["INP_COUNT"] = 0;

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

            if (_processType == 3)
            {
                if (view.FocusedColumn.FieldName == "PURCHCD")
                {
                    LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                    long bookCd = ConvertUtil.ToInt64(view.GetFocusedRowCellValue("BOOKCD"));
                    if (_dicLookUpEdit.ContainsKey(bookCd))
                        editor.Properties.DataSource = _dicPurchCdTable[bookCd];
                    else
                        editor.Properties.DataSource = null;
                }
                else if (view.FocusedColumn.FieldName == "INP_RATE")
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
        }

        private void rilePurchCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (popup == null)
                //{
                //    SetColFocus("ORDER_RATIO", gvList.FocusedRowHandle);
                //}
                //popup = null;
            }
        }

        private void rilePurchCd_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit editor = (LookUpEdit)sender;
            _currentRow["PURCHCD"] = _selectedPurchCd = ConvertUtil.ToInt64(editor.EditValue);

            //int purchCd = ConvertUtil.ToInt32(gvList.GetFocusedRowCellValue("PURCHCD"));

            LookUpEdit lookUpEdit = (LookUpEdit)sender;
            var purchCd = lookUpEdit.EditValue;

            getBookOrdInfo(ConvertUtil.ToInt64(_currentRow["BOOKCD"]), ConvertUtil.ToInt32(purchCd));

            //showEditor("ORDER_RATIO");

            SetColFocus("INP_RATE", gvList.FocusedRowHandle);

            string key = $"{_shopCd}/{_currentRow["BOOKCD"]}/{purchCd}";

            if (_dicOrderRatioTable.ContainsKey(key))
            {
                int value = ConvertUtil.ToInt32(_dicOrderRatioTable[key].Rows[0]["KEY"]);
                _currentRow.BeginEdit();
                _currentRow["INP_RATE"] = value;
                _currentRow.EndEdit();
            }
        }
        private bool getOrderRatio(bool init = true)
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
                if (init)
                {
                    DataTable dtRatio = _dicOrderRatioTable[key];
                    if (dtRatio.Rows.Count > 0)
                    {
                        _currentRow.BeginEdit();
                        _currentRow["INP_RATE"] = ConvertUtil.ToInt32(dtRatio.Rows[0]["KEY"]);
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

                        if (init)
                        {
                            _currentRow.BeginEdit();
                            _currentRow["INP_RATE"] = firstRate;
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
                    row["INP_RATE"] = ConvertUtil.ToInt32(dtRatio.Rows[0]["KEY"]);

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

                        row["INP_RATE"] = firstRate;

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

                    _currentRow["INP_COUNT"] = ConvertUtil.ToInt32(jResult["INP_COUNT"]);
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
                    //cnt = ConvertUtil.ToInt32(jResult["INP_COUNT"]);
                    //if (cnt != 0) _currentRow["INP_COUNT"] = cnt;
                    //else _currentRow["INP_COUNT"] = DBNull.Value;

                    //cnt = ConvertUtil.ToInt32(jResult["RET_CNT"]);
                    //if (cnt != 0) _currentRow["RETURN_CNT"] = cnt;
                    //else _currentRow["RETURN_CNT"] = DBNull.Value;

                    //cnt = ConvertUtil.ToInt32(jResult["ORD_CNT"]);
                    //if (cnt != 0) _currentRow["ORD_CNT"] = cnt;
                    //else _currentRow["ORD_CNT"] = DBNull.Value;

                    //cnt = ConvertUtil.ToInt32(jResult["ESTI_CNT"]);
                    //if (cnt != 0) _currentRow["ESTI_CNT"] = cnt;
                    //else _currentRow["ESTI_CNT"] = DBNull.Value;
                    _currentRow.EndEdit();
                }
                else
                {
                    _currentRow.BeginEdit();
                    //_currentRow["INP_COUNT"] = DBNull.Value;
                    //_currentRow["RETURN_CNT"] = DBNull.Value;

                    //_currentRow["ORD_CNT"] = DBNull.Value;
                    //_currentRow["ESTI_CNT"] = DBNull.Value;

                    //_currentRow["STOCK_CNT"] = DBNull.Value;
                    _currentRow.EndEdit();
                }
            }
            else
            {
                _currentRow.BeginEdit();
                //_currentRow["INP_COUNT"] = DBNull.Value;
                //_currentRow["RETURN_CNT"] = DBNull.Value;

                //_currentRow["ORD_CNT"] = DBNull.Value;
                //_currentRow["ESTI_CNT"] = DBNull.Value;

                //_currentRow["STOCK_CNT"] = DBNull.Value;
                _currentRow.EndEdit();
            }
        }
        private void rileOrderRatio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //LookUpEdit editor = (LookUpEdit)sender;
                //_currentRow["INP_RATE"] = ConvertUtil.ToInt32(editor.EditValue);

                //int price = ConvertUtil.ToInt32(_currentRow["INP_PRICE"]);
                //int orderRatio = ConvertUtil.ToInt32(_currentRow["INP_RATE"]);

                //int wPrice = price * orderRatio / 100;
                //_currentRow["WAREHOUSING_PRICE"] = wPrice;

                //int inpCnt = ConvertUtil.ToInt32(_currentRow["INP_COUNT"]);
                //_currentRow["TOTAL_PRICE"] = wPrice * inpCnt;

                //long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
                //int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);
                //int orderRatio = ConvertUtil.ToInt32(_currentRow["ORDER_RATIO"]);

                //setOrderCount(bookCd, purchCd, orderRatio);

                //SetColFocus("RATE_KBN", gvList.FocusedRowHandle);
            }
        }
        private void rileOrderRatio_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit editor = (LookUpEdit)sender;
            _currentRow["INP_RATE"] = ConvertUtil.ToInt32(editor.EditValue);

            //long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            //int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);
            //setOrderCount(bookCd, purchCd, orderRatio);

            //int price = ConvertUtil.ToInt32(_currentRow["INP_PRICE"]);
            //int orderRatio = ConvertUtil.ToInt32(_currentRow["INP_RATE"]);

            //int wPrice = price * orderRatio / 100;
            //_currentRow["WAREHOUSING_PRICE"] = wPrice;

            //int inpCnt = ConvertUtil.ToInt32(_currentRow["INP_COUNT"]);
            //_currentRow["TOTAL_PRICE"] = wPrice * inpCnt;

            SetColFocus("RATE_KBN", gvList.FocusedRowHandle);
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
            {
                //SetColFocus("INP_COUNT", gvList.FocusedRowHandle);
            }
        }

        private void rileCondition_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit editor = (LookUpEdit)sender;
            _currentRow["RATE_KBN"] = ConvertUtil.ToInt64(editor.EditValue);

            SetColFocus("INP_COUNT", gvList.FocusedRowHandle);
        }

        private void riseInpCnt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //SpinEdit editor = (SpinEdit)sender;

            //int inpCnt = ConvertUtil.ToInt32(e.NewValue);
            //int wPrice = ConvertUtil.ToInt32(_currentRow["WAREHOUSING_PRICE"]);
            //_currentRow["TOTAL_PRICE"] = wPrice * inpCnt;
        }

        private void riseInpCnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //SpinEdit editor = (SpinEdit)sender;
                //int inpCnt = ConvertUtil.ToInt32(editor.EditValue);
                //int wPrice = ConvertUtil.ToInt32(_currentRow["WAREHOUSING_PRICE"]);
                //_currentRow["TOTAL_PRICE"] = wPrice * inpCnt;
                int focusedRowHandle = gvList.FocusedRowHandle;

                if (_dt.Rows.Count - 1 > focusedRowHandle)
                {
                    if(_processType == 3)
                        SetColFocus("BOOKCD", focusedRowHandle + 1);
                    else
                        SetColFocus("INP_COUNT", focusedRowHandle + 1);
                }
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
