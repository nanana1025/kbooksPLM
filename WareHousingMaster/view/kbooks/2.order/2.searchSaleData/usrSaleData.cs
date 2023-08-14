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
    public partial class usrSaleData : DevExpress.XtraEditors.XtraForm
    {

        public usrSaleData()
        {
            InitializeComponent();

           
                
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSaleDataSearch1.searchHandler += new usrSaleDataSearch.SearchHandler(searchList);
            usrSaleDataSearch1.saveHandler += new usrSaleDataSearch.SaveHandler(save);
            

            setInitialize();
        }

        private void setInitialize()
        {
            usrSaleDataSearch1.setInitLoad();
            usrSaleDataList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrSaleDataList1.getList(jobj);
        }

        private void save()
        {
            usrSaleDataList1.save();
        }
        

    }
}