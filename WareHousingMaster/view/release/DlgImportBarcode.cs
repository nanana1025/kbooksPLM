
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using ImportExcel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using System.Text.RegularExpressions;


namespace WareHousingMaster.view.release
{
    public partial class DlgImportBarcode : DevExpress.XtraEditors.XtraForm
    {
        enum EnumInfoCode { Info = 0, Pass = 1, Warning = 2, Failure = 3 }

        public int _errCount = 0;

        public DataTable _dtExcel { get; private set; }

        public JObject _jResult { get; private set; }


        public long _id { get; private set; }
        public string fileNm { get; private set; }
        public string filePath { get; private set; }

        public bool _isSuccess { get; private set; }

        public DlgImportBarcode()
        {
            InitializeComponent();

            //_dtReceipt = dtReceipt;
            //_dtReceiptPart = dtReceiptPart;
            
            _isSuccess = false;
            fileNm = null;
        }

        private void DlgWizardImportData_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
        }

        #region WelcomePage

        private void sbSearch_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "Excel 통합문서|*.xlsx;*.xls";

                if (file.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string data in file.FileNames)
                    {
                        string[] fileNms = data.Split('\\');
                        filePath = data;
                        fileNm = fileNms[fileNms.Length - 1];
                    }
                    teFileName.Text = filePath;
                }

            }
        }

        private void sbTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog file = new SaveFileDialog())
            {
                file.Filter = "Excel 통합문서|*.xlsx";
                file.FileName = "BOM 작성 양식";

                if (file.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    DataTable dt = new DataTable();

                    dt.TableName = "";
                    dt.BeginInit();
                }
            }
        }

        #endregion

        //region WizardPage1



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"데이터를 불러오는 중입니다.", (int)EnumInfoCode.Info })));
            backgroundWorker1.ReportProgress(0);
            backgroundWorker1.ReportProgress(5);
            Standard(sender, e);
           
        }


        private void Standard(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0);

            // 파일 열기 검사
            if (!File.Exists(filePath))
            {
                tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"파일이 존재하지 않습니다. 파일명: {fileNm}", (int)EnumInfoCode.Failure })));
                e.Cancel = true;
                return;
            }

            backgroundWorker1.ReportProgress(10);

            _dtExcel = ExcelUtil.getDataTableFromExcel(fileNm, filePath);

            if (_dtExcel == null)
            {
                tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"가져오기를 실패하였습니다.", (int)EnumInfoCode.Failure })));
                backgroundWorker1.ReportProgress(100);
                return;
            }

            backgroundWorker1.ReportProgress(40);
            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"불러온 데이터를 기반으로 출고정보를 구성합니다.", (int)EnumInfoCode.Info })));
            backgroundWorker1.ReportProgress(50);

            //receiptChangeEvent(_dtExcel);
            if (!AddReceiptByExcel1(_dtExcel))
            {
                tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"출고정보구성을 실패하였습니다.", (int)EnumInfoCode.Failure })));
                backgroundWorker1.ReportProgress(100);
                return;
            }
            backgroundWorker1.ReportProgress(80);
            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"출고정보를 구성을 완료했습니다.", (int)EnumInfoCode.Info })));
            backgroundWorker1.ReportProgress(100);

            _isSuccess = true;
        }


        private bool AddReceiptByExcel1(DataTable dt)
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            var jArray = new JArray();

            List<string> listBarcode = new List<string>();
            string barcode;
            string oldCoa;
            string newCoa;
            string serialNo;

            string price;
            string releasePrice;
            string produceCost;
            foreach (DataRow row in dt.Rows)
            {
                barcode = ConvertUtil.ToString(row[0]);
                if (!string.IsNullOrWhiteSpace(barcode) && !listBarcode.Contains(barcode))
                {
                    oldCoa = "";
                    newCoa = "";
                    serialNo = "";

                    JObject jData = new JObject();

                    jData.Add("BARCODE", barcode);

                    if (dt.Columns.Count > 1)
                    {
                        oldCoa = $"{row[1]}";
                        jData.Add("OLD_COA", oldCoa);
                    }
                    if (dt.Columns.Count > 2)
                    {
                        newCoa = $"{row[2]}";
                        jData.Add("NEW_COA", newCoa);
                    }
                    if (dt.Columns.Count > 3)
                    {
                        serialNo = $"{row[3]}";
                        jData.Add("SERIAL_NO", serialNo);
                    }

                    if(!string.IsNullOrWhiteSpace(oldCoa) || !string.IsNullOrWhiteSpace(newCoa) || !string.IsNullOrWhiteSpace(serialNo))
                        jArray.Add(jData);

                    listBarcode.Add(barcode);
                }
            }

            jobj.Add("DATA", jArray);
            jobj.Add("LIST_BARCODE", string.Join(",", listBarcode));
            jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

            if (DBRelease.getPartInfo(jobj, ref jResult))
            {
                tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"부품정보 확인 완료. ", (int)EnumInfoCode.Info })));
                _jResult = jResult;
                return true;
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"{jResult["MSG"]} ", (int)EnumInfoCode.Failure })));
                return false;
            }
        }

        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (e.Page == welcomeWizardPage1)
            {
                if (teFileName.Text.Length <= 0)
                {
                    Dangol.Message("선택한 파일이 없습니다.");
                    e.Handled = true;
                    return;
                }

                wizardPage1.AllowNext = false;
                wizardPage1.AllowCancel = false;
                wizardPage1.AllowBack = false;
                tlResult.Nodes.Clear();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarControl1.Position = e.ProgressPercentage;

            if (e.UserState is List<string>)
            {
                List<string> errMsg = e.UserState as List<string>;

                if (errMsg.Count == 0)
                    return;

                string msg = errMsg.Last();
                errMsg.RemoveAt(0);
                int count = tlResult.AllNodesCount + 1;
                bool state = false;
                if (msg.Contains("완료"))
                    state = true;

                tlResult.Nodes.Add(new object[] { count, msg, state });
            }
            else if (e.UserState is string)
            {
                string msg = e.UserState as string;
                int count = tlResult.AllNodesCount + 1;
                bool state = false;
                if (msg.Contains("완료"))
                    state = true;
                else
                    ++_errCount;

                tlResult.Nodes.Add(new object[] { count, msg, state });
            }
        }


        //endregion WizardPage1

        public bool IsCompleted { get; set; }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarControl1.Position = 100;
            progressBarControl1.Refresh();

            if (!e.Cancelled)
            {
                wizardPage1.AllowCancel = true;
                wizardPage1.AllowBack = false;
                wizardPage1.AllowFinish = true;
            }
            else
            {
                tlResult.Nodes.Add(new object[] { null, "작업이 취소되었습니다.", EnumInfoCode.Failure });
                wizardPage1.AllowCancel = true;
                wizardPage1.AllowBack = true;
                wizardPage1.AllowFinish = true;
            }
            IsCompleted = true;
            //DataOperation.UpdateEchelon();
            this.Refresh();
        }

    }

}