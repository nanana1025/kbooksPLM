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

namespace WareHousingMaster.view.warehousing.componentCompare
{
    public partial class usrComponentCompare : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; private set; }

        public string _adpType { get; private set; }

        public string _componentCd { get; set; }

        public usrComponentCompare()
        {
            InitializeComponent();

            _dt = new DataTable();
            _bs = new BindingSource();

        }

        public void setinitialize()
        {
            DataTable dtComponentCd = Util.getCodeList("CD0101", "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

            DataTable dtInvnetoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryCat, dtInvnetoryCat, "KEY", "VALUE");

            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("COMPONENT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("MANUFACTURE_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("ADP_CAT", typeof(string)));
            _dt.Columns.Add(new DataColumn("OUTPUT_WATT", typeof(string)));
            _dt.Columns.Add(new DataColumn("OUTPUT_AMPERE", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            

            //gcADP.DataSource = null;
            //_bs.DataSource = _dt;
            //gcADP.DataSource = _bs;
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
    }
}
