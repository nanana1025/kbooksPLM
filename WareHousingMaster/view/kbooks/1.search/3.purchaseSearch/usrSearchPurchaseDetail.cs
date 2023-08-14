using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSearchPurchaseDetail : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        List<long> listShopCd;

        JObject _jobj;

        public usrSearchPurchaseDetail()
        {
            InitializeComponent();
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

            //foreach (DataRow row in dtShopCd.Rows)
            //    listShopCd.Add(ConvertUtil.ToInt64(row["SHOPCD"]));
            
            //Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");

        }

        private void setinitialize()
        {
            
        }

        public void getData(JObject jobj)
        {
            _jobj = jobj;

            JObject jResult = new JObject();
            string url = "/search/getPurchaseDetail.json";

            if (jobj != null)
            {
                if (DBConnect.getRequest(jobj, ref jResult, url))
                {
                    if (Convert.ToBoolean(jResult["EXIST"]))
                    {
                        setData(jResult);
                    }
                }
                else
                {
                    Dangol.Error(jResult["MSG"]);
                }
            }
            else
            {

            }
        }

        public void setData(JObject obj)
        {
            tePurchCd.Text = ConvertUtil.ToString(obj["PURCHCD"]);
            tePurchNm.Text = ConvertUtil.ToString(obj["PURCHNM"]);
            tePurchKBN.Text = ConvertUtil.ToString(obj["PURCH_KBN"]);
            teSpecialNm.Text = ConvertUtil.ToString(obj["SPECIALNM"]);
            teRepNm.Text = ConvertUtil.ToString(obj["REPNM"]);
            teEntNo.Text = ConvertUtil.ToString(obj["ENT_NUM"]);

            tePersonNo.Text = ConvertUtil.ToString(obj["PERSON_NUM"]);
            teBusinessItem.Text = ConvertUtil.ToString(obj["BUSINESS_ITEM"]);
            teBusinessType.Text = ConvertUtil.ToString(obj["BUSINESS_TYPE"]);
            tePostNo.Text = ConvertUtil.ToString(obj["POST_NUM"]);
            teAddress.Text = ConvertUtil.ToString(obj["ADDRESS"]);
            teTel1.Text = ConvertUtil.ToString(obj["TEL_NUM1"]);
            teTel2.Text = ConvertUtil.ToString(obj["TEL_NUM2"]);
            teFax.Text = ConvertUtil.ToString(obj["FAX_NUM"]);

            meBookConstNm1.Text = ConvertUtil.ToString(obj["BOOK_CONST_NM1"]);
            meBookConstNm2.Text = ConvertUtil.ToString(obj["BOOK_CONST_NM2"]);
            //tegrou.Text = ConvertUtil.ToString(obj["ORD_GROUPCD"]);
            teGrganNm.Text = ConvertUtil.ToString(obj["GRGAN_NM"]);
            tePayDt.Text = ConvertUtil.ToString(obj["PAY_DATE"]);
            teBookConstDay.Text = ConvertUtil.ToDateTimeNull_S(obj["BOOK_CONST_DAY"]);
            tePayGrade.Text = ConvertUtil.ToString(obj["PAY_GRADE"]);
        }

        public void setReset()
        {
            tePurchCd.Text = "";
            tePurchNm.Text = "";
            tePurchKBN.Text = "";
            teSpecialNm.Text = "";
            teRepNm.Text = "";
            teEntNo.Text = "";

            tePersonNo.Text = "";
            teBusinessItem.Text = "";
            teBusinessType.Text = "";
            tePostNo.Text = "";
            teAddress.Text = "";
            teTel1.Text = "";
            teTel2.Text = "";
            teFax.Text = "";

            meBookConstNm1.Text = "";
            meBookConstNm2.Text = "";
            //tegrou.Text = "";
            teGrganNm.Text = "";
            tePayDt.Text = "";
            teBookConstDay.Text = "";
            tePayGrade.Text = "";
        }
    }
}
