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
    public partial class usrOrderBookReport : DevExpress.XtraEditors.XtraForm
    {

        public usrOrderBookReport()
        {
            InitializeComponent();

           
                
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrOrderBookReportSearch1.searchHandler += new usrOrderBookReportSearch.SearchHandler(searchList);
            //usrOrderBookReportSearch1.searchPerformanceHandler += new usrOrderBookReportSearch.SearchPerformanceHandler(searchPerformanceHandler);
            //usrOrderBookReportSearch1.deleteRowHandler += new usrOrderBookReportSearch.DeleteRowHandler(deleteRowHandler);
            usrOrderBookReportSearch1.confirmHandler += new usrOrderBookReportSearch.ConfirmHandler(confirmHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrOrderBookReportSearch1.setInitLoad();
            usrOrderBookReportList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrOrderBookReportList1.setCondition(jobj);
            usrOrderBookReportList1.getList(jobj);
        }

        //private void searchPerformanceHandler()
        //{
        //    usrOrderBookReportList1.getPerformance();
        //}

        //private void deleteRowHandler()
        //{
        //    usrOrderBookReportList1.deleteRow();
        //}

        private void confirmHandler()
        {
            usrOrderBookReportList1.confirm();
        }

    }
}