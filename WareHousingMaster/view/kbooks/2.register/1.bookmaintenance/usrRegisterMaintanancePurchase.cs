using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.kbooks.search.common;

namespace WareHousingMaster.view.kbooks.register
{
    public partial class usrRegisterMaintanancePurchase : DevExpress.XtraEditors.XtraForm
    {
        long _bookCd;
        int _shopCd;
        bool _isEditable;

        Dictionary<int, Dictionary<string, string>> _dicPurchaseInfo;
        Dictionary<int, DataTable> _dicPurchaseInfoTable;

        int _price;

        public int _modifiedPrice { get; private set; }

        string[] _arrFunctionText;
        bool[] _arrFunctionEditable;
        Dictionary<Keys, int> _dicKeys;

        public usrRegisterMaintanancePurchase(Dictionary<int, Dictionary<string, string>> dicPurchaseInfo, Dictionary<int, DataTable> dicPurchaseInfoTable, int price)
        {
            InitializeComponent();

            _dicPurchaseInfo = dicPurchaseInfo;
            _dicPurchaseInfoTable = dicPurchaseInfoTable;
            _price = price;

            //_bookCd = bookCd;
            //_shopCd = shopCd;
            //_isEditable = isEditable;

            _dicKeys = new Dictionary<Keys, int>
            {
                { Keys.F1, 1 }, { Keys.F2, 2 },{ Keys.F3, 3 },{ Keys.F4, 4 },{ Keys.F5, 5 },{ Keys.F6, 6 },{ Keys.F7, 7 },{ Keys.F8, 8 },{ Keys.F9, 9 },{ Keys.F10, 10 }
            };

           
            _arrFunctionText = new string[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10\n닫기", };
            _arrFunctionEditable = new bool[] { false, false, false, false, false, false, true, false, false, true };
            
        }

       
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrSideCheck1.processHandler += new usrSideCheck.ProcessHandler(processHandler);
            //usrBookInfoDetail1.searchHandler += new usrBookInfoDetail.SearchHandler(searchList);

            usrRegisterPurchaseInfo1.setInitLoad(_dicPurchaseInfo[1], _dicPurchaseInfoTable[1], _price);
            usrRegisterPurchaseInfo2.setInitLoad(_dicPurchaseInfo[2], _dicPurchaseInfoTable[2], _price);
            usrRegisterPurchaseInfo3.setInitLoad(_dicPurchaseInfo[3], _dicPurchaseInfoTable[3], _price);
            usrRegisterPurchaseInfo4.setInitLoad(_dicPurchaseInfo[4], _dicPurchaseInfoTable[4], _price);
            usrRegisterPurchaseInfo5.setInitLoad(_dicPurchaseInfo[5], _dicPurchaseInfoTable[5], _price);
            usrRegisterPurchaseInfo6.setInitLoad(_dicPurchaseInfo[6], _dicPurchaseInfoTable[6], _price);

            setInitialize(_isEditable);
            //getBookInfo();
        }

        private void setInitialize(bool isEditable)
        {
            //usrBookInfoDetail1.setInitLoad(isEditable);
            usrSideCheck1.setInitLoad(_arrFunctionText, _arrFunctionEditable);
        }

        private void searchList(JObject jobj)
        {
            _bookCd = ConvertUtil.ToInt64(jobj["BOOKCD"]);
            //getBookInfo();
        }

        private void processHandler(int F)
        {
            switch (F)
            {
                case 1:
                    //if (_isEditable)
                        //usrBookInfoDetail1.searchBook();
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
            //if (_bookCd > 0)
                //usrBookInfoDetail1.getList(_bookCd, _shopCd);
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
            //usrBookInfoDetail1.setFocus();
        }
    }
}