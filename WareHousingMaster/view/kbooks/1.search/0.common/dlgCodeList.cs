using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.search.common
{
    public partial class dlgCodeList : DevExpress.XtraEditors.XtraForm
    {
        int _type;
        string _codeNm;
        int _code;

        public int _standCd { get; set; }
        public string _purchaseCompany { get; private set; }


        public DataRowView _drv { get; private set; }

        public int Cnt { get; private set; }

        public dlgCodeList(string title, int type, string codeNm, int code = -1) //1:
        {
            InitializeComponent();
            _type = type;
            _codeNm = codeNm.ToLower();
            _code = code;

            this.Text = title;

            usrCodeList1.setInitLoad();
        }
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrCodeList1.focusedRowObjectChangeHandler += new usrCodeList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);
            usrCodeList1.selectHandler += new usrCodeList.SelectHandler(select);
            usrCodeList1.doubleClickHandler += new usrCodeList.DoubleClickHandler(doubleClickHandler);


            JObject jobj = new JObject();
            jobj.Add("SHOPCD", 1);

            if (_type == (int)view.common.Enum.CODE_TYPE.STAND)
            {
                jobj.Add("CODE_NM", "STANDCD");

                if (_code == -1)
                {
                    jobj.Add("ORGAN_NM", _codeNm);
                    jobj.Add("NSTANDCD", 9999);
                }
                else
                {
                    jobj.Add("STANDCD", _code);
                }

                usrCodeList1.getList(jobj, 1);
            }
            else if(_type == (int)view.common.Enum.CODE_TYPE.PRODUCT)
            {
                if (_code == -1)
                {
                    jobj.Add("ORGAN_NM", _codeNm);
                    jobj.Add("STANDCD", _standCd);
                }
                else
                {
                    jobj.Add("STANDCD", _standCd);
                    jobj.Add("PRODUCT_CD", _code);
                }

                usrCodeList1.getList(jobj, 2);
            }

           
        }


        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;
        }

        private void doubleClickHandler()
        {
            select();
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            //_drv = usrCodeList1.getDataRowView();
            select();
        }

        private void select()
        {
            if (_drv == null)
            {
                Dangol.Warining("선택한 코드가 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN("선택한 코드를 적용하시겠습니까?") == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dlgPurchaseList_Shown(object sender, EventArgs e)
        {
            //usrPurchaseList1.SetFocus();
        }

        private void dlgPurchaseList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10 || e.KeyCode == Keys.F9)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}