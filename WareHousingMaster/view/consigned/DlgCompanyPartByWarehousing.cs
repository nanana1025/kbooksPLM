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
using WareHousingMaster.view.adjustment;
using WareHousingMaster.view.warehousing.copyComponent;
using WareHousingMaster.view.inventory;

namespace WareHousingMaster.view.consigned
{
    public partial class DlgCompanyPartByWarehousing : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtPart;
        BindingSource _bs;

        DataRowView _currentRow;
        long _companyId;
        long _componentId;
        string _componentCd;
        object _modelNm;
        int _consignedType;
        string _warehousing;

        public bool _isUpdate { private set; get; }

        public DlgCompanyPartByWarehousing(long companyId, long componentId, string componentCd, object modelNm, object consignedType)
        {
            InitializeComponent();

            _dtPart = new DataTable();
            _dtPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSiNG_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSING", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSING_DT", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("WAREHOUSING_TYPE", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("COMPANY_ID", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("SPEC_NM", typeof(string)));
            _dtPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));

            _dtPart.Columns.Add(new DataColumn("WAREHOUSING_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("TOTAL_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("GOOD_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("FAULT_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("RELEASE_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("LOCK_CNT", typeof(int)));
            _dtPart.Columns.Add(new DataColumn("PART_RELEASE_PRICE", typeof(long)));
            _dtPart.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));

            _companyId = companyId;
            _componentId = componentId;
            _componentCd = componentCd;
            _modelNm = modelNm;
            _consignedType = ConvertUtil.ToInt32(consignedType);

            _bs = new BindingSource();

            _isUpdate = false;
        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            getCompanyInventoryList();

            if (ProjectInfo._userType.Equals("E"))
            {
                lcgPartList.CustomHeaderButtons[0].Properties.Visible = false;
                gcPrice.Visible = false;
                gcTotalPrice.Visible = false;

            }

        }

        private void setInfoBox()
        {
            DataTable dtConsignedType = new DataTable();
            dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtConsignedType, 1, "생산대행");
            Util.insertRowonTop(dtConsignedType, 2, "자사재고");
            Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            gcPart.DataSource = null;
            _bs.DataSource = _dtPart;
            gcPart.DataSource = _bs;
        }

