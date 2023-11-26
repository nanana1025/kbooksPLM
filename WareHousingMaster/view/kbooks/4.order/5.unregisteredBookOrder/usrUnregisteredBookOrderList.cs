using DevExpress.XtraEditors;
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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrUnregisteredBookOrderList : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; set; }

        Regex regex;

        JObject _jobj;

        int _shopCd;

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


        public usrUnregisteredBookOrderList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("PURCHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHORNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("TRADE_ITEM", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORD_COUNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORDER_CNT", typeof(int)));
            //
            //_dt.Columns.Add(new DataColumn("STORE2", typeof(int)));
            //_dt.Columns.Add(new DataColumn("STOCK_CNT", typeof(int)));

            //_dt.Columns.Add(new DataColumn("RETURN_CNT", typeof(int)));
            //
            //_dt.Columns.Add(new DataColumn("WAREHOUSING_EXPECTED_CNT", typeof(int)));
            //_dt.Columns.Add(new DataColumn("SALE_CNT", typeof(int)));

            //_dt.Columns.Add(new DataColumn("ORDER_RATIO", typeof(double)));


            //

            _dt.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));  //0:default, 1:create, 2:verificated, 3:complete, -1:notavailable

            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _shopCd = 1;

            regex = new Regex(@"^[0-9]{6}:.*");

            _bs = new BindingSource();
        }



        public void setInitLoad()
        {
            setInfoBox();
            setinitialize();
        }

        private void setInfoBox()
        {
            //List<string> listLongCol = new List<string>(new string[] { "SHOPCD" });
            //DataTable dtShopCd = Util.getTable("HMA09", "SHOPCD,SHOP_NM ", "SHOPCD > 0", "SHOPCD ASC", listLongCol);
            //Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");

            DataTable dtTradeItem = new DataTable();
            dtTradeItem.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtTradeItem.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRow(dtTradeItem, "1", "1.매장위탁");
            Util.insertRow(dtTradeItem, "2", "2.매장현매");
            Util.insertRow(dtTradeItem, "3", "3.매장매절");

            Util.LookupEditHelper(rileTradeItem, dtTradeItem, "KEY", "VALUE");

        }

        public void setinitialize()
        {
            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;
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
                dr["STATE"] = 0;    //0:default, 1:create, 2:verificated, 3:complete, -1:notavailable
                dr["CHECK"] = false;
                _dt.Rows.Add(dr);
            }

            gvList.EndDataUpdate();
        }

        public void addTableInitialize()
        {
            gvList.BeginDataUpdate();

            int start = _dt.Rows.Count;
            for (int i = start; i < start + 30; i++)
            {
                DataRow dr = _dt.NewRow();

                dr["NO"] = i + 1;
                dr["ID"] = i + 1;
                dr["STATE"] = 0; //0:default, 1:create, 2:verificated, 3:complete, -1:notavailable
                dr["CHECK"] = false;
                _dt.Rows.Add(dr);
            }

            gvList.EndDataUpdate();
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
            string url = "/order/getUnregisteredBookList.json";

            gvList.BeginDataUpdate();
            //_dt.Clear();

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                int index = 1;

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string purchNm;
                    string[] arrData;
                    int purchCd;
                    DataRow[] rows;
                    DataRow row;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        purchNm = ConvertUtil.ToString(obj["PURCHNM"]);
                        arrData = purchNm.Split(':');

                        if (arrData.Length > 1)
                        {
                            purchCd = ConvertUtil.ToInt32(arrData[0]);
                            rows = _dt.Select($"ID = {index}");
                            if (index < 30)
                            {
                                row = rows[0];

                                row["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);

                                row["PURCHCD"] = purchCd;
                                row["PURCHNM"] = purchNm;
                                row["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
                                row["TRADE_ITEM"] = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
                                row["ORD_COUNT"] = ConvertUtil.ToInt32(obj["ORD_COUNT"]);
                                row["ORDER_CNT"] = DBNull.Value;
                                row["ETC"] = ConvertUtil.ToString(obj["ETC"]);
                                row["STATE"] = 2;
                            }
                            else
                            {
                                DataRow dr = _dt.NewRow();

                                dr["NO"] = index;
                                dr["ID"] = index;
                                dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);

                                dr["PURCHCD"] = purchCd;
                                dr["PURCHNM"] = purchNm;
                                dr["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
                                dr["TRADE_ITEM"] = ConvertUtil.ToInt32(obj["TRADE_ITEM"]);
                                dr["ORD_COUNT"] = ConvertUtil.ToInt32(obj["ORD_COUNT"]);
                                dr["ORDER_CNT"] = DBNull.Value;
                                dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);
                                dr["STATE"] = 2;
                                _dt.Rows.Add(dr);
                            }
                            index++;
                        }
                    }
                }

                gvList.EndDataUpdate();

                if (index > 25)
                    addTableInitialize();
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
            {
                _currentRow["BOOKNM"] = DBNull.Value;
                _currentRow["AUTHORNM"] = DBNull.Value;
                _currentRow["PURCHNM"] = DBNull.Value;
                _currentRow["PURCHCD"] = DBNull.Value;
                _currentRow["TRADE_ITEM"] = DBNull.Value;

                _currentRow["ORD_COUNT"] = DBNull.Value;
                _currentRow["ORDER_CNT"] = DBNull.Value;

                _currentRow["STATE"] = 0;
                _currentRow["CHECK"] = false;
            }
            else
            {
                if (Dangol.MessageYN("선택한 행을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    _currentRow["BOOKNM"] = DBNull.Value;
                    _currentRow["AUTHORNM"] = DBNull.Value;
                    _currentRow["PURCHNM"] = DBNull.Value;
                    _currentRow["PURCHCD"] = DBNull.Value;
                    _currentRow["TRADE_ITEM"] = DBNull.Value;

                    _currentRow["ORD_COUNT"] = DBNull.Value;
                    _currentRow["ORDER_CNT"] = DBNull.Value;

                    _currentRow["STATE"] = 0;
                    _currentRow["CHECK"] = false;
                }
            }
        }


        public void insertHandler()
        {
            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

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

            string bookNm;
            int purchCd;
            int orderCnt;
            int tradeItem;
            int faultCnt = 0;

            foreach (DataRow row in rows)
            {
                bookNm = ConvertUtil.ToString(row["BOOKNM"]);
                purchCd = ConvertUtil.ToInt32(row["PURCHCD"]);
                orderCnt = ConvertUtil.ToInt32(row["ORDER_CNT"]);
                tradeItem = ConvertUtil.ToInt32(row["TRADE_ITEM"]);
                

                if (!string.IsNullOrWhiteSpace(bookNm) && purchCd > 0 && orderCnt > 0 && tradeItem > 0)
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

            DataRow[] rows = _dt.Select("STATE = 2");


            foreach (DataRow row in rows)
            {
                JObject jdata = new JObject();

                jdata.Add("SHOPCD", _shopCd);
                jdata.Add("STORECD", ConvertUtil.ToInt32(_jobj["STORECD"]));
                jdata.Add("ORD_DATE", ConvertUtil.ToString(_jobj["ORD_DATE"]));
                jdata.Add("BOOKNM", ConvertUtil.ToString(row["BOOKNM"]));
                jdata.Add("PURCHNM", ConvertUtil.ToString(row["PURCHNM"]));
                jdata.Add("AUTHORNM", ConvertUtil.ToString(row["AUTHORNM"]));
                jdata.Add("TRADE_ITEM", ConvertUtil.ToInt32(row["TRADE_ITEM"]));
                jdata.Add("ORD_COUNT", ConvertUtil.ToInt32(row["ORDER_CNT"]));

                jArray.Add(jdata);
            }

            jobj.Add("DATA", jArray);
            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("STORECD", ConvertUtil.ToInt32(_jobj["STORECD"]));
            jobj.Add("ORD_DATE", ConvertUtil.ToString(_jobj["ORD_DATE"]));


            string url = "/order/insertUnregisterdOrderBook.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                Dangol.Message($"{rows.Length}개의 데이터가 확정되었습니다.");

                setTableInitialize();
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

 
        private void ritePurchNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEditor = (TextEdit)sender;
                string purchNm = textEditor.Text;

                if (regex.IsMatch(purchNm))
                {
                    string[] arrData = purchNm.Split(':');
                    purchNm = arrData[1];
                }

                if (getPurchase(purchNm))
                {
                    int rowHandle = gvList.FocusedRowHandle;
                    SetColFocus("AUTHORNM", rowHandle);
                }
            }
        }

        private bool getPurchase(string purchNm)
        {
            string nPurchNm = purchNm;

            using (dlgPurchaseList sellerList = new dlgPurchaseList(purchNm, _shopCd))
            {
                //sellerList.StartPosition = FormStartPosition.Manual;
                //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                if (sellerList.ShowDialog(this) == DialogResult.OK)
                {
                    int purchCd = ConvertUtil.ToInt32(sellerList._drv["PURCHCD"]);
                    nPurchNm = $"{string.Format("{0:D6}", purchCd)}:{sellerList._drv["PURCHNM"]}";

                    ritePurchNm.BeginUpdate();
                    _currentRow["PURCHCD"] = purchCd;
                    _currentRow["PURCHNM"] = nPurchNm;
                    _currentRow["TRADE_ITEM"] = 1;
                    _currentRow["ORD_COUNT"] = 0;
                    _currentRow["ORDER_CNT"] = 0;
                    _currentRow["STATE"] = 1;
                    ritePurchNm.EndUpdate();

                    return true;
                }
            }

            return false;

            //return nPurchNm;
        }

        private void usrUnregisteredBookOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                e.Handled = true;
            }
        }

        private void riteTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowHandle = gvList.FocusedRowHandle;
                SetColFocus("PURCHNM", rowHandle);
            }
        }

        private void riteAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowHandle = gvList.FocusedRowHandle;
                SetColFocus("ORDER_CNT", rowHandle);
            }
        }

        private void rileTradeItem_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    int rowHandle = gvList.FocusedRowHandle;
            //    SetColFocus("ORDER_CNT", rowHandle);
            //}
        }

        private void riseCnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowHandle = gvList.FocusedRowHandle;
                if (rowHandle < 29)
                    SetColFocus("BOOKNM", rowHandle + 1);
            }
        }

        
    }
}
