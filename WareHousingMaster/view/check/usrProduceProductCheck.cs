using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.inventory;

namespace WareHousingMaster.view.check
{
    public partial class usrProduceProductCheck : DevExpress.XtraEditors.XtraForm
    {

        string _representativeType = "O";
        string _representativeCol = "RELEASES";
        string _representativeIdCol = "RELEASE_ID";
        string _representativeNo = null;
        
        long _representativeId = -1;
        long _inventoryId = -1;
        object _companyId = -1;
        long _type = 1;
        string _componentCd = "ALL";

        List<long> _listRepresentativeId;

        DataRowView _currentWarehousing;
        DataRowView _currentProduct;

        DataTable _dtProduce;
        DataTable _dt;

        DataTable _dtPallet;

        BindingSource _bs;
        BindingSource _bsProduce;

        short _checkType = 1;
        short _checkTypeHistory = 1;
        int _latestCheckType;
        string _barcode = null;
        int _productType = 2;

        DataTable _dtPrintPort;
        DataTable _dtPGrade;

        Dictionary<short, short[]> _dicProductCheckType = null;

        Dictionary<int, string> _dicProductProduceState;

        DataTable _dtNTBAdjustmentPrice = null;
        DataTable _dtAllInOneAdjustmentPrice = null;

        Dictionary<string, short> _dicProductCheck = null;
        Dictionary<string, short> _dicProductCheckHistory = null;

        Dictionary<string, long> _dicAdjustmentPrice = null;
        Dictionary<string, long> _dicAdjustmentPriceHistory = null;

        string _etcDes = "";
        string _batteryRemain = "";
        string _productGrade = "";

        string _etcDesHistory = "";
        string _batteryRemainHistory = "";
        string _productGradeHistory = "";

        //bool _isCheckExist1 = false;
        //bool _isCheckExistHistory1 = false;

        bool _isCheckExist = false;
        bool _isAdjustmentExistHistory = false;


