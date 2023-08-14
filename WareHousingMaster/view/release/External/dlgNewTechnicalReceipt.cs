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
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Columns;

namespace WareHousingMaster.view.release.External
{
    public partial class dlgNewTechnicalReceipt : DevExpress.XtraEditors.XtraForm
    {
        Regex regex1;
        Regex regex2;
        Regex regex3;
        Regex regex4;

        public long _newReceiptId { set; get; }

        public dlgNewTechnicalReceipt()
        {
            InitializeComponent();

            regex1 = new Regex(@"^LT2[0-9]{9}$");
            regex2 = new Regex(@"^RT2[0-9]{9}$");
            regex3 = new Regex(@"^GT2[0-9]{9}$");
            regex4 = new Regex(@"^E[0-9]{9}$");

        }
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            DataTable dtProductType = new DataTable();

            dtProductType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtProductType.Columns.Add(new DataColumn("VALUE", typeof(string)));
            Util.insertRowonTop(dtProductType, 1, "Product");
            Util.insertRowonTop(dtProductType, 2, "Part");
            Util.LookupEditHelper(leProductType, dtProductType, "KEY", "VALUE");


            DataTable dtRequestType = Util.getCodeList("CD0822", "KEY", "VALUE");
            Util.insertRowonTop(dtRequestType, "-1", "N/A");
            Util.LookupEditHelper(leRequestType, dtRequestType, "KEY", "VALUE");

            DataTable dtRequestTypeDetail = Util.getCodeList("CD082401", "KEY", "VALUE");
            Util.insertRowonTop(dtRequestTypeDetail, "-1", "N/A");
            Util.LookupEditHelper(leRequestTypeDetail, dtRequestTypeDetail, "KEY", "VALUE");

            //DataTable dtModelId = Util.getCodeList("CD0826", "KEY", "VALUE");

            DataTable dtModelId = Util.getTable("TN_MBD", "DATA_ID, TYPE_CD, PRODUCT_NAME, MBD_MODEL_NM", "TYPE_CD = 'CPN' AND PRODUCT_NAME IS NOT NULL AND MBD_MODEL_NM IS NOT NULL", "PRODUCT_NAME ASC", "PRODUCT_NAME, MBD_MODEL_NM");

            foreach (DataRow row in dtModelId.Rows)
                row["TYPE_CD"] = $"{row["PRODUCT_NAME"]}/{row["MBD_MODEL_NM"]}";

            DataRow dr = dtModelId.NewRow();
            dr["DATA_ID"] = "-1";
            dr["TYPE_CD"] = "N/A";
            dr["PRODUCT_NAME"] = "N/A";
            dr["MBD_MODEL_NM"] = "N/A";
            dtModelId.Rows.InsertAt(dr, 0);

            leModelId.Properties.BeginUpdate();
            leModelId.Properties.View.Columns.Clear();
            leModelId.Properties.DisplayMember = "TYPE_CD";
            GridColumn col1 = leModelId.Properties.View.Columns.AddField("PRODUCT_NAME");
            GridColumn col2 = leModelId.Properties.View.Columns.AddField("MBD_MODEL_NM");
            col1.Visible = true;
            col2.Visible = true;
            leModelId.Properties.ValueMember = "DATA_ID";

            leModelId.Properties.View.OptionsView.ShowGroupPanel = false;
            leModelId.Properties.View.OptionsView.ShowColumnHeaders = true;
            leModelId.Properties.ShowFooter = false;
            leModelId.Properties.ShowClearButton = false;
            leModelId.Properties.DataSource = dtModelId;
            leModelId.Properties.EndUpdate();

            //Util.LookupEditHelper(leModelId, dtModelId, "KEY", "VALUE");

            Util.LookupEditHelper(leReceiptUserId, ProjectInfo._dtUserId, "KEY", "VALUE");

            DataTable dtCompany = Util.getCodeListCustom("TN_COMPANY_MST", "COMPANY_ID", "COMPANY_NM", "DEL_YN = 'N'", "COMPANY_NM ASC");
            Util.insertRowonTop(dtCompany, "-1", "SELECT ALL");
            Util.LookupEditHelper(leCompanyId1, dtCompany, "KEY", "VALUE");

            leCompanyId1.EditValue = ConvertUtil.ToString(ProjectInfo._userCompanyId);
            leReceiptUserId.EditValue = ProjectInfo._userId;

            var today = DateTime.Today;
            deReceiptDt.EditValue = today;

            leProductType.ItemIndex = 0;
            leModelId.EditValue = "-1";
            leRequestTypeDetail.EditValue = "-1";
            leRequestType.EditValue = "-1";


        }

        private void insertarcode()
        {
            JObject jobj = new JObject();

            //string export = teExport.Text.ToUpper().Trim();
            string barcode = teBarcode.Text.ToUpper().Trim();

            if (barcode.Length > 1)
            {
                if (barcode.Length != 12 || (!regex1.IsMatch(barcode) && !regex2.IsMatch(barcode) && !regex3.IsMatch(barcode)))
                {
                    Dangol.Message("Invalid inventory number.");
                    return;
                }
                else
                {
                    jobj.Add("BARCODE", barcode);
                }
            }

            //if (export.Length > 1)
            //{
            //    if (export.Length != 10 || !regex4.IsMatch(export))
            //    {
            //        Dangol.Message("Invalid export number.");
            //        return;
            //    }
            //    else
            //    {
            //        jobj.Add("EXPORT", export);
            //    }
            //}

            if (Dangol.MessageYN("Create a new receipt?") == DialogResult.Yes)
            {
                JObject jResult = new JObject();
                checkInfo(ref jobj);

                if (DBRelease.createTechnicalReceipt(jobj, ref jResult))
                {
                    _newReceiptId = ConvertUtil.ToInt64(jResult["RECEIPT_ID"]);
                    Dangol.Message(ConvertUtil.ToString("Execution completed."));
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                }
            }           
        }

        private void checkInfo(ref JObject jData)
        {            
            DateTime dt = Convert.ToDateTime(deReceiptDt.EditValue);
            jData.Add("RECEIPT_DT", dt.ToString("yyyy-MM-dd"));

            jData.Add("COMPANY_ID", ConvertUtil.ToInt64(leCompanyId1.EditValue));
            jData.Add("RECEIPT_USER_ID", ConvertUtil.ToString(leReceiptUserId.EditValue));
            jData.Add("PRODUCT_TYPE", ConvertUtil.ToString(leProductType.EditValue));
            jData.Add("MODEL_ID", ConvertUtil.ToInt64(leModelId.EditValue));
            jData.Add("REQUEST_TYPE", ConvertUtil.ToString(leRequestType.EditValue));
            jData.Add("REQUEST_TYPE_DETAIL", ConvertUtil.ToString(leRequestTypeDetail.EditValue));
            jData.Add("DES", ConvertUtil.ToString(meDes.Text));
            jData.Add("PRODUCT_YN", 0);
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            insertarcode();
        }

        private void sbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}