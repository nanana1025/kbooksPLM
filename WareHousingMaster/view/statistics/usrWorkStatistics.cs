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
using DevExpress.XtraSplashScreen;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.inventory;
using Newtonsoft.Json;

namespace WareHousingMaster.view.statistics
{
    public partial class usrWorkStatistics : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtWarehouse;
        DataTable _dtPallet;
        DataTable _dtPalletSelect;
        DataTable _dtComponentCd;
        DataTable _dtComponent;

        DataTable _dtPalletList;
        DataTable _dtComponentCdList;
        DataTable _dtComponentList;

        BindingSource bsWarehouse;
        BindingSource bsComponentCd;
        BindingSource bsComponent;
        BindingSource _bsPallet;

        BindingSource bsPalletList;
        BindingSource bsComponentCdList;
        BindingSource bsComponentList;

        //DataTable _dtLocation;
        //DataTable _dtPallet;
        //DataTable _dtBasket;
        DataRowView _currentRowWarehouse;
        DataRowView _currentRowDetail;
        List<string> _listBarcode;
        List<long> _listInventoryId;

        Dictionary<string, string> _dicPallet;
        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        long _warehouseMovementId;
        long _inventoryId;
        bool _isValidRow;

        int interval = 5000;


        public usrWorkStatistics()
        {
            InitializeComponent();

            //warehouseTimer.Interval = interval;

            //long _warehouseMovementId = -1;

            //_dtComponentCd = new DataTable();
            //_dtComponentCd.Columns.Add(new DataColumn("COMPONENT_CD_NM", typeof(string)));
            //_dtComponentCd.Columns.Add(new DataColumn("COMPONENT_CD_NO", typeof(string)));

            //_dtComponent = new DataTable();
            //_dtComponent.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            //_dtComponent.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            //_dtComponent.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            //_dtComponent.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            //_dtPalletList = new DataTable();
            //_dtPalletList.Columns.Add(new DataColumn("PALLET", typeof(string)));
            //_dtPalletList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            //_dtPalletList.Columns.Add(new DataColumn("CNT", typeof(int)));

            //_dtComponentCdList = new DataTable();
            //_dtComponentCdList.Columns.Add(new DataColumn("PALLET", typeof(string)));
            //_dtComponentCdList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            //_dtComponentCdList.Columns.Add(new DataColumn("CNT", typeof(int)));

            //_dtComponentList = new DataTable();
            //_dtComponentList.Columns.Add(new DataColumn("PALLET", typeof(string)));
            //_dtComponentList.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            //_dtComponentList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            //_dtComponentList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            //_dtComponentList.Columns.Add(new DataColumn("CNT", typeof(int)));


            //bsWarehouse = new BindingSource();
            //_bsPallet = new BindingSource();
            //bsComponentCd = new BindingSource();
            //bsComponent = new BindingSource();

            //bsPalletList = new BindingSource();
            //bsComponentCdList = new BindingSource();
            //bsComponentList = new BindingSource();


            //_listBarcode = new List<string>();
            //_listInventoryId = new List<long>();

            //_dicPallet = new Dictionary<string, string>();
        }

        private void usrWarehouseState_Load(object sender, EventArgs e)
        {
            //setIInitData();
        }


        //private void setIInitData()
        //{
        //    _dtWarehouse = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
        //    _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
        //    _dtComponentCd = Util.getCodeList("CD0101", "KEY", "VALUE");

        //    _dtPalletSelect = _dtPallet.Copy();

        //    Util.LookupEditHelper(leWarehouseNo, _dtWarehouse, "KEY", "VALUE");
        //    Util.LookupEditHelper(leWarehouseNo1, _dtWarehouse, "KEY", "VALUE");
        //    Util.LookupEditHelper(leWarehouseNo2, _dtWarehouse, "KEY", "VALUE");
        //    Util.LookupEditHelper(leWarehouseNo3, _dtWarehouse, "KEY", "VALUE");

        //    Util.LookupEditHelper(rilePalletSelect, _dtPallet, "PALLET_ID", "PALLET_NM");
        //    Util.LookupEditHelper(rilePalletComponent, _dtPallet, "PALLET_ID", "PALLET_NM");
        //    Util.LookupEditHelper(rilePalletInventory, _dtPallet, "PALLET_ID", "PALLET_NM");


