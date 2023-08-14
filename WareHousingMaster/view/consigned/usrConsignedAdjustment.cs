using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedAdjustment : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtConsignedAdjustMentSumary;

        DataTable _dtConsignedProductDetail;
        DataTable _dtConsignedPartDetail;
        DataTable _dtConsignedProduceDetail;
        DataTable _dtConsignedDeliveryDetail;
        DataTable _dtConsignedDetail;

        DataTable _dtConsignedProductReturnDetail;
        DataTable _dtConsignedPartReturnDetail;
        DataTable _dtConsignedProduceReturnDetail;
        DataTable _dtConsignedDeliveryReturnDetail;
        DataTable _dtConsignedReturnDetail;

        BindingSource bsConsignedAdjustMentSumary;
        BindingSource _bsConsignedtDetail;
        BindingSource _bsConsignedtReturnDetail;
        BindingSource _bsPallet;

        DataTable _dtLocation;
        DataTable _dtPallet;
        DataTable _dtPalletDetail;
        DataTable _dtPalletRelease;
        DataTable _dtPalletWarehousing;
        DataTable _dtBasket;
        DataRowView _currentRow;
        DataRowView _currentRowDetail;
        List<string> _listBarcode;
        List<long> _listInventoryId;
        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        long _warehouseMovementId;
        long _inventoryId;


        public usrConsignedAdjustment()
        {
            InitializeComponent();

            long _warehouseMovementId = -1;


            _dtConsignedAdjustMentSumary = new DataTable();
            _dtConsignedAdjustMentSumary.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedAdjustMentSumary.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedAdjustMentSumary.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedAdjustMentSumary.Columns.Add(new DataColumn("RETURN_CNT", typeof(int)));
            _dtConsignedAdjustMentSumary.Columns.Add(new DataColumn("RETURN_PRICE", typeof(long)));
            _dtConsignedAdjustMentSumary.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));


            _dtConsignedProductDetail = new DataTable();

            _dtConsignedProductDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedProductDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedProductDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedProductDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedProductDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedProductDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedPartDetail = new DataTable();

            _dtConsignedPartDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedPartDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedPartDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedPartDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedPartDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedPartDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedProduceDetail = new DataTable();

            _dtConsignedProduceDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedProduceDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedProduceDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedProduceDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedProduceDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedProduceDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedDeliveryDetail = new DataTable();

            _dtConsignedDeliveryDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedDeliveryDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedDeliveryDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedDeliveryDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedDeliveryDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedDeliveryDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedDetail = new DataTable();

            _dtConsignedDetail.Columns.Add(new DataColumn("CATEGORY", typeof(Int32)));
            _dtConsignedDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedProductReturnDetail = new DataTable();

            _dtConsignedProductReturnDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedProductReturnDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedProductReturnDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedProductReturnDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedProductReturnDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedProductReturnDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedPartReturnDetail = new DataTable();

            _dtConsignedPartReturnDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedPartReturnDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedPartReturnDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedPartReturnDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedPartReturnDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedPartReturnDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedProduceReturnDetail = new DataTable();

            _dtConsignedProduceReturnDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedProduceReturnDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedProduceReturnDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedProduceReturnDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedProduceReturnDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedProduceReturnDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedDeliveryReturnDetail = new DataTable();

            _dtConsignedDeliveryReturnDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedDeliveryReturnDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedDeliveryReturnDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedDeliveryReturnDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedDeliveryReturnDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedDeliveryReturnDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtConsignedReturnDetail = new DataTable();

            _dtConsignedReturnDetail.Columns.Add(new DataColumn("CATEGORY", typeof(Int32)));
            _dtConsignedReturnDetail.Columns.Add(new DataColumn("TYPE", typeof(string)));
            _dtConsignedReturnDetail.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtConsignedReturnDetail.Columns.Add(new DataColumn("MODEL", typeof(string)));
            _dtConsignedReturnDetail.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dtConsignedReturnDetail.Columns.Add(new DataColumn("CNT", typeof(int)));
            _dtConsignedReturnDetail.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));



            bsConsignedAdjustMentSumary = new BindingSource();
            _bsConsignedtDetail = new BindingSource();
            _bsConsignedtReturnDetail = new BindingSource();
            _bsPallet = new BindingSource();

            _listBarcode = new List<string>();
            _listInventoryId = new List<long>();
            //_dicWarehouMovementList = new Dictionary<long, Dictionary<long, string>>();

        }

        private void usrConsignedAdjustment_Load(object sender, EventArgs e)
        {
            setInfoBox();
            JObject jResult = new JObject();
            //getWarehouseMovementList("", "", "E,R,W", "", "", "", "", "", "", "", ref jResult);
            setIInitData();

            //teInputBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            //teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
        }


        private void setInfoBox()
        {

            _dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "WAREHOUSE_NM", "USE_YN != 'N'", "WAREHOUSE_NM ASC");
            Util.insertRowonTop(_dtLocation, "", "선택안합");

            DataTable _dtCompany = Util.getTable("TN_COMPANY_MST", "COMPANY_ID, COMPANY_NM", "DEL_YN != 'Y' AND COMPANY_TYPE = 'C'","COMPANY_ID");
            Dictionary<string, object> dicCompanyDefault = new Dictionary<string, object>();
            dicCompanyDefault.Add("COMPANY_ID", "-1");
            dicCompanyDefault.Add("COMPANY_NM", "선택안합");
            Util.insertRowonTop(_dtCompany, dicCompanyDefault);
            Util.LookupEditHelper(leCompany, _dtCompany, "COMPANY_ID", "COMPANY_NM");

            //Util.LookupEditHelper(leReleaseWarehouseNo, _dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(leWarehousingWarehouseNo, _dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(leReleaseWarehouseNoSelect, _dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(leWarehousingWarehouseNoSelect, _dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(rileReleaseWarehouse, _dtLocation, "KEY", "VALUE");
            //Util.LookupEditHelper(rileWarehousingWarehouse, _dtLocation, "KEY", "VALUE");


            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");

            Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
            dicPalletDefault.Add("WAREHOUSE_ID", "-1");
            dicPalletDefault.Add("PALLET_ID", "");
            dicPalletDefault.Add("PALLET_NM", "선택안합");

            Util.insertRowonTop(_dtPallet, dicPalletDefault);

            _dtPalletRelease = _dtPallet.Copy();
            _dtPalletWarehousing = _dtPallet.Copy();
            _dtPalletDetail = _dtPallet.Copy();
            

            //Util.LookupEditHelper(leReleasePalletNoSelect, _dtPalletRelease, "PALLET_ID", "PALLET_NM");
            //Util.LookupEditHelper(leWarehousingPalletNoSelect, _dtPalletWarehousing, "PALLET_ID", "PALLET_NM");

            _bsPallet.DataSource = _dtPalletDetail;
            //Util.LookupEditHelper(rileBasket, _bsPallet, "PALLET_ID", "PALLET_NM");


            DataTable dtWarehouseMovementState = Util.getCodeList("CD1201", "KEY", "VALUE");
            Util.insertRowonTop(dtWarehouseMovementState, "", "선택안합");
            //Util.LookupEditHelper(leCompany, dtWarehouseMovementState, "KEY", "VALUE");
            //Util.LookupEditHelper(rileWarehouseMovementState, dtWarehouseMovementState, "KEY", "VALUE");
            

            DataTable dtWarehouseInventoryState = Util.getCodeList("CD1202", "KEY", "VALUE");
            //Util.LookupEditHelper(rileInventoryState, dtWarehouseInventoryState, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            gcConsignedAdjustmentSummary.DataSource = null;
            bsConsignedAdjustMentSumary.DataSource = _dtConsignedAdjustMentSumary;
            gcConsignedAdjustmentSummary.DataSource = bsConsignedAdjustMentSumary;

            gcConsignedReturnDetail.DataSource = null;
            _bsConsignedtReturnDetail.DataSource = _dtConsignedReturnDetail;
            gcConsignedReturnDetail.DataSource = _bsConsignedtReturnDetail;

            gcConsignedDetail.DataSource = null;
            _bsConsignedtDetail.DataSource = _dtConsignedDetail;
            gcConsignedDetail.DataSource = _bsConsignedtDetail;

            rgTypeReturn.EditValue = 0;

            var today = DateTime.Today;
            var pastDate = today.AddDays(-30);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
        }

        private void rgTypeReturn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gcConsignedDetail.DataSource = null;

            //if(ConvertUtil.ToInt32(rgType.EditValue) == 0)
            //    _bsConsignedtDetail.DataSource = _dtConsignedDetail;
            //else if (ConvertUtil.ToInt32(rgType.EditValue) == 1)
            //    _bsConsignedtDetail.DataSource = _dtConsignedProductDetail;
            //else if (ConvertUtil.ToInt32(rgType.EditValue) == 2)
            //    _bsConsignedtDetail.DataSource = _dtConsignedPartDetail;
            //else if (ConvertUtil.ToInt32(rgType.EditValue) == 3)
            //    _bsConsignedtDetail.DataSource = _dtConsignedProduceDetail;
            //else if (ConvertUtil.ToInt32(rgType.EditValue) == 4)
            //    _bsConsignedtDetail.DataSource = _dtConsignedDeliveryDetail;
            //else if (ConvertUtil.ToInt32(rgType.EditValue) == 5)
            //    _bsConsignedtDetail.DataSource = _dtConsignedRefundDetail;

            //gcConsignedDetail.DataSource = _bsConsignedtDetail;

            if (ConvertUtil.ToInt32(rgTypeReturn.EditValue) == 0)
                _bsConsignedtReturnDetail.Filter = $"CATEGORY > 0";
            else
                _bsConsignedtReturnDetail.Filter = $"CATEGORY = {rgTypeReturn.EditValue}";

        }

        private void rgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ToInt32(rgType.EditValue) == 0)
                _bsConsignedtDetail.Filter = $"CATEGORY > 0";
            else
                _bsConsignedtDetail.Filter = $"CATEGORY = {rgType.EditValue}";
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            getSummaryList();
            Dangol.CloseSplash();
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

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(leCompany.EditValue)))
            {
                jData.Add("COMPANY_ID", ConvertUtil.ToString(leCompany.EditValue));
                date++;
            }

            return true;
        }

        private bool getSummaryList()
        {

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            if (!checkSearch(ref jobj))
            {
                Dangol.Message(jobj["MSG"]);
                return false;
            }

            _dtConsignedAdjustMentSumary.Clear();

            if (DBConnect.getConsignedAdjustmentSummary(jobj, ref jResult))
            {
                
                JObject jData = (JObject)jResult["PART"];
                JObject jReturnData = (JObject)jResult["REFUND_PART"];

                DataRow dr1 = _dtConsignedAdjustMentSumary.NewRow();
                dr1["TYPE"] = "제품";
                dr1["CNT"] = jData["PRODUCT_CNT"];
                dr1["PRICE"] = jData["PRODUCT_PRICE"];
                dr1["RETURN_CNT"] = jReturnData["PRODUCT_CNT"];
                dr1["RETURN_PRICE"] = jReturnData["PRODUCT_PRICE"];
                dr1["TOTAL_PRICE"] = ConvertUtil.ToInt64(jData["PRODUCT_PRICE"]) - ConvertUtil.ToInt64(jReturnData["PRODUCT_PRICE"]);
                _dtConsignedAdjustMentSumary.Rows.Add(dr1);

                DataRow dr2 = _dtConsignedAdjustMentSumary.NewRow();
                dr2["TYPE"] = "부품/소모품";
                dr2["CNT"] = jData["PART_CNT"];
                dr2["PRICE"] = jData["PART_PRICE"];
                dr2["RETURN_CNT"] = jReturnData["PART_CNT"];
                dr2["RETURN_PRICE"] = jReturnData["PART_PRICE"];
                dr2["TOTAL_PRICE"] = ConvertUtil.ToInt64(jData["PART_PRICE"]) - ConvertUtil.ToInt64(jReturnData["PART_PRICE"]);
                _dtConsignedAdjustMentSumary.Rows.Add(dr2);

                JObject jDataP = (JObject)jResult["PRODUCE"];
                JObject jReturnDataP = (JObject)jResult["REFUND_PRODUCE"];

                DataRow dr3 = _dtConsignedAdjustMentSumary.NewRow();
                dr3["TYPE"] = "생산";
                dr3["CNT"] = jDataP["PRODUCE_CNT"];
                dr3["PRICE"] = jDataP["PRODUCE_PRICE"];
                dr3["RETURN_CNT"] = jReturnDataP["PRODUCE_CNT"];
                dr3["RETURN_PRICE"] = jReturnDataP["PRODUCE_PRICE"];
                dr3["TOTAL_PRICE"] = ConvertUtil.ToInt64(jDataP["PRODUCE_PRICE"]) - ConvertUtil.ToInt64(jReturnDataP["PRODUCE_PRICE"]);
                _dtConsignedAdjustMentSumary.Rows.Add(dr3);

                DataRow dr4 = _dtConsignedAdjustMentSumary.NewRow();
                dr4["TYPE"] = "물류";
                dr4["CNT"] = jDataP["DEILIVERY_CNT"];
                dr4["PRICE"] = jDataP["DEILIVERY_PRICE"];
                dr4["RETURN_CNT"] = jReturnDataP["DEILIVERY_CNT"];
                dr4["RETURN_PRICE"] = jReturnDataP["DEILIVERY_PRICE"];
                dr4["TOTAL_PRICE"] = ConvertUtil.ToInt64(jDataP["DEILIVERY_PRICE"]) - ConvertUtil.ToInt64(jReturnDataP["DEILIVERY_PRICE"]);
                _dtConsignedAdjustMentSumary.Rows.Add(dr4);

                if(setConsignedDetail(jobj))
                {

                }
                else
                {

                }

                return true;

            }
            else
            {
                return false;
            }
        }

        private bool setConsignedDetail(JObject jobj)
        {
            JObject jResult = new JObject();

            _dtConsignedDetail.Clear();
            _dtConsignedProductDetail.Clear();
            _dtConsignedPartDetail.Clear();
            _dtConsignedProduceDetail.Clear();
            _dtConsignedDeliveryDetail.Clear();

            _dtConsignedReturnDetail.Clear();
            _dtConsignedProductReturnDetail.Clear();
            _dtConsignedPartReturnDetail.Clear();
            _dtConsignedProduceReturnDetail.Clear();
            _dtConsignedDeliveryReturnDetail.Clear();


            if (DBConnect.getConsignedAdjustmentDetail(jobj, ref jResult))
            {
                JArray jArrayProduct = JArray.Parse(jResult["PRODUCT"].ToString());

                foreach (JObject obj in jArrayProduct.Children<JObject>())
                {
                    DataRow dr = _dtConsignedProductDetail.NewRow();
                    dr["TYPE"] = "제품";
                    dr["NAME"] = obj["NAME"];
                    dr["MODEL"] = obj["MODEL"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedProductDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedDetail.NewRow(); 
                    dr1["CATEGORY"] = 1;
                    dr1["TYPE"] = "제품";
                    dr1["NAME"] = obj["NAME"];
                    dr1["MODEL"] = obj["MODEL"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedDetail.Rows.Add(dr1);
                }

                JArray jArrayPart = JArray.Parse(jResult["PART"].ToString());

                foreach (JObject obj in jArrayPart.Children<JObject>())
                {
                    DataRow dr = _dtConsignedPartDetail.NewRow();
                    dr["TYPE"] = "부품";
                    dr["NAME"] = obj["COMPONENT_CD"];
                    dr["MODEL"] = obj["MODEL_NM"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedPartDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedDetail.NewRow();
                    dr1["CATEGORY"] = 2;
                    dr1["TYPE"] = "부품";
                    dr1["NAME"] = obj["COMPONENT_CD"];
                    dr1["MODEL"] = obj["MODEL_NM"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedDetail.Rows.Add(dr1);
                }

                JArray jArrayProduce = JArray.Parse(jResult["PRODUCE"].ToString());

                foreach (JObject obj in jArrayProduce.Children<JObject>())
                {
                    DataRow dr = _dtConsignedProduceDetail.NewRow();
                    dr["TYPE"] = "생산";
                    dr["NAME"] = obj["NAME"];
                    dr["MODEL"] = obj["MODEL"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedProduceDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedDetail.NewRow();
                    dr1["CATEGORY"] = 3;
                    dr1["TYPE"] = "생산";
                    dr1["NAME"] = obj["NAME"];
                    dr1["MODEL"] = obj["MODEL"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedDetail.Rows.Add(dr1);
                }

                JArray jArrayDelivery = JArray.Parse(jResult["DELIVERY"].ToString());

                foreach (JObject obj in jArrayDelivery.Children<JObject>())
                {
                    DataRow dr = _dtConsignedDeliveryDetail.NewRow();
                    dr["TYPE"] = "물류";
                    dr["NAME"] = obj["NAME"];
                    dr["MODEL"] = obj["MODEL"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedDeliveryDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedDetail.NewRow();
                    dr1["CATEGORY"] = 4;
                    dr1["TYPE"] = "물류";
                    dr1["NAME"] = obj["NAME"];
                    dr1["MODEL"] = obj["MODEL"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedDetail.Rows.Add(dr1);
                }



                JArray jArrayReturnProduct = JArray.Parse(jResult["RETURN_PRODUCT"].ToString());

                foreach (JObject obj in jArrayReturnProduct.Children<JObject>())
                {
                    DataRow dr = _dtConsignedProductReturnDetail.NewRow();
                    dr["TYPE"] = "제품";
                    dr["NAME"] = obj["NAME"];
                    dr["MODEL"] = obj["MODEL"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedProductReturnDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedReturnDetail.NewRow();
                    dr1["CATEGORY"] = 1;
                    dr1["TYPE"] = "제품";
                    dr1["NAME"] = obj["NAME"];
                    dr1["MODEL"] = obj["MODEL"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedReturnDetail.Rows.Add(dr1);
                }

                JArray jArrayReturnPart = JArray.Parse(jResult["RETURN_PART"].ToString());

                foreach (JObject obj in jArrayReturnPart.Children<JObject>())
                {
                    DataRow dr = _dtConsignedPartReturnDetail.NewRow();
                    dr["TYPE"] = "부품";
                    dr["NAME"] = obj["COMPONENT_CD"];
                    dr["MODEL"] = obj["MODEL_NM"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedPartReturnDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedReturnDetail.NewRow();
                    dr1["CATEGORY"] = 2;
                    dr1["TYPE"] = "부품";
                    dr1["NAME"] = obj["COMPONENT_CD"];
                    dr1["MODEL"] = obj["MODEL_NM"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedReturnDetail.Rows.Add(dr1);
                }

                JArray jArrayReturnProduce = JArray.Parse(jResult["RETURN_PRODUCE"].ToString());

                foreach (JObject obj in jArrayReturnProduce.Children<JObject>())
                {
                    DataRow dr = _dtConsignedProduceReturnDetail.NewRow();
                    dr["TYPE"] = "생산";
                    dr["NAME"] = obj["NAME"];
                    dr["MODEL"] = obj["MODEL"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedProduceReturnDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedReturnDetail.NewRow();
                    dr1["CATEGORY"] = 3;
                    dr1["TYPE"] = "생산";
                    dr1["NAME"] = obj["NAME"];
                    dr1["MODEL"] = obj["MODEL"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedReturnDetail.Rows.Add(dr1);
                }

                JArray jArrayReturnDelivery = JArray.Parse(jResult["RETURN_DELIVERY"].ToString());

                foreach (JObject obj in jArrayReturnDelivery.Children<JObject>())
                {
                    DataRow dr = _dtConsignedDeliveryReturnDetail.NewRow();
                    dr["TYPE"] = "물류";
                    dr["NAME"] = obj["NAME"];
                    dr["MODEL"] = obj["MODEL"];
                    dr["PRICE"] = obj["PRICE"];
                    dr["CNT"] = obj["CNT"];
                    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedDeliveryReturnDetail.Rows.Add(dr);

                    DataRow dr1 = _dtConsignedReturnDetail.NewRow();
                    dr1["CATEGORY"] = 4;
                    dr1["TYPE"] = "물류";
                    dr1["NAME"] = obj["NAME"];
                    dr1["MODEL"] = obj["MODEL"];
                    dr1["PRICE"] = obj["PRICE"];
                    dr1["CNT"] = obj["CNT"];
                    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                    _dtConsignedReturnDetail.Rows.Add(dr1);
                }

                //JArray jArrayExchange = JArray.Parse(jResult["EXCHANGE"].ToString());
                string exchangeCustomer = ConvertUtil.ToString(jResult["EXCHANGE_CUSTOMER"]);

                //foreach (JObject obj in jArrayExchange.Children<JObject>())
                //{
                //    DataRow dr = _dtConsignedExchangeDetail.NewRow();
                //    dr["TYPE"] = "교환";
                //    dr["NAME"] = obj["NAME"];
                //    dr["MODEL"] = obj["MODEL"];
                //    dr["PRICE"] = obj["PRICE"];
                //    dr["CNT"] = obj["CNT"];
                //    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                //    _dtConsignedExchangeDetail.Rows.Add(dr);

                //    DataRow dr1 = _dtConsignedDetail.NewRow();
                //    dr1["CATEGORY"] = 5;
                //    dr1["TYPE"] = "환불";
                //    dr1["NAME"] = obj["NAME"];
                //    dr1["MODEL"] = obj["MODEL"];
                //    dr1["PRICE"] = obj["PRICE"];
                //    dr1["CNT"] = obj["CNT"];
                //    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                //    _dtConsignedDetail.Rows.Add(dr1);
                //}

                //JArray jArrayRefund = JArray.Parse(jResult["REFUND"].ToString());
                string refundCustomer = ConvertUtil.ToString(jResult["REFUND_CUSTOMER"]);

                //foreach (JObject obj in jArrayRefund.Children<JObject>())
                //{
                //    DataRow dr = _dtConsignedRefundDetail.NewRow();
                //    dr["TYPE"] = "환불";
                //    dr["NAME"] = obj["NAME"];
                //    dr["MODEL"] = obj["MODEL"];
                //    dr["PRICE"] = obj["PRICE"];
                //    dr["CNT"] = obj["CNT"];
                //    dr["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                //    _dtConsignedRefundDetail.Rows.Add(dr);

                //    DataRow dr1 = _dtConsignedDetail.NewRow();
                //    dr1["CATEGORY"] = 6;
                //    dr1["TYPE"] = "환불";
                //    dr1["NAME"] = obj["NAME"];
                //    dr1["MODEL"] = obj["MODEL"];
                //    dr1["PRICE"] = obj["PRICE"];
                //    dr1["CNT"] = obj["CNT"];
                //    dr1["TOTAL_PRICE"] = obj["TOTAL_PRICE"];
                //    _dtConsignedDetail.Rows.Add(dr1);
                //}

                meDesc.Text = $@"
교환건
 - {exchangeCustomer}

환불건
- {refundCustomer}";
            }
            else
            {

            }

            return true;
        }

        private void Root1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcConsignedAdjustmentSummary.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
        }

        private void lcgReturn_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcConsignedReturnDetail.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
        }


        private void lcgProcess_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (SaveFileDialog form = new SaveFileDialog())
                {
                    form.Filter = "Excel 통합문서|*.xlsx";
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gcConsignedDetail.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                }
            }
        }

        
    }
}