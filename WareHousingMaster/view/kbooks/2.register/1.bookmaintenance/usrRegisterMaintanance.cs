using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.register
{
    public partial class usrRegisterMaintanance : DevExpress.XtraEditors.XtraForm
    {
        long _bookCd;
        int _shopCd;
        bool _isEditable;

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrRegisterMaintanance()
        {
            InitializeComponent();

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };

            _arrFunctionText = new string[] { "F1\n등록", "F2\n변경", "F3\n삭제", "F4", "F5", "F6\n매입처정보", "F7", "F8\n취소", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { false, false, false, false, false, true, false, true, false, true };
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            usrRegisterMaintananceDetail1.buttonHandler += new usrRegisterMaintananceDetail.ButtonHandler(buttonHandler);
            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);

            

            setInitialize(_isEditable);
            //getBookInfo();
        }

        private void setInitialize(bool isEditable)
        {
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);

            usrRegisterMaintananceDetail1.setInitLoad();
            usrRegisterMaintananceDetail1.setFocus();
        }

        //private void searchList(JObject jobj)
        //{
        //    _bookCd = ConvertUtil.ToInt64(jobj["BOOKCD"]);
        //    getBookInfo();
        //}

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    usrRegisterMaintananceDetail1.InsertBookInfo();
                    break;
                case 2:
                    usrRegisterMaintananceDetail1.UpdateBookInfo();
                    break;
                case 3:
                    usrRegisterMaintananceDetail1.DeleteBookInfo();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    usrRegisterMaintananceDetail1.setPurchaseInfo();
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

        private void buttonHandler(bool search, bool update, bool delete)
        {
            usrSideCheck1.setButtonEnableExecutionTime(1, search);
            usrSideCheck1.setButtonEnableExecutionTime(2, update);
            usrSideCheck1.setButtonEnableExecutionTime(3, delete);
        }

        private void getBookInfo()
        {
            //if (_bookCd > 0)
                //usrBookInfoDetail1.getList(_bookCd, _shopCd);
        }

        private void usrBookDetail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_dicKeys.ContainsKey(e.KeyCode))
                processHandler(_dicKeys[e.KeyCode]);
        }

        private void usrBookDetail_Shown(object sender, EventArgs e)
        {
            //usrBookInfoDetail1.setFocus();
        }
    }
}