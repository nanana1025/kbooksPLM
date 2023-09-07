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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrBookOrderResultList : DevExpress.XtraEditors.XtraUserControl
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

        Dictionary<int, int> _dicPurchOrderCnt;

        public delegate void FocusedRowObjectChangeHandler(DataRowView drv);
        public event FocusedRowObjectChangeHandler focusedRowObjectChangeHandler;


        public usrBookOrderResultList()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("PURCHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PURCHCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("STORECD", typeof(int)));
            _dt.Columns.Add(new DataColumn("GROUP_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("BOOKCD", typeof(long)));
            _dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("AUTHORNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("ORD_COUNT", typeof(int)));

            _dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(int)));
            _dt.Columns.Add(new DataColumn("TEL", typeof(string)));
            _dt.Columns.Add(new DataColumn("FAX", typeof(string)));


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
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));

            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            
           
            _bs = new BindingSource();

            _dicPurchOrderCnt = new Dictionary<int, int>();
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

            JObject jResult = new JObject();
            string url = "/order/getOrderBookAllList.json";

            gvList.BeginDataUpdate();
            _dt.Clear();
            _dicPurchOrderCnt.Clear();

            Dangol.ShowSplash();

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                int index = 1;
                int purchCd;
                int orderCnt;

                if (Convert.ToBoolean(jResult["REGISTERED_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["REGISTERED_DATA"].ToString());
                   
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);
                        orderCnt = ConvertUtil.ToInt32(obj["ORD_COUNT"]);

                        if (_dicPurchOrderCnt.ContainsKey(purchCd))
                            _dicPurchOrderCnt[purchCd] += orderCnt;
                        else
                            _dicPurchOrderCnt.Add(purchCd, orderCnt);

                        DataRow dr = _dt.NewRow();

                        dr["NO"] = index++;
                        dr["PURCHNM"] = ConvertUtil.ToString(obj["PURCHNM"]);
                        dr["PURCHCD"] = purchCd;
                        dr["STORECD"] = ConvertUtil.ToInt32(obj["STORECD"]);
                        dr["GROUP_NM"] = ConvertUtil.ToString(obj["GROUP_NM"]);
                        dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                        dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                        dr["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
                        dr["ORD_COUNT"] = orderCnt;

                        dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);
                        dr["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
                        dr["TEL"] = ConvertUtil.ToString(obj["TEL"]);
                        dr["FAX"] = ConvertUtil.ToString(obj["FAX"]);

                        //dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);
                        dr["STATE"] = 1;
                        dr["CHECK"] = false;
                        _dt.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["UNREGISTERED_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["UNREGISTERED_DATA"].ToString());

                    string purchNm;
                    string[] arrData;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        purchNm = ConvertUtil.ToString(obj["PURCHNM"]);
                        arrData = purchNm.Split(':');
                        if (arrData.Length > 1)
                        {
                            purchCd = ConvertUtil.ToInt32(arrData[0]);
                            orderCnt = ConvertUtil.ToInt32(obj["ORD_COUNT"]);

                            if (_dicPurchOrderCnt.ContainsKey(purchCd))
                                _dicPurchOrderCnt[purchCd] += orderCnt;
                            else
                                _dicPurchOrderCnt.Add(purchCd, orderCnt);

                            DataRow dr = _dt.NewRow();

                            dr["NO"] = index++;
                            dr["PURCHNM"] = purchNm.Replace($"{arrData[0]}:", "");
                            dr["PURCHCD"] = purchCd;
                            dr["STORECD"] = ConvertUtil.ToInt32(obj["STORECD"]);
                            dr["GROUP_NM"] = ConvertUtil.ToString(obj["GROUP_NM"]);
                            dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                            dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                            dr["AUTHORNM"] = ConvertUtil.ToString(obj["AUTHORNM"]);
                            dr["ORD_COUNT"] = orderCnt;

                            dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);
                            dr["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
                            dr["TEL"] = ConvertUtil.ToString(obj["TEL"]);
                            dr["FAX"] = ConvertUtil.ToString(obj["FAX"]);

                            //dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);
                            dr["STATE"] = 1;
                            dr["CHECK"] = false;
                            _dt.Rows.Add(dr);
                        }
                    }
                }
                gvList.EndDataUpdate();

                Dangol.CloseSplash();

                if (_dt.Rows.Count < 1)
                    Dangol.Info("정보가 없습니다.");
            }
            else
            {
                gvList.EndDataUpdate();
                Dangol.CloseSplash();
                Dangol.Error(jResult["MSG"]);
            }
        }

        public void filter(bool isCheck, int filterCnt)
        {
            if (!isCheck)
                _bs.Filter = null;
            else
            {
                List<int> listPurchCd = new List<int>();
                foreach (KeyValuePair<int, int> item in _dicPurchOrderCnt)
                    if (item.Value <= filterCnt)
                        listPurchCd.Add(item.Key);

                if (listPurchCd.Count > 0)
                    _bs.Filter = $"PURCHCD IN ({string.Join(",", listPurchCd)})";
                else
                    _bs.Filter = $"PURCHCD = -1";

            }
        }

        public void print()
        {
            string ordDate = ConvertUtil.ToString(_jobj["ORD_DATE"]);
            string shopNm = ConvertUtil.ToString(_jobj["SHOPNM"]);
            _bs.Filter = null;
            _bs.Sort = "PURCHNM ASC";

            //파일 저장 위치 선택.
            SaveFileDialog saveDlg = new SaveFileDialog();
            //saveDlg.InitialDirectory = System.Environment.CurrentDirectory;
            saveDlg.InitialDirectory = "C:\\bonclt\\ISENDFILE\\";
            saveDlg.Title = "주문도서 내보내기";
            saveDlg.DefaultExt = "txt";
            saveDlg.FileName = $"{ordDate}.Txt";
            saveDlg.Filter = "txt (*txt)|*.txt|All files (*.*)|*.*";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                DataView dv = new DataView(_dt);
                dv.Sort = "PURCHNM ASC, BOOKNM ASC";

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("DATE", typeof(string)));
                dt.Columns.Add(new DataColumn("TEL", typeof(string)));
                dt.Columns.Add(new DataColumn("FAX", typeof(string)));
                dt.Columns.Add(new DataColumn("PURCHNM", typeof(string)));
                dt.Columns.Add(new DataColumn("A", typeof(string)));
                dt.Columns.Add(new DataColumn("BOOKNM", typeof(string)));
                dt.Columns.Add(new DataColumn("PUBSHNM", typeof(string)));
                dt.Columns.Add(new DataColumn("AUTHORNM", typeof(string)));
                dt.Columns.Add(new DataColumn("PRICE", typeof(string)));
                dt.Columns.Add(new DataColumn("ORD_COUNT", typeof(string)));
                dt.Columns.Add(new DataColumn("B", typeof(string)));
                dt.Columns.Add(new DataColumn("SHOPNM", typeof(string)));

                int purchCd;
                int index = 1;
                int order;
                int bookOrder;
                Dictionary<int, int> dicPurchOrder = new Dictionary<int, int>();
                Dictionary<int, int> dicPurchBookOrder = new Dictionary<int, int>();

                foreach (DataRowView row in dv)
                {
                    purchCd = ConvertUtil.ToInt32(row["PURCHCD"]);

                    if (dicPurchOrder.ContainsKey(purchCd))
                        order = dicPurchOrder[purchCd];
                    else
                    {
                        order = index;
                        dicPurchOrder.Add(purchCd, index);
                        index++;
                    }

                    if (dicPurchBookOrder.ContainsKey(purchCd))
                        bookOrder = ++dicPurchBookOrder[purchCd];
                    else
                    {
                        bookOrder = 1;
                        dicPurchBookOrder.Add(purchCd, bookOrder);
                    }

                    DataRow dr = dt.NewRow();
                    dr["DATE"] = $"O{ordDate}{order}";
                    dr["TEL"] = ConvertUtil.ToString(row["TEL"]);
                    dr["FAX"] = ConvertUtil.ToString(row["FAX"]);
                    dr["PURCHNM"] = ConvertUtil.ToString(row["PURCHNM"]);
                    dr["A"] = "1";
                    dr["BOOKNM"] = $"{string.Format("{0:D3}", bookOrder)}{string.Format("{0:D13}", row["BOOKCD"])}{row["BOOKNM"]}";
                    dr["PUBSHNM"] = ConvertUtil.ToString(row["PUBSHNM"]);
                    dr["AUTHORNM"] = ConvertUtil.ToString(row["AUTHORNM"]);
                    dr["PRICE"] = ConvertUtil.ToString(row["PRICE"]);
                    dr["ORD_COUNT"] = ConvertUtil.ToString(row["ORD_COUNT"]);
                    dr["B"] = "000";
                    dr["SHOPNM"] = shopNm;
                    dt.Rows.Add(dr);
                }


                //파일 저장을 위해 스트림 생성.
                FileStream fs = new FileStream(saveDlg.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                //컬럼 이름들을 ","로 나누고 저장.
                //string line = string.Join(",", dt.Columns.Cast<object>());
                //sw.WriteLine(line);

                //row들을 ","로 나누고 저장.
                string line;
                foreach (DataRow item in dt.Rows)
                {
                    line = string.Join("\t", item.ItemArray.Cast<object>());
                    sw.WriteLine(line);
                }

                sw.Close();
                fs.Close();

                if (Dangol.MessageYN("파일 내보내기를 완료 했습니다.\r\n해당 파일을 확인하시겠습니까?") == DialogResult.Yes)
                {
                    Process.Start(saveDlg.FileName);
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

        private void usrBookOrderResultList_KeyDown(object sender, KeyEventArgs e)
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

        private void riseCnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowHandle = gvList.FocusedRowHandle;
                if (rowHandle < _dt.Rows.Count - 1)
                    SetColFocus("ORD_COUNT", rowHandle + 1);
            }
        }
    }
}
