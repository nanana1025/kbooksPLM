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
using DevExpress.XtraLayout;

namespace WareHousingMaster.view.inventory
{
    public partial class DlgAllInOneCheck : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, short> _DicAllInOneCheck;

        Dictionary<string, short> _dicHistoryAllInOneCheck;

        DataTable _dtAllInOneAdjustmentPrice;

        Dictionary<string, long> _dicAllInOneAdjustmentPrice;

        public string _etcDes { get; private set; }
        public string _pGrade { get; private set; }

        public bool _isPrint { get; set; }

        public long _palletNo { get; set; }

        DataTable _dtPort;

        DataTable _dtPGrade;

        BindingSource _bsPallet;

        short _checkType;

        List<LayoutControlItem> listLayoutRepair;
        List<SpinEdit> listSpinEditExam;
        List<SpinEdit> listSpinEditRepair;

        public DlgAllInOneCheck(Dictionary<string, short> dicAllInOneCheck, Dictionary<string, short> dicHistoryNtbCheck, ref DataTable dtAllInOneAdjustmentPrice, Dictionary<string, long> dicAllInOneAdjustmentPrice, string etcDes, string pGrade, DataTable dtPort, DataTable dtPGrade, BindingSource bsPallet, long palletNo, short checkType)
        {
            InitializeComponent();

            _DicAllInOneCheck = dicAllInOneCheck;
            _dicHistoryAllInOneCheck = dicHistoryNtbCheck;
            _dtAllInOneAdjustmentPrice = dtAllInOneAdjustmentPrice;
            _dicAllInOneAdjustmentPrice = dicAllInOneAdjustmentPrice;
            _etcDes = etcDes;
            _pGrade = pGrade;
            _dtPort = dtPort;
            _dtPGrade = dtPGrade;
            _isPrint = false;
            _bsPallet = bsPallet;
            _palletNo = palletNo;
            _checkType = checkType;

            listLayoutRepair = new List<LayoutControlItem>(new[] { lcRepair, lcRepair1,lcRepair2, lcRepair3, lcRepair4, lcRepair5,
            lcRepair7, lcRepair8, lcRepair9, lcRepair10, lcRepair11, lcRepair12, lcRepair13, lcRepair14, lcRepair15, lcRepair16, lcRepairEtc});

            listSpinEditExam = new List<SpinEdit>(new[] { seExam1, seExam2,seExam3, seExam4, seExam5, seExam7,
            seExam8, seExam9, seExam10, seExam11, seExam12, seExam13, seExam14, seExam15, seExam16, seExamEtc});

            listSpinEditRepair = new List<SpinEdit>(new[] { seRepair1, seRepair2,seRepair3, seRepair4, seRepair5, seRepair7,
            seRepair8, seRepair9, seRepair10, seRepair11, seRepair12, seRepair13, seRepair14, seRepair15, seRepair16, seRepairEtc});

        }

