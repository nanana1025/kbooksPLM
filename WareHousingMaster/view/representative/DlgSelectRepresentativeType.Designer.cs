namespace WareHousingMaster.view.representative
{
    partial class DlgSelectRepresentativeType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgSelectRepresentativeType));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbReleaseSet = new DevExpress.XtraEditors.SimpleButton();
            this.sbWarehousingSet = new DevExpress.XtraEditors.SimpleButton();
            this.sbRelease = new DevExpress.XtraEditors.SimpleButton();
            this.sbConsigned = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbWarehousing = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbWarehousingConsigned = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbReleaseSet);
            this.layoutControl1.Controls.Add(this.sbWarehousingSet);
            this.layoutControl1.Controls.Add(this.sbRelease);
            this.layoutControl1.Controls.Add(this.sbConsigned);
            this.layoutControl1.Controls.Add(this.sbWarehousing);
            this.layoutControl1.Controls.Add(this.sbWarehousingConsigned);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(501, 172);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbReleaseSet
            // 
            this.sbReleaseSet.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.sbReleaseSet.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbReleaseSet.Appearance.Options.UseBackColor = true;
            this.sbReleaseSet.Appearance.Options.UseFont = true;
            this.sbReleaseSet.Location = new System.Drawing.Point(252, 12);
            this.sbReleaseSet.Name = "sbReleaseSet";
            this.sbReleaseSet.Size = new System.Drawing.Size(236, 46);
            this.sbReleaseSet.StyleController = this.layoutControl1;
            this.sbReleaseSet.TabIndex = 6;
            this.sbReleaseSet.Text = "출고";
            // 
            // sbWarehousingSet
            // 
            this.sbWarehousingSet.Appearance.BackColor = System.Drawing.Color.White;
            this.sbWarehousingSet.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbWarehousingSet.Appearance.Options.UseBackColor = true;
            this.sbWarehousingSet.Appearance.Options.UseFont = true;
            this.sbWarehousingSet.Location = new System.Drawing.Point(12, 12);
            this.sbWarehousingSet.Name = "sbWarehousingSet";
            this.sbWarehousingSet.Size = new System.Drawing.Size(236, 46);
            this.sbWarehousingSet.StyleController = this.layoutControl1;
            this.sbWarehousingSet.TabIndex = 5;
            this.sbWarehousingSet.Text = "입고";
            // 
            // sbRelease
            // 
            this.sbRelease.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.sbRelease.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbRelease.Appearance.Options.UseBackColor = true;
            this.sbRelease.Appearance.Options.UseFont = true;
            this.sbRelease.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbRelease.ImageOptions.Image")));
            this.sbRelease.Location = new System.Drawing.Point(252, 62);
            this.sbRelease.Name = "sbRelease";
            this.sbRelease.Size = new System.Drawing.Size(116, 96);
            this.sbRelease.StyleController = this.layoutControl1;
            this.sbRelease.TabIndex = 5;
            this.sbRelease.Text = "일반출고";
            this.sbRelease.Click += new System.EventHandler(this.sbRelease_Click);
            // 
            // sbConsigned
            // 
            this.sbConsigned.Appearance.BackColor = System.Drawing.Color.LightGreen;
            this.sbConsigned.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbConsigned.Appearance.Options.UseBackColor = true;
            this.sbConsigned.Appearance.Options.UseFont = true;
            this.sbConsigned.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbConsigned.ImageOptions.Image")));
            this.sbConsigned.Location = new System.Drawing.Point(372, 62);
            this.sbConsigned.Name = "sbConsigned";
            this.sbConsigned.Size = new System.Drawing.Size(116, 96);
            this.sbConsigned.StyleController = this.layoutControl1;
            this.sbConsigned.TabIndex = 5;
            this.sbConsigned.Text = "생산대행";
            this.sbConsigned.Click += new System.EventHandler(this.sbConsigned_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem27,
            this.layoutControlItem28,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(501, 172);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.sbWarehousingSet;
            this.layoutControlItem27.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.apply_16x162;
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem27.MaxSize = new System.Drawing.Size(240, 50);
            this.layoutControlItem27.MinSize = new System.Drawing.Size(240, 50);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(240, 50);
            this.layoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem27.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem27.TextToControlDistance = 0;
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.sbReleaseSet;
            this.layoutControlItem28.Location = new System.Drawing.Point(240, 0);
            this.layoutControlItem28.MaxSize = new System.Drawing.Size(240, 50);
            this.layoutControlItem28.MinSize = new System.Drawing.Size(240, 50);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(241, 50);
            this.layoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem28.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextToControlDistance = 0;
            this.layoutControlItem28.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbConsigned;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem4.ImageOptions.Image")));
            this.layoutControlItem4.Location = new System.Drawing.Point(360, 50);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(121, 102);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem27";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbRelease;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem3.ImageOptions.Image")));
            this.layoutControlItem3.Location = new System.Drawing.Point(240, 50);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(120, 102);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem27";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            // 
            // sbWarehousing
            // 
            this.sbWarehousing.Appearance.BackColor = System.Drawing.Color.LightBlue;
            this.sbWarehousing.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbWarehousing.Appearance.Options.UseBackColor = true;
            this.sbWarehousing.Appearance.Options.UseFont = true;
            this.sbWarehousing.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbWarehousing1.ImageOptions.Image")));
            this.sbWarehousing.Location = new System.Drawing.Point(12, 62);
            this.sbWarehousing.Name = "sbWarehousing";
            this.sbWarehousing.Size = new System.Drawing.Size(116, 96);
            this.sbWarehousing.StyleController = this.layoutControl1;
            this.sbWarehousing.TabIndex = 5;
            this.sbWarehousing.Text = "일반입고";
            this.sbWarehousing.Click += new System.EventHandler(this.sbWarehousing_Click_1);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbWarehousing;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem1.ImageOptions.Image")));
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(120, 102);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem27";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            // 
            // sbWarehousingConsigned
            // 
            this.sbWarehousingConsigned.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.sbWarehousingConsigned.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbWarehousingConsigned.Appearance.Options.UseBackColor = true;
            this.sbWarehousingConsigned.Appearance.Options.UseFont = true;
            this.sbWarehousingConsigned.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbWarehousing2.ImageOptions.Image")));
            this.sbWarehousingConsigned.Location = new System.Drawing.Point(132, 62);
            this.sbWarehousingConsigned.Name = "sbWarehousingConsigned";
            this.sbWarehousingConsigned.Size = new System.Drawing.Size(116, 96);
            this.sbWarehousingConsigned.StyleController = this.layoutControl1;
            this.sbWarehousingConsigned.TabIndex = 5;
            this.sbWarehousingConsigned.Text = "생산대행";
            this.sbWarehousingConsigned.Click += new System.EventHandler(this.sbWarehousingConsigned_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbWarehousingConsigned;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem2.ImageOptions.Image")));
            this.layoutControlItem2.Location = new System.Drawing.Point(120, 50);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(120, 100);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(120, 102);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem27";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            // 
            // DlgSelectRepresentativeType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 172);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgSelectRepresentativeType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "입출고 선택";
            this.Load += new System.EventHandler(this.DlgMonitorCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbReleaseSet;
        private DevExpress.XtraEditors.SimpleButton sbWarehousingSet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraEditors.SimpleButton sbRelease;
        private DevExpress.XtraEditors.SimpleButton sbConsigned;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton sbWarehousing;
        private DevExpress.XtraEditors.SimpleButton sbWarehousingConsigned;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}