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
    public partial class usrPurchaseMaintananceDetail : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;

        long _bookCd;
        int _shopCd;

        long _purchCd;

        bool _isExist;
        int _code;
        XtraTabPage[] _arrXtraTabPage;
        //usrPurchaseInfo[] _arrUsrPurchaseInfo;

        public delegate void ButtonHandler(bool search, bool update, bool delete);
        public event ButtonHandler buttonHandler;

        public usrPurchaseMaintananceDetail()
        {
            InitializeComponent();

            _searchType = -1;
            _bookCd = -1;
            _shopCd = 1;
            _purchCd = -1;
            _code = -1;

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

            DataTable dtPurchaseType = new DataTable();

            dtPurchaseType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtPurchaseType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRow(dtPurchaseType, 1, "1: 국내출판(직거래)");
            Util.insertRow(dtPurchaseType, 2, "2: 국내총판");
            Util.insertRow(dtPurchaseType, 3, "3: 문구");
            Util.insertRow(dtPurchaseType, 4, "4: 국내개인");
            Util.insertRow(dtPurchaseType, 5, "5: 국내해외총판");
            Util.insertRow(dtPurchaseType, 6, "6: 신문총판");
            Util.insertRow(dtPurchaseType, 99, "99: 기타");
            Util.insertRow(dtPurchaseType, -1, "");

            Util.LookupEditHelper(lePurchaseType, dtPurchaseType, "KEY", "VALUE");
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
            tePurchase.Focus();
        }

        public void clear()
        {
            _purchCd = -1;
            tePurchaseCd.Text = "";
            lePurchaseType.EditValue = -1;
            tePurchase.Text = "";
            teRepNm.Text = "";
            teSpecialNm.Text = "";
            teEntLocal.Text = "";
            teEntType.Text = "";
            teEntSeq.Text = "";
            teBirthDay.Text = "";
            tePostCd.Text = "";
            teAddress.Text = "";


            teTel11.Text = "";
            teTel12.Text = "";
            teTel13.Text = "";

            teTel21.Text = "";
            teTel22.Text = "";
            teTel23.Text = "";

            teFax1.Text = "";
            teFax2.Text = "";
            teFax3.Text = "";

            teEtc1.Text = "";
            teEtc2.Text = "";

            tePayGrade.Text = "";

            teGroupCd.Text = "";
            tePayDay.Text = "";

            teBusinessItem.Text = "";
            teBusinessType.Text = "";

            deConstDay.EditValue = DBNull.Value;

            //tePurchaseCd.Focus();

        }

        private void tePurchaseCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string data = tePurchaseCd.Text.Trim();
                string strNum = Regex.Replace(data, @"\D", "");

                if (string.IsNullOrWhiteSpace(strNum))
                {
                    Dangol.Warining("매입처 코드를 입력해주세요.");
                }
                else
                {
                    long purchaseCd = ConvertUtil.ToInt32(strNum);

                    _code = getPurchaseInfo(purchaseCd);
                    if (_code == 1)
                    {
                        _purchCd = purchaseCd;
                        _isExist = true;
                        buttonHandler(false, true, true);
                    }
                    else if(_code == 2)
                    {
                        _purchCd = purchaseCd;
                        _isExist = false;
                        buttonHandler(false, false, false);
                    }
                    else if(_code == 3)
                    {
                        tePurchaseCd.Text = ConvertUtil.ToString(purchaseCd);
                        _purchCd = purchaseCd;
                        _isExist = false;
                        buttonHandler(false, false, false);
                    }
                    else
                    {
                        _isExist = false;
                        buttonHandler(false, false, false);
                    }
                }
            }
        }

        public int getPurchaseInfo(long purchaseCd)
        {
            clear();

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/regist/getPurchaseInfo.json";

            jobj.Add("PURCHCD", purchaseCd);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    if (Convert.ToBoolean(jResult["PURCH_EXIST"]))
                    {
                        tePurchaseCd.Text = ConvertUtil.ToString(jResult["PURCHCD"]);
                        lePurchaseType.EditValue = ConvertUtil.ToInt32(jResult["PURCH_KBN"]);

                        tePurchase.Text = ConvertUtil.ToString(jResult["PURCHNM"]);
                        teSpecialNm.Text = ConvertUtil.ToString(jResult["SPECIALNM"]);

                        teRepNm.Text = ConvertUtil.ToString(jResult["REPNM"]);

                        teEntLocal.Text = ConvertUtil.ToString(jResult["ENT_LOCAL"]);
                        teEntType.Text = ConvertUtil.ToString(jResult["ENT_TYPE"]);
                        teEntSeq.Text = ConvertUtil.ToString(jResult["ENT_SEQNO"]);

                        teBirthDay.Text = ConvertUtil.ToString(jResult["JID_BIRTHDAY"]);

                        string postNo1 = ConvertUtil.ToString(jResult["POST_NO1"]);
                        string postNo2 = ConvertUtil.ToString(jResult["POST_NO2"]);
                        if (postNo1.Length == 3)
                            tePostCd.Text = $"{postNo1}-{postNo2}";
                        else
                            tePostCd.Text = postNo1+ postNo2;

                        teAddress.Text = ConvertUtil.ToString(jResult["ADDRESS"]);


                        teTel11.Text = ConvertUtil.ToString(jResult["TEL_LOCAL1"]);
                        teTel12.Text = ConvertUtil.ToString(jResult["TEL_DEPT1"]);
                        teTel13.Text = ConvertUtil.ToString(jResult["TEL_NO1"]);

                        teTel21.Text = ConvertUtil.ToString(jResult["TEL_LOCAL2"]);
                        teTel22.Text = ConvertUtil.ToString(jResult["TEL_DEPT2"]);
                        teTel23.Text = ConvertUtil.ToString(jResult["TEL_NO2"]);

                        teFax1.Text = ConvertUtil.ToString(jResult["FAX_LOCAL"]);
                        teFax2.Text = ConvertUtil.ToString(jResult["FAX_DEPT"]);
                        teFax3.Text = ConvertUtil.ToString(jResult["FAX_NO"]);

                        teEtc1.Text = ConvertUtil.ToString(jResult["BOOK_CONST_NM1"]);
                        teEtc2.Text = ConvertUtil.ToString(jResult["BOOK_CONST_NM2"]);

                        tePayGrade.Text = ConvertUtil.ToString(jResult["PAY_GRADE"]);

                        teGroupCd.Text = ConvertUtil.ToString(jResult["ORD_GROUPCD"]);
                        tePayDay.Text = ConvertUtil.ToString(jResult["PAY_DATE"]);

                        teBusinessItem.Text = ConvertUtil.ToString(jResult["BUSINESS_ITEM"]);
                        teBusinessType.Text = ConvertUtil.ToString(jResult["BUSINESS_TYPE"]);

                        deConstDay.EditValue = ConvertUtil.ToDateTimeNull(jResult["BOOK_CONST_DAY"]);

                        return 1;
                    }
                    else
                    {
                        tePurchaseCd.Text = ConvertUtil.ToString(jResult["PUBSHCD"]);
                        teSpecialNm.Text = ConvertUtil.ToString(jResult["SPECIALNM"]);
                        lePurchaseType.EditValue = ConvertUtil.ToInt32(jResult["PUBSH_KBN"]);
                        tePurchase.Text = ConvertUtil.ToString(jResult["PUBSHNM"]);
                        teRepNm.Text = ConvertUtil.ToString(jResult["REPNM"]);
                        string postNo1 = ConvertUtil.ToString(jResult["POST_NO1"]);
                        string postNo2 = ConvertUtil.ToString(jResult["POST_NO2"]);
                        if (postNo1.Length == 3)
                            tePostCd.Text = $"{postNo1}-{postNo2}";
                        else
                            tePostCd.Text = postNo1;

                        teAddress.Text = ConvertUtil.ToString(jResult["ADDRESS"]);
                        teTel11.Text = ConvertUtil.ToString(jResult["TEL_LOCAL1"]);
                        teTel12.Text = ConvertUtil.ToString(jResult["TEL_DEPT1"]);
                        teTel13.Text = ConvertUtil.ToString(jResult["TEL_NO1"]);

                        teTel21.Text = ConvertUtil.ToString(jResult["TEL_LOCAL2"]);
                        teTel22.Text = ConvertUtil.ToString(jResult["TEL_DEPT2"]);
                        teTel23.Text = ConvertUtil.ToString(jResult["TEL_NO2"]);

                        teFax1.Text = ConvertUtil.ToString(jResult["FAX_LOCAL"]);
                        teFax2.Text = ConvertUtil.ToString(jResult["FAX_DEPT"]);
                        teFax3.Text = ConvertUtil.ToString(jResult["FAX_NO"]);


                        //lePurchaseType.EditValue = ConvertUtil.ToInt32(jResult["PURCH_KBN"]);
                        //teEntLocal.Text = ConvertUtil.ToString(jResult["ENT_LOCAL"]);
                        //teEntType.Text = ConvertUtil.ToString(jResult["ENT_TYPE"]);
                        //teEntSeq.Text = ConvertUtil.ToString(jResult["ENT_SEQNO"]);

                        //teBirthDay.Text = ConvertUtil.ToString(jResult["JID_BIRTHDAY"]);
                        //teEtc1.Text = ConvertUtil.ToString(jResult["BOOK_CONST_NM1"]);
                        //teEtc2.Text = ConvertUtil.ToString(jResult["BOOK_CONST_NM2"]);


                        //tePayGrade.Text = ConvertUtil.ToString(jResult["PAY_GRADE"]);

                        //teGroupCd.Text = ConvertUtil.ToString(jResult["ORD_GROUPCD"]);
                        //tePayDay.Text = ConvertUtil.ToString(jResult["PAY_DATE"]);

                        //teBusinessItem.Text = ConvertUtil.ToString(jResult["BUSINESS_ITEM"]);
                        //teBusinessType.Text = ConvertUtil.ToString(jResult["BUSINESS_TYPE"]);

                        //deConstDay.EditValue = ConvertUtil.ToDateTimeNull(jResult["BOOK_CONST_DAY"]);

                        return 2;
                    }
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                Dangol.Error(jResult["MSG"]);
                return -1;
            }
        }


        private bool check()
        {
            string data = tePurchaseCd.Text.Trim();
            string strNum = Regex.Replace(data, @"\D", "");

            if (string.IsNullOrWhiteSpace(strNum))
            {
                Dangol.Warining("매입처 코드를 입력해주세요.");
            }

            if(_purchCd != ConvertUtil.ToInt64(strNum))
            {
                Dangol.Warining("매입처 코드를 확인하세요.");
                return false;
            }

            //if (string.IsNullOrEmpty(teAuthor1.Text.Trim()) && string.IsNullOrEmpty(teAuthor2.Text.Trim()))
            //{
            //    Dangol.Warining("저자 정보가 없습니다.");
            //    return false;
            //}

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

        public void DeletePurchaseInfo()
        {
            if (Dangol.MessageYN("매입처 정보를 삭제하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    string url = "/regist/deletePurchaseInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PURCHCD", ConvertUtil.ToInt64(tePurchaseCd.Text.Trim()));

                    
                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        clear();
                        tePurchaseCd.Focus();
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

        public void UpdatePurchaseInfo()
        {
            if (Dangol.MessageYN("매입처 정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    int purchaseCd = ConvertUtil.ToInt32(tePurchaseCd.Text.Trim());

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/updatePurchaseInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PUBSHCD", purchaseCd);
                    jobj.Add("PURCHCD", purchaseCd);
                    jobj.Add("PUBSHNM", tePurchase.Text.Trim());
                    jobj.Add("PURCHNM", tePurchase.Text.Trim());
                    jobj.Add("PURCH_KBN", ConvertUtil.ToInt32(lePurchaseType.EditValue));
                    jobj.Add("PUBSH_KBN", ConvertUtil.ToInt32(lePurchaseType.EditValue));
                    jobj.Add("SPECIALNM", teSpecialNm.Text.Trim());

                    jobj.Add("UP_FLAG", 0);
                    jobj.Add("REPNM", teRepNm.Text.Trim());
                    jobj.Add("ENT_LOCAL", teEntLocal.Text.Trim());
                    jobj.Add("ENT_TYPE", teEntType.Text.Trim());
                    jobj.Add("ENT_SEQNO", teEntSeq.Text.Trim());
                    jobj.Add("JID_BIRTHDAY", teBirthDay.Text.Trim());

                    string postNo = tePostCd.Text.Trim();

                    if(postNo.Contains("-"))
                    {
                        string[] sArr = postNo.Split('-');

                        jobj.Add("POST_NO1", sArr[0]);
                        jobj.Add("POST_NO2", sArr[1]);
                    }
                    else
                    {
                        jobj.Add("POST_NO1", postNo.Substring(0, 2));
                        jobj.Add("POST_NO2", postNo.Substring(0, 3));
                    }

                    jobj.Add("ADDRESS", teAddress.Text.Trim());

                    jobj.Add("TEL_LOCAL1", teTel11.Text.Trim());
                    jobj.Add("TEL_DEPT1", teTel12.Text.Trim());
                    jobj.Add("TEL_NO1", teTel13.Text.Trim());
                    jobj.Add("TEL_LOCAL2", teTel21.Text.Trim());
                    jobj.Add("TEL_DEPT2", teTel22.Text.Trim());
                    jobj.Add("TEL_NO2", teTel23.Text.Trim());
                    jobj.Add("FAX_LOCAL", teFax1.Text.Trim());
                    jobj.Add("FAX_DEPT", teFax2.Text.Trim());
                    jobj.Add("FAX_NO", teFax3.Text.Trim());

                    jobj.Add("ORD_GROUPCD", teGroupCd.Text.Trim());
                    jobj.Add("PAY_DATE", tePayDay.Text.Trim());


                    string constDay = "";
                    if (deConstDay.EditValue != null && !string.IsNullOrEmpty(deConstDay.EditValue.ToString()))
                    {
                        string dtDate = "";
                        dtDate = $"{deConstDay.Text} 00:00:00";
                        DateTime dtConstDay= Convert.ToDateTime(dtDate);
                        constDay = dtConstDay.ToString("yyyyMMdd");
                    }

                    jobj.Add("BOOK_CONST_DAY", constDay);
                    jobj.Add("BOOK_CONST_NM1", teEtc1.Text.Trim());
                    jobj.Add("BOOK_CONST_NM2", teEtc2.Text.Trim());
                    jobj.Add("PAY_GRADE", tePayGrade.Text.Trim());
                    
                    jobj.Add("BUSINESS_ITEM", teBusinessItem.Text.Trim());
                    jobj.Add("BUSINESS_TYPE", teBusinessType.Text.Trim());


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

        public void InsertPurchaseInfo()
        {
            if (Dangol.MessageYN("매입처 정보를 추가하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    int purchaseCd = ConvertUtil.ToInt32(tePurchaseCd.Text.Trim());

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/insertPurchaseInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PUBSHCD", purchaseCd);
                    jobj.Add("PURCHCD", purchaseCd);
                    jobj.Add("PUBSHNM", tePurchase.Text.Trim());
                    jobj.Add("PURCHNM", tePurchase.Text.Trim());
                    jobj.Add("PURCH_KBN", ConvertUtil.ToInt32(lePurchaseType.EditValue));
                    jobj.Add("PUBSH_KBN", ConvertUtil.ToInt32(lePurchaseType.EditValue));

                    jobj.Add("SPECIALNM", teSpecialNm.Text.Trim());

                    jobj.Add("UP_FLAG", 0);
                    jobj.Add("REPNM", teRepNm.Text.Trim());
                    jobj.Add("ENT_LOCAL", teEntLocal.Text.Trim());
                    jobj.Add("ENT_TYPE", teEntType.Text.Trim());
                    jobj.Add("ENT_SEQNO", teEntSeq.Text.Trim());
                    jobj.Add("JID_BIRTHDAY", teBirthDay.Text.Trim());

                    string postNo = tePostCd.Text.Trim();

                    if (postNo.Contains("-"))
                    {
                        string[] sArr = postNo.Split('-');

                        jobj.Add("POST_NO1", sArr[0]);
                        jobj.Add("POST_NO2", sArr[1]);
                    }
                    else
                    {
                        jobj.Add("POST_NO1", postNo.Substring(0, 2));
                        jobj.Add("POST_NO2", postNo.Substring(0, 3));
                    }

                    jobj.Add("ADDRESS", teAddress.Text.Trim());

                    jobj.Add("TEL_LOCAL1", teTel11.Text.Trim());
                    jobj.Add("TEL_DEPT1", teTel12.Text.Trim());
                    jobj.Add("TEL_NO1", teTel13.Text.Trim());
                    jobj.Add("TEL_LOCAL2", teTel21.Text.Trim());
                    jobj.Add("TEL_DEPT2", teTel22.Text.Trim());
                    jobj.Add("TEL_NO2", teTel23.Text.Trim());
                    jobj.Add("FAX_LOCAL", teFax1.Text.Trim());
                    jobj.Add("FAX_DEPT", teFax2.Text.Trim());
                    jobj.Add("FAX_NO", teFax3.Text.Trim());

                    jobj.Add("ORD_GROUPCD", teGroupCd.Text.Trim());
                    jobj.Add("PAY_DATE", tePayDay.Text.Trim());


                    string constDay = "";
                    if (deConstDay.EditValue != null && !string.IsNullOrEmpty(deConstDay.EditValue.ToString()))
                    {
                        string dtDate = "";
                        dtDate = $"{deConstDay.Text} 00:00:00";
                        DateTime dtConstDay = Convert.ToDateTime(dtDate);
                        constDay = dtConstDay.ToString("yyyyMMdd");
                    }

                    jobj.Add("BOOK_CONST_DAY", constDay);
                    jobj.Add("BOOK_CONST_NM1", teEtc1.Text.Trim());
                    jobj.Add("BOOK_CONST_NM2", teEtc2.Text.Trim());
                    jobj.Add("PAY_GRADE", tePayGrade.Text.Trim());
                    jobj.Add("BUSINESS_ITEM", teBusinessItem.Text.Trim());
                    jobj.Add("BUSINESS_TYPE", teBusinessType.Text.Trim());

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
            if (e.KeyCode == Keys.Enter)
            {
                string data = teGroupCd.Text.Trim();
                string strNum = Regex.Replace(data, @"\D", "");

                if (string.IsNullOrWhiteSpace(strNum))
                {
                    Dangol.Warining("조 코드를 입력해주세요.");
                }
                else
                {
                    JObject jResult = new JObject();
                    string query = "";

                    query = $"SELECT ORGAN_NM FROM HMA10 WHERE GROUPCD = {strNum} AND GROUPCD != 99 AND STANDCD = 9999";

                    if (DBConnect.getRow(query, ref jResult))
                    {
                        if (Convert.ToBoolean(jResult["EXIST"]))
                        {
                            teGroupCd.Text = ConvertUtil.ToString(strNum);
                            teGroupNm.Text = ConvertUtil.ToString(jResult["ORGAN_NM"]);

                            if(!_isExist && _purchCd == ConvertUtil.ToInt64(tePurchaseCd.Text) && _code == 3)
                            {
                                buttonHandler(true, false, false);
                            }
                        }
                        else
                        {
                            Dangol.Warining("조코드 정보가 없습니다.");
                        }
                    }
                }
            }
        }

        private void teGroupNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string standNm = teGroupNm.Text.Trim();

                using (dlgCodeList codeList = new dlgCodeList("조정보", (int)common.Enum.CODE_TYPE.STAND, standNm))
                {
                    //sellerList.StartPosition = FormStartPosition.Manual;
                    //sellerList.Location = new Point(this.Location.X + (this.Size.Width / 2) - (sellerList.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (sellerList.Size.Height / 2));

                    if (codeList.ShowDialog(this) == DialogResult.OK)
                    {
                        teGroupNm.Text = ConvertUtil.ToString(codeList._drv["CODE_NM"]);
                        teGroupCd.Text = ConvertUtil.ToString(codeList._drv["CODE_CD"]);

                        if (!_isExist && _purchCd == ConvertUtil.ToInt64(tePurchaseCd.Text) && _code == 3)
                        {
                            buttonHandler(true, false, false);
                        }
                    }
                }
            }
        }

        private void tePurchaseCd_EditValueChanged(object sender, EventArgs e)
        {
            if (_purchCd != ConvertUtil.ToInt64(tePurchaseCd.Text))
            {
                buttonHandler(false, false, false);
            }
        }

        private void sbAddressSearch_Click(object sender, EventArgs e)
        {
            using (dlgGetAddress getAddress = new dlgGetAddress())
            {
                if (getAddress.ShowDialog(this) == DialogResult.OK)
                {
                    DataRow dr = (DataRow)getAddress.Tag;

                    tePostCd.Text = $"{dr["zipcode"]}";
                    teAddress.Text = $"{dr["ADDR1"]}";

                    teAddress.Focus();

                    // usrReceiptPart1.updateCnt(updatePartCnt.Cnt);

                }
            }
        }
    }
}
