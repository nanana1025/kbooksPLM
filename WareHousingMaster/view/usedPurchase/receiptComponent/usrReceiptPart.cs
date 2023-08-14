using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.usedPurchase.receiptComponent
{
    public partial class usrReceiptPart : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dt { get; private set; }
        public BindingSource _bs { get; private set; }
        public DataRowView _currentRow { get; private set; }

        int _focusedRowHandle;
        int _topRowIndex;

        string _receipt;
        long _receiptId;

        int _sourceCd;

        public List<string> _listUsedPart;

        bool _isPartCntChange = true;

        public usrReceiptPart()
        {
            InitializeComponent();

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("PART_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("PARTCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("PART_CNT", typeof(long)));
            _dt.Columns.Add(new DataColumn("TOTAL_PRICE", typeof(long)));
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));


            _bs = new BindingSource();
            _listUsedPart = new List<string>();

            gcPart.DataSource = null;
            _bs.DataSource = _dt;
            gcPart.DataSource = _bs;

            DataTable dtComponentCd = Util.getCodeList("CD0101", "KEY", "VALUE");
            Util.LookupEditHelper(rileComponentCd, dtComponentCd, "KEY", "VALUE");

        }

        public void setinitialize(long receiptId, string receipt, int sourceCd)
        {
            _receiptId = receiptId;
            _receipt = receipt;
            _sourceCd = sourceCd;
        }

        public void resetInfo(long receiptId, string receipt, int sourceCd)
        {
            _receiptId = receiptId;
            _receipt = receipt;
            _sourceCd = sourceCd;
        }

        private void gvPart_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvPart.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

            }
        }

        public void getComponentAll()
        {
            JObject jResult = new JObject();

            bool isSuccess = false;

            if (_sourceCd == 0)
                isSuccess = DBUsedPurchase.getDanawaUsedPartList(_receiptId, _receipt, ref jResult);
            else
                isSuccess = DBUsedPurchase.getUsedPartList(_receiptId, _receipt, ref jResult);

            if (isSuccess)
            {
                _dt.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    int index = 1;

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();

                        dr["NO"] = index++;
                        dr["PART_ID"] = obj["PART_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["PARTCODE"] = obj["PARTCODE"];
                        if(ConvertUtil.ToString(obj["COMPONENT_CD"]).Equals("ETCMINUS"))
                            dr["PRICE"] = ConvertUtil.ToInt64(obj["PRICE"]) * -1;
                        else
                            dr["PRICE"] = obj["PRICE"];
                        dr["PART_CNT"] = obj["PART_CNT"];
                        dr["TOTAL_PRICE"] = ConvertUtil.ToInt64(dr["PRICE"]) * ConvertUtil.ToInt64(obj["PART_CNT"]);
                        dr["CHECK"] = false;
                        dr["STATE"] = 0;

                        _dt.Rows.Add(dr);

                        if (!_listUsedPart.Contains(ConvertUtil.ToString(dr["PARTCODE"])))
                            _listUsedPart.Add(ConvertUtil.ToString(dr["PARTCODE"]));
                    }
                }
                return;
            }
            else
            {
                return;
            }
        }

        public void deletePart()
        {
            JObject jResult = new JObject();

            JObject jobj = new JObject();
            jobj.Add("RECEIPT_ID", _receiptId);
            jobj.Add("RECEIPT", _receipt);
            jobj.Add("COMPONENT_CD", ConvertUtil.ToString(_currentRow["COMPONENT_CD"]));
            jobj.Add("USED_PART_ID", ConvertUtil.ToInt64(_currentRow["PART_ID"]));
            jobj.Add("PARTCODE", ConvertUtil.ToString(_currentRow["PARTCODE"]));
            jobj.Add("PART_CNT", ConvertUtil.ToInt64(_currentRow["PART_CNT"]));
            jobj.Add("SOURCE_CD", _sourceCd);

            if (DBUsedPurchase.deleteUsedPartComponent(jobj, ref jResult))
            {
                gvPart.BeginDataUpdate();
                _listUsedPart.Remove(ConvertUtil.ToString(_currentRow["PARTCODE"]));
                _currentRow.Delete();
                gvPart.EndDataUpdate();

                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }

        public void Clear()
        {
            _dt.Clear();
        }

        private void riseCnt_EditValueChanged(object sender, EventArgs e)
        {
            //SpinEdit View = sender as SpinEdit;
            //long partId = ConvertUtil.ToInt64(_currentRow["PART_ID"]);
            //JObject jResult = new JObject();

            //if (ConvertUtil.ToInt64(View.Value) < 0)
            //{
            //    Dangol.Message("양수만 입력 가능합니다.");
            //    View.Value = ConvertUtil.ToInt64(_currentRow["PART_CNT"]);
            //    return;
            //}

            //if (Dangol.MessageYN("선택하신 부품의 수량을 변경하시겠습니까?") == DialogResult.Yes)
            //{
            //    if (DBUsedPurchase.updateUsedPartCnt(_currentRow["PART_ID"], _receipt, View.Value, ref jResult))
            //    {
            //        _currentRow["TOTAL_PRICE"] = ConvertUtil.ToInt64(_currentRow["PRICE"]) * ConvertUtil.ToInt64(View.Value);
            //    }
            //    else
            //    {
            //        View.Value = ConvertUtil.ToInt64(_currentRow["PART_CNT"]);
            //        return;
            //    }
            //}
            //else
            //{
            //    View.Value = ConvertUtil.ToInt64(_currentRow["PART_CNT"]);
            //    return;
            //}
 
        }

        public void refresh()
        {
            _focusedRowHandle = gvPart.FocusedRowHandle;
            _topRowIndex = gvPart.TopRowIndex;
            getComponentAll();
            gvPart.FocusedRowHandle = _focusedRowHandle;
            gvPart.TopRowIndex = _topRowIndex;
        }

        public void save()
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("STATE = 2");

            var jArray = new JArray();
            JObject jobj = new JObject();
            JObject jResult = new JObject();
            jobj.Add("RECEIPT", _receipt);
            jobj.Add("RECEIPT_ID", _receiptId);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    JObject jdata = new JObject();
                    jdata.Add("USED_PART_ID", ConvertUtil.ToInt64(row["PART_ID"]));
                    jdata.Add("PARTCODE", ConvertUtil.ToString(row["PARTCODE"]));
                    jdata.Add("PART_CNT", ConvertUtil.ToString(row["PART_CNT"]));
                    jArray.Add(jdata);
                }

                jobj.Add("DATA", jArray);
                jobj.Add("SOURCE_CD", _sourceCd);

                if (DBUsedPurchase.updateUsedPartCnt(jobj, ref jResult))
                {
                    gvPart.BeginDataUpdate();

                    foreach (DataRow row in rows)
                    {
                        row["STATE"] = 0;
                    }

                    gvPart.EndDataUpdate();
                    Dangol.Message("저장되었습니다.");
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else
            {
                Dangol.Message("변경사항이 없습니다.");
            }




            //_totalCost -= ConvertUtil.ToInt64(_currentRow["TOTAL_PRICE"]);
            //_currentRow["PART_CNT"] = cnt;
            //_currentRow["TOTAL_PRICE"] = ConvertUtil.ToInt64(_currentRow["PRICE"]) * ConvertUtil.ToInt64(cnt);
            //_totalCost += ConvertUtil.ToInt64(_currentRow["TOTAL_PRICE"]);
        }

        public bool refreshCheck()
        {
            int rowhandle = gvPart.FocusedRowHandle;
            gvPart.FocusedRowHandle = -1;
            gvPart.FocusedRowHandle = rowhandle;

            DataRow[] rows = _dt.Select("STATE = 2");

            if (rows.Length > 0)
                return false;
            else
                return true;
        }

        private void gvPart_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "PRICE" || e.Column.FieldName == "TOTAL_PRICE")
            {
                long price = ConvertUtil.ToInt64(View.GetRowCellValue(e.RowHandle, View.Columns[e.Column.FieldName]));
                
                if(price < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }

            if(e.Column.FieldName == "PART_CNT")
            {
                string state = View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATE"]);

                if (state.Equals("2"))
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Lime);
                }
            }
        }

        private void gvPart_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _currentRow["STATE"] = 2;

            _currentRow["TOTAL_PRICE"] = ConvertUtil.ToInt64(_currentRow["PRICE"]) * ConvertUtil.ToInt64(_currentRow["PART_CNT"]);
        }
    }
}
