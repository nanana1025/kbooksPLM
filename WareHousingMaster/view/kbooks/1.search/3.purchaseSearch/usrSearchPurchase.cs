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
using Enum = WareHousingMaster.view.common.Enum;
using DevExpress.XtraTab;

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

        public usrSearchPurchase()
        {
            InitializeComponent();

            _shopCd = 1;
            _purchCd = -1;
            _purchCd4Book = -1;
            _bookCd = -1;
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

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
        }

        private void searchList(JObject jobj)
        {
            _shopCd = ConvertUtil.ToInt32(jobj["SHOPCD"]);
            usrSearchPurchaseList1.getList(jobj);
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
    }
}