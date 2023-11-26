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
    public partial class usrPublisherMaintananceDetail : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;

        long _bookCd;
        int _shopCd;

        int _publishCd;
        int _code;

        bool _isExist;

        XtraTabPage[] _arrXtraTabPage;

        public delegate void ButtonHandler(bool search, bool update, bool delete);
        public event ButtonHandler buttonHandler;

        public usrPublisherMaintananceDetail()
        {
            InitializeComponent();

            _searchType = -1;
            _bookCd = -1;
            _shopCd = 1;
            _publishCd = -1;
            _code = -1;
            _isExist = false;

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


            Util.LookupEditHelper(lePurblisherType, dtPurchaseType, "KEY", "VALUE");

        }

        private void setinitialize()
        {
      
        }

        public void setFocus()
        {
            tePublisherNm.Focus();
        }

        public void clear()
        {
            _publishCd = -1;
            tePublisherCd.Text = "";
            lePurblisherType.EditValue = -1;
            tePublisherNm.Text = "";
            teRepNm.Text = "";
          
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

            teBookTypeCnt.Text = "";

        }
        private void tePublisherCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string data = tePublisherCd.Text.Trim();
                string strNum = Regex.Replace(data, @"\D", "");

                if (string.IsNullOrWhiteSpace(strNum))
                {
                    Dangol.Warining("출판사 코드를 입력해주세요.");
                }
                else
                {
                    int publishCd = ConvertUtil.ToInt32(strNum);

                    _code = getPublishInfo(publishCd);
                    if (_code == 1)
                    {
                        _publishCd = publishCd;
                        _isExist = true;
                        buttonHandler(false, true, true);
                    }
                    else if (_code == 2)
                    {
                        tePublisherCd.Text = ConvertUtil.ToString(publishCd);
                        _publishCd = publishCd;
                        _isExist = false;
                        buttonHandler(false, false, false);
                    }
                    else if (_code == 3)
                    {
                        tePublisherCd.Text = ConvertUtil.ToString(publishCd);
                        _publishCd = publishCd;
                        _isExist = false;
                        buttonHandler(true, false, false);
                    }
                    else
                    {
                        _isExist = false;
                        buttonHandler(false, false, false);
                    }
                }
            }
        }

        public int getPublishInfo(int publishCd)
        {
            clear();

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string url = "/regist/getPublishInfo.json";

            jobj.Add("PUBSHCD", publishCd);

            if (DBConnect.getRequest(jobj, ref jResult, url))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    if (Convert.ToBoolean(jResult["PUBSH_EXIST"]))
                    {
                        tePublisherCd.Text = ConvertUtil.ToString(jResult["PUBSHCD"]);
                        lePurblisherType.EditValue = ConvertUtil.ToInt32(jResult["PUBSH_KBN"]);

                        tePublisherNm.Text = ConvertUtil.ToString(jResult["PUBSHNM"]);
                        teRepNm.Text = ConvertUtil.ToString(jResult["REPNM"]);

                        string postNo1 = ConvertUtil.ToString(jResult["POST_NO1"]);
                        string postNo2 = ConvertUtil.ToString(jResult["POST_NO2"]);
                        if (postNo1.Length == 3)
                            tePostCd.Text = $"{postNo1}-{postNo2}";
                        else
                            tePostCd.Text = postNo1 + postNo2;

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
                        teSpecialNm.Text = ConvertUtil.ToString(jResult["SPECIALNM"]);
                        teBookTypeCnt.Text = ConvertUtil.ToString(jResult["BOOK_CNT"]);



                        if (Convert.ToBoolean(jResult["PURCH_EXIST"]))
                        {
                            Dangol.Warining("매입처 정보가 존재합니다.");
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }

                        //return Convert.ToBoolean(jResult["PURCH_EXIST"])? 1 : 2;

                    }
                    else
                    {
                        Dangol.Warining("출판사코드가 매입처테이블에 존재합니다.\n매입처 테이블 유지보수업무를 사용하세요.");
                        //tePublisherCd.Text = ConvertUtil.ToString(jResult["PUBSHCD"]);
                        //lePurblisherType.EditValue = ConvertUtil.ToInt32(jResult["PURCH_KBN"]);

                        //tePublisherNm.Text = ConvertUtil.ToString(jResult["PURCHNM"]);
                        //teRepNm.Text = ConvertUtil.ToString(jResult["REPNM"]);

                        //string postNo1 = ConvertUtil.ToString(jResult["POST_NO1"]);
                        //string postNo2 = ConvertUtil.ToString(jResult["POST_NO2"]);
                        //if (postNo1.Length == 3)
                        //    tePostCd.Text = $"{postNo1}-{postNo2}";
                        //else
                        //    tePostCd.Text = postNo1;

                        //teAddress.Text = ConvertUtil.ToString(jResult["ADDRESS"]);
                        //teTel11.Text = ConvertUtil.ToString(jResult["TEL_LOCAL1"]);
                        //teTel12.Text = ConvertUtil.ToString(jResult["TEL_DEPT1"]);
                        //teTel13.Text = ConvertUtil.ToString(jResult["TEL_NO1"]);

                        //teTel21.Text = ConvertUtil.ToString(jResult["TEL_LOCAL2"]);
                        //teTel22.Text = ConvertUtil.ToString(jResult["TEL_DEPT2"]);
                        //teTel23.Text = ConvertUtil.ToString(jResult["TEL_NO2"]);

                        //teFax1.Text = ConvertUtil.ToString(jResult["FAX_LOCAL"]);
                        //teFax2.Text = ConvertUtil.ToString(jResult["FAX_DEPT"]);
                        //teFax3.Text = ConvertUtil.ToString(jResult["FAX_NO"]);

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
            string data = tePublisherCd.Text.Trim();
            string strNum = Regex.Replace(data, @"\D", "");

            if (string.IsNullOrWhiteSpace(strNum))
            {
                Dangol.Warining("출판사 코드를 입력해주세요.");
            }

            if (_publishCd != ConvertUtil.ToInt64(strNum))
            {
                Dangol.Warining("출판사 코드를 확인하세요.");
                return false;
            }


            return true;

        }

        public void DeletePublishInfo()
        {
            if (Dangol.MessageYN("출판사 정보를 삭제하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    string url = "/regist/deletePublishInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PUBSHCD", ConvertUtil.ToInt64(tePublisherCd.Text.Trim()));


                    if (DBConnect.getRequest(jobj, ref jResult, url))
                    {
                        clear();
                        tePublisherCd.Focus();
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

        public void UpdatePublishInfo()
        {
            if (Dangol.MessageYN("매입처 정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    int publishCd = ConvertUtil.ToInt32(tePublisherCd.Text.Trim());

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/updatePublishInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PUBSHCD", publishCd);
                    jobj.Add("PURCHCD", publishCd);
                    jobj.Add("PUBSHNM", tePublisherNm.Text.Trim());
                    jobj.Add("PURCHNM", tePublisherNm.Text.Trim());
                    jobj.Add("PURCH_KBN", ConvertUtil.ToInt32(lePurblisherType.EditValue));
                    jobj.Add("PUBSH_KBN", ConvertUtil.ToInt32(lePurblisherType.EditValue));

                    jobj.Add("REPNM", teRepNm.Text.Trim());
                  
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
                    jobj.Add("SPECIALNM", teSpecialNm.Text.Trim());
                    

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

        public void InsertPublishInfo()
        {
            if (Dangol.MessageYN("매입처 정보를 추가하시겠습니까?") == DialogResult.Yes)
            {
                if (check())
                {
                    int publishCd = ConvertUtil.ToInt32(tePublisherCd.Text.Trim());

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    string url = "/regist/insertPublishInfo.json";

                    jobj.Add("SHOPCD", 1);
                    jobj.Add("PUBSHCD", publishCd);
                    jobj.Add("PURCHCD", publishCd);
                    jobj.Add("PUBSHNM", tePublisherNm.Text.Trim());
                    jobj.Add("PURCHNM", tePublisherNm.Text.Trim());
                    jobj.Add("PURCH_KBN", ConvertUtil.ToInt32(lePurblisherType.EditValue));
                    jobj.Add("PUBSH_KBN", ConvertUtil.ToInt32(lePurblisherType.EditValue));

                    jobj.Add("REPNM", teRepNm.Text.Trim());

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
                    jobj.Add("SPECIALNM", teSpecialNm.Text.Trim());

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

        private void tePublisherCd_EditValueChanged(object sender, EventArgs e)
        {
            if (_publishCd != ConvertUtil.ToInt64(tePublisherCd.Text))
            {
                buttonHandler(false, false, false);
            }
        }
    }
}
