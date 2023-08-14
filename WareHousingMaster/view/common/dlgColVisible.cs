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
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;

namespace WareHousingMaster.view.common
{
    public partial class dlgColVisible : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtList;
        BindingSource _bs;

        DataRowView _currentRow;
        string _repType;
        int _repValue;

        int _viewCategory;
        int _viewType;

        bool _isChanged = false;

        public List<string> _listVisibleCol { get; private set; }
        public List<string> _listHideCol { get; private set; }

        List<string> _listReadOnlyCol;

        public dlgColVisible(int viewCategory, int viewType, List<string> listReadOnlyCol, List<string> listHideCol)
        {
            _viewCategory = viewCategory;
            _viewType = viewType;
            _listReadOnlyCol = listReadOnlyCol;

            InitializeComponent();

            _dtList = new DataTable();
            _dtList.Columns.Add(new DataColumn("VISIBLE_COL_ID", typeof(long)));
            _dtList.Columns.Add(new DataColumn("COL_NAME", typeof(string)));
            _dtList.Columns.Add(new DataColumn("FIELD_NAME", typeof(string)));
            _dtList.Columns.Add(new DataColumn("VISIBLE_YN", typeof(int)));

            _bs = new BindingSource();

            _repType = "입고번호";
            _repValue = 1;

            _listVisibleCol = new List<string>();
            _listHideCol = new List<string>();

            foreach (string col in listHideCol)
                _listHideCol.Add(col);

        }
        private void dlgUpdateWarehouseMovementList_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;


            DataTable dtVisibleYn = new DataTable();

            dtVisibleYn.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtVisibleYn.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtVisibleYn, 1, "Y");
            Util.insertRowonTop(dtVisibleYn, 0, "N");

            Util.LookupEditHelper(rileVisibleYn, dtVisibleYn, "KEY", "VALUE");

            gcList.DataSource = null;
            _bs.DataSource = _dtList;
            gcList.DataSource = _bs;

            getRepNo();
        }
        
        private void sbSave_Click(object sender, EventArgs e)
        {
            if (_isChanged)
            {
                string filedName;
                string colName;
                int visible;
                foreach (DataRow dr in _dtList.Rows)
                {
                    filedName = ConvertUtil.ToString(dr["FIELD_NAME"]);
                    visible = ConvertUtil.ToInt32(dr["VISIBLE_YN"]);

                    if (visible == 1)
                        _listVisibleCol.Add(filedName);
                    else
                        _listHideCol.Add(filedName);
                }

                this.DialogResult = DialogResult.OK;
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }


        private void getRepNo()
        {
            JObject jData = new JObject();
            JObject jResult = new JObject();

            jData.Add("VIEW_CATEGORY", _viewCategory);
            jData.Add("VIEW_TYPE", _viewType);

            if (DBConnect.getVisibleCol(jData, ref jResult))
            {
                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                string filedName;
                string colName;
                int visible;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    colName = ConvertUtil.ToString(obj["COL_NAME"]);
                    filedName = ConvertUtil.ToString(obj["FIELD_NAME"]);
                    visible = ConvertUtil.ToInt32(obj["VISIBLE_YN"]);

                    if (!_listHideCol.Contains(filedName))
                    {
                        DataRow dr = _dtList.NewRow();

                        dr["VISIBLE_COL_ID"] = obj["VISIBLE_COL_ID"];
                        dr["COL_NAME"] = colName;
                        dr["FIELD_NAME"] = filedName;
                        dr["VISIBLE_YN"] = visible;
                        _dtList.Rows.Add(dr);
                    }
                }
            }
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvList.RowCount > 0);

            if (isValidRow)
            {
                _currentRow = gvList.GetRow(e.FocusedRowHandle) as DataRowView;
                string fieldName = ConvertUtil.ToString(_currentRow["FIELD_NAME"]);

                if (_listReadOnlyCol.Contains(fieldName))
                    gcVisibleYn.OptionsColumn.ReadOnly = true;
                else
                    gcVisibleYn.OptionsColumn.ReadOnly = false;
            }
            else
            {
                _currentRow = null;
                gcVisibleYn.OptionsColumn.ReadOnly = true;
            }
        }

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_currentRow != null)
            {
                JObject jData = new JObject();
                JObject jResult = new JObject();

                jData.Add("VISIBLE_COL_ID", ConvertUtil.ToInt64(_currentRow["VISIBLE_COL_ID"]));
                jData.Add("VISIBLE_YN", ConvertUtil.ToInt32(_currentRow["VISIBLE_YN"]));

                if (DBConnect.updateVisibleCol(jData, ref jResult))
                    _isChanged = true;
            }
            else
            {
                Dangol.Message("오류가 발생했습니다. 화면 종료 후 다시 처리해 주세요");
            }
        }

        private void rileVisibleYn_Popup(object sender, EventArgs e)
        {
            PopupLookUpEditForm form = (sender as IPopupControl).PopupWindow as PopupLookUpEditForm;
            int width = Common.GetWidth(sender as LookUpEdit);
            if (form != null && form.Width > width)
            {
                form.Width = width;
            }
        }
    }
}