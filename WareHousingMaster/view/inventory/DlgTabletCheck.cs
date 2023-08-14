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
using static DevExpress.XtraEditors.BaseCheckedListBoxControl;
using DevExpress.XtraEditors.Controls;

namespace WareHousingMaster.view.inventory
{
    public partial class DlgTabletCheck : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, int> _DicTabletCheck;

        Dictionary<string, int> _DicTabletCheckTemp;

        Dictionary<string, int> _dicHistoryTabletCheck;

        DataTable _dtTabletAdjustmentPriceHistory;

        DataTable _dtTabletAdjustmentPrice;
        public string _etcDes { get; private set; }
        public string _pGrade { get; private set; }
        public string _batteryRemain { get; private set; }
        public string _serialNo { get; private set; }
        public string _repairContent { get; private set; }

        public string _printPort { get; private set; }

        
        public bool _isPrint { get; set; }

        public long _palletNo { get; set; }

        DataTable _dtPort;

        DataTable _dtPGrade;

        BindingSource _bsPallet;

        short _checkType;

        List<LayoutControlItem> listLayoutRepair;
        List<SpinEdit> listSpinEditExam;
        List<SpinEdit> listSpinEditRepair;
        List<CheckedListBoxControl> listCheckedBoxListControl;
        List<CheckedListBoxControl> listCaseCheckedBoxListControl;
        List<Label> listLabel;



