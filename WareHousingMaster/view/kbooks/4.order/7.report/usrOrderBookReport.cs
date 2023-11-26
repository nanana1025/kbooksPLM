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

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrOrderBookReport : DevExpress.XtraEditors.XtraForm
    {

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrOrderBookReport()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2", "F3", "F4", "F5", "F6\n인쇄", "F7\n취소", "F8", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, false, false, false, false, true, true, false, false, true };

        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrOrderBookReportSearch1.searchHandler += new usrOrderBookReportSearch.SearchHandler(searchList);
            usrOrderBookReportSearch1.clearHandler += new usrOrderBookReportSearch.ClearHandler(clear);
            //usrOrderBookReportSearch1.searchPerformanceHandler += new usrOrderBookReportSearch.SearchPerformanceHandler(searchPerformanceHandler);
            //usrOrderBookReportSearch1.deleteRowHandler += new usrOrderBookReportSearch.DeleteRowHandler(deleteRowHandler);
            //usrOrderBookReportSearch1.confirmHandler += new usrOrderBookReportSearch.ConfirmHandler(PrintHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrOrderBookReportSearch1.setInitLoad();
            usrOrderBookReportList1.setInitLoad();

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

        }

        private void searchList(JObject jobj)
        {
            usrOrderBookReportList1.setCondition(jobj);
            usrOrderBookReportList1.getList(jobj);
            usrOrderBookReportList1.SetFocus();
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrOrderBookReportSearch1.Search();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    PrintHandler();
                    break;
                case 7:
                    usrOrderBookReportSearch1.clear();
                    usrOrderBookReportList1.clear();
                    usrOrderBookReportSearch1.setFocus();
                    break;
                case 8:
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

        //private void searchPerformanceHandler()
        //{
        //    usrOrderBookReportList1.getPerformance();
        //}

        //private void deleteRowHandler()
        //{
        //    usrOrderBookReportList1.deleteRow();
        //}

        private void clear()
        {
            usrOrderBookReportList1.clear();
        }

        private void PrintHandler()
        {
            usrOrderBookReportList1.print();
        }

        private void usrOrderBookReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrOrderBookReport_Shown(object sender, EventArgs e)
        {
            usrOrderBookReportSearch1.setFocus();
        }
    }
}