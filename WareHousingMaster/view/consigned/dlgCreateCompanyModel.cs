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

namespace WareHousingMaster.view.consigned
{
    public partial class dlgCreateCompanyModel : DevExpress.XtraEditors.XtraForm
    {
        long _companyId;
        long _modelListId;
        string _modelNm;
        int _cpuAssignYn;
        string _delYn;
        int _type;

        public dlgCreateCompanyModel(long companyId, long modelListId, string modelNm, int cpuAssignYn, string delYn, int type)
        {
            InitializeComponent();

            _companyId = companyId;
            _modelListId = modelListId;
            _type = type;
            _modelNm = modelNm;
            _cpuAssignYn = cpuAssignYn;
            _delYn = delYn;

            if (_type == 1)
            {
                lcUpdate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcDelYn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                lcCreate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            teModelNm.Text = _modelNm;
        }
        private void dlgCreateADP_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            setInfoBox();
        }

        private void setInfoBox()
        {
            DataTable dtConsignedType = new DataTable();

            dtConsignedType.Columns.Add(new DataColumn("KEY", typeof(int)));
            dtConsignedType.Columns.Add(new DataColumn("VALUE", typeof(string)));
       
            Util.insertRowonTop(dtConsignedType, 0, "미적용");
            Util.insertRowonTop(dtConsignedType, 1, "적용");
            Util.LookupEditHelper(leCPUAssignCheck, dtConsignedType, "KEY", "VALUE");

            DataTable dtDelYn = new DataTable();

            dtDelYn.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtDelYn.Columns.Add(new DataColumn("VALUE", typeof(string)));

            Util.insertRowonTop(dtDelYn, "Y", "미사용");
            Util.insertRowonTop(dtDelYn, "N", "사용");   
            Util.LookupEditHelper(leDelYn, dtDelYn, "KEY", "VALUE");

            leCPUAssignCheck.EditValue = _cpuAssignYn;
            leDelYn.EditValue = _delYn;
        }

        private void sbSave_Click(object sender, EventArgs e)
        { 
            JObject jResult = new JObject();
            JObject job= new JObject();

            job.Add("COMPANY_ID", _companyId);
            job.Add("MODEL_NM", teModelNm.Text);
            job.Add("CPU_ASSIGN_YN", ConvertUtil.ToInt32(leCPUAssignCheck.EditValue));
            job.Add("DEL_YN", "N");

            if (DBConsigned.createCompanyModelInfo(job, ref jResult))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }

            //this.DialogResult = DialogResult.OK;
        }

        private void sbUpdate_Click(object sender, EventArgs e)
        {
            JObject jResult = new JObject();
            JObject job = new JObject();

            job.Add("MODEL_LIST_ID", _modelListId);
            job.Add("MODEL_NM", teModelNm.Text);
            job.Add("CPU_ASSIGN_YN", ConvertUtil.ToInt32(leCPUAssignCheck.EditValue));
            job.Add("DEL_YN", ConvertUtil.ToString(leDelYn.EditValue));

            if (DBConsigned.updateCompanyModelInfo(job, ref jResult))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Dangol.Message(jResult["MSG"]);
                return;
            }
        }

        private void sbClose_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}