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

namespace WareHousingMaster.view.usedPurchase
{
    public partial class dlgUpdatePartCnt : DevExpress.XtraEditors.XtraForm
    {

        long _usedPartId = -1;
        string _receipt = null;
        public int Cnt { get; private set; }

        string _partCode;

        public dlgUpdatePartCnt(object usedPartId, string receipt, object partCode, object cnt)
        {
            InitializeComponent();

            _usedPartId = ConvertUtil.ToInt32(usedPartId);
            _receipt = receipt;
            _partCode = ConvertUtil.ToString(partCode);
            Cnt = ConvertUtil.ToInt32(cnt);
        }
        private void dlgNewPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            seCnt.EditValue = Cnt;

            if (_usedPartId < 0)
            {
                Dangol.Message("부품번호가 선택되지 않았습니다.");
                this.DialogResult = DialogResult.Cancel;
            }

            if (string.IsNullOrEmpty(_receipt))
            {
                Dangol.Message("접수번호가 올바르지 않습니다.");
                this.DialogResult = DialogResult.Cancel;
            }
        }
        private void sbUpdate_Click(object sender, EventArgs e)
        {
            int cnt = ConvertUtil.ToInt32(seCnt.EditValue);
            if (cnt < 0)
            {
                Dangol.Message("양수만 입력 가능합니다.");
                return;
            }

            if (Dangol.MessageYN("부품의 수량을 변경하시겠습니까?") == DialogResult.Yes)
            {
               

                JObject jResult = new JObject();

                //if (DBUsedPurchase.updateUsedPartCnt(_usedPartId, _receipt, _partCode, Cnt, cnt, ref jResult))
                {
                    Cnt = cnt;
                    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                    this.DialogResult = DialogResult.OK;
                }
                //else
                //{
                //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
                //    return;
                //}
            }  
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}