namespace WareHousingMaster.view.produce
{
    partial class DlgCreateProduce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgCreateProduce));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.leProduceState = new DevExpress.XtraEditors.LookUpEdit();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.sbProduceAdd = new DevExpress.XtraEditors.SimpleButton();
            this.tePartCode = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCompanyId = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProduceAdd = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcWarehouse = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPallet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.leProduceType = new DevExpress.XtraEditors.LookUpEdit();
            this.leCompanyId = new DevExpress.XtraEditors.LookUpEdit();
            this.meDes = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leProduceState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePartCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompanyId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProduceAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leProduceType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leCompanyId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meDes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.separatorControl1);
            this.layoutControl1.Controls.Add(this.leProduceState);
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Controls.Add(this.sbProduceAdd);
            this.layoutControl1.Controls.Add(this.tePartCode);
            this.layoutControl1.Controls.Add(this.leProduceType);
            this.layoutControl1.Controls.Add(this.leCompanyId);
            this.layoutControl1.Controls.Add(this.meDes);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(368, 220);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(2, 28);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(364, 22);
            this.separatorControl1.TabIndex = 10;
            // 
            // leProduceState
            // 
            this.leProduceState.Location = new System.Drawing.Point(108, 77);
            this.leProduceState.Name = "leProduceState";
            this.leProduceState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leProduceState.Properties.NullText = "생산상태를 선택하세요.";
            this.leProduceState.Size = new System.Drawing.Size(257, 20);
            this.leProduceState.StyleController = this.layoutControl1;
            this.leProduceState.TabIndex = 9;
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(269, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(96, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "닫기";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click_1);
            // 
            // sbProduceAdd
            // 
            this.sbProduceAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbPartAdd.ImageOptions.Image")));
            this.sbProduceAdd.Location = new System.Drawing.Point(169, 3);
            this.sbProduceAdd.Name = "sbProduceAdd";
            this.sbProduceAdd.Size = new System.Drawing.Size(96, 22);
            this.sbProduceAdd.StyleController = this.layoutControl1;
            this.sbProduceAdd.TabIndex = 6;
            this.sbProduceAdd.Text = "추가";
            this.sbProduceAdd.Click += new System.EventHandler(this.sbPartAdd_Click);
            // 
            // tePartCode
            // 
            this.tePartCode.EditValue = "자동생성";
            this.tePartCode.Location = new System.Drawing.Point(108, 53);
            this.tePartCode.Name = "tePartCode";
            this.tePartCode.Properties.NullText = "적재위치를 선택하세요.";
            this.tePartCode.Properties.ReadOnly = true;
            this.tePartCode.Size = new System.Drawing.Size(257, 20);
            this.tePartCode.StyleController = this.layoutControl1;
            this.tePartCode.TabIndex = 9;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lcCompanyId,
            this.emptySpaceItem2,
            this.layoutControlItem16,
            this.lcProduceAdd,
            this.emptySpaceItem1,
            this.lcWarehouse,
            this.lcPallet,
            this.layoutControlItem13,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(368, 220);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.leProduceType;
            this.layoutControlItem1.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 98);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(366, 24);
            this.layoutControlItem1.Text = "생산유형";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(100, 16);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // lcCompanyId
            // 
            this.lcCompanyId.Control = this.leCompanyId;
            this.lcCompanyId.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcCompanyId.CustomizationFormText = "품목명";
            this.lcCompanyId.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem3.ImageOptions.Image")));
            this.lcCompanyId.Location = new System.Drawing.Point(0, 122);
            this.lcCompanyId.Name = "lcCompanyId";
            this.lcCompanyId.Size = new System.Drawing.Size(366, 24);
            this.lcCompanyId.Text = "거래업체";
            this.lcCompanyId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcCompanyId.TextSize = new System.Drawing.Size(100, 16);
            this.lcCompanyId.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 208);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(366, 10);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.separatorControl1;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem16.Size = new System.Drawing.Size(366, 24);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextVisible = false;
            // 
            // lcProduceAdd
            // 
            this.lcProduceAdd.Control = this.sbProduceAdd;
            this.lcProduceAdd.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcProduceAdd.CustomizationFormText = "layoutControlItem14";
            this.lcProduceAdd.Location = new System.Drawing.Point(166, 0);
            this.lcProduceAdd.MaxSize = new System.Drawing.Size(100, 26);
            this.lcProduceAdd.MinSize = new System.Drawing.Size(100, 26);
            this.lcProduceAdd.Name = "lcProduceAdd";
            this.lcProduceAdd.Size = new System.Drawing.Size(100, 26);
            this.lcProduceAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcProduceAdd.Text = "layoutControlItem14";
            this.lcProduceAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcProduceAdd.TextSize = new System.Drawing.Size(0, 0);
            this.lcProduceAdd.TextToControlDistance = 0;
            this.lcProduceAdd.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(166, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcWarehouse
            // 
            this.lcWarehouse.Control = this.leProduceState;
            this.lcWarehouse.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcWarehouse.CustomizationFormText = "품목명";
            this.lcWarehouse.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcWarehouse.ImageOptions.Image")));
            this.lcWarehouse.Location = new System.Drawing.Point(0, 74);
            this.lcWarehouse.Name = "lcWarehouse";
            this.lcWarehouse.Size = new System.Drawing.Size(366, 24);
            this.lcWarehouse.Text = "생산상태";
            this.lcWarehouse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcWarehouse.TextSize = new System.Drawing.Size(100, 16);
            this.lcWarehouse.TextToControlDistance = 5;
            // 
            // lcPallet
            // 
            this.lcPallet.Control = this.tePartCode;
            this.lcPallet.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcPallet.CustomizationFormText = "품목명";
            this.lcPallet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcPallet.ImageOptions.Image")));
            this.lcPallet.Location = new System.Drawing.Point(0, 50);
            this.lcPallet.Name = "lcPallet";
            this.lcPallet.Size = new System.Drawing.Size(366, 24);
            this.lcPallet.Text = "생산번호";
            this.lcPallet.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcPallet.TextSize = new System.Drawing.Size(100, 16);
            this.lcPallet.TextToControlDistance = 5;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.sbClose;
            this.layoutControlItem13.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem13.Location = new System.Drawing.Point(266, 0);
            this.layoutControlItem13.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.Text = "layoutControlItem14";
            this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.meDes;
            this.layoutControlItem2.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 146);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(366, 62);
            this.layoutControlItem2.Text = "설명";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // leProduceType
            // 
            this.leProduceType.Location = new System.Drawing.Point(108, 101);
            this.leProduceType.Name = "leProduceType";
            this.leProduceType.Properties.Appearance.Options.UseTextOptions = true;
            this.leProduceType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.leProduceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leProduceType.Properties.NullText = "";
            this.leProduceType.Size = new System.Drawing.Size(257, 20);
            this.leProduceType.StyleController = this.layoutControl1;
            this.leProduceType.TabIndex = 9;
            // 
            // leCompanyId
            // 
            this.leCompanyId.Location = new System.Drawing.Point(108, 125);
            this.leCompanyId.Name = "leCompanyId";
            this.leCompanyId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leCompanyId.Properties.NullText = "";
            this.leCompanyId.Size = new System.Drawing.Size(257, 20);
            this.leCompanyId.StyleController = this.layoutControl1;
            this.leCompanyId.TabIndex = 9;
            // 
            // meDes
            // 
            this.meDes.Location = new System.Drawing.Point(108, 149);
            this.meDes.Name = "meDes";
            this.meDes.Size = new System.Drawing.Size(257, 58);
            this.meDes.StyleController = this.layoutControl1;
            this.meDes.TabIndex = 11;
            // 
            // DlgCreateProduce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 220);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgCreateProduce";
            this.Text = "생산번호 추가";
            this.Load += new System.EventHandler(this.dlgCreateADP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leProduceState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePartCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompanyId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProduceAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leProduceType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leCompanyId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meDes.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit leProduceState;
        private DevExpress.XtraLayout.LayoutControlItem lcCompanyId;
        private DevExpress.XtraLayout.LayoutControlItem lcWarehouse;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem lcPallet;
        private DevExpress.XtraEditors.SimpleButton sbProduceAdd;
        private DevExpress.XtraLayout.LayoutControlItem lcProduceAdd;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit tePartCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit leProduceType;
        private DevExpress.XtraEditors.LookUpEdit leCompanyId;
        private DevExpress.XtraEditors.MemoEdit meDes;
    }
}