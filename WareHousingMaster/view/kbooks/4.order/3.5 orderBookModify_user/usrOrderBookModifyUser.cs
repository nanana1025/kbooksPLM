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
    public partial class usrOrderBookModifyUser : DevExpress.XtraEditors.XtraForm
    {

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrOrderBookModifyUser()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3\n실적조회", "F4", "F5", "F6", "F7\n취소", "F8\n수정", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, false, false, false, true, true, false, true };
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrOrderBookModifySearch1.searchHandler += new usrOrderBookModifySearch.SearchHandler(searchList);
            usrOrderBookModifySearch1.clearHandler += new usrOrderBookModifySearch.ClearHandler(clear);
            //usrOrderBookModifySearch1.searchPerformanceHandler += new usrOrderBookModifySearch.SearchPerformanceHandler(searchPerformanceHandler);
            //usrOrderBookModifySearch1.deleteRowHandler += new usrOrderBookModifySearch.DeleteRowHandler(deleteRowHandler);
            //usrOrderBookModifySearch1.confirmHandler += new usrOrderBookModifySearch.ConfirmHandler(confirmHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrOrderBookModifySearch1.setInitLoad();
            usrOrderBookModifyList1.setInitLoad();

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            usrOrderBookModifyList1.setCondition(jobj);
            usrOrderBookModifyList1.getList(jobj);
            usrOrderBookModifyList1.setFocus();


            DataTable dt = usrOrderBookModifyList1.getTable();
            if (dt.Rows.Count > 0)
            {
                int colNum = 1;
                if (jobj.ContainsKey("STORE_TYPE"))
                {
                    if (ConvertUtil.ToString(jobj["STORE_TYPE"]).Equals("SINGLE"))
                        colNum = ConvertUtil.ToInt32(jobj["STORECD"]);
                    else
                        colNum = ConvertUtil.ToInt32(jobj["STORECD_S"]);
                }

                if (colNum > 15)
                    colNum = 1;

                usrOrderBookModifyList1.SetColFocus($"STORE{colNum}", 0);
            }
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrOrderBookModifySearch1.Search();
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
                    usrOrderBookModifySearch1.clear();
                    usrOrderBookModifyList1.clear();
                    usrOrderBookModifySearch1.setFocus();
                    break;
                case 8:
                    confirmHandler(1);
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
            usrOrderBookModifyList1.clear();
        }
        private void searchPerformanceHandler()
        {
            usrOrderBookModifyList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            usrOrderBookModifyList1.deleteRow();
        }

        private void confirmHandler(int userType)
        {
            usrOrderBookModifyList1.confirm(userType);
        }

        private void usrOrderBookModify_Shown(object sender, EventArgs e)
        {
            usrOrderBookModifySearch1.setFocus();
        }

        private void usrOrderBookModify_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }
    }
}