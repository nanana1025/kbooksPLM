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

namespace WareHousingMaster.view.adjustment
{
    public partial class usrAdjustmentStatistics : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtWarehousing;
        DataTable _dt;

        BindingSource _bsWarehousing;
        BindingSource _bs;

        DataRowView _currentRow = null;
  
        List<long> _listWarehousingId;
        List<long> _listInventoryId;

        long _adjustmentId = 0;

        string _adjustmentState;
        int _manufactureType;
        int _category;


        public usrAdjustmentStatistics(long adjustmentId, string adjustmentState, List<long> listWarehousingId, List<long> listInventoryId)
        {
            InitializeComponent();
            _adjustmentId = adjustmentId;
            _adjustmentState = adjustmentState;
            _listWarehousingId = listWarehousingId;
            _listInventoryId = listInventoryId;

            _dtWarehousing = new DataTable();
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtWarehousing.Columns.Add(new DataColumn("ADJUSTMENT_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("ADJUSTMENT_COMPLETE_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("ADJUSTMENT_PROCESSING_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("ADJUSTMENT_REMAIN_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("TOTAL_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("WAREHOUSING_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("ADJUSTMENT_REPAIR_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("NON_NTB_CNT", typeof(int)));
            _dtWarehousing.Columns.Add(new DataColumn("NON_BOM_CNT", typeof(int)));           

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            _dt.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("INIT_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("PART_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("TOTAL_INIT_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("CASES_ADJUST", typeof(long)));
            _dt.Columns.Add(new DataColumn("CASES_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("BIOS_ADJUST", typeof(long)));
            _dt.Columns.Add(new DataColumn("BIOS_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("DISPLAY_ADJUST", typeof(long)));         
            _dt.Columns.Add(new DataColumn("DISPLAY_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("KEYBOARD_ADJUST", typeof(long)));
            _dt.Columns.Add(new DataColumn("KEYBOARD_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("USB_ADJUST", typeof(long)));
            _dt.Columns.Add(new DataColumn("USB_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("LAN_WIRELESS_ADJUST", typeof(long)));
            _dt.Columns.Add(new DataColumn("LAN_WIRELESS_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("BATTERY_ADJUST", typeof(long))); 
            _dt.Columns.Add(new DataColumn("BATTERY_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("ETC_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("TOTAL_ADJUST_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("PURCHASE_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("TAX", typeof(long)));
            _dt.Columns.Add(new DataColumn("TOTAL_PURCHASE_PRICE", typeof(long)));

            _bsWarehousing = new BindingSource();
            _bs = new BindingSource();

            if (adjustmentState.Equals("3"))
                gcCurrent.Visible = false;

        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            setInfoBox();
            setIInitData();
            getAdjustmentStatics();

        }

        private void setInfoBox()
        {
            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            DataTable dtCategory = Util.getCodeList("CD1101", "KEY", "VALUE");
            Util.LookupEditHelper(rileCategory, dtCategory, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(rileNickName, dtNickName, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            gcWarehousingList.DataSource = null;
            _bsWarehousing.DataSource = _dtWarehousing;
            gcWarehousingList.DataSource = _bsWarehousing;

            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;
        }




        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _manufactureType = ConvertUtil.ToInt32(_currentRow["MANUFACTURE_TYPE"]);
                _category = ConvertUtil.ToInt32(_currentRow["CATEGORY"]);
            }
            else
            {
                _currentRow = null;
                _manufactureType = -1;
                _category = -1;
            }
        }

        private void getAdjustmentStatics()
        {
            JObject jobj = new JObject();
            JObject jResult = new JObject();

            jobj.Add("ADJUSTMENT_ID", _adjustmentId);
            jobj.Add("LIST_INVENTORY_ID", string.Join(",", _listInventoryId));
            jobj.Add("LIST_WAREHOUSING_ID", string.Join(",", _listWarehousingId));


            if (DBAdjustment.getAdjustStatistics(jobj, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    getWarehousingStatistics(jResult);
                    geList(jResult);
                }
            }
        }

        private void getWarehousingStatistics(JObject jResult)
        {
            if (Convert.ToBoolean(jResult["EXIST"]))
            {
                JArray jArray = JArray.Parse(jResult["DATA_STATISTICS"].ToString());
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtWarehousing.NewRow();

                    dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                    dr["WAREHOUSING"] = obj["WAREHOUSING"];
                    dr["WAREHOUSING_CNT"] = obj["WAREHOUSING_CNT"];
                    dr["ADJUSTMENT_COMPLETE_CNT"] = obj["ADJUSTMENT_COMPLETE_CNT"];
                    dr["ADJUSTMENT_REMAIN_CNT"] = obj["ADJUSTMENT_REMAIN_CNT"];
                    if (_adjustmentState.Equals("3"))
                    {                     
                        dr["ADJUSTMENT_PROCESSING_CNT"] = ConvertUtil.ToInt64(obj["ADJUSTMENT_PROCESSING_CNT"]);
                        dr["ADJUSTMENT_CNT"] = 0;
                    }
                    else
                    {
                        dr["ADJUSTMENT_PROCESSING_CNT"] = ConvertUtil.ToInt64(obj["ADJUSTMENT_PROCESSING_CNT"]) - _listInventoryId.Count;
                        dr["ADJUSTMENT_CNT"] = _listInventoryId.Count;
                    }

                    dr["TOTAL_CNT"] = ConvertUtil.ToInt32(dr["ADJUSTMENT_COMPLETE_CNT"]) + ConvertUtil.ToInt32(dr["ADJUSTMENT_REMAIN_CNT"])
                        + ConvertUtil.ToInt32(dr["ADJUSTMENT_PROCESSING_CNT"]) + ConvertUtil.ToInt32(dr["ADJUSTMENT_CNT"]);


                    dr["ADJUSTMENT_REPAIR_CNT"] = obj["ADJUSTMENT_REPAIR_CNT"];
                    dr["NON_NTB_CNT"] = obj["NON_NTB_CNT"];
                    dr["NON_BOM_CNT"] = obj["NON_BOM_CNT"];

                    _dtWarehousing.Rows.Add(dr);
                }
            }
        }

        public void geList(JObject jResult)
        {
            _dt.Clear();

            if (Convert.ToBoolean(jResult["EXIST"]))
            {
                JArray jArrayAdjustment = JArray.Parse(jResult["DATA"].ToString());

                foreach (JObject obj in jArrayAdjustment.Children<JObject>())
                {
                    DataRow dr = _dt.NewRow();

                    dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                    dr["CATEGORY"] = obj["CATEGORY"];
                    dr["NTB_LIST_ID"] = obj["NTB_LIST_ID"];
                    dr["INIT_PRICE"] = obj["INIT_PRICE"];
                    dr["PART_CNT"] = obj["PART_CNT"];
                    dr["TOTAL_INIT_PRICE"] = obj["TOTAL_INIT_PRICE"];
                    dr["CASES_ADJUST"] = obj["CASES_ADJUST"];
                    dr["CASES_CNT"] = obj["CASES_CNT"];
                    dr["BIOS_ADJUST"] = obj["BIOS_ADJUST"];
                    dr["BIOS_CNT"] = obj["BIOS_CNT"];

                    dr["DISPLAY_ADJUST"] = obj["DISPLAY_ADJUST"];
                    dr["DISPLAY_CNT"] = obj["DISPLAY_CNT"];
                    dr["KEYBOARD_ADJUST"] = obj["KEYBOARD_ADJUST"];
                    dr["KEYBOARD_CNT"] = obj["KEYBOARD_CNT"];
                    dr["USB_ADJUST"] = obj["USB_ADJUST"];
                    dr["USB_CNT"] = obj["USB_CNT"];

                    dr["LAN_WIRELESS_ADJUST"] = obj["LAN_WIRELESS_ADJUST"];
                    dr["LAN_WIRELESS_CNT"] = obj["LAN_WIRELESS_CNT"];
                    dr["BATTERY_ADJUST"] = obj["BATTERY_ADJUST"];
                    dr["ETC_PRICE"] = obj["ETC_PRICE"];

                    dr["TOTAL_ADJUST_PRICE"] = obj["TOTAL_ADJUST_PRICE"];
                    dr["PURCHASE_PRICE"] = obj["PURCHASE_PRICE"];
                    dr["TAX"] = obj["TAX"];
                    dr["TOTAL_PURCHASE_PRICE"] = obj["TOTAL_PURCHASE_PRICE"];

                    _dt.Rows.Add(dr);
                }
            }
        }

        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(4))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcList.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
        }

        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -1;
            gvList.FocusedRowHandle = rowhandle;

            gvList.BeginDataUpdate();
            foreach (DataRow row in _dt.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = true;
                row.EndEdit();
            }
            gvList.EndDataUpdate();
        }

        private void lcgBarcodeList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -1;
            gvList.FocusedRowHandle = rowhandle;

            gvList.BeginDataUpdate();

            foreach (DataRow row in _dt.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvList.EndDataUpdate();
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gvList.RowCount > 0 && e.RowHandle > -1)
            {
                DataRow dr = gvList.GetDataRow(e.RowHandle);
                Color backColor = Color.LightGray;

                if (gvList.FocusedRowHandle == e.RowHandle)
                {

                    e.Appearance.BackColor = backColor;
                }

                int manufactureType = ConvertUtil.ToInt32(dr["MANUFACTURE_TYPE"]);
                int categoty = ConvertUtil.ToInt32(dr["CATEGORY"]);

                if (e.Column.FieldName == "MANUFACTURE_TYPE" && manufactureType == _manufactureType)
                {
                    e.Appearance.BackColor = backColor;
                }

                if (e.Column.FieldName == "CATEGORY" && categoty == _category && manufactureType == _manufactureType)
                {
                    e.Appearance.BackColor = backColor;
                }


            }
        }
    }
}