namespace WareHousingMaster.view.consigned
{
    partial class dlgCreateCompanyModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgCreateCompanyModel));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.leCPUAssignCheck = new DevExpress.XtraEditors.LookUpEdit();
            this.leDelYn = new DevExpress.XtraEditors.LookUpEdit();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.teModelNm = new DevExpress.XtraEditors.TextEdit();
            this.sbUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCreate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDelYn = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcUpdate = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leCPUAssignCheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leDelYn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teModelNm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.separatorControl1);
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Controls.Add(this.leCPUAssignCheck);
            this.layoutControl1.Controls.Add(this.leDelYn);
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Controls.Add(this.teModelNm);
            this.layoutControl1.Controls.Add(this.sbUpdate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(382, 125);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(2, 28);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(378, 22);
            this.separatorControl1.TabIndex = 10;
            // 
            // sbSave
            // 
            this.sbSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSave.ImageOptions.Image")));
            this.sbSave.Location = new System.Drawing.Point(83, 3);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(96, 22);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "추가";
            this.sbSave.Click += new System.EventHandler(this.sbSave_Click);
            // 
            // leCPUAssignCheck
            // 
            this.leCPUAssignCheck.Location = new System.Drawing.Point(108, 77);
            this.leCPUAssignCheck.Name = "leCPUAssignCheck";
            this.leCPUAssignCheck.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leCPUAssignCheck.Properties.DropDownRows = 2;
            this.leCPUAssignCheck.Properties.NullText = "CPU 할당 체크를 선택하세요";
            this.leCPUAssignCheck.Size = new System.Drawing.Size(271, 20);
            this.leCPUAssignCheck.StyleController = this.layoutControl1;
            this.leCPUAssignCheck.TabIndex = 9;
            // 
            // leDelYn
            // 
            this.leDelYn.Location = new System.Drawing.Point(108, 101);
            this.leDelYn.Name = "leDelYn";
            this.leDelYn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leDelYn.Properties.DropDownRows = 2;
            this.leDelYn.Properties.NullText = "사용여부를 선택하세요.";
            this.leDelYn.Size = new System.Drawing.Size(271, 20);
            this.leDelYn.StyleController = this.layoutControl1;
            this.leDelYn.TabIndex = 9;
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(283, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(96, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "닫기";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click_1);
            // 
            // teModelNm
            // 
            this.teModelNm.Location = new System.Drawing.Point(108, 53);
            this.teModelNm.Name = "teModelNm";
            this.teModelNm.Size = new System.Drawing.Size(271, 20);
            this.teModelNm.StyleController = this.layoutControl1;
            this.teModelNm.TabIndex = 9;
            // 
            // sbUpdate
            // 
            this.sbUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbUpdate.ImageOptions.Image")));
            this.sbUpdate.Location = new System.Drawing.Point(183, 3);
            this.sbUpdate.Name = "sbUpdate";
            this.sbUpdate.Size = new System.Drawing.Size(96, 22);
            this.sbUpdate.StyleController = this.layoutControl1;
            this.sbUpdate.TabIndex = 6;
            this.sbUpdate.Text = "수정";
            this.sbUpdate.Click += new System.EventHandler(this.sbUpdate_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCreate,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.lcDelYn,
            this.layoutControlItem16,
            this.layoutControlItem13,
            this.emptySpaceItem1,
            this.lcUpdate});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(382, 125);
            this.Root.TextVisible = false;
            // 
            // lcCreate
            // 
            this.lcCreate.Control = this.sbSave;
            this.lcCreate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcCreate.CustomizationFormText = "layoutControlItem14";
            this.lcCreate.Location = new System.Drawing.Point(80, 0);
            this.lcCreate.MaxSize = new System.Drawing.Size(100, 26);
            this.lcCreate.MinSize = new System.Drawing.Size(100, 26);
            this.lcCreate.Name = "lcCreate";
            this.lcCreate.Size = new System.Drawing.Size(100, 26);
            this.lcCreate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcCreate.Text = "layoutControlItem14";
            this.lcCreate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcCreate.TextSize = new System.Drawing.Size(0, 0);
            this.lcCreate.TextToControlDistance = 0;
            this.lcCreate.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teModelNm;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "품목명";
            this.layoutControlItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem3.ImageOptions.Image")));
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem3.Text = "모델명";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(100, 16);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.leCPUAssignCheck;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "품목명";
            this.layoutControlItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem4.ImageOptions.Image")));
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem4.Text = "CPU 할당 체크";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(100, 16);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // lcDelYn
            // 
            this.lcDelYn.Control = this.leDelYn;
            this.lcDelYn.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcDelYn.CustomizationFormText = "품목명";
            this.lcDelYn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcDelYn.ImageOptions.Image")));
            this.lcDelYn.Location = new System.Drawing.Point(0, 98);
            this.lcDelYn.Name = "lcDelYn";
            this.lcDelYn.Size = new System.Drawing.Size(380, 25);
            this.lcDelYn.Text = "사용여부";
            this.lcDelYn.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcDelYn.TextSize = new System.Drawing.Size(100, 16);
            this.lcDelYn.TextToControlDistance = 5;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.separatorControl1;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem16.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.sbClose;
            this.layoutControlItem13.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem13.Location = new System.Drawing.Point(280, 0);
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
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(80, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcUpdate
            // 
            this.lcUpdate.Control = this.sbUpdate;
            this.lcUpdate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcUpdate.CustomizationFormText = "layoutControlItem14";
            this.lcUpdate.Location = new System.Drawing.Point(180, 0);
            this.lcUpdate.MaxSize = new System.Drawing.Size(100, 26);
            this.lcUpdate.MinSize = new System.Drawing.Size(100, 26);
            this.lcUpdate.Name = "lcUpdate";
            this.lcUpdate.Size = new System.Drawing.Size(100, 26);
            this.lcUpdate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcUpdate.Text = "layoutControlItem14";
            this.lcUpdate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcUpdate.TextSize = new System.Drawing.Size(0, 0);
            this.lcUpdate.TextToControlDistance = 0;
            this.lcUpdate.TextVisible = false;
            // 
            // dlgCreateCompanyModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 125);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgCreateCompanyModel";
            this.Text = "모델정보";
            this.Load += new System.EventHandler(this.dlgCreateADP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leCPUAssignCheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leDelYn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teModelNm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpdate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraLayout.LayoutControlItem lcCreate;
        private DevExpress.XtraEditors.LookUpEdit leCPUAssignCheck;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.TextEdit teModelNm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraEditors.LookUpEdit leDelYn;
        private DevExpress.XtraLayout.LayoutControlItem lcDelYn;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbUpdate;
        private DevExpress.XtraLayout.LayoutControlItem lcUpdate;
    }
}