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
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrUnregisteredBookOrder : DevExpress.XtraEditors.XtraForm
    {

        public usrUnregisteredBookOrder()
        {
            InitializeComponent();
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrUnregisteredBookOrderSearch1.searchHandler += new usrUnregisteredBookOrderSearch.SearchHandler(searchList);
            usrUnregisteredBookOrderSearch1.deleteHandler += new usrUnregisteredBookOrderSearch.DeleteHandler(delete);
            usrUnregisteredBookOrderSearch1.confirmHandler += new usrUnregisteredBookOrderSearch.ConfirmHandler(confirm);

            setInitialize();
        }

        private void setInitialize()
        {
            usrUnregisteredBookOrderSearch1.setInitLoad();
            usrUnregisteredBookOrderList1.setInitLoad();
        }

        private void searchList(JObject jobj)
        {
            usrUnregisteredBookOrderList1.setTableInitialize();
            usrUnregisteredBookOrderList1.getList(jobj);
        }
        private void delete()
        {
            usrUnregisteredBookOrderList1.deleteRowHandler();
        }
        private void confirm()
        {
            usrUnregisteredBookOrderList1.insertHandler();
        }

    }
}