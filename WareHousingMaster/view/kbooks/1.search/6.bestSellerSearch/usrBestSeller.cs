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
using Enum = WareHousingMaster.view.common.Enum;

namespace WareHousingMaster.view.usedPurchase
{
    public partial class usrBestSeller : DevExpress.XtraEditors.XtraForm
    {

        public usrBestSeller()
        {
            InitializeComponent();    
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrBestSellerSearch1.searchHandler += new usrBestSellerSearch.SearchHandler(searchList);

            setInitialize();
        }

        private void setInitialize()
        {
            usrBestSellerSearch1.setInitLoad();

            usrBestSellerList1.setInitLoad();

        }

        private void searchList(JObject jobj)
        {
            usrBestSellerList1.getList(jobj);
        }

        private void lcgBestSellerList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                usrBestSellerList1.showBookInfoDetail();
            }
        }
    }
}