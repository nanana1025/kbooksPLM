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
using DevExpress.XtraGrid.Views.Grid;
using WareHousingMaster.view.warehousing.createComponent;
using WareHousingMaster.view.warehousing.selectComponent;
using System.Collections;

namespace WareHousingMaster.view.produce
{
    public partial class usrWarehousingProduce : DevExpress.XtraEditors.XtraForm
    {

        string _warehousing = "WAREHOUSING";
        string _produce = "WAREHOUSING";

        DataTable _dtBarcode;
        DataTable _dtProduct;
        long _warehousingId;

        BindingSource _bs;
        BindingSource _bsInventory;

        DataRowView _currentBarcode = null;
        DataRowView _currentView;

        List<int> _listBasis;
        List<string> _listBarcode;

        int _currentPage;
        int _totalPage;
        int _currentBasis;
        int _totalCnt;

        string _inventoryBarcode;
        List<string> _listBarcodeSearch;

        public usrWarehousingProduce()
        {
            InitializeComponent();


            _dtBarcode = new DataTable();
            _dtBarcode.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtBarcode.Columns.Add(new DataColumn("EXIST", typeof(bool)));

            _dtProduct = new DataTable();

            _dtProduct.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtProduct.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("RELEASES", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("PRODUCE_STATE_ORIGINAL", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("PRODUCE_STATE", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(int)));
            _dtProduct.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            //_dtProduct.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            //_dtProduct.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            //_dtProduct.Columns.Add(new DataColumn("SB_NM", typeof(string)));
            //_dtProduct.Columns.Add(new DataColumn("FAMILY_NM", typeof(string)));
            //_dtProduct.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));
            //_dtProduct.Columns.Add(new DataColumn("SYSTEM_SN", typeof(string)));

            _bs = new BindingSource();
            _bsInventory = new BindingSource();
            _bs.DataSource = _dtProduct;

            _listBasis = new List<int>(new[] {20, 50, 100, 200, 500 });
            _listBarcode = new List<string>();
            _listBarcodeSearch = new List<string>();

            _currentPage = 1;
            _totalPage = 0;
            _currentBasis = 20;
            _totalCnt = 0;
            _warehousingId = -1;

        }

        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();
        }

        public IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        private void setInfoBox()
        {
            DataTable dtInvnetoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState1, dtInvnetoryState, "KEY", "VALUE");

            DataTable dtNo = new DataTable();

            dtNo.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtNo.Columns.Add(new DataColumn("VALUE", typeof(string)));

            foreach(int size in _listBasis)
            {
                DataRow row = dtNo.NewRow();
                row["KEY"] = size;
                row["VALUE"] = size;
                dtNo.Rows.Add(row);
            }
            Util.LookupEditHelper(leBasis, dtNo, "KEY", "VALUE");

            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(rileNtbListId, dtNickName, "KEY", "VALUE");

            DataTable dtProduceState = Util.getCodeList("CD0804", "KEY", "VALUE");
            Util.insertRowonTop(dtProduceState, "0", "");
            Util.LookupEditHelper(rileProduceState, dtProduceState, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            leBasis.EditValueChanged -= leBasis_EditValueChanged;
            leBasis.EditValue = _currentBasis;
            leBasis.EditValueChanged += leBasis_EditValueChanged;
            lcPage.Text = "-/-";
            lcTotal.Text = "-개";

            //teModelNm.Text = "B365M Pro4";
        }

       

