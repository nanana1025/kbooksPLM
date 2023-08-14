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

namespace WareHousingMaster.view.warehousing
{
    public partial class usrProductSwap : DevExpress.XtraEditors.XtraForm
    {

        string _warehousing = "WAREHOUSING";


        DataTable _dtProduct;
        long _warehousingId;
       
        BindingSource _bs;

        DataRowView _currentView;

        List<int> _listBasis;

        int _currentPage;
        int _totalPage;
        int _currentBasis;
        int _totalCnt;

        public usrProductSwap()
        {
            InitializeComponent();


            _dtProduct = new DataTable();

            _dtProduct.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtProduct.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtProduct.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("REGIST_DT", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("CPU_MODEL_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("SB_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("FAMILY_NM", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("SERIAL_NO", typeof(string)));
            _dtProduct.Columns.Add(new DataColumn("SYSTEM_SN", typeof(string)));

            _bs = new BindingSource();
            _bs.DataSource = _dtProduct;

            _listBasis = new List<int>(new[] {20, 50, 100, 200, 500 });

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
            
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            searchProduct();
        }
        private void searchProduct()
        {
            string manufactureNm = teManufactureNm.Text;
            string modelNm = teModelNm.Text;
            string MBDModelNm = teMBDModelNm.Text;
            string productNm = teProductNm.Text;
            string sbNm = teSbNm.Text;
            string familyNm = teFamilyNm.Text;
            string serialNo = teSerialNo.Text;

            if (string.IsNullOrWhiteSpace(manufactureNm) &&
                string.IsNullOrWhiteSpace(modelNm) &&
                string.IsNullOrWhiteSpace(MBDModelNm) &&
                string.IsNullOrWhiteSpace(productNm) &&
                string.IsNullOrWhiteSpace(sbNm) &&
                string.IsNullOrWhiteSpace(familyNm) &&
                string.IsNullOrWhiteSpace(serialNo)
                )
            {
                Dangol.Message("하나 이상의 조건을 입력해 주세요.");
                return;
            }

            JObject jResult = new JObject();
            JObject jData = new JObject();
            gvProduct.BeginDataUpdate();
            _dtProduct.Clear();

            jData.Add("MANUFACTURE_NM", manufactureNm);
            jData.Add("MODEL_NM", modelNm);
            jData.Add("MBD_MODEL_NM", MBDModelNm);
            jData.Add("PRODUCT_NAME", productNm);
            jData.Add("SB_NM", sbNm);
            jData.Add("FAMILY_NM", familyNm);
            jData.Add("SERIAL_NO", serialNo);

            jData.Add("START", (_currentPage-1) * _currentBasis);
            jData.Add("END", _currentBasis);

            jData.Add("CURRENT_PAGE", _currentPage);
            jData.Add("CURRENT_BASIS", _currentBasis);

            if (DBConnect.getProductList(jData, ref jResult))
            {

                _totalCnt = ConvertUtil.ToInt32(jResult["TOTAL_CNT"]);
                double data = (double)_totalCnt / (double)_currentBasis;
                _totalPage = ConvertUtil.ToInt32(Math.Ceiling(data));
                string lbCount = $"{_currentPage}/{_totalPage}";

                lcPage.Text = lbCount;
                lcTotal.Text = $"TOTAL {_totalCnt}개";
                

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtProduct.NewRow();

                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];
                        dr["WAREHOUSING"] = obj["WAREHOUSING"];
                        dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dr["REGIST_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                        dr["CPU_MODEL_NM"] = obj["CPU_MODEL_NM"];
                        dr["MANUFACTURE_NM"] = obj["MANUFACTURE_NM"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];

                        dr["MBD_MODEL_NM"] = obj["MBD_MODEL_NM"];
                        dr["PRODUCT_NAME"] = obj["PRODUCT_NAME"];
                        dr["SB_NM"] = obj["SB_NM"];
                        dr["FAMILY_NM"] = obj["FAMILY_NM"];
                        dr["SERIAL_NO"] = obj["SERIAL_NO"];
                        dr["SYSTEM_SN"] = obj["SYSTEM_SN"];


                        _dtProduct.Rows.Add(dr);
                    }
                }

                gvProduct.EndDataUpdate();

                return;
            }
            else
            {
                lcPage.Text = "-/-";
                lcTotal.Text = "TOTAL -개";
                gvProduct.EndDataUpdate();
                return;
            }
        }



        private void sbMove_Click(object sender, EventArgs e)
        {

            _warehousing = teWarehousing.Text.ToUpper();

            if (string.IsNullOrWhiteSpace(_warehousing))
            {
                teWarehousing.Focus();
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
    }
}