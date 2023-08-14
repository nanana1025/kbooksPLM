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

namespace WareHousingMaster.view.usedPurchase
{
    public partial class dlgFaultPart : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dt;
        BindingSource _bs;
        string _receipt;

        DataRowView _currentRow = null;
        List<string> _listUsedPart;
        string _componentCd;

        long _warehousingId;

        bool _isSaved = false;

        public dlgFaultPart(string receipt, List<string> listUsedPart)
        {
            InitializeComponent();

            _receipt = receipt;
            _listUsedPart = listUsedPart;

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("FAULT_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("PART_CNT", typeof(int)));
            _dt.Columns.Add(new DataColumn("INVENTORY_CAT", typeof(string)));
            _dt.Columns.Add(new DataColumn("DES", typeof(string)));
            _dt.Columns.Add(new DataColumn("STATE", typeof(int)));
            //state, 0:없음, 1:삽입, 2:수정

            _bs = new BindingSource();

            _bs.DataSource = _dt;

            _warehousingId = -1;

            _isSaved = false;

        }
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            gcFault.DataSource = null;
            gcFault.DataSource = _bs;

            DataTable dtLTComponent = new DataTable();

            dtLTComponent.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtLTComponent.Columns.Add(new DataColumn("VALUE", typeof(string)));

            DataRow row = dtLTComponent.NewRow();
            row["KEY"] = " ";
            row["VALUE"] = "선택";
            dtLTComponent.Rows.Add(row);

            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr = dtLTComponent.NewRow();
                dr["KEY"] = ProjectInfo._componetCd[i];
                dr["VALUE"] = ProjectInfo._componetNm[i];
                dtLTComponent.Rows.Add(dr);
            }

            Util.LookupEditHelper(rileComponentCd, dtLTComponent, "KEY", "VALUE");

            DataTable dtInventoryCat = Util.getCodeList("CD0303", "KEY", "VALUE");
            Util.LookupEditHelper(rileInventoryCat, dtInventoryCat, "KEY", "VALUE");

            getFaultPart();
        }

        private void gvComponent_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvFault.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }

        private void gvComponent_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvFault.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvFault.GetRow(e.FocusedRowHandle) as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }

        private void getFaultPart()
        {
            _dt.Clear();

            JObject jResult = new JObject();

            if (DBUsedPurchase.getFaultList(-1, _receipt, ref jResult))
            {
                _warehousingId = ConvertUtil.ToInt64(jResult["WAREHOUSING_ID"]);

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["LIST_DATA"].ToString());

                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        DataRow dr = _dt.NewRow();
                        dr["FAULT_ID"] = obj["FAULT_ID"];
                        dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                        dr["MODEL_NM"] = obj["MODEL_NM"];
                        dr["PART_CNT"] = obj["PART_CNT"];
                        dr["INVENTORY_CAT"] = obj["INVENTORY_CAT"];
                        dr["DES"] = obj["DES"];
                        dr["STATE"] = 0;
                        _dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }
        private void lcgBarcodeList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                DataRow dr = _dt.NewRow();
                dr["FAULT_ID"] = -1;
                dr["COMPONENT_CD"] = " ";
                dr["MODEL_NM"] = "";
                dr["PART_CNT"] = 1;
                dr["INVENTORY_CAT"] = "F";
                dr["DES"] = "";
                dr["STATE"] = 1;
                _dt.Rows.Add(dr);
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (_currentRow == null)
                {
                    Dangol.Message("선택하신 불량내역이 없습니다.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 불량부품 정보를 삭제하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

                if (state == 1)
                {
                    gvFault.BeginUpdate();
                    _currentRow.Delete();
                    gvFault.EndUpdate();

                    Dangol.Message("삭제되었습니다.");
                }
                else
                {
                    JObject jResult = new JObject();
                    JObject jobj = new JObject();

                    jobj.Add("WAREHOUSING_ID", _warehousingId);
                    jobj.Add("FAULT_ID", ConvertUtil.ToInt64(_currentRow["FAULT_ID"]));

                    if (DBUsedPurchase.deleteFaultList(jobj, ref jResult))
                    {
                        gcFault.BeginUpdate();
                        _currentRow.Delete();
                        gcFault.EndUpdate();

                        _isSaved = true;
                        Dangol.Message("삭제되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }

            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                int rowhandle = gvFault.FocusedRowHandle;
                gvFault.FocusedRowHandle = -1;
                gvFault.FocusedRowHandle = rowhandle;

                if (Dangol.MessageYN("불량 부품정보를 저장하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                DataRow[] rowsUpdate = _dt.Select("STATE = 2");
                DataRow[] rowsInsert = _dt.Select("STATE = 1");

                if (rowsUpdate.Length < 1 && rowsInsert.Length < 1)
                {
                    Dangol.Message("변경사항이 없습니다.");
                    return;
                }

                var jArray = new JArray();
                JObject jobj = new JObject();
                JObject jResult = new JObject();
                jobj.Add("WAREHOUSING_ID", _warehousingId);

                bool isUpdateComplete = false;
                bool isInsertComplete = false;

                if (rowsUpdate.Length > 0)
                {
                    foreach (DataRow row in rowsUpdate)
                    {
                        JObject jdata = new JObject();
                        jdata.Add("FAULT_ID", ConvertUtil.ToInt64(row["FAULT_ID"]));
                        jdata.Add("COMPONENT_CD", ConvertUtil.ToString(row["COMPONENT_CD"]));
                        jdata.Add("PART_CODE", "-1");
                        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["MODEL_NM"]));
                        jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["PART_CNT"]));
                        jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                        jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                        jArray.Add(jdata);
                    }

                    jobj.Add("DATA", jArray);


                    if (DBUsedPurchase.updateFaultList(jobj, ref jResult))
                    {
                        foreach (DataRow row in rowsUpdate)
                        {
                            row["STATE"] = 0;
                        }

                        _isSaved = true;
                        isUpdateComplete = true;
                        //Dangol.Message("저장되었습니다.");
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }

                if (rowsInsert.Length > 0)
                {
                    int index = 1;
                    foreach (DataRow row in rowsInsert)
                    {
                        JObject jdata = new JObject();
                        row["FAULT_ID"] = index * -1;
                        jdata.Add("FAULT_ID", index * -1);
                        jdata.Add("COMPONENT_CD", ConvertUtil.ToString(row["COMPONENT_CD"]));
                        jdata.Add("PART_CODE", "-1");
                        jdata.Add("MODEL_NM", ConvertUtil.ToString(row["MODEL_NM"]));
                        jdata.Add("PART_CNT", ConvertUtil.ToInt32(row["PART_CNT"]));
                        jdata.Add("INVENTORY_CAT", ConvertUtil.ToString(row["INVENTORY_CAT"]));
                        jdata.Add("DES", ConvertUtil.ToString(row["DES"]));
                        jArray.Add(jdata);
                        index++;
                    }

                    jobj.Add("DATA", jArray);

                    if (DBUsedPurchase.insertFaultList(jobj, ref jResult))
                    {
                        foreach (DataRow row in rowsInsert)
                        {
                            row["FAULT_ID"] = jResult[ConvertUtil.ToString(row["FAULT_ID"])];
                            row["STATE"] = 0;
                        }

                        _isSaved = true;
                        isInsertComplete = true;

                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }

                if (isUpdateComplete || isInsertComplete)
                {
                    Dangol.Message("저장되었습니다.");
                }
            }
        }

        private void gvFault_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int state = ConvertUtil.ToInt32(_currentRow["STATE"]);

            if (state == 0)
                _currentRow["STATE"] = 2;
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            if (_isSaved)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
        }
    }
}