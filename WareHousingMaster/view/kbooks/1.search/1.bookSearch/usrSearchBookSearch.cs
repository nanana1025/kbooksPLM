using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSearchBookSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        //int _viewType;
        //int _searchType;
        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public delegate void ClearHandler();
        public event ClearHandler clearHandler;

        List<long> listShopCd;

        string _publiser;

        bool _isGetPublisherCd;

        //Dictionary<Keys, int> _dicKeys;
        public usrSearchBookSearch()
        {
            InitializeComponent();

            //_dicKeys = new Dictionary<Keys, int>
            //{
            //    { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            //};

            listShopCd = new List<long>();

            //_searchType = -1;

            _publiser = "-1";

            _isGetPublisherCd = false;
        }

        public void setInitLoad()
        {
            setInfoBox();
            setinitialize();
        }


        private void setInfoBox()
        {
            List<string> listLongCol = new List<string>(new string[] { "SHOPCD" });
            DataTable dtShopCd = Util.getTable("HMA09", "SHOPCD,SHOP_NM ", "SHOPCD > 0", "SHOPCD ASC", listLongCol);

            foreach (DataRow row in dtShopCd.Rows)
                listShopCd.Add(ConvertUtil.ToInt64(row["SHOPCD"]));
            
            Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");

        }

        private void setinitialize()
        {
            
        }

        public void setFocus()
        {
            //
            //this.ActiveControl = teTitle;
            teTitle.Focus();
        }
        public void setReleaseStateControl(bool isShow)
        {
            //Root.BeginUpdate();
            //if (isShow)
            //    lcReleaseState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //else
            //    lcReleaseState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //Root.EndUpdate();
        }

        public void setReleaseState(string releaseState)
        {
            //leState.EditValue = releaseState;
        }

        //public void setLayoutView(int viewType = (int)common.Enum.NRleaseProcessType.RELEASE)
        //{
        //    _viewType = viewType;

        //    if (viewType == (int)common.Enum.NRleaseProcessType.RELEASE)  //(int)common.Enum.NRleaseProcessType.RELEASE
        //    {
        //        lcReceiptNo.Text = "출고번호";
        //        lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReleaseCategory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //    }
        //    else if (viewType == (int)common.Enum.NRleaseProcessType.RETURN)
        //    {
        //        lcReceiptNo.Text = "반품번호";
        //        lcReleaseCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReleaseCategory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //    }
        //    else if (viewType == (int)common.Enum.NRleaseProcessType.CHANGE)
        //    {
        //        lcReceiptNo.Text = "교환번호";
        //        lcReleaseCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReleaseCategory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //    }
        //    else if (viewType == (int)common.Enum.NRleaseProcessType.AS)
        //    {
        //        lcReceiptNo.Text = "A/S번호";
        //        lcReleaseCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReleaseCategory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //    }
        //    else if (viewType == (int)common.Enum.NRleaseProcessType.ESTIMATE)
        //    {
        //        lcReceiptNo.Text = "견적번호";
        //        lcReleaseCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcReleaseCategory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //    }
        //    else if (viewType == (int)common.Enum.NRleaseProcessType.ACCOUNTS)
        //    {
        //        lcDt.Text = "완료일";
        //        lcReceiptNo.Text = "대표번호";
        //        lcReleaseCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //    }

        //    setInfoBox(viewType);
        //    Initialize();
        //}

        //public void setSearchStateView(int searchType = (int)common.Enum.NRleaseViewType.RECEIPT)
        //{
        //    _searchType = searchType;

        //    //if (searchType == (int)common.Enum.NRleaseViewType.ESTIMATE)
        //    //{
        //    //    setReleaseStateControl(true);
        //    //}
        //    //else if (searchType == (int)common.Enum.NRleaseViewType.RECEIPT)
        //    //{
        //    //    setReleaseStateControl(false);
        //    //}
        //    //else if (searchType == (int)common.Enum.NRleaseViewType.PROCESS)
        //    //{
        //    //    setReleaseStateControl(true);
        //    //}
        //    //else if (searchType == (int)common.Enum.NRleaseViewType.RELEASE)
        //    //{
        //    //    setReleaseStateControl(false);
        //    //}
        //    //else if (searchType == (int)common.Enum.NRleaseViewType.COMPLETE)
        //    //{
        //    //    setReleaseStateControl(false);
        //    //}
        //    //else if (searchType == (int)common.Enum.NRleaseViewType.ACCOUTNS)
        //    //{
        //    //    setReleaseStateControl(false);
        //    //}
        //}

        public void clear()
        {
            teTitle.Text = "";
            teAuthor.Text = "";
            tePublisherCd.Text = "";
            tePublisher.Text = "";
            _isGetPublisherCd = false;

            teTitle.Focus();
        }

        public JObject getSearch()
        {
            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                return jData;
            else
                return null;
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            clearHandler();

            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                searchHandler(jData);
            //else
            //    Dangol.Message(jData["MSG"]);
        }

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));

            if (!string.IsNullOrWhiteSpace(teTitle.Text))
            {
                string title = teTitle.Text.Trim();
                string titleExceptPercent = title.Replace("%", "");
                if(string.IsNullOrWhiteSpace(titleExceptPercent))
                {
                    Dangol.Warining("도서명을 입력하세요.");
                    return false;
                }
                else
                    jData.Add("BOOKNM", title);
            }
            else
            {
                Dangol.Warining("도서명을 입력하세요.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(teAuthor.Text))
                jData.Add("AUTHOR", teAuthor.Text.Trim());

            if (!string.IsNullOrWhiteSpace(tePublisherCd.Text))
            {
                if (Util.checkOnlyNumeric(tePublisherCd.Text))
                {
                    jData.Add("PUBSHCD", ConvertUtil.ToInt32(tePublisherCd.Text.Trim()));
                }
                else
                {
                    Dangol.Warining("출판사코드를 확인하세요.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(tePublisher.Text))
                jData.Add("PUBSHNM", tePublisher.Text.Trim());

            //if (!string.IsNullOrWhiteSpace(teGroupCd.Text))
            //    jData.Add("GROUPCD", ConvertUtil.ToInt32(teGroupCd.Text.Trim()));

            //if (!string.IsNullOrWhiteSpace(teGroup.Text))
            //    jData.Add("GROUP_NM", teGroup.Text.Trim());

            //if (!string.IsNullOrWhiteSpace(teStandCd.Text))
            //    jData.Add("STANDCD", ConvertUtil.ToInt32(teStandCd.Text.Trim()));

            //if (!string.IsNullOrWhiteSpace(teStand.Text))
            //    jData.Add("STAND_NM", teStand.Text.Trim());


            //if (date == 0)
            //{
            //    jData.Add("MSG", "접수일을 선택하지 않은 경우, 접수번호 또는 고객명은 필수로 입력되야 합니다.");
            //    return false;
            //}

            return true;
        }

        private void tePublisher_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(_isGetPublisherCd)
                {
                    search();
                }
                else
                {
                    string publisher = tePublisher.Text.Trim();
                    if (string.IsNullOrWhiteSpace(publisher) || _publiser.Equals(publisher))
                    {
                        sbSearch.Focus();
                    }
                    else
                    {
                        if(getPubliser())
                            sbSearch.Focus();
                        else
                            tePublisher.Focus();
                    }
                    
                }
               
            }
        }

        private bool getPubliser()
        {
            int shopCd = ConvertUtil.ToInt32(leShopCd.EditValue);
            string publisher = tePublisher.Text.Trim();

            using (dlgPublisherList publisherList = new dlgPublisherList(publisher, shopCd))
            {
                //sellerList.StartPosition = FormStartPosition.Manual;
                //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                if (publisherList.ShowDialog(this) == DialogResult.OK)
                {
                    tePublisherCd.Text = ConvertUtil.ToString(publisherList._drv["PUBSHCD"]);
                    tePublisher.Text = _publiser = ConvertUtil.ToString(publisherList._drv["PUBSHNM"]);
                    _isGetPublisherCd = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void teTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
                //teAuthor.Focus();
        }

        private void teAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
                //tePublisherCd.Focus();
        }

        private void tePublisherCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
                //tePublisher.Focus();
        }

        private void usrSearchBookSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }


}
