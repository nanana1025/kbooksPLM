using Newtonsoft.Json.Linq;
using System;
using System.Data;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrNewBooks : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _drv;

        public usrNewBooks()
        {
            InitializeComponent();    
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrNewBooksSearch1.searchHandler += new usrNewBooksSearch.SearchHandler(searchList);
            usrNewBooksList1.focusedRowObjectChangeHandler += new usrNewBooksList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);
            

            setInitialize();
        }

        private void setInitialize()
        {
            usrNewBooksSearch1.setInitLoad();

            usrNewBooksList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrNewBooksList1.getList(jobj);
        }

        private void lcgNewBookList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                usrNewBooksList1.showBookInfoDetail();
            }
        }

        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;

        }
    }
}