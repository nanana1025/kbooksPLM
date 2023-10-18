using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrNewBooksSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public int _releaseCategory { get; set; }

        int _viewType;
        int _searchType;
        public delegate void SearchHandler(JObject jData);
        public event SearchHandler searchHandler;

        public delegate void ClearHandler();
        public event ClearHandler clearHandler;

        public usrNewBooksSearch()
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
            var pastDate = today.AddDays(-15);
            if (ProjectInfo._userType.Equals("U"))
            {
                string value = Util.getValue((int)Enum.VALUE_MGNT_TYPE.SEARCH, (int)Enum.VALUE_MGNT_SEARCH.NEW);
                if(!string.IsNullOrEmpty(value))
                    pastDate = today.AddDays(ConvertUtil.ToInt32(value) * -1);
                else
                    pastDate = today.AddDays(-7);
            }

            deDtWork.EditValue = today;
            deDtFrom.EditValue = pastDate;
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
            Search();
        }

        public void Search()
        {
            clearHandler();

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

            try
            {
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
            }
            catch(FormatException ex)
            {
                jData.Add("MSG", "날짜 형식을 확인하세요.");
                return false;
            }

            

            //jData.Add("DT_FROM", dtFrom);
            //jData.Add("DT_TO", dtTo);


            //string dtWork = "";
            //if (deDtWork.EditValue != null && !string.IsNullOrEmpty(deDtWork.EditValue.ToString()))
            //    dtWork = $"{deDtWork.Text} 00:00:00";
           
            //if(string.IsNullOrWhiteSpace(dtWork))
            //{
            //    jData.Add("MSG", "영업일을 선택하세요.");
            //    return false;
            //}
            //else
            //    jData.Add("DT_WORK", dtWork);


            jData.Add("SHOPCD", ConvertUtil.ToInt32(leShopCd.EditValue));

            int category = ConvertUtil.ToInt32(rgCategory.EditValue);
            string colNm = ConvertUtil.ToString(rgCategory.Properties.Items[rgCategory.SelectedIndex].Tag);

            //if (category == 1)
            //    colNm = "DEPT";
            //else if (category == 2)
            //    colNm = "GROUP";
            //else if (category == 3)
            //    colNm = "STAND";

            jData.Add($"CATEGORY", category);
            jData.Add($"COLNM", $"{ colNm}CD");
            jData.Add($"SELECTED_TYPE", rgCategory.Properties.Items[rgCategory.SelectedIndex].Description);

            if (category == 0)
            {
                jData.Add("SEARCH_TYPE", "ALL");
                //jData.Add($"SELECTED_TYPE", "부");
            }
            else
            {
                string codeS = teCd_S.Text.Trim();
                string codeE = teCd_E.Text.Trim();
                string name = teName.Text.Trim();

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

            return true;
        }

        public void setFocus()
        {
            //teCd_S.Focus();
            deDtFrom.Focus();
        }

        public void clear()
        {
            teCd_S.Text = "";
            teCd_E.Text = "";
            teName.Text = "";

            rgCategory.EditValue = 0;
        }

        private void teCd_S_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void teCd_E_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void teName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void rgCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int category = ConvertUtil.ToInt32(rgCategory.EditValue);

            teCd_S.Text = "";
            teCd_E.Text = "";
            teName.Text = "";

            teCd_S.ReadOnly = category == 0;
            teCd_E.ReadOnly = category == 0;
            teName.ReadOnly = category == 0;
        }
    }
}
