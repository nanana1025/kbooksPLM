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

namespace WareHousingMaster.view.release
{
    public partial class dlgConsignedComponentList : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtComponentList;
        BindingSource _bs;

        DataRowView _currentRow;

        long _proxyPartId;
        long _companyId;
        string _componentCd;
        int _consignedType;
        public long _componentId { get; private set; }

        long _warehouseMovementId;

        string _warehousingPallet = "-1";
        public int Cnt { get; private set; }

        public dlgConsignedComponentList(long proxyPartId, long companyId, string componentCd, int consignedType)
        {
            InitializeComponent();

            _dtComponentList = new DataTable();
            _dtComponentList.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dtComponentList.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dtComponentList.Columns.Add(new DataColumn("INVENTORY_CNT", typeof(int)));
            _dtComponentList.Columns.Add(new DataColumn("GOOD_CNT", typeof(int)));
            _dtComponentList.Columns.Add(new DataColumn("FAULT_CNT", typeof(int)));


            _bs = new BindingSource();

            _proxyPartId = proxyPartId;
            _companyId = companyId;
            _componentCd = componentCd;
            _consignedType = consignedType;

        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {

            gcComponentList.DataSource = null;
            _bs.DataSource = _dtComponentList;
            gcComponentList.DataSource = _bs;

            getComponent();
        }


        private void getComponent()
        {
            JObject jResult = new JObject();

            if (DBConsigned.getConsignedComponentCdList(_proxyPartId, _companyId, _componentCd, _consignedType, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                _dtComponentList.BeginInit();
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtComponentList.NewRow();

                    dr["NO"] = index++;
                    dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                    dr["COMPONENT"] = obj["COMPONENT"];
                    dr["MODEL_NM"] = obj["MODEL_NM"];
                    dr["INVENTORY_CNT"] = obj["INVENTORY_CNT"];


                    _dtComponentList.Rows.Add(dr);
                }
                _dtComponentList.EndInit();
            }
            else
            {

            }

        }
        private void gvComponentList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvComponentList.RowCount > 0);
            //_dtComponentList.Clear();

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                _componentId = ConvertUtil.ToInt64(_currentRow["COMPONENT_ID"]);
            }
            else
            {
                _currentRow = null;


            }
        }


        private void sbSave_Click(object sender, EventArgs e)
        {

            if (_currentRow == null)
                Dangol.Message("선택된 부품이 없습니다.");
            else
            {
                if (Dangol.MessageYN("선택한 부품으로 할당하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();

                    if (DBConsigned.assignSelectComponent(_currentRow["COMPONENT_ID"], _proxyPartId, _companyId, _componentCd, _consignedType, ref jResult))
                    {
                        Dangol.Message("할당되었습니다.");
                        this.DialogResult = DialogResult.OK;
                    }

                }
            }


        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}