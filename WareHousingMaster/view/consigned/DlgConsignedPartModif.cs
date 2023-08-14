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
using DevExpress.XtraGrid.Views.Grid;
using WareHousingMaster.view.warehousing.createComponent;
using WareHousingMaster.view.warehousing.selectComponent;
using DevExpress.XtraTreeList.Nodes;
using WareHousingMaster.view.usedPurchase;
using ImportExcel;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;

namespace WareHousingMaster.view.consigned
{
    public partial class DlgConsignedPartModif : DevExpress.XtraEditors.XtraForm
    {
        string _componentCd;

        TreeListNode _currentReceiptPart;
        DataRowView _currentComponentPart;

        DataTable _dtReceiptPart;
        DataTable _dtComponentList;

        BindingSource _bsReceiptPart;
        BindingSource _bsComponentList;

        Dictionary<long, List<long>> _dicReceiptPart;
        Dictionary<long, List<long>> _dicConsignedModel;

        string[] _consignedComponetCd = new string[] { "MODEL", "CAS", "CPU", "MBD", "MEM", "SSD", "HDD", "VGA", "POW", "KEY", "MOU", "CAB", "PER", "LIC", "MON", "AIR", "PKG" };
        string[] _consignedComponetNm = new string[] { "MODEL", "본체OR케이스", "CPU", "메인보드", "메모리", "SSD", "HDD", "그래픽카드", "파워", "키보드", "마우스", "케이블", "주변기기", "라이센스", "모니터", "에어", "박스" };

        Dictionary<int, string> _dicConsignedComponentCd;
        Dictionary<string, int> _dicConsignedComponentCdReverse;

        string _currentGetComponentCd;

        long _modelId;
        string _modelNm;

        long _proxyId;
        long _companyId;

        int _currnetPartCnt=0;

        string _receipt;

        public DlgConsignedPartModif(long proxyId, string receipt, long companyId)
        {
            InitializeComponent();

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

            _dtComponentList = new DataTable();
            _dtComponentList.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("VISIBLE", typeof(bool)));

            _bsReceiptPart = new BindingSource();
            _bsComponentList = new BindingSource();

            _dicReceiptPart = new Dictionary<long, List<long>>();
            _dicConsignedModel = new Dictionary<long, List<long>>();

            _dicConsignedComponentCd = new Dictionary<int, string>();
            _dicConsignedComponentCdReverse = new Dictionary<string, int>();

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                _dicConsignedComponentCd.Add(i, _consignedComponetCd[i]);
                _dicConsignedComponentCdReverse.Add(_consignedComponetCd[i], i);
            }

            _currentGetComponentCd = null;

            _proxyId = proxyId;
            _companyId = companyId;
            _receipt = receipt;
        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();
            getConsignedData();

