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
    public partial class usrComponentTBL : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; private set; }

        public string _tblManufacture { get; private set; }
        public string _tblModelNm { get; private set; }
        public string _tblCapacity { get; private set; }

        //public string _adpType { get; private set; }

        int _id;
        public string _componentCd { get; set; }

        public usrComponentTBL()
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
            foreach (string col in ProjectInfo._dicDeviceFullColumn["TBL"])
                _dt.Columns.Add(new DataColumn(col, typeof(string)));
            _dt.Columns.Add(new DataColumn("CREATE_DT", typeof(string)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            

            gcPart.DataSource = null;
            _bs.DataSource = _dt;
            gcPart.DataSource = _bs;

            _id = 1;

            DataTable dtCat = Util.getCodeList("CD1601", "KEY", "VALUE");
            Util.LookupEditHelper(rileManufature, dtCat, "KEY", "VALUE");

            DataTable dtType = Util.getCodeList("CD1602", "KEY", "VALUE");
            Util.LookupEditHelper(rileModelNm, dtType, "KEY", "VALUE");

            DataTable dtClass = Util.getCodeList("CD1603", "KEY", "VALUE");
            Util.LookupEditHelper(rileCapacity, dtClass, "KEY", "VALUE");
        }

        public void setColumnHide()
        {
            gcInventoryId.Visible = false;
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
                        foreach (string col in ProjectInfo._dicDeviceFullColumn["TBL"])
                            dr[col] = obj[col];
                        dr["CREATE_DT"] = ConvertUtil.ToDateTimeNull(obj["CREATE_DT"]);
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

                if (!string.IsNullOrEmpty(ConvertUtil.ToString(_currentRow["MANUFACTURE_NM"])))
                    _tblManufacture = rileManufature.GetDisplayValueByKeyValue(_currentRow["MANUFACTURE_NM"]).ToString();
                else
                    _tblManufacture = "-1";

                if (!string.IsNullOrEmpty(ConvertUtil.ToString(_currentRow["MODEL_NM"])))
                    _tblModelNm = rileModelNm.GetDisplayValueByKeyValue(_currentRow["MODEL_NM"]).ToString();
                else
                    _tblModelNm = "-1";

                if (!string.IsNullOrEmpty(ConvertUtil.ToString(_currentRow["CAPACITY"])))
                    _tblCapacity = rileCapacity.GetDisplayValueByKeyValue(_currentRow["CAPACITY"]).ToString();
                else
                    _tblCapacity = "-1";
            }
            else 
            {
                _tblManufacture = "-1";
                _tblModelNm = "-1";
                _tblCapacity = "-1";
            }

        }
    }
}
