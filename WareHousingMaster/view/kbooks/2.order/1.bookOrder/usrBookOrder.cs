using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WareHousingMaster.view.common;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrBookOrder : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _drv;
        bool _isPreOrderExist;

        int _shopCd;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrBookOrder()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3\n실적조회", "F4", "F5", "F6", "F7\n취소", "F8\n확정", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, false, false, false, true, true, false, true };


            _isPreOrderExist = false;
            _shopCd = 1;
        }


        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrBookOrderSearch1.searchHandler += new usrBookOrderSearch.SearchHandler(searchList);
            usrBookOrderSearch1.clearHandler += new usrBookOrderSearch.ClearHandler(clear);
            //usrBookOrderSearch1.cancelHandler += new usrBookOrderSearch.CancelHandler(cancel);
            //usrBookOrderSearch1.insertHandler += new usrBookOrderSearch.InsertHandler(insert);
            //usrBookOrderSearch1.deleteRowHandler += new usrBookOrderSearch.DeleteRowHandler(deleteRowHandler);


            usrBookOrderList1.getSearchInfoHandler += new usrBookOrderList.GetSearchInfoHandler(getSearchInfo);
            usrBookOrderList1.focusedRowObjectChangeHandler += new usrBookOrderList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrBookOrderSearch1.setInitLoad();
            usrBookOrderList1.setInitLoad();
            usrPreOrderBookList1.setInitLoad();
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            _shopCd = ConvertUtil.ToInt32(jobj["SHOPCD"]);
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.setCondition(jobj);
            //Language.SwitchToKorean();

            int rowHandle = 0;

            if (_isPreOrderExist)
            {
                DataTable dtPreOrder = usrPreOrderBookList1.getDataTable();
                usrBookOrderList1.setTableInitialize(dtPreOrder);

                rowHandle = dtPreOrder.Rows.Count;
                _isPreOrderExist = false;
                usrPreOrderBookList1.clear();
                layoutControlGroup1.BeginUpdate();
                lcPreOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlGroup1.EndUpdate();

                usrBookOrderList1.setTableEditable(true);
                usrBookOrderList1.SetFocus();
                usrBookOrderList1.SetColFocus("ORDER_CNT", 0);
            }
            else
            {
                usrBookOrderList1.setTableInitialize();
                usrBookOrderList1.setTableEditable(true);
                usrBookOrderList1.SetFocus();
                usrBookOrderList1.SetColFocus("BOOKNM", rowHandle);
            }

           
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrBookOrderSearch1.Searches();
                    break;
                case 2:
                    deleteRowHandler();
                    break;
                case 3:
                    showBookSalePerformance();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    cancel();
                    break;
                case 8:
                    insert();
                    break;
                case 9:
                    //this.Close();
                    break;
                case 10:
                    this.Close();
                    break;
                default:
                    break;

            }
        }

        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;
        }

        private void showBookSalePerformance()
        {
            if (_drv != null && ConvertUtil.ToInt64(_drv["BOOKCD"]) > 0)
            {
                using (dlgBookOutCome bookOutCome = new dlgBookOutCome(_shopCd, ConvertUtil.ToInt64(_drv["BOOKCD"]), 0))
                {
                    //bookDetail.StartPosition = FormStartPosition.Manual;
                    //bookDetail.Location = new Point(this.Location.X + (this.Size.Width / 2) - (bookDetail.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (bookDetail.Size.Height / 2));

                    if (bookOutCome.ShowDialog(this) == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                Dangol.Warining("선택된 데이터가 없습니다.");
            }
        }
        public void clear()
        {
            //usrBookOrderSearch1.clear();
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.setTableInitialize();
            usrBookOrderList1.setTableEditable(false);
            //usrBookOrderSearch1.setFocus();
        }

        public void cancel()
        {
            usrBookOrderSearch1.clear();
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.setTableInitialize();
            usrBookOrderList1.setTableEditable(false);
            usrBookOrderSearch1.setFocus();
        }
        private void insert()
        {
            //usrBookOrderList1.getList(jobj);
            if (usrBookOrderList1.insertOrder())
                cancel();
        }

        private JObject getSearchInfo()
        {
            return usrBookOrderSearch1.getSearchInfo();
        }

        private void deleteRowHandler()
        {
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.deleteRowHandler();
        }

        public void setPreBookOrderLayout(bool isShow)
        {
            if (isShow)
            {
                layoutControlGroup1.BeginUpdate();
                lcPreOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlGroup1.EndUpdate();
                _isPreOrderExist = true;
            }
                
        }

        public void setPreBookOrderData(DataRow[] rows)
        {
            usrPreOrderBookList1.setData(rows);
        }

        private void usrBookOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrBookOrder_Shown(object sender, EventArgs e)
        {
            usrBookOrderSearch1.setFocus();
        }
    }
}