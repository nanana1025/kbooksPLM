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

namespace WareHousingMaster.view.release
{
    public partial class usrReleaseConsignedNew_UNUSED : DevExpress.XtraEditors.XtraForm
    {
        string _representativeType = "P";
        string _representativeCol = "RECEIPT";
        string _representativeNo = null;
        short _checkType = 3;
        int _proxyState = 0;

        string _componentCd = "ALL";
        GridColumn[] arrGridColumn;

        DataRowView _currentRow;

        BindingSource bs;
        BindingSource bsDetail;
        long _id;
        bool _headerButtonVisible = true;

        string _barcode = null;
        string _component = null;
        string _currentComponentCd = null;
        long _inventoryId = -1;
        long _componentId = -1;
        bool _isWarehousingCheck = false;

        DateTime _warehousingDate = new DateTime();
        DataTable _dtPrintPort;

        public usrReleaseConsignedNew_UNUSED()
        {
            InitializeComponent();

            arrGridColumn = new GridColumn[4] { gc1, gc2, gc3, gc4 };

            bs = new BindingSource();
            bsDetail = new BindingSource();

            bs.DataSource = ProjectInfo._dtDeviceInfo;
            //tnReceipt.EditValue = "LT201130001";
            lcComponent.Text = _componentCd;
           
        }


        private void getCheckInfoInit()
        {
            ProjectInfo._dicPartCheckRelease = new Dictionary<long, Dictionary<string, int>>();

            DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");
            JObject jResult = new JObject();

            long mbdInventoryId = -1;


            foreach (DataRow row in rows)
            {
                long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                string componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                if (componentCd.Equals("MBD"))
                    mbdInventoryId = inventoryId;

                if (!ProjectInfo._dicPartCheckRelease.ContainsKey(inventoryId))
                {
                    Dictionary<string, int> dicData = new Dictionary<string, int>();

                    if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                    {
                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {

                                short value = x.Value.ToObject<short>();

                                if (!dicData.ContainsKey(name))
                                    dicData.Add(name, value);
                            }
                        }
                    }

                    ProjectInfo._dicPartCheckRelease.Add(inventoryId, dicData);
                }
            }
 
            ProjectInfo._dicNtbCheckRelease = new Dictionary<string, short>();
            ProjectInfo._dicAllInOneCheckRelease = new Dictionary<string, short>();

            if (mbdInventoryId > 0)
            {
                if (DBConnect.getCheckInfo(mbdInventoryId, "NTB", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                ProjectInfo._caseDestroyDescriptionRelease = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                ProjectInfo._batteryRemainRelease = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                ProjectInfo._dicNtbCheckRelease.Add(name, value);
                            }
                        }
                    }
                }

