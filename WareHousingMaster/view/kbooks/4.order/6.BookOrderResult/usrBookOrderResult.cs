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
    public partial class usrBookOrderResult : DevExpress.XtraEditors.XtraForm
    {

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrBookOrderResult()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2", "F3", "F4", "F5", "F6\n인터넷주문", "F7\n취소", "F8", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, false, false, false, false, true, true, false, false, true };
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrBookOrderResultSearch1.searchHandler += new usrBookOrderResultSearch.SearchHandler(searchList);
            usrBookOrderResultSearch1.clearHandler += new usrBookOrderResultSearch.ClearHandler(clear);
            //usrBookOrderResultSearch1.printHandler += new usrBookOrderResultSearch.PrintHandler(print);
            //usrBookOrderResultSearch1.filterHandler += new usrBookOrderResultSearch.FilterHandler(filter);

            setInitialize();
        }

        private void setInitialize()
        {
            usrBookOrderResultSearch1.setInitLoad();
            usrBookOrderResultList1.setInitLoad();
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            usrBookOrderResultList1.getList(jobj);
            usrBookOrderResultList1.setFocus();
            DataTable dt = usrBookOrderResultList1.getTable();
            if(dt.Rows.Count > 0)
                usrBookOrderResultList1.SetColFocus("ORD_COUNT", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrBookOrderResultSearch1.Search();
                    break;
                case 2:
                    usrBookOrderResultSearch1.filter();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    print();
                    break;
                case 7:
                    usrBookOrderResultSearch1.clear();
                    usrBookOrderResultList1.clear();
                    usrBookOrderResultSearch1.setFocus();
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

        private void clear()
        {
            usrBookOrderResultList1.clear();
        }

        private void print()
        {
            usrBookOrderResultList1.print();
        }

        private void filter(bool isCheck, int filterCnt)
        {
            usrBookOrderResultList1.filter(isCheck, filterCnt);
        }

        private void usrBookOrderResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrBookOrderResult_Shown(object sender, EventArgs e)
        {
            usrBookOrderResultSearch1.setFocus();
        }
    }
}