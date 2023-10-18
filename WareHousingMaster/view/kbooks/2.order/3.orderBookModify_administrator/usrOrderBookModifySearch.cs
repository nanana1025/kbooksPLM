using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrOrderBookModifySearch : DevExpress.XtraEditors.XtraUserControl
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

        public delegate void ClearHandler();
        public event ClearHandler clearHandler;

        public usrOrderBookModifySearch()
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

            deDtWork.EditValue = today;
            var pastDate = today.AddDays(-7);
            
            deDtOrder.EditValue = today.AddDays(1);

            //deDtWork.EditValue = Convert.ToDateTime("2023-03-13");
            //deDtOrder.EditValue = Convert.ToDateTime("2023-07-10");
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
            Search();
        }

        public void Search()
        {
            clearHandler();

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

            int searchType = ConvertUtil.ToInt32(rgSearchType.EditValue);
            jData.Add("SEARCH_TYPE", searchType);

            if (searchType == 2)
            {
                string codeS = teStoreCd_S.Text.Trim();
                string codeE = teStoreCd_E.Text.Trim();

                int start = ConvertUtil.ToInt32(codeS);
                int end = ConvertUtil.ToInt32(codeE);

                if (!string.IsNullOrWhiteSpace(codeS) && string.IsNullOrWhiteSpace(codeE))
                {
                    jData.Add("STORE_TYPE", "SINGLE");
                    if (Util.checkOnlyNumeric(codeS))
                    {
                        jData.Add($"STORECD", ConvertUtil.ToInt32(codeS));
                    }
                    else
                    {
                        jData.Add("MSG", "매장코드를 확인하세요.");
                        return false;
                    }

                    start = end = ConvertUtil.ToInt32(jData["STORECD"]);
                }
                else if (string.IsNullOrWhiteSpace(codeS) && !string.IsNullOrWhiteSpace(codeE))
                {
                    jData.Add("STORE_TYPE", "SINGLE");
                    if (Util.checkOnlyNumeric(codeE))
                    {
                        jData.Add($"STORECD", ConvertUtil.ToInt32(codeE));
                    }
                    else
                    {
                        jData.Add("MSG", "매장코드를 확인하세요.");
                        return false;
                    }
                    start = end = ConvertUtil.ToInt32(jData["STORECD"]);

                }
                else if (codeS.Equals(codeE))
                {
                    jData.Add("STORE_TYPE", "SINGLE");
                    if (Util.checkOnlyNumeric(codeE))
                    {
                        jData.Add($"STORECD", ConvertUtil.ToInt32(codeE));
                    }
                    else
                    {
                        jData.Add("MSG", "매장코드를 확인하세요.");
                        return false;
                    }
                    start = end = ConvertUtil.ToInt32(jData["STORECD"]);
                }
                else
                {
                    if (Util.checkOnlyNumeric(codeS) && Util.checkOnlyNumeric(codeE))
                    {
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
                    else
                    {
                        jData.Add("MSG", "매장코드를 확인하세요.");
                        return false;
                    }
                }

                if(start < 1 || (end != 99 && end > 15))
                {
                    jData.Add("MSG", $"매장코드는 [1~12] 만 가능합니다.");
                    return false;
                }
            }
            else
            {
                jData.Add("STORECD", -1);
            }

            string colNm = ConvertUtil.ToString(rgType.Properties.Items[rgType.SelectedIndex].Tag);
            int GSType = ConvertUtil.ToInt32(rgGSType.EditValue);

            jData.Add($"GROUP_CATEGORY", ConvertUtil.ToInt32(rgType.EditValue));
            jData.Add($"GROUP_TYPE", GSType);

            if (GSType == 2)
            {
                string GSCd = ConvertUtil.ToString(teGSCD.Text);
                string GSNm = ConvertUtil.ToString(teGSNm.Text);

                if (string.IsNullOrWhiteSpace(GSCd) && string.IsNullOrWhiteSpace(GSNm))
                {
                    string colText = ConvertUtil.ToString(rgType.Properties.Items[rgType.SelectedIndex].Description);

                    jData.Add("MSG", $"{colText} 코드 또는 이름을 입력하세요.");
                    return false;
                }
                else
                {
                    string colText = ConvertUtil.ToString(rgType.Properties.Items[rgType.SelectedIndex].Description);

                    if (!string.IsNullOrWhiteSpace(GSCd) && !Util.checkOnlyNumeric(GSCd))
                    {
                        jData.Add("MSG", $"{colText}코드를 확인하세요.");
                        return false;
                    }

                    if (!string.IsNullOrWhiteSpace(GSCd))
                        jData.Add($"{colNm}CD", ConvertUtil.ToInt32(GSCd));

                    if (!string.IsNullOrWhiteSpace(GSNm))
                        jData.Add($"{colNm}_NM", GSNm);
                }
            }

            int purchaseType = ConvertUtil.ToInt32(rgPurchaseRange.EditValue);
            jData.Add($"PURCHCD_TYPE", purchaseType);

            if (purchaseType == 2)
            {
                string purchaseTCd = ConvertUtil.ToString(tePurchaseCd.Text);
                string purchaseTNm = ConvertUtil.ToString(tePurchaseNm.Text);

                if (string.IsNullOrWhiteSpace(purchaseTCd) && string.IsNullOrWhiteSpace(purchaseTNm))
                {
                    jData.Add("MSG", $"매입처 코드 또는 이름을 입력하세요.");
                    return false;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(purchaseTCd) && !Util.checkOnlyNumeric(purchaseTCd))
                    {
                        jData.Add("MSG", $"매입처 코드를 확인하세요.");
                        return false;
                    }

                    if (!string.IsNullOrWhiteSpace(purchaseTCd))
                        jData.Add($"PURCHCD", ConvertUtil.ToInt32(purchaseTCd));

                    if (!string.IsNullOrWhiteSpace(purchaseTNm))
                        jData.Add($"PURCH_NM", purchaseTNm);
                }
            }

            try
            {
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
                }
            }
            catch (FormatException ex)
            {
                jData.Add("MSG", "날짜 형식을 확인하세요.");
                return false;
            }

            int orderType = ConvertUtil.ToInt32(rgOrderType.EditValue);
            int orderCondition = ConvertUtil.ToInt32(rgOrderCondition.EditValue);

            jData.Add($"TRADE_ITEM", orderCondition + orderType);

            return true;
        }

        private void teStand_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ConvertUtil.ToInt32(rgPurchaseRange.EditValue) == 2)
            {
                string seller = tePurchaseNm.Text.Trim();

                using (dlgPurchaseList sellerList = new dlgPurchaseList(seller))
                {
                    //sellerList.StartPosition = FormStartPosition.Manual;
                    //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                    if (sellerList.ShowDialog(this) == DialogResult.OK)
                    {
                        tePurchaseNm.Text = ConvertUtil.ToString(sellerList._drv["PURCHNM"]);
                        tePurchaseCd.Text = ConvertUtil.ToString(sellerList._drv["PURCHCD"]);
                        //Dangol.ShowSplash();
                        //usrReleaseItemList1.receiptRefresh();
                        //Dangol.CloseSplash();
                    }
                }
            }
        }

        private void rgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = ConvertUtil.ToInt32(rgType.EditValue);

            if (type == 1)
            {
                lcType.Text = "과 구분";
                lcTypeNm.Text = "과 코드 / 명";
            }
            else if (type == 2)
            {
                lcType.Text = "조 구분";
                lcTypeNm.Text = "조 코드 / 명";
            }
        }

        private void rgGSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int purchaseType = ConvertUtil.ToInt32(rgGSType.EditValue);

            teGSCD.ReadOnly = purchaseType == 1;
            teGSNm.ReadOnly = purchaseType == 1;

            if (purchaseType == 1)
            {
                teGSCD.Text = "";
                teGSNm.Text = "";
            }
        }

        private void rgPurchaseRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            int purchaseType = ConvertUtil.ToInt32(rgPurchaseRange.EditValue);

            tePurchaseCd.ReadOnly = purchaseType == 1;
            tePurchaseNm.ReadOnly = purchaseType == 1;

            if (purchaseType == 1)
            {
                tePurchaseCd.Text = "";
                tePurchaseNm.Text = "";
            }
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

        private void sbSearch3_Click(object sender, EventArgs e)    //실적조회
        {
            searchPerformanceHandler();
        }

        private void sbSearch2_Click(object sender, EventArgs e)    //행삭제
        {
            deleteRowHandler();
        }

        private void sbSearch1_Click(object sender, EventArgs e)    //확정
        {
            confirmHandler();
        }

        public void setFocus()
        {
            rgSearchType.Focus();
        }

        private void rgSearchType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        public void clear()
        {
            
        }
    }
}
