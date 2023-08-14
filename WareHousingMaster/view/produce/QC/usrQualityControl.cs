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

namespace WareHousingMaster.view.QC
{
    public partial class usrQualityControl : DevExpress.XtraEditors.XtraForm
    {
        string _representativeType = "O";
        string _representativeCol = "RELEASES";
        string _representativeNo = null;
        short _examCheckType = 1;
        short _repairCheckType = 3;
        short _releaseCheckType = 2;
        short _checkType = 4;

       short[] _arrCheckType;

        string _componentCd = "ALL";
        GridColumn[] arrGridColumn;

        DataRowView _currentRow;

        BindingSource bs;
        BindingSource bsDetail;
        BindingSource _bsPallet;

        long _id;
        bool _headerButtonVisible = true;

        string _barcode = null;
        string _component = null;
        string _currentComponentCd = null;
        long _inventoryId = -1;
        long _componentId = -1;
        bool _isWarehousingCheckExist = false;
        bool _isRepairCheckExist = false;
        bool _isReleaseCheckExist = false;

        DateTime _warehousingDate = new DateTime();
        DataTable _dtPrintPort;
        DataTable _dtPGrade;
        DataTable _dtPallet;

        public usrQualityControl()
        {
            InitializeComponent();

            _arrCheckType = new short[] { _releaseCheckType, _repairCheckType, _examCheckType };

            arrGridColumn = new GridColumn[4] { gc1, gc2, gc3, gc4 };

            bs = new BindingSource();
            bsDetail = new BindingSource();
            _bsPallet = new BindingSource();

            bs.DataSource = ProjectInfo._dtDeviceInfo;
            //teRelease.EditValue = "O200923001";
            lcComponent.Text = _componentCd;

            Dangol.ShowSplash();
  
            if (ProjectInfo._dicProductList == null)
            {
                DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");

                if (rows.Length > 0)
                {
                    long inventoryId = ConvertUtil.ToInt64(rows[0]["INVENTORY_ID"]);
                    string barcode = ConvertUtil.ToString(rows[0]["BARCODE"]);
                    if (inventoryId > 0)
                    {
                        Util.checkProductState(inventoryId, barcode);
                        Util.getEtcComponent();
                        Util.checkProductRemainPart();
                    }
                    else
                        ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
                }
                else
                    ProjectInfo._dicProductList = new Dictionary<long, Dictionary<string, string>>();
            }

            //if (ProjectInfo._dicPartCheckRelease == null)
            getCheckInfoInit();

            setNtbControl();

            Dangol.CloseSplash();
        }


        private void getCheckInfoInit()
        {
            if (ProjectInfo._dicPartCheckQC == null)
                ProjectInfo._dicPartCheckQC = new Dictionary<long, Dictionary<string, int>>();
            else
                ProjectInfo._dicPartCheckQC.Clear();
            ProjectInfo._dicInventoryDesQC = new Dictionary<long, string>();
            ProjectInfo._dicInventoryGradeQC = new Dictionary<long, string>();
            ProjectInfo._dicMonSizeQC = new Dictionary<long, string>();


            DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");
            JObject jResult = new JObject();

            long mbdInventoryId = -1;

            foreach (DataRow row in rows)
            {
                long inventoryId = ConvertUtil.ToInt64(row["INVENTORY_ID"]);
                string componentCd = ConvertUtil.ToString(row["COMPONENT_CD"]);
                if (componentCd.Equals("MBD"))
                    mbdInventoryId = inventoryId;

                if (!ProjectInfo._dicPartCheckQC.ContainsKey(inventoryId))
                {
                    Dictionary<string, int> dicData = new Dictionary<string, int>();
                    ProjectInfo._dicInventoryDesQC.Add(inventoryId, "");
                    ProjectInfo._dicInventoryGradeQC.Add(inventoryId, "0");

                    if (componentCd.Equals("MON"))
                        ProjectInfo._dicMonSizeQC.Add(inventoryId, "");

                    if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                    {
                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("DES"))
                                    ProjectInfo._dicInventoryDesQC[inventoryId] = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._dicInventoryGradeQC[inventoryId] = x.Value.ToObject<string>();
                                else if (name.Equals("SIZE") && componentCd.Equals("MON"))
                                    ProjectInfo._dicMonSizeQC[inventoryId] = x.Value.ToObject<string>();
                                else
                                {
                                    int value = x.Value.ToObject<int>();

                                    if (!dicData.ContainsKey(name))
                                        dicData.Add(name, value);
                                }
                            }
                        }
                    }

