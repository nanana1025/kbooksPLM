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
    public partial class usrWarehousingSwap : DevExpress.XtraEditors.XtraForm
    {

        string _warehousingL = "WAREHOUSING";
        string _warehousingR = "WAREHOUSING";

        long _warehousingIdL = -1;
        long _warehousingIdR = -1;  

        DataTable _dtWarehousingL;
        DataTable _dtWarehousingR;
       
        BindingSource _bsL;
        BindingSource _bsR;

        DataRowView _currentViewL;
        DataRowView _currentViewR;

        public usrWarehousingSwap()
        {
            InitializeComponent();


            _dtWarehousingL = new DataTable();

            _dtWarehousingL.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtWarehousingL.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtWarehousingL.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtWarehousingL.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtWarehousingL.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtWarehousingL.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtWarehousingL.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));

            _dtWarehousingR = new DataTable();

            _dtWarehousingR.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtWarehousingR.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtWarehousingR.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtWarehousingR.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtWarehousingR.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtWarehousingR.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtWarehousingR.Columns.Add(new DataColumn("INVENTORY_STATE", typeof(string)));



            _bsL = new BindingSource();
            _bsR = new BindingSource();

            _bsL.DataSource = _dtWarehousingL;
            _bsR.DataSource = _dtWarehousingR;           
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
            DataTable dtComponentCd = Util.getCodeList("CD0101", "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd1, dtComponentCd, "KEY", "VALUE");

            DataTable dtInvnetoryState = Util.getCodeList("CD0302", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState, dtInvnetoryState, "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryState1, dtInvnetoryState, "KEY", "VALUE");
        }

        private void setIInitData()
        {
        }

       

        private void setGridControl()
        {
            gcWarehousingL.DataSource = null;
            gcWarehousingL.DataSource = _bsL;

            gcWarehousingR.DataSource = null;
            gcWarehousingR.DataSource = _bsR;
            
        }

        private void getWarehousingInventory(string warehouing, DataTable _dt, GridView gv, int place)
        {
            if (string.IsNullOrEmpty(warehouing))
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            JObject jResult = new JObject();

            gv.BeginDataUpdate();
            _dt.Clear();

            if (DBConnect.getWarehousingInventoryList(warehouing, ref jResult))
            {
                if (place == 1)
                    _warehousingIdL = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);
                else
                    _warehousingIdR = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        dr["NO"] = index++;
                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        dr["BARCODE"] = obj["BARCODE"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["COMPONENT"] = obj["COMPONENT"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["INVENTORY_STATE"] = obj["INVENTORY_STATE"];

                        _dt.Rows.Add(dr);
                    } 
                }

                gv.EndDataUpdate();

                return;
            }
            else
            {
                gv.EndDataUpdate();
                return;
            }
        }



        private void sbSearchL_Click(object sender, EventArgs e)
        {
            getWarehousingInventory(teWarehousingL.Text, _dtWarehousingL, gvWarehousingL, 1);
        }

        private void sbSearchR_Click(object sender, EventArgs e)
        {
            getWarehousingInventory(teWarehousingR.Text, _dtWarehousingR, gvWarehousingR, 2);
        }

        private void sbLeft_Click(object sender, EventArgs e)
        {
            int[] selectedRowHandles = gvWarehousingR.GetSelectedRows();

            if (selectedRowHandles.Count() < 0)
                Dangol.Message("선택된 부품이 없습니다.");
            else if (_warehousingIdL == -1)
            {
                Dangol.Message("이동할 입고정보가 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN("선택한 부품을 이동하시겠습니까?(메인보드 선택시 제품 전체가 이동합니다.)") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    ArrayList rows = new ArrayList();
                    List<long> listInventoryId = new List<long>();

                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                            rows.Add(gvWarehousingR.GetDataRow(selectedRowHandle));
                    }
                    try
                    {
                        //gvWarehousingR.BeginUpdate();
                        //gvWarehousingL.BeginUpdate();

                        for (int i = 0; i < rows.Count; i++)
                        {
                            DataRow row = rows[i] as DataRow;
                            // Change the field value.
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        }

                        if (DBConnect.swapInventory(_warehousingIdR, _warehousingIdL, listInventoryId, ref jResult))
                        {
                            //int index = _dtWarehousingL.Rows.Count;
                            //for (int i = 0; i < rows.Count; i++)
                            //{
                            //    DataRow row = rows[i] as DataRow;

                            //    DataRow dr = _dtWarehousingL.NewRow();

                            //    dr["NO"] = index++;
                            //    dr["INVENTORY_ID"] = row["INVENTORY_ID"];
                            //    dr["BARCODE"] = row["BARCODE"];
                            //    dr["COMPONENT_CD"] = row["COMPONENT_CD"];
                            //    dr["COMPONENT"] = row["COMPONENT"];
                            //    dr["MODEL_NM"] = row["MODEL_NM"];
                            //    dr["INVENTORY_STATE"] = row["INVENTORY_STATE"];

                            //    _dtWarehousingL.Rows.Add(dr);

                            //    row.Delete();
                            //}

                            int focusedRowHandleL = gvWarehousingL.FocusedRowHandle;
                            int topRowIndexL = gvWarehousingL.TopRowIndex;

                            int focusedRowHandleR = gvWarehousingR.FocusedRowHandle;
                            int topRowIndexR = gvWarehousingR.TopRowIndex;

                            getWarehousingInventory(teWarehousingL.Text, _dtWarehousingL, gvWarehousingL, 1);
                            getWarehousingInventory(teWarehousingR.Text, _dtWarehousingR, gvWarehousingR, 2);

                            gvWarehousingL.FocusedRowHandle = focusedRowHandleL;
                            gvWarehousingL.TopRowIndex = topRowIndexL;

                            gvWarehousingR.FocusedRowHandle = focusedRowHandleR;
                            gvWarehousingR.TopRowIndex = topRowIndexR;

                        }
                    }
                    finally
                    {
                        //gvWarehousingR.EndUpdate();
                        //gvWarehousingL.EndUpdate();
                        //gvWarehousingL.MoveLast();

                        Dangol.Message("처리되었습니다.");
                    }
                }
            }
        }

        private void sbRight_Click(object sender, EventArgs e)
        {
            int[] selectedRowHandles = gvWarehousingL.GetSelectedRows();

            if (selectedRowHandles.Count() < 0)
                Dangol.Message("선택된 부품이 없습니다.");
            else if (_warehousingIdR == -1)
            {
                Dangol.Message("이동할 입고정보가 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN("선택한 부품을 이동하시겠습니까?(메인보드 선택시 제품 전체가 이동합니다.)") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    ArrayList rows = new ArrayList();
                    List<long> listInventoryId = new List<long>();

                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                            rows.Add(gvWarehousingL.GetDataRow(selectedRowHandle));
                    }
                    try
                    {
                        //gvWarehousingL.BeginUpdate();
                        //gvWarehousingR.BeginUpdate();

                        for (int i = 0; i < rows.Count; i++)
                        {
                            DataRow row = rows[i] as DataRow;
                            // Change the field value.
                            listInventoryId.Add(ConvertUtil.ToInt64(row["INVENTORY_ID"]));
                        }

                        if (DBConnect.swapInventory(_warehousingIdL, _warehousingIdR, listInventoryId, ref jResult))
                        {
                            //int index = _dtWarehousingR.Rows.Count;
                            //for (int i = 0; i < rows.Count; i++)
                            //{
                            //    DataRow row = rows[i] as DataRow;
                            //    DataRow dr = _dtWarehousingR.NewRow();

                            //    dr["NO"] = index++;
                            //    dr["INVENTORY_ID"] = row["INVENTORY_ID"];
                            //    dr["BARCODE"] = row["BARCODE"];
                            //    dr["COMPONENT_CD"] = row["COMPONENT_CD"];
                            //    dr["COMPONENT"] = row["COMPONENT"];
                            //    dr["MODEL_NM"] = row["MODEL_NM"];
                            //    dr["INVENTORY_STATE"] = row["INVENTORY_STATE"];

                            //    _dtWarehousingR.Rows.Add(dr);

                            //    row.Delete();
                            //}

                            int focusedRowHandleL = gvWarehousingL.FocusedRowHandle;
                            int topRowIndexL = gvWarehousingL.TopRowIndex;

                            int focusedRowHandleR = gvWarehousingR.FocusedRowHandle;
                            int topRowIndexR = gvWarehousingR.TopRowIndex;

                            getWarehousingInventory(teWarehousingL.Text, _dtWarehousingL, gvWarehousingL, 1);
                            getWarehousingInventory(teWarehousingR.Text, _dtWarehousingR, gvWarehousingR, 2);

                            gvWarehousingL.FocusedRowHandle = focusedRowHandleL;
                            gvWarehousingL.TopRowIndex = topRowIndexL;

                            gvWarehousingR.FocusedRowHandle = focusedRowHandleR;
                            gvWarehousingR.TopRowIndex = topRowIndexR;

                        }
                    }
                    finally
                    {
                        //gvWarehousingL.EndUpdate();
                        //gvWarehousingR.EndUpdate();
                        //gvWarehousingR.MoveLast();

                        Dangol.Message("처리되었습니다.");
                    }
                }
            }
        }

       
    }
}