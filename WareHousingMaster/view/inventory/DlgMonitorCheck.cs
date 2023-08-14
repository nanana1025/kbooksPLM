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
using DevExpress.XtraEditors.Controls;

namespace WareHousingMaster.view.inventory
{
    public partial class DlgMonitorCheck : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, int> _DicMonitorCheck;

        public bool _isPrint { get; set; }
        public string _description { get; private set; }
        public string _pGrade { get; private set; }

        public string _size { get; private set; }

        DataTable _dtPort;
        DataTable _dtPGrade;

        public DlgMonitorCheck(Dictionary<string, int> dicMonitorCheck, string description, string pGrade, string size,DataTable dtPort, DataTable dtPGrade)
        {
            InitializeComponent();

            _DicMonitorCheck = dicMonitorCheck;

            _description = description;
            _pGrade = pGrade;
            _dtPort = dtPort;
            _dtPGrade = dtPGrade;
            _size = size;
            _isPrint = false;

        }

        private void DlgMonitorCheck_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            if (_DicMonitorCheck == null)
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                Util.LookupEditHelper(lePort, _dtPort, "KEY", "VALUE");
                lePort.EditValue = ProjectInfo._printerPort;

                Util.LookupEditHelper(lePGrade, _dtPGrade, "KEY", "VALUE");
                lePGrade.EditValue = _pGrade;

                DataTable dtMonDisplayType = Util.getCodeList("CD03011301", "KEY", "VALUE");
                DataTable dtMonPowerType = Util.getCodeList("CD03011302", "KEY", "VALUE");
                DataTable dtMonBrand = Util.getCodeList("CD03011303", "KEY", "VALUE");