            tlPart.ExpandAll();
        }

        public IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        private void setInfoBox()
        {
            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                DataRow row = dtComponentCd.NewRow();
                row["KEY"] = _consignedComponetCd[i];
                row["VALUE"] = _consignedComponetNm[i];
                dtComponentCd.Rows.Add(row);
            }
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtConsignedType = new DataTable();

            dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtConsignedType, 1, "생산대행");
            Util.insertRowonTop(dtConsignedType, 2, "자사재고");
            Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");

            

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsReceiptPart.DataSource = _dtReceiptPart;
            _bsComponentList.DataSource = _dtComponentList;
        }

        private void setGridControl()
        {
            tlPart.DataSource = null;
            tlPart.DataSource = _bsReceiptPart;

            gcComponentList.DataSource = null;
            gcComponentList.DataSource = _bsComponentList;
        }

        private void tlPart_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _currentReceiptPart = tlPart.FocusedNode;
            //_dtComponentList.Clear();

            if (_currentReceiptPart != null)
            {
                _currnetPartCnt = ConvertUtil.ToInt32(_currentReceiptPart["PART_CNT"]);
                if (tlPart.IsRootNode(_currentReceiptPart) || ConvertUtil.ToInt32(_currentReceiptPart["P_PART_ID"]) == 0)
                {
                    tlPartCnt.OptionsColumn.ReadOnly = true;
                    tlConsignedType.OptionsColumn.ReadOnly = true;
                }
                else
                {
                    tlPartCnt.OptionsColumn.ReadOnly = false;
                    tlConsignedType.OptionsColumn.ReadOnly = false;
                }
            }
            else
            {
                _currnetPartCnt = 0;
                tlPartCnt.OptionsColumn.ReadOnly = true;
                tlConsignedType.OptionsColumn.ReadOnly = true;
            }
        }

        private void gvComponentList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvComponentList.RowCount > 0);

            if (isValidRow)
            {
                _currentComponentPart = e.Row as DataRowView;
            }
            else
            {
                _currentComponentPart = null;
            }
        }

        private bool getConsignedData()
        {
            JObject jResult = new JObject();
            JObject jData = new JObject();

          
            jData.Add("PROXY_ID", _proxyId);

            if (DBConsigned.getConsignedPart(jData, ref jResult))
            {
                //private void makeRecipt(Dictionary<string, Dictionary<long, int>> dicComponent, Dictionary<long, string> dicComponentNm, List<long> listConsignedType)

                Dictionary<string, Dictionary<long, int>> dicComponent = new Dictionary<string, Dictionary<long, int>>();
                Dictionary<long, string> dicComponentNm = new Dictionary<long, string>();
                List<long> listConsignedType = new List<long>();

                if (ConvertUtil.ToBoolean(jResult["PART_EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    string componentCd;
                    long componentId;
                    int componentCnt;
                    int consignedType;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        componentCd = ConvertUtil.ToString(obj["COMPONENT_CD"]);
                        componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                        componentCnt = ConvertUtil.ToInt32(obj["COMPONENT_CNT"]);
                        consignedType = ConvertUtil.ToInt32(obj["CONSIGNED_TYPE"]);

                        if (dicComponent.ContainsKey(componentCd))
                        {
                            Dictionary<long, int>  dicData = dicComponent[componentCd];
                            dicData.Add(componentId, componentCnt);
                            dicComponent[componentCd] = dicData;
                        }
                        else
                        {
                            Dictionary<long, int> dicData = new Dictionary<long, int>();
                            dicData.Add(componentId, componentCnt);
                            dicComponent.Add(componentCd, dicData);
                        }

                        if (consignedType == 2)
                            listConsignedType.Add(componentId);

                        dicComponentNm.Add(componentId, ConvertUtil.ToString(obj["MODEL_NM"]));
                    }
                }
                if (ConvertUtil.ToBoolean(jResult["MODEL_EXIST"]))
                {
                    _modelId = ConvertUtil.ToInt64(jResult["MODEL_ID"]);
                    _modelNm = ConvertUtil.ToString(jResult["MODEL_NM"]);
                }

                makeRecipt(dicComponent, dicComponentNm, listConsignedType);


                return true;
            }
            else
            {
                return false;
            }
        }


        private void lcgReceipt_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                //if (_currentReceipt == null)
                //{
                //    Dangol.Message("선택된 항목이 없습니다.");
                //    return;
                //}

            }
        }


        private void lgcReceiptList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if(_currentReceiptPart == null)
            {
                Dangol.Message("선택된 항목이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                TreeListNode rootNode = null;
                if (tlPart.IsRootNode(_currentReceiptPart))
                {
                    rootNode = _currentReceiptPart;
                }
                else
                {
                    rootNode = _currentReceiptPart.RootNode;
                }

                string componentCd = ConvertUtil.ToString(rootNode["COMPONENT_CD"]);

                if(componentCd.Equals("MODEL"))
                    getModelList(componentCd);
                else
                    getComponentList(componentCd);

                _currentGetComponentCd = componentCd;

            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (tlPart.IsRootNode(_currentReceiptPart))
                {
                    Dangol.Message("부품 정보를 선택해 주세요.");
                }
                else
                {
                    string componentCd = ConvertUtil.ToString(_currentReceiptPart["COMPONENT_CD_T"]);

                    tlPart.BeginUpdate();
                    _dtReceiptPart.BeginInit();

                    int partCnt = ConvertUtil.ToInt32(_currentReceiptPart.RootNode["PART_CNT"]);
                    partCnt -= ConvertUtil.ToInt32(_currentReceiptPart["PART_CNT"]);
                    _currentReceiptPart.RootNode["PART_CNT"] = partCnt;

                    string refType = "COMPONENT_ID";

                    if (componentCd.Equals("MODEL"))
                    {
                        refType = "MODEL_ID";
                        List<long> listModel = _dicConsignedModel[_proxyId];
                        listModel.Remove(ConvertUtil.ToInt64(_currentReceiptPart["MODEL_ID"]));

                        //gvReceipt.BeginDataUpdate();
                        //_currentReceipt["MODEL_NM"] = "";
                        //gvReceipt.EndDataUpdate();
                    }              
                    else
                    {
                        refType = "COMPONENT_ID";
                        List<long> listPart = _dicReceiptPart[_proxyId];
                        listPart.Remove(ConvertUtil.ToInt64(_currentReceiptPart["COMPONENT_ID"]));
                    }

                    if (!string.IsNullOrEmpty(_currentGetComponentCd) && _currentGetComponentCd.Equals(componentCd))
                    {
                        gvComponentList.BeginDataUpdate();
                        DataRow[] rows = _dtComponentList.Select($"COMPONENT_ID = {_currentReceiptPart[refType]}");
                        foreach (DataRow row in rows)
                            row["VISIBLE"] = true;

                        gvComponentList.EndDataUpdate();
                    }

                    _currentReceiptPart.Remove();

                    _dtReceiptPart.EndInit();
                    tlPart.EndUpdate();
                }
            }
        }


        private void lgcComponentList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (_currentComponentPart == null)
            {
                Dangol.Message("선택된 항목이 없습니다.");
                return;
            }

            if (e.Button.Properties.Tag.Equals(1))
            {
                addComponentPart();
            }
        }

        private void gvComponentList_DoubleClick(object sender, EventArgs e)
        {
            if (_currentComponentPart != null)
            {
                addComponentPart();
            }
        }

        private void addComponentPart()
        {
            string componentCd = ConvertUtil.ToString(_currentComponentPart["COMPONENT_CD"]);

            if (componentCd.Equals("MODEL"))
            {
                if (!ConvertUtil.ToBoolean(_currentComponentPart["VISIBLE"]))
                    Dangol.Message("이미 등록된 모델입니다.");
                else
                    addModel();
            }
            else
            {
                if (!ConvertUtil.ToBoolean(_currentComponentPart["VISIBLE"]))
                    Dangol.Message("이미 등록된 부품입니다.");
                else
                    addComponent();
            }
        }

        private void addModel()
        {
            string componentCd = ConvertUtil.ToString(_currentComponentPart["COMPONENT_CD"]);
            long id = _proxyId;

            List<long> listPart = _dicReceiptPart[id];
            List<long> listModel = _dicConsignedModel[id];

            long partId = _dicConsignedComponentCdReverse[componentCd];
            DataRow[] rowRoots = null;
            DataRow rowRoot = null;

            tlPart.BeginUpdate();
            _dtReceiptPart.BeginInit();

            DataRow rRow = _dtReceiptPart.NewRow();

            rRow["ID"] = id;
            rRow["P_PART_ID"] = partId;
            rRow["PART_ID"] = ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]) * -1;
            rRow["COMPONENT_ID"] = -1;
            rRow["MODEL_ID"] = _currentComponentPart["COMPONENT_ID"];
            rRow["CONSIGNED_TYPE"] =-1;
            rRow["COMPONENT_CD"] = "";
            rRow["MODEL_NM"] = _currentComponentPart["MODEL_NM"];
            rRow["PART_CNT"] = 0;

            _dtReceiptPart.Rows.Add(rRow);

            _modelNm = ConvertUtil.ToString(_currentComponentPart["MODEL_NM"]);
  
            long partListId = ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]);

            listModel.Add(partListId);

            JObject jResult = new JObject();

            if (DBConsigned.getConsignedModelComponent(partListId, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {

                        long componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                        partId = _dicConsignedComponentCdReverse[ConvertUtil.ToString(obj["COMPONENT_CD"])];

                        rowRoots = _dtReceiptPart.Select($"ID = {_proxyId} AND PART_ID = {partId} AND P_PART_ID = -1");
                        if (rowRoots.Length < 1)
                            continue;
                        else
                            rowRoot = rowRoots[0];

                        if (listPart.Contains(componentId))
                        {
                            DataRow[] rows = _dtReceiptPart.Select($"ID = {id} AND COMPONENT_ID = {componentId}");

                            foreach(DataRow row in rows)
                            {
                                int cnt = ConvertUtil.ToInt32(row["PART_CNT"]);
                                cnt += ConvertUtil.ToInt32(obj["COMPONENT_CNT"]);
                                row["PART_CNT"] = cnt;

                                int partCnt = ConvertUtil.ToInt32(rowRoot["PART_CNT"]);
                                partCnt += ConvertUtil.ToInt32(obj["COMPONENT_CNT"]);
                                rowRoot["PART_CNT"] = partCnt;
                            }
                        }
                        else
                        {
                            DataRow dr = _dtReceiptPart.NewRow();
                            

                            dr["ID"] = id;
                            dr["P_PART_ID"] = partId;
                            dr["PART_ID"] = componentId;
                            dr["COMPONENT_ID"] = componentId;
                            dr["MODEL_ID"] = _currentComponentPart["COMPONENT_ID"];
                            dr["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                            //dr["COMPONENT_CD"] = _currentComponentPart["COMPONENT_CD"];
                            dr["COMPONENT_CD_T"] = obj["COMPONENT_CD"];
                            dr["COMPONENT_CD"] = "";
                            dr["MODEL_NM"] = $"{obj["MANUFACTURE_NM"]}/{obj["MODEL_NM"]}/{obj["SPEC_NM"]}";
                            dr["PART_CNT"] = obj["COMPONENT_CNT"];

                            _dtReceiptPart.Rows.Add(dr);

                            int partCnt = ConvertUtil.ToInt32(rowRoot["PART_CNT"]);
                            partCnt++;
                            rowRoot["PART_CNT"] = partCnt;

                            listPart.Add(ConvertUtil.ToInt64(obj["COMPONENT_ID"]));
                        }

                    }
                }
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }

            gvComponentList.BeginDataUpdate();
            _currentComponentPart["VISIBLE"] = false;
            gvComponentList.EndDataUpdate();


            _dtReceiptPart.EndInit();
            tlPart.EndUpdate();

            tlPart.ExpandAll();
        }

 
        private void addComponent()
        {
            string componentCd = ConvertUtil.ToString(_currentComponentPart["COMPONENT_CD"]);

            List<long> listPart = _dicReceiptPart[_proxyId];
            listPart.Add(ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]));

            long partId = _dicConsignedComponentCdReverse[componentCd];

            tlPart.BeginUpdate();
            _dtReceiptPart.BeginInit();

            DataRow dr = _dtReceiptPart.NewRow();

            long componentId = ConvertUtil.ToInt64(_currentComponentPart["COMPONENT_ID"]);

            dr["ID"] = ConvertUtil.ToInt64(_proxyId);
            dr["P_PART_ID"] = partId;
            dr["PART_ID"] = _currentComponentPart["COMPONENT_ID"];
            dr["COMPONENT_ID"] = _currentComponentPart["COMPONENT_ID"];
            dr["CONSIGNED_TYPE"] = 1;
            //dr["COMPONENT_CD"] = _currentComponentPart["COMPONENT_CD"];
            dr["COMPONENT_CD_T"] = _currentComponentPart["COMPONENT_CD"];
            dr["COMPONENT_CD"] = "";
            dr["MODEL_NM"] = _currentComponentPart["MODEL_NM"];
            dr["PART_CNT"] = 1;

            _dtReceiptPart.Rows.Add(dr);

            DataRow[] rows = _dtReceiptPart.Select($"ID = {_proxyId} AND PART_ID = {partId} AND P_PART_ID = -1");

            foreach (DataRow row in rows)
            {
                int partCnt = ConvertUtil.ToInt32(row["PART_CNT"]);
                partCnt++;
                row["PART_CNT"] = partCnt;
            }

            gvComponentList.BeginDataUpdate();
            _currentComponentPart["VISIBLE"] = false;
            gvComponentList.EndDataUpdate();

            _dtReceiptPart.EndInit();
            tlPart.EndUpdate();

            tlPart.ExpandAll();
        }

        private bool receipt(object id, ref JObject jResult)
        {
            JObject jobj = new JObject();

            DataRow[] rows = _dtReceiptPart.Select($"ID = {id} AND PART_ID <> -1 AND P_PART_ID > 0");

            var jPArt = new JArray();

            jobj.Add("PROXY_ID", _proxyId);
            jobj.Add("RECEIPT", _receipt);

            string componentCd;
            foreach (DataRow row in rows)
            {
                int componentCnt = ConvertUtil.ToInt32(row["PART_CNT"]);
               
                for (int i = 0; i < componentCnt; i++)
                {
                    JObject jdata = new JObject();
                    componentCd = _dicConsignedComponentCd[ConvertUtil.ToInt32(row["P_PART_ID"])];
                    jdata.Add("COMPONENT_ID", ConvertUtil.ToInt64(row["COMPONENT_ID"]));
                    if(componentCd.Equals("SSD") || componentCd.Equals("HDD"))
                        jdata.Add("COMPONENT_CD", "STG");
                    else
                        jdata.Add("COMPONENT_CD", componentCd);

                    jdata.Add("COMPONENT_CNT", 1);
                    jdata.Add("CONSIGNED_TYPE", ConvertUtil.ToInt32(row["CONSIGNED_TYPE"]));
                    jdata.Add("DISPLAY_SEQ", 1);

                    jPArt.Add(jdata);
                }
            }

            jobj.Add("DATA", jPArt);

            rows = _dtReceiptPart.Select($"ID = {id} AND PART_ID <> -1 AND P_PART_ID = 0");
            long modelId = -1;
            if (rows.Length > 0)
                modelId = ConvertUtil.ToInt64(rows[0]["MODEL_ID"]);

            jobj.Add("MODEL_ID", modelId);

            if (DBConsigned.updateReceiptPart(jobj, ref jResult))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void getModelList(string componentCd)
        {
            _dtComponentList.Clear();

            JObject jResult = new JObject();

            if (DBConsigned.getConsignedModelList(_companyId, componentCd, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    List<long> listModel = _dicConsignedModel[_proxyId];

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtComponentList.NewRow();

                        long modelListId = ConvertUtil.ToInt64(obj["MODEL_LIST_ID"]);
                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = modelListId;
                        dr["COMPONENT_CD"] = _currentReceiptPart["COMPONENT_CD_T"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        if (listModel.Contains(modelListId))
                            dr["VISIBLE"] = false;
                        else
                            dr["VISIBLE"] = true;

                        _dtComponentList.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }
        }

        private void getComponentList(string componentCd)
        {
            _dtComponentList.Clear();

            JObject jResult = new JObject();

            if (DBConsigned.getConsignedComponentList(_companyId, componentCd, ref jResult))
            {
                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    List<long> listPart = _dicReceiptPart[_proxyId];

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    int index = 1;
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtComponentList.NewRow();

                        long componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = componentId;
                        dr["COMPONENT_CD"] = _currentReceiptPart["COMPONENT_CD_T"];
                        dr["MODEL_NM"] = obj["REP_NAME"];
                        if (listPart.Contains(componentId))
                            dr["VISIBLE"] = false;
                        else
                            dr["VISIBLE"] = true;

                        _dtComponentList.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }
        }

       

        private void makeRecipt(Dictionary<string, Dictionary<long, int>> dicComponent, Dictionary<long, string> dicComponentNm, List<long> listConsignedType)
        {
            tlPart.FocusedNodeChanged -= tlPart_FocusedNodeChanged;
            tlPart.NodeCellStyle -= tlPart_NodeCellStyle;
            _dtReceiptPart.BeginInit();

            List<long> listPart = new List<long>();
            List<long> listModel = new List<long>();

            listModel.Add(_modelId);

            for (int i = 0; i < _consignedComponetCd.Length; i++)
            {
                string componentCd = _consignedComponetCd[i];
                DataRow row = _dtReceiptPart.NewRow();

                row["ID"] = _proxyId;
                row["P_PART_ID"] = -1;
                row["PART_ID"] = i;
                row["COMPONENT_ID"] = -1;
                row["CONSIGNED_TYPE"] = -1;
                row["COMPONENT_CD"] = componentCd;
                row["COMPONENT_CD_T"] = componentCd;
                //row["MODEL_NM"] = _consignedComponetNm[i];
                row["MODEL_NM"] = "";
                row["PART_CNT"] = 0;

                _dtReceiptPart.Rows.Add(row);

                if(dicComponent.ContainsKey(componentCd))
                {
                    Dictionary<long, int> dicComp = dicComponent[componentCd];
                    long partId = _dicConsignedComponentCdReverse[componentCd];
                    int totalCnt = 0;
                    foreach (KeyValuePair<long, int> item in dicComp)
                    {
                        long compId = item.Key;
                        int cnt = item.Value;
                        totalCnt += cnt;
                        listPart.Add(compId);


                        DataRow drComp = _dtReceiptPart.NewRow();

                        drComp["ID"] = _proxyId;
                        drComp["P_PART_ID"] = partId;
                        drComp["PART_ID"] = compId;
                        drComp["COMPONENT_ID"] = compId;
                        if(listConsignedType.Contains(compId))
                            drComp["CONSIGNED_TYPE"] = 2;
                        else
                            drComp["CONSIGNED_TYPE"] = 1;
                        //dr["COMPONENT_CD"] = _currentComponentPart["COMPONENT_CD"];
                        drComp["COMPONENT_CD_T"] = componentCd;
                        drComp["COMPONENT_CD"] = "";
                        drComp["MODEL_NM"] = dicComponentNm[compId];
                        drComp["PART_CNT"] = cnt;

                        _dtReceiptPart.Rows.Add(drComp);
                    }
       
                    DataRow[] rows = _dtReceiptPart.Select($"ID = {_proxyId} AND PART_ID = {partId} AND P_PART_ID = -1");

                    foreach (DataRow rowPart in rows)
                        rowPart["PART_CNT"] = totalCnt;
                }
            }

            if (_modelId > 0)
            {
                DataRow rRow = _dtReceiptPart.NewRow();

                rRow["ID"] = _proxyId;
                rRow["P_PART_ID"] = 0;
                rRow["PART_ID"] = _modelId * -1;
                rRow["COMPONENT_ID"] = -1;
                rRow["MODEL_ID"] = _modelId;
                rRow["CONSIGNED_TYPE"] = -1;
                rRow["COMPONENT_CD"] = "";
                rRow["MODEL_NM"] = _modelNm;
                rRow["PART_CNT"] = 1;

                _dtReceiptPart.Rows.Add(rRow);
                DataRow[] rows = _dtReceiptPart.Select($"ID = {_proxyId} AND PART_ID = 0 AND P_PART_ID = -1");

                foreach (DataRow rowPart in rows)
                    rowPart["PART_CNT"] = 1;

            }

            _dicReceiptPart.Add(_proxyId, listPart);
            _dicConsignedModel.Add(_proxyId, listModel);

            _dtReceiptPart.EndInit();
           
            tlPart.FocusedNodeChanged += tlPart_FocusedNodeChanged;
            tlPart.NodeCellStyle += tlPart_NodeCellStyle;
        }


        private void risePartCnt_EditValueChanged(object sender, EventArgs e)
        {
            tlPart.BeginUpdate();

            SpinEdit editor = (SpinEdit)sender;
            int partCnt = ConvertUtil.ToInt32(editor.EditValue);

            TreeListNode node = _currentReceiptPart.RootNode;
            int cnt = ConvertUtil.ToInt32(node["PART_CNT"]);
            cnt -= _currnetPartCnt;
            cnt += partCnt;
            _currnetPartCnt = partCnt;
            _currentReceiptPart["PART_CNT"] = partCnt;
            node["PART_CNT"] = cnt;
            tlPart.EndUpdate();
        }

        private void tlPart_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e != null && e.Node != null && e.Node["PART_ID"] != null)
            {
                Color backColor = Color.Transparent;
                Color foreColor = Color.Black;

                if (!String.IsNullOrEmpty(ConvertUtil.ToString(e.Node["PART_ID"])))
                {
                    long id = ConvertUtil.ToInt64(e.Node["P_PART_ID"]);

                    switch (id)
                    {
                        case -1:
                            backColor = Color.PapayaWhip;
                            foreColor = Color.Black;
                            break;
                        default:
                            backColor = Color.Transparent;
                            foreColor = Color.Black;
                            break;
                    }

                    e.Appearance.BackColor = backColor;
                }
            }
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("현재 부품 정보를 저장하시겠습니까?") == DialogResult.Yes)
            {
                object id = _proxyId;
                JObject jResult = new JObject();


                if (receipt(id, ref jResult))
                {
                    Dangol.Message("접수되었습니다.");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(jResult["MSG"]);
                }
               
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void rileConsignedType_Popup(object sender, EventArgs e)
        {
            PopupLookUpEditForm form = (sender as IPopupControl).PopupWindow as PopupLookUpEditForm;
            int width = Common.GetWidth(sender as LookUpEdit);
            if (form != null && form.Width > width)
            {
                form.Width = width;
            }
        }
    }
}