using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.returns
{
    public partial class usrInputReturn : DevExpress.XtraEditors.XtraForm
    {

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;


        public usrInputReturn()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3\n실적조회", "F4", "F5", "F6", "F7\n취소", "F8\n확정", "F9\n닫기", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, false, false, false, true, false, true, true };

        }


        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrInputReturnSearch1.searchHandler += new usrInputReturnSearch.SearchHandler(searchList);
            //usrInputReturnSearch1.confirmHandler += new usrInputReturnSearch.ConfirmHandler(confirmHandler);
            //usrInputReturnSearch1.searchPerformanceHandler += new usrInputReturnSearch.SearchPerformanceHandler(searchPerformanceHandler);
            //usrInputReturnSearch1.deleteRowHandler += new usrInputReturnSearch.DeleteRowHandler(deleteRowHandler);

            //usrInputReturnList1.getSearchInfoHandler += new usrInputReturnList.GetSearchInfoHandler(getSearchInfo);

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

            setInitialize();
        }

        private void setInitialize()
        {
            usrInputReturnSearch1.setInitLoad();
            usrInputReturnList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrInputReturnList1.setCondition(jobj);
            usrInputReturnList1.setTableInitialize();
            //usrInputReturnList1.setTableEditable(true);
            usrInputReturnList1.SetFocus();
            usrInputReturnList1.SetColFocus("BOOKNM", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrInputReturnSearch1.Search();
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
                    usrInputReturnSearch1.clear();
                    usrInputReturnList1.setTableInitialize();
                    //usrInputReturnList1.setTableEditable(false);
                    usrInputReturnSearch1.setFocus();
                    break;
                case 8:
                    confirmHandler();
                    break;
                case 9:
                    this.Close();
                    break;
                case 10:
                    this.Close();
                    break;
                default:
                    break;

            }
        }

        private void searchPerformanceHandler()
        {
            usrInputReturnList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            usrInputReturnList1.deleteRowHandler();
        }

        private void confirmHandler()
        {
            usrInputReturnList1.insertReturn();
        }

        private void usrInputReturn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrInputReturn_Shown(object sender, EventArgs e)
        {
            usrInputReturnSearch1.setFocus();
        }

        //private JObject getSearchInfo()
        //{
        //    return usrInputReturnSearch1.getSearchInfo();
        //}

    }
}