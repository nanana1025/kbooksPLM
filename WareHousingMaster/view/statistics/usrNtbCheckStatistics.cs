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
    public partial class usrNtbCheckStatistics : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtStatistics;

        DataTable _dtCheck;
        DataTable _dtQC;


        Dictionary<string, int> _dicCheckValue;

        List<string> _listRemainCol;

        bool _realtime;

        int _interval = 10000;

        bool _initialize;

        public usrNtbCheckStatistics()
        {
            InitializeComponent();

            NtbCheckTimer.Interval = _interval;

            //warehouseTimer.Interval = interval;

            //long _warehouseMovementId = -1;

            _dicCheckValue = new Dictionary<string, int>()
            {
                {"CHECK_CNT", 0 },
                {"QC_CNT", 0 },
                {"QC_PASS_CNT", 0 },
                {"QC_FAIL_CNT", 0 },
                {"CASES", 0 },
                {"CASE_HINGE", 0 },
                {"DISPLAY",0 },
                {"BIOS",0 },
                {"USB",0 },
                {"KEYBOARD",0 },
                {"BATTERY",0 },
                {"LAN_WIRELESS",0 },
                {"ETC",0},
                {"MOUSEPAD",0},
                {"CAM",0},
                {"ODD",0},
                {"HDD",0},
                {"LAN_WIRED",0},
                {"OS",0},
                {"TEST_CHECK",0}
            };

            _listRemainCol = new List<string>(new string[] { "CHECK_CNT", "QC_CNT", "QC_PASS_CNT", "QC_FAIL_CNT", "CASE_HINGE" });

            _dtCheck = new DataTable();
            _dtCheck.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtCheck.Columns.Add(new DataColumn("CHECK_TYPE", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtCheck.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            _dtCheck.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtCheck.Columns.Add(new DataColumn("UPDATE_USER_ID", typeof(string)));

            _dtQC = new DataTable();
            _dtQC.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtQC.Columns.Add(new DataColumn("CHECK_YN", typeof(bool)));
            _dtQC.Columns.Add(new DataColumn("PRODUCT_GRADE", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("CASE_DESTROYED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_SCRATCH", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_STABBED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_PRESSED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_DISCOLORED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_HINGE", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CASE_DES", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("DISPLAY", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("USB", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("MOUSEPAD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("KEYBOARD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("BATTERY", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("BATTERY_REMAIN", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("CAM", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("LAN_WIRELESS", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("LAN_WIRED", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("ODD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("HDD", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("BIOS", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("OS", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("TEST_CHECK", typeof(int)));
            _dtQC.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("UPDATE_DT", typeof(string)));
            _dtQC.Columns.Add(new DataColumn("UPDATE_USER_ID", typeof(string)));

            _realtime = true;

            _initialize = true;

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
            chartTitle1.Text = "검수 통계 현황";
            ccCheckStatistics.Titles.Add(chartTitle1);

            // Add the chart to the form.
            ccCheckStatistics.Dock = DockStyle.Fill;
            //this.Controls.Add(ccCheckStatistics);

           






            var today = DateTime.Today;
            var pastDate = today.AddDays(-15);

            deDtFrom.EditValue = pastDate;
            deDtTo.EditValue = today;
        }


        private void setIInitData()
        {
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

            Util.LookupEditHelper(leCheckType, dtCheckType, "KEY", "VALUE");

            leStatisticsType.EditValue = 2;
            leCheckType.EditValue = 1;

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

                jData.Add("CREATE_DT_S", dtFrom);
                jData.Add("CREATE_DT_E", dtTo);
            }
            else
            {
                date = 0;
            }

            if (ConvertUtil.ToInt32(leCheckType.EditValue) > 0)
            {
                int checkType = ConvertUtil.ToInt32(leCheckType.EditValue);

                jData.Add("CHECK_TYPE", ConvertUtil.ToInt32(leCheckType.EditValue));
            }

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

            _dtCheck.Clear();
            _dtQC.Clear();

            //foreach (KeyValuePair<string, int> item in _dicCheckValue)
            //    _dicCheckValue[item.Key] = 0;

            foreach (string col in ExamineInfo._listAdjustmentPriceColShort)
                _dicCheckValue[col] = 0;

            foreach (string col in _listRemainCol)
                _dicCheckValue[col] = 0;
            

            if (DBStatistics.getCheckStatistics(jData, ref jResult))
            {
                if (Convert.ToBoolean(jResult["CHECKDATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["CHECKDATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                        DataRow dr = _dtCheck.NewRow();

                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        //dr["CHECK_YN"] = checkYn;
                        //if (checkYn)
                        {
                            dr["CHECK_TYPE"] = ConvertUtil.ToInt32(obj["CHECK_TYPE"]);
                            dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];
                            dr["CASE_DESTROYED"] = obj["CASE_DESTROYED"];
                            dr["CASE_SCRATCH"] = obj["CASE_SCRATCH"];
                            dr["CASE_STABBED"] = obj["CASE_STABBED"];
                            dr["CASE_PRESSED"] = obj["CASE_PRESSED"];
                            dr["CASE_DISCOLORED"] = obj["CASE_DISCOLORED"];
                            dr["CASE_HINGE"] = obj["CASE_HINGE"];
                            dr["CASE_DES"] = obj["CASE_DES"];

                            dr["DISPLAY"] = obj["DISPLAY"];
                            dr["USB"] = obj["USB"];
                            dr["MOUSEPAD"] = obj["MOUSEPAD"];
                            dr["KEYBOARD"] = obj["KEYBOARD"];
                            dr["BATTERY"] = obj["BATTERY"];
                            dr["BATTERY_REMAIN"] = obj["BATTERY_REMAIN"];
                            dr["CAM"] = obj["CAM"];
                            dr["LAN_WIRELESS"] = obj["LAN_WIRELESS"];
                            dr["LAN_WIRED"] = obj["LAN_WIRED"];
                            dr["ODD"] = obj["ODD"];
                            dr["HDD"] = obj["HDD"];
                            dr["BIOS"] = obj["BIOS"];
                            dr["OS"] = obj["OS"];
                            dr["TEST_CHECK"] = obj["TEST_CHECK"];
                            dr["CREATE_DT"] = obj["CREATE_DT"];
                            dr["UPDATE_DT"] = obj["UPDATE_DT"];
                            dr["UPDATE_USER_ID"] = obj["UPDATE_USER_ID"];
                        }
                        _dtCheck.Rows.Add(dr);
                    }
                }

                if (Convert.ToBoolean(jResult["QCDATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["QCDATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        //bool checkYn = ConvertUtil.ToBoolean(obj["CHECK_YN"]);
                        DataRow dr = _dtQC.NewRow();

                        dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                        //dr["CHECK_YN"] = checkYn;
                        //if (checkYn)
                        {
                            dr["PRODUCT_GRADE"] = obj["PRODUCT_GRADE"];
                            dr["CASE_DESTROYED"] = obj["CASE_DESTROYED"];
                            dr["CASE_SCRATCH"] = obj["CASE_SCRATCH"];
                            dr["CASE_STABBED"] = obj["CASE_STABBED"];
                            dr["CASE_PRESSED"] = obj["CASE_PRESSED"];
                            dr["CASE_DISCOLORED"] = obj["CASE_DISCOLORED"];
                            dr["CASE_HINGE"] = obj["CASE_HINGE"];
                            dr["CASE_DES"] = obj["CASE_DES"];

                            dr["DISPLAY"] = obj["DISPLAY"];
                            dr["USB"] = obj["USB"];
                            dr["MOUSEPAD"] = obj["MOUSEPAD"];
                            dr["KEYBOARD"] = obj["KEYBOARD"];
                            dr["BATTERY"] = obj["BATTERY"];
                            dr["BATTERY_REMAIN"] = obj["BATTERY_REMAIN"];
                            dr["CAM"] = obj["CAM"];
                            dr["LAN_WIRELESS"] = obj["LAN_WIRELESS"];
                            dr["LAN_WIRED"] = obj["LAN_WIRED"];
                            dr["ODD"] = obj["ODD"];
                            dr["HDD"] = obj["HDD"];
                            dr["BIOS"] = obj["BIOS"];
                            dr["OS"] = obj["OS"];
                            dr["TEST_CHECK"] = obj["TEST_CHECK"];
                            dr["CREATE_DT"] = obj["CREATE_DT"];
                            dr["UPDATE_DT"] = obj["UPDATE_DT"];
                            dr["UPDATE_USER_ID"] = obj["UPDATE_USER_ID"];
                        }
                        _dtQC.Rows.Add(dr);
                    }
                }


                if (Convert.ToBoolean(jResult["DATA_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    setProductCheckData(jArray);
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
            Series series1 = new Series("check statistics", ViewType.Bar);
            series1.Points.Add(new SeriesPoint("검수", _dicCheckValue["CHECK_CNT"]));
            series1.Points.Add(new SeriesPoint("QC", _dicCheckValue["QC_CNT"]));
            series1.Points.Add(new SeriesPoint("P", _dicCheckValue["QC_PASS_CNT"]));
            series1.Points.Add(new SeriesPoint("F", _dicCheckValue["QC_FAIL_CNT"]));

            series1.Points.Add(new SeriesPoint("케이스", _dicCheckValue["CASES"]));
            series1.Points.Add(new SeriesPoint("힌지", _dicCheckValue["CASE_HINGE"]));
            series1.Points.Add(new SeriesPoint("액정", _dicCheckValue["DISPLAY"]));
            series1.Points.Add(new SeriesPoint("USB", _dicCheckValue["USB"]));
            series1.Points.Add(new SeriesPoint("패스워드", _dicCheckValue["BIOS"]));
            series1.Points.Add(new SeriesPoint("키보드", _dicCheckValue["KEYBOARD"]));
            series1.Points.Add(new SeriesPoint("배터리", _dicCheckValue["BATTERY"]));
            series1.Points.Add(new SeriesPoint("무선랜", _dicCheckValue["LAN_WIRELESS"]));
            series1.Points.Add(new SeriesPoint("유선랜", _dicCheckValue["LAN_WIRED"]));
            series1.Points.Add(new SeriesPoint("마우스패드", _dicCheckValue["MOUSEPAD"]));
            series1.Points.Add(new SeriesPoint("캠", _dicCheckValue["CAM"]));
            series1.Points.Add(new SeriesPoint("ODD", _dicCheckValue["ODD"]));
            series1.Points.Add(new SeriesPoint("storage", _dicCheckValue["HDD"]));              
            series1.Points.Add(new SeriesPoint("OS", _dicCheckValue["OS"]));
            series1.Points.Add(new SeriesPoint("기타", _dicCheckValue["ETC"]));


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

            SideBySideBarSeriesLabel label = ccCheckStatistics.Series[0].Label as SideBySideBarSeriesLabel;
            if (label != null)
                label.Position = BarSeriesLabelPosition.Top;
            label.Font = new Font("Tahoma", 20);


            // Set some properties to get a nice-looking chart.
            ((SideBySideBarSeriesView)series1.View).ColorEach = true;


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

        //private void sbSearch_Click_1(object sender, EventArgs e)
        //{
        //    setWarehouseState();
        //    //warehouseTimer.Enabled = true;
        //}

        //private void setWarehouseState()
        //{
        //    //usrHarnessDetail1.LoadData(harnessId, _harnessControl, _harnessPartControl, _harnessCostControl);

        //    int warehouseIndex = gvWarehouse.FocusedRowHandle;

        //    int palletIndex = gvPallet.FocusedRowHandle;
        //    int[] palletRow = gvPallet.GetSelectedRows();

        //    int componentCdIndex = gvComponentCd.FocusedRowHandle;
        //    int[] componentCdRow = gvComponentCd.GetSelectedRows();

        //    DataRow[] rows = _dtComponent.Select("CHECK = true");

        //    if (palletRow.Length <= 1 && palletIndex < 1 &&
        //    componentCdRow.Length <= 1 && componentCdIndex < 1
        //    )
        //    {
        //        string warehouseCurCnt = "0";
        //        string warehousePreCnt = "0";
        //        string state = "R";
        //        JObject jResult = new JObject();
        //        string query = $"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY WHERE LOCATION = {_currentRowWarehouse["KEY"]}";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    warehouseCurCnt = ConvertUtil.ToString(obj["CNT"]);
        //                }  
        //            }
        //        }


        //        query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, RELEASE_WAREHOUSE_ID, WAREHOUSING_WAREHOUSE_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE,
        //                                    RELEASE_WAREHOUSE_ID,
        //                                    WAREHOUSING_WAREHOUSE_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]} OR A.WAREHOUSING_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]})
        //                                    AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')

        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)";

        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                    string releaseWarehouseNo = ConvertUtil.ToString(obj["RELEASE_WAREHOUSE_ID"]);
        //                    string warehousingWarehouseNo = ConvertUtil.ToString(obj["WAREHOUSING_WAREHOUSE_ID"]);
        //                    string selectWarehousegNo = ConvertUtil.ToString(_currentRowWarehouse["KEY"]);
        //                    if (selectWarehousegNo.Equals(releaseWarehouseNo) && selectWarehousegNo.Equals(warehousingWarehouseNo))
        //                        warehousePreCnt = warehouseCurCnt;
        //                    else if(selectWarehousegNo.Equals(releaseWarehouseNo))
        //                        warehousePreCnt = ConvertUtil.ToString(ConvertUtil.ToInt32(warehouseCurCnt) + ConvertUtil.ToInt32(obj["CNT"]));
        //                    else
        //                        warehousePreCnt = ConvertUtil.ToString(ConvertUtil.ToInt32(warehouseCurCnt) - ConvertUtil.ToInt32(obj["CNT"]));                   
        //                }

        //            }
        //        }

        //        leWarehouseNo.EditValue = _currentRowWarehouse["VALUE"];
        //        lcPreCnt.Text = warehousePreCnt;
        //        lcCurCnt.Text = warehouseCurCnt;

        //        tcWarehouseState.SelectedTabPage = xtpWarehouse;
        //    }
        //    else if (palletIndex > 0 && palletRow.Length > 0 && componentCdRow.Length <= 1 && componentCdIndex <= 1)
        //    {
        //        List<string> lisPallet = new List<string>();
        //        string PalletCurCnt = "0";
        //        string PalletPreCnt = "0";
        //        string state = "R";
        //        for (int i = 0; i < palletRow.Length; i++)
        //        {
        //            if (palletRow[i] == 0)
        //                continue;

        //            DataRow drv = gvPallet.GetDataRow(palletRow[i]);
        //            lisPallet.Add(ConvertUtil.ToString(drv["PALLET_ID"]));
        //        }

        //        JObject jResult = new JObject();
        //        string query = $"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)})";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    PalletCurCnt = ConvertUtil.ToString(obj["CNT"]);
        //                }
        //            }
        //        }

        //        int preCnt = ConvertUtil.ToInt32(PalletCurCnt); 

        //        foreach (string pallet in lisPallet)
        //        {
        //            query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_PALLET_ID = {pallet} OR A.WAREHOUSING_PALLET_ID = {pallet}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')

        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)";

        //            if (DBConnect.queryDT(query, ref jResult))
        //            {
        //                if (Convert.ToBoolean(jResult["EXIST"]))
        //                {
        //                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                    foreach (JObject obj in jArray.Children<JObject>())
        //                    {
        //                        state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                        string releasePalletNo = ConvertUtil.ToString(obj["RELEASE_PALLET_ID"]);
        //                        string warehousingPalletNo = ConvertUtil.ToString(obj["WAREHOUSING_PALLET_ID"]);
        //                        if (pallet.Equals(releasePalletNo) && pallet.Equals(warehousingPalletNo))
        //                            preCnt += 0; // 여기 수정
        //                        else if (pallet.Equals(releasePalletNo))
        //                            preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                        else
        //                            preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                    }

        //                }
        //            }
        //        }

        //        PalletPreCnt = ConvertUtil.ToString(preCnt);

        //        query = $@"SELECT PALLET, COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                    WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)})
        //                    GROUP BY PALLET, COMPONENT_CD ORDER BY PALLET, COMPONENT_CD";
        //        _dtPalletList.Clear();
        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {  
        //                    DataRow dr = _dtPalletList.NewRow();
        //                    dr["PALLET"] = obj["PALLET"];
        //                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
        //                    dr["CNT"] = obj["CNT"];
        //                    _dtPalletList.Rows.Add(dr);

        //                }
        //            }
        //        }




        //        leWarehouseNo1.EditValue = _currentRowWarehouse["VALUE"];

        //        List<string> listPallet = new List<string>();
        //        foreach(string pallet in lisPallet)
        //        {
        //            listPallet.Add(_dicPallet[pallet]);
        //        }

        //        tePallet.Text = string.Join(",", listPallet);

        //        lcPrePallletCnt.Text = PalletPreCnt;
        //        lcCurPalletCnt.Text = PalletCurCnt;

        //        tcWarehouseState.SelectedTabPage = xtpPallet;
        //    }
        //    else if (componentCdRow.Length > 0 && componentCdIndex > 0 && rows.Length < 1)
        //    {
        //        int componentCdCurCnt = 0;
        //        int componentCdPreCnt = 0;
        //        string state = "R";
        //        bool isSelectPallet = false;
        //        string query = "";
        //        List<string> lisComponentCd = new List<string>();
        //        List<string> lisPallet = new List<string>();

        //        if (palletIndex > 0 && palletRow.Length > 0)
        //            isSelectPallet = true;

        //        if (isSelectPallet)
        //        {                    
        //            for (int i = 0; i < palletRow.Length; i++)
        //            {
        //                if (palletRow[i] == 0)
        //                    continue;
        //                DataRow drv = gvPallet.GetDataRow(palletRow[i]);
        //                lisPallet.Add(ConvertUtil.ToString(drv["PALLET_ID"]));
        //            }
        //        }

        //        for (int i = 0; i < componentCdRow.Length; i++)
        //        {
        //            if (componentCdRow[i] == 0)
        //                continue;
        //            DataRow drv = gvComponentCd.GetDataRow(componentCdRow[i]);
        //            lisComponentCd.Add(ConvertUtil.ToString(drv["KEY"]));
        //        }

        //        JObject jResult = new JObject();

        //        if (isSelectPallet)
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)}) AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";
        //        else
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    componentCdCurCnt = ConvertUtil.ToInt32(obj["CNT"]);
        //                }
        //            }
        //        }

        //        int preCnt = componentCdCurCnt;
        //        if (isSelectPallet)
        //        {
        //            foreach (string pallet in lisPallet)
        //            {

        //                query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_PALLET_ID = {pallet} OR A.WAREHOUSING_PALLET_ID = {pallet}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')

        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";

        //                if (DBConnect.queryDT(query, ref jResult))
        //                {
        //                    if (Convert.ToBoolean(jResult["EXIST"]))
        //                    {
        //                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                        foreach (JObject obj in jArray.Children<JObject>())
        //                        {
        //                            state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                            string releasePalletNo = ConvertUtil.ToString(obj["RELEASE_PALLET_ID"]);
        //                            string warehousingPalletNo = ConvertUtil.ToString(obj["WAREHOUSING_PALLET_ID"]);
        //                            if (pallet.Equals(releasePalletNo) && pallet.Equals(warehousingPalletNo))
        //                                preCnt += 0; // 여기 수정
        //                            else if (pallet.Equals(releasePalletNo))
        //                                preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                            else
        //                                preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]} OR A.WAREHOUSING_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')

        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')";

        //            if (DBConnect.queryDT(query, ref jResult))
        //            {
        //                if (Convert.ToBoolean(jResult["EXIST"]))
        //                {
        //                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                    foreach (JObject obj in jArray.Children<JObject>())
        //                    {
        //                        state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                        string releaseWarehouseNo = ConvertUtil.ToString(obj["RELEASE_WAREHOUSE_ID"]);
        //                        string warehousingWarehouseNo = ConvertUtil.ToString(obj["WAREHOUSING_WAREHOUSE_ID"]);
        //                        string selectWarehousegNo = ConvertUtil.ToString(_currentRowWarehouse["KEY"]);
        //                        if (releaseWarehouseNo.Equals(warehousingWarehouseNo))
        //                            preCnt += 0; // 여기 수정
        //                        else if (selectWarehousegNo.Equals(releaseWarehouseNo))
        //                            preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                        else
        //                            preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                    }

        //                }
        //            }
        //        }

        //        componentCdPreCnt = preCnt;

        //        if (isSelectPallet)
        //            query = $@"SELECT PALLET, COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                    WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)}) AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')
        //                    GROUP BY PALLET, COMPONENT_CD ORDER BY PALLET, COMPONENT_CD";
        //        else
        //            query = $@"SELECT PALLET, COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                    WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND COMPONENT_CD IN ('{string.Join("','", lisComponentCd)}')
        //                    GROUP BY PALLET, COMPONENT_CD ORDER BY PALLET, COMPONENT_CD";
        //        _dtComponentCdList.Clear();
        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    DataRow dr = _dtComponentCdList.NewRow();
        //                    dr["PALLET"] = obj["PALLET"];
        //                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
        //                    dr["CNT"] = obj["CNT"];
        //                    _dtComponentCdList.Rows.Add(dr);

        //                }
        //            }
        //        }

        //        leWarehouseNo2.EditValue = _currentRowWarehouse["KEY"];
        //        if(isSelectPallet)
        //        {
        //            lcPalletName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //            lcPallet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //            List<string> listPallet = new List<string>();
        //            foreach (string pallet in lisPallet)
        //            {
        //                listPallet.Add(_dicPallet[pallet]);
        //            }

        //            tePallet1.Text = string.Join(",", listPallet);
        //        }
        //        else
        //        {
        //            lcPalletName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //            lcPallet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //        }

        //        teComponentCd.Text = string.Join(",", lisComponentCd);
        //        lcPreComponentCdCnt.Text = ConvertUtil.ToString(componentCdPreCnt);
        //        lcCurComponentCdCnt.Text = ConvertUtil.ToString(componentCdCurCnt);

        //        tcWarehouseState.SelectedTabPage = xtpComponentCd;
        //    }
        //    else if (rows.Length > 0)
        //    {
        //        int componentCurCnt = 0;
        //        int componentPreCnt = 0;
        //        string state = "R";
        //        bool isSelectPallet = false;
        //        string query = "";
        //        List<string> lisComponentCd = new List<string>();
        //        List<string> lisComponent = new List<string>();
        //        List<long> lisComponentId = new List<long>();
        //        List<string> lisPallet = new List<string>();

        //        if (palletIndex > 0 && palletRow.Length > 0)
        //            isSelectPallet = true;

        //        if (isSelectPallet)
        //        {
        //            for (int i = 0; i < palletRow.Length; i++)
        //            {
        //                if (palletRow[i] == 0)
        //                    continue;
        //                DataRow drv = gvPallet.GetDataRow(palletRow[i]);
        //                lisPallet.Add(ConvertUtil.ToString(drv["PALLET_ID"]));
        //            }
        //        }

        //        for (int i = 0; i < componentCdRow.Length; i++)
        //        {
        //            if (componentCdRow[i] == 0)
        //                continue;
        //            DataRow drv = gvComponentCd.GetDataRow(componentCdRow[i]);
        //            lisComponentCd.Add(ConvertUtil.ToString(drv["KEY"]));
        //        }

        //        foreach (DataRow row in rows)
        //        {
        //            lisComponent.Add(ConvertUtil.ToString(row["COMPONENT"]));
        //            lisComponentId.Add(ConvertUtil.ToInt64(row["COMPONENT_ID"]));
        //        }


        //        JObject jResult = new JObject();

        //        if (isSelectPallet)
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND PALLET IN ({string.Join(",", lisPallet)}) AND COMPONENT_ID IN ({string.Join(",", lisComponentId)})";
        //        else
        //            query = $@"SELECT COUNT(INVENTORY_ID) AS CNT FROM TN_INVENTORY 
        //                        WHERE LOCATION = {_currentRowWarehouse["KEY"]} AND COMPONENT_ID IN ({string.Join(",", lisComponentId)})";


        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    componentCurCnt = ConvertUtil.ToInt32(obj["CNT"]);
        //                }
        //            }
        //        }

        //        int preCnt = componentCurCnt;
        //        if (isSelectPallet)
        //        {
        //            foreach (string pallet in lisPallet)
        //            {

        //                query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_PALLET_ID, A.WAREHOUSING_PALLET_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_PALLET_ID = {pallet} OR A.WAREHOUSING_PALLET_ID = {pallet}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')

        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_ID IN ({string.Join(",", lisComponentId)})";

        //                if (DBConnect.queryDT(query, ref jResult))
        //                {
        //                    if (Convert.ToBoolean(jResult["EXIST"]))
        //                    {
        //                        JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                        foreach (JObject obj in jArray.Children<JObject>())
        //                        {
        //                            state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                            string releasePalletNo = ConvertUtil.ToString(obj["RELEASE_PALLET_ID"]);
        //                            string warehousingPalletNo = ConvertUtil.ToString(obj["WAREHOUSING_PALLET_ID"]);
        //                            if (pallet.Equals(releasePalletNo) && pallet.Equals(warehousingPalletNo))
        //                                preCnt += 0; // 여기 수정
        //                            else if (pallet.Equals(releasePalletNo))
        //                                preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                            else
        //                                preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            query = $@"SELECT COUNT(B.INVENTORY_ID) AS CNT, WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM(
        //                            SELECT WAREHOUSE_MOVEMENT_ID,
        //                                    WAREHOUSE_MOVEMENT_STATE, A.RELEASE_WAREHOUSE_ID, A.WAREHOUSING_WAREHOUSE_ID
        //                            FROM TN_WAREHOUSE_MOVEMENT A
        //                            WHERE (A.RELEASE_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]} OR A.WAREHOUSING_WAREHOUSE_ID = {_currentRowWarehouse["KEY"]}) AND A.WAREHOUSE_MOVEMENT_STATE IN ('R', 'W')

        //                            ORDER BY A.UPDATE_DT DESC LIMIT 1
        //                            ) A  LEFT OUTER JOIN TN_WAREHOUSE_MOVEMENT_LIST B ON (A.WAREHOUSE_MOVEMENT_ID = B.WAREHOUSE_MOVEMENT_ID)
        //                                LEFT JOIN TN_INVENTORY C ON (B.INVENTORY_ID = C.INVENTORY_ID)
        //                                WHERE COMPONENT_CD IN ({string.Join(",", lisComponentId)})";

        //            if (DBConnect.queryDT(query, ref jResult))
        //            {
        //                if (Convert.ToBoolean(jResult["EXIST"]))
        //                {
        //                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

        //                    foreach (JObject obj in jArray.Children<JObject>())
        //                    {
        //                        state = ConvertUtil.ToString(obj["WAREHOUSE_MOVEMENT_STATE"]);
        //                        string releaseWarehouseNo = ConvertUtil.ToString(obj["RELEASE_WAREHOUSE_ID"]);
        //                        string warehousingWarehouseNo = ConvertUtil.ToString(obj["WAREHOUSING_WAREHOUSE_ID"]);
        //                        string selectWarehousegNo = ConvertUtil.ToString(_currentRowWarehouse["KEY"]);
        //                        if (releaseWarehouseNo.Equals(warehousingWarehouseNo))
        //                            preCnt += 0; // 여기 수정
        //                        else if (selectWarehousegNo.Equals(releaseWarehouseNo))
        //                            preCnt += ConvertUtil.ToInt32(obj["CNT"]);
        //                        else
        //                            preCnt -= ConvertUtil.ToInt32(obj["CNT"]);
        //                    }

        //                }
        //            }
        //        }

        //        componentPreCnt = preCnt;

        //        string modelquery = $@"CASE
        //                       WHEN A.COMPONENT_CD = 'CPU'
        //                       THEN (SELECT MODEL_NM FROM TN_CPU B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'MBD'
        //                       THEN (SELECT

        //                       CONCAT(IFNULL(MANUFACTURE_NM,''),'/',
        //                       IFNULL(
        //                       CASE
        //                        WHEN B.MANUFACTURE_NM = 'LENOVO' OR B.MANUFACTURE_NM = 'TOSHIBA'
        //                        THEN B.SYSTEM_VERSION
        //                        ELSE B.MBD_MODEL_NM
        //                        END, '')
        //                       ,'/', IFNULL(PRODUCT_NAME, ''))

        //                       FROM TN_MBD B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'MEM'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',IFNULL(BANDWIDTH,''),'/',IFNULL(CAPACITY,'')) FROM TN_MEM B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'STG'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',IFNULL(CAPACITY,'')) FROM TN_STG B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'VGA'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',IFNULL(CAPACITY,'')) FROM TN_VGA B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'MON'
        //                       THEN (SELECT CONCAT(IFNULL(MODEL_NM,''),'/',IFNULL(MODEL_ID,''), '/', SIZE) FROM TN_MON B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'CAS'
        //                       THEN(SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',CASE_CAT,'/',CASE_TYPE) FROM TN_CAS B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'POW'
        //                       THEN(SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',POW_CAT,'/',POW_TYPE) FROM TN_POW B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'ADP'
        //                       THEN(SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',ADP_CAT,'/', OUTPUT_WATT,'V/', OUTPUT_AMPERE,'A') FROM TN_ADP B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'KEY'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',KEY_CAT,'/', KEY_TYPE) FROM TN_KEY B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'MOU'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',MOU_CAT,'/', MOU_TYPE) FROM TN_MOU B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'FAN'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',FAN_CAT,'/', FAN_TYPE) FROM TN_FAN B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'CAB'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',CAB_CAT,'/', CAB_TYPE) FROM TN_CAB B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'BAT'
        //                       THEN  (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',BAT_CAT,'/', OUTPUT_WATT,'V/',OUTPUT_AMPERE,'mAh') FROM TN_BAT B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'PKG'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',PACKAGE_TYPE,'/', CATEGORY,'/',SIZE) FROM TN_PKG B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'AIR'
        //                       THEN (SELECT CONCAT(IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/',TYPE,'/', CATEGORY,'/',SIZE) FROM TN_AIR B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'LIC'
        //                       THEN (SELECT CONCAT(IFNULL(TYPE,''),'/',IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/', IFNULL(ETC,'')) FROM TN_LIC B WHERE A.DATA_ID = B.DATA_ID)
        //                       WHEN A.COMPONENT_CD = 'PER'
        //                       THEN (SELECT CONCAT(IFNULL(TYPE,''),'/',IFNULL(MANUFACTURE_NM,''),'/',IFNULL(MODEL_NM,''),'/', IFNULL(ETC,'')) FROM TN_PER B WHERE A.DATA_ID = B.DATA_ID)
        //                      END AS MODEL_NM";

        //        if (isSelectPallet)
        //            query = $@"SELECT A.PALLET, A.COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT, {modelquery} FROM TN_INVENTORY A
        //                    WHERE A.LOCATION = {_currentRowWarehouse["KEY"]} AND A.PALLET IN ({string.Join(",", lisPallet)}) AND A.COMPONENT_ID IN ({string.Join(",", lisComponentId)})
        //                    GROUP BY A.PALLET, A.COMPONENT_CD ORDER BY A.PALLET, A.COMPONENT_CD";
        //        else
        //            query = $@"SELECT A.PALLET, A.COMPONENT_CD, COUNT(INVENTORY_ID) AS CNT, {modelquery} FROM TN_INVENTORY A
        //                    WHERE A.LOCATION = {_currentRowWarehouse["KEY"]} AND A.COMPONENT_ID IN ({string.Join(",", lisComponentId)})
        //                    GROUP BY A.PALLET, A.COMPONENT_CD ORDER BY A.PALLET, A.COMPONENT_CD";

        //        _dtComponentCdList.Clear();
        //        if (DBConnect.queryDT(query, ref jResult))
        //        {
        //            if (Convert.ToBoolean(jResult["EXIST"]))
        //            {
        //                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
        //                foreach (JObject obj in jArray.Children<JObject>())
        //                {
        //                    DataRow dr = _dtComponentList.NewRow();
        //                    dr["PALLET"] = obj["PALLET"];
        //                    //dr["COMPONENT"] = obj["COMPONENT"];
        //                    dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
        //                    dr["MODEL_NM"] = obj["MODEL_NM"];
        //                    dr["CNT"] = obj["CNT"];
        //                    _dtComponentList.Rows.Add(dr);

        //                }
        //            }
        //        }

        //        leWarehouseNo3.EditValue = _currentRowWarehouse["KEY"];

        //        List<string> listPallet = new List<string>();
        //        foreach (string pallet in lisPallet)
        //        {
        //            listPallet.Add(_dicPallet[pallet]);
        //        }

        //        tePallet2.Text = string.Join(",", listPallet);
        //        teComponentCd2.Text = string.Join(",", lisComponentCd);
        //        teComponent.Text = string.Join(",", lisComponent);
        //        lcPreComponentCnt.Text = ConvertUtil.ToString(componentPreCnt);
        //        lcCurComponentCnt.Text = ConvertUtil.ToString(componentCurCnt);

        //        tcWarehouseState.SelectedTabPage = xtpComponent;
        //    }
        //}


        //private void gvComponentCd_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        //{
        //    if (_isValidRow)
        //    {
        //        getComponent();
        //    }

        //}

        //private void warehouseTimer_Tick(object sender, EventArgs e)
        //{
        //    setWarehouseState();
        //}

        //private void sbSearch_Click(object sender, EventArgs e)
        //{
        //    string dtFrom = "";
        //    string dtTo = "";


        //    if (deDtFrom.EditValue != null && !string.IsNullOrEmpty(deDtFrom.EditValue.ToString()))
        //        dtFrom = $"{deDtFrom.Text} 00:00:00";

        //    if (deDtTo.EditValue != null && !string.IsNullOrEmpty(deDtTo.EditValue.ToString()))
        //        dtTo = $"{deDtTo.Text} 23:59:59";

        //    JObject jResult = new JObject();
        //    getWarehouseMovementList(teRegistNo.EditValue,
        //        teBarcode.EditValue,
        //        ConvertUtil.ToString(leWarehouseMovementStatus.EditValue),
        //        dtFrom, dtTo, dtRegist, dtRelease, dtWarehousing, leReleaseWarehouseNo.EditValue, leWarehousingWarehouseNo.EditValue, ref jResult);

        //}
    }
}