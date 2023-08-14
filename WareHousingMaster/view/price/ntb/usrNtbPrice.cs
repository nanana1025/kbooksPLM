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
using DevExpress.XtraSplashScreen;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using WareHousingMaster.view.inventory;
using DevExpress.XtraGrid.Views.Grid;
using WareHousingMaster.view.warehousing.createComponent;
using WareHousingMaster.view.warehousing.selectComponent;
using DevExpress.XtraTreeList.Nodes;
using WareHousingMaster.view.usedPurchase;
using ImportExcel;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;

namespace WareHousingMaster.view.price.ntb
{
    public partial class usrNtbPrice : DevExpress.XtraEditors.XtraForm
    {

        DataRowView _currentNtbPricet;
        DataRowView _currentAdjustrice;

        DataTable _dtNtbPrice;
        DataTable _dtAdjustPrice;
    

        BindingSource _bsNtbPrice;
        BindingSource _bsAdjustPrice;


        long _ntbListId;
        long _ntbPriceId;


        public usrNtbPrice()
        {
            InitializeComponent();

            _dtNtbPrice = new DataTable();
            _dtNtbPrice.Columns.Add(new DataColumn("NO", typeof(int)));
            _dtNtbPrice.Columns.Add(new DataColumn("NTB_LIST_ID", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("NTB_CATEGORY", typeof(string)));
            _dtNtbPrice.Columns.Add(new DataColumn("NTB_CODE", typeof(string)));
            _dtNtbPrice.Columns.Add(new DataColumn("NTB_NAME", typeof(string)));
            _dtNtbPrice.Columns.Add(new DataColumn("NTB_GENERATION", typeof(string)));
            _dtNtbPrice.Columns.Add(new DataColumn("NTB_NICKNAME", typeof(string)));
            _dtNtbPrice.Columns.Add(new DataColumn("NTB_PRICE_ID", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("LT_PURCHASE_PRICE_MAJOR", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("LT_PURCHASE_PRICE_ETC", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("LT_DANAWA_PRICE_MAJOR", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("LT_DANAWA_PRICE_ETC", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("LT_DEALER_PRICE_MAJOR", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("LT_DEALER_PRICE_ETC", typeof(long)));
            _dtNtbPrice.Columns.Add(new DataColumn("STATE", typeof(int)));


            _dtAdjustPrice = new DataTable();
            _dtAdjustPrice.Columns.Add(new DataColumn("NTB_PRICE_ID", typeof(long)));
            _dtAdjustPrice.Columns.Add(new DataColumn("ADJUST_PRICE_ID", typeof(long)));
            _dtAdjustPrice.Columns.Add(new DataColumn("TITLE", typeof(string)));
            _dtAdjustPrice.Columns.Add(new DataColumn("LT_CUSTOMER_PRICE", typeof(long)));
            _dtAdjustPrice.Columns.Add(new DataColumn("LT_CUSTOMER_PRICE_ETC", typeof(long)));
            _dtAdjustPrice.Columns.Add(new DataColumn("LT_DANAWA_PRICE", typeof(long)));
            _dtAdjustPrice.Columns.Add(new DataColumn("LT_DANAWA_PRICE_ETC", typeof(long)));
            _dtAdjustPrice.Columns.Add(new DataColumn("LT_DEALER_PRICE", typeof(long)));
            _dtAdjustPrice.Columns.Add(new DataColumn("LT_DEALER_PRICE_ETC", typeof(long)));

            _bsNtbPrice = new BindingSource();
            _bsAdjustPrice = new BindingSource();
        }


        private void usrWarehousingManagement_Load(object sender, EventArgs e)
        {
            setInfoBox();
            setIInitData();
            setGridControl();
            getNtbPrice();

        }

        public IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        private void setInfoBox()
        {
            DataTable dtCategory = Util.getCodeList("CD1101", "KEY", "VALUE");
            Util.LookupEditHelper(rileCategory, dtCategory, "KEY", "VALUE");

            DataTable dtCode = Util.getCodeList("CD1102", "KEY", "VALUE");
            Util.LookupEditHelper(rileCode, dtCode, "KEY", "VALUE");

            DataTable dtName = Util.getCodeList("CD1103", "KEY", "VALUE");
            Util.LookupEditHelper(rileNm, dtName, "KEY", "VALUE");

            DataTable dtGen = Util.getCodeList("CD1104", "KEY", "VALUE");
            Util.LookupEditHelper(rileGen, dtGen, "KEY", "VALUE");

            //DataTable dtNick = Util.getCodeList("CD1105", "KEY", "VALUE");
            //Util.LookupEditHelper(rileNick, dtNick, "KEY", "VALUE");

        }

        private void setIInitData()
        {
            //// 기본 값 설정
            _bsNtbPrice.DataSource = _dtNtbPrice;
            _bsAdjustPrice.DataSource = _dtAdjustPrice;

            gcSamsung1.Visible = true;
            gcEtc1.Visible = true;

            gcSamsung2.Visible = true;
            gcEtc2.Visible = true;

            gcSamsung3.Visible = true;
            gcEtc3.Visible = true;
        }

        private void setGridControl()
        {
            gcNTB.DataSource = null;
            gcNTB.DataSource = _dtNtbPrice;

            gcAdjustPrice.DataSource = null;
            gcAdjustPrice.DataSource = _bsAdjustPrice;
        }


        private bool getNtbPrice()
        {
            JObject jResult = new JObject();

            _dtNtbPrice.Clear();

            if (DBPrice.getNtbPrice(ref jResult))
            {

                //gvNTB.FocusedRowObjectChanged -= gvNTB_FocusedRowObjectChanged;
                gvNTB.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                int index = 1;
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtNtbPrice.NewRow();

                    dr["NO"] = index++;
                    dr["NTB_LIST_ID"] = obj["NTB_LIST_ID"];
                    dr["NTB_CATEGORY"] = obj["NTB_CATEGORY"];
                    dr["NTB_CODE"] = obj["NTB_CODE"];
                    dr["NTB_NAME"] = obj["NTB_NAME"];
                    dr["NTB_GENERATION"] = obj["NTB_GENERATION"];
                    dr["NTB_NICKNAME"] = obj["NTB_NICKNAME"];
                    dr["NTB_PRICE_ID"] = obj["NTB_PRICE_ID"];
                    dr["LT_PURCHASE_PRICE_MAJOR"] = obj["LT_PURCHASE_PRICE_MAJOR"];
                    dr["LT_PURCHASE_PRICE_ETC"] = obj["LT_PURCHASE_PRICE_ETC"];
                    dr["LT_DANAWA_PRICE_MAJOR"] = obj["LT_DANAWA_PRICE_MAJOR"];
                    dr["LT_DANAWA_PRICE_ETC"] = obj["LT_DANAWA_PRICE_ETC"];
                    dr["LT_DEALER_PRICE_MAJOR"] = obj["LT_DEALER_PRICE_MAJOR"];
                    dr["LT_DEALER_PRICE_ETC"] = obj["LT_DEALER_PRICE_ETC"];
                    dr["STATE"] = 0;
                    

                    _dtNtbPrice.Rows.Add(dr);
                }

                gvNTB.EndDataUpdate();
                //gvNTB.FocusedRowObjectChanged += gvNTB_FocusedRowObjectChanged;
                //gvNTB.MoveFirst();

                return true;

            }
            else
            {
                return false;
            }
        }

        private bool getAdjustPrice()
        {
            JObject jResult = new JObject();

            if (DBPrice.getAdjustPrice(_ntbPriceId, ref jResult))
            {
                gvAdjustPrice.FocusedRowObjectChanged -= gvAdjustPrice_FocusedRowObjectChanged;
                gvAdjustPrice.BeginDataUpdate();

                JArray jArray = JArray.Parse(jResult["DATA"].ToString());
                foreach (JObject obj in jArray.Children<JObject>())
                {
                    DataRow dr = _dtAdjustPrice.NewRow();

                    dr["NTB_PRICE_ID"] = obj["NTB_PRICE_ID"];
                    dr["ADJUST_PRICE_ID"] = obj["ADJUST_PRICE_ID"];
                    dr["TITLE"] = obj["TITLE"];
                    dr["LT_CUSTOMER_PRICE"] = obj["LT_CUSTOMER_PRICE"];
                    dr["LT_CUSTOMER_PRICE_ETC"] = obj["LT_CUSTOMER_PRICE_ETC"];
                    dr["LT_DANAWA_PRICE"] = obj["LT_DANAWA_PRICE"];
                    dr["LT_DANAWA_PRICE_ETC"] = obj["LT_DANAWA_PRICE_ETC"];
                    dr["LT_DEALER_PRICE"] = obj["LT_DEALER_PRICE"];
                    dr["LT_DEALER_PRICE_ETC"] = obj["LT_DEALER_PRICE_ETC"];

                    _dtAdjustPrice.Rows.Add(dr);
                }

                gvAdjustPrice.EndDataUpdate();
                gvAdjustPrice.FocusedRowObjectChanged += gvAdjustPrice_FocusedRowObjectChanged;

                return true;

            }
            else
            {
                return false;
            }
        }

        private void gvNTB_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvNTB.RowCount > 0);

            _dtAdjustPrice.Clear();
            if (isValidRow)
            {
                _currentNtbPricet = e.Row as DataRowView;
                _ntbListId = ConvertUtil.ToInt64(_currentNtbPricet["NTB_LIST_ID"]);
                _ntbPriceId = ConvertUtil.ToInt64(_currentNtbPricet["NTB_PRICE_ID"]);

                getAdjustPrice();

                if (_dtAdjustPrice.Rows.Count == 0)
                    lgcAdjustPrice.CustomHeaderButtons[0].Properties.Visible = true;
                else
                    lgcAdjustPrice.CustomHeaderButtons[0].Properties.Visible = false;

            }
            else
            {
                _currentNtbPricet = null;
            }
        }

        private void gvAdjustPrice_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            bool isValidRow = (e.FocusedRowHandle >= 0 && gvAdjustPrice.RowCount > 0);

            if (isValidRow)
            {
                _currentAdjustrice = e.Row as DataRowView;
            }
            else
            {
                _currentAdjustrice = null;
            }

            
        }

        private void lgcNtbPrice_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int rowhandle = gvNTB.FocusedRowHandle;
            gvNTB.FocusedRowHandle = -1;
            gvNTB.FocusedRowHandle = rowhandle;

            if (Dangol.MessageYN("노트북 가격 정보를 저장하시겠습니까?") == DialogResult.No)
            {
                return;
            }

            DataRow[] rowsUpdate = _dtNtbPrice.Select("STATE = 2");

            if (rowsUpdate.Length < 1)
            {
                Dangol.Message("변경사항이 없습니다.");
                return;
            }

            var jArray = new JArray();
            JObject jobj = new JObject();
            JObject jResult = new JObject();

            if (rowsUpdate.Length > 0)
            {
                foreach (DataRow row in rowsUpdate)
                {
                    JObject jdata = new JObject();
                    jdata.Add("NTB_LIST_ID", ConvertUtil.ToString(row["NTB_LIST_ID"]));
                    jdata.Add("NTB_PRICE_ID", ConvertUtil.ToString(row["NTB_PRICE_ID"]));

                    jdata.Add("NTB_CATEGORY", ConvertUtil.ToString(row["NTB_CATEGORY"]));
                    jdata.Add("NTB_CODE", ConvertUtil.ToString(row["NTB_CODE"]));
                    jdata.Add("NTB_NAME", ConvertUtil.ToString(row["NTB_NAME"]));
                    jdata.Add("NTB_GENERATION", ConvertUtil.ToString(row["NTB_GENERATION"]));
                    jdata.Add("NTB_NICKNAME", ConvertUtil.ToString(row["NTB_NICKNAME"]));

                    jdata.Add("LT_PURCHASE_PRICE_MAJOR", ConvertUtil.ToInt64(row["LT_PURCHASE_PRICE_MAJOR"]));
                    jdata.Add("LT_PURCHASE_PRICE_ETC", ConvertUtil.ToInt64(row["LT_PURCHASE_PRICE_ETC"]));
                    jdata.Add("LT_DANAWA_PRICE_MAJOR", ConvertUtil.ToInt64(row["LT_DANAWA_PRICE_MAJOR"]));
                    jdata.Add("LT_DANAWA_PRICE_ETC", ConvertUtil.ToInt64(row["LT_DANAWA_PRICE_ETC"]));
                    jdata.Add("LT_DEALER_PRICE_MAJOR", ConvertUtil.ToInt64(row["LT_DEALER_PRICE_MAJOR"]));
                    jdata.Add("LT_DEALER_PRICE_ETC", ConvertUtil.ToInt64(row["LT_DEALER_PRICE_ETC"]));

                    jArray.Add(jdata);
                }

                jobj.Add("DATA", jArray);


                if (DBPrice.updateNtbPrice(jobj, ref jResult))
                {
                    Dangol.Message("저장되었습니다");
                    foreach (DataRow row in rowsUpdate)
                    {
                        row["STATE"] = 0;
                    }

                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
        }
    

        private void lgcAdjustPrice_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.Equals(1))
            {
                JObject jResult = new JObject();
                if (DBPrice.insertAdjustPrice(_ntbPriceId, ref jResult))
                {
                    getAdjustPrice();
                    lgcAdjustPrice.CustomHeaderButtons[0].Properties.Visible = false;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }
            else if (e.Button.Properties.Tag.Equals(2))
            {
                if (Dangol.MessageYN("차감 가격 정보를 저장하시겠습니까?") == DialogResult.No)
                {
                    return;
                }

                DataRow[] rowsUpdate = _dtAdjustPrice.Select("NTB_PRICE_ID > 0");

                if (rowsUpdate.Length < 1)
                {
                    Dangol.Message("변경사항이 없습니다.");
                    return;
                }

                var jArray = new JArray();
                JObject jobj = new JObject();
                JObject jResult = new JObject();

                if (rowsUpdate.Length > 0)
                {
                    foreach (DataRow row in rowsUpdate)
                    {
                        JObject jdata = new JObject();

                        jdata.Add("ADJUST_PRICE_ID", ConvertUtil.ToInt64(row["ADJUST_PRICE_ID"]));
                        jdata.Add("LT_CUSTOMER_PRICE", ConvertUtil.ToInt64(row["LT_CUSTOMER_PRICE"]));
                        jdata.Add("LT_CUSTOMER_PRICE_ETC", ConvertUtil.ToInt64(row["LT_CUSTOMER_PRICE_ETC"]));
                        jdata.Add("LT_DANAWA_PRICE", ConvertUtil.ToInt64(row["LT_DANAWA_PRICE"]));
                        jdata.Add("LT_DANAWA_PRICE_ETC", ConvertUtil.ToInt64(row["LT_DANAWA_PRICE_ETC"]));
                        jdata.Add("LT_DEALER_PRICE", ConvertUtil.ToInt64(row["LT_DEALER_PRICE"]));
                        jdata.Add("LT_DEALER_PRICE_ETC", ConvertUtil.ToInt64(row["LT_DEALER_PRICE_ETC"]));

                        jArray.Add(jdata);
                    }

                    jobj.Add("DATA", jArray);


                    if (DBPrice.updateAdjustPrice(jobj, ref jResult))
                    {
                        Dangol.Message("저장되었습니다");
                    }
                    else
                    {
                        Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    }
                }
            }
        }

        private void rgType_EditValueChanged(object sender, EventArgs e)
        {
            int type = ConvertUtil.ToInt32(rgType.EditValue);

            gcAdjustPrice.BeginUpdate();

            //if(type == 1)
            //{
            //    gcSamsung1.Visible = true;
            //    gcEtc1.Visible = true;

            //    gcSamsung2.Visible = false;
            //    gcEtc2.Visible = false;

            //    gcSamsung3.Visible = false;
            //    gcEtc3.Visible = false;
            //}
            //else if (type == 2)
            //{
            //    gcSamsung1.Visible = false;
            //    gcEtc1.Visible = false;

            //    gcSamsung2.Visible = true;
            //    gcEtc2.Visible = true;

            //    gcSamsung3.Visible = false;
            //    gcEtc3.Visible = false;
            //}
            //else if (type == 3)
            //{
            //    gcSamsung1.Visible = false;
            //    gcEtc1.Visible = false;

            //    gcSamsung2.Visible = false;
            //    gcEtc2.Visible = false;

            //    gcSamsung3.Visible = true;
            //    gcEtc3.Visible = true;
            //}

            gcAdjustPrice.EndUpdate();
        }

        private void gvNTB_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _currentNtbPricet["STATE"] = 2;
        }
    }
}