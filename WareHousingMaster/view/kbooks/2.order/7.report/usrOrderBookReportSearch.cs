using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrOrderBookReportSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;
        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public delegate void SearchPerformanceHandler();
        public event SearchPerformanceHandler searchPerformanceHandler;

        public delegate void DeleteRowHandler();
        public event DeleteRowHandler deleteRowHandler;

        public delegate void ConfirmHandler();
        public event ConfirmHandler confirmHandler;

        public usrOrderBookReportSearch()
        {
            InitializeComponent();

            _searchType = -1;
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
            Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");
        }

        private void setinitialize()
        {
            var today = DateTime.Today;

            //deDtWork.EditValue = today;
            //var pastDate = today.AddDays(-7);

            deDtOrder.EditValue = today;
            //deDtWarehousing.EditValue = today;

            //deDtOrder.EditValue = Convert.ToDateTime("2023-03-10");
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
            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                searchHandler(jData);
            else
                Dangol.Message(jData["MSG"]);
        }

        public void Search()
        {
            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                searchHandler(jData);
            else
                Dangol.Message(jData["MSG"]);
        }

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));
            jData.Add("SHOPNM", ConvertUtil.ToString(leShopCd.Text));

            int searchType = ConvertUtil.ToInt32(rgSearchType.EditValue);
            jData.Add("SEARCH_TYPE", searchType);

            if (searchType == 2)
            {
                string codeS = teStoreCd_S.Text.Trim();
                string codeE = teStoreCd_E.Text.Trim();

                if (!string.IsNullOrWhiteSpace(codeS) && string.IsNullOrWhiteSpace(codeE))
                {
                    jData.Add("STORE_TYPE", "SINGLE");
                    jData.Add($"STORECD", ConvertUtil.ToInt32(codeS));
                }
                else if (string.IsNullOrWhiteSpace(codeS) && !string.IsNullOrWhiteSpace(codeE))
                {
                    jData.Add("STORE_TYPE", "SINGLE");
                    jData.Add($"STORECD", ConvertUtil.ToInt32(codeE));

                }
                else if (codeS.Equals(codeE))
                {
                    jData.Add("STORE_TYPE", "SINGLE");
                    jData.Add($"STORECD", ConvertUtil.ToInt32(codeE));
                }
                else
                {
                    int start = ConvertUtil.ToInt32(codeS);
                    int end = ConvertUtil.ToInt32(codeE);

                    jData.Add("STORE_TYPE", "MULTI");
                    if (start > end)
                    {
                        jData.Add($"STORECD_S", end);
                        jData.Add($"STORECD_E", start);
                    }
                    else
                    {
                        jData.Add($"STORECD_S", start);
                        jData.Add($"STORECD_E", end);
                    }
                }
            }
            else
            {
                jData.Add("STORECD", -1);
            }

            

            string deOrderDt = "";
            if (deDtOrder.EditValue != null && !string.IsNullOrEmpty(deDtOrder.EditValue.ToString()))
                deOrderDt = $"{deDtOrder.Text} 00:00:00";

            if (string.IsNullOrWhiteSpace(deOrderDt))
            {
                jData.Add("MSG", "주문일자를 선택하세요.");
                return false;
            }
            else
            {
                DateTime dt = Convert.ToDateTime(deOrderDt);
                DateTime dtToday = DateTime.Now;

                //if(dt.CompareTo(dtToday) > 0)
                //{
                //    jData.Add("MSG", "주문일자는 당일영업일보다 작을 수 없습니다.");
                //    return false;
                //}
                //else
                    jData.Add("DT_ORDER", dt.ToString("yyyyMMdd"));
                jData.Add("ORD_DATE", dt.ToString("yyyyMMdd"));
                jData.Add("ORDER_DT", dt.ToString("yyyy년 MM월 dd일"));
                



            }

            return true;
        }
        private void rgSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int purchaseType = ConvertUtil.ToInt32(rgSearchType.EditValue);

            teStoreCd_S.ReadOnly = purchaseType == 1;
            teStoreCd_E.ReadOnly = purchaseType == 1;

            if (purchaseType == 1)
            {
                teStoreCd_S.Text = "";
                teStoreCd_E.Text = "";
            }
        }

        private void sbSearch1_Click(object sender, EventArgs e)    //확정
        {
            confirmHandler();
        }

        public void setFocus()
        {
            rgSearchType.Focus();
        }

        public void clear()
        {
            //teCd_S.Text = "";
            //teCd_E.Text = "";
            //teName.Text = "";
        }

        private void rgSearchType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        
    }
}
