using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSearchPublisherDetail : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        List<long> listShopCd;

        public usrSearchPublisherDetail()
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

        public void setData(DataRowView obj)
        {
            tePublisherCd.Text = ConvertUtil.ToString(obj["PUBSHCD"]);
            tePublisherNm.Text = ConvertUtil.ToString(obj["PUBSHNM"]);
            tePublisherKBN.Text = ConvertUtil.ToString(obj["PUBSH_KBN"]);
            teSpecialNm.Text = ConvertUtil.ToString(obj["SPECIALNM"]);
            teRepNm.Text = ConvertUtil.ToString(obj["REPNM"]);
            teTel1.Text = ConvertUtil.ToString(obj["TEL_NUM1"]);
            teTel2.Text = ConvertUtil.ToString(obj["TEL_NUM2"]);
            teFax.Text = ConvertUtil.ToString(obj["FAX_NUM"]);
            tePostNo.Text = ConvertUtil.ToString(obj["POST_NO"]);
            teAddress.Text = ConvertUtil.ToString(obj["ADDRESS"]);
        }

        public void setReset()
        {
            tePublisherCd.Text = "";
            tePublisherNm.Text = "";
            tePublisherKBN.Text = "";
            teSpecialNm.Text = "";
            teRepNm.Text = "";
            teTel1.Text = "";
            teTel2.Text = "";
            teFax.Text = "";
            tePostNo.Text = "";
            teAddress.Text = "";
        }
    }
}
