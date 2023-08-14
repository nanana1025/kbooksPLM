namespace WareHousingMaster.view.usedPurchase
{
    partial class dlgGetCustomerCheckDes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgGetCustomerCheckDes));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.leComponentCd = new DevExpress.XtraEditors.LookUpEdit();
            this.meRequest = new DevExpress.XtraEditors.MemoEdit();
            this.teModelNm = new DevExpress.XtraEditors.TextEdit();
            this.sbInsert = new DevExpress.XtraEditors.SimpleButton();
            this.sbAdd = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meRequest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teModelNm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.leComponentCd);
            this.layoutControl1.Controls.Add(this.meRequest);
            this.layoutControl1.Controls.Add(this.teModelNm);
            this.layoutControl1.Controls.Add(this.sbInsert);
            this.layoutControl1.Controls.Add(this.sbAdd);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(718, 202);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // leComponentCd
            // 
            this.leComponentCd.Location = new System.Drawing.Point(88, 29);
            this.leComponentCd.Name = "leComponentCd";
            this.leComponentCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leComponentCd.Size = new System.Drawing.Size(125, 20);
            this.leComponentCd.StyleController = this.layoutControl1;
            this.leComponentCd.TabIndex = 9;
            // 
            // meRequest
            // 
            this.meRequest.Location = new System.Drawing.Point(88, 55);
            this.meRequest.Name = "meRequest";
            this.meRequest.Size = new System.Drawing.Size(627, 144);
            this.meRequest.StyleController = this.layoutControl1;
            this.meRequest.TabIndex = 8;
            // 
            // teModelNm
            // 
            this.teModelNm.Location = new System.Drawing.Point(302, 29);
            this.teModelNm.Name = "teModelNm";
            this.teModelNm.Size = new System.Drawing.Size(313, 20);
            this.teModelNm.StyleController = this.layoutControl1;
            this.teModelNm.TabIndex = 7;
            // 
            // sbInsert
            // 
            this.sbInsert.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbInsert.ImageOptions.Image")));
            this.sbInsert.Location = new System.Drawing.Point(619, 3);
            this.sbInsert.Name = "sbInsert";
            this.sbInsert.Size = new System.Drawing.Size(96, 22);
            this.sbInsert.StyleController = this.layoutControl1;
            this.sbInsert.TabIndex = 6;
            this.sbInsert.Text = "확인";
            this.sbInsert.Click += new System.EventHandler(this.sbInsert_Click);
            // 
            // sbAdd
            // 
            this.sbAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAdd.ImageOptions.Image")));
            this.sbAdd.Location = new System.Drawing.Point(619, 29);
            this.sbAdd.Name = "sbAdd";
            this.sbAdd.Size = new System.Drawing.Size(96, 22);
            this.sbAdd.StyleController = this.layoutControl1;
            this.sbAdd.TabIndex = 6;
            this.sbAdd.Text = "추가";
            this.sbAdd.Click += new System.EventHandler(this.sbAdd_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(718, 202);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(616, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbInsert;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(616, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem14";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teModelNm;
            this.layoutControlItem1.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem1.Location = new System.Drawing.Point(214, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(402, 26);
            this.layoutControlItem1.Text = "모델명";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.meRequest;
            this.layoutControlItem3.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(716, 148);
            this.layoutControlItem3.Text = "참고정보";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.leComponentCd;
            this.layoutControlItem4.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(214, 26);
            this.layoutControlItem4.Text = "품목명";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbAdd;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem5.Location = new System.Drawing.Point(616, 26);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem14";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // dlgGetCustomerCheckDes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 202);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgGetCustomerCheckDes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "반송내용";
            this.Load += new System.EventHandler(this.dlgGetPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meRequest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teModelNm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbInsert;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.MemoEdit meRequest;
        private DevExpress.XtraEditors.TextEdit teModelNm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LookUpEdit leComponentCd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton sbAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}