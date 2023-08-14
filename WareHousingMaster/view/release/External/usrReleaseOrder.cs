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
using DevExpress.XtraGrid.Columns;
using Enum = WareHousingMaster.view.common.Enum;
using System.Text.RegularExpressions;
using ScreenCopy;
using System.IO;
using WareHousingMaster.UtilTest;
using DevExpress.XtraGrid.Views.Base;

namespace WareHousingMaster.view.release.External
{
    public partial class usrReleaseOrder : DevExpress.XtraEditors.XtraForm
    {
        int _viewCategory = (int)Enum.EnumView.Adjustment;
        int _viewType = (int)Enum.EnumTableAdjustment.ExamList;

        DataTable _dtOrder;
        DataTable _dt;
        

        BindingSource _baOrder;
        BindingSource _bs;

        DataRowView _currentRow = null;
        DataRowView _currentOrderRow = null;
        
  
        //List<long> _listExportId;

        List<string> _listVisibleCol;
        List<string> _listHideCol;

        List<string> _listMasterCol;
        List<string> _listReadOnlyCol;

        List<string> _lisDefaultHideCol;

        DataTable _dtSpareProduct;

        DataTable _dtSpareParts;

        Dictionary<string, string> _dicMonType;

        long _orderId;
        long _companyId;
        string _barcode;
        string _order;

        Regex regex1;
        Regex regex2;
        Regex regex3;
        Regex regex4;

        bool initialize = true;


        public usrReleaseOrder()
        {
            InitializeComponent();

            _dtOrder = new DataTable();
            _dtOrder.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtOrder.Columns.Add(new DataColumn("ORDER_ID", typeof(long)));
            _dtOrder.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
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

            _dt.Columns.Add(new DataColumn("ORDER_DT", typeof(string)));
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
            _dt.Columns.Add(new DataColumn("DEFECTIVE_YN", typeof(string)));
            _dt.Columns.Add(new DataColumn("QTY", typeof(int)));
            _dt.Columns.Add(new DataColumn("SPARE_PARTS", typeof(string)));
            _dt.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dt.Columns.Add(new DataColumn("NEW", typeof(int)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("PRODUCT_YN", typeof(int)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));


            _baOrder = new BindingSource();
            _bs = new BindingSource();

            //_listExportId = new List<long>();

            _dicMonType = new Dictionary<string, string>();

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

            regex4 = new Regex(@"^E[0-9]{9}$");

        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            //getVisibleCol();
            //setGridViewColumsWarehousing();
            //setGridViewColums();

            
            getExportList();
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

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "SELECT ALL");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileCompanyId1, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompany, dtCompany, "KEY", "VALUE");

            DataTable dtOrderState = Util.getCodeList("CD082001", "KEY", "VALUE");
            Util.LookupEditHelper(rileOrderState, dtOrderState, "KEY", "VALUE");
            Util.LookupEditHelper(rileOrderState1, dtOrderState, "KEY", "VALUE");


            DataTable dtOrderState1 = new DataTable();

            dtOrderState1.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtOrderState1.Columns.Add(new DataColumn("VALUE", typeof(string)));

            foreach (DataRow row in dtOrderState.Rows)
            {
                if (!ConvertUtil.ToString(row["KEY"]).Equals("D"))
                {
                    DataRow dr = dtOrderState1.NewRow();
                    dr["KEY"] = row["KEY"];
                    dr["VALUE"] = row["VALUE"];
                    dtOrderState1.Rows.Add(dr);
                }
            }
            Util.insertRowonTop(dtOrderState1, "-1", "SELECT ALL");

            Util.LookupEditHelper(leOrderState, dtOrderState, "KEY", "VALUE");

            _dtSpareProduct = Util.getCodeList("CD082101", "KEY", "VALUE");
            _dtSpareParts = Util.getCodeList("CD08210201", "KEY", "VALUE");
            Util.insertRowonTop(_dtSpareProduct, "-1", "N/A");
            Util.insertRowonTop(_dtSpareParts, "-1", "N/A");

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

