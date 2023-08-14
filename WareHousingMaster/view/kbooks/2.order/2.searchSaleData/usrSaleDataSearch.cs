using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSaleDataSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;
        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public delegate void SaveHandler();
        public event SaveHandler saveHandler;

        public usrSaleDataSearch()
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
            //deDtStart.EditValue = Convert.ToDateTime("2023-03-10");
            //deDtEnd.EditValue = Convert.ToDateTime("2023-03-13");

            //deDtOrder.EditValue = Convert.ToDateTime("2023-03-14");
            //deDtWarehousing.EditValue = Convert.ToDateTime("2023-03-15");


            var today = DateTime.Today;

            deDtStart.EditValue = today;
            deDtEnd.EditValue = today;

            deDtOrder.EditValue = today.AddDays(+1);
            deDtWarehousing.EditValue = today.AddDays(+2);
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

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));

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

            string colNm = ConvertUtil.ToString(rgType.Properties.Items[rgType.SelectedIndex].Tag);
            int type = ConvertUtil.ToInt32(rgType.EditValue);
            int GSType = ConvertUtil.ToInt32(rgGSType.EditValue);

            jData.Add($"TYPE", type);   //1:RATE, 2:GROUP
            jData.Add($"INP_GROUPCD", GSType);   //1:전체, 2:개별

            if (GSType == 2)
            {
                
                string GSCd = ConvertUtil.ToString(teGSCD.Text);
                string GSNm = ConvertUtil.ToString(teGSNm.Text);

                if(string.IsNullOrWhiteSpace(GSCd) && string.IsNullOrWhiteSpace(GSNm))
                {
                    string colText = ConvertUtil.ToString(rgType.Properties.Items[rgType.SelectedIndex].Description);

                    jData.Add("MSG", $"{colText} 코드 또는 이름을 입력하세요.");
                    return false;
                }
                else
                {
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
           





            string dtDate = "";

            DateTime dtfrom;
            DateTime dtto;
           
            

            if (deDtStart.EditValue != null && !string.IsNullOrEmpty(deDtStart.EditValue.ToString()))
                dtDate = $"{deDtStart.Text} 00:00:00";

            if (string.IsNullOrWhiteSpace(dtDate))
            {
                jData.Add("MSG", "(시작)판매 기간이 없습니다.");
                return false;
            }
            else
            {
                DateTime dt = dtfrom = Convert.ToDateTime(dtDate);
                jData.Add("DT_FROM", dt.ToString("yyyyMMdd"));
            }

            int result = DateTime.Compare(dtfrom, DateTime.Today);

            //if (result > 0)
            //{
            //    jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다.");
            //    return false;
            //}




            dtDate = "";
            if (deDtEnd.EditValue != null && !string.IsNullOrEmpty(deDtEnd.EditValue.ToString()))
                dtDate = $"{deDtEnd.Text} 23:59:59";

            if (string.IsNullOrWhiteSpace(dtDate))
            {
                jData.Add("MSG", "(종료)판매 기간이 없습니다.");
                return false;
            }
            else
            {
                DateTime dt = dtto = Convert.ToDateTime(dtDate);
                jData.Add("DT_TO", dt.ToString("yyyyMMdd"));
            }

            result = DateTime.Compare(dtfrom, dtto);

            if (result > 0)
            {
                jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다.");
                return false;
            }

            dtDate = "";
            if (deDtOrder.EditValue != null && !string.IsNullOrEmpty(deDtOrder.EditValue.ToString()))
                dtDate = $"{deDtOrder.Text} 00:00:00";

            if (string.IsNullOrWhiteSpace(dtDate))
            {
                jData.Add("MSG", "주문일자가 없습니다.");
                return false;
            }
            else
            {
                DateTime dt = Convert.ToDateTime(dtDate);
                jData.Add("ORDER_DATE", dt.ToString("yyyyMMdd"));
            }


            dtDate = "";
            if (deDtWarehousing.EditValue != null && !string.IsNullOrEmpty(deDtWarehousing.EditValue.ToString()))
                dtDate = $"{deDtWarehousing.Text} 00:00:00";

            if (string.IsNullOrWhiteSpace(dtDate))
            {
                jData.Add("MSG", "입고예정일자가 없습니다.");
                return false;
            }
            else
            {
                DateTime dt = Convert.ToDateTime(dtDate);
                jData.Add("INP_DATE", dt.ToString("yyyyMMdd"));
            }
                


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

            if(type ==1)
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

        private void sbSave_Click(object sender, EventArgs e)
        {
            saveHandler();
        }
    }
}
