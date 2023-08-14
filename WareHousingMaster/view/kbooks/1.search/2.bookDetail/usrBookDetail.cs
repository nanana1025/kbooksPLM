using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.bookDetail
{
    public partial class usrBookDetail : DevExpress.XtraEditors.XtraForm
    {
        long _bookCd;
        int _shopCd;
        bool _isEditable;

        public usrBookDetail(long bookCd, int shopCd = 1, bool isEditable = false)
        {
            InitializeComponent();

            _bookCd = bookCd;
            _shopCd = shopCd;
            _isEditable = isEditable;
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrBookInfoDetail1.searchHandler += new usrBookInfoDetail.SearchHandler(searchList);

            setInitialize(_isEditable);
            getBookInfo();
        }

        private void setInitialize(bool isEditable)
        {
            usrBookInfoDetail1.setInitLoad(isEditable);
        }

        private void searchList(JObject jobj)
        {
            _bookCd = ConvertUtil.ToInt64(jobj["BOOKCD"]);
            getBookInfo();
        }

        private void getBookInfo()
        {
            if (_bookCd > 0)
                usrBookInfoDetail1.getList(_bookCd, _shopCd);
        }

        private void usrBookDetail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                e.Handled = true;
                //this.DialogResult = DialogResult.OK;
            }
        }
    }
}