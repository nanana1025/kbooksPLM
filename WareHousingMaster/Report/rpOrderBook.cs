using System.Data;

namespace WareHousingMaster.Report
{
    public partial class rpOrderBook : DevExpress.XtraReports.UI.XtraReport
    {
        string _shopCd;
        string _shopNm;
        string _orderDt;
        string _makeDt;

        public rpOrderBook(string shopCd,  string shopNm, string orderDt, string makeDt)
        {
            InitializeComponent();

            _shopCd = shopCd;
            _shopNm = shopNm;
            _orderDt = orderDt;
            _makeDt = makeDt;

        }
        public void DataBinding()
        {
            xrlShopCd.Text = _shopCd;
            xrlShopNm.Text = _shopNm;
            xrlOrdDate.Text = _orderDt;
            xrlMakeDate.Text = _makeDt;
        }
    }
}
