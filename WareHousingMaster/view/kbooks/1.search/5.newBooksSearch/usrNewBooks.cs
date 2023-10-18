using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrNewBooks : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _drv;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrNewBooks()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조회", "F2\n도서상세정보", "F3\n엑셀저장", "F4", "F5", "F6", "F7\n선택주문", "F8\n취소", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, false, false, false, true, true, false, true };

        }


        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrNewBooksSearch1.searchHandler += new usrNewBooksSearch.SearchHandler(searchList);
            usrNewBooksSearch1.clearHandler += new usrNewBooksSearch.ClearHandler(clear);
            usrNewBooksList1.focusedRowObjectChangeHandler += new usrNewBooksList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);
            

            setInitialize();
        }

        private void setInitialize()
        {
            usrNewBooksSearch1.setInitLoad();
            usrNewBooksList1.setInitLoad();

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

        }

        private void searchList(JObject jobj)
        {
            usrNewBooksList1.getList(jobj);
            usrNewBooksList1.setFocus();

            DataTable dt = usrNewBooksList1.getTable();
            if (dt.Rows.Count > 0)
                usrNewBooksList1.SetColFocus("BOOKNM", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrNewBooksSearch1.Search();
                    break;
                case 2:
                    usrNewBooksList1.showBookInfoDetail();
                    break;
                case 3:
                    usrNewBooksList1.exportFile();
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
                    goOrderBook();
                    break;
                case 8:
                    usrNewBooksList1.clear();
                    usrNewBooksSearch1.clear();
                    usrNewBooksSearch1.setFocus();
                    break;
                case 9:
                    //this.Close();
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
        private void clear()
        {
            usrNewBooksList1.clear();
        }
        private void goOrderBook()
        {
            DataRow[] rows = usrNewBooksList1.getCheckedList();

            if (rows == null)
                Dangol.Warining("주문가능한 도서가 없습니다.");
            else
            {
                string tabName = "주문 예정입력";

                if (!(ProjectInfo._tabbedView.Documents.Any(x => x.Form.Tag.ToString() == tabName) || ProjectInfo._tabbedView.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
                {
                    Dangol.ShowSplash();
                    ProjectInfo._usrBookOrder = new usrBookOrder();
                    ProjectInfo._usrBookOrder.Tag = tabName;
                    ProjectInfo._usrBookOrder.MdiParent = this.MdiParent;

                    ProjectInfo._usrBookOrder.setPreBookOrderLayout(true);
                    ProjectInfo._usrBookOrder.setPreBookOrderData(rows);
                    Dangol.CloseSplash();

                    ProjectInfo._usrBookOrder.Show();
                    //ProjectInfo._biusrUsedPurchaseReceiptDetail.Caption = "중고매입상세";
                    if (!ProjectInfo._ribbonTabs.ContainsKey(ProjectInfo._bbiOrderCartInfo))
                        ProjectInfo._ribbonTabs.Add(ProjectInfo._bbiOrderCartInfo, ProjectInfo._usrBookOrder);
                    else
                    {
                        ProjectInfo._ribbonTabs.Remove(ProjectInfo._bbiOrderCartInfo);
                        ProjectInfo._ribbonTabs.Add(ProjectInfo._bbiOrderCartInfo, ProjectInfo._usrBookOrder);
                    }
                }
                else
                {
                    Dangol.ShowSplash();
                    ProjectInfo._usrBookOrder.cancel();
                    ProjectInfo._usrBookOrder.setPreBookOrderLayout(true);
                    ProjectInfo._usrBookOrder.setPreBookOrderData(rows);
                    ProjectInfo._documentManager.View.ActivateDocument(ProjectInfo._ribbonTabs[ProjectInfo._bbiOrderCartInfo]);
                    Dangol.CloseSplash();
                    ProjectInfo._usrBookOrder.Show();
                }
            }
        }

        private void lcgNewBookList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                usrNewBooksList1.showBookInfoDetail();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                usrNewBooksList1.exportFile();
            }
        }

        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;

        }

        private void usrNewBooks_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrNewBooks_Shown(object sender, EventArgs e)
        {
            usrNewBooksSearch1.setFocus();
        }
    }
}