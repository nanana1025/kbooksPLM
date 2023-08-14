using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.common;

namespace WareHousingMaster.Report
{
    public partial class rpUsedPurchaseCertificate : DevExpress.XtraReports.UI.XtraReport
    {
        private JObject _jData;
        string _userType1;
        string _userType2;

        public rpUsedPurchaseCertificate(JObject jData, string userType1, string userType2 )
        {
            InitializeComponent();

            _jData = jData;
            _userType1 = userType1;
            _userType2 = userType2;

        }
        public void DataBinding()
        {
            xrUserName.Text = $"{_jData["USER_NM"]}({_userType2})";
            xrUserNo.Text = $"{_jData["USER_ID"]}";
            xrUserTel.Text = $"{_jData["USER_TEL"]}";
            //xrUserType2.Text = $"{_userType1}";
            xrUserHp.Text = $"{_jData["USER_HP"]}";
            xrUserZipcode.Text = $"{_jData["USER_ZIPCODE"]}";
            xrUserAddress.Text = $"{_jData["USER_ADDRESS1"]} {_jData["USER_ADDRESS2"]}";

            xrReceipt.Text = $"{_jData["RECEIPT"]}";
            xrReceiptDt.Text = $"{ConvertUtil.ToDateTimeNull(_jData["RECEIPT_DT"])}";
            xrRequest.Text = $"{_jData["REQUEST"]}";
            //xrBankOwner.Text = $"{_jData["BANK_OWNER"]}";
            //xrBankNm.Text = $"{_jData["BANK_NM"]}";
            //xrBankNo.Text = $"{_jData["BANK_NO"]}";

            //xrPurchaseCost.Text = $"{_jData["PURCHASE_COST"]}";
            //xrEtcCost.Text = $"{_jData["ETC_COST"]}";
            //xrChangeCost.Text = $"{_jData["CHANGE_COST"]}";
            //xrFaultCost.Text = $"{_jData["FAULT_COST"]}";

            xrPurchaseCost.Text = String.Format("{0:c0}", _jData["PURCHASE_COST"]);
            xrEtcCost.Text = String.Format("{0:c0}", _jData["ETC_COST"]);
            xrFaultCost.Text = String.Format("{0:c0}", _jData["FAULT_COST"]);
            xrChangeCost.Text = String.Format("{0:c0}", _jData["CHANGE_COST"]);

            long cost = ConvertUtil.ToInt64(_jData["PURCHASE_COST"]) - ConvertUtil.ToInt64(_jData["FAULT_COST"]);

            //xrOCost.Text = String.Format("{0:c0}", cost);
            //xrCCost.Text = String.Format("{0:c0}", cost);
            //xrUCost.Text = String.Format("{0:c0}", cost);
            xrReason.Text = $"{_jData["REASON"]}";

        }
    }
}
