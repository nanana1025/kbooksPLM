
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


namespace WareHousingMaster.view.consigned
{
    public partial class DlgImportConsignedReceiptByModel : DevExpress.XtraEditors.XtraForm
    {
        enum EnumInfoCode { Info = 0, Pass = 1, Warning = 2, Failure = 3 }

        public int _errCount = 0;
        public DataTable _dtCompanyModel { get; private set; }
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
        public Dictionary<long, string> _dicComponentNm { get; private set; }

        Dictionary<string, int> _dicConsignedComponentCdReverse;

        Dictionary<string, string> _dicModelList;

        Dictionary<string, int> _dicDeviceType;

        Dictionary<string, int> _dicDeviceType2;

        Dictionary<string, int> _dicMemType;

        Dictionary<string, int> _dicMemSlotCnt;

        Dictionary<int, string> _dicModelNm;

        Dictionary<int, string> _dicModelGrade;

        public long _companyId { get; set; }


        public Dictionary<int, List<long>> _dicConsignedType { get; private set; }

        public Dictionary<int, Dictionary<string, Dictionary<long, int>>> _dicConsignedComponent { get; private set; }

        GridView _gvReceipt;


        int _cnt;

        public long _id { get; private set; }
        public string fileNm { get; private set; }
        public string filePath { get; private set; }

        public bool _isSuccess { get; private set; }

        public bool _isTomorrow { get; private set; }

        public delegate void receiptChangeHandler(DataTable dt);
        public event receiptChangeHandler receiptChangeEvent;

        XtraMessageBoxArgs args = new XtraMessageBoxArgs();