        private void DlgNtbCheck_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            if (_DicAllInOneCheck == null)
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                this.DialogResult = DialogResult.Cancel;
            }

            if (_checkType == 2)
            {
                sbRepair.Text = "출고차감가";
                lcRepairEtc.Text = "출고 기타 차감가";
            }
            else if (_checkType == 3)
            {
                sbRepair.Text = "리페어차감가";
                lcRepairEtc.Text = "리페어 기타 차감가";
            }
            else if (_checkType == 4)
            {
                sbRepair.Text = "생산대행차감가";
                lcRepairEtc.Text = "생산대행 기타 차감가";
            }

            if(_bsPallet == null)
                lcPallet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;


            lgcNtbCheck.BeginUpdate();
            if (_checkType == 1)
            {
                if (lcRepair.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {              
                    foreach (LayoutControlItem lcControl in listLayoutRepair)
                        lcControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                seExamEtc.ReadOnly = false;
            }
            else
            {
                if (lcRepair.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                {
                    foreach (LayoutControlItem lcControl in listLayoutRepair)
                        lcControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }

                seExamEtc.ReadOnly = true;

                setExamTextColor();
            }
            lgcNtbCheck.EndUpdate();

            setInit();

            Util.LookupEditHelper(lePort, _dtPort, "KEY", "VALUE");
            lePort.EditValue = ProjectInfo._printerPort;

            Util.LookupEditHelper(lePGrade, _dtPGrade, "KEY", "VALUE");
            lePGrade.EditValue = _pGrade;

            Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");
            lePallet.EditValue = $"{ _palletNo }";

            if (_DicAllInOneCheck.Count > 0)
            {
                short caseDestroyed = _DicAllInOneCheck["CASE_DESTROYED"];
                short caseScratch = _DicAllInOneCheck["CASE_SCRATCH"];
                short caseStabbed = _DicAllInOneCheck["CASE_STABBED"];
                short casePressed = _DicAllInOneCheck["CASE_PRESSED"];
                short caseDiscolored = _DicAllInOneCheck["CASE_DISCOLORED"];
                short menu = _DicAllInOneCheck["MENU"];
               

                short display = _DicAllInOneCheck["DISPLAY"];
                short port = _DicAllInOneCheck["PORT"];
                short adapter = _DicAllInOneCheck["ADAPTER"];
                short usb = _DicAllInOneCheck["USB"];
                short mousePad = _DicAllInOneCheck["MOUSEPAD"];
                short keyboard = _DicAllInOneCheck["KEYBOARD"];
                short cam = _DicAllInOneCheck["CAM"];
                short odd = _DicAllInOneCheck["ODD"];
                short hdd = _DicAllInOneCheck["HDD"];

                short lanWireless = _DicAllInOneCheck["LAN_WIRELESS"];
                short lanWired = _DicAllInOneCheck["LAN_WIRED"];              
                short bios = _DicAllInOneCheck["BIOS"];
                short os = _DicAllInOneCheck["OS"];
                short check = _DicAllInOneCheck["TEST_CHECK"];
                string etcDes = _etcDes;

                if ((caseDestroyed & 1) == 1)
                    ceCaseDestroyed1.CheckState = CheckState.Checked;
                if ((caseDestroyed & 2) == 2)
                    ceCaseDestroyed2.CheckState = CheckState.Checked;
                if ((caseDestroyed & 4) == 4)
                    ceCaseDestroyed3.CheckState = CheckState.Checked;

                if ((caseScratch & 1) == 1)
                    ceCaseScratch1.CheckState = CheckState.Checked;
                if ((caseScratch & 2) == 2)
                    ceCaseScratch2.CheckState = CheckState.Checked;
                if ((caseScratch & 4) == 4)
                    ceCaseScratch3.CheckState = CheckState.Checked;

                if ((caseStabbed & 1) == 1)
                    ceCaseStabbed1.CheckState = CheckState.Checked;
                if ((caseStabbed & 2) == 2)
                    ceCaseStabbed2.CheckState = CheckState.Checked;
                if ((caseStabbed & 4) == 4)
                    ceCaseStabbed3.CheckState = CheckState.Checked;

                if ((casePressed & 1) == 1)
                    ceCasePressed1.CheckState = CheckState.Checked;
                if ((casePressed & 2) == 2)
                    ceCasePressed2.CheckState = CheckState.Checked;
                if ((casePressed & 4) == 4)
                    ceCasePressed3.CheckState = CheckState.Checked;

                if ((caseDiscolored & 1) == 1)
                    ceCaseDiscolored1.CheckState = CheckState.Checked;
                if ((caseDiscolored & 2) == 2)
                    ceCaseDiscolored2.CheckState = CheckState.Checked;
                if ((caseDiscolored & 4) == 4)
                    ceCaseDiscolored3.CheckState = CheckState.Checked;

                if (menu == 1)
                    ceMenu.CheckState = CheckState.Checked;


               

                if ((display & 1) == 1)
                    ceDisplay1.CheckState = CheckState.Checked;
                if ((display & 2) == 2)
                    ceDisplay2.CheckState = CheckState.Checked;
                if ((display & 4) == 4)
                    ceDisplay3.CheckState = CheckState.Checked;
                if ((display & 8) == 8)
                    ceDisplay4.CheckState = CheckState.Checked;
                if ((display & 16) == 16)
                    ceDisplay5.CheckState = CheckState.Checked;
                if ((display & 32) == 32)
                    ceDisplay6.CheckState = CheckState.Checked;
                if ((display & 64) == 64)
                    ceDisplay7.CheckState = CheckState.Checked;
                if ((display & 128) == 128)
                    ceDisplay8.CheckState = CheckState.Checked;
                if ((display & 256) == 256)
                    ceDisplay9.CheckState = CheckState.Checked;

                if ((port & 1) == 1)
                    cePort1.CheckState = CheckState.Checked;
                if ((port & 2) == 2)
                    cePort2.CheckState = CheckState.Checked;
                if ((port & 4) == 4)
                    cePort3.CheckState = CheckState.Checked;
                if ((port & 8) == 8)
                    cePort4.CheckState = CheckState.Checked;

                if ((adapter & 1) == 1)
                    ceAdaptor1.CheckState = CheckState.Checked;
                if ((adapter & 2) == 2)
                    ceAdaptor2.CheckState = CheckState.Checked;
                if ((adapter & 4) == 4)
                    ceAdaptor3.CheckState = CheckState.Checked;


                if ((usb & 1) == 1)
                    ceUsb1.CheckState = CheckState.Checked;
                if ((usb & 2) == 2)
                    ceUsb2.CheckState = CheckState.Checked;


                if ((mousePad & 1) == 1)
                    ceMousePad1.CheckState = CheckState.Checked;
                if ((mousePad & 2) == 2)
                    ceMousePad2.CheckState = CheckState.Checked;
                if ((mousePad & 4) == 4)
                    ceMousePad3.CheckState = CheckState.Checked;


                if ((keyboard & 1) == 1)
                    ceKeyboard1.CheckState = CheckState.Checked;
                if ((keyboard & 2) == 2)
                    ceKeyboard2.CheckState = CheckState.Checked;
                if ((keyboard & 4) == 4)
                    ceKeyboard3.CheckState = CheckState.Checked;
                if ((keyboard & 8) == 8)
                    ceKeyboard4.CheckState = CheckState.Checked;
                if ((keyboard & 16) == 16)
                    ceKeyboard5.CheckState = CheckState.Checked;

                if ((cam & 1) == 1)
                    ceCam1.CheckState = CheckState.Checked;
                if ((cam & 2) == 2)
                    ceCam2.CheckState = CheckState.Checked;
                if ((cam & 4) == 4)
                    ceCam3.CheckState = CheckState.Checked;


                if ((odd & 1) == 1)
                    ceOdd1.CheckState = CheckState.Checked;
                if ((odd & 2) == 2)
                    ceOdd2.CheckState = CheckState.Checked;
                if ((odd & 4) == 4)
                    ceOdd3.CheckState = CheckState.Checked;
                if ((odd & 8) == 8)
                    ceOdd4.CheckState = CheckState.Checked;
                if ((odd & 16) == 16)
                    ceOdd5.CheckState = CheckState.Checked;
                if ((odd & 32) == 32)
                    ceOdd6.CheckState = CheckState.Checked;


                if ((hdd & 1) == 1)
                    ceHdd1.CheckState = CheckState.Checked;
                if ((hdd & 2) == 2)
                    ceHdd2.CheckState = CheckState.Checked;
                if ((hdd & 4) == 4)
                    ceHdd3.CheckState = CheckState.Checked;
                if ((hdd & 8) == 8)
                    ceHdd4.CheckState = CheckState.Checked;
                if ((hdd & 16) == 16)
                    ceHdd5.CheckState = CheckState.Checked;


                if ((lanWireless & 1) == 1)
                    ceLanWireless1.CheckState = CheckState.Checked;
                if ((lanWireless & 2) == 2)
                    ceLanWireless2.CheckState = CheckState.Checked;

                if ((lanWired & 1) == 1)
                    ceLanWired1.CheckState = CheckState.Checked;
                if ((lanWired & 2) == 2)
                    ceLanWired2.CheckState = CheckState.Checked;


                if ((bios & 1) == 1)
                    ceBios1.CheckState = CheckState.Checked;
                if ((bios & 2) == 2)
                    ceBios2.CheckState = CheckState.Checked;

                if ((os & 1) == 1)
                    ceOs1.CheckState = CheckState.Checked;


                if ((check & 1) == 1)
                    ceCheck1.CheckState = CheckState.Checked;
                if ((check & 2) == 2)
                    ceCheck2.CheckState = CheckState.Checked;
                if ((check & 4) == 4)
                    ceCheck3.CheckState = CheckState.Checked;
                if ((check & 8) == 8)
                    ceCheck4.CheckState = CheckState.Checked;

                teEtc.Text = _etcDes;


                if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0 || menu > 0)
                    lbCase.BackColor = Color.LightSkyBlue;
                if (display > 0)
                    lbDisplay.BackColor = Color.LightSkyBlue;
                if (port > 0)
                    lbPort.BackColor = Color.LightSkyBlue;
                if (adapter > 0)
                    lbAdapter.BackColor = Color.LightSkyBlue;
                if (usb > 0)
                    lbUsb.BackColor = Color.LightSkyBlue;                 
                if (mousePad > 0)
                    lbMousePad.BackColor = Color.LightSkyBlue;
                if (keyboard > 0)
                    lbKeyboard.BackColor = Color.LightSkyBlue;
                if (cam > 0)
                    lbCam.BackColor = Color.LightSkyBlue;
                if (odd > 0)
                    lbOdd.BackColor = Color.LightSkyBlue;
                if (hdd > 0)
                    lbHdd.BackColor = Color.LightSkyBlue;
                if (lanWireless > 0)
                    lbLanWireless.BackColor = Color.LightSkyBlue;
                if (lanWired > 0)
                    lbLanWired.BackColor = Color.LightSkyBlue;
                if (bios > 0)
                    lbBios.BackColor = Color.LightSkyBlue;
                if (os > 0)
                    lbOs.BackColor = Color.LightSkyBlue;
                if (check > 0)
                    lbCheck.BackColor = Color.LightSkyBlue;
            }
            else
            {
                _DicAllInOneCheck.Add("CASE_DESTROYED", 0);
                _DicAllInOneCheck.Add("CASE_SCRATCH", 0);
                _DicAllInOneCheck.Add("CASE_STABBED", 0);
                _DicAllInOneCheck.Add("CASE_PRESSED", 0);
                _DicAllInOneCheck.Add("CASE_DISCOLORED", 0);
                _DicAllInOneCheck.Add("MENU", 0);
                _DicAllInOneCheck.Add("DISPLAY", 0);
                _DicAllInOneCheck.Add("PORT", 0);
                _DicAllInOneCheck.Add("ADAPTER", 0);
                _DicAllInOneCheck.Add("USB", 0);
                _DicAllInOneCheck.Add("MOUSEPAD", 0);
                _DicAllInOneCheck.Add("KEYBOARD", 0);
                _DicAllInOneCheck.Add("CAM", 0);
                _DicAllInOneCheck.Add("ODD", 0);
                _DicAllInOneCheck.Add("HDD", 0);
                _DicAllInOneCheck.Add("LAN_WIRELESS", 0);
                _DicAllInOneCheck.Add("LAN_WIRED", 0);
                _DicAllInOneCheck.Add("BIOS", 0);
                _DicAllInOneCheck.Add("OS", 0);
                _DicAllInOneCheck.Add("TEST_CHECK", 0);
            }
            
        }

        private void setExamTextColor()
        {
            if (_dicHistoryAllInOneCheck.Count > 0)
            {
                short caseDestroyed = _dicHistoryAllInOneCheck["CASE_DESTROYED"];
                short caseScratch = _dicHistoryAllInOneCheck["CASE_SCRATCH"];
                short caseStabbed = _dicHistoryAllInOneCheck["CASE_STABBED"];
                short casePressed = _dicHistoryAllInOneCheck["CASE_PRESSED"];
                short caseDiscolored = _dicHistoryAllInOneCheck["CASE_DISCOLORED"];
                short menu = _dicHistoryAllInOneCheck["MENU"];

                short display = _dicHistoryAllInOneCheck["DISPLAY"];
                short port = _dicHistoryAllInOneCheck["PORT"];
                short adapter = _dicHistoryAllInOneCheck["ADAPTER"];
                short usb = _dicHistoryAllInOneCheck["USB"];
                short mousePad = _dicHistoryAllInOneCheck["MOUSEPAD"];
                short keyboard = _dicHistoryAllInOneCheck["KEYBOARD"];
                short cam = _dicHistoryAllInOneCheck["CAM"];
                short odd = _dicHistoryAllInOneCheck["ODD"];

                short lanWireless = _dicHistoryAllInOneCheck["LAN_WIRELESS"];
                short lanWired = _dicHistoryAllInOneCheck["LAN_WIRED"];
                short hdd = _dicHistoryAllInOneCheck["HDD"];
                short bios = _dicHistoryAllInOneCheck["BIOS"];
                short os = _dicHistoryAllInOneCheck["OS"];
                short check = _dicHistoryAllInOneCheck["TEST_CHECK"];


                Color color = Color.Red;

                if ((caseDestroyed & 1) == 1)
                    ceCaseDestroyed1.ForeColor = color;
                if ((caseDestroyed & 2) == 2)
                    ceCaseDestroyed2.ForeColor = color;
                if ((caseDestroyed & 4) == 4)
                    ceCaseDestroyed3.ForeColor = color;

                if ((caseScratch & 1) == 1)
                    ceCaseScratch1.ForeColor = color;
                if ((caseScratch & 2) == 2)
                    ceCaseScratch2.ForeColor = color;
                if ((caseScratch & 4) == 4)
                    ceCaseScratch3.ForeColor = color;

                if ((caseStabbed & 1) == 1)
                    ceCaseStabbed1.ForeColor = color;
                if ((caseStabbed & 2) == 2)
                    ceCaseStabbed2.ForeColor = color;
                if ((caseStabbed & 4) == 4)
                    ceCaseStabbed3.ForeColor = color;

                if ((casePressed & 1) == 1)
                    ceCasePressed1.ForeColor = color;
                if ((casePressed & 2) == 2)
                    ceCasePressed2.ForeColor = color;
                if ((casePressed & 4) == 4)
                    ceCasePressed3.ForeColor = color;

                if ((caseDiscolored & 1) == 1)
                    ceCaseDiscolored1.ForeColor = color;
                if ((caseDiscolored & 2) == 2)
                    ceCaseDiscolored2.ForeColor = color;
                if ((caseDiscolored & 4) == 4)
                    ceCaseDiscolored3.ForeColor = color;

                if (menu == 1)
                    lcMenu.AppearanceItemCaption.ForeColor = color;

                if ((display & 1) == 1)
                    ceDisplay1.ForeColor = color;
                if ((display & 2) == 2)
                    ceDisplay2.ForeColor = color;
                if ((display & 4) == 4)
                    ceDisplay3.ForeColor = color;
                if ((display & 8) == 8)
                    ceDisplay4.ForeColor = color;
                if ((display & 16) == 16)
                    ceDisplay5.ForeColor = color;
                if ((display & 32) == 32)
                    ceDisplay6.ForeColor = color;
                if ((display & 64) == 64)
                    ceDisplay7.ForeColor = color;
                if ((display & 128) == 128)
                    ceDisplay8.ForeColor = color;
                if ((display & 256) == 256)
                    ceDisplay9.ForeColor = color;

                if ((port & 1) == 1)
                    cePort1.ForeColor = color;
                if ((port & 2) == 2)
                    cePort2.ForeColor = color;
                if ((port & 4) == 4)
                    cePort3.ForeColor = color;
                if ((port & 8) == 8)
                    cePort4.ForeColor = color;

                if ((adapter & 1) == 1)
                    ceAdaptor1.ForeColor = color;
                if ((adapter & 2) == 2)
                    ceAdaptor2.ForeColor = color;
                if ((adapter & 4) == 4)
                    ceAdaptor3.ForeColor = color;


                if ((usb & 1) == 1)
                    ceUsb1.ForeColor = color;
                if ((usb & 2) == 2)
                    ceUsb2.ForeColor = color;


                if ((mousePad & 1) == 1)
                    ceMousePad1.ForeColor = color;
                if ((mousePad & 2) == 2)
                    ceMousePad2.ForeColor = color;
                if ((mousePad & 4) == 4)
                    ceMousePad3.ForeColor = color;


                if ((keyboard & 1) == 1)
                    ceKeyboard1.ForeColor = color;
                if ((keyboard & 2) == 2)
                    ceKeyboard2.ForeColor = color;
                if ((keyboard & 4) == 4)
                    ceKeyboard3.ForeColor = color;
                if ((keyboard & 8) == 8)
                    ceKeyboard4.ForeColor = color;
                if ((keyboard & 16) == 16)
                    ceKeyboard5.ForeColor = color;

                   

                if ((cam & 1) == 1)
                    ceCam1.ForeColor = color;
                if ((cam & 2) == 2)
                    ceCam2.ForeColor = color;
                if ((cam & 4) == 4)
                    ceCam3.ForeColor = color;


                if ((odd & 1) == 1)
                    ceOdd1.ForeColor = color;
                if ((odd & 2) == 2)
                    ceOdd2.ForeColor = color;
                if ((odd & 4) == 4)
                    ceOdd3.ForeColor = color;
                if ((odd & 8) == 8)
                    ceOdd4.ForeColor = color;
                if ((odd & 16) == 16)
                    ceOdd5.ForeColor = color;
                if ((odd & 32) == 32)
                    ceOdd6.ForeColor = color;


                if ((hdd & 1) == 1)
                    ceHdd1.ForeColor = color;
                if ((hdd & 2) == 2)
                    ceHdd2.ForeColor = color;
                if ((hdd & 4) == 4)
                    ceHdd3.ForeColor = color;
                if ((hdd & 8) == 8)
                    ceHdd4.ForeColor = color;
                if ((hdd & 16) == 16)
                    ceHdd5.ForeColor = color;


                if ((lanWireless & 1) == 1)
                    ceLanWireless1.ForeColor = color;
                if ((lanWireless & 2) == 2)
                    ceLanWireless2.ForeColor = color;

                if ((lanWired & 1) == 1)
                    ceLanWired1.ForeColor = color;
                if ((lanWired & 2) == 2)
                    ceLanWired2.ForeColor = color;


                if ((bios & 1) == 1)
                    ceBios1.ForeColor = color;
                if ((bios & 2) == 2)
                    ceBios2.ForeColor = color;

                if ((os & 1) == 1)
                    ceOs1.ForeColor = color;


                if ((check & 1) == 1)
                    ceCheck1.ForeColor = color;
                if ((check & 2) == 2)
                    ceCheck2.ForeColor = color;
                if ((check & 4) == 4)
                    ceCheck3.ForeColor = color;
                if ((check & 8) == 8)
                    ceCheck4.ForeColor = color;
            }
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if (_checkType == 1)
                CheckInfo();
            else
                CheckInfoForSave();

            if (_checkType == 1)
            {
                if (_pGrade.Equals("0"))
                {
                    if (MessageBox.Show("제품등급이 '등급없음' 입니다. 그대로 진행하시겠습니까?", "제품등급 확인", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            else if(_checkType == 3)
            {
                if (_pGrade.Equals("0"))
                {
                    if (MessageBox.Show("제품등급이 '등급없음' 입니다. 그대로 진행하시겠습니까?", "제품등급 확인", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (_pGrade.Equals("4"))
                {
                    if (MessageBox.Show("제품등급이 '리페어' 입니다. 그대로 진행하시겠습니까?", "제품등급 확인", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (MessageBox.Show("검수완료하시겠습니까(차감가 체크)?", "검수 저장 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveCheckInfo();
                //MessageBox.Show("검수완료되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                //MessageBox.Show("아니요 클릭");
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void CheckInfo()
        {
            short caseDestroyed = 0;
            short caseScratch = 0;
            short caseStabbed = 0;
            short casePressed = 0;
            short caseDiscolored = 0;
            _etcDes = teEtc.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);

            short display = 0;
            short port = 0;
            short adapter = 0;
            short usb = 0;
            short mousePad = 0;
            short keyboard = 0;
           
            short cam = 0;
            short odd = 0;

            short lanWireless = 0;
            short lanWired = 0;
            short hdd = 0;
            short bios = 0;
            short os = 0;
            short check = 0;

            if (ceCaseDestroyed1.CheckState == CheckState.Checked)
                caseDestroyed += 1;
            if (ceCaseDestroyed2.CheckState == CheckState.Checked)
                caseDestroyed += 2;
            if (ceCaseDestroyed3.CheckState == CheckState.Checked)
                caseDestroyed += 4;

            if (ceCaseScratch1.CheckState == CheckState.Checked)
                caseScratch += 1;
            if (ceCaseScratch2.CheckState == CheckState.Checked)
                caseScratch += 2;
            if (ceCaseScratch3.CheckState == CheckState.Checked)
                caseScratch += 4;

            if (ceCaseStabbed1.CheckState == CheckState.Checked)
                caseStabbed += 1;
            if (ceCaseStabbed2.CheckState == CheckState.Checked)
                caseStabbed += 2;
            if (ceCaseStabbed3.CheckState == CheckState.Checked)
                caseStabbed += 4;

            if (ceCasePressed1.CheckState == CheckState.Checked)
                casePressed += 1;
            if (ceCasePressed2.CheckState == CheckState.Checked)
                casePressed += 2;
            if (ceCasePressed3.CheckState == CheckState.Checked)
                casePressed += 4;

            if (ceCaseDiscolored1.CheckState == CheckState.Checked)
                caseDiscolored += 1;
            if (ceCaseDiscolored2.CheckState == CheckState.Checked)
                caseDiscolored += 2;
            if (ceCaseDiscolored3.CheckState == CheckState.Checked)
                caseDiscolored += 4;


            short menu = ceMenu.CheckState == CheckState.Checked ? (short)1 : (short)0;


            if (ceDisplay1.CheckState == CheckState.Checked)
                display += 1;
            if (ceDisplay2.CheckState == CheckState.Checked)
                display += 2;
            if (ceDisplay3.CheckState == CheckState.Checked)
                display += 4;
            if (ceDisplay4.CheckState == CheckState.Checked)
                display += 8;
            if (ceDisplay5.CheckState == CheckState.Checked)
                display += 16;
            if (ceDisplay6.CheckState == CheckState.Checked)
                display += 32;
            if (ceDisplay7.CheckState == CheckState.Checked)
                display += 64;
            if (ceDisplay8.CheckState == CheckState.Checked)
                display += 128;
            if (ceDisplay9.CheckState == CheckState.Checked)
                display += 256;

            if (cePort1.CheckState == CheckState.Checked)
                port += 1;
            if (cePort2.CheckState == CheckState.Checked)
                port += 2;
            if (cePort3.CheckState == CheckState.Checked)
                port += 4;
            if (cePort4.CheckState == CheckState.Checked)
                port += 8;

            if (ceAdaptor1.CheckState == CheckState.Checked)
                adapter += 1;
            if (ceAdaptor2.CheckState == CheckState.Checked)
                adapter += 2;
            if (ceAdaptor3.CheckState == CheckState.Checked)
                adapter += 4;



            if (ceUsb1.CheckState == CheckState.Checked)
                usb += 1;
            if (ceUsb2.CheckState == CheckState.Checked)
                usb += 2;

            if (ceMousePad1.CheckState == CheckState.Checked)
                mousePad += 1;
            if (ceMousePad2.CheckState == CheckState.Checked)
                mousePad += 2;
            if (ceMousePad3.CheckState == CheckState.Checked)
                mousePad += 4;


            if (ceKeyboard1.CheckState == CheckState.Checked)
                keyboard += 1;
            if (ceKeyboard2.CheckState == CheckState.Checked)
                keyboard += 2;
            if (ceKeyboard3.CheckState == CheckState.Checked)
                keyboard += 4;
            if (ceKeyboard4.CheckState == CheckState.Checked)
                keyboard += 8;
            if (ceKeyboard5.CheckState == CheckState.Checked)
                keyboard += 16;

            if (ceCam1.CheckState == CheckState.Checked)
                cam += 1;
            if (ceCam2.CheckState == CheckState.Checked)
                cam += 2;
            if (ceCam3.CheckState == CheckState.Checked)
                cam += 4;


            if (ceOdd1.CheckState == CheckState.Checked)
                odd += 1;
            if (ceOdd2.CheckState == CheckState.Checked)
                odd += 2;
            if (ceOdd3.CheckState == CheckState.Checked)
                odd += 4;
            if (ceOdd4.CheckState == CheckState.Checked)
                odd += 8;
            if (ceOdd5.CheckState == CheckState.Checked)
                odd += 16;
            if (ceOdd6.CheckState == CheckState.Checked)
                odd += 32;

            if (ceHdd1.CheckState == CheckState.Checked)
                hdd += 1;
            if (ceHdd2.CheckState == CheckState.Checked)
                hdd += 2;
            if (ceHdd3.CheckState == CheckState.Checked)
                hdd += 4;
            if (ceHdd4.CheckState == CheckState.Checked)
                hdd += 8;
            if (ceHdd5.CheckState == CheckState.Checked)
                hdd += 16;


            if (ceLanWireless1.CheckState == CheckState.Checked)
                lanWireless += 1;
            if (ceLanWireless2.CheckState == CheckState.Checked)
                lanWireless += 2;

            if (ceLanWired1.CheckState == CheckState.Checked)
                lanWired += 1;
            if (ceLanWired2.CheckState == CheckState.Checked)
                lanWired += 2;

            if (ceBios1.CheckState == CheckState.Checked)
                bios += 1;
            if (ceBios2.CheckState == CheckState.Checked)
                bios += 2;


            if (ceOs1.CheckState == CheckState.Checked)
                os += 1;


            if (ceCheck1.CheckState == CheckState.Checked)
                check += 1;
            if (ceCheck2.CheckState == CheckState.Checked)
                check += 2;
            if (ceCheck3.CheckState == CheckState.Checked)
                check += 4;
            if (ceCheck4.CheckState == CheckState.Checked)
                check += 8;

            string col = "";
            col = "CASES";
            if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0 || menu > 0)
            {
                lbCase.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam1.EditValue = adjustPrice;
                else 
                    seRepair1.EditValue = adjustPrice;
            }
            else
            {
                lbCase.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam1.EditValue = 0;
                else 
                    seRepair1.EditValue = 0;

            }

            col = "DISPLAY";
            if (display > 0)
            {
                lbDisplay.BackColor = Color.LightSkyBlue;

                
                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam2.EditValue = adjustPrice;
                else 
                    seRepair2.EditValue = adjustPrice;
            }
            else
            {
                lbDisplay.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam2.EditValue = 0;
                else 
                    seRepair2.EditValue = 0;
            }

            col = "PORT";
            if (port > 0)
            {
                lbPort.BackColor = Color.LightSkyBlue;


                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam15.EditValue = adjustPrice;
                else
                    seRepair15.EditValue = adjustPrice;
            }
            else
            {
                lbPort.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam15.EditValue = 0;
                else
                    seRepair15.EditValue = 0;
            }

            col = "ADAPTER";
            if (adapter > 0)
            {
                lbAdapter.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam16.EditValue = adjustPrice;
                else
                    seRepair16.EditValue = adjustPrice;
            }
            else
            {
                lbAdapter.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam16.EditValue = 0;
                else
                    seRepair16.EditValue = 0;
            }

            col = "USB";
            if (usb > 0)
            {
                lbUsb.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam3.EditValue = adjustPrice;
                else 
                    seRepair3.EditValue = adjustPrice;
            }
            else
            {
                lbUsb.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam3.EditValue = 0;
                else 
                    seRepair3.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam3.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair3.EditValue);

            col = "MOUSEPAD";
            if (mousePad > 0)
            {
                lbMousePad.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam4.EditValue = adjustPrice;
                else 
                    seRepair4.EditValue = adjustPrice;
            }
            else
            {
                lbMousePad.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam4.EditValue = 0;
                else 
                    seRepair4.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam4.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair4.EditValue);

            col = "KEYBOARD";
            if (keyboard > 0)
            {
                lbKeyboard.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam5.EditValue = adjustPrice;
                else 
                    seRepair5.EditValue = adjustPrice;
            }
            else
            {
                lbKeyboard.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam5.EditValue = 0;
                else 
                    seRepair5.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam5.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair5.EditValue);

            col = "CAM";
            if (cam > 0)
            {
                lbCam.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam7.EditValue = adjustPrice;
                else 
                    seRepair7.EditValue = adjustPrice;
            }
            else 
            { 
                lbCam.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam7.EditValue = 0;
                else 
                    seRepair7.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam7.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair7.EditValue);

            col = "ODD";
            if (odd > 0)
            {
                lbOdd.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam8.EditValue = adjustPrice;
                else 
                    seRepair8.EditValue = adjustPrice;
            }
            else
            {
                lbOdd.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam8.EditValue = 0;
                else 
                    seRepair8.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam8.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair8.EditValue);

            col = "HDD";
            if (hdd > 0)
            {
                lbHdd.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam9.EditValue = adjustPrice;
                else 
                    seRepair9.EditValue = adjustPrice;
            }
            else
            {
                lbHdd.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam9.EditValue = 0;
                else 
                    seRepair9.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam9.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair9.EditValue);

            col = "LAN_WIRELESS";
            if (lanWireless > 0)
            {
                lbLanWireless.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam10.EditValue = adjustPrice;
                else 
                    seRepair10.EditValue = adjustPrice;
            }
            else
            {
                lbLanWireless.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam10.EditValue = 0;
                else 
                    seRepair10.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam10.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair10.EditValue);

            col = "LAN_WIRED";
            if (lanWired > 0)
            {
                lbLanWired.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam11.EditValue = adjustPrice;
                else 
                    seRepair11.EditValue = adjustPrice;
            }
            else
            {
                lbLanWired.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam11.EditValue = 0;
                else 
                    seRepair11.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam11.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair11.EditValue);


            col = "BIOS";
            if (bios > 0)
            {
                lbBios.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam12.EditValue = adjustPrice;
                else
                    seRepair12.EditValue = adjustPrice;
            }
            else
            {
                lbBios.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam12.EditValue = 0;
                else
                    seRepair12.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam12.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair12.EditValue);

            col = "OS";
            if (os > 0)
            {
                lbOs.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam13.EditValue = adjustPrice;
                else
                    seRepair13.EditValue = adjustPrice;
            }
            else
            {
                lbOs.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam13.EditValue = 0;
                else 
                    seRepair13.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam13.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair13.EditValue);

            col = "TEST_CHECK";
            if (check > 0)
            {
                lbCheck.BackColor = Color.LightSkyBlue;

                long adjustPrice = 0;
                if (_dicAllInOneAdjustmentPrice.ContainsKey(col))
                    adjustPrice = _dicAllInOneAdjustmentPrice[col];

                if (_checkType == 1)
                    seExam14.EditValue = adjustPrice;
                else
                    seRepair14.EditValue = adjustPrice;
            }
            else
            {
                lbCheck.BackColor = Color.Transparent;

                if (_checkType == 1)
                    seExam14.EditValue = 0;
                else
                    seRepair14.EditValue = 0;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam14.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair14.EditValue);

            _palletNo = ConvertUtil.ToInt64(lePallet.EditValue);

        }

        private void CheckInfoForSave()
        {
            short caseDestroyed = 0;
            short caseScratch = 0;
            short caseStabbed = 0;
            short casePressed = 0;
            short caseDiscolored = 0;
            _etcDes = teEtc.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);

            short display = 0;
            short port = 0;
            short adapter = 0;
            short usb = 0;
            short mousePad = 0;
            short keyboard = 0;
            short battery = 0;
            short cam = 0;
            short odd = 0;

            short lanWireless = 0;
            short lanWired = 0;
            short hdd = 0;
            short bios = 0;
            short os = 0;
            short check = 0;

            if (ceCaseDestroyed1.CheckState == CheckState.Checked)
                caseDestroyed += 1;
            if (ceCaseDestroyed2.CheckState == CheckState.Checked)
                caseDestroyed += 2;
            if (ceCaseDestroyed3.CheckState == CheckState.Checked)
                caseDestroyed += 4;

            if (ceCaseScratch1.CheckState == CheckState.Checked)
                caseScratch += 1;
            if (ceCaseScratch2.CheckState == CheckState.Checked)
                caseScratch += 2;
            if (ceCaseScratch3.CheckState == CheckState.Checked)
                caseScratch += 4;

            if (ceCaseStabbed1.CheckState == CheckState.Checked)
                caseStabbed += 1;
            if (ceCaseStabbed2.CheckState == CheckState.Checked)
                caseStabbed += 2;
            if (ceCaseStabbed3.CheckState == CheckState.Checked)
                caseStabbed += 4;

            if (ceCasePressed1.CheckState == CheckState.Checked)
                casePressed += 1;
            if (ceCasePressed2.CheckState == CheckState.Checked)
                casePressed += 2;
            if (ceCasePressed3.CheckState == CheckState.Checked)
                casePressed += 4;

            if (ceCaseDiscolored1.CheckState == CheckState.Checked)
                caseDiscolored += 1;
            if (ceCaseDiscolored2.CheckState == CheckState.Checked)
                caseDiscolored += 2;
            if (ceCaseDiscolored3.CheckState == CheckState.Checked)
                caseDiscolored += 4;


            short menu = ceMenu.CheckState == CheckState.Checked ? (short)1 : (short)0;


            if (ceDisplay1.CheckState == CheckState.Checked)
                display += 1;
            if (ceDisplay2.CheckState == CheckState.Checked)
                display += 2;
            if (ceDisplay3.CheckState == CheckState.Checked)
                display += 4;
            if (ceDisplay4.CheckState == CheckState.Checked)
                display += 8;
            if (ceDisplay5.CheckState == CheckState.Checked)
                display += 16;
            if (ceDisplay6.CheckState == CheckState.Checked)
                display += 32;
            if (ceDisplay7.CheckState == CheckState.Checked)
                display += 64;
            if (ceDisplay8.CheckState == CheckState.Checked)
                display += 128;
            if (ceDisplay9.CheckState == CheckState.Checked)
                display += 256;

            if (cePort1.CheckState == CheckState.Checked)
                port += 1;
            if (cePort2.CheckState == CheckState.Checked)
                port += 2;
            if (cePort3.CheckState == CheckState.Checked)
                port += 4;
            if (cePort4.CheckState == CheckState.Checked)
                port += 8;

            if (ceAdaptor1.CheckState == CheckState.Checked)
                adapter += 1;
            if (ceAdaptor2.CheckState == CheckState.Checked)
                adapter += 2;
            if (ceAdaptor3.CheckState == CheckState.Checked)
                adapter += 4;


            if (ceUsb1.CheckState == CheckState.Checked)
                usb += 1;
            if (ceUsb2.CheckState == CheckState.Checked)
                usb += 2;

            if (ceMousePad1.CheckState == CheckState.Checked)
                mousePad += 1;
            if (ceMousePad2.CheckState == CheckState.Checked)
                mousePad += 2;
            if (ceMousePad3.CheckState == CheckState.Checked)
                mousePad += 4;


            if (ceKeyboard1.CheckState == CheckState.Checked)
                keyboard += 1;
            if (ceKeyboard2.CheckState == CheckState.Checked)
                keyboard += 2;
            if (ceKeyboard3.CheckState == CheckState.Checked)
                keyboard += 4;
            if (ceKeyboard4.CheckState == CheckState.Checked)
                keyboard += 8;
            if (ceKeyboard5.CheckState == CheckState.Checked)
                keyboard += 16;

            if (ceCam1.CheckState == CheckState.Checked)
                cam += 1;
            if (ceCam2.CheckState == CheckState.Checked)
                cam += 2;
            if (ceCam3.CheckState == CheckState.Checked)
                cam += 4;


            if (ceOdd1.CheckState == CheckState.Checked)
                odd += 1;
            if (ceOdd2.CheckState == CheckState.Checked)
                odd += 2;
            if (ceOdd3.CheckState == CheckState.Checked)
                odd += 4;
            if (ceOdd4.CheckState == CheckState.Checked)
                odd += 8;
            if (ceOdd5.CheckState == CheckState.Checked)
                odd += 16;
            if (ceOdd6.CheckState == CheckState.Checked)
                odd += 32;

            if (ceHdd1.CheckState == CheckState.Checked)
                hdd += 1;
            if (ceHdd2.CheckState == CheckState.Checked)
                hdd += 2;
            if (ceHdd3.CheckState == CheckState.Checked)
                hdd += 4;
            if (ceHdd4.CheckState == CheckState.Checked)
                hdd += 8;
            if (ceHdd5.CheckState == CheckState.Checked)
                hdd += 16;


            if (ceLanWireless1.CheckState == CheckState.Checked)
                lanWireless += 1;
            if (ceLanWireless2.CheckState == CheckState.Checked)
                lanWireless += 2;

            if (ceLanWired1.CheckState == CheckState.Checked)
                lanWired += 1;
            if (ceLanWired2.CheckState == CheckState.Checked)
                lanWired += 2;

            if (ceBios1.CheckState == CheckState.Checked)
                bios += 1;
            if (ceBios2.CheckState == CheckState.Checked)
                bios += 2;


            if (ceOs1.CheckState == CheckState.Checked)
                os += 1;


            if (ceCheck1.CheckState == CheckState.Checked)
                check += 1;
            if (ceCheck2.CheckState == CheckState.Checked)
                check += 2;
            if (ceCheck3.CheckState == CheckState.Checked)
                check += 4;
            if (ceCheck4.CheckState == CheckState.Checked)
                check += 8;

            if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0 || menu > 0)
            {
                lbCase.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbCase.BackColor = Color.Transparent;
            }

           
            if (display > 0)
            {
                lbDisplay.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbDisplay.BackColor = Color.Transparent;
            }

            if (port > 0)
            {
                lbPort.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbPort.BackColor = Color.Transparent;
            }

            if (adapter > 0)
            {
                lbAdapter.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbAdapter.BackColor = Color.Transparent;
            }

            if (usb > 0)
            {
                lbUsb.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbUsb.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam3.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair3.EditValue);

            if (mousePad > 0)
            {
                lbMousePad.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbMousePad.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam4.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair4.EditValue);

            if (keyboard > 0)
            {
                lbKeyboard.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbKeyboard.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam5.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair5.EditValue);

           
            if (cam > 0)
            {
                lbCam.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbCam.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam7.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair7.EditValue);
            if (odd > 0)
            {
                lbOdd.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbOdd.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam8.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair8.EditValue);

            if (hdd > 0)
            {
                lbHdd.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbHdd.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam9.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair9.EditValue);

            if (lanWireless > 0)
            {
                lbLanWireless.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbLanWireless.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam10.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair10.EditValue);

 
            if (lanWired > 0)
            {
                lbLanWired.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbLanWired.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam11.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair11.EditValue);

            if (bios > 0)
            {
                lbBios.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbBios.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam12.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair12.EditValue);

            if (os > 0)
            {
                lbOs.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbOs.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam13.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair13.EditValue);

            if (check > 0)
            {
                lbCheck.BackColor = Color.LightSkyBlue;
            }
            else
            {
                lbCheck.BackColor = Color.Transparent;
            }

            //if (_checkType == 1)
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seExam14.EditValue);
            //else 
            //    _dtAllInOneAdjustmentPrice.Rows[1][col] = ConvertUtil.ToInt64(seRepair14.EditValue);

            _palletNo = ConvertUtil.ToInt64(lePallet.EditValue);

        }

        private void SaveCheckInfo()
        {
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);
            short caseDestroyed = 0;
            short caseScratch = 0;
            short caseStabbed = 0;
            short casePressed = 0;
            short caseDiscolored = 0;
            _etcDes = teEtc.Text;

            short display = 0;
            short port = 0;
            short adapter = 0;
            short usb = 0;
            short mousePad = 0;
            short keyboard = 0;
            short battery = 0;
            short cam = 0;
            short odd = 0;

            short lanWireless = 0;
            short lanWired = 0;
            short hdd = 0;
            short bios = 0;
            short os = 0;
            short check = 0;

            if (ceCaseDestroyed1.CheckState == CheckState.Checked)
                caseDestroyed += 1;
            if (ceCaseDestroyed2.CheckState == CheckState.Checked)
                caseDestroyed += 2;
            if (ceCaseDestroyed3.CheckState == CheckState.Checked)
                caseDestroyed += 4;


            if (ceCaseScratch1.CheckState == CheckState.Checked)
                caseScratch += 1;
            if (ceCaseScratch2.CheckState == CheckState.Checked)
                caseScratch += 2;
            if (ceCaseScratch3.CheckState == CheckState.Checked)
                caseScratch += 4;

            if (ceCaseStabbed1.CheckState == CheckState.Checked)
                caseStabbed += 1;
            if (ceCaseStabbed2.CheckState == CheckState.Checked)
                caseStabbed += 2;
            if (ceCaseStabbed3.CheckState == CheckState.Checked)
                caseStabbed += 4;

            if (ceCasePressed1.CheckState == CheckState.Checked)
                casePressed += 1;
            if (ceCasePressed2.CheckState == CheckState.Checked)
                casePressed += 2;
            if (ceCasePressed3.CheckState == CheckState.Checked)
                casePressed += 4;

            if (ceCaseDiscolored1.CheckState == CheckState.Checked)
                caseDiscolored += 1;
            if (ceCaseDiscolored2.CheckState == CheckState.Checked)
                caseDiscolored += 2;
            if (ceCaseDiscolored3.CheckState == CheckState.Checked)
                caseDiscolored += 4;


            short menu = ceMenu.CheckState == CheckState.Checked ? (short)1 : (short)0;

            if (ceDisplay1.CheckState == CheckState.Checked)
                display += 1;
            if (ceDisplay2.CheckState == CheckState.Checked)
                display += 2;
            if (ceDisplay3.CheckState == CheckState.Checked)
                display += 4;
            if (ceDisplay4.CheckState == CheckState.Checked)
                display += 8;
            if (ceDisplay5.CheckState == CheckState.Checked)
                display += 16;
            if (ceDisplay6.CheckState == CheckState.Checked)
                display += 32;
            if (ceDisplay7.CheckState == CheckState.Checked)
                display += 64;
            if (ceDisplay8.CheckState == CheckState.Checked)
                display += 128;
            if (ceDisplay9.CheckState == CheckState.Checked)
                display += 256;

            if (cePort1.CheckState == CheckState.Checked)
                port += 1;
            if (cePort2.CheckState == CheckState.Checked)
                port += 2;
            if (cePort3.CheckState == CheckState.Checked)
                port += 4;
            if (cePort4.CheckState == CheckState.Checked)
                port += 8;

            if (ceAdaptor1.CheckState == CheckState.Checked)
                adapter += 1;
            if (ceAdaptor2.CheckState == CheckState.Checked)
                adapter += 2;
            if (ceAdaptor3.CheckState == CheckState.Checked)
                adapter += 4;


            if (ceUsb1.CheckState == CheckState.Checked)
                usb += 1;
            if (ceUsb2.CheckState == CheckState.Checked)
                usb += 2;

            if (ceMousePad1.CheckState == CheckState.Checked)
                mousePad += 1;
            if (ceMousePad2.CheckState == CheckState.Checked)
                mousePad += 2;
            if (ceMousePad3.CheckState == CheckState.Checked)
                mousePad += 4;


            if (ceKeyboard1.CheckState == CheckState.Checked)
                keyboard += 1;
            if (ceKeyboard2.CheckState == CheckState.Checked)
                keyboard += 2;
            if (ceKeyboard3.CheckState == CheckState.Checked)
                keyboard += 4;
            if (ceKeyboard4.CheckState == CheckState.Checked)
                keyboard += 8;
            if (ceKeyboard5.CheckState == CheckState.Checked)
                keyboard += 16;

            if (ceCam1.CheckState == CheckState.Checked)
                cam += 1;
            if (ceCam2.CheckState == CheckState.Checked)
                cam += 2;
            if (ceCam3.CheckState == CheckState.Checked)
                cam += 4;


            if (ceOdd1.CheckState == CheckState.Checked)
                odd += 1;
            if (ceOdd2.CheckState == CheckState.Checked)
                odd += 2;
            if (ceOdd3.CheckState == CheckState.Checked)
                odd += 4;
            if (ceOdd4.CheckState == CheckState.Checked)
                odd += 8;
            if (ceOdd5.CheckState == CheckState.Checked)
                odd += 16;
            if (ceOdd6.CheckState == CheckState.Checked)
                odd += 32;

            if (ceHdd1.CheckState == CheckState.Checked)
                hdd += 1;
            if (ceHdd2.CheckState == CheckState.Checked)
                hdd += 2;
            if (ceHdd3.CheckState == CheckState.Checked)
                hdd += 4;
            if (ceHdd4.CheckState == CheckState.Checked)
                hdd += 8;
            if (ceHdd5.CheckState == CheckState.Checked)
                hdd += 16;


            if (ceLanWireless1.CheckState == CheckState.Checked)
                lanWireless += 1;
            if (ceLanWireless2.CheckState == CheckState.Checked)
                lanWireless += 2;

            if (ceLanWired1.CheckState == CheckState.Checked)
                lanWired += 1;
            if (ceLanWired2.CheckState == CheckState.Checked)
                lanWired += 2;

            if (ceBios1.CheckState == CheckState.Checked)
                bios += 1;
            if (ceBios2.CheckState == CheckState.Checked)
                bios += 2;

            if (ceOs1.CheckState == CheckState.Checked)
                os += 1;

            if (ceCheck1.CheckState == CheckState.Checked)
                check += 1;
            if (ceCheck2.CheckState == CheckState.Checked)
                check += 2;
            if (ceCheck3.CheckState == CheckState.Checked)
                check += 4;
            if (ceCheck4.CheckState == CheckState.Checked)
                check += 8;


            _DicAllInOneCheck["CASE_DESTROYED"] = caseDestroyed;
            _DicAllInOneCheck["CASE_SCRATCH"] = caseScratch;
            _DicAllInOneCheck["CASE_STABBED"] = caseStabbed;
            _DicAllInOneCheck["CASE_PRESSED"] = casePressed;
            _DicAllInOneCheck["CASE_DISCOLORED"] = caseDiscolored;
            _DicAllInOneCheck["MENU"] = menu;
            _DicAllInOneCheck["DISPLAY"] = display;
            _DicAllInOneCheck["PORT"] = port;
            _DicAllInOneCheck["ADAPTER"] = adapter;
            _DicAllInOneCheck["USB"] = usb;
            _DicAllInOneCheck["MOUSEPAD"] = mousePad;
            _DicAllInOneCheck["KEYBOARD"] = keyboard;
            _DicAllInOneCheck["CAM"] = cam;
            _DicAllInOneCheck["ODD"] = odd;
            _DicAllInOneCheck["HDD"] = hdd;
            _DicAllInOneCheck["LAN_WIRELESS"] = lanWireless;
            _DicAllInOneCheck["LAN_WIRED"] = lanWired;
            _DicAllInOneCheck["BIOS"] = bios;
            _DicAllInOneCheck["OS"] = os;
            _DicAllInOneCheck["TEST_CHECK"] = check;

            for (int i = 0; i < ExamineInfo._listAdjustmentAllInOnePriceColShort.Count; i++)
            {
                SpinEdit seExam = listSpinEditExam[i];
                SpinEdit seRepair = listSpinEditRepair[i];
                string col = ExamineInfo._listAdjustmentAllInOnePriceColShort[i];

                if (_checkType == 1)
                    _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(seExam.EditValue);
                else
                    _dtAllInOneAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(seRepair.EditValue);
            }


            if (_checkType == 1)
                _dtAllInOneAdjustmentPrice.Rows[_checkType]["ETC"] = ConvertUtil.ToInt64(seExamEtc.EditValue);
            else
                _dtAllInOneAdjustmentPrice.Rows[_checkType]["ETC"] = ConvertUtil.ToInt64(seRepairEtc.EditValue);
        }

        private void sbPrint_Click(object sender, EventArgs e)
        {
            ProjectInfo._printerPort = lePort.EditValue.ToString();

            if (_checkType == 1)
                CheckInfo();
            else
                CheckInfoForSave();

            if (_checkType == 1)
            {
                if (_pGrade.Equals("0"))
                {
                    if (MessageBox.Show("제품등급이 '등급없음' 입니다. 그대로 진행하시겠습니까?", "제품등급 확인", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            else if (_checkType == 3)
            {
                if (_pGrade.Equals("0"))
                {
                    if (MessageBox.Show("제품등급이 '등급없음' 입니다. 그대로 진행하시겠습니까?", "제품등급 확인", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (_pGrade.Equals("4"))
                {
                    if (MessageBox.Show("제품등급이 '리페어' 입니다. 그대로 진행하시겠습니까?", "제품등급 확인", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (Dangol.MessageYN("검수 결과를 프린트 하시겠습니까? 검수결과는 자동으로 저장됩니다.", "검수 저장 확인") == DialogResult.Yes)
            {
                SaveCheckInfo();
                _isPrint = true;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
            }
        }

        private void setInit()
        {
            for (int i = 0; i < ExamineInfo._listAdjustmentAllInOnePriceColShort.Count; i++)
            {
                SpinEdit seExam = listSpinEditExam[i];
                SpinEdit seRepair = listSpinEditRepair[i];
                string col = ExamineInfo._listAdjustmentAllInOnePriceColShort[i];

                if (_checkType == 1)
                    seExam.EditValue = _dtAllInOneAdjustmentPrice.Rows[_checkType][col];
                else
                {
                    seExam.EditValue = _dtAllInOneAdjustmentPrice.Rows[1][col];
                    seRepair.EditValue = _dtAllInOneAdjustmentPrice.Rows[_checkType][col];
                }

                //seExam.Text = ConvertUtil.ToString(_dtAllInOneAdjustmentPrice.Rows[1][col]);
                //seRepair.Text = ConvertUtil.ToString(_dtAllInOneAdjustmentPrice.Rows[3][col]);
            }           
        }

        private void lgcNtbCheck_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            CheckInfo();
        }
    }



    
}