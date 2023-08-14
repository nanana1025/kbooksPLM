namespace WareHousingMaster.view.kbooks.user
{
    partial class dlgUserPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgUserPassword));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.teUserId = new DevExpress.XtraEditors.TextEdit();
            this.tePassword = new DevExpress.XtraEditors.TextEdit();
            this.teNewPassword = new DevExpress.XtraEditors.TextEdit();
            this.teNewPasswordConfirm = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNewPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNewPasswordConfirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Controls.Add(this.teUserId);
            this.layoutControl1.Controls.Add(this.tePassword);
            this.layoutControl1.Controls.Add(this.teNewPassword);
            this.layoutControl1.Controls.Add(this.teNewPasswordConfirm);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(332, 137);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbSave
            // 
            this.sbSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSave.ImageOptions.Image")));
            this.sbSave.Location = new System.Drawing.Point(193, 3);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(66, 22);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "저장";
            this.sbSave.Click += new System.EventHandler(this.sbSave_Click);
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(263, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(66, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "닫기";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // teUserId
            // 
            this.teUserId.Location = new System.Drawing.Point(113, 29);
            this.teUserId.Name = "teUserId";
            this.teUserId.Properties.ReadOnly = true;
            this.teUserId.Size = new System.Drawing.Size(216, 20);
            this.teUserId.StyleController = this.layoutControl1;
            this.teUserId.TabIndex = 6;
            this.teUserId.Tag = "BARCODE";
            // 
            // tePassword
            // 
            this.tePassword.Location = new System.Drawing.Point(113, 53);
            this.tePassword.Name = "tePassword";
            this.tePassword.Properties.PasswordChar = '*';
            this.tePassword.Properties.UseSystemPasswordChar = true;
            this.tePassword.Size = new System.Drawing.Size(216, 20);
            this.tePassword.StyleController = this.layoutControl1;
            this.tePassword.TabIndex = 4;
            this.tePassword.Tag = "INVENTORY_CAT";
            // 
            // teNewPassword
            // 
            this.teNewPassword.EditValue = "";
            this.teNewPassword.Location = new System.Drawing.Point(113, 78);
            this.teNewPassword.Name = "teNewPassword";
            this.teNewPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.teNewPassword.Properties.Appearance.Options.UseForeColor = true;
            this.teNewPassword.Properties.DisplayFormat.FormatString = "n0";
            this.teNewPassword.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teNewPassword.Properties.PasswordChar = '*';
            this.teNewPassword.Properties.UseSystemPasswordChar = true;
            this.teNewPassword.Size = new System.Drawing.Size(216, 20);
            this.teNewPassword.StyleController = this.layoutControl1;
            this.teNewPassword.TabIndex = 6;
            this.teNewPassword.Tag = "INIT_PRICE";
            // 
            // teNewPasswordConfirm
            // 
            this.teNewPasswordConfirm.EditValue = "";
            this.teNewPasswordConfirm.Location = new System.Drawing.Point(113, 103);
            this.teNewPasswordConfirm.Name = "teNewPasswordConfirm";
            this.teNewPasswordConfirm.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.teNewPasswordConfirm.Properties.Appearance.Options.UseForeColor = true;
            this.teNewPasswordConfirm.Properties.DisplayFormat.FormatString = "n0";
            this.teNewPasswordConfirm.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teNewPasswordConfirm.Properties.PasswordChar = '*';
            this.teNewPasswordConfirm.Properties.UseSystemPasswordChar = true;
            this.teNewPasswordConfirm.Size = new System.Drawing.Size(216, 20);
            this.teNewPasswordConfirm.StyleController = this.layoutControl1;
            this.teNewPasswordConfirm.TabIndex = 6;
            this.teNewPasswordConfirm.Tag = "ADJUST_PRICE";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem28,
            this.layoutControlItem20,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.emptySpaceItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(332, 137);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbSave;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem1.Location = new System.Drawing.Point(190, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem14";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(190, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbClose;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(260, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem14";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.teUserId;
            this.layoutControlItem28.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem28.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem28.MaxSize = new System.Drawing.Size(0, 25);
            this.layoutControlItem28.MinSize = new System.Drawing.Size(80, 24);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem28.Text = "유저 ID";
            this.layoutControlItem28.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem28.TextSize = new System.Drawing.Size(100, 14);
            this.layoutControlItem28.TextToControlDistance = 10;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.tePassword;
            this.layoutControlItem20.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem20.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem20.MaxSize = new System.Drawing.Size(0, 25);
            this.layoutControlItem20.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(330, 25);
            this.layoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem20.Text = "현재 비밀번호";
            this.layoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem20.TextSize = new System.Drawing.Size(100, 14);
            this.layoutControlItem20.TextToControlDistance = 10;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teNewPassword;
            this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem8.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(330, 25);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "변경할 비밀번호";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(100, 14);
            this.layoutControlItem8.TextToControlDistance = 10;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teNewPasswordConfirm;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem9.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 100);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(330, 25);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "비밀번호 확인";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(100, 14);
            this.layoutControlItem9.TextToControlDistance = 10;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 125);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(330, 10);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dlgUserPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 137);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgUserPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "비밀번호 수정";
            this.Load += new System.EventHandler(this.dlgUserPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNewPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNewPasswordConfirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit teUserId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraEditors.TextEdit tePassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraEditors.TextEdit teNewPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.TextEdit teNewPasswordConfirm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}
