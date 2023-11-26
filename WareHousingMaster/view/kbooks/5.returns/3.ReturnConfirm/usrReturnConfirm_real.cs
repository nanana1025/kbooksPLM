using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.returns
{
    public partial class usrReturnConfirm_real : DevExpress.XtraEditors.XtraForm
    {

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrReturnConfirm_real()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3\n실적조회", "F4\n화면인쇄", "F5\n인쇄", "F6", "F7\n취소", "F8\n확정", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, true, true, false, true, true, false, true };

        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrModifReturnSearch1.searchHandler += new usrModifReturnSearch.SearchHandler(searchList);
            usrModifReturnSearch1.clearHandler += new usrModifReturnSearch.ClearHandler(clear);
            //usrModifReturnSearch1.confirmHandler += new usrModifReturnSearch.ConfirmHandler(confirmHandler);
            //usrModifReturnSearch1.searchPerformanceHandler += new usrModifReturnSearch.SearchPerformanceHandler(searchPerformanceHandler);
            //usrModifReturnSearch1.deleteRowHandler += new usrModifReturnSearch.DeleteRowHandler(deleteRowHandler);

            //usrModifReturnList1.getSearchInfoHandler += new usrModifReturnList.GetSearchInfoHandler(getSearchInfo);

            setInitialize();
        }

        private void setInitialize()
        {
            usrModifReturnSearch1.setInitLoad();
            usrModifReturnList1.setInitLoad(2);
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

        }

        private void searchList(JObject jobj)
        {
            usrModifReturnList1.setCondition(jobj);
            //usrModifReturnList1.setTableInitialize();
            //usrModifReturnList1.setTableEditable(true);

            usrModifReturnList1.getList();
            usrModifReturnList1.setFocus();

            DataTable dt = usrModifReturnList1.getTable();

            if (dt.Rows.Count > 0)
            {
                usrModifReturnList1.SetColFocus($"RETURN_CNT", 0);
            }

        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrModifReturnSearch1.Search();
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
                    usrModifReturnSearch1.clear();
                    usrModifReturnList1.setTableInitialize();
                    usrModifReturnSearch1.setFocus();
                    break;
                case 8:
                    confirmHandler();
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

        private void reset(bool isCheck = true)
        {
            if (isCheck)
            {
                if (usrModifReturnList1.isDataClearAvailable())
                {
                    if (Dangol.MessageYN("처리되지 않은 데이터가 존재합니다. 그래도 취소하시겠습니까?") == DialogResult.No)
                        return;
                }
            }

            usrModifReturnSearch1.clear();
            usrModifReturnList1.setTableInitialize();
            usrModifReturnSearch1.setFocus();
        }

        private void clear()
        {
            usrModifReturnList1.setTableInitialize();
        }

        private void searchPerformanceHandler()
        {
            usrModifReturnList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            usrModifReturnList1.deleteRowHandler();
        }

        private void confirmHandler()
        {
            if (usrModifReturnList1.insertReturn())
                reset(false);
        }

        private void usrModifReturn_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrModifReturn_Shown(object sender, EventArgs e)
        {
            usrModifReturnSearch1.setFocus();
        }

    }
}