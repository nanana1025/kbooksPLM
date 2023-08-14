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
using WareHousingMaster.view.consigned;

namespace WareHousingMaster.view.inventory
{
    public partial class DlgConsignedReturnCheck : DevExpress.XtraEditors.XtraForm
    {
        public Dictionary<string, int> _dicReturnCheck { get; private set; }

        Dictionary<CheckedListBoxControl, string> _dicCheckListComboBox;

        public string _etcDes { get; private set; }
        public string _pGrade { get; private set; }
        public string _batteryRemain { get; private set; }
        public string _serialNo { get; private set; }
        public string _repairContent { get; private set; }

        public string _printPort { get; private set; }

        
        public bool _isPrint { get; set; }

        public long _palletNo { get; set; }

        long _proxyId;
        public Dictionary<long, long> _dicErrorPart { get; private set; }
        public bool _isErrorPartCheck { get; set; }

        public DlgConsignedReturnCheck(long proxyId, Dictionary<string, int> dicReturnCheck, Dictionary<long, long> dicErrorPart)
        {
            InitializeComponent();

            _proxyId = proxyId;
            _dicReturnCheck = dicReturnCheck;
            _dicErrorPart = dicErrorPart;

            _isErrorPartCheck = false;

            _dicCheckListComboBox = new Dictionary<CheckedListBoxControl, string>()
            {
                {clJustChange, "CUSTOMER"}, {clFunctionError, "FUNCTION"}, 
                {clProductError, "PRODUCT"}, {clPartError, "PART"}, {clUserError, "USER"}, 
                {clDestroyed, "DESTROYED"}, {clEtc1, "ETC1"}, {clEtc2, "ETC2"}
            };

           
        }

        private void DlgNtbCheck_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            setData();
            initData();
        }

        private void setData()
        {
            clJustChange.Items.Clear();
            DataTable dtJustChange = Util.getCodeList("CD090801", "KEY", "VALUE");
            CheckedListBoxItem[] clbJustChange = new CheckedListBoxItem[dtJustChange.Rows.Count];

            for(int i = 0; i < dtJustChange.Rows.Count; i++)
                clbJustChange[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtJustChange.Rows[i]["KEY"]), ConvertUtil.ToString(dtJustChange.Rows[i]["VALUE"]));

            clJustChange.Items.AddRange(clbJustChange);


            clFunctionError.Items.Clear();
            DataTable dtFunctionError = Util.getCodeList("CD090802", "KEY", "VALUE");
            CheckedListBoxItem[] clbFunctionError = new CheckedListBoxItem[dtFunctionError.Rows.Count];