        //    foreach (DataRow row in _dtPallet.Rows)
        //        if (!_dicPallet.ContainsKey(ConvertUtil.ToString(row["PALLET_NM"])))
        //            _dicPallet.Add(ConvertUtil.ToString(row["PALLET_ID"]), ConvertUtil.ToString(row["PALLET_NM"]));

        //    Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
        //    dicPalletDefault.Add("WAREHOUSE_ID", "-1");
        //    dicPalletDefault.Add("PALLET_ID", "");
        //    dicPalletDefault.Add("PALLET_NM", "선택안합");

        //    Util.insertRowonTop(_dtPalletSelect, dicPalletDefault);
        //    Util.insertRowonTop(_dtComponentCd, "-1", "선택안합");

        //    gcWarehouse.DataSource = null;
        //    bsWarehouse.DataSource = _dtWarehouse;
        //    gcWarehouse.DataSource = bsWarehouse;

        //    gcPallet.DataSource = null;
        //    _bsPallet.DataSource = _dtPalletSelect;
        //    gcPallet.DataSource = _bsPallet;

        //    gcComponentCd.DataSource = null;
        //    bsComponentCd.DataSource = _dtComponentCd;
        //    gcComponentCd.DataSource = bsComponentCd;

        //    gcComponent.DataSource = null;
        //    bsComponent.DataSource = _dtComponent;
        //    gcComponent.DataSource = bsComponent;

        //    gcPalletList.DataSource = null;
        //    bsPalletList.DataSource = _dtPalletList;
        //    gcPalletList.DataSource = bsPalletList;

        //    gcComponentCdList.DataSource = null;
        //    bsComponentCdList.DataSource = _dtComponentCdList;
        //    gcComponentCdList.DataSource = bsComponentCdList;

        //    gcComponentList.DataSource = null;
        //    bsComponentList.DataSource = _dtComponentList;
        //    gcComponentList.DataSource = bsComponentList;




        //}

        //private void gvWarehouse_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        //{
        //    bool isValidRow = (e.FocusedRowHandle >= 0 && gvWarehouse.RowCount > 0);

        //    if (isValidRow)
        //    {
        //        _currentRowWarehouse = e.Row as DataRowView;
        //        _bsPallet.Filter = $"WAREHOUSE_ID = '{_currentRowWarehouse["KEY"]}' OR WAREHOUSE_ID = '-1'";
        //    }
        //}

        //private void gvComponentCd_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        //{
        //    _isValidRow = (e.FocusedRowHandle >= 0 && gvComponentCd.RowCount > 0);

        //    if (_isValidRow)
        //    {

        //    }
        //}

        //private void getComponent()
        //{
        //    int[] rows = gvComponentCd.GetSelectedRows();

        //    _dtComponent.Clear();

        //    if (rows.Length > 0)
        //    {
        //        List<string> listComponentCd = new List<string>();

        //        for (int i = 0; i < rows.Length; i++)
        //        {
        //            DataRow drv = gvComponentCd.GetDataRow(rows[i]);
        //            listComponentCd.Add(ConvertUtil.ToString(drv["KEY"]));
        //        }

        //        JObject jResult = new JObject();

        //        if (DBConnect.getComponentList(listComponentCd, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    DataRow dr = _dtComponent.NewRow();
        //                    dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
        //                    dr["COMPONENT"] = obj["COMPONENT"];
        //                    dr["MODEL_NM"] = obj["MODEL_NM"];
        //                    dr["CHECK"] = false;
        //                    _dtComponent.Rows.Add(dr);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
        //        }

        //    }
        //}

        //private void sbSearch_Click_1(object sender, EventArgs e)
        //{
        //    setWarehouseState();
        //    //warehouseTimer.Enabled = true;
        //}

        //private void setWarehouseState()
        //{
        //    //usrHarnessDetail1.LoadData(harnessId, _harnessControl, _harnessPartControl, _harnessCostControl);

        //    int warehouseIndex = gvWarehouse.FocusedRowHandle;

        //    int palletIndex = gvPallet.FocusedRowHandle;
        //    int[] palletRow = gvPallet.GetSelectedRows();

