using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.price.partPriceMapping
{
    public partial class DlgCreateLTPart : DevExpress.XtraEditors.XtraForm
    {
        string _partCode;
        string _componentCd;
        string _partCategory;
        string _partName;

        string _partCodeWM;
        string _partNameWM;
        long _money;
        long _type;

        public int _insertType { get; private set; }


        public DlgCreateLTPart(string partCode, string partName, string componentCd, string partCategory, long money, long type, string partCodeWM, string partNameWM)
        {
            InitializeComponent();

            _partCode = partCode;
            _componentCd = componentCd;
            _partCategory = partCategory;
            _partName = partName;
            _type = type;
            _money = money;

            _partCodeWM = partCodeWM;
            _partNameWM = partNameWM;

            if (_type == 2)
            {
                lcPartMappingAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            _partCode = "자동생성";

        }
        private void dlgCreateADP_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            setInfoBox();

            tePartCode.Text = _partCode;
            lecategory.EditValue = _componentCd;
            teCategory.Text = _partCategory;
            tePartName.Text = _partName;
            seMoney.EditValue = _money;
            rgDanawaFlag.EditValue = "N";
        }

        private void setInfoBox()
        {
            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ConCSInfo._LTComponentCd.Length; i++)
            {
                DataRow dr = dtComponentCd.NewRow();

                dr["KEY"] = ConCSInfo._LTComponentCd[i];
                dr["VALUE"] = ConCSInfo._LTComponentCd[i];
                dtComponentCd.Rows.Add(dr);
            }

            Util.LookupEditHelper(lecategory, dtComponentCd, "KEY", "VALUE");
        }

        private void sbClose_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void sbSave_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                return;
            }


            if (Dangol.MessageYN($"LT부품을 추가하시겠습니까?") == DialogResult.Yes)
            {

                JObject jResult = new JObject();
                JObject jData = new JObject();

                jData.Add("PARTNAME", tePartName.Text);
                jData.Add("PARTCAT1", ConvertUtil.ToString(lecategory.EditValue));
                jData.Add("PARTCAT2", teCategory.Text);
                jData.Add("MONEY", ConvertUtil.ToInt64(seMoney.EditValue));
                jData.Add("DANAWAFLAG", ConvertUtil.ToString(rgDanawaFlag.EditValue));

                jData.Add("PARTCODEWM", _partCodeWM);
                jData.Add("PARTNAMEWM", _partNameWM);

                jData.Add("MAPPING_YN", 1);

                if (DBPrice.insertLTPart(jData, ref jResult))
                {
                    _insertType = 1;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    this.DialogResult = DialogResult.Cancel;
                }

            }
        }

        private void sbPartAdd_Click(object sender, EventArgs e)
        {

            if (!check())
            {
                return;
            }


            if (Dangol.MessageYN($"LT부품을 추가하시겠습니까?") == DialogResult.Yes)
            {

                JObject jResult = new JObject();
                JObject jData = new JObject();

                jData.Add("PARTNAME", tePartName.Text);
                jData.Add("PARTCAT1", ConvertUtil.ToString(lecategory.EditValue));
                jData.Add("PARTCAT2", teCategory.Text);
                jData.Add("MONEY", ConvertUtil.ToInt64(seMoney.EditValue));
                jData.Add("DANAWAFLAG", ConvertUtil.ToString(rgDanawaFlag.EditValue));

                //jData.Add("PARTCODEWM", _partCodeWM);
                //jData.Add("PARTNAMEWM", _partNameWM);

                jData.Add("MAPPING_YN", 0);

                if (DBPrice.insertLTPart(jData, ref jResult))
                {
                    _insertType = 2;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    this.DialogResult = DialogResult.Cancel;
                }

            }
        }

        private bool check()
        {
            if (lecategory.EditValue == null)
            {
                Dangol.Message("품목명을 입력하세요.");
                return false;
            }
            else if (string.IsNullOrEmpty(teCategory.Text))
            {
                Dangol.Message("상세품목명을 입력하세요");
                teCategory.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(tePartName.Text))
            {
                tePartName.Focus();
                Dangol.Message("모델명을 입력하세요");
                return false;
            }

            return true;


        }






    }
}