using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.search.common
{
    public partial class dlgBookOutCome : DevExpress.XtraEditors.XtraForm
    {
        long _bookCd;
        int _shopCd;
        int _purchCd;
        public string _purchaseCompany { get; private set; }


        public DataRowView _drv { get; private set; }

        public int Cnt { get; private set; }

        public dlgBookOutCome(int shopCd, long bookCd, int purchCd)
        {
            InitializeComponent();
            _bookCd = bookCd;
            _shopCd = shopCd;
            _purchCd = purchCd;
        }
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            //usrSearchPublisherOutcome1.focusedRowObjectChangeHandler += new usrPurchaseList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler); 
            //usrPurchaseList1.selectHandler += new usrPurchaseList.SelectHandler(select);
            usrSearchPublisherOutcome1.setInitLoad();


            JObject jobj = new JObject();
            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("BOOKCD", _bookCd);
            if(_purchCd > 0)
                jobj.Add("PURCHCD", _purchCd);

            if(!ProjectInfo._userType.Equals("M"))
            {
                var today = DateTime.Today;
                var pastDate = today.AddDays(-15);

                jobj.Add("DT_FROM", pastDate.ToString("yyyy-MM-dd"));
                jobj.Add("DT_TO", today.ToString("yyyy-MM-dd"));

            }

            usrSearchPublisherOutcome1.getList(jobj);
        }


        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void select()
        {
            //if (_drv == null)
            //{
            //    Dangol.Warining("선택한 매입처가 없습니다.");
            //}
            //else
            //{
            //    if (Dangol.MessageYN("선택한 매입처를 적용하시겠습니까?") == DialogResult.Yes)
            //        this.DialogResult = DialogResult.OK;
            //}
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dlgBookOutCome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10 || e.KeyCode == Keys.F9)
            {
                e.Handled = true;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}