        //    int componentCdIndex = gvComponentCd.FocusedRowHandle;
        //    int[] componentCdRow = gvComponentCd.GetSelectedRows();

        //    DataRow[] rows = _dtComponent.Select("CHECK = true");
            
        //    if (palletRow.Length <= 1 && palletIndex < 1 &&
        //    componentCdRow.Length <= 1 && componentCdIndex < 1
        //    )
        //    {
        //        string warehouseCurCnt = "0";
        //        string warehousePreCnt = "0";
        //        string state = "R";
        //        JObject jResult = new JObject();
        //        string query = $"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY WHERE LOCATION = {_currentRowWarehouse["KEY"]}";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    warehouseCurCnt = ConvertUtil.ToString(obj["CNT"]);
        //                }  
        //            }
        //        }


        //        query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, RELEASE_WAREHOUSE_ID, WAREHOUSING_WAREHOUSE_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE,
        //                                    RELEASE_WAREHOUSE_ID,
        //                                    WAREHOUSING_WAREHOUSE_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]} OR A.WAREHOUSING_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]})
        //                                    AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')
                                    
        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)";

        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                    string releaseWarehouseNo = ConvertUtil.ToString(obj["RELEASE_WAREHOUSE_ID"]);
        //                    string warehousingWarehouseNo = ConvertUtil.ToString(obj["WAREHOUSING_WAREHOUSE_ID"]);
        //                    string selectWarehousegNo = ConvertUtil.ToString(_currentRowWarehouse["KEY"]);
        //                    if (selectWarehousegNo.Equals(releaseWarehouseNo) && selectWarehousegNo.Equals(warehousingWarehouseNo))
        //                        warehousePreCnt = warehouseCurCnt;
        //                    else if(selectWarehousegNo.Equals(releaseWarehouseNo))
        //                        warehousePreCnt = ConvertUtil.ToString(ConvertUtil.ToInt32(warehouseCurCnt) + ConvertUtil.ToInt32(obj["CNT"]));
        //                    else
        //                        warehousePreCnt = ConvertUtil.ToString(ConvertUtil.ToInt32(warehouseCurCnt) - ConvertUtil.ToInt32(obj["CNT"]));                   
        //                }

        //            }
        //        }

        //        leWarehouseNo.EditValue = _currentRowWarehouse["VALUE"];
        //        lcPreCnt.Text = warehousePreCnt;
        //        lcCurCnt.Text = warehouseCurCnt;

        //        tcWarehouseState.SelectedTabPage = xtpWarehouse;
        //    }
        //    else if (palletIndex > 0 && palletRow.Length > 0 && componentCdRow.Length <= 1 && componentCdIndex <= 1)
        //    {
        //        List<string> lisPallet = new List<string>();
        //        string PalletCurCnt = "0";
        //        string PalletPreCnt = "0";
        //        string state = "R";
        //        for (int i = 0; i < palletRow.Length; i++)
        //        {
        //            if (palletRow[i] == 0)
        //                continue;

        //            DataRow drv = gvPallet.GetDataRow(palletRow[i]);
        //            lisPallet.Add(ConvertUtil.ToString(drv["PALLET_ID"]));
        //        }

        //        JObject jResult = new JObject();
        //        string query = $"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)})";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    PalletCurCnt = ConvertUtil.ToString(obj["CNT"]);
        //                }
        //            }
        //        }

        //        int preCnt = ConvertUtil.ToInt32(PalletCurCnt); 

        //        foreach (string pallet in lisPallet)
        //        {
        //            query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_PALLET_ID = {pallet} OR A.WAREHOUSING_PALLET_ID = {pallet}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')
                                    
        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)";

        //            if (DBConnect.queryDT(query, ref jResult))
        //            {
        //                if (Convert.ToBoolean(jResult["EXIST"]))
        //                {
        //                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                    foreach (JObject obj in jArray.Children<JObject>())
        //                    {
        //                        state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                        string releasePalletNo = ConvertUtil.ToString(obj["RELEASE_PALLET_ID"]);
        //                        string warehousingPalletNo = ConvertUtil.ToString(obj["WAREHOUSING_PALLET_ID"]);
        //                        if (pallet.Equals(releasePalletNo) && pallet.Equals(warehousingPalletNo))
        //                            preCnt += 0; // 여기 수정
        //                        else if (pallet.Equals(releasePalletNo))
        //                            preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                        else
        //                            preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                    }

        //                }
        //            }
        //        }

        //        PalletPreCnt = ConvertUtil.ToString(preCnt);

        //        query = $@"SELECT PALLET, COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                    WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)})
        //                    GROUP BY PALLET, COMPONENT_CD ORDER BY PALLET, COMPONENT_CD";
        //        _dtPalletList.Clear();
        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {  
        //                    DataRow dr = _dtPalletList.NewRow();
        //                    dr["PALLET"] = obj["PALLET"];
        //                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
        //                    dr["CNT"] = obj["CNT"];
        //                    _dtPalletList.Rows.Add(dr);
                            
        //                }
        //            }
        //        }

                


        //        leWarehouseNo1.EditValue = _currentRowWarehouse["VALUE"];

        //        List<string> listPallet = new List<string>();
        //        foreach(string pallet in lisPallet)
        //        {
        //            listPallet.Add(_dicPallet[pallet]);
        //        }

        //        tePallet.Text = string.Join(",", listPallet);

        //        lcPrePallletCnt.Text = PalletPreCnt;
        //        lcCurPalletCnt.Text = PalletCurCnt;

        //        tcWarehouseState.SelectedTabPage = xtpPallet;
        //    }
        //    else if (componentCdRow.Length > 0 && componentCdIndex > 0 && rows.Length < 1)
        //    {
        //        int componentCdCurCnt = 0;
        //        int componentCdPreCnt = 0;
        //        string state = "R";
        //        bool isSelectPallet = false;
        //        string query = "";
        //        List<string> lisComponentCd = new List<string>();
        //        List<string> lisPallet = new List<string>();

        //        if (palletIndex > 0 && palletRow.Length > 0)
        //            isSelectPallet = true;

        //        if (isSelectPallet)
        //        {                    
        //            for (int i = 0; i < palletRow.Length; i++)
        //            {
        //                if (palletRow[i] == 0)
        //                    continue;
        //                DataRow drv = gvPallet.GetDataRow(palletRow[i]);
        //                lisPallet.Add(ConvertUtil.ToString(drv["PALLET_ID"]));
        //            }
        //        }

        //        for (int i = 0; i < componentCdRow.Length; i++)
        //        {
        //            if (componentCdRow[i] == 0)
        //                continue;
        //            DataRow drv = gvComponentCd.GetDataRow(componentCdRow[i]);
        //            lisComponentCd.Add(ConvertUtil.ToString(drv["KEY"]));
        //        }

        //        JObject jResult = new JObject();

        //        if (isSelectPallet)
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)}) AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";
        //        else
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    componentCdCurCnt = ConvertUtil.ToInt32(obj["CNT"]);
        //                }
        //            }
        //        }

        //        int preCnt = componentCdCurCnt;
        //        if (isSelectPallet)
        //        {
        //            foreach (string pallet in lisPallet)
        //            {

        //                query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_PALLET_ID = {pallet} OR A.WAREHOUSING_PALLET_ID = {pallet}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')
                                    
        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";
                   
        //                if (DBConnect.queryDT(query, ref jResult))
        //                {
        //                    if (Convert.ToBoolean(jResult["EXIST"]))
        //                    {
        //                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                        foreach (JObject obj in jArray.Children<JObject>())
        //                        {
        //                            state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                            string releasePalletNo = ConvertUtil.ToString(obj["RELEASE_PALLET_ID"]);
        //                            string warehousingPalletNo = ConvertUtil.ToString(obj["WAREHOUSING_PALLET_ID"]);
        //                            if (pallet.Equals(releasePalletNo) && pallet.Equals(warehousingPalletNo))
        //                                preCnt += 0; // 여기 수정
        //                            else if (pallet.Equals(releasePalletNo))
        //                                preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                            else
        //                                preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]} OR A.WAREHOUSING_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')
                                    
        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";

        //            if (DBConnect.queryDT(query, ref jResult))
        //            {
        //                if (Convert.ToBoolean(jResult["EXIST"]))
        //                {
        //                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                    foreach (JObject obj in jArray.Children<JObject>())
        //                    {
        //                        state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                        string releaseWarehouseNo = ConvertUtil.ToString(obj["RELEASE_WAREHOUSE_ID"]);
        //                        string warehousingWarehouseNo = ConvertUtil.ToString(obj["WAREHOUSING_WAREHOUSE_ID"]);
        //                        string selectWarehousegNo = ConvertUtil.ToString(_currentRowWarehouse["KEY"]);
        //                        if (releaseWarehouseNo.Equals(warehousingWarehouseNo))
        //                            preCnt += 0; // 여기 수정
        //                        else if (selectWarehousegNo.Equals(releaseWarehouseNo))
        //                            preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                        else
        //                            preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                    }

        //                }
        //            }
        //        }

        //        componentCdPreCnt = preCnt;

        //        if (isSelectPallet)
        //            query = $@"SELECT PALLET, COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                    WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)}) AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')
        //                    GROUP BY PALLET, COMPONENT_CD ORDER BY PALLET, COMPONENT_CD";
        //        else
        //            query = $@"SELECT PALLET, COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                    WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')
        //                    GROUP BY PALLET, COMPONENT_CD ORDER BY PALLET, COMPONENT_CD";
        //        _dtComponentCdList.Clear();
        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    DataRow dr = _dtComponentCdList.NewRow();
        //                    dr["PALLET"] = obj["PALLET"];
        //                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
        //                    dr["CNT"] = obj["CNT"];
        //                    _dtComponentCdList.Rows.Add(dr);

        //                }
        //            }
        //        }

        //        leWarehouseNo2.EditValue = _currentRowWarehouse["KEY"];
        //        if(isSelectPallet)
        //        {
        //            lcPalletName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //            lcPallet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //            List<string> listPallet = new List<string>();
        //            foreach (string pallet in lisPallet)
        //            {
        //                listPallet.Add(_dicPallet[pallet]);
        //            }

        //            tePallet1.Text = string.Join(",", listPallet);
        //        }
        //        else
        //        {
        //            lcPalletName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //            lcPallet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        }

        //        teComponentCd.Text = string.Join(",", lisComponentCd);
        //        lcPreComponentCdCnt.Text = ConvertUtil.ToString(componentCdPreCnt);
        //        lcCurComponentCdCnt.Text = ConvertUtil.ToString(componentCdCurCnt);

        //        tcWarehouseState.SelectedTabPage = xtpComponentCd;
        //    }
        //    else if (rows.Length > 0)
        //    {
        //        int componentCurCnt = 0;
        //        int componentPreCnt = 0;
        //        string state = "R";
        //        bool isSelectPallet = false;
        //        string query = "";
        //        List<string> lisComponentCd = new List<string>();
        //        List<string> lisComponent = new List<string>();
        //        List<long> lisComponentId = new List<long>();
        //        List<string> lisPallet = new List<string>();

        //        if (palletIndex > 0 && palletRow.Length > 0)
        //            isSelectPallet = true;

        //        if (isSelectPallet)
        //        {
        //            for (int i = 0; i < palletRow.Length; i++)
        //            {
        //                if (palletRow[i] == 0)
        //                    continue;
        //                DataRow drv = gvPallet.GetDataRow(palletRow[i]);
        //                lisPallet.Add(ConvertUtil.ToString(drv["PALLET_ID"]));
        //            }
        //        }

        //        for (int i = 0; i < componentCdRow.Length; i++)
        //        {
        //            if (componentCdRow[i] == 0)
        //                continue;
        //            DataRow drv = gvComponentCd.GetDataRow(componentCdRow[i]);
        //            lisComponentCd.Add(ConvertUtil.ToString(drv["KEY"]));
        //        }

        //        foreach (DataRow row in rows)
        //        {
        //            lisComponent.Add(ConvertUtil.ToString(row["COMPONENT"]));
        //            lisComponentId.Add(ConvertUtil.ToInt64(row["COMPONENT_ID"]));
        //        }
               

        //        JObject jResult = new JObject();

        //        if (isSelectPallet)
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)}) AND COMPONENT_ID IN ({string.Join(",", lisComponentId)})";
        //        else
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND COMPONENT_ID IN ({string.Join(",", lisComponentId)})";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    componentCurCnt = ConvertUtil.ToInt32(obj["CNT"]);
        //                }
        //            }
        //        }

        //        int preCnt = componentCurCnt;
        //        if (isSelectPallet)
        //        {
        //            foreach (string pallet in lisPallet)
        //            {

        //                query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_PALLET_ID = {pallet} OR A.WAREHOUSING_PALLET_ID = {pallet}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')
                                    
        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_ID IN ({string.Join(",", lisComponentId)})";

        //                if (DBConnect.queryDT(query, ref jResult))
        //                {
        //                    if (Convert.ToBoolean(jResult["EXIST"]))
        //                    {
        //                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                        foreach (JObject obj in jArray.Children<JObject>())
        //                        {
        //                            state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                            string releasePalletNo = ConvertUtil.ToString(obj["RELEASE_PALLET_ID"]);
        //                            string warehousingPalletNo = ConvertUtil.ToString(obj["WAREHOUSING_PALLET_ID"]);
        //                            if (pallet.Equals(releasePalletNo) && pallet.Equals(warehousingPalletNo))
        //                                preCnt += 0; // 여기 수정
        //                            else if (pallet.Equals(releasePalletNo))
        //                                preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                            else
        //                                preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]} OR A.WAREHOUSING_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')
                                    
        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_CD IN ({string.Join(",", lisComponentId)})";

        //            if (DBConnect.queryDT(query, ref jResult))
        //            {
        //                if (Convert.ToBoolean(jResult["EXIST"]))
        //                {
        //                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                    foreach (JObject obj in jArray.Children<JObject>())
        //                    {
        //                        state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                        string releaseWarehouseNo = ConvertUtil.ToString(obj["RELEASE_WAREHOUSE_ID"]);
        //                        string warehousingWarehouseNo = ConvertUtil.ToString(obj["WAREHOUSING_WAREHOUSE_ID"]);
        //                        string selectWarehousegNo = ConvertUtil.ToString(_currentRowWarehouse["KEY"]);
        //                        if (releaseWarehouseNo.Equals(warehousingWarehouseNo))
        //                            preCnt += 0; // 여기 수정
        //                        else if (selectWarehousegNo.Equals(releaseWarehouseNo))
        //                            preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                        else
        //                            preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                    }

        //                }
        //            }
        //        }

        //        componentPreCnt = preCnt;

        //        string modelquery = $@"CASE
			     //                       WHEN A.COMPONENT_CD = 'CPU'
			     //                       THEN (SELECT MODEL_NM FROM TN_CPU B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'MBD'
			     //                       THEN (SELECT

			     //                       CONCAT(IFNULL(MANUFACTURE_NM,''),'/',
			     //                       IFNULL(
			     //                       CASE
				    //                        WHEN B.MANUFACTURE_NM = 'LENOVO' OR B.MANUFACTURE_NM = 'TOSHIBA'
				    //                        THEN B.SYSTEM_VERSION
				    //                        ELSE B.MBD_MODEL_NM
				    //                        END, '')
			     //                       ,'/', IFNULL(PRODUCT_NAME, ''))

			     //                       FROM TN_MBD B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'MEM'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',IFNULL(BANDWIDTH,''),'/',IFNULL(CAPACITY,'')) FROM TN_MEM B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'STG'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',IFNULL(CAPACITY,'')) FROM TN_STG B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'VGA'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',IFNULL(CAPACITY,'')) FROM TN_VGA B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'MON'
			     //                       THEN (SELECT CONCAT(IFNULL(MODEL_NM,''),'/',IFNULL(MODEL_ID,''), '/', SIZE) FROM TN_MON B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'CAS'
			     //                       THEN(SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',CASE_CAT,'/',CASE_TYPE) FROM TN_CAS B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'POW'
			     //                       THEN(SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',POW_CAT,'/',POW_TYPE) FROM TN_POW B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'ADP'
			     //                       THEN(SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',ADP_CAT,'/', OUTPUT_WATT,'V/', OUTPUT_AMPERE,'A') FROM TN_ADP B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'KEY'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',KEY_CAT,'/', KEY_TYPE) FROM TN_KEY B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'MOU'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',MOU_CAT,'/', MOU_TYPE) FROM TN_MOU B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'FAN'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',FAN_CAT,'/', FAN_TYPE) FROM TN_FAN B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'CAB'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',CAB_CAT,'/', CAB_TYPE) FROM TN_CAB B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'BAT'
			     //                       THEN  (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',BAT_CAT,'/', OUTPUT_WATT,'V/',OUTPUT_AMPERE,'mAh') FROM TN_BAT B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'PKG'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',PACKAGE_TYPE,'/', CATEGORY,'/',SIZE) FROM TN_PKG B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'AIR'
			     //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',TYPE,'/', CATEGORY,'/',SIZE) FROM TN_AIR B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'LIC'
			     //                       THEN (SELECT CONCAT(IFNULL(TYPE,''),'/',IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/', IFNULL(ETC,'')) FROM TN_LIC B WHERE A.DATA_ID = B.DATA_ID)
			     //                       WHEN A.COMPONENT_CD = 'PER'
			     //                       THEN (SELECT CONCAT(IFNULL(TYPE,''),'/',IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/', IFNULL(ETC,'')) FROM TN_PER B WHERE A.DATA_ID = B.DATA_ID)
		      //                      END AS MODEL_NM";

        //        if (isSelectPallet)
        //            query = $@"SELECT A.PALLET, A.COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT, {modelquery} FROM TN_INVENTORY A
        //                    WHERE A.LOCATION = {_currentRowWarehouse["KEY"]} AND A.PALLET IN ({string.Join(",", lisPallet)}) AND A.COMPONENT_ID IN ({string.Join(",", lisComponentId)})
        //                    GROUP BY A.PALLET, A.COMPONENT_CD ORDER BY A.PALLET, A.COMPONENT_CD";
        //        else
        //            query = $@"SELECT A.PALLET, A.COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT, {modelquery} FROM TN_INVENTORY A
        //                    WHERE A.LOCATION = {_currentRowWarehouse["KEY"]} AND A.COMPONENT_ID IN ({string.Join(",", lisComponentId)})
        //                    GROUP BY A.PALLET, A.COMPONENT_CD ORDER BY A.PALLET, A.COMPONENT_CD";

        //        _dtComponentCdList.Clear();
        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    DataRow dr = _dtComponentList.NewRow();
        //                    dr["PALLET"] = obj["PALLET"];
        //                    //dr["COMPONENT"] = obj["COMPONENT"];
        //                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
        //                    dr["MODEL_NM"] = obj["MODEL_NM"];
        //                    dr["CNT"] = obj["CNT"];
        //                    _dtComponentList.Rows.Add(dr);

        //                }
        //            }
        //        }

        //        leWarehouseNo3.EditValue = _currentRowWarehouse["KEY"];

        //        List<string> listPallet = new List<string>();
        //        foreach (string pallet in lisPallet)
        //        {
        //            listPallet.Add(_dicPallet[pallet]);
        //        }

        //        tePallet2.Text = string.Join(",", listPallet);
        //        teComponentCd2.Text = string.Join(",", lisComponentCd);
        //        teComponent.Text = string.Join(",", lisComponent);
        //        lcPreComponentCnt.Text = ConvertUtil.ToString(componentPreCnt);
        //        lcCurComponentCnt.Text = ConvertUtil.ToString(componentCurCnt);

        //        tcWarehouseState.SelectedTabPage = xtpComponent;
        //    }
        //}


        //private void gvComponentCd_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        //{
        //    if (_isValidRow)
        //    {
        //        getComponent();
        //    }
            
        //}

        //private void warehouseTimer_Tick(object sender, EventArgs e)
        //{
        //    setWarehouseState();
        //}

        //private void sbSearch_Click(object sender, EventArgs e)
        //{
        //    string dtFrom = "";
        //    string dtTo = "";
 

        //    if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
        //        dtFrom = $"{deDtFrom.Text} 00:00:00";

        //    if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
        //        dtTo = $"{deDtTo.Text} 23:59:59";

        //    JObject jResult = new JObject();
        //    getWarehouseMovementList(teRegistNo.EditValue,
        //        teBarcode.EditValue,
        //        ConvertUtil.ToString(leWarehouseMovementStatus.EditValue),
        //        dtFrom, dtTo, dtRegist, dtRelease, dtWarehousing, leReleaseWarehouseNo.EditValue, leWarehousingWarehouseNo.EditValue, ref jResult);

        //}
    }
}