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

namespace WareHousingMaster.view.kbooks.search.booksearch
{
    public partial class dlgControl : DevExpress.XtraEditors.XtraForm
    {
        string _title;
        public string _text { get; private set; }

        public DataRowView _drv { get; private set; }

        public int Cnt { get; private set; }

        public dlgControl(string title)
        {
            InitializeComponent();
            _title = title;

        }
        private void usrForm_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            usrBookSearchList1.focusedRowObjectChangeHandler += new usrBookSearchList.FocusedRowObjectChangeHandler(FocusedRowObjectChangeHandler);
            
            JObject jobj = new JObject();
            jobj.Add("TITLE", _title);
            usrBookSearchList1.getList(jobj);
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

        private void sbOk_Click(object sender, EventArgs e)
        {
            if(_drv == null)
            {
                Dangol.Warining("선택한 도서가 없습니다.");
            }
            else
            {
                if(Dangol.MessageYN("선택한 도서를 적용하시겠습니까?") == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}