using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.price.partPriceMapping
{
    public partial class DlgGetWmPartList : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtWMPartPrice;
        BindingSource _bsWMPartPrice;
        string _receipt;
        string _componentCd;

        List<string> _listUsedPart;

        string _partCdWm;
        string _partCd;
        string _partName;
        int _type;

        public DataRowView _currentRow { get; private set; }

        public DlgGetWmPartList(string partCd, string partCdWm, string partName, string componentCd, int type)
        {
            InitializeComponent();
            _componentCd = componentCd;
            _partCdWm = partCdWm;
            _partCd = partCd;
            _type = type;
            _partName = partName;

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

            _bsWMPartPrice = new BindingSource();

            _bsWMPartPrice.DataSource = _dtWMPartPrice;

        }
        private void dlgGetPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            gcWMPartList.DataSource = null;
            gcWMPartList.DataSource = _bsWMPartPrice;

            DataTable dtWMComponent = new DataTable();

            dtWMComponent.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtWMComponent.Columns.Add(new DataColumn("VALUE", typeof(string)));

            DataRow row = dtWMComponent.NewRow();
            row["KEY"] = "-1";
            row["VALUE"] = "선택";
            dtWMComponent.Rows.Add(row);

            for (int i = 0; i < ConCSInfo._WMComponentCd.Length; i++)
            {
                DataRow dr = dtWMComponent.NewRow();
                dr["KEY"] = ConCSInfo._WMComponentCd[i];
                dr["VALUE"] = ConCSInfo._WMComponentCd[i];
                dtWMComponent.Rows.Add(dr);
            }

            Util.LookupEditHelper(leComponentCd, dtWMComponent, "KEY", "VALUE");

            leComponentCd.ItemIndex = 0;

            Dangol.ShowSplash();
            setTable(_componentCd);
            Dangol.CloseSplash();
        }

        private void setTable(string componentCd)
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

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


            _dtWMPartPrice.Clear();
            jData.Add("PARTTYPE1_NAME", componentCd);

            if (DBPrice.searchWMPartPriceMapping(jData, ref jResult))
            {

                if (ConvertUtil.ToBoolean(jResult["EXIST"]))
                {

                    gvWMPartList.BeginDataUpdate();

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
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

                        _dtWMPartPrice.Rows.Add(dr);
                    }

                    gvWMPartList.EndDataUpdate();
                }

                //if (!_filterString.Equals("-1"))
                //{
                //    string filterString = $"Contains([MODEL_NM], '{_filterString}')";
                //    gvWMPartList.Columns["MODEL_NM"].FilterInfo = new ColumnFilterInfo(filterString);
                //}
            }         
            
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
            if (componentCd.Equals("-1"))
            {
                Dangol.Message("품목을 선택하세요");
                return;
            }

            Dangol.ShowSplash();
            setTable(componentCd);
            Dangol.CloseSplash();
        }

        private void sbInsert_Click(object sender, EventArgs e)
        {
            

            if (_currentRow == null)
            {
                Dangol.Message("부품을 선택해주세요.");
                return;
            }

            if(ConvertUtil.ToString(_currentRow["PARTCODE"]).Equals(_partCdWm))
            {
                Dangol.Message("이미 매핑된 부품입니다.");
                return;
            }

            if (Dangol.MessageYN($"선택하신 W사 부품으로 매핑을 적용하시겠습니까?") == DialogResult.Yes)
            {

                JObject jResult = new JObject();
                JObject jData = new JObject();

                jData.Add("PARTCODELTA", _partCd);
                jData.Add("PARTCODEWM", ConvertUtil.ToString(_currentRow["PARTCODE"]));
                jData.Add("PARTNAMELTA", _partName);
                jData.Add("PARTNAMEWM", ConvertUtil.ToString(_currentRow["PARTNAME"]));
                jData.Add("TYPE", _type);
                
                if (DBPrice.updatePartPriceMapping(jData, ref jResult))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    this.DialogResult = DialogResult.Cancel;
                }

            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvWMPartList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWMPartList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }

        private void gvWMPartList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvWMPartList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvWMPartList.GetRow(e.FocusedRowHandle) as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }
    }
}