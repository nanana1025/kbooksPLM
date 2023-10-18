using DevExpress.XtraTab;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSearchPublisher : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _drv;
        DataRowView _drvBook;

        int _shopCd;
        long _publiserCd;
        long _bookCd;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;
        public usrSearchPublisher()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조회", "F2", "F3", "F4", "F5", "F6", "F7\n선택주문", "F8\n취소", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, false, false, false, false, false, true, true, false, true };

            _shopCd = 1;
            _publiserCd = -1;
            _bookCd = -1;
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrSearchSellerAndPublisher1.searchHandler += new usrSearchPurchaseAndPublisher.SearchHandler(searchList);
            usrSearchSellerAndPublisher1.clearHandler += new usrSearchPurchaseAndPublisher.ClearHandler(clear);
            usrSearchPublisherList1.focusedRowObjectChangeHandler += new usrSearchPublisherList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);
            usrSearchBookSearchList1.focusedRowObjectChangeHandler += new usrSearchBookSearchList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeBookHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrSearchBookSearchList1.setinitialize();

            usrSearchSellerAndPublisher1.setInitLoad((int)Enum.SearchType.PUBLISHER);
            usrSearchBookSearchList1.setGreidView((int)view.common.Enum.SearchListType.PUBLISHER);

            usrSearchPublisherOutcome1.setInitLoad();
            usrSearchPublisherList1.setInitLoad();

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

        }

        private void searchList(JObject jobj)
        {
            _shopCd = ConvertUtil.ToInt32(jobj["SHOPCD"]);
            usrSearchPublisherList1.getList(jobj);
            usrSearchPublisherList1.setFocus();

            DataTable dt = usrSearchPublisherList1.getTable();
            if (dt.Rows.Count > 0)
                usrSearchPublisherList1.SetColFocus("PUBSHNM", 0);
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
                    clear(false);
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

        private void clear(bool isSearch = true)
        {
            xtcPublisher.SelectedTabPage = xtPubliserList;
            usrSearchPublisherList1.clear();
            if (!isSearch)
            {
                usrSearchSellerAndPublisher1.clear();
                usrSearchSellerAndPublisher1.setFocus();
            }
        }

        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;

            if(_drv == null)
                usrPublisherDetail1.setReset();
            else
                usrPublisherDetail1.setData(drv);
        }

        private void FocusedRowObjectChangeBookHandler(DataRowView drvBook)
        {
            _drvBook = drvBook;
        }

        private void stcPublisher_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void stcPublisher_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.PrevPage != null)
            {
                XtraTabPage deselectedPage = e.Page as XtraTabPage;

                if (deselectedPage.Name.Equals("xtPubliserBook"))
                {
                    long publisherCd;

                    if (_drv == null)
                    {
                        publisherCd = -2;
                    }
                    else
                    {
                        publisherCd = ConvertUtil.ToInt64(_drv["PUBSHCD"]);
                    }

                    if (publisherCd != _publiserCd)
                    {
                        _publiserCd = publisherCd;
                        if (publisherCd < 0)
                            usrSearchBookSearchList1.clearGridView();
                        else
                        {
                            JObject obj = new JObject();
                            obj.Add("SHOPCD", _shopCd);
                            obj.Add("PUBSHCD", publisherCd);

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

        private void xtcPublisher_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            
        }

        private void usrSearchPublisher_Shown(object sender, EventArgs e)
        {
            usrSearchSellerAndPublisher1.setFocus();
        }

        private void usrSearchPublisher_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }
    }
}