        private void setGridControl()
        {
            gcProduct.DataSource = null;
            gcProduct.DataSource = _bs;

            gcInventory.DataSource = null;
            _bsInventory.DataSource = _dtBarcode;
            gcInventory.DataSource = _bsInventory;

        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            searchProduct();
        }
        private void searchProduct()
        {
            
            _dtProduct.Clear();
            gvProduct.BeginDataUpdate();

            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("PRODUCT_TYPE", 2); //노트북
            jData.Add("LIST_BARCODE", string.Join(",", _listBarcode));

            if (DBConnect.getProductListByBarcode(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;
                    long releaseId;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        releaseId = ConvertUtil.ToInt64(obj["RELEASE_ID"]);
                        DataRow dr = _dtProduct.NewRow();

                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["WAREHOUSING"] = ConvertUtil.ToString(obj["WAREHOUSING"]);
                        dr["WAREHOUSING_ID"] = ConvertUtil.ToInt64(obj["WAREHOUSING_ID"]);
                        dr["RELEASES"] = ConvertUtil.ToString(obj["RELEASES"]);
                        dr["RELEASE_ID"] = releaseId;
                        
                        dr["PRODUCE_STATE_ORIGINAL"] = ConvertUtil.ToString(obj["PRODUCE_STATE"]);
                        if (releaseId > 0)
                            dr["PRODUCE_STATE"] = ConvertUtil.ToString(obj["PRODUCE_STATE"]);
                        else
                            dr["PRODUCE_STATE"] = 0;
                        dr["MANUFACTURE_TYPE"] = ConvertUtil.ToInt32(obj["MANUFACTURE_TYPE"]);
                        dr["NTB_LIST_ID"] = ConvertUtil.ToInt64(obj["NTB_LIST_ID"]);
                        
                        dr["CHECK"] = false;
                        //dr["REGIST_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);



                        //dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NM"];
                        //dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                        //dr["SB_NM"] = obj["SB_NM"];
                        //dr["FAMILY_NM"] = obj["FAMILY_NM"];
                        //dr["SERIAL_NO"] = obj["SERIAL_NO"];
                        //dr["SYSTEM_SN"] = obj["SYSTEM_SN"];


                        _dtProduct.Rows.Add(dr);

                        _listBarcodeSearch.Add(ConvertUtil.ToString(obj["BARCODE"]));
                    }

                    foreach (DataRow row in _dtBarcode.Rows)
                    {
                        if (!_listBarcodeSearch.Contains(ConvertUtil.ToString(row["BARCODE"])))
                            row["EXIST"] = false;
                        else
                            row["EXIST"] = true;
                    }
                    
                }

                gvProduct.EndDataUpdate();

                return;
            }
            else
            {
                //lcPage.Text = "-/-";
                //lcTotal.Text = "TOTAL -개";
                gvProduct.EndDataUpdate();
                return;
            }
        }



        private void sbMove_Click(object sender, EventArgs e)
        {

            _warehousing = teProduce.Text.ToUpper();

            if (string.IsNullOrWhiteSpace(_warehousing))
            {
                teProduce.Focus();
                Dangol.Message("이동할 입고번호를 입력하세요");
                return;

            }

            int[] selectedRowHandles = gvProduct.GetSelectedRows();

            if (selectedRowHandles.Count() < 0)
                Dangol.Message("선택된 제품이 없습니다.");
            else 
            { 
                if (Dangol.MessageYN("선택한 제품을 이동하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    ArrayList rows = new ArrayList();
                    var jArray = new JArray();

                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                            rows.Add(gvProduct.GetDataRow(selectedRowHandle));
                    }

                    try
                    {

                        gvProduct.BeginDataUpdate();

                        for (int i = 0; i < rows.Count; i++)
                        {
                            DataRow row = rows[i] as DataRow;

                            JObject jdata = new JObject();

                            long warehousingId = ConvertUtil.ToInt64(row["WAREHOUSING_ID"]);
                            if (warehousingId != -1)
                            {
                                jdata.Add("INVENTORY_ID", ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                                jdata.Add("WAREHOUSING_ID", warehousingId);

                                jArray.Add(jdata);
                            }
                        }

                        jobj.Add("DATA", jArray);

                        if (DBConnect.moveProduct(_warehousing, jobj, ref jResult))
                        {
                            _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);

                            for (int i = 0; i < rows.Count; i++)
                            {
                                DataRow row = rows[i] as DataRow;

                                JObject jdata = new JObject();

                                long warehousingId = ConvertUtil.ToInt64(row["WAREHOUSING_ID"]);
                                if (warehousingId != -1)
                                {

                                    row.BeginEdit();
                                    row["WAREHOUSING"] = _warehousing;
                                    row["WAREHOUSING_ID"] = _warehousingId;
                                    row.BeginEdit();
                                }
                            }

                        }
                    }
                    finally
                    {
                        gvProduct.EndDataUpdate();
                        //gvWarehousingL.EndUpdate();
                        //gvWarehousingL.MoveLast();

                        Dangol.Message("처리되었습니다.");
                    }
                }
            }
        }