            DataTable dtMonType = Util.getCodeList("CD03011301", "KEY", "VALUE");
            foreach (DataRow row in dtMonType.Rows)
                _dicMonType.Add(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));

            //_dtSpareProduct

            //Util.LookupEditHelper(rileLocation, ProjectInfo._dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(rilePallet, ProjectInfo._dtPallet, "PALLET_ID", "PALLET_NM");

            //DataTable dtHinge = new DataTable();

            //dtHinge.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtHinge.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtHinge, 0, "");
            //Util.insertRowonTop(dtHinge, 1, "힌지파손");

            //DataTable dtmanufactureType = new DataTable();

            //dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            //Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            //Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            //DataTable dtHinge = new DataTable();

            //dtHinge.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtHinge.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtHinge, 0, "");
            //Util.insertRowonTop(dtHinge, 1, "힌지파손");
            //Util.LookupEditHelper(rileHinge, dtHinge, "KEY", "VALUE");




        }

        private void setIInitData()
        {
            //gvList.AutoGenerateColumns = false;

            gcOrder.DataSource = null;
            _baOrder.DataSource = _dtOrder;
            gcOrder.DataSource = _baOrder;

            gcList.DataSource = null;
            _bs.DataSource = _dt;
            gcList.DataSource = _bs;

            //gcDetail.DataSource = null;
            //_bsDetail.DataSource = _dtDetail;
            //gcDetail.DataSource = _bsDetail;

            //rgDt.EditValue = 1;

            var today = DateTime.Today;
            var pastDate = today.AddDays(-365);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            //deArrivalDt.EditValue = today;

            leOrderState.EditValue = "-1";
            if (ProjectInfo._userCompanyId == 2)
                leCompany.EditValue = "-1";
            else
                leCompany.EditValue = ConvertUtil.ToString(ProjectInfo._userCompanyId);

            if(ProjectInfo._userCompanyId == 2)
            {
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                gcCompany.Visible = true;
            }
            else
            {
                lcCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCompany.Visible = false;
            }
        }


        private void getVisibleCol()
        {
            JObject jData = new JObject();
            JObject jResult = new JObject();

            jData.Add("VIEW_CATEGORY", _viewCategory);
            jData.Add("VIEW_TYPE", _viewType);

            _listVisibleCol.Clear();
            _listHideCol.Clear();

            if (DBConnect.getVisibleCol(jData, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                string filedName;
                int visible;
                foreach (JObject obj in jArray.Children<JObject>())
                {

                    filedName = ConvertUtil.ToString(obj["FIELD_NAME"]);
                    visible = ConvertUtil.ToInt32(obj["VISIBLE_YN"]);

                    if (visible == 1)
                        _listVisibleCol.Add(filedName);
                    else
                        _listHideCol.Add(filedName);
                }
            }
            else
            {
            }
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
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvOrder.RowCount > 0);

            _dt.Clear();

            if (isValidRow)
            {
                _currentOrderRow = e.Row as DataRowView;
                _order = ConvertUtil.ToString(_currentOrderRow["ORDERS"]);
                _orderId = ConvertUtil.ToInt64(_currentOrderRow["ORDER_ID"]);

                if (!initialize)
                    Dangol.ShowSplash();

                geList();

                if (!initialize)
                    Dangol.CloseSplash();
               
            }
            else
            {
                _currentOrderRow = null;
                _order = "";
                _orderId = -1;
            }
        }

        private void gvExport_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvOrder.RowCount > 0);

            if (isValidRow)
            {
                _currentOrderRow = gvOrder.GetRow(e.FocusedRowHandle) as DataRowView;
                _order = ConvertUtil.ToString(_currentOrderRow["ORDERS"]);
                _orderId = ConvertUtil.ToInt64(_currentOrderRow["ORDER_ID"]);
            }
            else
            {
                _order = "";
                _orderId = -1;
                _currentOrderRow = null;
            }
        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);

                if(ConvertUtil.ToString(_currentRow["PART_STATE"]).Equals("E"))
                {
                    gcSpareParts.OptionsColumn.ReadOnly = false;
                    gcEtc.OptionsColumn.ReadOnly = false;
                    gcQty.OptionsColumn.ReadOnly = false;
                }
                else
                {
                    gcSpareParts.OptionsColumn.ReadOnly = true;
                    gcEtc.OptionsColumn.ReadOnly = true;
                    gcQty.OptionsColumn.ReadOnly = true;
                }
            }
            else
            {
                _currentRow = null;
                _barcode = "";
                gcSpareParts.OptionsColumn.ReadOnly = true;
                gcEtc.OptionsColumn.ReadOnly = true;
                gcQty.OptionsColumn.ReadOnly = true;
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvList.GetRow(e.FocusedRowHandle) as DataRowView;

                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);

                if (ConvertUtil.ToString(_currentRow["PART_STATE"]).Equals("E"))
                {
                    gcSpareParts.OptionsColumn.ReadOnly = false;
                    gcEtc.OptionsColumn.ReadOnly = false;
                    gcQty.OptionsColumn.ReadOnly = false;
                }
                else
                {
                    gcSpareParts.OptionsColumn.ReadOnly = true;
                    gcEtc.OptionsColumn.ReadOnly = true;
                    gcQty.OptionsColumn.ReadOnly = true;
                }

            }
            else
            {
                _currentRow = null;
                _barcode = "";
                gcSpareParts.OptionsColumn.ReadOnly = true;
                gcEtc.OptionsColumn.ReadOnly = true;
                gcQty.OptionsColumn.ReadOnly = true;
            }
        }

        private bool getExportList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtOrder.Clear();

            if (DBRelease.getOrderList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtOrder.NewRow();
                        dr["NO"] = index++;
                        dr["ORDER_ID"] = obj["ORDER_ID"];
                        dr["COMPANY_ID"] = ConvertUtil.ToInt64(obj["COMPANY_ID"]);
                        dr["ORDERS"] = obj["ORDERS"];
                        dr["ORDER_DT"] = ConvertUtil.ToDateTimeNull(obj["ORDER_DT"]);
                        dr["QTY"] = obj["QTY"];
                        dr["ORDER_STATE"] = obj["ORDER_STATE"];
                        _dtOrder.Rows.Add(dr);
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

            jData.Add("ORDER_ID", _orderId);
            if (ProjectInfo._userCompanyId != 2)
                jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            _dt.Clear();
            //_dtDetail.Clear();
            //_dtProductDetail.Clear();

            if (DBRelease.getExportOrderPart(jData, ref jResult))
            {
                gvList.BeginDataUpdate();

                int index = 1;
                long inventoryId;
                long checkId;

                if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                        checkId = ConvertUtil.ToInt64(obj["CHECK_ID"]);

                        dr["NO"] = index++;
                        
                        dr["ORDER_PART_ID"] = obj["ORDER_PART_ID"];
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
                        dr["QTY"] = obj["QTY"];
                        dr["PART_STATE"] = obj["PART_STATE"];

                        if (checkId > 0)
                        {
                            dr["DEFECTIVE"] = setCheckInfo(obj);
                            dr["DEFECTIVE_YN"] = string.IsNullOrEmpty(ConvertUtil.ToString(dr["DEFECTIVE"]))?"N":"Y";
                        }
                        dr["NEW"] = 0;
                        dr["STATE"] = 0;
                        dr["CHECK"] = false;
                        
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

                        dr["NO"] = index++;
                        dr["ORDER_PART_ID"] = obj["ORDER_PART_ID"];
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
                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                        dr["NEW"] = 0;
                        dr["STATE"] = 0;
                        dr["QTY"] = obj["QTY"];
                        dr["PART_STATE"] = obj["PART_STATE"];
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

                _bs.Sort = "ORDER_PART_ID DESC";

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

            if(dtfrom.Year == 1970 || dtto.Year == 1970)
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
            jData.Add("TYPE", 2);

            return true;
        }

        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -2147483646;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("STATE = 2 OR NEW = 1");
                if (rows.Length < 1)
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

                    int productYn;
                    int newPart;
                    foreach (DataRow row in rows)
                    {
                        newPart = ConvertUtil.ToInt32(row["NEW"]);
                        productYn = ConvertUtil.ToInt32(row["PRODUCT_YN"]);

                        if (newPart == 1)
                        {
                            JObject jdata = new JObject();
                            jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                            jdata.Add("EXPORT_ID", ConvertUtil.ToInt64(row["EXPORT_ID"]));
                            jdata.Add("ORDER_ID", _orderId);
                            jdata.Add("PRODUCT_YN", ConvertUtil.ToInt32(row["PRODUCT_YN"]));
                            jdata.Add("SPARE_PARTS", ConvertUtil.ToString(row["SPARE_PARTS"]));
                            jdata.Add("PART_STATE", ConvertUtil.ToString(row["PART_STATE"]));
                            jdata.Add("ETC", ConvertUtil.ToString(row["ETC"]));
                            jdata.Add("QTY", ConvertUtil.ToInt32(row["QTY"]));
                            jArrayNew.Add(jdata);
                        }
                        else
                        {
                            JObject jdata = new JObject();
                            jdata.Add("ORDER_PART_ID", ConvertUtil.ToInt64(row["ORDER_PART_ID"]));
                            jdata.Add("SPARE_PARTS", ConvertUtil.ToString(row["SPARE_PARTS"]));
                            jdata.Add("ETC", ConvertUtil.ToString(row["ETC"]));
                            jdata.Add("QTY", ConvertUtil.ToInt32(row["QTY"]));
                            jArray.Add(jdata);
                        }
                    }

                    jobj.Add("DATA", jArray);
                    jobj.Add("NEW_DATA", jArrayNew);
                    jobj.Add("ORDER_ID", _orderId);
                    jobj.Add("ORDERS", _order);
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
                    Dangol.Message("There are no lists that can be deleted.");
                    return;
                }

                if (Dangol.MessageYN("Delete the selected lists?(only standby and order status.)") == DialogResult.Yes)
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
                        Dangol.Message("Execution completed.");
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
                    Dangol.Message("There are no lists available to order.");
                    return;
                }

                if (Dangol.MessageYN("Order the selected lists?(only standby status.)") == DialogResult.Yes)
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
                    jobj.Add("ORDER_DT", DateTime.Now.ToString("yyyy-MM-dd"));

                    if (DBRelease.updateReleaseOrderPartsBulk(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();
                        foreach (DataRow row in rows)
                        {
                            row["PART_STATE"] = "O";
                            row["ORDER_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        gvList.EndDataUpdate();

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
            else if (e.Button.Properties.Tag.Equals(4))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcList.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("Finished exporting Excel.\r\n. Open the excel file?") == DialogResult.Yes)
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
                using (dlgReleaseCheck dlgReleaseCheck = new dlgReleaseCheck(ConvertUtil.ToInt64(_currentRow["COMPANY_ID"]),_currentRow["BARCODE"]))
                {
                    dlgReleaseCheck.ShowDialog(this);
                }
            }
            else if (e.Button.Properties.Tag.Equals(9))
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
            int rowhandle = gvOrder.FocusedRowHandle;
            gvOrder.FocusedRowHandle = -2147483646;
            gvOrder.FocusedRowHandle = rowhandle;

            if (e.Button.Properties.Tag.Equals(1))
            {
                
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                Dangol.ShowSplash();
                getExportList();
                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                if (Dangol.MessageYN("Create a new request list?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();


                    JObject jResult = new JObject();
                    JObject jData = new JObject();
                    jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.createOrder(jData, ref jResult))
                    {
                        getExportList();
                    }
    
                    Dangol.CloseSplash();
                }
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                DataRow[] rows = _dt.Select($"ORDER_ID = {_orderId}");

                if(rows.Length > 0)
                {
                    Dangol.Warining("List with a request cannot be deleted.");
                    return;
                }



                if (Dangol.MessageYN("Delete the request?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jResult = new JObject();
                    JObject jData = new JObject();

                    jData.Add("ORDER_ID", _orderId);

                    if (DBRelease.deleteOrder(jData, ref jResult))
                    {
                        gvOrder.BeginDataUpdate();
                        _currentOrderRow.Delete();
                        gvOrder.EndDataUpdate();
                        gvOrder.BeginDataUpdate();
                        ArrayList arrRows = new ArrayList();
                        for (int i = 0; i < gvOrder.DataRowCount; i++)
                        {
                            int rowHandle = gvOrder.GetVisibleRowHandle(i);
                            arrRows.Add(gvOrder.GetDataRow(rowHandle));
                        }

                        for (int i = 0; i < arrRows.Count; i++)
                        {
                            DataRow row = arrRows[i] as DataRow;
                            // Change the field value.
                            row["NO"] = i+1;
                        }
                        gvOrder.EndDataUpdate();
                    }

                    Dangol.CloseSplash();
                }
            }
        }


        private void sbSetColVisible_Click(object sender, EventArgs e)
        {
            List<string> listHideCol = new List<string>();

            if (!ProjectInfo._userType.Equals("M") && !ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) && !ProjectInfo._userId.Equals("lta104"))
            {
                listHideCol = _listMasterCol;
            }
                
            using (dlgColVisible colVisible = new dlgColVisible(_viewCategory, _viewType, _listReadOnlyCol, listHideCol))
            {
                if (colVisible.ShowDialog(this) == DialogResult.OK)
                {
                    _listHideCol = colVisible._listHideCol;
                    _listVisibleCol = colVisible._listVisibleCol;

                    setGridViewColums();
                }
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            getExportList();
            Dangol.CloseSplash();
        }

        private void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvOrder.FocusedRowHandle;
            int topRowIndex = gvOrder.TopRowIndex;
            gvOrder.FocusedRowHandle = -2147483646;
            gvOrder.FocusedRowHandle = rowhandle;

            try
            {
                gvOrder.BeginUpdate();
                foreach (DataRow row in _dtOrder.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvOrder.DataRowCount; i++)
                {
                    int rowHandle = gvOrder.GetVisibleRowHandle(i);
                    rows.Add(gvOrder.GetDataRow(rowHandle));
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
                gvOrder.EndUpdate();
            }
        }

        private void layoutControlGroup1_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvOrder.FocusedRowHandle;
            int topRowIndex = gvOrder.TopRowIndex;
            gvOrder.FocusedRowHandle = -2147483646;
            gvOrder.FocusedRowHandle = rowhandle;

            gvOrder.BeginDataUpdate();

            foreach (DataRow row in _dtOrder.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvOrder.EndDataUpdate();
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
        private void teBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                checkInventoryData();
            }  
        }

        private void sbSearch1_Click(object sender, EventArgs e)
        {
            checkInventoryData();
        }

        private void checkInventoryData()
        {
            if (_orderId < 1)
            {
                Dangol.Warining("Select request list");
                return;
            }
            else
            {

                //string export = teExport.Text.Trim();
                string barcode = teBarcode.Text.Trim();

                if (barcode.Length < 1 || string.IsNullOrWhiteSpace(barcode))
                    return;

                if (barcode.Length == 12 && (regex1.IsMatch(barcode) || regex2.IsMatch(barcode) || regex3.IsMatch(barcode)))
                {
                    getCheckInfo("", barcode);
                    teBarcode.Text = "";
                }
                else
                {
                    Dangol.Warining("cheeck the management no");
                    return;
                }

                //string export = teExport.Text.Trim();
                //string barcode = teBarcode.Text.Trim();

                //if (barcode.Length < 1 || string.IsNullOrWhiteSpace(barcode) || export.Length < 1 || string.IsNullOrWhiteSpace(export))
                //    return;

                //if (barcode.Length == 12 && export.Length == 10 && (regex1.IsMatch(barcode) || regex2.IsMatch(barcode) || regex3.IsMatch(barcode)) && regex4.IsMatch(export))
                //{
                //    getCheckInfo(export, barcode);
                //}
            }
        }

        private void getCheckInfo(string export, string barcode)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();  

            //jobj.Add("EXPORT", export);
            jobj.Add("BARCODE", barcode);
            jobj.Add("ORDER_ID", _orderId);
            if (ProjectInfo._userCompanyId != 2)
                jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            if (DBRelease.getExportDataInfo(jobj, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {

                  

                    long inventoryId;
                    long checkId;

                    if (Convert.ToBoolean(jResult["PRODUCT"]))
                    {
                        if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
                        {
                            gvList.BeginDataUpdate();
                            JArray jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());

                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                DataRow dr = _dt.NewRow();

                                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                                checkId = ConvertUtil.ToInt64(obj["CHECK_ID"]);

                                dr["NO"] = _dt.Rows.Count + 1;
                                dr["ORDER_PART_ID"] = jResult["ORDER_PART_ID"];
                                dr["ORDER_DETAIL"] = jResult["ORDER_DETAIL"];
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
                                dr["ORDER_DT"] = "";
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
                                {
                                    dr["DEFECTIVE"] = setCheckInfo(obj);
                                    dr["DEFECTIVE_YN"] = string.IsNullOrEmpty(ConvertUtil.ToString(dr["DEFECTIVE"])) ? "N" : "Y";
                                }
                                dr["NEW"] = 0;
                                dr["STATE"] = 0;
                                dr["PRODUCT_YN"] = 1;
                                dr["CHECK"] = false;

                                _dt.Rows.InsertAt(dr, 0);
                            }

                            gvList.EndDataUpdate();
                            gvList.MoveFirst();
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(jResult["PART_EXIST"]))
                        {
                            gvList.BeginDataUpdate();
                            JArray jArray = JArray.Parse(jResult["PART_DATA"].ToString());
                            string componentCd;
                            string value;
                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                DataRow dr = _dt.NewRow();

                                componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                                dr["NO"] = _dt.Rows.Count + 1;
                                dr["ORDER_DETAIL"] = jResult["ORDER_DETAIL"];
                                dr["ORDER_PART_ID"] = jResult["ORDER_PART_ID"];
                                dr["COMPANY_ID"] = obj["COMPANY_ID"];
                                dr["EXPORT_ID"] = obj["EXPORT_ID"];
                                dr["ORDER_ID"] = _orderId;
                                dr["ORDERS"] = _order;
                                dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                                //dr["ORDERS"] = obj["ORDERS"];
                                //dr["ORDER_DETAIL"] = obj["ORDER_DETAIL"];
                                dr["BARCODE"] = obj["BARCODE"];
                                dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                                dr["ORDER_DT"] = "";
                                //dr["SPARE_PARTS"] = ConvertUtil.ToString(obj["SPARE_PARTS"]);
                                dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                                dr["ARRIVAL_DT"] = ConvertUtil.ToDateTimeNull(obj["ARRIVAL_DT"]);
                                dr["NEW"] = 0;
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

                                _dt.Rows.InsertAt(dr, 0);
                            }

                            gvList.EndDataUpdate();
                            gvList.MoveFirst();
                        }
                    }
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