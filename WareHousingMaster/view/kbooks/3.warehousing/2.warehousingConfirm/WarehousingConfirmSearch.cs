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
    public partial class WarehousingConfirmSearch : DevExpress.XtraEditors.XtraUserControl
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

        public WarehousingConfirmSearch()
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

            if (!setPurchCdLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), 99, 99, true, true))
            {
                Dictionary<int, string> dicDefault = new Dictionary<int, string>(){{ -2, "매입처 없음"}};

                Util.LookupEditHelper(lePurchaseCd_S, dicDefault);
                Util.LookupEditHelper(lePurchaseCd_E, dicDefault);
                lePurchaseCd_S.EditValue = -2;
                lePurchaseCd_E.EditValue = -2;

                tePurchaseCd_S.Text = "-2";
                tePurchaseCd_E.Text = "-2";
            }
        }

        public void refreshPurchase()
        {
            if (!setPurchCdLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), 99, 99, true, true))
            {
                Dictionary<int, string> dicDefault = new Dictionary<int, string>() { { -2, "매입처 없음" } };

                Util.LookupEditHelper(lePurchaseCd_S, dicDefault);
                Util.LookupEditHelper(lePurchaseCd_E, dicDefault);
                lePurchaseCd_S.EditValue = -2;
                lePurchaseCd_E.EditValue = -2;

                tePurchaseCd_S.Text = "-2";
                tePurchaseCd_E.Text = "-2";
            }
        }

        private void setinitialize()
        {
            var today = DateTime.Today;

            
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
                //if (jData.ContainsKey("PURCHCD"))
                //{
                //    string sData = DBCommon.checkHMA02(ConvertUtil.ToInt32(jData["PURCHCD"]));
                //    if (!string.IsNullOrWhiteSpace(sData))
                //    {
                //        if (Dangol.MessageYN($"지정하신 매입처가 {sData}로 설정되어 있습니다.\n계속 진행하시겠습니까?") == DialogResult.Yes)
                //            searchHandler(jData);
                //    }
                //    else
                //        searchHandler(jData);
                //}
                //else
                    searchHandler(jData);
            }
            else
                Dangol.Message(jData["MSG"]);
        }

        private bool checkSearch(ref JObject jData)
        {
            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));


            int purchCds = ConvertUtil.ToInt32(lePurchaseCd_S.EditValue);
            int purchCde = ConvertUtil.ToInt32(lePurchaseCd_E.EditValue);

            if(purchCds == -2 || purchCde == -2)
            {
                jData.Add("MSG", $"매입처가 없습니다.");
                    return false;
            }
            else
            {
                if(purchCds > purchCde)
                {
                    int temp = purchCds;
                    purchCds = purchCde;
                    purchCde = temp;
                }

                jData.Add($"PURCHCD_S", purchCds);
                jData.Add($"PURCHCD_E", purchCde);

            }


            return true;
        }

        private bool setPurchCdLookupEdit(int shopCd, int storeCd, int groupCd, bool isSearchAll = false, bool init = false)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("SHOPCD", shopCd);
            jobj.Add("STORECD", storeCd);
            if(!isSearchAll) jobj.Add("GROUPCD", groupCd);
            

            string url = "/warehousing/getPurchInfo4Warehousing.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("KEY", typeof(int)));
                    dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                    int index = 1;
                    int purchCd = -1;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = dt.NewRow();

                        purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);

                        dr["KEY"] = purchCd;
                        dr["VALUE"] = $"{purchCd}: {obj["PURCHNM"]}";

                        dt.Rows.Add(dr);

                        if (index == 1)
                        {
                            lePurchaseCd_S.EditValue = purchCd;
                           
                            index++;
                        }
                    }

                    //Util.insertRowonTop(dt, -1, "전체");

                    Util.LookupEditHelper(lePurchaseCd_S, dt, "KEY", "VALUE");
                    Util.LookupEditHelper(lePurchaseCd_E, dt, "KEY", "VALUE");
                    lePurchaseCd_E.EditValue = purchCd;

                    return true;
                }
                else
                {
                    if(!init)
                        Dangol.Error("입력하신 조건에 해당하는 매입처가 없습니다.");
                    return false;
                }
            }
            else
            {
                if (!init)
                    Dangol.Error(jResult["MSG"]);
                return false;
            }
        }

        public void setFocus()
        {
            lePurchaseCd_S.Focus();
        }

        public void clear()
        {
            //teStoreCd_S.Text = "";
            //teStoreCd_E.Text = "";
        }

        private void lePurchaseCd_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                lePurchaseCd_E.Focus();
        }

        private void lePurchaseCd_E_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void lePurchaseCd_S_EditValueChanged(object sender, EventArgs e)
        {
            tePurchaseCd_S.Text = ConvertUtil.ToString(lePurchaseCd_S.EditValue);
        }

        private void lePurchaseCd_E_EditValueChanged(object sender, EventArgs e)
        {
            tePurchaseCd_E.Text = ConvertUtil.ToString(lePurchaseCd_E.EditValue);
        }
    }
}