        public DlgImportConsignedReceiptByModel(
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

            _dtCompanyModel = new DataTable();

            _dtCompanyModel.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("MODEL_LIST_ID", typeof(long)));
            _dtCompanyModel.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtCompanyModel.Columns.Add(new DataColumn("PC_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("RECEIPT_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("CPU_ASSIGN_YN", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("MEM_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("MEM_SLOT", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("SERIAL_NO_TYPE", typeof(int)));
            _dtCompanyModel.Columns.Add(new DataColumn("STATE", typeof(int)));


            _dtReceipt = new DataTable();
            _dtReceipt.Columns.Add(new DataColumn("ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("TYPE", typeof(int)));
            _dtReceipt.Columns.Add(new DataColumn("RECEIPT_DT", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_NM_DETAIL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("CUSTOMER_NM", typeof(string)));        
            _dtReceipt.Columns.Add(new DataColumn("TEL", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("HP", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("POSTAL_CD", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("REQUEST", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_ID_NM", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MODEL_ID", typeof(long)));
            _dtReceipt.Columns.Add(new DataColumn("B_GRADE", typeof(short)));

            //임시 사용
            _dtReceipt.Columns.Add(new DataColumn("OLD_COA_SN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("NEW_COA_SN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("COUPON_MANAGE", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("MBD_SN", typeof(string)));
            _dtReceipt.Columns.Add(new DataColumn("DELIVERY", typeof(string)));

            _dtReceipt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dtReceipt.Columns.Add(new DataColumn("ERROR", typeof(string)));


            _dicModelList = new Dictionary<string, string>()
            {
                {"600G1", "600G1 AIO"}, {"600G2", "600G2 AIO"}, {"600G3", "600G3 AIO"}, {"6300", "6300 AIO"}, {"E5550", "E5550"}
                , {"L450", "L450"}, {"L540", "L540"}, {"L560", "L560"}, {"M700Z", "M700Z"}, {"X240", "X240"}
                , {"X260", "X260"}, {"3020", "데스크탑 3020 SFF"}, {"DB400", "DB400"}, {"E7240", "E7240"}, {"E5540", "E5540"}
                , {"3050", "3050 SFF"}, {"3240", "3240 AIO"}, {"1724", "SURFACE 1724"}, {"1796", "SURFACE 1796"}, {"M73Z", "M73Z AIO"}
                , {"M900Z", "M900Z AIO"}, {"9020", "9020 SFF"}, {"9010", "9010"}, {"7450", "7450"}, {"T580", "SM-T580"}, {"P585", "SM-P585"}, {"P600", "SM-P600"}
                , {"600G4S", "600G4S AIO"}, {"600G4H", "600G4H AIO"}, {"NT301E5C", "NT301E5C"}
            };

            _dicDeviceType = new Dictionary<string, int>() //server에서 사용하는 pctype, 1:pc, 2:laptop, 3:allinone, 4:slim, 5:tablet
            {
                {"600G1", 3}, {"600G2", 2}, {"600G3", 2}, {"6300", 3}, {"E5550", 2}
                , {"L450", 2}, {"L540", 2}, {"L560", 2}, {"M700Z", 2}, {"X240", 2}
                , {"X260", 2}, {"3020", 3}, {"DB400", 3}, {"E7240", 2}, {"E5540", 2}
                , {"3050", 4}, {"3240", 3}, {"1724", 5}, {"1796", 5}, {"M73Z", 3}
                , {"M900Z", 3}, {"9020", 4}, {"9010", 3}, {"7450", 3}, {"T580", 5}, {"P585", 5}, {"P600", 5}
                , {"600G4S", 2}, {"600G4H", 2}, {"NT301E5C", 2}
            };

            _dicDeviceType2 = new Dictionary<string, int>() //생산대행에서 사용하는 pctype, 1:올인원, 2:laptop, 3:PC, 4:slim, 5:tablet
            {
                {"600G1", 1}, {"600G2", 1}, {"600G3", 1}, {"6300", 1}, {"E5550", 2}
                , {"L450", 2}, {"L540", 2}, {"L560", 2}, {"M700Z", 1}, {"X240", 2}
                , {"X260", 2}, {"3020", 3}, {"DB400", 3}, {"E7240", 2}, {"E5540", 2}
                , {"3050", 4}, {"3240", 1}, {"1724", 5}, {"1796", 5}, {"M73Z", 1}
                , {"M900Z", 1}, {"9020", 4}, {"9010", 1}, {"7450", 1}, {"T580", 5}, {"P585", 5}, {"P600", 5}
                , {"600G4S", 1}, {"600G4H", 1}, {"NT301E5C", 1}
            };

            _dicMemType = new Dictionary<string, int>()
            {
                {"600G1", 3}, {"600G2", 3}, {"600G3", 3}, {"6300", 3}, {"E5550", 3}
                , {"L450", 3}, {"L540", 3}, {"L560", 3}, {"M700Z", 3}, {"X240", 3}
                , {"X260", 4}, {"3020", 3}, {"DB400", 3}, {"E7240", 3}, {"E5540", 3}
                , {"3050", 4}, {"3240", 3}, {"1724", -1}, {"1796", -1}, {"M73Z", 3}
                , {"M900Z", 4}, {"9020", 3}, {"9010", 3}, {"7450", 4},{"T580", -1}, {"P585", -1}, {"P600", -1}
                , {"600G4S", 4}, {"600G4H", 4}, {"NT301E5C", 3}
            };

            _dicMemSlotCnt = new Dictionary<string, int>()
            {
                {"600G1", 2}, {"600G2", 2}, {"600G3", 2}, {"6300", 2}, {"E5550", 2}
                , {"L450", 2}, {"L540", 2}, {"L560", 2}, {"M700Z", 2}, {"X240", 2}
                , {"X260", 1}, {"3020", 2}, {"DB400", 2}, {"E7240", 2}, {"E5540", 2}
                , {"3050", 2}, {"3240", 2}, {"1724", 0}, {"1796", 0}, {"M73Z", 2}
                , {"M900Z", 2}, {"9020", 2}, {"9010", 2}, {"7450", 2},{"T580", 0}, {"P585", 0}, {"P600", 0}
                , {"600G4S", 2}, {"600G4H", 2}, {"NT301E5C", 2}
            };

            _dicConsignedComponent = new Dictionary<int, Dictionary<string, Dictionary<long, int>>>();

            _dicComponentNm = new Dictionary<long, string>();

            _dicModelNm = new Dictionary<int, string>();

            _dicModelGrade = new Dictionary<int, string>();

            _dicConsignedType = new Dictionary<int, List<long>>();

            _isTomorrow = true;

        }

        private void DlgWizardImportData_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            getCompanyModelList();
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
            if (!AddReceiptByExcel1(_dtExcel))
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


        private bool AddReceiptByExcel1(DataTable dt)
        {
            string model;
            string[] arrModelPart;
            Char[] delimiter = { '(', ')', '[', ']', '/', '+', '_' };
            string[] subComponentCd = { "LIC", "PKG", "AIR", "CAB" };

            var today = DateTime.Today;
            int hour = ConvertUtil.ToInt32(DateTime.Now.ToString("HH"));

            if (hour > 16)
            {
                today = DateTime.Now.AddDays(1);
                _isTomorrow = true;
            }

            string receiptDt = today.ToString("yyyy-MM-dd");

            var jArray = new JArray();
            JObject jobj = new JObject();

            int deviceType = 0;
            int deviceType2 = -1;

            string modelNm;
            string modelCd;
            string cpu;
            string gen;
            string memType = "";
            int memSize = 0;

            bool wirelessLan = false;
            bool wirelessMouse = false;
            bool wirelessKeyboardMouse = false;
            bool keyboardMouse = false;
            bool HDMI = false;
            bool DPHDMI = false;
            int windows = 10;
            bool pro = false;
            string fault = "";
            bool skip = false;
            bool onlyProduct = false;
            bool memExist = false;
            bool stgExist = false;

            int memTypeCheck = 4;
            int memSlotCnt = 2;
            int id = 0;
            short bGrade = 0;
            string request;
            int modelComplete = 0;
            List<string[]> listStg = new List<string[]>();

            foreach (DataRow row in dt.Rows)
            {
                modelNm = "TEST";
                modelCd = "TEST";
                cpu = "";
                gen = "1";
                memType = "N/A";
                memSize = 0;

                wirelessLan = false;
                wirelessMouse = false;
                wirelessKeyboardMouse = false;
                keyboardMouse = false;
                HDMI = false;
                DPHDMI = false;
                windows = 10;
                pro = false;
                fault = "";
                skip = false;
                onlyProduct = false;
                memExist = false;
                stgExist = false;
                memTypeCheck = 4;
                bGrade = 0;
                deviceType = -1;
                deviceType2 = -1;
                memSlotCnt = 2;
                modelComplete = 0;

                listStg.Clear();

                request = ConvertUtil.ToString(row["참고사항"]);
                model = ConvertUtil.ToString(row["MODEL"]);

                if (request.ToUpper().Contains("B급") || request.ToUpper().Contains("B 급"))
                    bGrade = 1;

                if (bGrade == 0 && (model.ToUpper().Contains("B급") || model.ToUpper().Contains("B 급")))
                    bGrade = 1;

                DataRow dr = _dtReceipt.NewRow();
                dr["ID"] = id;

                dr["CUSTOMER_NM"] = row["고객명"];
                dr["TEL"] = row["전화번호"];
                dr["HP"] = row["휴대전화"];
                string postalCd = ConvertUtil.ToString(row["우편번호"]);
                if (postalCd.Length == 4)
                    dr["POSTAL_CD"] = $"0{postalCd}";
                else
                    dr["POSTAL_CD"] = postalCd;
                dr["ADDRESS"] = row["고객주소"];
                dr["DES"] = row["배송메세지"];
                dr["REQUEST"] = request;
                dr["RECEIPT_DT"] = receiptDt;

                //임시사용
                dr["OLD_COA_SN"] = row["WIN7"];
                dr["NEW_COA_SN"] = row["WIN10"];
                dr["COUPON_MANAGE"] = row["쿠폰번호"];
                dr["MBD_SN"] = row["S/N"];
                dr["DELIVERY"] = row["송장번호"];
               
                dr["CHECK"] = true;
                dr["ERROR"] = "";
                dr["B_GRADE"] = bGrade;



                dr["MODEL_NM_DETAIL"] = model;
                int index = model.IndexOf("_");
                if (index > 0 && index < 10)
                {
                    fault = model.Substring(0, index);
                    model = model.Substring(index + 1);

                    _dicModelGrade.Add(id, fault);
                }
                _dicModelNm.Add(id, model);
                arrModelPart = model.Split(delimiter);

                if (arrModelPart.Length > 4)
                {
                    modelNm = getMdelNm(arrModelPart[0], ref modelCd, ref deviceType, ref deviceType2);
                    dr["MODEL_NM"] = modelNm;
                    memTypeCheck = _dicMemType[modelCd];
                    memSlotCnt = _dicMemSlotCnt[modelCd];


                    cpu = getCpu(arrModelPart[1], ref gen);
                    getMem(arrModelPart[2], ref memType, ref memSize);

                    for (int i = 3; i < arrModelPart.Length; i++)
                    {
                        string str = arrModelPart[i].ToUpper();
                        if (str.Contains("교환건"))
                        {
                            onlyProduct = true;
                        }
                        else if (str.Contains("윈도우") || str.Contains("윈") || str.Contains("WIN"))
                        {
                            if (str.Contains("프로"))
                                pro = true;

                            if (str.Contains("GB"))
                            {
                                string[] arrStrTmp = str.Split(' ');

                                string strStg = $"SSD {arrStrTmp[0]}";
                                stgExist = true;

                                string strTmp = Regex.Replace(arrStrTmp[1], @"\D", "");
                                windows = ConvertUtil.ToInt32(strTmp);
                            }
                            else
                            {
                                string strTmp = Regex.Replace(str, @"\D", "");
                                windows = ConvertUtil.ToInt32(strTmp);
                            }
                        }
                        else if (str.Contains("SSD") || str.Contains("HDD") || str.Contains("NVME"))
                        {
                            getSTG(str, ref listStg);
                        }

                        else if (str.Contains("무선인터넷"))
                        {
                            if (str.Contains("없이"))
                                wirelessLan = false;
                            else
                                wirelessLan = true;
                        }
                        else if (str.Contains("마우스"))
                        {
                            wirelessMouse = true;
                        }
                        else if (str.Contains("무선키마"))
                        {
                            wirelessKeyboardMouse = true;
                        }
                        else if (str.Contains("유선키마"))
                        {
                            keyboardMouse = true;
                        }
                        else if (str.Contains("DP TO HDMI"))
                        {
                            DPHDMI = true;
                        }
                        else if (str.Contains("HDMI"))
                        {
                            HDMI = true;
                        }
                    }
                }
                else if (arrModelPart.Length > 0)
                {
                    modelNm = getMdelNm(arrModelPart[0], ref modelCd, ref deviceType, ref deviceType2);
                    dr["MODEL_NM"] = modelNm;
                    modelComplete = 1;

                }
                else
                    skip = true;


                dr["TYPE"] = deviceType2;

                JObject jdata = new JObject();

                jdata.Add("ID", id);

                jdata.Add("SKIP", skip);
                jdata.Add("PRODUCT", onlyProduct);

                jdata.Add("TYPE", deviceType);
                jdata.Add("GRADE", fault);
                jdata.Add("MODEL_NM", modelNm);
                jdata.Add("MODEL_CD", modelCd);
                
                jdata.Add("CPU", cpu);
                jdata.Add("GEN", gen);
                jdata.Add("MEM_TYPE_CHECK", memTypeCheck);
                jdata.Add("MEM_SLOT_CNT", memSlotCnt);
                jdata.Add("MEM_TYPE", memType);
                jdata.Add("MEM_SIZE", memSize);
                jdata.Add("STG_CNT", listStg.Count);

                int stgIndex = 0;
                foreach(string[] stg in listStg)
                {
                    jdata.Add($"STG_TYPE_{stgIndex}", stg[0]);
                    jdata.Add($"STG_SIZE_{stgIndex}", stg[1]);
                    stgIndex++;
                }

                jdata.Add("OS", windows);
                jdata.Add("OS_PRO", pro);

                jdata.Add("WIRELESS_LAN", wirelessLan);
                jdata.Add("WIRELESS_MOUSE", wirelessMouse);
                jdata.Add("WIRELESS_SET", wirelessKeyboardMouse);
                jdata.Add("WIRED_SET", keyboardMouse);
                
                jdata.Add("HDMI", HDMI);
                jdata.Add("DPHDMI", DPHDMI);
                jdata.Add("MODEL_COMPLETE", modelComplete);
                

                jArray.Add(jdata);

                id++;

                _dtReceipt.Rows.Add(dr);

            }

            jobj.Add("DATA", jArray);
            jobj.Add("COMPANY_ID", _companyId);

            JObject jResult = new JObject();

            if (DBConsigned.getReceiptModelPart(jobj, ref jResult))
            {
                //_dicConsignedComponent = new Dictionary<int, Dictionary<string, Dictionary<long, int>>>();

                JArray jArrayResult = JArray.Parse(jResult["DATA"].ToString());

                bool success = true;
                string error = "";

                foreach (JObject obj in jArrayResult.Children<JObject>())
                {
                    Dictionary<string, Dictionary<long, int>> dicComponent = new Dictionary<string, Dictionary<long, int>>();
                    success = true;
                    error = "";

                    skip = ConvertUtil.ToBoolean(obj["SKIP"]);
                    onlyProduct = ConvertUtil.ToBoolean(obj["EXCHANGE"]);

                    int nId = ConvertUtil.ToInt32(obj["ID"]);

                    tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"모델[{_dicModelNm[nId]}] 구성... ", (int)EnumInfoCode.Info })));
                    DataRow[] rows = _dtReceipt.Select($"ID = {nId}");

                    if (!skip)
                    {
                        rows[0]["MODEL_ID"] = obj["MODEL_ID"];
                        rows[0]["MODEL_ID_NM"] = obj["MODEL_ID_NM"];

                        if (ConvertUtil.ToBoolean(obj["CONSIGNED"]))
                        {
                            JArray jArrayConsignedTypeResult = JArray.Parse(obj["CONSIGNED_TYPE"].ToString());

                            List<long> listConsignedTypeId = new List<long>();
                            foreach (JObject CTObj in jArrayConsignedTypeResult.Children<JObject>())
                            {
                                long congisnedComponentId = ConvertUtil.ToInt64(CTObj["COMPONENT_ID"]);
                                if (!listConsignedTypeId.Contains(congisnedComponentId))
                                    listConsignedTypeId.Add(congisnedComponentId);
                            }

                            _dicConsignedType.Add(nId, listConsignedTypeId);
                        }

                        long mbd = ConvertUtil.ToInt64(obj["MBD"]);
                        if (mbd > 0)
                        {

                            Dictionary<long, int> dicComp = new Dictionary<long, int>();

                            dicComp.Add(mbd, 1);
                            dicComponent.Add("MBD", dicComp);

                            string repNm = ConvertUtil.ToString(obj["MBD_REP_NM"]);
                            if (!_dicComponentNm.ContainsKey(mbd))
                                _dicComponentNm.Add(mbd, repNm);

                        }
                        else
                        {
                            success = false;
                            error += "메인보드X / ";
                            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"메인보드 정보 없음", (int)EnumInfoCode.Warning })));
                        }

                        int memCnt = ConvertUtil.ToInt32(obj["MEM_CNT"]);
                        if (memCnt > 0)
                        {
                            Dictionary<long, int> dicComp = new Dictionary<long, int>();

                            for (int i = 0; i < memCnt; i++)
                            {
                                long mem = ConvertUtil.ToInt64(obj[$"MEM_{i}"]);

                                if (dicComp.ContainsKey(mem))
                                    dicComp[mem]++;
                                else
                                    dicComp.Add(mem, 1);

                                string repNm = ConvertUtil.ToString(obj[$"MEM_REP_NM_{i}"]);
                                if (!_dicComponentNm.ContainsKey(mem))
                                    _dicComponentNm.Add(mem, repNm);
                            }
                            dicComponent.Add("MEM", dicComp);
                        }
                        else
                        {
                            success = false;
                            error += "메모리 / ";
                            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"메모리 정보 없음", (int)EnumInfoCode.Warning })));
                        }

                        int stgCnt = ConvertUtil.ToInt32(obj["STG_CNT"]);
                        if (stgCnt > 0)
                        {
                            Dictionary<long, int> dicCompSSD = new Dictionary<long, int>();
                            Dictionary<long, int> dicCompHDD = new Dictionary<long, int>();

                            bool ssd = false;
                            bool hdd = false;

                            for (int i = 0; i < stgCnt; i++)
                            {
                                long stg = ConvertUtil.ToInt64(obj[$"STG_{i}"]);
                                string stgType = ConvertUtil.ToString(obj[$"STG_TYPE_{i}"]);

                                if (stgType.Equals("SSD"))
                                {
                                    if (dicCompSSD.ContainsKey(stg))
                                        dicCompSSD[stg]++;
                                    else
                                        dicCompSSD.Add(stg, 1);
                                    ssd = true;
                                }
                                else
                                {
                                    if (dicCompHDD.ContainsKey(stg))
                                        dicCompHDD[stg]++;
                                    else
                                        dicCompHDD.Add(stg, 1);
                                    hdd = true;
                                }

                                string repNm = ConvertUtil.ToString(obj[$"STG_REP_NM_{i}"]);
                                if (!_dicComponentNm.ContainsKey(stg))
                                    _dicComponentNm.Add(stg, repNm);
                            }

                            if (ssd)
                                dicComponent.Add("SSD", dicCompSSD);

                            if (hdd)
                                dicComponent.Add("HDD", dicCompHDD);
                        }
                        else
                        {
                            success = false;
                            error += "STORAGE X / ";
                            tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"STORAGE 정보 없음", (int)EnumInfoCode.Warning })));
                        }

