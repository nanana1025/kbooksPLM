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
using DevExpress.XtraGrid.Columns;
using Enum = WareHousingMaster.view.common.Enum;
using System.Text.RegularExpressions;
using ScreenCopy;
using System.IO;
using WareHousingMaster.UtilTest;

namespace WareHousingMaster.view.release.External
{
    public partial class usrReleaseCheck : DevExpress.XtraEditors.XtraForm
    {
        string _representativeType = "O";
        string _representativeCol = "RELEASES";
        string _representativeIdCol = "RELEASE_ID";

        public DataTable _dtProductInfo;

        Dictionary<string, short> _dicProductCheck = null;
        Dictionary<string, short> _dicProductCheckHistory = null;

        Dictionary<string, CheckEdit[]> _dicCheckEditList = null;

        Dictionary<string, Label> _dicCheckLabel = null;

        List<string> _caseExceptionCol;

        GridColumn[] arrGridColumn;
        DataRowView _currentRow;

        BindingSource _bs;

        short _checkType = 5;

        string _etcDes = "";
        string _batteryRemain = "";
        string _productGrade = "";

        string _etcDesHistory = "";
        string _batteryRemainHistory = "";
        string _productGradeHistory = "";

        string _componentCd = "ALL";

        string _barcode;
        long _inventoryId;
        string _produce;
        int _captureYn;
        int _wifiYn;
        string _camModelNm;
        double _battery;
        string _osName;


        string _devBarcode = null;
        string _devComponent = null;
        string _currentComponentCd = null;
        long _devInventoryId = -1;
        long _devComponentId = -1;
        bool _exist;
        bool _isGetData;

        string _dtStart;
        string _dtEnd;

        Regex regex1;
        Regex regex2;
        Regex regex3;

        Regex regex4;

        Image _image;


