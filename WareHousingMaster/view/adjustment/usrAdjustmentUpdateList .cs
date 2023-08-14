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

namespace WareHousingMaster.view.adjustment
{
    public partial class usrAdjustmentUpdateList : DevExpress.XtraEditors.XtraForm
    {
        string _representativeType = "W";
        string _representativeCol = "WAREHOUSING";
        string _representativeIdCol = "WAREHOUSING_ID";
        string _representativeNo = null;
        long _representativeId = -1;

        DataTable _dtBarcode;
        DataTable _dt;
        DataTable _dtRepairCheck;

        DataTable _dtExamine;
        DataTable _dtRepair;

        DataTable _dtExaminePrice;
        DataTable _dtRepairPrice;

        BindingSource _bsWarehousing;
        BindingSource _bs;

        DataRowView _currentRow = null;
  
        List<long> _listWarehousingId;

        List<string> _listBarcode;

        string[] _NTBCOLNAME2ND = null;

        DataTable _dtNTBAdjustmentPrice = null;
        Dictionary<string, short> _dicProductCheck = null;
        Dictionary<string, long> _dicAdjustmentPrice = null;
        List<string> _listCaseCheckCol = null;
        string _etcDes = "";
        string _batteryRemain = "";
        string _productGrade = "";


        string _barcode = "";

