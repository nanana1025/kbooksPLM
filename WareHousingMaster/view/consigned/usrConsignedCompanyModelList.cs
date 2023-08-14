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
    public partial class usrConsignedCompanyModelList : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtCompany;
        DataTable _dtCompanyModel;
        DataTable _dtModelInventory;

        BindingSource bsCompany;
        BindingSource bsCompanyModelI;
        BindingSource bsModelInventory;

        //public static Dictionary<long, Dictionary<long, string>> _dicWarehouMovementList = null;

        DataRowView _drvCompany;
        DataRowView _drvCompanyModel;
        DataRowView _drvModelInventory;
        long _companyId;
        long _modelListId;
        long _componentId;
        string _componentCd;

        Dictionary<int, string> _dicConsignedType;

        bool _isInit;


        public usrConsignedCompanyModelList()
        {
            InitializeComponent();


            _dtCompanyModel = new DataTable();

            _dtCompanyModel.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("MODEL_LIST_ID", typeof(long)));
            _dtCompanyModel.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtCompanyModel.Columns.Add(new DataColumn("KEYWORD", typeof(string)));
            _dtCompanyModel.Columns.Add(new DataColumn("PC_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("RECEIPT_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("CPU_ASSIGN_YN", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("MEM_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("MEM_SLOT", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("SERIAL_NO_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("STATE", typeof(int)));
            
            _dtCompanyModel.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));


            _dtModelInventory = new DataTable();

            _dtModelInventory.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtModelInventory.Columns.Add(new DataColumn("MODEL_PART_ID", typeof(long)));
            _dtModelInventory.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtModelInventory.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtModelInventory.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtModelInventory.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtModelInventory.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtModelInventory.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dtModelInventory.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtModelInventory.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtModelInventory.Columns.Add(new DataColumn("STATE", typeof(int)));




            bsCompany = new BindingSource();
            bsCompanyModelI = new BindingSource();
            bsModelInventory = new BindingSource();

            _drvCompany = null;
            _drvCompanyModel= null;
            _drvModelInventory = null;

            _dicConsignedType = new Dictionary<int, string>();

            _isInit = true;
        }

        private void usrConsignedAdjustment_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

            //if (ProjectInfo._userCompanyId == 2)
            //{
            //    lcCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    si1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    gcCheck.Visible = true;
            //    gcConsignedType.Visible = true;
            //    gcCompanyVisible.Visible = true;
            //    gcPrice.Visible = true;
            //    gcTotalPrice.Visible = true;

            //    for (int i = 2; i < lcgInventory.CustomHeaderButtons.Count; i++)
            //        lcgInventory.CustomHeaderButtons[i].Properties.Visible = true;

            //    lcChangeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    lcConsignedType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    empty1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //    lcCompanyVisible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    lcVisibleChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    empty2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            //else
            //{
            //    lcCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    si1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //    gcCheck.Visible = false;
            //    gcConsignedType.Visible = false;
            //    gcCompanyVisible.Visible = false;
            //    gcPrice.Visible = false;
            //    gcTotalPrice.Visible = false;

            //    for (int i = 2; i < lcgInventory.CustomHeaderButtons.Count; i++)
            //        lcgInventory.CustomHeaderButtons[i].Properties.Visible = false;

            //    lcChangeType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    lcConsignedType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    empty1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    lcCompanyVisible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    lcVisibleChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    empty2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}

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

            _dicConsignedType.Add(2, "생산대행");
            _dicConsignedType.Add(1, "자사재고");

            Util.insertRowonTop(dtConsignedType, 1, "생산대행");
            Util.insertRowonTop(dtConsignedType, 2, "자사재고");
            Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");
   

            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtCompanyVisible = new DataTable();

            dtCompanyVisible.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCompanyVisible.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtCompanyVisible, 0, "X");
            Util.insertRowonTop(dtCompanyVisible, 1, "O");
            Util.LookupEditHelper(rileCompanyVisible, dtCompanyVisible, "KEY", "VALUE");


            DataTable dtCpuAssignYn = new DataTable();

            dtCpuAssignYn.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCpuAssignYn.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtCpuAssignYn, 1, "X");
            Util.insertRowonTop(dtCpuAssignYn, 0, "O");

            Util.LookupEditHelper(rileCpuAssignYn, dtCpuAssignYn, "KEY", "VALUE");


            DataTable dtPcType  = Util.getCodeListInt("CD0903", "KEY", "VALUE");
            Util.LookupEditHelper(rilePcType, dtPcType, "KEY", "VALUE");

            DataTable dtReceiptType = Util.getCodeListInt("CD090904", "KEY", "VALUE");
            Util.LookupEditHelper(rileReceiptType, dtReceiptType, "KEY", "VALUE");

            DataTable dtMemType = Util.getCodeListInt("CD090901", "KEY", "VALUE");
            Util.LookupEditHelper(rileMemType, dtMemType, "KEY", "VALUE");

            DataTable dtMemSlot = Util.getCodeListInt("CD090902", "KEY", "VALUE");
            Util.LookupEditHelper(rileMemSlot, dtMemSlot, "KEY", "VALUE");

            DataTable dtSerialNoType = Util.getCodeListInt("CD090903", "KEY", "VALUE");
            Util.LookupEditHelper(rileSerialNoType, dtSerialNoType, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            gcCompanyList.DataSource = null;
            bsCompany.DataSource = _dtCompany;
            gcCompanyList.DataSource = bsCompany;

            gcModelList.DataSource = null;
            bsCompanyModelI.DataSource = _dtCompanyModel;
            gcModelList.DataSource = bsCompanyModelI;

            gcModelInventory.DataSource = null;
            bsModelInventory.DataSource = _dtModelInventory;
            gcModelInventory.DataSource = bsModelInventory;


        }

        private void gvCompanyList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvCompanyList.RowCount > 0);
            _dtCompanyModel.Clear();
            _dtModelInventory.Clear();
           
            if (isValidRow)
            {
                _drvCompany = e.Row as DataRowView;

                _companyId = ConvertUtil.ToInt64(_drvCompany["COMPANY_ID"]);
               

                if (!_isInit)
                    Dangol.ShowSplash();

               
                getCompanyModelList();
                


                if (!_isInit)
                    Dangol.CloseSplash();
            }
            else
            {
                _drvCompany = null;
            }
        }

        private void gvModelList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvModelList.RowCount > 0);
            _dtModelInventory.Clear();

            if (isValidRow)
            {
                _drvCompanyModel = e.Row as DataRowView;

                _modelListId = ConvertUtil.ToInt64(_drvCompanyModel["MODEL_LIST_ID"]);
                lcgInventory.CustomHeaderButtons[lcgInventory.CustomHeaderButtons.Count - 1].Properties.Checked = false;
                getCompanyInventoryList();
            }
            else
            {
                _drvCompanyModel = null;
            }
        }

        private void gvCompanyInventory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvModelInventory.RowCount > 0);

            if (isValidRow)
            {
                _drvModelInventory = e.Row as DataRowView;

                _componentId = ConvertUtil.ToInt64(_drvModelInventory["COMPONENT_ID"]);
                _componentCd = ConvertUtil.ToString(_drvModelInventory["COMPONENT_CD"]);
            }
            else
            {
                _componentId = -1;
                _componentCd = "";
                _drvModelInventory = null;
            }
        }

        private bool getCompanyModelList()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("COMPANY_ID", _companyId);

            if (DBConsigned.getCompanyModelList(jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;

                    foreach (JObject jData in jArray.Children<JObject>())
                    {
                        DataRow dr1 = _dtCompanyModel.NewRow();
                        dr1["NO"] = index++;
                        dr1["MODEL_LIST_ID"] = jData["MODEL_LIST_ID"];
                        dr1["MODEL_NM"] = ConvertUtil.ToString(jData["MODEL_NM"]);
                        dr1["KEYWORD"] = ConvertUtil.ToString(jData["KEYWORD"]);
                        
                        dr1["PC_TYPE"] = ConvertUtil.ToInt32(jData["PC_TYPE"]);
                        dr1["RECEIPT_TYPE"] = ConvertUtil.ToInt32(jData["RECEIPT_TYPE"]);
                        dr1["CPU_ASSIGN_YN"] = ConvertUtil.ToInt32(jData["CPU_ASSIGN_YN"]);
                        dr1["MEM_TYPE"] = ConvertUtil.ToInt32(jData["MEM_TYPE"]);
                        dr1["MEM_SLOT"] = ConvertUtil.ToInt32(jData["MEM_SLOT"]);
                        dr1["SERIAL_NO_TYPE"] = ConvertUtil.ToInt32(jData["SERIAL_NO_TYPE"]);

                        dr1["CREATE_DT"] = ConvertUtil.ToDateTimeNull(jData["CREATE_DT"]);
                        dr1["STATE"] = 0;

                        _dtCompanyModel.Rows.Add(dr1);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool getCompanyInventoryList()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("MODEL_LIST_ID", _modelListId);

            if (DBConsigned.getModelInventoryList(jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;

                    foreach (JObject jData in jArray.Children<JObject>())
                    {
                        DataRow dr1 = _dtModelInventory.NewRow();
                        dr1["NO"] = index++;
                        dr1["MODEL_PART_ID"] = jData["MODEL_PART_ID"];
                        dr1["COMPONENT_ID"] = jData["COMPONENT_ID"];
                        dr1["COMPONENT"] = jData["COMPONENT"];
                        dr1["MODEL_NM"] = jData["MODEL_NM"];
                        dr1["COMPONENT_CD"] = jData["COMPONENT_CD"];
                        dr1["CONSIGNED_TYPE"] = ConvertUtil.ToInt32(jData["CONSIGNED_TYPE"]);
                        dr1["PART_CNT"] = jData["COMPONENT_CNT"];
                        dr1["CREATE_DT"] = ConvertUtil.ToDateTimeNull(jData["CREATE_DT"]);
                        dr1["CHECK"] = false;
                        dr1["STATE"] = 0;

                        _dtModelInventory.Rows.Add(dr1);
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
                int rowhandle = gvModelInventory.FocusedRowHandle;
                int topRowIndex = gvModelInventory.TopRowIndex;
                gvModelInventory.FocusedRowHandle = -1;
                gvModelInventory.FocusedRowHandle = rowhandle;

                try
                {
                    gvModelInventory.BeginUpdate();
                    foreach (DataRow row in _dtModelInventory.Rows)
                    {
                        row.BeginEdit();
                        row["CHECK"] = false;
                        row.EndEdit();
                    }

                    ArrayList rows = new ArrayList();
                    for (int i = 0; i < gvModelInventory.DataRowCount; i++)
                    {
                        int rowHandle = gvModelInventory.GetVisibleRowHandle(i);
                        rows.Add(gvModelInventory.GetDataRow(rowHandle));
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
                    gvModelInventory.EndUpdate();
                }
                
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (gcConsignedType.OptionsColumn.ReadOnly)
                {
                    gcConsignedType.OptionsColumn.ReadOnly = false;
                    gcPartCnt.OptionsColumn.ReadOnly = false;
                }
                
            }
        }

        private void lcgInventory_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvModelInventory.FocusedRowHandle;
                int topRowIndex = gvModelInventory.TopRowIndex;
                gvModelInventory.FocusedRowHandle = -1;
                gvModelInventory.FocusedRowHandle = rowhandle;

                gvModelInventory.BeginDataUpdate();

                foreach (DataRow row in _dtModelInventory.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
                gvModelInventory.EndDataUpdate();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (!gcConsignedType.OptionsColumn.ReadOnly)
                {
                    gcConsignedType.OptionsColumn.ReadOnly = true;
                    gcPartCnt.OptionsColumn.ReadOnly = true;
                }
            }
        }

        private void lcgInventory_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.Equals(1))
            {
                using (DlgConsignedCompanyInventory dlgConsignedCompanyInventory = new DlgConsignedCompanyInventory(_companyId))
                {
                    dlgConsignedCompanyInventory.StartPosition = FormStartPosition.Manual;
                    dlgConsignedCompanyInventory.Location = new Point(this.Location.X + (this.Size.Width / 2) - (dlgConsignedCompanyInventory.Size.Width / 2),
                    this.Location.Y + (this.Size.Height / 2) - (dlgConsignedCompanyInventory.Size.Height / 2));

                    if (dlgConsignedCompanyInventory.ShowDialog(this) == DialogResult.OK)
                    {
                        List<long> listComponentId = dlgConsignedCompanyInventory._listComponentId;
                        DataRow[] rows;
                        List<long> listData = new List<long>();
                        foreach (long componentId in listComponentId)
                        {
                            rows = _dtModelInventory.Select($"COMPONENT_ID = {componentId}");
                            if (rows.Length < 1)
                                listData.Add(componentId);

                        }

                        if (listData.Count == 0)
                        {
                            Dangol.Message("처리되었습니다.");
                            return;
                        }

                        JObject jResult = new JObject();
                        JObject jobj = new JObject();

                        jobj.Add("MODEL_LIST_ID", _modelListId);
                        jobj.Add("LIST_COMPONENT_ID", string.Join(",", listData));
                        jobj.Add("COMPONENT_CNT", 1);
                        jobj.Add("CONSIGNED_TYPE", 1);
                        jobj.Add("BULK_YN", 1);

                        if (DBConsigned.creteCompanyModelComponent(jobj, ref jResult))
                        {
                            _dtModelInventory.Clear();
                            getCompanyInventoryList();
                            lcgInventory.CustomHeaderButtons[lcgInventory.CustomHeaderButtons.Count - 1].Properties.Checked = false;
                            Dangol.Message("처리되었습니다.");
                        }
                        else
                        {
                            Dangol.Message(jResult["MSG"]);
                            return;
                        }
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                int rowhandle = gvModelInventory.FocusedRowHandle;
                gvModelInventory.FocusedRowHandle = -1;
                gvModelInventory.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtModelInventory.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }


                if (Dangol.MessageYN("선택하신 부품을 모델에서 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<long> listId = new List<long>();
                    long data;
                    foreach (DataRow row in rows)
                    {
                        data = ConvertUtil.ToInt64(row["MODEL_PART_ID"]);
                        if (!listId.Contains(data))
                        {
                            listId.Add(data);
                        }

                    }

                    jobj.Add("LIST_MODEL_PART_ID", string.Join(",", listId));
                    jobj.Add("BULK_YN", 1);

                    if (DBConsigned.deleteCompanyModelComponent(jobj, ref jResult))
                    {
                        _dtModelInventory.Clear();
                        getCompanyInventoryList();
                        lcgInventory.CustomHeaderButtons[lcgInventory.CustomHeaderButtons.Count - 1].Properties.Checked = false;
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                int rowhandle = gvModelInventory.FocusedRowHandle;
                gvModelInventory.FocusedRowHandle = -1;
                gvModelInventory.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtModelInventory.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }


                if (Dangol.MessageYN("선택하신 부품을 자사재고로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<long> listId = new List<long>();
                    long data;
                    foreach (DataRow row in rows)
                    {
                        data = ConvertUtil.ToInt64(row["MODEL_PART_ID"]);
                        if (!listId.Contains(data))
                        {
                            listId.Add(data);
                        }

                    }

                    jobj.Add("LIST_MODEL_PART_ID", string.Join(",", listId));
                    jobj.Add("CONSIGNED_TYPE", 2);
                    jobj.Add("BULK_YN", 1);

                    if (DBConsigned.updateCompanyModelComponent(jobj, ref jResult))
                    {
                        gvModelInventory.BeginDataUpdate();
                        foreach (DataRow row in rows)
                            row["CONSIGNED_TYPE"] = 2;
                        gvModelInventory.EndDataUpdate();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(4))
            {
                int rowhandle = gvModelInventory.FocusedRowHandle;
                gvModelInventory.FocusedRowHandle = -1;
                gvModelInventory.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtModelInventory.Select("CHECK = TRUE");
                if (rows.Length < 1)
                {
                    Dangol.Message("선택된 부품이 없습니다.");
                    return;
                }


                if (Dangol.MessageYN("선택하신 부품을 생산대행재고로 변경하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    List<long> listId = new List<long>();
                    long data;
                    foreach (DataRow row in rows)
                    {
                        data = ConvertUtil.ToInt64(row["MODEL_PART_ID"]);
                        if (!listId.Contains(data))
                        {
                            listId.Add(data);
                        }

                    }

                    jobj.Add("LIST_MODEL_PART_ID", string.Join(",", listId));
                    jobj.Add("CONSIGNED_TYPE", 1);
                    jobj.Add("BULK_YN", 1);

                    if (DBConsigned.updateCompanyModelComponent(jobj, ref jResult))
                    {
                        gvModelInventory.BeginDataUpdate();
                        foreach (DataRow row in rows)
                            row["CONSIGNED_TYPE"] = 1;
                        gvModelInventory.EndDataUpdate();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                        return;
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(5))
            {
                if (Dangol.MessageYN($"변경사항을 저장하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                gvModelInventory.BeginUpdate();
                updateModelPartInfo();
                gvModelInventory.EndUpdate();

                Dangol.Message("처리되었습니다.");

            }
        }

        private void gvCompanyInventory_DoubleClick(object sender, EventArgs e)
        {
            if (_drvModelInventory == null || _componentId == -1)
            {
                Dangol.Message("선택한 부품이 없습니다.");
            }
            else
            {
                using (DlgCompanyPartByWarehousing dlgCompanyPartByWarehousing = new DlgCompanyPartByWarehousing(_companyId, _componentId, _componentCd, _drvModelInventory["MODEL_NM"], _drvModelInventory["CONSIGNED_TYPE"]))
                {
                    if (dlgCompanyPartByWarehousing.ShowDialog(this) == DialogResult.OK)
                    {

                    }
                }
            }
        }

        private void rileCompanyVisible_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_drvModelInventory == null)
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

                    jobj.Add("COMPANY_COMPONENT_ID", ConvertUtil.ToInt64(_drvModelInventory["COMPANY_COMPONENT_ID"]));
                    jobj.Add("COMPANY_VISIBLE", value);
                    jobj.Add("BULK_YN", 0);

                    if (DBConsigned.updateCompanyComponentInfo(jobj, ref jResult))
                    {
                        gvModelInventory.BeginDataUpdate();
                        _drvModelInventory.BeginEdit();
                        _drvModelInventory["COMPANY_VISIBLE"] = value;
                        _drvModelInventory.EndEdit();
                        gvModelInventory.EndDataUpdate();
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
        

        private void rileConsignedType_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_drvModelInventory == null)
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

                    jobj.Add("COMPANY_COMPONENT_ID", ConvertUtil.ToInt64(_drvModelInventory["COMPANY_COMPONENT_ID"]));
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

        private void setComponentInfo(bool isBulk, int type)
        {
            Dangol.ShowSplash();
            gvModelInventory.BeginDataUpdate();
            if (isBulk)
            {
                DataRow[] rows = _dtModelInventory.Select("CHECK = TRUE");

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
                _drvModelInventory.BeginEdit();

                _drvModelInventory["CONSIGNED_TYPE"] = type;

                if (type == 1)
                {
                    _drvModelInventory["TOTAL_CNT"] = _drvModelInventory["TOTAL_CNT_L"];
                    _drvModelInventory["GOOD_CNT"] = _drvModelInventory["GOOD_CNT_L"];
                    _drvModelInventory["RELEASE_CNT"] = _drvModelInventory["RELEASE_CNT_L"];
                    _drvModelInventory["FAULT_CNT"] = _drvModelInventory["FAULT_CNT_L"];
                    _drvModelInventory["SPARE_CNT"] = _drvModelInventory["SPAREL_CNT_L"];
                    _drvModelInventory["LOCK_CNT"] = _drvModelInventory["LOCK_CNT_L"];
                }
                else if (type == 2)
                {
                    _drvModelInventory["TOTAL_CNT"] = _drvModelInventory["TOTAL_CNT_A"];
                    _drvModelInventory["GOOD_CNT"] = _drvModelInventory["GOOD_CNT_A"];
                    _drvModelInventory["RELEASE_CNT"] = _drvModelInventory["RELEASE_CNT_A"];
                    _drvModelInventory["FAULT_CNT"] = _drvModelInventory["FAULT_CNT_A"];
                    _drvModelInventory["SPARE_CNT"] = _drvModelInventory["SPARE_CNT_A"];
                    _drvModelInventory["LOCK_CNT"] = _drvModelInventory["LOCK_CNT_A"];
                }
                else if (type == 3)
                {
                    _drvModelInventory["TOTAL_CNT"] = ConvertUtil.ToInt32(_drvModelInventory["TOTAL_CNT_A"]) + ConvertUtil.ToInt32(_drvModelInventory["TOTAL_CNT_L"]);
                    _drvModelInventory["GOOD_CNT"] = ConvertUtil.ToInt32(_drvModelInventory["GOOD_CNT_A"]) + ConvertUtil.ToInt32(_drvModelInventory["GOOD_CNT_L"]);
                    _drvModelInventory["RELEASE_CNT"] = ConvertUtil.ToInt32(_drvModelInventory["RELEASE_CNT_A"]) + ConvertUtil.ToInt32(_drvModelInventory["RELEASE_CNT_L"]);
                    _drvModelInventory["FAULT_CNT"] = ConvertUtil.ToInt32(_drvModelInventory["FAULT_CNT_A"]) + ConvertUtil.ToInt32(_drvModelInventory["FAULT_CNT_L"]);
                    _drvModelInventory["SPARE_CNT"] = ConvertUtil.ToInt32(_drvModelInventory["SPARE_CNT_A"]) + ConvertUtil.ToInt32(_drvModelInventory["SPARE_CNT_L"]);
                    _drvModelInventory["LOCK_CNT"] = ConvertUtil.ToInt32(_drvModelInventory["LOCK_CNT_A"]) + ConvertUtil.ToInt32(_drvModelInventory["LOCK_CNT_L"]);
                }

                _drvModelInventory.EndEdit();
            }

            gvModelInventory.EndDataUpdate();
            Dangol.CloseSplash();
        }

        private void gvCompanyInventory_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0 && e.Column.FieldName == "CONSIGNED_TYPE")
            {
                int value = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["CONSIGNED_TYPE"]));

                if(value == 1)
                    e.Appearance.ForeColor = System.Drawing.Color.Black;
                else if (value == 2)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            else if (e.RowHandle >= 0 && e.Column.FieldName == "COMPANY_VISIBLE")
            {
                int value = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["COMPANY_VISIBLE"]));

                if (value == 0)
                    e.Appearance.BackColor = System.Drawing.Color.LightCoral;
            }
            else if (e.Column.FieldName == "COMPONENT" && e.RowHandle >= 0)
            {
                int state = ConvertUtil.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]));

                if (state == 2)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
        }

        private void lcgModelList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (dlgCreateCompanyModel createCompanyModel = new dlgCreateCompanyModel(_companyId, -1, "", 0, "N", 1))
                {
                    createCompanyModel.StartPosition = FormStartPosition.Manual;
                    createCompanyModel.Location = new Point(this.Location.X + (this.Size.Width / 2) - (createCompanyModel.Size.Width / 2),
                    this.Location.Y + (this.Size.Height / 2) - (createCompanyModel.Size.Height / 2));

                    if (createCompanyModel.ShowDialog(this) == DialogResult.OK)
                    {
                        gvModelList.FocusedRowObjectChanged -= gvModelList_FocusedRowObjectChanged;
                        _dtCompanyModel.Clear();
                        _dtModelInventory.Clear();
                        getCompanyModelList();
                        gvModelList.FocusedRowObjectChanged += gvModelList_FocusedRowObjectChanged;
                        gvModelList.FocusedRowHandle = -2147483646;
                        gvModelList.MoveLast();

                        Dangol.Message("처리되었습니다.");
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                //using (dlgCreateCompanyModel createCompanyModel = new dlgCreateCompanyModel(_companyId, _modelListId,
                //    ConvertUtil.ToString(_drvCompanyModel["MODEL_NM"]),ConvertUtil.ToInt32(_drvCompanyModel["CPU_ASSIGN_YN"]),"N", 2))
                //{
                //    createCompanyModel.StartPosition = FormStartPosition.Manual;
                //    createCompanyModel.Location = new Point(this.Location.X + (this.Size.Width / 2) - (createCompanyModel.Size.Width / 2),
                //    this.Location.Y + (this.Size.Height / 2) - (createCompanyModel.Size.Height / 2));

                //    if (createCompanyModel.ShowDialog(this) == DialogResult.OK)
                //    {
                //        long modelListId = _modelListId;

                //        gvModelList.FocusedRowObjectChanged -= gvModelList_FocusedRowObjectChanged;
                //        _dtCompanyModel.Clear();
                //        _dtModelInventory.Clear();
                //        getCompanyModelList();
                //        gvModelList.FocusedRowObjectChanged += gvModelList_FocusedRowObjectChanged;
                //        int rowHandle = gvModelList.LocateByValue("MODEL_LIST_ID", modelListId);

                //        if (rowHandle > -1)
                //        {
                //            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                //            {
                //                gvModelList.FocusedRowHandle = -2147483646;
                //                gvModelList.FocusedRowHandle = rowHandle;
                //            }
                //        }
                //        else
                //        {
                //            if (_dtCompanyModel.Rows.Count > 0)
                //            {
                //                gvModelList.FocusedRowHandle = -2147483646;
                //                gvModelList.MoveFirst();
                //            }
                //        }


                //        Dangol.Message("처리되었습니다.");
                //    }
                //}


                if (Dangol.MessageYN($"변경사항을 저장하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                gvModelList.BeginUpdate();
                updateModelInfo();
                gvModelList.EndUpdate();

                Dangol.Message("처리되었습니다.");

            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                if (Dangol.MessageYN($"현재 모델을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    jobj.Add("MODEL_LIST_ID", _modelListId);
                    jobj.Add("DEL_YN", "Y");

                    if (DBConsigned.updateCompanyModelInfo(jobj, ref jResult))
                    {
                        gvCompanyList.BeginDataUpdate();
                        _drvCompanyModel.Delete();
                        gvCompanyList.EndDataUpdate();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(jResult["MSG"]);
                    }
                }
            }
        }

        private void gvModelList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "CHECK")
            {
                int state = ConvertUtil.ToInt32(_drvCompanyModel["STATE"]);

                if (state == 0)
                    _drvCompanyModel["STATE"] = 2;
            }
        }

        private void gvModelList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "MODEL_NM" && e.RowHandle >= 0)
            {
                int state = ConvertUtil.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]));

                if (state == 2)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
        }

        private void updateModelInfo()
        {
            int rowhandle = gvModelList.FocusedRowHandle;
            gvModelList.FocusedRowHandle = -1;
            gvModelList.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtCompanyModel.Select("STATE = 2");
            if (rows.Length < 1)
            {
                Dangol.Message("수정된 모델이 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            foreach (DataRow row in rows)
            {
                JObject jData = new JObject();

                jData.Add("MODEL_LIST_ID", ConvertUtil.ToInt64(row["MODEL_LIST_ID"]));
                jData.Add("MODEL_NM", ConvertUtil.ToString(row["MODEL_NM"]));
                jData.Add("KEYWORD", ConvertUtil.ToString(row["KEYWORD"]));
                
                jData.Add("CPU_ASSIGN_YN", ConvertUtil.ToInt32(row["CPU_ASSIGN_YN"]));
                jData.Add("PC_TYPE", ConvertUtil.ToInt32(row["PC_TYPE"]));
                jData.Add("MEM_TYPE", ConvertUtil.ToInt32(row["MEM_TYPE"]));
                jData.Add("MEM_SLOT", ConvertUtil.ToInt32(row["MEM_SLOT"]));
                jData.Add("SERIAL_NO_TYPE", ConvertUtil.ToInt32(row["SERIAL_NO_TYPE"]));
                jData.Add("RECEIPT_TYPE", ConvertUtil.ToInt32(row["RECEIPT_TYPE"]));
                jData.Add("USER_ID", ProjectInfo._userId);

                jArray.Add(jData);
            }

            jobj.Add("DATA", jArray);

            if (DBConsigned.updateModelInfo(jobj, ref jResult))
            {
                gvModelList.BeginDataUpdate();
                foreach (DataRow row in rows)
                {
                    row["STATE"] = 0;
                }
                gvModelList.EndDataUpdate();

                gvModelList.OptionsBehavior.ReadOnly = true;
                lcgModelList.CustomHeaderButtons[2].Properties.Checked = false;
            }
            else
            {
                //return false;
            }
        }

        private void updateModelPartInfo()
        {
            int rowhandle = gvModelInventory.FocusedRowHandle;
            gvModelInventory.FocusedRowHandle = -1;
            gvModelInventory.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dtModelInventory.Select("STATE = 2");
            if (rows.Length < 1)
            {
                Dangol.Message("수정된 부품이 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            foreach (DataRow row in rows)
            {
                JObject jData = new JObject();

                jData.Add("MODEL_PART_ID", ConvertUtil.ToInt64(row["MODEL_PART_ID"]));
                jData.Add("CONSIGNED_TYPE", ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]));
                jData.Add("COMPONENT_CNT", ConvertUtil.ToInt32(row["PART_CNT"]));
                jData.Add("USER_ID", ProjectInfo._userId);

                jArray.Add(jData);
            }

            jobj.Add("DATA", jArray);

            if (DBConsigned.updateModelPartInfo(jobj, ref jResult))
            {
                gvModelInventory.BeginDataUpdate();
                foreach (DataRow row in rows)
                {
                    row["STATE"] = 0;
                }
                gvModelInventory.EndDataUpdate();

                //gvModelInventory.OptionsBehavior.ReadOnly = true;
                gcConsignedType.OptionsColumn.ReadOnly = true;
                gcPartCnt.OptionsColumn.ReadOnly = true;
                lcgInventory.CustomHeaderButtons[2].Properties.Checked = false;
            }
            else
            {
                //return false;
            }
        }

        private void lcgModelList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (gvModelList.OptionsBehavior.ReadOnly)
                gvModelList.OptionsBehavior.ReadOnly = false;
        }

        private void lcgModelList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (!gvModelList.OptionsBehavior.ReadOnly)
                gvModelList.OptionsBehavior.ReadOnly = true;
        }

        private void gvModelInventory_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
            if (e.Column.FieldName != "CHECK")
            {
                int state = ConvertUtil.ToInt32(_drvModelInventory["STATE"]);

                if (state == 0)
                    _drvModelInventory["STATE"] = 2;
            }
        }
    }


    
}