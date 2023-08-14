using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.common
{
    public partial class usrBookInfoDetail : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;

        long _bookCd;
        int _shopCd;

        XtraTabPage[] _arrXtraTabPage;
        usrPurchaseInfo[] _arrUsrPurchaseInfo;

        public delegate void SearchHandler(JObject jobj);
        public event SearchHandler searchHandler;

        public usrBookInfoDetail()
        {
            InitializeComponent();

            _searchType = -1;
            _bookCd = -1;
            _shopCd = 1;

            _arrXtraTabPage = new XtraTabPage[] { xtpPruchaseList1, xtpPruchaseList2 , xtpPruchaseList3 , xtpPruchaseList4 , xtpPruchaseList5 , xtpPruchaseList6 , xtpPruchaseList7, xtpPruchaseList8 };
            _arrUsrPurchaseInfo = new usrPurchaseInfo[] { usrPurchaseInfo1, usrPurchaseInfo2, usrPurchaseInfo3, usrPurchaseInfo4, usrPurchaseInfo5, usrPurchaseInfo6, usrPurchaseInfo7, usrPurchaseInfo8 };
        }

        public void setInitLoad(bool isEditable)
        {
            setInfoBox();
            setinitialize(isEditable);
        }


        private void setInfoBox()
        {
            List<string> listLongCol = new List<string>(new string[] { "SHOPCD" });
            DataTable dtShopCd = Util.getTable("HMA09", "SHOPCD,SHOP_NM ", "SHOPCD > 0", "SHOPCD ASC", listLongCol);
            Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");

        }

        private void setinitialize(bool isEditable)
        {
            setXtratabinitialize();

            foreach(usrPurchaseInfo control in _arrUsrPurchaseInfo)
                control.setInitLoad();

            teBookCd.ReadOnly = !isEditable;
        }
        private void setXtratabinitialize()
        {
            for (int i = 0; i < _arrXtraTabPage.Length; i++)
                _arrXtraTabPage[i].PageVisible = false;
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

        public void getList(long bookCd, int shopCd = 1)
        {
            _bookCd = bookCd;

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/search/getBookInfoDetail.json";

            jobj.Add("BOOKCD", bookCd);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    teBookCd.Text = ConvertUtil.ToString(jResult["BOOKCD"]);
                    teBookNm.Text = ConvertUtil.ToString(jResult["BOOKNM"]);
                    tePublisherCd.Text = ConvertUtil.ToString(jResult["PUBSHCD"]);
                    tePublisher.Text = ConvertUtil.ToString(jResult["PUBSHNM"]);
                    sePrice.EditValue = ConvertUtil.ToInt32(jResult["PRICE"]);
                    teAuthor1.Text = ConvertUtil.ToString(jResult["AUTHOR1"]);
                    teAuthor2.Text = ConvertUtil.ToString(jResult["AUTHOR2"]);
                    teEditor.Text = ConvertUtil.ToString(jResult["TRANS_EDITOR"]);
                    teAbstract.Text = ConvertUtil.ToString(jResult["CONTENTS"]);
                    teTax.Text = ConvertUtil.ToString(jResult["TAX_FG"]);
                    deFirstIssue.EditValue = ConvertUtil.ToDateTimeNull(jResult["FIRSTISSUE"]);
                    

                    teDeptCd.Text = ConvertUtil.ToString(jResult["DEPTCD"]);
                    teDeptNm.Text = ConvertUtil.ToString(jResult["DEPT_NM"]);

                    teGroupCd.Text = ConvertUtil.ToString(jResult["GROUPCD"]);
                    teGroupNm.Text = ConvertUtil.ToString(jResult["GROUP_NM"]);

                    teStandCd.Text = ConvertUtil.ToString(jResult["STANDCD"]);
                    teStandNm.Text = ConvertUtil.ToString(jResult["STAND_NM"]);

                    teProductCd.Text = ConvertUtil.ToString(jResult["PRODUCT_CD"]);
                    teProductNm.Text = ConvertUtil.ToString(jResult["PRODUCT_NM"]);

                    //JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    //int index = 1;

                    //gvList.BeginDataUpdate();

                    //foreach (JObject obj in jArray.Children<JObject>())
                    //{
                    //    DataRow dr = _dt.NewRow();

                    //    dr["NO"] = index++;
                    //    dr["ID"] = ConvertUtil.ToInt64(obj["ID"]);
                    //    dr["BOOKCD"] = ConvertUtil.ToInt64(obj["BOOKCD"]);
                    //    dr["BOOKNM"] = ConvertUtil.ToString(obj["BOOKNM"]);
                    //    dr["AUTHOR1"] = ConvertUtil.ToString(obj["AUTHOR1"]);
                    //    dr["AUTHOR2"] = ConvertUtil.ToString(obj["AUTHOR2"]);
                    //    dr["PUBSHNM"] = ConvertUtil.ToString(obj["PUBSHNM"]);
                    //    dr["SPECIALNM"] = ConvertUtil.ToString(obj["SPECIALNM"]);
                    //    dr["GROUPCD"] = ConvertUtil.ToString(obj["GROUPCD"]);
                    //    dr["GROUP_NM"] = ConvertUtil.ToString(obj["GROUP_NM"]);
                    //    dr["STANDCD"] = ConvertUtil.ToString(obj["STANDCD"]);
                    //    dr["STAND_NM"] = ConvertUtil.ToString(obj["STAND_NM"]);

                    //    dr["PRICE"] = ConvertUtil.ToInt32(obj["PRICE"]);
                    //    dr["RETURN_CNT"] = ConvertUtil.ToInt32(obj["RETURN_CNT"]);
                    //    dr["DELIVERY_CNT"] = ConvertUtil.ToInt32(obj["DELIVERY_CNT"]);
                    //    dr["STOCK"] = ConvertUtil.ToInt32(obj["STOCK"]);

                    //    dr["STATE"] = 1;
                    //    dr["CHECK"] = false;
                    //    _dt.Rows.Add(dr);
                    //}

                    //gvList.EndDataUpdate();


                    getListDetail(shopCd);
                    getPurchaseListInfo(shopCd, bookCd);
                }
                else
                {
                    Dangol.Info("검색 결과가 없습니다.");
                }
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
            }
          
        }

        public void getListDetail(int shopCd = 1)
        {
            _shopCd = shopCd;

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/search/getShopBookInfoDetail.json";

            jobj.Add("BOOKCD", _bookCd);
            jobj.Add("SHOPCD", shopCd);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    sePrvPrice.EditValue = ConvertUtil.ToInt32(jResult["PRICE"]);
                    dePrvPriceDt.EditValue = ConvertUtil.ToDateTimeNull(jResult["PRICE_DATE"]);
                    seStock.EditValue = ConvertUtil.ToInt32(jResult["STOCK"]);
                    seStandStock.EditValue = ConvertUtil.ToInt32(jResult["STAND_STOCK"]);
                    teOutBookFlag.Text = ConvertUtil.ToString(jResult["OUT_BOOK_FG"]);
                    teSaleFlag.Text = ConvertUtil.ToString(jResult["SALE_FG"]);
                    teISBN.Text = ConvertUtil.ToString(jResult["ISBN_FG"]);
                    teBarcodeYn.Text = ConvertUtil.ToString(jResult["BARCODE_FG"]);
                    sePlanCount.EditValue = ConvertUtil.ToInt32(jResult["INP_PLAN_COUNT"]);
                    deFirstStore.EditValue = ConvertUtil.ToDateTimeNull(jResult["FIRSTSTORE"]);
                    deLastStore.EditValue = ConvertUtil.ToDateTimeNull(jResult["LASTSTORE"]);

                    deFirstSales.EditValue = ConvertUtil.ToDateTimeNull(jResult["FIRSTSALES"]);
                    deLastSales.EditValue = ConvertUtil.ToDateTimeNull(jResult["LASTSALES"]);

                    teAddSign.Text = Common.addComma(ConvertUtil.ToString(jResult["ADDSIGN"]));
                    teSaleFront.Text = Common.addComma(ConvertUtil.ToString(jResult["SALEFRONT"]));

                    teSpecialNo.Text = Common.addComma(ConvertUtil.ToInt32(jResult["SPECIAL_NO"]));
                    teSpecialNm.Text = ConvertUtil.ToString(jResult["SPECIAL_NM"]);
                }
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
            }

        }

        public void getPurchaseListInfo(int shopCd, long bookcd)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/search/getBookPurchasInfo.json";

            jobj.Add("SHOPCD", shopCd);
            jobj.Add("BOOKCD", bookcd);

            setXtratabinitialize();

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = -1;
                    int prePurProcess = -1;
                    int purProcess = -1;
                    int order = 1;
                    usrPurchaseInfo usrPurchaseInfoControl;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        purProcess = ConvertUtil.ToInt32(obj["PUR_PROCESS"]);

                        if(prePurProcess != purProcess)
                        {
                            prePurProcess = purProcess;
                            index++;
                            order = 1;

                            _arrXtraTabPage[index].PageVisible = true;
                            usrPurchaseInfoControl = _arrUsrPurchaseInfo[index];
                            usrPurchaseInfoControl.reSetGrid();
                            usrPurchaseInfoControl.addData(obj, order);
                        }
                        else
                        {
                            usrPurchaseInfoControl = _arrUsrPurchaseInfo[index];
                            usrPurchaseInfoControl.addData(obj, order);
                            order++;
                        }
                    }
                }
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
            }

        }

        private void leShopCd_EditValueChanged(object sender, EventArgs e)
        {
            getListDetail(ConvertUtil.ToInt32(leShopCd.EditValue));
        }

        private void teBookCd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEditor = (TextEdit)sender;
                string data = textEditor.Text;
                long bookCd = ConvertUtil.ToInt64(data);

                if (bookCd < 1)
                {
                    Dangol.Warining("도서코드를 입력하세요");
                }
                else
                {
                    JObject jobj = new JObject();
                    jobj.Add("BOOKCD", bookCd);
                    searchHandler(jobj);
                }
            }
        }
    }
}
