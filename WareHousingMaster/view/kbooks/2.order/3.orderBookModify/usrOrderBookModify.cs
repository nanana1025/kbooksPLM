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
    public partial class usrOrderBookModify : DevExpress.XtraEditors.XtraForm
    {

        public usrOrderBookModify()
        {
            InitializeComponent();

           
                
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrOrderBookModifySearch1.searchHandler += new usrOrderBookModifySearch.SearchHandler(searchList);
            usrOrderBookModifySearch1.searchPerformanceHandler += new usrOrderBookModifySearch.SearchPerformanceHandler(searchPerformanceHandler);
            usrOrderBookModifySearch1.deleteRowHandler += new usrOrderBookModifySearch.DeleteRowHandler(deleteRowHandler);
            usrOrderBookModifySearch1.confirmHandler += new usrOrderBookModifySearch.ConfirmHandler(confirmHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrOrderBookModifySearch1.setInitLoad();
            usrOrderBookModifyList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrOrderBookModifyList1.setCondition(jobj);
            usrOrderBookModifyList1.getList(jobj);
        }

        private void searchPerformanceHandler()
        {
            usrOrderBookModifyList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            usrOrderBookModifyList1.deleteRow();
        }

        private void confirmHandler()
        {
            usrOrderBookModifyList1.confirm();
        }

    }
}