        public usrReleaseCheck()
        {
            InitializeComponent();

            arrGridColumn = new GridColumn[4] { gc1, gc2, gc3, gc4 };

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

            regex4 = new Regex(@"^E[0-9]{9}$");

            _dicProductCheck = new Dictionary<string, short>();
            _dicProductCheckHistory = new Dictionary<string, short>();

         
            _bs = new BindingSource();

            lcComponent.Text = _componentCd;

            _exist = false;
            _isGetData = false;
            _barcode = "";

            _dicCheckEditList = new Dictionary<string, CheckEdit[]>()
            {
                {"CASE_DESTROYED", new  CheckEdit[]{ ceCaseDestroyed1 , ceCaseDestroyed2 , ceCaseDestroyed3 , ceCaseDestroyed4 } },
                {"CASE_SCRATCH", new  CheckEdit[]{ ceCaseScratch1, ceCaseScratch2, ceCaseScratch3, ceCaseScratch4 } },
                {"CASE_STABBED", new  CheckEdit[]{ ceCaseStabbed1, ceCaseStabbed2, ceCaseStabbed3, ceCaseStabbed4 } },
                {"CASE_PRESSED", new  CheckEdit[]{ ceCasePressed1, ceCasePressed2, ceCasePressed3, ceCasePressed4 } },
                {"CASE_DISCOLORED", new  CheckEdit[]{ ceCaseDiscolored1, ceCaseDiscolored2, ceCaseDiscolored3, ceCaseDiscolored4 } },
                {"CASE_HINGE", new  CheckEdit[]{ ceCaseHinge } },
                {"COOLER", new  CheckEdit[]{ ceCooler} },
                {"DISPLAY", new  CheckEdit[]{ ceDisplay1, ceDisplay2, ceDisplay3, ceDisplay4, ceDisplay5, ceDisplay6, ceDisplay7, ceDisplay8, ceDisplay9 } },
                {"USB", new  CheckEdit[]{ ceUsb1, ceUsb2} },
                {"MOUSEPAD", new  CheckEdit[]{ ceMousePad1, ceMousePad2, ceMousePad3} },
                {"KEYBOARD", new  CheckEdit[]{ ceKeyboard1, ceKeyboard2, ceKeyboard3, ceKeyboard4, ceKeyboard5 } },
                {"BATTERY", new  CheckEdit[]{ ceBattery1, ceBattery2, ceBattery3, ceBattery4, ceBattery5 } },
                {"CAM", new  CheckEdit[]{ ceCam1, ceCam2, ceCam3} },
                {"ODD", new  CheckEdit[]{ ceOdd1, ceOdd2, ceOdd3, ceOdd4, ceOdd5, ceOdd6 } },

                {"LAN_WIRELESS", new  CheckEdit[]{ ceLanWireless1, ceLanWireless2} },
                {"LAN_WIRED", new  CheckEdit[]{ ceLanWired1, ceLanWired2} },
                {"HDD", new  CheckEdit[]{ ceHdd1, ceHdd2, ceHdd3, ceHdd4, ceHdd5 } },
                {"BIOS", new  CheckEdit[]{ ceBios1, ceBios2 } },
                {"OS", new  CheckEdit[]{ ceOs1} },
                {"TEST_CHECK", new  CheckEdit[]{ ceCheck1, ceCheck2, ceCheck3, ceCheck4 } },
                {"SPEAKER", new  CheckEdit[]{ ceSpeaker1} },
                {"OVERHEAT", new  CheckEdit[]{ ceOverheat1} },
                {"SHUTDOWN", new  CheckEdit[]{ ceShutdown1} }
            };

            _dicCheckLabel = new Dictionary<string, Label>()
            {
                {"CASE_DESTROYED", lbCase},
                {"CASE_SCRATCH", lbCase},
                {"CASE_STABBED", lbCase},
                {"CASE_PRESSED", lbCase},
                {"CASE_DISCOLORED", lbCase},
                {"CASE_HINGE", lbCase},
                {"COOLER", lbCase },
                {"DISPLAY", lbDisplay},
                {"USB", lbUsb},
                {"MOUSEPAD", lbMousePad},
                {"KEYBOARD", lbKeyboard},
                {"BATTERY", lbBattery},
                {"CAM", lbCam},
                {"ODD", lbOdd},

                {"LAN_WIRELESS", lbLanWireless},
                {"LAN_WIRED", lbLanWired},
                {"HDD", lbHdd},
                {"BIOS",lbBios},
                {"OS", lbOs},
                {"TEST_CHECK", lbCheck},
                {"SPEAKER", lbSpeaker},
                {"OVERHEAT", lbOverheat},
                {"SHUTDOWN", lbShutdown}
            };

            _caseExceptionCol = new List<string>(new string[] { "CASE_DESTROYED", "CASE_SCRATCH", "CASE_STABBED", "CASE_PRESSED", "CASE_DISCOLORED", "CASE_HINGE", "COOLER" });

            _dtProductInfo = new DataTable();
            _dtProductInfo.Columns.Add(new DataColumn("ID", typeof(int)));
            _dtProductInfo.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtProductInfo.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtProductInfo.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("WAREHOUSE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("PALLET", typeof(string)));

            _dtProductInfo.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtProductInfo.Columns.Add(new DataColumn("NO1", typeof(int)));
            _dtProductInfo.Columns.Add(new DataColumn("NAME", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("DATA1", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("DATA2", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("DATA3", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("DATA4", typeof(string)));

            _dtProductInfo.Columns.Add(new DataColumn("RELEASE_RESULT", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtProductInfo.Columns.Add(new DataColumn("INVENTORY_YN", typeof(bool)));
            _dtProductInfo.Columns.Add(new DataColumn("PRODUCT_YN", typeof(bool)));
            _dtProductInfo.Columns.Add(new DataColumn("RELEASE_YN", typeof(bool)));
            
            _dtProductInfo.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("SOCKET_NM", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("CODE_NM", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("SB_NM", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("MEM_TYPE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("MEM_SIZE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("PRODUCT_NAME", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("BANDWIDTH", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("CAPACITY", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("CAPACITY_M", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("VOLTAGE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("STG_TYPE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("BUS_TYPE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("SPEED", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("SIZE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("RESOLUTION", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("POW_CAT", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("POW_TYPE", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("POW_CLASS", typeof(string)));
            _dtProductInfo.Columns.Add(new DataColumn("TYPE", typeof(string)));
            
            _dtProductInfo.Columns.Add(new DataColumn("ASSIGN_COMPONENT_ID", typeof(long)));

        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();

            if (ProjectInfo._getComponentInformationYn == 1)
            {
                setGridControl();

                DataRow[] rows = ProjectInfo._dtDeviceInfo.Select("COMPONENT_CD = 'MBD'");

                if(rows.Length > 0)
                {
                    string barcode = ConvertUtil.ToString(rows[0]["BARCODE"]);
                    teBarcode.Text = barcode;
                    _barcode = barcode;
                }

                if (ProjectInfo._wifiYn == 1) ceWifi.Checked = true;
                else ceWifi.Checked = false;

                if (!string.IsNullOrWhiteSpace(ProjectInfo._camModelNm)) ceCam.Checked = true;
                else ceCam.Checked = false;

                teBattery.Text = $"{ ProjectInfo._batteryRemain.ToString("F0")}";
                teOsInfo.Text = ProjectInfo._osName;
            }
        }

        private void setInfoBox()
        {
            _bs.DataSource = ProjectInfo._dtDeviceInfo;
            gcInventory.DataSource = _bs;
        }

        private void setIInitData()
        {
           
        }

        private void getCheckInfo(string export, string barcode)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            string typeNm = "NTB";
            _dicProductCheck.Clear();
            _etcDes = "";
            _batteryRemain = "";
            _productGrade = "";

            _dicProductCheckHistory.Clear();
            _etcDesHistory = "";
            _batteryRemainHistory = "";
            _productGradeHistory = "";
            _inventoryId = -1;
            _produce = "-1";
            _captureYn = 0;

            jobj.Add("EXPORT", export);
            jobj.Add("BARCODE", barcode);
            jobj.Add("PRODUCE_TYPE", typeNm);
            jobj.Add("CHECK_TYPE", _checkType);
            if (ProjectInfo._userCompanyId != 2)
                jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            if (DBRelease.getExportCheckInfo(jobj, ref jResult))
            {
                bool isCheckExist = false;
                if (Convert.ToBoolean(jResult["CHECK_EXIST"]))
                {
                    isCheckExist = true;
                    JObject jData = (JObject)jResult["CHECK_DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES") || name.Equals("DES") || name.Equals("STG_DES") || name.Equals("REPAIR_DES")) 
                                 _etcDes = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                _batteryRemain = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                _productGrade = x.Value.ToObject<string>();
                            else
                            {
                                short value = ConvertUtil.ToInt16(x.Value);
                                //short value = ConvertUtil.ToInt16(x.Value.ToObject<short>());

                                if (!_dicProductCheck.ContainsKey(name))
                                    _dicProductCheck.Add(name, value);
                            }
                        }
                    }
                }


                if (Convert.ToBoolean(jResult["HISTORY_EXIST"]))
                {
                    JObject jData = (JObject)jResult["HISTORY_DATA"];

                    foreach (var x in jData)
                    {
                        string name = x.Key;
                        if (!ProjectInfo._listCheckException.Contains(name))
                        {
                            if (name.Equals("CASE_DES") || name.Equals("DES") || name.Equals("STG_DES") || name.Equals("REPAIR_DES"))
                                _etcDesHistory = x.Value.ToObject<string>();
                            else if (name.Equals("BATTERY_REMAIN"))
                                _batteryRemainHistory = x.Value.ToObject<string>();
                            else if (name.Equals("PRODUCT_GRADE"))
                                _productGradeHistory = x.Value.ToObject<string>();
                            else
                            {
                                short value = ConvertUtil.ToInt16(x.Value);
                                //short value = ConvertUtil.ToInt16(x.Value.ToObject<short>());

                                if (!_dicProductCheckHistory.ContainsKey(name))
                                    _dicProductCheckHistory.Add(name, value);
                            }

                            if(!isCheckExist)
                            {
                                if (name.Equals("CASE_DES") || name.Equals("DES") || name.Equals("STG_DES") || name.Equals("REPAIR_DES"))
                                    _etcDes = x.Value.ToObject<string>();
                                else if (name.Equals("BATTERY_REMAIN"))
                                    _batteryRemain = x.Value.ToObject<string>();
                                else if (name.Equals("PRODUCT_GRADE"))
                                    _productGrade = x.Value.ToObject<string>();
                            }
                        }
                    }
                }

                _wifiYn = ConvertUtil.ToInt32(jResult["WIFI"]);
                _camModelNm = ConvertUtil.ToString(jResult["CAM"]);
                _battery = ConvertUtil.ToDouble(jResult["BATTERY"]);
                _osName = ConvertUtil.ToString(jResult["OS_NAME"]);

                _inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);
                _produce = ConvertUtil.ToString(jResult["PRODUCE"]);
                _captureYn = ConvertUtil.ToInt32(jResult["CAPTURE_YN"]);

                if (_battery > 100.0)
                    _battery = 100;

                _battery = Math.Round(_battery);
            }
        }

        private void getCheckData(bool refresh = false)
        {
            Dangol.ShowSplash();
            
            InitCheck();

            if (refresh)
            {
                getCheckInfo("", _barcode);

                if (true)
                {
                    setInitCheckData();
                    setInitHistoryCheckData();
                    // getCaptureImage();
                    
                }

                Dangol.CloseSplash();
            }
            else 
            { 

                string export = teExport.Text.ToUpper().Trim();
                string barcode = teBarcode.Text.ToUpper().Trim();

                if (barcode.Length < 1 || string.IsNullOrWhiteSpace(barcode))
                    return;

                if (barcode.Length == 12 && (regex1.IsMatch(barcode) || regex2.IsMatch(barcode) || regex3.IsMatch(barcode)))
                {

                    JObject jResult = new JObject();
                    JObject jData = new JObject();
                    jData.Add("BARCODE", barcode);
                    if (ProjectInfo._userCompanyId != 2)
                        jData.Add("COMPANY_ID", ProjectInfo._userCompanyId);


                    if (DBRelease.getExportInventoryInfo(jData, ref jResult))
                    {
                        if (ConvertUtil.ToBoolean(jResult["EXIST"]))
                        {

                            if (ConvertUtil.ToInt32(jResult["PRODUCT_YN"]) == 1)
                            {

                                _wifiYn = 0;
                                _camModelNm = "";
                                _battery = 0;
                                _osName = "";

                                _inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);

                                if (!_barcode.Equals(barcode))
                                {
                                    _isGetData = getProductInfo(_inventoryId);

                                    _componentCd = "ALL";

                                    gcInventory.BeginUpdate();

                                    _bs.DataSource = null;
                                    _bs.DataSource = _dtProductInfo;
                                    gcInventory.DataSource = null;
                                    gcInventory.DataSource = _bs;

                                    if (_isGetData)
                                        setGridControl();

                                    gcInventory.EndUpdate();
                                    _barcode = barcode;
                                }

                                getCheckInfo(export, barcode);

                                if (_wifiYn == 1) ceWifi.Checked = true;
                                else ceWifi.Checked = false;

                                if (!string.IsNullOrWhiteSpace(_camModelNm)) ceCam.Checked = true;
                                else ceCam.Checked = false;

                                teBattery.Text = $"{ _battery.ToString("F0")}";
                                teOsInfo.Text = _osName;

                                _dtStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                if (true)
                                {
                                    _exist = true;

                                    setInitCheckData();
                                    setInitHistoryCheckData();
                                    // getCaptureImage();
                                    Dangol.CloseSplash();
                                }
                            }
                            else
                            {
                                _isGetData = false;
                                _dtProductInfo.Clear();

                                gcInventory.BeginUpdate();
                                gvInventory.BeginDataUpdate();
                                _bs.DataSource = null;
                                _bs.DataSource = _dtProductInfo;
                                gcInventory.DataSource = null;
                                gcInventory.DataSource = _bs;

                                gvInventory.EndDataUpdate();
                                gcInventory.EndUpdate();

                                _barcode = barcode;

                                Dangol.CloseSplash();
                                Dangol.Message("Parts cannot be inspected.");
                            }
                        }
                        else
                        {
                            Dangol.CloseSplash();
                            Dangol.Message("It's not in stock.");
                            return;
                        }
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message("The product information cannot be checked.");
                        return;
                    }
                }
                else
                {
                    Dangol.CloseSplash();
                    Dangol.Message("Check the management number.");
                    return;
                }
            }

        }

        private void InitCheck()
        {
            lgcNtbCheck.BeginUpdate();
            foreach (KeyValuePair<string, CheckEdit[]> item in _dicCheckEditList)
                for (int i = 0; i < item.Value.Length; i++)
                {
                    item.Value[i].CheckState = CheckState.Unchecked;
                    item.Value[i].ForeColor = Color.Black;
                }

            teDestroyedDescription.Text = "";
            teBatteryRemain.Text = "";

            foreach (KeyValuePair<string, Label> item in _dicCheckLabel)
                item.Value.BackColor = Color.Transparent;

            lgcNtbCheck.EndUpdate();
        }

        private void setInitCheckData()
        {
          
            if (_dicProductCheck.Count > 0)
            {
                lgcNtbCheck.BeginUpdate();

                string col;
                int value;
                CheckEdit[] arrCheckEdit;
                Label label;
                foreach (KeyValuePair<string, CheckEdit[]> item in _dicCheckEditList)
                {
                    col = item.Key;
                    arrCheckEdit = item.Value;

                    value = _dicProductCheck[col];

                    for(int i = 0; i < arrCheckEdit.Length; i++)
                        if((value & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                            arrCheckEdit[i].CheckState = CheckState.Checked;        
                }


                teDestroyedDescription.Text = _etcDes;
                teBatteryRemain.Text = _batteryRemain;

                foreach(string cols in _caseExceptionCol)
                {
                    if(_dicProductCheck[cols] > 0)
                    {
                        lbCase.BackColor = Color.LightSkyBlue;
                        break;
                    }
                }

                foreach (KeyValuePair<string, Label> item in _dicCheckLabel)
                {
                     col = item.Key;
                    if (!_caseExceptionCol.Contains(col))
                    {
                        label = item.Value;
                        if (_dicProductCheck[col] > 0)
                            label.BackColor = Color.LightSkyBlue;
                    }
                }

                lgcNtbCheck.EndUpdate();

            }
            else
            {
                foreach (KeyValuePair<string, CheckEdit[]> item in _dicCheckEditList)
                    _dicProductCheck.Add(item.Key, 0);
            }
        }

        private void setInitHistoryCheckData()
        {
            if (_dicProductCheckHistory.Count > 0)
            {
                lgcNtbCheck.BeginUpdate();

                Color color = Color.Red;
                string col;
                int value;
                CheckEdit[] arrCheckEdit;
                foreach (KeyValuePair<string, CheckEdit[]> item in _dicCheckEditList)
                {
                    col = item.Key;
                    arrCheckEdit = item.Value;

                    value = _dicProductCheckHistory[col];

                    for (int i = 0; i < arrCheckEdit.Length; i++)
                        if ((value & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                            arrCheckEdit[i].ForeColor = color;
                }

                lgcNtbCheck.EndUpdate();
            }
        }

        private void SaveCheckInfo()
        {
            lgcNtbCheck.BeginUpdate();

            _etcDes = teDestroyedDescription.Text;
            _batteryRemain = teBatteryRemain.Text;

            string col;
            int value;
            CheckEdit[] arrCheckEdit;
            foreach (KeyValuePair<string, CheckEdit[]> item in _dicCheckEditList)
            {
                col = item.Key;
                arrCheckEdit = item.Value;
                _dicProductCheck[col] = 0;
                value = 0;

                for (int i = 0; i < arrCheckEdit.Length; i++)
                    if (arrCheckEdit[i].CheckState == CheckState.Checked)
                        value += ExamineInfo._BASE[i];

                _dicProductCheck[col] = ConvertUtil.ToInt16(value);
            }

            lbCase.BackColor = Color.Transparent;
            foreach (string cols in _caseExceptionCol)
            {
                if (_dicProductCheck[cols] > 0)
                {
                    lbCase.BackColor = Color.LightSkyBlue;
                    break;
                }
            }

            Label label;

            foreach (KeyValuePair<string, Label> item in _dicCheckLabel)
            {
                col = item.Key;
                if (!_caseExceptionCol.Contains(col))
                {
                    label = item.Value;
                    if (_dicProductCheck[col] > 0)
                        label.BackColor = Color.LightSkyBlue;
                    else
                        label.BackColor = Color.Transparent;
                }
            }

            lgcNtbCheck.EndUpdate();
        }

        private void teBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getCheckData();
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            getCheckData();
        }


        private void getCaptureImage()
        {
            if(string.IsNullOrWhiteSpace(_produce) || _inventoryId < 1)
            {
                Dangol.Message("The product information cannot be checked.");
                return;
            }


            if (_captureYn == 1)
            {
                Image image = ScreenCapture.GetCaptureImg(_produce, $"{_inventoryId}.png");

                using (DlgImgTest digImgTest = new DlgImgTest(image))
                {
                    digImgTest.ShowDialog(this);
                    File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_inventoryId}.png");
                }

                //_image = ScreenCapture.GetCaptureImg(_produce, $"{_inventoryId}.png");
                //File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_inventoryId}.png");
            }
            else
                Dangol.Message("The QC image does not exist.");

            //pictureEdit1.Image = _image;

        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (_inventoryId > 0)
            {
                SaveCheckInfo();

                if (Dangol.MessageYN("Save the inspection results?") == DialogResult.Yes)
                {
                    _dtEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    DBConnect.insertNtbCheckExternal(_representativeType, _produce, _representativeCol, _dtStart, _dtEnd, _barcode,
                        _inventoryId, _checkType, _dicProductCheck, new List<long>(new[] { _inventoryId }), _etcDes, _batteryRemain, _productGrade, 1, false);

                    Dangol.Message("Execution completed.");
                }
            }
            else
            {
                Dangol.Warining("The product information cannot be checked.");
            }
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            //pictureEdit1.Image = Image.FromFile($"{System.Windows.Forms.Application.StartupPath}\\imgerror.png");
        }

        private void sbShowCapture_Click(object sender, EventArgs e)
        {
            getCaptureImage();
           // pictureEdit1.Image = _image;
        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            if(_exist)
                getCheckData(true);
            else
                getCheckData(false);
        }

        private void sbGetDeviceInfo_Click(object sender, EventArgs e)
        {
            if (ProjectInfo._getComponentInformationYn == 0)
            {
                _produce = "";
                _inventoryId = -1;

                Dangol.ShowSplash();
                Util.GetSystemInfo();
                gcInventory.BeginUpdate();
                gcInventory.DataSource = null;
                _bs.DataSource = null;
                _bs.DataSource = ProjectInfo._dtDeviceInfo;
                gcInventory.DataSource = _bs;
                gcInventory.EndUpdate();
                setGridControl();
                Dangol.CloseSplash();

                if (ProjectInfo._batteryRemain > 100.0)
                    ProjectInfo._batteryRemain = 100;

                ProjectInfo._batteryRemain = Math.Round(ProjectInfo._batteryRemain);

                if (ProjectInfo._wifiYn == 1) ceWifi.Checked = true;
                else ceWifi.Checked = false;

                if (!string.IsNullOrWhiteSpace(ProjectInfo._camModelNm)) ceCam.Checked = true;
                else ceCam.Checked = false;

                teBattery.Text = $"{ ProjectInfo._batteryRemain.ToString("F0")}";
                teOsInfo.Text = ProjectInfo._osName;

                ProjectInfo._getComponentInformationYn = 1;

                DataRow[] rows = ProjectInfo._dtDeviceInfo.Select("COMPONENT_CD = 'MBD'");

                if (rows.Length > 0)
                {
                    string barcode = ConvertUtil.ToString(rows[0]["BARCODE"]);
                    teBarcode.Text = barcode;
                    _barcode = barcode;
                    getCheckData();
                }               
            }
            else
            {
                _produce = "";
                _inventoryId = -1;

                gcInventory.BeginUpdate();
                gcInventory.DataSource = null;
                _bs.DataSource = null;
                _bs.DataSource = ProjectInfo._dtDeviceInfo;
                gcInventory.DataSource = _bs;
                gcInventory.EndUpdate();
                setGridControl();

                if (ProjectInfo._wifiYn == 1) ceWifi.Checked = true;
                else ceWifi.Checked = false;

                if (!string.IsNullOrWhiteSpace(ProjectInfo._camModelNm)) ceCam.Checked = true;
                else ceCam.Checked = false;

                teBattery.Text = $"{ ProjectInfo._batteryRemain.ToString("F0")}";
                teOsInfo.Text = ProjectInfo._osName;

                DataRow[] rows = ProjectInfo._dtDeviceInfo.Select("COMPONENT_CD = 'MBD'");

                if (rows.Length > 0)
                {
                    string barcode = ConvertUtil.ToString(rows[0]["BARCODE"]);
                    teBarcode.Text = barcode;
                    _barcode = barcode;
                    getCheckData();
                }
            }
        }

        private void setGridControl()
        {
            try
            {
                //gcInventory.DataSource = null;
                if (_bs.DataSource != null)
                {

                    gvInventory.BeginDataUpdate();

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
                        _bs.Filter = $"";
                        gcNo.FieldName = "NO1";
                    }
                    else
                    {
                        _bs.Filter = $"COMPONENT_CD = '{_componentCd}'";
                        gcNo.FieldName = "NO";
                    }

                    gvInventory.EndDataUpdate();
                }
                //gcInventory.DataSource = _bs;

                //if (_componentCd.Equals("ALL") && !_headerButtonVisible)
                //{
                //    _headerButtonVisible = true;
                //    lcgInventory.CustomHeaderButtons[0].Properties.Visible = true;
                //    //lcgInventory.CustomHeaderButtons[1].Properties.Visible = true;
                //    gcCheck.Visible = true;
                //}
                //else if (!_componentCd.Equals("ALL") && _headerButtonVisible)
                //{
                //    _headerButtonVisible = false;
                //    lcgInventory.CustomHeaderButtons[0].Properties.Visible = false;
                //    //lcgInventory.CustomHeaderButtons[1].Properties.Visible = false;
                //    gcCheck.Visible = false;
                //}
            }
            catch(Exception ex)
            {

            }
        }

        private void gvInventory_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvInventory.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                _devBarcode = ConvertUtil.ToString(_currentRow["BARCODE"]);
                _devComponent = ConvertUtil.ToString(_currentRow["COMPONENT"]);
                _devInventoryId = ConvertUtil.ToInt64(_currentRow["INVENTORY_ID"]);
                _devComponentId = ConvertUtil.ToInt64(_currentRow["COMPONENT_ID"]);
                _currentComponentCd = ConvertUtil.ToString(_currentRow["COMPONENT_CD"]);

               

                //checkPictureState();



                //gcInventoryDetail.DataSource = null;
                //_id = ConvertUtil.ToInt64(_currentRow["ID"]);
                //bsDetail.DataSource = ProjectInfo._dicDeviceInfoDetail[_id];
                //gcInventoryDetail.DataSource = bsDetail;

            }
            else
            {
                _currentRow = null;
                //checkPictureState();
            }
        }

        private void sbAll_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("ALL"))
                return;

            _componentCd = "ALL";
            if (ProjectInfo._getComponentInformationYn == 1 || _isGetData)
                setGridControl();
        }

        private void sbCpu_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("CPU"))
                return;

            _componentCd = "CPU";
            if (ProjectInfo._getComponentInformationYn == 1 || _isGetData)
                setGridControl();
        }

        private void sbMbd_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MBD"))
                return;

            _componentCd = "MBD";
            if (ProjectInfo._getComponentInformationYn == 1 || _isGetData)
                setGridControl();
        }

        private void sbMEM_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MEM"))
                return;

            _componentCd = "MEM";
            if (ProjectInfo._getComponentInformationYn == 1 || _isGetData)
                setGridControl();
        }

        private void sbStg_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("STG"))
                return;

            _componentCd = "STG";
            if (ProjectInfo._getComponentInformationYn == 1 || _isGetData)
                setGridControl();
        }

        private void sbVga_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("VGA"))
                return;

            _componentCd = "VGA";
            if (ProjectInfo._getComponentInformationYn == 1 || _isGetData)
                setGridControl();
        }

        private void sbMON_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("MON"))
                return;

            _componentCd = "MON";
            if (ProjectInfo._getComponentInformationYn == 1 || _isGetData)
                setGridControl();
        }
        private void sbODD_Click(object sender, EventArgs e)
        {
            if (_componentCd.Equals("ODD"))
                return;

            _componentCd = "ODD";
            //if (ProjectInfo._getComponentInformationYn == 1)
                setGridControl();
        }

