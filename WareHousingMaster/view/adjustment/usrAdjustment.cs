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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace WareHousingMaster.view.adjustment
{
    public partial class usrAdjustment : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtWarehousing;
        DataTable _dt;
        DataTable _dtAdjustment;

        BindingSource _bsWarehousing;
        BindingSource _bs;

        DataRowView _currentRow = null;
  
        List<long> _listWarehousingId;

        List<long> _listInventoryId;

        JObject _jData;

        public bool _isAdjustmentReceipt { private set; get; }

        long _productCnt = 0;
        long _product_price = 0;
        long _adjustment_price = 0;
        long _purchasedPrice = 0;
        long _tax = 0;
        long _total_price = 0;


        Dictionary<long, Dictionary<string, long[]>> _dicAdjustment;
        
        long _passwordPrice = 0;
        long _dispalyPrice = 0;
        long _keyboard_price = 0;
        long _usbPrice = 0;
        long _lanWirelessPrice = 0;
        long _batteryPrice = 0;

        int _manufactureType;
        int _category;


        public usrAdjustment(JObject jData, List<long> listInventoryId)
        {
            InitializeComponent();
            _jData = jData;

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

            _dtAdjustment = new DataTable();
            _dtAdjustment.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dtAdjustment.Columns.Add(new DataColumn("NTB_PRICE_ID", typeof(long)));
            

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
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

            _dicAdjustment = new Dictionary<long, Dictionary<string, long[]>>();
            _bsWarehousing = new BindingSource();
            _bs = new BindingSource();

            //_bsWarehousing.DataSource = _dtWarehousing;
            //_bs.DataSource = _dt;

            _listWarehousingId = new List<long>();

            _listInventoryId = listInventoryId;

            _isAdjustmentReceipt = false;
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            setInfoBox();
            setIInitData();

            //JObject jResult = new JObject();
            //getWarehousingList(ref jResult);
            //getUsedPurchaseList(ref jResult);
        }

        private void setInfoBox()
        {
            //Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

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

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "해당없음");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");

            //DataTable dtWarehousingState = Util.getCodeList("CD0601", "KEY", "VALUE");
            //Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");

            //DataTable dtProductGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
            //Util.LookupEditHelper(rileProductGrade, dtProductGrade, "KEY", "VALUE");

            //DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            //Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            //Util.LookupEditHelper(rileComapnyId2, dtCompany, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            gcWarehousingList.DataSource = null;
            _bsWarehousing.DataSource = _dtWarehousing;
            gcWarehousingList.DataSource = _bsWarehousing;

            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;

            teNtbCnt.EditValue = ConvertUtil.ToInt64(_dt.Compute("Sum(PART_CNT)", "INVENTORY_ID > -1"));
            tePrice.EditValue = ConvertUtil.ToInt64(_dt.Compute("Sum(TOTAL_PURCHASE_PRICE)", "INVENTORY_ID > -1"));

            if (_jData.ContainsKey("COMPANY_ID"))
                leCompany.EditValue = ConvertUtil.ToString(_jData["COMPANY_ID"]);
            else
                leCompany.EditValue = "-1";
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

        private void getWarehousingStatistics()
        {
            if (Convert.ToBoolean(_jData["EXIST"]))
            {
                JArray jArray = JArray.Parse(_jData["DATA_STATISTICS"].ToString());
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtWarehousing.NewRow();

                    dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                    dr["WAREHOUSING"] = obj["WAREHOUSING"];

                    dr["WAREHOUSING_CNT"] = obj["WAREHOUSING_CNT"];
                    dr["ADJUSTMENT_COMPLETE_CNT"] = obj["ADJUSTMENT_COMPLETE_CNT"];
                    dr["ADJUSTMENT_REMAIN_CNT"] = ConvertUtil.ToInt64(obj["ADJUSTMENT_REMAIN_CNT"]) - ConvertUtil.ToInt64(obj["ADJUSTMENT_CNT"]);
                    dr["ADJUSTMENT_PROCESSING_CNT"] = obj["ADJUSTMENT_PROCESSING_CNT"];
                    
                    dr["ADJUSTMENT_CNT"] = obj["ADJUSTMENT_CNT"];                   
                    dr["TOTAL_CNT"] = ConvertUtil.ToInt32(dr["ADJUSTMENT_COMPLETE_CNT"]) + ConvertUtil.ToInt32(dr["ADJUSTMENT_REMAIN_CNT"]) 
                        + ConvertUtil.ToInt32(dr["ADJUSTMENT_PROCESSING_CNT"]) + ConvertUtil.ToInt32(dr["ADJUSTMENT_CNT"]);

                    dr["ADJUSTMENT_REPAIR_CNT"] = obj["ADJUSTMENT_REPAIR_CNT"];
                    dr["NON_NTB_CNT"] = obj["NON_NTB_CNT"];
                    dr["NON_BOM_CNT"] = obj["NON_BOM_CNT"];

                    _dtWarehousing.Rows.Add(dr);
                }
            }
        }

        public bool geList()
        {
            _dt.Clear();

            if (Convert.ToBoolean(_jData["EXIST"]))
            {
                long companyId = 1;
                JArray jArrayAdjustment = JArray.Parse(_jData["DATA_ADJUSTMENT"].ToString());

                foreach (JObject obj in jArrayAdjustment.Children<JObject>())
                {
                    long ntbListId = ConvertUtil.ToInt64(obj["NTB_LIST_ID"]);
                    companyId = ConvertUtil.ToInt64(obj["COMPANY_ID"]);
                    long ntbPriceId = ConvertUtil.ToInt64(obj["NTB_PRICE_ID"]);

                    long majorAdjustPrice = ConvertUtil.ToInt64(obj["LT_DEALER_PRICE"]);
                    long etcAdjustPrice = ConvertUtil.ToInt64(obj["LT_DEALER_PRICE_ETC"]);
                    string col = ConvertUtil.ToString(obj["COL_NM"]);

                    if (_dicAdjustment.ContainsKey(ntbPriceId))
                    {
                        Dictionary<string, long[]> data = _dicAdjustment[ntbPriceId];
                        long[] adjustprice = new long[] { 0, majorAdjustPrice, etcAdjustPrice };
                        data.Add(col, adjustprice);

                        _dicAdjustment[ntbPriceId] = data;
                    }
                    else
                    {
                        DataRow dr = _dtAdjustment.NewRow();
                        dr["NTB_LIST_ID"] = ntbListId;
                        dr["COMPANY_ID"] = companyId;
                        dr["NTB_PRICE_ID"] = ntbPriceId;
                        _dtAdjustment.Rows.Add(dr);

                        Dictionary<string, long[]> data = new Dictionary<string, long[]>();
                        long[] adjustprice = new long[] {0, majorAdjustPrice, etcAdjustPrice };
                        data.Add(col, adjustprice);

                        _dicAdjustment.Add(ntbPriceId, data);
                    }
                }

                JArray jArray = JArray.Parse(_jData["DATA"].ToString());

                long casePrice = 0;
                long passwordPrice = 0;
                long dispalyPrice = 0;
                long keyboardPrice = 0;
                long usbPrice = 0;
                long lanWirelessPrice = 0;
                long batteryPrice = 0;

                long totalAjustPrice = 0;
                long etcPrice = 0;

                long purchasePrice = 0;

                long adjustCase;
                long adjustDisplay;
                long adjustUsb;
                long adjustBattery;
                long adjustBios;
                long adjustKeyboard;
                long adjustLanWireless;

                companyId = 1;
                int manufactureType;
                DataRow[] rows;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                    DataRow dr = _dt.NewRow();

                    long ntbListId = ConvertUtil.ToInt64(obj["NTB_LIST_ID"]);
                    manufactureType = ConvertUtil.ToInt32(obj["MANUFACTURE_TYPE"]); 
                    rows = _dtAdjustment.Select($"NTB_LIST_ID = {ntbListId} AND COMPANY_ID = {companyId}");
                    if (rows.Length < 1)
                    {
                        adjustCase = 0;
                        adjustDisplay = 0;
                        adjustUsb = 0;
                        adjustBattery = 0;
                        adjustBios = 0;
                        adjustKeyboard = 0;
                        adjustLanWireless = 0;
                    }
                    else
                    {
                        long ntbPriceId = ConvertUtil.ToInt64(rows[0]["NTB_PRICE_ID"]);
                        Dictionary<string, long[]> data = _dicAdjustment[ntbPriceId];

                        adjustCase = data["CASES"][manufactureType];
                        adjustDisplay = data["DISPLAY"][manufactureType];
                        adjustUsb = data["USB"][manufactureType];
                        adjustBattery = data["BATTERY"][manufactureType];
                        adjustBios = data["BIOS"][manufactureType];
                        adjustKeyboard = data["KEYBOARD"][manufactureType];
                        adjustLanWireless = data["LAN_WIRELESS"][manufactureType];
                    }

                    casePrice = adjustCase * ConvertUtil.ToInt64(obj["CASES_CNT"]);
                    passwordPrice = adjustBios * ConvertUtil.ToInt64(obj["BIOS_CNT"]);
                    dispalyPrice = adjustDisplay * ConvertUtil.ToInt64(obj["DISPLAY_CNT"]);
                    keyboardPrice = adjustKeyboard * ConvertUtil.ToInt64(obj["KEYBOARD_CNT"]);
                    usbPrice = adjustUsb * ConvertUtil.ToInt64(obj["USB_CNT"]);
                    lanWirelessPrice = adjustLanWireless * ConvertUtil.ToInt64(obj["LAN_WIRELESS_CNT"]);
                    batteryPrice = adjustBattery * ConvertUtil.ToInt64(obj["BATTERY_CNT"]);
                    totalAjustPrice = casePrice + passwordPrice + dispalyPrice + keyboardPrice + usbPrice + lanWirelessPrice + batteryPrice;
                    etcPrice = ConvertUtil.ToInt64(obj["ETC_PRICE"]) + (ConvertUtil.ToInt64(obj["TOTAL_ADJUST_PRICE"]) - totalAjustPrice);
                    purchasePrice = ConvertUtil.ToInt64(obj["TOTAL_INIT_PRICE"]) + (ConvertUtil.ToInt64(obj["TOTAL_ADJUST_PRICE"]) + etcPrice);

                    if (etcPrice != 0)
                        etcPrice = etcPrice * 1;


                    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];

                    dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                    dr["CATEGORY"] = obj["CATEGORY"];
                    dr["NTB_LIST_ID"] = obj["NTB_LIST_ID"];

                    dr["INIT_PRICE"] = obj["INIT_PRICE"];
                    dr["PART_CNT"] = obj["PART_CNT"];
                    dr["TOTAL_INIT_PRICE"] = obj["TOTAL_INIT_PRICE"];

                    dr["CASES_ADJUST"] = adjustCase;
                    dr["CASES_CNT"] = obj["CASES_CNT"];
                    dr["BIOS_ADJUST"] = adjustBios;
                    dr["BIOS_CNT"] = obj["BIOS_CNT"];
                    dr["DISPLAY_ADJUST"] = adjustDisplay;
                    dr["DISPLAY_CNT"] = obj["DISPLAY_CNT"];

                    dr["KEYBOARD_ADJUST"] = adjustKeyboard;
                    dr["KEYBOARD_CNT"] = obj["KEYBOARD_CNT"];
                    dr["USB_ADJUST"] = adjustUsb;
                    dr["USB_CNT"] = obj["USB_CNT"];
                    dr["LAN_WIRELESS_ADJUST"] = adjustLanWireless;
                    dr["LAN_WIRELESS_CNT"] = obj["LAN_WIRELESS_CNT"];
                    dr["BATTERY_ADJUST"] = adjustBattery;
                    dr["BATTERY_CNT"] = obj["BATTERY_CNT"];

                    dr["ETC_PRICE"] = etcPrice;
                    dr["TOTAL_ADJUST_PRICE"] = ConvertUtil.ToInt64(obj["TOTAL_ADJUST_PRICE"]) + etcPrice;
                    dr["PURCHASE_PRICE"] = purchasePrice;
                    dr["TAX"] = Math.Round(purchasePrice * 0.1);
                    dr["TOTAL_PURCHASE_PRICE"] = Math.Round(purchasePrice * 1.1);



                    _productCnt += ConvertUtil.ToInt64(dr["PART_CNT"]);
                    _product_price += ConvertUtil.ToInt64(dr["TOTAL_INIT_PRICE"]);
                    _adjustment_price += ConvertUtil.ToInt64(dr["TOTAL_ADJUST_PRICE"]);
                    _purchasedPrice += ConvertUtil.ToInt64(dr["PURCHASE_PRICE"]);
                    _tax += ConvertUtil.ToInt64(dr["TAX"]);
                    _total_price += ConvertUtil.ToInt64(dr["TOTAL_PURCHASE_PRICE"]);


                    _dt.Rows.Add(dr);
                }

                getWarehousingStatistics();
            }

            return true;
        }

        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("STATE = 2");
                if (rows.Length < 1)
                {
                    Dangol.Message("변경사항이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("변경사항을 저장하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 0);

                    foreach (DataRow row in rows)
                    {
                        JObject jdata = new JObject();
                        jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        jdata.Add("INIT_PRICE", ConvertUtil.ToString(row["INIT_PRICE"]));
                        jdata.Add("ADJUST_PRICE", ConvertUtil.ToString(row["ADJUST_PRICE"]));
                        jdata.Add("MANUFACTURE_TYPE", ConvertUtil.ToString(row["MANUFACTURE_TYPE"]));
                        jdata.Add("NTB_LIST_ID", ConvertUtil.ToString(row["NTB_LIST_ID"]));
                        jArray.Add(jdata);
                    }

                    jobj.Add("DATA", jArray);

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["STATE"] = 0;
                        }

                        gvList.EndDataUpdate();
                        Dangol.CloseSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 제품을 승인하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();
                    
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_TYPE", ProjectInfo._userType);

                    if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    {
                        jobj.Add("ADJUSTMENT_STATE", 1);
                        jobj.Add("APPROVAL_USER2", ProjectInfo._userId);
                    }
                    else
                    {                    
                        jobj.Add("APPROVAL_USER1", ProjectInfo._userId);
                    }
                    List<long> listInventoryId = new List<long>();
                    foreach (DataRow row in rows)
                    {
                        listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            foreach (DataRow row in rows)
                            {
                                row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                row["ADJUSTMENT_STATE"] = 1;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in rows)
                                row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                        }
  
                        gvList.EndDataUpdate();
                        Dangol.CloseSplash();
                        Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
               




            }
            else if (e.Button.Properties.Tag.Equals(4))
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

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvWarehousingList.FocusedRowHandle;
            gvWarehousingList.FocusedRowHandle = -1;
            gvWarehousingList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtWarehousing.Select("CHECK = TRUE");


            if(rows.Length < 1)
            {
                Dangol.Message("입고번호를 선택하세요");
                return;
            }

            if (Dangol.MessageYN("선택하신 입고번호의 제품 정보를 확인하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            _listWarehousingId.Clear();

            foreach (DataRow row in rows)
                _listWarehousingId.Add(ConvertUtil.ToInt64(row["WAREHOUSING_ID"]));

            JObject jresult = new JObject();

        }

        //private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column.FieldName != "CHECK" )
        //    {
        //        int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

        //        if (state == 0)
        //            _currentRow["STATE"] = 2;
        //    }
            
        //}

        //private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        //{
        //    GridView View = sender as GridView;
        //    if (e.Column.FieldName == "BARCODE")
        //    {
        //        string state = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]);

        //        if (state.Equals("2"))
        //        {
        //            e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
        //        }
        //    }
        //}

        private void sbAdjustment_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("현재 데이터로 정산을 진행하시겠습니까?") == DialogResult.Yes)
            {

                var jArray = new JArray();
                JObject jobj = new JObject();

                foreach (DataRow row in _dt.Rows)
                {
                    JObject jdata = new JObject();
                    jdata.Add("MANUFACTURE_TYPE", ConvertUtil.ToInt32(row["MANUFACTURE_TYPE"]));
                    jdata.Add("NTB_LIST_ID", ConvertUtil.ToInt64(row["NTB_LIST_ID"]));
                    jdata.Add("CATEGORY", ConvertUtil.ToInt32(row["CATEGORY"]));
                    jdata.Add("INIT_PRICE", ConvertUtil.ToInt64(row["INIT_PRICE"]));
                    jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["PART_CNT"]));
                    jdata.Add("TOTAL_INIT_PRICE", ConvertUtil.ToInt64(row["TOTAL_INIT_PRICE"]));
                    jdata.Add("CASES_ADJUST", ConvertUtil.ToInt64(row["CASES_ADJUST"]));
                    jdata.Add("CASES_CNT", ConvertUtil.ToInt32(row["CASES_CNT"]));
                    jdata.Add("BIOS_CNT", ConvertUtil.ToInt32(row["BIOS_CNT"]));
                    jdata.Add("BIOS_ADJUST", ConvertUtil.ToInt64(row["BIOS_ADJUST"]));
                    jdata.Add("DISPLAY_ADJUST", ConvertUtil.ToInt64(row["DISPLAY_ADJUST"]));
                    jdata.Add("DISPLAY_CNT", ConvertUtil.ToInt32(row["DISPLAY_CNT"]));
                    jdata.Add("KEYBOARD_ADJUST", ConvertUtil.ToInt64(row["KEYBOARD_ADJUST"]));
                    jdata.Add("KEYBOARD_CNT", ConvertUtil.ToInt32(row["KEYBOARD_CNT"]));
                    jdata.Add("USB_ADJUST", ConvertUtil.ToInt64(row["USB_ADJUST"]));
                    jdata.Add("USB_CNT", ConvertUtil.ToInt32(row["USB_CNT"]));
                    jdata.Add("LAN_WIRELESS_ADJUST", ConvertUtil.ToInt64(row["LAN_WIRELESS_ADJUST"]));
                    jdata.Add("LAN_WIRELESS_CNT", ConvertUtil.ToInt32(row["LAN_WIRELESS_CNT"]));
                    jdata.Add("BATTERY_ADJUST", ConvertUtil.ToInt64(row["BATTERY_ADJUST"]));
                    jdata.Add("BATTERY_CNT", ConvertUtil.ToInt32(row["BATTERY_CNT"]));
                    jdata.Add("ETC_PRICE", ConvertUtil.ToInt64(row["ETC_PRICE"]));
                    jdata.Add("TOTAL_ADJUST_PRICE", ConvertUtil.ToInt64(row["TOTAL_ADJUST_PRICE"]));
                    jdata.Add("PURCHASE_PRICE", ConvertUtil.ToInt64(row["PURCHASE_PRICE"]));
                    jdata.Add("TAX", ConvertUtil.ToInt64(row["TAX"]));
                    jdata.Add("TOTAL_PURCHASE_PRICE", ConvertUtil.ToInt64(row["TOTAL_PURCHASE_PRICE"]));
                    jArray.Add(jdata);
                }

                jobj.Add("DATA", jArray);
                jobj.Add("COMPANY_ID", ConvertUtil.ToInt64(leCompany.EditValue));
                jobj.Add("PRODUCT_CNT", _productCnt);
                jobj.Add("PRODUCT_PRICE", _product_price);
                jobj.Add("ADJUSTMENT_PRICE", _adjustment_price);
                jobj.Add("PURCHASED_PRICE", _purchasedPrice);
                jobj.Add("TAX", _tax);
                jobj.Add("TOTAL_PRICE", _total_price);
                jobj.Add("LIST_INVENTORY_ID", string.Join(",", _listInventoryId));


                JObject jResult = new JObject();

                if (DBAdjustment.makeAdjustment(jobj, ref jResult))
                {
                    _isAdjustmentReceipt = true;
                    Dangol.Message("정산 리스트가 생성되었습니다.");
                }
            }
        }

        private void gvList_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
        {
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

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}