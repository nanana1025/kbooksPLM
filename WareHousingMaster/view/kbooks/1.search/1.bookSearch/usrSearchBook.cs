﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class usrSearchBook : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _drv;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;
        public usrSearchBook()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] {"F1\n도서검색", "F2\n도서명정렬", "F3\n저자명 정렬", "F4\n출판사 정렬", "F5", "F6\n도서상세정보", "F7\n도서주문", "F8\n실적조회", "F9\n초기화", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, true, false, true, true, true, true, true };
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrSearchBookSearch1.searchHandler += new usrSearchBookSearch.SearchHandler(searchList);
            usrSearchBookSearchList1.focusedRowObjectChangeHandler += new usrSearchBookSearchList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);

            setInitialize();
        }

        private void setInitialize()
        {
            usrSearchBookSearch1.setInitLoad();
            usrSearchBookSearchList1.setInitLoad();
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

            //this.ActiveControl = usrSearchBookSearch1;
            //usrSearchBookSearch1.setFocus();

        }

        private void searchList(JObject jobj)
        {
            usrSearchBookSearchList1.getList(jobj);
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    JObject jobj = usrSearchBookSearch1.getSearch();
                    if (jobj != null)
                    {
                        usrSearchBookSearchList1.getList(jobj);
                        usrSearchBookSearchList1.setFocus();
                    }
                    else
                        usrSearchBookSearchList1.clearGridView();
                    break;
                case 2:
                    usrSearchBookSearchList1.sortList("BOOKNM");
                    break;
                case 3:
                    usrSearchBookSearchList1.sortList("AUTHOR1");
                    break;
                case 4:
                    usrSearchBookSearchList1.sortList("PUBSHNM");
                    break;
                case 5:
                    break;
                case 6:
                    usrSearchBookSearchList1.showBookInfoDetail();
                    break;
                case 7:
                    goOrderBook();
                    break;
                case 8:
                    break;
                case 9:
                    usrSearchBookSearchList1.clearGridView();
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

        private void lcgSearchBookList_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            
            if (e.Button.Properties.Tag.Equals(0))
            {
                usrSearchBookSearchList1.clearGridView();
            }
            else if (e.Button.Properties.Tag.Equals(1))
            {
                JObject jobj = usrSearchBookSearch1.getSearch();
                if(jobj != null)
                    usrSearchBookSearchList1.getList(jobj);
                else
                    usrSearchBookSearchList1.clearGridView();
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                usrSearchBookSearchList1.showBookInfoDetail();
            }
            else if (e.Button.Properties.Tag.Equals(3))
            {
                goOrderBook();
            }
        }

        private void goOrderBook()
        {
            string tabName = "주문 예정입력";

            if (!(ProjectInfo._tabbedView.Documents.Any(x => x.Form.Tag.ToString() == tabName) || ProjectInfo._tabbedView.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                ProjectInfo._usrBookOrder = new usrBookOrder();
                ProjectInfo._usrBookOrder.Tag = tabName;
                ProjectInfo._usrBookOrder.MdiParent = this.MdiParent;

                //ProjectInfo._usrConsignedReturnList.setInitData(ConvertUtil.ToString(_currentReceipt["RETURN_DT"]), ConvertUtil.ToString(_currentReceipt["RECEIPT"]), ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"]));
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
                //ProjectInfo._usrConsignedReturnList.reload(ConvertUtil.ToString(_currentReceipt["RETURN_DT"]), ConvertUtil.ToString(_currentReceipt["RECEIPT"]), ConvertUtil.ToInt64(_currentReceipt["COMPANY_ID"]));
                ProjectInfo._documentManager.View.ActivateDocument(ProjectInfo._ribbonTabs[ProjectInfo._bbiOrderCartInfo]);
                Dangol.CloseSplash();
                ProjectInfo._usrBookOrder.Show();
            }
        }

        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;
        }

        private void usrSearchBook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);            
        }

        private void usrSearchBook_Shown(object sender, EventArgs e)
        {
            usrSearchBookSearch1.setFocus();
        }
    }
}