            for (int i = 0; i < dtFunctionError.Rows.Count; i++)
                clbFunctionError[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtFunctionError.Rows[i]["KEY"]), ConvertUtil.ToString(dtFunctionError.Rows[i]["VALUE"]));

            clFunctionError.Items.AddRange(clbFunctionError);


            clProductError.Items.Clear();
            DataTable dtProductError = Util.getCodeList("CD090803", "KEY", "VALUE");
            CheckedListBoxItem[] clbProductError = new CheckedListBoxItem[dtProductError.Rows.Count];

            for (int i = 0; i < dtProductError.Rows.Count; i++)
                clbProductError[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtProductError.Rows[i]["KEY"]), ConvertUtil.ToString(dtProductError.Rows[i]["VALUE"]));

            clProductError.Items.AddRange(clbProductError);


            clPartError.Items.Clear();
            DataTable dtPartError = Util.getCodeList("CD090804", "KEY", "VALUE");
            CheckedListBoxItem[] clbPartError = new CheckedListBoxItem[dtPartError.Rows.Count];

            for (int i = 0; i < dtPartError.Rows.Count; i++)
                clbPartError[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtPartError.Rows[i]["KEY"]), ConvertUtil.ToString(dtPartError.Rows[i]["VALUE"]));

            clPartError.Items.AddRange(clbPartError);


            clUserError.Items.Clear();
            DataTable dtUserError = Util.getCodeList("CD090805", "KEY", "VALUE");
            CheckedListBoxItem[] clbUserError = new CheckedListBoxItem[dtUserError.Rows.Count];

            for (int i = 0; i < dtUserError.Rows.Count; i++)
                clbUserError[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtUserError.Rows[i]["KEY"]), ConvertUtil.ToString(dtUserError.Rows[i]["VALUE"]));

            clUserError.Items.AddRange(clbUserError);

            clDestroyed.Items.Clear();
            DataTable dtDestroyed = Util.getCodeList("CD090806", "KEY", "VALUE");
            CheckedListBoxItem[] clbDestroyed = new CheckedListBoxItem[dtDestroyed.Rows.Count];

            for (int i = 0; i < dtDestroyed.Rows.Count; i++)
                clbDestroyed[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtDestroyed.Rows[i]["KEY"]), ConvertUtil.ToString(dtDestroyed.Rows[i]["VALUE"]));

            clDestroyed.Items.AddRange(clbDestroyed);

            clEtc1.Items.Clear();
            DataTable dtEtc1 = Util.getCodeList("CD090807", "KEY", "VALUE");
            CheckedListBoxItem[] clbEtc1 = new CheckedListBoxItem[dtEtc1.Rows.Count];

            for (int i = 0; i < dtEtc1.Rows.Count; i++)
                clbEtc1[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtEtc1.Rows[i]["KEY"]), ConvertUtil.ToString(dtEtc1.Rows[i]["VALUE"]));

            clEtc1.Items.AddRange(clbEtc1);

            clEtc2.Items.Clear();
            DataTable dtEtc2 = Util.getCodeList("CD090808", "KEY", "VALUE");
            CheckedListBoxItem[] clbEtc2 = new CheckedListBoxItem[dtEtc2.Rows.Count];

            for (int i = 0; i < dtEtc2.Rows.Count; i++)
                clbEtc2[i] = new CheckedListBoxItem(ConvertUtil.ToInt32(dtEtc2.Rows[i]["KEY"]), ConvertUtil.ToString(dtEtc2.Rows[i]["VALUE"]));

            clEtc2.Items.AddRange(clbEtc2);

        }
        private void initData()
        {
            foreach(KeyValuePair<CheckedListBoxControl, string> pair in _dicCheckListComboBox)
            {
                if (_dicReturnCheck.ContainsKey(pair.Value))
                {
                    int value = _dicReturnCheck[pair.Value];
                    CheckedListBoxItemCollection checkItem = pair.Key.Items;

                    for (int j = 0; j < checkItem.Count; j++)
                    {
                        if ((value & ExamineInfo._BASE[j]) == ExamineInfo._BASE[j])
                        {
                            CheckedListBoxItem item = checkItem[j];
                            item.CheckState = CheckState.Checked;
                        }
                    }
                }
            }
        }

        private void getCheckInfoStatus()
        {
            int value = 0;
            foreach (KeyValuePair<CheckedListBoxControl, string> pair in _dicCheckListComboBox)
            {
                value = 0;
                CheckedListBoxItemCollection checkItem = pair.Key.Items;

                for (int j = 0; j < checkItem.Count; j++)
                {
                    CheckedListBoxItem item = checkItem[j];
                    if (item.CheckState == CheckState.Checked)
                        value += ConvertUtil.ToInt32(item.Value);
                }

                if (_dicReturnCheck.ContainsKey(pair.Value))
                    _dicReturnCheck[pair.Value] = value;
                else
                    _dicReturnCheck.Add(pair.Value, value);
            }
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("반품체크를 완료하시겠습니까?", "반품체크 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                getCheckInfoStatus();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                //MessageBox.Show("아니요 클릭");
            }
        }

        private void sbPrint_Click(object sender, EventArgs e)
        {
            //save(true);
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void sbErrorPartCheck_Click(object sender, EventArgs e)
        {
            using (DlgConsignedReceiptPart dlgConsignedReceiptPart = new DlgConsignedReceiptPart(_proxyId, _dicErrorPart))
            {
                if (dlgConsignedReceiptPart.ShowDialog(this) == DialogResult.OK)
                {
                    _dicErrorPart = dlgConsignedReceiptPart._dicErrorPart;
                    _isErrorPartCheck = true;
                }
            }
        }
    }

    
}