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
using DevExpress.XtraCharts;

namespace WareHousingMaster.view.statistics
{
    public partial class usrConsignedReturnStatistics : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtStatistics;

        DataTable _dtReturnCheck;
        DataTable _dtReturnCheckDetail;

        DataTable _dtReturnCheckContent;
        DataTable _dtReturnCheckContentPast;

        DataTable _dtCheck;
        DataTable _dtQC;

        DataTable _dtModel;
        BindingSource _bsModel;

        List<string> _listCheckedCol;

        List<string> _listCheckedColNm;

        Dictionary<string, int> _dicCheckValue;

        Dictionary<string, int> _dicCheckValuePast;

        Dictionary<string, int> _dicCheckContentValue;

        Dictionary<string, int> _dicCheckContentValuePast;

        Dictionary<string, string> _dicCheckNm;

        Dictionary<string, string> _dicColCode;

        Dictionary<string, int> _dicCheckCodeCnt;

        List<string> _listRemainCol;

        List<long> _listCheckedCompany;
        List<int> _listCheckedProductType;
        List<string> _listCheckedContent;

        bool _realtime;

        int _interval = 10000;

        bool _initialize;

        public usrConsignedReturnStatistics()
        {
            InitializeComponent();

            NtbCheckTimer.Interval = _interval;

            _dtReturnCheckContent = new DataTable();
            _dtReturnCheckContent.Columns.Add(new DataColumn("CODE", typeof(string)));
            _dtReturnCheckContent.Columns.Add(new DataColumn("KEY", typeof(int)));
            _dtReturnCheckContent.Columns.Add(new DataColumn("CONTENT", typeof(string)));
            _dtReturnCheckContent.Columns.Add(new DataColumn("CNT", typeof(int)));

            _dtReturnCheckContentPast = new DataTable();
            _dtReturnCheckContentPast.Columns.Add(new DataColumn("CODE", typeof(string)));
            _dtReturnCheckContentPast.Columns.Add(new DataColumn("KEY", typeof(int)));
            _dtReturnCheckContentPast.Columns.Add(new DataColumn("CONTENT", typeof(string)));
            _dtReturnCheckContentPast.Columns.Add(new DataColumn("CNT", typeof(int)));


            

            //warehouseTimer.Interval = interval;

            //long _warehouseMovementId = -1;

            _listCheckedCol = new List<string>(new string[] {"RETURN_CNT","CUSTOMER_CNT", "FUNCTION_CNT", "PRODUCT_CNT", ",PART_CNT", "USER_CNT", "DESTROYED_CNT" });

            _dicCheckValue = new Dictionary<string, int>()
            {
                {"RETURN_CNT", 0 },
                {"CUSTOMER_CNT", 0 },
                {"FUNCTION_CNT", 0 },
                {"PRODUCT_CNT", 0 },
                {"PART_CNT", 0 },
                {"USER_CNT", 0 },
                {"DESTROYED_CNT", 0 }
            };

            _dicCheckValuePast = new Dictionary<string, int>()
            {
                {"RETURN_CNT", 0 },
                {"CUSTOMER_CNT", 0 },
                {"FUNCTION_CNT", 0 },
                {"PRODUCT_CNT", 0 },
                {"PART_CNT", 0 },
                {"USER_CNT", 0 },
                {"DESTROYED_CNT", 0 }
            };

            _dicCheckNm = new Dictionary<string, string>()
            {
                {"반품교환수","RETURN_CNT"},
                {"CUSTOMER","CUSTOMER_CNT"},
                {"기능불량","FUNCTION_CNT"},
                {"제품불량","PRODUCT_CNT"},
                {"부품불량","PART_CNT"},
                {"작업자실수","USER_CNT"},
                {"파손","DESTROYED_CNT"}
            };

            _dicColCode = new Dictionary<string, string>()
            {
                {"CUSTOMER", "CD090801" },
                {"FUNCTION", "CD090802" },
                {"PRODUCT", "CD090803" },
                {"PART", "CD090804" },
                {"USER", "CD090805" },
                {"DESTROYED", "CD090806" }
            };

            _dicCheckCodeCnt = new Dictionary<string, int>()
            {
                { "CD090801", 0 },
                { "CD090802", 0 },
                { "CD090803", 0 },
                { "CD090804", 0 },
                { "CD090805", 0 },
                { "CD090806", 0 }
            };

            _realtime = true;

            _initialize = true;

            _bsModel = new BindingSource();

            _listCheckedCompany = new List<long>();
            _listCheckedProductType = new List<int>();
            _listCheckedContent = new List<string>();
            _listCheckedColNm = new List<string>();
            _dicCheckContentValue = new Dictionary<string, int>();
            _dicCheckContentValuePast = new Dictionary<string, int>();


        }

