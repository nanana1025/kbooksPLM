using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrBestSellerSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;
        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public usrBestSellerSearch()
        {
            InitializeComponent();

            _searchType = -1;
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
            Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");
        }

        private void setinitialize()
        {
            var today = DateTime.Today;
            //var pastDate = today.AddDays(-7);

            deDtWork.EditValue = today;
            deDtFrom.EditValue = today;
            deDtTo.EditValue = today;
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

  
     
        public JObject getSearch()
        {
            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                return jData;
            else
                return null;
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            JObject jData = new JObject();
            bool isSuccess = checkSearch(ref jData);

            if (isSuccess)
                searchHandler(jData);
            else
                Dangol.Message(jData["MSG"]);
        }

        private bool checkSearch(ref JObject jData)
        {
            string dtFrom = "";
            string dtTo = "";
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
                dtFrom = $"{deDtFrom.Text} 00:00:00";

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
                dtTo = $"{deDtTo.Text} 23:59:59";

            DateTime dtfrom;
            DateTime dtto;
            dtfrom = Convert.ToDateTime(dtFrom);
            dtto = Convert.ToDateTime(dtTo);

            int result = DateTime.Compare(dtfrom, dtto);

            if (result > 0)
            {
                jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다.");
                return false;
            }

            TimeSpan TS = dtto - dtfrom;
            int diffDay = TS.Days;

            if (diffDay > 365)
            {
                jData.Add("MSG", "검색 기간은 1년(365일)을 초과할 수 없습니다.");
                return false;
            }

            jData.Add("DT_FROM", dtfrom.ToString("yyyyMMdd"));
            jData.Add("DT_TO", dtto.ToString("yyyyMMdd"));


            string dtWork = "";
            if (deDtWork.EditValue != null && !string.IsNullOrEmpty(deDtWork.EditValue.ToString()))
                dtWork = $"{deDtWork.Text} 00:00:00";
           
            if(string.IsNullOrWhiteSpace(dtWork))
            {
                jData.Add("MSG", "영업일을 선택하세요.");
                return false;
            }
            else
                jData.Add("DT_WORK", dtWork);


            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));


            int category = ConvertUtil.ToInt32(rgCategory.EditValue);
            string colNm = ConvertUtil.ToString(rgCategory.Properties.Items[rgCategory.SelectedIndex].Tag);

            if (category > 0)
            {
                string codeS = teCd_S.Text.Trim();
                string codeE = teCd_E.Text.Trim();
                string name = teName.Text.Trim();

                jData.Add($"COLNM", $"{ colNm}CD");
                jData.Add($"SELECTED_TYPE", rgCategory.Properties.Items[rgCategory.SelectedIndex].Description);
                if (string.IsNullOrWhiteSpace(codeS) && string.IsNullOrWhiteSpace(codeE))
                {
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        jData.Add("MSG", $"{rgCategory.Text}코드 또는 이름을 입력하세요.");
                        return false;
                    }
                    else
                    {
                        jData.Add("SEARCH_TYPE", "SINGLE");
                        jData.Add($"{colNm}_NM", name);
                    }
                }
                else
                {

                    if (!string.IsNullOrWhiteSpace(codeS) && string.IsNullOrWhiteSpace(codeE))
                    {
                        jData.Add("SEARCH_TYPE", "SINGLE");
                        jData.Add($"{colNm}CD", ConvertUtil.ToInt32(codeS));

                        if (!string.IsNullOrWhiteSpace(name))
                            jData.Add($"{colNm}_NM", name);
                    }
                    else if (string.IsNullOrWhiteSpace(codeS) && !string.IsNullOrWhiteSpace(codeE))
                    {
                        jData.Add("SEARCH_TYPE", "SINGLE");
                        jData.Add($"{colNm}CD", ConvertUtil.ToInt32(codeE));

                        if (!string.IsNullOrWhiteSpace(name))
                            jData.Add($"{colNm}_NM", name);
                    }
                    else if (codeS.Equals(codeE))
                    {
                        jData.Add("SEARCH_TYPE", "SINGLE");
                        jData.Add($"{colNm}CD", ConvertUtil.ToInt32(codeE));

                        if (!string.IsNullOrWhiteSpace(name))
                            jData.Add($"{colNm}_NM", name);
                    }
                    else
                    {
                        int start = ConvertUtil.ToInt32(codeS);
                        int end = ConvertUtil.ToInt32(codeE);

                        jData.Add("SEARCH_TYPE", "MULTI");
                        if (start > end)
                        {
                            jData.Add($"{colNm}CD_S", end);
                            jData.Add($"{colNm}CD_E", start);
                        }
                        else
                        {
                            jData.Add($"{colNm}CD_S", start);
                            jData.Add($"{colNm}CD_E", end);
                        }
                    }
                }
            }

            jData.Add($"SALE_CNT", ConvertUtil.ToInt32(seSaleCnt.EditValue));

            return true;
        }

        private void rgDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = ConvertUtil.ToInt32(rgDate.EditValue);
            var today = DateTime.Today;
           
            deDtFrom.ReadOnly = value > 1;
            deDtTo.ReadOnly = value > 1;

            if (value == 1)
            {
            }
            else if (value == 2)
            {
                deDtFrom.EditValue = today;
                deDtTo.EditValue = today;
            }
            else if (value == 3)
            {
                var pastDate = today.AddDays(-1);
                deDtFrom.EditValue = pastDate;
                deDtTo.EditValue = pastDate;
            }
            else if (value == 4)
            {
                var pastDate = today.AddDays(Convert.ToInt32(DayOfWeek.Sunday) - Convert.ToInt32(DateTime.Today.DayOfWeek));

                deDtFrom.EditValue = pastDate;
                deDtTo.EditValue = today;
            }
            else if (value == 5)
            {
                var pastDateSun = today.AddDays(Convert.ToInt32(DayOfWeek.Sunday) - Convert.ToInt32(DateTime.Today.DayOfWeek) - 7);
                var pastDateSat = pastDateSun.AddDays(6);

                deDtFrom.EditValue = pastDateSun;
                deDtTo.EditValue = pastDateSat;
            }
            else if (value == 6)
            {
                var dateMonthFirst = DateTime.Now.AddDays(1 - DateTime.Now.Day);

                deDtFrom.EditValue = dateMonthFirst;
                deDtTo.EditValue = today;
            }
            else if (value == 7)
            {
                var dateMonthFirst = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                var pastMonthFirst = dateMonthFirst.AddMonths(-1);
                var pastMonthLast = dateMonthFirst.AddDays(-1);

                deDtFrom.EditValue = pastMonthFirst;
                deDtTo.EditValue = pastMonthLast;
            }
        }
    }
}
