
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

namespace WareHousingMaster.view.search
{
    public partial class DlgImportInventoryPrice : DevExpress.XtraEditors.XtraForm
    {
        enum EnumInfoCode { Info = 0, Pass = 1, Warning = 2, Failure = 3 }

        public int _errCount = 0;

        public DataTable _dtExcel { get; private set; }

        int _cnt;

        public long _id { get; private set; }
        public string fileNm { get; private set; }
        public string filePath { get; private set; }

        public bool _isSuccess { get; private set; }

        public List<string> listBarcode { get; private set; }
        public List<long> listPrice { get; private set; }


        public delegate void receiptChangeHandler(DataTable dt);
        public event receiptChangeHandler receiptChangeEvent;

        XtraMessageBoxArgs args = new XtraMessageBoxArgs();

        public DlgImportInventoryPrice()
        {
            InitializeComponent();
            //_dtReceipt = dtReceipt;
            //_dtReceiptPart = dtReceiptPart;

            _isSuccess = false;
            fileNm = null;

            listBarcode = new List<string>();
            listPrice  = new List<long>();
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

                    dt.TableName = "BOM 구조";
                    dt.BeginInit();

                    //for (int i = 0; i < _BomColumns.Count; i++)
                    //{
                    //    string colName = _BomColumns[i];
                    //    dt.Columns.Add(colName, typeof(string));
                    //    dt.Columns[colName].SetOrdinal(i);
                    //    dt.Columns[colName].ColumnName = _BomColumnName[i];
                    //}

                    //dt.EndInit();


                    //ExportExportDOM(new List<DataTable>() { dt }, file.FileName);

                    //if (MessageHelper.Confirm("파일이 저장되었습니다. 폴더를 확인하시겠습니까?") == DialogResult.Yes)
                    //{
                    //    System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(file.FileName));
                    //}
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
            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"불러온 데이터를 기반으로 제품 가격을 수정합니다.", (int)EnumInfoCode.Info })));
            backgroundWorker1.ReportProgress(50);

            //receiptChangeEvent(_dtExcel);
            if (!updatePrice(_dtExcel))
            {
                tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"데이터 수정을 실패하였습니다.", (int)EnumInfoCode.Failure })));
                backgroundWorker1.ReportProgress(100);
                return;
            }
            backgroundWorker1.ReportProgress(80);
            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"데이터 수정을 완료했습니다.", (int)EnumInfoCode.Info })));
            backgroundWorker1.ReportProgress(100);

            _isSuccess = true;
        }


        private bool updatePrice(DataTable dt)
        {
            //_gvReceipt.BeginDataUpdate();

            try
            {
                JObject jResult = new JObject();
 
                foreach (DataRow row in dt.Rows)
                {
                    string barcode = ConvertUtil.ToString(row["재고번호"]);
                    long price = ConvertUtil.ToInt64(row["부품가"]);

                    if(!string.IsNullOrEmpty(barcode))
                    {
                        listBarcode.Add(barcode);
                        listPrice.Add(price);
                    }
                }

                if (DBConnect.updateInventorytPriceBulk(listBarcode, listPrice, ref jResult))
                {
                    tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"부품가 수정 완료", (int)EnumInfoCode.Info })));
                    return true;
                }
                else
                {
                    tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"부품가 수정 실패, 관리자 문의 바람", (int)EnumInfoCode.Failure })));
                    return false;
                }

                
            }
            catch (Exception ex)
            {
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