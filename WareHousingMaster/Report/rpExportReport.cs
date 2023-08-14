using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.common;
using System.Data;

namespace WareHousingMaster.Report
{
    public partial class rpExportReport : DevExpress.XtraReports.UI.XtraReport
    {
        DataRowView _row;
        long _totalReleasePrice;

        public rpExportReport(DataRowView row, long totalReleasePrice)
        {
            InitializeComponent();

            _row = row;
            _totalReleasePrice = totalReleasePrice;

        }
        public void DataBinding()
        {
          
            xrExportDt.Text = $"{_row["RECEIPT_DT"]}";
            //xrCompanyNo.Text = $"{_row[""]}";
            xrCompanyTel.Text = $"{_row["TEL"]}";
            xrCompanyNm.Text = $"{_row["CUSTOMER_NM"]}";
            //xrCompanyOwner.Text = $"{_row[""]}";
            //xrComapnyType.Text = $"{_row[""]}";
            //xrCompanyCat.Text = $"{_row[""]}";
            xrCompanyAddress.Text = $"{_row["ADDRESS"]}";

            double releasePrice = (double)_totalReleasePrice / (double)1.1;
            xrReleasePrice.Text = string.Format("{0:#,0}", releasePrice);

            double tax = _totalReleasePrice / 11;
            xrTax.Text = string.Format("{0:#,0}", tax);

            xrTotalReleasePrice.Text = string.Format("{0:#,0}", _totalReleasePrice);

        }
    }
}
