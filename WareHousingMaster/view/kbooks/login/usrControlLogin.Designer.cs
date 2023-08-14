namespace WareHousingMaster.view.login
{
    partial class usrControlLogin
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceAutoLogin = new DevExpress.XtraEditors.CheckEdit();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOK = new DevExpress.XtraEditors.SimpleButton();
            this.tePasswd = new DevExpress.XtraEditors.TextEdit();
            this.teId = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.아이디 = new DevExpress.XtraLayout.LayoutControlItem();
            this.비밀번호 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceAutoLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePasswd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.아이디)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.비밀번호)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceAutoLogin);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Controls.Add(this.sbOK);
            this.layoutControl1.Controls.Add(this.tePasswd);
            this.layoutControl1.Controls.Add(this.teId);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(350, 104);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceAutoLogin
            // 
            this.ceAutoLogin.Location = new System.Drawing.Point(260, 50);
            this.ceAutoLogin.Name = "ceAutoLogin";
            this.ceAutoLogin.Properties.Caption = "자동로그인";
            this.ceAutoLogin.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ceAutoLogin.Size = new System.Drawing.Size(88, 19);
            this.ceAutoLogin.StyleController = this.layoutControl1;
            this.ceAutoLogin.TabIndex = 8;
            // 
            // sbCancel
            // 
            this.sbCancel.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.cancel_16x161;
            this.sbCancel.Location = new System.Drawing.Point(252, 73);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(96, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 7;
            this.sbCancel.Text = "취소";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // sbOK
            // 
            this.sbOK.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.apply_16x164;
            this.sbOK.Location = new System.Drawing.Point(152, 73);
            this.sbOK.Name = "sbOK";
            this.sbOK.Size = new System.Drawing.Size(96, 22);
            this.sbOK.StyleController = this.layoutControl1;
            this.sbOK.TabIndex = 6;
            this.sbOK.Text = "로그인";
            this.sbOK.Click += new System.EventHandler(this.sbOK_Click);
            // 
            // tePasswd
            // 
            this.tePasswd.Location = new System.Drawing.Point(92, 26);
            this.tePasswd.Name = "tePasswd";
            this.tePasswd.Properties.PasswordChar = '*';
            this.tePasswd.Size = new System.Drawing.Size(256, 20);
            this.tePasswd.StyleController = this.layoutControl1;
            this.tePasswd.TabIndex = 5;
            this.tePasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tePasswd_KeyDown);
            // 
            // teId
            // 
            this.teId.Location = new System.Drawing.Point(92, 2);
            this.teId.Name = "teId";
            this.teId.Size = new System.Drawing.Size(256, 20);
            this.teId.StyleController = this.layoutControl1;
            this.teId.TabIndex = 4;
            this.teId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teId_KeyDown);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.아이디,
            this.비밀번호,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(350, 104);
            this.Root.TextVisible = false;
            // 
            // 아이디
            // 
            this.아이디.Control = this.teId;
            this.아이디.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.pagenext_16x16;
            this.아이디.Location = new System.Drawing.Point(0, 0);
            this.아이디.Name = "아이디";
            this.아이디.Size = new System.Drawing.Size(350, 24);
            this.아이디.Text = "Id";
            this.아이디.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.아이디.TextSize = new System.Drawing.Size(80, 16);
            this.아이디.TextToControlDistance = 10;
            // 
            // 비밀번호
            // 
            this.비밀번호.Control = this.tePasswd;
            this.비밀번호.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.pagenext_16x161;
            this.비밀번호.Location = new System.Drawing.Point(0, 24);
            this.비밀번호.Name = "비밀번호";
            this.비밀번호.Size = new System.Drawing.Size(350, 24);
            this.비밀번호.Text = "Password";
            this.비밀번호.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.비밀번호.TextSize = new System.Drawing.Size(80, 16);
            this.비밀번호.TextToControlDistance = 10;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbOK;
            this.layoutControlItem3.Location = new System.Drawing.Point(150, 71);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 33);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(250, 71);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 33);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 71);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(150, 33);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(258, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ceAutoLogin;
            this.layoutControlItem5.Location = new System.Drawing.Point(258, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(92, 23);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // usrControlLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 104);
            this.Controls.Add(this.layoutControl1);
            this.Name = "usrControlLogin";
            this.Text = "로그인";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usrControlLogin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceAutoLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePasswd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.아이디)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.비밀번호)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit ceAutoLogin;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOK;
        private DevExpress.XtraEditors.TextEdit tePasswd;
        private DevExpress.XtraEditors.TextEdit teId;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem 아이디;
        private DevExpress.XtraLayout.LayoutControlItem 비밀번호;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}