        private void leBasis_EditValueChanged(object sender, EventArgs e)
        {
            _currentBasis = ConvertUtil.ToInt32(leBasis.EditValue);
            _currentPage = 1;
            searchProduct();
        }

        private void sbRight_Click_1(object sender, EventArgs e)
        {
            if (_currentPage < _totalPage)
            {
                _currentPage++;
                searchProduct();
            }
        }

        private void sbLeft_Click_1(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                searchProduct();
            }
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            string text = teBarcode.Text;

            string[] arrbarcode = text.Split(new char[] { ' ', '\n', '/', '\r', ',' });

            _dtBarcode.BeginInit();
            gvInventory.BeginDataUpdate();
            foreach (string barcode in arrbarcode)
            {
                if (!string.IsNullOrWhiteSpace(barcode))
                    if (!_listBarcode.Contains(barcode))
                    {
                        _listBarcode.Add(barcode);

                        DataRow dr = _dtBarcode.NewRow();
                        dr["BARCODE"] = barcode;
                        dr["EXIST"] = true;
                        _dtBarcode.Rows.InsertAt(dr, 0);
                    }
            }
            _dtBarcode.EndInit();
            gvInventory.EndDataUpdate();
        }

        private void lcgBarcode_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("재고번호의 제품 정보를 확인하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                Dangol.ShowSplash();

                searchProduct();

                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_currentBarcode == null)
                {
                    Dangol.Message("재고번호가 선택되지 않았습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 재고번호를 제외하시겠습니까?") == DialogResult.Yes)
                {


                    ArrayList rows = new ArrayList();

                    // Add the selected rows to the list.
                    Int32[] selectedRowHandles = gvInventory.GetSelectedRows();
                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                            rows.Add(gvInventory.GetDataRow(selectedRowHandle));
                    }
                    try
                    {
                        gvInventory.BeginUpdate();
                        _dtBarcode.BeginInit();
                        string barcode;

                        for (int i = 0; i < rows.Count; i++)
                        {
                            DataRow row = rows[i] as DataRow;
                            barcode = ConvertUtil.ToString(row["BARCODE"]);
                            _listBarcode.Remove(barcode);
                            row.Delete();
                        }
                    }
                    finally
                    {
                        _dtBarcode.BeginInit();
                        gvInventory.EndUpdate();
                    }

                    Dangol.Message("처리되었습니다.");
                }
            }
        }

        private void sbAddWarehousing_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(teWarehousing.Text))
            {
                Dangol.Message("입고번호가 입력되지 않았습니다.");
                return;
            }


            if (Dangol.MessageYN("입력하신 입고번호의 제품을 추가하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jData = new JObject();

                jData.Add("WAREHOUSING", teWarehousing.Text);

                if (DBInventory.getProductBarcodeList(jData, ref jResult))
                {
                    if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                    {
                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                        _dtBarcode.BeginInit();
                        gvInventory.BeginDataUpdate();

                        int index = 1;
                        string barcode;
                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            barcode = ConvertUtil.ToString(obj["BARCODE"]);

                            if (!string.IsNullOrWhiteSpace(barcode))
                                if (!_listBarcode.Contains(barcode))
                                {
                                    _listBarcode.Add(barcode);

                                    DataRow dr = _dtBarcode.NewRow();
                                    dr["BARCODE"] = barcode;
                                    dr["EXIST"] = true;
                                    _dtBarcode.Rows.InsertAt(dr, 0);
                                }
                        }
                        _dtBarcode.EndInit();
                        gvInventory.EndDataUpdate();

                        Dangol.Message("추가되었습니다.");
                    }
                    else
                    {
                        Dangol.Message("제품이 없거나 잘못된 입고번호입니다.");

                    }
                }
            }
        }

        private void gvInventory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvInventory.RowCount > 0);

            if (isValidRow)
            {
                _currentBarcode = e.Row as DataRowView;
                _inventoryBarcode = ConvertUtil.ToString(_currentBarcode["BARCODE"]);
            }
            else
            {
                _currentBarcode = null;
                _inventoryBarcode = "";
            }
        }

        private void lcgInventory_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduct.FocusedRowHandle;
            int topRowIndex = gvProduct.TopRowIndex;
            gvProduct.FocusedRowHandle = -2147483646;
            gvProduct.FocusedRowHandle = rowhandle;

            try
            {
                gvProduct.BeginUpdate();
                foreach (DataRow row in _dtProduct.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvProduct.DataRowCount; i++)
                {
                    int rowHandle = gvProduct.GetVisibleRowHandle(i);
                    rows.Add(gvProduct.GetDataRow(rowHandle));
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
                gvProduct.EndUpdate();
            }
        }

        private void lcgInventory_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduct.FocusedRowHandle;
            int topRowIndex = gvProduct.TopRowIndex;
            gvProduct.FocusedRowHandle = -2147483646;
            gvProduct.FocusedRowHandle = rowhandle;

            gvProduct.BeginDataUpdate();

            foreach (DataRow row in _dtProduct.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvProduct.EndDataUpdate();
        }

        private void gvInventory_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (!ConvertUtil.ToBoolean(gvInventory.GetDataRow(e.RowHandle)["EXIST"]))
                {
                    e.Appearance.BackColor = Color.Lime;
                }
            }
        }

        private void lcgInventory_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduct.FocusedRowHandle;
            gvProduct.FocusedRowHandle = -2147483646;
            gvProduct.FocusedRowHandle = rowhandle;

            if (e.Button.Properties.Tag.Equals(1))
            {
                _produce = teProduce.Text.ToUpper();

                if (string.IsNullOrWhiteSpace(_produce))
                {
                    teProduce.Focus();
                    Dangol.Message("생산번호를 입력하세요");
                    return;

                }

                DataRow[] rowsCheck = _dtProduct.Select("CHECK = TRUE" );
                if (rowsCheck.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 제품을 생산하시겠습니까?") == DialogResult.Yes)
                {

                    DataRow[] rows = _dtProduct.Select("CHECK = TRUE AND RELEASE_ID = 0");

                    if (rows.Length < 1)
                    {
                        Dangol.Message("생산가능한 제품이 없습니다.");
                        return;
                    }

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<long> listInventoryId = new List<long>();

                    foreach (DataRow row in rows)
                    {
                        listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    }

                    jobj.Add("RELEASES", _produce);
                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBProductProduce.insertProduce(jobj, ref jResult))
                    {
                        gvProduct.BeginDataUpdate();
                        foreach (DataRow row in rows)
                        {
                            row["RELEASE_ID"] = ConvertUtil.ToInt64(jResult["PRODUCE_ID"]);
                            row["RELEASES"] = _produce;
                            row["PRODUCE_STATE"] = row["PRODUCE_STATE_ORIGINAL"];
                        }
                        gvProduct.EndDataUpdate();

                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message($"{jResult["MSG"]}");
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                DataRow[] rowsCheck = _dtProduct.Select("CHECK = TRUE");
                if (rowsCheck.Length < 1)
                {
                    Dangol.Message("선택된 제품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택한 제품을 생산 취소하시겠습니까?(생산대기제품만 가능)") == DialogResult.Yes)
                {

                    DataRow[] rows = _dtProduct.Select("CHECK = TRUE AND RELEASE_ID > 0 AND PRODUCE_STATE = 1");

                    if (rows.Length < 1)
                    {
                        Dangol.Message("생산취소 가능한 제품이 없습니다.");
                        return;
                    }

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<long> listInventoryId = new List<long>();

                    foreach (DataRow row in rows)
                    {
                        listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                    }

                    jobj.Add("LIST_INVENTORY_ID", string.Join(",", listInventoryId));

                    if (DBProductProduce.deleteWarehousingProduce(jobj, ref jResult))
                    {
                        gvProduct.BeginDataUpdate();
                        foreach (DataRow row in rows)
                        {
                            row["RELEASE_ID"] = 0;
                            row["RELEASES"] = "";
                            row["PRODUCE_STATE"] = 0;
                        }
                        gvProduct.EndDataUpdate();

                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message($"{jResult["MSG"]}");
                    }
                }
            }

        }

        private void gvProduct_ColumnFilterChanged(object sender, EventArgs e)
        {
            int rowhandle = gvProduct.FocusedRowHandle;
            int topRowIndex = gvProduct.TopRowIndex;
            gvProduct.FocusedRowHandle = -2147483646;
            gvProduct.FocusedRowHandle = rowhandle;

            gvProduct.BeginDataUpdate();

            foreach (DataRow row in _dtProduct.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvProduct.EndDataUpdate();
        }
    }
}