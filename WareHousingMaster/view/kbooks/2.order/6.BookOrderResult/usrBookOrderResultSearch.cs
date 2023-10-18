using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrBookOrderResultSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;
        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public delegate void PrintHandler();
        public event PrintHandler printHandler;

        public delegate void FilterHandler(bool isCheck, int filterCnt);
        public event FilterHandler filterHandler;

        public delegate void ClearHandler();
        public event ClearHandler clearHandler;

        public usrBookOrderResultSearch()
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

            deDtOrder.EditValue = today.AddDays(1);

            //deDtOrder.EditValue = Convert.ToDateTime("2023-03-14");
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
            jData.Add("SHOPNM", leShopCd.Text);
            
            string colNm = ConvertUtil.ToString(rgType.Properties.Items[rgType.SelectedIndex].Tag);
            int type = ConvertUtil.ToInt32(rgType.EditValue);
            int GSType = ConvertUtil.ToInt32(rgGSType.EditValue);

            jData.Add($"TYPE", type);   //1:RATE, 2:GROUP
            jData.Add($"INP_GROUPCD", GSType);   //1:전체, 2:개별


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

                    if (type == 1)
                    {
                        if (!string.IsNullOrWhiteSpace(GSCd))
                            jData.Add($"RATE", ConvertUtil.ToInt32(GSCd));

                        if (!string.IsNullOrWhiteSpace(GSNm))
                            jData.Add($"RATE_NM", GSNm);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(GSCd))
                            jData.Add($"GROUPCD", ConvertUtil.ToInt32(GSCd));

                        if (!string.IsNullOrWhiteSpace(GSNm))
                            jData.Add($"GROUP_NM", GSNm);
                    }
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
                    jData.Add("ORD_DATE", dt.ToString("yyyyMMdd"));
                }
            }
            catch (FormatException ex)
            {
                jData.Add("MSG", "날짜 형식을 확인하세요.");
                return false;
            }

            jData.Add($"REGIST_TYPE", ConvertUtil.ToInt32(rgRegistType.EditValue));

            return true;
        }

        private void teStand_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string seller = teStand.Text.Trim();

            //    using (dlgSellerList sellerList = new dlgSellerList(seller))
            //    {
            //        sellerList.StartPosition = FormStartPosition.Manual;
            //        sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
            //        this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

            //        if (sellerList.ShowDialog(this) == DialogResult.OK)
            //        {
            //            teStand.Text = ConvertUtil.ToString(sellerList._drv["SELLER"]);
            //            teStandCd.Text = ConvertUtil.ToString(sellerList._drv["SELLER_CD"]);
            //            //Dangol.ShowSplash();
            //            //usrReleaseItemList1.receiptRefresh();
            //            //Dangol.CloseSplash();
            //        }
            //    }
            //}
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

        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {
            filter();
        }

        public void filter()
        {
            bool isCheck = cbFilter.Checked;
            int filterCnt = ConvertUtil.ToInt32(seFilter.EditValue);

            filterHandler(isCheck, filterCnt);
        }

        private void sbPrint_Click(object sender, EventArgs e)
        {
            print();
        }

        public void print()
        {
            cbFilter.CheckedChanged -= cbFilter_CheckedChanged;
            cbFilter.Checked = false;
            cbFilter.CheckedChanged += cbFilter_CheckedChanged;
            printHandler();
        }

        public void setFocus()
        {
            rgType.Focus();
        }

        public void clear()
        {
            //teCd_S.Text = "";
            //teCd_E.Text = "";
            //teName.Text = "";
        }

        private void rgType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
