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
using DevExpress.XtraSplashScreen;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.inventory;
using Newtonsoft.Json;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ButtonPanel;
using WareHousingMaster.view.usedPurchase.receiptComponent;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrUsedPurchaseReceiptDetail_210330 : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtReceipt;
        DataTable _dtStateList;
        DataTable _dtFaultList;

        DataTable _dtCounselling;
        DataTable _dtResponse;

        BindingSource _bsReceipt;
        BindingSource _bsStateList;

        BindingSource _bsCounselling;
        BindingSource _bsResponse;
        BindingSource _bsFaultList;

        DataRowView _currentCounsellingRow;

        string _receipt;
        long _receiptId;
        long _warehousingId;
        bool _isExistCustomerCheck;
        string _receiptState = "-1";
        List<string> _listPartUpdate;
        List<string> _listCustomerUpdate;
        List<string> _listExamUpdate;

        int _visigleLayout = 1;
        int _returnState;


        public usrUsedPurchaseReceiptDetail_210330()
        {
            InitializeComponent();

            _receipt = "";

            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("USER_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("USER_SEQ", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("TEL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("POSTAL_CD", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("USER_TYPE1", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("USER_TYPE3", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("ACCOUNT_OWNER", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("BANK_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ACCOUNT_NO", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PROCESS_DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADJUST_DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RES_DES", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("MS_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("F_COST", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("F_ADJUST_COST", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("F_TOTAL_COST", typeof(long)));


            _dtStateList = new DataTable();
            _dtStateList.Columns.Add(new DataColumn("RECEIPT_STATE", typeof(string)));
            _dtStateList.Columns.Add(new DataColumn("USER_NM", typeof(string)));
            _dtStateList.Columns.Add(new DataColumn("DEPT_NM", typeof(string)));
            _dtStateList.Columns.Add(new DataColumn("REGIST_DT", typeof(string)));

            _dtCounselling = new DataTable();
            _dtCounselling.Columns.Add(new DataColumn("REQUEST_ID", typeof(long)));
            _dtCounselling.Columns.Add(new DataColumn("REQUEST_CAT", typeof(string)));
            _dtCounselling.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtCounselling.Columns.Add(new DataColumn("REQUEST_DT", typeof(string)));
            _dtCounselling.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));

            _dtResponse = new DataTable();
            _dtResponse.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtResponse.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtResponse.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));

            _dtFaultList = new DataTable();
            _dtFaultList.Columns.Add(new DataColumn("FAULT_ID", typeof(long)));
            _dtFaultList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtFaultList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtFaultList.Columns.Add(new DataColumn("CNT", typeof(int)));

            _bsReceipt = new BindingSource();
            _bsStateList = new BindingSource();
            _bsCounselling = new BindingSource();
            _bsResponse = new BindingSource();
            _bsFaultList = new BindingSource();

            _bsStateList.DataSource = _dtStateList;
            _bsCounselling.DataSource = _dtCounselling;
            _bsResponse.DataSource = _dtResponse;
            _bsFaultList.DataSource = _dtFaultList;

            _listPartUpdate = new List<string>(new[] { "1", "2", "3", "6", "7", "B" });
            _listCustomerUpdate = new List<string>(new[] { "1", "2", "3", "6", "B" });
            _listExamUpdate = new List<string>(new[] { "3", "5", "7" });

            _isExistCustomerCheck = false;
            _returnState = 0;
        }

        public void setInitData(object receiptId, object receipt, object receiptState)
        {
            _receiptId = ConvertUtil.ToInt64(receiptId);
            _receipt = ConvertUtil.ToString(receipt);
            _receiptState = ConvertUtil.ToString(receiptState);

            setControl();

            DataTable dtUsedPurchaseState = Util.getCodeList("CD1303", "KEY", "VALUE");
            Util.LookupEditHelper(rileReceiptState, dtUsedPurchaseState, "KEY", "VALUE");
            RadioGroupItem[] rgState = new RadioGroupItem[dtUsedPurchaseState.Rows.Count];

            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(rileUserId, dtUserId, "KEY", "VALUE");

            int index = 0;
            foreach (DataRow dr in dtUsedPurchaseState.Rows)
            {
                RadioGroupItem rgItem = new RadioGroupItem(dr["KEY"], ConvertUtil.ToString(dr["VALUE"]), true, dr["KEY"]);
                rgState[index++] = rgItem;
            }
            this.rgState.Properties.Items.AddRange(rgState);

            /*teReceipt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT", false, DataSourceUpdateMode.Never));
            teReceiptDt.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT_DT", false, DataSourceUpdateMode.Never));
            teCustomerNm.DataBindings.Add(new Binding("Text", _bsReceipt, "CUSTOMER_NM", false, DataSourceUpdateMode.Never));
            teTel.DataBindings.Add(new Binding("Text", _bsReceipt, "TEL", false, DataSourceUpdateMode.Never));
            teHp.DataBindings.Add(new Binding("Text", _bsReceipt, "HP", false, DataSourceUpdateMode.Never));
            tePostalCd.DataBindings.Add(new Binding("Text", _bsReceipt, "POSTAL_CD", false, DataSourceUpdateMode.Never));
            teAddress.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS", false, DataSourceUpdateMode.Never));
            teAddressDetail.DataBindings.Add(new Binding("Text", _bsReceipt, "ADDRESS_DETAIL", false, DataSourceUpdateMode.Never));
            rgUserType1.DataBindings.Add(new Binding("Text", _bsReceipt, "USER_TYPE1", false, DataSourceUpdateMode.Never));
            rgUserType3.DataBindings.Add(new Binding("Text", _bsReceipt, "USER_TYPE3", false, DataSourceUpdateMode.Never));
            teAccountOwner.DataBindings.Add(new Binding("Text", _bsReceipt, "ACCOUNT_OWNER", false, DataSourceUpdateMode.Never));
            teBankNm.DataBindings.Add(new Binding("Text", _bsReceipt, "BANK_NM", false, DataSourceUpdateMode.Never));
            teAccount.DataBindings.Add(new Binding("Text", _bsReceipt, "ACCOUNT_NO", false, DataSourceUpdateMode.Never));
            meDes.DataBindings.Add(new Binding("Text", _bsReceipt, "DES", false, DataSourceUpdateMode.Never));
            meProcessDes.DataBindings.Add(new Binding("Text", _bsReceipt, "PROCESS_DES", false, DataSourceUpdateMode.Never));
            meAdjustDes.DataBindings.Add(new Binding("Text", _bsReceipt, "ADJUST_DES", false, DataSourceUpdateMode.Never));
            rgState.DataBindings.Add(new Binding("Text", _bsReceipt, "RECEIPT_STATE", false, DataSourceUpdateMode.Never));*/

            meCounselling.DataBindings.Add(new Binding("Text", _bsCounselling, "DES", false, DataSourceUpdateMode.Never));
            meResponse.DataBindings.Add(new Binding("Text", _bsResponse, "DES", false, DataSourceUpdateMode.Never));

            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr1 = dtComponentCd.NewRow();
                dr1["KEY"] = ProjectInfo._componetCd[i];
                dr1["VALUE"] = ProjectInfo._componetNm[i];
                dtComponentCd.Rows.Add(dr1);
            }
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            _warehousingId = -1;
        }

        public void reload(object receiptId, object receipt, object receiptState)
        {
            _receiptId = ConvertUtil.ToInt64(receiptId);
            _receipt = ConvertUtil.ToString(receipt);
            _receiptState = ConvertUtil.ToString(receiptState);

            setControl();

            setIInitData(true);

        }


        private void usrWarehouseState_Load(object sender, EventArgs e)
        {
            usrReceiptPartTree1.totalCostChangeEvent += new usrReceiptPartTree.TotalCostChangeHandler(totalCostChange);

            setIInitData();

            gcStateList.DataSource = null;
            gcStateList.DataSource = _bsStateList;

            gcCounseling.DataSource = null;
            gcCounseling.DataSource = _bsCounselling;

            gcResponse.DataSource = null;
            gcResponse.DataSource = _bsResponse;

            gcFault.DataSource = null;
            gcFault.DataSource = _bsFaultList;
        }


        private void setIInitData(bool reload = false)
        {
            usrReceiptPart1.Clear();
            usrReceiptPartTree1.Clear();
            _dtFaultList.Clear();

            if (getReceiptDetail())
            {
                if (_listExamUpdate.Contains(_receiptState))
                {
                    if (reload)
                        usrReceiptPartTree1.resetInfo(_receiptId, _receipt, _receiptState);
                    else
                        usrReceiptPartTree1.setinitialize(_receiptId, _receipt, _receiptState);
                    usrReceiptPartTree1.getComponentAll();

                    if (ConvertUtil.ToInt32(_dtReceipt.Rows[0]["MS_TYPE"]) < 0)
                    {
                        int cost = 0;
                        int adjustCost = 0;
                        int totalCost = 0;
                        usrReceiptPartTree1.getSummaryValue(ref cost, ref adjustCost, ref totalCost);

                        _dtReceipt.Rows[0]["F_COST"] = seFCost.EditValue = cost;
                        _dtReceipt.Rows[0]["F_ADJUST_COST"] = seFAdjustCost.EditValue = adjustCost;
                        _dtReceipt.Rows[0]["F_TOTAL_COST"] = seFTotalCost.EditValue = totalCost;
                    }
                }
                else
                {
                    if (reload)
                        usrReceiptPart1.resetInfo(_receiptId, _receipt, 0);
                    else
                        usrReceiptPart1.setinitialize(_receiptId, _receipt, 0);
                    usrReceiptPart1.getComponentAll();
                }

                getFaultList();
            }
        }

        private bool getReceiptDetail()
        {
            if (getReceiptData())
            {
                _receiptState = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"]);
                teReceipt.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT"]);
                teReceiptDt.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_DT"]);
                teCustomerNm.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_NM"]);
                teCustomerNo.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]);
                teTel.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["TEL"]);
                teHp.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["HP"]);
                tePostalCd.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["POSTAL_CD"]);
                teAddress.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS"]);
                teAddressDetail.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS_DETAIL"]);
                rgUserType1.EditValue = ConvertUtil.ToInt32(_dtReceipt.Rows[0]["USER_TYPE1"]);
                //rgUserType3.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_TYPE3"]);
                teAccountOwner.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_OWNER"]);
                teBankNm.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["BANK_NM"]);
                teAccount.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_NO"]);
                meDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["DES"]);
                //meProcessDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["PROCESS_DES"]);
                meAdjustDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ADJUST_DES"]);
                rgState.EditValue = _receiptState;
                meProcessDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["RES_DES"]);

                rgMsType.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["MS_TYPE"]);
                seFCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_COST"]);
                seFAdjustCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADJUST_COST"]) * -1;
                seFTotalCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_TOTAL_COST"]);

                setControl();

                sbCustomerCheckComplete.Enabled = false;
                if (_receiptState.Equals("7") || _receiptState.Equals("5"))
                {
                    _isExistCustomerCheck = false;
                    getCustomerState();
                    if (_isExistCustomerCheck)
                    {
                        sbCustomerCheckComplete.Enabled = true;
                        getRequest();
                    }
                    else
                    {
                        _returnState = 0;
                        rgCustomerCheckState.EditValue = 0;
                        rgCustomerCheckFault.EditValue = _returnState;
                        _warehousingId = -1;
                        _dtCounselling.Clear();
                    }

                }
                else
                {
                    _returnState = 0;
                    rgCustomerCheckState.EditValue = 0;
                    rgCustomerCheckFault.EditValue = _returnState;
                    _warehousingId = -1;
                    _isExistCustomerCheck = false;

                    _dtCounselling.Clear();


                }


                checkReceiptState();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void checkReceiptState()
        {
            lcReceiptDetail.BeginInit();

            tePostalCd.ReadOnly = true;

            //if (_receiptState.Equals("7"))
            //{
            //    sbComplete.Enabled = true;
            //    rgMsType.ReadOnly = false;
            //    seFCost.ReadOnly = false;
            //    seFAdjustCost.ReadOnly = false;
            //}
            //else
            //{
            //    sbComplete.Enabled = false;
            //    rgMsType.ReadOnly = true;
            //    seFCost.ReadOnly = true;
            //    seFAdjustCost.ReadOnly = true;
            //}

            //if (_listCustomerUpdate.Contains(_receiptState))
            //{
            //    teCustomerNm.ReadOnly = false;
            //    teTel.ReadOnly = false;
            //    teHp.ReadOnly = false;
            //    tePostalCd.ReadOnly = false;
            //    teAddress.ReadOnly = false;
            //    teAddressDetail.ReadOnly = false;
            //    rgUserType1.ReadOnly = false;
            //    teAccountOwner.ReadOnly = false;
            //    teBankNm.ReadOnly = false;
            //    teAccount.ReadOnly = false;
            //    meDes.ReadOnly = false;
            //    meAdjustDes.ReadOnly = false;
            //    meProcessDes.ReadOnly = false;
            //    sbAddress.Enabled = true;
            //}
            //else
            //{
            //    teCustomerNm.ReadOnly = true;
            //    teTel.ReadOnly = true;
            //    teHp.ReadOnly = true;
            //    tePostalCd.ReadOnly = true;
            //    teAddress.ReadOnly = true;
            //    teAddressDetail.ReadOnly = true;
            //    rgUserType1.ReadOnly = true;
            //    teAccountOwner.ReadOnly = true;
            //    teBankNm.ReadOnly = true;
            //    teAccount.ReadOnly = true;
            //    meDes.ReadOnly = true;
            //    meAdjustDes.ReadOnly = true;
            //    meProcessDes.ReadOnly = true;
            //    sbAddress.Enabled = false;
            //}

            lcReceiptDetail.EndInit();

            if (_listPartUpdate.Contains(_receiptState))
            {
                //foreach (IBaseButton btn in lcgPartList.CustomHeaderButtons)
                //    btn.Properties.Enabled = true;

                for (int i = 2; i < lcgPartList.CustomHeaderButtons.Count; i++)
                    lcgPartList.CustomHeaderButtons[i].Properties.Enabled = true;
            }
            else
            {
                //foreach (IBaseButton btn in lcgPartList.CustomHeaderButtons)
                //    btn.Properties.Enabled = false;

                for (int i = 2; i < lcgPartList.CustomHeaderButtons.Count; i++)
                    lcgPartList.CustomHeaderButtons[i].Properties.Enabled = false;
            }

            if (_receiptState.Equals("7"))
            {
                lcCustomerCheckState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                empty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcCustomerCheckButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcCustomerCheckButton1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            }
            else
            {
                lcCustomerCheckState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                empty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCustomerCheckButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCustomerCheckButton1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            if (_receiptState.Equals("7") || _receiptState.Equals("5"))
                lcCounselling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                lcCounselling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (_receiptState.Equals("7"))
                lcSendResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                lcSendResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }

        private void setControl()
        {
            lcgPartList.BeginUpdate();
            if (_listExamUpdate.Contains(_receiptState))
            {
                _visigleLayout = 1;
                lcGridPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcTreePart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcgPartList.CustomHeaderButtons[0].Properties.Visible = true;
                lcgPartList.CustomHeaderButtons[1].Properties.Visible = true;

            }
            else
            {
                _visigleLayout = 2;
                lcGridPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcTreePart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcgPartList.CustomHeaderButtons[0].Properties.Visible = false;
                lcgPartList.CustomHeaderButtons[1].Properties.Visible = false;
            }
            lcgPartList.EndUpdate();
        }

        private void reloadLayout(string receiptState)
        {
            if (_listExamUpdate.Contains(receiptState))
            {
                if (_visigleLayout == 2)
                {
                    usrReceiptPartTree1.setinitialize(_receiptId, _receipt, _receiptState);
                    usrReceiptPartTree1.getComponentAll();
                }
                else
                {
                    usrReceiptPartTree1.resetInfo(_receiptId, _receipt, _receiptState);
                }
            }
            else
            {
                if (_visigleLayout == 1)
                {
                    usrReceiptPart1.setinitialize(_receiptId, _receipt, 0);
                    usrReceiptPart1.getComponentAll();
                }
                else
                {

                }
            }
        }


        private void sbCustomerCheckComplete_Click_1(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("고객확인 사항을 저장하시겠습니까?") == DialogResult.Yes)
            {
                if (updateCustomerState())
                {
                    Dangol.Message("수정되었습니다.");
                }
            }
        }

        private void getCustomerState()
        {
            JObject jResult = new JObject();

            if (DBUsedPurchase.getCustomerState(_receipt, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    _returnState = ConvertUtil.ToInt32(jResult["RETURN_STATE"]);
                    rgCustomerCheckState.EditValue = ConvertUtil.ToInt32(jResult["STATE"]);
                    rgCustomerCheckFault.EditValue = _returnState;
                    _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);

                    _isExistCustomerCheck = true;
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private bool updateCustomerState()
        {
            JObject jResult = new JObject();

            int checkState = ConvertUtil.ToInt32(rgCustomerCheckState.EditValue);
            int returnState = ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue);
            if (DBUsedPurchase.updateCustomerState(_warehousingId, ConvertUtil.ToString(checkState), returnState, ref jResult))
            {
                _returnState = returnState;
                if (checkState > 1)
                    getRequest();
                else
                {
                    _dtCounselling.Clear();
                    _dtResponse.Clear();
                }


                return true;
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                return false;
            }
        }

        private void getRequest()
        {
            JObject jResult = new JObject();

            _dtCounselling.Clear();

            if (DBUsedPurchase.getCounselling(_warehousingId, 1, 1, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtCounselling.NewRow();
                        dr["REQUEST_ID"] = obj["REQUEST_ID"];
                        dr["REQUEST_CAT"] = obj["REQUEST_CAT"];
                        dr["REQUEST_DT"] = obj["REQUEST_DT"];
                        dr["DES"] = obj["DES"];
                        dr["CREATE_DT"] = obj["CREATE_DT"];
                        _dtCounselling.Rows.Add(dr);
                    }

                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void getResponse()
        {
            JObject jResult = new JObject();

            if (DBUsedPurchase.getResponse(ConvertUtil.ToInt64(_currentCounsellingRow["REQUEST_ID"]), ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtResponse.NewRow();
                        dr["DES"] = obj["DES"];
                        dr["USER_ID"] = obj["CREATE_USER_ID"];
                        dr["CREATE_DT"] = obj["CREATE_DT"];
                        _dtResponse.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        private void lcgCounselling_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentCounsellingRow == null)
            {
                Dangol.Warining("상담신청 내용이 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN("응대내용을 저장하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    if (DBUsedPurchase.InsertResponse(jobj, ref jResult))
                    {
                        DataRow dr = _dtResponse.NewRow();
                        dr["DES"] = meResponse.Text;
                        dr["user_id"] = ProjectInfo._userId;
                        dr["CREATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                        _dtResponse.Rows.InsertAt(dr, 0);

                        gvResponse.MoveFirst();

                        Dangol.Message("접수 정보가 수정되었습니다.");
                    }
                }
            }
        }

        private void gvCounseling_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvCounseling.RowCount > 0);

            _dtResponse.Clear();

            if (isValidRow)
            {
                _currentCounsellingRow = e.Row as DataRowView;
                getResponse();
            }
            else
                _currentCounsellingRow = null;
        }


        private void sbSave_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("접수정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jData = new JObject();

                checkUserInfo(ref jData);

                if (jData.Count > 0)
                {
                    jData.Add("WAREHOUSING_ID", _warehousingId);

                    jData.Add("RECEIPT_ID", ConvertUtil.ToInt64(_receiptId));
                    jData.Add("RECEIPT", ConvertUtil.ToString(_receipt));

                    jData.Add("USER_SEQ", ConvertUtil.ToInt64(_dtReceipt.Rows[0]["USER_SEQ"]));
                    jData.Add("USER_NO", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]));

                    if (DBUsedPurchase.updateReceiptInfo(jData, ref jResult))
                    {
                        if (!ConvertUtil.ToString(rgState.EditValue).Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"])))
                        {
                            StringBuilder postParams = new StringBuilder();
                            postParams.Append($"RECEIPT_STATE={ConvertUtil.ToString(rgState.EditValue)}");
                            postParams.Append($"&TEL={teTel.Text}");
                            postParams.Append($"&HP={teHp.Text}");
                            postParams.Append($"&RECEIPT={_dtReceipt.Rows[0]["RECEIPT"]}");
                            postParams.Append($"&CUSTOMER_NM={teCustomerNm.Text}");

                            if (DBUsedPurchase.updateDanawaReceiptState(postParams))
                            {
                                reloadLayout(ConvertUtil.ToString(rgState.EditValue));
                            }

                            _receiptState = ConvertUtil.ToString(rgState.EditValue);
                            usrReceiptPartTree1.resetInfo(_receiptId, _receipt, _receiptState);
                        }

                        getReceiptDetail();
                        Dangol.Message("접수 정보가 수정되었습니다.");

                    }
                }
                else
                {
                    if (!ConvertUtil.ToString(rgState.EditValue).Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"])))
                    {
                        StringBuilder postParams = new StringBuilder();
                        postParams.Append($"RECEIPT_STATE={ConvertUtil.ToString(rgState.EditValue)}");
                        //postParams.Append($"&USER_NM={teCustomerNm.Text}");
                        postParams.Append($"&TEL={teTel.Text}");
                        postParams.Append($"&HP={teHp.Text}");
                        postParams.Append($"&RECEIPT={_dtReceipt.Rows[0]["RECEIPT"]}");
                        postParams.Append($"&CUSTOMER_NM={teCustomerNm.Text}");

                        if (DBUsedPurchase.updateDanawaReceiptState(postParams))
                        {
                            reloadLayout(ConvertUtil.ToString(rgState.EditValue));
                            getReceiptDetail();
                            Dangol.Message("접수 정보가 수정되었습니다.");
                        }

                        _receiptState = ConvertUtil.ToString(rgState.EditValue);
                        usrReceiptPartTree1.resetInfo(_receiptId, _receipt, _receiptState);
                    }
                    else
                        Dangol.Message("수정 사항이 없습니다.");
                }
            }
            else
            {
                return;
            }
        }

        private void checkUserInfo(ref JObject jData)
        {
            if (!teCustomerNm.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_NM"])))
                jData.Add("USER_NM", teCustomerNm.Text);

            if (!teTel.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["TEL"])))
                jData.Add("TEL", teTel.Text);

            if (!teHp.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["HP"])))
                jData.Add("HP", teHp.Text);

            if (!tePostalCd.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["POSTAL_CD"])))
                jData.Add("POSTAL_CD", tePostalCd.Text);

            if (!teAddress.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS"])))
                jData.Add("ADDRESS", teAddress.Text);

            if (!teAddressDetail.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS_DETAIL"])))
                jData.Add("ADDRESS_DETAIL", teAddressDetail.Text);

            if (!ConvertUtil.ToString(rgUserType1.EditValue).Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_TYPE1"])))
                jData.Add("USER_TYPE1", ConvertUtil.ToString(rgUserType1.EditValue));

            if (!teAccountOwner.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_OWNER"])))
                jData.Add("ACCOUNT_OWNER", teAccountOwner.Text);

            if (!teBankNm.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["BANK_NM"])))
                jData.Add("BANK_NM", teBankNm.Text);

            if (!teAccount.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_NO"])))
                jData.Add("ACCOUNT_NO", teAccount.Text);

            if (!meDes.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["DES"])))
                jData.Add("DES", meDes.Text);

            if (!meAdjustDes.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ADJUST_DES"])))
                jData.Add("ADJUST_DES", meAdjustDes.Text);

            if (!meProcessDes.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["RES_DES"])))
                jData.Add("RES_DES", meProcessDes.Text);

        }



        private bool getReceiptData()
        {
            if (string.IsNullOrWhiteSpace(_receipt))
            {
                Dangol.Message("접수번호를 가져올수 없습니다. 다시 시도해 주세요.");
                return false;
            }

            JObject jResult = new JObject();

            if (DBUsedPurchase.getReceiptDetail(_receiptId, _receipt, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    _dtReceipt.Clear();
                    JObject jData = new JObject();
                    jData = (JObject)jResult["DATA"];


                    DataRow dr = _dtReceipt.NewRow();
                    dr["RECEIPT"] = jData["RECEIPT"];
                    dr["RECEIPT_DT"] = jData["RECEIPT_DT"];
                    dr["USER_NM"] = jData["USER_NM"];
                    dr["USER_SEQ"] = jData["USER_SEQ"];
                    dr["USER_ID"] = jData["USER_ID"];
                    dr["TEL"] = jData["TEL"];
                    dr["HP"] = jData["HP"];
                    dr["POSTAL_CD"] = jData["POSTAL_CD"];
                    dr["ADDRESS"] = jData["ADDRESS"];
                    dr["ADDRESS_DETAIL"] = jData["ADDRESS_DETAIL"];
                    dr["USER_TYPE1"] = jData["USER_TYPE1"];
                    dr["ACCOUNT_OWNER"] = jData["ACCOUNT_OWNER"];
                    dr["BANK_NM"] = jData["BANK_NM"];
                    dr["ACCOUNT_NO"] = jData["ACCOUNT_NO"];
                    dr["DES"] = jData["DES"];
                    //dr["PROCESS_DES"] = jData["PROCESS_DES"];
                    dr["ADJUST_DES"] = jData["ADJUST_DES"];
                    dr["RECEIPT_STATE"] = jData["RECEIPT_STATE"];
                    dr["RES_DES"] = jData["RES_DES"];

                    dr["MS_TYPE"] = jData["MS_TYPE"];
                    dr["F_COST"] = jData["F_COST"];
                    dr["F_ADJUST_COST"] = jData["F_ADJUST_COST"];
                    dr["F_TOTAL_COST"] = jData["F_TOTAL_COST"];

                    _dtReceipt.Rows.Add(dr);

                }
                if (Convert.ToBoolean(jResult["LIST_EXIST"]))
                {
                    _dtStateList.Clear();

                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtStateList.NewRow();
                        dr["RECEIPT_STATE"] = obj["RECEIPT_STATE"];
                        dr["USER_NM"] = obj["USER_NM"];
                        dr["DEPT_NM"] = obj["DEPT_NM"];
                        dr["REGIST_DT"] = obj["REGIST_DT"];
                        _dtStateList.Rows.Add(dr);
                    }

                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }

            return true;
        }

        private void layoutControlGroup5_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_listExamUpdate.Contains(_receiptState))
            {
                if (e.Button.Properties.Tag.Equals(1)) //추가
                {
                    Dangol.Message("현재 접수 상태에서는 '기타추가', '기타감소'만 추가 가능합니다.");
                }
                else if (e.Button.Properties.Tag.Equals(2)) //기타추가
                {
                    using (dlgNewETCPart newEtcPart = new dlgNewETCPart(_receipt))
                    {
                        if (newEtcPart.ShowDialog(this) == DialogResult.OK)
                        {
                            usrReceiptPartTree1.refresh();
                            seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartTree1._totalCost);
                        }
                    }
                }
                else if (e.Button.Properties.Tag.Equals(3))
                {
                    if (usrReceiptPartTree1._FocusedNode == null)
                    {
                        Dangol.Message("선택된 품목이 없습니다.");
                        return;
                    }

                    string componentCd = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["COMPONENT_CD"]);
                    if (!(componentCd.Equals("ETCADD") || componentCd.Equals("ETCMINUS")))
                    {
                        Dangol.Message("현재 접수 상태에서는 '기타추가', '기타감소'만 삭제 가능합니다.");
                        return;
                    }

                    if (Dangol.MessageYN("선택하신 품목을 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        usrReceiptPartTree1.deletePart();
                        usrReceiptPartTree1.refresh();
                        seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartTree1._totalCost);
                    }
                }
                else if (e.Button.Properties.Tag.Equals(4)) //수량변경
                {
                    string componentCd = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["COMPONENT_CD"]);
                    if (!(componentCd.Equals("ETCADD") || componentCd.Equals("ETCMINUS")))
                    {
                        Dangol.Message("현재 접수 상태에서는 '기타추가', '기타감소'만 수정 가능합니다.");
                        return;
                    }

                    using (dlgUpdatePartCnt updatePartCnt = new dlgUpdatePartCnt(usrReceiptPartTree1._FocusedNode["RECEIPT_PART_ID"], _receipt, usrReceiptPartTree1._FocusedNode["LT_COMPONENT"], usrReceiptPartTree1._FocusedNode["RECEIPT_PART_CNT"]))
                    {
                        if (updatePartCnt.ShowDialog(this) == DialogResult.OK)
                        {
                            usrReceiptPartTree1.updateCnt(updatePartCnt.Cnt);
                            seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartTree1._totalCost);
                        }
                    }

                }
                else if (e.Button.Properties.Tag.Equals(5)) //전체보기
                {
                    usrReceiptPartTree1.expandAll();
                }
                else if (e.Button.Properties.Tag.Equals(6)) //간략히
                {
                    usrReceiptPartTree1.foldAll();
                }
            }
            else
            {
                if (e.Button.Properties.Tag.Equals(1)) //추가
                {
                    using (dlgNewPart newPart = new dlgNewPart(_receiptId, _receipt, usrReceiptPart1._listUsedPart, -1))
                    {
                        if (newPart.ShowDialog(this) == DialogResult.OK)
                        {

                            usrReceiptPart1.refresh();
                            //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPart1._totalCost);

                        }
                    }
                }
                else if (e.Button.Properties.Tag.Equals(2)) //기타추가
                {
                    using (dlgNewETCPart newEtcPart = new dlgNewETCPart(_receipt))
                    {
                        if (newEtcPart.ShowDialog(this) == DialogResult.OK)
                        {
                            usrReceiptPart1.refresh();
                            //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPart1._totalCost);
                        }
                    }
                }
                else if (e.Button.Properties.Tag.Equals(3)) //삭제
                {
                    if (usrReceiptPart1._currentRow == null)
                    {
                        Dangol.Message("선택된 품목이 없습니다.");
                        return;
                    }
                    if (Dangol.MessageYN("선택하신 품목을 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        usrReceiptPart1.deletePart();
                        //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPart1._totalCost);
                    }
                }
                else if (e.Button.Properties.Tag.Equals(4)) //수량변경
                {
                    using (dlgUpdatePartCnt updatePartCnt = new dlgUpdatePartCnt(usrReceiptPart1._currentRow["PART_ID"], _receipt, usrReceiptPart1._currentRow["PARTCODE"], usrReceiptPart1._currentRow["PART_CNT"]))
                    {
                        if (updatePartCnt.ShowDialog(this) == DialogResult.OK)
                        {
                            //usrReceiptPart1.save(updatePartCnt.Cnt);
                            //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPart1._totalCost);
                        }
                    }

                }

                else if (e.Button.Properties.Tag.Equals(7)) //수량변경
                {
                    using (dlgUpdatePartCnt updatePartCnt = new dlgUpdatePartCnt(usrReceiptPart1._currentRow["PART_ID"], _receipt, usrReceiptPart1._currentRow["PARTCODE"], usrReceiptPart1._currentRow["PART_CNT"]))
                    {
                        if (updatePartCnt.ShowDialog(this) == DialogResult.OK)
                        {
                            //usrReceiptPart1.save(updatePartCnt.Cnt);
                            //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPart1._totalCost);
                        }
                    }

                }
            }
        }

        private void totalCostChange()
        {
            //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartTree1._totalCost);

            if (ConvertUtil.ToInt32(_dtReceipt.Rows[0]["MS_TYPE"]) < 0)
            {
                int cost = 0;
                int adjustCost = 0;
                int totalCost = 0;
                usrReceiptPartTree1.getSummaryValue(ref cost, ref adjustCost, ref totalCost);

                _dtReceipt.Rows[0]["F_COST"] = seFCost.EditValue = cost;
                _dtReceipt.Rows[0]["F_ADJUST_COST"] = seFAdjustCost.EditValue = adjustCost;
                _dtReceipt.Rows[0]["F_TOTAL_COST"] = seFTotalCost.EditValue = totalCost;


            }
        }

        private void sbAddress_Click(object sender, EventArgs e)
        {
            using (dlgGetAddress getAddress = new dlgGetAddress())
            {
                if (getAddress.ShowDialog(this) == DialogResult.OK)
                {
                    DataRow dr = (DataRow)getAddress.Tag;

                    tePostalCd.Text = $"{dr["zipcode"]}";
                    teAddress.Text = $"{dr["ADDR1"]}";
                    teAddressDetail.Text = "";



                    // usrReceiptPart1.updateCnt(updatePartCnt.Cnt);

                }
            }
        }

        private void seFCost_EditValueChanged(object sender, EventArgs e)
        {
            seFTotalCost.EditValue = $"{ConvertUtil.ToInt64(seFCost.EditValue) + ConvertUtil.ToInt64(seFAdjustCost.EditValue)}";
            _dtReceipt.Rows[0]["F_COST"] = ConvertUtil.ToInt64(seFCost.EditValue);
            _dtReceipt.Rows[0]["F_TOTAL_COST"] = ConvertUtil.ToInt64(seFTotalCost.EditValue);
        }

        private void seFAdjustCost_EditValueChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ToInt64(seFAdjustCost.EditValue) > 0)
            {
                Dangol.Message("차감 금액은 음수만 입력 가능합니다.");
                seFAdjustCost.EditValue = 0;
            }
            else
            {
                seFTotalCost.EditValue = $"{ConvertUtil.ToInt64(seFCost.EditValue) + ConvertUtil.ToInt64(seFAdjustCost.EditValue)}";
                _dtReceipt.Rows[0]["F_ADJUST_COST"] = ConvertUtil.ToInt64(seFAdjustCost.EditValue);
                _dtReceipt.Rows[0]["F_TOTAL_COST"] = ConvertUtil.ToInt64(seFTotalCost.EditValue);
            }
        }

        private void sbComplete_Click(object sender, EventArgs e)
        {
            if (ConvertUtil.ToString(rgMsType.EditValue).Equals("-1"))
            {
                Dangol.Message("정산비 상태를 선택하세요.");
                return;
            }


            if (Dangol.MessageYN("최종 비용 정보를 저장하시겠습니까??") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jData = new JObject();

                jData.Add("RECEIPT_ID", ConvertUtil.ToInt64(_receiptId));
                jData.Add("RECEIPT", ConvertUtil.ToString(_receipt));

                jData.Add("MONEY_TYPE", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_TYPE1"]));
                jData.Add("COM_CD", "");
                jData.Add("CUSTOMER_NO", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]));
                jData.Add("PICKUP_COM", "D865");
                jData.Add("F_PURCHASE_COST", ConvertUtil.ToInt64(seFCost.EditValue));

                //jData.Add("PURCHASE_COST", 0);
                //jData.Add("ADJUST_COST", ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADJUST_COST"]));

                jData.Add("F_ADJUST_COST", ConvertUtil.ToInt64(seFAdjustCost.EditValue) * -1);
                jData.Add("OUT_COST", 0);
                jData.Add("F_TOTAL_COST", ConvertUtil.ToInt64(seFTotalCost.EditValue));
                jData.Add("POST_COST", 0);
                jData.Add("DEPT_COST", 0);
                jData.Add("DEPT_WON", 0);
                jData.Add("DEPT_OFF", 0);
                jData.Add("DEPT_COST_ADD", 0);
                jData.Add("ROYALTY", 0);
                jData.Add("ROYALTY_ADD", 0);
                jData.Add("N_DEPT_TOTAL", 0);
                jData.Add("M_STATE", ConvertUtil.ToString(rgMsType.EditValue));
                jData.Add("BIGO", "");


                if (DBUsedPurchase.updateRecetFinalState(jData, ref jResult))
                {
                    if (!ConvertUtil.ToString(rgMsType.EditValue).Equals("1"))
                    {
                        if (ConvertUtil.ToString(rgMsType.EditValue).Equals("9"))
                            setIInitData();
                        getReceiptDetail();

                    }

                    _dtReceipt.Rows[0]["MS_TYPE"] = ConvertUtil.ToString(rgMsType.EditValue);

                    Dangol.Message("처리되었습니다.");
                }
                else
                {
                    return;
                }
            }
        }

        private void sbSendResult_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("검수 결과를 전송하시겠습니까?") == DialogResult.Yes)
            {
                JObject jData = new JObject();
                JObject jResult = new JObject();


                jData.Add("RECEIPT_ID", _receiptId);
                jData.Add("RECEIPT", _receipt);
                jData.Add("USER_SEQ", ConvertUtil.ToInt64(_dtReceipt.Rows[0]["USER_SEQ"]));
                jData.Add("RECEIPT_DT", ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_DT"]));
                jData.Add("CUSTOMER_NM", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_NM"]));
                jData.Add("CUSTOMER_HP", ConvertUtil.ToString(_dtReceipt.Rows[0]["HP"]));
                jData.Add("CUSTOMER_TEL", ConvertUtil.ToString(_dtReceipt.Rows[0]["TEL"]));
                jData.Add("POSTAL_CD", ConvertUtil.ToString(_dtReceipt.Rows[0]["POSTAL_CD"]));
                jData.Add("ADDRESS", ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS"]));
                jData.Add("ADDRESS_DETAIL", ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS_DETAIL"]));
                jData.Add("ACCOUNT_OWNER", ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_OWNER"]));
                jData.Add("BANK_NM", ConvertUtil.ToString(_dtReceipt.Rows[0]["BANK_NM"]));
                jData.Add("ACCOUNT_NO", ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_NO"]));
                jData.Add("ADJUST_DES", ConvertUtil.ToString(_dtReceipt.Rows[0]["ADJUST_DES"]));

                var jArray = usrReceiptPartTree1.makeExamComplete();

                jData.Add("DATA", jArray);

                if (DBUsedPurchase.updateReceiptExamComplete(jData, ref jResult))
                {
                    Object key = jResult["KEY"];
                    _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);
                    StringBuilder postParams = new StringBuilder();
                    postParams.Append($"RECEIPT_STATE=F");
                    postParams.Append($"&TEL={teTel.Text}");
                    postParams.Append($"&HP={teHp.Text}");
                    postParams.Append($"&RECEIPT={_dtReceipt.Rows[0]["RECEIPT"]}");
                    postParams.Append($"&CUSTOMER_NM={teCustomerNm.Text}");
                    postParams.Append($"&LINK={key}");

                    _isExistCustomerCheck = true;

                    if (DBUsedPurchase.updateDanawaReceiptState(postParams))
                    {
                        Dangol.Message("검수결과가 전송되었습니다.");
                    }
                    else
                    {
                        Dangol.Message("오류가 발생했습니다. ERROR: UR2");
                    }

                    if (ConvertUtil.ToInt32(rgCustomerCheckState.EditValue) == 0)
                    {
                        sbCustomerCheckComplete.Enabled = true;
                        rgCustomerCheckState.EditValue = 1;
                        rgCustomerCheckFault.EditValue = 0;

                    }


                    //Dangol.Message("검수결과가 전송되었습니다.");
                }
                else
                {
                    Dangol.Message("오류가 발생했습니다. ERROR: UR1");
                }
            }
        }

        private void sbCustomerCheckDes_Click(object sender, EventArgs e)
        {
            using (dlgSetCustomerCheckReturn getCustomerCheckReturn = new dlgSetCustomerCheckReturn(_receiptId, _receipt, _receiptState, _warehousingId, _returnState))
            {
                if (getCustomerCheckReturn.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
        }

        private void getFaultList()
        {
            _dtFaultList.Clear();

            if (_receiptId < 0)
            {
                Dangol.Message("접수번호를 가져올수 없습니다. 다시 시도해 주세요.");
                return;
            }

            JObject jResult = new JObject();

            if (DBUsedPurchase.getFaultList(_receiptId, _receipt, ref jResult))
            {
                _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtFaultList.NewRow();
                        dr["FAULT_ID"] = obj["FAULT_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["CNT"] = obj["CNT"];
                        _dtFaultList.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }


    }
}