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
    public partial class dlgNewPart : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dt;
        BindingSource _bs;
        long _receiptId;
        string _receipt;
        int _sourceCd;

        DataRowView _currentRow = null;
        List<string> _listUsedPart;
        string _componentCd;

        bool _isCustom = false;

        public int Cnt { get; private set; }

        public dlgNewPart(long receiptId, string receipt, List<string> listUsedPart, int sourceCd)
        {
            InitializeComponent();

            _receiptId = receiptId;
            _receipt = receipt;
            _listUsedPart = listUsedPart;
            _sourceCd = sourceCd;

            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("PARTCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(long)));


            _bs = new BindingSource();

            _bs.DataSource = _dt;

            _isCustom = false;

        }
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            gcComponent.DataSource = null;
            gcComponent.DataSource = _bs;

            DataTable dtLTComponent = new DataTable();

            dtLTComponent.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtLTComponent.Columns.Add(new DataColumn("VALUE", typeof(string)));

            DataRow row = dtLTComponent.NewRow();
            row["KEY"] = "-1";
            row["VALUE"] = "선택";
            dtLTComponent.Rows.Add(row);

            for (int i = 0; i < ProjectInfo._LTComponentCd.Length; i++)
            {
                DataRow dr = dtLTComponent.NewRow();
                dr["KEY"] = ProjectInfo._LTComponentCd[i];
                dr["VALUE"] = ProjectInfo._LTComponentCd[i];
                dtLTComponent.Rows.Add(dr);
            }

            DataRow row1 = dtLTComponent.NewRow();
            row1["KEY"] = "ETCADD";
            row1["VALUE"] = "기타추가";
            dtLTComponent.Rows.Add(row1);

            DataRow row2 = dtLTComponent.NewRow();
            row2["KEY"] = "ETCMINUS";
            row2["VALUE"] = "기타감소";
            dtLTComponent.Rows.Add(row2);

            Util.LookupEditHelper(leComponentCd, dtLTComponent, "KEY", "VALUE");
            leComponentCd.EditValueChanged -= leComponentCd_EditValueChanged;
            leComponentCd.ItemIndex = 0;
            leComponentCd.EditValueChanged += leComponentCd_EditValueChanged;
        }

        private void gvComponent_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvComponent.RowCount > 0);

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
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvComponent.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvComponent.GetRow(e.FocusedRowHandle) as DataRowView;
            }
            else
            {
                _currentRow = null;
            }
        }

        private void sbCustom_Click(object sender, EventArgs e)
        {
            _isCustom = true;
            lcModelNm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcPrice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcSelect.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcCustom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            gcComponent.Enabled = false;

        }

        private void sbSelect_Click(object sender, EventArgs e)
        {
            _isCustom = false;
            lcModelNm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcPrice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcSelect.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcCustom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            gcComponent.Enabled = true;
        }

        private void leComponentCd_EditValueChanged(object sender, EventArgs e)
        {
            gcComponent.Enabled = true;
            _isCustom = false;
            lcModelNm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcPrice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcSelect.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcCustom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
            if (componentCd.Equals(" "))
            {
                return;
            }

            JObject jResult = new JObject();

            if (DBUsedPurchase.getLTComonent(componentCd, ref jResult))
            {
                _componentCd = componentCd;
                _dt.Clear();

                if (Convert.ToBoolean(jResult["EXIST"]))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string partCode = ConvertUtil.ToString(obj["PARTCODE"]);

                        //if (!_listUsedPart.Contains(partCode))
                        {
                            DataRow dr = _dt.NewRow();
                            dr["PARTCODE"] = partCode;
                            if(_componentCd.Equals("노트북"))
                                dr["MODEL_NM"] = $"{obj["CATEGORY2"]}/{obj["MODEL_NM"]}";
                            else
                                dr["MODEL_NM"] = obj["MODEL_NM"];
                            dr["CATEGORY"] = obj["CATEGORY"];
                            dr["PRICE"] = obj["PRICE"];
                            _dt.Rows.Add(dr);
                        }
                    }
                }
            }
            else
            {
                Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            }
        }


        private void sbInsert_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();

            if (leComponentCd.EditValue.Equals("-1"))
            {
                Dangol.Message("품목을 선택해 주세요.");
                return;
            }

            bool isSuccess = false;


            if (_isCustom)
            {
                JObject jobj = new JObject();
                jobj.Add("RECEIPT_ID", _receiptId);
                jobj.Add("RECEIPT", _receipt);
                jobj.Add("COMPONENT_CD", _componentCd);
                jobj.Add("RECEIPT_COMPONENT_CD", ProjectInfo._dicLTComponentCd[_componentCd]);
                jobj.Add("MODEL_NM", temodelNm.Text);
                jobj.Add("PART_PRICE", ConvertUtil.ToInt64(sePrice.EditValue));
                jobj.Add("PART_CNT", ConvertUtil.ToInt64(seCnt.EditValue));
                jobj.Add("PARTCODE", "-1");
  
                if (_sourceCd == 0)
                    isSuccess = DBUsedPurchase.insertUsedPartComponent(jobj, "insertDanawaUsedPartComponent", ref jResult);
                else
                    isSuccess = DBUsedPurchase.insertUsedPartComponent(jobj, "insertUsedPartComponent", ref jResult);

                if (isSuccess)
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else
            {
                if (_currentRow == null)
                {
                    Dangol.Message("부품을 선택해주세요.");
                    return;
                }

                string partCode = ConvertUtil.ToString(_currentRow["PARTCODE"]);

                if (_listUsedPart.Contains(partCode))
                {
                    Dangol.Message("이미 존재하는 부품입니다.");
                    return;
                }

                JObject jobj = new JObject();
                jobj.Add("RECEIPT_ID", _receiptId);
                jobj.Add("RECEIPT", _receipt);
                jobj.Add("COMPONENT_CD", _componentCd);
                jobj.Add("RECEIPT_COMPONENT_CD", ProjectInfo._dicLTComponentCd[_componentCd]);
                jobj.Add("MODEL_NM", ConvertUtil.ToString(_currentRow["MODEL_NM"]));
                jobj.Add("PART_PRICE", ConvertUtil.ToInt64(_currentRow["PRICE"]));
                jobj.Add("PART_CNT", ConvertUtil.ToInt64(seCnt.EditValue));
                jobj.Add("PARTCODE", ConvertUtil.ToString(_currentRow["PARTCODE"]));

                if (_sourceCd == 0)
                    isSuccess = DBUsedPurchase.insertUsedPartComponent(jobj, "insertDanawaUsedPartComponent", ref jResult);
                else
                    isSuccess = DBUsedPurchase.insertUsedPartComponent(jobj, "insertUsedPartComponent", ref jResult);

                if (isSuccess)
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }


    }
}