        private bool getProductInfo(long inventoryId)
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();
            jData.Add("INVENTORY_ID", inventoryId);

            _dtProductInfo.Clear();

            if (DBRelease.getProductInfo(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    //if()
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    List<string> listCol;
                    Dictionary<string, int> dicComponentCnt = new Dictionary<string, int>();
                    string componentCd;
                    int index = 1;
                    int subIndex = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtProductInfo.NewRow();

                        componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);

                        if (dicComponentCnt.ContainsKey(componentCd))
                        {
                            dicComponentCnt[componentCd]++;
                            subIndex = dicComponentCnt[componentCd];
                        }
                        else
                        {
                            subIndex = 1;
                            dicComponentCnt.Add(componentCd, subIndex);
                        }

                        dr["NO1"] = index++;
                        dr["NO"] = subIndex;
                        dr["DATA1"] = ConvertUtil.ToString(obj["DATA1"]);
                        dr["DATA2"] = ConvertUtil.ToString(obj["DATA2"]);
                        dr["DATA3"] = ConvertUtil.ToString(obj["DATA3"]);
                        dr["DATA4"] = ConvertUtil.ToString(obj["DATA4"]);

                        if (ProjectInfo._dicDeviceColumn.ContainsKey(componentCd))
                        {
                            listCol = ProjectInfo._dicDeviceColumn[componentCd];

                            for (int i = 0; i < 4; i++)
                                dr[listCol[i]] = ConvertUtil.ToString(obj[$"DATA{i + 1}"]);
                        }

                        dr["INVENTORY_ID"] = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                        dr["BARCODE"] = ConvertUtil.ToString(obj["BARCODE"]);
                        dr["COMPONENT_ID"] = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                        dr["COMPONENT_CD"] = componentCd;
                        dr["NAME"] = componentCd;

                        _dtProductInfo.Rows.Add(dr);
                    }

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        
    }
}