using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.register
{
    public partial class usrCalendar : DevExpress.XtraEditors.XtraForm
    {
        long _bookCd;
        int _shopCd;
        bool _isEditable;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        bool _isDirect;

        public usrCalendar()
        {
            InitializeComponent();

            _isDirect = false;

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };

            
            _arrFunctionText = new string[] { "F1\n확정", "F2", "F3", "F4", "F5", "F6", "F7\n취소", "F8", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { true, false, false, false, false, false, true, false, false, true };
           
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrCalendarList1.existHandler += new usrCalendarList.ExistHandler(setDataExist);
            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            //usrBookInfoDetail1.searchHandler += new usrBookInfoDetail.SearchHandler(searchList);


            setInitialize(_isEditable);
           
            //getBookInfo();
        }

        private void setInitialize(bool isEditable)
        {
            List<string> listLongCol = new List<string>(new string[] { "SHOPCD" });
            DataTable dtShopCd = Util.getTable("HMA09", "SHOPCD,SHOP_NM ", "SHOPCD > 0", "SHOPCD ASC", listLongCol);
            Util.LookupEditHelper(leShopCd, dtShopCd, "SHOPCD", "SHOP_NM");

            deThisMonth.EditValue = DateTime.Today;
            _shopCd = ConvertUtil.ToInt32(leShopCd.EditValue);

            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
            usrCalendarList1.setInitLoad(_shopCd);
        }

        private void searchList(JObject jobj)
        {
            _bookCd = ConvertUtil.ToInt64(jobj["BOOKCD"]);
            getBookInfo();
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrCalendarList1.InsertUpdateHolidayDate();
                    break;
                case 2:
                    //usrPublisherMaintananceDetail1.UpdatePublishInfo();
                    break;
                case 3:
                    //usrPublisherMaintananceDetail1.DeletePublishInfo();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    //if (_isEditable)
                        //usrBookInfoDetail1.clear();
                    break;
                case 8:
                    break;
                case 9:
                    //if (_isEditable)
                    //    this.Close();
                    //else
                    //    this.DialogResult = DialogResult.OK;
                    break;
                case 10:
                    this.Close();
                    break;
                default:
                    break;

            }
        }

        private void getBookInfo()
        {
            //if (_bookCd > 0)
                //usrBookInfoDetail1.getList(_bookCd, _shopCd);
        }
        private void buttonHandler(bool search, bool update, bool delete)
        {
            //usrSideCheck1.setButtonEnableExecutionTime(1, search);
            //usrSideCheck1.setButtonEnableExecutionTime(2, update);
            //usrSideCheck1.setButtonEnableExecutionTime(3, delete);
        }
        private void setDataExist(bool exist)
        {
            lbExist.Text = exist ? "" : "데이터 없음";
        }
        private void usrCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
            else
            {
                if(e.Control && e.KeyCode == Keys.Right)
                {
                    nextMonth(-1);
                }
                else if (e.Control && e.KeyCode == Keys.Left)
                {
                    prevMonth(+1);
                }
            }
        }
        private void usrBookDetail_Shown(object sender, EventArgs e)
        {
            setFirstDayFocus();
        }

        private void setFirstDayFocus(int move = 0)
        {
            usrCalendarList1.SetFocus();
            usrCalendarList1.SetColFocus(move);
        }

        private void sbPrevMonth_Click(object sender, EventArgs e)
        {
            prevMonth(0);
        }
        private void prevMonth(int move = 0)
        { 
            if (deThisMonth.EditValue != null && !string.IsNullOrEmpty(deThisMonth.EditValue.ToString()))
            {
                _isDirect = false;
                string dtDate = "";
                dtDate = $"{deThisMonth.Text}-01 00:00:00";
                DateTime deMonth = Convert.ToDateTime(dtDate);
                deMonth = deMonth.AddMonths(-1);
                deThisMonth.EditValue = deMonth;
                usrCalendarList1.SetCalendar(deMonth);
                setFirstDayFocus(move);
                _isDirect = true;
            }
        }

        private void sbNextMonth_Click(object sender, EventArgs e)
        {
            nextMonth(0);
        }
        private void nextMonth(int move = 0)
        { 
            if (deThisMonth.EditValue != null && !string.IsNullOrEmpty(deThisMonth.EditValue.ToString()))
            {
                _isDirect = false;
                string dtDate = "";
                dtDate = $"{deThisMonth.Text}-01 00:00:00";
                DateTime deMonth = Convert.ToDateTime(dtDate);
                deMonth = deMonth.AddMonths(+1);
                deThisMonth.EditValue = deMonth;
                usrCalendarList1.SetCalendar(deMonth);
                setFirstDayFocus(move);
                _isDirect = true;
            }
        }

        private void deThisMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (deThisMonth.EditValue != null && !string.IsNullOrEmpty(deThisMonth.EditValue.ToString()))
            {
                if (_isDirect)
                {
                    string dtDate = "";
                    dtDate = $"{deThisMonth.Text}-01 00:00:00";
                    DateTime deMonth = Convert.ToDateTime(dtDate);
                    usrCalendarList1.SetCalendar(deMonth);
                    setFirstDayFocus();
                }
            }
        }

       
    }
}