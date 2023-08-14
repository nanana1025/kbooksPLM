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
using System.Collections;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrUsedPurchaseExamine : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtReceipt;
        DataTable _dtStateList;
        DataTable _dtFaultList;
        DataTable _dtUsedPurchase;

        DataTable _dtCounselling;
        DataTable _dtResponse;

        BindingSource _bsReceipt;
        BindingSource _bsStateList;
        BindingSource _bsUsedPurchase;

        BindingSource _bsCounselling;
        BindingSource _bsResponse;
        BindingSource _bsFaultList;

        DataRowView _currentCounsellingRow;
        DataRowView _currentReceipt;

        string _receipt;
        long _receiptId;
        long _warehousingId;
        bool _isExistCustomerCheck;
        string _receiptState = "-1";
        List<string> _listPartUpdate;
        List<string> _listCustomerUpdate;
        List<string> _listExamUpdate;
        List<string> _listCustomerCheckandComplete;
        List<string> _listCustomerCheckSate;

        List<string> _listFaultState;
        List<string> _listAddDelState;
        List<string> _listReleaseReturnState;
        List<string> _listStateEnabled;
        List<string> _listFinalPriceState;
        List<string> _listExamEnabled;

        Dictionary<string, string> _dicDangolToEtcStateMapping;

        int _visibleLayout = 1;
        int _returnState;
        long _releaseReturnId;
        int _sourceCd;
        int _mstype;

        bool initialize = true;
        bool initializeEnter = true;

        public usrUsedPurchaseExamine()
        {
            InitializeComponent();

            _receipt = "";

            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_ID", typeof(long)));
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
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RES_DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST_DT", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("MS_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("F_COST", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("F_ADJUST_COST", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("F_ADD_COST", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("F_TOTAL_COST", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("F_COST_DES", typeof(string)));


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

            _dtUsedPurchase = new DataTable();
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_ID", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("NO", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("SOURCE_CD", typeof(string)));
            
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TYPE", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TYPE2", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_NM", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_TEL", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_HP", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("PURCHASE_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("FAULT_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USER_COST", typeof(long)));
            _dtUsedPurchase.Columns.Add(new DataColumn("ADJUSTMENT_STATE", typeof(int)));
            _dtUsedPurchase.Columns.Add(new DataColumn("COMPLETE_DT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("USEDPURCHASE_STATE", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RECEIPT_REPORT", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("CHECK_STATE", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("RETURN_STATE", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("ETC", typeof(string)));
            _dtUsedPurchase.Columns.Add(new DataColumn("DT", typeof(long)));

            _bsReceipt = new BindingSource();
            _bsStateList = new BindingSource();
            _bsCounselling = new BindingSource();
            _bsResponse = new BindingSource();
            _bsUsedPurchase = new BindingSource();

            _bsStateList.DataSource = _dtStateList;
            _bsCounselling.DataSource = _dtCounselling;
            _bsResponse.DataSource = _dtResponse;
            _bsUsedPurchase.DataSource = _dtUsedPurchase;


            _listExamUpdate = new List<string>(new[] { "3", "5", "7", "C", "D", "E" });
            _listCustomerCheckandComplete = new List<string>(new[] { "5", "7", "C", "D", "E" });         
            _listCustomerCheckSate = new List<string>(new[] {"C", "D", "E" });

            _listCustomerUpdate = new List<string>(new[] { "1", "3", "7", "C", "D", "E" });
            _listPartUpdate = new List<string>(new[] { "1", "6", "3", "7", "C", "D", "E" });
            _listFaultState = new List<string>(new[] { "3", "7", "C", "D", "E" });
            _listAddDelState = new List<string>(new[] { "1", "6", "3", "7", "C", "D", "E" });
            _listReleaseReturnState = new List<string>(new[] { "5", "C", "D", "E" });
            _listStateEnabled = new List<string>(new[] { "1", "3", "6", "7", "4" });
            _listFinalPriceState = new List<string>(new[] { "5", "7", "C", "D", "E" });

            _listExamEnabled = new List<string>(new[] { "3", "6", "7", "4"});

            _dicDangolToEtcStateMapping = new Dictionary<string, string>();

            _isExistCustomerCheck = false;
            _returnState = 0;

            initialize = true;
            initializeEnter = true;
        }

        private void usrUsedPurchaseExamine_Load(object sender, EventArgs e)
        {
            usrReceiptPartResult1.totalCostChangeEvent += new usrReceiptPartResult.TotalCostChangeHandler(totalCostChange);
            usrReceiptPartResult1.visibleReturnCheck(false);
            setLookupEdit();
            //setIInitData();

            setGridControl();


            gvUsedPurchaseList.FocusedRowObjectChanged -= gvUsedPurchaseList_FocusedRowObjectChanged;
            JObject jResult = new JObject();
            getUsedPurchaseList(ref jResult);
            gvUsedPurchaseList.FocusedRowObjectChanged += gvUsedPurchaseList_FocusedRowObjectChanged;
            gvUsedPurchaseList.MoveFirst();


            initialize = false;

            ArrayList rows = new ArrayList();
            for (int i = 0; i < gvUsedPurchaseList.DataRowCount; i++)
            {
                int rowHandle = gvUsedPurchaseList.GetVisibleRowHandle(i);
                rows.Add(gvUsedPurchaseList.GetDataRow(rowHandle));
            }

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                // Change the field value.
                row["NO"] = i + 1;
            }
        }


        private void setGridControl()
        {
            gcStateList.DataSource = null;
            gcStateList.DataSource = _bsStateList;

            gcCounseling.DataSource = null;
            gcCounseling.DataSource = _bsCounselling;

            gcResponse.DataSource = null;
            gcResponse.DataSource = _bsResponse;

            gcUsedPurchaseList.DataSource = null;
            gcUsedPurchaseList.DataSource = _bsUsedPurchase;

            _bsUsedPurchase.Sort = "DT DESC";
        }

        public void setLookupEdit()
        {
            DataTable dtUsedPurchaseState = Util.getCodeList("CD1303", "KEY", "VALUE");
            Util.LookupEditHelper(rileReceiptState, dtUsedPurchaseState, "KEY", "VALUE");
            RadioGroupItem[] rgState = new RadioGroupItem[dtUsedPurchaseState.Rows.Count];

            DataTable dtUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", "STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(rileUserId, dtUserId, "KEY", "VALUE");

            int index = 0;
            foreach (DataRow dr in dtUsedPurchaseState.Rows)
            {
                string state = ConvertUtil.ToString(dr["KEY"]);
                string name = ConvertUtil.ToString(dr["VALUE"]);
                bool enabled = _listExamEnabled.Contains(state);
                    
                RadioGroupItem rgItem = new RadioGroupItem(state, name, enabled, state);
                rgState[index++] = rgItem;
            }
            this.rgState.Properties.Items.AddRange(rgState);

            Util.insertRowonTop(dtUsedPurchaseState, "-1", "선택안합");
            Util.LookupEditHelper(rileUsedPurchaseState, dtUsedPurchaseState, "KEY", "VALUE");
            Util.LookupEditHelper(leReceiptState, dtUsedPurchaseState, "KEY", "VALUE");

            meCounselling.DataBindings.Add(new Binding("Text", _bsCounselling, "DES", false, DataSourceUpdateMode.Never));
            meResponse.DataBindings.Add(new Binding("Text", _bsResponse, "DES", false, DataSourceUpdateMode.Never));

            DataTable dtSource = Util.getCodeList("CD1309", "KEY", "VALUE");
            Util.LookupEditHelper(rileSource, dtSource, "KEY", "VALUE");

            DataTable dtReceiptType = Util.getCodeList("CD1310", "KEY", "VALUE");
            Util.LookupEditHelper(leReceiptType, dtReceiptType, "KEY", "VALUE");

            DataTable dtDangolToEtcStateMapping = Util.getCodeList("CD1311", "KEY", "VALUE");
            foreach (DataRow row in dtDangolToEtcStateMapping.Rows)
            {
                if (!_dicDangolToEtcStateMapping.ContainsKey(ConvertUtil.ToString(row["KEY"])))
                    _dicDangolToEtcStateMapping.Add(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));
            }


            var today = DateTime.Today;
            var pastDate = today.AddDays(-30);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
            leReceiptState.ItemIndex = 0;

            setControl();
        }
        private void setResultSendLayout()
        {
            if (_listCustomerCheckandComplete.Contains(_receiptState))
            {
                lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                _isExistCustomerCheck = false;
                getCustomerState();
                //if (_isExistCustomerCheck)
                //    getRequest();
                //else
                //{
                //    _returnState = 0;
                //    rgCustomerCheckFault.EditValue = _returnState;
                //    _dtCounselling.Clear();
                //}
            }
            else
            {
                lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                _returnState = 0;
                rgCustomerCheckFault.EditValue = _returnState;
                _isExistCustomerCheck = false;
                //_dtCounselling.Clear();
            }
        }

        public void reload(object receiptId, object receipt, object receiptState)
        {
            _receiptId = ConvertUtil.ToInt64(receiptId);
            _receipt = ConvertUtil.ToString(receipt);
            _receiptState = ConvertUtil.ToString(receiptState);

            setControl();

            setIInitData(true);
        }

        private void setIInitData(bool reload = false)
        {
            usrReceiptPart1.Clear();
            
            if (getReceiptDetail())
            {
                if (_listExamUpdate.Contains(_receiptState))
                {
                    if (reload)
                        usrReceiptPartResult1.resetInfo(_receiptId, _receipt, _receiptState, _sourceCd);
                    else
                        usrReceiptPartResult1.setinitialize(_receiptId, _receipt, _receiptState, _sourceCd);
                    usrReceiptPartResult1.getComponentAll();

                    lcgPartList.CustomHeaderButtons[1].Properties.Enabled = true;
                }
                else
                {
                    if (reload)
                        usrReceiptPart1.resetInfo(_receiptId, _receipt, _sourceCd);
                    else
                        usrReceiptPart1.setinitialize(_receiptId, _receipt, _sourceCd);
                    usrReceiptPart1.getComponentAll();

                    lcgPartList.CustomHeaderButtons[1].Properties.Enabled = false;
                }

            }
        }

        private bool getReceiptDetail()
        {
            _dtReceipt.Clear();
            _dtStateList.Clear();

            if (getReceiptData())
            {
                _receiptId = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["RECEIPT_ID"]);
                _receiptState = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"]);

                teReceipt.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT"]);
                teCustomerNm.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_NM"]);
                teCustomerNo.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]);
                teTel.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["TEL"]);
                teHp.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["HP"]);
                //tePostalCd.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["POSTAL_CD"]);
                //teAddress.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS"]);
                //teAddressDetail.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS_DETAIL"]);
                //rgUserType1.EditValue = ConvertUtil.ToInt32(_dtReceipt.Rows[0]["USER_TYPE1"]);
                //teAccountOwner.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_OWNER"]);
                //teBankNm.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["BANK_NM"]);
                //teAccount.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_NO"]);
                meDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["DES"]);
                rgState.EditValue = _receiptState;
                //meProcessDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["PROCESS_DES"]);
                meAdjustDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ADJUST_DES"]);
                leReceiptType.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_TYPE"]);

                //rgUserType3.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_TYPE3"]);
                meProcessDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["RES_DES"]);
                deRequestDt.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["REQUEST_DT"]); 


                //rgMsType.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["MS_TYPE"]);

                //seFCost.EditValueChanged -= seFCost_EditValueChanged;
                //seFAdjustCost.EditValueChanged -= seFAdjustCost_EditValueChanged;
                //seFAddCost.EditValueChanged -= seFAddCost_EditValueChanged;

                //seFCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_COST"]);
                //seFAdjustCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADJUST_COST"]) * -1;
                //seFAddCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADD_COST"]);
                //seFTotalCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_TOTAL_COST"]);

                //seFCost.EditValueChanged += seFCost_EditValueChanged;
                //seFAdjustCost.EditValueChanged += seFAdjustCost_EditValueChanged;
                //seFAddCost.EditValueChanged += seFAddCost_EditValueChanged;

                //meFcostDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["F_COST_DES"]);

                setControl();

                //if (_listCustomerCheckandComplete.Contains(_receiptState))
                //{
                //    lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //    _isExistCustomerCheck = false;
                //    getCustomerState();
                //    if (_isExistCustomerCheck)
                //        getRequest();
                //    else
                //    {
                //        _returnState = 0;
                //        rgCustomerCheckFault.EditValue = _returnState;
                //        _dtCounselling.Clear();
                //    } 
                //}
                //else
                //{
                //    lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //    _returnState = 0;
                //    rgCustomerCheckFault.EditValue = _returnState;
                //    _isExistCustomerCheck = false;
                //    _dtCounselling.Clear();
                //}

                //if (_listFinalPriceState.Contains(_receiptState))
                //    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //else
                //    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                
                usrReceiptPartResult1._msType = _mstype = ConvertUtil.ToInt32(_dtReceipt.Rows[0]["MS_TYPE"]);
                checkReceiptState();

                return true;
            }
            else
            {
                _mstype = 99;
                usrReceiptPartResult1._msType = 99;
                return false;
            }
        }

        private void checkReceiptState()
        {
            if (_listExamUpdate.Contains(_receiptState))
                lcgPartList.CustomHeaderButtons[1].Properties.Enabled = true;
            else
                lcgPartList.CustomHeaderButtons[1].Properties.Enabled = false;

            if (_listFaultState.Contains(_receiptState) && _mstype < 1) //불량 등록
                lcgPartList.CustomHeaderButtons[2].Properties.Enabled = true;
            else
                lcgPartList.CustomHeaderButtons[2].Properties.Enabled = false;

            if (_listAddDelState.Contains(_receiptState) && _mstype < 1) //추가 삭제
            {
                lcgPartList.CustomHeaderButtons[3].Properties.Enabled = true;
                lcgPartList.CustomHeaderButtons[4].Properties.Enabled = true;
                lcgPartList.CustomHeaderButtons[5].Properties.Enabled = true;
            }
            else
            {
                lcgPartList.CustomHeaderButtons[3].Properties.Enabled = false;
                lcgPartList.CustomHeaderButtons[4].Properties.Enabled = false;
                lcgPartList.CustomHeaderButtons[5].Properties.Enabled = false;
            }

            //if (_listReleaseReturnState.Contains(_receiptState)) //반송
            //    lcgPartList.CustomHeaderButtons[4].Properties.Enabled = true;
            //else
            //    lcgPartList.CustomHeaderButtons[4].Properties.Enabled = false;

            if (_listPartUpdate.Contains(_receiptState) && _mstype < 1) //저장
                lcgPartList.CustomHeaderButtons[6].Properties.Enabled = true;
            else
                lcgPartList.CustomHeaderButtons[6].Properties.Enabled = false;

            if (_listCustomerUpdate.Contains(_receiptState) && _mstype < 1) //고객수정버튼
                lcgCustomerInfo.CustomHeaderButtons[0].Properties.Enabled = true;
            else
                lcgCustomerInfo.CustomHeaderButtons[0].Properties.Enabled = false;

            if (_listCustomerUpdate.Contains(_receiptState) && _mstype < 1) //고객수정버튼
                lcgCustomerInfo.CustomHeaderButtons[0].Properties.Enabled = true;
            else
                lcgCustomerInfo.CustomHeaderButtons[0].Properties.Enabled = false;

            if (_listStateEnabled.Contains(_receiptState)) //상담저장버튼
            {
                rgState.Enabled = true;
                lcgReceiptState.CustomHeaderButtons[0].Properties.Enabled = true;
            }
            else
            {
                rgState.Enabled = false;
                lcgReceiptState.CustomHeaderButtons[0].Properties.Enabled = false;
            }

            //if (_listFinalPriceState.Contains(_receiptState)) //최종비용수정버튼
            //    lcgFinalCost.CustomHeaderButtons[0].Properties.Enabled = true;
            //else
            //    lcgFinalCost.CustomHeaderButtons[0].Properties.Enabled = false;

        }

        private void setControl()
        {
            lcgPartList.BeginUpdate();
            if (_listExamUpdate.Contains(_receiptState)) // 검수
            {
                _visibleLayout = 1;
                lcGridPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcGridExam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else // 접수
            {
                _visibleLayout = 2;
                lcGridPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcGridExam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            lcgPartList.EndUpdate();
        }

        private void reloadLayout(string receiptState)
        {
            if (_listExamUpdate.Contains(receiptState))
            {
                if (_visibleLayout == 2)
                {
                    usrReceiptPartResult1.setinitialize(_receiptId, _receipt, _receiptState, _sourceCd);
                    usrReceiptPartResult1.getComponentAll();
                }
                else
                {
                    //usrReceiptPart1.setinitialize(_receiptId, _receipt);
                    //usrReceiptPart1.getComponentAll();
                }
            }
            else
            {
                if (_visibleLayout == 1)
                {
                    usrReceiptPart1.setinitialize(_receiptId, _receipt, _sourceCd);
                    usrReceiptPart1.getComponentAll();
                }
                else
                {
                    //usrReceiptPartResult1.setinitialize(_receiptId, _receipt, _receiptState);
                    //usrReceiptPartResult1.getComponentAll();
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
                    rgCustomerCheckFault.EditValue = _returnState;
                    _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);

                    _isExistCustomerCheck = true;
                }

                if (Convert.ToBoolean(jResult["RETURN_EXIST"]))
                {
                    teReleaseReceipt.Text = ConvertUtil.ToString(jResult["RELEASE_RECEIPT"]);
                    _releaseReturnId = ConvertUtil.ToInt64(jResult["RELEASE_ID"]);
                }

            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        /*
         * 
         * 접수 번호 처리
         * 
         * 
         */

        private void gvUsedPurchaseList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvUsedPurchaseList.RowCount > 0);

            if (isValidRow)
            {
                _currentReceipt = e.Row as DataRowView;
                _receiptId = ConvertUtil.ToInt64(_currentReceipt["RECEIPT_ID"]);
                _receipt = ConvertUtil.ToString(_currentReceipt["RECEIPT"]);
                _sourceCd = ConvertUtil.ToInt32(_currentReceipt["SOURCE_CD"]);

                if (_receiptId > 0)
                {
                    if (!initialize)
                        Dangol.ShowSplash();

                    setIInitData();

                    if (!initialize)
                        Dangol.CloseSplash();
                }
                else
                {

                }

                lcgCustomerInfo.BeginUpdate();
                if (_sourceCd == 0)
                {
                    lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcRequestDt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcRequestDt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                lcgCustomerInfo.EndUpdate();
            }
            else
            {
                _receiptId = -1;
                _receipt = "-1";
                _sourceCd = -1;
                _currentCounsellingRow = null;

                lcgCustomerInfo.BeginUpdate();
                lcReceiptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcRequestDt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgCustomerInfo.EndUpdate();
            }
        }


        /*
         * 
         * 고객 확인
         * 
         */
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

        private void lcgCounselling_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            // 현재 화면에서는 보이지 않기 때문에 사용안함, 사용할 경우, DataTable 정보를 usrUsedPurchaseReceiptDetail 처럼 수정해야함.
            if (_currentCounsellingRow == null)
            {
                Dangol.Warining("문의 내용이 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN("응대내용을 저장하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    jobj.Add("REQUEST_ID", ConvertUtil.ToInt64(_currentCounsellingRow["REQUEST_ID"]));
                    jobj.Add("DES", meResponse.Text);

                    if (DBUsedPurchase.InsertResponse(jobj, ref jResult))
                    {
                        DataRow dr = _dtResponse.NewRow();
                        dr["DES"] = meResponse.Text;
                        dr["user_id"] = ProjectInfo._userId;
                        dr["CREATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                        _dtResponse.Rows.InsertAt(dr, 0);

                        gvResponse.MoveFirst();

                        Dangol.Message("상담 정보가 수정되었습니다.");
                    }
                }
            }
        }

        private void getRequest()
        {
            JObject jResult = new JObject();

            _dtCounselling.Clear();

            if (DBUsedPurchase.getCounselling(_warehousingId, _receiptId, _sourceCd, ref jResult))
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



        /*
         * 
         * 고객 정보 수정
         * 
         * 
         */
        private void lcgCustomerInfo_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("고객정보를 수정하시겠습니까?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                JObject jData = new JObject();
                bool isSuccess = false;

                checkUserInfo(ref jData);

                if (jData.Count > 0)
                {
                    jData.Add("WAREHOUSING_ID", _warehousingId);

                    jData.Add("RECEIPT_ID", ConvertUtil.ToInt64(_receiptId));
                    jData.Add("RECEIPT", ConvertUtil.ToString(_receipt));

                    jData.Add("USER_SEQ", ConvertUtil.ToInt64(_dtReceipt.Rows[0]["USER_SEQ"]));
                    jData.Add("USER_NO", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]));

                    if (_sourceCd == 0)
                        isSuccess = DBUsedPurchase.updateDanawaReceiptInfo(jData, ref jResult);
                    else
                        isSuccess = DBUsedPurchase.updateReceiptInfo(jData, ref jResult);

                    if (isSuccess)
                    {
                        updateUserInfo(jData);
                        Dangol.Message("접수 정보가 수정되었습니다.");
                    }
                }
                else
                {
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
            //if (!teCustomerNm.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_NM"])))
            //    jData.Add("USER_NM", teCustomerNm.Text);

            //if (!teTel.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["TEL"])))
            //    jData.Add("TEL", teTel.Text);

            //if (!teHp.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["HP"])))
            //    jData.Add("HP", teHp.Text);

            //if (!tePostalCd.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["POSTAL_CD"])))
            //    jData.Add("POSTAL_CD", tePostalCd.Text);

            //if (!teAddress.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS"])))
            //    jData.Add("ADDRESS", teAddress.Text);

            //if (!teAddressDetail.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ADDRESS_DETAIL"])))
            //    jData.Add("ADDRESS_DETAIL", teAddressDetail.Text);

            //if (!ConvertUtil.ToString(rgUserType1.EditValue).Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_TYPE1"])))
            //    jData.Add("USER_TYPE1", ConvertUtil.ToString(rgUserType1.EditValue));

            //if (!teAccountOwner.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_OWNER"])))
            //    jData.Add("ACCOUNT_OWNER", teAccountOwner.Text);

            //if (!teBankNm.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["BANK_NM"])))
            //    jData.Add("BANK_NM", teBankNm.Text);

            //if (!teAccount.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_NO"])))
            //    jData.Add("ACCOUNT_NO", teAccount.Text);

            //if (!meDes.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["DES"])))
            //    jData.Add("DES", meDes.Text);

            if (!meAdjustDes.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["ADJUST_DES"])))
                jData.Add("ADJUST_DES", meAdjustDes.Text);

            if (!meProcessDes.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["RES_DES"])))
                jData.Add("RES_DES", meProcessDes.Text);


            if (_sourceCd != 0)
            {
                string requestDt = ConvertUtil.ToString(_dtReceipt.Rows[0]["REQUEST_DT"]);

                if (!requestDt.Equals(deRequestDt.Text))
                {
                    DateTime dt = Convert.ToDateTime(deRequestDt.EditValue);
                    jData.Add("REQUEST_DT", dt.ToString("yyyy-MM-dd"));
                }
            }

        }

        private void updateUserInfo(JObject jData)
        {
            if (jData.ContainsKey("TEL"))
                _dtReceipt.Rows[0]["TEL"] = jData["TEL"];

            if (jData.ContainsKey("HP"))
                _dtReceipt.Rows[0]["HP"] = jData["HP"];

            if (jData.ContainsKey("POSTAL_CD"))
                _dtReceipt.Rows[0]["POSTAL_CD"] = jData["POSTAL_CD"];

            if (jData.ContainsKey("ADDRESS"))
                _dtReceipt.Rows[0]["ADDRESS"] = jData["ADDRESS"];

            if (jData.ContainsKey("ADDRESS_DETAIL"))
                _dtReceipt.Rows[0]["ADDRESS_DETAIL"] = jData["ADDRESS_DETAIL"];

            if (jData.ContainsKey("USER_TYPE1"))
                _dtReceipt.Rows[0]["USER_TYPE1"] = jData["USER_TYPE1"];

            if (jData.ContainsKey("ACCOUNT_OWNER"))
                _dtReceipt.Rows[0]["ACCOUNT_OWNER"] = jData["ACCOUNT_OWNER"];

            if (jData.ContainsKey("USER_TYPE1"))
                _dtReceipt.Rows[0]["USER_TYPE1"] = jData["USER_TYPE1"];

            if (jData.ContainsKey("ACCOUNT_OWNER"))
                _dtReceipt.Rows[0]["ACCOUNT_OWNER"] = jData["ACCOUNT_OWNER"];

            if (jData.ContainsKey("ACCOUNT_NO"))
                _dtReceipt.Rows[0]["ACCOUNT_NO"] = jData["ACCOUNT_NO"];

            if (jData.ContainsKey("BANK_NM"))
                _dtReceipt.Rows[0]["BANK_NM"] = jData["BANK_NM"];

            if (jData.ContainsKey("DES"))
                _dtReceipt.Rows[0]["DES"] = jData["DES"];

            if (jData.ContainsKey("ADJUST_DES"))
                _dtReceipt.Rows[0]["ADJUST_DES"] = jData["ADJUST_DES"];

            if (jData.ContainsKey("RES_DES"))
                _dtReceipt.Rows[0]["RES_DES"] = jData["RES_DES"];

            if (jData.ContainsKey("REQUEST_DT"))
                _dtReceipt.Rows[0]["REQUEST_DT"] = jData["REQUEST_DT"];
        }


        /*
         * 
         * 진행 상태 
         * 
         * 
         */

        private void lcgReceiptState_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Dangol.MessageYN("진행상태를 수정하시겠습니까?") == DialogResult.Yes)
            {
                if (_sourceCd == 0) //다나와
                {
                    if (!ConvertUtil.ToString(rgState.EditValue).Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"])))
                    {
                        string state = ConvertUtil.ToString(rgState.EditValue);

                        StringBuilder postParams = new StringBuilder();
                        postParams.Append($"RECEIPT_STATE={state}");
                        postParams.Append($"&TEL={teTel.Text}");
                        postParams.Append($"&HP={teHp.Text}");
                        postParams.Append($"&RECEIPT={_dtReceipt.Rows[0]["RECEIPT"]}");
                        postParams.Append($"&CUSTOMER_NM={teCustomerNm.Text}");
                        postParams.Append($"&CUSTOMER_CHECK=0");

                        if (DBUsedPurchase.updateDanawaReceiptState(postParams))
                        {

                        }

                        if (_listCustomerCheckSate.Contains(state))
                        {
                            checkCustomerCheck(state);
                        }

                        _receiptState = state;
                        _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;
                        _currentReceipt["USEDPURCHASE_STATE"] = _receiptState;
                        reloadLayout(state);
                        setControl();
                        checkReceiptState();
                        getStateHistory();

                        if (_receiptState.Equals("C"))
                            Dangol.Message("검수결과가 전송되었습니다.");
                        else
                            Dangol.Message("접수 정보가 수정되었습니다.");
                    }
                    else
                    {
                        string state = ConvertUtil.ToString(rgState.EditValue);
                        if (_listCustomerCheckSate.Contains(state))
                        {
                            if (state.Equals("C"))
                            {
                                sendExamResult(state);
                                Dangol.Message("검수결과가 전송되었습니다.");
                            }
                            else
                            {
                                if (_warehousingId > 0 && ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue) != _returnState)
                                    updateCustomerState(state);

                                Dangol.Message("접수 정보가 수정되었습니다.");
                            }
                        }
                        else
                            Dangol.Message("수정 사항이 없습니다.");
                    }
                }
                else
                {
                    string state = ConvertUtil.ToString(rgState.EditValue);

                    bool isReceiptStateupdated = false;
                    if (!state.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"])))
                    {
                        JObject jResult = new JObject();
                        JObject jData = new JObject();

                        jData.Add("RECEIPT_ID", ConvertUtil.ToInt64(_receiptId));
                        jData.Add("RECEIPT", ConvertUtil.ToString(_receipt));
                        if (_dicDangolToEtcStateMapping.ContainsKey(state))
                            jData.Add("RECEIPT_STATE_ETC", _dicDangolToEtcStateMapping[state]);
                        jData.Add("RECEIPT_STATE", state);

                        if (DBUsedPurchase.updateReceiptState(jData, ref jResult))
                        {
                            isReceiptStateupdated = true;
                            _receiptState = state;
                            _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;
                            _currentReceipt["USEDPURCHASE_STATE"] = _receiptState;

                            reloadLayout(state);
                            setControl();
                            checkReceiptState();
                            getStateHistory();
                        }

                    }

                    setResultSendLayout(); //확인해 봐야함.

                    Dangol.CloseSplash();

                    if (_receiptState.Equals("C"))
                        Dangol.Message("검수결과가 전송되었습니다.");
                    else
                    {
                        if (isReceiptStateupdated)
                            Dangol.Message("접수 정보가 수정되었습니다.");
                    }
                }
                   
            }       
        }

        private void checkCustomerCheck(string state)
        {
            if (state.Equals("C"))
            {
                sendExamResult(state);
            }
            else
            {
                if(_warehousingId > 0)
                    updateCustomerState(state);
            }
        }

        private void sendExamResult(string state)
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

            var jArray = usrReceiptPartResult1.makeExamComplete();

            jData.Add("DATA", jArray);

            if (DBUsedPurchase.updateReceiptExamComplete(jData, ref jResult))
            {
                Object key = jResult["KEY"];
                _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);
                StringBuilder postParams = new StringBuilder();
                postParams.Append($"RECEIPT_STATE=C");
                postParams.Append($"&TEL={teTel.Text}");
                postParams.Append($"&HP={teHp.Text}");
                postParams.Append($"&RECEIPT={_dtReceipt.Rows[0]["RECEIPT"]}");
                postParams.Append($"&CUSTOMER_NM={teCustomerNm.Text}");
                postParams.Append($"&KEY={key}");
                postParams.Append($"&CUSTOMER_CHECK=1");

                _isExistCustomerCheck = true;

                if (DBUsedPurchase.updateDanawaReceiptState(postParams))
                {
                    //Dangol.Message("검수결과가 전송되었습니다.");
                }
                else
                {
                    Dangol.Message("오류가 발생했습니다. ERROR: UR2");
                }
            }
            else
            {
                Dangol.Message("오류가 발생했습니다. ERROR: UR1");
            }

        }

        private bool updateCustomerState(string state)
        {
            JObject jResult = new JObject();

            if (state == "C")
                state = "1";
            else if(state == "D")
                state = "3";
            else if (state == "E")
                state = "2";

            int returnState = ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue);
            if (DBUsedPurchase.updateCustomerState(_warehousingId, state, returnState, ref jResult))
            {
                _returnState = returnState;
                getRequest();

                if (_listFinalPriceState.Contains(_receiptState))
                    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                return true;
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                return false;
            }
        }

        /*
         * 
         * 
         * 최종 비용 정보 수정
         * 
         * 
         */

        private void lcgFinalCost_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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
                jData.Add("OUT_COST", ConvertUtil.ToInt64(seFAddCost.EditValue));
                jData.Add("F_TOTAL_COST", ConvertUtil.ToInt64(seFTotalCost.EditValue));
                jData.Add("BIGO", meFcostDes.Text);
                jData.Add("POST_COST", 0);
                jData.Add("DEPT_COST", 0);
                jData.Add("DEPT_WON", 0);
                jData.Add("DEPT_OFF", 0);
                jData.Add("DEPT_COST_ADD", 0);
                jData.Add("ROYALTY", 0);
                jData.Add("ROYALTY_ADD", 0);
                jData.Add("N_DEPT_TOTAL", 0);
                jData.Add("M_STATE", ConvertUtil.ToString(rgMsType.EditValue));

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


        private bool getReceiptData()
        {
            if (string.IsNullOrWhiteSpace(_receipt))
            {
                //Dangol.Message("접수번호를 가져올수 없습니다. 다시 시도해 주세요.");
                return false;
            }

            JObject jResult = new JObject();
            bool isSuccess = false;

            if (_sourceCd == 0)
                isSuccess = DBUsedPurchase.getDanawaReceiptDetail(_receiptId, _receipt, ref jResult);
            else
                isSuccess = DBUsedPurchase.getReceiptDetail(_receiptId, _receipt, ref jResult);

            if (isSuccess)
            {
                _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JObject jData = new JObject();
                    jData = (JObject)jResult["DATA"];

                    DataRow dr = _dtReceipt.NewRow();
                    dr["RECEIPT_ID"] = jData["RECEIPT_ID"];
                    dr["RECEIPT"] = jData["RECEIPT"];
                    dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(jData["RECEIPT_DT"]);
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
                    dr["F_ADD_COST"] = jData["F_ADD_COST"];
                    dr["F_TOTAL_COST"] = jData["F_TOTAL_COST"];

                    dr["F_COST_DES"] = jData["F_COST_DES"];
                    dr["REQUEST_DT"] = jData["REQUEST_DT"];

                    _dtReceipt.Rows.Add(dr);

                    if (_sourceCd != 1)
                    {
                        int receiptType = ConvertUtil.ToInt32(jData["RECEIPT_TYPE"]);
                        int serviceType = ConvertUtil.ToInt32(jData["SERVICE_TYPE"]);

                        if (receiptType > 0)
                            dr["RECEIPT_TYPE"] = ConvertUtil.ToString(receiptType);
                        else if (serviceType > 0)
                            dr["RECEIPT_TYPE"] = ConvertUtil.ToString(serviceType * -1);
                    }
                    else
                        dr["RECEIPT_TYPE"] = jData["RECEIPT_TYPE"];

                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString("접수 데이터가 없습니다"));
                    return false;
                }
                if (Convert.ToBoolean(jResult["LIST_EXIST"]))
                {
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
                return true;
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                return false;
            }

            
        }

        private void getStateHistory()
        {
            JObject jResult = new JObject();

            _dtStateList.Clear();

            bool isSuccess = false;

            if (_sourceCd == 0)
                isSuccess = DBUsedPurchase.getDanawaStateHistory(_receiptId, _receipt, ref jResult);
            else
                isSuccess = DBUsedPurchase.getStateHistory(_receiptId, _receipt, ref jResult);

            if (isSuccess)
            {
                if (Convert.ToBoolean(jResult["LIST_EXIST"]))
                {
                    gvStateList.BeginDataUpdate();
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtStateList.NewRow();
                        dr["RECEIPT_STATE"] = obj["RECEIPT_STATE"];
                        dr["USER_NM"] = obj["USER_NM"];
                        dr["DEPT_NM"] = obj["DEPT_NM"];
                        dr["REGIST_DT"] = obj["REGIST_DT"];
                        _dtStateList.Rows.Add(dr);
                    }

                    gvStateList.EndDataUpdate();

                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }


        private void lcgPartList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.Equals(0)) //REFRESH
            {
                bool refreshPossible = false;
                if (_visibleLayout == 1)
                    refreshPossible = usrReceiptPartResult1.refreshCheck();
                else
                    refreshPossible = usrReceiptPart1.refreshCheck();

                if (refreshPossible)
                {
                    refreshGrid();
                    Dangol.Message("새로고침 완료");
                }
                else
                {
                    if (Dangol.MessageYN("수정중인 데이터가 있습니다. 그래도 새로고침하시겠습니까?") == DialogResult.Yes)
                    {
                        refreshGrid();
                        Dangol.Message("새로고침 완료");
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(1)) //불량등록
            {
                using (dlgFaultPart faultPart = new dlgFaultPart(_receipt, usrReceiptPart1._listUsedPart))
                {
                    if (faultPart.ShowDialog(this) == DialogResult.OK)
                    {
                        refreshGrid();
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(2)) //추가
            {
                using (dlgNewPart newPart = new dlgNewPart(_receiptId, _receipt, usrReceiptPart1._listUsedPart, _sourceCd))
                {
                    if (newPart.ShowDialog(this) == DialogResult.OK)
                    {
                        refreshGrid();
                    }
                }
            }
            else if (e.Button.Properties.Tag.Equals(3)) //삭제
            {
                if (Dangol.MessageYN("선택하신 접수 품목을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    if (_visibleLayout == 1)
                        usrReceiptPartResult1.deletePart();
                    else
                        usrReceiptPart1.deletePart();

                    refreshGrid();

                    //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartResult1._totalCost);
                }
            }
            else if (e.Button.Properties.Tag.Equals(4)) //가격 가져오기
            {
                if (_visibleLayout == 2)
                {
                    Dangol.Message("현재 상태에서는 가격을 가져올 수 없습니다.");
                }
                else
                {
                    usrReceiptPartResult1.getPrice();
                }
            }
            else if (e.Button.Properties.Tag.Equals(5)) //저장
            {
                if (Dangol.MessageYN("현재 검수결과를 저장하시겠습니까?") == DialogResult.Yes)
                {
                    if (_visibleLayout == 1)
                    {
                        usrReceiptPartResult1.save();
                    }
                    else
                        usrReceiptPart1.save();
                }
            }
            else if (e.Button.Properties.Tag.Equals(6)) 
            {
                string componentCd = "";
                string barcode = usrReceiptPartResult1.getBarcode(ref componentCd);
                if (string.IsNullOrWhiteSpace(barcode))
                    Dangol.Message("등록된 사진이 없습니다.");
                else
                    ImageInfo.GetImage(1, componentCd.Equals("NTB"), barcode);
            }
        }

        private void refreshGrid()
        {
            if (_visibleLayout == 1)
            {
                usrReceiptPartResult1.refresh();
            }
            else
            {
                usrReceiptPart1.refresh();
            }
        }

        private void totalCostChange()
        {
            //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartResult1._totalCost);

            if (ConvertUtil.ToInt32(_dtReceipt.Rows[0]["MS_TYPE"]) < 5)
            {
                long cost = 0;
                long adjustCost = 0;
                long totalCost = 0;
                usrReceiptPartResult1.getSummaryValue(ref cost, ref adjustCost, ref totalCost);

                _dtReceipt.Rows[0]["F_COST"] = seFCost.EditValue = cost;
                _dtReceipt.Rows[0]["F_ADJUST_COST"] = seFAdjustCost.EditValue = adjustCost;
                _dtReceipt.Rows[0]["F_TOTAL_COST"] = seFTotalCost.EditValue = totalCost + ConvertUtil.ToInt64(seFAddCost.EditValue);
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
                }
            }
        }

        private void seFCost_EditValueChanged(object sender, EventArgs e)
        {
            seFTotalCost.EditValue = $"{ConvertUtil.ToInt64(seFCost.EditValue) + ConvertUtil.ToInt64(seFAdjustCost.EditValue) + ConvertUtil.ToInt64(seFAddCost.EditValue)}";
            _dtReceipt.Rows[0]["F_COST"] = ConvertUtil.ToInt64(seFCost.EditValue);
            _dtReceipt.Rows[0]["F_TOTAL_COST"] = ConvertUtil.ToInt64(seFTotalCost.EditValue);
        }

        private void seFAdjustCost_EditValueChanged(object sender, EventArgs e)
        {
            //if (ConvertUtil.ToInt64(seFAdjustCost.EditValue) > 0)
            //{
            //    Dangol.Message("차감 금액은 음수만 입력 가능합니다.");
            //    seFAdjustCost.EditValue = 0;
            //}
            //else

            {
                seFTotalCost.EditValue = $"{ConvertUtil.ToInt64(seFCost.EditValue) + ConvertUtil.ToInt64(seFAdjustCost.EditValue) + ConvertUtil.ToInt64(seFAddCost.EditValue)}";
                _dtReceipt.Rows[0]["F_ADJUST_COST"] = ConvertUtil.ToInt64(seFAdjustCost.EditValue);
                _dtReceipt.Rows[0]["F_TOTAL_COST"] = ConvertUtil.ToInt64(seFTotalCost.EditValue);
            }
        }

        private void seFAddCost_EditValueChanged(object sender, EventArgs e)
        {
            seFTotalCost.EditValue = $"{ConvertUtil.ToInt64(seFCost.EditValue) + ConvertUtil.ToInt64(seFAdjustCost.EditValue) + ConvertUtil.ToInt64(seFAddCost.EditValue)}";
            _dtReceipt.Rows[0]["F_ADD_COST"] = ConvertUtil.ToInt64(seFAddCost.EditValue);
            _dtReceipt.Rows[0]["F_TOTAL_COST"] = ConvertUtil.ToInt64(seFTotalCost.EditValue);
        }

        private void meAdjustDes_DoubleClick(object sender, EventArgs e)
        {
            using (dlgEditor editor = new dlgEditor("차감사유", meAdjustDes.Text))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    meAdjustDes.Text = editor._text;
                }
            }
        }

        private void meProcessDes_DoubleClick(object sender, EventArgs e)
        {
            using (dlgEditor editor = new dlgEditor("처리내용", meProcessDes.Text))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    meProcessDes.Text = editor._text;
                }
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teReceipt.Text))
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            _receipt = teReceipt.Text.Trim();
            _receiptId = -1;
            setIInitData();
        }

        private void receiptRefresh()
        {
            string receipt = _receipt;
            gvUsedPurchaseList.FocusedRowObjectChanged -= gvUsedPurchaseList_FocusedRowObjectChanged;  
            JObject jResult = new JObject();
            getUsedPurchaseList(ref jResult);
            gvUsedPurchaseList.FocusedRowObjectChanged += gvUsedPurchaseList_FocusedRowObjectChanged;
            int rowHandle = gvUsedPurchaseList.LocateByValue("RECEIPT", receipt);

            if (rowHandle > -1)
            {
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gvUsedPurchaseList.FocusedRowHandle = -2147483646;
                    gvUsedPurchaseList.FocusedRowHandle = rowHandle;
                }
            }
            else
            {
                if (_dtUsedPurchase.Rows.Count > 0)
                {
                    gvUsedPurchaseList.FocusedRowHandle = -2147483646;
                    gvUsedPurchaseList.MoveFirst();
                }
            }

            ArrayList rows = new ArrayList();
            for (int i = 0; i < gvUsedPurchaseList.DataRowCount; i++)
            {
                rowHandle = gvUsedPurchaseList.GetVisibleRowHandle(i);
                rows.Add(gvUsedPurchaseList.GetDataRow(rowHandle));
            }

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                // Change the field value.
                row["NO"] = i + 1;
            }
        }

        private void sbSearch1_Click(object sender, EventArgs e)
        {
            initialize = true;
            Dangol.ShowSplash();
            receiptRefresh();
            Dangol.CloseSplash();
            initialize = false;
        }

        private bool getUsedPurchaseList(ref JObject jResult)
        {
            JObject jData = new JObject();

            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }
            //gvUsedPurchaseList.FocusedRowObjectChanged -= gvUsedPurchaseList_FocusedRowObjectChanged;
            gvUsedPurchaseList.BeginDataUpdate();
            _dtUsedPurchase.BeginInit();
            _dtUsedPurchase.Clear();

            if (DBConnect.getUsedPurchaseList(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DANAWA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DANAWA_DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtUsedPurchase.NewRow();

                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        dr["RECEIPT"] = obj["RECEIPT"];
                        dr["SOURCE_CD"] = "0";
                        dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                        dr["USER_TYPE"] = obj["USER_TYPE"];
                        dr["USER_TYPE2"] = obj["USER_TYPE2"];
                        dr["USER_ID"] = obj["USER_ID"];
                        dr["USER_NM"] = obj["USER_NM"];
                        dr["USER_TEL"] = obj["USER_TEL"];
                        dr["PURCHASE_COST"] = obj["PURCHASE_COST"];
                        dr["FAULT_COST"] = obj["FAULT_COST"];
                        dr["USER_COST"] = obj["USER_COST"];
                        dr["ADJUSTMENT_STATE"] = obj["ADJUSTMENT_STATE"];
                        dr["COMPLETE_DT"] = ConvertUtil.ToDateTimeNull(obj["COMPLETE_DT"]);
                        dr["USEDPURCHASE_STATE"] = obj["USEDPURCHASE_STATE"];
                        dr["RECEIPT_REPORT"] = "[접수증]";

                        dr["CHECK_STATE"] = ConvertUtil.ToString(obj["CHECK_STATE"]);
                        dr["RETURN_STATE"] = ConvertUtil.ToString(obj["RETURN_STATE"]);
                        dr["ETC"] = obj["ETC"];
                        dr["DT"] = obj["RECEIPT_DT"];

                        _dtUsedPurchase.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtUsedPurchase.NewRow();

                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        dr["RECEIPT"] = obj["RECEIPT"];
                        dr["SOURCE_CD"] = ConvertUtil.ToString(obj["SOURCE_CD"]);
                        dr["RECEIPT_DT"] = ConvertUtil.ToDateTimeNull(obj["RECEIPT_DT"]);
                        dr["USER_TYPE"] = ConvertUtil.ToInt32(obj["USER_TYPE"]);
                        dr["USER_TYPE2"] = ConvertUtil.ToInt32(obj["USER_TYPE2"]);
                        dr["USER_ID"] = ConvertUtil.ToString(obj["USER_ID"]);
                        dr["USER_NM"] = ConvertUtil.ToString(obj["USER_NM"]);
                        dr["USER_TEL"] = ConvertUtil.ToString(obj["USER_TEL"]);
                        dr["USER_HP"] = ConvertUtil.ToString(obj["USER_HP"]);
                        dr["PURCHASE_COST"] = ConvertUtil.ToInt64(obj["PURCHASE_COST"]);
                        dr["FAULT_COST"] = ConvertUtil.ToInt64(obj["FAULT_COST"]);
                        dr["USER_COST"] = ConvertUtil.ToInt64(obj["PURCHASE_COST"]) - ConvertUtil.ToInt64(obj["FAULT_COST"]);
                        dr["ADJUSTMENT_STATE"] = ConvertUtil.ToString(obj["ADJUSTMENT_STATE"]);
                        dr["COMPLETE_DT"] = ConvertUtil.ToDateTimeNull(obj["COMPLETE_DT"]);
                        dr["USEDPURCHASE_STATE"] = ConvertUtil.ToString(obj["USEDPURCHASE_STATE"]);
                        dr["RECEIPT_REPORT"] = "[접수증]";

                        dr["CHECK_STATE"] = ConvertUtil.ToString(obj["CHECK_STATE"]);
                        dr["RETURN_STATE"] = ConvertUtil.ToString(obj["RETURN_STATE"]);
                        dr["ETC"] = obj["ETC"];
                        dr["DT"] = obj["RECEIPT_DT"];

                        _dtUsedPurchase.Rows.Add(dr);
                    }
                }



                //gvUsedPurchaseList.FocusedRowObjectChanged += gvUsedPurchaseList_FocusedRowObjectChanged;

                //if(_dtUsedPurchase.Rows.Count > 0)
                //{
                //    //DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs

                //    //gvUsedPurchaseList_FocusedRowObjectChanged(null, gvUsedPurchaseList.view);
                //    gvUsedPurchaseList.FocusedRowHandle = -1;
                //    gvUsedPurchaseList.FocusedRowHandle = 0;
                //}

                _dtUsedPurchase.EndInit();
                gvUsedPurchaseList.EndDataUpdate();

                return true;

            }
            else
            {
                _dtUsedPurchase.EndInit();
                gvUsedPurchaseList.EndDataUpdate();
                //gvUsedPurchaseList.FocusedRowObjectChanged += gvUsedPurchaseList_FocusedRowObjectChanged;
                return false;
            }
        }

        private bool checkSearch(ref JObject jData)
        {

            string dtFrom = "";
            string dtTo = "";
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
                dtFrom = $"{deDtFrom.Text} 00:00:00";

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
                dtTo = $"{deDtTo.Text} 23:59:59";

            DateTime dtfrom;
            DateTime dtto;
            dtfrom = Convert.ToDateTime(dtFrom);
            dtto = Convert.ToDateTime(dtTo);

            int result = DateTime.Compare(dtfrom, dtto);

            if (result > 0)
            {
                jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다.");
                return false;
            }

            TimeSpan TS = dtto - dtfrom;
            int diffDay = TS.Days;

            if (diffDay > 90)
            {
                jData.Add("MSG", "검색 기간은 90일을 초과할 수 없습니다.");
                return false;
            }


            jData.Add("RECEIPT_DT_FROM", dtFrom);
            jData.Add("RECEIPT_DT_TO", dtTo);

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teReceiptSearch.Text)))
                jData.Add("RECEIPT", teReceiptSearch.Text);

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teCustomerNmSearch.Text)))
                jData.Add("USER_NM", teCustomerNmSearch.Text);

            if (!string.IsNullOrEmpty(ConvertUtil.ToString(teTelSearch.Text)))
                jData.Add("TEL", teTelSearch.Text);

            if (ConvertUtil.ToInt32(leReceiptState.EditValue) >= 0)
                jData.Add("RECEIPT_STATE", ConvertUtil.ToInt32(leReceiptState.EditValue));

            return true;
        }

        private void lcgReceiptList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(3))
            { 
                if (_currentReceipt != null)
                {
                    Dangol.ShowSplash();
                    long receiptId = ConvertUtil.ToInt64(_currentReceipt["RECEIPT_ID"]);

                    JObject jResult = new JObject();
                    getUsedPurchaseList(ref jResult);



                    int rowHandle = gvUsedPurchaseList.LocateByValue("RECEIPT_ID", receiptId);
                    if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        gvUsedPurchaseList.FocusedRowHandle = rowHandle;

                    ArrayList rows = new ArrayList();
                    for (int i = 0; i < gvUsedPurchaseList.DataRowCount; i++)
                    {
                        int rowHandle1 = gvUsedPurchaseList.GetVisibleRowHandle(i);
                        rows.Add(gvUsedPurchaseList.GetDataRow(rowHandle1));
                    }

                    for (int i = 0; i < rows.Count; i++)
                    {
                        DataRow row = rows[i] as DataRow;
                        // Change the field value.
                        row["NO"] = i + 1;
                    }

                    Dangol.CloseSplash();
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                ImageInfo.GetImage(2, false, _receipt);
            }
        }

        private void usrUsedPurchaseExamine_Enter(object sender, EventArgs e)
        {
            if (!initializeEnter)
            {
                initialize = true; 
                Dangol.ShowSplash();
                receiptRefresh();
                Dangol.CloseSplash();
                initialize = false;
            }

            initializeEnter = false;
        }
    }
}