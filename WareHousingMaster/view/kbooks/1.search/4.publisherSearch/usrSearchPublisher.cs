using DevExpress.XtraTab;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
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
        

        public usrSearchPublisher()
        {
            InitializeComponent();

            _shopCd = 1;
            _publiserCd = -1;
            _bookCd = -1;
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSearchSellerAndPublisher1.searchHandler += new usrSearchPurchaseAndPublisher.SearchHandler(searchList);
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

        }

        private void searchList(JObject jobj)
        {
            _shopCd = ConvertUtil.ToInt32(jobj["SHOPCD"]);
            usrSearchPublisherList1.getList(jobj);
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

        private void xtcPublisher_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            
        }
    }
}