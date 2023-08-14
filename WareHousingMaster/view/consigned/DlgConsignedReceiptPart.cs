using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;


namespace WareHousingMaster.view.consigned
{
    public partial class DlgConsignedReceiptPart : DevExpress.XtraEditors.XtraForm
    {

        DataTable _dtReceiptPart;
        BindingSource _bsReceiptPart;

        DataRowView _drvReceiptPart;

        long _proxyId;
        public Dictionary<long, long> _dicErrorPart { get; private set; }

        public DlgConsignedReceiptPart(long proxyId, Dictionary<long, long> dicErrorPart)
        {
            InitializeComponent();

            _proxyId = proxyId;
            _dicErrorPart = dicErrorPart;

            _dtReceiptPart = new DataTable();
            _dtReceiptPart.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("PROXY_PART_ID", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("DETAIL_DATA", typeof(string)));
            _dtReceiptPart.Columns.Add(new DataColumn("CONSIGNED_TYPE", typeof(int)));
            _dtReceiptPart.Columns.Add(new DataColumn("ERROR_PART", typeof(bool)));
            _dtReceiptPart.Columns.Add(new DataColumn("PROXY_PRICE", typeof(long)));
            _dtReceiptPart.Columns.Add(new DataColumn("ASSIGN_YN", typeof(bool)));
            _dtReceiptPart.Columns.Add(new DataColumn("CHECK", typeof(bool)));



            _bsReceiptPart = new BindingSource();
        }

        private void usrConsignedAdjustment_Load(object sender, EventArgs e)
        {
            Dangol.ShowSplash();
            setInfoBox();
            setIInitData();

            getReceiptPart();
            Dangol.CloseSplash();
        }


        private void setInfoBox()
        {

            DataTable dtConsignedType = new DataTable();
            dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtConsignedType, 1, "생산대행");
            Util.insertRowonTop(dtConsignedType, 2, "자사재고");
            Util.LookupEditHelper(rileConsignedType, dtConsignedType, "KEY", "VALUE");
        }

        private void setIInitData()
        {
            gcPart.DataSource = null;
            _bsReceiptPart.DataSource = _dtReceiptPart;
            gcPart.DataSource = _bsReceiptPart;
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _drvReceiptPart = e.Row as DataRowView;
            }
            else
            {
                _drvReceiptPart = null;
            }
        }

        private void gvPart_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _drvReceiptPart = gvPart.GetRow(e.FocusedRowHandle) as DataRowView;

            }
            else
            {
                _drvReceiptPart = null;
            }
        }
        private void getReceiptPart()
        {
            _dtReceiptPart.Clear();

            JObject jResult = new JObject();
            JObject jobj = new JObject();

            jobj.Add("PROXY_ID", _proxyId);
            jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);
            jobj.Add("RECEIPT_DT", DateTime.Today.ToString("yyyy-MM-dd"));

            if (DBConsigned.getConsignedReceiptPart(jobj, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                long price = 0;
                long proxyPartId;
                long componentId;
                int releaseCnt;
                bool assignYn;

                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtReceiptPart.NewRow();

                    proxyPartId = ConvertUtil.ToInt64(obj["PROXY_PART_ID"]);
                    componentId = ConvertUtil.ToInt64(obj["COMPONENT_ID"]);
                    releaseCnt = ConvertUtil.ToInt32(obj["RELEASE_CNT"]);
                    assignYn = releaseCnt > 0 ? true : false;

                    DataRow drGrid = _dtReceiptPart.NewRow();

                    drGrid["NO"] = index++;
                    drGrid["PROXY_PART_ID"] = proxyPartId;
                    drGrid["COMPONENT_ID"] = componentId;
                    drGrid["COMPONENT_CD"] = obj["COMPONENT_CD"];
                    drGrid["DETAIL_DATA"] = obj["DETAIL_DATA"];
                    drGrid["CONSIGNED_TYPE"] = obj["CONSIGNED_TYPE"];
                    drGrid["ASSIGN_YN"] = assignYn;
                    drGrid["PROXY_PRICE"] = obj["PROXY_PRICE"];
                    drGrid["CHECK"] = false;
                    drGrid["ERROR_PART"] = _dicErrorPart.ContainsKey(proxyPartId);


                    _dtReceiptPart.Rows.Add(drGrid);

                    price += ConvertUtil.ToInt64(obj["PROXY_PRICE"]);

                }
            }
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (Dangol.MessageYN("선택하신 부품을 반품/교환 체크에 저장하시겠습니까?") != DialogResult.Yes)
            {
                return;
            }

            _dicErrorPart.Clear();
            
            DataRow[] rows = _dtReceiptPart.Select("ERROR_PART = TRUE");

            foreach (DataRow row in rows)
                _dicErrorPart.Add(ConvertUtil.ToInt64(row["PROXY_PART_ID"]), ConvertUtil.ToInt64(row["COMPONENT_ID"]));

            this.DialogResult = DialogResult.OK;
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }


    
}