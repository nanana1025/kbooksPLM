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
    public partial class usrReturnMaintananceDetail : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;

        long _bookCd;
        int _shopCd;

        long _purchCd;
        string _purcdNm;
        string _defaultReturnNm;

        bool _isExist;
        int _code;
        XtraTabPage[] _arrXtraTabPage;
        //usrPurchaseInfo[] _arrUsrPurchaseInfo;

        public delegate void ButtonHandler(bool search, bool update, bool delete);
        public event ButtonHandler buttonHandler;

        public usrReturnMaintananceDetail()
        {
            InitializeComponent();

            _searchType = -1;
            _bookCd = -1;
            _shopCd = 1;
            _purchCd = -1;
            _code = -1;
            _purcdNm = "";
            _defaultReturnNm = "날개";

            _isExist = false;

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
            //List<string> listLongCol = new List<string>(new string[] { "SHOPCD" });
            //DataTable dtShopCd = Util.getTable("HMA09", "SHOPCD,SHOP_NM ", "SHOPCD > 0", "SHOPCD ASC", listLongCol);

            //DataTable dtPurchaseType = new DataTable();

            //dtPurchaseType.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtPurchaseType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            //Util.insertRow(dtPurchaseType, 1, "1: 국내출판(직거래)");
            //Util.insertRow(dtPurchaseType, 2, "2: 국내총판");
            //Util.insertRow(dtPurchaseType, 3, "3: 문구");
            //Util.insertRow(dtPurchaseType, 4, "4: 국내개인");
            //Util.insertRow(dtPurchaseType, 5, "5: 국내해외총판");
            //Util.insertRow(dtPurchaseType, 6, "6: 신문총판");
            //Util.insertRow(dtPurchaseType, 99, "99: 기타");
            //Util.insertRow(dtPurchaseType, -1, "");

            //Util.LookupEditHelper(lePurchaseType, dtPurchaseType, "KEY", "VALUE");
        }

        private void setinitialize()
        {
            //setXtratabinitialize();

            //foreach(usrPurchaseInfo control in _arrUsrPurchaseInfo)
            //    control.setInitLoad();

            //tePurchase.ReadOnly = !isEditable;
        }

        public void setFocus()
        {
            tePurchNm.Focus();
        }

        public void clear(bool isAll = false)
        {
            if (isAll)
            {
                _purchCd = -1;
                _purcdNm = "";

                tePurchNm.Text = "";
                tePurchCd.Text = "";
            }
            teReturnNm.Text = "";
            seStdRate.EditValue = 0;
            deStartDt.EditValue = DBNull.Value;
            deStopDt.EditValue = DBNull.Value;
           

            seFaxFg.EditValue = 0;
            seOnlineFg.EditValue = 0;
            teAccount.Text = "";
            teBankNm.Text = "";
            teOwnerNm.Text = "";
            teSaleNm.Text = "";
            seDepositPrice.EditValue = 0;

            seLimitKbn.EditValue = 0;
            seLimitPrice.EditValue = 0;

            deLastDt.EditValue = DBNull.Value;
            //tePurchaseCd.Focus();

            if (isAll)
            {
                tePurchNm.Focus();
            }

        }


        public void getReturnInfo(int purchCd)
        {
            _purchCd = purchCd;

            clear();

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/regist/getReturnInfo.json";

            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("PURCHCD", purchCd);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        string returnNm = ConvertUtil.ToString(jResult["RET_NM"]);
                        if (string.IsNullOrEmpty(returnNm))
                            teReturnNm.Text = _defaultReturnNm;
                        else
                            teReturnNm.Text = returnNm;

                       seStdRate.EditValue = ConvertUtil.ToInt32(jResult["STD_RATE"]);

                        deStartDt.EditValue = ConvertUtil.ToDateTimeNull(jResult["START_DAY"]);
                        deStopDt.EditValue = ConvertUtil.ToDateTimeNull(jResult["STOP_DAY"]);

                        seFaxFg.EditValue = ConvertUtil.ToInt32(jResult["FAX_FG"]);
                        seOnlineFg.EditValue = ConvertUtil.ToInt32(jResult["ONLINE_FG"]);
                        teBankNm.Text = ConvertUtil.ToString(jResult["BANK_NM"]);
                        teAccount.Text = ConvertUtil.ToString(jResult["ONLINE_NO"]);
                        teOwnerNm.Text = ConvertUtil.ToString(jResult["RPNM"]);
                        teSaleNm.Text = ConvertUtil.ToString(jResult["TRADE_NM"]);
                        seDepositPrice.EditValue = ConvertUtil.ToInt32(jResult["PURCH_BOND"]);

                        seLimitKbn.EditValue = ConvertUtil.ToInt32(jResult["LIMIT_KBN"]);
                        seLimitPrice.EditValue = ConvertUtil.ToInt32(jResult["LIMIT_AMOUNT"]);

                        deLastDt.EditValue = ConvertUtil.ToDateTimeNull(jResult["LAST_RET_DATE"]);

                        buttonHandler(false, true, true);
                    }
                }
                else
                {
                    teReturnNm.Text = _defaultReturnNm;
                    deStartDt.EditValue = DateTime.Now;
                    seFaxFg.Text = "2";
                    seOnlineFg.Text = "2";

                    buttonHandler(true, false, false);
                }
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
            }
        }


        private bool check()
        {
            if (_purchCd < 0)
            {
                Dangol.Warining("매입처 코드를 확인해주세요.");
                return false;
            }

            if (string.IsNullOrEmpty(teReturnNm.Text.Trim()))
            {
                Dangol.Warining("반품처 정보가 없습니다.");
                return false;
            }

            //int deptCd = ConvertUtil.ToInt32(teDeptCd.Text);
            //if (deptCd < 1)
            //{
            //    Dangol.Warining("부 정보가 없습니다.");
            //    return false;
            //}

            //int groupCd = ConvertUtil.ToInt32(teGroupCd.Text);
            //if (groupCd < 1)
            //{
            //    Dangol.Warining("조 정보가 없습니다.");
            //    return false;
            //}

            //int standCd = ConvertUtil.ToInt32(teStandCd.Text);
            //if (standCd < 1)
            //{
            //    Dangol.Warining("서가 정보가 없습니다.");
            //    return false;
            //}

            //long pubshCd = ConvertUtil.ToInt64(tePublisherCd.Text);

            //if (pubshCd < 1)
            //{
            //    Dangol.Warining("출판사 정보가 없습니다.");
            //    return false;
            //}

            //bool isPurchaseExist = false;
            //for (int i = 1; i < 7; i++)
            //{
            //    int purchCd = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PURCHCD"]);
            //    if (purchCd > 0)
            //    {
            //        isPurchaseExist = true;

            //        int priority = ConvertUtil.ToInt32(_dicPurchaseInfo[i]["PRIORITY"]);

            //        if (priority < 1)
            //        {
            //            Dangol.Warining("매입처 처리순위를 확인하세요.");
            //            return false;
            //        }

            //        bool isinfoExist = false;
            //        foreach (DataRow row in _dicPurchaseInfoTable[i].Rows)
            //        {
            //            int ratio = ConvertUtil.ToInt32(row["PURCHASE_RATIO"]);

            //            if (ratio > 0)
            //                isinfoExist = true;
            //        }


            //        if (!isinfoExist)
            //        {
            //            Dangol.Warining("매입 정보를 확인하세요.");
            //            return false;
            //        }

            //    }
            //}

            //if (!isPurchaseExist)
            //{
            //    Dangol.Warining("매입처가 없습니다.");
            //    return false;
            //}


            return true;

        }

        public void DeleteReturnInfo()
        {
            if (Dangol.MessageYN("반품처 정보를 삭제하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/deleteReturnInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PURCHCD", _purchCd);

                    //jobj.Add("RET_NM", "");
                    //jobj.Add("STD_RATE", 0);

                    //jobj.Add("START_DAY", "");
                    //jobj.Add("STOP_DAY", "");
                    
                    //jobj.Add("FAX_FG", 0);
                    //jobj.Add("ONLINE_FG", 0);
                    //jobj.Add("BANK_NM", "");
                    //jobj.Add("ONLINE_NO", "");
                    //jobj.Add("RPNM", "");
                    //jobj.Add("TRADE_NM", "");
                    //jobj.Add("PURCH_BOND", 0);

                    //jobj.Add("LIMIT_KBN", 0);
                    //jobj.Add("LIMIT_AMOUNT", 0);


                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        clear();
                        //tePurchNm.Text = _purcdNm;
                        //tePurchCd.Text = _purchCd.ToString();
                        teReturnNm.Focus();
                        deStartDt.EditValue = DateTime.Now;
                        seFaxFg.Text = "2";
                        seOnlineFg.Text = "2";
                        buttonHandler(false, true, false);
                        Dangol.Info("삭제되었습니다.");
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
            }
        }

        public void UpdateReturnInfo()
        {
            if (Dangol.MessageYN("반품처 정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/updateReturnInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PURCHCD", _purchCd);

                    jobj.Add("RET_NM", ConvertUtil.ToString(teReturnNm.Text));

                    jobj.Add("STD_RATE", ConvertUtil.ToInt32(seStdRate.EditValue));


                    if (deStartDt.EditValue != null && !string.IsNullOrEmpty(deStartDt.EditValue.ToString()))
                    {
                        string dtDate = "";
                        dtDate = $"{deStartDt.Text} 00:00:00";
                        DateTime dtConstDay = Convert.ToDateTime(dtDate);
                        jobj.Add("START_DAY", dtConstDay.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        jobj.Add("START_DAY", "");
                    }


                    if (deStopDt.EditValue != null && !string.IsNullOrEmpty(deStopDt.EditValue.ToString()))
                    {
                        string dtDate = "";
                        dtDate = $"{deStopDt.Text} 00:00:00";
                        DateTime dtConstDay = Convert.ToDateTime(dtDate);
                        jobj.Add("STOP_DAY", dtConstDay.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        jobj.Add("STOP_DAY", "");
                    }

                    jobj.Add("FAX_FG", ConvertUtil.ToInt32(seFaxFg.EditValue));
                    jobj.Add("ONLINE_FG", ConvertUtil.ToInt32(seOnlineFg.EditValue));
                    jobj.Add("BANK_NM", ConvertUtil.ToString(teBankNm.Text));
                    jobj.Add("ONLINE_NO", ConvertUtil.ToString(teAccount.Text));
                    jobj.Add("RPNM", ConvertUtil.ToString(teOwnerNm.Text));
                    jobj.Add("TRADE_NM", ConvertUtil.ToString(teSaleNm.Text));
                    jobj.Add("PURCH_BOND", ConvertUtil.ToInt32(seDepositPrice.EditValue));

                    jobj.Add("LIMIT_KBN", ConvertUtil.ToInt32(seLimitKbn.EditValue));
                    jobj.Add("LIMIT_AMOUNT", ConvertUtil.ToInt32(seLimitPrice.EditValue));

                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        buttonHandler(false, true, true);
                        Dangol.Info("수정되었습니다.");
                    }
                    else
                    {
                        Dangol.Error(jResult["MSG"]);
                    }
                }
            }
        }

        public void InsertReturnInfo()
        {
            if (Dangol.MessageYN("반품처 정보를 추가하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/insertReturnInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PURCHCD", _purchCd);
                    jobj.Add("PURCHNM", _purcdNm);

                    jobj.Add("RET_NM", ConvertUtil.ToString(teReturnNm.Text));

                    jobj.Add("STD_RATE", ConvertUtil.ToInt32(seStdRate.EditValue));

                    if (deStartDt.EditValue != null && !string.IsNullOrEmpty(deStartDt.EditValue.ToString()))
                    {
                        string dtDate = "";
                        dtDate = $"{deStartDt.Text} 00:00:00";
                        DateTime dtConstDay = Convert.ToDateTime(dtDate);
                        jobj.Add("START_DAY", dtConstDay.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        jobj.Add("START_DAY", "");
                    }

                    if (deStopDt.EditValue != null && !string.IsNullOrEmpty(deStopDt.EditValue.ToString()))
                    {
                        string dtDate = "";
                        dtDate = $"{deStopDt.Text} 00:00:00";
                        DateTime dtConstDay = Convert.ToDateTime(dtDate);
                        jobj.Add("STOP_DAY", dtConstDay.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        jobj.Add("STOP_DAY", "");
                    }

                    jobj.Add("FAX_FG", ConvertUtil.ToInt32(seFaxFg.EditValue));
                    jobj.Add("ONLINE_FG", ConvertUtil.ToInt32(seOnlineFg.EditValue));
                    jobj.Add("BANK_NM", ConvertUtil.ToString(teBankNm.Text));
                    jobj.Add("ONLINE_NO", ConvertUtil.ToString(teAccount.Text));
                    jobj.Add("RPNM", ConvertUtil.ToString(teOwnerNm.Text));
                    jobj.Add("TRADE_NM", ConvertUtil.ToString(teSaleNm.Text));
                    jobj.Add("PURCH_BOND", ConvertUtil.ToInt32(seDepositPrice.EditValue));

                    jobj.Add("LIMIT_KBN", ConvertUtil.ToInt32(seLimitKbn.EditValue));
                    jobj.Add("LIMIT_AMOUNT", ConvertUtil.ToInt32(seLimitPrice.EditValue));

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



        private void teGroupCd_KeyDown(object sender, KeyEventArgs e)
        {
           //if (e.KeyCode == Keys.Enter)
            //{
            //    string data = teLimitKbn.Text.Trim();
            //    string strNum = Regex.Replace(data, @"\D", "");

            //    if (string.IsNullOrWhiteSpace(strNum))
            //    {
            //        Dangol.Warining("조 코드를 입력해주세요.");
            //    }
            //    else
            //    {
            //        JObject jResult = new JObject();
            //        string query = "";

            //        query = $"SELECT ORGAN_NM FROM HMA10 WHERE GROUPCD = {strNum} AND GROUPCD != 99 AND STANDCD = 9999";

            //        if (DBConnect.getRow(query, ref jResult))
            //        {
            //            if (Convert.ToBoolean(jResult["EXIST"]))
            //            {
            //                teLimitKbn.Text = ConvertUtil.ToString(strNum);
            //                teGroupNm.Text = ConvertUtil.ToString(jResult["ORGAN_NM"]);

            //                if(!_isExist && _purchCd == ConvertUtil.ToInt64(tePurchaseCd.Text) && _code == 3)
            //                {
            //                    buttonHandler(true, false, false);
            //                }
            //            }
            //            else
            //            {
            //                Dangol.Warining("조코드 정보가 없습니다.");
            //            }
            //        }
            //    }
            //}
        }

       

        private void tePurchNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string seller = tePurchNm.Text.Trim();

                using (dlgPurchaseList sellerList = new dlgPurchaseList(seller))
                {
                    //sellerList.StartPosition = FormStartPosition.Manual;
                    //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                    if (sellerList.ShowDialog(this) == DialogResult.OK)
                    {
                        tePurchNm.Text = ConvertUtil.ToString(sellerList._drv["PURCHNM"]);
                        tePurchCd.Text = ConvertUtil.ToString(sellerList._drv["PURCHCD"]);
                        
                        getReturnInfo(ConvertUtil.ToInt32(sellerList._drv["PURCHCD"]));
                        seStdRate.Focus();
                    }
                }
            }
        }

        private void tePurchCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int purchCd = ConvertUtil.ToInt32(tePurchCd.Text.Trim());

                JObject jResult = new JObject();
                string query = $"SELECT PURCHCD, PURCHNM FROM HMA02 WHERE PURCHCD = {purchCd}";

                if (DBConnect.getRow(query, ref jResult))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        _purcdNm = tePurchNm.Text = ConvertUtil.ToString(jResult["PURCHNM"]);
                        tePurchNm.Text = ConvertUtil.ToString(jResult["PURCHNM"]);
                        tePurchCd.Text = ConvertUtil.ToString(jResult["PURCHCD"]);
                        getReturnInfo(ConvertUtil.ToInt32(jResult["PURCHCD"]));
                        seStdRate.Focus();
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

        private void sbGetReturn_Click(object sender, EventArgs e)
        {
            teReturnNm.Text = _defaultReturnNm;
            seStdRate.Focus();
        }

        private void teReturnNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                seStdRate.Focus();
        }

        private void seStdRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                seFaxFg.Focus();
        }

        private void seFaxFg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                seOnlineFg.Focus();
        }

        private void seOnlineFg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teBankNm.Focus();
        }

        private void teBankNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teAccount.Focus();
        }

        private void teAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teOwnerNm.Focus();
        }

        private void teOwnerNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                teSaleNm.Focus();
        }

        private void teSaleNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                seDepositPrice.Focus();
        }

        private void seDepositPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                seLimitKbn.Focus();
        }

        private void seLimitKbn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                seLimitPrice.Focus();
        }

        
    }
}
