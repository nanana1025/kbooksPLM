using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.kbooks.warehouisng;

namespace WareHousingMaster.view.kbooks.payment
{
    public partial class PaymentTrading : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _currentRow;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;


        public PaymentTrading()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3\n실적조회", "F4", "F5", "F6", "F7\n취소", "F8\n확정", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, false, false, false, true, true, false, true };

        }


        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);


            paymentTradingSearch1.searchHandler += new PaymentTradingSearch.SearchHandler(searchList);
            paymentTradingSearch1.clearHandler += new PaymentTradingSearch.ClearHandler(clear);

            paymentTradingList1.focusedRowObjectChangeHandler += new PaymentTradingList.FocusedRowObjectChangeHandler(focusedRowObjectChangeHandler);
            paymentTradingList1.refreshHandler += new PaymentTradingList.RefreshHandler(refreshHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
            paymentTradingSearch1.setInitLoad();

            //paymentTradingList1.setInitLoad();

           
        }

        private void searchList(JObject jobj)
        {

            //paymentTradingList1.setTableInitialize(jobj);
            //DataTable dt = paymentTradingList1.getDataTable();
            //paymentTradingList1.SetFocus();
            //paymentTradingList1.SetColFocus("INP_CNT", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    //paymentTradingSearch1.Searches();
                    break;
                case 2:
                    deleteRowHandler();
                    break;
                case 3:
                    searchPerformanceHandler();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    reset();
                    break;
                case 8:
                    confirmHandler();
                    reset();
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
        private void focusedRowObjectChangeHandler(DataRowView currentRow)
        {
            _currentRow = currentRow;
        }

        private void refreshHandler()
        {
            //paymentTradingSearch1.refreshPurchase();
        }

        private void reset()
        {
            //usrInputReturnSearch1.clear();
            //usrInputReturnList1.setTableInitialize();

            //usrInputReturnSearch1.setFocus();
        }

        private void clear()
        {
            //paymentTradingList1.setTableInitialize();
        }

        private void searchPerformanceHandler()
        {
            //usrInputReturnList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            //paymentTradingList1.deleteData();
        }

        private void confirmHandler()
        {
            //paymentTradingList1.confirmData();
        }

        private void usrInputReturn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrInputReturn_Shown(object sender, EventArgs e)
        {
            //usrInputReturnSearch1.setFocus();
        }

        //private JObject getSearchInfo()
        //{
        //    return usrInputReturnSearch1.getSearchInfo();
        //}

    }
}