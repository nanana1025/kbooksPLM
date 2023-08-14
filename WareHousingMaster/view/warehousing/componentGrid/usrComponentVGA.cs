using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WareHousingMaster.view.common;
using Newtonsoft.Json.Linq;

namespace WareHousingMaster.view.warehousing.componentGrid
{
    public partial class usrComponentVGA : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; private set; }

        //public string _adpType { get; private set; }

        

        public string _componentCd { get; set; }

        int _id;

        public usrComponentVGA()
        {
            InitializeComponent();

            _dt = new DataTable();
            _bs = new BindingSource();

        }

        public void setinitialize()
        {
            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("DATA_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            foreach (string col in ProjectInfo._dicDeviceFullColumn["VGA"])
                _dt.Columns.Add(new DataColumn(col, typeof(string)));
            _dt.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            

            gcPart.DataSource = null;
            _bs.DataSource = _dt;
            gcPart.DataSource = _bs;

            _id = 1;
        }

        public void getComponentAll()
        {
            JObject jResult = new JObject();

            if (DBConnect.getComponentAll(_componentCd, ref jResult))
            {
                _dt.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    int index = 1;
                    List<string> listCol = ProjectInfo._dicDeviceFullColumn[_componentCd];

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["INVENTORY_ID"] = -1;
                        dr["DATA_ID"] = obj["DATA_ID"];
                        dr["COMPONENT"] = obj["COMPONENT"];
                        dr["BARCODE"] = "";
                        dr["MANUFACTURE_NM"] = obj["MANUFACTURE_NM"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["SPEC_NM"] = obj["SPEC_NM"];
                        dr["CODE_NM"] = obj["CODE_NM"];
                        dr["SOCKET_NM"] = obj["SOCKET_NM"];
                        dr["CORE_CNT"] = obj["CORE_CNT"];
                        dr["THREAD_CNT"] = obj["THREAD_CNT"];
                        dr["CREATE_DT"] = obj["CREATE_DT"];
                        dr["CHECK"] = false;
                        foreach (string col in listCol)
                            dr[col] = obj[col];
                       
                        _dt.Rows.Add(dr);
                    }
                }
                return;
            }
            else
            {
                return;
            }
        }
        public void getInventoryAll()
        {
            JObject jResult = new JObject();

            if (DBConnect.getComponentAll(_componentCd, ref jResult))
            {
                _dt.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    int index = 1;
                    List<string> listCol = ProjectInfo._dicDeviceFullColumn[_componentCd];

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        dr["NO"] = index++;
                        dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["COMPONENT"] = obj["COMPONENT"];
                        dr["CHECK"] = false;
                        foreach (string col in listCol)
                            dr[col] = obj[col];

                        _dt.Rows.Add(dr);
                    }
                }
                return;
            }
            else
            {
                return;
            }
        }

        public void setDataInitialize()
        {
            _id = 1;
            _dt.Clear();
        }

        public void addComponent(DataRow row)
        {
            DataRow dr = _dt.NewRow();

            dr["NO"] = _id++;
            dr["BARCODE"] = row["BARCODE"];
            dr["COMPONENT_ID"] = row["COMPONENT_ID"];
            dr["COMPONENT_CD"] = row["COMPONENT_CD"];
            dr["COMPONENT"] = row["COMPONENT"];
            dr["CREATE_DT"] = row["CREATE_DT"];
            dr["CHECK"] = false;
            foreach (string col in ProjectInfo._dicDeviceFullColumn[_componentCd])
                dr[col] = row[col];

            _dt.Rows.Add(dr);
        }

        public void addComponent(JObject obj)
        {
            DataRow dr = _dt.NewRow();

            dr["NO"] = _id++;
            dr["BARCODE"] = obj["BARCODE"];
            dr["COMPONENT_ID"] = obj["COMPONENT_ID"];
            dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
            dr["COMPONENT"] = obj["COMPONENT"];
            dr["CREATE_DT"] = obj["CREATE_DT"];
            dr["CHECK"] = false;
            foreach (string col in ProjectInfo._dicDeviceFullColumn[_componentCd])
                dr[col] = obj[col];

            _dt.Rows.Add(dr);
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
                //_adpType = rileAdpType.GetDisplayValueByKeyValue(_currentRow["ADP_CAT"]).ToString();
            }
        }
    }
}