        public DlgTabletCheck(ref Dictionary<string, int> dicTabletCheck, Dictionary<string, int> dicHistoryNtbCheck, ref DataTable dtTabletAdjustmentPrice, string etcDes, string pGrade, string batteryRemain, string repairContent, string serialNo, DataTable dtPort, DataTable dtPGrade, BindingSource bsPallet, long palletNo, short checkType)
        {
            InitializeComponent();

            _DicTabletCheck = dicTabletCheck;
            _dicHistoryTabletCheck = dicHistoryNtbCheck;
            _dtTabletAdjustmentPrice = dtTabletAdjustmentPrice;
            _etcDes = etcDes;
            _pGrade = pGrade;
            _batteryRemain = batteryRemain;
            _repairContent = repairContent;
            _dtPort = dtPort;
            _dtPGrade = dtPGrade;
            _isPrint = false;
            _bsPallet = bsPallet;
            _palletNo = palletNo;
            _checkType = checkType;
            _serialNo = serialNo;
            

            _DicTabletCheckTemp = new Dictionary<string, int>();

            listLayoutRepair = new List<LayoutControlItem>(new[] { lcRepair01, lcRepair02,lcRepair03, lcRepair04, lcRepair05, lcRepair06,
            lcRepair07, lcRepair08, lcRepair09, lcRepair010, lcRepair011, lcRepair012, lcRepair013, lcRepair014, lcRepair015, lcRepair016, lcRepair017, lcRepairEtc});

            listSpinEditExam = new List<SpinEdit>(new[] { seExam01, seExam02,seExam03, seExam04, seExam05, seExam06,
            seExam07, seExam08, seExam09, seExam010, seExam011, seExam012, seExam013, seExam014, seExam015, seExam016,seExam017, seExamEtc});

            listSpinEditRepair = new List<SpinEdit>(new[] { seRepair01, seRepair02,seRepair03, seRepair04, seRepair05, seRepair06,
            seRepair07, seRepair08, seRepair09, seRepair010, seRepair011, seRepair012, seRepair013, seRepair014, seRepair015, seRepair016,seRepair017, seRepairEtc});

            listCheckedBoxListControl = new List<CheckedListBoxControl>(new[] { ceCheck, ceCaseDestroyed, ceDispaly, ceBattery, ceAdapter, ceButton,
            ceUsbPort, ceUsbCable, cePen, ceSdCard, ceSoftware, ceCam, ceSound, ceEarPhone, ceMike, ceWirelessLan, ceSelfCheck});

            listCaseCheckedBoxListControl = new List<CheckedListBoxControl>(new[] { ceCaseDestroyed, ceCaseScratch, ceCaseStabbed, ceCaseDiscolored });

            listLabel = new List<Label>(new[] { lbCheck, lbCase, lbDisplay, lbBattery, lbAdapter, lbButton,
            lbUsbPort, lbUsbCable, lbPen, lbSDCard, lbSoftWare, lbCam, lbSound, lbEarPhone, lbMike, lbLanWireless, lbSelfCheck});

            string col;
            for (int i = 0; i < ExamineInfo._listTabletCol.Count; i++)
            {
                if (i == 1)
                {
                    for (int k = 0; k < ExamineInfo._listTabletCaseCheckCol.Count; k++)
                    {
                        col = ExamineInfo._listTabletCaseCheckCol[k];
                        _DicTabletCheckTemp.Add(col, 0);
                    }
                }
                else
                {
                    col = ExamineInfo._listTabletCol[i];
                    _DicTabletCheckTemp.Add(col, 0);
                }
            }

            meSelfTest.Text = $@"기능 테스트: 계산기 앱 → (+30012012732+ → *#0*# or *#7353#
배터리 성능: 계산기 앱 → (+30012012732+ → *#0228#";

            if(string.IsNullOrEmpty(_pGrade))
            {
                _pGrade = "0";
            }
        }

        private void DlgNtbCheck_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            if (_DicTabletCheck == null)
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


            lgcNtbCheck.BeginUpdate();
            if (_checkType == 1)
            {
                if (lcRepair.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    lcRepair.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    foreach (LayoutControlItem lcControl in listLayoutRepair)
                        lcControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                lcRepairContent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                seExamEtc.ReadOnly = false;
                //empty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                if (lcRepair.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                {
                    lcRepair.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    foreach (LayoutControlItem lcControl in listLayoutRepair)
                        lcControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }

                lcRepairContent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                seExamEtc.ReadOnly = true;
                //empty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                setExamTextColor();
            }
            lgcNtbCheck.EndUpdate();

            setInit();

            Util.LookupEditHelper(lePort, _dtPort, "KEY", "VALUE");
            lePort.EditValue = ProjectInfo._printerPort;

            Util.LookupEditHelper(lePGrade, _dtPGrade, "KEY", "VALUE");
            lePGrade.EditValue = _pGrade;

            //Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");
            //lePallet.EditValue = $"{ _palletNo }";

            teBatteryRemain.Text = _batteryRemain;
            teEtc.Text = _etcDes;
            teSerialNo.Text = _serialNo;
            teRepairContent.Text = _repairContent;

            string col = "";
            if (_DicTabletCheck.Count > 0)
            {
                int value = 0;

                for (int i = 0; i < ExamineInfo._listTabletCol.Count; i++)
                {
                    if (i == 1)
                    {
                        bool isCheck = false;
                        for (int k = 0; k < ExamineInfo._listTabletCaseCheckCol.Count; k++)
                        {
                            col = ExamineInfo._listTabletCaseCheckCol[k];

                            CheckedListBoxControl checkControl = listCaseCheckedBoxListControl[k];
                            value = ConvertUtil.ToInt32(_DicTabletCheck[col]);

                            CheckedListBoxItemCollection checkItem = checkControl.Items;

                            for (int j = 0; j < checkItem.Count; j++)
                            {
                                if ((value & ExamineInfo._BASE[j]) == ExamineInfo._BASE[j])
                                {
                                    CheckedListBoxItem item = checkItem[j];
                                    item.CheckState = CheckState.Checked;
                                    isCheck = true;
                                }
                            }
                        }

                        if (isCheck)
                            listLabel[i].BackColor = Color.LightSkyBlue;
                    }
                    else
                    {
                        col = ExamineInfo._listTabletCol[i];

                        CheckedListBoxControl checkControl = listCheckedBoxListControl[i];
                        value = ConvertUtil.ToInt32(_DicTabletCheck[col]);

                        CheckedListBoxItemCollection checkItem = checkControl.Items;

                        for (int j = 0; j < checkItem.Count; j++)
                        {
                            if ((value & ExamineInfo._BASE[j]) == ExamineInfo._BASE[j])
                            {
                                CheckedListBoxItem item = checkItem[j];
                                item.CheckState = CheckState.Checked;
                            }

                        }

                        if (value > 0)
                            listLabel[i].BackColor = Color.LightSkyBlue;
                    }
                }
            }
            else
            {
                for (int i = 0; i < ExamineInfo._listTabletCol.Count; i++)
                {
                    if (i == 1)
                    {
                        for (int k = 0; k < ExamineInfo._listTabletCaseCheckCol.Count; k++)
                        {
                            col = ExamineInfo._listTabletCaseCheckCol[k];
                            _DicTabletCheck.Add(col, 0);
                        }
                    }
                    else
                    {
                        col = ExamineInfo._listTabletCol[i];
                        _DicTabletCheck.Add(col, 0);
                    }
                }
            }
        }

