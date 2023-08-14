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
    public partial class usrBookOrderResult : DevExpress.XtraEditors.XtraForm
    {

        public usrBookOrderResult()
        {
            InitializeComponent();   
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrBookOrderResultSearch1.searchHandler += new usrBookOrderResultSearch.SearchHandler(searchList);
            usrBookOrderResultSearch1.printHandler += new usrBookOrderResultSearch.PrintHandler(print);
            usrBookOrderResultSearch1.filterHandler += new usrBookOrderResultSearch.FilterHandler(filter);

            setInitialize();
        }

        private void setInitialize()
        {
            usrBookOrderResultSearch1.setInitLoad();
            usrBookOrderResultList1.setInitLoad();
        }

        private void searchList(JObject jobj)
        {
            usrBookOrderResultList1.getList(jobj);
        }

        private void print()
        {
            usrBookOrderResultList1.print();
        }

        private void filter(bool isCheck, int filterCnt)
        {
            usrBookOrderResultList1.filter(isCheck, filterCnt);
        }

    }
}