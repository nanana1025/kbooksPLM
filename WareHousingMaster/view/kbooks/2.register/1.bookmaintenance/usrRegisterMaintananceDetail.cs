using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.register
{
    public partial class usrRegisterMaintananceDetail : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _dt;
        BindingSource _bs;
        public int _releaseCategory { get; set; }

        Dictionary<int, Dictionary<string, string>> _dicPurchaseInfo;
        Dictionary<int, DataTable> _dicPurchaseInfoTable;

        Dictionary<int, string> _dicRateKbn;

        int _viewType;
        int _searchType;

        long _bookCd;
        int _shopCd;

        XtraTabPage[] _arrXtraTabPage;
        //usrPurchaseInfo[] _arrUsrPurchaseInfo;

        public delegate void ButtonHandler(bool search, bool update, bool delete);
        public event ButtonHandler buttonHandler;

        public usrRegisterMaintananceDetail()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("SHOPCD", typeof(int)));
            _dt.Columns.Add(new DataColumn("SHOP_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            
            _bs = new BindingSource();

            _dicPurchaseInfo = new Dictionary<int, Dictionary<string, string>>();
            _dicPurchaseInfoTable = new Dictionary<int, DataTable>();

            _dicRateKbn = new Dictionary<int, string>()
            {
                {11, "위탁1"},
                {12, "위탁2"},
                {13, "위탁3"},
                {14, "위탁4"},
                {21, "현매1"},
                {22, "현매2"},
                {23, "현매3"},
                {24, "현매4"},
            };

            _searchType = -1;
            _bookCd = -1;
            _shopCd = 1;

            //_arrXtraTabPage = new XtraTabPage[] { xtpPruchaseList1, xtpPruchaseList2 , xtpPruchaseList3 , xtpPruchaseList4 , xtpPruchaseList5 , xtpPruchaseList6 , xtpPruchaseList7, xtpPruchaseList8 };
            //_arrUsrPurchaseInfo = new usrPurchaseInfo[] { usrPurchaseInfo1, usrPurchaseInfo2, usrPurchaseInfo3, usrPurchaseInfo4, usrPurchaseInfo5, usrPurchaseInfo6, usrPurchaseInfo7, usrPurchaseInfo8 };
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

            int index = 1;

            foreach (DataRow row in dtShopCd.Rows)
            {
                DataRow dr = _dt.NewRow();

                dr["NO"] = index;
                dr["SHOPCD"] = ConvertUtil.ToInt32(row["SHOPCD"]);
                dr["SHOP_NM"] = ConvertUtil.ToString(row["SHOP_NM"]);
                dr["CHECK"] = index == 1;
                _dt.Rows.Add(dr);

                index++;
            }

            //Util.LookupEditHelper(leISBNFGCd, dtShopCd, "SHOPCD", "SHOP_NM");

            DataTable dtFlagCd = new DataTable();
            dtFlagCd.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtFlagCd.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRow(dtFlagCd, 1, "1: ISBN부여+EAN부착");
            Util.insertRow(dtFlagCd, 2, "2: ISBN부여+EAN미부착");
            Util.insertRow(dtFlagCd, 3, "3: INSTORE");
            Util.insertRow(dtFlagCd, -1, "ERROR");

            Util.LookupEditHelper(leISBNFG, dtFlagCd, "KEY", "VALUE");

            //DataTable dtFlag = new DataTable();
            //dtFlag.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtFlag.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRow(dtFlag, 1, "1: ISBN부여+EAN부착");
            //Util.insertRow(dtFlag, 2, "2: ISBN부여+EAN미부착");
            //Util.insertRow(dtFlag, 3, "3: INSTORE");

            //Util.LookupEditHelper(leISBNFG, dtFlag, "KEY", "VALUE");

        }

        private void setinitialize()
        {
            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;
        }
        public void clear()
        {
            teBookCd.Text = "";
            teBookNm.Text = "";
            teStandCd.Text = "";
            teStandNm.Text = "";
            teProductCd.Text = "";
            teProductNm.Text = "";
            tePublisherCd.Text = "";
            tePublisher.Text = "";
            sePrice.EditValue = "";
            teTax.Text = "";
            teAuthor1.Text = "";
            teAuthor2.Text = "";
            teEditor.Text = "";

            teDeptCd.Text = "";
            teDeptNm.Text = "";

            teGroupCd.Text = "";
            teGroupNm.Text = "";

            teStandCd.Text = "";
            teStandNm.Text = "";

            seStock.EditValue = "";

            deFirstIssue.EditValue = "";

            sePrvPrice.EditValue = "";
            dePrvPriceDt.EditValue = "";
            
            seStandStock.EditValue = "";
            teJPFlag.Text = "";
            tePJFlag.Text = "";
            teISBN.Text = "";
            teBarcodeYn.Text = "0";
            sePlanCount.EditValue = "";
            deFirstStore.EditValue = "";
            deLastStore.EditValue = "";

            deFirstSales.EditValue = "";
            deLastSales.EditValue = "";

            teAddSign.Text = "";
            teSaleFront.Text = "";

            teSpecialNo.Text = "";
            teSpecialNm.Text = "";

            _dicPurchaseInfo.Clear();
            _dicPurchaseInfoTable.Clear();
        }

        public void setFocus()
        {
            teISBNFG.Focus();
        }

        private void setPurchaseInfoInitialize()
        {
            _dicPurchaseInfo.Clear();
            _dicPurchaseInfoTable.Clear();

            for(int i = 1; i < 7; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("PURCHCD", "");
                dic.Add("PURCHNM", "");
                dic.Add("PRIORITY", "");
                _dicPurchaseInfo.Add(i, dic);

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("NAME", typeof(string)));
                dt.Columns.Add(new DataColumn("PURCHASE_RATIO", typeof(string)));
                dt.Columns.Add(new DataColumn("PRICE", typeof(string)));
                dt.Columns.Add(new DataColumn("STOCK", typeof(string)));
                dt.Columns.Add(new DataColumn("RETURN_CNT", typeof(string)));


                foreach (KeyValuePair<int, string> item in Common._dicRateKbn)
                {
                    DataRow dr = dt.NewRow();

                    dr["ID"] = item.Key;
                    dr["NAME"] = item.Value;
                    dt.Rows.Add(dr);
                }

                _dicPurchaseInfoTable.Add(i, dt);
            }
            
        }


        public void setPurchaseInfo()
        {
            using (usrRegisterMaintanancePurchase registerMaintanancePurchase = new usrRegisterMaintanancePurchase(_dicPurchaseInfo, _dicPurchaseInfoTable, ConvertUtil.ToInt32(sePrice.EditValue)))
            {
                //sellerList.StartPosition = FormStartPosition.Manual;
                //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                if (registerMaintanancePurchase.ShowDialog(this) == DialogResult.OK)
                {
                    //sePrice.EditValue = registerMaintanancePurchase._modifiedPrice;
                }
            }
        }

        private void teISBNFG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                clear();

                int isbn = ConvertUtil.ToInt32(leISBNFG.EditValue);

                if (isbn == 2 || isbn == 3)
                {
                    int bookCd = getNewBookCd();
                    if(bookCd > 0)
                    {
                        teBookCd.Text = ConvertUtil.ToString(bookCd);

                        seStock.Text = "0";
                        teJPFlag.Text = "0";
                        tePJFlag.Text = "0";
                        tePJFlag.Text = "1";

                        setPurchaseInfoInitialize();
                        buttonHandler(true, false, false);
                        teBookNm.Focus();
                    }
                        
                }
                else if(isbn == 1)
                {
                    teBookCd.Focus();
                    buttonHandler(false, false, false);
                }
            }
        }
        public int getNewBookCd()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/regist/getNewBookCd.json";

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    return ConvertUtil.ToInt32(jResult["BOOKCD"]);
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                    return -1;
                }
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
                return -1;
            }
        }

        public void getBookInfo(long bookCd)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/regist/getBookRegistInfo.json";

            jobj.Add("BOOKCD", bookCd);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    teBookNm.Text = ConvertUtil.ToString(jResult["BOOKNM"]);
                    teStandCd.Text = ConvertUtil.ToString(jResult["STANDCD"]);
                    tePublisherCd.Text = ConvertUtil.ToString(jResult["PUBSHCD"]);
                    sePrice.EditValue = ConvertUtil.ToInt32(jResult["PRICE"]);
                    teTax.Text = ConvertUtil.ToString(jResult["TAX_FG"]);
                    deFirstIssue.EditValue = ConvertUtil.ToDateTimeNull(jResult["FIRSTISSUE"]);
                    teAuthor1.Text = ConvertUtil.ToString(jResult["AUTHOR1"]);
                    teAuthor2.Text = ConvertUtil.ToString(jResult["AUTHOR2"]);
                    teEditor.Text = ConvertUtil.ToString(jResult["TRANS_EDITOR"]);
                    teDeptCd.Text = ConvertUtil.ToString(jResult["DEPTCD"]);
                    teGroupCd.Text = ConvertUtil.ToString(jResult["GROUPCD"]);
                    seStock.EditValue = ConvertUtil.ToInt32(jResult["STOCK"]);
                    tePublisher.Text = ConvertUtil.ToString(jResult["PUBSHNM"]);
                    teISBN.Text = ConvertUtil.ToString(jResult["ISBM_CD"]);
                    teAddSign.Text = ConvertUtil.ToString(jResult["ADDSIGN"]);  //MAX LENGTH 5
                    teJPFlag.Text = ConvertUtil.ToString(jResult["OUT_BOOK_FG"]);
                    tePJFlag.Text = ConvertUtil.ToString(jResult["SALE_FG"]);

                    teProductCd.Text = ConvertUtil.ToString(jResult["PRODUCT_CD"]);
                    teProductNm.Text = ConvertUtil.ToString(jResult["PRODUCT_NM"]);

                    getListDetail(1, bookCd);

                    setOherCode(1, ConvertUtil.ToInt32(jResult["DEPTCD"]), ConvertUtil.ToInt32(jResult["GROUPCD"]), ConvertUtil.ToInt32(jResult["STANDCD"]), true, true, true);

                    setPurchaseInfoInitialize();
                    getPurchaseListInfo(1, bookCd);

                    buttonHandler(false, true, true);

                }
                else
                {
                    clear();
                    teBookCd.Text = ConvertUtil.ToString(bookCd);
                    buttonHandler(true, false, false);
                    teBookNm.Focus();
                    Dangol.Info("도서정보가 없음");
                }
            }
            else
            {
                buttonHandler(false, false, false);
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

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int prePurProcess = -1;
                    int purProcess = -1;
                    int order = 0;

                    DataTable dt = new DataTable();

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        purProcess = ConvertUtil.ToInt32(obj["PUR_PROCESS"]);

                        if (prePurProcess != purProcess)
                        {
                            prePurProcess = purProcess;

                            order++;

                            _dicPurchaseInfo[order]["PURCHCD"] = ConvertUtil.ToString(obj["PURCHCD"]);
                            _dicPurchaseInfo[order]["PURCHNM"] = ConvertUtil.ToString(obj["PURCHNM"]);
                            _dicPurchaseInfo[order]["PRIORITY"] = ConvertUtil.ToString(obj["PUR_PROCESS"]);

                            dt = _dicPurchaseInfoTable[order];
                        }

                        int rateKbn = ConvertUtil.ToInt32(obj["RATE_KBN"]);

                        DataRow[] rows = dt.Select($"ID = {rateKbn}");

                        foreach (DataRow row in rows)
                        {
                            row["PURCHASE_RATIO"] = ConvertUtil.ToString(obj["RATE"]);
                            row["PRICE"] = ConvertUtil.ToInt32(obj["COST"]);
                            row["STOCK"] = ConvertUtil.ToInt32(obj["STOCK"]);
                            row["RETURN_CNT"] = ConvertUtil.ToInt32(obj["RET_PLAN_COUNT"]);
                        }
                    }
                }
            }
            else
            {
                //Dangol.Error(jResult["MSG"]);
            }

        }

        public void getListDetail(int shopCd, long bookCd)
        {
            _shopCd = shopCd;

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/search/getShopBookInfoDetail.json";

            jobj.Add("BOOKCD", bookCd);
            jobj.Add("SHOPCD", shopCd);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    
                    sePrvPrice.EditValue = ConvertUtil.ToInt32(jResult["PRICE"]);
                    dePrvPriceDt.EditValue = ConvertUtil.ToDateTimeNull(jResult["PRICE_DATE"]);
                    seStock.EditValue = ConvertUtil.ToInt32(jResult["STOCK"]);
                    seStandStock.EditValue = ConvertUtil.ToInt32(jResult["STAND_STOCK"]);
                    //teISBN.Text = ConvertUtil.ToString(jResult["ISBN_FG"]);
                    string barcodeYn = ConvertUtil.ToString(jResult["BARCODE_FG"]);
                    if (string.IsNullOrEmpty(barcodeYn))
                        teBarcodeYn.Text = "0";
                    else
                        teBarcodeYn.Text = barcodeYn.Substring(0, 1);
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
                //Dangol.Error(jResult["MSG"]);
            }

        }

        private bool check()
        {
            long bookCd = ConvertUtil.ToInt64(teBookCd.Text.Trim());

            if (bookCd < 1)
            {
                Dangol.Warining("도서 정보가 없습니다.");
                return false;
            }

            //if (string.IsNullOrEmpty(teAuthor1.Text.Trim()) && string.IsNullOrEmpty(teAuthor2.Text.Trim()))
            //{
            //    Dangol.Warining("저자 정보가 없습니다.");
            //    return false;
            //}

            int deptCd = ConvertUtil.ToInt32(teDeptCd.Text);
            if (deptCd < 1)
            {
                Dangol.Warining("부 정보가 없습니다.");
                return false;
            }

            int groupCd = ConvertUtil.ToInt32(teGroupCd.Text);
            if (groupCd < 1)
            {
                Dangol.Warining("조 정보가 없습니다.");
                return false;
            }

            int standCd = ConvertUtil.ToInt32(teStandCd.Text);
            if (standCd < 1)
            {
                Dangol.Warining("서가 정보가 없습니다.");
                return false;
            }

            long pubshCd = ConvertUtil.ToInt64(tePublisherCd.Text);

            if (pubshCd < 1)
            {
                Dangol.Warining("출판사 정보가 없습니다.");
                return false;
            }

            bool isPurchaseExist = false;
            for (int i = 1; i < 7; i++)
            {
                int purchCd = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PURCHCD"]);
                if (purchCd > 0)
                {
                    isPurchaseExist = true;

                    int priority = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PRIORITY"]);

                    if (priority < 1)
                    {
                        Dangol.Warining("매입처 처리순위를 확인하세요.");
                        return false;
                    }

                    bool isinfoExist = false;
                    foreach (DataRow row in _dicPurchaseInfoTable[i].Rows)
                    {
                        int ratio = ConvertUtil.ToInt32(row["PURCHASE_RATIO"]);

                        if (ratio > 0)
                            isinfoExist = true;
                    }


                    if (!isinfoExist)
                    {
                        Dangol.Warining("매입 정보를 확인하세요.");
                        return false;
                    }

                }
            }

            if(!isPurchaseExist)
            {
                Dangol.Warining("매입처가 없습니다.");
                return false;
            }


            return true;

        }

        public void DeleteBookInfo()
        {
            if (Dangol.MessageYN("도서 정보를 삭제하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    string url = "/regist/deleteBookInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("BOOKCD", teBookCd.Text.Trim());

                    jobj.Add("DEPTCD", ConvertUtil.ToInt32(teDeptCd.Text));
                    jobj.Add("GROUPCD", ConvertUtil.ToInt32(teGroupCd.Text));
                    jobj.Add("STANDCD", ConvertUtil.ToInt32(teStandCd.Text));


                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        clear();
                        teISBNFG.Focus();
                        buttonHandler(false, false, false);
                        Dangol.Info("삭제되었습니다.");
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
            }
        }

        public void UpdateBookInfo()
        {
            if (Dangol.MessageYN("도서 정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    long bookCd = ConvertUtil.ToInt64(teBookCd.Text.Trim());
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/updateBookInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("BOOKCD", bookCd);
                    jobj.Add("BOOKNM", teBookNm.Text.Trim());

                    jobj.Add("DEPTCD", ConvertUtil.ToInt32(teDeptCd.Text));
                    jobj.Add("GROUPCD", ConvertUtil.ToInt32(teGroupCd.Text));
                    jobj.Add("STANDCD", ConvertUtil.ToInt32(teStandCd.Text));
                    jobj.Add("PUBSHCD", ConvertUtil.ToInt32(tePublisherCd.Text));

                    jobj.Add("BOOK_FLAG", 2);
                    jobj.Add("UP_END_FG", 2);
                    jobj.Add("PRICE", ConvertUtil.ToInt32(sePrice.EditValue));
                    jobj.Add("TAX_FG", ConvertUtil.ToInt32(teTax.Text));
                    jobj.Add("STAND_STOCK", ConvertUtil.ToInt32(seStandStock.EditValue));
                    jobj.Add("SPECIAL_NO", ConvertUtil.ToInt32(teSpecialNo.Text));
                    jobj.Add("BARCODE_FG", ConvertUtil.ToInt32(teBarcodeYn.EditValue));
                    jobj.Add("ISBN_FG", ConvertUtil.ToInt32(teISBNFG.Text));

                    jobj.Add("FIRSTISSUE", Util.getDateTime(deFirstStore));

                    {
                        var jAuthor = new JArray();
                        string author1 = teAuthor1.Text.Trim();
                        string author2 = teAuthor2.Text.Trim();

                        if (!string.IsNullOrEmpty(author1))
                        {
                            JObject jAuthorObj = new JObject();
                            jAuthorObj.Add("AUTHORNM", author1);
                            jAuthorObj.Add("AUTHOR_KBN", 1);
                            jAuthorObj.Add("BOOKCD", bookCd);

                            jAuthor.Add(jAuthorObj);
                        }

                        if (!string.IsNullOrEmpty(author2))
                        {
                            JObject jAuthorObj = new JObject();
                            jAuthorObj.Add("AUTHORNM", author2);
                            jAuthorObj.Add("AUTHOR_KBN", 2);
                            jAuthorObj.Add("BOOKCD", bookCd);

                            jAuthor.Add(jAuthorObj);
                        }

                        jobj.Add("AUTHOR1", author1);
                        jobj.Add("AUTHOR2", author2);
                        jobj.Add("AUTHOR", jAuthor);
                    }

                    jobj.Add("SALEFRONT", ConvertUtil.ToInt32(teSaleFront.Text));
                    jobj.Add("TRANS_EDITOR", teEditor.Text);
                    jobj.Add("LD_CD", 0);
                    jobj.Add("MD_CD", 0);
                    jobj.Add("SD_CD", 0);
                    jobj.Add("ISBN_CD", teISBN.Text);
                    jobj.Add("ADDSIGN", teAddSign.Text);
                    jobj.Add("BOARD", "");
                    jobj.Add("BOOKSIZE", "");
                    jobj.Add("PAGE", "");
                    jobj.Add("OUT_BOOK_FG", 1);
                    jobj.Add("SALE_FG", ConvertUtil.ToInt32(tePJFlag.Text));
                    jobj.Add("BOARD_CH_DATE", "");
                    jobj.Add("PRODUCT_NM", teProductNm.Text);
                    jobj.Add("PRODUCT_CD", teProductCd.Text);


                    for (int i = 1; i < 7; i++)
                    {
                        int purchCd = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PURCHCD"]);
                        if (purchCd > 0)
                        {
                            JObject jPurchData = new JObject();

                            int priority = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PRIORITY"]);
                            var jPurchaseDetail = new JArray();

                            foreach (DataRow row in _dicPurchaseInfoTable[i].Rows)
                            {
                                JObject jPurchDetail = new JObject();

                                int ratio = ConvertUtil.ToInt32(row["PURCHASE_RATIO"]);

                                if (ratio > 0)
                                {
                                    jPurchDetail.Add("SHOPCD", 1);
                                    jPurchDetail.Add("BOOKCD", bookCd);
                                    jPurchDetail.Add("PURCHCD", purchCd);
                                    jPurchDetail.Add("RATE_KBN", ConvertUtil.ToInt32(row["ID"]));
                                    jPurchDetail.Add("RATE", ConvertUtil.ToInt32(row["PURCHASE_RATIO"]));
                                    jPurchDetail.Add("COST", ConvertUtil.ToInt32(row["PRICE"]));
                                    jPurchDetail.Add("STOCK", ConvertUtil.ToInt32(row["STOCK"]));

                                    jPurchaseDetail.Add(jPurchDetail);
                                }
                            }

                            jPurchData.Add("SHOPCD", 1);
                            jPurchData.Add("BOOKCD", bookCd);
                            jPurchData.Add("PURCHCD", purchCd);
                            jPurchData.Add("PUR_PROCESS", priority);
                            jPurchData.Add($"PURCH_DETAIL", jPurchaseDetail);

                            jobj.Add($"PURCH_{i}_DATA", jPurchData);
                        }
                    }


                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        Dangol.Info("수정되었습니다.");
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
            }
        }

        public void InsertBookInfo()
        {
            if (Dangol.MessageYN("도서 정보를 추가하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    long bookCd = ConvertUtil.ToInt64(teBookCd.Text.Trim());
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/insertBookInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("BOOKCD", bookCd);
                    jobj.Add("BOOKNM", teBookNm.Text.Trim());

                    jobj.Add("DEPTCD", ConvertUtil.ToInt32(teDeptCd.Text));
                    jobj.Add("GROUPCD", ConvertUtil.ToInt32(teGroupCd.Text));
                    jobj.Add("STANDCD", ConvertUtil.ToInt32(teStandCd.Text));
                    jobj.Add("PRICE", ConvertUtil.ToInt32(sePrice.EditValue));
                    jobj.Add("ADDSIGN", teAddSign.Text);

                    jobj.Add("PUBSHCD", ConvertUtil.ToInt32(tePublisherCd.Text));
                    jobj.Add("SALEFRONT", ConvertUtil.ToInt32(teSaleFront.Text));
                    jobj.Add("OUT_BOOK_FG", 1);
                    jobj.Add("SALE_FG", ConvertUtil.ToInt32(tePJFlag.Text));
                    jobj.Add("TAX_FG", ConvertUtil.ToInt32(teTax.Text));
                    jobj.Add("SPECIAL_NO", ConvertUtil.ToInt32(teSpecialNo.Text));
                    jobj.Add("ISBN_FG", ConvertUtil.ToInt32(teISBNFG.Text));


                    jobj.Add("BOOK_FLAG", 1);
                    jobj.Add("UP_END_FG", 2);
                    jobj.Add("FIRSTISSUE", Util.getDateTime(deFirstStore));

                    {
                        var jAuthor = new JArray();
                        string author1 = teAuthor1.Text.Trim();
                        string author2 = teAuthor2.Text.Trim();

                        if (!string.IsNullOrEmpty(author1))
                        {
                            JObject jAuthorObj = new JObject();
                            jAuthorObj.Add("AUTHORNM", author1);
                            jAuthorObj.Add("AUTHOR_KBN", 1);
                            jAuthorObj.Add("BOOKCD", bookCd);

                            jAuthor.Add(jAuthorObj);
                        }

                        if (!string.IsNullOrEmpty(author2))
                        {
                            JObject jAuthorObj = new JObject();
                            jAuthorObj.Add("AUTHORNM", author2);
                            jAuthorObj.Add("AUTHOR_KBN", 2);
                            jAuthorObj.Add("BOOKCD", bookCd);

                            jAuthor.Add(jAuthorObj);
                        }

                        jobj.Add("AUTHOR1", author1);
                        jobj.Add("AUTHOR2", author2);
                        jobj.Add("AUTHOR", jAuthor);
                    }

                    jobj.Add("TRANS_EDITOR", teEditor.Text);
                    jobj.Add("LD_CD", 0);
                    jobj.Add("MD_CD", 0);
                    jobj.Add("SD_CD", 0);
                    jobj.Add("ISBN_CD", teISBN.Text);

                    jobj.Add("BOARD", "");
                    jobj.Add("BOOKSIZE", "");
                    jobj.Add("PAGE", "");

                    jobj.Add("BOARD_CH_DATE", "");
                    jobj.Add("PRODUCT_NM", teProductNm.Text);
                    jobj.Add("PRODUCT_CD", teProductCd.Text);


                    for (int i = 1; i < 7; i++)
                    {
                        int purchCd = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PURCHCD"]);
                        if (purchCd > 0)
                        {
                            JObject jPurchData = new JObject();

                            int priority = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PRIORITY"]);
                            var jPurchaseDetail = new JArray();

                            foreach (DataRow row in _dicPurchaseInfoTable[i].Rows)
                            {
                                JObject jPurchDetail = new JObject();

                                int ratio = ConvertUtil.ToInt32(row["PURCHASE_RATIO"]);

                                if (ratio > 0)
                                {
                                    jPurchDetail.Add("SHOPCD", 1);
                                    jPurchDetail.Add("BOOKCD", bookCd);
                                    jPurchDetail.Add("PURCHCD", purchCd);
                                    jPurchDetail.Add("RATE_KBN", ConvertUtil.ToInt32(row["ID"]));
                                    jPurchDetail.Add("RATE", ConvertUtil.ToInt32(row["PURCHASE_RATIO"]));
                                    jPurchDetail.Add("COST", ConvertUtil.ToInt32(row["PRICE"]));
                                    jPurchDetail.Add("STOCK", ConvertUtil.ToInt32(row["STOCK"]));

                                    jPurchaseDetail.Add(jPurchDetail);
                                }
                            }

                            jPurchData.Add("SHOPCD", 1);
                            jPurchData.Add("BOOKCD", bookCd);
                            jPurchData.Add("PURCHCD", purchCd);
                            jPurchData.Add("PUR_PROCESS", priority);
                            jPurchData.Add($"PURCH_DETAIL", jPurchaseDetail);

                            jobj.Add($"PURCH_{i}_DATA", jPurchData);
                        }
                    }

                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        // clear();
                        buttonHandler(false, true, true);
                        Dangol.Info("등록되었습니다.");
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
            }
        }

            

        private void teBookCd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //TextEdit textEditor = (TextEdit)sender;
                //string data = textEditor.Text;
                searchBook();
            }
        }

        public void searchBook()
        {
            string data = teBookCd.Text.Trim();
            string strNum = Regex.Replace(data, @"\D", "");

            if (string.IsNullOrWhiteSpace(strNum))
            {
                Dangol.Warining("도서코드를 입력하세요");
                teBookCd.Text = "";
            }
            else
            {
                long bookCd = ConvertUtil.ToInt64(strNum);
                getBookInfo(bookCd);
                
            }
        }


        private void setOherCode(int shpopCd, int deptCd, int groupCd, int standCd, bool isDept = true, bool isGroup = true, bool isStand = true)
        {
            JObject jResult = new JObject();
            string query = "";
            if (isDept)
            {
                query = $"SELECT ORGAN_NM FROM HMA10 WHERE DEPTCD = {deptCd} AND GROUPCD = 99";

                if (DBConnect.getRow(query, ref jResult))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        teDeptCd.Text = ConvertUtil.ToString(deptCd);
                        teDeptNm.Text = ConvertUtil.ToString(jResult["ORGAN_NM"]);
                    }
                }
            }

            if (isGroup)
            {
                query = $"SELECT ORGAN_NM FROM HMA10 WHERE GROUPCD = {groupCd} AND GROUPCD != 99 AND STANDCD = 9999";


                if (DBConnect.getRow(query, ref jResult))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        teGroupCd.Text = ConvertUtil.ToString(groupCd);
                        teGroupNm.Text = ConvertUtil.ToString(jResult["ORGAN_NM"]);
                    }
                }
            }

            if (isStand)
            {
                query = $"SELECT SHOPCD, DEPTCD, GROUPCD, STANDCD, ORGAN_NM FROM HMA10 WHERE ORGAN_FG != 2 AND STANDCD = {standCd}";

                if (DBConnect.getRow(query, ref jResult))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        teStandNm.Text = ConvertUtil.ToString(jResult["ORGAN_NM"]);
                        teStandCd.Text = ConvertUtil.ToString(jResult["STANDCD"]);
                    }
                }
            }
        }

        private void teISBNFG_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            string data = teISBNFG.Text.Trim();
            string strData = Regex.Replace(data, @"\D", "");
            int value = ConvertUtil.ToInt32(data);

            if (value < 1 || value > 3)
                leISBNFG.EditValue = -1;
            else
                leISBNFG.EditValue = value;
        }

        private void teStandNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string standNm = teStandNm.Text.Trim();

                using (dlgCodeList codeList = new dlgCodeList("서가정보", (int)common.Enum.CODE_TYPE.STAND, standNm))
                {
                    //sellerList.StartPosition = FormStartPosition.Manual;
                    //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                    if (codeList.ShowDialog(this) == DialogResult.OK)
                    {
                        teStandNm.Text = ConvertUtil.ToString(codeList._drv["CODE_NM"]);
                        teStandCd.Text = ConvertUtil.ToString(codeList._drv["CODE_CD"]);
                        int standCd = ConvertUtil.ToInt32(teStandCd.Text);
                        if (standCd >= 2000 && standCd < 4000)
                            teTax.Text = "1";
                        else
                            teTax.Text = "2";

                        setOherCode(ConvertUtil.ToInt32(codeList._drv["SHOPCD"]), ConvertUtil.ToInt32(codeList._drv["DEPTCD"]), ConvertUtil.ToInt32(codeList._drv["GROUPCD"]), ConvertUtil.ToInt32(codeList._drv["STANDCD"]), true, true, false);

                        teProductNm.Text = "";
                        teProductCd.Text = "";
                        teProductNm.Focus();


                    }
                }
            }
        }

        private void teBookNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teStandNm.Focus();
        }

        private void teProductNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string standCd = teStandCd.Text.Trim();

                if (string.IsNullOrEmpty(standCd))
                {
                    Dangol.Warining("서가정보를 입력해주세요.");
                }
                else
                {
                    string productNm = teProductNm.Text.Trim();

                    using (dlgCodeList codeList = new dlgCodeList("서가정보", (int)common.Enum.CODE_TYPE.PRODUCT, productNm))
                    {
                        //sellerList.StartPosition = FormStartPosition.Manual;
                        //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                        //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                        codeList._standCd = ConvertUtil.ToInt32(standCd);

                        if (codeList.ShowDialog(this) == DialogResult.OK)
                        {
                            teProductNm.Text = ConvertUtil.ToString(codeList._drv["CODE_NM"]);
                            teProductCd.Text = ConvertUtil.ToString(codeList._drv["CODE_CD"]);
                            tePublisher.Focus();
                        }
                    }
                }
            }
        }

        private void tePublisher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int shopCd = 1;
                string publisher = tePublisher.Text.Trim();

                using (dlgPublisherList publisherList = new dlgPublisherList(publisher, shopCd))
                {
                    //sellerList.StartPosition = FormStartPosition.Manual;
                    //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                    if (publisherList.ShowDialog(this) == DialogResult.OK)
                    {
                        tePublisherCd.Text = ConvertUtil.ToString(publisherList._drv["PUBSHCD"]);
                        tePublisher.Text =  ConvertUtil.ToString(publisherList._drv["PUBSHNM"]);
                        sePrice.Focus();
                    }
                }
            }
        }

        private void teStandCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int standCd = ConvertUtil.ToInt32(teStandCd.Text.Trim());

                JObject jResult = new JObject();
                string query = $"SELECT SHOPCD, DEPTCD, GROUPCD, STANDCD, ORGAN_NM FROM HMA10 WHERE ORGAN_FG != 2 AND STANDCD = {standCd}";
                

                if (DBConnect.getRow(query, ref jResult))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        teStandNm.Text = ConvertUtil.ToString(jResult["ORGAN_NM"]);
                        teStandCd.Text = ConvertUtil.ToString(jResult["STANDCD"]);
                        if (standCd >= 2000 && standCd < 4000)
                            teTax.Text = "1";
                        else
                            teTax.Text = "2";

                        setOherCode(ConvertUtil.ToInt32(jResult["SHOPCD"]), ConvertUtil.ToInt32(jResult["DEPTCD"]), ConvertUtil.ToInt32(jResult["GROUPCD"]), ConvertUtil.ToInt32(jResult["STANDCD"]), true, true, false);

                        teProductNm.Text = "";
                        teProductCd.Text = "";
                        teProductNm.Focus();
                    }
                    else
                    {
                        Dangol.Warining("서가정보가 없습니다.");
                    }
                }
                else
                    Dangol.Warining("서가정보가 없습니다.");
            }
        }

        private void teProductCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string standCd = teStandCd.Text.Trim();

                if (string.IsNullOrEmpty(standCd))
                {
                    Dangol.Warining("서가정보를 입력해주세요.");
                }
                else
                {
                    JObject jResult = new JObject();
                    string query = $"SELECT NVL(PRODUCT_CD, '') AS CODE_CD, NVL(ORGAN_NAME, ' ') AS CODE_NM FROM HMA28 WHERE STANDCD = {standCd} AND PRODUCT_CD = {teProductCd.Text.Trim()} ";

                    if (DBConnect.getRow(query, ref jResult))
                    {
                        if (Convert.ToBoolean(jResult["EXIST"]))
                        {
                            teProductNm.Text = ConvertUtil.ToString(jResult["CODE_NM"]);
                            teProductCd.Text = ConvertUtil.ToString(jResult["CODE_CD"]);
                            tePublisher.Focus();
                        }
                        else
                        {
                            Dangol.Warining("소소분류정보가 없습니다.");
                        }
                    }
                    else
                        Dangol.Warining("소소분류정보가 없습니다.");
                }
            }
        }

        private void tePublisherCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int publisherCd = ConvertUtil.ToInt32(tePublisherCd.Text.Trim());

                JObject jResult = new JObject();
                string query = $"SELECT PUBSHCD, PUBSHNM FROM HMA11 WHERE PUBSHCD = {publisherCd}";

                if (DBConnect.getRow(query, ref jResult))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        tePublisher.Text = ConvertUtil.ToString(jResult["PUBSHNM"]);
                        tePublisherCd.Text = ConvertUtil.ToString(jResult["PUBSHCD"]);
                        sePrice.Focus();
                    }
                    else
                    {
                        Dangol.Warining("출판사 정보가 없습니다.");
                    }
                }
                else
                    Dangol.Warining("출판사 정보가 없습니다.");
            }
        }

        private void sePrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teAuthor1.Focus();
        }

        private void teAuthor1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teAuthor2.Focus();
        }

        private void teAuthor2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teEditor.Focus();
        }
    }
}
