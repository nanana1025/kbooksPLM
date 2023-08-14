using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace WareHousingMaster.view.common.DB.kbooks
{
    static class DBCommon
    {
        static public string checkHMA02(int purchCd)
        {
            string sData = "";
            try
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                string url = "/search/checkHMA02.json";
                jobj.Add("PURCHCD", purchCd);

                // 요청 전송
                string result = DBConnect.SendRequestMessage(jobj, url);

                // 반환값 파싱
                jResult = JObject.Parse(result);
                if (Convert.ToBoolean(jResult["SUCCESS"]))
                {
                    if(ConvertUtil.ToBoolean(jResult["EXIST"]))
                    {
                        string bookConstNm = ConvertUtil.ToString(jResult["BOOK_CONST_NM2"]);
                        if (string.IsNullOrEmpty(bookConstNm))
                            sData = "";
                        else
                            sData = bookConstNm;
                    }
                }
                else
                {
                    sData = "-1";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                sData = "-1";
            }

            return sData;
        }

        
    }
}
