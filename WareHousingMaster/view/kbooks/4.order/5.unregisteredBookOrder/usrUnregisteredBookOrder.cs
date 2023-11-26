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
    public partial class usrUnregisteredBookOrder : DevExpress.XtraEditors.XtraForm
    {

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrUnregisteredBookOrder()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3", "F4", "F5", "F6", "F7\n취소", "F8\n확정", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, false, false, false, false, true, true, false, true };
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrUnregisteredBookOrderSearch1.searchHandler += new usrUnregisteredBookOrderSearch.SearchHandler(searchList);
            usrUnregisteredBookOrderSearch1.clearHandler += new usrUnregisteredBookOrderSearch.ClearHandler(clear);
            //usrUnregisteredBookOrderSearch1.deleteHandler += new usrUnregisteredBookOrderSearch.DeleteHandler(delete);
            //usrUnregisteredBookOrderSearch1.confirmHandler += new usrUnregisteredBookOrderSearch.ConfirmHandler(confirm);

            setInitialize();
        }

        private void setInitialize()
        {
            usrUnregisteredBookOrderSearch1.setInitLoad();
            usrUnregisteredBookOrderList1.setInitLoad();
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            usrUnregisteredBookOrderList1.setTableInitialize();
            usrUnregisteredBookOrderList1.getList(jobj);

            DataTable dt = usrUnregisteredBookOrderList1.getTable();
            usrUnregisteredBookOrderList1.setFocus();
            usrUnregisteredBookOrderList1.SetColFocus("BOOKNM", dt.Rows.Count);
        }
        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrUnregisteredBookOrderSearch1.Search();
                    break;
                case 2:
                    delete();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    usrUnregisteredBookOrderSearch1.clear();
                    usrUnregisteredBookOrderList1.clear();
                    usrUnregisteredBookOrderSearch1.setFocus();
                    break;
                case 8:
                    confirm();
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
            usrUnregisteredBookOrderList1.clear();
        }
        private void delete()
        {
            usrUnregisteredBookOrderList1.deleteRowHandler();
        }
        private void confirm()
        {
            usrUnregisteredBookOrderList1.insertHandler();
        }

        private void usrUnregisteredBookOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrUnregisteredBookOrder_Shown(object sender, EventArgs e)
        {
            usrUnregisteredBookOrderSearch1.setFocus();
        }
    }
}