        private void usrWarehouseState_Load(object sender, EventArgs e)
        {
            setIInitData();

            ccCheckStatistics.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ccCheckStatistics.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)ccCheckStatistics.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)ccCheckStatistics.Diagram).AxisY.AutoScaleBreaks.Enabled = true;
            //((XYDiagram)ccCheckStatistics.Diagram).AxisY.AutoScaleBreaks.MaxCount = 3;
            ((XYDiagram)ccCheckStatistics.Diagram).AxisY.NumericScaleOptions.AutoGrid = false;
            ((XYDiagram)ccCheckStatistics.Diagram).AxisY.NumericScaleOptions.GridSpacing = 1;
            ((XYDiagram)ccCheckStatistics.Diagram).AxisY.NumericScaleOptions.GridOffset = 0;

            ((XYDiagram)ccCheckStatistics.Diagram).AxisX.Label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // Rotate the diagram (if necessary).
            ((XYDiagram)ccCheckStatistics.Diagram).Rotated = false;

            // Add a title to the chart (if necessary).
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "생산대행 반품 통계";
            ccCheckStatistics.Titles.Add(chartTitle1);

            // Add the chart to the form.
            ccCheckStatistics.Dock = DockStyle.Fill;
            //this.Controls.Add(ccCheckStatistics);

            ccCheckStatisticsDetail.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ccCheckStatisticsDetail.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)ccCheckStatisticsDetail.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)ccCheckStatisticsDetail.Diagram).AxisY.AutoScaleBreaks.Enabled = true;
            //((XYDiagram)ccCheckStatistics.Diagram).AxisY.AutoScaleBreaks.MaxCount = 3;
            ((XYDiagram)ccCheckStatisticsDetail.Diagram).AxisY.NumericScaleOptions.AutoGrid = false;
            ((XYDiagram)ccCheckStatisticsDetail.Diagram).AxisY.NumericScaleOptions.GridSpacing = 1;
            ((XYDiagram)ccCheckStatisticsDetail.Diagram).AxisY.NumericScaleOptions.GridOffset = 0;

            ((XYDiagram)ccCheckStatisticsDetail.Diagram).AxisX.Label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // Rotate the diagram (if necessary).
            ((XYDiagram)ccCheckStatisticsDetail.Diagram).Rotated = false;

            // Add a title to the chart (if necessary).
            ChartTitle chartTitle2 = new ChartTitle();
            chartTitle2.Text = "생산대행 반품 상세항목 통계";
            ccCheckStatisticsDetail.Titles.Add(chartTitle2);

            // Add the chart to the form.
            ccCheckStatisticsDetail.Dock = DockStyle.Fill;
            //this.Controls.Add(ccCheckStatistics);


            int pastDay = -15;
            var today = DateTime.Today;
            var pastDate = today.AddDays(pastDay);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;

            //dePastDtFrom.EditValue = pastDate;
            //dePastDtTo.EditValue = today;

            dePastDtFrom.EditValue = today.AddDays(-30);
            dePastDtTo.EditValue = today.AddDays(pastDay-1);

        }


        private void setIInitData()
        {
            _dtReturnCheck = Util.getTable("TCSY_CODE_CLS", "CODE_CLS, CODE_CLS_NM", "UP_CODE_CLS = 'CD0908' AND USE_YN = 'Y'", "SORT_SEQ ASC");

            foreach (DataRow row in _dtReturnCheck.Rows)
                _listCheckedColNm.Add(ConvertUtil.ToString(row["CODE_CLS"]));

            _dtReturnCheckDetail = Util.getTable("TCSY_CODE_LIST", "CODE_CLS, CODE_CD, CODE_NM", $"CODE_CLS IN ('{string.Join("', '", _listCheckedColNm)}') AND USE_YN = 'Y'", "SORT_SEQ ASC");

            foreach (DataRow row in _dtReturnCheckDetail.Rows)
            {
                DataRow dr1 = _dtReturnCheckContent.NewRow();
                dr1["CODE"] = row["CODE_CLS"];
                dr1["KEY"] = ConvertUtil.ToInt32(row["CODE_CD"]);
                dr1["CONTENT"] = ConvertUtil.ToString(row["CODE_NM"]);
                dr1["CNT"] = 0;
                _dtReturnCheckContent.Rows.Add(dr1);

                DataRow dr2 = _dtReturnCheckContentPast.NewRow();
                dr2["CODE"] = row["CODE_CLS"];
                dr2["KEY"] = ConvertUtil.ToInt32(row["CODE_CD"]);
                dr2["CONTENT"] = ConvertUtil.ToString(row["CODE_NM"]);
                dr2["CNT"] = 0;
                _dtReturnCheckContentPast.Rows.Add(dr2);


                
            }
            DataRow[] rows;
            foreach(string code in _listCheckedColNm)
            {
                rows = _dtReturnCheckDetail.Select($"CODE_CLS = '{code}'");
                _dicCheckCodeCnt[code] = rows.Length;
            }
            




            DataTable dtStatisticsType= new DataTable();
            dtStatisticsType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtStatisticsType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtStatisticsType, 2, "기간선택");
            Util.insertRowonTop(dtStatisticsType, 1, "실시간");

            Util.LookupEditHelper(leStatisticsType, dtStatisticsType, "KEY", "VALUE");


            DataTable dtCheckType = new DataTable();
            dtCheckType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtCheckType.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtCheckType, 3, "리페어");
            Util.insertRowonTop(dtCheckType, 2, "검수");
            Util.insertRowonTop(dtCheckType, 1, "전체");

            _dtModel = Util.getTable("TN_MODEL_LIST", "MODEL_LIST_ID, COMPANY_ID, MODEL_NM, PC_TYPE", "DEL_YN = 'N'", "MODEL_LIST_ID ASC");
            _bsModel.DataSource = _dtModel;
            Util.LookupEditHelper(ccbModel, _bsModel, "MODEL_LIST_ID", "MODEL_NM");

            DataTable dtProductType = Util.getCodeList("CD0903", "KEY", "VALUE");
            Util.LookupEditHelper(ccbeProductType, dtProductType, "KEY", "VALUE");


            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "COMPANY_TYPE = 'C'", "COMPANY_NM ASC");
            Util.LookupEditHelper(ccbeCompany, dtCompany, "KEY", "VALUE");

            Util.LookupEditHelper(ccbContent, _dtReturnCheck, "CODE_CLS", "CODE_CLS_NM");
            
            //Util.LookupEditHelper(leCheckType, dtCheckType, "KEY", "VALUE");

            leStatisticsType.EditValue = 2;
            //leCheckType.EditValue = 1;

        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            _initialize = false;

            if (_realtime)
            {
                deDtTo.EditValue = DateTime.Today;

                if (!NtbCheckTimer.Enabled)
                    NtbCheckTimer.Enabled = true;
            }
            else
                NtbCheckTimer.Enabled = false;

            Dangol.ShowSplash();
            getList();
            DrawChart();
            //if(ceShowDetail.CheckState == CheckState.Checked)
                DrawChartContent();


            Dangol.CloseSplash();
        }

        private bool checkSearch(ref JObject jData)
        {

            string dtFrom = "";
            string dtTo = "";

            int date = 0;
            if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
            {
                dtFrom = $"{deDtFrom.Text} 00:00:00";
                date++;
            }

            if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
            {
                dtTo = $"{deDtTo.Text} 23:59:59";
                date++;
            }

            if (date == 2)
            {
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

                if (diffDay > 180)
                {
                    jData.Add("MSG", "검색 기간은 6개월(180일)을 초과할 수 없습니다.");
                    return false;
                }

                jData.Add("RETURN_COMPLETE_DT_S", dtFrom);
                jData.Add("RETURN_COMPLETE_DT_E", dtTo);
            }
            else
            {
                date = 0;
            }


            string dtPastFrom = "";
            string dtPastTo = "";
            date = 0;
            if (dePastDtFrom.EditValue != null && !string.IsNullOrEmpty(dePastDtFrom.EditValue.ToString()))
            {
                dtPastFrom = $"{dePastDtFrom.Text} 00:00:00";
                date++;
            }

            if (dePastDtTo.EditValue != null && !string.IsNullOrEmpty(dePastDtTo.EditValue.ToString()))
            {
                dtPastTo = $"{dePastDtTo.Text} 23:59:59";
                date++;
            }

            if (date == 2)
            {
                DateTime dtfrom;
                DateTime dtto;
                dtfrom = Convert.ToDateTime(dtPastFrom);
                dtto = Convert.ToDateTime(dtPastTo);

                int result = DateTime.Compare(dtfrom, dtto);

                if (result > 0)
                {
                    jData.Add("MSG", "종료날짜는 시작날짜보다 커야합니다(과거).");
                    return false;
                }

                TimeSpan TS = dtto - dtfrom;
                int diffDay = TS.Days;

                if (diffDay > 180)
                {
                    jData.Add("MSG", "검색 기간은 6개월(180일)을 초과할 수 없습니다(과거).");
                    return false;
                }

                jData.Add("RETURN_COMPLETE_PAST_DT_S", dtPastFrom);
                jData.Add("RETURN_COMPLETE_PAST_DT_E", dtPastTo);
            }
            else
            {
                date = 0;
            }

            if (_listCheckedCompany.Count > 0)
                jData.Add("LIST_COMPANY_ID", string.Join(",", _listCheckedCompany));

            if (_listCheckedProductType.Count > 0)
                jData.Add("LIST_PC_TYPE", string.Join(",", _listCheckedProductType));


            List<long> listModelId = new List<long>();
            int count = ccbModel.Properties.Items.Count;
            for (int i = 0; i < count; i++)
                if (ccbModel.Properties.Items[i].CheckState == CheckState.Checked)
                    listModelId.Add(ConvertUtil.ToInt64(ccbModel.Properties.Items[i].Value));

            if (listModelId.Count > 0)
                jData.Add("LIST_MODEL_ID", string.Join(",", listModelId));

            jData.Add("CHECK_TYPE", 2);

            return true;
        }


        private bool getList()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();


            if (!checkSearch(ref jData))
            {
                Dangol.Message(jData["MSG"]);
                return false;
            }

            //_dtCheck.Clear();
            //_dtQC.Clear();


            foreach (string col in _listCheckedCol)
            {
                _dicCheckValue[col] = 0;
                _dicCheckValuePast[col] = 0;
            }

            foreach (DataRow col in _dtReturnCheckContent.Rows)
                col["CNT"] = 0;

            foreach (DataRow col in _dtReturnCheckContentPast.Rows)
                col["CNT"] = 0;


            if (DBStatistics.getConsignedReturnCheckStatistics(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JObject jCheckData = (JObject)jResult["DATA"];
                    foreach (string col in _listCheckedCol)
                    {
                        if (jCheckData.ContainsKey(col))
                            _dicCheckValue[col] = ConvertUtil.ToInt32(jCheckData[col]);
                    }

                    if (Convert.ToBoolean(jResult["CHECKDATA_EXIST"]))
                    {
                        JArray jArray = JArray.Parse(jResult["CHECKDATA"].ToString());

                        int value;
                        int cnt;
                        string code;
                        string col;
                        DataRow[] rows;

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            foreach (KeyValuePair<string, string> item in _dicColCode)
                            {
                                col = item.Key;
                                code = item.Value;

                                value = ConvertUtil.ToInt32(obj[col]);

                                cnt = _dicCheckCodeCnt[code];

                                for (int i = 0; i < cnt; i++)
                                {
                                    if ((value & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                    {
                                        rows = _dtReturnCheckContent.Select($"CODE = '{code}' AND KEY = {ExamineInfo._BASE[i]}");

                                        if (rows.Length > 0)
                                        {
                                            foreach (DataRow row in rows)
                                            {
                                                row["CNT"] = ConvertUtil.ToInt32(row["CNT"]) + 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Convert.ToBoolean(jResult["DATA_PAST_EXIST"]))
                {
                    JObject jCheckData = (JObject)jResult["PAST_DATA"];
                    foreach (string col in _listCheckedCol)
                    {
                        if (jCheckData.ContainsKey(col))
                            _dicCheckValuePast[col] = ConvertUtil.ToInt32(jCheckData[col]);
                    }

                    if (Convert.ToBoolean(jResult["CHECKDATA_PAST_EXIST"]))
                    {
                        JArray jArray = JArray.Parse(jResult["PAST_CHECKDATA"].ToString());

                        int value;
                        int cnt;
                        string code;
                        string col;
                        DataRow[] rows;

                        foreach (JObject obj in jArray.Children<JObject>())
                        {
                            foreach (KeyValuePair<string, string> item in _dicColCode)
                            {
                                col = item.Key;
                                code = item.Value;

                                value = ConvertUtil.ToInt32(obj[col]);

                                cnt = _dicCheckCodeCnt[code];

                                for (int i = 0; i < cnt; i++)
                                {
                                    if ((value & ExamineInfo._BASE[i]) == ExamineInfo._BASE[i])
                                    {
                                        rows = _dtReturnCheckContentPast.Select($"CODE = '{code}' AND KEY = {ExamineInfo._BASE[i]}");

                                        if (rows.Length > 0)
                                        {
                                            foreach (DataRow row in rows)
                                            {
                                                row["CNT"] = ConvertUtil.ToInt32(row["CNT"]) + 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }



                return true;

            }
            else
            {
                return false;
            }
        }

        private void setProductCheckData(JArray jArray)
        {
            long inventoryId;
            string qcState;
            List<long> listInventoryId = new List<long>();
            int qcPassCnt = 0;
            int qcFailCnt = 0;

            foreach (JObject obj in jArray.Children<JObject>())
            {
                inventoryId = ConvertUtil.ToInt64(obj["INVENTORY_ID"]);
                qcState = ConvertUtil.ToString(obj["QC_STATE"]);

                if (!listInventoryId.Contains(inventoryId))
                {
                    listInventoryId.Add(inventoryId);

                    if (qcState.Equals("P"))
                        qcPassCnt++;
                    else if (qcState.Equals("F"))
                        qcFailCnt++;
                }
            }

            _dicCheckValue["CHECK_CNT"] = listInventoryId.Count;
            _dicCheckValue["QC_CNT"] = qcPassCnt + qcFailCnt;
            _dicCheckValue["QC_PASS_CNT"] = qcPassCnt;
            _dicCheckValue["QC_FAIL_CNT"] = qcFailCnt;

            setCheckInfo(listInventoryId);
        }

        private void setCheckInfo(List<long> listInventoryId)
        {
            int checkValue = 0;
            int examCheckValue = 0;
            bool caseCheck = false;

            //Dictionary<string, int>  listCaseCheckCol = new Dictionary<string, int>()
            //{
            //    {"CASE_DESTROYED", 0 },
            //    {"CASE_SCRATCH", 0 },
            //    {"CASE_STABBED", 0 },
            //    {"CASE_PRESSED", 0 },
            //    {"CASE_DISCOLORED", 0 }
            //};

            foreach (long inventoryId in listInventoryId)
            {
                caseCheck = false;

                DataRow[] rowsCheck = _dtCheck.Select($"INVENTORY_ID = {inventoryId}");
                DataRow[] rowsQCCheck = _dtQC.Select($"INVENTORY_ID = {inventoryId}");

                if (rowsCheck.Length > 0 && rowsQCCheck.Length > 0)
                {
                    DataRow rowCheck = rowsCheck[0];
                    DataRow rowQCCheck = rowsQCCheck[0];

                    if (!ConvertUtil.ToString(rowCheck["CASE_DES"]).Equals(ConvertUtil.ToString(rowQCCheck["CASE_DES"])))
                        _dicCheckValue["ETC"]++;

                    if (ConvertUtil.ToInt32(rowCheck["CASE_HINGE"]) != ConvertUtil.ToInt32(rowQCCheck["CASE_HINGE"]))
                        _dicCheckValue["CASE_HINGE"]++;


                    foreach (string col in ExamineInfo._NTBCOLNAME2ND)
                    {
                        examCheckValue = ConvertUtil.ToInt32(rowCheck[col]);
                        checkValue = ConvertUtil.ToInt32(rowQCCheck[col]);

                        if (examCheckValue != checkValue && checkValue > 0)
                        {
                            if (ExamineInfo._listCaseCheckCol.Contains(col))
                                caseCheck = true;
                            else
                                _dicCheckValue[col]++;
                        }
                    }
                    if(caseCheck)
                        _dicCheckValue["CASES"]++;
                }
            }
        }


        private void DrawChart()
        {
            // Create an empty chart.

            ccCheckStatistics.Series.Clear();

                // Create the first side-by-side bar series and add points to it.
            Series series1 = new Series("반품체크", ViewType.Bar);
            foreach(KeyValuePair<string, string> item in _dicCheckNm)
            {
                series1.Points.Add(new SeriesPoint(item.Key, _dicCheckValue[_dicCheckNm[item.Key]]));
            }
     
            series1.ArgumentScaleType = ScaleType.Qualitative;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            // Create the second side-by-side bar series and add points to it.
            //Series series2 = new Series("Side-by-Side Bar Series 2", ViewType.Bar);
            //series2.Points.Add(new SeriesPoint("A", 15));
            //series2.Points.Add(new SeriesPoint("B", 18));
            //series2.Points.Add(new SeriesPoint("C", 25));
            //series2.Points.Add(new SeriesPoint("D", 33));

            // Add the series to the chart.
            ccCheckStatistics.Series.Add(series1);

            Series series2 = new Series("반품체크과거", ViewType.Bar);

            if (ceShowPast.CheckState == CheckState.Checked)
            {         
                foreach (KeyValuePair<string, string> item in _dicCheckNm)
                {
                    series2.Points.Add(new SeriesPoint(item.Key, _dicCheckValuePast[_dicCheckNm[item.Key]]));
                }

                series2.ArgumentScaleType = ScaleType.Qualitative;
                series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                (series2.View as SideBySideBarSeriesView).FillStyle.FillMode = FillMode.Hatch;
                HatchFillOptions options = (series2.View as SideBySideBarSeriesView).FillStyle.Options as HatchFillOptions;
                //options.Color2 = Color.Red;
                options.HatchStyle = System.Drawing.Drawing2D.HatchStyle.DiagonalCross;

                ccCheckStatistics.Series.Add(series2);
            }

            SideBySideBarSeriesLabel label = ccCheckStatistics.Series[0].Label as SideBySideBarSeriesLabel;
            if (label != null)
                label.Position = BarSeriesLabelPosition.Top;
            label.Font = new Font("Tahoma", 20);

            // Set some properties to get a nice-looking chart.
            ((SideBySideBarSeriesView)series1.View).ColorEach = true;

            if (ceShowPast.CheckState == CheckState.Checked)
            {
                SideBySideBarSeriesLabel label2 = ccCheckStatistics.Series[1].Label as SideBySideBarSeriesLabel;
                if (label2 != null)
                    label2.Position = BarSeriesLabelPosition.Top;
                label2.Font = new Font("Tahoma", 20);

                ((SideBySideBarSeriesView)series2.View).ColorEach = true;

            }
        }

        private void DrawChartContent()
        {
            // Create an empty chart.

            ccCheckStatisticsDetail.Series.Clear();
            _dicCheckContentValue.Clear();
            _dicCheckContentValuePast.Clear();
            // Create the first side-by-side bar series and add points to it.
            Series series1 = new Series("반품체크", ViewType.Bar);


            DataRow[] rows = _dtReturnCheckContent.Select($"CODE IN('{string.Join("', '", _listCheckedContent)}')");
            int cnt;
            string content;
            foreach(DataRow row in rows)
            {
                content = ConvertUtil.ToString(row["CONTENT"]);
                cnt = ConvertUtil.ToInt32(row["CNT"]);

                if (_dicCheckContentValue.ContainsKey(content))
                    _dicCheckContentValue[content] += cnt;
                else
                    _dicCheckContentValue.Add(content, cnt);
            }


            foreach (KeyValuePair<string, int> item in _dicCheckContentValue)
            {
                series1.Points.Add(new SeriesPoint(item.Key, item.Value));
            }

            series1.ArgumentScaleType = ScaleType.Qualitative;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            // Create the second side-by-side bar series and add points to it.
            //Series series2 = new Series("Side-by-Side Bar Series 2", ViewType.Bar);
            //series2.Points.Add(new SeriesPoint("A", 15));
            //series2.Points.Add(new SeriesPoint("B", 18));
            //series2.Points.Add(new SeriesPoint("C", 25));
            //series2.Points.Add(new SeriesPoint("D", 33));

            // Add the series to the chart.
            ccCheckStatisticsDetail.Series.Add(series1);

            Series series2 = new Series("반품체크과거", ViewType.Bar);

            if (ceShowPast.CheckState == CheckState.Checked)
            {
                rows = _dtReturnCheckContentPast.Select($"CODE IN('{string.Join("', '", _listCheckedContent)}')");
                foreach (DataRow row in rows)
                {
                    content = ConvertUtil.ToString(row["CONTENT"]);
                    cnt = ConvertUtil.ToInt32(row["CNT"]);

                    if (_dicCheckContentValuePast.ContainsKey(content))
                        _dicCheckContentValuePast[content] += cnt;
                    else
                        _dicCheckContentValuePast.Add(content, cnt);
                }


                foreach (KeyValuePair<string, int> item in _dicCheckContentValuePast)
                {
                    series2.Points.Add(new SeriesPoint(item.Key, item.Value));
                }

                series2.ArgumentScaleType = ScaleType.Qualitative;
                series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                (series2.View as SideBySideBarSeriesView).FillStyle.FillMode = FillMode.Hatch;
                HatchFillOptions options = (series2.View as SideBySideBarSeriesView).FillStyle.Options as HatchFillOptions;
                //options.Color2 = Color.Red;
                options.HatchStyle = System.Drawing.Drawing2D.HatchStyle.DiagonalCross;
                // Add the series to the chart.
                ccCheckStatisticsDetail.Series.Add(series2);
            }

            SideBySideBarSeriesLabel label = ccCheckStatisticsDetail.Series[0].Label as SideBySideBarSeriesLabel;
            if (label != null)
                label.Position = BarSeriesLabelPosition.Top;
            label.Font = new Font("Tahoma", 20);

            ((SideBySideBarSeriesView)series1.View).ColorEach = true;

            if (ceShowPast.CheckState == CheckState.Checked)
            {
                SideBySideBarSeriesLabel label2 = ccCheckStatisticsDetail.Series[1].Label as SideBySideBarSeriesLabel;
                if (label2 != null)
                    label2.Position = BarSeriesLabelPosition.Top;
                label2.Font = new Font("Tahoma", 20);

                ((SideBySideBarSeriesView)series2.View).ColorEach = true;
            }


            // Set some properties to get a nice-looking chart.
            


        }

        private void leStatisticsType_EditValueChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ToInt32(leStatisticsType.EditValue) == 1)
            {
                deDtTo.EditValue = DateTime.Today;
                deDtTo.ReadOnly = true;
                _realtime = true;
            }
            else
            {
                deDtTo.ReadOnly = false;
                _realtime = false;
            }
        }

        private void NtbCheckTimer_Tick(object sender, EventArgs e)
        {
            //Dangol.ShowSplash();
            getList();
            DrawChart();
            //Thread.Sleep(1000);
            //Dangol.CloseSplash();

        }

        private void usrNtbCheckStatistics_Enter(object sender, EventArgs e)
        {
            if (!_initialize)
            {
                if (_realtime)
                    NtbCheckTimer.Enabled = true;
                else
                    NtbCheckTimer.Enabled = false;
            }
        }

        private void usrNtbCheckStatistics_Leave(object sender, EventArgs e)
        {
            NtbCheckTimer.Enabled = false;
        }

        private void ccbeCompany_EditValueChanged(object sender, EventArgs e)
        {
            _listCheckedCompany.Clear();

            int count = ccbeCompany.Properties.Items.Count;
            for (int i = 0; i < count; i++)
                if (ccbeCompany.Properties.Items[i].CheckState == CheckState.Checked)
                    _listCheckedCompany.Add(ConvertUtil.ToInt64(ccbeCompany.Properties.Items[i].Value));

            setModelFilter();
        }

        private void ccbeProductType_EditValueChanged(object sender, EventArgs e)
        {
            _listCheckedProductType.Clear();

            int count = ccbeProductType.Properties.Items.Count;
            for (int i = 0; i < count; i++)
                if (ccbeProductType.Properties.Items[i].CheckState == CheckState.Checked)
                    _listCheckedProductType.Add(ConvertUtil.ToInt32(ccbeProductType.Properties.Items[i].Value));

            setModelFilter();
        }

        private void setModelFilter()
        {
            if (_listCheckedCompany.Count < 1 && _listCheckedProductType.Count < 1)
                _bsModel.Filter = "MODEL_LIST_ID = -1";
            else if(_listCheckedCompany.Count < 1)
                _bsModel.Filter = $"PC_TYPE IN ('{string.Join("','", _listCheckedProductType)}')";
            else if (_listCheckedProductType.Count < 1)
                _bsModel.Filter = $"COMPANY_ID IN ('{string.Join("','", _listCheckedCompany)}')";
            else
                _bsModel.Filter = $"COMPANY_ID IN ('{string.Join("','", _listCheckedCompany)}') AND PC_TYPE IN ('{string.Join("','", _listCheckedProductType)}')";

            if (_bsModel.Count < 7)
                ccbModel.Properties.DropDownRows = _bsModel.Count+1;
            else
                ccbModel.Properties.DropDownRows = 8;
        }

        private void ceShowDetail_CheckedChanged(object sender, EventArgs e)
        {
            if(ceShowDetail.CheckState == CheckState.Checked)
            {
                lcConsignedCheckGraph.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcConsignedCheckGraphDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcContent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcConsignedCheckGraph.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcConsignedCheckGraphDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcContent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void ccbContent_EditValueChanged(object sender, EventArgs e)
        {
            _listCheckedContent.Clear();

            int count = ccbContent.Properties.Items.Count;
            for (int i = 0; i < count; i++)
                if (ccbContent.Properties.Items[i].CheckState == CheckState.Checked)
                    _listCheckedContent.Add(ConvertUtil.ToString(ccbContent.Properties.Items[i].Value));

            DrawChartContent();
        }

        private void ceShowPast_CheckedChanged(object sender, EventArgs e)
        {
            DrawChart();
            DrawChartContent();
        }

        private void ccCheckStatistics_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.Series.Name == "check statistics past")
            {
                //BarDrawOptions barDrawOptions = (BarDrawOptions)e.SeriesDrawOptions;
                //HatchFillOptions fillOptions = ((BarDrawOptions)e.SeriesDrawOptions).FillStyle.Options as HatchFillOptions;
                //barDrawOptions.FillStyle.FillMode = FillMode.Hatch;
                //Color color2 = barDrawOptions.Color;
                //fillOptions.Color2 = color2;
                //barDrawOptions.Color = Color.White;
            }
        }

        private void ccCheckStatisticsDetail_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            //if (e.Series.Name == "Series2")
            //{
            //    BarDrawOptions barDrawOptions = (BarDrawOptions)e.SeriesDrawOptions;
            //    HatchFillOptions fillOptions = ((BarDrawOptions)e.SeriesDrawOptions).FillStyle.Options as HatchFillOptions;
            //    barDrawOptions.FillStyle.FillMode = FillMode.Hatch;
            //    Color color2 = barDrawOptions.Color;
            //    fillOptions.Color2 = color2;
            //    barDrawOptions.Color = Color.White;
            //}
        }

        
    }
}