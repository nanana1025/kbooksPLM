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
    public partial class usrBookOrder : DevExpress.XtraEditors.XtraForm
    {

        public usrBookOrder()
        {
            InitializeComponent();   
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrBookOrderSearch1.searchHandler += new usrBookOrderSearch.SearchHandler(searchList);
            usrBookOrderSearch1.cancelHandler += new usrBookOrderSearch.CancelHandler(cancel);
            usrBookOrderSearch1.insertHandler += new usrBookOrderSearch.InsertHandler(insert);
            usrBookOrderSearch1.deleteRowHandler += new usrBookOrderSearch.DeleteRowHandler(deleteRowHandler);
            



            usrBookOrderList1.getSearchInfoHandler += new usrBookOrderList.GetSearchInfoHandler(getSearchInfo);

            setInitialize();
        }

        private void setInitialize()
        {
            usrBookOrderSearch1.setInitLoad();
            usrBookOrderList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.setCondition(jobj);
            usrBookOrderList1.setTableInitialize();
            usrBookOrderList1.setTableEditable(true);
        }
        private void cancel()
        {
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.setTableInitialize();
            usrBookOrderList1.setTableEditable(false);
        }
        private void insert()
        {
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.insertOrder();
        }

        private JObject getSearchInfo()
        {
            return usrBookOrderSearch1.getSearchInfo();
        }

        private void deleteRowHandler()
        {
            //usrBookOrderList1.getList(jobj);
            usrBookOrderList1.deleteRowHandler();
        }

        

    }
}