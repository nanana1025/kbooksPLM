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
    public partial class dlgGetCustomerCheckDes : DevExpress.XtraEditors.XtraForm
    {
        long _receiptId;

        public dlgGetCustomerCheckDes(long receiptId)
        {
            InitializeComponent();
            _receiptId = receiptId;
        }
        private void dlgGetPart_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;

            getData();
        }

        private void getData()
        {
            JObject jResult = new JObject();


            //if (DBUsedPurchase.getLTComonent(_componentCd, ref jResult))
            //{
            //    if (Convert.ToBoolean(jResult["EXIST"]))
            //    {
            //        JArray jArray = JArray.Parse(jResult["DATA"].ToString());
            //        foreach (JObject obj in jArray.Children<JObject>())
            //        {
            //            string partCode = ConvertUtil.ToString(obj["PARTCODE"]);

            //            //if (!_listUsedPart.Contains(partCode))
            //            {
            //                DataRow dr = _dt.NewRow();
            //                dr["PARTCODE"] = partCode;
            //                dr["MODEL_NM"] = obj["MODEL_NM"];
            //                dr["CATEGORY"] = obj["CATEGORY"];
            //                dr["PRICE"] = obj["PRICE"];
            //                _dt.Rows.Add(dr);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //} 
        }


        private void sbInsert_Click(object sender, EventArgs e)
        {
            //JObject jResult = new JObject();

            //if (_currentRow == null)
            //{
            //    Dangol.Message("부품을 선택해주세요.");
            //    return;
            //}

            //_price = ConvertUtil.ToInt64(_currentRow["PRICE"]);
            this.DialogResult = DialogResult.OK;

            //if (_listUsedPart.Contains(partCode))
            //{
            //    Dangol.Message("이미 존재하는 부품입니다.");
            //    return;
            //}

            //if (DBUsedPurchase.insertUsedPartComponent(_receipt, _currentRow["PARTCODE"], ref jResult))
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //    this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    Dangol.Message(ConvertUtil.ToString(jResult["MSG"]));
            //} 
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            meRequest.Text = $"{ meRequest.Text}\n{leComponentCd.Text} {teModelNm}";
        }
    }
}