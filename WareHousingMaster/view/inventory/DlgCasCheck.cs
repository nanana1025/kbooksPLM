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
    public partial class DlgCasCheck : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, int> _DicCasitorCheck;

        public bool _isPrint { get; set; }
        public string _description { get; private set; }
        public string _pGrade { get; private set; }

        DataTable _dtPort;
        DataTable _dtPGrade;

        public DlgCasCheck(Dictionary<string, int> dicCasitorCheck, string description, string pGrade, DataTable dtPort, DataTable dtPGrade)
        {
            InitializeComponent();

            _description = description;
            _pGrade = pGrade;
            _dtPort = dtPort;
            _dtPGrade = dtPGrade;
            _isPrint = false;
        }

        private void DlgMonitorCheck_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            if (_DicCasitorCheck == null)
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

                if (_DicCasitorCheck.Count > 0)
                {
                    int caseDestroyed = _DicCasitorCheck["CASE_DESTROYED"];
                    int caseScratch = _DicCasitorCheck["CASE_SCRATCH"];
                    int caseStabbed = _DicCasitorCheck["CASE_STABBED"];
                    int casePressed = _DicCasitorCheck["CASE_PRESSED"];
                    int caseDiscolored = _DicCasitorCheck["CASE_DISCOLORED"];
                    string description = _description;
                    teDescription.Text = description;

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

                    if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0)
                        lbCase.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    _DicCasitorCheck.Add("CASE_DESTROYED", 0);
                    _DicCasitorCheck.Add("CASE_SCRATCH", 0);
                    _DicCasitorCheck.Add("CASE_STABBED", 0);
                    _DicCasitorCheck.Add("CASE_PRESSED", 0);
                    _DicCasitorCheck.Add("CASE_DISCOLORED", 0);

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

            if (caseDestroyed > 0 || caseScratch > 0 || caseStabbed > 0 || casePressed > 0 || caseDiscolored > 0)
                lbCase.BackColor = Color.LightSkyBlue;
            else
                lbCase.BackColor = Color.Transparent;

        }

        private void SaveCheckInfo()
        {

            int caseDestroyed = 0;
            int caseScratch = 0;
            int caseStabbed = 0;
            int casePressed = 0;
            int caseDiscolored = 0;

            _description = teDescription.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);

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
            if(ceCaseScratch4.CheckState == CheckState.Checked)
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

            _DicCasitorCheck["CASE_DESTROYED"] = caseDestroyed;
            _DicCasitorCheck["CASE_SCRATCH"] = caseScratch;
            _DicCasitorCheck["CASE_STABBED"] = caseStabbed;
            _DicCasitorCheck["CASE_PRESSED"] = casePressed;
            _DicCasitorCheck["CASE_DISCOLORED"] = caseDiscolored;

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