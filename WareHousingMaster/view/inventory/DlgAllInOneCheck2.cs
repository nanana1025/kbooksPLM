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
    public partial class DlgAllInOneCheck2 : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, short> _DicAllInOneCheck;
        public bool _isPrint { get; set; }

        DataTable _dtPort;

        public DlgAllInOneCheck2(Dictionary<string, short> dicAllInOneCheck, DataTable dtPort)
        {
            InitializeComponent();

            _DicAllInOneCheck = dicAllInOneCheck;

            _dtPort = dtPort;
            _isPrint = false;
        }

        private void DlgAllInOneCheck_Load(object sender, EventArgs e)
        {

            if (_DicAllInOneCheck == null)
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                Util.LookupEditHelper(lePort, _dtPort, "KEY", "VALUE");
                lePort.EditValue = ProjectInfo._printerPort;

                if (_DicAllInOneCheck.Count > 0)
                {
                    short caseDestroyed = _DicAllInOneCheck["CASE_DESTROYED"];
                    short caseScratch = _DicAllInOneCheck["CASE_SCRATCH"];
                    short caseStabbed = _DicAllInOneCheck["CASE_STABBED"];
                    short casePressed = _DicAllInOneCheck["CASE_PRESSED"];
                    short caseDiscolored = _DicAllInOneCheck["CASE_DISCOLORED"];
                    short display = _DicAllInOneCheck["DISPLAY"];
                    short cam = _DicAllInOneCheck["CAM"];
                    short usb = _DicAllInOneCheck["USB"];
                    short sound = _DicAllInOneCheck["SOUND"];
                    short lanWireless = _DicAllInOneCheck["LAN_WIRELESS"];
                    short lanWired = _DicAllInOneCheck["LAN_WIRED"];
                    short hdd = _DicAllInOneCheck["HDD"];
                    short odd = _DicAllInOneCheck["ODD"];
                    short adapter = _DicAllInOneCheck["ADAPTER"];
                    short bios = _DicAllInOneCheck["BIOS"];
                    short os = _DicAllInOneCheck["OS"];


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

                    rgDisplay.EditValue = display;
                    rgCam.EditValue = cam;
                    rgUsb.EditValue = usb;
                    rgSound.EditValue = sound;
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

                    if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0)
                        lbCase.BackColor = Color.LightSkyBlue;
                    if (display > 0)
                        lbDisplay.BackColor = Color.LightSkyBlue;
                    if (cam > 0)
                        lbCam.BackColor = Color.LightSkyBlue;
                    if (usb > 0)
                        lbUsb.BackColor = Color.LightSkyBlue;
                    if (sound > 0)
                        lbSound.BackColor = Color.LightSkyBlue;
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
                    _DicAllInOneCheck.Add("CASE_DESTROYED", 0);
                    _DicAllInOneCheck.Add("CASE_SCRATCH", 0);
                    _DicAllInOneCheck.Add("CASE_STABBED", 0);
                    _DicAllInOneCheck.Add("CASE_PRESSED", 0);
                    _DicAllInOneCheck.Add("CASE_DISCOLORED", 0);
                    _DicAllInOneCheck.Add("DISPLAY", 0);
                    _DicAllInOneCheck.Add("CAM", 0);
                    _DicAllInOneCheck.Add("USB", 0);
                    _DicAllInOneCheck.Add("SOUND", 0);
                    _DicAllInOneCheck.Add("LAN_WIRELESS", 0);
                    _DicAllInOneCheck.Add("LAN_WIRED", 0);
                    _DicAllInOneCheck.Add("HDD", 0);
                    _DicAllInOneCheck.Add("ODD", 0);
                    _DicAllInOneCheck.Add("ADAPTER", 0);
                    _DicAllInOneCheck.Add("BIOS", 0);
                    _DicAllInOneCheck.Add("OS", 0);
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

            short display = ConvertUtil.ToInt16(rgDisplay.EditValue);
            short cam = ConvertUtil.ToInt16(rgCam.EditValue);
            short usb = ConvertUtil.ToInt16(rgUsb.EditValue);
            short sound = ConvertUtil.ToInt16(rgSound.EditValue);
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


            if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0)
                lbCase.BackColor = Color.LightSkyBlue;
            else
                lbCase.BackColor = Color.Transparent;
            if (display > 0)
                lbDisplay.BackColor = Color.LightSkyBlue;
            else
                lbDisplay.BackColor = Color.Transparent;
            if (cam > 0)
                lbCam.BackColor = Color.LightSkyBlue;
            else
                lbCam.BackColor = Color.Transparent;
            if (usb > 0)
                lbUsb.BackColor = Color.LightSkyBlue;
            else
                lbUsb.BackColor = Color.Transparent;
            if (sound > 0)
                lbSound.BackColor = Color.LightSkyBlue;
            else
                lbSound.BackColor = Color.Transparent;
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

            short display = ConvertUtil.ToInt16(rgDisplay.EditValue);
            short cam = ConvertUtil.ToInt16(rgCam.EditValue);
            short usb = ConvertUtil.ToInt16(rgUsb.EditValue);
            short sound = ConvertUtil.ToInt16(rgSound.EditValue);
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


            _DicAllInOneCheck["CASE_DESTROYED"] = caseDestroyed;
            _DicAllInOneCheck["CASE_SCRATCH"] = caseScratch;
            _DicAllInOneCheck["CASE_STABBED"] = caseStabbed;
            _DicAllInOneCheck["CASE_PRESSED"] = casePressed;
            _DicAllInOneCheck["CASE_DISCOLORED"] = caseDiscolored;
            _DicAllInOneCheck["DISPLAY"] = display;
            _DicAllInOneCheck["CAM"] = cam;
            _DicAllInOneCheck["USB"] = usb;
            _DicAllInOneCheck["SOUND"] = sound;
            _DicAllInOneCheck["LAN_WIRELESS"] = lanWireless;
            _DicAllInOneCheck["LAN_WIRED"] = lanWired;
            _DicAllInOneCheck["HDD"] = hdd;
            _DicAllInOneCheck["ODD"] = odd;
            _DicAllInOneCheck["ADAPTER"] = adapter;
            _DicAllInOneCheck["BIOS"] = bios;
            _DicAllInOneCheck["OS"] = os;
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