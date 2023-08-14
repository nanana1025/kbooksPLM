using Newtonsoft.Json.Linq;
using System;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.returns
{
    public partial class usrInputReturn : DevExpress.XtraEditors.XtraForm
    {

        public usrInputReturn()
        {
            InitializeComponent();

        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrInputReturnSearch1.searchHandler += new usrInputReturnSearch.SearchHandler(searchList);
            usrInputReturnSearch1.confirmHandler += new usrInputReturnSearch.ConfirmHandler(confirmHandler);
            usrInputReturnSearch1.searchPerformanceHandler += new usrInputReturnSearch.SearchPerformanceHandler(searchPerformanceHandler);
            usrInputReturnSearch1.deleteRowHandler += new usrInputReturnSearch.DeleteRowHandler(deleteRowHandler);

            //usrInputReturnList1.getSearchInfoHandler += new usrInputReturnList.GetSearchInfoHandler(getSearchInfo);

            setInitialize();
        }

        private void setInitialize()
        {
            usrInputReturnSearch1.setInitLoad();
            usrInputReturnList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrInputReturnList1.setCondition(jobj);
            usrInputReturnList1.setTableInitialize();
            //usrInputReturnList1.setTableEditable(true);
        }

        private void searchPerformanceHandler()
        {
            usrInputReturnList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            usrInputReturnList1.deleteRowHandler();
        }

        private void confirmHandler()
        {
            usrInputReturnList1.insertReturn();
        }

        //private JObject getSearchInfo()
        //{
        //    return usrInputReturnSearch1.getSearchInfo();
        //}

    }
}