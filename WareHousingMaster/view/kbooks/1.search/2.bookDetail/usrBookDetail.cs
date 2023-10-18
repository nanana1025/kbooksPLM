using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.search.bookDetail
{
    public partial class usrBookDetail : DevExpress.XtraEditors.XtraForm
    {
        long _bookCd;
        int _shopCd;
        bool _isEditable;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrBookDetail(long bookCd, int shopCd = 1, bool isEditable = false)
        {
            InitializeComponent();

            _bookCd = bookCd;
            _shopCd = shopCd;
            _isEditable = isEditable;

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };

            if (isEditable)
            {
                _arrFunctionText = new string[] { "F1\n조회", "F2", "F3", "F4", "F5", "F6", "F7\n취소", "F8", "F9", "F10\n닫기", };
                _arrFunctionEditable = new bool[] { true, false, false, false, false, false, true, false, false, true };
            }
            else
            {
                _arrFunctionText = new string[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10\n닫기", };
                _arrFunctionEditable = new bool[] { false, false, false, false, false, false, false, false, false, true };
            }
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            usrBookInfoDetail1.searchHandler += new usrBookInfoDetail.SearchHandler(searchList);

            setInitialize(_isEditable);
            getBookInfo();
        }

        private void setInitialize(bool isEditable)
        {
            usrBookInfoDetail1.setInitLoad(isEditable);
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
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
                    if (_isEditable)
                        usrBookInfoDetail1.searchBook();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    if (_isEditable)
                        usrBookInfoDetail1.clear();
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
                    if (_isEditable)
                        this.Close();
                    else
                        this.DialogResult = DialogResult.OK;
                    break;
                default:
                    break;

            }
        }

        private void getBookInfo()
        {
            if (_bookCd > 0)
                usrBookInfoDetail1.getList(_bookCd, _shopCd);
        }

        private void usrBookDetail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                if (_isEditable)
                    this.Close();
                else
                {
                    e.Handled = true;
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (_dicKeys.ContainsKey(e.KeyCode))
                    processHandler(_dicKeys[e.KeyCode]);
            }
        }

        private void usrBookDetail_Shown(object sender, EventArgs e)
        {
            usrBookInfoDetail1.setFocus();
        }
    }
}