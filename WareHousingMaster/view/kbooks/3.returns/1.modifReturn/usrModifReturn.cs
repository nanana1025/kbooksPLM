using Newtonsoft.Json.Linq;
using System;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.returns
{
    public partial class usrModifReturn : DevExpress.XtraEditors.XtraForm
    {

        public usrModifReturn()
        {
            InitializeComponent();

        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrModifReturnSearch1.searchHandler += new usrModifReturnSearch.SearchHandler(searchList);
            usrModifReturnSearch1.confirmHandler += new usrModifReturnSearch.ConfirmHandler(confirmHandler);
            usrModifReturnSearch1.searchPerformanceHandler += new usrModifReturnSearch.SearchPerformanceHandler(searchPerformanceHandler);
            usrModifReturnSearch1.deleteRowHandler += new usrModifReturnSearch.DeleteRowHandler(deleteRowHandler);

            //usrModifReturnList1.getSearchInfoHandler += new usrModifReturnList.GetSearchInfoHandler(getSearchInfo);

            setInitialize();
        }

        private void setInitialize()
        {
            usrModifReturnSearch1.setInitLoad();
            usrModifReturnList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrModifReturnList1.setCondition(jobj);
            //usrModifReturnList1.setTableInitialize();
            //usrModifReturnList1.setTableEditable(true);

            usrModifReturnList1.getList();

        }

        private void searchPerformanceHandler()
        {
            usrModifReturnList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            usrModifReturnList1.deleteRowHandler();
        }

        private void confirmHandler()
        {
            usrModifReturnList1.insertReturn();
        }

        //private JObject getSearchInfo()
        //{
        //    usrModifReturnList1.getList();
        //}

    }
}