                        //long os = ConvertUtil.ToInt64(obj["LIC"]);
                        //if (os > 0)
                        //{
                        //    Dictionary<long, int> dicComp = new Dictionary<long, int>();

                        //    dicComp.Add(os, 1);
                        //    dicComponent.Add("LIC", dicComp);

                        //    string repNm = ConvertUtil.ToString(obj["LIC_REP_NM"]);
                        //    if (!_dicComponentNm.ContainsKey(os))
                        //        _dicComponentNm.Add(os, repNm);
                        //}


                        long per = ConvertUtil.ToInt64(obj["PER"]);
                        if (per > 0)
                        {
                            Dictionary<long, int> dicComp = new Dictionary<long, int>();

                            dicComp.Add(per, 1);
                            dicComponent.Add("PER", dicComp);

                            string repNm = ConvertUtil.ToString(obj["PER_REP_NM"]);
                            if (!_dicComponentNm.ContainsKey(per))
                                _dicComponentNm.Add(per, repNm);
                        }

                        long key = ConvertUtil.ToInt64(obj["KEY"]);
                        if (key > 0)
                        {
                            Dictionary<long, int> dicComp = new Dictionary<long, int>();

                            dicComp.Add(key, 1);
                            dicComponent.Add("KEY", dicComp);

                            string repNm = ConvertUtil.ToString(obj["KEY_REP_NM"]);
                            if (!_dicComponentNm.ContainsKey(key))
                                _dicComponentNm.Add(key, repNm);
                        }

