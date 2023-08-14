using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.usedPurchase.receiptComponent;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrUsedPurchaseReceiptDetail : DevExpress.XtraEditors.XtraForm
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
        int _sendResult = 0;

        List<string> _listPartUpdate;
        List<string> _listCustomerUpdate;
        List<string> _listExamUpdate;
        List<string> _listCustomerCheckandComplete;
        List<string> _listCustomerCheckSate;

        List<string> _listFaultState;
        List<string> _listAddDelState;
        List<string> _listReleaseReturnState;
        List<string> _listCounsellingState;
        List<string> _listFinalPriceState;

        Dictionary<string, string> _dicDangolToEtcStateMapping;

        int _visibleLayout = 1;
        int _returnState;
        long _releaseReturnId;
        int _mstype;

        int _sourceCd;

        bool _isCounsellingExist = true;


        public usrUsedPurchaseReceiptDetail()
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
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RETURN_STATE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RES_DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("SEND_RESULT", typeof(int)));
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
            _dtCounselling.Columns.Add(new DataColumn("WAREHOUSING_ID", typeof(long)));
            _dtCounselling.Columns.Add(new DataColumn("RECEIPT_ID", typeof(long)));
            _dtCounselling.Columns.Add(new DataColumn("SOURCE_CD", typeof(int)));
            _dtCounselling.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtCounselling.Columns.Add(new DataColumn("REQUEST_DT", typeof(string)));
            _dtCounselling.Columns.Add(new DataColumn("REQUEST_DATE", typeof(string)));
            _dtCounselling.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));

            _dtResponse = new DataTable();
            _dtResponse.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtResponse.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dtResponse.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));

            _bsReceipt = new BindingSource();
            _bsStateList = new BindingSource();
            _bsCounselling = new BindingSource();
            _bsResponse = new BindingSource();

            _bsStateList.DataSource = _dtStateList;
            _bsCounselling.DataSource = _dtCounselling;
            _bsResponse.DataSource = _dtResponse;

            _listExamUpdate = new List<string>(new[] { "3", "5", "7", "C", "D", "E" });
            _listCustomerCheckandComplete = new List<string>(new[] { "5", "C", "D", "E" });         
            _listCustomerCheckSate = new List<string>(new[] {"C", "D", "E" });

            _listCustomerUpdate = new List<string>(new[] { "1", "3", "7", "C", "D", "E" });
            _listPartUpdate = new List<string>(new[] { "1", "6", "3", "7", "C", "D", "E" });
            _listFaultState = new List<string>(new[] { "3", "7", "C", "D", "E" });
            _listAddDelState = new List<string>(new[] { "1"});
            _listReleaseReturnState = new List<string>(new[] { "5", "C", "D", "E" });
            _listCounsellingState = new List<string>(new[] { "5", "C", "D", "E", "9" });
            _listFinalPriceState = new List<string>(new[] { "7", "C", "D", "E" });

            _dicDangolToEtcStateMapping = new Dictionary<string, string>();

            _isExistCustomerCheck = false;
            _returnState = 0;
            _mstype = 99;
        }

        public void setInitData(object receiptId, object receipt, object receiptState, object sourceCd)
        {
            _receiptId = ConvertUtil.ToInt64(receiptId);
            _receipt = ConvertUtil.ToString(receipt);
            _receiptState = ConvertUtil.ToString(receiptState);
            _sourceCd = ConvertUtil.ToInt32(sourceCd);

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

            meCounselling.DataBindings.Add(new Binding("Text", _bsCounselling, "DES", false, DataSourceUpdateMode.Never));
            meResponse.DataBindings.Add(new Binding("Text", _bsResponse, "DES", false, DataSourceUpdateMode.Never));

            DataTable dtReceiptType = Util.getCodeList("CD1310", "KEY", "VALUE");
            Util.LookupEditHelper(leReceiptType, dtReceiptType, "KEY", "VALUE");

            DataTable dtDangolToEtcStateMapping = Util.getCodeList("CD1311", "KEY", "VALUE");
            foreach(DataRow row in dtDangolToEtcStateMapping.Rows)
            {
                if (!_dicDangolToEtcStateMapping.ContainsKey(ConvertUtil.ToString(row["KEY"])))
                    _dicDangolToEtcStateMapping.Add(ConvertUtil.ToString(row["KEY"]), ConvertUtil.ToString(row["VALUE"]));
            }


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
            

            _warehousingId = -1;
        }

        public void reload(object receiptId, object receipt, object receiptState, object sourceCd)
        {
            _receiptId = ConvertUtil.ToInt64(receiptId);
            _receipt = ConvertUtil.ToString(receipt);
            _receiptState = ConvertUtil.ToString(receiptState);
            _sourceCd = ConvertUtil.ToInt32(sourceCd);

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

            setIInitData(true);
            setControl();

        }


        private void usrUsedPurchaseReceiptDetail_Load(object sender, EventArgs e)
        {
            usrReceiptPartResult1.totalCostChangeEvent += new usrReceiptPartResult.TotalCostChangeHandler(totalCostChange);

            setIInitData();

            gcStateList.DataSource = null;
            gcStateList.DataSource = _bsStateList;

            gcCounseling.DataSource = null;
            gcCounseling.DataSource = _bsCounselling;

            gcResponse.DataSource = null;
            gcResponse.DataSource = _bsResponse;
 
        }


        private void setIInitData(bool reload = false)
        {
            usrReceiptPart1.Clear();
            teReleaseReceipt.Text = "";
            _releaseReturnId = -1;

            if (getReceiptDetail())
            {
                if (_listExamUpdate.Contains(_receiptState))
                {
                    if (reload)
                        usrReceiptPartResult1.resetInfo(_receiptId, _receipt, _receiptState, _sourceCd);
                    else
                        usrReceiptPartResult1.setinitialize(_receiptId, _receipt, _receiptState, _sourceCd);
                    usrReceiptPartResult1.getComponentAll();

                    totalCostChange();

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

            setFinalCostEditable();
        }

        private bool getReceiptDetail()
        {
            _dtReceipt.Clear();
            _dtStateList.Clear();

            if (getReceiptData())
            {
                _receiptState = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"]);
                _sendResult = ConvertUtil.ToInt32(_dtReceipt.Rows[0]["SEND_RESULT"]);
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
                leReceiptType.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_TYPE"]);
                deRequestDt.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["REQUEST_DT"]);
                teAccountOwner.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_OWNER"]);
                teBankNm.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["BANK_NM"]);
                teAccount.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ACCOUNT_NO"]);
                meDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["DES"]);
                rgState.EditValue = _receiptState;
                //meProcessDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["PROCESS_DES"]);
                meAdjustDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["ADJUST_DES"]);

                //rgUserType3.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_TYPE3"]);
                meProcessDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["RES_DES"]);

                rgMsType.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["MS_TYPE"]);

                seFCost.EditValueChanged -= seFCost_EditValueChanged;
                seFAdjustCost.EditValueChanged -= seFAdjustCost_EditValueChanged;
                seFAddCost.EditValueChanged -= seFAddCost_EditValueChanged;

                seFCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_COST"]);
                seFAdjustCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADJUST_COST"]) * -1;
                seFAddCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADD_COST"]);
                seFTotalCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_TOTAL_COST"]);

                seFCost.EditValueChanged += seFCost_EditValueChanged;
                seFAdjustCost.EditValueChanged += seFAdjustCost_EditValueChanged;
                seFAddCost.EditValueChanged += seFAddCost_EditValueChanged;

                meFcostDes.Text = ConvertUtil.ToString(_dtReceipt.Rows[0]["F_COST_DES"]);
               
                setResultSendLayout();

                if(_sourceCd > 0 && _sendResult == 0 && _receiptState == "D")
                    checkExternalRequest();

                getRequest();

                if (_listFinalPriceState.Contains(_receiptState))
                    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    lcReturn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

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

        private void setResultSendLayout()
        {
            if (_listCustomerCheckandComplete.Contains(_receiptState) && _sendResult == 1)
            {
                lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                _isExistCustomerCheck = false;
                getCustomerState();
                //if (_isExistCustomerCheck)
                //    getRequest();
                //else
                //{
                //    //if (_sourceCd == 0)
                //    //{
                //        _returnState = 0;
                //        rgCustomerCheckFault.EditValue = _returnState;
                //    //}
                //    _dtCounselling.Clear();
                //}
            }
            else
            {
                lcCustomerCheckFault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //if (_sourceCd == 0)
                //{
                    _returnState = 0;
                    rgCustomerCheckFault.EditValue = _returnState;
                //}
                _isExistCustomerCheck = false;
                //_dtCounselling.Clear();
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
            }
            else
            {
                lcgPartList.CustomHeaderButtons[3].Properties.Enabled = false;
                lcgPartList.CustomHeaderButtons[4].Properties.Enabled = false;
            }

            if (_listReleaseReturnState.Contains(_receiptState)) //반송
                lcgPartList.CustomHeaderButtons[5].Properties.Enabled = true;
            else
                lcgPartList.CustomHeaderButtons[5].Properties.Enabled = false;

            if (_listPartUpdate.Contains(_receiptState) && _mstype < 1) //저장
                lcgPartList.CustomHeaderButtons[6].Properties.Enabled = true;
            else
                lcgPartList.CustomHeaderButtons[6].Properties.Enabled = false;

            if (_listCustomerUpdate.Contains(_receiptState) && _mstype < 1) //고객수정버튼
                lcgCustomerInfo.CustomHeaderButtons[2].Properties.Enabled = true;
            else
                lcgCustomerInfo.CustomHeaderButtons[2].Properties.Enabled = false;

            if (_isCounsellingExist)
            { //상담저장버튼
                lcgCounselling.CustomHeaderButtons[0].Properties.Enabled = true;
                lcgCounselling.CustomHeaderButtons[1].Properties.Enabled = true;
            }
            else
            {
                lcgCounselling.CustomHeaderButtons[0].Properties.Enabled = false;
                lcgCounselling.CustomHeaderButtons[1].Properties.Enabled = false;
            }

            if (_listFinalPriceState.Contains(_receiptState)) //최종비용수정버튼
            {
                lcgFinalCost.CustomHeaderButtons[0].Properties.Enabled = true;
                rgMsType.ReadOnly = false;
            }
            else
            {
                lcgFinalCost.CustomHeaderButtons[0].Properties.Enabled = false;
                rgMsType.ReadOnly = true;
            }
        }

        private void setFinalCostEditable()
        {
            if(_mstype == 0)
                seFAddCost.ReadOnly = false;
            else
                seFAddCost.ReadOnly = true;
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
            if (_currentCounsellingRow == null)
            {
                Dangol.Warining("문의 내용이 없습니다.");
            }
            else
            {
                if (e.Button.Properties.Tag.Equals(1))
                {
                    if (Dangol.MessageYN("응대내용을 저장하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();
                        JObject jobj = new JObject();

                        jobj.Add("REQUEST_ID", ConvertUtil.ToInt64(_currentCounsellingRow["REQUEST_ID"]));
                        jobj.Add("DES", meResponse.Text);
                        jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(_currentCounsellingRow["RECEIPT_ID"]));
                        jobj.Add("SOURCE_CD", ConvertUtil.ToInt32(_currentCounsellingRow["SOURCE_CD"]));
                        jobj.Add("USER_SEQ", ConvertUtil.ToInt64(_dtReceipt.Rows[0]["USER_SEQ"]));

                        if (DBUsedPurchase.InsertResponse(jobj, ref jResult))
                        {
                            DataRow dr = _dtResponse.NewRow();
                            dr["DES"] = meResponse.Text;
                            dr["USER_ID"] = ProjectInfo._userId;
                            dr["CREATE_DT"] = DateTime.Now.ToString("yyyy-MM-dd");
                            _dtResponse.Rows.InsertAt(dr, 0);

                            gvResponse.MoveFirst();

                            Dangol.Message("상담 정보가 수정되었습니다.");
                        }
                    }
                }
                else if (e.Button.Properties.Tag.Equals(2))
                {
                    Dangol.ShowSplash();
                    getRequest();
                    Dangol.CloseSplash();
                }
            }
        }

        private void getRequest()
        {

            if(_sourceCd == 0 && _warehousingId == -1)
            {

            }



            JObject jResult = new JObject();

            _dtCounselling.Clear();
            _isCounsellingExist = false;

            if (_sourceCd == 0 && _warehousingId == -1)
            {
                return;
            }

            if (DBUsedPurchase.getCounselling(_warehousingId, _receiptId, _sourceCd, ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtCounselling.NewRow();
                        dr["REQUEST_ID"] = obj["REQUEST_ID"];
                        dr["WAREHOUSING_ID"] = obj["WAREHOUSING_ID"];
                        dr["RECEIPT_ID"] = obj["RECEIPT_ID"];
                        dr["SOURCE_CD"] = obj["SOURCE_CD"];
                        dr["REQUEST_CAT"] = obj["REQUEST_CAT"];
                        dr["REQUEST_DT"] = obj["REQUEST_DT"];
                        dr["REQUEST_DATE"] = ConvertUtil.ToString(obj["REQUEST_DATE"]);
                        dr["DES"] = obj["DES"];
                        dr["CREATE_DT"] = obj["CREATE_DT"];
                        _dtCounselling.Rows.Add(dr);
                    }
                    _isCounsellingExist = true;
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
            if (e.Button.Properties.Tag.Equals(1)) //저장
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
            else if (e.Button.Properties.Tag.Equals(2)) //새로고침
            {
                Dangol.ShowSplash();
                setIInitData(true);
                setControl();
                Dangol.CloseSplash();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                ImageInfo.GetImage(2, false, $"{teReceipt.Text}");
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
                bool refreshPossible = false;
                if (_visibleLayout == 1)
                    refreshPossible = usrReceiptPartResult1.refreshCheck();
                else
                    refreshPossible = usrReceiptPart1.refreshCheck();

                if (!refreshPossible)
                {
                    if (Dangol.MessageYN("수정중인 데이터가 있습니다. 그래도 수정을 취소하고 진행상태를 수정하시겠습니까?") == DialogResult.Yes)
                    {
                        refreshGrid();
                    }
                    else
                        return;
                }

                Dangol.ShowSplash();

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

                        if (state == "5" || state == "9")
                        {
                            //string checkState = "4";
                            //if (state == "5")
                            //    checkState = "5";
                            //else
                            //    checkState = "5";

                            int returnState = ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue);
                            JObject jResult1 = new JObject();
                            DBUsedPurchase.updateCustomerState(_warehousingId, state, returnState, ref jResult1);
                        }

                        _receiptState = state;
                        _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;

                        reloadLayout(state);
                        setControl();
                        checkReceiptState();
                        getStateHistory();

                        setResultSendLayout(); //확인해 봐야함.

                        Dangol.CloseSplash();

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
                                Dangol.CloseSplash();
                                Dangol.Message("검수결과가 전송되었습니다.");
                            }
                            else
                            {
                                if (_warehousingId > 0 && ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue) != _returnState)
                                    updateCustomerState(state);
                                Dangol.CloseSplash();
                                Dangol.Message("접수 정보가 수정되었습니다.");
                            }
                        }
                        else
                        {
                            Dangol.CloseSplash();
                            Dangol.Message("수정 사항이 없습니다.");
                        }
                    }
                }
                else //옥션
                {
                    string state = ConvertUtil.ToString(rgState.EditValue);

                    if (!state.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["RECEIPT_STATE"])))
                    {
                        JObject jResult = new JObject();
                        JObject jData = new JObject();

                        jData.Add("RECEIPT_ID", ConvertUtil.ToInt64(_receiptId));
                        jData.Add("RECEIPT", ConvertUtil.ToString(_receipt));
                        if (_returnState == 0  || !state.Equals("5"))
                        {
                            if (_dicDangolToEtcStateMapping.ContainsKey(state))
                                jData.Add("RECEIPT_STATE_ETC", _dicDangolToEtcStateMapping[state]);
                        }
                        if(state.Equals("7"))
                            jData.Add("EXAMINE_DT", 1);
                        if (state.Equals("C") && rgMsType.EditValue.Equals("-1"))
                            jData.Add("M_STATE", 0);
                        jData.Add("RECEIPT_STATE", state);


                        if (DBUsedPurchase.updateReceiptState(jData, ref jResult))
                        {
                            if (_listCustomerCheckSate.Contains(state))
                            {
                                checkCustomerCheck(state);
                            }

                            if (state == "5" || state == "9")
                            {
                                string checkState = "4";
                                if (state == "5")
                                    checkState = "4";
                                else
                                    checkState = "5";

                                int returnState = ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue);
                                JObject jResult1 = new JObject();
                                DBUsedPurchase.updateCustomerState(_warehousingId, checkState, returnState, ref jResult1);
                            }

                            _receiptState = state;
                            _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;

                            reloadLayout(state);
                            setControl();
                            checkReceiptState();
                            getStateHistory();

                            setResultSendLayout(); //확인해 봐야함.

                            Dangol.CloseSplash();

                            if (_receiptState.Equals("C"))
                                Dangol.Message("검수결과가 전송되었습니다.");
                            else
                                Dangol.Message("접수 정보가 수정되었습니다.");
                        }
                        else
                        {
                            Dangol.CloseSplash();
                            Dangol.Message("오류가 발생했습니다. 관리자에게 문의하세요, ERROR CODE: UP201");
                        }
                    }
                    else
                    {
                        if (_listCustomerCheckSate.Contains(state))
                        {
                            if (state.Equals("C"))
                            {
                                sendExamResult(state);
                                Dangol.CloseSplash();
                                Dangol.Message("검수결과가 전송되었습니다.");
                            }
                            else
                            {
                                if (_warehousingId > 0 && ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue) != _returnState)
                                    updateCustomerState(state);
                                Dangol.CloseSplash();
                                Dangol.Message("접수 정보가 수정되었습니다.");
                            }
                        }
                        else
                        {
                            Dangol.CloseSplash();
                            Dangol.Message("수정 사항이 없습니다.");
                        }
                    }


                    //                    if (state.Equals("C"))
                    //                    {
                    //                        string userId = ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]);
                    //                        long price = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_TOTAL_COST"]);
                    //                        string content = $@"{_dtReceipt.Rows[0]["USER_NM"]}님께서 매입신청한 {usrReceiptPartResult1.getModelNm()}이
                    //검수 완료 되었습니다.
                    //접수번호 : {_dtReceipt.Rows[0]["RECEIPT"]}
                    //제품명 : {usrReceiptPartResult1.getModelNm()}
                    //확정 매입가 : {price.ToString("N0")}원
                    //해당 제품을 판매 또는 반환 하시려면
                    //아래 URL을 클릭하여 확인 진행 바랍니다.
                    //확인 https://auction.gochigo.kr/view/{userId.Substring(1, userId.Length-1)}";

                    //                        string hp = ConvertUtil.ToString(_dtReceipt.Rows[0]["HP"]);
                    //                        hp = hp.Replace("-", "");
                    //                        hp = hp.Substring(1, hp.Length - 1);
                    //                        JObject jResult = new JObject();
                    //                        JArray array = new JArray();
                    //                        JObject jObj = new JObject();

                    //                        jObj.Add("message_type", "at");
                    //                        jObj.Add("phn", $"82{hp}");
                    //                        jObj.Add("profile", "b0be757b8bbcafbba523d6fcc4ed890603de96f9");
                    //                        jObj.Add("msg", $"{content}");
                    //                        jObj.Add("tmplId", "veri_03");
                    //                        jObj.Add("smsKind", "L");
                    //                        jObj.Add("msgSms", $"{content}");
                    //                        jObj.Add("smsSender", "07082404495");

                    //                        array.Add(jObj);


                    //                        if (DBUsedPurchase.sendMsgToCustomer(array, ref jResult))
                    //                        {

                    //                        }
                    //                    }
                }
            }       
        }

        private void checkCustomerCheck(string state)
        {
            if (state.Equals("C"))
            {
                _sendResult = 1;
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


            int goodCnt = usrReceiptPartResult1.getCnt("INVENTORY_CAT = 'G'");
            int faultCnt = usrReceiptPartResult1.getCnt("INVENTORY_CAT = 'F'");
            int badCnt = usrReceiptPartResult1.getCnt("INVENTORY_CAT = 'B'");

            jData.Add("RECEIPT_ID", _receiptId);
            jData.Add("RECEIPT", _receipt);
            jData.Add("GOOD_CNT", goodCnt);
            jData.Add("FAULT_CNT", faultCnt);
            jData.Add("BAD_CNT", badCnt);
            jData.Add("SOURCE_CD", _sourceCd);
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
            jData.Add("ADD_PRICE", ConvertUtil.ToInt64(seFAddCost.EditValue));
            jData.Add("ADD_DES", ConvertUtil.ToString(meFcostDes.Text));
            jData.Add("REQUEST", ConvertUtil.ToString(meDes.Text));


            var jArray = usrReceiptPartResult1.makeExamComplete();

            jData.Add("DATA", jArray);

            if (DBUsedPurchase.updateReceiptExamComplete(jData, ref jResult))
            {
                object key = jResult["KEY"];
                //_warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);
                //StringBuilder postParams = new StringBuilder();
                //postParams.Append($"RECEIPT_STATE=C");
                //postParams.Append($"&TEL={teTel.Text}");
                //postParams.Append($"&HP={teHp.Text}");
                //postParams.Append($"&RECEIPT={_dtReceipt.Rows[0]["RECEIPT"]}");
                //postParams.Append($"&CUSTOMER_NM={teCustomerNm.Text}");
                //postParams.Append($"&KEY={key}");
                //postParams.Append($"&CUSTOMER_CHECK=1");

                //_isExistCustomerCheck = true;

                //if (DBUsedPurchase.updateDanawaReceiptState(postParams))
                //{
                //    //Dangol.Message("검수결과가 전송되었습니다.");
                //}
                //else
                //{
                //    Dangol.Message("오류가 발생했습니다. ERROR: UR2");
                //}

                
               
                JArray array = new JArray();
                JObject jResult2 = new JObject();
                JObject jObj = new JObject();

                string msgType = "E";
                string des = "";

                string url = $"{ProjectInfo._url}/layoutUsedPurchase.do?content=usedPurchase&KEY={key}";
                string profile = "b0be757b8bbcafbba523d6fcc4ed890603de96f9";
                string hp = ConvertUtil.ToString(_dtReceipt.Rows[0]["HP"]);
                string content = $@"안녕하세요. 중고 매입 서비스 입니다.
{_dtReceipt.Rows[0]["USER_NM"]} 님의 제품 검수가 완료 되었습니다.
검수 결과는 아래의 링크로 접속하여 확인이 가능 합니다.
{url}
감사합니다.";
                string hpn = hp.Replace("-", "");
                hpn = hpn.Substring(1, hpn.Length - 1);

                if (hpn.Length == 10)
                {
                    jObj.Add("message_type", "at");
                    jObj.Add("phn", $"82{hpn}");
                    jObj.Add("profile", profile);
                    jObj.Add("msg", $"{content}");
                    jObj.Add("tmplId", "call_03");
                    jObj.Add("smsKind", "L");
                    jObj.Add("msgSms", $"{content}");
                    jObj.Add("smsSender", "07082404495");

                    array.Add(jObj);


                    if (DBUsedPurchase.sendMsgToCustomer(array, ref jResult2))
                    {
                        string type = "M";
                        try
                        {
                            JObject jOb2 = new JObject();
                            jOb2 = (JObject)jResult2["data"];
                            type = ConvertUtil.ToString(jOb2["type"]);
                        }
                        catch (Exception ex) { }

                        if (type.Equals("AT"))
                            msgType = "K";
                        else
                            msgType = "M";
                    }
                    else
                        des = ConvertUtil.ToString(jResult2["message"]);
                }
                else
                {
                    des = "전화번호 오류";
                }

                jObj.RemoveAll();
                jObj.Add("MSG_TYPE", msgType);
                jObj.Add("USER_NM", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_NM"]));
                jObj.Add("HP", hp);
                jObj.Add("CALLER_HP", "070-8240-4495");
                jObj.Add("CODE", "call_03");
                jObj.Add("MSG", $"{content}");
                jObj.Add("DES", des);


                DBUsedPurchase.updateMsgHistory(jObj, ref jResult2);
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
                if (_sourceCd == 0) //다나와
                {
                    Dangol.ShowSplash();

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

                    if (DBUsedPurchase.updateDanawaRecetFinalState(jData, ref jResult))
                    {
                        _dtReceipt.Rows[0]["MS_TYPE"] = ConvertUtil.ToString(rgMsType.EditValue);
                        usrReceiptPartResult1._msType = _mstype = ConvertUtil.ToInt32(rgMsType.EditValue);

                        if (_mstype == 5)
                        {
                            _receiptState = "5";
                            rgState.EditValue = _receiptState;
                            _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;
                            getStateHistory();
                            int returnState = ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue);
                            JObject jResult1 = new JObject();
                            DBUsedPurchase.updateCustomerState(_warehousingId, _receiptState, returnState, ref jResult1);
                        }
                        else if (_mstype == 9)
                        {
                            _receiptState = "9";
                            rgState.EditValue = _receiptState;
                            _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;
                            setControl();
                            getStateHistory();

                            int returnState = ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue);
                            JObject jResult1 = new JObject();
                            DBUsedPurchase.updateCustomerState(_warehousingId, _receiptState, returnState, ref jResult1);
                            usrReceiptPart1.setinitialize(_receiptId, _receipt, _sourceCd);
                            usrReceiptPart1.refresh();
                            
                        }

                        setFinalCostEditable();
                        checkReceiptState();
                        Dangol.CloseSplash();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        return;
                    }
                }
                else //기타 매입
                {

                    _mstype = ConvertUtil.ToInt32(rgMsType.EditValue);

                    Dangol.ShowSplash();

                    JObject jResult = new JObject();
                    JObject jData = new JObject();

                    jData.Add("RECEIPT_ID", ConvertUtil.ToInt64(_receiptId));
                    jData.Add("RECEIPT", ConvertUtil.ToString(_receipt));

                    //jData.Add("M_USER_ID", ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_ID"]));
                    jData.Add("F_PRICE", ConvertUtil.ToInt64(seFCost.EditValue));
                    jData.Add("F_ADJUST_PRICE", ConvertUtil.ToInt64(seFAdjustCost.EditValue));
                    jData.Add("F_ADD_PRICE", ConvertUtil.ToInt64(seFAddCost.EditValue));
                    jData.Add("F_PRICE_DES", meFcostDes.Text);
                    jData.Add("M_STATE", ConvertUtil.ToString(rgMsType.EditValue));

                    if (_mstype == 5 || _mstype == 9)
                    {
                        if (_dicDangolToEtcStateMapping.ContainsKey(_mstype.ToString()))
                            jData.Add("RECEIPT_STATE_ETC", _dicDangolToEtcStateMapping[_mstype.ToString()]);
                        jData.Add("RECEIPT_STATE", _mstype.ToString());
                    }

                    if (DBUsedPurchase.updateRecetFinalState(jData, ref jResult))
                    {
                        _dtReceipt.Rows[0]["MS_TYPE"] = ConvertUtil.ToString(rgMsType.EditValue);
                        usrReceiptPartResult1._msType = _mstype;

                        if (_mstype == 5)
                        {
                            _receiptState = "5";
                            rgState.EditValue = _receiptState;
                            _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;
                            getStateHistory();

                            int returnState = ConvertUtil.ToInt32(rgCustomerCheckFault.EditValue);
                            JObject jResult1 = new JObject();
                            DBUsedPurchase.updateCustomerState(_warehousingId, _receiptState, returnState, ref jResult1);
                        }
                        else if (_mstype == 9)
                        {
                            _receiptState = "9";
                            rgState.EditValue = _receiptState;
                            _dtReceipt.Rows[0]["RECEIPT_STATE"] = _receiptState;

                            setControl();
                            getStateHistory();
                            usrReceiptPart1.setinitialize(_receiptId, _receipt, _sourceCd);
                            usrReceiptPart1.refresh();
                        }

                        setFinalCostEditable();
                        checkReceiptState();
                        Dangol.CloseSplash();
                        Dangol.Message("처리되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        return;
                    }
                }
            }
        }


        private bool getReceiptData()
        {
            if(string.IsNullOrWhiteSpace(_receipt))
            {
                Dangol.Message("접수번호를 가져올수 없습니다. 다시 시도해 주세요.");
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
                    dr["F_TOTAL_COST"] = ConvertUtil.ToInt64(jData["F_COST"]) + ConvertUtil.ToInt64(jData["F_ADJUST_COST"]) + ConvertUtil.ToInt64(jData["F_ADD_COST"]);
                    dr["F_COST_DES"] = jData["F_COST_DES"];
                    dr["SEND_RESULT"] = jResult["SEND_RESULT"];
                    dr["REQUEST_DT"] = jData["REQUEST_DT"];
                    
                    if (_sourceCd != 0)
                    {
                        int receiptType = ConvertUtil.ToInt32(jData["RECEIPT_TYPE"]);
                        int serviceType = ConvertUtil.ToInt32(jData["SERVICE_TYPE"]);

                        if (receiptType > 0)
                            dr["RECEIPT_TYPE"] = ConvertUtil.ToString(receiptType);
                        else if (serviceType > 0)
                            dr["RECEIPT_TYPE"] = ConvertUtil.ToString(serviceType * -1);

                        //dr["RETURN_STATE"] = jData["RETURN_STATE"];
                        //_returnState = ConvertUtil.ToInt32(jData["RETURN_STATE"]);
                        //rgCustomerCheckFault.EditValue = _returnState;
                    }
                    else
                        dr["RECEIPT_TYPE"] = jData["RECEIPT_TYPE"];

                    _dtReceipt.Rows.Add(dr);
                }
                if (Convert.ToBoolean(jResult["LIST_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtStateList.NewRow();
                        dr["RECEIPT_STATE"] = obj["RECEIPT_STATE"];
                        dr["USER_NM"] = ConvertUtil.ToString(obj["USER_NM"]);
                        dr["DEPT_NM"] = ConvertUtil.ToString(obj["DEPT_NM"]);
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
            else if (e.Button.Properties.Tag.Equals(4)) //반송
            {
                if(_releaseReturnId > 0)
                {
                    Dangol.Message("이미 접수된 반송이 있습니다.");
                    return;
                }

                if (!usrReceiptPartResult1.CheckCheck())
                {
                    Dangol.Message("체크된 부품이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("체크한 부품을 반송하시겠습니까?") == DialogResult.Yes)
                {
                    string releaseReceipt = "";

                    usrReceiptPartResult1.returnRelease(_warehousingId, ref _releaseReturnId, ref releaseReceipt);
                    {
                        teReleaseReceipt.Text = releaseReceipt;
                    }
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

            if (ConvertUtil.ToInt32(_dtReceipt.Rows[0]["MS_TYPE"]) < 1)
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

        private void checkExternalRequest()
        {
            JObject jResult = new JObject();

            if (DBUsedPurchase.checkExternalRequest(_receiptId, ref jResult))
            {
                
            }
        }
    }
}