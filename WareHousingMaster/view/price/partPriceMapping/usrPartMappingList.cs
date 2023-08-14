using DevExpress.XtraSplashScreen;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.price.partPriceMapping
{
    public partial class usrPartMappingList : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _currentPartPrice;
        DataTable _dtPartPrice;
        BindingSource _bsPartPrice;

        int _requestId;
        string _currentUserId;

        bool initialize = true;
        bool initializeEnter = true;

        string _partKey;
        int _display_seq;

        public usrPartMappingList()
        {
            InitializeComponent();


            _dtPartPrice = new DataTable();
            _dtPartPrice.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPartPrice.Columns.Add(new DataColumn("LT_CUSTOM_PART_ID", typeof(long)));
            _dtPartPrice.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtPartPrice.Columns.Add(new DataColumn("KEYWORD", typeof(string)));

            _dtPartPrice.Columns.Add(new DataColumn("PART_KEY", typeof(string)));
            _dtPartPrice.Columns.Add(new DataColumn("PART_NAME", typeof(string)));

            _dtPartPrice.Columns.Add(new DataColumn("PART_KEY_WM", typeof(string)));
            _dtPartPrice.Columns.Add(new DataColumn("PART_NAME_WM", typeof(string)));

            _dtPartPrice.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtPartPrice.Columns.Add(new DataColumn("PRICE_WM", typeof(long)));
            _dtPartPrice.Columns.Add(new DataColumn("DIFF", typeof(long)));
            _dtPartPrice.Columns.Add(new DataColumn("DISPLAY_SEQ", typeof(int)));             _dtPartPrice.Columns.Add(new DataColumn("MAPPING_YN", typeof(int)));
            _dtPartPrice.Columns.Add(new DataColumn("PART_STATE", typeof(int)));
            _dtPartPrice.Columns.Add(new DataColumn("LAST_DT", typeof(string)));

            _dtPartPrice.Columns.Add(new DataColumn("DISPLAY_YN", typeof(int)));


            _bsPartPrice = new BindingSource();
            _bsPartPrice.DataSource = _dtPartPrice;

            gcPartPrice.DataSource = _bsPartPrice;

            initialize = true;
            initializeEnter = true;

        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {

            setInfoBox();

            tePartKey.DataBindings.Add(new Binding("Text", _bsPartPrice, "PART_KEY", false, DataSourceUpdateMode.Never));
            teComponentCd.DataBindings.Add(new Binding("Text", _bsPartPrice, "COMPONENT_CD", false, DataSourceUpdateMode.Never));
            tePartName1.DataBindings.Add(new Binding("Text", _bsPartPrice, "PART_NAME", false, DataSourceUpdateMode.Never));
            teKeyword.DataBindings.Add(new Binding("Text", _bsPartPrice, "KEYWORD", false, DataSourceUpdateMode.Never));
           
            sePrice.DataBindings.Add(new Binding("EditValue", _bsPartPrice, "PRICE", false, DataSourceUpdateMode.Never));
            sePartPriceWm.DataBindings.Add(new Binding("EditValue", _bsPartPrice, "PRICE_WM", false, DataSourceUpdateMode.Never));
            seDisplaySeq.DataBindings.Add(new Binding("EditValue", _bsPartPrice, "DISPLAY_SEQ", false, DataSourceUpdateMode.Never));
            rgDispalyYn.DataBindings.Add(new Binding("EditValue", _bsPartPrice, "DISPLAY_YN", false, DataSourceUpdateMode.Never));


            if (!initialize)
                Dangol.ShowSplash();

            gvPartPrice.FocusedRowObjectChanged -= gvPartPrice_FocusedRowObjectChanged;
            getPartParicetList(true);
            gvPartPrice.FocusedRowObjectChanged += gvPartPrice_FocusedRowObjectChanged;
            if (_dtPartPrice.Rows.Count > 0)
            {
                gvPartPrice.FocusedRowHandle = -2147483646;
                gvPartPrice.MoveFirst();
            }
            if (!initialize)
                Dangol.CloseSplash();

            initialize = false;

        }

        public IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        private void setInfoBox()
        {
            
           DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtComponentCd, "NTB", "노트북");
            Util.insertRowonTop(dtComponentCd, "DKT", "PC");
            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr = dtComponentCd.NewRow();

                dr["KEY"] = ProjectInfo._componetCd[i];
                dr["VALUE"] = ProjectInfo._componetNm[i];
                dtComponentCd.Rows.Add(dr);
            }

            Util.insertRowonTop(dtComponentCd, "-1", "전체");

            Util.LookupEditHelper(leComonentCd, dtComponentCd, "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtDanawa = new DataTable();
            //DataTable dtDanawa1 = new DataTable();

            dtDanawa.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtDanawa.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //dtDanawa1.Columns.Add(new DataColumn("KEY", typeof(string)));
            //dtDanawa1.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtDanawa, 0, "숨김");
            Util.insertRowonTop(dtDanawa, 1, "조회");
            Util.insertRowonTop(dtDanawa, -1, "전체");

            //Util.insertRowonTop(dtDanawa1, "N", "미조회");
            //Util.insertRowonTop(dtDanawa1, "Y", "조회");

            Util.LookupEditHelper(leDisplayYn, dtDanawa, "KEY", "VALUE");
            //Util.LookupEditHelper(leDanawaFlag1, dtDanawa1, "KEY", "VALUE");

            leComonentCd.EditValue = "-1";
            leDisplayYn.EditValue = 1;
        }

        private void setEditable(bool flag)
        {
            //teCategory.ReadOnly = !flag;
            //meRequest.ReadOnly = !flag;
            //meResponse.ReadOnly = flag;

        }
  
        private void gvPartPrice_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPartPrice.RowCount > 0);

            if (isValidRow)
            {
                _currentPartPrice = e.Row as DataRowView;

                _partKey = ConvertUtil.ToString(_currentPartPrice["PART_KEY"]);
                _display_seq = ConvertUtil.ToInt32(_currentPartPrice["DISPLAY_SEQ"]);

                //if (_currentUserId.Equals(ProjectInfo._userId))
                //{
                //    setEditable(true);
                //}
                //else
                //{
                //    setEditable(false);
                //}
            }
            else
            {
                _partKey = "";
                _currentPartPrice = null;
                _display_seq = -1;
                teCategory.ReadOnly = true;
                //meRequest.ReadOnly = true;
                //meResponse.ReadOnly = true;
            }
        }

        private void partPriceRefresh()
        {
            Dangol.ShowSplash();
            string partCode = _partKey;
            gvPartPrice.FocusedRowObjectChanged -= gvPartPrice_FocusedRowObjectChanged;
            getPartParicetList(false);
            gvPartPrice.FocusedRowObjectChanged += gvPartPrice_FocusedRowObjectChanged;
            int rowHandle = gvPartPrice.LocateByValue("PARTCODE", partCode);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvPartPrice.FocusedRowHandle = -2147483646;
                    gvPartPrice.FocusedRowHandle = rowHandle;
                }
            }
            else
            {
                if (_dtPartPrice.Rows.Count > 0)
                {
                    gvPartPrice.FocusedRowHandle = -2147483646;
                    gvPartPrice.MoveFirst();
                }
            }
            Dangol.CloseSplash();
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            partPriceRefresh();
        }

        private bool getPartParicetList(bool isinit)
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtPartPrice.Clear();

            if (DBPrice.searchPartPriceMapping(jData, ref jResult))
            {
                gvPartPrice.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                string partKeyWm;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtPartPrice.NewRow();

                    partKeyWm = ConvertUtil.ToString(obj["PART_KEY_WM"]);

                    dr["NO"] = index++;
                    dr["LT_CUSTOM_PART_ID"] = obj["LT_CUSTOM_PART_ID"];
                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];

                    dr["KEYWORD"] = obj["KEYWORD"];
                    dr["PART_KEY"] = obj["PART_KEY"];
                    dr["PART_NAME"] = obj["PART_NAME"];
                    dr["PART_KEY_WM"] = partKeyWm;
                    dr["PART_NAME_WM"] = obj["PART_NAME_WM"];

                    dr["PRICE"] = obj["PRICE"];
                    dr["PRICE_WM"] = obj["PRICE_WM"];
                    if (string.IsNullOrEmpty(partKeyWm))
                        dr["DIFF"] = 0;
                    else
                        dr["DIFF"] = ConvertUtil.ToInt64(obj["PRICE"]) - ConvertUtil.ToInt64(obj["PRICE_WM"]);
                    dr["DISPLAY_SEQ"] = obj["DISPLAY_SEQ"];
                    dr["MAPPING_YN"] = obj["MAPPING_YN"];
                    dr["PART_STATE"] = obj["PART_STATE"];
                    dr["LAST_DT"] = ConvertUtil.ToDateTime(obj["LAST_DT"], "yyyy-MM-dd");

                    dr["DISPLAY_YN"] = obj["DISPLAY_YN"];

                    _dtPartPrice.Rows.Add(dr);
                }

                gvPartPrice.EndDataUpdate();

                return true;

            }
            else
            {
                return false;
            }
        }

        private bool checkSearch(ref JObject jData)
        {
            var today = DateTime.Today;
            int hour = ConvertUtil.ToInt32(DateTime.Now.ToString("HH"));

            if (hour > 7 || hour < 24)
            {
                string stoday = $"{DateTime.Now.ToString("yy")}-{DateTime.Now.ToString("MM")}-{DateTime.Now.ToString("dd")} 06:00:00";
                jData.Add("TODAY", stoday);
            }
            else if (hour < 7)
            {
                var pastDate = today.AddDays(-1);
                string stoday = $"{pastDate.ToString("yy")}-{pastDate.ToString("MM")}-{pastDate.ToString("dd")} 06:00:00";
                jData.Add("TODAY", stoday);
            }
            else
            {
                var pastDate = today.AddDays(-1);
                string stoday = $"{pastDate.ToString("yy")}-{pastDate.ToString("MM")}-{pastDate.ToString("dd")} 06:00:00";
                jData.Add("TODAY", stoday);
            }

            if (!ConvertUtil.ToString(leComonentCd.EditValue).Equals("-1"))
            {
                jData.Add("COMPONENT_CD", ConvertUtil.ToString(leComonentCd.EditValue));
            }

            if (ConvertUtil.ToInt32(leDisplayYn.EditValue) > -1)
            {
                jData.Add("DISPLAY_YN", ConvertUtil.ToString(leDisplayYn.EditValue));
            }

            //if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCategory.Text)))
            //{
            //    jData.Add("PARTCAT2", teCategory.Text);
            //}

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(tePartName.Text)))
            {
                jData.Add("PART_NAME", tePartName.Text);
            }

            return true;
        }

        private void lcgBoard_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(0))
            {
                using (DlgCreateLTPart dlgCreateLTPart = new DlgCreateLTPart(
                    "",
                    "",
                    "",
                    "",
                    0,
                    2,
                    "",
                    "")
                )
                {
                    if (dlgCreateLTPart.ShowDialog(this) == DialogResult.OK)
                    {
                        partPriceRefresh();
                        Dangol.Message("처리되었습니다.");
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(1))
            {
                int mappingYn = ConvertUtil.ToInt32(_currentPartPrice["MAPPING_YN"]);
                int type = 1;
                string partCodeWm = "";

                if (mappingYn == 1)
                {
                    type = 2;
                    partCodeWm = ConvertUtil.ToString(_currentPartPrice["PARTCODEWM"]);
                }
                else
                {
                    type = 1;
                }

                string componentCd = ConvertUtil.ToString(_currentPartPrice["PARTCAT1"]);

                if (ConCSInfo._dicLTWMComponentCd.ContainsKey(componentCd))
                {
                    string WMcomponentCd = ConCSInfo._dicLTWMComponentCd[componentCd];

                    using (DlgGetWmPartList dlgGetWmPartList = new DlgGetWmPartList(
                        ConvertUtil.ToString(_currentPartPrice["PARTCODE"]),
                        partCodeWm,
                        ConvertUtil.ToString(_currentPartPrice["PARTNAME"]),
                        WMcomponentCd,
                        type)
                        )
                    {
                        if (dlgGetWmPartList.ShowDialog(this) == DialogResult.OK)
                        {
                            gvPartPrice.BeginDataUpdate();
                            _currentPartPrice.BeginEdit();

                            _currentPartPrice["PARTCODEWM"] = dlgGetWmPartList._currentRow["PARTCODE"];
                            _currentPartPrice["PARTNAMEWM"] = dlgGetWmPartList._currentRow["PARTNAME"];
                            _currentPartPrice["PARTPRICEWM"] = dlgGetWmPartList._currentRow["MONEY"];
                            _currentPartPrice["PART_STATE"] = dlgGetWmPartList._currentRow["PART_STATE"];
                            _currentPartPrice["MAPPING_YN"] = 1;

                            long money = ConvertUtil.ToInt64(_currentPartPrice["MONEY"]);
                            long partPrice = ConvertUtil.ToInt64(dlgGetWmPartList._currentRow["MONEY"]);

                            _currentPartPrice["DIFF"] = money - partPrice;

                            _currentPartPrice.EndEdit();
                            gvPartPrice.EndDataUpdate();

                            Dangol.Message("처리되었습니다.");
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int mappingYn = ConvertUtil.ToInt32(_currentPartPrice["MAPPING_YN"]);
                if (mappingYn == 1)
                {
                    if (Dangol.MessageYN($"선택하신 부품 매핑 정보를 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();
                        JObject jData = new JObject();

                        jData.Add("PARTCODELTA", ConvertUtil.ToString(_currentPartPrice["PARTCODE"]));
                        jData.Add("PARTCODEWM", ConvertUtil.ToString(_currentPartPrice["PARTCODEWM"]));
                        //jData.Add("PARTNAMELTA", ConvertUtil.ToString(_currentPartPrice["PARTNAME"]));
                        //jData.Add("PARTNAMEWM", ConvertUtil.ToString(_currentPartPrice["PARTNAMEWM"]));
                        jData.Add("TYPE", 3);

                        if (DBPrice.updatePartPriceMapping(jData, ref jResult))
                        {
                            gvPartPrice.BeginDataUpdate();
                            _currentPartPrice.BeginEdit();

                            _currentPartPrice["PARTCODEWM"] = "";
                            _currentPartPrice["PARTNAMEWM"] = "";
                            _currentPartPrice["PARTPRICEWM"] = 0;
                            _currentPartPrice["DIFF"] = 0;
                            _currentPartPrice["NORDER"] = 0;
                            _currentPartPrice["MAPPING_YN"] = 0;
                            _currentPartPrice["DANAWAFLAG"] = "N";
                            _currentPartPrice["PART_STATE"] = 3;
                            _currentPartPrice["MAPPING_YN"] = 0;

                            _currentPartPrice.EndEdit();
                            gvPartPrice.EndDataUpdate();
                            Dangol.Message("처리되었습니다.");
                        }
                        else
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }
                    }
                }
                else
                {
                    Dangol.Message("매핑되지 않은 부품입니다.");
                    return;
                }
            }
        }

        private void lgcDetail_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if(_currentPartPrice == null)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            if (Dangol.MessageYN($"부품 정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jData = new JObject();

                jData.Add("LT_CUSTOM_PART_ID", ConvertUtil.ToInt64(_currentPartPrice["LT_CUSTOM_PART_ID"]));
                jData.Add("PART_KEY", tePartKey.Text);

                if (!ConvertUtil.ToString(_currentPartPrice["PART_NAME"]).Equals(tePartName1.Text))
                    jData.Add("PART_NAME", tePartName1.Text);

                if (!ConvertUtil.ToString(_currentPartPrice["KEYWORD"]).Equals(teKeyword.Text))
                    jData.Add("KEYWORD", teKeyword.Text);

                if (ConvertUtil.ToInt64(_currentPartPrice["PRICE"]) != Convert.ToInt64(sePrice.EditValue))
                    jData.Add("PRICE", Convert.ToInt64(sePrice.EditValue));

                int displayYn = ConvertUtil.ToInt32(rgDispalyYn.EditValue);

                if (ConvertUtil.ToInt32(_currentPartPrice["DISPLAY_YN"]) != displayYn)
                {
                    jData.Add("DISPLAY_YN", displayYn);
                    jData.Add("DANAWAFLAG", displayYn == 1 ? "Y" : "N");
                }

                if (DBPrice.updatePartPrice(jData, ref jResult))
                {
                    if (_display_seq != ConvertUtil.ToInt32(seDisplaySeq.EditValue))
                    {
                        jData.RemoveAll();
                        jData.Add("LT_CUSTOM_PART_ID", ConvertUtil.ToInt64(_currentPartPrice["LT_CUSTOM_PART_ID"]));
                        jData.Add("DISPLAY_SEQ", ConvertUtil.ToInt32(seDisplaySeq.EditValue));

                        jData.Add("PART_KEY", tePartKey.Text);
                        jData.Add("NORDER", ConvertUtil.ToInt32(seDisplaySeq.EditValue));

                        int newDisplaySeq = ConvertUtil.ToInt32(seDisplaySeq.EditValue);

                        if (_display_seq > newDisplaySeq)
                        {
                            jData.Add("PLUS", 1);
                            jData.Add("START", newDisplaySeq);
                            jData.Add("END", _display_seq - 1);
                        }
                        else
                        {
                            jData.Add("MINUS", 1);
                            jData.Add("START", _display_seq+1);
                            jData.Add("END", newDisplaySeq);
                        }

                        DBPrice.updatePartNOrder(jData, ref jResult);

                        partPriceRefresh();
                    }
                    else
                    {
                        gvPartPrice.BeginDataUpdate();
                        _currentPartPrice.BeginEdit();
                        _currentPartPrice["PART_NAME"] = jData["PART_NAME"];
                        _currentPartPrice["KEYWORD"] = jData["KEYWORD"];
                        if (ConvertUtil.ToInt64(_currentPartPrice["PRICE"]) != Convert.ToInt64(sePrice.EditValue))
                            _currentPartPrice["PRICE"] = jData["PRICE"];
                        _currentPartPrice["DISPLAY_YN"] = jData["DISPLAY_YN"];
                        _currentPartPrice.EndEdit();
                        gvPartPrice.EndDataUpdate();
                    }

                    Dangol.Message("수정되었습니다.");
                }
            }
        }

        
    }
}