                        long mou = ConvertUtil.ToInt64(obj["MOU"]);
                        if (mou > 0)
                        {
                            Dictionary<long, int> dicComp = new Dictionary<long, int>();

                            dicComp.Add(mou, 1);
                            dicComponent.Add("MOU", dicComp);

                            string repNm = ConvertUtil.ToString(obj["MOU_REP_NM"]);
                            if (!_dicComponentNm.ContainsKey(mou))
                                _dicComponentNm.Add(mou, repNm);
                        }


                        foreach (string compCd in subComponentCd)
                        {
                            int cnt = ConvertUtil.ToInt32(obj[$"{compCd}_CNT"]);
                            if (cnt > 0)
                            {
                                Dictionary<long, int> dicComp = new Dictionary<long, int>();

                                for (int i = 0; i < cnt; i++)
                                {
                                    long compId = ConvertUtil.ToInt64(obj[$"{compCd}_{i}"]);

                                    if (dicComp.ContainsKey(compId))
                                        dicComp[compId]++;
                                    else
                                        dicComp.Add(compId, 1);

                                    string repNm = ConvertUtil.ToString(obj[$"{compCd}_REP_NM_{i}"]);
                                    if (!_dicComponentNm.ContainsKey(compId))
                                        _dicComponentNm.Add(compId, repNm);
                                }

                                dicComponent.Add(compCd, dicComp);
                            }
                        }

