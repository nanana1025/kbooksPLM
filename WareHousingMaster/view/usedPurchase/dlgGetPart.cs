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
using DevExpress.XtraGrid.Columns;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class dlgGetPart : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dt;
        BindingSource _bs;
        string _receipt;
        string _componentCd;

        DataRowView _currentRow = null;
        List<string> _listUsedPart;

        string _filterString;
        public long _price { get; private set; }

        public int Cnt { get; private set; }

        public dlgGetPart(string componentCd, string filterString)
        {
            InitializeComponent();
            _componentCd = componentCd;
            _filterString = filterString;
            _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("PARTCODE", typeof(string)));
            _dt.Columns.Add(new DataColumn("MODEL_NM", typeof(string)));
            _dt.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            _dt.Columns.Add(new DataColumn("PRICE", typeof(long)));


            _bs = new BindingSource();

            _bs.DataSource = _dt;

        }
        private void dlgGetPart_Load(object sender, EventArgs e)
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

            Util.LookupEditHelper(leComponentCd, dtLTComponent, "KEY", "VALUE");
            leComponentCd.ItemIndex = 0;

            setTable();
        }

        private void setTable()
        {
            JObject jResult = new JObject();

            if (_componentCd.Equals("MON"))
            {
                _dt.Clear();

                if (DBUsedPurchase.getLTComonent("LED", ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string partCode = ConvertUtil.ToString(obj["PARTCODE"]);

                        //if (!_listUsedPart.Contains(partCode))
                        {
                            DataRow dr = _dt.NewRow();
                            dr["PARTCODE"] = partCode;
                            dr["MODEL_NM"] = obj["MODEL_NM"];
                            dr["CATEGORY"] = obj["CATEGORY"];
                            dr["PRICE"] = obj["PRICE"];
                            _dt.Rows.Add(dr);
                        }
                    }
                }

                if (DBUsedPurchase.getLTComonent("LCD", ref jResult))
                {
                    JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                    foreach (JObject obj in jArray.Children<JObject>())
                    {
                        string partCode = ConvertUtil.ToString(obj["PARTCODE"]);

                        //if (!_listUsedPart.Contains(partCode))
                        {
                            DataRow dr = _dt.NewRow();
                            dr["PARTCODE"] = partCode;
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
                if (DBUsedPurchase.getLTComonent(_componentCd, ref jResult))
                {
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
            if (!_filterString.Equals("-1"))
            {
                string filterString = $"Contains([MODEL_NM], '{_filterString}')";
                gvComponent.Columns["MODEL_NM"].FilterInfo = new ColumnFilterInfo(filterString);
            }
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            //string componentCd = ConvertUtil.ToString(leComponentCd.EditValue);
            //if (componentCd.Equals("-1"))
            //{
            //    Dangol.Message("품목을 선택하세요");
            //    return;
            //}

            //JObject jResult = new JObject();

            //if (DBUsedPurchase.getLTComonent(componentCd, ref jResult))
            //{
            //    _dt.Clear();

            //    if (Convert.ToBoolean(jResult["EXIST"]))
            //    {
            //        JArray jArray = JArray.Parse(jResult["DATA"].ToString());
            //        foreach (JObject obj in jArray.Children<JObject>())
            //        {
            //            string partCode = ConvertUtil.ToString(obj["PARTCODE"]);

            //            //if (!_listUsedPart.Contains(partCode))
            //            {
            //                DataRow dr = _dt.NewRow();
            //                dr["PARTCODE"] = partCode;
            //                dr["MODEL_NM"] = obj["MODEL_NM"];
            //                dr["CATEGORY"] = obj["CATEGORY"];
            //                dr["PRICE"] = obj["PRICE"];
            //                _dt.Rows.Add(dr);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //}
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

        private void sbInsert_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();

            if (_currentRow == null)
            {
                Dangol.Message("부품을 선택해주세요.");
                return;
            }

            _price = ConvertUtil.ToInt64(_currentRow["PRICE"]);
            this.DialogResult = DialogResult.OK;

            //if (_listUsedPart.Contains(partCode))
            //{
            //    Dangol.Message("이미 존재하는 부품입니다.");
            //    return;
            //}

            //if (DBUsedPurchase.insertUsedPartComponent(_receipt, _currentRow["PARTCODE"], ref jResult))
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //    this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //} 
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}