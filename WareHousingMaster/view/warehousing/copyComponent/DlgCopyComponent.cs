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

namespace WareHousingMaster.view.warehousing.copyComponent
{
    public partial class DlgCopyComponent : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtPallet;
        BindingSource _bsPallet;

        public JObject jPartInfo { get; private set; }
        Dictionary<string, string> _dicSelectData;

        long _warehousingId;
        int _warehousingType;
        string _componentCd;
        long _companyId;
        long _releaseCompanyId;
        long _componentId;
        int _type;
        long _price;

        public DlgCopyComponent(long warehousingId, int warehousingType, int type, long componentId, string componentCd, long price, string modelNm, long companyId, long releaseCompanyId)
        {
            InitializeComponent();

            _warehousingId = warehousingId;
            _warehousingType = warehousingType;

            _companyId = companyId;
            _releaseCompanyId = releaseCompanyId;
            _componentId = componentId;
            _componentCd = componentCd;
            _type = type;
            _price = price;

            _bsPallet = new BindingSource();

            jPartInfo = new JObject();
            _dicSelectData = new Dictionary<string, string>();

            teModelNm.Text = modelNm;
        }
        private void dlgCopyComponent_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            setInfoBox();
            setInit();
        }

        private void setInfoBox()
        {

            DataTable dtLocation = Util.getCodeListCustom("TN_WAREHOUSE", "WAREHOUSE_ID", "PLACE", "USE_YN != 'N'", "WAREHOUSE_ID ASC");
            Util.insertRowonTop(dtLocation, "-1", " 없음");
            Util.LookupEditHelper(leWarehouse, dtLocation, "KEY", "VALUE");

            _dtPallet = Util.getTable("TN_PALLET", "WAREHOUSE_ID, PALLET_ID, PALLET_NM", "USE_YN != 'N'", "PALLET_NM ASC");
            Dictionary<string, object> dicPalletDefault = new Dictionary<string, object>();
            dicPalletDefault.Add("WAREHOUSE_ID", "-1");
            dicPalletDefault.Add("PALLET_ID", "");
            dicPalletDefault.Add("PALLET_NM", "선택안합");
            Util.insertRowonTop(_dtPallet, dicPalletDefault);
            _bsPallet.DataSource = _dtPallet;
            Util.LookupEditHelper(lePallet, _bsPallet, "PALLET_ID", "PALLET_NM");

            DataTable dtUseRange = Util.getCodeList("CD0105", "KEY", "VALUE");
            Util.LookupEditHelper(leUseRange, dtUseRange, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", " 없음");
            Util.LookupEditHelper(leUseCompany, dtCompany, "KEY", "VALUE");

            DataTable dtComponentCd = new DataTable();

            dtComponentCd.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtComponentCd.Columns.Add(new DataColumn("VALUE", typeof(string)));

            for (int i = 0; i < ProjectInfo._componetCd.Length; i++)
            {
                DataRow dr1 = dtComponentCd.NewRow();
                dr1["KEY"] = ProjectInfo._componetCd[i];
                dr1["VALUE"] = ProjectInfo._componetNm[i];
                dtComponentCd.Rows.Add(dr1);
            }

            Util.LookupEditHelper(leComponentCd, dtComponentCd, "KEY", "VALUE");
            
        }

        private void setInit()
        {
            leUseCompany.EditValue = _releaseCompanyId.ToString();
            leComponentCd.EditValue = _componentCd;

            if (_type == 1) //1:생산대행, 2:일반,
                leUseRange.EditValue = "1";
            else
                leUseRange.EditValue = "2";

            seReleasePrice.EditValue = _price;
        }

        private void leWarehouse_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            _bsPallet.Filter = $"WAREHOUSE_ID = '{e.NewValue}' OR WAREHOUSE_ID = '-1'";
            lePallet.ItemIndex = 0;
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                return;
            }

            JObject jResult = new JObject();

            jPartInfo.RemoveAll();

            jPartInfo.Add("WAREHOUSING_ID", _warehousingId);     
            jPartInfo.Add("WAREHOUSING_TYPE", _warehousingType);
            jPartInfo.Add("COMPANY_ID", _companyId);
            jPartInfo.Add("COMPONENT_CD", _componentCd);

            jPartInfo.Add("COMPONENT_ID", _componentId);           
            jPartInfo.Add("LOCATION", ConvertUtil.ToInt64(leWarehouse.EditValue));
            jPartInfo.Add("PALLET", ConvertUtil.ToInt64(lePallet.EditValue));
            jPartInfo.Add("USE_RANGE", ConvertUtil.ToString(leUseRange.EditValue));
            jPartInfo.Add("RELEASE_COMPANY_ID", ConvertUtil.ToString(leUseCompany.EditValue));
            jPartInfo.Add("PRICE", ConvertUtil.ToInt64(seWarehousingPrice.EditValue));
            jPartInfo.Add("RELEASE_PRICE", ConvertUtil.ToInt64(seReleasePrice.EditValue));
            jPartInfo.Add("PART_CNT", ConvertUtil.ToString(seCreateCnt.EditValue));

            Dangol.ShowSplash();

            if (DBConnect.copyInventory(jPartInfo, ref jResult))
            {
                jPartInfo.Add("INVENTORY_ID", jResult["INVENTORY_ID"]);
                jPartInfo.Add("BARCODE", jResult["BARCODE"]);
                jPartInfo.Add("COMPONENT", jResult["COMPONENT"]);

                Dangol.CloseSplash();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Dangol.CloseSplash();
                Dangol.Message(jResult["MSG"]);
                return;
            }
        }

        private void sbClose_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool check()
        {
            if (_componentId == -1)
            {
                Dangol.Message("부품이 선택되지 않았습니다.");
                return false;
            }
            else if (leWarehouse.EditValue == null)
            {
                Dangol.Message("재고위치을 선택하세요.");
                return false;
            }
            else if (lePallet.EditValue == null)
            {
                Dangol.Message("적재위치을 선택하세요.");
                return false;
            }
            else if (leUseRange.EditValue == null)
            {
                Dangol.Message("사용위치을 선택하세요.");
                return false;
            }
            else if (leUseCompany.EditValue == null)
            {
                Dangol.Message("사용업체을 선택하세요.");
                return false;
            }
            else if (ConvertUtil.ToInt32(seCreateCnt.EditValue) < 1)
            {
                seCreateCnt.Focus();
                Dangol.Message("생성개수는 0보다 커야합니다.");
                return false;
            }

            return true;


        }




        

    }
}