                this.rgDisplayType.Properties.Columns = dtMonDisplayType.Rows.Count;
                RadioGroupItem[] rgItemDisplayType = new RadioGroupItem[dtMonDisplayType.Rows.Count];
                int index = 0;
                foreach (DataRow row in dtMonDisplayType.Rows)
                    rgItemDisplayType[index++] = new RadioGroupItem(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));
                this.rgDisplayType.Properties.Items.AddRange(rgItemDisplayType);
                this.rgDisplayType.Size = new System.Drawing.Size(dtMonDisplayType.Rows.Count * 50, 20);

                this.rgPowerType.Properties.Columns = dtMonPowerType.Rows.Count;
                RadioGroupItem[] rgItemPowerType = new RadioGroupItem[dtMonPowerType.Rows.Count];
                index = 0;
                foreach (DataRow row in dtMonPowerType.Rows)
                    rgItemPowerType[index++] = new RadioGroupItem(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));
                this.rgPowerType.Properties.Items.AddRange(rgItemPowerType);
                this.rgPowerType.Size = new System.Drawing.Size(dtMonPowerType.Rows.Count * 50, 20);

                this.rgBrand.Properties.Columns = dtMonBrand.Rows.Count;
                RadioGroupItem[] rgItemBrand = new RadioGroupItem[dtMonBrand.Rows.Count];
                index = 0;
                foreach (DataRow row in dtMonBrand.Rows)
                    rgItemBrand[index++] = new RadioGroupItem(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));
                this.rgBrand.Properties.Items.AddRange(rgItemBrand);
                this.rgBrand.Size = new System.Drawing.Size(dtMonBrand.Rows.Count * 50, 20);


                if (_DicMonitorCheck.Count > 0)
                {
                    int caseDestroyed = _DicMonitorCheck["CASE_DESTROYED"];
                    int caseScratch = _DicMonitorCheck["CASE_SCRATCH"];
                    int caseStabbed = _DicMonitorCheck["CASE_STABBED"];
                    int casePressed = _DicMonitorCheck["CASE_PRESSED"];
                    int caseDiscolored = _DicMonitorCheck["CASE_DISCOLORED"];
                    int display = _DicMonitorCheck["DISPLAY"];
                    int port = _DicMonitorCheck["PORT"];
                    int adapter = _DicMonitorCheck["ADAPTER"];
                    int type = _DicMonitorCheck["TYPE"];

                    int menu = _DicMonitorCheck["MENU"];
                    int powerType = _DicMonitorCheck["POWER_TYPE"];
                    int brand = _DicMonitorCheck["BRAND"];


                    string description = _description;

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
                    if ((display & 512) == 512)
                        ceDisplay10.CheckState = CheckState.Checked;

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

                    if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0 || menu > 0)
                        lbCase.BackColor = Color.LightSkyBlue;
                    if (display > 0)
                        lbDisplay.BackColor = Color.LightSkyBlue;
                    if (port > 0)
                        lbPort.BackColor = Color.LightSkyBlue;
                    if (adapter > 0)
                        lbAdapter.BackColor = Color.LightSkyBlue;

                    if (type > 0)
                        lbType.BackColor = Color.LightSkyBlue;
                    else
                        lbType.BackColor = Color.Transparent;

                    if (type > 0)
                        lbType.BackColor = Color.LightSkyBlue;
                    else
                        lbType.BackColor = Color.Transparent;

                    if (powerType > 0)
                        lbPowerType.BackColor = Color.LightSkyBlue;
                    else
                        lbPowerType.BackColor = Color.Transparent;

                    if (brand > 0)
                        lbBrand.BackColor = Color.LightSkyBlue;
                    else
                        lbBrand.BackColor = Color.Transparent;

                    if (!string.IsNullOrEmpty(_size))
                        lbSize.BackColor = Color.LightSkyBlue;
                    else
                        lbSize.BackColor = Color.Transparent;


                    rgDisplayType.EditValue = ConvertUtil.ToString(type);
                    rgPowerType.EditValue = ConvertUtil.ToString(powerType);
                    rgBrand.EditValue = ConvertUtil.ToString(brand);
                    if ((menu & 1) == 1)
                        ceMenu.CheckState = CheckState.Checked;


                    teSize.Text = _size;
                    teDescription.Text = description;
                }
                else
                {
                    _DicMonitorCheck.Add("CASE_DESTROYED", 0);
                    _DicMonitorCheck.Add("CASE_SCRATCH", 0);
                    _DicMonitorCheck.Add("CASE_STABBED", 0);
                    _DicMonitorCheck.Add("CASE_PRESSED", 0);
                    _DicMonitorCheck.Add("CASE_DISCOLORED", 0);
                    _DicMonitorCheck.Add("DISPLAY", 0);
                    _DicMonitorCheck.Add("PORT", 0);
                    _DicMonitorCheck.Add("ADAPTER", 0);
                    _DicMonitorCheck.Add("TYPE", 0);
                    _DicMonitorCheck.Add("MENU", 0);
                    _DicMonitorCheck.Add("POWER_TYPE", 0);
                    _DicMonitorCheck.Add("BRAND", 0);
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

            int caseDestroyed = 0;
            int caseScratch = 0;
            int caseStabbed = 0;
            int casePressed = 0;
            int caseDiscolored = 0;
            short display = 0;
            short adapter = 0;
            int type = 0;
            int menu = 0;
            int powerType = 0;
            int brand = 0;

            _size = teSize.Text;
            _description = teDescription.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);

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
            if (ceDisplay10.CheckState == CheckState.Checked)
                display += 512;

            int port = 0;

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

            type = ConvertUtil.ToInt32(rgDisplayType.EditValue);
            powerType = ConvertUtil.ToInt32(rgPowerType.EditValue);
            brand = ConvertUtil.ToInt32(rgBrand.EditValue);

            if (ceMenu.CheckState == CheckState.Checked)
                menu = 1;

            if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0 || menu > 0)
                lbCase.BackColor = Color.LightSkyBlue;
            else
                lbCase.BackColor = Color.Transparent;
            if (display > 0)
                lbDisplay.BackColor = Color.LightSkyBlue;
            else
                lbDisplay.BackColor = Color.Transparent;
            if (port > 0)
                lbPort.BackColor = Color.LightSkyBlue;
            else
                lbPort.BackColor = Color.Transparent;
            if (adapter > 0)
                lbAdapter.BackColor = Color.LightSkyBlue;
            else
                lbAdapter.BackColor = Color.Transparent;

            if (type > 0)
                lbType.BackColor = Color.LightSkyBlue;
            else
                lbType.BackColor = Color.Transparent;

            if(!string.IsNullOrEmpty(_size))
                lbSize.BackColor = Color.LightSkyBlue;
            else
                lbSize.BackColor = Color.Transparent;

            if (powerType > 0)
                lbPowerType.BackColor = Color.LightSkyBlue;
            else
                lbPowerType.BackColor = Color.Transparent;

            if (brand > 0)
                lbBrand.BackColor = Color.LightSkyBlue;
            else
                lbBrand.BackColor = Color.Transparent;


        }

        private void SaveCheckInfo()
        {

            int caseDestroyed = 0;
            int caseScratch = 0;
            int caseStabbed = 0;
            int casePressed = 0;
            int caseDiscolored = 0;
            short display = 0;
            short adapter = 0;
            int type = 0;
            int menu = 0;
            int powerType = 0;
            int brand = 0;

            _size = teSize.Text;

            _description = teDescription.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);

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
            if (ceDisplay10.CheckState == CheckState.Checked)
                display += 512;

            int port = 0;

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

            type = ConvertUtil.ToInt32(rgDisplayType.EditValue);
            powerType = ConvertUtil.ToInt32(rgPowerType.EditValue);
            brand = ConvertUtil.ToInt32(rgBrand.EditValue);

            if (ceMenu.CheckState == CheckState.Checked)
                menu = 1;

            _DicMonitorCheck["CASE_DESTROYED"] = caseDestroyed;
            _DicMonitorCheck["CASE_SCRATCH"] = caseScratch;
            _DicMonitorCheck["CASE_STABBED"] = caseStabbed;
            _DicMonitorCheck["CASE_PRESSED"] = casePressed;
            _DicMonitorCheck["CASE_DISCOLORED"] = caseDiscolored;
            _DicMonitorCheck["DISPLAY"] = display;
            _DicMonitorCheck["PORT"] = port;
            _DicMonitorCheck["ADAPTER"] = adapter;
            _DicMonitorCheck["TYPE"] = type;
            _DicMonitorCheck["MENU"] = menu;
            _DicMonitorCheck["POWER_TYPE"] = powerType;
            _DicMonitorCheck["BRAND"] = brand;


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