        private void setExamTextColor()
        {
            string col = "";
            if (_dicHistoryTabletCheck.Count > 0)
            {
                int value = 0;

                for (int i = 0; i < ExamineInfo._listTabletCol.Count; i++)
                {
                    if (i == 1)
                    {
                        for (int k = 0; k < ExamineInfo._listTabletCaseCheckCol.Count; k++)
                        {
                            col = ExamineInfo._listTabletCaseCheckCol[k];

                            CheckedListBoxControl checkControl = listCaseCheckedBoxListControl[k];
                            value = ConvertUtil.ToInt32(_dicHistoryTabletCheck[col]);

                            CheckedListBoxItemCollection checkItem = checkControl.Items;

                            for (int j = 0; j < checkItem.Count; j++)
                            {
                                if ((value & ExamineInfo._BASE[j]) == ExamineInfo._BASE[j])
                                {
                                    CheckedListBoxItem item = checkItem[j];
                                    item.Description = item.Description + "(v)";
                                }
                            }
                        }
                    }
                    else
                    {
                        col = ExamineInfo._listTabletCol[i];

                        CheckedListBoxControl checkControl = listCheckedBoxListControl[i];
                        value = ConvertUtil.ToInt32(_dicHistoryTabletCheck[col]);

                        CheckedListBoxItemCollection checkItem = checkControl.Items;

                        for (int j = 0; j < checkItem.Count; j++)
                        {
                            if ((value & ExamineInfo._BASE[j]) == ExamineInfo._BASE[j])
                            {
                                CheckedListBoxItem item = checkItem[j];
                                item.Description = item.Description + "(v)";
                            }
                        }
                    }
                }
            }
            
        }

        private void setInit()
        {
            for (int i = 0; i < ExamineInfo._listAdjustmentTabletPriceColShort.Count; i++)
            {
                SpinEdit seExam = listSpinEditExam[i];
                SpinEdit seRepair = listSpinEditRepair[i];
                string col = ExamineInfo._listAdjustmentTabletPriceColShort[i];

                if (_checkType == 1)
                    seExam.EditValue = _dtTabletAdjustmentPrice.Rows[_checkType][col];
                else
                {
                    seExam.EditValue = _dtTabletAdjustmentPrice.Rows[1][col];
                    seRepair.EditValue = _dtTabletAdjustmentPrice.Rows[_checkType][col];
                }
            }
        }

