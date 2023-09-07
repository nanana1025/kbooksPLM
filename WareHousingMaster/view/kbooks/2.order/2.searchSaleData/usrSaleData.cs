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
    public partial class usrSaleData : DevExpress.XtraEditors.XtraForm
    {
        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrSaleData()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2", "F3", "F4", "F5", "F6", "F7\n취소", "F8\n확정", "F9\n닫기", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, false, false, false, false, false, true, true, true, true };
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrSaleDataSearch1.searchHandler += new usrSaleDataSearch.SearchHandler(searchList);
            usrSaleDataSearch1.saveHandler += new usrSaleDataSearch.SaveHandler(save);
            

            setInitialize();
        }

        private void setInitialize()
        {
            usrSaleDataSearch1.setInitLoad();
            usrSaleDataList1.setInitLoad();

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            usrSaleDataList1.getList(jobj);
            usrSaleDataList1.setFocus();
            DataTable dt = usrSaleDataList1.getTable();
            if (dt.Rows.Count > 0)
                usrSaleDataList1.SetColFocus("ORDER_CNT", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrSaleDataSearch1.Search();
                    break;
                case 2:
                    //usrSearchBookSearchList1.sortList("BOOKNM");
                    break;
                case 3:
                    //usrSearchBookSearchList1.sortList("AUTHOR1");
                    break;
                case 4:
                    //usrSearchBookSearchList1.sortList("PUBSHNM");
                    break;
                case 5:
                    break;
                case 6:
                    //usrSearchBookSearchList1.showBookInfoDetail();
                    break;
                case 7:
                    usrSaleDataSearch1.clear();
                    usrSaleDataList1.clear();
                    usrSaleDataSearch1.setFocus();
                    break;
                case 8:
                    save();
                    break;
                case 9:
                    this.Close();
                    break;
                case 10:
                    this.Close();
                    //usrSearchBookSearchList1.clearGridView();
                    //Dangol.Message("1111");
                    //usrSearchBookSearch1.Focus();
                    break;
                default:
                    break;
            }
        }

        private void save()
        {
            usrSaleDataList1.save();
        }

        private void usrSaleData_Shown(object sender, EventArgs e)
        {
            usrSaleDataSearch1.setFocus();
        }

        private void usrSaleData_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }
    }
}