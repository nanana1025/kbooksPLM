using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.search.common
{
    public partial class dlgPublisherList : DevExpress.XtraEditors.XtraForm
    {
        string _title;

        int _shopCd;
        public string _publisher { get; private set; }


        public DataRowView _drv { get; private set; }

        public int Cnt { get; private set; }

        public dlgPublisherList(string publisher, int shopCd = 1)
        {
            InitializeComponent();
            _publisher = publisher;
            _shopCd = shopCd;

        }
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrPublishList1.focusedRowObjectChangeHandler += new usrPublishList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);
            usrPublishList1.selectHandler += new usrPublishList.SelectHandler(select);
            usrPublishList1.setInitLoad();


            JObject jobj = new JObject();
            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("PUBSHNM", _publisher);
            usrPublishList1.getList(jobj);
        }


        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if(_drv == null)
            {
                Dangol.Warining("선택한 출판사가 없습니다.");
            }
            else
            {
                if(Dangol.MessageYN("선택한 출판사를 적용하시겠습니까?") == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void select()
        {
            if (_drv == null)
            {
                Dangol.Warining("선택한 출판사가 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN("선택한 출판사를 적용하시겠습니까?") == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}