﻿using System;
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
    public partial class DlgMbdCheck : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, int> _dicInventory;
        public bool _isPrint { get; set; }
        public string _description { get; private set; }
        public string _pGrade { get; private set; }

        DataTable _dtPort;
        DataTable _dtPGrade;
        public DlgMbdCheck(Dictionary<string, int> dicInventory, string description, string pGrade, DataTable dtPort, DataTable dtPGrade)
        {
            InitializeComponent();

            _dicInventory = dicInventory;
            _description = description;
            _pGrade = pGrade;
            _dtPort = dtPort;
            _dtPGrade = dtPGrade;
            _isPrint = false;
        }

        private void DlgMonitorCheck_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            if (_dicInventory == null)
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

                if (_dicInventory.Count > 0)
                {
                    int fault = _dicInventory["FAULT"];

                    string description = _description;
                    teDescription.Text = description;

                    if ((fault & 1) == 1)
                        ceFault1.CheckState = CheckState.Checked;
                    if ((fault & 2) == 2)
                        ceFault2.CheckState = CheckState.Checked;
                    if ((fault & 4) == 4)
                        ceFault3.CheckState = CheckState.Checked;
                    if ((fault & 8) == 8)
                        ceFault4.CheckState = CheckState.Checked;
                    if ((fault & 16) == 16)
                        ceFault5.CheckState = CheckState.Checked;
                    if ((fault & 32) == 32)
                        ceFault6.CheckState = CheckState.Checked;
                    if ((fault & 64) == 64)
                        ceFault7.CheckState = CheckState.Checked;
                    if ((fault & 128) == 128)
                        ceFault8.CheckState = CheckState.Checked;
                    if ((fault & 256) == 256)
                        ceFault9.CheckState = CheckState.Checked;
                    if ((fault & 512) == 512)
                        ceFault10.CheckState = CheckState.Checked;
                    if ((fault & 1024) == 1024)
                        ceFault11.CheckState = CheckState.Checked;
                    if ((fault & 2048) == 2048)
                        ceFault12.CheckState = CheckState.Checked;
                    if ((fault & 4096) == 4096)
                        ceFault13.CheckState = CheckState.Checked;
                    if ((fault & 8192) == 8192)
                        ceFault14.CheckState = CheckState.Checked;
                    if ((fault & 16384) == 16384)
                        ceFault15.CheckState = CheckState.Checked;
                    if ((fault & 32768) == 32768)
                        ceFault16.CheckState = CheckState.Checked;

                    if (fault > 0)
                        lbFault.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    _dicInventory.Add("FAULT", 0);

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

            int fault = 0;

            _description = teDescription.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);

            if (ceFault1.CheckState == CheckState.Checked)
                fault += 1;
            if (ceFault2.CheckState == CheckState.Checked)
                fault += 2;
            if (ceFault3.CheckState == CheckState.Checked)
                fault += 4;
            if (ceFault4.CheckState == CheckState.Checked)
                fault += 8;
            if (ceFault5.CheckState == CheckState.Checked)
                fault += 16;
            if (ceFault6.CheckState == CheckState.Checked)
                fault += 32;
            if (ceFault7.CheckState == CheckState.Checked)
                fault += 64;
            if (ceFault8.CheckState == CheckState.Checked)
                fault += 128;
            if (ceFault9.CheckState == CheckState.Checked)
                fault += 256;
            if (ceFault10.CheckState == CheckState.Checked)
                fault += 512;
            if (ceFault11.CheckState == CheckState.Checked)
                fault += 1024;
            if (ceFault12.CheckState == CheckState.Checked)
                fault += 2048;
            if (ceFault13.CheckState == CheckState.Checked)
                fault += 4096;
            if (ceFault14.CheckState == CheckState.Checked)
                fault += 8192;
            if (ceFault15.CheckState == CheckState.Checked)
                fault += 16384;
            if (ceFault16.CheckState == CheckState.Checked)
                fault += 32768;


            if (fault > 0)
                lbFault.BackColor = Color.LightSkyBlue;
            else
                lbFault.BackColor = Color.Transparent;
        }

        private void SaveCheckInfo()
        {

            int fault = 0;

            _description = teDescription.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);

            if (ceFault1.CheckState == CheckState.Checked)
                fault += 1;
            if (ceFault2.CheckState == CheckState.Checked)
                fault += 2;
            if (ceFault3.CheckState == CheckState.Checked)
                fault += 4;
            if (ceFault4.CheckState == CheckState.Checked)
                fault += 8;
            if (ceFault5.CheckState == CheckState.Checked)
                fault += 16;
            if (ceFault6.CheckState == CheckState.Checked)
                fault += 32;
            if (ceFault7.CheckState == CheckState.Checked)
                fault += 64;
            if (ceFault8.CheckState == CheckState.Checked)
                fault += 128;
            if (ceFault9.CheckState == CheckState.Checked)
                fault += 256;
            if (ceFault10.CheckState == CheckState.Checked)
                fault += 512;
            if (ceFault11.CheckState == CheckState.Checked)
                fault += 1024;
            if (ceFault12.CheckState == CheckState.Checked)
                fault += 2048;
            if (ceFault13.CheckState == CheckState.Checked)
                fault += 4096;
            if (ceFault14.CheckState == CheckState.Checked)
                fault += 8192;
            if (ceFault15.CheckState == CheckState.Checked)
                fault += 16384;
            if (ceFault16.CheckState == CheckState.Checked)
                fault += 32768;


            _dicInventory["FAULT"] = fault;
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