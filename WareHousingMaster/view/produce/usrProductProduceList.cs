using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.produce
{
    public partial class usrProductProduceList : DevExpress.XtraEditors.XtraForm
    {

        string _representativeType = "R";
        string _representativeCol = "RELEASES";
        string _representativeIdCol = "RELEASE_ID";
        string _representativeNo = null;
        
        long _representativeId = -1;
        long _inventoryId = -1;
        object _companyId = -1;
        long _type = 1;
        string _componentCd = "ALL";
        string _barcode;

        List<long> _listRepresentativeId;

        DataRowView _currentProductProduce;
        DataRowView _currentProduct;

        DataTable _dtProductProduce;
        DataTable _dt;

        DataTable _dtPallet;

        BindingSource _bs;
        BindingSource _bsRelease;

        bool initialize = true;
        bool initializeEnter = true;



        public usrProductProduceList()
        {
            InitializeComponent();

            _dtProductProduce = new DataTable();
            _dtProductProduce.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtProductProduce.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtProductProduce.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dtProductProduce.Columns.Add(new DataColumn("RELEASES", typeof(string)));
            _dtProductProduce.Columns.Add(new DataColumn("RELEASE_STATE", typeof(string)));
            _dtProductProduce.Columns.Add(new DataColumn("RELEASE_TYPE", typeof(string)));
            _dtProductProduce.Columns.Add(new DataColumn("RELEASE_DT", typeof(string)));
            _dtProductProduce.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtProductProduce.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtProductProduce.Columns.Add(new DataColumn("PRODUCT_CNT", typeof(int)));
            _dtProductProduce.Columns.Add(new DataColumn("REPAIR_CNT", typeof(int)));

            //_dtProductProduce.Columns.Add(new DataColumn("EXAM_CNT", typeof(int)));
            //_dtProductProduce.Columns.Add(new DataColumn("COMPLETE_CNT", typeof(int)));
            //_dtProductProduce.Columns.Add(new DataColumn("REMAIN_CNT", typeof(int)));

            _dtProductProduce.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("RELEASE_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("RELEASES", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCE_STATE", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));

            _dt.Columns.Add(new DataColumn("CHECK_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK_USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("QC_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("QC_USER_ID", typeof(string)));

            _dt.Columns.Add(new DataColumn("MANUFACTURE_TYPE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dt.Columns.Add(new DataColumn("MBD_SN", typeof(string)));
            _dt.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));

            _dt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(int)));
            _dt.Columns.Add(new DataColumn("SYSTEM_SN", typeof(string)));
            _dt.Columns.Add(new DataColumn("CPU_MODEL_NM", typeof(string)));



            _bsRelease = new BindingSource();
            _bs = new BindingSource();


            _listRepresentativeId = new List<long>();

            initialize = true;
            initializeEnter = true;
        }

        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            teReleases.DataBindings.Add(new Binding("Text", _bsRelease, "RELEASES", false, DataSourceUpdateMode.Never));
            leProduceState.DataBindings.Add(new Binding("EditValue", _bsRelease, "RELEASE_STATE", false, DataSourceUpdateMode.Never));
            deReleaseDt.DataBindings.Add(new Binding("EditValue", _bsRelease, "RELEASE_DT", false, DataSourceUpdateMode.Never));
            leProduceType.DataBindings.Add(new Binding("EditValue", _bsRelease, "RELEASE_TYPE", false, DataSourceUpdateMode.Never));
            leCompanyId.DataBindings.Add(new Binding("EditValue", _bsRelease, "COMPANY_ID", false, DataSourceUpdateMode.Never));
            meDes.DataBindings.Add(new Binding("Text", _bsRelease, "DES", false, DataSourceUpdateMode.Never));


            gvProduceList.FocusedRowObjectChanged -= gvProduceList_FocusedRowObjectChanged;
            gvProduceList.FocusedRowChanged -= gvProduceList_FocusedRowChanged;

            getReleaseList();

            gvProduceList.FocusedRowChanged += gvProduceList_FocusedRowChanged;
            gvProduceList.FocusedRowObjectChanged += gvProduceList_FocusedRowObjectChanged;

            if(_dtProductProduce.Rows.Count > 0)
            {
                gvProduceList.FocusedRowHandle = -2147483646;
                gvProduceList.MoveFirst();
            }

            initialize = false;
        }

        private void setInfoBox()
        {
            Util.LookupEditHelper(rileUserId, ProjectInfo._dtUserId, "KEY", "VALUE");     
            
            DataTable dtmanufactureType = new DataTable();

            dtmanufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtmanufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtmanufactureType, 1, "삼성/LG");
            Util.insertRowonTop(dtmanufactureType, 2, "외산/기타");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            Util.LookupEditHelper(leCompanyId, dtmanufactureType, "KEY", "VALUE");
            Util.LookupEditHelper(rileManufactureType, dtmanufactureType, "KEY", "VALUE");

            DataTable dtNickName = Util.getCodeListCustom("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "NTB_NICKNAME IS NOT NULL", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(rileNickName, dtNickName, "KEY", "VALUE");

            DataTable dtProductProduceState = Util.getCodeList("CD0804", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductProduceState, dtProductProduceState, "KEY", "VALUE");

            DataTable dtProduceState = Util.getCodeList("CD0801", "KEY", "VALUE");
            Util.LookupEditHelper(rileProduceState, dtProduceState, "KEY", "VALUE");
            Util.LookupEditHelper(leProduceState, dtProduceState, "KEY", "VALUE");

            DataTable dtProduceState1 = new DataTable();

            dtProduceState1.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtProduceState1.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtProduceState1, "-1", "해당없음");

            foreach (DataRow row in dtProduceState.Rows)
            {
                DataRow dr = dtProduceState1.NewRow();
                dr["KEY"] = row["KEY"];
                dr["VALUE"] = row["VALUE"];
                dtProduceState1.Rows.Add(dr);
            }
            Util.LookupEditHelper(leProduceState1, dtProduceState1, "KEY", "VALUE");

            DataTable dtProduceType = Util.getCodeList("CD0802", "KEY", "VALUE");
            Util.LookupEditHelper(leProduceType, dtProduceType, "KEY", "VALUE");

                
            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_ID ASC");
            Util.insertRowonTop(dtCompany, "-1", "해당없음");
            Util.LookupEditHelper(rileCompanyId, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompanyId, dtCompany, "KEY", "VALUE");
            Util.LookupEditHelper(leCompanyId1, dtCompany, "KEY", "VALUE");

            DataTable dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
            Util.LookupEditHelper(rileProductGrade, dtPGrade, "KEY", "VALUE");

            var today = DateTime.Now;
            var pastDate = today.AddDays(-364);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

            deReleaseDtFrom.EditValue = null;
            deReleaseDtTo.EditValue = null;

            leCompanyId1.EditValue = "-1";
            leProduceState1.EditValue = "-1";

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsRelease.DataSource = _dtProductProduce;
            _bs.DataSource = _dt;
        }

       

        private void setGridControl()
        {
            gcProduceList.DataSource = null;
            gcProduceList.DataSource = _bsRelease;

            gcList.DataSource = null;
            gcList.DataSource = _bs;
        }

        private void gvProduceList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvProduceList.RowCount > 0);

            gvList.BeginDataUpdate();

            _listRepresentativeId.Clear();
            _dt.Clear();

            if (isValidRow)
            {
                _currentProductProduce = e.Row as DataRowView;
                _representativeNo = ConvertUtil.ToString(_currentProductProduce["RELEASES"]);
                _representativeId = ConvertUtil.ToInt64(_currentProductProduce["RELEASE_ID"]);

                if (!initialize)
                    Dangol.ShowSplash();

                getProduct();

                if (!initialize)
                    Dangol.CloseSplash();


            }
            else
            {
                _representativeNo = "";
                _representativeId = -1;
                _currentProductProduce = null;
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
                //_productType = ConvertUtil.ToInt32(_currentProduct["PRODUCT_TYPE"]);
                _barcode = ConvertUtil.ToString(_currentProduct["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentProduct["INVENTORY_ID"]);
                //_representativeNo = ConvertUtil.ToString(_currentProduct["RELEASES"]);
                //_representativeId = ConvertUtil.ToInt64(_currentProduct["RELEASE_ID"]);
            }
            else
            {
                //_representativeNo = "";
                //_representativeId = -1;
                _currentProduct = null;
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentProduct = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
                //_productType = ConvertUtil.ToInt32(_currentProduct["PRODUCT_TYPE"]);
                _barcode = ConvertUtil.ToString(_currentProduct["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(_currentProduct["INVENTORY_ID"]);
                //_representativeNo = ConvertUtil.ToString(_currentProduct["RELEASES"]);
                //_representativeId = ConvertUtil.ToInt64(_currentProduct["RELEASE_ID"]);
            }
            else
            {
                //_representativeNo = "";
                //_representativeId = -1;
                _currentProduct = null;
            }
        }


        private void sbSearch_Click(object sender, EventArgs e)
        {
            initialize = true;
            Dangol.ShowSplash();
            refresh();
            Dangol.CloseSplash();
            initialize = false;
        }

        private void refresh()
        {
            int rowHandle = gvProduceList.FocusedRowHandle;
            int topRowIndex = gvProduceList.TopRowIndex;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowHandle;

            string produceNo = _representativeNo;

            gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged -= gvList_FocusedRowChanged;

            getReleaseList();

            gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
            gvList.FocusedRowChanged += gvList_FocusedRowChanged;

            rowHandle = gvList.LocateByValue("RELEASES", produceNo);

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
        }

        private bool getReleaseList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            _dtProductProduce.Clear();

            if (DBProductProduce.getProduceList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    gvProduceList.BeginDataUpdate();
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtProductProduce.NewRow();

                        dr["NO"] = index++;
                        dr["CHECK"] = false;
                        dr["RELEASE_ID"] = obj["RELEASE_ID"];
                        dr["RELEASES"] = obj["RELEASES"];
                        dr["RELEASE_DT"] = ConvertUtil.ToDateTimeNull(obj["RELEASE_DT"]);
                        dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
                        dr["RELEASE_STATE"] = obj["RELEASE_STATE"];
                        dr["RELEASE_TYPE"] = obj["RELEASE_TYPE"];
                        dr["COMPANY_ID"] = obj["COMPANY_ID"];
                        dr["DES"] = obj["DES"];
                        dr["PRODUCT_CNT"] = obj["PRODUCT_CNT"];
                        //dr["REPAIR_CNT"] = obj["REPAIR_CNT"];
  

                        _dtProductProduce.Rows.Add(dr);
                    }
                    gvProduceList.EndDataUpdate();
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

            //string dtReleaseFrom = "";
            //string dtReleaseTo = "";
            //if (deReleaseDtFrom.EditValue != null && !string.IsNullOrEmpty(deReleaseDtFrom.EditValue.ToString()))
            //    dtReleaseFrom = $"{deReleaseDtFrom.Text} 00:00:00";

            //if (deReleaseDtTo.EditValue != null && !string.IsNullOrEmpty(deReleaseDtTo.EditValue.ToString()))
            //    dtReleaseTo = $"{deReleaseDtTo.Text} 23:59:59";

            //DateTime dtReleasefrom;
            //DateTime dtReleaseto;
            //dtReleasefrom = Convert.ToDateTime(dtReleaseFrom);
            //dtReleaseto = Convert.ToDateTime(dtReleaseTo);

            //int resultRelease = DateTime.Compare(dtReleasefrom, dtReleaseto);

            //if (resultRelease > 0)
            //{
            //    jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다.");
            //    return false;
            //}

            //TimeSpan TSRelease = dtReleaseto - dtReleasefrom;
            //int diffDayRelease = TSRelease.Days;

            //if (diffDayRelease > 365)
            //{
            //    jData.Add("MSG", "출고 기간은 365일을 초과할 수 없습니다.");
            //    return false;
            //}


            //jData.Add("RELEASE_DT_S", dtReleaseFrom);
            //jData.Add("RELEASE_DT_E", dtReleaseTo);

    
            if (!ConvertUtil.ToString(leProduceState1.EditValue).Equals("-1"))
                jData.Add("RELEASE_STATE", ConvertUtil.ToString(leProduceState1.EditValue));

            if (ConvertUtil.ToInt64(leCompanyId1.EditValue) != -1)
                jData.Add("COMPNAY_ID", ConvertUtil.ToInt64(leCompanyId1.EditValue));

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teDes.Text)))
                jData.Add("DES", ConvertUtil.ToString(teDes.Text));

            return true;
        }

        private void getProduct()
        {
            if (_currentProductProduce == null)
            {
                Dangol.Message("선택하신 출고번호가 없습니다.");
                return;
            }

            JObject jResult = new JObject();
            JObject jData = new JObject();

            jData.Add("TN_TABLE_PART", "TN_RELEASE_PART");
            jData.Add("TN_TABLE", "TN_RELEASE");
            jData.Add("REPRESENTATIVE_ID_COL", "RELEASE_ID");
            jData.Add("REPRESENTATIVE_COL", "RELEASES");
            jData.Add("TN_BOM", "TN_RELEASE_BOM_TREE");
            jData.Add("LIST_REPRESENTATIVE_ID", string.Join(",", new List<long>(new[] {_representativeId })));

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

                _dt.Rows.Add(dr);
            }
        }


        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("생산정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jobj = new JObject();
                JObject jResult = new JObject();

                jobj.Add("BULK_YN", 0);
                jobj.Add("RELEASE_ID", _representativeId);
                jobj.Add("RELEASE_STATE", ConvertUtil.ToString(leProduceState.EditValue));
                jobj.Add("RELEASE_TYPE", ConvertUtil.ToString(leProduceType.EditValue));
                jobj.Add("COMPANY_ID", ConvertUtil.ToString(leCompanyId.EditValue));
                jobj.Add("DES", meDes.Text);


                if (DBProductProduce.updateProduceInfo(jobj, ref jResult))
                {
                    int rowhandle = gvProduceList.FocusedRowHandle;
                    int topRowIndex = gvProduceList.TopRowIndex;

                    gvProduceList.BeginDataUpdate();
                    _currentProductProduce.BeginEdit();
                    _currentProductProduce["RELEASE_STATE"] = ConvertUtil.ToString(jobj["RELEASE_STATE"]);
                    _currentProductProduce["RELEASE_TYPE"] = ConvertUtil.ToString(jobj["RELEASE_TYPE"]);
                    _currentProductProduce["COMPANY_ID"] = ConvertUtil.ToString(jobj["COMPANY_ID"]);
                    _currentProductProduce["DES"] = ConvertUtil.ToString(jobj["DES"]);
                    _currentProductProduce.EndEdit();
                    gvProduceList.EndDataUpdate();

                    gvProduceList.FocusedRowHandle = rowhandle;
                    gvProduceList.TopRowIndex = topRowIndex;

                    Dangol.Message("저장되었습니다.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }


        private void lcgProduce_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
          
            if (e.Button.Properties.Tag.Equals(1))
            {
                using (DlgCreateProduce createProduce = new DlgCreateProduce())
                {
                    if (createProduce.ShowDialog(this) == DialogResult.OK)
                    {
                        gvProduceList.FocusedRowObjectChanged -= gvProduceList_FocusedRowObjectChanged;
                        gvProduceList.FocusedRowChanged -= gvProduceList_FocusedRowChanged;

                        getReleaseList();

                        gvProduceList.FocusedRowChanged += gvProduceList_FocusedRowChanged;
                        gvProduceList.FocusedRowObjectChanged += gvProduceList_FocusedRowObjectChanged;
                        
                        gvProduceList.MoveFirst();
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_currentProductProduce == null)
                {
                    Dangol.Warining("선택하신 출고번호가 없습니다.");
                    return;
                }

                if(_dt.Rows.Count > 0)
                {
                    Dangol.Warining("등록된 제품이 있는 생산은 삭제할 수 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 생산을 수정하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    jobj.Add("BULK_YN", 0);
                    jobj.Add("RELEASE_ID", _representativeId);

                    if (DBProductProduce.deleteProduce(jobj, ref jResult))
                    {
                        gvProduceList.BeginDataUpdate();

                        _currentProductProduce.Delete();

                        gvProduceList.EndDataUpdate();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                Dangol.ShowSplash();
                refresh();
                Dangol.CloseSplash();
            }
        }

        private void lcgProduce_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduceList.FocusedRowHandle;
            int topRowIndex = gvProduceList.TopRowIndex;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            try
            {
                gvProduceList.BeginUpdate();
                foreach (DataRow row in _dtProductProduce.Rows)
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

        private void lcgProduce_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvProduceList.FocusedRowHandle;
            int topRowIndex = gvProduceList.TopRowIndex;
            gvProduceList.FocusedRowHandle = -2147483646;
            gvProduceList.FocusedRowHandle = rowhandle;

            gvProduceList.BeginDataUpdate();

            foreach (DataRow row in _dtProductProduce.Rows)
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
        }
    }
}