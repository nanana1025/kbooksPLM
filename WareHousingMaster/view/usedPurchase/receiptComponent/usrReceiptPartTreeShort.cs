using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WareHousingMaster.view.common;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using System.Text.RegularExpressions;

namespace WareHousingMaster.view.usedPurchase.receiptComponent
{
    public partial class usrReceiptPartTreeShort : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        //public DataRowView _currentRow { get; private set; }

        DataTable _dtUsedPart;
        DataTable _dtExamineComponent;
        DataTable _dtExamineInventory;

        DataTable _dtProduct;
        DataTable _dtProductInventory;

        public TreeListNode _FocusedNode { get; private set; }

        string _receipt;
        long _receiptId;
        string _receiptState;

        public long _totalCost { get; private set; }

        public List<string> _listUsedPart;
        List<string> _listPartUpdate;

        long _inventoryId;
        long _initPrice;
        long _adjustPrice;
        long _componentTotalCost;

        public delegate void TotalCostChangeHandler();
        public event TotalCostChangeHandler totalCostChangeEvent;
        public usrReceiptPartTreeShort()
        {
            InitializeComponent();

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("P_PART_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("PART_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("RECEIPT_PART_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("PART_COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("PART_LT_COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("RECEIPT_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("RECEIPT_PART_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("RECEIPT_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("RECEIPT_COST", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dt.Columns.Add(new DataColumn("LT_COMPONENT", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAMINE_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAMINE_PART_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("COMPARE", typeof(string)));
            _dt.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dt.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dt.Columns.Add(new DataColumn("PURCHASE_COST", typeof(long)));
            _dt.Columns.Add(new DataColumn("ADJUST_COST", typeof(long)));
            _dt.Columns.Add(new DataColumn("TOTAL_COST", typeof(long)));
            _dt.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));


            _dtUsedPart = new DataTable();
            _dtUsedPart.Columns.Add(new DataColumn("PART_ID", typeof(long)));
            _dtUsedPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtUsedPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtUsedPart.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtUsedPart.Columns.Add(new DataColumn("LT_COMPONENT", typeof(string)));
            _dtUsedPart.Columns.Add(new DataColumn("PRICE", typeof(long)));


            _dtExamineComponent = new DataTable();
            _dtExamineComponent.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("LT_COMPONENT", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtExamineComponent.Columns.Add(new DataColumn("PART_COST", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtExamineComponent.Columns.Add(new DataColumn("ADJUST_PRICE", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtExamineComponent.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtExamineComponent.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));


            _dtExamineInventory = new DataTable();
            _dtExamineInventory.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtExamineInventory.Columns.Add(new DataColumn("INIT_PRICE", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("ADJUST_PRICE", typeof(long)));
            _dtExamineInventory.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));

            _dtProduct = new DataTable();
            _dtProduct.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtProduct.Columns.Add(new DataColumn("INIT_PRICE", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("ADJUST_PRICE", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("TOTAL_COST", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtProduct.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));

            _dtProductInventory = new DataTable(); 
            _dtProductInventory.Columns.Add(new DataColumn("PRODUCT_ID", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtProductInventory.Columns.Add(new DataColumn("INIT_PRICE", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("ADJUST_PRICE", typeof(long)));
            _dtProductInventory.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));


            _bs = new BindingSource();

            tlPart.DataSource = null;
            _bs.DataSource = _dt;
            tlPart.DataSource = _bs;

            _listUsedPart = new List<string>();

            DataTable dtCompare = Util.getCodeList("CD1305", "KEY", "VALUE");
            Util.LookupEditHelper(rileCompare, dtCompare, "KEY", "VALUE");

            DataTable dtInventoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileExamineResult, dtInventoryCat, "KEY", "VALUE");

            DataTable dtInventoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState, dtInventoryState, "KEY", "VALUE");

            _listPartUpdate = new List<string>(new[] { "3", "6", "7" });

        }

        public void setinitialize(long receiptId, string receipt, string receiptState)
        {
            _receiptId = receiptId;
            _receipt = receipt;
            _receiptState = receiptState;
        }

        public void resetInfo(long receiptId, string receipt, string receiptState)
        {
            _receiptId = receiptId;
            _receipt = receipt;
            _receiptState = receiptState;
        }

        public void Clear()
        {
            _dt.Clear();
        }

        private void tlPart_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _FocusedNode = tlPart.FocusedNode;

            if (_FocusedNode != null)
            {
                _inventoryId = ConvertUtil.ToInt64(_FocusedNode["INVENTORY_ID"]);
                _initPrice = ConvertUtil.ToInt64(_FocusedNode["PURCHASE_COST"]);
                _adjustPrice = ConvertUtil.ToInt64(_FocusedNode["ADJUST_COST"]);
                _componentTotalCost = ConvertUtil.ToInt64(_FocusedNode["TOTAL_COST"]);
                long id = ConvertUtil.ToInt64(_FocusedNode["P_PART_ID"]);

                bool allow = false;

                if (id != -1)
                    if (_listPartUpdate.Contains(_receiptState))
                        allow = true;
                
                tlExamineResult.OptionsColumn.AllowEdit = allow;
                tlExamineDetail.OptionsColumn.AllowEdit = allow;
                tlPurchaseCost.OptionsColumn.AllowEdit = allow;
                tlAdjustCost.OptionsColumn.AllowEdit = allow;
                tlInventoryState.OptionsColumn.AllowEdit = allow;
            }
        }


        public void getComponentAll()
        {
            JObject jResult = new JObject();

            if (DBUsedPurchase.getReceiptExaminePartList(_receiptId, _receipt, 0, ref jResult))
            {
                setComponentTable(jResult);
                setComponentTree();
            }
            else
            {
                return;
            }
        }

        public void setComponentTree()
        {
            _dt.Clear();

            DataRow[] drPurchase;
            DataRow[] drExamComponent;
            DataRow[] drExamInventory;
            DataRow[] drTemp;

            string LTComponent;
            long index = 0;
            string compare = "0";
            foreach (string componentCd in ProjectInfo._componetCdLT)
            {
                drPurchase = _dtUsedPart.Select($"COMPONENT_CD = '{componentCd}'");

                if (drPurchase.Length > 0)
                {
                    foreach (DataRow row in drPurchase)
                    {
                        index++;
                        compare = "4";

                        LTComponent = ConvertUtil.ToString(row["LT_COMPONENT"]);
                        DataRow dr = _dt.NewRow();
                        dr["COMPONENT_CD"] = componentCd;
                        dr["PART_COMPONENT_CD"] = componentCd;
                        dr["PART_LT_COMPONENT_CD"] = componentCd;
                        dr["RECEIPT_MODEL_NM"] = row["MODEL_NM"];
                        dr["RECEIPT_PART_CNT"] = row["PART_CNT"];

                        dr["P_PART_ID"] = "-1";
                        dr["PART_ID"] = index;

                        dr["RECEIPT_PART_ID"] = row["PART_ID"];

                        dr["RECEIPT_COST"] = ConvertUtil.ToInt64(row["PRICE"]) * ConvertUtil.ToInt64(row["PART_CNT"]);
                        dr["RECEIPT_PRICE"] = ConvertUtil.ToInt64(row["PRICE"]);


                        drExamComponent = _dtExamineComponent.Select($"COMPONENT_CD = '{componentCd}' AND LT_COMPONENT = '{LTComponent}'");

                        if (drExamComponent.Length > 0)
                        {
                            List<object> listComponentId = new List<object>();
                            long CompPartCnt = 0;
                            long adjustTotalCost = 0;
                            long TotalCost = 0;


                            foreach (DataRow rowComponent in drExamComponent)
                            {
                                listComponentId.Add(rowComponent["COMPONENT_ID"]);
                                CompPartCnt += ConvertUtil.ToInt64(rowComponent["PART_CNT"]);
                                TotalCost += ConvertUtil.ToInt64(rowComponent["PRICE"]);
                                adjustTotalCost += ConvertUtil.ToInt64(rowComponent["ADJUST_PRICE"]);
                                rowComponent["CHECK"] = true;
                            }

                            dr["COMPONENT"] = drExamComponent[0]["COMPONENT"];
                            dr["EXAMINE_MODEL_NM"] = drExamComponent[0]["MODEL_NM"];
                            dr["EXAMINE_PART_CNT"] = CompPartCnt;

                            if (ConvertUtil.ToInt64(row["PART_CNT"]) == CompPartCnt)
                                compare = "1";
                            else
                                compare = "2";

                            dr["COMPARE"] = compare;

                            dr["PURCHASE_COST"] = TotalCost;
                            dr["ADJUST_COST"] = adjustTotalCost;
                            dr["TOTAL_COST"] = TotalCost + adjustTotalCost;

                            _totalCost += ConvertUtil.ToInt64(dr["TOTAL_COST"]);

                            dr["INVENTORY_ID"] = -1;
                            dr["COMPONENT_ID"] = drExamComponent[0]["COMPONENT_ID"];
                            _dt.Rows.Add(dr);

                            drExamInventory = _dtExamineInventory.Select($"COMPONENT_ID IN ({string.Join(",", listComponentId)})");

                            foreach (DataRow rowInventory in drExamInventory)
                            {
                                DataRow drSub = _dt.NewRow();
                                drSub["P_PART_ID"] = index;
                                drSub["PART_ID"] = ConvertUtil.ToInt64(rowInventory["INVENTORY_ID"]) * 100;
                                drSub["RECEIPT_PART_ID"] = -1;

                                drSub["COMPONENT"] = rowInventory["COMPONENT"];
                                drSub["PART_LT_COMPONENT_CD"] = componentCd;
                                
                                drTemp = _dtExamineComponent.Select($"COMPONENT_ID = {rowInventory["COMPONENT_ID"]}");
                                drSub["STG_TYPE"] = drTemp[0]["STG_TYPE"];
                                drSub["EXAMINE_MODEL_NM"] = drTemp[0]["MODEL_NM"];
                                drSub["EXAMINE_PART_CNT"] = 1;
                                drSub["COMPARE"] = compare;
                                drSub["INVENTORY_CAT"] = rowInventory["INVENTORY_CAT"];
                                drSub["INVENTORY_STATE"] = rowInventory["INVENTORY_STATE"];
                                
                                drSub["DES"] = rowInventory["DES"];
                                drSub["PURCHASE_COST"] = rowInventory["INIT_PRICE"];
                                drSub["ADJUST_COST"] = rowInventory["ADJUST_PRICE"];
                                //adjustTotalCost += ConvertUtil.ToInt64(rowInventory["ADJUST_PRICE"]);
                                drSub["TOTAL_COST"] = ConvertUtil.ToInt64(rowInventory["INIT_PRICE"]) + ConvertUtil.ToInt64(rowInventory["ADJUST_PRICE"]);

                                drSub["INVENTORY_ID"] = rowInventory["INVENTORY_ID"];
                                drSub["COMPONENT_ID"] = rowInventory["COMPONENT_ID"];

                                _dt.Rows.Add(drSub);
                            }
                        }
                        else
                        {
                            if (componentCd.Equals("노트북"))
                            {
                                dr["PURCHASE_COST"] = 0;
                                dr["ADJUST_COST"] = 0;
                                dr["TOTAL_COST"] = 0;
                                dr["COMPARE"] = "0";

                                dr["INVENTORY_ID"] = -1;
                                dr["COMPONENT_ID"] = -1;

                                _dt.Rows.Add(dr);
                            }
                            else if (componentCd.Equals("ETCADD") || componentCd.Equals("ETCMINUS"))
                            {
                                dr["COMPARE"] = "0";
                                dr["PURCHASE_COST"] = 0;
                                dr["ADJUST_COST"] = ConvertUtil.ToInt64(row["PRICE"]) * ConvertUtil.ToInt64(row["PART_CNT"]); ;
                                dr["TOTAL_COST"] = ConvertUtil.ToInt64(row["PRICE"]) * ConvertUtil.ToInt64(row["PART_CNT"]);

                                dr["INVENTORY_ID"] = -1;
                                dr["COMPONENT_ID"] = -1;

                                _totalCost += ConvertUtil.ToInt64(dr["TOTAL_COST"]);

                                _dt.Rows.Add(dr);
                            }
                            else
                            {
                                dr["PURCHASE_COST"] = 0;
                                dr["ADJUST_COST"] = 0;
                                dr["TOTAL_COST"] = 0;
                                dr["COMPARE"] = compare;

                                dr["INVENTORY_ID"] = -1;
                                dr["COMPONENT_ID"] = -1;

                                _dt.Rows.Add(dr);
                            }
                        }
                    }
                }


                if (componentCd.Equals("노트북") || componentCd.Equals("데스크탑"))
                {
                    drExamComponent = _dtProduct.Select($"COMPONENT_CD = '{componentCd}' AND CHECK = false");

                    if (drExamComponent.Length > 0)
                    {
                        foreach (DataRow rowComponent in drExamComponent)
                        {
                            index++;
                            compare = "3";
                            DataRow dr = _dt.NewRow();
                            dr["P_PART_ID"] = "-1";
                            dr["PART_ID"] = index;
                            dr["RECEIPT_PART_ID"] = -1;
                            
                            dr["COMPONENT_CD"] = componentCd;
                            dr["PART_COMPONENT_CD"] = componentCd;
                            dr["PART_LT_COMPONENT_CD"] = componentCd;
                            dr["COMPONENT"] = "";
                            dr["EXAMINE_MODEL_NM"] = rowComponent["MODEL_NM"];
                            dr["EXAMINE_PART_CNT"] = rowComponent["PART_CNT"];
                            dr["COMPARE"] = compare;
                            dr["PURCHASE_COST"] = ConvertUtil.ToInt64(rowComponent["INIT_PRICE"]);
                            dr["ADJUST_COST"] = rowComponent["ADJUST_PRICE"];
                            dr["TOTAL_COST"] = rowComponent["TOTAL_COST"];
                            rowComponent["CHECK"] = true;

                            dr["INVENTORY_ID"] = -1;
                            dr["COMPONENT_ID"] = rowComponent["PRODUCT_ID"];

                            _totalCost += ConvertUtil.ToInt64(dr["TOTAL_COST"]);

                            _dt.Rows.Add(dr);

                            drExamInventory = _dtProductInventory.Select($"PRODUCT_ID = {rowComponent["PRODUCT_ID"]}");

                            foreach (DataRow rowInventory in drExamInventory)
                            {
                                DataRow drSub = _dt.NewRow();
                                drSub["P_PART_ID"] = index;
                                drSub["PART_ID"] = ConvertUtil.ToInt64(rowInventory["INVENTORY_ID"]) * 100;
                                drSub["RECEIPT_PART_ID"] = -1;
                                drSub["PART_COMPONENT_CD"] = rowInventory["COMPONENT_CD"];
                                drSub["PART_LT_COMPONENT_CD"] = componentCd;
                                drSub["STG_TYPE"] = rowComponent["STG_TYPE"];
                                drSub["COMPONENT"] = rowInventory["COMPONENT"];
                                drSub["EXAMINE_MODEL_NM"] = rowInventory["MODEL_NM"];
                                drSub["EXAMINE_PART_CNT"] = 1;
                                drSub["COMPARE"] = compare;
                                drSub["INVENTORY_CAT"] = rowInventory["INVENTORY_CAT"];
                                drSub["INVENTORY_STATE"] = rowInventory["INVENTORY_STATE"];
                                
                                drSub["DES"] = rowInventory["DES"];
                                drSub["PURCHASE_COST"] = ConvertUtil.ToInt64(rowInventory["INIT_PRICE"]);
                                drSub["ADJUST_COST"] = rowInventory["ADJUST_PRICE"];
                                drSub["TOTAL_COST"] = ConvertUtil.ToInt64(rowInventory["INIT_PRICE"]) + ConvertUtil.ToInt64(rowInventory["ADJUST_PRICE"]);
                                drSub["INVENTORY_ID"] = rowInventory["INVENTORY_ID"];
                                drSub["COMPONENT_ID"] = rowInventory["COMPONENT_ID"];

                                _dt.Rows.Add(drSub);
                            }
                        }
                    }


                }
                else
                {
                    drExamComponent = _dtExamineComponent.Select($"COMPONENT_CD = '{componentCd}' AND CHECK = false");

                    if (drExamComponent.Length > 0)
                    {
                        foreach (DataRow rowComponent in drExamComponent)
                        {
                            index++;
                            compare = "3";
                            DataRow dr = _dt.NewRow();
                            dr["P_PART_ID"] = "-1";
                            dr["PART_ID"] = index;
                            dr["RECEIPT_PART_ID"] = -1;

                            dr["PART_COMPONENT_CD"] = componentCd;
                            dr["PART_LT_COMPONENT_CD"] = componentCd;
                            dr["STG_TYPE"] = rowComponent["STG_TYPE"];
                            dr["COMPONENT_CD"] = componentCd;
                            dr["COMPONENT"] = rowComponent["COMPONENT"];
                            dr["EXAMINE_MODEL_NM"] = rowComponent["MODEL_NM"];
                            dr["EXAMINE_PART_CNT"] = rowComponent["PART_CNT"];
                            dr["COMPARE"] = compare;
                            dr["PURCHASE_COST"] = ConvertUtil.ToInt64(rowComponent["PRICE"]);
                            dr["ADJUST_COST"] = rowComponent["ADJUST_PRICE"];
                            dr["TOTAL_COST"] = ConvertUtil.ToInt64(dr["PURCHASE_COST"]) + ConvertUtil.ToInt64(dr["ADJUST_COST"]);
                            rowComponent["CHECK"] = true;

                            dr["INVENTORY_ID"] = -1;
                            dr["COMPONENT_ID"] = rowComponent["COMPONENT_ID"];

                            _totalCost += ConvertUtil.ToInt64(dr["TOTAL_COST"]);

                            _dt.Rows.Add(dr);

                            drExamInventory = _dtExamineInventory.Select($"COMPONENT_ID = {rowComponent["COMPONENT_ID"]}");

                            foreach (DataRow rowInventory in drExamInventory)
                            {
                                DataRow drSub = _dt.NewRow();
                                drSub["P_PART_ID"] = index;
                                drSub["PART_ID"] = ConvertUtil.ToInt64(rowInventory["INVENTORY_ID"]) * 100;
                                drSub["RECEIPT_PART_ID"] = -1;

                                drSub["COMPONENT"] = rowInventory["COMPONENT"];

                                drSub["PART_COMPONENT_CD"] = rowInventory["COMPONENT_CD"];
                                drSub["PART_LT_COMPONENT_CD"] = componentCd;
                                drTemp = _dtExamineComponent.Select($"COMPONENT_ID = {rowInventory["COMPONENT_ID"]}");
                                drSub["STG_TYPE"] = drTemp[0]["STG_TYPE"];
                                drSub["EXAMINE_MODEL_NM"] = drTemp[0]["MODEL_NM"];
                                drSub["EXAMINE_PART_CNT"] = 1;
                                drSub["COMPARE"] = compare;
                                drSub["INVENTORY_CAT"] = rowInventory["INVENTORY_CAT"];
                                drSub["INVENTORY_STATE"] = rowInventory["INVENTORY_STATE"];
                                
                                drSub["DES"] = rowInventory["DES"];
                                drSub["PURCHASE_COST"] = ConvertUtil.ToInt64(rowInventory["INIT_PRICE"]);
                                drSub["ADJUST_COST"] = rowInventory["ADJUST_PRICE"];
                                drSub["TOTAL_COST"] = ConvertUtil.ToInt64(rowInventory["INIT_PRICE"]) + ConvertUtil.ToInt64(rowInventory["ADJUST_PRICE"]);

                                drSub["INVENTORY_ID"] = rowInventory["INVENTORY_ID"];
                                drSub["COMPONENT_ID"] = rowInventory["COMPONENT_ID"];

                                _dt.Rows.Add(drSub);
                            }
                        }
                    }
                }
            }
        }

        public void setComponentTable(JObject jResult)
        {
           
            _dtUsedPart.Clear();
            _dtExamineComponent.Clear();
            _dtExamineInventory.Clear();
            _dtProduct.Clear();
            _dtProductInventory.Clear();
            JArray jArray;
            if (Convert.ToBoolean(jResult["EXIST"]))
            {
                jArray = JArray.Parse(jResult["DATA"].ToString());

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtUsedPart.NewRow();

                    string componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                    if (ProjectInfo._dicLTComponentCd.ContainsKey(componentCd))
                        componentCd = ProjectInfo._dicLTComponentCd[componentCd];

                    dr["COMPONENT_CD"] = componentCd;
                    dr["PART_ID"] = obj["PART_ID"];
                    dr["MODEL_NM"] = obj["MODEL_NM"];
                    dr["PART_CNT"] = obj["PART_CNT"];
                    dr["LT_COMPONENT"] = obj["LT_COMPONENT"];
                    if (componentCd.Equals("ETCMINUS"))
                        dr["PRICE"] = ConvertUtil.ToInt64(obj["PRICE"]) * -1;
                    else
                        dr["PRICE"] = obj["PRICE"];
                    //dr["PRICE"] = obj["PRICE"];
                    _dtUsedPart.Rows.Add(dr);
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
                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    dr["LT_COMPONENT"] = obj["LT_COMPONENT"];
                    dr["MODEL_NM"] = obj["MODEL_NM"];
                    dr["PART_COST"] = obj["PART_COST"];
                    dr["PART_CNT"] = obj["PART_CNT"];
                    dr["ADJUST_PRICE"] = obj["ADJUST_PRICE"];
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
                    dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                    
                    dr["DES"] = obj["DES"];
                    dr["INIT_PRICE"] = obj["INIT_PRICE"];
                    dr["ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];

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
                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    dr["COMPONENT"] = obj["COMPONENT"];
                    dr["MODEL_NM"] = obj["MODEL_NM"];
                    dr["PART_CNT"] = obj["PART_CNT"];
                    dr["INIT_PRICE"] = obj["INIT_PRICE"];
                    dr["ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                    dr["TOTAL_COST"] = obj["TOTAL_COST"];
                    dr["STG_TYPE"] = obj["STG_TYPE"];
                    dr["CHECK"] = false;

                    _dtProduct.Rows.Add(dr);
                }

                jArray = JArray.Parse(jResult["PRODUCT_LIST_DATA"].ToString());

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtProductInventory.NewRow();
                    dr["PRODUCT_ID"] = obj["P_INVENTORY_ID"];
                    dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                    dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                    dr["COMPONENT"] = obj["COMPONENT"];
                    dr["BARCODE"] = obj["BARCODE"];
                    dr["MODEL_NM"] = obj["MODEL_NM"];
                    dr["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                    dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                    
                    dr["DES"] = obj["DES"];
                    dr["INIT_PRICE"] = obj["INIT_PRICE"];
                    dr["ADJUST_PRICE"] = obj["ADJUST_PRICE"];
                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    _dtProductInventory.Rows.Add(dr);
                }
            }
        }

        public JArray makeExamComplete()
        {
            var jArray = new JArray();
            Regex reg = new Regex(@"[0-9]*\.*[0-9]+");

            foreach (DataRow row in _dt.Rows)
            {
                JObject jdata = new JObject();

                string componentCd = ConvertUtil.ToString(row["PART_COMPONENT_CD"]);
                string ltComponentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);

                long componentId = ConvertUtil.ToInt64(row["COMPONENT_ID"]);
                long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                

                jdata.Add("PART_ID", ConvertUtil.ToInt32(row["PART_ID"]));
                jdata.Add("P_PART_ID", ConvertUtil.ToInt32(row["P_PART_ID"]));

                //jdata.Add("STATE", ConvertUtil.ToInt32(row["INVENTORY_CAT"]));
                jdata.Add("STATE", 1);

                jdata.Add("COMPONENT_CD", ConvertUtil.ToString(row["PART_COMPONENT_CD"]));
                jdata.Add("LT_COMPONENT_ID", ConvertUtil.ToString(row["LT_COMPONENT"]));
                jdata.Add("LT_MODEL_NM", ConvertUtil.ToString(row["RECEIPT_MODEL_NM"]));
                jdata.Add("LT_PART_CNT", ConvertUtil.ToInt32(row["RECEIPT_PART_CNT"]));

                jdata.Add("INVENTORY_ID", inventoryId);
                jdata.Add("COMPONENT_ID", componentId);
                jdata.Add("COMPONENT", ConvertUtil.ToString(row["COMPONENT"]));

                if (ltComponentCd.Equals("노트북"))
                {
                    jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                    jdata.Add("MODEL_NM_SHORT", "노트북");
                }
                else if (ltComponentCd.Equals("데스크탑"))
                {
                    jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                    jdata.Add("MODEL_NM_SHORT", "데스크탑");
                }
                else
                {
                    if (componentCd.Equals("MEM") || componentCd.Equals("VGA"))
                    {
                        string modelNm = ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]);
                        if (!string.IsNullOrEmpty(modelNm))
                        {
                            string[] splitedModelNm = modelNm.Split('/');
                            string strTmp = Regex.Replace(splitedModelNm[splitedModelNm.Length - 1], @"\D", "");
                            int capacity = int.Parse(strTmp) / 1024;
                            modelNm = "";
                            for (int i = 0; i < splitedModelNm.Length - 1; i++)
                                modelNm += $"{splitedModelNm[i].Trim()} / ";
                            modelNm += $"{capacity}GB";

                            jdata.Add("MODEL_NM", modelNm);

                            if (componentCd.Equals("MEM"))
                                jdata.Add("MODEL_NM_SHORT", $"{ splitedModelNm[0]}/{capacity}GB");
                            else
                                jdata.Add("MODEL_NM_SHORT", splitedModelNm[1]);
                        }
                    }
                    else if (componentCd.Equals("STG"))
                    {
                        string modelNm = ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]);
                        if (!string.IsNullOrEmpty(modelNm))
                        {
                            string[] splitedModelNm = modelNm.Split('/');

                           

                            Match match = reg.Match(splitedModelNm[splitedModelNm.Length - 1]);
                            if (match.Success)
                            {
                                string strTmp = match.Value.ToString();
                                
                                strTmp = string.Format("0{0}", strTmp);

                                double capacity = ConvertUtil.ToDouble(strTmp);

                                capacity = capacity * 1.073741824;


                                modelNm = "";
                                for (int i = 0; i < splitedModelNm.Length - 1; i++)
                                    modelNm += $"{splitedModelNm[i].Trim()} / ";
                                modelNm += $"{ConvertUtil.ToInt32(capacity)}GB";
                                
                            }
                        }

                        jdata.Add("MODEL_NM", modelNm);
                        jdata.Add("MODEL_NM_SHORT", modelNm);
                    }
                    else
                    {
                        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                        jdata.Add("MODEL_NM_SHORT", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                    }
                }


                jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["EXAMINE_PART_CNT"]));
                jdata.Add("DIFF", ConvertUtil.ToString(row["COMPARE"]));

                jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                jdata.Add("INVENTORY_STATE", ConvertUtil.ToString(row["INVENTORY_STATE"]));
                
                jdata.Add("ADJUST_DES", ConvertUtil.ToString(row["DES"]));

                jdata.Add("OFFER_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                jdata.Add("INIT_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                jdata.Add("ADJUST_PRICE", ConvertUtil.ToInt64(row["ADJUST_COST"]));
                jdata.Add("TOTAL_PRICE", ConvertUtil.ToInt64(row["TOTAL_COST"]));
                jdata.Add("DES", ConvertUtil.ToString(row["DES"]));




                //if (componentCd.Equals("노트북") || componentCd.Equals("데스크탑"))
                //{
                //    if (componentId >= 0 && inventoryId >= 0)
                //    {
                //        DataRow[] drows = _dt.Select($"PART_ID = {row["P_PART_ID"]}");

                //        //int inventoryCat
                //        jdata.Add("STATE", ConvertUtil.ToString(row["INVENTORY_CAT"]));

                //        jdata.Add("COMPONENT_CD", ConvertUtil.ToString(row["PART_COMPONENT_CD"]));
                //        jdata.Add("LT_COMPONENT_ID", ConvertUtil.ToString(drows[0]["LT_COMPONENT"]));
                //        jdata.Add("LT_MODEL_NM", ConvertUtil.ToString(drows[0]["RECEIPT_MODEL_NM"]));
                //        jdata.Add("LT_PART_CNT", 1);

                //        jdata.Add("INVENTORY_ID", inventoryId);
                //        jdata.Add("COMPONENT_ID", componentId);
                //        jdata.Add("COMPONENT", ConvertUtil.ToString(row["COMPONENT"]));
                //        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                //        jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["EXAMINE_PART_CNT"]));
                //        jdata.Add("DIFF", ConvertUtil.ToString(row["COMPARE"]));

                //        jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                //        jdata.Add("ADJUST_DES", ConvertUtil.ToString(row["DES"]));

                //        jdata.Add("OFFER_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("INIT_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("ADJUST_PRICE", ConvertUtil.ToInt64(row["ADJUST_COST"]));
                //        jdata.Add("TOTAL_PRICE", ConvertUtil.ToInt64(row["TOTAL_COST"]));
                //        jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                //    }
                //    if (componentId < 0 && inventoryId < 0)
                //    {

                //        //int inventoryCat
                //        jdata.Add("STATE", ConvertUtil.ToString(row["INVENTORY_CAT"]));

                //        jdata.Add("COMPONENT_CD", ConvertUtil.ToString(row["PART_COMPONENT_CD"]));
                //        jdata.Add("LT_COMPONENT_ID", ConvertUtil.ToString(row["LT_COMPONENT"]));
                //        jdata.Add("LT_MODEL_NM", ConvertUtil.ToString(row["RECEIPT_MODEL_NM"]));
                //        jdata.Add("LT_PART_CNT", ConvertUtil.ToInt32(row["RECEIPT_PART_CNT"]));

                //        jdata.Add("INVENTORY_ID", inventoryId);
                //        jdata.Add("COMPONENT_ID", componentId);
                //        jdata.Add("COMPONENT", ConvertUtil.ToString(row["COMPONENT"]));
                //        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                //        jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["EXAMINE_PART_CNT"]));
                //        jdata.Add("DIFF", ConvertUtil.ToString(row["COMPARE"]));

                //        jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                //        jdata.Add("ADJUST_DES", ConvertUtil.ToString(row["DES"]));

                //        jdata.Add("OFFER_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("INIT_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("ADJUST_PRICE", ConvertUtil.ToInt64(row["ADJUST_COST"]));
                //        jdata.Add("TOTAL_PRICE", ConvertUtil.ToInt64(row["TOTAL_COST"]));
                //        jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                //    }
                //}
                //else
                //{
                //    long componentId = ConvertUtil.ToInt64(row["COMPONENT_ID"]);
                //    long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);


                //    if (componentId >= 0 && inventoryId >= 0)
                //    {
                //        DataRow[] drows = _dt.Select($"PART_ID = {row["P_PART_ID"]}");

                //        //int inventoryCat
                //        jdata.Add("STATE", ConvertUtil.ToString(row["INVENTORY_CAT"]));

                //        jdata.Add("COMPONENT_CD", ConvertUtil.ToString(row["PART_COMPONENT_CD"]));
                //        jdata.Add("LT_COMPONENT_ID", ConvertUtil.ToString(drows[0]["LT_COMPONENT"]));
                //        jdata.Add("LT_MODEL_NM", ConvertUtil.ToString(drows[0]["RECEIPT_MODEL_NM"]));
                //        jdata.Add("LT_PART_CNT", 1);

                //        jdata.Add("INVENTORY_ID", inventoryId);
                //        jdata.Add("COMPONENT_ID", componentId);
                //        jdata.Add("COMPONENT", ConvertUtil.ToString(row["COMPONENT"]));
                //        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                //        jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["EXAMINE_PART_CNT"]));
                //        jdata.Add("DIFF", ConvertUtil.ToString(row["COMPARE"]));

                //        jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                //        jdata.Add("ADJUST_DES", ConvertUtil.ToString(row["DES"]));

                //        jdata.Add("OFFER_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("INIT_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("ADJUST_PRICE", ConvertUtil.ToInt64(row["ADJUST_COST"]));
                //        jdata.Add("TOTAL_PRICE", ConvertUtil.ToInt64(row["TOTAL_COST"]));
                //        jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                //    }
                //    if (componentId < 0 && inventoryId < 0)
                //    {

                //        //int inventoryCat
                //        jdata.Add("STATE", ConvertUtil.ToString(row["INVENTORY_CAT"]));

                //        jdata.Add("COMPONENT_CD", ConvertUtil.ToString(row["PART_COMPONENT_CD"]));
                //        jdata.Add("LT_COMPONENT_ID", ConvertUtil.ToString(row["LT_COMPONENT"]));
                //        jdata.Add("LT_MODEL_NM", ConvertUtil.ToString(row["RECEIPT_MODEL_NM"]));
                //        jdata.Add("LT_PART_CNT", ConvertUtil.ToInt32(row["RECEIPT_PART_CNT"]));

                //        jdata.Add("INVENTORY_ID", inventoryId);
                //        jdata.Add("COMPONENT_ID", componentId);
                //        jdata.Add("COMPONENT", ConvertUtil.ToString(row["COMPONENT"]));
                //        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["EXAMINE_MODEL_NM"]));
                //        jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["EXAMINE_PART_CNT"]));
                //        jdata.Add("DIFF", ConvertUtil.ToString(row["COMPARE"]));

                //        jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                //        jdata.Add("ADJUST_DES", ConvertUtil.ToString(row["DES"]));

                //        jdata.Add("OFFER_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("INIT_PRICE", ConvertUtil.ToInt64(row["PURCHASE_COST"]));
                //        jdata.Add("ADJUST_PRICE", ConvertUtil.ToInt64(row["ADJUST_COST"]));
                //        jdata.Add("TOTAL_PRICE", ConvertUtil.ToInt64(row["TOTAL_COST"]));
                //        jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                //    }
                //}

                jArray.Add(jdata);

            }


            return jArray;

            //_dt.Columns.Add(new DataColumn("P_PART_ID", typeof(long)));
            //_dt.Columns.Add(new DataColumn("PART_ID", typeof(long)));
            //_dt.Columns.Add(new DataColumn("RECEIPT_PART_ID", typeof(long)));
            //_dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            //_dt.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            //_dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            //_dt.Columns.Add(new DataColumn("RECEIPT_MODEL_NM", typeof(string)));
            //_dt.Columns.Add(new DataColumn("RECEIPT_PART_CNT", typeof(int)));
            //_dt.Columns.Add(new DataColumn("RECEIPT_PRICE", typeof(long)));
            //_dt.Columns.Add(new DataColumn("RECEIPT_COST", typeof(long)));
            //_dt.Columns.Add(new DataColumn("COMPONENT", typeof(string)));

            //_dt.Columns.Add(new DataColumn("LT_COMPONENT", typeof(string)));

            //_dt.Columns.Add(new DataColumn("EXAMINE_MODEL_NM", typeof(string)));
            //_dt.Columns.Add(new DataColumn("EXAMINE_PART_CNT", typeof(int)));
            //_dt.Columns.Add(new DataColumn("COMPARE", typeof(string)));
            //_dt.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            //_dt.Columns.Add(new DataColumn("DES", typeof(string)));
            //_dt.Columns.Add(new DataColumn("PURCHASE_COST", typeof(long)));
            //_dt.Columns.Add(new DataColumn("ADJUST_COST", typeof(long)));
            //_dt.Columns.Add(new DataColumn("TOTAL_COST", typeof(long)));

        }


        public void expandAll()
        {
            tlPart.ExpandAll();
        }

        public void foldAll()
        {
            tlPart.CollapseToLevel(0);
        }

        public void refresh()
        {    
            getComponentAll();
        }

        private void tlPart_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e != null && e.Node != null && e.Node["PART_ID"] != null && e.Node != tlPart.FocusedNode)
            {
                Color backColor = Color.Transparent;
                Color foreColor = Color.Black;

                if (!String.IsNullOrEmpty(ConvertUtil.ToString(e.Node["PART_ID"])))
                {
                    long id = ConvertUtil.ToInt64(e.Node["P_PART_ID"]);

                    switch (id)
                    {
                        case -1:
                            backColor = Color.Transparent;
                            foreColor = Color.Black;
                            break;
                        default:
                            backColor = Color.PapayaWhip;
                            foreColor = Color.Black;
                            break;
                    }

                    e.Appearance.BackColor = backColor;              
                }

                if (e.Column.FieldName.Equals("EXAMINE_MODEL_NM") || e.Column.FieldName.Equals("INVENTORY_STATE"))
                {
                    string value = ConvertUtil.ToString(e.Node["INVENTORY_STATE"]);

                    if (value.Equals("R"))
                        foreColor = Color.Red;
                    else if (value.Equals("X"))
                        foreColor = Color.BlueViolet;
                }

                e.Appearance.ForeColor = foreColor;
            }
            else if(e.Node == tlPart.FocusedNode)
            {
                Color foreColor = Color.Black;

                if (e.Column.FieldName.Equals("EXAMINE_MODEL_NM") || e.Column.FieldName.Equals("INVENTORY_STATE"))
                {
                    string value = ConvertUtil.ToString(e.Node["INVENTORY_STATE"]);

                    if (value.Equals("R"))
                        foreColor = Color.Red;
                    else if (value.Equals("X"))
                        foreColor = Color.BlueViolet;

                    e.Appearance.ForeColor = foreColor;
                }
            }
        }