                        if(onlyProduct)
                        {
                            success = false;
                            error += "교환건 / ";
                        }
                    }
                    else
                    {
                        success = false;
                        error += "접수 정보 확인 필요 / ";
                    }

                    if(_dicModelGrade.ContainsKey(nId))
                    {
                        success = false;
                        error += $"{_dicModelGrade[nId]} / ";
                    }

                    _dicConsignedComponent.Add(nId, dicComponent);

                    if(success)
                    {
                        tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"모델 구성 성공", (int)EnumInfoCode.Info })));

                        //backgroundWorker1.ReportProgress(50);
                    }
                    else
                    {
                        

                        foreach(DataRow row in rows)
                        {
                            row["CHECK"] = false;
                            row["ERROR"] = error.Substring(0, error.Length-3);
                        }

                        tlResult.Invoke(new Action(() => tlResult.Nodes.Add(new object[] { ++_errCount, $"모델 구성 확인 필요", (int)EnumInfoCode.Warning })));

                    }

                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void getSTG(string str, ref List<string[]> listStg)
        {
            string type = "SSD";
            if (str.Contains("SSD") )
                type = "SSD";
            else if (str.Contains("HDD"))
                type = "HDD";
            else if (str.Contains("NVME"))
                type = "NVME";

            string strTmp = Regex.Replace(str, @"\D", "");
            int capacity = ConvertUtil.ToInt32(strTmp);

            string[] arrStg = new string[2] { type, ConvertUtil.ToString(capacity) };
            listStg.Add(arrStg);
        }

        private string getMdelNm(string str, ref string modelCd, ref int deviceType, ref int deviceType2)
        {
            string modelNm = str.ToUpper();
            foreach (KeyValuePair<string, string> item in _dicModelList)
            {
                if (modelNm.Contains(item.Key))
                {
                    modelCd = item.Key;
                    deviceType = _dicDeviceType[item.Key];
                    deviceType2 = _dicDeviceType2[item.Key];
                    return item.Value;
                }
            }

            Char[] delimiter = { '(', ')', '[', ']', '/', '+', ' ' };
            string[] arrStr = str.Trim().Split(delimiter);

            return arrStr[arrStr.Length - 1];
        }

        private string getCpu(string str, ref string gen)
        {
            Char[] delimiter = { '(', ')', '[', ']', '/', '+', ' ', '-' };
            str = str.ToUpper();
            string[] arrStr = str.Trim().Split(delimiter);

            int len = arrStr.Length;
            string[] arrCode = { "I3", "I5", "I7", "I9" };
            int cpuIndex;
            string code ="i5";
            for (cpuIndex = 0; cpuIndex < arrCode.Length; cpuIndex++)
            {
                if (str.Contains(arrCode[cpuIndex]))
                    break;
            }

            if (cpuIndex == arrCode.Length)
            {
                if (arrStr[len - 1].Length == 3)
                    gen = "3";
                else
                {
                    if(arrStr[len - 1].Substring(0,1).Equals(3))
                        gen = "3";
                    else if (arrStr[len - 1].Substring(0, 1).Equals(4))
                        gen = "6";
                    else if (arrStr[len - 1].Substring(0, 1).Equals(5))
                        gen = "6";
                    else if (arrStr[len - 1].Substring(0, 1).Equals(6))
                        gen = "6";
                }

                return $"G{arrStr[len - 1]}";
            }
            else
                code = arrCode[cpuIndex];


            string str1 = str.Replace(" ", "");
            str1 = str1.Replace("-", "");

            if(str1.Contains("세대"))
            {
                int index = str1.IndexOf("세대");
                gen = str1.Substring(index-1, 1);
            }
            else
            {
                if (str1.Contains("HZ"))
                {
                    gen = arrStr[len - 2].Substring(0, 1);
                }
                else
                {
                    gen = arrStr[len - 1].Substring(0, 1);
                }
                
            }


            string cpu = $"{code.ToLower()}-{gen}";

            return cpu;
        }

        private void getMem(string str, ref string memType, ref int memSize)
        {
            str = str.Replace("RAM", "");
            str = str.Replace("램", "");

            Char[] delimiter = { '(', ')', '[', ']', '/', '+', ' ', '-' };
            string[] arrStr = str.Trim().Split(delimiter);
            int len = arrStr.Length;

            if (len > 1)
            {
                memType = arrStr[0];
                string strTmp = Regex.Replace(arrStr[1], @"\D", "");
                memSize = ConvertUtil.ToInt32(strTmp);
            }
            else
            {
                string strTmp = Regex.Replace(arrStr[0], @"\D", "");
                memSize = ConvertUtil.ToInt32(strTmp);
            }
        }


        private void getCompanyModelList()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("COMPANY_ID", _companyId);

            if (DBConsigned.getCompanyModelList(jobj, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    int index = 1;

                    foreach (JObject jData in jArray.Children<JObject>())
                    {
                        DataRow dr1 = _dtCompanyModel.NewRow();
                        dr1["MODEL_LIST_ID"] = jData["MODEL_LIST_ID"];
                        dr1["MODEL_NM"] = jData["MODEL_NM"];
                        dr1["PC_TYPE"] = ConvertUtil.ToInt32(jData["PC_TYPE"]);
                        dr1["RECEIPT_TYPE"] = ConvertUtil.ToInt32(jData["RECEIPT_TYPE"]);
                        dr1["CPU_ASSIGN_YN"] = ConvertUtil.ToInt32(jData["CPU_ASSIGN_YN"]);
                        dr1["MEM_TYPE"] = ConvertUtil.ToInt32(jData["MEM_TYPE"]);
                        dr1["MEM_SLOT"] = ConvertUtil.ToInt32(jData["MEM_SLOT"]);
                        dr1["SERIAL_NO_TYPE"] = ConvertUtil.ToInt32(jData["SERIAL_NO_TYPE"]);
                        _dtCompanyModel.Rows.Add(dr1);
                    }
                }
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