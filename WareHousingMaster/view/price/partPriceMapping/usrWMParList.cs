using DevExpress.XtraSplashScreen;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.price.partPriceMapping
{
    public partial class usrWMParList : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _currentWMPartPrice;
        DataTable _dtWMPartPrice;
        BindingSource _bsWMPartPrice;

        DataRowView _currentLTPartPrice;
        DataTable _dtLTPartPrice;
        BindingSource _bsLTPartPrice;

        int _requestId;
        string _currentUserId;

        bool initialize = true;
        bool initializeEnter = true;

        string _partCode;

        public usrWMParList()
        {
            InitializeComponent();


            _dtWMPartPrice = new DataTable();
            _dtWMPartPrice.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtWMPartPrice.Columns.Add(new DataColumn("PARTCAT1", typeof(string)));
            _dtWMPartPrice.Columns.Add(new DataColumn("PARTCAT2", typeof(string)));
            _dtWMPartPrice.Columns.Add(new DataColumn("PARTCAT3", typeof(string)));
            _dtWMPartPrice.Columns.Add(new DataColumn("PARTCODE", typeof(string)));
            _dtWMPartPrice.Columns.Add(new DataColumn("PARTNAME", typeof(string)));
            _dtWMPartPrice.Columns.Add(new DataColumn("MONEY", typeof(long)));
            _dtWMPartPrice.Columns.Add(new DataColumn("MAPPING_YN", typeof(int)));
            _dtWMPartPrice.Columns.Add(new DataColumn("PART_STATE", typeof(int)));
            _dtWMPartPrice.Columns.Add(new DataColumn("USE_YN", typeof(int)));
            _dtWMPartPrice.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _dtLTPartPrice = new DataTable();
            _dtLTPartPrice.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtLTPartPrice.Columns.Add(new DataColumn("SEQ", typeof(long)));
            _dtLTPartPrice.Columns.Add(new DataColumn("PARTCAT1", typeof(string)));
            _dtLTPartPrice.Columns.Add(new DataColumn("PARTCAT2", typeof(string)));
            _dtLTPartPrice.Columns.Add(new DataColumn("PARTCODE", typeof(string)));
            _dtLTPartPrice.Columns.Add(new DataColumn("PARTNAME", typeof(string)));
            _dtLTPartPrice.Columns.Add(new DataColumn("MONEY", typeof(long)));


            _bsWMPartPrice = new BindingSource();
            _bsWMPartPrice.DataSource = _dtWMPartPrice;

            gcWMPartList.DataSource = _bsWMPartPrice;

            _bsLTPartPrice = new BindingSource();
            _bsLTPartPrice.DataSource = _dtLTPartPrice;

            gcLTPartList.DataSource = _bsLTPartPrice;

            initialize = true;
            initializeEnter = true;

        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {

            setInfoBox();

            //tePartCode.DataBindings.Add(new Binding("Text", _bsWMPartPrice, "PARTCODE", false, DataSourceUpdateMode.Never));
            //teCategory1.DataBindings.Add(new Binding("Text", _bsWMPartPrice, "PARTCAT1", false, DataSourceUpdateMode.Never));
            //teCategory2.DataBindings.Add(new Binding("Text", _bsWMPartPrice, "PARTCAT2", false, DataSourceUpdateMode.Never));
            //tePartName1.DataBindings.Add(new Binding("Text", _bsWMPartPrice, "PARTNAME", false, DataSourceUpdateMode.Never));
            //seMoney.DataBindings.Add(new Binding("EditValue", _bsWMPartPrice, "MONEY", false, DataSourceUpdateMode.Never));
            //sePartPriceWm.DataBindings.Add(new Binding("EditValue", _bsWMPartPrice, "PARTPRICEWM", false, DataSourceUpdateMode.Never));
            //rgDanawaFlag.DataBindings.Add(new Binding("EditValue", _bsWMPartPrice, "DANAWAFLAG", false, DataSourceUpdateMode.Never));

            if (!initialize)
                Dangol.ShowSplash();

            gvWMPartList.FocusedRowObjectChanged -= gvWMPartList_FocusedRowObjectChanged;
            getWMPartParicetList(true);
            postProcessing(true) ;
            gvWMPartList.FocusedRowObjectChanged += gvWMPartList_FocusedRowObjectChanged;
            if (_dtWMPartPrice.Rows.Count > 0)
            {
                gvWMPartList.FocusedRowHandle = -2147483646;
                gvWMPartList.MoveFirst();
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

            for (int i = 0; i < ConCSInfo._WMComponentCd.Length; i++)
            {
                DataRow dr = dtComponentCd.NewRow();

                dr["KEY"] = ConCSInfo._WMComponentCd[i];
                dr["VALUE"] = ConCSInfo._WMComponentCd[i];
                dtComponentCd.Rows.Add(dr);
            }
            Util.insertRowonTop(dtComponentCd, "-1", "전체");

            Util.LookupEditHelper(lecategory, dtComponentCd, "KEY", "VALUE");

            DataTable dtDanawa = new DataTable();
            //DataTable dtDanawa1 = new DataTable();

            dtDanawa.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtDanawa.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //dtDanawa1.Columns.Add(new DataColumn("KEY", typeof(string)));
            //dtDanawa1.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtDanawa, "N", "미조회");
            Util.insertRowonTop(dtDanawa, "Y", "조회");
            Util.insertRowonTop(dtDanawa, "-1", "전체");

            //Util.insertRowonTop(dtDanawa1, "N", "미조회");
            //Util.insertRowonTop(dtDanawa1, "Y", "조회");

            //Util.LookupEditHelper(leDanawaFlag, dtDanawa, "KEY", "VALUE");
            ////Util.LookupEditHelper(leDanawaFlag1, dtDanawa1, "KEY", "VALUE");

            lecategory.EditValue = "-1";
            //leDanawaFlag.EditValue = "Y";
        }

        private void setEditable(bool flag)
        {
            //teCategory.ReadOnly = !flag;
            //meRequest.ReadOnly = !flag;
            //meResponse.ReadOnly = flag;

        }
  
        private void gvWMPartList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWMPartList.RowCount > 0);

            if (isValidRow)
            {
                _currentWMPartPrice = e.Row as DataRowView;

                _partCode = ConvertUtil.ToString(_currentWMPartPrice["PARTCODE"]);


                if (ConvertUtil.ToInt32(_currentWMPartPrice["MAPPING_YN"]) == 1)
                {
                    getPartParicetList();
                }
                else
                {
                    _dtLTPartPrice.Clear();
                }
            }
            else
            {
                _partCode = "";
                _currentWMPartPrice = null;

                //teCategory2.ReadOnly = true;
                //meRequest.ReadOnly = true;
                //meResponse.ReadOnly = true;
            }
        }

        private void gvLTPartList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvLTPartList.RowCount > 0);

            if (isValidRow)
            {
                _currentLTPartPrice = e.Row as DataRowView;
            }
            else
            {
                _currentLTPartPrice = null;

                //teCategory2.ReadOnly = true;
                //meRequest.ReadOnly = true;
                //meResponse.ReadOnly = true;
            }
        }

        private void partPriceRefresh()
        {
            Dangol.ShowSplash();
            string partCode = _partCode;
            gvWMPartList.FocusedRowObjectChanged -= gvWMPartList_FocusedRowObjectChanged;
            getWMPartParicetList(false);
            postProcessing(true);
            gvWMPartList.FocusedRowObjectChanged += gvWMPartList_FocusedRowObjectChanged;
            int rowHandle = gvWMPartList.LocateByValue("PARTCODE", partCode);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvWMPartList.FocusedRowHandle = -2147483646;
                    gvWMPartList.FocusedRowHandle = rowHandle;
                }
            }
            else
            {
                if (_dtWMPartPrice.Rows.Count > 0)
                {
                    gvWMPartList.FocusedRowHandle = -2147483646;
                    gvWMPartList.MoveFirst();
                }
            }
            Dangol.CloseSplash();
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            partPriceRefresh();
        }

        private void postProcessing(bool refresh)
        {
            if(refresh)
                lcgWMPartList.CustomHeaderButtons[3].Properties.Checked = false;

            if(lcgWMPartList.CustomHeaderButtons[2].Properties.Checked)
            {
                _bsWMPartPrice.Filter = "1=1";
                gcVisible.Visible = true;
                lcgWMPartList.CustomHeaderButtons[1].Properties.Visible = true;
            }
            else
            {
                lcgWMPartList.CustomHeaderButtons[1].Properties.Visible = false;
                _bsWMPartPrice.Filter = "USE_YN = 1";
                gcVisible.Visible = false;
            }
        }

        private bool getWMPartParicetList(bool isinit)
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtWMPartPrice.Clear();

            if (DBPrice.searchWMPartPriceMapping(jData, ref jResult))
            {
                gvWMPartList.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                string partCodeWm;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtWMPartPrice.NewRow();

                    dr["NO"] = index++;
                    dr["PARTCAT1"] = obj["PARTCAT1"];
                    dr["PARTCAT2"] = obj["PARTCAT2"];
                    dr["PARTCAT3"] = obj["PARTCAT3"];

                    dr["PARTCODE"] = obj["PARTCODE"];
                    dr["PARTNAME"] = obj["PARTNAME"];

                    dr["MONEY"] = obj["MONEY"];
                    dr["MAPPING_YN"] = obj["MAPPING_YN"];
                    dr["PART_STATE"] = obj["PART_STATE"];
                    dr["USE_YN"] = obj["USE_YN"];
                    
                    dr["CHECK"] = false;
                    

                    _dtWMPartPrice.Rows.Add(dr);
                }

                gvWMPartList.EndDataUpdate();


                return true;

            }
            else
            {
                return false;
            }
        }

        private bool getPartParicetList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject(); 
                
            jData.Add("PARTCODEWM", _partCode);

            _dtLTPartPrice.Clear();

            if (DBPrice.getPartPriceMapping(jData, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["EXIST"]))
                {
                    gvLTPartList.BeginDataUpdate();

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtLTPartPrice.NewRow();

                        dr["NO"] = index++;
                        dr["SEQ"] = obj["SEQ"];
                        dr["PARTCAT1"] = obj["PARTCAT1"];
                        dr["PARTCAT2"] = obj["PARTCAT2"];

                        dr["PARTCODE"] = obj["PARTCODE"];
                        dr["PARTNAME"] = obj["PARTNAME"];

                        dr["MONEY"] = obj["MONEY"];


                        _dtLTPartPrice.Rows.Add(dr);
                    }

                    gvLTPartList.EndDataUpdate();

                    gvLTPartList.FocusedRowHandle = -2147483646;
                    gvLTPartList.FocusedRowHandle = 0;
                   
                }
                else
                {
                    _currentLTPartPrice = null;
                }


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
            else if(hour < 7)
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


            if (!ConvertUtil.ToString(lecategory.EditValue).Equals("-1"))
            {
                jData.Add("PARTTYPE1_NAME", ConvertUtil.ToString(lecategory.EditValue));
            }

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCategory2.Text)))
            {
                jData.Add("PARTTYPE2_NAME", teCategory2.Text);
            }
            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCategory3.Text)))
            {
                jData.Add("PARTTYPE3_NAME", teCategory3.Text);
            }

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(tePartName.Text)))
            {
                jData.Add("PARTNAME", tePartName.Text);
            }

            return true;
        }

        private void lcgWMPartList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWMPartList.FocusedRowHandle;
            int topRowIndex = gvWMPartList.TopRowIndex;
            gvWMPartList.FocusedRowHandle = 2147483646;
            gvWMPartList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtWMPartPrice.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("체크된 부품이 없습니다.");
                return;
            }


            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN($"체크하신 부품을 '숨김'처리 하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<string> listId = new List<string>();
                    string data;
                    foreach (DataRow row in rows)
                    {
                        data = ConvertUtil.ToString(row["PARTCODE"]);
                        if (!listId.Contains(data))
                        {
                            listId.Add(data);
                        }

                    }

                    jobj.Add("LIST_PARTCODE", string.Join(",", listId));
                    jobj.Add("USE_YN", 0);
                    jobj.Add("BULK_YN", 1);

                    if (DBPrice.updateWmPartInfo(jobj, ref jResult))
                    {
                        Dangol.ShowSplash();
                        gvWMPartList.BeginDataUpdate();
                        foreach (DataRow row in rows)
                        {
                            row["USE_YN"] = 0;
                        }
                        gvWMPartList.EndDataUpdate();

                        postProcessing(false);
                        Dangol.CloseSplash();

                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
            } 
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (Dangol.MessageYN($"체크하신 부품을 '보임'처리 하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<string> listId = new List<string>();
                    string data;
                    foreach (DataRow row in rows)
                    {
                        data = ConvertUtil.ToString(row["PARTCODE"]);
                        if (!listId.Contains(data))
                        {
                            listId.Add(data);
                        }

                    }

                    jobj.Add("LIST_PARTCODE", string.Join(",", listId));
                    jobj.Add("USE_YN", 1);
                    jobj.Add("BULK_YN", 1);

                    if (DBPrice.updateWmPartInfo(jobj, ref jResult))
                    {
                        Dangol.ShowSplash();
                        gvWMPartList.BeginDataUpdate();
                        foreach (DataRow row in rows)
                        {
                            row["USE_YN"] = 1;
                        }
                        gvWMPartList.EndDataUpdate();

                        postProcessing(false);
                        Dangol.CloseSplash();

                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
            }

        }

        private void layoutControlGroup2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                string partCategory = $"{ConvertUtil.ToString(_currentWMPartPrice["PARTCAT2"])} {ConvertUtil.ToString(_currentWMPartPrice["PARTCAT3"])}";

                string componentCd = ConvertUtil.ToString(_currentWMPartPrice["PARTCAT1"]);

                if (ConCSInfo._dicWMLTComponentCd.ContainsKey(componentCd))
                {
                    string LTcomponentCd = ConCSInfo._dicWMLTComponentCd[componentCd];

                    using (DlgCreateLTPart dlgCreateLTPart = new DlgCreateLTPart(
                       "",
                        ConvertUtil.ToString(_currentWMPartPrice["PARTNAME"]),
                        LTcomponentCd,
                        partCategory,
                        ConvertUtil.ToInt64(_currentWMPartPrice["MONEY"]),
                        1,
                        ConvertUtil.ToString(_currentWMPartPrice["PARTCODE"]),
                        ConvertUtil.ToString(_currentWMPartPrice["PARTNAME"]))
                    )
                    {
                        if (dlgCreateLTPart.ShowDialog(this) == DialogResult.OK)
                        {
                            if (dlgCreateLTPart._insertType == 1)
                            {
                                getPartParicetList();

                                gvWMPartList.BeginDataUpdate();
                                _currentWMPartPrice.BeginEdit();
                                _currentWMPartPrice["MAPPING_YN"] = 1;
                                _currentWMPartPrice.EndEdit();
                                gvWMPartList.EndDataUpdate();

                            }
                            Dangol.Message("처리되었습니다.");
                        }
                    }
                }

            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if(_currentLTPartPrice != null)
                { 
                    if (Dangol.MessageYN($"선택하신 부품 매핑 정보를 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();
                        JObject jData = new JObject();

                        jData.Add("PARTCODELTA", ConvertUtil.ToString(_currentLTPartPrice["PARTCODE"]));
                        jData.Add("PARTCODEWM", ConvertUtil.ToString(_currentWMPartPrice["PARTCODE"]));
                        //jData.Add("PARTNAMELTA", ConvertUtil.ToString(_currentPartPrice["PARTNAME"]));
                        //jData.Add("PARTNAMEWM", ConvertUtil.ToString(_currentPartPrice["PARTNAMEWM"]));
                        jData.Add("TYPE", 3);

                        if (DBPrice.updatePartPriceMapping(jData, ref jResult))
                        {
                            getPartParicetList();

                            if (_dtLTPartPrice.Rows.Count < 1)
                            {
                                gvWMPartList.BeginDataUpdate();
                                _currentWMPartPrice.BeginEdit();
                                _currentWMPartPrice["MAPPING_YN"] = 0;
                                _currentWMPartPrice.EndEdit();
                                gvWMPartList.EndDataUpdate();
                            }

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

        private void lcgWMPartList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                postProcessing(false);
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvWMPartList.FocusedRowHandle;
                int topRowIndex = gvWMPartList.TopRowIndex;
                gvWMPartList.FocusedRowHandle = -1;
                gvWMPartList.FocusedRowHandle = rowhandle;

                try
                {
                    gvWMPartList.BeginDataUpdate();
                    foreach (DataRow row in _dtWMPartPrice.Rows)
                    {
                        row.BeginEdit();
                        row["CHECK"] = false;
                        row.EndEdit();
                    }

                    ArrayList rows = new ArrayList();
                    for (int i = 0; i < gvWMPartList.DataRowCount; i++)
                    {
                        int rowHandle = gvWMPartList.GetVisibleRowHandle(i);
                        rows.Add(gvWMPartList.GetDataRow(rowHandle));
                    }

                    for (int i = 0; i < rows.Count; i++)
                    {
                        DataRow row = rows[i] as DataRow;
                        // Change the field value.
                        row["CHECK"] = true;
                    }
                }
                finally
                {
                    gvWMPartList.EndDataUpdate();
                }
            }
        }

        private void lcgWMPartList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                postProcessing(false);
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvWMPartList.FocusedRowHandle;
                int topRowIndex = gvWMPartList.TopRowIndex;
                gvWMPartList.FocusedRowHandle = -1;
                gvWMPartList.FocusedRowHandle = rowhandle;

                gvWMPartList.BeginDataUpdate();

                foreach (DataRow row in _dtWMPartPrice.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
                gvWMPartList.EndDataUpdate();
            }
        }

    }
}