        private void sbCreate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvPart_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (ConvertUtil.ToInt32(gvPart.GetDataRow(e.RowHandle)["CONSIGNED_TYPE"]) == 2)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _warehousing = ConvertUtil.ToString(_currentRow["WAREHOUSING"]);
                //if (ConvertUtil.ToBoolean(_currentRow["ASSIGN_YN_O"]))
                //    gcAssign.OptionsColumn.AllowEdit = true;
                //else
                //    gcAssign.OptionsColumn.AllowEdit = false;
            }
            else
            {
                _warehousing = "";
                _currentRow = null;
                //gcAssign.OptionsColumn.AllowEdit = false;
            }
        }

        private void gvPart_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.Column.FieldName == "ASSIGN_YN")
            //{
            //    bool assignYnO = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN_O"]));
            //    bool assignYn = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["ASSIGN_YN"]));
            //    bool check = ConvertUtil.ToBoolean(View.GetRowCellValue(e.RowHandle, View.Columns["CHECK"]));

            //    if (check && assignYnO && !assignYn)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
            //    }
            //    else
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Transparent);
            //}
        }


        private bool getCompanyInventoryList()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("COMPANY_ID", _companyId);
            jobj.Add("COMPONENT_ID", _componentId);
            jobj.Add("CONSIGNED_TYPE", _consignedType);

            _dtPart.Clear();

            if (DBConsigned.getCompanyInventoryListByWarehousing(jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;
                    long warehousingId;
                    string warehousing;

                    foreach (JObject jData in jArray.Children<JObject>())
                    {
                        warehousingId = ConvertUtil.ToInt64(jData["WAREHOUSING_ID"]);

                        if (warehousingId > 0)
                            warehousing = ConvertUtil.ToString(jData["WAREHOUSING"]);
                        else
                            warehousing = "알수없음";

                        DataRow dr1 = _dtPart.NewRow();
                        dr1["NO"] = index++;
                        dr1["COMPONENT_CD"] = jData["COMPONENT_CD"];
                        dr1["WAREHOUSING"] = warehousing;
                        dr1["WAREHOUSING_ID"] = warehousingId;
                        dr1["COMPANY_ID"] = ConvertUtil.ToInt64(jData["COMPANY_ID"]);
                        dr1["WAREHOUSING_DT"] = ConvertUtil.ToDateTimeNull(jData["WAREHOUSING_DT"]);
                        
                        dr1["WAREHOUSING_TYPE"] = ConvertUtil.ToInt32(jData["WAREHOUSING_TYPE"]);
                        dr1["COMPANY_ID"] = ConvertUtil.ToInt64(jData["COMPANY_ID"]);
                        dr1["SPEC_NM"] = jData["SPEC_NM"];
                        dr1["MODEL_NM"] = _modelNm;

                        dr1["TOTAL_CNT"] = ConvertUtil.ToInt32(jData["GOOD_CNT"]) + ConvertUtil.ToInt32(jData["FAULT_CNT"]);
                        dr1["GOOD_CNT"] = jData["GOOD_CNT"];
                        dr1["RELEASE_CNT"] = jData["RELEASE_CNT"];
                        dr1["FAULT_CNT"] = jData["FAULT_CNT"];
                        //dr1["SPARE_CNT"] = jData["SPARE_CNT"];
                        dr1["LOCK_CNT"] = jData["LOCK_CNT"];
                        dr1["WAREHOUSING_CNT"] = ConvertUtil.ToInt32(dr1["TOTAL_CNT"]) + ConvertUtil.ToInt32(dr1["RELEASE_CNT"]);
                        dr1["PART_RELEASE_PRICE"] = jData["RELEASE_PRICE"];
                        dr1["TOTAL_PRICE"] = ConvertUtil.ToInt64(dr1["TOTAL_CNT"]) * ConvertUtil.ToInt64(dr1["PART_RELEASE_PRICE"]);

                        _dtPart.Rows.Add(dr1);
                    }
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private void lcgPartList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if(_currentRow == null)
            {
                Dangol.Warining("선택된 부품이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                int type = 1;
                long releaseCompanyId = _companyId;
                int warehousingType = ConvertUtil.ToInt32(_currentRow["WAREHOUSING_TYPE"]);

                if(ConsignedInfo._listLeadersTechComponetCd.Contains(_componentCd))
                {
                    if (warehousingType == 1)
                    {
                        releaseCompanyId = ConvertUtil.ToInt64(_currentRow["COMPANY_ID"]);
                        type = 2;
                    }                        
                }
  
                using (DlgCopyComponent dlgCopyComponent = new DlgCopyComponent(
                    ConvertUtil.ToInt64(_currentRow["WAREHOUSING_ID"]),
                    warehousingType,
                    type,
                    _componentId,
                    _componentCd,
                     ConvertUtil.ToInt64(_currentRow["PART_RELEASE_PRICE"]),
                    ConvertUtil.ToString(_currentRow["MODEL_NM"]),
                    ConvertUtil.ToInt64(_currentRow["COMPANY_ID"]),
                    releaseCompanyId))
                {
                    dlgCopyComponent.StartPosition = FormStartPosition.Manual;
                    dlgCopyComponent.Location = new Point(this.Location.X + (this.Size.Width / 2) - (dlgCopyComponent.Size.Width / 2),
                    this.Location.Y + (this.Size.Height / 2) - (dlgCopyComponent.Size.Height / 2));

                    if (dlgCopyComponent.ShowDialog(this) == DialogResult.OK)
                    {
                        _isUpdate = true;
                        string warehousing = _warehousing;
                        gvPart.FocusedRowObjectChanged -= gvPart_FocusedRowObjectChanged;
                        getCompanyInventoryList();
                        gvPart.FocusedRowObjectChanged += gvPart_FocusedRowObjectChanged;
                        int rowHandle = gvPart.LocateByValue("WAREHOUSING", warehousing);

                        if (rowHandle > -1)
                        {
                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            {
                                gvPart.FocusedRowHandle = -2147483646;
                                gvPart.FocusedRowHandle = rowHandle;
                            }
                        }
                        else
                        {
                            if (_dtPart.Rows.Count > 0)
                            {
                                gvPart.FocusedRowHandle = -2147483646;
                                gvPart.MoveFirst();
                            }
                        }
                        
                        Dangol.Message("추가되었습니다.");
                    }
                }
            }
        }

        private void gvPart_DoubleClick(object sender, EventArgs e)
        {
            if (_currentRow == null)
            {
                Dangol.Message("선택한 부품이 없습니다.");
            }
            else
            {
                long releaseCompanyId = _companyId;
                int warehousingType = ConvertUtil.ToInt32(_currentRow["WAREHOUSING_TYPE"]);

                if (ConsignedInfo._listLeadersTechComponetCd.Contains(_componentCd))
                {
                    if (warehousingType == 1)
                    {
                        releaseCompanyId = ConvertUtil.ToInt64(_currentRow["COMPANY_ID"]);
                    }
                }
                JObject jobj = new JObject();

                jobj.Add("WAREHOUSING_ID", ConvertUtil.ToInt64(_currentRow["WAREHOUSING_ID"]));
                jobj.Add("COMPONENT_ID", _componentId);
                jobj.Add("COMPANY_ID", releaseCompanyId);

                using (DlgInventoryListByWarehousing dlgInventoryListByWarehousing = new DlgInventoryListByWarehousing(jobj))
                {
                    //Dangol.setDlgPositionCenter(dlgInventoryListByWarehousing, this);

                    dlgInventoryListByWarehousing.StartPosition = FormStartPosition.Manual;
                    dlgInventoryListByWarehousing.Location = new Point(this.Location.X + (this.Size.Width / 2) - (dlgInventoryListByWarehousing.Size.Width / 2),
                    this.Location.Y + (this.Size.Height / 2) - (dlgInventoryListByWarehousing.Size.Height / 2));

                    dlgInventoryListByWarehousing.ShowDialog(this);

                    if (dlgInventoryListByWarehousing._isUpdate)
                    {
                        _isUpdate = true;
                        string warehousing = _warehousing;
                        gvPart.FocusedRowObjectChanged -= gvPart_FocusedRowObjectChanged;
                        getCompanyInventoryList();
                        gvPart.FocusedRowObjectChanged += gvPart_FocusedRowObjectChanged;
                        int rowHandle = gvPart.LocateByValue("WAREHOUSING", warehousing);

                        if (rowHandle > -1)
                        {
                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            {
                                gvPart.FocusedRowHandle = -2147483646;
                                gvPart.FocusedRowHandle = rowHandle;
                            }
                        }
                        else
                        {
                            if (_dtPart.Rows.Count > 0)
                            {
                                gvPart.FocusedRowHandle = -2147483646;
                                gvPart.MoveFirst();
                            }
                        }
                    }
                }
            }
        }
    }
}