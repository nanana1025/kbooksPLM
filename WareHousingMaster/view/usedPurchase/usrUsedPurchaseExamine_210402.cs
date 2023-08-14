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
    public partial class usrUsedPurchaseExamine_210402 : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtReceipt;
        DataTable _dtStateList;
        DataTable _dtFaultList;

        BindingSource _bsReceipt;
        BindingSource _bsStateList;
        BindingSource _bsFaultList;

        DataRowView _currentFaultRow;

        string _receipt;
        long _receiptId;
        long _warehousingId;
        string _receiptState = "-1";
        List<string> _listPartUpdate;
        List<string> _listCustomerUpdate;
        List<string> _listExamUpdate;


        public usrUsedPurchaseExamine_210402()
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

            _dtFaultList = new DataTable();
            _dtFaultList.Columns.Add(new DataColumn("FAULT_ID", typeof(long)));
            _dtFaultList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtFaultList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtFaultList.Columns.Add(new DataColumn("CNT", typeof(int)));

            _bsReceipt = new BindingSource();
            _bsStateList = new BindingSource();
            _bsFaultList = new BindingSource();

            _bsStateList.DataSource = _dtStateList;
            _bsFaultList.DataSource = _dtFaultList;


            _listPartUpdate = new List<string>(new[] { "1", "2", "3", "6", "B"});
            _listCustomerUpdate = new List<string>(new[] { "1", "2", "3", "6", "B" });
            _listExamUpdate = new List<string>(new[] { "3", "5", "7" });


        }

        public void setInitData()
        {

            DataTable dtUsedPurchaseState = Util.getCodeList("CD1303", "KEY", "VALUE");
            Util.LookupEditHelper(rileReceiptState, dtUsedPurchaseState, "KEY", "VALUE");
            RadioGroupItem[] rgState = new RadioGroupItem[dtUsedPurchaseState.Rows.Count];

            int index = 0;
            foreach (DataRow dr in dtUsedPurchaseState.Rows)
            {
                RadioGroupItem rgItem = new RadioGroupItem(dr["KEY"], ConvertUtil.ToString(dr["VALUE"]), true, dr["KEY"]);
                rgState[index++] = rgItem;
            }
            this.rgState.Properties.Items.AddRange(rgState);

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

            Util.LookupEditHelper(leComponentCd, dtComponentCd, "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");
            leComponentCd.ItemIndex = 0;

        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(teReceipt.Text))
            {
                Dangol.Message("접수번호를 입력해주세요.");
                return;
            }

            _receipt = teReceipt.Text.Trim();

            setIInitData();
            
        }

        public void reload(object receiptId, object receipt)
        {
            _receiptId = ConvertUtil.ToInt64(receiptId);
            _receipt = ConvertUtil.ToString(receipt);
            setIInitData(true);
            
        }


        private void usrUsedPurchaseExamine_Load(object sender, EventArgs e)
        {
            usrReceiptPartTree1.totalCostChangeEvent += new usrReceiptPartTree.TotalCostChangeHandler(totalCostChange);
            gcStateList.DataSource = null;
            gcStateList.DataSource = _bsStateList;

            gcFault.DataSource = null;
            gcFault.DataSource = _bsFaultList;
         

            setInitData();

            teCustomerNm.ReadOnly = true;
            teTel.ReadOnly = true;
            teHp.ReadOnly = true;
            tePostalCd.ReadOnly = true;
            teAddress.ReadOnly = true;
            teAddressDetail.ReadOnly = true;
            rgUserType1.ReadOnly = true;
            teAccountOwner.ReadOnly = true;
            teBankNm.ReadOnly = true;
            teAccount.ReadOnly = true;
            meDes.ReadOnly = true;
            //meAdjustDes.ReadOnly = true;
            //meProcessDes.ReadOnly = true;
            sbSave.Enabled = false;
        }


        private void setIInitData(bool reload = false)
        {
            _dtFaultList.Clear();

            if (getReceiptDetail())
            {
                if (reload)
                    usrReceiptPartTree1.resetInfo(_receiptId, _receipt, _receiptState);
                else
                    usrReceiptPartTree1.setinitialize(_receiptId, _receipt, _receiptState);
                usrReceiptPartTree1.getComponentAll();

                getFaultList();
            }
        }

        private bool getReceiptDetail()
        {
            if (getReceiptData())
            {
                sbSave.Enabled = true;

                if (_dtReceipt.Rows.Count > 0)
                {
                    _receiptId = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["RECEIPT_ID"]);
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

                    //rgMsType.EditValue = ConvertUtil.ToString(_dtReceipt.Rows[0]["MS_TYPE"]);
                    //seFCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_COST"]);
                    //seFAdjustCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_ADJUST_COST"]) * -1;
                    //seFTotalCost.EditValue = ConvertUtil.ToInt64(_dtReceipt.Rows[0]["F_TOTAL_COST"]);
                }

                

                return true;
            }
            else
            {
                return false;
            }
        }

        private void totalCostChange()
        {
            //seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartTree1._totalCost);
        }

        private void checkReceiptState()
        {
           
            {
                teCustomerNm.ReadOnly = true;
                teTel.ReadOnly = true;
                teHp.ReadOnly = true;
                tePostalCd.ReadOnly = true;
                teAddress.ReadOnly = true;
                teAddressDetail.ReadOnly = true;
                rgUserType1.ReadOnly = true;
                teAccountOwner.ReadOnly = true;
                teBankNm.ReadOnly = true;
                teAccount.ReadOnly = true;
                meDes.ReadOnly = true;
                meAdjustDes.ReadOnly = true;
                meProcessDes.ReadOnly = true;
                sbSearch.Enabled = false;
            }



            if (_listExamUpdate.Contains(_receiptState))
                lcgPartList.CustomHeaderButtons[7].Properties.Enabled = true;
            else
                lcgPartList.CustomHeaderButtons[7].Properties.Enabled = false;



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
            if(!teCustomerNm.Text.Equals(ConvertUtil.ToString(_dtReceipt.Rows[0]["USER_NM"])))
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
            if(string.IsNullOrWhiteSpace(_receipt))
            {
                Dangol.Message("접수번호를 가져올수 없습니다. 다시 시도해 주세요.");
                return false;
            }

            JObject jResult = new JObject();

            if (DBUsedPurchase.getReceiptDetail(_receiptId, _receipt,  ref jResult))
            {
                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    _dtReceipt.Clear();
                    JObject jData = new JObject();
                    jData = (JObject)jResult["DATA"];

                
                    DataRow dr = _dtReceipt.NewRow();
                    dr["RECEIPT_ID"] = jData["RECEIPT_ID"];
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
                return false;
            }

            return true;
        }

        private void layoutControlGroup5_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(5)) //전체보기
            {
                usrReceiptPartTree1.expandAll();
            }
            else if (e.Button.Properties.Tag.Equals(6)) //간략히
            {
                usrReceiptPartTree1.foldAll();
            }
            else if (e.Button.Properties.Tag.Equals(7)) //가격 가져오기
            {
                long pPartId = ConvertUtil.ToInt64(usrReceiptPartTree1._FocusedNode["P_PART_ID"]);

                if(pPartId == -1)
                {
                    Dangol.Message("검수 부품을 선택해 주세요.");
                    return;
                }

                string componentCd = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["PART_LT_COMPONENT_CD"]);

                if(componentCd.Equals("데스크탑") || componentCd.Equals("노트북"))
                    componentCd = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["PART_COMPONENT_CD"]);

                string LTcomponentCd = "CPU";

                string filterString = "-1";

                if (componentCd.Equals("CPU"))
                {
                    string modelNm = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["EXAMINE_MODEL_NM"]);
                    string[] sMdodelNm = modelNm.Split(' ');
                    filterString = sMdodelNm[sMdodelNm.Length - 1];
                }
                else if(componentCd.Equals("MEM")){
                    string modelNm = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["EXAMINE_MODEL_NM"]);
                    string[] sMdodelNm = modelNm.Split('/');

                    string type;
                    string capa;

                    if (sMdodelNm[2].Contains("DDR3"))
                        type = "DDR3";
                    else
                        type = "DDR4";

                    capa = ConvertUtil.ToString((ConvertUtil.ToInt32(sMdodelNm[3].Replace("MBytes", ""))/ 1024));
                    filterString = capa;

                    LTcomponentCd = ProjectInfo._dicLTComponentCdRevers[componentCd];
                }
                else if (componentCd.Equals("STG"))
                {
                    string stgType = ConvertUtil.ToString(usrReceiptPartTree1._FocusedNode["STG_TYPE"]);
                    if (stgType.ToUpper().Contains("SSD"))
                        LTcomponentCd = "SSD";
                    else
                        LTcomponentCd = "HDD";
                }
                else if (componentCd.Equals("MON"))
                    LTcomponentCd = "MON";
                else
                    LTcomponentCd = ProjectInfo._dicLTComponentCdRevers[componentCd];



                using (dlgGetPart getPart = new dlgGetPart(LTcomponentCd, filterString))
                {
                    if (getPart.ShowDialog(this) == DialogResult.OK)
                    {
                        usrReceiptPartTree1._FocusedNode["PURCHASE_COST"] = getPart._price;
                    }
                }
            }

            //else if (e.Button.Properties.Tag.Equals(3)) //삭제
            //{
            //    if(usrReceiptPartTree1._currentRow == null)
            //    {
            //        Dangol.Message("선택된 품목이 없습니다.");
            //        return;
            //    }
            //    if(Dangol.MessageYN("선택하신 품목을 삭제하시겠습니까?") == DialogResult.Yes)
            //    {
            //        usrReceiptPartTree1.deletePart();
            //        seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartTree1._totalCost);
            //    }
            //}
            //else if (e.Button.Properties.Tag.Equals(4)) //수량변경
            //{
            //    using (dlgUpdatePartCnt updatePartCnt = new dlgUpdatePartCnt(usrReceiptPartTree1._currentRow["PART_ID"], _receipt, usrReceiptPartTree1._currentRow["PART_CNT"]))
            //    {
            //        if (updatePartCnt.ShowDialog(this) == DialogResult.OK)
            //        {
            //            usrReceiptPartTree1.updateCnt(updatePartCnt.Cnt);
            //            seFCost.EditValue = ConvertUtil.ToInt64(usrReceiptPartTree1._totalCost);
            //        }
            //    }

            //}
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
            if (ConvertUtil.ToString(rgState.EditValue).Equals("-1"))
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
                jData.Add("M_STATE", ConvertUtil.ToString(rgState.EditValue));
                jData.Add("BIGO", "");
                

                if (DBUsedPurchase.updateRecetFinalState(jData, ref jResult))
                {
                    if (!ConvertUtil.ToString(rgState.EditValue).Equals("1"))
                        getReceiptDetail();

                    Dangol.Message("접수 정보가 수정되었습니다.");

                }
                else
                {
                    return;
                }
            }
        }

        private void layoutControlGroup5_CustomButtonClick_1(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                if (Dangol.MessageYN("선택하신 정보를 불량내역에 추가하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                if (_warehousingId < 0)
                {
                    Dangol.Message("접수번호를 가져올수 없습니다. 다시 시도해 주세요.");
                    return;
                }

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("WAREHOUSING_ID", _warehousingId);
                jobj.Add("COMPONENT_CD", ConvertUtil.ToString(leComponentCd.EditValue));
                jobj.Add("MODEL_NM", ConvertUtil.ToString(teModelNm.Text));
                jobj.Add("CNT", ConvertUtil.ToInt32(seCnt.EditValue));

                if (DBUsedPurchase.insertFaultList(jobj, ref jResult))
                {
                    gcFault.BeginUpdate();
                    DataRow dr = _dtFaultList.NewRow();
                    dr["FAULT_ID"] = jResult["FAULT_ID"];
                    dr["COMPONENT_CD"] = jobj["COMPONENT_CD"];
                    dr["MODEL_NM"] = jobj["MODEL_NM"];
                    dr["CNT"] = jobj["CNT"];
                    _dtFaultList.Rows.Add(dr);
                    gcFault.EndUpdate();

                    Dangol.Message("추가되었습니다.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else if(e.Button.Properties.Tag.Equals(2))
            {
                if (Dangol.MessageYN("선택하신 불량내역 정보를 삭제하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                if (_warehousingId < 0)
                {
                    Dangol.Message("접수번호를 가져올수 없습니다. 다시 시도해 주세요.");
                    return;
                }

                JObject jResult = new JObject();
                JObject jobj = new JObject();

                jobj.Add("WAREHOUSING_ID", _warehousingId);
                jobj.Add("FAULT_ID", ConvertUtil.ToInt64(_currentFaultRow["FAULT_ID"]));

                if (DBUsedPurchase.deleteFaultList(jobj, ref jResult))
                {
                    gcFault.BeginUpdate();
                    _currentFaultRow.Delete();
                    gcFault.EndUpdate();

                    Dangol.Message("삭제되었습니다.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
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

        private void gvFault_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvFault.RowCount > 0);

            if (isValidRow)
            {
                _currentFaultRow = e.Row as DataRowView;
            }
            else
                _currentFaultRow = null;
        }
    }
}