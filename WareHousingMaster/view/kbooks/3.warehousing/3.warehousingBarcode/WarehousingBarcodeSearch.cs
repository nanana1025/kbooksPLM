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
    public partial class WarehousingBarcodeSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;
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

        public WarehousingBarcodeSearch()
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

            dePurDt.EditValue = today;


            //deDtWork.EditValue = today;
            //var pastDate = today.AddDays(-7);

            //deDtOrder.EditValue = today;
            //deDtWarehousing.EditValue = today;



            //deDtWork.EditValue = Convert.ToDateTime("2023-03-13");
            //deDtOrder.EditValue = Convert.ToDateTime("2023-07-10");
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
                //if (jData.ContainsKey("PURCHCD"))
                //{
                //    string sData = DBCommon.checkHMA02(ConvertUtil.ToInt32(jData["PURCHCD"]));
                //    if (!string.IsNullOrWhiteSpace(sData))
                //    {
                //        if (Dangol.MessageYN($"지정하신 매입처가 {sData}로 설정되어 있습니다.\n계속 진행하시겠습니까?") == DialogResult.Yes)
                //            searchHandler(jData);
                //    }
                //    else
                        searchHandler(jData);
                //}
                //else
                //    searchHandler(jData);
            }
            else
                Dangol.Message(jData["MSG"]);
        }

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));

            jData.Add($"BARCODE_FG", ConvertUtil.ToInt32(rgBarcodeFg.EditValue));
            int bookType = ConvertUtil.ToInt32(rgBookType.EditValue);
            jData.Add($"BOOK_TYPE", bookType);


            if (dePurDt.EditValue != null && !string.IsNullOrEmpty(dePurDt.EditValue.ToString()))
            {
                string dtDate = "";
                dtDate = $"{dePurDt.Text} 00:00:00";
                DateTime deMonth = Convert.ToDateTime(dtDate);

                jData.Add($"PUR_DATE", deMonth.ToString("yyyyMMdd"));
            }
            else
            {
                jData.Add("MSG", $"입고일자를 확인하세요.");
                return false;
            }

            if (bookType == 1)
            {
                int processType = ConvertUtil.ToInt32(rgProcessType.EditValue);

                jData.Add($"PROCESS_TYPE", processType);

                if (processType == 1)
                {
                    int purchCd = ConvertUtil.ToInt32(tePurchaseCd.Text);

                    if (purchCd < 1)
                    {
                        jData.Add("MSG", $"매입처가 없습니다.");
                        return false;
                    }
                    else
                    {
                        jData.Add($"PURCHCD", purchCd);
                    }
                }
            }

            return true;
        }

       

        private void rgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int type = ConvertUtil.ToInt32(rgType.EditValue);

            //if (type == 1)
            //{
            //    lcType.Text = "과 구분";
            //    lcTypeNm.Text = "과 코드 / 명";
            //}
            //else if (type == 2)
            //{
            //    lcType.Text = "조 구분";
            //    lcTypeNm.Text = "조 코드 / 명";
            //}
        }

        private void rgGSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int processType = ConvertUtil.ToInt32(rgProcessType.EditValue);

            if (processType == 3)
            {
                tePurchaseCd.Text = "";
                tePurchaseNm.Text = "";
            }
        }

        private void rgPurchaseRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int purchaseType = ConvertUtil.ToInt32(rgBarcodeFg.EditValue);

            //tePurchaseCd.ReadOnly = purchaseType == 1;
            //tePurchaseNm.ReadOnly = purchaseType == 1;

            //if (purchaseType == 1)
            //{
            //    tePurchaseCd.Text = "";
            //    tePurchaseNm.Text = "";
            //}
        }

        private void rgSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int purchaseType = ConvertUtil.ToInt32(rgSearchType.EditValue);

            //teStoreCd_S.ReadOnly = purchaseType == 1;
            //teStoreCd_E.ReadOnly = purchaseType == 1;

            //if (purchaseType == 1)
            //{
            //    teStoreCd_S.Text = "";
            //    teStoreCd_E.Text = "";
            //}
        }

        //private void sbSearch3_Click(object sender, EventArgs e)    //실적조회
        //{
        //    searchPerformanceHandler();
        //}

        //private void sbSearch2_Click(object sender, EventArgs e)    //행삭제
        //{
        //    deleteRowHandler();
        //}

        //private void sbSearch1_Click(object sender, EventArgs e)    //확정
        //{
        //    confirmHandler();
        //}

        private void rgSearchType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
        public void setFocus()
        {
            rgProcessType.Focus();
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
                        //deDt_S.Focus();
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
                        //deDt_S.Focus();
                    }
                }
            }
        }

        private void rgProcessType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int processType = ConvertUtil.ToInt32(rgProcessType.EditValue);

                if (processType == 1)
                {
                    tePurchaseCd.Focus();
                }
                else
                {
                    Search();
                }
            }
        }
    }
}
