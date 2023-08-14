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
    public partial class DlgInventoryCheck : DevExpress.XtraEditors.XtraForm
    {
        private string _mbdBarcode;
        Dictionary<string, short> _DicMonitorCheck;



        public DlgInventoryCheck(string mbdBarcode, Dictionary<string, short> dicMonitorCheck)
        {
            InitializeComponent();

            _mbdBarcode = mbdBarcode;
            _DicMonitorCheck = dicMonitorCheck;
        }

        private void DlgMonitorCheck_Load(object sender, EventArgs e)
        {

            if (_DicMonitorCheck == null)
            {
                MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                if (_DicMonitorCheck.Count > 1)
                {
                    short caseDestroyed = _DicMonitorCheck["CASE_DESTROYED"];
                    short caseScratch = _DicMonitorCheck["CASE_SCRATCH"];
                    short caseStabbed = _DicMonitorCheck["CASE_STABBED"];
                    short casePressed = _DicMonitorCheck["CASE_PRESSED"];
                    short caseDiscolored = _DicMonitorCheck["CASE_DISCOLORED"];
                    short display = _DicMonitorCheck["DISPLAY"];
                    short port = _DicMonitorCheck["PORT"];
                    short adapter = _DicMonitorCheck["ADAPTER"];


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

                    if ((port & 1) == 1)
                        cePort1.CheckState = CheckState.Checked;
                    if ((port & 2) == 2)
                        cePort2.CheckState = CheckState.Checked;
                    if ((port & 4) == 4)
                        cePort3.CheckState = CheckState.Checked;
                    if ((port & 8) == 8)
                        cePort4.CheckState = CheckState.Checked;

                    rgAdapter.EditValue = adapter;
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

                }
            }
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("검수완료하시겠습니까?", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

            short display = ConvertUtil.toInt16(rgDisplay.EditValue);

            short port = 0;

            if (cePort1.CheckState == CheckState.Checked)
                port += 1;
            if (cePort2.CheckState == CheckState.Checked)
                port += 2;
            if (cePort3.CheckState == CheckState.Checked)
                port += 4;
            if (cePort4.CheckState == CheckState.Checked)
                port += 8;

            short adapter = ConvertUtil.toInt16(rgAdapter.EditValue);

            _DicMonitorCheck["CASE_DESTROYED"] = caseDestroyed;
            _DicMonitorCheck["CASE_SCRATCH"] = caseScratch;
            _DicMonitorCheck["CASE_STABBED"] = caseStabbed;
            _DicMonitorCheck["CASE_PRESSED"] = casePressed;
            _DicMonitorCheck["CASE_DISCOLORED"] = caseDiscolored;
            _DicMonitorCheck["DISPLAY"] = display;
            _DicMonitorCheck["PORT"] = port;
            _DicMonitorCheck["ADAPTER"] = adapter;

        }


    }



    
}