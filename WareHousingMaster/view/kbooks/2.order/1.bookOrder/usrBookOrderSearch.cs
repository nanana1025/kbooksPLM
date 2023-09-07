using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrBookOrderSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;

        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public delegate void CancelHandler();
        public event CancelHandler cancelHandler;

        public delegate void InsertHandler();
        public event InsertHandler insertHandler;

        public delegate void DeleteRowHandler();
        public event DeleteRowHandler deleteRowHandler;

        
        public usrBookOrderSearch()
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
            //var pastDate = today.AddDays(-7);

            deDtOrder.EditValue = today;
            deDtWarehousing.EditValue = today;
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

        public void Searches()
        {
            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                searchHandler(jData);
            else
                Dangol.Message(jData["MSG"]);
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            cancelHandler();
        }

        private bool checkSearch(ref JObject jData)
        {

            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));


            if (string.IsNullOrWhiteSpace(teStoreCd.Text))
            {
                jData.Add("MSG", "매장코드를 입력하세요.");
                return false;
            }
            else
                jData.Add("STORECD", ConvertUtil.ToInt32(teStoreCd.Text.Trim()));


            int groupType = ConvertUtil.ToInt32(rgGroupType.EditValue);

            jData.Add("GROUP_TYPE", groupType);

            if (groupType == 2)
            {
                if(string.IsNullOrWhiteSpace(teGroupCd.Text) && string.IsNullOrWhiteSpace(teGroupNm.Text))
                {
                    jData.Add("MSG", "조 코드 또는 조 이름을 입력하세요.");
                    return false;
                }
                else
                {
                    DataTable checkJoCd = Search.checkGroupCd(ConvertUtil.ToInt32(leShopCd.EditValue), ConvertUtil.ToInt32(teGroupCd.Text.Trim()), ConvertUtil.ToString(teGroupNm.Text.Trim()));

                    if(checkJoCd.Rows.Count < 1)
                    {
                        jData.Add("MSG", "유효한 조 코드 또는 조 이름이 아닙니다. ");
                        return false;
                    }
                    else
                    {
                        teGroupCd.Text = ConvertUtil.ToString(checkJoCd.Rows[0]["CD"]);
                        teGroupNm.Text = ConvertUtil.ToString(checkJoCd.Rows[0]["NM"]);

                        jData.Add("GROUPCD", ConvertUtil.ToInt32(checkJoCd.Rows[0]["CD"]));
                        jData.Add("INP_GROUPCD", ConvertUtil.ToInt32(checkJoCd.Rows[0]["CD"]));
                        jData.Add("GROUPNM", ConvertUtil.ToString(checkJoCd.Rows[0]["NM"]));
                    }

                    //if (!string.IsNullOrWhiteSpace(teGroupCd.Text))
                    //    jData.Add("GROUPCD", ConvertUtil.ToInt32(teGroupCd.Text.Trim()));

                    //if (!string.IsNullOrWhiteSpace(teGroupNm.Text))
                    //    jData.Add("GROUPNM", teGroupNm.Text.Trim());
                }
            }
            else
            {
                jData.Add("INP_GROUPCD", 99);
            }

            int PurchaseType = ConvertUtil.ToInt32(rgPurchaseRange.EditValue);

            if (PurchaseType == 2)
            {
                if (string.IsNullOrWhiteSpace(tePurchaseCd.Text) && string.IsNullOrWhiteSpace(tePurchaseNm.Text))
                {
                    jData.Add("MSG", "매입처를 입력하세요.");
                    return false;
                }
                else
                {

                    DataTable checkPurchCd1 = Order.checkPurchCd(ConvertUtil.ToInt32(leShopCd.EditValue), ConvertUtil.ToInt32(tePurchaseCd.Text.Trim()), ConvertUtil.ToString(tePurchaseNm.Text.Trim()));

                    if (checkPurchCd1.Rows.Count < 1)
                    {
                        jData.Add("MSG", "유효한 매입처 코드 또는 매입처 이름이 아닙니다. ");
                        return false;
                    }
                    else
                    {
                        //DataTable checkPurchCd2 = Order.checkPurchCd(ConvertUtil.ToInt32(leShopCd.EditValue), ConvertUtil.ToInt32(tePurchaseCd.Text.Trim()), ConvertUtil.ToString(tePurchaseNm.Text.Trim()));

                        tePurchaseCd.Text = ConvertUtil.ToString(checkPurchCd1.Rows[0]["CD"]);
                        tePurchaseNm.Text = ConvertUtil.ToString(checkPurchCd1.Rows[0]["NM"]);

                        jData.Add("PURCHCD", ConvertUtil.ToInt32(checkPurchCd1.Rows[0]["CD"]));
                        jData.Add("PURCHNM", ConvertUtil.ToString(checkPurchCd1.Rows[0]["NM"]));
                    }

                    //if (!string.IsNullOrWhiteSpace(tePurchaseCd.Text))
                    //    jData.Add("PURCHCD", ConvertUtil.ToInt32(tePurchaseCd.Text.Trim()));

                    //if (!string.IsNullOrWhiteSpace(tePurchaseNm.Text))
                    //    jData.Add("PURCHNM", tePurchaseNm.Text.Trim());
                }
            }

            int orderType = ConvertUtil.ToInt32(rgOrderType.EditValue);
            jData.Add("ORDER_TYPE", orderType);

            int orderCondition = ConvertUtil.ToInt32(rgOrderCondition.EditValue);   //Trade_item

            jData.Add("ORDER_CONTIDION", orderCondition);
            string conditionString = rgOrderCondition.Properties.Items[rgOrderCondition.SelectedIndex].Description;
            jData.Add("ORDER_CONTIDION_STRING", conditionString);

            int tradeItem = orderType + orderCondition;

            jData.Add("TRADE_ITEM", tradeItem);

            DateTime orderTime;
            string dtOrder = "";
            if (deDtOrder.EditValue != null && !string.IsNullOrEmpty(deDtOrder.EditValue.ToString()))
                dtOrder = $"{deDtOrder.Text} 00:00:00";

            if (string.IsNullOrWhiteSpace(dtOrder))
            {
                jData.Add("MSG", "주문일자를 선택하세요.");
                return false;
            }
            else
            {
                orderTime = Convert.ToDateTime(dtOrder);
                jData.Add("DT_ORDER", orderTime.ToString("yyyyMMdd"));
            }

            DateTime inpTime;
            string dtWarehousing = "";
            if (deDtWarehousing.EditValue != null && !string.IsNullOrEmpty(deDtWarehousing.EditValue.ToString()))
                dtWarehousing = $"{deDtWarehousing.Text} 00:00:00";

            if (string.IsNullOrWhiteSpace(dtOrder))
            {
                jData.Add("MSG", "입고예정일자를 선택하세요.");
                return false;
            }
            else
            {
                inpTime = Convert.ToDateTime(dtWarehousing);
                jData.Add("DT_INP", inpTime.ToString("yyyyMMdd"));
            }

            return true;
        }

        public JObject getSearchInfo()
        {
            JObject jData = new JObject();

            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));


            if (!string.IsNullOrWhiteSpace(teStoreCd.Text))
                jData.Add("DEPTCD", ConvertUtil.ToInt32(teStoreCd.Text.Trim()));


            int groupType = ConvertUtil.ToInt32(rgGroupType.EditValue);
            jData.Add("GROUP_TYPE", groupType);

            if (groupType == 2)
            {
                if (!string.IsNullOrWhiteSpace(teGroupCd.Text))
                    jData.Add("GROUPCD", ConvertUtil.ToInt32(teGroupCd.Text.Trim()));

                if (!string.IsNullOrWhiteSpace(teGroupNm.Text))
                    jData.Add("GROUPNM", teGroupNm.Text.Trim());
            }

            int PurchaseType = ConvertUtil.ToInt32(rgPurchaseRange.EditValue);
            jData.Add("PURCH_TYPE", groupType);

            if (PurchaseType == 2)
            {
                if (!string.IsNullOrWhiteSpace(teGroupCd.Text))
                    jData.Add("PURCHCD", ConvertUtil.ToInt32(tePurchaseCd.Text.Trim()));

                if (!string.IsNullOrWhiteSpace(teGroupNm.Text))
                    jData.Add("PURCHNM", tePurchaseNm.Text.Trim());
            }

            jData.Add("ORDER_TYPE", ConvertUtil.ToInt32(rgOrderType.EditValue));
            jData.Add("ORDER_CONTIDION", ConvertUtil.ToInt32(rgOrderCondition.EditValue));

            string dtOrder = "";
            if (deDtOrder.EditValue != null && !string.IsNullOrEmpty(deDtOrder.EditValue.ToString()))
                dtOrder = $"{deDtOrder.Text} 00:00:00";

            if (!string.IsNullOrWhiteSpace(dtOrder))
                jData.Add("DT_ORDER", dtOrder);

            string dtWarehousing = "";
            if (deDtWarehousing.EditValue != null && !string.IsNullOrEmpty(deDtWarehousing.EditValue.ToString()))
                dtWarehousing = $"{deDtWarehousing.Text} 00:00:00";

            if (!string.IsNullOrWhiteSpace(dtOrder))
                jData.Add("DT_INP", dtWarehousing);

            return jData;
        }

        private void tePurchaseNm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ConvertUtil.ToInt32(rgPurchaseRange.EditValue) == 2)
            {
                getPurchase();
            }
        }

        private void getPurchase()
        {
            int shopCd = ConvertUtil.ToInt32(leShopCd.EditValue);
            string purchaseCompany = tePurchaseNm.Text.Trim();

            using (dlgPurchaseList sellerList = new dlgPurchaseList(purchaseCompany, shopCd))
            {
                //sellerList.StartPosition = FormStartPosition.Manual;
                //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                if (sellerList.ShowDialog(this) == DialogResult.OK)
                {
                    tePurchaseCd.Text = ConvertUtil.ToString(sellerList._drv["PURCHCD"]);
                    tePurchaseNm.Text = ConvertUtil.ToString(sellerList._drv["PURCHNM"]);
                }
            }
        }

        private void tePurchaseNm_DoubleClick(object sender, EventArgs e)
        {
            getPurchase();
        }

        private void rgGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int groupType = ConvertUtil.ToInt32(rgGroupType.EditValue);

            teGroupCd.ReadOnly = groupType == 1;
            teGroupNm.ReadOnly = groupType == 1;

            if(groupType == 1)
            {
                teGroupCd.Text = "";
                teGroupNm.Text = "";
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
        public void setFocus()
        {
            teStoreCd.Focus();
        }
        public void clear()
        {
            teStoreCd.Text = "";
        }

        private void sbInsert_Click(object sender, EventArgs e)
        {
            insertHandler();
        }

        private void sbDeleteRow_Click(object sender, EventArgs e)
        {
            deleteRowHandler();
        }

        private void teStoreCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Searches();
        }

    }
}
