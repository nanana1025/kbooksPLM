using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.warehouisng
{
    public partial class WarehousingPlan : DevExpress.XtraEditors.XtraForm
    {
        DataRowView _currentRow;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;


        public WarehousingPlan()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };
            _arrFunctionText = new string[] { "F1\n조건확정", "F2\n행삭제", "F3\n도서검색", "F4\n신간등록", "F5", "F6", "F7\n취소", "F8\n확정", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, true, true, true, false, false, true, true, false, true };

        }


        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            warehousingPlanSearch1.searchHandler += new WarehousingPlanSearch.SearchHandler(searchList);
            warehousingPlanSearch1.clearHandler += new WarehousingPlanSearch.ClearHandler(clear);

            //usrInputReturnSearch1.confirmHandler += new usrInputReturnSearch.ConfirmHandler(confirmHandler);
            //usrInputReturnSearch1.searchPerformanceHandler += new usrInputReturnSearch.SearchPerformanceHandler(searchPerformanceHandler);
            //usrInputReturnSearch1.deleteRowHandler += new usrInputReturnSearch.DeleteRowHandler(deleteRowHandler);

            warehousingPlanList1.focusedRowObjectChangeHandler += new WarehousingPlanList.FocusedRowObjectChangeHandler(focusedRowObjectChangeHandler);
            //warehousingPlanList1.getSearchInfoHandler += new WarehousingPlanList.GetSearchInfoHandler(getSearchInfo);

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

            setInitialize();
        }

        private void setInitialize()
        {
            warehousingPlanSearch1.setInitLoad();
            warehousingPlanList1.setInitLoad();
        }

        private void searchList(JObject jobj)
        {
           
            int noticeType = ConvertUtil.ToInt32(jobj["ORDER_NOTICE"]);


            if(noticeType == 1)
            {
                warehousingPlanList1.setTableInitialize(jobj);

                warehousingPlanList1.SetFocus();
                warehousingPlanList1.SetColFocus("BOOKNM", 0);
            }
            else
            {
                warehousingPlanList1.setTableEditable(true);
                warehousingPlanList1.setCondition(jobj);

                warehousingPlanList1.SetFocus();
                warehousingPlanList1.SetColFocus("BOOKNM", 0);
            }
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    if (!warehousingPlanList1.checkDataExist())
                        warehousingPlanSearch1.Searches();
                    else
                    {
                        if(Dangol.MessageYN("작업중인 데이터가 있습니다. 계속하시겠습니까?") == DialogResult.Yes)
                            warehousingPlanSearch1.Searches();
                    }
                    break;
                case 2:
                    deleteRowHandler();
                    break;
                case 3:
                    searchBook();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    if (warehousingPlanList1.isPossibleClear())
                        warehousingPlanSearch1.Searches();
                    else
                    {
                        if (Dangol.MessageYN("작업중인 데이터가 있습니다. 계속하시겠습니까?") == DialogResult.Yes)
                            warehousingPlanSearch1.Searches();
                    }
                    reset();
                    break;
                case 8:
                    confirmHandler();
                    reset();
                    break;
                case 9:
                    //this.Close();
                    break;
                case 10:
                    this.Close();
                    break;
                default:
                    break;

            }
        }
        //private JObject getSearchInfo()
        //{
        //    return warehousingPlanSearch1.getSearchInfo();
        //}

        private void focusedRowObjectChangeHandler(DataRowView currentRow)
        {
            _currentRow = currentRow;
        }

        private void reset()
        {
            if(warehousingPlanList1.checkDataExist())
            {
                if (Dangol.MessageYN("확정하지 않은 데이터가 존재합니다. 취소하시겠습니까?") == DialogResult.Yes) ;
                    cancel();
            }
            else
                cancel();
        }

        private void clear()
        {
            warehousingPlanList1.setTableInitialize();
            warehousingPlanList1.setTableEditable(false);
        }

        public void cancel()
        {
            warehousingPlanSearch1.clear();
            warehousingPlanList1.setTableInitialize();
            warehousingPlanList1.setTableEditable(false);
            warehousingPlanSearch1.setFocus();
        }

        private void searchPerformanceHandler()
        {
            //warehousingPlanList1.getPerformance();
        }

        private void deleteRowHandler()
        {
            warehousingPlanList1.deleteRowHandler();
        }

        private void confirmHandler()
        {
            if (warehousingPlanList1.insertOrder())
                cancel();
        }

        private void usrInputReturn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrInputReturn_Shown(object sender, EventArgs e)
        {
            warehousingPlanSearch1.setFocus();
        }

        //private JObject getSearchInfo()
        //{
        //    return usrInputReturnSearch1.getSearchInfo();
        //}

        public void searchBook()
        {
            string tabName = "도서정보조회";

            if (!(ProjectInfo._tabbedView.Documents.Any(x => x.Form.Tag.ToString() == tabName) || ProjectInfo._tabbedView.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                ProjectInfo._usrSearchBook = new usrSearchBook();
                ProjectInfo._usrSearchBook.Tag = tabName;
                ProjectInfo._usrSearchBook.MdiParent = this.MdiParent;
                Dangol.CloseSplash();

                ProjectInfo._usrSearchBook.Show();

                if (!ProjectInfo._ribbonTabs.ContainsKey(ProjectInfo._bbiOrderCartInfo))
                    ProjectInfo._ribbonTabs.Add(ProjectInfo._bbiOrderCartInfo, ProjectInfo._usrSearchBook);
                else
                {
                    ProjectInfo._ribbonTabs.Remove(ProjectInfo._bbiOrderCartInfo);
                    ProjectInfo._ribbonTabs.Add(ProjectInfo._bbiOrderCartInfo, ProjectInfo._usrSearchBook);
                }
            }
            else
            {
                Dangol.ShowSplash();
                ProjectInfo._documentManager.View.ActivateDocument(ProjectInfo._ribbonTabs[ProjectInfo._bbiOrderCartInfo]);
                Dangol.CloseSplash();
                ProjectInfo._usrSearchBook.Show();
            }
        }

    }
}