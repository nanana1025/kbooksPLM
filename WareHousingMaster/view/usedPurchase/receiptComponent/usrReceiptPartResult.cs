using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.usedPurchase.receiptComponent
{
    public partial class usrReceiptPartResult : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        DataTable _dtUsedPart;
        DataTable _dtExamineComponent;
        DataTable _dtExamineInventory;

        DataTable _dtProduct;
        DataTable _dtProductInventory;
        DataTable _dtfault;
        public BindingSource _bs { get; private set; }
        
        public DataRowView _currentRow { get; private set; }

        int _focusedRowHandle;
        int _topRowIndex;

        string _receipt;
        long _receiptId;

        long _partPrice;
        long _adjustPrice;
        string _receiptState;
        string _barcode;
        string _componentCd;
        int _sourceCd;

        Regex regex;

        public List<string> _listUsedPart;

        public int _msType { get; set; }

        public delegate void TotalCostChangeHandler();
        public event TotalCostChangeHandler totalCostChangeEvent;
        public usrReceiptPartResult()
        {
            InitializeComponent();

            _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("RECEIPT_PART_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("FAULT_ID", typeof(long)));

            _dt.Columns.Add(new DataColumn("RECEIPT_COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("RECEIPT_ORIGINAL_COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("RECEIPT_PART_CODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("RECEIPT_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("RECEIPT_PART_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("RECEIPT_PART_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("RECEIPT_TOTAL_PRICE", typeof(long)));

            _dt.Columns.Add(new DataColumn("EXAM_COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAM_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAM_PART_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("COMPARE", typeof(string)));
            _dt.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAM_PART_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("EXAM_ADJUST_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("EXAM_TOTAL_PRICE", typeof(long)));

            _dt.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("FAULT", typeof(bool)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));


            _dtUsedPart = new DataTable();
            _dtUsedPart.Columns.Add(new DataColumn("RECEIPT_PART_ID", typeof(long)));
            _dtUsedPart.Columns.Add(new DataColumn("RECEIPT_COMPONENT_CD", typeof(string))); 
            _dtUsedPart.Columns.Add(new DataColumn("RECEIPT_ORIGINAL_COMPONENT_CD", typeof(string)));
            _dtUsedPart.Columns.Add(new DataColumn("RECEIPT_MODEL_NM", typeof(string)));
            _dtUsedPart.Columns.Add(new DataColumn("RECEIPT_PART_CNT", typeof(int)));
            _dtUsedPart.Columns.Add(new DataColumn("RECEIPT_PART_CODE", typeof(string)));
            _dtUsedPart.Columns.Add(new DataColumn("RECEIPT_PART_PRICE", typeof(long)));


            _dtExamineComponent = new DataTable();
            _dtExamineComponent.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("EXAM_COMPONENT_CD", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("EXAM_PART_CODE", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("EXAM_MODEL_NM", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("EXAM_PART_PRICE", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("EXAM_PART_CNT", typeof(int)));
            _dtExamineComponent.Columns.Add(new DataColumn("EXAM_ADJUST_PRICE", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtExamineComponent.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));


            _dtExamineInventory = new DataTable();
            _dtExamineInventory.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("EXAM_PART_PRICE", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("EXAM_ADJUST_PRICE", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("EXAM_COMPONENT_CD", typeof(string)));

            _dtProduct = new DataTable();
            _dtProduct.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("EXAM_COMPONENT_CD", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("EXAM_MODEL_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("EXAM_PART_CNT", typeof(int)));
            _dtProduct.Columns.Add(new DataColumn("EXAM_PART_PRICE", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("EXAM_ADJUST_PRICE", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("EXAM_TOTAL_PRICE", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtProduct.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("BARCODE", typeof(string)));

            _dtProductInventory = new DataTable();
            _dtProductInventory.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("EXAM_MODEL_NM", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("EXAM_PART_PRICE", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("EXAM_ADJUST_PRICE", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("EXAM_COMPONENT_CD", typeof(string)));

            _dtProductInventory.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));

            _dtfault = new DataTable();
            _dtfault.Columns.Add(new DataColumn("FAULT_ID", typeof(long)));
            _dtfault.Columns.Add(new DataColumn("FAULT_PART_CODE", typeof(string)));
            _dtfault.Columns.Add(new DataColumn("FAULT_COMPONENT_CD", typeof(string)));
            _dtfault.Columns.Add(new DataColumn("FAULT_MODEL_NM", typeof(string)));
            _dtfault.Columns.Add(new DataColumn("FAULT_PART_CNT", typeof(int)));
            _dtfault.Columns.Add(new DataColumn("FAULT_INVENTORY_CAT", typeof(string)));
            _dtfault.Columns.Add(new DataColumn("FAULT_DES", typeof(string)));
            _dtfault.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _bs = new BindingSource();
            _listUsedPart = new List<string>();

            gcPart.DataSource = null;
            _bs.DataSource = _dt;
            gcPart.DataSource = _bs;

            setLookupedit();

            _msType = 99;

        }

        private void setLookupedit()
        {
            Util.LookupEditHelper(rileComponentCd, ConsignedInfo._dtComponent, "KEY", "VALUE");

            DataTable dtCompare = Util.getCodeList("CD1305", "KEY", "VALUE");
            Util.LookupEditHelper(rileCompare, dtCompare, "KEY", "VALUE");

            DataTable dtInventoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryCat, dtInventoryCat, "KEY", "VALUE");
        }


        public void setinitialize(long receiptId, string receipt, string receiptState, int sourceCd)
        {
            _receiptId = receiptId;
            _receipt = receipt;
            _receiptState = receiptState;
            _sourceCd = sourceCd;
        }

        public void resetInfo(long receiptId, string receipt, string receiptState, int sourceCd)
        {
            _receiptId = receiptId;
            _receipt = receipt;
            _receiptState = receiptState;
            _sourceCd = sourceCd;
        }

        public void getSummaryValue(ref long cost, ref long adjustCost, ref long totalCost)
        {
            //cost = ConvertUtil.ToInt32(bgcPrice.SummaryItem.SummaryValue);
            //adjustCost = ConvertUtil.ToInt32(bgcAdjustPrice.SummaryItem.SummaryValue);
            //totalCost = ConvertUtil.ToInt32(bgcTotalPrice.SummaryItem.SummaryValue);

            cost = ConvertUtil.ToInt64(_dt.Compute("Sum(EXAM_PART_PRICE)", "INVENTORY_ID <> -2"));
            adjustCost = ConvertUtil.ToInt64(_dt.Compute("Sum(EXAM_ADJUST_PRICE)", "INVENTORY_ID <> -2"));
            totalCost = ConvertUtil.ToInt64(_dt.Compute("Sum(EXAM_TOTAL_PRICE)", "INVENTORY_ID <> -2"));
        }

        private void setColumnAllowEdit(bool editable)
        {
            bool flag = !editable;
            //bgcCompare.OptionsColumn.ReadOnly = flag;
            bgcInventoryCat.OptionsColumn.ReadOnly = flag;
            bgcDes.OptionsColumn.ReadOnly = flag;
            bgcPrice.OptionsColumn.ReadOnly = flag;
            bgcAdjustPrice.OptionsColumn.ReadOnly = flag;

            //bgcCheck.OptionsColumn.ReadOnly = flag;

            bgcInventoryCat.OptionsColumn.AllowEdit = editable;
            bgcDes.OptionsColumn.AllowEdit = editable;
            bgcPrice.OptionsColumn.AllowEdit = editable;
            bgcAdjustPrice.OptionsColumn.AllowEdit = editable;
            //bgcCheck.OptionsColumn.AllowEdit = editable;
        }

        private void setEditableReturnCheck(bool flag)
        {
            bgcCheck.OptionsColumn.ReadOnly = !flag;
        }
        private void setEditableNtbCheck(bool flag)
        {
            bgcInventoryCat.OptionsColumn.ReadOnly = !flag;
            bgcPrice.OptionsColumn.ReadOnly = !flag;
            bgcAdjustPrice.OptionsColumn.ReadOnly = !flag;
        }
        public void refresh()
        {
            _focusedRowHandle = gvPart.FocusedRowHandle;
            _topRowIndex = gvPart.TopRowIndex;
            getComponentAll();
            gvPart.FocusedRowHandle = _focusedRowHandle;
            gvPart.TopRowIndex = _topRowIndex;
        }

        public void visibleReturnCheck(bool flag)
        {
            bgcCheck.Visible = flag;
        }

        public string getModelNm()
        {
            string modelNm = "";


            if(_dtUsedPart.Rows.Count == 1)
            {
                modelNm = ConvertUtil.ToString(_dtUsedPart.Rows[0]["RECEIPT_MODEL_NM"]);
            }
            else if (_dtUsedPart.Rows.Count > 1)
            {
                modelNm = $"{ConvertUtil.ToString(_dtUsedPart.Rows[0]["RECEIPT_MODEL_NM"])} 외 {_dtUsedPart.Rows.Count-1}건";
            }

            return modelNm;
        }

        /*
         * 
         * 
         * 검수정보 세팅
         * 
         */
        public void getComponentAll()
        {
            _dtUsedPart.Clear();
            _dtExamineComponent.Clear();
            _dtExamineInventory.Clear();
            _dtProduct.Clear();
            _dtProductInventory.Clear();

            JObject jResult = new JObject();
            bool isSuccess = false;

            isSuccess = DBUsedPurchase.getReceiptExaminePartList(_receiptId, _receipt, _sourceCd,  ref jResult);

            if (isSuccess)
            {
                setComponentTable(jResult);
                getFaultPart();
                setComponentResult();
            }
            else
            {
                return;
            }
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                long inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);
                _partPrice = ConvertUtil.ToInt64(_currentRow["EXAM_PART_PRICE"]);
                _adjustPrice = ConvertUtil.ToInt64(_currentRow["EXAM_ADJUST_PRICE"]);
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _componentCd = ConvertUtil.ToString(_currentRow["EXAM_COMPONENT_CD"]);
                long faultId = ConvertUtil.ToInt64(_currentRow["FAULT_ID"]);
                string componentCd = ConvertUtil.ToString(_currentRow["EXAM_COMPONENT_CD"]);
                if (_msType < 1)
                {

                    if (inventoryId == -1)
                        setColumnAllowEdit(false);
                    else
                        setColumnAllowEdit(true);
                }
                else
                    setColumnAllowEdit(false);

                if (faultId > 0 || inventoryId > 0)
                    setEditableReturnCheck(true);
                else
                    setEditableReturnCheck(false);

                if (componentCd.Equals("NTB") || componentCd.Equals("DKT"))
                    setEditableNtbCheck(false);
            }
            else
            {
                setColumnAllowEdit(false);
                setEditableReturnCheck(false);
                _barcode = "";
                _componentCd = "";
            }
        }

        private void gvPart_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvPart.GetRow(e.FocusedRowHandle) as DataRowView;
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _componentCd = ConvertUtil.ToString(_currentRow["EXAM_COMPONENT_CD"]);
            }
            else
            {
                _currentRow = null;
                _componentCd = "";
                _barcode = "";
            }
        }


        public void setComponentTable(JObject jResult)
        {
            JArray jArray;
            if (Convert.ToBoolean(jResult["EXIST"]))
            {
                jArray = JArray.Parse(jResult["DATA"].ToString());

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtUsedPart.NewRow();

                    string componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                    dr["RECEIPT_ORIGINAL_COMPONENT_CD"] = componentCd;

                    if (ProjectInfo._dicLTComponentCd.ContainsKey(componentCd))
                        componentCd = ProjectInfo._dicLTComponentCd[componentCd];

                    dr["RECEIPT_COMPONENT_CD"] = componentCd;                                  
                    dr["RECEIPT_PART_ID"] = obj["PART_ID"];
                    dr["RECEIPT_MODEL_NM"] = obj["MODEL_NM"];
                    dr["RECEIPT_PART_CNT"] = obj["PART_CNT"];
                    dr["RECEIPT_PART_CODE"] = obj["LT_COMPONENT"];
                    if (componentCd.Equals("EMS"))
                        dr["RECEIPT_PART_PRICE"] = ConvertUtil.ToInt64(obj["PRICE"]) * -1;
                    else
                        dr["RECEIPT_PART_PRICE"] = obj["PRICE"];
                    _dtUsedPart.Rows.Add(dr);

                    _listUsedPart.Add(ConvertUtil.ToString(obj["LT_COMPONENT"]));
                }
            }

            if (Convert.ToBoolean(jResult["EXAM_COMPONENT_EXIST"]))
            {
                jArray = JArray.Parse(jResult["EXAM_COMPONENT"].ToString());

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtExamineComponent.NewRow();
                    dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                    dr["COMPONENT"] = obj["COMPONENT"];
                    dr["EXAM_COMPONENT_CD"] = obj["COMPONENT_CD"];
                    dr["EXAM_PART_CODE"] = obj["LT_COMPONENT"];
                    dr["EXAM_MODEL_NM"] = obj["MODEL_NM"];
                    dr["EXAM_PART_PRICE"] = obj["PART_COST"];
                    dr["EXAM_PART_CNT"] = obj["PART_CNT"];
                    dr["EXAM_ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["STG_TYPE"] = obj["STG_TYPE"];
                    dr["CHECK"] = false;

                    _dtExamineComponent.Rows.Add(dr);
                }
            }

            if (Convert.ToBoolean(jResult["EXAM_INVENTORY_EXIST"]))
            {
                jArray = JArray.Parse(jResult["EXAM_INVENTORY"].ToString());

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtExamineInventory.NewRow();
                    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                    dr["COMPONENT"] = obj["COMPONENT"];
                    dr["BARCODE"] = obj["BARCODE"];
                    dr["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                    dr["DES"] = obj["DES"];
                    dr["EXAM_PART_PRICE"] = obj["INIT_PRICE"];
                    dr["EXAM_ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                    dr["EXAM_COMPONENT_CD"] = obj["COMPONENT_CD"];

                    _dtExamineInventory.Rows.Add(dr);
                }
            }

            if (Convert.ToBoolean(jResult["PRODUCT_EXIST"]))
            {
                jArray = JArray.Parse(jResult["PRODUCT_DATA"].ToString());

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtProduct.NewRow();
                    dr["PRODUCT_ID"] = obj["PRODUCT_ID"];
                    dr["EXAM_COMPONENT_CD"] = obj["COMPONENT_CD"];
                    dr["COMPONENT"] = obj["COMPONENT"];
                    dr["EXAM_MODEL_NM"] = obj["MODEL_NM"];
                    dr["EXAM_PART_CNT"] = obj["PART_CNT"];
                    dr["EXAM_PART_PRICE"] = obj["INIT_PRICE"];
                    dr["EXAM_ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                    dr["EXAM_TOTAL_PRICE"] = obj["TOTAL_COST"];
                    dr["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                    dr["DES"] = obj["DES"];

                    //dr["STG_TYPE"] = obj["STG_TYPE"];
                    dr["CHECK"] = false;
                    dr["BARCODE"] = obj["BARCODE"];

                    _dtProduct.Rows.Add(dr);
                }

                //jArray = JArray.Parse(jResult["PRODUCT_LIST_DATA"].ToString());

                //foreach (JObject obj in jArray.Children<JObject>())
                //{
                //    DataRow dr = _dtProductInventory.NewRow();
                //    dr["PRODUCT_ID"] = obj["P_INVENTORY_ID"];
                //    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                //    dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                //    dr["COMPONENT"] = obj["COMPONENT"];
                //    dr["BARCODE"] = obj["BARCODE"];
                //    dr["EXAM_MODEL_NM"] = obj["MODEL_NM"];
                //    dr["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                //    dr["DES"] = obj["DES"];
                //    dr["EXAM_PART_PRICE"] = obj["INIT_PRICE"];
                //    dr["EXAM_ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                //    dr["EXAM_COMPONENT_CD"] = obj["COMPONENT_CD"];
                //    dr["STG_TYPE"] = obj["STG_TYPE"];
                //    _dtProductInventory.Rows.Add(dr);
                //}
            }
        }

        private void getFaultPart()
        {
            _dt.Clear();

            JObject jResult = new JObject();

            if (DBUsedPurchase.getFaultList(-1, _receipt, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtfault.NewRow();
                        dr["FAULT_ID"] = obj["FAULT_ID"];
                        dr["FAULT_COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["FAULT_PART_CODE"] = obj["PART_CODE"];
                        dr["FAULT_MODEL_NM"] = obj["MODEL_NM"];
                        dr["FAULT_PART_CNT"] = obj["PART_CNT"];
                        dr["FAULT_INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                        dr["FAULT_DES"] = obj["DES"];
                        dr["CHECK"] = false;
                        _dtfault.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        

        public void setComponentResult()
        {
            _dt.Clear();

            DataRow[] drPurchase;
            DataRow[] drExamComponent;
            DataRow[] drFault;
            DataRow[] drExamInventory;
            DataRow[] drTemp;

            string receiptPartCode;
            string compare = "0";

            long totalPrice; 

            foreach (string componentCd in ProjectInfo._componetCdLT)
            {
                drPurchase = _dtUsedPart.Select($"RECEIPT_COMPONENT_CD = '{componentCd}'");

                if (drPurchase.Length > 0)
                {
                    foreach (DataRow row in drPurchase)
                    {
                        compare = "4";

                        receiptPartCode = ConvertUtil.ToString(row["RECEIPT_PART_CODE"]);
                        DataRow dr = _dt.NewRow();
                        dr["RECEIPT_PART_ID"] = row["RECEIPT_PART_ID"];
                        dr["RECEIPT_COMPONENT_CD"] = componentCd;
                        dr["RECEIPT_ORIGINAL_COMPONENT_CD"] = row["RECEIPT_ORIGINAL_COMPONENT_CD"];
                        
                        dr["RECEIPT_PART_CODE"] = receiptPartCode;
                        dr["RECEIPT_MODEL_NM"] = row["RECEIPT_MODEL_NM"];
                        dr["RECEIPT_PART_CNT"] = row["RECEIPT_PART_CNT"];
                        dr["RECEIPT_PART_PRICE"] = ConvertUtil.ToInt64(row["RECEIPT_PART_PRICE"]);
                        dr["RECEIPT_TOTAL_PRICE"] = ConvertUtil.ToInt64(row["RECEIPT_PART_PRICE"]) * ConvertUtil.ToInt64(row["RECEIPT_PART_CNT"]);
                        dr["BARCODE"] = "";

                        if (componentCd.Equals("NTB") || componentCd.Equals("DKT"))
                        {
                            //임시

                            string manufacture = "";
                            string cpuType = "";
                            string cpuGen = "";

                            string receiptModelNm = $"{row["RECEIPT_MODEL_NM"]}";

                            if (receiptModelNm.Contains("삼성"))
                                manufacture = "삼성";
                            else
                                manufacture = "외산";

                            if (receiptModelNm.Contains("I3"))
                                cpuType = "i3";
                            else if (receiptModelNm.Contains("I5"))
                                cpuType = "i5";
                            else if (receiptModelNm.Contains("I7"))
                                cpuType = "i7";

                            if (receiptModelNm.Contains("세대"))
                            {
                                int index = receiptModelNm.IndexOf("세대");
                                cpuGen = $"{receiptModelNm.Substring(index - 1, 1)}세대";
                            }
                            else
                                cpuGen = "0 세대";

                            //drExamComponent = _dtProduct.Select($"EXAM_COMPONENT_CD = '{componentCd}'AND CHECK = false");
                            drExamComponent = _dtProduct.Select($"EXAM_COMPONENT_CD = '{componentCd}' AND EXAM_MODEL_NM LIKE '%{manufacture}%' AND EXAM_MODEL_NM LIKE '%{cpuType} {cpuGen}%' AND CHECK = false");

                            if (drExamComponent.Length > 0)
                            {
                                int index = 0;
                                bool isCntSame = false;
                                if (ConvertUtil.ToInt32(row["RECEIPT_PART_CNT"]) == drExamComponent.Length)
                                {
                                    compare = "1";
                                    isCntSame = true;
                                }
      
                                foreach (DataRow rowComponent in drExamComponent)
                                //DataRow rowComponent = drExamComponent[0];
                                {
                                    if (index == 0)
                                    { 
                                        //DataRow dr = _dt.NewRow();
                                        //dr["RECEIPT_PART_ID"] = -1;
                                        //dr["RECEIPT_COMPONENT_CD"] = -1;

                                        dr["EXAM_COMPONENT_CD"] = componentCd;
                                        dr["COMPONENT"] = rowComponent["COMPONENT"];

                                        dr["EXAM_MODEL_NM"] = rowComponent["EXAM_MODEL_NM"];
                                        dr["EXAM_PART_CNT"] = 1;
                                        dr["EXAM_PART_PRICE"] = rowComponent["EXAM_PART_PRICE"];
                                        dr["COMPARE"] = compare;
                                        dr["INVENTORY_CAT"] = rowComponent["INVENTORY_CAT"];
                                        dr["DES"] = rowComponent["DES"];
                                        dr["EXAM_PART_PRICE"] = rowComponent["EXAM_PART_PRICE"];
                                        dr["EXAM_ADJUST_PRICE"] = rowComponent["EXAM_ADJUST_PRICE"];

                                        if (ConvertUtil.ToInt64(rowComponent["EXAM_TOTAL_PRICE"]) < 0)
                                            dr["EXAM_TOTAL_PRICE"] = 0;
                                        else
                                            dr["EXAM_TOTAL_PRICE"] = rowComponent["EXAM_TOTAL_PRICE"];
                                        dr["STG_TYPE"] = 1;
                                        dr["CHECK"] = false;
                                        dr["FAULT"] = false;
                                        dr["STATE"] = 0;

                                        dr["INVENTORY_ID"] = rowComponent["PRODUCT_ID"];
                                        dr["COMPONENT_ID"] = rowComponent["PRODUCT_ID"];
                                        dr["FAULT_ID"] = -1;
                                        dr["BARCODE"] = rowComponent["BARCODE"];

                                        _dt.Rows.Add(dr);

                                        rowComponent["CHECK"] = true;
                                    }
                                    else
                                    {
                                        DataRow drNew = _dt.NewRow();
                                        drNew["RECEIPT_PART_ID"] = -1;
                                        drNew["RECEIPT_COMPONENT_CD"] = "";

                                        drNew["EXAM_COMPONENT_CD"] = componentCd;
                                        drNew["COMPONENT"] = rowComponent["COMPONENT"];

                                        drNew["EXAM_MODEL_NM"] = rowComponent["EXAM_MODEL_NM"];
                                        drNew["EXAM_PART_CNT"] = 1;
                                        drNew["EXAM_PART_PRICE"] = rowComponent["EXAM_PART_PRICE"];
                                        if (isCntSame)
                                            drNew["COMPARE"] = compare;
                                        else
                                        {
                                            if (ConvertUtil.ToInt32(row["RECEIPT_PART_CNT"]) > index)
                                                drNew["COMPARE"] = compare;
                                            else
                                                drNew["COMPARE"] = 3;
                                        }
                                        drNew["INVENTORY_CAT"] = rowComponent["INVENTORY_CAT"];
                                        drNew["DES"] = rowComponent["DES"];
                                        drNew["EXAM_PART_PRICE"] = rowComponent["EXAM_PART_PRICE"];
                                        drNew["EXAM_ADJUST_PRICE"] = rowComponent["EXAM_ADJUST_PRICE"];

                                        if (ConvertUtil.ToInt64(rowComponent["EXAM_TOTAL_PRICE"]) < 0)
                                            drNew["EXAM_TOTAL_PRICE"] = 0;
                                        else
                                            drNew["EXAM_TOTAL_PRICE"] = rowComponent["EXAM_TOTAL_PRICE"];
                                        drNew["STG_TYPE"] = 1;
                                        drNew["CHECK"] = false;
                                        drNew["FAULT"] = false;
                                        drNew["STATE"] = 0;

                                        drNew["INVENTORY_ID"] = rowComponent["PRODUCT_ID"];
                                        drNew["COMPONENT_ID"] = rowComponent["PRODUCT_ID"];
                                        drNew["FAULT_ID"] = -1;
                                        drNew["BARCODE"] = rowComponent["BARCODE"];
                                        _dt.Rows.Add(drNew);

                                        rowComponent["CHECK"] = true;
                                    }
                                }
                            }
                            else
                            {
                                compare = "0";
                                dr["COMPARE"] = compare;
                                dr["EXAM_PART_PRICE"] = 0;
                                dr["EXAM_ADJUST_PRICE"] = 0;
                                dr["EXAM_TOTAL_PRICE"] = 0;


                                dr["INVENTORY_ID"] = -1;
                                dr["COMPONENT_ID"] = -1;
                                dr["CHECK"] = false;
                                dr["FAULT"] = false;
                                dr["STATE"] = 0;
                                dr["FAULT_ID"] = -1;

                                _dt.Rows.Add(dr);
                                
                            }
                        }
                        else
                        {
                            drExamComponent = _dtExamineComponent.Select($"EXAM_COMPONENT_CD = '{componentCd}' AND EXAM_PART_CODE = '{receiptPartCode}' AND CHECK = FALSE");
                            drFault = _dtfault.Select($"FAULT_COMPONENT_CD = '{componentCd}' AND FAULT_PART_CODE = '{receiptPartCode}' AND CHECK = FALSE");

                            if (drExamComponent.Length > 0 || drFault.Length > 0)
                            {
                                int index = 0;
                                compare = "1";

                                if (drExamComponent.Length > 0)
                                {
                                    List<object> listComponentId = new List<object>();

                                    foreach (DataRow rowComponent in drExamComponent)
                                    {
                                        listComponentId.Add(rowComponent["COMPONENT_ID"]);
                                        rowComponent["CHECK"] = true;
                                    }

                                    drExamInventory = _dtExamineInventory.Select($"COMPONENT_ID IN ({string.Join(",", listComponentId)})");

                                    foreach (DataRow rowInventory in drExamInventory)
                                    {

                                        drTemp = _dtExamineComponent.Select($"COMPONENT_ID = {rowInventory["COMPONENT_ID"]}");

                                        if (index == 0)
                                        {

                                            dr["EXAM_COMPONENT_CD"] = componentCd;
                                            dr["COMPONENT"] = drTemp[0]["COMPONENT"];
                                            if (componentCd.Equals("MEM"))
                                                dr["EXAM_MODEL_NM"] = setMem(drTemp[0]["EXAM_MODEL_NM"]);
                                            else if (componentCd.Equals("VGA"))
                                                dr["EXAM_MODEL_NM"] = setVga(drTemp[0]["EXAM_MODEL_NM"]);
                                            else
                                                dr["EXAM_MODEL_NM"] = drTemp[0]["EXAM_MODEL_NM"];
                                            dr["EXAM_PART_CNT"] = 1;
                                            dr["COMPARE"] = compare;
                                            dr["INVENTORY_CAT"] = rowInventory["INVENTORY_CAT"];
                                            dr["DES"] = rowInventory["DES"];
                                            dr["EXAM_PART_PRICE"] = rowInventory["EXAM_PART_PRICE"];
                                            dr["EXAM_ADJUST_PRICE"] = rowInventory["EXAM_ADJUST_PRICE"];
                                            totalPrice = ConvertUtil.ToInt64(rowInventory["EXAM_PART_PRICE"]) + ConvertUtil.ToInt64(rowInventory["EXAM_ADJUST_PRICE"]);
                                            if (totalPrice < 0)
                                                dr["EXAM_TOTAL_PRICE"] = 0;
                                            else
                                                dr["EXAM_TOTAL_PRICE"] = totalPrice;
                                            dr["STG_TYPE"] = drTemp[0]["STG_TYPE"];
                                            dr["CHECK"] = false;
                                            dr["FAULT"] = false;

                                            dr["STATE"] = 0;

                                            dr["INVENTORY_ID"] = rowInventory["INVENTORY_ID"];
                                            dr["COMPONENT_ID"] = rowInventory["COMPONENT_ID"];
                                            dr["FAULT_ID"] = -1;
                                            dr["BARCODE"] = rowInventory["BARCODE"];
                                            

                                            _dt.Rows.Add(dr);
                                        }
                                        else
                                        {
                                            DataRow drNew = _dt.NewRow();
                                            drNew["RECEIPT_PART_ID"] = -1;
                                            drNew["RECEIPT_COMPONENT_CD"] = "";

                                            drNew["EXAM_COMPONENT_CD"] = componentCd;
                                            drNew["COMPONENT"] = drTemp[0]["COMPONENT"];
                                            if (componentCd.Equals("MEM"))
                                                drNew["EXAM_MODEL_NM"] = setMem(drTemp[0]["EXAM_MODEL_NM"]);
                                            else if (componentCd.Equals("VGA"))
                                                drNew["EXAM_MODEL_NM"] = setVga(drTemp[0]["EXAM_MODEL_NM"]);
                                            else
                                                drNew["EXAM_MODEL_NM"] = drTemp[0]["EXAM_MODEL_NM"];
                                            drNew["EXAM_PART_CNT"] = 1;
                                            drNew["COMPARE"] = compare;
                                            drNew["INVENTORY_CAT"] = rowInventory["INVENTORY_CAT"];
                                            drNew["DES"] = rowInventory["DES"];
                                            drNew["EXAM_PART_PRICE"] = rowInventory["EXAM_PART_PRICE"];
                                            drNew["EXAM_ADJUST_PRICE"] = rowInventory["EXAM_ADJUST_PRICE"];

                                            totalPrice = ConvertUtil.ToInt64(rowInventory["EXAM_PART_PRICE"]) + ConvertUtil.ToInt64(rowInventory["EXAM_ADJUST_PRICE"]);
                                            if (totalPrice < 0)
                                                drNew["EXAM_TOTAL_PRICE"] = 0;
                                            else
                                                drNew["EXAM_TOTAL_PRICE"] = totalPrice;

                                            drNew["STG_TYPE"] = drTemp[0]["STG_TYPE"];
                                            drNew["CHECK"] = false;
                                            drNew["FAULT"] = false;
                                            drNew["STATE"] = 0;

                                            drNew["INVENTORY_ID"] = rowInventory["INVENTORY_ID"];
                                            drNew["COMPONENT_ID"] = rowInventory["COMPONENT_ID"];
                                            drNew["FAULT_ID"] = -1;
                                            drNew["BARCODE"] = rowInventory["BARCODE"];

                                            _dt.Rows.Add(drNew);
                                        }
                                        index++;
                                    }
                                }

                                if (drFault.Length > 0)
                                {
                                    index = 0;

                                    foreach (DataRow rowFaultData in drFault)
                                    {
                                        if (index == 0)
                                        {

                                            dr["EXAM_COMPONENT_CD"] = componentCd;
                                            dr["COMPONENT"] = "";
                                            dr["EXAM_MODEL_NM"] = rowFaultData["FAULT_MODEL_NM"];
                                            dr["EXAM_PART_CNT"] = rowFaultData["FAULT_PART_CNT"];

                                            dr["COMPARE"] = compare;
                                            dr["INVENTORY_CAT"] = rowFaultData["FAULT_INVENTORY_CAT"];
                                            dr["DES"] = rowFaultData["FAULT_DES"];
                                            dr["EXAM_PART_PRICE"] = 0;
                                            dr["EXAM_ADJUST_PRICE"] = 0;
                                            dr["EXAM_TOTAL_PRICE"] = 0;
                                            dr["STG_TYPE"] = "";
                                            dr["CHECK"] = false;
                                            dr["FAULT"] = true;
                                            dr["STATE"] = 0;

                                            dr["INVENTORY_ID"] = -1;
                                            dr["COMPONENT_ID"] = -1;
                                            dr["FAULT_ID"] = rowFaultData["FAULT_ID"];

                                            _dt.Rows.Add(dr);
                                        }
                                        else
                                        {
                                            DataRow drNew = _dt.NewRow();
                                            drNew["RECEIPT_PART_ID"] = -1;
                                            drNew["RECEIPT_COMPONENT_CD"] = "";

                                            drNew["EXAM_COMPONENT_CD"] = componentCd;
                                            drNew["COMPONENT"] = "";
                                            drNew["EXAM_MODEL_NM"] = rowFaultData["FAULT_MODEL_NM"];
                                            drNew["EXAM_PART_CNT"] = rowFaultData["FAULT_PART_CNT"];

                                            drNew["COMPARE"] = compare;
                                            drNew["INVENTORY_CAT"] = rowFaultData["FAULT_INVENTORY_CAT"];
                                            drNew["DES"] = rowFaultData["FAULT_DES"];
                                            drNew["EXAM_PART_PRICE"] = 0;
                                            drNew["EXAM_ADJUST_PRICE"] = 0;
                                            drNew["EXAM_TOTAL_PRICE"] = 0;
                                            drNew["STG_TYPE"] = "";
                                            drNew["CHECK"] = false;
                                            drNew["FAULT"] = true;
                                            drNew["STATE"] = 0;

                                            drNew["INVENTORY_ID"] = -1;
                                            drNew["COMPONENT_ID"] = -1;
                                            drNew["FAULT_ID"] = rowFaultData["FAULT_ID"];

                                            _dt.Rows.Add(drNew);
                                        }
                                        index++;

                                        rowFaultData["CHECK"] = true;
                                    }
                                }
                            }
                            else
                            {
                                if (componentCd.Equals("NTB"))
                                {
                                    compare = "0";
                                    dr["COMPARE"] = compare;
                                    dr["EXAM_PART_PRICE"] = 0;
                                    dr["EXAM_ADJUST_PRICE"] = 0;
                                    dr["EXAM_TOTAL_PRICE"] = 0;


                                    dr["INVENTORY_ID"] = -1;
                                    dr["COMPONENT_ID"] = -1;
                                    dr["CHECK"] = false;
                                    dr["FAULT"] = false;
                                    dr["STATE"] = 0;
                                    dr["FAULT_ID"] = -1;

                                    _dt.Rows.Add(dr);
                                }
                                else if (componentCd.Equals("EAD") || componentCd.Equals("EMS"))
                                {
                                    compare = "0";
                                    dr["COMPARE"] = compare;
                                    dr["EXAM_PART_CNT"] = row["RECEIPT_PART_CNT"];
                                    dr["EXAM_PART_PRICE"] = 0;
                                    dr["EXAM_ADJUST_PRICE"] = ConvertUtil.ToInt64(row["RECEIPT_PART_PRICE"]) * ConvertUtil.ToInt64(row["RECEIPT_PART_CNT"]); ;
                                    dr["EXAM_TOTAL_PRICE"] = ConvertUtil.ToInt64(row["RECEIPT_PART_PRICE"]) * ConvertUtil.ToInt64(row["RECEIPT_PART_CNT"]);

                                    dr["INVENTORY_ID"] = -1;
                                    dr["COMPONENT_ID"] = -1;
                                    dr["CHECK"] = false;
                                    dr["FAULT"] = false;
                                    dr["STATE"] = 0;
                                    dr["FAULT_ID"] = -1;

                                    _dt.Rows.Add(dr);
                                }
                                else
                                {
                                    dr["EXAM_PART_PRICE"] = 0;
                                    dr["EXAM_ADJUST_PRICE"] = 0;
                                    dr["EXAM_TOTAL_PRICE"] = 0;
                                    dr["COMPARE"] = compare;

                                    dr["INVENTORY_ID"] = -1;
                                    dr["COMPONENT_ID"] = -1;
                                    dr["CHECK"] = false;
                                    dr["FAULT"] = false;
                                    dr["STATE"] = 0;
                                    dr["FAULT_ID"] = -1;

                                    _dt.Rows.Add(dr);
                                }
                            }

                            drFault = _dtfault.Select($"FAULT_COMPONENT_CD = '{componentCd}' AND CHECK = FALSE");

                            if (drFault.Length > 0)
                            {
                                compare = "3";
                                foreach (DataRow rowFaultData in drFault)
                                {
                                    DataRow drNew = _dt.NewRow();
                                    drNew["RECEIPT_PART_ID"] = -1;
                                    drNew["RECEIPT_COMPONENT_CD"] = "";

                                    drNew["EXAM_COMPONENT_CD"] = componentCd;
                                    drNew["COMPONENT"] = "";
                                    drNew["EXAM_MODEL_NM"] = rowFaultData["FAULT_MODEL_NM"];
                                    drNew["EXAM_PART_CNT"] = rowFaultData["FAULT_PART_CNT"];

                                    drNew["COMPARE"] = compare;
                                    drNew["INVENTORY_CAT"] = rowFaultData["FAULT_INVENTORY_CAT"];
                                    drNew["DES"] = rowFaultData["FAULT_DES"];
                                    drNew["EXAM_PART_PRICE"] = 0;
                                    drNew["EXAM_ADJUST_PRICE"] = 0;
                                    drNew["EXAM_TOTAL_PRICE"] = 0;
                                    drNew["STG_TYPE"] = "";
                                    drNew["CHECK"] = false;
                                    drNew["FAULT"] = true;
                                    drNew["STATE"] = 0;

                                    drNew["INVENTORY_ID"] = -1;
                                    drNew["COMPONENT_ID"] = -1;
                                    drNew["FAULT_ID"] = rowFaultData["FAULT_ID"];

                                    _dt.Rows.Add(drNew);

                                    rowFaultData["CHECK"] = true;
                                }
                            }
                        }//여기가 임시로 끝
                    }
                }
                else
                {
                    drFault = _dtfault.Select($"FAULT_COMPONENT_CD = '{componentCd}' AND CHECK = FALSE");

                    if (drFault.Length > 0)
                    {
                        compare = "3";
                        foreach (DataRow rowFaultData in drFault)
                        {
                            DataRow drNew = _dt.NewRow();
                            drNew["RECEIPT_PART_ID"] = -1;
                            drNew["RECEIPT_COMPONENT_CD"] = "";

                            drNew["EXAM_COMPONENT_CD"] = componentCd;
                            drNew["COMPONENT"] = "";
                            drNew["EXAM_MODEL_NM"] = rowFaultData["FAULT_MODEL_NM"];
                            drNew["EXAM_PART_CNT"] = rowFaultData["FAULT_PART_CNT"];

                            drNew["COMPARE"] = compare;
                            drNew["INVENTORY_CAT"] = rowFaultData["FAULT_INVENTORY_CAT"];
                            drNew["DES"] = rowFaultData["FAULT_DES"];
                            drNew["EXAM_PART_PRICE"] = 0;
                            drNew["EXAM_ADJUST_PRICE"] = 0;
                            drNew["EXAM_TOTAL_PRICE"] = 0;
                            drNew["STG_TYPE"] = "";
                            drNew["CHECK"] = false;
                            drNew["FAULT"] = true;
                            drNew["STATE"] = 0;

                            drNew["INVENTORY_ID"] = -1;
                            drNew["COMPONENT_ID"] = -1;
                            drNew["FAULT_ID"] = rowFaultData["FAULT_ID"];

                            _dt.Rows.Add(drNew);

                            rowFaultData["CHECK"] = true;
                        }
                    }
                }


                if (componentCd.Equals("NTB") || componentCd.Equals("DKT"))
                {

                    //임시로 주석처리. 전부다 매칭되게 하려고 잠시 주석이다.
                    drExamComponent = _dtProduct.Select($"EXAM_COMPONENT_CD = '{componentCd}' AND CHECK = false");

                    if (drExamComponent.Length > 0)
                    {
                        foreach (DataRow rowComponent in drExamComponent)
                        {
                            compare = "3";

                            DataRow dr = _dt.NewRow();
                            dr["RECEIPT_PART_ID"] = -1;
                            dr["RECEIPT_COMPONENT_CD"] = -1;

                            dr["EXAM_COMPONENT_CD"] = componentCd;
                            dr["COMPONENT"] = rowComponent["COMPONENT"];

                            dr["EXAM_MODEL_NM"] = rowComponent["EXAM_MODEL_NM"];
                            dr["EXAM_PART_CNT"] = 1;
                            dr["EXAM_PART_PRICE"] = rowComponent["EXAM_PART_PRICE"];
                            dr["COMPARE"] = compare;
                            dr["INVENTORY_CAT"] = rowComponent["INVENTORY_CAT"];
                            dr["DES"] = rowComponent["DES"];
                            dr["EXAM_PART_PRICE"] = rowComponent["EXAM_PART_PRICE"];
                            dr["EXAM_ADJUST_PRICE"] = rowComponent["EXAM_ADJUST_PRICE"];

                            if (ConvertUtil.ToInt64(rowComponent["EXAM_TOTAL_PRICE"]) < 0)
                                dr["EXAM_TOTAL_PRICE"] = 0;
                            else
                                dr["EXAM_TOTAL_PRICE"] = rowComponent["EXAM_TOTAL_PRICE"];
                            dr["STG_TYPE"] = 1;
                            dr["CHECK"] = false;
                            dr["FAULT"] = false;
                            dr["STATE"] = 0;

                            dr["INVENTORY_ID"] = rowComponent["PRODUCT_ID"];
                            dr["COMPONENT_ID"] = rowComponent["PRODUCT_ID"];
                            dr["FAULT_ID"] = -1;
                            dr["BARCODE"] = rowComponent["BARCODE"];
                            _dt.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    drExamComponent = _dtExamineComponent.Select($"EXAM_COMPONENT_CD = '{componentCd}' AND CHECK = false");

                    if (drExamComponent.Length > 0)
                    {
                        foreach (DataRow rowComponent in drExamComponent)
                        {
                            compare = "3";

                            drExamInventory = _dtExamineInventory.Select($"COMPONENT_ID = {rowComponent["COMPONENT_ID"]}");

                            foreach (DataRow rowInventory in drExamInventory)
                            {
                                drTemp = _dtExamineComponent.Select($"COMPONENT_ID = {rowInventory["COMPONENT_ID"]}");

                                DataRow dr = _dt.NewRow();
                                dr["RECEIPT_PART_ID"] = -1;
                                dr["RECEIPT_COMPONENT_CD"] = -1;
                                
                                dr["EXAM_COMPONENT_CD"] = componentCd;
                                dr["COMPONENT"] = rowInventory["COMPONENT"];
                                if (componentCd.Equals("MEM"))
                                    dr["EXAM_MODEL_NM"] = setMem(drTemp[0]["EXAM_MODEL_NM"]);
                                else if (componentCd.Equals("VGA"))
                                    dr["EXAM_MODEL_NM"] = setVga(drTemp[0]["EXAM_MODEL_NM"]);
                                else
                                    dr["EXAM_MODEL_NM"] = drTemp[0]["EXAM_MODEL_NM"];
                                dr["EXAM_PART_CNT"] = 1;
                                dr["EXAM_PART_PRICE"] = rowInventory["EXAM_PART_PRICE"];
                                dr["COMPARE"] = compare;
                                dr["INVENTORY_CAT"] = rowInventory["INVENTORY_CAT"];
                                dr["DES"] = rowInventory["DES"];
                                dr["EXAM_PART_PRICE"] = rowInventory["EXAM_PART_PRICE"];
                                dr["EXAM_ADJUST_PRICE"] = rowInventory["EXAM_ADJUST_PRICE"];


                                totalPrice = ConvertUtil.ToInt64(rowInventory["EXAM_PART_PRICE"]) + ConvertUtil.ToInt64(rowInventory["EXAM_ADJUST_PRICE"]);
                                if (totalPrice < 0)
                                    dr["EXAM_TOTAL_PRICE"] = 0;
                                else
                                    dr["EXAM_TOTAL_PRICE"] = totalPrice;

                                dr["STG_TYPE"] = drTemp[0]["STG_TYPE"];
                                dr["CHECK"] = false;
                                dr["FAULT"] = false;
                                dr["STATE"] = 0;

                                dr["INVENTORY_ID"] = rowInventory["INVENTORY_ID"];
                                dr["COMPONENT_ID"] = rowInventory["COMPONENT_ID"];
                                dr["FAULT_ID"] = -1;
                                dr["BARCODE"] = rowInventory["BARCODE"];
                                _dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
        }

        private string setMem(object oModelNm)
        {
            string modelNm = ConvertUtil.ToString(oModelNm);
            string newModelNm = modelNm;

            string[] arrModelNm = modelNm.Split('/');
            if (arrModelNm.Length < 4)
                return modelNm;

            if (!arrModelNm[3].Contains("MBytes"))
                return modelNm;

            int capacity = ConvertUtil.ToInt32(arrModelNm[3].Replace("MBytes", ""));
            int newCapacity = capacity / 1024;

            newModelNm = $"{arrModelNm[0]}/{arrModelNm[1]}/{arrModelNm[2]}/{newCapacity}GB";

            return newModelNm;
        }
        private string setVga(object oModelNm)
        {
            string modelNm = ConvertUtil.ToString(oModelNm);
            string newModelNm = modelNm;

            string[] arrModelNm = modelNm.Split('/');
            if (arrModelNm.Length < 3)
                return modelNm;

            if (!arrModelNm[2].Contains("MBytes"))
                return modelNm;

            int capacity = ConvertUtil.ToInt32(arrModelNm[2].Replace("MBytes", "").Trim());
            int newCapacity = capacity / 1024;

            newModelNm = $"{arrModelNm[0]}/{arrModelNm[1]}/{newCapacity}GB";

            return newModelNm;
        }

        public JArray makeExamComplete()
        {
            var jArray = new JArray();
            int index = 0;
            foreach (DataRow row in _dt.Rows)
            {
                JObject jdata = new JObject();

                string componentCd = ConvertUtil.ToString(row["EXAM_COMPONENT_CD"]);
                string ltComponentCd = ConvertUtil.ToString(row["RECEIPT_COMPONENT_CD"]);

                long componentId = ConvertUtil.ToInt64(row["COMPONENT_ID"]);
                long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                jdata.Add("STATE", 1);

                jdata.Add("PART_ID", index++);
                jdata.Add("P_PART_ID", -1);
                jdata.Add("COMPONENT_CD", componentCd);
                jdata.Add("LT_MODEL_NM", ConvertUtil.ToString(row["RECEIPT_MODEL_NM"]));
                jdata.Add("LT_PART_CNT", ConvertUtil.ToInt32(row["RECEIPT_PART_CNT"]));

                jdata.Add("INVENTORY_ID", inventoryId);
                jdata.Add("COMPONENT_ID", componentId);
                jdata.Add("COMPONENT", ConvertUtil.ToString(row["COMPONENT"]));

                if (componentCd.Equals("NTB"))
                {
                    jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAM_MODEL_NM"]));
                    jdata.Add("MODEL_NM_SHORT", "노트북");
                }
                else if (componentCd.Equals("DKT"))
                {
                    jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAM_MODEL_NM"]));
                    jdata.Add("MODEL_NM_SHORT", "데스크탑");
                }
                else
                {
                    //if (componentCd.Equals("MEM") || componentCd.Equals("VGA"))
                    //{
                    //    bool isFault = ConvertUtil.ToBoolean(row["FAULT"]);
                    //    if (isFault)
                    //    {
                    //        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAM_MODEL_NM"]));
                    //        jdata.Add("MODEL_NM_SHORT", ConvertUtil.ToString(row["EXAM_MODEL_NM"]));
                    //    }
                    //    else
                    //    {
                    //        string modelNm = ConvertUtil.ToString(row["EXAM_MODEL_NM"]);
                    //        if (!string.IsNullOrEmpty(modelNm))
                    //        {
                    //            string[] splitedModelNm = modelNm.Split('/');

                    //            if (componentCd.Equals("MEM"))
                    //                jdata.Add("MODEL_NM_SHORT", $"{ splitedModelNm[0]}/{splitedModelNm[2]}");
                    //            else
                    //                jdata.Add("MODEL_NM_SHORT", splitedModelNm[1]);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAM_MODEL_NM"]));
                        jdata.Add("MODEL_NM_SHORT", ConvertUtil.ToString(row["EXAM_MODEL_NM"]));
                    //}
                }

                jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["EXAM_PART_CNT"]));
                jdata.Add("DIFF", ConvertUtil.ToString(row["COMPARE"]));

                jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                jdata.Add("ADJUST_DES", ConvertUtil.ToString(row["DES"]));

                jdata.Add("OFFER_PRICE", ConvertUtil.ToInt64(row["RECEIPT_PART_PRICE"]));
                jdata.Add("INIT_PRICE", ConvertUtil.ToInt64(row["EXAM_PART_PRICE"]));
                jdata.Add("ADJUST_PRICE", ConvertUtil.ToInt64(row["EXAM_ADJUST_PRICE"]));
                jdata.Add("TOTAL_PRICE", ConvertUtil.ToInt64(row["EXAM_TOTAL_PRICE"]));

                jArray.Add(jdata);

            }

            return jArray;
        }

        public void save()
        {
           
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            int failCnt = 0;

            DataRow[] rows = _dt.Select("STATE = 2 AND INVENTORY_ID > 0 AND EXAM_COMPONENT_CD NOT IN ('NTB', 'DKT')");
            DataRow[] rowsNtb = _dt.Select("STATE = 2 AND INVENTORY_ID > 0 AND EXAM_COMPONENT_CD IN ('NTB', 'DKT')");

            if(rows.Length < 1 && rowsNtb.Length < 1)
            {
                Dangol.Message("변경사항이 없습니다.");
                return;
            }

            Dangol.ShowSplash();

            var jArray = new JArray();
            JObject jobj = new JObject();
            JObject jResult = new JObject();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    JObject jdata = new JObject();
                    jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                    jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                    jdata.Add("INIT_PRICE", ConvertUtil.ToString(row["EXAM_PART_PRICE"]));
                    jdata.Add("ADJUST_PRICE", ConvertUtil.ToString(row["EXAM_ADJUST_PRICE"]));
                    jArray.Add(jdata);
                }

                jobj.Add("DATA", jArray);

                if (DBConnect.updateInventoryInfoBulk(jobj, ref jResult))
                {
                    gvPart.BeginDataUpdate();

                    foreach (DataRow row in rows)
                    {
                        row["STATE"] = 0;
                    }

                    gvPart.EndDataUpdate();
                    //Dangol.CloseSplash();
                    //Dangol.Message("저장되었습니다.");

                   
                }
                else
                {
                    failCnt += 1;
                    //Dangol.CloseSplash();
                    //Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }

            
            if (rowsNtb.Length > 0)
            {
                JObject jobj2 = new JObject();
                jArray.Clear();

                jobj2.Add("BULK_YN", 0);

                foreach (DataRow row in rowsNtb)
                {
                    JObject jdata = new JObject();
                    jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                    jArray.Add(jdata);
                }

                jobj2.Add("DATA", jArray);

                if (DBAdjustment.updateProductInfo(jobj2, ref jResult))
                {
                    gvPart.BeginDataUpdate();

                    foreach (DataRow row in rowsNtb)
                    {
                        row["STATE"] = 0;
                    }

                    gvPart.EndDataUpdate();

                   
                    //Dangol.CloseSplash();
                    //Dangol.Message("저장되었습니다.");
                }
                else
                {
                    failCnt += 2;
                    //Dangol.CloseSplash();
                    //Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }

            Dangol.CloseSplash();

            if (failCnt == 1)
            {
                Dangol.Message("부품 정보 수정에서 오류가 발생했습니다.");
            }
            else if (failCnt == 2)
            {
                Dangol.Message("제품 정보 수정에서 오류가 발생했습니다.");
            }
            else if (failCnt == 3)
            {
                Dangol.Message("부품 및 제품 정보 수정에서 오류가 발생했습니다.");
            }
            else
            {
                Dangol.Message("저장되었습니다.");
            }            
        }

        public bool refreshCheck()
        {
            Dangol.ShowSplash();

            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("STATE = 2 AND INVENTORY_ID > 0");

            Dangol.CloseSplash();

            if (rows.Length > 0)
                return false;
            else
                return true;
        }

        public bool CheckCheck()
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("CHECK = TRUE");

            if (rows.Length > 0)
                return true;
            else
                return false;
        }
        public int getCnt(string query)
        {
            int cnt = 0;
            DataRow[] rows = _dt.Select(query);

            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    cnt += ConvertUtil.ToInt32(row["EXAM_PART_CNT"]);
                }
            }


            return cnt;
        }


        public string getBarcode(ref string componentCd)
        {
            if (_currentRow == null)
            {
                componentCd = "";
                return "";
            }
            else
            {
                componentCd = _componentCd;
                return _barcode;
            }
        }

        public void getPrice()
        {

            long inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);

            if (inventoryId < 0)
            {
                Dangol.Message("검수 부품을 선택해 주세요.");
                return;
            }

            string componentCd = ConvertUtil.ToString(_currentRow["EXAM_COMPONENT_CD"]);

            if (componentCd.Equals("DKT") || componentCd.Equals("NTB"))
            {
                //componentCd = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["PART_COMPONENT_CD"]);
                return;
            }


            string LTcomponentCd = "CPU";

            string filterString = "-1";

            if (componentCd.Equals("CPU"))
            {
                string modelNm = ConvertUtil.ToString(_currentRow["EXAM_MODEL_NM"]);
                string[] sMdodelNm = modelNm.Split(' ');
                filterString = sMdodelNm[sMdodelNm.Length - 1];
            }
            else if (componentCd.Equals("MEM"))
            {
                string modelNm = ConvertUtil.ToString(_currentRow["EXAM_MODEL_NM"]);
                string[] sMdodelNm = modelNm.Split('/');

                string type;
                string capa;

                if (sMdodelNm[2].Contains("DDR3"))
                    type = "DDR3";
                else
                    type = "DDR4";

                capa = ConvertUtil.ToString(ConvertUtil.ToInt32(sMdodelNm[3].Replace("GB", "")));
                filterString = $"{capa}G";

                LTcomponentCd = ProjectInfo._dicLTComponentCdRevers[componentCd];
            }
            else if (componentCd.Equals("STG"))
            {
                string stgType = ConvertUtil.ToString(_currentRow["STG_TYPE"]);
                if (stgType.ToUpper().Contains("SSD"))
                    LTcomponentCd = "SSD";
                else
                    LTcomponentCd = "HDD";
            }
            else if (componentCd.Equals("MON"))
                LTcomponentCd = "MON";
            else
                LTcomponentCd = ProjectInfo._dicLTComponentCdRevers[componentCd];



            using (dlgGetPart getPart = new dlgGetPart(LTcomponentCd, filterString))
            {
                if (getPart.ShowDialog(this) == DialogResult.OK)
                {
                    _currentRow["EXAM_PART_PRICE"] = getPart._price;

                    long pric= getPart._price + ConvertUtil.ToInt64(_currentRow["EXAM_ADJUST_PRICE"]);
                    if (pric < 0)
                        _currentRow["EXAM_TOTAL_PRICE"] = 0;
                    else
                    _currentRow["EXAM_TOTAL_PRICE"] = pric;

                    _currentRow["STATE"] = 2;

                    int rowhandle = gvPart.FocusedRowHandle;
                    gvPart.FocusedRowHandle = -1;
                    gvPart.FocusedRowHandle = rowhandle;

                }
            }
        }

        public void returnRelease(long _warehousingId, ref long releaseReturnId, ref string releaseReceipt)
        {
            JObject jResult = new JObject();

            List<long> listInventoryId = new List<long>();

            DataRow[] rows = _dt.Select("CHECK = TRUE AND FAULT = FALSE");

            foreach (DataRow row in rows)
            {
                long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                listInventoryId.Add(inventoryId);
            }

            if (listInventoryId.Count > 0)
            {
                if (DBUsedPurchase.receiptUsedPurchaseReturn(_warehousingId, listInventoryId, "", ref jResult))
                {
                    releaseReturnId = ConvertUtil.ToInt64(jResult["RELEASE_ID"]);
                    releaseReceipt = ConvertUtil.ToString(jResult["RECEIPT"]);
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    return;
                }
            }

            List<long> listfaultId = new List<long>();
            DataRow[] rowsFault = _dt.Select("CHECK = TRUE AND FAULT = TRUE");
           

            foreach (DataRow row in rowsFault)
            {
                long faultId = ConvertUtil.ToInt64(row["FAULT_ID"]);
                listfaultId.Add(faultId);
            }

            if (listfaultId.Count > 0)
            {
                if (DBUsedPurchase.receiptUsedPurchaseFaultReturn(_warehousingId, listfaultId, "", ref jResult))
                {
                    releaseReturnId = ConvertUtil.ToInt64(jResult["RELEASE_ID"]);
                    releaseReceipt = ConvertUtil.ToString(jResult["RECEIPT"]);
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    return;
                }
            }

            Dangol.Message("접수되었습니다");
        }

        public void deletePart()
        {
            JObject jResult = new JObject();

            long receiptPartId = ConvertUtil.ToInt64(_currentRow["RECEIPT_PART_ID"]);

            if(receiptPartId == -1)
            {
                Dangol.Message("접수 부품을 선택해 주세요.");
                return;
            }
            //DBUsedPurchase.deleteUsedPartComponent(_receipt, receiptPartId, _currentRow["RECEIPT_ORIGINAL_COMPONENT_CD"], _currentRow["RECEIPT_PART_CNT"], _currentRow["EXAM_COMPONENT_CD"], ref jResult);

            JObject jobj = new JObject();
            jobj.Add("RECEIPT_ID", _receiptId);
            jobj.Add("RECEIPT", _receipt);
            jobj.Add("COMPONENT_CD", ConvertUtil.ToString(_currentRow["EXAM_COMPONENT_CD"]));
            jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(receiptPartId));
            jobj.Add("PARTCODE", ConvertUtil.ToString(_currentRow["RECEIPT_ORIGINAL_COMPONENT_CD"]));
            jobj.Add("PART_CNT", ConvertUtil.ToInt64(_currentRow["RECEIPT_PART_CNT"]));
            jobj.Add("SOURCE_CD", _sourceCd);

            if (DBUsedPurchase.deleteUsedPartComponent(jobj, ref jResult))   
            {
                gvPart.BeginDataUpdate();
                _listUsedPart.Remove(ConvertUtil.ToString(_currentRow["RECEIPT_PART_CODE"]));
                _currentRow.Delete();

                gvPart.EndDataUpdate();

                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }
      

        private void gvPart_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "EXAM_PART_PRICE" || e.Column.FieldName == "EXAM_ADJUST_PRICE" || e.Column.FieldName == "EXAM_TOTAL_PRICE")
            {
                long price = ConvertUtil.ToInt64(View.GetRowCellValue(e.RowHandle, View.Columns[e.Column.FieldName]));
                
                if(price < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
            else if (e.Column.FieldName == "EXAM_MODEL_NM")
            {
                string state = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]);

                if (state.Equals("2"))
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
            else if (e.Column.FieldName == "INVENTORY_CAT")
            {
                string inventoryCat = ConvertUtil.ToString(View.GetRowCellValue(e.RowHandle, View.Columns["INVENTORY_CAT"]));

                if (inventoryCat.Equals("F"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (inventoryCat.Equals("B"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
            }

        }

        private void gvPart_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "EXAM_PART_PRICE" || e.Column.FieldName == "EXAM_ADJUST_PRICE")
            {
                gvPart.BeginDataUpdate();

                long totalPrice = ConvertUtil.ToInt64(_currentRow["EXAM_PART_PRICE"]) + ConvertUtil.ToInt64(_currentRow["EXAM_ADJUST_PRICE"]);

                if(totalPrice < 0)
                    _currentRow["EXAM_TOTAL_PRICE"] = 0;
                else
                    _currentRow["EXAM_TOTAL_PRICE"] = totalPrice;
                gvPart.EndDataUpdate();
                totalCostChangeEvent();
            }
            else if (e.Column.FieldName == "EXAM_TOTAL_PRICE")
            {
                
            }

            if(e.Column.FieldName != "CHECK")
                _currentRow["STATE"] = 2;

        }

        private void riseCnt_EditValueChanged(object sender, EventArgs e)
        {
            //SpinEdit View = sender as SpinEdit;
            //long partId = ConvertUtil.ToInt64(_currentRow[""]);
            //JObject jResult = new JObject();

            //if (ConvertUtil.ToInt64(View.Value) < 0)
            //{
            //    Dangol.Message("양수만 입력 가능합니다.");
            //    View.Value = ConvertUtil.ToInt64(_currentRow["PART_CNT"]);
            //    return;
            //}

            //if (Dangol.MessageYN("선택하신 부품의 수량을 변경하시겠습니까?") == DialogResult.Yes)
            //{
            //    if (DBUsedPurchase.updateUsedPartCnt(_currentRow["PART_ID"], _receipt, View.Value, ref jResult))
            //    {
            //        _currentRow["TOTAL_PRICE"] = ConvertUtil.ToInt64(_currentRow["PRICE"]) * ConvertUtil.ToInt64(View.Value);
            //    }
            //    else
            //    {
            //        View.Value = ConvertUtil.ToInt64(_currentRow["PART_CNT"]);
            //        return;
            //    }
            //}
            //else
            //{
            //    View.Value = ConvertUtil.ToInt64(_currentRow["PART_CNT"]);
            //    return;
            //}
        }

        private void riseAdjustPrice_EditValueChanged(object sender, EventArgs e)
        {

        }

  
    }
}
