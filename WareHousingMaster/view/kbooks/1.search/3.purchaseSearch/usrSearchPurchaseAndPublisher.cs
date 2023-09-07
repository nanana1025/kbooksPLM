using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSearchPurchaseAndPublisher : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }


        int _viewType;
        string _colNm;

        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public usrSearchPurchaseAndPublisher()
        {
            InitializeComponent();
        }

        public void setInitLoad(int viewType = (int)Enum.SearchType.SELLER)
        {
            _viewType = viewType;

            setInfoBox();
            setinitialize();
            setLayoutView();
        }


        private void setInfoBox()
        {
            List<string> listLongCol = new List<string>(new string[] { "SHOPCD" });
            DataTable dtShopCd = Util.getTable("HMA09", "SHOPCD,SHOP_NM ", "SHOPCD > 0", "SHOPCD ASC", listLongCol);
            Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");
        }

        private void setinitialize()
        {
            
        }

        public void setLayoutView()
        {
            if (_viewType == (int)Enum.SearchType.SELLER)
            {
                lcCd.Text = "매입처 코드";
                lcNm.Text = "매입처 명";

                _colNm = "PURCH";
            }
            else if (_viewType == (int)Enum.SearchType.PUBLISHER)
            {
                lcCd.Text = "출판사 코드";
                lcNm.Text = "출판사 명";

                _colNm = "PUBSH";
            }
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

            string code = teCd.Text.Trim();
            string name = teNm.Text.Trim();

            if(string.IsNullOrWhiteSpace(code) && string.IsNullOrWhiteSpace(name))
            {
                if (_viewType == (int)Enum.SearchType.SELLER)
                    jData.Add("MSG", "매입처 코드 또는 매입처 명을 입력하세요.");
                else if (_viewType == (int)Enum.SearchType.SELLER)
                    jData.Add("MSG", "출판사 코드 또는 출판사 명을 입력하세요.");

                return false;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(code))
                    jData.Add($"{_colNm}CD", code);

                if (!string.IsNullOrWhiteSpace(name))
                    jData.Add($"{_colNm}NM", name);
            }

            return true;
        }
        public void clear()
        {
            teNm.Text = "";
            teCd.Text = "";
        }

        public void setFocus()
        {
            teNm.Focus();
        }

        private void teNm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void teCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
