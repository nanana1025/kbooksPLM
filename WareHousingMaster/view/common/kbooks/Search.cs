using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Diagnostics;



namespace WareHousingMaster.view.common
{
    static class Search
    {
        static public DataTable checkGroupCd(int shopCd, int groupCd, string groupNm)
        {
            try
            {
                JObject jobj = new JObject();
                string url = "/common/checkGroupCd.json";

                jobj.Add("SHOPCD", shopCd);

                if (groupCd > 0)
                    jobj.Add("GROUPCD", groupCd);
                if(!string.IsNullOrEmpty(groupNm))
                    jobj.Add("GROUP_NM", groupNm);
                
                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jobj = JObject.Parse(result);

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("CD", typeof(int)));
                dt.Columns.Add(new DataColumn("NM", typeof(string)));

                if (ConvertUtil.ToBoolean(jobj["SUCCESS"]) && ConvertUtil.ToBoolean(jobj["EXIST"]))
                {
                    int codeCd = ConvertUtil.ToInt32(jobj["GROUPCD"]);
                    string codeNm = ConvertUtil.ToString(jobj["ORGAN_NM"]);

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
