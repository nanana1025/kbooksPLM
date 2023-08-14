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

namespace WareHousingMaster.view.inventory
{
    public partial class DlgNtbCheck : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, short> _DicNtbCheck;

        public bool _isPrint { get; set; }
        public long _palletNo { get; set; }

        DataTable _dtPort;

        BindingSource _bsPallet;

        public DlgNtbCheck(Dictionary<string, short> dicNtbCheck, DataTable dtPort, BindingSource bsPallet, long palletNo)
        {
            InitializeComponent();

            _DicNtbCheck = dicNtbCheck;

            _dtPort = dtPort;
            _isPrint = false;
            _bsPallet = bsPallet;
            _palletNo = palletNo;
        }

        private void DlgNtbCheck_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            if (_DicNtbCheck == null)
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                Util.LookupEditHelper(lePort, _dtPort, "KEY", "VALUE");
                lePort.EditValue = ProjectInfo._printerPort;

                Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");
                lePallet.EditValue = $"{ _palletNo }";

                if (_DicNtbCheck.Count > 0)
                {
                    short caseDestroyed = _DicNtbCheck["CASE_DESTROYED"];
                    short caseScratch = _DicNtbCheck["CASE_SCRATCH"];
                    short caseStabbed = _DicNtbCheck["CASE_STABBED"];
                    short casePressed = _DicNtbCheck["CASE_PRESSED"];
                    short caseDiscolored = _DicNtbCheck["CASE_DISCOLORED"];
                    short caseHinge = _DicNtbCheck["CASE_HINGE"];
                    short display = _DicNtbCheck["DISPLAY"];
                    short battery = _DicNtbCheck["BATTERY"];
                    short mousePad = _DicNtbCheck["MOUSEPAD"];
                    short keyboard = _DicNtbCheck["KEYBOARD"];
                    short cam = _DicNtbCheck["CAM"];
                    short usb = _DicNtbCheck["USB"];
                    short lanWireless = _DicNtbCheck["LAN_WIRELESS"];
                    short lanWired = _DicNtbCheck["LAN_WIRED"];
                    short hdd = _DicNtbCheck["HDD"];
                    short odd = _DicNtbCheck["ODD"];
                    short adapter = _DicNtbCheck["ADAPTER"];
                    short bios = _DicNtbCheck["BIOS"];
                    short os = _DicNtbCheck["OS"];


                    if ((caseDestroyed & 1) == 1)
                        ceCaseDestroyed1.CheckState = CheckState.Checked;
                    if ((caseDestroyed & 2) == 2)
                        ceCaseDestroyed2.CheckState = CheckState.Checked;
                    if ((caseDestroyed & 4) == 4)
                        ceCaseDestroyed3.CheckState = CheckState.Checked;
                    if ((caseDestroyed & 8) == 8)
                        ceCaseDestroyed4.CheckState = CheckState.Checked;

                    if ((caseScratch & 1) == 1)
                        ceCaseScratch1.CheckState = CheckState.Checked;
                    if ((caseScratch & 2) == 2)
                        ceCaseScratch2.CheckState = CheckState.Checked;
                    if ((caseScratch & 4) == 4)
                        ceCaseScratch3.CheckState = CheckState.Checked;
                    if ((caseScratch & 8) == 8)
                        ceCaseScratch4.CheckState = CheckState.Checked;

                    if ((caseStabbed & 1) == 1)
                        ceCaseStabbed1.CheckState = CheckState.Checked;
                    if ((caseStabbed & 2) == 2)
                        ceCaseStabbed2.CheckState = CheckState.Checked;
                    if ((caseStabbed & 4) == 4)
                        ceCaseStabbed3.CheckState = CheckState.Checked;
                    if ((caseStabbed & 8) == 8)
                        ceCaseStabbed4.CheckState = CheckState.Checked;

                    if ((casePressed & 1) == 1)
                        ceCasePressed1.CheckState = CheckState.Checked;
                    if ((casePressed & 2) == 2)
                        ceCasePressed2.CheckState = CheckState.Checked;
                    if ((casePressed & 4) == 4)
                        ceCasePressed3.CheckState = CheckState.Checked;
                    if ((casePressed & 8) == 8)
                        ceCasePressed4.CheckState = CheckState.Checked;

                    if ((caseDiscolored & 1) == 1)
                        ceCaseDiscolored1.CheckState = CheckState.Checked;
                    if ((caseDiscolored & 2) == 2)
                        ceCaseDiscolored2.CheckState = CheckState.Checked;
                    if ((caseDiscolored & 4) == 4)
                        ceCaseDiscolored3.CheckState = CheckState.Checked;
                    if ((caseDiscolored & 8) == 8)
                        ceCaseDiscolored4.CheckState = CheckState.Checked;

                    if (caseHinge == 1)
                        ceCaseHinge.CheckState = CheckState.Checked;

                    rgDisplay.EditValue = display;
                    rgBattery.EditValue = battery;
                    rgMousepad.EditValue = mousePad;
                    rgKeyboard.EditValue = keyboard;
                    rgCam.EditValue = cam;
                    rgUsb.EditValue = usb;
                    rgLanWireless.EditValue = lanWireless;
                    rgLanWired.EditValue = lanWired;

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


                    rgOdd.EditValue = odd;
                    rgAdapter.EditValue = adapter;
                    rgBios.EditValue = bios;
                    rgOs.EditValue = os;

                    if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0 || caseHinge > 0)
                        lbCase.BackColor = Color.LightSkyBlue;
                    if (display > 0)
                        lbDisplay.BackColor = Color.LightSkyBlue;
                    if (battery > 0)
                        lbBattery.BackColor = Color.LightSkyBlue;
                    if (mousePad > 0)
                        lbMousePad.BackColor = Color.LightSkyBlue;
                    if (keyboard > 0)
                        lbKeyboard.BackColor = Color.LightSkyBlue;
                    if (cam > 0)
                        lbCam.BackColor = Color.LightSkyBlue;
                    if (usb > 0)
                        lbUsb.BackColor = Color.LightSkyBlue;
                    if (lanWireless > 0)
                        lbLanWireless.BackColor = Color.LightSkyBlue;
                    if (lanWired > 0)
                        lbLanWired.BackColor = Color.LightSkyBlue;
                    if (hdd > 0)
                        lbHdd.BackColor = Color.LightSkyBlue;
                    if (odd > 0)
                        lbOdd.BackColor = Color.LightSkyBlue;
                    if (adapter > 0)
                        lbAdapter.BackColor = Color.LightSkyBlue;
                    if (bios > 0)
                        lbBios.BackColor = Color.LightSkyBlue;
                    if (os > 0)
                        lbOs.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    _DicNtbCheck.Add("CASE_DESTROYED", 0);
                    _DicNtbCheck.Add("CASE_SCRATCH", 0);
                    _DicNtbCheck.Add("CASE_STABBED", 0);
                    _DicNtbCheck.Add("CASE_PRESSED", 0);
                    _DicNtbCheck.Add("CASE_DISCOLORED", 0);
                    _DicNtbCheck.Add("CASE_HINGE", 0);
                    _DicNtbCheck.Add("DISPLAY", 0);
                    _DicNtbCheck.Add("BATTERY", 0);
                    _DicNtbCheck.Add("MOUSEPAD", 0);
                    _DicNtbCheck.Add("KEYBOARD", 0);
                    _DicNtbCheck.Add("CAM", 0);
                    _DicNtbCheck.Add("USB", 0);
                    _DicNtbCheck.Add("LAN_WIRELESS", 0);
                    _DicNtbCheck.Add("LAN_WIRED", 0);
                    _DicNtbCheck.Add("HDD", 0);
                    _DicNtbCheck.Add("ODD", 0);
                    _DicNtbCheck.Add("ADAPTER", 0);
                    _DicNtbCheck.Add("BIOS", 0);
                    _DicNtbCheck.Add("OS", 0);
                }
            }
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            CheckInfo(); 

            if (MessageBox.Show("검수완료하시겠습니까?", "검수 저장 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
 
            if (ceCaseDestroyed1.CheckState == CheckState.Checked)
                caseDestroyed += 1;
            if (ceCaseDestroyed2.CheckState == CheckState.Checked)
                caseDestroyed += 2;
            if (ceCaseDestroyed3.CheckState == CheckState.Checked)
                caseDestroyed += 4;
            if (ceCaseDestroyed4.CheckState == CheckState.Checked)
                caseDestroyed += 8;

            if (ceCaseScratch1.CheckState == CheckState.Checked)
                caseScratch += 1;
            if (ceCaseScratch2.CheckState == CheckState.Checked)
                caseScratch += 2;
            if (ceCaseScratch3.CheckState == CheckState.Checked)
                caseScratch += 4;
            if (ceCaseScratch4.CheckState == CheckState.Checked)
                caseScratch += 8;

            if (ceCaseStabbed1.CheckState == CheckState.Checked)
                caseStabbed += 1;
            if (ceCaseStabbed2.CheckState == CheckState.Checked)
                caseStabbed += 2;
            if (ceCaseStabbed3.CheckState == CheckState.Checked)
                caseStabbed += 4;
            if (ceCaseStabbed4.CheckState == CheckState.Checked)
                caseStabbed += 8;

            if (ceCasePressed1.CheckState == CheckState.Checked)
                casePressed += 1;
            if (ceCasePressed2.CheckState == CheckState.Checked)
                casePressed += 2;
            if (ceCasePressed3.CheckState == CheckState.Checked)
                casePressed += 4;
            if (ceCasePressed4.CheckState == CheckState.Checked)
                casePressed += 8;

            if (ceCaseDiscolored1.CheckState == CheckState.Checked)
                caseDiscolored += 1;
            if (ceCaseDiscolored2.CheckState == CheckState.Checked)
                caseDiscolored += 2;
            if (ceCaseDiscolored3.CheckState == CheckState.Checked)
                caseDiscolored += 4;
            if (ceCaseDiscolored4.CheckState == CheckState.Checked)
                caseDiscolored += 8;

            
            short caseHinge = ceCaseHinge.CheckState == CheckState.Checked ? (short)1 : (short)0;
            short display = ConvertUtil.ToInt16(rgDisplay.EditValue);
            short battery = ConvertUtil.ToInt16(rgBattery.EditValue);
            short mousePad = ConvertUtil.ToInt16(rgMousepad.EditValue);
            short keyboard = ConvertUtil.ToInt16(rgKeyboard.EditValue);
            short cam = ConvertUtil.ToInt16(rgCam.EditValue);
            short usb = ConvertUtil.ToInt16(rgUsb.EditValue);
            short lanWireless = ConvertUtil.ToInt16(rgLanWireless.EditValue);
            short lanWired = ConvertUtil.ToInt16(rgLanWired.EditValue);

            short hdd = 0;

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

            short odd = ConvertUtil.ToInt16(rgOdd.EditValue);
            short adapter = ConvertUtil.ToInt16(rgAdapter.EditValue);
            short bios = ConvertUtil.ToInt16(rgBios.EditValue);
            short os = ConvertUtil.ToInt16(rgOs.EditValue);


            if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0 || caseHinge > 0)
                lbCase.BackColor = Color.LightSkyBlue;
            else
                lbCase.BackColor = Color.Transparent;
            if (display > 0)
                lbDisplay.BackColor = Color.LightSkyBlue;
            else
                lbDisplay.BackColor = Color.Transparent;
            if (battery > 0)
                lbBattery.BackColor = Color.LightSkyBlue;
            else
                lbBattery.BackColor = Color.Transparent;
            if (mousePad > 0)
                lbMousePad.BackColor = Color.LightSkyBlue;
            else
                lbMousePad.BackColor = Color.Transparent;
            if (keyboard > 0)
                lbKeyboard.BackColor = Color.LightSkyBlue;
            else
                lbKeyboard.BackColor = Color.Transparent;
            if (cam > 0)
                lbCam.BackColor = Color.LightSkyBlue;
            else
                lbCam.BackColor = Color.Transparent;
            if (usb > 0)
                lbUsb.BackColor = Color.LightSkyBlue;
            else
                lbUsb.BackColor = Color.Transparent;
            if (lanWireless > 0)
                lbLanWireless.BackColor = Color.LightSkyBlue;
            else
                lbLanWireless.BackColor = Color.Transparent;
            if (lanWired > 0)
                lbLanWired.BackColor = Color.LightSkyBlue;
            else
                lbLanWired.BackColor = Color.Transparent;
            if (hdd > 0)
                lbHdd.BackColor = Color.LightSkyBlue;
            else
                lbHdd.BackColor = Color.Transparent;
            if (odd > 0)
                lbOdd.BackColor = Color.LightSkyBlue;
            else
                lbOdd.BackColor = Color.Transparent;
            if (adapter > 0)
                lbAdapter.BackColor = Color.LightSkyBlue;
            else
                lbAdapter.BackColor = Color.Transparent;
            if (bios > 0)
                lbBios.BackColor = Color.LightSkyBlue;
            else
                lbBios.BackColor = Color.Transparent;
            if (os > 0)
                lbOs.BackColor = Color.LightSkyBlue;
            else
                lbOs.BackColor = Color.Transparent;

            _palletNo = ConvertUtil.ToInt64(lePallet.EditValue);

        }

        private void SaveCheckInfo()
        {

            short caseDestroyed = 0;
            short caseScratch = 0;
            short caseStabbed = 0;
            short casePressed = 0;
            short caseDiscolored = 0;

            if (ceCaseDestroyed1.CheckState == CheckState.Checked)
                caseDestroyed += 1;
            if (ceCaseDestroyed2.CheckState == CheckState.Checked)
                caseDestroyed += 2;
            if (ceCaseDestroyed3.CheckState == CheckState.Checked)
                caseDestroyed += 4;
            if (ceCaseDestroyed4.CheckState == CheckState.Checked)
                caseDestroyed += 8;

            if (ceCaseScratch1.CheckState == CheckState.Checked)
                caseScratch += 1;
            if (ceCaseScratch2.CheckState == CheckState.Checked)
                caseScratch += 2;
            if (ceCaseScratch3.CheckState == CheckState.Checked)
                caseScratch += 4;
            if (ceCaseScratch4.CheckState == CheckState.Checked)
                caseScratch += 8;

            if (ceCaseStabbed1.CheckState == CheckState.Checked)
                caseStabbed += 1;
            if (ceCaseStabbed2.CheckState == CheckState.Checked)
                caseStabbed += 2;
            if (ceCaseStabbed3.CheckState == CheckState.Checked)
                caseStabbed += 4;
            if (ceCaseStabbed4.CheckState == CheckState.Checked)
                caseStabbed += 8;

            if (ceCasePressed1.CheckState == CheckState.Checked)
                casePressed += 1;
            if (ceCasePressed2.CheckState == CheckState.Checked)
                casePressed += 2;
            if (ceCasePressed3.CheckState == CheckState.Checked)
                casePressed += 4;
            if (ceCasePressed4.CheckState == CheckState.Checked)
                casePressed += 8;

            if (ceCaseDiscolored1.CheckState == CheckState.Checked)
                caseDiscolored += 1;
            if (ceCaseDiscolored2.CheckState == CheckState.Checked)
                caseDiscolored += 2;
            if (ceCaseDiscolored3.CheckState == CheckState.Checked)
                caseDiscolored += 4;
            if (ceCaseDiscolored4.CheckState == CheckState.Checked)
                caseDiscolored += 8;


            short caseHinge = ceCaseHinge.CheckState == CheckState.Checked ? (short)1 : (short)0;
            short display = ConvertUtil.ToInt16(rgDisplay.EditValue);
            short battery = ConvertUtil.ToInt16(rgBattery.EditValue);
            short mousePad = ConvertUtil.ToInt16(rgMousepad.EditValue);
            short keyboard = ConvertUtil.ToInt16(rgKeyboard.EditValue);
            short cam = ConvertUtil.ToInt16(rgCam.EditValue);
            short usb = ConvertUtil.ToInt16(rgUsb.EditValue);
            short lanWireless = ConvertUtil.ToInt16(rgLanWireless.EditValue);
            short lanWired = ConvertUtil.ToInt16(rgLanWired.EditValue);

            short hdd = 0;

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

            short odd = ConvertUtil.ToInt16(rgOdd.EditValue);
            short adapter = ConvertUtil.ToInt16(rgAdapter.EditValue);
            short bios = ConvertUtil.ToInt16(rgBios.EditValue);
            short os = ConvertUtil.ToInt16(rgOs.EditValue);


            _DicNtbCheck["CASE_DESTROYED"] = caseDestroyed;
            _DicNtbCheck["CASE_SCRATCH"] = caseScratch;
            _DicNtbCheck["CASE_STABBED"] = caseStabbed;
            _DicNtbCheck["CASE_PRESSED"] = casePressed;
            _DicNtbCheck["CASE_DISCOLORED"] = caseDiscolored;
            _DicNtbCheck["CASE_HINGE"] = caseHinge;
            _DicNtbCheck["DISPLAY"] = display;
            _DicNtbCheck["BATTERY"] = battery;
            _DicNtbCheck["MOUSEPAD"] = mousePad;
            _DicNtbCheck["KEYBOARD"] = keyboard;
            _DicNtbCheck["CAM"] = cam;
            _DicNtbCheck["USB"] = usb;
            _DicNtbCheck["LAN_WIRELESS"] = lanWireless;
            _DicNtbCheck["LAN_WIRED"] = lanWired;
            _DicNtbCheck["HDD"] = hdd;
            _DicNtbCheck["ODD"] = odd;
            _DicNtbCheck["ADAPTER"] = adapter;
            _DicNtbCheck["BIOS"] = bios;
            _DicNtbCheck["OS"] = os;
        }

        private void sbPrint_Click(object sender, EventArgs e)
        {
            ProjectInfo._printerPort = lePort.EditValue.ToString();

            CheckInfo();

            if (Dangol.MessageYN("검수 결과를 프린트 하시겠습니까? 검수결과는 자동으로 저장됩니다.", "검수 저장 확인") == DialogResult.Yes)
            {
                SaveCheckInfo();

                //MessageBox.Show("검수완료되었습니다.");
                _isPrint = true;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                //MessageBox.Show("아니요 클릭");
            }
        }
    }



    
}