
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

namespace WareHousingMaster.view.consigned
{
    public partial class DlgImportConsignedReceipt : DevExpress.XtraEditors.XtraForm
    {
        enum EnumInfoCode { Info = 0, Pass = 1, Warning = 2, Failure = 3 }

        public int _errCount = 0;
        public DataTable _dtReceipt { get; private set; }
        public DataTable _dtReceiptPart { get; private set; }
        //DataTable _dtReceipt;
        //DataTable _dtReceiptPart;
        public DataTable _dtExcel { get; private set; }

        Dictionary<string, string> _dicProductType;
        Dictionary<string, string> _dicGuarantee;

        string[] _consignedComponetCd;

        Dictionary<long, List<long>> _dicReceiptPart;
        Dictionary<long, List<long>> _dicConsignedModel;

        Dictionary<string, int> _dicConsignedComponentCdReverse;

        GridView _gvReceipt;

        int _cnt;

        public long _id { get; private set; }
        public string fileNm { get; private set; }
        public string filePath { get; private set; }

        public bool _isSuccess { get; private set; }

        public delegate void receiptChangeHandler(DataTable dt);
        public event receiptChangeHandler receiptChangeEvent;

        XtraMessageBoxArgs args = new XtraMessageBoxArgs();

        public DlgImportConsignedReceipt(
            //DataTable dtReceipt,
            //DataTable dtReceiptPart,
            Dictionary<string, string> dicProductType,
            Dictionary<string, string> dicGuarantee,
            string[] consignedComponetCd,
            Dictionary<string, int> dicConsignedComponentCdReverse,
             Dictionary<long, List<long>> dicReceiptPart,
            Dictionary<long, List<long>> dicConsignedModel,
             GridView gvReceipt,
            long id,
            int cnt)
        {
            InitializeComponent();

            //_dtReceipt = dtReceipt;
            //_dtReceiptPart = dtReceiptPart;
            _dicProductType = dicProductType;
            _dicGuarantee = dicGuarantee;
            _consignedComponetCd = consignedComponetCd;
            _dicConsignedComponentCdReverse = dicConsignedComponentCdReverse;
            _dicReceiptPart = dicReceiptPart;
            _dicConsignedModel = dicConsignedModel;
            _gvReceipt = gvReceipt;
            _id = id;
            _cnt = cnt;
            _isSuccess = false;
            fileNm = null;

            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("PROXY_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("PRODUCT_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("GUARANTEE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("GUARANTEE_FROM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("GUARANTEE_TO", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("SALE_ROOT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("RELEASE_TYPE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM1", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM2", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TEL1", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("TEL2", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP1", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP2", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("POSTAL_CD", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS_DETAIL", typeof(string)));


            _dtReceiptPart = new DataTable();
            _dtReceiptPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPart.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("P_PART_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("PART_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("MODEL_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_CD_T", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
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
            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"불러온 데이터를 기반으로 접수정보를 구성합니다.", (int)EnumInfoCode.Info })));
            backgroundWorker1.ReportProgress(50);

            //receiptChangeEvent(_dtExcel);
            if (!AddReceiptByExcel(_dtExcel))
            {
                tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"접수정보구성을 실패하였습니다.", (int)EnumInfoCode.Failure })));
                backgroundWorker1.ReportProgress(100);
                return;
            }
            backgroundWorker1.ReportProgress(80);
            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"접수정보를 구성을 완료했습니다.", (int)EnumInfoCode.Info })));
            backgroundWorker1.ReportProgress(100);

            _isSuccess = true;
        }



        private bool AddReceiptByExcel(DataTable dt)
        {
            //_gvReceipt.BeginDataUpdate();

            try
            {
                _dtReceipt.BeginInit();
                _dtReceiptPart.BeginInit();

                foreach (DataRow row in dt.Rows)
                {
                    DataRow dr = _dtReceipt.NewRow();

                    string productType = ConvertUtil.ToString(row["제품유형"]);
                    string guarantee = ConvertUtil.ToString(row["보증기간"]);
                    int lGuarantee = 0;

                    dr["NO"] = _cnt++;
                    dr["ID"] = _id;
                    dr["PROXY_ID"] = -1;
                    dr["RECEIPT"] = "";
                    dr["RECEIPT_DT"] = string.Format("{0:d}", DateTime.Today);
                    dr["MODEL_NM"] = row["모델"];
                    if (_dicProductType.ContainsKey(productType))
                        dr["PRODUCT_TYPE"] = _dicProductType[productType];
                    else
                        dr["PRODUCT_TYPE"] = "-1";

                    if (_dicGuarantee.ContainsKey(guarantee))
                    {
                        dr["GUARANTEE"] = _dicGuarantee[guarantee];
                        lGuarantee = ConvertUtil.ToInt32(_dicGuarantee[guarantee]);
                    }
                    else
                        dr["GUARANTEE"] = "-1";
                    dr["GUARANTEE_FROM"] = string.Format("{0:d}", DateTime.Today);
                    dr["GUARANTEE_TO"] = string.Format("{0:d}", DateTime.Today.AddMonths(lGuarantee));
                    dr["COMPANY_ID"] = ProjectInfo._userCompanyId;
                    dr["SALE_ROOT"] = "0";
                    dr["RELEASE_TYPE"] = "1";
                    dr["DES"] = row["요청사항"];

                    dr["CUSTOMER_NM1"] = row["고객명1"];
                    dr["CUSTOMER_NM2"] = row["고객명2"];
                    dr["TEL1"] = row["고객전화번호1"];
                    dr["TEL2"] = row["고객전화번호2"];
                    dr["HP1"] = row["고객휴대폰1"];
                    dr["HP2"] = row["고객휴대폰2"];
                    dr["POSTAL_CD"] = row["우편번호"];
                    dr["ADDRESS"] = row["고객주소"];
                    dr["ADDRESS_DETAIL"] = "";

                    for (int i = 0; i < _consignedComponetCd.Length; i++)
                    {

                        DataRow rowComp = _dtReceiptPart.NewRow();

                        rowComp["ID"] = _id;
                        rowComp["P_PART_ID"] = -1;
                        rowComp["PART_ID"] = i;
                        rowComp["COMPONENT_ID"] = -1;
                        rowComp["CONSIGNED_TYPE"] = -1;
                        rowComp["COMPONENT_CD"] = _consignedComponetCd[i];
                        rowComp["COMPONENT_CD_T"] = _consignedComponetCd[i];
                        //row["MODEL_NM"] = _consignedComponetNm[i];
                        rowComp["MODEL_NM"] = "";
                        rowComp["PART_CNT"] = 0;

                        _dtReceiptPart.Rows.Add(rowComp);
                    }



                    JObject jobj = new JObject();
                    jobj.Add("MODEL_NM", $"{ row["모델"]}");
                    jobj.Add("CPU", $"{ row["CPU"]}");
                    jobj.Add("MBD", $"{ row["MBD"]}");
                    jobj.Add("MEM1", $"{ row["MEM1"]}");

                    jobj.Add("MEM2", $"{ row["MEM2"]}");
                    jobj.Add("SSD", $"{ row["SSD"]}");
                    jobj.Add("HDD", $"{ row["HDD"]}");
                    jobj.Add("VGA", $"{ row["VGA"]}");

                    jobj.Add("MON", $"{ row["모니터"]}");
                    jobj.Add("POW", $"{ row["파워"]}");
                    jobj.Add("KEY", $"{ row["키보드"]}");
                    jobj.Add("MOU", $"{ row["마우스"]}");
                    jobj.Add("CAB", $"{ row["케이블"]}");
                    jobj.Add("PER", $"{ row["주변기기"]}");
                    jobj.Add("OS", $"{ row["OS"]}");
                    jobj.Add("COUPON", $"{ row["쿠폰"]}");
                    jobj.Add("AIR", $"{ row["AIR"]}");
                    jobj.Add("BOX", $"{ row["박스"]}");
                    jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    JObject jResult = new JObject();
                    List<long> listModel = new List<long>();
                    List<long> listPart = new List<long>();

                    if (DBConsigned.getReceiptComponentListByExcel(jobj, ref jResult))
                    {
                        if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                        {

                            long modelListId = ConvertUtil.ToInt64(jResult["MODEL_LIST_ID"]);
                            if (modelListId >= 0)
                            {
                                DataRow rRow = _dtReceiptPart.NewRow();

                                rRow["ID"] = _id;
                                rRow["P_PART_ID"] = _dicConsignedComponentCdReverse["MODEL"];
                                rRow["PART_ID"] = modelListId * -1;
                                rRow["COMPONENT_ID"] = -1;
                                rRow["MODEL_ID"] = modelListId;
                                rRow["CONSIGNED_TYPE"] = -1;
                                rRow["COMPONENT_CD"] = "";
                                rRow["MODEL_NM"] = row["모델"];
                                rRow["PART_CNT"] = 0;

                                _dtReceiptPart.Rows.Add(rRow);

                                listModel.Add(modelListId);
                            }

                            DataRow[] rowRoots;
                            DataRow rowRoot;
                            long partId;

                            JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                            foreach (JObject obj in jArray.Children<JObject>())
                            {
                                long componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                                partId = _dicConsignedComponentCdReverse[ConvertUtil.ToString(obj["COMPONENT_CD"])];

                                rowRoots = _dtReceiptPart.Select($"ID = {_id} AND PART_ID = {partId} AND P_PART_ID = -1");
                                if (rowRoots.Length < 1)
                                    continue;
                                else
                                    rowRoot = rowRoots[0];



                                // 여기부터 다시, model 정보에서  부품 model_nm 가져오도록 수정하고, model 정보 table에 넣어야 하고, dic list에 관리하는거 추가하면 될듯.
                                if (listPart.Contains(componentId))
                                {
                                    DataRow[] rows = _dtReceiptPart.Select($"ID = {_id} AND COMPONENT_ID = {componentId}");

                                    foreach (DataRow rowp in rows)
                                    {
                                        int cnt = ConvertUtil.ToInt32(rowp["PART_CNT"]);
                                        cnt += ConvertUtil.ToInt32(obj["COMPONENT_CNT"]);
                                        rowp["PART_CNT"] = cnt;

                                        int partCnt = ConvertUtil.ToInt32(rowRoot["PART_CNT"]);
                                        partCnt += ConvertUtil.ToInt32(obj["COMPONENT_CNT"]);
                                        rowRoot["PART_CNT"] = partCnt;
                                    }
                                }
                                else
                                {
                                    listPart.Add(componentId);

                                    DataRow rowComp = _dtReceiptPart.NewRow();

                                    rowComp["ID"] = _id;
                                    rowComp["P_PART_ID"] = partId;
                                    rowComp["PART_ID"] = componentId;
                                    rowComp["COMPONENT_ID"] = componentId;
                                    rowComp["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                                    rowComp["COMPONENT_CD_T"] = obj["COMPONENT_CD"];
                                    rowComp["COMPONENT_CD"] = "";
                                    rowComp["MODEL_NM"] = $"{obj["PART_NM"]}";
                                    rowComp["PART_CNT"] = obj["COMPONENT_CNT"];

                                    _dtReceiptPart.Rows.Add(rowComp);

                                    int partCnt = ConvertUtil.ToInt32(rowRoot["PART_CNT"]);
                                    partCnt++;
                                    rowRoot["PART_CNT"] = partCnt;
                                }
                            }
                        }
                    }

                    _dtReceipt.Rows.Add(dr);

                    _dicReceiptPart.Add(_id, listPart);
                    _dicConsignedModel.Add(_id, listModel);

                    _id++;
                }

                _dtReceiptPart.EndInit();
                _dtReceipt.EndInit();

                return true;
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