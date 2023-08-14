using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.release.External
{
    public partial class usrReleaseOrderList : DevExpress.XtraEditors.XtraForm
    {
        int _viewCategory = (int)Enum.EnumView.Adjustment;
        int _viewType = (int)Enum.EnumTableAdjustment.ExamList;

        DataTable _dtOrder;
        DataTable _dt;
        DataTable _dtPart;


        BindingSource _baOrder;
        BindingSource _bs;
        BindingSource _bsPart;

        DataRowView _currentRow = null;
        //DataRowView _currentOrderRow = null;


        //List<long> _listExportId;

        Dictionary<string, string> _dicReleaseState;
        List<string> _listReleaseBarcode;
  

        long _orderPartId;
        string _state;
        long _orderId;
        long _companyId;
        string _barcode;
        string _order;

        Dictionary<string, string> _dicMonType;

        DataTable _dtSpareProduct;

        DataTable _dtSpareParts;

        Regex regex1;
        Regex regex2;
        Regex regex3;
        Regex regex4;

        bool initialize = true;


        public usrReleaseOrderList()
        {
            InitializeComponent();

            _dtOrder = new DataTable();
            _dtOrder.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtOrder.Columns.Add(new DataColumn("ORDER_ID", typeof(long)));
            _dtOrder.Columns.Add(new DataColumn("ORDERS", typeof(string)));
            _dtOrder.Columns.Add(new DataColumn("ORDER_DT", typeof(string)));
            _dtOrder.Columns.Add(new DataColumn("QTY", typeof(int)));
            _dtOrder.Columns.Add(new DataColumn("ORDER_STATE", typeof(string)));

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            
            _dt.Columns.Add(new DataColumn("ORDER_PART_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("EXPORT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("ORDER_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("ORDERS", typeof(string)));
            _dt.Columns.Add(new DataColumn("ORDER_DETAIL", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("PART_STATE", typeof(string)));

            _dt.Columns.Add(new DataColumn("PART_RELEASE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("ORDER_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("ARRIVAL_DT", typeof(object)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(string)));
            //_dt.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("MANUFACTURER", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("DETAIL", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL1", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL2", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL3", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL4", typeof(string)));
            _dt.Columns.Add(new DataColumn("COL5", typeof(string)));
            _dt.Columns.Add(new DataColumn("CPU_INFO", typeof(string)));
            _dt.Columns.Add(new DataColumn("MON_SIZE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MEM_INFO", typeof(string)));
            _dt.Columns.Add(new DataColumn("SSD_INFO", typeof(string)));
            _dt.Columns.Add(new DataColumn("HDD_INFO", typeof(string)));
            _dt.Columns.Add(new DataColumn("DEFECTIVE", typeof(string)));
            _dt.Columns.Add(new DataColumn("QTY", typeof(int)));
            _dt.Columns.Add(new DataColumn("RELEASE_QTY", typeof(int)));
            
            _dt.Columns.Add(new DataColumn("SPARE_PARTS", typeof(string)));
            _dt.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dt.Columns.Add(new DataColumn("NEW", typeof(int)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING_YN", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _dtPart = new DataTable();
            _dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("ORDER_EXPORT_PART_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("CNT", typeof(int)));
            //_dtPart.Columns.Add(new DataColumn("ORDER_STATE", typeof(string)));


            _baOrder = new BindingSource();
            _bs = new BindingSource();
            _bsPart = new BindingSource();


            _listReleaseBarcode = new List<string>();

            _dicReleaseState = new Dictionary<string, string>();

            _dicMonType = new Dictionary<string, string>();
            //_listExportId = new List<long>();

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

            regex4 = new Regex(@"^E[0-9]{9}$");

            //if (this.Width < 1569)
            //{
            //    lcSearch.Size = new Size(lcSearch.Size.Width, 54);
            //    lcSearch.MaxSize = new Size(0, 54);
            //}
            //else
            //{
            //    lcSearch.Size = new Size(lcSearch.Size.Width, 30);
            //    lcSearch.MaxSize = new Size(0, 30);
            //}

        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            //getVisibleCol();
            //setGridViewColumsWarehousing();
            //setGridViewColums();

            rgProcess.DataBindings.Add(new Binding("EditValue", _bs, "PART_STATE", false, DataSourceUpdateMode.OnPropertyChanged));

            geList();
            setStatistics();
            //getUsedPurchaseList(ref jResult);

            initialize = false;

            //teExport.Text = "E210715002";
            //teBarcode.Text = "LT2106000023";
        }

        private void setInfoBox()
        {

            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "SAMSUNG/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "ETC");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr = dtComponentCd.NewRow();
                dr["KEY"] = ProjectInfo._componetCd[i];
                dr["VALUE"] = ProjectInfo._componetCd[i];
                dtComponentCd.Rows.Add(dr);
            }
            Util.insertRowonTop(dtComponentCd, "NTB", "NTB");
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd1, dtComponentCd, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "SELECT ALL");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany1, dtCompany, "KEY", "VALUE");

            DataTable dtOrderState = Util.getCodeList("CD082001", "KEY", "VALUE");
            //Util.LookupEditHelper(rileOrderState, dtOrderState, "KEY", "VALUE");
            Util.LookupEditHelper(rileOrderState1, dtOrderState, "KEY", "VALUE");


            DataTable dtOrderState1 = new DataTable();

            dtOrderState1.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtOrderState1.Columns.Add(new DataColumn("VALUE", typeof(string)));


            RadioGroupItem[] rgProcessState = new RadioGroupItem[dtOrderState.Rows.Count];
            int indexProcess = 0;
            string value;
            foreach (DataRow row in dtOrderState.Rows)
            {
                //if (!ConvertUtil.ToString(row["KEY"]).Equals("D"))
                {
                    DataRow dr = dtOrderState1.NewRow();
                    dr["KEY"] = row["KEY"];
                    dr["VALUE"] = row["VALUE"];
                    dtOrderState1.Rows.Add(dr);

                    value = ConvertUtil.ToString(row["KEY"]);

                    RadioGroupItem rgItem = new RadioGroupItem(row["KEY"], ConvertUtil.ToString(row["VALUE"]), true, row["KEY"]);
                    _dicReleaseState.Add(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));

                    if (ProjectInfo._userCompanyId == 2)
                    {
                        if (value.Equals("E"))
                            rgItem.Enabled = false;
                    }
                    else
                    {
                        if (value.Equals("P") || value.Equals("R") || value.Equals("E"))
                            rgItem.Enabled = false;
                        else
                            rgItem.Enabled = true;
                    }

                    rgProcessState[indexProcess++] = rgItem;
                }
            }

            this.rgProcess.Properties.Items.AddRange(rgProcessState);

            Util.insertRowonTop(dtOrderState1, "-1", "SELECT ALL");

            Util.LookupEditHelper(leOrderState, dtOrderState, "KEY", "VALUE");

            //DataTable dtSpareParts = Util.getCodeList("CD0821", "KEY", "VALUE");
            //Util.insertRowonTop(dtSpareParts, "0", "N/A");
            //Util.LookupEditHelper(rileSpareParts, dtSpareParts, "KEY", "VALUE");

            _dtSpareProduct = Util.getCodeList("CD082101", "KEY", "VALUE");
            _dtSpareParts = Util.getCodeList("CD08210201", "KEY", "VALUE");

            DataTable dtSpare = new DataTable();
            dtSpare.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtSpare.Columns.Add(new DataColumn("VALUE", typeof(string)));

            foreach (DataRow row in _dtSpareProduct.Rows)
            {
                DataRow dr = dtSpare.NewRow();
                dr["KEY"] = row["KEY"];
                dr["VALUE"] = row["VALUE"];
                dtSpare.Rows.Add(dr);
            }

            foreach (DataRow row in _dtSpareParts.Rows)
            {
                DataRow dr = dtSpare.NewRow();
                dr["KEY"] = row["KEY"];
                dr["VALUE"] = row["VALUE"];
                dtSpare.Rows.Add(dr);

            }

            Util.LookupEditHelper(rileSpareParts, dtSpare, "KEY", "VALUE");


            DataTable dtPayment = new DataTable();
            dtPayment.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtPayment.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtPayment, 2, "후");
            Util.insertRowonTop(dtPayment, 1, "선");
            Util.insertRowonTop(dtPayment, -1, "해당없음");

            Util.LookupEditHelper(lePayment, dtPayment, "KEY", "VALUE");

            DataTable dtTaxInvoice = new DataTable();
            dtTaxInvoice.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtTaxInvoice.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtTaxInvoice, 1, "현금영수증");
            Util.insertRowonTop(dtTaxInvoice, 2, "미발행");
            Util.insertRowonTop(dtTaxInvoice, 1, "발행");
            Util.insertRowonTop(dtTaxInvoice, -1, "해당없음");

            Util.LookupEditHelper(leTaxInvoice, dtTaxInvoice, "KEY", "VALUE");

            DataTable dtMonType = Util.getCodeList("CD03011301", "KEY", "VALUE");
            foreach (DataRow row in dtMonType.Rows)
                _dicMonType.Add(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));
        }

        private void setIInitData()
        {
            //gvList.AutoGenerateColumns = false;

            //gcOrder.DataSource = null;
            //_baOrder.DataSource = _dtOrder;
            //gcOrder.DataSource = _baOrder;

            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;

            gcPart.DataSource = null;
            _bsPart.DataSource = _dtPart;
            gcPart.DataSource = _bsPart;

            //gcDetail.DataSource = null;
            //_bsDetail.DataSource = _dtDetail;
            //gcDetail.DataSource = _bsDetail;

            //rgDt.EditValue = 1;

            

            var today = DateTime.Today;
            var pastDate = today.AddDays(-30);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            //deArrivalDt.EditValue = today;

            leOrderState.EditValue = "-1";

            if (ProjectInfo._userCompanyId == 2)
            {
                leCompany.EditValue = "-1";
                lcExport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                gcCompany.Visible = true;
            }
            else
            {
                leCompany.EditValue = ConvertUtil.ToString(ProjectInfo._userCompanyId);
                lcExport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lgcOrderPart.CustomHeaderButtons[0].Properties.Enabled = false;
                gcCompany.Visible = false;
            }
        }

        private void setStatistics()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            if (DBRelease.getReleaseOrderReceiptStatistics(jobj, ref jResult))
            {
                teStandByCnt1.Text = $"{jResult["STANDBY_CNT"]}";
                teReceiptCnt1.Text = $"{jResult["ORDER_CNT"]}";
                teProcessCnt1.Text = $"{jResult["PROCESS_CNT"]}";
                teReleaseCnt1.Text = $"{jResult["RELEASE_CNT"]}";
                teWarehousingCnt1.Text = $"{jResult["WAREHOUSING_CNT"]}";
                //tePackageCnt.Text = $"{jResult["PACKAGE_CNT"]}";
            }
        }

        private void setReadonly(string state)
        {
            if (ProjectInfo._userCompanyId == 2)
            {
                if (state.Equals("P"))
                {
                    lgcOrderPart.CustomHeaderButtons[0].Properties.Enabled = true;
                }
                else
                {
                    lgcOrderPart.CustomHeaderButtons[0].Properties.Enabled = false;
                }
            }
        }

        private void getVisibleCol()
        {
            //JObject jData = new JObject();
            //JObject jResult = new JObject();

            //jData.Add("VIEW_CATEGORY", _viewCategory);
            //jData.Add("VIEW_TYPE", _viewType);

            //_listVisibleCol.Clear();
            //_listHideCol.Clear();

            //if (DBConnect.getVisibleCol(jData, ref jResult))
            //{
            //    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
            //    string filedName;
            //    int visible;
            //    foreach (JObject obj in jArray.Children<JObject>())
            //    {

            //        filedName = ConvertUtil.ToString(obj["FIELD_NAME"]);
            //        visible = ConvertUtil.ToInt32(obj["VISIBLE_YN"]);

            //        if (visible == 1)
            //            _listVisibleCol.Add(filedName);
            //        else
            //            _listHideCol.Add(filedName);
            //    }
            //}
            //else
            //{

            //}
        }


        private void setGridViewColums()
        {
            //lcgBarcodeList.BeginUpdate();
            //lcList.BeginInit();
            //gvList.BeginUpdate();
            //foreach (GridColumn gridcol in gvList.Columns)
            //{
            //    if (_listHideCol.Contains(gridcol.FieldName) || _lisDefaultHideCol.Contains(gridcol.FieldName))
            //        gridcol.Visible = false;
            //    else
            //        gridcol.Visible = true;
            //}

            //if(ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) || ProjectInfo._userId.Equals("lta104"))
            //{
            //    gcInitPrice.Visible = true;
            //    gcAdjust.Visible = true;
            //}
            //else
            //{
            //    gcInitPrice.Visible = false;
            //    gcAdjust.Visible = false;
            //}

            //gvList.EndUpdate();
            //lcList.EndInit();
            //lcgBarcodeList.EndUpdate();
        }

        private void setGridViewColumsWarehousing()
        {
            //lcgBarcodeList.BeginUpdate();
            //lcList.BeginInit();
            //gvList.BeginUpdate();
            //gvWarehousingList.BeginUpdate();
            //foreach (GridColumn gridcol in gvList.Columns)
            //{
            //    if (_listHideCol.Contains(gridcol.FieldName) || _lisDefaultHideCol.Contains(gridcol.FieldName))
            //        gridcol.Visible = false;
            //    else
            //        gridcol.Visible = true;
            //}

            //if (ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) || ProjectInfo._userId.Equals("lta104"))
            //{
            //    gcInitPrice.Visible = true;
            //    gcAdjust.Visible = true;

            //    gcComplete.Visible = true;
            //    gcProcess.Visible = true;
            //    //gcRemain.Visible = true;

            //    gcPasswordCnt.Visible = true;
            //    gcDisplayCnt.Visible = true;
            //    gcKeyboardCnt.Visible = true;
            //    gcUsbCnt.Visible = true;
            //    gcCaseCnt.Visible = true;
            //    gcWirelessCnt.Visible = true;
            //    gcBatteryCnt.Visible = true;
            //    gcEtcCnt.Visible = true;

            //    if (ProjectInfo._userType.Equals("M") || ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId))
            //    {
            //        lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = true;
            //        //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = true;
            //        lcgBarcodeList.CustomHeaderButtons[5].Properties.Visible = true;
            //    }
            //    else
            //    {
            //        lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //        //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //        lcgBarcodeList.CustomHeaderButtons[5].Properties.Visible = false;
            //    }
            //}
            //else
            //{
            //    gcInitPrice.Visible = false;
            //    gcAdjust.Visible = false;

            //    gcComplete.Visible = false;
            //    gcProcess.Visible = false;
            //    //gcRemain.Visible = false;

            //    gcPasswordCnt.Visible = false;
            //    gcDisplayCnt.Visible = false;
            //    gcKeyboardCnt.Visible = false;
            //    gcUsbCnt.Visible = false;
            //    gcCaseCnt.Visible = false;
            //    gcWirelessCnt.Visible = false;
            //    gcBatteryCnt.Visible = false;
            //    gcEtcCnt.Visible = false;

            //    lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //    //lcgBarcodeList.CustomHeaderButtons[1].Properties.Visible = false;
            //    lcgBarcodeList.CustomHeaderButtons[5].Properties.Visible = false;
            //}

            //gvWarehousingList.EndUpdate();
            //gvList.EndUpdate();
            //lcList.EndInit();
            //lcgBarcodeList.EndUpdate();
        }

        private void gvExport_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            //bool isValidRow = (e.FocusedRowHandle >= 0 && gvOrder.RowCount > 0);

            //if (isValidRow)
            //{
            //    _currentOrderRow = e.Row as DataRowView;
            //    _order = ConvertUtil.ToString(_currentOrderRow["ORDERS"]);
            //    _orderId = ConvertUtil.ToInt64(_currentOrderRow["ORDER_ID"]);

            //    if (!initialize)
            //        Dangol.ShowSplash();

            //    geList();

            //    if (!initialize)
            //        Dangol.CloseSplash();
               
            //}
            //else
            //{
            //    _currentOrderRow = null;
            //    _order = "";
            //    _orderId = -1;
            //}
        }

        private void gvExport_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //bool isValidRow = (e.FocusedRowHandle >= 0 && gvOrder.RowCount > 0);

            //if (isValidRow)
            //{
            //    _currentOrderRow = gvOrder.GetRow(e.FocusedRowHandle) as DataRowView;
            //    _order = ConvertUtil.ToString(_currentOrderRow["ORDERS"]);
            //    _orderId = ConvertUtil.ToInt64(_currentOrderRow["ORDER_ID"]);
            //}
            //else
            //{
            //    _order = "";
            //    _orderId = -1;
            //    _currentOrderRow = null;
            //}
        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _orderPartId = ConvertUtil.ToInt64(_currentRow["ORDER_PART_ID"]);
                _state = ConvertUtil.ToString(_currentRow["PART_STATE"]);

                setReadonly(_state);

                string state = ConvertUtil.ToString(_currentRow["PART_STATE"]);
                if (ProjectInfo._userCompanyId == 2)
                {
                    if (state.Equals("E") || state.Equals("O") || state.Equals("P"))
                    {
                        gcSpareParts.OptionsColumn.ReadOnly = false;
                        gcEtc.OptionsColumn.ReadOnly = false;
                        gcQty.OptionsColumn.ReadOnly = false;
                        lcgBarcodeList.CustomHeaderButtons[1].Properties.Enabled = true;
                    }
                    else
                    {
                        gcSpareParts.OptionsColumn.ReadOnly = true;
                        gcEtc.OptionsColumn.ReadOnly = true;
                        gcQty.OptionsColumn.ReadOnly = true;
                        lcgBarcodeList.CustomHeaderButtons[1].Properties.Enabled = false;
                    }
                }
                else
                {
                    if (state.Equals("E") || state.Equals("O"))
                    {
                        gcSpareParts.OptionsColumn.ReadOnly = false;
                        gcEtc.OptionsColumn.ReadOnly = false;
                        gcQty.OptionsColumn.ReadOnly = false;
                        lcgBarcodeList.CustomHeaderButtons[1].Properties.Enabled = true;
                    }
                    else
                    {
                        gcSpareParts.OptionsColumn.ReadOnly = true;
                        gcEtc.OptionsColumn.ReadOnly = true;
                        gcQty.OptionsColumn.ReadOnly = true;
                        lcgBarcodeList.CustomHeaderButtons[1].Properties.Enabled = false;
                    }
                }

                if (!initialize)
                    Dangol.ShowSplash();

                getDetailInfo();

                if (!initialize)
                    Dangol.CloseSplash();
            }
            else
            {
                setReadonly("-1");

                _currentRow = null;
                _barcode = "";
                _orderPartId = -1;
                _state = "";
                gcSpareParts.OptionsColumn.ReadOnly = true;
                gcEtc.OptionsColumn.ReadOnly = true;
                gcQty.OptionsColumn.ReadOnly = true;

                _dtPart.Clear();
                _listReleaseBarcode.Clear();

                teExport.Text = "";
                lePayment.EditValue = -1;
                deReceiptDt.EditValue = -1;
                leCompany.EditValue = -1;
                leTaxInvoice.EditValue = -1;
                rgReleaseType.EditValue = -1;
                meDes.Text = "";
                teCustomerNm.Text = "";
                teTel.Text = "";
                teHp.Text = "";
                tePostalCd.Text = "";
                teAddress.Text = "";
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvList.GetRow(e.FocusedRowHandle) as DataRowView;

                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _state = ConvertUtil.ToString(_currentRow["PART_STATE"]);

                //if (ConvertUtil.ToString(_currentRow["PART_STATE"]).Equals("E"))
                //{
                //    gcSpareParts.OptionsColumn.ReadOnly = false;
                //    gcEtc.OptionsColumn.ReadOnly = false;
                //    gcQty.OptionsColumn.ReadOnly = false;
                //}
                //else
                //{
                //    gcSpareParts.OptionsColumn.ReadOnly = true;
                //    gcEtc.OptionsColumn.ReadOnly = true;
                //    gcQty.OptionsColumn.ReadOnly = true;
                //}

            }
            else
            {
                _currentRow = null;
                _barcode = "";
                _state = "";
                gcSpareParts.OptionsColumn.ReadOnly = true;
                gcEtc.OptionsColumn.ReadOnly = true;
                gcQty.OptionsColumn.ReadOnly = true;
            }
        }

        private bool getDetailInfo()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("ORDER_PART_ID", _orderPartId);
            jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentRow["EXPORT_ID"]));

            _dtPart.Clear();
            _listReleaseBarcode.Clear();

            teExport.Text = "";
            lePayment.EditValue = -1;
            deReceiptDt.EditValue = -1;
            leCompany.EditValue = -1;
            leTaxInvoice.EditValue = -1;
            rgReleaseType.EditValue = -1;
            meDes.Text = "";
            teCustomerNm.Text = "";
            teTel.Text = "";
            teHp.Text = "";
            tePostalCd.Text = "";
            teAddress.Text = "";

            if (DBRelease.getOrderPartInfoDetail(jobj, ref jResult))
            {

                JObject jData = (JObject)jResult["EXPORT_DATA"];

                teExport.Text = $"{jData["EXPORT"] }";
                lePayment.EditValue = ConvertUtil.ToInt32(jData["PAYMENT"]);
                deReceiptDt.EditValue = ConvertUtil.ToDateTimeNull(jData["RECEIPT_DT"]); 
                leCompany.EditValue = ConvertUtil.ToInt64(jData["COMPANY_ID"]);
                leTaxInvoice.EditValue = ConvertUtil.ToInt32(jData["TAX_INVOICE"]);
                rgReleaseType.EditValue = ConvertUtil.ToString(jData["RELEASE_TYPE"]);
                meDes.Text = $"{jData["DES"] }";
                teCustomerNm.Text = $"{jData["CUSTOMER_NM"] }";
                teTel.Text = $"{jData["TEL"] }";
                teHp.Text = $"{jData["HP"] }";
                tePostalCd.Text = $"{jData["POSTAL_CD"] }";
                teAddress.Text = $"{jData["ADDRESS"] }";

                if (Convert.ToBoolean(jResult["PART_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                    int index = 1;
                    string barcode = "";
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        barcode = ConvertUtil.ToString(obj["BARCODE"]);
                        DataRow dr = _dtPart.NewRow();
                        dr["NO"] = index++;
                        
                        dr["ORDER_EXPORT_PART_ID"] = obj["ORDER_EXPORT_PART_ID"];
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["BARCODE"] = barcode;
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["CNT"] = obj["CNT"];
                        _dtPart.Rows.Add(dr);

                        if (string.IsNullOrWhiteSpace(barcode) && !_listReleaseBarcode.Contains(barcode))
                            _listReleaseBarcode.Add(barcode);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool getReleasePart()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("ORDER_PART_ID", _orderPartId);
            jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_currentRow["EXPORT_ID"]));

            _dtPart.Clear();
            _listReleaseBarcode.Clear();

            if (DBRelease.getOrderReleasePartInfoDetail(jobj, ref jResult))
            {
                if (Convert.ToBoolean(jResult["PART_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                    int index = 1;
                    string barcode = "";
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        barcode = ConvertUtil.ToString(obj["BARCODE"]);
                        DataRow dr = _dtPart.NewRow();
                        dr["NO"] = index++;

                        dr["ORDER_EXPORT_PART_ID"] = obj["ORDER_EXPORT_PART_ID"];
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["BARCODE"] = barcode;
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["CNT"] = obj["CNT"];
                        _dtPart.Rows.Add(dr);

                        if (string.IsNullOrWhiteSpace(barcode) && !_listReleaseBarcode.Contains(barcode))
                            _listReleaseBarcode.Add(barcode);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }



        private void geList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return;
            }


            _dt.Clear();
            //_dtDetail.Clear();
            //_dtProductDetail.Clear();

            if (DBRelease.getExportOrderList(jData, ref jResult))
            {
                gvList.BeginDataUpdate();

                int index = 1;
                long inventoryId;
                long checkId;
                string state;
                if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                        checkId = ConvertUtil.ToInt64(obj["CHECK_ID"]);
                        state = ConvertUtil.ToString(obj["PART_STATE"]);

                        dr["NO"] = index++;
                        
                        dr["ORDER_PART_ID"] = obj["ORDER_PART_ID"];
                        dr["EXPORT_ID"] = obj["EXPORT_ID"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["ORDER_ID"] = obj["ORDER_ID"];
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["ORDERS"] = obj["ORDERS"];
                        dr["ORDER_DETAIL"] = obj["ORDER_DETAIL"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["SPARE_PARTS"] = ConvertUtil.ToString(obj["SPARE_PARTS"]);
                        dr["ETC"] = ConvertUtil.ToString(obj["ETC"]); 
                        dr["ORDER_DT"] = ConvertUtil.ToDateTimeNull(obj["ORDER_DT"]);
                        if (state.Equals("R"))
                        {
                            dr["PART_RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["PART_RELEASE_DT"]);
                            dr["WAREHOUSING_DT"] = "";
                        }
                        else if (state.Equals("W"))
                        {
                            dr["PART_RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["PART_RELEASE_DT"]);
                            dr["WAREHOUSING_DT"] = ConvertUtil.ToDateTimeNull(obj["WAREHOUSING_DT"]);
                        }
                        else
                        {
                            dr["PART_RELEASE_DT"] = "";
                            dr["WAREHOUSING_DT"] = "";
                        }
                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                        dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                        dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURER"]);
                        dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                        //dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                        dr["CPU_INFO"] = obj["CPU_INFO"];
                        dr["MON_SIZE"] = obj["MON_INFO"];
                        dr["MEM_INFO"] = $"{ ConvertUtil.ToInt32(obj["MEM_INFO"])} GB";
                        dr["SSD_INFO"] = ConvertUtil.ToString(obj["SSD_INFO"]);
                        dr["HDD_INFO"] = ConvertUtil.ToString(obj["HDD_INFO"]);
                        dr["QTY"] = ConvertUtil.ToInt32(obj["QTY"]);
                        dr["RELEASE_QTY"] = ConvertUtil.ToInt32(obj["RELEASE_QTY"]);
                        
                        dr["PART_STATE"] = obj["PART_STATE"];
                        
                        if (checkId > 0)
                            dr["DEFECTIVE"] = setCheckInfo(obj);
                        dr["NEW"] = 0;
                        dr["STATE"] = 1;
                        dr["CHECK"] = false;

                        if (ConvertUtil.ToInt32(obj["WAREHOUSING_YN"]) == 1)
                            dr["WAREHOUSING_YN"] = "Y";

                        _dt.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["PART_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                    string componentCd;
                    string value;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                        state = ConvertUtil.ToString(obj["PART_STATE"]);

                        dr["NO"] = index++;
                        dr["ORDER_PART_ID"] = obj["ORDER_PART_ID"];
                        dr["EXPORT_ID"] = obj["EXPORT_ID"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["ORDER_ID"] = obj["ORDER_ID"];
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["ORDERS"] = obj["ORDERS"];
                        dr["ORDER_DETAIL"] = obj["ORDER_DETAIL"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["SPARE_PARTS"] = ConvertUtil.ToString(obj["SPARE_PARTS"]);
                        dr["ETC"] = ConvertUtil.ToString(obj["ETC"]);
                        dr["ORDER_DT"] = ConvertUtil.ToDateTimeNull(obj["ORDER_DT"]);
                        if (state.Equals("R"))
                        {
                            dr["PART_RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["PART_RELEASE_DT"]);
                            dr["WAREHOUSING_DT"] = "";
                        }
                        else if (state.Equals("W"))
                        {
                            dr["PART_RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["PART_RELEASE_DT"]);
                            dr["WAREHOUSING_DT"] = ConvertUtil.ToDateTimeNull(obj["WAREHOUSING_DT"]);
                        }
                        else
                        {
                            dr["PART_RELEASE_DT"] = "";
                            dr["WAREHOUSING_DT"] = "";
                        }
                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                        dr["NEW"] = 0;
                        dr["STATE"] = 0;
                        dr["QTY"] = obj["QTY"];
                        dr["RELEASE_QTY"] = obj["RELEASE_QTY"];
                        dr["PART_STATE"] = obj["PART_STATE"];
                        dr["CHECK"] = false;

                        if (ConvertUtil.ToInt32(obj["WAREHOUSING_YN"]) == 1)
                            dr["WAREHOUSING_YN"] = "Y";

                        dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURE_NM"]);
                        //dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                        if (componentCd.Equals("MEM"))
                        {
                            value = ConvertUtil.ToString(obj["MODEL_NM"]);
                            if (value.Equals("SO-DIMM"))
                                dr["MODEL_NM"] = "laptop";
                            else if (value.Equals("UDIMM"))
                                dr["MODEL_NM"] = "desktop";
                            else
                                dr["MODEL_NM"] = value;


                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                            value = ConvertUtil.ToString(obj["COL2"]);
                            int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                            if (capa < 1024)
                                dr["COL2"] = value;
                            else
                                dr["COL2"] = $"{capa / 1024}GB";
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                        }
                        else if (componentCd.Equals("VGA"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            value = ConvertUtil.ToString(obj["COL2"]);
                            int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                            if (capa < 1024)
                                dr["COL2"] = value;
                            else
                                dr["COL2"] = $"{capa / 1024}GB";
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                        }
                        else if (componentCd.Equals("MON"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);

                            value = ConvertUtil.ToString(obj["COL3"]);

                            if (_dicMonType.ContainsKey(value))
                                dr["COL3"] = _dicMonType[value];
                            else
                                dr["COL3"] = "unkown";
                        }
                        else if (componentCd.Equals("BAT"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                            dr["COL4"] = ConvertUtil.ToString(obj["COL4"]);
                        }
                        else if (componentCd.Equals("ADP"))
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                            value = ConvertUtil.ToString(obj["COL4"]);

                            if (string.IsNullOrEmpty(value))
                            {
                                string svolt = ConvertUtil.ToString(obj["COL2"]);
                                string sampere = ConvertUtil.ToString(obj["COL3"]);
                                double volt = ConvertUtil.ParseDouble(svolt);
                                double ampere = ConvertUtil.ParseDouble(sampere);

                                double watt = Math.Round(volt * ampere, 0);

                                dr["COL2"] = ConvertUtil.ToInt16(watt);
                            }
                            else
                                dr["COL2"] = ConvertUtil.ToString(obj["COL4"]);


                            dr["COL3"] = ConvertUtil.ToString(obj["COL2"]);
                            dr["COL4"] = ConvertUtil.ToString(obj["COL3"]);

                            value = ConvertUtil.ToString(obj["COL5"]);
                            if (!string.IsNullOrEmpty(value))
                            {
                                double size = ConvertUtil.ParseDouble(value);
                                if (size > 0)
                                    dr["COL5"] = $"{value} mm";
                                else
                                    dr["COL5"] = value;
                            }
                            else
                                dr["COL5"] = value;

                        }
                        else
                        {
                            dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                            dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                            dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                            dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                        }

                        dr["DETAIL"] = $"{dr["COL1"]}/{dr["COL2"]}/{dr["COL3"]}/{dr["COL4"]}/{dr["COL5"]}";

                        //if (componentCd.Equals("CPU"))
                        //{
                        //    dr["CPU_INFO"] = obj["MODEL_NM"];
                        //}
                        //else if (componentCd.Equals("MBD"))
                        //{
                        //    dr["MBD_MANUFACT"] = obj["MANUFACTURE_NM"];
                        //    dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NAME"];
                        //    dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                        //}
                        //else if (componentCd.Equals("MEM"))
                        //{
                        //    //dr["MEM_INFO"] = $"{obj["MANUFACTURE_NM"]}/{obj["SPEC_NM"]}";
                        //    dr["MEM_INFO"] = obj["COL1"];
                        //}
                        //else if (componentCd.Equals("STG"))
                        //{
                        //    string stgType = ConvertUtil.ToString(obj["COL1"]);
                        //    if (stgType.Contains("SSD"))
                        //        dr["SSD_INFO"] = obj["SPEC_NM"];
                        //    else
                        //        dr["HDD_INFO"] = obj["SPEC_NM"];
                        //    //dr["PRODUCT_NAME"] = obj["SPEC_NM"];
                        //}
                        //else if (componentCd.Equals("VGA"))
                        //{
                        //    dr["PRODUCT_NAME"] = obj["MODEL_NM"];
                        //}
                        //else if (componentCd.Equals("MON"))
                        //{
                        //    dr["MON_INFO"] = obj["SPEC_NM"];
                        //}

                        _dt.Rows.Add(dr);
                    }
                }

                _bs.Sort = "ORDER_DT DESC, ORDER_PART_ID DESC";

                gvList.EndDataUpdate();

            }
        }

        private string setCheckInfo(JObject obj)
        {    
            int checkValue = 0;
            List<string> checkList;
            string content = "";

            foreach (string col in ExamineInfo._NTBCOLNAME2ND)
            {
                
                checkValue = ConvertUtil.ToInt32(obj[col]);

                if (checkValue > 0)
                {
                    if (ExamineInfo._listCaseCheckCol.Contains(col))
                    {
                        checkList = ExamineInfo._NTBCHECK2ND["CASE"];
                        for (int i = 0; i < checkList.Count; i++)
                        {
                            if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[1]},";
                            else
                                content += $"{ExamineInfo._NTBCHECK2ND["CASE"][i]}{ExamineInfo._SYMBOL[0]},";
                        }

                        if (ConvertUtil.ToInt32(obj["CASE_HINGE"]) == 1)
                            content += "HINGE DESTROYED, ";
                    }
                    else
                    {
                        checkList = ExamineInfo._NTBCHECK2ND[col];
                        for (int i = 0; i < checkList.Count; i++)
                        {
                            if ((checkValue & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                content += $"{ExamineInfo._NTBCHECK2ND[col][i]},";
                        }
                        
                    }

                    if (content.Length > 1)
                        content = content.Substring(0, content.Length - 1);

                    content += " / ";
                }
            }

            if(!string.IsNullOrWhiteSpace(ConvertUtil.ToString(obj["CASE_DES"])))
                content += $" {obj["CASE_DES"]} ";

            return content;
        }

        private bool checkSearch(ref JObject jData)
        {

            DateTime dtfrom = ConvertUtil.ToDateTime(deDtFrom, 1);
            DateTime dtto = ConvertUtil.ToDateTime(deDtTo, 2);

            if (dtfrom.Year == 1970 || dtto.Year == 1970)
            {
                jData.Add("MSG", "Check the start and end dates are correctly.");
                return false;
            }

            int result = DateTime.Compare(dtfrom, dtto);

            if (result > 0)
            {
                jData.Add("MSG", "The end date must be greater than the start date");
                return false;
            }

            TimeSpan TS = dtto - dtfrom;
            int diffDay = TS.Days;

            if (diffDay > 730)
            {
                jData.Add("MSG", "The search period cannot exceed 2 years(730 days).");
                return false;
            }

            string dtFrom = dtfrom.ToString("yyyy-MM-dd HH:mm:ss");
            string dtTo = dtto.ToString("yyyy-MM-dd HH:mm:ss");


            jData.Add("ORDER_DT_S", dtFrom);
            jData.Add("ORDER_DT_E", dtTo);

            if (!string.IsNullOrWhiteSpace(teOrder.Text))
                jData.Add("ORDERS", ConvertUtil.ToString(teOrder.Text));

            if (!ConvertUtil.ToString(leOrderState.EditValue).Equals("-1"))
                jData.Add("ORSER_STATE", ConvertUtil.ToString(leOrderState.EditValue));

            if (!ConvertUtil.ToString(leCompany.EditValue).Equals("-1"))
                jData.Add("COMPANY_ID", ConvertUtil.ToString(leCompany.EditValue));

            //jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);
            //jData.Add("TYPE", 2);

            return true;
        }

        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                
                if (ConvertUtil.ToInt32(_currentRow["STATE"]) == 1)
                {
                    Dangol.Message("There are no changes.");
                    return;
                }

                if (Dangol.MessageYN("Save the changes?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    var jArrayNew = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    
                    JObject jdata = new JObject();
                    jdata.Add("ORDER_PART_ID", ConvertUtil.ToInt64(_currentRow["ORDER_PART_ID"]));
                    jdata.Add("SPARE_PARTS", ConvertUtil.ToString(_currentRow["SPARE_PARTS"]));
                    jdata.Add("ETC", ConvertUtil.ToString(_currentRow["ETC"]));
                    jdata.Add("QTY", ConvertUtil.ToInt32(_currentRow["QTY"]));
                    jArray.Add(jdata);
                        
                    

                    jobj.Add("DATA", jArray);
                    jobj.Add("NEW_DATA", jArrayNew);
                    jobj.Add("ORDER_ID", ConvertUtil.ToInt64(_currentRow["ORDER_ID"]));
                    jobj.Add("ORDERS", ConvertUtil.ToInt64(_currentRow["ORDERS"]));

                    if (ProjectInfo._userCompanyId != 2)
                        jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.updateReleaseOrderParts(jobj, ref jResult))
                    {
                        refresh();
                        Dangol.CloseSplash();

                        Dangol.Message("Execution completed.");
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
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE AND PART_STATE IN ('E', 'O')");
                if (rows.Length < 1)
                {
                    Dangol.Message("삭제 가능한 품목이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 품목을 삭제하시겠습니까?(대기 및 주문 상태 품목)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    List<long> listOrderPartId = new List<long>();
       
                    foreach (DataRow row in rows)
                        listOrderPartId.Add(ConvertUtil.ToInt64(row["ORDER_PART_ID"]));

                    jobj.Add("LIST_ORDER_PART_ID", string.Join(",", listOrderPartId));
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.deleteReleaseOrderParts(jobj, ref jResult))
                    {
                        refresh();

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
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE AND PART_STATE = 'E' AND NEW = 0");
                if (rows.Length < 1)
                {
                    Dangol.Message("주문 가능한 품목이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 품목을 주문하시겠습니까?(대기중인 품목)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    var jArray = new JArray();
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    List<long> listOrderPartId = new List<long>();

                    foreach (DataRow row in rows)
                        listOrderPartId.Add(ConvertUtil.ToInt64(row["ORDER_PART_ID"]));

                    jobj.Add("LIST_ORDER_PART_ID", string.Join(",", listOrderPartId));
                    jobj.Add("PART_STATE", "O");
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.updateReleaseOrderPartsBulk(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();
                        foreach(DataRow row in rows)
                        {
                            row["PART_STATE"] = "O";
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
            else if (e.Button.Properties.Tag.Equals(5))
            {
                Dangol.ShowSplash();
                refresh();
                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(6))
            {
                ImageInfo.GetImage(1, true, _barcode);
            }
        }

        private void refresh()
        {           
            int rowHandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowHandle;

            string barcode = _barcode;
            List<long> listChecked = new List<long>();

            //DataRow[] rows = _dt.Select("CHECK = TRUE");
            //long inventoryId;
            //foreach (DataRow row in rows)
            //{
            //    inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
            //    if (!listChecked.Contains(inventoryId))
            //        listChecked.Add(inventoryId);
            //}

            lcgBarcodeList.CustomHeaderButtons[5].Properties.Checked = false;

            gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged -= gvList_FocusedRowChanged;
            geList();

            //if (listChecked.Count > 0)
            //{
            //    gvList.BeginDataUpdate();
            //    foreach (DataRow row in _dt.Rows)
            //    {
            //        inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
            //        if (listChecked.Contains(inventoryId))
            //        {
            //            row.BeginEdit();
            //            row["CHECK"] = true;
            //            row.EndEdit();
            //        }
            //    }
            //    gvList.EndDataUpdate();
            //}
            gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged += gvList_FocusedRowChanged;

            rowHandle = gvList.LocateByValue("BARCODE", barcode);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvList.FocusedRowHandle = -2147483646;
                    gvList.FocusedRowHandle = rowHandle;
                    gvList.TopRowIndex = topRowIndex;
                }
            }
            else
            {
                if (_dt.Rows.Count > 0)
                {
                    gvList.FocusedRowHandle = -2147483646;
                    gvList.MoveFirst();
                }
            }
        }

        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            try
            {
                gvList.BeginUpdate();
                foreach (DataRow row in _dt.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvList.DataRowCount; i++)
                {
                    int rowHandle = gvList.GetVisibleRowHandle(i);
                    rows.Add(gvList.GetDataRow(rowHandle));
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
                gvList.EndUpdate();
            }
        }

        private void lcgBarcodeList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -2147483646;
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
            //int rowhandle = gvOrder.FocusedRowHandle;
            //gvOrder.FocusedRowHandle = -2147483646;
            //gvOrder.FocusedRowHandle = rowhandle;

            //if (e.Button.Properties.Tag.Equals(1))
            //{
                
            //}
            //else if (e.Button.Properties.Tag.Equals(2))
            //{
            //    Dangol.ShowSplash();
            //    getExportList();
            //    Dangol.CloseSplash();
            //}
            //else if (e.Button.Properties.Tag.Equals(3))
            //{
            //    if (Dangol.MessageYN("주문을 추가하시겠습니까?") == DialogResult.Yes)
            //    {
            //        Dangol.ShowSplash();


            //        JObject jResult = new JObject();
            //        JObject jData = new JObject();
            //        jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            //        if (DBRelease.createOrder(jData, ref jResult))
            //        {
            //            getExportList();
            //        }
    
            //        Dangol.CloseSplash();
            //    }
            //}
            //else if (e.Button.Properties.Tag.Equals(4))
            //{
            //    DataRow[] rows = _dt.Select($"ORDER_ID = {_orderId}");

            //    if(rows.Length > 0)
            //    {
            //        Dangol.Warining("자재가 존재하는 주문은 삭제할 수 없습니다.");
            //        return;
            //    }



            //    if (Dangol.MessageYN("주문을 삭제하시겠습니까?") == DialogResult.Yes)
            //    {
            //        Dangol.ShowSplash();

            //        JObject jResult = new JObject();
            //        JObject jData = new JObject();

            //        jData.Add("ORDER_ID", _orderId);

            //        if (DBRelease.deleteOrder(jData, ref jResult))
            //        {
            //            gvOrder.BeginDataUpdate();
            //            _currentOrderRow.Delete();
            //            gvOrder.EndDataUpdate();
            //            gvOrder.BeginDataUpdate();
            //            ArrayList arrRows = new ArrayList();
            //            for (int i = 0; i < gvOrder.DataRowCount; i++)
            //            {
            //                int rowHandle = gvOrder.GetVisibleRowHandle(i);
            //                arrRows.Add(gvOrder.GetDataRow(rowHandle));
            //            }

            //            for (int i = 0; i < arrRows.Count; i++)
            //            {
            //                DataRow row = arrRows[i] as DataRow;
            //                // Change the field value.
            //                row["NO"] = i+1;
            //            }
            //            gvOrder.EndDataUpdate();
            //        }

            //        Dangol.CloseSplash();
            //    }
            //}
        }


        private void sbSetColVisible_Click(object sender, EventArgs e)
        {
            //List<string> listHideCol = new List<string>();

            //if (!ProjectInfo._userType.Equals("M") && !ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) && !ProjectInfo._userId.Equals("lta104"))
            //{
            //    listHideCol = _listMasterCol;
            //}
                
            //using (dlgColVisible colVisible = new dlgColVisible(_viewCategory, _viewType, _listReadOnlyCol, listHideCol))
            //{
            //    if (colVisible.ShowDialog(this) == DialogResult.OK)
            //    {
            //        _listHideCol = colVisible._listHideCol;
            //        _listVisibleCol = colVisible._listVisibleCol;

            //        setGridViewColums();
            //    }
            //}
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            geList();
            Dangol.CloseSplash();
        }

        private void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //int rowhandle = gvOrder.FocusedRowHandle;
            //int topRowIndex = gvOrder.TopRowIndex;
            //gvOrder.FocusedRowHandle = -2147483646;
            //gvOrder.FocusedRowHandle = rowhandle;

            //try
            //{
            //    gvOrder.BeginUpdate();
            //    foreach (DataRow row in _dtOrder.Rows)
            //    {
            //        row.BeginEdit();
            //        row["CHECK"] = false;
            //        row.EndEdit();
            //    }

            //    ArrayList rows = new ArrayList();
            //    for (int i = 0; i < gvOrder.DataRowCount; i++)
            //    {
            //        int rowHandle = gvOrder.GetVisibleRowHandle(i);
            //        rows.Add(gvOrder.GetDataRow(rowHandle));
            //    }

            //    for (int i = 0; i < rows.Count; i++)
            //    {
            //        DataRow row = rows[i] as DataRow;
            //        // Change the field value.
            //        row["CHECK"] = true;
            //    }
            //}
            //finally
            //{
            //    gvOrder.EndUpdate();
            //}
        }

        private void layoutControlGroup1_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //int rowhandle = gvOrder.FocusedRowHandle;
            //int topRowIndex = gvOrder.TopRowIndex;
            //gvOrder.FocusedRowHandle = -2147483646;
            //gvOrder.FocusedRowHandle = rowhandle;

            //gvOrder.BeginDataUpdate();

            //foreach (DataRow row in _dtOrder.Rows)
            //{
            //    row.BeginEdit();
            //    row["CHECK"] = false;
            //    row.EndEdit();
            //}
            //gvOrder.EndDataUpdate();
        }

        private void rgDt_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            //try
            //{
            //    gvList.BeginDataUpdate();
            //    if (ConvertUtil.ToInt32(rgDt.EditValue) == 1)
            //    {

            //        gvList.BeginUpdate();
            //        this.baInfo.Columns.Clear();
            //        this.baInfo.Columns.Add(this.bandedGridColumn10);
            //        this.baInfo.Columns.Add(this.gcReleaseDt);
            //        this.baInfo.Columns.Add(this.gcArrivalDt);
            //        this.baInfo.Columns.Add(this.bandedGridColumn12);
            //        this.baInfo.Columns.Add(this.bandedGridColumn38);
            //        this.baInfo.Columns.Add(this.bandedGridColumn39);
            //        this.baInfo.Columns.Add(this.bandedGridColumn40);
            //        this.baInfo.Columns.Add(this.bandedGridColumn41);
            //        this.baInfo.Columns.Add(this.bandedGridColumn42);
            //        this.baInfo.Columns.Add(this.bandedGridColumn43);
            //        this.baInfo.Columns.Add(this.bandedGridColumn44);
            //        this.baInfo.Columns.Add(this.bandedGridColumn45);
            //        this.baInfo.Columns.Add(this.bandedGridColumn53);
            //        gvList.EndUpdate();

            //        gvList.ClearSorting();
            //        gvList.Columns["RELEASE_DT"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            //        gvList.Columns["ARRIVAL_DT"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;


            //    }
            //    else
            //    {
            //        gvList.BeginUpdate();
            //        this.baInfo.Columns.Clear();
            //        this.baInfo.Columns.Add(this.bandedGridColumn10);
            //        this.baInfo.Columns.Add(this.gcArrivalDt);
            //        this.baInfo.Columns.Add(this.gcReleaseDt);
            //        this.baInfo.Columns.Add(this.bandedGridColumn12);
            //        this.baInfo.Columns.Add(this.bandedGridColumn38);
            //        this.baInfo.Columns.Add(this.bandedGridColumn39);
            //        this.baInfo.Columns.Add(this.bandedGridColumn40);
            //        this.baInfo.Columns.Add(this.bandedGridColumn41);
            //        this.baInfo.Columns.Add(this.bandedGridColumn42);
            //        this.baInfo.Columns.Add(this.bandedGridColumn43);
            //        this.baInfo.Columns.Add(this.bandedGridColumn44);
            //        this.baInfo.Columns.Add(this.bandedGridColumn45);
            //        this.baInfo.Columns.Add(this.bandedGridColumn53);
            //        gvList.EndUpdate();

            //        gvList.ClearSorting();
            //        gvList.Columns["ARRIVAL_DT"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            //        gvList.Columns["RELEASE_DT"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            //    }
            //}
            //finally
            //{
            //    gvList.EndDataUpdate();

            //    gvList.BeginDataUpdate();
            //    ArrayList rows = new ArrayList();
            //    for (int i = 0; i < gvList.DataRowCount; i++)
            //    {
            //        int rowHandle = gvList.GetVisibleRowHandle(i);
            //        rows.Add(gvList.GetDataRow(rowHandle));
            //    }

            //    for (int i = 0; i < rows.Count; i++)
            //    {
            //        DataRow row = rows[i] as DataRow;
            //        // Change the field value.
            //        row["NO"] = i + 1;
            //    }

            //    gvList.EndDataUpdate();
            //}
        }

        private void sbSearch1_Click(object sender, EventArgs e)
        {
            //if (_orderId < 1)
            //{
            //    Dangol.Message("주문번호를 선택하세요.");
            //    return;
            //}
            //else
            //{

            //    string export = teExport.Text.Trim();
            //    string barcode = teBarcode.Text.Trim();

            //    if (barcode.Length < 1 || string.IsNullOrWhiteSpace(barcode) || export.Length < 1 || string.IsNullOrWhiteSpace(export))
            //        return;

            //    if (barcode.Length == 12 && export.Length == 10 && (regex1.IsMatch(barcode) || regex2.IsMatch(barcode) || regex3.IsMatch(barcode)) && regex4.IsMatch(export))
            //    {
            //        getCheckInfo(export, barcode);
            //    }
            //}
        }

        private void getCheckInfo(string export, string barcode)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();  

            jobj.Add("EXPORT", export);
            jobj.Add("BARCODE", barcode);
            jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            if (DBRelease.getExportDataInfo(jobj, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {

                    gvList.BeginDataUpdate();

                    int index = 1;
                    long inventoryId;
                    long checkId;

                    if (Convert.ToBoolean(jResult["PRODUCT"]))
                    {
                        if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
                        {
                            JArray jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());

                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                DataRow dr = _dt.NewRow();

                                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                                checkId = ConvertUtil.ToInt64(obj["CHECK_ID"]);

                                dr["NO"] = index++;
                                dr["COMPANY_ID"] = obj["COMPANY_ID"];
                                dr["ORDER_ID"] = _orderId;
                                dr["ORDERS"] = _order;
                                dr["EXPORT_ID"] = obj["EXPORT_ID"];
                                dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                                //dr["ORDERS"] = obj["ORDERS"];
                                //dr["ORDER_DETAIL"] = obj["ORDER_DETAIL"];
                                dr["BARCODE"] = obj["BARCODE"];
                                dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                                //dr["SPARE_PARTS"] = ConvertUtil.ToString(obj["SPARE_PARTS"]);
                                dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                                dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                                dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                                dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURER"]);
                                dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                                //dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                                dr["CPU_INFO"] = obj["CPU_INFO"];
                                dr["MON_SIZE"] = obj["MON_INFO"];
                                dr["MEM_INFO"] = $"{ ConvertUtil.ToInt32(obj["MEM_INFO"])} GB";
                                dr["SSD_INFO"] = ConvertUtil.ToString(obj["SSD_INFO"]);
                                dr["HDD_INFO"] = ConvertUtil.ToString(obj["HDD_INFO"]);
                                dr["QTY"] = 0;
                                dr["PART_STATE"] = "E";

                                if (checkId > 0)
                                    dr["DEFECTIVE"] = setCheckInfo(obj);
                                dr["NEW"] = 1;
                                dr["STATE"] = 0;
                                dr["PRODUCT_YN"] = 1;
                                dr["CHECK"] = false;

                                _dt.Rows.Add(dr);
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(jResult["PART_EXIST"]))
                        {
                            JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                            string componentCd;
                            string value;
                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                DataRow dr = _dt.NewRow();

                                componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                                dr["NO"] = index++;
                                dr["COMPANY_ID"] = obj["COMPANY_ID"];
                                dr["EXPORT_ID"] = obj["EXPORT_ID"];
                                dr["ORDER_ID"] = _orderId;
                                dr["ORDERS"] = _order;
                                dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                                //dr["ORDERS"] = obj["ORDERS"];
                                //dr["ORDER_DETAIL"] = obj["ORDER_DETAIL"];
                                dr["BARCODE"] = obj["BARCODE"];
                                dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                                //dr["SPARE_PARTS"] = ConvertUtil.ToString(obj["SPARE_PARTS"]);
                                dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                                dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                                dr["NEW"] = 1;
                                dr["STATE"] = 0;
                                dr["PRODUCT_YN"] = 0;
                                dr["QTY"] = 0;
                                dr["PART_STATE"] = "E";
                                dr["CHECK"] = false;

                                dr["MANUFACTURER"] = ConvertUtil.ToString(obj["MANUFACTURE_NM"]);
                                //dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                                if (componentCd.Equals("MEM"))
                                {
                                    value = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    if (value.Equals("SO-DIMM"))
                                        dr["MODEL_NM"] = "laptop";
                                    else if (value.Equals("UDIMM"))
                                        dr["MODEL_NM"] = "desktop";
                                    else
                                        dr["MODEL_NM"] = value;


                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                                    value = ConvertUtil.ToString(obj["COL2"]);
                                    int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                                    if (capa < 1024)
                                        dr["COL2"] = value;
                                    else
                                        dr["COL2"] = $"{capa / 1024}GB";
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                }
                                else if (componentCd.Equals("VGA"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    value = ConvertUtil.ToString(obj["COL2"]);
                                    int capa = ConvertUtil.ToInt32(Regex.Replace(value, @"\D", ""));
                                    if (capa < 1024)
                                        dr["COL2"] = value;
                                    else
                                        dr["COL2"] = $"{capa / 1024}GB";
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                }
                                else if (componentCd.Equals("MON"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);

                                    value = ConvertUtil.ToString(obj["COL3"]);

                                    if (_dicMonType.ContainsKey(value))
                                        dr["COL3"] = _dicMonType[value];
                                    else
                                        dr["COL3"] = "unkown";
                                }
                                else if (componentCd.Equals("BAT"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);

                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                    dr["COL4"] = ConvertUtil.ToString(obj["COL4"]);
                                }
                                else if (componentCd.Equals("ADP"))
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);

                                    value = ConvertUtil.ToString(obj["COL4"]);

                                    if (string.IsNullOrEmpty(value))
                                    {
                                        string svolt = ConvertUtil.ToString(obj["COL2"]);
                                        string sampere = ConvertUtil.ToString(obj["COL3"]);
                                        double volt = ConvertUtil.ParseDouble(svolt);
                                        double ampere = ConvertUtil.ParseDouble(sampere);

                                        double watt = Math.Round(volt * ampere, 0);

                                        dr["COL2"] = ConvertUtil.ToInt16(watt);
                                    }
                                    else
                                        dr["COL2"] = ConvertUtil.ToString(obj["COL4"]);


                                    dr["COL3"] = ConvertUtil.ToString(obj["COL2"]);
                                    dr["COL4"] = ConvertUtil.ToString(obj["COL3"]);

                                    value = ConvertUtil.ToString(obj["COL5"]);
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        double size = ConvertUtil.ParseDouble(value);
                                        if (size > 0)
                                            dr["COL5"] = $"{value} mm";
                                        else
                                            dr["COL5"] = value;
                                    }
                                    else
                                        dr["COL5"] = value;

                                }
                                else
                                {
                                    dr["MODEL_NM"] = ConvertUtil.ToString(obj["MODEL_NM"]);
                                    dr["COL1"] = ConvertUtil.ToString(obj["COL1"]);
                                    dr["COL2"] = ConvertUtil.ToString(obj["COL2"]);
                                    dr["COL3"] = ConvertUtil.ToString(obj["COL3"]);
                                }

                                dr["DETAIL"] = $"{dr["COL1"]}/{dr["COL2"]}/{dr["COL3"]}/{dr["COL4"]}/{dr["COL5"]}";

                                //if (componentCd.Equals("CPU"))
                                //{
                                //    dr["CPU_INFO"] = obj["MODEL_NM"];
                                //}
                                //else if (componentCd.Equals("MBD"))
                                //{
                                //    dr["MBD_MANUFACT"] = obj["MANUFACTURE_NM"];
                                //    dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NAME"];
                                //    dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                                //}
                                //else if (componentCd.Equals("MEM"))
                                //{
                                //    //dr["MEM_INFO"] = $"{obj["MANUFACTURE_NM"]}/{obj["SPEC_NM"]}";
                                //    dr["MEM_INFO"] = obj["COL1"];
                                //}
                                //else if (componentCd.Equals("STG"))
                                //{
                                //    string stgType = ConvertUtil.ToString(obj["COL1"]);
                                //    if (stgType.Contains("SSD"))
                                //        dr["SSD_INFO"] = obj["SPEC_NM"];
                                //    else
                                //        dr["HDD_INFO"] = obj["SPEC_NM"];
                                //    //dr["PRODUCT_NAME"] = obj["SPEC_NM"];
                                //}
                                //else if (componentCd.Equals("VGA"))
                                //{
                                //    dr["PRODUCT_NAME"] = obj["MODEL_NM"];
                                //}
                                //else if (componentCd.Equals("MON"))
                                //{
                                //    dr["MON_INFO"] = obj["SPEC_NM"];
                                //}

                                _dt.Rows.Add(dr);
                            }
                        }
                    }

                    gvList.EndDataUpdate();
                }
            }
        }

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "CHECK")
            {
                int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                if (state == 0)
                    _currentRow["STATE"] = 2;
            }
        }

        private void gvList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "BARCODE")
            {
                int state = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["STATE"]));

                if (state == 2)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
            }
            else if (e.Column.FieldName == "ORDERS")
            {
                int newPart = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["NEW"]));

                if (newPart == 1)
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Beige);
            }
        }

        private void usrReleaseOrderList_Resize(object sender, EventArgs e)
        {
            if (this.Width < 1569)
            {
                lcSearch.Size = new Size(lcSearch.Size.Width, 54);
                lcSearch.MaxSize = new Size(0, 54);
            }
            else
            {
                lcSearch.Size = new Size(lcSearch.Size.Width, 30);
                lcSearch.MaxSize = new Size(0, 30);
            }
        }

        private void lgcOrderPart_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (dlgReleasePart releasePart = new dlgReleasePart(_currentRow, _dtPart))
                {
                    releasePart.ShowDialog(this);

                    if (releasePart._isChanged)
                    {
                        Dangol.ShowSplash();
                        getReleasePart();
                        Dangol.CloseSplash();
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                //ORDER_EXPOCRT_PART_ID;

                if(!_state.Equals("O") && !_state.Equals("P"))
                {
                    Dangol.Message("주문, 처리중 상태에서만 삭제 가능합니다.");
                    return;
                }

                int[] selectedIdxes = gvPart.GetSelectedRows();

                if (selectedIdxes.Length < 1)
                {
                    Dangol.Message("선택한 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 품목을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    List<object> listId = new List<object>();

                    for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                    {
                        DataRowView row = gvPart.GetRow(selectedIdxes[i]) as DataRowView;
                        listId.Add(row["ORDER_EXPORT_PART_ID"]);
                    }

                    jobj.Add("LIST_ORDER_EXPORT_PART_ID", string.Join(",", listId));
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.deleteOrderReleasePart(jobj, ref jResult))
                    {

                        gvPart.BeginDataUpdate();

                        string barcode;
                        for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                        {
                            DataRowView row = gvPart.GetRow(selectedIdxes[i]) as DataRowView;

                            barcode = ConvertUtil.ToString(row["BARCODE"]);
                            if (!string.IsNullOrWhiteSpace(barcode) && _listReleaseBarcode.Contains(barcode))
                                _listReleaseBarcode.Remove(barcode);
                            row.Delete();
                        }

                        gvPart.EndDataUpdate();

                        Dangol.CloseSplash();
                        Dangol.Message("삭제되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }

        private void rgProcess_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            string state = ConvertUtil.ToString(e.NewValue);

            if (Dangol.MessageYN($"현재 주문을 '{_dicReleaseState[e.NewValue.ToString()]}'처리하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("ORDER_PART_ID", _orderPartId);
                jobj.Add("PART_STATE", state);

                if (DBRelease.updateOrderReleaseStatus(jobj, ref jResult))
                {
                    _currentRow.BeginEdit();

                    _currentRow["PART_STATE"] = state;

                    if(state.Equals("R"))
                    {
                        _currentRow["PART_RELEASE_DT"] = DateTime.Today.ToString("yyyy-MM-dd");
                        _currentRow["WAREHOUSING_DT"] = "";
                    }
                    else if (state.Equals("W"))
                    {
                        _currentRow["WAREHOUSING_DT"] = DateTime.Today.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        _currentRow["PART_RELEASE_DT"] = "";
                        _currentRow["WAREHOUSING_DT"] = "";
                    }

                    _currentRow.EndEdit();

                    setReadonly(state);
                    setStatistics();

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    e.Cancel = true;
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void peReleasePart_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gvList_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;

            if (view.FocusedColumn.FieldName == "SPARE_PARTS")
            {
                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;

                string componentCd = Convert.ToString(view.GetFocusedRowCellValue("COMPONENT_CD"));

                if (componentCd.Equals("NTB"))
                {
                    editor.Properties.DataSource = _dtSpareProduct;
                }
                else
                {
                    editor.Properties.DataSource = _dtSpareParts;
                }

            }
        }
    }
}