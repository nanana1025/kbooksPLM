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
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;

namespace WareHousingMaster.view.release.External
{
    public partial class dlgSelectReceiveUser : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dt;
        BindingSource _bs;
        DataRowView _currentRow;
        TreeListNode _FocusedNode = null;

        List<string> _listReceiver;

        public dlgSelectReceiveUser(TreeListNode FocusedNode, List<string> listReceiver)
        {
            InitializeComponent();

            _listReceiver = listReceiver;
            _FocusedNode = FocusedNode;

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            _dt.Columns.Add(new DataColumn("USER_ID", typeof(string)));
            _dt.Columns.Add(new DataColumn("CLASS", typeof(string)));
            _dt.Columns.Add(new DataColumn("DEPT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("EXIST", typeof(int)));

            _bs = new BindingSource();
            _bs.DataSource = _dt;
        }
        
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            gcUser.DataSource = null;
            gcUser.DataSource = _bs;

            Util.LookupEditHelper(leUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

            DataTable dtDeptNm = Util.getCodeList("CD0502", "KEY", "VALUE");
            Util.LookupEditHelper(rileDeptNm, dtDeptNm, "KEY", "VALUE");

            DataTable dtClass = Util.getCodeList("CD0504", "KEY", "VALUE");
            Util.LookupEditHelper(rileClass, dtClass, "KEY", "VALUE");

            DataTable dtCompanyUserId = Util.getCodeListCustom("TN_USER_MST", "USER_ID", "USER_NM", $"COMPANY_ID = '{ProjectInfo._userCompanyId}' AND STATE_CD = 'A'", "USER_ID ASC");
            Util.LookupEditHelper(rileUserId, dtCompanyUserId, "KEY", "VALUE");

            getUserList();


            leUserId.EditValue = ProjectInfo._userId;

        }

        private void getUserList()
        {
            JObject jResult = new JObject();
            JObject jobj = new JObject();
            jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);
            
            if (DBRelease.getUserInfo(jobj,  ref jResult))
            {
                string userId;

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        userId = ConvertUtil.ToString(obj["USER_ID"]);
                        DataRow dr = _dt.NewRow();

                        dr["USER_ID"] = obj["USER_ID"];
                        dr["CLASS"] = obj["CLASS"];
                        dr["DEPT_CD"] = obj["DEPT_CD"];

                        if (_listReceiver.Contains(userId))
                        {
                            dr["CHECK"] = true;
                            dr["EXIST"] = 1;
                        }
                        else
                        {
                            dr["CHECK"] = false;
                            dr["EXIST"] = 0;
                        }

                        _dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            } 
        }

        private void insertarcodeCustom()
        {

            //Dangol.ShowSplash();

            //JObject jResult = new JObject();
            //JObject jobj = new JObject();
            //jobj.Add("BARCODE", "");
            //jobj.Add("ORDER_ID", ConvertUtil.ToInt64(_rowInfo["ORDER_ID"]));
            //jobj.Add("ORDER_PART_ID", ConvertUtil.ToInt64(_rowInfo["ORDER_PART_ID"]));
            //jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_rowInfo["EXPORT_ID"]));
            //jobj.Add("INVENTORY_ID", -1);
            //jobj.Add("COMPONENT_CD", ConvertUtil.ToString(leUserId.EditValue));
            //jobj.Add("MODEL_NM", teModelNm.Text);
            //jobj.Add("CNT", ConvertUtil.ToInt32(seCnt.EditValue));
            //jobj.Add("TYPE", 2);

            //if (DBRelease.insertOrderReleasePart(jobj, ref jResult))
            //{
            //    _isChanged = true;
            //    gvPart.BeginDataUpdate();

            //    DataRow dr = _dt.NewRow();
            //    dr["NO"] = 0;
            //    dr["ORDER_EXPORT_PART_ID"] = jResult["ORDER_EXPORT_PART_ID"];
            //    dr["INVENTORY_ID"] = jResult["INVENTORY_ID"];
            //    dr["COMPONENT_CD"] = jResult["COMPONENT_CD"];
            //    dr["BARCODE"] = jResult["BARCODE"];
            //    dr["MODEL_NM"] = jResult["MODEL_NM"];
            //    dr["CNT"] = jResult["CNT"];
            //    _dt.Rows.Add(dr);

            //    foreach (DataRow obj in _dt.Rows)
            //        dr["NO"] = ConvertUtil.ToInt32(obj["NO"]) + 1;

            //    gvPart.EndDataUpdate();

            //    Dangol.CloseSplash();
            //    Dangol.Message(ConvertUtil.ToString("추가되었습니다"));
            //}
            //else
            //{
            //    Dangol.CloseSplash();
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}
        }


        private void sbOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void gvUser_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "CHECK")
            {
                int exist = ConvertUtil.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["EXIST"]));

                if (exist == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.LightGray);
                }
            }
        }

        private void gvUser_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvUser.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = e.Row as DataRowView;

                //if (ConvertUtil.ToInt32(_currentRow["EXIST"]) == 1)
                //    gcCheck.OptionsColumn.ReadOnly = true;
                //else
                //    gcCheck.OptionsColumn.ReadOnly = false;
            }
            else
            {
                _currentRow = null;
                //gcCheck.OptionsColumn.ReadOnly = true;
            }
        }

        private void sbSelect_Click(object sender, EventArgs e)
        {

            DataRow[] rows = _dt.Select("(CHECK = TRUE AND EXIST = 0) OR (CHECK = FALSE AND EXIST = 1)");

            if (rows.Length < 1)
            {
                Dangol.Message("변경사항이 없습니다.");
                return;
            }

            if (Dangol.MessageYN("수정하시겠습니까") == DialogResult.Yes)
            {
                rows = _dt.Select("CHECK = TRUE AND EXIST = 0");

                if (rows.Length > 0)
                {
                    List<string> listUserId = new List<string>();

                    foreach (DataRow row in rows)
                        listUserId.Add(ConvertUtil.ToString(row["USER_ID"]));

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(_FocusedNode["RECEIPT_ID"]));
                    jobj.Add("LIST_USER_ID", string.Join(",", listUserId));
                    //jobj.Add("CHARGE_USER_ID", ConvertUtil.ToString(leUserId.EditValue));

                    if (DBRelease.insertMsgReceiverList(jobj, ref jResult))
                    {

                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        return;
                    }
                }

                rows = _dt.Select("CHECK = FALSE AND EXIST = 1");

                if (rows.Length > 0)
                {
                    List<string> listUserId = new List<string>();

                    foreach (DataRow row in rows)
                        listUserId.Add(ConvertUtil.ToString(row["USER_ID"]));

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    jobj.Add("RECEIPT_ID", ConvertUtil.ToInt64(_FocusedNode["RECEIPT_ID"]));
                    jobj.Add("LIST_USER_ID", string.Join(",", listUserId));

                    if (DBRelease.deleteMsgReceiverList(jobj, ref jResult))
                    {

                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                        return;
                    }
                }

                Dangol.Message(ConvertUtil.ToString("수정되었습니다"));
                this.DialogResult = DialogResult.OK;
            }
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}