        public usrAdjustmentUpdateList()
        {
            InitializeComponent();

            _dtBarcode = new DataTable();
            _dtBarcode.Columns.Add(new DataColumn("BARCODE", typeof(string)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("INVENTORY_PTYPE", typeof(int)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("ADJUSTMENT_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("APPROVAL_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("PRODUCT_ADJUST_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("ADJUST_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dt.Columns.Add(new DataColumn("CPU_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("MON_SIZE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dt.Columns.Add(new DataColumn("CHECK_TYPE", typeof(int))); //1:EXAMINE, 2:REPAIR
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));

            _dt.Columns.Add(new DataColumn("EXAM_CHECK_YN", typeof(string)));
            _dt.Columns.Add(new DataColumn("REPAIR_CHECK_YN", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXAM_ADJUST_YN", typeof(string)));
            _dt.Columns.Add(new DataColumn("REPAIR_ADJUST_YN", typeof(string)));


            _bsWarehousing = new BindingSource();
            _bs = new BindingSource();

            //_bsWarehousing.DataSource = _dtBarcode;
            //_bs.DataSource = _dt;

            _listWarehousingId = new List<long>();
            _listBarcode = new List<string>();

            _dicProductCheck = new Dictionary<string, short>();
            _dicAdjustmentPrice = new Dictionary<string, long>();
            _dtNTBAdjustmentPrice = new DataTable();

            _listCaseCheckCol = new List<string>(new[] { "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED", "CASE_HINGE", "COOLER" });

            _NTBCOLNAME2ND = new string[]{ //            "CASE_HINGE" COOLER 별도 처리
            "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED","CASE_HINGE", "COOLER",
            "DISPLAY", "USB", "MOUSEPAD", "KEYBOARD", "BATTERY",
            "CAM", "ODD", "HDD", "LAN_WIRELESS", "LAN_WIRED", "BIOS", "OS", "TEST_CHECK" };
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

            
            //getUsedPurchaseList(ref jResult);

        }

        private void setInfoBox()
        {
            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

            DataTable dtdjustmentState = Util.getCodeList("CD1501", "KEY", "VALUE");
            Util.LookupEditHelper(rileAdjustmentState, dtdjustmentState, "KEY", "VALUE");
            Util.LookupEditHelper(leAdjustmentState, dtdjustmentState, "KEY", "VALUE");
            

            DataTable dtCategory = Util.getCodeList("CD1101", "KEY", "VALUE");
            Util.LookupEditHelper(rileCategory, dtCategory, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(rileNickName, dtNickName, "KEY", "VALUE");
            Util.insertRowonTop(dtNickName, "-1", "선택");
            Util.LookupEditHelper(leNickName, dtNickName, "KEY", "VALUE");

            //DataTable dtProductGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
            //Util.LookupEditHelper(rileProductGrade, dtProductGrade, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(rileComapnyId2, dtCompany, "KEY", "VALUE");
            

            DataTable dtWarehousingState = Util.getCodeList("CD0601", "KEY", "VALUE");
            Util.LookupEditHelper(rileWarehousingState, dtWarehousingState, "KEY", "VALUE");

            DataTable dtDeviceType = new DataTable();

            dtDeviceType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtDeviceType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._arrTypeNm.Length; i++)
            {
                DataRow dr = dtDeviceType.NewRow();

                dr["KEY"] = i;
                dr["VALUE"] = ProjectInfo._arrTypeNm[i];
                dtDeviceType.Rows.Add(dr);
            }

            Util.LookupEditHelper(rileProductType, dtDeviceType, "KEY", "VALUE");
            Util.LookupEditHelper(leProductType, dtDeviceType, "KEY", "VALUE");
            


            //DataTable dtHinge = new DataTable();

            //dtHinge.Columns.Add(new DataColumn("KEY", typeof(int)));
            //dtHinge.Columns.Add(new DataColumn("VALUE", typeof(string)));
            //Util.insertRowonTop(dtHinge, 0, "");
            //Util.insertRowonTop(dtHinge, 1, "힌지파손");

            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");
            Util.insertRowonTop(dtmanufactureType, -1, "선택");
            Util.LookupEditHelper(leManufactureType, dtmanufactureType, "KEY", "VALUE");


            var today = DateTime.Today;
           var pastDate = today.AddDays(-30);

           deDtFrom.EditValue = pastDate;
           deDtTo.EditValue = today;
           leReceiptState.ItemIndex = 0;


            _dtNTBAdjustmentPrice.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtNTBAdjustmentPrice.Columns.Add(new DataColumn("EXIST", typeof(bool)));

            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                _dtNTBAdjustmentPrice.Columns.Add(new DataColumn(col, typeof(long)));

            for (int i = 1; i < 6; i++)
            {
                DataRow dr = _dtNTBAdjustmentPrice.NewRow();

                dr["TYPE"] = i;
                dr["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    dr[col] = 0;

                _dtNTBAdjustmentPrice.Rows.Add(dr);
            }

        }

        private void setIInitData()
        {
            gcWarehousingList.DataSource = null;
            _bsWarehousing.DataSource = _dtBarcode;
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

                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
            }
            else
            {
                _currentRow = null;
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
            }
            else
            {
                _currentRow = null;
            }
        }

        private void getWarehousingList(List<string> listBarcode)
        {
            _dtBarcode.Clear();

            foreach (string barcode in listBarcode)
            {
                DataRow dr = _dtBarcode.NewRow();

                dr["BARCODE"] = barcode;
                _dtBarcode.Rows.Add(dr);
            }
              
        }

        private void setCheckInfo(long inventoryId, ref DataRow dr)
        {

            DataRow[] rowsExamCheck = _dtExamine.Select($"INVENTORY_ID = {inventoryId}");
            DataRow[] rowsRepairCheck = _dtRepair.Select($"INVENTORY_ID = {inventoryId}");

            if (rowsExamCheck.Length < 1 && rowsRepairCheck.Length < 1)
            {
                dr["CHECK_YN"] = false;
                dr["CHECK_TYPE"] = 0;
                return;
            }
            else
            {
                dr["CHECK_YN"] = true;
                if (rowsRepairCheck.Length > 0)
                {
                    DataRow rowExamCheck = rowsExamCheck[0];
                    DataRow rowRepairCheck = rowsRepairCheck[0];

                    DataRow drRepairCheck = _dtRepairCheck.NewRow();
                    drRepairCheck["INVENTORY_ID"] = inventoryId;

                    dr["PRODUCT_GRADE"] = rowRepairCheck["PRODUCT_GRADE"];
                    dr["CASE_DES"] = rowRepairCheck["CASE_DES"];
                    dr["CASE_HINGE"] = rowRepairCheck["CASE_HINGE"];
                    dr["CHECK_TYPE"] = 2;

                    if (rowExamCheck["CASE_HINGE"] == rowRepairCheck["CASE_HINGE"])
                        drRepairCheck["CASE_HINGE"] = 0;
                    //else if(ConvertUtil.ToInt32(rowExamCheck["CASE_HINGE"]) < ConvertUtil.ToInt32(rowRepairCheck["CASE_HINGE"]))
                    //    drRepairCheck["CASE_HINGE"] = 1;
                    else
                        drRepairCheck["CASE_HINGE"] = 2;

                    int checkValue = 0;
                    List<string> checkList;
                    string content;

                    foreach (string col in ExamineInfo._NTBCOLNAME2ND)
                    {
                        content = "";
                        checkValue = ConvertUtil.ToInt32(rowRepairCheck[col]);

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

                            dr[col] = content.Substring(0, content.Length - 1);
                        }
                        else
                            dr[col] = "";

                        if (rowExamCheck[col] == rowRepairCheck[col])
                            drRepairCheck[col] = 0;
                        //else if (ConvertUtil.ToInt32(rowExamCheck[col]) < ConvertUtil.ToInt32(rowRepairCheck[col]))
                        //    drRepairCheck[col] = 1;
                        else
                            drRepairCheck[col] = 2;
                    }

                    checkValue = ConvertUtil.ToInt32(rowExamCheck["BATTERY"]);
                    if ((checkValue & ExamineInfo._BASE[4]) == ExamineInfo._BASE[4])
                    {
                        string batteryRemain = ConvertUtil.ToString(rowExamCheck["BATTERY_REMAIN"]);
                        dr["BATTERY"] += $"{dr["BATTERY"]}[{ batteryRemain}%]";
                    }

                    _dtRepairCheck.Rows.Add(drRepairCheck);
                }
                else
                {
                    DataRow rowExamCheck = rowsExamCheck[0];
                    dr["PRODUCT_GRADE"] = rowExamCheck["PRODUCT_GRADE"];
                    dr["CASE_DES"] = rowExamCheck["CASE_DES"];
                    dr["CASE_HINGE"] = rowExamCheck["CASE_HINGE"];
                    dr["CHECK_TYPE"] = 1;

                    int checkValue = 0;
                    List<string> checkList;
                    string content;

                    foreach (string col in ExamineInfo._NTBCOLNAME2ND)
                    {
                        content = "";
                        checkValue = ConvertUtil.ToInt32(rowExamCheck[col]);

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

                            dr[col] = content;
                        }
                        else
                            dr[col] = "";
                    }

                    checkValue = ConvertUtil.ToInt32(rowExamCheck["BATTERY"]);
                    if ((checkValue & ExamineInfo._BASE[4]) == ExamineInfo._BASE[4])
                        dr["BATTERY"] += $"{dr["BATTERY"]}[{ rowExamCheck["BATTERY_REMAIN"]}%]";
                }
            }
        }



        private void setProductCheckData(JArray jArray)
        {
            int manufactureType;
            foreach (JObject obj in jArray.Children<JObject>())
            {
                DataRow dr = _dt.NewRow();

                string approvalType = ConvertUtil.ToString(obj["APPROVAL_TYPE"]);
                string repairUserId = ConvertUtil.ToString(obj["REPAIR_USER_ID"]);
                manufactureType = ConvertUtil.ToInt32(obj["MANUFACTURE_TYPE"]);

                dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                dr["INVENTORY_PTYPE"] = ConvertUtil.ToInt32(obj["INVENTORY_PTYPE"]);
                dr["CHECK"] = false;
                dr["ADJUSTMENT_STATE"] = obj["ADJUSTMENT_STATE"];
                dr["APPROVAL_TYPE"] = obj["APPROVAL_TYPE"];
                dr["ADJUSTMENT_STATE"] = obj["ADJUSTMENT_STATE"];
                if (approvalType.Equals("G") || approvalType.Equals("U"))
                    dr["APPROVAL_USER_ID"] = obj["APPROVAL_USER1"];
                else if (approvalType.Equals("S") || approvalType.Equals("M"))
                    dr["APPROVAL_USER_ID"] = obj["APPROVAL_USER2"];
                else
                    dr["APPROVAL_USER_ID"] = "-1";
                dr["COMPANY_ID"] = obj["COMPANY_ID"];
                dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                dr["WAREHOUSING"] = obj["WAREHOUSING"];
                dr["BARCODE"] = obj["BARCODE"];
                if (manufactureType == 1)
                    dr["PRODUCT_PRICE"] = obj["LT_DEALER_PRICE_MAJOR"];
                else
                    dr["PRODUCT_PRICE"] = obj["LT_DEALER_PRICE_ETC"];

                //dr["PRODUCT_ADJUST_PRICE"] = obj["PRODUCT_ADJUST_PRICE"];
                dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                dr["MANUFACTURE_NM"] = obj["MANUFACTURE_NM"];
                dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NM"];
                dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                dr["CPU_MODEL_NM"] = obj["CPU_MODEL_NM"];

                dr["NTB_LIST_ID"] = obj["NTB_LIST_ID"];

                dr["EXAM_CHECK_YN"] = obj["EXAM_CHECK_YN"];
                dr["REPAIR_CHECK_YN"] = obj["REPAIR_CHECK_YN"];
                dr["EXAM_ADJUST_YN"] = obj["EXAM_ADJUST_YN"];
                dr["REPAIR_ADJUST_YN"] = obj["REPAIR_ADJUST_YN"];

                dr["STATE"] = 0;
                

                _dt.Rows.Add(dr);
            }
        }
        private bool geList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dt.Clear();

            jData.Add("LIST_BARCODE_ID", string.Join(",", _listBarcode));

            if (DBAdjustment.getProductListByBarcode(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    setProductCheckData(jArray);
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
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
                dtFrom = $"{deDtFrom.Text} 00:00:00";

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
                dtTo = $"{deDtTo.Text} 23:59:59";

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

            if (diffDay > 90)
            {
                jData.Add("MSG", "검색 기간은 90일을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("CREATE_DT_S", dtFrom);
            jData.Add("CREATE_DT_E", dtTo);




            //if (!string.IsNullOrEmpty(ConvertUtil.ToString(teReceip.Text)))
            //    jData.Add("RECEIPT", teReceip.Text);

            //if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCustomerNm.Text)))
            //    jData.Add("USER_NM", teCustomerNm.Text);

            //if (!teTel.Text.Equals(ConvertUtil.ToString(teTel.Text)))
            //    jData.Add("TEL", teTel.Text);

            //if (ConvertUtil.ToInt32(leUserType1.EditValue) >= 0)
            //    jData.Add("USER_TYPE1", ConvertUtil.ToInt32(leUserType1.EditValue));

            //if (ConvertUtil.ToInt32(leUserType2.EditValue) >= 0)
            //    jData.Add("USER_TYPE2", ConvertUtil.ToInt32(leUserType2.EditValue));

            if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
                jData.Add("PRODUCT_GRADE", ConvertUtil.ToString(leReceiptState.EditValue));

            return true;
        }

        private void setNtbCheck(long inventoryId, short _checkType, int manufactureType, long ntbListId)
        {
            JObject jResult = new JObject();
            _dicProductCheck.Clear();
            _etcDes = "";
            _batteryRemain = "";
            _productGrade = "0";

            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                _dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;


            foreach (string col in _NTBCOLNAME2ND)
                _dicProductCheck.Add(col, 0);



            DBConnect.insertNtbCheck("W", _representativeType, _representativeCol, "","",_barcode, inventoryId, _checkType, _dicProductCheck, new List<long>(new[] { inventoryId }), _etcDes, _batteryRemain, _productGrade, 1, false);


            if (!DBAdjustment.insertAdjustmentPrice(inventoryId, _checkType, "insertAdjustmentPrice", ExamineInfo._listAdjustmentPriceColShort, _dtNTBAdjustmentPrice, ref jResult))
            {
                //Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }



        private void setAdjustment(long inventoryId, string warehousing, short _checkType, int manufactureType, long ntbListId)
        {
            JObject jResult = new JObject();
            string typeNm = "NTB";
            _dicProductCheck.Clear();
            _etcDes = "";
            _batteryRemain = "";
            _productGrade = "";

            typeNm = "NTB";
            _dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
           

            if (DBConnect.getCheckInfo(inventoryId, typeNm, _checkType, ref jResult))
            {
                JObject jData = (JObject)jResult["DATA"];

                foreach (var x in jData)
                {
                    string name = x.Key;
                    if (!ProjectInfo._listCheckException.Contains(name))
                    {
                        if (name.Equals("CASE_DES"))
                            _etcDes = x.Value.ToObject<string>();
                        else if (name.Equals("BATTERY_REMAIN"))
                            _batteryRemain = x.Value.ToObject<string>();
                        else if (name.Equals("PRODUCT_GRADE"))
                            _productGrade = x.Value.ToObject<string>();
                        else
                        {
                            short value = x.Value.ToObject<short>();

                            if (!_dicProductCheck.ContainsKey(name))
                                _dicProductCheck.Add(name, value);
                        }
                    }
                }

                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    _dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;


                _dicAdjustmentPrice.Clear();
                if (DBConnect.getNTBAdjustmentPrice(warehousing, "W", ntbListId, ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string col = "";
                    if (manufactureType == 1)
                        col = "MAJOR_PRICE";
                    else
                        col = "ETC_PRICE";

                    bool isCheck;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string key = ConvertUtil.ToString(obj["COL_NM"]);
                        isCheck = false;
                        if(key.Equals("CASES"))
                        {
                            
                            foreach(string caseCol in _listCaseCheckCol)
                            {
                                if(_dicProductCheck[caseCol] > 0)
                                {
                                    isCheck = true;
                                    break;
                                }         
                            }
                        }
                        else 
                        {
                            if(_dicProductCheck.ContainsKey(key))
                                if (_dicProductCheck[key] > 0)
                                    isCheck = true;
                        }

                        if(isCheck)
                        {
                            _dtNTBAdjustmentPrice.Rows[_checkType][key] = ConvertUtil.ToInt64(obj[col]);
                        }
                    }
                }

                if (!DBAdjustment.insertAdjustmentPrice(inventoryId, _checkType, "insertAdjustmentPrice", ExamineInfo._listAdjustmentPriceColShort, _dtNTBAdjustmentPrice, ref jResult))
                {
                    //Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }

            }
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
                        jdata.Add("ADJUSTMENT_STATE", ConvertUtil.ToInt64(row["ADJUSTMENT_STATE"]));
                        jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        jdata.Add("INVENTORY_PTYPE", ConvertUtil.ToString(row["INVENTORY_PTYPE"]));
                        //jdata.Add("ADJUST_PRICE", ConvertUtil.ToString(row["ADJUST_PRICE"]));
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

                if (Dangol.MessageYN("선택하신 제품의 차감가를 계산하시겠습니까?(제조사, 상세스펙 저장된 제품만)") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();
                    gvList.BeginDataUpdate();
                    _dt.BeginInit();
                    long inventoryId = -1;
                    foreach (DataRow row in rows)
                    {
                        inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);

                        string examCheckYn = ConvertUtil.ToString(row["EXAM_CHECK_YN"]);
                        string repairCheckYn = ConvertUtil.ToString(row["REPAIR_CHECK_YN"]);
                        string examAdjustYn = ConvertUtil.ToString(row["EXAM_ADJUST_YN"]);
                        string repairAdjustYn = ConvertUtil.ToString(row["REPAIR_ADJUST_YN"]);
                        string warehousing = ConvertUtil.ToString(row["WAREHOUSING"]);

                        int manufactureType = ConvertUtil.ToInt32(row["MANUFACTURE_TYPE"]);
                        long ntbListId = ConvertUtil.ToInt64(row["NTB_LIST_ID"]);

                        if (manufactureType < 1 || ntbListId < 1)
                            continue;

                        if (examCheckYn.Equals("N") && examAdjustYn.Equals("N"))
                        {
                            setNtbCheck(inventoryId, 1, manufactureType, ntbListId);
                            row["EXAM_CHECK_YN"] = "Y";
                            row["EXAM_ADJUST_YN"] = "Y";
                        }

                        if (examCheckYn.Equals("Y"))
                        //if (examCheckYn.Equals("Y")  && examAdjustYn.Equals("N"))
                        {
                            setAdjustment(inventoryId, warehousing, 1, manufactureType, ntbListId);
                            row["EXAM_ADJUST_YN"] = "Y";
                        }

                        if (repairCheckYn.Equals("Y"))
                        //if (repairCheckYn.Equals("Y") && repairAdjustYn.Equals("N"))
                        {
                            setAdjustment(inventoryId, warehousing, 3, manufactureType, ntbListId);
                            row["REPAIR_ADJUST_YN"] = "Y";
                        }

                    }
                    _dt.EndInit();
                    gvList.EndDataUpdate();
                    Dangol.CloseSplash();
                    Dangol.Message("처리되었습니다.");
                }
            }
            else if (e.Button.Properties.Tag.Equals(5))
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

                    bool isAdmin = false;
                    string approvalType = "";
                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_TYPE", ProjectInfo._userType);
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                        isAdmin = true;

                    if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                    {
                        jobj.Add("ADJUSTMENT_STATE", 1);
                        jobj.Add("APPROVAL_USER2", ProjectInfo._userId);
                        jobj.Add("APPROVAL_DT2", now);
                    }
                    else
                    {
                        jobj.Add("APPROVAL_USER1", ProjectInfo._userId);
                        jobj.Add("APPROVAL_DT1", now);
                    }
                    List<long> listInventoryId = new List<long>();
                    foreach (DataRow row in rows)
                    {
                        if (isAdmin)
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        else
                        {
                            approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);
                            if (!approvalType.Equals("M") && !approvalType.Equals("S"))
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        now = DateTime.Today.ToString("yyyy-MM-dd");

                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            foreach (DataRow row in rows)
                            {
                                row["APPROVAL_TYPE"] = ProjectInfo._userType;
                                row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                row["ADJUSTMENT_STATE"] = 1;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in rows)
                            {
                                if (isAdmin)
                                    listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                                else
                                {
                                    approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);
                                    if (!approvalType.Equals("M") && !approvalType.Equals("S"))
                                    {
                                        row["APPROVAL_TYPE"] = ProjectInfo._userType;
                                        row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                    }
                                }
                            }                      
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
            else if (e.Button.Properties.Tag.Equals(9))
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

                if (Dangol.MessageYN("선택하신 제품을 승인취소하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_TYPE", "");
                    jobj.Add("APPROVAL_USER2", "");
                    jobj.Add("APPROVAL_DT2", "-1");
                    jobj.Add("APPROVAL_DT1", "-1");

                    List<long> listInventoryId = new List<long>();
                    foreach (DataRow row in rows)
                    {
                        listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        foreach (DataRow row in rows)
                        {
                            row["APPROVAL_TYPE"] = "";
                            row["APPROVAL_USER_ID"] = "";
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
            else if (e.Button.Properties.Tag.Equals(5))
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

                    bool isAdmin = false;
                    string approvalType = "";
                    jobj.Add("BULK_YN", 1);
                    jobj.Add("APPROVAL_TYPE", ProjectInfo._userType);

                    if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                        isAdmin = true;

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
                        if (isAdmin)
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        else
                        {
                            approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);
                            if (!approvalType.Equals("M") && !approvalType.Equals("S"))
                                listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                    {
                        gvList.BeginDataUpdate();

                        if (ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("M"))
                        {
                            foreach (DataRow row in rows)
                            {
                                row["APPROVAL_TYPE"] = ProjectInfo._userType;
                                row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                row["ADJUSTMENT_STATE"] = 1;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in rows)
                            {
                                if (isAdmin)
                                    listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                                else
                                {
                                    approvalType = ConvertUtil.ToString(row["APPROVAL_TYPE"]);
                                    if (!approvalType.Equals("M") && !approvalType.Equals("S"))
                                    {
                                        row["APPROVAL_TYPE"] = ProjectInfo._userType;
                                        row["APPROVAL_USER_ID"] = ProjectInfo._userId;
                                    }
                                }
                            }
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
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dt.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 제품으로 정산을 진행하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    List<long> listInventoryId = new List<long>();
                    List<long> listRepresentativeId = new List<long>();
                    string adjustmentState;

                    foreach (DataRow row in rows)
                    {
                        adjustmentState = ConvertUtil.ToString(row["ADJUSTMENT_STATE"]);
                        //if (adjustmentState.Equals("1"))
                        {
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));

                            long representativeId = ConvertUtil.ToInt64(row["WAREHOUSING_ID"]);
                            if (!listRepresentativeId.Contains(representativeId))
                            {
                                listRepresentativeId.Add(representativeId);
                            }
                        }
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));
                    jobj.Add("LIST_WAREHOUSING_ID", string.Join(",", listRepresentativeId));
                    jobj.Add("ADJUSTMENT_STATE", 1);
                    jobj.Add("COMPANY_ID", 1);
                    jobj.Add("MAJOR_COL", "LT_DEALER_PRICE_MAJOR");
                    jobj.Add("ETC_COL", "LT_DEALER_PRICE_ETC");
                    jobj.Add("MAJOR_ADJUST_COL", "LT_DEALER_PRICE");
                    jobj.Add("ETC_ADJUST_COL", "LT_DEALER_PRICE_ETC");


                    if (DBAdjustment.getAdjustNtbData(jobj, ref jResult))
                    {
                        using (usrAdjustment Adjustment = new usrAdjustment(jResult, listInventoryId))
                        {
                            Adjustment.geList();
                            Dangol.CloseSplash();
                            if (Adjustment.ShowDialog(this) == DialogResult.OK)
                            {

                            }
                        }

                        //Dangol.Message("저장되었습니다.");
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
        }

        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            leManufactureType.ItemIndex = 0;
            leNickName.ItemIndex = 0;

            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -1;
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
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (DlgImportBarcode dlgImportBarcode = new DlgImportBarcode())
                {
                    dlgImportBarcode.ShowDialog();

                    if (dlgImportBarcode._isSuccess)
                    {
                        _listBarcode = dlgImportBarcode._listBarcode;
                        getWarehousingList(_listBarcode);
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                /*DataRow[] rows = _dtBarcode.Select("CHECK = TRUE");*/

                if (Dangol.MessageYN("재고번호의 제품 정보를 확인하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                Dangol.ShowSplash();

                geList();

                Dangol.CloseSplash();
            }
        }

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "CHECK" )
            {
                leManufactureType.ItemIndex = 0;
                leNickName.ItemIndex = 0;
            }
            else
            {
                int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                if (state == 0)
                    _currentRow["STATE"] = 2;
            }
            
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "BARCODE")
            {
                string state = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]);

                if (state.Equals("2"))
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
        }

        private void leAdjustmentState_EditValueChanged(object sender, EventArgs e)
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

            if (Dangol.MessageYN("선택하신 제품의 정산상태를 수정하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();
                gvList.BeginDataUpdate();
                _dt.BeginInit();
                foreach (DataRow row in rows)
                {
                    row["ADJUSTMENT_STATE"] = ConvertUtil.ToInt32(leAdjustmentState.EditValue);
                    row["STATE"] = 2;
                }
                _dt.EndInit();
                gvList.EndDataUpdate();
                Dangol.CloseSplash();
            }
        }

        private void leManufactureType_EditValueChanged(object sender, EventArgs e)
        {
            int value = ConvertUtil.ToInt32(leManufactureType.EditValue);

            if (value == -1)
                return;

            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -1;
            gvList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 제품이 없습니다.");
                return;
            }

            if (Dangol.MessageYN("선택하신 제품의 제조사 구분을 변경하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();
                gvList.BeginDataUpdate();
                _dt.BeginInit();
                foreach (DataRow row in rows)
                {
                    row["MANUFACTURE_TYPE"] = ConvertUtil.ToInt32(leManufactureType.EditValue);
                    row["STATE"] = 2;
                }
                _dt.EndInit();
                gvList.EndDataUpdate();
                Dangol.CloseSplash();
            }
        }

        private void leNickName_EditValueChanged(object sender, EventArgs e)
        {
            long value = ConvertUtil.ToInt64(leNickName.EditValue);

            if (value == -1)
                return;

            int rowhandle = gvList.FocusedRowHandle;
            gvList.FocusedRowHandle = -1;
            gvList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 제품이 없습니다.");
                return;
            }

            if (Dangol.MessageYN("선택하신 제품의 상세스펙을 변경하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();
                gvList.BeginDataUpdate();
                _dt.BeginInit();
                foreach (DataRow row in rows)
                {
                    row["NTB_LIST_ID"] = ConvertUtil.ToInt64(leNickName.EditValue);
                    row["STATE"] = 2;
                }
                _dt.EndInit();
                gvList.EndDataUpdate();
                Dangol.CloseSplash();
            }
        }

        private void leProductType_EditValueChanged(object sender, EventArgs e)
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

            if (Dangol.MessageYN("선택하신 제품의 제품타입을 변경하시겠습니까?") == DialogResult.Yes)
            {
                Dangol.ShowSplash();
                gvList.BeginDataUpdate();
                _dt.BeginInit();
                foreach (DataRow row in rows)
                {
                    row["INVENTORY_PTYPE"] = ConvertUtil.ToInt32(leProductType.EditValue);
                    row["STATE"] = 2;
                }
                _dt.EndInit();
                gvList.EndDataUpdate();
                Dangol.CloseSplash();
            }
        }
    }
}