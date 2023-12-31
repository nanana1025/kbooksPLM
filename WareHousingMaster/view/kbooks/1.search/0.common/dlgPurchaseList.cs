﻿using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.kbooks.search.booksearch;

namespace WareHousingMaster.view.kbooks.search.common
{
    public partial class dlgPurchaseList : DevExpress.XtraEditors.XtraForm
    {
        string _title;

        int _shopCd;
        public string _purchaseCompany { get; private set; }


        public DataRowView _drv { get; private set; }

        public int Cnt { get; private set; }

        public dlgPurchaseList(string purchaseCompany, int shopCd = 1)
        {
            InitializeComponent();
            _purchaseCompany = purchaseCompany;
            _shopCd = shopCd;

        }
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrPurchaseList1.focusedRowObjectChangeHandler += new usrPurchaseList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler); 
            usrPurchaseList1.selectHandler += new usrPurchaseList.SelectHandler(select);
            usrPurchaseList1.doubleClickHandler += new usrPurchaseList.DoubleClickHandler(doubleClickHandler);
            usrPurchaseList1.setInitLoad();


            JObject jobj = new JObject();
            jobj.Add("SHOPCD", _shopCd);
            jobj.Add("PURCHNM", _purchaseCompany);
            usrPurchaseList1.getList(jobj);
        }


        private void FocusedRowObjectChangeHandler(DataRowView drv)
        {
            _drv = drv;
            //JObject jobj = new JObject();


            //int completeYn;
            //if (drv != null)
            //{
            //    _id = ConvertUtil.ToInt64(drv["RELEASE_ID"]);
            //    _releaseState = ConvertUtil.ToInt32(drv["RELEASE_STATE"]);
            //    completeYn = ConvertUtil.ToInt32(drv["COMPLETE_YN"]);
            //}
            //else
            //{
            //    _id = -1;
            //    _releaseState = 0;
            //    completeYn = 0;
            //}

            //jobj.Add("RELEASE_ID", _id);
            //jobj.Add("RELEASE_STATE", _releaseState);
            //jobj.Add("COMPLETE_YN", completeYn);
            //jobj.Add("ID", _id);

            //setCustomheaderButtonEnabled(_releaseState, completeYn);

            //usrReleaseDetail1.setLayoutViewByState(_id, _releaseState, completeYn);
            //usrReleaseDetail1.rgReleaseCategoryChange(drv);

            //usrReleaseNAccount1.setLayoutViewByState(drv, _id, _releaseState, completeYn == 1);
            //if (!_isInit) Dangol.ShowSplash();
            //usrReleaseItemList1.getList(jobj);
            //if (!_isInit) Dangol.CloseSplash();

        }

        private void doubleClickHandler()
        {
            select();
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            select();
        }

        private void select()
        {
            if (_drv == null)
            {
                Dangol.Warining("선택한 매입처가 없습니다.");
            }
            else
            {
                if (Dangol.MessageYN("선택한 매입처를 적용하시겠습니까?") == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dlgPurchaseList_Shown(object sender, EventArgs e)
        {
            usrPurchaseList1.SetFocus();
        }

        private void dlgPurchaseList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10 || e.KeyCode == Keys.F9)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}