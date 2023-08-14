using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WareHousingMaster.view.common;
using Newtonsoft.Json.Linq;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using WareHousingMaster.view.adjustment;
using WareHousingMaster.view.inventory;

namespace WareHousingMaster.view.consigned
{
    public partial class DlgCompanyPartStatistice : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtPart;
        BindingSource _bs;

        DataRowView _currentRow;
        long _companyId;
        JObject _jobj;

        public DlgCompanyPartStatistice(long companyId)
        {
            InitializeComponent();

            _dtPart = new DataTable();
            _dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSiNG_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));

            _dtPart.Columns.Add(new DataColumn("WAREHOUSING_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("TOTAL_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("GOOD_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("FAULT_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("RELEASE_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("LOCK_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("PART_RELEASE_PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _companyId = companyId;

            _bs = new BindingSource();
            _jobj = new JObject();
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            Dangol.ShowSplash();
            getCompanyInventoryList();
            Dangol.CloseSplash();

        }

        private void setInfoBox()
        {
            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr1 = dtComponentCd.NewRow();
                dr1["KEY"] = ProjectInfo._componetCd[i];
                dr1["VALUE"] = ProjectInfo._componetNm[i];
                dtComponentCd.Rows.Add(dr1);
            }

            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            var today = DateTime.Today;
            var pastDate = today.AddDays(0);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
        }

        private void setIInitData()
        {
            gcPart.DataSource = null;
            _bs.DataSource = _dtPart;
            gcPart.DataSource = _bs;
        }

        private void sbCreate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvPart_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (ConvertUtil.ToInt32(gvPart.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                //if (ConvertUtil.ToBoolean(_currentRow["ASSIGN_YN_O"]))
                //    gcAssign.OptionsColumn.AllowEdit = true;
                //else
                //    gcAssign.OptionsColumn.AllowEdit = false;
            }
            else
            {
                _currentRow = null;
                //gcAssign.OptionsColumn.AllowEdit = false;
            }
        }

        private void gvPart_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.Column.FieldName == "ASSIGN_YN")
            //{
            //    bool assignYnO = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN_O"]));
            //    bool assignYn = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN"]));
            //    bool check = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["CHECK"]));

            //    if (check && assignYnO && !assignYn)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
            //    }
            //    else
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Transparent);
            //}
        }


        private bool getCompanyInventoryList()
        {
            JObject jResult = new JObject();
            _jobj.RemoveAll();

            if (!checkSearch(ref _jobj))
            {
                Dangol.Message(_jobj["MSG"]);
                return false;
            }

            _jobj.Add("COMPANY_ID", _companyId);
            _jobj.Add("TYPE", 0);

            _dtPart.Clear();

            if (DBConsigned.getCompanyInventoryUsedList(_jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;

                    foreach (JObject jData in jArray.Children<JObject>())
                    {
                        DataRow dr1 = _dtPart.NewRow();
                        dr1["NO"] = index++;
                        dr1["COMPONENT_CD"] = jData["COMPONENT_CD"];
                        dr1["MODEL_NM"] = jData["MODEL_NM"];

                        dr1["RELEASE_CNT"] = jData["RELEASE_CNT"];
                        dr1["TOTAL_PRICE"] = jData["TOTAL_PRICE"];
                        dr1["COMPONENT_ID"] = jData["COMPONENT_ID"];
                        

                        _dtPart.Rows.Add(dr1);
                    }
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

            string dtFrom = "";
            string dtTo = "";

            int date = 0;
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
            {
                dtFrom = $"{deDtFrom.Text} 00:00:00";
                date++;
            }

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
            {
                dtTo = $"{deDtTo.Text} 23:59:59";
                date++;
            }

            if (date == 2)
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

                if (diffDay > 180)
                {
                    jData.Add("MSG", "검색 기간은 6개월(180일)을 초과할 수 없습니다.");
                    return false;
                }

                jData.Add("RECEIPT_DT_S", dtFrom);
                jData.Add("RECEIPT_DT_E", dtTo);
            }
            else
            {
                date = 0;
            }


            return true;
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            getCompanyInventoryList();
            Dangol.CloseSplash();
        }

        private void gvPart_DoubleClick(object sender, EventArgs e)
        {
            if (_currentRow != null)
            {
                _jobj.Remove("COMPONENT_ID");
                _jobj.Add("COMPONENT_ID", ConvertUtil.ToInt64(_currentRow["COMPONENT_ID"]));

                using (DlgInventoryListByWarehousingShort dlgInventoryReturnListByWarehousing = new DlgInventoryListByWarehousingShort(_jobj, ConvertUtil.ToString(_currentRow["COMPONENT_CD"])))
                {
                    //Dangol.setDlgPositionCenter(dlgInventoryListByWarehousing, this);

                    dlgInventoryReturnListByWarehousing.StartPosition = FormStartPosition.Manual;
                    dlgInventoryReturnListByWarehousing.Location = new Point(this.Location.X + (this.Size.Width / 2) - (dlgInventoryReturnListByWarehousing.Size.Width / 2),
                    this.Location.Y + (this.Size.Height / 2) - (dlgInventoryReturnListByWarehousing.Size.Height / 2));

                    dlgInventoryReturnListByWarehousing.ShowDialog(this);
                }
            }
        }
    }
}