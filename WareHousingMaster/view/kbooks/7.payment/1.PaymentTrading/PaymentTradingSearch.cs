using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.DB.kbooks;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.payment
{
    public partial class PaymentTradingSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;

        Dictionary<int, string> _dicDefault;

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

        public PaymentTradingSearch()
        {
            InitializeComponent();

            //_dicDefault = new Dictionary<int, string>()
            //{
            //    //{ 99, "99 - 전체"},
            //    { -2, "해당없음"}
            //};

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

            //Util.LookupEditHelper(leStoreCd, _dicDefault);
            //Util.LookupEditHelper(lePurchaseCd_E, _dicDefault);

            

        }

        public void refreshPurchase()
        {

        }

        private void setinitialize()
        {
            var today = DateTime.Today;

            deDt_E.EditValue = today;
            deDt_S.EditValue = today;


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
                searchHandler(jData);
            else
                Dangol.Message(jData["MSG"]);
        }

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));


            int purchCd = ConvertUtil.ToInt32(tePurchaseCd.Text);
            

            if(purchCd < 1 )
            {
                jData.Add("MSG", $"매입처가 없습니다.");
                return false;
            }
            else
            {
                jData.Add($"PURCHCD", purchCd);
            }


            if (deDt_S.EditValue != null && !string.IsNullOrEmpty(deDt_S.EditValue.ToString()))
            {
                string dtDate = "";
                dtDate = $"{deDt_S.Text}-01 00:00:00";
                DateTime deMonth = Convert.ToDateTime(dtDate);

                jData.Add($"DATE_S", deMonth.ToString("yyyyMM"));
            }
            else
            {
                jData.Add("MSG", $"실적기간을 확인하세요.");
                return false;
            }

            if (deDt_E.EditValue != null && !string.IsNullOrEmpty(deDt_E.EditValue.ToString()))
            {
                string dtDate = "";
                dtDate = $"{deDt_E.Text}-01 00:00:00";
                DateTime deMonth = Convert.ToDateTime(dtDate);

                jData.Add($"DATE_E", deMonth.ToString("yyyyMM"));
            }
            else
            {
                jData.Add("MSG", $"실적기간을 확인하세요.");
                return false;
            }


            return true;
        }

        //private bool setPurchCdLookupEdit(int shopCd, int storeCd, int groupCd, bool isSearchAll = false, bool init = false)
        //{
        //    JObject jResult = new JObject();
        //    JObject jobj = new JObject();

        //    jobj.Add("SHOPCD", shopCd);
        //    jobj.Add("STORECD", storeCd);
        //    if(!isSearchAll) jobj.Add("GROUPCD", groupCd);
            

        //    string url = "/warehousing/getPurchInfo4Warehousing.json";

        //    if (DBConnect.getRequest(jobj, ref jResult, url))
        //    {
        //        if (Convert.ToBoolean(jResult["EXIST"]))
        //        {
        //            JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //            DataTable dt = new DataTable();
        //            dt.Columns.Add(new DataColumn("KEY", typeof(int)));
        //            dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

        //            int index = 1;
        //            int purchCd = -1;

        //            foreach (JObject obj in jArray.Children<JObject>())
        //            {
        //                DataRow dr = dt.NewRow();

        //                purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);

        //                dr["KEY"] = purchCd;
        //                dr["VALUE"] = $"{purchCd}: {obj["PURCHNM"]}";

        //                dt.Rows.Add(dr);

        //                if (index == 1)
        //                {
        //                    lePurchaseCd_S.EditValue = purchCd;
                           
        //                    index++;
        //                }
        //            }

        //            //Util.insertRowonTop(dt, -1, "전체");

        //            Util.LookupEditHelper(lePurchaseCd_S, dt, "KEY", "VALUE");
        //            Util.LookupEditHelper(lePurchaseCd_E, dt, "KEY", "VALUE");
        //            lePurchaseCd_E.EditValue = purchCd;

        //            return true;
        //        }
        //        else
        //        {
        //            if(!init)
        //                Dangol.Error("입력하신 조건에 해당하는 매입처가 없습니다.");
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        if (!init)
        //            Dangol.Error(jResult["MSG"]);
        //        return false;
        //    }
        //}

        public void setFocus()
        {
            tePurchaseCd.Focus();
        }

        public void clear()
        {
            //teStoreCd_S.Text = "";
            //teStoreCd_E.Text = "";
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
                        deDt_S.Focus();
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

        private void tePurchaseNm_KeyDown(object sender, KeyEventArgs e)
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
                        deDt_S.Focus();
                    }
                }
            }
        }

        private void deDt_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                deDt_E.Focus();
        }

        private void deDt_E_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
