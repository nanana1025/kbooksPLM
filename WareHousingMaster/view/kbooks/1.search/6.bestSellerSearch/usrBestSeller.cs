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
        DataRowView _drv;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrBestSeller()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조회", "F2\n도서상세정보", "F3", "F4", "F5", "F6", "F7\n선택주문", "F8취소", "F9\n닫기", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, false, false, false, false, true, true, true, true };

        }


        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrBestSellerSearch1.searchHandler += new usrBestSellerSearch.SearchHandler(searchList);
            usrBestSellerList1.focusedRowObjectChangeHandler += new usrBestSellerList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrBestSellerSearch1.setInitLoad();
            usrBestSellerList1.setInitLoad();

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            usrBestSellerList1.getList(jobj);
            usrBestSellerList1.setFocus();

            DataTable dt = usrBestSellerList1.getTable();
            if (dt.Rows.Count > 0)
                usrBestSellerList1.SetColFocus("BOOKNM", 0);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrBestSellerSearch1.Search();
                    break;
                case 2:
                    usrBestSellerList1.showBookInfoDetail();
                    break;
                case 3:
                    //usrSearchBookSearchList1.sortList("AUTHOR1");
                    break;
                case 4:
                    //usrSearchBookSearchList1.sortList("PUBSHNM");
                    break;
                case 5:
                    break;
                case 6:
                    //usrSearchBookSearchList1.showBookInfoDetail();
                    break;
                case 7:
                    goOrderBook();
                    break;
                case 8:
                    usrBestSellerList1.clear();
                    usrBestSellerSearch1.clear();
                    usrBestSellerSearch1.setFocus();
                    break;
                case 9:
                    this.Close();
                    break;
                case 10:
                    this.Close();
                    //usrSearchBookSearchList1.clearGridView();
                    //Dangol.Message("1111");
                    //usrSearchBookSearch1.Focus();
                    break;
                default:
                    break;

            }
        }

        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;

        }

        private void goOrderBook()
        {
            DataRow[] rows = usrBestSellerList1.getCheckedList();

            if (rows == null)
                Dangol.Warining("주문가능한 도서가 없습니다.");
            else
            {
                string tabName = "주문 예정입력";

                if (!(ProjectInfo._tabbedView.Documents.Any(x => x.Form.Tag.ToString() == tabName) || ProjectInfo._tabbedView.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
                {
                    Dangol.ShowSplash();
                    ProjectInfo._usrBookOrder = new usrBookOrder();
                    ProjectInfo._usrBookOrder.Tag = tabName;
                    ProjectInfo._usrBookOrder.MdiParent = this.MdiParent;

                    ProjectInfo._usrBookOrder.setPreBookOrderLayout(true);
                    ProjectInfo._usrBookOrder.setPreBookOrderData(rows);
                    Dangol.CloseSplash();

                    ProjectInfo._usrBookOrder.Show();
                    //ProjectInfo._biusrUsedPurchaseReceiptDetail.Caption = "중고매입상세";
                    if (!ProjectInfo._ribbonTabs.ContainsKey(ProjectInfo._bbiOrderCartInfo))
                        ProjectInfo._ribbonTabs.Add(ProjectInfo._bbiOrderCartInfo, ProjectInfo._usrBookOrder);
                    else
                    {
                        ProjectInfo._ribbonTabs.Remove(ProjectInfo._bbiOrderCartInfo);
                        ProjectInfo._ribbonTabs.Add(ProjectInfo._bbiOrderCartInfo, ProjectInfo._usrBookOrder);
                    }
                }
                else
                {
                    Dangol.ShowSplash();
                    ProjectInfo._usrBookOrder.cancel();
                    ProjectInfo._usrBookOrder.setPreBookOrderLayout(true);
                    ProjectInfo._usrBookOrder.setPreBookOrderData(rows);
                    ProjectInfo._documentManager.View.ActivateDocument(ProjectInfo._ribbonTabs[ProjectInfo._bbiOrderCartInfo]);
                    Dangol.CloseSplash();
                    ProjectInfo._usrBookOrder.Show();
                }
            }
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

        private void usrBestSeller_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrBestSeller_Shown(object sender, EventArgs e)
        {
            usrBestSellerSearch1.setFocus();
        }
    }
}