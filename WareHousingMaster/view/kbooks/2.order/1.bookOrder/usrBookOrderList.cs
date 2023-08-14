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
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrBookOrderList : DevExpress.XtraEditors.XtraUserControl
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


        public usrBookOrderList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("ID", typeof(int)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHOR", typeof(string)));
            _dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(int)));

            _dt.Columns.Add(new DataColumn("GROUPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("STANDCD", typeof(int)));

            _dt.Columns.Add(new DataColumn("INP_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("RETURN_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORD_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("ESTI_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("STOCK_CNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORDER_RATIO", typeof(double)));
            _dt.Columns.Add(new DataColumn("ORDER_CNT", typeof(int)));
           
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
            _dicPurchGroupCd = new Dictionary<int, int>();
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

            //Util.LookupEditHelper(rileComponentCd, InventoryInfo._dtInventoryCd, "KEY", "VALUE");

            //DataTable dtUsedYn = Util.getCodeList_Int_String("CD0107", "KEY", "VALUE");
            //Util.LookupEditHelper(rileUsedYn, dtUsedYn, "KEY", "VALUE");

            //RepositoryItemLookUpEdit riLookup2 = new RepositoryItemLookUpEdit();

            //riLookup2.DisplayMember = "VALUE";

            //riLookup2.ValueMember = "KEY";

            //riLookup2.DataSource = _dtPurchCd;

            //gcList.RepositoryItems.Add(riLookup2);

            //gvList.Columns["PURCHCD"].OptionsColumn.AllowEdit = true;

            //gvList.Columns["PURCHCD"].ColumnEdit = riLookup2;

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
            _shopCd = ConvertUtil.ToInt32(obj["SHOPCD"]);
            _storeCd = ConvertUtil.ToInt32(obj["STORECD"]);
            _groupType = ConvertUtil.ToInt32(obj["GROUP_TYPE"]);
            _groupCd = ConvertUtil.ToInt32(obj["GROUPCD"]);
            _inpGroupCd = ConvertUtil.ToInt32(obj["INP_GROUPCD"]);
            _groupNm = ConvertUtil.ToString(obj["GROUPNM"]);
            _purchType = ConvertUtil.ToInt32(obj["PURCH_TYPE"]);
            _purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);
            _purchNm = ConvertUtil.ToString(obj["PURCHNM"]);
            _orderType = ConvertUtil.ToInt32(obj["ORDER_TYPE"]);
            _orderCondition = ConvertUtil.ToInt32(obj["ORDER_CONTIDION"]);
            _conditionString = ConvertUtil.ToString(obj["ORDER_CONTIDION_STRING"]);
            _tradeItem = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
            _orderDt = ConvertUtil.ToString(obj["DT_ORDER"]);
            _inpDt = ConvertUtil.ToString(obj["DT_INP"]);
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

        public void insertOrder()
        {
            DataRow[] rows = _dt.Select("STATE <> 0");

            if (rows.Length < 1)
                Dangol.Warining("주문건이 없습니다.");
            else
            {
                if (dataVerification())
                {
                    if (Dangol.MessageYN("주문건을 저장하시겠습니까?") == DialogResult.Yes)
                        insertData();
                }
                else
                {
                    if (Dangol.MessageYN("완료되지 않은 주문건이 있습니다. 그래도 진행하시겠습니까?") == DialogResult.Yes)
                    {
                        insertData();
                    }
                }
            }
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
                orderCnt = ConvertUtil.ToInt32(row["ORDER_CNT"]);

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

        private void insertData()
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
            int ordRate;
            int ordCost;
            int purchCd;

            foreach (DataRow row in rows)
            {
                JObject jdata = new JObject();

                jdata.Add("ID", ConvertUtil.ToInt32(row["ID"]));
                jdata.Add("SHOPCD", _shopCd);
                jdata.Add("STORECD", _storeCd);
                jdata.Add("INP_GROUPCD", _inpGroupCd);
                jdata.Add("ORD_DATE", _orderDt);
                jdata.Add("INP_DATE", _inpDt);
                jdata.Add("TRADE_ITEM", _tradeItem);

                jdata.Add("BOOKCD", ConvertUtil.ToInt64(row["BOOKCD"]));
                jdata.Add("GROUPCD", ConvertUtil.ToInt32(row["GROUPCD"]));
                jdata.Add("STANDCD", ConvertUtil.ToInt32(row["STANDCD"]));
                purchCd = ConvertUtil.ToInt32(row["PURCHCD"]);
                jdata.Add("PURCHCD", purchCd);

                price = ConvertUtil.ToInt32(row["PRICE"]);
                ordRate = ConvertUtil.ToInt32(row["ORDER_RATIO"]);

                ordCost = (price * ordRate) / 100;

                jdata.Add("ORD_CNT", ConvertUtil.ToInt32(row["ORDER_CNT"]));
                jdata.Add("ORD_RATE", ordRate);
                jdata.Add("ORD_COST", ordCost);
                jdata.Add("BOOKNM", ConvertUtil.ToString(row["BOOKNM"]));
                jdata.Add("AUTHORNM", ConvertUtil.ToString(row["AUTHOR"]));
                if(_dicPurchNm.ContainsKey(purchCd))
                    jdata.Add("PURCHNM", _dicPurchNm[purchCd]);
                else
                    jdata.Add("PURCHNM", "");

                if (_dicPurchGroupCd.ContainsKey(purchCd))
                    jdata.Add("ORD_GROUPCD", _dicPurchGroupCd[purchCd]);
                else
                    jdata.Add("ORD_GROUPCD", -1);

                jArray.Add(jdata);
            }

            jobj.Add("DATA", jArray);

            string url = "/order/insertOrderBook.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                Dangol.Message($"{rows.Length}개의 데이터가 확정되었습니다.");
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
            }
        }

        public void getList(JObject jobj)
        {
            _jobj = jobj;

            JObject jResult = new JObject();
            string url = "/Nrelease/getReceiptItemList.json";

            gvList.BeginDataUpdate();
            _dt.Clear();

            if (_representativeId > 0)
            {
                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                        int index = 1;

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            DataRow dr = _dt.NewRow();

                            dr["NO"] = index++;
                            dr["ID"] = ConvertUtil.ToInt64(obj["ID"]);

                            dr["BOOK_CD"] = ConvertUtil.ToString(obj["BOOK_CD"]);
                            dr["TITLE"] = ConvertUtil.ToString(obj["TITLE"]);
                            dr["AUTHOR"] = ConvertUtil.ToString(obj["AUTHOR"]);
                            dr["PUBLISHER"] = ConvertUtil.ToString(obj["PUBLISHER"]);
                            dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);
                            dr["RETURN_CNT"] = ConvertUtil.ToInt32(obj["RETURN_CNT"]);
                            dr["DELIVERY_CNT"] = ConvertUtil.ToInt32(obj["DELIVERY_CNT"]);

                            dr["WAREHOUSING_CNT"] = ConvertUtil.ToInt32(obj["WAREHOUSING_CNT"]);
                            dr["WAREHOUSING_EXPECTED_CNT"] = ConvertUtil.ToInt32(obj["WAREHOUSING_EXPECTED_CNT"]);
                            dr["STOCK_CNT"] = ConvertUtil.ToInt32(obj["STOCK_CNT"]);

                            dr["SELLER"] = ConvertUtil.ToString(obj["SELLER"]);
                            dr["ORDER_RATIO"] = ConvertUtil.ToDouble(obj["ORDER_RATIO"]);

                            dr["ORDER_CNT"] = ConvertUtil.ToInt32(obj["ORDER_CNT"]);
                            dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);

                            dr["STATE"] = 1;
                            dr["CHECK"] = false;
                            _dt.Rows.Add(dr);
                        }
                    }

                    gvList.EndDataUpdate();
                }
                else
                {
                    gvList.EndDataUpdate();
                    Dangol.Error(jResult["MSG"]);
                }
            }
            else
            {
                gvList.EndDataUpdate();
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
                    _currentRow["BOOKCD"] = DBNull.Value;
                    _currentRow["BOOKNM"] = DBNull.Value;
                    _currentRow["AUTHOR"] = DBNull.Value;
                    _currentRow["PUBSHNM"] = DBNull.Value;
                    _currentRow["PRICE"] = DBNull.Value;

                    _currentRow["GROUPCD"] = DBNull.Value;
                    _currentRow["STANDCD"] = DBNull.Value;

                    _currentRow["INP_CNT"] = DBNull.Value;
                    _currentRow["RETURN_CNT"] = DBNull.Value;
                    _currentRow["ORD_CNT"] = DBNull.Value;
                    _currentRow["ESTI_CNT"] = DBNull.Value;
                    _currentRow["STOCK_CNT"] = DBNull.Value;

                    _currentRow["PURCHCD"] = DBNull.Value;
                    _currentRow["ORDER_RATIO"] = DBNull.Value;
                    _currentRow["ORDER_CNT"] = DBNull.Value;

                    _currentRow["STATE"] = 0;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable
                }
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
                    this.gvList.PostEditor();
                    this.gvList.SetFocusedRowCellValue("ORDER_CNT", null);
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
                    this.gvList.PostEditor();
                    this.gvList.SetFocusedRowCellValue("ORDER_CNT", null);
                }
            }
        }

        private void getBookList(long bookCd, string bookNm)
        {
            JObject jData = getSearchInfoHandler();

            int shopCd = ConvertUtil.ToInt32(jData["SHOPCD"]);
            long purchCd = 0;

            if (bookCd == 0)
            {
                if (jData.ContainsKey("PURCHCD"))
                    purchCd = ConvertUtil.ToInt64(jData["PURCHCD"]);

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
                                    int ordGroupCd;

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
                                            ordGroupCd = ConvertUtil.ToInt32(obj["ORD_GROUPCD"]); 

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

                                            if (!_dicPurchGroupCd.ContainsKey(purchCdT))
                                                _dicPurchGroupCd.Add(purchCdT, ordGroupCd);
                                        }

                                        if(index == 1)
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
                                    _currentRow["BOOKCD"] = firstPurchCd;
                                    _currentRow.EndEdit();
                                    getOrderRatio();
                                }
                                else
                                {
                                    Dangol.Warining($"도서명['{bookNm}']에 대한 매입처가 없습니다. 주문할수없습니다.");
                                    _currentRow["STATE"] = -1;
                                    //_currentRow["BOOKNM"] = "12312121231231";
                                }
                            }
                        }
                    }
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
                                    int ordGroupCd;

                                    long firstPurchCd = -1;
                                    int index = 1;

                                    foreach (JObject obj in jArray.Children<JObject>())
                                    {
                                        purchCd = ConvertUtil.ToInt64(obj["PURCHCD"]);

                                        rows = _dtPurchCd.Select($"BOOKCD = {bookCd} AND KEY = {purchCd}");
                                        if(rows.Length == 0)
                                        {
                                            purchCdT = ConvertUtil.ToInt32(obj["PURCHCD"]);
                                            purchNmT = ConvertUtil.ToString(obj["PURCHNM"]);
                                            ordGroupCd = ConvertUtil.ToInt32(obj["ORD_GROUPCD"]);

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

                                            if (!_dicPurchGroupCd.ContainsKey(purchCdT))
                                                _dicPurchGroupCd.Add(purchCdT, ordGroupCd);
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
                                    getOrderRatio();
                                }
                                else
                                {
                                    Dangol.Warining($"도서코드['{bookCd}']에 대한 매입처가 없습니다. 주문할수없습니다.");
                                    _currentRow["STATE"] = -1;
                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        Dangol.Warining($"도서코드 ['{bookCd}']가 점별도서테이블에 존재하지 않습니다.");
                    }
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                }
            }
        }

        private void insertOrderBook(DataRowView obj)
        {
            _currentRow.BeginEdit();

            _currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            riteTitle.BeginUpdate();
            _currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
            riteTitle.EndUpdate();
            _currentRow["AUTHOR"] = ConvertUtil.ToString(obj["AUTHOR1"]);
            _currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
            _currentRow["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

            _currentRow["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
            _currentRow["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);


            _currentRow["INP_CNT"] = 0;
            _currentRow["RETURN_CNT"] = 0;

            _currentRow["ORD_CNT"] = 0;
            _currentRow["ESTI_CNT"] = 0;
            _currentRow["STOCK_CNT"] = 0;
            _currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable

            _currentRow.EndEdit();

        }

        private void insertOrderBook(JObject obj)
        {
            _currentRow["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
            _currentRow["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
            _currentRow["AUTHOR"] = ConvertUtil.ToString(obj["AUTHOR1"]);
            _currentRow["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
            _currentRow["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);

            _currentRow["GROUPCD"] = ConvertUtil.ToInt32(obj["GROUPCD"]);
            _currentRow["STANDCD"] = ConvertUtil.ToInt32(obj["STANDCD"]);


            _currentRow["INP_CNT"] = 0;
            _currentRow["RETURN_CNT"] = 0;

            _currentRow["ORD_CNT"] = 0;
            _currentRow["ESTI_CNT"] = 0;

            _currentRow["STOCK_CNT"] = 0;
            _currentRow["STATE"] = 1;   //0:default, 1:create, 2:available, 3:complete, -1:notavailable
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

        private void rilePurchCd_EditValueChanged(object sender, EventArgs e)
        {

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

            string key = $"{_shopCd}/{bookCd}/{purchCd}";

            if (!_dicOrderRatioTable.ContainsKey(key) && bookCd > 0 && purchCd > 0)
            {
                if (_orderCondition == 1)
                {
                    jobj.Add("RATE_KBN_S", 10);
                    jobj.Add("RATE_KBN_E", 20);
                }
                else
                {
                    jobj.Add("RATE_KBN_S", 20);
                    jobj.Add("RATE_KBN_E", 30);
                }

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

                            rows = _dtPurchCd.Select($"KEY = {rate}");

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

                            if(index == 1)
                            {
                                firstRate = rate;
                                index++;
                            }
                            
                        }

                        _dicOrderRatioTable.Add(key, dtOrderRatioTable);

                        _currentRow.BeginEdit();
                        _currentRow["ORDER_RATIO"] = firstRate;
                        _currentRow.EndEdit();

                    }
                    else
                        Dangol.Warining($"도서코드 ['{bookCd}']에 {_conditionString} 해당하는 매입율이 없습니다. 주문할 수 없습니다.");
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                }
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

        private void rileOrderRatio_EditValueChanged(object sender, EventArgs e)
        {
            long bookCd = ConvertUtil.ToInt64(_currentRow["BOOKCD"]);
            int purchCd = ConvertUtil.ToInt32(_currentRow["PURCHCD"]);
            int orderRatio = ConvertUtil.ToInt32(_currentRow["ORDER_RATIO"]);

            setOrderCount(bookCd, purchCd, orderRatio);
        }
    }
}
