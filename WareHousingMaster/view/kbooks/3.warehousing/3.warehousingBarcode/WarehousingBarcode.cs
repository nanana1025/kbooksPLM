using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.warehouisng
{
    public partial class WarehousingBarcode : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _currentRow;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;


        public WarehousingBarcode()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3", "F4", "F5", "F6", "F7\n취소", "F8\n발행", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, false, false, false, false, true, true, false, true };

        }


        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            warehousingBarcodeSearch1.searchHandler += new WarehousingBarcodeSearch.SearchHandler(searchList);
            warehousingBarcodeSearch1.clearHandler += new WarehousingBarcodeSearch.ClearHandler(clear);

            warehousingBarcodeList1.focusedRowObjectChangeHandler += new WarehousingBarcodeList.FocusedRowObjectChangeHandler(focusedRowObjectChangeHandler);
            warehousingBarcodeList1.refreshHandler += new WarehousingBarcodeList.RefreshHandler(refreshHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
            warehousingBarcodeSearch1.setInitLoad();
            warehousingBarcodeList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            int bookType = ConvertUtil.ToInt32(jobj["BOOK_TYPE"]);
            int processType = ConvertUtil.ToInt32(jobj["PROCESS_TYPE"]);

            warehousingBarcodeList1.setCondition(jobj, bookType, processType);
            if (bookType == 2 || processType == 1)
            {
                warehousingBarcodeList1.setTableInitialize(jobj);
                warehousingBarcodeList1.SetFocus();
                DataTable dt = warehousingBarcodeList1.getDataTable();

                if (dt.Rows.Count > 0)
                {
                    warehousingBarcodeList1.SetColFocus("INP_COUNT");
                }
            }
            else
            {
                warehousingBarcodeList1.setTableInitialize();
                warehousingBarcodeList1.SetFocus();
                warehousingBarcodeList1.SetColFocus("BOOKCD");
            }

            //usrInputReturnList1.SetFocus();
            //usrInputReturnList1.SetColFocus("BOOKNM", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    warehousingBarcodeSearch1.Search();
                    break;
                case 2:
                    deleteRowHandler();
                    break;
                case 3:
                    //searchPerformanceHandler();
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
                    //reset();
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
            //warehousingBarcodeSearch1.refreshPurchase();
        }

        private void reset()
        {
            if (Dangol.MessageYN("취소하시겠습니까?") == DialogResult.Yes)
            {
                warehousingBarcodeList1.setTableInitialize();
                warehousingBarcodeSearch1.setFocus();
            }
            //usrInputReturnList1.setTableInitialize();

            //usrInputReturnSearch1.setFocus();
        }

        private void clear()
        {
            warehousingBarcodeList1.setTableInitialize();
        }

        private void searchPerformanceHandler()
        {
            //usrInputReturnList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            warehousingBarcodeList1.deleteData();
        }

        private void confirmHandler()
        {
            warehousingBarcodeList1.barcodePrint();
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