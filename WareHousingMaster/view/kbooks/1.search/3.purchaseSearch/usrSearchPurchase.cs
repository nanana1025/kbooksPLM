using DevExpress.XtraTab;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrSearchPurchase : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _drv;
        DataRowView _drvBook;

        int _shopCd;
        long _purchCd;
        long _purchCd4Book;
        long _bookCd;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrSearchPurchase()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조회", "F2", "F3", "F4", "F5", "F6", "F7\n선택주문", "F8\n취소", "F9\n닫기", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, false, false, false, false, false, true, true, true, true };

            _shopCd = 1;
            _purchCd = -1;
            _purchCd4Book = -1;
            _bookCd = -1;
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrSearchSellerAndPublisher1.searchHandler += new usrSearchPurchaseAndPublisher.SearchHandler(searchList);
            usrSearchPurchaseList1.focusedRowObjectChangeHandler += new usrSearchPurchaseList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);
            usrSearchBookSearchList1.focusedRowObjectChangeHandler += new usrSearchBookSearchList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeBookHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrSearchBookSearchList1.setInitLoad();

            usrSearchSellerAndPublisher1.setInitLoad((int)Enum.SearchType.SELLER);
            usrSearchBookSearchList1.setGreidView((int)view.common.Enum.SearchListType.PURCHASE);
            usrSearchPublisherOutcome1.setInitLoad();
            usrSearchPurchaseList1.setInitLoad();
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            _shopCd = ConvertUtil.ToInt32(jobj["SHOPCD"]);
            usrSearchPurchaseList1.getList(jobj);
            usrSearchPurchaseList1.setFocus();

            DataTable dt = usrSearchPurchaseList1.getTable();
            if (dt.Rows.Count > 0)
                usrSearchPurchaseList1.SetColFocus("PURCHNM", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrSearchSellerAndPublisher1.Search();
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
                    goOrderBook();
                    break;
                case 8:
                    xtcSellerList.SelectedTabPage = xtPurchasedList;
                    usrSearchPurchaseList1.clear();
                    usrSearchSellerAndPublisher1.clear();
                    usrSearchSellerAndPublisher1.setFocus();
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

        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;

            //if (_drv == null)
            //    usrSearchPurchaseDetail1.setReset();
            //else
            //{

            //}
            //    usrSearchPurchaseDetail1.setData(drv);
        }

        private void FocusedRowObjectChangeBookHandler(DataRowView drvBook)
        {
            _drvBook = drvBook;
        }

        private void xtcSellerList_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            
        }

        private void xtcSellerList_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.PrevPage != null)
            {
                XtraTabPage deselectedPage = e.Page as XtraTabPage;

                if (deselectedPage.Name.Equals("xtPurchasedDetail"))
                {
                    long purchCd;

                    if (_drv == null)
                    {
                        purchCd = -2;
                    }
                    else
                    {
                        purchCd = ConvertUtil.ToInt64(_drv["PURCHCD"]);
                    }

                    if (purchCd != _purchCd)
                    {
                        _purchCd = purchCd;
                        if (purchCd < 0)
                            usrSearchPurchaseDetail1.setReset();
                        else
                        {
                            JObject obj = new JObject();
                            obj.Add("SHOPCD", _shopCd);
                            obj.Add("PURCHCD", purchCd);

                            usrSearchPurchaseDetail1.getData(obj);
                        }
                    }
                }
                else if (deselectedPage.Name.Equals("xtBook"))
                {
                    long purchCd;

                    if (_drv == null)
                    {
                        purchCd = -2;
                    }
                    else
                    {
                        purchCd = ConvertUtil.ToInt64(_drv["PURCHCD"]);
                    }

                    if (purchCd != _purchCd4Book)
                    {
                        _purchCd4Book = purchCd;
                        if (purchCd < 0)
                            usrSearchBookSearchList1.clearGridView();
                        else
                        {
                            JObject obj = new JObject();
                            obj.Add("SHOPCD", _shopCd);
                            obj.Add("PURCHCD", purchCd);
                            obj.Add("TAX_FG", 2);
                            
                            usrSearchBookSearchList1.getList(obj);

                        }
                    }
                }
                else if (deselectedPage.Name.Equals("xtPerformance"))
                {
                    long bookCd;

                    if (_drvBook == null)
                    {
                        bookCd = -2;
                    }
                    else
                    {
                        bookCd = ConvertUtil.ToInt64(_drvBook["BOOKCD"]);
                    }

                    if (bookCd != _bookCd)
                    {
                        _bookCd = bookCd;
                        if (bookCd < 0)
                            usrSearchPublisherOutcome1.clearGridView();
                        else
                        {
                            JObject obj = new JObject();
                            obj.Add("SHOPCD", _shopCd);
                            obj.Add("BOOKCD", bookCd);
                            obj.Add("PURCHCD", _purchCd4Book);

                            //SHLEE-수정
                            var today = Convert.ToDateTime("2023-03-10");

                            obj.Add("DT_FROM", today.AddDays(-10).ToString("yyyyMMdd"));
                            obj.Add("DT_TO", today.ToString("yyyyMMdd"));

                            //obj.Add("DT_FROM", DateTime.Today.AddDays(-10).ToString("yyyyMMdd"));
                            //obj.Add("DT_TO", DateTime.Now.ToString("yyyyMMdd"));

                            usrSearchPublisherOutcome1.getList(obj);
                        }
                    }
                }
            }
        }

        private void goOrderBook()
        {
            DataRow[] rows = usrSearchBookSearchList1.getCheckedList();

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

        private void usrSearchPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrSearchPurchase_Shown(object sender, EventArgs e)
        {
            usrSearchSellerAndPublisher1.setFocus();
        }
    }
}