        private void rileExamineResult_EditValueChanged(object sender, EventArgs e)
        {
            JObject jData = new JObject();
            JObject jResult = new JObject();
            LookUpEdit editor = (LookUpEdit)sender;

            jData.Add("INVENTORY_ID", _inventoryId);
            jData.Add("INVENTORY_CAT", ConvertUtil.ToString(editor.EditValue));

            if (DBConnect.updateInventoryDetail(jData, ref jResult))
            {
                
            }
        }

        private void rileInventoryState_EditValueChanged(object sender, EventArgs e)
        {
            JObject jData = new JObject();
            JObject jResult = new JObject();
            LookUpEdit editor = (LookUpEdit)sender;

            jData.Add("INVENTORY_ID", _inventoryId);
            jData.Add("INVENTORY_STATE", ConvertUtil.ToString(editor.EditValue));

            if (DBConnect.updateInventoryDetail(jData, ref jResult))
            {

            }
        }

        public DataRow[] getDaraRow()
        {
            DataRow[] rows = _dt.Select("P_PART_ID <> -1 AND INVENTORY_STATE = 'R'");

            return rows;
        }

        private void tlPart_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Node == null || e.Node["PART_ID"] == null)
            {
                return;
            }

            TreeListNode node = tlPart.FocusedNode;
            JObject jData = new JObject();
            JObject jResult = new JObject();
            jData.Add("INVENTORY_ID", _inventoryId);

            //if (e.Column.FieldName == "DES")
            //{
            //    jData.Add("DES", ConvertUtil.ToString(node["DES"]));

            //    if (DBConnect.updateInventoryDetail(jData, ref jResult))
            //    {

            //    }
            //}
            //else if (e.Column.FieldName == "ADJUST_COST")
            //{
            //    jData.Add("ADJUST_PRICE", ConvertUtil.ToInt64(node["ADJUST_COST"]));

            //    if (DBConnect.updateInventoryDetail(jData, ref jResult))
            //    {
            //        tlPart.BeginUpdate();
            //        node["TOTAL_COST"] = ConvertUtil.ToInt64(node["PURCHASE_COST"]) + ConvertUtil.ToInt64(node["ADJUST_COST"]);

            //        TreeListNode pNode = node.ParentNode;

            //        pNode["ADJUST_COST"] = ConvertUtil.ToInt64(pNode["ADJUST_COST"]) -_adjustPrice + ConvertUtil.ToInt64(node["ADJUST_COST"]);
            //        pNode["TOTAL_COST"] = ConvertUtil.ToInt64(pNode["PURCHASE_COST"]) + ConvertUtil.ToInt64(pNode["ADJUST_COST"]);
            //        _adjustPrice = ConvertUtil.ToInt64(node["ADJUST_COST"]);
            //        tlPart.EndUpdate();

            //        _totalCost = _totalCost - _componentTotalCost + ConvertUtil.ToInt64(pNode["TOTAL_COST"]);
            //        _componentTotalCost = ConvertUtil.ToInt64(pNode["TOTAL_COST"]);
            //        totalCostChangeEvent();
            //    }
            //}
            //else if (e.Column.FieldName == "PURCHASE_COST")
            //{
            //    jData.Add("INIT_PRICE", ConvertUtil.ToInt64(node["PURCHASE_COST"]));

            //    if (DBConnect.updateInventoryDetail(jData, ref jResult))
            //    {
            //        tlPart.BeginUpdate();
            //        node["TOTAL_COST"] = ConvertUtil.ToInt64(node["PURCHASE_COST"]) + ConvertUtil.ToInt64(node["ADJUST_COST"]);
            //        TreeListNode pNode = node.ParentNode;

            //        pNode["PURCHASE_COST"] = ConvertUtil.ToInt64(pNode["PURCHASE_COST"]) - _initPrice + ConvertUtil.ToInt64(node["PURCHASE_COST"]);
            //        pNode["TOTAL_COST"] = ConvertUtil.ToInt64(pNode["PURCHASE_COST"]) + ConvertUtil.ToInt64(pNode["ADJUST_COST"]);
            //        _initPrice = ConvertUtil.ToInt64(node["PURCHASE_COST"]);

            //        tlPart.EndUpdate();

            //        _totalCost = _totalCost - _componentTotalCost + ConvertUtil.ToInt64(pNode["TOTAL_COST"]);
            //        _componentTotalCost = ConvertUtil.ToInt64(pNode["TOTAL_COST"]);
            //        totalCostChangeEvent();
            //    }
            //}
        }

        private void risePurchaseCost_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //_FocusedNode["TOTAL_COST"] = ConvertUtil.ToInt64(_FocusedNode["ADJUST_COST"]) + ConvertUtil.ToInt64(e.NewValue);
        }

        private void rileAdjustCost_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //_FocusedNode["TOTAL_COST"] = ConvertUtil.ToInt64(_FocusedNode["PURCHASE_COST"]) + ConvertUtil.ToInt64(e.NewValue);
        }

       
    }
}