        public usrProduceProductCheck()
        {
            InitializeComponent();

            _dtProduce = new DataTable();
            _dtProduce.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtProduce.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dtProduce.Columns.Add(new DataColumn("RELEASES", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("RELEASE_STATE", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtProduce.Columns.Add(new DataColumn("PRODUCT_CNT", typeof(int)));
            _dtProduce.Columns.Add(new DataColumn("DES", typeof(string)));
            //_dtProduce.Columns.Add(new DataColumn("COMPLETE_CNT", typeof(int)));
            //_dtProduce.Columns.Add(new DataColumn("REMAIN_CNT", typeof(int)));
            //_dtProduce.Columns.Add(new DataColumn("REPAIR_CNT", typeof(int)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("RELEASES", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCE_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));

            _dt.Columns.Add(new DataColumn("CHECK_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("QC_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("QC_USER_ID", typeof(string)));

            _dt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(int)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_SN", typeof(string)));
            _dt.Columns.Add(new DataColumn("SYSTEM_SN", typeof(string)));
            _dt.Columns.Add(new DataColumn("CPU_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("CHECK_YN", typeof(string)));
            _dt.Columns.Add(new DataColumn("QC_CHECK_YN", typeof(string)));
            //_dt.Columns.Add(new DataColumn("CHECK_ADJUST_YN", typeof(string)));
            //_dt.Columns.Add(new DataColumn("QC_ADJUST_YN", typeof(string)));
            //_dt.Columns.Add(new DataColumn("CHECK_ALLINONE_CHECK_YN", typeof(string)));
            //_dt.Columns.Add(new DataColumn("QC_ALLINONE_CHECK_YN", typeof(string)));

            _dt.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            //_dt.Columns.Add(new DataColumn("PRODUCT_GRADE_REPAIR", typeof(string)));



            _bsProduce = new BindingSource();
            _bs = new BindingSource();

            _dicProductCheck = new Dictionary<string, short>();
            _dicProductCheckHistory = new Dictionary<string, short>();

            _dicAdjustmentPrice = new Dictionary<string, long>();
            _dicAdjustmentPriceHistory = new Dictionary<string, long>();

            _dtNTBAdjustmentPrice = new DataTable();
            _dtAllInOneAdjustmentPrice = new DataTable();

            _listRepresentativeId = new List<long>();

            _dicProductProduceState = new Dictionary<int, string>();

            _dicProductCheckType = new Dictionary<short, short[]>()
            {
                {2, new short[]{3, 1} },
                {3, new short[]{1} },
                {4, new short[]{2, 3, 1} }
            };
        }

        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            leProductType.DataBindings.Add(new Binding("EditValue", _bs, "PRODUCT_TYPE", false, DataSourceUpdateMode.Never));
            leProduceState.DataBindings.Add(new Binding("EditValue", _bs, "PRODUCE_STATE", false, DataSourceUpdateMode.Never));
            leManufactureType.DataBindings.Add(new Binding("EditValue", _bs, "MANUFACTURE_TYPE", false, DataSourceUpdateMode.Never));
            leNickName.DataBindings.Add(new Binding("EditValue", _bs, "NTB_LIST_ID", false, DataSourceUpdateMode.Never));

            getWarehousingList();
        }

        private void setInfoBox()
        {
            _dtPrintPort = new DataTable();

            _dtPrintPort.Columns.Add(new DataColumn("KEY", typeof(string)));
            _dtPrintPort.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = ProjectInfo._startPort; i < ProjectInfo._endPort; i++)
            {
                DataRow dr = _dtPrintPort.NewRow();

                dr["KEY"] = i;
                dr["VALUE"] = i;
                _dtPrintPort.Rows.Add(dr);
            }

            Util.LookupEditHelper(lePrintPort, _dtPrintPort, "KEY", "VALUE");

            DataTable dtCheckTypet = Util.getCodeList("CD1701", "KEY", "VALUE");
            Util.LookupEditHelper(leCheckType, dtCheckTypet, "KEY", "VALUE");
            Util.LookupEditHelper(rileCheckType, dtCheckTypet, "KEY", "VALUE");

            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");        

            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            Util.LookupEditHelper(leManufactureType, dtmanufactureType, "KEY", "VALUE");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(rileNickName, dtNickName, "KEY", "VALUE");
            Util.LookupEditHelper(leNickName, dtNickName, "KEY", "VALUE");

            DataTable dtProductProduceState = Util.getCodeList("CD0804", "KEY", "VALUE");
            Util.LookupEditHelper(rileProduceState, dtProductProduceState, "KEY", "VALUE");
            Util.LookupEditHelper(leProduceState, dtProductProduceState, "KEY", "VALUE");

            foreach (DataRow row in dtProductProduceState.Rows)
                _dicProductProduceState.Add(ConvertUtil.ToInt32(row["KEY"]), $"{row["VALUE"]}");

            DataTable dtProduceState = Util.getCodeList("CD0801", "KEY", "VALUE");
            Util.LookupEditHelper(rileReleaseState, dtProduceState, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");

            _dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");

            Util.LookupEditHelper(rileProductGrade, _dtPGrade, "KEY", "VALUE");

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

            Util.LookupEditHelper(leProductType, dtDeviceType, "KEY", "VALUE");

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

            _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn("EXIST", typeof(bool)));

            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                _dtAllInOneAdjustmentPrice.Columns.Add(new DataColumn(col, typeof(long)));

            for (int i = 1; i < 6; i++)
            {
                DataRow dr = _dtAllInOneAdjustmentPrice.NewRow();

                dr["TYPE"] = i;
                dr["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                    dr[col] = 0;

                _dtAllInOneAdjustmentPrice.Rows.Add(dr);
            }

            var today = DateTime.Today;
            var pastDate = today.AddDays(-364);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

            leCheckType.EditValue = 1;


        }

        private void setIInitData()
        {
            //// 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;

            _bsProduce.DataSource = _dtProduce;
            _bs.DataSource = _dt;
        }

       

        private void setGridControl()
        {
            gcProduceList.DataSource = null;
            gcProduceList.DataSource = _bsProduce;

            gcList.DataSource = null;
            gcList.DataSource = _bs;
        }

        private void gvProduceList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvProduceList.RowCount > 0);

            gvList.BeginDataUpdate();

            _listRepresentativeId.Clear();
            //_dt.Clear();

            if (isValidRow)
            {
                _currentWarehousing = e.Row as DataRowView;
                //_representativeNo = ConvertUtil.ToString(_currentWarehousing["RELEASES"]);


                //int[] selectedRowHandles = gvWarehousingList.GetSelectedRows();
                //ArrayList rows = new ArrayList();

                //for (int i = 0; i < selectedRowHandles.Length; i++)
                //{
                //    int selectedRowHandle = selectedRowHandles[i];
                //    if (selectedRowHandle >= 0)
                //        rows.Add(gvWarehousingList.GetDataRow(selectedRowHandle));
                //}
                //_representativeId = ConvertUtil.ToInt64(_currentWarehousing["WAREHOUSING_ID"]);
                //if (_representativeId > 0)
                //    getProduct(_representativeId);
            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _currentWarehousing = null;
                //_dt.Clear();
            }

            gvList.EndDataUpdate();
        }

        private void gvProduceList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentProduct = e.Row as DataRowView;
                _productType = ConvertUtil.ToInt32(_currentProduct["PRODUCT_TYPE"]);
                _barcode = ConvertUtil.ToString(_currentProduct["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentProduct["INVENTORY_ID"]);
                _representativeNo = ConvertUtil.ToString(_currentProduct["RELEASES"]);
                _representativeId = ConvertUtil.ToInt64(_currentProduct["RELEASE_ID"]);
                _latestCheckType = ConvertUtil.ToInt32(_currentProduct["CHECK_TYPE"]);
            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _latestCheckType = 0;
                _currentProduct = null;
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentProduct = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
                _productType = ConvertUtil.ToInt32(_currentProduct["PRODUCT_TYPE"]);
                _barcode = ConvertUtil.ToString(_currentProduct["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentProduct["INVENTORY_ID"]);
                _representativeNo = ConvertUtil.ToString(_currentProduct["RELEASES"]);
                _representativeId = ConvertUtil.ToInt64(_currentProduct["RELEASE_ID"]);
                _latestCheckType = ConvertUtil.ToInt32(_currentProduct["CHECK_TYPE"]);
            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _latestCheckType = 0;
                _currentProduct = null;
            }
        }


        private void sbSearch_Click(object sender, EventArgs e)
        {

            Dangol.ShowSplash();
            getWarehousingList();
            Dangol.CloseSplash();
        }

        private bool getWarehousingList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtProduce.Clear();

            if (DBProductProduce.getProduceList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtProduce.NewRow();

                        dr["NO"] = index++;
                        dr["CHECK"] = false;
                        dr["RELEASE_ID"] = obj["RELEASE_ID"];
                        dr["RELEASES"] = obj["RELEASES"];
                        dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                        dr["RELEASE_STATE"] = obj["RELEASE_STATE"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["PRODUCT_CNT"] = obj["PRODUCT_CNT"];
                        dr["DES"] = obj["DES"];
                        
                        _dtProduce.Rows.Add(dr);
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

            if (diffDay > 365)
            {
                jData.Add("MSG", "검색 기간은 365일을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("CREATE_DT_S", dtFrom);
            jData.Add("CREATE_DT_E", dtTo);

            //if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
            //    jData.Add("PRODUCT_GRADE", ConvertUtil.ToString(leReceiptState.EditValue));

            return true;
        }

        private void getProduct()
        {
            _dt.Clear();

            if (_listRepresentativeId.Count < 0)
            {
                Dangol.Message("선택하신 입고번호가 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("TN_TABLE_PART", "TN_RELEASE_PART");
            jData.Add("TN_TABLE", "TN_RELEASE");
            jData.Add("REPRESENTATIVE_ID_COL", "RELEASE_ID");
            jData.Add("REPRESENTATIVE_COL", "RELEASES");
            jData.Add("TN_BOM", "TN_RELEASE_BOM_TREE");
            jData.Add("LIST_REPRESENTATIVE_ID", string.Join(",", _listRepresentativeId));

            if (DBProductProduce.getProductListByProduceId(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    setProductCheckData(jArray);
                }
            }
        }
        private void setProductCheckData(JArray jArray)
        {
            foreach (JObject obj in jArray.Children<JObject>())
            {
                DataRow dr = _dt.NewRow();

                dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                dr["RELEASE_ID"] = obj["RELEASE_ID"];
                dr["RELEASES"] = obj["RELEASES"];
                dr["PRODUCE_STATE"] = obj["PRODUCE_STATE"];
                dr["BARCODE"] = obj["BARCODE"];
                dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];

                dr["PRODUCT_TYPE"] = ConvertUtil.ToInt32(obj["PRODUCT_TYPE"]); 
                dr["CHECK_TYPE"] = obj["CHECK_TYPE"];
                dr["CHECK_DT"] = ConvertUtil.ToDateTime(obj["CHECK_DT"], "yyyy-MM-dd");
                dr["CHECK_USER_ID"] = obj["CHECK_USER_ID"];
                dr["QC_DT"] = ConvertUtil.ToDateTime(obj["QC_DT"], "yyyy-MM-dd");
                dr["QC_USER_ID"] = obj["QC_USER_ID"];

                dr["MANUFACTURE_TYPE"] = obj["MANUFACTURE_TYPE"];
                dr["MANUFACTURE_NM"] = obj["MANUFACTURE_NM"];
                dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NM"];
                dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                dr["MBD_SN"] = obj["MBD_SN"];
                dr["SYSTEM_SN"] = obj["SYSTEM_SN"];
                dr["CPU_MODEL_NM"] = obj["CPU_MODEL_NM"];

                dr["NTB_LIST_ID"] = obj["NTB_LIST_ID"];

                dr["CHECK_YN"] = string.IsNullOrWhiteSpace(ConvertUtil.ToString(dr["CHECK_USER_ID"])) ? "N" : "Y";
                dr["QC_CHECK_YN"] = string.IsNullOrWhiteSpace(ConvertUtil.ToString(dr["QC_USER_ID"])) ? "N" : "Y";


                _dt.Rows.Add(dr);
            }
        }


        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("제품 정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                var jArray = new JArray();
                JObject jobj = new JObject();
                JObject jResult = new JObject();

                jobj.Add("BULK_YN", 0);

                jobj.Add("INVENTORY_PTYPE", ConvertUtil.ToInt32(leProductType.EditValue));
                jobj.Add("MANUFACTURE_TYPE", ConvertUtil.ToInt32(leManufactureType.EditValue));
                jobj.Add("NTB_LIST_ID", ConvertUtil.ToInt64(leNickName.EditValue));
                jobj.Add("ADJUSTMENT_STATE", ConvertUtil.ToString(leProduceState.EditValue));
                jobj.Add("INVENTORY_ID", _inventoryId);
                jobj.Add("UPDATE_ONE", 1);
                jobj.Add("DATA", jArray);

                if (DBAdjustment.updateProductInfo(jobj, ref jResult))
                {
                    int rowhandle = gvList.FocusedRowHandle;
                    int topRowIndex = gvList.TopRowIndex;
  
                    gvList.BeginDataUpdate();
                    _currentProduct.BeginEdit();
                    _currentProduct["PRODUCT_TYPE"] = ConvertUtil.ToInt32(leProductType.EditValue);
                    _currentProduct["MANUFACTURE_TYPE"] = ConvertUtil.ToString(leManufactureType.EditValue);
                    _currentProduct["NTB_LIST_ID"] = ConvertUtil.ToInt64(leNickName.EditValue);
                    _currentProduct["ADJUSTMENT_STATE"] = ConvertUtil.ToString(leProduceState.EditValue);
                    _currentProduct.EndEdit();
                    gvList.EndDataUpdate();

                    gvList.FocusedRowHandle = rowhandle;
                    gvList.TopRowIndex = topRowIndex;

                    Dangol.Message("저장되었습니다.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private void getCheckInfo(long inventoryId)
        {
            JObject jResult = new JObject();
            string typeNm = "NTB";
            _dicProductCheck.Clear();
            _etcDes = "";
            _batteryRemain = "";
            _productGrade = "";
            _isCheckExist = false;

            if (_productType == 2)
            {
                typeNm = "NTB";
                _dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            }
            else if (_productType == 3)
            {
                typeNm = "ALLINONE";
                _dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            }

            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                _dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;

            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = 0;


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

                if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                {
                    JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];


                    if (_productType == 2)
                    {
                        _dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                            _dtNTBAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }
                    else if (_productType == 3)
                    {
                        _dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                            _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }
                }

                _isCheckExist = true;
            }


            if (_checkType > 1)
            {
                _dicProductCheckHistory.Clear();
                _etcDesHistory = "";
                _batteryRemainHistory = "";
                _productGradeHistory = "";
                _isAdjustmentExistHistory = false;

                _checkTypeHistory = -1;

                short[] arrType = _dicProductCheckType[_checkType];

                for(int i = 0; i < arrType.Length; i++)
                {
                    if(DBConnect.getCheckInfo(inventoryId, typeNm, arrType[i], ref jResult))
                    {
                        _checkTypeHistory = arrType[i];
                        break;
                    }
                }

                if(_checkTypeHistory > 0)
                {

                    if (_productType == 2)
                        _dtNTBAdjustmentPrice.Rows[_checkTypeHistory]["EXIST"] = false;
                    else if (_productType == 3)
                        _dtAllInOneAdjustmentPrice.Rows[_checkTypeHistory]["EXIST"] = false;

                    foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                        _dtAllInOneAdjustmentPrice.Rows[_checkTypeHistory][col] = 0;

                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                _etcDesHistory = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                _batteryRemainHistory = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                _productGradeHistory = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!_dicProductCheckHistory.ContainsKey(name))
                                    _dicProductCheckHistory.Add(name, value);
                            }

                            if (!_isCheckExist)
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
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        if (_productType == 2)
                        {
                            _dtNTBAdjustmentPrice.Rows[_checkTypeHistory]["EXIST"] = true;
                            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                                _dtNTBAdjustmentPrice.Rows[_checkTypeHistory][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                            if (!_isCheckExist)
                            {
                                _dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                                    _dtNTBAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                            }
                        }
                        else if (_productType == 3)
                        {

                            _dtAllInOneAdjustmentPrice.Rows[_checkTypeHistory]["EXIST"] = true;
                            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                                _dtAllInOneAdjustmentPrice.Rows[_checkTypeHistory][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                            if (!_isCheckExist)
                            {
                                _dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                                    _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                            }
                        }
                    }

                    _isAdjustmentExistHistory = true;
                }
                else
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (i != (int)_checkType)
                        {
                            if (_productType == 2)
                                _dtNTBAdjustmentPrice.Rows[i]["EXIST"] = false;
                            else if (_productType == 3)
                                _dtAllInOneAdjustmentPrice.Rows[i]["EXIST"] = false;

                            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                                _dtAllInOneAdjustmentPrice.Rows[i][col] = 0;
                        }
                    }
                } 
            }
        }

        private void sbPrintPart_Click(object sender, EventArgs e)
        {
            if (_currentWarehousing == null)
            {
                Dangol.Message("입고번호가 없습니다.");
                return;
            }

            if (string.IsNullOrEmpty(_barcode) || _inventoryId < 1)
            {
                Dangol.Message("재고로 등록되지 않은 부품입니다.");
                return;
            }

            JObject jResult = new JObject();

            //if (DBConnect.printInventoryInfo(_representativeType, _representativeNo, _representativeCol, _barcode, lePrintPort.EditValue.ToString(), ref jResult))
            //{
            //    Dangol.Message("부품정보가 출력되었습니다.");
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}
        }

        private void sbPartCheck_Click(object sender, EventArgs e)
        {
            _checkType = ConvertUtil.ToInt16(leCheckType.EditValue);

            if(_checkType < 1)
            {
                Dangol.Message("검수타입을 선택하세요.");
                return;
            }

            getCheckInfo(_inventoryId);

            if(_checkType > 1 && _checkTypeHistory == -1)
            {
                Dangol.Message("검수결과가 없습니다. 입고검수 후 진행해 주세요.");
                return;
            }

            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

            
            JObject jResult = new JObject();

            if (_productType == 2)
            {
                long ntbListId = ConvertUtil.ToInt64(leNickName.EditValue);
                int manufactureType = ConvertUtil.ToInt32(leManufactureType.EditValue);

                if (ntbListId < 1)
                {
                    Dangol.Message("제품 사양을 선택해주세요");
                    return;
                }

                if (manufactureType < 1)
                {
                    Dangol.Message("제조사를 선택해주세요");
                    return;
                }

                _dicAdjustmentPrice.Clear();
                if (DBConnect.getNTBAdjustmentPrice(_representativeNo, _representativeType, ntbListId, ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string col = "";
                    if (manufactureType == 1)
                        col = "MAJOR_PRICE";
                    else
                        col = "ETC_PRICE";
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string key = ConvertUtil.ToString(obj["COL_NM"]);

                        if (!_dicAdjustmentPrice.ContainsKey(key))
                            _dicAdjustmentPrice.Add(key, ConvertUtil.ToInt64(obj[col]));
                    }
                }
            }
            else if(_productType == 3)
            {
                _dicAdjustmentPrice.Clear();
            }
            else
            {
                Dangol.Message("제품 검수는 노트북, 올인원PC만 가능합니다.");
                return;
            }

            if (DBConnect.getProductInfo(_inventoryId, ref jResult))
            {
                if (_checkType == 1 || _checkType == 3)
                {
                    string approveType = ConvertUtil.ToString(jResult["APPROVE_TYPE"]);
                    int adjustmentState = ConvertUtil.ToInt32(jResult["ADJUSTMENT_STATE"]);

                    if (!string.IsNullOrWhiteSpace(approveType) && adjustmentState == 1)
                    {
                        if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                        {
                            if (Dangol.MessageYN("승인완료&정산대기인 제품입니다. 계속 진행하시겠습니까?") == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else
                        {
                            Dangol.Message("승인완료 & 정산대기인 제품은 검수를 변경할 수 없습니다.");
                            return;
                        }
                    }
                }
                else
                {
                    string approveType = ConvertUtil.ToString(jResult["APPROVE_PRODUCE_TYPE"]);
                    int produceState = ConvertUtil.ToInt32(jResult["PRODUCE_STATE"]);

                    if (produceState > 2)
                    {
                        if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
                        {
                            if (_dicProductProduceState.ContainsKey(produceState))
                            {
                                if (Dangol.MessageYN($"제품상태가 '{_dicProductProduceState[produceState]}' 입니다. 계속 진행하시겠습니까?") == DialogResult.No)
                                    return;
                            }
                            else
                            {
                                if (Dangol.MessageYN("생산대기중인 제품이 아닙니다. 계속 진행하시겠습니까?") == DialogResult.No)
                                    return;
                            }
                        }
                        else
                        {
                            if (_dicProductProduceState.ContainsKey(produceState))
                                Dangol.Message($"'{_dicProductProduceState[produceState]}' 상태의 제품은 QC 검수를 변경할 수 없습니다. ");
                            else
                                Dangol.Message("생산대기 or 리페어 상태의 제품만 QC검수가 가능합니다.");
 
                            return;
                        }
                    }
                }
            }
            

            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

            if (_productType == 2)
            {
                string dtStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                using (DlgNtb2ndEditionCheck2 ntbCheck = new DlgNtb2ndEditionCheck2(_dicProductCheck, _dicProductCheckHistory, ref _dtNTBAdjustmentPrice, _dicAdjustmentPrice,_etcDes,
                    _batteryRemain, _productGrade, _dtPrintPort, _dtPGrade, null, -1, _checkType))
                {


                    if (_checkType == 2)
                    {
                        
                    }
                    else if (_checkType == 4)
                    {
                        ntbCheck._isExistReleaseCheck = _checkTypeHistory == (int)2;
                        ntbCheck._directoryNm = _representativeNo;
                        ntbCheck._fileNm = $"{_inventoryId}";
                    }
                        
                    if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        _etcDes = ntbCheck._caseDestroyDescription;
                        _batteryRemain = ntbCheck._batteryRemain;
                        _productGrade = ntbCheck._pGrade;

                        string dtEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, dtStart, dtEnd, _barcode, _inventoryId, _checkType, _dicProductCheck, new List<long>(new[] {_inventoryId }), _etcDes, _batteryRemain, _productGrade, 1);

                        if (!DBAdjustment.insertAdjustmentPrice(_inventoryId, _checkType, "insertAdjustmentPrice", ExamineInfo._listAdjustmentPriceColShort, _dtNTBAdjustmentPrice, ref jResult))
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }

                        _currentProduct.BeginEdit();

                        if (_checkType == 4)
                        {
                            _currentProduct["QC_CHECK_YN"] = "Y";
                            _currentProduct["QC_DT"] = DateTime.Today.ToString("yyyy-MM-dd");
                            _currentProduct["QC_USER_ID"] = ProjectInfo._userId;
                            _currentProduct["PRODUCT_GRADE"] = _productGrade;

                            if (DBConnect.getProductInfo(_inventoryId, ref jResult))
                                _currentProduct["PRODUCE_STATE"] = ConvertUtil.ToString(jResult["PRODUCE_STATE"]);


                            JObject jobj = new JObject();

                            jobj.Add("BULK_YN", 0);
                            jobj.Add("UPDATE_ONE", 1);
                            jobj.Add("INVENTORY_ID", _inventoryId);
                            jobj.Add("CAPTURE_YN", ntbCheck._isCapture ? 1 : 0);
                            jobj.Add("QC_STATE", ntbCheck._QCState);

                            DBConnect.updateProductInfo(jobj, ref jResult);
                        }
                        else
                        {
                            bool isCheckNonExist = _latestCheckType == 0;// X -- > ?
                            bool isWarehousingCheck = _latestCheckType == 1; // 입고 --> ?
                            bool isHighGrade = _latestCheckType == 3 && (_checkType == 2 || _checkType == 3); // 리페어 --> 리페어/출고
                            bool isHighGrade2 = _latestCheckType == 2 && (_checkType == 2); // 출고 --> 출고

                            if (isCheckNonExist || isWarehousingCheck || isHighGrade || isHighGrade2)
                            {
                                _latestCheckType = _checkType;
                                _currentProduct["CHECK_TYPE"] = _checkType;
                                _currentProduct["CHECK_YN"] = "Y";
                                _currentProduct["CHECK_DT"] = DateTime.Today.ToString("yyyy-MM-dd");
                                _currentProduct["CHECK_USER_ID"] = ProjectInfo._userId;
                                if (string.IsNullOrWhiteSpace(ConvertUtil.ToString(_currentProduct["QC_USER_ID"])))
                                    _currentProduct["PRODUCT_GRADE"] = _productGrade;
                            }
                        }

                        _currentProduct.EndEdit();

                        if (ntbCheck._isPrint)
                        {
                            if (DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, _inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                            {
                                Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                            }
                            else
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }
                        }

                        
                    }
                }
            }
            else if (_productType == 3)
            {
                Dangol.Message("개발중입니다. 관리자에게 문의하세요");
                return;

                using (DlgAllInOneCheck AllInOneCheck = new DlgAllInOneCheck(_dicProductCheck, _dicProductCheckHistory, ref _dtAllInOneAdjustmentPrice, _dicAdjustmentPrice, _etcDes,
                    _productGrade, _dtPrintPort, _dtPGrade, null, -1, _checkType))
                {
                    if (AllInOneCheck.ShowDialog(this) == DialogResult.OK)
                    {
                       _etcDes = AllInOneCheck._etcDes;
                        _productGrade = AllInOneCheck._pGrade;

                        if (DBConnect.insertAllInOneCheck(_representativeType, _representativeNo, _representativeCol, _barcode, _inventoryId, _checkType, _dicProductCheck, new List<long>(new[] { _inventoryId }), _etcDes, _productGrade, 1))
                        {
                            if (!DBAdjustment.insertAdjustmentPrice(_inventoryId, _checkType, "insertAdjustmentAllInOnePrice", ExamineInfo._listAdjustmentAllInOnePriceColShort, _dtAllInOneAdjustmentPrice, ref jResult))
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }

                            if (AllInOneCheck._isPrint)
                            {
                                if (DBConnect.printAllInOneProduct(_representativeType, _representativeNo, _representativeCol, _inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                                {
                                    Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                                }
                                else
                                {
                                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                }
                            }
                        }
                    }
                }
            }

        }

        private void leProductType_EditValueChanged(object sender, EventArgs e)
        {
            _productType = ConvertUtil.ToInt32(leProductType.EditValue);
        }

        private void lcgWarehousing_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduceList.FocusedRowHandle;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtProduce.Select("CHECK = TRUE");


            if (rows.Length < 1)
            {
                Dangol.Message("생산번호를 선택하세요");
                return;
            }

            if (Dangol.MessageYN("선택하신 생산번호의 제품 정보를 확인하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            _listRepresentativeId.Clear();

            foreach (DataRow row in rows)
                _listRepresentativeId.Add(ConvertUtil.ToInt64(row["RELEASE_ID"]));

            Dangol.ShowSplash();

            getProduct();

            Dangol.CloseSplash();
        }

        private void lcgWarehousing_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduceList.FocusedRowHandle;
            int topRowIndex = gvProduceList.TopRowIndex;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            try
            {
                gvProduceList.BeginUpdate();
                foreach (DataRow row in _dtProduce.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvProduceList.DataRowCount; i++)
                {
                    int rowHandle = gvProduceList.GetVisibleRowHandle(i);
                    rows.Add(gvProduceList.GetDataRow(rowHandle));
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
                gvProduceList.EndUpdate();
            }
        }

        private void lcgWarehousing_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduceList.FocusedRowHandle;
            int topRowIndex = gvProduceList.TopRowIndex;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            gvProduceList.BeginDataUpdate();

            foreach (DataRow row in _dtProduce.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvProduceList.EndDataUpdate();
        }

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
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
            else if (e.Button.Properties.Tag.Equals(2))
            {
                refresh();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                ImageInfo.GetImage(1, true, _barcode);
            }
        }

        private void refresh()
        {
            Dangol.ShowSplash();

            int topRowIndex = gvList.TopRowIndex;
            string barcode = _barcode;
            gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged -= gvList_FocusedRowChanged;
            getProduct();
            gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged += gvList_FocusedRowChanged;

            int rowHandle = gvList.LocateByValue("BARCODE", barcode);

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

            Dangol.CloseSplash();
        }

        private void gcList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                refresh();
            }
        }
    }
}