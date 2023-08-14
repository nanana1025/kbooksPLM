namespace WareHousingMaster.view.PreView
{
    partial class DlgSoundTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgSoundTest));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tbarVolume = new DevExpress.XtraEditors.TrackBarControl();
            this.sbPause = new DevExpress.XtraEditors.SimpleButton();
            this.sbRight = new DevExpress.XtraEditors.SimpleButton();
            this.sbLeft = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarVolume.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tbarVolume);
            this.layoutControl1.Controls.Add(this.sbPause);
            this.layoutControl1.Controls.Add(this.sbRight);
            this.layoutControl1.Controls.Add(this.sbLeft);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(280, 160);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tbarVolume
            // 
            this.tbarVolume.EditValue = null;
            this.tbarVolume.Location = new System.Drawing.Point(28, 131);
            this.tbarVolume.Name = "tbarVolume";
            this.tbarVolume.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbarVolume.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tbarVolume.Properties.Maximum = 100;
            this.tbarVolume.Size = new System.Drawing.Size(249, 45);
            this.tbarVolume.StyleController = this.layoutControl1;
            this.tbarVolume.TabIndex = 7;
            this.tbarVolume.EditValueChanged += new System.EventHandler(this.tbarVolume_EditValueChanged);
            // 
            // sbPause
            // 
            this.sbPause.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbPause.Appearance.Options.UseFont = true;
            this.sbPause.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbPause.ImageOptions.Image")));
            this.sbPause.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.sbPause.Location = new System.Drawing.Point(102, 3);
            this.sbPause.Name = "sbPause";
            this.sbPause.Size = new System.Drawing.Size(82, 124);
            this.sbPause.StyleController = this.layoutControl1;
            this.sbPause.TabIndex = 6;
            this.sbPause.Text = "정지";
            this.sbPause.Click += new System.EventHandler(this.sbPause_Click);
            // 
            // sbRight
            // 
            this.sbRight.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbRight.Appearance.Options.UseFont = true;
            this.sbRight.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.sbRight.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sbRight.ImageOptions.SvgImage")));
            this.sbRight.Location = new System.Drawing.Point(188, 3);
            this.sbRight.Name = "sbRight";
            this.sbRight.Size = new System.Drawing.Size(89, 124);
            this.sbRight.StyleController = this.layoutControl1;
            this.sbRight.TabIndex = 5;
            this.sbRight.Text = "오른쪽";
            this.sbRight.Click += new System.EventHandler(this.sbRight_Click);
            // 
            // sbLeft
            // 
            this.sbLeft.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbLeft.Appearance.Options.UseFont = true;
            this.sbLeft.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.sbLeft.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sbLeft.ImageOptions.SvgImage")));
            this.sbLeft.Location = new System.Drawing.Point(3, 3);
            this.sbLeft.Name = "sbLeft";
            this.sbLeft.Size = new System.Drawing.Size(95, 124);
            this.sbLeft.StyleController = this.layoutControl1;
            this.sbLeft.TabIndex = 4;
            this.sbLeft.Text = "왼쪽";
            this.sbLeft.Click += new System.EventHandler(this.sbLeft_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(280, 160);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbLeft;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(89, 1);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(99, 128);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbRight;
            this.layoutControlItem2.Location = new System.Drawing.Point(185, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(89, 1);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(93, 128);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbPause;
            this.layoutControlItem3.Location = new System.Drawing.Point(99, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(30, 1);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(86, 128);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tbarVolume;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 128);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(73, 30);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(278, 30);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Vol.";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(21, 14);
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // DlgSoundTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(280, 160);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgSoundTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "사운드 테스트";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgSoundTest_FormClosing);
            this.Load += new System.EventHandler(this.DlgCameraTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbarVolume.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sbRight;
        private DevExpress.XtraEditors.SimpleButton sbLeft;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbPause;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TrackBarControl tbarVolume;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}