                if (DBConnect.getCheckInfo(mbdInventoryId, "ALLINONE", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            short value = x.Value.ToObject<short>();

                            if (!ProjectInfo._dicAllInOneCheckRelease.ContainsKey(name))
                                ProjectInfo._dicAllInOneCheckRelease.Add(name, value);
                        }
                    }
                }
            }
        }

        private void getCheckInfo(long inventoryId, string componentCd)
        {
            JObject jResult = new JObject();

            if (!ProjectInfo._dicPartCheckRelease.ContainsKey(inventoryId))
            {
                Dictionary<string, int> dicData = new Dictionary<string, int>();

                if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            int value = x.Value.ToObject<int>();

                            if (!dicData.ContainsKey(name))
                                dicData.Add(name, value);
                        }
                    }
                }

                ProjectInfo._dicPartCheckRelease.Add(inventoryId, dicData);
            }

            if (componentCd.Equals("MBD"))
            {
                if (DBConnect.getCheckInfo(inventoryId, "NTB", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                ProjectInfo._caseDestroyDescriptionRelease = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                ProjectInfo._batteryRemainRelease = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                    ProjectInfo._dicNtbCheckRelease.Add(name, value);
                            }
                        }
                    }
                }

                if (DBConnect.getCheckInfo(inventoryId, "ALLINONE", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            short value = x.Value.ToObject<short>();

                            if (!ProjectInfo._dicAllInOneCheckRelease.ContainsKey(name))
                                ProjectInfo._dicAllInOneCheckRelease.Add(name, value);
                        }
                    }
                }
            }
        }



        private void usrRelease_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();

            string serialNo = ProjectInfo.osProductNo;
            serialNo = serialNo.Replace("-", "");
            serialNo = serialNo.Substring(1, 14);
            teSerialNo.Text = serialNo;
            teSerialNo.EditValue = serialNo;

            teBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
            //leLocation.DataBindings.Add(new Binding("EditValue", bs, "WAREHOUSE", false, DataSourceUpdateMode.Never));
            //lePallet.DataBindings.Add(new Binding("EditValue", bs, "PALLET", false, DataSourceUpdateMode.Never));
            //teSerialNo.DataBindings.Add(new Binding("Text", bs, "RELEASE_RESULT", false, DataSourceUpdateMode.Never));
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

            //DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "WAREHOUSE_NM", "USE_YN != 'N'", "WAREHOUSE_NM ASC");
            //Util.LookupEditHelper(leLocation, dtLocation, "KEY", "VALUE");

            //DataTable dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            //Util.LookupEditHelper(lePallet, dtPallet, "PALLET_ID", "PALLET_NM");
        }

        private void setIInitData()
        {
            // 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;
            //leLocation.EditValue = ProjectInfo._location;
            leProductType.EditValue = ProjectInfo._type;
            teUserName.EditValue = ProjectInfo._userName;
        }

        private void gvInventory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvInventory.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                _id = ConvertUtil.ToInt64(_currentRow["ID"]);
                _barcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _component = ConvertUtil.ToString(_currentRow["COMPONENT"]);
                _inventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);
                _componentId = ConvertUtil.ToInt64(_currentRow["COMPONENT_ID"]);
                _currentComponentCd = ConvertUtil.ToString(_currentRow["COMPONENT_CD"]);

                if (string.IsNullOrEmpty(_barcode) || _inventoryId < 0)
                    setBarcodeButton(false, false, true);
                else
                    setBarcodeButton(true, true, false);

                gcInventoryDetail.DataSource = null;
                bsDetail.DataSource = ProjectInfo._dicDeviceInfoDetail[_id];
                gcInventoryDetail.DataSource = bsDetail;

            }
        }

        private void setGridControl()
        {
            gcInventory.DataSource = null;

            List<string> listColnm = ProjectInfo._dicDeviceColumnNm[_componentCd];
            List<string> listCol = ProjectInfo._dicDeviceColumn[_componentCd];

            lcComponent.Text = _componentCd;

            for (int i = 0; i < Math.Min(listCol.Count, arrGridColumn.Length); i++)
            {
                arrGridColumn[i].Caption = listColnm[i];
                arrGridColumn[i].FieldName = listCol[i];
            }
            if (_componentCd.Equals("ALL"))
            {
                bs.Filter = $"";
                gcNo.FieldName = "NO1";
            }
            else
            {
                bs.Filter = $"COMPONENT_CD = '{_componentCd}'";
                gcNo.FieldName = "NO";
            }

            gcInventory.DataSource = bs;

            if (_componentCd.Equals("ALL") && !_headerButtonVisible)
            {
                _headerButtonVisible = true;
                lcgInventory.CustomHeaderButtons[0].Properties.Visible = true;
                lcgInventory.CustomHeaderButtons[1].Properties.Visible = true;
                gcCheck.Visible = true;
            }
            else if (!_componentCd.Equals("ALL") && _headerButtonVisible)
            {
                _headerButtonVisible = false;
                lcgInventory.CustomHeaderButtons[0].Properties.Visible = false;
                lcgInventory.CustomHeaderButtons[1].Properties.Visible = false;
                gcCheck.Visible = false;
            }
        }

        private void sbAll_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("ALL"))
                return;

            _componentCd = "ALL";
            setGridControl();
        }

        private void sbCpu_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("CPU"))
                return;

            _componentCd = "CPU";
            setGridControl();
        }

        private void sbMbd_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MBD"))
                return;

            _componentCd = "MBD";
            setGridControl();
        }

        private void sbMEM_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MEM"))
                return;

            _componentCd = "MEM";
            setGridControl();
        }

        private void sbStg_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("STG"))
                return;

            _componentCd = "STG";
            setGridControl();
        }

        private void sbVga_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("VGA"))
                return;

            _componentCd = "VGA";
            setGridControl();
        }

        private void sbMON_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MON"))
                return;

            _componentCd = "MON";
            setGridControl();
        }

        

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.Equals(1))
            {
                foreach(DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = true;
                    row.EndEdit();
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                foreach (DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
            }
        }

        private void lcgInventory_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                foreach (DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = true;
                    row.EndEdit();
                }
            }
        }

        private void lcgInventory_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                foreach (DataRow row in ProjectInfo._dtDeviceInfo.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }
            }
        }

        private void sbGetReleasePart_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible) { }
            else SplashScreenManager.CloseForm();

            SplashScreenManager.ShowForm(typeof(WareHousingMaster.Helper.DlgWaitForm));
            SplashScreenManager.Default.SetWaitFormCaption($"생산대행");
            SplashScreenManager.Default.SetWaitFormDescription("상태 정보 가져오는 중...");

            JObject jResult = new JObject();

            if (DBConnect.getProxyInfo(_representativeNo, ref jResult))
            {
                _proxyState = ConvertUtil.ToInt32(jResult["PROXY_STATE"]);
                if(_proxyState != 1)
                {
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("처리중인 생산대행만 사용할수 있습니다.");
                    return;
                }
            }
            else
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show(jResult["MSG"].ToString());
                return;
            }

            SplashScreenManager.Default.SetWaitFormDescription("부품 정보 가져오는 중...");


            JObject jData = new JObject();
            DataRow[] rows;
            DataRow[] detailRows;

            if (ProjectInfo._listReleaseList == null)
                ProjectInfo._listReleaseList = new List<long>();
            else
                ProjectInfo._listReleaseList.Clear();

            if (DBConnect.getConsignedReleaseList(_representativeNo, ref jResult))
            {
                foreach (var x in jResult)
                {
                    string id = x.Key;

                    if (!id.Equals("SUCCESS"))
                    {
                        jData = (JObject)jResult[id];
                        rows = ProjectInfo._dtDeviceInfo.Select($"NAME = '{id}'");

                        if (rows.Length > 0)
                        {
                            foreach (string keyCol in ProjectInfo._listKeyColumn)
                                rows[0][keyCol] = jData[keyCol];
                            rows[0]["RELEASE_YN"] = true;
                            rows[0]["INVENTORY_YN"] = true;

                            long rowId = ConvertUtil.ToInt64(rows[0]["ID"]);
                            DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[rowId];

                            string componentCd = jData["COMPONENT_CD"].ToString();
                            List<string> listFullColum = ProjectInfo._dicDeviceFullColumn[componentCd];

                            foreach (string col in listFullColum)
                            {
                                detailRows = dtDeviceInfoFromDB.Select($"SPEC_NM = '{col}'");
                                if (detailRows.Length > 0)
                                    detailRows[0]["MATCHING_INFO"] = jData[col];
                            }

                            long inventoryId = ConvertUtil.ToInt64(jData["INVENTORY_ID"]);

                            if (!ProjectInfo._listReleaseList.Contains(inventoryId))
                                ProjectInfo._listReleaseList.Add(inventoryId);


                            if (rowId == _id)
                            {
                                _barcode = ConvertUtil.ToString(jData["BARCODE"]);
                                _component = ConvertUtil.ToString(jData["COMPONENT"]);
                                _inventoryId = ConvertUtil.ToInt64(jData["INVENTORY_ID"]);
                                _componentId = ConvertUtil.ToInt64(jData["COMPONENT_ID"]);
                            }

                        }
                    }
                }

                if (ProjectInfo._dicPartCheckRelease == null)
                    getCheckInfoInit();

                if (ProjectInfo._dicProductList == null)
                {
                    rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");

                    if (rows.Length > 0)
                    {
                        long inventoryId = ConvertUtil.ToInt64(rows[0]["INVENTORY_ID"]);
                        string barcode = ConvertUtil.ToString(rows[0]["BARCODE"]);
                        if (inventoryId > 0)
                            Util.checkProductState(inventoryId, barcode);
                        else
                            ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
                    }
                    else
                        ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
                }
                SplashScreenManager.CloseForm();
                //using (dlgCompare compreComponent = new dlgCompare())
                //{
                //    if (compreComponent.ShowDialog(this) == DialogResult.OK)
                //    {

                //    }
                //}

            }
            else
            {
                SplashScreenManager.CloseForm();
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                
            }
        }

        private void sbDeleteReleasePart_Click(object sender, EventArgs e)
        {
            //using (dlgCompare compreComponent = new dlgCompare())
            //{
            //    if (compreComponent.ShowDialog(this) == DialogResult.OK)
            //    {

            //    }
            //}
        }

        private void sbConstructProduct_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return;
            }

            if (!_componentCd.Equals("ALL"))
            {
                Dangol.Message($"제품 구성은 전체 화면에서만 가능합니다.");
                return;
            }

            DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");
            DataRow[] rowUnCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = FALSE");

            if (rowCheck.Length < 2)
            {
                Dangol.Message("제품은 2개 이상의 부품으로 구성돼야 합니다.");
                return;
            }

            Dictionary<string, string> dicInventoryId = new Dictionary<string, string>();
            List<long> listInventoryId = new List<long>();
            long inventoryId = -1;
            long mbdInventoryId = -1;
            string componentCd;
            bool isCheckMbd = false;
            JObject jResult = new JObject();
            int inventoryCnt = 0;

            foreach (DataRow drCheck in rowCheck)
            {
                inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                if (componentCd.Equals("MBD"))
                {
                    isCheckMbd = true;
                    mbdInventoryId = inventoryId;
                }
                else
                {
                    if (inventoryId < 0)
                        continue;

                    if (!listInventoryId.Contains(inventoryId))
                        listInventoryId.Add(inventoryId);
                }

                inventoryCnt++;

                if (dicInventoryId.ContainsKey(componentCd))
                {
                    string sinventoryId = dicInventoryId[componentCd];
                    sinventoryId = $"{sinventoryId},{inventoryId}";
                    dicInventoryId[componentCd] = sinventoryId;
                }
                else
                    dicInventoryId.Add(componentCd, ConvertUtil.ToString(inventoryId));

            }

            if (!isCheckMbd)
            {
                Dangol.Message("메인보드가 체크되지 않았습니다.");
                return;
            }

            if (mbdInventoryId < 1)
            {
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return;
            }

            if (inventoryCnt < 2)
            {
                Dangol.Message("제품 구성은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            if (Dangol.MessageYN($"선택하신 부품들로 제품을 구성하시겠습니까?") == DialogResult.No)
                return;

            if (DBConnect.constructProduct(_representativeType, _representativeNo, _representativeCol, mbdInventoryId, listInventoryId, ref jResult))
            {
                string id = "";
                string barcode = "";
                ProjectInfo._dicProductList.Clear();

                foreach (DataRow drCheck in rowCheck)
                {
                    inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                    id = ConvertUtil.ToString(inventoryId);
                    barcode = ConvertUtil.ToString(drCheck["BARCODE"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                    if (ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]) > 0)
                    {

                        Dictionary<string, string> dicData = new Dictionary<string, string>();

                        dicData.Add("INVENTORY_ID", id);
                        dicData.Add("BARCODE", barcode);
                        dicData.Add("COMPONENT_CD", componentCd);

                        ProjectInfo._dicProductList.Add(inventoryId, dicData);


                        drCheck["PRODUCT_YN"] = true;
                    }
                    else
                        drCheck["PRODUCT_YN"] = false;
                }

                foreach (DataRow drCheck in rowUnCheck)
                    drCheck["PRODUCT_YN"] = false;

                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void sbAddReleasePart_Click(object sender, EventArgs e)
        {
            if(!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return;
            }

            DataRow[] rowsSelect;

            if (_componentCd.Equals("ALL"))
            {  
                DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

                string componentCd;
                DataRow[] row;
                JObject jResult = new JObject();
                List<long> listInventoryId = new List<long>();
                long id;
                long componentId;
                long inventoryId = -1;
                string msg = "오류가 발생했습니다. 관리자에게 문의하세요.";

                if (rowCheck.Length < 1)
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                foreach (DataRow drCheck in rowCheck)
                {
                    id = ConvertUtil.ToInt64(drCheck["ID"]);
                    componentId = ConvertUtil.ToInt64(drCheck["COMPONENT_ID"]);
                    inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                    if (componentId < 0 && inventoryId < 0)
                    {
                        continue;
                    }

                    if(!listInventoryId.Contains(inventoryId))
                        listInventoryId.Add(inventoryId);
                }

                if(listInventoryId.Count < 1)
                {
                    Dangol.Message("등록된 부품이 없습니다.");
                    return;
                }

                if (DBConnect.InsertReleasePart(_representativeType, _representativeNo, _representativeCol, listInventoryId, ref jResult))
                {
                    //리턴 받은 부품들 상태 update

                    foreach (DataRow drCheck in rowCheck)
                    {
                        id = ConvertUtil.ToInt64(drCheck["ID"]);
                        rowsSelect = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");

                        if (rowsSelect.Length > 0)
                        {
                            string sInventoryId = ConvertUtil.ToString(rowsSelect[0]["INVENTORY_ID"]);
                            string resultMsg = ConvertUtil.ToString(jResult[sInventoryId]);

                            drCheck["RELEASE_RESULT"] = resultMsg;

                            if (resultMsg.Equals("정상 등록"))
                            {
                                if (!ProjectInfo._listReleaseList.Contains(inventoryId))
                                    ProjectInfo._listReleaseList.Add(inventoryId);

                                drCheck["RELEASE_YN"] = true;  
                            }
                        }
                    }

                    if (Convert.ToBoolean(jResult["COMPLETE"])) //
                        Dangol.Message("출고등록되었습니다.");
                    else
                        Dangol.Message("개별 출고 등록 결과를 확인하세요.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else
            {
                if(_inventoryId < 0)
                {
                    Dangol.Message("등록되지 않은 재고입니다.");
                    return;
                }

                List<long> listInventoryId = new List<long>();
                JObject jResult = new JObject();

                listInventoryId.Add(_inventoryId);

                if (DBConnect.InsertReleasePart(_representativeType, _representativeNo, _representativeCol, listInventoryId, ref jResult))
                {
                    rowsSelect = ProjectInfo._dtDeviceInfo.Select($"ID = {_id}");

                    if (rowsSelect.Length > 0)
                    {
                        string sInventoryId = ConvertUtil.ToString(rowsSelect[0]["INVENTORY_ID"]);
                        string resultMsg = ConvertUtil.ToString(jResult[sInventoryId]);

                        _currentRow.BeginEdit();

                        _currentRow["RELEASE_RESULT"] = resultMsg;

                        if (resultMsg.Equals("정상 등록"))
                        {
                            if (!ProjectInfo._listReleaseList.Contains(_inventoryId))
                                ProjectInfo._listReleaseList.Add(_inventoryId);

                            _currentRow["RELEASE_YN"] = true;
                        }
                        _currentRow.EndEdit();
                    }

                    if (Convert.ToBoolean(jResult["COMPLETE"]))
                        Dangol.Message("출고등록되었습니다.");
                    else
                        Dangol.Message("개별 출고 등록 결과를 확인하세요.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }

        }

        private bool checkRepresentativeInfo()
        {
            if(tnReceipt.EditValue == null)
                return false;
            else
             _representativeNo = tnReceipt.EditValue.ToString();

            if (string.IsNullOrWhiteSpace(_representativeNo))
                return false;
            else
            {
                return true;
            }
            
        }

       

        private void sbUpdateComponent_Click(object sender, EventArgs e)
        {
            string msg = "오류가 발생했습니다. 관리자에게 문의하세요.";

            if (_componentCd.Equals("ALL"))
            {
                DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

                string componentCd;
                DataTable dtComponentInfo;
                DataRow[] rowDeviceInfo;
                DataRow[] row;
                JObject jResult = new JObject();
                long id;
                long componentId;
                long inventoryId;

                if (rowCheck.Length < 1)
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                foreach (DataRow drCheck in rowCheck)
                {
                    id = ConvertUtil.ToInt64(drCheck["ID"]);
                    inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                    componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                    if (inventoryId < 0)
                        continue;

                    dtComponentInfo = ProjectInfo._dicDeviceInfo[componentCd];
                    rowDeviceInfo = dtComponentInfo.Select($"ID = {id}");
                    row = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");

                    if (rowDeviceInfo.Length > 0)
                    {
                        if (DBConnect.updateInventoryInfo(inventoryId, componentCd, rowDeviceInfo[0], ref jResult))
                        {
                            DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];
                            foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                            {
                                dr.BeginEdit();
                                dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                                dr.EndEdit();
                            }

                            if (componentCd.Equals("MBD"))
                            {
                                JObject jResultos = new JObject();
                                DBConnect.osInfoCheck(inventoryId, rowDeviceInfo[0], ref jResultos, _representativeNo);
                            }

                            msg = ConvertUtil.ToString(jResult["MSG"]);
                        }
                        else
                            msg = ConvertUtil.ToString(jResult["MSG"]);
                    }
                }

                Dangol.Message(msg);

            }
            else
            {

                DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_componentCd];
                DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
                JObject jResult = new JObject();

                if (rowDeviceInfo.Length > 0)
                {
                    if (DBConnect.updateInventoryInfo(_inventoryId, _currentComponentCd, rowDeviceInfo[0], ref jResult))
                    {
                        DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];
                        foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                        {
                            dr.BeginEdit();
                            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                            dr.EndEdit();
                        }

                        if (_currentComponentCd.Equals("MBD"))
                        {
                            JObject jResultos = new JObject();
                            DBConnect.osInfoCheck(ConvertUtil.ToInt64(jResult["INVENTORY_ID"]), rowDeviceInfo[0], ref jResultos);
                        }

                        msg = ConvertUtil.ToString(jResult["MSG"]);
                    }
                    else
                        msg = ConvertUtil.ToString(jResult["MSG"]);
                }

                Dangol.Message(msg);
            }
        }

        private void sbPrintProduct_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            if (!_componentCd.Equals("ALL"))
            {
                Dangol.Message($"제품 출력은 전체 화면에서만 가능합니다.");
                return;
            }

            DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"CHECK = TRUE");

            if (rowCheck.Length < 2)
            {
                Dangol.Message("제품 출력은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            Dictionary<string, string> dicInventoryId = new Dictionary<string, string>();
            long inventoryId;
            string componentCd;
            bool isCheckMbd = false;
            bool isMbdInventoryIdExist = false;
            JObject jResult = new JObject();
            int inventoryCnt = 0;

            foreach (DataRow drCheck in rowCheck)
            {
                inventoryId = ConvertUtil.ToInt64(drCheck["INVENTORY_ID"]);
                componentCd = ConvertUtil.ToString(drCheck["COMPONENT_CD"]);

                if(componentCd.Equals("MBD"))
                {
                    isCheckMbd = true;

                    if (inventoryId > 0)
                        isMbdInventoryIdExist = true;
                }

                if (inventoryId < 0)
                    continue;

                inventoryCnt++;

                if (dicInventoryId.ContainsKey(componentCd))
                {
                    string sinventoryId = dicInventoryId[componentCd];
                    sinventoryId= $"{sinventoryId},{inventoryId}";
                    dicInventoryId[componentCd] = sinventoryId;
                }
                else
                    dicInventoryId.Add(componentCd, ConvertUtil.ToString(inventoryId));

            }

            if (!isCheckMbd)
            {
                Dangol.Message("메인보드가 체크되지 않았습니다.");
                return;
            }

            if (!isMbdInventoryIdExist)
            {
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return;
            }

            if(inventoryCnt < 2)
            {
                Dangol.Message("제품 출력은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            if (DBConnect.printProduct(_representativeType, _representativeNo, _representativeCol, dicInventoryId, lePrintPort.EditValue.ToString(), ref jResult))
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void sbPrintPart_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            if (string.IsNullOrEmpty(_barcode) || _inventoryId < 1)
            {
                Dangol.Message("재고로 등록되지 않은 부품입니다.");
                return;
            }

            JObject jResult = new JObject();

            if (DBConnect.printInventoryInfo(_representativeType, _representativeNo, _representativeCol, _barcode, lePrintPort.EditValue.ToString(), ref jResult))
            {
                Dangol.Message("부품정보가 출력되었습니다.");
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void sbCheck_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return;
            }

            //if (!_isWarehousingCheck)
            //{
            //    if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
            //    {
            //        return;
            //    }
            //    _isWarehousingCheck = true;
            //}

            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();

            int checkValue = ConvertUtil.ToInt32(leProductType.EditValue);
            string checkText = leProductType.Text;

            if (checkValue == 1)
            {
                Dangol.Message("PC는 개별 부품 체크만 가능합니다.");
                return;
            }

            if (ProjectInfo._type != checkValue)
            {
                if (Dangol.MessageYN($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{ProjectInfo._typeNm}, 선택:{checkText})", $"{checkText} 검수 체크") == DialogResult.No)
                    return;
            }

            DataRow[] row = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");
            string barcode = null;
            long inventoryId = -1;
            List<long> listInventoryId = new List<long>();

            if (row.Length > 0)
            {
                barcode = ConvertUtil.ToString(row[0]["BARCODE"]);
                inventoryId = ConvertUtil.ToInt64(row[0]["INVENTORY_ID"]);

                if (string.IsNullOrEmpty(barcode) || inventoryId < 1)
                {
                    Dangol.Message("메인보드가 재고로 등록되지 않았습니다.");
                    return;
                }
            }

            row = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");

            if (row.Length > 0)
            {
                //if (Dangol.MessageYN($"등록된 재고가 {row.Length}개 입니다. 검수를 진행 하시겠습니까?", $"{checkText} 검수 체크") == DialogResult.No)
                //    return;

                foreach (DataRow dr in row)
                    listInventoryId.Add(ConvertUtil.ToInt64(dr["INVENTORY_ID"]));
            }

            if (checkValue == 2)
            {
                //if ($"{checkText} 검수 체크".Equals("B200806001") || $"{checkText} 검수 체크".Equals("B201030004") || _warehousingDate > Convert.ToDateTime("2020-11-11"))
                //{
                    //using (DlgNtb2ndEditionCheck ntbCheck = new DlgNtb2ndEditionCheck(ProjectInfo._dicNtbCheckRelease, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, _dtPrintPort, _d))
                    //{
                    //    if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                    //    {
                    //        ProjectInfo._caseDestroyDescriptionRelease = ntbCheck._caseDestroyDescription;
                    //        ProjectInfo._batteryRemainRelease = ntbCheck._batteryRemain;
                    //        DBConnect.insertNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckRelease, listInventoryId, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease);

                    //        if (ntbCheck._isPrint)
                    //            DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckRelease, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, ProjectInfo._printerPort);

                    //    }
                    //}
            //    }
            //    else
            //    {
            //        using (DlgNtbCheck ntbCheck = new DlgNtbCheck(ProjectInfo._dicNtbCheck, _dtPrintPort))
            //        {
            //            if (ntbCheck.ShowDialog(this) == DialogResult.OK)
            //            {
            //                DBConnect.insertNtbCheck1stEdition(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheck, listInventoryId);

            //                if (ntbCheck._isPrint)
            //                    DBConnect.printNtbCheck1stEdition(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheck, ProjectInfo._printerPort);

            //            }
            //        }
            //    }
            }
            else if (checkValue == 3)
            {
                using (DlgAllInOneCheck2 allInOneCheck = new DlgAllInOneCheck2(ProjectInfo._dicAllInOneCheckRelease, _dtPrintPort))
                {
                    if (allInOneCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        //DBConnect.insertAllInOneCheck(barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckRelease, listInventoryId);

                        if (allInOneCheck._isPrint)
                            DBConnect.printAllInOneCheck(barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckRelease, ProjectInfo._printerPort);
                    }
                }
            }
        }

        private void sbPartCheck_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            //if (!_isWarehousingCheck)
            //{
            //    if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
            //    {
            //        return;
            //    }
            //    _isWarehousingCheck = true;
            //}

            if (string.IsNullOrEmpty(_barcode) || _inventoryId < 0)
            {
                Dangol.Message("선택하신 부품은 재고로 등록되지 않았습니다.");
                return;
            }

            Dictionary<string, int> dicCheckInfo = ProjectInfo._dicPartCheckRelease[_inventoryId];

            if (_currentComponentCd.Equals("MON"))
            {
                //using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(dicCheckInfo, _dtPrintPort))
                //{
                //    if (monitorCheck.ShowDialog(this) == DialogResult.OK)
                //    {
                //        DBConnect.insertMonitorCheck(_barcode, _checkType, dicCheckInfo);

                //        if (monitorCheck._isPrint)
                //            DBConnect.printMonitorCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
                //    }
                //}
            }
            else if (_currentComponentCd.Equals("CAS"))
            {
                //using (DlgCasCheck inventoryCheck = new DlgCasCheck(dicCheckInfo, _dtPrintPort))
                //{
                //    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                //    {
                //        DBConnect.insertCasCheck(_barcode, _checkType, dicCheckInfo);

                //        if (inventoryCheck._isPrint)
                //            DBConnect.printCasCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
                //    }
                //}
            }
            //else if (_currentComponentCd.Equals("CPU"))
            //{
            //    using (DlgCpuCheck inventoryCheck = new DlgCpuCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("MEM"))
            //{
            //    using (DlgRamCheck inventoryCheck = new DlgRamCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("MBD"))
            //{
            //    using (DlgMbdCheck inventoryCheck = new DlgMbdCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("VGA"))
            //{
            //    using (DlgVgaCheck inventoryCheck = new DlgVgaCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("STG"))
            //{
            //    string type = ConvertUtil.ToString(_currentRow["STG_TYPE"]);

            //    if (type.Contains("SSD"))
            //    {
            //        using (DlgSsdCheck inventoryCheck = new DlgSsdCheck(dicCheckInfo, _dtPrintPort))
            //        {
            //            if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //            {
            //                DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //                if (inventoryCheck._isPrint)
            //                    DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, "SSD", ProjectInfo._printerPort);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        using (DlgHddCheck inventoryCheck = new DlgHddCheck(dicCheckInfo, _dtPrintPort))
            //        {
            //            if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //            {
            //                DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //                if (inventoryCheck._isPrint)
            //                    DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, "HDD", ProjectInfo._printerPort);
            //            }
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("ODD"))
            //{
            //    using (DlgOddCheck inventoryCheck = new DlgOddCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            //else if (_currentComponentCd.Equals("POW"))
            //{
            //    using (DlgPowCheck inventoryCheck = new DlgPowCheck(dicCheckInfo, _dtPrintPort))
            //    {
            //        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
            //        {
            //            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo);

            //            if (inventoryCheck._isPrint)
            //                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._printerPort);
            //        }
            //    }
            //}
            else
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                return;
            }
        }

        private void sbClear_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN($"매칭 정보를 초기화하시겠습니까?") == DialogResult.No)
                return;

            DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_currentComponentCd];
            DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
            JObject jResult = new JObject();


            if (rowDeviceInfo.Length > 0)
            {
                ProjectInfo._dicPartCheckRelease.Remove(_inventoryId);
                if (_currentComponentCd.Equals("MBD"))
                {
                    ProjectInfo._dicNtbCheckRelease.Clear();
                    ProjectInfo._dicAllInOneCheckRelease.Clear();

                    foreach (DataRow dr in ProjectInfo._dtDeviceInfo.Rows)
                        dr["PRODUCT_YN"] = false;
                }

                _currentRow.BeginEdit();

                _currentRow["INVENTORY_ID"] = dtComponentInfo.Rows[0]["INVENTORY_ID"] = -1;
                _currentRow["BARCODE"] = dtComponentInfo.Rows[0]["BARCODE"] = "";
                _currentRow["COMPONENT_ID"] = dtComponentInfo.Rows[0]["COMPONENT_ID"] = -1;
                _currentRow["COMPONENT"] = dtComponentInfo.Rows[0]["COMPONENT"] = "";
                _currentRow["INVENTORY_YN"] = false;
                _currentRow["PRODUCT_YN"] = false;
                _currentRow["RELEASE_YN"] = false;
                _currentRow["RELEASE_RESULT"] = "";

                _currentRow.EndEdit();


                DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];

                foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                {
                    dr.BeginEdit();
                    dr["MATCHING_INFO"] = "";
                    dr.EndEdit();
                }

                _barcode = "";
                _inventoryId = -1;
                _componentId = -1;
                _component = "";

                setBarcodeButton(false, false, true);
            };
        }

        private void sbGetDeviceInfo_Click(object sender, EventArgs e)
        {
            if (teBarcode.EditValue == null || teBarcode.EditValue.ToString().Length < 12)
            {
                Dangol.Message("관리번호를 입력하세요.");
                return;
            }

            string barcode = teBarcode.EditValue.ToString();
            DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_currentComponentCd];
            JObject jResult = new JObject();


            if (DBConnect.getInventoryFromConsigned(barcode, _currentComponentCd, ref jResult))
            {
                _currentRow.BeginEdit();
                DataRow[] rowBarcode = ProjectInfo._dtDeviceInfo.Select($"BARCODE = '{barcode}'");

                if (rowBarcode.Length > 0)
                { 
                    Dangol.Message("이미 현재 제품에 존재하는 부품입니다.");
                    return;
                }

                JObject jData = (JObject)jResult["DATA"];

                _currentRow["INVENTORY_ID"] = dtComponentInfo.Rows[0]["INVENTORY_ID"] = jData["INVENTORY_ID"];
                _currentRow["BARCODE"] = dtComponentInfo.Rows[0]["BARCODE"] = jData["BARCODE"];
                _currentRow["COMPONENT_ID"] = dtComponentInfo.Rows[0]["COMPONENT_ID"] = jData["COMPONENT_ID"];
                _currentRow["COMPONENT"] = dtComponentInfo.Rows[0]["COMPONENT"] = jData["COMPONENT"];

                _currentRow["INVENTORY_YN"] = true;

                DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];
                DataRow[] detailRows;

                string componentCd = jData["COMPONENT_CD"].ToString();
                List<string> listFullColum = ProjectInfo._dicDeviceFullColumn[componentCd];

                foreach (string col in listFullColum)
                {
                    detailRows = dtDeviceInfoFromDB.Select($"SPEC_NM = '{col}'");
                    if (detailRows.Length > 0)
                        detailRows[0]["MATCHING_INFO"] = jData[col];
                }

                gcInventoryDetail.DataSource = null;
                bsDetail.DataSource = dtDeviceInfoFromDB;
                gcInventoryDetail.DataSource = bsDetail;
                    

                _barcode = ConvertUtil.ToString(jData["BARCODE"]);
                _inventoryId = ConvertUtil.ToInt64(jData["INVENTORY_ID"]);
                _component = ConvertUtil.ToString(jData["COMPONENT"]);
                _componentId = ConvertUtil.ToInt64(jData["COMPONENT_ID"]);

                getCheckInfo(_inventoryId, _currentComponentCd);

                setBarcodeButton(true, true, false);

                if (_currentComponentCd.Equals("MBD"))
                    Util.checkProductState(_inventoryId, _barcode);
                else
                {
                    if (ProjectInfo._dicProductList.ContainsKey(_inventoryId))
                        _currentRow["PRODUCT_YN"] = true;
                }

                if (ProjectInfo._listReleaseList != null)
                    if (ProjectInfo._listReleaseList.Contains(_inventoryId))
                    {
                        _currentRow["RELEASE_YN"] = true;
                        _currentRow["RELEASE_RESULT"] = "정상 등록";
                    }
                _currentRow.EndEdit();

                Dangol.Message("부품 정보를 변경하였습니다.");
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            
        }

        private void setBarcodeButton(bool teBarcodeFlag, bool sbClearFlag, bool sbGetDeviceInfoFlag)
        {
            teBarcode.Properties.ReadOnly = teBarcodeFlag;
            sbClear.Enabled = sbClearFlag;
            sbGetDeviceInfo.Enabled = sbGetDeviceInfoFlag;
        }

        private void sbGetInventoryInfo_Click(object sender, EventArgs e)
        {

        }

        private void leProductType_EditValueChanged(object sender, EventArgs e)
        {
            ProjectInfo._type = ConvertUtil.ToInt32(leProductType.EditValue);
            ProjectInfo._typeNm = leProductType.Text;
        }
    }
}