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

namespace WareHousingMaster.view.release.External
{
    public partial class dlgReleasePart : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dt;
        BindingSource _bs;

        DataRowView _rowInfo = null;

        public bool _isChanged { get; private set; }

        Regex regex1;
        Regex regex2;
        Regex regex3;

        public dlgReleasePart(DataRowView rowInfo, DataTable dt)
        {
            InitializeComponent();

            _rowInfo = rowInfo;

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("NO", typeof(int)));
            _dt.Columns.Add(new DataColumn("ORDER_EXPORT_PART_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("INVENTORY_ID", typeof(long)));
            _dt.Columns.Add(new DataColumn("COMPONENT_CD", typeof(string)));
            _dt.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("CNT", typeof(int)));

            foreach(DataRow obj in dt.Rows)
            {
                DataRow dr = _dt.NewRow();
                dr["NO"] = obj["NO"];
                dr["ORDER_EXPORT_PART_ID"] = obj["ORDER_EXPORT_PART_ID"];
                dr["INVENTORY_ID"] = obj["INVENTORY_ID"];
                dr["COMPONENT_CD"] = obj["COMPONENT_CD"];
                dr["BARCODE"] = obj["BARCODE"];
                dr["MODEL_NM"] = obj["MODEL_NM"];
                dr["CNT"] = obj["CNT"];
                _dt.Rows.Add(dr);
            }

            _bs = new BindingSource();
            _bs.DataSource = _dt;

            _isChanged = false;

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");

        }
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            gcPart.DataSource = null;
            gcPart.DataSource = _bs;

            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr = dtComponentCd.NewRow();
                dr["KEY"] = ProjectInfo._componetCd[i];
                dr["VALUE"] = ProjectInfo._componetNm[i];
                dtComponentCd.Rows.Add(dr);
            }
            Util.insertRowonTop(dtComponentCd, "-1", "기타");
            Util.LookupEditHelper(rileComponentCd1, dtComponentCd, "KEY", "VALUE");
            Util.LookupEditHelper(leComponentCd, dtComponentCd, "KEY", "VALUE");
            
            leComponentCd.EditValue = "CPU";
        }

        private void sbInsert_Click(object sender, EventArgs e)
        {
            
        }

        private void ceCheck_CheckedChanged(object sender, EventArgs e)
        {
            Root.BeginUpdate();
            if (ceCheck.CheckState == CheckState.Checked)
            {
                teBarcode.Text = "";
                teBarcode.ReadOnly = true;

                leComponentCd.ReadOnly = false;
                teModelNm.ReadOnly = false;
                seCnt.ReadOnly = false;


                lcModelNm1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcComponentCd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcModelNm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcPartCnt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            }
            else
            {
                teBarcode.ReadOnly = false;

                teModelNm.Text = "";
                seCnt.EditValue = 1;
                leComponentCd.ReadOnly = true;
                teModelNm.ReadOnly = true;
                seCnt.ReadOnly = true;

                lcModelNm1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcComponentCd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcModelNm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcPartCnt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            Root.EndUpdate();
        }

        private void sbAddBarcode_Click(object sender, EventArgs e)
        {
            if (ceCheck.CheckState == CheckState.Checked)
            {
                insertarcodeCustom();
            }
            else
            {
                insertarcode();
            }
        }

        private void insertarcode()
        { 
            string barcode = teBarcode.Text.ToUpper().Trim();

            if (barcode.Length < 1 || string.IsNullOrWhiteSpace(barcode))
                return;

            if (barcode.Length == 12 && (regex1.IsMatch(barcode) || regex2.IsMatch(barcode) || regex3.IsMatch(barcode)))
            {
                teBarcode.Text = "";
                DataRow[] rows = _dt.Select($"BARCODE = '{barcode}'");

                if(rows.Length > 0)
                {
                    Dangol.Message("이미 존재하는 부품입니다.");
                }
                else
                {
                    Dangol.ShowSplash();

                    JObject jResult = new JObject();
                    JObject jobj = new JObject();
                    jobj.Add("BARCODE", barcode);
                    jobj.Add("ORDER_ID", ConvertUtil.ToInt64(_rowInfo["ORDER_ID"]));
                    jobj.Add("ORDER_PART_ID", ConvertUtil.ToInt64(_rowInfo["ORDER_PART_ID"]));
                    jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_rowInfo["EXPORT_ID"]));
                    jobj.Add("INVENTORY_ID", -1);
                    jobj.Add("MODEL_NM", "");
                    jobj.Add("COMPONENT_CD", "-1");
                    jobj.Add("CNT", 1);
                    jobj.Add("TYPE", 1);

                    if (DBRelease.insertOrderReleasePart(jobj,  ref jResult))
                    {
                        _isChanged = true;
                        gvPart.BeginDataUpdate();

                        DataRow dr = _dt.NewRow();
                        dr["NO"] = 0;
                        dr["ORDER_EXPORT_PART_ID"] = jResult["ORDER_EXPORT_PART_ID"];
                        dr["INVENTORY_ID"] = jResult["INVENTORY_ID"];
                        dr["COMPONENT_CD"] = jResult["COMPONENT_CD"];
                        dr["BARCODE"] = jResult["BARCODE"];
                        dr["MODEL_NM"] = jResult["MODEL_NM"];
                        dr["CNT"] = jResult["CNT"];
                        _dt.Rows.Add(dr);

                        foreach (DataRow obj in _dt.Rows)
                            dr["NO"] = ConvertUtil.ToInt32(obj["NO"]) + 1;

                        gvPart.EndDataUpdate();

                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString("추가되었습니다"));
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }

        private void insertarcodeCustom()
        {

            Dangol.ShowSplash();

            JObject jResult = new JObject();
            JObject jobj = new JObject();
            jobj.Add("BARCODE", "");
            jobj.Add("ORDER_ID", ConvertUtil.ToInt64(_rowInfo["ORDER_ID"]));
            jobj.Add("ORDER_PART_ID", ConvertUtil.ToInt64(_rowInfo["ORDER_PART_ID"]));
            jobj.Add("EXPORT_ID", ConvertUtil.ToInt64(_rowInfo["EXPORT_ID"]));
            jobj.Add("INVENTORY_ID", -1);
            jobj.Add("COMPONENT_CD", ConvertUtil.ToString(leComponentCd.EditValue));
            jobj.Add("MODEL_NM", teModelNm.Text);
            jobj.Add("CNT", ConvertUtil.ToInt32(seCnt.EditValue));
            jobj.Add("TYPE", 2);

            if (DBRelease.insertOrderReleasePart(jobj, ref jResult))
            {
                _isChanged = true;
                gvPart.BeginDataUpdate();

                DataRow dr = _dt.NewRow();
                dr["NO"] = 0;
                dr["ORDER_EXPORT_PART_ID"] = jResult["ORDER_EXPORT_PART_ID"];
                dr["INVENTORY_ID"] = jResult["INVENTORY_ID"];
                dr["COMPONENT_CD"] = jResult["COMPONENT_CD"];
                dr["BARCODE"] = jResult["BARCODE"];
                dr["MODEL_NM"] = jResult["MODEL_NM"];
                dr["CNT"] = jResult["CNT"];
                _dt.Rows.Add(dr);

                foreach (DataRow obj in _dt.Rows)
                    dr["NO"] = ConvertUtil.ToInt32(obj["NO"]) + 1;

                gvPart.EndDataUpdate();

                Dangol.CloseSplash();
                Dangol.Message(ConvertUtil.ToString("추가되었습니다"));
            }
            else
            {
                Dangol.CloseSplash();
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }


        private void lgcOrderPart_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(2))
            {
                //ORDER_EXPOCRT_PART_ID;

                int[] selectedIdxes = gvPart.GetSelectedRows();

                if (selectedIdxes.Length < 1)
                {
                    Dangol.Message("선택한 부품이 없습니다.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(teModelNm.Text))
                {
                    Dangol.Message("모델명을 입력하세요.");
                    return;
                }

                if (Dangol.MessageYN("선택하신 품목을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    Dangol.ShowSplash();

                    JObject jobj = new JObject();
                    JObject jResult = new JObject();

                    List<object> listId = new List<object>();

                    for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                    {
                        DataRowView row = gvPart.GetRow(selectedIdxes[i]) as DataRowView;
                        listId.Add(row["ORDER_EXPORT_PART_ID"]);
                    }

                    jobj.Add("LIST_ORDER_EXPORT_PART_ID", string.Join(",", listId));
                    if (ProjectInfo._userCompanyId != 2)
                        jobj.Add("COMPANY_ID", ProjectInfo._userCompanyId);

                    if (DBRelease.deleteOrderReleasePart(jobj, ref jResult))
                    {
                        _isChanged = true;

                        gvPart.BeginDataUpdate();

                        for (int i = selectedIdxes.Count() - 1; i >= 0; i--)
                        {
                            DataRowView row = gvPart.GetRow(selectedIdxes[i]) as DataRowView;
                            row.Delete();
                        }

                        gvPart.EndDataUpdate();

                        Dangol.CloseSplash();
                        Dangol.Message("삭제되었습니다.");
                    }
                    else
                    {
                        Dangol.CloseSplash();
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }
        private void sbOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void teBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (ceCheck.CheckState == CheckState.Unchecked)
                    insertarcode();
            }
           
        }
    }
}