        private void getCheckInfoStatus(bool isCheckAdjustment = false)
        {
            string col = "";
            short value = 0;
            bool isCheck = false;

            for (int i = 0; i < ExamineInfo._listTabletCol.Count; i++)
            {
                isCheck = false;

                col = ExamineInfo._listTabletCol[i];

                if (i == 1)
                {
                    for (int k = 0; k < ExamineInfo._listTabletCaseCheckCol.Count; k++)
                    {
                        string caseCol = ExamineInfo._listTabletCaseCheckCol[k];

                        value = 0;
                        CheckedListBoxControl checkControl = listCaseCheckedBoxListControl[k];
                        CheckedItemCollection items = checkControl.CheckedItems;

                        foreach (CheckedListBoxItem item in items)
                            value += ConvertUtil.ToInt16(item.Value);

                        _DicTabletCheckTemp[caseCol] = value;

                        if (items.Count > 0)
                            isCheck = true;
                    }
                }
                else
                {
                    value = 0;
                    CheckedListBoxControl checkControl = listCheckedBoxListControl[i];
                    CheckedItemCollection items = checkControl.CheckedItems;

                    foreach (CheckedListBoxItem item in items)
                        value += ConvertUtil.ToInt16(item.Value);

                    _DicTabletCheckTemp[col] = value;

                    if (items.Count > 0)
                        isCheck = true;
                }


                if (isCheck)
                {
                    listLabel[i].BackColor = Color.LightSkyBlue;

                    if (isCheckAdjustment)
                    {
                        long adjustPrice = 0;

                        //if (ProjectInfo._dicTabletAdjustmentPrice.ContainsKey(col))
                        //    adjustPrice = ProjectInfo._dicTabletAdjustmentPrice[col];

                        SpinEdit exam = listSpinEditExam[i];
                        SpinEdit repair = listSpinEditRepair[i];


                        if (_checkType == 1)
                            exam.EditValue = adjustPrice;
                        else
                            repair.EditValue = adjustPrice;
                    }
                }
                else
                {
                    listLabel[i].BackColor = Color.Transparent;

                    if (isCheckAdjustment)
                    {
                        SpinEdit exam = listSpinEditExam[i];
                        SpinEdit repair = listSpinEditRepair[i];

                        if (_checkType == 1)
                            exam.EditValue = 0;
                        else
                            repair.EditValue = 0;
                    }
                }
            }
        }

        private void SaveCheckInfo()
        {
            string col = "";

            for (int i = 0; i < ExamineInfo._listTabletCol.Count; i++)
            {
                col = ExamineInfo._listTabletCol[i];

                if (i == 1)
                {
                    for (int k = 0; k < ExamineInfo._listTabletCaseCheckCol.Count; k++)
                    {
                        string caseCol = ExamineInfo._listTabletCaseCheckCol[k];

                        _DicTabletCheck[caseCol] = _DicTabletCheckTemp[caseCol];
                    }
                }
                else
                {
                    _DicTabletCheck[col] = _DicTabletCheckTemp[col];
                }
            }

            for (int i = 0; i < ExamineInfo._listAdjustmentTabletPriceColShort.Count; i++)
            {
                SpinEdit seExam = listSpinEditExam[i];
                SpinEdit seRepair = listSpinEditRepair[i];
                col = ExamineInfo._listAdjustmentTabletPriceColShort[i];

                if (_checkType == 1)
                    _dtTabletAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(seExam.EditValue);
                else
                    _dtTabletAdjustmentPrice.Rows[_checkType][col] = ConvertUtil.ToInt64(seRepair.EditValue);
            }

            _etcDes = teEtc.Text;
            _batteryRemain = teBatteryRemain.Text;
            _pGrade = ConvertUtil.ToString(lePGrade.EditValue);
            _serialNo = teSerialNo.Text;
            _repairContent = teRepairContent.Text;
        }

        private void save(bool isPrint = false)
        {
            if(string.IsNullOrWhiteSpace(teSerialNo.Text))
            {
                Dangol.Warining("시리얼 번호를 입력하세요.");
                return;
            }

            if (_checkType == 1)
                getCheckInfoStatus(true);
            else
                getCheckInfoStatus();

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

            if (isPrint)
            {
                _printPort = ConvertUtil.ToString(lePort.EditValue);

                if (_printPort.Equals("-1"))
                {
                    Dangol.Message("포트를 선택하세요");
                    return;
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
            else
            {
                if (MessageBox.Show("검수완료하시겠습니까(차감가 체크)?", "검수 저장 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveCheckInfo();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    //MessageBox.Show("아니요 클릭");
                }
            }
        }

        private void lgcNtbCheck_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            getCheckInfoStatus(true);
        }


        private void sbOk_Click(object sender, EventArgs e)
        {
            save();
        }

        private void sbPrint_Click(object sender, EventArgs e)
        {
            save(true);
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }

    
}