                    ProjectInfo._dicPartCheckQC.Add(inventoryId, dicData);
                }
            }

            ProjectInfo._dicNtbCheckQC = new Dictionary<string, short>();

            ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;

            ProjectInfo._isExistNtbCheckQC = false;

            if (ProjectInfo._dicNtbCheckRelease == null)
            {
                ProjectInfo._dicNtbCheckRelease = new Dictionary<string, short>();

                ProjectInfo._dtNTBAdjustmentPrice.Rows[_releaseCheckType]["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    ProjectInfo._dtNTBAdjustmentPrice.Rows[_releaseCheckType][col] = 0;

                ProjectInfo._isExistNtbCheckRelease = false;
            }

            if (ProjectInfo._dicNtbCheckRepair == null)
            {
                ProjectInfo._dicNtbCheckRepair = new Dictionary<string, short>();

                ProjectInfo._dtNTBAdjustmentPrice.Rows[_repairCheckType]["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    ProjectInfo._dtNTBAdjustmentPrice.Rows[_repairCheckType][col] = 0;

                ProjectInfo._isExistNtbCheckRepair = false;
            }

            if (ProjectInfo._dicNtbCheckWarehousing == null)
            {
                ProjectInfo._dicNtbCheckWarehousing = new Dictionary<string, short>();

                ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType]["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType][col] = 0;

                ProjectInfo._isExistNtbCheckWarehousing = false;
            }            

 
            ProjectInfo._dicAllInOneCheckQC = new Dictionary<string, short>();
            ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType][col] = 0;

            ProjectInfo._isExistAllInOneCheckQC = false;

            if (ProjectInfo._dicAllInOneCheckRelease == null)
            {
                ProjectInfo._dicAllInOneCheckRepair = new Dictionary<string, short>();

                ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_releaseCheckType]["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                    ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_releaseCheckType][col] = 0;

                ProjectInfo._isExistAllInOneCheckRelease = false;
            }

            if (ProjectInfo._dicAllInOneCheckRepair == null)
            {
                ProjectInfo._dicAllInOneCheckRepair = new Dictionary<string, short>();

                ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_repairCheckType]["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                    ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_repairCheckType][col] = 0;

                ProjectInfo._isExistAllInOneCheckRepair = false;
            }

            if (ProjectInfo._dicAllInOneCheckWarehousing == null)
            {
                ProjectInfo._dicAllInOneCheckWarehousing = new Dictionary<string, short>();

                ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_examCheckType]["EXIST"] = false;
                foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                    ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_examCheckType][col] = 0;

                ProjectInfo._isExistAllInOneCheckWarehousing = false;
            }


            if (mbdInventoryId > 0)
            {
                if (ProjectInfo._type > 1)
                    setCheckInfo(ProjectInfo._type, mbdInventoryId);
                else
                {
                    setCheckInfo(2, mbdInventoryId);
                    setCheckInfo(3, mbdInventoryId);
                }
            }
        }

        private void getCheckInfo(long inventoryId, string componentCd)
        {
            JObject jResult = new JObject();

            if (!ProjectInfo._dicPartCheckQC.ContainsKey(inventoryId))
            {
                Dictionary<string, int> dicData = new Dictionary<string, int>();
                ProjectInfo._dicInventoryDesQC.Add(inventoryId, "");
                ProjectInfo._dicInventoryGradeQC.Add(inventoryId, "0");
                ProjectInfo._dicMonSizeQC.Add(inventoryId, "");

                if (DBConnect.getCheckInfo(inventoryId, componentCd, _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("DES"))
                                ProjectInfo._dicInventoryDesQC[inventoryId] = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._dicInventoryGradeQC[inventoryId] = x.Value.ToObject<string>();
                            else if (name.Equals("SIZE"))
                                ProjectInfo._dicMonSizeQC[inventoryId] = x.Value.ToObject<string>();
                            else
                            {
                                int value = x.Value.ToObject<int>();

                                if (!dicData.ContainsKey(name))
                                    dicData.Add(name, value);
                            }
                        }
                    }
                }

                ProjectInfo._dicPartCheckQC.Add(inventoryId, dicData);
            }

            if (componentCd.Equals("MBD"))
            {
                if (ProjectInfo._type > 1)
                    setCheckInfo(ProjectInfo._type, inventoryId);
                else
                {
                    setCheckInfo(2, inventoryId);
                    setCheckInfo(3, inventoryId);
                }
            }
        
        }

        private void getProductCheckInfoInit(long inventoryId)
        {
            if (inventoryId > 0)
            {
                if (ProjectInfo._type > 1)
                    setCheckInfo(ProjectInfo._type, inventoryId);
                else
                {
                    setCheckInfo(2, inventoryId);
                    setCheckInfo(3, inventoryId);
                }
            }
        }


        private void setCheckInfo(int checkTyep, long inventoryId)
        {
            JObject jResult = new JObject();

            _isWarehousingCheckExist = false;
            _isRepairCheckExist = false;
            _isReleaseCheckExist = false;

          

            if (checkTyep == 2)
            {
                ProjectInfo._isExistNtbCheckQC = false;
                ProjectInfo._isExistNtbCheckWarehousing = false;
                ProjectInfo._isExistNtbCheckRelease = false;
                ProjectInfo._isExistNtbCheckRepair = false;

                for (int i = 1; i < 5; i++)
                {
                    ProjectInfo._dtNTBAdjustmentPrice.Rows[i]["EXIST"] = false;
                    foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                        ProjectInfo._dtNTBAdjustmentPrice.Rows[i][col] = 0;
                }

                ProjectInfo._dicNtbCheckQC.Clear();

                ProjectInfo._caseDestroyDescriptionQC = "";
                ProjectInfo._batteryRemainQC = "";
                ProjectInfo._productGradeQC = "";

                if (DBConnect.getCheckInfo(inventoryId, "NTB", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES"))
                                ProjectInfo._caseDestroyDescriptionQC = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                ProjectInfo._batteryRemainQC = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._productGradeQC = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicNtbCheckQC.ContainsKey(name))
                                    ProjectInfo._dicNtbCheckQC.Add(name, value);
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                            ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }

                    ProjectInfo._isExistNtbCheckQC = true;
                }

                short NTBcheckType = -1;
                bool isExist = false;



                for (int i = 0; i < _arrCheckType.Length; i++)
                {
                    NTBcheckType = _arrCheckType[i];
                    if (DBConnect.getCheckInfo(inventoryId, "NTB", NTBcheckType, ref jResult))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist)
                {
                    if (NTBcheckType == 1)
                    {
                        _isWarehousingCheckExist = true;

                        ProjectInfo._dicNtbCheckWarehousing.Clear();
                        ProjectInfo._caseDestroyDescriptionWarehousing = "";
                        ProjectInfo._batteryRemainWarehousing = "";
                        ProjectInfo._productGradeWarehousing = "";

                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("CASE_DES"))
                                    ProjectInfo._caseDestroyDescriptionWarehousing = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN"))
                                    ProjectInfo._batteryRemainWarehousing = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._productGradeWarehousing = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicNtbCheckWarehousing.ContainsKey(name))
                                        ProjectInfo._dicNtbCheckWarehousing.Add(name, value);
                                }
                            }
                        }

                        ProjectInfo._isExistNtbCheckWarehousing = true;
                    }
                    else if (NTBcheckType == 2)
                    {
                        _isReleaseCheckExist = true;
                        _isWarehousingCheckExist = true;


                        ProjectInfo._dicNtbCheckRelease.Clear();

                        ProjectInfo._caseDestroyDescriptionRelease = "";
                        ProjectInfo._batteryRemainRelease = "";
                        ProjectInfo._productGradeRelease = "";

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
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._productGradeRelease = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicNtbCheckRelease.ContainsKey(name))
                                        ProjectInfo._dicNtbCheckRelease.Add(name, value);
                                }
                            }
                        }

                        ProjectInfo._isExistNtbCheckRelease = true;
                        ProjectInfo._isExistNtbCheckWarehousing = true;
                    }
                    else if (NTBcheckType == 3)
                    {
                        _isRepairCheckExist = true;
                        _isWarehousingCheckExist = true;

                        ProjectInfo._dicNtbCheckRepair.Clear();

                        ProjectInfo._caseDestroyDescriptionRepair = "";
                        ProjectInfo._batteryRemainRepair = "";
                        ProjectInfo._productGradeRepair = "";

                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("CASE_DES"))
                                    ProjectInfo._caseDestroyDescriptionRepair = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN"))
                                    ProjectInfo._batteryRemainRepair = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._productGradeRepair = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicNtbCheckRepair.ContainsKey(name))
                                        ProjectInfo._dicNtbCheckRepair.Add(name, value);
                                }
                            }
                        }

                        ProjectInfo._isExistNtbCheckRepair = true;
                        ProjectInfo._isExistNtbCheckWarehousing = true;
                    }

                    if (!ProjectInfo._isExistNtbCheckQC)
                    {
                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("CASE_DES"))
                                    ProjectInfo._caseDestroyDescriptionQC = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN"))
                                    ProjectInfo._batteryRemainQC = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._productGradeQC = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicNtbCheckQC.ContainsKey(name))
                                        ProjectInfo._dicNtbCheckQC.Add(name, value);
                                }
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtNTBAdjustmentPrice.Rows[NTBcheckType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                            ProjectInfo._dtNTBAdjustmentPrice.Rows[NTBcheckType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                        if (!ProjectInfo._isExistNtbCheckQC)
                        {
                            ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                            foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                                ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                        }
                    }
                }
            }
            else if (checkTyep == 3)
            {
                ProjectInfo._isExistAllInOneCheckQC = false;
                ProjectInfo._isExistAllInOneCheckWarehousing = false;
                ProjectInfo._isExistAllInOneCheckRelease = false;
                ProjectInfo._isExistAllInOneCheckRepair = false;

                ProjectInfo._allInOneProductGradeQC = "";
                ProjectInfo._etcQC = "";

                for (int i = 1; i < 5; i++)
                {
                    ProjectInfo._dtAllInOneAdjustmentPrice.Rows[i]["EXIST"] = false;
                    foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                        ProjectInfo._dtAllInOneAdjustmentPrice.Rows[i][col] = 0;
                }

                ProjectInfo._dicAllInOneCheckQC.Clear();

                if (DBConnect.getCheckInfo(inventoryId, "ALLINONE", _checkType, ref jResult))
                {
                    JObject jData = (JObject)jResult["DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("ETC_DES"))
                                ProjectInfo._etcQC = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                ProjectInfo._allInOneProductGradeQC = x.Value.ToObject<string>();
                            else
                            {
                                short value = x.Value.ToObject<short>();

                                if (!ProjectInfo._dicAllInOneCheckQC.ContainsKey(name))
                                    ProjectInfo._dicAllInOneCheckQC.Add(name, value);
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                            ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
                    }

                    ProjectInfo._isExistAllInOneCheckQC = true;
                }

                short AllInOnecheckType = -1;
                bool isExist = false;

                for (int i = 0; i < _arrCheckType.Length; i++)
                {
                    AllInOnecheckType = _arrCheckType[i];
                    if (DBConnect.getCheckInfo(inventoryId, "ALLINONE", AllInOnecheckType, ref jResult))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist)
                {
                    if (AllInOnecheckType == 1)
                    {
                        _isWarehousingCheckExist = true;

                        ProjectInfo._dicAllInOneCheckWarehousing.Clear();
                        ProjectInfo._etcWarehousing = "";
                        ProjectInfo._allInOneProductGradeWarehousing = "";

                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("ETC_DES"))
                                    ProjectInfo._etcWarehousing = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._allInOneProductGradeWarehousing = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicAllInOneCheckWarehousing.ContainsKey(name))
                                        ProjectInfo._dicAllInOneCheckWarehousing.Add(name, value);
                                }
                            }
                        }

                        ProjectInfo._isExistAllInOneCheckWarehousing = true;
                    }
                    else if (AllInOnecheckType == 2)
                    {
                        _isReleaseCheckExist = true;
                        _isWarehousingCheckExist = true;

                        ProjectInfo._dicAllInOneCheckRelease.Clear();
                        ProjectInfo._etcRelease = "";
                        ProjectInfo._allInOneProductGradeRelease = "";

                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("ETC_DES"))
                                    ProjectInfo._etcRelease = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._allInOneProductGradeRelease = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicAllInOneCheckRelease.ContainsKey(name))
                                        ProjectInfo._dicAllInOneCheckRelease.Add(name, value);
                                }
                            }
                        }

                        ProjectInfo._isExistAllInOneCheckRelease = true;
                        ProjectInfo._isExistAllInOneCheckWarehousing = true;
                    }
                    else if (AllInOnecheckType == 3)
                    {
                        _isRepairCheckExist = true;
                        _isWarehousingCheckExist = true;

                        ProjectInfo._dicAllInOneCheckRepair.Clear();
                        ProjectInfo._etcRepair = "";
                        ProjectInfo._allInOneProductGradeRepair = "";

                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("ETC_DES"))
                                    ProjectInfo._etcRepair = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._allInOneProductGradeRepair = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicAllInOneCheckRepair.ContainsKey(name))
                                        ProjectInfo._dicAllInOneCheckRepair.Add(name, value);
                                }
                            }
                        }

                        ProjectInfo._isExistAllInOneCheckRepair = true;
                        ProjectInfo._isExistAllInOneCheckWarehousing = true;
                    }

                    if (!ProjectInfo._isExistAllInOneCheckQC)
                    {
                        JObject jData = (JObject)jResult["DATA"];

                        foreach (var x in jData)
                        {
                            string name = x.Key;
                            if (!ProjectInfo._listCheckException.Contains(name))
                            {
                                if (name.Equals("ETC_DES"))
                                    ProjectInfo._etcQC = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    ProjectInfo._allInOneProductGradeQC = x.Value.ToObject<string>();
                                else
                                {
                                    short value = x.Value.ToObject<short>();

                                    if (!ProjectInfo._dicAllInOneCheckQC.ContainsKey(name))
                                        ProjectInfo._dicAllInOneCheckQC.Add(name, value);
                                }
                            }
                        }
                    }

                    if (ConvertUtil.ToBoolean(jResult["ADJUST_EXIST"]))
                    {
                        JObject jAdjustmentPriceData = (JObject)jResult["ADJUST_DATA"];

                        ProjectInfo._dtAllInOneAdjustmentPrice.Rows[AllInOnecheckType]["EXIST"] = true;
                        foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                            ProjectInfo._dtAllInOneAdjustmentPrice.Rows[AllInOnecheckType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);

                        if (!ProjectInfo._isExistAllInOneCheckQC)
                        {
                            ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = true;
                            foreach (string col in ExamineInfo._listAdjustmentAllInOnePriceCol)
                                ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(jAdjustmentPriceData[col]);
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
            //getWarehousing();

            teBarcode.DataBindings.Add(new Binding("Text", bs, "BARCODE", false, DataSourceUpdateMode.Never));
            teComponent.DataBindings.Add(new Binding("Text", bs, "COMPONENT", false, DataSourceUpdateMode.Never));
            teReleaseResult.DataBindings.Add(new Binding("Text", bs, "RELEASE_RESULT", false, DataSourceUpdateMode.Never));
            leLocation.DataBindings.Add(new Binding("EditValue", bs, "WAREHOUSE", false, DataSourceUpdateMode.Never));
            lePallet.DataBindings.Add(new Binding("EditValue", bs, "PALLET", false, DataSourceUpdateMode.Never));

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

            DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
            Util.LookupEditHelper(leLocation, dtLocation, "KEY", "VALUE");

            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            Util.LookupEditHelper(lePallet, _dtPallet, "PALLET_ID", "PALLET_NM");
            _bsPallet.DataSource = _dtPallet;

            DataTable dtNickName = Util.getCodeListCustomLS("TN_NTB_LIST", "NTB_LIST_ID", "NTB_NICKNAME", "USE_YN = 1", "DISPLAY_SEQ ASC, CREATE_DT ASC");
            Util.LookupEditHelper(leNickName, dtNickName, "KEY", "VALUE");

            DataTable dtManufactureType = new DataTable();

            dtManufactureType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtManufactureType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtManufactureType, 2, "외산/기타");
            Util.insertRowonTop(dtManufactureType, 1, "삼성/엘지");
            Util.insertRowonTop(dtManufactureType, -1, "알수없음");
            Util.LookupEditHelper(leManufactureType, dtManufactureType, "KEY", "VALUE");

            _dtPGrade = Util.getCodeList("CD0106", "KEY", "VALUE");
        }

        private void setIInitData()
        {
            // 기본 값 설정
            lePrintPort.EditValue = ProjectInfo._printerPort;
            //leLocation.EditValue = ProjectInfo._location;
            leProductType.EditValue = ProjectInfo._type;
            teUserName.EditValue = ProjectInfo._userName;
        }

        private void setNtbControl()
        {
            if (ProjectInfo._type == 2)
            {
                lcNickName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcManufactureType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcNickName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcManufactureType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            leNickName.EditValue = ProjectInfo._ntbListId;
            leManufactureType.EditValue = ProjectInfo._ntbManufactureType;
        }

        private void getWarehousing()
        {
            JObject jResult = new JObject();

            if (DBConnect.getRepNo(2, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        _representativeNo = $"{obj["REP_NO"]}";
                        teRelease.Text = _representativeNo;
                        break;
                    }
                }
            }
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

                _bsPallet.Filter = $"WAREHOUSE_ID = '{_currentRow["WAREHOUSE"]}'";

            }
            else
            {
                _currentRow = null;
                checkPictureState();
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
                int rowhandle = gvInventory.FocusedRowHandle;
                int topRowIndex = gvInventory.TopRowIndex;
                gvInventory.FocusedRowHandle = -1;
                gvInventory.FocusedRowHandle = rowhandle;

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
                int rowhandle = gvInventory.FocusedRowHandle;
                int topRowIndex = gvInventory.TopRowIndex;
                gvInventory.FocusedRowHandle = -1;
                gvInventory.FocusedRowHandle = rowhandle;

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
            getReleasePart();
        }
        private bool getReleasePart(bool isNeedMassage = true)
        { 
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return false;
            }

            Dangol.ShowSplash();

            JObject jResult = new JObject();


            if (ProjectInfo._listReleaseList == null)
                ProjectInfo._listReleaseList = new List<long>();
            else
                ProjectInfo._listReleaseList.Clear();

            if (DBConnect.getReleaseList(_representativeNo, ref jResult))
            {
                foreach (var x in jResult)
                {
                    string id = x.Key;

                    if (!id.Equals("SUCCESS"))
                    {  
                        long value = x.Value.ToObject<long>();

                        if (!ProjectInfo._listReleaseList.Contains(value))
                            ProjectInfo._listReleaseList.Add(value);
                    }
                }
            }
            else
            {
                Dangol.CloseSplash();
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                return false;
            }

            foreach (DataRow dr in ProjectInfo._dtDeviceInfo.Rows)
            {
                long id = ConvertUtil.ToInt64(dr["INVENTORY_ID"]);
                if (ProjectInfo._listReleaseList.Contains(id))
                {
                    dr["RELEASE_YN"] = true;
                    dr["RELEASE_RESULT"] = "정상 등록";
                }
                else
                {
                    dr["RELEASE_YN"] = false;
                    dr["RELEASE_RESULT"] = "";
                }
            }

            Dangol.CloseSplash();
            if (isNeedMassage)
                Dangol.Message("출고 부품 리스트를 구성하였습니다.");


            return true;
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

            Dangol.ShowSplash();

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
                Dangol.CloseSplash();
                Dangol.Message("메인보드가 체크되지 않았습니다.");
                return;
            }

            if (mbdInventoryId < 1)
            {
                Dangol.CloseSplash();
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return;
            }

            if (inventoryCnt < 2)
            {
                Dangol.CloseSplash();
                Dangol.Message("제품 구성은 2개 이상 부품으로 구성돼야 합니다.");
                return;
            }

            if (Dangol.MessageYN($"선택하신 부품들로 제품을 구성하시겠습니까?") == DialogResult.No)
            {
                Dangol.CloseSplash();
                return;
            }

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

                Dangol.CloseSplash();
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.CloseSplash();
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

            if(ProjectInfo._listReleaseList == null)
            {
                if (!getReleasePart(false))
                {
                    Dangol.Message("출고정보를 가져오는 과정에서 오류가 발생했습니다.");
                    return;
                }
            }

            Dangol.ShowSplash();

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
                    Dangol.CloseSplash();
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

                    Dangol.CloseSplash();

                    if (Convert.ToBoolean(jResult["COMPLETE"])) //
                        Dangol.Message("출고등록되었습니다.");
                    else
                        Dangol.Message("개별 출고 등록 결과를 확인하세요.");
                }
                else
                {
                    Dangol.CloseSplash();
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else
            {
                if(_inventoryId < 0)
                {
                    Dangol.CloseSplash();
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
                    Dangol.CloseSplash();

                    if (Convert.ToBoolean(jResult["COMPLETE"]))
                        Dangol.Message("출고등록되었습니다.");
                    else
                        Dangol.Message("개별 출고 등록 결과를 확인하세요.");
                }
                else
                {
                    Dangol.CloseSplash();
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"])+", "+ ConvertUtil.ToString(jResult["ERROR"]));
                }
            }

        }

        private bool checkRepresentativeInfo()
        {
            if(teRelease.EditValue == null)
                return false;
            else
             _representativeNo = teRelease.EditValue.ToString();

            if (string.IsNullOrWhiteSpace(_representativeNo))
                return false;
            else
                return true;
            
        }

        private void sbDeleteReleasePart_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return;
            }

            if (ProjectInfo._listReleaseList == null)
            {
                if (!getReleasePart(false))
                {
                    Dangol.Message("출고정보를 가져오는 과정에서 오류가 발생했습니다.");
                    return;
                }
            }

            Dangol.ShowSplash();

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
                    Dangol.CloseSplash();
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

                    if (!listInventoryId.Contains(inventoryId))
                        listInventoryId.Add(inventoryId);
                }

                if (listInventoryId.Count < 1)
                {
                    Dangol.CloseSplash();
                    Dangol.Message("등록된 부품이 없습니다.");
                    return;
                }

                if (DBConnect.deleteReleasePart(_representativeType, _representativeNo, _representativeCol, listInventoryId, ref jResult))
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
                            drCheck["RELEASE_YN"] = false;

                            if (ProjectInfo._listReleaseList.Contains(inventoryId))
                                ProjectInfo._listReleaseList.Remove(inventoryId);
                        }
                    }

                    Dangol.CloseSplash();

                    if (Convert.ToBoolean(jResult["COMPLETE"])) //
                        Dangol.Message("출고 해제되었습니다.");
                    else
                        Dangol.Message("개별 출고 해제 결과를 확인하세요.");
                }
                else
                {
                    Dangol.CloseSplash();

                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else
            {
                if (_inventoryId < 0)
                {
                    Dangol.CloseSplash();
                    Dangol.Message("등록되지 않은 재고입니다.");
                    return;
                }

                List<long> listInventoryId = new List<long>();
                JObject jResult = new JObject();

                listInventoryId.Add(_inventoryId);

                if (DBConnect.deleteReleasePart(_representativeType, _representativeNo, _representativeCol, listInventoryId, ref jResult))
                {
                    rowsSelect = ProjectInfo._dtDeviceInfo.Select($"ID = {_id}");

                    if (rowsSelect.Length > 0)
                    {
                        string sInventoryId = ConvertUtil.ToString(rowsSelect[0]["INVENTORY_ID"]);
                        string resultMsg = ConvertUtil.ToString(jResult[sInventoryId]);

                        _currentRow.BeginEdit();
                        _currentRow["RELEASE_RESULT"] = resultMsg;
                        _currentRow["RELEASE_YN"] = false;
                        _currentRow.EndEdit();

                        if (ProjectInfo._listReleaseList.Contains(_inventoryId))
                            ProjectInfo._listReleaseList.Remove(_inventoryId);
                    }

                    Dangol.CloseSplash();

                    if (Convert.ToBoolean(jResult["COMPLETE"]))
                        Dangol.Message("출고해제되었습니다.");
                    else
                        Dangol.Message("개별 출고 해제 결과를 확인하세요.");
                }
                else
                {
                    Dangol.CloseSplash();

                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }

        private void sbUpdateComponent_Click(object sender, EventArgs e)
        {
           
        }

        private void sbPrintProduct_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
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
                Dangol.Message("입고번호를 입력해주세요.");
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

            //_representativeNo = teRelease.EditValue.ToString();

            //if (!_isWarehousingCheckExist)
            //{
            //    if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
            //    {
            //        return;
            //    }
            //    _isWarehousingCheckExist = true;
            //}



            int checkValue = ConvertUtil.ToInt32(leProductType.EditValue);
           

            if (checkValue == 1)
            {
                Dangol.Message("PC는 개별 부품 체크만 가능합니다.");
                return;
            }

            JObject jResult = new JObject();

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

            if (ProjectInfo._isExistNtbCheckQC || ProjectInfo._isExistNtbCheckWarehousing || ProjectInfo._isExistNtbCheckRelease || ProjectInfo._isExistNtbCheckRepair
               || ProjectInfo._isExistAllInOneCheckQC  || ProjectInfo._isExistAllInOneCheckWarehousing || ProjectInfo._isExistAllInOneCheckRelease || ProjectInfo._isExistAllInOneCheckRepair)
            {
                if (DBConnect.getProductInfo(inventoryId, ref jResult))
                {
                    string approveType = ConvertUtil.ToString(jResult["APPROVE_TYPE"]);
                    int adjustmentState = ConvertUtil.ToInt32(jResult["ADJUSTMENT_STATE"]);
                    int produceState = ConvertUtil.ToInt32(jResult["PRODUCE_STATE"]);

                    //if (!string.IsNullOrWhiteSpace(approveType) && adjustmentState == 1)
                    //{
                    //    Dangol.Message("승인완료 & 정산대기인 제품은 검수를 변경할 수 없습니다.");
                    //    return;
                    //}

                    if(produceState > 1)
                    {
                        Dangol.Message("생산대기중인 제품만 검수가능합니다.");
                        return;
                    }

                }
            }

            if (ProjectInfo._type == 2)
            {
                if (ProjectInfo._ntbListId < 1)
                {
                    Dangol.Message("제품 사양을 선택해주세요");
                    return;
                }

                if (ProjectInfo._ntbManufactureType < 1)
                {
                    Dangol.Message("제조사를 선택해주세요");
                    return;
                }
              
                ProjectInfo._dicNTBAdjustmentPrice.Clear();
                if (DBConnect.getNTBAdjustmentPrice(_representativeNo, _representativeType, ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string col = "";
                    if (ProjectInfo._ntbManufactureType == 1)
                        col = "MAJOR_PRICE";
                    else
                        col = "ETC_PRICE";
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string key = ConvertUtil.ToString(obj["COL_NM"]);

                        if (!ProjectInfo._dicNTBAdjustmentPrice.ContainsKey(key))
                            ProjectInfo._dicNTBAdjustmentPrice.Add(key, ConvertUtil.ToInt64(obj[col]));
                    }
                }
            }
            else
            {
                ProjectInfo._dicAllInOneAdjustmentPrice.Clear();
            }


            ProjectInfo._printerPort = lePrintPort.EditValue.ToString();
            string checkText = leProductType.Text;
            if (ProjectInfo._type != checkValue)
            {
                if (Dangol.MessageYN($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{ProjectInfo._typeNm}, 선택:{checkText})", $"{checkText} 검수 체크") == DialogResult.No)
                    return;
            }

            

            row = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > 0");

            if (row.Length > 0)
            {
                //if (Dangol.MessageYN($"등록된 재고가 {row.Length}개 입니다. 검수를 진행 하시겠습니까?", $"{checkText} 검수 체크") == DialogResult.No)
                //    return;

                foreach (DataRow dr in row)
                    listInventoryId.Add(ConvertUtil.ToInt64(dr["INVENTORY_ID"]));
            }


            getProductCheckInfoInit(inventoryId);

            if (ProjectInfo._type == 2)
            {
                if (!ProjectInfo._isExistNtbCheckWarehousing)
                {
                    Dangol.Message("입고 검수 정보가 없습니다. 입고 검수 후 QC 검수를 수행해 주세요.");
                    return;
                }
            }
            else if (ProjectInfo._type == 3)
            {
                if (!ProjectInfo._isExistAllInOneCheckWarehousing)
                {
                    Dangol.Message("입고 검수 정보가 없습니다. 입고 검수 후 QC 검수를 수행해 주세요.");
                    return;
                }
            }

            if (checkValue == 2)
            {
                //if ($"{checkText} 검수 체크".Equals("B200806001") || $"{checkText} 검수 체크".Equals("B201030004") || _warehousingDate > Convert.ToDateTime("2020-11-11"))
                //{

                string dtStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Dictionary<string, short> dicNtbCheckLast = null;
                if (_isReleaseCheckExist)
                    dicNtbCheckLast = ProjectInfo._dicNtbCheckRelease;
                else if(_isRepairCheckExist)
                    dicNtbCheckLast = ProjectInfo._dicNtbCheckRepair;
                else 
                    dicNtbCheckLast = ProjectInfo._dicNtbCheckWarehousing;

                using (DlgNtb2ndEditionCheck2 ntbCheck = new DlgNtb2ndEditionCheck2(ProjectInfo._dicNtbCheckQC, dicNtbCheckLast, ref ProjectInfo._dtNTBAdjustmentPrice, ProjectInfo._dicNTBAdjustmentPrice, ProjectInfo._caseDestroyDescriptionQC, 
                    ProjectInfo._batteryRemainQC, ProjectInfo._productGradeQC, _dtPrintPort, _dtPGrade, _bsPallet, ConvertUtil.ToInt64(lePallet.EditValue), _checkType, _isRepairCheckExist))
                {
                    ntbCheck._isExistReleaseCheck = _isReleaseCheckExist;
                    ntbCheck._directoryNm = _representativeNo;
                    ntbCheck._fileNm = $"{inventoryId}";


                    if (ntbCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._caseDestroyDescriptionQC = ntbCheck._caseDestroyDescription;
                        ProjectInfo._batteryRemainQC = ntbCheck._batteryRemain;
                        ProjectInfo._productGradeQC = ntbCheck._pGrade;

                        //long warehouse = ConvertUtil.ToInt64(leLocation.EditValue);

                        //DBConnect.updatePallet(listInventoryId, warehouse, ntbCheck._palletNo);

                        //foreach (DataRow dr in row)
                        //{
                        //    if (ConvertUtil.ToInt64(dr["WAREHOUSE"]) == warehouse)
                        //    {
                        //        dr.BeginEdit();
                        //        dr["PALLET"] = $"{ntbCheck._palletNo}";
                        //        dr.EndEdit();
                        //    }
                        //}

                        string dtEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        DBConnect.insertNtbCheck(_representativeType, _representativeNo, _representativeCol, dtStart, dtEnd, barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckQC, listInventoryId, ProjectInfo._caseDestroyDescriptionQC, ProjectInfo._batteryRemainQC, ProjectInfo._productGradeQC);

     
                        JObject jobj = new JObject();

                        jobj.Add("BULK_YN", 0);
                        jobj.Add("UPDATE_ONE", 1);
                        jobj.Add("INVENTORY_ID", inventoryId);
                        jobj.Add("CAPTURE_YN", ntbCheck._isCapture?1:0);
                        jobj.Add("QC_STATE", ntbCheck._QCState);

                        DBConnect.updateProductInfo(jobj, ref jResult);
                        

                        if (!DBAdjustment.insertAdjustmentPrice(inventoryId, _checkType, ref jResult))
                        {
                            Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        }

                        if (ntbCheck._isPrint) {
                            //JObject jResult = new JObject();
                            string port = ntbCheck._port.Equals("-1") ? lePrintPort.EditValue.ToString() : ntbCheck._port;
                            if (DBConnect.printNtbProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, port, ref jResult))
                            {
                                Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                            }
                            else
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }

                            //DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckRelease, ProjectInfo._caseDestroyDescriptionRelease, ProjectInfo._batteryRemainRelease, ProjectInfo._productGradeRelease, ProjectInfo._printerPort);
                        }

                        ProjectInfo._isExistNtbCheckQC = true;
                    }
                }
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
                Dangol.Message("개발중입니다. 관리자에게 문의하세요.");
                return;

                using (DlgAllInOneCheck AllInOneCheck = new DlgAllInOneCheck(ProjectInfo._dicAllInOneCheckRelease, ProjectInfo._dicAllInOneCheckWarehousing, ref ProjectInfo._dtAllInOneAdjustmentPrice, ProjectInfo._dicAllInOneAdjustmentPrice,
                    ProjectInfo._etcWarehousing,ProjectInfo._allInOneProductGradeWarehousing, _dtPrintPort, _dtPGrade, _bsPallet, ConvertUtil.ToInt64(lePallet.EditValue), _checkType))
                {
                    if (AllInOneCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._etcRelease = AllInOneCheck._etcDes;
                        ProjectInfo._allInOneProductGradeRelease = AllInOneCheck._pGrade;

                        //DBConnect.updatePallet(listInventoryId, warehouse, AllInOneCheck._palletNo);

                        //foreach (DataRow dr in row)
                        //{
                        //    if (ConvertUtil.ToInt64(dr["WAREHOUSE"]) == warehouse)
                        //    {
                        //        dr.BeginEdit();
                        //        dr["PALLET"] = $"{AllInOneCheck._palletNo}";
                        //        dr.EndEdit();
                        //    }
                        //}

                        if (DBConnect.insertAllInOneCheck(_representativeType, _representativeNo, _representativeCol, barcode, inventoryId, _checkType, ProjectInfo._dicAllInOneCheckRelease, listInventoryId, ProjectInfo._etcRelease, ProjectInfo._allInOneProductGradeRelease))
                        {
                            //JObject jResult = new JObject();

                            if (!DBAdjustment.insertAdjustmentAllInOnePrice(inventoryId, _checkType, ref jResult))
                            {
                                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                            }

                            if (AllInOneCheck._isPrint)
                            {
                                if (DBConnect.printAllInOneProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, _checkType, lePrintPort.EditValue.ToString(), ref jResult))
                                {
                                    Dangol.Message("제품정보와 검수 정보가 출력되었습니다");
                                }
                                else
                                {
                                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                                }

                                //DBConnect.printNtbCheck(barcode, inventoryId, _checkType, ProjectInfo._dicNtbCheckWarehousing, ProjectInfo._caseDestroyDescriptionWarehousing, ProjectInfo._batteryRemainWarehousing, ProjectInfo._productGradeWarehousing, ProjectInfo._printerPort);
                            }

                            ProjectInfo._isExistAllInOneCheckRelease = true;
                        }
                    }
                }
            }
        }

        private void sbPartCheck_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return;
            }

            if (!_isWarehousingCheckExist)
            {
                if (!DBConnect.getWarehousingInfo(_representativeNo, ref _warehousingDate))
                {
                    return;
                }
                _isWarehousingCheckExist = true;
            }

            if (string.IsNullOrEmpty(_barcode) || _inventoryId < 0)
            {
                Dangol.Message("선택하신 부품은 재고로 등록되지 않았습니다.");
                return;
            }

            if (!ProjectInfo._dicPartCheckRelease.ContainsKey(_inventoryId))
                getCheckInfo(_inventoryId, _currentComponentCd);

            Dictionary<string, int> dicCheckInfo = ProjectInfo._dicPartCheckRelease[_inventoryId];

            if (_currentComponentCd.Equals("MON"))
            {
                string size = ProjectInfo._dicMonSizeRelease[_inventoryId];

                using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(dicCheckInfo,
                    ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                     ProjectInfo._dicMonSizeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (monitorCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = monitorCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = monitorCheck._pGrade;
                        ProjectInfo._dicMonSizeRelease[_inventoryId] = monitorCheck._size;

                        DBConnect.insertMonitorCheck(_barcode, _checkType, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._dicMonSizeRelease[_inventoryId]);

                        if (monitorCheck._isPrint)
                            DBConnect.printMonitorCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._dicMonSizeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("CAS"))
            {
                using (DlgCasCheck casCheck = new DlgCasCheck(dicCheckInfo,
                    ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (casCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = casCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = casCheck._pGrade;

                        DBConnect.insertCasCheck(_barcode, _checkType, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (casCheck._isPrint)
                            DBConnect.printCasCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("CPU"))
            {
                using (DlgCpuCheck inventoryCheck = new DlgCpuCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("MEM"))
            {
                using (DlgMEMCheck inventoryCheck = new DlgMEMCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("MBD"))
            {
                using (DlgMbdCheck inventoryCheck = new DlgMbdCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("VGA"))
            {
                using (DlgVgaCheck inventoryCheck = new DlgVgaCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("STG"))
            {
                string type = ConvertUtil.ToString(_currentRow["STG_TYPE"]);

                if (type.Contains("SSD"))
                {
                    using (DlgSsdCheck inventoryCheck = new DlgSsdCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                    {
                        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                            ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                            if (inventoryCheck._isPrint)
                                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                        }
                    }
                }
                else
                {
                    using (DlgHddCheck inventoryCheck = new DlgHddCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                    {
                        if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                        {
                            ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                            ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                            DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                            if (inventoryCheck._isPrint)
                                DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                        }
                    }
                }
            }
            else if (_currentComponentCd.Equals("ODD"))
            {
                using (DlgOddCheck inventoryCheck = new DlgOddCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else if (_currentComponentCd.Equals("POW"))
            {
                using (DlgPowCheck inventoryCheck = new DlgPowCheck(dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId],
                    ProjectInfo._dicInventoryGradeRelease[_inventoryId],
                    _dtPrintPort,
                    _dtPGrade
                    ))
                {
                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._dicInventoryDesRelease[_inventoryId] = inventoryCheck._description;
                        ProjectInfo._dicInventoryGradeRelease[_inventoryId] = inventoryCheck._pGrade;

                        DBConnect.insertInventoryCheck(_barcode, _checkType, _currentComponentCd, dicCheckInfo, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId]);

                        if (inventoryCheck._isPrint)
                            DBConnect.printInventoryCheck(_barcode, _checkType, dicCheckInfo, _currentComponentCd, ProjectInfo._dicInventoryDesRelease[_inventoryId], ProjectInfo._dicInventoryGradeRelease[_inventoryId], ProjectInfo._printerPort);
                    }
                }
            }
            else
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                return;
            }
        }

        private void sbClear_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN($"관리번호를 초기화하시겠습니까?") == DialogResult.No)
                return;

            DataTable dtComponentInfo = ProjectInfo._dicDeviceInfo[_currentComponentCd];
            DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
            JObject jResult = new JObject();


            if (rowDeviceInfo.Length > 0)
            {
                ProjectInfo._dicPartCheckRelease.Remove(_inventoryId);
                ProjectInfo._dicInventoryDesRelease.Remove(_inventoryId);
                ProjectInfo._dicInventoryGradeRelease.Remove(_inventoryId);

                if (_currentComponentCd.Equals("MON"))
                    ProjectInfo._dicMonSizeRelease.Remove(_inventoryId);

                if (_currentComponentCd.Equals("MBD"))
                {
                    ProjectInfo._dicNtbCheckWarehousing.Clear();
                    ProjectInfo._dicNtbCheckRelease.Clear();

                    ProjectInfo._dicAllInOneCheckWarehousing.Clear();
                    ProjectInfo._dicAllInOneCheckRelease.Clear();

                    ProjectInfo._caseDestroyDescriptionWarehousing = "";
                    ProjectInfo._batteryRemainWarehousing = "";
                    ProjectInfo._productGradeWarehousing = "";

                    ProjectInfo._caseDestroyDescriptionRelease = "";
                    ProjectInfo._batteryRemainRelease = "";
                    ProjectInfo._productGradeRelease = "";

                    ProjectInfo._etcWarehousing = "";
                    ProjectInfo._allInOneProductGradeWarehousing = "";

                    ProjectInfo._etcRelease = "";
                    ProjectInfo._allInOneProductGradeRelease = "";

                    ProjectInfo._isExistNtbCheckWarehousing = false;
                    ProjectInfo._isExistNtbCheckRelease = false;

                    ProjectInfo._isExistAllInOneCheckWarehousing = false;
                    ProjectInfo._isExistAllInOneCheckRelease = false;

                    ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
                    ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType]["EXIST"] = false;
                    foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    {
                        ProjectInfo._dtNTBAdjustmentPrice.Rows[_checkType][col] = 0;
                        ProjectInfo._dtNTBAdjustmentPrice.Rows[_examCheckType][col] = 0;
                    }

                    ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType]["EXIST"] = false;
                    ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_examCheckType]["EXIST"] = false;
                    foreach (string col in ExamineInfo._listAdjustmentPriceCol)
                    {
                        ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_checkType][col] = 0;
                        ProjectInfo._dtAllInOneAdjustmentPrice.Rows[_examCheckType][col] = 0;
                    }

                    foreach (DataRow dr in ProjectInfo._dtDeviceInfo.Rows)
                        dr["PRODUCT_YN"] = false;
                }

                _currentRow.BeginEdit();

                _currentRow["INVENTORY_ID"] = dtComponentInfo.Rows[0]["INVENTORY_ID"] = -1;
                _currentRow["BARCODE"] = dtComponentInfo.Rows[0]["BARCODE"] = "";
                _currentRow["INVENTORY_YN"] = false;
                _currentRow["PRODUCT_YN"] = false;
                _currentRow["RELEASE_YN"] = false;
                _currentRow["RELEASE_RESULT"] = "";

                _currentRow.EndEdit();

                _barcode = "";
                _inventoryId = -1;

                setBarcodeButton(false, false, true);
                checkPictureState();
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
            DataRow[] rowDeviceInfo = dtComponentInfo.Select($"ID = {_id}");
            JObject jResult = new JObject();

            if (rowDeviceInfo.Length > 0)
            {
                if (DBConnect.getInventory(barcode, _currentComponentCd, rowDeviceInfo[0], ref jResult))
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
                    _currentRow["INVENTORY_YN"] = true;

                    DataTable dtDeviceInfoFromDB = ProjectInfo._dicDeviceInfoDetail[_id];
                    string specNm;
                    foreach (DataRow dr in dtDeviceInfoFromDB.Rows)
                    {
                        dr.BeginEdit();
                        specNm = ConvertUtil.ToString(dr["SPEC_NM"]);
                        if (jData.ContainsKey(specNm))
                        {
                            dr["MATCHING_INFO"] = ConvertUtil.ToString(jData[specNm]);
                        }
                        else
                            dr["MATCHING_INFO"] = dr["DEVICE_INFO"];
                        dr.EndEdit();
                    }

                    gcInventoryDetail.DataSource = null;
                    bsDetail.DataSource = dtDeviceInfoFromDB;
                    gcInventoryDetail.DataSource = bsDetail;
                    

                    _barcode = ConvertUtil.ToString(jData["BARCODE"]);
                    _inventoryId = ConvertUtil.ToInt64(jData["INVENTORY_ID"]);

                    getCheckInfo(_inventoryId, _currentComponentCd);

                    setBarcodeButton(true, true, false);

                    if (_currentComponentCd.Equals("MBD"))
                    {
                        Util.checkProductState(_inventoryId, _barcode);
                        Util.getEtcComponent();
                        Util.checkProductRemainPart();

                        setNtbControl();
                    }
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
                    checkPictureState();

                    Dangol.Message("부품 정보를 변경하였습니다.");
                }
                else
                {
                    checkPictureState();
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
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

            setNtbControl();
        }

        private void leLocation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
        }

        private void sbProductDeleteReleasePart_Click(object sender, EventArgs e)
        {
            if (!checkRepresentativeInfo())
            {
                Dangol.Message("출고번호를 입력해주세요.");
                return;
            }

            if (ProjectInfo._listReleaseList == null)
            {
                if (!getReleasePart(false))
                {
                    Dangol.Message("출고정보를 가져오는 과정에서 오류가 발생했습니다.");
                    return;
                }
            } 

            DataRow[] rows = ProjectInfo._dtDeviceInfo.Select($"COMPONENT_CD = 'MBD'");

            if(rows.Length < 1)
            {
                Dangol.Message("메인보드가 없습니다.");
                return;
            }
            DataRow drMbd = rows[0];

            long inventoryId = ConvertUtil.ToInt64(drMbd["INVENTORY_ID"]);
                
            if (inventoryId < 0)                  
            {
                Dangol.Message("메인보드 관리번호가 없습니다.");
                return;
            }


            Dangol.ShowSplash();
            DataRow[] rowsSelect;
            JObject jResult = new JObject();

            if (DBConnect.deleteReleaseProduct(_representativeType, _representativeNo, _representativeCol, inventoryId, ref jResult))
            {
                //리턴 받은 부품들 상태 update
                DataRow[] rowCheck = ProjectInfo._dtDeviceInfo.Select($"INVENTORY_ID > -1");

                foreach (DataRow drCheck in rowCheck)
                {
                   long id = ConvertUtil.ToInt64(drCheck["ID"]);
                    rowsSelect = ProjectInfo._dtDeviceInfo.Select($"ID = {id}");

                    if (rowsSelect.Length > 0)
                    {
                        string sInventoryId = ConvertUtil.ToString(rowsSelect[0]["INVENTORY_ID"]);
                        string resultMsg = ConvertUtil.ToString(jResult[sInventoryId]);

                        if(string.IsNullOrWhiteSpace(resultMsg))
                        {
                            drCheck["RELEASE_RESULT"] = resultMsg;
                            drCheck["RELEASE_YN"] = false;
                        }
                        else
                        {
                            drCheck["RELEASE_RESULT"] = resultMsg;
                            drCheck["RELEASE_YN"] = false;
                        }
                       

                        if (ProjectInfo._listReleaseList.Contains(inventoryId))
                            ProjectInfo._listReleaseList.Remove(inventoryId);
                    }
                }

                Dangol.CloseSplash();

                if (Convert.ToBoolean(jResult["COMPLETE"])) //
                    Dangol.Message("출고 해제되었습니다.");
                else
                    Dangol.Message("개별 출고 해제 결과를 확인하세요.");
            }
            else
            {
                Dangol.CloseSplash();

                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            
        }

        private void leNickName_EditValueChanged(object sender, EventArgs e)
        {
            ProjectInfo._ntbListId = ConvertUtil.ToInt64(leNickName.EditValue);
        }

        private void leManufactureType_EditValueChanged(object sender, EventArgs e)
        {
            ProjectInfo._ntbManufactureType = ConvertUtil.ToInt32(leManufactureType.EditValue);
        }

        private void checkPictureState()
        {
            if (_currentRow == null)
            {
                lcCheckPicture.Enabled = false;
                lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                if (ConvertUtil.ToBoolean(_currentRow["INVENTORY_YN"]))
                {
                    lcCheckPicture.Enabled = true;

                    if (_currentComponentCd.Equals("MBD") && ConvertUtil.ToBoolean(_currentRow["PRODUCT_YN"]))
                        lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    else
                        lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    lcCheckPicture.Enabled = false;
                    lcCheckProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }

        }

        private void sbCheckPicture_Click(object sender, EventArgs e)
        {
            if (ConvertUtil.ToBoolean(_currentRow["INVENTORY_YN"]))
            {
                ImageInfo.GetImage(1, false, _barcode);
            }
        }

        private void sbCheckProductPicture_Click(object sender, EventArgs e)
        {
            if (ConvertUtil.ToBoolean(_currentRow["INVENTORY_YN"]) && _currentComponentCd.Equals("MBD") && ConvertUtil.ToBoolean(_currentRow["PRODUCT_YN"]))
            {
                ImageInfo.GetImage(1, true, _barcode);
            }
        }
    }
}