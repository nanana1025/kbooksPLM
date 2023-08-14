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
    public partial class dlgReleaseCheck : DevExpress.XtraEditors.XtraForm
    {
        string _representativeType = "O";
        string _representativeCol = "RELEASES";
        string _representativeIdCol = "RELEASE_ID";

        Dictionary<string, short> _dicProductCheck = null;
        Dictionary<string, short> _dicProductCheckHistory = null;

        Dictionary<string, CheckEdit[]> _dicCheckEditList = null;

        Dictionary<string, Label> _dicCheckLabel = null;

        List<string> _caseExceptionCol;

        short _checkType = 5;

        string _etcDes = "";
        string _batteryRemain = "";
        string _productGrade = "";

        string _etcDesHistory = "";
        string _batteryRemainHistory = "";
        string _productGradeHistory = "";

        string _barcode;
        long _inventoryId;
        long _companyId;
        string _produce;
        int _captureYn;

        string _dtStart;
        string _dtEnd;

        Regex regex1;
        Regex regex2;
        Regex regex3;

        Regex regex4;

        Image _image;


        public dlgReleaseCheck(long companyId, object barcode)
        {
            InitializeComponent();

            teBarcode.Text = $"{barcode}";
            _companyId = companyId;


            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

            regex4 = new Regex(@"^E[0-9]{9}$");

            _dicProductCheck = new Dictionary<string, short>();
            _dicProductCheckHistory = new Dictionary<string, short>();


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

        }
        private void usrAdjustmentExamineList_Load(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            setInfoBox();
            setIInitData();
            getCheckData();
            Dangol.CloseSplash();

        }

        private void setInfoBox()
        {
           

        }

        private void setIInitData()
        {
           
        }

        private void getCheckInfo(string barcode)
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

            jobj.Add("EXPORT", "1");
            jobj.Add("BARCODE", barcode);
            jobj.Add("PRODUCE_TYPE", typeNm);
            jobj.Add("CHECK_TYPE", _checkType);
            jobj.Add("COMPANY_ID", _companyId);

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


                _inventoryId = ConvertUtil.ToInt64(jResult["INVENTORY_ID"]);
                _produce = ConvertUtil.ToString(jResult["PRODUCE"]);
                _captureYn = ConvertUtil.ToInt32(jResult["CAPTURE_YN"]);
            }
        }

        private void getCheckData()
        {
            InitCheck();

            //string export = teExport.Text.ToUpper().Trim();
            string barcode = teBarcode.Text.ToUpper().Trim();

            if (barcode.Length < 1 || string.IsNullOrWhiteSpace(barcode) )
                return;

            if (barcode.Length == 12 && (regex1.IsMatch(barcode) || regex2.IsMatch(barcode) || regex3.IsMatch(barcode) ))
            {
                getCheckInfo(barcode);

                _dtStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _barcode = barcode;
                if (true)
                {
                    setInitCheckData();
                    setInitHistoryCheckData();
                    getCaptureImage();
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

        private void sbSearch_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            getCheckData();
            Dangol.CloseSplash();
        }


        private void getCaptureImage()
        {
            if (_captureYn == 1)
            {
                _image = ScreenCapture.GetCaptureImg(_produce, $"{_inventoryId}.png");
                File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{_inventoryId}.png");
            }
            else
                _image = null;

            pictureEdit1.Image = _image;

        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (_inventoryId > 0)
            {
                SaveCheckInfo();

                if (Dangol.MessageYN("검수 정보를 저장하시겠습니까") == DialogResult.Yes)
                {
                    _dtEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    DBConnect.insertNtbCheckExternal(_representativeType, _produce, _representativeCol, _dtStart, _dtEnd, _barcode,
                        _inventoryId, _checkType, _dicProductCheck, new List<long>(new[] { _inventoryId }), _etcDes, _batteryRemain, _productGrade, 1, false);

                    Dangol.Message("처리되었습니다.");
                }
            }
            else
            {
                Dangol.Warining("제품 정보가 없습니다.");
            }
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            pictureEdit1.Image = Image.FromFile($"{System.Windows.Forms.Application.StartupPath}\\imgerror.png");
        }

        private void sbShowCapture_Click(object sender, EventArgs e)
        {
            pictureEdit1.Image = _image;
        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            getCheckData();
            Dangol.CloseSplash();
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}