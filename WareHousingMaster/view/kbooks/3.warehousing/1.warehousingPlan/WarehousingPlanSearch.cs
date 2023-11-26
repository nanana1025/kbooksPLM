using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.DB.kbooks;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.warehouisng
{
    public partial class WarehousingPlanSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        //int _viewType;
        //int _searchType;
        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        //public delegate void SearchPerformanceHandler();
        //public event SearchPerformanceHandler searchPerformanceHandler;

        //public delegate void DeleteRowHandler();
        //public event DeleteRowHandler deleteRowHandler;

        //public delegate void ConfirmHandler();
        //public event ConfirmHandler confirmHandler;

        public delegate void ClearHandler();
        public event ClearHandler clearHandler;

        public WarehousingPlanSearch()
        {
            InitializeComponent();

            //_searchType = -1;
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

            setFocus();
        }



        public void Searches()
        {
            clearHandler();

            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                searchHandler(jData);
            else
                Dangol.Message(jData["MSG"]);
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
            Search();
        }

        public void Search()
        {
            clearHandler();

            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
            {
                searchHandler(jData);
            }
            else
                Dangol.Message(jData["MSG"]);
        }

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));

            int searchType = ConvertUtil.ToInt32(rgOrderNoticeType.EditValue);

            jData.Add("ORDER_NOTICE", searchType);

            int pruchCd = ConvertUtil.ToInt32(tePurchaseCd.Text);

            if(pruchCd < 1)
            {
                jData.Add("MSG", $"매입처 코드 또는 이름을 입력하세요.");
                return false;
            }
            else
            {
                jData.Add($"PURCHCD", pruchCd);
                jData.Add($"PURCHNM", tePurchaseNm.Text);
            }

            return true;
        }

        public void setFocus()
        {
            tePurchaseNm.Focus();
        }

        public void clear()
        {
            tePurchaseCd.Text = "";
            tePurchaseNm.Text = "";
        }

        private void teStand_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                        //Search();
                    }
                }
            }
        }


        private void tePurchaseCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int purchCd = ConvertUtil.ToInt32(tePurchaseCd.Text.Trim());

                JObject jResult = new JObject();
                string query = $"SELECT PURCHCD, PURCHNM FROM HMA02 WHERE PURCHCD = {purchCd}";

                if (DBConnect.getRow(query, ref jResult))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        tePurchaseNm.Text = ConvertUtil.ToString(jResult["PURCHNM"]);
                        tePurchaseCd.Text = ConvertUtil.ToString(jResult["PURCHCD"]);
                        //Search();
                    }
                    else
                    {
                        Dangol.Warining("매입처 정보가 없습니다.");
                    }
                }
                //else
                    //Dangol.Warining("출판사 정보가 없습니다.");
            }
        }
    }
}
