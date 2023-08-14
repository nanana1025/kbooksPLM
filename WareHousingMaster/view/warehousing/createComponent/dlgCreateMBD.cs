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

namespace WareHousingMaster.view.warehousing.createComponent
{
    public partial class dlgCreateMBD : DevExpress.XtraEditors.XtraForm
    {
        DataTable _dtPrintPort;
        DataTable _dtPallet;

        BindingSource _bsPallet;

        JObject jPartInfo;

        string _representativeType;
        string _representativeCol;
        string _representativeNo;
        long _representativeId;
        string _representativeIdCol;
        long _type;

        long _companyId;



        public int Cnt { get; private set; }


        public dlgCreateMBD(string representativeType, string representativeCol, string representativeNo, long representativeId, string representativeIdCol, long type, DataTable dtPrintPort, long companyId =2)
        {
            InitializeComponent();

            _representativeType = representativeType;
            _representativeId = representativeId;
            _representativeNo = representativeNo;
            _representativeCol = representativeCol;
            _representativeIdCol = representativeIdCol;
            _companyId = companyId;
            _type = type;
            _dtPrintPort = dtPrintPort;

            _bsPallet = new BindingSource();


            jPartInfo = new JObject();

        }
        private void dlgCreateADP_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            setInfoBox();
            setInit();
        }

        private void setInfoBox()
        {
            Util.LookupEditHelper(lePrintPort, _dtPrintPort, "KEY", "VALUE");
            lePrintPort.EditValue = ProjectInfo._printerPort;

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
 
        }

        private void setInit()
        {
            leUseCompany.EditValue = _companyId;

            if (_type == 2) //1:자사재고, 2:생산대행, 3:출고
                leUseRange.EditValue = 1;
        }

        private void leWarehouse_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            _bsPallet.Filter = $"WAREHOUSE_ID = '{e.NewValue}' OR WAREHOUSE_ID = '-1'";
            lePallet.ItemIndex = 0;
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            createInventory();


            JObject jResult = new JObject();

            jPartInfo.Add("REPRESENTATIVE_TYPE", _representativeType);     
            jPartInfo.Add(_representativeCol, _representativeNo);
            jPartInfo.Add(_representativeIdCol, _representativeId);
            jPartInfo.Add("COMPONENT_CD", "MBD");
            jPartInfo.Add("MANUFACTURE_NM", teManufactureNm.Text);
            jPartInfo.Add("MODEL_NM", teModelNm.Text);
            jPartInfo.Add("NB_NM", teModelNm.Text);
            jPartInfo.Add("MBD_MODEL_NM", teModelNm.Text);
            jPartInfo.Add("SYSTEM_VERSION", teModelNm.Text);
            jPartInfo.Add("SB_NM", teSpecNm.Text);
            jPartInfo.Add("PRODUCT_NAME", teSpecNm.Text);
            jPartInfo.Add("LOCATION", ConvertUtil.ToInt64(leWarehouse.EditValue));
            jPartInfo.Add("PALLET", ConvertUtil.ToInt64(lePallet.EditValue));
            jPartInfo.Add("USE_RANGE", ConvertUtil.ToString(leUseRange.EditValue));
            jPartInfo.Add("RELEASE_COMPANY_ID", ConvertUtil.ToString(leUseCompany.EditValue));
            jPartInfo.Add("INIT_PRICE", ConvertUtil.ToInt64(seWarehousingPrice.EditValue));
            jPartInfo.Add("RELEASE_PRICE", ConvertUtil.ToInt64(seReleasePrice.EditValue));
            jPartInfo.Add("PART_CNT", ConvertUtil.ToString(seCreateCnt.EditValue));
            jPartInfo.Add("BULK_YN", "Y");

            if (DBConnect.InsertInventory(jPartInfo, ref jResult))
            {
                Dangol.Message(jResult["MSG"]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }




            //this.DialogResult = DialogResult.OK;
        }



        private void sbSavePrint_Click(object sender, EventArgs e)
        {





            //this.DialogResult = DialogResult.OK;
        }
        private void sbClose_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }




        private void createInventory()
        {
            if(!check())
            {
                return;
            }


        }

        private bool check()
        {
            if (string.IsNullOrEmpty(teManufactureNm.Text))
            {
                Dangol.Message("제조사를 입력하세요");
                teManufactureNm.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(teModelNm.Text))
            {
                teModelNm.Focus();
                Dangol.Message("모델명을 입력하세요");
                return false;
            }
            else if (string.IsNullOrEmpty(teSpecNm.Text))
            {
                Dangol.Message("상세스펙을 입력하세요.");
                return false;
            }
            else if (leWarehouse.EditValue == null)
            {
                Dangol.Message("재고위치을 선택하세요");
                return false;
            }
            else if (lePallet.EditValue == null)
            {
                Dangol.Message("적재위치을 선택하세요");
                return false;
            }
            else if (leUseRange.EditValue == null)
            {
                Dangol.Message("사용위치을 선택하세요");
                return false;
            }
            else if (leUseCompany.EditValue == null)
            {
                Dangol.Message("사용업체을 선택하세요");
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