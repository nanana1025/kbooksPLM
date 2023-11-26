using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.DB.kbooks;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.returns
{
    public partial class usrReturnPrintSearch : DevExpress.XtraEditors.XtraUserControl
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

        public usrReturnPrintSearch()
        {
            InitializeComponent();

            _dicDefault = new Dictionary<int, string>()
            {
                { 99, "99 - 전체"},
                { -2, "해당없음"}
            };

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

            Util.LookupEditHelper(leStoreCd, _dicDefault);
            Util.LookupEditHelper(leGroupCd, _dicDefault);

            if (!setPurchCdLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), 99, 99, true, true))
            {
                Dictionary<int, string> dicDefault = new Dictionary<int, string>(){{ -2, "매입처 없음"}};

                Util.LookupEditHelper(lePurchaseCd, dicDefault);
                lePurchaseCd.EditValue = -2;
            }
        }

        private void setinitialize()
        {
            var today = DateTime.Today;

            
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
                if (jData.ContainsKey("PURCHCD"))
                {
                    string sData = DBCommon.checkHMA02(ConvertUtil.ToInt32(jData["PURCHCD"]));
                    if (!string.IsNullOrWhiteSpace(sData))
                    {
                        if (Dangol.MessageYN($"지정하신 매입처가 {sData}로 설정되어 있습니다.\n계속 진행하시겠습니까?") == DialogResult.Yes)
                            searchHandler(jData);
                    }
                    else
                        searchHandler(jData);
                }
                else
                    searchHandler(jData);
            }
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
                int storeCd = ConvertUtil.ToInt32(leStoreCd.EditValue);
                if (storeCd == -2)
                {
                    jData.Add("MSG", $"매장 정보가 없습니다.");
                    return false;
                }
                else if (storeCd == 99)
                {
                    jData.Add($"STORECD", storeCd);
                }
                else
                    jData.Add($"STORECD", storeCd);

                int groupCd = ConvertUtil.ToInt32(leGroupCd.EditValue);
                if (groupCd == -2)
                {
                    jData.Add("MSG", $"조코드 정보가 없습니다.");
                    return false;
                }
                else if (groupCd == 99)
                {
                    jData.Add($"RET_GROUPCD", groupCd);
                }
                else
                    jData.Add($"RET_GROUPCD", groupCd);
            }
            else
            {
                jData.Add("STORECD", 99);
                jData.Add("GROUPCD", 99);
            }

            int purchCd = ConvertUtil.ToInt32(lePurchaseCd.EditValue);

            if(purchCd == -2)
            {
                jData.Add("MSG", $"매입처 정보가 없습니다.");
                return false;
            }
            else if(purchCd == -1)
            {

            }
            else
                jData.Add($"PURCHCD", purchCd);

            return true;
        }

        private void teStand_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string seller = tePurchaseNm.Text.Trim();

            //    using (dlgPurchaseList sellerList = new dlgPurchaseList(seller))
            //    {
            //        //sellerList.StartPosition = FormStartPosition.Manual;
            //        //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
            //        //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

            //        if (sellerList.ShowDialog(this) == DialogResult.OK)
            //        {
            //            tePurchaseNm.Text = ConvertUtil.ToString(sellerList._drv["PURCHNM"]);
            //            lePurchaseCd.Text = ConvertUtil.ToString(sellerList._drv["PURCHCD"]);
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

            //teGSCD.ReadOnly = purchaseType == 1;
            //teGSNm.ReadOnly = purchaseType == 1;

            //if (purchaseType == 1)
            //{
            //    teGSCD.Text = "";
            //    teGSNm.Text = "";
            //}
        }

        private void rgPurchaseRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            int purchaseType = ConvertUtil.ToInt32(rgPurchaseRange.EditValue);

            //lePurchaseCd.ReadOnly = purchaseType == 1;
            //tePurchaseNm.ReadOnly = purchaseType == 1;

            //if (purchaseType == 1)
            //{
            //    lePurchaseCd.Text = "";
            //    tePurchaseNm.Text = "";
            //}
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

        private void rgSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int purchaseType = ConvertUtil.ToInt32(rgSearchType.EditValue); //1:점단위, 2:매장단위

            if (purchaseType == 1)
            {
                leStoreCd.ReadOnly = true;
                leGroupCd.ReadOnly = true;

                leStoreCd.EditValueChanged -= leStoreCd_EditValueChanged;
                leGroupCd.EditValueChanged -= leGroupCd_EditValueChanged;

                Util.LookupEditHelper(leStoreCd, _dicDefault);
                Util.LookupEditHelper(leGroupCd, _dicDefault);

                leStoreCd.EditValue = -2;
                leGroupCd.EditValue = -2;

                leStoreCd.EditValueChanged += leStoreCd_EditValueChanged;
                leGroupCd.EditValueChanged += leGroupCd_EditValueChanged;

                if (!setPurchCdLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), 99, 99, true))
                {
                    Dictionary<int, string> dicDefault2 = new Dictionary<int, string>(){ { -2, "매입처 없음"}};
                    Util.LookupEditHelper(lePurchaseCd, dicDefault2);
                    lePurchaseCd.EditValue = -2;
                }
            }
            else
            {
                leStoreCd.ReadOnly = false;
                leGroupCd.ReadOnly = false;
                leStoreCd.EditValueChanged -= leStoreCd_EditValueChanged;
                leGroupCd.EditValueChanged -= leGroupCd_EditValueChanged;

                if (setStoreLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue)))
                {
                    int storeCd = ConvertUtil.ToInt32(leStoreCd.EditValue);

                    if (setGroupLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), storeCd))
                    {
                        int groupCd = ConvertUtil.ToInt32(leGroupCd.EditValue);

                        if (!setPurchCdLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), storeCd, groupCd))
                        {
                            Dictionary<int, string> dicDefault2 = new Dictionary<int, string>() { { -2, "매입처 없음" } };
                            Util.LookupEditHelper(lePurchaseCd, dicDefault2);
                            lePurchaseCd.EditValue = -2;
                        }
                    }
                    else
                    {
                        Dictionary<int, string> dicDefault1 = new Dictionary<int, string>() { { -2, "조코드 없음" } };
                        Util.LookupEditHelper(leGroupCd, dicDefault1);
                        leGroupCd.EditValue = -2;

                        Dictionary<int, string> dicDefault2 = new Dictionary<int, string>() { { -2, "매입처 없음" } };
                        Util.LookupEditHelper(lePurchaseCd, dicDefault2);
                        lePurchaseCd.EditValue = -2;
                    }
                }
                else
                {

                    Dictionary<int, string> dicDefault1 = new Dictionary<int, string>() { { -2, "매장 없음" } };
                    Util.LookupEditHelper(leStoreCd, dicDefault1);
                    leStoreCd.EditValue = -2;

                    Dictionary<int, string> dicDefault2 = new Dictionary<int, string>() { { -2, "조코드 없음" } };
                    Util.LookupEditHelper(leGroupCd, dicDefault2);
                    leGroupCd.EditValue = -2;

                    Dictionary<int, string> dicDefault3 = new Dictionary<int, string>() { { -2, "매입처 없음" } };
                    Util.LookupEditHelper(lePurchaseCd, dicDefault3);
                    lePurchaseCd.EditValue = -2;
                }

                leStoreCd.EditValueChanged += leStoreCd_EditValueChanged;
                leGroupCd.EditValueChanged += leGroupCd_EditValueChanged;
            }
        }

        private void leStoreCd_EditValueChanged(object sender, EventArgs e)
        {
            int storeCd = ConvertUtil.ToInt32(leStoreCd.EditValue);

            leGroupCd.EditValueChanged -= leGroupCd_EditValueChanged;

            if (setGroupLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), storeCd))
            {
                int groupCd = ConvertUtil.ToInt32(leGroupCd.EditValue);

                if (!setPurchCdLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), storeCd, groupCd))
                {
                    Dictionary<int, string> dicDefault2 = new Dictionary<int, string>() { { -2, "매입처 없음" } };
                    Util.LookupEditHelper(lePurchaseCd, dicDefault2);
                    lePurchaseCd.EditValue = -2;
                }
            }
            else
            {
                Dictionary<int, string> dicDefault1 = new Dictionary<int, string>() { { -2, "조코드 없음" } };
                Util.LookupEditHelper(leGroupCd, dicDefault1);
                leGroupCd.EditValue = -2;

                Dictionary<int, string> dicDefault2 = new Dictionary<int, string>(){{ -2, "매입처 없음"}};
                Util.LookupEditHelper(lePurchaseCd, dicDefault2);
                lePurchaseCd.EditValue = -2;
            }

            leGroupCd.EditValueChanged += leGroupCd_EditValueChanged;
        }

        private void leGroupCd_EditValueChanged(object sender, EventArgs e)
        {
            int storeCd = ConvertUtil.ToInt32(leStoreCd.EditValue);
            int groupCd = ConvertUtil.ToInt32(leGroupCd.EditValue);

            if (!setPurchCdLookupEdit(ConvertUtil.ToInt32(leShopCd.EditValue), storeCd, groupCd))
            {
                Dictionary<int, string> dicDefault2 = new Dictionary<int, string>() { { -2, "매입처 없음" } };
                Util.LookupEditHelper(lePurchaseCd, dicDefault2);
                lePurchaseCd.EditValue = -2;
            }
        }

        private bool setPurchCdLookupEdit(int shopCd, int storeCd, int groupCd, bool isSearchAll = false, bool init = false)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("SHOPCD", shopCd);
            jobj.Add("STORECD", storeCd);
            if(!isSearchAll) jobj.Add("GROUPCD", groupCd);
            

            string url = "/returns/getPurchInfo4return.json";

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

                        dr["KEY"] = ConvertUtil.ToInt32(obj["PURCHCD"]);
                        dr["VALUE"] = ConvertUtil.ToString(obj["PURCHNM"]);

                        dt.Rows.Add(dr);

                        if(index == 1)
                        {
                            purchCd = ConvertUtil.ToInt32(obj["PURCHCD"]);
                            index++;
                        }
                    }

                    //Util.insertRowonTop(dt, -1, "전체");

                    Util.LookupEditHelper(lePurchaseCd, dt, "KEY", "VALUE");
                    lePurchaseCd.EditValue = purchCd;

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

        private bool setGroupLookupEdit(int shopCd, int storeCd, bool init = false)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("SHOPCD", shopCd);
            jobj.Add("STORECD", storeCd);

            string url = "/returns/getGroupInfo4return.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("KEY", typeof(int)));
                    dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                    int index = 1;
                    int groupCd = -1;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = dt.NewRow();

                        dr["KEY"] = ConvertUtil.ToInt32(obj["RET_GROUPCD"]);
                        dr["VALUE"] = $"{obj["RET_GROUPCD"]} - {obj["ORGAN_NM"]}";

                        dt.Rows.Add(dr);

                        if (index == 1)
                        {
                            groupCd = ConvertUtil.ToInt32(obj["RET_GROUPCD"]);
                            index++;
                        }
                    }

                    //Util.insertRowonTop(dt, 99, "99 - 전체");

                    Util.LookupEditHelper(leGroupCd, dt, "KEY", "VALUE");
                    leGroupCd.EditValue = groupCd;

                    return true;
                }
                else
                {
                    if (!init)
                        Dangol.Error("입력하신 조건에 해당하는 조코드가 없습니다.");
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

        private bool setStoreLookupEdit(int shopCd, bool init = false)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("SHOPCD", shopCd);

            string url = "/returns/getStoreInfo4return.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("KEY", typeof(int)));
                    dt.Columns.Add(new DataColumn("VALUE", typeof(string)));

                    int index = 1;
                    int storeCd = -1;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = dt.NewRow();

                        dr["KEY"] = ConvertUtil.ToInt32(obj["STORECD"]);
                        dr["VALUE"] = ConvertUtil.ToString(obj["STORECD"]);

                        dt.Rows.Add(dr);

                        if (index == 1)
                        {
                            storeCd = ConvertUtil.ToInt32(obj["STORECD"]);
                            index++;
                        }
                    }

                    //Util.insertRowonTop(dt, 99, "99 - 전체");

                    Util.LookupEditHelper(leStoreCd, dt, "KEY", "VALUE");
                    leStoreCd.EditValue = storeCd;

                    return true;
                }
                else
                {
                    if (!init)
                        Dangol.Error("입력하신 조건에 해당하는 매장이 없습니다.");
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
            rgSearchType.Focus();
        }

        public void clear()
        {
            //teStoreCd_S.Text = "";
            //teStoreCd_E.Text = "";
        }

        private void rgSearchType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
