using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Diagnostics;



namespace WareHousingMaster.view.common
{
    static class Order
    {

        public static int STORES = 1;
        public static int STOREE = 15;


        static public DataTable checkPurchCd(int shopCd, int purchCd, string purchNm)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/order/checkPurchCd.json";

                jobj.Add("SHOPCD", shopCd);

                if (purchCd > 0)
                    jobj.Add("PURCHCD", purchCd);
                if (!string.IsNullOrEmpty(purchNm))
                    jobj.Add("PURCHNM", purchNm);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("CD", typeof(int)));
                dt.Columns.Add(new DataColumn("NM", typeof(string)));

                if (ConvertUtil.ToBoolean(jobj["SUCCESS"]) && ConvertUtil.ToBoolean(jobj["EXIST"]))
                {
                    int codeCd = ConvertUtil.ToInt32(jobj["PURCHCD"]);
                    string codeNm = ConvertUtil.ToString(jobj["PURCHNM"]);

                    DataRow dr = dt.NewRow();

                    dr["CD"] = codeCd;
                    dr["NM"] = codeNm;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new DataTable();
            }
        }
    }
}
