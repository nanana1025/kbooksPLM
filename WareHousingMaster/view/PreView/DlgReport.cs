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
using Newtonsoft.Json.Linq;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using System.Drawing.Printing;
using System.Management;

namespace WareHousingMaster.view.PreView
{
    public partial class DlgReport : DevExpress.XtraEditors.XtraForm
    {
        private XtraReport _reports;
        private bool isFileCreate;
      
        /// <summary>
        /// 리포트 Form 생성자
        /// </summary>
        /// 

        public String FileName { get; private set; }
        public string _reportName { get; private set; }
        public DataRow _drRatio { get; private set; }

        public DlgReport()
        {
            InitializeComponent();
            isFileCreate = false;
        }
        /// <summary>
        /// 미리보기를 위해 생성하는 리포트
        /// </summary>
        /// <param name="reportName">해당 리포트 이름</param>
        /// <param name="reports">표출할 리포트</param>
        public DlgReport(string reportName, XtraReport reports) : this()
        {
            _reportName = reportName;
            _reports = reports;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraReport report = new XtraReport();

            try
            {
                report.CreateDocument(false);

                _reports.CreateDocument();
                report.Pages.AddRange(_reports.Pages);
                //report.Landscape = true;
                using (ReportPrintTool tool = new ReportPrintTool(report))
                {
                    tool.PrinterSettings.PrinterName = ConvertUtil.ToString(barEditItem1.EditValue);

                    tool.PrinterSettings.Copies = Convert.ToInt16(barEditItem3.EditValue);

                    //tool.PrinterSettings.Copies = Convert.ToInt16(5);
                    
                    //tool.PrintDialog();
                    tool.Print();
                }
            }
            catch(Exception ex)
            {
                
            }
   
        }

        private void rirgPageSize_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            documentViewer1.DocumentSource = _reports;
        }

        private void DlgReport_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ProjectInfo.ProjectIcon;
                barEditItem3.EditValue = 1;

                _reports.CreateDocument();
                 
                documentViewer1.DocumentSource = _reports;
                   
                if (!string.IsNullOrEmpty(_reportName))
                {
                    documentViewer1.PrintingSystem.Document.Name = _reportName;
                }

                List<string> printerNameList = new List<string>();
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    printerNameList.Add(printer);
                }

                var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
                foreach (var printer in printerQuery.Get())
                {
                    var name = printer.GetPropertyValue("Name");
                    var status = printer.GetPropertyValue("Status");
                    var isDefault = printer.GetPropertyValue("Default");
                    var isNetworkPrinter = printer.GetPropertyValue("Network");

                    Console.WriteLine("{0} (Status: {1}, Default: {2}, Network: {3}",
                                name, status, isDefault, isNetworkPrinter);
                }



                rilePrintName.DataSource = printerNameList;
                barEditItem1.EditValue = printerNameList[0];

            }
            finally
            {
                //SplashScreenManager.CloseForm();
            }
        }

        private void closeBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (isFileCreate)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void DlgReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFileCreate)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
        }
    }
}