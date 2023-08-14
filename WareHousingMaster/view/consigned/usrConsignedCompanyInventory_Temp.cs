using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;


namespace WareHousingMaster.view.consigned
{
    public partial class usrConsignedCompanyInventory_Temp : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtCompany;
        DataTable _dtCompanyInventory;

        BindingSource bsCompany;
        BindingSource bsCompanyInventory;

        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        DataRowView _drvCompany;
        DataRowView _drvCompanyInventory;
        long _companyId;
        long _componentId;
        string _componentCd;
        string _component;

        Dictionary<int, string> _dicConsignedType;

        bool _isInit;

        bool initializeEnter;


        public usrConsignedCompanyInventory_Temp()
        {
            InitializeComponent();

            _dtCompanyInventory = new DataTable();

            _dtCompanyInventory.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("VISIBLE_YN", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtCompanyInventory.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtCompanyInventory.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtCompanyInventory.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("COMPANY_VISIBLE", typeof(int)));

            _dtCompanyInventory.Columns.Add(new DataColumn("TOTAL_CNT", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("GOOD_CNT", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("RELEASE_CNT", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("FAULT_CNT", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("SPARE_CNT", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("LOCK_CNT", typeof(int)));

            _dtCompanyInventory.Columns.Add(new DataColumn("TOTAL_CNT_A", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("GOOD_CNT_A", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("RELEASE_CNT_A", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("FAULT_CNT_A", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("SPARE_CNT_A", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("LOCK_CNT_A", typeof(int)));

            _dtCompanyInventory.Columns.Add(new DataColumn("TOTAL_CNT_L", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("GOOD_CNT_L", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("RELEASE_CNT_L", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("FAULT_CNT_L", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("SPARE_CNT_L", typeof(int)));
            _dtCompanyInventory.Columns.Add(new DataColumn("LOCK_CNT_L", typeof(int)));

            _dtCompanyInventory.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dtCompanyInventory.Columns.Add(new DataColumn("PART_NM", typeof(string)));
            _dtCompanyInventory.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
            _dtCompanyInventory.Columns.Add(new DataColumn("COL1", typeof(string)));
            _dtCompanyInventory.Columns.Add(new DataColumn("COL2", typeof(string)));

            _dtCompanyInventory.Columns.Add(new DataColumn("RELEASE_PRICE", typeof(long)));
            _dtCompanyInventory.Columns.Add(new DataColumn("TOTAL_RELEASE_PRICE", typeof(long)));
            _dtCompanyInventory.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _dtCompanyInventory.Columns.Add(new DataColumn("COMPANY_COMPONENT_ID", typeof(long)));
            _dtCompanyInventory.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dtCompanyInventory.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));

            _dtCompanyInventory.Columns.Add(new DataColumn("CHECK", typeof(bool)));



            bsCompany = new BindingSource();
            bsCompanyInventory = new BindingSource();
            _drvCompany = null;
            _drvCompanyInventory = null;

            _dicConsignedType = new Dictionary<int, string>();

            _isInit = true;
            initializeEnter = true;

        }

        private void usrConsignedAdjustment_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

            if (ProjectInfo._userType.Equals("E"))
            {
                lcCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                si1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                gcCheck.Visible = false;
                gcConsignedType.Visible = false;
                gcCompanyVisible.Visible = false;
                gcPrice.Visible = false;
                gcTotalPrice.Visible = false;

                for (int i = 2; i < lcgInventory.CustomHeaderButtons.Count; i++)
                    lcgInventory.CustomHeaderButtons[i].Properties.Visible = false;

                lcChangeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcConsignedType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                empty1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCompanyVisible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcVisibleChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                empty2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                lcCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                si1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                gcCheck.Visible = true;
                gcConsignedType.Visible = true;
                gcCompanyVisible.Visible = true;
                gcPrice.Visible = true;
                gcTotalPrice.Visible = true;

                for (int i = 2; i < lcgInventory.CustomHeaderButtons.Count; i++)
                    lcgInventory.CustomHeaderButtons[i].Properties.Visible = true;

                lcChangeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcConsignedType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                empty1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcCompanyVisible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcVisibleChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                empty2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            _isInit = false;

        }


        private void setInfoBox()
        {
            if(ProjectInfo._userCompanyId == 2)
                _dtCompany = Util.getTable("TN_COMPANY_MST", "COMPANY_ID, COMPANY_NM, COMPANY_CD, COMPANY_NO, CHIEF_NM", "DEL_YN != 'Y' AND COMPANY_TYPE = 'C'","COMPANY_ID");
            else
                _dtCompany = Util.getTable("TN_COMPANY_MST", "COMPANY_ID, COMPANY_NM, COMPANY_CD, COMPANY_NO, CHIEF_NM", $"DEL_YN != 'Y' AND COMPANY_TYPE = 'C' AND COMPANY_ID = {ProjectInfo._userCompanyId }", "COMPANY_ID");

            _dtCompany.BeginInit();
            _dtCompany.Columns.Add(new DataColumn("NO", typeof(Int32)));
            int no = 1;
            foreach (DataRow dr in _dtCompany.Rows)
                dr["NO"] = no++;
            _dtCompany.EndInit();


            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for(int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr1 = dtComponentCd.NewRow();
                dr1["KEY"] = ProjectInfo._componetCd[i];
                dr1["VALUE"] = ProjectInfo._componetNm[i];
                dtComponentCd.Rows.Add(dr1);
            }

            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtConsignedType = new DataTable();

            dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            _dicConsignedType.Add(1, "리더스텍");
            _dicConsignedType.Add(2, "대행업체");
            _dicConsignedType.Add(3, "모든재고");

            Util.insertRowonTop(dtConsignedType, 3, "모든재고");
            Util.insertRowonTop(dtConsignedType, 2, "대행업체");
            Util.insertRowonTop(dtConsignedType, 1, "리더스텍");
            Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");
            Util.LookupEditHelper(leConsignedType, dtConsignedType, "KEY", "VALUE");

            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtCompanyVisible = new DataTable();

            dtCompanyVisible.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCompanyVisible.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtCompanyVisible, 0, "X");
            Util.insertRowonTop(dtCompanyVisible, 1, "O");
            Util.LookupEditHelper(rileCompanyVisible, dtCompanyVisible, "KEY", "VALUE");

            DataTable dtCompanyVisible1 = new DataTable();

            dtCompanyVisible1.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCompanyVisible1.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtCompanyVisible1, 0, "비노출");
            Util.insertRowonTop(dtCompanyVisible1, 1, "노출");
            Util.LookupEditHelper(leCompanyVisible, dtCompanyVisible1, "KEY", "VALUE");


            leConsignedType.EditValue = 1;
            leCompanyVisible.EditValue = 1;
        }

        private void setIInitData()
        {
            gcCompanyList.DataSource = null;
            bsCompany.DataSource = _dtCompany;
            gcCompanyList.DataSource = bsCompany;

            gcCompanyInventory.DataSource = null;
            bsCompanyInventory.DataSource = _dtCompanyInventory;
            gcCompanyInventory.DataSource = bsCompanyInventory;

            gcVisibleYn.Visible = false;

        }

        private void gvCompanyList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvCompanyList.RowCount > 0);

            if (isValidRow)
            {
                _drvCompany = e.Row as DataRowView;

                _companyId = ConvertUtil.ToInt64(_drvCompany["COMPANY_ID"]);
               

                if (!_isInit)
                    Dangol.ShowSplash();

                getCompanyInventoryList(_companyId);
                lcgInventory.CustomHeaderButtons[lcgInventory.CustomHeaderButtons.Count - 1].Properties.Checked = false;


                if (!_isInit)
                    Dangol.CloseSplash();
            }
            else
            {
                _drvCompany = null;
            }
        }

        private void gvCompanyInventory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvCompanyInventory.RowCount > 0);

            if (isValidRow)
            {
                _drvCompanyInventory = e.Row as DataRowView;

                _componentId = ConvertUtil.ToInt64(_drvCompanyInventory["COMPONENT_ID"]);
                _componentCd = ConvertUtil.ToString(_drvCompanyInventory["COMPONENT_CD"]);
            }
            else
            {
                _componentId = -1;
                _componentCd = "";
                _drvCompanyInventory = null;
            }
        }

        private void gvCompanyInventory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvCompanyInventory.RowCount > 0);

            if (isValidRow)
            {
                _drvCompanyInventory = gvCompanyInventory.GetRow(e.FocusedRowHandle) as DataRowView;
                _componentId = ConvertUtil.ToInt64(_drvCompanyInventory["COMPONENT_ID"]);
                _componentCd = ConvertUtil.ToString(_drvCompanyInventory["COMPONENT_CD"]);
                _component = ConvertUtil.ToString(_drvCompanyInventory["COMPONENT"]);
            }
            else
            {
                _componentId = -1;
                _componentCd = "";
                _component = "";
                _drvCompanyInventory = null;
            }
        }

        private void inputRow(int index, JObject jData)
        {
            DataRow dr1 = _dtCompanyInventory.NewRow();
            dr1["NO"] = index;
            dr1["VISIBLE_YN"] = jData["VISIBLE_YN"];
            dr1["COMPONENT"] = jData["COMPONENT"];
            dr1["COMPONENT_CD"] = jData["COMPONENT_CD"];
            dr1["MODEL_NM"] = jData["MODEL_NM"];
            dr1["CONSIGNED_TYPE"] = jData["CONSIGNED_TYPE"];
            dr1["COMPANY_VISIBLE"] = jData["COMPANY_VISIBLE"];

            dr1["TOTAL_CNT_L"] = ConvertUtil.ToInt32(jData["GOOD_CNT_L"]) + ConvertUtil.ToInt32(jData["FAULT_CNT_L"]) + ConvertUtil.ToInt32(jData["SPARE_CNT_L"]);
            dr1["GOOD_CNT_L"] = jData["GOOD_CNT_L"];
            dr1["RELEASE_CNT_L"] = jData["RELEASE_CNT_L"];
            dr1["FAULT_CNT_L"] = jData["FAULT_CNT_L"];
            dr1["SPARE_CNT_L"] = jData["SPARE_CNT_L"];
            dr1["LOCK_CNT_L"] = jData["LOCK_CNT_L"];

            dr1["TOTAL_CNT_A"] = ConvertUtil.ToInt32(jData["GOOD_CNT"]) + ConvertUtil.ToInt32(jData["FAULT_CNT"]) + ConvertUtil.ToInt32(jData["SPARE_CNT"]);
            dr1["GOOD_CNT_A"] = jData["GOOD_CNT"];
            dr1["RELEASE_CNT_A"] = jData["RELEASE_CNT"];
            dr1["FAULT_CNT_A"] = jData["FAULT_CNT"];
            dr1["SPARE_CNT_A"] = jData["SPARE_CNT"];
            dr1["LOCK_CNT_A"] = jData["LOCK_CNT"];

            int consignedType = ConvertUtil.ToInt32(jData["CONSIGNED_TYPE"]);

            if (consignedType == 1)
            {
                dr1["TOTAL_CNT"] = dr1["TOTAL_CNT_L"];
                dr1["GOOD_CNT"] = dr1["GOOD_CNT_L"];
                dr1["RELEASE_CNT"] = dr1["RELEASE_CNT_L"];
                dr1["FAULT_CNT"] = dr1["FAULT_CNT_L"];
                dr1["SPARE_CNT"] = dr1["SPARE_CNT_L"];
                dr1["LOCK_CNT"] = dr1["LOCK_CNT_L"];
            }
            else if (consignedType == 2)
            {
                dr1["TOTAL_CNT"] = dr1["TOTAL_CNT_A"];
                dr1["GOOD_CNT"] = dr1["GOOD_CNT_A"];
                dr1["RELEASE_CNT"] = dr1["RELEASE_CNT_A"];
                dr1["FAULT_CNT"] = dr1["FAULT_CNT_A"];
                dr1["SPARE_CNT"] = dr1["SPARE_CNT_A"];
                dr1["LOCK_CNT"] = dr1["LOCK_CNT_A"];
            }
            else if (consignedType == 3)
            {
                dr1["TOTAL_CNT"] = ConvertUtil.ToInt32(dr1["TOTAL_CNT_A"]) + ConvertUtil.ToInt32(dr1["TOTAL_CNT_L"]);
                dr1["GOOD_CNT"] = ConvertUtil.ToInt32(dr1["GOOD_CNT_A"]) + ConvertUtil.ToInt32(dr1["GOOD_CNT_L"]);
                dr1["RELEASE_CNT"] = ConvertUtil.ToInt32(dr1["RELEASE_CNT_A"]) + ConvertUtil.ToInt32(dr1["RELEASE_CNT_L"]);
                dr1["FAULT_CNT"] = ConvertUtil.ToInt32(dr1["FAULT_CNT_A"]) + ConvertUtil.ToInt32(dr1["FAULT_CNT_L"]);
                dr1["SPARE_CNT"] = ConvertUtil.ToInt32(dr1["SPARE_CNT_A"]) + ConvertUtil.ToInt32(dr1["SPARE_CNT_L"]);
                dr1["LOCK_CNT"] = ConvertUtil.ToInt32(dr1["LOCK_CNT_A"]) + ConvertUtil.ToInt32(dr1["LOCK_CNT_L"]);
            }

            dr1["MANUFACTURE_NM"] = jData["MANUFACTURE_NM"];
            dr1["PART_NM"] = jData["PART_NM"];
            dr1["SPEC_NM"] = jData["SPEC_NM"];
            dr1["COL1"] = jData["COL1"];
            dr1["COL2"] = jData["COL2"];

            dr1["RELEASE_PRICE"] = ConvertUtil.ToInt64(jData["RELEASE_PRICE"]);
            dr1["TOTAL_RELEASE_PRICE"] = ConvertUtil.ToInt64(jData["RELEASE_PRICE"]) * ConvertUtil.ToInt64(dr1["TOTAL_CNT"]);
            dr1["TOTAL_PRICE"] = ConvertUtil.ToInt64(jData["TOTAL_PRICE"]);

            dr1["COMPANY_COMPONENT_ID"] = jData["COMPANY_COMPONENT_ID"];
            dr1["COMPANY_ID"] = jData["COMPANY_ID"];
            dr1["COMPONENT_ID"] = jData["COMPONENT_ID"];
            dr1["CHECK"] = false;

            _dtCompanyInventory.Rows.Add(dr1);
        }


        private bool getCompanyInventoryList(long companyId)
        {
            JObject jResult = new JObject();

            _dtCompanyInventory.Clear();

            if (DBConnect.getCompanyInventoryList(companyId, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                int index = 1;
                int companyVisible;

                if (ProjectInfo._userCompanyId == 2)
                {
                    foreach (JObject jData in jArray.Children<JObject>())
                        inputRow(index++, jData);
                }
                else
                {
                    foreach (JObject jData in jArray.Children<JObject>())
                    { 
                        companyVisible = ConvertUtil.ToInt32(jData["COMPANY_VISIBLE"]);
                        if (companyVisible == 1)
                            inputRow(index++, jData);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void lcgInventory_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                bsCompanyInventory.Filter = "VISIBLE_YN < 2";
                gcVisibleYn.Visible = true;
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvCompanyInventory.FocusedRowHandle;
                int topRowIndex = gvCompanyInventory.TopRowIndex;
                gvCompanyInventory.FocusedRowHandle = -1;
                gvCompanyInventory.FocusedRowHandle = rowhandle;

                try
                {
                    gvCompanyInventory.BeginUpdate();
                    foreach (DataRow row in _dtCompanyInventory.Rows)
                    {
                        row.BeginEdit();
                        row["CHECK"] = false;
                        row.EndEdit();
                    }

                    ArrayList rows = new ArrayList();
                    for (int i = 0; i < gvCompanyInventory.DataRowCount; i++)
                    {
                        int rowHandle = gvCompanyInventory.GetVisibleRowHandle(i);
                        rows.Add(gvCompanyInventory.GetDataRow(rowHandle));
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
                    gvCompanyInventory.EndUpdate();
                }
            }
        }

        private void lcgInventory_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                bsCompanyInventory.Filter = "VISIBLE_YN = 1";
                gcVisibleYn.Visible = false;
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvCompanyInventory.FocusedRowHandle;
                int topRowIndex = gvCompanyInventory.TopRowIndex;
                gvCompanyInventory.FocusedRowHandle = -1;
                gvCompanyInventory.FocusedRowHandle = rowhandle;

                gvCompanyInventory.BeginDataUpdate();

                foreach (DataRow row in _dtCompanyInventory.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
                gvCompanyInventory.EndDataUpdate();
            }
        }

        private void lcgInventory_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if(_drvCompany == null)
            {
                Dangol.Message("선택한 업체가 없습니다.");
                return;
            }

            int[] selectedIdxes = gvCompanyInventory.GetSelectedRows();

            if(selectedIdxes.Length < 1)
            {
                Dangol.Message("선택한 부품이 없습니다.");
                return;
            }


            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("선택하신 부품을 보임처리하시겠습니까?") == DialogResult.Yes)
                {
                    List<object> listCompanyComponentId = new List<object>();
                    for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                    {
                        DataRowView row = gvCompanyInventory.GetRow(selectedIdxes[i]) as DataRowView;
                        listCompanyComponentId.Add(row["COMPANY_COMPONENT_ID"]);
                    }

                    JObject jResult = new JObject();

                    if (DBConnect.changeCompanyComponentVisible(_companyId, listCompanyComponentId, 1, ref jResult))
                    {
                        gvCompanyInventory.BeginDataUpdate();
                        for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                        {
                            DataRowView row = gvCompanyInventory.GetRow(selectedIdxes[i]) as DataRowView;
                            row["VISIBLE_YN"] = 1;
                        }
                        gvCompanyInventory.EndDataUpdate();
                    }
                }

                int rowhandle = gvCompanyInventory.FocusedRowHandle;
                gvCompanyInventory.FocusedRowHandle = -1;
                gvCompanyInventory.FocusedRowHandle = rowhandle;

            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (Dangol.MessageYN("선택하신 부품을 숨김처리하시겠습니까?") == DialogResult.Yes)
                {
                    List<object> listCompanyComponentId = new List<object>();
                    for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                    {
                        DataRowView row = gvCompanyInventory.GetRow(selectedIdxes[i]) as DataRowView;
                        listCompanyComponentId.Add(row["COMPANY_COMPONENT_ID"]);
                    }

                    JObject jResult = new JObject();

                    if (DBConnect.changeCompanyComponentVisible(_companyId, listCompanyComponentId, 0, ref jResult))
                    {
                        gvCompanyInventory.BeginDataUpdate();
                        for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                        {
                            DataRowView row = gvCompanyInventory.GetRow(selectedIdxes[i]) as DataRowView;
                            row["VISIBLE_YN"] = 0;
                        }
                        gvCompanyInventory.EndDataUpdate();
                    }
                }

                int rowhandle = gvCompanyInventory.FocusedRowHandle;
                gvCompanyInventory.FocusedRowHandle = -1;
                gvCompanyInventory.FocusedRowHandle = rowhandle;

            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                if (_drvCompanyInventory == null || _componentId == -1)
                {
                    Dangol.Message("선택한 부품이 없습니다.");
                }
                else
                {
                    using (DlgCompanyPartByWarehousing dlgCompanyPartByWarehousing = new DlgCompanyPartByWarehousing(_companyId, _componentId, _componentCd, _drvCompanyInventory["MODEL_NM"], _drvCompanyInventory["CONSIGNED_TYPE"]))
                    {
                        dlgCompanyPartByWarehousing.ShowDialog(this);

                        if (dlgCompanyPartByWarehousing._isUpdate)
                        {
                            Dangol.ShowSplash();

                            string component = _component;
                            gvCompanyInventory.FocusedRowObjectChanged -= gvCompanyInventory_FocusedRowObjectChanged;
                            getCompanyInventoryList(_companyId);
                            gvCompanyInventory.FocusedRowObjectChanged += gvCompanyInventory_FocusedRowObjectChanged;
                            int rowHandle = gvCompanyInventory.LocateByValue("COMPONENT", component);

                            if (rowHandle > -1)
                            {
                                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                                {
                                    gvCompanyInventory.FocusedRowHandle = -2147483646;
                                    gvCompanyInventory.FocusedRowHandle = rowHandle;
                                }
                            }
                            else
                            {
                                if (_dtCompanyInventory.Rows.Count > 0)
                                {
                                    gvCompanyInventory.FocusedRowHandle = -2147483646;
                                    gvCompanyInventory.MoveFirst();
                                }
                            }

                            lcgInventory.CustomHeaderButtons[lcgInventory.CustomHeaderButtons.Count - 1].Properties.Checked = false;

                            Dangol.CloseSplash();
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                long companyId = _companyId;

                if (ProjectInfo._userType.Equals("E"))
                {
                    companyId = ProjectInfo._userCompanyId;
                }

                using (DlgCompanyPartStatistice dlgCompanyPartStatistice = new DlgCompanyPartStatistice(companyId))
                {
                    if (dlgCompanyPartStatistice.ShowDialog(this) == DialogResult.OK)
                    {

                    }
                }
            }


        }

        private void gvCompanyInventory_DoubleClick(object sender, EventArgs e)
        {
            if (_drvCompanyInventory == null || _componentId == -1)
            {
                Dangol.Message("선택한 부품이 없습니다.");
            }
            else
            {
                using (DlgCompanyPartByWarehousing dlgCompanyPartByWarehousing = new DlgCompanyPartByWarehousing(_companyId, _componentId, _componentCd, _drvCompanyInventory["MODEL_NM"], _drvCompanyInventory["CONSIGNED_TYPE"]))
                {
                    dlgCompanyPartByWarehousing.ShowDialog(this);

                    if (dlgCompanyPartByWarehousing._isUpdate)
                    {
                        Dangol.ShowSplash();

                        string component = _component;
                        int topRowIndex = gvCompanyInventory.TopRowIndex;
                        gvCompanyInventory.FocusedRowObjectChanged -= gvCompanyInventory_FocusedRowObjectChanged;
                        getCompanyInventoryList(_companyId);
                        gvCompanyInventory.FocusedRowObjectChanged += gvCompanyInventory_FocusedRowObjectChanged;
                        int rowHandle = gvCompanyInventory.LocateByValue("COMPONENT", component);

                        if (rowHandle > -1)
                        {
                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            {
                                gvCompanyInventory.FocusedRowHandle = -2147483646;
                                gvCompanyInventory.FocusedRowHandle = rowHandle;
                                gvCompanyInventory.TopRowIndex = topRowIndex;
                            }
                        }
                        else
                        {
                            if (_dtCompanyInventory.Rows.Count > 0)
                            {
                                gvCompanyInventory.FocusedRowHandle = -2147483646;
                                gvCompanyInventory.MoveFirst();
                            }
                        }

                        lcgInventory.CustomHeaderButtons[lcgInventory.CustomHeaderButtons.Count - 1].Properties.Checked = false;

                        Dangol.CloseSplash();
                    }
                }
            }
        }

        private void rileCompanyVisible_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_drvCompanyInventory == null)
            {
                //Dangol.Message("부품이 선택되지 않았습니다.");
                return;
            }
            else
            {
                int value = ConvertUtil.ToInt32(e.NewValue);
                string text = value == 1 ? "노출" : "비노출";

                if (Dangol.MessageYN($"현재 부품 상태를 '{text}로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    jobj.Add("COMPANY_COMPONENT_ID", ConvertUtil.ToInt64(_drvCompanyInventory["COMPANY_COMPONENT_ID"]));
                    jobj.Add("COMPANY_VISIBLE", value);
                    jobj.Add("BULK_YN", 0);

                    if (DBConsigned.updateCompanyComponentInfo(jobj, ref jResult))
                    {
                        gvCompanyInventory.BeginDataUpdate();
                        _drvCompanyInventory.BeginEdit();
                        _drvCompanyInventory["COMPANY_VISIBLE"] = value;
                        _drvCompanyInventory.EndEdit();
                        gvCompanyInventory.EndDataUpdate();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        e.Cancel = true;
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
                else
                {
                    e.Cancel = true;
                }

            }
        }
        private void sbVisibleChange_Click(object sender, EventArgs e)
        {
            int rowhandle = gvCompanyInventory.FocusedRowHandle;
            gvCompanyInventory.FocusedRowHandle = -1;
            gvCompanyInventory.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtCompanyInventory.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            int value = ConvertUtil.ToInt32(leCompanyVisible.EditValue);
            string text = value == 1 ? "노출" : "비노출";

            if (Dangol.MessageYN($"선택하신 부품을 '{text}'으로 변경하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                List<long> listId = new List<long>();

                foreach (DataRow row in rows)
                {
                    long representativeId = ConvertUtil.ToInt64(row["COMPANY_COMPONENT_ID"]);
                    if (!listId.Contains(representativeId))
                    {
                        listId.Add(representativeId);
                    }

                }

                jobj.Add("LIST_COMPANY_COMPONENT_ID", string.Join(",", listId));
                jobj.Add("COMPANY_VISIBLE", value);
                jobj.Add("BULK_YN", 1);

                if (DBConsigned.updateCompanyComponentInfo(jobj, ref jResult))
                {
                    gvCompanyInventory.BeginDataUpdate();

                    foreach (DataRow row in rows)
                    {
                        row["COMPANY_VISIBLE"] = value;
                    }

                    gvCompanyInventory.EndDataUpdate();
                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
        }

        private void rileConsignedType_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_drvCompanyInventory == null)
            {
                //Dangol.Message("부품이 선택되지 않았습니다.");
                return;
            }
            else
            {
                int value = ConvertUtil.ToInt32(e.NewValue);

                if (Dangol.MessageYN($"현재 부품 상태를 '{_dicConsignedType[value]}'로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    jobj.Add("COMPANY_COMPONENT_ID", ConvertUtil.ToInt64(_drvCompanyInventory["COMPANY_COMPONENT_ID"]));
                    jobj.Add("CONSIGNED_TYPE", value);
                    jobj.Add("BULK_YN", 0);

                    if (DBConsigned.updateCompanyComponentInfo(jobj, ref jResult))
                    {
                        setComponentInfo(false, value);
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        e.Cancel = true;
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        private void sbTypeChange_Click(object sender, EventArgs e)
        {
            int rowhandle = gvCompanyInventory.FocusedRowHandle;
            gvCompanyInventory.FocusedRowHandle = -1;
            gvCompanyInventory.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtCompanyInventory.Select("CHECK = TRUE");
            if (rows.Length < 1)
            {
                Dangol.Message("선택된 부품이 없습니다.");
                return;
            }

            int value = ConvertUtil.ToInt32(leConsignedType.EditValue);

            if (Dangol.MessageYN($"선택하신 부품을 '{_dicConsignedType[value]}'로 변경하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jobj = new JObject();

                List<long> listId = new List<long>();

                foreach (DataRow row in rows)
                {
                    long representativeId = ConvertUtil.ToInt64(row["COMPANY_COMPONENT_ID"]);
                    if (!listId.Contains(representativeId))
                    {
                        listId.Add(representativeId);
                    }
                    
                }

                jobj.Add("LIST_COMPANY_COMPONENT_ID", string.Join(",", listId));
                jobj.Add("CONSIGNED_TYPE", value);
                jobj.Add("BULK_YN", 1);

                if (DBConsigned.updateCompanyComponentInfo(jobj, ref jResult))
                {
                    setComponentInfo(true, value);
                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                    return;
                }
            }
        }
        private void setComponentInfo(bool isBulk, int type)
        {
            Dangol.ShowSplash();
            gvCompanyInventory.BeginDataUpdate();
            if (isBulk)
            {
                DataRow[] rows = _dtCompanyInventory.Select("CHECK = TRUE");

                foreach (DataRow dr1 in rows)
                {
                    dr1["CONSIGNED_TYPE"] = type;
                    if (type == 1)
                    {
                        dr1["TOTAL_CNT"] = dr1["TOTAL_CNT_L"];
                        dr1["GOOD_CNT"] = dr1["GOOD_CNT_L"];
                        dr1["RELEASE_CNT"] = dr1["RELEASE_CNT_L"];
                        dr1["FAULT_CNT"] = dr1["FAULT_CNT_L"];
                        dr1["SPARE_CNT"] = dr1["SPARE_CNT_L"];
                        dr1["LOCK_CNT"] = dr1["LOCK_CNT_L"];
                    }
                    else if (type == 2)
                    {
                        dr1["TOTAL_CNT"] = dr1["TOTAL_CNT_A"];
                        dr1["GOOD_CNT"] = dr1["GOOD_CNT_A"];
                        dr1["RELEASE_CNT"] = dr1["RELEASE_CNT_A"];
                        dr1["FAULT_CNT"] = dr1["FAULT_CNT_A"];
                        dr1["SPARE_CNT"] = dr1["SPARE_CNT_A"];
                        dr1["LOCK_CNT"] = dr1["LOCK_CNT_A"];
                    }
                    else if (type == 3)
                    {
                        dr1["TOTAL_CNT"] = ConvertUtil.ToInt32(dr1["TOTAL_CNT_A"]) + ConvertUtil.ToInt32(dr1["TOTAL_CNT_L"]);
                        dr1["GOOD_CNT"] = ConvertUtil.ToInt32(dr1["GOOD_CNT_A"]) + ConvertUtil.ToInt32(dr1["GOOD_CNT_L"]);
                        dr1["RELEASE_CNT"] = ConvertUtil.ToInt32(dr1["RELEASE_CNT_A"]) + ConvertUtil.ToInt32(dr1["RELEASE_CNT_L"]);
                        dr1["FAULT_CNT"] = ConvertUtil.ToInt32(dr1["FAULT_CNT_A"]) + ConvertUtil.ToInt32(dr1["FAULT_CNT_L"]);
                        dr1["SPARE_CNT"] = ConvertUtil.ToInt32(dr1["SPARE_CNT_A"]) + ConvertUtil.ToInt32(dr1["SPARE_CNT_L"]);
                        dr1["LOCK_CNT"] = ConvertUtil.ToInt32(dr1["LOCK_CNT_A"]) + ConvertUtil.ToInt32(dr1["LOCK_CNT_L"]);
                    }
                }
               
            }
            else
            {
                _drvCompanyInventory.BeginEdit();

                _drvCompanyInventory["CONSIGNED_TYPE"] = type;

                if (type == 1)
                {
                    _drvCompanyInventory["TOTAL_CNT"] = _drvCompanyInventory["TOTAL_CNT_L"];
                    _drvCompanyInventory["GOOD_CNT"] = _drvCompanyInventory["GOOD_CNT_L"];
                    _drvCompanyInventory["RELEASE_CNT"] = _drvCompanyInventory["RELEASE_CNT_L"];
                    _drvCompanyInventory["FAULT_CNT"] = _drvCompanyInventory["FAULT_CNT_L"];
                    _drvCompanyInventory["SPARE_CNT"] = _drvCompanyInventory["SPARE_CNT_L"];
                    _drvCompanyInventory["LOCK_CNT"] = _drvCompanyInventory["LOCK_CNT_L"];
                }
                else if (type == 2)
                {
                    _drvCompanyInventory["TOTAL_CNT"] = _drvCompanyInventory["TOTAL_CNT_A"];
                    _drvCompanyInventory["GOOD_CNT"] = _drvCompanyInventory["GOOD_CNT_A"];
                    _drvCompanyInventory["RELEASE_CNT"] = _drvCompanyInventory["RELEASE_CNT_A"];
                    _drvCompanyInventory["FAULT_CNT"] = _drvCompanyInventory["FAULT_CNT_A"];
                    _drvCompanyInventory["SPARE_CNT"] = _drvCompanyInventory["SPARE_CNT_A"];
                    _drvCompanyInventory["LOCK_CNT"] = _drvCompanyInventory["LOCK_CNT_A"];
                }
                else if (type == 3)
                {
                    _drvCompanyInventory["TOTAL_CNT"] = ConvertUtil.ToInt32(_drvCompanyInventory["TOTAL_CNT_A"]) + ConvertUtil.ToInt32(_drvCompanyInventory["TOTAL_CNT_L"]);
                    _drvCompanyInventory["GOOD_CNT"] = ConvertUtil.ToInt32(_drvCompanyInventory["GOOD_CNT_A"]) + ConvertUtil.ToInt32(_drvCompanyInventory["GOOD_CNT_L"]);
                    _drvCompanyInventory["RELEASE_CNT"] = ConvertUtil.ToInt32(_drvCompanyInventory["RELEASE_CNT_A"]) + ConvertUtil.ToInt32(_drvCompanyInventory["RELEASE_CNT_L"]);
                    _drvCompanyInventory["FAULT_CNT"] = ConvertUtil.ToInt32(_drvCompanyInventory["FAULT_CNT_A"]) + ConvertUtil.ToInt32(_drvCompanyInventory["FAULT_CNT_L"]);
                    _drvCompanyInventory["SPARE_CNT"] = ConvertUtil.ToInt32(_drvCompanyInventory["SPARE_CNT_A"]) + ConvertUtil.ToInt32(_drvCompanyInventory["SPARE_CNT_L"]);
                    _drvCompanyInventory["LOCK_CNT"] = ConvertUtil.ToInt32(_drvCompanyInventory["LOCK_CNT_A"]) + ConvertUtil.ToInt32(_drvCompanyInventory["LOCK_CNT_L"]);
                }

                _drvCompanyInventory.EndEdit();
            }

            gvCompanyInventory.EndDataUpdate();
            Dangol.CloseSplash();
        }

        private void gvCompanyInventory_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0 && e.Column.FieldName == "CONSIGNED_TYPE")
            {
                int value = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["CONSIGNED_TYPE"]));

                if(value == 3)
                    e.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
                else if (value == 1)
                    e.Appearance.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (e.RowHandle >= 0 && e.Column.FieldName == "COMPANY_VISIBLE")
            {
                int value = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["COMPANY_VISIBLE"]));

                if (value == 0)
                    e.Appearance.BackColor = System.Drawing.Color.LightCoral;
            }
        }

        private void usrConsignedCompanyInventory_Enter(object sender, EventArgs e)
        {
            if (!initializeEnter)
            {
                Dangol.ShowSplash();
                getCompanyInventoryList(_companyId);
                lcgInventory.CustomHeaderButtons[lcgInventory.CustomHeaderButtons.Count - 1].Properties.Checked = false;
                Dangol.CloseSplash();
            }

            initializeEnter = false;
        }
    }


    
}