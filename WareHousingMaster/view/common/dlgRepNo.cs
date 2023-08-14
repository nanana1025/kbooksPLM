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

namespace WareHousingMaster.view.common
{
    public partial class dlgRepNo : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtList;
        BindingSource _bs;

        DataRowView _currentRow;
        string _repType;
        int _repValue;

        public dlgRepNo()
        {
            InitializeComponent();

            _dtList = new DataTable();
            _dtList.Columns.Add(new DataColumn("REP_NO", typeof(string)));
            _dtList.Columns.Add(new DataColumn("SEQ", typeof(int)));
            _dtList.Columns.Add(new DataColumn("CHECK", typeof(bool)));

            _bs = new BindingSource();

            _repType = "입고번호";
            _repValue = 1;


        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            DataTable dtNo = new DataTable();

            dtNo.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtNo.Columns.Add(new DataColumn("VALUE", typeof(string)));

            DataRow row = dtNo.NewRow();
            row["KEY"] = 1;
            row["VALUE"] = "입고번호";
            dtNo.Rows.Add(row);

            DataRow row1 = dtNo.NewRow();
            row1["KEY"] = 2;
            row1["VALUE"] = "생산번호";
            dtNo.Rows.Add(row1);

            Util.LookupEditHelper(leRepType, dtNo, "KEY", "VALUE");

            gcList.DataSource = null;
            _bs.DataSource = _dtList;
            gcList.DataSource = _bs;


            leRepType.EditValue = 1;
        }

        private void sbInput_Click(object sender, EventArgs e) { 

            string data = teNo.Text;
            putDataToGrid(data);   
            teNo.Text = "";
        }

        private void putDataToGrid(string data)
        {
            string repNo = data.ToUpper();
            int seq = ConvertUtil.ToInt32(_dtList.Compute("MAX(SEQ)", "")) + 1;
            JObject jResult = new JObject();
            DataRow dr = _dtList.NewRow();
            dr["REP_NO"] = repNo;
            dr["SEQ"] = seq;
            dr["CHECK"] = false;
            _dtList.Rows.Add(dr);

            DBConnect.InsertRepNo(_repValue, repNo, seq, ref jResult);

        }


        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                int rowhandle = gvList.FocusedRowHandle;
                gvList.FocusedRowHandle = -1;
                gvList.FocusedRowHandle = rowhandle;

                DataRow[] rows = _dtList.Select("CHECK = True");
                if (rows.Length < 1) 
                    Dangol.Message("체크된 번호가 없습니다.");
                else
                {
                    if (Dangol.MessageYN("선택하신 번호를 목록에서 삭제하시겠습니까?") == DialogResult.Yes)
                    {
                        JObject jResult = new JObject();
                        List<string> listData = new List<string>();
                        _dtList.BeginInit();
                        foreach (DataRow row in rows)
                        {
                            listData.Add(ConvertUtil.ToString(row["REP_NO"]));
                            row.Delete();
                        }
                        _dtList.EndInit();

                        DBConnect.deleteRepNo(_repValue, listData, ref jResult);
                    }
                }

            }
        }
        private void lcgBarcodeList_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            bool ischeck = e.Button.IsChecked.Value;
            if (ischeck)
            {
                _dtList.BeginInit();
                foreach (DataRow row in _dtList.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = ischeck;
                    row.EndEdit();
                }
                _dtList.EndInit();
            } 
        }

        private void lcgBarcodeList_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            bool ischeck = e.Button.IsChecked.Value;
            if (!ischeck)
            {
                _dtList.BeginInit();
                foreach (DataRow row in _dtList.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = ischeck;
                    row.EndEdit();
                }
                _dtList.EndInit();
            }
        }

        
        private void sbSave_Click(object sender, EventArgs e)
        {
            if (_dtList.Rows.Count < 0)
                Dangol.Message("입력된 번호가 없습니다.");
            else
            {
                if (Dangol.MessageYN($"{_repType}를 저장하시겠습니까?") == DialogResult.Yes)
                {
                    JObject jResult = new JObject();

                    List<string> listData = new List<string>();
                    List<int> listSeq = new List<int>();

                    foreach (DataRow row in _dtList.Rows)
                    {
                        listData.Add(ConvertUtil.ToString(row["REP_NO"]));
                        listSeq.Add(ConvertUtil.ToInt32(row["SEQ"]));
                    }

                    DBConnect.updateRepNo(_repValue, listData, listSeq, ref jResult);

                    Dangol.Message("저장되었습니다.");
                    //this.DialogResult = DialogResult.OK;
                }
            }
        }


        private void getRepNo()
        {
            JObject jResult = new JObject();

            _dtList.Clear();
            if (DBConnect.getRepNo(_repValue, ref jResult))
            {

                if (ConvertUtil.ToBoolean(jResult["DATA_EXIST"]))
                {
                    gvList.FocusedRowObjectChanged -= gvList_FocusedRowObjectChanged;
                    gvList.BeginDataUpdate();

                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dtList.NewRow();


                        dr["REP_NO"] = obj["REP_NO"];
                        dr["SEQ"] = obj["SEQ"];
                        dr["CHECK"] = false;
                        _dtList.Rows.Add(dr);
                    }

                    gvList.EndDataUpdate();
                    gvList.FocusedRowObjectChanged += gvList_FocusedRowObjectChanged;
                }

            }

        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvList_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
            }
        }

        private void leRepType_EditValueChanged(object sender, EventArgs e)
        {
            _repType = leRepType.Text;
            _repValue = ConvertUtil.ToInt32(leRepType.EditValue);
            lcNoNm.Text = _repType;
            gcName.Caption = _repType;

            getRepNo();
        }
    }
}